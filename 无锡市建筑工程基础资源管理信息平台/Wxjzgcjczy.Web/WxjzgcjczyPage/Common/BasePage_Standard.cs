using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Wxjzgcjczy.BLL;
using Bigdesk8.Web;
using Bigdesk8;


namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Common
{
    public class BasePage_Standard :Page
    {
        public AppUser WorkUser = new AppUser();
        private SystemBLL BLL = new SystemBLL();
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            this.Page.Theme = ConfigManager.GetTheme2InUsing();
        }

        protected override void OnPreLoad(EventArgs e)
        {
            base.OnPreLoad(e);
            string loginID = LoginManager.GetLoginID();
            //Session[ConfigManager.GetSignInAppUserSessionName()] = WorkUser;

            if (!string.IsNullOrEmpty(loginID))
            {
                object sessionAppUser = Session[ConfigManager.GetSignInAppUserSessionName()];
                if (!sessionAppUser.IsEmpty())
                {
                    this.WorkUser = (AppUser)sessionAppUser;
                }
                else
                {
                    this.WorkUser = BLL.InitAppUser(loginID).Result;
                    Session[ConfigManager.GetSignInAppUserSessionName()] = WorkUser;
                }
            }
            else
            {
                this.TopWindowLocation(ConfigManager.GetLoginPageUrl());
                return;
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
