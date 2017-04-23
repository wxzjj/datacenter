using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bigdesk8.Web;
using Wxjzgcjczy.BLL;
using Bigdesk8;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage
{
    public partial class UserAuthorization : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string loginName = Request.QueryString["loginid"];//获取用户帐号
                SystemBLL BLL = new SystemBLL();

                if (!string.IsNullOrEmpty(loginName))
                {
                    FunctionResult<AppUser> result = BLL.InitAppUserByLoginName(loginName);
                    if (result.Status != FunctionResultStatus.Error)
                    {
                        Session[ConfigManager.GetSignInAppUserSessionName()] = result.Result;
                        LoginManager.SetLoginID(result.Result.UserID);

                        Response.Redirect("MainPage/Index.aspx");
                    }
                    else
                    {
                        Response.Clear();
                        Response.Write("您在本系统里没有权限！");
                        Response.Flush();
                        Response.End();
                    }
                }
                else
                {
                    Response.Clear();
                    Response.Write("非法访问，请从局OA系统登录！");
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
