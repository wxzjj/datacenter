using System;
using System.Collections.Generic;
using System.Configuration;
using System;
using System.Web;
using System.Web.Services;
using Bigdesk8.Data;
using System.Data;

namespace SparkServiceDesk
{
    /// <summary>
    /// 为配合Web Service客户端程序的开发试验而奋斗！！！
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class TestBed : System.Web.Services.WebService
    {

        [WebMethod]
        public string Echo(string inString)
        {
            return inString;
        }
    }
}

