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
using YXShop.Model.Admin;
using YXShop.Common;

namespace _101shop.admin.v3.member
{
    public partial class admin_edit : System.Web.UI.Page
    {
        public bool enabled_edit_everyone = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UpdatePhoneAndQQ();
                int adminid = 0; try { adminid = Convert.ToInt32(Request.QueryString["adminid"]); }
                catch { }
                SOSOshop.Model.AdminInfo aInfo = (SOSOshop.Model.AdminInfo)SOSOshop.BLL.AdministrorManager.Get();
                if (aInfo == null || aInfo.AdminId != adminid)
                {
                    SOSOshop.BLL.PromptInfo.Popedom("007001002", "对不起，您没有权限进行编辑");
                    SOSOshop.BLL.PromptInfo.Popedom("007001004", "对不起，您没有权限进行编辑");
                }
                //拒绝访问
                if (Request.UrlReferrer != null && Request.UrlReferrer.Host != Request.Url.Host)
                {
                    Response.Write("拒绝访问。"); Response.End(); return;
                }
                //初始化
                InitWebControl();
                GetModel();
            }
        }

        private void GetModel()
        {
            SOSOshop.Model.AdminInfo aInfo = SOSOshop.BLL.AdministrorManager.Get();
            int adminid = ChangeHope.WebPage.PageRequest.GetInt("adminid");
            this.txtName.ReadOnly = false;
            if (Request["edit"] != null && Request["edit"] == "pwd")
            {
                adminid = aInfo.AdminId; enabled_edit_everyone = false;
                this.HyperLink1.Visible = false;
                this.Tr0.Attributes.Add("style", "display:none");
                this.Tr1.Attributes.Add("style", "display:none");
                this.Tr2.Attributes.Add("style", "display:none");
                this.Tr3.Attributes.Add("style", "display:none");
            }
            if (adminid > 0)
            {
                SOSOshop.BLL.Administrators bll = new SOSOshop.BLL.Administrators();
                SOSOshop.Model.Administrators model = bll.GetModel(adminid);
                if (model != null)
                {
                    this.txtName.ReadOnly = true;
                    this.txtAdminId.Value = model.AdminId.ToString();
                    this.txtManageBeginTime.Text = model.ManageBeginTime.ToString();
                    this.txtManageEndTime.Text = model.ManageEndTime.ToString();
                    this.txtName.Text = model.Name;
                    this.txtName.ReadOnly = true;
                    this.ckbAllowModifyPassword.Checked = model.AllowModifyPassWord.Equals(1) ? true : false;
                    if (aInfo.AdminName == "admin")
                    {
                        this.ckbPower.Enabled = false;
                    }
                    this.ckbPower.SelectedValue = model.Power.ToString();
                    this.ckbState.Checked = model.State.Equals(1) ? true : false;
                    ChangeHope.WebPage.WebControl.Validate(this.txtPasswordRe, "密码为空时，则不修改密码", "no", "", "");

                }
                model = null;
                bll = null;
                return;

            }
            ChangeHope.WebPage.WebControl.Validate(this.txtPasswordRe, "密码为空时，则不修改密码", "isnull_8_20", "必填", "该项为必填项,且为8~20个字符");
        }
        private void InitWebControl()
        {

            ChangeHope.WebPage.WebControl.Validate(this.txtName, "设置管理员的帐号，该帐号的长度为2~20个英文字符", "isnull_2_20", "必填", "该项为必填项");
            ChangeHope.WebPage.WebControl.Validate(this.txtPassword, "为防止输入密码错误，在此处再次确认密码，不修改密码该处则不填写", "compare", "与密码相同", "两次输入的密码不相同！");            
            //ChangeHope.WebPage.WebControl.SetDate(this.txtManageBeginTime);
            //ChangeHope.WebPage.WebControl.SetDate(this.txtManageEndTime);
            ChangeHope.WebPage.WebControl.Validate(this.txtManageEndTime, "该管理员开始有管理权限的时间", "isnull", "必填", "该项为必填项");
            ChangeHope.WebPage.WebControl.Validate(this.txtManageBeginTime, "该管理员管理权限的过期时间", "isnull", "必填", "该项为必填项");
            this.Form.Attributes.Add("onsubmit", "return CheckForm()");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void Save()
        {
            if (ChangeHope.Common.StringHelper.StringToDateTime(this.txtManageBeginTime.Text) > ChangeHope.Common.StringHelper.StringToDateTime(this.txtManageEndTime.Text))
            {
                this.ltlMsg.Text = "保存失败！开始时间大于结局时间。";
                this.pnlMsg.CssClass = "actionErr";                
                return;
            }
            if(!string.IsNullOrEmpty(this.txtPasswordRe.Text))
            {
                if (Library.Lang.DataValidator.isNumber(this.txtPasswordRe))
                {
                    this.ltlMsg.Text = "保存失败！新密码不能全为数字！";
                    this.pnlMsg.CssClass = "actionErr";
                    pnlMsg.Visible = true;
                    return;
                }
                if (this.txtPasswordRe.Text.Length < 8)
                {
                    this.ltlMsg.Text = "保存失败！新密码的长度必须大于等于8位！";
                    this.pnlMsg.CssClass = "actionErr";
                    pnlMsg.Visible = true;
                    return;
                }
            }
            
            SOSOshop.BLL.Administrators bll = new SOSOshop.BLL.Administrators();
            SOSOshop.Model.Administrators model = new SOSOshop.Model.Administrators();

            try
            {
                model.AdminId = ChangeHope.Common.StringHelper.StringToInt(this.txtAdminId.Value);
                if (model.AdminId > 0) model = bll.GetModel(model.AdminId);
                model.AllowModifyPassWord = this.ckbAllowModifyPassword.Checked ? 1 : 0;
                model.ManageBeginTime = ChangeHope.Common.StringHelper.StringToDateTime(this.txtManageBeginTime.Text);
                model.ManageEndTime = ChangeHope.Common.StringHelper.StringToDateTime(this.txtManageEndTime.Text);
                model.Name = this.txtName.Text;
                model.PassWord = this.txtPassword.Text;
                model.Power = ChangeHope.Common.StringHelper.StringToInt(this.ckbPower.SelectedValue);
                model.State = this.ckbState.Checked ? 1 : 0;
                if (model.AdminId > 0)
                {
                    bll.Update(model);
                }
                else
                {
                    model.Role = "";
                    this.txtAdminId.Value = bll.Add(model).ToString();
                }
                this.ltlMsg.Text = "保存成功！";
                this.pnlMsg.CssClass = "actionOk";

                if (this.txtAdminId.Value.Equals("0"))
                {
                    this.ltlMsg.Text = "保存失败！已经有相同的用户名存在";
                    this.pnlMsg.CssClass = "actionErr";
                }
                else
                {
                    this.txtName.ReadOnly = true;
                    #region 后台用户操作日志记录
                    SOSOshop.Model.AdminInfo adminInfo = SOSOshop.BLL.AdministrorManager.Get();
                    SOSOshop.BLL.Logs.Log.LogAdminAdd("修改管理员", (adminInfo == null ? 0 : adminInfo.AdminId), (adminInfo == null ? "" : adminInfo.AdminName), 1);
                    #endregion
                }
                if (!enabled_edit_everyone || this.HyperLink1.Visible == false || this.Tr1.Visible == false)
                {
                    this.ltlMsg.Text += "<script>setTimeout(function(){window.location='../systeminfo/site_sysinfo.aspx';},2000);</script>";
                    this.formtbl.Visible = false;
                }
            }
            catch (Exception ex)
            {
                this.ltlMsg.Text = "保存失败：<br/>" + ex.ToString();
                this.pnlMsg.CssClass = "actionErr";
            }
            finally
            {
                this.pnlMsg.Visible = true;
                bll = null;
                model = null;
            }
            Library.Client.Jscript.ExecuteJs(this, "skip();");
        }

        /// <summary>
        /// 更新号码与QQ
        /// </summary>
        private void UpdatePhoneAndQQ()
        {
            string strAct = Request["act"];
            if (!Library.Lang.DataValidator.isNULL(strAct))
            {
                if (strAct.Contains("yxs_administrators"))
                {
                    string[] act = strAct.Split(':');
                    string val = Request["val"];
                    string id = Request["id"];
                    int adminId = 0;
                    string value = val;
                    string column = string.Empty;

                    if (id != "" || id != "0")
                    {
                        adminId = Convert.ToInt32(id);
                    }

                    if (act[2] == "LoginAuthenticationOfficePhone")
                    {
                        column = "LoginAuthenticationOfficePhone";
                    }
                    else if (act[2] == "OfficePhone")
                    {
                        column = "OfficePhone";
                    }
                    else if (act[2] == "QQ")
                    {
                        column = "QQ";
                    }

                    if (adminId != 0)
                    {
                        string sql = string.Format("update dbo.yxs_administrators set {0}='{1}' where adminId={2}", column, value, adminId);

                        SOSOshop.BLL.DbBase db = new SOSOshop.BLL.DbBase();

                        int returnVal = db.ExecuteNonQuery(sql);
                        Response.Write(returnVal > 0 ? "ok" : "抱歉，编辑失败。"); 
                        Response.End(); return;
                    }
                    else
                    {
                        Response.Write("编辑失败，未提供相应信息的Id");
                        Response.End(); return;
                    }
                }
            }
        }
    }
}
