using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace SOSOshop.MSG
{
    public class Sms
    {
        public SOSOshop.MSG.IMSG msg = null;

        #region 构造函数
        public Sms()
        {
            msg = new SOSOshop.MSG.DZMSG();
            //msg = new SOSOshop.MSG.SHPMSG();
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
        /// 获得企业用户余额
        /// </summary>
        /// <returns></returns>
        public int GetFee()
        {
            return 0;
        }
    }
}
