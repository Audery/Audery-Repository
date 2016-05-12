using System;

using System.Web.Mvc;
using System.Web.Routing;
namespace BrnMall.Web.StoreAdminNew
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            ////将默认视图引擎替换为ThemeRazorViewEngine引擎
            //ViewEngines.Engines.Clear();
            //ViewEngines.Engines.Add(new ThemeRazorViewEngine());

            AreaRegistration.RegisterAllAreas();

            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ////启动事件机制
            //BMAEvent.Start();
            ////服务器宕机启动后重置在线用户表
            //if (Environment.TickCount > 0 && Environment.TickCount < 900000)
            //    OnlineUsers.ResetOnlineUserTable();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}