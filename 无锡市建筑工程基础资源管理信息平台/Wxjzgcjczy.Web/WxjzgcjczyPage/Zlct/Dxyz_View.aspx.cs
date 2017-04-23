using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wxjzgcjczy.BLL;
using System.Data;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;
using Bigdesk8;


namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Zlct
{
    public partial class Dxyz_View : BasePage
    {
        ZlctBLL BLL;
        protected string DxjbId;
        protected string PageIndex;
        protected void Page_Load(object sender, EventArgs e)
        {
            DxjbId =Request.QueryString["dxjbId"];

            BLL = new ZlctBLL(this.WorkUser);
            if (!this.IsPostBack)
            {
                PageIndex=Request.QueryString["pageIndex"];
                this.lblPageIndex.Text= PageIndex;
                DataTable dt = BLL.ReadSzgkjc_Dxjb(DxjbId).Result;
                List<IDataItem> list = dt.Rows[0].ToDataItem();
                this.SetControlValue(list);

                bool isDsfs = bool.Parse(dt.Rows[0]["IsDsfs"].ToString());
                if (isDsfs)
                    this.db_fslx.Text = "定时发送";
                else
                    this.db_fslx.Text = "手动发送";

                int month=0,week=0,quarter=0;
                week = int.Parse(dt.Rows[0]["EveryWeekOne"].ToString2());
                month = int.Parse(dt.Rows[0]["EveryMonthOne"].ToString2());
                quarter = int.Parse(dt.Rows[0]["EveryQuarterOne"].ToString2());

                if (week > 0)
                    this.db_fszq.Text = "每周发送一次，";
                if(month>0)
                    this.db_fszq.Text += "每月发送一次，";
                if (quarter > 0)
                    this.db_fszq.Text += "每季度发送一次";
                this.db_fszq.Text = this.db_fszq.Text.TrimEnd('，');
                string jbnr = list.GetDataItem("Jbnr").ItemData;
                string jbnrNew = string.Copy(jbnr);
                List<SqlPara> sqls = WebCommon.ConvertSql(jbnr);
                if (sqls.Count > 0)
                {
                    foreach (SqlPara item in sqls)
                    {
                        string str = BLL.ExecuteSql(item.executeSql);
                        item.newStr = str;
                        jbnrNew = jbnrNew.Replace(item.oldStr, item.newStr);
                    }
                }
                this.db_Jbnr.Text = jbnrNew;
                dt = BLL.ReadSzgkjc_DxjbAndSjml(DxjbId).Result;
                if (dt.Rows.Count > 0)
                {
                    string txt = ",", txt2 = "";

                    foreach (DataRow item in dt.Rows)
                    {
                        txt += item["SjmlID"] + ",";
                        txt2 += item["SjmlName"] + "【" + item["sjmlMobile"] + "】,";
                    }
                    this.db_fsdx.Text = txt2.Trim(new char[] { ',' });
                }

            }
        }
    }
}
