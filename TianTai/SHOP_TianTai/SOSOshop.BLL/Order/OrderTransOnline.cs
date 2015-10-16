using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Linq;
using Library.Data;
using System.Windows.Forms;
namespace SOSOshop.BLL.Order
{
    /// <summary>
    /// 订单在线支付网银数据访问类:OrderTransOnline
    /// </summary>
    public partial class OrderTransOnline : Db
    {
        public OrderTransOnline()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string OrderId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from OrderTransOnline where OrderId=@OrderId ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "OrderId", DbType.String, OrderId);
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
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID, string OrderId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from OrderTransOnline where ID=@ID and OrderId=@OrderId ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ID", DbType.Int32, ID);
            db.AddInParameter(dbCommand, "OrderId", DbType.String, OrderId);
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
        /// 是否存在成功交易的记录
        /// </summary>
        public bool ExistsOK(string OrderId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from OrderTransOnline where OrderId=@OrderId and TransStatus='00'");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "OrderId", DbType.String, OrderId);
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

        public int GetID(string OrderId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID from OrderTransOnline where OrderId=@OrderId ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "OrderId", DbType.String, OrderId);
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
            return cmdresult;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SOSOshop.Model.Order.OrderTransOnline model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OrderTransOnline set ");
            strSql.Append("OrderId=@OrderId,");
            strSql.Append("CustomerID=@CustomerID,");
            strSql.Append("CustomerName=@CustomerName,");
            strSql.Append("MerSecName=@MerSecName,");
            strSql.Append("TransId=@TransId,");
            strSql.Append("TransOrderId=@TransOrderId,");
            strSql.Append("TransName=@TransName,");
            strSql.Append("TransSeqNo=@TransSeqNo,");
            strSql.Append("TransAmt=@TransAmt,");
            strSql.Append("TransAmt1=@TransAmt1,");
            strSql.Append("FeeAmt=@FeeAmt,");
            strSql.Append("TransDateTime=@TransDateTime,");
            strSql.Append("TransPPDateTime=@TransPPDateTime,");
            strSql.Append("ClearingDate=@ClearingDate,");
            strSql.Append("TransStatus=@TransStatus,");
            strSql.Append("CurrencyType=@CurrencyType,");
            strSql.Append("ProductInfo=@ProductInfo,");
            strSql.Append("MerchantId=@MerchantId,");
            strSql.Append("PayAcctType=@PayAcctType,");
            strSql.Append("PayAccNo=@PayAccNo,");
            strSql.Append("PayBankNo=@PayBankNo,");
            strSql.Append("PayBankName=@PayBankName,");
            strSql.Append("PayIp=@PayIp,");
            strSql.Append("MsgExt=@MsgExt");
            strSql.Append(" where ID=@ID ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ID", DbType.Int32, model.ID);
            db.AddInParameter(dbCommand, "OrderId", DbType.AnsiString, model.OrderId);
            db.AddInParameter(dbCommand, "CustomerID", DbType.AnsiString, model.CustomerID);
            db.AddInParameter(dbCommand, "CustomerName", DbType.AnsiString, model.CustomerName);
            db.AddInParameter(dbCommand, "MerSecName", DbType.AnsiString, model.MerSecName);
            db.AddInParameter(dbCommand, "TransId", DbType.AnsiString, model.TransId);
            db.AddInParameter(dbCommand, "TransOrderId", DbType.AnsiString, model.TransOrderId);
            db.AddInParameter(dbCommand, "TransName", DbType.AnsiString, model.TransName);
            db.AddInParameter(dbCommand, "TransSeqNo", DbType.AnsiString, model.TransSeqNo);
            db.AddInParameter(dbCommand, "TransAmt", DbType.Decimal, model.TransAmt);
            db.AddInParameter(dbCommand, "TransAmt1", DbType.Decimal, model.TransAmt1);
            db.AddInParameter(dbCommand, "FeeAmt", DbType.Decimal, model.FeeAmt);
            db.AddInParameter(dbCommand, "TransDateTime", DbType.DateTime, model.TransDateTime);
            db.AddInParameter(dbCommand, "TransPPDateTime", DbType.DateTime, model.TransPPDateTime);
            db.AddInParameter(dbCommand, "ClearingDate", DbType.DateTime, model.ClearingDate);
            db.AddInParameter(dbCommand, "TransStatus", DbType.AnsiString, model.TransStatus);
            db.AddInParameter(dbCommand, "CurrencyType", DbType.AnsiString, model.CurrencyType);
            db.AddInParameter(dbCommand, "ProductInfo", DbType.AnsiString, model.ProductInfo);
            db.AddInParameter(dbCommand, "MerchantId", DbType.AnsiString, model.MerchantId);
            db.AddInParameter(dbCommand, "PayAcctType", DbType.AnsiString, model.PayAcctType);
            db.AddInParameter(dbCommand, "PayAccNo", DbType.AnsiString, model.PayAccNo);
            db.AddInParameter(dbCommand, "PayBankNo", DbType.AnsiString, model.PayBankNo);
            db.AddInParameter(dbCommand, "PayBankName", DbType.AnsiString, model.PayBankName);
            db.AddInParameter(dbCommand, "PayIp", DbType.AnsiString, model.PayIp);
            db.AddInParameter(dbCommand, "MsgExt", DbType.AnsiString, model.MsgExt);
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
        /// 更新任意一个字段
        /// </summary>
        /// <param name="OrderId"></param>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Amend(int Id, string columnName, object value)
        {
            string sequel = "Update OrderTransOnline set ";
            sequel += "[" + columnName + "] = @Value ";
            sequel += "Where Id = @Id";

            DbCommand dbCommand = db.GetSqlStringCommand(sequel);
            db.AddInParameter(dbCommand, "Value", DbType.AnsiString, value);
            db.AddInParameter(dbCommand, "Id", DbType.Int32, Id);
            return 0 < db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 删除交易失败的数据
        /// </summary>
        public bool Delete(string OrderId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from OrderTransOnline ");
            strSql.Append(" where OrderId=@OrderId and TransStatus<>'00'");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "OrderId", DbType.String, OrderId);
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
        public SOSOshop.Model.Order.OrderTransOnline GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from OrderTransOnline ");
            strSql.AppendFormat(" where ID='{0}' ", ID);
            var select = db.ExecuteSqlStringAccessor<SOSOshop.Model.Order.OrderTransOnline>(strSql.ToString());
            return select != null && select.Count() > 0 ? select.First() : null;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SOSOshop.Model.Order.OrderTransOnline GetModelByOrderId(string OrderId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from OrderTransOnline ");
            strSql.AppendFormat(" where OrderId='{0}' ", OrderId);
            var select = db.ExecuteSqlStringAccessor<SOSOshop.Model.Order.OrderTransOnline>(strSql.ToString());
            return select != null && select.Count() > 0 ? select.First() : null;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SOSOshop.Model.Order.OrderTransOnline GetModelByTransOrderId(string TransOrderId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from OrderTransOnline ");
            strSql.AppendFormat(" where TransOrderId='{0}' ", TransOrderId);
            var select = db.ExecuteSqlStringAccessor<SOSOshop.Model.Order.OrderTransOnline>(strSql.ToString());
            return select != null && select.Count() > 0 ? select.First() : null;
        }

        #endregion  Method

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SOSOshop.Model.Order.OrderTransOnline model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("if(not exists(select 1 from OrderTransOnline where OrderId=@OrderId)) begin insert into OrderTransOnline(");
            strSql.Append("OrderId, CustomerID, CustomerName, MerSecName, TransId, TransOrderId, TransName, TransSeqNo, TransAmt, TransAmt1, FeeAmt, TransDateTime, TransPPDateTime, ClearingDate, TransStatus, CurrencyType, ProductInfo, MerchantId, PayAcctType, PayAccNo, PayBankNo, PayBankName, PayIp, MsgExt)");

            strSql.Append(" values (");
            strSql.Append("@OrderId, @CustomerID, @CustomerName, @MerSecName, @TransId, @TransOrderId, @TransName, @TransSeqNo, @TransAmt, @TransAmt1, @FeeAmt, @TransDateTime, @TransPPDateTime, @ClearingDate, @TransStatus, @CurrencyType, @ProductInfo, @MerchantId, @PayAcctType, @PayAccNo, @PayBankNo, @PayBankName, @PayIp, @MsgExt)");
            strSql.Append(" SELECT SCOPE_IDENTITY() end");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "OrderId", DbType.AnsiString, model.OrderId);
            db.AddInParameter(dbCommand, "CustomerID", DbType.AnsiString, model.CustomerID);
            db.AddInParameter(dbCommand, "CustomerName", DbType.AnsiString, model.CustomerName);
            db.AddInParameter(dbCommand, "MerSecName", DbType.AnsiString, model.MerSecName);
            db.AddInParameter(dbCommand, "TransId", DbType.AnsiString, model.TransId);
            db.AddInParameter(dbCommand, "TransOrderId", DbType.AnsiString, model.TransOrderId);
            db.AddInParameter(dbCommand, "TransName", DbType.AnsiString, model.TransName);
            db.AddInParameter(dbCommand, "TransSeqNo", DbType.AnsiString, model.TransSeqNo);
            db.AddInParameter(dbCommand, "TransAmt", DbType.Decimal, model.TransAmt);
            db.AddInParameter(dbCommand, "TransAmt1", DbType.Decimal, model.TransAmt1);
            db.AddInParameter(dbCommand, "FeeAmt", DbType.Decimal, model.FeeAmt);
            db.AddInParameter(dbCommand, "TransDateTime", DbType.DateTime, model.TransDateTime);
            db.AddInParameter(dbCommand, "TransPPDateTime", DbType.DateTime, model.TransPPDateTime);
            db.AddInParameter(dbCommand, "ClearingDate", DbType.DateTime, model.ClearingDate);
            db.AddInParameter(dbCommand, "TransStatus", DbType.AnsiString, model.TransStatus);
            db.AddInParameter(dbCommand, "CurrencyType", DbType.AnsiString, model.CurrencyType);
            db.AddInParameter(dbCommand, "ProductInfo", DbType.AnsiString, model.ProductInfo);
            db.AddInParameter(dbCommand, "MerchantId", DbType.AnsiString, model.MerchantId);
            db.AddInParameter(dbCommand, "PayAcctType", DbType.AnsiString, model.PayAcctType);
            db.AddInParameter(dbCommand, "PayAccNo", DbType.AnsiString, model.PayAccNo);
            db.AddInParameter(dbCommand, "PayBankNo", DbType.AnsiString, model.PayBankNo);
            db.AddInParameter(dbCommand, "PayBankName", DbType.AnsiString, model.PayBankName);
            db.AddInParameter(dbCommand, "PayIp", DbType.AnsiString, model.PayIp);
            db.AddInParameter(dbCommand, "MsgExt", DbType.AnsiString, model.MsgExt);

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
            return cmdresult;
        }

    }
}

