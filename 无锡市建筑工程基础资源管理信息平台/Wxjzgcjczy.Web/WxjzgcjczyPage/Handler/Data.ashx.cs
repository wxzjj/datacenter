using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wxjzgcjczy.BLL;
using System.Web.Services;
using System.Web.SessionState;
using Wxjzgcjczy.Common;
using Bigdesk8;
using System.Data;
using System.Web.Script.Serialization;
using System.Collections;
using System.Xml;
using Newtonsoft.Json;
using System.Text;

using System.IO;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Handler
{ //getXmtzjg
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Data : IHttpHandler, IRequiresSessionState
    {
        private string json;
        //private SzgcBLL BLL;
        private SzqyBLL bll;
        AppUser WorkUser;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string type = context.Request["type"];

            try
            {
                GgfzInfoModel model = new GgfzInfoModel();
                object sessionAppUser = context.Session[ConfigManager.GetSignInAppUserSessionName()];
                WorkUser = (AppUser)sessionAppUser;
                DataTable dt;
                // BLL = new SzgcBLL(WorkUser);
                bll = new SzqyBLL(WorkUser);

                switch (type)
                {
                    case "getZzmc":
                        json = bll.getZTreeOfZzmc();
                        break;
                    case "getTxlTree":
                        ZlctBLL zlctBLL = new ZlctBLL(WorkUser);
                        string gzzsId = context.Request.QueryString["gzzsId"];
                        if (String.IsNullOrEmpty(gzzsId))
                            json = zlctBLL.ZTreeJsonOfTxl(model);
                        else
                            json = zlctBLL.ZTreeJsonOfTxl(model, gzzsId);
                        break;
                    case "getTxlCombox":
                        zlctBLL = new ZlctBLL(WorkUser);
                        dt = zlctBLL.ZComboxJsonOfTxl();
                        json = Dtb2Json(dt); ;
                        break;
                    case "getTxlTree_Dxjb":

                        zlctBLL = new ZlctBLL(WorkUser);
                        string dxjbId = context.Request.QueryString["dxjbId"];
                        if (String.IsNullOrEmpty(dxjbId))
                            json = zlctBLL.ZTreeJsonOfDxjb(model);
                        else
                            json = zlctBLL.ZTreeJsonOfDxjb(model, dxjbId);
                        break;

                    case "getYear":

                        GetYear(context);
                        break;
                    case "getMonth":
                        GetMonth(context);
                        break;
                    case "ssdq":
                        json = GetSsdq(context);
                        break;
                    case "QySsdq":
                        json = this.GetQySsdq(context);
                        break;
                    case "xmxx":
                        //File.AppendAllText("E:\\test.txt", "111");
                        json = GetXmxx(context);
                        break;
                    case "QueryProjectList":
                        json = QueryProjectList(context);
                        break;
                    case "SetLxxmGIS":
                        json = SetLxxmGIS(context);
                        break;
                    case "rysd":
                        json = this.GetQySsdq(context);
                        //json = GetRysd(context);
                        break;
                    case "roleRights":
                        json = GetRoleRights(context);
                        break;

                    case "saveRoles":
                        json = SaveRoles(context);
                        break;
                    case "saveRoleRights":
                        json = SaveRoleRights(context);
                        break;

                    case "getUserRights":
                        json = GetUserRights(context);
                        break;

                    case "getXxcjMenu":
                        json = GetXxcjMenu(context);
                        break;
                    case "getIndexTopMenu":
                        json = GetIndexTopMenu(context);
                        break;

                    case "xxgx_csjk":
                        json = GetXxgx_Csjk(context);
                        break;
                    //往省厅上传项目信息
                    case "uploadToStTBProjectInfo":
                        json = uploadToStTBProjectInfo(context);
                        break;
                    //往省厅上传项目补充信息
                    case "uploadToStTBProjectAddInfo":
                        json = uploadToStTBProjectAddInfo(context);
                        break;
                    //从一站式申报平台按uuid下行安监、质监申报数据
                    case "downloadByUuid":
                        json = downloadByUuid(context);
                        break;
                    //下载企业注册人员数据
                    case "downloadCorpRegStaff":
                        json = downloadCorpRegStaff(context);
                        break;
                    //下载企业资质信息
                    case "downloadCorpCert":
                        json = downloadCorpCert(context);
                        break;
                    //保存合同备案-工程类型
                    case "saveHtbaPrjType":
                        json = saveHtbaPrjType(context);
                        break;
                }
            }
            catch (Exception ex)
            {
                json = ex.Message;

            }

            context.Response.Write(json);
            context.Response.End();
        }

        #region DataTable转Json
        public string Dtb2Json(DataTable dtb)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            ArrayList dic = new ArrayList();
            foreach (DataRow row in dtb.Rows)
            {
                Dictionary<string, string> drow = new Dictionary<string, string>();
                foreach (DataColumn col in dtb.Columns)
                {
                    drow.Add(col.ColumnName, row[col.ColumnName].ToString2());
                }
                dic.Add(drow);
            }
            return jss.Serialize(dic);
        }
        #endregion
        public string GetYear(HttpContext context)
        {
            string isWithSpace = context.Request.QueryString["isWithSpace"];

            if (isWithSpace == "1")
            {
                int y = 2000;
                json = "[";
                json += "{\"text\":\"--未选择--\",\"value\":\"\"},";
                while (y <= DateTime.Now.Year)
                {
                    json += "{\"text\":\"" + y + "\",\"value\":\"" + y + "\"},";
                    y++;
                }
                json = json.TrimEnd(',');
                json += "]";
            }
            else
            {
                int y = 2000;
                json = "[";
                while (y <= DateTime.Now.Year)
                {
                    json += "{\"text\":\"" + y + "\",\"value\":\"" + y + "\"},";
                    y++;
                }
                json = json.TrimEnd(',');
                json += "]";
            }
            return json;
        }
        public string GetMonth(HttpContext context)
        {
            string isWithSpace = context.Request.QueryString["isWithSpace"];
            if (isWithSpace == "1")
            {
                int m = 1;
                json = "[";
                json += "{\"text\":\"--未选择--\",\"value\":\"\"},";
                while (m <= 12)
                {
                    json += "{\"text\":\"" + m + "\",\"value\":\"" + m + "\"},";
                    m++;
                }
                json = json.TrimEnd(',');
                json += "]";
            }
            else
            {
                int m = 1;
                json = "[";
                while (m <= 12)
                {
                    json += "{\"text\":\"" + m + "\",\"value\":\"" + m + "\"},";
                    m++;
                }
                json = json.TrimEnd(',');
                json += "]";
            }
            return json;
        }

        public string GetSsdq(HttpContext context)
        {
            XmxxBLL BLL = new XmxxBLL(WorkUser);
            DataTable dt = BLL.GettbXzqdmDic();
            StringBuilder str = new StringBuilder();
            str.AppendFormat("<option  value='{0}' selected=\"selected\">{1}</option>", "", "请选择所属区域");
            foreach (DataRow row in dt.Rows)
            {
                str.AppendFormat("<option value='{0}'>{1}</option>", row["Code"], row["CodeInfo"]);
            }

            str.AppendFormat("<option value='{0}'>{1}</option>", "省内企业", "省内企业");
            str.AppendFormat("<option value='{0}'>{1}</option>", "省外企业", "省外企业");


            return str.ToString();

        }
        public string GetQySsdq(HttpContext context)
        {
            return this.GetSsdq(context);

        }


        public string GetRysd(HttpContext context)
        {
            return this.GetSsdq(context);

        }

        public string GetXmxx(HttpContext context)
        {
            XmxxBLL BLL = new XmxxBLL(WorkUser);

            string ssdq = context.Request.Params["ssdq"];
            string xmdjrq = context.Request.Params["xmdjrq"];
            string xmmc = context.Request.Params["xmmc"];
            StringBuilder str = new StringBuilder();
            try
            {
                str.Append("[");
                DataTable dt = BLL.GetXmxx(ssdq, xmdjrq, xmmc);

                foreach (DataRow row in dt.Rows)
                {
                    str.Append("{");
                    str.AppendFormat("\"title\":\"{0}\",\"point\":\"{1}|{2}\",\"xmmc\":\"{3}\",\"jsdw\":\"{4}\",\"sgdw\":\"{5}\",\"jldw\":\"{6}\",\"kcdw\":\"{7}\",\"sjdw\":\"{8}\",\"kgrq\":\"{9}\",\"jsgm\":\"{10}\",\"xmztz\":\"{11}万元\",\"prjNum\":\"{12}\",\"isSgbz\":{13},\"PKID\":\"{14}\"",
                        "工程项目概要信息", row["jd"], row["wd"], row["PrjName"].ToString().Replace("\"", ""), row["BuildCorpName"]
                        , row["SgzcbCorpNames"] == DBNull.Value ? "" : row["SgzcbCorpNames"].ToString2().Trim(';')
                        , row["JLCorpNames"] == DBNull.Value ? "" : row["JLCorpNames"].ToString2().Trim(';')
                        , row["EconCorpNames"] == DBNull.Value ? "" : row["EconCorpNames"].ToString2().Trim(';')
                        , row["DesignCorpNames"] == DBNull.Value ? "" : row["DesignCorpNames"].ToString2().Trim(';')
                        , row["BDate"] == DBNull.Value ? "" : row["BDate"].ToDateTime().ToString("yyyy-MM-dd")
                        , row["PrjSize"], row["AllInvest"] == DBNull.Value ? "0" : row["AllInvest"].ToString2()
                        , row["PrjNum"], row["isSgbz"].ToInt32(0), row["PKID"]);
                    str.Append("},");
                }

                return str.ToString().TrimEnd(',') + "]";

            }
            catch (Exception ex)
            {
                str.Append(ex.Message);

            }
            return str.ToString();

        }


        public string QueryProjectList(HttpContext context)
        {
            DataExchangeBLLForGIS BLL = new DataExchangeBLLForGIS();

            string prjNum = context.Request.Params["prjNum"];
            string prjName = context.Request.Params["prjName"];
            string prjAddress = context.Request.Params["prjAddress"];
            StringBuilder str = new StringBuilder();
            WebCommon.WriteLog("QueryProjectList");
            try
            {
                
                DataTable dt = BLL.GetProject(prjNum, prjName, prjAddress);
                WebCommon.WriteLog("dt.Rows.Count:" + dt.Rows.Count);
                str.Append("{\"landmarkcount\": " + dt.Rows.Count + ",");
                str.Append("\"pois\":[");

                decimal jd = 0M, wd=0M;
                foreach (DataRow row in dt.Rows)
                {
                    bool isInWuxi = false;
                    if (row["jd"] != DBNull.Value && row["wd"] != DBNull.Value)
                    {
                        jd = row["jd"].ToDecimal();
                        wd = row["wd"].ToDecimal();
                        if (jd > 119.51M && jd < 120.58M && wd > 31.21M && wd < 36.97M)
                        {
                            isInWuxi = true;
                        }
                    }
                    else
                    {
                        isInWuxi = true;
                    }

                    if (isInWuxi)
                    {
                        str.Append("{");
                        str.AppendFormat("\"PrjNum\":\"{0}\",\"PrjName\":\"{1}\",\"PrjTypeNum\":\"{2}\",\"BuildCorpName\":\"{3}\",\"BuildCorpCode\":\"{4}\",\"ProvinceNum\":\"{5}\",\"CityNum\":\"{6}\",\"CountyNum\":\"{7}\",\"BDate\":\"{8}\",\"EDate\":\"{9}\",\"jd\":\"{10}\",\"wd\":\"{11}\",\"programme_address\":\"{12}\",\"PKID\":\"{13}\",\"DocNum\":\"{14}\",\"DocCount\":\"{15}\"",
                            row["PrjNum"], row["PrjName"], row["PrjTypeNum"], row["BuildCorpName"], row["BuildCorpCode"]
                            , row["ProvinceNum"] == DBNull.Value ? "" : row["ProvinceNum"].ToString2()
                            , row["CityNum"] == DBNull.Value ? "" : row["CityNum"].ToString2()
                            , row["CountyNum"] == DBNull.Value ? "" : row["CountyNum"].ToString2()
                            , row["BDate"] == DBNull.Value ? "" : row["BDate"].ToString2()
                            , row["EDate"] == DBNull.Value ? "" : row["EDate"].ToString2()
                            , row["jd"] == DBNull.Value ? "" : row["jd"].ToString2()
                            , row["wd"] == DBNull.Value ? "" : row["wd"].ToString2()
                            , row["programme_address"] == DBNull.Value ? "" : row["programme_address"].ToString2()
                            , row["PKID"] == DBNull.Value ? "" : row["PKID"].ToString2()
                            , row["DocNum"] == DBNull.Value ? "" : row["DocNum"].ToString2()
                            , row["DocCount"] == DBNull.Value ? "" : row["DocCount"].ToString2()
                            );
                        str.Append("},");
                    }
                   
                }

                return str.ToString().TrimEnd(',') + "] }" ;

            }
            catch (Exception ex)
            {
                str.Append(ex.Message);

            }
            return str.ToString();

        }

        public string SetLxxmGIS(HttpContext context)
        {
            XmxxBLL BLL = new XmxxBLL(WorkUser);
            string PKID = context.Request.Form["PKID"];
            string x = context.Request.Form["x"];
            string y = context.Request.Form["y"];

            StringBuilder str = new StringBuilder();
            if (string.IsNullOrEmpty(x) || string.IsNullOrEmpty(y))
            {
                str.Append("{\"isSuccess\":false,\"Msg\":\"经纬度坐标不合法！\"}");
                return str.ToString();
            }

            DataTable dt = BLL.GetXmxx(PKID);
            if (dt.Rows.Count > 0)
            {
                dt.Rows[0]["jd"] = x;
                dt.Rows[0]["wd"] = y;
                dt.Rows[0]["isSgbz"] = 1;

                if (BLL.SaveXmxx(dt))
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

        public string GetRoleRights(HttpContext context)
        {
            string roleId = context.Request.QueryString["roleId"];
            YhglBLL BLL = new YhglBLL(WorkUser);
            StringBuilder str = new StringBuilder();
            DataTable dt_modules = BLL.Get_RoleModules_List(roleId).Result;
            str.Append("[");
            foreach (DataRow row in dt_modules.Rows)
            {
                str.Append("{");

                str.AppendFormat("\"moduleCode\":\"{0}\",\"moduleName\":\"{1}\"", row["ModuleCode"], row["ModuleName"]);

                str.Append(",\"rights\":[");
                DataTable dt_operators = BLL.Get_ModuleOperators_List(roleId, row["ModuleCode"].ToString2()).Result;

                foreach (DataRow row_operate in dt_operators.Rows)
                {
                    str.Append("{");

                    str.AppendFormat("\"operateCode\":\"{0}\",\"operateName\":\"{1}\",\"hasRights\":{2}", row_operate["OperateCode"], row_operate["OperateName"], (row_operate["HasRight"].ToInt32(0) > 0).ToString().ToLower());
                    str.Append("},");
                }
                if (dt_modules.Rows.Count > 0)
                    str.Remove(str.Length - 1, 1);
                str.Append("]");
                str.Append("},");
            }
            if (dt_modules.Rows.Count > 0)
                str.Remove(str.Length - 1, 1);
            str.Append("]");



            return str.ToString();
        }

        public string SaveRoles(HttpContext context)
        {
            string userID = context.Request.Form["UserID"];

            string roleIDs = context.Request.Form["RoleIDs"];
            StringBuilder str = new StringBuilder();
            YhglBLL BLL = new YhglBLL(WorkUser);
            if (string.IsNullOrEmpty(userID))
            {
                str.Append("{\"isSuccess\":false,\"Msg\":\"非法请求，操作失败！\"}");
                return str.ToString();
            }
            if (!WorkUser.list.Exists(p => p.moduleCode.Equals(ModuleCode_Enum.yhgl.ToString(), StringComparison.CurrentCultureIgnoreCase)
                && (p.operateCode.Equals(Yhgl_Operate.Add.ToString(), StringComparison.CurrentCultureIgnoreCase) || p.operateCode.Equals(Yhgl_Operate.Edit.ToString(), StringComparison.CurrentCultureIgnoreCase))))
            {
                str.Append("{\"isSuccess\":false,\"Msg\":\"您无权限进行此操作！\"}");
                return str.ToString();
            }

            if (BLL.Update_UserRoles(userID, roleIDs))
            {
                str.Append("{\"isSuccess\":true,\"Msg\":\"更新成功！\"}");
            }
            else
                str.Append("{\"isSuccess\":false,\"Msg\":\"更新失败！\"}");


            return str.ToString();
        }

        public string SaveRoleRights(HttpContext context)
        {
            string roleId = context.Request.Form["RoleID"];
            string roleRights = context.Request.Form["RoleRights"];
            StringBuilder str = new StringBuilder();

            if (string.IsNullOrEmpty(roleId))
            {
                str.Append("{\"isSuccess\":false,\"Msg\":\"参数错误，操作失败！\"}");
                return str.ToString();
            }
            YhglBLL BLL = new YhglBLL(WorkUser);
            if (string.IsNullOrEmpty(WorkUser.UserID))
            {
                str.Append("{\"isSuccess\":false,\"Msg\":\"未登录用户，操作失败！\"}");
                return str.ToString();
            }

            List<ModuleOperate> list = JSONHelper.FromJson<List<ModuleOperate>>(roleRights);

            if (BLL.Update_RoleRights(roleId, list))
                str.Append("{\"isSuccess\":true,\"Msg\":\"角色设置成功！\"}");
            else
                str.Append("{\"isSuccess\":false,\"Msg\":\"角色设置失败！\"}");


            return str.ToString();
        }
        public string GetUserRights(HttpContext context)
        {
            YhglBLL BLL = new YhglBLL(WorkUser);
            StringBuilder str = new StringBuilder();
            DataTable dt_modules = BLL.Get_UserRights_List().Result;
            str.Append("[");
            foreach (DataRow row in dt_modules.Rows)
            {
                str.Append("{");

                str.AppendFormat("\"moduleCode\":\"{0}\",\"moduleName\":\"{1}\"", row["ModuleCode"], row["ModuleName"]);
                str.AppendFormat(",\"operateCode\":\"{0}\",\"operateName\":\"{1}\"", row["OperateCode"], row["OperateName"]);
                str.Append("},");
            }
            if (dt_modules.Rows.Count > 0)
                str.Remove(str.Length - 1, 1);
            str.Append("]");
            return str.ToString();
        }

        public string GetXxcjMenu(HttpContext context)
        {
            StringBuilder str = new StringBuilder();
            string htmlSplit = "<tr><td class=\"menuSpan\"></td></tr>";
            if (WorkUser.list.Exists(p => p.moduleCode.Equals(ModuleCode_Enum.ajxx.ToString(), StringComparison.CurrentCultureIgnoreCase)))
            {
                str.Append("<tr><td id=\"left_icon_xxcj_ajxx\" class=\"left_icon_leave\" onmousemove=\"leftover(this.id);\" onmouseout=\"leftout(this.id);\" onclick=\"leftclick('xxcj_ajxx',this.id);\"><table ><tr><td  class=\"tdImg\"><img src=\"../Common/images/FrameImages/02_01_0.png\" /></td><td  class=\"tdFont\">安监信息</td></tr></table></td></tr>");
                str.Append(htmlSplit);
            }

            if (WorkUser.list.Exists(p => p.moduleCode.Equals(ModuleCode_Enum.zjxx.ToString(), StringComparison.CurrentCultureIgnoreCase)))
            {
                str.Append("<tr><td id=\"left_icon_xxcj_zjxx\" class=\"left_icon_leave\" onmousemove=\"leftover(this.id);\" onmouseout=\"leftout(this.id);\" onclick=\"leftclick('xxcj_zjxx',this.id);\"><table ><tr><td  class=\"tdImg\"><img src=\"../Common/images/FrameImages/02_02_0.png\" /></td><td  class=\"tdFont\">质监信息</td></tr></table></td></tr>");
                str.Append(htmlSplit);
            }

            return str.ToString();
        }

        public string GetIndexTopMenu(HttpContext context)
        {

            StringBuilder str = new StringBuilder();
            string htmlSplit = "<td width=\"1\"><img src=\"../Common/images/FrameImages/top_line.png\" width=\"1\" height=\"100\" style=\"padding:0;\" /></td>";
            if (WorkUser.list.Exists(p => p.moduleCode.Equals(ModuleCode_Enum.gcxm.ToString(), StringComparison.CurrentCultureIgnoreCase)))
            {
                str.Append("<td width=\"100\" id=\"top_icon_1\" class=\"top_icon_1_0\" onmousemove=\"tdover(this.id);\" onmouseout=\"tdout(this.id);\" onclick=\"Loading('10000000',this.id,this.id);\"  style=\"padding:0;\" ></td>");
                str.Append(htmlSplit);
            }

            if (WorkUser.list.Exists(p => p.moduleCode.Equals(ModuleCode_Enum.sczt.ToString(), StringComparison.CurrentCultureIgnoreCase)))
            {
                str.Append("<td width=\"100\" id=\"top_icon_2\" class=\"top_icon_2_0\" onmousemove=\"tdover(this.id);\" onmouseout=\"tdout(this.id);\" onclick=\"Loading('20000000',this.id,this.id);\"  style=\"padding:0;\" ></td>");
                str.Append(htmlSplit);
            }
            if (WorkUser.list.Exists(p => p.moduleCode.Equals(ModuleCode_Enum.zyry.ToString(), StringComparison.CurrentCultureIgnoreCase)))
            {
                str.Append("<td width=\"100\" id=\"top_icon_3\" class=\"top_icon_3_0\" onmousemove=\"tdover(this.id);\" onmouseout=\"tdout(this.id);\" onclick=\"Loading('30000000',this.id,this.id);\"  style=\"padding:0;\" ></td>");
                str.Append(htmlSplit);
            }
            if (WorkUser.list.Exists(p => p.moduleCode.Equals(ModuleCode_Enum.xytx.ToString(), StringComparison.CurrentCultureIgnoreCase)))
            {
                str.Append("<td width=\"100\" id=\"top_icon_xykh\" class=\"top_icon_xykh_0\" onmousemove=\"tdover(this.id);\" onmouseout=\"tdout(this.id);\" onclick=\"Loading('xytx',this.id,this.id);\" style=\"padding:0;\"  ></td>");
                str.Append(htmlSplit);
            }

            if (WorkUser.list.Exists(p => p.moduleCode.Equals(ModuleCode_Enum.gzyj.ToString(), StringComparison.CurrentCultureIgnoreCase)))
            {
                str.Append("<td width=\"100\" id=\"top_icon_4\" class=\"top_icon_4_0\" onmousemove=\"tdover(this.id);\" onmouseout=\"tdout(this.id);\" onclick=\"Loading('40000000',this.id,this.id);\"  style=\"padding:0;\" ></td>");
                str.Append(htmlSplit);
            }
            if (WorkUser.list.Exists(p => p.moduleCode.Equals(ModuleCode_Enum.tjfx.ToString(), StringComparison.CurrentCultureIgnoreCase)))
            {
                str.Append("<td width=\"100\" id=\"top_icon_5\" class=\"top_icon_5_0\" onmousemove=\"tdover(this.id);\" onmouseout=\"tdout(this.id);\" onclick=\"Loading('50000000',this.id,this.id);\"  style=\"padding:0;\" ></td>");
                str.Append(htmlSplit);
            }
            //if (WorkUser.list.Exists(p => p.moduleCode.Equals(ModuleCode_Enum.zlct.ToString(), StringComparison.CurrentCultureIgnoreCase)))
            //{
            //    str.Append("<td width=\"100\" id=\"top_icon_7\" class=\"top_icon_7_0\" onmousemove=\"tdover(this.id);\" onmouseout=\"tdout(this.id);\" onclick=\"Loading('70000000',this.id,this.id);\"  style=\"padding:0;\" ></td>");
            //    str.Append(htmlSplit);
            //}
            //if (WorkUser.list.Exists(p => p.moduleCode.Equals(ModuleCode_Enum.xxgx.ToString(), StringComparison.CurrentCultureIgnoreCase)))
            //{
            //    str.Append("<td width=\"100\" id=\"top_icon_xxgx\" class=\"top_icon_xxgx_0\" onmousemove=\"tdover(this.id);\" onmouseout=\"tdout(this.id);\" onclick=\"Loading('xxgx',this.id,this.id);\"  style=\"padding:0;\" ></td>");
            //    str.Append(htmlSplit);
            //}
            if (WorkUser.list.Exists(p => p.moduleCode.Equals(ModuleCode_Enum.tdt.ToString(), StringComparison.CurrentCultureIgnoreCase)))
            {
                str.Append("<td width=\"100\" id=\"top_icon_tdt\" class=\"top_icon_tdt_0\" onmousemove=\"tdover(this.id);\" onmouseout=\"tdout(this.id);\" onclick=\"Loading('tdt',this.id,this.id);\"  style=\"padding:0;\" ></td>");
                str.Append(htmlSplit);
            }
            if (WorkUser.list.Exists(p => p.moduleCode.Equals(ModuleCode_Enum.yhgl.ToString(), StringComparison.CurrentCultureIgnoreCase)))
            {
                str.Append("<td width=\"100\" id=\"top_icon_yhgl\" class=\"top_icon_yhgl_0\" onmousemove=\"tdover(this.id);\" onmouseout=\"tdout(this.id);\" onclick=\"Loading('yhgl',this.id,this.id);\"  style=\"padding:0;\" ></td>");
                str.Append(htmlSplit);

            }

            return str.ToString();
        }


        public string GetXxgx_Csjk(HttpContext context)
        {
            XxgxBLL BLL = new XxgxBLL(WorkUser);

            //string ssdq = context.Request.Params["ssdq"];
            //string xmdjrq = context.Request.Params["xmdjrq"];
            //string xmmc = context.Request.Params["xmmc"];
            StringBuilder str = new StringBuilder();
            try
            {
                DataTable dt = BLL.GetXxgx_Csjk();

                foreach (DataRow row in dt.Rows)
                {
                    str.Append("{");
                    str.AppendFormat("\"id\":\"{0}\"", row["ID"]);
                    str.AppendFormat(",\"value\":\"{0}\"", row["DataFlow"]);
                    str.AppendFormat(",\"name\":\"{0}\"", row["DataFlowName"]);
                    str.AppendFormat(",\"enable\":\"{0}\"", row["IsOk"] == DBNull.Value ? 0 : (Int32.Parse(row["IsOk"].ToString())) > 0 ? 1 : 0);
                    str.AppendFormat(",\"serviceUrl\":\"{0}\"", row["ServiceUrl"].ToString2());
                    str.Append("},");
                }
                if (str.Length == 0)
                    return String.Empty;
                else
                    return "[" + str.ToString().TrimEnd(',') + "]";
            }
            catch (Exception ex)
            {
                str.Append(ex.Message);

            }
            return str.ToString();
        }


        /// <summary>
        /// 手动往省厅上传项目信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string uploadToStTBProjectInfo(HttpContext context)
        {
            DataExchangeBLLForUpload BLL = new DataExchangeBLLForUpload();

            string pKID = context.Request.Params["PKID"];
            ProcessResultData result = new ProcessResultData();
            try
            {
               result = BLL.SaveTBData_TBProjectInfo(WorkUser.LoginName, pKID);
            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = ex.Message;

            }
            return result.ResultMessage;

        }

        /// <summary>
        /// 手动往省厅上传项目补充信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string uploadToStTBProjectAddInfo(HttpContext context)
        {
            DataExchangeBLLForUpload BLL = new DataExchangeBLLForUpload();

            string prjNum = context.Request.Params["PrjNum"];
            ProcessResultData result = new ProcessResultData();
            try
            {
                result = BLL.SaveTBData_TBProjectAddInfo(WorkUser.LoginName, prjNum);
            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = ex.Message;

            }
            return result.ResultMessage;

        }

        /// <summary>
        /// 手动从一站式申报平台按uuid下行安监、质监申报数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string downloadByUuid(HttpContext context)
        {
            DataExchangeBLLForYZSSBDownload BLL = new DataExchangeBLLForYZSSBDownload();

            string uuid = context.Request.Params["uuid"];
            string deptType = context.Request.Params["deptType"];
            ProcessResultData result = new ProcessResultData();
            try
            {
                string msg = null;
                if ("AJ".Equals(deptType))
                {
                    msg = BLL.PullAJSBDataFromSythptByUUID(uuid);
                }
                else
                {
                    msg = BLL.PullZJSBDataFromSythptByUUID(uuid);
                }
                result.message = msg;

            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = ex.Message;

            }
            return result.ResultMessage;

        }

        public string downloadCorpRegStaff(HttpContext context)
        {
            DataExchangeBLLForJSCEDC BLL = new DataExchangeBLLForJSCEDC();

            string qyID = context.Request.Params["qyID"]; 
            ProcessResultData result = new ProcessResultData();
            try
            {
                string msg = null;
                msg = BLL.PullDataCorpRegStaff(qyID);
                result.message = msg;

            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = ex.Message;

            }
            return result.ResultMessage;

        }

        public string downloadCorpCert(HttpContext context)
        {
            DataExchangeBLLForJSCEDC BLL = new DataExchangeBLLForJSCEDC();

            string qyID = context.Request.Params["qyID"];
            ProcessResultData result = new ProcessResultData();
            try
            {
                string msg = null;
                msg = BLL.PullDataCorpCert(qyID);
                result.message = msg;

            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = ex.Message;

            }
            return result.ResultMessage;

        }

        public string saveHtbaPrjType(HttpContext context)
        {
            HtbaBLL BLL = new HtbaBLL();

            string recordNum = context.Request.Params["RecordNum"];
            string prjType = context.Request.Params["prjType"];
            
            ProcessResultData result = new ProcessResultData();
            try
            {
                //BLLCommon.WriteLog("recordNum : " + recordNum + ",prjType:" + prjType);
                BLL.saveHtbaPrjType(recordNum, prjType);
                result.message = "OK";

            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = ex.Message;
                BLLCommon.WriteLog("message:" + result.message);

            }
            return result.ResultMessage;

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
