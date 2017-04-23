using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegrativeShow2.SysFiles.PagesZhjg
{
 
   public partial class Zhjg_Menu : BasePageGuanli
    {
        #region 基本配置
        // 常量   
        public string publicViewUrl = DataDefine.publicViewUrlZHJG;
        public string iframe_url = ""; //用于标签打开新页面URL
        //public string LoginID = "2";
        //变量
        protected string[] arrParas;
        protected string PKID="";
        protected string PrjNum="";
        #endregion

        #region 相关控件事件
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 变量初始化
         
            arrParas = new string[] { PKID, PrjNum };
            this.iframe_url = string.Format("Zhjg_Lxxmdj_List.aspx?LoginID={0}", this.WorkUser.UserID);
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
                    this.iframe_url = string.Format("Zhjg_Lxxmdj_List.aspx?LoginID={0}", this.WorkUser.UserID);
                    break;
                case "1"://招投标信息
                    this.iframe_url = string.Format("Zhjg_Zbtb_List.aspx?LoginID={0}&BeFrom={1}", this.WorkUser.UserID, BeFrom.Zhjg_Menu.ToString());
                    break;
                case "2"://合同备案信息  
                    this.iframe_url = string.Format("Zhjg_Htba_List.aspx?LoginID={0}&BeFrom={1}", this.WorkUser.UserID, BeFrom.Zhjg_Menu.ToString());
                    break;
                case "3"://安全报监信息
                    this.iframe_url = string.Format("Zhjg_Aqbj_List.aspx?LoginID={0}&BeFrom={1}", this.WorkUser.UserID, BeFrom.Zhjg_Menu.ToString());
                    break;
                case "4"://质量报监信息
                    this.iframe_url = string.Format("Zhjg_Zlbj_List.aspx?LoginID={0}&BeFrom={1}", this.WorkUser.UserID, BeFrom.Zhjg_Menu.ToString());
                    break;
                case "5"://施工许可信息
                    this.iframe_url = string.Format("Zhjg_Sgxkz_List.aspx?LoginID={0}&BeFrom={1}", this.WorkUser.UserID, BeFrom.Zhjg_Menu.ToString());
                    break;
                case "6"://施工图审查信息
                    this.iframe_url = string.Format("Zhjg_Sgtsc_List.aspx?LoginID={0}&BeFrom={1}", this.WorkUser.UserID, BeFrom.Zhjg_Menu.ToString());
                    break;
                case "7"://竣工验收备案信息
                    this.iframe_url = string.Format("Zhjg_Jgba_List.aspx?LoginID={0}&BeFrom={1}", this.WorkUser.UserID, BeFrom.Zhjg_Menu.ToString());
                    break;
            }
        }
        #endregion
    }
}
