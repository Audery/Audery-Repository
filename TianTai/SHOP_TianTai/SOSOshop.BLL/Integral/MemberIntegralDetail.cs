using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace SOSOshop.BLL.Integral
{
    /// <summary>
    /// 会员积分明细
    /// </summary>
    public class MemberIntegralDetail : DbBase
    {
        #region  Method
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SOSOshop.Model.Integral.MemberIntegralDetail model, DbTransaction tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MemberIntegralDetail(");
            strSql.Append("uid,integral,remarks,action,created)");

            strSql.Append(" values (");
            strSql.Append("@uid,@integral,@remarks,@action,@created)");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "uid", DbType.Int32, model.uid);
            db.AddInParameter(dbCommand, "integral", DbType.Int32, model.integral);
            db.AddInParameter(dbCommand, "remarks", DbType.String, model.remarks);
            db.AddInParameter(dbCommand, "action", DbType.String, model.action);
            db.AddInParameter(dbCommand, "created", DbType.DateTime, model.created);
            db.ExecuteScalar(dbCommand, tran);
        }
        #endregion  Method

        #region 列表
        public DataTable GetList(string where, string orderby, int pageindex = 1, int pagesize = 15)
        {
            string sql = @"SELECT * FROM (" + table + ") AS T WHERE 1=1 " + where;

            if (pageindex <= 1) pageindex = 1;
            if (pagesize <= 1) pagesize = 1;
            sql = string.Format("SELECT * FROM (SELECT *,ROW_NUMBER() OVER(ORDER BY {3}) AS I FROM ({0}) AS T) AS T WHERE I BETWEEN ({1}*({2}-1))+1 AND ({1}*{2})",
                sql, pagesize, pageindex, orderby);

            return base.ExecuteDataSet(sql).Tables[0];
        }
        public int GetListCount(string where)
        {
            string sql = @"SELECT Count(*) FROM (" + table + ") AS T WHERE 1=1 " + where;
            object obj = base.ExecuteScalar(sql);
            int result = 0; if (obj != null) int.TryParse(obj.ToString(), out result);
            return result;
        }

        public string table
        {
            get
            {
                return @"SELECT b.TrueName AS truename,ISNULL((SELECT [Name] FROM DrugsBase_Enterprise WHERE ID=b.ParentId),'') AS CompanyName,
a.MobilePhone AS phone,a.CompanyClass,c.* 
FROM memberaccount a INNER JOIN memberinfo b ON a.UID=b.UID INNER JOIN MemberIntegralDetail c ON a.UID=c.uid";
            }
        }
        #endregion

    }
}
