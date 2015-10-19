using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using SOSOshop.BLL;

namespace _101shop.Common
{
    public class Sms
    {
        public SOSOshop.MSG.IMSG msg = null;

        #region 构造函数
        public Sms()
        {
            string config = System.Configuration.ConfigurationManager.AppSettings["Sms"];
            if (string.IsNullOrEmpty(config) || config == "SHPMSG")
            {
                msg = new SOSOshop.MSG.SHPMSG();
            }
            else if (config == "TMSG")
            {
                msg = new SOSOshop.MSG.TMSG();
            }
            else if (config == "YMSG")
            {
                msg = new SOSOshop.MSG.YMSG();
            }
        }
        #endregion

        /// <summary>
        /// 发送Sms消息给指定的手机
        /// </summary>
        /// <param name="DstMobile">手机号码</param>
        /// <param name="SmsMsg">Sms消息</param>
        /// <param name="from">发件人</param>
        /// <param name="to">收件人</param>
        /// <returns></returns>
        public bool Send(string mobile, string content)
        {
            bool Success = false;
            if (msg != null)
            {
                Success = msg.Send(mobile, content);
            }
            return Success;
        }
        /// <summary>
        /// 发送Sms消息给指定的手机,成功后保存到数据库
        /// </summary>
        /// <param name="DstMobile">手机号码</param>
        /// <param name="SmsMsg">Sms消息</param>
        /// <param name="from">发件人</param>
        /// <param name="to">收件人</param>
        /// <returns></returns>
        public bool SaveDataBase(string DstMobile, string SmsMsg, string from, string to, bool Success)
        {
            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                int rows_affected = 0;
                string sqlString = "INSERT INTO yxs_Sms(fromUID,toUID,Mobile,Msg,OperateTime,State) "
                    + "VALUES(@fromUID,@toUID,@Mobile,@Msg,@OperateTime,@State)";
                SqlCommand cmd = new SqlCommand(sqlString, connection);
                SqlParameter[] paras = new SqlParameter[6];
                paras[0] = new SqlParameter("@fromUID", SqlDbType.VarChar, 200);
                paras[0].Value = from.Substring(0, Math.Min(200, from.Length));
                paras[1] = new SqlParameter("@toUID", SqlDbType.VarChar, 200);
                paras[1].Value = to.Substring(0, Math.Min(200, to.Length));
                paras[2] = new SqlParameter("@Mobile", SqlDbType.VarChar, 200);
                paras[2].Value = DstMobile.Substring(0, Math.Min(200, DstMobile.Length));
                paras[3] = new SqlParameter("@Msg", SqlDbType.VarChar, 500);
                paras[3].Value = SmsMsg.Substring(0, Math.Min(200, SmsMsg.Length));
                paras[4] = new SqlParameter("@OperateTime", SqlDbType.DateTime);
                paras[4].Value = DateTime.Now;
                paras[5] = new SqlParameter("@State", SqlDbType.Int, 4);
                paras[5].Value = Success ? 1 : 0;
                foreach (SqlParameter parm in paras) cmd.Parameters.Add(parm);
                try
                {
                    connection.Open();
                    rows_affected = cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    connection.Close();
                    throw new Exception(e.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
                return (rows_affected > 0);
            }
        }
        public bool UpdateDataBase(int ID, string DstMobile, string SmsMsg, string from, string to, bool Success)
        {
            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                int rows_affected = 0;
                string sqlString = "UPDATE yxs_Sms SET fromUID=@fromUID,toUID=@toUID,Mobile=@Mobile,Msg=@Msg,OperateTime=@OperateTime,State=@State "
                    + " WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(sqlString, connection);
                SqlParameter[] paras = new SqlParameter[7];
                paras[0] = new SqlParameter("@fromUID", SqlDbType.VarChar, 200);
                paras[0].Value = from.Substring(0, Math.Min(200, from.Length));
                paras[1] = new SqlParameter("@toUID", SqlDbType.VarChar, 200);
                paras[1].Value = to.Substring(0, Math.Min(200, to.Length));
                paras[2] = new SqlParameter("@Mobile", SqlDbType.VarChar, 200);
                paras[2].Value = DstMobile.Substring(0, Math.Min(200, DstMobile.Length));
                paras[3] = new SqlParameter("@Msg", SqlDbType.VarChar, 500);
                paras[3].Value = SmsMsg.Substring(0, Math.Min(200, SmsMsg.Length));
                paras[4] = new SqlParameter("@OperateTime", SqlDbType.DateTime);
                paras[4].Value = DateTime.Now;
                paras[5] = new SqlParameter("@State", SqlDbType.Int, 4);
                paras[5].Value = Success ? 1 : 0;
                paras[6] = new SqlParameter("@Id", SqlDbType.Int, 8);
                paras[6].Value = ID;
                foreach (SqlParameter parm in paras) cmd.Parameters.Add(parm);
                try
                {
                    connection.Open();
                    rows_affected = cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    connection.Close();
                    throw new Exception(e.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
                return (rows_affected > 0);
            }
        }

        /// <summary>
        /// 获得企业用户余额
        /// </summary>
        /// <returns></returns>
        public int GetFee()
        {
            return 0;
        }
    }
}
