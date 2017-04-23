using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.Script.Serialization;
using System.Web;
using System.Web.UI.WebControls;
using Bigdesk8.Business;
using Bigdesk8.Data;
using Bigdesk8;

namespace Wxjzgcjczy.Common
{
    public class JSONHelper
    {
        #region Dt2Json
        public static string DataTableToJson(DataTable dt)
        {

            string columnFirst = "";

            List<string> result = new List<string>();

            StringBuilder Json = new StringBuilder();

            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    //if (columnFirst != dt.Rows[i][0].ToString())
                    //{

                    if (i != 0)
                    {

                        AddNewJson(Json, result, dt);

                    }

                    columnFirst = dt.Rows[i][0].ToString();

                    result = new List<string>();

                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        string value = dt.Rows[i][k].ToString();
                        if (value.Contains("\r\n"))
                        {
                            //value = value.Replace("\r\n", "\\r\\n");
                            value = value.Replace("\r\n", "");//干脆将回车换行符去掉拉倒
                        }

                        if (value.Contains("\""))
                            value = value.Replace("\"", "”");
                        value = value.Replace("\\", "/");

                        value = TrimLR(value);

                        result.Add("\"" + value + "\"");

                    }

                    //}

                    //else
                    //{

                    //    for (int k = 0; k < dt.Columns.Count; k++)
                    //    {

                    //        if (!result[k].Contains(dt.Rows[i][k].ToString()))
                    //        {

                    //            result[k] += ",\"" + dt.Rows[i][k].ToString() + "\"";

                    //        }

                    //    }

                    //}

                    if (i == dt.Rows.Count - 1)
                    {

                        AddNewJson(Json, result, dt);

                    }

                }

            }
            string r_json = Json.ToString();
            if (r_json != string.Empty)
            {
                r_json = r_json.Remove(r_json.LastIndexOf(","), 1);//去掉最后一个逗号
            }
            else
            {
                r_json = @"[]";
            }
            return r_json;

        }


        public static string TrimLR(string str)
        {

            str = System.Text.RegularExpressions.Regex.Replace(str, @"(^\s*)|(\s*$)|(^\t*)|(\t*$)|(^\n*$)", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            return str;

        }



        private static void AddNewJson(StringBuilder Json, List<string> result, DataTable dt)
        {

            Json.Append("{");

            for (int i = 0; i < dt.Columns.Count; i++)
            {

                Json.Append("\"");

                Json.Append(dt.Columns[i].ColumnName);

                Json.Append("\":");

                if (result[i].Contains(","))
                {

                    Json.Append("[");

                    Json.Append(result[i].Replace("\0", ""));

                    if (i == dt.Columns.Count - 1)
                    {

                        Json.Append("]");

                    }

                    else
                    {

                        Json.Append("],");

                    }

                }
                else
                {

                    Json.Append(result[i].TrimString().Replace("\0", ""));

                    if (i != dt.Columns.Count - 1)
                    {
                        Json.Append(",");
                    }

                }

            }
            Json.Append("},");

        }
        private static void ChildDtTostring(DataTable dt, StringBuilder Json, string parentId)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["parentId"].ToString2() == parentId)
                {
                    Json.Append(",\"children\": [");
                    break;
                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["parentId"].ToString2() == parentId)
                {
                    result = new List<string>();
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        result.Add("\"" + dt.Rows[i][k].ToString() + "\"");
                    }
                    AddNewJson1(Json, result, dt);
                    if (dt.Rows[i]["isParent"].ToBoolean(false))
                    {
                        ChildDtTostring(dt, Json, dt.Rows[i]["id"].ToString2());
                    }
                    Json.Append("},");
                }
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["parentId"].ToString2() == parentId)
                {
                    Json.Remove(Json.ToString().LastIndexOf(","), 1);
                    Json.Append("]");
                    break;
                }
            }
        }
        private static StringBuilder DtTostring(DataTable dt)
        {
            List<string> result = new List<string>();

            StringBuilder Json = new StringBuilder();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["parentId"].ToString2().Length <= 0)
                    {
                        result = new List<string>();
                        for (int k = 0; k < dt.Columns.Count; k++)
                        {
                            result.Add("\"" + dt.Rows[i][k].ToString() + "\"");
                        }
                        AddNewJson1(Json, result, dt);
                        if (dt.Rows[i]["id"].ToString2().Length > 0)
                        {
                            ChildDtTostring(dt, Json, dt.Rows[i]["id"].ToString2());
                        }
                        Json.Append("},");
                    }
                }

            }

            return Json;
        }

        private static void AddNewJson1(StringBuilder Json, List<string> result, DataTable dt)
        {
            Json.Append("{");
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (result[i] != "\"\"")
                {
                    Json.Append("\"");
                    Json.Append(dt.Columns[i].ColumnName);
                    Json.Append("\":");
                    Json.Append(result[i]);
                    if (i != dt.Columns.Count - 1)
                    {
                        Json.Append(",");
                    }
                }

            }
            if (Json.ToString().Substring(Json.ToString().Length - 1) == ",")
            {
                Json.Remove(Json.ToString().Length - 1, 1);
            }
            //Json.Append("},");

        }
        /// <summary>
        /// 转换datatable  
        /// 列中必须有 parentId和Id两个字段 isParent 是否为父节点
        /// parentId 为空表示为父组
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DtToJsonPC(DataTable dt)
        {
            StringBuilder Json = DtTostring(dt);
            string r_json = Json.ToString();
            if (r_json != string.Empty)
            {
                r_json = r_json.Remove(r_json.LastIndexOf(","), 1);//去掉最后一个逗号
            }
            else
            {
                r_json = @"[]";
            }
            return r_json;
        }

        #endregion


        /// <summary>
        /// json格式转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T FromJson<T>(string strJson) where T : class
        {
            return new JavaScriptSerializer().Deserialize<T>(strJson);
        }

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="strJson"></param>
        /// <param name="name">名称</param>
        /// <param name="type">类型</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="value">进度值</param>
        /// <param name="color">颜色</param>
        /// <returns></returns>
        public static string stringToJson(string strJson, string name, string type, string startDate, string endDate, string value, string color, string title)
        {
            if (startDate.ToString2().Length > 0)
            {
                if (endDate.ToString2().Length <= 0)
                {
                    endDate = DateTime.Now.ToString();
                }
                string startTime = Decimal.ToInt64(Decimal.Divide(startDate.ToDateTime().Ticks - new DateTime(1970, 1, 1, 0, 0, 0).Ticks, 10000)).ToString2();
                string endTime = Decimal.ToInt64(Decimal.Divide(endDate.ToDateTime().Ticks - new DateTime(1970, 1, 1, 0, 0, 0).Ticks, 10000)).ToString2();
                strJson += "{ \"name\": \"" + name + "\",   \"desc\": \"" + type + "\", \"values\": [{ \"from\": \"/Date(" + startTime + ")/\",  \"to\":\"/Date(" + endTime + ")/\",  \"label\": \"" + value + "\", \"desc\":\"<b>名称</b>: " + title + "(" + type + ")" + "<br/><b>进度</b>: " + value + "<br/><b>起始日期</b>:" + startDate.ToDate().ToString("yyyy年MM月dd日") + "---" + endDate.ToDate().ToString("yyyy年MM月dd日") + "\"  , \"customClass\": \"" + color + "\" }]  },";
            }
            else
            {
                startDate = DateTime.Now.ToString();
                endDate = DateTime.Now.ToString();
                string startTime = Decimal.ToInt64(Decimal.Divide(startDate.ToDateTime().Ticks - new DateTime(1970, 1, 1, 0, 0, 0).Ticks, 10000)).ToString2();
                string endTime = Decimal.ToInt64(Decimal.Divide(endDate.ToDateTime().Ticks - new DateTime(1970, 1, 1, 0, 0, 0).Ticks, 10000)).ToString2();
                strJson += "{ \"name\": \"" + name + "\",   \"desc\": \"" + type + "\", \"values\": [{ \"from\": \"/Date(" + startTime + ")/\",  \"to\":\"/Date(" + endTime + ")/\",  \"label\": \"" + value + "\", \"customClass\": \"write\" }]  },";
            }
            return strJson;
        }


        #region DataTable转Json
        public static string Dtb2Json(DataTable dtb)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            ArrayList dic = new ArrayList();
            foreach (DataRow row in dtb.Rows)
            {
                Dictionary<string, object> drow = new Dictionary<string, object>();
                foreach (DataColumn col in dtb.Columns)
                {
                    drow.Add(col.ColumnName, row[col.ColumnName]);
                }
                dic.Add(drow);
            }
            return jss.Serialize(dic);
        }
        #endregion

        #region Json转DataTable
        public static DataTable Json2Dtb(string json)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            ArrayList dic = jss.Deserialize<ArrayList>(json);
            DataTable dtb = new DataTable();
            if (dic.Count > 0)
            {
                foreach (Dictionary<string, object> drow in dic)
                {
                    if (dtb.Columns.Count == 0)
                    {
                        foreach (string key in drow.Keys)
                        {
                            dtb.Columns.Add(key, drow[key].GetType());
                        }
                    }
                    DataRow row = dtb.NewRow();
                    foreach (string key in drow.Keys)
                    {
                        row[key] = drow[key];
                    }
                    dtb.Rows.Add(row);
                }
            }
            return dtb;
        }
        #endregion

        #region DataTable转Json(非Json)
        //类似 前台jQuery.parseJSON（dt）函数
        private static Dictionary<string, object> DatToJson(DataTable table)
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            d.Add(table.TableName, RowsToDictionary(table));
            return d;
        }
        private static List<Dictionary<string, object>> RowsToDictionary(DataTable table)
        {
            List<Dictionary<string, object>> objs = new List<Dictionary<string, object>>();
            foreach (DataRow dr in table.Rows)
            {
                Dictionary<string, object> drow = new Dictionary<string, object>();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    drow.Add(table.Columns[i].ColumnName, dr[i]);
                }
                objs.Add(drow);
            }
            return objs;
        }
        #endregion


    }
}
