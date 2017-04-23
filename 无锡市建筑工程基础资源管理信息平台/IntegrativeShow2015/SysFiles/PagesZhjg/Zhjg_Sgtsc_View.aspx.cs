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
    public partial class Zhjg_Sgtsc_View : BasePageGuanli
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
            gv = this.Gdv_SgtscRyInfo;
            PKID = Request.QueryString["PKID"];
            ihb = new HandleBusinessBase();
            arrParas = new string[] { PKID };
            dtPageData = ihb.SearchInfo(InstanceName.Instance_Read_SgtscInfo.ToString(), arrParas);
            if (!IsPostBack)
            {
                arrParas = new string[] { dtPageData.Rows[0]["CensorNum"].ToString() };
                //查找相关数据并赋值给GridView数据源
                gv.DataSource = ihb.SearchInfo(InstanceHead + gv.ID, arrParas);
                gv.DataBind();
                //数据绑定
                if (dtPageData.Rows.Count > 0)
                {
                    //dtPageData.Rows[0].SetControlValue(this);
                    this.SetControlValue(dtPageData.Rows[0].ToDataItem());
                    this.hlk_EconCorpName.Text = dtPageData.Rows[0]["EconCorpName"].ToString();
                    if (string.IsNullOrEmpty(dtPageData.Rows[0]["kcqyID"].ToString2()))
                    {
                        this.hlk_EconCorpName.Enabled = false;
                    }
                    else
                    {
                        this.hlk_EconCorpName.NavigateUrl = string.Format("/WxjzgcjczyPage/Szqy/QyxxToolBar.aspx?qyid={0}", dtPageData.Rows[0]["kcqyID"]);
                    }


                    hlk_DesignCorpName.Text = dtPageData.Rows[0]["DesignCorpName"].ToString();
                    if (string.IsNullOrEmpty(dtPageData.Rows[0]["sjqyID"].ToString2()))
                    {
                        this.hlk_DesignCorpName.Enabled = false;
                    }
                    else
                    {
                        this.hlk_DesignCorpName.NavigateUrl = string.Format("/WxjzgcjczyPage/Szqy/QyxxToolBar.aspx?qyid={0}", dtPageData.Rows[0]["sjqyID"]);
                    }
                 }
            }
        }
        #endregion 
    }
}
