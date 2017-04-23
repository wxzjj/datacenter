using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wxjzgcjczy.BLL;
using Bigdesk8.Data;
using Bigdesk8.Web.Controls;
using System.Data;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Zlct
{
    public partial class SelectFxr_List : BasePage
    {
        ZlctBLL BLL;
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL = new ZlctBLL(this.WorkUser);
            if (!this.IsPostBack)
            {
                searchData(0);
            }
        }

        private void searchData(int pageIndex)
        {
            int allRecordCount;
            List<IDataItem> list = new List<IDataItem>();
            this.GetControlValue(list, false, true);

            DataTable dt = BLL.RetrieveUser_List( list, this.gridView.PageSize, 0, out allRecordCount).Result;
            this.gridView.RecordCount = allRecordCount;
            this.gridView.PageIndex = pageIndex;
            this.gridView.DataSource = dt;
            this.gridView.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            searchData(0);
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            searchData(e.NewPageIndex);
        }

        /// <summary>
        /// 刷新列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            searchData(0);
        }
    }
}
