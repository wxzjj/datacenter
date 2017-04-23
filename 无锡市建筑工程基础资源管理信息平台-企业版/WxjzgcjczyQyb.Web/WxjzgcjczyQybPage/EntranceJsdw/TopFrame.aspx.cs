using System;
using System.Configuration;
using WxjzgcjczyQyb;
using System.Web;

namespace WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.EntranceJsdw
{
    public partial class TopFrame : BasePage2
    {
        public string modifyPasswordPageUrl;
        public string theme = string.Empty;
        public string reloginPageUrl = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.reloginPageUrl = ConfigManager.GetReloginPageUrl();
            modifyPasswordPageUrl = ConfigurationManager.AppSettings["ModifyPasswordPageUrl"];
        }
    }
}
