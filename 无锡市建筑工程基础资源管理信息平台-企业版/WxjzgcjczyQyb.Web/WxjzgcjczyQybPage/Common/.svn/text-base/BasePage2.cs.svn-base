using System;
using Bigdesk8;
using Bigdesk8.Business;
using Bigdesk8.Web;
using WxjzgcjczyQyb;
using WxjzgcjczyQyb.BLL;
using System.Web;

namespace WxjzgcjczyQyb.Web
{
    public class BasePage2 : System.Web.UI.Page
    {
        public AppUser WorkUser = new AppUser();
        private readonly SundriesBFP BFPInstance = new SundriesBFP();
        private string themeName = string.Empty;
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.Page.Theme = ConfigManager.GetThemeInUsingB();

        }

        protected override void OnPreLoad(EventArgs e)
        {
            base.OnPreLoad(e);

            string loginID = LoginManager.GetLoginID();

            if (string.IsNullOrEmpty(loginID))
            {
                this.TopWindowLocation(ConfigManager.GetLoginPageUrl());
                return;
            }
            else
            {
                AppUser appUser = BFPInstance.InitAppUser(loginID.ToInt32()).Result;
                this.WorkUser = appUser;
                Session[ConfigManager.GetSignInAppUserSessionName()] = WorkUser;
            }



            //下面的代码防止A用户打开的页面，B用户登录进来，操作前面A用户打开的页面，造成数据错乱
            if (!Page.IsPostBack)
                this.ViewState.Add("PageOpenedByThisUser", WorkUser.UserID);
            else
                if (WorkUser.UserID != this.ViewState["PageOpenedByThisUser"].ToString())
                {
                    Response.Write("页面用户信息已过期！");
                    Response.End();
                }
        }
    }
}
