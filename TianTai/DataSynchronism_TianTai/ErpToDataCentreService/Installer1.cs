using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;

namespace ErpToDataCentreService
{
    [RunInstaller(true)]
    public partial class Installer1 : System.Configuration.Install.Installer
    {
        public static string ServiceName = "HLToSHOPService";
        public Installer1()
        {
            InitializeComponent();
            this.serviceInstaller1.Description = ServiceName;
            this.serviceInstaller1.DisplayName = ServiceName;
            this.serviceInstaller1.ServiceName = ServiceName;
        }

        private void serviceInstaller1_Committed(object sender, InstallEventArgs e)
        {
            System.ServiceProcess.ServiceController ser = new System.ServiceProcess.ServiceController(ServiceName);
            if (ser.Status != System.ServiceProcess.ServiceControllerStatus.Running | ser.Status != System.ServiceProcess.ServiceControllerStatus.StartPending)
            {
                ser.Start();
            }
        }
    }
}
