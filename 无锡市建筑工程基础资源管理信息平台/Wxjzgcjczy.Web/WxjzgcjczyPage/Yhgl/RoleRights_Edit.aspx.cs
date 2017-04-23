using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Yhgl
{
    public partial class RoleRights_Edit : BasePage
    {
        public string RoleID;
        protected void Page_Load(object sender, EventArgs e)
        {
            RoleID=Request.QueryString["roleId"];
        }
    }
}
