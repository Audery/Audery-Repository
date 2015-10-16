using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _101shop.admin.v3.admin.member
{
    public partial class Buyer_Permissions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SOSOshop.BLL.PromptInfo.Popedom("008009014", "对不起，您没有权限进行查看");
                int uid = 0; int.TryParse(Request["UID"], out uid);
                SOSOshop.Model.MemberAccount a = null;
                SOSOshop.Model.MemberInfo b = null;
                SOSOshop.Model.MemberPermission c = null;
                if (uid > 0)
                {
                    //查询数据
                    a = new SOSOshop.BLL.MemberAccount().GetModel(uid);
                    if (a != null) b = new SOSOshop.BLL.MemberInfo().GetModel(uid);
                    if (a != null) c = new SOSOshop.BLL.MemberPermission().GetModelWithNoCache(uid);
                }
                if (a != null && b != null && c != null)
                {
                    //买家ID
                    this.hfUID.Value = uid.ToString();
                    //买家单位
                    this.ltlMemberinfo.Text = string.Format("<b>{0}</b> &nbsp;&nbsp;<b>{1}</b> &nbsp; <span>（类别：{2} &nbsp; {3}）</span>",
                        new SOSOshop.BLL.DbBase().ExecuteScalar("select Name from DrugsBase_Enterprise where ID=" + b.ParentId),
                        b.TrueName,
                        a.UserType >= 0 ? Enum.GetName(typeof(SOSOshop.Model.MemberKeyValue.UserType), a.UserType) : "",
                        b.Member_Class >= 0 ? Enum.GetName(typeof(SOSOshop.Model.MemberKeyValue.Member_Class), b.Member_Class) : "");

                    //权限列表
                    this.cb_IsCOD.SelectedValue = c.IsCOD ? "1" : "0";
                    this.cb_IsLookPrice_01.SelectedValue = c.IsLookPrice_01 ? "1" : "0";
                    this.cb_IsLookPrice_02.SelectedValue = c.IsLookPrice_02 ? "1" : "0";
                    this.cb_IsLookProduct_01.SelectedValue = c.IsLookProduct_01 ? "1" : "0";
                    this.cb_IsLookProduct_02.SelectedValue = c.IsLookProduct_02 ? "1" : "0";
                    this.cb_IsLookStock.SelectedValue = c.IsLookStock ? "1" : "0";
                    this.cb_IsMoneyAndShipping.SelectedValue = c.IsMoneyAndShipping ? "1" : "0";
                    this.cb_IsPeriodicalSettle.SelectedValue = c.IsPeriodicalSettle ? "1" : "0";
                    this.cb_IsTrade.SelectedValue = c.IsTrade ? "1" : "0";
                    this.cb_IsPriorDistribution.SelectedValue = c.IsPriorDistribution ? "1" : "0";
                    this.cb_IsShippingFor48h.SelectedValue = c.IsShippingFor48h ? "1" : "0";
                    this.cb_IsSpecialTrade.SelectedValue = c.IsSpecialTrade ? "1" : "0";

                    //授权
                    bool isCheckUp = SOSOshop.BLL.PowerPass.isPass("008009014");
                    this.cb_IsCOD.Enabled = isCheckUp;
                    this.cb_IsLookPrice_01.Enabled = isCheckUp;
                    this.cb_IsLookPrice_02.Enabled = isCheckUp;
                    this.cb_IsLookProduct_01.Enabled = isCheckUp;
                    this.cb_IsLookProduct_02.Enabled = isCheckUp;
                    this.cb_IsLookStock.Enabled = isCheckUp;
                    this.cb_IsMoneyAndShipping.Enabled = isCheckUp;
                    this.cb_IsPeriodicalSettle.Enabled = isCheckUp;
                    this.cb_IsTrade.Enabled = isCheckUp;
                    this.cb_IsPriorDistribution.Enabled = isCheckUp;
                    this.cb_IsShippingFor48h.Enabled = isCheckUp;
                    this.cb_IsSpecialTrade.Enabled = isCheckUp;
                    //建档通过
                    bool bBuyFilingStatus = 1 == _101shop.admin.v3.member.BuyerLib.GetGSP(uid);
                    this.cb_IsSpecialTrade.Enabled = !bBuyFilingStatus && a.State == 0;//已审核的未建档的才能开通快捷交易的权限

                    //权限【拥有快捷开通交易的权限】, 第一次建档状态.通过 > 允许已经建档通过的会员的定单可以执行流程
                    int UID_BuyFilingStatus = (bBuyFilingStatus ? 1 : 0);
                    //var lbll = new SOSOshop.BLL.Logs.Log("LogService");
                    string oks = "买家" + b.Code + "的建档已经通过";
                    if (UID_BuyFilingStatus > 0 && c.IsSpecialTrade && !b.Code.StartsWith("del", StringComparison.CurrentCultureIgnoreCase))
                    {
                        SOSOshop.BLL.Order.Orders obll = new SOSOshop.BLL.Order.Orders();
                        obll.LetOrders2(b.Code, "一级单位");
                        //SOSOshop.BLL.Logs.Log.LogServiceAdd(oks, uid, b.TrueName, "往来单位消息处理1", "同步商城买家" + b.Code + "成功！", 0);
                    }
                    if (b.Member_Class != 0)
                    {
                        cb_IsPriorDistribution.Enabled = false;
                        c.IsPriorDistribution = false;
                        cb_IsPriorDistribution.SelectedValue = "0";
                    }
                }
                else
                {
                    Response.Write("<center><br><h3>未知买家！</h3>"); Response.End();
                }
            }
        }

        private void UpdateMemberPermission(RadioButtonList cb)
        {
            int uid = 0; int.TryParse(this.hfUID.Value, out uid);
            if (uid > 0 && cb.Enabled)
            {
                //授权
                bool isCheckUp = SOSOshop.BLL.PowerPass.isPass("008009014");
                if (!isCheckUp)
                {
                    Response.Write("<script type=\"text/javascript\">alert('操作失败！无权限！');location='Buyer_Permissions.aspx?UID=" + uid + "';</script>");
                    Response.End();
                }
                else
                {
                    SOSOshop.BLL.MemberPermission bll = new SOSOshop.BLL.MemberPermission();
                    string c = cb.ID.Replace("cb_", "");
                    string v = cb.SelectedValue;
                    bool ok = bll.Amend(uid, c, v);
                    if (!ok)
                    {
                        Response.Write("<script type=\"text/javascript\">alert('操作失败！');location='Buyer_Permissions.aspx?UID=" + uid + "';</script>");
                        Response.End();
                    }
                    else
                    {

                        #region 后台用户操作日志记录
                        SOSOshop.Model.AdminInfo adminInfo = SOSOshop.BLL.AdministrorManager.Get();
                        SOSOshop.BLL.Logs.Log.LogAdminAdd("修改买家权限[" + uid + "]" + c, (adminInfo == null ? 0 : adminInfo.AdminId), (adminInfo == null ? "" : adminInfo.AdminName), 1);
                        #endregion
                        #region 清除缓存
                        SOSOshop.BLL.DbBase db1 = new SOSOshop.BLL.DbBase(); db1.ClearCache();
                        #endregion

                        //快捷开通交易权限
                        if (v == "1" && c == "IsSpecialTrade")
                        {
                            string sql = "UPDATE dbo.memberaccount SET isIsSpecialTradeDate=getdate() where uid={0};Update memberpermission set IsMoneyAndShipping=1,IsCOD=0 Where UID=(select top(1) UID from memberinfo where UID={0} and Member_Class=0) ";
                            sql += "Update memberpermission set IsMoneyAndShipping=0,IsCOD=1 Where UID=(select top(1) UID from memberinfo where UID={0} and Member_Class=1) ";
                            int effected = bll.ExecuteNonQuery(string.Format(sql, uid));
                            if (effected <= 0)
                            {
                                bll.Amend(uid, c, "0");
                                Response.Write("<script type=\"text/javascript\">alert('操作失败！');location='Buyer_Permissions.aspx?UID=" + uid + "';</script>");
                                Response.End();
                            }
                            else
                            {
                                //发送短信通知
                                string MobilePhone = Convert.ToString(bll.ExecuteScalar("select MobilePhone from memberaccount where UID=" + uid));
                                string SmsMsg = "尊敬的" + bll.ExecuteScalar("select TrueName from MemberInfo where UID=" + uid)
                                    + "，快捷交易权限（7天内有效）已开通，立即登录，畅享医药电商的网上采购快感";
                                string from = "系统";
                                string to = MobilePhone;
                                SOSOshop.BLL.Sms.SendAndSaveDataBase(MobilePhone, SmsMsg, from, to);

                                Response.Write("<script type=\"text/javascript\">location='Buyer_Permissions.aspx?UID=" + uid + "';</script>");
                                Response.End();
                            }
                        }

                    }
                }
            }
        }

        protected void cb_IsLookPrice_02_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMemberPermission(sender as RadioButtonList);
        }

        protected void cb_IsLookPrice_01_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMemberPermission(sender as RadioButtonList);
        }

        protected void cb_IsLookProduct_01_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMemberPermission(sender as RadioButtonList);
        }

        protected void cb_IsLookProduct_02_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMemberPermission(sender as RadioButtonList);
        }

        protected void cb_IsLookStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMemberPermission(sender as RadioButtonList);
        }

        protected void cb_IsTrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMemberPermission(sender as RadioButtonList);
        }

        protected void cb_IsPeriodicalSettle_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMemberPermission(sender as RadioButtonList);
        }

        protected void cb_IsCOD_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMemberPermission(sender as RadioButtonList);
        }

        protected void cb_IsMoneyAndShipping_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMemberPermission(sender as RadioButtonList);
        }

        protected void cb_IsPriorDistribution_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMemberPermission(sender as RadioButtonList);
        }

        protected void cb_IsShippingFor48h_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMemberPermission(sender as RadioButtonList);
        }

        protected void cb_IsSpecialTrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMemberPermission(sender as RadioButtonList);
        }
    }
}