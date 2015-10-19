using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;

namespace _101shop.v3.Controllers
{
    /// <summary>
    ///必须登陆的手机过滤器
    /// </summary>
    public class AuthorizeMobileAttribute : ActionFilterAttribute
    {

        /// <summary>
        /// Action还没被执行之前
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAuthenticated)
            {
                Hashtable ht = new Hashtable();
                filterContext.HttpContext.Response.Write("-100");//没有登陆，返回-100
                filterContext.HttpContext.Response.End();
            }

        }
    }
}