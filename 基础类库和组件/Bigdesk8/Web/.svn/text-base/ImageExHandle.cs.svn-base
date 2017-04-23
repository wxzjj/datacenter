using System;
using System.Collections.Generic;
using System.Text;
using System.Web.SessionState;
using System.Web;
using System.IO;

namespace Bigdesk8.Web
{
    class ImageExHandle : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public void ProcessRequest(HttpContext context)
        {
            string i = HttpContext.Current.Request.QueryString["i"];
            MemoryStream stream = (MemoryStream)HttpContext.Current.Session[i];
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ContentType = "image/Png";
            HttpContext.Current.Response.BinaryWrite(stream.ToArray());
        }
    }
}
