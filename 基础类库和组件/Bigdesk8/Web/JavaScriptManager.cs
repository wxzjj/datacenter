using System;
using System.Web.UI;

namespace Bigdesk8.Web
{
    /// <summary>
    /// Web下的脚本处理类
    /// </summary>
    public static class JavaScriptManager
    {
        #region 打开链接，会留下历史记录

        /// <summary>
        /// 在最顶端的窗口打开链接，会留下历史记录
        /// </summary>
        /// <param name="page">WEB窗体</param>
        /// <param name="navigateUrl">URL</param>
        public static void TopWindowLocation(this Page page, string navigateUrl)
        {
            TopWindowLocation(page, navigateUrl, true);
        }

        /// <summary>
        /// 在最顶端的窗口打开链接，会留下历史记录
        /// </summary>
        /// <param name="page">WEB窗体</param>
        /// <param name="navigateUrl">URL</param>
        /// <param name="isScriptOnTop">JS是否在页面顶部</param>
        public static void TopWindowLocation(this Page page, string navigateUrl, bool isScriptOnTop)
        {
            WindowLocation(page, navigateUrl, "top", isScriptOnTop);
        }

        /// <summary>
        /// 在父窗口打开链接，会留下历史记录
        /// </summary>
        /// <param name="page">WEB窗体</param>
        /// <param name="navigateUrl">URL</param>
        public static void ParentWindowLocation(this Page page, string navigateUrl)
        {
            ParentWindowLocation(page, navigateUrl, true);
        }

        /// <summary>
        /// 在父窗口打开链接，会留下历史记录
        /// </summary>
        /// <param name="page">WEB窗体</param>
        /// <param name="navigateUrl">URL</param>
        /// <param name="isScriptOnTop">JS是否在页面顶部</param>
        public static void ParentWindowLocation(this Page page, string navigateUrl, bool isScriptOnTop)
        {
            WindowLocation(page, navigateUrl, "parent", isScriptOnTop);
        }

        /// <summary>
        /// 在当前窗口打开链接，会留下历史记录
        /// </summary>
        /// <param name="page">WEB窗体</param>
        /// <param name="navigateUrl">URL</param>
        public static void WindowLocation(this Page page, string navigateUrl)
        {
            WindowLocation(page, navigateUrl, true);
        }

        /// <summary>
        /// 在当前窗口打开链接，会留下历史记录
        /// </summary>
        /// <param name="page">WEB窗体</param>
        /// <param name="navigateUrl">URL</param>
        /// <param name="isScriptOnTop">JS是否在页面顶部</param>
        public static void WindowLocation(this Page page, string navigateUrl, bool isScriptOnTop)
        {
            WindowLocation(page, navigateUrl, "window", isScriptOnTop);
        }

        /// <summary>
        /// 打开链接，会留下历史记录
        /// </summary>
        /// <param name="page">WEB窗体</param>
        /// <param name="navigateUrl">URL</param>
        /// <param name="window">JS窗口</param>
        /// <param name="isScriptOnTop">JS是否在页面顶部</param>
        public static void WindowLocation(this Page page, string navigateUrl, string window, bool isScriptOnTop)
        {
            string script = "{0}.location.href = \"{1}\";";
            script = string.Format(script, DealString(window), DealString(navigateUrl));
            RegisterClientScript(page, script, isScriptOnTop);
        }

        #endregion 打开链接，会留下历史记录

        #region 打开链接，不会留下历史记录

        /// <summary>
        /// 在最顶端的窗口打开链接，不会留下历史记录
        /// </summary>
        /// <param name="page">WEB窗体</param>
        /// <param name="navigateUrl">URL</param>
        public static void TopWindowLocationReplace(this Page page, string navigateUrl)
        {
            TopWindowLocationReplace(page, navigateUrl, true);
        }

        /// <summary>
        /// 在最顶端的窗口打开链接，不会留下历史记录
        /// </summary>
        /// <param name="page">WEB窗体</param>
        /// <param name="navigateUrl">URL</param>
        /// <param name="isScriptOnTop">JS是否在页面顶部</param>
        public static void TopWindowLocationReplace(this Page page, string navigateUrl, bool isScriptOnTop)
        {
            WindowLocationReplace(page, navigateUrl, "top", isScriptOnTop);
        }

        /// <summary>
        /// 在父窗口打开链接，不会留下历史记录
        /// </summary>
        /// <param name="page">WEB窗体</param>
        /// <param name="navigateUrl">URL</param>
        public static void ParentWindowLocationReplace(this Page page, string navigateUrl)
        {
            ParentWindowLocationReplace(page, navigateUrl, true);
        }

        /// <summary>
        /// 在父窗口打开链接，不会留下历史记录
        /// </summary>
        /// <param name="page">WEB窗体</param>
        /// <param name="navigateUrl">URL</param>
        /// <param name="isScriptOnTop">JS是否在页面顶部</param>
        public static void ParentWindowLocationReplace(this Page page, string navigateUrl, bool isScriptOnTop)
        {
            WindowLocationReplace(page, navigateUrl, "parent", isScriptOnTop);
        }

        /// <summary>
        /// 在当前窗口打开链接，不会留下历史记录
        /// </summary>
        /// <param name="page">WEB窗体</param>
        /// <param name="navigateUrl">URL</param>
        public static void WindowLocationReplace(this Page page, string navigateUrl)
        {
            WindowLocationReplace(page, navigateUrl, true);
        }

        /// <summary>
        /// 在当前窗口打开链接，不会留下历史记录
        /// </summary>
        /// <param name="page">WEB窗体</param>
        /// <param name="navigateUrl">URL</param>
        /// <param name="isScriptOnTop">JS是否在页面顶部</param>
        public static void WindowLocationReplace(this Page page, string navigateUrl, bool isScriptOnTop)
        {
            WindowLocationReplace(page, navigateUrl, "window", isScriptOnTop);
        }

        /// <summary>
        /// 打开链接，不会留下历史记录
        /// </summary>
        /// <param name="page">WEB窗体</param>
        /// <param name="navigateUrl">URL</param>
        /// <param name="window">JS窗口</param>
        /// <param name="isScriptOnTop">JS是否在页面顶部</param>
        public static void WindowLocationReplace(this Page page, string navigateUrl, string window, bool isScriptOnTop)
        {
            string script = "{0}.location.replace(\"{1}\");";
            script = string.Format(script, DealString(window), DealString(navigateUrl));
            RegisterClientScript(page, script, isScriptOnTop);
        }

        #endregion 打开链接，不会留下历史记录

        #region 刷新窗口

        /// <summary>
        /// 刷新最顶端的窗口
        /// </summary>
        /// <param name="page">WEB窗体</param>
        public static void TopWindowRefresh(this Page page)
        {
            TopWindowRefresh(page, true);
        }

        /// <summary>
        /// 刷新最顶端的窗口
        /// </summary>
        /// <param name="page">WEB窗体</param>
        /// <param name="isScriptOnTop">JS是否在页面顶部</param>
        public static void TopWindowRefresh(this Page page, bool isScriptOnTop)
        {
            WindowRefresh(page, "top", isScriptOnTop);
        }

        /// <summary>
        /// 刷新父窗口
        /// </summary>
        /// <param name="page">WEB窗体</param>
        public static void ParentWindowRefresh(this Page page)
        {
            ParentWindowRefresh(page, true);
        }

        /// <summary>
        /// 刷新父窗口
        /// </summary>
        /// <param name="page">WEB窗体</param>
        /// <param name="isScriptOnTop">JS是否在页面顶部</param>
        public static void ParentWindowRefresh(this Page page, bool isScriptOnTop)
        {
            WindowRefresh(page, "parent", isScriptOnTop);
        }

        /// <summary>
        /// 刷新当前窗口
        /// </summary>
        /// <param name="page">WEB窗体</param>
        public static void WindowRefresh(this Page page)
        {
            WindowRefresh(page, true);
        }

        /// <summary>
        /// 刷新当前窗口
        /// </summary>
        /// <param name="page">WEB窗体</param>
        /// <param name="isScriptOnTop">JS是否在页面顶部</param>
        public static void WindowRefresh(this Page page, bool isScriptOnTop)
        {
            WindowRefresh(page, "window", isScriptOnTop);
        }

        /// <summary>
        /// 刷新指定窗口
        /// </summary>
        /// <param name="page">WEB窗体</param>
        /// <param name="window">JS窗口</param>
        /// <param name="isScriptOnTop">JS是否在页面顶部</param>
        public static void WindowRefresh(this Page page, string window, bool isScriptOnTop)
        {
            string script = "{0}.location={0}.location.href;";
            script = string.Format(script, DealString(window));
            RegisterClientScript(page, script, isScriptOnTop);
        }

        #endregion 刷新窗口

        #region 打开新窗口

        /// <summary>
        /// 打开一个新的默认外观的窗口
        /// </summary>
        /// <param name="page">WEB窗体</param>
        /// <param name="navigateUrl">URL</param>
        public static void OpenNewWindow(this Page page, string navigateUrl)
        {
            OpenNewWindow(page, navigateUrl, true);
        }

        /// <summary>
        /// 打开一个新的默认外观的窗口
        /// </summary>
        /// <param name="page">WEB窗体</param>
        /// <param name="navigateUrl">URL</param>
        /// <param name="isScriptOnTop">JS是否在页面顶部</param>
        public static void OpenNewWindow(this Page page, string navigateUrl, bool isScriptOnTop)
        {
            string script = "window.open(\"{0}\");";
            script = string.Format(script, DealString(navigateUrl));
            RegisterClientScript(page, script, isScriptOnTop);
        }

        /// <summary>
        /// 打开一个新的窗口, 无菜单栏, 无状态栏, 无工具栏, 位于屏幕左上角
        /// </summary>
        /// <param name="page">WEB窗体</param>
        /// <param name="navigateUrl">URL</param>
        /// <param name="scrollbarsVisible">滚动条是否可见</param>
        public static void OpenNewWindow2(this Page page, string navigateUrl, bool scrollbarsVisible)
        {
            OpenNewWindow2(page, navigateUrl, scrollbarsVisible, true);
        }

        /// <summary>
        /// 打开一个新的窗口, 无菜单栏, 无状态栏, 无工具栏, 位于屏幕左上角
        /// </summary>
        /// <param name="page">WEB窗体</param>
        /// <param name="navigateUrl">URL</param>
        /// <param name="scrollbarsVisible">滚动条是否可见</param>
        /// <param name="isScriptOnTop">JS是否在页面顶部</param>
        public static void OpenNewWindow2(this Page page, string navigateUrl, bool scrollbarsVisible, bool isScriptOnTop)
        {
            string scrollbars = scrollbarsVisible ? "yes" : "no";
            string script = "window.open(\"{0}\",\"{1}\",\"toolbar=no,directories=no,status=no,Menubar=no,scrollbars={2},resizable=yes,channelmode=no,width=screen.availWidth,height=screen.availHeight,top=0,left=0\");";
            script = string.Format(script, DealString(navigateUrl), Guid.NewGuid().ToString(), scrollbars);
            RegisterClientScript(page, script, isScriptOnTop);
        }

        #endregion 打开新窗口

        #region 警告窗口

        /// <summary>
        /// 在窗口中显示警告信息
        /// </summary>
        /// <param name="page">WEB窗体</param>
        /// <param name="message">警告信息</param>
        public static void WindowAlert(this Page page, string message)
        {
            WindowAlert(page, message, true);
        }

        /// <summary>
        /// 在窗口中显示警告信息
        /// </summary>
        /// <param name="page">WEB窗体</param>
        /// <param name="message">警告信息</param>
        /// <param name="isScriptOnTop">JS是否在页面顶部</param>
        public static void WindowAlert(this Page page, string message, bool isScriptOnTop)
        {
            string script = "window.alert(\"{0}\");";
            script = string.Format(script, DealString(message));
            RegisterClientScript(page, script, isScriptOnTop);
        }

        #endregion 警告窗口

        #region 关闭窗口

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="page">WEB窗体</param>
        public static void WindowClose(this Page page)
        {
            WindowClose(page, true);
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="page">WEB窗体</param>
        /// <param name="isScriptOnTop">JS是否在页面顶部</param>
        public static void WindowClose(this Page page, bool isScriptOnTop)
        {
            string script = "window.opener=null;window.open('','_self','');window.close();";
            RegisterClientScript(page, script, isScriptOnTop);
        }

        /// <summary>
        /// 关闭最顶端的窗口
        /// </summary>
        /// <param name="page">WEB窗体</param>
        public static void TopWindowClose(this Page page)
        {
            TopWindowClose(page, true);
        }

        /// <summary>
        /// 关闭最顶端的窗口
        /// </summary>
        /// <param name="page">WEB窗体</param>
        /// <param name="isScriptOnTop">JS是否在页面顶部</param>
        public static void TopWindowClose(this Page page, bool isScriptOnTop)
        {
            string script = "top.opener=null;top.open('','_self','');top.close();";
            RegisterClientScript(page, script, isScriptOnTop);
        }

        #endregion 关闭窗口

        #region 执行脚本

        /// <summary>
        /// 执行脚本
        /// </summary>
        /// <param name="page">WEB窗体</param>
        /// <param name="javascriptString">需要执行的 javascript 脚本字符串</param>
        public static void WindowExecuteString(this Page page, string javascriptString)
        {
            WindowExecuteString(page, javascriptString, true);
        }

        /// <summary>
        /// 执行脚本
        /// </summary>
        /// <param name="page">WEB窗体</param>
        /// <param name="javascriptString">需要执行的 javascript 脚本字符串</param>
        /// <param name="isScriptOnTop">JS是否在页面顶部</param>
        public static void WindowExecuteString(this Page page, string javascriptString, bool isScriptOnTop)
        {
            string script = DealString(javascriptString);
            RegisterClientScript(page, script, isScriptOnTop);
        }

        #endregion 执行脚本

        #region 私有函数

        /// <summary>
        /// 处理参数格式
        /// </summary>
        /// <param name="param">格式</param>
        /// <returns></returns>
        private static string DealString(string param)
        {
            if (string.IsNullOrEmpty(param)) return string.Empty;

            param = param.Replace(Environment.NewLine, "\\n");//回车//换行 
            param = param.Replace(Convert.ToChar(13).ToString(), "\\n");//回车   
            param = param.Replace(Convert.ToChar(10).ToString(), "\\n");//换行

            return param;
        }

        /// <summary>
        /// 注册脚本
        /// </summary>
        /// <param name="page">WEB窗体</param>
        /// <param name="script">脚本</param>
        /// <param name="isScriptOnTop">JS是否在页面顶部</param>
        private static void RegisterClientScript(Page page, string script, bool isScriptOnTop)
        {
            if (isScriptOnTop)
                page.ClientScript.RegisterClientScriptBlock(page.GetType(), Guid.NewGuid().ToString(), script, true);
            else
                page.ClientScript.RegisterStartupScript(page.GetType(), Guid.NewGuid().ToString(), script, true);
        }

        #endregion 私有函数
    }
}
