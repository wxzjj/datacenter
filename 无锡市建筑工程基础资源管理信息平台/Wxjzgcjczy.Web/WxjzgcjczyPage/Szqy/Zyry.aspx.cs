using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wxjzgcjczy.BLL;
using System.Data;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Szqy
{
    public partial class Zyry : BasePage
    {
        protected string qyID, ryID;
        private string befrom;
        protected void Page_Load(object sender, EventArgs e)
        {
            qyID = Request.QueryString["qyid"];
            ryID = Request.QueryString["ryid"];
            befrom = Request.QueryString["befrom"];
        }
    }
}
