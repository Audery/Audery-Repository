using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace ErpShopService.BLL
{
    public class Orders : DbBase
    {
        /// <summary>
        /// 添加一条订单详情
        /// </summary>
        public void AddOrders(com.Orders model, string salesman, DbTransaction tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO dbo.I_ERP_Order_NOTE ( actionCode,noteTime,actionId ,handleStatus)VALUES (1,GETDATE(),@OrderId,0);insert into Orders(");
            strSql.Append("dwid,OrderId,UserName,ReceiverId,ShopDate,OrderDate,ConsigneeRealName,ConsigneeName,ConsigneePhone,ConsigneeProvince,ConsigneeAddress,ConsigneeZip,ConsigneeTel,ConsigneeFax,ConsigneeEmail,PaymentType,Payment,TotalPrice,Fees,OtherFees,Invoice,Remark,OrderStatus,PaymentStatus,OgisticsStatus,BusinessmanID,BusinessmanName,Carriage,OrderType,ContractNo,ConsigneeCity,ConsigneeBorough,ConsigneeConstructionSigns,ConsignesTime,TradeFees,TradeFeesPay,Editer,parentid,parentCorpName,BillingCorp,BillingCorpName,IsBusinessCheck,isFinancialReview,BusinessCheckDate,FinancialCheckDate,salesman)");

            strSql.Append(" values (");
            strSql.Append("@dwid,@OrderId,@UserName,@ReceiverId,@ShopDate,@OrderDate,@ConsigneeRealName,@ConsigneeName,@ConsigneePhone,@ConsigneeProvince,@ConsigneeAddress,@ConsigneeZip,@ConsigneeTel,@ConsigneeFax,@ConsigneeEmail,@PaymentType,@Payment,@TotalPrice,@Fees,@OtherFees,@Invoice,@Remark,@OrderStatus,@PaymentStatus,@OgisticsStatus,@BusinessmanID,@BusinessmanName,@Carriage,@OrderType,@ContractNo,@ConsigneeCity,@ConsigneeBorough,@ConsigneeConstructionSigns,@ConsignesTime,@TradeFees,@TradeFeesPay,@Editer,@parentid,@parentCorpName,@BillingCorp,@BillingCorpName,@IsBusinessCheck,@isFinancialReview,@BusinessCheckDate,@FinancialCheckDate,@salesman)");
            strSql.Append(";select @@IDENTITY;");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "dwid", DbType.AnsiString, model.dwid);
            db.AddInParameter(dbCommand, "OrderId", DbType.AnsiString, model.OrderId);
            db.AddInParameter(dbCommand, "UserName", DbType.AnsiString, model.UserName);
            db.AddInParameter(dbCommand, "ReceiverId", DbType.AnsiString, model.ReceiverId);
            db.AddInParameter(dbCommand, "ShopDate", DbType.DateTime, model.ShopDate);
            db.AddInParameter(dbCommand, "OrderDate", DbType.DateTime, model.OrderDate);
            db.AddInParameter(dbCommand, "ConsigneeRealName", DbType.AnsiString, model.ConsigneeRealName);
            db.AddInParameter(dbCommand, "ConsigneeName", DbType.AnsiString, model.ConsigneeName);
            db.AddInParameter(dbCommand, "ConsigneePhone", DbType.AnsiString, model.ConsigneePhone);
            db.AddInParameter(dbCommand, "ConsigneeProvince", DbType.AnsiString, model.ConsigneeProvince);
            db.AddInParameter(dbCommand, "ConsigneeAddress", DbType.AnsiString, model.ConsigneeAddress);
            db.AddInParameter(dbCommand, "ConsigneeZip", DbType.AnsiString, model.ConsigneeZip);
            db.AddInParameter(dbCommand, "ConsigneeTel", DbType.AnsiString, model.ConsigneeTel);
            db.AddInParameter(dbCommand, "ConsigneeFax", DbType.AnsiString, model.ConsigneeFax);
            db.AddInParameter(dbCommand, "ConsigneeEmail", DbType.AnsiString, model.ConsigneeEmail);
            db.AddInParameter(dbCommand, "PaymentType", DbType.Int32, model.PaymentType);
            db.AddInParameter(dbCommand, "Payment", DbType.Int32, model.Payment);
            db.AddInParameter(dbCommand, "TotalPrice", DbType.Decimal, model.TotalPrice);
            db.AddInParameter(dbCommand, "Fees", DbType.Decimal, model.Fees);
            db.AddInParameter(dbCommand, "OtherFees", DbType.Decimal, model.OtherFees);
            db.AddInParameter(dbCommand, "Invoice", DbType.Int32, model.Invoice);
            db.AddInParameter(dbCommand, "Remark", DbType.AnsiString, model.Remark);
            db.AddInParameter(dbCommand, "OrderStatus", DbType.Int32, model.OrderStatus);
            db.AddInParameter(dbCommand, "PaymentStatus", DbType.Int32, model.PaymentStatus);
            db.AddInParameter(dbCommand, "OgisticsStatus", DbType.Int32, model.OgisticsStatus);
            db.AddInParameter(dbCommand, "BusinessmanID", DbType.Int32, model.BusinessmanID);
            db.AddInParameter(dbCommand, "BusinessmanName", DbType.AnsiString, model.BusinessmanName);
            db.AddInParameter(dbCommand, "Carriage", DbType.Int32, model.Carriage);
            db.AddInParameter(dbCommand, "OrderType", DbType.Int32, model.OrderType);
            db.AddInParameter(dbCommand, "ContractNo", DbType.AnsiString, model.ContractNo);
            db.AddInParameter(dbCommand, "ConsigneeCity", DbType.AnsiString, model.ConsigneeCity);
            db.AddInParameter(dbCommand, "ConsigneeBorough", DbType.AnsiString, model.ConsigneeBorough);
            db.AddInParameter(dbCommand, "ConsigneeConstructionSigns", DbType.AnsiString, model.ConsigneeConstructionSigns);
            db.AddInParameter(dbCommand, "ConsignesTime", DbType.AnsiString, model.ConsignesTime);
            db.AddInParameter(dbCommand, "TradeFees", DbType.Decimal, model.TradeFees);
            db.AddInParameter(dbCommand, "TradeFeesPay", DbType.Int32, model.TradeFeesPay);
            db.AddInParameter(dbCommand, "Editer", DbType.Int32, model.Editer);
            db.AddInParameter(dbCommand, "parentid", DbType.AnsiString, model.parentid);
            db.AddInParameter(dbCommand, "parentCorpName", DbType.AnsiString, model.parentCorpName);
            db.AddInParameter(dbCommand, "BillingCorp", DbType.Int32, model.BillingCorp);
            db.AddInParameter(dbCommand, "BillingCorpName", DbType.AnsiString, model.BillingCorpName);
            db.AddInParameter(dbCommand, "IsBusinessCheck", DbType.Int32, model.IsBusinessCheck);
            db.AddInParameter(dbCommand, "isFinancialReview", DbType.Int32, model.isFinancialReview);
            db.AddInParameter(dbCommand, "BusinessCheckDate", DbType.DateTime, model.BusinessCheckDate);
            db.AddInParameter(dbCommand, "FinancialCheckDate", DbType.DateTime, model.FinancialCheckDate);
            db.AddInParameter(dbCommand, "salesman", DbType.String, salesman);
            db.ExecuteNonQuery(dbCommand, tran);

        }

        /// <summary>
        /// 添加一条订单详情
        /// </summary>
        public void AddOrderProduct(com.OrderProduct model, DbTransaction tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into OrderProduct(");
            strSql.Append("OrderId,ProId,ProName,ProPrice,ProNum,AddTime,Status,spid,StorageID,Extend1)");

            strSql.Append(" values (");
            strSql.Append("@OrderId,@ProId,@ProName,@ProPrice,@ProNum,@AddTime,@Status,@spid,@StorageID,@Extend1)");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "OrderId", DbType.AnsiString, model.OrderId);
            db.AddInParameter(dbCommand, "ProId", DbType.AnsiString, model.ProId);
            db.AddInParameter(dbCommand, "ProName", DbType.AnsiString, model.ProName);
            db.AddInParameter(dbCommand, "ProPrice", DbType.Decimal, model.ProPrice);
            db.AddInParameter(dbCommand, "ProNum", DbType.Int32, model.ProNum);
            db.AddInParameter(dbCommand, "AddTime", DbType.DateTime, model.AddTime);
            db.AddInParameter(dbCommand, "Status", DbType.Int32, model.Status);
            db.AddInParameter(dbCommand, "spid", DbType.String, model.spid);
            db.AddInParameter(dbCommand, "StorageID", DbType.String, model.StorageID);
            db.AddInParameter(dbCommand, "Extend1", DbType.String, model.Extend1);
            db.ExecuteNonQuery(dbCommand, tran);
        }

    }
}
