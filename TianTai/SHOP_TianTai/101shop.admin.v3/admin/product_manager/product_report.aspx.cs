using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using SOSOshop.BLL;

namespace _101shop.admin.v3.admin.product_manager
{
    public partial class product_report : SOSOshop.WEB.UI.ManageBasePage
    {
        SOSOshop.BLL.Product.Product bll = new SOSOshop.BLL.Product.Product();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StartLoad(1, null);
            }
        }
        public override void SetModuleTag()
        {
            base.ModuleBrowse = "001020001";
        }
        //分页数据初始化
        protected override void StartLoad(int PageIndex, string strWhere)
        {
            int recordCount, pageCount;
            AspNetPager1.PageSize = 10;
            System.Text.StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(TextBox1.Text))
            {
                sb.AppendFormat(" and Product_Name like('%{0}%')", Library.Lang.Input.Filter(TextBox1.Text));
            }
            if (!string.IsNullOrEmpty(TextBox2.Text))
            {
                sb.AppendFormat(" and DrugsBase_Manufacturer like('%{0}%')", Library.Lang.Input.Filter(TextBox2.Text));
            }
            if (!string.IsNullOrEmpty(TextBox3.Text))
            {
                sb.AppendFormat(" and DrugsBase_ApprovalNumber like('%{0}%')", Library.Lang.Input.Filter(TextBox3.Text));
            }
            if (DropDownList1.SelectedValue == "1")
            {
                sb.Append(" and Product_ID in (SELECT Product_ID FROM dbo.product_online_v)");
            }
            else if (DropDownList1.SelectedValue == "2")
            {
                sb.Append(" and Product_ID not in (SELECT Product_ID FROM dbo.product_online_v)");
            }

            SOSOshop.BLL.Report.DrugTestingReport blldtr = new SOSOshop.BLL.Report.DrugTestingReport();
            if (DropDownList2.SelectedValue == "1")
            {
                string ids = blldtr.GetProducts_Id();
                if (ids == "") ids = "-1";
                sb.AppendFormat(" and Product_ID in ({0})", ids);
            }
            else if (DropDownList2.SelectedValue == "2")
            {
                string ids = blldtr.GetProducts_Id();
                if (ids == "") ids = "-1";
                sb.AppendFormat(" and Product_ID not in ({0})", ids);
            }
            var dt = bll.GetListByPage("Product", "*,0 is_ZYC", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, "Product_id desc", sb.ToString(), out recordCount, out pageCount);
            tablist.DataSource = dt;
            AspNetPager1.RecordCount = recordCount;
            tablist.DataBind();

        }


        protected void Search_Click(object sender, EventArgs e)
        {

            StartLoad(1, null);
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            StartLoad(1, null);
        }
    }
}