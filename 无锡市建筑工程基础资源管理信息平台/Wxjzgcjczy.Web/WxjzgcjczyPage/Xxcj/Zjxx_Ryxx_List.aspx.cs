using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Xxcj
{
    public partial class Zjxx_Ryxx_List : BasePage
    {
        public string zljdbm;
        protected void Page_Load(object sender, EventArgs e)
        {
            zljdbm=Request.QueryString["zljdbm"];
        }
    }
}
