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
    public partial class JbZxjk_FailureList : System.Web.UI.Page
    {
        public XxgxBLL xxgxBll;
        private string pkid;
        protected void Page_Load(object sender, EventArgs e)
        {
            pkid = Request.QueryString["pkid"];

            xxgxBll = new XxgxBLL(new AppUser());
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

            IDataItem item = new DataItem();
            item.ItemName = "pkid";
            item.ItemData = pkid;

            list.Add(item);

            DataTable dt = new DataTable();

            dt = xxgxBll.GetFailureStLog(list, this.gridView.PageSize, pageIndex, out allRecordCount);

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
    }
}