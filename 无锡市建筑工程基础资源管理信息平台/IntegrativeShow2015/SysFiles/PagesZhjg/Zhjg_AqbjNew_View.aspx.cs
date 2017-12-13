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
    public partial class Zhjg_AqbjNew_View : BasePageGuanli
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
        GridView gvClqd;
        GridView gvHjssjd;
        GridView gvCgmgcqd;
        GridView gvWxyjdgcqd;
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

            gvHt = this.Gdv_AqbjNew_ht;
            gvDwry = this.Gdv_AqbjNew_dwry;
            gvClqd = this.Gdv_AqbjNew_clqd;
            gvHjssjd = this.Gdv_AqbjNew_hjssjd;
            gvWxyjdgcqd = this.Gdv_AqbjNew_wxyjdgcqd;
            gvCgmgcqd = this.Gdv_AqbjNew_cgmgcqd;
            gvSprz = this.Gdv_AqbjNew_sprz;

            dtPageData = ihb.SearchInfo(InstanceName.Instance_Read_AqbjNewInfo.ToString(),arrParas);
            if (!IsPostBack)
            {
                if (dtPageData.Rows.Count > 0)
                {
                    arrParas = new string[] { dtPageData.Rows[0]["uuid"].ToString() };
                    //查找合同数据并赋值给合同gridview
                    gvHt.DataSource = ihb.SearchInfo(InstanceHead + gvHt.ID, arrParas);
                    gvHt.DataBind();

                    //查找单位人员数据并赋值给单位人员gridview
                    gvDwry.DataSource = ihb.SearchInfo(InstanceHead + gvDwry.ID, arrParas);
                    gvDwry.DataBind();

                    //材料清单
                    gvClqd.DataSource = ihb.SearchInfo(InstanceHead + gvClqd.ID, arrParas);
                    gvClqd.DataBind();

                    //环境及地下设施交底项目
                    gvHjssjd.DataSource = ihb.SearchInfo(InstanceHead + gvHjssjd.ID, arrParas);
                    gvHjssjd.DataBind();

                    //危险源较大工程清单
                    gvWxyjdgcqd.DataSource = ihb.SearchInfo(InstanceHead + gvWxyjdgcqd.ID, arrParas);
                    gvWxyjdgcqd.DataBind();

                    //超大规模危险源工程清单
                    gvCgmgcqd.DataSource = ihb.SearchInfo(InstanceHead + gvCgmgcqd.ID, arrParas);
                    gvCgmgcqd.DataBind();

                    //审批日志列表
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
