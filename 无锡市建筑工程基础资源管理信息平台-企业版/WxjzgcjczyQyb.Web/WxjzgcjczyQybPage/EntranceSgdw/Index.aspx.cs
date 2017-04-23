using System;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using Bigdesk8;
using Bigdesk8.Business;
using Bigdesk8.Web;
using WxjzgcjczyQyb;

namespace WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Entrance
{
    public partial class Index : BasePage2
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (WorkUser.UserType != UserType.建设单位 && WorkUser.UserType != UserType.施工单位)
                {
                    this.TopWindowLocation(ConfigManager.GetLoginPageUrl());
                    return;
                }
            }
        }
    }
}