using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _101shop.v3.Controllers
{
    public class SigninController : Controller
    {
        //
        // 每日签到
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.js = 0;//是否执行提示js
            ViewBag.info = "";//提交信息文字            
            SOSOshop.BLL.Integral.MemberIntegralLock bll = new SOSOshop.BLL.Integral.MemberIntegralLock();
            bool isAllow = bll.isAllow(BaseController.GetUserId(), SOSOshop.BLL.Integral.MemberIntegralTemplateEnum.每日签到);
            ViewBag.isAllow = isAllow;
            if (isAllow)
            {
                if (Request.HttpMethod == "POST")
                {
                    new SOSOshop.BLL.Integral.MemberIntegral().AddIntegral(BaseController.GetUserId(), 0, SOSOshop.BLL.Integral.MemberIntegralTemplateEnum.每日签到, "");
                    ViewBag.js = 1;
                    ViewBag.isAllow = false;
                    SOSOshop.BLL.DbBase db = new SOSOshop.BLL.DbBase();
                    string sql = "SELECT name,diff FROM (SELECT TOP 1 *,(Integral-(SELECT integral FROM dbo.MemberIntegral WHERE uid=" + BaseController.GetUserId() + "))diff FROM dbo.MemberIntegralGift  WHERE Integral>(SELECT integral FROM dbo.MemberIntegral WHERE uid=" + BaseController.GetUserId() + ") AND State=1)a ORDER BY diff ASC";
                    var dt = db.ExecuteTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        ViewBag.info = string.Format("还差{0:F0}积分即可兑换礼品({1})", dt.Rows[0]["diff"], dt.Rows[0]["name"]);
                    }
                }
            }
            return View();
        }

    }
}
