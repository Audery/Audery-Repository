using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using DSWebService.BLL;
using Memcached.ClientLibrary;
using System.Configuration;

namespace DSWebService
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
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
            Exception LastError = Server.GetLastError();
            string error = LastError.ToString();
            string errorCode = MongoDB.Oid.NewOid().ToString();
            Log bllLog = new Log()
            {
                created = DateTime.Now,
                describe = LastError.Message + errorCode,
                ip = Request.UserHostAddress,
                // source = string.Format("{0}:{1}", LastError.ToString(), this.Request.Url.ToString()),
                source = this.Request.Url.ToString(),
                type = 500,
                userid = 0,
                username = "",
                detail = error
            };
            bllLog.insert(bllLog);
            //HttpContext.Current.Response.End();
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}