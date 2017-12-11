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
    public partial class Zhjg_Lxxmdj_Menu : BasePageGuanli
    {
        #region 基本配置
        // 常量   
        public string publicViewUrl = DataDefine.publicViewUrlZHJG;
        public string iframe_url = ""; //用于标签打开新页面URL
        //public string LoginID = "2";
        //变量
        protected string[] arrParas;
        protected string PKID;
        protected string PrjNum;
        #endregion

        #region 相关控件事件
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 变量初始化
            PKID = Request.QueryString["PKID"];
            PrjNum = Request.QueryString["PrjNum"]; ;
            arrParas = new string[] { PKID, PrjNum };
            iframe_url = string.Format("Zhjg_Lxxmdj_View.aspx?LoginID={0}&PKID={1}", this.WorkUser.UserID, arrParas[0]);
            #endregion
            //配置菜单
            if (!this.IsPostBack)
            {
                string menu = this.Request.QueryString["menu"];
                foreach (MenuItem mi in this.myMenu.Items)
                {
                    if (mi.Value != menu) continue;
                    mi.Selected = true;
                    break;
                }
            }
        }

        protected void myMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            switch (this.myMenu.SelectedValue)
            {
                default:
                case "0"://项目基本信息
                    this.iframe_url = string.Format("Zhjg_Lxxmdj_View.aspx?LoginID={0}&PKID={1}", this.WorkUser.UserID, arrParas[0]);
                    break;
                case "1"://招投标信息
                    this.iframe_url = string.Format("Zhjg_Zbtb_List.aspx?LoginID={0}&PrjNum={1}&BeFrom={2}", this.WorkUser.UserID, arrParas[1], BeFrom.Zhjg_Lxxmdj_Menu);
                    break;
                case "2"://合同备案信息  
                    this.iframe_url = string.Format("Zhjg_Htba_List.aspx?LoginID={0}&PrjNum={1}&BeFrom={2}", this.WorkUser.UserID, arrParas[1], BeFrom.Zhjg_Lxxmdj_Menu);
                    break;
                case "3"://安全报监信息
                    this.iframe_url = string.Format("Zhjg_Aqbj_List.aspx?LoginID={0}&PrjNum={1}&BeFrom={2}", this.WorkUser.UserID, arrParas[1], BeFrom.Zhjg_Lxxmdj_Menu);
                    break;
                case "4"://质量报监信息
                    this.iframe_url = string.Format("Zhjg_Zlbj_List.aspx?LoginID={0}&PrjNum={1}&BeFrom={2}", this.WorkUser.UserID, arrParas[1], BeFrom.Zhjg_Lxxmdj_Menu);
                    break;
                case "5"://施工许可信息
                    this.iframe_url = string.Format("Zhjg_Sgxkz_List.aspx?LoginID={0}&PrjNum={1}&BeFrom={2}", this.WorkUser.UserID, arrParas[1], BeFrom.Zhjg_Lxxmdj_Menu);
                    break;
                case "6"://施工图审查信息
                    this.iframe_url = string.Format("Zhjg_Sgtsc_List.aspx?LoginID={0}&PrjNum={1}&BeFrom={2}", this.WorkUser.UserID, arrParas[1], BeFrom.Zhjg_Lxxmdj_Menu);
                    break;
                case "7"://竣工验收备案信息
                    this.iframe_url = string.Format("Zhjg_Jgba_List.aspx?LoginID={0}&PrjNum={1}&BeFrom={2}", this.WorkUser.UserID, arrParas[1], BeFrom.Zhjg_Lxxmdj_Menu);
                    break;
                case "13"://安全报监信息（新）
                    this.iframe_url = string.Format("Zhjg_AqbjNew_List.aspx?LoginID={0}&PrjNum={1}&BeFrom={2}", this.WorkUser.UserID, arrParas[1], BeFrom.Zhjg_Lxxmdj_Menu);
                    break;
                case "14"://质量报监信息（新）
                    this.iframe_url = string.Format("Zhjg_ZlbjNew_List.aspx?LoginID={0}&PrjNum={1}&BeFrom={2}", this.WorkUser.UserID, arrParas[1], BeFrom.Zhjg_Lxxmdj_Menu);
                    break;
            }
        }
        #endregion
    }
}
