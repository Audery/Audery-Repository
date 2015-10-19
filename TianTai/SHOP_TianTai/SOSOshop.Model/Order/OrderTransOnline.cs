using System;

namespace SOSOshop.Model.Order
{
    /// <summary>
    /// 订单在线支付信息
    /// </summary>
    [Serializable]
    public partial class OrderTransOnline
    {
        public OrderTransOnline()
        { }
        #region Model
        /// <summary>
        /// 主键
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 客户编号UID
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// 客户姓名 
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 客户单位
        /// </summary>
        public string MerSecName { get; set; }
        /// <summary>
        /// 交易号
        /// </summary>
        public string TransId { get; set; }
        /// <summary>
        /// 交易订单号
        /// </summary>
        public string TransOrderId { get; set; }
        /// <summary>
        /// 交易名称（IPER:个人, EPER:企业）
        /// </summary>
        public string TransName { get; set; }
        /// <summary>
        /// 交易流水号
        /// </summary>
        public string TransSeqNo { get; set; }
        /// <summary>
        /// 交易金额
        /// </summary>
        public decimal TransAmt { get; set; }
        /// <summary>
        /// 退货金额
        /// </summary>
        public decimal TransAmt1 { get; set; }
        /// <summary>
        /// 服务费用金额
        /// </summary>
        public decimal FeeAmt { get; set; }
        /// <summary>
        /// 交易时间
        /// </summary>
        public DateTime TransDateTime { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime TransPPDateTime { get; set; }
        /// <summary>
        /// 结算时间
        /// </summary>
        public DateTime ClearingDate { get; set; }
        /// <summary>
        /// 交易状态（00：交易成功，01：交易失败，02：撤消成功，98：待交易，99：交易超时）
        /// </summary>
        public string TransStatus { get; set; }
        /// <summary>
        /// 货币（01：人民币）
        /// </summary>
        public string CurrencyType { get; set; }
        /// <summary>
        /// 商品信息
        /// </summary>
        public string ProductInfo { get; set; }
        /// <summary>
        /// 商户网银ID
        /// </summary>
        public string MerchantId { get; set; }
        /// <summary>
        /// 客户网银类别
        /// </summary>
        public string PayAcctType { get; set; }
        /// <summary>
        /// 客户网银银行ID
        /// </summary>
        public string PayAccNo { get; set; }
        /// <summary>
        /// 客户网银银行
        /// </summary>
        public string PayBankNo { get; set; }
        /// <summary>
        /// 客户网银银行名称
        /// </summary>
        public string PayBankName { get; set; }
        /// <summary>
        /// 交易时客户主机IP
        /// </summary>
        public string PayIp { get; set; }
        /// <summary>
        /// 交易其他信息
        /// </summary>
        public string MsgExt { get; set; }
        #endregion Model
    }
}
