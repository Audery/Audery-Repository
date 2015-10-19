using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOSOshop.Model
{
    /// <summary>
    /// 用户反馈数据模型
    /// </summary>
    [Serializable]
    public class MemberFeedbackModel
    {
        /// <summary>
        /// 行号
        /// </summary>
        public long RowNumber { get; set; }

        /// <summary>
        /// 记录Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UID { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string TrueName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string HandPhone { get; set; }

        /// <summary>
        /// 省份Id
        /// </summary>
        public int Province { get; set; }

        /// <summary>
        /// 省份名
        /// </summary>
        public string Province_ { get; set; }

        /// <summary>
        /// 城市Id
        /// </summary>
        public int City { get; set; }

        /// <summary>
        /// 城市名
        /// </summary>
        public string City_ { get; set; }

        /// <summary>
        /// 县区Id
        /// </summary>
        public int Borough { get; set; }

        /// <summary>
        /// 县区名
        /// </summary>
        public string Borough_ { get; set; }

        ///// <summary>
        ///// 企业Id
        ///// </summary>
        //public int EnterpriseId { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary>
        public string EnterpriseName { get; set; }

        /// <summary>
        /// 用户反馈信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 记录生成日期
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }
    }
}
