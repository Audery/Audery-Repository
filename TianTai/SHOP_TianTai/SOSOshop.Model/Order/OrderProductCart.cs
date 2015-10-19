using System;
using System.Collections.Generic;
namespace SOSOshop.Model.Order
{
    /// <summary>
    /// 购买（下订单）的模板
    /// </summary>
    [Serializable]
    public partial class OrderProductCart
    {
        public OrderProductCart()
        { }
        #region Model
        /// <summary>
        /// 模板编号
        /// </summary>
        public int CartId { get; set; }
        /// <summary>
        /// 买家ID
        /// </summary>
        public int UID { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 模板描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 状态[1.正常，0.已删除]
        /// </summary>
        public int State { get; set; }
        #endregion Model
    }
}

