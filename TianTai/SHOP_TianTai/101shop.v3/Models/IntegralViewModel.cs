using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _101shop.v3.Models
{
    public class IntegralViewModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        public int Integral { get; set; }
        /// <summary>
        /// 属性
        /// </summary>
        public string Attribute { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImgPath { get; set; }
    }
}