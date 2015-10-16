using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
namespace SOSOshop.BLL.Service
{
    /// <summary>
    /// 订单消息处理
    /// </summary>
    public class I_ERP_GXDDHZ_NOTE
    {
        /// <summary>
        /// 消息处理订单状态
        /// </summary>
        /// <param name="actionId"></param>
        public static void Syn(string actionId)
        {
            Database dberp = DatabaseFactory.CreateDatabase("ConnectionStringERP");
            Database db = DatabaseFactory.CreateDatabase("ConnectionString");
            string sql = "SELECT is_zx,is_caigou FROM gxddhz WHERE webdjbh='" + actionId + "'";
            DataTable dt = dberp.ExecuteDataSet(dberp.GetSqlStringCommand(sql)).Tables[0];
            if (dt.Rows.Count > 0)
            {
                var dr = dt.Rows[0];
                if (dr["is_zx"].ToString().Trim() == "清")
                {
                    using (DbConnection conn = db.CreateConnection())
                    {
                        conn.Open();
                        DbTransaction tran = conn.BeginTransaction();
                        try
                        {
                            db.ExecuteNonQuery(db.GetSqlStringCommand("UPDATE dbo.Orders SET OrderStatus=-2 WHERE OrderId='" + actionId + "'"), tran);
                            db.ExecuteNonQuery(db.GetSqlStringCommand("UPDATE dbo.OrderProduct SET Status=6 WHERE OrderId='" + actionId + "'"), tran);
                            db.ExecuteNonQuery(db.GetSqlStringCommand("DELETE OrdersMQ WHERE orderid='" + actionId + "'"), tran);
                            tran.Commit();
                        }
                        catch (Exception e)
                        {
                            tran.Rollback();
                            throw e;
                        }
                    }
                }
                else
                {
                    sql = "SELECT b.product_id,a.is_zx,a.is_caigou,is_ck,is_fh FROM gxddmx a LEFT JOIN dbo.spzl b ON a.spid=b.spid WHERE djbh =(SELECT djbh FROM gxddhz WHERE webdjbh='" + actionId + "')";
                    DataTable dt2 = dberp.ExecuteDataSet(dberp.GetSqlStringCommand(sql)).Tables[0];
                    foreach (DataRow item in dt2.Rows)
                    {
                        if (item["is_fh"].ToString().Trim() == "是")//is_fh 是 已经发货。9＝货物已发出,10＝已收货
                        {
                            db.ExecuteNonQuery(db.GetSqlStringCommand("UPDATE dbo.OrderProduct SET Status=9 WHERE OrderId='" + actionId + "' and ProId=(SELECT Product_ID FROM dbo.Product WHERE Product_ID_02=" + item["product_id"] + ")"));
                        }
                        else if (item["is_ck"].ToString().Trim() == "是")//是 已经出库但没有发货。 8＝已出库待发运 (现在由于软件中不会作出发货操作没有发货状态，所以目前只要已出库就算已经发货则发短信)
                        {
                            db.ExecuteNonQuery(db.GetSqlStringCommand("UPDATE dbo.OrderProduct SET Status=8 WHERE OrderId='" + actionId + "' and ProId=(SELECT Product_ID FROM dbo.Product WHERE Product_ID_02=" + item["product_id"] + ")"));
                            #region 标记订单已完成
                            sql = "SELECT COUNT(*) FROM OrderProduct WHERE OrderId='" + actionId + "' AND Status<>8 AND Status<>6";
                            if (0 == (int)db.ExecuteScalar(db.GetSqlStringCommand(sql)))
                            {
                                sql = "UPDATE dbo.Orders SET OrderStatus=4,OrderDate=GETDATE() WHERE OrderId='" + actionId + "'";
                                db.ExecuteNonQuery(db.GetSqlStringCommand(sql));
                            }
                            #endregion
                            #region 发送已发货短信
                            MsgLog bll = new MsgLog();
                            if (!bll.isfhsend(actionId))
                            {
                                string username = db.ExecuteScalar(db.GetSqlStringCommand("SELECT TrueName FROM dbo.memberinfo WHERE UID=(SELECT ReceiverId  FROM dbo.Orders WHERE OrderId='" + actionId + "')")) as string;
                                string mobile = db.ExecuteScalar(db.GetSqlStringCommand("SELECT ConsigneePhone FROM dbo.Orders WHERE OrderId='" + actionId + "'")) as string;
                                Sms.SendAndSaveDataBase(mobile, string.Format("{0}，您的订单（单号：{1}）已出库发货，正在配送中，正常情况24小时内到货，请保持通信畅通，注意查收", username, Getid(actionId)));
                                bll.orderid = actionId;
                                bll.fhsend = true;
                                bll.Insert();
                            }
                            #endregion
                        }
                        else if (item["is_zx"].ToString().Trim() == "是")//是 代表已经采购  7＝已申请出库
                        {
                            db.ExecuteNonQuery(db.GetSqlStringCommand("UPDATE dbo.OrderProduct SET Status=7 WHERE OrderId='" + actionId + "' and ProId=(SELECT Product_ID FROM dbo.Product WHERE Product_ID_02=" + item["product_id"] + ")"));
                        }
                        else if (item["is_zx"].ToString().Trim() == "清")//清 代表取消订单中的此商品
                        {
                            db.ExecuteNonQuery(db.GetSqlStringCommand("UPDATE dbo.OrderProduct SET Status=6 WHERE OrderId='" + actionId + "' and ProId=(SELECT Product_ID FROM dbo.Product WHERE Product_ID_02=" + item["product_id"] + ")"));
                        }
                    }
                    //订单取消
                    sql = "SELECT COUNT(*) FROM dbo.OrderProduct WHERE Status<>6 AND OrderId='" + actionId + "'";
                    if (0 == (int)db.ExecuteScalar(db.GetSqlStringCommand(sql)))
                    {
                        db.ExecuteNonQuery(db.GetSqlStringCommand("UPDATE dbo.Orders SET OrderStatus=-2 WHERE OrderId='" + actionId + "'"));
                        return;
                    }
                    //订单完成
                    sql = "SELECT COUNT(*) FROM dbo.OrderProduct WHERE Status<>8  AND Status<>6  AND Status<>9  AND Status<>10 AND OrderId='" + actionId + "'";
                    if (0 == (int)db.ExecuteScalar(db.GetSqlStringCommand(sql)))
                    {
                        db.ExecuteNonQuery(db.GetSqlStringCommand("UPDATE dbo.Orders SET OrderStatus=4 WHERE OrderId='" + actionId + "'"));
                        //订单完成后加积分
                        new SOSOshop.BLL.Integral.MemberIntegral().OrderSucceed(actionId);
                    }
                }
            }
            else
            {
                throw new Exception("商城系统中不存在此订单号:" + actionId);
            }
        }

        public static string Getid(string orderid)
        {
            if (orderid.Contains("-"))
            {
                orderid = orderid.Split('-')[0];
            }
            return orderid;
        }
    }
}
