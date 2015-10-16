using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Memcached.ClientLibrary;
using System.Configuration;
using System.IO;

namespace _101shop.v3
{

    public class MvcApplication : System.Web.HttpApplication
    {
        public MvcApplication()
        {
            this.PreRequestHandlerExecute += MvcApplication_PreRequestHandlerExecute;
        }
        public static string getMD5(string FilePath)
        {
            try
            {
                FileStream get_file = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                System.Security.Cryptography.MD5CryptoServiceProvider get_md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] hash_byte = get_md5.ComputeHash(get_file);
                string resule = System.BitConverter.ToString(hash_byte);
                resule = resule.Replace("-", "");
                return resule;
            }
            catch (Exception e)
            {
                return DateTime.Now.Ticks.ToString();
            }
        }
        private static string _ModifiedNo = null;
        /// <summary>
        /// 取得程序本次更新的惟一标识值(用于css新式，或js浏览器不取最新版本)
        /// </summary>
        /// <returns></returns>
        public static string ModifiedNo()
        {
            if (_ModifiedNo == null)
            {
                _ModifiedNo = getMD5(HttpContext.Current.Server.MapPath("/bin/101shop.v3.dll"));
            }
            return _ModifiedNo;
        }
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
        /// <summary>
        /// 商品搜索页路由限定
        /// </summary>
        public class ListConstraint : IRouteConstraint
        {

            bool IRouteConstraint.Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
            {
                return values["filter"].ToString().Split('-').Length == 8;
            }
        }
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("首页商品搜索页面", "products/{filter}-{pageIndex}.html", new { controller = "List", action = "Index", pageIndex = 1 });
            routes.MapRoute("商品详细页", "{id}.html", new { controller = "Product", action = "Index", id = @"\d" });
            routes.MapRoute("资讯详细页", "article/{id}.html", new { controller = "article", action = "Index", id = @"\d" });
            routes.MapRoute("帮助中心页面", "help/{id}.html", new { controller = "help", action = "Index", id = @"\d" });
            routes.MapRoute("基药", "jy/{id}-{lv2}-{lv3}-{lv4}-{attribute}.html", new { controller = "jy", action = "Index", id = "0", lv2 = "0", lv3 = "0", lv4 = "0", attribute = "0" });
            routes.MapRoute("基药缓存", "jy/Index_Cache/{id}-{lv2}-{lv3}-{lv4}-{attribute}.html", new { controller = "jy", action = "Index_Cache", id = "0", lv2 = "0", lv3 = "0", lv4 = "0", attribute = "0" });
            routes.MapRoute("Mongodb资源", "resource/Image/{id}", new { controller = "resource", action = "Image" });
            routes.MapRoute("药检包", "resource/package/{id}", new { controller = "resource", action = "package" });           
            routes.MapRoute("交易大厅", "g", new { controller = "Home", action = "g", id = UrlParameter.Optional });
            routes.MapRoute("在线支付回显", "Payment/CallbackUrl", new { controller = "Payment", action = "CallbackUrl" });
            routes.MapRoute("在线支付", "Payment/{orderId}", new { controller = "Payment", action = "Index", orderId = UrlParameter.Optional });            
            routes.MapRoute("Sitemap", "Sitemap.xml", new { controller = "Sitemap", action = "Sitemap" });
            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }

        protected void Application_Start()
        {
            MvcHandler.DisableMvcResponseHeader = true;
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            SockIOPool pool = SockIOPool.GetInstance();
            string[] serverlist = ConfigurationManager.AppSettings["ServerList"].Split(',');
            pool.SetServers(serverlist);
            pool.SetWeights(new int[] { 1 });
            pool.InitConnections = 5;
            pool.MinConnections = 5;
            pool.MaxConnections = 280;
            pool.MaxIdle = 1000 * 60 * 60 * 6;
            pool.SocketTimeout = 1000 * 3;
            pool.SocketConnectTimeout = 0;
            pool.SocketTimeout = 3000;
            pool.MaintenanceSleep = 60;
            pool.Failover = true;
            pool.Nagle = false;
            pool.MaxBusy = 1000 * 10;
            pool.Initialize();

            SockIOPool pool2 = SockIOPool.GetInstance("Price_Cache");
            string[] serverlist2 = ConfigurationManager.AppSettings["ServerList2"].Split(',');
            pool2.SetServers(serverlist2);
            pool2.SetWeights(new int[] { 1 });
            pool2.InitConnections = 5;
            pool2.MinConnections = 5;
            pool2.MaxConnections = 280;
            pool2.MaxIdle = 1000 * 60 * 60 * 6;
            pool2.SocketTimeout = 1000 * 3;
            pool2.SocketConnectTimeout = 0;
            pool2.SocketTimeout = 3000;
            pool2.MaintenanceSleep = 60;
            pool2.Failover = true;
            pool2.Nagle = false;
            pool2.MaxBusy = 1000 * 10;
            pool2.Initialize();
        }
        public override string GetVaryByCustomString(HttpContext context, string arg)
        {
            if (HttpContext.Current.Cache[arg] == null) HttpContext.Current.Cache[arg] = DateTime.Now.Ticks.ToString();
            return (string)HttpContext.Current.Cache[arg];
        }
        void Application_Error(object sender, EventArgs e)
        {
            Exception LastError = Server.GetLastError();
            string[] ips = { "192.168.1" };
            if (LastError != null)
            {
                string n = MongoDB.Bson.BsonObjectId.GenerateNewId().ToString();
                HttpException LastError1 = LastError as HttpException;
                if (LastError1 != null)
                {
                    if (LastError1.GetHttpCode() != 404)
                    {
                        SOSOshop.BLL.Logs.Log.LogShopAdd(LastError.Message + "[" + n + "]", LastError.ToString(),
                            _101shop.v3.Controllers.BaseController.GetUserId(),
                            Request.IsAuthenticated ? HttpContext.Current.User.Identity.Name : "", 2);
                    }
                    else
                    {
                        SOSOshop.BLL.Logs.Log.LogShopAdd(LastError.Message + "[" + n + "]", LastError.ToString(),
                               _101shop.v3.Controllers.BaseController.GetUserId(),
                               Request.IsAuthenticated ? HttpContext.Current.User.Identity.Name : "", 3);
                    }
                }
                else
                {
                    SOSOshop.BLL.Logs.Log.LogShopAdd(LastError.Message + "[" + n + "]", LastError.ToString(),
                        _101shop.v3.Controllers.BaseController.GetUserId(),
                        Request.IsAuthenticated ? HttpContext.Current.User.Identity.Name : "", 2);
                }
                bool isTransfer = true;
                foreach (var item in ips)
                {
                    if (Request.UserHostAddress.Contains(item))
                    {
                        isTransfer = false;
                        break;
                    }
                }
                //判断是否执行跳转
                //if (isTransfer)
                //{
                //    HttpContext.Current.Response.Redirect("/error?n=" + n, true);
                //}

            }

        }
        void MvcApplication_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            //HttpContext.Current.Response.Headers.Set("Server", "nginx");

        }
    }
}
namespace System.Runtime.CompilerServices { public class ExtensionAttribute : Attribute { } }