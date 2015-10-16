using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace ErpShopService.BLL
{
    public class Command
    {
        /// <summary>
        /// 认证key
        /// </summary>
        public static string key = "7307C5E7-CE7F-43DC-B3C9-520C76CAC4CA";
        /// <summary>
        /// 导入订单到erp对接数据库
        /// </summary>
        public void ImportOrderList()
        {
            var bll = new com.APISoapClient();
            var list = bll.GetOrderList(key);
            var op = new Orders();
            foreach (var item in list)
            {
                using (DbConnection conn = op._db.CreateConnection())
                {
                    conn.Open();
                    DbTransaction tran = conn.BeginTransaction();
                    try
                    {
                        op.AddOrders(item.order, item.salesman, tran);
                        foreach (var item2 in item.li)
                        {
                            op.AddOrderProduct(item2, tran);
                        }
                        bll.UpdateOrdersMQ_1(item.order.OrderId, key);
                        tran.Commit();
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        AddLog(e.ToString());
                    }
                }
            }
        }
        /// <summary>
        /// 发送会员信息至商城
        /// </summary>
        public void SendMemberinfo()
        {
            DbBase db = new DbBase();
            com.APISoapClient bll = new com.APISoapClient();
            string SQL_Memberinfo = System.Configuration.ConfigurationManager.AppSettings["SQL_Memberinfo"];
            try
            {
                bll.SendMember(db.ExecuteDataSet(SQL_Memberinfo), key);
            }
            catch (Exception ex)
            {
                AddLog(ex.ToString());
            }
        }

        /// <summary>
        /// 发送订单状态变化至商城
        /// </summary>
        public void SendOrderStatus()
        {
            com.APISoapClient bll = new com.APISoapClient();
            DbBase db = new DbBase();

            string sql = System.Configuration.ConfigurationManager.AppSettings["SQL_I_ERP_OrderStatus_NOTE"]; 
            foreach (DataRow item in db.ExecuteTable(sql).Rows)
            {
                try
                {
                    bll.UpdateOrderStatusForErp((string)item["orderid"], (string)item["proid"], (int)item["Status"], key);
                    db.ExecuteNonQuery(string.Format("UPDATE I_ERP_OrderStatus_NOTE SET handleStatus=1,handleTime=GETDATE() WHERE id={0}", item["id"]));
                }
                catch (Exception e)
                {
                    sql = string.Format("UPDATE I_ERP_OrderStatus_NOTE SET handleStatus=2,handleTime=GETDATE(),notes=@notes WHERE id={0}", item["id"]);
                    var dbCommand = db._db.GetSqlStringCommand(sql);
                    db._db.AddInParameter(dbCommand, "notes", DbType.String, e.ToString());
                    db._db.ExecuteNonQuery(dbCommand);
                }
            }

        }
        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="msg"></param>
        public void AddLog(string msg)
        {
            string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\Log";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            System.IO.File.AppendAllText(string.Format("{0}\\{1:yyyy-MM-dd}.log", path, DateTime.Now), DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ":" + msg + "\r\n\r\n\r\n\r\n");
        }

        /// <summary>
        /// 同步近效期产品
        /// </summary>
        public void SyncExpritationProduct()
        {
            DbBase db = new DbBase();
          
            com.APISoapClient bll = new com.APISoapClient();
            string sql = System.Configuration.ConfigurationManager.AppSettings["SQL_Product_ExpirationTime"];
            DataSet ds = db.ExecuteDataSet(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                //调用web服务
                bll.AddExpirationTimeProduct(ds, key);
            }

        }

        /// <summary>
        /// 同步会员经营范围
        /// </summary>
        public void SyncMemberBusinessScope()
        {
            DbBase db = new DbBase();
            string sql = System.Configuration.ConfigurationManager.AppSettings["SQL_MemberBusinessScope"];
            
            com.APISoapClient bll = new com.APISoapClient();

            DataSet ds = db.ExecuteDataSet(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                //调用web服务
                bll.SyncMemberBusinessScope(ds, key);
            }

        }
    }
}
