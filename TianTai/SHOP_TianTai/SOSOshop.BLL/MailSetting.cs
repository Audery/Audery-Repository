using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace SOSOshop.BLL
{
    /// <summary>
    /// 数据访问类MailSetting。
    /// </summary>
    public class MailSetting : Db
    {
        public MailSetting()
        { }
        #region  成员方法

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(Model.MailSetting model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update yxs_MailSetting set ");
            strSql.Append("SmtpServerIP=@SmtpServerIP,");
            strSql.Append("SmtpServerPort=@SmtpServerPort,");
            strSql.Append("MailId=@MailId,");
            strSql.Append("MailPassword=@MailPassword");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@SmtpServerIP", SqlDbType.VarChar,50),
					new SqlParameter("@SmtpServerPort", SqlDbType.Int,4),
					new SqlParameter("@MailId", SqlDbType.VarChar,50),
					new SqlParameter("@MailPassword", SqlDbType.VarChar,50)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.SmtpServerIP;
            parameters[2].Value = model.SmtpServerPort;
            parameters[3].Value = model.MailId;
            parameters[4].Value = model.MailPassword;

            ChangeHope.DataBase.SQLServerHelper.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.MailSetting GetModel()
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,SmtpServerIP,SmtpServerPort,MailId,MailPassword from yxs_MailSetting ");

            Model.MailSetting model = new Model.MailSetting();
            DataSet ds =ExecuteDataSet(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.SmtpServerIP = ds.Tables[0].Rows[0]["SmtpServerIP"].ToString();
                if (ds.Tables[0].Rows[0]["SmtpServerPort"].ToString() != "")
                {
                    model.SmtpServerPort = int.Parse(ds.Tables[0].Rows[0]["SmtpServerPort"].ToString());
                }
                model.MailId = ds.Tables[0].Rows[0]["MailId"].ToString();
                model.MailPassword = ds.Tables[0].Rows[0]["MailPassword"].ToString();
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
