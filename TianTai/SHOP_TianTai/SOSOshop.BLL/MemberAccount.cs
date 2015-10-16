using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.Common;
using SOSOshop.Model;

namespace SOSOshop.BLL
{
    /// <summary>
    /// 数据访问类MemberAccount。
    /// </summary>
    public class MemberAccount : Db
    {
        public MemberAccount()
        {
            //ChangeHope.DataBase.SQLServerHelper.connectionString = _db.ConnectionString;//new SOSOshop.BLL.Db()._db.ConnectionString;
        }
        #region  成员方法

        /// <summary>
        /// 得到自增ID最大值
        /// </summary>
        public int GetMaxID()
        {
            return GetMaxID("UID", "memberaccount");
        }
        /// <summary>
        /// 得到买家类别
        /// </summary>
        public Model.MemberKeyValue.Member_Class GetMember_Class(int UID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Member_Class from memberinfo");
            strSql.Append(" where Member_Class>=0 and UID=" + UID);
            object obj = ExecuteScalar(strSql.ToString());
            if (obj == null)
            {
                return Model.MemberKeyValue.Member_Class.无;
            }
            else
            {
                return (Model.MemberKeyValue.Member_Class)Enum.Parse(typeof(Model.MemberKeyValue.Member_Class), obj.ToString());
            }
        }
        /// <summary>
        /// 得到会员类别
        /// </summary>
        public Model.MemberKeyValue.UserType GetUserType(int UID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserType from memberaccount");
            strSql.Append(" where UserType>=0 and UID=" + UID);
            object obj = ExecuteScalar(strSql.ToString());
            if (obj == null)
            {
                return Model.MemberKeyValue.UserType.无;
            }
            else
            {
                return (Model.MemberKeyValue.UserType)Enum.Parse(typeof(Model.MemberKeyValue.UserType), obj.ToString());
            }
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int UID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from memberaccount");
            strSql.Append(" where UID=" + UID);
            object obj = ExecuteScalar(strSql.ToString());
            if (obj == null)
            {
                return false;
            }
            else
            {
                return 0 < Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsCode(string Code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from memberinfo");
            strSql.AppendFormat(" where Code='{0}'", Code);
            object obj = ExecuteScalar(strSql.ToString());
            if (obj == null)
            {
                return false;
            }
            else
            {
                return 0 < Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 是否存在该账号[包括手机号和邮箱]
        /// </summary>
        public bool Exists(string userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from memberaccount");

            strSql.Append(" where " + GetUserIdSqlParam(userId));

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserId", DbType.AnsiString, userId);
            return 0 < Convert.ToInt32(db.ExecuteScalar(dbCommand));
        }

        /// <summary>
        /// 是否存在该Email_QQ记录
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool ExistEmail_QQ(string email_qq)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from memberaccount");
            strSql.Append(" where Email_QQ=@Email_QQ ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Email_QQ", DbType.AnsiString, email_qq);
            return 0 < Convert.ToInt32(db.ExecuteScalar(dbCommand));
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.MemberAccount model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("if(not exists(select 1 from memberaccount where (@UserId<>'' and UserId=@UserId) or (@MobilePhone<>'' and MobilePhone=@MobilePhone) or (@Email<>'' and Email=@Email))) begin insert into memberaccount(");
            strSql.Append("UserType, UserGroup, UserId, MobilePhone, Email, Email_QQ, PassWord, Question, Answer, State, RegisterDate, RegisterIP, Capital, Coupons, Points, PeriodOfValidity, CompanyClass)");
            strSql.Append(" values (");
            strSql.Append("@UserType, @UserGroup, @UserId, @MobilePhone, @Email, @Email_QQ, @PassWord, @Question, @Answer, @State, @RegisterDate, @RegisterIP, @Capital, @Coupons, @Points, @PeriodOfValidity, @CompanyClass)");
            strSql.Append(" SELECT SCOPE_IDENTITY() end");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserType", DbType.Int32, model.UserType);
            db.AddInParameter(dbCommand, "UserGroup", DbType.Int32, model.UserGroup);
            db.AddInParameter(dbCommand, "UserId", DbType.AnsiString, model.UserId);
            db.AddInParameter(dbCommand, "MobilePhone", DbType.AnsiString, model.MobilePhone);
            db.AddInParameter(dbCommand, "Email", DbType.AnsiString, model.Email);
            db.AddInParameter(dbCommand, "Email_QQ", DbType.AnsiString, model.Email_QQ);
            db.AddInParameter(dbCommand, "PassWord", DbType.AnsiString, model.PassWord);
            db.AddInParameter(dbCommand, "Question", DbType.AnsiString, model.Question);
            db.AddInParameter(dbCommand, "Answer", DbType.AnsiString, model.Answer);
            db.AddInParameter(dbCommand, "State", DbType.Int32, model.State);
            db.AddInParameter(dbCommand, "RegisterDate", DbType.DateTime, model.RegisterDate);
            db.AddInParameter(dbCommand, "RegisterIP", DbType.AnsiString, model.RegisterIP);
            db.AddInParameter(dbCommand, "Capital", DbType.Decimal, model.Capital);
            db.AddInParameter(dbCommand, "Coupons", DbType.Int32, model.Coupons);
            db.AddInParameter(dbCommand, "Points", DbType.Int32, model.Points);
            db.AddInParameter(dbCommand, "PeriodOfValidity", DbType.DateTime, model.PeriodOfValidity);
            db.AddInParameter(dbCommand, "CompanyClass", DbType.String, model.CompanyClass);
            object obj = db.ExecuteScalar(dbCommand);
            return obj != null ? Convert.ToInt32(obj) : 0;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.MemberAccount model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update memberaccount set ");
            strSql.Append("UserType=@UserType,");
            strSql.Append("UserGroup=@UserGroup,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("MobilePhone=@MobilePhone,");
            strSql.Append("Email=@Email,");
            strSql.Append("Email_QQ=@Email_QQ,");
            if (model.PassWord.Length > 0)
            {
                strSql.Append("PassWord=@PassWord,");
            }
            strSql.Append("Question=@Question,");
            strSql.Append("Answer=@Answer,");
            strSql.Append("State=@State,");
            strSql.Append("Coupons=@Coupons,");
            strSql.Append("Capital=@Capital,");
            strSql.Append("Points=@Points,");
            strSql.Append("PeriodOfValidity=@PeriodOfValidity,");
            strSql.Append("CompanyClass=@CompanyClass");
            strSql.Append(" where UID=@UID ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UID", DbType.Int32, model.UID);
            db.AddInParameter(dbCommand, "UserType", DbType.Int32, model.UserType);
            db.AddInParameter(dbCommand, "UserGroup", DbType.Int32, model.UserGroup);
            db.AddInParameter(dbCommand, "UserId", DbType.AnsiString, model.UserId);
            db.AddInParameter(dbCommand, "MobilePhone", DbType.AnsiString, model.MobilePhone);
            db.AddInParameter(dbCommand, "Email", DbType.AnsiString, model.Email);
            db.AddInParameter(dbCommand, "Email_QQ", DbType.AnsiString, model.Email_QQ);
            db.AddInParameter(dbCommand, "PassWord", DbType.AnsiString, model.PassWord);
            db.AddInParameter(dbCommand, "Question", DbType.AnsiString, model.Question);
            db.AddInParameter(dbCommand, "Answer", DbType.AnsiString, model.Answer);
            db.AddInParameter(dbCommand, "State", DbType.Int32, model.State);
            db.AddInParameter(dbCommand, "RegisterDate", DbType.DateTime, model.RegisterDate);
            db.AddInParameter(dbCommand, "RegisterIP", DbType.AnsiString, model.RegisterIP);
            db.AddInParameter(dbCommand, "Capital", DbType.Decimal, model.Capital);
            db.AddInParameter(dbCommand, "Coupons", DbType.Int32, model.Coupons);
            db.AddInParameter(dbCommand, "Points", DbType.Int32, model.Points);
            db.AddInParameter(dbCommand, "PeriodOfValidity", DbType.DateTime, model.PeriodOfValidity);
            db.AddInParameter(dbCommand, "CompanyClass", DbType.String, model.CompanyClass);
            return 0 < db.ExecuteNonQuery(dbCommand);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int UID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("begin transaction declare @errors int ");
            strSql.Append("delete from memberaccount where UID=@UID ");
            strSql.Append("set @errors=@errors+@@error ");
            strSql.Append("delete from memberinfo where UID=@UID ");
            strSql.Append("set @errors=@errors+@@error ");
            strSql.Append("delete from membercheck where UID=@UID ");
            strSql.Append("set @errors=@errors+@@error ");
            strSql.Append("delete from memberfavorite where UID=@UID ");
            strSql.Append("set @errors=@errors+@@error ");
            strSql.Append("delete from memberpermission where UID=@UID ");
            strSql.Append("set @errors=@errors+@@error ");
            strSql.Append("delete from memberloginlog where UID=@UID ");
            strSql.Append("set @errors=@errors+@@error ");
            strSql.Append("if @errors>0 begin rollback transaction end else begin commit transaction end ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UID", DbType.Int32, UID);
            return 0 < db.ExecuteNonQuery(dbCommand);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        public bool DeleteAll(string ids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("begin transaction declare @errors int ");
            strSql.Append("delete from memberaccount where UID in(" + ids + ") ");
            strSql.Append("set @errors=@errors+@@error ");
            strSql.Append("delete from memberinfo where UID in(" + ids + ") ");
            strSql.Append("set @errors=@errors+@@error ");
            strSql.Append("delete from membercheck where UID in(" + ids + ") ");
            strSql.Append("set @errors=@errors+@@error ");
            strSql.Append("delete from memberfavorite where UID in(" + ids + ") ");
            strSql.Append("set @errors=@errors+@@error ");
            strSql.Append("delete from memberpermission where UID in(" + ids + ") ");
            strSql.Append("set @errors=@errors+@@error ");
            strSql.Append("delete from memberloginlog where UID in(" + ids + ") ");
            strSql.Append("set @errors=@errors+@@error ");
            strSql.Append("if @errors>0 begin rollback transaction end else begin commit transaction end ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            return 0 < db.ExecuteNonQuery(dbCommand);
        }
        /// <summary>
        /// 批量解锁与冻结
        /// </summary>
        /// <param name="ids">id集合</param>
        /// <param name="flag">是否冻结为真解锁</param>
        public bool LockAddUnLock(string ids, bool flag)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update memberaccount set ");
            if (flag == true)
            {  //解锁
                strSql.Append("State=0 ");
            }
            else
            {  //冻结
                strSql.Append("State=1 ");
            }
            strSql.Append(" where UID in(");
            strSql.Append(ids + " )");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            return 0 < db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 更新任意一个字段
        /// </summary>
        /// <param name="id"></param>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Amend(int id, string columnName, object value)
        {
            string sequel = "Update memberaccount set ";
            sequel += "[" + columnName + "] = @Value ";
            sequel += "Where UID = @UID";

            DbCommand dbCommand = db.GetSqlStringCommand(sequel);
            db.AddInParameter(dbCommand, "Value", DbType.AnsiString, value);
            db.AddInParameter(dbCommand, "UID", DbType.Int32, id);
            return 0 < db.ExecuteNonQuery(dbCommand);
        }
        public Model.MemberAccount GetModelNoCache(int UID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from memberaccount ");
            strSql.AppendFormat(" where UID={0} ", UID);
            return GetModelByDataSet(base.ExecuteDataSet(strSql.ToString()));
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.MemberAccount GetModel(int UID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from memberaccount ");
            strSql.AppendFormat(" where UID={0} ", UID);
            return GetModelByDataSet(base.ExecuteDataSetForCache(strSql.ToString(), "BLL_MemberPermission_GetModel_" + UID, DateTime.Now.AddMinutes(30)));
        }
        /// <summary>
        /// 得到101商城的一个对象实体
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public Model.MemberAccount GetModel(string userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from memberaccount ");

            strSql.Append(" where " + GetUserIdSqlParam(userId));

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserId", DbType.AnsiString, userId);
            DataSet ds = db.ExecuteDataSet(dbCommand);
            return GetModelByDataSet(ds);
        }

        /// <summary>
        /// 账号[包括手机号和邮箱]
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Model.MemberAccount GetModelByNameAndPassword(string userId, string password)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from memberaccount ");

            strSql.Append(" where " + GetUserIdSqlParam(userId));
            strSql.Append(" and PassWord=@PassWord ");//密码

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserId", DbType.AnsiString, userId);
            db.AddInParameter(dbCommand, "PassWord", DbType.AnsiString, password);
            DataSet ds = db.ExecuteDataSet(dbCommand);
            return GetModelByDataSet(ds);
        }
        /// <summary>
        /// 登陆验证, 账号[包括手机号和邮箱]
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Model.MemberAccount GetModelByNameAndPasswordIsChecked(string userId, string password)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from memberaccount ");

            strSql.Append(" where " + GetUserIdSqlParam(userId));
            strSql.Append(" and PassWord=@PassWord ");//密码
            strSql.Append(" and State<>2 ");//冻结的买家不能登录
            strSql.Append(" and PeriodOfValidity>(getdate()) ");//过期时间
            strSql.Append(" and exists(select UID from memberinfo where UID=memberaccount.UID and Member_Class>=0) ");//买家类别

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserId", DbType.AnsiString, userId);
            db.AddInParameter(dbCommand, "PassWord", DbType.AnsiString, password);
            DataSet ds = db.ExecuteDataSet(dbCommand);
            return GetModelByDataSet(ds);
        }

        /// <summary>
        /// 获取登陆用户名类型
        /// </summary>
        /// <param name="UserId">登陆用户名</param>
        /// <returns></returns>
        public int GetUserIdNameClass(string UserId)
        {
            if (Regex.IsMatch(UserId, "^1\\d{10}$"))
            {
                return 2;
            }
            else if (Regex.IsMatch(UserId, "^[\\w-]+@[\\w-]+\\.(com|cn|net|org|edu|mil|tv|biz|info)$"))
            {
                return 3;
            }
            else
            {
                return 1;
            }
        }
        /// <summary>
        /// 登陆账号查询语句
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public string GetUserIdSqlParam(string UserId)
        {
            int i = GetUserIdNameClass(UserId);
            if (i == 2)
            {
                return "MobilePhone=@UserId";
            }
            else if (i == 3)
            {
                return "Email=@UserId";
            }
            else
            {
                return "UserId=@UserId";
            }
        }
        private Model.MemberAccount GetModelByDataSet(DataSet ds)
        {
            Model.MemberAccount model = null;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model = GetModelByDataRow(ds.Tables[0].Rows[0]);
            }
            return model;
        }
        private Model.MemberAccount GetModelByDataRow(DataRow dr)
        {
            Model.MemberAccount model = new Model.MemberAccount();
            model.UID = int.Parse(dr["UID"].ToString());
            model.UserType = int.Parse(dr["UserType"].ToString());
            model.UserGroup = int.Parse(dr["UserGroup"].ToString());
            model.UserId = dr["UserId"].ToString();
            model.PassWord = dr["PassWord"].ToString();
            model.MobilePhone = dr["MobilePhone"].ToString();
            model.Question = dr["Question"].ToString();
            model.Answer = dr["Answer"].ToString();
            model.Email = dr["Email"].ToString();
            model.Email_QQ = dr["Email_QQ"].ToString();
            model.State = int.Parse(dr["State"].ToString());
            model.RegisterDate = DateTime.Parse(dr["RegisterDate"].ToString());
            model.RegisterIP = dr["RegisterIP"].ToString();
            model.Capital = decimal.Parse(dr["Capital"].ToString());
            model.Coupons = int.Parse(dr["Coupons"].ToString());
            model.Points = int.Parse(dr["Points"].ToString());
            model.PeriodOfValidity = DateTime.Parse(dr["PeriodOfValidity"].ToString());
            model.CompanyClass = dr["CompanyClass"].ToString();
            return model;
        }
        /// <summary>
        /// 查所有 根据条件
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<Model.MemberAccount> GetAll(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from memberaccount  ");
            if (!string.IsNullOrEmpty(where))
            {
                strSql.Append("where " + where);
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            DataSet ds = db.ExecuteDataSet(dbCommand);

            List<Model.MemberAccount> accountList = new List<Model.MemberAccount>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    accountList.Add(GetModelByDataRow(dr));
                }
            }
            return accountList;
        }

        public ChangeHope.DataBase.DataByPage GetList(string where)
        {
            string a = "a.UserType, a.UserGroup, a.UserId, a.MobilePhone, a.Email, a.Email_QQ, a.PassWord, a.Question, a.Answer, a.State, a.RegisterDate, a.RegisterIP, a.Capital, a.Coupons, a.Points, a.PeriodOfValidity, a.CompanyClass ";
            string b = "b.*,c.*";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("[select] " + a + b);
            strSql.Append(" [from] memberaccount a inner join memberinfo b on a.UID=b.UID inner join memberpermission c on a.UID=c.UID [where] 1=1 ");
            if (!string.IsNullOrEmpty(where))
            {
                strSql.Append(where);
            }
            strSql.Append(" [order by] a.UID desc");
            ChangeHope.DataBase.DataByPage dataPage = new ChangeHope.DataBase.DataByPage();
            dataPage.Sql = strSql.ToString();
            dataPage.GetRecordSetByPage();
            return dataPage;
        }
        #endregion  成员方法

        /// <summary>
        /// 15天未登录的用户进行提醒服务
        /// </summary>
        public void WarnLogonAfter15Days()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT (SELECT MobilePhone FROM memberaccount WHERE UID=a.UID),UID,lastloginintime FROM ");
            strSql.Append("(SELECT UID,(SELECT TOP(1) loginintime FROM memberloginlog WHERE UID=a.UID ORDER BY loginintime DESC) AS lastloginintime ");
            strSql.Append("FROM (SELECT DISTINCT UID FROM memberloginlog AS a1 ");
            strSql.Append("WHERE NOT EXISTS(SELECT TOP(1) * FROM memberloginlog WHERE UID=a1.UID AND datediff(day,loginintime,getdate())<=15)) AS a) AS a ");
            strSql.Append("WHERE datediff(day,lastloginintime,getdate())=15 ");
            //strSql.Append("AND datepart(hour,lastloginintime)=datepart(hour,getdate()) ");
            //strSql.Append("AND datepart(minute,lastloginintime)=datepart(minute,getdate()) ");
            DataSet ds = ExecuteDataSet(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int UID = 0;
                string MobilePhone = "";
                string SmsMsg = "亲，您已15天未登录www.101yao.com，若忘记密码，可凭手机获取验证码找回；更多热销新品、劲爆促销立即登录抢购";
                StringBuilder strSql1 = new StringBuilder();
                strSql1.Append("insert into memberloginlog(");
                strSql1.Append("UID, name, nameclass, password, loginintime, loginouttime, loginip, loginregion, hostcomputername, operatenote)");
                strSql1.Append(" values (");
                strSql1.Append("@UID, @name, @nameclass, @password, @loginintime, @loginouttime, @loginip, @loginregion, @hostcomputername, @operatenote)");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    UID = 0; MobilePhone = "";
                    if (!dr.IsNull(0) && dr[0].ToString().Trim() != "")
                    {
                        MobilePhone = dr[0].ToString().Trim();
                        int.TryParse(Convert.ToString(dr[1]), out UID);
                        if (UID > 0 && SOSOshop.BLL.Sms.SendAndSaveDataBase(MobilePhone, SmsMsg, "系统", MobilePhone))
                        {
                            DbCommand dbCommand = db.GetSqlStringCommand(strSql1.ToString());
                            db.AddInParameter(dbCommand, "UID", DbType.Int32, UID);
                            db.AddInParameter(dbCommand, "name", DbType.AnsiString, "");
                            db.AddInParameter(dbCommand, "nameclass", DbType.Int32, 0);
                            db.AddInParameter(dbCommand, "password", DbType.AnsiString, "");
                            DateTime now = DateTime.Now; DateTime.TryParse(Convert.ToString(dr[2]), out now);
                            db.AddInParameter(dbCommand, "loginintime", DbType.DateTime, now);
                            db.AddInParameter(dbCommand, "loginouttime", DbType.DateTime, now);
                            db.AddInParameter(dbCommand, "loginip", DbType.AnsiString, "");
                            db.AddInParameter(dbCommand, "loginregion", DbType.AnsiString, "");
                            db.AddInParameter(dbCommand, "hostcomputername", DbType.AnsiString, "");
                            db.AddInParameter(dbCommand, "operatenote", DbType.AnsiString, "15天未登录");
                            db.ExecuteNonQuery(dbCommand);
                        }
                    }
                }
            }
        }
        ///
        /// 用户信息以及用户邮箱状态
        ///
        public Model.MemberCheck GetAccountAndCenter(int uid, string checkType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Checked,CheckType from membercheck where uid=@UID and CheckType=@checkType");
            SqlParameter[] parameters = {
					new SqlParameter("@UID", SqlDbType.Int,4),
                    new SqlParameter("@checkType",SqlDbType.Char,2)
                                        };
            parameters[0].Value = uid;
            parameters[1].Value = checkType;

            DataTable dt = ChangeHope.DataBase.SQLServerHelper.Query(strSql.ToString(), parameters).Tables[0];
            Model.MemberCheck check = new Model.MemberCheck();
            check.Uid = uid;
            foreach (DataRow row in dt.Rows)
            {
                check.Checked = string.IsNullOrEmpty(row["Checked"].ToString()) == true ? false : Convert.ToBoolean(row["Checked"].ToString());
                check.CheckType = row["CheckType"].ToString();
            }
            return check;
        }

        
        /// <summary>
        /// 取得企业Id
        /// </summary>
        /// <param name="companyName"></param>
        /// <returns></returns>
        public int GetEnterpriseId(string companyName)
        {
            string strWhere = " WHERE 1=1";
            if (!string.IsNullOrEmpty(companyName))
            {
                strWhere += " AND Name LIKE '%" + companyName + "%'";
            }

            string sql = string.Format("SELECT TOP 1 Id,Name FROM DrugsBase_Enterprise ") + strWhere;
            DataTable dt = base.ExecuteTableForCache(sql, DateTime.Now.AddMinutes(5));

            int id = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                int.TryParse(dt.Rows[0]["Id"].ToString().Trim(), out id);
            }
            return id;
        }
    }
}
