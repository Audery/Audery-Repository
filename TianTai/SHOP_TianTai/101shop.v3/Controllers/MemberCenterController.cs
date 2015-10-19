using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Text;
using System.Data;
using System.Data.Common;
using SOSOshop.BLL;
using Webdiyer.WebControls.Mvc;
using YXShop.BLL.Order;
using System.Configuration;
using SOSOshop.BLL.Common;

namespace _101shop.v3.Controllers
{
    public class MemberCenterController : Controller
    {
        Db db = new Db();
        // 会员中心主页
        // GET: /MemberCenter/
        [Authorize]
        public ActionResult Index()
        {
            return RedirectToAction("Orders", new { orderdate = "3" });//暂时不显示会员中心首页，因为很多信息无法提供，转到订单列表页

        }

        // 账户信息
        // GET: /MemberCenter/Info

        [Authorize]
        public ActionResult Info()
        {
            int UID = BaseController.GetUserId();//账户ID
            ViewBag.UID = UID;
            SOSOshop.BLL.MemberAccount bll = new SOSOshop.BLL.MemberAccount();
            BaseController.SetAccount(ViewBag);
            return View();
        }

        // 账户信息
        // GET: /MemberCenter/Info

        [Authorize]
        [HttpPost]
        public ActionResult InfoUpdate()
        {
            int UID = BaseController.GetUserId();//账户ID
            ViewBag.UID = UID;
            SOSOshop.BLL.MemberAccount bll = new SOSOshop.BLL.MemberAccount();

            string Email = Request["Email"];
            //if (string.IsNullOrEmpty(Email))
            //{
            //ModelState.AddModelError("", "请填写邮箱地址！");
            //}
            //else if (!Common.ValidateHelper.IsEmail(Email))
            //{
            //    ModelState.AddModelError("", "请填写正确的邮箱地址！");
            //}
            if (Email != "")
            {
                if (null != bll.ExecuteScalar("select top(1) UID from memberaccount where UID<>" + UID + " AND Email='" + Email + "'"))
                {
                    ModelState.AddModelError("", "请填写其它邮箱地址！此邮箱地址已经在系统中使用！");
                }
            }

            string MobilePhone = Request["MobilePhone"];
            if (string.IsNullOrEmpty(MobilePhone))
            {
                ModelState.AddModelError("", "请填写手机号！");
            }
            else if (!Common.ValidateHelper.IsMobilePhone(MobilePhone))
            {
                ModelState.AddModelError("", "请填写正确的手机号！");
            }
            else if (null != bll.ExecuteScalar("select top(1) UID from memberaccount where UID<>" + UID + " AND MobilePhone='" + MobilePhone + "'"))
            {
                ModelState.AddModelError("", "请填写其它手机号！此手机号已经在系统中使用！");
            }

            //if (string.IsNullOrEmpty(Request["TrueName"]))
            //{
            //    ModelState.AddModelError("", "请填写联系人！");
            //}

            if (string.IsNullOrEmpty(Request["province"]))
            {
                ModelState.AddModelError("", "请选择省！");
            }

            if (string.IsNullOrEmpty(Request["city"]))
            {
                ModelState.AddModelError("", "请选择市！");
            }

            if (string.IsNullOrEmpty(Request["county"]))
            {
                ModelState.AddModelError("", "请选择区！");
            }

            if (ModelState.Count == 0)
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update memberaccount set MobilePhone=@MobilePhone, Email=@Email ");//账号
                sql.Append("where UID=@UID ");
                sql.Append("update memberinfo set ");
                sql.Append("Province=@Province,City=@City,Borough=@Borough,Address=@Address, ");//所在地
                sql.Append("OfficePhone=@OfficePhone, Fax=@Fax ");//电话
                sql.Append("where UID=@UID ");
                DbCommand dbCommand = bll._db.GetSqlStringCommand(sql.ToString());
                bll._db.AddInParameter(dbCommand, "MobilePhone", DbType.AnsiString, Request["MobilePhone"]);
                bll._db.AddInParameter(dbCommand, "Email", DbType.AnsiString, Request["Email"]);
                //bll._db.AddInParameter(dbCommand, "TrueName", DbType.AnsiString, Request["TrueName"]);
                int area = 0; int.TryParse(Request["province"], out area);
                bll._db.AddInParameter(dbCommand, "Province", DbType.Int32, area);
                area = 0; int.TryParse(Request["city"], out area);
                bll._db.AddInParameter(dbCommand, "City", DbType.Int32, area);
                area = 0; int.TryParse(Request["county"], out area);
                bll._db.AddInParameter(dbCommand, "Borough", DbType.Int32, area);
                bll._db.AddInParameter(dbCommand, "Address", DbType.AnsiString, Request["Address"]);
                bll._db.AddInParameter(dbCommand, "OfficePhone", DbType.AnsiString, Request["OfficePhone"]);
                bll._db.AddInParameter(dbCommand, "Fax", DbType.AnsiString, Request["Fax"]);
                bll._db.AddInParameter(dbCommand, "UID", DbType.Int32, UID);
                bool ok = 0 < Convert.ToInt32(bll._db.ExecuteNonQuery(dbCommand));
                ModelState.AddModelError("", ok ? "保存成功。" : "保存失败！");
            }
            BaseController.SetAccount(ViewBag);
            return View("Info");
        }

        // 我的往来帐
        // GET: /MemberCenter/Bill

        [Authorize]
        public ActionResult Bill()
        {
            int UID = BaseController.GetUserId();//账户ID
            ViewBag.UID = UID;
            BaseController.SetAccount(ViewBag);

            return View();
        }

        // 账户安全
        // GET: /MemberCenter/SafetyCenter

        [Authorize]
        public ActionResult SafetyCenter()
        {
            int UID = BaseController.GetUserId();//账户ID
            ViewBag.UID = UID;
            SOSOshop.BLL.MemberAccount bll = new SOSOshop.BLL.MemberAccount();
            StringBuilder sql = new StringBuilder();
            sql.Append("select MobilePhone, Email, ");//账号
            sql.Append("ISNULL((SELECT TOP(1) 1 AS M FROM membercheck WHERE CheckType='M' AND Checked=1 AND UID=a.UID),0) AS membercheckM, ");//手机验证
            sql.Append("ISNULL((SELECT TOP(1) 1 AS M FROM membercheck WHERE CheckType='E' AND Checked=1 AND UID=a.UID),0) AS membercheckE ");//邮箱验证
            ////sql.Append("c.* ");//权限
            sql.AppendFormat("from memberaccount a inner join memberinfo b on a.UID=b.UID inner join memberpermission c on a.UID=c.UID where a.UID={0}", UID);
            using (IDataReader rd = (IDataReader)bll.ExecuteReader(sql.ToString()))
            {
                if (rd != null && rd.Read())
                {
                    ViewBag.MobilePhone = rd["MobilePhone"].ToString();
                    ViewBag.Email = rd["Email"].ToString();
                    //验证
                    bool membercheckM = int.Parse(rd["membercheckM"].ToString()) == 1;
                    ViewBag.membercheckM = membercheckM;
                    bool membercheckE = int.Parse(rd["membercheckE"].ToString()) == 1;
                    ViewBag.membercheckE = membercheckE;
                    if (membercheckM && membercheckE)
                    {
                        ViewBag.membercheck = "高级";
                    }
                    else if (membercheckM || membercheckE)
                    {
                        ViewBag.membercheck = "中级";
                    }
                    else
                    {
                        ViewBag.membercheck = "初级";
                    }
                    rd.Close();
                }
            }
            return View();
        }

        // 普通会员升级到企业会员
        // GET: /MemberCenter/Upgrade

        [Authorize]
        public ActionResult Upgrade()
        {
            int UID = BaseController.GetUserId();//账户ID
            ViewBag.UID = UID;
            //资质, 调用ERP:wldwwdzl往来单位文档资料
            //ViewBag.Qualifications = BaseController.GetQualifications(UID);
            SOSOshop.BLL.MemberPermission mp = new MemberPermission();

            if (mp.GetModel(UID).IsSpecialTrade)
            {
                if ((int)db.ExecuteTable(string.Format("select DateDiff(d,[isIsSpecialTradeDate],getdate()) from [memberaccount] where uid={0}", UID)).Rows[0][0] > 7)
                {
                    ViewBag.Msg = "暂时关闭客户权限，请提供相关证照恢复会员资格！";
                }
            }
            return View();
        }

        // 修改密码
        // GET: /MemberCenter/Password

        [Authorize]
        public ActionResult Password()
        {
            int UID = BaseController.GetUserId();//账户ID
            ViewBag.UID = UID;
            return View();
        }

        // 修改密码
        // GET: /MemberCenter/PasswordUpdate

        [Authorize]
        [HttpPost]
        public ActionResult PasswordUpdate()
        {
            bool ok = false;
            int UID = BaseController.GetUserId();//账户ID
            ViewBag.UID = UID;
            SOSOshop.BLL.MemberAccount bll = new SOSOshop.BLL.MemberAccount();
            string oldPassword = Request["oldPassword"];
            if (string.IsNullOrEmpty(oldPassword))
            {
                ModelState.AddModelError("", "请填写旧密码！");
            }
            else if (!ChangeHope.Common.DEncryptHelper.Encrypt(oldPassword, 1).Equals(bll.ExecuteScalar("select Password from memberaccount where UID=" + UID).ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                ModelState.AddModelError("", "填写的旧密码不正确！");
            }
            else
            {
                string newPassword = Request["newPassword"];
                string comfirmPassword = Request["comfirmPassword"];
                if (string.IsNullOrEmpty(newPassword))
                {
                    ModelState.AddModelError("", "请填写新密码！");
                }
                else if (!newPassword.Equals(comfirmPassword, StringComparison.CurrentCultureIgnoreCase))
                {
                    ModelState.AddModelError("", "再次输入密码不正确！");
                }
                else
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("update memberaccount set Password=@Password ");
                    sql.Append("where UID=@UID ");
                    DbCommand dbCommand = bll._db.GetSqlStringCommand(sql.ToString());
                    newPassword = ChangeHope.Common.DEncryptHelper.Encrypt(newPassword, 1);
                    bll._db.AddInParameter(dbCommand, "Password", DbType.AnsiString, newPassword);
                    bll._db.AddInParameter(dbCommand, "UID", DbType.Int32, UID);
                    ok = 0 < Convert.ToInt32(bll._db.ExecuteNonQuery(dbCommand));
                    ModelState.AddModelError("", ok ? "保存成功。" : "保存失败！");
                }
            }
            ViewBag.ok = ok;
            return View("Password");
        }

        // 修改验证邮箱
        // GET: /MemberCenter/Email

        [Authorize]
        public ActionResult Email()
        {
            int UID = BaseController.GetUserId();//账户ID
            ViewBag.UID = UID;
            int Step = 1; int.TryParse(Request["Step"], out Step); if (Step < 1) Step = 1;
            bool StepOk = false;
            string Email = "";
            SOSOshop.BLL.MemberAccount bll = new SOSOshop.BLL.MemberAccount();
            if (Step >= 3)
            {
                bool ok = false;
                DateTime h24 = DateTime.Now.AddHours(-24.0);
                string source = "http://" + Request.Url.Host + "/include/ajax.ashx?act=updateEmailComplete&v";
                string where = "UID = " + UID + " AND CHARINDEX('" + Server.UrlEncode(source) + "', Source) > 0 AND OperateTime > CONVERT(DATETIME, '" + h24.ToString() + "', 120)";
                DataSet ds = SysLog.Select("FieldForValue = '1' AND FieldAfterValue = '1' AND " + where);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Email = ds.Tables[0].Rows[0]["FieldName"].ToString();
                }
                if (!string.IsNullOrEmpty(Email))
                {
                    ViewBag.Email = Email;
                    StringBuilder sql = new StringBuilder();
                    sql.Append("update memberaccount set Email=@Email ");
                    sql.Append("where UID=@UID ");
                    DbCommand dbCommand = bll._db.GetSqlStringCommand(sql.ToString());
                    bll._db.AddInParameter(dbCommand, "Email", DbType.AnsiString, Email);
                    bll._db.AddInParameter(dbCommand, "UID", DbType.Int32, UID);
                    ok = 0 < Convert.ToInt32(bll._db.ExecuteNonQuery(dbCommand));
                }
                if (ok)
                {
                    bll.ExecuteNonQuery(string.Format("IF (NOT EXISTS(SELECT TOP(1) * FROM membercheck WHERE CheckType='E' AND UID={0})) INSERT INTO membercheck (UID, Checked, CheckType) VALUES ({0},1,'E') ELSE UPDATE membercheck SET Checked=1 WHERE CheckType='E' AND UID={0}", UID));
                    Step = 3;
                }
                else
                {
                    Step = 1;
                }
            }
            else
            {
                if (Session["updateEmail"] == null)
                {
                    string act1 = Step <= 1 ? "updateEmail" : "updateEmailComplete";
                    DateTime h24 = DateTime.Now.AddHours(-24.0);
                    string source = "http://" + Request.Url.Host + "/include/ajax.ashx?act=" + act1 + "&v";
                    string where = "UID = " + UID + " AND CHARINDEX('" + Server.UrlEncode(source) + "', Source) > 0 AND OperateTime > CONVERT(DATETIME, '" + h24.ToString() + "', 120)";
                    int getpass_ticks = SysLog.SelectCount("FieldForValue = '1' AND FieldAfterValue = '0' AND " + where);
                    StepOk = (getpass_ticks > 0);
                }
                if (Step == 2 && Session["updateEmail"] != null)
                {
                    Email = null;
                    Session["updateEmail"] = null;
                }
                else
                {
                    Email = Convert.ToString(bll.ExecuteScalar("select Email from memberaccount where UID=" + UID));
                }
            }
            ViewBag.Step = Step;
            ViewBag.StepOk = StepOk;
            ViewBag.Email = Email;
            return View();
        }

        // 修改验证邮箱
        // GET: /MemberCenter/EmailUpdate

        [Authorize]
        [HttpPost]
        public ActionResult EmailUpdate1()
        {
            int UID = BaseController.GetUserId();//账户ID
            ViewBag.UID = UID;
            ViewBag.Step = 1;
            ViewBag.StepOk = false;
            SOSOshop.BLL.MemberAccount bll = new SOSOshop.BLL.MemberAccount();
            string Email = Request["Email"];
            ViewBag.Email = Email;
            string Captcha = Request["Captcha"];
            if (string.IsNullOrEmpty(Captcha) || !Captcha.Equals(Convert.ToString(Session["Captcha"]), StringComparison.CurrentCultureIgnoreCase))
            {
                ModelState.AddModelError("", "提供的验证码不正确。");
            }
            else
            {
                if (string.IsNullOrEmpty(Email))
                {
                    ModelState.AddModelError("", "请填写邮箱地址！");
                }
                else if (!Common.ValidateHelper.IsEmail(Email))
                {
                    ModelState.AddModelError("", "请填写正确的邮箱地址！");
                }
                else if (null != bll.ExecuteScalar("select top(1) UID from memberaccount where UID<>" + UID + " AND Email='" + Email + "'"))
                {
                    ModelState.AddModelError("", "请填写其它邮箱地址！此邮箱地址已经在系统中使用！");
                }
                else
                {
                    bool ok = false;
                    BaseController.SetAccount(ViewBag);
                    Session["Captcha"] = null;
                    string url = "http://" + Request.Url.Host + "/include/ajax.ashx?act=updateEmail&v=" + Guid.NewGuid().ToString() + "&uid=" + UID;
                    if (new SysLog(UID, 0, Server.UrlEncode(url), "会员邮箱验证", "", "1", "0", "会员中心", "验证邮箱", OperateCode.添加记录, DateTime.Now).Record())
                    {
                        Response.Redirect(url, true);

                    }
                    if (ok)
                    {
                        ViewBag.StepOk = true;
                    }
                    else
                    {
                        ModelState.AddModelError("", "操作失败！");
                    }
                }
            }
            return View("Email");
        }
        [Authorize]
        [HttpPost]
        public ActionResult EmailUpdate2()
        {
            int UID = BaseController.GetUserId();//账户ID
            ViewBag.UID = UID;
            ViewBag.Step = 2;
            ViewBag.StepOk = false;
            SOSOshop.BLL.MemberAccount bll = new SOSOshop.BLL.MemberAccount();
            string Email = Request["Email"];
            ViewBag.Email = Email;
            string Captcha = Request["Captcha"];
            if (string.IsNullOrEmpty(Captcha) || !Captcha.Equals(Convert.ToString(Session["Captcha"]), StringComparison.CurrentCultureIgnoreCase))
            {
                ModelState.AddModelError("", "提供的验证码不正确。");
            }
            else
            {
                if (string.IsNullOrEmpty(Email))
                {
                    ModelState.AddModelError("", "请填写邮箱地址！");
                }
                else if (!Common.ValidateHelper.IsEmail(Email))
                {
                    ModelState.AddModelError("", "请填写正确的邮箱地址！");
                }
                else if (null != bll.ExecuteScalar("select top(1) UID from memberaccount where UID<>" + UID + " AND Email='" + Email + "'"))
                {
                    ModelState.AddModelError("", "请填写其它邮箱地址！此邮箱地址已经在系统中使用！");
                }
                else
                {
                    bool ok = false;
                    BaseController.SetAccount(ViewBag);
                    Session["Captcha"] = null;
                    string url = "http://" + Request.Url.Host + "/include/ajax.ashx?act=updateEmailComplete&v=" + Guid.NewGuid().ToString() + "&uid=" + UID;
                    if (new SysLog(UID, 0, Server.UrlEncode(url), "会员邮箱验证", Email, "1", "0", "会员中心", "验证邮箱", OperateCode.添加记录, DateTime.Now).Record())
                    {
                        string title = string.Format("{0} 邮箱验证提醒", ConfigurationManager.AppSettings["CompanyFullName"]);
                        string content = System.IO.File.ReadAllText(Server.MapPath("/Config/邮箱验证提醒.config"));
                        content = content.Replace("{Now}", DateTime.Now.ToString("yyyy年MM月dd日 hh时mm分ss秒"));
                        content = content.Replace("{UserId}", (string.IsNullOrEmpty(ViewBag.TrueName) ? ViewBag.MobilePhone : ViewBag.TrueName));
                        content = content.Replace("{URL}", url);
                        try
                        {
                            SmtpMail ESM = new SmtpMail();
                            ESM.AddRecipient(Email.Split(','));
                            ok = ESM.Send(title, content);
                        }
                        catch { }
                    }
                    if (ok)
                    {
                        ViewBag.StepOk = true;
                    }
                    else
                    {
                        ModelState.AddModelError("", "操作失败！");
                    }
                }
            }
            return View("Email");
        }

        // 修改验证手机
        // GET: /MemberCenter/MobilePhone

        [Authorize]
        public ActionResult MobilePhone()
        {
            int UID = BaseController.GetUserId();//账户ID
            ViewBag.UID = UID;
            int Step = 1; int.TryParse(Request["Step"], out Step); if (Step < 1) Step = 1;
            bool StepOk = false;
            string MobilePhone = "";
            SOSOshop.BLL.MemberAccount bll = new SOSOshop.BLL.MemberAccount();
            if (Step >= 3)
            {
                bool ok = false;
                DateTime h24 = DateTime.Now.AddMinutes(-10.0);
                string source = "http://" + Request.Url.Host + "/include/ajax.ashx?act=updateMobilePhoneComplete&v";
                string where = "UID = " + UID + " AND CHARINDEX('" + Server.UrlEncode(source) + "', Source) > 0 AND OperateTime > CONVERT(DATETIME, '" + h24.ToString() + "', 120)";
                DataSet ds = SysLog.Select("FieldForValue = '1' AND FieldAfterValue = '1' AND " + where);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    MobilePhone = ds.Tables[0].Rows[0]["FieldName"].ToString();
                }
                if (!string.IsNullOrEmpty(MobilePhone))
                {
                    ViewBag.Email = MobilePhone;
                    StringBuilder sql = new StringBuilder();
                    sql.Append("update memberaccount set MobilePhone=@MobilePhone ");
                    sql.Append("where UID=@UID ");
                    DbCommand dbCommand = bll._db.GetSqlStringCommand(sql.ToString());
                    bll._db.AddInParameter(dbCommand, "MobilePhone", DbType.AnsiString, MobilePhone);
                    bll._db.AddInParameter(dbCommand, "UID", DbType.Int32, UID);
                    ok = 0 < Convert.ToInt32(bll._db.ExecuteNonQuery(dbCommand));
                }
                if (ok)
                {
                    bll.ExecuteNonQuery(string.Format("IF (NOT EXISTS(SELECT TOP(1) * FROM membercheck WHERE CheckType='M' AND UID={0})) INSERT INTO membercheck (UID, Checked, CheckType) VALUES ({0},1,'M') ELSE UPDATE membercheck SET Checked=1 WHERE CheckType='M' AND UID={0}", UID));
                    Step = 3;
                }
                else
                {
                    Step = 1;
                }
            }
            else
            {
                if (Session["updateMobilePhone"] == null)
                {
                    string act1 = Step <= 1 ? "updateMobilePhone" : "updateMobilePhoneComplete";
                    DateTime h24 = DateTime.Now.AddHours(-24.0);
                    string source = "http://" + Request.Url.Host + "/include/ajax.ashx?act=" + act1 + "&v";
                    string where = "UID = " + UID + " AND CHARINDEX('" + Server.UrlEncode(source) + "', Source) > 0 AND OperateTime > CONVERT(DATETIME, '" + h24.ToString() + "', 120)";
                    int getpass_ticks = SysLog.SelectCount("FieldForValue = '1' AND FieldAfterValue = '0' AND " + where);
                    StepOk = (getpass_ticks > 0);
                }
                if (Step == 2 && Session["updateMobilePhone"] != null)
                {
                    MobilePhone = null;
                    Session["updateMobilePhone"] = null;
                }
                else
                {
                    MobilePhone = Convert.ToString(bll.ExecuteScalar("select MobilePhone from memberaccount where UID=" + UID));
                }
            }
            ViewBag.Step = Step;
            ViewBag.StepOk = StepOk;
            ViewBag.MobilePhone = MobilePhone;
            return View();
        }

        // 修改验证手机
        // GET: /MemberCenter/MobilePhoneUpdate

        [Authorize]
        [HttpPost]
        public ActionResult MobilePhoneUpdate1()
        {
            int UID = BaseController.GetUserId();//账户ID
            ViewBag.UID = UID;
            ViewBag.Step = 1;
            ViewBag.StepOk = (Request["StepOk"] != null && Request["StepOk"].ToLower() == "true");
            SOSOshop.BLL.MemberAccount bll = new SOSOshop.BLL.MemberAccount();
            string MobilePhone = Request["MobilePhone"];
            ViewBag.MobilePhone = MobilePhone;
            Session["updateMobilePhone"] = null;
            string Captcha = Request["Captcha"];
            if (ViewBag.StepOk && (string.IsNullOrEmpty(Captcha) || !Captcha.Equals(Convert.ToString(Session["Captcha"]), StringComparison.CurrentCultureIgnoreCase)))
            {
                ModelState.AddModelError("", "提供的验证码不正确。");
            }
            else
            {
                if (!ViewBag.StepOk)
                {
                    if (string.IsNullOrEmpty(MobilePhone))
                    {
                        ModelState.AddModelError("", "请填写手机号！");
                    }
                    else if (!Common.ValidateHelper.IsMobilePhone(MobilePhone))
                    {
                        ModelState.AddModelError("", "请填写正确的手机号！");
                    }
                    else if (null != bll.ExecuteScalar("select top(1) UID from memberaccount where UID<>" + UID + " AND MobilePhone='" + MobilePhone + "'"))
                    {
                        ModelState.AddModelError("", "请填写其它手机号！此手机号已经在系统中使用！");
                    }
                    else
                    {
                        bool ok = false;
                        BaseController.SetAccount(ViewBag);
                        string smsCaptcha = Common.CheckCode.GenerateNumber();
                        Session["Captcha"] = smsCaptcha;
                        string url = "http://" + Request.Url.Host + "/include/ajax.ashx?act=updateMobilePhone&v=" + Guid.NewGuid().ToString();
                        var sms = new SOSOshop.MSG.Sms();
                        object phone = db.ExecuteScalar("select OfficePhone from yxs_administrators where adminid=(select Editer from memberinfo where UID=" + UID + ")");
                        if (phone == null || phone.ToString().Trim() == "") phone = "028-66321993";
                        string SmsMsg = "您的账户安全验证码为：" + smsCaptcha + "，请在页面填写。如非本人操作，请致电您的专属采购顾问" + phone;
                        string from = "系统";
                        string to = MobilePhone;
                        ok = SOSOshop.BLL.Sms.SendAndSaveDataBase(MobilePhone, SmsMsg, from, to);
                        ok = ok && new SysLog(UID, 0, Server.UrlEncode(url), "会员手机验证", smsCaptcha, "1", "0", "会员中心", "验证手机", OperateCode.添加记录, DateTime.Now).Record();
                        if (ok)
                        {
                            ViewBag.StepOk = true;
                        }
                        else
                        {
                            ModelState.AddModelError("", "抱歉，当前用户设置的手机号码可能有误或网络繁忙，你可以重新发送短信！");
                        }
                    }
                }
                else
                {
                    Session["updateMobilePhone"] = true;
                    Response.Redirect("/MemberCenter/MobilePhone?Step=2", true);
                }
            }
            return View("MobilePhone");
        }
        [Authorize]
        [HttpPost]
        public ActionResult MobilePhoneUpdate2()
        {
            int UID = BaseController.GetUserId();//账户ID
            ViewBag.UID = UID;
            ViewBag.Step = 2;
            ViewBag.StepOk = (Request["StepOk"] != null && Request["StepOk"].ToLower() == "true");
            SOSOshop.BLL.MemberAccount bll = new SOSOshop.BLL.MemberAccount();
            string MobilePhone = Request["MobilePhone"];
            ViewBag.MobilePhone = MobilePhone;
            string Captcha = Request["Captcha"];
            if (ViewBag.StepOk && (string.IsNullOrEmpty(Captcha) || !Captcha.Equals(Convert.ToString(Session["Captcha"]), StringComparison.CurrentCultureIgnoreCase)))
            {
                ModelState.AddModelError("", "提供的验证码不正确。");
            }
            else
            {
                if (!ViewBag.StepOk)
                {
                    if (string.IsNullOrEmpty(MobilePhone))
                    {
                        ModelState.AddModelError("", "请填写手机号！");
                    }
                    else if (!Common.ValidateHelper.IsMobilePhone(MobilePhone))
                    {
                        ModelState.AddModelError("", "请填写正确的手机号！");
                    }
                    else if (null != bll.ExecuteScalar("select top(1) UID from memberaccount where UID<>" + UID + " AND MobilePhone='" + MobilePhone + "'"))
                    {
                        ModelState.AddModelError("", "请填写其它手机号！此手机号已经在系统中使用！");
                    }
                    else
                    {
                        bool ok = false;
                        BaseController.SetAccount(ViewBag);
                        string smsCaptcha = Common.CheckCode.GenerateNumber();
                        Session["Captcha"] = smsCaptcha;
                        string url = "http://" + Request.Url.Host + "/include/ajax.ashx?act=updateMobilePhone&v=" + Guid.NewGuid().ToString();
                        var sms = new SOSOshop.MSG.Sms();
                        object phone = db.ExecuteScalar("select OfficePhone from yxs_administrators where adminid=(select Editer from memberinfo where UID=" + UID + ")");
                        if (phone == null || phone.ToString().Trim() == "") phone = "028-66321993";
                        //string SmsMsg = "您的账户安全验证码为：" + smsCaptcha + "，请在页面填写。如非本人操作，请致电您的专属采购顾问" + phone;
                        string SmsMsg = "尊敬的用户您好，您的验证码为：" + smsCaptcha + "，请在30秒内完成短信验证，谢谢！";
                        string from = "系统";
                        string to = MobilePhone;
                        ok = SOSOshop.BLL.Sms.SendAndSaveDataBase(MobilePhone, SmsMsg, from, to);
                        ok = ok && new SysLog(UID, 0, Server.UrlEncode(url), "会员手机验证", smsCaptcha, "1", "0", "会员中心", "验证手机", OperateCode.添加记录, DateTime.Now).Record();
                        if (ok)
                        {
                            ViewBag.StepOk = true;
                        }
                        else
                        {
                            ModelState.AddModelError("", "抱歉，当前用户设置的手机号码可能有误或网络繁忙，你可以重新发送短信！");
                        }
                    }
                }
                else
                {
                    string url = "http://" + Request.Url.Host + "/include/ajax.ashx?act=updateMobilePhoneComplete&v=" + Guid.NewGuid().ToString();
                    bool ok = new SysLog(UID, 0, Server.UrlEncode(url), "会员手机验证", MobilePhone, "1", "1", "会员中心", "验证手机", OperateCode.添加记录, DateTime.Now).Record();
                    if (ok)
                    {
                        Response.Redirect("/MemberCenter/MobilePhone?Step=3", true);
                    }
                    else
                    {
                        ViewBag.StepOk = false;
                        ViewBag.MobilePhone = MobilePhone;
                        ModelState.AddModelError("", "抱歉，当前用户设置的手机号码可能有误或网络繁忙，你可以重新发送短信！");
                    }
                }
            }
            return View("MobilePhone");
        }

        //[Authorize]
        //public ActionResult Qualifications()
        //{
        //    int UID = BaseController.GetUserId();//账户ID
        //    ViewBag.UID = UID;
        //    BaseController.SetAccount(ViewBag);
        //    IncreatedQualifications bll = new IncreatedQualifications();
        //    ViewBag.IncreatedQualifications = bll.GetModel(UID);

        //    //资质, 调用ERP:wldwwdzl往来单位文档资料
        //    ViewBag.Qualifications = BaseController.GetQualifications(UID);


        //    return View();
        //}

        //[Authorize]
        //[HttpPost]
        //public ActionResult QualificationsUpdate()
        //{
        //    int UID = BaseController.GetUserId();
        //    string DrugsBase_Enterprise = Request["DrugsBase_Enterprise"];
        //    string TaxpayerID = Request["TaxpayerID"];
        //    string Address = Request["Address"];
        //    string TelPhone = Request["TelPhone"];
        //    string BankName = Request["BankName"];
        //    string BankAccount = Request["BankAccount"];
        //    SOSOshop.BLL.IncreatedQualifications bll = new SOSOshop.BLL.IncreatedQualifications();
        //    SOSOshop.Model.IncreatedQualifications model = new SOSOshop.Model.IncreatedQualifications();
        //    model.UID = UID;
        //    model.DrugsBase_Enterprise = DrugsBase_Enterprise;
        //    model.TaxpayerID = TaxpayerID;
        //    model.Address = Address;
        //    model.TelPhone = TelPhone;
        //    model.BankName = BankName;
        //    model.BankAccount = BankAccount;
        //    if (bll.GetModel(UID) == null)
        //    {
        //        bll.Add(model);
        //        ModelState.AddModelError("", "添加成功。");
        //    }
        //    else
        //    {
        //        bll.Update(model);
        //        ModelState.AddModelError("", "保存成功。");
        //    }

        //    BaseController.SetAccount(ViewBag);
        //    ViewBag.IncreatedQualifications = bll.GetModel(UID);
        //    int ParentId = 0;//默认单位ID
        //    SOSOshop.BLL.MemberAccount abll = new SOSOshop.BLL.MemberAccount();
        //    StringBuilder sql = new StringBuilder();
        //    sql.AppendFormat("select ParentId from memberinfo where UID={0}", UID);
        //    using (IDataReader rd = (IDataReader)abll.ExecuteReader(sql.ToString()))
        //    {
        //        if (rd != null && rd.Read())
        //        {
        //            ParentId = int.Parse(rd[0].ToString());
        //            rd.Close();
        //            //资质, 调用ERP接口
        //            ViewBag.Qualifications = BaseController.GetQualifications(UID);
        //        }
        //    }
        //    return View("Qualifications");
        //}

        #region 会员中心 - 账户管理

        #region 收货地址

        [Authorize]
        public ActionResult ReceAddress()
        {
            int UID = BaseController.GetUserId();//账户ID
            ViewBag.UID = UID;
            SOSOshop.BLL.ReceAddress address = new ReceAddress();

            ViewBag.Address = address.GetAddressListByWhere(" and uid=" + UID + " order by stat desc");

            return View();
        }

        /// <summary>
        /// 保存会员收货地址
        /// </summary>
        /// <param name="data">保存的收货地址</param>
        /// <param name="id">自增编号</param>
        /// <returns>1：成功</returns>
        [HttpPost]
        public string Save_Member_MemberReceAddress()
        {
            SOSOshop.BLL.MemberReceAddress memberReceAddress = new SOSOshop.BLL.MemberReceAddress();
            SOSOshop.Model.MemberReceAddress data = new SOSOshop.Model.MemberReceAddress();
            data.ID = string.IsNullOrEmpty(Request["id"]) ? 0 : Convert.ToInt32(Request["id"]);

            if (data.ID != 0)
            {
                data = memberReceAddress.GetModel(data.ID);
            }
            else
            {
                data.Phone = "";
                data.Consignestime = "";
                data.ConstructionSigns = "";
                data.Stat = false;
                data.Email = "";
            }

            data.UID = BaseController.GetUserId();
            data.Name = string.IsNullOrEmpty(Request["userName"]) ? null : Request["userName"];
            data.Province = string.IsNullOrEmpty(Request["province"]) ? "" : Request["province"];
            data.City = string.IsNullOrEmpty(Request["city"]) ? "" : Request["city"];
            data.Borough = string.IsNullOrEmpty(Request["borough"]) ? "" : Request["borough"];
            data.Address = string.IsNullOrEmpty(Request["address"]) ? null : Request["address"];
            data.Mobile = string.IsNullOrEmpty(Request["mobile"]) ? "" : Request["mobile"];
            data.Zip = string.IsNullOrEmpty(Request["postCode"]) ? "" : Request["postCode"];

            if (data.Province.ToLower() != "null" && data.City.ToLower() != "null")
            {
                //如果是新增
                if (data.ID == 0 || !memberReceAddress.Exists(data.ID))
                {
                    memberReceAddress.Add(data);
                }
                //如果是修改
                else
                {
                    memberReceAddress.Update(data, data.UID);
                }

                return "保存成功！";
            }
            else
            {
                return "保存失败！";
            }
        }

        /// <summary>
        /// 删除会员收货地址
        /// </summary>
        /// <param name="id">自增编号</param>
        /// <returns>1表示删除成功，0表示删除的数据在数据库中不存在</returns>
        [HttpPost]
        public string Delete_Member_MemberReceAddress()
        {
            int id = string.IsNullOrEmpty(Request["id"]) ? -1 : Convert.ToInt32(Request["id"]);
            int uid = BaseController.GetUserId();
            SOSOshop.BLL.MemberReceAddress memberReceAddress = new SOSOshop.BLL.MemberReceAddress();

            //如果数据不存在（新增）
            if (!memberReceAddress.Exists(id))
            {
                return "no";
            }
            //如果数据已存在
            else
            {
                memberReceAddress.Delete(id, uid);

                return "ok";
            }
        }

        /// <summary>
        /// 设置会员收货地址的默认地址
        /// </summary>
        /// <param name="id">自增编号</param>
        /// <returns>1表示设置默认成功，0表示设置默认的数据在数据库中不存在</returns>
        [HttpPost]
        public string Set_Member_MemberReceAddress_As_Default()
        {
            int uid = BaseController.GetUserId();
            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : Convert.ToInt32(Request["id"]);
            SOSOshop.BLL.MemberReceAddress memberReceAddress = new SOSOshop.BLL.MemberReceAddress();

            //如果数据不存在（新增）
            if (!memberReceAddress.Exists(id))
            {
                return "no";
            }
            //如果数据已存在
            else
            {
                StringBuilder sbStr = new StringBuilder();

                sbStr.Append("update memberreceaddress set ");
                sbStr.Append("stat = 'true' ");
                sbStr.AppendFormat("where id = {0} and uid={1} ", id, uid);
                sbStr.Append("update memberreceaddress set ");
                sbStr.Append("stat = 'false' ");
                sbStr.AppendFormat("where id != {0} and uid={1} ", id, uid);

                memberReceAddress.ExecuteDataSet(sbStr.ToString());

                return "ok"; ;
            }
        }

        /// <summary>
        /// 根据id获取用户收货地址
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUserRecieveAddressInfo()
        {
            string info = string.Empty;
            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : Convert.ToInt32(Request["id"]);
            SOSOshop.BLL.MemberReceAddress memberReceAddress = new SOSOshop.BLL.MemberReceAddress();
            SOSOshop.Model.MemberReceAddress data = memberReceAddress.GetModel(id);

            if (data != null)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 我的关注

        [Authorize]
        public ActionResult Follow()
        {
            int UID = BaseController.GetUserId();//账户ID
            ViewBag.UID = UID;
            ViewBag.MemberFavoriteList = Get_Member_MemberFavoriteList(UID);

            return View();
        }

        /// <summary>
        /// 获取当前会员的关注商品列表
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Member_MemberFavoriteList(int uid)
        {
            StringBuilder sbStr = new StringBuilder();
            sbStr.AppendFormat("SELECT " + SOSOshop.BLL.Product.Product._PriceTableColumns.Replace("is_ZYC", "ISNULL((SELECT 1 FROM DrugsBase_ZYC WHERE (DrugsBase_ID=p.DrugsBase_ID)), 0) AS is_ZYC") + "memberfavorite.id,is_cl, uid, Product_ID, sellType, ProId, Goods_Unit, Product_Name, DrugsBase_Manufacturer, Goods_Pcs, Goods_Pcs_Small, AddDate, drug_sensitive,minsell,maxsell " +
                               "FROM memberfavorite " +
                               "INNER JOIN product p ON memberfavorite.ProId = p.Product_ID " +
                               "WHERE uid = {0}", uid);

            return db.ExecuteTable(sbStr.ToString()).GetPriceTable();
        }

        /// <summary>
        /// 取消关注
        /// </summary>
        [HttpPost]
        public string MemberFavorite_CancelAttention()
        {
            int userID = BaseController.GetUserId();
            int id = string.IsNullOrEmpty(Request["id"]) ? -1 : Convert.ToInt32(Request["id"]);

            SOSOshop.BLL.Memberfavorite memberfavorite = new Memberfavorite();
            memberfavorite.CancelMemberFavorite(id, userID);

            return "删除成功！";
        }

        /// <summary>
        /// 删除用户全部的关注信息
        /// </summary>
        [HttpPost]
        public string DeleteAll()
        {
            int userID = BaseController.GetUserId();
            StringBuilder sbStr = new StringBuilder();
            sbStr.AppendFormat("DELETE FROM memberfavorite WHERE uid = {0}", userID);

            db.ExecuteNonQuery(sbStr.ToString());

            return "删除成功！";
        }

        #endregion

        #endregion

        #region 会员中心 - 我的订单
        [Authorize]
        public ActionResult Orders()
        {
            //记录总数
            int recordCount = 0;

            //页总数
            int pageCount = 0;

            //页大小
            int pageSize = 12;

            //当前页
            int pageIndex = 1;
            if (!string.IsNullOrEmpty(Request["pageindex"]))
            {
                pageIndex = int.Parse(Request["pageindex"]);
            }

            StringBuilder s = new StringBuilder();
            //缺货订单
            s.Append(string.IsNullOrEmpty(Request["wait"]) ? "" : " and orderid in (select orderid from orderproduct where status=3)");

            //搜索订单
            string search = Request["search"];
            search = Library.Lang.Input.Filter(search);
            ViewBag.search = search;
            s.Append(string.IsNullOrEmpty(search) ? "" : " and orderid in (select orderid from orderproduct where ProName like '%" + search + "%' or OrderId like '%" + search + "%')");

            //订单状态
            s.Append(string.IsNullOrEmpty(Request["ordertype"]) ? "" : " and ordertype=" + Request["ordertype"]);
            ViewBag.orderdate = Request["orderdate"];

            //支付类型
            s.Append(string.IsNullOrEmpty(Request["Paymenttype"]) ? "" : " and Paymenttype=" + Request["Paymenttype"]);
            ViewBag.Paymenttype = Request["Paymenttype"];

            //订单状态
            string s_orderstatus = (string.IsNullOrEmpty(Request["orderstatus"]) ? "" : " and dbo.fn_QianTai_Select_Orders_OrderStatus(OrderId)=" + Request["orderstatus"]);
            ViewBag.orderstatus = Request["orderstatus"];

            //订单时间
            ViewBag.orderdate = Request["orderdate"];
            if (!string.IsNullOrEmpty(Request["orderdate"]))
            {
                switch (int.Parse(Request["orderdate"]))
                {
                    case 1:
                        s.AppendFormat(" and shopdate>'{0}'", DateTime.Now.AddDays(-7));//一周内
                        break;
                    case 2:
                        s.AppendFormat(" and shopdate>'{0}'", DateTime.Now.AddMonths(-1));//一个月内
                        break;
                    case 3:
                        s.AppendFormat(" and shopdate>'{0}'", DateTime.Now.AddMonths(-3));//三个月内
                        break;

                }
            }
            s.Append(" and ReceiverId=" + BaseController.GetUserId());

            //读取列表数据
            SOSOshop.BLL.Db db = new Db();
            //字段增加两个条件 iswait表示待用户处理的商品数, isprocess大于0表示订单正在处理中，用户将不能取消订单;去掉时间的限制 datediff(minute,shopdate,getdate()) as mtime ,
            string fields = "OrderId";
            fields += ",(select top 1 CONVERT(VARCHAR(3),Payment)+','+CONVERT(VARCHAR(3),PaymentType)+','+CONVERT(VARCHAR(3),PaymentStatus) from Orders where OrderId=T.OrderId)Temp";
            fields += @",Payment
                        ,PaymentType
                        ,OrderStatus
                        ,PaymentStatus
                        ,(select top 1 BusinessCheckDate from Orders where OrderId =T.OrderId) BusinessCheckDate";
            fields += ",ISNULL((SELECT TotalPrice FROM dbo.Orders WHERE OrderId=T.OrderId),(SELECT TOP 1 TotalPrice FROM Orders WHERE OrderId=T.OrderId))TotalPrice";
            fields += ",ShopDate";
            fields += ",(select count(id) from orderproduct as x where (x.OrderId=T.OrderId) and x.status=3) as iswait";
            fields += ",case when exists(select 1 from Orders x where (x.OrderId=T.OrderId) and OrderStatus<>1 and OrderStatus<>3) then 1 else (select count(id) from OrderProduct as x where (x.OrderId=T.OrderId) and x.Status<>1) end as isprocess ";
            DataTable dt = db.GetListByPage("Orders", fields, pageSize, pageIndex, " shopdate desc ", s_orderstatus + s, out recordCount, out pageCount);
            foreach (DataRow item in dt.Rows)
            {
                if (!Library.Lang.DataValidator.isNULL(item["Temp"] as string))
                {
                    string[] temp = item["Temp"].ToString().Split(',');
                    item["Payment"] = int.Parse(temp[0]);
                    item["PaymentType"] = int.Parse(temp[1]);
                    item["OrderStatus"] = GetOrderStatus(item["OrderId"] as string);
                }
            }

            //展示的列表
            ViewBag.List = dt;

            //定义页面
            PagedList<DataRow> pl = new PagedList<DataRow>(dt.Select(), pageIndex, pageSize, recordCount);

            return View(pl);
        }
        /// <summary>
        /// 取得订单状态(合并订单显示)
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        [NonAction]
        public int GetOrderStatus(string orderid)
        {
            string sql = string.Format(@"IF EXISTS(SELECT OrderStatus FROM dbo.Orders WHERE OrderId='{0}' AND OrderType<>0)
                                         SELECT OrderStatus FROM dbo.Orders WHERE OrderId='{0}'
                                         ELSE 
                                         SELECT DISTINCT OrderStatus FROM dbo.Orders WHERE OrderId LIKE '{0}%'", orderid);
            DataTable dt = db.ExecuteTable(sql);
            if (dt.Rows.Count == 1)
            {
                return (int)dt.Rows[0][0];
            }
            var li = dt.AsEnumerable().Select(x => x.Field<int>("OrderStatus"));
            if (li.Contains(1))
            {
                return 1;
            }
            if (li.Contains(2))
            {
                return 2;
            }
            if (li.Contains(3))
            {
                return 3;
            }
            if (li.Contains(4))
            {
                return 4;
            }
            if (li.Contains(-1))
            {
                return -1;
            }
            if (li.Contains(-2))
            {
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 订单产品里的商品名称
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public DataTable OrderProudctName(string orderId)
        {
            string sql = string.Format("select o.* from OrderProduct o where o.orderid in(SELECT Id FROM [dbo].[fn_QianTai_Select_Orders_OrderId]('{0}')f)", orderId);
            SOSOshop.BLL.Db db = new Db();
            DataTable dt = db.ExecuteTableForCache(sql);
            return dt;
        }
        /// <summary>
        /// 订单产品里的商品名称
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public DataTable OrderProudctNameAndPihao(string orderId)
        {
            string ckSql = ""; Sql_Table_SelectPihao(ref ckSql);
            ckSql = ",(select top(1) b.pihao " + ckSql + " where g.webdjbh=o.orderid and b.spid=p.spid)pihao";
            string sql = string.Format("select o.*{1} from OrderProduct o inner join Product p on o.ProId=p.Product_ID where o.orderid in(SELECT Id FROM [dbo].[fn_QianTai_Select_Orders_OrderId]('{0}')f)", orderId, ckSql);
            SOSOshop.BLL.Db db = new Db();
            DataTable dt = db.ExecuteTableForCache(sql);
            return dt;
        }

        /// <summary>
        /// 订单详情列表
        /// </summary>
        /// <param name="OrderNo">订单号</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult OrderDetails(string OrderNo)
        {
            OrderNo = Request["OrderNo"];
            OrderNo = Library.Lang.Input.Filter(OrderNo);
            ViewBag.OrderNo = OrderNo;
            //取得订单商品列表
            SOSOshop.BLL.Db db = new Db();
            string sql = "select orderid from orders where orderid like '" + OrderNo + "%' and ReceiverId=" + BaseController.GetUserId();
            if (db.ExecuteScalarForCache(sql) != null)
            {
                string sqldetails = string.Format("select a.*,isnull((select DrugsBase_Manufacturer from orderproductdetails as c where c.orderid=a.orderid and c.proid=a.proid),(select DrugsBase_Manufacturer from product where Product_ID=a.proid)) as DrugsBase_Manufacturer,"
                    + "isnull((select GuiGe from orderproductdetails as c where c.orderid=a.orderid and c.proid=a.proid),(select DrugsBase_Specification from product where Product_ID=a.proid)) as GuiGe from orderproduct a where orderid in(SELECT Id FROM [dbo].[fn_QianTai_Select_Orders_OrderId]('{0}')f)", OrderNo);
                ViewBag.List = db.ExecuteTable(sqldetails);
                //取订单内容
                SOSOshop.Model.Order.Orders orders = new SOSOshop.Model.Order.Orders();
                SOSOshop.BLL.Order.Orders borders = new SOSOshop.BLL.Order.Orders();
                orders = borders.GetModelLike(OrderNo);
                ViewBag.Orders = orders;
                if (ViewBag.Orders == null || ViewBag.List == null) return RedirectToAction("Orders");
                return View();
            }
            return RedirectToAction("Orders");
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public string CancelOrders()
        {
            string ret = BaseController.Json(-1, "取消失败！");
            string id = Request["oid"];
            id = Library.Lang.Input.Filter(id);
            if (!string.IsNullOrEmpty(id) && User.Identity.IsAuthenticated)
            {
                SOSOshop.BLL.Order.Orders orders = new SOSOshop.BLL.Order.Orders();
                //try
                //{
                if (orders.CancelOrder(id))
                {
                    ret = BaseController.Json(1, "取消成功！");
                    SOSOshop.BLL.Logs.Log.LogShopAdd("订单ID:" + id + " 取消成功", "", BaseController.GetUserId(), User.Identity.Name, 1);//用户操作日志
                }
                //}
                //catch (Exception e)
                //{
                //    ret = BaseController.Json(-1, e.Message);
                //}
            }
            return ret;
        }

        private bool OrderStatus()
        {
            return true;
        }

        /// <summary>
        /// 更改商品状态
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public string CancelShop()
        {
            string ret = BaseController.Json(-1, "状态更改失败！");
            string pid = Request["pid"];
            string oid = Request["oid"];
            string status = Request["stu"];//4为预购,6为取消

            if (!string.IsNullOrEmpty(oid) && !string.IsNullOrEmpty(pid) && User.Identity.IsAuthenticated)
            {
                SOSOshop.BLL.Order.OrderProduct product = new SOSOshop.BLL.Order.OrderProduct();
                try
                {
                    if (product.UpdateShop(pid, oid, int.Parse(status)))
                    {
                        string msg = string.Format("订单ID:{1},商品ID:{0},操作事项：{2}", pid, oid, status == "4" ? "预购" : (status == "6" ? "取消" : ""));
                        SOSOshop.BLL.Logs.Log.LogShopAdd(msg, "", BaseController.GetUserId(), User.Identity.Name, 1);//用户操作日志
                        ret = BaseController.Json(1, "更改成功！");
                    }
                }
                catch (Exception e)
                {
                    SOSOshop.BLL.Logs.Log.LogShopAdd(e.Message, e.ToString(), BaseController.GetUserId(), User.Identity.Name);//异常日志
                }
            }
            return ret;
        }

        /// <summary>
        /// 是否缺货
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static bool IsNoShop(int status)
        {
            if (status == 3)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断订单的时间是否超过10分钟
        /// </summary>
        /// <param name="shoptime"></param>
        /// <returns>该放方法前台已停止使用</returns>
        public static bool IsModifyCancelOrders(DateTime shoptime)
        {
            TimeSpan d3 = DateTime.Now.Subtract(shoptime);

            if (d3.Minutes < 10)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// 拷贝订单里的商品到购物车（还要买）
        /// </summary>
        /// <returns></returns>

        public string CopyShopToShoppinCart()
        {
            string ret = BaseController.Json(-1, "复制失败！");
            string orderid = Request["oid"];
            if (!string.IsNullOrEmpty(orderid))
            {
                //查询订单里的商品
                string sql = string.Format("select ProId,ProNum from orderproduct where orderid in(SELECT Id FROM [dbo].[fn_QianTai_Select_Orders_OrderId]('{0}')f) AND ProId IN (SELECT Product_ID FROM dbo.product_online_v)", orderid);
                SOSOshop.BLL.Db db = new Db();
                DataSet ds = db.ExecuteDataSet(sql);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    ShoppingcartController sc = new ShoppingcartController();
                    bool copyok = true;
                    foreach (DataRow dv in dt.Rows)
                    {
                        try
                        {
                            //将查询到的商品放入到购物车中                        
                            int r = sc.addCarts(dv["proid"].ToString(), 0, (decimal)dv["pronum"], true);
                            if (r > 0)
                            {
                                ret = sc.ShowInfo(r);
                                copyok = false;
                                break;
                            }


                        }
                        catch (Exception ex)
                        {
                            //品种已经不上架
                            SOSOshop.BLL.Logs.Log.LogShopAdd("复制到购物车:" + ex.Message, ex.ToString(), 0, "", 2);
                        }

                    }
                    if (copyok)
                    {
                        ret = BaseController.Json(1, "该订单的商品已经复制到您的购物车！");
                    }
                }
            }
            else
            {
                ret = BaseController.Json(-1, "请选择订单号！");
            }
            return ret;
        }

        /// <summary>
        /// 待处理订单
        /// </summary>
        /// <returns></returns>
        public int WaitProcessOrders()
        {
            int uid = BaseController.GetUserId();
            string sql = string.Format("select orderid from orders where orderid in (select orderid from orderproduct where status=3) and ReceiverId={0}", uid);
            SOSOshop.BLL.Db db = new Db();
            DataTable dt = db.ExecuteTable(sql);
            return dt.Rows.Count;
        }

        /// <summary>
        /// 取缺货时，预计到货天数
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public static string GetWaitDay(int pid)
        {
            string sql = "SELECT describe FROM OrderProduct_Stockout where OrderProduct_id=" + pid;
            DataTable dt = new SOSOshop.BLL.Db().ExecuteTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                return string.Format("预计{0}到货", dt.Rows[0][0]);
            }
            else
            {
                return "";
            }
        }

        #endregion

        #region 会员中心 - 交易意向
        /// <summary>
        /// 管理交易意向
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult TradingIntention()
        {
            //记录总数
            int recordCount = 0;

            //页总数
            int pageCount = 0;

            //页大小
            int pageSize = 12;

            //当前页
            int pageIndex = 1;
            if (!string.IsNullOrEmpty(Request["pageindex"]))
            {
                pageIndex = int.Parse(Request["pageindex"]);
            }

            StringBuilder s = new StringBuilder();
            //搜索产品
            string search = Request["search"];
            search = Library.Lang.Input.Filter(search);
            ViewBag.search = search;
            s.Append(string.IsNullOrEmpty(search) ? "" : " and (DrugsBase_DrugName like '%" + search + "%' or DrugsBase_Manufacturer like '%" + search + "%' or DrugsBase_SimpeCode like '%" + search + "%')");

            //读取列表数据
            SOSOshop.BLL.Db db = new Db();
            string fields = "*";
            DataTable dt = db.GetListByPage("memberTradingIntention", fields, pageSize, pageIndex, " ID desc ", " And UID=" + BaseController.GetUserId(), out recordCount, out pageCount);

            //展示的列表
            ViewBag.List = dt;

            //定义页面
            PagedList<DataRow> pl = new PagedList<DataRow>(dt.Select(), pageIndex, pageSize, recordCount);

            Session["TradingIntentionAdd-Step"] = null;

            return View(pl);
        }
        /// <summary>
        /// 取消交易意向
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public string TradingIntentionCancel()
        {
            string ret = BaseController.Json(-1, "取消失败！");
            int id = 0; int.TryParse(Request["id"], out id);
            if (id > 0)
            {
                SOSOshop.BLL.memberTradingIntention bll = new SOSOshop.BLL.memberTradingIntention();
                if (bll.Amend(id, "State", "0"))
                {
                    ret = BaseController.Json(1, "取消成功！");
                    SOSOshop.BLL.Logs.Log.LogShopAdd("交易意向ID:" + id + " 取消成功", "", BaseController.GetUserId(), User.Identity.Name, 1);//用户操作日志
                }
            }
            return ret;
        }
        /// <summary>
        /// 添加交易意向第一步
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public string TradingIntentionAdd1()
        {
            string ret = BaseController.Json(-1, "操作失败！");
            string DrugsBase_Name = Request["DrugsBase_Name"];
            string guige = Request["guige"];
            string jixing = Request["jixing"];
            string qiye = Request["qiye"];
            string pzwh = Request["pzwh"];
            int jz = 0; int.TryParse(Request["jz"], out jz);
            if (!string.IsNullOrEmpty(DrugsBase_Name) && !string.IsNullOrEmpty(guige))
            {
                SOSOshop.Model.memberTradingIntention model = new SOSOshop.Model.memberTradingIntention();
                model.UID = BaseController.GetUserId();
                model.DrugsBase_Name = DrugsBase_Name;
                model.Guige = guige;
                model.JiXing = jixing;
                model.QiYe = qiye;
                model.pzwh = pzwh;
                model.jz = jz;
                ret = BaseController.Json(1, "操作成功！");
                Session["TradingIntentionAdd-Step"] = 2;
                Session["TradingIntentionAdd-Step1-Ok"] = model;
            }
            return ret;
        }
        /// <summary>
        /// 添加交易意向第二步
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public string TradingIntentionAdd2()
        {
            string ret = BaseController.Json(-1, "操作失败！");
            SOSOshop.Model.memberTradingIntention model = Session["TradingIntentionAdd-Step1-Ok"] as SOSOshop.Model.memberTradingIntention;
            int zq = 0; int.TryParse(Request["zq"], out zq);
            string bz = Request["bz"];
            if (zq > 0 && model != null)
            {
                model.UID = BaseController.GetUserId();
                model.ArrivalPeriod = zq;
                model.Detail = bz;
                model.State = 1;
                SOSOshop.BLL.memberTradingIntention bll = new SOSOshop.BLL.memberTradingIntention();
                int id = bll.Add(model);
                if (id > 0)
                {
                    Session["TradingIntentionAdd-Step2-Ok"] = model;
                    Session["TradingIntentionAdd-Step"] = 3;
                    ret = BaseController.Json(1, "操作成功！");

                    //交易意向提交成功后加积分
                    new SOSOshop.BLL.Integral.MemberIntegral().AddIntegral(BaseController.GetUserId(), 0, SOSOshop.BLL.Integral.MemberIntegralTemplateEnum.提交交易意向, "");

                    SOSOshop.BLL.Logs.Log.LogShopAdd("交易意向ID:" + id + " 添加成功", "", BaseController.GetUserId(), User.Identity.Name, 1);//用户操作日志
                }
                else
                {
                    model.ArrivalPeriod = 0;
                    model.Detail = "";
                    Session["TradingIntentionAdd-Step2-Ok"] = null;
                }
            }
            return ret;
        }

        /// <summary>
        /// 添加交易意向
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult TradingIntentionAdd()
        {
            int Step = Session["TradingIntentionAdd-Step"] == null || Session["TradingIntentionAdd-Step1-Ok"] == null ? 1 : (int)Session["TradingIntentionAdd-Step"];
            bool StepOk = (Step == 1 && Session["TradingIntentionAdd-Step1-Ok"] != null);
            ViewBag.Step = Step;
            ViewBag.StepOk = StepOk;
            ViewBag.UID = BaseController.GetUserId();

            if (Step == 1)
            {

                //return View(pl);
                return View();
            }
            else if (Step == 2)
            {
                SOSOshop.Model.memberTradingIntention model = Session["TradingIntentionAdd-Step1-Ok"] as SOSOshop.Model.memberTradingIntention;
                return View(model);
            }
            else if (Step == 3)
            {
                Session["TradingIntentionAdd-Step"] = null;
                Session["TradingIntentionAdd-Step1-Ok"] = null;
                SOSOshop.Model.memberTradingIntention model = Session["TradingIntentionAdd-Step2-Ok"] as SOSOshop.Model.memberTradingIntention;
                Session["TradingIntentionAdd-Step2-Ok"] = null;
                return View(model);
            }
            return View();
        }
        #endregion
        #region 会员中心 - 采购模板
        /// <summary>
        /// 管理采购模板-下单
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult CartPro()
        {
            int UID = BaseController.GetUserId();
            ViewBag.UID = UID;
            //采购模板选择
            SOSOshop.BLL.Order.OrderProductCart bll = new SOSOshop.BLL.Order.OrderProductCart();
            ViewBag.Carts = bll.GetList(UID);
            //记录总数
            int recordCount = 0;

            //页总数
            int pageCount = 0;

            //页大小
            int pageSize = 500;

            //当前页
            int pageIndex = 1;
            if (!string.IsNullOrEmpty(Request["pageindex"]))
            {
                pageIndex = int.Parse(Request["pageindex"]);
            }

            //采购模板选择的产品
            int id = 0; int.TryParse(Request["id"], out id);
            //读取列表数据
            SOSOshop.BLL.Db db = new Db();
            string fields = "*, ISNULL((select sum(p1.ProNum) from OrderProduct p1 inner join Orders o1 on o1.OrderId=p1.OrderId AND o1.OrderStatus=4 AND (o1.OrderType=1 OR o1.OrderType=2) and T.Product_ID=p1.ProId and o1.ReceiverId=" + UID + "),0) AS ProNumBought ";
            DataTable dt = db.GetListByPage(@"(select distinct d.Product_ID, d.spid, d.DrugsBase_DrugName, 
d.DrugsBase_Specification, d.DrugsBase_Formulation, d.DrugsBase_Manufacturer, d.DrugsBase_ApprovalNumber, d.Goods_ConveRatio, d.Goods_Pcs, d.Goods_Pcs_Small, d.sellType, d.Goods_Unit, 
'' AS is_cl, 0 AS minsell, 0 AS maxsell, '已下架' AS showPrice, 0.00 AS Price, 0 AS Stock, 0 AS iscu, 0 AS is_kong, 0.00 AS price_03, b.ProNum  
from product d inner join OrderProduct p on d.Product_ID=p.ProId inner join Orders o on o.OrderId=p.OrderId and o.OrderStatus=4 and (o.OrderType=1 OR o.OrderType=2)  " +
(id > 0 ?
"inner join OrderProductCartPro b on p.ProId=b.ProId inner join OrderProductCart a on a.CartId=b.CartId and a.State=1 and a.CartId=" + id + ")" :
"and o.ReceiverId=" + UID + " left join OrderProductCartPro b on p.ProId=b.ProId left join OrderProductCart a on a.CartId=b.CartId and a.State=1)"),
                fields, pageSize, pageIndex, " Product_ID desc ", "", out recordCount, out pageCount);

            //展示的列表
            if (dt != null)
            {
                DataTable dtp = db.ExecuteTableForCache("select " + SOSOshop.BLL.Product.Product._PriceTableColumns + "Product_ID,Stock,is_cl,drug_sensitive from product_online_v", DateTime.Now.AddHours(6)).GetPriceTable();
                foreach (DataRow dr in dt.Rows)
                {
                    var drs = dtp.Select("Product_ID=" + dr["Product_ID"]);
                    if (drs != null && drs.Length > 0)
                    {
                        dr["showPrice"] = drs[0]["showPrice"];
                        dr["Price"] = drs[0]["Price"];
                        dr["Stock"] = drs[0]["Stock"];
                        dr["is_cl"] = drs[0]["is_cl"];
                        dr["minsell"] = drs[0]["minsell"];
                        dr["maxsell"] = drs[0]["maxsell"];
                        dr["iscu"] = drs[0]["iscu"];//是否促销
                        dr["price_03"] = 0;//控销价格
                        if ((decimal)drs[0]["price_03"] > 0)
                        {
                            dr["is_kong"] = 1;//是否控销
                            SOSOshop.BLL.JTTX.Price bllPrice = new SOSOshop.BLL.JTTX.Price();
                            var priceModel = bllPrice.GetModel((int)dr["Product_ID"]);

                            dr["price_03"] = drs[0]["price_03"];
                        }
                    }
                }
            }
            else
            {
                dt = new DataTable();
            }
            ViewBag.List = dt;
            //获取账户ViewBag.UID,ViewBag.Member_IsLogOn是否登陆?ViewBag.UserType,ViewBag.Member_Type,ViewBag.Member_Class,ViewBag.MemberPermission权限等
            BaseController.SetAccount(ViewBag);
            //取得会员权限
            int Member_Class;
            Price.GetMemberpermission(out Member_Class);
            ViewBag.Member_Class = Member_Class;

            //定义页面
            PagedList<DataRow> pl = new PagedList<DataRow>(dt.Select(), pageIndex, pageSize, recordCount);

            Session["CartProAdd-Step"] = 1;

            return View(pl);
        }
        /// <summary>
        /// 管理采购模板-添加删除
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult CartProManage()
        {
            int UID = BaseController.GetUserId();
            ViewBag.UID = UID;
            //采购模板选择
            SOSOshop.BLL.Order.OrderProductCart bll = new SOSOshop.BLL.Order.OrderProductCart();
            ViewBag.Carts = bll.GetList(UID);
            //记录总数
            int recordCount = 0;

            //页总数
            int pageCount = 0;

            //页大小
            int pageSize = 500;

            //当前页
            int pageIndex = 1;
            if (!string.IsNullOrEmpty(Request["pageindex"]))
            {
                pageIndex = int.Parse(Request["pageindex"]);
            }

            //采购模板选择的产品
            int id = 0; int.TryParse(Request["id"], out id);
            //读取列表数据
            SOSOshop.BLL.Db db = new Db();
            string fields = "*, ISNULL((select sum(p1.ProNum) from OrderProduct p1 inner join Orders o1 on o1.OrderId=p1.OrderId AND o1.OrderStatus=4 AND (o1.OrderType=1 OR o1.OrderType=2) and T.Product_ID=p1.ProId and o1.ReceiverId=" + UID + "),0) AS ProNumBought ";
            DataTable dt = db.GetListByPage(@"(select distinct d.Product_ID, d.spid, d.DrugsBase_DrugName, 
d.DrugsBase_Specification, d.DrugsBase_Formulation, d.DrugsBase_Manufacturer, d.DrugsBase_ApprovalNumber, d.Goods_ConveRatio, d.Goods_Pcs, d.Goods_Unit, 
'' AS is_cl, 0 AS minsell, 0 AS maxsell, '已下架' AS showPrice, 0 AS Price, 0 AS Stock, b.ProNum  
from product d inner join OrderProduct p on d.Product_ID=p.ProId inner join Orders o on o.OrderId=p.OrderId and o.OrderStatus=4 and (o.OrderType=1 OR o.OrderType=2)  " +
(id > 0 ?
"inner join OrderProductCartPro b on p.ProId=b.ProId inner join OrderProductCart a on a.CartId=b.CartId and a.State=1 and a.CartId=" + id + ")" :
"and o.ReceiverId=" + UID + " left join OrderProductCartPro b on p.ProId=b.ProId left join OrderProductCart a on a.CartId=b.CartId and a.State=1)"),
                fields, pageSize, pageIndex, " Product_ID desc ", "", out recordCount, out pageCount);

            //展示的列表
            if (dt != null)
            {
                DataTable dtp = db.ExecuteTableForCache("select " + SOSOshop.BLL.Product.Product._PriceTableColumns + "Product_ID,Stock,is_cl,drug_sensitive from product_online_v", DateTime.Now.AddHours(6)).GetPriceTable();
                foreach (DataRow dr in dt.Rows)
                {
                    var drs = dtp.Select("Product_ID=" + dr["Product_ID"]);
                    if (drs != null && drs.Length > 0)
                    {
                        dr["showPrice"] = drs[0]["showPrice"];
                        dr["Price"] = drs[0]["Price"];
                        dr["Stock"] = drs[0]["Stock"];
                        dr["is_cl"] = drs[0]["is_cl"];
                        dr["minsell"] = drs[0]["minsell"];
                        dr["maxsell"] = drs[0]["maxsell"];
                    }
                }
            }
            else
            {
                dt = new DataTable();
            }
            ViewBag.List = dt;
            //获取账户ViewBag.UID,ViewBag.Member_IsLogOn是否登陆?ViewBag.UserType,ViewBag.Member_Type,ViewBag.Member_Class,ViewBag.MemberPermission权限等
            BaseController.SetAccount(ViewBag);
            //取得会员权限
            int Member_Class;
            Price.GetMemberpermission(out Member_Class);
            ViewBag.Member_Class = Member_Class;

            //定义页面
            PagedList<DataRow> pl = new PagedList<DataRow>(dt.Select(), pageIndex, pageSize, recordCount);

            Session["CartProAdd-Step"] = null;

            return View(pl);
        }
        /// <summary>
        /// 删除采购模板
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public string CartProCancel()
        {
            string ret = BaseController.Json(-1, "删除失败！");
            int id = 0; int.TryParse(Request["id"], out id);
            if (id > 0)
            {
                SOSOshop.BLL.Order.OrderProductCart bll = new SOSOshop.BLL.Order.OrderProductCart();
                if (bll.Amend(id, "State", "0"))
                {
                    ret = BaseController.Json(1, "删除成功！");
                    SOSOshop.BLL.Logs.Log.LogShopAdd("采购模板ID:" + id + " 删除成功", "", BaseController.GetUserId(), User.Identity.Name, 1);//用户操作日志
                }
            }
            return ret;
        }

        /// <summary>
        /// 添加采购模板
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public string CartProAdd()
        {
            string ret = BaseController.Json(-1, "抱歉，添加失败！");
            int UID = BaseController.GetUserId();
            string pids = Request["pids"];
            string Name = Request["Name"];
            string Description = Request["Description"] + "";
            if (!string.IsNullOrEmpty(pids) && !string.IsNullOrEmpty(Name))
            {
                SOSOshop.BLL.Order.OrderProductCart bll1 = new SOSOshop.BLL.Order.OrderProductCart();
                int CartId = bll1.Add(new SOSOshop.Model.Order.OrderProductCart()
                {
                    UID = UID,
                    Name = Name,
                    Description = Description,
                    State = 1
                });
                if (CartId > 0)
                {
                    SOSOshop.BLL.Order.OrderProductCartPro bll2 = new SOSOshop.BLL.Order.OrderProductCartPro();
                    bll2.Add(CartId, pids.TrimEnd(','));
                    ret = BaseController.Json(CartId, "恭喜，添加成功。");
                }
            }
            return ret;
        }
        #endregion

        /// <summary>
        /// 缺货到货通知
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ViewResult StockOutInfo()
        {
            int recordCount = 0, pageCount = 0, pageSize = 12;
            //当前页
            int pageIndex = 1;
            if (!string.IsNullOrEmpty(Request["pageindex"]))
            {
                pageIndex = int.Parse(Request["pageindex"]);
            }
            // ViewData["filterTime"] = Request.QueryString["fTime"];
            ViewData["search2"] = Request.QueryString["key"];
            List<SelectListItem> list = new List<SelectListItem> {
                new SelectListItem { Text = "全部", Value = "0"},
                new SelectListItem { Text = "最近一月", Value = "1"},
                new SelectListItem { Text = "最近三月", Value = "2"},
                new SelectListItem { Text = "最近半年", Value = "3"},
                new SelectListItem { Text = "最近一年", Value = "4"}
            };
            if (!string.IsNullOrEmpty(Request.QueryString["fTime"]))
            {
                foreach (var item in list)
                {
                    if (item.Value == Request.QueryString["fTime"])
                    {
                        item.Selected = true;
                    }
                }
            }
            ViewData["filterTime"] = list;
            string sqlwhere = " and UID=" + BaseController.GetUserId();
            switch (Request.QueryString["fTime"])
            {
                case "1":
                    {
                        sqlwhere += " and created>'" + DateTime.Now.AddDays(-30) + "'";
                        break;
                    }
                case "2":
                    {
                        sqlwhere += " and created>'" + DateTime.Now.AddMonths(-3) + "'";
                        break;
                    }
                case "3":
                    {
                        sqlwhere += " and created>'" + DateTime.Now.AddMonths(-6) + "'";
                        break;
                    }
                case "4":
                    {
                        sqlwhere += " and created>'" + DateTime.Now.AddMonths(-12) + "'";
                        break;
                    }
            }
            if (!string.IsNullOrEmpty(Request.QueryString["key"]))
            {
                sqlwhere += " and Product_Name like('%" + Library.Lang.Input.Filter(Request.QueryString["key"]).Trim() + "%')";
            }
            SOSOshop.BLL.Stockout bll = new Stockout();
            DataTable dt = bll.GetListByPage("View_Stockout", "*", pageSize, pageIndex, "created desc", sqlwhere, out recordCount, out pageCount).GetSwitchPriceTable();
            PagedList<DataRow> pl = new PagedList<DataRow>(dt.Select(), pageIndex, pageSize, recordCount);
            string sql = "SELECT " + SOSOshop.BLL.Product.Product._PriceTableColumns + "drug_sensitive,is_cl,DrugsBase_Specification,Goods_ConveRatio_Unit_Name,Goods_ConveRatio_Unit,Goods_ConveRatio,Goods_Unit,Product_ID,Product_Name,DrugsBase_Manufacturer,Product_Advertisement,ggy1,Image FROM product_online_v WHERE Product_ID IN(30,69,279,181)";
            ViewBag.最新推荐 = bll.ExecuteTableForCache(sql).GetSwitchPriceTable();
            return View(pl);
        }
        /// <summary>
        /// 缺货到货通知的ajax操作
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public string StockOutInfoAjax(string id, string action, string count)
        {

            SOSOshop.BLL.Stockout bll = new Stockout();
            switch (action)
            {
                case "addcart"://批量添加到购物车
                    {
                        Cart cart = new Cart();
                        string[] ids = Library.Lang.Input.Filter(id.Trim(',')).Split(',');
                        string[] counts = Library.Lang.Input.Filter(count.Trim(',')).Split(',');
                        for (int i = 0; i < ids.Length; i++)
                        {
                            cart.AddCart(ids[i], int.Parse(counts[i]), BaseController.GetUserId().ToString(), "0");
                        }
                        break;
                    }
                case "del"://删除货到提醒
                    {
                        bll.Delete(int.Parse(Library.Lang.Input.Filter(id)), BaseController.GetUserId());
                        break;
                    }
                case "cancelall"://清空货到提醒
                    {
                        bll.DeleteAll(BaseController.GetUserId());
                        break;
                    }

            }
            return "";
        }

        public ActionResult EmailChecked()
        {
            int uid = 0; int.TryParse(Request["uid"], out uid);
            if (uid > 0)
            {
                SOSOshop.BLL.MemberAccount bll = new SOSOshop.BLL.MemberAccount();
                ViewBag.MemberAccount = bll.GetModel(uid);
                return View();
            }
            else
            {
                return View("Index");
            }
        }
        /// <summary>
        /// 积分兑换
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Integral()
        {
            int recordCount = 0, pageCount = 0, pageSize = 12;
            //当前页
            int pageIndex = 1;
            if (!string.IsNullOrEmpty(Request["pageindex"]))
            {
                pageIndex = int.Parse(Request["pageindex"]);
            }
            SOSOshop.BLL.Integral.MemberIntegralGift bll = new SOSOshop.BLL.Integral.MemberIntegralGift();
            string sql = string.Format("select CompanyClass FROM dbo.memberaccount WHERE UID={0}", Public.GetUserId());
            var mclass = SOSOshop.Model.CompanyClass.GetModel((string)bll.ExecuteScalar(sql));
            if (mclass.Price == "Price_01")
            {
                sql = " and State=1 AND Member_Class LIKE('%,0,%')";
            }
            else
            {
                sql = " and State=1 AND Member_Class LIKE('%,1,%')";
            }

            DataTable dt = bll.GetListByPage("MemberIntegralGift", "*", pageSize, pageIndex, "Integral desc", sql, out recordCount, out pageCount);
            PagedList<DataRow> pl = new PagedList<DataRow>(dt.Select(), pageIndex, pageSize, recordCount);
            return View(pl);
        }
        /// <summary>
        /// 积分兑换成交页面
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IntegralExchange()
        {
            int id = int.Parse(Request.Form["id"]);
            int Number = int.Parse(Request.Form["Number"]);
            SOSOshop.BLL.Integral.MemberIntegralGift bllgif = new SOSOshop.BLL.Integral.MemberIntegralGift();
            var model = bllgif.GetModel(id);
            ViewBag.Count = (int)(Number * model.Integral); //共需要花多少积分
            ViewBag.name = model.name;
            //常用收货地址
            string sql = "SELECT username,mobile,phone,address,(SELECT Name FROM dbo.Region WHERE ID=a.province)province,(SELECT Name FROM dbo.Region WHERE ID=a.city)city,(SELECT Name FROM dbo.Region WHERE ID=a.borough)borough FROM dbo.memberreceaddress a WHERE uid=" + BaseController.GetUserId();
            ViewBag.address = bllgif.ExecuteTable(sql).Rows;
            return View();
        }
        /// <summary>
        /// 积分交换提交
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public string IntegralExchangeAdd(FormCollection coll)
        {
            string id = Library.Lang.Input.Filter(coll["id"]);
            string Number = Library.Lang.Input.Filter(coll["Number"]);
            string ConsigneeName = Library.Lang.Input.Filter(coll["ConsigneeName"]);
            string province = Library.Lang.Input.Filter(coll["province1"]);
            string city = Library.Lang.Input.Filter(coll["city1"]);
            string county = Library.Lang.Input.Filter(coll["county1"]);
            string ConsigneeAddress = province + city + county + Library.Lang.Input.Filter(coll["ConsigneeAddress"]);
            string ConsigneePhone = Library.Lang.Input.Filter(coll["ConsigneePhone"]);
            SOSOshop.BLL.Integral.MemberIntegralGift bll = new SOSOshop.BLL.Integral.MemberIntegralGift();
            var gift = bll.GetModel(int.Parse(id));
            if (gift.Number < int.Parse(Number))
            {
                return "礼品数量不足!";
            }
            int RealityIntegral = new SOSOshop.BLL.Integral.MemberIntegral().GetRealityIntegral(BaseController.GetUserId());
            int Integral = (int)(gift.Integral * int.Parse(Number));
            if (RealityIntegral < Integral)
            {
                return "积分不足!";
            }
            //扣除礼品数量
            System.Text.StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE dbo.MemberIntegralGift SET Number=Number-{0} WHERE id={1};", Number, id);
            //扣除用户可用积分数量
            sb.AppendFormat("UPDATE dbo.MemberIntegral SET realityIntegral=realityIntegral-{0} WHERE uid={1};", Integral, (BaseController.GetUserId()));
            //写入兑换记录
            sb.AppendFormat(@"SELECT * FROM dbo.MemberIntegralGiftExchange

INSERT INTO dbo.MemberIntegralGiftExchange
        ( uid ,
          Gift_ID ,
          Gift_Number ,
          State ,
          ontime ,
          ConsigneeAddress ,
          ConsigneeName ,
          ConsigneePhone ,
          Editer
        )
VALUES  ( {0} , -- uid - int
          {1} , -- Gift_ID - int
          {2} , -- Gift_Number - decimal
          1, -- State - int
          '{3}' , -- ontime - datetime
          N'{4}' , -- ConsigneeAddress - nvarchar(500)
          N'{5}' , -- ConsigneeName - nvarchar(50)
          N'{6}' , -- ConsigneePhone - nvarchar(50)
          0  -- Editer - int
        );", BaseController.GetUserId(), gift.id, Number, DateTime.Now, ConsigneeAddress, ConsigneeName, ConsigneePhone);
            //写入积分历史明细
            sb.AppendFormat(@"INSERT INTO dbo.MemberIntegralDetail
        ( uid ,
          integral ,
          remarks ,
          action ,
          created
        )
VALUES  ( {0} , -- uid - int
          {1} , -- integral - int
          N'{2}' , -- remarks - nvarchar(50)
          N'{3}' , -- action - nchar(5)
          getdate()  -- created - datetime
        )", BaseController.GetUserId(), -Integral, string.Format("兑换了{0}个{1}", Number, gift.name), "兑换");
            using (DbConnection conn = bll._db.CreateConnection())
            {
                conn.Open();
                DbTransaction tran = conn.BeginTransaction();
                try
                {
                    bll._db.ExecuteNonQuery(bll._db.GetSqlStringCommand(sb.ToString()), tran);
                    tran.Commit();
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    throw e;
                }
            }
            return "您的礼品兑换已经申请成功，我们工作人员将尽快为您处理!";
        }

        /// <summary>
        /// 积分兑换历史
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult IntegralHistory()
        {
            int recordCount = 0, pageCount = 0, pageSize = 12;
            //当前页
            int pageIndex = 1;
            if (!string.IsNullOrEmpty(Request["pageindex"]))
            {
                pageIndex = int.Parse(Request["pageindex"]);
            }
            SOSOshop.BLL.Integral.MemberIntegralGift bll = new SOSOshop.BLL.Integral.MemberIntegralGift();
            string sql = " and uid=" + BaseController.GetUserId();
            DataTable dt = bll.GetListByPage("MemberIntegralGiftExchange", "(SELECT name FROM dbo.MemberIntegralGift WHERE id=Gift_ID)gname,*", pageSize, pageIndex, "id desc", sql, out recordCount, out pageCount);
            PagedList<DataRow> pl = new PagedList<DataRow>(dt.Select(), pageIndex, pageSize, recordCount);
            return View(pl);
        }
        /// <summary>
        /// 会员取得积分的明细
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Integraldetail()
        {
            int recordCount = 0, pageCount = 0, pageSize = 12;
            //当前页
            int pageIndex = 1;
            if (!string.IsNullOrEmpty(Request["pageindex"]))
            {
                pageIndex = int.Parse(Request["pageindex"]);
            }
            SOSOshop.BLL.Integral.MemberIntegralGift bll = new SOSOshop.BLL.Integral.MemberIntegralGift();
            string sql = " and uid=" + BaseController.GetUserId();
            DataTable dt = bll.GetListByPage("MemberIntegralDetail", "*", pageSize, pageIndex, "created desc", sql, out recordCount, out pageCount);
            PagedList<DataRow> pl = new PagedList<DataRow>(dt.Select(), pageIndex, pageSize, recordCount);
            return View(pl);
        }

        #region 会员中心 - 药检报告
        /// <summary>
        /// 下载药检报告
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult InspectionReport()
        {
            int recordCount = 0, pageCount = 0, pageSize = 12;

            //当前页
            int pageIndex = 1;
            if (!string.IsNullOrEmpty(Request["pageindex"]))
            {
                pageIndex = int.Parse(Request["pageindex"]);
            }
            if (!string.IsNullOrEmpty(Request["pagesize"]))
            {
                pageSize = int.Parse(Request["pagesize"]);
            }

            string table = "", query = "";
            InspectionReportQuery(ref table, ref query, "distinct x.Product_ID,x.UID,x.Product_Name,x.DrugsBase_Manufacturer,x.GuiGe,x.JianZhuang,y.pihao");

            DataTable dt = db.GetListByPage(table, "*", pageSize, pageIndex, "Product_Name asc", query, out recordCount, out pageCount);
            PagedList<DataRow> pl = new PagedList<DataRow>(dt.Select(), pageIndex, pageSize, recordCount);
            ViewBag.recordCount = recordCount;
            return View(pl);
        }
        /// <summary>
        /// 下载药检报告
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult InspectionReport_Orders()
        {
            int recordCount = 0, pageCount = 0, pageSize = 6;

            //当前页
            int pageIndex = 1;
            if (!string.IsNullOrEmpty(Request["pageindex"]))
            {
                pageIndex = int.Parse(Request["pageindex"]);
            }
            if (!string.IsNullOrEmpty(Request["pagesize"]))
            {
                pageSize = int.Parse(Request["pagesize"]);
            }

            SOSOshop.BLL.Report.Qualification bll = new SOSOshop.BLL.Report.Qualification();
            var ids = bll.GetProducts_Id();
            if (ids == "")
            {
                ids = "-1";
            }
            string query = " and Product_ID IN (SELECT ProId FROM dbo.OrderProduct WHERE Status>6) and Product_ID IN(" + ids + ")";
            //string query = " and Product_ID IN(" + bll.GetProducts_Id() + ")";
            if (Request.Form["OrderId"] != "" && Request.Form["OrderId"] != "订单编号")
            {
                query += string.Format(" and Product_ID IN (SELECT ProId FROM dbo.OrderProduct where orderid='{0}')", Library.Lang.Input.Filter(Request.Form["OrderId"]));
            }
            string table = "Product";

            string fields = "*";
            DataTable dt = db.GetListByPage(table, fields, pageSize, pageIndex, "Product_ID desc", query, out recordCount, out pageCount);
            PagedList<DataRow> pl = new PagedList<DataRow>(dt.Select(), pageIndex, pageSize, recordCount);
            ViewBag.recordCount = recordCount;
            return View(pl);
        }
        /// <summary>
        /// 质检报告下载新增不按成交品种下载
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult InspectionReport_All()
        {
            int recordCount = 0, pageCount = 0, pageSize = 6;

            //当前页
            int pageIndex = 1;
            if (!string.IsNullOrEmpty(Request["pageindex"]))
            {
                pageIndex = int.Parse(Request["pageindex"]);
            }
            if (!string.IsNullOrEmpty(Request["pagesize"]))
            {
                pageSize = int.Parse(Request["pagesize"]);
            }

            SOSOshop.BLL.Report.Qualification bll = new SOSOshop.BLL.Report.Qualification();
            var ids = bll.GetProducts_Id();
            if (ids == "")
            {
                ids = "-1";
            }
            string query = " and Product_ID IN(" + ids + ")";
            //string query = " and Product_ID IN(" + bll.GetProducts_Id() + ")";
            if (Request.Form["seachCondition"] != "")
            {
                query += string.Format(" and Product_Name like '%{0}%'", Library.Lang.Input.Filter(Request.Form["seachCondition"]));
            }
            string table = "Product";

            string fields = "*";
            DataTable dt = db.GetListByPage(table, fields, pageSize, pageIndex, "Product_ID desc", query, out recordCount, out pageCount);
            PagedList<DataRow> pl = new PagedList<DataRow>(dt.Select(), pageIndex, pageSize, recordCount);
            ViewBag.recordCount = recordCount;
            ViewBag.seachCondition = Request.Form["seachCondition"];
            return View(pl);
        }

        /// <summary>
        /// 是否可以下载药检报告
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public string InspectionReport_ExistsDownload()
        {
            SOSOshop.BLL.Report.DrugTestingReport report = new SOSOshop.BLL.Report.DrugTestingReport();
            int UID = BaseController.GetUserId();//账户ID
            StringBuilder sb = new StringBuilder();

            //选择商品下载
            string id = Request.Form["id"];
            string oid = Request.Form["oid"];
            if (!string.IsNullOrEmpty(id))
            {
                foreach (string s in id.Split(','))
                {
                    string[] ids = s.Split('-');
                    if (ids.Length > 1)
                    {
                        string pid = ids[0], pihao = ids[1];
                        List<SOSOshop.BLL.Report.DrugTestingReport> list = report.GetList(int.Parse(pid), pihao);
                        if (list.Count > 0 && !string.IsNullOrEmpty(list[0].file))
                        {
                            sb.Append(list[0].id + "-" + list[0].file + "$");
                        }
                        else
                        {
                            sb.Append("0$");
                        }
                    }
                }
            }
            //选择订单下载
            else if (!string.IsNullOrEmpty(oid))
            {
                foreach (string s in oid.Split(','))
                {
                    string[] ids = s.Substring(s.IndexOf('$') + 1).Split(';');
                    List<int> pids = new List<int>(); List<string> pihaos = new List<string>();
                    foreach (string ss in ids)
                    {
                        string[] _ids = ss.Split('$');
                        int _p = 0; int.TryParse(_ids[0], out _p);
                        if (_p > 0) pids.Add(_p); if (_ids.Length > 1 && _ids[1] != "") pihaos.Add(_ids[1]);
                    }
                    string[] list = report.GetFileList(pids.ToArray(), pihaos.ToArray());
                    string files = string.Join(";", list);
                    if (!string.IsNullOrEmpty(files))
                    {
                        sb.Append(files + "$");
                    }
                    else
                    {
                        sb.Append("0$");
                    }
                }
            }
            else
            {

            }
            return sb.ToString();
        }
        [Authorize]
        [HttpPost]
        public string InspectionReport_TimesDownload()
        {
            SOSOshop.BLL.Report.DrugTestingReport report = new SOSOshop.BLL.Report.DrugTestingReport();
            int UID = BaseController.GetUserId();//账户ID
            StringBuilder sb = new StringBuilder();

            //选择商品下载
            string id = Request.Form["id"];
            string oid = Request.Form["oid"];
            if (!string.IsNullOrEmpty(id))
            {
                foreach (string s in id.Split(','))
                {
                    string[] ids = s.Split('-');
                    if (ids.Length > 1)
                    {
                        sb.Append(SOSOshop.BLL.Report.DrugTestingReportDownloadCount.GetDowCount(ids[0], UID) + "$");
                    }
                    else
                    {
                        sb.Append("0$");
                    }
                }
            }
            //选择订单下载
            else if (!string.IsNullOrEmpty(oid))
            {
                foreach (string s in oid.Split(','))
                {
                    string[] ids = s.Split('$');
                    if (ids.Length > 1)
                    {
                        sb.Append(SOSOshop.BLL.Report.DrugTestingReportDownloadOrder.GetDowCount(ids[0], UID) + "$");
                    }
                    else
                    {
                        sb.Append("0$");
                    }
                }
            }
            else
            {

            }
            return sb.ToString();
        }
        /// <summary>
        /// 下载药检报告压缩包
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult InspectionReport_Download()
        {
            SOSOshop.BLL.Report.DrugTestingReport report = new SOSOshop.BLL.Report.DrugTestingReport();
            SOSOshop.BLL.Report.DrugTestingReportDownloadOrder reportOrder = new SOSOshop.BLL.Report.DrugTestingReportDownloadOrder();
            int UID = BaseController.GetUserId();//账户ID

            List<string> files = new List<string>();
            string act = Request.Form["act"];
            string id = Request.Form["id"];
            List<int> Products_Id = new List<int>(); List<string> pihao = new List<string>(); List<string> OrderIds = new List<string>(); List<string> ExistsOrderIds = new List<string>();
            string table = "", query = "";
            if (Request.HttpMethod.Equals("POST") && !string.IsNullOrEmpty(act) && !string.IsNullOrEmpty(id))
            {
                DataTable dt1 = new DataTable();
                if (id.Equals("-1") || id.Equals("0"))//未下载过, 所有查询结果
                {
                    InspectionReportQuery(ref table, ref query, "x.Product_ID,y.pihao,x.OrderId,x.Product_Name,x.DrugsBase_Manufacturer,x.UID");
                    dt1 = db.ExecuteTableForCache("select Product_ID,pihao,OrderId from " + table + "t where 1=1 " + query, DateTime.Now.AddHours(1));
                    foreach (DataRow dr in dt1.Rows)
                    {
                        Products_Id.Add(int.Parse(dr[0].ToString()));
                        pihao.Add(dr[1].ToString().Trim());
                        OrderIds.Add(dr[2].ToString().Trim());
                    }
                }
                switch (act)
                {
                    //选择订单下载
                    case "orders":
                        if (id.Equals("-1"))//未下载过
                        {
                            int i = 0;
                            string[] sss = report.GetFileListByNotDownload(UID, Products_Id.ToArray(), pihao.ToArray(), false);
                            foreach (string ss in sss)
                            {
                                bool exists = false;
                                foreach (string s in ss.Trim(',').Trim().Split(','))
                                {
                                    if (!s.ToLower().Contains(".jpg")) continue;
                                    files.Add(s);
                                    exists = true;
                                }
                                if (exists && OrderIds.Count > i && !ExistsOrderIds.Contains(OrderIds[i].Substring(0, 12)))
                                {
                                    ExistsOrderIds.Add(OrderIds[i].Substring(0, 12));
                                }
                                i++;
                            }
                        }
                        else if (id.Equals("0"))//所有查询结果
                        {
                            int i = 0;
                            string[] sss = report.GetFileList(Products_Id.ToArray(), pihao.ToArray(), false);
                            foreach (string ss in sss)
                            {
                                bool exists = false;
                                foreach (string s in ss.Trim(',').Trim().Split(','))
                                {
                                    if (!s.ToLower().Contains(".jpg")) continue;
                                    files.Add(s);
                                    exists = true;
                                }
                                if (exists && OrderIds.Count > i && !ExistsOrderIds.Contains(OrderIds[i].Substring(0, 12)))
                                {
                                    ExistsOrderIds.Add(OrderIds[i].Substring(0, 12));
                                }
                                i++;
                            }
                        }
                        else//多选
                        {
                            foreach (string s in id.Split(','))
                            {
                                if (!s.ToLower().Contains(".jpg")) continue;
                                int i = s.IndexOf('$');
                                bool exists = false;
                                foreach (string fs in s.Substring(i + 1).Split(';'))
                                {
                                    string[] f = fs.Split('-');
                                    if (f.Length < 2 || !f[1].ToLower().Contains(".jpg")) continue;
                                    files.Add(f[1]);
                                    exists = true;
                                }
                                if (exists && !ExistsOrderIds.Contains(s.Substring(0, i)))
                                {
                                    ExistsOrderIds.Add(s.Substring(0, i));
                                }
                            }
                        }
                        //记录下载过的订单
                        reportOrder.uid = UID;
                        foreach (string oid in ExistsOrderIds)
                        {
                            reportOrder.OrderId = oid;
                            reportOrder.Insert();
                        }
                        break;
                    //选择商品下载
                    case "pihao":
                        if (id.Equals("-1"))//未下载过
                        {
                            string[] sss = report.GetFileListByNotDownload(UID, Products_Id.ToArray(), pihao.ToArray(), false);
                            foreach (string ss in sss)
                            {
                                foreach (string s in ss.Trim(',').Trim().Split(','))
                                {
                                    if (!s.ToLower().Contains(".jpg")) continue;
                                    files.Add(s);
                                }
                            }
                        }
                        else if (id.Equals("0"))//所有查询结果
                        {
                            string[] sss = report.GetFileList(Products_Id.ToArray(), pihao.ToArray(), false);
                            foreach (string ss in sss)
                            {
                                foreach (string s in ss.Trim(',').Trim().Split(','))
                                {
                                    if (!s.ToLower().Contains(".jpg")) continue;
                                    files.Add(s);
                                }
                            }
                        }
                        else//多选
                        {
                            foreach (string s in id.Split(','))
                            {
                                if (!s.ToLower().Contains(".jpg")) continue;
                                foreach (string fs in s.Split(';'))
                                {
                                    string[] f = fs.Split('-');
                                    if (f.Length > 1 && f[1].ToLower().EndsWith(".jpg")) files.Add(f[1]);
                                }
                            }
                        }
                        break;
                }
            }
            ViewBag.files = string.Join(",", files.ToArray());
            return View();
        }

        /// <summary>
        /// 获取查询批号的sql
        /// </summary>
        void Sql_Table_SelectPihao(ref string sql)
        {
            sql = "from [cr_yppf10].dbo.gxywhz a(nolock) inner join [cr_yppf10].dbo.gxywmx b(nolock) on a.djbh=b.djbh and a.djbh like 'XHC%' inner join [cr_yppf10].dbo.spzl c(nolock) on b.spid=c.spid inner join [cr_yppf10].dbo.ckzl d(nolock) on b.ckid=d.ckid inner join [cr_yppf10].dbo.wldwzl e(nolock) on a.wldwid=e.wldwid inner join [cr_yppf10].dbo.gxkpmx f(nolock) on f.djbh=b.xgdjbh inner join [cr_yppf10].dbo.gxddhz g(nolock) on g.djbh=f.xgdjbh";
        }
        void InspectionReportQuery(ref string table, ref string query, string columns)
        {
            int UID = BaseController.GetUserId();//账户ID
            ViewBag.OrderId = !string.IsNullOrEmpty(Request.QueryString["OrderId"]) ? Request.QueryString["OrderId"] : Request.Form["OrderId"];
            ViewBag.From = !string.IsNullOrEmpty(Request.QueryString["From"]) ? Request.QueryString["From"] : Request.Form["From"];
            ViewBag.To = !string.IsNullOrEmpty(Request.QueryString["To"]) ? Request.QueryString["To"] : Request.Form["To"];
            ViewBag.Product_Name = !string.IsNullOrEmpty(Request.QueryString["Product_Name"]) ? Request.QueryString["Product_Name"] : Request.Form["Product_Name"];
            ViewBag.DrugsBase_Manufacturer = !string.IsNullOrEmpty(Request.QueryString["DrugsBase_Manufacturer"]) ? Request.QueryString["DrugsBase_Manufacturer"] : Request.Form["DrugsBase_Manufacturer"];
            ViewBag.pihao = !string.IsNullOrEmpty(Request.QueryString["pihao"]) ? Request.QueryString["pihao"] : Request.Form["pihao"];

            StringBuilder sqlwhere = new StringBuilder(query);
            StringBuilder sqlwhereOrder = new StringBuilder(query);
            //订单号搜索
            if (!string.IsNullOrEmpty(ViewBag.OrderId) && ViewBag.OrderId != "订单编号")
            {
                sqlwhereOrder.AppendFormat(" and o.OrderId like('%{0}%') ", Library.Lang.Input.Filter(ViewBag.OrderId));
            }
            //下单时间
            if (!Library.Lang.DataValidator.isNULL(ViewBag.From, ViewBag.To))
            {
                sqlwhereOrder.AppendFormat(" and (o.AddTime>'{0}' and o.AddTime<'{1}')", ViewBag.From, DateTime.Parse(ViewBag.To).AddHours(24));
            }

            //商品搜索
            if (!string.IsNullOrEmpty(ViewBag.Product_Name) && ViewBag.Product_Name != "商品名称")
            {
                sqlwhere.AppendFormat(" and Product_Name like('%{0}%')", Library.Lang.Input.Filter(ViewBag.Product_Name));
            }

            //厂家搜索
            if (!string.IsNullOrEmpty(ViewBag.DrugsBase_Manufacturer) && ViewBag.DrugsBase_Manufacturer != "厂家名称")
            {
                sqlwhere.AppendFormat(" and DrugsBase_Manufacturer like('%{0}%')", Library.Lang.Input.Filter(ViewBag.DrugsBase_Manufacturer));
            }

            //批号
            if (!string.IsNullOrEmpty(ViewBag.pihao) && ViewBag.pihao != "批号")
            {
                sqlwhere.AppendFormat(" and pihao='{0}' ", Library.Lang.Input.Filter(ViewBag.pihao));
            }

            //买家
            sqlwhere.AppendFormat(" and UID={0} ", UID);
            //下单
            string xdSql = "select o.OrderId,(select ReceiverId from Orders(nolock) where OrderId=o.OrderId and OrderType<>0 and OrderStatus=4)UID,p.Product_ID,p.spid,p.Product_Name,s.DrugsBase_Manufacturer,s.GuiGe,s.JianZhuang from product p(nolock) inner join orderproduct o(nolock) on p.Product_ID=o.ProId and o.Status<>6 inner join orderproductdetails s(nolock) on o.proid=s.proid and o.orderid=s.orderid";
            //出库
            string ckSql = ""; Sql_Table_SelectPihao(ref ckSql);
            ViewBag.ckSql = ckSql;
            ckSql = "select distinct g.webdjbh,b.spid,b.pihao " + ckSql;
            table = "(select " + columns + " from (" + xdSql + sqlwhereOrder + ")x inner join (" + ckSql + ")y on x.OrderId=y.webdjbh and x.spid=y.spid)";
            query = sqlwhere.ToString();
        }
        void InspectionReportQuery_Orders(ref string table, ref string query, string columns)
        {
            int UID = BaseController.GetUserId();//账户ID
            ViewBag.OrderId = !string.IsNullOrEmpty(Request.QueryString["OrderId"]) ? Request.QueryString["OrderId"] : Request.Form["OrderId"];
            ViewBag.From = !string.IsNullOrEmpty(Request.QueryString["From"]) ? Request.QueryString["From"] : Request.Form["From"];
            ViewBag.To = !string.IsNullOrEmpty(Request.QueryString["To"]) ? Request.QueryString["To"] : Request.Form["To"];

            StringBuilder sqlwhere = new StringBuilder(query);
            //订单号搜索
            if (!string.IsNullOrEmpty(ViewBag.OrderId) && ViewBag.OrderId != "订单编号")
            {
                sqlwhere.AppendFormat(" and OrderId like('%{0}%') ", Library.Lang.Input.Filter(ViewBag.OrderId));
            }
            //下单时间
            if (!Library.Lang.DataValidator.isNULL(ViewBag.From, ViewBag.To))
            {
                sqlwhere.AppendFormat(" and (ShopDate>'{0}' and ShopDate<'{1}')", ViewBag.From, DateTime.Parse(ViewBag.To).AddHours(24));
            }

            //买家
            sqlwhere.AppendFormat(" and ReceiverId={0} ", UID);
            //下单
            string xdSql = "select " + columns + " from Orders o(nolock) where OrderType<>0 and OrderStatus=4 " + sqlwhere;
            table = xdSql;
        }
        #endregion

    }
}
