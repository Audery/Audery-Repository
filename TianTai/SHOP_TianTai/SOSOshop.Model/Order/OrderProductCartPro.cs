using System;
using System.Collections.Generic;
namespace SOSOshop.Model.Order
{
    /// <summary>
    /// 购买（下订单）的模板的商品信息
    /// </summary>
    [Serializable]
    public partial class OrderProductCartPro
    {
        public OrderProductCartPro()
        { }
        #region Model
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 模板编号
        /// </summary>
        public int CartId { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProId { get; set; }
        /// <summary>
        /// 预计购买数量
        /// </summary>
        public int ProNum { get; set; }
        #endregion Model
    }
}

