using System;
using Bigdesk8.Business.UserRightManager;

namespace Bigdesk8.Business
{
    public class BasePage : System.Web.UI.Page
    {
        public const int 系统管理员特性编号 = 1;

        /// <summary>
        /// 用户信息
        /// </summary>
        public IUserRightInfo UserInfo
        {
            get;
            private set;
        }

        /// <summary>
        /// 用户是否登录
        /// </summary>
        public bool IsUserLogin
        {
            get
            {
                return this.UserInfo.Exists && !this.UserInfo.IsGuest;
            }
        }

        protected override void OnPreLoad(EventArgs e)
        {
            base.OnPreLoad(e);

            this.UserInfo = new UserRightInfo(WebCommon.GetLoginNameFromSession());
        }
    }

    public class SystemBasePage : BasePage
    {
        protected override void OnPreLoad(EventArgs e)
        {
            base.OnPreLoad(e);

            if (!this.UserInfo.HasAttribute(系统管理员特性编号))
            {
                this.ResponseRedirect(UserInfo.UserName + "(" + UserInfo.LoginName + ")" + "，您不是系统管理员，没有 管理中心 权限!");
                return;
            }
        }
    }
}
