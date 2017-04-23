using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace Bigdesk2010.Web
{
    /// <summary>
    /// some note here
    /// </summary>
    public class TreeViewGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="pathOfConfigFile"></param>
        /// <param name="treeConfigTableName"></param>
        /// <param name="parentNodeID"></param>
        public static void InitTreeView(TreeView treeView, string pathOfConfigFile, string treeConfigTableName, string parentNodeID)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(pathOfConfigFile);

            if (ds.Tables[treeConfigTableName].Rows.Count > 0)
            {
                InitTree(treeView.Nodes, ds.Tables[treeConfigTableName], ds.Tables[treeConfigTableName].Rows[0]["ParentNodeID"].ToString());
            }
        }

        private static void InitTree(TreeNodeCollection tnc, DataTable dt, string parentId)
        {
            DataView dv = new DataView();
            dv.Table = dt;
            dv.RowFilter = "ParentNodeID=" + parentId + "";

            foreach (DataRowView drv in dv)
            {
                TreeNode tmpNd = new TreeNode();

                if (drv["NavigateUrl"].IsEmpty())	//是一个树枝节点
                {
                    tmpNd.Value = drv["Value"].ToString();
                    tmpNd.Text = drv["Text"].ToString();
                    tmpNd.ImageToolTip = drv["Value"].ToString();
                    tmpNd.ImageUrl = drv["ImageUrl"].ToString();
                    tmpNd.SelectAction = TreeNodeSelectAction.Select;

                    InitTree(tmpNd.ChildNodes, dt, drv["NodeID"].ToString());

                    if (tmpNd.ChildNodes.Count > 0)
                        tnc.Add(tmpNd);
                }
                else	//是一个树叶节点
                {
                    tmpNd.Value = drv["Value"].ToString();
                    tmpNd.Text = drv["Text"].ToString(); ;
                    tmpNd.ImageToolTip = drv["Value"].ToString();
                    tmpNd.ImageUrl = drv["ImageUrl"].ToString();
                    tmpNd.NavigateUrl = drv["NavigateUrl"].ToString();
                    tmpNd.Target = drv["Target"].ToString();
                    tnc.Add(tmpNd);
                }
            }
        }
    }
}
