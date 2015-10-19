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
    /// 购买（下订单）的模板的商品信息
    /// </summary>
    public partial class OrderProductCartPro : Db
    {
        public OrderProductCartPro()
        { }
        #region  Method

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SOSOshop.Model.Order.OrderProductCartPro model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OrderProductCartPro set ");
            strSql.Append("CartId=@CartId,");
            strSql.Append("ProId=@ProId,");
            strSql.Append("ProNum=@ProNum");
            strSql.Append(" where Id=@Id ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CartId", DbType.Int32, model.CartId);
            db.AddInParameter(dbCommand, "ProId", DbType.Int32, model.ProId);
            db.AddInParameter(dbCommand, "ProNum", DbType.Int32, model.ProNum);
            db.AddInParameter(dbCommand, "Id", DbType.Int32, model.Id);
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
            strSql.Append("delete from OrderProductCartPro where Id=@Id ");

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
        public SOSOshop.Model.Order.OrderProductCartPro GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [Id],[CartId],[ProId],[ProNum] FROM [OrderProductCartPro] ");
            strSql.AppendFormat(" where Id={0} ", Id);
            var items = db.ExecuteSqlStringAccessor<SOSOshop.Model.Order.OrderProductCartPro>(strSql.ToString());
            return items.Count() > 0 ? items.First() : null;
        }
        /// <summary>
        /// 得到一组对象实体
        /// </summary>
        public IEnumerable<SOSOshop.Model.Order.OrderProductCartPro> GetList(int CartId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [Id],[CartId],[ProId],[ProNum] FROM [OrderProductCartPro] ");
            strSql.AppendFormat(" where CartId={0} ", CartId);
            return db.ExecuteSqlStringAccessor<SOSOshop.Model.Order.OrderProductCartPro>(strSql.ToString());
        }
        #endregion  Method

        /// <summary>
        /// 增加一条数据([CartId],[ProId],[ProNum])
        /// </summary>
        public void Add(SOSOshop.Model.Order.OrderProductCartPro model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into OrderProductCartPro(");
            strSql.Append("[CartId],[ProId],[ProNum])");

            strSql.Append(" values (");
            strSql.Append("@CartId,@ProId,@ProNum)");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CartId", DbType.Int32, model.CartId);
            db.AddInParameter(dbCommand, "ProId", DbType.Int32, model.ProId);
            db.AddInParameter(dbCommand, "ProNum", DbType.Int32, model.ProNum);
            db.ExecuteScalar(dbCommand);
        }
        /// <summary>
        /// 增加一条数据([CartId],[ProId],[ProNum])
        /// </summary>
        public void Add(int CartId, string pids)
        {

            StringBuilder strSql = new StringBuilder();
            foreach (string pid in pids.Split(','))
            {
                int i = 0; int.TryParse(pid, out i);
                strSql.Append("insert into OrderProductCartPro([CartId],[ProId],[ProNum]) values(" + CartId + "," + i + ",0)\n");
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.ExecuteScalar(dbCommand);
        }

    }
}

