using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _101shop.admin.v3.admin.order
{
    public partial class orderEdit : SOSOshop.WEB.UI.ManageBasePage
    {
        public SOSOshop.Model.Order.Orders model
        {
            get { return ViewState["model"] as SOSOshop.Model.Order.Orders; }
            set { ViewState["model"] = value; }
        }
        /// <summary>
        /// 是否有快捷交易的权限，有此权限的买家的订单不能处理
        /// </summary>
        public bool IsSpecialTrade
        {
            get { return (bool)ViewState["IsSpecialTrade"]; }
            set { ViewState["IsSpecialTrade"] = value; }
        }
        /// <summary>
        /// 是否有快捷交易的权限，有此权限的买家的订单不能处理
        /// </summary>
        public bool IsBuyFilingStatus
        {
            get { return (bool)ViewState["IsBuyFilingStatus"]; }
            set { ViewState["IsBuyFilingStatus"] = value; }
        }
        public SOSOshop.BLL.Order.Orders bll = new SOSOshop.BLL.Order.Orders();
        public SOSOshop.BLL.Order.OrderProduct bllp = new SOSOshop.BLL.Order.OrderProduct();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!SOSOshop.BLL.PowerPass.isPass("005004001") && !SOSOshop.BLL.PowerPass.isPass("005004005"))
                {
                    SOSOshop.BLL.PromptInfo.Popedom("000000000000", "对不起，您没有查看的权限!");
                }
                model = bll.GetModel(int.Parse(Request.QueryString["id"]));
                var Permission = new SOSOshop.BLL.MemberPermission().GetModel(model.ReceiverId);
                IsBuyFilingStatus = Permission.IsBuyFilingStatus;
                IsSpecialTrade = Permission.IsSpecialTrade;

                BindList();
            }
        }
        protected void BindList()
        {
            Repeater1.DataSource = bllp.GetList(model.OrderId);
            Repeater1.DataBind();
        }
        /// <summary>
        /// 是否建档
        /// </summary>
        /// <param name="spid"></param>
        /// <returns></returns>
        public string jdstate(object Product_ID_02)
        {
            if (!Library.Lang.DataValidator.isNumber(Product_ID_02)) return "";
            if ((int)Product_ID_02 == 0)
            {
                return "<span style='color:red'>[待建档]</span>";
            }
            return "";
        }
        public override void SetModuleTag()
        {
            base.ModuleBrowse = "005004001";
        }
        protected void affirmOrder(object sender, EventArgs e)
        {
            if (!bll.isDispose(model.OrderId))
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "no", "$.jBox.alert('下单时间须超过十分钟才能处理！', '提示');", true);
                return;
            }
            LinkButton lb = (LinkButton)sender;
            string[] p = lb.CommandArgument.Split(':');            
            //删除商品
            if (p[1] == "0")
            {
                bll.ExecuteNonQuery(string.Format("DELETE dbo.OrderProduct WHERE Id={0};UPDATE dbo.Orders SET TotalPrice=(SELECT SUM(ProPrice*ProNum) FROM OrderProduct WHERE OrderId='{1}') WHERE OrderId='{1}'", p[0],model.OrderId));
                LogAdd("将订单：{0} 下面的商品编号: {1}删除了", model.OrderId, p[0], lb.Text);
                BindList();
                return;
            }
            if (p[1] == "3")
            {
                bllp.UpdateShop(p[0], model.OrderId, 4);
            }
            else
            {
                bllp.UpdateShop(p[0], model.OrderId, int.Parse(p[1]));
            }
            LogAdd("将订单：{0} 下面的商品编号: {1}设置为:{2}", model.OrderId, p[0], lb.Text);
            //选择了缺货，写入货周期
            if (p[1] == "3")
            {
                bll.ExecuteNonQuery(string.Format("INSERT INTO OrderProduct_Stockout values({0},'{1}')", p[0], lb.Text));
            }
            //选择了缺货，写入缺货通知
            if (p[1] == "5")
            {
                SOSOshop.BLL.Stockout bllst = new SOSOshop.BLL.Stockout();
                SOSOshop.Model.Stockout modelst = new SOSOshop.Model.Stockout();
                System.Data.DataTable dt = bll.ExecuteTable("SELECT ProId,ProNum FROM dbo.OrderProduct WHERE Id={0}", p[0]);
                modelst.Product_ID = (int)dt.Rows[0][0];
                modelst.Num = (int)dt.Rows[0][1];
                modelst.UID = model.ReceiverId;
                modelst.created = DateTime.Now;
                bllst.Add(modelst);
            }
            BindList();
        }
        /// <summary>
        /// 取得订单状态可否确认供货
        /// </summary>
        /// <returns></returns>
        public bool GetPaymentAndPaymentStatus()
        {
            if (IsSpecialTrade)
            {
                return false;
            }
            if (!IsBuyFilingStatus)
            {
                return false;
            }
            return (model.Payment != 2 || model.PaymentStatus == 1);
        }
        //取得是否可以手动操作订单
        public string GetOperation(int Status)
        {
            //判决是否是此交易员
            if (0 == (int)bll.ExecuteScalar(string.Format("SELECT COUNT(*) FROM dbo.memberinfo WHERE UID={1} AND Editer={0}", SOSOshop.BLL.AdministrorManager.Get().AdminId, model.ReceiverId)))
            {
                return "display:none";
            }

            if (!SOSOshop.BLL.PowerPass.isPass("005004006"))
            {
                return "display:none";
            }
            if (model.ShopDate > DateTime.Now.AddDays(-3))
            {
                return "display:none";
            }
            if (model.OrderStatus < 0 || model.OrderStatus == 4)
            {
                return "display:none";
            }
            if ((Status == 6 || Status > 7) && Status != 11)
            {
                return "display:none";
            }

            return "display:block";
        }
        /// <summary>
        /// 手动完成订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SetOperation(object sender, EventArgs e)
        {

            LinkButton lb = (LinkButton)sender;
            string[] p = lb.CommandArgument.Split(':');
            bllp.UpdateShopProd(p[0], model.OrderId, int.Parse(p[1]));
            model = bll.GetModel(int.Parse(Request.QueryString["id"]));
            LogAdd("将订单：{0} 下面的商品编号: {1}手动设置为:{2}", model.OrderId, p[0], p[0] == "8" ? "完成" : "取消");
            BindList();
        }
    }
}