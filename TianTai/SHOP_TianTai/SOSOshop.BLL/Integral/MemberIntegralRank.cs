using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SOSOshop.BLL.Integral
{
    /// <summary>
    /// 会员积分礼品
    /// </summary>
    public class MemberIntegralRank : DbBase
    {
        public DataTable GetList(string where, string orderby, int pageindex = 1, int pagesize = 15)
        {
            string sql = @"SELECT * FROM (" + table + ") AS T WHERE 1=1 " + where;

            if (pageindex <= 1) pageindex = 1;
            if (pagesize <= 1) pagesize = 1;
            sql = string.Format("SELECT * FROM (SELECT *,ROW_NUMBER() OVER(ORDER BY {3}) AS I FROM ({0}) AS T) AS T WHERE I BETWEEN ({1}*({2}-1))+1 AND ({1}*{2})",
                sql, pagesize, pageindex, orderby);

            return base.ExecuteDataSet(sql).Tables[0];
        }
        public DataTable GetList(string where, string date, string orderby, int pageindex = 1, int pagesize = 15)
        {
            string sql = @"SELECT * FROM (" + string.Format(table_date, date) + ") AS T WHERE 1=1 " + where;

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
        public int GetListCount(string where, string date)
        {
            string sql = @"SELECT Count(*) FROM (" + string.Format(table_date, date) + ") AS T WHERE 1=1 " + where;
            object obj = base.ExecuteScalar(sql);
            int result = 0; if (obj != null) int.TryParse(obj.ToString(), out result);
            return result;
        }

        public string table
        {
            get
            {
                return @"SELECT a.UID, b.TrueName AS truename,ISNULL((SELECT [Name] FROM DrugsBase_Enterprise WHERE ID=b.ParentId),'') AS CompanyName,
a.MobilePhone AS phone,a.CompanyClass,c.integral, c.realityIntegral, c.integral AS totalIntegral
FROM memberaccount a INNER JOIN memberinfo b ON a.UID=b.UID INNER JOIN MemberIntegral c ON a.UID=c.uid";
            }
        }
        public string table_date
        {
            get
            {
                return @"SELECT UID,truename,CompanyName,phone,CompanyClass,SUM(integral) AS integral,SUM(realityIntegral) AS realityIntegral, (SELECT TOP(1) integral FROM MemberIntegral WHERE uid=T.UID) AS totalIntegral FROM (
SELECT a.UID, b.TrueName AS truename,ISNULL((SELECT [Name] FROM DrugsBase_Enterprise WHERE ID=b.ParentId),'') AS CompanyName,
a.MobilePhone AS phone,a.CompanyClass, (CASE WHEN c.[action] LIKE '增加%' THEN c.integral ELSE 0 END) AS integral,
(CASE WHEN c.[action] LIKE '增加%' THEN c.integral ELSE c.integral*-1 END) AS realityIntegral
FROM memberaccount a INNER JOIN memberinfo b ON a.UID=b.UID INNER JOIN MemberIntegralDetail c ON a.UID=c.uid {0}
) T WHERE CompanyName<>'' GROUP BY UID,truename,CompanyName,phone,CompanyClass";
            }
        }
    }
}
