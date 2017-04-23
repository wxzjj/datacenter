using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WxjzgcjczyQyb.BLL;
using System.Data;
using Bigdesk8;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;

namespace WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Zyry
{
    public partial class Aqscglry_List : BasePage
    {
        public ZcryBLL zcryBll;
        private AppUser WorkUser = new AppUser();

        protected void Page_Load(object sender, EventArgs e)
        {
            zcryBll = new ZcryBLL(this.WorkUser);
            if (!IsPostBack)
            {
                WebCommon.DropDownListDataBind(this.ssdq, true);
                //数据绑定  
                SearData(0);
            }
        }

        //数据绑定
        public void SearData(int pageIndex)
        {
            int allRecordCount;
            List<IDataItem> list = this.GetControlValue();
            string tblb = "";
            foreach (ListItem item in this.DBCheckBoxList1.Items)
            {
                if (item.Selected)
                    tblb += item.Value + ",";
            }
            tblb = tblb.Trim(',');
            WebCommon.AddDataItem(list, "ryzyzglxID", tblb);


            DataTable dt = new DataTable();

            dt = zcryBll.RetrieveZyryJbxx("aqscglry", list, this.gridView.PageSize, pageIndex, "", out allRecordCount).Result;

            this.gridView.RecordCount = allRecordCount;
            this.gridView.PageIndex = pageIndex;
            this.gridView.DataSource = dt;
            this.gridView.DataBind();
        }

        //分页
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SearData(e.NewPageIndex);
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
            SearData(0);
        }
    }
}