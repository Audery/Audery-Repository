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
using YXShop.BLL.Admin;

namespace _101shop.admin.v3.accessories
{
    public partial class hailhellowlink_edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SOSOshop.BLL.PromptInfo.Popedom("012008004", "对不起，您没有权限进行编辑");                
                if (ChangeHope.WebPage.PageRequest.GetInt("id") > 0)
                {
                    BandInfo(ChangeHope.WebPage.PageRequest.GetInt("id"));
                }
                InitWebControl();
            }
        }

        #region 验证
        private void InitWebControl()
        {
            SOSOshop.BLL.SysParameter sp = new SOSOshop.BLL.SysParameter();
            this.txtArea.Attributes.Add("readonly", "readonly");
            this.txtArea.Attributes.Add("onclick", "selectFile('Area',new Array(" + this.hfAread.ClientID + "," + this.txtArea.ClientID + "),310,450,'" + sp.DummyPaht + "');");
            ChangeHope.WebPage.WebControl.Validate(this.txtSiteName, "输入链接网站的名称", "isnull", "必填", "该项为必填项");
            ChangeHope.WebPage.WebControl.Validate(this.txtSiteUrl, "输入网站的链接地址", "ishttpurl", "必填", "该项为必填项，注意链接地址格式");
            ChangeHope.WebPage.WebControl.Validate(this.txtSiteLevel, "请选择链接的优先级，数字越大优先级别越高", "isint", "必填", "该项必须为数字类型");
            ChangeHope.WebPage.WebControl.Validate(this.txtSiteClickCount, "输入数字将作为图片广告的宽", "isint", "必填", "该项必须为数字类型");
            this.Form.Attributes.Add("onsubmit", "return CheckForm()");
        }
        #endregion

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        /// <summary>
        /// 保存
        /// </summary>
        protected void Save()
        {
            YXShop.BLL.Accessories.Hailhellowlink bll = new YXShop.BLL.Accessories.Hailhellowlink();
            YXShop.Model.Accessories.Hailhellowlink model = new YXShop.Model.Accessories.Hailhellowlink();
            SOSOshop.BLL.SysParameter sp = new SOSOshop.BLL.SysParameter();
            model.SiteName = this.txtSiteName.Text.Trim();
            model.SiteUrl = this.txtSiteUrl.Text.Trim();
            if (this.txtSiteLogo.Text.Trim() != null && this.txtSiteLogo.Text.Trim() != "" && !this.fuSmallImages.HasFile)
            {
                model.SiteLogo = this.txtSiteLogo.Text.Trim();
            }
            else 
            {
                //获取图片
                ChangeHope.Common.UploadFile uf = new ChangeHope.Common.UploadFile();
                uf.ExtensionLim = ".gif,.jpg,.jpeg,.dmp";
                uf.FileLengthLim = sp.ImageSize;
                uf.PostedFile = this.fuSmallImages;
                uf.SavePath = "/yxuploadfile/brand";
                if (uf.Upload())
                {
                    if (uf.HaveLoad)
                    {
                        model.SiteLogo = uf.FilePath;
                    }
                    else
                    {
                        if (ViewState["SiteImages"] != null)
                        {
                            model.SiteLogo = ViewState["SiteImages"].ToString();
                        }
                        else
                        {
                            model.SiteLogo = string.Empty;
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
            //model.SiteLogo = this.txtSiteLogo.Text.Trim();
            model.SiteLevel = Convert.ToInt32(this.txtSiteLevel.Text.Trim());
            model.SiteContent = this.txtSiteContent.Text.Trim();
            model.SiteLinkType = Convert.ToInt32(this.rablistLinkType.SelectedValue);
            model.SiteState = Convert.ToInt32(this.rablistSiteState.SelectedValue);
            model.SiteClickCount = this.txtSiteClickCount.Text == string.Empty ? 0 : Convert.ToInt32(this.txtSiteClickCount.Text.Trim());
            model.Aread = this.hfAread.Value != string.Empty ? this.hfAread.Value : "0";
           





            if (ViewState["ID"] != null)
            {
                model.ID = Convert.ToInt32(ViewState["ID"].ToString());
                model.PutoutID = Convert.ToInt32(ViewState["PutoutID"]);
                model.PutoutTyID = Convert.ToInt32(ViewState["PutoutTyID"]);
                model.UpdateDate = DateTime.Now;
                model.CreateDate = Convert.ToDateTime(ViewState["CreateDate"].ToString());
                bll.Amend(model);
                this.ltlMsg.Text = "操作成功，已保存该信息";
                this.BandInfo(model.ID);//刷新
                this.pnlMsg.Visible = true;
                this.pnlMsg.CssClass = "actionOk";
            }
            else
            {
                //添加时的类型 
                model.PutoutTyID = 0;
                SOSOshop.Model.AdminInfo adminModel = (SOSOshop.Model.AdminInfo)SOSOshop.BLL.AdministrorManager.Get();
                //管理员ID
                model.PutoutID = adminModel.AdminId;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                bll.Add(model);
                this.ltlMsg.Text = "操作成功，已添加该信息";
                this.pnlMsg.Visible = true;
                this.pnlMsg.CssClass = "actionOk";
            }
        }

        /// <summary>
        /// 显示编辑信息
        /// </summary>
        /// <param name="id"></param>
        protected void BandInfo(int id)
        {
            YXShop.BLL.Accessories.Hailhellowlink bll = new YXShop.BLL.Accessories.Hailhellowlink();
            YXShop.Model.Accessories.Hailhellowlink model = bll.GetModelByID(id);
            hfAread.Value = model.Aread;
            this.txtArea.Text = AreaName(model.Aread);
            this.txtSiteName.Text = model.SiteName;
            this.txtSiteUrl.Text = model.SiteUrl;
            this.txtSiteLogo.Text = model.SiteLogo;
            this.txtSiteLevel.Text = model.SiteLevel.ToString();
            this.txtSiteContent.Text = model.SiteContent;
            this.rablistLinkType.SelectedValue = model.SiteLinkType.ToString();
            this.rablistSiteState.SelectedValue = model.SiteState.ToString();
            this.txtSiteClickCount.Text = model.SiteClickCount.ToString();
            ViewState["CreateDate"] = model.CreateDate.ToString();
            ViewState["UpdateDate"] = model.UpdateDate.ToString();
            ViewState["PutoutID"] = model.PutoutID.ToString();
            ViewState["PutoutTyID"] = model.PutoutTyID.ToString();
            ViewState["ID"] = model.ID.ToString();
            ViewState["SiteImages"] = model.SiteLogo;//
        }

        #region 绑定区域
        protected string AreaName(string IdStr)
        {
            string reStr = string.Empty;
            YXShop.BLL.SystemInfo.Provinces bll = new YXShop.BLL.SystemInfo.Provinces();
            DataTable dt = bll.ProvincesStr(IdStr);
            if (dt.Rows.Count>0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (reStr != string.Empty)
                    {
                        reStr += "," + dt.Rows[i]["CityName"].ToString();
                    }
                    else
                    {
                        reStr = dt.Rows[i]["CityName"].ToString();
                    }
                }
            }
            return reStr;
        }
        #endregion

       
     




    }
}
