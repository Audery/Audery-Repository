using System;

namespace SOSOshop.Model
{
    /// <summary>
    /// 实体类Administrators 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Administrators
    {
        public Administrators()
        { }
        #region Model
        private int _adminid;
        private string _name;
        private string _password;
        private int? _state;
        private DateTime? _managebegintime;
        private DateTime? _manageendtime;
        private int? _power;
        private int? _allowmodifypassword;
        private string _role;
        private string _officePhone;
        private string _homePhone;
        private string _mobilePhone;
        private string _loginAuthenticationOfficePhone;
        private string _qq;
        /// <summary>
        /// 
        /// </summary>
        public int AdminId
        {
            set { _adminid = value; }
            get { return _adminid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
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
        public int? State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ManageBeginTime
        {
            set { _managebegintime = value; }
            get { return _managebegintime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ManageEndTime
        {
            set { _manageendtime = value; }
            get { return _manageendtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Power
        {
            set { _power = value; }
            get { return _power; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AllowModifyPassWord
        {
            set { _allowmodifypassword = value; }
            get { return _allowmodifypassword; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Role
        {
            set { _role = value; }
            get { return _role; }
        }
        /// <summary>
        /// 公司办公电话
        /// </summary>
        public string OfficePhone 
        {
            set { _officePhone = value; }
            get { return _officePhone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HomePhone
        {
            set { _homePhone = value; }
            get { return _homePhone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MobilePhone
        {
            set { _mobilePhone = value; }
            get { return _mobilePhone; }
        }
        /// <summary>
        /// 登录验证电话
        /// </summary>
        public string LoginAuthenticationOfficePhone
        {
            set { _loginAuthenticationOfficePhone = value; }
            get { return _loginAuthenticationOfficePhone; }
        }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ 
        {
            set { _qq = value;}
            get { return _qq; }
        }
        #endregion Model

    }
}
