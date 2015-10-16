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
using System.Collections.Generic;

namespace _101shop.admin.v3
{
    public partial class admin_index : System.Web.UI.Page
    {
        protected string WebName;
        protected string WebTitle;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SOSOshop.BLL.AdministrorManager.CheckAdmin();
                SOSOshop.BLL.SysParameter sp = new SOSOshop.BLL.SysParameter();
                WebName = sp.WebSiteName;
                WebTitle = sp.WebSiteTitle;
                if (Request.QueryString["ClearCache"] != null)//清除缓存
                {
                    SOSOshop.BLL.DbBase db1 = new SOSOshop.BLL.DbBase(); db1.ClearCache();
                    Response.Redirect("admin_index.aspx"); Response.End();
                }                
                ChangeHope.DataBase.SQLServerHelper.connectionString = new SOSOshop.BLL.Db()._db.ConnectionString;                
                
            }
        }
    }
}
