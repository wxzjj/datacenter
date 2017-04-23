using System;
using System.Collections.Generic;
using System.Text;

namespace Bigdesk2010.Security
{
    /// <summary>
    /// Griddle: 一个秀安全保护功能的筛子。
    /// </summary>
    public class Griddle
    {
        public static string GetSafeUrl(string inputUrl)
        {
            ///目前仅直接调用微软AntiXSS中的功能，必要时可以自行修改补充；
            ///可考虑在此函数中加入对“响应拆分”漏洞的处理功能
            /// 此处用到了AntiXSSLibrary.dll，而此dll中win7环境中会导致System.Runtime.CompilerServices.ExtensionAttribute引用错乱，
            /// 故将对AntiXSSLibrary.dll的引用去除，从而此功能被废止，2014-1-24，mwh
            //return Microsoft.Security.Application.Encoder.UrlEncode(inputUrl);
            return inputUrl;
        }
    }
}
