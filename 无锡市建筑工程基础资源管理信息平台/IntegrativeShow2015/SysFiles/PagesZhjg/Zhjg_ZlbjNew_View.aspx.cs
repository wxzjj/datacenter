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
    public partial class Zhjg_ZlbjNew_View : BasePageGuanli
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
        GridView gvHt;
        GridView gvDwry;
        GridView gvSchgs;
        GridView gvDwgc;
        GridView gvClqd;
        GridView gvSprz;
       
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

            gvHt = this.Gdv_ZlbjNew_ht;
            gvDwry = this.Gdv_ZlbjNew_dwry;
            gvSchgs = this.Gdv_ZlbjNew_schgs;
            gvDwgc = this.Gdv_ZlbjNew_dwgc;
            gvClqd = this.Gdv_ZlbjNew_clqd;
            gvSprz = this.Gdv_ZlbjNew_sprz;

            dtPageData = ihb.SearchInfo(InstanceName.Instance_Read_ZlbjNewInfo.ToString(),arrParas);
            if (!IsPostBack)
            {
                if (dtPageData.Rows.Count > 0)
                {
                    arrParas = new string[] { dtPageData.Rows[0]["uuid"].ToString() };
                    //合同
                    gvHt.DataSource = ihb.SearchInfo(InstanceHead + gvHt.ID, arrParas);
                    gvHt.DataBind();

                    //单位人员
                    gvDwry.DataSource = ihb.SearchInfo(InstanceHead + gvDwry.ID, arrParas);
                    gvDwry.DataBind();

                    //施工图审查合格书
                    gvSchgs.DataSource = ihb.SearchInfo(InstanceHead + gvSchgs.ID, arrParas);
                    gvSchgs.DataBind();

                    //单位工程
                    gvDwgc.DataSource = ihb.SearchInfo(InstanceHead + gvDwgc.ID, arrParas);
                    gvDwgc.DataBind();

                    //材料清单
                    gvClqd.DataSource = ihb.SearchInfo(InstanceHead + gvClqd.ID, arrParas);
                    gvClqd.DataBind();

                    //审批日志
                    gvSprz.DataSource = ihb.SearchInfo(InstanceHead + gvSprz.ID, arrParas);
                    gvSprz.DataBind();

                     
                    //数据绑定
                    if (dtPageData.Rows.Count > 0)
                    {
                        this.SetControlValue(dtPageData.Rows[0].ToDataItem());
                         
                    }
                }
            }
        }
        #endregion 
    }
}
