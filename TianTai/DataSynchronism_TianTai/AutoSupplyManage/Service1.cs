using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Memcached.ClientLibrary;
using System.Configuration;
using System.Reflection;

namespace AutoSupplyManage
{
    public partial class Service1 : ServiceBase
    {

        public Service1()
        {
            InitializeComponent();

            //每隔十分钟执行一次数据变更
            BackgroundWorker b1 = new BackgroundWorker();
            b1.DoWork += b1_DoWork;
            b1.RunWorkerAsync();

            //定时执行一些任务
            BackgroundWorker b2 = new BackgroundWorker();
            b2.DoWork += b2_DoWork;
            b2.RunWorkerAsync();

        }
        /// <summary>
        /// 处理数据变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void b1_DoWork(object sender, DoWorkEventArgs e)
        {

            //System.Threading.Thread.Sleep(new System.Random().Next(1000 * 60 * 3));            
            while (true)
            {
                //晚上8点后，到早上7点前不执行数据同步
                if (DateTime.Now.Hour > 20 || DateTime.Now.Hour < 7)
                {
                    goto lable;
                }
                try
                {
                    DSWebService.BLL.SynLock ck = new DSWebService.BLL.SynLock();
                    ck.Lock();
                    //System.Threading.Thread.Sleep(1000 * 60 * 1);
                    DSWebService.BLL.Data_Centre.Price p = new DSWebService.BLL.Data_Centre.Price();
                    p.InitPrice();
                    DSWebService.BLL.Data_Centre.Product_Centre bll = new DSWebService.BLL.Data_Centre.Product_Centre();
                    DSWebService.BLL.Data_Centre.Config con = new DSWebService.BLL.Data_Centre.Config();

                    foreach (var item in con.GetAllList())
                    {
                        bll.IdenData(int.Parse(item.id));
                    }

                    foreach (var item in con.GetAllList())
                    {
                        int iden = int.Parse(item.id);
                        bll.InitializeData(iden);
                        bll.InitializeDataNotfiling(iden);
                    }
                    ck.UnLock();
                }
                catch (Exception ex)
                {
                    DSWebService.BLL.Log.AddLog(ex.ToString(), 500);
                    try
                    {
                        DSWebService.BLL.SynLock ck = new DSWebService.BLL.SynLock();
                        ck.UnLock();
                    }
                    catch { }
                }
            lable:
                System.Threading.Thread.Sleep(1000 * 60 * 10);

            }
        }
        /// <summary>       
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void b2_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {
                    int h = DateTime.Now.Hour;
                    if (h >= 3)
                    {
                        DSWebService.BLL.Data_Centre.AutoSupplySwitchLog assl = new DSWebService.BLL.Data_Centre.AutoSupplySwitchLog();
                        if (assl.IsExcu(3))
                        {
                            DSWebService.BLL.Data_Centre.Product_Centre bll = new DSWebService.BLL.Data_Centre.Product_Centre();
                            bll.UpdatePuclic();
                            bll.initStock();
                            DSWebService.BLL.Data_Centre.Price.InitAllPrice();
                            assl.insert(3);
                        }
                    }
                }
                catch (Exception ex)
                {
                    DSWebService.BLL.Log.AddLog(ex.ToString(), 500);
                }
                System.Threading.Thread.Sleep(1000 * 60 * 3);
            }
        }



        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
    }
}
