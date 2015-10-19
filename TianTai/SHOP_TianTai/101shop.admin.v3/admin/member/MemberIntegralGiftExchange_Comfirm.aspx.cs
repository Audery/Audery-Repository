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
using System.Text;

namespace _101shop.admin.v3.member
{
    public partial class MemberIntegralGiftExchange_Comfirm : System.Web.UI.Page
    {
        public bool enabled_edit_everyone = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region ajax 状态处理
                if (!string.IsNullOrEmpty(Request.QueryString["ajax"]))
                {
                    if (SOSOshop.BLL.PowerPass.isPass("008011014"))
                    {
                        SOSOshop.Model.AdminInfo adminInfo = SOSOshop.BLL.AdministrorManager.Get();
                        int id = 0; int.TryParse(Request.Form["id"], out id);
                        string act = Convert.ToString(Request.Form["act"]);//取消,已邮寄
                        bool ok = false;
                        if ("QuXiao" == act)
                        {
                            ok = QuXiao(id);
                        }
                        else if ("YiYouji" == act)
                        {
                            ok = YiYouji(id);
                        }
                        else if ("YiChuli" == act)
                        {
                            ok = YiChuli(id);
                        }
                        if (ok)
                        {
                            Response.Write("{\"state\":1,\"message\":\"处理成功！\"}");
                        }
                        else
                        {
                            Response.Write("{\"state\":0,\"message\":\"处理失败！\"}");
                        }
                    }
                    else
                    {
                        Response.Write("{\"state\":-1,\"message\":\"对不起，您没有编辑权限，请联系管理员！\"}");
                    }
                    Response.End();
                }
                #endregion

                SOSOshop.BLL.PromptInfo.Popedom("008011014");

                this.ReturnUrl.Value = string.IsNullOrEmpty(Request["ReturnUrl"]) ? (Request.UrlReferrer == null ? "MemberIntegralGiftExchange.aspx" : Request.UrlReferrer.ToString()) : Request["ReturnUrl"];
                this.HyperLink1.NavigateUrl = this.ReturnUrl.Value;
                this.HyperLink2.NavigateUrl = this.ReturnUrl.Value;
                //取得要兑换的积分礼品
                GetMode();
            }
        }
        /// <summary>
        /// 取消操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool QuXiao(int id)
        {
            SOSOshop.Model.AdminInfo adminInfo = SOSOshop.BLL.AdministrorManager.Get();
            string s = string.Format(@"declare @uid int,@Gift_ID int,@Gift_Number decimal,@Integral decimal 
if exists(select * from MemberIntegralGiftExchange where [State]<>0 and [id]={0}) begin 
update MemberIntegralGiftExchange set ontime=getdate(),Editer={1},[State]=0 
where [State]<>0 and [id]={0} 
select @uid = uid, @Gift_ID = Gift_ID, @Gift_Number = Gift_Number from MemberIntegralGiftExchange where [id]={0}
select @Integral = Integral * @Gift_Number from MemberIntegralGift where [id]=@Gift_ID 
update MemberIntegralGift set Number = Number + @Gift_Number where [id]=@Gift_ID
update MemberIntegral set realityIntegral = realityIntegral + @Integral where [uid]=@uid 
end
select @uid,@Integral", id, adminInfo == null ? 0 : adminInfo.AdminId);
            SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();
            DataSet ds = db.ExecuteDataSet(s);
            bool ok = ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && !ds.Tables[0].Rows[0].IsNull(0);
            if (ok)
            {
                int uid = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                decimal integral = decimal.Parse(ds.Tables[0].Rows[0][1].ToString());
                SOSOshop.BLL.Integral.MemberIntegral bll2 = new SOSOshop.BLL.Integral.MemberIntegral();
                bll2.PresentIntegral(uid, Convert.ToInt32(integral), "返还兑换礼品的积分", true);
            }
            return ok;
        }
        /// <summary>
        /// 已邮寄操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool YiYouji(int id)
        {
            SOSOshop.Model.AdminInfo adminInfo = SOSOshop.BLL.AdministrorManager.Get();
            string s = string.Format(@"update MemberIntegralGiftExchange set ontime=getdate(),Editer={1},[State]=3 where [State]<>0 and [id]={0}",
                id, adminInfo == null ? 0 : adminInfo.AdminId);
            SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();
            return 0 < db.ExecuteNonQuery(s);
        }
        /// <summary>
        /// 待处理操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DaiChuli(int id)
        {
            SOSOshop.Model.AdminInfo adminInfo = SOSOshop.BLL.AdministrorManager.Get();
            string s = string.Format(@"update MemberIntegralGiftExchange set ontime=getdate(),Editer={1},[State]=1 where [State]<>0 and [id]={0}",
                id, adminInfo == null ? 0 : adminInfo.AdminId);
            SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();
            return 0 < db.ExecuteNonQuery(s);
        }
        /// <summary>
        /// 已处理操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool YiChuli(int id)
        {
            SOSOshop.Model.AdminInfo adminInfo = SOSOshop.BLL.AdministrorManager.Get();
            string s = string.Format(@"update MemberIntegralGiftExchange set ontime=getdate(),Editer={1},[State]=2 where [State]<>0 and [id]={0}",
                id, adminInfo == null ? 0 : adminInfo.AdminId);
            SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();
            return 0 < db.ExecuteNonQuery(s);
        }
        /// <summary>
        /// 取得要兑换的积分礼品
        /// </summary>
        private void GetMode()
        {
            int id = 0; int.TryParse(Request["id"], out id);
            SOSOshop.BLL.Integral.MemberIntegralGiftExchange bll1 = new SOSOshop.BLL.Integral.MemberIntegralGiftExchange();
            SOSOshop.Model.Integral.MemberIntegralGiftExchange model1 = null;
            SOSOshop.BLL.Integral.MemberIntegralGift bll2 = new SOSOshop.BLL.Integral.MemberIntegralGift();
            SOSOshop.Model.Integral.MemberIntegralGift model2 = null;
            string err = "";
            if (id <= 0 || (model1 = bll1.GetModel(id)) == null)
            {
                err = "请选择要处理的兑换礼品申请";
            }
            else
            {
                if ((model2 = bll2.GetModel(model1.Gift_ID)) == null || model2.State == 0)
                {
                    err = "无法处理兑换礼品申请，不存在此礼品";
                }
                else if (model2.State == 2)
                {
                    err = "无法处理兑换礼品申请，此礼品已下架";
                }
                //已经预扣了数量，这里就不用判断数量了
                //else if (model2.Number < model1.Gift_Number)
                //{
                //    err = "无法处理兑换礼品申请，此礼品可兑换数量小于兑换数量";
                //}
            }
            if (err != "")
            {
                this.pnlMsg.Visible = true;
                this.ltlMsg.Text += "<script>setTimeout(function(){alert('" + err + "');window.location='" + this.ReturnUrl.Value + "';},2000);</script>";
                this.button1.Visible = false;
                this.button2.Visible = false;
                return;
            }

            this.txtId.Value = model1.id.ToString();
            this.txtUID.Value = model1.uid.ToString();
            this.txtTruename.Text = model1.truename;
            this.txtPhone.Text = model1.phone;
            this.txtCompanyName.Text = model1.CompanyName;
            this.txtGift_Number.Text = Convert.ToInt32(model1.Gift_Number).ToString();
            this.txtConsigneeAddress.Text = model1.ConsigneeAddress;
            this.txtConsigneeName.Text = model1.ConsigneeName;
            this.txtConsigneePhone.Text = model1.ConsigneePhone;
            this.ddlState.SelectedValue = model1.State.ToString();
            this.txtRemark.Text = model1.Remark;

            this.txtGId.Value = model2.id.ToString();
            this.txtName.Text = model2.name;
            this.txtDetail.Text = model2.detail;
            this.txtIntegral.Text = model2.Integral.ToString();
            this.txtNumber.Text = Convert.ToInt32(model2.Number).ToString();
            string mc = ""; foreach (ListItem item in this.ckbMember_Class.Items) if (model2.Member_Class.Contains("," + item.Value + ",")) mc += item.Text + ",";
            this.txtMember_Class.Text = mc.TrimEnd(',');
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void Save()
        {
            SOSOshop.BLL.Integral.MemberIntegralGiftExchange bll1 = new SOSOshop.BLL.Integral.MemberIntegralGiftExchange();
            SOSOshop.Model.Integral.MemberIntegralGiftExchange model1 = null;
            SOSOshop.BLL.Integral.MemberIntegralGift bll2 = new SOSOshop.BLL.Integral.MemberIntegralGift();
            SOSOshop.Model.Integral.MemberIntegralGift model2 = null;
            //处理状态修改
            int State = int.Parse(this.ddlState.SelectedValue);

            try
            {
                bool ok = true;
                string err = "";
                model1 = bll1.GetModel(int.Parse(this.txtId.Value));
                if ((model2 = bll2.GetModel(model1.Gift_ID)) == null || model2.State == 0)
                {
                    err = "无法处理兑换礼品申请，不存在此礼品";
                }
                else if (model2.State == 2)
                {
                    err = "无法处理兑换礼品申请，此礼品已下架";
                }
                //已预扣数量，不用判断
                //else if (model2.Number < model1.Gift_Number)
                //{
                //    err = "无法处理兑换礼品申请，此礼品可兑换数量小于兑换数量";
                //}
                else
                {
                    if (model1.State != State)
                    {
                        if (model1.State == 0)
                        {
                            var db = bll1._db;
                            using (var conn = db.CreateConnection())
                            {
                                conn.Open();
                                var tran = conn.BeginTransaction();
                                try
                                {
                                    SOSOshop.Model.AdminInfo adminInfo = SOSOshop.BLL.AdministrorManager.Get();
                                    string s = string.Format(@"if exists(select * from MemberIntegralGiftExchange where [State]=0 and [id]={0}) begin 
update MemberIntegralGiftExchange set ontime=getdate(),Editer={1},[State]={2} 
where [State]=0 and [id]={0} 
declare @uid int,@Gift_ID int,@Gift_Number decimal,@Integral decimal  
select @uid = uid, @Gift_ID = Gift_ID, @Gift_Number = Gift_Number from MemberIntegralGiftExchange where [id]={0}
select @Integral = Integral * @Gift_Number from MemberIntegralGift where [id]=@Gift_ID 
update MemberIntegralGift set Number = Number - @Gift_Number where [id]=@Gift_ID
update MemberIntegral set realityIntegral = realityIntegral - @Integral where [uid]=@uid 
end", model1.id, adminInfo == null ? 0 : adminInfo.AdminId, State);
                                    var dbCommand = db.GetSqlStringCommand(s);
                                    db.ExecuteScalar(dbCommand, tran);
                                    tran.Commit();
                                }
                                catch (Exception e)
                                {
                                    tran.Rollback();
                                    err = e.Message;
                                    ok = false;
                                }
                            }
                        }
                        else
                        {
                            if (State == 0) ok = QuXiao(model1.id);
                            if (State == 1) ok = DaiChuli(model1.id);
                            if (State == 2) ok = YiChuli(model1.id);
                            if (State == 3) ok = YiYouji(model1.id);
                            if (ok && !string.IsNullOrEmpty(this.txtRemark.Text.Trim()))
                            {
                                var db = bll1._db;
                                using (var conn = db.CreateConnection())
                                {
                                    conn.Open();
                                    try
                                    {
                                        SOSOshop.Model.AdminInfo adminInfo = SOSOshop.BLL.AdministrorManager.Get();
                                        string s = "update MemberIntegralGiftExchange set remark=@remark where [id]=" + model1.id;
                                        var dbCommand = db.GetSqlStringCommand(s);
                                        db.AddInParameter(dbCommand, "remark", DbType.AnsiString, this.txtRemark.Text.Trim());
                                        db.ExecuteNonQuery(dbCommand);
                                    }
                                    catch (Exception e)
                                    {
                                        err = e.Message;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        this.pnlMsg.Visible = true;
                        this.ltlMsg.Text = "未进行任何处理！";
                        this.pnlMsg.CssClass = "actionErr";
                        this.ltlMsg.Text += "<script>setTimeout(function(){window.location='" + this.ReturnUrl.Value + "';},2000);</script>";
                        this.button1.Visible = false;
                        this.button2.Visible = false;
                        return;
                    }
                }

                this.pnlMsg.Visible = true;
                if (!ok)
                {
                    this.ltlMsg.Text = "保存失败！" + err;
                    this.pnlMsg.CssClass = "actionErr";
                }
                else
                {
                    this.ltlMsg.Text = "保存成功！";
                    this.pnlMsg.CssClass = "actionOk";

                    #region 后台用户操作日志记录
                    SOSOshop.Model.AdminInfo adminInfo = SOSOshop.BLL.AdministrorManager.Get();
                    SOSOshop.BLL.Logs.Log.LogAdminAdd("处理兑换礼品申请[id:" + model1.id + ",状态改变:" + State + "]", (adminInfo == null ? 0 : adminInfo.AdminId), (adminInfo == null ? "" : adminInfo.AdminName), 1);
                    #endregion
                }
                if (ok)
                {
                    this.ltlMsg.Text += "<script>setTimeout(function(){window.location='" + this.ReturnUrl.Value + "';},2000);</script>";
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
            }
        }
    }
}
