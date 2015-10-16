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
    public class MemberIntegralGift : DbBase
    {
        /// <summary>
        /// 取得积分礼品全部数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetList(string where)
        {
            string sql = "SELECT * FROM MemberIntegralGift WHERE 1=1 " + where;
            return base.ExecuteDataSet(sql).Tables[0];
        }
        public DataTable GetList(string where, string orderby, int pageindex = 1, int pagesize = 15)
        {
            string sql = "SELECT * FROM MemberIntegralGift WHERE 1=1 " + where;

            if (pageindex <= 1) pageindex = 1;
            if (pagesize <= 1) pagesize = 1;
            sql = string.Format("SELECT * FROM (SELECT *,ROW_NUMBER() OVER(ORDER BY {3}) AS I FROM ({0}) AS T) AS T WHERE I BETWEEN ({1}*({2}-1))+1 AND ({1}*{2})",
                sql, pagesize, pageindex, orderby);

            return base.ExecuteDataSet(sql).Tables[0];
        }
        public int GetListCount(string where)
        {
            object obj = base.ExecuteScalar("SELECT Count(*) FROM MemberIntegralGift WHERE 1=1 " + where);
            int result = 0; if (obj != null) int.TryParse(obj.ToString(), out result);
            return result;
        }

        public Model.Integral.MemberIntegralGift GetModel(int id)
        {
            Model.Integral.MemberIntegralGift model = null;
            DataTable dt = GetList("and id=" + id);
            if (dt.Rows.Count > 0)
            {
                model = new Model.Integral.MemberIntegralGift()
                {
                    id = (int)dt.Rows[0]["id"],
                    name = (string)dt.Rows[0]["name"],
                    detail = (string)dt.Rows[0]["detail"],
                    Integral = (decimal)dt.Rows[0]["Integral"],
                    Number = (decimal)dt.Rows[0]["Number"],
                    Member_Class = (string)dt.Rows[0]["Member_Class"],
                    State = (int)dt.Rows[0]["State"]
                };
            }
            return model;
        }

        public int Add(Model.Integral.MemberIntegralGift model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MemberIntegralGift(");
            strSql.Append("name,detail,Integral,Number,Member_Class,State)");
            strSql.Append(" values (");
            strSql.Append("@name,@detail,@Integral,@Number,@Member_Class,@State)");
            strSql.Append(";select @@IDENTITY");
            var dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "name", DbType.String, model.name);
            db.AddInParameter(dbCommand, "detail", DbType.String, model.detail);
            db.AddInParameter(dbCommand, "Integral", DbType.Decimal, model.Integral);
            db.AddInParameter(dbCommand, "Number", DbType.Decimal, model.Number);
            db.AddInParameter(dbCommand, "Member_Class", DbType.String, model.Member_Class);
            db.AddInParameter(dbCommand, "State", DbType.Int32, model.State);

            int result;
            object obj = db.ExecuteScalar(dbCommand);
            if (obj == null || !int.TryParse(obj.ToString(), out result))
            {
                return 0;
            }
            return result;
        }
        public bool Edit(Model.Integral.MemberIntegralGift model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MemberIntegralGift set ");
            strSql.Append("name=@name,detail=@detail,Integral=@Integral,Number=@Number,Member_Class=@Member_Class,State=@State");
            strSql.Append(" where id=@id");
            var dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "name", DbType.String, model.name);
            db.AddInParameter(dbCommand, "detail", DbType.String, model.detail);
            db.AddInParameter(dbCommand, "Integral", DbType.Decimal, model.Integral);
            db.AddInParameter(dbCommand, "Number", DbType.Decimal, model.Number);
            db.AddInParameter(dbCommand, "Member_Class", DbType.String, model.Member_Class);
            db.AddInParameter(dbCommand, "State", DbType.Int32, model.State);
            db.AddInParameter(dbCommand, "id", DbType.Int32, model.id);

            return 0 < db.ExecuteNonQuery(dbCommand);
        }
    }
    /// <summary>
    /// 可兑换客户类型
    /// </summary>
    public class MemberIntegralGift_Member_Class
    {
        public static string Get(string Member_Class, string Format)
        {
            StringBuilder str = new StringBuilder();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("0", "批发/连锁");
            dic.Add("1", "OTC终端");

            foreach (string s in Member_Class.Split(','))
            {
                if (s.Length > 0 && dic.ContainsKey(s))
                {
                    str.AppendFormat(Format, dic[s]);
                }
            }

            return str.Length > 0 ? str.ToString() : string.Format(Format, "未知");
        }

    }
}
