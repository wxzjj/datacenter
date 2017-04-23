using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Wxjzgcjczy.BLL;
using Bigdesk8;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage
{
    public partial class DataExChange_List : System.Web.UI.Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.Page.Theme = ConfigManager.GetThemeInUsing();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                powerGridViewDataBind(0);
            }
        }

        //分页
        protected void powerGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.powerGridViewDataBind(e.NewPageIndex);
        }

        public void powerGridViewDataBind(int pageIndex)
        {
            int allRecordCount;
            List<IDataItem> list = this.GetControlValue();
            SystemBLL BLL = new SystemBLL();
            DataTable dt = BLL.GetStLog(list, this.gridView.PageSize, pageIndex, out allRecordCount, DropScsfcg.SelectedValue);


            this.gridView.RecordCount = allRecordCount;
            this.gridView.PageIndex = pageIndex;
            this.gridView.DataSource = dt;
            this.gridView.DataBind();
        }

        protected void ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            powerGridViewDataBind(0);
        }

    }
}
