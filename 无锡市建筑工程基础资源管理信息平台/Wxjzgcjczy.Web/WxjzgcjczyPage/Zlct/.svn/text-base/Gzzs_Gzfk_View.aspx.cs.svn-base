using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Wxjzgcjczy.BLL;
using Bigdesk8.Data;
using Bigdesk8.Web.Controls;



namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Zlct
{
    public partial class Gzzs_Gzfk_View : BasePage
    {
        protected ZlctBLL BLL;
        protected string ZshfId;
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL = new ZlctBLL(WorkUser);
            ZshfId = Request.QueryString["zshfId"];
            if (!this.IsPostBack)
            {
                DataTable dt = BLL.ReadSzgkjc_Gzhf(ZshfId).Result;
                if (dt.Rows.Count > 0)
                {
                    List<IDataItem> list = dt.Rows[0].ToDataItem();
                    this.SetControlValue(list);

                    string gzzsId = list.GetDataItem("GzzsId").ItemData;
                    dt = BLL.ReadSzgkjc_Gzzs(gzzsId).Result;
                    list = dt.Rows[0].ToDataItem();
                    this.SetControlValue(list);
                    if (Convert.ToInt32(db_zshfState.Text)==0)
                    {
                     BLL.UpdateZlhfToReadedState(ZshfId);
                    }
                }
            
            }
        }
    }
}
