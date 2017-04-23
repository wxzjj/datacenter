using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wxjzgcjczy.Web.WxjzgcjczyPage.Common;
using Wxjzgcjczy.BLL;
using System.Data;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Gcxm
{

    public partial class Sgjlht_List : BasePage_Standard
    {
        public GcxmBLL BLL;
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL=  new GcxmBLL(this.WorkUser);
            if(!this.IsPostBack)
            {
                //数据绑定  
                WebCommon.DropDownListDataBind(this.DDL_Htlb, true);
                powerGridViewDataBind(0);
            }
          
        }
        public void powerGridViewDataBind(int page)
        {
            int allRecordCount=0;
            List<IDataItem> list=this.GetControlValue();
            DataTable dt = BLL.RetrieveSgjlht(list, this.Gdv_HtbaInfo.PageSize, page, out allRecordCount).Result;
            this.Gdv_HtbaInfo.RecordCount = allRecordCount;
            this.Gdv_HtbaInfo.DataSource = dt;
            this.Gdv_HtbaInfo.DataBind();
           
        }

        protected void powerGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

        protected void powerGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            powerGridViewDataBind(e.NewPageIndex);
        }

        protected void ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            powerGridViewDataBind(0);
        }
    }
}
