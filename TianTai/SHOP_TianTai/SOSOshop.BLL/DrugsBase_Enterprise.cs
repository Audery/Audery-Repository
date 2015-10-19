using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using Library.Data;
namespace SOSOshop.BLL
{
    /// <summary>
    /// 企业库，包括所有企业基本信息
    /// </summary>
    public partial class DrugsBase_Enterprise : DbBase
    {
        public DrugsBase_Enterprise() { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from DrugsBase_Enterprise where ID=@ID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ID", DbType.Int32, ID);
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
        public int Add(SOSOshop.Model.DrugsBase_Enterprise model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("if(not exists(select 1 from DrugsBase_Enterprise where Name=@Name)) begin insert into DrugsBase_Enterprise(");
            strSql.Append("Code,PYJM,Name,Fax,Email,ShortName,TrueName,MobilePhone,OfficePhone,Province,City,Borough,Address,Money,LegalRepresentative,Nature,Limits,IncType,buyIncType,Status,TaxpayerID,SellFilingStatus,BuyFilingStatus,IsSell,IsBuy)");
            strSql.Append(" values (");
            strSql.Append("@Code,@PYJM,@Name,@Fax,@Email,@ShortName,@TrueName,@MobilePhone,@OfficePhone,@Province,@City,@Borough,@Address,@Money,@LegalRepresentative,@Nature,@Limits,@IncType,@buyIncType,@Status,@TaxpayerID,@SellFilingStatus,@BuyFilingStatus,@IsSell,@IsBuy)");
            strSql.Append(" SELECT SCOPE_IDENTITY() end");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Code", DbType.AnsiString, model.Code);
            db.AddInParameter(dbCommand, "PYJM", DbType.AnsiString, model.PYJM);
            db.AddInParameter(dbCommand, "Name", DbType.AnsiString, model.Name);
            db.AddInParameter(dbCommand, "Fax", DbType.String, model.Fax);
            db.AddInParameter(dbCommand, "Email", DbType.String, model.Email);
            db.AddInParameter(dbCommand, "ShortName", DbType.AnsiString, model.ShortName);
            db.AddInParameter(dbCommand, "TrueName", DbType.AnsiString, model.TrueName);
            db.AddInParameter(dbCommand, "MobilePhone", DbType.AnsiString, model.MobilePhone);
            db.AddInParameter(dbCommand, "OfficePhone", DbType.AnsiString, model.OfficePhone);
            db.AddInParameter(dbCommand, "Province", DbType.Int32, model.Province);
            db.AddInParameter(dbCommand, "City", DbType.Int32, model.City);
            db.AddInParameter(dbCommand, "Borough", DbType.Int32, model.Borough);
            db.AddInParameter(dbCommand, "Address", DbType.AnsiString, model.Address);
            db.AddInParameter(dbCommand, "Money", DbType.Double, model.Money);
            db.AddInParameter(dbCommand, "LegalRepresentative", DbType.AnsiString, model.LegalRepresentative);
            db.AddInParameter(dbCommand, "Nature", DbType.Int32, model.Nature);
            db.AddInParameter(dbCommand, "Limits", DbType.AnsiString, model.Limits);
            db.AddInParameter(dbCommand, "IncType", DbType.Int32, model.IncType);
            db.AddInParameter(dbCommand, "buyIncType", DbType.Int32, model.buyIncType);
            db.AddInParameter(dbCommand, "Status", DbType.Int32, model.Status);
            db.AddInParameter(dbCommand, "TaxpayerID", DbType.String, model.TaxpayerID);
            db.AddInParameter(dbCommand, "SellFilingStatus", DbType.Int32, model.SellFilingStatus);
            db.AddInParameter(dbCommand, "BuyFilingStatus", DbType.Int32, model.BuyFilingStatus);
            db.AddInParameter(dbCommand, "IsSell", DbType.Boolean, model.IsSell);
            db.AddInParameter(dbCommand, "IsBuy", DbType.Boolean, model.IsBuy);
            int result;
            object obj = db.ExecuteScalar(dbCommand);
            if(obj==null)
            {
                return 0;
            }
            else if (!int.TryParse(obj.ToString(), out result))                
            {
                return 0;
            }           
            return result;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SOSOshop.Model.DrugsBase_Enterprise model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update DrugsBase_Enterprise set ");
                strSql.Append("Code=@Code,");
                strSql.Append("PYJM=@PYJM,");
                strSql.Append("Name=@Name,");
                strSql.Append("Fax=@Fax,");
                strSql.Append("Email=@Email,");
                strSql.Append("ShortName=@ShortName,");
                strSql.Append("TrueName=@TrueName,");
                strSql.Append("MobilePhone=@MobilePhone,");
                strSql.Append("OfficePhone=@OfficePhone,");
                strSql.Append("Province=@Province,");
                strSql.Append("City=@City,");
                strSql.Append("Borough=@Borough,");
                strSql.Append("Address=@Address,");
                strSql.Append("Money=@Money,");
                strSql.Append("LegalRepresentative=@LegalRepresentative,");
                strSql.Append("Nature=@Nature,");
                strSql.Append("Limits=@Limits,");
                strSql.Append("IncType=@IncType,");
                strSql.Append("buyIncType=@buyIncType,");
                strSql.Append("Status=@Status,");
                strSql.Append("TaxpayerID=@TaxpayerID,");
                strSql.Append("SellFilingStatus=@SellFilingStatus,");
                strSql.Append("BuyFilingStatus=@BuyFilingStatus,");
                strSql.Append("IsSell=@IsSell,");
                strSql.Append("IsBuy=@IsBuy");
                strSql.Append(" where ID=@ID ");

                DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
                db.AddInParameter(dbCommand, "ID", DbType.Int32, model.ID);
                db.AddInParameter(dbCommand, "Code", DbType.AnsiString, model.Code);
                db.AddInParameter(dbCommand, "PYJM", DbType.AnsiString, model.PYJM);
                db.AddInParameter(dbCommand, "Name", DbType.AnsiString, model.Name);
                db.AddInParameter(dbCommand, "Fax", DbType.String, model.Fax);
                db.AddInParameter(dbCommand, "Email", DbType.String, model.Email);
                db.AddInParameter(dbCommand, "ShortName", DbType.AnsiString, model.ShortName);
                db.AddInParameter(dbCommand, "TrueName", DbType.AnsiString, model.TrueName);
                db.AddInParameter(dbCommand, "MobilePhone", DbType.AnsiString, model.MobilePhone);
                db.AddInParameter(dbCommand, "OfficePhone", DbType.AnsiString, model.OfficePhone);
                db.AddInParameter(dbCommand, "Province", DbType.Int32, model.Province);
                db.AddInParameter(dbCommand, "City", DbType.Int32, model.City);
                db.AddInParameter(dbCommand, "Borough", DbType.Int32, model.Borough);
                db.AddInParameter(dbCommand, "Address", DbType.AnsiString, model.Address);
                db.AddInParameter(dbCommand, "Money", DbType.Double, model.Money);
                db.AddInParameter(dbCommand, "LegalRepresentative", DbType.AnsiString, model.LegalRepresentative);
                db.AddInParameter(dbCommand, "Nature", DbType.Int32, model.Nature);
                db.AddInParameter(dbCommand, "Limits", DbType.AnsiString, model.Limits);
                db.AddInParameter(dbCommand, "IncType", DbType.Int32, model.IncType);
                db.AddInParameter(dbCommand, "buyIncType", DbType.Int32, model.buyIncType);
                db.AddInParameter(dbCommand, "Status", DbType.Int32, model.Status);
                db.AddInParameter(dbCommand, "TaxpayerID", DbType.String, model.TaxpayerID);
                db.AddInParameter(dbCommand, "SellFilingStatus", DbType.Int32, model.SellFilingStatus);
                db.AddInParameter(dbCommand, "BuyFilingStatus", DbType.Int32, model.BuyFilingStatus);
                db.AddInParameter(dbCommand, "IsSell", DbType.Boolean, model.IsSell);
                db.AddInParameter(dbCommand, "IsBuy", DbType.Boolean, model.IsBuy);

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
            catch (Exception ex)
            {
                SOSOshop.BLL.Logs.Log.LogServiceAdd(ex.Message, 0, "", "Update", ex.ToString(), 2);
                return false;
            }
            
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from DrugsBase_Enterprise ");
            strSql.Append(" where ID=@ID ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ID", DbType.Int32, ID);
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
        /// 得到一个对象实体
        /// </summary>
        public SOSOshop.Model.DrugsBase_Enterprise GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Code,PYJM,Name,Fax,Email,ShortName,TrueName,MobilePhone,OfficePhone,Province,City,Borough,Address,Money,LegalRepresentative,Nature,Limits,IncType,buyIncType,Status,TaxpayerID,SellFilingStatus,BuyFilingStatus,IsSell,IsBuy from DrugsBase_Enterprise ");
            strSql.Append(" where ID=@ID ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ID", DbType.Int32, ID);
            SOSOshop.Model.DrugsBase_Enterprise model = null;
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    model = ReaderBind(dataReader);
                }
            }
            return model;
        }
        public SOSOshop.Model.DrugsBase_Enterprise GetModel(string Code)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Code,PYJM,Name,Fax,Email,ShortName,TrueName,MobilePhone,OfficePhone,Province,City,Borough,Address,Money,LegalRepresentative,Nature,Limits,IncType,buyIncType,Status,TaxpayerID,SellFilingStatus,BuyFilingStatus,IsSell,IsBuy from DrugsBase_Enterprise ");
            strSql.Append(" where Code=@Code ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Code", DbType.String, Code);
            SOSOshop.Model.DrugsBase_Enterprise model = null;
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Code,PYJM,Name,Fax,Email,ShortName,TrueName,MobilePhone,OfficePhone,Province,City,Borough,Address,Money,LegalRepresentative,Nature,Limits,IncType,buyIncType,Status,TaxpayerID,SellFilingStatus,BuyFilingStatus,IsSell,IsBuy ");
            strSql.Append(" FROM DrugsBase_Enterprise ");
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
            strSql.Append(" ID,Code,PYJM,Name,Fax,Email,ShortName,TrueName,MobilePhone,OfficePhone,Province,City,Borough,Address,Money,LegalRepresentative,Nature,Limits,IncType,buyIncType,Status,TaxpayerID,SellFilingStatus,BuyFilingStatus,IsSell,IsBuy ");
            strSql.Append(" FROM DrugsBase_Enterprise ");
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
            strSql.Append("select count(1) FROM DrugsBase_Enterprise ");
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
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from DrugsBase_Enterprise T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            
            DbCommand dbCommand = db.GetStoredProcCommand("UP_GetRecordByPage");
            db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "DrugsBase_Enterprise");
            db.AddInParameter(dbCommand, "fldName", DbType.AnsiString, "ID");
            db.AddInParameter(dbCommand, "PageSize", DbType.Int32, PageSize);
            db.AddInParameter(dbCommand, "PageIndex", DbType.Int32, PageIndex);
            db.AddInParameter(dbCommand, "IsReCount", DbType.Boolean, 0);
            db.AddInParameter(dbCommand, "OrderType", DbType.Boolean, 0);
            db.AddInParameter(dbCommand, "strWhere", DbType.AnsiString, strWhere);
            return db.ExecuteDataSet(dbCommand);
        }*/

        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<SOSOshop.Model.DrugsBase_Enterprise> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Code,PYJM,Name,Fax,Email,ShortName,TrueName,MobilePhone,OfficePhone,Province,City,Borough,Address,Money,LegalRepresentative,Nature,Limits,IncType,buyIncType,Status,TaxpayerID,SellFilingStatus,BuyFilingStatus,IsSell,IsBuy ");
            strSql.Append(" FROM DrugsBase_Enterprise ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<SOSOshop.Model.DrugsBase_Enterprise> list = new List<SOSOshop.Model.DrugsBase_Enterprise>();

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
        public SOSOshop.Model.DrugsBase_Enterprise ReaderBind(IDataReader dataReader)
        {
            SOSOshop.Model.DrugsBase_Enterprise model = new SOSOshop.Model.DrugsBase_Enterprise();
            object ojb;
            ojb = dataReader["ID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ID = (int)ojb;
            }
            model.Code = dataReader["Code"].ToString();
            model.PYJM = dataReader["PYJM"].ToString();
            model.Name = dataReader["Name"].ToString();
            model.Fax = dataReader["Fax"].ToString();
            model.Email = dataReader["Email"].ToString();
            model.ShortName = dataReader["ShortName"].ToString();
            model.TrueName = dataReader["TrueName"].ToString();
            model.MobilePhone = dataReader["MobilePhone"].ToString();
            model.OfficePhone = dataReader["OfficePhone"].ToString();
            ojb = dataReader["Province"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Province = (int)ojb;
            }
            ojb = dataReader["City"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.City = (int)ojb;
            }
            ojb = dataReader["Borough"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Borough = (int)ojb;
            }
            model.Address = dataReader["Address"].ToString();
            ojb = dataReader["Money"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Money = decimal.Parse(ojb.ToString());
            }
            model.LegalRepresentative = dataReader["LegalRepresentative"].ToString();
            ojb = dataReader["Nature"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Nature = (int)ojb;
            }
            model.Limits = dataReader["Limits"].ToString();
            ojb = dataReader["IncType"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IncType = (int)ojb;
            }
            ojb = dataReader["buyIncType"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.buyIncType = (int)ojb;
            }
            ojb = dataReader["Status"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Status = (int)ojb;
            }
            model.TaxpayerID = dataReader["TaxpayerID"].ToString();
            ojb = dataReader["SellFilingStatus"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SellFilingStatus = (int)ojb;
            }
            ojb = dataReader["BuyFilingStatus"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BuyFilingStatus = (int)ojb;
            }
            ojb = dataReader["IsSell"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IsSell = (bool)ojb;
            }
            ojb = dataReader["IsBuy"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IsBuy = (bool)ojb;
            }
            return model;
        }

        #endregion  Method

        public DataTable GetList(int PageSize, int PageIndex, bool order, string orderField, bool like, string whereField, string whereString, out int recordCount, out int pageCount, string extStirng)
        {
            string sql = " 1=1";
            if (extStirng != null)
            {
                sql += extStirng;
            }
            return base.GetList("DrugsBase_Enterprise", sql, PageSize, PageIndex, order, orderField, like, whereField, whereString, out recordCount, out pageCount);
        }
        /// <summary>
        /// 取得受信客户
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="order"></param>
        /// <param name="orderField"></param>
        /// <param name="like"></param>
        /// <param name="whereField"></param>
        /// <param name="whereString"></param>
        /// <param name="recordCount"></param>
        /// <param name="pageCount"></param>
        /// <param name="extStirng"></param>
        /// <returns></returns>
        public DataTable GetListEnterprise_Trust(int PageSize, int PageIndex, bool order, string orderField, bool like, string whereField, string whereString, out int recordCount, out int pageCount, string extStirng)
        {
            string sql = " 1=1";
            if (extStirng != null)
            {
                sql += extStirng;
            }
            return base.GetList("View_Memberinfo_Enterprise_Trust", sql, PageSize, PageIndex, order, orderField, like, whereField, whereString, out recordCount, out pageCount);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string Name)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from DrugsBase_Enterprise where Name=@Name ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Name", DbType.String, Name);
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
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string Name, int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from DrugsBase_Enterprise where Name=@Name and ID<>@ID");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Name", DbType.String, Name);
            db.AddInParameter(dbCommand, "ID", DbType.Int32, ID);
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

        public ChangeHope.DataBase.DataByPage GetListDataByPage(string where)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("[select] * [from] DrugsBase_Enterprise [where] 1=1 ");
            if (where != null && where != "")
            {
                strSql.Append(where);
            }
            strSql.Append(" [order by] ID desc");
            ChangeHope.DataBase.DataByPage dataPage = new ChangeHope.DataBase.DataByPage();
            dataPage.Sql = strSql.ToString();
            dataPage.GetRecordSetByPage();
            return dataPage;
        }

    }
}

