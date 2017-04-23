using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wxjzgcjczy.BLL;
using System.Data;
using Bigdesk8.Data;
using Bigdesk8.Web.Controls;
using Bigdesk8;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Zlct
{
    public partial class Gzzs_Gzfk_Edit :BasePage
    {
        protected ZlctBLL BLL;
        protected string ZshfId;
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL = new ZlctBLL(WorkUser);
            ZshfId = Request.QueryString["zshfId"];
            
            if (!this.IsPostBack)
            {
                DataTable dt= BLL.ReadSzgkjc_Gzhf(ZshfId).Result;
                if (dt.Rows.Count > 0)
                {
                    List<IDataItem> list = dt.Rows[0].ToDataItem();
                    this.SetControlValue(list);

                    string gzzsId = list.GetDataItem("GzzsId").ItemData;
                    dt = BLL.ReadSzgkjc_Gzzs(gzzsId).Result;
                    list = dt.Rows[0].ToDataItem();
                    this.SetControlValue(list);
                }
                BLL.UpdateZlhfToReadedState(ZshfId);
            }
        }
        /// <summary>
        /// 指令回复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_save_Click(object sender, EventArgs e)
        {
            string msg = this.CheckControlValue();
            if (!string.IsNullOrEmpty(msg))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "showWarn('" + msg + "');", true);
                return;
            }
            List<IDataItem> list = new List<IDataItem>();
            //WebCommon.AddDataItem(list, "ZshfrName", this.db_ZshfrName.Text);
            //WebCommon.AddDataItem(list, "Zshfsj", this.db_Zshfsj.Text);

            WebCommon.AddDataItem(list, "ZshfNr", this.db_ZshfNr.Text);
            FunctionResult<string> result= BLL.HfZlhfxx(ZshfId,list);
            if (result.Status == FunctionResultStatus.Error)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "showError('" + result.Message.Message + "');", true);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "SaveResult('" + result.Message.Message + "');", true);
            }

        }


       

    }
}
