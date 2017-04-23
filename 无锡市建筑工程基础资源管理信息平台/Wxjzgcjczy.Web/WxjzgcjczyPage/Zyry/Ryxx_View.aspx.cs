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
    public partial class Ryxx_View : BasePage
    {
        private ZyryBLL BLL;
        protected string  ryID;
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL = new ZyryBLL(WorkUser);
            ryID = Request.QueryString["ryid"];
            //rowID = Request.QueryString["rowID"];
            if (!IsPostBack)
            {
                List<IDataItem> list = BLL.ReadRyxxView(ryID).Result;
                if (list.Count <= 0)
                {
                    this.WindowAlert("没有获取到数据！");
                    return;
                }
                this.SetControlValue(list);
                //if (string.IsNullOrEmpty(ryID) && list.Count > 0)
                //    ryID = list.GetDataItem("ryid").ItemData;

            }
        }


    }
}
