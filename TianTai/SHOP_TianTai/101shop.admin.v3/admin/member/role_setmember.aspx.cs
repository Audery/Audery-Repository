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

namespace _101shop.admin.v3.member
{
    public partial class role_setmember : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SOSOshop.BLL.PromptInfo.Popedom("007002006", "对不起，您没有权限进行设置");
                int roleid = ChangeHope.WebPage.PageRequest.GetQueryInt("id");
                if (roleid > 0)
                {
                    ViewState["RoleId"] = roleid;
                    SOSOshop.BLL.Role bll = new SOSOshop.BLL.Role();
                    SOSOshop.Model.Role model = bll.GetModelID(roleid);
                    if (model != null)
                    {
                        this.lbRoleName.Text = model.Name;
                        this.lbRoleDescription.Text = model.Description;
                        this.BindInfo(roleid.ToString());
                    }
                }
            }
        }

        protected void BindInfo(string RoleId)
        {
            SOSOshop.BLL.Administrators bll = new SOSOshop.BLL.Administrators();
            ChangeHope.WebPage.Table table = new ChangeHope.WebPage.Table();
            ChangeHope.DataBase.DataByPage dataPage = bll.GetListDB(" and State=0 ");
            //添加表的内容
            if (dataPage.DataReader != null)
            {
                while (dataPage.DataReader.Read())
                {
                    bool WhetherIN = false;
                    String[] gly = dataPage.DataReader["role"].ToString().Split(',');
                    for (int i = 0; i < gly.Length; i++)
                    {
                        if (gly[i].ToString() == RoleId)
                        {
                            lbOption2.Items.Add(new ListItem(dataPage.DataReader["name"].ToString(), dataPage.DataReader["adminid"].ToString()));
                            WhetherIN = true;
                            break;
                        }
                    }
                    if (!WhetherIN)
                    {
                        lbOption.Items.Add(new ListItem(dataPage.DataReader["name"].ToString(), dataPage.DataReader["adminid"].ToString()));
                    }
                }
            }
        }

        protected void butAddRoleMember_Click(object sender, EventArgs e)
        {
            string ReAddId = this.lbOption.SelectedValue;
            if (ReAddId != string.Empty)
            {
                SOSOshop.BLL.Administrators bll = new SOSOshop.BLL.Administrators();
                SOSOshop.Model.Administrators model = bll.GetModel(int.Parse(ReAddId));
                if (model != null)
                {
                    char sep = ','; string RoleStr = model.Role;
                    if (!string.IsNullOrEmpty(RoleStr))
                    {
                        bll.Amend(Convert.ToInt32(model.AdminId), "role", (RoleStr.Trim(sep) + "," + ViewState["RoleId"]).Trim(sep));
                    }
                    else
                    {
                        bll.Amend(Convert.ToInt32(model.AdminId), "role", ViewState["RoleId"].ToString());
                    }
                    #region 后台用户操作日志记录
                    SOSOshop.Model.AdminInfo adminInfo = SOSOshop.BLL.AdministrorManager.Get();
                    SOSOshop.BLL.Logs.Log.LogAdminAdd("添加角色【" + this.lbRoleName.Text + "】成员【" + model.Name + "】", (adminInfo == null ? 0 : adminInfo.AdminId), (adminInfo == null ? "" : adminInfo.AdminName), 1);
                    #endregion
                }
                Response.Redirect("role_setmember.aspx?id=" + ViewState["RoleId"].ToString() + "");
            }
        }

        protected void butRemoerMember_Click(object sender, EventArgs e)
        {
            string ReMoveId = this.lbOption2.SelectedValue;
            if (ReMoveId != string.Empty)
            {
                SOSOshop.BLL.Administrators bll = new SOSOshop.BLL.Administrators();
                SOSOshop.Model.Administrators model = bll.GetModel(int.Parse(ReMoveId));
                string AmentStr = string.Empty;
                if (model != null)
                {
                    char sep = ','; string[] RoleStrs = model.Role.Trim(',').Split(sep);
                    foreach (string RoleStr in RoleStrs) if (!string.IsNullOrEmpty(RoleStr) && !ViewState["RoleId"].ToString().Equals(RoleStr)) AmentStr += RoleStr + sep;
                    bll.Amend(Convert.ToInt32(model.AdminId), "role", AmentStr.Trim(sep));
                    #region 后台用户操作日志记录
                    SOSOshop.Model.AdminInfo adminInfo = SOSOshop.BLL.AdministrorManager.Get();
                    SOSOshop.BLL.Logs.Log.LogAdminAdd("删除角色【" + this.lbRoleName.Text + "】成员【" + model.Name + "】", (adminInfo == null ? 0 : adminInfo.AdminId), (adminInfo == null ? "" : adminInfo.AdminName), 1);
                    #endregion
                }
                Response.Redirect("role_setmember.aspx?id=" + ViewState["RoleId"].ToString() + "");
            }
        }

    }
}
