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
    public partial class role_edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ChangeHope.WebPage.PageRequest.GetInt("id") > 0)
                {
                    BandInfo(ChangeHope.WebPage.PageRequest.GetInt("id"));
                }
                InitWebControl();

                if (ViewState["ID"] != null)
                {
                    SOSOshop.BLL.PromptInfo.Popedom("007002004", "对不起，您没有权限进行编辑");
                }
                else
                {
                    SOSOshop.BLL.PromptInfo.Popedom("007002002", "对不起，您没有权限进行新增");
                }
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Save();
        }
        /// <summary>
        /// 验证
        /// </summary>
        private void InitWebControl()
        {
            ChangeHope.WebPage.WebControl.Validate(this.txtRoleName, "输入角色的名称", "isnull", "必填", "该项为必填项");
            this.Form.Attributes.Add("onsubmit", "return CheckForm()");
        }
        protected void BandInfo(int id)
        {
            SOSOshop.BLL.Role bll = new SOSOshop.BLL.Role();
            SOSOshop.Model.Role model = bll.GetModelID(id);
            this.txtRoleName.Text = model.Name;
            this.txtDescription.Text = model.Description;
            ViewState["ID"] = model.ID;
        }
        /// <summary>
        /// 保存信息
        /// </summary>
        protected void Save()
        {
            SOSOshop.BLL.Role bll = new SOSOshop.BLL.Role();
            SOSOshop.Model.Role model = new SOSOshop.Model.Role();
            model.Name =this.txtRoleName.Text.Trim();
            model.Description =this.txtDescription.Text.Trim();
            if (ViewState["ID"] != null)
            {
                model.ID = int.Parse(ViewState["ID"].ToString());
                bll.Amend(model);
                this.ltlMsg.Text = "操作成功，已保存该信息";
                this.pnlMsg.Visible = true;
                this.pnlMsg.CssClass = "actionOk";
                #region 后台用户操作日志记录
                SOSOshop.Model.AdminInfo adminInfo = SOSOshop.BLL.AdministrorManager.Get();
                SOSOshop.BLL.Logs.Log.LogAdminAdd("修改角色", (adminInfo == null ? 0 : adminInfo.AdminId), (adminInfo == null ? "" : adminInfo.AdminName), 1);
                #endregion
            }
            else
            {
                bll.Create(model);
                this.ltlMsg.Text = "操作成功，已保存该信息";
                this.pnlMsg.Visible = true;
                this.pnlMsg.CssClass = "actionOk";
                #region 后台用户操作日志记录
                SOSOshop.Model.AdminInfo adminInfo = SOSOshop.BLL.AdministrorManager.Get();
                SOSOshop.BLL.Logs.Log.LogAdminAdd("添加角色", (adminInfo == null ? 0 : adminInfo.AdminId), (adminInfo == null ? "" : adminInfo.AdminName), 1);
                #endregion
            }
        }
    }
}
