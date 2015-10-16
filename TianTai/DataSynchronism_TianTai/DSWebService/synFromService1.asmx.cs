using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MongoDB;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

namespace DSWebService
{
    /// <summary>
    /// synFromService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class synFromService1 : System.Web.Services.WebService
    {

        string dkey = "B3JFIIINFJAI8W";

        /// <summary>
        /// 添加日志
        /// </summary>
        [WebMethod]
        public void AddLog(string msg, int type)
        {
            if (msg == "请求同步表:商品数据10000 v1.0") return;
            if (msg.Contains("请求同步表:商品数据1000")) return;
            BLL.Log bll = new BLL.Log();
            bll.created = DateTime.Now;
            bll.describe = msg;
            bll.detail = "";
            bll.ip = HttpContext.Current.Request.UserHostAddress;
            bll.source = HttpContext.Current.Request.Url.ToString();
            bll.type = type;
            if (msg == "更新请求timer1_Tick！")
            {
                bll.Delete(msg);
            }
            if (msg.Contains("请求同步表"))
            {
                bll.Delete(msg);
            }
            bll.insert(bll);
        }

        /// <summary>
        /// 取得操作的表的字段定义
        /// </summary>
        /// <param name="ts"></param>
        /// <param name="key"></param>
        /// <returns>表的字段</returns>
        [WebMethod]
        public string GetDataColumn(TableSyn ts, string key)
        {
            string s = "";
            if (dkey.Equals(key))
            {
                switch (ts.TableName)
                {
                    case "商品数据":
                        {
                            //s = "商品编号|批准文号|厂家|通用名|规格|剂型|包装单位|转换比|件装|中包装|生产日期|产地|类别|片型|等级|品牌|制法";
                            s = "商品编号|批准文号|厂家|通用名|规格|剂型|包装单位|转换比|件装|中包装|经营范围|条码";
                            break;
                        }
                    case "商品价格":
                        {
                            s = "商品编号|价格类型|商品价格";
                            break;
                        }
                    case "商品库存":
                        {
                            s = "商品编号|商品库存|批号|效期";
                            break;
                        }
                }
            }
            return s;
        }


        /// <summary>
        /// 新增一批数据
        /// </summary>
        /// <param name="te"></param>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [WebMethod]
        public int Add(TableSyn ts, byte[] data, string key)
        {
            if (dkey.Equals(key))
            {
                string ekey = string.Format("{0}_{1}", ts.TableName, ts.EnterpriseID);
                if (get(ekey) != null)
                {

                    if ((DateTime.Now - (DateTime)get(ekey)).TotalMinutes <= 5)
                    {
                        return 0;
                    }
                    set(ekey, DateTime.Now);
                }
                else
                {
                    set(ekey, DateTime.Now);
                }


                BLL.Data_Centre.Product_Centre logBll = new BLL.Data_Centre.Product_Centre();
                BLL.DbBase db = new BLL.DbBase();
                db.ChangeDBData_Centre();
                DateTime now = DateTime.Now;
                int iden = 0; int.TryParse(ts.EnterpriseID, out iden);//公司来源                              

                #region 开始同步并检查数据
                BLL.Data_Centre.Product_Centre.AddLog("请求数据同步,企业编号:" + iden, 1);
                DSWebService.BLL.ServiceState sstate = new BLL.ServiceState();
                sstate.iden = iden.ToString();
                sstate.UpdateTime = DateTime.Now;
                sstate.insert();

                var ds = BLL.Utils.GetDataSetByZipBytes(data);//数据来源
                if (iden <= 0 || ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0) return 0;
                DataColumnCollection sourceColumns = ds.Tables[0].Columns;
                #endregion
                DateTime now1 = DateTime.Now;//计时
                try
                {
                    switch (ts.TableName)
                    {
                        case "商品数据":
                            {
                                foreach (DataColumn item in ds.Tables[0].Columns)
                                {
                                    switch (item.ColumnName)
                                    {
                                        case "商品编号":
                                            {
                                                item.ColumnName = "ID";
                                                continue;
                                            }
                                        case "批准文号":
                                            {
                                                item.ColumnName = "DrugsBase_ApprovalNumber";
                                                continue;
                                            }
                                        case "厂家":
                                            {
                                                item.ColumnName = "DrugsBase_Manufacturer";
                                                continue;
                                            }
                                        case "通用名":
                                            {
                                                item.ColumnName = "DrugsBase_DrugName";
                                                continue;
                                            }
                                        case "规格":
                                            {
                                                item.ColumnName = "DrugsBase_Specification";
                                                continue;
                                            }
                                        case "剂型":
                                            {
                                                item.ColumnName = "DrugsBase_Formulation";
                                                continue;
                                            }
                                        case "包装单位":
                                            {
                                                item.ColumnName = "Goods_Unit";
                                                continue;
                                            }
                                        case "转换比":
                                            {
                                                item.ColumnName = "Goods_ConveRatio";
                                                continue;
                                            }
                                        case "件装":
                                            {
                                                item.ColumnName = "Goods_Pcs";
                                                continue;
                                            }
                                        case "中包装":
                                            {
                                                item.ColumnName = "Goods_Pcs_Small";
                                                continue;
                                            }
                                        case "生产日期":
                                            {
                                                item.ColumnName = "PDate";
                                                continue;
                                            }
                                        case "产地":
                                            {
                                                item.ColumnName = "ProductionAddress";
                                                continue;
                                            }
                                        case "类别":
                                            {
                                                item.ColumnName = "ProductionClassName";
                                                continue;
                                            }
                                        case "片型":
                                            {
                                                item.ColumnName = "DrugsBase_Formulation1";
                                                continue;
                                            }
                                        case "等级":
                                            {
                                                item.ColumnName = "ProductionLevelName";
                                                continue;
                                            }
                                        case "品牌":
                                            {
                                                item.ColumnName = "ProductionBrandName";
                                                continue;
                                            }
                                        case "制法":
                                            {
                                                item.ColumnName = "ProductionMethodName";
                                                continue;
                                            }
                                        case "条码":
                                            {
                                                item.ColumnName = "Barcode";
                                                continue;
                                            }
                                        case "经营范围":
                                            {
                                                item.ColumnName = "BusinessScope";
                                                continue;
                                            }
                                    }
                                }
                                db.ExecuteNonQuery("TRUNCATE TABLE Product");
                                db.BulkToDB(ds.Tables[0], "Product");
                                BLL.Log.AddLog("Product 新增加了" + ds.Tables[0].Rows.Count + "条数据，共执行" + (int)((DateTime.Now - now1).TotalMilliseconds) + "毫秒！", 2);
                                break;
                            }
                        case "商品价格":
                            {
                                foreach (DataColumn item in ds.Tables[0].Columns)
                                {
                                    switch (item.ColumnName)
                                    {
                                        case "商品编号":
                                            {
                                                item.ColumnName = "ID";
                                                continue;
                                            }
                                        case "价格类型":
                                            {
                                                item.ColumnName = "category";
                                                continue;
                                            }
                                        case "商品价格":
                                            {
                                                item.ColumnName = "price";
                                                continue;
                                            }
                                    }

                                }
                                db.ExecuteNonQuery("TRUNCATE TABLE Price_TEMP");
                                db.BulkToDB(ds.Tables[0], "Price_TEMP");
                                BLL.Log.AddLog("Price_TEMP 新增加了" + ds.Tables[0].Rows.Count + "条数据，共执行" + (int)((DateTime.Now - now1).TotalMilliseconds) + "毫秒！", 2);
                                break;
                            }
                        case "商品库存":
                            {
                                foreach (DataColumn item in ds.Tables[0].Columns)
                                {
                                    switch (item.ColumnName)
                                    {
                                        case "商品编号":
                                            {
                                                item.ColumnName = "id";
                                                continue;
                                            }
                                        case "商品库存":
                                            {
                                                item.ColumnName = "Stock";
                                                continue;
                                            }
                                        case "批号":
                                            {
                                                item.ColumnName = "pihao";
                                                continue;
                                            }
                                        case "效期":
                                            {
                                                item.ColumnName = "sxrq";
                                                continue;
                                            }
                                    }
                                }
                                db.ExecuteNonQuery("TRUNCATE TABLE View_Stock1");
                                db.BulkToDB(ds.Tables[0], "View_Stock1");
                                BLL.Log.AddLog("View_Stock1 新增加了" + ds.Tables[0].Rows.Count + "条数据，共执行" + (int)((DateTime.Now - now1).TotalMilliseconds) + "毫秒！", 2);
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    BLL.Log.AddLog("请求数据同步,企业编号:" + iden + "," + ex.Message.ToString() + ex.StackTrace.ToString(), 0);
                }

                return 1;
            }
            return 0;
        }
        public void set(string key, object o)
        {
            Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            mc.PoolName = "Price_Cache";
            mc.Set(key, o);
        }
        public object get(string key)
        {
            Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            mc.PoolName = "Price_Cache";
            return mc.Get(key);
        }
        /// <summary>
        /// 要同步的表
        /// </summary>
        [Serializable]
        public struct TableSyn
        {
            /// <summary>
            /// 单位ID
            /// </summary>
            public string EnterpriseID { get; set; }
            /// <summary>
            /// 表的名称
            /// </summary>
            public string TableName { get; set; }
            /// <summary>
            /// 字段的名称
            /// </summary>
            public string[] ColumbName { get; set; }
            /// <summary>
            /// 执行间隔
            /// </summary>
            public int interval { get; set; }
            public string pk { get; set; }
            public string sql { get; set; }
        }

    }
}
