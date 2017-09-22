using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxjzgcjczy.DAL;
using Bigdesk8;
using System.Data;
using Wxjzgcjczy.Common;
using Bigdesk8.Data;
using System.Transactions;
using System.Text.RegularExpressions;

namespace Wxjzgcjczy.BLL
{
    /// <summary>
    /// 功能： 无锡数据中心与各县市系统数据交换业务处理类之一站式申报数据处理
    /// 作者：黄正宇
    /// 时间：2017-09-10
    /// </summary>
    public class DataExchangeBLLForYZSSB
    {
        private readonly Wxjzgcjczy.DAL.Sqlserver.DataExchangeDALForYZSSB DAL = new Wxjzgcjczy.DAL.Sqlserver.DataExchangeDALForYZSSB();

        XmlHelper xmlHelper = new XmlHelper();

        ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient client = new ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient();


        public DataExchangeBLLForYZSSB()
        {
            DAL.DB = new Wxjzgcjczy.DAL.Sqlserver.DatabaseOperator();
        }

        public bool SubmitData(string sql, DataTable dt)
        {

            return DAL.DB.Update(sql, null, dt);

        }

        #region 读取安监申报数据

        public DataTable GetAp_ajsbb(string date, string countryCodes)
        {
            DataTable dt = DAL.GetAp_ajsbb(date, countryCodes);
            return dt;
        }

        public DataTable GetAp_ajsbb_ht(string uuid)
        {
            DataTable dt = DAL.GetAp_ajsbb_ht(uuid);
            return dt;
        }

        public DataTable GetAp_ajsbb_dwry(string uuid)
        {
            DataTable dt = DAL.GetAp_ajsbb_dwry(uuid);
            return dt;
        }

        public DataTable GetAp_ajsbb_clqd(string uuid)
        {
            DataTable dt = DAL.GetAp_ajsbb_clqd(uuid);
            return dt;
        }

        public DataTable GetAp_ajsbb_hjssjd(string uuid)
        {
            DataTable dt = DAL.GetAp_ajsbb_hjssjd(uuid);
            return dt;
        }

        public DataTable GetAp_ajsbb_wxyjdgcqd(string uuid)
        {
            DataTable dt = DAL.GetAp_ajsbb_wxyjdgcqd(uuid);
            return dt;
        }

        public DataTable GetAp_ajsbb_cgmgcqd(string uuid)
        {
            DataTable dt = DAL.GetAp_ajsbb_cgmgcqd(uuid);
            return dt;
        }

        #endregion

        #region 读取质监申报数据

        public DataTable GetAp_zjsbb(string date, string countryCodes)
        {
            DataTable dt = DAL.GetAp_zjsbb(date, countryCodes);
            return dt;
        }

        public DataTable GetAp_zjsbb_ht(string uuid)
        {
            DataTable dt = DAL.GetAp_zjsbb_ht(uuid);
            return dt;
        }

        public DataTable GetAp_zjsbb_dwry(string uuid)
        {
            DataTable dt = DAL.GetAp_zjsbb_dwry(uuid);
            return dt;
        }

        public DataTable GetAp_zjsbb_schgs(string uuid)
        {
            DataTable dt = DAL.GetAp_zjsbb_schgs(uuid);
            return dt;
        }

        public DataTable GetAp_zjsbb_dwgc(string uuid)
        {
            DataTable dt = DAL.GetAp_zjsbb_dwgc(uuid);
            return dt;
        }

        public DataTable GetAp_zjsbb_clqd(string uuid)
        {
            DataTable dt = DAL.GetAp_zjsbb_clqd(uuid);
            return dt;
        }


        #endregion

        #region 推送安监申报结果

        /// <summary>
        /// 向省一站式申报平台推送安监申报结果
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dt_Data"></param>
        /// <returns></returns>
        public ProcessResultData pushAJSBJG(string user, string deptcode, string sbPassword, DataTable dt_Data)
        {
            ProcessResultData result = new ProcessResultData();
            StringBuilder prjInnerNumSb = new StringBuilder();

            string msg = String.Empty;
            string[] fields = new string[] { "uuid", "success", "slry", "slrq" };

            DataRow item = dt_Data.Rows[0];
            List<string> novalidates = new List<string>();
            novalidates.Add(String.Empty);
            novalidates.Add(" ");
            novalidates.Add("无");
            novalidates.Add("无数据");
            novalidates.Add("/");

            if (BLLCommon.DataFieldIsNullOrEmpty(novalidates, fields, item, out msg))
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = msg + "不能为空！";
                return result;
            }

            DataTable dt_ajsbjg = DAL.GetAp_ajsbjg();
            DataRow row = dt_ajsbjg.NewRow();
            DataTableHelp.DataRow2DataRow(item, row);

            row["id"] = Guid.NewGuid();
            row["UpdateTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dt_ajsbjg.Rows.Add(row);
            row["deptcode"] = deptcode;
            row["sbPassword"] = sbPassword;
            row["UpdateUser"] = user;
            
           

            if (dt_ajsbjg.Rows.Count > 0)
            {
                if (DAL.SaveAp_ajsbjg(dt_ajsbjg))
                {
                    try
                    {
                        StringBuilder str = new StringBuilder();
                        string xmlData = "";
                        DataRow dataRow = dt_ajsbjg.Rows[0];

                        //向省一站式申报平台推送安监申报结果
                        string[] excludeColumns = new string[] { "id", "deptcode", "", "sbPassword", "UpdateTime", "UpdateUser" };
                        xmlData = xmlHelper.ConvertDataRowToXML(dataRow);

                        str.AppendLine("<?xml version=\"1.0\" encoding=\"gb2312\"?>");
                        str.AppendFormat("<{0}>", "data");
                        str.AppendFormat("<{0}>", "result");
                        str.Append(xmlData);
                        str.AppendFormat("</{0}>", "result");
                        str.AppendFormat("</{0}>", "data");

                        string addResultSt = client.pushAJSBJG(deptcode, sbPassword, str.ToString());
                        BLLCommon.WriteLog("deptcode:" + deptcode);
                        BLLCommon.WriteLog("向省一站式申报平台推送安监申报结果:" + xmlData + "\n结果：" + addResultSt);

                        DataTable dt = DAL.GetTBData_SaveToStLog("Ap_ajsbjg", dataRow["id"].ToString());

                        if (dt.Rows.Count > 0)
                        {
                            row = dt.Rows[0];
                            row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            row["TableName"] = "Ap_ajsbjg";
                        }
                        else
                        {
                            row = dt.NewRow();
                            row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            row["UpdateDate"] = row["CreateDate"];
                            row["TableName"] = "Ap_ajsbjg";
                            row["PKID"] = dataRow["id"];
                            dt.Rows.Add(row);
                        }
                        if (addResultSt != "OK")
                        {
                            row["OperateState"] = 1;
                            row["Msg"] = addResultSt;
                        }
                        else
                        {
                            row["OperateState"] = 0;
                            row["Msg"] = "上传成功";
                        }
                        if (dt.Rows.Count > 0)
                        {
                           DAL.SaveTBData_SaveToStLog(dt);
                        }

                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            BLLCommon.WriteLog(ex.Message);
                        }
                        catch
                        {

                        }
                    }

                    result.code = ProcessResult.数据保存成功;
                    result.message = "数据保存成功！";
                }
                else
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "网络故障，数据保存失败！";
                }
            }
            else
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = "非法格式的数据！";
            }
            return result;
        }

        #endregion

        #region 推送质监申报结果

        /// <summary>
        /// 向省一站式申报平台推送质监申报结果
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dt_Data"></param>
        /// <returns></returns>
        public ProcessResultData pushZJSBJG(string user, string deptcode, string sbPassword, DataTable dt_Data, DataTable dwgcDt)
        {
            ProcessResultData result = new ProcessResultData();
            StringBuilder prjInnerNumSb = new StringBuilder();

            string msg = String.Empty;
            string[] fields = new string[] { "uuid", "success", "slry", "slrq" };

            DataRow item = dt_Data.Rows[0];
            List<string> novalidates = new List<string>();
            novalidates.Add(String.Empty);
            novalidates.Add(" ");
            novalidates.Add("无");
            novalidates.Add("无数据");
            novalidates.Add("/");

            if (BLLCommon.DataFieldIsNullOrEmpty(novalidates, fields, item, out msg))
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = msg + "不能为空！";
                return result;
            }

            DataTable dt_zjsbjg = DAL.GetAp_zjsbjg();
            DataRow row = dt_zjsbjg.NewRow();
            DataTableHelp.DataRow2DataRow(item, row);
            row["id"] = Guid.NewGuid();
            row["UpdateTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dt_zjsbjg.Rows.Add(row);
            row["deptcode"] = deptcode;
            row["sbPassword"] = sbPassword;
            row["UpdateUser"] = user;

            //保存单位工程列表
            DataTable dt_zjsbjg_dwgc = null;
            if (dwgcDt != null && dwgcDt.Rows.Count > 0)
            {
                dt_zjsbjg_dwgc = DAL.GetAp_zjsbjg_dwgc();

                foreach (DataRow dwgcItem in dwgcDt.Rows)
                {
                    DataRow dwgcRow = dt_zjsbjg_dwgc.NewRow();
                    DataTableHelp.DataRow2DataRow(dwgcItem, dwgcRow);
                    dwgcRow["uuid"] = row["uuid"];
                    dt_zjsbjg_dwgc.Rows.Add(dwgcRow);
                }
            }

            if (row["success"].ToString().Equals("Yes") && dt_zjsbjg_dwgc != null)
            {
                DAL.SaveAp_zjsbjg_dwgc(dt_zjsbjg_dwgc);
            }
            

            if (dt_zjsbjg.Rows.Count > 0)
            {
                if (DAL.SaveAp_zjsbjg(dt_zjsbjg))
                {
                    try
                    {
                        StringBuilder str = new StringBuilder();
                        string xmlData = "";
                        string dwgcXmlData = string.Empty;
                        DataRow dataRow = dt_zjsbjg.Rows[0]; 

                        //向省一站式申报平台推送安监申报结果
                        //string[] excludeColumns = new string[] { "id", "deptcode", "sbPassword", "UpdateTime", "UpdateUser" };
                        xmlData = xmlHelper.ConvertDataRowToXML(dataRow);

                        //string[] dwgcExcludeCols = new string[] { "id", "uuid" };
                        dwgcXmlData = xmlHelper.ConvertDataTableToXML(dwgcDt, "dwgcList", "dwgccontent");

                        str.AppendLine("<?xml version=\"1.0\" encoding=\"gb2312\"?>");
                        str.AppendFormat("<{0}>", "data");
                        str.AppendFormat("<{0}>", "result");
                        str.Append(xmlData);
                        //单位工程列表
                        if (row["success"].ToString().Equals("Yes"))
                        {
                            str.Append(dwgcXmlData);
                        }
                        str.AppendFormat("</{0}>", "result");
                        str.AppendFormat("</{0}>", "data");

                        string addResultSt = client.pushZJSBJG(deptcode, sbPassword, str.ToString());
                        BLLCommon.WriteLog("deptcode:" + deptcode);
                        BLLCommon.WriteLog("向省一站式申报平台推送安监申报结果:" + str.ToString() + "\n结果：" + addResultSt);
                        BLLCommon.WriteLog("id" + dataRow["id"].ToString());
                        DataTable dt = DAL.GetTBData_SaveToStLog("Ap_zjsbjg", dataRow["id"].ToString());

                        if (dt.Rows.Count > 0)
                        {
                            row = dt.Rows[0];
                            row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            row["TableName"] = "Ap_zjsbjg";
                        }
                        else
                        {
                            row = dt.NewRow();
                            row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            row["UpdateDate"] = row["CreateDate"];
                            row["TableName"] = "Ap_zjsbjg";
                            row["PKID"] = dataRow["id"];
                            dt.Rows.Add(row);
                        }
                        if (addResultSt != "OK")
                        {
                            row["OperateState"] = 1;
                            row["Msg"] = addResultSt;
                        }
                        else
                        {
                            row["OperateState"] = 0;
                            row["Msg"] = "上传成功";
                        }
                        if (dt.Rows.Count > 0)
                        {
                            DAL.SaveTBData_SaveToStLog(dt);
                        }

                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            BLLCommon.WriteLog(ex.Message);
                        }
                        catch
                        {

                        }
                    }

                    result.code = ProcessResult.数据保存成功;
                    result.message = "数据保存成功！";
                }
                else
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "网络故障，数据保存失败！";
                }
            }
            else
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = "非法格式的数据！";
            }
            return result;
        }

        #endregion


    }
}
