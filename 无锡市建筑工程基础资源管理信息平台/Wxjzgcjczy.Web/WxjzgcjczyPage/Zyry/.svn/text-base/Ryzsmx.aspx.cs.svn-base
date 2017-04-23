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

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Zyry
{
    public partial class Ryzsmx : BasePage
    {
        private ZyryBLL BLL;
        protected string rowID;
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL = new ZyryBLL(WorkUser);
            rowID = Request.QueryString["rowid"];
            List<IDataItem> list = BLL.ReadRyzs(rowID).Result;
            this.SetControlValue(list);
        }
    }
}
