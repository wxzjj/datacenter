using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;
using Wxjzgcjczy.Common;
using System.Data;
using Wxjzgcjczy.BLL;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Zyry
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class List : IHttpHandler, IRequiresSessionState
    {
        private ZyryBLL BLL;
        private string json;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int allRecordCount = 0;
            string sortname = context.Request.Params["sortname"];
            string sortorder = context.Request.Params["sortorder"];
            int page = Convert.ToInt32(context.Request.Params["page"]) - 1; // 系统的索引从0开始，所以此处需要减1
            int pagesize = Convert.ToInt32(context.Request.Params["pagesize"]);
            string fromWhere = context.Request.QueryString["fromwhere"];
            string rylx = context.Request.QueryString["rylx"];
            string ryid = context.Request.QueryString["ryid"];
            string rowid = context.Request.QueryString["rowid"];
            string befrom = context.Request.QueryString["befrom"];
            string ryzyzglxid = context.Request.QueryString["ryzyzglxid"];
            string ryzslxid = context.Request.QueryString["ryzslxid"];
            object sessionAppUser = context.Session[ConfigManager.GetSignInAppUserSessionName()];
            AppUser workUser = (AppUser)sessionAppUser;
            BLL = new ZyryBLL(workUser);
            //排序
            string orderby = @" " + sortname + " " + sortorder + " ";
            //通过检索翻译 生成查询条件
            FilterTranslator ft = ContextExtension.GetGridData(context);
            //分页
            DataTable dt = new DataTable();

            switch (fromWhere)
            {
                case "Ryxx":
                    dt = BLL.RetrieveZyryJbxx(rylx, ft, pagesize, page, orderby, out allRecordCount).Result;
                    break;
                case "Cjxm":
                    dt = BLL.RetrieveCjxm(ryid).Result;
                    break;
                case "RyxxView":
                    dt = BLL.RetrieveZyryJbxxViewList(ryid, rylx).Result;
                    break;
                case "RyxxZymx":
                    dt = BLL.RetrieveRyzymx(ryid, ryzyzglxid, ryzslxid, ft).Result;
                    break;
                case "Ryzsmx":
                    dt = BLL.RetrieveRyzsmx(rowid).Result;
                    break;
            }

            ft.Parms.Clear();
            string result = JSONHelper.DataTableToJson(dt);
            json = @"{""Rows"":[" + result + @"],""Total"":""" + allRecordCount + @"""}";
            context.Response.Write(json);
            context.Response.End();

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
