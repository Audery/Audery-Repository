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
    public partial class article_read : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.Page.IsPostBack)
            {
                
                this.ltlLink.Text = "<a href='"+this.Request.UrlReferrer.ToString()+"'>返回资讯</a>";
                GetModel();
            }
        }
        private void GetModel()
        {
            int id = ChangeHope.WebPage.PageRequest.GetQueryInt("id");
            if(id>0)
            {
                this.ltlLink.Text += " | <a href='article_edit.aspx?id=" + id+ "'>编辑</a>";
                YXShop.BLL.SystemInfo.Article bll = new YXShop.BLL.SystemInfo.Article();
                YXShop.Model.SystemInfo.Article model = bll.GetModel(id);
                if (model != null)
                {
                    this.ltlTitle.Text = "标题：" + model.Title;
                    this.ltlContent.Text = model.Content;
                    this.lblMark.Text = "";
                    this.lblMark.Text += "<font color='Green'>";
                    this.lblMark.Text += "<br/><b>作者</b>:" + model.Author;
                    this.lblMark.Text += "<br/><b>发布人</b>:" + model.Users;
                    this.lblMark.Text += "<br/><b>更新人</b>:" + model.Editor;
                    this.lblMark.Text += "<br/><b>发布时间</b>:" + model.CreateTime;

                    this.lblMark.Text += "<br/><b>更新时间</b>:" + model.UpdateTime;
                    this.lblMark.Text += "<br/><b>关键字</b>:" + model.KeyWord;
                    this.lblMark.Text += "<br/><b>来源</b>:" + model.CopyFrom;

                    this.lblMark.Text += "</font><hr size='1'color='#EEEEEE'/>";
                }
                model = null;
                bll = null;
            }
        }
    }
}
