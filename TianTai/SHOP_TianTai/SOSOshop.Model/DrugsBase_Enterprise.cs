using System;
namespace SOSOshop.Model
{
    /// <summary>
    /// 企业库，包括所有企业基本信息
    /// </summary>
    [Serializable]
    public partial class DrugsBase_Enterprise
    {
        public DrugsBase_Enterprise()
        { }
        #region Model
        private int _id;
        private string _code;
        private string _pyjm;
        private string _name;
        private string _fax;
        private string _email;
        private string _shortname;
        private string _truename;
        private string _mobilephone;
        private string _officephone;
        private int? _province;
        private int? _city;
        private int? _borough;
        private string _address;
        private decimal? _money;
        private string _legalrepresentative;
        private int? _nature;
        private string _limits;
        private int _inctype = 0;
        private int? _buyinctype = 0;
        private int _status = 0;
        private string _taxpayerid;
        private int? _sellfilingstatus = 2;
        private int? _buyfilingstatus = 2;
        private bool _issell = false;
        private bool _isbuy = false;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 企业编码
        /// </summary>
        public string Code
        {
            set { _code = value; }
            get { return _code; }
        }
        /// <summary>
        /// 拼音简码
        /// </summary>
        public string PYJM
        {
            set { _pyjm = value; }
            get { return _pyjm; }
        }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax
        {
            set { _fax = value; }
            get { return _fax; }
        }
        /// <summary>
        /// Email
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 企业简称
        /// </summary>
        public string ShortName
        {
            set { _shortname = value; }
            get { return _shortname; }
        }
        /// <summary>
        /// 联系人
        /// </summary>
        public string TrueName
        {
            set { _truename = value; }
            get { return _truename; }
        }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhone
        {
            set { _mobilephone = value; }
            get { return _mobilephone; }
        }
        /// <summary>
        /// 办公电话
        /// </summary>
        public string OfficePhone
        {
            set { _officephone = value; }
            get { return _officephone; }
        }
        /// <summary>
        /// 省份ID
        /// </summary>
        public int? Province
        {
            set { _province = value; }
            get { return _province; }
        }
        /// <summary>
        /// 城市ID
        /// </summary>
        public int? City
        {
            set { _city = value; }
            get { return _city; }
        }
        /// <summary>
        /// 区县ID
        /// </summary>
        public int? Borough
        {
            set { _borough = value; }
            get { return _borough; }
        }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 注册资金
        /// </summary>
        public decimal? Money
        {
            set { _money = value; }
            get { return _money; }
        }
        /// <summary>
        /// 法人
        /// </summary>
        public string LegalRepresentative
        {
            set { _legalrepresentative = value; }
            get { return _legalrepresentative; }
        }
        /// <summary>
        /// 企业性质(1.政府机关/事业单位,2.国营,3.私营等)
        /// </summary>
        public int? Nature
        {
            set { _nature = value; }
            get { return _nature; }
        }
        /// <summary>
        /// 经营范围/生产范围/诊疗范围等描述
        /// </summary>
        public string Limits
        {
            set { _limits = value; }
            get { return _limits; }
        }
        /// <summary>
        /// 供应商 企业类型(1.生产企业,2.经营企业）
        /// </summary>
        public int IncType
        {
            set { _inctype = value; }
            get { return _inctype; }
        }
        /// <summary>
        /// 买家企业类型（3.医疗机构,4.单体药店,5.连锁药店,6.诊所,7.其他)
        /// </summary>
        public int? buyIncType
        {
            set { _buyinctype = value; }
            get { return _buyinctype; }
        }
        /// <summary>
        /// 状态(0正常,1异常)
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 纳税人识别号
        /// </summary>
        public string TaxpayerID
        {
            set { _taxpayerid = value; }
            get { return _taxpayerid; }
        }
        /// <summary>
        /// 卖家(供应商)建档状态 1,资料完备 2资料不完备 3 不完备可交易
        /// </summary>
        public int? SellFilingStatus
        {
            set { _sellfilingstatus = value; }
            get { return _sellfilingstatus; }
        }
        /// <summary>
        /// 买家建档状态 1,资料完备 2资料不完备 3 不完备可交易
        /// </summary>
        public int? BuyFilingStatus
        {
            set { _buyfilingstatus = value; }
            get { return _buyfilingstatus; }
        }
        /// <summary>
        /// 是否卖家
        /// </summary>
        public bool IsSell
        {
            set { _issell = value; }
            get { return _issell; }
        }
        /// <summary>
        /// 是否买家
        /// </summary>
        public bool IsBuy
        {
            set { _isbuy = value; }
            get { return _isbuy; }
        }
        #endregion Model

    }
}

