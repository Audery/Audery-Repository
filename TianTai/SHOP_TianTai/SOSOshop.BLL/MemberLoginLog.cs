using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.Common;

namespace SOSOshop.BLL
{
    /// <summary>
    /// 数据访问类MemberLoginLog。
    /// </summary>
    public class MemberLoginLog : Db
    {
        public MemberLoginLog()
        { }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(int UID, string name, string password)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into memberloginlog(");
            strSql.Append("UID, name, nameclass, password, loginintime, loginouttime, loginip, loginregion, hostcomputername, operatenote)");
            strSql.Append(" values (");
            strSql.Append("@UID, @name, @nameclass, @password, @loginintime, @loginouttime, @loginip, @loginregion, @hostcomputername, @operatenote)");
            strSql.Append(";SELECT SCOPE_IDENTITY();UPDATE dbo.memberaccount SET LoginCount=LoginCount+1 WHERE UID=" + UID);

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UID", DbType.Int32, UID);
            db.AddInParameter(dbCommand, "name", DbType.AnsiString, name);
            db.AddInParameter(dbCommand, "nameclass", DbType.Int32, new SOSOshop.BLL.MemberAccount().GetUserIdNameClass(name));
            db.AddInParameter(dbCommand, "password", DbType.AnsiString, password);
            DateTime now = DateTime.Now;
            db.AddInParameter(dbCommand, "loginintime", DbType.DateTime, now);
            db.AddInParameter(dbCommand, "loginouttime", DbType.DateTime, now);
            db.AddInParameter(dbCommand, "loginip", DbType.AnsiString,
                ChangeHope.WebPage.PageRequest.GetIP());
            db.AddInParameter(dbCommand, "loginregion", DbType.AnsiString,
                ChangeHope.WebPage.PageRequest.GetIPLocation());
            db.AddInParameter(dbCommand, "hostcomputername", DbType.AnsiString,
                System.Web.HttpContext.Current.Request.ServerVariables.Get("Remote_Addr").ToString());
            db.AddInParameter(dbCommand, "operatenote", DbType.AnsiString, "登陆成功!");
            object obj = db.ExecuteScalar(dbCommand);
            return obj != null ? Convert.ToInt32(obj) : 0;
        }

        /// <summary>
        /// 用户已经退出登陆
        /// </summary>
        public bool IsLogout(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT TOP (1) loginintime, loginouttime FROM memberloginlog WHERE name = @name AND operatenote = @operatenote ORDER BY loginintime DESC");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "name", DbType.AnsiString, name);
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
        /// 用户退出登陆
        /// </summary>
        public bool Logout(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE memberloginlog SET loginouttime=(GETDATE()) WHERE name = @name AND operatenote = @operatenote");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "adminname", DbType.AnsiString, name);
            db.AddInParameter(dbCommand, "operatenote", DbType.AnsiString, "登陆成功!");

            return 0 < db.ExecuteNonQuery(dbCommand);
        }
        #endregion
    }
}
