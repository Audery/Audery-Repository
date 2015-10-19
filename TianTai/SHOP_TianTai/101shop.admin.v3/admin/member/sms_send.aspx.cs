using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace _101shop.admin.v3.member
{
    public partial class sms_send : SOSOshop.WEB.UI.ManageBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cbxlMemberRank.DataSource = new SOSOshop.BLL.DbBase().ExecuteTable("SELECT DISTINCT CompanyClass FROM memberaccount WHERE CompanyClass<>''");
                cbxlMemberRank.DataTextField = "CompanyClass";
                cbxlMemberRank.DataValueField = "CompanyClass";
                cbxlMemberRank.DataBind();
            }
        }

        public override void SetModuleTag()
        {

        }

        protected void lbSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMsg.Text))
            {
                if (!string.IsNullOrEmpty(txtMobile.Text))
                {
                    SOSOshop.BLL.Sms.SendAndSaveDataBase(txtMobile.Text, txtMsg.Text, base.UserName);
                }
                foreach (ListItem item in cbxlMemberRank.Items)
                {
                    if (item.Selected)
                    {
                        string mobile = string.Join(",", new SOSOshop.BLL.DbBase().ExecuteTable("SELECT MobilePhone FROM memberaccount WHERE CompanyClass='" + item.Text + "' AND State=0").AsEnumerable().Select(x => x.Field<string>("MobilePhone")));
                        SOSOshop.BLL.Sms.SendAndSaveDataBase(mobile, txtMsg.Text, base.UserName, item.Text);
                    }
                }
                Library.Client.Jscript.Alert(this, "发送成功!");
            }

        }
    }
}
