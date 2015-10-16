using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _101shop.Common
{
    /// <summary>
    /// 销售方式 
    /// </summary>
    public enum SellTypes
    {
        最小包装 = 1, 中包装 = 2, 整件 = 3
    }
    /// <summary>
    /// 销售方式 
    /// </summary>
    public class SellType
    {
        /// <summary>
        /// 获取销售方式
        /// </summary>
        /// <param name="sellType"></param>
        /// <returns></returns>
        public static string GetType(int sellType)
        {
            Dictionary<int, string> di = new Dictionary<int, string>();
            di.Add(1, "最小包装");
            di.Add(2, "中包装");
            di.Add(3, "整件");
            return di[sellType];
        }
        /// <summary>
        /// 计算价格
        /// </summary>
        /// <param name="sellType">销售方式</param>
        /// <param name="goods_Pcs">件装</param>
        /// <param name="goods_Pcs_Small">中包装</param>
        /// <param name="price">价格</param>
        /// <returns></returns>
        public static decimal GetPrice(int sellType, int goods_Pcs, int goods_Pcs_Small, decimal price)
        {
            decimal newprice = price;
            switch (sellType)
            {
                case 2: newprice = goods_Pcs_Small * price; break;
                case 3: newprice = goods_Pcs * price; break;
            }
            return newprice;
        }
        /// <summary>
        /// 计算库存
        /// </summary>
        /// <param name="sellType">销售方式</param>
        /// <param name="goods_Pcs">件装</param>
        /// <param name="goods_Pcs_Small">中包装</param>
        /// <param name="stock">库存</param>
        /// <returns></returns>
        public static int GetStock(int sellType, int goods_Pcs, int goods_Pcs_Small, int stock)
        {
            int newstock = stock;
            switch (sellType)
            {
                case 2: newstock = stock / goods_Pcs_Small; break;
                case 3: newstock = stock / goods_Pcs; break;
            }
            return newstock;
        }
    }
}
