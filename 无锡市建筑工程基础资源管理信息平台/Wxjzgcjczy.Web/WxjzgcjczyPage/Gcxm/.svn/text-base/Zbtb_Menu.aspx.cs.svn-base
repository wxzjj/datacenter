using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wxjzgcjczy.Web.WxjzgcjczyPage.Common;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Gcxm
{
    public partial class Zbtb_Menu : BasePage_Standard
    {
        public string iframe_url = String.Empty;
         protected void Page_Load(object sender, EventArgs e)
        {
            #region 变量初始化
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
                myMenu_MenuItemClick(null,null);
            }
            
        }

        protected void myMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            switch (this.myMenu.SelectedValue)
            {
                case "0"://招标信息
                    this.iframe_url = string.Format("Zhaobxx_List.aspx" );
                    break;
                case "1"://中标信息
                    this.iframe_url = string.Format("Zhongbxx_List.aspx");
                    break;
                case "2"://合同备案  
                    this.iframe_url = string.Format("Htba_List.aspx");
                    break;
                case "3"://造价备案
                    this.iframe_url = string.Format("Zjba_List.aspx");
                    break;
                default:
                    break;
            }
        }
    }
}
