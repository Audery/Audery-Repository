using System;
using System.Collections.Generic;
namespace SOSOshop.Model.Order
{
    /// <summary>
    /// 订单的商品明细（买家购买的商品单价与数量等）
    /// </summary>
    [Serializable]
    public partial class OrderProduct
    {
        public OrderProduct()
        { }
        #region Model
        private int _id;
        private string _orderid;
        private int? _proid;
        private string _proname;
        private decimal? _proprice;
        private decimal _pronum;
        private DateTime? _addtime;
        private string _pro_pno;
        private string _pro_pdate;
        private int? _status;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 商品ID
        /// </summary>
        public int? ProId
        {
            set { _proid = value; }
            get { return _proid; }
        }
        /// <summary>
        /// 商品名
        /// </summary>
        public string ProName
        {
            set { _proname = value; }
            get { return _proname; }
        }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal? ProPrice
        {
            set { _proprice = value; }
            get { return _proprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal ProNum
        {
            set { _pronum = value; }
            get { return _pronum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string pro_pno
        {
            set { _pro_pno = value; }
            get { return _pro_pno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string pro_pdate
        {
            set { _pro_pdate = value; }
            get { return _pro_pdate; }
        }

        #region Addbychenjiayu
        /// <summary>
        /// 仓库ID(用于会写到客户的ERP中)
        /// </summary>
        public string StorageID { get; set; }

        /// <summary>
        /// 扩展字段1(商城产品的促销信息，需要传输给ERP)
        /// </summary>
        public string Extend1 { get; set; }

        /// <summary>
        /// 是否近效期
        /// </summary>
        public int IsExpirationProduct { get; set; }
        #endregion

        /// <summary>
        /// 商品供应商(默认0)
        /// </summary>
        public int iden { get; set; }
        /// <summary>
        /// 商品机构id默认000
        /// </summary>
        public string jigid { get; set; }
        /// <summary>
        /// 1＝已提交
        ///2＝确认供货(手动确认,需要调货)
        ///3＝确认缺货
        ///4＝已预购
        ///5＝无货
        ///6＝已取消
        ///7＝已申请出库
        ///8＝已出库待发运
        ///9＝货物已发出
        ///10＝已收货
        ///11=确认供货(自动，有库存)
        /// </summary>
        public int? Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 是否被分单
        /// </summary>
        public bool issplit { get; set; }
        #endregion Model
        /// <summary>
        /// erp中的商品编号
        /// </summary>
        public string spid { get; set; }
        /// <summary>
        /// 取得订单状态
        /// </summary>
        /// <param name="stauts"></param>
        /// <returns></returns>
        public static string GetStauts(int stauts)
        {
            Dictionary<int, string> di = new Dictionary<int, string>();
            di.Add(1, "待审核");
            di.Add(2, "确认供货");
            di.Add(11, "确认供货-库");//有库存系统自动确认的供货
            di.Add(3, "确认缺货");
            di.Add(4, "已预购");
            di.Add(5, "无货");
            di.Add(6, "已取消");
            di.Add(7, "已申请出库");
            di.Add(8, "已出库待发运");
            di.Add(9, "货物已发出");
            di.Add(10, "已收货");
            return di[stauts];
        }

        /// <summary>
        /// 取得订单状态
        /// </summary>
        /// <param name="stauts"></param>
        /// <returns></returns>
        public static string GetClientStauts(int stauts)
        {
            Dictionary<int, string> di = new Dictionary<int, string>();
            di.Add(1, "已提交");
            di.Add(2, "已审核");
            di.Add(11, "已提交");//有库存系统自动确认的供货
            di.Add(3, "缺货");
            di.Add(4, "已预购");
            di.Add(5, "无货");
            di.Add(6, "已取消");
            di.Add(7, "已申请出库");
            //di.Add(8, "已出库待发运");
            di.Add(8, "货物已发出");
            di.Add(9, "货物已发出");
            di.Add(10, "已收货");
            return di[stauts];
        }

    }
}

