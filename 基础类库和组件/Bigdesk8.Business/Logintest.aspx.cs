using System;
using Bigdesk8;

namespace Bigdesk8.Business
{
    public partial class Logintest : System.Web.UI.Page
    {
        protected void Button1_Click(object sender, EventArgs e)
        {
            string loginName = this.TextBox1.Text.TrimString();

            if (loginName.IsEmpty())
            {
                this.Label1.Text = "请输入用户名";
                return;
            }

            Bigdesk8.Business.UserRightManager.UserRightManager um = new Bigdesk8.Business.UserRightManager.UserRightManager();
            if (!um.UserExists(loginName))
            {
                this.Label1.Text = "用户名不存在";
                return;
            }
            else
            {
                Session[WebCommon.GetLoginSessionName()] = loginName;
                this.Response.Redirect("Index.aspx");
                return;
            }
        }
    }
}
