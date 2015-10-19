using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SOSOshop.BLL.Integral
{
    /// <summary>
    /// 会员通过积分兑换礼品的情况
    /// </summary>
    public class MemberIntegralGiftExchange : DbBase
    {

        public Model.Integral.MemberIntegralGiftExchange GetModel(int id)
        {
            Model.Integral.MemberIntegralGiftExchange model = null;
            DataTable dt = GetList("and id=" + id, "id");
            if (dt.Rows.Count > 0)
            {
                model = new Model.Integral.MemberIntegralGiftExchange()
                {
                    id = (int)dt.Rows[0]["id"],
                    uid = (int)dt.Rows[0]["uid"],
                    Gift_ID = (int)dt.Rows[0]["Gift_ID"],
                    Gift_Number = (decimal)dt.Rows[0]["Gift_Number"],
                    State = (int)dt.Rows[0]["State"],
                    ontime = (DateTime)dt.Rows[0]["ontime"],
                    ConsigneeAddress = (string)dt.Rows[0]["ConsigneeAddress"],
                    ConsigneeName = (string)dt.Rows[0]["ConsigneeName"],
                    ConsigneePhone = (string)dt.Rows[0]["ConsigneePhone"],
                    Editer = (int)dt.Rows[0]["Editer"],

                    truename = (string)dt.Rows[0]["truename"],
                    phone = (string)dt.Rows[0]["phone"],
                    CompanyName = (string)dt.Rows[0]["CompanyName"],
                    GiftName = (string)dt.Rows[0]["GiftName"],
                    EditerName = (string)dt.Rows[0]["EditerName"],
                    Remark = dt.Rows[0].IsNull("remark") ? "" : (string)dt.Rows[0]["remark"]
                };
            }
            return model;
        }

        public int Add(Model.Integral.MemberIntegralGiftExchange model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MemberIntegralGiftExchange(");
            strSql.Append("uid,Gift_ID,Gift_Number,State,ontime,ConsigneeAddress,ConsigneeName,ConsigneePhone,Editer,remark)");
            strSql.Append(" values (");
            strSql.Append("@uid,@Gift_ID,@Gift_Number,@State,@ontime,@ConsigneeAddress,@ConsigneeName,@ConsigneePhone,@Editer,@remark)");
            strSql.Append(";select @@IDENTITY");
            var dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "uid", DbType.Int32, model.uid);
            db.AddInParameter(dbCommand, "Gift_ID", DbType.Int32, model.Gift_ID);
            db.AddInParameter(dbCommand, "Gift_Number", DbType.Decimal, model.Gift_Number);
            db.AddInParameter(dbCommand, "State", DbType.Int32, model.State);
            db.AddInParameter(dbCommand, "ontime", DbType.DateTime, model.ontime);
            db.AddInParameter(dbCommand, "ConsigneeAddress", DbType.String, model.ConsigneeAddress);
            db.AddInParameter(dbCommand, "ConsigneeName", DbType.String, model.ConsigneeName);
            db.AddInParameter(dbCommand, "ConsigneePhone", DbType.String, model.ConsigneePhone);
            db.AddInParameter(dbCommand, "Editer", DbType.Int32, model.Editer);
            db.AddInParameter(dbCommand, "remark", DbType.AnsiString, model.Remark);

            int result;
            object obj = db.ExecuteScalar(dbCommand);
            if (obj == null || !int.TryParse(obj.ToString(), out result))
            {
                return 0;
            }
            return result;
        }
        public bool Edit(Model.Integral.MemberIntegralGiftExchange model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MemberIntegralGiftExchange set ");
            strSql.Append("uid=@uid,Gift_ID=@Gift_ID,Gift_Number=@Gift_Number,State=@State,ontime=@ontime,ConsigneeAddress=@ConsigneeAddress,ConsigneeName=@ConsigneeName,ConsigneePhone=@ConsigneePhone,Editer=@Editer,remark=@remark");
            strSql.Append(" where id=@id");
            var dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "uid", DbType.Int32, model.uid);
            db.AddInParameter(dbCommand, "Gift_ID", DbType.Int32, model.Gift_ID);
            db.AddInParameter(dbCommand, "Gift_Number", DbType.Decimal, model.Gift_Number);
            db.AddInParameter(dbCommand, "State", DbType.Int32, model.State);
            db.AddInParameter(dbCommand, "ontime", DbType.DateTime, model.ontime);
            db.AddInParameter(dbCommand, "ConsigneeAddress", DbType.String, model.ConsigneeAddress);
            db.AddInParameter(dbCommand, "ConsigneeName", DbType.String, model.ConsigneeName);
            db.AddInParameter(dbCommand, "ConsigneePhone", DbType.String, model.ConsigneePhone);
            db.AddInParameter(dbCommand, "Editer", DbType.Int32, model.Editer);
            db.AddInParameter(dbCommand, "id", DbType.Int32, model.id);
            db.AddInParameter(dbCommand, "remark", DbType.AnsiString, model.Remark);

            return 0 < db.ExecuteNonQuery(dbCommand);
        }

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
                return @"SELECT e.*, b.TrueName AS truename,ISNULL((SELECT [Name] FROM DrugsBase_Enterprise WHERE ID=b.ParentId),'') AS CompanyName,
a.MobilePhone AS phone,a.CompanyClass, g.[name] AS GiftName, ISNULL((SELECT [name] FROM yxs_administrators WHERE adminid=e.Editer),'') AS EditerName
FROM memberaccount a INNER JOIN memberinfo b ON a.UID=b.UID INNER JOIN MemberIntegral c ON a.UID=c.uid
INNER JOIN MemberIntegralGiftExchange e ON a.UID=e.uid INNER JOIN MemberIntegralGift g ON e.Gift_ID=g.id";
            }
        }
    }
}
