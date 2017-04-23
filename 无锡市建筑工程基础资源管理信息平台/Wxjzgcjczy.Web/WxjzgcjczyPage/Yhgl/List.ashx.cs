using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wxjzgcjczy.Common;
using System.Data;
using System.Web.Services;
using System.Web.SessionState;
using Wxjzgcjczy.BLL;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Yhgl
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class List : IHttpHandler, IRequiresSessionState
    {

        public YhglBLL yhglBLL;
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
            string rowid = context.Request.QueryString["rowid"];
            object sessionAppUser = context.Session[ConfigManager.GetSignInAppUserSessionName()];
            AppUser workUser = (AppUser)sessionAppUser;
            yhglBLL = new YhglBLL(workUser);
            //排序
            string orderby = @" " + sortname + " " + sortorder + " ";
            //通过检索翻译 生成查询条件
            FilterTranslator ft = ContextExtension.GetGridData(context);
            //分页
            DataTable dt = new DataTable();
            switch (fromWhere)
            {
                case "yhxx":
                    dt = yhglBLL.Retrieve_g_user(ft, pagesize, page, orderby, out allRecordCount).Result;
                    break;
                case "roleRight_List":
                    string userID=context.Request.QueryString["userID"];

                    dt = yhglBLL.Retrieve_RoleRight_List(userID,ft, pagesize, page, orderby, out allRecordCount).Result;
                    break;

            }
            ft.Parms.Clear();

            string result = JSONHelper.DataTableToJson(dt);
            //result = Regex.Replace(result, @"[/n/r]", ""); //去掉字符串里所有换行符
            //result = result.TrimEnd((char[])"\n\r".ToCharArray());  //去掉换行符
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
