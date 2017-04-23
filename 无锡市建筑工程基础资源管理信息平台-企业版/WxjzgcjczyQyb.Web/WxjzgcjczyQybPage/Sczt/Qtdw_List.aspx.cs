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


namespace WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Sczt
{
    public partial class Qtdw_List : BasePage
    {
        public ScztBLL scztBll;
        private AppUser WorkUser = new AppUser();

        protected void Page_Load(object sender, EventArgs e)
        {
            scztBll = new ScztBLL(this.WorkUser);
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

            DataTable dt = new DataTable();

            dt = scztBll.RetrieveQyxxList("qtdw", list, this.gridView.PageSize, pageIndex, out allRecordCount).Result;

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

