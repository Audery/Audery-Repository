using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using ErpToDataCentreService.BLL;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using ErpToDataCentreService.SynFromServiceReference;

namespace ErpToDataCentreService
{
    public partial class Service1 : ServiceBase
    {
        MyTimer[] mt;
        string key = "B3JFIIINFJAI8W";
        string EnterpriseID = System.Configuration.ConfigurationManager.AppSettings["EnterpriseID"];

        #region 初始化服务
        string exeLocation = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        public Service1()
        {
            ServiceName = Installer1.ServiceName;
            InitializeComponent();
        }



        #region 初始化服务事件
        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            StartAfter(1000);
            WriteLog(ServiceName + "  服务开始运行");
        }
        protected override void OnContinue()
        {
            base.OnContinue();
            StartAfter(1000);
            WriteLog(ServiceName + "  服务重新启动");
        }
        protected override void OnStop()
        {
            base.OnStop();
            WriteLog(ServiceName + "  服务停止");
        }
        private void StartAfter(double Interval)
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = Interval;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(delegate(object source, System.Timers.ElapsedEventArgs e)
            {
                timer.Stop();
                Init();
            });
            timer.Start();
        }
        #endregion

        /// <summary>
        /// 初始化服务的工作线程
        /// </summary>
        protected void Init()
        {
            while (true)
            {
                bool t = false;
                try
                {
                    if (!string.IsNullOrEmpty(EnterpriseID))
                    {
                        TableSyn[] ts = null;
                        try
                        {
                            ts = GetTableList();
                            t = true;
                        }
                        catch (Exception ex)
                        {
                            t = false;
                            WriteLog("异常 : " + ex.ToString() + "");
                        }
                        if (ts != null)
                        {
                            mt = new MyTimer[ts.Length];
                            for (int i = 0; i < ts.Length; i++)
                            {
                                mt[i] = new MyTimer();
                                mt[i].i = i;
                                mt[i].Interval = ts[i].interval;
                                mt[i].Elapsed += new System.Timers.ElapsedEventHandler(timer1_Tick);
                                mt[i].AutoReset = true;
                                mt[i].Enabled = true;
                                mt[i].ts = ts[i];
                            }
                            WriteLog(ServiceName + "  服务配置成功");
                        }
                    }
                    else
                    {
                        //未配置服务
                        WriteLog("未配置服务 ");
                    }
                }
                catch (Exception ex)
                {
                    WriteLog("异常 : " + ex.ToString() + "");
                }
                if (t) break;
                System.Threading.Thread.Sleep(1000);
            }
        }
        #endregion

        #region 取得要同步的表
        private TableSyn[] GetTableList()
        {
            List<TableSyn> li = new List<TableSyn>();
            if (!string.IsNullOrEmpty(EnterpriseID))
            {
                // string[] tables = "商品数据,客户信息,限销数据,促销商品数据".Split(',');

                string[] tables = "商品数据,商品库存,商品价格".Split(',');
                foreach (string table in tables)
                {
                    string sql = System.Configuration.ConfigurationManager.AppSettings[table];
                    if (!string.IsNullOrEmpty(sql) && sql.Trim() != string.Empty)
                    {
                        string[] sqls = sql.Trim().Split("；".ToCharArray());
                        if (!sqls[0].Contains(" "))
                        {
                            sql = "SELECT * FROM " + sqls[0];
                        }
                        else
                        {
                            sql = sqls[0];
                        }
                        int interval = 0; if (sqls.Length > 1) int.TryParse(sqls[1], out interval);
                        if (interval < 60) interval = 60;//最少一分钟
                        if (interval > 60 * 60 * 12) interval = 60 * 60 * 12;//最多半天
                        li.Add(new TableSyn()
                                {
                                    EnterpriseID = EnterpriseID,
                                    TableName = table,
                                    sql = sql,
                                    interval = 1000 * interval
                                });
                    }
                }
            }
            return li.ToArray();
        }
        #endregion

        #region 处理要同步的表
        private void timer1_Tick(object sender, EventArgs e)
        {

            MyTimer mt = sender as MyTimer;
            if (!this.mt[mt.i].locked)
            {

                DateTime startTime = DateTime.Now;
                this.mt[mt.i].Stop();
                this.mt[mt.i].locked = true;
                try
                {
                    SynFromServiceReference.synFromService1SoapClient bll = new SynFromServiceReference.synFromService1SoapClient();

                    string tsColumns = bll.GetDataColumn(mt.ts, key);//取得表的字段配置
                    //WriteLog("同步" + mt.ts.TableName + "(开始取数据: " + tsColumns + ")");
                    if (!string.IsNullOrEmpty(tsColumns))
                    {
                        DataSet ds = new DataSet();
                        DbHelperSQL db = new DbHelperSQL();
                        if (!string.IsNullOrEmpty(mt.ts.sql))
                        {
                            ds = db.GetDS(mt.ts.sql);
                            //WriteLog("同步" + mt.ts.TableName + "(取数据成功)");
                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                #region 检查字段
                                List<string> err = new List<string>();
                                List<string> err2 = new List<string>();
                                var ts = tsColumns.Split('|');
                                foreach (var item in ts)
                                {
                                    if (!ds.Tables[0].Columns.Contains(item))
                                    {
                                        err.Add(item);
                                    }
                                }
                                foreach (DataColumn item in ds.Tables[0].Columns)
                                {
                                    if (!ts.Contains(item.ColumnName))
                                    {
                                        err2.Add(item.ColumnName);
                                    }
                                }
                                #endregion
                                WriteLog("同步" + mt.ts.TableName + "(检查数据完成)");
                                //开始调用web服务
                                if (err.Count > 0)
                                {
                                    bll.AddLog(string.Format("同步表:{0}，发生异常，缺少字段:{1}", mt.ts.TableName, string.Join(",", err)), 0);
                                }
                                //开始调用web服务
                                if (err2.Count > 0)
                                {
                                    bll.AddLog(string.Format("同步表:{0}，发生异常，多余字段:{1}", mt.ts.TableName, string.Join(",", err2)), 0);
                                }
                                else
                                {
                                    if (!ds.Tables[0].Columns.Contains("iden"))
                                    {
                                        var colu = new DataColumn("iden", typeof(int));
                                        colu.DefaultValue = int.Parse(EnterpriseID);
                                        ds.Tables[0].Columns.Add(colu);
                                    }
                                    bll.Add(mt.ts, BLL.Utils.GetZipBytesByDataSet(ds), key);
                                }
                                //本地日志
                                WriteLog("同步" + mt.ts.TableName + "(" + ds.Tables[0].Rows.Count + "条记录) ：成功");
                            }
                            else
                            {
                                WriteLog("同步" + mt.ts.TableName + "(0条记录)");
                            }
                        }
                        else
                        {
                            WriteLog("同步" + mt.ts.TableName + "(sql查询语句异常)");
                        }
                    }
                }
                catch (Exception ex)
                {
                    //本地日志
                    WriteLog("同步" + mt.ts.TableName + " ：" + ex.Message);
                }
                this.mt[mt.i].locked = false;
                this.mt[mt.i].Start();
            }
        }
        #endregion

        #region 本地日志
        public void WriteLog(string ActionName)
        {
            //return;
            string content = ActionName + "\t\t" + DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + "\n";
            string path = exeLocation + "\\" + DateTime.Now.ToString(" yyyy-MM-dd") + ".txt";
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
            StreamWriter m_streamWriter = new StreamWriter(fs);
            m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
            m_streamWriter.WriteLine(content);
            m_streamWriter.Flush();
            m_streamWriter.Close();
            fs.Close();
        }
        #endregion
    }
    public class MyTimer : System.Timers.Timer
    {
        public int i;
        public bool locked;
        public TableSyn ts { get; set; }
    }
}
