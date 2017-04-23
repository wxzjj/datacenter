using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bigdesk8;
using Wxjzgcjczy.BLL;
using System.Data;
using System.Web.SessionState;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Handler
{
    public class Delete : IHttpHandler, IRequiresSessionState
    {
        protected string XmId;
        protected AppUser appUser;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string operate = context.Request["operate"];
            object sessionAppUser = context.Session[ConfigManager.GetSignInAppUserSessionName()];
            appUser = (AppUser)sessionAppUser;

            try
            {
                switch (operate)
                {
                    case "deleteGzzs":
                        DeleteGzzshfById(context);
                        break;
                    case "deleteSzgkjc_Dxjb_Records":
                        DeleteSzgkjc_Dxjb_Records(context);
                        break;
                    case "deleteSzgkjc_Dxjb_YzJb":
                        DeleteSzgkjc_Dxjb_YzJb(context);
                        break;
                }
            }
            catch (Exception err)
            {
                context.Response.Write("{\"Type\":\"Error\",\"Message\":\"" + err.Message + "\"}");
            }
        }

        public void DeleteGzzshfById(HttpContext context)
        {
            ZlctBLL BLL = new ZlctBLL(appUser);
            string gzzsId = context.Request.QueryString["gzzsId"];
            FunctionResult re = BLL.DeleteGzzshfById(gzzsId);
            if (re.Status != FunctionResultStatus.Error)
            {
                context.Response.Write("{\"Type\":\"Success\",\"Message\":\"" + re.Message.Message + "\"}");
            }
            else
            {
                context.Response.Write("{\"Type\":\"Failure\",\"Message\":\"" + re.Message.Message + "\"}");
            }
        }

        public void DeleteSzgkjc_Dxjb_Records(HttpContext context)
        {
            ZlctBLL BLL = new ZlctBLL(appUser);
            string id = context.Request.QueryString["id"];
            FunctionResult re = BLL.DeleteSzgkjc_Dxjb_Records(id);
            if (re.Status != FunctionResultStatus.Error)
            {
                context.Response.Write("{\"Type\":\"Success\",\"Message\":\"" + re.Message.Message + "\"}");
            }
            else
            {
                context.Response.Write("{\"Type\":\"Failure\",\"Message\":\"" + re.Message.Message + "\"}");
            }
        }
        public void DeleteSzgkjc_Dxjb_YzJb(HttpContext context)
        {
            ZlctBLL BLL = new ZlctBLL(appUser);
            string id = context.Request.QueryString["id"];
            FunctionResult re = BLL.DeleteSzgkjc_Dxjb_YzJb(id);
            if (re.Status != FunctionResultStatus.Error)
            {
                context.Response.Write("{\"Type\":\"Success\",\"Message\":\"" + re.Message.Message + "\"}");
            }
            else
            {
                context.Response.Write("{\"Type\":\"Failure\",\"Message\":\"" + re.Message.Message + "\"}");
            }
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
