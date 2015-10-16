using System;
namespace SOSOshop.Model.Integral
{
    /// <summary>
    /// 会员积分礼品兑换详情
    /// </summary>
    [Serializable]
    public partial class MemberIntegralGiftExchange
    {
        public MemberIntegralGiftExchange()
        { }
        
        #region Model
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 会员ID
        /// </summary>
        public int uid { get; set; }
        /// <summary>
        /// 礼品ID
        /// </summary>
        public int Gift_ID { get; set; }
        /// <summary>
        /// 兑换数量
        /// </summary>
        public decimal Gift_Number { get; set; }
        /// <summary>
        /// 状态: 0.取消，1.待处理，2.已处理，3.已邮寄
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime ontime { get; set; }
        /// <summary>
        /// 收货地址
        /// </summary>
        public string ConsigneeAddress { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ConsigneeName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ConsigneePhone { get; set; }
        /// <summary>
        /// 处理人ID
        /// </summary>
        public int Editer { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        #endregion Model

        #region Model 相关数据
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string truename { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 礼品名称
        /// </summary>
        public string GiftName { get; set; }
        /// <summary>
        /// 处理人
        /// </summary>
        public string EditerName { get; set; }
        #endregion Model

    }
}

