using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;

namespace SOSOshopService
{
    partial class Command : ServiceBase
    {
        private ServiceHost _host = new ServiceHost(typeof(CommandListen));
        public Command()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _host.Open();
        }

        protected override void OnStop()
        {
            _host.Close();
        }
    }
}
