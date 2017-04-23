using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using Bigdesk8;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;

namespace Bigdesk8.Business.UserRightManager.UserRole
{
    public partial class SetUserRole : SystemBasePage
    {
        private readonly IUserRightManager userManager = new UserRightManager();
        private int userID;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.userID = this.Request.QueryString["UserID"].ToInt32();

            if (!this.IsPostBack)
            {
                UserModel model = userManager.ReadUser(this.userID);
                List<IDataItem> list = model.ToDataItem();
                this.SetControlValue(list);

                this.searchData(0);
            }
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            this.searchData(0);
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            int roleID = ((Label)e.Row.FindControl("lbl_RoleID")).Text.ToInt32();
            CheckBox cb_select = (CheckBox)e.Row.FindControl("cb_select");

            cb_select.Checked = userManager.UserRoleExists(this.userID, roleID);
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.searchData(e.NewPageIndex);
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in this.gridView.Rows)
            {
                int roleID = ((Label)gvr.FindControl("lbl_RoleID")).Text.ToInt32();
                bool bSelect = ((CheckBox)gvr.FindControl("cb_select")).Checked;

                if (bSelect)
                {
                    userManager.SaveUserRole(this.userID, roleID);
                }
                else
                {
                    userManager.DeleteUserRole(this.userID, roleID);
                }
            }

            this.WindowAlert("设置成功！");
        }

        private void searchData(int pageIndex)
        {
            string result = this.CheckControlValue();
            if (!result.IsEmpty())
            {
                this.WindowAlert(result, false);
                return;
            }

            List<IDataItem> list = this.GetControlValue();

            int allRecordCount;
            DataTable dt = userManager.SearchRole(list, this.gridView.PageSize, pageIndex, out allRecordCount);

            this.gridView.RecordCount = allRecordCount;
            this.gridView.PageIndex = pageIndex;
            this.gridView.DataSource = dt;
            this.gridView.DataBind();
        }
    }
}
