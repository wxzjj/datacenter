using System;
using System.Collections.Generic;
using System.Text;

namespace Bigdesk8.Security
{
    /// <summary>
    /// Griddle: 一个秀安全保护功能的筛子
    /// </summary>
    public class Griddle
    {
        public static string GetSafeUrl(string inputUrl)
        {
            ///目前仅直接调用微软AntiXSS中的功能，必要时可以自行修改补充；
            ///可考虑在此函数中加入对“响应拆分”漏洞的处理功能
            return Microsoft.Security.Application.Encoder.UrlEncode(inputUrl);
        }
    }
}
