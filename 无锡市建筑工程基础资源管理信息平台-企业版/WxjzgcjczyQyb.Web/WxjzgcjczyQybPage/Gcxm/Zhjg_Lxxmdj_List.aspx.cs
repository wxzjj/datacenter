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

namespace WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Gcxm
{
    public partial class Zhjg_Lxxmdj_List : BasePage
    {
        public GcxmBLL gcxmBll;
        private AppUser WorkUser = new AppUser();

        protected void Page_Load(object sender, EventArgs e)
        {
            gcxmBll = new GcxmBLL(this.WorkUser);
            if (!IsPostBack)
            {
                this.pl_gjss.Visible = false;
                //WebCommon.DropDownListDataBind(this.ddl_lxjb, true);
                WebCommon.DropDownListDataBind(this.ddl_Xmfl, true);
                //WebCommon.DropDownListDataBind(this.ddl_jsxz, true);
                WebCommon.CheckBoxListDataBind(this.cbl_ssdq);

                //数据绑定  
                SearData(0);
            }
        }


        public void SearData(int pageIndex)
        {
            string ssdq = "";
            foreach (ListItem item in this.cbl_ssdq.Items)
            {
                if (item.Selected)
                    ssdq += item.Value + ",";
            }
            ssdq = ssdq.Trim(',');
            int allRecordCount;
            List<IDataItem> list = this.GetControlValue();

            //if (this.WorkUser.UserType == UserType.建设单位)
            //{
            //    WebCommon.AddDataItem(list, "BuildCorpCode", this.WorkUser.LoginName);
            //}
            //if (this.WorkUser.UserType == UserType.施工单位)
            //{
            //    this.WindowLocation("../Msg.aspx");
            //    return;
            //}

            DataTable dt = gcxmBll.GetXmxx(list, ssdq, this.gridView.PageSize, "CreateDate desc", pageIndex, out allRecordCount).Result;
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

        protected void linkButton_Click(object sender, EventArgs e)
        {
            this.pl_gjss.Visible = !this.pl_gjss.Visible;
            if (this.pl_gjss.Visible)
            {
                linkButton1.Text = "关闭高级搜索";
            }
            else
            {
                linkButton1.Text = "打开高级搜索";
                foreach (Control c in this.pl_gjss.Controls)
                {
                    if (c is DropDownList)
                    {
                        DropDownList ddl = c as DropDownList;
                        ddl.SelectedIndex = 0;
                    }
                    if (c is TextBox)
                    {
                        TextBox txt = c as TextBox;
                        txt.Text = "";
                    }
                }
            }
        }

        protected void Button_Search_Click(object sender, EventArgs e)
        {
            SearData(0);
        }
    }
}
