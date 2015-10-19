using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Memcached.ClientLibrary;
using System.Configuration;

namespace SOSOshopService
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
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

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{   
				new Order(),
                new cr_yppf()                
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
