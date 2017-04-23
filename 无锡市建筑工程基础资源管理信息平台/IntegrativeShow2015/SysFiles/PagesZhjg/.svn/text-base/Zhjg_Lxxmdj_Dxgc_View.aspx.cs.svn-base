using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;
using System.Data;
using Bigdesk8.Web;
using IntegrativeShow2;

namespace IntegrativeShow2.SysFiles.PagesZhjg
{
    public partial class Zhjg_Lxxmdj_Dxgc_View : BasePageGuanli
    {
        #region 基本配置
        // 常量
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
            dtPageData = ihb.SearchInfo(InstanceName.Instance_Read_LxxmDxgcInfo.ToString(), arrParas);
            if (!IsPostBack)
            {
                //数据绑定
                if (dtPageData.Rows.Count > 0)
                {
                    //dtPageData.Rows[0].SetControlValue(this);
                    this.SetControlValue(dtPageData.Rows[0].ToDataItem());
                }
            }
        }
        #endregion 
    }
}
