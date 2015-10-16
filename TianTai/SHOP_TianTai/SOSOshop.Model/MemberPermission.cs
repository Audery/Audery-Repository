using System;

namespace SOSOshop.Model
{
    /// <summary>
    /// 权限 实体类MemberPermission 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class MemberPermission
    {
        public MemberPermission()
        { }
        #region Model

        private int _uid;
        private bool _istrade;
        private bool _islookstock;
        private bool _islookprice_01;
        private bool _islookprice_02;
        private bool _islookproduct_01;
        private bool _islookproduct_02;
        private bool _isperiodicalsettle;
        private bool _ismoneyandshipping;
        private bool _isCOD;
        private bool _isBuyFilingStatus;

        /// <summary>
        /// 会员UID
        /// </summary>
        public int UID
        {
            set { _uid = value; }
            get { return _uid; }
        }
        /// <summary>
        /// 交易权限
        /// </summary>
        public bool IsTrade
        {
            set { _istrade = value; }
            get { return _istrade; }
        }
        /// <summary>
        /// 实时库存查看权限
        /// </summary>
        public bool IsLookStock
        {
            set { _islookstock = value; }
            get { return _islookstock; }
        }
        /// <summary>
        /// 批发价格查看权限
        /// </summary>
        public bool IsLookPrice_01
        {
            set { _islookprice_01 = value; }
            get { return _islookprice_01; }
        }
        /// <summary>
        /// OTC拆零价格查看权限
        /// </summary>
        public bool IsLookPrice_02
        {
            set { _islookprice_02 = value; }
            get { return _islookprice_02; }
        }
        /// <summary>
        /// 批发商品查看权限
        /// </summary>
        public bool IsLookProduct_01
        {
            set { _islookproduct_01 = value; }
            get { return _islookproduct_01; }
        }
        /// <summary>
        /// OTC拆零商品查看权限
        /// </summary>
        public bool IsLookProduct_02
        {
            set { _islookproduct_02 = value; }
            get { return _islookproduct_02; }
        }
        /// <summary>
        /// 定期结算权限
        /// </summary>
        public bool IsPeriodicalSettle
        {
            set { _isperiodicalsettle = value; }
            get { return _isperiodicalsettle; }
        }
        /// <summary>
        /// 款到发货权限
        /// </summary>
        public bool IsMoneyAndShipping
        {
            get { return _ismoneyandshipping; }
            set { _ismoneyandshipping = value; }
        }
        /// <summary>
        /// 货到付款权限
        /// </summary>
        public bool IsCOD
        {
            get { return _isCOD; }
            set { _isCOD = value; }
        }
        /// <summary>
        /// GSP是否建档
        /// </summary>
        public bool IsBuyFilingStatus
        {
            get { return _isBuyFilingStatus; }
            set { _isBuyFilingStatus = value; }
        }
        #endregion Model
        /// <summary>
        /// 是否有有货先发的权限
        /// </summary>
        public bool IsPriorDistribution { get; set; }
        /// <summary>
        /// 是否有48小时内送货上门的权限
        /// </summary>
        public bool IsShippingFor48h { get; set; }
        /// <summary>
        /// 是否拥有快捷开通交易的权限
        /// </summary>
        public bool IsSpecialTrade { get; set; }
    }
}
