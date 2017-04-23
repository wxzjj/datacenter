using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Wxjzgcjczy.BLL;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;



namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Yhgl
{
    public partial class Yhxx_Edit : BasePage
    {
        public string Operate;
        public string Id;
        public YhglBLL BLL;

        protected void Page_Load(object sender, EventArgs e)
        {
            Operate=Request.QueryString["operate"];
            Id=Request.QueryString["id"];
            BLL = new YhglBLL(WorkUser);
            if (!this.IsPostBack)
            {
                if (Operate.Equals("edit"))
                {
                    DataTable dt = BLL.ReadUser(Id).Result;
                    if (dt.Rows.Count > 0)
                    {
                       this.SetControlValue( dt.Rows[0].ToDataItem());
                    }
                }
               
            }

        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            string msg = this.CheckControlValue();
            if (!string.IsNullOrEmpty(msg))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "showWarn('" + msg + "');", true);
                return;
            }

            List<IDataItem> list = this.GetControlValue();
            if (Operate.Equals("edit"))
            { 
                if (BLL.Edit_User(Id, list))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "f_SaveResult('用户信息修改成功！');", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "showError('用户信息修改成功！');", true);
                    return;
                }
            }
            else
            {
                if (BLL.Add_User(list))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "f_SaveResult('用户信息创建成功！');", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "showError('用户信息创建成功！');", true);
                    return;
                }
            }
        }
    }
}
