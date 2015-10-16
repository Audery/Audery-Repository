using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOSOshop.Model
{
    public class ProductState
    {
        /// <summary>
        /// 新品
        /// </summary>
        public int IsNew_0 { get; set; }

        /// <summary>
        /// 抢购
        /// </summary>
        public int QiangGou_1 { get; set; }

        /// <summary>
        /// 促销
        /// </summary>
        public int CuXiao_2 { get; set; }

        /// <summary>
        /// 推荐
        /// </summary>
        public int TuiJian_3 { get; set; }

        /// <summary>
        /// 热卖
        /// </summary>
        public int Hot_4 { get; set; }
        
    }
}
