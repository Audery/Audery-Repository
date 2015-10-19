using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Linq;
using System.Data.Common;
namespace SOSOshop.BLL.Order
{
    /// <summary>
    /// 购买（下订单）的模板
    /// </summary>
    public partial class OrderProductCart : Db
    {
        public OrderProductCart()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string Name)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from OrderProductCart where [Name]=@Name ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Name", DbType.AnsiString, Name);
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
        public bool Update(SOSOshop.Model.Order.OrderProductCart model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OrderProductCart set ");
            strSql.Append("UID=@UID,");
            strSql.Append("Name=@Name,");
            strSql.Append("Description=@Description,");
            strSql.Append("UpdateTime=GETDATE(),");
            strSql.Append("State=@State");
            strSql.Append(" where CartId=@CartId ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CartId", DbType.Int32, model.CartId);
            db.AddInParameter(dbCommand, "UID", DbType.Int32, model.UID);
            db.AddInParameter(dbCommand, "Name", DbType.AnsiString, model.Name);
            db.AddInParameter(dbCommand, "Description", DbType.AnsiString, model.Description);
            db.AddInParameter(dbCommand, "State", DbType.Int32, model.State);
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
        /// <param name="id"></param>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Amend(int CartId, string columnName, object value)
        {
            string sequel = "Update OrderProductCart set ";
            sequel += "[" + columnName + "] = @Value ";
            sequel += "Where CartId = @CartId";

            DbCommand dbCommand = db.GetSqlStringCommand(sequel);
            db.AddInParameter(dbCommand, "Value", DbType.AnsiString, value);
            db.AddInParameter(dbCommand, "CartId", DbType.Int32, CartId);
            return 0 < db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int CartId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from OrderProductCart where CartId=@CartId ");
            strSql.Append("delete from OrderProductCartPro where CartId=@CartId ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CartId", DbType.Int32, CartId);
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
        public SOSOshop.Model.Order.OrderProductCart GetModel(int CartId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [CartId],[UID],[Name],[Description],[AddTime],[UpdateTime],[State] FROM [OrderProductCart] WHERE [State]=1");
            strSql.AppendFormat(" AND CartId={0} ", CartId);
            var items = db.ExecuteSqlStringAccessor<SOSOshop.Model.Order.OrderProductCart>(strSql.ToString());
            return items.Count() > 0 ? items.First() : null;
        }
        /// <summary>
        /// 得到一组对象实体
        /// </summary>
        public IEnumerable<SOSOshop.Model.Order.OrderProductCart> GetList(int UID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [CartId],[UID],[Name],[Description],[AddTime],[UpdateTime],[State] FROM [OrderProductCart] WHERE [State]=1");
            strSql.AppendFormat(" AND UID={0} ", UID);
            return db.ExecuteSqlStringAccessor<SOSOshop.Model.Order.OrderProductCart>(strSql.ToString());
        }
        #endregion  Method

        /// <summary>
        /// 增加一条数据([UID],[Name],[Description])
        /// </summary>
        public int Add(SOSOshop.Model.Order.OrderProductCart model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("declare @now datetime set @now=GETDATE()\n");
            strSql.Append("insert into OrderProductCart(");
            strSql.Append("[UID],[Name],[Description],[AddTime],[UpdateTime],[State])");

            strSql.Append(" values (");
            strSql.Append("@UID,@Name,@Description,@now,@now,@State)");
            strSql.Append(";SELECT SCOPE_IDENTITY()");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UID", DbType.Int32, model.UID);
            db.AddInParameter(dbCommand, "Name", DbType.AnsiString, model.Name);
            db.AddInParameter(dbCommand, "Description", DbType.AnsiString, model.Description);
            db.AddInParameter(dbCommand, "State", DbType.Int32, model.State);
            int id = 0; int.TryParse(Convert.ToString(db.ExecuteScalar(dbCommand)), out id);
            return id;
        }

    }
}

