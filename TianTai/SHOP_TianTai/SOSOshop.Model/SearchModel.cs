using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOSOshop.Model
{
    /// <summary>
    /// 产品搜索模板
    /// </summary>
    public class SearchModel
    {
        /// <summary>
        /// 标签id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 扩展id
        /// </summary>
        public int tid { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 标签条件
        /// </summary>
        public string condition { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int order { get; set; }
        /// <summary>
        /// 父类ID
        /// </summary>
        public int ParentID { get; set; }
    }
}
