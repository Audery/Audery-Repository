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

namespace _101shop.admin.v3.systeminfo
{
    public partial class sms_setting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                new ChangeHope.WebPage.Sms();
                SOSOshop.BLL.PromptInfo.Popedom("012011007");
            }
            else
            {
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SOSOshop.BLL.PromptInfo.Popedom("012011007", "对不起，您没有权限进行修改");
            try
            {
                HttpContext.Current.Application[ChangeHope.WebPage.Sms.ConfigPrefix] = null;
                HttpContext.Current.Application.Lock();
                foreach (string input in Request.Form.Keys)
                {
                    if (HttpContext.Current.Application.AllKeys.Contains(ChangeHope.WebPage.Sms.ConfigPrefix + "_" + input))
                        HttpContext.Current.Application.Set(ChangeHope.WebPage.Sms.ConfigPrefix + "_" + input, Request.Form[input].Trim());
                }
                HttpContext.Current.Application.UnLock();
                ChangeHope.WebPage.Sms._ConfigSave();
                //保存
                this.ltlMsg.Text = "操作成功，已经保存了您的设置";
                this.pnlMsg.Visible = true;
                this.pnlMsg.CssClass = "actionOk";
                return;
            }
            catch (Exception ex)
            {
                this.ltlMsg.Text = "操作失败<br/>" + ex.ToString();
                this.pnlMsg.Visible = true;
                this.pnlMsg.CssClass = "actionErr";
                return;
            }
        }

    }
}
