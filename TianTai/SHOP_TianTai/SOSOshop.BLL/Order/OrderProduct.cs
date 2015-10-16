using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Linq;
using System.Data.Common;
using SOSOshop.BLL.Common;
namespace SOSOshop.BLL.Order
{
    /// <summary>
    /// 订单商品数据访问类
    /// </summary>
    public partial class OrderProduct : Db
    {
        public OrderProduct()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from OrderProduct where Id=@Id ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Id", DbType.Int32, Id);
            int cmdresult;
            object obj = db.ExecuteScalar(dbCommand);
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SOSOshop.Model.Order.OrderProduct model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OrderProduct set ");
            strSql.Append("OrderId=@OrderId,");
            strSql.Append("ProId=@ProId,");
            strSql.Append("ProName=@ProName,");
            strSql.Append("ProPrice=@ProPrice,");
            strSql.Append("ProNum=@ProNum,");
            strSql.Append("AddTime=@AddTime,");
            strSql.Append("pro_pno=@pro_pno,");
            strSql.Append("pro_pdate=@pro_pdate,");
            strSql.Append("Status=@Status");
            strSql.Append(" where Id=@Id ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Id", DbType.Int32, model.Id);
            db.AddInParameter(dbCommand, "OrderId", DbType.AnsiString, model.OrderId);
            db.AddInParameter(dbCommand, "ProId", DbType.Int32, model.ProId);
            db.AddInParameter(dbCommand, "ProName", DbType.AnsiString, model.ProName);
            db.AddInParameter(dbCommand, "ProPrice", DbType.Double, model.ProPrice);
            db.AddInParameter(dbCommand, "ProNum", DbType.Double, model.ProNum);
            db.AddInParameter(dbCommand, "AddTime", DbType.DateTime, model.AddTime);
            db.AddInParameter(dbCommand, "pro_pno", DbType.AnsiString, model.pro_pno);
            db.AddInParameter(dbCommand, "pro_pdate", DbType.AnsiString, model.pro_pdate);
            db.AddInParameter(dbCommand, "Status", DbType.Int32, model.Status);
            int rows = db.ExecuteNonQuery(dbCommand);

            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from OrderProduct ");
            strSql.Append(" where Id=@Id ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Id", DbType.Int32, Id);
            int rows = db.ExecuteNonQuery(dbCommand);

            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SOSOshop.Model.Order.OrderProduct GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from OrderProduct ");
            strSql.AppendFormat(" where Id={0} ", Id);
            return db.ExecuteSqlStringAccessor<SOSOshop.Model.Order.OrderProduct>(strSql.ToString()).First();
        }
        #endregion  Method

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SOSOshop.Model.Order.OrderProduct model, DbTransaction tran)
        {
            //重新计算订单原始明细
            //DELETE OrderProductDetails WHERE ProId IN (SELECT Product_ID FROM dbo.Product)
            //Insert into OrderProductDetails(OrderId,ProId,dbo.DrugsBase_Manufacturer,guige,jianzhuang) SELECT a.OrderId,a.ProId,b.DrugsBase_Manufacturer,c.shpgg,CONVERT(VARCHAR(11),b.Goods_Pcs)+b.Goods_Unit FROM  dbo.OrderProduct a INNER JOIN dbo.Product b ON a.ProId=b.Product_ID INNER JOIN dbo.spzl c ON b.Product_ID=c.product_id WHERE a.ProId IN (SELECT Product_ID FROM dbo.Product)
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into OrderProduct(");
            strSql.Append("OrderId,ProId,ProName,ProPrice,ProNum,AddTime,pro_pno,pro_pdate,Status,issplit,iden,jigid,IsExpirationProduct)");

            strSql.Append(" values (");
            strSql.Append("@OrderId,@ProId,@ProName,@ProPrice,@ProNum,@AddTime,@pro_pno,@pro_pdate,@Status,@issplit,@iden,@jigid,@IsExpirationProduct)");
            strSql.AppendFormat(";Insert into OrderProductDetails(OrderId,ProId,dbo.DrugsBase_Manufacturer,guige,jianzhuang) SELECT '{0}' OrderId,{1} ProId,b.DrugsBase_Manufacturer,null,CONVERT(VARCHAR(11),b.Goods_Pcs)+b.Goods_Unit FROM  dbo.Product b WHERE b.product_id={1}", model.OrderId, model.ProId);
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "OrderId", DbType.AnsiString, model.OrderId);
            db.AddInParameter(dbCommand, "ProId", DbType.Int32, model.ProId);
            db.AddInParameter(dbCommand, "ProName", DbType.AnsiString, model.ProName);
            db.AddInParameter(dbCommand, "ProPrice", DbType.Double, model.ProPrice);
            db.AddInParameter(dbCommand, "ProNum", DbType.Double, model.ProNum);
            db.AddInParameter(dbCommand, "AddTime", DbType.DateTime, model.AddTime);
            db.AddInParameter(dbCommand, "pro_pno", DbType.AnsiString, model.pro_pno);
            db.AddInParameter(dbCommand, "pro_pdate", DbType.AnsiString, model.pro_pdate);
            db.AddInParameter(dbCommand, "Status", DbType.Int32, model.Status);
            db.AddInParameter(dbCommand, "issplit", DbType.Boolean, model.issplit);
            db.AddInParameter(dbCommand, "iden", DbType.Int32, model.iden);
            db.AddInParameter(dbCommand, "jigid", DbType.String, model.jigid);
            db.AddInParameter(dbCommand, "IsExpirationProduct", DbType.Byte, model.IsExpirationProduct);
            db.ExecuteScalar(dbCommand, tran);
        }
        /// <summary>
        /// 根据订单号取得订单下面所有的商品
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="type">0原始订单(被拆分过) 非 0订单</param>
        /// <returns></returns>
        public DataTable GetList(string orderId, int type = -1)
        {
            System.Text.StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT a.*,c.describe,b.DrugsBase_Manufacturer,b.DrugsBase_Specification,b.Goods_ConveRatio_Unit_Name,b.Goods_ConveRatio_Unit,b.Goods_ConveRatio,b.Goods_Unit,b.spid,b.Product_ID_02 FROM OrderProduct a LEFT JOIN dbo.product_online_v_1 b ON a.ProId=b.Product_ID LEFT JOIN OrderProduct_Stockout c ON a.Id=c.OrderProduct_id WHERE OrderId='{0}'", Library.Lang.Input.Filter(orderId));
            if (type != -1)
            {
                sql.AppendFormat(" and issplit=" + type);
            }

            DataTable dt = ExecuteTable(sql.ToString());
            dt.Columns.Add("Specification", typeof(string));
            foreach (DataRow item in dt.Rows)
            {
                item["Specification"] = GetSpecification(item);
            }
            return dt;
        }
        /// <summary>
        /// 取得订单总价格
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public decimal GetTotalPrices(string orderid)
        {
            try
            {
                return (decimal)base.ExecuteScalar(string.Format("SELECT SUM(ProNum*ProPrice) FROM dbo.OrderProduct where orderid='{0}'", orderid));
            }
            catch (Exception)
            {
                return (decimal)base.ExecuteScalar(string.Format("SELECT SUM(ProNum*ProPrice) FROM dbo.OrderProduct where orderid like('{0}%')", orderid));
            }

        }
        /// <summary>
        /// 取得规格显示方式
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static string GetSpecification(DataRow dr)
        {

            var item = dr;
            object pcs = "";
            if (Library.Lang.DataValidator.isNULL(item["Goods_ConveRatio_Unit_Name"]))
            {
                if (Library.Lang.DataValidator.isNULL(item["Goods_ConveRatio_Unit"]))
                {
                    pcs = string.Format("{0}{1}", item["Goods_ConveRatio"], item["Goods_Unit"]);
                }
                else
                {
                    if (Library.Lang.DataValidator.isNULL(item["Goods_Unit"] as string))
                    {
                        pcs = string.Format("{0}{1}", item["Goods_ConveRatio"], item["Goods_ConveRatio_Unit"]);
                    }
                    else
                    {
                        pcs = string.Format("{0}{1}/{2}", item["Goods_ConveRatio"], item["Goods_ConveRatio_Unit"], item["Goods_Unit"]);
                    }
                }
            }
            else
            {
                pcs = item["Goods_ConveRatio_Unit_Name"];
            }
            return pcs.ToString();
        }

        /// <summary>
        /// 更改商品状态
        /// </summary>
        /// <param name="id">订单商品id</param>
        /// <param name="orderid">订单id</param>
        /// <param name="status">订单状态 4:已预购，6：取消商品</param>
        /// <returns></returns>
        public bool UpdateShop(string id, string orderid, int status)
        {

            //判断可否变更商品状态
            int s = (int)base.ExecuteScalar("SELECT Status FROM dbo.OrderProduct WHERE Id={0}", id);
            if (s != (int)Enums.OrderProductStatus.Submit && s != 3)
            {
                throw new Exception("订单商品状态已发生改变！");
            }
            base.ExecuteNonQuery("UPDATE dbo.OrderProduct SET Status=" + status + " WHERE id=" + id);
            Orders bll = new Orders();
            //订单商品已经设置为全部取消，则取消订单
            if (0 == (int)base.ExecuteScalar(string.Format("SELECT (SELECT COUNT(*) FROM dbo.OrderProduct WHERE OrderId='{0}')-(SELECT COUNT(*) FROM dbo.OrderProduct WHERE OrderId='{0}' AND Status in (5,6))", orderid)))
            {
                bll.CancelOrder(orderid);
            }
            else//否则则执行分单
            {
                //bll.SplitOrder(orderid, false);
            }
            return true;
        }
        //手动更新订单商品状态(手动完单)
        public bool UpdateShopProd(string id, string orderid, int status)
        {

            base.ExecuteNonQuery("UPDATE dbo.OrderProduct SET Status=" + status + " WHERE id=" + id);
            //订单取消
            string sql = "SELECT COUNT(*) FROM dbo.OrderProduct WHERE Status<>6 AND OrderId='" + orderid + "'";
            if (0 == (int)db.ExecuteScalar(db.GetSqlStringCommand(sql)))
            {
                db.ExecuteNonQuery(db.GetSqlStringCommand("UPDATE dbo.Orders SET OrderStatus=-2 WHERE OrderId='" + orderid + "'"));
                return true;
            }
            //订单完成
            sql = "SELECT COUNT(*) FROM dbo.OrderProduct WHERE Status<>8  AND Status<>6  AND Status<>9  AND Status<>10 AND OrderId='" + orderid + "'";
            if (0 == (int)db.ExecuteScalar(db.GetSqlStringCommand(sql)))
            {
                db.ExecuteNonQuery(db.GetSqlStringCommand("UPDATE dbo.Orders SET OrderStatus=4 WHERE OrderId='" + orderid + "'"));
                //订单完成后加积分
                new SOSOshop.BLL.Integral.MemberIntegral().OrderSucceed(orderid);
            }
            return true;
        }
    }
}

