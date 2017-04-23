using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WxjzgcjczyQyb.BLL;
using Bigdesk8;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;

namespace WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Xxgx
{
    public partial class Jkcbxx_View : BasePage2
    {
        public string apiFlow;
        XxgxBLL xxgxBll;
        protected void Page_Load(object sender, EventArgs e)
        {
            apiFlow = Request.QueryString["apiFlow"].ToString();
            xxgxBll = new XxgxBLL(this.WorkUser);
            if (!IsPostBack)
            {
                SearchData(0);
            }
        }

        public void SearchData(int pageIndex)
        {
            int allRecordCount;
            List<IDataItem> list = this.GetControlValue();
            WebCommon.AddDataItem(list, "apiFlow", apiFlow);

            DataTable dt = xxgxBll.RetrieveApiCb(list, this.gridView.PageSize, pageIndex, "apiDyTime desc", out allRecordCount);

            this.gridView.RecordCount = allRecordCount;
            this.gridView.PageIndex = pageIndex;
            this.gridView.DataSource = dt;
            this.gridView.DataBind();
        }



        //分页
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SearchData(e.NewPageIndex);
        }


        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TableCellCollection cells = e.Row.Cells;
                foreach (TableCell cell in cells)
                {
                    if (!string.IsNullOrEmpty(cell.Text))
                    {
                        cell.Text = Server.HtmlDecode(cell.Text);
                    }
                }
            }
        }


        protected void Button_Search_Click(object sender, EventArgs e)
        {
            SearchData(0);
        }


    }
}
