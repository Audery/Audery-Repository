using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace SOSOshop.Model
{
    /// <summary>
    /// 网站所有列表参数
    /// </summary>
    public class KeyValue
    {
        /// <summary>
        /// 取得企业库中的企业性质
        /// </summary>
        /// <returns></returns>
        public static List<ListItem> GetNature()
        {
            List<ListItem> li = new List<ListItem>();
            li.Add(new ListItem() { Text = "---请选择---", Value = "0" });
            li.Add(new ListItem() { Text = "政府机关/事业单位", Value = "1" });
            li.Add(new ListItem() { Text = "国营", Value = "2" });
            li.Add(new ListItem() { Text = "私营", Value = "3" });
            li.Add(new ListItem() { Text = "中外合资", Value = "4" });
            li.Add(new ListItem() { Text = "外资", Value = "5" });
            li.Add(new ListItem() { Text = "其他", Value = "6" });
            return li;
        }
        /// <summary>
        /// 取得企业库中的状态
        /// </summary>
        /// <returns></returns>
        public static List<ListItem> GetDrugsBase_EnterpriseStatus()
        {
            List<ListItem> li = new List<ListItem>();
            li.Add(new ListItem() { Text = "未审核", Value = "1" });
            li.Add(new ListItem() { Text = "已审核", Value = "0" });
            li.Add(new ListItem() { Text = "已冻结", Value = "2" });
            return li;
        }


        /// <summary>
        /// 取得企业库中的买家和卖家建档状态卖家(供应商)建档状态 1,资料完备 2资料不完备 3 不完备可交易
        /// </summary>
        /// <returns></returns>
        public static List<ListItem> GetDrugsBase_EnterpriseSellFilingStatus()
        {
            List<ListItem> li = new List<ListItem>();
            li.Add(new ListItem() { Text = "资料不完备", Value = "2" });
            li.Add(new ListItem() { Text = "资料完备", Value = "1" });
            li.Add(new ListItem() { Text = "不完备可交易", Value = "3" });
            return li;
        }
        /// <summary>
        /// 取得企业库中的卖家类型(生产企业1,经营企业2)
        /// </summary>
        /// <returns></returns>
        public static List<ListItem> GetDrugsBase_EnterpriseSellType()
        {
            List<ListItem> li = new List<ListItem>();
            li.Add(new ListItem() { Text = "生产企业", Value = "1" });
            li.Add(new ListItem() { Text = "经营企业", Value = "2" });
            return li;
        }
        /// <summary>
        /// 取得企业库中的买家类型(医疗机构3,单体药店4,连锁药店5,诊所6,其他7)
        /// </summary>
        /// <returns></returns>
        public static List<ListItem> GetDrugsBase_EnterpriseBuyType()
        {
            List<ListItem> li = new List<ListItem>();
            li.Add(new ListItem() { Text = "医疗机构", Value = "3" });
            //li.Add(new ListItem() { Text = "", Value = "4" });
            li.Add(new ListItem() { Text = "单体药店/连锁药店", Value = "5" });
            li.Add(new ListItem() { Text = "批发企业", Value = "6" });
            li.Add(new ListItem() { Text = "其他", Value = "7" });
            return li;
        }
        /// <summary>
        /// 取得所有资质
        /// </summary>
        /// <returns></returns>
        public static List<KeyValueItem> GetQualificationsList()
        {
            List<KeyValueItem> li = new List<KeyValueItem>();
            li.Add(new KeyValueItem() { Name = "营业执照", Value = "1", Contain = ",2,3,4,5,7," });
            li.Add(new KeyValueItem() { Name = "组织机构代码证", Value = "2", Contain = ",2,3,5," });
            li.Add(new KeyValueItem() { Name = "药品经营许可证", Value = "3", Contain = ",2,3,5," });
            li.Add(new KeyValueItem() { Name = "GSP证书", Value = "4", Contain = ",2,3,5," });
            li.Add(new KeyValueItem() { Name = "税务登记证", Value = "5", Contain = ",2,3,5," });
            li.Add(new KeyValueItem() { Name = "医疗机构执业许可证", Value = "6", Contain = ",4,7," });
            li.Add(new KeyValueItem() { Name = "采购委托书+被委托人身份证复印件", Value = "9", Contain = ",2,3,5,7," });
            li.Add(new KeyValueItem() { Name = "开户行许可证", Value = "10", Contain = ",5," });
            li.Add(new KeyValueItem() { Name = "联系人身份证复印件（正反面）", Value = "11", Contain = ",5," });
            li.Add(new KeyValueItem() { Name = "开票信息", Value = "14", Contain = ",5," });
            return li;
        }

        /// <summary>
        /// 取得所有客户类型
        /// </summary>
        /// <returns></returns>
        public static List<KeyValueItem> GetMemberTypeList()
        {
            List<KeyValueItem> li = new List<KeyValueItem>();
            li.Add(new KeyValueItem() { Name = "公立医院", Value = "1" });
            li.Add(new KeyValueItem() { Name = "零售连锁", Value = "2" });
            li.Add(new KeyValueItem() { Name = "单体/加盟药店", Value = "3" });
            li.Add(new KeyValueItem() { Name = "民营医院", Value = "4" });
            li.Add(new KeyValueItem() { Name = "商业公司", Value = "5" });
            li.Add(new KeyValueItem() { Name = "生产企业", Value = "6" });
            li.Add(new KeyValueItem() { Name = "诊所", Value = "7" });
            return li;
        }
    }
}
