using System;
using System.Web;
using System.Web.UI.HtmlControls;
using Bigdesk8;
using Bigdesk8.Business;
using Bigdesk8.Web;
using Wxjzgcjczy.BLL;

namespace Wxjzgcjczy.Web
{
    public class BasePage : System.Web.UI.Page
    {

        public AppUser WorkUser = new AppUser();
        private SystemBLL BLL = new SystemBLL();
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            this.Page.Theme = ConfigManager.GetThemeInUsing();

        }

        protected override void OnPreLoad(EventArgs e)
        {
            base.OnPreLoad(e);
            if (Request.Url.AbsolutePath.IndexOf("Index.aspx") >= 0)
            {
                if (!string.IsNullOrEmpty(WorkUser.qyID))
                    Response.Redirect("/WxjzgcjczyPage/MainPage/Index2.aspx");
            }

            string loginID =  LoginManager.GetLoginID();
   
            //Session[ConfigManager.GetSignInAppUserSessionName()] = WorkUser;

            if (!string.IsNullOrEmpty(loginID))
            {
                //object sessionAppUser = Session[ConfigManager.GetSignInAppUserSessionName()];
                //if (!sessionAppUser.IsEmpty())
                //{
                //    this.WorkUser = (AppUser)sessionAppUser;
                //}
                //else
                //{

                FunctionResult<AppUser> result = BLL.InitAppUser(loginID);
                if (result.Status != FunctionResultStatus.Error)
                {
                    this.WorkUser = result.Result;
                    Session[ConfigManager.GetSignInAppUserSessionName()] = result.Result;
                }
                else
                {
                    Response.Clear();
                    Response.Write("您在本系统里没有权限！");
                    Response.Flush();
                    Response.End();
                }

                //}
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
