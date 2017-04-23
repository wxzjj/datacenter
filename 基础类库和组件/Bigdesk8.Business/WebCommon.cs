using System;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bigdesk8;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;

namespace Bigdesk8.Business
{
    public static class WebCommon
    {
        /// <summary>
        /// 根URL，必须以“/”开头和结尾
        /// </summary>
        public static string GetRootUrl()
        {
            return ConfigurationManager.AppSettings["Bigdesk_Business_RootUrl"];
        }

        /// <summary>
        /// 主页
        /// </summary>
        public static string GetIndexUrl()
        {
            return GetRootUrl() + "Index.aspx";
        }

        /// <summary>
        /// 是否是调试状态
        /// </summary>
        public static bool GetIsDebug()
        {
            return ConfigurationManager.AppSettings["Bigdesk_Business_IsDebug"].ToBoolean();
        }

        #region 登录用户

        /// <summary>
        /// 登录用户在Session中的名称
        /// </summary>
        public static string GetLoginSessionName()
        {
            return ConfigurationManager.AppSettings["Bigdesk_Business_LoginSessionName"];
        }

        /// <summary>
        /// 从Session中得到登录用户名
        /// </summary>
        public static string GetLoginNameFromSession()
        {
            object sessionObject = System.Web.HttpContext.Current.Session[GetLoginSessionName()];
            if (sessionObject.IsEmpty())
            {
                sessionObject = Bigdesk8.Business.UserRightManager.UserRightManager.GuestLoginName;
            }
            return sessionObject.ToString2();
        }

        public static string GetLoginPageUrl()
        {
            return ConfigurationManager.AppSettings["Bigdesk_Business_LoginPageUrl"];
        }

        public static string GetReloginPageUrl()
        {
            return ConfigurationManager.AppSettings["Bigdesk_Business_ReloginPageUrl"];
        }

        public static string GetModifyPasswordPageUrl()
        {
            return ConfigurationManager.AppSettings["Bigdesk_Business_ModifyPasswordPageUrl"];
        }

        #endregion 登录用户

        #region 结果处理

        /// <summary>
        /// 页面跳转，显示提示信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg">提示信息</param>
        public static void ResponseRedirect(this Page page, string msg)
        {
            page.ResponseRedirect(msg, "");
        }
        /// <summary>
        /// 页面跳转，显示提示信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg">提示信息</param>
        /// <param name="window">目标窗口名称</param>
        public static void ResponseRedirect(this Page page, string msg, string window)
        {
            msg = msg.Replace(Environment.NewLine, "<br/>");

            if (window.IsEmpty())
            {
                if (msg.Length > 100)
                {
                    string nam = "CFBMSG" + Guid.NewGuid().ToString();
                    page.Session[nam] = msg;
                    page.Response.Redirect(GetRootUrl() + "Msg.aspx?nam=" + nam, true);
                }
                else
                {
                    page.Response.Redirect(GetRootUrl() + "Msg.aspx?msg=" + msg, true);
                }
            }
            else
            {
                string nam = "CFBMSG" + Guid.NewGuid().ToString();
                page.Session[nam] = msg;
                page.WindowLocation(GetRootUrl() + "Msg.aspx?nam=" + nam, window, true);
            }
        }
        /// <summary>
        /// 页面跳转，显示提示信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg">提示信息</param>
        public static void ResponseRedirect(this Page page, Exception msg)
        {
            page.ResponseRedirect(msg, "");
        }
        /// <summary>
        /// 页面跳转，显示提示信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg">提示信息</param>
        /// <param name="window">目标窗口名称</param>
        public static void ResponseRedirect(this Page page, Exception msg, string window)
        {
            page.ResponseRedirect(GetExceptionMessage(msg), window);
        }
        /// <summary>
        /// 处理返回的结果,
        /// 成功返回true，程序可以继续运行；失败返回false，程序不能继续运行。
        /// </summary>
        public static bool DealResult(this Page page, FunctionResult fr)
        {
            if (fr == null) return true;

            FunctionResultException a = fr.Message.GetFunctionResultException();
            if (a != null) fr = a.Result;

            switch (fr.Status)
            {
                default:
                case FunctionResultStatus.None:
                    return true;
                case FunctionResultStatus.Info:
                    page.WindowAlert(fr.Message.Message);
                    return true;
                case FunctionResultStatus.Warn:
                    page.WindowAlert(fr.Message.Message);
                    return false;
                case FunctionResultStatus.Error:
                    page.ResponseRedirect(fr.Message);
                    return false;
            }
        }
        /// <summary>
        /// 处理返回的结果,
        /// 成功返回true，程序可以继续运行；失败返回false，程序不能继续运行。
        /// </summary>
        public static bool DealResult<T>(this Page page, FunctionResult<T> fr)
        {
            if (fr == null) return true;

            FunctionResultException<T> a = fr.Message.GetFunctionResultException<T>();
            if (a != null) fr = a.Result;

            switch (fr.Status)
            {
                default:
                case FunctionResultStatus.None:
                    return true;
                case FunctionResultStatus.Info:
                    page.WindowAlert(fr.Message.Message);
                    return true;
                case FunctionResultStatus.Warn:
                    page.WindowAlert(fr.Message.Message);
                    return false;
                case FunctionResultStatus.Error:
                    page.ResponseRedirect(fr.Message);
                    return false;
            }
        }
        /// <summary>
        /// 处理返回的结果,
        /// 成功返回true，程序可以继续运行；失败直接抛出异常FunctionResultException。
        /// </summary>
        public static bool DealResult(FunctionResult fr)
        {
            if (fr == null) return true;

            FunctionResultException a = fr.Message.GetFunctionResultException();
            if (a != null) fr = a.Result;

            switch (fr.Status)
            {
                default:
                case FunctionResultStatus.None:
                    return true;
                case FunctionResultStatus.Info:
                case FunctionResultStatus.Warn:
                case FunctionResultStatus.Error:
                    {
                        throw new FunctionResultException(fr);
                    }
            }
        }
        /// <summary>
        /// 处理返回的结果,
        /// 成功返回true，程序可以继续运行；失败直接抛出异常FunctionResultException<T>。
        /// </summary>
        public static bool DealResult<T>(FunctionResult<T> fr)
        {
            if (fr == null) return true;

            FunctionResultException<T> a = fr.Message.GetFunctionResultException<T>();
            if (a != null) fr = a.Result;

            switch (fr.Status)
            {
                default:
                case FunctionResultStatus.None:
                    return true;
                case FunctionResultStatus.Info:
                case FunctionResultStatus.Warn:
                case FunctionResultStatus.Error:
                    {
                        throw new FunctionResultException<T>(fr);
                    }
            }
        }
        private static string GetExceptionMessage(Exception ex)
        {
            if (ex == null) return "";
            string r = ex.Message;
            if (GetIsDebug())
                r += Environment.NewLine + ex.StackTrace;
            return r + Environment.NewLine + GetExceptionMessage(ex.InnerException);
        }

        #endregion 结果处理
    }
}
