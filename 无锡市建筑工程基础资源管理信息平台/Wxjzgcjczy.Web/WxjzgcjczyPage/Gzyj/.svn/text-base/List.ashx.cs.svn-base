using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;
using Wxjzgcjczy.Common;
using System.Data;
using Wxjzgcjczy.BLL;
using System.Text.RegularExpressions;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Gzyj
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class List : IHttpHandler, IRequiresSessionState
    {

        private GzyjBLL BLL;
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
            //string qylx = context.Request.QueryString["qylx"];
            //string qyId = context.Request.QueryString["qyid"];
            string rowid = context.Request.QueryString["rowid"];

            object sessionAppUser = context.Session[ConfigManager.GetSignInAppUserSessionName()];
            AppUser workUser = (AppUser)sessionAppUser;
            BLL = new GzyjBLL((AppUser)sessionAppUser);

            //排序
            string orderby = @" " + sortname + " " + sortorder + " ";
            //通过检索翻译 生成查询条件
            FilterTranslator ft = ContextExtension.GetGridData(context);
            //分页
            DataTable dt = new DataTable();
            //switch (fromWhere)
            //{
            //case "qyzsgq":
            dt = BLL.Retrieve(fromWhere, workUser, ft, pagesize, page, orderby, out allRecordCount).Result;
            //        break;
            //    case "JsdwAjxm":
            //        dt = BLL.RetrieveJsdwAjxm(rowid, pagesize, page, orderby, out allRecordCount).Result;
            //        break;

            //    case "QyxxView":
            //        dt = BLL.RetrieveQyxxViewList(qyId, ft).Result;
            //        break;
            //}
            ft.Parms.Clear();
            string result = JSONHelper.DataTableToJson(dt);
            result = Regex.Replace(result, @"[\n\r]", ""); //去掉字符串里所有换行符
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
