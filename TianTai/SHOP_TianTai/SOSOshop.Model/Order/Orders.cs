using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOSOshop.Model.Order
{
    /// <summary>
    /// 订单信息
    /// </summary>
    [Serializable]
    public partial class Orders
    {
        public Orders()
        { }
        #region Model
        private int _id;
        private string _orderid;
        private string _username;
        private int _receiverid;
        private DateTime _shopdate = DateTime.Now;
        private DateTime _orderdate = DateTime.Now;
        private string _consigneerealname;
        private string _consigneename;
        private string _consigneephone;
        private string _consigneeprovince;
        private string _consigneeaddress;
        private string _consigneezip;
        private string _consigneetel;
        private string _consigneefax;
        private string _consigneeemail;
        private int _paymenttype = 2;
        private int _payment = 2;
        private decimal _totalprice = 0M;
        private decimal _fees = 0M;
        private decimal _otherfees = 0M;
        private int _invoice = 0;
        private string _remark;
        private int _orderstatus = 0;
        private int _paymentstatus = 0;
        private int _ogisticsstatus = 0;
        private int? _businessmanid;
        private string _businessmanname;
        private int _carriage = 1;
        private int _ordertype = 0;
        private string _contractno;
        private string _consigneecity;
        private string _consigneeborough;
        private string _consigneeconstructionsigns;
        private string _consignestime;
        private decimal _tradefees = 0M;
        private int _tradefeespay = 0;
        private int _editer = 0;
        private int _parentid = 0;
        private string _parentcorpname;
        private int _billingcorp = 0;
        private string _billingcorpname;
        private int _isbusinesscheck = 0;
        private int? _isfinancialreview;
        private DateTime? _businesscheckdate;
        private DateTime? _financialcheckdate;
        private bool _issend;
        private string _dwid;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }

        public string dwid
        {
            set { _dwid = value; }
            get { return _dwid; }
        }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderId
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 买家姓名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 买家ID
        /// </summary>
        public int ReceiverId
        {
            set { _receiverid = value; }
            get { return _receiverid; }
        }
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime ShopDate
        {
            set { _shopdate = value; }
            get { return _shopdate; }
        }
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime OrderDate
        {
            set { _orderdate = value; }
            get { return _orderdate; }
        }
        /// <summary>
        /// 收货人真实姓名(订单填写时)
        /// </summary>
        public string ConsigneeRealName
        {
            set { _consigneerealname = value; }
            get { return _consigneerealname; }
        }
        /// <summary>
        /// 购买者姓名
        /// </summary>
        public string ConsigneeName
        {
            set { _consigneename = value; }
            get { return _consigneename; }
        }
        /// <summary>
        /// 收货人手机
        /// </summary>
        public string ConsigneePhone
        {
            set { _consigneephone = value; }
            get { return _consigneephone; }
        }
        /// <summary>
        /// 收货人所在的省市
        /// </summary>
        public string ConsigneeProvince
        {
            set { _consigneeprovince = value; }
            get { return _consigneeprovince; }
        }
        /// <summary>
        /// 收货人联系地址
        /// </summary>
        public string ConsigneeAddress
        {
            set { _consigneeaddress = value; }
            get { return _consigneeaddress; }
        }
        /// <summary>
        /// 收货人邮政编码
        /// </summary>
        public string ConsigneeZip
        {
            set { _consigneezip = value; }
            get { return _consigneezip; }
        }
        /// <summary>
        /// 收货人电话
        /// </summary>
        public string ConsigneeTel
        {
            set { _consigneetel = value; }
            get { return _consigneetel; }
        }
        /// <summary>
        /// 收货人传真
        /// </summary>
        public string ConsigneeFax
        {
            set { _consigneefax = value; }
            get { return _consigneefax; }
        }
        /// <summary>
        /// 收货人电子邮件
        /// </summary>
        public string ConsigneeEmail
        {
            set { _consigneeemail = value; }
            get { return _consigneeemail; }
        }
        /// <summary>
        /// 支付类型
        ///1-在线支付
        ///2-银行转帐
        /// </summary>
        public int PaymentType
        {
            set { _paymenttype = value; }
            get { return _paymenttype; }
        }
        /// <summary>
        /// 付款方式 （1：货到付款,2：款到发货，3,账期结算-月结等）
        /// </summary>
        public int Payment
        {
            set { _payment = value; }
            get { return _payment; }
        }
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        /// <summary>
        /// 手续费
        /// </summary>
        public decimal Fees
        {
            set { _fees = value; }
            get { return _fees; }
        }
        /// <summary>
        /// 其他费用
        /// </summary>
        public decimal OtherFees
        {
            set { _otherfees = value; }
            get { return _otherfees; }
        }
        /// <summary>
        /// 开票
        /// </summary>
        public int Invoice
        {
            set { _invoice = value; }
            get { return _invoice; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 订单进展状态
        ///1＝已提交
        ///2＝已审核
        ///3＝已支付
        ///4＝已完成
        ///-1＝已取消
        ///-2＝已作废(对应erp中的中止订单)
        /// </summary>
        public int OrderStatus
        {
            set { _orderstatus = value; }
            get { return _orderstatus; }
        }
        /// <summary>
        /// 付款情况（0：未付款；1：买家已付款）
        /// </summary>
        public int PaymentStatus
        {
            set { _paymentstatus = value; }
            get { return _paymentstatus; }
        }
        /// <summary>
        /// 送货状态（未发货 = 0, 未确认货源 = -1, 已确认货源 = -2, 多次发货中 = -3, 已发货 = 1, 已收货（签收） = 2）
        /// </summary>
        public int OgisticsStatus
        {
            set { _ogisticsstatus = value; }
            get { return _ogisticsstatus; }
        }
        /// <summary>
        /// E商的ID
        /// </summary>
        public int? BusinessmanID
        {
            set { _businessmanid = value; }
            get { return _businessmanid; }
        }
        /// <summary>
        /// E商名称
        /// </summary>
        public string BusinessmanName
        {
            set { _businessmanname = value; }
            get { return _businessmanname; }
        }
        /// <summary>
        /// 配送方式（0：未选择配送； 1：送货上门；2：其它；3：自提,4 第三方物流）
        /// </summary>
        public int Carriage
        {
            set { _carriage = value; }
            get { return _carriage; }
        }
        /// <summary>
        /// 订单类型 0＝原始订单(表示订单已被拆分)
        ///1＝标准订单
        /// 2＝预购订单
        /// </summary>
        public int OrderType
        {
            set { _ordertype = value; }
            get { return _ordertype; }
        }
        /// <summary>
        /// 货单号（第三方货运时，填写 货运货单号。)
        /// </summary>
        public string ContractNo
        {
            set { _contractno = value; }
            get { return _contractno; }
        }
        /// <summary>
        /// 购买者 所在城市
        /// </summary>
        public string ConsigneeCity
        {
            set { _consigneecity = value; }
            get { return _consigneecity; }
        }
        /// <summary>
        /// 购买者 所在地区
        /// </summary>
        public string ConsigneeBorough
        {
            set { _consigneeborough = value; }
            get { return _consigneeborough; }
        }
        /// <summary>
        /// 购买者 周围建筑
        /// </summary>
        public string ConsigneeConstructionSigns
        {
            set { _consigneeconstructionsigns = value; }
            get { return _consigneeconstructionsigns; }
        }
        /// <summary>
        /// 购买者 送货时间
        /// </summary>
        public string ConsignesTime
        {
            set { _consignestime = value; }
            get { return _consignestime; }
        }
        /// <summary>
        /// 运费
        /// </summary>
        public decimal TradeFees
        {
            set { _tradefees = value; }
            get { return _tradefees; }
        }
        /// <summary>
        /// 运费支付方(暂未使用)
        /// </summary>
        public int TradeFeesPay
        {
            set { _tradefeespay = value; }
            get { return _tradefeespay; }
        }
        /// <summary>
        /// 销售人员
        /// </summary>
        public int Editer
        {
            set { _editer = value; }
            get { return _editer; }
        }
        /// <summary>
        /// 购买单位ID
        /// </summary>
        public int parentid
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 购买单位名称
        /// </summary>
        public string parentCorpName
        {
            set { _parentcorpname = value; }
            get { return _parentcorpname; }
        }
        /// <summary>
        /// 开票单位ID
        /// </summary>
        public int BillingCorp
        {
            set { _billingcorp = value; }
            get { return _billingcorp; }
        }
        /// <summary>
        /// 开票单位名称
        /// </summary>
        public string BillingCorpName
        {
            set { _billingcorpname = value; }
            get { return _billingcorpname; }
        }
        /// <summary>
        /// 是否业务审核
        /// </summary>
        public int IsBusinessCheck
        {
            set { _isbusinesscheck = value; }
            get { return _isbusinesscheck; }
        }
        /// <summary>
        /// 是否财务审核
        /// </summary>
        public int? isFinancialReview
        {
            set { _isfinancialreview = value; }
            get { return _isfinancialreview; }
        }
        /// <summary>
        /// 业务审核时间
        /// </summary>
        public DateTime? BusinessCheckDate
        {
            set { _businesscheckdate = value; }
            get { return _businesscheckdate; }
        }
        /// <summary>
        /// 财务审核时间
        /// </summary>
        public DateTime? FinancialCheckDate
        {
            set { _financialcheckdate = value; }
            get { return _financialcheckdate; }
        }

        /// <summary>
        /// 是否先发有货商品
        /// </summary>
        public bool IsSend
        {
            set { _issend = value; }
            get { return _issend; }
        }
        /// <summary>
        /// 分单状态 :1-未分单,2-已分单，不过未分完,3-已分单并已完成.
        /// </summary>
        public int splitStatus { get; set; }
        /// <summary>
        /// 订单来源 0 商城订单 1,手机app订单
        /// </summary>
        public int source { get; set; }
        #endregion Model        
        /// <summary>
        /// 取得订单状态
        /// </summary>
        /// <param name="OrderStatus"></param>
        /// <returns></returns>
        public static string GetOrderStatus(int OrderStatus)
        {
            Dictionary<int, string> di = new Dictionary<int, string>();
            di.Add(1, "待审核");
            di.Add(2, "已审核");
            //di.Add(3, "<span style='color:#008085'>已支付</span>");
            di.Add(3, "<span style='color:#008085'>待审核</span>");
            di.Add(4, "<span style='color:#008085'>已完成</span>");
            di.Add(-1, "<span style='color:red'>已取消</span>");
            di.Add(-2, "<span style='color:red'>已作废</span>");
            return di[OrderStatus];
        }
        /// <summary>
        /// 取得物流状态送货状态（未发货 = 0, 未确认货源 = -1, 已确认货源 = -2, 多次发货中 = -3, 已发货 = 1, 已收货（签收） = 2）
        /// </summary>
        /// <param name="OgisticsStatus"></param>
        /// <returns></returns>
        public static string GetOgisticsStatus(int OgisticsStatus)
        {
            Dictionary<int, string> di = new Dictionary<int, string>();
            di.Add(0, "未发货");
            di.Add(1, "已发货");
            di.Add(2, "已收货");
            di.Add(-1, "未确认货源");
            di.Add(-2, "已确认货源");
            di.Add(-3, "多次发货中");
            return di[OgisticsStatus];
        }
        /// <summary>
        /// 取得付款方式 （1：货到付款,2：款到发货，3,账期结算-月结等）
        /// </summary>
        /// <param name="Payment"></param>
        /// <returns></returns>
        public static string GetPayment(int Payment)
        {
            Dictionary<int, string> di = new Dictionary<int, string>();
            di.Add(1, "货到付款");
            di.Add(2, "款到发货");
            di.Add(3, "账期结算");
            return di[Payment];
        }

        /// <summary>
        /// 取得支付类型 (1: 在线支付, 2: 银行转账)
        /// </summary>
        /// <param name="PaymentType"></param>
        /// <returns></returns>
        public static string GetPaymentType(int PaymentType)
        {
            Dictionary<int, string> di = new Dictionary<int, string>();
            di.Add(1, "在线支付");
            di.Add(2, "银行转账");
            return di[PaymentType];
        }

        /// <summary>
        /// 配送方式（0：未选择配送； 1：送货上门；2：其它；3：自提,4 第三方物流）
        /// </summary>
        /// <param name="Carriage"></param>
        /// <returns></returns>
        public static string GetCarriage(int Carriage)
        {
            Dictionary<int, string> di = new Dictionary<int, string>();
            di.Add(0, "未选择配送");
            di.Add(1, "送货上门");
            di.Add(2, "其它");
            di.Add(3, "自提");
            di.Add(4, "第三方物流");
            di.Add(5, "48小时内送货上门");
            return di[Carriage];
        }

        /// <summary>
        /// 订单类型 (1:普通订单, 2: 拆分订单, 3：预购订单
        /// </summary>
        /// <param name="OrderType"></param>
        /// <returns></returns>
        public static string GetOrderType(int OrderType)
        {
            Dictionary<int, string> di = new Dictionary<int, string>();
            di.Add(0, "未知订单");
            di.Add(1, "普通订单");
            di.Add(2, "拆分订单");
            di.Add(3, "预购订单");
            return di[OrderType];
        }

        /// <summary>
        /// 判断是否为子订单 true为子订单，false为普通订单
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static bool IsSubOrders(string OrderNo)
        {
            if (OrderNo.IndexOf('-') > 0)
            {
                return true;
            }
            return false;
        }
    }
}
