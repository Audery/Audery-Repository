using System;

namespace SOSOshop.Model
{
    /// <summary>
    /// 登陆账号 实体类MemberAccount 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class MemberAccount
    {
        public MemberAccount()
        { }
        #region Model

        private int _uid;
        private int _usertype;
        private int _usergroup;
        private string _userid;
        private string _mobilephone;
        private string _email;
        private string _email_qq;
        private string _password;
        private string _question;
        private string _answer;
        private int _state;
        private DateTime _registerdate;
        private string _registerip;
        private decimal _capital;
        private int _coupons;
        private int _points;
        private DateTime _periodofvalidity;
        private string _companyclass;

        /// <summary>
        /// 会员UID
        /// </summary>
        public int UID
        {
            set { _uid = value; }
            get { return _uid; }
        }
        /// <summary>
        /// 会员类别（0.普通会员,1企业会员）
        /// </summary>
        public int UserType
        {
            set { _usertype = value; }
            get { return _usertype; }
        }
        /// <summary>
        /// 用户群
        /// </summary>
        public int UserGroup
        {
            set { _usergroup = value; }
            get { return _usergroup; }
        }
        /// <summary>
        /// 登陆账号
        /// </summary>
        public string UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 登陆手机号
        /// </summary>
        public string MobilePhone
        {
            set { _mobilephone = value; }
            get { return _mobilephone; }
        }
        /// <summary>
        /// 登陆邮箱
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// QQ邮箱
        /// </summary>
        public string Email_QQ
        {
            get { return _email_qq; }
            set { _email_qq = value; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 保护问题
        /// </summary>
        public string Question
        {
            set { _question = value; }
            get { return _question; }
        }
        /// <summary>
        /// 保护答案
        /// </summary>
        public string Answer
        {
            set { _answer = value; }
            get { return _answer; }
        }
        /// <summary>
        /// 审核状态（0.已审核,1.未审核,2.冻结）
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegisterDate
        {
            set { _registerdate = value; }
            get { return _registerdate; }
        }
        /// <summary>
        /// 注册IP
        /// </summary>
        public string RegisterIP
        {
            set { _registerip = value; }
            get { return _registerip; }
        }
        /// <summary>
        /// 资金
        /// </summary>
        public decimal Capital
        {
            set { _capital = value; }
            get { return _capital; }
        }
        /// <summary>
        /// 优惠券
        /// </summary>
        public int Coupons
        {
            set { _coupons = value; }
            get { return _coupons; }
        }
        /// <summary>
        /// 积分
        /// </summary>
        public int Points
        {
            set { _points = value; }
            get { return _points; }
        }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime PeriodOfValidity
        {
            set { _periodofvalidity = value; }
            get { return _periodofvalidity; }
        }

        public string CompanyClass
        {
            set { _companyclass = value; }
            get { return _companyclass; }
        }
        #endregion Model

    }

    /// <summary>
    /// 企业经纬度坐标
    /// </summary>
    public class EnterpriseLocation
    {
        public EnterpriseLocation() { }

        /// <summary>
        /// 企业Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 企业经度
        /// </summary>
        public decimal Lon { get; set; }

        /// <summary>
        /// 企业纬度
        /// </summary>
        public decimal Lat { get; set; }
    }
}
