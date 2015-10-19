using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace SOSOshop.BLL
{
    /// <summary>
    /// 数据访问类WebSetting。
    /// </summary>
    public class WebSetting : Db
    {
        public WebSetting()
        { }
        #region  成员方法
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.WebSetting model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update yxs_websetting set ");
            strSql.Append("websitetitle=@websitetitle,");
            strSql.Append("tel=@tel,");
            strSql.Append("fax=@fax,");
            strSql.Append("email=@email,");
            strSql.Append("websitepath=@websitepath,");
            strSql.Append("logopath=@logopath,");
            strSql.Append("bannerpath=@bannerpath,");
            strSql.Append("copyright=@copyright,");
            strSql.Append("metekey=@metekey,");
            strSql.Append("meteinfo=@meteinfo,");
            strSql.Append("publicmethod=@publicmethod,");
            strSql.Append("closewebsite=@closewebsite,");
            strSql.Append("closewebsiteinfo=@closewebsiteinfo,");
            strSql.Append("closebbs=@closebbs,");
            strSql.Append("closebbsinfo=@closebbsinfo,");
            strSql.Append("websitename=@websitename,");
            strSql.Append("closeshop=@closeshop,");
            strSql.Append("closestation=@closestation,");
            strSql.Append("websitedomain=@websitedomain,");
            strSql.Append("usersagreement=@usersagreement,");
            strSql.Append("loginmothod=@loginmothod,");
            strSql.Append("staticpagefiletype=@staticpagefiletype,");
            strSql.Append("tmplatepath=@tmplatepath,");
            strSql.Append("statisticscode=@statisticscode,");
            strSql.Append("filesize=@filesize");
            strSql.Append(" where id=@id ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "id", DbType.Int32, model.Id);
            db.AddInParameter(dbCommand, "websitetitle", DbType.AnsiString, model.WebSiteTitle);
            db.AddInParameter(dbCommand, "tel", DbType.AnsiString, model.Tel);
            db.AddInParameter(dbCommand, "fax", DbType.AnsiString, model.Fax);
            db.AddInParameter(dbCommand, "email", DbType.AnsiString, model.Email);
            db.AddInParameter(dbCommand, "websitepath", DbType.AnsiString, model.WebSitePath);
            db.AddInParameter(dbCommand, "logopath", DbType.AnsiString, model.LogoPath);
            db.AddInParameter(dbCommand, "bannerpath", DbType.AnsiString, model.BannerPath);
            db.AddInParameter(dbCommand, "copyright", DbType.AnsiString, model.CopyRight);
            db.AddInParameter(dbCommand, "metekey", DbType.AnsiString, model.MeteKey);
            db.AddInParameter(dbCommand, "meteinfo", DbType.AnsiString, model.MeteInfo);
            db.AddInParameter(dbCommand, "publicmethod", DbType.Int32, model.PublicMethod);
            db.AddInParameter(dbCommand, "closewebsite", DbType.Int32, model.CloseWebSite);
            db.AddInParameter(dbCommand, "closewebsiteinfo", DbType.AnsiString, model.CloseWebSiteInfo);
            db.AddInParameter(dbCommand, "closebbs", DbType.Int32, model.CloseBBS);
            db.AddInParameter(dbCommand, "closebbsinfo", DbType.AnsiString, model.CloseBBSInfo);
            db.AddInParameter(dbCommand, "websitename", DbType.AnsiString, model.WebSiteName);
            db.AddInParameter(dbCommand, "closeshop", DbType.Int32, model.CloseShop);
            db.AddInParameter(dbCommand, "closestation", DbType.Int32, model.CloseStation);
            db.AddInParameter(dbCommand, "websitedomain", DbType.AnsiString, model.WebSiteDomain);
            db.AddInParameter(dbCommand, "usersagreement", DbType.AnsiString, model.UsersAgreement);
            db.AddInParameter(dbCommand, "loginmothod", DbType.Int32, model.LoginMothod);
            db.AddInParameter(dbCommand, "staticpagefiletype", DbType.AnsiString, model.StaticPageFileType);
            db.AddInParameter(dbCommand, "tmplatepath", DbType.AnsiString, model.TmplatePath);
            db.AddInParameter(dbCommand, "statisticscode", DbType.AnsiString, model.Statisticscode);
            db.AddInParameter(dbCommand, "filesize", DbType.Int32, model.Filesize);

            return 0 < db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.WebSetting GetModel()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,websitetitle,tel,fax,email,websitepath,logopath,bannerpath,copyright,metekey,meteinfo,publicmethod,closewebsite,closewebsiteinfo,closebbs,closebbsinfo,websitename,closeshop,closestation,websitedomain,usersagreement,loginmothod,staticpagefiletype,tmplatepath,statisticscode,filesize from yxs_websetting ");

            Model.WebSetting model = new Model.WebSetting();
            DataSet ds = ExecuteDataSet(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.Id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                model.WebSiteTitle = ds.Tables[0].Rows[0]["websitetitle"].ToString();
                model.Tel = ds.Tables[0].Rows[0]["tel"].ToString();
                model.Fax = ds.Tables[0].Rows[0]["fax"].ToString();
                model.Email = ds.Tables[0].Rows[0]["email"].ToString();
                model.WebSitePath = ds.Tables[0].Rows[0]["websitepath"].ToString();
                model.LogoPath = ds.Tables[0].Rows[0]["logopath"].ToString();
                model.BannerPath = ds.Tables[0].Rows[0]["bannerpath"].ToString();
                model.CopyRight = ds.Tables[0].Rows[0]["copyright"].ToString();
                model.MeteKey = ds.Tables[0].Rows[0]["metekey"].ToString();
                model.MeteInfo = ds.Tables[0].Rows[0]["meteinfo"].ToString();
                model.Statisticscode = ds.Tables[0].Rows[0]["statisticscode"].ToString();
                if (ds.Tables[0].Rows[0]["filesize"].ToString() != "")
                {
                    model.Filesize = int.Parse(ds.Tables[0].Rows[0]["filesize"].ToString());
                }
                if (ds.Tables[0].Rows[0]["publicmethod"].ToString() != "")
                {
                    model.PublicMethod = int.Parse(ds.Tables[0].Rows[0]["publicmethod"].ToString());
                }
                if (ds.Tables[0].Rows[0]["closewebsite"].ToString() != "")
                {
                    model.CloseWebSite = int.Parse(ds.Tables[0].Rows[0]["closewebsite"].ToString());
                }
                model.CloseWebSiteInfo = ds.Tables[0].Rows[0]["closewebsiteinfo"].ToString();
                if (ds.Tables[0].Rows[0]["closebbs"].ToString() != "")
                {
                    model.CloseBBS = int.Parse(ds.Tables[0].Rows[0]["closebbs"].ToString());
                }
                model.CloseBBSInfo = ds.Tables[0].Rows[0]["closebbsinfo"].ToString();
                model.WebSiteName = ds.Tables[0].Rows[0]["websitename"].ToString();
                if (ds.Tables[0].Rows[0]["closeshop"].ToString() != "")
                {
                    model.CloseShop = int.Parse(ds.Tables[0].Rows[0]["closeshop"].ToString());
                }
                if (ds.Tables[0].Rows[0]["closestation"].ToString() != "")
                {
                    model.CloseStation = int.Parse(ds.Tables[0].Rows[0]["closestation"].ToString());
                }
                model.WebSiteDomain = ds.Tables[0].Rows[0]["websitedomain"].ToString();
                model.UsersAgreement = ds.Tables[0].Rows[0]["usersagreement"].ToString();
                if (ds.Tables[0].Rows[0]["loginmothod"].ToString() != "")
                {
                    model.LoginMothod = int.Parse(ds.Tables[0].Rows[0]["loginmothod"].ToString());
                }
                model.StaticPageFileType = ds.Tables[0].Rows[0]["staticpagefiletype"].ToString();
                model.TmplatePath = ds.Tables[0].Rows[0]["tmplatepath"].ToString();
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
