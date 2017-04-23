using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wxjzgcjczy.BLL;
using Bigdesk8;
using System.Web.Services;
using System.Web.SessionState;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Yhgl
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Delete : IHttpHandler, IRequiresSessionState
    {

        public YhglBLL BLL;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string message = "", type = "";
            string operate = context.Request.QueryString["operate"];
            object sessionAppUser = context.Session[ConfigManager.GetSignInAppUserSessionName()];
            BLL = new YhglBLL((AppUser)sessionAppUser);
            FunctionResult<string> result = new FunctionResult<string>() { Status = FunctionResultStatus.Error };
            switch (operate.ToLower())
            {
                case "user":
                    string userID = context.Request.QueryString["UserID"];
                    result = BLL.DeleteUser(userID);
                    break;
                case "role":
                    string roleID = context.Request.QueryString["roleID"];
                    result = BLL.DeleteRole(roleID);
                    break;

            }
            if (result.Status != FunctionResultStatus.Error)
            {
                type = "Success";
            }
            else
            {
                type = "Error";
                message = result.Message.Message;
            }
            string json = @"{""Type"":""" + type + @""",""Message"":""" + message + @"""}";
            context.Response.Write(json);
            context.Response.End();
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
