using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Data;
using Bigdesk8;
using Wxjzgcjczy;
using Wxjzgcjczy.BLL;
using Wxjzgcjczy.Common;

namespace BuildingProject.Web.BuildingProjectPage.GantT
{
    public class List : IHttpHandler, IRequiresSessionState
    {
        private string json;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
           
            string fromWhere = context.Request.QueryString["fromwhere"];

            object sessionAppUser = context.Session[ConfigManager.GetSignInAppUserSessionName()];
      
            json = "[";
            switch (fromWhere)
            {
              

                case "test2":{
                    json = JSONHelper.stringToJson(json, "路基清理", "计划进度", "2013-05-02", "2013-06-02", "", "ganttGreen", "路基清理");
                    json = JSONHelper.stringToJson(json, "", "形象进度", "2013-05-02", "2013-06-19", "80%", "ganttRed", "路基清理");
                    json = JSONHelper.stringToJson(json, "安装管道", "计划进度", "2013-06-02", "2013-09-02", "", "ganttGreen", "安装管道");
                    json = JSONHelper.stringToJson(json, "", "形象进度", "2013-05-27", "2013-09-01", "100%", "ganttRed", "安装管道");
                    json = JSONHelper.stringToJson(json, "弃土转运", "计划进度", "2013-08-02", "2013-12-02", "", "ganttGreen", "弃土转运");
                    json = JSONHelper.stringToJson(json, "", "形象进度", "2013-09-01", "", "10%", "ganttRed", "弃土转运");
                    json = JSONHelper.stringToJson(json, "沥青铺设", "计划进度", "2013-10-02", "2014-02-02", "", "ganttGreen", "沥青铺设");
                    json = JSONHelper.stringToJson(json, "", "形象进度", "", "", "", "ganttRed", "沥青铺设");
                } break;
            }
           //json = "[{ \"name\": \"桩基\",   \"desc\": \"计划进度\", \"values\": [{ \"from\": \"/Date(1320192000000)/\",  \"to\":\"/Date(1322401600000)/\",  \"label\": \"100%\", \"desc\":\"<b>名称</b>:桩基<br/><b>name</b>:计划进度<br/><b>Description</b>: Task desc.\"  , \"customClass\": \"ganttRed\" }]  }," +
           //           "{ \"name\": \"\",   \"desc\": \"计划进度\", \"values\": [{ \"from\": \"/Date(1320192000000)/\",  \"to\":\"/Date(1324401600000)/\",  \"label\": \"100%\", \"desc\":\"<b>Type</b>: 桩基<br/><b>name</b>: Task 11<br/><b>Description</b>: Task desc.\"  , \"customClass\": \"ganttRed\" }]  }]";

           
            json = json.Remove(json.LastIndexOf(","), 1);//去掉最后一个逗号
            json += "]";
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
