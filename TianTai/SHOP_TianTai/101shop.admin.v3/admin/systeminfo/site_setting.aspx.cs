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

namespace _101shop.admin.v3.systeminfo
{
    public partial class site_setting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitWebControl();
            if(!this.Page.IsPostBack)
            {
                SOSOshop.BLL.PromptInfo.Popedom("010001006");
                GetModel();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SOSOshop.BLL.PromptInfo.Popedom("010001007","对不起，您没有权限进行修改");
            Save();
        }
        private void GetModel()
        {
            this.pnlMsg.Visible = false;
            YXShop.Model.SystemInfo.WebSetting model =null;
            YXShop.BLL.SystemInfo.WebSetting bll = new YXShop.BLL.SystemInfo.WebSetting();
            try
            {
                model = bll.GetModel();
                if(model!=null)
                {
                    this.txtId.Value = model.Id.ToString();
                    this.txtBannerPath.Text = model.BannerPath;
                    this.txtStatisticsCode.Text = model.Statisticscode;//
                    this.txtFileSize.Text = model.Filesize.ToString();//
                    this.txtCloseBBSInfo.Text = model.CloseBBSInfo;
                    this.txtCloseWebSiteInfo.Text = model.CloseWebSiteInfo;
                    this.txtCopyRight.Text = model.CopyRight;
                    this.txtEmail.Text = model.Email;
                    this.txtFax.Text = model.Fax;
                    this.txtLogoPath.Text = model.LogoPath;
                    this.txtMeteInfo.Text = model.MeteInfo;
                    this.txtMeteKey.Text = model.MeteKey;
                    this.txtTel.Text = model.Tel;
                    this.txtTemplatePath.Text = model.TmplatePath;
                    this.txtUsersAgreement.Text = model.UsersAgreement;
                    this.txtWebSiteDomain.Text = model.WebSiteDomain;
                    this.txtWebSiteName.Text = model.WebSiteName;
                    this.txtWebSitePath.Text = model.WebSitePath;
                    this.txtWebSiteTitle.Text = model.WebSiteTitle;

                    this.ckbCloseBBS.Checked = model.CloseBBS.Equals(1);
                    this.ddlCloseShop.SelectedValue = model.CloseShop.ToString();
                    this.ddlCloseStation.SelectedValue = model.CloseStation.ToString();
                    this.ckbCloseWebSite.Checked = model.CloseWebSite.Equals(1);
                    this.ddlLoginMothod.SelectedValue = model.LoginMothod.ToString();
                    this.ddlPublicMethod.SelectedValue = model.PublicMethod.ToString();
                    this.ddlStaticPageFileType.SelectedValue = model.StaticPageFileType;
                }
            }
            catch { }
            finally
            {
                model = null;
                bll = null;
            }
        }
        private void Save()
        {
            YXShop.Model.SystemInfo.WebSetting model = new YXShop.Model.SystemInfo.WebSetting();
            YXShop.BLL.SystemInfo.WebSetting bll = new YXShop.BLL.SystemInfo.WebSetting();
            try
            {
                if (this.txtTel.Text!="")
                {
                    if(!ChangeHope.Common.ValidateHelper.IsPhone(txtTel.Text))
                    {
                        this.ltlMsg.Text = "操作失败,请输入正确电话号码！";
                        this.pnlMsg.Visible = true;
                        this.pnlMsg.CssClass = "actionErr";
                        return;
                    }
                }
                if (this.txtFax.Text!="")
                {
                    if(!ChangeHope.Common.ValidateHelper.IsPhone(txtFax.Text))
                    {
                        this.ltlMsg.Text = "操作失败,请输入正确传真号码！";
                        this.pnlMsg.Visible = true;
                        this.pnlMsg.CssClass = "actionErr";
                        return;
                    }
                }
                if (this.txtEmail.Text!="")
                {
                    if(!ChangeHope.Common.ValidateHelper.IsEmail(txtEmail.Text))
                    {
                        this.ltlMsg.Text = "操作失败,请输入正确邮件地址！";
                        this.pnlMsg.Visible = true;
                        this.pnlMsg.CssClass = "actionErr";
                        return;
                    }
                }
                if (this.txtFileSize.Text!="")
                {
                    if(!ChangeHope.Common.ValidateHelper.IsNumber(txtFileSize.Text))
                    {
                        this.ltlMsg.Text = "操作失败,整站上传文件大小必须为整数！";
                        this.pnlMsg.Visible = true;
                        this.pnlMsg.CssClass = "actionErr";
                        return;
                    }
                }
               
                model.Id = ChangeHope.Common.StringHelper.StringToInt(this.txtId.Value);
                model.Statisticscode = this.txtStatisticsCode.Text;//
                model.Filesize = ChangeHope.Common.StringHelper.StringToInt(this.txtFileSize.Text);//
                model.BannerPath = this.txtBannerPath.Text;
                model.CloseBBSInfo = this.txtCloseBBSInfo.Text;
                model.CloseWebSiteInfo = this.txtCloseWebSiteInfo.Text;
                model.CopyRight = this.txtCopyRight.Text;
                model.Email = this.txtEmail.Text;
                model.Fax = this.txtFax.Text;
                model.LogoPath = this.txtLogoPath.Text;
                model.MeteInfo = this.txtMeteInfo.Text;
                model.MeteKey = this.txtMeteKey.Text;
                model.Tel = this.txtTel.Text;
                model.TmplatePath = this.txtTemplatePath.Text;
                model.UsersAgreement = this.txtUsersAgreement.Text;
                model.WebSiteDomain = this.txtWebSiteDomain.Text;
                model.WebSiteName = this.txtWebSiteName.Text;
                model.WebSitePath = this.txtWebSitePath.Text;
                model.WebSiteTitle = this.txtWebSiteTitle.Text;

                model.CloseBBS = this.ckbCloseBBS.Checked?1:0;
                model.CloseShop = ChangeHope.Common.StringHelper.StringToInt(this.ddlCloseShop.SelectedValue);
                model.CloseStation =ChangeHope.Common.StringHelper.StringToInt( this.ddlCloseStation.SelectedValue);
                model.CloseWebSite = this.ckbCloseWebSite.Checked?1:0;
                model.LoginMothod = ChangeHope.Common.StringHelper.StringToInt(this.ddlLoginMothod.SelectedValue);
                model.PublicMethod = ChangeHope.Common.StringHelper.StringToInt(this.ddlPublicMethod.SelectedValue);
                model.StaticPageFileType = this.ddlStaticPageFileType.SelectedValue;
                bll.AddOrUpdate(model);
                this.ltlMsg.Text = "操作成功，已经保存了您的设置";
                this.pnlMsg.Visible = true;
                this.pnlMsg.CssClass = "actionOk";
            }
            catch (Exception e) {
                this.ltlMsg.Text = "操作失败<br/>"+e.ToString();
                this.pnlMsg.Visible = true;
                this.pnlMsg.CssClass = "actionErr";
            }

            finally
            {
                ChangeHope.Common.CacheClass.RemoveOneCache("WebSetting");
                model = null;
                bll = null;
            }
        }
        private void InitWebControl()
        {
            ChangeHope.WebPage.WebControl.Validate(txtWebSiteName, "给网站起一个名字，建议20个字符以内", "isnull", "必填", "该项为必填项");
            ChangeHope.WebPage.WebControl.Validate(txtWebSiteTitle, "网站的标题，显示在浏览器的标题栏", "isnull", "必填", "该项为必填项");
            ChangeHope.WebPage.WebControl.Validate(txtTel, "您的联系电话，方便您网站的用户联系到您", "no", "", "");
            ChangeHope.WebPage.WebControl.Validate(txtFax, "输入您的传真机号码，方便您网站的用户联系到您", "no", "", "");
            ChangeHope.WebPage.WebControl.Validate(this.txtEmail, "您的邮箱，方便您网站的用户联系到您,以及接受系统给您发的信息", "isemail", "必填", "该项为必填项，并符合邮件格式");
            ChangeHope.WebPage.WebControl.Validate(this.txtWebSiteDomain, "您网站的地址或者域名，HTTP地址以\"http://\"开始<br/>比如：http://www.yxshop.cn.", "ishttpurl", "必填", "该项为必填项");
            ChangeHope.WebPage.WebControl.Validate(this.txtWebSitePath, "该处是指您网站的安装路径，相对于根目录的路径<br/>虚拟目录安装路径以\" / \"开始与结尾,根目录直接填写\" / \"", "isnull", "必填", "该项为必填项");
            ChangeHope.WebPage.WebControl.Validate(this.txtTemplatePath, "为防止别人猜测到模板存放地址，您可以在此输入多层目录做为网站模板的根目录，最好在取带“#”号的目录名。目录的格式如下：/Template1/Template2模板方案", "isnull", "必填", "该项为必填项");
            ChangeHope.WebPage.WebControl.Validate(this.txtLogoPath, "您网站或者公司的徽标，用于显示在页面的左上角作为您网站的标志,如果该项不填写则显示默认的LOGO", "no", "", "");
            ChangeHope.WebPage.WebControl.Validate(this.txtBannerPath, "为您的网站做一个广告条吧", "no", "", "");
            ChangeHope.WebPage.WebControl.Validate(this.txtStatisticsCode, "为网站设置统计代码", "isnull", "必填", "该项为必填项");//
            ChangeHope.WebPage.WebControl.Validate(this.txtFileSize, "为网站设置整站上传附件大小", "isint", "必填", "该项为必填项，格式应为正整数");//
            ChangeHope.WebPage.WebControl.Validate(this.txtMeteKey, "为了让搜索引擎尽快找到您的网站，请在此填写您网站的关键词", "no", "", "");
            ChangeHope.WebPage.WebControl.Validate(this.txtMeteInfo, "对Key的叙述", "no", "", "");
            ChangeHope.WebPage.WebControl.Validate(this.txtCloseWebSiteInfo, "您关闭网站的原因，在您关闭网站时，该信息将显示在网站的首页上", "no", "", "");
            ChangeHope.WebPage.WebControl.Validate(this.txtCloseBBSInfo, "您关闭BBS的原因，当您关闭BBS时，该信息将显示在BBS的首页上", "no", "", "");
            ChangeHope.WebPage.WebControl.Validate(this.txtCopyRight, "显示在您页面底部的版权信息,例如:<br/>CopyRight&copy 2008-2010 www.changehope.com", "no", "", "");
            this.Form.Attributes.Add("onsubmit", "return CheckForm();");
        }

        
    }
}
