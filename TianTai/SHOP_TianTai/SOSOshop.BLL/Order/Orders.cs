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
using System.Threading;
using SOSOshop.BLL.Common;
namespace SOSOshop.BLL.Order
{
    /// <summary>
    /// 订单数据访问类:Orders
    /// </summary>
    public partial class Orders : Db
    {

        public Orders()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Orders where Id=@Id ");
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
        public bool Update(SOSOshop.Model.Order.Orders model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Orders set ");
            strSql.Append("UserName=@UserName,");
            strSql.Append("ReceiverId=@ReceiverId,");
            strSql.Append("ShopDate=@ShopDate,");
            strSql.Append("OrderDate=@OrderDate,");
            strSql.Append("ConsigneeRealName=@ConsigneeRealName,");
            strSql.Append("ConsigneeName=@ConsigneeName,");
            strSql.Append("ConsigneePhone=@ConsigneePhone,");
            strSql.Append("ConsigneeProvince=@ConsigneeProvince,");
            strSql.Append("ConsigneeAddress=@ConsigneeAddress,");
            strSql.Append("ConsigneeZip=@ConsigneeZip,");
            strSql.Append("ConsigneeTel=@ConsigneeTel,");
            strSql.Append("ConsigneeFax=@ConsigneeFax,");
            strSql.Append("ConsigneeEmail=@ConsigneeEmail,");
            strSql.Append("PaymentType=@PaymentType,");
            strSql.Append("Payment=@Payment,");
            strSql.Append("TotalPrice=@TotalPrice,");
            strSql.Append("Fees=@Fees,");
            strSql.Append("OtherFees=@OtherFees,");
            strSql.Append("Invoice=@Invoice,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("OrderStatus=@OrderStatus,");
            strSql.Append("PaymentStatus=@PaymentStatus,");
            strSql.Append("OgisticsStatus=@OgisticsStatus,");
            strSql.Append("BusinessmanID=@BusinessmanID,");
            strSql.Append("BusinessmanName=@BusinessmanName,");
            strSql.Append("Carriage=@Carriage,");
            strSql.Append("OrderType=@OrderType,");
            strSql.Append("ContractNo=@ContractNo,");
            strSql.Append("ConsigneeCity=@ConsigneeCity,");
            strSql.Append("ConsigneeBorough=@ConsigneeBorough,");
            strSql.Append("ConsigneeConstructionSigns=@ConsigneeConstructionSigns,");
            strSql.Append("ConsignesTime=@ConsignesTime,");
            strSql.Append("TradeFees=@TradeFees,");
            strSql.Append("TradeFeesPay=@TradeFeesPay,");
            strSql.Append("Editer=@Editer,");
            strSql.Append("parentid=@parentid,");
            strSql.Append("parentCorpName=@parentCorpName,");
            strSql.Append("BillingCorp=@BillingCorp,");
            strSql.Append("BillingCorpName=@BillingCorpName,");
            strSql.Append("IsBusinessCheck=@IsBusinessCheck,");
            strSql.Append("isFinancialReview=@isFinancialReview,");
            strSql.Append("BusinessCheckDate=@BusinessCheckDate,");
            strSql.Append("FinancialCheckDate=@FinancialCheckDate");
            strSql.Append(" where Id=@Id ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Id", DbType.Int32, model.Id);
            db.AddInParameter(dbCommand, "OrderId", DbType.AnsiString, model.OrderId);
            db.AddInParameter(dbCommand, "UserName", DbType.AnsiString, model.UserName);
            db.AddInParameter(dbCommand, "ReceiverId", DbType.Int32, model.ReceiverId);
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
            db.AddInParameter(dbCommand, "TotalPrice", DbType.Double, model.TotalPrice);
            db.AddInParameter(dbCommand, "Fees", DbType.Double, model.Fees);
            db.AddInParameter(dbCommand, "OtherFees", DbType.Double, model.OtherFees);
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
            db.AddInParameter(dbCommand, "TradeFees", DbType.Double, model.TradeFees);
            db.AddInParameter(dbCommand, "TradeFeesPay", DbType.Int32, model.TradeFeesPay);
            db.AddInParameter(dbCommand, "Editer", DbType.Int32, model.Editer);
            db.AddInParameter(dbCommand, "parentid", DbType.Int32, model.parentid);
            db.AddInParameter(dbCommand, "parentCorpName", DbType.AnsiString, model.parentCorpName);
            db.AddInParameter(dbCommand, "BillingCorp", DbType.Int32, model.BillingCorp);
            db.AddInParameter(dbCommand, "BillingCorpName", DbType.AnsiString, model.BillingCorpName);
            db.AddInParameter(dbCommand, "IsBusinessCheck", DbType.Int32, model.IsBusinessCheck);
            db.AddInParameter(dbCommand, "isFinancialReview", DbType.Int32, model.isFinancialReview);
            db.AddInParameter(dbCommand, "BusinessCheckDate", DbType.DateTime, model.BusinessCheckDate);
            db.AddInParameter(dbCommand, "FinancialCheckDate", DbType.DateTime, model.FinancialCheckDate);
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
            strSql.Append("delete from Orders ");
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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Orders ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
        public SOSOshop.Model.Order.Orders GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,0 dwid from Orders ");
            strSql.AppendFormat(" where Id={0} ", Id);
            return db.ExecuteSqlStringAccessor<SOSOshop.Model.Order.Orders>(strSql.ToString()).First();
        }

        /// <summary>
        /// 根据订单号得到一个对象实体
        /// </summary>
        public SOSOshop.Model.Order.Orders GetModel(string orderId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top(1) *,0 dwid from Orders ");
            strSql.AppendFormat(" where orderId='{0}' ", orderId);
            return db.ExecuteSqlStringAccessor<SOSOshop.Model.Order.Orders>(strSql.ToString()).First();
        }
        /// <summary>
        /// 根据订单号得到一个对象实体
        /// </summary>
        public SOSOshop.Model.Order.Orders GetModelLike(string orderId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top(1) *,0 dwid from Orders ");
            strSql.AppendFormat(" where orderId='{0}'", orderId);
            return db.ExecuteSqlStringAccessor<SOSOshop.Model.Order.Orders>(strSql.ToString()).First();
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,OrderId,UserName,ReceiverId,ShopDate,OrderDate,ConsigneeRealName,ConsigneeName,ConsigneePhone,ConsigneeProvince,ConsigneeAddress,ConsigneeZip,ConsigneeTel,ConsigneeFax,ConsigneeEmail,PaymentType,Payment,TotalPrice,Fees,OtherFees,Invoice,Remark,OrderStatus,PaymentStatus,OgisticsStatus,BusinessmanID,BusinessmanName,Carriage,OrderType,ContractNo,ConsigneeCity,ConsigneeBorough,ConsigneeConstructionSigns,ConsignesTime,TradeFees,TradeFeesPay,Editer,parentid,parentCorpName,BillingCorp,BillingCorpName,IsBusinessCheck,isFinancialReview,BusinessCheckDate,FinancialCheckDate ");
            strSql.Append(" FROM Orders ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return db.ExecuteDataSet(CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" Id,OrderId,UserName,ReceiverId,ShopDate,OrderDate,ConsigneeRealName,ConsigneeName,ConsigneePhone,ConsigneeProvince,ConsigneeAddress,ConsigneeZip,ConsigneeTel,ConsigneeFax,ConsigneeEmail,PaymentType,Payment,TotalPrice,Fees,OtherFees,Invoice,Remark,OrderStatus,PaymentStatus,OgisticsStatus,BusinessmanID,BusinessmanName,Carriage,OrderType,ContractNo,ConsigneeCity,ConsigneeBorough,ConsigneeConstructionSigns,ConsignesTime,TradeFees,TradeFeesPay,Editer,parentid,parentCorpName,BillingCorp,BillingCorpName,IsBusinessCheck,isFinancialReview,BusinessCheckDate,FinancialCheckDate ");
            strSql.Append(" FROM Orders ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);

            return db.ExecuteDataSet(CommandType.Text, strSql.ToString());
        }
        #endregion  Method

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SOSOshop.Model.Order.Orders model, DbTransaction tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Orders(");
            strSql.Append("OrderId,UserName,ReceiverId,ShopDate,OrderDate,ConsigneeRealName,ConsigneeName,ConsigneePhone,ConsigneeProvince,ConsigneeAddress,ConsigneeZip,ConsigneeTel,ConsigneeFax,ConsigneeEmail,PaymentType,Payment,TotalPrice,Fees,OtherFees,Invoice,Remark,OrderStatus,PaymentStatus,OgisticsStatus,BusinessmanID,BusinessmanName,Carriage,OrderType,ContractNo,ConsigneeCity,ConsigneeBorough,ConsigneeConstructionSigns,ConsignesTime,TradeFees,TradeFeesPay,Editer,parentid,parentCorpName,BillingCorp,BillingCorpName,IsBusinessCheck,isFinancialReview,BusinessCheckDate,FinancialCheckDate,IsSend,source)");

            strSql.Append(" values (");
            strSql.Append("@OrderId,@UserName,@ReceiverId,@ShopDate,@OrderDate,@ConsigneeRealName,@ConsigneeName,@ConsigneePhone,@ConsigneeProvince,@ConsigneeAddress,@ConsigneeZip,@ConsigneeTel,@ConsigneeFax,@ConsigneeEmail,@PaymentType,@Payment,@TotalPrice,@Fees,@OtherFees,@Invoice,@Remark,@OrderStatus,@PaymentStatus,@OgisticsStatus,@BusinessmanID,@BusinessmanName,@Carriage,@OrderType,@ContractNo,@ConsigneeCity,@ConsigneeBorough,@ConsigneeConstructionSigns,@ConsignesTime,@TradeFees,@TradeFeesPay,@Editer,@parentid,@parentCorpName,@BillingCorp,@BillingCorpName,@IsBusinessCheck,@isFinancialReview,@BusinessCheckDate,@FinancialCheckDate,@IsSend,@source)");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "OrderId", DbType.AnsiString, model.OrderId);
            db.AddInParameter(dbCommand, "UserName", DbType.AnsiString, model.UserName);
            db.AddInParameter(dbCommand, "ReceiverId", DbType.Int32, model.ReceiverId);
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
            db.AddInParameter(dbCommand, "TotalPrice", DbType.Double, model.TotalPrice);
            db.AddInParameter(dbCommand, "Fees", DbType.Double, model.Fees);
            db.AddInParameter(dbCommand, "OtherFees", DbType.Double, model.OtherFees);
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
            db.AddInParameter(dbCommand, "TradeFees", DbType.Double, model.TradeFees);
            db.AddInParameter(dbCommand, "TradeFeesPay", DbType.Int32, model.TradeFeesPay);
            db.AddInParameter(dbCommand, "Editer", DbType.Int32, model.Editer);
            db.AddInParameter(dbCommand, "parentid", DbType.Int32, model.parentid);
            db.AddInParameter(dbCommand, "parentCorpName", DbType.AnsiString, model.parentCorpName);
            db.AddInParameter(dbCommand, "BillingCorp", DbType.Int32, model.BillingCorp);
            db.AddInParameter(dbCommand, "BillingCorpName", DbType.AnsiString, model.BillingCorpName);
            db.AddInParameter(dbCommand, "IsBusinessCheck", DbType.Int32, model.IsBusinessCheck);
            db.AddInParameter(dbCommand, "isFinancialReview", DbType.Int32, model.isFinancialReview);
            db.AddInParameter(dbCommand, "BusinessCheckDate", DbType.DateTime, model.BusinessCheckDate);
            db.AddInParameter(dbCommand, "FinancialCheckDate", DbType.DateTime, model.FinancialCheckDate);
            db.AddInParameter(dbCommand, "IsSend", DbType.Boolean, model.IsSend);
            db.AddInParameter(dbCommand, "source", DbType.Int32, model.source);
            db.ExecuteScalar(dbCommand, tran);
        }
        /// <summary>
        /// 生成流水订单号
        /// </summary>
        /// <returns></returns>
        public string CreateOrderId(int id)
        {
            int PrimaryKey = (int)db.ExecuteScalar(db.GetSqlStringCommand("Declare @NewSeqVal INT Exec @NewSeqVal =  P_GetNewSeqVal_SeqT_0101001 SELECT @NewSeqVal"));
            string obj = (string)db.ExecuteScalar(db.GetSqlStringCommand("SELECT CompanyClass FROM dbo.memberaccount WHERE UID=" + id));

            string inc = "X";
            //1,生产企业 2,经营企业 3.医疗机构,4.单体药店,5.连锁药店,6.诊所,7.其他
            //商业公司为A类，药店为B类，诊所为C类，医院为D类
            switch (obj)
            {
                case "批发公司":
                    {
                        inc = "A";
                        break;
                    }
                case "零售连锁":
                    {
                        inc = "A";
                        break;
                    }
                case "单体药房/诊所":
                    {
                        inc = "B";
                        break;
                    }
                case "民营医院":
                    {
                        inc = "B";
                        break;
                    }
                case "公立医院":
                    {
                        inc = "B";
                        break;
                    }
                case "内部门店":
                    {
                        inc = "B";
                        break;
                    }
            }
            return string.Format("{0}{1}", inc, 10000000000 + PrimaryKey);
        }
        /// <summary>
        /// 添加订单(一般来说添加订单必须用此api)
        /// </summary>
        /// <param name="order">订单model</param>
        /// <param name="li">订单商品列表</param>
        /// <returns>如果添加成功则返回订单号，否则则返回null</returns>
        public string Add(SOSOshop.Model.Order.Orders order, List<SOSOshop.Model.Order.OrderProduct> li)
        {
            order.TotalPrice = (decimal)(from a in li select a.ProNum * a.ProPrice).Sum();
            //如果不是在线支付则取消需要在后台手动确认付款
            //如果有E商的品种就不执行上述规则(取消此规则2014-06-17)
            if (order.PaymentType == 2)
            {
                order.PaymentStatus = 1;
                order.OrderStatus = (int)Enums.OrderStatus.Paid;
                order.FinancialCheckDate = DateTime.Now;

            }
            if (li.Count < 1) { throw new Exception("品种数量不能为空!"); }
            order.OrderId = CreateOrderId(order.ReceiverId);
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction tran = conn.BeginTransaction();
                try
                {
                    order.splitStatus = 1;
                    Add(order, tran);
                    SOSOshop.BLL.Order.OrderProduct bll = new OrderProduct();
                    foreach (var item in li)
                    {
                        if (item.ProNum > 0 && item.ProPrice > 0)
                        {
                            item.OrderId = order.OrderId;
                            bll.Add(item, tran);
                        }
                        else
                        {
                            SOSOshop.BLL.Logs.Log.LogShopAdd(string.Format("订单商品数量为零,或价格为零,订单号：{0},商品id:{1}", order.OrderId, item.ProId), "", 0, "", 2);
                        }
                    }
                    tran.Commit();
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    throw e;
                }
            }
            base.ExecuteNonQuery(string.Format("INSERT INTO OrdersMQ (orderid) VALUES('{0}')", order.OrderId));
            return order.OrderId;
        }
        #region 分单处理核心方法
        /// <summary>
        /// 异步调用分单处理
        /// </summary>        
        public void AsySplitOrder()
        {
            //未建档订单不分单
            string sql = "SELECT OrderId FROM dbo.OrdersMQ WHERE  state=0 AND orderid NOT IN (SELECT orderid FROM dbo.Orders where ReceiverId IN (SELECT UID FROM dbo.memberpermission WHERE IsSpecialTrade=1))";
            foreach (DataRow item in base.ExecuteTable(sql).Rows)
            {
                string orderid = (string)item["OrderId"];
                try
                {
                    SplitOrder(orderid, true);
                    ExecuteNonQuery(string.Format("UPDATE OrdersMQ SET state=1 WHERE orderid='{0}'", orderid));
                }
                catch (Exception ex)
                {
                    DbCommand dbCommand = db.GetSqlStringCommand("UPDATE OrdersMQ SET state=-1,notes=@notes WHERE orderid=@orderid");
                    db.AddInParameter(dbCommand, "notes", DbType.String, ex.ToString());
                    db.AddInParameter(dbCommand, "orderid", DbType.String, orderid);
                    db.ExecuteNonQuery(dbCommand);
                }

            }
        }
        /// <summary>
        /// 分单处理
        /// </summary>
        /// <param name="orderid">订单编号</param>
        /// <param name="isNews">是否新订单(新订单是以是否判断分过单计,如刚审核通过，或刚刚提交的都算新订单)</param>
        public void SplitOrder(string orderId, bool isNews)
        {

            string sql = "";
            //将有库存的订单设置为自动确认供货,不需要采购在手动确认
            base.ExecuteNonQuery(string.Format("UPDATE dbo.OrderProduct SET Status={0} WHERE orderid={1}", (int)Enums.OrderProductStatus.ConfirmationSupplyAuto, orderId));
            var dr = base.ExecuteTable("SELECT OrderStatus,Payment,PaymentStatus,IsSend FROM Orders where orderid='" + orderId + "'").Rows[0];
            int OrderStatus = (int)dr["OrderStatus"];

            //已经分过单了，不能二次分单
            if (!(new Enums.OrderProductStatus[] { 
                Enums.OrderProductStatus.Submit, 
                Enums.OrderProductStatus.ConfirmationSupply, 
                Enums.OrderProductStatus.ConfirmationNoStock }).Contains((Enums.OrderProductStatus)OrderStatus))
            {
                sql = "DELETE OrdersMQ WHERE orderid ='" + orderId + "'";
                base.ExecuteNonQuery(sql);
                throw new Exception("订单：" + orderId + "已经被处理，不能二次分单！");
            }
            //支付类型不是款到发货，或者支付状态是已经支付，都要提交到ERP（如果是在线支付，还没支付的时候，消息就被处理掉了，需要在调用一下分单方法）
            if ((int)dr["Payment"] != 2 || (int)dr["PaymentStatus"] == 1)
            {
                SubmitOrderToERP_Note(orderId);
                //更新订单状态为已审核
                base.ExecuteNonQuery(string.Format("UPDATE dbo.Orders SET BusinessCheckDate=GETDATE(),OrderStatus={1} WHERE OrderId='{0}'", orderId, (int)Enums.OrderStatus.Audit));
            }
        }

        /// <summary>
        /// 将订单写入ERP消息同步表用户订单同步
        /// </summary>
        /// <param name="orderid"></param>
        public void SubmitOrderToERP_Note(string orderid)
        {
            base.ExecuteNonQuery(string.Format("INSERT INTO OrdersMQ_1 (orderid) VALUES('{0}')", orderid));
        }
        #endregion



        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public bool CancelOrder(string orderid)
        {
            var dt = base.ExecuteTable("SELECT OrderStatus FROM dbo.Orders WHERE OrderId='{0}'", orderid);
            if ((int)dt.Rows[0]["OrderStatus"] != 1 && (int)dt.Rows[0]["OrderStatus"] != 3)//已支付也可以取消
            {
                throw new Exception("订单状态已发生改变!");
            }
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction tran = conn.BeginTransaction();
                try
                {
                    db.ExecuteNonQuery(db.GetSqlStringCommand(string.Format("UPDATE dbo.Orders SET OrderStatus={0} WHERE OrderId='{1}'", (int)Enums.OrderStatus.Cancelled, orderid)), tran);
                    db.ExecuteNonQuery(db.GetSqlStringCommand(string.Format("UPDATE dbo.OrderProduct SET Status={1} WHERE Status<>{2} and OrderId='{0}'", orderid, (int)Enums.OrderProductStatus.Cancelled, (int)Enums.OrderProductStatus.NoGoods)), tran);
                    db.ExecuteNonQuery(db.GetSqlStringCommand("DELETE OrdersMQ WHERE orderid='" + orderid + "'"), tran);
                    tran.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    throw e;
                }
            }
        }
        /// <summary>
        /// 判断订单是否可以处理(必须下单时间超过10分钟,和被系统先分过单)
        /// </summary>
        /// <returns></returns>
        public bool isDispose(string orderid)
        {
            return true;
        }
        /// 允许已经建档通过的会员的定单可以执行流程
        /// </summary>
        /// <param name="uid">企业编号，或会员编号</param>
        ///<param name="type">字符串 二级单位 或 一级单位</param>
        public static void LetOrders(string code, string type)
        {
            Orders bll = new Orders();
            bll.LetOrders2(code, type);
        }
        /// <summary>
        /// 允许已经建档通过的会员的定单可以执行流程
        /// </summary>
        /// <param name="uid">企业编号，或会员编号</param>
        ///<param name="type">字符串 二级单位 或 一级单位</param>
        public void LetOrders2(string code, string type)
        {
            string sql = "SELECT OrderId FROM dbo.Orders WHERE OrderStatus=1 AND ReceiverId IN ({0});UPDATE dbo.memberpermission SET IsSpecialTrade=0 WHERE UID IN ({0});UPDATE dbo.Orders SET parentid=b.ParentId,parentCorpName=c.Name,BillingCorp=b.ParentId,BillingCorpName=c.Name FROM Orders a INNER JOIN dbo.memberinfo b ON a.ReceiverId=b.UID INNER JOIN dbo.DrugsBase_Enterprise c ON b.ParentId=c.ID WHERE a.parentid=0 AND a.ReceiverId IN({0})";
            if (type == "二级单位")
            {
                sql = string.Format(sql, "SELECT UID FROM dbo.memberinfo WHERE ParentId IN (SELECT ID FROM dbo.DrugsBase_Enterprise WHERE Code='" + code + "')");
            }
            else if (type == "一级单位")
            {
                sql = string.Format(sql, "SELECT UID FROM dbo.memberinfo WHERE Code='" + code + "'");
            }
            //foreach (DataRow item in base.ExecuteTable(sql).Rows)
            //{
            //    SplitOrder((string)item[0], true);
            //}
        }

        /// <summary>
        /// 取得所有未同步至erp的订单
        /// </summary>
        /// <returns></returns>
        public List<OrderList> GetOrdersMQ_1()
        {
            List<OrderList> list = new List<OrderList>();
            string sql = @"SELECT  [Id] ,
                        CONVERT(NVARCHAR(11), ( SELECT  code
                                                FROM    memberinfo
                                                WHERE   uid = ReceiverId
                                              )) AS dwid ,
                        [OrderId] ,
                        ReceiverId,
                        [UserName],[ShopDate] ,
                        [OrderDate] ,
                        [ConsigneeRealName] ,
                        [ConsigneeName] ,
                        [ConsigneePhone] ,
                        [ConsigneeProvince] ,
                        [ConsigneeAddress] ,
                        [ConsigneeZip] ,
                        [ConsigneeTel] ,
                        [ConsigneeFax] ,
                        [ConsigneeEmail] ,
                        [PaymentType] ,
                        [Payment] ,
                        [TotalPrice] ,
                        [Fees] ,
                        [OtherFees] ,
                        [Invoice] ,
                        [Remark] ,
                        [OrderStatus] ,
                        [PaymentStatus] ,
                        [OgisticsStatus] ,
                        [BusinessmanID] ,
                        [BusinessmanName] ,
                        [Carriage] ,
                        [OrderType] ,
                        [ContractNo] ,
                        [ConsigneeCity] ,
                        [ConsigneeBorough] ,
                        [ConsigneeConstructionSigns] ,
                        [ConsignesTime] ,
                        [TradeFees] ,
                        [TradeFeesPay] ,
                        [Editer] ,
                        [parentid] ,
                        [parentCorpName] ,
                        [BillingCorp] ,
                        [BillingCorpName] ,
                        [IsBusinessCheck] ,
                        [isFinancialReview] ,
                        [BusinessCheckDate] ,
                        [FinancialCheckDate] ,
                        [IsSend] ,
                        [splitStatus] ,
                        [source]
                FROM    dbo.Orders
                WHERE   OrderId IN ( SELECT orderid
                                     FROM   OrdersMQ_1
                                     WHERE  state = 0 )
                ORDER BY id ASC";
            List<SOSOshop.Model.Order.Orders> ol = db.ExecuteSqlStringAccessor<SOSOshop.Model.Order.Orders>(sql).ToList();
            foreach (var item in ol)
            {
                OrderList temp = new OrderList();
                temp.order = item;
                sql = string.Format(@"SELECT  a.* ,
                                    b.spid ,
                                    CASE a.IsExpirationProduct
                                      WHEN 1
                                      THEN ( '促销价：' + CONVERT(NVARCHAR(20), b.CuPrice) + '则扣率：'
                                             + CONVERT(NVARCHAR(20), b.Discount) )
                                      WHEN 0 THEN ''
                                      ELSE ''
                                    END AS Extend1 ,
                                    ( SELECT    Extended1
                                      FROM      Product_ExpirationTime
                                      WHERE     Product_ID = a.ProId
                                                AND a.IsExpirationProduct = 1
                                    ) AS StorageID
                            FROM    dbo.OrderProduct a
                                    INNER JOIN dbo.Product b ON a.ProId = b.Product_ID
                            WHERE   OrderId ='{0}'", item.OrderId);
                temp.li = db.ExecuteSqlStringAccessor<SOSOshop.Model.Order.OrderProduct>(sql).ToList();
                temp.salesman = db.ExecuteScalar(db.GetSqlStringCommand(string.Format("SELECT name FROM dbo.yxs_administrators WHERE adminid=(	SELECT OSPId FROM dbo.memberinfo WHERE UID={0})", item.ReceiverId))) as string;
                list.Add(temp);
            }
            return list;
        }
        /// <summary>
        /// 更新消息状态为已处理
        /// </summary>
        /// <param name="orderid"></param>
        public void UpdateOrdersMQ_1(string orderid)
        {
            string sql = string.Format("UPDATE dbo.OrdersMQ_1 SET state=1 WHERE orderid='{0}'", orderid);
            ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 同步erp订单状态至商城
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="OrderStatus">订单商品状态
        ///1＝已提交
        ///5＝无货
        ///6＝已取消
        ///7＝已申请出库
        ///8＝已出库待发运
        ///9＝货物已发出
        ///10＝已收货
        ///</param>
        public void UpdateOrderStatusForErp(string orderid, string spid, int OrderStatus)
        {
            string sql = string.Format("UPDATE dbo.OrderProduct SET Status={2} WHERE OrderId='{0}' AND ProId=(SELECT TOP 1 Product_ID FROM dbo.Product WHERE spid='{1}')", orderid, spid, OrderStatus);
            ExecuteNonQuery(sql);

            //修改销售数量
            if (OrderStatus > 6)
            {
                sql = string.Format("UPDATE dbo.Product SET Product_SaleNum=Product_SaleNum+(SELECT ProNum FROM dbo.OrderProduct where ProId=Product_ID AND OrderId='{0}') WHERE Product_ID=(SELECT TOP 1 Product_ID FROM dbo.Product WHERE spid='{1}')", orderid, spid);
                ExecuteNonQuery(sql);
            }
            //所有订单详情明细都包含已提交和自动确认供货则执行订单完成
            sql = string.Format("SELECT COUNT(*) FROM dbo.OrderProduct WHERE OrderId='{0}' AND Status=1 or Status=11", orderid);
            if ((int)ExecuteScalar(sql) == 0)
            {
                ExecuteNonQuery("UPDATE dbo.Orders SET OrderStatus=4 where OrderId='" + orderid + "'");
            }
            //所有订单详情明细都是取消，则更新订单为取消
            sql = string.Format("SELECT COUNT(*) FROM dbo.OrderProduct WHERE OrderId='{0}' AND Status<>6", orderid);
            if ((int)ExecuteScalar(sql) == 0)
            {
                ExecuteNonQuery("UPDATE dbo.Orders SET OrderStatus=-1 where OrderId='" + orderid + "'");
            }
        }

    }

    /// <summary>
    /// 订单集合
    /// </summary>
    public class OrderList
    {
        public SOSOshop.Model.Order.Orders order { get; set; }
        /// <summary>
        /// 销售员
        /// </summary>
        public string salesman { get; set; }
        public List<SOSOshop.Model.Order.OrderProduct> li { get; set; }
    }
}

