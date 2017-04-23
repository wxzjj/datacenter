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

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Gzyj
{
    public partial class Gcxmbg_View : BasePage
    {
        private GzyjBLL BLL;
        private string rowID;
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL = new GzyjBLL(WorkUser);
            rowID = Request.QueryString["rowid"];
            List<IDataItem> list = BLL.Read(rowID).Result;
            this.SetControlValue(list);
        }
    }
}
