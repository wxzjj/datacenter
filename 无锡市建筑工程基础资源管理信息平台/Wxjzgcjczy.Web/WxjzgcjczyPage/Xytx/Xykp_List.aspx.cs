using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Xytx
{
    public partial class Xykp_List : BasePage
    {
        public string qylx;
        protected void Page_Load(object sender, EventArgs e)
        {
           qylx=Request.QueryString["qylx"];
        }
    }
}
