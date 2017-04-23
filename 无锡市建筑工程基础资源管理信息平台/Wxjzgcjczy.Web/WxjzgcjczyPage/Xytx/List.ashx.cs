using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wxjzgcjczy.Common;
using System.Data;
using System.Web.Services;
using System.Web.SessionState;
using Wxjzgcjczy.BLL;
using Bigdesk8;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Xytx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class List : IHttpHandler, IRequiresSessionState
    {
        public XytxBLL xytxBLL;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int allRecordCount = 0;
            string json = "";
            string sortname = context.Request.Params["sortname"];
            string sortorder = context.Request.Params["sortorder"];
            int page = Convert.ToInt32(context.Request.Params["page"]) - 1; // 系统的索引从0开始，所以此处需要减1
            int pagesize = Convert.ToInt32(context.Request.Params["pagesize"]);
            string fromWhere = context.Request.QueryString["fromwhere"];
            string type = context.Request.QueryString["type"];
            object sessionAppUser = context.Session[ConfigManager.GetSignInAppUserSessionName()];
            AppUser workUser = (AppUser)sessionAppUser;
            xytxBLL = new XytxBLL(workUser);
            //排序
            string orderby = @" " + sortname.ToString2() + " " + sortorder.ToString2() + " ";
            orderby = orderby.Trim();
            //通过检索翻译 生成查询条件
            FilterTranslator ft = ContextExtension.GetGridData(context);
            //分页
            DataTable dt = new DataTable();
            switch (fromWhere)
            {
                case "qyxykp":
                    string qylx = context.Request.QueryString["qylx"];

                    dt = xytxBLL.RetrieveQyxykp(qylx, ft, pagesize, page, orderby, out allRecordCount);

                    break;
                case "xzcf":
                    //throw new Exception("aaa");
                    dt = xytxBLL.RetrieveXzcf(type, ft, pagesize, page, orderby, out allRecordCount);
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
