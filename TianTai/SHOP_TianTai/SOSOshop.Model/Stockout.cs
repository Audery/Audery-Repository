using System;
namespace SOSOshop.Model
{
    /// <summary>
    /// 用户下了订单的商品，后台商品管理员确认无货后装商品添加到此表，做货到通知等操作。
    /// </summary>
    [Serializable]
    public partial class Stockout
    {
        public Stockout()
        { }
        #region Model
        private int _id;
        private int _uid;
        private int _product_id;
        private DateTime _created;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 会员UID
        /// </summary>
        public int UID
        {
            set { _uid = value; }
            get { return _uid; }
        }
        /// <summary>
        /// 商品ID
        /// </summary>
        public int Product_ID
        {
            set { _product_id = value; }
            get { return _product_id; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime created
        {
            set { _created = value; }
            get { return _created; }
        }
        /// <summary>
        /// 此次缺货购买的数量
        /// </summary>
        public int Num { get; set; }
        #endregion Model

    }
}

