using System;
using Bigdesk8.Web;

namespace Bigdesk8.Business
{
    public partial class BottomFrame : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsUserLogin)
            {
                // 未登录
                this.TopWindowLocation(WebCommon.GetLoginPageUrl());
                return;
            }

            this.a_reloginPageUrl.HRef = WebCommon.GetReloginPageUrl();
            this.a_modifyPasswordPageUrl.HRef = WebCommon.GetModifyPasswordPageUrl();
            this.lbl_zsmc.InnerText = this.UserInfo.UserName;
        }
    }
}