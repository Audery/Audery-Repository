using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;


namespace DataSynService1
{
    [RunInstaller(true)]
    public partial class Installer1 : System.Configuration.Install.Installer
    {
        public Installer1()
        {
            InitializeComponent();
        }

        private void serviceInstaller1_Committed(object sender, InstallEventArgs e)
        {
            System.ServiceProcess.ServiceController ser = new System.ServiceProcess.ServiceController("AutoSupplyManage_HL");
            if (ser.Status != System.ServiceProcess.ServiceControllerStatus.Running | ser.Status != System.ServiceProcess.ServiceControllerStatus.StartPending)
            {
                ser.Start();
            }
        }
    }
}
