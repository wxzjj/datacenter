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

namespace WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Xytx
{
    public partial class Xykp_List : BasePage
    {
        public XytxBLL xytxBll;
        private AppUser WorkUser = new AppUser();

        protected void Page_Load(object sender, EventArgs e)
        {
            xytxBll = new XytxBLL(this.WorkUser);
            if (!IsPostBack)
            {
                WebCommon.CheckBoxListDataBind(this.cbl_ssdq);
                //数据绑定  
                SearData(0);
            }
        }

        //数据绑定
        public void SearData(int pageIndex)
        {
            string ssdq = "";
            string zzlb = "";
            foreach (ListItem item in this.cbl_ssdq.Items)
            {
                if (item.Selected)
                    ssdq += item.Text + ",";
            }
            ssdq = ssdq.Trim(',');

            foreach (ListItem item in this.cbl_zzlb.Items)
            {
                if (item.Selected)
                    zzlb += item.Text + ",";
            }
            zzlb = zzlb.Trim(',');

            int allRecordCount;
            List<IDataItem> list = this.GetControlValue();
            //WebCommon.AddDataItem(list, "zzjgdm", this.WorkUser.LoginName);

            if (!string.IsNullOrEmpty(ssdq))
            {
                WebCommon.AddDataItem(list, "qysd", ssdq);
            }

            if (!string.IsNullOrEmpty(zzlb))
            {
                WebCommon.AddDataItem(list, "zzlb", zzlb);
            }


            DataTable dt = new DataTable();

            dt = xytxBll.RetrieveQyxykp(list, this.gridView.PageSize, pageIndex, "kpnd desc", out allRecordCount).Result;

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

