using System;
using System.Web.UI.WebControls;
using Bigdesk8.Web;

namespace Bigdesk8.Business
{
    public partial class LeftFrame : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //string parentID = this.Request.QueryString["parentID"];

                if (!this.IsUserLogin)
                {
                    // 未登录
                    this.TopWindowLocation(WebCommon.GetLoginPageUrl());
                    return;
                }

                if (!this.UserInfo.HasAttribute(系统管理员特性编号))
                {
                    this.ResponseRedirect(UserInfo.UserName + "(" + UserInfo.LoginName + ")" + "，您不是系统管理员，没有 管理中心 权限!", "top");
                    return;
                }
            }
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            CollapseAllTreeNode();
            this.TreeView1.SelectedNode.Expand();
        }

        private void CollapseAllTreeNode()
        {
            if (this.TreeView1.Nodes.Count <= 0) return;

            foreach (TreeNode tn in this.TreeView1.Nodes)
            {
                tn.Collapse();
            }
        }
    }
}
