using System;

namespace SOSOshop.Model
{
    /// <summary>
    /// 实体类MailSetting 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class MailSetting
    {
        public MailSetting()
        { }
        #region Model
        private int _id;
        private string _smtpserverip;
        private int? _smtpserverport;
        private string _mailid;
        private string _mailpassword;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SmtpServerIP
        {
            set { _smtpserverip = value; }
            get { return _smtpserverip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SmtpServerPort
        {
            set { _smtpserverport = value; }
            get { return _smtpserverport; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MailId
        {
            set { _mailid = value; }
            get { return _mailid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MailPassword
        {
            set { _mailpassword = value; }
            get { return _mailpassword; }
        }
        #endregion Model

    }
}
