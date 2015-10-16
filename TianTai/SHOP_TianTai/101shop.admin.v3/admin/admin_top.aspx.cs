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
using YXShop.Common;
using System.Web.Services;
using SOSOshop.BLL;

namespace _101shop.admin.v3
{
    public partial class admin_top : System.Web.UI.Page
    {
        public SOSOshop.Model.AdminInfo aInfo = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            aInfo = SOSOshop.BLL.AdministrorManager.Get();
            /*货源&订单 权限分配*/
            bool power_manage_order = SOSOshop.BLL.PowerPass.isPass("005003000") || SOSOshop.BLL.PowerPass.isPass("005004000");
            /*买家&卖家 权限分配*/
            bool power_manage_member = SOSOshop.BLL.PowerPass.isPass("008009000") && (SOSOshop.BLL.PowerPass.isPass("008008006") || SOSOshop.BLL.PowerPass.isPass("008009006"));
           

            if (SOSOshop.BLL.PowerPass.isPass("008009006"))
            {
                //DbBase db = new DbBase();

                //object obj = db.ExecuteScalar("SELECT COUNT(*) FROM dbo.memberaccount WHERE state = 1");

                //notReviewedMemberCount.InnerHtml = obj.ToString();
            }
            else 
            {
               // divUnReviewed.Visible = false;
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            new SOSOshop.BLL.DbBase().ClearCache();
        }
    }
}
