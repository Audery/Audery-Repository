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
    public partial class admin_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {   
            if(!this.Page.IsPostBack)
            {
                SOSOshop.BLL.PromptInfo.Popedom("007001001");
                GetList();
            }
        }
        
        private void GetList()
        {
            SOSOshop.BLL.Administrators bll = new SOSOshop.BLL.Administrators();
            string action = ChangeHope.WebPage.PageRequest.GetQueryString("action");
            int adminid = ChangeHope.WebPage.PageRequest.GetInt("adminid");
            if (action.Equals("del") && adminid>0)
            {
                SOSOshop.BLL.PromptInfo.Popedom("007001003","对不起，您没有权限进行删除");
                bll.Delete(adminid);
                #region 后台用户操作日志记录
                SOSOshop.Model.AdminInfo adminInfo = SOSOshop.BLL.AdministrorManager.Get();
                SOSOshop.BLL.Logs.Log.LogAdminAdd("删除管理员", (adminInfo == null ? 0 : adminInfo.AdminId), (adminInfo == null ? "" : adminInfo.AdminName), 1);
                #endregion
            }
            this.ltlView.Text = bll.GetList();
            bll = null;
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            GetList();
        }
    }
}
