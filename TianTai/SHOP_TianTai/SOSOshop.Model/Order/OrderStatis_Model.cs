using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOSOshop.Model.Order
{
    /// <summary>
    /// 订单统计数据模型
    /// </summary>
    public class OrderStatis_Model
    {
        public OrderStatis_Model() { }

        /// <summary>
        /// 行号
        /// </summary>
        public long RowNumber { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        public int Product_ID { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Product_Name { get; set; }

        /// <summary>
        /// 药品规格
        /// </summary>
        public string DrugsBase_Specification { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Goods_ConveRatio_Unit_Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Goods_ConveRatio_Unit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Goods_Unit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Goods_ConveRatio { get; set; }

        /// <summary>
        /// 药品剂型
        /// </summary>
        public string DrugsBase_Formulation { get; set; }

        /// <summary>
        /// 生产厂家
        /// </summary>
        public string DrugsBase_Manufacturer { get; set; }

        /// <summary>
        /// 下单客户数量
        /// </summary>
        public int CustomerNum { get; set; }

        /// <summary>
        /// 订单数量
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 销售数量
        /// </summary>
        public int SaleNum { get; set; }

        /// <summary>
        /// 销售金额
        /// </summary>
        public decimal SaleAllPrice { get; set; }

        /// <summary>
        /// 最末一笔交易的商品来源
        /// </summary>
        public int ProductSource { get; set; }
    }
}
