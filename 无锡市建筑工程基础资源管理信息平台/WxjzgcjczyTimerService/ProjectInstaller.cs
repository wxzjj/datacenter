using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;


namespace WxjzgcjczyTimerService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }
        //安装程序时自动启动
        //private void ProjectInstaller_AfterInstall(object sender, InstallEventArgs e)
        //{
        //    try
        //    {
        //        //ServiceController myService1 = new ServiceController("WxsjzxTimerService");
        //        //myService1.Start();
        //        //ServiceController myService = new ServiceController("WxsjzxTimerPushService");
        //        //myService.Start();

        //    }
        //    catch (Exception ee1)
        //    {
        //    }
        //}
        //卸载时自动关闭
        private void ProjectInstaller_BeforeUninstall(object sender, InstallEventArgs e)
        {
            try
            {
                //关闭windows 服务
                ServiceController myService1 = new ServiceController("WxsjzxTimerService");
                ServiceController myService2 = new ServiceController("WxsjzxTimerPushService");
                ServiceController myService3 = new ServiceController("WxsjzxTimerPushServiceToSzxspt");
              
                myService1.Stop();
                myService2.Stop();
                myService3.Stop();

            }
            catch (Exception ee)
            {
            }
        }


    }
}
