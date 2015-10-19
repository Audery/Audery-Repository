using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _101shop.v3.Models
{
    public class ExpirationTimeModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        public int Product_ID { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string Product_Name { get; set; }

        /// <summary>
        /// 扩展字段1（此次用来放客户仓库ID）
        /// </summary>
        public string Extended1 { get; set; }
        /// <summary>
        /// 近效期产品库存
        /// </summary>
        public decimal Stock { get; set; }

        /// <summary>
        /// 近效期产品价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 效期
        /// </summary>
        public string ExpirationTime { get; set; }

        /// <summary>
        /// （ERP商品ID）即客户产品ID
        /// </summary>
        public string Erp_ID { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        public string Tag_PharmAttributeName { get; set; }

        /// <summary>
        /// 显示价格
        /// </summary>
        public string ShowPrice { get; set; }

        /// <summary>
        /// 商品单位
        /// </summary>
        public string Goods_Unit { get; set; }

        /// <summary>
        /// 生产厂家
        /// </summary>
        public string DrugsBase_Manufacturer { get; set; }
    }
}