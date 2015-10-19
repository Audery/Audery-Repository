using System;

namespace SOSOshop.Model
{
    /// <summary>
    /// 实体类CustomerSetting 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class CustomerSetting
    {
        public CustomerSetting()
        { }
        #region Model
        private int _sid;
        private string _allowregister;
        private int? _sameemailregister;
        private int? _adminvalidate;
        private int? _emailvalidate;
        private string _emailvalidatecontent;
        private int? _handselcoupons;
        private int? _handselcouponsnumber;
        private DateTime? _handselcouponsbegintime;
        private DateTime? _handselcouponsendtime;
        private string _handselpoint;
        private string _forbiduserid;
        private int? _answervalidate;
        private string _questionfirst;
        private string _answerfirst;
        private string _questionsecond;
        private string _answersecond;
        private string _userdefaultgroup;
        private int? _getpasswordmethod;
        private int? _newuserpont;
        private decimal? _loginpoint;
        private int? _loginisneedcheckcode;
        private int? _allowotherlogin;
        private string _moneytocoupons;
        private string _moneytodate;
        private string _pointtocoupons;
        private string _pointtodate;
        private string _couponsname;
        private string _couponsunits;
        private string _registerrequired;
        private string _registerrequiredoptional;
        /// <summary>
        /// 
        /// </summary>
        public int SID
        {
            set { _sid = value; }
            get { return _sid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AllowRegister
        {
            set { _allowregister = value; }
            get { return _allowregister; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SameEmailRegister
        {
            set { _sameemailregister = value; }
            get { return _sameemailregister; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AdminValidate
        {
            set { _adminvalidate = value; }
            get { return _adminvalidate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? EmailValidate
        {
            set { _emailvalidate = value; }
            get { return _emailvalidate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EmailValidateContent
        {
            set { _emailvalidatecontent = value; }
            get { return _emailvalidatecontent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? HandselCoupons
        {
            set { _handselcoupons = value; }
            get { return _handselcoupons; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? HandselCouponsNumber
        {
            set { _handselcouponsnumber = value; }
            get { return _handselcouponsnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? HandselCouponsBeginTime
        {
            set { _handselcouponsbegintime = value; }
            get { return _handselcouponsbegintime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? HandselCouponsEndTime
        {
            set { _handselcouponsendtime = value; }
            get { return _handselcouponsendtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HandselPoint
        {
            set { _handselpoint = value; }
            get { return _handselpoint; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ForbidUserId
        {
            set { _forbiduserid = value; }
            get { return _forbiduserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AnswerValidate
        {
            set { _answervalidate = value; }
            get { return _answervalidate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string QuestionFirst
        {
            set { _questionfirst = value; }
            get { return _questionfirst; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AnswerFirst
        {
            set { _answerfirst = value; }
            get { return _answerfirst; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string QuestionSecond
        {
            set { _questionsecond = value; }
            get { return _questionsecond; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AnswerSecond
        {
            set { _answersecond = value; }
            get { return _answersecond; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserDefaultGroup
        {
            set { _userdefaultgroup = value; }
            get { return _userdefaultgroup; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? GetPasswordMethod
        {
            set { _getpasswordmethod = value; }
            get { return _getpasswordmethod; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? NewUserPont
        {
            set { _newuserpont = value; }
            get { return _newuserpont; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? LoginPoint
        {
            set { _loginpoint = value; }
            get { return _loginpoint; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? LoginIsNeedCheckCode
        {
            set { _loginisneedcheckcode = value; }
            get { return _loginisneedcheckcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AllowOtherLogin
        {
            set { _allowotherlogin = value; }
            get { return _allowotherlogin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MoneyToCoupons
        {
            set { _moneytocoupons = value; }
            get { return _moneytocoupons; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MoneyToDate
        {
            set { _moneytodate = value; }
            get { return _moneytodate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PointToCoupons
        {
            set { _pointtocoupons = value; }
            get { return _pointtocoupons; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PointToDate
        {
            set { _pointtodate = value; }
            get { return _pointtodate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CouponsName
        {
            set { _couponsname = value; }
            get { return _couponsname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CouponsUnits
        {
            set { _couponsunits = value; }
            get { return _couponsunits; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RegisterRequired
        {
            set { _registerrequired = value; }
            get { return _registerrequired; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RegisterRequiredOptional
        {
            set { _registerrequiredoptional = value; }
            get { return _registerrequiredoptional; }
        }
        #endregion Model

    }
}
