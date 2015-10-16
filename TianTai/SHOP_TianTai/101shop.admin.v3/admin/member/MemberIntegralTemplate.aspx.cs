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
    public partial class MemberIntegralTemplate : System.Web.UI.Page
    {
        SOSOshop.BLL.Integral.OtcIntegralDay bllo = new SOSOshop.BLL.Integral.OtcIntegralDay();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region ajax 修改积分
            if (!string.IsNullOrEmpty(Request.QueryString["ajax"]))
            {
                if (SOSOshop.BLL.PowerPass.isPass("008011001"))
                {
                    string id = Request.Form["id"];
                    string integral = Request.Form["integral"];
                    string s = string.Format("update MemberIntegralTemplate set [integral]={0} where [id]={1}", integral, id);
                    try
                    {
                        SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();
                        int ret = db.ExecuteNonQuery(s);
                        if (ret > 0)
                        {
                            Response.Write("{\"state\":1,\"message\":\"更新成功！\"}");
                        }
                        else
                        {
                            Response.Write("{\"state\":0,\"message\":\"更新失败！\"}");
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

            //修改积分倍数
            if (!string.IsNullOrEmpty(Request.Form["multiple"]))
            {
                bllo.update(Request.Form["id"], int.Parse(Request.Form["multiple"]));
                Response.End();
            }

            if (!this.Page.IsPostBack)
            {
                SOSOshop.BLL.PromptInfo.Popedom("008011001");
                GetList();
            }
        }

        private void GetList()
        {
            SOSOshop.BLL.Integral.MemberIntegralTemplate bll = new SOSOshop.BLL.Integral.MemberIntegralTemplate();
            Repeater1.DataSource = bll.GetList();
            Repeater1.DataBind();
            bll = null;

            Repeater2.DataSource = bllo.GetList();
            Repeater2.DataBind();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

            GetList();
        }

        public string getname(string name)
        {
            if (name == "成交订单")
            {
                return "成交订单(限 批发/连锁)";
            }
            return name;
        }
    }
}
