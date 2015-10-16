using System;
namespace SOSOshop.Model.Integral
{
    /// <summary>
    /// 会员积分礼品
    /// </summary>
    [Serializable]
    public partial class MemberIntegralGift
    {
        public MemberIntegralGift()
        { }
        
        #region Model
        /// <summary>
        /// 礼品ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 礼品名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 礼品说明
        /// </summary>
        public string detail { get; set; }
        /// <summary>
        /// 兑换积分
        /// </summary>
        public decimal Integral { get; set; }
        /// <summary>
        /// 可兑换数量
        /// </summary>
        public decimal Number { get; set; }
        /// <summary>
        /// 可兑换客户类型: 0.批发客户,1.OTC拆零客户或其他组合（用逗号隔开）
        /// </summary>
        public string Member_Class { get; set; }
        /// <summary>
        /// 状态: 0.已删除，1.上架，2.下架
        /// </summary>
        public int State { get; set; }
        #endregion Model

    }
}

