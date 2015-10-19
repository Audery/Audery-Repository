using System;

namespace SOSOshop.Model
{
    /// <summary>
    /// 实体类ShopSetting 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class ShopSetting
    {
        public ShopSetting()
        { }
        #region Model
        private int _shopid;
        private int? _allowvisitorbuy;
        private int? _coupon;
        private int? _thumbnails;
        private int? _watermark;
        private int? _numberlimit;
        private int? _ordersreceive;
        private string _orderstext;
        private string _tel;
        private string _adress;
        private string _zip;
        /******************************************************************************** 
        ** 修改人：ym
        ** 修改时间：2009-9-11 
        ** 修改内容：添加
        *********************************************************************************/
        private int? _allowgroupbuydeposit;
        private int? _allowauctiondeposit;
        /// <summary>
        /// 
        /// </summary>
        public int ShopId
        {
            set { _shopid = value; }
            get { return _shopid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AllowVisitorBuy
        {
            set { _allowvisitorbuy = value; }
            get { return _allowvisitorbuy; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Coupon
        {
            set { _coupon = value; }
            get { return _coupon; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Thumbnails
        {
            set { _thumbnails = value; }
            get { return _thumbnails; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? WaterMark
        {
            set { _watermark = value; }
            get { return _watermark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? NumberLimit
        {
            set { _numberlimit = value; }
            get { return _numberlimit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? OrdersReceive
        {
            set { _ordersreceive = value; }
            get { return _ordersreceive; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OrdersText
        {
            set { _orderstext = value; }
            get { return _orderstext; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tel
        {
            set { _tel = value; }
            get { return _tel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Adress
        {
            set { _adress = value; }
            get { return _adress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Zip
        {
            set { _zip = value; }
            get { return _zip; }
        }
        /******************************************************************************** 
       ** 修改人：ym
       ** 修改时间：2009-9-11 
       ** 修改内容：添加
       *********************************************************************************/
        /// <summary>
        /// 团购时是否启用保证金
        /// </summary>
        public int? AllowGroupbuyDeposit
        {
            set { _allowgroupbuydeposit = value; }
            get { return _allowgroupbuydeposit; }
        }
        /// <summary>
        /// 拍卖时是否启用保证金
        /// </summary>
        public int? AllowAuctionDeposit
        {
            set { _allowauctiondeposit = value; }
            get { return _allowauctiondeposit; }
        }
        #endregion Model

    }
}
