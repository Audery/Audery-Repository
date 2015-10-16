using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;


namespace _101shop.admin.v3.member
{
    public partial class MemberIntegralGiftExchange : System.Web.UI.Page
    {
        /// <summary>
        /// 兑换礼品处理
        /// </summary>
        public bool isPass = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                SOSOshop.BLL.PromptInfo.Popedom("008011004");
                isPass = SOSOshop.BLL.PowerPass.isPass("008011014");
                GetList();
            }
        }

        private void GetList()
        {
            #region where
            string where = "";
            string truename = Request["truename"];
            if (!string.IsNullOrEmpty(truename))
            {
                this.TextBox_truename.Text = truename;
                where += " and truename like '%" + truename.Replace("'", "''").Replace("%", "") + "%' ";
            }
            string CompanyName = Request["CompanyName"];
            if (!string.IsNullOrEmpty(CompanyName))
            {
                this.TextBox_CompanyName.Text = CompanyName;
                where += " and CompanyName like '%" + CompanyName.Replace("'", "''").Replace("%", "") + "%' ";
            }
            string GiftName = Request["GiftName"];
            if (!string.IsNullOrEmpty(GiftName))
            {
                this.TextBox_GiftName.Text = GiftName;
                where += " and GiftName like '%" + GiftName.Replace("'", "''").Replace("%", "") + "%' ";
            }
            string phone = Request["phone"];
            if (!string.IsNullOrEmpty(phone))
            {
                this.TextBox_phone.Text = phone;
                where += " and phone like '%" + phone.Replace("'", "''").Replace("%", "") + "%' ";
            }
            string State = Request["State"];
            if (!string.IsNullOrEmpty(State))
            {
                this.DropDownList_State.SelectedValue = State;
                where += " and State = '" + State.Replace("'", "''") + "' ";
            }
            #endregion

            SOSOshop.BLL.Integral.MemberIntegralGiftExchange bll = new SOSOshop.BLL.Integral.MemberIntegralGiftExchange();
            int pageindex = 1; int.TryParse(Request["current"], out pageindex);
            int pagesize = 15;
            string sort = Request["sort"];//排序
            if (string.IsNullOrEmpty(sort) || (sort.Contains("asc") == false && sort.Contains("desc") == false)) sort = "truename ASC";
            Repeater1.DataSource = bll.GetList(where, sort, pageindex, pagesize);
            Repeater1.DataBind();
            int recordcount = bll.GetListCount(where);
            page(recordcount, pageindex, pagesize);//分页
        }

        public void page(int recordcount, int pageindex, int pagesize)
        {
            string param = "&truename=" + Request["truename"] + "&CompanyName=" + Request["CompanyName"] + "&GiftName=" + Request["GiftName"] + "&phone=" + Request["phone"] + "&State=" + Request["State"];

            double cs = (int)recordcount / pagesize;
            //页总数
            int pagecount = (recordcount % pagesize == 0 ? 0 : 1) + int.Parse(Math.Floor(cs).ToString());
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.Append("共<span style='color: Red'>" + recordcount + "</span>条记录");
            s.Append("<a href=\"?current=1" + param + "\">");
            s.Append("<<");
            s.Append("</a> ");
            int j, i;
            j = i = 0;
            if (pageindex > 5)
            {
                i = pageindex - 5;
                j = i;
            }
            for (; i < j + 9 && i < pagecount; i++)
            {
                s.Append("<a href=\"?current=" + (i + 1) + param +
                    (pageindex == i + 1 ? "\" style=\"color:Red" : "")
                    + "\">");
                s.Append(i + 1);
                s.Append("</a> ");
            }
            s.Append("<a href=\"?current=" + pagecount + param + "\">");
            s.Append(">>");
            s.Append("</a> ");

            pages.Text = s.ToString();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            GetList();
        }
    }
}
