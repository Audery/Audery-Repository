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
    public partial class product_list_Lock : SOSOshop.WEB.UI.ManageBasePage
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
                if (!SOSOshop.BLL.PowerPass.isPass("001030001") && !SOSOshop.BLL.PowerPass.isPass("001030002"))
                {
                    Response.End();
                }
                StartLoad(1, null);
            }
        }
        public override void SetModuleTag()
        {
            base.ModuleBrowse = "001030001";
        }
        //分页数据初始化
        protected override void StartLoad(int PageIndex, string strWhere)
        {
            bll = new SOSOshop.BLL.Order.Orders();
            int recordCount, pageCount;
            AspNetPager1.PageSize = 10;
            #region 搜索条件
            System.Text.StringBuilder sb = new StringBuilder();
            //只有查看自己的权限
            if (!SOSOshop.BLL.PowerPass.isPass("001030002"))
            {
                if (SOSOshop.BLL.PowerPass.isPass("001030001"))
                {
                    sb.AppendFormat(" AND product_id_02 IN (select p1.product_id from dbo.spzl p1 inner join dbo.spzl_jg p2 on p1.spid=p2.spid AND p1.sx1=p2.jigid AND p2.cgy='{0}')", adminInfo.AdminName);
                }
            }
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
            if (!string.IsNullOrEmpty(TextBox4.Text))
            {
               
                sb.AppendFormat(" and product_id in (SELECT ProId FROM dbo.OrderProduct a INNER JOIN dbo.Orders b ON a.OrderId = b.OrderId  WHERE AddTime>'{0}'  AND Status<6 AND  Status<>4 AND b.OrderStatus>0 AND b.OrderStatus<>4 )", TextBox4.Text);
            }
            if (!string.IsNullOrEmpty(TextBox5.Text))
            {
                
                sb.AppendFormat(" and product_id in (SELECT ProId FROM dbo.OrderProduct a INNER JOIN dbo.Orders b ON a.OrderId = b.OrderId  WHERE AddTime<'{0}'  AND Status<6 AND  Status<>4 AND b.OrderStatus>0 AND b.OrderStatus<>4 )", TextBox5.Text);
            }
            #endregion
            sb.Append(" AND Stock2>0");
            var dt = bll.GetListByPage("View_Stock_Lock", "*,ISNULL((SELECT 1 FROM DrugsBase_ZYC WHERE DrugsBase_ID=T.DrugsBase_ID),0) is_ZYC", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, "Stock2 desc", sb.ToString(), out recordCount, out pageCount);
            tablist.DataSource = dt.GetSpecification();
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

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            TextBox5.Text = DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd");
            StartLoad(1, null);
        }
    }
}