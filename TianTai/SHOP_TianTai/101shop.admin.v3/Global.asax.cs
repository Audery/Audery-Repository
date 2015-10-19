using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Memcached.ClientLibrary;
using System.Configuration;

namespace _101shop.admin.v3
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

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
        protected void Application_Start()
        { 
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
        }
    }
}