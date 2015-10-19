using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOSOshop.BLL.Common;

namespace _101shop.v3.Controllers
{
    public class ApiController : Controller
    {
        //
        // GET: /Api/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 判断是否登陆
        /// </summary>
        /// <returns>1已经登陆,0未登陆</returns>
        public string IsLogin()
        {
            return User.Identity.IsAuthenticated ? "1" : "0";
        }

        /// <summary>
        /// 判断登录状态 
        /// 
        /// </summary>
        /// <returns>返回用户名，未登录返回空</returns>
        public string UserName()
        {
            return User.Identity.IsAuthenticated ? User.Identity.Name : "";
        }
        /// <summary>
        /// 返回用户名       
        /// </summary>
        /// <returns>返回用户名，未登录返回空</returns>
        public string TrueName()
        {
            if (!User.Identity.IsAuthenticated) return "no";
            try
            {
                return HttpContext.Server.UrlEncode(new SOSOshop.BLL.Db().ExecuteScalarForCache("SELECT TrueName FROM dbo.memberinfo WHERE UID=" + BaseController.GetUserId()).ToString().Trim());
            }
            catch
            {
                return "no";
            }
        }
        /// <summary>
        /// 取得会员js的帐号状态，去购物车结算，和添加购物车用
        /// </summary>
        /// <returns></returns>
        public string member()
        {
            if (User.Identity.IsAuthenticated)
            {
                return "member = { uid: " + Public.GetUserId() + " };";
            }
            return "member = { uid: 0 };";
        }
        
        /// <summary>
        /// 更新商品浏览次
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        public string clickNum(int id)
        {
            if (Request.HttpMethod == "POST")
            {
                SOSOshop.BLL.Db bll = new SOSOshop.BLL.Db();
                bll.ExecuteNonQuery("UPDATE Product SET Product_ClickNum=Product_ClickNum+1 WHERE Product_ID=" + id);
            }            
            return "";
        }

    }
}
