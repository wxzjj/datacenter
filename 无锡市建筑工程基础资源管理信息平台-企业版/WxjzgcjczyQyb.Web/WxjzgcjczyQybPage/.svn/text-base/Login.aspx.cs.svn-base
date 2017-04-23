using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bigdesk8;
using Bigdesk8.Business;
using Bigdesk8.Web;
using WxjzgcjczyQyb.BLL;

namespace WxjzgcjczyQyb.Web.WxjzgcjczyQybPage
{
    public partial class Login : System.Web.UI.Page
    {
        protected AppUser WorkUser = new AppUser();
        private readonly SundriesBFP BFPInstance = new SundriesBFP();
        protected string op = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                op = Request.QueryString["exit"].ToString2();
                if (op == "exit") { Session.RemoveAll(); }
                object sessionUserID = System.Web.HttpContext.Current.Session["UserID"];
                if ((sessionUserID != null) || (sessionUserID.ToString2().Trim() != string.Empty))
                {
                    int loginid = Convert.ToInt32(sessionUserID);
                    WorkUser = BFPInstance.InitAppUser(loginid).Result;
                    Session[ConfigManager.GetSignInAppUserSessionName()] = WorkUser;
                    if (WorkUser.UserType == UserType.管理用户)
                    {
                        Response.Redirect("Index.aspx");
                    }
                }
            }
        }

        protected void login_Click(object sender, EventArgs e)
        {
            ValidateLogin();
        }

        private void ValidateLogin()
        {
            string loginName = string.Empty;
            loginName = this.txtUsername.Text.Trim();
            FunctionResult<AppUser> fr = BFPInstance.InitAppUser(loginName, this.txtPassword.Text.Trim().ToString());
            Session[ConfigManager.GetSignInAppUserSessionName()] = fr.Result;

            if (fr.Message.ToString2() != string.Empty)
            {
                this.WindowAlert(fr.Message.ToString());
                return;
            }
            else
            {
                string url = string.Empty;
                if (!string.IsNullOrEmpty(Request.QueryString["url"]))
                    url = Request.QueryString["url"];
                else
                    url = "Index.aspx";

                LoginManager.SetLoginID(fr.Result.UserID);

                Response.Write("<script>alert('" + "欢迎您：" + fr.Result.UserName + "，您已经登录成功！" + "');location='" + url + "';</script>");
            }
        }

    }
}