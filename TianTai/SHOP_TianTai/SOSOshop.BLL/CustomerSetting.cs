using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace SOSOshop.BLL
{
    /// <summary>
    /// 数据访问类CustomerSetting。
    /// </summary>
    public class CustomerSetting : Db
    {
        public CustomerSetting()
        { }
        #region  成员方法

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.CustomerSetting model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update yxs_CustomerSetting set ");
            strSql.Append("AllowRegister=@AllowRegister,");
            strSql.Append("SameEmailRegister=@SameEmailRegister,");
            strSql.Append("AdminValidate=@AdminValidate,");
            strSql.Append("EmailValidate=@EmailValidate,");
            strSql.Append("EmailValidateContent=@EmailValidateContent,");
            strSql.Append("HandselCoupons=@HandselCoupons,");
            strSql.Append("HandselCouponsNumber=@HandselCouponsNumber,");
            strSql.Append("HandselCouponsBeginTime=@HandselCouponsBeginTime,");
            strSql.Append("HandselCouponsEndTime=@HandselCouponsEndTime,");
            strSql.Append("HandselPoint=@HandselPoint,");
            strSql.Append("ForbidUserId=@ForbidUserId,");
            strSql.Append("AnswerValidate=@AnswerValidate,");
            strSql.Append("QuestionFirst=@QuestionFirst,");
            strSql.Append("AnswerFirst=@AnswerFirst,");
            strSql.Append("QuestionSecond=@QuestionSecond,");
            strSql.Append("AnswerSecond=@AnswerSecond,");
            strSql.Append("UserDefaultGroup=@UserDefaultGroup,");
            strSql.Append("GetPasswordMethod=@GetPasswordMethod,");
            strSql.Append("LoginPoint=@LoginPoint,");
            strSql.Append("LoginIsNeedCheckCode=@LoginIsNeedCheckCode,");
            strSql.Append("AllowOtherLogin=@AllowOtherLogin,");
            strSql.Append("MoneyToCoupons=@MoneyToCoupons,");
            strSql.Append("MoneyToDate=@MoneyToDate,");
            strSql.Append("PointToCoupons=@PointToCoupons,");
            strSql.Append("PointToDate=@PointToDate,");
            strSql.Append("CouponsName=@CouponsName,");
            strSql.Append("CouponsUnits=@CouponsUnits,");
            strSql.Append("RegisterRequired=@RegisterRequired,");
            strSql.Append("RegisterRequiredOptional=@RegisterRequiredOptional");
            strSql.Append(" where SID=@SID ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "SID", DbType.Int32, model.SID);
            db.AddInParameter(dbCommand, "AllowRegister", DbType.AnsiString, model.AllowRegister);
            db.AddInParameter(dbCommand, "SameEmailRegister", DbType.Int32, model.SameEmailRegister);
            db.AddInParameter(dbCommand, "AdminValidate", DbType.Int32, model.AdminValidate);
            db.AddInParameter(dbCommand, "EmailValidate", DbType.Int32, model.EmailValidate);
            db.AddInParameter(dbCommand, "EmailValidateContent", DbType.AnsiString, model.EmailValidateContent);
            db.AddInParameter(dbCommand, "HandselCoupons", DbType.Int32, model.HandselCoupons);
            db.AddInParameter(dbCommand, "HandselCouponsNumber", DbType.Int32, model.HandselCouponsNumber);
            db.AddInParameter(dbCommand, "HandselCouponsBeginTime", DbType.DateTime, model.HandselCouponsBeginTime);
            db.AddInParameter(dbCommand, "HandselCouponsEndTime", DbType.DateTime, model.HandselCouponsEndTime);
            db.AddInParameter(dbCommand, "HandselPoint", DbType.AnsiString, model.HandselPoint);
            db.AddInParameter(dbCommand, "ForbidUserId", DbType.AnsiString, model.ForbidUserId);
            db.AddInParameter(dbCommand, "AnswerValidate", DbType.Int32, model.AnswerValidate);
            db.AddInParameter(dbCommand, "QuestionFirst", DbType.AnsiString, model.QuestionFirst);
            db.AddInParameter(dbCommand, "AnswerFirst", DbType.AnsiString, model.AnswerFirst);
            db.AddInParameter(dbCommand, "QuestionSecond", DbType.AnsiString, model.QuestionSecond);
            db.AddInParameter(dbCommand, "AnswerSecond", DbType.AnsiString, model.AnswerSecond);
            db.AddInParameter(dbCommand, "UserDefaultGroup", DbType.AnsiString, model.UserDefaultGroup);
            db.AddInParameter(dbCommand, "GetPasswordMethod", DbType.Int32, model.GetPasswordMethod);
            db.AddInParameter(dbCommand, "LoginPoint", DbType.Decimal, model.LoginPoint);
            db.AddInParameter(dbCommand, "LoginIsNeedCheckCode", DbType.Int32, model.LoginIsNeedCheckCode);
            db.AddInParameter(dbCommand, "AllowOtherLogin", DbType.Int32, model.AllowOtherLogin);
            db.AddInParameter(dbCommand, "MoneyToCoupons", DbType.AnsiString, model.MoneyToCoupons);
            db.AddInParameter(dbCommand, "MoneyToDate", DbType.AnsiString, model.MoneyToDate);
            db.AddInParameter(dbCommand, "PointToCoupons", DbType.AnsiString, model.PointToCoupons);
            db.AddInParameter(dbCommand, "PointToDate", DbType.AnsiString, model.PointToDate);
            db.AddInParameter(dbCommand, "CouponsName", DbType.AnsiString, model.CouponsName);
            db.AddInParameter(dbCommand, "CouponsUnits", DbType.AnsiString, model.CouponsUnits);
            db.AddInParameter(dbCommand, "RegisterRequired", DbType.AnsiString, model.RegisterRequired);
            db.AddInParameter(dbCommand, "RegisterRequiredOptional", DbType.AnsiString, model.RegisterRequiredOptional);

            return 0 < db.ExecuteNonQuery(dbCommand);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.CustomerSetting GetModel()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 SID,AllowRegister,SameEmailRegister,AdminValidate,EmailValidate,EmailValidateContent,HandselCoupons,HandselCouponsNumber,HandselCouponsBeginTime,HandselCouponsEndTime,HandselPoint,ForbidUserId,AnswerValidate,QuestionFirst,AnswerFirst,QuestionSecond,AnswerSecond,UserDefaultGroup,GetPasswordMethod,LoginPoint,LoginIsNeedCheckCode,AllowOtherLogin,MoneyToCoupons,MoneyToDate,PointToCoupons,PointToDate,CouponsName,CouponsUnits,RegisterRequired,RegisterRequiredOptional from yxs_CustomerSetting ");
            
            Model.CustomerSetting model = new Model.CustomerSetting();
            DataSet ds = ExecuteDataSet(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SID"].ToString() != "")
                {
                    model.SID = int.Parse(ds.Tables[0].Rows[0]["SID"].ToString());
                }
                model.AllowRegister = ds.Tables[0].Rows[0]["AllowRegister"].ToString();
                if (ds.Tables[0].Rows[0]["SameEmailRegister"].ToString() != "")
                {
                    model.SameEmailRegister = int.Parse(ds.Tables[0].Rows[0]["SameEmailRegister"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AdminValidate"].ToString() != "")
                {
                    model.AdminValidate = int.Parse(ds.Tables[0].Rows[0]["AdminValidate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EmailValidate"].ToString() != "")
                {
                    model.EmailValidate = int.Parse(ds.Tables[0].Rows[0]["EmailValidate"].ToString());
                }
                model.EmailValidateContent = ds.Tables[0].Rows[0]["EmailValidateContent"].ToString();
                if (ds.Tables[0].Rows[0]["HandselCoupons"].ToString() != "")
                {
                    model.HandselCoupons = int.Parse(ds.Tables[0].Rows[0]["HandselCoupons"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HandselCouponsNumber"].ToString() != "")
                {
                    model.HandselCouponsNumber = int.Parse(ds.Tables[0].Rows[0]["HandselCouponsNumber"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HandselCouponsBeginTime"].ToString() != "")
                {
                    model.HandselCouponsBeginTime = DateTime.Parse(ds.Tables[0].Rows[0]["HandselCouponsBeginTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HandselCouponsEndTime"].ToString() != "")
                {
                    model.HandselCouponsEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["HandselCouponsEndTime"].ToString());
                }
                model.HandselPoint = ds.Tables[0].Rows[0]["HandselPoint"].ToString();
                model.ForbidUserId = ds.Tables[0].Rows[0]["ForbidUserId"].ToString();
                if (ds.Tables[0].Rows[0]["AnswerValidate"].ToString() != "")
                {
                    model.AnswerValidate = int.Parse(ds.Tables[0].Rows[0]["AnswerValidate"].ToString());
                }
                model.QuestionFirst = ds.Tables[0].Rows[0]["QuestionFirst"].ToString();
                model.AnswerFirst = ds.Tables[0].Rows[0]["AnswerFirst"].ToString();
                model.QuestionSecond = ds.Tables[0].Rows[0]["QuestionSecond"].ToString();
                model.AnswerSecond = ds.Tables[0].Rows[0]["AnswerSecond"].ToString();
                model.UserDefaultGroup = ds.Tables[0].Rows[0]["UserDefaultGroup"].ToString();
                if (ds.Tables[0].Rows[0]["GetPasswordMethod"].ToString() != "")
                {
                    model.GetPasswordMethod = int.Parse(ds.Tables[0].Rows[0]["GetPasswordMethod"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LoginPoint"].ToString() != "")
                {
                    model.LoginPoint = decimal.Parse(ds.Tables[0].Rows[0]["LoginPoint"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LoginIsNeedCheckCode"].ToString() != "")
                {
                    model.LoginIsNeedCheckCode = int.Parse(ds.Tables[0].Rows[0]["LoginIsNeedCheckCode"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AllowOtherLogin"].ToString() != "")
                {
                    model.AllowOtherLogin = int.Parse(ds.Tables[0].Rows[0]["AllowOtherLogin"].ToString());
                }
                model.MoneyToCoupons = ds.Tables[0].Rows[0]["MoneyToCoupons"].ToString();
                model.MoneyToDate = ds.Tables[0].Rows[0]["MoneyToDate"].ToString();
                model.PointToCoupons = ds.Tables[0].Rows[0]["PointToCoupons"].ToString();
                model.PointToDate = ds.Tables[0].Rows[0]["PointToDate"].ToString();
                model.CouponsName = ds.Tables[0].Rows[0]["CouponsName"].ToString();
                model.CouponsUnits = ds.Tables[0].Rows[0]["CouponsUnits"].ToString();
                model.RegisterRequired = ds.Tables[0].Rows[0]["RegisterRequired"].ToString();
                model.RegisterRequiredOptional = ds.Tables[0].Rows[0]["RegisterRequiredOptional"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }

        #endregion  成员方法
    }
}
