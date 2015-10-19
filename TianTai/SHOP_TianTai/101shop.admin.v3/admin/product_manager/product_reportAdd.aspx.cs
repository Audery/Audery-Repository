using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CuteEditor;

namespace _101shop.admin.v3.admin.product_manager
{
    public partial class product_reportAdd : SOSOshop.WEB.UI.ManageBasePage
    {
        SOSOshop.BLL.Report.DrugTestingReport bll = new SOSOshop.BLL.Report.DrugTestingReport();
        SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (isBrowse())
            {
                if (!IsPostBack)
                {
                    init();
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
            //string sql = string.Format("SELECT pihao FROM dbo.Product_Centre WHERE product_id={0} UNION ALL SELECT pihao FROM phspkc WHERE spid=(SELECT spid FROM dbo.Product WHERE Product_ID={0})", Product_ID);
            //foreach (System.Data.DataRow item in db.ExecuteTable(sql).Rows)
            //{
            //    if (item["pihao"].ToString().Trim() != "")
            //    {
            //        if (!bll.Exist(Product_ID, item["pihao"].ToString().Trim()))
            //        {
            //            bll.created = DateTime.Now;
            //            bll.dowCount = 0;
            //            bll.file = "";
            //            bll.pihao = item["pihao"].ToString().Trim();
            //            bll.Products_Id = Product_ID;
            //            bll.Insert();
            //        }
            //    }
            //}
            tablist.DataSource = bll.GetList(Product_ID);
            tablist.DataBind();
        }
        public override void SetModuleTag()
        {
            base.ModuleAdd = "001020002";
            base.ModuleBrowse = "001020001";
            base.ModuleEdit = "001020004";
            base.ModuleDelete = "001020003";
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
        protected void Edit_Click(object sender, EventArgs e)
        {
            var model = bll.GetModle(((LinkButton)(sender)).CommandArgument);
            TextBox1.Text = model.pihao;
            HiddenField2.Value = model.id;
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
            int Product_ID = int.Parse(Request.QueryString["id"]);
            if (Library.Lang.DataValidator.isNULL(TextBox1.Text))
            {
                Library.Client.Jscript.Alert(this.Page, "数据请填写完整!");
                return;
            }
            bll.created = DateTime.Now;
            bll.dowCount = 0;
            if (HiddenField1.Value != "")
            {
                SOSOshop.BLL.Common.ImageWater.AddPicWatermarkAsJPG(HiddenField1.Value, Server.MapPath("/bin/101zhang.png"), HiddenField1.Value+"_", SOSOshop.BLL.Common.ImageWater.MarkPosition.MP_Left_Top);
                bll.file = bll.AddFile(HiddenField1.Value+"_");
            }
            else
            {
                bll.file = "";
            }
            bll.pihao = TextBox1.Text.Trim();
            bll.Products_Id = Product_ID;
            bll.id = HiddenField2.Value;
            bll.iden = 101;
            HiddenField2.Value = "";
            bll.Update();
            init();
        }
    }


}