using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wxjzgcjczy.BLL;
using System.Web.SessionState;
using Bigdesk8.Web;
using System.Text;//ashx页面使用session, 第一步：导入此命名空间
using Bigdesk8;


namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Handler
{
    //第二步：实现接口IRequiresSessionState   到此就可以像平时一样用Session了
    public class Login : IHttpHandler, IRequiresSessionState
    {

        private readonly SystemBLL systemBLL = new SystemBLL();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            try
            {
                //登出
                if (context.Request.Params["Action"] == "Exist")
                {
                    context.Session.Remove(ConfigManager.GetSignInAppUserSessionName());
                    context.Session.RemoveAll();
                    LoginManager.SignOut();
                    //context.Response.Write("true");
                    context.Response.Write("true");
                }
                else
                    //登录
                    if (context.Request.Params["Action"] == "Login")
                    {
                        ValidateLogin();
                    }
                    else
                        //修改密码
                        if (context.Request.Params["Action"] == "Changepwd")
                        {
                            ChangePwd();
                        }
            }
            catch (Exception ex)
            {

                context.Response.Write("{\"IsSuccess\":false,\"Msg\":\"" + ex.Message + ex.StackTrace.Replace("\"", "") + "\"}");
            }
        }

        /// <summary>
        /// 登录校验
        /// </summary>
        private void ValidateLogin()
        {
            StringBuilder str = new StringBuilder();
            HttpContext context = System.Web.HttpContext.Current;
            string username = context.Request.Params["username"];
            string password = context.Request.Params["password"];
            string verificationCode = context.Request.Params["verificationCode"];

            if (string.IsNullOrEmpty(username))
            {
                str.Append("{\"IsSuccess\":false,\"Msg\":\"用户名不能为空\"}");
                context.Response.Write(str.ToString());
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                str.Append("{\"IsSuccess\":false,\"Msg\":\"密码不能为空\"}");
                context.Response.Write(str.ToString());
                return;
            }
            if (string.IsNullOrEmpty(verificationCode))
            {
                str.Append("{\"IsSuccess\":false,\"Msg\":\"验证码不能为空\"}");
                context.Response.Write(str.ToString());
                return;
            }

            object verificationCodeServer = context.Session[ConfigManager.GetVerificationCode_SessionName()];
            if (verificationCodeServer == null || verificationCodeServer.Equals(String.Empty))
            {
                str.Append("{\"IsSuccess\":false,\"Msg\":\"服务器端找不到验证码\"}");
                context.Response.Write(str.ToString());
                return;
            }


            if (string.IsNullOrEmpty(verificationCode) || !verificationCode.ToString().Equals(verificationCodeServer.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                str.Append("{\"IsSuccess\":false,\"Msg\":\"验证码不正确\"}");
                context.Response.Write(str.ToString());
                return;
            }

            object loginCount = context.Session["loginCount"];
            int waitTime = ConfigManager.GetLoginErrorWait();
            if (loginCount != null && loginCount.ToInt32(0) >= ConfigManager.GetAllowLoginCount() && !string.IsNullOrEmpty(context.Session["loginForbidTime"].ToString2()))
            {

                DateTime oldTime = DateTime.Parse(context.Session["loginForbidTime"].ToString2());
                TimeSpan span = DateTime.Now - oldTime;

                if (span.Minutes < waitTime)
                {
                    str.Append("{\"IsSuccess\":false,\"Msg\":\"你登录错误已超过" + ConfigManager.GetAllowLoginCount() + "次，请" + (waitTime - span.Minutes) + "分钟后重试\"}");
                    context.Response.Write(str.ToString());
                    return;
                }
                else
                {
                    context.Session["loginCount"] = 0;
                    context.Session["loginForbidTime"] = "";
                }

            }
            if (systemBLL.UserLogin(username, password))
            {
                if (loginCount != null)
                {
                    context.Session["loginCount"] = 0;

                }
                AppUser workUser = new AppUser();
                
                workUser = systemBLL.InitAppUser(username, password).Result;
                //更新用户登录时间
                systemBLL.UpdateLoginTime(workUser.UserID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                context.Session[ConfigManager.GetSignInAppUserSessionName()] = workUser;

                LoginManager.SetLoginID(workUser.UserID);
                if (string.IsNullOrEmpty(workUser.qyID))
                    str.Append("{\"IsSuccess\":true,\"Msg\":\"登录成功\",\"url\":\"/WxjzgcjczyPage/MainPage/Index.aspx\"}");
                else
                    str.Append("{\"IsSuccess\":true,\"Msg\":\"登录成功\",\"url\":\"/WxjzgcjczyPage/MainPage/Index2.aspx\"}");
                context.Response.Write(str.ToString());

            }
            else
            {
                loginCount = context.Session["loginCount"];
                if (loginCount == null)
                {
                    loginCount = 1;
                }
                else
                {
                    loginCount = loginCount.ToInt32(0) + 1;
                }
                context.Session["loginCount"] = loginCount;

                if (loginCount != null && loginCount.ToInt32(0) == ConfigManager.GetAllowLoginCount())
                {
                    context.Session["loginForbidTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }

                str.Append("{\"IsSuccess\":false,\"Msg\":\"用户名或密码错误\"}");
                context.Response.Write(str.ToString());
            }
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        private void ChangePwd()
        {
            HttpContext context = System.Web.HttpContext.Current;
            var userid = context.Request.Params["userid"];
            var password = context.Request.Params["password"];

            try
            {
                systemBLL.ChangePwd(userid, password);
                context.Response.Write("true");
            }
            catch (Exception e)
            {
                context.Response.Write("false");
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
