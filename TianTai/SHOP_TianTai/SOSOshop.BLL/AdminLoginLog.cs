using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace SOSOshop.BLL
{
    /// <summary>
    /// 数据访问类AdminLoginLog。
    /// </summary>
    public class AdminLoginLog : Db
    {
        public AdminLoginLog()
        { }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.AdminLoginLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into yxs_adminloginlog(");
            strSql.Append("adminname,password,loginintime,loginouttime,loginip,hostcomputername,operatenote)");
            strSql.Append(" values (");
            strSql.Append("@adminname,@password,@loginintime,@loginouttime,@loginip,@hostcomputername,@operatenote)");
            strSql.Append(";select @@IDENTITY");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "adminname", DbType.AnsiString, model.AdminName);
            db.AddInParameter(dbCommand, "password", DbType.AnsiString, model.PassWord);
            db.AddInParameter(dbCommand, "loginintime", DbType.DateTime, model.LoginInTime);
            db.AddInParameter(dbCommand, "loginouttime", DbType.DateTime, model.LoginOutTime);
            db.AddInParameter(dbCommand, "loginip", DbType.AnsiString, model.LoginIp);
            db.AddInParameter(dbCommand, "hostcomputername", DbType.AnsiString, model.HostComputerName);
            db.AddInParameter(dbCommand, "operatenote", DbType.AnsiString, model.OperateNote);
            object obj = db.ExecuteScalar(dbCommand);

            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from yxs_adminloginlog ");
            strSql.Append(" where id=@id ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "id", DbType.Int32, id);

            return 0 < db.ExecuteNonQuery(dbCommand);
        }
        /// <summary>
        /// 用户已经退出登陆
        /// </summary>
        public bool IsLogout(string AdminName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT TOP (1) loginintime, loginouttime FROM yxs_adminloginlog WHERE adminname = @adminname AND operatenote = @operatenote ORDER BY loginintime DESC");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "adminname", DbType.AnsiString, AdminName);
            db.AddInParameter(dbCommand, "operatenote", DbType.AnsiString, "登陆成功!");

            DataSet ds = db.ExecuteDataSet(dbCommand);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0 || Convert.ToDateTime(ds.Tables[0].Rows[0][0]).Ticks != Convert.ToDateTime(ds.Tables[0].Rows[0][1]).Ticks)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 用户已经登陆好久
        /// </summary>
        public DateTime IsLogin(string AdminName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT TOP (1) loginintime, loginouttime FROM yxs_adminloginlog WHERE adminname = @adminname AND operatenote = @operatenote ORDER BY loginintime DESC");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "adminname", DbType.AnsiString, AdminName);
            db.AddInParameter(dbCommand, "operatenote", DbType.AnsiString, "登陆成功!");

            DataSet ds = db.ExecuteDataSet(dbCommand);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0 || Convert.ToDateTime(ds.Tables[0].Rows[0][0]).Ticks != Convert.ToDateTime(ds.Tables[0].Rows[0][1]).Ticks)
            {
                return DateTime.MinValue;
            }
            else
            {
                return Convert.ToDateTime(ds.Tables[0].Rows[0][0]);
            }
        }

        #endregion  成员方法
    }
}
