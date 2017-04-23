using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Wxjzgcjczy.BLL;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;
using Bigdesk8;
using Bigdesk8.Data;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Zlct
{
    public partial class Dxjb_Record_View : BasePage
    {
        protected ZlctBLL BLL;
        protected string Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            Id = Request.QueryString["id"];
            BLL = new ZlctBLL(this.WorkUser);
            if (!this.IsPostBack)
            {
                DataTable dt = BLL.ReadSzgkjc_Dxjb_Records(Id).Result;
                List<IDataItem> list = dt.Rows[0].ToDataItem();
                bool isZdfs = list.GetDataItem("IsDsfs").ItemData.ToBoolean();
                int week = list.GetDataItem("EveryWeekOne").ItemData.ToInt32();
                int month = list.GetDataItem("EveryMonthOne").ItemData.ToInt32();
                int quarter = list.GetDataItem("EveryQuarterOne").ItemData.ToInt32();
                string fslx = "";
                if (isZdfs)
                {
                    if (week > 0)
                        fslx += "自动发送，每周发送一次";
                    else
                        if (month > 0)
                            fslx += "自动发送，每月发送一次";
                        else
                            fslx += "自动发送，每季度发送一次";

                }
                else
                {
                    fslx = "手动发送";
                }
                this.db_fslx.Text = fslx;
                this.SetControlValue(list);
            }
        }
    }
}
