using System;

namespace SOSOshop.Model
{
    /// <summary>
    /// 实体类AdminLoginLog 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class AdminLoginLog
    {
        public AdminLoginLog()
        { }
        #region Model
        private int _id;
        private string _adminname;
        private string _password;
        private DateTime _loginintime;
        private DateTime? _loginouttime;
        private string _loginip;
        private string _hostcomputername;
        private string _operatenote;
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
        public string AdminName
        {
            set { _adminname = value; }
            get { return _adminname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PassWord
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LoginInTime
        {
            set { _loginintime = value; }
            get { return _loginintime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LoginOutTime
        {
            set { _loginouttime = value; }
            get { return _loginouttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LoginIp
        {
            set { _loginip = value; }
            get { return _loginip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HostComputerName
        {
            set { _hostcomputername = value; }
            get { return _hostcomputername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OperateNote
        {
            set { _operatenote = value; }
            get { return _operatenote; }
        }
        #endregion Model

    }
}
