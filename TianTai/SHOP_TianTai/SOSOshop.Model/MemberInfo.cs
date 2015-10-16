using System;

namespace SOSOshop.Model
{
    /// <summary>
    /// 会员信息 实体类MemberInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class MemberInfo
    {
        public MemberInfo()
        { }
        #region Model
        private int _uid;
        private string _code;
        private string _truename;
        private string _signed;
        private string _photo;
        private DateTime? _birthday;
        private int _paperstype;
        private string _papersnumber;
        private int _province;
        private int _city;
        private int _borough;
        private string _address;
        private string _officephone;
        private string _homephone;
        private string _mobilephone;
        private string _handphone;
        private string _fax;
        private string _personwebsite;
        private string _qq;
        private int _parentid;
        private string _parents;
        private int _CRMID;
        private int _member_type;
        private int _member_class = -1;

        /// <summary>
        /// 会员UID
        /// </summary>
        public int UID
        {
            set { _uid = value; }
            get { return _uid; }
        }
        /// <summary>
        /// 编码
        /// </summary>
        public string Code
        {
            set { _code = value; }
            get { return _code; }
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
        /// 签名
        /// </summary>
        public string Signed
        {
            set { _signed = value; }
            get { return _signed; }
        }
        /// <summary>
        /// 头像
        /// </summary>
        public string Photo
        {
            set { _photo = value; }
            get { return _photo; }
        }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday
        {
            set { _birthday = value; }
            get { return _birthday; }
        }
        /// <summary>
        /// 登记证件选择(1.身份证)
        /// </summary>
        public int PapersType
        {
            set { _paperstype = value; }
            get { return _paperstype; }
        }
        /// <summary>
        /// 登记证件号码(1.身份证号)
        /// </summary>
        public string PapersNumber
        {
            set { _papersnumber = value; }
            get { return _papersnumber; }
        }
        /// <summary>
        /// 省份
        /// </summary>
        public int Province
        {
            set { _province = value; }
            get { return _province; }
        }
        /// <summary>
        /// 城市
        /// </summary>
        public int City
        {
            set { _city = value; }
            get { return _city; }
        }
        /// <summary>
        /// 区域(县)
        /// </summary>
        public int Borough
        {
            set { _borough = value; }
            get { return _borough; }
        }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
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
        /// 家庭电话
        /// </summary>
        public string HomePhone
        {
            set { _homephone = value; }
            get { return _homephone; }
        }
        /// <summary>
        /// 联系手机
        /// </summary>
        public string HandPhone
        {
            set { _handphone = value; }
            get { return _handphone; }
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
        /// 网站
        /// </summary>
        public string PersonWebSite
        {
            set { _personwebsite = value; }
            get { return _personwebsite; }
        }
        /// <summary>
        /// 联系QQ
        /// </summary>
        public string QQ
        {
            set { _qq = value; }
            get { return _qq; }
        }

        /// <summary>
        /// 上级单位ID
        /// </summary>
        public int ParentId
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 上级单位集[逗号隔开]
        /// </summary>
        public string Parents
        {
            set { _parents = value; }
            get { return _parents; }
        }
        /// <summary>
        /// CRM联系人ID
        /// </summary>
        public int CRMID
        {
            set { _CRMID = value; }
            get { return _CRMID; }
        }
        /// <summary>
        /// 用户来源ID[0,网上注册; 1,交易员注册]
        /// </summary>
        public int Member_Type
        {
            set { _member_type = value; }
            get { return _member_type; }
        }
        /// <summary>
        /// 买家类别[-1.未知; 0.批发客户; 1.OTC拆零客户]
        /// </summary>
        public int Member_Class
        {
            set { _member_class = value; }
            get { return _member_class; }
        }

        #endregion Model

        /// <summary>
        /// 交易员
        /// </summary>
        public int Editer { get; set; }

        /// <summary>
        /// 外销人员（线下推广人员）
        /// </summary>
        public int OSPId { get; set; }

        /// <summary>
        /// 会员享受的折扣率
        /// </summary>
        public decimal discount { get; set; }
        /// <summary>
        /// 价格类型
        /// </summary>
        public string PriceCategory { get; set; }
    }
}
