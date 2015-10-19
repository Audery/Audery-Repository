using System;
namespace SOSOshop.Model.DrugsBase
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
        private int _inctype;
        private int? _buyinctype = 0;
        private int _status = 0;
        private string _taxpayerid;
        private int? _sellfilingstatus = 2;
        private int? _buyfilingstatus = 2;
        private bool _issell = false;
        private bool _isbuy = false;
        private int? _oldid;
        private DateTime? _created = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Code
        {
            set { _code = value; }
            get { return _code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PYJM
        {
            set { _pyjm = value; }
            get { return _pyjm; }
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
        public string Fax
        {
            set { _fax = value; }
            get { return _fax; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShortName
        {
            set { _shortname = value; }
            get { return _shortname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TrueName
        {
            set { _truename = value; }
            get { return _truename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MobilePhone
        {
            set { _mobilephone = value; }
            get { return _mobilephone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OfficePhone
        {
            set { _officephone = value; }
            get { return _officephone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Province
        {
            set { _province = value; }
            get { return _province; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? City
        {
            set { _city = value; }
            get { return _city; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Borough
        {
            set { _borough = value; }
            get { return _borough; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Money
        {
            set { _money = value; }
            get { return _money; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LegalRepresentative
        {
            set { _legalrepresentative = value; }
            get { return _legalrepresentative; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Nature
        {
            set { _nature = value; }
            get { return _nature; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Limits
        {
            set { _limits = value; }
            get { return _limits; }
        }
        /// <summary>
        /// 
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
        /// 状态：0正常，1未审核，2已冻结
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
        /// <summary>
        /// 原ID
        /// </summary>
        public int? oldID
        {
            set { _oldid = value; }
            get { return _oldid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? created
        {
            set { _created = value; }
            get { return _created; }
        }
        #endregion Model

    }
}

