using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.Common;
using Library.Data;

namespace SOSOshop.BLL
{
    /// <summary>
    /// 数据访问类MemberReceAddress
    /// </summary>
    public class MemberReceAddress : Db
    {
        public MemberReceAddress()
        {
            //ChangeHope.DataBase.SQLServerHelper.connectionString = new SOSOshop.BLL.Db()._db.ConnectionString;
        }

        #region 成员方法

        /// <summary>
        /// 得到自增ID最大值
        /// </summary>
        public int GetMaxID()
        {
            return GetMaxID("UID", "memberreceaddress");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select count(1) from memberreceaddress where id=@id ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());

            db.AddInParameter(dbCommand, "id", DbType.Int32, id);

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
        public int Add(Model.MemberReceAddress model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("insert into memberreceaddress(");
            strSql.Append("uid,username,mobile,phone,province,city,borough,address,zip,email,constructionsigns,consignestime,stat)");
            strSql.Append(" values (");
            strSql.Append("@uid,@username,@mobile,@phone,@province,@city,@borough,@address,@zip,@email,@constructionsigns,@consignestime,@stat)");
            strSql.Append(";select @@IDENTITY");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());

            db.AddInParameter(dbCommand, "uid", DbType.Int32, model.UID);
            db.AddInParameter(dbCommand, "username", DbType.AnsiString, model.Name);
            db.AddInParameter(dbCommand, "mobile", DbType.AnsiString, model.Mobile);
            db.AddInParameter(dbCommand, "phone", DbType.AnsiString, model.Phone);
            db.AddInParameter(dbCommand, "province", DbType.AnsiString, model.Province);
            db.AddInParameter(dbCommand, "city", DbType.AnsiString, model.City);
            db.AddInParameter(dbCommand, "borough", DbType.AnsiString, model.Borough);
            db.AddInParameter(dbCommand, "address", DbType.AnsiString, model.Address);
            db.AddInParameter(dbCommand, "zip", DbType.AnsiString, model.Zip);
            db.AddInParameter(dbCommand, "email", DbType.AnsiString, model.Email);
            db.AddInParameter(dbCommand, "constructionsigns", DbType.AnsiString, model.ConstructionSigns);
            db.AddInParameter(dbCommand, "consignestime", DbType.AnsiString, model.Consignestime);
            db.AddInParameter(dbCommand, "stat", DbType.Boolean, model.Stat);

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
        public bool Update(SOSOshop.Model.MemberReceAddress model, int uid)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update memberreceaddress set ");
            strSql.Append("username=@username,");
            strSql.Append("mobile=@mobile,");
            strSql.Append("phone=@phone,");
            strSql.Append("province=@province,");
            strSql.Append("city=@city,");
            strSql.Append("borough=@borough,");
            strSql.Append("address=@address,");
            strSql.Append("zip=@zip,");
            strSql.Append("email=@email,");
            strSql.Append("constructionsigns=@constructionsigns,");
            strSql.Append("consignestime=@consignestime,");
            strSql.Append("stat=@stat");
            strSql.Append(" where id=@id and uid=@uid");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());

            db.AddInParameter(dbCommand, "id", DbType.Int32, model.ID);
            db.AddInParameter(dbCommand, "uid", DbType.Int32, model.UID);
            db.AddInParameter(dbCommand, "username", DbType.AnsiString, model.Name);
            db.AddInParameter(dbCommand, "mobile", DbType.AnsiString, model.Mobile);
            db.AddInParameter(dbCommand, "phone", DbType.AnsiString, model.Phone);
            db.AddInParameter(dbCommand, "province", DbType.AnsiString, model.Province);
            db.AddInParameter(dbCommand, "city", DbType.AnsiString, model.City);
            db.AddInParameter(dbCommand, "borough", DbType.AnsiString, model.Borough);
            db.AddInParameter(dbCommand, "address", DbType.AnsiString, model.Address);
            db.AddInParameter(dbCommand, "zip", DbType.AnsiString, model.Zip);
            db.AddInParameter(dbCommand, "email", DbType.AnsiString, model.Email);
            db.AddInParameter(dbCommand, "constructionsigns", DbType.AnsiString, model.ConstructionSigns);
            db.AddInParameter(dbCommand, "consignestime", DbType.AnsiString, model.Consignestime);
            db.AddInParameter(dbCommand, "stat", DbType.Boolean, model.Stat);

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
        public bool Delete(int id, int uid)
        {

            StringBuilder strSql = new StringBuilder();

            strSql.Append("delete from memberreceaddress ");
            strSql.Append(" where id=@id and uid=@uid ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());

            db.AddInParameter(dbCommand, "id", DbType.Int32, id);
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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("delete from memberreceaddress ");
            strSql.Append(" where id in (" + idlist + ")  ");

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
        public SOSOshop.Model.MemberReceAddress GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select id,uid,username,mobile,phone,province,city,borough,address,zip,email,constructionsigns,consignestime,stat from memberreceaddress ");
            strSql.Append(" where id=@id ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());

            db.AddInParameter(dbCommand, "id", DbType.Int32, id);

            SOSOshop.Model.MemberReceAddress model = null;

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    model = ReaderBind(dataReader);
                }
            }
            return model;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SOSOshop.Model.MemberReceAddress DataRowToModel(DataRow row)
        {
            SOSOshop.Model.MemberReceAddress model = new SOSOshop.Model.MemberReceAddress();

            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.ID = int.Parse(row["id"].ToString());
                }
                if (row["uid"] != null && row["uid"].ToString() != "")
                {
                    model.UID = int.Parse(row["uid"].ToString());
                }
                if (row["username"] != null)
                {
                    model.Name = row["username"].ToString();
                }
                if (row["mobile"] != null)
                {
                    model.Mobile = row["mobile"].ToString();
                }
                if (row["phone"] != null)
                {
                    model.Phone = row["phone"].ToString();
                }
                if (row["province"] != null)
                {
                    model.Province = row["province"].ToString();
                }
                if (row["city"] != null)
                {
                    model.City = row["city"].ToString();
                }
                if (row["borough"] != null)
                {
                    model.Borough = row["borough"].ToString();
                }
                if (row["address"] != null)
                {
                    model.Address = row["address"].ToString();
                }
                if (row["zip"] != null)
                {
                    model.Zip = row["zip"].ToString();
                }
                if (row["email"] != null)
                {
                    model.Email = row["email"].ToString();
                }
                if (row["constructionsigns"] != null)
                {
                    model.ConstructionSigns = row["constructionsigns"].ToString();
                }
                if (row["consignestime"] != null)
                {
                    model.Consignestime = row["consignestime"].ToString();
                }
                if (row["stat"] != null && row["stat"].ToString() != "")
                {
                    if ((row["stat"].ToString() == "1") || (row["stat"].ToString().ToLower() == "true"))
                    {
                        model.Stat = true;
                    }
                    else
                    {
                        model.Stat = false;
                    }
                }
            }

            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select id,uid,username,mobile,phone,province,city,borough,address,zip,email,constructionsigns,consignestime,stat ");
            strSql.Append(" FROM memberreceaddress ");

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

            strSql.Append(" id,uid,username,mobile,phone,province,city,borough,address,zip,email,constructionsigns,consignestime,stat ");
            strSql.Append(" FROM memberreceaddress ");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by " + filedOrder);

            return db.ExecuteDataSet(CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select count(1) FROM memberreceaddress ");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            object obj = DbHelperSQL.GetSingle(strSql.ToString());

            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<SOSOshop.Model.MemberReceAddress> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select id,uid,username,mobile,phone,province,city,borough,address,zip,email,constructionsigns,consignestime,stat ");
            strSql.Append(" FROM memberreceaddress ");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            List<SOSOshop.Model.MemberReceAddress> list = new List<SOSOshop.Model.MemberReceAddress>();

            using (IDataReader dataReader = db.ExecuteReader(CommandType.Text, strSql.ToString()))
            {
                while (dataReader.Read())
                {
                    list.Add(ReaderBind(dataReader));
                }
            }

            return list;
        }


        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public SOSOshop.Model.MemberReceAddress ReaderBind(IDataReader dataReader)
        {
            SOSOshop.Model.MemberReceAddress model = new SOSOshop.Model.MemberReceAddress();

            object ojb;

            ojb = dataReader["id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ID = (int)ojb;
            }
            ojb = dataReader["uid"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.UID = (int)ojb;
            }
            model.Name = dataReader["username"].ToString();
            model.Mobile = dataReader["mobile"].ToString();
            model.Phone = dataReader["phone"].ToString();
            model.Province = dataReader["province"].ToString();
            model.City = dataReader["city"].ToString();
            model.Borough = dataReader["borough"].ToString();
            model.Address = dataReader["address"].ToString();
            model.Zip = dataReader["zip"].ToString();
            model.Email = dataReader["email"].ToString();
            model.ConstructionSigns = dataReader["constructionsigns"].ToString();
            model.Consignestime = dataReader["consignestime"].ToString();
            ojb = dataReader["stat"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Stat = (bool)ojb;
            }

            return model;
        }

        #endregion
    }
}
