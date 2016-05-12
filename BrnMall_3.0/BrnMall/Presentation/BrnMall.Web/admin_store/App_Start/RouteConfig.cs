using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BrnMall.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            
            //默认路由(此路由不能删除)
            routes.MapRoute("default",
                            "{controller}/{action}",
                            new { controller = "home", action = "index" },
                            new[] { "BrnMall.Web.Controllers" });
        }
    }
}