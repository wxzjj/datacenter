using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bigdesk8.Data;
using Bigdesk8.Web.Controls;
using Bigdesk8.Web;
using Bigdesk8;
using Wxjzgcjczy.BLL;
using System.Data;
namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Zlct
{
    public partial class Gzzs_Gzzs_Edit : BasePage
    {
        ZlctBLL BLL;
        protected string Src;
        protected string GzzsId;
        protected string Operate;

        protected void Page_Load(object sender, EventArgs e)
        {
            Operate = Request.QueryString["operate"];
            GzzsId = Request.QueryString["gzzsId"];
            Src = "Txl_Tree.aspx?type=gzzs&gzzsId=" + GzzsId;
            BLL = new ZlctBLL(this.WorkUser);

            if (!this.IsPostBack)
            {
                //if (Operate == "edit")
                //{
                //    DataTable dt = BLL.ReadSzgkjc_Gzzs(GzzsId).Result;
                //    if (dt.Rows.Count > 0)
                //    {
                //        List<IDataItem> list = dt.Rows[0].ToDataItem();
                //        this.SetControlValue(list);
                //    }
                //    dt = BLL.RetrieveZshfListByGzzsId(GzzsId).Result;
                //    if (dt.Rows.Count > 0)
                //    {
                //        string hfrIds = ",";
                //        foreach (DataRow row in dt.Rows)
                //        {
                //            hfrIds += row["ZshfrId"] + ",";
                //        }
                //        this.txtReceiverIds.Text = hfrIds;
                //    }

                //}
                //else
                //{ 
                this.txtReceiverIds.Text = ",";
                //this.db_ZsrName.Text = this.WorkUser.UserName;
                //this.dp_Zssj.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm");
                //}
            }
        }


        protected void btn_send_Click(object sender, EventArgs e)
        {
            string msg = this.CheckControlValue();
            if (!string.IsNullOrEmpty(msg))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "showMessage('" + msg + "');", true);
                return;
            }

            List<IDataItem> list = this.GetControlValue();
            WebCommon.AddDataItem(list, "Sxtxx", this.txtReceiverIds.Text);

            FunctionResult<string> result = BLL.SendGzzs(list);
            if (result.Status == FunctionResultStatus.Error)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "showError('" + result.Result + "');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "SaveResult('" + result.Result + "');", true);

        }


        protected void btn_refresh_Click(object sender, EventArgs e)
        {
            string txt = "";

            string sxrIds = this.txtReceiverIds.Text.Trim(new char[] { ',' });
            if (string.IsNullOrEmpty(sxrIds))
            {
                this.db_fsdx.Text = "";
                return;
            }

            DataTable dt = BLL.RetrieveSjml_List(sxrIds).Result;
            foreach (string item in sxrIds.Split(','))
            {
                DataRow[] rows = dt.Select("SjmlID=" + item);
                if (rows.Length > 0)
                {
                    txt += rows[0]["SjmlName"] + "【" + rows[0]["sjmlMobile"] + "】,";
                }
            }
            this.db_fsdx.Text = txt.Trim(new char[] { ',' });

        }

    }
}
