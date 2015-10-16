using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOSOshop.Model
{
    /// <summary>
    /// 买家信息的相关定义
    /// </summary>
    public class MemberKeyValue
    {
        /// <summary>
        /// 状态
        /// </summary>
        public enum State
        {
            已审核 = 0, 未审核 = 1, 冻结 = 2
        };
        /// <summary>
        /// 买家类别
        /// </summary>
        public enum Member_Class
        {
            批发客户 = 0, OTC客户 = 1, 无 = int.MaxValue
        };
        /// <summary>
        /// 会员类别
        /// </summary>
        public enum UserType
        {
            普通会员 = 0, 企业会员 = 1, 无 = int.MaxValue
        };
        /// <summary>
        /// 来源
        /// </summary>
        public enum Member_Type
        {
            网上注册 = 0, 电话注册 = 1
        };
    }
}
