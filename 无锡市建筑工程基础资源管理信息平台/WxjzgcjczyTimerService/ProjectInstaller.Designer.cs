namespace WxjzgcjczyTimerService
{
    partial class ProjectInstaller
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
            this.serviceInstaller3 = new System.ServiceProcess.ServiceInstaller();
            this.serviceInstaller2 = new System.ServiceProcess.ServiceInstaller();
            this.serviceInstaller1 = new System.ServiceProcess.ServiceInstaller();
            // 
            // serviceProcessInstaller1
            // 
            this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessInstaller1.Password = null;
            this.serviceProcessInstaller1.Username = null;
            this.serviceProcessInstaller1.BeforeUninstall += new System.Configuration.Install.InstallEventHandler(this.ProjectInstaller_BeforeUninstall);
            // 
            // serviceInstaller3
            // 
            this.serviceInstaller3.Description = "无锡数据中心定时推送服务，定时往市中心四平台推送数据";
            this.serviceInstaller3.DisplayName = "WxsjzxTimerPushServiceToSzxspt";
            this.serviceInstaller3.ServiceName = "WxsjzxTimerPushServiceToSzxspt";
            this.serviceInstaller3.StartType = System.ServiceProcess.ServiceStartMode.Automatic;

            // 
            // serviceInstaller2
            // 
            this.serviceInstaller2.Description = "无锡数据中心定时推送服务，定时往省一体化平台推送项目各环节的数据";
            this.serviceInstaller2.DisplayName = "WxsjzxTimerPushService";
            this.serviceInstaller2.ServiceName = "WxsjzxTimerPushService";
            this.serviceInstaller2.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // serviceInstaller1
            // 
            this.serviceInstaller1.Description = "定时拉取省省施工许可、一号通建设单位、安监站施工单位、省一体化平台企业和人员等信息，并定时上传外省企业和人员信息";
            this.serviceInstaller1.DisplayName = "WxsjzxTimerService";
            this.serviceInstaller1.ServiceName = "WxsjzxTimerService";
            this.serviceInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstaller1,this.serviceInstaller3,
            this.serviceInstaller2,
            this.serviceInstaller1});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller1;
        private System.ServiceProcess.ServiceInstaller serviceInstaller3;
        private System.ServiceProcess.ServiceInstaller serviceInstaller2;
        private System.ServiceProcess.ServiceInstaller serviceInstaller1;
    }
}