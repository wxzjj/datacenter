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
using Bigdesk8;

namespace IntegrativeShow2.SysFiles.PagesZhjg
{
    public partial class Zhjg_Aqbj_View : BasePageGuanli
    {
        #region 基本配置
        // 常量
        protected const string InstanceHead = "Instance_";
        public string publicViewUrl = DataDefine.publicViewUrlZHJG;
        //public string LoginID = "2";
        //变量
        protected string PKID;
        protected string[] arrParas;
        DataTable dtPageData;
        GridView gv;
        //接口
        protected IHandleBusiness ihb;
        #endregion

        #region 控件相关方法
        protected void Page_Load(object sender, EventArgs e)
        {
            //实例化
            PKID = Request.QueryString["PKID"];
            ihb = new HandleBusinessBase();
            arrParas = new string[]{PKID};
            gv = this.Gdv_SgxkCyryInfo;
            dtPageData = ihb.SearchInfo(InstanceName.Instance_Read_AqbjInfo.ToString(),arrParas);
            if (!IsPostBack)
            {
                if (dtPageData.Rows.Count > 0)
                {
                    arrParas = new string[] { dtPageData.Rows[0]["aqjdbm"].ToString() };
                    //查找相关数据并赋值给GridView数据源
                    gv.DataSource = ihb.SearchInfo(InstanceHead + gv.ID, arrParas);
                    gv.DataBind();
                    //数据绑定
                    if (dtPageData.Rows.Count > 0)
                    {
                        this.SetControlValue(dtPageData.Rows[0].ToDataItem());
                        this.hlk_dwmc.Text = "";
                        if (string.IsNullOrEmpty(dtPageData.Rows[0]["qyID"].ToString2()))
                        {
                            this.hlk_dwmc.Enabled=false;
                        }
                        else
                        {
                        this.hlk_dwmc.NavigateUrl = string.Format("/WxjzgcjczyPage/Szqy/QyxxToolBar.aspx?qyid={0}", dtPageData.Rows[0]["qyID"]);
                        }
                    }
                }
            }
        }
        #endregion 
    }
}
