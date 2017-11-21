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
        //安监
        public const string AP_AJZZGZ = "Ap_ajzzgz";


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

        public DataTable GetAp_ajsbb_byuuid(string uuid)
        {
            DataTable dt = DAL.GetAp_ajsbb_byuuid(uuid);
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

        public DataTable GetAp_zjsbb_byDeptCode(string date, string deptCode)
        {
            DataTable dt = DAL.GetAp_zjsbb_byDeptCode(date, deptCode);
            return dt;
        }

        public DataTable GetAp_zjsbb_byuuid(string uuid)
        {
            DataTable dt = DAL.GetAp_zjsbb_byuuid(uuid);
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
                            result.code = ProcessResult.保存失败和失败原因;
                            result.message ="上传到省厅失败：" +  addResultSt;
                        }
                        else
                        {
                            row["OperateState"] = 0;
                            row["Msg"] = "上传成功";
                            result.code = ProcessResult.数据保存成功;
                            result.message = "数据保存成功！";
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
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = ex.Message;
                        return result;
                    }
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

        #region 推送安监通知书
        /// <summary>
        /// 推送安监通知书
        /// </summary>
        /// <param name="user"></param>
        /// <param name="deptcode"></param>
        /// <param name="sbPassword"></param>
        /// <param name="dt_Data"></param>
        /// <returns></returns>
        public ProcessResultData pushAJTZS(string user, string deptcode, string sbPassword, DataTable dt_Data, DataTable jdryDtData)
        {
            ProcessResultData result = new ProcessResultData();
            StringBuilder prjInnerNumSb = new StringBuilder();

            string msg = String.Empty;
            string[] fields = new string[] { "uuid", "tzrq", "tzdw" };

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

            DataTable dt_ajtzs = DAL.GetApTable("Ap_ajtzs");
            DataRow row = dt_ajtzs.NewRow();
            DataTableHelp.DataRow2DataRow(item, row);

            row["id"] = Guid.NewGuid();
            row["UpdateTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dt_ajtzs.Rows.Add(row);
            row["deptcode"] = deptcode;
            row["sbPassword"] = sbPassword;
            row["UpdateUser"] = user;

            //保存监督人员列表
            DataTable dt_ajtzs_jdry = null;
            if (jdryDtData != null && jdryDtData.Rows.Count > 0)
            {
                dt_ajtzs_jdry = DAL.GetApTable("Ap_ajtzs_jdry");

                foreach (DataRow jdryItem in jdryDtData.Rows)
                {
                    DataRow jdryRow = dt_ajtzs_jdry.NewRow();
                    DataTableHelp.DataRow2DataRow(jdryItem, jdryRow);
                    jdryRow["id"] = Guid.NewGuid();
                    jdryRow["uuid"] = row["uuid"];
                    dt_ajtzs_jdry.Rows.Add(jdryRow);
                } 
            }

            if (dt_ajtzs.Rows.Count > 0)
            {
                if (DAL.SaveAp_ajtzs(dt_ajtzs) && DAL.SaveAp_ajtzs_jdry(dt_ajtzs_jdry))
                {
                    try
                    {
                        StringBuilder str = new StringBuilder();
                        string xmlData = string.Empty;
                        string jdryXmlData = string.Empty;
                        DataRow dataRow = dt_ajtzs.Rows[0];

                        //向省一站式申报平台推送安监通知书
                        List<string> excludeColumns = new List<string>();
                        excludeColumns.Add("id");
                        excludeColumns.Add("deptcode");
                        excludeColumns.Add("sbPassword");
                        excludeColumns.Add("UpdateTime");
                        excludeColumns.Add("UpdateUser");
                        xmlData = xmlHelper.ConvertDataRowToXML(dataRow, excludeColumns);
                        

                        str.AppendLine("<?xml version=\"1.0\" encoding=\"gb2312\"?>");
                        str.AppendFormat("<{0}>", "data");
                        str.AppendFormat("<{0}>", "result");
                        str.Append(xmlData);
                       
                        foreach (DataRow jdryItem in jdryDtData.Rows)
                        {
                            str.AppendFormat("<{0}>", "jdryList");
                            DataRow jdryRow = dt_ajtzs_jdry.NewRow();
                            jdryXmlData = xmlHelper.ConvertDataRowToXML(jdryItem);
                            str.Append(jdryXmlData);
                            str.AppendFormat("</{0}>", "jdryList");
                        }
                        
                        str.AppendFormat("</{0}>", "result");
                        str.AppendFormat("</{0}>", "data");

                        string addResultSt = client.pushAJTZS(deptcode, sbPassword, str.ToString());
                        BLLCommon.WriteLog("deptcode:" + deptcode);
                        BLLCommon.WriteLog("向省一站式申报平台推送安监监督通知书:" + str.ToString() + "\n结果：" + addResultSt);

                        DataTable dt = DAL.GetTBData_SaveToStLog("Ap_ajtzs", dataRow["id"].ToString());

                        if (dt.Rows.Count > 0)
                        {
                            row = dt.Rows[0];
                            row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            row["TableName"] = "Ap_ajtzs";
                        }
                        else
                        {
                            row = dt.NewRow();
                            row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            row["UpdateDate"] = row["CreateDate"];
                            row["TableName"] = "Ap_ajtzs";
                            row["PKID"] = dataRow["id"];
                            dt.Rows.Add(row);
                        }
                        if (addResultSt != "OK")
                        {
                            row["OperateState"] = 1;
                            row["Msg"] = addResultSt;
                            result.code = ProcessResult.保存失败和失败原因;
                            result.message = "数据上传省厅失败：" + addResultSt;
                        }
                        else
                        {
                            row["OperateState"] = 0;
                            row["Msg"] = "上传成功";
                            result.code = ProcessResult.数据保存成功;
                            result.message = "数据保存成功！";
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
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = ex.Message;
                        return result;
                    }

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

        #region 推送终止施工安全监督告知书

        /// <summary>
        /// 向省一站式申报平台推送终止施工安全监督告知书
        /// </summary>
        /// <param name="user"></param>
        /// <param name="deptcode"></param>
        /// <param name="sbPassword"></param>
        /// <param name="dt_Data"></param>
        /// <returns></returns>
        public ProcessResultData pushAJZZGZ(string user, string deptcode, string sbPassword, DataTable dt_Data)
        {
            ProcessResultData result = new ProcessResultData();
            StringBuilder prjInnerNumSb = new StringBuilder();

            string msg = String.Empty;
            string[] fields = new string[] { "uuid", "zzyy", "zzrq" };

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

            DataTable dt_ajzzgz = DAL.GetApTable(AP_AJZZGZ);
            DataRow row = dt_ajzzgz.NewRow();
            DataTableHelp.DataRow2DataRow(item, row);

            row["id"] = Guid.NewGuid();
            row["UpdateTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dt_ajzzgz.Rows.Add(row);
            row["deptcode"] = deptcode;
            row["sbPassword"] = sbPassword;
            row["UpdateUser"] = user;

            if (dt_ajzzgz.Rows.Count > 0)
            {
                if (DAL.SaveApTable(AP_AJZZGZ, dt_ajzzgz))
                {
                    try
                    {
                        StringBuilder str = new StringBuilder();
                        string xmlData = "";
                        DataRow dataRow = dt_ajzzgz.Rows[0];

                        //向省一站式申报平台推送安监申报结果
                        List<string> excludeColumns = new List<string>();
                        excludeColumns.Add("id");
                        excludeColumns.Add("deptcode");
                        excludeColumns.Add("sbPassword");
                        excludeColumns.Add("UpdateTime");
                        excludeColumns.Add("UpdateUser");
                        xmlData = xmlHelper.ConvertDataRowToXML(dataRow, excludeColumns);

                        str.AppendLine("<?xml version=\"1.0\" encoding=\"gb2312\"?>");
                        str.AppendFormat("<{0}>", "data");
                        str.AppendFormat("<{0}>", "result");
                        str.Append(xmlData);
                        str.AppendFormat("</{0}>", "result");
                        str.AppendFormat("</{0}>", "data");

                        string addResultSt = client.pushAJZZGZ(deptcode, sbPassword, str.ToString());
                        BLLCommon.WriteLog("deptcode:" + deptcode);
                        BLLCommon.WriteLog("向省一站式申报平台推送终止施工安全监督告知书:" + str.ToString() + "\n结果：" + addResultSt);

                        DataTable dt = DAL.GetTBData_SaveToStLog(AP_AJZZGZ, dataRow["id"].ToString());

                        if (dt.Rows.Count > 0)
                        {
                            row = dt.Rows[0];
                            row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            row["TableName"] = AP_AJZZGZ;
                        }
                        else
                        {
                            row = dt.NewRow();
                            row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            row["UpdateDate"] = row["CreateDate"];
                            row["TableName"] = AP_AJZZGZ;
                            row["PKID"] = dataRow["id"];
                            dt.Rows.Add(row);
                        }
                        if (addResultSt != "OK")
                        {
                            row["OperateState"] = 1;
                            row["Msg"] = addResultSt;
                            result.code = ProcessResult.保存失败和失败原因;
                            result.message = "数据上传省厅失败：" + addResultSt;
                        }
                        else
                        {
                            row["OperateState"] = 0;
                            row["Msg"] = "上传成功";
                            result.code = ProcessResult.数据保存成功;
                            result.message = "数据保存成功！";
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
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = ex.Message;
                        return result;
                    }

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
                        //向省一站式申报平台推送质监申报结果
                        DataRow dataRow = dt_zjsbjg.Rows[0]; 

                        //string[] excludeColumns = new string[] { "id", "deptcode", "sbPassword", "UpdateTime", "UpdateUser" };
                        xmlData = xmlHelper.ConvertDataRowToXML(dataRow);

                        //string[] dwgcExcludeCols = new string[] { "id", "uuid" };
                        List<string> dwgcExcludeCols = new List<string>();
                        dwgcExcludeCols.Add("id");
                        dwgcExcludeCols.Add("uuid");
                        //dwgcXmlData = xmlHelper.ConvertDataTableToXML(dwgcDt, "dwgcList", "dwgccontent");

                        str.AppendLine("<?xml version=\"1.0\" encoding=\"gb2312\"?>");
                        str.AppendFormat("<{0}>", "data");
                        str.AppendFormat("<{0}>", "result");
                        str.Append(xmlData);
                        //单位工程列表
                        if (row["success"].ToString().Equals("Yes"))
                        {
                            if (dwgcDt != null && dwgcDt.Rows.Count > 0)
                            {
                                foreach (DataRow dwgcItem in dwgcDt.Rows)
                                {
                                    str.AppendFormat("<{0}>", "dwgcList");
                                    dwgcXmlData = xmlHelper.ConvertDataRowToXML(dwgcItem, dwgcExcludeCols);
                                    str.Append(dwgcXmlData);
                                    str.AppendFormat("</{0}>", "dwgcList");
                                }
                                str.Append(dwgcXmlData);
                            }
                        }
                        str.AppendFormat("</{0}>", "result");
                        str.AppendFormat("</{0}>", "data");

                        string addResultSt = client.pushZJSBJG(deptcode, sbPassword, str.ToString());
                        //string addResultSt = string.Empty;
                        BLLCommon.WriteLog("deptcode:" + deptcode);
                        BLLCommon.WriteLog("向省一站式申报平台推送质监申报结果:" + str.ToString() + "\n结果：" + addResultSt);
                        BLLCommon.WriteLog("id" + dataRow["id"].ToString());
                        DataTable dt = DAL.GetTBData_SaveToStLog("Ap_zjsbjg", dataRow["id"].ToString());
                        DataRow logRow = null;
                        if (dt.Rows.Count > 0)
                        {
                            logRow = dt.Rows[0];
                            logRow["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            logRow["TableName"] = "Ap_zjsbjg";
                        }
                        else
                        {
                            logRow = dt.NewRow();
                            logRow["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            logRow["UpdateDate"] = logRow["CreateDate"];
                            logRow["TableName"] = "Ap_zjsbjg";
                            logRow["PKID"] = dataRow["id"];
                            dt.Rows.Add(logRow);
                        }
                        if (addResultSt != "OK")
                        {
                            logRow["OperateState"] = 1;
                            logRow["Msg"] = addResultSt;
                            result.code = ProcessResult.保存失败和失败原因;
                            result.message = "数据上传省厅失败：" + addResultSt;
                        }
                        else
                        {
                            //跟新质监申报结果审批状态
                            if (row["success"].ToString().Equals("Yes"))
                            {
                                updateSBStatus(row["uuid"].ToString2(), 2);
                            }
                            else
                            {
                                updateSBStatus(row["uuid"].ToString2(), 1);
                            }

                            logRow["OperateState"] = 0;
                            logRow["Msg"] = "上传成功";
                            result.code = ProcessResult.数据保存成功;
                            result.message = "数据保存成功！";
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
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = ex.Message;
                        return result;
                    }

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

        #region 推送质监通知书
        /// <summary>
        /// 推送质监通知书
        /// </summary>
        /// <param name="user"></param>
        /// <param name="deptcode"></param>
        /// <param name="sbPassword"></param>
        /// <param name="dt_Data"></param>
        /// <returns></returns>
        public ProcessResultData pushZJTZS(string user, string deptcode, string sbPassword, DataTable dt_Data, DataTable jdryDtData)
        {
            ProcessResultData result = new ProcessResultData();
            StringBuilder prjInnerNumSb = new StringBuilder();

            string msg = String.Empty;
            string[] fields = new string[] { "uuid", "tzrq", "tzdw" };

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

            DataTable dt_ajtzs = DAL.GetApTable("Ap_zjtzs");
            DataRow row = dt_ajtzs.NewRow();
            DataTableHelp.DataRow2DataRow(item, row);

            row["id"] = Guid.NewGuid();
            row["UpdateTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dt_ajtzs.Rows.Add(row);
            row["deptcode"] = deptcode;
            row["sbPassword"] = sbPassword;
            row["UpdateUser"] = user;

            //保存监督人员列表
            DataTable dt_tzs_jdry = null;
            if (jdryDtData != null && jdryDtData.Rows.Count > 0)
            {
                dt_tzs_jdry = DAL.GetApTable("Ap_zjtzs_jdry");

                foreach (DataRow jdryItem in jdryDtData.Rows)
                {
                    DataRow jdryRow = dt_tzs_jdry.NewRow();
                    DataTableHelp.DataRow2DataRow(jdryItem, jdryRow);
                    jdryRow["id"] = row["id"];
                    jdryRow["uuid"] = row["uuid"];
                    dt_tzs_jdry.Rows.Add(jdryRow);
                }
            }

            if (dt_ajtzs.Rows.Count > 0)
            {
                if (DAL.SaveApTable("Ap_zjtzs", dt_ajtzs) && DAL.SaveApTable("Ap_zjtzs_jdry", dt_tzs_jdry))
                {
                    try
                    {
                        StringBuilder str = new StringBuilder();
                        string xmlData = string.Empty;
                        string jdryXmlData = string.Empty;
                        DataRow dataRow = dt_ajtzs.Rows[0];

                        //向省一站式申报平台推送质监通知书
                        List<string> excludeColumns = new List<string>();
                        excludeColumns.Add("id");
                        excludeColumns.Add("deptcode");
                        excludeColumns.Add("sbPassword");
                        excludeColumns.Add("UpdateTime");
                        excludeColumns.Add("UpdateUser");
                        xmlData = xmlHelper.ConvertDataRowToXML(dataRow, excludeColumns);


                        str.AppendLine("<?xml version=\"1.0\" encoding=\"gb2312\"?>");
                        str.AppendFormat("<{0}>", "data");
                        str.AppendFormat("<{0}>", "result");
                        str.Append(xmlData);

                        foreach (DataRow jdryItem in jdryDtData.Rows)
                        {
                            str.AppendFormat("<{0}>", "jdryList");
                            DataRow jdryRow = dt_tzs_jdry.NewRow();
                            jdryXmlData = xmlHelper.ConvertDataRowToXML(jdryItem);
                            str.Append(jdryXmlData);
                            str.AppendFormat("</{0}>", "jdryList");
                        }

                        str.AppendFormat("</{0}>", "result");
                        str.AppendFormat("</{0}>", "data");

                        string addResultSt = client.pushZJTZS(deptcode, sbPassword, str.ToString());
                        BLLCommon.WriteLog("deptcode:" + deptcode);
                        BLLCommon.WriteLog("向省一站式申报平台推送质监监督通知书:" + str.ToString() + "\n结果：" + addResultSt);

                        DataTable dt = DAL.GetTBData_SaveToStLog("Ap_zjtzs", dataRow["id"].ToString());

                        if (dt.Rows.Count > 0)
                        {
                            row = dt.Rows[0];
                            row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            row["TableName"] = "Ap_zjtzs";
                        }
                        else
                        {
                            row = dt.NewRow();
                            row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            row["UpdateDate"] = row["CreateDate"];
                            row["TableName"] = "Ap_zjtzs";
                            row["PKID"] = dataRow["id"];
                            dt.Rows.Add(row);
                        }
                        if (addResultSt != "OK")
                        {
                            row["OperateState"] = 1;
                            row["Msg"] = addResultSt;
                            result.code = ProcessResult.保存失败和失败原因;
                            result.message = "数据上传省厅失败：" + addResultSt;
                        }
                        else
                        {
                            row["OperateState"] = 0;
                            row["Msg"] = "上传成功";
                            result.code = ProcessResult.数据保存成功;
                            result.message = "数据保存成功！";
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
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = ex.Message;
                        return result;
                    }
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


        public void updateSBStatus(string uuid , int status)
        {
            DataTable dt = DAL.GetAp_zjsbb_single_byuuid(uuid);
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Rows[0]["Status"] = status;
                DAL.SaveAp_zjsbb(dt);
            }
        }

    }
}
