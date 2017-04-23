using System;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using Bigdesk8;
using Bigdesk8.Business;
using Bigdesk8.Web;
using WxjzgcjczyQyb;


namespace WxjzgcjczyQyb.Web.WxjzgcjczyQybPage
{
    /// <summary>
    /// 系统主页面，负责补充完善登录用户的信息，并将用户引导到合适的功能集.
    /// </summary>
    public partial class Index : BasePage2
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                switch (WorkUser.UserType)
                {
                    //case UserType.建设单位:
                    //    this.Response.Redirect("EntranceJsdw/Index.aspx");
                    //    break;
                    //case UserType.施工单位:
                    //    this.Response.Redirect("EntranceSgdw/Index.aspx");
                    //    break;
                    case UserType.管理用户:
                        if (this.WorkUser.LoginName == "admin")
                        {
                            this.Response.Redirect("EntranceGlyh/Index.aspx");
                        }
                        else
                        {
                            this.Response.Write("<script>alert('非法用户，禁止登录！');location='" + ConfigManager.GetLoginPageUrl() + "';</script>");
                        }

                        break;
                    default:
                        this.Response.Write("<script>alert('非法用户，禁止登录！');location='" + ConfigManager.GetLoginPageUrl() + "';</script>");
                        break;
                }

            }
        }
    }
}