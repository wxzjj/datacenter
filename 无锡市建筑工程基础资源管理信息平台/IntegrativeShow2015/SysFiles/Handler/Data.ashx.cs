using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;
using System.Data;
using System.Configuration;
using System.Text;
using Bigdesk8.Data;

namespace IntegrativeShow2.SysFiles.Handler
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Data : IHttpHandler, IRequiresSessionState
    {

       

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string json = "";
            string type = context.Request["type"];
         
            object sessionAppUser = context.Session[ConfigurationManager.AppSettings["SignInAppUserSessionName"]];
            AppUser WorkUser = (AppUser)sessionAppUser;
          
            switch (type)
            {
                case "SetLxxmGIS":
                    json = SetLxxmGIS(context);
                    break;
  
            }

            context.Response.Write(json);
            context.Response.End();
        }

        public string SetLxxmGIS(HttpContext context)
        {
            string prjNum = context.Request.Form["prjNum"];
            string x = context.Request.Form["x"];
            string y = context.Request.Form["y"];

            StringBuilder str = new StringBuilder();
            if (string.IsNullOrEmpty(x) || string.IsNullOrEmpty(y))
            {
                str.Append("{\"isSuccess\":false,\"Msg\":\"经纬度坐标不合法！\"}");
                return str.ToString();
            }

            DBOperator DB = new SqlServerDbOperator(ConfigurationManager.ConnectionStrings["WJSJZXConnectionString"]);
            SqlParameterCollection sp=DB.CreateSqlParameterCollection();
            string sql = "select * from TBProjectInfo where PrjNum=@prjNum and UpdateFlag='U' ";
            sp.Add("@prjNum", prjNum);
            DataTable dt=DB.ExeSqlForDataTable(sql,sp,"dt");
            if (dt.Rows.Count > 0)
            {
                dt.Rows[0]["jd"] = x;
                dt.Rows[0]["wd"] = y;
                dt.Rows[0]["isSgbz"] = 1;

                if (DB.Update("select * from TBProjectInfo where 1=2 ", sp, dt))
                {
                    str.Append("{\"isSuccess\":true,\"Msg\":\"标注成功！\"}");
                }
                else
                {
                    str.Append("{\"isSuccess\":false,\"Msg\":\"标注失败！\"}");
                }
            }
            else
            {
                str.Append("{\"isSuccess\":false,\"Msg\":\"没有记录！\"}");
            }

            return str.ToString();
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
