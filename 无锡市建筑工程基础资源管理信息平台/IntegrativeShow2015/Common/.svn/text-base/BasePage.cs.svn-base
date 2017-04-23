using System;
using System.Collections.Generic;
using System.Linq;
using IntegrativeShow2.Common;
using System.Configuration;
using Bigdesk8.Web;
using Bigdesk8;


namespace IntegrativeShow2
{
    public class BasePage : System.Web.UI.Page
    {
        protected AppUser WorkUser;
        private string sessionLoginID;
        private string urlLoginID;
        public string beFrom = string.Empty;  //参数来源
        public SystemBLL BLL = new SystemBLL();

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.Theme = WebCommon.SystemTheme;
        }
    }


    public class BasePageGuanli : BasePage
    {
        protected override void OnPreLoad(EventArgs e)
        {
            base.OnPreLoad(e);


            string loginID =LoginManager.GetLoginID();
            // Session[ConfigManager.GetSignInAppUserSessionName()] = WorkUser;

            if (!string.IsNullOrEmpty(loginID))
            {
                string signInAppUserSessionName = ConfigurationManager.AppSettings["SignInAppUserSessionName"];
                if (string.IsNullOrEmpty(signInAppUserSessionName))
                {
                    this.WindowAlert("SessionKey不存在！");
                    return;
                }
                object sessionAppUser = Session[signInAppUserSessionName];
                if (!sessionAppUser.IsEmpty())
                {
                    this.WorkUser = (AppUser)sessionAppUser;
                }
                else
                {
                    this.WorkUser = BLL.InitAppUser(loginID).Result;
                    Session[signInAppUserSessionName] = WorkUser;
                }
            }
            else
            {
                this.TopWindowLocation(ConfigurationManager.AppSettings["LoginPageUrl"].ToString2());
                return;
            }

            //下面的代码防止A用户打开的页面，B用户登录进来，操作前面A用户打开的页面，造成数据错乱
            //if (!Page.IsPostBack)
            //    this.ViewState.Add("PageOpenedByThisUser", WorkUser.UserID);
            //else
            //    if (WorkUser.UserID != this.ViewState["PageOpenedByThisUser"].ToString())
            //    {
            //        Response.Write("页面用户信息已过期！");
            //        Response.End();
            //    }

        }
    }


}