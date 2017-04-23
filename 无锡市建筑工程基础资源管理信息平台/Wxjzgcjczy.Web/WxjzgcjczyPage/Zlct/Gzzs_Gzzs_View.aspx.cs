using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bigdesk8.Data;
using Wxjzgcjczy.BLL;
using Bigdesk8.Data;
using Bigdesk8.Web.Controls;
using System.Data;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Zlct
{
    public partial class Gzzs_Gzzs_View : BasePage
    {
        protected ZlctBLL BLL;
        protected string gzzsId;
        protected string contentHtml;
        protected void Page_Load(object sender, EventArgs e)
        {
            gzzsId = Request.QueryString["gzzsId"];
            BLL = new ZlctBLL(this.WorkUser);
            if(!this.IsPostBack)
            {
                BindGzzsxx();
                SearchData(0);
            }
        }

        private void SearchData(int pageIndex)
        {
            int allRecordCount = 0;
            List<IDataItem> list = new List<IDataItem>();
            DataTable dt = BLL.RetrieveZshfListByGzzsId(list,gzzsId, this.gridView.PageSize, pageIndex, out allRecordCount).Result;
            this.gridView.RecordCount = allRecordCount;
            this.gridView.DataSource = dt;
            this.gridView.DataBind();
        }

        private void BindGzzsxx()
        {
            DataTable dt = BLL.ReadSzgkjc_Gzzs(gzzsId).Result;
            if (dt.Rows.Count > 0)
            {
                List<IDataItem> list = dt.Rows[0].ToDataItem();
                this.SetControlValue(list);
                contentHtml = list.GetDataItem("GzzsNr").ItemData;
            }
        
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SearchData(e.NewPageIndex);
        }
    }
}
