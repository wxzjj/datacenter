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
using Wxjzgcjczy.DAL.Sqlserver;

namespace Wxjzgcjczy.BLL
{
    /// <summary>
    /// 功能： 无锡数据中心与各县市系统数据交换业务处理类之一站式申报数据处理
    /// 作者：黄正宇
    /// 时间：2017-09-10
    /// </summary>
    public class DataExchangeBLLForYZSSBDownload
    {

        private readonly DataExchangeDALForYZSSBDownload DAL = new DataExchangeDALForYZSSBDownload();

        XmlHelper xmlHelper = new XmlHelper();

        ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient client = new ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient();


        public DataExchangeBLLForYZSSBDownload()
        {
            DAL.DB = new Wxjzgcjczy.DAL.Sqlserver.DatabaseOperator();
        }


        //质监
        public const string AP_ZJSBB = "Ap_zjsbb";
        public const string AP_ZJSBB_HT = "Ap_zjsbb_ht";
        public const string AP_ZJSBB_DWRY = "Ap_zjsbb_dwry";
        public const string AP_ZJSBB_SCHGS = "Ap_zjsbb_schgs";
        public const string AP_ZJSBB_DWGC = "Ap_zjsbb_dwgc";
        public const string AP_ZJSBB_CLQD = "Ap_zjsbb_clqd";

        //安监
        public const string AP_AJSBB = "Ap_ajsbb";
        public const string AP_AJSBB_HT = "Ap_ajsbb_ht";
        public const string AP_AJSBB_DWRY = "Ap_ajsbb_dwry";
        public const string AP_AJSBB_CLQD = "Ap_ajsbb_clqd";
        public const string AP_AJSBB_HJSSJD = "Ap_ajsbb_hjssjd";
        public const string AP_AJSBB_WXYJDGCQD = "Ap_ajsbb_wxyjdgcqd";
        public const string AP_AJSBB_CGMGCQD = "Ap_ajsbb_cgmgcqd";
  
        #region 拉取安监数据
 
        /// <summary>
        /// 从省一体化平台按uuid获取数据安监申报数据到无锡数据中心
        /// </summary>
        public string YourTask_PullAJSBDataFromSythptByUUID(string deptCode , string password, string uuid)
        {
            string apiMessage = string.Empty;
 
            DataTable dtapizb = DAL.Get_API_zb_apiFlowDetail("0");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                try
                {
                    ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient client = new ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient();
                    //根据uuid获取安监申报详细数据
                    string getDetailDataXml = String.Empty;

                    BLLCommon.WriteLog("YourTask_PullAJSBDataFromSythptByUUID：" + uuid);
                    getDetailDataXml = client.getAJSBBByUuid(deptCode, password, uuid);
                    //BLLCommon.WriteLog("结果：" + getDetailDataXml);
                    if (getDetailDataXml.Contains("<?xml version=\"1.0\" encoding=\"gb2312\"?>"))
                    {
                        apiMessage = "success";
                        getDetailDataXml = getDetailDataXml.Replace("<?xml version=\"1.0\" encoding=\"gb2312\"?>", "").Replace("<body>", "").Replace("</body>", "");
                        saveAJSBXmlDataToDb(uuid, deptCode, getDetailDataXml, DateTime.Now);

                    }

                }
                catch (Exception ex)
                {
                    BLLCommon.WriteLog("执行YourTask_PullAJSBDataFromSythptByUUID方法出现异常:" + ex.Message);
                    apiMessage += ex.Message;
                } 
            }
            return apiMessage;
        }

        public string saveAJSBXmlDataToDb(string uuid ,string user, string xmlData, DateTime pullDate)
        {
            string message = string.Empty;
            int index = -1;
            string mainList = string.Empty;
            string htList = string.Empty;
            string dwryList = string.Empty;
            string clList = string.Empty;
            string hjssjdList = string.Empty;
            string wxygcList = string.Empty;
            string cgmgcList = string.Empty;

            //安监监督申报表
            index = xmlData.IndexOf("<mainList>");
            if (index >= 0)
            {
                mainList = xmlData.Substring(index, xmlData.LastIndexOf("</mainList>") - index + "</mainList>".Length);
                saveMainListXmlDataToDb(user, mainList, pullDate);
            }
            //勘察、设计、施工、监理合同列表
            index = xmlData.IndexOf("<htList>");
            if (index >= 0)
            {
                htList = xmlData.Substring(index, xmlData.LastIndexOf("</htList>") - index + "</htList>".Length);
                saveHtListXmlDataToDb(uuid, htList);
            }

            //责任单位及人员列表
            index = xmlData.IndexOf("<dwryList>");
            if (index >= 0)
            {
                dwryList = xmlData.Substring(index, xmlData.LastIndexOf("</dwryList>") - index + "</dwryList>".Length);
                saveDwryListXmlDataToDb(uuid, dwryList);
            }

            //材料清单
            index = xmlData.IndexOf("<clList>");
            if (index >= 0)
            {
                clList = xmlData.Substring(index, xmlData.LastIndexOf("</clList>") - index + "</clList>".Length);
                saveClListXmlDataToDb(uuid, clList);
            }
            //施工现场周边环境和地下设施情况交底表
            index = xmlData.IndexOf("<hjssjdList>");
            if (index >= 0)
            {
                hjssjdList = xmlData.Substring(index, xmlData.LastIndexOf("</hjssjdList>") - index + "</hjssjdList>".Length);
                saveHjssjdListXmlDataToDb(uuid, hjssjdList);
            }
            //危险源较大分项工程清单
            index = xmlData.IndexOf("<wxygcList>");
            if (index >= 0)
            {
                wxygcList = xmlData.Substring(index, xmlData.LastIndexOf("</wxygcList>") - index + "</wxygcList>".Length);
                saveWxygcListXmlDataToDb(uuid, wxygcList);
            }
            //超规模危险源分项工程清单
            index = xmlData.IndexOf("<cgmgcList>");
            if (index >= 0)
            {
                cgmgcList = xmlData.Substring(index, xmlData.LastIndexOf("</cgmgcList>") - index + "</cgmgcList>".Length);
                saveCgmgcListXmlDataToDb(uuid, cgmgcList);
            }
            return "success";
        }

        public string saveMainListXmlDataToDb(string user, string xmlData, DateTime pullDate)
        {
            try
            {
                string message = string.Empty;
                DataTable mainDt = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(xmlData, out message);
                if (mainDt == null || mainDt.Rows.Count < 1)
                {
                    return message;
                }
                else
                {
                    //一个安监申报表编号，对应一条安监申报表
                    DataRow item = mainDt.Rows[0];

                    DataTable dt_Ap_ajsbb = DAL.Get_Ap_ajsbb(item["uuid"].ToString2());
                    DataRow toSaveRow;

                    if (dt_Ap_ajsbb != null && dt_Ap_ajsbb.Rows.Count > 0)
                    {
                        toSaveRow = dt_Ap_ajsbb.Rows[0];

                        int cmpFlag = DateTime.Compare(item["updateDate"].ToDateTime(), toSaveRow["updateDate"].ToDateTime());
                        //BLLCommon.WriteLog("====" + toSaveRow["Status"] + "|cmpFlag:" + cmpFlag);

                        if ((!string.IsNullOrEmpty(toSaveRow["Status"].ToString()) && toSaveRow["Status"].ToInt32() != 0) && cmpFlag > 0)
                        {
                            //重新提交,审批状态清零
                            toSaveRow["Status"] = 0;
                        }

                        DataTableHelp.DataRow2DataRow(item, toSaveRow, new List<string>() { "uuid" });

                    }
                    else
                    {
                        toSaveRow = dt_Ap_ajsbb.NewRow();
                        DataTableHelp.DataRow2DataRow(item, toSaveRow);
                        dt_Ap_ajsbb.Rows.Add(toSaveRow);
                    }
                    //只有申报表有UpdateTime跟UpdateUser
                    toSaveRow["UpdateTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    toSaveRow["UpdateUser"] = user;
                    toSaveRow["FetchDate"] = pullDate;

                    //BLLCommon.WriteLog("====" + toSaveRow["uuid"] + toSaveRow["PrjNum"]);

                    if (dt_Ap_ajsbb.Rows.Count > 0)
                    {
                        if (!DAL.Save_Ap_ajsbb(dt_Ap_ajsbb))
                        {
                            //保存失败的错误处理
                            BLLCommon.WriteLog("Save_Ap_ajsbb:fail");
                        }
                    }

                    return "success";

                }
            }
            catch (Exception ex)
            {
                BLLCommon.WriteLog("执行saveMainListXmlDataToDb方法出现异常:" + ex.Message);
                return "fail";
            }
            
        }

        public string saveHtListXmlDataToDb(string uuid, string xmlData)
        {
            try
            {
                string message = string.Empty;
                DataTable dt = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(xmlData, out message);
                if (dt == null || dt.Rows.Count < 1)
                {
                    return message;
                }
                else
                {
                    DAL.Delete_ApTable(AP_AJSBB_HT, uuid);
                    DataTable existDt = DAL.Get_ApTable(AP_AJSBB_HT);

                    foreach (DataRow item in dt.Rows)
                    {
                        DataRow toSaveRow = existDt.NewRow();
                        DataTableHelp.DataRow2DataRow(item, toSaveRow);

                        if(!string.IsNullOrEmpty(toSaveRow["contractMoney"].ToString())
                            && !string.IsNullOrEmpty(toSaveRow["recordNum"].ToString()) ){
                            existDt.Rows.Add(toSaveRow);
                        }        

                        //由于文档跟实际获取的xml不一致，特殊处理字段
                        //toSaveRow["CorpCode"] = item["contractorCorpCode"];
                        //toSaveRow["CorpName"] = item["contractorCorpName"];
                        //toSaveRow["RecordNum"] = item["recordNum"];
                        //toSaveRow["xmfzrsfzh"] = item["iDCard"];
                        //toSaveRow["xmfzr"] = item["prjHead"];

                    }
                    if (existDt.Rows.Count > 0)
                    {
                        if (!DAL.Save_Ap_ajsbb_ht(existDt))
                        {
                            //保存失败的错误处理
                            BLLCommon.WriteLog("Save_Ap_ajsbb:fail");
                        }
                    }
                    return "success";

                }
            }
            catch (Exception ex)
            {
                BLLCommon.WriteLog("执行saveHtListXmlDataToDb方法出现异常:" + ex.Message);
                return "fail";
            }
             
            
        }

        public string saveDwryListXmlDataToDb(string uuid ,string xmlData)
        {
            string message = string.Empty;
            DataTable dt = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(xmlData, out message);
            if (dt == null || dt.Rows.Count < 1)
            {
                return message;
            }
            else
            {
                //插入数据前删除列表记录
                DAL.Delete_ApTable(AP_AJSBB_DWRY, uuid);
                DataTable existDt = DAL.Get_ApTable(AP_AJSBB_DWRY);

                foreach (DataRow item in dt.Rows)
                {
                    DataRow toSaveRow = existDt.NewRow();
                    DataTableHelp.DataRow2DataRow(item, toSaveRow);
                    existDt.Rows.Add(toSaveRow);
                }
                if (existDt.Rows.Count > 0)
                {
                    //TODO, 循环保存单条数据，有待重构
                    if (!DAL.Save_Ap_ajsbb_dwry(existDt))
                    {
                        //保存失败的错误处理
                        BLLCommon.WriteLog("Save_Ap_ajsbb:fail");
                    }
                }
                return "success";

            }
        }

        public string saveClListXmlDataToDb(string uuid , string xmlData)
        {
            string message = string.Empty;
            DataTable dt = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(xmlData, out message);
            if (dt == null || dt.Rows.Count < 1)
            {
                return message;
            }
            else
            {
                //插入数据前删除列表记录
                DAL.Delete_ApTable(AP_AJSBB_CLQD, uuid);
                DataTable existDt = DAL.Get_ApTable(AP_AJSBB_CLQD);

                foreach (DataRow item in dt.Rows)
                {
                    DataRow toSaveRow = existDt.NewRow();
                    DataTableHelp.DataRow2DataRow(item, toSaveRow);
                    existDt.Rows.Add(toSaveRow);
                }
                if (existDt.Rows.Count > 0)
                {
                    //TODO, 循环保存单条数据，有待重构
                    if (!DAL.Save_Ap_ajsbb_clqd(existDt))
                    {
                        //保存失败的错误处理
                        BLLCommon.WriteLog("Save_Ap_ajsbb:fail");
                    }
                }
                return "success";

            }
        }

        public string saveHjssjdListXmlDataToDb(string uuid , string xmlData)
        {
            string message = string.Empty;
            DataTable dt = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(xmlData, out message);
            if (dt == null || dt.Rows.Count < 1)
            {
                return message;
            }
            else
            {
                //插入数据前删除列表记录
                DAL.Delete_ApTable(AP_AJSBB_HJSSJD, uuid);
                DataTable existDt = DAL.Get_ApTable(AP_AJSBB_HJSSJD);

                foreach (DataRow item in dt.Rows)
                {
                    DataRow toSaveRow = existDt.NewRow();
                    DataTableHelp.DataRow2DataRow(item, toSaveRow);
                    existDt.Rows.Add(toSaveRow);

                }
                if (existDt.Rows.Count > 0)
                {
                    //TODO, 循环保存单条数据，有待重构
                    if (!DAL.Save_Ap_ajsbb_hjssjd(existDt))
                    {
                        //保存失败的错误处理
                        BLLCommon.WriteLog("Save_Ap_ajsbb:fail");
                    }
                }
                return "success";

            }
        }

        public string saveWxygcListXmlDataToDb(string uuid , string xmlData)
        {
            string message = string.Empty;
            DataTable dt = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(xmlData, out message);
            if (dt == null || dt.Rows.Count < 1)
            {
                return message;
            }
            else
            {
                //插入数据前删除列表记录
                DAL.Delete_ApTable(AP_AJSBB_WXYJDGCQD, uuid);
                DataTable existDt = DAL.Get_ApTable(AP_AJSBB_WXYJDGCQD);

                foreach (DataRow item in dt.Rows)
                {
                    DataRow toSaveRow = existDt.NewRow();
                    DataTableHelp.DataRow2DataRow(item, toSaveRow);
                    existDt.Rows.Add(toSaveRow);

                }
                if (existDt.Rows.Count > 0)
                {
                    //TODO, 循环保存单条数据，有待重构
                    if (!DAL.Save_Ap_ajsbb_wxyjdgcqd(existDt))
                    {
                        //保存失败的错误处理
                        BLLCommon.WriteLog("Save_Ap_ajsbb:fail");
                    }
                }
                return "success";

            }
        }

        public string saveCgmgcListXmlDataToDb(string uuid , string xmlData)
        {
            string message = string.Empty;
            DataTable dt = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(xmlData, out message);
            if (dt == null || dt.Rows.Count < 1)
            {
                return message;
            }
            else
            {
                //插入数据前删除列表记录
                DAL.Delete_ApTable(AP_AJSBB_CGMGCQD, uuid);
                DataTable existDt = DAL.Get_ApTable(AP_AJSBB_CGMGCQD);
                foreach (DataRow item in dt.Rows)
                {
                    DataRow toSaveRow = existDt.NewRow();
                    DataTableHelp.DataRow2DataRow(item, toSaveRow);
                    existDt.Rows.Add(toSaveRow);

                }
                if (existDt.Rows.Count > 0)
                {
                    //TODO, 循环保存单条数据，有待重构
                    if (!DAL.Save_Ap_ajsbb_cgmgcqd(existDt))
                    {
                        //保存失败的错误处理
                        BLLCommon.WriteLog("Save_Ap_ajsbb:fail");
                    }
                }
                return "success";

            }
        }

        /// <summary>
        /// 手动触发按uuid下行安监申报数据
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public string PullAJSBDataFromSythptByUUID(string uuid)
        {
            string deptCode = null, password = null;

            DataTable ajsbbTable = DAL.Get_Ap_ajsbb(uuid);

            if (ajsbbTable.Rows.Count > 0){
                DataRow row = ajsbbTable.Rows[0];
                deptCode = row["AjCorpCode"].ToString();

                DataTable apiUserTable = DAL.GetApApiUserByDeptCode(deptCode);

                if (apiUserTable.Rows.Count > 0)
                {
                    DataRow apiRow = apiUserTable.Rows[0];
                    password = apiRow["password"].ToString();

                    return YourTask_PullAJSBDataFromSythptByUUID(deptCode, password, uuid);
                }
                else {
                    return "机构代码为" + deptCode + "的安监机构在库中不存在";
                }

            }else {
                return "uuid为" + uuid + "的安监申报表在库中不存在";
            }     
            
        }

        /// <summary>
        /// 手动触发按uuid下行安监申报数据
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public string PullZJSBDataFromSythptByUUID(string uuid)
        {
            string deptCode = null, password = null;

            DataTable zjsbbTable = DAL.Get_Ap_zjsbb(uuid);

            if (zjsbbTable.Rows.Count > 0)
            {
                DataRow row = zjsbbTable.Rows[0];
                deptCode = row["ZjCorpCode"].ToString();

                DataTable apiUserTable = DAL.GetApApiUserByDeptCode(deptCode);

                if (apiUserTable.Rows.Count > 0)
                {
                    DataRow apiRow = apiUserTable.Rows[0];
                    password = apiRow["password"].ToString();

                    return YourTask_PullZJSBDataFromSythptByUUID(deptCode, password, uuid);
                }
                else
                {
                    return "机构代码为" + deptCode + "的质监机构在库中不存在";
                }

            }
            else
            {
                return "uuid为" + uuid + "的质监申报表在库中不存在";
            }

        }

        #endregion

        #region 拉取质监数据

        /// <summary>
        /// 从省一体化平台按uuid获取质监申报数据到无锡数据中心
        /// </summary>
        public string YourTask_PullZJSBDataFromSythptByUUID(string deptCode, string password, string uuid)
        {
            string apiMessage = string.Empty;

            DataTable dtapizb = DAL.Get_API_zb_apiFlowDetail("0");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                try
                {
                    ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient client = new ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient();
                    //根据uuid获取安监申报详细数据
                    string getDetailDataXml = String.Empty;

                    BLLCommon.WriteLog("YourTask_PullZJSBDataFromSythptByUUID：" + uuid);
                    //根据uuid获取质监申报详细数据

                    getDetailDataXml = client.getZJSBBByUuid(deptCode, password, uuid);
                    //BLLCommon.WriteLog("getZJSBBByUuid结果：" + getDetailDataXml);
                    if (getDetailDataXml.Contains("<?xml version=\"1.0\" encoding=\"gb2312\"?>"))
                    {
                        apiMessage = "success";
                        getDetailDataXml = getDetailDataXml.Replace("<?xml version=\"1.0\" encoding=\"gb2312\"?>", "").Replace("<body>", "").Replace("</body>", "");
                        saveZJSBXmlDataToDb(uuid , deptCode, getDetailDataXml, DateTime.Now);

                    }

                }
                catch (Exception ex)
                {
                    BLLCommon.WriteLog("执行YourTask_PullZJSBDataFromSythptByUUID方法出现异常:" + ex.Message);
                    apiMessage += ex.Message;
                }
            }
            return apiMessage;
        }

        public string saveZJSBXmlDataToDb(string uuid , string user, string xmlData, DateTime pullDate)
        {
            string message = string.Empty;
            int index = -1;
            string mainList = string.Empty;
            string htList = string.Empty;
            string dwryList = string.Empty;
            string sgtscList = string.Empty;
            string dwgcList = string.Empty;
            string clList = string.Empty;


            //质监监督申报表
            index = xmlData.IndexOf("<mainList>");
            if (index >= 0)
            {
                mainList = xmlData.Substring(index, xmlData.LastIndexOf("</mainList>") - index + "</mainList>".Length);
                saveZJMainListXmlDataToDb(user, mainList, pullDate);
            }
            //勘察、设计、施工、监理合同列表
            index = xmlData.IndexOf("<htList>");
            if (index >= 0)
            {
                htList = xmlData.Substring(index, xmlData.LastIndexOf("</htList>") - index + "</htList>".Length);
                saveZJHtListXmlDataToDb(uuid, htList);
            }

            //责任单位及人员列表
            index = xmlData.IndexOf("<dwryList>");
            if (index >= 0)
            {
                dwryList = xmlData.Substring(index, xmlData.LastIndexOf("</dwryList>") - index + "</dwryList>".Length);
                saveZJDwryListXmlDataToDb(uuid, dwryList);
            }

            //施工图审核合格书列表
            index = xmlData.IndexOf("<sgtscList>");
            if (index >= 0)
            {
                sgtscList = xmlData.Substring(index, xmlData.LastIndexOf("</sgtscList>") - index + "</sgtscList>".Length);
                saveZJSgtscListXmlDataToDb(uuid, sgtscList);
            }
            //单位工程列表
            index = xmlData.IndexOf("<dwgcList>");
            if (index >= 0)
            {
                dwgcList = xmlData.Substring(index, xmlData.LastIndexOf("</dwgcList>") - index + "</dwgcList>".Length);
                saveZJDwgcListXmlDataToDb(uuid, dwgcList);
            }

            //材料清单
            index = xmlData.IndexOf("<clList>");
            if (index >= 0)
            {
                clList = xmlData.Substring(index, xmlData.LastIndexOf("</clList>") - index + "</clList>".Length);
                saveZJClListXmlDataToDb(uuid, clList);
            }

            return "success";
        }

        public string saveZJMainListXmlDataToDb(string user, string xmlData, DateTime pullDate)
        {
            string message = string.Empty;
            DataTable mainDt = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(xmlData, out message);
            if (mainDt == null || mainDt.Rows.Count < 1)
            {
                return message;
            }
            else
            {
                //一个质监申报表编号，对应一条质监申报表
                DataRow item = mainDt.Rows[0];

                DataTable dt_Ap_zjsbb = DAL.Get_Ap_zjsbb(item["uuid"].ToString2());
                DataRow toSaveRow;

                if (dt_Ap_zjsbb != null && dt_Ap_zjsbb.Rows.Count > 0)
                {
                    toSaveRow = dt_Ap_zjsbb.Rows[0];
                    //BLLCommon.WriteLog("====" + toSaveRow["uuid"] + "|" +  toSaveRow["Status"]);

                    int cmpFlag = DateTime.Compare(item["updateDate"].ToDateTime() , toSaveRow["updateDate"].ToDateTime());
                    //BLLCommon.WriteLog("====" + toSaveRow["Status"] + "|cmpFlag:" + cmpFlag);

                    if ((!string.IsNullOrEmpty(toSaveRow["Status"].ToString()) && toSaveRow["Status"].ToInt32() != 0) && cmpFlag > 0)
                    {
                        //重新提交,审批状态清零
                        toSaveRow["Status"] = 0;
                    }

                    DataTableHelp.DataRow2DataRow(item, toSaveRow, new List<string>() { "uuid" });

                }
                else
                {
                    toSaveRow = dt_Ap_zjsbb.NewRow();
                    DataTableHelp.DataRow2DataRow(item, toSaveRow);
                    toSaveRow["Status"] = 0;
                    dt_Ap_zjsbb.Rows.Add(toSaveRow);
                }
                //只有申报表有UpdateTime跟UpdateUser
                toSaveRow["UpdateTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                toSaveRow["UpdateUser"] = user;
                toSaveRow["FetchDate"] = pullDate;


                //BLLCommon.WriteLog("====" + toSaveRow["uuid"] + toSaveRow["PrjNum"]);

                if (dt_Ap_zjsbb.Rows.Count > 0)
                {
                    if (!DAL.Save_Ap_sbb(dt_Ap_zjsbb, AP_ZJSBB))
                    {
                        //保存失败的错误处理
                        BLLCommon.WriteLog("Save_Ap_ajsbb:fail");
                    }
                }

                return "success";

            }
        }

        public string saveZJHtListXmlDataToDb(string uuid, string xmlData)
        {
            string message = string.Empty;
            DataTable dt = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(xmlData, out message);
            if (dt == null || dt.Rows.Count < 1)
            {
                return message;
            }
            else
            {
                DAL.Delete_ApTable(AP_ZJSBB_HT, uuid);
                DataTable existDt = DAL.Get_ApTable(AP_ZJSBB_HT);

                foreach (DataRow item in dt.Rows)
                {
                    DataRow toSaveRow = existDt.NewRow();
                    DataTableHelp.DataRow2DataRow(item, toSaveRow);
                    existDt.Rows.Add(toSaveRow);

                    //由于文档跟实际获取的xml不一致，特殊处理字段
                    //toSaveRow["CorpCode"] = item["contractorCorpCode"];
                    //toSaveRow["CorpName"] = item["contractorCorpName"];
                    //toSaveRow["RecordNum"] = item["sgzcbhtbam"];
                    //toSaveRow["xmfzrsfzh"] = item["iDCard"];
                    //toSaveRow["xmfzr"] = item["prjHead"];

                    //BLLCommon.WriteLog("====" + toSaveRow["uuid"] + toSaveRow["RecordNum"]);

                }
                if (existDt.Rows.Count > 0)
                {
                    if (!DAL.Save_Ap_sbb(existDt, AP_ZJSBB_HT))
                    {
                        //保存失败的错误处理
                        BLLCommon.WriteLog("Save_Ap_ajsbb:fail");
                    }
                }
                return "success";

            }
        }

        public string saveZJDwryListXmlDataToDb(string uuid, string xmlData)
        {
            string message = string.Empty;
            DataTable dt = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(xmlData, out message);
            if (dt == null || dt.Rows.Count < 1)
            {
                return message;
            }
            else
            {
                DAL.Delete_ApTable(AP_ZJSBB_DWRY, uuid);
                DataTable existDt = DAL.Get_ApTable(AP_ZJSBB_DWRY);
                foreach (DataRow item in dt.Rows)
                {
                    DataRow  toSaveRow = existDt.NewRow();
                    DataTableHelp.DataRow2DataRow(item, toSaveRow);
                    existDt.Rows.Add(toSaveRow);

                }
                if (existDt.Rows.Count > 0)
                {
                    if (!DAL.Save_Ap_sbb(existDt, AP_ZJSBB_DWRY))
                    {
                        //保存失败的错误处理
                        BLLCommon.WriteLog("Save_Ap_ajsbb:fail");
                    }
                }
                message = "success";

            }
            return message;
        }

        public string saveZJSgtscListXmlDataToDb(string uuid, string xmlData)
        {
            string message = string.Empty;
            DataTable dt = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(xmlData, out message);
            if (dt == null || dt.Rows.Count < 1)
            {
                return message;
            }
            else
            {
                DAL.Delete_ApTable(AP_ZJSBB_SCHGS, uuid);
                DataTable existDt = DAL.Get_ApTable(AP_ZJSBB_SCHGS);
                foreach (DataRow item in dt.Rows)
                {
                    DataRow toSaveRow = existDt.NewRow();
                    DataTableHelp.DataRow2DataRow(item, toSaveRow);
                    existDt.Rows.Add(toSaveRow);
                }
                if (existDt.Rows.Count > 0)
                {
                    if (!DAL.Save_Ap_sbb(existDt, AP_ZJSBB_SCHGS))
                    {
                        //保存失败的错误处理
                        BLLCommon.WriteLog("Save_Ap_ajsbb:fail");
                    }
                }
                return "success";

            }
        }

        public string saveZJDwgcListXmlDataToDb(string uuid , string xmlData)
        {
            string message = string.Empty;
            DataTable dt = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(xmlData, out message);
            if (dt == null || dt.Rows.Count < 1)
            {
                return message;
            }
            else
            {
                DAL.Delete_ApTable(AP_ZJSBB_DWGC, uuid);
                DataTable existDt = DAL.Get_ApTable(AP_ZJSBB_DWGC);
                foreach (DataRow item in dt.Rows)
                {
                    DataRow toSaveRow = existDt.NewRow();
                    DataTableHelp.DataRow2DataRow(item, toSaveRow);
                    existDt.Rows.Add(toSaveRow);

                }
                if (existDt.Rows.Count > 0)
                {
                    if (!DAL.Save_Ap_sbb(existDt, AP_ZJSBB_DWGC))
                    {
                        //保存失败的错误处理
                        BLLCommon.WriteLog("Save_Ap_ajsbb:fail");
                    }
                }
                return "success";

            }
        }

        public string saveZJClListXmlDataToDb(string uuid , string xmlData)
        {
            string message = string.Empty;
            DataTable dt = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(xmlData, out message);
            if (dt == null || dt.Rows.Count < 1)
            {
                return message;
            }
            else
            {
                DAL.Delete_ApTable(AP_ZJSBB_CLQD, uuid);
                DataTable existDt = DAL.Get_ApTable(AP_ZJSBB_CLQD);
                foreach (DataRow item in dt.Rows)
                {
                    DataRow toSaveRow = existDt.NewRow();
                    DataTableHelp.DataRow2DataRow(item, toSaveRow);
                    existDt.Rows.Add(toSaveRow);
                }
                if (existDt.Rows.Count > 0)
                {
                    if (!DAL.Save_Ap_sbb(existDt, AP_ZJSBB_CLQD))
                    {
                        //保存失败的错误处理
                        BLLCommon.WriteLog("Save_Ap_ajsbb:fail");
                    }
                }

                return "success";

            }
        }

        #endregion


        /// <summary>
        /// 生成日志
        /// </summary>
        /// <param name="uuid"></param>
        public void createMonitorLog(DataExchangeDALForYZSSBDownload DAL, DataTable dt_DataJkDataDetail, string dataJkLogId,
            string tableName, string methodName)
        {
            DataRow row_DataJkDataDetail = dt_DataJkDataDetail.NewRow();
            dt_DataJkDataDetail.Rows.Add(row_DataJkDataDetail);
            Int64 id = DAL.Get_DataJkDataDetailNewID().ToInt64();
            row_DataJkDataDetail["ID"] = id;
            row_DataJkDataDetail["DataJkLogID"] = dataJkLogId;
            row_DataJkDataDetail["tableName"] = tableName;
            row_DataJkDataDetail["MethodName"] = methodName;
        }

        

    }
}
