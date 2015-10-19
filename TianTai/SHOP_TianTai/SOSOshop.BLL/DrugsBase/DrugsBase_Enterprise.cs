using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;

namespace SOSOshop.BLL.DrugsBase
{
    /// <summary>
    /// 数据访问类:DrugsBase_Enterprise
    /// </summary>
    public partial class DrugsBase_Enterprise : DbBase
    {
        public DrugsBase_Enterprise()
        { }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string Code)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from DrugsBase_Enterprise where Code=@Code ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Code", DbType.String, Code);
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
        public int Add(SOSOshop.Model.DrugsBase.DrugsBase_Enterprise model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into DrugsBase_Enterprise(");
            strSql.Append("Code,PYJM,Name,Fax,Email,ShortName,TrueName,MobilePhone,OfficePhone,Province,City,Borough,Address,Money,LegalRepresentative,Nature,Limits,IncType,buyIncType,Status,TaxpayerID,SellFilingStatus,BuyFilingStatus,IsSell,IsBuy,oldID,created)");

            strSql.Append(" values (");
            strSql.Append("@Code,@PYJM,@Name,@Fax,@Email,@ShortName,@TrueName,@MobilePhone,@OfficePhone,@Province,@City,@Borough,@Address,@Money,@LegalRepresentative,@Nature,@Limits,@IncType,@buyIncType,@Status,@TaxpayerID,@SellFilingStatus,@BuyFilingStatus,@IsSell,@IsBuy,@oldID,@created)");
            strSql.Append(";select @@IDENTITY");
            Database db = DatabaseFactory.CreateDatabase();
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
            db.AddInParameter(dbCommand, "oldID", DbType.Int32, model.oldID);
            db.AddInParameter(dbCommand, "created", DbType.DateTime, model.created);
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
        public bool Update(SOSOshop.Model.DrugsBase.DrugsBase_Enterprise model)
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
            strSql.Append("IsBuy=@IsBuy,");
            strSql.Append("oldID=@oldID,");
            strSql.Append("created=@created");
            strSql.Append(" where ID=@ID ");
            Database db = DatabaseFactory.CreateDatabase();
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
            db.AddInParameter(dbCommand, "oldID", DbType.Int32, model.oldID);
            db.AddInParameter(dbCommand, "created", DbType.DateTime, model.created);
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
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from DrugsBase_Enterprise ");
            strSql.Append(" where ID=@ID ");
            Database db = DatabaseFactory.CreateDatabase();
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
    }
}