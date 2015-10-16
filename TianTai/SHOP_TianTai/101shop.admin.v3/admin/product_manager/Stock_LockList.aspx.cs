using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SOSOshop.BLL;
using System.Text;

namespace _101shop.admin.v3.admin.product_manager
{
    public partial class Stock_LockList : SOSOshop.WEB.UI.ManageBasePage
    {
        /// <summary>
        /// 统计数据
        /// </summary>
        public int c_pcount = 0;//品种数量
        public decimal c_TotalPrice = 0;//金额

        SOSOshop.BLL.Order.Orders bll = new SOSOshop.BLL.Order.Orders();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                StartLoad(1, null);
            }
        }

        //分页数据初始化
        protected override void StartLoad(int PageIndex, string strWhere)
        {
            int recordCount, pageCount;
            AspNetPager1.PageSize = 10;
            #region 搜索条件
            System.Text.StringBuilder sb = new StringBuilder();
            SOSOshop.Model.AdminInfo aInfo = null;
            aInfo = SOSOshop.BLL.AdministrorManager.Get();

            //不看原始订单
            sb.Append(" and OrderType<>0 ");
            //不看系统还未来得及分单的订单
            sb.Append(" and OrderId NOT IN (SELECT OrderId FROM dbo.OrdersMQ WHERE  state<>1)");

            //if (!SOSOshop.BLL.PowerPass.isPass("005004001") && SOSOshop.BLL.PowerPass.isPass("005004005"))
            //{
            //    //外销按地区
            //    if (bll.ExecuteScalar("SELECT role FROM dbo.yxs_administrators WHERE adminid=" + UserId).ToString().Contains("60"))
            //    {
            //        sb.AppendFormat(" and ReceiverId IN (SELECT UID FROM dbo.memberinfo WHERE Borough IN (SELECT ResponseCounty FROM ResponseRegionsOfOutSellPerson WHERE PersonID={0})) ", aInfo.AdminId);
            //    }
            //    else
            //    {
            //        sb.AppendFormat(" and ReceiverId IN (SELECT uid FROM dbo.memberinfo WHERE Editer={0} or OSPId={0}) ", aInfo.AdminId);
            //    }

            //}
            //订单号
            string OrderId = TextBox6.Text;
            if (!string.IsNullOrEmpty(Request.QueryString["OrderId"]))
            {
                OrderId = Request.QueryString["OrderId"];
                TextBox6.Text = OrderId;
            }
            if (!string.IsNullOrEmpty(OrderId))
            {
                string[] OrderIds = OrderId.Split(',');
                if (OrderIds.Length > 1)
                {
                    sb.Append(" and OrderId IN ('" + string.Join("','", OrderIds) + "')");
                }
                else
                {
                    sb.AppendFormat(" and OrderId like('%{0}%')", OrderId);
                }
            }
            //买家姓名
            if (!string.IsNullOrEmpty(TextBox1.Text))
            {
                sb.AppendFormat(" and UserName like('%{0}%')", TextBox1.Text);
            }
            //买家单位
            if (!string.IsNullOrEmpty(TextBox2.Text))
            {
                sb.AppendFormat(" and parentCorpName like('%{0}%')", TextBox2.Text);
            }
            #endregion
            //只查未完成的订单
            sb.Append(" and OrderStatus>0 AND OrderStatus<>4");
            //只查传了订单编号进来的商品
            sb.AppendFormat(" and OrderId in (SELECT OrderId  FROM dbo.OrderProduct WHERE ProId={0} and Status in (1,2,11))", Request.QueryString["id"]);
            var dt = bll.GetListByPage("Orders", "*,(SELECT ProNum FROM OrderProduct WHERE OrderId=t.orderid and ProId=" + Request.QueryString["id"] + ") pcount,(SELECT TOP 1 ProName FROM OrderProduct WHERE OrderId=t.orderid and ProId=" + Request.QueryString["id"] + " )ProName,isnull((SELECT IsSpecialTrade FROM memberpermission WHERE UID=t.ReceiverId),0)IsSpecialTrade,(SELECT name FROM yxs_administrators as a INNER JOIN memberinfo as m ON a.adminid=m.Editer WHERE m.UID=t.ReceiverId)adminname", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, "ShopDate desc", sb.ToString(), out recordCount, out pageCount);
            tablist.DataSource = dt;
            AspNetPager1.RecordCount = recordCount;
            tablist.DataBind();
            //统计数据
            if (recordCount > 0)
            {
                string sql = "select OrderId,TotalPrice into #T038 from Orders t where 1=1 " + sb + " SELECT (SELECT COUNT(DISTINCT ProId) FROM OrderProduct WHERE OrderId IN (select OrderId from #T038)) c_pcount, (SELECT sum(TotalPrice) from #T038) c_TotalPrice DROP TABLE #T038";
                DataSet ds = bll.ExecuteDataSet(sql);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    c_pcount = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                    c_TotalPrice = decimal.Parse(ds.Tables[0].Rows[0][1].ToString());
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


        public override void SetModuleTag()
        {

        }
    }
}