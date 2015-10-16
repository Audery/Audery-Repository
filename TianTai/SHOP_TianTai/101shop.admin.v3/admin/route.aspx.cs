using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _101shop.admin.v3.admin
{
    public partial class route : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["baseurl"] + Request.QueryString["url"]);
        }
    }
}