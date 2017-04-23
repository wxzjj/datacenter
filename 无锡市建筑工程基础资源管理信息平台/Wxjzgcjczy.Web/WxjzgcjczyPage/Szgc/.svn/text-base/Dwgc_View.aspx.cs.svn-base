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

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Szgc
{
    public partial class Dwgc_View :BasePage
    {
        private string rowID;
        private SzgcBLL BLL;
        protected void Page_Load(object sender, EventArgs e)
        {
            rowID =Request.QueryString["rowid"];
            BLL = new SzgcBLL(WorkUser);
            List<IDataItem> list = BLL.ReadDwgc(rowID).Result;
            if (list.Count <= 0)
            {
                this.WindowAlert("没有获取到数据！");
                return;
            }
            this.SetControlValue(list);
        }
    }
}
