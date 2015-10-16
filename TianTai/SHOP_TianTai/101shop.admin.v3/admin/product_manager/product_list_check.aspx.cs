using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using SOSOshop.BLL.Common;

namespace _101shop.admin.v3.admin.product_manager
{
    public partial class product_list_check : SOSOshop.WEB.UI.ManageBasePage
    {
        /// <summary>
        /// 统计数据
        /// </summary>
        public int c_pcount = 0;//品种数量
        public decimal c_TotalPrice = 0;//金额
        SOSOshop.BLL.Order.Orders bll = new SOSOshop.BLL.Order.Orders();
        public SOSOshop.Model.AdminInfo adminInfo = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            adminInfo = (SOSOshop.Model.AdminInfo)SOSOshop.BLL.AdministrorManager.Get();
            if (!IsPostBack)
            {
                StartLoad(1, null);
            }
        }
        public override void SetModuleTag()
        {
            base.ModuleBrowse = "001010001";
        }
        //分页数据初始化
        protected override void StartLoad(int PageIndex, string strWhere)
        {
            bll = new SOSOshop.BLL.Order.Orders();
            int recordCount, pageCount;
            AspNetPager1.PageSize = 10;
            #region 搜索条件
            System.Text.StringBuilder sb = new StringBuilder();
            sb.Append(" and (isnull(Product_ID_02,-1)=0) AND Product_bShelves=1 AND beactive='是'");

            if (!string.IsNullOrEmpty(TextBox6.Text))
            {
                sb.AppendFormat(" and Product_Name like('%{0}%')", Library.Lang.Input.Filter(TextBox6.Text));
            }

            if (!string.IsNullOrEmpty(TextBox1.Text))
            {
                sb.AppendFormat(" and DrugsBase_Manufacturer like('%{0}%')", Library.Lang.Input.Filter(TextBox1.Text));
            }

            if (!string.IsNullOrEmpty(TextBox2.Text))
            {
                sb.AppendFormat(" and DrugsBase_ApprovalNumber like('%{0}%')", Library.Lang.Input.Filter(TextBox2.Text));
            }

            if (DropDownList2.SelectedValue == "1")
            {
                sb.Append(" and stock>0");
            }
            else if (DropDownList2.SelectedValue == "2")
            {
                sb.Append(" and stock<1");
            }

            if (DropDownList1.SelectedValue == "1")
            {
                sb.Append(" and product_id in (SELECT DISTINCT ProId FROM dbo.OrderProduct) ");
            }
            else if (DropDownList1.SelectedValue == "2")
            {
                sb.Append(" and product_id not in (SELECT DISTINCT ProId FROM dbo.OrderProduct) ");
            }

            #endregion

            var dt = bll.GetListByPage("Product", "*,(SELECT iden FROM dbo.Product_Centre WHERE product_id=T.Product_ID) iden,ISNULL((SELECT 1 FROM DrugsBase_ZYC WHERE DrugsBase_ID=T.DrugsBase_ID),0) is_ZYC", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, "product_id desc", sb.ToString(), out recordCount, out pageCount);
            tablist.DataSource = dt;
            AspNetPager1.RecordCount = recordCount;
            tablist.DataBind();
            foreach (GridViewRow item in tablist.Rows)
            {
                var Label1 = item.FindControl("Label1") as Label;
                if (Label1 != null)
                {
                    Label1.Text = Public.GetSpecificationAndS(dt.Rows[item.RowIndex]);
                }
            }
        }

        protected void Search_Click(object sender, EventArgs e)
        {

            StartLoad(1, null);
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            StartLoad(1, null);
        }
        /// <summary>
        /// 取得待建档商品处理状态
        /// </summary>
        /// <param name="Product_Id"></param>
        /// <returns></returns>
        protected string GetState(int Product_Id)
        {
            string key = "product_list_check_GetState" + Product_Id;
            Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            if (mc.KeyExists(key))
            {
                return "处理中";
            }
            else
            {
                return "待处理";
            }

        }
        /// <summary>
        /// 修改商品状态
        /// </summary>
        /// <param name="Product_Id"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static string Update(int Product_Id)
        {
            string key = "product_list_check_GetState" + Product_Id;
            Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            if (mc.KeyExists(key))
            {
                mc.Delete(key);
                return "待处理";
            }
            else
            {
                mc.Add(key, DateTime.Now);
                return "处理中";
            }

        }
    }
}