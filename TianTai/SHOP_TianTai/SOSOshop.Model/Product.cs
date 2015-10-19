using System;
namespace SOSOshop.Model
{
    /// <summary>
    /// 商品列表
    /// </summary>
    [Serializable]
    public partial class Product
    {
        public Product()
        { }
        #region Model
        private int _product_id;
        private int _product_id_02 = 0;
        private decimal _price_01 = 0M;
        private decimal _price_02 = 0M;
        private decimal _price_03 = 0M;
        private int _stock = 0;
        private string _pihao = "";
        private string _sxrq = "";
        private int _minsell = 0;
        private int _otcminsell = 0;
        private string _is_cl = "否";
        private string _ggy1 = "";
        private string _maid1 = "";
        private int _maxsell = 600;
        private decimal _zbj = 0M;
        private decimal _drj = 0M;
        private int _selltype = 1;
        private int _otcselltype = 1;
        private int _goods_id;
        private int _drugsbase_id;
        private int? _goods_package_id;
        private string _product_name;
        private int _product_bshelves;
        private int _shop_bshelves = 0;
        private string _beactive;
        private int _product_salenum;
        private int _product_kjzs;
        private string _drugsbase_approvalnumber;
        private string _registration_no;
        private string _drugsbase_manufacturer;
        private string _manufacturer_short;
        private string _drugsbase_drugname;
        private string _drugsbase_formulation;
        private int _goods_converatio;
        private string _goods_converatio_unit_name;
        private string _goods_converatio_unit;
        private string _goods_package_material;
        private string _goods_package_material_name;
        private int _goods_pcs;
        private int _goods_pcs_small;
        private string _tag_ids;
        private bool _drug_sensitive;
        private string _spid;
        private string _product_advertisement;
        private string _product_sellingpoint;
        private string _drugsbase_proname;
        private string _image;
        private string _original;
        private string _goods_unit;
        private string _drugsbase_simpecode;
        private string _drugsbase_specification;
        private decimal _retailprice;
        private decimal _limitprice;
        private int _product_clicknum;
        private string _product_state;
        private bool _bimage = false;
        private DateTime _created = DateTime.Now;
        private DateTime _created2 = DateTime.Now;
        private bool _isliuxiang = false;
        private bool _iskong = false;
        private int? _drugsbase_bstop;
        private int? _drugsbase_botc;
        private int? _drugsbase_bnewdrugs;
        private int? _drugsbase_bfinished;
        private int? _drugsbase_braw;
        private int? _drugsbase_bcommissionprocessing;
        private int? _drugsbase_bnationalessentialdrug;
        private int? _drugsbase_bhealthinsurancetype;
        private int? _drugsbase_medicinetype;
        private decimal? _cuprice = 0M;
        private decimal? _discount = 1M;
        private DateTime? _begindate;
        private DateTime? _enddate;
        private string _cgy;
        /// <summary>
        /// 
        /// </summary>
        public int Product_ID
        {
            set { _product_id = value; }
            get { return _product_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Product_ID_02
        {
            set { _product_id_02 = value; }
            get { return _product_id_02; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Price_01
        {
            set { _price_01 = value; }
            get { return _price_01; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Price_02
        {
            set { _price_02 = value; }
            get { return _price_02; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Price_03
        {
            set { _price_03 = value; }
            get { return _price_03; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Stock
        {
            set { _stock = value; }
            get { return _stock; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string pihao
        {
            set { _pihao = value; }
            get { return _pihao; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string sxrq
        {
            set { _sxrq = value; }
            get { return _sxrq; }
        }
        /// <summary>
        /// 每个会员每天可买最大促销数量
        /// </summary>
        public int minsell
        {
            set { _minsell = value; }
            get { return _minsell; }
        }
        /// <summary>
        /// 每天可促销的最大数量
        /// </summary>
        public int otcMinSell
        {
            set { _otcminsell = value; }
            get { return _otcminsell; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string is_cl
        {
            set { _is_cl = value; }
            get { return _is_cl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ggy1
        {
            set { _ggy1 = value; }
            get { return _ggy1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string maid1
        {
            set { _maid1 = value; }
            get { return _maid1; }
        }
        /// <summary>
        /// 促销最大可购买数量
        /// </summary>
        public int maxsell
        {
            set { _maxsell = value; }
            get { return _maxsell; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal zbj
        {
            set { _zbj = value; }
            get { return _zbj; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal drj
        {
            set { _drj = value; }
            get { return _drj; }
        }
        /// <summary>
        /// 出售数量规则 1 不限制，2中包装，3整件
        /// </summary>
        public int sellType
        {
            set { _selltype = value; }
            get { return _selltype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int otcSellType
        {
            set { _otcselltype = value; }
            get { return _otcselltype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Goods_ID
        {
            set { _goods_id = value; }
            get { return _goods_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int DrugsBase_ID
        {
            set { _drugsbase_id = value; }
            get { return _drugsbase_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Goods_Package_ID
        {
            set { _goods_package_id = value; }
            get { return _goods_package_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Product_Name
        {
            set { _product_name = value; }
            get { return _product_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Product_bShelves
        {
            set { _product_bshelves = value; }
            get { return _product_bshelves; }
        }
        /// <summary>
        /// 映射工具设定的上下架，操作员设置后前后台都执行该规则
        /// </summary>
        public int Shop_bShelves
        {
            set { _shop_bshelves = value; }
            get { return _shop_bshelves; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string beactive
        {
            set { _beactive = value; }
            get { return _beactive; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Product_SaleNum
        {
            set { _product_salenum = value; }
            get { return _product_salenum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Product_KJZS
        {
            set { _product_kjzs = value; }
            get { return _product_kjzs; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DrugsBase_ApprovalNumber
        {
            set { _drugsbase_approvalnumber = value; }
            get { return _drugsbase_approvalnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Registration_No
        {
            set { _registration_no = value; }
            get { return _registration_no; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DrugsBase_Manufacturer
        {
            set { _drugsbase_manufacturer = value; }
            get { return _drugsbase_manufacturer; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Manufacturer_Short
        {
            set { _manufacturer_short = value; }
            get { return _manufacturer_short; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DrugsBase_DrugName
        {
            set { _drugsbase_drugname = value; }
            get { return _drugsbase_drugname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DrugsBase_Formulation
        {
            set { _drugsbase_formulation = value; }
            get { return _drugsbase_formulation; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Goods_ConveRatio
        {
            set { _goods_converatio = value; }
            get { return _goods_converatio; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Goods_ConveRatio_Unit_Name
        {
            set { _goods_converatio_unit_name = value; }
            get { return _goods_converatio_unit_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Goods_ConveRatio_Unit
        {
            set { _goods_converatio_unit = value; }
            get { return _goods_converatio_unit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Goods_Package_Material
        {
            set { _goods_package_material = value; }
            get { return _goods_package_material; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Goods_Package_Material_Name
        {
            set { _goods_package_material_name = value; }
            get { return _goods_package_material_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Goods_Pcs
        {
            set { _goods_pcs = value; }
            get { return _goods_pcs; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Goods_Pcs_Small
        {
            set { _goods_pcs_small = value; }
            get { return _goods_pcs_small; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string tag_ids
        {
            set { _tag_ids = value; }
            get { return _tag_ids; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool drug_sensitive
        {
            set { _drug_sensitive = value; }
            get { return _drug_sensitive; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string spid
        {
            set { _spid = value; }
            get { return _spid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Product_Advertisement
        {
            set { _product_advertisement = value; }
            get { return _product_advertisement; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Product_SellingPoint
        {
            set { _product_sellingpoint = value; }
            get { return _product_sellingpoint; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DrugsBase_ProName
        {
            set { _drugsbase_proname = value; }
            get { return _drugsbase_proname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Image
        {
            set { _image = value; }
            get { return _image; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Original
        {
            set { _original = value; }
            get { return _original; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Goods_Unit
        {
            set { _goods_unit = value; }
            get { return _goods_unit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DrugsBase_SimpeCode
        {
            set { _drugsbase_simpecode = value; }
            get { return _drugsbase_simpecode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DrugsBase_Specification
        {
            set { _drugsbase_specification = value; }
            get { return _drugsbase_specification; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal RetailPrice
        {
            set { _retailprice = value; }
            get { return _retailprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal LimitPrice
        {
            set { _limitprice = value; }
            get { return _limitprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Product_ClickNum
        {
            set { _product_clicknum = value; }
            get { return _product_clicknum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Product_State
        {
            set { _product_state = value; }
            get { return _product_state; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool bImage
        {
            set { _bimage = value; }
            get { return _bimage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime created
        {
            set { _created = value; }
            get { return _created; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime created2
        {
            set { _created2 = value; }
            get { return _created2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool isLiuXiang
        {
            set { _isliuxiang = value; }
            get { return _isliuxiang; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool isKong
        {
            set { _iskong = value; }
            get { return _iskong; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DrugsBase_bStop
        {
            set { _drugsbase_bstop = value; }
            get { return _drugsbase_bstop; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DrugsBase_bOTC
        {
            set { _drugsbase_botc = value; }
            get { return _drugsbase_botc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DrugsBase_bNewDrugs
        {
            set { _drugsbase_bnewdrugs = value; }
            get { return _drugsbase_bnewdrugs; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DrugsBase_bFinished
        {
            set { _drugsbase_bfinished = value; }
            get { return _drugsbase_bfinished; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DrugsBase_bRaw
        {
            set { _drugsbase_braw = value; }
            get { return _drugsbase_braw; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DrugsBase_bCommissionProcessing
        {
            set { _drugsbase_bcommissionprocessing = value; }
            get { return _drugsbase_bcommissionprocessing; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DrugsBase_bNationalEssentialDrug
        {
            set { _drugsbase_bnationalessentialdrug = value; }
            get { return _drugsbase_bnationalessentialdrug; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DrugsBase_bHealthInsuranceType
        {
            set { _drugsbase_bhealthinsurancetype = value; }
            get { return _drugsbase_bhealthinsurancetype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DrugsBase_MedicineType
        {
            set { _drugsbase_medicinetype = value; }
            get { return _drugsbase_medicinetype; }
        }
        /// <summary>
        /// 促销价
        /// </summary>
        public decimal? CuPrice
        {
            set { _cuprice = value; }
            get { return _cuprice; }
        }
        /// <summary>
        /// 折扣率
        /// </summary>
        public decimal? Discount
        {
            set { _discount = value; }
            get { return _discount; }
        }
        /// <summary>
        /// 促销终止时间
        /// </summary>
        public DateTime? BeginDate
        {
            set { _begindate = value; }
            get { return _begindate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }

        /// <summary>
        /// 采购员
        /// </summary>
        public string Cgy
        {
            set { _cgy = value; }
            get { return _cgy; }
        }
        #endregion Model

    }
}

