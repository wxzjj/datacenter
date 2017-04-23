using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using Bigdesk8;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;

namespace Bigdesk8.Business.UserRightManager.RoleRight
{
    public partial class SetRoleRight : SystemBasePage
    {
        private readonly IUserRightManager userManager = new UserRightManager();
        private int roleID;
        private int systemID;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.roleID = this.Request.QueryString["RoleID"].ToInt32();
            this.systemID = this.Request.QueryString["SystemID"].ToInt32();

            if (!this.IsPostBack)
            {
                RoleModel model = userManager.ReadRole(this.roleID);
                List<IDataItem> list = model.ToDataItem();
                this.SetControlValue(list);

                SystemModel model2 = userManager.ReadSystem(this.systemID);
                List<IDataItem> list2 = model2.ToDataItem();
                this.SetControlValue(list2);

                this.searchData(0);
            }
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            this.searchData(0);
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.searchData(e.NewPageIndex);
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in this.gridView.Rows)
            {
                bool bSelect = ((CheckBox)gvr.FindControl("cb_select")).Checked;
                int moduleID = ((Label)gvr.FindControl("lbl_ModuleID")).Text.ToInt32();
                int operateID = ((Label)gvr.FindControl("lbl_OperateID")).Text.ToInt32();
                int dataTypeID = ((Label)gvr.FindControl("lbl_DataTypeID")).Text.ToInt32();

                if (bSelect)
                {
                    userManager.SaveRoleRight(this.roleID, this.systemID, moduleID, operateID, dataTypeID);
                }
                else
                {
                    userManager.DeleteRoleRight(this.roleID, this.systemID, moduleID, operateID, dataTypeID);
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
            DataTable dt = userManager.SearchRoleRight(this.roleID, this.systemID, list, this.gridView.PageSize, pageIndex, out allRecordCount);

            this.gridView.RecordCount = allRecordCount;
            this.gridView.PageIndex = pageIndex;
            this.gridView.DataSource = dt;
            this.gridView.DataBind();
        }
    }
}
