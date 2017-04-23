using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.MainPage
{
    public partial class Index : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(WorkUser.qyID))
                Response.Redirect("Index2.aspx");
            if (!this.IsPostBack)
            {
                this.lblLoginTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                this.lblUserName.Text = this.WorkUser.UserName;
                
           }
        }
    }
}
