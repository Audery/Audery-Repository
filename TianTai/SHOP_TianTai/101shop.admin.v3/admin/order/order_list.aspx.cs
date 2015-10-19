using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SOSOshop.Model;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using SOSOshop.BLL;
namespace _101shop.admin.v3.admin.order
{
    public partial class OrderList : SOSOshop.WEB.UI.ManageBasePage
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
                //选中订单状态过滤
                if (!string.IsNullOrEmpty(Request.QueryString["OrderStatus"]))
                {
                    DropDownListStatus.SelectedValue = Request.QueryString["OrderStatus"];
                }
                if (!SOSOshop.BLL.PowerPass.isPass("005004001") && !SOSOshop.BLL.PowerPass.isPass("005004005"))
                {
                    SOSOshop.BLL.PromptInfo.Popedom("000000000000", "对不起，您没有查看的权限!");
                }
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    StringBuilder sb = new StringBuilder();
                    if (!string.IsNullOrEmpty(Request.QueryString["jd"]))
                    {
                        string sql = "SELECT count(1) FROM OrderProduct p WHERE OrderId='" + Library.Lang.Input.Filter(Request.QueryString["id"]) + "'";
                        sql += " and isnull((select Product_ID_02 from Product where Product_ID=p.ProId),-1)=0";
                        sb.Append(bll.ExecuteScalar(sql));
                    }
                    else
                    {
                        string sql = "SELECT ProName,isnull((select Product_ID_02 from Product where Product_ID=p.ProId),-1)spid";
                        sql += " FROM OrderProduct p WHERE OrderId='" + Library.Lang.Input.Filter(Request.QueryString["id"]) + "'";
                        using (var dr = bll.ExecuteReader(sql))
                        {
                            int i = 1;
                            while (dr.Read())
                            {
                                sb.AppendFormat("<span style=\"line-height:180%; font-size:12px; white-space: nowrap;\">商品名:{0}、{1}</span><br>", i, dr.GetString(0));
                                i++;
                            }
                        }
                    }
                    Response.Write(sb.ToString());
                    Response.End();
                }
                if (!string.IsNullOrEmpty(Request.QueryString["ProName"]))
                {
                    TextBox3.Text = Request.QueryString["ProName"];
                }

                if (!IsPostBack)
                {
                    DropDownList1.DataSource = SOSOshop.Model.CompanyClass.GetList();
                    DropDownList1.DataValueField = "CompanyClassName";
                    DropDownList1.DataTextField = "CompanyClassName";
                    DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, new ListItem() { Text = "全部", Value = "0" });
                }


                StartLoad(1, null);
            }
        }
        public override void SetModuleTag()
        {
            base.ModuleBrowse = "005004001";
        }
        //分页数据初始化
        protected override void StartLoad(int PageIndex, string strWhere)
        {
            if (!IsPostBack)
            {
                SelectEditer();
                //BindOutSellPerson();
            }
            bll = new SOSOshop.BLL.Order.Orders();
            int recordCount, pageCount;
            AspNetPager1.PageSize = 10;
            #region 搜索条件
            System.Text.StringBuilder sb = new StringBuilder();


            SOSOshop.Model.AdminInfo aInfo = null;
            aInfo = SOSOshop.BLL.AdministrorManager.Get();

            if (!SOSOshop.BLL.PowerPass.isPass("005004001") && SOSOshop.BLL.PowerPass.isPass("005004005"))
            {
                //外销按地区
                if (bll.ExecuteScalar("SELECT role FROM dbo.yxs_administrators WHERE adminid=" + UserId).ToString().Contains("60"))
                {
                    sb.AppendFormat(" and ReceiverId IN (SELECT UID FROM dbo.memberinfo WHERE Borough IN (SELECT ResponseCounty FROM ResponseRegionsOfOutSellPerson WHERE PersonID={0})) ", aInfo.AdminId);
                }
                else
                {
                    sb.AppendFormat(" and ReceiverId IN (SELECT uid FROM dbo.memberinfo WHERE Editer={0} or OSPId={0}) ", aInfo.AdminId);
                }

            }
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
            //买家类型
            if (DropDownList1.SelectedValue != "0")
            {
                sb.AppendFormat(" and ReceiverId in (SELECT UID FROM dbo.memberaccount WHERE CompanyClass='{0}')", DropDownList1.SelectedValue);
            }
            //品种
            if (!string.IsNullOrEmpty(TextBox3.Text))
            {
                sb.AppendFormat(" and orderid in (SELECT OrderId FROM OrderProduct WHERE ProName LIKE('%{0}%'))", TextBox3.Text);
            }
            //付款方式
            if (DropDownList2.SelectedValue != "0")
            {
                sb.AppendFormat(" and Payment={0}", DropDownList2.SelectedValue);
            }
            //订单状态
            if (DropDownListStatus.SelectedValue != "0")
            {
                //已经支付也算成待审核
                if (DropDownListStatus.SelectedValue == "1")
                {
                    sb.Append(" and (OrderStatus=3 or OrderStatus=1)");
                }
                else
                {
                    sb.AppendFormat(" and OrderStatus={0}", DropDownListStatus.SelectedValue);
                }
            }
            if (!Library.Lang.DataValidator.isNULL(TextBox4.Text, TextBox5.Text))
            {
                sb.AppendFormat(" and (ShopDate>'{0}' and ShopDate<'{1}')", TextBox4.Text, DateTime.Parse(TextBox5.Text).AddHours(24));
            }
            //交易员
            if (ddlEditer.SelectedIndex > 0)
            {
                sb.AppendFormat(" and EXISTS(SELECT * FROM memberinfo WHERE UID=t.ReceiverId and Editer=" + ddlEditer.SelectedValue + ")");
            }
            //生产厂家
            if (!string.IsNullOrEmpty(txtManufacturer.Text.Trim()))
            {
                sb.AppendFormat(" AND orderid IN (SELECT OrderId FROM dbo.OrderProduct a INNER JOIN dbo.Product b ON a.ProId=b.Product_ID WHERE b.DrugsBase_Manufacturer LIKE('%{0}%'))  ", txtManufacturer.Text.Trim());
            }
            //批准文号
            if (!string.IsNullOrEmpty(txtApprovalNumber.Text.Trim()))
            {
                sb.AppendFormat(" AND orderid IN (SELECT OrderId FROM dbo.OrderProduct a INNER JOIN dbo.Product b ON a.ProId=b.Product_ID WHERE b.DrugsBase_ApprovalNumber LIKE('%{0}%')) ", txtApprovalNumber.Text.Trim());
            }
            #endregion
            var dt = bll.GetListByPage("Orders", @"*,
                                                  (SELECT COUNT(1) FROM OrderProduct WHERE OrderId=t.orderid) pcount,
                                                  (SELECT TOP 1 ProName FROM OrderProduct WHERE OrderId=t.orderid )ProName,
                                                  isnull((SELECT IsSpecialTrade FROM memberpermission WHERE UID=t.ReceiverId),0)IsSpecialTrade,
                                                  (SELECT name FROM yxs_administrators as a INNER JOIN memberinfo as m ON a.adminid=m.Editer WHERE m.UID=t.ReceiverId)adminname",
                                      AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, "ShopDate desc", sb.ToString(), out recordCount, out pageCount);
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
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                #region 是否符合功能处理全新

                if ((int)dt.Rows[i]["OrderStatus"] != 1 && (int)dt.Rows[i]["OrderStatus"] != 3)//订单状态如果不等于已提交则不能取消
                {
                    tablist.Rows[i].FindControl("LinkButton1").Visible = false;
                }
                //先隐藏审核按钮
                tablist.Rows[i].FindControl("LinkButton2").Visible = false;
                //显示确认支付按钮（1订单状态未取消，未确认支付，审核,和是货到付款的订单）
                if ((int)dt.Rows[i]["OrderStatus"] < 3 && (int)dt.Rows[i]["OrderStatus"] != 2 && (int)dt.Rows[i]["OrderStatus"] > 0 && (int)dt.Rows[i]["Payment"] == 2)
                {
                    tablist.Rows[i].FindControl("LinkButton2").Visible = true;
                }
                //订单已被拆分
                if ((int)dt.Rows[i]["OrderType"] == 0)
                {
                    tablist.Rows[i].FindControl("LinkButton1").Visible = false;
                    tablist.Rows[i].FindControl("LinkButton2").Visible = false;
                }
                #endregion
                #region 确认是否有权限
                if (!SOSOshop.BLL.PowerPass.isPass("005004002"))//是否有取消订单的权限
                {
                    tablist.Rows[i].FindControl("LinkButton1").Visible = false;
                }
                if (!SOSOshop.BLL.PowerPass.isPass("005004003"))//是否有确认支付的权限
                {
                    tablist.Rows[i].FindControl("LinkButton2").Visible = false;
                }
                //是快捷交易的订单不能处理
                if (1 == int.Parse(dt.Rows[i]["IsSpecialTrade"].ToString()))
                {
                    //tablist.Rows[i].FindControl("LinkButton1").Visible = false;
                    tablist.Rows[i].FindControl("LinkButton2").Visible = false;
                }
                #endregion
            }
        }

        /// <summary>
        /// 查询交易员
        /// </summary>
        protected void SelectEditer()
        {
            var dt= bll.ExecuteTable("select adminid id,name from yxs_administrators");
            ddlEditer.DataSource = dt;
            ddlEditer.DataBind();
            ListItem li = new ListItem("全部人员", "");
            ddlEditer.Items.Insert(0, li);

          
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
        /// 取消订单
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        protected void cancelOrder(object src, EventArgs e)
        {
            string orderid = ((LinkButton)src).CommandArgument;
            bll.CancelOrder(orderid);
            LogAdd("取消了订单：{0}", ((LinkButton)src).CommandArgument);
            StartLoad(1, null);
        }
        /// <summary>
        /// 确认支付
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        protected void ConfirmOrder(object src, EventArgs e)
        {
            string orderid = ((LinkButton)src).CommandArgument;
            if (!bll.isDispose(orderid))
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "no", "alert('下单时间须超过十分钟才能处理！');", true);
                return;
            }
            var dt = bll.ExecuteTable("SELECT OrderStatus,Payment FROM dbo.Orders WHERE OrderId='{0}'", orderid);
            int i = 0;
            if ((int)dt.Rows[i]["OrderStatus"] < 3 && (int)dt.Rows[i]["OrderStatus"] != 2 && (int)dt.Rows[i]["OrderStatus"] > 0 && (int)dt.Rows[i]["Payment"] == 2)
            {
                bll.ExecuteNonQuery(string.Format("UPDATE dbo.Orders SET OrderStatus=3,PaymentStatus=1,FinancialCheckDate=GETDATE() WHERE OrderId='{0}'", orderid));
                bll.SplitOrder(orderid, true);
                LogAdd("订单确认了支付：{0}", orderid);
                StartLoad(1, null);
            }
            else
            {
                ShowError("订单状态已被变更！");
            }
        }

        //日期选择
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            switch (lb.Text)
            {
                case "今日":
                    {
                        TextBox4.Text = DateTime.Now.ToString("yyyy-MM-dd");
                        TextBox5.Text = DateTime.Now.ToString("yyyy-MM-dd");
                        break;
                    }
                case "昨日":
                    {
                        TextBox4.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                        TextBox5.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                        break;
                    }
                case "最近7天":
                    {
                        TextBox4.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                        TextBox5.Text = DateTime.Now.ToString("yyyy-MM-dd");
                        break;
                    }
                case "最近30天":
                    {
                        TextBox4.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                        TextBox5.Text = DateTime.Now.ToString("yyyy-MM-dd");
                        break;
                    }
                case "本月":
                    {
                        TextBox4.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
                        TextBox5.Text = DateTime.Now.ToString("yyyy-MM-dd");
                        break;
                    }
                case "上月":
                    {
                        TextBox4.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM") + "-01";
                        TextBox5.Text = DateTime.Parse(DateTime.Now.ToString("yyyy-MM") + "-01").AddDays(-1).ToString("yyyy-MM-dd");
                        break;
                    }
                case "全部":
                    {
                        TextBox4.Text = "";
                        TextBox5.Text = "";
                        break;
                    }
            }
            StartLoad(1, null);
        }
    }
}