using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bigdesk8.Data;
using Bigdesk8.Web;
using System.Data;
using Bigdesk8.Web;
using IntegrativeShow2;

namespace IntegrativeShow2.SysFiles.PagesZhjg
{
    public partial class Zhjg_AqbjNew_List : BasePageGuanli
    {
        #region 基本配置
        //常量   
        protected const string InstanceHead = "Instance_";
        public string publicViewUrl = DataDefine.publicViewUrlZHJG;
        //public string LoginID = "2";
        //变量
        protected string[] arrParas;
        //protected GridView gv;
        private string BeFrom;
        private bool bIsBeFrom = false;
        //接口
        protected IHandleBusiness ihb;
        #endregion

        #region 控件相关方法
        protected void Page_Load(object sender, EventArgs e)
        {
            //实例化
            ihb = new HandleBusinessBase();
            //gv = this.Gdv_AqbjInfo;
            BeFrom = Request.QueryString["BeFrom"];
            if (!string.IsNullOrEmpty(BeFrom) && BeFrom == IntegrativeShow2.BeFrom.Zhjg_Lxxmdj_Menu.ToString())
            {
                bIsBeFrom = true;
                arrParas = new string[] { Request.QueryString["PrjNum"] };

            }
            if (!IsPostBack)
            {
                //数据绑定  
                powerGridViewDataBind(0);
                WebCommon.DropDownListDataBind(this.DDL_xmsd, true);
            }
        }

        //数据绑定
        private void powerGridViewDataBind(int pageIndex)
        {
            if (bIsBeFrom)
            {
                ihb.SearchInfo(InstanceHead + this.Gdv_AqbjNewInfo.ID, arrParas, this, "Gdv_AqbjNewInfo", pageIndex, 20);
            }
            else
            {
                ihb.SearchInfo(InstanceHead + this.Gdv_AqbjNewInfo.ID, null, this, "Gdv_AqbjNewInfo", pageIndex, 20);
            }
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
    }
}