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
    public partial class check_code : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Expires = -1;
            if (!this.Page.IsPostBack)
            {
                this.CreateCheckCode();
            }
        }

        private void CreateCheckCode()
        {
            ChangeHope.Common.ImagesHelper img = new ChangeHope.Common.ImagesHelper();
            string checkCode = ChangeHope.Common.DEncryptHelper.GetRandWord(5);
            Response.Cookies.Add(new HttpCookie("CheckCode", checkCode));
            img.CreateCheckImage(checkCode);
            img = null;
        }
    }
}
