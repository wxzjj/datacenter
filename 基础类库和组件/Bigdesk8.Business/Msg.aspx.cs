using System;
using System.Web.UI;

namespace Bigdesk8.Business
{
    public partial class Msg : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string msg = this.Request.QueryString["msg"];
            string nam = this.Request.QueryString["nam"];

            if (!msg.IsEmpty())
            {
                msg += "<br/>";
            }

            if (!nam.IsEmpty())
            {
                msg += this.Session[nam].ToString2();
            }

            if (msg.IsEmpty())
            {
                msg = "发生未知错误a0！";
            }

            this.p_msg.InnerHtml = msg;
        }
    }
}
