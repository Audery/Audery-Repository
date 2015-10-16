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

namespace _101shop.admin.v3
{
    public partial class admin_page : System.Web.UI.MasterPage
    {
        public SOSOshop.BLL.SysParameter sp = new SOSOshop.BLL.SysParameter();
        /// <summary>
        /// 后台绝对路径
        /// </summary>
        public static string adminPath = "";
        /// <summary>
        /// 安全处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            adminPath = sp.DummyPaht + "admin/";
            if (!IsPostBack)
            {
                if (Request["is_ajax"] == "1" && Request["sn"] != null && Request["sn"] == System.Configuration.ConfigurationManager.AppSettings["AppCode"])
                {
                    //API HANDLE For Being sn is not null
                }
                else
                {
                    //ADMIN HANDLE
                    SOSOshop.BLL.AdministrorManager.CheckAdmin();
                }
            }
        }
    }
}
