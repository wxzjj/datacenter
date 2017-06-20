using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bigdesk8.Data;
using Bigdesk8.Web;
using System.Data;
using Bigdesk8.Web.Controls;
using IntegrativeShow2;

using IntegrativeShow2.Common;

namespace IntegrativeShow2.SysFiles.PagesZhjg
{
    public partial class Zhjg_Lxxmdj_List : BasePageGuanli
    {

        #region 基本配置
        // 常量   
        protected const string InstanceHead = "Instance_";
        public string publicViewUrl = DataDefine.publicViewUrlZHJG;
        //public string LoginID = "2";
        //变量
        protected string[] arrParas;
        //接口
        protected IHandleBusiness ihb;        
        #endregion

        #region 控件相关方法
        protected void Page_Load(object sender, EventArgs e)
        {   

            //实例化
            ihb = new HandleBusinessBase();
            if (!IsPostBack)
            {
                this.holder_gjcx.Visible = false;
                WebCommon.DropDownListDataBind(this.ddl_lxjb, true);
                WebCommon.DropDownListDataBind(this.ddl_Xmfl,true);
                WebCommon.DropDownListDataBind(this.ddl_jsxz, true);
                //WebCommon.DropDownListDataBind(this.ddl_ssdq, true);
                WebCommon.CheckBoxListDataBind(this.cbl_ssdq);
            
                //数据绑定  
                powerGridViewDataBind(0);
            }
        }

        //数据绑定
        private void powerGridViewDataBind(int pageIndex)
        {
            Gdv_LxxmInfo.PageIndex = pageIndex;
            string ssdq="";
            foreach (ListItem item in this.cbl_ssdq.Items)
            {
                if (item.Selected)
                {
                    ssdq += item.Value + ",";

                    if (string.Equals(item.Value, "320213"))
                    {
                        ssdq += "320202,320203,320204,";
                    }
                }
                    
            }

            //Gdv_LxxmInfo.DataSource = ihb.SearchInfo(InstanceHead + this
            //    .Gdv_LxxmInfo.ID, new string[] { this.ddl_Heba.SelectedValue, ssdq.TrimEnd(',') }, this);
            //Gdv_LxxmInfo.DataBind();
            ihb.SearchInfo(InstanceHead + this.Gdv_LxxmInfo.ID, new string[] { this.ddl_Heba.SelectedValue, ssdq.TrimEnd(',') }, this,"Gdv_LxxmInfo", pageIndex, 20);

        }

        //分页
        protected void powerGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.powerGridViewDataBind(e.NewPageIndex);
        }

        protected void powerGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }

        protected void ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            powerGridViewDataBind(0);
        }
        #endregion 

        protected void linkButton_Click(object sender, EventArgs e)
        {
            this.holder_gjcx.Visible = !this.holder_gjcx.Visible;
            if (this.holder_gjcx.Visible)
            {
                linkButton.Text = "关闭高级搜索";
            }
            else
            {
                linkButton.Text = "打开高级搜索";
                foreach (Control  c in this.holder_gjcx.Controls)
                {
                    if (c is DropDownList)
                    {
                         DropDownList ddl= c as DropDownList;
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
    }
}
