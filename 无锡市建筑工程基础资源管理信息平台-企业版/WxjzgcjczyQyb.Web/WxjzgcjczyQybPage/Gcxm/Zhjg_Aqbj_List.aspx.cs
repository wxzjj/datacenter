﻿using System;
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
    public partial class Zhjg_Aqbj_List : BasePage
    {
        private string BeFrom;
        private bool bIsBeFrom = false;
        private string PrjNum;
        public GcxmBLL gcxmBll;
        private AppUser WorkUser = new AppUser();

        protected void Page_Load(object sender, EventArgs e)
        {
            gcxmBll = new GcxmBLL(this.WorkUser);
            BeFrom = Request.QueryString["BeFrom"];
            if (!string.IsNullOrEmpty(BeFrom) && BeFrom == WxjzgcjczyQyb.BeFrom.Zhjg_Lxxmdj_Menu.ToString())
            {
                bIsBeFrom = true;
                PrjNum = Request.QueryString["PrjNum"];
            }
            if (!IsPostBack)
            {
                WebCommon.DropDownListDataBind(this.DDL_xmsd, true);
                //数据绑定  
                SearData(0);
            }
        }

        //数据绑定
        public void SearData(int pageIndex)
        {
            int allRecordCount;
            List<IDataItem> list = this.GetControlValue();

            //if (this.WorkUser.UserType == UserType.建设单位)
            //{
            //    WebCommon.AddDataItem(list, "BuildCorpCode", this.WorkUser.LoginName);
            //}
            //if (this.WorkUser.UserType == UserType.施工单位)
            //{
            //    WebCommon.AddDataItem(list, "zbdwDwdm", this.WorkUser.LoginName);
            //}

            DataTable dt = new DataTable();
            if (bIsBeFrom)
            {
                WebCommon.AddDataItem(list, "xmbm", PrjNum);
                dt = gcxmBll.GetAqbjxx(list, this.gridView.PageSize, " bjrq desc ", pageIndex, out allRecordCount).Result;
            }
            else
            {
                dt = gcxmBll.GetAqbjxx(list, this.gridView.PageSize, " bjrq desc ", pageIndex, out allRecordCount).Result;
            }
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

