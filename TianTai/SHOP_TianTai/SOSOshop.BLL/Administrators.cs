using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace SOSOshop.BLL
{
    /// <summary>
    /// 数据访问类Administrators。
    /// </summary>
    public class Administrators : Db
    {
        public Administrators()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string adminName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from yxs_administrators");
            strSql.Append(" where name=@name ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "name", DbType.AnsiString, adminName);
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
        public int Add(Model.Administrators model)
        {
            if (!Exists(model.Name))
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into yxs_administrators(");
                strSql.Append("name,password,state,managebegintime,manageendtime,power,allowmodifypassword,role,OfficePhone,HomePhone,MobilePhone,LoginAuthenticationOfficePhone,QQ)");
                strSql.Append(" values (");
                strSql.Append("@name,@password,@state,@managebegintime,@manageendtime,@power,@allowmodifypassword,@role,@OfficePhone,@HomePhone,@MobilePhone,@LoginAuthenticationOfficePhone,@QQ)");
                strSql.Append(";select @@IDENTITY");

                DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
                db.AddInParameter(dbCommand, "name", DbType.AnsiString, model.Name);
                db.AddInParameter(dbCommand, "password", DbType.AnsiString, ChangeHope.Common.DEncryptHelper.Encrypt(model.PassWord, 1));
                db.AddInParameter(dbCommand, "state", DbType.Int32, model.State);
                db.AddInParameter(dbCommand, "managebegintime", DbType.DateTime, model.ManageBeginTime);
                db.AddInParameter(dbCommand, "manageendtime", DbType.DateTime, model.ManageEndTime);
                db.AddInParameter(dbCommand, "power", DbType.Int32, model.Power);
                db.AddInParameter(dbCommand, "allowmodifypassword", DbType.Int32, model.AllowModifyPassWord);
                db.AddInParameter(dbCommand, "role", DbType.AnsiString, model.Role);
                db.AddInParameter(dbCommand, "OfficePhone", DbType.AnsiString, model.OfficePhone);
                db.AddInParameter(dbCommand, "HomePhone", DbType.AnsiString, model.HomePhone);
                db.AddInParameter(dbCommand, "MobilePhone", DbType.AnsiString, model.MobilePhone);
                db.AddInParameter(dbCommand, "LoginAuthenticationOfficePhone", DbType.AnsiString, model.LoginAuthenticationOfficePhone);
                db.AddInParameter(dbCommand, "QQ", DbType.AnsiString, model.QQ);
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
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.Administrators model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update yxs_administrators set ");
            strSql.Append("name=@name,");
            if (model.PassWord.Length > 0)
            {
                strSql.Append("password=@password,");
            }
            strSql.Append("state=@state,");
            strSql.Append("managebegintime=@managebegintime,");
            strSql.Append("manageendtime=@manageendtime,");
            strSql.Append("power=@power,");
            strSql.Append("role=@role,");
            strSql.Append("OfficePhone=@OfficePhone,");
            strSql.Append("HomePhone=@HomePhone,");
            strSql.Append("MobilePhone=@MobilePhone,");
            strSql.Append("LoginAuthenticationOfficePhone=@LoginAuthenticationOfficePhone,");
            strSql.Append("QQ=@QQ,");
            strSql.Append("allowmodifypassword=@allowmodifypassword ");
            strSql.Append(" where adminid=@adminid ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "adminid", DbType.Int32, model.AdminId);
            db.AddInParameter(dbCommand, "name", DbType.AnsiString, model.Name);
            db.AddInParameter(dbCommand, "password", DbType.AnsiString, ChangeHope.Common.DEncryptHelper.Encrypt(model.PassWord, 1));
            db.AddInParameter(dbCommand, "state", DbType.Int32, model.State);
            db.AddInParameter(dbCommand, "managebegintime", DbType.DateTime, model.ManageBeginTime);
            db.AddInParameter(dbCommand, "manageendtime", DbType.DateTime, model.ManageEndTime);
            db.AddInParameter(dbCommand, "power", DbType.Int32, model.Power);
            db.AddInParameter(dbCommand, "allowmodifypassword", DbType.Int32, model.AllowModifyPassWord);
            db.AddInParameter(dbCommand, "role", DbType.AnsiString, model.Role);
            db.AddInParameter(dbCommand, "OfficePhone", DbType.AnsiString, model.OfficePhone);
            db.AddInParameter(dbCommand, "HomePhone", DbType.AnsiString, model.HomePhone);
            db.AddInParameter(dbCommand, "MobilePhone", DbType.AnsiString, model.MobilePhone);
            db.AddInParameter(dbCommand, "LoginAuthenticationOfficePhone", DbType.AnsiString, model.LoginAuthenticationOfficePhone);
            db.AddInParameter(dbCommand, "QQ", DbType.AnsiString, model.QQ);

            return 0 < db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 修改任一字段的记录
        /// </summary>
        /// <param name="uID"></param>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Amend(int id, string columnName, Object value)
        {
            string sequel = "Update [yxs_administrators] set ";
            sequel = sequel + "[" + columnName + "] =@Value ";
            sequel = sequel + "  where adminid=@adminid";

            DbCommand dbCommand = db.GetSqlStringCommand(sequel.ToString());
            db.AddInParameter(dbCommand, "adminid", DbType.Int32, id);
            db.AddInParameter(dbCommand, "Value", DbType.AnsiString, value);

            return db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int adminId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from yxs_administrators ");
            strSql.Append(" where adminid=@adminid and name<>'admin'");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "adminid", DbType.Int32, adminId);

            return 0 < db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 通过DataRow获取一个实例
        /// </summary>
        protected Model.Administrators GetModelByDataRow(DataRow row)
        {
            Model.Administrators model = new Model.Administrators();
            model.AdminId = int.Parse(row["adminid"].ToString());
            model.Name = row["name"].ToString();
            model.PassWord = row["password"].ToString();
            if (row["state"].ToString() != "")
            {
                model.State = int.Parse(row["state"].ToString());
            }
            if (row["managebegintime"].ToString() != "")
            {
                model.ManageBeginTime = DateTime.Parse(row["managebegintime"].ToString());
            }
            if (row["manageendtime"].ToString() != "")
            {
                model.ManageEndTime = DateTime.Parse(row["manageendtime"].ToString());
            }
            if (row["power"].ToString() != "")
            {
                model.Power = int.Parse(row["power"].ToString());
            }
            if (row["allowmodifypassword"].ToString() != "")
            {
                model.AllowModifyPassWord = int.Parse(row["allowmodifypassword"].ToString());
            }
            model.Role = row["role"].ToString();
            return model;
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Administrators GetModel(int adminId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select adminid,name,password,state,managebegintime,manageendtime,power,allowmodifypassword,role,OfficePhone,HomePhone,MobilePhone,LoginAuthenticationOfficePhone,QQ from yxs_administrators ");
            strSql.Append(" where adminid=@adminid ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "adminid", DbType.Int32, adminId);
            SOSOshop.Model.Administrators model = null;
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
        /// 对象实体绑定数据
        /// </summary>
        public SOSOshop.Model.Administrators ReaderBind(IDataReader dataReader)
        {
            SOSOshop.Model.Administrators model = new Model.Administrators();
            object ojb;
            ojb = dataReader["adminid"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AdminId = (int)ojb;
            }
            model.Name = dataReader["name"].ToString();
            model.PassWord = dataReader["password"].ToString();
            ojb = dataReader["state"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AllowModifyPassWord = int.Parse(ojb.ToString());
            }
            ojb = dataReader["managebegintime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ManageBeginTime = (DateTime)ojb;
            }
            ojb = dataReader["manageendtime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ManageEndTime = (DateTime)ojb;
            }
            ojb = dataReader["power"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Power = int.Parse(ojb.ToString());
            }
            ojb = dataReader["allowmodifypassword"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AllowModifyPassWord = int.Parse(ojb.ToString());
            }
            model.Role = dataReader["role"].ToString();
            model.OfficePhone = dataReader["OfficePhone"].ToString();
            model.HomePhone = dataReader["HomePhone"].ToString();
            model.MobilePhone = dataReader["MobilePhone"].ToString();
            model.LoginAuthenticationOfficePhone = dataReader["LoginAuthenticationOfficePhone"].ToString();
            model.QQ = dataReader["QQ"].ToString();
            return model;
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Administrators GetModelByAdminName(string adminName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 adminid,name,password,state,managebegintime,manageendtime,power,allowmodifypassword,role,officePhone,homePhone,mobilePhone,LoginAuthenticationOfficePhone,QQ from yxs_administrators ");
            strSql.Append(" where name=@name ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "name", DbType.AnsiString, adminName);

            DataSet ds = db.ExecuteDataSet(dbCommand);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return GetModelByDataRow(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 得到一组对象实体
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public List<Model.Administrators> GetList(System.Data.DataTable table)
        {
            List<Model.Administrators> list = new List<Model.Administrators>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                list.Add(GetModelByDataRow(table.Rows[i]));
            }
            return list;
        }

        public string GetList()
        {
            ChangeHope.WebPage.DataView dataview = new ChangeHope.WebPage.DataView();
            dataview.Sql = "[select] *,(select top 1 loginintime from yxs_adminloginlog where adminname=yxs_administrators.name order by id desc)as logintime,(select top 1 loginip from yxs_adminloginlog where adminname=yxs_administrators.name order by id desc)as loginip [from] yxs_administrators [where] 1=1 [order by] state,adminid asc";
            dataview.RowHead = "序号/5%,管理员帐号/10%,最后登陆时间/15%,最后登陆IP/10%,是否冻结/10%,开始管理时间/15%,管理过期时间/15%,操作/20%";
            dataview.RowText = "{0}$@allnum,name,logintime,loginip,<script>GetStat('{0}');</script>$state,managebegintime,manageendtime,<a href='admin_edit.aspx?adminid={0}'>编辑</a>  <a href='?action=del&adminid={0}' onclick=\"return confirm('是否删除该数据')\">删除</a><script>OtherAction('{0}—{1}—{2}—{3}—{4}—{5}')</script>$adminid|OfficePhone|HomePhone|MobilePhone|LoginAuthenticationOfficePhone|QQ";
            string view = dataview.GetView();
            dataview = null;
            return view;
        }

        

        /// <summary>
        /// 根据区域获取交易员Id
        /// </summary>
        public string GetOutSellPersonIdByRegion(int provinceId, int cityId, int countyId, out int personId, string customerType)
        {
            personId = 0;
            string personName = null;
            string sql = string.Format("SELECT top 1 PersonId, PersonName FROM dbo.ResponseRegionsOfOutSellPerson WHERE ResponseProvince={0} and ResponseCity={1} AND ResponseCounty={2} and customerType='{3}'", provinceId, cityId, countyId, customerType);
            DataTable dt = base.ExecuteTable(sql);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    personName = dr["PersonName"].ToString();
                    personId = Convert.ToInt32(dr["PersonId"].ToString());
                }
            }

            return personName;
        }

        public ChangeHope.DataBase.DataByPage GetListDB()
        {
            ChangeHope.DataBase.DataByPage dataPage = new ChangeHope.DataBase.DataByPage();
            dataPage.PageSize = 100;
            dataPage.Sql = "[select] *,(select top 1 loginintime from yxs_adminloginlog where adminname=yxs_administrators.name order by id desc)as logintime,(select top 1 loginip from yxs_adminloginlog where adminname=yxs_administrators.name order by id desc)as loginip [from] yxs_administrators [where] 1=1 [order by] state,adminid asc";
            dataPage.GetRecordSetByPage();
            return dataPage;
        }

        public ChangeHope.DataBase.DataByPage GetListDB(string where)
        {
            ChangeHope.DataBase.DataByPage dataPage = new ChangeHope.DataBase.DataByPage();
            dataPage.PageSize = 100;
            dataPage.Sql = "[select] *,(select top 1 loginintime from yxs_adminloginlog where adminname=yxs_administrators.name order by id desc)as logintime,(select top 1 loginip from yxs_adminloginlog where adminname=yxs_administrators.name order by id desc)as loginip [from] yxs_administrators [where] 1=1 " + where + " [order by] state,adminid asc";
            dataPage.GetRecordSetByPage();
            return dataPage;
        }

        #endregion  成员方法
    }
}
