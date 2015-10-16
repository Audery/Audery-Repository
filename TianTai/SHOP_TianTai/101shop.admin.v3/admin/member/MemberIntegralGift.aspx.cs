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
    public partial class MemberIntegralGift : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region ajax 上架、下架、删除
            if (!string.IsNullOrEmpty(Request.QueryString["ajax"]))
            {
                if (SOSOshop.BLL.PowerPass.isPass("008011002"))
                {
                    string id = Request.Form["id"];
                    string Shangjia = Request.Form["Shangjia"];
                    string Del = Request.Form["Del"];
                    string s = "";
                    if (!string.IsNullOrEmpty(Shangjia) && Shangjia == "1")
                    {
                        s = string.Format("update MemberIntegralGift set [State]=(case when State=1 then 2 when State=2 then 1 else State end) where [id]={0}", id);
                    }
                    else if (!string.IsNullOrEmpty(Del) && Del == "1")
                    {
                        s = string.Format("update MemberIntegralGift set [State]=0 where [id]={0}", id);
                    }
                    try
                    {
                        SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();
                        int ret = db.ExecuteNonQuery(s);
                        if (ret > 0)
                        {
                            Response.Write("{\"state\":1,\"message\":\"处理成功！\"}");
                        }
                        else
                        {
                            Response.Write("{\"state\":0,\"message\":\"处理失败！\"}");
                        }
                    }
                    catch (Exception x)
                    {
                        Response.Write("{\"state\":-2,\"message\":\"" + x.Message + "\"}");
                    }
                }
                else
                {
                    Response.Write("{\"state\":-1,\"message\":\"对不起，您没有编辑权限，请联系管理员！\"}");
                }
                Response.End();
            }
            #endregion
            if (!this.Page.IsPostBack)
            {
                SOSOshop.BLL.PromptInfo.Popedom("008011002");
                GetList();
            }
        }

        private void GetList()
        {
            #region where
            string where = "";
            string name = Request["name"];
            string State = Request["State"];
            if (!string.IsNullOrEmpty(name))
            {
                this.TextBox_name.Text = name;
                where += " and name like '%" + name.Replace("'", "''").Replace("%", "") + "%' ";
            }
            if (!string.IsNullOrEmpty(State))
            {
                this.DropDownList_State.SelectedValue = State;
                where += " and State = '" + State.Replace("'", "''") + "' ";
            }
            #endregion

            SOSOshop.BLL.Integral.MemberIntegralGift bll = new SOSOshop.BLL.Integral.MemberIntegralGift();
            int pageindex = 1; int.TryParse(Request["current"], out pageindex);
            int pagesize = 15;
            string sort = Request["sort"];//排序
            if (string.IsNullOrEmpty(sort) || (sort.Contains("asc") == false && sort.Contains("desc") == false)) sort = "id DESC";
            Repeater1.DataSource = bll.GetList(where, sort, pageindex, pagesize);
            Repeater1.DataBind();
            int recordcount = bll.GetListCount(where);
            page(recordcount, pageindex, pagesize);//分页
        }

        public void page(int recordcount, int pageindex, int pagesize)
        {
            string param = "&name=" + Request["name"] + "&State=" + Request["State"];

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
