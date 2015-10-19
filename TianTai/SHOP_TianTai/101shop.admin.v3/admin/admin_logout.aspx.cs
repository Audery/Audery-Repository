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
    public partial class admin_logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SOSOshop.Model.AdminInfo aInfo = (SOSOshop.Model.AdminInfo)SOSOshop.BLL.AdministrorManager.Get();
                if (aInfo != null)
                {
                    //登出日志时间
                    ChangeHope.DataBase.SQLServerHelper.ExecuteSql("update yxs_adminloginlog set loginouttime = getdate() where id in (select top (1) id from yxs_adminloginlog where " +
                        "adminname = '" + aInfo.AdminName + "' " +
                        "and convert(char(10),loginintime,120) = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' " +
                        "and loginip = '" + Request.UserHostAddress + "' " +
                        "and operatenote = '登陆成功!' order by loginintime desc)");
                    //登出session & cookies
                    SOSOshop.BLL.AdministrorManager.DelAdminInfo();
                }
                ChangeHope.WebPage.Script.AlertAndRedirect("您已经成功退出该系统！", "index.aspx");
            }
        }
    }
}
