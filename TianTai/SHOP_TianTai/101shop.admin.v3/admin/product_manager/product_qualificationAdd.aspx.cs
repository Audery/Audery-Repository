using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CuteEditor;

namespace _101shop.admin.v3.admin.product_manager
{
    public partial class product_qualificationAdd : SOSOshop.WEB.UI.ManageBasePage
    {
        SOSOshop.BLL.Report.Qualification bll = new SOSOshop.BLL.Report.Qualification();
        SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();

        protected void Page_Load(object sender, EventArgs e)
        {
        
            if (isBrowse())
            {
                if (!IsPostBack)
                {
                    init();
                    hidProductName.Value = Request.QueryString["ProductName"];
                    hidFactoryName.Value = Request.QueryString["FactoryName"];
                    hidCodeNum.Value = Request.QueryString["CodeNum"];
                    hidIsUpload.Value = Request.QueryString["IsUpload"];
                    hidIsForHead.Value = Request.QueryString["IsForHead"];
                }
                Uploader1.FileUploaded += new CuteEditor.UploaderEventHandler(Uploader1_FileUploaded);
            }

        }
        public void Uploader1_FileUploaded(object sender, UploaderEventArgs args)
        {
            HiddenField1.Value = args.GetTempFilePath();
            TextBox2.Text = args.FileName;
        }
        public void init()
        {
            int Product_ID = int.Parse(Request.QueryString["id"]);
            Label1.Text = db.ExecuteScalar("SELECT Product_Name FROM dbo.Product WHERE Product_ID=" + Product_ID) as string;
            tablist.DataSource = bll.GetList(Product_ID);
            tablist.DataBind();

            DropDownList1.DataSource = SOSOshop.BLL.Report.Qualification.GetQualList();
            DropDownList1.DataTextField = "name";
            DropDownList1.DataValueField = "id";
            DropDownList1.DataBind();
        }
        public override void SetModuleTag()
        {
            base.ModuleAdd = "001040002";
            base.ModuleBrowse = "001040001";
            base.ModuleEdit = "001040004";
            base.ModuleDelete = "001040003";
        }
        /// <summary>
        /// 取得上传文件状态
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetFile(object obj)
        {
            if (string.IsNullOrEmpty(obj as string))
            {
                return "未上传";
            }
            return string.Format("<a target=\"_blank\" href=\"MFile.ashx?file={0}\">查看</a>", obj);
        }

        public string GetQualType(int obj)
        {
            return SOSOshop.BLL.Report.Qualification.GetQualList().Where(x => x.id == obj).First().name;
        }

        protected void Edit_Click(object sender, EventArgs e)
        {
            var model = bll.GetModle(((LinkButton)(sender)).CommandArgument);
            txtoutOfDate.Text = model.outOfDate.ToLocalTime().ToString("yyyy-MM-dd");
            TextBox2.Text = model.file;
            DropDownList1.SelectedValue = model.QualType.ToString();
            hidUpdateId.Value = model.id;
        }
        protected void Delte_Click(object sender, EventArgs e)
        {
            bll.Delte(((LinkButton)(sender)).CommandArgument);
            init();
        }
        /// <summary>
        /// 上传药检报告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SearchManage_Click(object sender, EventArgs e)
        {
            if (!isAdd())
            {
                Library.Client.Jscript.Alert(this.Page, "您没有添加权限!");
                return;
            }
             DateTime outOfDate = DateTime.MinValue;
           
             if (!DateTime.TryParse(txtoutOfDate.Text, out outOfDate))
             {
               
                 if (txtoutOfDate.Text.Length == 8)
                 {
                     string time = txtoutOfDate.Text.Substring(0, 4) + "-" + txtoutOfDate.Text.Substring(4, 2)+"-"+txtoutOfDate.Text.Substring(6, 2);
                     DateTime.TryParse(time, out outOfDate);
                 }
                     //else{
                     //    Response.Write("<script>alert('请输入正确的时间格式，如：20150707')</script>");
                     //    return;
                     //}
             }
           string url="product_qualification.aspx?ProductName=" + hidProductName.Value + "&FactoryName=" + hidFactoryName.Value + "&CodeNum=" + hidCodeNum.Value + "&IsUpload=" + hidIsUpload.Value + "&IsForHead=" + hidIsForHead.Value + "";
            //修改
            if (hidUpdateId.Value.Length > 4)
            {
               bll= bll.GetModle(hidUpdateId.Value);
               bll.outOfDate = outOfDate;
               bll.created = DateTime.Now;
               if (HiddenField1.Value != "")
               {
                   bll.file = bll.AddFile(HiddenField1.Value, System.IO.Path.GetExtension(TextBox2.Text));
               }
               bll.QualType = int.Parse(DropDownList1.SelectedValue);
               bll.editer = base.UserName;
               bll.Update();
               ShowRight(url, false);
            }
            else //新增
            {
                int Product_ID = int.Parse(Request.QueryString["id"]);

                bll.created = DateTime.Now;
               
                bll.outOfDate = outOfDate;
                bll.dowCount = 0;
                if (HiddenField1.Value != "")
                {
                    bll.file = bll.AddFile(HiddenField1.Value, System.IO.Path.GetExtension(TextBox2.Text));
                }
                else
                {
                    ShowError("文件不能为空!");
                    return;
                }

                LogAdd("上传了或修改了商品资质:商品编号：{0},资质类型：{1}", bll.Products_Id, DropDownList1.SelectedItem.Text);
                bll.Products_Id = Product_ID;
                HiddenField2.Value = "";
                bll.QualType = int.Parse(DropDownList1.SelectedValue);
                bll.editer = base.UserName;
                bll.Insert();
                init();
                ShowRight(url, false);
            }

            
        }
    }


}