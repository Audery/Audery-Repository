using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace _101shop.v3.Models
{

    /// <summary>
    /// 登陆信息
    /// </summary>
    public class LogOnModel
    {
        [StringLength(30, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [StringLength(11, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 11)]
        [Display(Name = "手机号")]
        public string MobilePhone { get; set; }

        [StringLength(30, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string PassWord { get; set; }

        [StringLength(4, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 4)]
        [Display(Name = "验证码")]
        public string Captcha { get; set; }
    }

    /// <summary>
    /// 注册信息
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [StringLength(30, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [StringLength(11, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 11)]
        [Display(Name = "手机号")]
        public string MobilePhone { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [StringLength(30, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string PassWord { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("PassWord", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [Display(Name = "联系人")]
        public string LinkMan { get; set; }

        /// <summary>
        /// 联系邮箱
        /// </summary>
        [DataType(DataType.EmailAddress)]
        [Display(Name = "联系邮箱")]
        public string Email { get; set; }

        /// <summary>
        /// 注册地址
        /// </summary>
        [Display(Name = "注册地址")]
        public string RegAddress { get; set; }

        /// <summary>
        /// 企业类型
        /// </summary>
        [Display(Name = "企业类型")]
        public string CompanyClass { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [StringLength(4, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 4)]
        [Display(Name = "验证码")]        
        public string Captcha { get; set; }
    }

    /// <summary>
    /// 用户信息[包括账户、联系人、单位、地区、用户权限等]
    /// </summary>
    [Serializable]
    public class UserModel
    {
        /// <summary>
        /// UID
        /// </summary>
        [Display(Name = "UID")]
        public int UID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Display(Name = "用户ID")]
        public string UserId { get; set; }

        /// <summary>
        /// 会员类别
        /// </summary>
        [Display(Name = "会员类别")]
        public SOSOshop.Model.MemberKeyValue.UserType UserType { get; set; }

        /// <summary>
        /// 买家类别
        /// </summary>
        [Display(Name = "买家类别")]
        public SOSOshop.Model.MemberKeyValue.Member_Class Member_Class { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        [Display(Name = "来源")]
        public SOSOshop.Model.MemberKeyValue.Member_Type Member_Type { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [Display(Name = "联系人")]
        public string LinkMan { get; set; }

        /// <summary>
        /// 默认单位ID
        /// </summary>
        [Display(Name = "默认单位ID")]
        public int ParentId { get; set; }

        /// <summary>
        /// 默认单位名称
        /// </summary>
        [Display(Name = "默认单位名称")]
        public string IncName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Display(Name = "手机号")]
        public string MobilePhone { get; set; }

        /// <summary>
        /// 联系邮箱
        /// </summary>
        [DataType(DataType.EmailAddress)]
        [Display(Name = "联系邮箱")]
        public string Email { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        [Display(Name = "省")]
        public int Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        [Display(Name = "市")]
        public int City { get; set; }

        /// <summary>
        /// 区.县
        /// </summary>
        [Display(Name = "区")]
        public int Borough { get; set; }

        /// <summary>
        /// 联系地址
        /// </summary>
        [Display(Name = "联系地址")]
        public string Address { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [Display(Name = "电话")]
        public string OfficePhone { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        [Display(Name = "传真")]
        public string Fax { get; set; }

        /// <summary>
        /// 手机号是否验证
        /// </summary>
        [Display(Name = "手机号是否验证")]
        public bool CheckM { get; set; }

        /// <summary>
        /// 邮箱是否验证
        /// </summary>
        [Display(Name = "邮箱是否验证")]
        public bool CheckE { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        [Display(Name = "权限")]
        public SOSOshop.Model.MemberPermission MemberPermission { get; set; }
    }
}
