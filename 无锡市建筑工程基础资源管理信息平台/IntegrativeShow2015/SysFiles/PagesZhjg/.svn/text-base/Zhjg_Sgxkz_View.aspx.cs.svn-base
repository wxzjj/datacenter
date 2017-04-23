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
    public partial class Zhjg_Sgxkz_View : BasePageGuanli
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
            gv = this.Gdv_SgxkCyryInfo;
            PKID = Request.QueryString["PKID"];
            ihb = new HandleBusinessBase();
            arrParas = new string[] { PKID };
            dtPageData = ihb.SearchInfo(InstanceName.Instance_Read_SgxkzInfo.ToString(), arrParas);
            if (!IsPostBack)
            {
                //查找相关数据并赋值给GridView数据源
                gv.DataSource = ihb.SearchInfo(InstanceHead + gv.ID, arrParas);
                gv.DataBind();
                //数据绑定
                if (dtPageData.Rows.Count > 0)
                {
                    //dtPageData.Rows[0].SetControlValue(this);
                    this.SetControlValue(dtPageData.Rows[0].ToDataItem());
                    this.hlk_EconCorpName.Text = dtPageData.Rows[0]["EconCorpName"].ToString();
                    if (string.IsNullOrEmpty(dtPageData.Rows[0]["kcqyID"].ToString()))
                    {
                        this.hlk_EconCorpName.Enabled = false;
                    }
                    else
                    {
                        this.hlk_EconCorpName.NavigateUrl = string.Format("/WxjzgcjczyPage/Szqy/QyxxToolBar.aspx?qyid={0}", dtPageData.Rows[0]["kcqyID"]);
                    }

                    this.hlk_DesignCorpName.Text = dtPageData.Rows[0]["DesignCorpName"].ToString();
                    if (string.IsNullOrEmpty(dtPageData.Rows[0]["sjqyID"].ToString()))
                    {
                        this.hlk_DesignCorpName.Enabled = false;
                    }
                    else
                    {
                        this.hlk_DesignCorpName.NavigateUrl = string.Format("/WxjzgcjczyPage/Szqy/QyxxToolBar.aspx?qyid={0}", dtPageData.Rows[0]["sjqyID"]);
                    }

                    this.hlk_ConsCorpName.Text = dtPageData.Rows[0]["ConsCorpName"].ToString();
                    if (string.IsNullOrEmpty(dtPageData.Rows[0]["sgqyID"].ToString()))
                    {
                        this.hlk_ConsCorpName.Enabled = false;
                    }
                    else
                    {
                        this.hlk_ConsCorpName.NavigateUrl = string.Format("/WxjzgcjczyPage/Szqy/QyxxToolBar.aspx?qyid={0}", dtPageData.Rows[0]["sgqyID"]);
                    }

                }
            }
        }
        #endregion 
    }
}
