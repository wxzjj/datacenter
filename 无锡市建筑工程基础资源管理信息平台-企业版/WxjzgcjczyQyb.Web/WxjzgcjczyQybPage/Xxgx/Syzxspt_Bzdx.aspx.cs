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
    public partial class Syzxspt_Bzdx : BasePage2
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

            dt = xxgxBll.GetSyzxspt(list, this.gridView.PageSize, pageIndex, out allRecordCount, "Syzxspt_Bzdxxx");

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

        protected void Button_Search_Click(object sender, EventArgs e)
        {
            SearData(0);
        }

        protected void DBChl_sfyts_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearData(0);
        }
    }
}

