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
using System.Text;

namespace _101shop.admin.v3.systeminfo
{
    public partial class articlechannel_edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ChangeHope.DataBase.SQLServerHelper.connectionString = new SOSOshop.BLL.Db()._db.ConnectionString;
            if (!this.Page.IsPostBack)
            {
                SOSOshop.BLL.PromptInfo.Popedom("009001002", "对不起，您没有权限进行编辑");
                SOSOshop.BLL.PromptInfo.Popedom("009001004", "对不起，您没有权限进行编辑");
                string channelid = ChangeHope.WebPage.PageRequest.GetQueryString("chanelid");
                this.txtParentId.Value = channelid;
                GetParentChannel(channelid); 
                InitWebControls();
                GetModel();
            }
        }
        private void GetModel()
        {
            string id = ChangeHope.WebPage.PageRequest.GetQueryString("id");
            YXShop.BLL.SystemInfo.ArticleChannel bll = new YXShop.BLL.SystemInfo.ArticleChannel();
            YXShop.Model.SystemInfo.ArticleChannel model = bll.GetModel(id);
            if(model!=null)
            {
                this.txtDescription.Text = model.Description;
                this.txtId.Value = model.Id;
                this.txtName.Text = model.Name;
                this.txtShop.SelectedValue = model.Shop;
                this.ckbType.SelectedValue = model.Type;
                if (model.Type == "2")
                {
                    Page.RegisterStartupScript("ggg", "<script>changetype(2);</script>");
                }
                this.txtProjectName.Text = model.ProjectName;
                this.txtProjectUtil.Text = model.ProjectUtil;
                this.ckbTarget.SelectedValue = model.Target;
                this.txtAddress.Text = model.ExternalLink;
                this.txtWebPagePath.Text = model.DefaultTemplate;
                this.txtListPageTemplate.Text = model.ListTemplate;
                this.txtContentPageTemplate.Text = model.ContentTemplate;
                //this.txtTemplate.Text = model.Template;
                this.txtMeteKey.Text = model.MeteKey;
                this.txtMeteDescription.Text = model.MeteDescription;
                this.txtWebPagePath.Text = model.DefaultTemplate;
                this.txtListPageTemplate.Text = model.ListTemplate;
                this.txtContentPageTemplate.Text = model.ContentTemplate;
                
                foreach (ListItem item in this.ckbPower.Items)
                {
                    if (model.Power.IndexOf(item.Value+",") >= 0)
                    {
                        item.Selected = true;
                    }
                }
            }
            model = null;
            bll = null;
        }
        private void InitWebControls()
        {          
            ChangeHope.WebPage.WebControl.Validate(this.txtName,"栏目名称，栏目下面可以设置子栏目,栏目名称设置为4~10个字符","isnull_4_10","必填","该项为必填项");
            //ChangeHope.WebPage.WebControl.Validate(this.txtProjectName, "比如是文章的话，项目就是文章或者新闻或者通讯等", "isnull", "必填", "该项为必填项");
            //ChangeHope.WebPage.WebControl.Validate(this.txtProjectUtil, "该栏目的文章的单位，比如是文章的话就是X篇，通讯的话就是X条等", "isnull", "必填", "该项为必填项");
            //ChangeHope.WebPage.WebControl.Validate(this.txtTemplate, "该栏目的网页模板，按照要求制定，选择文件夹", "isnull", "必填", "该项为必填项");
            //ChangeHope.WebPage.WebControl.Validate(this.txtMeteKey, "该栏目的文章的单位，比如是文章的话就是X篇，通讯的话就是X条等", "isnull", "必填", "该项为必填项");
            //ChangeHope.WebPage.WebControl.Validate(this.txtMeteDescription, "该栏目的文章的单位，比如是文章的话就是X篇，通讯的话就是X条等", "isnull", "必填", "该项为必填项");
            //this.ckbType.Items[0].Attributes.Add("onclick", "changetype(this.value)");
            //this.ckbType.Items[1].Attributes.Add("onclick", "changetype(this.value)");
            //this.ckbType.Items[2].Attributes.Add("onclick", "changetype(this.value)");
            this.Form.Attributes.Add("onsubmit","return CheckForm();");

            //this.txtWebPagePath.Attributes.Add("onclick", "GetFile(this)");
            //this.txtWebPagePath.Attributes.Add("readonly", "readonly");
            //this.txtListPageTemplate.Attributes.Add("onclick", "GetFile(this)");
            //this.txtListPageTemplate.Attributes.Add("readonly", "readonly");
            //this.txtContentPageTemplate.Attributes.Add("onclick", "GetFile(this)");
            //this.txtContentPageTemplate.Attributes.Add("readonly", "readonly");

            //this.txtTemplate.Attributes.Add("onclick", "GetFile(this)");
            //this.txtTemplate.Attributes.Add("readonly","readonly");
            GetMemberRank();
            //GetFileList();
            //GetFileList1();
        }

        private void GetFileList1()
        {
            /*
            修改人：ym
            修改时间：2009-8-26
            修改内容：添加
             */
            SOSOshop.BLL.SysParameter sp = new SOSOshop.BLL.SysParameter();
            ChangeHope.Common.FileHelper file = new ChangeHope.Common.FileHelper();

            StringBuilder filelist = new StringBuilder();
            file.rootUrl = Server.MapPath("~/"+ sp.WebSiteTemplatePath);
            file.listFiles(file.rootUrl, 0);

            filelist.AppendLine("<script type=\"text/javascript\">");
            filelist.AppendLine("d = new dTree('d');");
            filelist.AppendLine("d.add(0,-1,'请选择模版');");
            filelist.AppendLine(file.fileTree.ToString());
            filelist.AppendLine("document.write(d);");
            filelist.AppendLine("$(\"fileLists\").style.visibility=\"hidden\";");
            filelist.AppendLine("</script>");
            this.ltlFileList.Text = filelist.ToString();
            file = null;
        }


        private void GetMemberRank()
        {
            //YXShop.BLL.Member.MemberRank bll = new YXShop.BLL.Member.MemberRank();
            //System.Data.DataTable tb = bll.GetList();
            
            //foreach(System.Data.DataRow row in tb.Rows)
            //{
            //    this.ckbPower.Items.Add(new ListItem(row["Name"].ToString(), row["Id"].ToString()));
            //}
            //tb.Dispose();
            //tb = null;
        }
       
        //private void GetFileList()
        //{
        //    SOSOshop.BLL.SysParameter sp = new SOSOshop.BLL.SysParameter();
        //    ChangeHope.Common.FileHelper file = new ChangeHope.Common.FileHelper();
        //    StringBuilder filelist = new StringBuilder();
        //    file.rootUrl = Server.MapPath("~/" + sp.DummyPaht + sp.WebSiteTemplatePath + "/banner");
        //    file.listFileName(file.rootUrl, 0);           
        //    filelist.AppendLine("<script type=\"text/javascript\">");
        //    filelist.AppendLine("d = new dTree('d');");
        //    filelist.AppendLine("d.add(0,-1,'请选择栏目模版文件夹');");
        //    filelist.AppendLine(file.fileTree.ToString());
        //    filelist.AppendLine("document.write(d);");
        //    filelist.AppendLine("$(\"fileLists\").style.visibility=\"hidden\";");
        //    filelist.AppendLine("</script>");
        //    this.ltlFileList.Text = filelist.ToString();
        //    file = null;
        //}

        private void GetParentChannel(string channelid)
        {
            YXShop.BLL.SystemInfo.ArticleChannel bll = new YXShop.BLL.SystemInfo.ArticleChannel();
            StringBuilder text = new StringBuilder();
            SortedList hashtable = bll.GetChannel(channelid);
            foreach (DictionaryEntry de in hashtable)
            {
                text.Append("→"+de.Value.ToString());
            }
            hashtable.Clear();
            hashtable = null;
            this.Label1.Text = text.ToString().Substring(1);
            bll = null;
            
        }
        private void Save()
        {
            YXShop.BLL.SystemInfo.ArticleChannel bll = new YXShop.BLL.SystemInfo.ArticleChannel();
            YXShop.Model.SystemInfo.ArticleChannel model = new YXShop.Model.SystemInfo.ArticleChannel();
            model.Description = this.txtDescription.Text;
            model.Name = this.txtName.Text;
            model.Shop = this.txtShop.SelectedValue;
                  model.Type = this.ckbType.SelectedValue;
            model.ProjectName = this.txtProjectName.Text;
            model.ProjectUtil = this.txtProjectUtil.Text;
            model.Target = this.ckbTarget.SelectedValue;
            model.ExternalLink = this.txtAddress.Text.Trim().ToString();
            model.MeteKey = this.txtMeteKey.Text;
            model.MeteDescription = this.txtMeteDescription.Text;
            model.DefaultTemplate = this.txtWebPagePath.Text;
            model.ListTemplate = this.txtListPageTemplate.Text;
            model.ContentTemplate = this.txtContentPageTemplate.Text;
            model.Power = "";
            foreach (ListItem item in this.ckbPower.Items)
            {
                if (item.Selected)
                {
                    model.Power = model.Power  + item.Value+ ",";
                }
            }

            if(this.txtId.Value.Equals(""))
            {
                this.txtId.Value = bll.GetMaxId(this.txtParentId.Value);
            }
            model.Id = txtId.Value;
            SOSOshop.Model.AdminInfo adminInfo = (SOSOshop.Model.AdminInfo)SOSOshop.BLL.AdministrorManager.Get();
            if (adminInfo != null)
            {
                model.Users = adminInfo.AdminName;
            }
            if (bll.Exists(model.Id))
            {
                bll.Update(model);          
            }
            else
            {
                bll.Add(model);
            }
            
            model = null;
            bll = null;
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Save();
            this.Response.Redirect("articlechannel_list.aspx?q_chanelid=" + ChangeHope.Common.StringHelper.SubString(this.txtId.Value, this.txtId.Value.Length-3));
        }
    }

}
