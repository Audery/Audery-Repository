using System;

namespace SOSOshop.Model
{
    /// <summary>
    /// 权限 实体类memberTradingIntention 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class memberTradingIntention
    {
        public memberTradingIntention()
        { }
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 会员UID
        /// </summary>
        public int UID { get; set; }
        /// <summary>
        /// 药品ID
        /// </summary>
        public string DrugsBase_Name { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string Guige { get; set; }
        /// <summary>
        /// 剂型
        /// </summary>
        public string JiXing { get; set; }
        /// <summary>
        /// 生产企业
        /// </summary>
        public string QiYe { get; set; }
        /// <summary>
        /// 批准文号
        /// </summary>
        public string pzwh { get; set; }
        /// <summary>
        /// 件装量
        /// </summary>
        public int jz { get; set; }
        /// <summary>
        /// 购买数量
        /// </summary>
        public int ProNum { get; set; }
        /// <summary>
        /// 可接受到货周期(天数)
        /// </summary>
        public int ArrivalPeriod { get; set; }
        /// <summary>
        /// 情况说明
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime AddDate { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
        /// <summary>
        /// 状态[1：正常，0：已取消]
        /// </summary>
        public int State { get; set; }

        public string Disposition { set; get; }
    }
}
