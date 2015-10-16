using System;
namespace SOSOshop.Model
{
    /// <summary>
    /// 商品基本信息
    /// </summary>
    [Serializable]
    public partial class ProductInfo
    {
        public ProductInfo()
        { }
        #region Model
        private int _Product_ID;
        private int _Product_KJZS;
        private int _DrugsBase_ID;
        private int _Goods_ID;
        private decimal _Price_01;
        private decimal _Price_02;
        private decimal _RetailPrice;
        private decimal _LimitPrice;
        //private decimal _BidPrice;
        private string _Product_Name;
        //private int _Product_Type;
        //private string _Product_Synopsis;
        //private string _Product_Remark;
        private string _Product_SellingPoint;
        private string _Product_Advertisement;
        private int _Product_ClickNum;
        private int _Product_SaleNum;
        private string _Product_State;
        //private int _Product_bStop;
        //private int _Product_bPromotion;
        //private int _Product_bDiscount;
        private int _Product_bShelves = 1;
        //private int _Product_bPriceConstantlyChanging;
        private string _DrugsBase_DrugName;
        private string _DrugsBase_ProName;
        private string _DrugsBase_Specification;
        private string _DrugsBase_Formulation;
        private string _DrugsBase_Manufacturer;
        private string _DrugsBase_ApprovalNumber;
        //private int _DrugsBase_bOTC;
        //private int _DrugsBase_bNewDrugs;
        //private int _DrugsBase_bFinished;
        //private int _DrugsBase_bRaw;
        //private int _DrugsBase_bCommissionProcessing;
        //private int _DrugsBase_bNationalEssentialDrug;
        //private int _DrugsBase_bHealthInsuranceType;
        //private int _DrugsBase_MedicineType;
        //private int _DrugsBase_bStop;
        //private int _YZYJ;
        //private int _DDDJ;
        //private int _YY;
        //private int _ZL;
        //private int _GMP;
        //private int _YC;
        //private int _JK;
        //private int _DrugsBase_ProtectMedicine_ID;
        //private int _Drugsbase_Direct_ID;
        //private int _DrugsBase_MadeIn;
        //private string _DrugsBase_BaseCode;
        private string _DrugsBase_SimpeCode;
        //private string _DrugsBase_QualityStandards;
        //private string _DrugsBase_Address;
        //private string _DrugsBase_RegisteredTrademark;
        //private string _DrugsBase_RegisteredGMP;
        private int _Goods_ConveRatio;
        //private int _Goods_ConveRatio_Unit_ID;
        private string _Goods_ConveRatio_Unit;
        private string _Goods_ConveRatio_Unit_Name;
        private int _Goods_Package_ID;
        private int _Goods_Pcs;
        private int _Goods_Pcs_Small;
        private string _Goods_Package_Material;
        private string _Goods_Package_Material_Name;
        //private int _Goods_Unit_ID;
        private string _Goods_Unit;
        private string _Image;
        private string _Original;
        //private DateTime _CreateDate;
        //private DateTime _UpdateDate;
        //private string _Comment;
        //private int _Supplier_Province;
        private string _Registration_No;
        private string _tag_ids;

        //private int _IsSpecial;
        //private int _SpecialRole;

        private bool _drug_sensitive;

        /// <summary>
        /// 商品编号
        /// </summary>
        public int Product_ID
        {
            set { _Product_ID = value; }
            get { return _Product_ID; }
        }
        /// <summary>
        /// 空间指数
        /// </summary>
        public int Product_KJZS
        {
            set { _Product_KJZS = value; }
            get { return _Product_KJZS; }
        }
        /// <summary>
        /// 药品转换比编号
        /// </summary>
        public int Goods_ID
        {
            set { _Goods_ID = value; }
            get { return _Goods_ID; }
        }
        /// <summary>
        /// 商城价格101批发价
        /// </summary>
        public decimal Price_01
        {
            set { _Price_01 = value; }
            get { return _Price_01; }
        }
        /// <summary>
        /// 商城价格101拆零价
        /// </summary>
        public decimal Price_02
        {
            set { _Price_02 = value; }
            get { return _Price_02; }
        }
        /// <summary>
        /// 零售价
        /// </summary>
        public decimal RetailPrice
        {
            set { _RetailPrice = value; }
            get { return _RetailPrice; }
        }
        /// <summary>
        /// 挂网价
        /// </summary>
        public decimal LimitPrice
        {
            set { _LimitPrice = value; }
            get { return _LimitPrice; }
        }
        /// <summary>
        /// 中标价
        /// </summary>
        //public decimal BidPrice
        //{
        //    set { _BidPrice = value; }
        //    get { return _BidPrice; }
        //}
        /// <summary>
        /// 商城名称[展示的名称]
        /// </summary>
        public string Product_Name
        {
            set { _Product_Name = value; }
            get { return _Product_Name; }
        }
        /// <summary>
        /// 商品分类(1:药品)
        /// </summary>
        //public int Product_Type
        //{
        //    set { _Product_Type = value; }
        //    get { return _Product_Type; }
        //}
        /// <summary>
        /// 商品简述
        /// </summary>
        //public string Product_Synopsis
        //{
        //    set { _Product_Synopsis = value; }
        //    get { return _Product_Synopsis; }
        //}
        /// <summary>
        /// 购买须知
        /// </summary>
        //public string Product_Remark
        //{
        //    set { _Product_Remark = value; }
        //    get { return _Product_Remark; }
        //}
        /// <summary>
        /// 卖点
        /// </summary>
        public string Product_SellingPoint
        {
            set { _Product_SellingPoint = value; }
            get { return _Product_SellingPoint; }
        }
        /// <summary>
        /// 广告词
        /// </summary>
        public string Product_Advertisement
        {
            set { _Product_Advertisement = value; }
            get { return _Product_Advertisement; }
        }
        /// <summary>
        /// 点击数量
        /// </summary>
        public int Product_ClickNum
        {
            set { _Product_ClickNum = value; }
            get { return _Product_ClickNum; }
        }
        /// <summary>
        /// 销售数量
        /// </summary>
        public int Product_SaleNum
        {
            set { _Product_SaleNum = value; }
            get { return _Product_SaleNum; }
        }
        /// <summary>
        /// 标示状态[首页|中标基药|热销品种|空间指数|热卖|精品|最新]
        /// </summary>
        public string Product_State
        {
            set { _Product_State = value; }
            get { return _Product_State; }
        }
        /// <summary>
        /// 商品是否停用
        /// </summary>
        //public int Product_bStop
        //{
        //    set { _Product_bStop = value; }
        //    get { return _Product_bStop; }
        //}
        /// <summary>
        /// 商品是否促销
        /// </summary>
        //public int Product_bPromotion
        //{
        //    set { _Product_bPromotion = value; }
        //    get { return _Product_bPromotion; }
        //}
        /// <summary>
        /// 商品是否打折
        /// </summary>
        //public int Product_bDiscount
        //{
        //    set { _Product_bDiscount = value; }
        //    get { return _Product_bDiscount; }
        //}
        /// <summary>
        /// 商品是否上架
        /// </summary>
        public int Product_bShelves
        {
            set { _Product_bShelves = value; }
            get { return _Product_bShelves; }
        }
        /// <summary>
        /// 商品价格变化是否明显
        /// </summary>
        //public int Product_bPriceConstantlyChanging
        //{
        //    set { _Product_bPriceConstantlyChanging = value; }
        //    get { return _Product_bPriceConstantlyChanging; }
        //}
        /// <summary>
        /// 药品编号
        /// </summary>
        public int DrugsBase_ID
        {
            set { _DrugsBase_ID = value; }
            get { return _DrugsBase_ID; }
        }
        /// <summary>
        /// 通用名
        /// </summary>
        public string DrugsBase_DrugName
        {
            set { _DrugsBase_DrugName = value; }
            get { return _DrugsBase_DrugName; }
        }
        /// <summary>
        /// 商品名
        /// </summary>
        public string DrugsBase_ProName
        {
            set { _DrugsBase_ProName = value; }
            get { return _DrugsBase_ProName; }
        }
        /// <summary>
        /// 规格
        /// </summary>
        public string DrugsBase_Specification
        {
            set { _DrugsBase_Specification = value; }
            get { return _DrugsBase_Specification; }
        }
        /// <summary>
        /// 剂型
        /// </summary>
        public string DrugsBase_Formulation
        {
            set { _DrugsBase_Formulation = value; }
            get { return _DrugsBase_Formulation; }
        }
        /// <summary>
        /// 厂家
        /// </summary>
        public string DrugsBase_Manufacturer
        {
            set { _DrugsBase_Manufacturer = value; }
            get { return _DrugsBase_Manufacturer; }
        }
        /// <summary>
        /// 批准文号
        /// </summary>
        public string DrugsBase_ApprovalNumber
        {
            set { _DrugsBase_ApprovalNumber = value; }
            get { return _DrugsBase_ApprovalNumber; }
        }
        /// <summary>
        /// 是否OTC
        /// </summary>
        //public int DrugsBase_bOTC
        //{
        //    set { _DrugsBase_bOTC = value; }
        //    get { return _DrugsBase_bOTC; }
        //}
        /// <summary>
        /// 是否新药
        /// </summary>
        //public int DrugsBase_bNewDrugs
        //{
        //    set { _DrugsBase_bNewDrugs = value; }
        //    get { return _DrugsBase_bNewDrugs; }
        //}
        /// <summary>
        /// 是否成品
        /// </summary>
        //public int DrugsBase_bFinished
        //{
        //    set { _DrugsBase_bFinished = value; }
        //    get { return _DrugsBase_bFinished; }
        //}
        /// <summary>
        /// 是否原料
        /// </summary>
        //public int DrugsBase_bRaw
        //{
        //    set { _DrugsBase_bRaw = value; }
        //    get { return _DrugsBase_bRaw; }
        //}
        /// <summary>
        /// 是否委托加工
        /// </summary>
        //public int DrugsBase_bCommissionProcessing
        //{
        //    set { _DrugsBase_bCommissionProcessing = value; }
        //    get { return _DrugsBase_bCommissionProcessing; }
        //}
        /// <summary>
        /// 是否基药
        /// </summary>
        //public int DrugsBase_bNationalEssentialDrug
        //{
        //    set { _DrugsBase_bNationalEssentialDrug = value; }
        //    get { return _DrugsBase_bNationalEssentialDrug; }
        //}
        /// <summary>
        /// 是否医保
        /// </summary>
        //public int DrugsBase_bHealthInsuranceType
        //{
        //    set { _DrugsBase_bHealthInsuranceType = value; }
        //    get { return _DrugsBase_bHealthInsuranceType; }
        //}
        /// <summary>
        /// 中药类型
        /// </summary>
        //public int DrugsBase_MedicineType
        //{
        //    set { _DrugsBase_MedicineType = value; }
        //    get { return _DrugsBase_MedicineType; }
        //}
        /// <summary>
        /// 是否停用
        /// </summary>
        //public int DrugsBase_bStop
        //{
        //    set { _DrugsBase_bStop = value; }
        //    get { return _DrugsBase_bStop; }
        //}
        /// <summary>
        /// 药品：优质优价
        /// </summary>
        //public int YZYJ
        //{
        //    set { _YZYJ = value; }
        //    get { return _YZYJ; }
        //}
        /// <summary>
        /// 药品：单独定价
        /// </summary>
        //public int DDDJ
        //{
        //    set { _DDDJ = value; }
        //    get { return _DDDJ; }
        //}
        /// <summary>
        /// 药品：原研
        /// </summary>
        //public int YY
        //{
        //    set { _YY = value; }
        //    get { return _YY; }
        //}
        /// <summary>
        /// 药品：专利
        /// </summary>
        //public int ZL
        //{
        //    set { _ZL = value; }
        //    get { return _ZL; }
        //}
        /// <summary>
        /// 药品：GMP
        /// </summary>
        //public int GMP
        //{
        //    set { _GMP = value; }
        //    get { return _GMP; }
        //}
        /// <summary>
        /// 药品：预充
        /// </summary>
        //public int YC
        //{
        //    set { _YC = value; }
        //    get { return _YC; }
        //}
        /// <summary>
        /// 药品：进口
        /// </summary>
        //public int JK
        //{
        //    set { _JK = value; }
        //    get { return _JK; }
        //}
        /// <summary>
        /// 中药保护品种ID[中药保护品种Drugsbase_Direct]
        /// </summary>
        //public int DrugsBase_ProtectMedicine_ID
        //{
        //    set { _DrugsBase_ProtectMedicine_ID = value; }
        //    get { return _DrugsBase_ProtectMedicine_ID; }
        //}
        /// <summary>
        /// 说明书ID[说明书Drugsbase_Direct]
        /// </summary>
        //public int Drugsbase_Direct_ID
        //{
        //    set { _Drugsbase_Direct_ID = value; }
        //    get { return _Drugsbase_Direct_ID; }
        //}
        /// <summary>
        /// 1.国产或2.进口
        /// </summary>
        //public int DrugsBase_MadeIn
        //{
        //    set { _DrugsBase_MadeIn = value; }
        //    get { return _DrugsBase_MadeIn; }
        //}
        /// <summary>
        /// 本位码
        /// </summary>
        //public string DrugsBase_BaseCode
        //{
        //    set { _DrugsBase_BaseCode = value; }
        //    get { return _DrugsBase_BaseCode; }
        //}
        /// <summary>
        /// 拼音简码
        /// </summary>
        public string DrugsBase_SimpeCode
        {
            set { _DrugsBase_SimpeCode = value; }
            get { return _DrugsBase_SimpeCode; }
        }
        /// <summary>
        /// 质量层次
        /// </summary>
        //public string DrugsBase_QualityStandards
        //{
        //    set { _DrugsBase_QualityStandards = value; }
        //    get { return _DrugsBase_QualityStandards; }
        //}
        /// <summary>
        /// 产地
        /// </summary>
        //public string DrugsBase_Address
        //{
        //    set { _DrugsBase_Address = value; }
        //    get { return _DrugsBase_Address; }
        //}
        /// <summary>
        /// 注册商标
        /// </summary>
        //public string DrugsBase_RegisteredTrademark
        //{
        //    set { _DrugsBase_RegisteredTrademark = value; }
        //    get { return _DrugsBase_RegisteredTrademark; }
        //}
        /// <summary>
        /// 注册GMP
        /// </summary>
        //public string DrugsBase_RegisteredGMP
        //{
        //    set { _DrugsBase_RegisteredGMP = value; }
        //    get { return _DrugsBase_RegisteredGMP; }
        //}
        /// <summary>
        /// 转换比
        /// </summary>
        public int Goods_ConveRatio
        {
            set { _Goods_ConveRatio = value; }
            get { return _Goods_ConveRatio; }
        }
        /// <summary>
        /// 转换比单位ID
        /// </summary>
        //public int Goods_ConveRatio_Unit_ID
        //{
        //    set { _Goods_ConveRatio_Unit_ID = value; }
        //    get { return _Goods_ConveRatio_Unit_ID; }
        //}
        /// <summary>
        /// 转换比单位
        /// </summary>
        public string Goods_ConveRatio_Unit
        {
            set { _Goods_ConveRatio_Unit = value; }
            get { return _Goods_ConveRatio_Unit; }
        }
        /// <summary>
        /// 转换比说明
        /// </summary>
        public string Goods_ConveRatio_Unit_Name
        {
            set { _Goods_ConveRatio_Unit_Name = value; }
            get { return _Goods_ConveRatio_Unit_Name; }
        }
        /// <summary>
        /// 包装ID
        /// </summary>
        public int Goods_Package_ID
        {
            set { _Goods_Package_ID = value; }
            get { return _Goods_Package_ID; }
        }
        /// <summary>
        /// 件装量
        /// </summary>
        public int Goods_Pcs
        {
            set { _Goods_Pcs = value; }
            get { return _Goods_Pcs; }
        }
        /// <summary>
        /// 中包装量
        /// </summary>
        public int Goods_Pcs_Small
        {
            set { _Goods_Pcs_Small = value; }
            get { return _Goods_Pcs_Small; }
        }
        /// <summary>
        /// 包装材料
        /// </summary>
        public string Goods_Package_Material
        {
            set { _Goods_Package_Material = value; }
            get { return _Goods_Package_Material; }
        }
        /// <summary>
        /// 包装材料说明
        /// </summary>
        public string Goods_Package_Material_Name
        {
            set { _Goods_Package_Material_Name = value; }
            get { return _Goods_Package_Material_Name; }
        }
        /// <summary>
        /// 包装单位ID
        /// </summary>
        //public int Goods_Unit_ID
        //{
        //    set { _Goods_Unit_ID = value; }
        //    get { return _Goods_Unit_ID; }
        //}
        /// <summary>
        /// 包装单位
        /// </summary>
        public string Goods_Unit
        {
            set { _Goods_Unit = value; }
            get { return _Goods_Unit; }
        }
        /// <summary>
        /// 包装盒图片缩略图[可为空]
        /// </summary>
        public string Image
        {
            set { _Image = value; }
            get { return _Image; }
        }
        /// <summary>
        /// 包装盒图片原图[可为空]
        /// </summary>
        public string Original
        {
            set { _Original = value; }
            get { return _Original; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        //public DateTime CreateDate
        //{
        //    set { _CreateDate = value; }
        //    get { return _CreateDate; }
        //}
        /// <summary>
        /// 更新时间
        /// </summary>
        //public DateTime UpdateDate
        //{
        //    set { _UpdateDate = value; }
        //    get { return _UpdateDate; }
        //}
        /// <summary>
        /// 备注说明
        /// </summary>
        //public string Comment
        //{
        //    set { _Comment = value; }
        //    get { return _Comment; }
        //}
        /// <summary>
        /// 供应地区
        /// </summary>
        //public int Supplier_Province
        //{
        //    set { _Supplier_Province = value; }
        //    get { return _Supplier_Province; }
        //}
        /// <summary>
        /// 进口药品注册证
        /// </summary>
        public string Registration_No
        {
            set { _Registration_No = value; }
            get { return _Registration_No; }
        }
        /// <summary>
        /// 商品标签
        /// </summary>
        public string tag_ids
        {
            set { _tag_ids = value; }
            get { return _tag_ids; }
        }
        /// <summary>
        /// 
        /// </summary>
        //public int IsSpecial
        //{
        //    set { _IsSpecial = value; }
        //    get { return _IsSpecial; }
        //}
        /// <summary>
        /// 
        /// </summary>
        //public int SpecialRole
        //{
        //    set { _SpecialRole = value; }
        //    get { return _SpecialRole; }
        //}
        /// <summary>
        /// 是否为敏感品种
        /// </summary>
        public bool drug_sensitive
        {
            set { _drug_sensitive = value; }
            get { return _drug_sensitive; }
        }
        #endregion Model

        /// <summary>
        /// 销售方式 1 最小包装，2中包装，3整件
        /// </summary>
        public int sellType { get; set; }

    }
}

