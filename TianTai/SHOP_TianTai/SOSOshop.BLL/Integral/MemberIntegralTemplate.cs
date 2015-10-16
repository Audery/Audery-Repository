using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SOSOshop.BLL.Integral
{
    /// <summary>
    /// 会员积分模板
    /// </summary>
    public class MemberIntegralTemplate : DbBase
    {
        /// <summary>
        /// 取得模板所能获取的积分数量
        /// </summary>
        /// <param name="me"></param>
        /// <returns></returns>
        public decimal GetIntegral(MemberIntegralTemplateEnum me)
        {
            return (decimal)base.ExecuteScalar("SELECT Integral FROM MemberIntegralTemplate WHERE id=" + me.GetHashCode());
        }       
        /// <summary>
        /// 取得模板全部数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetList()
        {
            return base.ExecuteDataSet("SELECT * FROM MemberIntegralTemplate").Tables[0];
        }
    }
    /// <summary>
    /// 积分模板类型
    /// </summary>
    public enum MemberIntegralTemplateEnum
    {
        赠送积分 = 0, 会员注册 = 1, 建档通过 = 2, 提交交易意向 = 3, 每日签到 = 4, 成交订单 = 5
    }
    /// <summary>
    /// 积分模板类型描述
    /// </summary>
    public class MemberIntegralTemplateDetail
    {
        /// <summary>
        /// 频率
        /// </summary>
        /// <param name="id">模板ID</param>
        /// <returns></returns>
        public static string MemberIntegralTemplateEnumFrequency(MemberIntegralTemplateEnum id)
        {
            string s = "";
            switch (id)
            {
                case MemberIntegralTemplateEnum.会员注册:
                    s = "一次";
                    break;
                case MemberIntegralTemplateEnum.建档通过:
                    s = "一次";
                    break;
                case MemberIntegralTemplateEnum.提交交易意向:
                    s = "每天10次";
                    break;
                case MemberIntegralTemplateEnum.每日签到:
                    s = "每天一次";
                    break;
                case MemberIntegralTemplateEnum.成交订单:
                    s = "无限制";
                    break;
            }
            return s;
        }
        /// <summary>
        /// 奖励积分
        /// </summary>
        /// <param name="id">模板ID</param>
        /// <param name="Integral">积分</param>
        /// <returns></returns>
        public static string MemberIntegralTemplateEnumIntegral(MemberIntegralTemplateEnum id, decimal Integral)
        {
            string s = "";
            switch (id)
            {
                case MemberIntegralTemplateEnum.会员注册:
                    s = string.Format("{0:f0}", Integral);
                    break;
                case MemberIntegralTemplateEnum.建档通过:
                    s = string.Format("{0:f0}", Integral);
                    break;
                case MemberIntegralTemplateEnum.提交交易意向:
                    s = string.Format("每次{0:f0}", Integral);
                    break;
                case MemberIntegralTemplateEnum.每日签到:
                    s = string.Format("每次{0:f0}", Integral);
                    break;
                case MemberIntegralTemplateEnum.成交订单:
                    s = string.Format("成交金额×{0}", Integral.ToString().TrimEnd('0').TrimEnd('.'));
                    break;
            }
            return s;
        }

    }
}
