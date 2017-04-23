using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using Bigdesk8;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using Wxjzgcjczy.BLL;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Handler
{
    public class TreeData : IHttpHandler, IRequiresSessionState
    {

        string zgscfs = string.Empty;
        string bdguid = string.Empty;
        string json = string.Empty;
        string parentNodeID = string.Empty;
        protected AppUser WorkUser = new AppUser();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            object sessionAppUser = context.Session[ConfigManager.GetSignInAppUserSessionName()];
            WorkUser = (AppUser)sessionAppUser;
            switch (WorkUser.UserType)
            {
                case UserType.管理用户:
                    parentNodeID = "10000000";
                    break;
                case UserType.实施单位:
                case UserType.代理机构:
                    parentNodeID = "30000000";
                    break;
                case UserType.申报部门:
                    parentNodeID = "20000000";
                    break;
                //case UserType.施工单位:
                //    parentNodeID = "40000000";
                //    break;
                case UserType.监理单位:
                    parentNodeID = "50000000";
                    break;
                default:
                    break;
            }

            string treeConfigFileName = context.Server.MapPath(ConfigManager.GetTreeConfigFileUrl());
            DataSet dsConfig = this.GetConfigDataSet(treeConfigFileName);
            
            string treeConfigTableName = "TreeConfig";
            if (dsConfig.Tables[treeConfigTableName].Rows.Count > 0)
            {
                json = TreeToJson(parentNodeID, dsConfig.Tables[treeConfigTableName]);
            }

            context.Response.Write(json);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentNodeID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string TreeToJson(string parentNodeID, DataTable dt)
        {
            string time = string.Empty;

            string tabid = string.Empty;
            string text = string.Empty;
            string imgurl = string.Empty;
            string url = string.Empty;

            DataView dv = new DataView();
            dv.Table = dt;
            dv.RowFilter = "ParentNodeID='" + parentNodeID + "'";

            foreach (DataRowView drv in dv)
            {
                string tempstr = string.Empty;
                tabid = drv["NodeID"].ToString();
                url = drv["NavigateUrl"].ToString();
                text = drv["Text"].ToString();
                imgurl = drv["ImageUrl"].ToString();


                if (url == string.Empty) //有分支
                {
                    string tabid2 = string.Empty;
                    string text2 = string.Empty;
                    string imgurl2 = string.Empty;
                    string url2 = string.Empty;
                    string ModuleCode = string.Empty;
                    dv.RowFilter = "ParentNodeID='" + tabid + "'";
                    string children = string.Empty;

                    foreach (DataRowView drv2 in dv)
                    {
                        string tempstr2 = string.Empty;
                        tabid2 = drv2["NodeID"].ToString();
                        ModuleCode = drv2["ModuleCode"].ToString2();

                        //对于需要权限的模块，才会给这个树的ModuleCode赋值
                        if (!string.IsNullOrEmpty(ModuleCode))
                        {
                            
                        }
                        else
                        {
                            url2 = drv2["NavigateUrl"].ToString();
                            text2 = drv2["Text"].ToString();
                            imgurl2 = drv2["ImageUrl"].ToString();

                            tempstr2 = @"{tabid:'" + tabid2 + "',text:'" + text2 + "',imgurl:'" + imgurl2 + "',url:'" + url2 + "'},";
                            children = children + tempstr2;
                        }

                    }
                    if (children.Length > 0)
                    {
                        children = children.Remove(children.LastIndexOf(","), 1);//去掉最后一个逗号
                        children = "[" + children + "]";

                        tempstr = @"{tabid:'" + tabid + "',text:'" + text + "',children:" + children + "},";

                        json = json + tempstr;

                    }

                }

            }
            if (json.Length > 0)
            {
                json = json.Remove(json.LastIndexOf(","), 1);//去掉最后一个逗号
            }
            json = "[" + json + "]";
            return json;
        }


        /// <summary>
        /// 本函数从配置文件中读取配置信息到数据集中。
        /// 从安全的角度考虑，如果对配置文件进行了加密处理，该函数就可以加入解密功能
        /// </summary>
        /// <param name="pathOfConfigFile"></param>
        /// <returns></returns>
        private DataSet GetConfigDataSet(string pathOfConfigFile)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(pathOfConfigFile);
            return ds;
        }
    }
}
