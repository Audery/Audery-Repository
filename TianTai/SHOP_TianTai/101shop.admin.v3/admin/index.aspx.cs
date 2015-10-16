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
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _101shop.admin.v3
{
    public partial class index : System.Web.UI.Page
    {
        protected string WebName;

        #region 统计时间(毫秒)
        public DateTime _StatisticalTime = DateTime.Now;//开始时间
        public int[] StatisticalTime = new int[3];//初始化,加载数据,呈现页面
        protected override void OnPreLoad(EventArgs e)
        {
            StatisticalTime[0] = (int)((DateTime.Now - _StatisticalTime).TotalMilliseconds);
            base.OnPreLoad(e);
        }
        protected override void OnPreRender(EventArgs e)
        {
            StatisticalTime[1] = (int)((DateTime.Now - _StatisticalTime).TotalMilliseconds) - StatisticalTime[0];
            base.OnPreRender(e);
        }
        protected override void OnPreRenderComplete(EventArgs e)
        {
            StatisticalTime[2] = (int)((DateTime.Now - _StatisticalTime).TotalMilliseconds) - StatisticalTime[1];
            base.OnPreRenderComplete(e);
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //#if DEBUG
                //                    //测试编译状态不用登陆
                //                    string pwd = ChangeHope.Common.DEncryptHelper.Encrypt("101administrator", 1);
                //                    bool loginResult = false;
                //                    YXShop.BLL.Admin.Administrators administrators = new YXShop.BLL.Admin.Administrators();
                //                    loginResult = AdminLogin("admin", pwd);
                //                    if (loginResult)
                //                    {
                //                         Response.Redirect("admin_index.aspx", true);
                //                    }
                //#endif

                if (ChangeHope.WebPage.PageRequest.GetFormString("Option") != string.Empty && ChangeHope.WebPage.PageRequest.GetFormString("id") != string.Empty)
                {
                    string types = Request.Form["Option"].Trim();
                    string StrID = ChangeHope.WebPage.PageRequest.GetFormString("id");
                    if (types == "sendAdminLoginCheckCode")
                    {
                        string userLoginName = ChangeHope.WebPage.PageRequest.GetFormString("toUID");
                        string userLoginPwd = ChangeHope.WebPage.PageRequest.GetFormString("toPWD");
                        userLoginPwd = ChangeHope.Common.DEncryptHelper.Encrypt(userLoginPwd, 1);
                        SOSOshop.BLL.Administrators bll = new SOSOshop.BLL.Administrators();
                        SOSOshop.Model.Administrators model = bll.GetModelByAdminName(userLoginName);
                        string message = "";
                        //无数据
                        if (model == null)
                        {
                            message = "用户名错误!";
                        }
                        //密码错误
                        else if (!model.PassWord.ToLower().Equals(userLoginPwd.ToLower()))
                        {
                            message = "密码错误!";
                        }
                        //帐号被冻结
                        else if (model.State.Equals(1))
                        {
                            message = "您输入的账户以被冻结!";
                        }
                        //帐号已经过期
                        else if (model.ManageEndTime < DateTime.Now)
                        {
                            message = "你的帐户已经过期!";
                        }
                        else
                        {
                            string DstMobile = Convert.ToString(new SOSOshop.BLL.Db().ExecuteScalar("select LoginAuthenticationOfficePhone from yxs_administrators where adminid = " + model.AdminId)).Trim();
                            if (DstMobile.Trim() == "")
                            {
                                message = "no";
                            }
                            else
                            {
                                //设置Cookies 2分钟
                                string checkCode = ChangeHope.Common.DEncryptHelper.GetRandWord(5);
                                HttpCookie cookie = new HttpCookie("CheckCode", checkCode);
                                cookie.Expires = DateTime.Now.AddMinutes(-1000);
                                Response.Cookies.Add(cookie);
                                cookie = new HttpCookie("CheckCode", checkCode);
                                cookie.Expires = DateTime.Now.AddMinutes(2);
                                Response.Cookies.Add(cookie);
                                //发送短信
                                ChangeHope.WebPage.Sms sms = new ChangeHope.WebPage.Sms();
                                string SmsMsg = model.Name + ",101后台管理员," + "需要登陆后台,请告知验证码:" + checkCode + "\r\n"; if (SmsMsg.Length > 120) SmsMsg = SmsMsg.Substring(0, 120);
                                string from = "登陆后台的验证码";
                                string to = model.Name;
                                bool Success = false;
                                Success = (sms.Send(DstMobile, SmsMsg, from, to));
                                if (ChangeHope.WebPage.PageRequest.GetFormInt("add") != -1)
                                    sms.SaveDataBase(DstMobile, SmsMsg, from, to, Success);
                                else
                                    sms.UpdateDataBase(int.Parse(StrID), DstMobile, SmsMsg, from, to, Success);
                                if (Success)
                                {
                                    message = "ok";
                                    string adminname = Convert.ToString(new SOSOshop.BLL.Db().ExecuteScalar("select name from yxs_administrators where OfficePhone = '" + DstMobile + "'"));
                                    message += "," + from + "已经发送给" + (adminname.Trim() != "" ? adminname : DstMobile);
                                    ArrayList smsRecord = new ArrayList();
                                    if (Session["smsRecord"] != null) smsRecord = Session["smsRecord"] as ArrayList;
                                    if (smsRecord != null) smsRecord.Add(checkCode);
                                    Session["smsRecord"] = smsRecord;
                                }
                                else
                                {
                                    message = "短信发送失败！";
                                }
                            }
                        }
                        Response.Write(message);
                    }
                    Response.End();
                    return;
                }

                SOSOshop.BLL.SysParameter sp = new SOSOshop.BLL.SysParameter();
                WebName = sp.WebSiteName;
                if (SOSOshop.BLL.AdministrorManager.Get() != null)
                {
                    ChangeHope.WebPage.Script.Alert("提示：您已经登陆成功，转向后台管理页面");
                    ChangeHope.WebPage.Script.ParentPageRedirect(sp.DummyPaht + "admin/admin_index.aspx");
                    return;
                }
            }
        }

        public string message = "";
        protected void Login_Click(object sender, ImageClickEventArgs e)
        {

            SOSOshop.BLL.AdminLoginLog log = new SOSOshop.BLL.AdminLoginLog();
            SOSOshop.Model.AdminLoginLog logModel = new SOSOshop.Model.AdminLoginLog();

            //检查填写的表单
            if (!CheckForm())
            {
                return;
            }
            //系统登陆
            string userLoginName = this.txtUserLoginName.Text;
            string userLoginPwd = this.txtUserLoginPwd.Text;
            userLoginPwd = ChangeHope.Common.DEncryptHelper.Encrypt(userLoginPwd, 1);
            bool loginResult = false;
            SOSOshop.BLL.Administrators administrators = new SOSOshop.BLL.Administrators();
            loginResult = AdminLogin(userLoginName, userLoginPwd);
            if (!loginResult)
            {
                ChangeHope.WebPage.Script.Alert("温馨提示:" + message);
            }
            logModel.OperateNote = message;
            administrators = null;

            //写入日志
            DateTime dt = DateTime.Now;
            logModel.HostComputerName = Request.UserHostName;
            logModel.LoginInTime = dt;
            logModel.LoginIp = Request.UserHostAddress;
            logModel.LoginOutTime = dt;
            logModel.PassWord = userLoginPwd;
            logModel.AdminName = userLoginName;
            log.Add(logModel);
            logModel = null;
            log = null;

            if (loginResult)
            {
                //if (SOSOshop.BLL.PowerPass.isPass("002053000") && userLoginName != "admin")
                //{
                //    Response.Redirect("Goods/Default.aspx", true); return;
                //}
                System.Web.Security.FormsAuthentication.SetAuthCookie(userLoginName, false);
                if (userLoginPwd.Length < 8)
                {
                    Response.Redirect("/admin/member/admin_edit.aspx?adminid=" + SOSOshop.BLL.AdministrorManager.Get().AdminId + "&edit=pwd", true);
                }
                else
                {
                    Response.Redirect("admin_index.aspx", true);
                }

            }
        }
        /// <summary>
        /// 系统管理员登陆系统
        /// </summary>
        /// <param name="adminName"></param>
        /// <param name="adminPwd"></param>
        /// <returns></returns>
        public bool AdminLogin(string adminName, string adminPwd)
        {

            SOSOshop.BLL.Administrators bll = new SOSOshop.BLL.Administrators();
            SOSOshop.Model.Administrators model = bll.GetModelByAdminName(adminName);
            //无数据
            if (model == null)
            {
                message = "用户名或密码错误!";
                return false;
            }
            //密码错误
            if (!model.PassWord.ToLower().Equals(adminPwd.ToLower()))
            {
                message = "用户名或密码错误!";
                model = null;
                return false;
            }

            //帐号被冻结
            if (model.State.Equals(1))
            {
                message = "您输入的账户以被冻结!";
                model = null;
                return false;
            }

            //帐号已经过期
            if (model.ManageEndTime < DateTime.Now)
            {
                message = "你的帐户已经过期!";
                model = null;
                return false;
            }

            //一人一机登陆验证
            //ChangeHope.Common.DEncryptHelper.Encrypt(model.Name, 1);
            //if (model.Name != "admin")
            //{
            //    object AllowOtherLogin = new SOSOshop.BLL.Db().ExecuteScalar("select top (1) AllowOtherLogin from yxs_CustomerSetting");
            //    if (AllowOtherLogin != null && AllowOtherLogin.ToString() == "0")
            //    {
            //        //查询登陆日志
            //        object adminid = new SOSOshop.BLL.Db().ExecuteScalar("select top (1) id from yxs_adminloginlog where " +
            //            "adminname = '" + model.Name + "' " +
            //            "and convert(char(10),loginintime,120) = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' and loginintime = loginouttime " +
            //            "and loginip != '" + Request.UserHostAddress + "' " +
            //            "and operatenote = '登陆成功!' order by loginintime desc");
            //        if (adminid != null)
            //        {
            //            message = "你的帐户已经登陆!";
            //            model = null;
            //            return false;
            //        }
            //    }
            //}

            string DstMobile = Convert.ToString(new SOSOshop.BLL.Db().ExecuteScalar("select LoginAuthenticationOfficePhone from yxs_administrators where adminid = " + model.AdminId)).Trim();
            if (DstMobile.Trim() != "" && false)
            {
                //发送短信
                ArrayList smsRecord = new ArrayList();
                if (Session["smsRecord"] != null) smsRecord = Session["smsRecord"] as ArrayList;
                bool ok = smsRecord != null && smsRecord.Count > 0 && txtCheckCode.Text.ToUpper() == smsRecord[smsRecord.Count - 1].ToString().ToUpper();
                if (!ok)
                {
                    message = "验证码错误!";
                    model = null;
                    return false;
                }
            }


            //初始化权限
            SOSOshop.Model.AdminInfo admin = new SOSOshop.Model.AdminInfo();
            if (model.Power.Equals(0))
            {
                admin.AdminPowerType = "all";
            }
            else
            {
                //非管理员权限，等待添加相关内容
                admin.AdminPowerType = "";
            }

            admin.AdminId = model.AdminId;
            admin.AdminName = model.Name;
            admin.AdminRole = model.Role;
            SOSOshop.BLL.AdministrorManager.Set(admin);

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
            1,
            admin.AdminName,
            DateTime.Now,
            DateTime.Now.AddMinutes(1000),
            true,
            ""
            );
            HttpCookie cookie = new System.Web.HttpCookie(FormsAuthentication.FormsCookieName,
            FormsAuthentication.Encrypt(ticket));
            cookie.Domain = System.Configuration.ConfigurationManager.AppSettings["Domain"];
            cookie.Expires = DateTime.Now.AddHours(10);
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);


            admin = null;
            message = "登陆成功!";
            return true;
        }

        /// <summary>
        /// 检查表单
        /// </summary>
        private bool CheckForm()
        {   
            string message = "";
            //检查浏览器是否打开Cookies
            if (Request.Cookies["CheckCode"] == null)
            {
                message = "温馨提示：验证码错误";
                ChangeHope.WebPage.Script.Alert(message);
                return false;
            }

            //检查验证码
            if (!Request.Cookies["CheckCode"].Value.Equals(txtCheckCode.Text.ToUpper()))
            {
                message = "温馨提示：验证码错误";
                ChangeHope.WebPage.Script.Alert(message);
                return false;
            }

            //系统登陆
            string userLoginName = this.txtUserLoginName.Text;
            string userLoginPwd = this.txtUserLoginPwd.Text;

            if (userLoginName.Equals("") || userLoginPwd.Equals(""))
            {
                message = "温馨提示：用户名或者密码为空";
                return false;
            }
            return true;
        }
    }
}
