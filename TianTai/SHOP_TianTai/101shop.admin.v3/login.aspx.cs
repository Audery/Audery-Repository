using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _101shop.admin.v3
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (YXShop.Common.AdministrorManager.CheckAdmin())
                {
                    Response.Redirect("admin/admin_index.aspx", true);
                }
                else
                {
                    Response.Redirect("admin/index.aspx", true);
                }
            }
        }
    }
}