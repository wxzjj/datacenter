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
using Bigdesk8.Security;

namespace IntegrativeShow2.SysFiles.PagesZhjg
{
    public partial class Zhjg_Htba_View : BasePageGuanli
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
            dtPageData = ihb.SearchInfo(InstanceName.Instance_Read_HtbaInfo.ToString(), arrParas);
            if (!IsPostBack)
            {
                //数据绑定
                if (dtPageData.Rows.Count > 0)
                {
                    //dtPageData.Rows[0].SetControlValue(this);
                    this.SetControlValue(dtPageData.Rows[0].ToDataItem());
                    this.hlk_PropietorCorpName.Text = dtPageData.Rows[0]["PropietorCorpName"].ToString();
                    if (string.IsNullOrEmpty(dtPageData.Rows[0]["jsdwID"].ToString()))
                    {
                        this.hlk_PropietorCorpName.Enabled = false;
                    }
                    else
                    {
                        this.hlk_PropietorCorpName.NavigateUrl = string.Format("/WxjzgcjczyPage/Szqy/JsdwxxToolBar.aspx?jsdwid={0}", dtPageData.Rows[0]["jsdwID"]);
                    }

                    this.hlk_ContractorCorpName.Text = dtPageData.Rows[0]["ContractorCorpName"].ToString();
                    if (string.IsNullOrEmpty(dtPageData.Rows[0]["qyID"].ToString()))
                    {
                        this.hlk_ContractorCorpName.Enabled = false;
                    }
                    else
                    {
                        this.hlk_ContractorCorpName.NavigateUrl = string.Format("/WxjzgcjczyPage/Szqy/QyxxToolBar.aspx?qyid={0}", dtPageData.Rows[0]["qyID"]);
                    }

                    this.hlk_PrjHead.Text = dtPageData.Rows[0]["PrjHead"].ToString();
                    if (dtPageData.Rows[0]["ContractTypeNum"].ToString().IndexOf("301,302,304") >= 0)
                    {
                        this.hlk_PrjHead.NavigateUrl = string.Format("/WxjzgcjczyPage/RyxxToolBar.aspx?ryid={0}&rylx=zczyry", dtPageData.Rows[0]["ryID"]);
                    }

                    if (dtPageData.Rows[0]["tag"].ToString() == "省一体化平台")
                    {
                        this.hl_htbaxxView.Enabled = true;
                        string htbabm = dtPageData.Rows[0]["RecordInnerNum"].ToString();
                        string key =  SecurityUtility.MD5(dtPageData.Rows[0]["RecordInnerNum"].ToString()+ "htba"+DateTime.Now.ToString("yyyyMMdd"));
                        this.hl_htbaxxView.NavigateUrl = string.Format("http://58.213.147.230:8089/Jsjzyxyglpt/faces/sghtba.jsp?htbabm={0}&key={1}",htbabm,key);
                    }
                    else
                    {
                        this.hl_htbaxxView.Enabled = false;
                    }

                }
            }
        }
        #endregion 
    }
}
