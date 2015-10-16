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
using CuteEditor;
namespace _101shop.admin.v3.member
{
    public partial class MemberIntegralGift_Add : System.Web.UI.Page
    {
        public bool enabled_edit_everyone = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SOSOshop.BLL.PromptInfo.Popedom("008011012");
                this.ReturnUrl.Value = string.IsNullOrEmpty(Request["ReturnUrl"]) ? (Request.UrlReferrer == null ? "MemberIntegralGift.aspx" : Request.UrlReferrer.ToString()) : Request["ReturnUrl"];
                this.HyperLink1.NavigateUrl = this.ReturnUrl.Value;
            }
            this.Uploader1.FileUploaded += new CuteEditor.UploaderEventHandler(Uploader1_FileUploaded);
        }
        public void Uploader1_FileUploaded(object sender, UploaderEventArgs args)
        {
            ViewState["TempFilePath"] = args.GetTempFilePath();
            TextBox1.Text = args.FileName;
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void Save()
        {
            SOSOshop.BLL.Integral.MemberIntegralGift bll = new SOSOshop.BLL.Integral.MemberIntegralGift();

            string Name = this.txtName.Text.Trim();
            if (!string.IsNullOrEmpty(Name))
            {
                if (Name.Length < 2)
                {
                    this.ltlMsg.Text = "保存失败！礼品名称必须大于等于2个字！";
                    this.pnlMsg.CssClass = "actionErr";
                    pnlMsg.Visible = true;
                    return;
                }
                DataTable dt = bll.GetList("and Name='" + Name.Replace("'", "''") + "'");
                if (dt.Rows.Count > 0)
                {
                    this.ltlMsg.Text = "保存失败！已经有相同的礼品名称！";
                    this.pnlMsg.CssClass = "actionErr";
                    pnlMsg.Visible = true;
                    return;
                }
            }
            else
            {
                this.ltlMsg.Text = "保存失败！请填写礼品名称！";
                this.pnlMsg.CssClass = "actionErr";
                return;
            }

            string Detail = this.txtDetail.Text.Trim();
            if (!string.IsNullOrEmpty(Detail))
            {
                if (Detail.Length < 2)
                {
                    this.ltlMsg.Text = "保存失败！礼品说明必须大于等于2个字！";
                    this.pnlMsg.CssClass = "actionErr";
                    pnlMsg.Visible = true;
                    return;
                }
            }
            else
            {
                this.ltlMsg.Text = "保存失败！请填写礼品名称！";
                this.pnlMsg.CssClass = "actionErr";
                return;
            }

            decimal Integral = 0; decimal.TryParse(this.txtIntegral.Text.Trim(), out Integral);
            if (Integral <= 0)
            {
                this.ltlMsg.Text = "保存失败！请填写兑换积分！";
                this.pnlMsg.CssClass = "actionErr";
                return;
            }
            decimal Number = 0; decimal.TryParse(this.txtNumber.Text.Trim(), out Number);
            if (Number <= 0)
            {
                this.ltlMsg.Text = "保存失败！请填写可兑换数量！";
                this.pnlMsg.CssClass = "actionErr";
                return;
            }

            string Member_Class = ",";
            foreach (ListItem item in this.ckbMember_Class.Items)
            {
                if (item.Selected)
                {
                    Member_Class += item.Value + ",";
                }
            }
            if (Member_Class.Length <= 1)
            {
                this.ltlMsg.Text = "保存失败！请至少选择一个可兑换客户类型！";
                this.pnlMsg.CssClass = "actionErr";
                return;
            }

            SOSOshop.Model.Integral.MemberIntegralGift model = new SOSOshop.Model.Integral.MemberIntegralGift()
            {
                name = Name,
                detail = Detail,
                Integral = Integral,
                Number = Number,
                Member_Class = Member_Class,
                State = 1
            };

            try
            {
                int id = bll.Add(model);
                if (ViewState["TempFilePath"] != null)
                {
                    string item = ViewState["TempFilePath"].ToString();
                    string fileName = Server.MapPath("/JFimages/" + id + ".jpg");
                    System.IO.File.Copy(item, fileName);
                }

                if (id <= 0)
                {
                    this.ltlMsg.Text = "保存失败！";
                    this.pnlMsg.CssClass = "actionErr";
                }
                else
                {
                    this.ltlMsg.Text = "保存成功！";
                    this.pnlMsg.CssClass = "actionOk";

                    #region 后台用户操作日志记录
                    SOSOshop.Model.AdminInfo adminInfo = SOSOshop.BLL.AdministrorManager.Get();
                    SOSOshop.BLL.Logs.Log.LogAdminAdd("添加积分礼品", (adminInfo == null ? 0 : adminInfo.AdminId), (adminInfo == null ? "" : adminInfo.AdminName), 1);
                    #endregion
                }
                if (id > 0)
                {
                    this.ltlMsg.Text += "<script>setTimeout(function(){window.location='" + this.ReturnUrl.Value + "';},2000);</script>";
                    this.formtbl.Visible = false;
                    this.button1.Visible = false;
                    this.button2.Visible = false;
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
        }

    }
}
