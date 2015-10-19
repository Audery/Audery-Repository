using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;


namespace ErpShopService
{
    [RunInstaller(true)]
    public partial class Installer1 : System.Configuration.Install.Installer
    {
        public Installer1()
        {
            InitializeComponent();
            serviceInstaller1.Committed += new InstallEventHandler(serviceInstaller1_Committed);
        }
        void serviceInstaller1_Committed(object sender, InstallEventArgs e)
        {
            System.ServiceProcess.ServiceController ser = new System.ServiceProcess.ServiceController("RJShopService");
            if (ser.Status != System.ServiceProcess.ServiceControllerStatus.Running | ser.Status != System.ServiceProcess.ServiceControllerStatus.StartPending)
            {
                ser.Start();
            }
        }
    }
}
