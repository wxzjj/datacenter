using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Bigdesk8.Web
{
    public enum NavigatorType
    {
        UnKnown,    

        /// <summary>
        /// 低于11的ie
        /// </summary>
        IE,

        /// <summary>
        /// ie11及以上版本
        /// </summary>
        InternetExplorer
    }

    public struct NavigatorStruct
    {
        public NavigatorType TypeOfNavigator;
        public int MajorVersion;
    }

    /*
     * 1. 对于360浏览器，可以在页面的Head中添加一行代码：<meta name="renderer"content="webkit|ie-comp|ie-stand">
          content的取值为webkit，ie-comp，ie-stand之一，区分大小写，分别代表用极速模式，兼容模式，IE模式打开。
          ----该死的360浏览器，上述官方文档中信誓旦旦的说法也没有用！！！
     */

    public static class Waiter
    {
        /// <summary>
        /// 本函数用以识别各种浏览器，其功能的不足之处，请大家适时完善补充之。
        /// </summary>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        public static NavigatorStruct IdentifyNavigator(System.Web.HttpRequest argRequest)
        {
            NavigatorStruct resultStruct = new NavigatorStruct();

            if (argRequest.Browser.Browser.Equals("IE"))
            {
                resultStruct.TypeOfNavigator = NavigatorType.IE;
                resultStruct.MajorVersion = argRequest.Browser.MajorVersion;
                return resultStruct;
            }

            //可恶, 下面这种办法在某些windows 2003 server、windows 2008 server下无法识别ie11，其时Request.Browser.Browser中的值是Mozilla。
            //if (argRequest.Browser.Browser.Equals("InternetExplorer")) 
            //{
            //    resultStruct.TypeOfNavigator = NavigatorType.InternetExplorer;
            //    resultStruct.MajorVersion = argRequest.Browser.MajorVersion;
            //    return resultStruct;
            //}

            //对于argRequest.Browser中没能正确识别的浏览器，可考虑直接由argRequest.UserAgent用正则表达式来判断
            string ua = argRequest.UserAgent;
            if (ua.IndexOf("Trident/7.0") > -1)
            {
                resultStruct.TypeOfNavigator = NavigatorType.InternetExplorer;
                int rvIndex = ua.IndexOf("; rv:");
                resultStruct.MajorVersion = ua.Substring(rvIndex + 5, 2).ToInt32();
                return resultStruct;
            }
            //Match localMatch = Regex.Match(ua, "(msie ([\\d.]+))");
            ////Match localMatch = Regex.Match(ua, "msie");
            //if (localMatch.Success)
            //{
            //    resultStruct.TypeOfNavigator = NavigatorType.UnKnown;
            //    return resultStruct;
            //}
            resultStruct.TypeOfNavigator = NavigatorType.UnKnown;
            return resultStruct;
        }
    }
}
