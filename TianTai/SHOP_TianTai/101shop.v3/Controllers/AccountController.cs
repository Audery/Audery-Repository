using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _101shop.v3.Models;
using System.Web.Security;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using SOSOshop.Model;
using System.Configuration;
using SOSOshop.BLL.Common;

namespace _101shop.v3.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }
        public AccountController()
        {

        }
        public AccountController(ControllerContext cc)
        {
            this.ControllerContext = cc;
        }

        // 会员登陆页面
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            if (Request.UrlReferrer == null || string.IsNullOrEmpty(Request.UrlReferrer.ToString()))
            {
                ViewBag.Ref = "/";
            }
            else if (Request.IsAuthenticated && Public.GetUserId() > 0)
            {
                return RedirectToAction("index", "home");
            }
            else
            {
                ViewBag.Ref = Request.UrlReferrer.ToString();
            }
            return View();
        }

        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        private bool IsUserName(string UserName)
        {
            bool Exists = false;
            if (!string.IsNullOrEmpty(UserName))
            {
                SOSOshop.BLL.MemberAccount bll = new SOSOshop.BLL.MemberAccount();
                Exists = bll.Exists(UserName);
            }
            return Exists;
        }

        // 会员登陆页面异步验证
        // GET: /Account/LogOnCheck
        [HttpPost]
        public void LogOnCheck()
        {
            //参数提交?act=ExistsUserName
            if (Request["act"] != null && Request["act"] == "ExistsUserName" && Request["UserName"] != null)
            {
                string UserName = Request["UserName"];
                Response.Write(IsUserName(UserName) ? 1 : 0);
            }

            //参数提交?act=ExistsEMail
            if (Request["act"] != null && Request["act"] == "ExistsEMail" && Request["Email"] != null)
            {
                bool Exists = false;
                string Email = Request["Email"];
                if (!string.IsNullOrEmpty(Email))
                {
                    SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();
                    DataTable dt = db.ExecuteTable(string.Format("select count(uid) from [memberaccount] where Email='{0}'", Email.Trim()));
                    Exists = (int)dt.Rows[0][0] > 0 ? true : false;
                }
                Response.Write(Exists ? 1 : 0);
            }

            //参数提交?act=ExistsCaptcha
            if (Request["act"] != null && Request["act"] == "ExistsCaptcha" && Request["Captcha"] != null)
            {
                bool Exists = (Request.UserHostAddress.StartsWith("::") || Request.UserHostAddress.StartsWith("192.168") || Request.UserHostAddress.StartsWith("125.69.66"));//如果是局域网，或公司内部就不用验证验证验证码方便自动化测试;
                string Captcha = Request["Captcha"];
                if (!Exists && !string.IsNullOrEmpty(Captcha))
                {
                    Exists = (Session["Captcha"] != null
                        && Session["Captcha"].ToString().ToUpper() == Captcha.ToUpper());
                }
                Response.Write(Exists ? 1 : 0);
            }
        }

        // 会员登陆页面提交表单
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            //Response.Write(returnUrl);

            returnUrl = Request["referrer"];
            if (ModelState.IsValid)
            {
                if (model.UserName != null)
                {
                    model.UserName = model.UserName.Trim();
                }
                SOSOshop.BLL.MemberAccount bll = new SOSOshop.BLL.MemberAccount();
                SOSOshop.Model.MemberAccount obj = null;
                if ((string.IsNullOrEmpty(model.Captcha) || !model.Captcha.Equals(Convert.ToString(Session["Captcha"]), StringComparison.CurrentCultureIgnoreCase)))
                {
                    ModelState.AddModelError("", "您输入的验证码不正确。");
                }
                else
                {
                    string loginname = model.UserName;
                    string loginpwd = model.PassWord;
                    if (!string.IsNullOrEmpty(model.UserName) && !string.IsNullOrEmpty(model.PassWord))
                    {
                        model.PassWord = ChangeHope.Common.DEncryptHelper.Encrypt(model.PassWord, 1);
                        obj = bll.GetModelByNameAndPassword(model.UserName, model.PassWord);
                    }
                    if (obj != null)
                    {
                        if (obj.State == 2)
                        {
                            ModelState.AddModelError("", "用户已经被冻结，请联系客服。");
                        }
                        else if (obj.PeriodOfValidity <= DateTime.Now)
                        {
                            ModelState.AddModelError("", "用户过期时间已到，请联系客服。");
                        }
                        //else if (bll.GetMember_Class(obj.UID) == SOSOshop.Model.Member.Member_Class.无)
                        //{
                        //    ModelState.AddModelError("", "用户属未知买家类别，请联系客服。");
                        //}
                        else
                        {
                            //写入登陆日志
                            new SOSOshop.BLL.MemberLoginLog().Add(obj.UID, model.UserName, model.PassWord);
                            Session["Captcha"] = null;

                            DateTime expiration = DateTime.Now.AddHours(12);
                            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                            model.UserName,
                            DateTime.Now,
                            expiration,
                            false,
                            obj.UID.ToString(),
                            FormsAuthentication.FormsCookiePath);
                            string encTicket = FormsAuthentication.Encrypt(ticket);
                            HttpCookie tk = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                            Response.Cookies.Add(tk);
                            //普通会员引导去升级                            
                            SOSOshop.BLL.MemberPermission mpb = new SOSOshop.BLL.MemberPermission();
                            SOSOshop.BLL.MemberInfo mifo = new SOSOshop.BLL.MemberInfo();//判断用户类型
                            SOSOshop.Model.MemberInfo obj2 = mifo.GetModel(obj.UID);
                            int memberClass = obj2.Member_Class;
                            //判断用户是否GSP建档
                            //未建档用户跳转到会员建档资料提交页面
                            if (!mpb.GetBuyFilingStatus(obj.UID))
                            {
                                if (1 == (int)mpb.ExecuteScalar("SELECT IsSpecialTrade FROM dbo.memberpermission WHERE UID=" + obj.UID))
                                {
                                    #region 短信提醒
                                    string phone = obj.MobilePhone;
                                    string CompanyShortName = ConfigurationManager.AppSettings["CompanyShortName"];
                                    SOSOshop.BLL.Sms.SendAndSaveDataBase(phone, "尊敬的" + obj2.TrueName + string.Format("，快捷交易权限仅7天内有效，请尽快邮寄合法的首营资料到{0}审核，享永久会员权益", CompanyShortName), "系统", phone);
                                    int tId = obj2.Editer;
                                    if (tId > 0)
                                    {
                                        string jyphone = (string)new SOSOshop.BLL.Administrators().ExecuteScalar("select MobilePhone from yxs_administrators where adminid=" + tId);

                                        if (!string.IsNullOrEmpty(jyphone))
                                        {
                                            SOSOshop.BLL.Sms.SendAndSaveDataBase(phone, "手机号为" + model.UserName + "的快捷开通客户已登录，请及时联系跟进，督促首营资质到位。", "系统", phone);
                                        }

                                    }
                                    #endregion
                                    return RedirectToAction("Upgrade", "MemberCenter");
                                }
                                else
                                {
                                    return RedirectToAction("registerok", "account");
                                }
                            }
                            else
                            {
                                if (returnUrl.Length > 10)
                                {
                                    int pos = returnUrl.LastIndexOf("/");
                                    int outs = 0;

                                    if (pos != -1)
                                    {
                                        pos++;
                                    }

                                    int.TryParse(returnUrl.Substring(pos).Replace(".html", ""), out outs);

                                    if ((outs != 0) || (returnUrl.IndexOf("products") != -1))
                                    {
                                        return Redirect(returnUrl);
                                    }
                                }
                                return RedirectToAction("index", "home");
                                //if (memberClass == 0)//批发客户跳转到基药频道
                                //{
                                //    return RedirectToAction("Index", "jy");
                                //}
                                //else if (memberClass == 1)//OTC客户跳转到OTC频道
                                //{

                                //    //return RedirectToAction("Index", "Otc");
                                //}
                                //else//其它用户
                                //{
                                //    if (returnUrl.ToLower().IndexOf("logon") != -1 || returnUrl.ToLower().IndexOf("register") != -1)
                                //    {
                                //        return RedirectToAction("LoginOK", "Account");
                                //    }
                                //    else if (!string.IsNullOrEmpty(returnUrl))
                                //    {
                                //        return Redirect(returnUrl);
                                //    }
                                //    else
                                //    {
                                //        return RedirectToAction("LoginOK", "Account");
                                //    }
                                //}
                            }
                        }
                    }
                    else
                    {

                        ModelState.AddModelError("", "您输入的用户名或密码不正确。");
                    }
                }
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }

        // 会员注销并返回首页
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        // 会员注册页面
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        // 会员注册页面提交表单
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // 注册用户
                bool ok = false;
                int UID = 0;

                //添加买家账号信息
                SOSOshop.BLL.MemberAccount aBll = new SOSOshop.BLL.MemberAccount();
                SOSOshop.Model.MemberAccount a = new SOSOshop.Model.MemberAccount();
                if (string.IsNullOrEmpty(model.Captcha) || !model.Captcha.Equals(Convert.ToString(Session["Captcha"]), StringComparison.CurrentCultureIgnoreCase))
                {
                    ModelState.AddModelError("", "提供的验证码不正确。");
                }
                else
                {
                    if (!string.IsNullOrEmpty(model.UserName)
                        && !string.IsNullOrEmpty(model.PassWord)
                        && model.PassWord.Equals(model.ConfirmPassword)
                        && !string.IsNullOrEmpty(model.LinkMan)
                        && !string.IsNullOrEmpty(model.Email))
                    {
                        int i = new SOSOshop.BLL.MemberAccount().GetUserIdNameClass(model.UserName);
                        if (i == 2)
                        {
                            a.UserId = "";
                            a.MobilePhone = model.UserName;
                            a.Email = model.Email;
                        }
                        //else if (i == 3)
                        //{
                        //    a.UserId = "";
                        //    a.MobilePhone = "";
                        //    a.Email = model.UserName;
                        //}
                        //else
                        //{
                        //    a.UserId = model.UserName;
                        //    a.MobilePhone = "";
                        //    a.Email = model.Email;
                        //}
                        //Response.Write(model.PassWord+" "+i);
                        a.PassWord = ChangeHope.Common.DEncryptHelper.Encrypt(model.PassWord, 1);

                        a.Email_QQ = model.Email.EndsWith("@qq.com") ? model.Email : "";
                        a.Question = "";
                        a.Answer = "";
                        a.RegisterDate = DateTime.Now;
                        a.RegisterIP = ChangeHope.WebPage.PageRequest.GetIP();
                        a.PeriodOfValidity = a.RegisterDate.AddYears(20);
                        a.State = 1;
                        a.CompanyClass = model.CompanyClass;
                        UID = aBll.Add(a);
                    }
                    //添加买家联系信息
                    if (UID > 0)
                    {
                        SOSOshop.BLL.MemberInfo bBll = new SOSOshop.BLL.MemberInfo();
                        SOSOshop.Model.MemberInfo b = new SOSOshop.Model.MemberInfo();
                        b.UID = UID;
                        b.TrueName = model.LinkMan;
                        b.Member_Class = -1;
                        b.Member_Type = 0;
                        int area = 0; int.TryParse(Request["province"], out area);
                        b.Province = area;
                        area = 0; int.TryParse(Request["city"], out area);
                        b.City = area;
                        area = 0; int.TryParse(Request["county"], out area);
                        b.Borough = area;

                        if (model.CompanyClass == "生产企业" || model.CompanyClass == "商业公司" || model.CompanyClass == "民营医院")
                        {
                            b.Member_Class = 0;
                        }
                        else
                        {
                            b.Member_Class = 1;
                        }
                        //通知交易人员     
                        //int tId = 0;//交易员ID  取消//改为数据库设置默认值 2014/2/12
                        //string tname = new SOSOshop.BLL.Administrators().GetTraderIdByRegion(b.Province, b.City, b.Borough, out tId, model.CompanyClass);//交易员姓名
                        int oId = 0;//外销人员（线下推广人员)ID
                        string oName = new SOSOshop.BLL.Administrators().GetOutSellPersonIdByRegion(b.Province, b.City, b.Borough, out oId, model.CompanyClass);//外销人员（线下推广人员）姓名

                        //if (tId != 0)
                        //{
                        //    string jyphone = string.Format(" SELECT zyphone FROM zhiyzl WHERE is_czy='是' and beactive='是' and zhiyname='{0}'", tname);
                        //    SOSOshop.BLL.DbBase db = new SOSOshop.BLL.Db();
                        //    db.ChangeDB("ConnectionStringERP");
                        //    DataTable dt = db.ExecuteTable(jyphone);
                        //    if (dt.Rows.Count > 0)
                        //    {
                        //        string phone = dt.Rows[0][0].ToString().Trim();
                        //        if (!string.IsNullOrEmpty(phone))
                        //        {
                        //            ok = SOSOshop.BLL.Sms.SendAndSaveDataBase(phone, "手机号为" + model.UserName + "的用户，已经在101商城前台进行注册，请尽快联系完成后续注册审核。", "系统", phone);
                        //        }
                        //    }
                        //}
                        //b.Editer = tId;
                        b.OSPId = oId;
                        b.HandPhone = a.MobilePhone;
                        ok = bBll.Add(b);
                        //添加权限
                        if (ok)
                        {
                            SOSOshop.BLL.MemberPermission cBll = new SOSOshop.BLL.MemberPermission();
                            SOSOshop.Model.MemberPermission c = new SOSOshop.Model.MemberPermission();
                            c.UID = UID;
                            c.IsMoneyAndShipping = true;//款到发货权限
                            ok = cBll.Add(c);
                        }
                        if (!ok) aBll.Delete(UID);
                    }
                    if (ok)
                    {
                        //发送注册成功的短信 取消//彭宴负责交易员分配工作，后台操作后发送短信 2014/2/12
                        var sms = new SOSOshop.MSG.Sms();
                        object phone = aBll.ExecuteScalar("select OfficePhone from yxs_administrators where adminid=(select Editer from memberaccount a inner join memberinfo b on a.UID=b.UID where a.UID='" + UID + "')");
                        //string SmsMsg = "尊敬的" + model.LinkMan + "，您已完成注册第一步，立即致电您的专属采购顾问" + phone + "开通查看价格权限";
                        string CompanyShortName = ConfigurationManager.AppSettings["CompanyShortName"];
                        string SmsMsg = string.Format("尊敬的用户，您在{0}医药网注册已成功，您的账户名为", CompanyShortName) + model.UserName + "，请留意保存，祝您采购愉快！";
                        string from = "系统";
                        string to = model.UserName;
                        ok = SOSOshop.BLL.Sms.SendAndSaveDataBase(model.UserName, SmsMsg, from, to);
                        //写入登陆日志
                        new SOSOshop.BLL.MemberLoginLog().Add(UID, model.UserName, a.PassWord);
                        Session["Captcha"] = null;
                        DateTime expiration = DateTime.Now.AddHours(12);
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                        model.UserName,
                        DateTime.Now,
                        expiration,
                        false,
                        UID.ToString(),
                        FormsAuthentication.FormsCookiePath);
                        string encTicket = FormsAuthentication.Encrypt(ticket);
                        HttpCookie tk = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                        Response.Cookies.Add(tk);
                        return RedirectToAction("RegisterOK");
                    }
                    else
                    {
                        ModelState.AddModelError("", "注册失败!");
                    }
                }
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }

        // 会员注册成功
        // GET: /Account/Register

        public ActionResult RegisterOK()
        {
            ViewBag.reg = Request.QueryString["u"];
            return View();
        }

        // 会员登录成功
        // GET: /Account/Register
        public ActionResult LoginOK()
        {
            return View();
        }

        //找回密码
        public ActionResult FindPassword()
        {
            int step = 0;

            if (!string.IsNullOrEmpty(Request.Form["step"]))
            {
                step = int.Parse(Request.Form["step"]);
            }
            if (!string.IsNullOrEmpty(Request.Form["phone"]))
            {
                ViewBag.phone = Request.Form["phone"];
            }
            if (step == 2)
            {
                string phone = Request["sphone"];
                try
                {


                    if (Request["smscode"] == Session["Captcha"].ToString() && !string.IsNullOrEmpty(Request["smscode"]))
                    {
                        Session["Captcha"] = null;
                    }
                    else
                    {
                        step = 1;
                        ViewBag.error = "验证码错误！";
                    }
                }
                catch
                {
                    step = 1;
                    ViewBag.error = "验证码错误！";
                }
                ViewBag.phone = phone;
            }
            if (step == 3)
            {
                string phone = Request["cphone"];
                Session["Captcha"] = null;
                if (IsUserName(phone))
                {
                    string pass = Request["pass"];
                    if (pass.Length > 5)
                    {
                        pass = ChangeHope.Common.DEncryptHelper.Encrypt(pass, 1);
                        string sql = string.Format("update memberaccount set Password='{0}' where MobilePhone='{1}'", pass, phone);
                        bool ok = new SOSOshop.BLL.Db().ExecuteNonQuery(sql) > 0;
                        ViewBag.result = ok ? "保存成功。" : "保存失败！";
                    }
                    else
                    {
                        step = 2;
                        ViewBag.error = "您的密码不符合规则！";
                    }
                }
                else
                {
                    step = 0;
                    ViewBag.error = "请输入正确的手机号码！";
                }
            }
            ViewBag.Step = step;

            return View();
        }

        /// <summary>
        /// 找回秘密时，给用户发送验证码
        /// </summary>
        /// <returns></returns>
        public string GetSms()
        {
            string ret = BaseController.Json(-1, "抱歉，当前用户设置的手机号码可能有误或网络繁忙，你可以重新发送短信！");
            string MobilePhone = Request.Form["phone"];
            //判断用户的手机号码是否已经注册过
            if (IsUserName(MobilePhone))
            {
                bool ok = false;
                string smsCaptcha = Common.CheckCode.GenerateNumber();
                Session["Captcha"] = smsCaptcha;
                string url = "http://" + Request.Url.Host + "/include/ajax.ashx?act=updateMobilePhone&v=" + Guid.NewGuid().ToString();
                var sms = new SOSOshop.MSG.Sms();
                SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();
                object phone = db.ExecuteScalar("select OfficePhone from yxs_administrators where adminid=(select Editer from memberaccount a inner join memberinfo b on a.UID=b.UID where MobilePhone='" + MobilePhone + "')");
                if (phone == null || phone.ToString().Trim() == "") phone = "028-66321993";
                string SmsMsg = "您的账户安全验证码为：" + smsCaptcha + "，请在页面填写。如非本人操作，请致电您的专属采购顾问" + phone;
                string from = "系统";
                string to = MobilePhone;
                ok = SOSOshop.BLL.Sms.SendAndSaveDataBase(MobilePhone, SmsMsg, from, to);
                if (ok)
                {
                    ret = BaseController.Json(0, "ok");
                }
            }
            return ret;
        }


        /// <summary>
        /// 选择企业名称
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public JsonResult SelectCompanyName(string q)
        {
            string search = Library.Lang.Input.Filter(q);

            DrugsBaseServices.DrugsBaseSoapClient bll = new DrugsBaseServices.DrugsBaseSoapClient();

            var rtnJson = bll.SelectCompanyName(search).Select(o => new EnterpriseLocation
            {
                Id = o.Id,
                Name = o.Name,
                Lat = o.Lat,
                Lon = o.Lon
            }).ToList();

            if (rtnJson != null)
            {
                //查询条件过滤
                rtnJson = rtnJson.Where(o => o.Name.Contains(search)).ToList();
            }

            if (rtnJson != null && rtnJson.Count > 0)
            {
                return Json(rtnJson, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(string.Empty, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 地图定位
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public ActionResult EnterpriseLocation()
        {
            SOSOshop.Model.EnterpriseLocation location = new SOSOshop.Model.EnterpriseLocation();

            location.Name = Request["name"].ToString();
            location.Lon = 0.0M;
            location.Lat = 0.0M;

            if (Request["lon"] != null)
            {
                decimal lon = 0.0M;
                decimal.TryParse(Request["lon"].ToString(), out lon);
                location.Lon = lon;
            }
            if (Request["lat"] != null)
            {
                decimal lat = 0.0M;
                decimal.TryParse(Request["lat"].ToString(), out lat);
                location.Lat = lat;
            }

            return View(location);
        }
    }
}
