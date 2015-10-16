namespace DataSynService1
{
    partial class Installer1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.serviceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
            this.AutoSupplyManageserviceInstaller1 = new System.ServiceProcess.ServiceInstaller();
            // 
            // serviceProcessInstaller1
            // 
            this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessInstaller1.Password = null;
            this.serviceProcessInstaller1.Username = null;
            // 
            // AutoSupplyManageserviceInstaller1
            // 
            this.AutoSupplyManageserviceInstaller1.Description = "货源自动管理程序";
            this.AutoSupplyManageserviceInstaller1.DisplayName = "AutoSupplyManage_HL";
            this.AutoSupplyManageserviceInstaller1.ServiceName = "AutoSupplyManage_HL";
            this.AutoSupplyManageserviceInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.AutoSupplyManageserviceInstaller1.Committed += new System.Configuration.Install.InstallEventHandler(this.serviceInstaller1_Committed);
            // 
            // Installer1
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstaller1,
            this.AutoSupplyManageserviceInstaller1});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller1;
        private System.ServiceProcess.ServiceInstaller AutoSupplyManageserviceInstaller1;
    }
}