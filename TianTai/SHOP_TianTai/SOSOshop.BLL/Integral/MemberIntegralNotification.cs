using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace SOSOshop.BLL.Integral
{
    /// <summary>
    /// 会员积分短信通知
    /// </summary>
    public class MemberIntegralNotification : DbBase
    {
        public MemberIntegralNotification()
        {
            base.ChangeShop();
        }

        /// <summary>
        /// 短信通知所有有积分的会员 [通知时间：每个月1号中午12点] [包含日志记录]
        /// </summary>
        /// <returns></returns>
        public int SendSms()
        {
            int i = 0;
            DateTime now = DateTime.Now;
            if ((now.Hour == 12 || now.Hour == 13) && null == ExecuteScalar("SELECT Id FROM yxs_Sms WHERE fromUID='积分系统' AND DATEPART(YEAR,OperateTime)=DATEPART(YEAR,GETDATE()) AND DATEPART(MONTH,OperateTime)=DATEPART(MONTH,GETDATE())"))
            {
                string MsgFormat = "尊敬的{0}，您有{1}积分，请及时兑换您需要的礼物。";
                string sql = "SELECT DISTINCT b.TrueName, a.MobilePhone, c.realityIntegral FROM memberaccount a INNER JOIN memberinfo b ON a.UID=b.UID INNER JOIN MemberIntegral c ON a.UID=c.uid WHERE a.State=0 AND c.realityIntegral>0 ORDER BY c.realityIntegral DESC";
                DataSet ds = ExecuteDataSet(sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string TrueName = Convert.ToString(dr["TrueName"]).Trim();
                        string MobilePhone = Convert.ToString(dr["MobilePhone"]).Trim();
                        string SmsMsg = string.Format(MsgFormat, TrueName, dr["realityIntegral"]);
                        string from = "积分系统";
                        string to = MobilePhone;
                        if (Sms.SendAndSaveDataBase(MobilePhone, SmsMsg, from, to))
                        {
                            i++;
                            //通知间隔时间
                            //System.Threading.Thread.Sleep(2000);
                        }
                    }
                    #region 日志记录
                    SOSOshop.BLL.Logs.Log.LogServiceAdd("[积分系统]短信通知所有有积分的会员共" + i + "个", 0, "", "", "", 0);
                    #endregion
                }
            }
            return i;
        }
    }
}
