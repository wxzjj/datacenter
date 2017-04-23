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

namespace WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Xxgx
{
    public partial class Jkztjk_List : BasePage2
    {
        public XxgxBLL xxgxBll;
        protected void Page_Load(object sender, EventArgs e)
        {
            xxgxBll = new XxgxBLL(this.WorkUser);
            if (!IsPostBack)
            {
                //数据绑定  
                SearData(0);
            }
        }

        //数据绑定
        public void SearData(int pageIndex)
        {
            int allRecordCount;
            List<IDataItem> list = this.GetControlValue();

            DataTable dt = new DataTable();

            dt = xxgxBll.RetrieveApiZb(list, this.gridView.PageSize, pageIndex, "apiFlow asc", out allRecordCount);

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

                LinkButton hl_apiC = (LinkButton)e.Row.FindControl("lnkbtnApiControl");//设置
                Label apiRunState = (Label)e.Row.FindControl("lblapiRunState");

                if (apiRunState.Text == "1")
                {
                    apiRunState.Text = "正常";
                }
                else
                {
                    apiRunState.Text = "异常";
                    apiRunState.ForeColor=System.Drawing.Color.Red;
                }

                if (hl_apiC.Text == "开启")
                {
                    hl_apiC.ForeColor = System.Drawing.Color.Red;
                }
                hl_apiC.CommandArgument = e.Row.RowIndex.ToString();
            }
        }

        protected void gridView_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = this.gridView.Rows[rowIndex];
            string apiFlow = (row.FindControl("lblapiFlow") as Label).Text;
            string apiControl = (row.FindControl("lblapiControl") as Label).Text;

            if (e.CommandName == "Jkkz")
            {
                apiControl = (apiControl == "1" ? "0" : "1");
                xxgxBll.UpdateApiZbApiControl(apiFlow, apiControl);
                this.WindowAlert("提示:接口" + (apiControl == "1" ? "开启" : "关闭") + "成功!");
                SearData(0);
            }

            else
            {
                return;
            }
        }

        protected void Button_Search_Click(object sender, EventArgs e)
        {
            SearData(0);
        }
    }
}

