using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using Library.Data;
namespace SOSOshop.BLL
{
    /// <summary>
    /// 缺货通知表
    /// </summary>
    public partial class Stockout : Db
    {
        public Stockout()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int UID, int Product_ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Stockout where UID=@UID and  Product_ID=@Product_ID");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UID", DbType.Int32, UID);
            db.AddInParameter(dbCommand, "Product_ID", DbType.Int32, Product_ID);
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
        /// 增加一条数据
        /// </summary>
        public int Add(SOSOshop.Model.Stockout model)
        {
            if (Exists(model.UID, model.Product_ID))
            {
                return 0;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Stockout(");
            strSql.Append("UID,Product_ID,Num,created)");

            strSql.Append(" values (");
            strSql.Append("@UID,@Product_ID,@Num,@created)");
            strSql.Append(";select @@IDENTITY");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UID", DbType.Int32, model.UID);
            db.AddInParameter(dbCommand, "Product_ID", DbType.Int32, model.Product_ID);
            db.AddInParameter(dbCommand, "Num", DbType.Int32, model.Num);
            db.AddInParameter(dbCommand, "created", DbType.DateTime, model.created);
            int result;
            object obj = db.ExecuteScalar(dbCommand);
            if (!int.TryParse(obj.ToString(), out result))
            {
                return 0;
            }
            return result;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SOSOshop.Model.Stockout model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Stockout set ");
            strSql.Append("UID=@UID,");
            strSql.Append("Product_ID=@Product_ID,");
            strSql.Append("Num=@Num,");
            strSql.Append("created=@created");
            strSql.Append(" where Id=@Id ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Id", DbType.Int32, model.Id);
            db.AddInParameter(dbCommand, "UID", DbType.Int32, model.UID);
            db.AddInParameter(dbCommand, "Product_ID", DbType.Int32, model.Product_ID);
            db.AddInParameter(dbCommand, "Num", DbType.Int32, model.Num);
            db.AddInParameter(dbCommand, "created", DbType.DateTime, model.created);
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
        public bool Delete(int Id, int uid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Stockout ");
            strSql.Append(" where Product_ID=@Product_ID and uid=@uid");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Product_ID", DbType.Int32, Id);
            db.AddInParameter(dbCommand, "uid", DbType.Int32, uid);
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
        public bool DeleteAll(int uid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Stockout ");
            strSql.Append(" where  uid=@uid");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "uid", DbType.Int32, uid);
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
        #endregion  Method
    }
}

