using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.EntranceGlyh
{
    public partial class BottomFrame : BasePage2
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.lb_loginname.Text = this.WorkUser.LoginName.ToString();
                this.lb_userrealname.Text = this.WorkUser.UserName.ToString();
            }
        }
    }
}
