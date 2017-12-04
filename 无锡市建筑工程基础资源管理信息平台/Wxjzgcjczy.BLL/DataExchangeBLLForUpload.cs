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
    /// 功能：手动往省厅上传数据
    /// 作者：黄正宇
    /// 时间：2017-11-29
    /// </summary>
    public class DataExchangeBLLForUpload
    {
        string userName = "320200", password = "we&gjh45H";

        private readonly Wxjzgcjczy.DAL.Sqlserver.DataExchangeDALForUpload DAL = new Wxjzgcjczy.DAL.Sqlserver.DataExchangeDALForUpload();

        XmlHelper xmlHelper = new XmlHelper();

        ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient client = new ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient();


        public DataExchangeBLLForUpload()
        {
            DAL.DB = new Wxjzgcjczy.DAL.Sqlserver.DatabaseOperator();
        }

        public DataTable GetTBData_TBProjectAdditionalInfo(string prjNum)
        {
            return DAL.GetTBData_TBProjectAdditionalInfo(prjNum);
        }


        #region 往省厅推送信息

        /// <summary>
        /// 往省厅推送项目信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dt_Data"></param>
        /// <returns></returns>
        public ProcessResultData SaveTBData_TBProjectInfo(string user, string pKID)
        {
            ProcessResultData result = new ProcessResultData();

            DataTable dt_TBProjectInfo = DAL.GetTBData_TBProjectInfoByPKID(pKID);
            DataRow row;

            if (dt_TBProjectInfo.Rows.Count > 0)
            {
                DataRow dataRow = dt_TBProjectInfo.Rows[0];

                try
                {
                    string xmlData = "";

                    dataRow["sbdqbm"] = "320200";
                    if (dataRow["BuildCorpCode"].ToString2().Length == 18)
                    {
                        string shxydm = dataRow["BuildCorpCode"].ToString2();
                        dataRow["BuildCorpCode"] = shxydm.Substring(8, 8) + "-" + shxydm.Substring(16, 1);
                    }

                    xmlData = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);
                    BLLCommon.WriteLog("SaveTBData_TBProjectInfo 上传省厅数据：" + xmlData);
                    string resultSt = client.SaveTBDataToSt("TBProjectInfo", xmlData, userName, password);
                    //string resultSt = "OK";
                    BLLCommon.WriteLog("\nSaveTBData_TBProjectInfo 上传省厅结果：" + resultSt);

                    DataTable dt = DAL.GetTBData_SaveToStLog("TBProjectInfo", dataRow["PKID"].ToString());

                    if (dt.Rows.Count > 0)
                    {
                        row = dt.Rows[0];
                        row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        row["TableName"] = "TBProjectInfo";
                    }
                    else
                    {
                        row = dt.NewRow();
                        row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        row["UpdateDate"] = row["CreateDate"];
                        row["TableName"] = "TBProjectInfo";
                        row["PKID"] = dataRow["PKID"];
                        dt.Rows.Add(row);
                    }
                    if (resultSt != "OK")
                    {
                        row["OperateState"] = 1;
                        row["Msg"] = resultSt;
                        result.code = ProcessResult.保存失败和失败原因;
                    }
                    else
                    {
                        row["OperateState"] = 0;
                        row["Msg"] = "上传成功";
                        result.code = ProcessResult.数据保存成功;
                    }
                    result.message = resultSt;
                    if (dt.Rows.Count > 0)
                    {
                        DAL.SaveTBData_SaveToStLog(dt);
                    }

                }
                catch (Exception ex)
                {
                    BLLCommon.WriteLog("执行SaveTBData_TBProjectInfo异常:" + ex.Message);
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = ex.Message;
                }

            }

            return result;
        }

        /// <summary>
        /// 往省厅推送项目补充信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dt_Data"></param>
        /// <returns></returns>
        public ProcessResultData SaveTBData_TBProjectAddInfo(string user, string prjNum)
        {
            ProcessResultData result = new ProcessResultData();

            DataTable dt_TBProjectAddInfo = DAL.GetTBData_TBProjectAdditionalInfo(prjNum);
            DataRow row;
            string[] fields = new string[] { "prjnum", "prjpassword", "gyzzpl", "dzyx", "lxr", "yddh", "xmtz", "gytze", "gytzbl", "lxtzze", "sbdqbm" };


            if (dt_TBProjectAddInfo.Rows.Count > 0)
            {
                DataRow dataRow = dt_TBProjectAddInfo.Rows[0];

                string xmlData = "";
                dataRow["sbdqbm"] = "320200";//设置上报地区编码为无锡市

                DataTable dt = DAL.GetTBData_SaveToStLog("TBProjectAdditionalInfo", dataRow["PKID"].ToString());
                if (dt.Rows.Count > 0)
                {
                    row = dt.Rows[0];
                    row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    row["TableName"] = "TBProjectAdditionalInfo";
                }
                else
                {
                    row = dt.NewRow();
                    row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    row["UpdateDate"] = row["CreateDate"];
                    row["TableName"] = "TBProjectAdditionalInfo";
                    row["PKID"] = dataRow["PKID"];
                    dt.Rows.Add(row);
                }
                try
                {
                    //向省一体化平台传送项目登记补充数据
                    xmlData = xmlHelper.ConvertDataRowToXMLWithBase64EncodingIncludeForAddPrj(dataRow, fields);
                    string addResultSt = client.getProjectAdd(dataRow["prjnum"].ToString(), xmlData, userName, password);
                    //string addResultSt = "OK";
                    BLLCommon.WriteLog("向省一体化平台传送项目登记补充数据:" + xmlData + "\n结果1：" + addResultSt);

                    if (addResultSt != "OK")
                    {
                        row["OperateState"] = 1;
                        row["Msg"] = addResultSt;
                        result.code = ProcessResult.保存失败和失败原因;
                    }
                    else
                    {
                        row["OperateState"] = 0;
                        row["Msg"] = "上传成功";
                        result.code = ProcessResult.数据保存成功;
                    }

                    result.message = addResultSt;
                }
                catch (Exception ex)
                {
                    BLLCommon.WriteLog(ex.Message);
                    row["OperateState"] = 1;
                    row["Msg"] = ex.Message;
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = ex.Message;
                }
                finally
                {
                    if (dt.Rows.Count > 0)
                    {
                        DAL.SaveTBData_SaveToStLog(dt);
                    }
                }
            }
            else
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = "请先填写项目补充信息.";
            }

            return result;
        }

        #endregion
        

    }
}
