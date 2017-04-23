using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wxjzgcjczy.BLL;
using System.Web.Services;
using Bigdesk8;
using System.Web.SessionState;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Xxcj
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Delete : IHttpHandler,IRequiresSessionState
    {

       public XmxxBLL BLL;
       public void ProcessRequest(HttpContext context)
       {
           context.Response.ContentType = "text/plain";
           string message = "", type = "";
           string operate = context.Request.QueryString["operate"];
           object sessionAppUser = context.Session[ConfigManager.GetSignInAppUserSessionName()];
           BLL = new XmxxBLL((AppUser)sessionAppUser);
           FunctionResult<string> result = new FunctionResult<string>() { Status = FunctionResultStatus.Error };

           string pkid = context.Request.QueryString["pkid"];

           switch (operate.ToLower())
           {
               case "ajxx":
                   result = BLL.Delete_aj_gcjbxx(pkid);
                   break;
               case "zjxx":
                   result = BLL.Delete_zj_gcjbxx(pkid);
                   break;

               case "ajxx_ryxx":
                   result = BLL.Delete_TBProjectBuilderUserInfo(pkid);
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
