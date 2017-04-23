using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Zyry
{
    public partial class Zczyry_List : BasePage
    {
        protected string rylx;
        protected void Page_Load(object sender, EventArgs e)
        {
            rylx = Request.QueryString["rylx"];
        }
    }
}
