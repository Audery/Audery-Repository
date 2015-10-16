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
    public partial class article_edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                SOSOshop.BLL.PromptInfo.Popedom("009002002", "对不起，您没有权限进行编辑");
                SOSOshop.BLL.PromptInfo.Popedom("009002004", "对不起，您没有权限进行编辑");
                InitWebControl();
                int id = ChangeHope.WebPage.PageRequest.GetQueryInt("id");
                if (id > 0)
                {
                    GetModel(id);
                }
            }
        }
        private void InitWebControl()
        {
            SOSOshop.BLL.SysParameter sp = new SOSOshop.BLL.SysParameter();
            YXShop.BLL.SystemInfo.ArticleChannel bll = new YXShop.BLL.SystemInfo.ArticleChannel();
            string channelid = ChangeHope.WebPage.PageRequest.GetQueryString("channelid");
            bll.GetDropList(this.ddlChannel, ""); if (channelid != "") try { this.ddlChannel.SelectedValue = channelid; }catch{}
            bll = null;
            this.txtArea.Attributes.Add("readonly", "readonly");
            this.txtArea.Attributes.Add("onclick", "selectFile('Area',new Array(" + this.txtAreaValue.ClientID + "," + this.txtArea.ClientID + "),310,450,'" + sp.DummyPaht + "');");
            ChangeHope.WebPage.WebControl.Validate(this.txtTitle, "资讯标题，2~50个字符", "isnull_2_50", "必填", "该项为必填");
            ChangeHope.WebPage.WebControl.Validate(this.txtSubTitle, "副标题,选填项目", "no", "", "");
            ChangeHope.WebPage.WebControl.Validate(this.txtKeyWord, "关键字,多个关键字用逗号(,)隔开，该关键字将会直接关联相关的其他内容", "isnull", "必填", "该项为必填");
            ChangeHope.WebPage.WebControl.Validate(this.txtCopyFrom, "如果该文章属于转载，则需要填写转载的地址", "no", "", "");
            ChangeHope.WebPage.WebControl.Validate(this.txtLinkUrl, "如果该文章是直接指向某个链接地址，则填写链接地址", "no", "", "");
            ChangeHope.WebPage.WebControl.Validate(this.txtAuthor, "如果有作者的信息，则需要填写作者信息", "no", "", "");
            ChangeHope.WebPage.WebControl.Validate(this.txtIntroduction, "简介内容,对于该篇文章的简介内容", "no", "", "");
            ChangeHope.WebPage.WebControl.Validate(this.txtArea, "该文章属于哪个分站的内容,不填写则不属于任何分站，只属于该主站内容", "no", "", "");
            this.Form.Attributes.Add("onsubmit", "return CheckForm();");

            if (!sp.IsCloseStation)
            {
                this.txtArea.Enabled = false;
            }

        }

        public void GetModel(int id)
        {
            YXShop.BLL.SystemInfo.Article bll = new YXShop.BLL.SystemInfo.Article();
            YXShop.Model.SystemInfo.Article model = bll.GetModel(id);
            if (model != null)
            {
                this.txtId.Value = id.ToString();
                this.ddlChannel.SelectedValue = model.Channel;
                this.txtTitle.Text = model.Title;
                this.txtSubTitle.Text = model.SubTitle;
                this.txtKeyWord.Text = model.KeyWord;
                this.txtContent.Text = model.Content;
                this.txtCopyFrom.Text = model.CopyFrom;
                this.txtLinkUrl.Text = model.LinkUrl;
                this.txtAuthor.Text = model.Author;
                this.txtIntroduction.Text = model.Introduction;
                this.rblImagesSoure.SelectedValue = model.ImagesSoure != null ? model.ImagesSoure.ToString() : "0";

                if (model.ImagesSoure == 1)
                {
                    this.UpLoadImages.Style.Add("display", "");
                    ViewState["Images"] = model.ImagesAddress;
                }
                else if (model.ImagesSoure == 2)
                {
                    this.ImagesAddress.Style.Add("display", "");
                    this.txtImagesSoure.Text = model.ImagesAddress;
                }

                //this.chkIsTop.Checked = model.IsTop;
                //this.chkIsElite.Checked = model.IsElite;
                //this.chkIsPic.Checked = model.IsPic;
                this.chkIsPageType.Checked = model.IsPageType;
                this.txtArea.Text = ChangeHope.DataBase.DataHelper.GetContent("yxs_provinces", "Id", "CityName", model.Area);
                this.txtAreaValue.Value = model.Area;
                this.chkState.Checked = model.State == 1 ? true : false;
                string[] Property = model.Property.Split('|');
                for (int i = 0; i < Property.Length; i++)
                {
                    if (Property[i] == "1")
                    {
                        this.cblArticlePropery.Items[i].Selected = true;
                    }
                }
            }
            model = null;
            bll = null;
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void Save()
        {
            YXShop.Model.SystemInfo.Article model = new YXShop.Model.SystemInfo.Article();
            if (this.ddlChannel.SelectedValue.Trim() == "" || this.ddlChannel.SelectedValue.Trim() == string.Empty)
            {
                this.ltlMsg.Text = "操作失败，请选择所属栏目.";
                this.pnlMsg.Visible = true;
                this.pnlMsg.CssClass = "actionOk";
                return;
            }
            model.Channel = this.ddlChannel.SelectedValue;
            model.Title = this.txtTitle.Text;
            model.SubTitle = this.txtSubTitle.Text;
            model.KeyWord = this.txtKeyWord.Text;
            model.Content = this.txtContent.Text;
            model.CopyFrom = this.txtCopyFrom.Text;
            model.LinkUrl = this.txtLinkUrl.Text;
            model.Author = this.txtAuthor.Text;
            SOSOshop.Model.AdminInfo adminInfo = (SOSOshop.Model.AdminInfo)SOSOshop.BLL.AdministrorManager.Get();
            model.Users = adminInfo.AdminName;
            model.Editor = adminInfo.AdminName;
            model.Hits = 0;
            model.Introduction = this.txtIntroduction.Text;
            model.CreateTime = System.DateTime.Now;
            model.UpdateTime = System.DateTime.Now;
            model.IsTop = false;
            model.IsElite = false;
            model.IsPic = false;
            model.IsPageType = this.chkIsPageType.Checked;
            model.State = chkState.Checked ? 1 : 0;
            model.Area = this.txtAreaValue.Value;
            model.ImagesSoure = Convert.ToInt32(this.rblImagesSoure.SelectedValue);

            if (this.rblImagesSoure.SelectedValue == "1")
            {
                ChangeHope.Common.UploadFile uf = new ChangeHope.Common.UploadFile();
                uf.ExtensionLim = ".gif,.jpg,.jpeg,.bmp";
                uf.FileLengthLim = 4000;
                uf.PostedFile = this.FileUpload1;
                uf.SavePath = "/yxuploadfile/article";
                if (uf.Upload())
                {
                    if (uf.HaveLoad)
                    {
                        model.ImagesAddress = uf.FilePath;
                    }
                    else
                    {
                        if (ViewState["Images"] != null)
                        {
                            model.ImagesAddress = ViewState["Images"].ToString();
                        }
                        else
                        {
                            model.ImagesAddress = string.Empty;
                        }
                    }
                }
                else
                {
                    this.ltlMsg.Text = "操作失败，" + uf.Message + "";
                    this.pnlMsg.Visible = true;
                    this.pnlMsg.CssClass = "actionErr";
                    return;
                }
            }
            else if (this.rblImagesSoure.SelectedValue == "2")
            {
                model.ImagesAddress = this.txtImagesSoure.Text;
            }
            else if (this.rblImagesSoure.SelectedValue == "0")
            {
                model.ImagesAddress = "";
            }

            string Property = "";
            for (int i = 0; i < this.cblArticlePropery.Items.Count; i++)
            {
                if (cblArticlePropery.Items[i].Selected)
                {
                    Property += "1|";
                }
                else
                {
                    Property += "0|";
                }
            }
            model.Property = Property;

            YXShop.BLL.SystemInfo.Article bll = new YXShop.BLL.SystemInfo.Article();
            if (ChangeHope.Common.ValidateHelper.IsNumber(this.txtId.Value))
            {
                model.Id = ChangeHope.Common.StringHelper.StringToInt(this.txtId.Value);
                bll.Update(model);
                this.ltlMsg.Text = "操作成功，已修改该信息";
                this.pnlMsg.Visible = true;
                this.pnlMsg.CssClass = "actionOk";
            }
            else
            {
                bll.Add(model);
                this.txtTitle.Text = string.Empty;
                this.ltlMsg.Text = "操作成功，已添加该信息;你可以继续添加.";
                this.pnlMsg.Visible = true;
                this.pnlMsg.CssClass = "actionOk";
            }
            bll = null;
            model = null;
        }
    }
}
