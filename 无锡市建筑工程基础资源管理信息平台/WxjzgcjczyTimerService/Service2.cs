using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using WxsjzxTimerService;
using WxsjzxTimerService.Common;
using System.Configuration;
using System.Timers;
using Bigdesk8;
using System.Web.Script.Serialization;
using System.Collections;
using System.Net;
using System.IO;

namespace WxjzgcjczyTimerService
{
    partial class Service2 : ServiceBase
    {
        public Service2()
        {
            InitializeComponent();
        }
        System.Timers.Timer myTimer;
        int timeSpan = 1;
        DataService dataService = new DataService();
        XmlHelper xmlHelper = new XmlHelper();
        ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient client = new ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient();
        string userName = "320200", password = "we&gjh45H";
        string userName_qyry = "320200", password_qyry = "W123YheAge";
        public bool isRunning;
        public object obj = "111";

        const string fileName = "往省一体化平台推送";

        protected override void OnStart(string[] args)
        {
            //定义定时器 
            string timeSpanStr = ConfigurationManager.AppSettings["timeSpan_ToSythpt"];

            if (!Int32.TryParse(timeSpanStr, out timeSpan))
            {
                timeSpan = 1;
            }

            isRunning = false;

            System.Timers.Timer myTimer = new System.Timers.Timer(1000 * 60 * timeSpan);
            myTimer.Elapsed += new ElapsedEventHandler(myTimer_Elapsed);
            myTimer.Enabled = true;
            myTimer.AutoReset = true;
            myTimer.Start();
        }


        void myTimer_Elapsed(object source, ElapsedEventArgs e)
        {
            try
            {
                List<string> setTimes = ConfigurationManager.AppSettings["setTime_ToSythpt"].ToString().Split(',').ToList();
                int f = 0;
                for (int i = 0; i < setTimes.Count; i++)
                {
                    int hour = setTimes[i].Substring(0, 2).ToInt32();
                    int minute = setTimes[i].Substring(2, 2).ToInt32();

                    if (DateTime.Now.Hour == hour && DateTime.Now.Minute < minute + timeSpan && DateTime.Now.Minute >= minute)
                    {
                        if (isRunning)
                        {
                            WriteLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "上一次服务正在运行中。。。");
                            WriteLog("结束记录日志:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            return;
                        }
                        lock (obj)
                        {
                            isRunning = true;
                        }

                        f = 1;
                        break;
                    }
                }

                if (f == 1)
                {
                    WriteLog("开始记录日志:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    YourTask_PullDataFromSythpt();     //从省一体化平台获取数据（江阴立项项目，施工合同备案）到无锡数据中心-2016.9.21 原本在service1,因为service1耗时太长而放到此

                    #region 推送数据

                    YourTask_PushDataToSxxzx_Xmhjxx(); //往省厅数据中心推送项目各环节数据

                    WriteLog("\r\n");

                    #endregion

                    lock (obj)
                    {
                        isRunning = false;
                    }
                    WriteLog("结束记录日志:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }
            catch (Exception ex)
            {
                lock (obj)
                {
                    isRunning = false;
                }
                WriteLog(ex.Message);
            }
        }

        /// <summary>
        /// 往省厅数据中心推送项目各环节数据
        /// </summary>
        void YourTask_PushDataToSxxzx_Xmhjxx()
        {
            string apiMessage = string.Empty;// 2016.10.21
            //string methodMessage = string.Empty;
            DataTable dtapizb = dataService.Get_API_zb_apiFlow("27");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                try
                {
                    //往数据监控日志表添加一条记录
                    DataTable dt_DataJkLog = dataService.GetSchema_DataJkLog();
                    DataRow row_DataJkLog = dt_DataJkLog.NewRow();
                    dt_DataJkLog.Rows.Add(row_DataJkLog);
                    row_DataJkLog["ID"] = dataService.Get_DataJkLogNewID();
                    row_DataJkLog["DataFlow"] = DataFlow.无锡数据中心到省一体化平台.ToInt32();
                    row_DataJkLog["DataFlowName"] = DataFlow.无锡数据中心到省一体化平台.ToString();
                    row_DataJkLog["ServiceUrl"] = "";
                    row_DataJkLog["csTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dataService.Submit_DataJkLog(dt_DataJkLog);

                    //往数据监控日志表项添加一条记录
                    DataTable dt_DataJkDataDetail = dataService.GetSchema_DataJkDataDetail();
                    long Id_DataJkDataDetail = dataService.Get_DataJkDataDetailNewID().ToInt64();

                    DateTime beginTime = DateTime.Now;
                    WriteLog("开始执行YourTask_PushDataToSxxzx_Xmhjxx任务:" + beginTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (ConfigurationManager.AppSettings["IsPush_TBProjectInfo"].ToString2() == "1")
                        UploadToSt_TBProjectInfo(dt_DataJkDataDetail, row_DataJkLog["ID"].ToString2(), Id_DataJkDataDetail++);
                    if (ConfigurationManager.AppSettings["IsPush_TBTenderInfo"].ToString2() == "1")
                        UploadToSt_TBTenderInfo(dt_DataJkDataDetail, row_DataJkLog["ID"].ToString2(), Id_DataJkDataDetail++);

                    if (ConfigurationManager.AppSettings["IsPush_aj_gcjbxx"].ToString2() == "1")
                        UploadToSt_aj_gcjbxx(dt_DataJkDataDetail, row_DataJkLog["ID"].ToString2(), Id_DataJkDataDetail++);
                    if (ConfigurationManager.AppSettings["IsPush_zj_gcjbxx"].ToString2() == "1")
                        UploadToSt_zj_gcjbxx(dt_DataJkDataDetail, row_DataJkLog["ID"].ToString2(), Id_DataJkDataDetail++);

                    if (ConfigurationManager.AppSettings["IsPush_zj_gcjbxx_zrdw"].ToString2() == "1")
                        UploadToSt_zj_gcjbxx_zrdw(dt_DataJkDataDetail, row_DataJkLog["ID"].ToString2(), Id_DataJkDataDetail++);

                    if (dt_DataJkDataDetail.Rows.Count > 0)
                    {
                        dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail);
                    }

                    DateTime endTime = DateTime.Now;
                    TimeSpan span1 = new TimeSpan(beginTime.Hour, beginTime.Minute, beginTime.Second);
                    TimeSpan span2 = new TimeSpan(endTime.Hour, endTime.Minute, endTime.Second);
                    TimeSpan span = span2 - span1;

                    WriteLog(string.Format("结束YourTask_PushDataToSxxzx_Xmhjxx任务:{0}，历时：{1}时{2}分{3}秒", endTime.ToString("yyyy-MM-dd HH:mm:ss"), span.Hours, span.Minutes, span.Seconds));
                }
                catch (Exception ex)
                {
                    WriteLog("执行YourTask_PushDataToSSjzx方法出现异常:" + ex.Message);
                    apiMessage += ex.Message;
                }

                finally
                {
                    DataTable dtapicb = dataService.GetSchema_API_cb();
                    DataRow row_apicb = dtapicb.NewRow();
                    dtapicb.Rows.Add(row_apicb);
                    row_apicb["apiCbID"] = dataService.Get_apiCbNewID();
                    row_apicb["apiFlow"] = "27";
                    row_apicb["apiMethod"] = "GetTBData_TBProjectInfo;GetTBData_TBTenderInfo;GetTBData_SaveToStLog;GetTBData_zj_gcjbxx";
                    row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                    row_apicb["apiDyMessage"] = apiMessage;
                    row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dataService.Submit_API_cb(dtapicb);

                    dataService.UpdateZbJkzt("27", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

                }
            }
        }


        #region 拉取数据

        /// <summary>
        /// 从省一体化平台获取数据（江阴立项项目，施工合同备案）到无锡数据中心
        /// </summary>
        void YourTask_PullDataFromSythpt()
        {
            string apiMessage = string.Empty;
            //string methodMessage = string.Empty;
            DataService dataService = new DataService();
            DataTable dtapizb = dataService.Get_API_zb_apiFlow("0");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                try
                {
                    DateTime beginTime = DateTime.Now;
                    Public.WriteLog("开始执行YourTask_PullDataFromSythpt任务:" + beginTime.ToString("yyyy-MM-dd HH:mm:ss"));

                    XmlHelper xmlHelper = new XmlHelper();

                    //往数据监控日志表添加一条记录
                    DataTable dt_DataJkLog = dataService.GetSchema_DataJkLog();
                    DataRow row_DataJkLog = dt_DataJkLog.NewRow();
                    dt_DataJkLog.Rows.Add(row_DataJkLog);
                    row_DataJkLog["ID"] = dataService.Get_DataJkLogNewID();
                    row_DataJkLog["DataFlow"] = DataFlow.省一体化平台到无锡数据中心.ToInt32();
                    row_DataJkLog["DataFlowName"] = DataFlow.省一体化平台到无锡数据中心.ToString();
                    row_DataJkLog["ServiceUrl"] = "http://58.213.147.230:8000/tDataService/ReceiveDataService.ws?wsdl";

                    row_DataJkLog["csTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    dataService.Submit_DataJkLog(dt_DataJkLog);

                    ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient client = new ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient();
                    #region  从省一体化平台获取江阴地区的立项项目信息
                    string userName_jyxm = "320281", password_jyxm = "RTn&53o";

                    string beginDate = DateTime.Now.AddDays(-10).ToString("yyyy/MM/dd 00:00:00");//"2015/01/01 00:00:00";
                    string endDate = DateTime.Now.ToString("yyyy/MM/dd 23:00:00");

                    string xmlData_lxxm = "";

                    //往数据监控日志表项添加一条记录
                    DataTable dt_DataJkDataDetail = dataService.GetSchema_DataJkDataDetail();
                    DataRow row_DataJkDataDetail = dt_DataJkDataDetail.NewRow();
                    dt_DataJkDataDetail.Rows.Add(row_DataJkDataDetail);

                    Int64 id = dataService.Get_DataJkDataDetailNewID().ToInt64();

                    row_DataJkDataDetail["ID"] = id;
                    row_DataJkDataDetail["DataJkLogID"] = row_DataJkLog["ID"];
                    row_DataJkDataDetail["tableName"] = "TBProjectInfo";
                    row_DataJkDataDetail["MethodName"] = "TBProjectInfoTime";
                    int allCount_jyxm = 0, successCount_jyxm = 0;

                    try
                    {
                        xmlData_lxxm = client.TBProjectInfoTime(beginDate, endDate, userName_jyxm, password_jyxm);

                        row_DataJkDataDetail["IsOk"] = 1;

                        DataTable dt_SaveDataLog = dataService.GetSchema_SaveDataLog();

                        xmlData_lxxm = xmlData_lxxm.Replace("<?xml version=\"1.0\" encoding=\"gb2312\"?>", "").Replace("<ResultSet>", "").Replace("</ResultSet>", "");
                        string message_lxxm = String.Empty;
                        if (!string.IsNullOrEmpty(xmlData_lxxm.Trim()))
                        {
                            DataTable dt = xmlHelper.ConvertXMLToDataTable(xmlData_lxxm, out message_lxxm);
                            if (string.IsNullOrEmpty(message_lxxm))
                            {
                                string[] fields = new string[] { "PrjNum", "PrjInnerNum", "PrjName", "PrjTypeNum", "BuildCorpName", "BuildCorpCode", "CreateDate", "ProvinceNum", "CityNum", "CountyNum", "PrjPropertyNum", "PrjFunctionNum", "UpdateFlag", "sbdqbm" };
                                List<string> novalidates = new List<string>();
                                novalidates.Add(String.Empty);
                                novalidates.Add(" ");
                                novalidates.Add("无");
                                novalidates.Add("无数据");
                                novalidates.Add("/");

                                foreach (DataRow row in dt.Rows)
                                {
                                    if (!row["CountyNum"].ToString().Equals("320281"))
                                    {
                                        continue;
                                    }
                                    allCount_jyxm++;
                                    DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                                    dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                                    row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail["ID"];
                                    row_SaveDataLog["DataXml"] = xmlHelper.ConvertDataRowToXML(row);
                                    row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                    #region 保存合同备案
                                    try
                                    {
                                        string msg_lxxm = "";
                                        if (DataTableHelp.DataFieldIsNullOrEmpty(novalidates, fields, row, out msg_lxxm))
                                        {
                                            row_SaveDataLog["SaveState"] = 0;
                                            row_SaveDataLog["Msg"] = msg_lxxm + "等字段内容不合法！";
                                            row_SaveDataLog["PKID"] = row["PKID"];

                                            continue;
                                        }

                                        DataTable dt_TBProjectInfo = dataService.GetTBData_TBProjectInfo(row["PKID"].ToString());
                                        DataTable dt_log_TBProjectInfo = dataService.GetTBData_SaveToStLog("TBProjectInfo", row["PKID"].ToString());

                                        DataRow row_temp;
                                        if (dt_TBProjectInfo.Rows.Count > 0)
                                        {
                                            if (dt_log_TBProjectInfo.Rows.Count > 0 && dt_log_TBProjectInfo.Rows[0]["OperateState"].ToString2() != "2")
                                            {
                                                row_SaveDataLog["SaveState"] = 0;
                                                row_SaveDataLog["Msg"] = "此项目为本系统上传的项目";
                                                row_SaveDataLog["PKID"] = row["PKID"];

                                                continue;
                                            }
                                            row_temp = dt_TBProjectInfo.Rows[0];
                                        }
                                        else
                                        {
                                            DataTable dt_TBProjectInfoByPrjNum = dataService.GetTBData_TBProjectInfoByPrjNum(row["PrjNum"].ToString());
                                            if (dt_TBProjectInfoByPrjNum.Rows.Count > 0)
                                            {
                                                row_SaveDataLog["SaveState"] = 0;
                                                row_SaveDataLog["Msg"] = "出现与本库重复的项目，项目编号：" + row["PrjNum"] + "，项目名称：" + row["PrjName"] + "，请核实！";
                                                row_SaveDataLog["PKID"] = row["PKID"];

                                                continue;
                                            }
                                            row_temp = dt_TBProjectInfo.NewRow();
                                            dt_TBProjectInfo.Rows.Add(row_temp);

                                            row_temp["PKID"] = row["pKID"];
                                        }

                                        row_temp["PrjNum"] = row["prjNum"];
                                        row_temp["PrjInnerNum"] = row["prjInnerNum"];
                                        row_temp["PrjName"] = row["prjName"];
                                        row_temp["PrjTypeNum"] = row["prjTypeNum"];
                                        row_temp["BuildCorpName"] = row["buildCorpName"];

                                        //检查是否存在社会信用代码，若存在，则转化为社会信用代码
                                        if (row["buildCorpCode"].ToString2().Length != 18)
                                        {
                                            string jsdwShxydm = dataService.Get_UEPP_Jsdw_Shxydm(row["buildCorpCode"].ToString2());
                                            if (!string.IsNullOrEmpty(jsdwShxydm))
                                            {
                                                row_temp["BuildCorpCode"] = jsdwShxydm;
                                            }
                                            else
                                            {
                                                row_temp["BuildCorpCode"] = row["buildCorpCode"];
                                            }
                                        }
                                        else
                                        {
                                            row_temp["BuildCorpCode"] = row["buildCorpCode"];
                                        }

                                        row_temp["ProvinceNum"] = row["provinceNum"];
                                        row_temp["CityNum"] = row["cityNum"];
                                        row_temp["CountyNum"] = row["countyNum"];
                                        row_temp["PrjApprovalNum"] = row["prjApprovalNum"];
                                        row_temp["PrjApprovalLevelNum"] = row["prjApprovalLevelNum"];
                                        row_temp["BuldPlanNum"] = row["buldPlanNum"];
                                        row_temp["ProjectPlanNum"] = row["projectPlanNum"];

                                        if (row["allInvest"] == DBNull.Value || string.IsNullOrEmpty(row["allInvest"].ToString()))
                                            row_temp["AllInvest"] = DBNull.Value;
                                        else
                                            row_temp["AllInvest"] = row["allInvest"];

                                        if (row["allArea"] == DBNull.Value || string.IsNullOrEmpty(row["allArea"].ToString()))
                                            row_temp["AllArea"] = DBNull.Value;
                                        else
                                            row_temp["AllArea"] = row["allArea"];

                                        row_temp["PrjSize"] = row["prjSize"];
                                        row_temp["PrjPropertyNum"] = row["prjPropertyNum"];
                                        row_temp["PrjFunctionNum"] = row["prjFunctionNum"];
                                        if (row["bDate"] == DBNull.Value || string.IsNullOrEmpty(row["bDate"].ToString()))
                                            row_temp["BDate"] = DBNull.Value;
                                        else
                                            row_temp["BDate"] = row["bDate"];

                                        if (row["eDate"] == DBNull.Value || string.IsNullOrEmpty(row["eDate"].ToString()))
                                            row_temp["EDate"] = DBNull.Value;
                                        else
                                            row_temp["EDate"] = row["eDate"];
                                        row_temp["CreateDate"] = row["createDate"];
                                        row_temp["UpdateFlag"] = row["updateFlag"];
                                        row_temp["sbdqbm"] = row["sbdqbm"];

                                        if (dataService.Submit_TBProjectInfo(dt_TBProjectInfo))
                                        {
                                            successCount_jyxm++;
                                            row_SaveDataLog["SaveState"] = 1;
                                            row_SaveDataLog["Msg"] = "";

                                            #region  生成获取日志

                                            DataRow row_log;
                                            if (dt_log_TBProjectInfo.Rows.Count > 0)
                                            {
                                                row_log = dt_log_TBProjectInfo.Rows[0];
                                            }
                                            else
                                            {
                                                row_log = dt_log_TBProjectInfo.NewRow();
                                                dt_log_TBProjectInfo.Rows.Add(row_log);

                                                row_log["PKID"] = row["PKID"];
                                                row_log["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                            }
                                            row_log["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                            row_log["TableName"] = "TBProjectInfo";
                                            row_log["OperateState"] = 2;
                                            row_log["Msg"] = "数据来自省一体化平台江阴立项";
                                            dataService.Submit_SaveToStLog(dt_log_TBProjectInfo);
                                            #endregion
                                        }
                                        else
                                        {
                                            row_SaveDataLog["SaveState"] = 0;
                                            row_SaveDataLog["Msg"] = "编号为“" + row["prjNum"] + "”的江阴立项项目保存失败！";
                                        }

                                    }
                                    catch (Exception ex2)
                                    {

                                        row_SaveDataLog["SaveState"] = 0;
                                        row_SaveDataLog["Msg"] = ex2.Message;
                                        row_SaveDataLog["PKID"] = row["PKID"];
                                        //dataService.Submit_SaveDataLog(dt_SaveDataLog);

                                    }

                                    #endregion

                                }
                                if (dt_SaveDataLog.Rows.Count > 0)
                                    dataService.Submit_SaveDataLog(dt_SaveDataLog);
                            }

                        }
                        row_DataJkDataDetail["IsOk"] = 1;
                        row_DataJkDataDetail["ErrorMsg"] = "";

                    }
                    catch (Exception ex)
                    {
                        row_DataJkDataDetail["IsOk"] = 0;
                        row_DataJkDataDetail["ErrorMsg"] = ex.Message;
                        //methodMessage +="TBProjectInfoTime:"+ ex.Message+";";
                    }

                    row_DataJkDataDetail["allCount"] = allCount_jyxm;
                    row_DataJkDataDetail["successCount"] = successCount_jyxm;

                    dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail);


                    #endregion

                    #region 获取省一体化平台合同备案信息数据处理

                    string userName_htba = "320200", password_htba = "we&gjh45H";

                    DataTable dt_TBProjectInfo_all = dataService.GetTBData_AllTBProjectInfo();

                    //List<string> list_htlb = new List<string>() { "100", "200", "301", "302", "304", "400" };

                    DataTable dt_DataJkDataDetail2 = dataService.GetSchema_DataJkDataDetail();


                    //往数据监控日志表项添加一条记录
                    DataRow row_DataJkDataDetail2 = dt_DataJkDataDetail2.NewRow();
                    dt_DataJkDataDetail2.Rows.Add(row_DataJkDataDetail2);

                    row_DataJkDataDetail2["ID"] = ++id;
                    row_DataJkDataDetail2["DataJkLogID"] = row_DataJkLog["ID"];
                    row_DataJkDataDetail2["tableName"] = "TBContractRecordManage";
                    row_DataJkDataDetail2["MethodName"] = "TBContractRecordManage";

                    string msg_htba = "";
                    int allCount = 0, successCount = 0;
                    int is_OK = 0;
                    try
                    {
                        DataTable dt_SaveDataLog2 = dataService.GetSchema_SaveDataLog();

                        foreach (DataRow row in dt_TBProjectInfo_all.Rows)
                        {
                            string xmlData = String.Empty;

                            #region 一次服务调用

                            xmlData = client.TBContractRecordManage(row["PrjNum"].ToString(), "", userName_htba, password_htba);

                            is_OK = 1;
                            xmlData = xmlData.Replace("<?xml version=\"1.0\" encoding=\"gb2312\"?>", "").Replace("<ResultSet>", "").Replace("</ResultSet>", "");
                            string message_htba = String.Empty;
                            DataTable dt = xmlHelper.ConvertXMLToDataTable(xmlData, out message_htba);

                            if (!string.IsNullOrEmpty(message_htba))
                            {
                                row_DataJkDataDetail2["ErrorMsg"] = "解析xml出现错误：" + message_htba;
                                row_DataJkDataDetail2["IsOk"] = 0;
                                row_DataJkDataDetail2["allCount"] = allCount;
                                row_DataJkDataDetail2["successCount"] = successCount;

                                throw new Exception("解析xml出现错误：" + message_htba);

                            }
                            if (dt == null || dt.Rows.Count == 0)
                            {
                                continue;
                            }

                            foreach (DataRow row2 in dt.Rows)
                            {
                                //if (!list_htlb.Exists(p => p.Equals(row2["ContractTypeNum"].ToString2(), StringComparison.CurrentCultureIgnoreCase)))
                                //{
                                //    continue;
                                //}
                                allCount++;

                                //具体的数据记录状态记录日志
                                DataRow row_SaveDataLog2 = dt_SaveDataLog2.NewRow();
                                dt_SaveDataLog2.Rows.Add(row_SaveDataLog2);

                                row_SaveDataLog2["DataJkDataDetailID"] = row_DataJkDataDetail2["ID"];

                                row_SaveDataLog2["DataXml"] = xmlHelper.ConvertDataRowToXML(row2);
                                try
                                {
                                    DataTable dt_TBContractRecordManage;
                                    dt_TBContractRecordManage = dataService.GetTBData_TBContractRecordManage(row2["PKID"].ToString());
                                    DataRow row3;
                                    if (dt_TBContractRecordManage.Rows.Count == 0)
                                    {
                                        row3 = dt_TBContractRecordManage.NewRow();
                                        dt_TBContractRecordManage.Rows.Add(row3);
                                        DataTableHelp.DataRow2DataRow(row2, row3);
                                    }
                                    else
                                    {
                                        row3 = dt_TBContractRecordManage.Rows[0];
                                        DataTableHelp.DataRow2DataRow(row2, row3, new List<string>() { "PKID" });
                                    }
                                    string propietorCorpCode = row3["PropietorCorpCode"].ToString2();
                                    //检查建设单位是否存在社会信用代码，若存在，则转化为社会信用代码
                                    if (propietorCorpCode.Length != 18)
                                    {
                                        string jsdwShxydm = dataService.Get_UEPP_Jsdw_Shxydm(propietorCorpCode);
                                        if (!string.IsNullOrEmpty(jsdwShxydm))
                                        {
                                            row3["PropietorCorpCode"] = jsdwShxydm;
                                        }
                                        else
                                        {
                                            row3["PropietorCorpCode"] = propietorCorpCode;
                                        }
                                    }
                                    else
                                    {
                                        row3["PropietorCorpCode"] = propietorCorpCode;
                                    }

                                    string contractorCorpCode = row3["ContractorCorpCode"].ToString2();
                                    //检查该企业是否存在社会信用代码，若存在，则转化为社会信用代码
                                    if (contractorCorpCode.Length != 18)
                                    {
                                        string qyShxydm = dataService.Get_UEPP_Qyjbxx_Shxydm(contractorCorpCode);
                                        if (!string.IsNullOrEmpty(qyShxydm))
                                        {
                                            row3["ContractorCorpCode"] = qyShxydm;
                                        }
                                        else
                                        {
                                            row3["ContractorCorpCode"] = contractorCorpCode;
                                        }
                                    }
                                    else
                                    {
                                        row3["ContractorCorpCode"] = contractorCorpCode;
                                    }

                                    row3["PrjNum"] = row["PrjNum"];
                                    row3["Tag"] = Tag.省一体化平台.ToString();

                                    if (!dataService.Submit_TBContractRecordManage(dt_TBContractRecordManage))
                                    {
                                        row_SaveDataLog2["SaveState"] = 0;
                                        row_SaveDataLog2["Msg"] = "保存TBContractRecordManage数据失败：PKID:" + dt_TBContractRecordManage.Rows[0]["PKID"] + ",RecordNum:" + dt_TBContractRecordManage.Rows[0]["RecordNum"];

                                    }
                                    else
                                    {
                                        row_SaveDataLog2["Msg"] = "";
                                        row_SaveDataLog2["SaveState"] = 1;
                                        successCount++;
                                    }
                                    row_SaveDataLog2["PKID"] = row3["PKID"];

                                }
                                catch (Exception ex2)
                                {
                                    row_SaveDataLog2["SaveState"] = 0;
                                    row_SaveDataLog2["Msg"] = ex2.Message;
                                }

                                row_SaveDataLog2["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                            }



                            #endregion

                        }

                        if (dt_SaveDataLog2.Rows.Count > 0)
                        {
                            dataService.Submit_SaveDataLog(dt_SaveDataLog2);
                        }

                        row_DataJkDataDetail2["ErrorMsg"] = msg_htba;

                        if (is_OK > 0)
                        {
                            row_DataJkDataDetail2["IsOk"] = 1;
                        }
                        else
                        {
                            row_DataJkDataDetail2["IsOk"] = 0;
                        }
                        row_DataJkDataDetail2["allCount"] = allCount;
                        row_DataJkDataDetail2["successCount"] = successCount;

                    }
                    catch (Exception ex)
                    {
                        row_DataJkDataDetail2["IsOk"] = 0;
                        row_DataJkDataDetail2["allCount"] = allCount;
                        row_DataJkDataDetail2["successCount"] = successCount;
                        row_DataJkDataDetail2["ErrorMsg"] = ex.Message;
                        //methodMessage += "TBContractRecordManage:" + ex.Message + ";";
                    }

                    if (dt_DataJkDataDetail2.Rows.Count > 0)
                        dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail2);

                    #endregion

                    #region 获取省一体化平台竣工备案信息数据处理- 2016-6-30加上去的-沈桂平

                    string userName_jgba = "320200", password_jgba = "W123YheAge";
                    DataTable dt_DataJkDataDetail3 = dataService.GetSchema_DataJkDataDetail();


                    //往数据监控日志表项添加一条记录
                    DataRow row_DataJkDataDetail3 = dt_DataJkDataDetail3.NewRow();
                    dt_DataJkDataDetail3.Rows.Add(row_DataJkDataDetail3);

                    row_DataJkDataDetail3["ID"] = ++id;
                    row_DataJkDataDetail3["DataJkLogID"] = row_DataJkLog["ID"];
                    row_DataJkDataDetail3["tableName"] = "TBProjectFinishManage";
                    row_DataJkDataDetail3["MethodName"] = "TBProjectFinishManage";

                    string msg_jgba = "";
                    allCount = 0;
                    successCount = 0;
                    is_OK = 0;
                    try
                    {
                        DataTable dt_SaveDataLog3 = dataService.GetSchema_SaveDataLog();


                        string xmlData = String.Empty;

                        #region 竣工备案

                        xmlData = client.RetrieveData("TBProjectFinishManage", "", "2016-01-01", DateTime.Now.Date.ToString(), userName_jgba, password_jgba);

                        is_OK = 1;
                        xmlData = xmlData.Replace("<?xml version=\"1.0\" encoding=\"gb2312\"?>", "").Replace("<ResultSet>", "").Replace("</ResultSet>", "");
                        string message_jgba = String.Empty;
                        DataTable dt = xmlHelper.ConvertXMLToDataTable(xmlData, out message_jgba);

                        if (!string.IsNullOrEmpty(message_jgba))
                        {
                            row_DataJkDataDetail3["ErrorMsg"] = "解析xml出现错误：" + message_jgba;
                            row_DataJkDataDetail3["IsOk"] = 0;
                            row_DataJkDataDetail3["allCount"] = allCount;
                            row_DataJkDataDetail3["successCount"] = successCount;

                            throw new Exception("解析xml出现错误：" + message_jgba);

                        }
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row2 in dt.Rows)
                            {

                                allCount++;

                                //具体的数据记录状态记录日志
                                DataRow row_SaveDataLog3 = dt_SaveDataLog3.NewRow();
                                dt_SaveDataLog3.Rows.Add(row_SaveDataLog3);
                                row_SaveDataLog3["DataJkDataDetailID"] = row_DataJkDataDetail3["ID"];
                                row_SaveDataLog3["DataXml"] = xmlHelper.ConvertDataRowToXML(row2);
                                try
                                {
                                    DataTable dt_TBProjectFinishManage;
                                    dt_TBProjectFinishManage = dataService.GetTBData_TBProjectFinishManage(row2["PKID"].ToString());
                                    DataRow row3;
                                    if (dt_TBProjectFinishManage.Rows.Count == 0)
                                    {
                                        row3 = dt_TBProjectFinishManage.NewRow();
                                        dt_TBProjectFinishManage.Rows.Add(row3);
                                        DataTableHelp.DataRow2DataRow(row2, row3);
                                    }
                                    else
                                    {
                                        row3 = dt_TBProjectFinishManage.Rows[0];
                                        DataTableHelp.DataRow2DataRow(row2, row3, new List<string>() { "PKID" });
                                    }
                                    string QCCorpCode = row3["QCCorpCode"].ToString2();
                                    //检查质量检测机构是否存在社会信用代码，若存在，则转化为社会信用代码
                                    if (QCCorpCode.Length != 18)
                                    {
                                        string jsdwShxydm = dataService.Get_UEPP_Jsdw_Shxydm(QCCorpCode);
                                        if (!string.IsNullOrEmpty(jsdwShxydm))
                                        {
                                            row3["QCCorpCode"] = jsdwShxydm;
                                        }
                                        else
                                        {
                                            row3["QCCorpCode"] = QCCorpCode;
                                        }
                                    }
                                    else
                                    {
                                        row3["QCCorpCode"] = QCCorpCode;
                                    }



                                    row3["PrjNum"] = row2["PrjNum"];
                                    row3["Tag"] = Tag.省一体化平台.ToString();

                                    if (!dataService.Submit_TBProjectFinishManage(dt_TBProjectFinishManage))
                                    {
                                        row_SaveDataLog3["SaveState"] = 0;
                                        row_SaveDataLog3["Msg"] = "保存TBContractRecordManage数据失败：PKID:" + dt_TBProjectFinishManage.Rows[0]["PKID"] + ",RecordNum:" + dt_TBProjectFinishManage.Rows[0]["RecordNum"];

                                    }
                                    else
                                    {
                                        row_SaveDataLog3["Msg"] = "";
                                        row_SaveDataLog3["SaveState"] = 1;
                                        successCount++;
                                    }
                                    row_SaveDataLog3["PKID"] = row3["PKID"];

                                }
                                catch (Exception ex2)
                                {
                                    row_SaveDataLog3["SaveState"] = 0;
                                    row_SaveDataLog3["Msg"] = ex2.Message;
                                }

                                row_SaveDataLog3["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                            }
                        }


                        #endregion



                        if (dt_SaveDataLog3.Rows.Count > 0)
                        {
                            dataService.Submit_SaveDataLog(dt_SaveDataLog3);
                        }

                        row_DataJkDataDetail3["ErrorMsg"] = msg_jgba;

                        if (is_OK > 0)
                        {
                            row_DataJkDataDetail3["IsOk"] = 1;
                        }
                        else
                        {
                            row_DataJkDataDetail3["IsOk"] = 0;
                        }
                        row_DataJkDataDetail3["allCount"] = allCount;
                        row_DataJkDataDetail3["successCount"] = successCount;

                    }
                    catch (Exception ex)
                    {
                        row_DataJkDataDetail3["IsOk"] = 0;
                        row_DataJkDataDetail3["allCount"] = allCount;
                        row_DataJkDataDetail3["successCount"] = successCount;
                        row_DataJkDataDetail3["ErrorMsg"] = ex.Message;

                        //methodMessage += "TBProjectFinishManage:" + ex.Message + ";";
                    }

                    if (dt_DataJkDataDetail3.Rows.Count > 0)
                        dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail3);

                    #endregion

                    DateTime endTime = DateTime.Now;
                    TimeSpan span1 = new TimeSpan(beginTime.Year, beginTime.Month, beginTime.Second);
                    TimeSpan span2 = new TimeSpan(endTime.Year, endTime.Month, endTime.Second);
                    TimeSpan span = span2 - span1;
                    Public.WriteLog(string.Format("结束YourTask_PullDataFromSythpt任务:{0}，历时：{1}时{2}分{3}秒", endTime.ToString("yyyy-MM-dd HH:mm:ss"), span.Hours, span.Minutes, span.Seconds));
                }
                catch (Exception ex)
                {
                    Public.WriteLog("执行YourTask_PullDataFromSythpt方法出现异常:" + ex.Message);
                    apiMessage += ex.Message;
                }

                finally
                {
                    DataTable dtapicb = dataService.GetSchema_API_cb();
                    DataRow row_apicb = dtapicb.NewRow();
                    dtapicb.Rows.Add(row_apicb);
                    row_apicb["apiCbID"] = dataService.Get_apiCbNewID();
                    row_apicb["apiFlow"] = "0";
                    row_apicb["apiMethod"] = "TBProjectInfoTime;TBContractRecordManage;TBProjectFinishManage";
                    row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                    row_apicb["apiDyMessage"] = apiMessage;
                    row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dataService.Submit_API_cb(dtapicb);

                    dataService.UpdateZbJkzt("0", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

                }
            }
        }
        #endregion


        #region 往省厅数据中心推送各环节数据

        public void UploadToSt_TBProjectInfo(DataTable dt_DataJkDataDetail, string Id_DataJkLog, long Id_DataJkDataDetail)
        {

            DataRow row_DataJkDataDetail = dt_DataJkDataDetail.NewRow();
            dt_DataJkDataDetail.Rows.Add(row_DataJkDataDetail);

            row_DataJkDataDetail["ID"] = Id_DataJkDataDetail;
            row_DataJkDataDetail["DataJkLogID"] = Id_DataJkLog;
            row_DataJkDataDetail["tableName"] = "TBProjectInfo";
            row_DataJkDataDetail["MethodName"] = "UploadToSt_TBProjectInfo";
            row_DataJkDataDetail["bz"] = "往省一体化平台推送立项项目数据";
            int all_count = 0, success_count = 0;

            DataTable dt = dataService.GetTBData_TBProjectInfo();

            WriteLog("获取了 " + dt.Rows.Count + " 条TBProjectInfo数据！");

            if (dt.Rows.Count == 0)
            {
                return;
            }
            //StringBuilder str_pkid = new StringBuilder();
            //foreach (DataRow item in dt.Rows)
            //{
            //    str_pkid.AppendFormat("'{0}',", item["PKID"]);
            //}

            //if (str_pkid.Length > 0)
            //    str_pkid.Remove(str_pkid.Length - 1, 1);

            #region 保存至省厅（TBProjectInfo）
            try
            {
                string xmlData = "";

                DataRow row;
                foreach (DataRow dataRow in dt.Rows)
                {

                    dataRow["sbdqbm"] = "320200";

                    //因为省一体化平台不接收社会信用代码，这里处理将社会信用代码转化为组织机构代码
                    if (dataRow["BuildCorpCode"].ToString2().Length == 18)
                    {
                        dataRow["BuildCorpCode"] = Public.ShxydmToZzjgdm(dataRow["BuildCorpCode"].ToString2());
                    }

                    xmlData = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);
                    string resultSt = client.SaveTBDataToSt("TBProjectInfo", xmlData, userName, password);

                    DataTable dt_log_TBProjectInfo = dataService.GetTBData_SaveToStLog("TBProjectInfo", dataRow["PKID"].ToString());

                    if (dt_log_TBProjectInfo.Rows.Count > 0)
                    {
                        row = dt_log_TBProjectInfo.Rows[0];
                        row["TableName"] = "TBProjectInfo";
                        row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                    }
                    else
                    {
                        row = dt_log_TBProjectInfo.NewRow();
                        dt_log_TBProjectInfo.Rows.Add(row);
                        row["PKID"] = dataRow["PKID"];
                        row["TableName"] = "TBProjectInfo";
                        row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        row["UpdateDate"] = row["CreateDate"];
                    }
                    all_count++;
                    if (resultSt != "OK")
                    {
                        row["OperateState"] = 1;
                        row["Msg"] = resultSt;
                    }
                    else
                    {
                        success_count++;
                        row["OperateState"] = 0;
                        row["Msg"] = resultSt;
                    }
                    if (dt_log_TBProjectInfo.Rows.Count > 0)
                    {
                        dataService.SaveTBData_SaveToStLog(dt_log_TBProjectInfo);
                    }
                }
                row_DataJkDataDetail["allCount"] = all_count;
                row_DataJkDataDetail["successCount"] = success_count;
                row_DataJkDataDetail["IsOk"] = 1;
                row_DataJkDataDetail["ErrorMsg"] = "";

            }
            catch (Exception ex)
            {
                row_DataJkDataDetail["allCount"] = all_count;
                row_DataJkDataDetail["successCount"] = success_count;
                row_DataJkDataDetail["IsOk"] = 0;
                row_DataJkDataDetail["ErrorMsg"] = ex.Message;
            }

            #endregion

        }

        public void UploadToSt_TBTenderInfo(DataTable dt_DataJkDataDetail, string Id_DataJkLog, long Id_DataJkDataDetail)
        {

            DataRow row_DataJkDataDetail = dt_DataJkDataDetail.NewRow();
            dt_DataJkDataDetail.Rows.Add(row_DataJkDataDetail);

            row_DataJkDataDetail["ID"] = Id_DataJkDataDetail;
            row_DataJkDataDetail["DataJkLogID"] = Id_DataJkLog;
            row_DataJkDataDetail["tableName"] = "TBTenderInfo";
            row_DataJkDataDetail["MethodName"] = "UploadToSt_TBTenderInfo";
            row_DataJkDataDetail["bz"] = "往省一体化平台推送招投标项目数据";
            int all_count = 0, success_count = 0;

            DataTable dt = dataService.GetTBData_TBTenderInfo();

            //WriteLog("记录日志:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            WriteLog("获取了 " + dt.Rows.Count + " 条TBTenderInfo数据！");

            if (dt.Rows.Count == 0)
            {
                return;
            }
            StringBuilder str_pkid = new StringBuilder();
            foreach (DataRow item in dt.Rows)
            {
                str_pkid.AppendFormat("'{0}',", item["PKID"]);
            }

            if (str_pkid.Length > 0)
                str_pkid.Remove(str_pkid.Length - 1, 1);

            #region 保存至省厅（TBTenderInfo）

            try
            {
                string xmlData = "";
                DataTable dt_log_TBTenderInfo = dataService.GetTBData_SaveToStLog("TBTenderInfo", str_pkid.ToString());

                DataRow row;
                foreach (DataRow dataRow in dt.Rows)
                {
                    dataRow["sbdqbm"] = "320200";
                    //因为省一体化平台不接收社会信用代码，这里处理将社会信用代码转化为组织机构代码
                    if (dataRow["TenderCorpCode"].ToString2().Length == 18)
                    {
                        dataRow["TenderCorpCode"] = Public.ShxydmToZzjgdm(dataRow["TenderCorpCode"].ToString2());
                    }

                    if (dataRow["AgencyCorpCode"].ToString2().Length == 18)
                    {
                        dataRow["AgencyCorpCode"] = Public.ShxydmToZzjgdm(dataRow["AgencyCorpCode"].ToString2());
                    }

                    xmlData = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);

                    string resultSt = client.SaveTBDataToSt("TBTenderInfo", xmlData, userName, password);

                    int i = -1;
                    for (int index = 0; index < dt_log_TBTenderInfo.Rows.Count; index++)
                    {
                        if (dt_log_TBTenderInfo.Rows[index]["PKID"].ToString2().Equals(dataRow["PKID"].ToString(), StringComparison.CurrentCultureIgnoreCase))
                        {
                            i = index;
                            break;
                        }
                    }

                    if (i < 0)
                    {
                        row = dt_log_TBTenderInfo.NewRow();
                        dt_log_TBTenderInfo.Rows.Add(row);
                        row["PKID"] = dataRow["PKID"];
                        row["TableName"] = "TBTenderInfo";
                        row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        row = dt_log_TBTenderInfo.Rows[i];
                    }
                    row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    all_count++;
                    if (resultSt != "OK")
                    {
                        row["OperateState"] = 1;
                        row["Msg"] = resultSt;
                    }
                    else
                    {
                        success_count++;
                        row["OperateState"] = 0;
                        row["Msg"] = resultSt;
                    }
                }
                if (dt_log_TBTenderInfo.Rows.Count > 0)
                {
                    dataService.SaveTBData_SaveToStLog(dt_log_TBTenderInfo);
                }
                row_DataJkDataDetail["allCount"] = all_count;
                row_DataJkDataDetail["successCount"] = success_count;
                row_DataJkDataDetail["IsOk"] = 1;
                row_DataJkDataDetail["ErrorMsg"] = "";
            }
            catch (Exception ex)
            {
                row_DataJkDataDetail["allCount"] = all_count;
                row_DataJkDataDetail["successCount"] = success_count;
                row_DataJkDataDetail["IsOk"] = 0;
                row_DataJkDataDetail["ErrorMsg"] = ex.Message;
            }

            #endregion

        }
        /// <summary>
        /// 只送勘察设计合同备案信息，施工监理合同从省厅获取的，不需要往省厅推送
        /// </summary>
        public void UploadToSt_TBContractRecordManage()
        {
            DataTable dt = dataService.GetTBData_TBContractRecordManage();

            WriteLog("获取了 " + dt.Rows.Count + " 条TBContractRecordManage数据！");

            if (dt.Rows.Count == 0)
            {
                return;
            }
            StringBuilder str_pkid = new StringBuilder();
            foreach (DataRow item in dt.Rows)
            {
                str_pkid.AppendFormat("'{0}',", item["PKID"]);
            }

            if (str_pkid.Length > 0)
                str_pkid.Remove(str_pkid.Length - 1, 1);

            //WriteLog(str_pkid.ToString());

            #region 保存至省厅（TBContractRecordManage）

            try
            {
                string xmlData = "";

                DataTable dt_log_TBContractRecordManage = dataService.GetTBData_SaveToStLog("TBContractRecordManage", str_pkid.ToString());

                DataRow row;
                foreach (DataRow dataRow in dt.Rows)
                {
                    dataRow["sbdqbm"] = "320200";
                    xmlData = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);

                    string resultSt = client.SaveTBDataToSt("TBContractRecordManage", xmlData, userName, password);

                    int i = -1;
                    for (int index = 0; index < dt_log_TBContractRecordManage.Rows.Count; index++)
                    {
                        if (dt_log_TBContractRecordManage.Rows[index]["PKID"].ToString2().Equals(dataRow["PKID"].ToString(), StringComparison.CurrentCultureIgnoreCase))
                        {
                            i = index;
                            break;
                        }
                    }

                    if (i < 0)
                    {
                        row = dt_log_TBContractRecordManage.NewRow();
                        dt_log_TBContractRecordManage.Rows.Add(row);
                        row["PKID"] = dataRow["PKID"];
                        row["TableName"] = "TBContractRecordManage";
                        row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        row["UpdateDate"] = row["CreateDate"];
                    }
                    else
                    {
                        row = dt_log_TBContractRecordManage.Rows[i];
                        row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    if (resultSt != "OK")
                    {
                        row["OperateState"] = 1;
                        row["Msg"] = resultSt;
                    }
                    else
                    {
                        row["OperateState"] = 0;
                        row["Msg"] = "上传成功";
                    }
                }
                if (dt_log_TBContractRecordManage.Rows.Count > 0)
                {
                    dataService.SaveTBData_SaveToStLog(dt_log_TBContractRecordManage);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    WriteLog(ex.Message);
                }
                catch
                {

                }
            }

            #endregion

        }

        public void UploadToSt_TBProjectCensorInfo()
        {
            DataTable dt = dataService.GetTBData_TBProjectCensorInfo();

            WriteLog("获取了 " + dt.Rows.Count + " 条TBProjectCensorInfo数据！");

            if (dt.Rows.Count == 0)
            {
                return;
            }
            StringBuilder str_pkid = new StringBuilder();
            foreach (DataRow item in dt.Rows)
            {
                str_pkid.AppendFormat("'{0}',", item["PKID"]);
            }

            if (str_pkid.Length > 0)
                str_pkid.Remove(str_pkid.Length - 1, 1);


            #region 保存至省厅（TBProjectCensorInfo）

            try
            {
                string xmlData = "";
                DataTable dt_log_TBProjectCensorInfo = dataService.GetTBData_SaveToStLog("TBProjectCensorInfo", str_pkid.ToString());

                DataRow row;
                foreach (DataRow dataRow in dt.Rows)
                {
                    dataRow["sbdqbm"] = "320200";
                    xmlData = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);

                    string resultSt = client.SaveTBDataToSt("TBProjectCensorInfo", xmlData, userName, password);

                    int i = -1;
                    for (int index = 0; index < dt_log_TBProjectCensorInfo.Rows.Count; index++)
                    {
                        if (dt_log_TBProjectCensorInfo.Rows[index]["PKID"].ToString2().Equals(dataRow["PKID"].ToString(), StringComparison.CurrentCultureIgnoreCase))
                        {
                            i = index;
                            break;
                        }
                    }

                    if (i < 0)
                    {
                        row = dt_log_TBProjectCensorInfo.NewRow();
                        dt_log_TBProjectCensorInfo.Rows.Add(row);
                        row["PKID"] = dataRow["PKID"];
                        row["TableName"] = "TBProjectCensorInfo";
                        row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        row["UpdateDate"] = row["CreateDate"];
                    }
                    else
                    {
                        row = dt_log_TBProjectCensorInfo.Rows[i];
                        row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    if (resultSt != "OK")
                    {
                        row["OperateState"] = 1;
                        row["Msg"] = resultSt;
                    }
                    else
                    {
                        row["OperateState"] = 0;
                        row["Msg"] = "上传成功";
                    }
                }
                if (dt_log_TBProjectCensorInfo.Rows.Count > 0)
                {
                    dataService.SaveTBData_SaveToStLog(dt_log_TBProjectCensorInfo);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    WriteLog(ex.Message);
                }
                catch
                {

                }
            }

            #endregion

        }

        public void UploadToSt_TBProjectDesignEconUserInfo()
        {
            DataTable dt = dataService.GetTBData_TBProjectDesignEconUserInfo();

            WriteLog("获取了 " + dt.Rows.Count + " 条TBProjectDesignEconUserInfo数据！");

            if (dt.Rows.Count == 0)
            {
                return;
            }
            StringBuilder str_pkid = new StringBuilder();
            foreach (DataRow item in dt.Rows)
            {
                str_pkid.AppendFormat("'{0}',", item["PKID"]);
            }

            if (str_pkid.Length > 0)
                str_pkid.Remove(str_pkid.Length - 1, 1);


            #region 保存至省厅（TBProjectDesignEconUserInfo）

            try
            {
                string xmlData = "";
                DataTable dt_log_TBProjectDesignEconUserInfo = dataService.GetTBData_SaveToStLog("TBProjectDesignEconUserInfo", str_pkid.ToString());

                DataRow row;
                foreach (DataRow dataRow in dt.Rows)
                {
                    dataRow["sbdqbm"] = "320200";
                    xmlData = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);

                    string resultSt = client.SaveTBDataToSt("TBProjectDesignEconUserInfo", xmlData, userName, password);

                    int i = -1;
                    for (int index = 0; index < dt_log_TBProjectDesignEconUserInfo.Rows.Count; index++)
                    {
                        if (dt_log_TBProjectDesignEconUserInfo.Rows[index]["PKID"].ToString2().Equals(dataRow["PKID"].ToString(), StringComparison.CurrentCultureIgnoreCase))
                        {
                            i = index;
                            break;
                        }
                    }

                    if (i < 0)
                    {
                        row = dt_log_TBProjectDesignEconUserInfo.NewRow();
                        dt_log_TBProjectDesignEconUserInfo.Rows.Add(row);
                        row["PKID"] = dataRow["PKID"];
                        row["TableName"] = "TBProjectDesignEconUserInfo";
                        row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        row["UpdateDate"] = row["CreateDate"];
                    }
                    else
                    {
                        row = dt_log_TBProjectDesignEconUserInfo.Rows[i];
                        row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    if (resultSt != "OK")
                    {
                        row["OperateState"] = 1;
                        row["Msg"] = resultSt;
                    }
                    else
                    {
                        row["OperateState"] = 0;
                        row["Msg"] = "上传成功";
                    }
                }
                if (dt_log_TBProjectDesignEconUserInfo.Rows.Count > 0)
                {
                    dataService.SaveTBData_SaveToStLog(dt_log_TBProjectDesignEconUserInfo);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    WriteLog(ex.Message);
                }
                catch
                {

                }
            }

            #endregion

        }

        public void UploadToSt_TBBuilderLicenceManage()
        {
            DataTable dt = dataService.GetTBData_TBBuilderLicenceManage();

            WriteLog("获取了 " + dt.Rows.Count + " 条TBBuilderLicenceManage数据！");

            if (dt.Rows.Count == 0)
            {
                return;
            }
            StringBuilder str_pkid = new StringBuilder();
            foreach (DataRow item in dt.Rows)
            {
                str_pkid.AppendFormat("'{0}',", item["PKID"]);
            }

            if (str_pkid.Length > 0)
                str_pkid.Remove(str_pkid.Length - 1, 1);


            #region 保存至省厅（TBBuilderLicenceManage）

            try
            {
                string xmlData = "";
                DataTable dt_log_TBBuilderLicenceManage = dataService.GetTBData_SaveToStLog("TBBuilderLicenceManage", str_pkid.ToString());

                DataRow row;
                foreach (DataRow dataRow in dt.Rows)
                {
                    dataRow["sbdqbm"] = "320200";
                    xmlData = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);

                    string resultSt = client.SaveTBDataToSt("TBBuilderLicenceManage", xmlData, userName, password);

                    int i = -1;
                    for (int index = 0; index < dt_log_TBBuilderLicenceManage.Rows.Count; index++)
                    {
                        if (dt_log_TBBuilderLicenceManage.Rows[index]["PKID"].ToString2().Equals(dataRow["PKID"].ToString(), StringComparison.CurrentCultureIgnoreCase))
                        {
                            i = index;
                            break;
                        }
                    }

                    if (i < 0)
                    {
                        row = dt_log_TBBuilderLicenceManage.NewRow();
                        dt_log_TBBuilderLicenceManage.Rows.Add(row);
                        row["PKID"] = dataRow["PKID"];
                        row["TableName"] = "TBBuilderLicenceManage";
                        row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        row["UpdateDate"] = row["CreateDate"];
                    }
                    else
                    {
                        row = dt_log_TBBuilderLicenceManage.Rows[i];
                        row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    if (resultSt != "OK")
                    {
                        row["OperateState"] = 1;
                        row["Msg"] = resultSt;
                    }
                    else
                    {
                        row["OperateState"] = 0;
                        row["Msg"] = "上传成功";
                    }
                }
                if (dt_log_TBBuilderLicenceManage.Rows.Count > 0)
                {
                    dataService.SaveTBData_SaveToStLog(dt_log_TBBuilderLicenceManage);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    WriteLog(ex.Message);
                }
                catch
                {

                }
            }

            #endregion

        }

        public void UploadToSt_TBProjectFinishManage()
        {
            DataTable dt = dataService.GetTBData_TBProjectFinishManage();

            WriteLog("获取了 " + dt.Rows.Count + " 条TBProjectFinishManage数据！");

            if (dt.Rows.Count == 0)
            {
                return;
            }
            StringBuilder str_pkid = new StringBuilder();
            foreach (DataRow item in dt.Rows)
            {
                str_pkid.AppendFormat("'{0}',", item["PKID"]);
            }

            if (str_pkid.Length > 0)
                str_pkid.Remove(str_pkid.Length - 1, 1);


            #region 保存至省厅（TBProjectFinishManage）

            try
            {
                string xmlData = "";
                DataTable dt_log_TBProjectFinishManage = dataService.GetTBData_SaveToStLog("TBProjectFinishManage", str_pkid.ToString());
                DataRow row;
                foreach (DataRow dataRow in dt.Rows)
                {
                    dataRow["sbdqbm"] = "320200";
                    xmlData = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);

                    string resultSt = client.SaveTBDataToSt("TBProjectFinishManage", xmlData, userName, password);

                    int i = -1;
                    for (int index = 0; index < dt_log_TBProjectFinishManage.Rows.Count; index++)
                    {
                        if (dt_log_TBProjectFinishManage.Rows[index]["PKID"].ToString2().Equals(dataRow["PKID"].ToString(), StringComparison.CurrentCultureIgnoreCase))
                        {
                            i = index;
                            break;
                        }
                    }

                    if (i < 0)
                    {
                        row = dt_log_TBProjectFinishManage.NewRow();
                        dt_log_TBProjectFinishManage.Rows.Add(row);
                        row["PKID"] = dataRow["PKID"];
                        row["TableName"] = "TBProjectFinishManage";
                        row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        row["UpdateDate"] = row["CreateDate"];
                    }
                    else
                    {
                        row = dt_log_TBProjectFinishManage.Rows[i];
                        row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    if (resultSt != "OK")
                    {
                        row["OperateState"] = 1;
                        row["Msg"] = resultSt;
                    }
                    else
                    {
                        row["OperateState"] = 0;
                        row["Msg"] = "上传成功";
                    }
                }
                if (dt_log_TBProjectFinishManage.Rows.Count > 0)
                {
                    dataService.SaveTBData_SaveToStLog(dt_log_TBProjectFinishManage);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    WriteLog(ex.Message);
                }
                catch
                {

                }
            }

            #endregion

        }

        public void UploadToSt_xm_gcdjb_dtxm()
        {
            DataTable dt = dataService.GetTBData_xm_gcdjb_dtxm();

            WriteLog("获取了 " + dt.Rows.Count + " 条xm_gcdjb_dtxm数据！");

            if (dt.Rows.Count == 0)
            {
                return;
            }
            StringBuilder str_pkid = new StringBuilder();
            foreach (DataRow item in dt.Rows)
            {
                str_pkid.AppendFormat("'{0}',", item["PKID"]);
            }

            if (str_pkid.Length > 0)
                str_pkid.Remove(str_pkid.Length - 1, 1);

            #region 保存至省厅（xm_gcdjb_dtxm）

            try
            {
                string xmlData = "";
                DataTable dt_log_xm_gcdjb_dtxm = dataService.GetTBData_SaveToStLog("xm_gcdjb_dtxm", str_pkid.ToString());
                DataRow row;
                foreach (DataRow dataRow in dt.Rows)
                {
                    dataRow["sbdqbm"] = "320200";
                    xmlData = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);

                    string resultSt = client.SaveTBDataToSt("xm_gcdjb_dtxm", xmlData, userName, password);

                    int i = -1;
                    for (int index = 0; index < dt_log_xm_gcdjb_dtxm.Rows.Count; index++)
                    {
                        if (dt_log_xm_gcdjb_dtxm.Rows[index]["PKID"].ToString2().Equals(dataRow["PKID"].ToString(), StringComparison.CurrentCultureIgnoreCase))
                        {
                            i = index;
                            break;
                        }
                    }

                    if (i < 0)
                    {
                        row = dt_log_xm_gcdjb_dtxm.NewRow();
                        dt_log_xm_gcdjb_dtxm.Rows.Add(row);
                        row["PKID"] = dataRow["PKID"];
                        row["TableName"] = "xm_gcdjb_dtxm";
                        row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        row["UpdateDate"] = row["CreateDate"];
                    }
                    else
                    {
                        row = dt_log_xm_gcdjb_dtxm.Rows[i];
                        row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    if (resultSt != "OK")
                    {
                        row["OperateState"] = 1;
                        row["Msg"] = resultSt;
                    }
                    else
                    {
                        row["OperateState"] = 0;
                        row["Msg"] = "上传成功";
                    }
                }
                if (dt_log_xm_gcdjb_dtxm.Rows.Count > 0)
                {
                    dataService.SaveTBData_SaveToStLog(dt_log_xm_gcdjb_dtxm);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    WriteLog(ex.Message);
                }
                catch
                {

                }
            }

            #endregion

        }

        public void UploadToSt_aj_gcjbxx(DataTable dt_DataJkDataDetail, string Id_DataJkLog, long Id_DataJkDataDetail)
        {
            DataRow row_DataJkDataDetail = dt_DataJkDataDetail.NewRow();
            dt_DataJkDataDetail.Rows.Add(row_DataJkDataDetail);

            row_DataJkDataDetail["ID"] = Id_DataJkDataDetail;
            row_DataJkDataDetail["DataJkLogID"] = Id_DataJkLog;
            row_DataJkDataDetail["tableName"] = "aj_gcjbxx";
            row_DataJkDataDetail["MethodName"] = "UploadToSt_aj_gcjbxx";
            row_DataJkDataDetail["bz"] = "往省一体化平台推送安监数据";
            int all_count = 0, success_count = 0;

            DataTable dt = dataService.GetTBData_aj_gcjbxx();

            WriteLog("获取了 " + dt.Rows.Count + " 条aj_gcjbxx数据！");

            if (dt.Rows.Count == 0)
            {
                return;
            }
            StringBuilder str_pkid = new StringBuilder();
            foreach (DataRow item in dt.Rows)
            {
                str_pkid.AppendFormat("'{0}',", item["pKID"].ToString());
            }

            if (str_pkid.Length > 0)
                str_pkid.Remove(str_pkid.Length - 1, 1);

            #region 保存至省厅（aj_gcjbxx）

            try
            {
                string xmlData = "";
                DataTable dt_log_aj_gcjbxx = dataService.GetTBData_SaveToStLog("aj_gcjbxx", str_pkid.ToString());

                DataRow row;
                foreach (DataRow dataRow in dt.Rows)
                {
                    row = null;
                    try
                    {
                        dataRow["sbdqbm"] = "320200";
                        xmlData = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);

                        //string resultSt = client.SaveTBDataToSt("aj_gcjbxx", xmlData, userName, password);
                        string resultSt = client.SaveStData("aj_gcjbxx", xmlData, userName_qyry, password_qyry, dataRow["UpdateFlag"].ToString2());

                        if (!string.IsNullOrEmpty(dataRow["sdcode"].ToString2()) && dataRow["sdcode"].ToString2().Length == 18)
                        {
                            dataRow["sdcode"] = Public.ShxydmToZzjgdm(dataRow["sdcode"].ToString2());
                        }

                        if (!string.IsNullOrEmpty(dataRow["zbdwDwdm"].ToString2()) && dataRow["zbdwDwdm"].ToString2().Length == 18)
                        {
                            dataRow["zbdwDwdm"] = Public.ShxydmToZzjgdm(dataRow["zbdwDwdm"].ToString2());
                        }

                        if (!string.IsNullOrEmpty(dataRow["jldwDwdm"].ToString2()) && dataRow["jldwDwdm"].ToString2().Length == 18)
                        {
                            dataRow["jldwDwdm"] = Public.ShxydmToZzjgdm(dataRow["jldwDwdm"].ToString2());
                        }
                        int i = -1;
                        for (int index = 0; index < dt_log_aj_gcjbxx.Rows.Count; index++)
                        {
                            if (dt_log_aj_gcjbxx.Rows[index]["pKID"].ToString2().Equals(dataRow["pKID"].ToString2(), StringComparison.CurrentCultureIgnoreCase))
                            {
                                i = index;
                                break;
                            }
                        }

                        if (i < 0)
                        {
                            row = dt_log_aj_gcjbxx.NewRow();
                            dt_log_aj_gcjbxx.Rows.Add(row);
                            row["PKID"] = dataRow["PKID"];
                            row["TableName"] = "aj_gcjbxx";
                            row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            row["UpdateDate"] = row["CreateDate"];
                        }
                        else
                        {
                            row = dt_log_aj_gcjbxx.Rows[i];
                            row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        all_count++;
                        if (resultSt != "OK" && resultSt != "删除成功！")
                        {
                            row["OperateState"] = 1;
                            row["Msg"] = resultSt;
                        }
                        else
                        {
                            success_count++;
                            row["OperateState"] = 0;
                            row["Msg"] = resultSt;
                        }
                    }
                    catch (Exception ex)
                    {
                        row = dt_log_aj_gcjbxx.NewRow();
                        dt_log_aj_gcjbxx.Rows.Add(row);
                        row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        row["PKID"] = dataRow["PKID"];
                        row["TableName"] = "aj_gcjbxx";
                        row["UpdateDate"] = row["CreateDate"];
                        row["OperateState"] = 1;
                        row["Msg"] = ex.ToString();
                    }

                }
                if (dt_log_aj_gcjbxx.Rows.Count > 0)
                {
                    dataService.SaveTBData_SaveToStLog(dt_log_aj_gcjbxx);
                }
                row_DataJkDataDetail["allCount"] = all_count;
                row_DataJkDataDetail["successCount"] = success_count;
                row_DataJkDataDetail["IsOk"] = 1;
                row_DataJkDataDetail["ErrorMsg"] = "";
            }
            catch (Exception ex)
            {
                row_DataJkDataDetail["allCount"] = all_count;
                row_DataJkDataDetail["successCount"] = success_count;
                row_DataJkDataDetail["IsOk"] = 0;
                row_DataJkDataDetail["ErrorMsg"] = ex.ToString();
            }

            #endregion
        }

        public void UploadToSt_zj_gcjbxx(DataTable dt_DataJkDataDetail, string Id_DataJkLog, long Id_DataJkDataDetail)
        {
            DataRow row_DataJkDataDetail = dt_DataJkDataDetail.NewRow();
            dt_DataJkDataDetail.Rows.Add(row_DataJkDataDetail);

            row_DataJkDataDetail["ID"] = Id_DataJkDataDetail;
            row_DataJkDataDetail["DataJkLogID"] = Id_DataJkLog;
            row_DataJkDataDetail["tableName"] = "zj_gcjbxx";
            row_DataJkDataDetail["MethodName"] = "UploadToSt_zj_gcjbxx";
            row_DataJkDataDetail["bz"] = "往省一体化平台推送质监数据";
            int all_count = 0, success_count = 0;

            DataTable dt = dataService.GetTBData_zj_gcjbxx();

            WriteLog("获取了 " + dt.Rows.Count + " 条zj_gcjbxx数据！");

            if (dt.Rows.Count == 0)
            {
                return;
            }
            StringBuilder str_pkid = new StringBuilder();
            foreach (DataRow item in dt.Rows)
            {
                str_pkid.AppendFormat("'{0}',", item["pKID"]);
            }

            if (str_pkid.Length > 0)
                str_pkid.Remove(str_pkid.Length - 1, 1);

            #region 保存至省厅（zj_gcjbxx）

            try
            {
                string xmlData = "";
                DataTable dt_log_zj_gcjbxx = dataService.GetTBData_SaveToStLog("zj_gcjbxx", str_pkid.ToString());
                DataRow row;
                foreach (DataRow dataRow in dt.Rows)
                {
                    dataRow["sbdqbm"] = "320200";

                    if (!string.IsNullOrEmpty(dataRow["zjzbm"].ToString2()) && dataRow["zjzbm"].ToString2().Length == 18)
                    {
                        dataRow["zjzbm"] = Public.ShxydmToZzjgdm(dataRow["zjzbm"].ToString2());
                    }


                    xmlData = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);

                    string resultSt = client.SaveStData("zj_gcjbxx", xmlData, userName_qyry, password_qyry, dataRow["UpdateFlag"].ToString2());

                    int i = -1;
                    for (int index = 0; index < dt_log_zj_gcjbxx.Rows.Count; index++)
                    {
                        if (dt_log_zj_gcjbxx.Rows[index]["PKID"].ToString2().Equals(dataRow["pKID"].ToString2(), StringComparison.CurrentCultureIgnoreCase))
                        {
                            i = index;
                            break;
                        }
                    }

                    if (i < 0)
                    {
                        row = dt_log_zj_gcjbxx.NewRow();
                        dt_log_zj_gcjbxx.Rows.Add(row);
                        row["PKID"] = dataRow["pKID"];
                        row["TableName"] = "zj_gcjbxx";
                        row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        row["UpdateDate"] = row["CreateDate"];
                    }
                    else
                    {
                        row = dt_log_zj_gcjbxx.Rows[i];
                        row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    all_count++;
                    if (resultSt != "OK" && resultSt != "删除成功！")
                    {
                        row["OperateState"] = 1;
                        row["Msg"] = resultSt;
                    }
                    else
                    {
                        success_count++;
                        row["OperateState"] = 0;
                        row["Msg"] = resultSt;
                    }

                }
                if (dt_log_zj_gcjbxx.Rows.Count > 0)
                {
                    dataService.SaveTBData_SaveToStLog(dt_log_zj_gcjbxx);
                }

                row_DataJkDataDetail["allCount"] = all_count;
                row_DataJkDataDetail["successCount"] = success_count;
                row_DataJkDataDetail["IsOk"] = 1;
                row_DataJkDataDetail["ErrorMsg"] = "";

            }
            catch (Exception ex)
            {
                row_DataJkDataDetail["allCount"] = all_count;
                row_DataJkDataDetail["successCount"] = success_count;
                row_DataJkDataDetail["IsOk"] = 0;
                row_DataJkDataDetail["ErrorMsg"] = ex.Message;
            }

            #endregion
        }

        public void UploadToSt_zj_gcjbxx_zrdw(DataTable dt_DataJkDataDetail, string Id_DataJkLog, long Id_DataJkDataDetail)
        {
            DataRow row_DataJkDataDetail = dt_DataJkDataDetail.NewRow();
            dt_DataJkDataDetail.Rows.Add(row_DataJkDataDetail);

            row_DataJkDataDetail["ID"] = Id_DataJkDataDetail;
            row_DataJkDataDetail["DataJkLogID"] = Id_DataJkLog;
            row_DataJkDataDetail["tableName"] = "zj_gcjbxx_zrdw";
            row_DataJkDataDetail["MethodName"] = "UploadToSt_zj_gcjbxx_zrdw";
            row_DataJkDataDetail["bz"] = "往省一体化平台推送质监人员数据";
            int all_count = 0, success_count = 0;


            DataTable dt = dataService.GetTBData_zj_gcjbxx_zrdw();

            WriteLog("获取了 " + dt.Rows.Count + " 条zj_gcjbxx_zrdw数据！");

            if (dt.Rows.Count == 0)
            {
                return;
            }
            StringBuilder str_pkid = new StringBuilder();
            foreach (DataRow item in dt.Rows)
            {
                str_pkid.AppendFormat("'{0}',", item["pKID"] + "-" + item["xh"].ToString());
            }

            if (str_pkid.Length > 0)
                str_pkid.Remove(str_pkid.Length - 1, 1);


            #region 保存至省厅（zj_gcjbxx_zrdw）

            try
            {
                string xmlData = "";
                DataTable dt_log_zj_gcjbxx_zrdw = dataService.GetTBData_SaveToStLog("zj_gcjbxx_zrdw", str_pkid.ToString());
                DataRow row;
                foreach (DataRow dataRow in dt.Rows)
                {
                    dataRow["sbdqbm"] = "320200";
                    if (!string.IsNullOrEmpty(dataRow["dwdm"].ToString2()) && dataRow["dwdm"].ToString2().Length == 18)
                    {
                        dataRow["dwdm"] = Public.ShxydmToZzjgdm(dataRow["dwdm"].ToString2());
                    }

                    xmlData = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);
                    string resultSt = client.SaveStData("zj_gcjbxx_zrdw", xmlData, userName_qyry, password_qyry, "U");

                    int i = -1;
                    for (int index = 0; index < dt_log_zj_gcjbxx_zrdw.Rows.Count; index++)
                    {
                        if (dt_log_zj_gcjbxx_zrdw.Rows[index]["PKID"].ToString2().Equals(dataRow["PKID"].ToString2() + "-" + dataRow["xh"].ToString2(), StringComparison.CurrentCultureIgnoreCase))
                        {
                            i = index;
                            break;
                        }
                    }

                    if (i < 0)
                    {
                        row = dt_log_zj_gcjbxx_zrdw.NewRow();
                        dt_log_zj_gcjbxx_zrdw.Rows.Add(row);
                        row["PKID"] = dataRow["pKID"].ToString2() + "-" + dataRow["xh"].ToString2();
                        row["TableName"] = "zj_gcjbxx_zrdw";
                        row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        row["UpdateDate"] = row["CreateDate"];
                    }
                    else
                    {
                        row = dt_log_zj_gcjbxx_zrdw.Rows[i];
                        row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    all_count++;
                    if (resultSt != "OK")
                    {
                        row["OperateState"] = 1;
                        row["Msg"] = resultSt;
                    }
                    else
                    {
                        success_count++;
                        row["OperateState"] = 0;
                        row["Msg"] = resultSt;
                    }
                }
                if (dt_log_zj_gcjbxx_zrdw.Rows.Count > 0)
                {
                    dataService.SaveTBData_SaveToStLog(dt_log_zj_gcjbxx_zrdw);
                }
                row_DataJkDataDetail["allCount"] = all_count;
                row_DataJkDataDetail["successCount"] = success_count;
                row_DataJkDataDetail["IsOk"] = 1;
                row_DataJkDataDetail["ErrorMsg"] = "";

            }
            catch (Exception ex)
            {
                row_DataJkDataDetail["allCount"] = all_count;
                row_DataJkDataDetail["successCount"] = success_count;
                row_DataJkDataDetail["IsOk"] = 0;
                row_DataJkDataDetail["ErrorMsg"] = ex.Message;

            }

            #endregion

        }

        public void UploadToSt_TBProjectBuilderUserInfo()
        {
            DataTable dt = dataService.GetTBData_TBProjectBuilderUserInfo();

            WriteLog("获取了 " + dt.Rows.Count + " 条TBProjectBuilderUserInfo数据！");

            if (dt.Rows.Count == 0)
            {
                return;
            }
            StringBuilder str_pkid = new StringBuilder();
            foreach (DataRow item in dt.Rows)
            {
                str_pkid.AppendFormat("'{0}',", item["PKID"]);
            }

            if (str_pkid.Length > 0)
                str_pkid.Remove(str_pkid.Length - 1, 1);

            #region 保存至省厅（TBProjectBuilderUserInfo）
            try
            {
                string xmlData = "";
                DataTable dt_log_TBProjectBuilderUserInfo = dataService.GetTBData_SaveToStLog("TBProjectBuilderUserInfo", str_pkid.ToString());

                DataRow row;
                foreach (DataRow dataRow in dt.Rows)
                {
                    dataRow["sbdqbm"] = "320200";
                    xmlData = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);

                    string resultSt = client.SaveTBDataToSt("TBProjectBuilderUserInfo", xmlData, userName, password);

                    int i = -1;
                    for (int index = 0; index < dt_log_TBProjectBuilderUserInfo.Rows.Count; index++)
                    {
                        if (dt_log_TBProjectBuilderUserInfo.Rows[index]["PKID"].ToString2().Equals(dataRow["PKID"].ToString2(), StringComparison.CurrentCultureIgnoreCase))
                        {
                            i = index;
                            break;
                        }
                    }

                    if (i < 0)
                    {
                        row = dt_log_TBProjectBuilderUserInfo.NewRow();
                        dt_log_TBProjectBuilderUserInfo.Rows.Add(row);
                        row["PKID"] = dataRow["PKID"];
                        row["TableName"] = "TBProjectBuilderUserInfo";
                        row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        row["UpdateDate"] = row["CreateDate"];
                    }
                    else
                    {
                        row = dt_log_TBProjectBuilderUserInfo.Rows[i];
                        row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    if (resultSt != "OK")
                    {
                        row["OperateState"] = 1;
                        row["Msg"] = resultSt;
                    }
                    else
                    {
                        row["OperateState"] = 0;
                        row["Msg"] = resultSt;
                    }
                }
                if (dt_log_TBProjectBuilderUserInfo.Rows.Count > 0)
                {
                    dataService.SaveTBData_SaveToStLog(dt_log_TBProjectBuilderUserInfo);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    WriteLog(ex.Message);
                }
                catch
                {

                }
            }

            #endregion
        }

        #endregion

        #region 公用方法

        public void WriteLog(string msg)
        {
            Public.WriteLog(fileName, msg);
        }

        #endregion


        protected override void OnStop()
        {
            if (myTimer != null)
                myTimer.Stop();
        }

    }
}
