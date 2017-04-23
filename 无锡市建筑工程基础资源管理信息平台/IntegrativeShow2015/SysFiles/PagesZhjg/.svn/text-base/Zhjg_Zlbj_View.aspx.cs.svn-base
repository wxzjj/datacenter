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

namespace IntegrativeShow2.SysFiles.PagesZhjg
{
    public partial class Zhjg_Zlbj_View : BasePageGuanli
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
        //接口
        protected IHandleBusiness ihb;
        #endregion

        #region 控件相关方法
        protected void Page_Load(object sender, EventArgs e)
        {
            //实例化
            PKID = Request.QueryString["PKID"];
            ihb = new HandleBusinessBase();
            arrParas = new string[] { PKID };
            if (!IsPostBack)
            {
                dtPageData = ihb.SearchInfo(InstanceName.Instance_Read_ZlbjInfo.ToString(), arrParas);
                //数据绑定
                if (dtPageData.Rows.Count > 0)
                {
                    this.SetControlValue(dtPageData.Rows[0].ToDataItem());

                    arrParas = new string[] { dtPageData.Rows[0]["zljdbm"].ToString() };
                    //查找相关数据并赋值给GridView数据源
                    this.Gdv_ZlbjZrryInfo.DataSource = ihb.SearchInfo(InstanceHead + this.Gdv_ZlbjZrryInfo.ID, arrParas);
                    this.Gdv_ZlbjZrryInfo.DataBind();

                }
            }
        }
        #endregion 
    }
}
