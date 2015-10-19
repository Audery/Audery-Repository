using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOSOshop.BLL.Common
{
    public class Enums
    {
        /// <summary>
        /// 订单状态
        /// </summary>
        public enum OrderStatus
        {
            /// <summary>
            /// 已提交1
            /// </summary>
            Submit = 1,
            /// <summary>
            /// 已审核2
            /// </summary>
            Audit = 2,

            /// <summary>
            /// 已支付3
            /// </summary>
            Paid = 3,

            /// <summary>
            /// 已完成4
            /// </summary>
            Completed = 4,

            /// <summary>
            /// 已取消-1
            /// </summary>
            Cancelled = -1,

            /// <summary>
            /// 已作废-2
            /// </summary>
            Invalid = -2

        }
        /// <summary>
        /// 订单详情状态
        /// </summary>
        public enum OrderProductStatus
        {
            /// <summary>
            /// 已提交1
            /// </summary>
            Submit = 1,
            /// <summary>
            /// 确认供货(手动确认,需要调货)2
            /// </summary>
            ConfirmationSupply = 2,

            /// <summary>
            /// 确确认缺货3
            /// </summary>
            ConfirmationNoStock = 3,

            /// <summary>
            /// 已预购4
            /// </summary>
            Ordered = 4,

            /// <summary>
            /// 无货5
            /// </summary>
            NoGoods = 5,

            /// <summary>
            /// 已取消6
            /// </summary>
            Cancelled = 6,

            /// <summary>
            /// 已申请出库7
            /// </summary>
            AppliedOutbound = 7,

            /// <summary>
            /// 已出库待发运8
            /// </summary>
            WaitOutbound = 8,

            /// <summary>
            /// 货物已发出9
            /// </summary>
            HaveBeenSent = 9,

            /// <summary>
            /// 已收货10
            /// </summary>
            Received = 10,

            /// <summary>
            /// 确认供货(自动，有库存)11
            /// </summary>
            ConfirmationSupplyAuto = 11

        }
    }
}
