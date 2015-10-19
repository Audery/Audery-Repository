using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace SOSOshop.BLL
{
    /// <summary>
    /// 会员库数据操作类
    /// </summary>
    public partial class MemberInfo : Db
    {
        public MemberInfo() { }
        public DataTable GetList(int PageSize, int PageIndex, bool order, string orderField, bool like, string whereField, string whereString, out int recordCount, out int pageCount, string extStirng)
        {
            string sql = " 1=1";
            if (extStirng != null)
            {
                sql += extStirng;
            }
            return base.GetList("View_MemberinfoDrugsBase_Enterprise", sql, PageSize, PageIndex, order, orderField, like, whereField, whereString, out recordCount, out pageCount);
        }

        public ChangeHope.DataBase.DataByPage GetListDataByPage(string where)
        {
            StringBuilder strSql = new StringBuilder();
            string a = "a.UserType, a.UserGroup, a.UserId, a.MobilePhone, a.Email, a.Email_QQ, a.PassWord, a.Question, a.Answer, a.State, a.IsLookPrice, a.RegisterDate, a.RegisterIP, a.Capital, a.Coupons, a.Points, a.PeriodOfValidity, ";
            string b = "b.* ";
            strSql.Append("[select] " + a + b + ",ISNULL(e.Name,'') AS Enterprise [from] memberaccount AS a INNER JOIN memberinfo AS b ON a.UID=b.UID LEFT JOIN DrugsBase_Enterprise AS e ON b.ParentId=e.ID [where] 1=1 ");
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

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int UID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from memberinfo");
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
        public bool Exists(string Code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from memberinfo");
            strSql.Append(" where Code='" + Code + "'");
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
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.MemberInfo model, bool Checked = false)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            strSql1.Append("UID,");
            strSql2.Append("" + model.UID + ",");
            if (model.TrueName != null)
            {
                strSql1.Append("TrueName,");
                strSql2.Append("'" + model.TrueName + "',");
            }
            if (model.Signed != null)
            {
                strSql1.Append("Signed,");
                strSql2.Append("'" + model.Signed + "',");
            }
            if (model.Photo != null)
            {
                strSql1.Append("Photo,");
                strSql2.Append("'" + model.Photo + "',");
            }
            if (model.Birthday != null)
            {
                strSql1.Append("Birthday,");
                strSql2.Append("'" + model.Birthday + "',");
            }
            if (model.PapersType != null)
            {
                strSql1.Append("PapersType,");
                strSql2.Append("'" + model.PapersType + "',");
            }
            if (model.PapersNumber != null)
            {
                strSql1.Append("PapersNumber,");
                strSql2.Append("'" + model.PapersNumber + "',");
            }
            if (model.Province != null)
            {
                strSql1.Append("Province,");
                strSql2.Append("" + model.Province + ",");
            }
            if (model.City != null)
            {
                strSql1.Append("City,");
                strSql2.Append("" + model.City + ",");
            }
            if (model.Borough != null)
            {
                strSql1.Append("Borough,");
                strSql2.Append("" + model.Borough + ",");
            }
            if (model.Address != null)
            {
                strSql1.Append("Address,");
                strSql2.Append("'" + model.Address + "',");
            }
            if (model.OfficePhone != null)
            {
                strSql1.Append("OfficePhone,");
                strSql2.Append("'" + model.OfficePhone + "',");
            }
            if (model.HomePhone != null)
            {
                strSql1.Append("HomePhone,");
                strSql2.Append("'" + model.HomePhone + "',");
            }
            if (model.HandPhone != null)
            {
                strSql1.Append("HandPhone,");
                strSql2.Append("'" + model.HandPhone + "',");
            }
            if (model.Fax != null)
            {
                strSql1.Append("Fax,");
                strSql2.Append("'" + model.Fax + "',");
            }
            if (model.PersonWebSite != null)
            {
                strSql1.Append("PersonWebSite,");
                strSql2.Append("'" + model.PersonWebSite + "',");
            }
            if (model.QQ != null)
            {
                strSql1.Append("QQ,");
                strSql2.Append("'" + model.QQ + "',");
            }
            if (model.ParentId > 0)
            {
                strSql1.Append("ParentId,");
                strSql2.Append("" + model.ParentId + ",");
            }
            if (model.Parents != null)
            {
                strSql1.Append("Parents,");
                strSql2.Append("'" + model.Parents + "',");
            }
            if (model.CRMID != null)
            {
                strSql1.Append("CRMID,");
                strSql2.Append("'" + model.CRMID + "',");
            }
            if (model.Member_Type != null)
            {
                strSql1.Append("Member_Type,");
                strSql2.Append("" + model.Member_Type + ",");
            }
            if (model.Member_Class != null)
            {
                strSql1.Append("Member_Class,");
                strSql2.Append("" + model.Member_Class + ",");
            }
            if (model.Editer != 0)
            {
                strSql1.Append("Editer,");
                strSql2.Append("" + model.Editer + ",");
            }
            if (model.OSPId != 0)
            {
                strSql1.Append("OSPId,");
                strSql2.Append("" + model.OSPId + ",");
            }
            if (model.Code != null)
            {
                strSql1.Append("Code,");
                strSql2.Append("'" + model.Code + "',");
            }
            if (model.PriceCategory != null)
            {
                strSql1.Append("PriceCategory,");
                strSql2.Append("'" + model.PriceCategory + "',");
            }

            strSql.Append("insert into memberinfo(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            if (db.ExecuteNonQuery(dbCommand) <= 0)
            {
                BLL.MemberAccount abll = new BLL.MemberAccount();
                abll.Delete(model.UID);
                return false;
            }
            return true;
        }

        public static decimal GetDiscount(int uid)
        {
            if (uid > 0)
            {
                DbBase db = new DbBase();
                decimal discount = 0;
                object obj = db.ExecuteScalar("select discount from memberinfo where uid=" + uid);
                if (obj != null)
                {
                    discount = (decimal)obj;
                }
                return discount;
            }
            return 1.0M;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.MemberInfo model, bool Checked = false)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update memberinfo set ");
            if (model.TrueName != null)
            {
                strSql.Append("TrueName='" + model.TrueName + "',");
            }
            if (model.Signed != null)
            {
                strSql.Append("Signed='" + model.Signed + "',");
            }
            if (model.Photo != null)
            {
                strSql.Append("Photo='" + model.Photo + "',");
            }
            if (model.Birthday != null)
            {
                strSql.Append("Birthday='" + model.Birthday + "',");
            }
            if (model.PapersType != null)
            {
                strSql.Append("PapersType='" + model.PapersType + "',");
            }
            if (model.PapersNumber != null)
            {
                strSql.Append("PapersNumber='" + model.PapersNumber + "',");
            }
            if (model.Province != null)
            {
                strSql.Append("Province=" + model.Province + ",");
            }
            if (model.City != null)
            {
                strSql.Append("City=" + model.City + ",");
            }
            if (model.Borough != null)
            {
                strSql.Append("Borough=" + model.Borough + ",");
            }
            if (model.Address != null)
            {
                strSql.Append("Address='" + model.Address + "',");
            }
            if (model.OfficePhone != null)
            {
                strSql.Append("OfficePhone='" + model.OfficePhone + "',");
            }
            if (model.HomePhone != null)
            {
                strSql.Append("HomePhone='" + model.HomePhone + "',");
            }
            if (model.HandPhone != null)
            {
                strSql.Append("HandPhone='" + model.HandPhone + "',");
            }
            if (model.Fax != null)
            {
                strSql.Append("Fax='" + model.Fax + "',");
            }
            if (model.PersonWebSite != null)
            {
                strSql.Append("PersonWebSite='" + model.PersonWebSite + "',");
            }
            if (model.QQ != null)
            {
                strSql.Append("QQ='" + model.QQ + "',");
            }
            if (model.ParentId != null)
            {
                strSql.Append("ParentId=" + model.ParentId + ",");
            }
            if (model.Parents != null)
            {
                strSql.Append("Parents='" + model.Parents + "',");
            }
            if (model.CRMID != null)
            {
                strSql.Append("CRMID=" + model.CRMID + ",");
            }
            if (model.Member_Type != null)
            {
                strSql.Append("Member_Type=" + model.Member_Type + ",");
            }
            if (model.Member_Class != null)
            {
                strSql.Append("Member_Class=" + model.Member_Class + ",");
            }
            if (model.Editer != 0)
            {
                strSql.Append("Editer=" + model.Editer + ",");
            }
            if (model.OSPId != 0)
            {
                strSql.Append("OSPId=" + model.OSPId + ",");
            }
            if (model.Code != null)
            {
                strSql.Append("Code='" + model.Code + "',");
            }
            if (model.PriceCategory != null)
            {
                strSql.Append("PriceCategory='" + model.PriceCategory + "',");
            }
            strSql.Append("discount=" + model.discount + ",");
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where UID=" + model.UID + " ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            Model.MemberInfo oldmodel = GetModel(model.UID);
            db.ExecuteNonQuery(dbCommand);
            return true;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int UID)
        {
            return (new MemberAccount().Delete(UID));
        }

        private Model.MemberInfo GetModelByDataSet(DataSet ds)
        {
            Model.MemberInfo model = null;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model = GetModelByDataRow(ds.Tables[0].Rows[0]);
            }
            return model;
        }
        private Model.MemberInfo GetModelByDataRow(DataRow dr)
        {
            Model.MemberInfo model = new Model.MemberInfo();
            model.UID = int.Parse(dr["UID"].ToString());
            model.Code = dr["Code"].ToString();
            model.TrueName = dr["TrueName"].ToString();
            model.Signed = dr["Signed"].ToString();
            model.Photo = dr["Photo"].ToString();
            if (Convert.ToString(dr["Birthday"]) != string.Empty)
                model.Birthday = DateTime.Parse(Convert.ToString(dr["Birthday"]));
            model.PapersType = int.Parse(dr["PapersType"].ToString());
            model.PapersNumber = dr["PapersNumber"].ToString();
            model.Province = int.Parse(dr["Province"].ToString());
            model.City = int.Parse(dr["City"].ToString());
            model.Borough = int.Parse(dr["Borough"].ToString());
            model.Address = dr["Address"].ToString();
            model.OfficePhone = dr["OfficePhone"].ToString();
            model.HomePhone = dr["HomePhone"].ToString();
            model.HandPhone = dr["HandPhone"].ToString();
            model.Fax = dr["Fax"].ToString();
            model.PersonWebSite = dr["PersonWebSite"].ToString();
            model.QQ = dr["QQ"].ToString();
            model.ParentId = int.Parse(dr["ParentId"].ToString());
            model.Parents = dr["Parents"].ToString();
            model.CRMID = int.Parse(dr["CRMID"].ToString());
            model.Member_Type = int.Parse(dr["Member_Type"].ToString());
            model.Member_Class = int.Parse(dr["Member_Class"].ToString());
            model.Editer = int.Parse(dr["Editer"].ToString());
            model.OSPId = int.Parse(dr["OSPId"].ToString());
            model.discount = (decimal)dr["discount"];
            model.PriceCategory = dr["PriceCategory"] as string;
            return model;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.MemberInfo GetModel(int UID)
        {
            string strSql = " select top 1 * from memberinfo where UID=" + UID;
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            DataSet ds = db.ExecuteDataSet(dbCommand);
            return GetModelByDataSet(ds);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.MemberInfo GetModel(string Code)
        {
            string strSql = " select top 1 * from memberinfo where Code='" + Code + "'";
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            DataSet ds = db.ExecuteDataSet(dbCommand);
            return GetModelByDataSet(ds);
        }
        /// <summary>
        /// 获得用户姓名
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public string GetUserName(int UID)
        {
            string strSql = " select TrueName from memberinfo where UID=" + UID;
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            DataSet ds = db.ExecuteDataSet(dbCommand);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            return null;
        }
        /// <summary>
        /// 取得客户类型
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public Member_Class GetMember_Class(int UID)
        {
            Member_Class m = (Member_Class)ExecuteScalarForCache("SELECT TOP 1 Member_Class FROM dbo.memberinfo  WHERE UID=" + UID, DateTime.Now.AddMinutes(3));
            return m;
        }
    }
    /// <summary>
    /// 0.批发客户,1.OTC拆零客户, -1未知【刚注册的】
    /// </summary>
    public enum Member_Class
    {
        批发客户 = 0, OTC客户 = 1, 未知 = -1
    }
}
