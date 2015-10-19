using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace _101shop.admin.v3.plugin
{
    public partial class check_object : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                string objName = ChangeHope.WebPage.PageRequest.GetQueryString("objName");
                Response.Write(objName + "组件" + CreateObject(objName));
            }
        }

        private string CreateObject(string objName)
        {
            try
            {
                object obj = Server.CreateObject(objName);
                obj = null;
                return "<font color='Green'>已安装</font>";
            }
            catch (Exception)
            {
                return "<font color='Red'>未安装</font>";
            }
        }

    }
}
