using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Web;
using System.Timers;
using System.Net;
using System.IO;
using System.Threading;
using Bigdesk8.Data;
using Bigdesk8;
using System.Configuration;
using System.Windows.Forms;
using WxsjzxTimerService;
using WxsjzxTimerService.Common;
using WxsjzxTimerService.model;
using System.Transactions;
using WxjzgcjczyTimerService.model;
using WxjzgcjczyTimerService.YiZhanShiShenBao;

namespace WxjzgcjczyTimerService
{
    
    public partial class Service1 : ServiceBase
    {
        //private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        int timeSpan = 1;
        DataService dataService = new DataService();
        XmlHelper xmlHelper = new XmlHelper();
        ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient client = new ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient();
        string userName = "320200", password = "we&gjh45H";
        string userName_qyry = "320200", password_qyry = "W123YheAge";
        public bool isRunning;
        public bool sbIsRunning;
        public object obj = "111";
        System.Timers.Timer myTimer;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //定义定时器 
            string timeSpanStr = ConfigurationManager.AppSettings["timeSpan_ToWxsjzx"];

            if (!Int32.TryParse(timeSpanStr, out timeSpan))
            {
                timeSpan = 1;
            }

            isRunning = false;
            sbIsRunning = false;

            System.Timers.Timer myTimer = new System.Timers.Timer(1000 * 60 * timeSpan);
            myTimer.Elapsed += new ElapsedEventHandler(myTimer_Elapsed);
            myTimer.Enabled = true;
            myTimer.AutoReset = true;
            myTimer.Start();
        }

        protected override void OnStop()
        {
            if (myTimer != null)
            {
                myTimer.Stop();
            }
        }

        void myTimer_Elapsed(object source, ElapsedEventArgs e)
        {
            try
            {
                List<string> setTimes = ConfigurationManager.AppSettings["setTime_ToWxsjzx"].ToString().Split(',').ToList();
                int f = 0;
                
                for (int i = 0; i < setTimes.Count; i++)
                {
                    int hour = setTimes[i].Substring(0, 2).ToInt32();
                    int minute = setTimes[i].Substring(2, 2).ToInt32();

                    if (DateTime.Now.Hour == hour && DateTime.Now.Minute < minute + timeSpan && DateTime.Now.Minute >= minute)
                    {
                        if (isRunning)
                        {
                            Public.WriteLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "上一次服务正在运行中。。。");
                            Public.WriteLog("结束记录日志:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
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
                    Public.WriteLog("开始记录日志:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    #region 拉取数据

                    // 从省一体化平台获取数据（江阴立项项目，合同备案）到无锡数据中心
                    YourTask_PullDataFromSythpt();
                    Public.WriteLog("\r\n");

                    //从省厅获取信息无锡地区施工图审查信息
                    YourTask_PullDataFromStTBProjectCensorInfo();
                    Public.WriteLog("\r\n");

                    //从省施工许可系统拉取施工许可信息到无锡数据中心
                    YourTask_PullDataFromSSgxk();
                    Public.WriteLog("\r\n");

                    //获取一号通系统里的建设单位信息
                    YourTask_PullDataFromYht_Jsdwxx();
                    Public.WriteLog("\r\n");

                    //获取省勘察设计系统勘察设计单位信息
                    YourTask_PullDataFromSKcsj_qyxx();
                    Public.WriteLog("\r\n");

                    //获取省勘察设计系统勘察设计合同备案
                    YourTask_PullDataFromSKcsj_htba();
                    Public.WriteLog("\r\n");

                    //往数据监控日志表添加一条记录---江苏建设公共基础数据平台到无锡数据中心
                    string apiMessage = string.Empty;// 2016.10.21
                    //string methodMessage = string.Empty;
                    DataTable dtapizb = dataService.Get_API_zb_apiFlow("26");
                    if (dtapizb.Rows[0][0].ToString() == "1")
                    {
                        DataTable dt_DataJkLog = dataService.GetSchema_DataJkLog();
                        DataRow row_DataJkLog = dt_DataJkLog.NewRow();
                        dt_DataJkLog.Rows.Add(row_DataJkLog);
                        row_DataJkLog["ID"] = dataService.Get_DataJkLogNewID();
                        row_DataJkLog["DataFlow"] = DataFlow.江苏建设公共基础数据平台到无锡数据中心.ToInt32();
                        row_DataJkLog["DataFlowName"] = DataFlow.江苏建设公共基础数据平台到无锡数据中心.ToString();
                        row_DataJkLog["ServiceUrl"] = "http://58.213.147.243:8080/jscedc/services/NewDataService?wsdl";
                        row_DataJkLog["csTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        dataService.Submit_DataJkLog(dt_DataJkLog);

                        try
                        {
                            

                            //获取省内建设单位信息
                            try
                            {
                                YourTask_PullDataFromSxxzx_Jsdw(row_DataJkLog["ID"].ToString2());
                                Public.WriteLog("\r\n");
                            } 
                            catch (Exception ex)
                            {
                                Public.WriteLog( DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-获取省内建设单位信息异常:" + ex.Message);
                            }
                            
                            //获取市内企业信息
                            try
                            {
                                YourTask_PullDataFromSxxzx_qyxx(row_DataJkLog["ID"].ToString2());
                                Public.WriteLog("\r\n"); 
                            } 
                            catch (Exception ex)
                            {
                                Public.WriteLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-获取市内企业信息异常:" + ex.Message);
                            }
                           

                            //获取省内市外企业信息

                            DataTable dtCityCodes= dataService.Get_tbXzqdmDic_AllCityExceptWuxi();
                            foreach (DataRow cityCode in dtCityCodes.Rows)
                            {
                                try
                                {
                                    YourTask_PullDataFromSxxzx_Jiangsu_qyxx(row_DataJkLog["ID"].ToString2(), cityCode["Code"].ToString2());
                                    Public.WriteLog("\r\n");
                                }
                                catch (Exception ex)
                                {
                                    Public.WriteLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-获取市内企业信息异常:" + cityCode["Code"].ToString2() + "\n" + ex.Message);
                                }
                                
                            }

                            //获取省外企业信息
                            try
                            {
                                YourTask_PullDataFromSxxzx_Swqyxx(row_DataJkLog["ID"].ToString2());
                                Public.WriteLog("\r\n");
                            }
                            catch (Exception ex)
                            {
                                Public.WriteLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-获取省外企业信息异常:" + ex.Message);
                            }
                            
                            //获取省外企业人员信息
                            try
                            {
                                YourTask_PullDataFromSxxzx_Swryxx(row_DataJkLog["ID"].ToString2());
                                Public.WriteLog("\r\n");
                            }
                            catch (Exception ex)
                            {
                                Public.WriteLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-获取省外企业人员信息异常:" + ex.Message);
                            }
                           
                             
                            //获取省内注册执业人员信息
                            try
                            {
                                 YourTask_PullDataFromSxxzx_Ryxx_Zczyry(row_DataJkLog["ID"].ToString2());
                                Public.WriteLog("\r\n");
                            }
                            catch (Exception ex)
                            {
                                Public.WriteLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-获取省内注册执业人员信息异常:" + ex.Message);
                            }

                            try
                            {
                                YourTask_PullDataFromSxxzx_Ryxx_Aqscgl(row_DataJkLog["ID"].ToString2());
                                Public.WriteLog("\r\n");
                            }
                            catch (Exception ex)
                            {
                                Public.WriteLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-YourTask_PullDataFromSxxzx_Ryxx_Aqscgl异常:" + ex.Message);
                            }

                            try
                            {
                                YourTask_PullDataFromSxxzx_Ryxx_Zygwgl(row_DataJkLog["ID"].ToString2());
                                Public.WriteLog("\r\n");
                            }
                            catch (Exception ex)
                            {
                                Public.WriteLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-YourTask_PullDataFromSxxzx_Ryxx_Zygwgl异常:" + ex.Message);
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            apiMessage += ex.Message;
                        }

                        finally
                        {
                            DataTable dtapicb = dataService.GetSchema_API_cb();
                            DataRow row_apicb = dtapicb.NewRow();
                            dtapicb.Rows.Add(row_apicb);
                            row_apicb["apiCbID"] = dataService.Get_apiCbNewID();
                            row_apicb["apiFlow"] = "26";
                            row_apicb["apiMethod"] = "getOutCorpInfo;getOutPersonCert;getCorpInfo;getPersonRegCert_Inc;getPersonJobCert;getPersonJobCert";
                            row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                            row_apicb["apiDyMessage"] = apiMessage;
                            row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            dataService.Submit_API_cb(dtapicb);

                            dataService.UpdateZbJkzt("26", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);
                        }
                    }
                    #endregion



                    lock (obj)
                    {
                        isRunning = false;
                    }
                    Public.WriteLog("结束记录日志:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }


                #region 从一站式申报上获取申报数据
                List<string> setTimesSB = ConfigurationManager.AppSettings["setTime_ToWxsjzx_SB"].ToString().Split(',').ToList();
                int fsb = 0;
                for (int i = 0; i < setTimesSB.Count; i++)
                {
                    int hour = setTimesSB[i].Substring(0, 2).ToInt32();
                    int minute = setTimesSB[i].Substring(2, 2).ToInt32();

                    if (DateTime.Now.Hour == hour && DateTime.Now.Minute < minute + timeSpan && DateTime.Now.Minute >= minute)
                    {
                        if (sbIsRunning)
                        {
                            Public.WriteLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "上一次获取申报数据服务正在运行中。。。");
                            Public.WriteLog("获取申报数据服务-结束记录日志:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            return;
                        }
                        
                        lock (obj)
                        {
                            sbIsRunning = true;
                        }

                        fsb = 1;
                        break;
                    }
                   
                }

                if (fsb == 1)
                {
                    DateTime now = DateTime.Now;
                    Public.WriteLog("获取申报数据服务-开始记录日志:" + now.ToString("yyyy-MM-dd HH:mm:ss"));
                    DataFetchOfAJSB dataFetchOfAJSB = DataFetchOfAJSB.GetInstance();
                    dataFetchOfAJSB.YourTask_PullSBDataFromSythpt(); 

                    lock (obj)
                    {
                        sbIsRunning = false;
                    }
                    Public.WriteLog("获取申报数据服务-结束记录日志:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                 
                
                #endregion

            }
            catch (Exception ex)
            {
                lock (obj)
                {
                    isRunning = false;
                    sbIsRunning = false;
                }
                Public.WriteLog(ex.Message);
            }
        }


        #region 拉取数据

        /// <summary>
        /// 从省一体化平台获取数据（江阴立项项目，施工合同备案）到无锡数据中心
        /// </summary>
        void YourTask_PullDataFromSythpt()
        {
            try
            {
                DateTime beginTime = DateTime.Now;
                Public.WriteLog("开始执行YourTask_PullDataFromSythpt任务:" + beginTime.ToString("yyyy-MM-dd HH:mm:ss"));

                XmlHelper xmlHelper = new XmlHelper();
                DataService dataService = new DataService();

                //往数据监控日志表添加一条记录
                DataTable dt_DataJkLog = dataService.GetSchema_DataJkLog();
                DataRow row_DataJkLog = dt_DataJkLog.NewRow();
                dt_DataJkLog.Rows.Add(row_DataJkLog);
                row_DataJkLog["ID"] = dataService.Get_DataJkLogNewID();
                row_DataJkLog["DataFlow"] = DataFlow.省一体化平台到无锡数据中心.ToInt32();
                row_DataJkLog["DataFlowName"] = DataFlow.省一体化平台到无锡数据中心.ToString();
                row_DataJkLog["ServiceUrl"] = "http://58.213.147.230:8000/tDataService/ReceiveDataService.ws?wsdl";

                row_DataJkLog["csTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                Int64 id = dataService.Get_DataJkDataDetailNewID().ToInt64();

                dataService.Submit_DataJkLog(dt_DataJkLog);

                ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient client = new ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient();
                #region  从省一体化平台获取江阴地区的立项项目信息 项目信息都走一号通获取，所以这部分删除
                /**
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
                }

                row_DataJkDataDetail["allCount"] = allCount_jyxm;
                row_DataJkDataDetail["successCount"] = successCount_jyxm;

                dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail);

                */
                #endregion

                #region 获取省一体化平台合同备案信息数据处理

                string userName_htba = "320200", password_htba = "we&gjh45H";

                //DataTable dt_TBProjectInfo_all = dataService.GetTBData_AllTBProjectInfo();
                DataTable dt_DataJkDataDetail2 = dataService.GetSchema_DataJkDataDetail();

                //DateTime beginDate = DateTime.Today.AddDays(-730);
                DateTime beginDate = DateTime.Today.AddDays(-5);
                DateTime endDate = DateTime.Today.AddDays(1);


                //往数据监控日志表项添加一条记录
                DataRow row_DataJkDataDetail2 = dt_DataJkDataDetail2.NewRow();
                dt_DataJkDataDetail2.Rows.Add(row_DataJkDataDetail2);

                row_DataJkDataDetail2["ID"] = ++id;
                row_DataJkDataDetail2["DataJkLogID"] = row_DataJkLog["ID"];
                row_DataJkDataDetail2["tableName"] = "TBContractRecordManageTime";
                row_DataJkDataDetail2["MethodName"] = "TBContractRecordManageTime";

                string msg_htba = "";
                int allCount = 0, successCount = 0;
                int is_OK = 0;
                try
                {
                    DataTable dt_SaveDataLog2 = dataService.GetSchema_SaveDataLog();

                    //Public.WriteLog("all:" + dt_TBProjectInfo_all.Rows.Count);

                    string xmlData = String.Empty;

                    #region 一次服务调用

                    try
                    {
                        xmlData = client.TBContractRecordManageTime("", beginDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"), userName_htba, password_htba);

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
                        if (dt != null || dt.Rows.Count > 0)
                        {
                            allCount = dt.Rows.Count;
                            foreach (DataRow row2 in dt.Rows)
                            {
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
                                        row3["cjrqsj"] = DateTime.Now;
                                        row3["xgrqsj"] = DateTime.Now;  
                                    }
                                    else
                                    {
                                        row3 = dt_TBContractRecordManage.Rows[0];
                                        DataTableHelp.DataRow2DataRow(row2, row3, new List<string>() { "PKID" });
                                        row3["xgrqsj"] = DateTime.Now;  
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

                                    //row3["PrjNum"] = row2["PrjNum"];
                                    row3["Tag"] = Tag.省一体化平台.ToString();
                                    row3["updateUser"] = userName_htba;

                                    //新增工程类型
                                    if (string.IsNullOrEmpty(row3["PrjType"].ToString()))
                                    {
                                        row3["PrjType"] = row3["PrjNum"].ToString().Substring(12, 2);
                                    }
                                    

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

                        }

                    }
                    catch (Exception ex)
                    {
                        /**
                        row_DataJkDataDetail2["IsOk"] = 0;
                        row_DataJkDataDetail2["allCount"] = allCount;
                        row_DataJkDataDetail2["successCount"] = successCount;
                        row_DataJkDataDetail2["ErrorMsg"] = ex.Message;*/
                        Public.WriteLog("获取:" + beginDate.ToString("yyyy-MM-dd") + "-" + endDate.ToString("yyyy-MM-dd") + "合同备案信息失败:" + ex.Message);
                    }

                    #endregion

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

                    Public.WriteLog("out ex:" + ex.Message);
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

                DateTime jgBeginDate = DateTime.Today.AddDays(-5);
                DateTime jgEndDate = DateTime.Today.AddDays(1);

                try
                {
                    DataTable dt_SaveDataLog3 = dataService.GetSchema_SaveDataLog();


                    string xmlData = String.Empty;

                    #region 竣工备案

                    xmlData = client.RetrieveData("TBProjectFinishManage", "", jgBeginDate.ToString(), jgEndDate.ToString(), userName_jgba, password_jgba);

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
                }

                if (dt_DataJkDataDetail3.Rows.Count > 0)
                    dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail3);

                #endregion

                DateTime endTime = DateTime.Now;
                TimeSpan span = compareDateTime(beginTime, endTime);
                Public.WriteLog(string.Format("结束YourTask_PullDataFromSythpt任务:{0}，历时：{1}时{2}分{3}秒", endTime.ToString("yyyy-MM-dd HH:mm:ss"), span.Hours, span.Minutes, span.Seconds));
            }
            catch (Exception ex)
            {
                Public.WriteLog("执行YourTask_PullDataFromSythpt方法出现异常:" + ex.Message);
            }
        }

        /// <summary>
        /// 从省施工图审查管理系统获取信息无锡地区施工图审查信息
        /// </summary>
        void YourTask_PullDataFromStTBProjectCensorInfo()
        {
            string apiMessage = string.Empty;
            //string methodMessage = string.Empty;
            DataService dataService = new DataService();
            DataTable dtapizb = dataService.Get_API_zb_apiFlow("1");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                try
                {
                    DateTime beginTime = DateTime.Now;
                    Public.WriteLog("开始执行YourTask_PullDataFromStTBProjectCensorInfo任务:" + beginTime.ToString("yyyy-MM-dd HH:mm:ss"));

                    #region 从省厅获取信息无锡地区施工图审查信息数据处理
                    string sstxmUserName = "jsst", sstxmPwd = "jsst123!@#";

                    //往数据监控日志表添加一条记录
                    DataTable dt_DataJkLog = dataService.GetSchema_DataJkLog();
                    DataRow row_DataJkLog = dt_DataJkLog.NewRow();
                    dt_DataJkLog.Rows.Add(row_DataJkLog);
                    row_DataJkLog["ID"] = dataService.Get_DataJkLogNewID();
                    row_DataJkLog["DataFlow"] = DataFlow.省审图系统到无锡数据中心.ToInt32();
                    row_DataJkLog["DataFlowName"] = DataFlow.省审图系统到无锡数据中心.ToString();
                    row_DataJkLog["ServiceUrl"] = "http://221.226.0.185/AppSgtSjsc/WebServiceStxm.asmx";
                    row_DataJkLog["csTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                    //往数据监控日志表项添加一条记录
                    DataTable dt_DataJkDataDetail = dataService.GetSchema_DataJkDataDetail();
                    DataRow row_DataJkDataDetail = dt_DataJkDataDetail.NewRow();
                    dt_DataJkDataDetail.Rows.Add(row_DataJkDataDetail);
                    DataRow row_DataJkDataDetail2 = dt_DataJkDataDetail.NewRow();
                    dt_DataJkDataDetail.Rows.Add(row_DataJkDataDetail2);

                    row_DataJkDataDetail["ID"] = dataService.Get_DataJkDataDetailNewID();
                    row_DataJkDataDetail["DataJkLogID"] = row_DataJkLog["ID"];
                    row_DataJkDataDetail["tableName"] = "TBProjectCensorInfo";
                    row_DataJkDataDetail["MethodName"] = "ReadStxmByStStandard";


                    row_DataJkDataDetail2["ID"] = row_DataJkDataDetail["ID"].ToInt64() + 1;
                    row_DataJkDataDetail2["DataJkLogID"] = row_DataJkLog["ID"];
                    row_DataJkDataDetail2["tableName"] = "TBProjectDesignEconUserInfo";
                    row_DataJkDataDetail2["MethodName"] = "ReadStxmByStStandard";

                    int allCount = 0, successCount = 0;
                    int allCount_ry = 0, successCount_ry = 0;
                    int is_OK = 0;
                    string webServiceMsg = "";

                    WebServiceStxm.WebServiceStxm client;
                    string resultString = "", resultStringRaw = "";
                    try
                    {
                        DateTime beginDate = DateTime.Today.AddDays(-7);
     
                        client = new WxjzgcjczyTimerService.WebServiceStxm.WebServiceStxm();
                        client.Timeout = 6000000;
                        resultStringRaw = client.ReadStxmByStStandard(sstxmUserName, sstxmPwd, "", "", "320200", beginDate.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));
                        resultString = xmlHelper.ConvertSpecialLetter(resultStringRaw);
                        //Public.WriteLog(resultString);
                        is_OK = 1;
                    }
                    catch (Exception ex)
                    {
                        webServiceMsg = ex.Message;
                        Public.WriteLog(webServiceMsg);
                        //methodMessage += "ReadStxmByStStandard:" + ex.Message + ";";

                    }

                    int index_TBProjectCensorInfo = resultString.IndexOf("<TBProjectCensorInfo>");

                    if (index_TBProjectCensorInfo >= 0)
                    {
                        string tBProjectCensorInfoString = resultString.Substring(index_TBProjectCensorInfo, resultString.IndexOf("</TBProjectCensorInfo>") - index_TBProjectCensorInfo + 22);
                        if (!tBProjectCensorInfoString.Equals("<TBProjectCensorInfo></TBProjectCensorInfo>", StringComparison.CurrentCultureIgnoreCase))
                        {
                            string msg = String.Empty;
                            DataTable dt = xmlHelper.ConvertXMLToDataTable(tBProjectCensorInfoString, out msg);
                            if (string.IsNullOrEmpty(msg))
                            {
                                allCount += dt.Rows.Count;

                                DataTable dt_SaveDataLog = dataService.GetSchema_SaveDataLog();

                                foreach (DataRow item in dt.Rows)
                                {
                                    DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                                    dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                                    row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail["ID"];

                                    row_SaveDataLog["DataXml"] = xmlHelper.ConvertDataRowToXML(item);

                                    DataTable dt_TBProjectCensorInfo = dataService.GetTBData_TBProjectCensorInfoByPkId(item["PKID"].ToString());
                                    if (dt_TBProjectCensorInfo.Rows.Count == 0)
                                    {
                                        dt_TBProjectCensorInfo.Rows.Add(dt_TBProjectCensorInfo.NewRow());
                                        dt_TBProjectCensorInfo.Rows[0]["cjrqsj"] = DateTime.Now;
                                    }

                                    DataTableHelp.DataRow2DataRow(item, dt_TBProjectCensorInfo.Rows[0]);

                                    string econCorpCode = dt_TBProjectCensorInfo.Rows[0]["EconCorpCode"].ToString2();
                                    if (!string.IsNullOrEmpty(econCorpCode) && !econCorpCode.Equals("-"))
                                    {
                                        if (econCorpCode.Length == 9)
                                        {
                                            econCorpCode = econCorpCode.Substring(0, 8) + "-" + econCorpCode.Substring(8, 1);
                                        }

                                        if (econCorpCode.Length == 10)
                                        {
                                            //检查该企业是否存在社会信用代码，若存在，则转化为社会信用代码
                                            string qyShxydm = dataService.Get_UEPP_Qyjbxx_Shxydm(econCorpCode);
                                            if (!string.IsNullOrEmpty(qyShxydm))
                                            {
                                                dt_TBProjectCensorInfo.Rows[0]["EconCorpCode"] = qyShxydm;
                                            }
                                        }
                                    }

                                    string designCorpCode = dt_TBProjectCensorInfo.Rows[0]["DesignCorpCode"].ToString2();
                                    if (!string.IsNullOrEmpty(designCorpCode) && !designCorpCode.Equals("-"))
                                    {
                                        if (designCorpCode.Length == 9)
                                        {
                                            designCorpCode = designCorpCode.Substring(0, 8) + "-" + designCorpCode.Substring(8, 1);
                                        }

                                        if (designCorpCode.Length == 10)
                                        {
                                            //检查该企业是否存在社会信用代码，若存在，则转化为社会信用代码
                                            string qyShxydm = dataService.Get_UEPP_Qyjbxx_Shxydm(designCorpCode);
                                            if (!string.IsNullOrEmpty(qyShxydm))
                                            {
                                                dt_TBProjectCensorInfo.Rows[0]["DesignCorpCode"] = qyShxydm;
                                            }
                                        }
                                    }

                                    dt_TBProjectCensorInfo.Rows[0]["xgrqsj"] = DateTime.Now;


                                    try
                                    {
                                        if (!dataService.SaveTBData_TBProjectCensorInfo(dt_TBProjectCensorInfo))
                                        {
                                            row_SaveDataLog["SaveState"] = 0;
                                            row_SaveDataLog["Msg"] = "保存施工图审查项目信息时失败：CensorNum：" + item["CensorNum"] + ",PrjNum：" + item["PrjNum"];
                                        }
                                        else
                                        {
                                            row_SaveDataLog["SaveState"] = 1;
                                            successCount++;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        row_SaveDataLog["Msg"] = ex.Message;
                                    }
                                    row_SaveDataLog["PKID"] = item["PKID"].ToString();
                                    row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                }
                                if (dt_SaveDataLog.Rows.Count > 0)
                                    dataService.Submit_SaveDataLog(dt_SaveDataLog);
                            }
                            else
                            {
                                Public.WriteLog("解析XML失败:" + msg);
                            }
                        }
                    }

                    int index_TBProjectDesignEconUserInfo = resultString.IndexOf("<TBProjectDesignEconUserInfo>");

                    if (index_TBProjectDesignEconUserInfo >= 0)
                    {
                        string tBProjectDesignEconUserInfoString = resultString.Substring(index_TBProjectDesignEconUserInfo, resultString.IndexOf("</TBProjectDesignEconUserInfo>") - index_TBProjectDesignEconUserInfo + 30);
                        if (!tBProjectDesignEconUserInfoString.Equals("<TBProjectDesignEconUserInfo></TBProjectDesignEconUserInfo>", StringComparison.CurrentCultureIgnoreCase))
                        {

                            string msg = String.Empty;

                            DataTable dt = xmlHelper.ConvertXMLToDataTable(tBProjectDesignEconUserInfoString, out msg);
                            if (string.IsNullOrEmpty(msg))
                            {
                                allCount_ry += dt.Rows.Count;

                                //DataTable dt_SaveDataLog = dataService.GetSchema_SaveDataLog();

                                foreach (DataRow item in dt.Rows)
                                {
                                    //DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                                    //dt_SaveDataLog.Rows.Add(row_SaveDataLog);

                                    //row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail2["ID"];

                                    //row_SaveDataLog["DataXml"] = xmlHelper.ConvertDataRowToXML(item);

                                    try
                                    {
                                        DataTable dt_TBProjectDesignEconUserInfo = dataService.GetTBData_TBProjectDesignEconUserInfoByPkId(item["PKID"].ToString());
                                        if (dt_TBProjectDesignEconUserInfo.Rows.Count == 0)
                                        {
                                            dt_TBProjectDesignEconUserInfo.Rows.Add(dt_TBProjectDesignEconUserInfo.NewRow());
                                        }

                                        DataTableHelp.DataRow2DataRow(item, dt_TBProjectDesignEconUserInfo.Rows[0]);
                                        if (string.IsNullOrEmpty(dt_TBProjectDesignEconUserInfo.Rows[0]["DataState"].ToString2()))
                                        {
                                            dt_TBProjectDesignEconUserInfo.Rows[0]["DataState"] = 0;
                                        }

                                        if (!dataService.SaveTBData_TBProjectDesignEconUserInfo(dt_TBProjectDesignEconUserInfo))
                                        {
                                            Public.WriteLog("保存施工图审查合格证失败:" + xmlHelper.ConvertDataRowToXML(item));
                                            //row_SaveDataLog["SaveState"] = 0;
                                            //row_SaveDataLog["Msg"] = "保存施工图审查人员信息时失败：CensorNum：" + item["CensorNum"] + ",UserName：" + item["UserName"];
                                        }
                                        else
                                        {
                                            //row_SaveDataLog["SaveState"] = 1;
                                            successCount_ry++;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Public.WriteLog("保存施工图审查合格证失败:" + xmlHelper.ConvertDataRowToXML(item));
                                        Public.WriteLog("错误信息：" + ex.Message);
                                        //row_SaveDataLog["Msg"] = ex.Message;
                                    }
                                    //row_SaveDataLog["PKID"] = item["PKID"].ToString();
                                    //row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                }
                                //if (dt_SaveDataLog.Rows.Count > 0) dataService.Submit_SaveDataLog(dt_SaveDataLog);
                            }
                            else
                            {
                                Public.WriteLog("解析XML失败:" + msg);
                            }
                        }
                    }

                    if (is_OK > 0)
                    {
                        row_DataJkDataDetail["IsOk"] = 1;
                        row_DataJkDataDetail2["IsOk"] = 1;
                    }
                    else
                    {
                        row_DataJkDataDetail["IsOk"] = 0;
                        row_DataJkDataDetail["ErrorMsg"] = webServiceMsg;

                        row_DataJkDataDetail2["IsOk"] = 0;
                        row_DataJkDataDetail2["ErrorMsg"] = webServiceMsg;
                    }

                    dataService.Submit_DataJkLog(dt_DataJkLog);

                    row_DataJkDataDetail["allCount"] = allCount;
                    row_DataJkDataDetail["successCount"] = successCount;

                    row_DataJkDataDetail2["allCount"] = allCount_ry;
                    row_DataJkDataDetail2["successCount"] = successCount_ry;

                    dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail);

                    #endregion

                    DateTime endTime = DateTime.Now;
                    TimeSpan span = compareDateTime(beginTime, endTime);
                    Public.WriteLog(string.Format("结束YourTask_PullDataFromStTBProjectCensorInfo任务:{0}，历时：{1}时{2}分{3}秒", endTime.ToString("yyyy-MM-dd HH:mm:ss"), span.Hours, span.Minutes, span.Seconds));

                }
                catch (Exception ex)
                {
                    Public.WriteLog("执行YourTask_PullDataFromStTBProjectCensorInfo方法出现异常:" + ex.Message);
                    apiMessage += ex.Message;
                }
                finally
                {
                    DataTable dtapicb = dataService.GetSchema_API_cb();
                    DataRow row_apicb = dtapicb.NewRow();
                    dtapicb.Rows.Add(row_apicb);
                    row_apicb["apiCbID"] = dataService.Get_apiCbNewID();
                    row_apicb["apiFlow"] = "1";
                    row_apicb["apiMethod"] = "ReadStxmByStStandard";
                    row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                    row_apicb["apiDyMessage"] = apiMessage;
                    row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dataService.Submit_API_cb(dtapicb);

                    dataService.UpdateZbJkzt("1", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

                }
            }
        }

        private static TimeSpan compareDateTime(DateTime beginTime, DateTime endTime)
        {
            TimeSpan span1 = new TimeSpan(beginTime.Hour, beginTime.Minute, beginTime.Second);
            TimeSpan span2 = new TimeSpan(endTime.Hour, endTime.Minute, endTime.Second);
            TimeSpan span = span2 - span1;
            return span;
        }

        /// <summary>
        /// 从省施工许可系统拉取施工许可信息
        /// </summary>
        void YourTask_PullDataFromSSgxk()
        {
            string apiMessage = string.Empty;
            //string methodMessage = string.Empty;
            DataService dataService = new DataService();
            DataTable dtapizb = dataService.Get_API_zb_apiFlow("2");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                try
                {
                    DateTime beginTime = DateTime.Now;
                    Public.WriteLog("开始执行YourTask_PullDataFromSSgxk任务:" + beginTime.ToString("yyyy-MM-dd HH:mm:ss"));

                    //往数据监控日志表添加一条记录
                    DataTable dt_DataJkLog = dataService.GetSchema_DataJkLog();
                    DataRow row_DataJkLog = dt_DataJkLog.NewRow();
                    dt_DataJkLog.Rows.Add(row_DataJkLog);
                    row_DataJkLog["ID"] = dataService.Get_DataJkLogNewID();
                    row_DataJkLog["DataFlow"] = DataFlow.省施工许可系统到无锡数据中心.ToInt32();
                    row_DataJkLog["DataFlowName"] = DataFlow.省施工许可系统到无锡数据中心.ToString();
                    row_DataJkLog["ServiceUrl"] = "http://58.213.147.228/JSFront/SGXKDataExchange/DataShareService.asmx";
                    row_DataJkLog["csTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dataService.Submit_DataJkLog(dt_DataJkLog);

                    //往数据监控日志表项添加一条记录
                    DataTable dt_DataJkDataDetail = dataService.GetSchema_DataJkDataDetail();
                    DataRow row_DataJkDataDetail;
                    long Id_DataJkDataDetail = dataService.Get_DataJkDataDetailNewID().ToInt64();


                    DateTime beginDate, endDate;
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["flag"]) && ConfigurationManager.AppSettings["flag"].Equals("1"))
                    {
                        beginDate = DateTime.Parse(ConfigurationManager.AppSettings["beginDate"]);
                        endDate = DateTime.Parse(ConfigurationManager.AppSettings["endDate"]);
                    }
                    else
                    {
                        beginDate = DateTime.Today.AddDays(-5);
                        endDate = DateTime.Today;
                    }

                    DataTable dt_TBProjectInfo = dataService.GetTBData_AllTBProjectInfo();

                    for (DateTime tmpDate = beginDate.AddDays(1); (tmpDate.CompareTo(endDate) < 0); )
                    {
                        row_DataJkDataDetail = PullDataFromSSgxkByDate(dataService, row_DataJkLog, dt_DataJkDataDetail, ref Id_DataJkDataDetail, dt_TBProjectInfo, tmpDate);

                        //执行完之后，往后推一天
                        beginDate = tmpDate;
                        tmpDate = tmpDate.AddDays(1);
                    }

                   

                    if (dt_DataJkDataDetail.Rows.Count > 0)
                        dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail);

                    DateTime endTime = DateTime.Now;
                    TimeSpan span = compareDateTime(beginTime, endTime);
                    Public.WriteLog(string.Format("结束YourTask_PullDataFromSSgxk任务:{0}，历时：{1}时{2}分{3}秒", endTime.ToString("yyyy-MM-dd HH:mm:ss"), span.Hours, span.Minutes, span.Seconds));
                }
                catch (Exception ex)
                {
                    Public.WriteLog("执行YourTask_PullDataFromSSgxk方法出现异常:" + ex.Message);
                    apiMessage += ex.Message;
                }
                finally
                {
                    DataTable dtapicb = dataService.GetSchema_API_cb();
                    DataRow row_apicb = dtapicb.NewRow();
                    dtapicb.Rows.Add(row_apicb);
                    row_apicb["apiCbID"] = dataService.Get_apiCbNewID();
                    row_apicb["apiFlow"] = "2";
                    row_apicb["apiMethod"] = "GetTBData_AllTBProjectInfo;ShareDataXML2";
                    row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                    row_apicb["apiDyMessage"] = apiMessage;
                    row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dataService.Submit_API_cb(dtapicb);

                    dataService.UpdateZbJkzt("2", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

                }
            }
        }

        private DataRow PullDataFromSSgxkByDate(DataService dataService, DataRow row_DataJkLog, DataTable dt_DataJkDataDetail, ref long Id_DataJkDataDetail, 
            DataTable dt_TBProjectInfo,
            DateTime date)
        {
            DataRow row_DataJkDataDetail;
            #region 数据处理

            DataShareServiceSpace.DataShareServiceSoapClient dataShareService = new DataShareServiceSpace.DataShareServiceSoapClient();
            //DataTable dt_TBProjectInfo = dataService.GetTBData_AllTBProjectInfo();

            /**
            int pageCount = 0;
            try
            {
                pageCount = dataShareService.GetTatalPageNum("3202000", "p3202000", date, date, 9);
            }
            catch (Exception ex)
            {
                row_DataJkDataDetail = dt_DataJkDataDetail.NewRow();
                dt_DataJkDataDetail.Rows.Add(row_DataJkDataDetail);

                row_DataJkDataDetail["ID"] = Id_DataJkDataDetail++;
                row_DataJkDataDetail["tableName"] = "TBBuilderLicenceManage";
                row_DataJkDataDetail["MethodName"] = "GetTatalPageNum";
                row_DataJkDataDetail["allCount"] = 0;
                row_DataJkDataDetail["successCount"] = 0;
                row_DataJkDataDetail["IsOk"] = 0;
                row_DataJkDataDetail["ErrorMsg"] = ex.Message;
                dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail);
                //return;
            }*/

            Public.WriteLog(date.ToString() + "获取" + date.ToString() + "施工许可数据：");

            int allCount_xm = 0, success_xm = 0;
            bool is_Ok = false;
            string errorMsg = "";
            row_DataJkDataDetail = dt_DataJkDataDetail.NewRow();
            dt_DataJkDataDetail.Rows.Add(row_DataJkDataDetail);
            row_DataJkDataDetail["ID"] = Id_DataJkDataDetail++;

            row_DataJkDataDetail["DataJkLogID"] = row_DataJkLog["ID"];
            row_DataJkDataDetail["tableName"] = "TBBuilderLicenceManage";
            row_DataJkDataDetail["MethodName"] = "ShareDataXML2";

            string resultStringRaw = string.Empty;
            string resultXmlString = string.Empty;

            try
            {
                //for (int pageNum = 1; pageNum <= pageCount; pageNum++)
                //{
                int pageNum = 0;
                   // Public.WriteLog("第" + pageNum + "页");
                    try
                    {
                        resultStringRaw = dataShareService.ShareDataXML2("3202000", "p3202000", date, date.AddDays(1), 9, pageNum);
                    }
                    catch (Exception ex)
                    {
                        errorMsg = ex.Message;
                        Public.WriteLog("调用接口ShareDataXML2异常:pageNum：" + pageNum + ",beginDate:" + date.ToString()  + ",ex:" + ex.Message);
                        //continue;
                    }
                    Public.WriteLog(resultXmlString);
                    resultXmlString = xmlHelper.ConvertSpecialLetter(resultStringRaw);
                    is_Ok = true;

                    DataBody dataBody = xmlHelper.DeserializeXML<DataBody>("<DataBody>" + resultXmlString + "</DataBody>");
                    string message = string.Empty;
                    if (dataBody.xkxms == null || dataBody.xkxms.Count == 0)
                    {
                        row_DataJkDataDetail["ErrorMsg"] = "无数据";
                        row_DataJkDataDetail["allCount"] = 0;
                        row_DataJkDataDetail["successCount"] = 0;
                        row_DataJkDataDetail["IsOk"] = 0;

                        //continue;
                    }

                    //List<Xkxm> toSaveXkxm = dataBody.xkxms.Where(p => (!string.IsNullOrEmpty(p.projectInfo.XiangMuBeiAnNum) && !string.IsNullOrEmpty(p.gcxx.applyConstInfo.XuKeZhengNum) && p.projectInfo.CodeNum.IndexOf("3202") >= 0)).ToList<Xkxm>();

                    List<Xkxm> toSaveXkxm = dataBody.xkxms.Where(p => (
                       p.projectInfo.XMSuoZaiDi.IndexOf("3202") >= 0
                       && !string.IsNullOrEmpty(p.projectInfo.XiangMuBeiAnNum)
                       && !string.IsNullOrEmpty(p.gcxx.applyConstInfo.XuKeZhengNum))).ToList<Xkxm>();

                    Public.WriteLog("第" + pageNum + "页，获取了" + toSaveXkxm.Count + "条施工许可数据！原有总条数：" + dataBody.xkxms.Count);
                    if (toSaveXkxm.Count == 0)
                    {
                        row_DataJkDataDetail["ErrorMsg"] = "无数据";
                        row_DataJkDataDetail["allCount"] = 0;
                        row_DataJkDataDetail["successCount"] = 0;
                        row_DataJkDataDetail["IsOk"] = 0;

                        //continue;
                    }

                    DataTable dt_SaveDataLog = dataService.GetSchema_SaveDataLog();
                    allCount_xm += toSaveXkxm.Count;
                    foreach (Xkxm xkxm in toSaveXkxm)
                    {
                        DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                        dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                        row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail["ID"];
                        row_SaveDataLog["DataXml"] = xmlHelper.SerializeXML<Xkxm>(xkxm);
                        row_SaveDataLog["CreateDate"] = DateTime.Now.ToString();

                        try
                        {
                            if (string.IsNullOrEmpty(xkxm.projectInfo.XiangMuBeiAnNum))
                            {
                                row_SaveDataLog["SaveState"] = 0;
                                row_SaveDataLog["PKID"] = xkxm.gcxx.applyConstInfo.RowGuid.ToString2();
                                row_SaveDataLog["Msg"] = "项目“" + xkxm.gcxx.applyConstInfo.GongChengName + "”的PrjNum不能为空，施工许可证编号为" + xkxm.gcxx.applyConstInfo.XuKeZhengNum + "！";

                                continue;
                            }

                            if (string.IsNullOrEmpty(xkxm.gcxx.applyConstInfo.XuKeZhengNum))
                            {
                                row_SaveDataLog["SaveState"] = 0;
                                row_SaveDataLog["PKID"] = xkxm.gcxx.applyConstInfo.RowGuid.ToString2();
                                row_SaveDataLog["Msg"] = "项目编号为“" + xkxm.projectInfo.XiangMuBeiAnNum + "”的项目施工许可证编号不能为空！";
                                continue;
                            }

                            if (string.IsNullOrEmpty(xkxm.gcxx.applyConstInfo.BuilderLicenceInnerNum)
                                 && string.IsNullOrEmpty(xkxm.gcxx.applyConstInfo.XuKeZhengNum))
                            {
                                row_SaveDataLog["SaveState"] = 0;
                                row_SaveDataLog["PKID"] = xkxm.gcxx.applyConstInfo.RowGuid.ToString2();
                                row_SaveDataLog["Msg"] = "项目编号为“" + xkxm.projectInfo.XiangMuBeiAnNum + "”的项目BuilderLicenceInnerNum与XuKeZhengNum不能为空！";

                                continue;
                            }

                            int flag = 0;
                            foreach (DataRow row in dt_TBProjectInfo.Rows)
                            {
                                if (row["PrjNum"].ToString().Equals(xkxm.projectInfo.XiangMuBeiAnNum))
                                {
                                    flag = 1;
                                    break;
                                }
                            }
                            if (flag == 0)
                            {
                                row_SaveDataLog["SaveState"] = 0;
                                row_SaveDataLog["PKID"] = xkxm.gcxx.applyConstInfo.RowGuid.ToString2();
                                row_SaveDataLog["Msg"] = "项目编号“" + xkxm.projectInfo.XiangMuBeiAnNum + "”不存在，施工许可证编号为" + xkxm.gcxx.applyConstInfo.XuKeZhengNum + "的施工许可添加失败！";
                                continue;
                            }

                            DataTable dt = dataService.GetTBData_TBBuilderLicenceManage(xkxm.gcxx.applyConstInfo.BuilderLicenceInnerNum, xkxm.gcxx.applyConstInfo.XuKeZhengNum);
                            DataRow dataRow;
                            StringBuilder str = new StringBuilder();

                            if (dt.Rows.Count == 0)
                            {
                                dataRow = dt.NewRow();
                                dt.Rows.Add(dataRow);
                                dataRow["PKID"] = xkxm.gcxx.applyConstInfo.RowGuid;
                            }
                            else
                            {
                                dataRow = dt.Rows[0];

                                if (dataRow["CreateDate"].ToString2() == xkxm.gcxx.applyConstInfo.TiaoBaoDate)
                                {
                                    continue;
                                }
                            }
                            if (!string.IsNullOrEmpty(xkxm.gcxx.applyConstInfo.BuilderLicenceInnerNum))
                                dataRow["BuilderLicenceInnerNum"] = xkxm.gcxx.applyConstInfo.BuilderLicenceInnerNum;
                            else
                                dataRow["BuilderLicenceInnerNum"] = xkxm.gcxx.applyConstInfo.XuKeZhengNum;

                            dataRow["BuilderLicenceNum"] = xkxm.gcxx.applyConstInfo.XuKeZhengNum;
                            dataRow["BuilderLicenceName"] = xkxm.gcxx.applyConstInfo.GongChengName;
                            dataRow["RecordInnerNum"] = xkxm.gcxx.applyConstInfo.BuilderLicenceInnerNum;

                            dataRow["PrjNum"] = xkxm.projectInfo.XiangMuBeiAnNum;
                            dataRow["BuldPlanNum"] = "";
                            dataRow["ProjectPlanNum"] = "";
                            dataRow["CensorNum"] = "";
                            if (string.IsNullOrEmpty(xkxm.gcxx.applyConstInfo.HeTongJia))
                                dataRow["ContractMoney"] = 0;
                            else
                                dataRow["ContractMoney"] = xkxm.gcxx.applyConstInfo.HeTongJia;

                            if (string.IsNullOrEmpty(xkxm.gcxx.applyConstInfo.JianZuArea))
                                dataRow["Area"] = "0";
                            else
                                dataRow["Area"] = xkxm.gcxx.applyConstInfo.JianZuArea;
                            dataRow["PrjSize"] = xkxm.gcxx.applyConstInfo.JIanSheGuiMo;

                            dataRow["IssueCertDate"] = string.IsNullOrEmpty(xkxm.gcxx.applyConstInfo.ApplyZSDate) ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : xkxm.gcxx.applyConstInfo.ApplyZSDate;

                            string EconCorpName = String.Empty, EconCorpCode = String.Empty,
                                DesignCorpName = String.Empty, DesignCorpCode = String.Empty
                                , ConsCorpName = String.Empty, ConsCorpCode = String.Empty
                                , ConstructorName = String.Empty, CIDCardTypeNum = String.Empty, ConstructorIDCard = String.Empty, ConstructorPhone = String.Empty
, SuperCorpName = String.Empty, SuperCorpCode = String.Empty, SupervisionName = String.Empty, SIDCardTypeNum = String.Empty, SupervisionIDCard = String.Empty, SupervisionPhone = String.Empty;

                            #region 单位信息

                            foreach (Jscin_SGXK_CanJianDanW cjdw in xkxm.gcxx.canJianDanW)
                            {
                                string zzjgdm = "";
                                if (!string.IsNullOrEmpty(cjdw.OrganizationRegCode)
                                    && cjdw.OrganizationRegCode.Length == 9 && cjdw.OrganizationRegCode.IndexOf('-') < 0)
                                {
                                    zzjgdm = cjdw.OrganizationRegCode.Substring(0, 8) + "-" + cjdw.OrganizationRegCode.Substring(8);
                                }
                                else
                                {
                                    zzjgdm = cjdw.OrganizationRegCode;
                                }

                                //检查该企业是否存在社会信用代码，若存在，则转化为社会信用代码
                                if (zzjgdm.Length != 18)
                                {
                                    string qyShxydm = dataService.Get_UEPP_Qyjbxx_Shxydm(zzjgdm);
                                    if (!string.IsNullOrEmpty(qyShxydm))
                                    {
                                        zzjgdm = qyShxydm;
                                    }
                                }

                                switch (cjdw.CanJianDWType)
                                {
                                    case "0004"://"勘察单位":

                                        EconCorpName = cjdw.CanJianDWName;
                                        EconCorpCode = zzjgdm;
                                        break;

                                    case "0003"://"设计单位":

                                        DesignCorpName = cjdw.CanJianDWName;
                                        DesignCorpCode = zzjgdm;

                                        break;
                                    case "0002"://"施工单位（总包）":

                                        ConsCorpName = cjdw.CanJianDWName;
                                        ConsCorpCode = zzjgdm;

                                        ConstructorName = cjdw.CanJianFZMan;
                                        CIDCardTypeNum = "1";
                                        ConstructorIDCard = cjdw.IdentifyNum;
                                        ConstructorPhone = "";
                                        break;

                                    case "0001"://"监理单位":

                                        SuperCorpName = cjdw.CanJianDWName;
                                        SuperCorpCode = zzjgdm;

                                        SupervisionName = cjdw.CanJianFZMan;
                                        SIDCardTypeNum = "1";
                                        SupervisionIDCard = cjdw.IdentifyNum;
                                        SupervisionPhone = "";

                                        break;

                                    case "0005"://"施工单位（分包）":

                                        ConsCorpName = cjdw.CanJianDWName;
                                        ConsCorpCode = zzjgdm;

                                        ConstructorName = cjdw.CanJianFZMan;
                                        CIDCardTypeNum = "1";
                                        ConstructorIDCard = cjdw.IdentifyNum;
                                        ConstructorPhone = "";
                                        break;
                                    case "0006"://"工程总承包":

                                        ConsCorpName = cjdw.CanJianDWName;
                                        ConsCorpCode = zzjgdm;

                                        ConstructorName = cjdw.CanJianFZMan;
                                        CIDCardTypeNum = "1";
                                        ConstructorIDCard = cjdw.IdentifyNum;
                                        ConstructorPhone = "";
                                        break;

                                    case "0007"://"专业承包":

                                        ConsCorpName = cjdw.CanJianDWName;
                                        ConsCorpCode = zzjgdm;

                                        ConstructorName = cjdw.CanJianFZMan;
                                        CIDCardTypeNum = "1";
                                        ConstructorIDCard = cjdw.IdentifyNum;
                                        ConstructorPhone = "";
                                        break;
                                }

                                //新增TBBuilderLicenceManageCanJianDanW
                                DataTable cjdwDt = dataService.GetTBData_TBBuilderLicenceManageCanJianDanW(cjdw.RowGuid);
                                DataRow cjdwRow;
                                if (cjdwDt.Rows.Count == 0)
                                {
                                    cjdwRow = cjdwDt.NewRow();
                                    cjdwDt.Rows.Add(cjdwRow);
                                    cjdwRow["RowGuid"] = cjdw.RowGuid;
                                    cjdwRow["GCRowGuid"] = cjdw.GCRowGuid;
                                    cjdwRow["JSGuid"] = cjdw.JSGuid;
                                    cjdwRow["CanJianDWType"] = cjdw.CanJianDWType;
                                    cjdwRow["CanJianDWName"] = cjdw.CanJianDWName;
                                    cjdwRow["CanJianDWZiZhi"] = cjdw.CanJianDWZiZhi;
                                    cjdwRow["CanJianDWZiZhiDJ"] = cjdw.CanJianDWZiZhiDJ;
                                    cjdwRow["CanJianFZMan"] = cjdw.CanJianFZMan;
                                    cjdwRow["FZManZhenshu"] = cjdw.FZManZhenshu;
                                    cjdwRow["OrderNumber"] = cjdw.OrderNumber;
                                    cjdwRow["IdentifyNum"] = cjdw.IdentifyNum;
                                    cjdwRow["OrganizationRegCode"] = zzjgdm;
                                    cjdwRow["BuilderLicenceNum"] = dataRow["BuilderLicenceNum"];
                                    try
                                    {
                                        if (!dataService.Submit_TBBuilderLicenceManageCanJianDanW(cjdwDt))
                                        {
                                            Public.WriteLog("保存参建单位出错:" + cjdw.RowGuid + ", zzjgdm:" + zzjgdm + ",CanJianDWName:" + cjdw.CanJianDWName);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                            Public.WriteLog("保存参建单位出错:" + cjdw.RowGuid + ", zzjgdm:" + zzjgdm + ",CanJianDWName:" + cjdw.CanJianDWName);
                                         
                                    }

                                   
                                }
                                


                            }
                            #endregion

                            if (String.IsNullOrEmpty(EconCorpName))
                            {
                                dataRow["EconCorpName"] = "/";
                            }
                            else
                            {
                                dataRow["EconCorpName"] = EconCorpName;
                            }
                            if (String.IsNullOrEmpty(EconCorpCode))
                            {
                                dataRow["EconCorpCode"] = "/";
                            }
                            else
                            {
                                dataRow["EconCorpCode"] = EconCorpCode;
                            }

                            if (String.IsNullOrEmpty(DesignCorpName))
                            {
                                dataRow["DesignCorpName"] = "/";
                            }
                            else
                            {
                                dataRow["DesignCorpName"] = DesignCorpName;
                            }

                            if (String.IsNullOrEmpty(DesignCorpCode))
                            {
                                dataRow["DesignCorpCode"] = "/";
                            }
                            else
                            {
                                dataRow["DesignCorpCode"] = DesignCorpCode;
                            }

                            if (String.IsNullOrEmpty(ConsCorpName))
                            {
                                //dataRow["ConsCorpName"] = "/";
                                row_SaveDataLog["SaveState"] = 0;
                                row_SaveDataLog["PKID"] = xkxm.gcxx.applyConstInfo.RowGuid.ToString2();
                                row_SaveDataLog["Msg"] = "施工许可证编号为“" + xkxm.gcxx.applyConstInfo.XuKeZhengNum + "”的项目施工单位不能为空！";
                                continue;
                            }
                            else
                            {
                                dataRow["ConsCorpName"] = ConsCorpName;
                            }

                            if (String.IsNullOrEmpty(ConsCorpCode))
                            {
                                dataRow["ConsCorpCode"] = "/";
                            }
                            else
                            {
                                dataRow["ConsCorpCode"] = ConsCorpCode;
                            }

                            if (String.IsNullOrEmpty(ConstructorName))
                            {
                                dataRow["ConstructorName"] = "/";
                                dataRow["CIDCardTypeNum"] = "1";
                                dataRow["ConstructorIDCard"] = "/";
                            }
                            else
                            {
                                dataRow["ConstructorName"] = ConstructorName;
                                dataRow["CIDCardTypeNum"] = CIDCardTypeNum;
                                dataRow["ConstructorIDCard"] = ConstructorIDCard;
                            }

                            if (String.IsNullOrEmpty(SuperCorpName))
                            {
                                dataRow["SuperCorpName"] = "/";
                            }
                            else
                            {
                                dataRow["SuperCorpName"] = SuperCorpName;
                            }

                            if (String.IsNullOrEmpty(SuperCorpCode))
                            {
                                dataRow["SuperCorpCode"] = "/";
                            }
                            else
                            {
                                dataRow["SuperCorpCode"] = SuperCorpCode;
                            }

                            if (String.IsNullOrEmpty(SupervisionName))
                            {
                                dataRow["SupervisionName"] = "/";
                                dataRow["SIDCardTypeNum"] = "1";
                                dataRow["SupervisionIDCard"] = "/";
                                dataRow["SupervisionPhone"] = "";
                            }
                            else
                            {
                                dataRow["SupervisionName"] = SupervisionName;
                                dataRow["SIDCardTypeNum"] = SIDCardTypeNum;
                                dataRow["SupervisionIDCard"] = SupervisionIDCard;
                                dataRow["SupervisionPhone"] = SupervisionPhone;
                            }

                            dataRow["SafetyCerID"] = "";
                            dataRow["CreateDate"] = xkxm.gcxx.applyConstInfo.TiaoBaoDate;
                            dataRow["UpdateFlag"] = "U";
                            dataRow["sbdqbm"] = xkxm.projectInfo.XMSuoZaiDi.Substring(0, 6);

                            if (dt.Rows.Count > 0)
                            {
                                if (dataService.Submit_TBBuilderLicenceManage(dt))
                                {
                                    row_SaveDataLog["SaveState"] = 1;
                                    row_SaveDataLog["PKID"] = dataRow["PKID"];
                                    row_SaveDataLog["Msg"] = "施工许可添加成功！";
                                    row_SaveDataLog["CreateDate"] = DateTime.Now.ToString();
                                    success_xm++;
                                }
                                else
                                {
                                    row_SaveDataLog["SaveState"] = 0;
                                    row_SaveDataLog["PKID"] = dataRow["PKID"];
                                    row_SaveDataLog["Msg"] = "施工许可添加失败！";
                                    row_SaveDataLog["CreateDate"] = DateTime.Now.ToString();
                                }

                                try
                                {
                                    decimal d;

                                    if (!string.IsNullOrEmpty(xkxm.projectInfo.XiangMuBeiAnNum)
                                        && !string.IsNullOrEmpty(xkxm.projectInfo.Longitude)
                                        && !string.IsNullOrEmpty(xkxm.projectInfo.Latitude)
                                        && Decimal.TryParse(xkxm.projectInfo.Longitude, out d)
                                        && Decimal.TryParse(xkxm.projectInfo.Latitude, out d))
                                    {
                                        string sql = "update TBProjectInfo set jd=@jd ,wd=@wd where PrjNum=@PrjNum and (jd=0 or jd is null or jd=0 or wd is null)  ";
                                        SqlParameterCollection sp = dataService.DB.CreateSqlParameterCollection();
                                        sp.Add("@PrjNum", xkxm.projectInfo.XiangMuBeiAnNum);
                                        sp.Add("@jd", xkxm.projectInfo.Longitude);
                                        sp.Add("@wd", xkxm.projectInfo.Latitude);
                                        dataService.DB.ExecuteNonQuerySql(sql, sp);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Public.WriteLog("执行YourTask_PullDataFromSSgxk方法出现异常1:" + ex.Message);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Public.WriteLog("执行YourTask_PullDataFromSSgxk方法出现异常:" + ex.Message);
                            Public.WriteLog("执行YourTask_PullDataFromSSgxk方法出现异常:BuilderLicenceInnerNum:" + xkxm.gcxx.applyConstInfo.BuilderLicenceInnerNum + ",XuKeZhengNum" + xkxm.gcxx.applyConstInfo.XuKeZhengNum);

                            row_SaveDataLog["SaveState"] = 0;
                            row_SaveDataLog["Msg"] = ex.Message;
                        }
                    }
                    if (dt_SaveDataLog.Rows.Count > 0)
                    {
                        dataService.Submit_SaveDataLog(dt_SaveDataLog);
                    }
                //}
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                Public.WriteLog("--执行YourTask_PullDataFromSSgxk方法出现异常:" + ex.Message);
                //methodMessage += "GetTatalPageNum:" + ex.Message + ";";
            }

            row_DataJkDataDetail["ErrorMsg"] = errorMsg;
            row_DataJkDataDetail["allCount"] = allCount_xm;
            row_DataJkDataDetail["successCount"] = success_xm;

            if (is_Ok)
            {
                row_DataJkDataDetail["IsOk"] = 1;
            }
            else
            {
                row_DataJkDataDetail["IsOk"] = 0;
            }

            #endregion
            return row_DataJkDataDetail;
        }


        /// <summary>
        /// 获取一号通系统里的建设单位信息
        /// </summary>
        void YourTask_PullDataFromYht_Jsdwxx()
        {
            string apiMessage = string.Empty;
            //string methodMessage = string.Empty;
            DataService dataService = new DataService();
            DataTable dtapizb = dataService.Get_API_zb_apiFlow("5");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                string tag = Tag.局一号通系统.ToString();
                try
                {
                    //往数据监控日志表添加一条记录
                    DataTable dt_DataJkLog = dataService.GetSchema_DataJkLog();
                    DataRow row_DataJkLog = dt_DataJkLog.NewRow();
                    dt_DataJkLog.Rows.Add(row_DataJkLog);
                    row_DataJkLog["ID"] = dataService.Get_DataJkLogNewID();
                    row_DataJkLog["DataFlow"] = DataFlow.局一号通系统到无锡数据中心.ToInt32();
                    row_DataJkLog["DataFlowName"] = DataFlow.局一号通系统到无锡数据中心.ToString();
                    row_DataJkLog["ServiceUrl"] = "";
                    row_DataJkLog["csTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dataService.Submit_DataJkLog(dt_DataJkLog);

                    //往数据监控日志表项添加一条记录
                    DataTable dt_DataJkDataDetail = dataService.GetSchema_DataJkDataDetail();

                    long Id_DataJkDataDetail = dataService.Get_DataJkDataDetailNewID().ToInt64();

                    DataTable dt = new DataTable();
                    int allCount = 0, success_count = 0;
                    DataRow row_DataJkDataDetail = dt_DataJkDataDetail.NewRow();
                    dt_DataJkDataDetail.Rows.Add(row_DataJkDataDetail);

                    row_DataJkDataDetail["ID"] = Id_DataJkDataDetail++;
                    row_DataJkDataDetail["DataJkLogID"] = row_DataJkLog["ID"];
                    row_DataJkDataDetail["tableName"] = "UEPP_Jsdw";
                    row_DataJkDataDetail["MethodName"] = "Get_DG_QY_JBXX";

                    try
                    {
                        dt = dataService.Get_DG_QY_JBXX();
                    }
                    catch (Exception ex)
                    {
                        row_DataJkDataDetail["allCount"] = 0;
                        row_DataJkDataDetail["successCount"] = 0;
                        row_DataJkDataDetail["IsOk"] = 0;
                        row_DataJkDataDetail["ErrorMsg"] = ex.Message;

                        dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail);
                        return;
                    }

                    DataTable dt_SaveDataLog = dataService.GetSchema_SaveDataLog();

                    foreach (DataRow row in dt.Rows)
                    {
                        string jsdwID = row["qy_card"].ToString2();

                        DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                        row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail["ID"];
                        row_SaveDataLog["DataXml"] = "";
                        row_SaveDataLog["PKID"] = jsdwID;
                        row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");
                        try
                        {
                            if (jsdwID.Length == 9)
                                jsdwID = jsdwID.Substring(0, 8) + "-" + jsdwID.Substring(8, 1);

                            DataTable dt_uepp_jsdw = dataService.Get_uepp_jsdw(jsdwID);
                            if (dt_uepp_jsdw.Rows.Count > 0 && dt_uepp_jsdw.Rows[0]["xgrqsj"].ToString2() == row["shenhe_time"].ToString2())
                            {
                                continue;
                            }

                            DataRow temp;
                            if (dt_uepp_jsdw.Rows.Count == 0)
                            {
                                temp = dt_uepp_jsdw.NewRow();
                                dt_uepp_jsdw.Rows.Add(temp);

                                temp["jsdwID"] = jsdwID;
                                temp["tag"] = tag;
                                temp["dwflid"] = 3;//单位分类ID
                                temp["dwfl"] = "其它";//单位分类ID

                                temp["tag"] = tag;
                            }
                            else
                            {
                                temp = dt_uepp_jsdw.Rows[0];

                                if (temp["tag"].ToString2().IndexOf(Tag.江苏建设公共基础数据平台.ToString()) >= 0)
                                {
                                    continue;
                                }
                                if (temp["tag"].ToString2().IndexOf(tag) < 0)
                                {
                                    temp["tag"] = tag;
                                }
                            }

                            DataTable dt_saveToStLog2 = dataService.Get_SaveToStLog2("UEPP_Jsdw", jsdwID);
                            if (dt_saveToStLog2.Rows.Count == 0)
                            {
                                dt_saveToStLog2.Rows.Add(dt_saveToStLog2.NewRow());
                                dt_saveToStLog2.Rows[0]["PKID"] = jsdwID;
                                dt_saveToStLog2.Rows[0]["TableName"] = "UEPP_Jsdw";
                                dt_saveToStLog2.Rows[0]["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            dt_saveToStLog2.Rows[0]["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                            allCount++;

                            temp["jsdw"] = row["qy_name"];
                            temp["zzjgdm"] = row["qy_card"];
                            temp["dwdz"] = row["qy_address"];
                            temp["fax"] = row["qy_fax"];
                            temp["fddbr"] = row["qy_faren"];
                            temp["fddbrdh"] = row["qy_faren_tel"];

                            temp["lxr"] = row["qy_lianxi_man"];
                            temp["lxdh"] = row["qy_lianxi_man_phone"];
                            temp["yyzz"] = row["qy_yyzz"];

                            temp["xgrqsj"] = string.IsNullOrEmpty(row["shenhe_time"].ToString2()) ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : row["shenhe_time"].ToString2();
                            temp["OperateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            temp["xgr"] = "";
                            temp["DataState"] = 0;

                            try
                            {
                                if (dataService.Submit_uepp_jsdw(dt_uepp_jsdw))
                                {
                                    success_count++;

                                    row_SaveDataLog["SaveState"] = 1;
                                    row_SaveDataLog["Msg"] = "建设单位保存成功！";

                                    dt_saveToStLog2.Rows[0]["SbToZxState"] = "0";
                                    dt_saveToStLog2.Rows[0]["SbToZxMsg"] = "获取成功";
                                }
                                else
                                {
                                    row_SaveDataLog["SaveState"] = 0;
                                    row_SaveDataLog["Msg"] = "建设单位保存失败！";

                                    dt_saveToStLog2.Rows[0]["SbToZxState"] = "1";
                                    dt_saveToStLog2.Rows[0]["SbToZxMsg"] = "获取失败";
                                }
                            }
                            catch (Exception ex)
                            {
                                row_SaveDataLog["SaveState"] = 0;
                                row_SaveDataLog["Msg"] = ex.Message;

                                dt_saveToStLog2.Rows[0]["SbToZxState"] = "1";
                                dt_saveToStLog2.Rows[0]["SbToZxMsg"] = ex.Message;

                            }
                            dataService.Submit_SaveToStLog2(dt_saveToStLog2);
                        }
                        catch (Exception ex)
                        {
                            row_SaveDataLog["SaveState"] = 0;
                            row_SaveDataLog["Msg"] = ex.Message;
                        }

                        dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                    }
                    if (dt_SaveDataLog.Rows.Count > 0)
                        dataService.Submit_SaveDataLog(dt_SaveDataLog);

                    row_DataJkDataDetail["allCount"] = allCount;
                    row_DataJkDataDetail["successCount"] = success_count;
                    row_DataJkDataDetail["IsOk"] = 1;
                    row_DataJkDataDetail["ErrorMsg"] = "";

                    if (dt_DataJkDataDetail.Rows.Count > 0)
                        dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail);
                }
                catch (Exception ex_jsdw)
                {
                    Public.WriteLog("获取建设单位时出现异常:" + ex_jsdw.Message);
                    apiMessage += ex_jsdw.Message;
                }

                finally
                {
                    DataTable dtapicb = dataService.GetSchema_API_cb();
                    DataRow row_apicb = dtapicb.NewRow();
                    dtapicb.Rows.Add(row_apicb);
                    row_apicb["apiCbID"] = dataService.Get_apiCbNewID();
                    row_apicb["apiFlow"] = "5";
                    row_apicb["apiMethod"] = "Get_DG_QY_JBXX;";
                    row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                    row_apicb["apiDyMessage"] = apiMessage;
                    row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dataService.Submit_API_cb(dtapicb);

                    dataService.UpdateZbJkzt("5", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

                }
            }

        }

        /// <summary>
        /// 从无锡勘察设计系统获取企业和人员信息
        /// </summary>
        void YourTask_PullDataFromWxkcsj_qyry()
        {
            string apiMessage = string.Empty;
            //string methodMessage = string.Empty;
            DataService dataService = new DataService();
            DataTable dtapizb = dataService.Get_API_zb_apiFlow("6");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {

                DateTime beginTime = DateTime.Now;
                Public.WriteLog("开始执行YourTask_PullDataFromWxkcsj_qyry任务:" + beginTime.ToString("yyyy-MM-dd HH:mm:ss"));
                string tag = "";

                //往数据监控日志表添加一条记录
                DataTable dt_DataJkLog = dataService.GetSchema_DataJkLog();
                DataRow row_DataJkLog = dt_DataJkLog.NewRow();
                dt_DataJkLog.Rows.Add(row_DataJkLog);
                row_DataJkLog["ID"] = dataService.Get_DataJkLogNewID();
                row_DataJkLog["DataFlow"] = DataFlow.市勘察设计系统到无锡数据中心.ToInt32();
                row_DataJkLog["DataFlowName"] = DataFlow.市勘察设计系统到无锡数据中心.ToString();
                row_DataJkLog["ServiceUrl"] = "";
                row_DataJkLog["csTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dataService.Submit_DataJkLog(dt_DataJkLog);

                #region 勘察设计单位
                tag = Tag.无锡市勘察设计行业信息管理系统.ToString();

                //往数据监控日志表项添加一条记录
                DataTable dt_DataJkDataDetail_qy = dataService.GetSchema_DataJkDataDetail();
                long Id_DataJkDataDetail_qy = dataService.Get_DataJkDataDetailNewID().ToInt64();

                DataTable dt_kcsjqy = new DataTable();
                int allCount_qy = 0, success_count_qy = 0;
                DataRow row_DataJkDataDetail_qy = dt_DataJkDataDetail_qy.NewRow();
                dt_DataJkDataDetail_qy.Rows.Add(row_DataJkDataDetail_qy);

                row_DataJkDataDetail_qy["ID"] = Id_DataJkDataDetail_qy++;
                row_DataJkDataDetail_qy["DataJkLogID"] = row_DataJkLog["ID"];
                row_DataJkDataDetail_qy["tableName"] = "UEPP_Qyjbxx";
                row_DataJkDataDetail_qy["MethodName"] = "Get_Enterprise_Tab";

                try
                {
                    dt_kcsjqy = dataService.Get_Enterprise_Tab();

                    DataTable dt_SaveDataLog = dataService.GetSchema_SaveDataLog();

                    foreach (DataRow row in dt_kcsjqy.Rows)
                    {
                        string qyID = row["ZZJGDM"].ToString2();
                        if (qyID.Length == 9)
                            qyID = qyID.Substring(0, 8) + "-" + qyID.Substring(8, 1);

                        DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                        row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_qy["ID"];
                        row_SaveDataLog["DataXml"] = "";
                        row_SaveDataLog["PKID"] = qyID;
                        row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                        #region 企业信息

                        try
                        {
                            DataTable dt_uepp_kcsjqy = dataService.Get_uepp_Qyjbxx(qyID);


                            DataRow temp;
                            if (dt_uepp_kcsjqy.Rows.Count == 0)
                            {
                                temp = dt_uepp_kcsjqy.NewRow();
                                dt_uepp_kcsjqy.Rows.Add(temp);
                                temp["qyID"] = qyID;
                                temp["tag"] = tag;
                            }
                            else
                            {
                                temp = dt_uepp_kcsjqy.Rows[0];

                                if (temp["tag"].ToString2().IndexOf(tag) < 0)
                                    continue;

                                if (dt_uepp_kcsjqy.Rows[0]["xgrqsj"].ToString2() == row["CheckTime"].ToString2())
                                {
                                    continue;
                                }
                            }

                            DataTable dt_saveToStLog2 = dataService.Get_SaveToStLog2("UEPP_Qyjbxx", qyID);
                            if (dt_saveToStLog2.Rows.Count == 0)
                            {
                                dt_saveToStLog2.Rows.Add(dt_saveToStLog2.NewRow());
                                dt_saveToStLog2.Rows[0]["PKID"] = qyID;
                                dt_saveToStLog2.Rows[0]["TableName"] = "UEPP_Qyjbxx";
                                dt_saveToStLog2.Rows[0]["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            dt_saveToStLog2.Rows[0]["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                            #region 勘察设计企业从事业务类型

                            DataTable dt_uepp_kcsj_csywlx = dataService.Get_uepp_qycsyw_kcsj(qyID);
                            DataRow tempRow;

                            int kcFlag = 0, sjFlag = 0, flag = 0;

                            for (int i = 0; i < dt_uepp_kcsj_csywlx.Rows.Count; i++)
                            {

                                if (dt_uepp_kcsj_csywlx.Rows[i]["csywlxID"].ToString2().Equals("5"))
                                {
                                    if (row["IsKc"].ToInt32(0) > 0)
                                    {
                                        kcFlag = 1;
                                    }
                                    //else
                                    //{
                                    //    dt_uepp_kcsj_csywlx.Rows[i].Delete();
                                    //    flag = 1;
                                    //}
                                }
                                else
                                    if (dt_uepp_kcsj_csywlx.Rows[i]["csywlxID"].ToString2() == "6")
                                    {
                                        if (row["IsSj"].ToInt32(0) > 0)
                                        {
                                            sjFlag = 1;
                                        }
                                        //else
                                        //{
                                        //    dt_uepp_kcsj_csywlx.Rows[i].Delete();
                                        //    flag = 1;
                                        //}
                                    }
                                //else
                                //{
                                //    dt_uepp_kcsj_csywlx.Rows[i].Delete();
                                //    flag = 1;
                                //}
                            }

                            //if (flag > 0)
                            //{
                            //    dt_uepp_kcsj_csywlx.AcceptChanges();
                            //}

                            if (row["IsKc"].ToInt32(0) > 0 && kcFlag == 0)
                            {
                                tempRow = dt_uepp_kcsj_csywlx.NewRow();
                                dt_uepp_kcsj_csywlx.Rows.Add(tempRow);
                                tempRow["qyID"] = qyID;
                                tempRow["csywlxID"] = "5";
                                tempRow["csywlx"] = "工程勘察";
                                tempRow["balxID"] = "0";
                                tempRow["balx"] = "无";
                                tempRow["DataState"] = "0";
                                tempRow["tag"] = tag;

                                tempRow["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                tempRow["xgr"] = "";
                                flag = 1;
                            }

                            if (row["IsSj"].ToInt32(0) > 0 && sjFlag == 0)
                            {
                                tempRow = dt_uepp_kcsj_csywlx.NewRow();
                                dt_uepp_kcsj_csywlx.Rows.Add(tempRow);
                                tempRow["qyID"] = qyID;
                                tempRow["csywlxID"] = "6";
                                tempRow["csywlx"] = "工程设计";
                                tempRow["balxID"] = "0";
                                tempRow["balx"] = "无";
                                tempRow["DataState"] = "0";
                                tempRow["tag"] = tag;
                                tempRow["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                tempRow["xgr"] = "";
                                flag = 1;
                            }

                            if (dt_uepp_kcsj_csywlx.Rows.Count > 0 && flag > 0)
                            {
                                if (!dataService.Submit_uepp_qycsyw(dt_uepp_kcsj_csywlx))
                                {
                                    Public.WriteLog("更新设计企业业务类型时失败！");
                                    continue;
                                }
                            }

                            #endregion

                            temp["qymc"] = row["DWName"];
                            temp["zzjgdm"] = row["ZZJGDM"];
                            temp["yyzzzch"] = row["YYZZZCH"];
                            temp["tyshxydm"] = row["YYZZZCH"];
                            if (row["ZaiSuSuoZDCode"] != DBNull.Value && !string.IsNullOrEmpty(row["ZaiSuSuoZDCode"].ToString2()))
                            {
                                temp["ProvinceID"] = row["ZaiSuSuoZDCode"].ToString2().Substring(0, 2) + "0000";
                                string Province = dataService.DB.ExeSqlForString("select codeInfo from UEPP_Code where codeType='城市地区' and code='" + temp["ProvinceID"] + "'", null);
                                temp["Province"] = Province;

                                temp["CityID"] = row["ZaiSuSuoZDCode"].ToString2().Substring(0, 4) + "00";
                                string City = dataService.DB.ExeSqlForString("select codeInfo from UEPP_Code where codeType='城市地区' and code='" + temp["CityID"] + "'", null);
                                temp["City"] = City;

                                temp["CountyID"] = row["ZaiSuSuoZDCode"].ToString2().Length > 6 ? row["ZaiSuSuoZDCode"].ToString2().Substring(0, 6) : row["ZaiSuSuoZDCode"].ToString2();
                                string County = dataService.DB.ExeSqlForString("select codeInfo from UEPP_Code where codeType='城市地区' and code='" + temp["CountyID"] + "'", null);
                                temp["County"] = County;
                            }
                            temp["zcdd"] = row["GSZCDZ"];
                            temp["xxdd"] = row["XXAddress"];

                            temp["zczb"] = row["ZCZJ"];
                            if (!string.IsNullOrEmpty(row["CLDate"].ToString2()))
                                temp["clrq"] = row["CLDate"].ToString2();
                            temp["yzbm"] = row["PostNo"];

                            temp["cz"] = row["ChuanZheng"];
                            temp["lxr"] = row["LXR_Name"];
                            temp["lxdh"] = row["LXR_LXDH"];
                            temp["fddbr_ryid"] = row["FRDB_ZJNO"];
                            temp["fddbr"] = row["FRDBDW_FZRName"].ToString2().Length > 12 ? row["FRDBDW_FZRName"].ToString2().Substring(0, 12) : row["FRDBDW_FZRName"].ToString2();
                            temp["jsfzr"] = row["JSFZ"];


                            temp["xgrqsj"] = string.IsNullOrEmpty(row["CheckTime"].ToString2()) ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : row["CheckTime"].ToString2();//DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                            temp["xgr"] = "";
                            temp["DataState"] = 0;
                            allCount_qy++;

                            if (dataService.Submit_uepp_qyjbxx(dt_uepp_kcsjqy))
                            {
                                dt_saveToStLog2.Rows[0]["SbToZxState"] = "0";
                                dt_saveToStLog2.Rows[0]["SbToZxMsg"] = "上传成功";

                                success_count_qy++;

                                row_SaveDataLog["SaveState"] = 1;
                                row_SaveDataLog["Msg"] = "市勘察设计企业信息保存成功！";

                            }
                            else
                            {
                                dt_saveToStLog2.Rows[0]["SbToZxState"] = "1";
                                dt_saveToStLog2.Rows[0]["SbToZxMsg"] = "上传失败";

                                row_SaveDataLog["SaveState"] = 0;
                                row_SaveDataLog["Msg"] = "市勘察设计企业信息保存失败！";
                            }

                            dataService.Submit_SaveToStLog2(dt_saveToStLog2);
                        }
                        catch (Exception ex)
                        {
                            //Public.WriteLog("获取勘察设计企业时出现异常:qyID:" + row["ZZJGDM"].ToString2() + ",Msg:" + ex.Message);
                            row_SaveDataLog["SaveState"] = 0;
                            row_SaveDataLog["Msg"] = "获取勘察设计企业时出现异常:qyID:" + row["ZZJGDM"].ToString2() + ",Msg:" + ex.Message;
                        }

                        dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                        #endregion

                        #region 勘察设计企业资质信息

                        try
                        {
                            DataTable dt_uepp_qyzzmx_kcsj = dataService.Get_UEPP_Qyzzmx_kcsj(qyID);
                            DataTable dt_kcsjqy_qyzz = dataService.Get_Enterprise_ZZ_Tab(row["RowGuid"].ToString2());

                            //bool isKc = row_kcsj["iskc"].ToInt32(0) > 0, isSj = row_kcsj["issj"].ToInt32(0) > 0;

                            #region 删除资质信息
                            //for (int i = 0; i < dt_uepp_qyzzmx_kcsj.Rows.Count; i++)
                            //{
                            //    int flag = 0;
                            //    foreach (DataRow item in dt_kcsjqy_qyzz.Rows)
                            //    {
                            //        string zzName = item["ZZName"].ToString2();
                            //        int kcIndex = zzName.IndexOf("勘察");
                            //        int sjIndex = zzName.IndexOf("设计");

                            //        int zzdjIndex = zzName.IndexOf('级');

                            //        int zyIndex = zzName.IndexOf("专业");
                            //        int lwIndex = zzName.IndexOf("劳务");
                            //        int zhIndex = zzName.IndexOf("综合");

                            //        int hyIndex = zzName.IndexOf("行业");
                            //        int zxIndex = zzName.IndexOf("专项");
                            //        int swsIndex = zzName.IndexOf("事务所");

                            //        string zzlb = "";
                            //        if (kcIndex >= 0)
                            //        {
                            //            if (zyIndex >= 0)
                            //            {
                            //                if (zzName.LastIndexOf("-") - zyIndex - 2 > 0)
                            //                    zzlb = zzName.Substring(zyIndex + 3, zzName.LastIndexOf("-") - zyIndex - 2).Trim(new char[] { ' ', '-' });

                            //            }
                            //            else
                            //                if (lwIndex >= 0)
                            //                {
                            //                    if (zzName.LastIndexOf("-") - lwIndex - 2 > 0)
                            //                        zzlb = zzName.Substring(lwIndex + 3, zzName.LastIndexOf("-") - lwIndex - 2).Trim(new char[] { ' ', '-' });
                            //                }
                            //                else if (zhIndex >= 0)
                            //                {
                            //                    if (zzName.LastIndexOf("-") - zhIndex - 2 > 0)
                            //                        zzlb = zzName.Substring(zhIndex + 3, zzName.LastIndexOf("-") - zhIndex - 2).Trim(new char[] { ' ', '-' });
                            //                }


                            //        }
                            //        else
                            //        {
                            //            if (zyIndex >= 0)
                            //            {
                            //                if (zzName.LastIndexOf("-") - zyIndex - 2 > 0)
                            //                    zzlb = zzName.Substring(zyIndex + 3, zzName.LastIndexOf("-") - zyIndex - 2).Trim(new char[] { ' ', '-' });

                            //            }
                            //            else
                            //                if (zhIndex >= 0)
                            //                {
                            //                    if (zzName.LastIndexOf("-") - zhIndex - 2 > 0)
                            //                        zzlb = zzName.Substring(zhIndex + 3, zzName.LastIndexOf("-") - zhIndex - 2).Trim(new char[] { ' ', '-' });

                            //                }
                            //                else if (hyIndex >= 0)
                            //                {
                            //                    if (zzName.LastIndexOf("-") - hyIndex - 2 > 0)
                            //                        zzlb = zzName.Substring(hyIndex + 3, zzName.LastIndexOf("-") - hyIndex - 2).Trim(new char[] { ' ', '-' });
                            //                }
                            //                else if (zxIndex >= 0)
                            //                {
                            //                    if (zzName.LastIndexOf("-") - zxIndex - 2 > 0)
                            //                        zzlb = zzName.Substring(zxIndex + 3, zzName.LastIndexOf("-") - zxIndex - 2).Trim(new char[] { ' ', '-' });
                            //                }
                            //                else if (swsIndex >= 0)
                            //                {
                            //                    if (zzName.LastIndexOf("-") - swsIndex - 3 > 0)
                            //                        zzlb = zzName.Substring(swsIndex + 4, zzName.LastIndexOf("-") - swsIndex - 3).Trim(new char[] { ' ', '-' });
                            //                }
                            //        }

                            //        if (zzlb.Equals(dt_uepp_qyzzmx_kcsj.Rows[i]["zzlb"].ToString2()))
                            //        {
                            //            flag = 1;
                            //            break;
                            //        }
                            //    }
                            //    if (flag == 0)
                            //    {
                            //        dt_uepp_qyzzmx_kcsj.Rows[i].Delete();
                            //    }

                            //}
                            //dt_uepp_qyzzmx_kcsj.AcceptChanges();
                            #endregion

                            //Public.WriteLog("qyID:" + qyID + "获取了" + dt_kcsjqy_qyzz.Rows.Count + "条勘察设计企业资质信息");
                            foreach (DataRow row_qyzz in dt_kcsjqy_qyzz.Rows)
                            {
                                int isExists = -1;
                                string zzlb = "", zzlbId = "";
                                string zzxlId = "", zzxl = "";
                                string zzName = row_qyzz["ZZName"].ToString2();
                                string zsbh = row_qyzz["ZZZSNO"].ToString2();
                                string zsjb = row_qyzz["XKJG_JB"].ToString2();

                                int kcIndex = zzName.IndexOf("勘察");
                                int sjIndex = zzName.IndexOf("设计");

                                int zzdjIndex = zzName.IndexOf('级');

                                int zyIndex = zzName.IndexOf("专业");
                                int lwIndex = zzName.IndexOf("劳务");
                                int zhIndex = zzName.IndexOf("综合");

                                int hyIndex = zzName.IndexOf("行业");
                                int zxIndex = zzName.IndexOf("专项");
                                int swsIndex = zzName.IndexOf("事务所");


                                #region 企业资质证书
                                string zslx = "", zslxID = "";

                                if (!string.IsNullOrEmpty(zsbh))
                                {
                                    DataTable dt_qyzzzs;
                                    if (kcIndex >= 0)
                                    {
                                        if (zsjb.Equals("省级"))
                                        {
                                            dt_qyzzzs = dataService.Get_UEPP_Qyzzzs_kcsj(qyID, "5", "51", zsbh);
                                            if (dt_qyzzzs.Rows.Count == 0)
                                            {

                                                dt_qyzzzs.Rows.Add(dt_qyzzzs.NewRow());
                                                dt_qyzzzs.Rows[0]["zsjlId"] = dataService.Get_uepp_qyzs_NewID();
                                                dt_qyzzzs.Rows[0]["qyID"] = qyID;
                                                dt_qyzzzs.Rows[0]["csywlx"] = "工程勘察";
                                                dt_qyzzzs.Rows[0]["csywlxID"] = "5";

                                                dt_qyzzzs.Rows[0]["zslxID"] = "51";
                                                dt_qyzzzs.Rows[0]["zslx"] = "省工程勘察资质证";
                                                dt_qyzzzs.Rows[0]["sfzzz"] = "1";
                                            }
                                            dt_qyzzzs.Rows[0]["zsbh"] = zsbh;
                                            dt_qyzzzs.Rows[0]["fzrq"] = row_qyzz["XKJG_Date"];
                                            dt_qyzzzs.Rows[0]["zsyxqrq"] = row_qyzz["ZS_YXQDate"];
                                            dt_qyzzzs.Rows[0]["zsyxzrq"] = row_qyzz["ZS_YXQEndDate"];
                                            dt_qyzzzs.Rows[0]["fzdw"] = row_qyzz["XKJG_Name"];

                                            dt_qyzzzs.Rows[0]["DataState"] = 0;
                                            dt_qyzzzs.Rows[0]["tag"] = tag;
                                            dt_qyzzzs.Rows[0]["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                            dataService.Submit_uepp_qyzzzs(dt_qyzzzs);
                                        }
                                        else
                                        {
                                            dt_qyzzzs = dataService.Get_UEPP_Qyzzzs_kcsj(qyID, "5", "50", zsbh);
                                            if (dt_qyzzzs.Rows.Count == 0)
                                            {
                                                dt_qyzzzs.Rows.Add(dt_qyzzzs.NewRow());
                                                dt_qyzzzs.Rows[0]["zsjlId"] = dataService.Get_uepp_qyzs_NewID();
                                                dt_qyzzzs.Rows[0]["qyID"] = qyID;
                                                dt_qyzzzs.Rows[0]["csywlx"] = "工程勘察";
                                                dt_qyzzzs.Rows[0]["csywlxID"] = "5";

                                                dt_qyzzzs.Rows[0]["zslxID"] = "50";
                                                dt_qyzzzs.Rows[0]["zslx"] = "部工程勘察资质证";
                                                dt_qyzzzs.Rows[0]["sfzzz"] = "1";
                                            }
                                            dt_qyzzzs.Rows[0]["zsbh"] = zsbh;
                                            dt_qyzzzs.Rows[0]["fzrq"] = row_qyzz["XKJG_Date"];
                                            dt_qyzzzs.Rows[0]["zsyxqrq"] = row_qyzz["ZS_YXQDate"];
                                            dt_qyzzzs.Rows[0]["zsyxzrq"] = row_qyzz["ZS_YXQEndDate"];
                                            dt_qyzzzs.Rows[0]["fzdw"] = row_qyzz["XKJG_Name"];

                                            dt_qyzzzs.Rows[0]["DataState"] = 0;
                                            dt_qyzzzs.Rows[0]["tag"] = tag;
                                            dt_qyzzzs.Rows[0]["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                            dataService.Submit_uepp_qyzzzs(dt_qyzzzs);
                                        }
                                    }
                                    else
                                    {
                                        if (zsjb.Equals("省级"))
                                        {
                                            dt_qyzzzs = dataService.Get_UEPP_Qyzzzs_kcsj(qyID, "6", "61", zsbh);

                                            if (dt_qyzzzs.Rows.Count == 0)
                                            {
                                                dt_qyzzzs.Rows.Add(dt_qyzzzs.NewRow());
                                                dt_qyzzzs.Rows[0]["zsjlId"] = dataService.Get_uepp_qyzs_NewID();
                                                dt_qyzzzs.Rows[0]["qyID"] = qyID;
                                                dt_qyzzzs.Rows[0]["csywlx"] = "工程设计";
                                                dt_qyzzzs.Rows[0]["csywlxID"] = "6";

                                                dt_qyzzzs.Rows[0]["zslxID"] = "61";
                                                dt_qyzzzs.Rows[0]["zslx"] = "省工程设计资质证";
                                                dt_qyzzzs.Rows[0]["sfzzz"] = "1";
                                            }
                                            dt_qyzzzs.Rows[0]["zsbh"] = zsbh;
                                            dt_qyzzzs.Rows[0]["fzrq"] = row_qyzz["XKJG_Date"];
                                            dt_qyzzzs.Rows[0]["zsyxqrq"] = row_qyzz["ZS_YXQDate"];
                                            dt_qyzzzs.Rows[0]["zsyxzrq"] = row_qyzz["ZS_YXQEndDate"];
                                            dt_qyzzzs.Rows[0]["fzdw"] = row_qyzz["XKJG_Name"];

                                            dt_qyzzzs.Rows[0]["DataState"] = 0;
                                            dt_qyzzzs.Rows[0]["tag"] = tag;
                                            dt_qyzzzs.Rows[0]["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                            dataService.Submit_uepp_qyzzzs(dt_qyzzzs);
                                        }
                                        else
                                        {
                                            dt_qyzzzs = dataService.Get_UEPP_Qyzzzs_kcsj(qyID, "6", "60", zsbh);

                                            if (dt_qyzzzs.Rows.Count == 0)
                                            {
                                                dt_qyzzzs.Rows.Add(dt_qyzzzs.NewRow());
                                                dt_qyzzzs.Rows[0]["zsjlId"] = dataService.Get_uepp_qyzs_NewID();
                                                dt_qyzzzs.Rows[0]["qyID"] = qyID;
                                                dt_qyzzzs.Rows[0]["csywlx"] = "工程设计";
                                                dt_qyzzzs.Rows[0]["csywlxID"] = "6";

                                                dt_qyzzzs.Rows[0]["zslxID"] = "60";
                                                dt_qyzzzs.Rows[0]["zslx"] = "部工程设计资质证";
                                                dt_qyzzzs.Rows[0]["sfzzz"] = "1";
                                            }
                                            dt_qyzzzs.Rows[0]["zsbh"] = zsbh;
                                            dt_qyzzzs.Rows[0]["fzrq"] = row_qyzz["XKJG_Date"];
                                            dt_qyzzzs.Rows[0]["zsyxqrq"] = row_qyzz["ZS_YXQDate"];
                                            dt_qyzzzs.Rows[0]["zsyxzrq"] = row_qyzz["ZS_YXQEndDate"];
                                            dt_qyzzzs.Rows[0]["fzdw"] = row_qyzz["XKJG_Name"];

                                            dt_qyzzzs.Rows[0]["DataState"] = 0;
                                            dt_qyzzzs.Rows[0]["tag"] = tag;
                                            dt_qyzzzs.Rows[0]["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                            dataService.Submit_uepp_qyzzzs(dt_qyzzzs);
                                        }

                                    }
                                    zslx = dt_qyzzzs.Rows[0]["zslx"].ToString2();
                                    zslxID = dt_qyzzzs.Rows[0]["zslxID"].ToString2();

                                }

                                #endregion

                                #region 企业资质明细
                                if (kcIndex >= 0)
                                {
                                    if (zyIndex >= 0)
                                    {

                                        zzxlId = "10";
                                        zzxl = "专业类";

                                        if (zzName.LastIndexOf("-") - zyIndex - 2 > 0)
                                            zzlb = zzName.Substring(zyIndex + 3, zzName.LastIndexOf("-") - zyIndex - 2).Trim(new char[] { ' ', '-' });
                                    }
                                    else
                                        if (lwIndex >= 0)
                                        {
                                            zzxlId = "11";
                                            zzxl = "劳务类";
                                            if (zzName.LastIndexOf("-") - lwIndex - 2 > 0)
                                                zzlb = zzName.Substring(lwIndex + 3, zzName.LastIndexOf("-") - lwIndex - 2).Trim(new char[] { ' ', '-' });
                                        }
                                        else if (zhIndex >= 0)
                                        {
                                            zzxlId = "9";
                                            zzxl = "综合类";
                                            if (zzName.LastIndexOf("-") - zhIndex - 2 > 0)
                                                zzlb = zzName.Substring(zhIndex + 3, zzName.LastIndexOf("-") - zhIndex - 2).Trim(new char[] { ' ', '-' });
                                        }

                                }
                                else
                                {
                                    if (zyIndex >= 0)
                                    {
                                        zzxlId = "14";
                                        zzxl = "专业资质";
                                        if (zzName.LastIndexOf("-") - zyIndex - 2 > 0)
                                            zzlb = zzName.Substring(zyIndex + 3, zzName.LastIndexOf("-") - zyIndex - 2).Trim(new char[] { ' ', '-' });
                                    }
                                    else
                                        if (zhIndex >= 0)
                                        {
                                            zzxlId = "12";
                                            zzxl = "综合资质";
                                            if (zzName.LastIndexOf("-") - zhIndex - 2 > 0)
                                                zzlb = zzName.Substring(zhIndex + 3, zzName.LastIndexOf("-") - zhIndex - 2).Trim(new char[] { ' ', '-' });
                                        }
                                        else if (hyIndex >= 0)
                                        {
                                            zzxlId = "13";
                                            zzxl = "行业资质";

                                            if (zzName.LastIndexOf("-") - hyIndex - 2 > 0)
                                                zzlb = zzName.Substring(hyIndex + 3, zzName.LastIndexOf("-") - hyIndex - 2).Trim(new char[] { ' ', '-' });
                                        }
                                        else if (zxIndex >= 0)
                                        {

                                            zzxlId = "15";
                                            zzxl = "专项资质";
                                            if (zzName.LastIndexOf("-") - zxIndex - 2 > 0)
                                                zzlb = zzName.Substring(zxIndex + 3, zzName.LastIndexOf("-") - zxIndex - 2).Trim(new char[] { ' ', '-' });
                                        }
                                        else if (swsIndex >= 0)
                                        {
                                            zzxlId = "16";
                                            zzxl = "事务所资质";
                                            if (zzName.LastIndexOf("-") - swsIndex - 3 > 0)
                                                zzlb = zzName.Substring(swsIndex + 4, zzName.LastIndexOf("-") - swsIndex - 3).Trim(new char[] { ' ', '-' });
                                        }
                                }

                                if (!string.IsNullOrEmpty(zzlb))
                                {
                                    string sql = "select * from UEPP_Code where CodeType='企业资质类别' and parentCodeType='企业资质序列' and ParentCode=" + zzxlId + " and CodeInfo='" + zzlb + "'";
                                    DataTable dt_uepp_code = this.dataService.DB.ExeSqlForDataTable(sql, null, "dt");
                                    if (dt_uepp_code.Rows.Count == 0)
                                    {
                                        dt_uepp_code.Rows.Add(dt_uepp_code.NewRow());
                                        dt_uepp_code.Rows[0]["ParentCodeType"] = "企业资质序列";
                                        dt_uepp_code.Rows[0]["ParentCode"] = zzxlId;
                                        dt_uepp_code.Rows[0]["CodeType"] = "企业资质类别";
                                        dt_uepp_code.Rows[0]["Code"] = dataService.Get_uepp_Code_NewCode("企业资质类别");
                                        dt_uepp_code.Rows[0]["CodeInfo"] = zzlb;
                                        dt_uepp_code.Rows[0]["CodeDesc"] = "";
                                        dt_uepp_code.Rows[0]["OrderID"] = dt_uepp_code.Rows[0]["Code"];
                                        dataService.Submit_uepp_code(dt_uepp_code);
                                    }

                                    zzlbId = dt_uepp_code.Rows[0]["Code"].ToString2();
                                }

                                for (int i = 0; i < dt_uepp_qyzzmx_kcsj.Rows.Count; i++)
                                {
                                    if (kcIndex >= 0)
                                    {
                                        if (zsjb.Equals("省级"))
                                        {
                                            if (dt_uepp_qyzzmx_kcsj.Rows[i]["zzlb"].ToString2().Equals(zzlb) && dt_uepp_qyzzmx_kcsj.Rows[i]["csywlxID"].ToString2().Equals("5") && dt_uepp_qyzzmx_kcsj.Rows[i]["zslxID"].ToString2().Equals("51"))
                                            {
                                                isExists = i;
                                                break;
                                            }

                                        }
                                        else
                                        {
                                            if (dt_uepp_qyzzmx_kcsj.Rows[i]["zzlb"].ToString2().Equals(zzlb) && dt_uepp_qyzzmx_kcsj.Rows[i]["csywlxID"].ToString2().Equals("5") && dt_uepp_qyzzmx_kcsj.Rows[i]["zslxID"].ToString2().Equals("50"))
                                            {
                                                isExists = i;
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (zsjb.Equals("省级"))
                                        {
                                            if (dt_uepp_qyzzmx_kcsj.Rows[i]["zzlb"].ToString2().Equals(zzlb) && dt_uepp_qyzzmx_kcsj.Rows[i]["csywlxID"].ToString2().Equals("6") && dt_uepp_qyzzmx_kcsj.Rows[i]["zslxID"].ToString2().Equals("61"))
                                            {
                                                isExists = i;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if (dt_uepp_qyzzmx_kcsj.Rows[i]["zzlb"].ToString2().Equals(zzlb) && dt_uepp_qyzzmx_kcsj.Rows[i]["csywlxID"].ToString2().Equals("6") && dt_uepp_qyzzmx_kcsj.Rows[i]["zslxID"].ToString2().Equals("60"))
                                            {
                                                isExists = i;
                                                break;
                                            }
                                        }
                                    }
                                }
                                DataRow row_qyzzmx;
                                if (isExists >= 0)
                                {
                                    row_qyzzmx = dt_uepp_qyzzmx_kcsj.Rows[isExists];
                                    if (row_qyzzmx["tag"].ToString2().IndexOf(tag) < 0)
                                    {
                                        continue;
                                    }
                                    //if (row_qyzzmx["xgrqsj"].ToString2() == row_qyzz["XKJG_Date"].ToString2())
                                    //{
                                    //    continue;
                                    //}
                                }
                                else
                                {
                                    row_qyzzmx = dt_uepp_qyzzmx_kcsj.NewRow();
                                    dt_uepp_qyzzmx_kcsj.Rows.Add(row_qyzzmx);
                                    row_qyzzmx["ID"] = dataService.Get_uepp_qyxxmx_NewID();
                                }

                                row_qyzzmx["qyID"] = qyID;
                                if (kcIndex >= 0)
                                {
                                    row_qyzzmx["csywlx"] = "工程勘察";
                                    row_qyzzmx["csywlxID"] = "5";
                                    if (zsjb.Equals("省级"))
                                    {
                                        row_qyzzmx["zslx"] = "省工程勘察资质证";
                                        row_qyzzmx["zslxID"] = "51";
                                    }
                                    else
                                    {
                                        row_qyzzmx["zslx"] = "部工程勘察资质证";
                                        row_qyzzmx["zslxID"] = "50";
                                    }
                                }
                                else
                                {
                                    row_qyzzmx["csywlx"] = "工程设计";
                                    row_qyzzmx["csywlxID"] = "6";
                                    if (zsjb.Equals("省级"))
                                    {
                                        row_qyzzmx["zslx"] = "工程设计资质证";
                                        row_qyzzmx["zslxID"] = "61";
                                    }
                                    else
                                    {
                                        row_qyzzmx["zslx"] = "部工程设计资质证";
                                        row_qyzzmx["zslxID"] = "60";
                                    }
                                }
                                Public.WriteLog("市勘察设计系统zzbz:" + row_qyzz["ZXZZ"].ToString2());
                                if (row_qyzz["ZXZZ"].ToString2() == "是" || row_qyzz["ZXZZ"].ToString2() == "1")
                                {
                                    row_qyzzmx["zzbz"] = "主项";
                                }
                                else
                                {
                                    row_qyzzmx["zzbz"] = "增项";
                                }
                                row_qyzzmx["zzxl"] = zzxl;
                                row_qyzzmx["zzxlID"] = zzxlId;
                                if (!string.IsNullOrEmpty(zzlb))
                                {
                                    row_qyzzmx["zzlb"] = zzlb;
                                    row_qyzzmx["zzlbID"] = zzlbId;
                                }
                                if (!string.IsNullOrEmpty(row_qyzz["ZZZSDJ"].ToString2()))
                                {
                                    row_qyzzmx["zzdj"] = row_qyzz["ZZZSDJ"].ToString2();

                                    DataTable dt_uepp_code_zzdj = dataService.DB.ExeSqlForDataTable("select * from UEPP_Code where parentCodeType='企业资质序列' and parentCode='" + zzxlId + "' and CodeType='企业资质等级' and CodeInfo='" + row_qyzz["ZZZSDJ"].ToString2() + "'", null, "dt");

                                    if (dt_uepp_code_zzdj.Rows.Count == 0)
                                    {
                                        dt_uepp_code_zzdj.Rows.Add(dt_uepp_code_zzdj.NewRow());
                                        dt_uepp_code_zzdj.Rows[0]["ParentCodeType"] = "企业资质序列";
                                        dt_uepp_code_zzdj.Rows[0]["ParentCode"] = zzxlId;
                                        dt_uepp_code_zzdj.Rows[0]["CodeType"] = "企业资质等级";
                                        dt_uepp_code_zzdj.Rows[0]["Code"] = dataService.Get_uepp_Code_NewCode("企业资质等级");
                                        dt_uepp_code_zzdj.Rows[0]["CodeInfo"] = row_qyzz["ZZZSDJ"].ToString2();
                                        dt_uepp_code_zzdj.Rows[0]["CodeDesc"] = "";
                                        dt_uepp_code_zzdj.Rows[0]["OrderID"] = dt_uepp_code_zzdj.Rows[0]["Code"];
                                        dataService.Submit_uepp_code(dt_uepp_code_zzdj);
                                    }
                                    row_qyzzmx["zzdjID"] = dt_uepp_code_zzdj.Rows[0]["Code"];
                                }
                                row_qyzzmx["cjywfw"] = row_qyzz["ZZName"];

                                row_qyzzmx["xgrqsj"] = string.IsNullOrEmpty(row_qyzz["XKJG_Date"].ToString2()) ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : row_qyzz["XKJG_Date"].ToString2();
                                row_qyzzmx["tag"] = tag;
                                row_qyzzmx["xgr"] = "";
                                row_qyzzmx["DataState"] = 0;

                                #endregion

                            }
                            if (dt_uepp_qyzzmx_kcsj.Rows.Count > 0)
                                dataService.Submit_uepp_qyzzmx(dt_uepp_qyzzmx_kcsj);
                        }
                        catch (Exception ex)
                        {
                            Public.WriteLog("获取勘察设计企业资质时出现异常:" + ex.Message);
                        }

                        #endregion
                    }

                    if (dt_SaveDataLog.Rows.Count > 0)
                        dataService.Submit_SaveDataLog(dt_SaveDataLog);

                    row_DataJkDataDetail_qy["allCount"] = allCount_qy;
                    row_DataJkDataDetail_qy["successCount"] = success_count_qy;
                    row_DataJkDataDetail_qy["IsOk"] = 1;
                    row_DataJkDataDetail_qy["ErrorMsg"] = "";

                    if (dt_DataJkDataDetail_qy.Rows.Count > 0)
                        dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_qy);

                }
                catch (Exception ex_jsdw)
                {
                    //Public.WriteLog("获取勘察设计企业时出现异常:" + ex_jsdw.Message);

                    row_DataJkDataDetail_qy["allCount"] = allCount_qy;
                    row_DataJkDataDetail_qy["successCount"] = success_count_qy;
                    row_DataJkDataDetail_qy["IsOk"] = 0;
                    row_DataJkDataDetail_qy["ErrorMsg"] = ex_jsdw.Message;

                    dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_qy);
                    return;
                }

                #endregion

                #region 勘察设计单位人员信息
                tag = Tag.无锡市勘察设计行业信息管理系统.ToString();
                try
                {
                    //往数据监控日志表项添加一条记录
                    DataTable dt_DataJkDataDetail = dataService.GetSchema_DataJkDataDetail();
                    long Id_DataJkDataDetail = dataService.Get_DataJkDataDetailNewID().ToInt64();

                    DataTable dt_kcsjry = new DataTable();
                    int allCount = 0, success_count = 0;
                    DataRow row_DataJkDataDetail = dt_DataJkDataDetail.NewRow();
                    dt_DataJkDataDetail.Rows.Add(row_DataJkDataDetail);

                    row_DataJkDataDetail["ID"] = Id_DataJkDataDetail++;
                    row_DataJkDataDetail["DataJkLogID"] = row_DataJkLog["ID"];
                    row_DataJkDataDetail["tableName"] = "UEPP_Ryjbxx";
                    row_DataJkDataDetail["MethodName"] = "Get_RY_JBXX_Tab";

                    try
                    {
                        dt_kcsjry = dataService.Get_RY_JBXX_Tab();

                        DataTable dt_SaveDataLog = dataService.GetSchema_SaveDataLog();

                        foreach (DataRow row in dt_kcsjry.Rows)
                        {
                            string ryID = row["ZJNO"].ToString2();

                            DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                            row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail["ID"];
                            row_SaveDataLog["DataXml"] = "";
                            row_SaveDataLog["PKID"] = ryID;
                            row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                            try
                            {
                                DataTable dt_uepp_ryxx = dataService.Get_uepp_Ryjbxx(ryID);

                                DataRow temp;
                                if (dt_uepp_ryxx.Rows.Count == 0)
                                {
                                    temp = dt_uepp_ryxx.NewRow();
                                    dt_uepp_ryxx.Rows.Add(temp);
                                    temp["ryID"] = ryID;
                                    temp["tag"] = tag;
                                }
                                else
                                {
                                    temp = dt_uepp_ryxx.Rows[0];

                                    if (temp["tag"].ToString2().IndexOf(tag) < 0)
                                    {
                                        continue;
                                    }

                                    if (dt_uepp_ryxx.Rows.Count > 0 && dt_uepp_ryxx.Rows[0]["xgrqsj"].ToString2() == row["shtgsj"].ToString2())
                                    {
                                        continue;
                                    }
                                }
                                allCount++;

                                temp["xm"] = row["UserName"];
                                temp["zjlxID"] = "1";
                                temp["zjlx"] = "身份证";
                                temp["zjhm"] = row["ZJNO"];
                                temp["xb"] = row["Sex"];
                                temp["csrq"] = row["CSNY"];
                                temp["mz"] = row["Nation"];
                                temp["xl"] = row["XL"];
                                string xlID = dataService.DB.ExeSqlForString("select Code from uepp_code where codetype = '人员学历' and CodeInfo='" + row["XL"].ToString2() + "'", null);
                                if (string.IsNullOrEmpty(xlID))
                                    temp["xlID"] = DBNull.Value;
                                else
                                    temp["xlID"] = xlID;

                                temp["sxzy"] = row["SXZY"];
                                temp["byyx"] = row["BYYX"];
                                temp["zc"] = row["ZCMC"];
                                string zcID = dataService.DB.ExeSqlForString("select Code from uepp_code where  codetype='人员职称' and CodeInfo='" + row["ZCMC"].ToString2() + "'", null);
                                if (string.IsNullOrEmpty(zcID))
                                    temp["zcID"] = DBNull.Value;
                                else
                                    temp["zcID"] = zcID;

                                string zwid = dataService.DB.ExeSqlForString("select Code from uepp_code where codetype = '人员职务' and CodeInfo='" + row["RYZW"].ToString2() + "'", null);
                                if (string.IsNullOrEmpty(zwid))
                                    temp["zwID"] = DBNull.Value;
                                else
                                    temp["zwID"] = zwid;

                                temp["zw"] = row["RYZW"];
                                temp["lxdh"] = row["LXDH"];
                                temp["yddh"] = row["HandlePhone"];
                                temp["csgzjsnx"] = row["GZNX"];

                                temp["xgrqsj"] = string.IsNullOrEmpty(row["shtgsj"].ToString2()) ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : row["shtgsj"].ToString2();//DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                temp["tag"] = tag;
                                temp["xgr"] = "";

                                temp["DataState"] = 0;

                                DataTable dt_saveToStLog2 = dataService.Get_SaveToStLog2("UEPP_Ryjbxx", ryID);
                                if (dt_saveToStLog2.Rows.Count == 0)
                                {
                                    dt_saveToStLog2.Rows.Add(dt_saveToStLog2.NewRow());
                                    dt_saveToStLog2.Rows[0]["PKID"] = ryID;
                                    dt_saveToStLog2.Rows[0]["TableName"] = "UEPP_Ryjbxx";
                                    dt_saveToStLog2.Rows[0]["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                }
                                dt_saveToStLog2.Rows[0]["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                if (dataService.Submit_uepp_ryjbxx(dt_uepp_ryxx))
                                {
                                    dt_saveToStLog2.Rows[0]["SbToZxState"] = "0";
                                    dt_saveToStLog2.Rows[0]["SbToZxMsg"] = "获取成功";

                                    success_count++;

                                    row_SaveDataLog["SaveState"] = 1;
                                    row_SaveDataLog["Msg"] = "勘察设计企业人员信息保存成功！";
                                    string qyID = row["ZZJGDM"].ToString2();
                                    if (!string.IsNullOrEmpty(qyID))
                                    {
                                        if (qyID.Length == 9)
                                        {
                                            qyID = qyID.Substring(0, 8) + "-" + qyID.Substring(8, 1);
                                        }
                                        DataTable dt_uepp_kcsjqy = dataService.Get_uepp_QyryNoRyzyzg(ryID, qyID);
                                        if (dt_uepp_kcsjqy.Rows.Count == 0)
                                        {
                                            dt_uepp_kcsjqy.Rows.Add(dt_uepp_kcsjqy.NewRow());
                                            dt_uepp_kcsjqy.Rows[0]["qyID"] = qyID;
                                            dt_uepp_kcsjqy.Rows[0]["ryID"] = ryID;
                                            dataService.Submit_uepp_qyry(dt_uepp_kcsjqy);
                                        }
                                    }
                                }
                                else
                                {
                                    dt_saveToStLog2.Rows[0]["SbToZxState"] = "1";
                                    dt_saveToStLog2.Rows[0]["SbToZxMsg"] = "获取失败";

                                    row_SaveDataLog["SaveState"] = 0;
                                    row_SaveDataLog["Msg"] = "勘察设计企业人员信息保存失败！";
                                }

                                dataService.Submit_SaveToStLog2(dt_saveToStLog2);
                            }
                            catch (Exception ex)
                            {
                                row_SaveDataLog["SaveState"] = 0;
                                row_SaveDataLog["Msg"] = "获取勘察设计企业人员时出现异常:ryID:" + row["RowGuid"].ToString2() + ",Msg:" + ex.Message;
                            }

                            dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                        }

                        if (dt_SaveDataLog.Rows.Count > 0)
                            dataService.Submit_SaveDataLog(dt_SaveDataLog);

                        row_DataJkDataDetail["allCount"] = allCount;
                        row_DataJkDataDetail["successCount"] = success_count;
                        row_DataJkDataDetail["IsOk"] = 1;
                        row_DataJkDataDetail["ErrorMsg"] = "";

                        if (dt_DataJkDataDetail.Rows.Count > 0)
                            dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail);
                    }
                    catch (Exception ex)
                    {
                        row_DataJkDataDetail["allCount"] = allCount;
                        row_DataJkDataDetail["successCount"] = success_count;
                        row_DataJkDataDetail["IsOk"] = 0;
                        row_DataJkDataDetail["ErrorMsg"] = ex.Message;
                        dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail);
                    }
                }
                catch (Exception ex_jsdw)
                {
                    Public.WriteLog("获取勘察设计企业人员时出现异常:" + ex_jsdw.Message);
                    apiMessage += ex_jsdw.Message;
                }

                #endregion

                DateTime endTime = DateTime.Now;
                TimeSpan span = compareDateTime(beginTime, endTime);
                Public.WriteLog(string.Format("结束YourTask_PullDataFromWxkcsj_qyry任务:{0}，历时：{1}时{2}分{3}秒", endTime.ToString("yyyy-MM-dd HH:mm:ss"), span.Hours, span.Minutes, span.Seconds));


                DataTable dtapicb = dataService.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = dataService.Get_apiCbNewID();
                row_apicb["apiFlow"] = "6";
                row_apicb["apiMethod"] = "Get_Enterprise_Tab;Get_RY_JBXX_Tab";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dataService.Submit_API_cb(dtapicb);

                dataService.UpdateZbJkzt("6", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);


            }
        }

        /// <summary>
        /// 从安监站前置机获取企业和人员数据
        /// </summary>
        void YourTask_PullDataFromAqjdz_qyryxx()
        {
            string apiMessage = string.Empty;
            //string methodMessage = string.Empty;
            DataService dataService = new DataService();
            DataTable dtapizb = dataService.Get_API_zb_apiFlow("10");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                try
                {
                    string tag = Tag.无锡市建设工程安全监督站.ToString();

                    //往数据监控日志表添加一条记录
                    DataTable dt_DataJkLog = dataService.GetSchema_DataJkLog();
                    DataRow row_DataJkLog = dt_DataJkLog.NewRow();
                    dt_DataJkLog.Rows.Add(row_DataJkLog);
                    row_DataJkLog["ID"] = dataService.Get_DataJkLogNewID();
                    row_DataJkLog["DataFlow"] = DataFlow.市安监系统到无锡数据中心.ToInt32();
                    row_DataJkLog["DataFlowName"] = DataFlow.市安监系统到无锡数据中心.ToString();
                    row_DataJkLog["ServiceUrl"] = "";
                    row_DataJkLog["csTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dataService.Submit_DataJkLog(dt_DataJkLog);

                    #region 获取安监站前置机里的施工单位信息

                    DataTable dt_ajz_sgqy = new DataTable();

                    //往数据监控日志表项添加一条记录
                    DataTable dt_DataJkDataDetail_sgdw = dataService.GetSchema_DataJkDataDetail();
                    long Id_DataJkDataDetail_sgdw = dataService.Get_DataJkDataDetailNewID().ToInt64();

                    int allCount_sgdw = 0, success_count_sgdw = 0;
                    DataRow row_DataJkDataDetail_sgdw = dt_DataJkDataDetail_sgdw.NewRow();
                    dt_DataJkDataDetail_sgdw.Rows.Add(row_DataJkDataDetail_sgdw);

                    row_DataJkDataDetail_sgdw["ID"] = Id_DataJkDataDetail_sgdw++;
                    row_DataJkDataDetail_sgdw["DataJkLogID"] = row_DataJkLog["ID"];
                    row_DataJkDataDetail_sgdw["tableName"] = "UEPP_Qyjbxx";
                    row_DataJkDataDetail_sgdw["MethodName"] = "Get_Contractors";

                    try
                    {
                        dt_ajz_sgqy = dataService.Get_Contractors();

                        DataTable dt_SaveDataLog = dataService.GetSchema_SaveDataLog();

                        foreach (DataRow row in dt_ajz_sgqy.Rows)
                        {
                            string zzjgdm = row["FullNOC"].ToString2();

                            DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                            row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_sgdw["ID"];
                            row_SaveDataLog["DataXml"] = "";
                            row_SaveDataLog["PKID"] = zzjgdm;
                            row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                            {
                                DataTable dt_saveToStLog2 = dataService.Get_SaveToStLog2("UEPP_Qyjbxx", zzjgdm);
                                if (dt_saveToStLog2.Rows.Count == 0)
                                {
                                    dt_saveToStLog2.Rows.Add(dt_saveToStLog2.NewRow());
                                    dt_saveToStLog2.Rows[0]["PKID"] = zzjgdm;
                                    dt_saveToStLog2.Rows[0]["TableName"] = "UEPP_Qyjbxx";
                                    dt_saveToStLog2.Rows[0]["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                }
                                dt_saveToStLog2.Rows[0]["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                if (zzjgdm.Length == 9)
                                    zzjgdm = Public.ZzjgdmToStandard(zzjgdm);

                                try
                                {
                                    DataTable dt_uepp_sgqy = dataService.Get_uepp_Qyjbxx(zzjgdm);
                                    DataRow tempRow;
                                    if (dt_uepp_sgqy.Rows.Count == 0)
                                    {
                                        tempRow = dt_uepp_sgqy.NewRow();
                                        dt_uepp_sgqy.Rows.Add(tempRow);

                                        tempRow["qyID"] = zzjgdm;
                                        tempRow["tag"] = tag;
                                    }
                                    else
                                    {
                                        zzjgdm = dt_uepp_sgqy.Rows[0]["zzjgdm"].ToString2();

                                        tempRow = dt_uepp_sgqy.Rows[0];
                                        if (tempRow["tag"].ToString2().IndexOf(tag) < 0)
                                        {
                                            continue;
                                        }
                                        if (tempRow["xgrqsj"].ToString2().Equals(row["Modified"].ToString2(), StringComparison.CurrentCultureIgnoreCase))
                                            continue;

                                        tempRow["tag"] = tag;
                                    }

                                    tempRow["qymc"] = row["FullName"];
                                    tempRow["zzjgdm"] = zzjgdm;
                                    tempRow["yyzzzch"] = row["BizLicense"];
                                    string sfbdqy = row["InsideWX"].ToString2();
                                    if (string.IsNullOrEmpty(sfbdqy))
                                        tempRow["sfsyq"] = DBNull.Value;
                                    else
                                        if (sfbdqy.Equals("1"))
                                            tempRow["sfsyq"] = "0";
                                        else
                                            tempRow["sfsyq"] = "1";
                                    tempRow["gcjsry_zs"] = row["EngineerTotal"];
                                    tempRow["gcjsry_gjzcrs"] = row["EmployeeTitledH"];
                                    tempRow["gcjsry_zjzcrs"] = row["EmployeeTitledM"];


                                    #region 企业从事业务类型
                                    DataTable dt_qycsyw = dataService.Get_uepp_Qycsyw_sgqy(zzjgdm);
                                    DataRow tempRow_qycsyw;
                                    if (dt_qycsyw.Rows.Count == 0)
                                    {
                                        tempRow_qycsyw = dt_qycsyw.NewRow();
                                        dt_qycsyw.Rows.Add(tempRow_qycsyw);
                                        tempRow_qycsyw["qyID"] = zzjgdm;

                                        tempRow_qycsyw["csywlxID"] = "1";
                                        tempRow_qycsyw["csywlx"] = "建筑施工";
                                        tempRow_qycsyw["balxID"] = "1";
                                        tempRow_qycsyw["balx"] = "长期备案";
                                        tempRow_qycsyw["DataState"] = "0";
                                        tempRow_qycsyw["tag"] = tag;
                                        tempRow_qycsyw["xgrqsj"] = row["Modified"].ToString2();

                                        if (dt_qycsyw.Rows.Count > 0)
                                        {
                                            dataService.Submit_uepp_qycsyw(dt_qycsyw);
                                        }
                                    }
                                    else
                                    {
                                        tempRow_qycsyw = dt_qycsyw.Rows[0];
                                    }

                                    #endregion

                                    string zzzslx = row["QualificationCategory"].ToString2();
                                    string zzzs = row["QualificationNo"].ToString2();

                                    #region 企业证书
                                    if (!string.IsNullOrEmpty(zzzs))
                                    {
                                        DataTable dt_qyzzzs = dataService.Get_UEPP_Qyzzzs(zzjgdm, "1");
                                        if (dt_qyzzzs.Rows.Count == 0)
                                        {
                                            dt_qyzzzs.Rows.Add(dt_qyzzzs.NewRow());
                                            dt_qyzzzs.Rows[0]["zsjlId"] = dataService.Get_uepp_qyzs_NewID();
                                            dt_qyzzzs.Rows[0]["qyID"] = zzjgdm;
                                            dt_qyzzzs.Rows[0]["csywlx"] = tempRow_qycsyw["csywlx"];
                                            dt_qyzzzs.Rows[0]["csywlxID"] = tempRow_qycsyw["csywlxID"];

                                            dt_qyzzzs.Rows[0]["zslxID"] = "1";
                                            dt_qyzzzs.Rows[0]["zslx"] = "建筑业资质证";
                                            dt_qyzzzs.Rows[0]["sfzzz"] = "1";
                                        }
                                        dt_qyzzzs.Rows[0]["zsbh"] = zzzs;
                                        dt_qyzzzs.Rows[0]["fzrq"] = row["Qualified"];

                                        dt_qyzzzs.Rows[0]["DataState"] = 0;
                                        dt_qyzzzs.Rows[0]["tag"] = tag;
                                        dt_qyzzzs.Rows[0]["xgrqsj"] = row["Modified"].ToString2();

                                        dataService.Submit_uepp_qyzzzs(dt_qyzzzs);

                                    }


                                    string aqscxkh = row["SafetyPermit"].ToString2();

                                    if (!string.IsNullOrEmpty(aqscxkh))
                                    {
                                        DataTable dt_qyzzzs = dataService.Get_UEPP_Qyzzzs(zzjgdm, "11");
                                        if (dt_qyzzzs.Rows.Count == 0)
                                        {
                                            dt_qyzzzs.Rows.Add(dt_qyzzzs.NewRow());
                                            dt_qyzzzs.Rows[0]["zsjlId"] = dataService.Get_uepp_qyzs_NewID();
                                            dt_qyzzzs.Rows[0]["qyID"] = zzjgdm;
                                            dt_qyzzzs.Rows[0]["csywlx"] = "建筑施工";
                                            dt_qyzzzs.Rows[0]["csywlxID"] = "1";

                                            dt_qyzzzs.Rows[0]["zslxID"] = "11";
                                            dt_qyzzzs.Rows[0]["zslx"] = "安全生产许可证";
                                            dt_qyzzzs.Rows[0]["sfzzz"] = "1";
                                        }
                                        dt_qyzzzs.Rows[0]["zsbh"] = aqscxkh;
                                        dt_qyzzzs.Rows[0]["zsyxqrq"] = row["SafetyPermitValidFrom"];
                                        dt_qyzzzs.Rows[0]["zsyxzrq"] = row["SafetyPermitValidTo"];

                                        dt_qyzzzs.Rows[0]["DataState"] = 0;
                                        dt_qyzzzs.Rows[0]["tag"] = tag;
                                        dt_qyzzzs.Rows[0]["xgrqsj"] = row["Modified"].ToString2();

                                        dataService.Submit_uepp_qyzzzs(dt_qyzzzs);

                                    }
                                    #endregion

                                    #region 企业资质信息

                                    string zhuxzz = row["PrimaryQualificationName"].ToString2();
                                    string zxzz = row["MoreQualificationNames"].ToString2().Trim();

                                    //主项资质
                                    if (!string.IsNullOrEmpty(zhuxzz))
                                    {
                                        string zzxl = "", zzxlId = "", zzlb = "", zzdj = "";
                                        int zzxlIndex1 = zhuxzz.IndexOf("总承包");
                                        int zzxlIndex2 = zhuxzz.IndexOf("专业承包");
                                        int zzxlIndex3 = zhuxzz.IndexOf("劳务分包");
                                        int zzxlIndex4 = zhuxzz.IndexOf("设计与施工一体化");
                                        int zzdjIndex = zhuxzz.IndexOf("级");

                                        if (zzxlIndex1 >= 0)
                                        {
                                            zzxl = "施工总承包";
                                            zzxlId = "1";

                                            zzlb = zhuxzz.Substring(0, zzxlIndex1);

                                        }
                                        else
                                            if (zzxlIndex2 >= 0)
                                            {
                                                zzxl = "专业承包";
                                                zzxlId = "2";
                                                zzlb = zhuxzz.Substring(0, zzxlIndex2);
                                            }
                                            else if (zzxlIndex3 >= 0)
                                            {
                                                zzxl = "劳务分包";
                                                zzxlId = "3";
                                                zzlb = zhuxzz.Substring(0, zzxlIndex3);
                                            }
                                            else if (zzxlIndex4 >= 0)
                                            {
                                                zzxl = "设计与施工一体化";
                                                zzxlId = "4";
                                                zzlb = zhuxzz.Substring(0, zzxlIndex4);
                                            }
                                        zzlb = zzlb.Trim(new char[] { ' ', '\r', '\n' });
                                        if (zzdjIndex >= 0)
                                        {
                                            zzdj = zhuxzz.Substring(zzdjIndex - 1, 2);
                                        }

                                        if (!string.IsNullOrEmpty(zzlb))
                                        {
                                            DataTable dt_qyzzlb = dataService.Get_uepp_Code("企业资质类别", zzxlId, zzlb);
                                            if (dt_qyzzlb.Rows.Count == 0)
                                            {
                                                dt_qyzzlb.Rows.Add(dt_qyzzlb.NewRow());
                                                dt_qyzzlb.Rows[0]["CodeInfo"] = zzlb;
                                                dt_qyzzlb.Rows[0]["ParentCodeType"] = "企业资质序列";
                                                dt_qyzzlb.Rows[0]["ParentCode"] = zzxlId;
                                                dt_qyzzlb.Rows[0]["CodeType"] = "企业资质类别";
                                                dt_qyzzlb.Rows[0]["CodeDesc"] = "";
                                                dt_qyzzlb.Rows[0]["Code"] = dataService.Get_uepp_Code_NewCode("企业资质类别");
                                                dt_qyzzlb.Rows[0]["OrderID"] = dt_qyzzlb.Rows[0]["Code"];
                                                dataService.Submit_uepp_code(dt_qyzzlb);

                                            }
                                            object zzdjId = DBNull.Value;
                                            if (!string.IsNullOrEmpty(zzdj))
                                            {
                                                DataTable dt_qyzzdj = dataService.Get_uepp_Code_zzdj("企业资质等级", zzxlId, zzdj);
                                                if (dt_qyzzdj.Rows.Count == 0)
                                                {
                                                    zzdjId = dataService.Get_uepp_Code_NewCode("企业资质等级");
                                                    dt_qyzzdj.Rows.Add(dt_qyzzdj.NewRow());
                                                    dt_qyzzdj.Rows[0]["CodeInfo"] = zzdj;
                                                    dt_qyzzdj.Rows[0]["ParentCodeType"] = "企业资质序列";
                                                    dt_qyzzdj.Rows[0]["ParentCode"] = zzxlId;
                                                    dt_qyzzdj.Rows[0]["CodeType"] = "企业资质等级";
                                                    dt_qyzzdj.Rows[0]["CodeDesc"] = "";
                                                    dt_qyzzdj.Rows[0]["Code"] = zzdjId;
                                                    dt_qyzzdj.Rows[0]["OrderID"] = dt_qyzzdj.Rows[0]["Code"];
                                                    dataService.Submit_uepp_code(dt_qyzzdj);
                                                }
                                                else
                                                {
                                                    zzdjId = dt_qyzzdj.Rows[0]["Code"].ToString2();
                                                }
                                            }

                                            DataTable dt_qyzzxx_zhux = dataService.Get_UEPP_Qyzzmx_zhux(zzjgdm);
                                            if (dt_qyzzxx_zhux.Rows.Count == 0)
                                            {
                                                dt_qyzzxx_zhux.Rows.Add(dt_qyzzxx_zhux.NewRow());
                                                dt_qyzzxx_zhux.Rows[0]["ID"] = dataService.Get_uepp_qyxxmx_NewID();
                                                dt_qyzzxx_zhux.Rows[0]["qyID"] = zzjgdm;
                                                dt_qyzzxx_zhux.Rows[0]["csywlx"] = "建筑施工";
                                                dt_qyzzxx_zhux.Rows[0]["csywlxID"] = "1";
                                                dt_qyzzxx_zhux.Rows[0]["zzbz"] = "主项";

                                            }

                                            dt_qyzzxx_zhux.Rows[0]["zzxl"] = zzxl;
                                            dt_qyzzxx_zhux.Rows[0]["zzxlID"] = zzxlId;

                                            dt_qyzzxx_zhux.Rows[0]["zslxID"] = "1";
                                            dt_qyzzxx_zhux.Rows[0]["zslx"] = "建筑业资质证";

                                            dt_qyzzxx_zhux.Rows[0]["zzlb"] = dt_qyzzlb.Rows[0]["CodeInfo"];
                                            dt_qyzzxx_zhux.Rows[0]["zzlbID"] = dt_qyzzlb.Rows[0]["Code"];
                                            dt_qyzzxx_zhux.Rows[0]["zzdj"] = zzdj;
                                            dt_qyzzxx_zhux.Rows[0]["zzdjID"] = zzdjId;

                                            dt_qyzzxx_zhux.Rows[0]["DataState"] = 0;
                                            dt_qyzzxx_zhux.Rows[0]["tag"] = tag;
                                            dt_qyzzxx_zhux.Rows[0]["xgrqsj"] = string.IsNullOrEmpty(row["Modified"].ToString2()) ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : row["Modified"].ToString2();

                                            dataService.Submit_uepp_qyzzmx(dt_qyzzxx_zhux);

                                        }
                                    }

                                    List<Zzxx> list = new List<Zzxx>();

                                    //增项资质

                                    string[] zxzzArr = zxzz.Split(new char[] { ' ', '\r', '\n' });

                                    foreach (string item in zxzzArr)
                                    {
                                        string item_zxzz = item.Trim(new char[] { ' ', '\r', '\n' });
                                        string zxzzlb = "", zxzzlx = "", zxzzdj = "", zxzzlxId = "";
                                        if (string.IsNullOrEmpty(item_zxzz))
                                        {
                                            continue;
                                        }
                                        int ji_Index = item_zxzz.IndexOf('级');
                                        int zcb_Index = item_zxzz.IndexOf("总承包");
                                        int zycb_Index = item_zxzz.IndexOf("专业承包");
                                        int lwfb_Index = item_zxzz.IndexOf("劳务分包");
                                        int sjysgyth_Index = item_zxzz.IndexOf("设计与施工一体化");
                                        int min_index = -1;

                                        if (zcb_Index >= 0)
                                        {

                                            if (min_index == -1)
                                                min_index = zcb_Index;
                                            else
                                                min_index = Math.Min(min_index, zcb_Index);

                                        }

                                        if (zycb_Index >= 0)
                                        {

                                            if (min_index == -1)
                                                min_index = zycb_Index;
                                            else
                                                min_index = Math.Min(min_index, zycb_Index);

                                        }

                                        if (lwfb_Index >= 0)
                                        {
                                            if (min_index == -1)
                                                min_index = lwfb_Index;
                                            else
                                                min_index = Math.Min(min_index, lwfb_Index);

                                        }

                                        if (sjysgyth_Index >= 0)
                                        {
                                            if (min_index == -1)
                                                min_index = sjysgyth_Index;
                                            else
                                                min_index = Math.Min(min_index, sjysgyth_Index);

                                        }

                                        if (min_index == zcb_Index)
                                        {
                                            zxzzlx = "施工总承包";
                                            zxzzlxId = "1";
                                            zxzzlb = item_zxzz.Substring(0, zcb_Index);

                                            if (ji_Index - zcb_Index == 4)
                                                zxzzdj = item_zxzz.Substring(ji_Index - 1, 2);
                                        }
                                        else
                                            if (min_index == zycb_Index)
                                            {
                                                zxzzlx = "专业承包";
                                                zxzzlxId = "2";
                                                zxzzlb = item_zxzz.Substring(0, zycb_Index);

                                                if (ji_Index - zycb_Index == 5)
                                                    zxzzdj = item_zxzz.Substring(ji_Index - 1, 2);
                                            }
                                            else
                                                if (min_index == lwfb_Index)
                                                {
                                                    zxzzlx = "劳务分包";
                                                    zxzzlxId = "3";
                                                    zxzzlb = item_zxzz.Substring(0, lwfb_Index);

                                                    if (ji_Index - lwfb_Index == 5)
                                                        zxzzdj = item_zxzz.Substring(ji_Index - 1, 2);
                                                }
                                                else
                                                    if (min_index == sjysgyth_Index)
                                                    {
                                                        zxzzlx = "设计与施工一体化";
                                                        zxzzlxId = "4";
                                                        zxzzlb = item_zxzz.Substring(0, sjysgyth_Index);

                                                        if (ji_Index - sjysgyth_Index == 9)
                                                            zxzzdj = item_zxzz.Substring(ji_Index - 1, 2);
                                                    }
                                                    else
                                                    {
                                                        continue;
                                                    }
                                        zxzzlb = zxzzlb.Trim(new char[] { ' ', '\r', '\n' });
                                        DataTable dt_qyzzlb = dataService.Get_uepp_Code("企业资质类别", zxzzlxId, zxzzlb);
                                        if (dt_qyzzlb.Rows.Count == 0)
                                        {
                                            dt_qyzzlb.Rows.Add(dt_qyzzlb.NewRow());
                                            dt_qyzzlb.Rows[0]["CodeInfo"] = zxzzlb;
                                            dt_qyzzlb.Rows[0]["ParentCodeType"] = "企业资质序列";
                                            dt_qyzzlb.Rows[0]["ParentCode"] = zxzzlxId;
                                            dt_qyzzlb.Rows[0]["CodeType"] = "企业资质类别";
                                            dt_qyzzlb.Rows[0]["CodeDesc"] = "";
                                            dt_qyzzlb.Rows[0]["Code"] = dataService.Get_uepp_Code_NewCode("企业资质类别");
                                            dt_qyzzlb.Rows[0]["OrderID"] = dt_qyzzlb.Rows[0]["Code"];
                                            dataService.Submit_uepp_code(dt_qyzzlb);

                                        }
                                        string zzdjId = "";
                                        zxzzdj = zxzzdj.Trim(new char[] { ' ', '\r', '\n' });
                                        if (!string.IsNullOrEmpty(zxzzdj))
                                        {
                                            DataTable dt_qyzzdj = dataService.Get_uepp_Code("企业资质等级", zxzzlxId, zxzzdj);
                                            if (dt_qyzzdj.Rows.Count == 0)
                                            {
                                                zzdjId = dataService.Get_uepp_Code_NewCode("企业资质等级");
                                                dt_qyzzdj.Rows.Add(dt_qyzzdj.NewRow());
                                                dt_qyzzdj.Rows[0]["CodeInfo"] = zxzzdj;
                                                dt_qyzzdj.Rows[0]["ParentCodeType"] = "企业资质序列";
                                                dt_qyzzdj.Rows[0]["ParentCode"] = zxzzlxId;
                                                dt_qyzzdj.Rows[0]["CodeType"] = "企业资质等级";
                                                dt_qyzzdj.Rows[0]["CodeDesc"] = "";
                                                dt_qyzzdj.Rows[0]["Code"] = zzdjId;
                                                dt_qyzzdj.Rows[0]["OrderID"] = dt_qyzzdj.Rows[0]["Code"];
                                                dataService.Submit_uepp_code(dt_qyzzdj);

                                            }
                                            else
                                            {
                                                zzdjId = dt_qyzzdj.Rows[0]["Code"].ToString2();
                                            }
                                        }

                                        list.Add(new Zzxx(dt_qyzzlb.Rows[0]["Code"].ToString2(), dt_qyzzlb.Rows[0]["CodeInfo"].ToString2(), zxzzdj, zzdjId, zxzzlx, zxzzlxId));
                                    }

                                    DataTable dt_qyzzxx_zx = dataService.Get_UEPP_Qyzzmx_zx(zzjgdm);

                                    for (int i = 0; i < dt_qyzzxx_zx.Rows.Count; i++)
                                    {
                                        int flag = 0;
                                        foreach (Zzxx zzxx in list)
                                        {
                                            if (zzxx.zzlbId.Equals(dt_qyzzxx_zx.Rows[i]["zzlbId"].ToString(), StringComparison.CurrentCultureIgnoreCase))
                                            {
                                                if (!string.IsNullOrEmpty(zzxx.zzdjId))
                                                {
                                                    dt_qyzzxx_zx.Rows[i]["zzdjId"] = zzxx.zzdjId;
                                                    dt_qyzzxx_zx.Rows[i]["zzdj"] = zzxx.zzdj;
                                                }
                                                flag = 1;
                                                break;
                                            }
                                        }
                                        if (flag == 0)
                                        {
                                            dt_qyzzxx_zx.Rows[i].Delete();
                                        }
                                    }
                                    //dt_qyzzxx_zx.AcceptChanges();

                                    foreach (Zzxx zzxx in list)
                                    {
                                        int flag = 0;
                                        for (int i = 0; i < dt_qyzzxx_zx.Rows.Count; i++)
                                        {
                                            if (dt_qyzzxx_zx.Rows[i].RowState == DataRowState.Deleted)
                                                continue;
                                            if (zzxx.zzlbId.Equals(dt_qyzzxx_zx.Rows[i]["zzlbId"].ToString(), StringComparison.CurrentCultureIgnoreCase))
                                            {
                                                flag = 1;
                                                break;
                                            }
                                        }
                                        if (flag == 0)
                                        {
                                            DataRow temp = dt_qyzzxx_zx.NewRow();
                                            dt_qyzzxx_zx.Rows.Add(temp);
                                            temp["ID"] = dataService.Get_uepp_qyxxmx_NewID();
                                            temp["qyID"] = zzjgdm;
                                            temp["csywlx"] = "建筑施工";
                                            temp["csywlxID"] = "1";
                                            temp["zzbz"] = "增项";
                                            temp["zslxID"] = "1";
                                            temp["zslx"] = "建筑业资质证";
                                            temp["zzlbId"] = zzxx.zzlbId;
                                            temp["zzlb"] = zzxx.zzlb;
                                            if (!string.IsNullOrEmpty(zzxx.zzdjId))
                                            {
                                                temp["zzdjId"] = zzxx.zzdjId;
                                                temp["zzdj"] = zzxx.zzdj;
                                            }

                                            temp["zzxlId"] = zzxx.zzxlId;
                                            temp["zzxl"] = zzxx.zzxl;
                                            temp["DataState"] = 0;
                                            temp["tag"] = tag;
                                            temp["xgrqsj"] = string.IsNullOrEmpty(row["Modified"].ToString2()) ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : row["Modified"].ToString2();
                                        }
                                    }

                                    if (dt_qyzzxx_zx.Rows.Count > 0)
                                    {
                                        dataService.Submit_uepp_qyzzmx(dt_qyzzxx_zx);
                                    }

                                    #endregion

                                    tempRow["zcdd"] = row["Address"];
                                    tempRow["xxdd"] = row["AddressMore"];

                                    if (row["RegisteredCapital"].ToString2().IndexOf("亿") >= 0)
                                    {
                                        string money = row["RegisteredCapital"].ToString2().Replace("万", "").Replace("元", "").Replace("无", "").Replace("亿", "").Replace("人民币", "").Trim(new char[] { ' ', '"' });
                                        decimal d;
                                        if (!string.IsNullOrEmpty(money) && decimal.TryParse(money, out d))
                                            tempRow["zczb"] = (d * 1000).ToString();
                                        else
                                            tempRow["zczb"] = "";
                                    }
                                    else
                                    {
                                        string money = row["RegisteredCapital"].ToString2().Replace("万", "").Replace("元", "").Replace("人民币", "").Replace("无", "").Trim(new char[] { ' ', '"' });
                                        decimal d;
                                        if (!string.IsNullOrEmpty(money) && decimal.TryParse(money, out d))
                                            tempRow["zczb"] = money;
                                        else
                                            tempRow["zczb"] = "0";
                                    }
                                    if (!string.IsNullOrEmpty(row["Founded"].ToString2()))
                                        tempRow["clrq"] = row["Founded"];
                                    tempRow["yzbm"] = row["PostCode"];
                                    tempRow["cz"] = row["Faxs"];
                                    tempRow["email"] = row["Emails"];
                                    tempRow["webAddress"] = row["Webs"];
                                    tempRow["lxr"] = DBNull.Value;
                                    tempRow["lxdh"] = row["Phones"];
                                    if (row["RegionName"] != DBNull.Value && !string.IsNullOrEmpty(row["RegionName"].ToString2()))
                                    {
                                        tempRow["County"] = row["RegionName"];
                                        DataTable dt_county = dataService.DB.ExeSqlForDataTable(@"select Code from UEPP_Code where CodeType='城市地区' and CodeInfo='" + tempRow["County"] + "'", null, "dt");
                                        if (dt_county.Rows.Count > 0)
                                        {
                                            tempRow["CountyID"] = dt_county.Rows[0]["Code"];
                                            DataTable dt_city = dataService.DB.ExeSqlForDataTable(@"select Code,CodeInfo from UEPP_Code where CodeType='城市地区' and ParentCode='" + tempRow["CountyID"] + "'", null, "dt");

                                            if (dt_city.Rows.Count > 0)
                                            {
                                                tempRow["CityID"] = dt_city.Rows[0]["Code"];
                                                tempRow["City"] = dt_city.Rows[0]["CodeInfo"];
                                                DataTable dt_province = dataService.DB.ExeSqlForDataTable(@"select Code,CodeInfo from UEPP_Code where CodeType='城市地区' and ParentCode='" + tempRow["CityID"] + "'", null, "dt");

                                                if (dt_province.Rows.Count > 0)
                                                {
                                                    tempRow["ProvinceID"] = dt_city.Rows[0]["Code"];
                                                    tempRow["Province"] = dt_city.Rows[0]["CodeInfo"];
                                                }
                                            }
                                        }
                                    }

                                    tempRow["DataState"] = "0";

                                    tempRow["xgrqsj"] = row["Modified"].ToString2();
                                    allCount_sgdw++;

                                    if (dataService.Submit_uepp_qyjbxx(dt_uepp_sgqy))
                                    {
                                        success_count_sgdw++;

                                        dt_saveToStLog2.Rows[0]["SbToZxState"] = 0;
                                        dt_saveToStLog2.Rows[0]["SbToZxMsg"] = "获取成功";
                                        if (dt_saveToStLog2.Rows[0]["SbToStState"].ToString2().Equals("0"))
                                        {
                                            dt_saveToStLog2.Rows[0]["SbToStState"] = 1;
                                            dt_saveToStLog2.Rows[0]["SbToStMsg"] = "未更新";
                                        }

                                        row_SaveDataLog["SaveState"] = 1;
                                        row_SaveDataLog["Msg"] = "";

                                    }
                                    else
                                    {
                                        dt_saveToStLog2.Rows[0]["SbToZxState"] = 0;
                                        dt_saveToStLog2.Rows[0]["SbToZxMsg"] = "获取失败";

                                        row_SaveDataLog["SaveState"] = 0;
                                        row_SaveDataLog["Msg"] = "施工企业保存失败！";

                                    }

                                    dataService.Submit_SaveToStLog2(dt_saveToStLog2);

                                    scope.Complete();

                                }
                                catch (Exception ex)
                                {
                                    try
                                    {
                                        row_SaveDataLog["SaveState"] = 0;
                                        row_SaveDataLog["Msg"] = "施工企业保存失败,Message:" + ex.Message;

                                        dt_saveToStLog2.Rows[0]["SbToZxState"] = 1;
                                        dt_saveToStLog2.Rows[0]["SbToZxMsg"] = ex.Message;
                                        dataService.Submit_SaveToStLog2(dt_saveToStLog2);
                                    }
                                    catch (Exception ex2)
                                    {
                                        Public.WriteLog("获取施工单位时出现异常:zzjgdm:" + zzjgdm + ",Message:" + ex2.Message);
                                    }
                                }
                                dt_SaveDataLog.Rows.Add(row_SaveDataLog);

                            }
                        }
                        if (dt_SaveDataLog.Rows.Count > 0)
                            dataService.Submit_SaveDataLog(dt_SaveDataLog);

                        row_DataJkDataDetail_sgdw["allCount"] = allCount_sgdw;
                        row_DataJkDataDetail_sgdw["successCount"] = success_count_sgdw;
                        row_DataJkDataDetail_sgdw["IsOk"] = 1;
                        row_DataJkDataDetail_sgdw["ErrorMsg"] = "";

                        if (dt_DataJkDataDetail_sgdw.Rows.Count > 0)
                            dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_sgdw);
                    }
                    catch (Exception ex)
                    {
                        row_DataJkDataDetail_sgdw["allCount"] = allCount_sgdw;
                        row_DataJkDataDetail_sgdw["successCount"] = success_count_sgdw;
                        row_DataJkDataDetail_sgdw["IsOk"] = 0;
                        row_DataJkDataDetail_sgdw["ErrorMsg"] = ex.Message;
                        dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_sgdw);
                    }


                    #endregion
                    Public.WriteLog("更新施工企业信息完成");

                    #region 获取安监站前置机里的三类(A,B,C)人员信息

                    //往数据监控日志表项添加一条记录
                    DataTable dt_DataJkDataDetail_slry = dataService.GetSchema_DataJkDataDetail();
                    long Id_DataJkDataDetail_slry = dataService.Get_DataJkDataDetailNewID().ToInt64();

                    int allCount_slry = 0, success_count_slry = 0;
                    DataRow row_DataJkDataDetail_slry = dt_DataJkDataDetail_slry.NewRow();
                    dt_DataJkDataDetail_slry.Rows.Add(row_DataJkDataDetail_slry);

                    row_DataJkDataDetail_slry["ID"] = Id_DataJkDataDetail_slry++;
                    row_DataJkDataDetail_slry["DataJkLogID"] = row_DataJkLog["ID"];
                    row_DataJkDataDetail_slry["tableName"] = "UEPP_Ryjbxx";
                    row_DataJkDataDetail_slry["MethodName"] = "Get_SafetyOfficers";

                    try
                    {
                        DataTable dt_ajz_ryxx_aqy = dataService.Get_SafetyOfficers();

                        DataTable dt_SaveDataLog = dataService.GetSchema_SaveDataLog();

                        foreach (DataRow row in dt_ajz_ryxx_aqy.Rows)
                        {
                            string sfzh = row["ID"].ToString2();
                            string ryID = sfzh;

                            DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                            row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_slry["ID"];
                            row_SaveDataLog["DataXml"] = "";
                            row_SaveDataLog["PKID"] = ryID;
                            row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                            {
                                try
                                {
                                    DataTable dt_uepp_Ryjbxx = dataService.Get_uepp_Ryjbxx(ryID);
                                    DataRow row_uepp_Ryjbxx;
                                    if (dt_uepp_Ryjbxx.Rows.Count == 0)
                                    {
                                        row_uepp_Ryjbxx = dt_uepp_Ryjbxx.NewRow();
                                        dt_uepp_Ryjbxx.Rows.Add(row_uepp_Ryjbxx);
                                        row_uepp_Ryjbxx["ryID"] = ryID;
                                        row_uepp_Ryjbxx["tag"] = tag;
                                    }
                                    else
                                    {
                                        if (dt_uepp_Ryjbxx.Rows[0]["xgrqsj"].ToString2().Equals(row["Modified"].ToString2(), StringComparison.CurrentCultureIgnoreCase))
                                        {
                                            continue;
                                        }
                                        row_uepp_Ryjbxx = dt_uepp_Ryjbxx.Rows[0];
                                        if (row_uepp_Ryjbxx["tag"].ToString2().IndexOf(tag) < 0)
                                        {
                                            continue;
                                            //row_uepp_Ryjbxx["tag"] = row_uepp_Ryjbxx["tag"].ToString2() + "," + tag;
                                        }
                                        row_uepp_Ryjbxx["tag"] = tag;

                                    }

                                    row_uepp_Ryjbxx["xm"] = row["Name"];
                                    row_uepp_Ryjbxx["zjlxID"] = "1";
                                    row_uepp_Ryjbxx["zjlx"] = "身份证";
                                    row_uepp_Ryjbxx["zjhm"] = sfzh;
                                    row_uepp_Ryjbxx["xb"] = row["Gender"];
                                    DateTime time;
                                    if (!string.IsNullOrEmpty(row["BirthDate"].ToString2()) && DateTime.TryParse(row["BirthDate"].ToString2(), out time))
                                    {
                                        row_uepp_Ryjbxx["csrq"] = row["BirthDate"];
                                    }

                                    string zc = row["Title"].ToString2().Trim(new char[] { '-', ' ' });
                                    if (!string.IsNullOrEmpty(zc))
                                    {
                                        string code = "";
                                        #region 人员职称代码表
                                        DataTable dt_code_ryzc = dataService.Get_uepp_Code("人员职称", "", zc);
                                        if (dt_code_ryzc.Rows.Count == 0)
                                        {
                                            code = dataService.Get_uepp_Code_NewCode("人员职称");
                                            dt_code_ryzc.Rows.Add(dt_code_ryzc.NewRow());
                                            dt_code_ryzc.Rows[0]["CodeInfo"] = zc;
                                            dt_code_ryzc.Rows[0]["ParentCodeType"] = "";
                                            dt_code_ryzc.Rows[0]["ParentCode"] = "";
                                            dt_code_ryzc.Rows[0]["CodeType"] = "人员职称";
                                            dt_code_ryzc.Rows[0]["CodeDesc"] = "";
                                            dt_code_ryzc.Rows[0]["Code"] = code;
                                            dt_code_ryzc.Rows[0]["OrderID"] = dt_code_ryzc.Rows[0]["Code"];
                                            dataService.Submit_uepp_code(dt_code_ryzc);
                                        }
                                        else
                                        {
                                            code = dt_code_ryzc.Rows[0]["Code"].ToString2();
                                        }
                                        #endregion
                                        row_uepp_Ryjbxx["zc"] = zc;
                                        row_uepp_Ryjbxx["zcID"] = code;
                                    }

                                    #region 人员执业资格

                                    DataTable dt_uepp_Ryzyzg = dataService.Get_uepp_Ryzyzg_aqry(ryID);

                                    DataRow row_uepp_Ryzyzg;
                                    if (dt_uepp_Ryzyzg.Rows.Count == 0)
                                    {
                                        row_uepp_Ryzyzg = dt_uepp_Ryzyzg.NewRow();
                                        dt_uepp_Ryzyzg.Rows.Add(row_uepp_Ryzyzg);
                                        row_uepp_Ryzyzg["ryID"] = ryID;
                                    }
                                    else
                                    {
                                        row_uepp_Ryzyzg = dt_uepp_Ryzyzg.Rows[0];
                                    }

                                    if (row["CateGory"].ToString2().IndexOf("A类") >= 0)
                                    {
                                        row_uepp_Ryzyzg["ryzyzglxID"] = "4";
                                        row_uepp_Ryzyzg["ryzyzglx"] = "企业A类人员";
                                    }
                                    else
                                        if (row["CateGory"].ToString2().IndexOf("B类") >= 0)
                                        {
                                            row_uepp_Ryzyzg["ryzyzglxID"] = "5";
                                            row_uepp_Ryzyzg["ryzyzglx"] = "项目B类人员";
                                        }
                                        else
                                        {
                                            row_uepp_Ryzyzg["ryzyzglxID"] = "6";
                                            row_uepp_Ryzyzg["ryzyzglx"] = "安全员(C类人员)";
                                        }
                                    row_uepp_Ryzyzg["balxID"] = "1";
                                    row_uepp_Ryzyzg["balx"] = "长期备案";

                                    row_uepp_Ryzyzg["DataState"] = "0";
                                    row_uepp_Ryzyzg["tag"] = tag;
                                    row_uepp_Ryzyzg["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                    dataService.Submit_uepp_Ryzyzg(dt_uepp_Ryzyzg);

                                    #endregion

                                    #region 人员证书 UEPP_Ryzs
                                    DataTable dt_uepp_Ryzs = dataService.Get_uepp_Ryzs_aqry(ryID);

                                    DataRow row_uepp_Ryzs;
                                    if (dt_uepp_Ryzs.Rows.Count == 0)
                                    {
                                        row_uepp_Ryzs = dt_uepp_Ryzs.NewRow();
                                        dt_uepp_Ryzs.Rows.Add(row_uepp_Ryzs);
                                        row_uepp_Ryzs["zsjlId"] = dataService.Get_uepp_RyzsNewID();
                                        row_uepp_Ryzs["ryID"] = ryID;
                                    }
                                    else
                                    {
                                        row_uepp_Ryzs = dt_uepp_Ryzs.Rows[0];
                                    }

                                    if (row["CateGory"].ToString2().IndexOf("A类") >= 0)
                                    {
                                        row_uepp_Ryzs["ryzyzglxID"] = "4";
                                        row_uepp_Ryzs["ryzyzglx"] = "企业A类人员";
                                        row_uepp_Ryzs["ryzslxID"] = "41";
                                        row_uepp_Ryzs["ryzslx"] = "A类安全生产考核证";

                                    }
                                    else
                                        if (row["CateGory"].ToString2().IndexOf("B类") >= 0)
                                        {
                                            row_uepp_Ryzs["ryzyzglxID"] = "5";
                                            row_uepp_Ryzs["ryzyzglx"] = "项目B类人员";
                                            row_uepp_Ryzs["ryzslxID"] = "42";
                                            row_uepp_Ryzs["ryzslx"] = "B类安全生产考核证";
                                        }
                                        else
                                        {
                                            row_uepp_Ryzs["ryzyzglxID"] = "6";
                                            row_uepp_Ryzs["ryzyzglx"] = "安全员(C类人员)";
                                            row_uepp_Ryzs["ryzslxID"] = "43";
                                            row_uepp_Ryzs["ryzslx"] = "C类安全生产考核证";
                                        }
                                    row_uepp_Ryzs["sfzzz"] = "1";
                                    row_uepp_Ryzs["zsbh"] = row["LicenseNo"];
                                    if (!string.IsNullOrEmpty(row["ValidFrom"].ToString2()))
                                        row_uepp_Ryzs["zsyxqrq"] = row["ValidFrom"];
                                    if (!string.IsNullOrEmpty(row["ValidTo"].ToString2()))
                                        row_uepp_Ryzs["zsyxzrq"] = row["ValidTo"];

                                    row_uepp_Ryzs["DataState"] = "0";
                                    row_uepp_Ryzs["tag"] = tag;
                                    row_uepp_Ryzs["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                    dataService.Submit_uepp_Ryzs(dt_uepp_Ryzs);

                                    #endregion

                                    #region 人员专业明细 UEPP_Ryzymx

                                    //DataTable dt_uepp_Ryzymx = dataService.Get_uepp_Ryzymx(ryID);
                                    //DataRow row_uepp_Ryzymx;
                                    //if (dt_uepp_Ryzymx.Rows.Count == 0)
                                    //{
                                    //    row_uepp_Ryzymx = dt_uepp_Ryzymx.NewRow();
                                    //    dt_uepp_Ryzymx.Rows.Add(row_uepp_Ryzymx);
                                    //    row_uepp_Ryzymx["ryID"] = ryID;
                                    //}
                                    //else
                                    //{
                                    //    row_uepp_Ryzymx = dt_uepp_Ryzymx.Rows[0];
                                    //}

                                    //if (row["CateGory"].ToString2().IndexOf("A类") >= 0)
                                    //{
                                    //    row_uepp_Ryzs["ryzyzglxID"] = "4";
                                    //    row_uepp_Ryzs["ryzyzglx"] = "企业A类人员";
                                    //    row_uepp_Ryzs["ryzslxID"] = "41";
                                    //    row_uepp_Ryzs["ryzslx"] = "A类安全生产考核证";

                                    //}
                                    //else
                                    //    if (row["CateGory"].ToString2().IndexOf("B类") >= 0)
                                    //    {
                                    //        row_uepp_Ryzs["ryzyzglxID"] = "5";
                                    //        row_uepp_Ryzs["ryzyzglx"] = "项目B类人员";
                                    //        row_uepp_Ryzs["ryzslxID"] = "42";
                                    //        row_uepp_Ryzs["ryzslx"] = "B类安全生产考核证";
                                    //    }
                                    //    else
                                    //    {
                                    //        row_uepp_Ryzs["ryzyzglxID"] = "6";
                                    //        row_uepp_Ryzs["ryzyzglx"] = "安全员(C类人员)";
                                    //        row_uepp_Ryzs["ryzslxID"] = "43";
                                    //        row_uepp_Ryzs["ryzslx"] = "C类安全生产考核证";
                                    //    }

                                    ////row_uepp_Ryzymx["zyzgdjID"] = "2";
                                    ////row_uepp_Ryzymx["zyzgdj"] = "贰级";

                                    //row_uepp_Ryzymx["DataState"] = "0";
                                    //row_uepp_Ryzymx["tag"] = tag;
                                    //row_uepp_Ryzymx["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                    //dataService.Submit_uepp_Ryzymx(dt_uepp_Ryzymx);
                                    #endregion

                                    row_uepp_Ryjbxx["lxdh"] = row["Phones"];
                                    row_uepp_Ryjbxx["yddh"] = row["Mobile"];
                                    row_uepp_Ryjbxx["sfzsmj"] = row["PhotoBytes"];
                                    row_uepp_Ryjbxx["ryzz"] = row["Address"];
                                    row_uepp_Ryjbxx["fzjg"] = row["Agency"];


                                    if (row["IDValidFrom"] != DBNull.Value && !string.IsNullOrEmpty(row["IDValidFrom"].ToString2().Trim(new char[] { ' ', '-' })) && DateTime.TryParse(row["IDValidFrom"].ToString2().Trim(new char[] { ' ', '-' }), out time))
                                        row_uepp_Ryjbxx["sfzyxqs"] = row["IDValidFrom"].ToString2().Trim(new char[] { ' ', '-' });
                                    else
                                        row_uepp_Ryjbxx["sfzyxqs"] = DBNull.Value;

                                    if (row["IDValidTo"] != DBNull.Value && !string.IsNullOrEmpty(row["IDValidTo"].ToString2().Trim(new char[] { ' ', '-' })) && DateTime.TryParse(row["IDValidTo"].ToString2().Trim(new char[] { ' ', '-' }), out time))
                                        row_uepp_Ryjbxx["sfzyxqz"] = row["IDValidTo"].ToString2().Trim(new char[] { ' ', '-' });
                                    else
                                        if (row["IDValidTo"].ToString2().Trim(new char[] { ' ', '-' }).Equals("长期", StringComparison.CurrentCultureIgnoreCase))

                                            row_uepp_Ryjbxx["sfzyxqz"] = DateTime.MaxValue.ToString("yyyy-MM-dd");
                                        else
                                            row_uepp_Ryjbxx["sfzyxqz"] = DBNull.Value;

                                    row_uepp_Ryjbxx["AJ_IsRefuse"] = row["IsRefuse"];
                                    row_uepp_Ryjbxx["AJ_EXISTINIDCARDS"] = row["EXISTINIDCARDS"];
                                    row_uepp_Ryjbxx["DataState"] = "0";


                                    row_uepp_Ryjbxx["xgrqsj"] = string.IsNullOrEmpty(row["Modified"].ToString2()) ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : row["Modified"].ToString2();
                                    allCount_slry++;
                                    if (!dataService.Submit_uepp_ryjbxx(dt_uepp_Ryjbxx))
                                    {
                                        //Public.WriteLog("RYID:" + ryID + ",获取失败！");

                                        row_SaveDataLog["SaveState"] = 1;
                                        row_SaveDataLog["Msg"] = "三类人员信息获取失败！";
                                    }
                                    else
                                    {
                                        success_count_slry++;
                                        row_SaveDataLog["SaveState"] = 1;
                                        row_SaveDataLog["Msg"] = "";
                                    }

                                    #region 企业人员执业资格关系表
                                    DataTable dt_uepp_qyryzyzg = dataService.Get_uepp_qyryzyzg_aqry(row["FullNoc"].ToString2(), ryID);
                                    DataRow row_uepp_qyryzyzg;
                                    if (dt_uepp_qyryzyzg.Rows.Count == 0)
                                    {
                                        row_uepp_qyryzyzg = dt_uepp_qyryzyzg.NewRow();
                                        dt_uepp_qyryzyzg.Rows.Add(row_uepp_qyryzyzg);
                                        row_uepp_qyryzyzg["qyID"] = row["FullNoc"].ToString2();
                                        row_uepp_qyryzyzg["ryID"] = ryID;
                                        row_uepp_qyryzyzg["ryzyzglxID"] = row_uepp_Ryzyzg["ryzyzglxID"].ToString2();
                                    }
                                    else
                                    {
                                        row_uepp_qyryzyzg = dt_uepp_qyryzyzg.Rows[0];
                                    }
                                    row_uepp_qyryzyzg["ryzyzglx"] = row_uepp_Ryzyzg["ryzyzglx"].ToString2();
                                    row_uepp_qyryzyzg["DataState"] = "0";
                                    row_uepp_qyryzyzg["tag"] = tag;
                                    row_uepp_qyryzyzg["xgrqsj"] = string.IsNullOrEmpty(row["Modified"].ToString2()) ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : row["Modified"].ToString2();


                                    dataService.Submit_uepp_qyry(dt_uepp_qyryzyzg);
                                    #endregion

                                    scope.Complete();

                                }
                                catch (Exception ex)
                                {
                                    row_SaveDataLog["SaveState"] = 0;
                                    row_SaveDataLog["Msg"] = "获取三类(A,B,C)人员信息时出现异常:RYID:" + ryID + ",Message:" + ex.Message;
                                }
                            }
                            dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                        }

                        if (dt_SaveDataLog.Rows.Count > 0)
                            dataService.Submit_SaveDataLog(dt_SaveDataLog);

                        row_DataJkDataDetail_slry["allCount"] = allCount_slry;
                        row_DataJkDataDetail_slry["successCount"] = success_count_slry;
                        row_DataJkDataDetail_slry["IsOk"] = 1;
                        row_DataJkDataDetail_slry["ErrorMsg"] = "";

                        if (dt_DataJkDataDetail_slry.Rows.Count > 0)
                            dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_slry);
                    }
                    catch (Exception ex)
                    {
                        Public.WriteLog("获取人员信息时出现异常:" + ex.Message);
                        row_DataJkDataDetail_slry["allCount"] = allCount_slry;
                        row_DataJkDataDetail_slry["successCount"] = success_count_slry;
                        row_DataJkDataDetail_slry["IsOk"] = 0;
                        row_DataJkDataDetail_slry["ErrorMsg"] = ex.Message;
                        if (dt_DataJkDataDetail_slry.Rows.Count > 0)
                            dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_slry);

                    }

                    #endregion

                    Public.WriteLog("更新三类人员信息完成");

                    #region 获取安监站前置机里的技经人员信息

                    //往数据监控日志表项添加一条记录
                    DataTable dt_DataJkDataDetail_jjry = dataService.GetSchema_DataJkDataDetail();
                    long Id_DataJkDataDetail_jjry = dataService.Get_DataJkDataDetailNewID().ToInt64();

                    int allCount_jjry = 0, success_count_jjry = 0;
                    DataRow row_DataJkDataDetail_jjry = dt_DataJkDataDetail_slry.NewRow();
                    dt_DataJkDataDetail_slry.Rows.Add(row_DataJkDataDetail_jjry);

                    row_DataJkDataDetail_jjry["ID"] = Id_DataJkDataDetail_jjry++;
                    row_DataJkDataDetail_jjry["DataJkLogID"] = row_DataJkLog["ID"];
                    row_DataJkDataDetail_jjry["tableName"] = "UEPP_Ryjbxx";
                    row_DataJkDataDetail_jjry["MethodName"] = "Get_Employees";

                    try
                    {
                        DataTable dt_ajz_ryxx_jjry = dataService.Get_Employees();

                        DataTable dt_SaveDataLog = dataService.GetSchema_SaveDataLog();

                        foreach (DataRow row in dt_ajz_ryxx_jjry.Rows)
                        {
                            string sfzh = row["ID"].ToString2();
                            string ryID = sfzh;

                            DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                            row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_jjry["ID"];
                            row_SaveDataLog["DataXml"] = "";
                            row_SaveDataLog["PKID"] = ryID;
                            row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");


                            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                            {
                                try
                                {
                                    DataTable dt_uepp_Ryjbxx = dataService.Get_uepp_Ryjbxx(ryID);
                                    DataRow row_uepp_Ryjbxx;
                                    if (dt_uepp_Ryjbxx.Rows.Count == 0)
                                    {
                                        row_uepp_Ryjbxx = dt_uepp_Ryjbxx.NewRow();
                                        dt_uepp_Ryjbxx.Rows.Add(row_uepp_Ryjbxx);
                                        row_uepp_Ryjbxx["ryID"] = ryID;

                                        row_uepp_Ryjbxx["tag"] = tag;
                                    }
                                    else
                                    {
                                        if (dt_uepp_Ryjbxx.Rows[0]["xgrqsj"].ToString2().Equals(row["Modified"].ToString2()))
                                        {
                                            continue;
                                        }
                                        row_uepp_Ryjbxx = dt_uepp_Ryjbxx.Rows[0];

                                        if (row_uepp_Ryjbxx["tag"].ToString2().IndexOf(tag) < 0)
                                        {
                                            continue;
                                            //row_uepp_Ryjbxx["tag"] = row_uepp_Ryjbxx["tag"].ToString2() + "," + tag;
                                        }
                                        row_uepp_Ryjbxx["tag"] = tag;
                                    }

                                    row_uepp_Ryjbxx["xm"] = row["Name"];
                                    row_uepp_Ryjbxx["zjlxID"] = "1";
                                    row_uepp_Ryjbxx["zjlx"] = "身份证";
                                    row_uepp_Ryjbxx["zjhm"] = sfzh;
                                    row_uepp_Ryjbxx["xb"] = row["Gender"];
                                    DateTime time;
                                    if (!string.IsNullOrEmpty(row["BirthDate"].ToString2()) && DateTime.TryParse(row["BirthDate"].ToString2(), out time))
                                    {
                                        row_uepp_Ryjbxx["csrq"] = row["BirthDate"];
                                    }

                                    string zc = row["Title"].ToString2().Trim(new char[] { '-', ' ' });
                                    if (!string.IsNullOrEmpty(zc))
                                    {
                                        string code = "";
                                        #region 人员职称代码表
                                        DataTable dt_code_ryzc = dataService.Get_uepp_Code("人员职称", "", zc);
                                        if (dt_code_ryzc.Rows.Count == 0)
                                        {
                                            code = dataService.Get_uepp_Code_NewCode("人员职称");
                                            dt_code_ryzc.Rows.Add(dt_code_ryzc.NewRow());
                                            dt_code_ryzc.Rows[0]["CodeInfo"] = zc;
                                            dt_code_ryzc.Rows[0]["ParentCodeType"] = "";
                                            dt_code_ryzc.Rows[0]["ParentCode"] = "";
                                            dt_code_ryzc.Rows[0]["CodeType"] = "人员职称";
                                            dt_code_ryzc.Rows[0]["CodeDesc"] = "";
                                            dt_code_ryzc.Rows[0]["Code"] = code;
                                            dt_code_ryzc.Rows[0]["OrderID"] = dt_code_ryzc.Rows[0]["Code"];
                                            dataService.Submit_uepp_code(dt_code_ryzc);

                                        }
                                        else
                                        {
                                            code = dt_code_ryzc.Rows[0]["Code"].ToString2();
                                        }
                                        #endregion

                                        row_uepp_Ryjbxx["zc"] = zc;
                                        row_uepp_Ryjbxx["zcID"] = code;
                                    }

                                    #region 人员执业资格

                                    DataTable dt_uepp_Ryzyzg = dataService.Get_uepp_Ryzyzg_jjry(ryID);

                                    DataRow row_uepp_Ryzyzg;
                                    if (dt_uepp_Ryzyzg.Rows.Count == 0)
                                    {
                                        row_uepp_Ryzyzg = dt_uepp_Ryzyzg.NewRow();
                                        dt_uepp_Ryzyzg.Rows.Add(row_uepp_Ryzyzg);
                                        row_uepp_Ryzyzg["ryID"] = ryID;
                                    }
                                    else
                                    {
                                        row_uepp_Ryzyzg = dt_uepp_Ryzyzg.Rows[0];
                                    }

                                    row_uepp_Ryzyzg["ryzyzglxID"] = "20";
                                    row_uepp_Ryzyzg["ryzyzglx"] = "技经人员";

                                    row_uepp_Ryzyzg["balxID"] = "1";
                                    row_uepp_Ryzyzg["balx"] = "长期备案";

                                    row_uepp_Ryzyzg["DataState"] = "0";
                                    row_uepp_Ryzyzg["tag"] = tag;
                                    row_uepp_Ryzyzg["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                    dataService.Submit_uepp_Ryzyzg(dt_uepp_Ryzyzg);

                                    #endregion

                                    row_uepp_Ryjbxx["yddh"] = row["Mobile"];

                                    if (row["IDValidFrom"] != DBNull.Value && !string.IsNullOrEmpty(row["IDValidFrom"].ToString2()) && DateTime.TryParse(row["IDValidFrom"].ToString2(), out time))
                                        row_uepp_Ryjbxx["sfzyxqs"] = row["IDValidFrom"];
                                    else
                                        row_uepp_Ryjbxx["sfzyxqs"] = DBNull.Value;

                                    if (row["IDValidTo"] != DBNull.Value && !string.IsNullOrEmpty(row["IDValidTo"].ToString2()) && DateTime.TryParse(row["IDValidTo"].ToString2(), out time))
                                        row_uepp_Ryjbxx["sfzyxqz"] = row["IDValidTo"];
                                    else
                                        row_uepp_Ryjbxx["sfzyxqz"] = DateTime.MaxValue.ToString("yyyy-MM-dd");

                                    row_uepp_Ryjbxx["AJ_IsRefuse"] = row["IsRefuse"];
                                    row_uepp_Ryjbxx["AJ_EXISTINIDCARDS"] = row["EXISTINIDCARDS"];
                                    row_uepp_Ryjbxx["DataState"] = "0";

                                    row_uepp_Ryjbxx["xgrqsj"] = row["Modified"];

                                    if (!dataService.Submit_uepp_ryjbxx(dt_uepp_Ryjbxx))
                                    {
                                        //Public.WriteLog("RYID:" + ryID + ",获取失败！");
                                        row_SaveDataLog["SaveState"] = 0;
                                        row_SaveDataLog["Msg"] = "三类人员信息获取失败！";
                                    }
                                    else
                                    {
                                        row_SaveDataLog["SaveState"] = 1;
                                        row_SaveDataLog["Msg"] = "";
                                    }

                                    #region 企业人员执业资格关系表
                                    DataTable dt_uepp_qyryzyzg = dataService.Get_uepp_qyryzyzg_jjry(row["FullNoc"].ToString2(), ryID);
                                    DataRow row_uepp_qyryzyzg;
                                    if (dt_uepp_qyryzyzg.Rows.Count == 0)
                                    {
                                        row_uepp_qyryzyzg = dt_uepp_qyryzyzg.NewRow();
                                        dt_uepp_qyryzyzg.Rows.Add(row_uepp_qyryzyzg);
                                        row_uepp_qyryzyzg["qyID"] = row["FullNoc"].ToString2();
                                        row_uepp_qyryzyzg["ryID"] = ryID;
                                        row_uepp_qyryzyzg["ryzyzglxID"] = row_uepp_Ryzyzg["ryzyzglxID"].ToString2();
                                    }
                                    else
                                    {
                                        row_uepp_qyryzyzg = dt_uepp_qyryzyzg.Rows[0];
                                    }
                                    row_uepp_qyryzyzg["ryzyzglx"] = row_uepp_Ryzyzg["ryzyzglx"].ToString2();
                                    row_uepp_qyryzyzg["DataState"] = "0";
                                    row_uepp_qyryzyzg["tag"] = tag;
                                    row_uepp_qyryzyzg["xgrqsj"] = row["Modified"];
                                    dataService.Submit_uepp_qyry(dt_uepp_qyryzyzg);
                                    #endregion

                                    scope.Complete();

                                }
                                catch (Exception ex)
                                {
                                    //Public.WriteLog("获取技经人员信息时出现异常:RYID:" + ryID + ",Message:" + ex.Message);
                                    row_SaveDataLog["SaveState"] = 0;
                                    row_SaveDataLog["Msg"] = "获取技经人员信息时出现异常:RYID:" + ryID + ",Message:" + ex.Message;
                                }
                            }
                            dt_SaveDataLog.Rows.Add(row_SaveDataLog);

                        }
                        if (dt_SaveDataLog.Rows.Count > 0)
                            dataService.Submit_SaveDataLog(dt_SaveDataLog);

                        row_DataJkDataDetail_jjry["allCount"] = allCount_jjry;
                        row_DataJkDataDetail_jjry["successCount"] = success_count_jjry;
                        row_DataJkDataDetail_jjry["IsOk"] = 1;
                        row_DataJkDataDetail_jjry["ErrorMsg"] = "";

                        if (dt_DataJkDataDetail_jjry.Rows.Count > 0)
                            dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_jjry);
                    }
                    catch (Exception ex)
                    {
                        //Public.WriteLog("获取技经人员信息时出现异常:" + ex.Message);
                        row_DataJkDataDetail_jjry["allCount"] = allCount_jjry;
                        row_DataJkDataDetail_jjry["successCount"] = success_count_jjry;
                        row_DataJkDataDetail_jjry["IsOk"] = 0;
                        row_DataJkDataDetail_jjry["ErrorMsg"] = "获取技经人员信息时出现异常:" + ex.Message;

                    }

                    #endregion

                    Public.WriteLog("更新技经人员信息完成");

                    #region 获取安监站前置机里的特种作业人员信息

                    //往数据监控日志表项添加一条记录
                    DataTable dt_DataJkDataDetail_tzzyry = dataService.GetSchema_DataJkDataDetail();
                    long Id_DataJkDataDetail_tzzyry = dataService.Get_DataJkDataDetailNewID().ToInt64();

                    int allCount_tzzyry = 0, success_count_tzzyry = 0;
                    DataRow row_DataJkDataDetail_tzzyry = dt_DataJkDataDetail_tzzyry.NewRow();
                    dt_DataJkDataDetail_tzzyry.Rows.Add(row_DataJkDataDetail_tzzyry);

                    row_DataJkDataDetail_tzzyry["ID"] = Id_DataJkDataDetail_tzzyry++;
                    row_DataJkDataDetail_tzzyry["DataJkLogID"] = row_DataJkLog["ID"];
                    row_DataJkDataDetail_tzzyry["tableName"] = "UEPP_Ryjbxx";
                    row_DataJkDataDetail_tzzyry["MethodName"] = "Get_SpecialOperators";

                    try
                    {
                        DataTable dt_ajz_ryxx_tzzy = dataService.Get_SpecialOperators();
                        DataTable dt_SaveDataLog = dataService.GetSchema_SaveDataLog();

                        foreach (DataRow row in dt_ajz_ryxx_tzzy.Rows)
                        {
                            string sfzh = row["ID"].ToString2();
                            string ryID = sfzh;

                            DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                            row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_tzzyry["ID"];
                            row_SaveDataLog["DataXml"] = "";
                            row_SaveDataLog["PKID"] = ryID;
                            row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                            {
                                try
                                {
                                    DataTable dt_uepp_Ryjbxx = dataService.Get_uepp_Ryjbxx(ryID);
                                    DataRow row_uepp_Ryjbxx;
                                    if (dt_uepp_Ryjbxx.Rows.Count == 0)
                                    {
                                        row_uepp_Ryjbxx = dt_uepp_Ryjbxx.NewRow();
                                        dt_uepp_Ryjbxx.Rows.Add(row_uepp_Ryjbxx);
                                        row_uepp_Ryjbxx["ryID"] = ryID;
                                        row_uepp_Ryjbxx["tag"] = tag;
                                    }
                                    else
                                    {
                                        if (dt_uepp_Ryjbxx.Rows[0]["xgrqsj"].ToString2().Equals(row["Modified"].ToString2()))
                                        {
                                            continue;
                                        }
                                        row_uepp_Ryjbxx = dt_uepp_Ryjbxx.Rows[0];
                                        if (row_uepp_Ryjbxx["tag"].ToString2().IndexOf(tag) < 0)
                                        {
                                            continue;
                                            //row_uepp_Ryjbxx["tag"] = row_uepp_Ryjbxx["tag"].ToString2() + "," + tag;
                                        }
                                        //row_uepp_Ryjbxx["tag"] = tag;

                                    }

                                    row_uepp_Ryjbxx["xm"] = row["Name"];
                                    row_uepp_Ryjbxx["zjlxID"] = "1";
                                    row_uepp_Ryjbxx["zjlx"] = "身份证";
                                    row_uepp_Ryjbxx["zjhm"] = sfzh;
                                    row_uepp_Ryjbxx["xb"] = row["Gender"];

                                    DateTime time;
                                    if (!string.IsNullOrEmpty(row["BirthDate"].ToString2()) && DateTime.TryParse(row["BirthDate"].ToString2(), out time))
                                    {
                                        row_uepp_Ryjbxx["csrq"] = row["BirthDate"];
                                    }

                                    #region 人员执业资格

                                    DataTable dt_uepp_Ryzyzg = dataService.Get_uepp_Ryzyzg_tzzy(ryID);

                                    DataRow row_uepp_Ryzyzg;
                                    if (dt_uepp_Ryzyzg.Rows.Count == 0)
                                    {
                                        row_uepp_Ryzyzg = dt_uepp_Ryzyzg.NewRow();
                                        dt_uepp_Ryzyzg.Rows.Add(row_uepp_Ryzyzg);
                                        row_uepp_Ryzyzg["ryID"] = ryID;
                                    }
                                    else
                                    {
                                        row_uepp_Ryzyzg = dt_uepp_Ryzyzg.Rows[0];
                                    }

                                    row_uepp_Ryzyzg["ryzyzglxID"] = "17";
                                    row_uepp_Ryzyzg["ryzyzglx"] = "特种作业人员";

                                    row_uepp_Ryzyzg["balxID"] = "1";
                                    row_uepp_Ryzyzg["balx"] = "长期备案";

                                    row_uepp_Ryzyzg["DataState"] = "0";
                                    row_uepp_Ryzyzg["tag"] = tag;
                                    row_uepp_Ryzyzg["xgrqsj"] = row["Modified"].ToString2();

                                    dataService.Submit_uepp_Ryzyzg(dt_uepp_Ryzyzg);

                                    #endregion

                                    row_uepp_Ryjbxx["sfzyxqs"] = row["IDValidFrom"];
                                    if (row["IDValidTo"] != DBNull.Value && !string.IsNullOrEmpty(row["IDValidTo"].ToString2()) && DateTime.TryParse(row["IDValidTo"].ToString2(), out time))
                                        row_uepp_Ryjbxx["sfzyxqz"] = row["IDValidTo"];
                                    else
                                        row_uepp_Ryjbxx["sfzyxqz"] = DateTime.MaxValue.ToString("yyyy-MM-dd");
                                    row_uepp_Ryjbxx["AJ_EXISTINIDCARDS"] = row["EXISTINIDCARDS"];
                                    row_uepp_Ryjbxx["AJ_IsRefuse"] = row["IsRefuse"];
                                    row_uepp_Ryjbxx["DataState"] = "0";

                                    row_uepp_Ryjbxx["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                    allCount_tzzyry++;

                                    if (!dataService.Submit_uepp_ryjbxx(dt_uepp_Ryjbxx))
                                    {
                                        Public.WriteLog("RYID:" + ryID + ",获取失败！");
                                        row_SaveDataLog["SaveState"] = 0;
                                        row_SaveDataLog["Msg"] = "特种作业人员信息获取失败！";
                                    }
                                    else
                                    {
                                        success_count_tzzyry++;
                                        row_SaveDataLog["SaveState"] = 1;
                                        row_SaveDataLog["Msg"] = "";
                                    }

                                    #region 人员证书 UEPP_Ryzs
                                    string zsbh = row["LicenseNo"].ToString2();
                                    if (!string.IsNullOrEmpty(zsbh))
                                    {
                                        DataTable dt_uepp_Ryzs = dataService.Get_uepp_Ryzs_tzzy(ryID);

                                        DataRow row_uepp_Ryzs;
                                        if (dt_uepp_Ryzs.Rows.Count == 0)
                                        {
                                            row_uepp_Ryzs = dt_uepp_Ryzs.NewRow();
                                            dt_uepp_Ryzs.Rows.Add(row_uepp_Ryzs);
                                            row_uepp_Ryzs["zsjlId"] = dataService.Get_uepp_RyzsNewID();
                                            row_uepp_Ryzs["ryID"] = ryID;
                                        }
                                        else
                                        {
                                            row_uepp_Ryzs = dt_uepp_Ryzs.Rows[0];
                                        }

                                        row_uepp_Ryzs["ryzyzglxID"] = "17";
                                        row_uepp_Ryzs["ryzyzglx"] = "特种作业人员";
                                        row_uepp_Ryzs["ryzslxID"] = "87";
                                        row_uepp_Ryzs["ryzslx"] = "特种作业人员上岗证";

                                        row_uepp_Ryzs["sfzzz"] = "1";
                                        row_uepp_Ryzs["zsbh"] = zsbh;

                                        if (!string.IsNullOrEmpty(row["Licensed"].ToString2()))
                                            row_uepp_Ryzs["zsyxqrq"] = row["Licensed"];
                                        if (!string.IsNullOrEmpty(row["Checked"].ToString2()))
                                            row_uepp_Ryzs["zsyxzrq"] = row["Checked"];

                                        row_uepp_Ryzs["DataState"] = "0";
                                        row_uepp_Ryzs["tag"] = tag;
                                        row_uepp_Ryzs["xgrqsj"] = row["Modified"].ToString2();

                                        dataService.Submit_uepp_Ryzs(dt_uepp_Ryzs);
                                    }
                                    #endregion

                                    #region 企业人员执业资格关系表
                                    DataTable dt_uepp_qyryzyzg = dataService.Get_uepp_qyryzyzg_tzzy(row["FullNoc"].ToString2(), ryID);
                                    DataRow row_uepp_qyryzyzg;
                                    if (dt_uepp_qyryzyzg.Rows.Count == 0)
                                    {
                                        row_uepp_qyryzyzg = dt_uepp_qyryzyzg.NewRow();
                                        dt_uepp_qyryzyzg.Rows.Add(row_uepp_qyryzyzg);
                                        row_uepp_qyryzyzg["qyID"] = row["FullNoc"].ToString2();
                                        row_uepp_qyryzyzg["ryID"] = ryID;
                                        row_uepp_qyryzyzg["ryzyzglxID"] = row_uepp_Ryzyzg["ryzyzglxID"].ToString2();
                                    }
                                    else
                                    {
                                        row_uepp_qyryzyzg = dt_uepp_qyryzyzg.Rows[0];
                                    }
                                    row_uepp_qyryzyzg["ryzyzglx"] = row_uepp_Ryzyzg["ryzyzglx"].ToString2();
                                    row_uepp_qyryzyzg["DataState"] = "0";
                                    row_uepp_qyryzyzg["tag"] = tag;
                                    row_uepp_qyryzyzg["xgrqsj"] = row["Modified"].ToString2();
                                    dataService.Submit_uepp_qyry(dt_uepp_qyryzyzg);
                                    #endregion

                                    scope.Complete();
                                }
                                catch (Exception ex)
                                {
                                    //Public.WriteLog("获取特种作业人员信息时出现异常:RYID:" + ryID + ",Message:" + ex.Message);
                                    row_SaveDataLog["SaveState"] = 0;
                                    row_SaveDataLog["Msg"] = "获取特种作业人员信息时出现异常:RYID:" + ryID + ",Message:" + ex.Message;
                                }
                            }
                            dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                        }
                        if (dt_SaveDataLog.Rows.Count > 0)
                            dataService.Submit_SaveDataLog(dt_SaveDataLog);

                        row_DataJkDataDetail_tzzyry["allCount"] = allCount_tzzyry;
                        row_DataJkDataDetail_tzzyry["successCount"] = success_count_tzzyry;
                        row_DataJkDataDetail_tzzyry["IsOk"] = 1;
                        row_DataJkDataDetail_tzzyry["ErrorMsg"] = "";

                        if (dt_DataJkDataDetail_tzzyry.Rows.Count > 0)
                            dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_tzzyry);
                    }
                    catch (Exception ex)
                    {
                        Public.WriteLog("获取特种作业人员信息时出现异常:" + ex.Message);
                        row_DataJkDataDetail_tzzyry["allCount"] = allCount_tzzyry;
                        row_DataJkDataDetail_tzzyry["successCount"] = success_count_tzzyry;
                        row_DataJkDataDetail_tzzyry["IsOk"] = 0;
                        row_DataJkDataDetail_tzzyry["ErrorMsg"] = "获取特种作业人员信息时出现异常:" + ex.Message;

                        if (dt_DataJkDataDetail_tzzyry.Rows.Count > 0)
                            dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_tzzyry);
                    }

                    #endregion

                    Public.WriteLog("更新特种作业人员信息完成");

                    #region 获取安监站前置机里的注册建造师，小型项目管理师信息
                    //往数据监控日志表项添加一条记录
                    DataTable dt_DataJkDataDetail_zcjzs = dataService.GetSchema_DataJkDataDetail();
                    long Id_DataJkDataDetail_zcjzs = dataService.Get_DataJkDataDetailNewID().ToInt64();

                    int allCount_zcjzs = 0, success_count_zcjzs = 0;
                    DataRow row_DataJkDataDetail_zcjzs = dt_DataJkDataDetail_zcjzs.NewRow();
                    dt_DataJkDataDetail_zcjzs.Rows.Add(row_DataJkDataDetail_zcjzs);

                    row_DataJkDataDetail_zcjzs["ID"] = Id_DataJkDataDetail_zcjzs++;
                    row_DataJkDataDetail_zcjzs["DataJkLogID"] = row_DataJkLog["ID"];
                    row_DataJkDataDetail_zcjzs["tableName"] = "UEPP_Ryjbxx";
                    row_DataJkDataDetail_zcjzs["MethodName"] = "Get_Constructors";

                    try
                    {
                        DataTable dt_ajz_ryxx = dataService.Get_Constructors();
                        DataTable dt_SaveDataLog = dataService.GetSchema_SaveDataLog();

                        foreach (DataRow row in dt_ajz_ryxx.Rows)
                        {
                            string sfzh = row["ID"].ToString2();

                            DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                            row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_zcjzs["ID"];
                            row_SaveDataLog["DataXml"] = "";
                            row_SaveDataLog["PKID"] = sfzh;
                            row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                            DataTable dt_saveToStLog2 = dataService.Get_SaveToStLog2("UEPP_Ryjbxx", sfzh);
                            if (dt_saveToStLog2.Rows.Count == 0)
                            {
                                dt_saveToStLog2.Rows.Add(dt_saveToStLog2.NewRow());
                                dt_saveToStLog2.Rows[0]["PKID"] = sfzh;
                                dt_saveToStLog2.Rows[0]["TableName"] = "UEPP_Ryjbxx";
                                dt_saveToStLog2.Rows[0]["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            dt_saveToStLog2.Rows[0]["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                            {
                                try
                                {
                                    DataTable dt_uepp_Ryjbxx = dataService.Get_uepp_Ryjbxx(sfzh);
                                    DataRow row_uepp_Ryjbxx;
                                    if (dt_uepp_Ryjbxx.Rows.Count == 0)
                                    {
                                        row_uepp_Ryjbxx = dt_uepp_Ryjbxx.NewRow();
                                        dt_uepp_Ryjbxx.Rows.Add(row_uepp_Ryjbxx);
                                        row_uepp_Ryjbxx["ryID"] = sfzh;
                                        row_uepp_Ryjbxx["tag"] = tag;
                                    }
                                    else
                                    {
                                        if (dt_uepp_Ryjbxx.Rows[0]["xgrqsj"].ToString2().Equals(row["Modified"].ToString2()))
                                        {
                                            continue;
                                        }
                                        row_uepp_Ryjbxx = dt_uepp_Ryjbxx.Rows[0];

                                        //if (row_uepp_Ryjbxx["tag"].ToString2().IndexOf(tag) < 0)
                                        //{
                                        //    continue;
                                        //    //row_uepp_Ryjbxx["tag"] = row_uepp_Ryjbxx["tag"].ToString2() + "," + tag;
                                        //}
                                        row_uepp_Ryjbxx["tag"] = tag;
                                    }

                                    row_uepp_Ryjbxx["xm"] = row["Name"];
                                    row_uepp_Ryjbxx["zjlxID"] = "1";
                                    row_uepp_Ryjbxx["zjlx"] = "身份证";
                                    row_uepp_Ryjbxx["zjhm"] = sfzh;
                                    row_uepp_Ryjbxx["xb"] = row["Gender"];
                                    DateTime time;
                                    if (!string.IsNullOrEmpty(row["BirthDate"].ToString2()) && DateTime.TryParse(row["BirthDate"].ToString2(), out time))
                                    {
                                        row_uepp_Ryjbxx["csrq"] = row["BirthDate"];
                                    }

                                    string zc = row["Title"].ToString2().Trim(new char[] { '-', ' ' });
                                    if (!string.IsNullOrEmpty(zc))
                                    {
                                        string code = "";
                                        #region 人员职称代码表
                                        DataTable dt_code_ryzc = dataService.Get_uepp_Code("人员职称", "", zc);
                                        if (dt_code_ryzc.Rows.Count == 0)
                                        {
                                            code = dataService.Get_uepp_Code_NewCode("人员职称");
                                            dt_code_ryzc.Rows.Add(dt_code_ryzc.NewRow());
                                            dt_code_ryzc.Rows[0]["CodeInfo"] = zc;
                                            dt_code_ryzc.Rows[0]["ParentCodeType"] = "";
                                            dt_code_ryzc.Rows[0]["ParentCode"] = "";
                                            dt_code_ryzc.Rows[0]["CodeType"] = "人员职称";
                                            dt_code_ryzc.Rows[0]["CodeDesc"] = "";
                                            dt_code_ryzc.Rows[0]["Code"] = code;
                                            dt_code_ryzc.Rows[0]["OrderID"] = dt_code_ryzc.Rows[0]["Code"];
                                            dataService.Submit_uepp_code(dt_code_ryzc);

                                        }
                                        else
                                        {
                                            code = dt_code_ryzc.Rows[0]["Code"].ToString2();
                                        }
                                        #endregion

                                        row_uepp_Ryjbxx["zc"] = zc;
                                        row_uepp_Ryjbxx["zcID"] = code;

                                    }

                                    row_uepp_Ryjbxx["lxdh"] = row["Mobile"];
                                    row_uepp_Ryjbxx["yddh"] = row["Phones"];
                                    row_uepp_Ryjbxx["sfzsmj"] = row["PhotoBytes"];
                                    row_uepp_Ryjbxx["ryzz"] = row["Address"];
                                    row_uepp_Ryjbxx["fzjg"] = row["Agency"];
                                    row_uepp_Ryjbxx["sfzyxqs"] = row["IDValidFrom"];
                                    if (row["IDValidTo"].ToString2() == "长期")
                                    {
                                        row_uepp_Ryjbxx["sfzyxqz"] = DateTime.MaxValue.ToString("yyyy-MM-dd");
                                    }
                                    else
                                    {
                                        row_uepp_Ryjbxx["sfzyxqz"] = row["IDValidTo"];
                                    }

                                    row_uepp_Ryjbxx["AJ_IsRefuse"] = row["IsRefuse"];
                                    row_uepp_Ryjbxx["AJ_EXISTINIDCARDS"] = row["EXISTINIDCARDS"];
                                    row_uepp_Ryjbxx["DataState"] = "0";

                                    row_uepp_Ryjbxx["xgrqsj"] = string.IsNullOrEmpty(row["Modified"].ToString2()) ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : row["Modified"].ToString2();
                                    allCount_zcjzs++;

                                    if (dataService.Submit_uepp_ryjbxx(dt_uepp_Ryjbxx))
                                    {
                                        dt_saveToStLog2.Rows[0]["SbToZxState"] = 0;
                                        dt_saveToStLog2.Rows[0]["SbToZxMsg"] = "获取成功";
                                        if (dt_saveToStLog2.Rows[0]["SbToStState"].ToString2().Equals("0"))
                                        {
                                            dt_saveToStLog2.Rows[0]["SbToStState"] = 1;
                                            dt_saveToStLog2.Rows[0]["SbToStMsg"] = "未更新";
                                        }
                                        success_count_zcjzs++;
                                    }
                                    else
                                    {
                                        dt_saveToStLog2.Rows[0]["SbToZxState"] = 1;
                                        dt_saveToStLog2.Rows[0]["SbToZxMsg"] = "获取失败";
                                    }

                                    dataService.Submit_SaveToStLog2(dt_saveToStLog2);

                                    #region 人员执业资格

                                    DataTable dt_uepp_Ryzyzg = dataService.Get_uepp_Ryzyzg_zcjzs(sfzh);

                                    DataRow row_uepp_Ryzyzg;
                                    if (dt_uepp_Ryzyzg.Rows.Count == 0)
                                    {
                                        row_uepp_Ryzyzg = dt_uepp_Ryzyzg.NewRow();
                                        dt_uepp_Ryzyzg.Rows.Add(row_uepp_Ryzyzg);
                                        row_uepp_Ryzyzg["ryID"] = sfzh;
                                    }
                                    else
                                    {
                                        row_uepp_Ryzyzg = dt_uepp_Ryzyzg.Rows[0];
                                    }

                                    if (row["Grade"].ToString2().Equals("一级") || row["Grade"].ToString2().Equals("二级"))
                                    {
                                        row_uepp_Ryzyzg["ryzyzglxID"] = "1";
                                        row_uepp_Ryzyzg["ryzyzglx"] = "注册建造师";
                                    }
                                    else
                                    {
                                        row_uepp_Ryzyzg["ryzyzglxID"] = "2";
                                        row_uepp_Ryzyzg["ryzyzglx"] = "小型项目管理师";
                                    }
                                    row_uepp_Ryzyzg["balxID"] = "1";
                                    row_uepp_Ryzyzg["balx"] = "长期备案";
                                    row_uepp_Ryzyzg["tag"] = tag;
                                    row_uepp_Ryzyzg["DataState"] = "0";
                                    row_uepp_Ryzyzg["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                    dataService.Submit_uepp_Ryzyzg(dt_uepp_Ryzyzg);

                                    #endregion

                                    #region 人员证书 UEPP_Ryzs

                                    DataTable dt_uepp_Ryzs = dataService.Get_uepp_Ryzs_zcjzs_zgz(sfzh);
                                    DataRow row_uepp_Ryzs;
                                    if (dt_uepp_Ryzs.Rows.Count == 0)
                                    {
                                        row_uepp_Ryzs = dt_uepp_Ryzs.NewRow();
                                        dt_uepp_Ryzs.Rows.Add(row_uepp_Ryzs);
                                        row_uepp_Ryzs["zsjlId"] = dataService.Get_uepp_RyzsNewID();
                                        row_uepp_Ryzs["ryID"] = sfzh;
                                    }
                                    else
                                    {
                                        row_uepp_Ryzs = dt_uepp_Ryzs.Rows[0];
                                    }

                                    if (row["Grade"].ToString2().Equals("一级") || row["Grade"].ToString2().Equals("二级"))
                                    {
                                        row_uepp_Ryzs["ryzyzglxID"] = "1";
                                        row_uepp_Ryzs["ryzyzglx"] = "注册建造师";
                                        row_uepp_Ryzs["ryzslxID"] = "11";
                                        row_uepp_Ryzs["ryzslx"] = "注册建造师资格证";

                                    }
                                    else
                                    {
                                        row_uepp_Ryzs["ryzyzglxID"] = "2";
                                        row_uepp_Ryzs["ryzyzglx"] = "小型项目管理师";
                                        row_uepp_Ryzs["ryzslxID"] = "21";
                                        row_uepp_Ryzs["ryzslx"] = "小型项目管理师资格证";
                                    }
                                    row_uepp_Ryzs["sfzzz"] = "1";
                                    row_uepp_Ryzs["zsbh"] = row["RegNo"];
                                    if (!string.IsNullOrEmpty(row["ValidFrom"].ToString2()))
                                        row_uepp_Ryzs["zsyxqrq"] = row["ValidFrom"];
                                    if (!string.IsNullOrEmpty(row["ValidTo"].ToString2()))
                                        row_uepp_Ryzs["zsyxzrq"] = row["ValidTo"];

                                    row_uepp_Ryzs["DataState"] = "0";
                                    row_uepp_Ryzs["tag"] = tag;
                                    row_uepp_Ryzs["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                    dataService.Submit_uepp_Ryzs(dt_uepp_Ryzs);


                                    #endregion

                                    #region 人员专业明细 UEPP_Ryzymx

                                    DataTable dt_uepp_Ryzymx = dataService.Get_uepp_Ryzymx(sfzh);
                                    DataRow row_uepp_Ryzymx;
                                    if (dt_uepp_Ryzymx.Rows.Count == 0)
                                    {
                                        row_uepp_Ryzymx = dt_uepp_Ryzymx.NewRow();
                                        dt_uepp_Ryzymx.Rows.Add(row_uepp_Ryzymx);
                                        row_uepp_Ryzymx["ryID"] = sfzh;
                                    }
                                    else
                                    {
                                        row_uepp_Ryzymx = dt_uepp_Ryzymx.Rows[0];
                                    }

                                    if (row["Grade"].ToString2().Equals("一级") || row["Grade"].ToString2().Equals("二级"))
                                    {
                                        row_uepp_Ryzymx["ryzyzglxID"] = "1";
                                        row_uepp_Ryzymx["ryzyzglx"] = "注册建造师";


                                        row_uepp_Ryzymx["ryzslxID"] = "11";
                                        row_uepp_Ryzymx["ryzslx"] = "注册建造师资格证";
                                        if (row["Grade"].ToString2().Equals("一级"))
                                        {
                                            row_uepp_Ryzymx["zyzgdjID"] = "1";
                                            row_uepp_Ryzymx["zyzgdj"] = "壹级";
                                        }
                                        else
                                        {

                                            row_uepp_Ryzymx["zyzgdjID"] = "2";
                                            row_uepp_Ryzymx["zyzgdj"] = "贰级";
                                        }
                                    }
                                    else
                                    {
                                        row_uepp_Ryzymx["ryzyzglxID"] = "2";
                                        row_uepp_Ryzymx["ryzyzglx"] = "小型项目管理师";
                                        row_uepp_Ryzymx["ryzslxID"] = "21";
                                        row_uepp_Ryzymx["ryzslx"] = "小型项目管理师资格证";
                                        row_uepp_Ryzymx["zyzgdjID"] = "1";
                                        row_uepp_Ryzymx["zyzgdj"] = "小型";
                                    }

                                    row_uepp_Ryzymx["zzbz"] = "主项";
                                    row_uepp_Ryzymx["DataState"] = "0";
                                    row_uepp_Ryzymx["tag"] = tag;
                                    row_uepp_Ryzymx["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                    dataService.Submit_uepp_Ryzymx(dt_uepp_Ryzymx);
                                    #endregion

                                    #region 企业人员执业资格关系表
                                    DataTable dt_uepp_qyryzyzg = dataService.Get_uepp_qyryzyzg_zcjzs(row["FullNoc"].ToString2(), sfzh);
                                    DataRow row_uepp_qyryzyzg;
                                    if (dt_uepp_qyryzyzg.Rows.Count == 0)
                                    {
                                        row_uepp_qyryzyzg = dt_uepp_qyryzyzg.NewRow();
                                        dt_uepp_qyryzyzg.Rows.Add(row_uepp_qyryzyzg);
                                        row_uepp_qyryzyzg["qyID"] = row["FullNoc"].ToString2();
                                        row_uepp_qyryzyzg["ryID"] = sfzh;
                                    }
                                    else
                                    {
                                        row_uepp_qyryzyzg = dt_uepp_qyryzyzg.Rows[0];
                                    }
                                    row_uepp_qyryzyzg["ryzyzglxID"] = row_uepp_Ryzyzg["ryzyzglxID"].ToString2();
                                    row_uepp_qyryzyzg["ryzyzglx"] = row_uepp_Ryzyzg["ryzyzglx"].ToString2();
                                    row_uepp_qyryzyzg["DataState"] = "0";
                                    row_uepp_qyryzyzg["tag"] = tag;
                                    row_uepp_qyryzyzg["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                    dataService.Submit_uepp_qyry(dt_uepp_qyryzyzg);
                                    #endregion

                                    scope.Complete();

                                    row_SaveDataLog["SaveState"] = 1;
                                    row_SaveDataLog["Msg"] = "";

                                    dt_saveToStLog2.Rows[0]["SbToZxState"] = "0";
                                    dt_saveToStLog2.Rows[0]["SbToZxMsg"] = "";

                                }
                                catch (Exception ex)
                                {
                                    Public.WriteLog("获取注册建造师，小型项目管理师等人员信息时出现异常:sfzh:" + sfzh + ",Message:" + ex.Message);

                                    row_SaveDataLog["SaveState"] = 0;
                                    row_SaveDataLog["Msg"] = ex.Message;

                                    dt_saveToStLog2.Rows[0]["SbToZxState"] = "1";
                                    dt_saveToStLog2.Rows[0]["SbToZxMsg"] = ex.Message;
                                }
                            }
                            dataService.Submit_SaveToStLog2(dt_saveToStLog2);
                            dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                        }

                        if (dt_SaveDataLog.Rows.Count > 0)
                            dataService.Submit_SaveDataLog(dt_SaveDataLog);

                        row_DataJkDataDetail_zcjzs["allCount"] = allCount_zcjzs;
                        row_DataJkDataDetail_zcjzs["successCount"] = success_count_zcjzs;
                        row_DataJkDataDetail_zcjzs["IsOk"] = 1;
                        row_DataJkDataDetail_zcjzs["ErrorMsg"] = "";

                        if (dt_DataJkDataDetail_zcjzs.Rows.Count > 0)
                            dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_zcjzs);
                    }
                    catch (Exception ex)
                    {
                        row_DataJkDataDetail_zcjzs["allCount"] = allCount_zcjzs;
                        row_DataJkDataDetail_zcjzs["successCount"] = success_count_zcjzs;
                        row_DataJkDataDetail_zcjzs["IsOk"] = 0;
                        row_DataJkDataDetail_zcjzs["ErrorMsg"] = "获取注册建造师人员信息时出现异常:" + ex.Message;

                        if (dt_DataJkDataDetail_zcjzs.Rows.Count > 0)
                            dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_zcjzs);
                    }

                    #endregion

                    Public.WriteLog("更新注册建造师、小型项目管理师信息完成");
                }
                catch (Exception ex)
                {
                    Public.WriteLog(ex.Message + "," + ex.StackTrace);
                    apiMessage += ex.Message;
                }

                finally
                {
                    DataTable dtapicb = dataService.GetSchema_API_cb();
                    DataRow row_apicb = dtapicb.NewRow();
                    dtapicb.Rows.Add(row_apicb);
                    row_apicb["apiCbID"] = dataService.Get_apiCbNewID();
                    row_apicb["apiFlow"] = "10";
                    row_apicb["apiMethod"] = "Get_Contractors;Get_SafetyOfficers;Get_Employees;Get_Constructors";
                    row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                    row_apicb["apiDyMessage"] = apiMessage;
                    row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dataService.Submit_API_cb(dtapicb);

                    dataService.UpdateZbJkzt("10", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

                }
            }
        }

        /// <summary>
        /// 省外企业信息
        /// </summary>
        /// <param name="DataJkLogID"></param>
        void YourTask_PullDataFromSxxzx_Swqyxx(string DataJkLogID)
        {
            DateTime beginTime = DateTime.Now;
            Public.WriteLog("开始执行YourTask_PullDataFromSxxzx_Swqyxx方法：" + beginTime.ToString("yyyy-MM-dd HH:mm:ss"));
            string tag = Tag.江苏建设公共基础数据平台.ToString();
            string userID = "wxszjj01";
            DataService dataService = new DataService();
            DataTable dt_xzqhdm = dataService.Get_tbXzqdmDic();
            XmlHelper helper = new XmlHelper();

            //DataDownService.dataDownService dataDownService = new DataDownService.dataDownService();
            DataRow row;

            //往数据监控日志表项添加一条记录
            DataTable dt_DataJkDataDetail_qyxx = dataService.GetSchema_DataJkDataDetail();
            long Id_DataJkDataDetail_qyxx = dataService.Get_DataJkDataDetailNewID().ToInt64();

            int allCount_qyxx = 0, success_count_qyxx = 0;
            DataRow row_DataJkDataDetail_qyxx = dt_DataJkDataDetail_qyxx.NewRow();
            dt_DataJkDataDetail_qyxx.Rows.Add(row_DataJkDataDetail_qyxx);

            row_DataJkDataDetail_qyxx["ID"] = Id_DataJkDataDetail_qyxx++;
            row_DataJkDataDetail_qyxx["DataJkLogID"] = DataJkLogID;
            row_DataJkDataDetail_qyxx["tableName"] = "UEPP_Qyjbxx";
            row_DataJkDataDetail_qyxx["MethodName"] = "getOutCorpInfo";
            row_DataJkDataDetail_qyxx["bz"] = "从江苏建设公共基础数据平台拉取省外企业信息";
            try
            {
                DataTable dt_SaveDataLog = dataService.GetSchema_SaveDataLog();

                NewDataService.NewDataService newDataService = new WxjzgcjczyTimerService.NewDataService.NewDataService();

                //string beginDate = "2016-03-11";
                //string endDate = DateTime.Now.ToString("yyyy-MM-dd");

                byte[] bytes;
                int index;
                string result;

                //bytes = newDataService.getPersonRegCert(userID, "320200", "", "", "0");
                //result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);

                #region  获取省外企业信息
                bytes = newDataService.getOutCorpInfo(userID, "0");
                result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);


                index = result.IndexOf("<ReturnInfo>");

                if (index >= 0)
                {
                    string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
                    if (string.IsNullOrEmpty(returnResult))
                    {
                        return;
                    }
                    ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
                    if (!returnInfo.Status)
                    {
                        return;
                    }
                }

                index = result.IndexOf("<OutCorpBasicInfo>");
                if (index >= 0)
                {
                    string corpBasicInfoString = result.Substring(index, result.LastIndexOf("</OutCorpBasicInfo>") - index + 19);
                    OutCorpBasicInfoBody outCorpBasicInfoBody = helper.DeserializeXML<OutCorpBasicInfoBody>("<OutCorpBasicInfoBody><OutCorpBasicInfoArray>" + corpBasicInfoString + "</OutCorpBasicInfoArray></OutCorpBasicInfoBody>");
                    if (outCorpBasicInfoBody != null)
                    {
                        foreach (OutCorpBasicInfo corpBasicInfo in outCorpBasicInfoBody.array)
                        {
                            //Public.WriteLog("corpBasicInfo.CorpCode：" + corpBasicInfo.CorpCode);
                            if (corpBasicInfo.CorpCode.Length == 9)
                            {
                                corpBasicInfo.CorpCode = corpBasicInfo.CorpCode.Substring(0, 8) + '-' + corpBasicInfo.CorpCode.Substring(8, 1);
                            }
                            //检查该企业是否存在社会信用代码，若存在，则转化为社会信用代码
                            if (corpBasicInfo.CorpCode.Length == 10)
                            {
                                string qyShxydm = dataService.Get_UEPP_Qyjbxx_Shxydm(corpBasicInfo.CorpCode);
                                if (!string.IsNullOrEmpty(qyShxydm))
                                {
                                    corpBasicInfo.CorpCode = qyShxydm;

                                    string sql = "update  UEPP_Qyzs set qyID=@qyIDNew where qyID=@qyID;update UEPP_Qyzzmx set qyID=@qyIDNew where qyID=@qyID;update  UEPP_Qycsyw set qyID=@qyIDNew where qyID=@qyID";
                                    SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                    sp.Add("@qyID", corpBasicInfo.CorpCode);
                                    sp.Add("@qyIDNew", qyShxydm);
                                    dataService.ExecuteNonQuerySql2(sql, sp);
                                }
                            }

                            DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                            row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_qyxx["ID"];
                            row_SaveDataLog["DataXml"] = "";
                            row_SaveDataLog["PKID"] = corpBasicInfo.CorpCode;
                            row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");
                            dt_SaveDataLog.Rows.Add(row_SaveDataLog);

                            try
                            {
                                #region  更新企业基本信息

                                DataTable dt = dataService.Get_uepp_Qyjbxx(corpBasicInfo.CorpCode);
                                if (dt.Rows.Count == 0)
                                {
                                    row = dt.NewRow();
                                    dt.Rows.Add(row);
                                    row["tyshxydm"] = corpBasicInfo.LicenseNo;
                                }
                                else
                                {
                                    row = dt.Rows[0];
                                    if (!string.IsNullOrEmpty(corpBasicInfo.UpdateDate) && !string.IsNullOrEmpty(row["xgrqsj"].ToString2()))
                                        if (DateTime.Parse(row["xgrqsj"].ToString2()).ToString("yyyy-MM-dd") == DateTime.Parse(corpBasicInfo.UpdateDate).ToString("yyyy-MM-dd"))
                                        {
                                            continue;
                                        }
                                }
                                row["qyID"] = corpBasicInfo.CorpCode;
                                row["zzjgdm"] = corpBasicInfo.CorpCode;
                                row["tag"] = tag;

                                row["qymc"] = corpBasicInfo.CorpName;
                                row["yyzzzch"] = corpBasicInfo.LicenseNo;
                               

                                if (!string.IsNullOrEmpty(corpBasicInfo.ProvinceCode.ToString2()))
                                {
                                    SqlParameterCollection sp = dataService.CreateSqlParameterCollection();

                                    row["Province"] = corpBasicInfo.ProvinceCode;
                                    sp.Add("@CodeInfo", corpBasicInfo.ProvinceCode.ToString2());
                                    string provinceCode = dataService.ExecuteSql("select top 1 Code from  UEPP_Code where CodeType='城市地区' and  CodeInfo=@CodeInfo", sp);
                                    if (!string.IsNullOrEmpty(provinceCode))
                                    {
                                        row["ProvinceID"] = provinceCode;
                                        sp.Clear();
                                        if (!string.IsNullOrEmpty(corpBasicInfo.CityCode.ToString2()))
                                        {
                                            row["City"] = corpBasicInfo.CityCode;

                                            sp.Add("@CodeInfo", corpBasicInfo.CityCode.ToString2());
                                            sp.Add("@parentCode", provinceCode);
                                            string cityCode = dataService.ExecuteSql("select top 1 Code from  UEPP_Code where CodeType='城市地区' and ParentCode=@parentCode and  CodeInfo=@CodeInfo", sp);
                                            if (!string.IsNullOrEmpty(cityCode))
                                            {
                                                row["CityID"] = cityCode;
                                            }

                                            sp.Clear();
                                            if (!string.IsNullOrEmpty(corpBasicInfo.CountyCode.ToString2()))
                                            {
                                                row["County"] = corpBasicInfo.CountyCode;

                                                sp.Add("@CodeInfo", corpBasicInfo.CountyCode.ToString2());
                                                sp.Add("@parentCode", cityCode);
                                                string countyCode = dataService.ExecuteSql("select top 1 Code from  UEPP_Code where CodeType='城市地区' and ParentCode=@parentCode and  CodeInfo=@CodeInfo", sp);
                                                if (!string.IsNullOrEmpty(countyCode))
                                                {
                                                    row["CountyID"] = countyCode;
                                                }

                                            }
                                        }
                                    }
                                }

                                row["zcdd"] = corpBasicInfo.RegAddress;
                                row["xxdd"] = corpBasicInfo.CorpAddress;
                                if (!string.IsNullOrEmpty(corpBasicInfo.FoundDate))
                                    row["clrq"] = corpBasicInfo.FoundDate;
                                row["jjxz"] = corpBasicInfo.CorpTypeDesc.ToString2().Trim();
                                if (!string.IsNullOrEmpty(corpBasicInfo.CorpTypeDesc))
                                {
                                    SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                    sp.Add("@CodeInfo", corpBasicInfo.CorpTypeDesc.ToString2().Trim());
                                    string jjxzID = dataService.ExecuteSql("select * from  UEPP_Code where CodeType='企业经济性质' and  CodeInfo=@CodeInfo", sp);
                                    if (!string.IsNullOrEmpty(jjxzID))
                                    {
                                        row["jjxzID"] = jjxzID;
                                    }
                                }

                                row["zczb"] = corpBasicInfo.RegCapital;
                                row["cz"] = corpBasicInfo.Fax;
                                row["lxdh"] = corpBasicInfo.LinkPhone;
                                row["fddbr"] = corpBasicInfo.LegalMan;

                                if (!string.IsNullOrEmpty(corpBasicInfo.UpdateDate))
                                    row["xgrqsj"] = corpBasicInfo.UpdateDate;
                                else
                                    row["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd");

                                row["DataState"] = 0;
                                allCount_qyxx++;
                                if (!dataService.Submit_uepp_qyjbxx(dt))
                                {
                                    row_SaveDataLog["SaveState"] = 0;
                                    row_SaveDataLog["Msg"] = "从江苏建设公共基础数据平台获取的单位：" + corpBasicInfo.CorpName + "," + corpBasicInfo.CorpCode + "，基本信息保存失败！";
                                    continue;
                                }
                                else
                                {
                                    success_count_qyxx++;
                                    row_SaveDataLog["SaveState"] = 1;
                                    row_SaveDataLog["Msg"] = "";
                                }
                                #endregion
                            }
                            catch (Exception ex)
                            {
                                row_SaveDataLog["SaveState"] = 0;
                                row_SaveDataLog["Msg"] = ex.Message;
                            }
                        }
                    }
                }
                #endregion

                #region 获取企业资质信息
                bytes = newDataService.getOutCorpQual(userID, "0");

                result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                //Public.WriteLog("省外企业资质信息.txt",  result);

                index = result.IndexOf("<ReturnInfo>");

                if (index >= 0)
                {
                    string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
                    if (string.IsNullOrEmpty(returnResult))
                    {
                        return;
                    }
                    ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
                    if (!returnInfo.Status)
                    {
                        return;
                    }
                }

                #region 更新企业资质(TCorpCertQual)
                index = result.IndexOf("<OutCorpCertQual>");
                if (index >= 0)
                {
                    string returnResult = result.Substring(index, result.LastIndexOf("</OutCorpCertQual>") - index + 18);
                    if (string.IsNullOrEmpty(returnResult))
                    {
                        return;
                    }
                    OutCorpCertQualBody outCorpCertQualBody = helper.DeserializeXML<OutCorpCertQualBody>("<OutCorpCertQualBody><OutCorpCertQualArray>" + returnResult + "</OutCorpCertQualArray></OutCorpCertQualBody>");

                    
                    if (outCorpCertQualBody != null)
                    {
                        foreach (OutCorpCertQual corpCertQual in outCorpCertQualBody.array)
                        {
                            try
                            {
                                if (corpCertQual.CorpCode.Length == 9)
                                {
                                    corpCertQual.CorpCode = corpCertQual.CorpCode.Substring(0, 8) + '-' + corpCertQual.CorpCode.Substring(8, 1);
                                }
                                //检查该企业是否存在社会信用代码，若存在，则转化为社会信用代码
                                if (corpCertQual.CorpCode.Length == 10)
                                {
                                    string qyShxydm = dataService.Get_UEPP_Qyjbxx_Shxydm(corpCertQual.CorpCode);
                                    if (!string.IsNullOrEmpty(qyShxydm))
                                    {
                                        corpCertQual.CorpCode = qyShxydm;

                                        string sql = "update  UEPP_Qyzs set qyID=@qyIDNew where qyID=@qyID;update UEPP_Qyzzmx set qyID=@qyIDNew where qyID=@qyID;update  UEPP_Qycsyw set qyID=@qyIDNew where qyID=@qyID";
                                        SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                        sp.Add("@qyID", corpCertQual.CorpCode);
                                        sp.Add("@qyIDNew", qyShxydm);
                                        dataService.ExecuteNonQuerySql2(sql, sp);
                                    }
                                }

                                //string csywlxID = "", csywlx = "";
                                Qyxx qycsywlx = getCsywlx(corpCertQual.CertType);
                                if (string.IsNullOrEmpty(qycsywlx.csywlxID))
                                    continue;

                                #region 企业从事业务类型

                                DataTable dt_qycsyw = dataService.Get_uepp_Qycsyw_sjsgyth(corpCertQual.CorpCode, qycsywlx.csywlxID);

                                DataRow tempRow_qycsyw;

                                if (dt_qycsyw.Rows.Count == 0)
                                {
                                    tempRow_qycsyw = dt_qycsyw.NewRow();
                                    dt_qycsyw.Rows.Add(tempRow_qycsyw);
                                    tempRow_qycsyw["qyID"] = corpCertQual.CorpCode;
                                }
                                else
                                {
                                    tempRow_qycsyw = dt_qycsyw.Rows[0];
                                }

                                tempRow_qycsyw["csywlxID"] = qycsywlx.csywlxID;
                                tempRow_qycsyw["csywlx"] = qycsywlx.csywlx;

                                tempRow_qycsyw["balxID"] = "1";
                                tempRow_qycsyw["balx"] = "长期备案";
                                tempRow_qycsyw["DataState"] = "0";
                                tempRow_qycsyw["tag"] = tag;

                                if (!string.IsNullOrEmpty(corpCertQual.UpdateDate))
                                    tempRow_qycsyw["xgrqsj"] = corpCertQual.UpdateDate;
                                else
                                    tempRow_qycsyw["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                if (dt_qycsyw.Rows.Count > 0)
                                {
                                    dataService.Submit_uepp_qycsyw(dt_qycsyw);
                                }
                                #endregion

                                DataTable dt_jsdw_zzmx = dataService.Get_uepp_zzmxxx_qyxx_nostatus(corpCertQual.CorpCode, qycsywlx.csywlxID);

                                int rowIndex = -1;

                                for (int i = 0; i < dt_jsdw_zzmx.Rows.Count; i++)
                                {
                                    //if (dt_jsdw_zzmx.Rows[i].RowState == DataRowState.Deleted)
                                    //    continue;

                                    if (qycsywlx.csywlxID == dt_jsdw_zzmx.Rows[i]["csywlxID"].ToString2()
                                         && corpCertQual.CertCode == dt_jsdw_zzmx.Rows[i]["zsbh"].ToString2()
                                        && (
                                         corpCertQual.TradeType == "工程勘察综合类" && dt_jsdw_zzmx.Rows[i]["zzxl"].ToString2() == "综合类"
                                         || corpCertQual.TradeType == "工程勘察专业类" && dt_jsdw_zzmx.Rows[i]["zzxl"].ToString2() == "专业类"
                                         || corpCertQual.TradeType == "工程勘察劳务类" && dt_jsdw_zzmx.Rows[i]["zzxl"].ToString2() == "劳务类"
                                         || corpCertQual.TradeType == dt_jsdw_zzmx.Rows[i]["zzxl"].ToString2()
                                        )
                                        && (dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "岩土工程勘察" && corpCertQual.MajorType == "岩土工程（勘察）"
                                        || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "岩土工程设计" && corpCertQual.MajorType == "岩土工程（设计）"
                                          || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "岩土工程测试、监测、检测" && corpCertQual.MajorType == "岩土工程（物探测试检测监测）"
                                          || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "岩土工程咨询、监理" && corpCertQual.MajorType == "岩土工程（咨询监理）"

                                          || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "建筑装修装饰" && corpCertQual.MajorType == "建筑装饰装修工程"
                                          || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "模板作业" && corpCertQual.MajorType == "模板作业分包"
                                          || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "脚手架搭设作业" && corpCertQual.MajorType == "脚手架作业分包"
                                          || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == corpCertQual.MajorType
                                        )
                                        )
                                    {
                                        rowIndex = i;
                                        break;
                                    }
                                }

                                if (rowIndex < 0)
                                {
                                    row = dt_jsdw_zzmx.NewRow();
                                    dt_jsdw_zzmx.Rows.Add(row);
                                    row["ID"] = dataService.Get_uepp_qyxxmx_NewID();
                                    row["qyID"] = corpCertQual.CorpCode;
                                    row["csywlx"] = qycsywlx.csywlx;
                                    row["csywlxID"] = qycsywlx.csywlxID;
                                }
                                else
                                {
                                    row = dt_jsdw_zzmx.Rows[rowIndex];
                                    if ("qlmsoft".Equals(row["xgr"].ToString2()))
                                    {
                                        //如果是手动修改的数据，则不更新，如果需要更新，则把xgr栏设置为空
                                        continue;
                                    }
                                    if (!string.IsNullOrEmpty(corpCertQual.UpdateDate) && !string.IsNullOrEmpty(row["xgrqsj"].ToString2()))
                                    {
                                        int cmpFlag = DateTime.Compare(DateTime.Parse(corpCertQual.UpdateDate), row["xgrqsj"].ToDateTime());
                                        //if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(corpCertQual.UpdateDate).ToString("yyyy-MM-dd"))
                                        if (cmpFlag < 0)
                                        {
                                            continue;
                                        }
                                    }
                                }
                                if (corpCertQual.IsMaster == "主项")
                                    row["zzbz"] = "主项";
                                else
                                    row["zzbz"] = "增项";

                                if (corpCertQual.TradeType == "工程勘察综合类")
                                {
                                    row["zzxl"] = "综合类";
                                    row["zzxlID"] = "9";
                                }
                                else
                                    if (corpCertQual.TradeType == "工程勘察专业类")
                                    {
                                        row["zzxl"] = "专业类";
                                        row["zzxlID"] = "10";
                                    }
                                    else
                                        if (corpCertQual.TradeType == "工程勘察劳务类")
                                        {
                                            row["zzxl"] = "劳务类";
                                            row["zzxlID"] = "11";
                                        }
                                        else
                                            if (corpCertQual.TradeType == "工程设计综合")
                                            {
                                                row["zzxl"] = "综合资质";
                                                row["zzxlID"] = "12";
                                            }
                                            else
                                            {
                                                row["zzxl"] = corpCertQual.TradeType;
                                                if (!string.IsNullOrEmpty(qycsywlx.csywlxID))
                                                {
                                                    string sql = @"select Code from UEPP_Code where  CodeType='企业资质序列' and ParentCodeType='企业从事业务类型'
 and ParentCode=@parentCode and CodeInfo=@CodeInfo ";
                                                    SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                                    sp.Add("@CodeInfo", corpCertQual.TradeType);
                                                    sp.Add("@parentCode", qycsywlx.csywlxID);
                                                    string zzxlID = dataService.ExecuteSql(sql, sp);
                                                    if (!string.IsNullOrEmpty(zzxlID))
                                                        row["zzxlID"] = zzxlID;
                                                }
                                            }

                                if (corpCertQual.MajorType == "岩土工程（勘察）")
                                {
                                    row["zzlb"] = "岩土工程勘察";
                                    row["zzlbID"] = "300";
                                }
                                else
                                    if (corpCertQual.MajorType == "岩土工程（设计）")
                                    {
                                        row["zzlb"] = "岩土工程设计";
                                        row["zzlbID"] = "301";
                                    }
                                    else
                                        if (corpCertQual.MajorType == "岩土工程（物探测试检测监测）")
                                        {
                                            row["zzlb"] = "岩土工程测试、监测、检测";
                                            row["zzlbID"] = "302";
                                        }
                                        else
                                            if (corpCertQual.MajorType == "岩土工程（咨询监理））")
                                            {
                                                row["zzlb"] = "岩土工程咨询、监理";
                                                row["zzlbID"] = "303";
                                            }
                                            else
                                                if (corpCertQual.TradeType == "建筑装饰装修工程")
                                                {
                                                    row["zzlb"] = "建筑装修装饰";
                                                    row["zzlbID"] = "33";
                                                }
                                                else if (corpCertQual.TradeType == "模板作业分包")
                                                {
                                                    row["zzlb"] = "模板作业";
                                                    row["zzlbID"] = "128";
                                                }
                                                else
                                                    if (corpCertQual.TradeType == "脚手架作业分包")
                                                    {
                                                        row["zzlb"] = "脚手架搭设作业";
                                                        row["zzlbID"] = "127";
                                                    }
                                                    else
                                                    {
                                                        row["zzlb"] = corpCertQual.MajorType;
                                                        if (!string.IsNullOrEmpty(row["zzxlID"].ToString2().Trim()))
                                                        {
                                                            string sql = @"select Code from UEPP_Code where  CodeType='企业资质类别' and  ParentCodeType='企业资质序列'
 and ParentCode=@parentCode and (CodeInfo=@CodeInfo or CodeInfo=@CodeInfo1) ";
                                                            SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                                            sp.Add("@CodeInfo", corpCertQual.MajorType);
                                                            sp.Add("@parentCode", row["zzxlID"]);
                                                            sp.Add("@CodeInfo1", corpCertQual.MajorType.ToString2().Replace("分包", ""));
                                                            string zzlbID = dataService.ExecuteSql(sql, sp);
                                                            if (!string.IsNullOrEmpty(zzlbID))
                                                                row["zzlbID"] = zzlbID;
                                                        }
                                                    }




                                row["zzdj"] = corpCertQual.TitleLevel;
                                //新增证书跟资质的一对多关联关系
                                row["zsbh"] = corpCertQual.CertCode;

                                if (!string.IsNullOrEmpty(corpCertQual.TitleLevel))
                                {
                                    SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                    sp.Add("@CodeInfo", corpCertQual.TitleLevel);

                                    string sql = "select Code from UEPP_Code  where  CodeType='企业资质等级' and ParentCodeType='企业资质序列' and CodeInfo=@CodeInfo ";
                                    string zzdjID = dataService.ExecuteSql(sql, sp);
                                    if (!string.IsNullOrEmpty(zzdjID))
                                        row["zzdjID"] = zzdjID;
                                    else
                                    {
                                        // 新增代码表
                                    }
                                }

                                if (corpCertQual.Status == "0")
                                {
                                    row["DataState"] = -1;
                                }
                                else
                                {
                                    row["DataState"] = 0;
                                }

                                row["tag"] = tag;
                                row["xgrqsj"] = corpCertQual.UpdateDate;

                                if (!dataService.Submit_uepp_qyzzmx(dt_jsdw_zzmx))
                                {
                                    Public.WriteLog("单位ID：" + corpCertQual.CorpCode + "，企业资质保存失败！");
                                }
                            }
                            catch (Exception ex)
                            {
                                Public.WriteLog("保存企业资质时出现异常：" + ex.Message);
                            }
                        }
                    }

                }
                #endregion

                #region 更新企业资质证书信息(OutCorpCertInfo)

                index = result.IndexOf("<OutCorpCertInfo>");
                if (index >= 0)
                {
                    string outCorpCertInfoResult = result.Substring(index, result.LastIndexOf("</OutCorpCertInfo>") - index + 18);
                    if (string.IsNullOrEmpty(outCorpCertInfoResult))
                    {
                        return;
                    }
                    OutCorpCertInfoBody outCorpCertInfoBody = helper.DeserializeXML<OutCorpCertInfoBody>("<OutCorpCertInfoBody><OutCorpCertInfoArray>" + outCorpCertInfoResult + "</OutCorpCertInfoArray></OutCorpCertInfoBody>");
                    if (outCorpCertInfoBody != null)
                    {
                        foreach (OutCorpCertInfo corpCertInfo in outCorpCertInfoBody.certArray)
                        {
                            try
                            {
                                if (corpCertInfo.CorpCode.Length == 9)
                                {
                                    corpCertInfo.CorpCode = corpCertInfo.CorpCode.Substring(0, 8) + '-' + corpCertInfo.CorpCode.Substring(8, 1);
                                }
                                //检查该企业是否存在社会信用代码，若存在，则转化为社会信用代码
                                if (corpCertInfo.CorpCode.Length == 10)
                                {
                                    string qyShxydm = dataService.Get_UEPP_Qyjbxx_Shxydm(corpCertInfo.CorpCode);
                                    if (!string.IsNullOrEmpty(qyShxydm))
                                    {
                                        corpCertInfo.CorpCode = qyShxydm;
                                    }
                                }

                                int rowIndex = -1;
                                //string csywlxID = "", csywlx = "";
                                Qyxx qyxx = getCsywlx(corpCertInfo.CertType);
                                if (string.IsNullOrEmpty(qyxx.csywlxID))
                                    continue;
                                //特殊处理国基建设集团有限公司错误的安全生产许可证
                                if ("(晋)JZ安许证字[2011]00045843".Equals(corpCertInfo.CertCode))
                                {
                                    continue;
                                }

                                DataTable dt_qy_zzzs = dataService.Get_uepp_zzzsxx_qyxx(corpCertInfo.CorpCode);

                                for (int i = 0; i < dt_qy_zzzs.Rows.Count; i++)
                                {
                                    //if (dt_jsdw_zzzs.Rows[i].RowState == DataRowState.Deleted)
                                    //    continue;

                                    if (qyxx.csywlx == dt_qy_zzzs.Rows[i]["csywlx"].ToString2()
                                        && dt_qy_zzzs.Rows[i]["zsbh"].ToString2() == corpCertInfo.CertCode)
                                    {
                                        rowIndex = i;
                                        break;
                                    }
                                }

                                if (rowIndex < 0)
                                {
                                    row = dt_qy_zzzs.NewRow();
                                    dt_qy_zzzs.Rows.Add(row);
                                    row["zsjlId"] = dataService.Get_uepp_qyQyzs_NewID();
                                }
                                else
                                {
                                    row = dt_qy_zzzs.Rows[rowIndex];
                                    if ("qlmsoft".Equals(row["xgr"].ToString2()))
                                    {
                                        //如果是手动修改的数据，则不更新，如果需要更新，则把xgr栏设置为空
                                        continue;
                                    }
                                    if (!string.IsNullOrEmpty(corpCertInfo.UpdateDate))
                                        if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(corpCertInfo.UpdateDate).ToString("yyyy-MM-dd"))
                                        {
                                            continue;
                                        }
                                }
                                row["qyID"] = corpCertInfo.CorpCode;
                                row["csywlx"] = qyxx.csywlx;
                                row["csywlxID"] = qyxx.csywlxID;

                                //if (!string.IsNullOrEmpty(corpCertInfo.CertType))
                                //{
                                //    SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                //    sp.Add("@parentCode", csywlxID);
                                //    sp.Add("@CodeInfo", corpCertInfo.CertType);

                                //    string sql = @"select * from UEPP_Code  where  CodeType ='企业证书类型' and ParentCodeType='企业从事业务类型' and ParentCode=@parentCode and CodeInfo=@CodeInfo ";
                                //    string zslxID = dataService.ExecuteSql(sql, sp);
                                //    if (!string.IsNullOrEmpty(zslxID))
                                //    {
                                //        row["zslxID"] = zslxID;
                                //    }
                                //}
                                //row["zslx"] = "开发企业资质证书";

                                row["sfzzz"] = "1";
                                //string zslx = "", zslxID = "";
                                Qyxx qyzslx = getZslx(corpCertInfo.CertType);
                                row["zslxID"] = qyzslx.zslxID;
                                row["zslx"] = qyzslx.zslx;
                                row["zsbh"] = corpCertInfo.CertCode;
                                if (!string.IsNullOrEmpty(corpCertInfo.ValidDate.Trim()))
                                    row["zsyxzrq"] = corpCertInfo.ValidDate;
                                if (!string.IsNullOrEmpty(corpCertInfo.IssueDate.Trim()))
                                {
                                    row["fzrq"] = corpCertInfo.IssueDate;
                                    row["zsyxqrq"] = corpCertInfo.IssueDate;
                                }

                                row["fzdw"] = corpCertInfo.IssueOrgan;
                                row["xgrqsj"] = corpCertInfo.UpdateDate;
                                row["xgr"] = "定时服务";
                                row["tag"] = tag;
                                row["DataState"] = 0;

                                if (!dataService.Submit_uepp_qyzzzs(dt_qy_zzzs))
                                {
                                    Public.WriteLog("单位ID：" + corpCertInfo.CorpCode + "，企业资质证书信息保存失败！");
                                }

                            }
                            catch (Exception ex)
                            {
                                Public.WriteLog("保存企业资质证书信息时出现异常：" + corpCertInfo.CorpCode + ex.Message);
                            }
                        }
                    }
                }
                
                #endregion

                DateTime endTime = DateTime.Now;
                TimeSpan span = compareDateTime(beginTime, endTime);
                Public.WriteLog(string.Format("结束YourTask_PullDataFromSxxzx_Swqyxx方法:{0}，历时：{1}时{2}分{3}秒", endTime.ToString("yyyy-MM-dd HH:mm:ss"), span.Hours, span.Minutes, span.Seconds));


                if (dt_SaveDataLog.Rows.Count > 0)
                    dataService.Submit_SaveDataLog(dt_SaveDataLog);

                row_DataJkDataDetail_qyxx["allCount"] = allCount_qyxx;
                row_DataJkDataDetail_qyxx["successCount"] = success_count_qyxx;
                row_DataJkDataDetail_qyxx["IsOk"] = 1;
                row_DataJkDataDetail_qyxx["ErrorMsg"] = "";

                if (dt_DataJkDataDetail_qyxx.Rows.Count > 0)
                    dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_qyxx);

                #endregion

            }
            catch (Exception ex)
            {
                row_DataJkDataDetail_qyxx["allCount"] = allCount_qyxx;
                row_DataJkDataDetail_qyxx["successCount"] = success_count_qyxx;
                row_DataJkDataDetail_qyxx["IsOk"] = 0;
                row_DataJkDataDetail_qyxx["ErrorMsg"] = "从江苏建设公共基础数据平台获取省外企业信息时出现异常:" + ex.Message;

                if (dt_DataJkDataDetail_qyxx.Rows.Count > 0)
                    dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_qyxx);
            }

        }
   

        /// <summary>
        /// 省外人员信息
        /// </summary>
        /// <param name="DataJkLogID"></param>
        void YourTask_PullDataFromSxxzx_Swryxx(string DataJkLogID)
        {
            DateTime beginTime = DateTime.Now;
            int allCount_ryxx = 0, success_count_ryxx = 0;
            DataRow row;
            Public.WriteLog("开始执行YourTask_PullDataFromSxxzx_Swryxx方法：" + beginTime.ToString("yyyy-MM-dd HH:mm:ss"));
            string tag = Tag.江苏建设公共基础数据平台.ToString();
            string userID = "wxszjj01";
            DataService dataService = new DataService();
            XmlHelper helper = new XmlHelper();

            NewDataService.NewDataService newDataService = new WxjzgcjczyTimerService.NewDataService.NewDataService();

            //往数据监控日志表项添加一条记录
            DataTable dt_DataJkDataDetail_ryxx = dataService.GetSchema_DataJkDataDetail();
            long Id_DataJkDataDetail_ryxx = dataService.Get_DataJkDataDetailNewID().ToInt64();

            DataRow row_DataJkDataDetail_ryxx = dt_DataJkDataDetail_ryxx.NewRow();
            dt_DataJkDataDetail_ryxx.Rows.Add(row_DataJkDataDetail_ryxx);

            row_DataJkDataDetail_ryxx["ID"] = Id_DataJkDataDetail_ryxx++;
            row_DataJkDataDetail_ryxx["DataJkLogID"] = DataJkLogID;
            row_DataJkDataDetail_ryxx["tableName"] = "UEPP_Ryjbxx";
            row_DataJkDataDetail_ryxx["MethodName"] = "getOutPersonCert";
            row_DataJkDataDetail_ryxx["bz"] = "从江苏建设公共基础数据平台拉取省外人员信息";

            try
            {
                byte[] bytes = newDataService.getOutPersonCert(userID, "0");
                string result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);

                int index = result.IndexOf("<ReturnInfo>");
                if (index >= 0)
                {
                    string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
                    if (string.IsNullOrEmpty(returnResult))
                    {
                        return;
                    }

                    ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
                    if (!returnInfo.Status)
                    {
                        return;
                    }

                }
                else
                {
                    return;
                }
                //index = result.IndexOf("<OutChiefPerson>");
                //if (index >= 0)
                //{
                //    string outChiefPersonString = result.Substring(index, result.LastIndexOf("</OutChiefPerson>") - index + 17);
                //    OutChiefPersonBody outChiefPersonBody = helper.DeserializeXML<OutChiefPersonBody>("<OutChiefPersonBody><PersonBasicInfoArray>" + outChiefPersonString + "</OutChiefPersonArray></OutChiefPersonBody>");
                //}

                OutConsInfoBody outConsInfoBody = new OutConsInfoBody();
                index = result.IndexOf("<OutConsInfo>");
                if (index >= 0)
                {
                    string outConsInfoString = result.Substring(index, result.LastIndexOf("</OutConsInfo>") - index + 14);
                    outConsInfoBody = helper.DeserializeXML<OutConsInfoBody>("<OutConsInfoBody><OutConsInfoArray>" + outConsInfoString + "</OutConsInfoArray></OutConsInfoBody>");


                    if (outConsInfoBody != null)
                    {
                        int rowIndex = -1;
                        DataTable dt_SaveDataLog = dataService.GetSchema_SaveDataLog();

                        foreach (OutConsInfo outConsInfo in outConsInfoBody.array)
                        {
                            DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                            row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_ryxx["ID"];
                            row_SaveDataLog["DataXml"] = "";
                            row_SaveDataLog["PKID"] = outConsInfo.IDCardNo;
                            row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                            try
                            {
                                #region 人员基本信息
                                DataTable dt_ryxx = dataService.Get_UEPP_Ryjbxx(outConsInfo.IDCardNo);
                                if (dt_ryxx.Rows.Count == 0)
                                {
                                    row = dt_ryxx.NewRow();
                                    dt_ryxx.Rows.Add(row);
                                    row["ryID"] = outConsInfo.IDCardNo;
                                }
                                else
                                {
                                    row = dt_ryxx.Rows[0];
                                }
                                row["tag"] = tag;
                                row["xm"] = outConsInfo.PersonName;
                                row["zjlxID"] = "1";
                                row["zjlx"] = "身份证";
                                row["zjhm"] = outConsInfo.IDCardNo;
                                row["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd");
                                row["xgr"] = "定时服务";
                                row["DataState"] = 0;

                                allCount_ryxx++;
                                if (!dataService.Submit_uepp_ryjbxx(dt_ryxx))
                                {
                                    //Public.WriteLog("建设单位人员信息保存失败，ryID：" + personBasicInfo.IDCardNo);
                                    row_SaveDataLog["SaveState"] = 0;
                                    row_SaveDataLog["Msg"] = "建设单位人员信息保存失败，ryID：" + outConsInfo.IDCardNo;
                                }
                                else
                                {
                                    success_count_ryxx++;

                                    row_SaveDataLog["SaveState"] = 1;
                                    row_SaveDataLog["Msg"] = "";
                                }
                                #endregion

                                dt_SaveDataLog.Rows.Add(row_SaveDataLog);


                                #region 人员执业资格

                                string ryzclbNo = String.Empty, ryzclb = String.Empty, ryzyzglxID = String.Empty, ryzyzglx = String.Empty;
                                string ryzslxID = String.Empty, ryzslx = String.Empty, zyzgdjID = String.Empty, zyzgdj = String.Empty;

                                ryzclbNo = outConsInfo.RegType;

                                switch (ryzclbNo)
                                {
                                    case "1001":
                                        ryzclb = "一级注册建筑师";
                                        ryzyzglxID = "51";
                                        ryzyzglx = "注册建筑师";
                                        ryzslxID = "151";
                                        ryzslx = "注册建筑师资格证";

                                        zyzgdjID = "21";
                                        zyzgdj = "壹级";
                                        break;
                                    case "1002":
                                        ryzclb = "二级注册建筑师";
                                        ryzyzglxID = "51";
                                        ryzyzglx = "注册建筑师";

                                        ryzslxID = "151";
                                        ryzslx = "注册建筑师资格证";
                                        zyzgdjID = "22";
                                        zyzgdj = "贰级";
                                        break;
                                    case "1101":
                                        ryzclb = "一级注册结构工程师";
                                        ryzyzglxID = "61";
                                        ryzyzglx = "注册结构师";
                                        ryzslxID = "161";
                                        ryzslx = "注册结构师资格证";

                                        zyzgdjID = "26";
                                        zyzgdj = "壹级";
                                        break;
                                    case "1102":
                                        ryzclb = "二级注册结构工程师";
                                        ryzyzglxID = "61";
                                        ryzyzglx = "注册结构师";
                                        ryzslxID = "161";
                                        ryzslx = "注册结构师资格证";
                                        zyzgdjID = "27";
                                        zyzgdj = "贰级";
                                        break;
                                    case "1210":
                                        ryzclb = "注册土木工程师（岩土）";
                                        ryzyzglxID = "73";

                                        ryzyzglx = "注册土木工程师（岩土）";
                                        ryzslxID = "731";
                                        ryzslx = "注册土木工程师（岩土）资格证";

                                        break;
                                    case "1220":
                                        ryzclb = "注册土木工程师（港口与航道工程）";
                                        ryzyzglxID = "84";

                                        ryzyzglx = "注册土木工程师（港口与航道工程）";
                                        ryzslxID = "841";
                                        ryzslx = "注册土木工程师（港口与航道工程）资格证";

                                        break;
                                    case "1310":
                                        ryzclb = "注册公用设备工程师（暖通空调）";
                                        ryzyzglxID = "74";

                                        ryzyzglx = "注册公用设备工程师（暖通空调）";
                                        ryzslxID = "741";
                                        ryzslx = "注册公用设备工程师（暖通空调）资格证";
                                        break;
                                    case "1320":
                                        ryzclb = "注册公用设备工程师（给水排水）";
                                        ryzyzglxID = "75";
                                        ryzyzglx = "注册公用设备工程师（给水排水）";
                                        ryzslxID = "751";
                                        ryzslx = "注册公用设备工程师（给水排水）资格证";
                                        break;
                                    case "1330":
                                        ryzclb = "注册公用设备工程师（动力）";
                                        ryzyzglxID = "76";

                                        ryzyzglx = "注册公用设备工程师（动力）";
                                        ryzslxID = "761";
                                        ryzslx = "注册公用设备工程师（动力）资格证";
                                        break;
                                    case "1410":
                                        ryzclb = "注册电气工程师（发输变电）";
                                        ryzyzglxID = "77";

                                        ryzyzglx = "注册电气工程师（发输变电）";
                                        ryzslxID = "771";
                                        ryzslx = "注册电气工程师（发输变电）资格证";
                                        break;
                                    case "1420":
                                        ryzclb = "注册电气工程师（供配电）";
                                        ryzyzglxID = "78";
                                        ryzyzglx = "注册电气工程师（供配电）";
                                        ryzslxID = "781";
                                        ryzslx = "注册电气工程师（供配电）资格证";
                                        break;
                                    case "1511":
                                        ryzclb = "一级注册建造师";
                                        ryzyzglxID = "1";
                                        ryzyzglx = "注册建造师";
                                        ryzslxID = "11";
                                        ryzslx = "注册建造师资格证";

                                        zyzgdjID = "1";
                                        zyzgdj = "壹级";

                                        break;
                                    case "1512":
                                        ryzclb = "二级注册建造师";
                                        ryzyzglxID = "1";
                                        ryzyzglx = "注册建造师";
                                        ryzslxID = "11";
                                        ryzslx = "注册建造师资格证";

                                        zyzgdjID = "2";
                                        zyzgdj = "贰级";

                                        break;
                                    case "1521":
                                        ryzclb = "注册建造师（一级临时）";
                                        ryzyzglxID = "1";
                                        ryzyzglx = "注册建造师";
                                        ryzslxID = "11";
                                        ryzslx = "注册建造师资格证";

                                        zyzgdjID = "3";
                                        zyzgdj = "壹级临时";
                                        break;
                                    case "1522":
                                        ryzclb = "注册建造师（二级临时）";
                                        ryzyzglxID = "1";

                                        ryzyzglx = "注册建造师";
                                        ryzslxID = "11";
                                        ryzslx = "注册建造师资格证";

                                        zyzgdjID = "4";
                                        zyzgdj = "贰级临时";

                                        break;
                                    case "1530":
                                        ryzclb = "小型项目管理师";
                                        ryzyzglxID = "2";

                                        ryzyzglx = "小型项目管理师";
                                        ryzslxID = "21";
                                        ryzslx = "小型项目管理师资格证";

                                        zyzgdjID = "2";
                                        zyzgdj = "小型";
                                        break;
                                    case "1600":
                                        ryzclb = "注册造价工程师";
                                        ryzyzglxID = "41";

                                        ryzyzglx = "注册造价工程师";
                                        ryzslxID = "131";
                                        ryzslx = "注册造价师资格证";
                                        break;
                                    case "1700":
                                        ryzclb = "注册监理工程师";
                                        ryzyzglxID = "21";
                                        ryzyzglx = "注册监理工程师";
                                        ryzslxID = "91";
                                        ryzslx = "注册监理师资格证";
                                        break;
                                    case "1800":
                                        ryzclb = "注册城市规划师";
                                        ryzyzglxID = "79";

                                        ryzyzglx = "注册城市规划师";
                                        ryzslxID = "791";
                                        ryzslx = "注册城市规划师资格证";
                                        break;
                                    case "1900":
                                        ryzclb = "注册化工工程师";
                                        ryzyzglxID = "80";
                                        ryzyzglxID = "注册化工工程师";
                                        ryzslxID = "801";
                                        ryzslx = "注册化工工程师资格证";
                                        break;
                                    case "2000":
                                        ryzclb = "注册房地产估价师";
                                        ryzyzglxID = "81";
                                        ryzyzglx = "注册房地产估价师";
                                        ryzslxID = "811";
                                        ryzslx = "注册房地产估价师资格证";
                                        break;
                                    case "2100":
                                        ryzclb = "注册房地产经纪人";
                                        ryzyzglxID = "82";
                                        ryzyzglx = "注册房地产经纪人";
                                        ryzslxID = "821";
                                        ryzslx = "注册房地产经纪人资格证";
                                        break;
                                    case "2200":
                                        ryzclb = "物业管理师";
                                        ryzyzglxID = "83";

                                        ryzyzglx = "物业管理师";
                                        ryzslxID = "831";
                                        ryzslx = "物业管理师资格证";
                                        break;
                                }


                                DataTable dt_ryzyzg = dataService.Get_uepp_Ryzyzg(outConsInfo.IDCardNo);
                                rowIndex = -1;
                                for (int i = 0; i < dt_ryzyzg.Rows.Count; i++)
                                {
                                    if (dt_ryzyzg.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxID)
                                    {
                                        rowIndex = i;
                                        break;
                                    }
                                }
                                if (rowIndex < 0)
                                {
                                    row = dt_ryzyzg.NewRow();
                                    dt_ryzyzg.Rows.Add(row);

                                    row["ryID"] = outConsInfo.IDCardNo;
                                    row["ryzyzglxID"] = ryzyzglxID;
                                    row["ryzyzglx"] = ryzyzglx;

                                    row["balxID"] = 1;
                                    row["balx"] = "长期备案";

                                    row["DataState"] = 0;
                                    row["tag"] = tag;
                                    row["xgr"] = "定时服务";
                                    row["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd");
                                    dataService.Submit_uepp_Ryzyzg(dt_ryzyzg);
                                }

                                #endregion

                                #region 人员专业明细

                                DataTable dt_ryzymx = dataService.Get_uepp_Ryzymx(outConsInfo.IDCardNo);
                                rowIndex = -1;
                                for (int i = 0; i < dt_ryzymx.Rows.Count; i++)
                                {
                                    if (dt_ryzymx.Rows[i]["ryzyzglxID"].ToString2() == ryzyzglxID)
                                    {
                                        rowIndex = i;
                                        break;
                                    }
                                }
                                if (rowIndex < 0)
                                {
                                    row = dt_ryzymx.NewRow();
                                    dt_ryzymx.Rows.Add(row);
                                    row["ryID"] = outConsInfo.IDCardNo;
                                    row["ryzyzglxID"] = ryzyzglxID;
                                    row["ryzyzglx"] = ryzyzglx;

                                    row["zyzglb"] = outConsInfo.RegMajor;
                                    if (!string.IsNullOrEmpty(outConsInfo.RegMajor))
                                    {
                                        SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                        sp.Add("@CodeInfo", outConsInfo.RegMajor);
                                        sp.Add("@parentCode", ryzyzglxID);

                                        DataTable dt_code = dataService.GetTable("select * from UEPP_Code where  CodeType='人员资质类别' and ParentCodeType='人员执业资格类型' and ParentCode=@parentCode and CodeInfo=@CodeInfo", sp);
                                        if (dt_code.Rows.Count == 0)
                                        {
                                            dt_code.Rows.Add(dt_code.NewRow());
                                            dt_code.Rows[0]["ParentCodeType"] = "人员执业资格类型";
                                            dt_code.Rows[0]["ParentCode"] = ryzyzglxID;
                                            dt_code.Rows[0]["CodeType"] = "人员资质类别";
                                            dt_code.Rows[0]["CodeInfo"] = outConsInfo.RegMajor.Trim();
                                            dt_code.Rows[0]["Code"] = dataService.Get_uepp_Code_NewCode("人员资质类别");
                                            dt_code.Rows[0]["CodeDesc"] = "";
                                            dt_code.Rows[0]["OrderID"] = dt_code.Rows[0]["Code"];
                                            dataService.Submit_uepp_code(dt_code);

                                        }
                                        row["zyzglbID"] = dt_code.Rows[0]["Code"];
                                    }
                                    row["zyzgdjID"] = zyzgdjID;
                                    row["zyzgdj"] = zyzgdj;
                                    row["DataState"] = 0;
                                    row["tag"] = tag;
                                    row["xgr"] = "定时服务";
                                    row["xgrqsj"] = string.IsNullOrEmpty(outConsInfo.ValidDate) ? DateTime.Now.ToString("yyyy-MM-dd") : outConsInfo.ValidDate;
                                    dataService.Submit_uepp_Ryzymx(dt_ryzymx);
                                }
                                #endregion

                                #region 人员证书基本信息
                                DataTable dt_ryzs = dataService.Get_uepp_Ryzs(outConsInfo.IDCardNo);
                                rowIndex = -1;
                                for (int i = 0; i < dt_ryzs.Rows.Count; i++)
                                {
                                    if (dt_ryzs.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxID)
                                    {
                                        rowIndex = i;
                                        break;
                                    }
                                }
                                if (rowIndex < 0)
                                {
                                    row = dt_ryzs.NewRow();
                                    dt_ryzs.Rows.Add(row);
                                    row["zsjlId"] = dataService.Get_uepp_RyzsNewID();
                                    row["ryID"] = outConsInfo.IDCardNo;
                                    row["ryzyzglxID"] = ryzyzglxID;
                                    row["ryzyzglx"] = ryzyzglx;
                                    row["ryzslxID"] = ryzslxID;
                                    row["ryzslx"] = ryzslx;
                                }
                                else
                                {
                                    row = dt_ryzs.Rows[rowIndex];
                                }
                                row["sfzzz"] = 1;
                                if (!string.IsNullOrEmpty(outConsInfo.IssueDate))
                                {
                                    row["fzrq"] = outConsInfo.IssueDate;
                                    row["zsyxqrq"] = outConsInfo.IssueDate;
                                }
                                if (!string.IsNullOrEmpty(outConsInfo.ValidDate))
                                    row["zsyxzrq"] = outConsInfo.ValidDate;

                                row["zsbh"] = outConsInfo.RegNo;
                                row["Status"] = outConsInfo.Status;
                                row["StampNo"] = outConsInfo.RegNo;
                                row["RegNo"] = outConsInfo.RegNo;

                                row["DataState"] = 0;
                                row["tag"] = tag;
                                row["xgr"] = "定时服务";
                                row["xgrqsj"] = DateTime.Now;
                                row["UpdateTime"] = DateTime.Now;
                                dataService.Submit_uepp_Ryzs(dt_ryzs);

                                #endregion

                                #region 企业与人员及其执业资格对应关系
                                DataTable dt_qyry = dataService.Get_uepp_Qyry(outConsInfo.IDCardNo, outConsInfo.CorpCode, ryzyzglxID);
                                if (dt_qyry.Rows.Count == 0)
                                {

                                    row = dt_qyry.NewRow();
                                    dt_qyry.Rows.Add(row);
                                    row["ryID"] = outConsInfo.IDCardNo;
                                    row["qyID"] = outConsInfo.CorpCode;
                                    row["ryzyzglxID"] = ryzyzglxID;
                                    row["ryzyzglx"] = ryzyzglx;
                                    row["DataState"] = 0;
                                    row["tag"] = tag;
                                    row["xgr"] = "定时服务";
                                    row["xgrqsj"] = string.IsNullOrEmpty(outConsInfo.ValidDate) ? DateTime.Now.ToString("yyyy-MM-dd") : outConsInfo.ValidDate;
                                    dataService.Submit_uepp_qyry(dt_qyry);
                                }
                                #endregion
                            }
                            catch (Exception ex)
                            {
                                row_SaveDataLog["SaveState"] = 0;
                                row_SaveDataLog["Msg"] = ex.Message;
                                dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                            }

                            if (dt_SaveDataLog.Rows.Count > 0)
                                dataService.Submit_SaveDataLog(dt_SaveDataLog);

                            row_DataJkDataDetail_ryxx["allCount"] = allCount_ryxx;
                            row_DataJkDataDetail_ryxx["successCount"] = success_count_ryxx;
                            row_DataJkDataDetail_ryxx["IsOk"] = 1;
                            row_DataJkDataDetail_ryxx["ErrorMsg"] = "";

                            if (dt_DataJkDataDetail_ryxx.Rows.Count > 0)
                                dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_ryxx);
                        }
                    }
                }

                DateTime endTime = DateTime.Now;
                TimeSpan span = compareDateTime(beginTime, endTime);
                Public.WriteLog(string.Format("结束YourTask_PullDataFromSxxzx_Swryxx方法:{0}，历时：{1}时{2}分{3}秒", endTime.ToString("yyyy-MM-dd HH:mm:ss"), span.Hours, span.Minutes, span.Seconds));

            }
            catch (Exception ex)
            {
                row_DataJkDataDetail_ryxx["allCount"] = allCount_ryxx;
                row_DataJkDataDetail_ryxx["successCount"] = success_count_ryxx;
                row_DataJkDataDetail_ryxx["IsOk"] = 0;
                row_DataJkDataDetail_ryxx["ErrorMsg"] = ex.Message;

                if (dt_DataJkDataDetail_ryxx.Rows.Count > 0)
                    dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_ryxx);

            }
        }

        /// <summary>
        /// 从江苏建设公共基础数据平台获取建设单位信息
        /// </summary>
        /// <param name="DataJkLogID"></param>
        void YourTask_PullDataFromSxxzx_Jsdw(string DataJkLogID)
        {
            DateTime beginTime = DateTime.Now;
            Public.WriteLog("开始执行YourTask_PullDataFromSxxzx_Jsdw方法：" + beginTime.ToString("yyyy-MM-dd HH:mm:ss"));

            string tag = Tag.江苏建设公共基础数据平台.ToString();
            string userID = "wxszjj01";
            DataService dataService = new DataService();
            DataTable dt_xzqhdm = dataService.Get_tbXzqdmDic();
            XmlHelper helper = new XmlHelper();

            //往数据监控日志表项添加一条记录
            int allCount_qyxx = 0, success_count_qyxx = 0;
            DataTable dt_DataJkDataDetail_qyxx = dataService.GetSchema_DataJkDataDetail();
            long Id_DataJkDataDetail_qyxx = dataService.Get_DataJkDataDetailNewID().ToInt64();

            DataRow row_DataJkDataDetail_qyxx = dt_DataJkDataDetail_qyxx.NewRow();
            dt_DataJkDataDetail_qyxx.Rows.Add(row_DataJkDataDetail_qyxx);

            row_DataJkDataDetail_qyxx["ID"] = Id_DataJkDataDetail_qyxx++;
            row_DataJkDataDetail_qyxx["DataJkLogID"] = DataJkLogID;
            row_DataJkDataDetail_qyxx["tableName"] = "UEPP_Qyjbxx";
            row_DataJkDataDetail_qyxx["MethodName"] = "getCorpInfo(Jsdw)";
            row_DataJkDataDetail_qyxx["bz"] = "从江苏建设公共基础数据平台获取建设单位信息";

            NewDataService.NewDataService newDataService = new NewDataService.NewDataService();
            DataRow row;

            try
            {
                DataTable dt_SaveDataLog = dataService.GetSchema_SaveDataLog();
                foreach (DataRow row_xzqdm in dt_xzqhdm.Rows)
                {
                    string xzqdm = row_xzqdm["Code"].ToString2();
                    byte[] bytes = newDataService.getCorpInfo(userID, "320200", xzqdm, "13", "0");

                    string result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);

                    var index = result.IndexOf("<ReturnInfo>");
                    if (index >= 0)
                    {
                        string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
                        if (string.IsNullOrEmpty(returnResult))
                        {
                            continue;
                        }

                        ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
                        if (!returnInfo.Status)
                        {
                            continue;
                        }

                    }
                    index = result.IndexOf("<CorpBasicInfo>");
                    if (index >= 0)
                    {
                        string corpBasicInfoString = result.Substring(index, result.LastIndexOf("</CorpBasicInfo>") - index + 16);
                        CorpBasicInfoBody corpBasicInfoBody = helper.DeserializeXML<CorpBasicInfoBody>("<CorpBasicInfoBody><CorpBasicInfoArray>" + corpBasicInfoString + "</CorpBasicInfoArray></CorpBasicInfoBody>");
                        if (corpBasicInfoBody != null)
                        {
                            foreach (CorpBasicInfo corpBasicInfo in corpBasicInfoBody.array)
                            {
                                DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                                row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_qyxx["ID"];
                                row_SaveDataLog["DataXml"] = "";
                                row_SaveDataLog["PKID"] = corpBasicInfo.CorpCode;
                                row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                                try
                                {
                                    #region  更新企业基本信息

                                    DataTable dt = dataService.Get_uepp_jsdw(corpBasicInfo.CorpCode);

                                    if (corpBasicInfo.CorpCode.Length == 9)
                                    {
                                        corpBasicInfo.CorpCode = corpBasicInfo.CorpCode.Substring(0, 8) + '-' + corpBasicInfo.CorpCode.Substring(8, 1);
                                    }
                                    //检查该企业是否存在社会信用代码，若存在，则转化为社会信用代码
                                    if (corpBasicInfo.CorpCode.Length == 10)
                                    {
                                        string qyShxydm = dataService.Get_UEPP_Jsdw_Shxydm(corpBasicInfo.CorpCode);
                                        if (!string.IsNullOrEmpty(qyShxydm))
                                        {
                                            corpBasicInfo.CorpCode = qyShxydm;
                                        }
                                    }

                                    if (dt.Rows.Count == 0)
                                    {
                                        row = dt.NewRow();
                                        dt.Rows.Add(row);
                                    }
                                    else
                                    {
                                        row = dt.Rows[0];
                                        if (!string.IsNullOrEmpty(corpBasicInfo.UpdateDate))
                                        {
                                            if (DateTime.Parse(row["xgrqsj"].ToString2()).ToString("yyyy-MM-dd") == DateTime.Parse(corpBasicInfo.UpdateDate).ToString("yyyy-MM-dd"))
                                            {
                                                continue;
                                            }
                                        }
                                    }
                                    row["jsdwID"] = corpBasicInfo.CorpCode;
                                    row["tag"] = tag;

                                    row["zzjgdm"] = corpBasicInfo.CorpCode;
                                    row["jsdw"] = corpBasicInfo.CorpName;
                                    row["dwflID"] = "1";
                                    row["dwfl"] = "房地产开发企业";
                                    row["yb"] = corpBasicInfo.PostCode;
                                    row["dwdz"] = corpBasicInfo.CorpAddress;
                                    row["fddbr"] = corpBasicInfo.LegalMan;
                                    row["yyzz"] = corpBasicInfo.LicenseNo;
                                    row["fax"] = corpBasicInfo.Fax;
                                    row["lxdh"] = corpBasicInfo.LinkPhone;
                                    if (!string.IsNullOrEmpty(corpBasicInfo.UpdateDate))
                                        row["xgrqsj"] = corpBasicInfo.UpdateDate;
                                    else
                                        row["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                    row["OperateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                    row["DataState"] = 0;
                                    allCount_qyxx++;
                                    if (!dataService.Submit_uepp_jsdw(dt))
                                    {
                                        Public.WriteLog("建设单位：" + corpBasicInfo.CorpName + "," + corpBasicInfo.CorpCode + "，基本信息保存失败！");
                                        row_SaveDataLog["SaveState"] = 0;
                                        row_SaveDataLog["Msg"] = "建设单位：" + corpBasicInfo.CorpName + "," + corpBasicInfo.CorpCode + "，基本信息保存失败！";
                                    }
                                    else
                                    {
                                        success_count_qyxx++;
                                        row_SaveDataLog["SaveState"] = 1;
                                        row_SaveDataLog["Msg"] = "";
                                    }
                                    #endregion
                                }
                                catch (Exception ex)
                                {
                                    row_SaveDataLog["SaveState"] = 0;
                                    row_SaveDataLog["Msg"] = "出现异常：" + ex.Message;
                                }
                                dt_SaveDataLog.Rows.Add(row_SaveDataLog);

                            }
                        }
                    }
                }
                if (dt_SaveDataLog.Rows.Count > 0)
                {
                    dataService.Submit_SaveDataLog(dt_SaveDataLog);
                }


                DateTime endTime = DateTime.Now;
                TimeSpan span = compareDateTime(beginTime, endTime);
                Public.WriteLog(string.Format("结束YourTask_PullDataFromSxxzx_Jsdw任务:{0}，历时：{1}时{2}分{3}秒", endTime.ToString("yyyy-MM-dd HH:mm:ss"), span.Hours, span.Minutes, span.Seconds));


                row_DataJkDataDetail_qyxx["allCount"] = allCount_qyxx;
                row_DataJkDataDetail_qyxx["successCount"] = success_count_qyxx;
                row_DataJkDataDetail_qyxx["IsOk"] = 1;
                row_DataJkDataDetail_qyxx["ErrorMsg"] = "";

                if (dt_DataJkDataDetail_qyxx.Rows.Count > 0)
                    dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_qyxx);
            }
            catch (Exception ex)
            {
                row_DataJkDataDetail_qyxx["allCount"] = allCount_qyxx;
                row_DataJkDataDetail_qyxx["successCount"] = success_count_qyxx;
                row_DataJkDataDetail_qyxx["IsOk"] = 0;
                row_DataJkDataDetail_qyxx["ErrorMsg"] = ex.Message;

                if (dt_DataJkDataDetail_qyxx.Rows.Count > 0)
                    dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_qyxx);
            }
        }

        /// <summary>
        /// 从江苏建设公共基础数据平台拉取无锡市企业（建设单位除外）信息
        /// </summary>
        void YourTask_PullDataFromSxxzx_qyxx(string DataJkLogID)
        {
            DateTime beginTime = DateTime.Now;
            Public.WriteLog("开始执行YourTask_PullDataFromSxxzx_qyxx方法：" + beginTime.ToString("yyyy-MM-dd HH:mm:ss"));
            string tag = Tag.江苏建设公共基础数据平台.ToString();
            string userID = "wxszjj01";
            DataService dataService = new DataService();
            DataTable dt_xzqhdm = dataService.Get_tbXzqdmDic();
            DataRow nr = dt_xzqhdm.NewRow();
            nr["Code"] = "320200";
            dt_xzqhdm.Rows.Add(nr);

            DataRow lxRow = dt_xzqhdm.NewRow();
            lxRow["Code"] = "320207";
            dt_xzqhdm.Rows.Add(lxRow);
            DataRow xwRow = dt_xzqhdm.NewRow();
            xwRow["Code"] = "320208";
            dt_xzqhdm.Rows.Add(xwRow);

            XmlHelper helper = new XmlHelper();
            string[] certType = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "14", "15", "16", "20" };


            NewDataService.NewDataService newDataService = new WxjzgcjczyTimerService.NewDataService.NewDataService();
            DataRow row;

            //往数据监控日志表项添加一条记录
            DataTable dt_DataJkDataDetail_qyxx = dataService.GetSchema_DataJkDataDetail();
            long Id_DataJkDataDetail_qyxx = dataService.Get_DataJkDataDetailNewID().ToInt64();

            int allCount_qyxx = 0, success_count_qyxx = 0;
            DataRow row_DataJkDataDetail_qyxx = dt_DataJkDataDetail_qyxx.NewRow();
            dt_DataJkDataDetail_qyxx.Rows.Add(row_DataJkDataDetail_qyxx);

            row_DataJkDataDetail_qyxx["ID"] = Id_DataJkDataDetail_qyxx++;
            row_DataJkDataDetail_qyxx["DataJkLogID"] = DataJkLogID;
            row_DataJkDataDetail_qyxx["tableName"] = "UEPP_Qyjbxx";
            row_DataJkDataDetail_qyxx["MethodName"] = "getCorpInfo(Except jsdw)";
            row_DataJkDataDetail_qyxx["bz"] = "从江苏建设公共基础数据平台拉取企业（建设单位除外）信息";
            try
            {
                DataTable dt_SaveDataLog = dataService.GetSchema_SaveDataLog();

                foreach (DataRow row_xzqdm in dt_xzqhdm.Rows)
                {
                    for (int cttp = 0; cttp < certType.Length; cttp++)
                    {
                        string xzqdm = row_xzqdm["Code"].ToString2();

                        #region 获取企业基本信息
                        byte[] bytes = newDataService.getCorpInfo(userID, "320200", xzqdm, certType[cttp], "0");
                        string result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                        var index = result.IndexOf("<ReturnInfo>");

                        if (index >= 0)
                        {
                            string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
                            if (string.IsNullOrEmpty(returnResult))
                            {
                                continue;
                            }
                            ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
                            if (!returnInfo.Status)
                            {
                                continue;
                            }
                        }

                        index = result.IndexOf("<CorpBasicInfo>");
                        if (index >= 0)
                        {
                            string corpBasicInfoString = result.Substring(index, result.LastIndexOf("</CorpBasicInfo>") - index + 16);
                            CorpBasicInfoBody corpBasicInfoBody = helper.DeserializeXML<CorpBasicInfoBody>("<CorpBasicInfoBody><CorpBasicInfoArray>" + corpBasicInfoString + "</CorpBasicInfoArray></CorpBasicInfoBody>");
                            if (corpBasicInfoBody != null)
                            {
                                foreach (CorpBasicInfo corpBasicInfo in corpBasicInfoBody.array)
                                {
                                    //if (corpCertQualArr.array.Exists(p => p.CorpCode == corpBasicInfo.CorpCode && p.CertType == "房地产开发"))
                                    //{
                                    //    continue;
                                    //}
                                    
                                    if (corpBasicInfo.CorpCode.Length == 9)
                                    {
                                        corpBasicInfo.CorpCode = corpBasicInfo.CorpCode.Substring(0, 8) + '-' + corpBasicInfo.CorpCode.Substring(8, 1);
                                    }
                                    //检查该企业是否存在社会信用代码，若存在，则转化为社会信用代码
                                    if (corpBasicInfo.CorpCode.Length == 10)
                                    {
                                        string qyShxydm = dataService.Get_UEPP_Qyjbxx_Shxydm(corpBasicInfo.CorpCode);
                                        if (!string.IsNullOrEmpty(qyShxydm))
                                        {
                                            corpBasicInfo.CorpCode = qyShxydm;
                                        }
                                    }

                                    DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                                    row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_qyxx["ID"];
                                    row_SaveDataLog["DataXml"] = "";
                                    row_SaveDataLog["PKID"] = corpBasicInfo.CorpCode;
                                    row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");
                                    dt_SaveDataLog.Rows.Add(row_SaveDataLog);

                                    try
                                    {
                                        #region  更新企业基本信息

                                        DataTable dt = dataService.Get_uepp_Qyjbxx(corpBasicInfo.CorpCode);

                                        if (dt.Rows.Count == 0)
                                        {
                                            row = dt.NewRow();
                                            dt.Rows.Add(row);
                                            row["tyshxydm"] = corpBasicInfo.LicenseNo;
                                        }
                                        else
                                        {
                                            row = dt.Rows[0];
                                            //if (!string.IsNullOrEmpty(corpBasicInfo.UpdateDate) && !string.IsNullOrEmpty(row["xgrqsj"].ToString2()))
                                            //    if (DateTime.Parse(row["xgrqsj"].ToString2()).ToString("yyyy-MM-dd") == DateTime.Parse(corpBasicInfo.UpdateDate).ToString("yyyy-MM-dd"))
                                            //    {
                                            //        continue;
                                            //    }
                                            //if (row["tag"].ToString2().IndexOf(tag) < 0)
                                            //{
                                            //    row["tag"] = row["tag"].ToString2().TrimEnd(',') + "," + tag;
                                            //}
                                            if (!string.IsNullOrEmpty(row["needUpdateFlag"].ToString()) && !row["needUpdateFlag"].ToBoolean())
                                            {
                                                //needUpdateFlag默认为1都需要更新，不需要更新的企业需要在四库数据库手动配置为0
                                                continue;
                                            }
                                        }
                                        row["tag"] = tag;
                                        row["qyID"] = corpBasicInfo.CorpCode;
                                        row["qymc"] = corpBasicInfo.CorpName;
                                        row["zzjgdm"] = corpBasicInfo.CorpCode;
                                        row["yyzzzch"] = corpBasicInfo.LicenseNo;
                                        

                                        if (!string.IsNullOrEmpty(corpBasicInfo.ProvinceCode.ToString2()))
                                        {
                                            SqlParameterCollection sp = dataService.CreateSqlParameterCollection();

                                            row["Province"] = corpBasicInfo.ProvinceCode;
                                            sp.Add("@CodeInfo", corpBasicInfo.ProvinceCode.ToString2());
                                            string provinceCode = dataService.ExecuteSql("select top 1 Code from  UEPP_Code where CodeType='城市地区' and  CodeInfo=@CodeInfo", sp);
                                            if (!string.IsNullOrEmpty(provinceCode))
                                            {
                                                row["ProvinceID"] = provinceCode;
                                                sp.Clear();
                                                if (!string.IsNullOrEmpty(corpBasicInfo.CityCode.ToString2()))
                                                {
                                                    if (string.IsNullOrEmpty(row["City"].ToString2()))
                                                    {
                                                        row["City"] = corpBasicInfo.CityCode;
                                                    }
                                                   

                                                    sp.Add("@CodeInfo", corpBasicInfo.CityCode.ToString2());
                                                    sp.Add("@parentCode", provinceCode);
                                                    string cityCode = dataService.ExecuteSql("select top 1 Code from  UEPP_Code where CodeType='城市地区' and ParentCode=@parentCode and  CodeInfo=@CodeInfo", sp);
                                                    if (!string.IsNullOrEmpty(cityCode))
                                                    {
                                                        if (string.IsNullOrEmpty( row["CityID"].ToString2()))
                                                        {
                                                            row["CityID"] = cityCode;
                                                        }
                                                       
                                                    }

                                                    sp.Clear();
                                                    if (!string.IsNullOrEmpty(corpBasicInfo.CountyCode.ToString2()))
                                                    {
                                                        if (string.IsNullOrEmpty( row["County"].ToString2()))
                                                        {
                                                            row["County"] = corpBasicInfo.CountyCode;
                                                        }
                                                        

                                                        sp.Add("@CodeInfo", corpBasicInfo.CountyCode.ToString2());
                                                        sp.Add("@parentCode", cityCode);
                                                        string countyCode = dataService.ExecuteSql("select top 1 Code from  UEPP_Code where CodeType='城市地区' and ParentCode=@parentCode and  CodeInfo=@CodeInfo", sp);
                                                        if (!string.IsNullOrEmpty(countyCode))
                                                        {
                                                            row["CountyID"] = countyCode;
                                                        }

                                                    }
                                                }
                                            }
                                        }

                                        row["zcdd"] = corpBasicInfo.RegAddress;
                                        row["xxdd"] = corpBasicInfo.CorpAddress;
                                        if (!string.IsNullOrEmpty(corpBasicInfo.FoundDate))
                                            row["clrq"] = corpBasicInfo.FoundDate;
                                        row["jjxz"] = corpBasicInfo.CorpTypeDesc.ToString2().Trim();
                                        if (!string.IsNullOrEmpty(corpBasicInfo.CorpTypeDesc))
                                        {
                                            SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                            sp.Add("@CodeInfo", corpBasicInfo.CorpTypeDesc.ToString2().Trim());
                                            string jjxzID = dataService.ExecuteSql("select * from  UEPP_Code where CodeType='企业经济性质' and  CodeInfo=@CodeInfo", sp);
                                            if (!string.IsNullOrEmpty(jjxzID))
                                            {
                                                row["jjxzID"] = jjxzID;
                                            }
                                        }

                                        row["zczb"] = corpBasicInfo.RegCapital;
                                        row["cz"] = corpBasicInfo.Fax;
                                        row["lxdh"] = corpBasicInfo.LinkPhone;
                                        row["fddbr"] = corpBasicInfo.LegalMan;

                                        if (!string.IsNullOrEmpty(corpBasicInfo.UpdateDate))
                                            row["xgrqsj"] = corpBasicInfo.UpdateDate;
                                        else
                                            row["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd");

                                        row["DataState"] = 0;
                                        allCount_qyxx++;
                                        if (!dataService.Submit_uepp_qyjbxx(dt))
                                        {
                                            row_SaveDataLog["SaveState"] = 0;
                                            row_SaveDataLog["Msg"] = "从江苏建设公共基础数据平台获取的单位：" + corpBasicInfo.CorpName + "," + corpBasicInfo.CorpCode + "，基本信息保存失败！";
                                            continue;
                                        }
                                        else
                                        {
                                            success_count_qyxx++;
                                            row_SaveDataLog["SaveState"] = 1;
                                            row_SaveDataLog["Msg"] = "";
                                        }
                                        #endregion
                                    }
                                    catch (Exception ex)
                                    {
                                        row_SaveDataLog["SaveState"] = 0;
                                        row_SaveDataLog["Msg"] = ex.Message;
                                    }
                                }


                            }
                        }
                        #endregion

                        #region  获取企业证书信息
                        CorpCertInfoBody corpCertInfoArr = new CorpCertInfoBody(); //企业证书信息
                        bytes = newDataService.getCorpCert(userID, "320200", xzqdm, certType[cttp], "0");
                        result = System.Text.Encoding.UTF8.GetString(bytes);

                        index = result.IndexOf("<ReturnInfo>");

                        if (index >= 0)
                        {
                            string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
                            if (string.IsNullOrEmpty(returnResult))
                            {
                                continue;
                            }
                            ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
                            if (!returnInfo.Status)
                            {
                                continue;
                            }
                        }


                        index = result.IndexOf("<CorpCertInfo>");
                        if (index >= 0)
                        {
                            string corpCertInfoString = result.Substring(index, result.LastIndexOf("</CorpCertInfo>") - index + 15);
                            corpCertInfoArr = helper.DeserializeXML<CorpCertInfoBody>(
                                "<CorpCertInfoBody><CorpCertInfoArray>" + corpCertInfoString + "</CorpCertInfoArray></CorpCertInfoBody>");
                        }

                        CorpCertQualBody corpCertQualArr = new CorpCertQualBody();  //企业资质
                        index = result.IndexOf("<CorpCertQual>");

                        if (index >= 0)
                        {
                            string corpCertQualString = result.Substring(index, result.LastIndexOf("</CorpCertQual>") - index + 15);
                            corpCertQualArr = helper.DeserializeXML<CorpCertQualBody>(
                                "<CorpCertQualBody><CorpCertQualArray>" + corpCertQualString + "</CorpCertQualArray></CorpCertQualBody>");

                        }

                        #region 更新企业资质(TCorpCertQual)
                        if (corpCertQualArr != null)
                        {
                            foreach (CorpCertQual corpCertQual in corpCertQualArr.array)
                            {
                                //if (corpCertQual.CertType == "房地产开发")
                                //    break;
                                //for (int i = 0; i < dt_jsdw_zzmx.Rows.Count; i++)
                                //{
                                //    if (!list.Exists(p => p.CertType == "房地产开发" && dt_jsdw_zzmx.Rows[i]["csywlxID"].ToString2() == "11"
                                //        && (p.TradeType == "不分行业" || p.TradeType == dt_jsdw_zzmx.Rows[i]["zzxl"].ToString2())
                                //        && p.MajorType == dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2()))
                                //    {
                                //        dt_jsdw_zzmx.Rows[i].Delete();
                                //    }
                                //}

                                try
                                {
                                    if (corpCertQual.CorpCode.Length == 9)
                                    {
                                        corpCertQual.CorpCode = corpCertQual.CorpCode.Substring(0, 8) + '-' + corpCertQual.CorpCode.Substring(8, 1);
                                    }
                                    //检查该企业是否存在社会信用代码，若存在，则转化为社会信用代码
                                    if (corpCertQual.CorpCode.Length == 10)
                                    {
                                        string qyShxydm = dataService.Get_UEPP_Qyjbxx_Shxydm(corpCertQual.CorpCode);
                                        if (!string.IsNullOrEmpty(qyShxydm))
                                        {
                                            corpCertQual.CorpCode = qyShxydm;
                                        }
                                    }

                                    //string csywlxID = "", csywlx = "";
                                    Qyxx qycsywlx = getCsywlx(corpCertQual.CertType);
                                    if (string.IsNullOrEmpty(qycsywlx.csywlxID))
                                        continue;

                                    #region 企业从事业务类型

                                    DataTable dt_qycsyw = dataService.Get_uepp_Qycsyw_sjsgyth(corpCertQual.CorpCode, qycsywlx.csywlxID);

                                    DataRow tempRow_qycsyw;

                                    if (dt_qycsyw.Rows.Count == 0)
                                    {
                                        tempRow_qycsyw = dt_qycsyw.NewRow();
                                        dt_qycsyw.Rows.Add(tempRow_qycsyw);
                                        tempRow_qycsyw["qyID"] = corpCertQual.CorpCode;
                                    }
                                    else
                                    {
                                        tempRow_qycsyw = dt_qycsyw.Rows[0];
                                    }

                                    tempRow_qycsyw["csywlxID"] = qycsywlx.csywlxID;
                                    tempRow_qycsyw["csywlx"] = qycsywlx.csywlx;

                                    tempRow_qycsyw["balxID"] = "1";
                                    tempRow_qycsyw["balx"] = "长期备案";
                                    tempRow_qycsyw["DataState"] = "0";
                                    tempRow_qycsyw["tag"] = tag;

                                    if (!string.IsNullOrEmpty(corpCertQual.UpdateDate))
                                        tempRow_qycsyw["xgrqsj"] = corpCertQual.UpdateDate;
                                    else
                                        tempRow_qycsyw["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");



                                    if (dt_qycsyw.Rows.Count > 0)
                                    {
                                        dataService.Submit_uepp_qycsyw(dt_qycsyw);
                                    }
                                    #endregion

                                    DataTable dt_jsdw_zzmx = dataService.Get_uepp_zzmxxx_qyxx_nostatus(corpCertQual.CorpCode, qycsywlx.csywlxID);

                                    int rowIndex = -1;

                                    for (int i = 0; i < dt_jsdw_zzmx.Rows.Count; i++)
                                    {
                                        //if (dt_jsdw_zzmx.Rows[i].RowState == DataRowState.Deleted)
                                        //    continue;

                                        if (qycsywlx.csywlxID == dt_jsdw_zzmx.Rows[i]["csywlxID"].ToString2()
                                            && corpCertQual.CertCode == dt_jsdw_zzmx.Rows[i]["zsbh"].ToString2()
                                            && (
                                             corpCertQual.TradeType == "工程勘察综合类" && dt_jsdw_zzmx.Rows[i]["zzxl"].ToString2() == "综合类"
                                             || corpCertQual.TradeType == "工程勘察专业类" && dt_jsdw_zzmx.Rows[i]["zzxl"].ToString2() == "专业类"
                                             || corpCertQual.TradeType == "工程勘察劳务类" && dt_jsdw_zzmx.Rows[i]["zzxl"].ToString2() == "劳务类"
                                             || corpCertQual.TradeType == dt_jsdw_zzmx.Rows[i]["zzxl"].ToString2()
                                            )
                                            && (dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "岩土工程勘察" && corpCertQual.MajorType == "岩土工程（勘察）"
                                            || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "岩土工程设计" && corpCertQual.MajorType == "岩土工程（设计）"
                                              || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "岩土工程测试、监测、检测" && corpCertQual.MajorType == "岩土工程（物探测试检测监测）"
                                              || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "岩土工程咨询、监理" && corpCertQual.MajorType == "岩土工程（咨询监理）"

                                              || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "建筑装修装饰" && corpCertQual.MajorType == "建筑装饰装修工程"
                                              || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "模板作业" && corpCertQual.MajorType == "模板作业分包"
                                              || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "脚手架搭设作业" && corpCertQual.MajorType == "脚手架作业分包"
                                              || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == corpCertQual.MajorType
                                            )
                                            )
                                        {
                                            rowIndex = i;
                                            break;
                                        }
                                    }

                                    if (rowIndex < 0)
                                    {
                                        row = dt_jsdw_zzmx.NewRow();
                                        dt_jsdw_zzmx.Rows.Add(row);
                                        row["ID"] = dataService.Get_uepp_qyxxmx_NewID();
                                        row["qyID"] = corpCertQual.CorpCode;
                                        row["csywlx"] = qycsywlx.csywlx;
                                        row["csywlxID"] = qycsywlx.csywlxID;
                                    }
                                    else
                                    {
                                        row = dt_jsdw_zzmx.Rows[rowIndex];
                                        if("qlmsoft".Equals(row["xgr"].ToString2())){
                                            //如果是手动修改的数据，则不更新，如果需要更新，则把xgr栏设置为空
                                            continue;
                                        }
                                        
                                        if (!string.IsNullOrEmpty(corpCertQual.UpdateDate) && !string.IsNullOrEmpty(row["xgrqsj"].ToString2()))
                                        {
                                            int cmpFlag = DateTime.Compare(DateTime.Parse(corpCertQual.UpdateDate), row["xgrqsj"].ToDateTime());
                                            //if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(corpCertQual.UpdateDate).ToString("yyyy-MM-dd"))
                                            if (cmpFlag < 0)
                                            {
                                                continue;
                                            }
                                        }        
                                    }
                                    if (corpCertQual.IsMaster == "主项")
                                        row["zzbz"] = "主项";
                                    else
                                        row["zzbz"] = "增项";

                                    if (corpCertQual.TradeType == "工程勘察综合类")
                                    {
                                        row["zzxl"] = "综合类";
                                        row["zzxlID"] = "9";
                                    }
                                    else
                                        if (corpCertQual.TradeType == "工程勘察专业类")
                                        {
                                            row["zzxl"] = "专业类";
                                            row["zzxlID"] = "10";
                                        }
                                        else
                                            if (corpCertQual.TradeType == "工程勘察劳务类")
                                            {
                                                row["zzxl"] = "劳务类";
                                                row["zzxlID"] = "11";
                                            }
                                            else
                                                if (corpCertQual.TradeType == "工程设计综合")
                                                {
                                                    row["zzxl"] = "综合资质";
                                                    row["zzxlID"] = "12";
                                                }
                                                else
                                                {
                                                    row["zzxl"] = corpCertQual.TradeType;
                                                    if (!string.IsNullOrEmpty(qycsywlx.csywlxID))
                                                    {
                                                        string sql = @"select Code from UEPP_Code where  CodeType='企业资质序列' and ParentCodeType='企业从事业务类型'
 and ParentCode=@parentCode and CodeInfo=@CodeInfo ";
                                                        SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                                        sp.Add("@CodeInfo", corpCertQual.TradeType);
                                                        sp.Add("@parentCode", qycsywlx.csywlxID);
                                                        string zzxlID = dataService.ExecuteSql(sql, sp);
                                                        if (!string.IsNullOrEmpty(zzxlID))
                                                            row["zzxlID"] = zzxlID;
                                                    }
                                                }

                                    if (corpCertQual.MajorType == "岩土工程（勘察）")
                                    {
                                        row["zzlb"] = "岩土工程勘察";
                                        row["zzlbID"] = "300";
                                    }
                                    else
                                        if (corpCertQual.MajorType == "岩土工程（设计）")
                                        {
                                            row["zzlb"] = "岩土工程设计";
                                            row["zzlbID"] = "301";
                                        }
                                        else
                                            if (corpCertQual.MajorType == "岩土工程（物探测试检测监测）")
                                            {
                                                row["zzlb"] = "岩土工程测试、监测、检测";
                                                row["zzlbID"] = "302";
                                            }
                                            else
                                                if (corpCertQual.MajorType == "岩土工程（咨询监理））")
                                                {
                                                    row["zzlb"] = "岩土工程咨询、监理";
                                                    row["zzlbID"] = "303";
                                                }
                                                else
                                                    if (corpCertQual.TradeType == "建筑装饰装修工程")
                                                    {
                                                        row["zzlb"] = "建筑装修装饰";
                                                        row["zzlbID"] = "33";
                                                    }
                                                    else if (corpCertQual.TradeType == "模板作业分包")
                                                    {
                                                        row["zzlb"] = "模板作业";
                                                        row["zzlbID"] = "128";
                                                    }
                                                    else
                                                        if (corpCertQual.TradeType == "脚手架作业分包")
                                                        {
                                                            row["zzlb"] = "脚手架搭设作业";
                                                            row["zzlbID"] = "127";
                                                        }
                                                        else
                                                        {
                                                            row["zzlb"] = corpCertQual.MajorType;
                                                            if (!string.IsNullOrEmpty(row["zzxlID"].ToString2().Trim()))
                                                            {
                                                                string sql = @"select Code from UEPP_Code where  CodeType='企业资质类别' and  ParentCodeType='企业资质序列'
 and ParentCode=@parentCode and (CodeInfo=@CodeInfo or CodeInfo=@CodeInfo1) ";
                                                                SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                                                sp.Add("@CodeInfo", corpCertQual.MajorType);
                                                                sp.Add("@parentCode", row["zzxlID"]);
                                                                sp.Add("@CodeInfo1", corpCertQual.MajorType.ToString2().Replace("分包", ""));
                                                                string zzlbID = dataService.ExecuteSql(sql, sp);
                                                                if (!string.IsNullOrEmpty(zzlbID))
                                                                    row["zzlbID"] = zzlbID;
                                                            }
                                                        }




                                    row["zzdj"] = corpCertQual.TitleLevel;
                                    //新增证书跟资质的一对多关联关系
                                    row["zsbh"] = corpCertQual.CertCode;

                                    if (!string.IsNullOrEmpty(corpCertQual.TitleLevel))
                                    {
                                        SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                        sp.Add("@CodeInfo", corpCertQual.TitleLevel);

                                        string sql = "select Code from UEPP_Code  where  CodeType='企业资质等级' and ParentCodeType='企业资质序列' and CodeInfo=@CodeInfo ";
                                        string zzdjID = dataService.ExecuteSql(sql, sp);
                                        if (!string.IsNullOrEmpty(zzdjID))
                                            row["zzdjID"] = zzdjID;
                                        else
                                        {
                                            // 新增代码表
                                        }
                                    }

                                    if (corpCertQual.Status == "1" || corpCertQual.Status == "有效")
                                    {
                                        row["DataState"] = 0;
                                    }
                                    else
                                    {
                                        row["DataState"] = -1;
                                        //还没到注销日期
                                        if (!string.IsNullOrEmpty(corpCertQual.CancelDate.ToString()))
                                        {
                                            int cancelDateFlag = DateTime.Compare(corpCertQual.CancelDate.ToDateTime(), DateTime.Now);
                                            if (cancelDateFlag > 0)
                                            {
                                                row["DataState"] = 0;
                                            }
                                        }
                                    }

                                    
                                    
                                    row["tag"] = tag;
                                    row["xgrqsj"] = corpCertQual.UpdateDate;

                                    if (!dataService.Submit_uepp_qyzzmx(dt_jsdw_zzmx))
                                    {
                                        Public.WriteLog("单位ID：" + corpCertQual.CorpCode + "，企业资质保存失败！");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Public.WriteLog("保存企业资质时出现异常：" + ex.Message);
                                }
                            }
                        }
                        #endregion

                        #region 更新企业资质证书信息(TCorpCertInfo)
                        if (corpCertInfoArr != null)
                        {
                            //List<CorpCertInfo> list = corpCertInfoArr.array.FindAll(p => p.CorpCode == corpBasicInfo.CorpCode);

                            //DataTable dt_jsdw_zzzs = dataService.Get_uepp_jsdw_zzzsxx(corpBasicInfo.CorpCode);

                            //for (int i = 0; i < dt_jsdw_zzzs.Rows.Count; i++)
                            //{
                            //    if (!list.Exists(p => p.CertType == "房地产开发" && p.CertCode == dt_jsdw_zzzs.Rows[i]["zsbh"].ToString2()
                            //        && p.CertType == "开发企业资质证书"))
                            //    {
                            //        dt_jsdw_zzzs.Rows[i].Delete();
                            //    }
                            //}

                            foreach (CorpCertInfo corpCertInfo in corpCertInfoArr.array)
                            {
                                try
                                {
                                    if (corpCertInfo.CorpCode.Length == 9)
                                    {
                                        corpCertInfo.CorpCode = corpCertInfo.CorpCode.Substring(0, 8) + '-' + corpCertInfo.CorpCode.Substring(8, 1);
                                    }
                                    //检查该企业是否存在社会信用代码，若存在，则转化为社会信用代码
                                    if (corpCertInfo.CorpCode.Length == 10)
                                    {
                                        string qyShxydm = dataService.Get_UEPP_Qyjbxx_Shxydm(corpCertInfo.CorpCode);
                                        if (!string.IsNullOrEmpty(qyShxydm))
                                        {
                                            corpCertInfo.CorpCode = qyShxydm;
                                        }
                                    }

                                    int rowIndex = -1;
                                    //string csywlxID = "", csywlx = "";
                                    Qyxx qycsywlx = getCsywlx(corpCertInfo.CertType);
                                    if (string.IsNullOrEmpty(qycsywlx.csywlxID))
                                        continue;

                                    #region 设计与施工一体化证书的企业，没有企业资质，只能通过证书信息产生企业从事业务类型

                                    if ("2".Equals(qycsywlx.csywlxID))
                                    {
                                        DataTable dt_qycsyw = dataService.Get_uepp_Qycsyw_sjsgyth(corpCertInfo.CorpCode, qycsywlx.csywlxID);

                                        DataRow tempRow_qycsyw;

                                        if (dt_qycsyw.Rows.Count == 0)
                                        {
                                            tempRow_qycsyw = dt_qycsyw.NewRow();
                                            dt_qycsyw.Rows.Add(tempRow_qycsyw);
                                            tempRow_qycsyw["qyID"] = corpCertInfo.CorpCode;
                                        }
                                        else
                                        {
                                            tempRow_qycsyw = dt_qycsyw.Rows[0];
                                        }

                                        tempRow_qycsyw["csywlxID"] = qycsywlx.csywlxID;
                                        tempRow_qycsyw["csywlx"] = qycsywlx.csywlx;

                                        tempRow_qycsyw["balxID"] = "1";
                                        tempRow_qycsyw["balx"] = "长期备案";
                                        tempRow_qycsyw["DataState"] = "0";
                                        tempRow_qycsyw["tag"] = tag;
                                        tempRow_qycsyw["xgr"] = "qlmsoft";

                                        if (!string.IsNullOrEmpty(corpCertInfo.UpdateDate)){
                                            tempRow_qycsyw["xgrqsj"] = corpCertInfo.UpdateDate;
                                        }else{
                                            tempRow_qycsyw["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd");
                                        }


                                        if (dt_qycsyw.Rows.Count > 0)
                                        {
                                            dataService.Submit_uepp_qycsyw(dt_qycsyw);
                                        }
                                    }

                                    
                                    #endregion

                                    DataTable dt_qy_zzzs = dataService.Get_uepp_zzzsxx_qyxx(corpCertInfo.CorpCode);

                                    for (int i = 0; i < dt_qy_zzzs.Rows.Count; i++)
                                    {
                                        //if (dt_jsdw_zzzs.Rows[i].RowState == DataRowState.Deleted)
                                        //    continue;
                                        //转换过的从事业务类型（企业资质类别）比较
                                        if (qycsywlx.csywlx == dt_qy_zzzs.Rows[i]["csywlx"].ToString2()
                                            && dt_qy_zzzs.Rows[i]["zsbh"].ToString2() == corpCertInfo.CertCode)
                                        {
                                            rowIndex = i;
                                            break;
                                        }
                                        /**
                                        if (corpCertInfo.CertType == dt_qy_zzzs.Rows[i]["csywlx"].ToString2()
                                            && dt_qy_zzzs.Rows[i]["zsbh"].ToString2() == corpCertInfo.CertCode
                                            && dt_qy_zzzs.Rows[i]["csywlx"].ToString2() == corpCertInfo.CertType)
                                        {
                                            rowIndex = i;
                                            break;
                                        }*/
                                    }

                                    if (rowIndex < 0)
                                    {
                                        row = dt_qy_zzzs.NewRow();
                                        dt_qy_zzzs.Rows.Add(row);
                                        row["zsjlId"] = dataService.Get_uepp_qyQyzs_NewID();
                                        row["qyID"] = corpCertInfo.CorpCode;
                                    }
                                    else
                                    {
                                       row = dt_qy_zzzs.Rows[rowIndex];
                                       if ("qlmsoft".Equals(row["xgr"].ToString2()))
                                       {
                                           //如果是手动修改的数据，则不更新，如果需要更新，则把xgr栏设置为空
                                           continue;
                                       }
                                       if (!string.IsNullOrEmpty(corpCertInfo.UpdateDate))
                                            if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(corpCertInfo.UpdateDate).ToString("yyyy-MM-dd"))
                                            {
                                                continue;
                                            }
                                    }

                                    row["csywlx"] = qycsywlx.csywlx;
                                    row["csywlxID"] = qycsywlx.csywlxID;
                                    //增加证书正本流水号， 证书正本流水号为空的资质不用显示，跟住建部、省厅保持一致
                                    row["PrintNo"] = corpCertInfo.PrintNo;

                                    //if (!string.IsNullOrEmpty(corpCertInfo.CertType))
                                    //{
                                    //    SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                    //    sp.Add("@parentCode", csywlxID);
                                    //    sp.Add("@CodeInfo", corpCertInfo.CertType);

                                    //    string sql = @"select * from UEPP_Code  where  CodeType ='企业证书类型' and ParentCodeType='企业从事业务类型' and ParentCode=@parentCode and CodeInfo=@CodeInfo ";
                                    //    string zslxID = dataService.ExecuteSql(sql, sp);
                                    //    if (!string.IsNullOrEmpty(zslxID))
                                    //    {
                                    //        row["zslxID"] = zslxID;
                                    //    }
                                    //}
                                    //row["zslx"] = "开发企业资质证书";

                                    row["sfzzz"] = "1";
                                    //string zslx = "", zslxID = "";
                                    Qyxx qyzslx = getZslx(corpCertInfo.CertType);
                                    row["zslxID"] = qyzslx.zslxID;
                                    row["zslx"] = qyzslx.zslx;
                                    row["zsbh"] = corpCertInfo.CertCode;
                                    if (!string.IsNullOrEmpty(corpCertInfo.ValidDate.Trim()))
                                        row["zsyxzrq"] = corpCertInfo.ValidDate;
                                    if (!string.IsNullOrEmpty(corpCertInfo.IssueDate.Trim()))
                                    {
                                        row["fzrq"] = corpCertInfo.IssueDate;
                                        row["zsyxqrq"] = corpCertInfo.IssueDate;
                                    }

                                    row["fzdw"] = corpCertInfo.IssueOrgan;
                                    row["xgrqsj"] = corpCertInfo.UpdateDate;
                                    row["xgr"] = "定时服务";
                                    row["tag"] = tag;
                                    row["DataState"] = 0;

                                    if (!dataService.Submit_uepp_qyzzzs(dt_qy_zzzs))
                                    {
                                        Public.WriteLog("单位ID：" + corpCertInfo.CorpCode + "，企业资质证书信息保存失败！");
                                    }

                                }
                                catch (Exception ex)
                                {
                                    Public.WriteLog("保存企业资质证书信息时出现异常：" + corpCertInfo.CorpCode + ex.Message);
                                }
                            }
                        }
                        #endregion

                        #endregion
                    }
                }
                if (dt_SaveDataLog.Rows.Count > 0)
                    dataService.Submit_SaveDataLog(dt_SaveDataLog);

                DateTime endTime = DateTime.Now;
                TimeSpan span = compareDateTime(beginTime, endTime);
                Public.WriteLog(string.Format("结束YourTask_PullDataFromSxxzx_qyxx任务:{0}，历时：{1}时{2}分{3}秒", endTime.ToString("yyyy-MM-dd HH:mm:ss"), span.Hours, span.Minutes, span.Seconds));


                row_DataJkDataDetail_qyxx["allCount"] = allCount_qyxx;
                row_DataJkDataDetail_qyxx["successCount"] = success_count_qyxx;
                row_DataJkDataDetail_qyxx["IsOk"] = 1;
                row_DataJkDataDetail_qyxx["ErrorMsg"] = "";

                if (dt_DataJkDataDetail_qyxx.Rows.Count > 0)
                    dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_qyxx);
            }
            catch (Exception ex)
            {
                row_DataJkDataDetail_qyxx["allCount"] = allCount_qyxx;
                row_DataJkDataDetail_qyxx["successCount"] = success_count_qyxx;
                row_DataJkDataDetail_qyxx["IsOk"] = 0;
                row_DataJkDataDetail_qyxx["ErrorMsg"] = "从江苏建设公共基础数据平台获取企业信息时出现异常:" + ex.Message;

                if (dt_DataJkDataDetail_qyxx.Rows.Count > 0)
                    dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_qyxx);
            }
        }

        /// <summary>
        /// 从江苏建设公共基础数据平台拉取省内市外企业（建设单位除外）信息
        /// </summary>
        void YourTask_PullDataFromSxxzx_Jiangsu_qyxx(string DataJkLogID , string aCityCode)
        {
            DateTime beginTime = DateTime.Now;
            Public.WriteLog("YourTask_PullDataFromSxxzx_Jiangsu_qyxx：" + beginTime.ToString("yyyy-MM-dd HH:mm:ss") + ":cityCode :" + aCityCode);
            string tag = Tag.江苏建设公共基础数据平台.ToString();
            string userID = "wxszjj01";
            DataService dataService = new DataService();
            DataTable dt_xzqhdm = dataService.Get_tbXzqdmDic_Country(aCityCode); 

            XmlHelper helper = new XmlHelper();
            string[] certType = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "14", "15", "16", "20" };


            NewDataService.NewDataService newDataService = new WxjzgcjczyTimerService.NewDataService.NewDataService();
            DataRow row;

            //往数据监控日志表项添加一条记录
            DataTable dt_DataJkDataDetail_qyxx = dataService.GetSchema_DataJkDataDetail();
            long Id_DataJkDataDetail_qyxx = dataService.Get_DataJkDataDetailNewID().ToInt64();

            int allCount_qyxx = 0, success_count_qyxx = 0;
            DataRow row_DataJkDataDetail_qyxx = dt_DataJkDataDetail_qyxx.NewRow();
            dt_DataJkDataDetail_qyxx.Rows.Add(row_DataJkDataDetail_qyxx);

            row_DataJkDataDetail_qyxx["ID"] = Id_DataJkDataDetail_qyxx++;
            row_DataJkDataDetail_qyxx["DataJkLogID"] = DataJkLogID;
            row_DataJkDataDetail_qyxx["tableName"] = "UEPP_Qyjbxx";
            row_DataJkDataDetail_qyxx["MethodName"] = "getCorpInfo(Except jsdw)";
            row_DataJkDataDetail_qyxx["bz"] = "从江苏建设公共基础数据平台拉取企业（建设单位除外）信息(省内市外):" + aCityCode;
            try
            {
                DataTable dt_SaveDataLog = dataService.GetSchema_SaveDataLog();

                foreach (DataRow row_xzqdm in dt_xzqhdm.Rows)
                {
                    for (int cttp = 0; cttp < certType.Length; cttp++)
                    {
                        string xzqdm = row_xzqdm["Code"].ToString2();


                        #region 获取企业基本信息
                        byte[] bytes = newDataService.getCorpInfo(userID, aCityCode, xzqdm, certType[cttp], "0");
                        string result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                        var index = result.IndexOf("<ReturnInfo>");

                        if (index >= 0)
                        {
                            string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
                            if (string.IsNullOrEmpty(returnResult))
                            {
                                continue;
                            }
                            ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
                            if (!returnInfo.Status)
                            {
                                continue;
                            }
                        }

                        index = result.IndexOf("<CorpBasicInfo>");
                        if (index >= 0)
                        {
                            string corpBasicInfoString = result.Substring(index, result.LastIndexOf("</CorpBasicInfo>") - index + 16);
                            CorpBasicInfoBody corpBasicInfoBody = helper.DeserializeXML<CorpBasicInfoBody>("<CorpBasicInfoBody><CorpBasicInfoArray>" + corpBasicInfoString + "</CorpBasicInfoArray></CorpBasicInfoBody>");
                            if (corpBasicInfoBody != null)
                            {
                                foreach (CorpBasicInfo corpBasicInfo in corpBasicInfoBody.array)
                                {
                                    //if (corpCertQualArr.array.Exists(p => p.CorpCode == corpBasicInfo.CorpCode && p.CertType == "房地产开发"))
                                    //{
                                    //    continue;
                                    //}
                                    //Public.WriteLog("===" + corpBasicInfo.CorpCode);
                                    if (corpBasicInfo.CorpCode.Length == 9)
                                    {
                                        corpBasicInfo.CorpCode = corpBasicInfo.CorpCode.Substring(0, 8) + '-' + corpBasicInfo.CorpCode.Substring(8, 1);
                                    }
                                    //检查该企业是否存在社会信用代码，若存在，则转化为社会信用代码
                                    if (corpBasicInfo.CorpCode.Length == 10)
                                    {
                                        string qyShxydm = dataService.Get_UEPP_Qyjbxx_Shxydm(corpBasicInfo.CorpCode);
                                        if (!string.IsNullOrEmpty(qyShxydm))
                                        {
                                            corpBasicInfo.CorpCode = qyShxydm;
                                        }
                                    }

                                    DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                                    row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_qyxx["ID"];
                                    row_SaveDataLog["DataXml"] = "";
                                    row_SaveDataLog["PKID"] = corpBasicInfo.CorpCode;
                                    row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");
                                    dt_SaveDataLog.Rows.Add(row_SaveDataLog);

                                    try
                                    {
                                        #region  更新企业基本信息

                                        DataTable dt = dataService.Get_uepp_Qyjbxx(corpBasicInfo.CorpCode);

                                        if (dt.Rows.Count == 0)
                                        {
                                            row = dt.NewRow();
                                            dt.Rows.Add(row);
                                            row["tyshxydm"] = corpBasicInfo.LicenseNo;
                                        }
                                        else
                                        {
                                            row = dt.Rows[0];
                                            //if (!string.IsNullOrEmpty(corpBasicInfo.UpdateDate) && !string.IsNullOrEmpty(row["xgrqsj"].ToString2()))
                                            //    if (DateTime.Parse(row["xgrqsj"].ToString2()).ToString("yyyy-MM-dd") == DateTime.Parse(corpBasicInfo.UpdateDate).ToString("yyyy-MM-dd"))
                                            //    {
                                            //        continue;
                                            //    }
                                            //if (row["tag"].ToString2().IndexOf(tag) < 0)
                                            //{
                                            //    row["tag"] = row["tag"].ToString2().TrimEnd(',') + "," + tag;
                                            //}
                                            if (!string.IsNullOrEmpty(row["needUpdateFlag"].ToString()) && !row["needUpdateFlag"].ToBoolean())
                                            {
                                                continue;
                                            }
                                        }
                                        row["tag"] = tag;
                                        row["qyID"] = corpBasicInfo.CorpCode;
                                        row["qymc"] = corpBasicInfo.CorpName;
                                        row["zzjgdm"] = corpBasicInfo.CorpCode;
                                        row["yyzzzch"] = corpBasicInfo.LicenseNo;
                                        

                                        if (!string.IsNullOrEmpty(corpBasicInfo.ProvinceCode.ToString2()))
                                        {
                                            SqlParameterCollection sp = dataService.CreateSqlParameterCollection();

                                            row["Province"] = corpBasicInfo.ProvinceCode;
                                            sp.Add("@CodeInfo", corpBasicInfo.ProvinceCode.ToString2());
                                            string provinceCode = dataService.ExecuteSql("select top 1 Code from  UEPP_Code where CodeType='城市地区' and  CodeInfo=@CodeInfo", sp);
                                            if (!string.IsNullOrEmpty(provinceCode))
                                            {
                                                row["ProvinceID"] = provinceCode;
                                                sp.Clear();
                                                if (!string.IsNullOrEmpty(corpBasicInfo.CityCode.ToString2()))
                                                {
                                                    row["City"] = corpBasicInfo.CityCode;

                                                    sp.Add("@CodeInfo", corpBasicInfo.CityCode.ToString2());
                                                    sp.Add("@parentCode", provinceCode);
                                                    string cityCode = dataService.ExecuteSql("select top 1 Code from  UEPP_Code where CodeType='城市地区' and ParentCode=@parentCode and  CodeInfo=@CodeInfo", sp);
                                                    if (!string.IsNullOrEmpty(cityCode))
                                                    {
                                                        row["CityID"] = cityCode;
                                                    }

                                                    sp.Clear();
                                                    if (!string.IsNullOrEmpty(corpBasicInfo.CountyCode.ToString2()))
                                                    {
                                                        row["County"] = corpBasicInfo.CountyCode;

                                                        sp.Add("@CodeInfo", corpBasicInfo.CountyCode.ToString2());
                                                        sp.Add("@parentCode", cityCode);
                                                        string countyCode = dataService.ExecuteSql("select top 1 Code from  UEPP_Code where CodeType='城市地区' and ParentCode=@parentCode and  CodeInfo=@CodeInfo", sp);
                                                        if (!string.IsNullOrEmpty(countyCode))
                                                        {
                                                            row["CountyID"] = countyCode;
                                                        }

                                                    }
                                                }
                                            }
                                        }

                                        row["zcdd"] = corpBasicInfo.RegAddress;
                                        row["xxdd"] = corpBasicInfo.CorpAddress;
                                        if (!string.IsNullOrEmpty(corpBasicInfo.FoundDate))
                                            row["clrq"] = corpBasicInfo.FoundDate;
                                        row["jjxz"] = corpBasicInfo.CorpTypeDesc.ToString2().Trim();
                                        if (!string.IsNullOrEmpty(corpBasicInfo.CorpTypeDesc))
                                        {
                                            SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                            sp.Add("@CodeInfo", corpBasicInfo.CorpTypeDesc.ToString2().Trim());
                                            string jjxzID = dataService.ExecuteSql("select * from  UEPP_Code where CodeType='企业经济性质' and  CodeInfo=@CodeInfo", sp);
                                            if (!string.IsNullOrEmpty(jjxzID))
                                            {
                                                row["jjxzID"] = jjxzID;
                                            }
                                        }

                                        row["zczb"] = corpBasicInfo.RegCapital;
                                        row["cz"] = corpBasicInfo.Fax;
                                        row["lxdh"] = corpBasicInfo.LinkPhone;
                                        row["fddbr"] = corpBasicInfo.LegalMan;

                                        if (!string.IsNullOrEmpty(corpBasicInfo.UpdateDate))
                                            row["xgrqsj"] = corpBasicInfo.UpdateDate;
                                        else
                                            row["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd");

                                        row["DataState"] = 0;
                                        allCount_qyxx++;
                                        if (!dataService.Submit_uepp_qyjbxx(dt))
                                        {
                                            row_SaveDataLog["SaveState"] = 0;
                                            row_SaveDataLog["Msg"] = "从江苏建设公共基础数据平台获取的单位：" + corpBasicInfo.CorpName + "," + corpBasicInfo.CorpCode + "，基本信息保存失败！";
                                            continue;
                                        }
                                        else
                                        {
                                            success_count_qyxx++;
                                            row_SaveDataLog["SaveState"] = 1;
                                            row_SaveDataLog["Msg"] = "";
                                        }
                                        #endregion
                                    }
                                    catch (Exception ex)
                                    {
                                        row_SaveDataLog["SaveState"] = 0;
                                        row_SaveDataLog["Msg"] = ex.Message;
                                    }
                                }


                            }
                        }
                        #endregion

                        #region  获取企业证书信息
                        CorpCertInfoBody corpCertInfoArr = new CorpCertInfoBody(); //企业证书信息
                        bytes = newDataService.getCorpCert(userID, aCityCode, xzqdm, certType[cttp], "0");
                        result = System.Text.Encoding.UTF8.GetString(bytes);

                        index = result.IndexOf("<ReturnInfo>");

                        if (index >= 0)
                        {
                            string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
                            if (string.IsNullOrEmpty(returnResult))
                            {
                                continue;
                            }
                            ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
                            if (!returnInfo.Status)
                            {
                                continue;
                            }
                        }


                        index = result.IndexOf("<CorpCertInfo>");
                        if (index >= 0)
                        {
                            string corpCertInfoString = result.Substring(index, result.LastIndexOf("</CorpCertInfo>") - index + 15);
                            corpCertInfoArr = helper.DeserializeXML<CorpCertInfoBody>(
                                "<CorpCertInfoBody><CorpCertInfoArray>" + corpCertInfoString + "</CorpCertInfoArray></CorpCertInfoBody>");
                        }

                        CorpCertQualBody corpCertQualArr = new CorpCertQualBody();  //企业资质
                        index = result.IndexOf("<CorpCertQual>");

                        if (index >= 0)
                        {
                            string corpCertQualString = result.Substring(index, result.LastIndexOf("</CorpCertQual>") - index + 15);
                            corpCertQualArr = helper.DeserializeXML<CorpCertQualBody>(
                                "<CorpCertQualBody><CorpCertQualArray>" + corpCertQualString + "</CorpCertQualArray></CorpCertQualBody>");

                        }

                        #region 更新企业资质(TCorpCertQual)
                        if (corpCertQualArr != null)
                        {
                            foreach (CorpCertQual corpCertQual in corpCertQualArr.array)
                            {
                                //if (corpCertQual.CertType == "房地产开发")
                                //    break;
                                //for (int i = 0; i < dt_jsdw_zzmx.Rows.Count; i++)
                                //{
                                //    if (!list.Exists(p => p.CertType == "房地产开发" && dt_jsdw_zzmx.Rows[i]["csywlxID"].ToString2() == "11"
                                //        && (p.TradeType == "不分行业" || p.TradeType == dt_jsdw_zzmx.Rows[i]["zzxl"].ToString2())
                                //        && p.MajorType == dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2()))
                                //    {
                                //        dt_jsdw_zzmx.Rows[i].Delete();
                                //    }
                                //}

                                try
                                {
                                    if (corpCertQual.CorpCode.Length == 9)
                                    {
                                        corpCertQual.CorpCode = corpCertQual.CorpCode.Substring(0, 8) + '-' + corpCertQual.CorpCode.Substring(8, 1);
                                    }
                                    //检查该企业是否存在社会信用代码，若存在，则转化为社会信用代码
                                    if (corpCertQual.CorpCode.Length == 10)
                                    {
                                        string qyShxydm = dataService.Get_UEPP_Qyjbxx_Shxydm(corpCertQual.CorpCode);
                                        if (!string.IsNullOrEmpty(qyShxydm))
                                        {
                                            corpCertQual.CorpCode = qyShxydm;
                                        }
                                    }

                                    string csywlxID = "", csywlx = "";
                                    switch (corpCertQual.CertType)
                                    {
                                        //施工
                                        case "建筑业":
                                            csywlxID = "1";
                                            csywlx = "建筑施工";
                                            break;
                                        case "城市园林绿化":
                                            csywlxID = "3";
                                            csywlx = "园林绿化";
                                            break;
                                        case "设计与施工一体化":
                                            csywlxID = "2";
                                            csywlx = "设计施工一体化";
                                            break;
                                        case "房屋拆迁":
                                            csywlxID = "13";
                                            csywlx = "房屋拆迁";
                                            break;
                                        case "安全生产许可证":
                                            csywlxID = "14";
                                            csywlx = "安全生产许可证";
                                            break;
                                        //勘察
                                        case "工程勘察":
                                            csywlxID = "5";
                                            csywlx = "工程勘察";
                                            break;
                                        //设计
                                        case "工程设计":
                                            csywlxID = "6";
                                            csywlx = "工程设计";
                                            break;
                                        case "城市规划":
                                            csywlxID = "18";
                                            csywlx = "城市规划";
                                            break;
                                        case "外商城市规划":
                                            csywlxID = "19";
                                            csywlx = "外商城市规划";
                                            break;

                                        //中介机构
                                        case "工程招标代理":
                                            csywlxID = "7";
                                            csywlx = "招标代理";
                                            break;
                                        case "工程监理":
                                            csywlxID = "4";
                                            csywlx = "工程监理";
                                            break;
                                        case "工程造价咨询":
                                            csywlxID = "8";
                                            csywlx = "造价咨询";
                                            break;
                                        case "工程质量检测":
                                            csywlxID = "9";
                                            csywlx = "工程检测";
                                            break;
                                        case "施工图审查":
                                            csywlxID = "15";
                                            csywlx = "施工图审查";
                                            break;
                                        case "房地产估价":
                                            csywlxID = "16";
                                            csywlx = "房地产估价";
                                            break;
                                        case "物业服务":
                                            csywlxID = "17";
                                            csywlx = "物业服务";
                                            break;
                                        default:
                                            break;
                                    }
                                    if (string.IsNullOrEmpty(csywlxID))
                                        continue;

                                    #region 企业从事业务类型

                                    DataTable dt_qycsyw = dataService.Get_uepp_Qycsyw_sjsgyth(corpCertQual.CorpCode, csywlxID);

                                    DataRow tempRow_qycsyw;

                                    if (dt_qycsyw.Rows.Count == 0)
                                    {
                                        tempRow_qycsyw = dt_qycsyw.NewRow();
                                        dt_qycsyw.Rows.Add(tempRow_qycsyw);
                                        tempRow_qycsyw["qyID"] = corpCertQual.CorpCode;
                                    }
                                    else
                                    {
                                        tempRow_qycsyw = dt_qycsyw.Rows[0];
                                    }

                                    tempRow_qycsyw["csywlxID"] = csywlxID;
                                    tempRow_qycsyw["csywlx"] = csywlx;

                                    tempRow_qycsyw["balxID"] = "1";
                                    tempRow_qycsyw["balx"] = "长期备案";
                                    tempRow_qycsyw["DataState"] = "0";
                                    tempRow_qycsyw["tag"] = tag;

                                    if (!string.IsNullOrEmpty(corpCertQual.UpdateDate))
                                        tempRow_qycsyw["xgrqsj"] = corpCertQual.UpdateDate;
                                    else
                                        tempRow_qycsyw["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");



                                    if (dt_qycsyw.Rows.Count > 0)
                                    {
                                        dataService.Submit_uepp_qycsyw(dt_qycsyw);
                                    }
                                    #endregion

                                    DataTable dt_jsdw_zzmx = dataService.Get_uepp_zzmxxx_qyxx_nostatus(corpCertQual.CorpCode, csywlxID);

                                    int rowIndex = -1;

                                    for (int i = 0; i < dt_jsdw_zzmx.Rows.Count; i++)
                                    {
                                        //if (dt_jsdw_zzmx.Rows[i].RowState == DataRowState.Deleted)
                                        //    continue;

                                        if (csywlxID == dt_jsdw_zzmx.Rows[i]["csywlxID"].ToString2()
                                             && corpCertQual.CertCode == dt_jsdw_zzmx.Rows[i]["zsbh"].ToString2()
                                            && (
                                             corpCertQual.TradeType == "工程勘察综合类" && dt_jsdw_zzmx.Rows[i]["zzxl"].ToString2() == "综合类"
                                             || corpCertQual.TradeType == "工程勘察专业类" && dt_jsdw_zzmx.Rows[i]["zzxl"].ToString2() == "专业类"
                                             || corpCertQual.TradeType == "工程勘察劳务类" && dt_jsdw_zzmx.Rows[i]["zzxl"].ToString2() == "劳务类"
                                             || corpCertQual.TradeType == dt_jsdw_zzmx.Rows[i]["zzxl"].ToString2()
                                            )
                                            && (dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "岩土工程勘察" && corpCertQual.MajorType == "岩土工程（勘察）"
                                            || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "岩土工程设计" && corpCertQual.MajorType == "岩土工程（设计）"
                                              || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "岩土工程测试、监测、检测" && corpCertQual.MajorType == "岩土工程（物探测试检测监测）"
                                              || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "岩土工程咨询、监理" && corpCertQual.MajorType == "岩土工程（咨询监理）"

                                              || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "建筑装修装饰" && corpCertQual.MajorType == "建筑装饰装修工程"
                                              || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "模板作业" && corpCertQual.MajorType == "模板作业分包"
                                              || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "脚手架搭设作业" && corpCertQual.MajorType == "脚手架作业分包"
                                              || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == corpCertQual.MajorType
                                            )
                                            )
                                        {
                                            rowIndex = i;
                                            break;
                                        }
                                    }

                                    if (rowIndex < 0)
                                    {
                                        row = dt_jsdw_zzmx.NewRow();
                                        dt_jsdw_zzmx.Rows.Add(row);
                                        row["ID"] = dataService.Get_uepp_qyxxmx_NewID();
                                        row["qyID"] = corpCertQual.CorpCode;
                                        row["csywlx"] = csywlx;
                                        row["csywlxID"] = csywlxID;
                                    }
                                    else
                                    {
                                        row = dt_jsdw_zzmx.Rows[rowIndex];
                                        if ("qlmsoft".Equals(row["xgr"].ToString2()))
                                        {
                                            //如果是手动修改的数据，则不更新，如果需要更新，则把xgr栏设置为空
                                            continue;
                                        }
                                        if (!string.IsNullOrEmpty(corpCertQual.UpdateDate) && !string.IsNullOrEmpty(row["xgrqsj"].ToString2()))
                                        {
                                            int cmpFlag = DateTime.Compare(DateTime.Parse(corpCertQual.UpdateDate), row["xgrqsj"].ToDateTime());
                                            //if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(corpCertQual.UpdateDate).ToString("yyyy-MM-dd"))
                                            if (cmpFlag < 0)
                                            {
                                                continue;
                                            }
                                        }
                                    }
                                    if (corpCertQual.IsMaster == "主项")
                                        row["zzbz"] = "主项";
                                    else
                                        row["zzbz"] = "增项";

                                    if (corpCertQual.TradeType == "工程勘察综合类")
                                    {
                                        row["zzxl"] = "综合类";
                                        row["zzxlID"] = "9";
                                    }
                                    else
                                        if (corpCertQual.TradeType == "工程勘察专业类")
                                        {
                                            row["zzxl"] = "专业类";
                                            row["zzxlID"] = "10";
                                        }
                                        else
                                            if (corpCertQual.TradeType == "工程勘察劳务类")
                                            {
                                                row["zzxl"] = "劳务类";
                                                row["zzxlID"] = "11";
                                            }
                                            else
                                                if (corpCertQual.TradeType == "工程设计综合")
                                                {
                                                    row["zzxl"] = "综合资质";
                                                    row["zzxlID"] = "12";
                                                }
                                                else
                                                {
                                                    row["zzxl"] = corpCertQual.TradeType;
                                                    if (!string.IsNullOrEmpty(csywlxID))
                                                    {
                                                        string sql = @"select Code from UEPP_Code where  CodeType='企业资质序列' and ParentCodeType='企业从事业务类型'
 and ParentCode=@parentCode and CodeInfo=@CodeInfo ";
                                                        SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                                        sp.Add("@CodeInfo", corpCertQual.TradeType);
                                                        sp.Add("@parentCode", csywlxID);
                                                        string zzxlID = dataService.ExecuteSql(sql, sp);
                                                        if (!string.IsNullOrEmpty(zzxlID))
                                                            row["zzxlID"] = zzxlID;
                                                    }
                                                }

                                    if (corpCertQual.MajorType == "岩土工程（勘察）")
                                    {
                                        row["zzlb"] = "岩土工程勘察";
                                        row["zzlbID"] = "300";
                                    }
                                    else
                                        if (corpCertQual.MajorType == "岩土工程（设计）")
                                        {
                                            row["zzlb"] = "岩土工程设计";
                                            row["zzlbID"] = "301";
                                        }
                                        else
                                            if (corpCertQual.MajorType == "岩土工程（物探测试检测监测）")
                                            {
                                                row["zzlb"] = "岩土工程测试、监测、检测";
                                                row["zzlbID"] = "302";
                                            }
                                            else
                                                if (corpCertQual.MajorType == "岩土工程（咨询监理））")
                                                {
                                                    row["zzlb"] = "岩土工程咨询、监理";
                                                    row["zzlbID"] = "303";
                                                }
                                                else
                                                    if (corpCertQual.TradeType == "建筑装饰装修工程")
                                                    {
                                                        row["zzlb"] = "建筑装修装饰";
                                                        row["zzlbID"] = "33";
                                                    }
                                                    else if (corpCertQual.TradeType == "模板作业分包")
                                                    {
                                                        row["zzlb"] = "模板作业";
                                                        row["zzlbID"] = "128";
                                                    }
                                                    else
                                                        if (corpCertQual.TradeType == "脚手架作业分包")
                                                        {
                                                            row["zzlb"] = "脚手架搭设作业";
                                                            row["zzlbID"] = "127";
                                                        }
                                                        else
                                                        {
                                                            row["zzlb"] = corpCertQual.MajorType;
                                                            if (!string.IsNullOrEmpty(row["zzxlID"].ToString2().Trim()))
                                                            {
                                                                string sql = @"select Code from UEPP_Code where  CodeType='企业资质类别' and  ParentCodeType='企业资质序列'
 and ParentCode=@parentCode and (CodeInfo=@CodeInfo or CodeInfo=@CodeInfo1) ";
                                                                SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                                                sp.Add("@CodeInfo", corpCertQual.MajorType);
                                                                sp.Add("@parentCode", row["zzxlID"]);
                                                                sp.Add("@CodeInfo1", corpCertQual.MajorType.ToString2().Replace("分包", ""));
                                                                string zzlbID = dataService.ExecuteSql(sql, sp);
                                                                if (!string.IsNullOrEmpty(zzlbID))
                                                                    row["zzlbID"] = zzlbID;
                                                            }
                                                        }




                                    row["zzdj"] = corpCertQual.TitleLevel;
                                    //新增证书跟资质的一对多关联关系
                                    row["zsbh"] = corpCertQual.CertCode;

                                    if (!string.IsNullOrEmpty(corpCertQual.TitleLevel))
                                    {
                                        SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                        sp.Add("@CodeInfo", corpCertQual.TitleLevel);

                                        string sql = "select Code from UEPP_Code  where  CodeType='企业资质等级' and ParentCodeType='企业资质序列' and CodeInfo=@CodeInfo ";
                                        string zzdjID = dataService.ExecuteSql(sql, sp);
                                        if (!string.IsNullOrEmpty(zzdjID))
                                            row["zzdjID"] = zzdjID;
                                        else
                                        {
                                            // 新增代码表
                                        }
                                    }

                                    if (corpCertQual.Status == "1" || corpCertQual.Status == "有效")
                                    {
                                        row["DataState"] = 0;
                                    }
                                    else
                                    {
                                        row["DataState"] = -1;
                                        //还没到注销日期
                                        if (!string.IsNullOrEmpty(corpCertQual.CancelDate.ToString()))
                                        {
                                            int cancelDateFlag = DateTime.Compare(corpCertQual.CancelDate.ToDateTime(), DateTime.Now);
                                            if (cancelDateFlag > 0)
                                            {
                                                row["DataState"] = 0;
                                            }
                                        }
                                    }

                                    row["tag"] = tag;
                                    row["xgrqsj"] = corpCertQual.UpdateDate;

                                    if (!dataService.Submit_uepp_qyzzmx(dt_jsdw_zzmx))
                                    {
                                        Public.WriteLog("单位ID：" + corpCertQual.CorpCode + "，企业资质保存失败！");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Public.WriteLog("保存企业资质时出现异常：" + ex.Message);
                                }
                            }
                        }
                        #endregion

                        #region 更新企业资质证书信息(TCorpCertInfo)
                        if (corpCertInfoArr != null)
                        {
                            //List<CorpCertInfo> list = corpCertInfoArr.array.FindAll(p => p.CorpCode == corpBasicInfo.CorpCode);

                            //DataTable dt_jsdw_zzzs = dataService.Get_uepp_jsdw_zzzsxx(corpBasicInfo.CorpCode);

                            //for (int i = 0; i < dt_jsdw_zzzs.Rows.Count; i++)
                            //{
                            //    if (!list.Exists(p => p.CertType == "房地产开发" && p.CertCode == dt_jsdw_zzzs.Rows[i]["zsbh"].ToString2()
                            //        && p.CertType == "开发企业资质证书"))
                            //    {
                            //        dt_jsdw_zzzs.Rows[i].Delete();
                            //    }
                            //}

                            foreach (CorpCertInfo corpCertInfo in corpCertInfoArr.array)
                            {
                                try
                                {
                                    if (corpCertInfo.CorpCode.Length == 9)
                                    {
                                        corpCertInfo.CorpCode = corpCertInfo.CorpCode.Substring(0, 8) + '-' + corpCertInfo.CorpCode.Substring(8, 1);
                                    }
                                    //检查该企业是否存在社会信用代码，若存在，则转化为社会信用代码
                                    if (corpCertInfo.CorpCode.Length == 10)
                                    {
                                        string qyShxydm = dataService.Get_UEPP_Qyjbxx_Shxydm(corpCertInfo.CorpCode);
                                        if (!string.IsNullOrEmpty(qyShxydm))
                                        {
                                            corpCertInfo.CorpCode = qyShxydm;
                                        }
                                    }

                                    int rowIndex = -1;
                                    string csywlxID = "", csywlx = "";
                                    switch (corpCertInfo.CertType)
                                    {
                                        //施工
                                        case "建筑业":
                                            csywlxID = "1";
                                            csywlx = "建筑施工";
                                            break;
                                        case "城市园林绿化":
                                            csywlxID = "3";
                                            csywlx = "园林绿化";
                                            break;
                                        case "设计与施工一体化":
                                            csywlxID = "2";
                                            csywlx = "设计施工一体化";
                                            break;
                                        case "房屋拆迁":
                                            csywlxID = "13";
                                            csywlx = "房屋拆迁";
                                            break;
                                        case "安全生产许可证":
                                            csywlxID = "14";
                                            csywlx = "安全生产许可证";
                                            break;
                                        //勘察
                                        case "工程勘察":
                                            csywlxID = "5";
                                            csywlx = "工程勘察";
                                            break;
                                        //设计
                                        case "工程设计":
                                            csywlxID = "6";
                                            csywlx = "工程设计";
                                            break;
                                        case "城市规划":
                                            csywlxID = "18";
                                            csywlx = "城市规划";
                                            break;
                                        case "外商城市规划":
                                            csywlxID = "19";
                                            csywlx = "外商城市规划";
                                            break;

                                        //中介机构
                                        case "工程招标代理":
                                            csywlxID = "7";
                                            csywlx = "招标代理";
                                            break;
                                        case "工程监理":
                                            csywlxID = "4";
                                            csywlx = "工程监理";
                                            break;
                                        case "工程造价咨询":
                                            csywlxID = "8";
                                            csywlx = "造价咨询";
                                            break;
                                        case "工程质量检测":
                                            csywlxID = "9";
                                            csywlx = "工程检测";
                                            break;
                                        case "施工图审查":
                                            csywlxID = "15";
                                            csywlx = "施工图审查";
                                            break;
                                        case "房地产估价":
                                            csywlxID = "16";
                                            csywlx = "房地产估价";
                                            break;
                                        case "物业服务":
                                            csywlxID = "17";
                                            csywlx = "物业服务";
                                            break;
                                        default:
                                            break;
                                    }
                                    if (string.IsNullOrEmpty(csywlxID))
                                        continue;

                                    DataTable dt_qy_zzzs = dataService.Get_uepp_zzzsxx_qyxx(corpCertInfo.CorpCode);

                                    for (int i = 0; i < dt_qy_zzzs.Rows.Count; i++)
                                    {
                                        //if (dt_jsdw_zzzs.Rows[i].RowState == DataRowState.Deleted)
                                        //    continue;
                                        //转换过的从事业务类型（企业资质类别）比较
                                        if (csywlx == dt_qy_zzzs.Rows[i]["csywlx"].ToString2()
                                            && dt_qy_zzzs.Rows[i]["zsbh"].ToString2() == corpCertInfo.CertCode)
                                        {
                                            rowIndex = i;
                                            break;
                                        }
                                        /**
                                        if (corpCertInfo.CertType == dt_qy_zzzs.Rows[i]["csywlx"].ToString2()
                                            && dt_qy_zzzs.Rows[i]["zsbh"].ToString2() == corpCertInfo.CertCode
                                            && dt_qy_zzzs.Rows[i]["csywlx"].ToString2() == corpCertInfo.CertType)
                                        {
                                            rowIndex = i;
                                            break;
                                        }*/
                                    }

                                    if (rowIndex < 0)
                                    {
                                        row = dt_qy_zzzs.NewRow();
                                        dt_qy_zzzs.Rows.Add(row);
                                        row["zsjlId"] = dataService.Get_uepp_qyQyzs_NewID();
                                        row["qyID"] = corpCertInfo.CorpCode;
                                    }
                                    else
                                    {
                                        row = dt_qy_zzzs.Rows[rowIndex];
                                        if ("qlmsoft".Equals(row["xgr"].ToString2()))
                                        {
                                            //如果是手动修改的数据，则不更新，如果需要更新，则把xgr栏设置为空
                                            continue;
                                        }
                                        if (!string.IsNullOrEmpty(corpCertInfo.UpdateDate))
                                            if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(corpCertInfo.UpdateDate).ToString("yyyy-MM-dd"))
                                            {
                                                continue;
                                            }
                                    }

                                    row["csywlx"] = csywlx;
                                    row["csywlxID"] = csywlxID;
                                    //增加证书正本流水号， 证书正本流水号为空的资质不用显示，跟住建部、省厅保持一致
                                    row["PrintNo"] = corpCertInfo.PrintNo;

                                    //if (!string.IsNullOrEmpty(corpCertInfo.CertType))
                                    //{
                                    //    SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                    //    sp.Add("@parentCode", csywlxID);
                                    //    sp.Add("@CodeInfo", corpCertInfo.CertType);

                                    //    string sql = @"select * from UEPP_Code  where  CodeType ='企业证书类型' and ParentCodeType='企业从事业务类型' and ParentCode=@parentCode and CodeInfo=@CodeInfo ";
                                    //    string zslxID = dataService.ExecuteSql(sql, sp);
                                    //    if (!string.IsNullOrEmpty(zslxID))
                                    //    {
                                    //        row["zslxID"] = zslxID;
                                    //    }
                                    //}
                                    //row["zslx"] = "开发企业资质证书";

                                    row["sfzzz"] = "1";
                                    string zslx = "", zslxID = "";
                                    switch (corpCertInfo.CertType)
                                    {
                                        //施工
                                        case "建筑业":
                                            zslxID = "10";
                                            zslx = "建筑业资质证";
                                            break;
                                        case "城市园林绿化":
                                            zslxID = "30";
                                            zslx = "园林绿化资质证";
                                            break;
                                        case "设计与施工一体化":
                                            zslxID = "20";
                                            zslx = "设计施工一体化资质证";
                                            break;
                                        case "房屋拆迁":
                                            zslxID = "130";
                                            zslx = "房屋拆迁资质证";
                                            break;
                                        case "安全生产许可证":
                                            zslxID = "140";
                                            zslx = "安全生产许可证";
                                            break;
                                        //勘察
                                        case "工程勘察":
                                            zslxID = "51";
                                            csywlx = "省工程勘察资质证";
                                            break;
                                        //设计
                                        case "工程设计":
                                            zslxID = "61";
                                            zslx = "省工程设计资质证";
                                            break;
                                        case "城市规划":
                                            zslxID = "18";
                                            zslx = "城市规划资质证";
                                            break;
                                        case "外商城市规划":
                                            zslxID = "19";
                                            zslx = "外商城市规划资质证";
                                            break;

                                        //中介机构
                                        case "工程招标代理":
                                            zslxID = "70";
                                            zslx = "招标代理资质证";
                                            break;
                                        case "工程监理":
                                            zslxID = "40";
                                            zslx = "工程监理资质证";
                                            break;
                                        case "工程造价咨询":
                                            zslxID = "80";
                                            csywlx = "造价咨询资质证";
                                            break;
                                        case "工程质量检测":
                                            zslxID = "90";
                                            zslx = "工程检测资质证";
                                            break;
                                        case "施工图审查":
                                            zslxID = "150";
                                            csywlx = "施工图审查资质证";
                                            break;
                                        case "房地产估价":
                                            zslxID = "160";
                                            zslx = "房地产估价资质证";
                                            break;
                                        case "物业服务":
                                            zslxID = "170";
                                            zslx = "物业服务资质证";
                                            break;
                                        default:
                                            break;
                                    }
                                    row["zslxID"] = zslxID;
                                    row["zslx"] = zslx;
                                    row["zsbh"] = corpCertInfo.CertCode;
                                    if (!string.IsNullOrEmpty(corpCertInfo.ValidDate.Trim()))
                                        row["zsyxzrq"] = corpCertInfo.ValidDate;
                                    if (!string.IsNullOrEmpty(corpCertInfo.IssueDate.Trim()))
                                    {
                                        row["fzrq"] = corpCertInfo.IssueDate;
                                        row["zsyxqrq"] = corpCertInfo.IssueDate;
                                    }

                                    row["fzdw"] = corpCertInfo.IssueOrgan;
                                    row["xgrqsj"] = corpCertInfo.UpdateDate;
                                    row["xgr"] = "定时服务";
                                    row["tag"] = tag;
                                    row["DataState"] = 0;

                                    if (!dataService.Submit_uepp_qyzzzs(dt_qy_zzzs))
                                    {
                                        Public.WriteLog("单位ID：" + corpCertInfo.CorpCode + "，企业资质证书信息保存失败！");
                                    }

                                }
                                catch (Exception ex)
                                {
                                    Public.WriteLog("保存企业资质证书信息时出现异常：" + corpCertInfo.CorpCode + ex.Message);
                                }
                            }
                        }
                        #endregion

                        #endregion
                    }
                }
                if (dt_SaveDataLog.Rows.Count > 0)
                    dataService.Submit_SaveDataLog(dt_SaveDataLog);

                DateTime endTime = DateTime.Now;
                TimeSpan span = compareDateTime(beginTime, endTime);
                Public.WriteLog(string.Format("结束YourTask_PullDataFromSxxzx_Jiangsu_qyxx任务:{0}，历时：{1}时{2}分{3}秒", endTime.ToString("yyyy-MM-dd HH:mm:ss"), span.Hours, span.Minutes, span.Seconds));


                row_DataJkDataDetail_qyxx["allCount"] = allCount_qyxx;
                row_DataJkDataDetail_qyxx["successCount"] = success_count_qyxx;
                row_DataJkDataDetail_qyxx["IsOk"] = 1;
                row_DataJkDataDetail_qyxx["ErrorMsg"] = "";

                if (dt_DataJkDataDetail_qyxx.Rows.Count > 0)
                    dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_qyxx);
            }
            catch (Exception ex)
            {
                row_DataJkDataDetail_qyxx["allCount"] = allCount_qyxx;
                row_DataJkDataDetail_qyxx["successCount"] = success_count_qyxx;
                row_DataJkDataDetail_qyxx["IsOk"] = 0;
                row_DataJkDataDetail_qyxx["ErrorMsg"] = "从江苏建设公共基础数据平台获取企业信息时出现异常:" + ex.Message;

                if (dt_DataJkDataDetail_qyxx.Rows.Count > 0)
                    dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_qyxx);
            }
        }


        /// <summary>
        /// 从江苏建设公共基础数据平台拉取人员（注册执业人员）信息
        /// </summary>
        void YourTask_PullDataFromSxxzx_Ryxx_Zczyry(string DataJkLogID)
        {
            try
            {
                DateTime beginTime = DateTime.Now;
                Public.WriteLog("开始执行YourTask_PullDataFromSxxzx_Ryxx_Zczyry：" + beginTime.ToString("yyyy-MM-dd HH:mm:ss"));
                string tag = Tag.江苏建设公共基础数据平台.ToString();
                string userID = "wxszjj01";
                string[] regType = { "1001", "1002", "1101", "1102", "1210", "1220", "1310", "1320", "1330", "1410", "1420", "1511", "1512", "1521", "1522", "1530", "1600", "1700", "1800", "1900", "2000", "2100", "2200" };


                DataService dataService = new DataService();
                DataTable dt_xzqhdm = dataService.Get_tbXzqdmDic();
                //DataTable dt_xzqhdm = dataService.Get_tbXzqdmDic_AllCountryExceptWuxi();
                XmlHelper helper = new XmlHelper();
                Base64EncodeHelper base64EncodeHelper = new Base64EncodeHelper();

                //往数据监控日志表项添加一条记录
                DataTable dt_DataJkDataDetail_ryxx = dataService.GetSchema_DataJkDataDetail();
                long Id_DataJkDataDetail_ryxx = dataService.Get_DataJkDataDetailNewID().ToInt64();

                int allCount_ryxx = 0, success_count_ryxx = 0;
                DataRow row_DataJkDataDetail_ryxx = dt_DataJkDataDetail_ryxx.NewRow();
                dt_DataJkDataDetail_ryxx.Rows.Add(row_DataJkDataDetail_ryxx);

                row_DataJkDataDetail_ryxx["ID"] = Id_DataJkDataDetail_ryxx++;
                row_DataJkDataDetail_ryxx["DataJkLogID"] = DataJkLogID;
                row_DataJkDataDetail_ryxx["tableName"] = "UEPP_Ryjbxx";
                row_DataJkDataDetail_ryxx["MethodName"] = "getPersonRegCert_Inc";
                row_DataJkDataDetail_ryxx["bz"] = "从江苏建设公共基础数据平台拉取人员（注册执业人员）信息";

                NewDataService.NewDataService newdataService = new WxjzgcjczyTimerService.NewDataService.NewDataService();
                DataRow row;

                try
                {
                    DataTable dt_SaveDataLog = dataService.GetSchema_SaveDataLog();
                    foreach (DataRow row_xzqdm in dt_xzqhdm.Rows)
                    {
                        for (int retp = 0; retp < regType.Length; retp++)
                        {
                            string xzqdm = row_xzqdm["Code"].ToString2();
                            string citeCode = row_xzqdm["parentCode"].ToString2();
                            byte[] bytes;
                            string result = String.Empty;
                            int index = -1;

                            #region 获取注册执业人员信息
                           

                            //默认每次获取7天前到今天这段时间段更新的人员数据，如需要全量更新，请将App.config中的ZczyrybeginDate更改为其他值即可
                            string grobleStartDate = ConfigurationManager.AppSettings["ZczyrybeginDate"].ToString();
                            if("1900-01-01".Equals(grobleStartDate)){
                                grobleStartDate = DateTime.Now.AddDays(-14).ToString("yyyy-MM-dd");
                            }

                            //Public.WriteLog("获取" + tag + "注册执业人员信息：" + regType[retp] + ",xzqdm:" + xzqdm + " from " + grobleStartDate + " to " + DateTime.Now.ToString("yyyy-MM-dd"));
                            bytes = newdataService.getPersonRegCert_Inc(userID, citeCode, xzqdm, regType[retp], grobleStartDate, DateTime.Now.ToString("yyyy-MM-dd"), "0");
                            result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                            #endregion

                            index = result.IndexOf("<ReturnInfo>");
                            if (index >= 0)
                            {
                                string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
                                if (string.IsNullOrEmpty(returnResult))
                                {
                                    continue;
                                }

                                ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
                                if (!returnInfo.Status)
                                {
                                    continue;
                                }

                            }
                            else
                            {
                                continue;
                            }

                            index = -1;
                            index = result.IndexOf("<PersonRegCert>");
                            if (index >= 0)
                            {
                                string personRegCertString = result.Substring(index, result.LastIndexOf("</PersonRegCert>") - index + 16);
                                PersonRegCertBody personRegCertBody = helper.DeserializeXML<PersonRegCertBody>("<PersonRegCertBody><PersonRegCertArray>" + personRegCertString + "</PersonRegCertArray></PersonRegCertBody>");

                                #region 人员（基本信息+执业资格信息+证书+企业与人员及其执业资格对应关系）
                                if (personRegCertBody != null)
                                {
                                    Public.WriteLog("获取" + tag + "注册执业人员信息：" + regType[retp] + ",xzqdm:" + xzqdm + " from " + grobleStartDate + " to " + DateTime.Now.ToString("yyyy-MM-dd") + ",num:" + personRegCertBody.array.Count);
                                    //Public.WriteLog("获取注册执业人员数目：" + personRegCertBody.array.Count);
                                    foreach (PersonRegCert personRegCert in personRegCertBody.array)
                                    {
                                        string ryzyzglxID = String.Empty;
                                        string ryzclb = String.Empty;
                                        string ryzyzglx = String.Empty;
                                        string ryzslxID = String.Empty;
                                        string ryzslx = String.Empty;
                                        string zyzgdjID = String.Empty;
                                        string zyzgdj = String.Empty;

                                        DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                                        row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_ryxx["ID"];
                                        row_SaveDataLog["DataXml"] = "";
                                        row_SaveDataLog["PKID"] = personRegCert.IDCardNo;
                                        row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                                        if (string.IsNullOrEmpty(personRegCert.UpdateDate))
                                            personRegCert.UpdateDate = DateTime.Now.ToString("yyyy-MM-dd");


                                        try
                                        {
                                            #region 人员基本信息
                                            DataTable dt_ryxx = dataService.Get_UEPP_Ryjbxx(personRegCert.IDCardNo);
                                            bool needUpdateBasicInfo = true;
                                            if (dt_ryxx.Rows.Count == 0)
                                            {
                                                row = dt_ryxx.NewRow();
                                                dt_ryxx.Rows.Add(row);
                                                row["ryID"] = personRegCert.IDCardNo;
                                            }
                                            else
                                            { 
                                                row = dt_ryxx.Rows[0];
                                                if (!string.IsNullOrEmpty(row["xgrqsj"].ToString2()) && !string.IsNullOrEmpty(personRegCert.UpdateDate))
                                                    if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(personRegCert.UpdateDate).ToString("yyyy-MM-dd"))
                                                    {
                                                        //continue;
                                                        needUpdateBasicInfo = false;
                                                    }
                                            }
                                            if (needUpdateBasicInfo)
                                            {
                                                row["tag"] = tag;
                                                row["xm"] = personRegCert.PersonName;
                                                switch (personRegCert.IDCardType)
                                                {
                                                    case "身份证":
                                                        row["zjlxID"] = "1";
                                                        break;
                                                    case "护照":
                                                        row["zjlxID"] = "3";
                                                        break;
                                                    case "军官证":
                                                        row["zjlxID"] = "2";
                                                        break;
                                                    case "台湾居民身份证":
                                                        row["zjlxID"] = "4";
                                                        break;
                                                    case "香港永久性居民身份证":
                                                        row["zjlxID"] = "5";
                                                        break;
                                                    case "警官证":
                                                        row["zjlxID"] = "6";
                                                        break;
                                                    case "其他":
                                                        row["zjlxID"] = "9";
                                                        break;
                                                }

                                                row["zjlx"] = personRegCert.IDCardType;
                                                row["zjhm"] = personRegCert.IDCardNo;

                                                if (string.IsNullOrEmpty(personRegCert.UpdateDate))
                                                    personRegCert.UpdateDate = DateTime.Now.ToString("yyyy-MM-dd");
                                                row["xgrqsj"] = personRegCert.UpdateDate;
                                                row["xgr"] = "定时服务";
                                                row["DataState"] = 0;
                                                row["sfzsmj"] = base64EncodeHelper.Base64DecodeToBytes(personRegCert.PhotoBase64);
                                                row["AJ_EXISTINIDCARDS"] = "2";
                                                row["AJ_IsRefuse"] = "0";
                                                row["UpdateTime"] = DateTime.Now;
                                                allCount_ryxx++;
                                                if (!dataService.Submit_uepp_ryjbxx(dt_ryxx))
                                                {
                                                    //Public.WriteLog("建设单位人员信息保存失败，ryID：" + personBasicInfo.IDCardNo);
                                                    row_SaveDataLog["SaveState"] = 0;
                                                    row_SaveDataLog["Msg"] = "建设单位人员信息保存失败，ryID：" + personRegCert.IDCardNo;
                                                }
                                                else
                                                {
                                                    success_count_ryxx++;

                                                    row_SaveDataLog["SaveState"] = 1;
                                                    row_SaveDataLog["Msg"] = "";
                                                }
                                            #endregion

                                                dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                                            }
                                            

                                            #region 人员执业资格

                                            #region 注册人员注册类型及等级DRegType
                                            switch (personRegCert.RegType)
                                            {
                                                case "1001":
                                                    ryzclb = "一级注册建筑师";
                                                    ryzyzglxID = "51";
                                                    ryzyzglx = "注册建筑师";
                                                    ryzslxID = "151";
                                                    ryzslx = "注册建筑师资格证";

                                                    zyzgdjID = "21";
                                                    zyzgdj = "壹级";
                                                    break;
                                                case "1002":
                                                    ryzclb = "二级注册建筑师";
                                                    ryzyzglxID = "51";
                                                    ryzyzglx = "注册建筑师";

                                                    ryzslxID = "151";
                                                    ryzslx = "注册建筑师资格证";
                                                    zyzgdjID = "22";
                                                    zyzgdj = "贰级";
                                                    break;
                                                case "1101":
                                                    ryzclb = "一级注册结构工程师";
                                                    ryzyzglxID = "61";
                                                    ryzyzglx = "注册结构师";
                                                    ryzslxID = "161";
                                                    ryzslx = "注册结构师资格证";

                                                    zyzgdjID = "26";
                                                    zyzgdj = "壹级";
                                                    break;
                                                case "1102":
                                                    ryzclb = "二级注册结构工程师";
                                                    ryzyzglxID = "61";
                                                    ryzyzglx = "注册结构师";
                                                    ryzslxID = "161";
                                                    ryzslx = "注册结构师资格证";
                                                    zyzgdjID = "27";
                                                    zyzgdj = "贰级";
                                                    break;
                                                case "1210":
                                                    ryzclb = "注册土木工程师（岩土）";
                                                    ryzyzglxID = "73";

                                                    ryzyzglx = "注册土木工程师（岩土）";
                                                    ryzslxID = "731";
                                                    ryzslx = "注册土木工程师（岩土）资格证";

                                                    break;
                                                case "1220":
                                                    ryzclb = "注册土木工程师（港口与航道工程）";
                                                    ryzyzglxID = "84";

                                                    ryzyzglx = "注册土木工程师（港口与航道工程）";
                                                    ryzslxID = "841";
                                                    ryzslx = "注册土木工程师（港口与航道工程）资格证";

                                                    break;
                                                case "1310":
                                                    ryzclb = "注册公用设备工程师（暖通空调）";
                                                    ryzyzglxID = "74";

                                                    ryzyzglx = "注册公用设备工程师（暖通空调）";
                                                    ryzslxID = "741";
                                                    ryzslx = "注册公用设备工程师（暖通空调）资格证";
                                                    break;
                                                case "1320":
                                                    ryzclb = "注册公用设备工程师（给水排水）";
                                                    ryzyzglxID = "75";
                                                    ryzyzglx = "注册公用设备工程师（给水排水）";
                                                    ryzslxID = "751";
                                                    ryzslx = "注册公用设备工程师（给水排水）资格证";
                                                    break;
                                                case "1330":
                                                    ryzclb = "注册公用设备工程师（动力）";
                                                    ryzyzglxID = "76";

                                                    ryzyzglx = "注册公用设备工程师（动力）";
                                                    ryzslxID = "761";
                                                    ryzslx = "注册公用设备工程师（动力）资格证";
                                                    break;
                                                case "1410":
                                                    ryzclb = "注册电气工程师（发输变电）";
                                                    ryzyzglxID = "77";

                                                    ryzyzglx = "注册电气工程师（发输变电）";
                                                    ryzslxID = "771";
                                                    ryzslx = "注册电气工程师（发输变电）资格证";
                                                    break;
                                                case "1420":
                                                    ryzclb = "注册电气工程师（供配电）";
                                                    ryzyzglxID = "78";
                                                    ryzyzglx = "注册电气工程师（供配电）";
                                                    ryzslxID = "781";
                                                    ryzslx = "注册电气工程师（供配电）资格证";
                                                    break;
                                                case "1511":
                                                    ryzclb = "一级注册建造师";
                                                    ryzyzglxID = "1";
                                                    ryzyzglx = "注册建造师";
                                                    ryzslxID = "11";
                                                    ryzslx = "注册建造师资格证";

                                                    zyzgdjID = "1";
                                                    zyzgdj = "壹级";

                                                    break;
                                                case "1512":
                                                    ryzclb = "二级注册建造师";
                                                    ryzyzglxID = "1";
                                                    ryzyzglx = "注册建造师";
                                                    ryzslxID = "11";
                                                    ryzslx = "注册建造师资格证";

                                                    zyzgdjID = "2";
                                                    zyzgdj = "贰级";

                                                    break;
                                                case "1521":
                                                    ryzclb = "注册建造师（一级临时）";
                                                    ryzyzglxID = "1";
                                                    ryzyzglx = "注册建造师";
                                                    ryzslxID = "11";
                                                    ryzslx = "注册建造师资格证";

                                                    zyzgdjID = "3";
                                                    zyzgdj = "壹级临时";
                                                    break;
                                                case "1522":
                                                    ryzclb = "注册建造师（二级临时）";
                                                    ryzyzglxID = "1";

                                                    ryzyzglx = "注册建造师";
                                                    ryzslxID = "11";
                                                    ryzslx = "注册建造师资格证";

                                                    zyzgdjID = "4";
                                                    zyzgdj = "贰级临时";

                                                    break;
                                                case "1530":
                                                    ryzclb = "小型项目管理师";
                                                    ryzyzglxID = "2";

                                                    ryzyzglx = "小型项目管理师";
                                                    ryzslxID = "21";
                                                    ryzslx = "小型项目管理师资格证";

                                                    zyzgdjID = "2";
                                                    zyzgdj = "小型";
                                                    break;
                                                case "1600":
                                                    ryzclb = "注册造价工程师";
                                                    ryzyzglxID = "41";

                                                    ryzyzglx = "注册造价工程师";
                                                    ryzslxID = "131";
                                                    ryzslx = "注册造价师资格证";
                                                    break;
                                                case "1700":
                                                    ryzclb = "注册监理工程师";
                                                    ryzyzglxID = "21";
                                                    ryzyzglx = "注册监理工程师";
                                                    ryzslxID = "91";
                                                    ryzslx = "注册监理师资格证";
                                                    break;
                                                case "1800":
                                                    ryzclb = "注册城市规划师";
                                                    ryzyzglxID = "79";

                                                    ryzyzglx = "注册城市规划师";
                                                    ryzslxID = "791";
                                                    ryzslx = "注册城市规划师资格证";
                                                    break;
                                                case "1900":
                                                    ryzclb = "注册化工工程师";
                                                    ryzyzglxID = "80";
                                                    ryzyzglx = "注册化工工程师";
                                                    ryzslxID = "801";
                                                    ryzslx = "注册化工工程师资格证";
                                                    break;
                                                case "2000":
                                                    ryzclb = "注册房地产估价师";
                                                    ryzyzglxID = "81";
                                                    ryzyzglx = "注册房地产估价师";
                                                    ryzslxID = "811";
                                                    ryzslx = "注册房地产估价师资格证";
                                                    break;
                                                case "2100":
                                                    ryzclb = "注册房地产经纪人";
                                                    ryzyzglxID = "82";
                                                    ryzyzglx = "注册房地产经纪人";
                                                    ryzslxID = "821";
                                                    ryzslx = "注册房地产经纪人资格证";
                                                    break;
                                                case "2200":
                                                    ryzclb = "物业管理师";
                                                    ryzyzglxID = "83";

                                                    ryzyzglx = "物业管理师";
                                                    ryzslxID = "831";
                                                    ryzslx = "物业管理师资格证";
                                                    break;


                                            }
                                            #endregion


                                            DataTable dt_ryzyzg = dataService.Get_uepp_Ryzyzg(personRegCert.IDCardNo);
                                            int rowIndex = -1;
                                            for (int i = 0; i < dt_ryzyzg.Rows.Count; i++)
                                            {
                                                if (dt_ryzyzg.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxID)
                                                {
                                                    rowIndex = i;
                                                    break;
                                                }
                                            }
                                            if (rowIndex < 0)
                                            {
                                                row = dt_ryzyzg.NewRow();
                                                dt_ryzyzg.Rows.Add(row);
                                                row["ryID"] = personRegCert.IDCardNo;
                                                row["ryzyzglxID"] = ryzyzglxID;
                                                row["ryzyzglx"] = ryzyzglx;

                                                row["balxID"] = 1;
                                                row["balx"] = "长期备案";

                                                row["DataState"] = 0;
                                                row["tag"] = tag;
                                                row["xgr"] = "定时服务";
                                                row["xgrqsj"] = personRegCert.UpdateDate;
                                                row["UpdateTime"] = DateTime.Now;
                                                dataService.Submit_uepp_Ryzyzg(dt_ryzyzg);
                                            }

                                            #endregion

                                            #region 人员证书基本信息
                                            DataTable dt_ryzs = dataService.Get_uepp_Ryzs(personRegCert.IDCardNo);
                                            rowIndex = -1;
                                            for (int i = 0; i < dt_ryzs.Rows.Count; i++)
                                            {
                                                if (dt_ryzs.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxID)
                                                {
                                                    rowIndex = i;
                                                    break;
                                                }
                                            }
                                            if (rowIndex < 0)
                                            {

                                                row = dt_ryzs.NewRow();
                                                dt_ryzs.Rows.Add(row);
                                                row["zsjlId"] = dataService.Get_uepp_RyzsNewID();
                                                row["ryID"] = personRegCert.IDCardNo;
                                                row["ryzyzglxID"] = ryzyzglxID;
                                                row["ryzyzglx"] = ryzyzglx;
                                                row["ryzslxID"] = ryzslxID;
                                                row["ryzslx"] = ryzslx;

                                            }
                                            else
                                            {
                                                row = dt_ryzs.Rows[rowIndex];

                                            }
                                            row["sfzzz"] = 1;
                                            row["zsbh"] = personRegCert.QualCertNo;
                                            if (!string.IsNullOrEmpty(personRegCert.IssueDate))
                                            {
                                                row["fzrq"] = personRegCert.IssueDate;
                                                row["zsyxqrq"] = personRegCert.IssueDate;
                                            }
                                            if (!string.IsNullOrEmpty(personRegCert.ValidDate))
                                                row["zsyxzrq"] = personRegCert.ValidDate;

                                            row["fzdw"] = personRegCert.IssueOrgan;

                                            row["Status"] = personRegCert.Status;
                                            if (!string.IsNullOrEmpty(personRegCert.QualIssueDate))
                                                row["QualIssueDate"] = personRegCert.QualIssueDate;
                                            row["StampNo"] = personRegCert.StampNo;
                                            row["RegNo"] = personRegCert.RegNo;

                                            row["DataState"] = 0;
                                            row["tag"] = tag;
                                            row["xgr"] = "定时服务";
                                            row["xgrqsj"] = personRegCert.UpdateDate;
                                            row["UpdateTime"] = DateTime.Now;
                                            dataService.Submit_uepp_Ryzs(dt_ryzs);

                                            #endregion

                                            #region 企业人员关系表

                                            DataTable dt_qyry = dataService.Get_uepp_Qyry(personRegCert.IDCardNo, personRegCert.CorpCode, ryzyzglxID);
                                            if (dt_qyry.Rows.Count == 0)
                                            {
                                                if (personRegCert.Status.ToString2() != "2")
                                                {
                                                    row = dt_qyry.NewRow();
                                                    dt_qyry.Rows.Add(row);
                                                    row["ryID"] = personRegCert.IDCardNo;
                                                    row["qyID"] = personRegCert.CorpCode;
                                                    row["ryzyzglxID"] = ryzyzglxID;
                                                    row["ryzyzglx"] = ryzyzglx;
                                                    row["DataState"] = 0;
                                                    row["tag"] = tag;
                                                    row["xgr"] = "定时服务";
                                                    row["xgrqsj"] = personRegCert.UpdateDate;
                                                    dataService.Submit_uepp_qyry(dt_qyry);
                                                }
                                            }
                                            else
                                            {
                                                if (personRegCert.Status.ToString2() == "2")
                                                {
                                                    foreach (DataRow item in dt_qyry.Rows)
                                                    {
                                                        item.Delete();
                                                    }

                                                    dataService.Submit_uepp_qyry(dt_qyry);
                                                }
                                            }
                                            #endregion


                                        }
                                        catch (Exception ex)
                                        {
                                            row_SaveDataLog["SaveState"] = 0;
                                            row_SaveDataLog["Msg"] = ex.Message;
                                            dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                                        }
                                    }

                                }
                                #endregion
                            }

                            #region 人员专业明细
                            index = -1;
                            index = result.IndexOf("<PersonRegMajor>");
                            if (index > 0)
                            {
                                string personRegMajorString = result.Substring(index, result.LastIndexOf("</PersonRegMajor>") - index + 17);
                                PersonRegMajorBody personRegMajorBody = helper.DeserializeXML<PersonRegMajorBody>("<PersonRegMajorBody><PersonRegMajorArray>" + personRegMajorString + "</PersonRegMajorArray></PersonRegMajorBody>");
                                if (personRegMajorBody != null)
                                {
                                    Public.WriteLog("人员专业明细size:" + personRegMajorBody.array.Count);
                                    foreach (PersonRegMajor personRegMajor in personRegMajorBody.array)
                                    {
                                        string ryzyzglxID = String.Empty;
                                        string ryzclb = String.Empty;
                                        string ryzyzglx = String.Empty;
                                        string ryzslxID = String.Empty;
                                        string ryzslx = String.Empty;
                                        string zyzgdjID = String.Empty;
                                        string zyzgdj = String.Empty;

                                        DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                                        row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_ryxx["ID"];
                                        row_SaveDataLog["DataXml"] = "";
                                        row_SaveDataLog["PKID"] = personRegMajor.IDCardNo;
                                        row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                                        if (string.IsNullOrEmpty(personRegMajor.UpdateDate))
                                            personRegMajor.UpdateDate = DateTime.Now.ToString("yyyy-MM-dd");

                                        try
                                        {
                                            #region 人员专业类型及等级DRegType
                                            switch (personRegMajor.RegType)
                                            {
                                                //case "1001":
                                                case "一级注册建筑师":
                                                    ryzclb = "一级注册建筑师";
                                                    ryzyzglxID = "51";
                                                    ryzyzglx = "注册建筑师";
                                                    ryzslxID = "151";
                                                    ryzslx = "注册建筑师资格证";

                                                    zyzgdjID = "21";
                                                    zyzgdj = "壹级";
                                                    break;
                                                //case "1002":
                                                case "二级注册建筑师":
                                                    ryzclb = "二级注册建筑师";
                                                    ryzyzglxID = "51";
                                                    ryzyzglx = "注册建筑师";

                                                    ryzslxID = "151";
                                                    ryzslx = "注册建筑师资格证";
                                                    zyzgdjID = "22";
                                                    zyzgdj = "贰级";
                                                    break;
                                                //case "1101":
                                                case "一级注册结构工程师":
                                                    ryzclb = "一级注册结构工程师";
                                                    ryzyzglxID = "61";
                                                    ryzyzglx = "注册结构师";
                                                    ryzslxID = "161";
                                                    ryzslx = "注册结构师资格证";

                                                    zyzgdjID = "26";
                                                    zyzgdj = "壹级";
                                                    break;
                                                //case "1102":
                                                case "二级注册结构工程师":
                                                    ryzclb = "二级注册结构工程师";
                                                    ryzyzglxID = "61";
                                                    ryzyzglx = "注册结构师";
                                                    ryzslxID = "161";
                                                    ryzslx = "注册结构师资格证";
                                                    zyzgdjID = "27";
                                                    zyzgdj = "贰级";
                                                    break;
                                                //case "1210":
                                                case "注册土木工程师（岩土）":
                                                    ryzclb = "注册土木工程师（岩土）";
                                                    ryzyzglxID = "73";

                                                    ryzyzglx = "注册土木工程师（岩土）";
                                                    ryzslxID = "731";
                                                    ryzslx = "注册土木工程师（岩土）资格证";

                                                    break;
                                                //case "1220":
                                                case "注册土木工程师（港口与航道工程）":
                                                    ryzclb = "注册土木工程师（港口与航道工程）";
                                                    ryzyzglxID = "84";

                                                    ryzyzglx = "注册土木工程师（港口与航道工程）";
                                                    ryzslxID = "841";
                                                    ryzslx = "注册土木工程师（港口与航道工程）资格证";

                                                    break;
                                                //case "1310":
                                                case "注册公用设备工程师（暖通空调）":
                                                    ryzclb = "注册公用设备工程师（暖通空调）";
                                                    ryzyzglxID = "74";

                                                    ryzyzglx = "注册公用设备工程师（暖通空调）";
                                                    ryzslxID = "741";
                                                    ryzslx = "注册公用设备工程师（暖通空调）资格证";
                                                    break;
                                                //case "1320":
                                                case "注册公用设备工程师（给水排水）":
                                                    ryzclb = "注册公用设备工程师（给水排水）";
                                                    ryzyzglxID = "75";
                                                    ryzyzglx = "注册公用设备工程师（给水排水）";
                                                    ryzslxID = "751";
                                                    ryzslx = "注册公用设备工程师（给水排水）资格证";
                                                    break;
                                                //case "1330":
                                                case "注册公用设备工程师（动力）":
                                                    ryzclb = "注册公用设备工程师（动力）";
                                                    ryzyzglxID = "76";

                                                    ryzyzglx = "注册公用设备工程师（动力）";
                                                    ryzslxID = "761";
                                                    ryzslx = "注册公用设备工程师（动力）资格证";
                                                    break;
                                                //case "1410":
                                                case "注册电气工程师（发输变电）":
                                                    ryzclb = "注册电气工程师（发输变电）";
                                                    ryzyzglxID = "77";

                                                    ryzyzglx = "注册电气工程师（发输变电）";
                                                    ryzslxID = "771";
                                                    ryzslx = "注册电气工程师（发输变电）资格证";
                                                    break;
                                                //case "1420":
                                                case "注册电气工程师（供配电）":
                                                    ryzclb = "注册电气工程师（供配电）";
                                                    ryzyzglxID = "78";
                                                    ryzyzglx = "注册电气工程师（供配电）";
                                                    ryzslxID = "781";
                                                    ryzslx = "注册电气工程师（供配电）资格证";
                                                    break;
                                                //case "1511":
                                                case "一级注册建造师":
                                                    ryzclb = "一级注册建造师";
                                                    ryzyzglxID = "1";
                                                    ryzyzglx = "注册建造师";
                                                    ryzslxID = "11";
                                                    ryzslx = "注册建造师资格证";

                                                    zyzgdjID = "1";
                                                    zyzgdj = "壹级";

                                                    break;
                                                //case "1512":
                                                case "二级注册建造师":
                                                    ryzclb = "二级注册建造师";
                                                    ryzyzglxID = "1";
                                                    ryzyzglx = "注册建造师";
                                                    ryzslxID = "11";
                                                    ryzslx = "注册建造师资格证";

                                                    zyzgdjID = "2";
                                                    zyzgdj = "贰级";

                                                    break;
                                                //case "1521":
                                                case "注册建造师（一级临时）":
                                                    ryzclb = "注册建造师（一级临时）";
                                                    ryzyzglxID = "1";
                                                    ryzyzglx = "注册建造师";
                                                    ryzslxID = "11";
                                                    ryzslx = "注册建造师资格证";

                                                    zyzgdjID = "3";
                                                    zyzgdj = "壹级临时";
                                                    break;
                                                //case "1522":
                                                case "（二级临时）":
                                                    ryzclb = "注册建造师（二级临时）";
                                                    ryzyzglxID = "1";

                                                    ryzyzglx = "注册建造师";
                                                    ryzslxID = "11";
                                                    ryzslx = "注册建造师资格证";

                                                    zyzgdjID = "4";
                                                    zyzgdj = "贰级临时";

                                                    break;
                                                //case "1530":
                                                case "小型项目管理师":
                                                    ryzclb = "小型项目管理师";
                                                    ryzyzglxID = "2";

                                                    ryzyzglx = "小型项目管理师";
                                                    ryzslxID = "21";
                                                    ryzslx = "小型项目管理师资格证";

                                                    zyzgdjID = "2";
                                                    zyzgdj = "小型";
                                                    break;
                                                //case "1600":
                                                case "注册造价工程师":
                                                    ryzclb = "注册造价工程师";
                                                    ryzyzglxID = "41";

                                                    ryzyzglx = "注册造价工程师";
                                                    ryzslxID = "131";
                                                    ryzslx = "注册造价师资格证";
                                                    break;
                                                //case "1700":
                                                case "注册监理工程师":
                                                    ryzclb = "注册监理工程师";
                                                    ryzyzglxID = "21";
                                                    ryzyzglx = "注册监理工程师";
                                                    ryzslxID = "91";
                                                    ryzslx = "注册监理师资格证";
                                                    break;
                                                //case "1800":
                                                case "注册城市规划师":
                                                    ryzclb = "注册城市规划师";
                                                    ryzyzglxID = "79";

                                                    ryzyzglx = "注册城市规划师";
                                                    ryzslxID = "791";
                                                    ryzslx = "注册城市规划师资格证";
                                                    break;
                                                //case "1900":
                                                case "注册化工工程师":
                                                    ryzclb = "注册化工工程师";
                                                    ryzyzglxID = "80";
                                                    ryzyzglxID = "注册化工工程师";
                                                    ryzslxID = "801";
                                                    ryzslx = "注册化工工程师资格证";
                                                    break;
                                                //case "2000":
                                                case "注册房地产估价师":
                                                    ryzclb = "注册房地产估价师";
                                                    ryzyzglxID = "81";
                                                    ryzyzglx = "注册房地产估价师";
                                                    ryzslxID = "811";
                                                    ryzslx = "注册房地产估价师资格证";
                                                    break;
                                                //case "2100":
                                                case "注册房地产经纪人":
                                                    ryzclb = "注册房地产经纪人";
                                                    ryzyzglxID = "82";
                                                    ryzyzglx = "注册房地产经纪人";
                                                    ryzslxID = "821";
                                                    ryzslx = "注册房地产经纪人资格证";
                                                    break;
                                                //case "2200":
                                                case "物业管理师":
                                                    ryzclb = "物业管理师";
                                                    ryzyzglxID = "83";

                                                    ryzyzglx = "物业管理师";
                                                    ryzslxID = "831";
                                                    ryzslx = "物业管理师资格证";
                                                    break;


                                            }
                                            #endregion

                                            #region 人员专业明细

                                            DataTable dt_ryzymx = dataService.Get_uepp_Ryzymx(personRegMajor.IDCardNo);
                                            int rowIndex = -1;
                                            bool needUpdateFlag = false;
                                            for (int i = 0; i < dt_ryzymx.Rows.Count; i++)
                                            {
                                                if (dt_ryzymx.Rows[i]["ryzyzglxID"].ToString2() == ryzyzglxID
                                                    && dt_ryzymx.Rows[i]["ryzslxID"].ToString2() == ryzslxID)
                                                {
                                                    rowIndex = i;
                                                    break;
                                                }
                                            }
                                            //Public.WriteLog("人员专业明细详细:" + personRegMajor.IDCardNo + "," + personRegMajor.RegType + ",rowIndex:" + rowIndex);
                                                
                                            if (rowIndex < 0)
                                            {
                                                row = dt_ryzymx.NewRow();
                                                dt_ryzymx.Rows.Add(row);
                                            }else{
                                                row = dt_ryzymx.Rows[rowIndex];
                                                if(!string.IsNullOrEmpty(personRegMajor.UpdateDate) && !string.IsNullOrEmpty(row["xgrqsj"].ToString()) &&
                                                    DateTime.Parse(personRegMajor.UpdateDate).CompareTo(DateTime.Parse(row["xgrqsj"].ToString())) > 0){
                                                    needUpdateFlag = true;
                                                } 
                                            }

                                            if (rowIndex < 0 || needUpdateFlag)
                                            {
                                                row["ryID"] = personRegMajor.IDCardNo;
                                                row["ryzyzglxID"] = ryzyzglxID;
                                                row["ryzyzglx"] = ryzyzglx;
                                                row["ryzslxID"] = ryzslxID;
                                                row["ryzslx"] = ryzslx;

                                                if (!string.IsNullOrEmpty(zyzgdjID))
                                                    row["zyzgdjID"] = zyzgdjID;
                                                row["zyzgdj"] = zyzgdj;
                                                row["DataState"] = 0;
                                                row["tag"] = tag;
                                                row["xgr"] = "定时服务";
                                                row["xgrqsj"] = personRegMajor.UpdateDate;
                                                row["UpdateTime"] = DateTime.Now;
                                                if (personRegMajor.IsMaster == "主项" || personRegMajor.IsMaster == "1")
                                                    row["zzbz"] = "主项";
                                                else
                                                    row["zzbz"] = "增项";
                                                dataService.Submit_uepp_Ryzymx(dt_ryzymx);
                                            }

                                            #endregion
                                        }
                                        catch (Exception ex)
                                        {
                                            row_SaveDataLog["SaveState"] = 0;
                                            row_SaveDataLog["Msg"] = ex.Message;
                                            dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                                        }
                                    }
                                }

                            }

                            #endregion
                        }
                    }

                    if (dt_SaveDataLog.Rows.Count > 0)
                        dataService.Submit_SaveDataLog(dt_SaveDataLog);

                    DateTime endTime = DateTime.Now;
                    TimeSpan span = compareDateTime(beginTime, endTime);
                    Public.WriteLog(string.Format("结束YourTask_PullDataFromSxxzx_Ryxx_Zczyry任务:{0}，历时：{1}时{2}分{3}秒", endTime.ToString("yyyy-MM-dd HH:mm:ss"), span.Hours, span.Minutes, span.Seconds));



                    row_DataJkDataDetail_ryxx["allCount"] = allCount_ryxx;
                    row_DataJkDataDetail_ryxx["successCount"] = success_count_ryxx;
                    row_DataJkDataDetail_ryxx["IsOk"] = 1;
                    row_DataJkDataDetail_ryxx["ErrorMsg"] = "";

                    if (dt_DataJkDataDetail_ryxx.Rows.Count > 0)
                        dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_ryxx);
                }
                catch (Exception ex)
                {
                    row_DataJkDataDetail_ryxx["allCount"] = allCount_ryxx;
                    row_DataJkDataDetail_ryxx["successCount"] = success_count_ryxx;
                    row_DataJkDataDetail_ryxx["IsOk"] = 0;
                    row_DataJkDataDetail_ryxx["ErrorMsg"] = ex.Message;
                    Public.WriteLog("ex.Message:" + ex.Message);
                    if (dt_DataJkDataDetail_ryxx.Rows.Count > 0)
                        dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_ryxx);

                }
            }
            catch (Exception ex)
            {
                Public.WriteLog("Exception:" + ex.Message);
            }
        }


        /// <summary>
        /// 从江苏建设公共基础数据平台拉取人员（安全生产管理人员）信息
        /// </summary>
        void YourTask_PullDataFromSxxzx_Ryxx_Aqscgl(string DataJkLogID)
        {
            try
            {
                DateTime beginTime = DateTime.Now;
                Public.WriteLog("开始执行YourTask_PullDataFromSxxzx_Ryxx_Aqscgl任务:" + beginTime.ToString("yyyy-MM-dd HH:mm:ss"));


                string tag = Tag.江苏建设公共基础数据平台.ToString();
                string userID = "wxszjj01";

                DataService dataService = new DataService();
                DataTable dt_xzqhdm = dataService.Get_tbXzqdmDic();
                XmlHelper helper = new XmlHelper();

                //往数据监控日志表项添加一条记录
                DataTable dt_DataJkDataDetail_ryxx = dataService.GetSchema_DataJkDataDetail();
                long Id_DataJkDataDetail_ryxx = dataService.Get_DataJkDataDetailNewID().ToInt64();

                int allCount_ryxx = 0, success_count_ryxx = 0;
                DataRow row_DataJkDataDetail_ryxx = dt_DataJkDataDetail_ryxx.NewRow();
                dt_DataJkDataDetail_ryxx.Rows.Add(row_DataJkDataDetail_ryxx);

                row_DataJkDataDetail_ryxx["ID"] = Id_DataJkDataDetail_ryxx++;
                row_DataJkDataDetail_ryxx["DataJkLogID"] = DataJkLogID;
                row_DataJkDataDetail_ryxx["tableName"] = "UEPP_Ryjbxx";
                row_DataJkDataDetail_ryxx["MethodName"] = "getPersonJobCert(aqsc)";
                row_DataJkDataDetail_ryxx["bz"] = "从江苏建设公共基础数据平台拉取人员（安全生产管理人员）信息";

                try
                {
                    DataTable dt_SaveDataLog = dataService.GetSchema_SaveDataLog();

                    NewDataService.NewDataService newdataService = new WxjzgcjczyTimerService.NewDataService.NewDataService();
                    DataRow row;
                    foreach (DataRow row_xzqdm in dt_xzqhdm.Rows)
                    {
                        string xzqdm = row_xzqdm["Code"].ToString2();

                        //Public.WriteLog("获取" + tag + "A类安全员：");
                        #region A类安全人员
                        string rygwlx = "021A";
                        string ryzyzglxid = "4";
                        string ryzyzglx = "企业A类人员";

                        byte[] bytes = newdataService.getPersonJobCert(userID, "320200", xzqdm, rygwlx, "0");

                        string result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);

                        var index = result.IndexOf("<ReturnInfo>");
                        if (index >= 0)
                        {
                            string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
                            if (string.IsNullOrEmpty(returnResult))
                            {
                                continue;
                            }

                            ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
                            if (!returnInfo.Status)
                            {
                                continue;
                            }

                        }
                        else
                        {
                            continue;
                        }
                        string str = result.Substring(result.IndexOf("</ReturnInfo>") + 13, result.IndexOf("</ResultSet>") - result.IndexOf("</ReturnInfo>") - 13);
                        if (string.IsNullOrEmpty(str))
                        {
                            continue;
                        }

                        index = result.IndexOf("<PersonJobCertInfo>");
                        if (index < 0)
                            continue;

                        string personJobCertInfoString = result.Substring(index, result.LastIndexOf("</PersonJobCertInfo>") - index + 20);
                        string msg = "";
                        DataTable dt_personJobCertInfo = helper.ConvertXMLToDataTable("<dataTable>" + personJobCertInfoString + "</dataTable>", out msg);
                        if (!string.IsNullOrEmpty(msg) || dt_personJobCertInfo == null || dt_personJobCertInfo.Rows.Count == 0)
                            continue;

                        foreach (DataRow item in dt_personJobCertInfo.Rows)
                        {
                            DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                            row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_ryxx["ID"];
                            row_SaveDataLog["DataXml"] = "";
                            row_SaveDataLog["PKID"] = item["IDCardNo"].ToString2();
                            row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");


                            int rowIndex = -1;
                            try
                            {
                                string updateDate = item["UpdateDate"].ToString2();

                                #region 人员基本信息

                                DataTable dt_ryxx = dataService.Get_UEPP_Ryjbxx(item["IDCardNo"].ToString2());
                                if (dt_ryxx.Rows.Count == 0)
                                {
                                    row = dt_ryxx.NewRow();
                                    dt_ryxx.Rows.Add(row);
                                    row["ryID"] = item["IDCardNo"].ToString2();
                                }
                                else
                                {
                                    row = dt_ryxx.Rows[0];
                                    if (!string.IsNullOrEmpty(row["xgrqsj"].ToString2()) && !string.IsNullOrEmpty(updateDate))
                                        if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(updateDate).ToString("yyyy-MM-dd"))
                                        {
                                            continue;
                                        }
                                }

                                if (string.IsNullOrEmpty(updateDate))
                                    updateDate = DateTime.Now.ToString("yyyy-MM-dd");
                                row["tag"] = tag;
                                row["xm"] = item["PersonName"];
                                switch (item["IDCardType"].ToString2())
                                {
                                    case "1":
                                        row["zjlxID"] = "1";
                                        row["zjlx"] = "身份证";
                                        break;
                                    case "2":
                                        row["zjlxID"] = "3";
                                        row["zjlx"] = "护照";
                                        break;
                                    case "3":
                                        row["zjlxID"] = "2";
                                        row["zjlx"] = "军官证";
                                        break;
                                    case "4":
                                        row["zjlxID"] = "4";
                                        row["zjlx"] = "台湾居民身份证";

                                        break;
                                    case "5":
                                        row["zjlxID"] = "5";
                                        row["zjlx"] = "香港永久性居民身份证";

                                        break;
                                    case "6":
                                        row["zjlxID"] = "6";
                                        row["zjlx"] = "警官证";

                                        break;
                                    case "其他":
                                        row["zjlxID"] = "9";
                                        row["zjlx"] = "其他";

                                        break;
                                }

                                row["zjhm"] = item["IDCardNo"];
                                //row["xb"] = item["Sex"];
                                //if (!string.IsNullOrEmpty(item["Birthday"].ToString2().Trim()))
                                //    row["csrq"] = item["Birthday"];
                                row["xgrqsj"] = updateDate;
                                row["xgr"] = "定时服务";
                                row["DataState"] = 0;
                                row["AJ_EXISTINIDCARDS"] = "2";
                                row["AJ_IsRefuse"] = "0";
                                allCount_ryxx++;

                                if (!dataService.Submit_uepp_ryjbxx(dt_ryxx))
                                {
                                    row_SaveDataLog["SaveState"] = 0;
                                    row_SaveDataLog["Msg"] = "建设单位人员信息保存失败，ryID：" + item["IDCardNo"].ToString2();

                                }
                                else
                                {
                                    success_count_ryxx++;
                                    row_SaveDataLog["SaveState"] = 1;
                                    row_SaveDataLog["Msg"] = "";

                                }
                                #endregion

                                #region 人员执业资格

                                DataTable dt_ryzyzg = dataService.Get_uepp_Ryzyzg(item["IDCardNo"].ToString2());
                                rowIndex = -1;
                                for (int i = 0; i < dt_ryzyzg.Rows.Count; i++)
                                {
                                    if (dt_ryzyzg.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                    {
                                        rowIndex = i;
                                        break;
                                    }
                                }
                                if (rowIndex < 0)
                                {
                                    row = dt_ryzyzg.NewRow();
                                    dt_ryzyzg.Rows.Add(row);

                                    row["ryID"] = item["IDCardNo"].ToString2();
                                    row["ryzyzglxID"] = ryzyzglxid;
                                    row["ryzyzglx"] = ryzyzglx;
                                    row["balxID"] = 1;
                                    row["balx"] = "长期备案";
                                    row["DataState"] = 0;
                                    row["tag"] = tag;
                                    row["xgr"] = "定时服务";
                                    row["xgrqsj"] = updateDate;
                                    dataService.Submit_uepp_Ryzyzg(dt_ryzyzg);
                                }

                                #endregion

                                #region 人员专业明细

                                //if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                //{
                                //    DataTable dt_ryzymx = dataService.Get_uepp_Ryzymx(item["IDCardNo"].ToString2());
                                //    rowIndex = -1;
                                //    for (int i = 0; i < dt_ryzymx.Rows.Count; i++)
                                //    {
                                //        if (dt_ryzymx.Rows[i]["ryzyzglxID"].ToString2() == ryzyzglxid && dt_ryzymx.Rows[i]["zyzglb"].ToString2() == item["CertMajor"].ToString2().Trim())
                                //        {
                                //            rowIndex = i;
                                //            break;
                                //        }
                                //    }
                                //    if (rowIndex < 0)
                                //    {
                                //        row = dt_ryzymx.NewRow();
                                //        dt_ryzymx.Rows.Add(row);
                                //        row["ryID"] = item["IDCardNo"].ToString2();
                                //        row["ryzyzglxID"] = ryzyzglxid;
                                //        row["ryzyzglx"] = ryzyzglx;

                                //        if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                //        {
                                //            row["zyzglb"] = item["CertMajor"].ToString2().Trim();

                                //            SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                //            sp.Add("@CodeInfo", item["CertMajor"].ToString2().Trim());
                                //            sp.Add("@parentCode", ryzyzglxid);

                                //            DataTable dt_code = dataService.GetTable("select * from UEPP_Code where CodeType='人员资质类别' and ParentCodeType='人员执业资格类型' and ParentCode=@parentCode and CodeInfo=@CodeInfo", sp);
                                //            if (dt_code.Rows.Count == 0)
                                //            {
                                //                dt_code.Rows.Add(dt_code.NewRow());
                                //                dt_code.Rows[0]["ParentCodeType"] = "人员执业资格类型";
                                //                dt_code.Rows[0]["ParentCode"] = ryzyzglxid;
                                //                dt_code.Rows[0]["CodeType"] = "人员资质类别";
                                //                dt_code.Rows[0]["CodeInfo"] = item["CertMajor"].ToString2().Trim();
                                //                dt_code.Rows[0]["Code"] = dataService.Get_uepp_Code_NewCode("人员资质类别");
                                //                dt_code.Rows[0]["CodeDesc"] = "";
                                //                dt_code.Rows[0]["OrderID"] = dt_code.Rows[0]["Code"];
                                //                dataService.Submit_uepp_code(dt_code);

                                //            }
                                //            row["zyzglbID"] = dt_code.Rows[0]["Code"];
                                //        }


                                //        row["zzbz"] = "主项";
                                //        row["DataState"] = 0;
                                //        row["tag"] = tag;
                                //        row["xgr"] = "定时服务";
                                //        row["xgrqsj"] = updateDate;
                                //        dataService.Submit_uepp_Ryzymx(dt_ryzymx);
                                //    }
                                //}

                                #endregion

                                #region 人员证书基本信息
                                DataTable dt_ryzs = dataService.Get_uepp_Ryzs(item["IDCardNo"].ToString2());
                                rowIndex = -1;
                                for (int i = 0; i < dt_ryzs.Rows.Count; i++)
                                {
                                    if (dt_ryzs.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                    {
                                        rowIndex = i;
                                        break;
                                    }
                                }
                                if (rowIndex < 0)
                                {

                                    row = dt_ryzs.NewRow();
                                    dt_ryzs.Rows.Add(row);
                                    row["zsjlId"] = dataService.Get_uepp_RyzsNewID();
                                    row["ryID"] = item["IDCardNo"].ToString2();
                                    row["ryzyzglxID"] = ryzyzglxid;
                                    row["ryzyzglx"] = ryzyzglx;
                                    row["ryzslxID"] = 41;
                                    row["ryzslx"] = "A类安全生产考核证";

                                }
                                else
                                {
                                    row = dt_ryzs.Rows[rowIndex];
                                }
                                row["sfzzz"] = 1;
                                row["zsbh"] = item["CertNo"].ToString2();
                                if (!string.IsNullOrEmpty(item["ValidDate"].ToString2()))
                                    row["zsyxzrq"] = item["ValidDate"].ToString2();
                                row["fzdw"] = item["IssueOrgan"].ToString2();
                                if (!string.IsNullOrEmpty(item["IssueDate"].ToString2()))
                                {
                                    row["fzrq"] = item["IssueDate"].ToString2();
                                    row["zsyxqrq"] = item["IssueDate"].ToString2();
                                }
                                row["Status"] = item["Status"];
                                if (dt_personJobCertInfo.Columns.Contains("QualIssueDate"))
                                    row["QualIssueDate"] = item["QualIssueDate"];
                                if (dt_personJobCertInfo.Columns.Contains("StampNo"))
                                    row["StampNo"] = item["StampNo"];
                                if (dt_personJobCertInfo.Columns.Contains("RegNo"))
                                    row["RegNo"] = item["RegNo"];

                                row["DataState"] = 0;
                                row["tag"] = tag;
                                row["xgr"] = "定时服务";
                                row["xgrqsj"] = updateDate;
                                dataService.Submit_uepp_Ryzs(dt_ryzs);
                                #endregion

                                #region 企业人员关系表

                                DataTable dt_qyry = dataService.Get_uepp_Qyry(item["IDCardNo"].ToString2(), Public.ZzjgdmToStandard(item["CorpCode"].ToString2()), ryzyzglxid);
                                if (dt_qyry.Rows.Count == 0)
                                {
                                    if (item["Status"].ToString2() != "2")
                                    {
                                        row = dt_qyry.NewRow();
                                        dt_qyry.Rows.Add(row);
                                        row["ryID"] = item["IDCardNo"].ToString2();
                                        row["qyID"] = item["CorpCode"].ToString2();
                                        row["ryzyzglxID"] = ryzyzglxid;
                                        row["ryzyzglx"] = ryzyzglx;
                                        row["DataState"] = 0;
                                        row["tag"] = tag;
                                        row["xgr"] = "定时服务";
                                        row["xgrqsj"] = updateDate;
                                        dataService.Submit_uepp_qyry(dt_qyry);
                                    }
                                }
                                else
                                {
                                    if (item["Status"].ToString2() == "2")
                                    {
                                        dt_qyry.Rows[0].Delete();
                                        dataService.Submit_uepp_qyry(dt_qyry);
                                    }
                                }
                                #endregion
                            }
                            catch (Exception ex)
                            {
                                row_SaveDataLog["SaveState"] = 0;
                                row_SaveDataLog["Msg"] = "出现异常，ryID：" + item["IDCardNo"].ToString2() + ",Exception:" + ex.Message;
                            }

                            dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                        }
                        #endregion

                        //Public.WriteLog("获取" + tag + "B类安全员：");
                        #region B类安全人员
                        rygwlx = "021B";
                        ryzyzglxid = "5";
                        ryzyzglx = "项目B类人员";

                        bytes = newdataService.getPersonJobCert(userID, "320200", xzqdm, rygwlx, "0");

                        result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);

                        index = result.IndexOf("<ReturnInfo>");
                        if (index >= 0)
                        {
                            string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
                            if (string.IsNullOrEmpty(returnResult))
                            {
                                continue;
                            }

                            ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
                            if (!returnInfo.Status)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            continue;
                        }
                        str = result.Substring(result.IndexOf("</ReturnInfo>") + 13, result.IndexOf("</ResultSet>") - result.IndexOf("</ReturnInfo>") - 13);
                        if (string.IsNullOrEmpty(str))
                        {
                            continue;
                        }

                        index = result.IndexOf("<PersonJobCertInfo>");
                        if (index < 0)
                            continue;

                        personJobCertInfoString = result.Substring(index, result.LastIndexOf("</PersonJobCertInfo>") - index + 20);
                        msg = "";
                        dt_personJobCertInfo = helper.ConvertXMLToDataTable("<dataTable>" + personJobCertInfoString + "</dataTable>", out msg);
                        if (!string.IsNullOrEmpty(msg) || dt_personJobCertInfo == null || dt_personJobCertInfo.Rows.Count == 0)
                            continue;

                        foreach (DataRow item in dt_personJobCertInfo.Rows)
                        {
                            DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                            row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_ryxx["ID"];
                            row_SaveDataLog["DataXml"] = "";
                            row_SaveDataLog["PKID"] = item["IDCardNo"].ToString2();
                            row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                            int rowIndex = -1;
                            try
                            {
                                string updateDate = item["UpdateDate"].ToString2();

                                #region 人员基本信息

                                DataTable dt_ryxx = dataService.Get_UEPP_Ryjbxx(item["IDCardNo"].ToString2());
                                if (dt_ryxx.Rows.Count == 0)
                                {
                                    row = dt_ryxx.NewRow();
                                    dt_ryxx.Rows.Add(row);
                                    row["ryID"] = item["IDCardNo"].ToString2();
                                }
                                else
                                {
                                    row = dt_ryxx.Rows[0];
                                    //if (!string.IsNullOrEmpty(row["xgrqsj"].ToString2()) && !string.IsNullOrEmpty(updateDate))
                                    //    if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(updateDate).ToString("yyyy-MM-dd"))
                                    //    {
                                    //        continue;
                                    //    }
                                }

                                if (string.IsNullOrEmpty(updateDate))
                                    updateDate = DateTime.Now.ToString("yyyy-MM-dd");
                                row["tag"] = tag;
                                row["xm"] = item["PersonName"];
                                switch (item["IDCardType"].ToString2())
                                {
                                    case "1":
                                        row["zjlxID"] = "1";
                                        row["zjlx"] = "身份证";
                                        break;
                                    case "2":
                                        row["zjlxID"] = "3";
                                        row["zjlx"] = "护照";
                                        break;
                                    case "3":
                                        row["zjlxID"] = "2";
                                        row["zjlx"] = "军官证";
                                        break;
                                    case "4":
                                        row["zjlxID"] = "4";
                                        row["zjlx"] = "台湾居民身份证";

                                        break;
                                    case "5":
                                        row["zjlxID"] = "5";
                                        row["zjlx"] = "香港永久性居民身份证";

                                        break;
                                    case "6":
                                        row["zjlxID"] = "6";
                                        row["zjlx"] = "警官证";

                                        break;
                                    case "其他":
                                        row["zjlxID"] = "9";
                                        row["zjlx"] = "其他";

                                        break;
                                }

                                row["zjhm"] = item["IDCardNo"];
                                //row["xb"] = item["Sex"];
                                //if (!string.IsNullOrEmpty(item["Birthday"].ToString2()))
                                //    row["csrq"] = item["Birthday"];
                                row["xgrqsj"] = updateDate;
                                row["xgr"] = "定时服务";
                                row["DataState"] = 0;
                                row["AJ_EXISTINIDCARDS"] = "2";
                                row["AJ_IsRefuse"] = "0";
                                allCount_ryxx++;
                                if (!dataService.Submit_uepp_ryjbxx(dt_ryxx))
                                {
                                    row_SaveDataLog["SaveState"] = 0;
                                    row_SaveDataLog["Msg"] = "建设单位人员信息保存失败，ryID：" + item["IDCardNo"].ToString2();
                                }
                                else
                                {
                                    success_count_ryxx++;
                                    row_SaveDataLog["SaveState"] = 1;
                                    row_SaveDataLog["Msg"] = "";
                                }
                                #endregion

                                #region 人员执业资格

                                DataTable dt_ryzyzg = dataService.Get_uepp_Ryzyzg(item["IDCardNo"].ToString2());
                                rowIndex = -1;
                                for (int i = 0; i < dt_ryzyzg.Rows.Count; i++)
                                {
                                    if (dt_ryzyzg.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                    {
                                        rowIndex = i;
                                        break;
                                    }
                                }
                                if (rowIndex < 0)
                                {
                                    row = dt_ryzyzg.NewRow();
                                    dt_ryzyzg.Rows.Add(row);

                                    row["ryID"] = item["IDCardNo"].ToString2();
                                    row["ryzyzglxID"] = ryzyzglxid;
                                    row["ryzyzglx"] = ryzyzglx;
                                    row["balxID"] = 1;
                                    row["balx"] = "长期备案";
                                    row["DataState"] = 0;
                                    row["tag"] = tag;
                                    row["xgr"] = "定时服务";
                                    row["xgrqsj"] = updateDate;
                                    dataService.Submit_uepp_Ryzyzg(dt_ryzyzg);
                                }

                                #endregion

                                #region 人员专业明细

                                //if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                //{
                                //    DataTable dt_ryzymx = dataService.Get_uepp_Ryzymx(item["IDCardNo"].ToString2());
                                //    rowIndex = -1;
                                //    for (int i = 0; i < dt_ryzymx.Rows.Count; i++)
                                //    {
                                //        if (dt_ryzymx.Rows[i]["ryzyzglxID"].ToString2() == ryzyzglxid && dt_ryzymx.Rows[i]["zyzglb"].ToString2() == item["CertMajor"].ToString2().Trim())
                                //        {
                                //            rowIndex = i;
                                //            break;
                                //        }
                                //    }
                                //    if (rowIndex < 0)
                                //    {
                                //        row = dt_ryzymx.NewRow();
                                //        dt_ryzymx.Rows.Add(row);
                                //        row["ryID"] = item["IDCardNo"].ToString2();
                                //        row["ryzyzglxID"] = ryzyzglxid;
                                //        row["ryzyzglx"] = ryzyzglx;

                                //        if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                //        {
                                //            row["zyzglb"] = item["CertMajor"].ToString2().Trim();

                                //            SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                //            sp.Add("@CodeInfo", item["CertMajor"].ToString2().Trim());
                                //            sp.Add("@parentCode", ryzyzglxid);

                                //            DataTable dt_code = dataService.GetTable("select * from UEPP_Code where CodeType='人员资质类别' and ParentCodeType='人员执业资格类型' and ParentCode=@parentCode and CodeInfo=@CodeInfo", sp);
                                //            if (dt_code.Rows.Count == 0)
                                //            {
                                //                dt_code.Rows.Add(dt_code.NewRow());
                                //                dt_code.Rows[0]["ParentCodeType"] = "人员执业资格类型";
                                //                dt_code.Rows[0]["ParentCode"] = ryzyzglxid;
                                //                dt_code.Rows[0]["CodeType"] = "人员资质类别";
                                //                dt_code.Rows[0]["CodeInfo"] = item["CertMajor"].ToString2().Trim();
                                //                dt_code.Rows[0]["Code"] = dataService.Get_uepp_Code_NewCode("人员资质类别");
                                //                dt_code.Rows[0]["CodeDesc"] = "";
                                //                dt_code.Rows[0]["OrderID"] = dt_code.Rows[0]["Code"];
                                //                dataService.Submit_uepp_code(dt_code);

                                //            }
                                //            row["zyzglbID"] = dt_code.Rows[0]["Code"];
                                //        }


                                //        row["zzbz"] = "主项";
                                //        row["DataState"] = 0;
                                //        row["tag"] = tag;
                                //        row["xgr"] = "定时服务";
                                //        row["xgrqsj"] = updateDate;
                                //        dataService.Submit_uepp_Ryzymx(dt_ryzymx);
                                //    }
                                //}

                                #endregion

                                #region 人员证书基本信息
                                DataTable dt_ryzs = dataService.Get_uepp_Ryzs(item["IDCardNo"].ToString2());
                                rowIndex = -1;
                                for (int i = 0; i < dt_ryzs.Rows.Count; i++)
                                {
                                    if (dt_ryzs.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                    {
                                        rowIndex = i;
                                        break;
                                    }
                                }
                                if (rowIndex < 0)
                                {

                                    row = dt_ryzs.NewRow();
                                    dt_ryzs.Rows.Add(row);
                                    row["zsjlId"] = dataService.Get_uepp_RyzsNewID();
                                    row["ryID"] = item["IDCardNo"].ToString2();
                                    row["ryzyzglxID"] = ryzyzglxid;
                                    row["ryzyzglx"] = ryzyzglx;
                                    row["ryzslxID"] = 51;
                                    row["ryzslx"] = "B类安全生产考核证";


                                }
                                else
                                {
                                    row = dt_ryzs.Rows[rowIndex];
                                }
                                row["sfzzz"] = 1;
                                row["zsbh"] = item["CertNo"].ToString2();
                                if (!string.IsNullOrEmpty(item["ValidDate"].ToString2()))
                                    row["zsyxzrq"] = item["ValidDate"].ToString2();
                                row["fzdw"] = item["IssueOrgan"].ToString2();
                                if (!string.IsNullOrEmpty(item["IssueDate"].ToString2()))
                                {
                                    row["fzrq"] = item["IssueDate"].ToString2();
                                    row["zsyxqrq"] = item["IssueDate"].ToString2();
                                }
                                row["Status"] = item["Status"];
                                if (dt_personJobCertInfo.Columns.Contains("QualIssueDate"))
                                    row["QualIssueDate"] = item["QualIssueDate"];
                                if (dt_personJobCertInfo.Columns.Contains("StampNo"))
                                    row["StampNo"] = item["StampNo"];
                                if (dt_personJobCertInfo.Columns.Contains("RegNo"))
                                    row["RegNo"] = item["RegNo"];

                                row["DataState"] = 0;
                                row["tag"] = tag;
                                row["xgr"] = "定时服务";
                                row["xgrqsj"] = updateDate;
                                dataService.Submit_uepp_Ryzs(dt_ryzs);

                                #endregion

                                #region 企业人员关系表

                                DataTable dt_qyry = dataService.Get_uepp_Qyry(item["IDCardNo"].ToString2(), Public.ZzjgdmToStandard(item["CorpCode"].ToString2()), ryzyzglxid);
                                if (dt_qyry.Rows.Count == 0)
                                {
                                    if (item["Status"].ToString2() != "2")
                                    {
                                        row = dt_qyry.NewRow();
                                        dt_qyry.Rows.Add(row);
                                        row["ryID"] = item["IDCardNo"].ToString2();
                                        row["qyID"] = item["CorpCode"].ToString2();
                                        row["ryzyzglxID"] = ryzyzglxid;
                                        row["ryzyzglx"] = ryzyzglx;
                                        row["DataState"] = 0;
                                        row["tag"] = tag;
                                        row["xgr"] = "定时服务";
                                        row["xgrqsj"] = updateDate;
                                        dataService.Submit_uepp_qyry(dt_qyry);
                                    }
                                }
                                else
                                {
                                    if (item["Status"].ToString2() == "2")
                                    {
                                        dt_qyry.Rows[0].Delete();
                                        dataService.Submit_uepp_qyry(dt_qyry);
                                    }
                                }
                                #endregion
                            }
                            catch (Exception ex)
                            {
                                row_SaveDataLog["SaveState"] = 0;
                                row_SaveDataLog["Msg"] = "出现异常，ryID：" + item["IDCardNo"].ToString2() + ",Exception:" + ex.Message;
                            }
                            dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                        }
                        #endregion

                        //Public.WriteLog("获取" + tag + "C类安全员：");
                        #region C类安全人员
                        rygwlx = "021C";
                        ryzyzglxid = "6";
                        ryzyzglx = "安全员(C类人员)";

                        bytes = newdataService.getPersonJobCert(userID, "320200", xzqdm, rygwlx, "0");

                        result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);

                        index = result.IndexOf("<ReturnInfo>");
                        if (index >= 0)
                        {
                            string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
                            if (string.IsNullOrEmpty(returnResult))
                            {
                                continue;
                            }

                            ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
                            if (!returnInfo.Status)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            continue;
                        }
                        str = result.Substring(result.IndexOf("</ReturnInfo>") + 13, result.IndexOf("</ResultSet>") - result.IndexOf("</ReturnInfo>") - 13);
                        if (string.IsNullOrEmpty(str))
                        {
                            continue;
                        }

                        index = result.IndexOf("<PersonJobCertInfo>");
                        if (index < 0)
                            continue;

                        personJobCertInfoString = result.Substring(index, result.LastIndexOf("</PersonJobCertInfo>") - index + 20);
                        msg = "";
                        dt_personJobCertInfo = helper.ConvertXMLToDataTable("<dataTable>" + personJobCertInfoString + "</dataTable>", out msg);
                        if (!string.IsNullOrEmpty(msg) || dt_personJobCertInfo == null || dt_personJobCertInfo.Rows.Count == 0)
                            continue;

                        foreach (DataRow item in dt_personJobCertInfo.Rows)
                        {
                            DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                            row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_ryxx["ID"];
                            row_SaveDataLog["DataXml"] = "";
                            row_SaveDataLog["PKID"] = item["IDCardNo"].ToString2();
                            row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                            int rowIndex = -1;
                            try
                            {
                                string updateDate = item["UpdateDate"].ToString2();

                                #region 人员基本信息

                                DataTable dt_ryxx = dataService.Get_UEPP_Ryjbxx(item["IDCardNo"].ToString2());
                                if (dt_ryxx.Rows.Count == 0)
                                {
                                    row = dt_ryxx.NewRow();
                                    dt_ryxx.Rows.Add(row);
                                    row["ryID"] = item["IDCardNo"].ToString2();
                                }
                                else
                                {
                                    row = dt_ryxx.Rows[0];
                                    //if (!string.IsNullOrEmpty(row["xgrqsj"].ToString2()) && !string.IsNullOrEmpty(updateDate))
                                    //    if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(updateDate).ToString("yyyy-MM-dd"))
                                    //    {
                                    //        continue;
                                    //    }
                                }

                                if (string.IsNullOrEmpty(updateDate))
                                    updateDate = DateTime.Now.ToString("yyyy-MM-dd");
                                row["tag"] = tag;
                                row["xm"] = item["PersonName"];
                                switch (item["IDCardType"].ToString2())
                                {
                                    case "1":
                                        row["zjlxID"] = "1";
                                        row["zjlx"] = "身份证";
                                        break;
                                    case "2":
                                        row["zjlxID"] = "3";
                                        row["zjlx"] = "护照";
                                        break;
                                    case "3":
                                        row["zjlxID"] = "2";
                                        row["zjlx"] = "军官证";
                                        break;
                                    case "4":
                                        row["zjlxID"] = "4";
                                        row["zjlx"] = "台湾居民身份证";

                                        break;
                                    case "5":
                                        row["zjlxID"] = "5";
                                        row["zjlx"] = "香港永久性居民身份证";

                                        break;
                                    case "6":
                                        row["zjlxID"] = "6";
                                        row["zjlx"] = "警官证";

                                        break;
                                    case "其他":
                                        row["zjlxID"] = "9";
                                        row["zjlx"] = "其他";

                                        break;
                                }

                                row["zjhm"] = item["IDCardNo"];
                                //row["xb"] = item["Sex"];
                                //if (!string.IsNullOrEmpty(item["Birthday"].ToString2()))
                                //    row["csrq"] = item["Birthday"];
                                row["xgrqsj"] = updateDate;
                                row["xgr"] = "定时服务";
                                row["DataState"] = 0;
                                row["AJ_EXISTINIDCARDS"] = "2";
                                row["AJ_IsRefuse"] = "0";
                                allCount_ryxx++;
                                if (!dataService.Submit_uepp_ryjbxx(dt_ryxx))
                                {
                                    row_SaveDataLog["SaveState"] = 0;
                                    row_SaveDataLog["Msg"] = "建设单位人员信息保存失败，ryID：" + item["IDCardNo"].ToString2();
                                }
                                else
                                {
                                    success_count_ryxx++;
                                    row_SaveDataLog["SaveState"] = 1;
                                    row_SaveDataLog["Msg"] = "";
                                }
                                #endregion

                                #region 人员执业资格

                                DataTable dt_ryzyzg = dataService.Get_uepp_Ryzyzg(item["IDCardNo"].ToString2());
                                rowIndex = -1;
                                for (int i = 0; i < dt_ryzyzg.Rows.Count; i++)
                                {
                                    if (dt_ryzyzg.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                    {
                                        rowIndex = i;
                                        break;
                                    }
                                }
                                if (rowIndex < 0)
                                {
                                    row = dt_ryzyzg.NewRow();
                                    dt_ryzyzg.Rows.Add(row);

                                    row["ryID"] = item["IDCardNo"].ToString2();
                                    row["ryzyzglxID"] = ryzyzglxid;
                                    row["ryzyzglx"] = ryzyzglx;
                                    row["balxID"] = 1;
                                    row["balx"] = "长期备案";
                                    row["DataState"] = 0;
                                    row["tag"] = tag;
                                    row["xgr"] = "定时服务";
                                    row["xgrqsj"] = updateDate;
                                    dataService.Submit_uepp_Ryzyzg(dt_ryzyzg);
                                }

                                #endregion

                                #region 人员专业明细

                                //if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                //{
                                //    DataTable dt_ryzymx = dataService.Get_uepp_Ryzymx(item["IDCardNo"].ToString2());
                                //    rowIndex = -1;
                                //    for (int i = 0; i < dt_ryzymx.Rows.Count; i++)
                                //    {
                                //        if (dt_ryzymx.Rows[i]["ryzyzglxID"].ToString2() == ryzyzglxid && dt_ryzymx.Rows[i]["zyzglb"].ToString2() == item["CertMajor"].ToString2().Trim())
                                //        {
                                //            rowIndex = i;
                                //            break;
                                //        }
                                //    }
                                //    if (rowIndex < 0)
                                //    {
                                //        row = dt_ryzymx.NewRow();
                                //        dt_ryzymx.Rows.Add(row);
                                //        row["ryID"] = item["IDCardNo"].ToString2();
                                //        row["ryzyzglxID"] = ryzyzglxid;
                                //        row["ryzyzglx"] = ryzyzglx;

                                //        if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                //        {
                                //            row["zyzglb"] = item["CertMajor"].ToString2().Trim();

                                //            SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                //            sp.Add("@CodeInfo", item["CertMajor"].ToString2().Trim());
                                //            sp.Add("@parentCode", ryzyzglxid);

                                //            DataTable dt_code = dataService.GetTable("select * from UEPP_Code where CodeType='人员资质类别' and ParentCodeType='人员执业资格类型' and ParentCode=@parentCode and CodeInfo=@CodeInfo", sp);
                                //            if (dt_code.Rows.Count == 0)
                                //            {
                                //                dt_code.Rows.Add(dt_code.NewRow());
                                //                dt_code.Rows[0]["ParentCodeType"] = "人员执业资格类型";
                                //                dt_code.Rows[0]["ParentCode"] = ryzyzglxid;
                                //                dt_code.Rows[0]["CodeType"] = "人员资质类别";
                                //                dt_code.Rows[0]["CodeInfo"] = item["CertMajor"].ToString2().Trim();
                                //                dt_code.Rows[0]["Code"] = dataService.Get_uepp_Code_NewCode("人员资质类别");
                                //                dt_code.Rows[0]["CodeDesc"] = "";
                                //                dt_code.Rows[0]["OrderID"] = dt_code.Rows[0]["Code"];
                                //                dataService.Submit_uepp_code(dt_code);

                                //            }
                                //            row["zyzglbID"] = dt_code.Rows[0]["Code"];
                                //        }


                                //        row["zzbz"] = "主项";
                                //        row["DataState"] = 0;
                                //        row["tag"] = tag;
                                //        row["xgr"] = "定时服务";
                                //        row["xgrqsj"] = updateDate;
                                //        dataService.Submit_uepp_Ryzymx(dt_ryzymx);
                                //    }
                                //}

                                #endregion

                                #region 人员证书基本信息
                                DataTable dt_ryzs = dataService.Get_uepp_Ryzs(item["IDCardNo"].ToString2());
                                rowIndex = -1;
                                for (int i = 0; i < dt_ryzs.Rows.Count; i++)
                                {
                                    if (dt_ryzs.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                    {
                                        rowIndex = i;
                                        break;
                                    }
                                }
                                if (rowIndex < 0)
                                {
                                    row = dt_ryzs.NewRow();
                                    dt_ryzs.Rows.Add(row);
                                    row["zsjlId"] = dataService.Get_uepp_RyzsNewID();
                                    row["ryID"] = item["IDCardNo"].ToString2();
                                    row["ryzyzglxID"] = ryzyzglxid;
                                    row["ryzyzglx"] = ryzyzglx;
                                    row["ryzslxID"] = 51;
                                    row["ryzslx"] = "B类安全生产考核证";


                                }
                                else
                                {
                                    row = dt_ryzs.Rows[rowIndex];
                                }
                                row["sfzzz"] = 1;
                                row["zsbh"] = item["CertNo"].ToString2();
                                if (!string.IsNullOrEmpty(item["ValidDate"].ToString2()))
                                    row["zsyxzrq"] = item["ValidDate"].ToString2();
                                row["fzdw"] = item["IssueOrgan"].ToString2();
                                if (!string.IsNullOrEmpty(item["IssueDate"].ToString2()))
                                {
                                    row["fzrq"] = item["IssueDate"].ToString2();
                                    row["zsyxqrq"] = item["IssueDate"].ToString2();
                                }

                                row["Status"] = item["Status"];
                                if (dt_personJobCertInfo.Columns.Contains("QualIssueDate"))
                                    row["QualIssueDate"] = item["QualIssueDate"];
                                if (dt_personJobCertInfo.Columns.Contains("StampNo"))
                                    row["StampNo"] = item["StampNo"];
                                if (dt_personJobCertInfo.Columns.Contains("RegNo"))
                                    row["RegNo"] = item["RegNo"];

                                row["DataState"] = 0;
                                row["tag"] = tag;
                                row["xgr"] = "定时服务";
                                row["xgrqsj"] = updateDate;
                                dataService.Submit_uepp_Ryzs(dt_ryzs);

                                #endregion

                                #region 企业人员关系表

                                DataTable dt_qyry = dataService.Get_uepp_Qyry(item["IDCardNo"].ToString2(), Public.ZzjgdmToStandard(item["CorpCode"].ToString2()), ryzyzglxid);
                                if (dt_qyry.Rows.Count == 0)
                                {
                                    if (item["Status"].ToString2() != "2")
                                    {
                                        row = dt_qyry.NewRow();
                                        dt_qyry.Rows.Add(row);
                                        row["ryID"] = item["IDCardNo"].ToString2();
                                        row["qyID"] = item["CorpCode"].ToString2();
                                        row["ryzyzglxID"] = ryzyzglxid;
                                        row["ryzyzglx"] = ryzyzglx;
                                        row["DataState"] = 0;
                                        row["tag"] = tag;
                                        row["xgr"] = "定时服务";
                                        row["xgrqsj"] = updateDate;
                                        dataService.Submit_uepp_qyry(dt_qyry);
                                    }
                                }
                                else
                                {
                                    if (item["Status"].ToString2() == "2")
                                    {
                                        dt_qyry.Rows[0].Delete();
                                        dataService.Submit_uepp_qyry(dt_qyry);
                                    }
                                }
                                #endregion
                            }
                            catch (Exception ex)
                            {
                                row_SaveDataLog["SaveState"] = 0;
                                row_SaveDataLog["Msg"] = "出现异常，ryID：" + item["IDCardNo"].ToString2() + ",Exception:" + ex.Message;
                            }
                            dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                        }
                        #endregion
                    }
                    if (dt_SaveDataLog.Rows.Count > 0)
                        dataService.Submit_SaveDataLog(dt_SaveDataLog);
                    row_DataJkDataDetail_ryxx["allCount"] = allCount_ryxx;
                    row_DataJkDataDetail_ryxx["successCount"] = success_count_ryxx;
                    row_DataJkDataDetail_ryxx["IsOk"] = 1;
                    row_DataJkDataDetail_ryxx["ErrorMsg"] = "";

                    if (dt_DataJkDataDetail_ryxx.Rows.Count > 0)
                        dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_ryxx);


                    DateTime endTime = DateTime.Now;
                    TimeSpan span = compareDateTime(beginTime, endTime);
                    Public.WriteLog(string.Format("结束YourTask_PullDataFromSxxzx_Ryxx_Aqscgl任务:{0}，历时：{1}时{2}分{3}秒", endTime.ToString("yyyy-MM-dd HH:mm:ss"), span.Hours, span.Minutes, span.Seconds));


                }
                catch (Exception ex)
                {

                    row_DataJkDataDetail_ryxx["allCount"] = allCount_ryxx;
                    row_DataJkDataDetail_ryxx["successCount"] = success_count_ryxx;
                    row_DataJkDataDetail_ryxx["IsOk"] = 0;
                    row_DataJkDataDetail_ryxx["ErrorMsg"] = ex.Message;

                    if (dt_DataJkDataDetail_ryxx.Rows.Count > 0)
                        dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_ryxx);
                }
            }
            catch (Exception ex)
            {
                Public.WriteLog("Exception:" + ex.Message);
            }

        }

        /// <summary>
        /// 从江苏建设公共基础数据平台拉取人员（专业岗位管理人员）信息
        /// </summary>
        void YourTask_PullDataFromSxxzx_Ryxx_Zygwgl(string DataJkLogID)
        {
            try
            {
                DateTime beginTime = DateTime.Now;
                Public.WriteLog("开始执行YourTask_PullDataFromSxxzx_Ryxx_Zygwgl：" + beginTime.ToString("yyyy-MM-dd HH:mm:ss"));
                string tag = Tag.江苏建设公共基础数据平台.ToString();
                string userID = "wxszjj01";

                DataService dataService = new DataService();
                DataTable dt_xzqhdm = dataService.Get_tbXzqdmDic();
                XmlHelper helper = new XmlHelper();

                //往数据监控日志表项添加一条记录
                DataTable dt_DataJkDataDetail_ryxx = dataService.GetSchema_DataJkDataDetail();
                long Id_DataJkDataDetail_ryxx = dataService.Get_DataJkDataDetailNewID().ToInt64();

                int allCount_ryxx = 0, success_count_ryxx = 0;
                DataRow row_DataJkDataDetail_ryxx = dt_DataJkDataDetail_ryxx.NewRow();
                dt_DataJkDataDetail_ryxx.Rows.Add(row_DataJkDataDetail_ryxx);

                row_DataJkDataDetail_ryxx["ID"] = Id_DataJkDataDetail_ryxx++;
                row_DataJkDataDetail_ryxx["DataJkLogID"] = DataJkLogID;
                row_DataJkDataDetail_ryxx["tableName"] = "UEPP_Ryjbxx";
                row_DataJkDataDetail_ryxx["MethodName"] = "getPersonJobCert(zygw)";
                row_DataJkDataDetail_ryxx["bz"] = "从江苏建设公共基础数据平台拉取人员（专业岗位管理人员）信息";

                try
                {
                    DataTable dt_SaveDataLog = dataService.GetSchema_SaveDataLog();

                    NewDataService.NewDataService newdataService = new WxjzgcjczyTimerService.NewDataService.NewDataService();
                    DataRow row;

                    foreach (DataRow row_xzqdm in dt_xzqhdm.Rows)
                    {
                        string xzqdm = row_xzqdm["Code"].ToString2();

                        //Public.WriteLog("获取" + tag + "施工员：");
                        #region 施工员
                        string rygwlx = "0201";
                        string ryzyzglxid = "7";
                        string ryzyzglx = "施工员";
                        string ryzslxid = "82";
                        string ryzslx = "施工员上岗证";

                        byte[] bytes = newdataService.getPersonJobCert(userID, "320200", xzqdm, rygwlx, "0");

                        string result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);

                        int index = result.IndexOf("<ReturnInfo>");
                        if (index >= 0)
                        {
                            string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
                            if (!string.IsNullOrEmpty(returnResult))
                            {
                                ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
                                if (returnInfo.Status)
                                {
                                    index = result.IndexOf("<PersonJobCertInfo>");
                                    if (index >= 0)
                                    {
                                        string personJobCertInfoString = result.Substring(index, result.LastIndexOf("</PersonJobCertInfo>") - index + 20);
                                        string msg = "";
                                        DataTable dt_personJobCertInfo = helper.ConvertXMLToDataTable("<dataTable>" + personJobCertInfoString + "</dataTable>", out msg);
                                        if (string.IsNullOrEmpty(msg) && dt_personJobCertInfo != null && dt_personJobCertInfo.Rows.Count > 0)
                                        {
                                            foreach (DataRow item in dt_personJobCertInfo.Rows)
                                            {
                                                DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                                                row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_ryxx["ID"];
                                                row_SaveDataLog["DataXml"] = "";
                                                row_SaveDataLog["PKID"] = item["IDCardNo"].ToString2();
                                                row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                                                int rowIndex = -1;
                                                try
                                                {
                                                    string updateDate = item["UpdateDate"].ToString2();

                                                    #region 人员基本信息

                                                    DataTable dt_ryxx = dataService.Get_UEPP_Ryjbxx(item["IDCardNo"].ToString2());
                                                    if (dt_ryxx.Rows.Count == 0)
                                                    {
                                                        row = dt_ryxx.NewRow();
                                                        dt_ryxx.Rows.Add(row);
                                                        row["ryID"] = item["IDCardNo"].ToString2();
                                                    }
                                                    else
                                                    {
                                                        row = dt_ryxx.Rows[0];
                                                        //if (!string.IsNullOrEmpty(row["xgrqsj"].ToString2()) && !string.IsNullOrEmpty(updateDate))
                                                        //    if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(updateDate).ToString("yyyy-MM-dd"))
                                                        //    {
                                                        //        continue;
                                                        //    }
                                                    }

                                                    if (string.IsNullOrEmpty(updateDate))
                                                        updateDate = DateTime.Now.ToString("yyyy-MM-dd");
                                                    row["tag"] = tag;
                                                    row["xm"] = item["PersonName"];
                                                    switch (item["IDCardType"].ToString2())
                                                    {
                                                        case "1":
                                                            row["zjlxID"] = "1";
                                                            row["zjlx"] = "身份证";
                                                            break;
                                                        case "2":
                                                            row["zjlxID"] = "3";
                                                            row["zjlx"] = "护照";
                                                            break;
                                                        case "3":
                                                            row["zjlxID"] = "2";
                                                            row["zjlx"] = "军官证";
                                                            break;
                                                        case "4":
                                                            row["zjlxID"] = "4";
                                                            row["zjlx"] = "台湾居民身份证";

                                                            break;
                                                        case "5":
                                                            row["zjlxID"] = "5";
                                                            row["zjlx"] = "香港永久性居民身份证";

                                                            break;
                                                        case "6":
                                                            row["zjlxID"] = "6";
                                                            row["zjlx"] = "警官证";

                                                            break;
                                                        case "其他":
                                                            row["zjlxID"] = "9";
                                                            row["zjlx"] = "其他";

                                                            break;
                                                    }

                                                    row["zjhm"] = item["IDCardNo"];
                                                    //row["xb"] = item["Sex"];
                                                    //if (!string.IsNullOrEmpty(item["Birthday"].ToString2()))
                                                    //    row["csrq"] = item["Birthday"];
                                                    row["xgrqsj"] = updateDate;
                                                    row["xgr"] = "定时服务";
                                                    row["DataState"] = 0;
                                                    row["AJ_EXISTINIDCARDS"] = "2";
                                                    row["AJ_IsRefuse"] = "0";
                                                    allCount_ryxx++;
                                                    if (!dataService.Submit_uepp_ryjbxx(dt_ryxx))
                                                    {
                                                        row_SaveDataLog["SaveState"] = 0;
                                                        row_SaveDataLog["Msg"] = "建设单位人员信息保存失败，ryID：" + item["IDCardNo"].ToString2();
                                                    }
                                                    else
                                                    {
                                                        success_count_ryxx++;
                                                        row_SaveDataLog["SaveState"] = 1;
                                                        row_SaveDataLog["Msg"] = "";
                                                    }
                                                    #endregion

                                                    #region 人员执业资格

                                                    DataTable dt_ryzyzg = dataService.Get_uepp_Ryzyzg(item["IDCardNo"].ToString2());
                                                    rowIndex = -1;
                                                    for (int i = 0; i < dt_ryzyzg.Rows.Count; i++)
                                                    {
                                                        if (dt_ryzyzg.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                                        {
                                                            rowIndex = i;
                                                            break;
                                                        }
                                                    }
                                                    if (rowIndex < 0)
                                                    {
                                                        row = dt_ryzyzg.NewRow();
                                                        dt_ryzyzg.Rows.Add(row);

                                                        row["ryID"] = item["IDCardNo"].ToString2();
                                                        row["ryzyzglxID"] = ryzyzglxid;
                                                        row["ryzyzglx"] = ryzyzglx;
                                                        row["balxID"] = 1;
                                                        row["balx"] = "长期备案";
                                                        row["DataState"] = 0;
                                                        row["tag"] = tag;
                                                        row["xgr"] = "定时服务";
                                                        row["xgrqsj"] = updateDate;
                                                        dataService.Submit_uepp_Ryzyzg(dt_ryzyzg);
                                                    }

                                                    #endregion

                                                    #region 人员专业明细

                                                    //if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                                    //{
                                                    //    DataTable dt_ryzymx = dataService.Get_uepp_Ryzymx_Sgy(item["IDCardNo"].ToString2());
                                                    //    rowIndex = -1;
                                                    //    for (int i = 0; i < dt_ryzymx.Rows.Count; i++)
                                                    //    {
                                                    //        if (dt_ryzymx.Rows[i]["ryzyzglxID"].ToString2() == ryzyzglxid && dt_ryzymx.Rows[i]["zyzglb"].ToString2() == item["CertMajor"].ToString2().Trim())
                                                    //        {
                                                    //            rowIndex = i;
                                                    //            break;
                                                    //        }
                                                    //    }
                                                    //    if (rowIndex < 0)
                                                    //    {
                                                    //        row = dt_ryzymx.NewRow();
                                                    //        dt_ryzymx.Rows.Add(row);
                                                    //        row["ryID"] = item["IDCardNo"].ToString2();
                                                    //        row["ryzyzglxID"] = ryzyzglxid;
                                                    //        row["ryzyzglx"] = ryzyzglx;

                                                    //        if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                                    //        {
                                                    //            row["zyzglb"] = item["CertMajor"].ToString2().Trim();

                                                    //            SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                                    //            sp.Add("@CodeInfo", item["CertMajor"].ToString2().Trim());
                                                    //            sp.Add("@parentCode", ryzyzglxid);

                                                    //            DataTable dt_code = dataService.GetTable("select * from UEPP_Code where CodeType='人员资质类别' and ParentCodeType='人员执业资格类型' and ParentCode=@parentCode and CodeInfo=@CodeInfo", sp);
                                                    //            if (dt_code.Rows.Count == 0)
                                                    //            {
                                                    //                dt_code.Rows.Add(dt_code.NewRow());
                                                    //                dt_code.Rows[0]["ParentCodeType"] = "人员执业资格类型";
                                                    //                dt_code.Rows[0]["ParentCode"] = ryzyzglxid;
                                                    //                dt_code.Rows[0]["CodeType"] = "人员资质类别";
                                                    //                dt_code.Rows[0]["CodeInfo"] = item["CertMajor"].ToString2().Trim();
                                                    //                dt_code.Rows[0]["Code"] = dataService.Get_uepp_Code_NewCode("人员资质类别");
                                                    //                dt_code.Rows[0]["CodeDesc"] = "";
                                                    //                dt_code.Rows[0]["OrderID"] = dt_code.Rows[0]["Code"];
                                                    //                dataService.Submit_uepp_code(dt_code);

                                                    //            }
                                                    //            row["zyzglbID"] = dt_code.Rows[0]["Code"];
                                                    //        }


                                                    //        row["zzbz"] = "主项";
                                                    //        row["DataState"] = 0;
                                                    //        row["tag"] = tag;
                                                    //        row["xgr"] = "定时服务";
                                                    //        row["xgrqsj"] = updateDate;
                                                    //        dataService.Submit_uepp_Ryzymx(dt_ryzymx);
                                                    //    }
                                                    //}

                                                    #endregion

                                                    #region 人员证书基本信息
                                                    DataTable dt_ryzs = dataService.Get_uepp_Ryzs(item["IDCardNo"].ToString2());
                                                    rowIndex = -1;
                                                    for (int i = 0; i < dt_ryzs.Rows.Count; i++)
                                                    {
                                                        if (dt_ryzs.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                                        {
                                                            rowIndex = i;
                                                            break;
                                                        }
                                                    }
                                                    if (rowIndex < 0)
                                                    {

                                                        row = dt_ryzs.NewRow();
                                                        dt_ryzs.Rows.Add(row);
                                                        row["zsjlId"] = dataService.Get_uepp_RyzsNewID();
                                                        row["ryID"] = item["IDCardNo"].ToString2();
                                                        row["ryzyzglxID"] = ryzyzglxid;
                                                        row["ryzyzglx"] = ryzyzglx;
                                                        row["ryzslxID"] = ryzslxid;
                                                        row["ryzslx"] = ryzslx;


                                                    }
                                                    else
                                                    {
                                                        row = dt_ryzs.Rows[rowIndex];
                                                    }
                                                    row["sfzzz"] = 1;
                                                    row["zsbh"] = item["CertNo"].ToString2();
                                                    if (!string.IsNullOrEmpty(item["ValidDate"].ToString2()))
                                                        row["zsyxzrq"] = item["ValidDate"].ToString2();
                                                    row["fzdw"] = item["IssueOrgan"].ToString2();
                                                    if (!string.IsNullOrEmpty(item["IssueDate"].ToString2()))
                                                    {
                                                        row["fzrq"] = item["IssueDate"].ToString2();
                                                        row["zsyxqrq"] = item["IssueDate"].ToString2();
                                                    }
                                                    row["Status"] = item["Status"];
                                                    //if (dt_ryzs.Columns.Contains("QualIssueDate"))
                                                    //    row["QualIssueDate"] = item["QualIssueDate"];
                                                    //if (dt_ryzs.Columns.Contains("StampNo"))
                                                    //    row["StampNo"] = item["StampNo"];
                                                    //if (dt_ryzs.Columns.Contains("RegNo"))
                                                    //    row["RegNo"] = item["RegNo"];

                                                    row["DataState"] = 0;
                                                    row["tag"] = tag;
                                                    row["xgr"] = "定时服务";
                                                    row["xgrqsj"] = updateDate;
                                                    dataService.Submit_uepp_Ryzs(dt_ryzs);

                                                    #endregion

                                                    #region 企业人员关系表

                                                    DataTable dt_qyry = dataService.Get_uepp_Qyry(item["IDCardNo"].ToString2(), Public.ZzjgdmToStandard(item["CorpCode"].ToString2()), ryzyzglxid);
                                                    if (dt_qyry.Rows.Count == 0)
                                                    {
                                                        if (item["Status"].ToString2() != "2")
                                                        {
                                                            row = dt_qyry.NewRow();
                                                            dt_qyry.Rows.Add(row);
                                                            row["ryID"] = item["IDCardNo"].ToString2();
                                                            row["qyID"] = item["CorpCode"].ToString2();
                                                            row["ryzyzglxID"] = ryzyzglxid;
                                                            row["ryzyzglx"] = ryzyzglx;
                                                            row["DataState"] = 0;
                                                            row["tag"] = tag;
                                                            row["xgr"] = "定时服务";
                                                            row["xgrqsj"] = updateDate;
                                                            dataService.Submit_uepp_qyry(dt_qyry);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (item["Status"].ToString2() == "2")
                                                        {
                                                            dt_qyry.Rows[0].Delete();
                                                            dataService.Submit_uepp_qyry(dt_qyry);
                                                        }
                                                    }
                                                    #endregion
                                                }
                                                catch (Exception ex)
                                                {
                                                    row_SaveDataLog["SaveState"] = 0;
                                                    row_SaveDataLog["Msg"] = "出现异常，ryID：" + item["IDCardNo"].ToString2() + ",Exception:" + ex.Message;
                                                }
                                                dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                                            }

                                        }
                                    }
                                }
                            }
                        }

                        #endregion
                        //Public.WriteLog("获取" + tag + "质量员：");
                        #region 质量员
                        rygwlx = "0202";
                        ryzyzglxid = "8";
                        ryzyzglx = "质量员";
                        ryzslxid = "61";
                        ryzslx = "质量员上岗证";

                        bytes = newdataService.getPersonJobCert(userID, "320200", xzqdm, rygwlx, "0");

                        result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);

                        index = result.IndexOf("<ReturnInfo>");
                        if (index >= 0)
                        {
                            string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
                            if (!string.IsNullOrEmpty(returnResult))
                            {
                                ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
                                if (returnInfo.Status)
                                {
                                    index = result.IndexOf("<PersonJobCertInfo>");
                                    if (index > 0)
                                    {
                                        string personJobCertInfoString = result.Substring(index, result.LastIndexOf("</PersonJobCertInfo>") - index + 20);
                                        string msg = "";
                                        DataTable dt_personJobCertInfo = helper.ConvertXMLToDataTable("<dataTable>" + personJobCertInfoString + "</dataTable>", out msg);
                                        if (string.IsNullOrEmpty(msg) && dt_personJobCertInfo != null && dt_personJobCertInfo.Rows.Count > 0)
                                        {
                                            foreach (DataRow item in dt_personJobCertInfo.Rows)
                                            {
                                                DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                                                row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_ryxx["ID"];
                                                row_SaveDataLog["DataXml"] = "";
                                                row_SaveDataLog["PKID"] = item["IDCardNo"].ToString2();
                                                row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                                                int rowIndex = -1;
                                                try
                                                {
                                                    string updateDate = item["UpdateDate"].ToString2();

                                                    #region 人员基本信息

                                                    DataTable dt_ryxx = dataService.Get_UEPP_Ryjbxx(item["IDCardNo"].ToString2());
                                                    if (dt_ryxx.Rows.Count == 0)
                                                    {
                                                        row = dt_ryxx.NewRow();
                                                        dt_ryxx.Rows.Add(row);
                                                        row["ryID"] = item["IDCardNo"].ToString2();
                                                    }
                                                    else
                                                    {
                                                        row = dt_ryxx.Rows[0];
                                                        if (!string.IsNullOrEmpty(row["xgrqsj"].ToString2()) && !string.IsNullOrEmpty(updateDate))
                                                            if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(updateDate).ToString("yyyy-MM-dd"))
                                                            {
                                                                continue;
                                                            }
                                                    }

                                                    if (string.IsNullOrEmpty(updateDate))
                                                        updateDate = DateTime.Now.ToString("yyyy-MM-dd");
                                                    row["tag"] = tag;
                                                    row["xm"] = item["PersonName"];
                                                    switch (item["IDCardType"].ToString2())
                                                    {
                                                        case "1":
                                                            row["zjlxID"] = "1";
                                                            row["zjlx"] = "身份证";
                                                            break;
                                                        case "2":
                                                            row["zjlxID"] = "3";
                                                            row["zjlx"] = "护照";
                                                            break;
                                                        case "3":
                                                            row["zjlxID"] = "2";
                                                            row["zjlx"] = "军官证";
                                                            break;
                                                        case "4":
                                                            row["zjlxID"] = "4";
                                                            row["zjlx"] = "台湾居民身份证";

                                                            break;
                                                        case "5":
                                                            row["zjlxID"] = "5";
                                                            row["zjlx"] = "香港永久性居民身份证";

                                                            break;
                                                        case "6":
                                                            row["zjlxID"] = "6";
                                                            row["zjlx"] = "警官证";

                                                            break;
                                                        case "其他":
                                                            row["zjlxID"] = "9";
                                                            row["zjlx"] = "其他";

                                                            break;
                                                    }

                                                    row["zjhm"] = item["IDCardNo"];
                                                    //row["xb"] = item["Sex"];
                                                    //if (!string.IsNullOrEmpty(item["Birthday"].ToString2()))
                                                    //    row["csrq"] = item["Birthday"];
                                                    row["xgrqsj"] = updateDate;
                                                    row["xgr"] = "定时服务";
                                                    row["DataState"] = 0;
                                                    row["AJ_EXISTINIDCARDS"] = "2";
                                                    row["AJ_IsRefuse"] = "0";
                                                    allCount_ryxx++;
                                                    if (!dataService.Submit_uepp_ryjbxx(dt_ryxx))
                                                    {
                                                        //Public.WriteLog("建设单位人员信息保存失败，ryID：" + item["IDCardNo"].ToString2());
                                                        row_SaveDataLog["SaveState"] = 0;
                                                        row_SaveDataLog["Msg"] = "建设单位人员信息保存失败，ryID：" + item["IDCardNo"].ToString2();
                                                    }
                                                    else
                                                    {
                                                        success_count_ryxx++;
                                                        row_SaveDataLog["SaveState"] = 1;
                                                        row_SaveDataLog["Msg"] = "";
                                                    }
                                                    #endregion

                                                    #region 人员执业资格

                                                    DataTable dt_ryzyzg = dataService.Get_uepp_Ryzyzg(item["IDCardNo"].ToString2());
                                                    rowIndex = -1;
                                                    for (int i = 0; i < dt_ryzyzg.Rows.Count; i++)
                                                    {
                                                        if (dt_ryzyzg.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                                        {
                                                            rowIndex = i;
                                                            break;
                                                        }
                                                    }
                                                    if (rowIndex < 0)
                                                    {
                                                        row = dt_ryzyzg.NewRow();
                                                        dt_ryzyzg.Rows.Add(row);

                                                        row["ryID"] = item["IDCardNo"].ToString2();
                                                        row["ryzyzglxID"] = ryzyzglxid;
                                                        row["ryzyzglx"] = ryzyzglx;
                                                        row["balxID"] = 1;
                                                        row["balx"] = "长期备案";
                                                        row["DataState"] = 0;
                                                        row["tag"] = tag;
                                                        row["xgr"] = "定时服务";
                                                        row["xgrqsj"] = updateDate;
                                                        dataService.Submit_uepp_Ryzyzg(dt_ryzyzg);
                                                    }

                                                    #endregion

                                                    #region 人员专业明细

                                                    //if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                                    //{
                                                    //    DataTable dt_ryzymx = dataService.Get_uepp_Ryzym_Zly(item["IDCardNo"].ToString2());
                                                    //    rowIndex = -1;
                                                    //    for (int i = 0; i < dt_ryzymx.Rows.Count; i++)
                                                    //    {
                                                    //        if (dt_ryzymx.Rows[i]["ryzyzglxID"].ToString2() == ryzyzglxid && dt_ryzymx.Rows[i]["zyzglb"].ToString2() == item["CertMajor"].ToString2().Trim())
                                                    //        {
                                                    //            rowIndex = i;
                                                    //            break;
                                                    //        }
                                                    //    }
                                                    //    if (rowIndex < 0)
                                                    //    {
                                                    //        row = dt_ryzymx.NewRow();
                                                    //        dt_ryzymx.Rows.Add(row);
                                                    //        row["ryID"] = item["IDCardNo"].ToString2();
                                                    //        row["ryzyzglxID"] = ryzyzglxid;
                                                    //        row["ryzyzglx"] = ryzyzglx;

                                                    //        if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                                    //        {
                                                    //            row["zyzglb"] = item["CertMajor"].ToString2().Trim();

                                                    //            SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                                    //            sp.Add("@CodeInfo", item["CertMajor"].ToString2().Trim());
                                                    //            sp.Add("@parentCode", ryzyzglxid);

                                                    //            DataTable dt_code = dataService.GetTable("select * from UEPP_Code where CodeType='人员资质类别' and ParentCodeType='人员执业资格类型' and ParentCode=@parentCode and CodeInfo=@CodeInfo", sp);
                                                    //            if (dt_code.Rows.Count == 0)
                                                    //            {
                                                    //                dt_code.Rows.Add(dt_code.NewRow());
                                                    //                dt_code.Rows[0]["ParentCodeType"] = "人员执业资格类型";
                                                    //                dt_code.Rows[0]["ParentCode"] = ryzyzglxid;
                                                    //                dt_code.Rows[0]["CodeType"] = "人员资质类别";
                                                    //                dt_code.Rows[0]["CodeInfo"] = item["CertMajor"].ToString2().Trim();
                                                    //                dt_code.Rows[0]["Code"] = dataService.Get_uepp_Code_NewCode("人员资质类别");
                                                    //                dt_code.Rows[0]["CodeDesc"] = "";
                                                    //                dt_code.Rows[0]["OrderID"] = dt_code.Rows[0]["Code"];
                                                    //                dataService.Submit_uepp_code(dt_code);

                                                    //            }
                                                    //            row["zyzglbID"] = dt_code.Rows[0]["Code"];
                                                    //        }


                                                    //        row["zzbz"] = "主项";
                                                    //        row["DataState"] = 0;
                                                    //        row["tag"] = tag;
                                                    //        row["xgr"] = "定时服务";
                                                    //        row["xgrqsj"] = updateDate;
                                                    //        dataService.Submit_uepp_Ryzymx(dt_ryzymx);
                                                    //    }
                                                    //}

                                                    #endregion

                                                    #region 人员证书基本信息
                                                    DataTable dt_ryzs = dataService.Get_uepp_Ryzs(item["IDCardNo"].ToString2());
                                                    rowIndex = -1;
                                                    for (int i = 0; i < dt_ryzs.Rows.Count; i++)
                                                    {
                                                        if (dt_ryzs.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                                        {
                                                            rowIndex = i;
                                                            break;
                                                        }
                                                    }
                                                    if (rowIndex < 0)
                                                    {

                                                        row = dt_ryzs.NewRow();
                                                        dt_ryzs.Rows.Add(row);
                                                        row["zsjlId"] = dataService.Get_uepp_RyzsNewID();
                                                        row["ryID"] = item["IDCardNo"].ToString2();
                                                        row["ryzyzglxID"] = ryzyzglxid;
                                                        row["ryzyzglx"] = ryzyzglx;
                                                        row["ryzslxID"] = ryzslxid;
                                                        row["ryzslx"] = ryzslx;


                                                    }
                                                    else
                                                    {
                                                        row = dt_ryzs.Rows[rowIndex];
                                                    }

                                                    row["sfzzz"] = 1;
                                                    row["zsbh"] = item["CertNo"].ToString2();
                                                    if (!string.IsNullOrEmpty(item["ValidDate"].ToString2()))
                                                        row["zsyxzrq"] = item["ValidDate"].ToString2();
                                                    row["fzdw"] = item["IssueOrgan"].ToString2();
                                                    if (!string.IsNullOrEmpty(item["IssueDate"].ToString2()))
                                                    {
                                                        row["fzrq"] = item["IssueDate"].ToString2();
                                                        row["zsyxqrq"] = item["IssueDate"].ToString2();
                                                    }
                                                    row["Status"] = item["Status"];

                                                    //if (dt_ryzs.Columns.Contains("QualIssueDate"))
                                                    //    row["QualIssueDate"] = item["QualIssueDate"];
                                                    //if (dt_ryzs.Columns.Contains("StampNo"))
                                                    //    row["StampNo"] = item["StampNo"];
                                                    //if (dt_ryzs.Columns.Contains("RegNo"))
                                                    //    row["RegNo"] = item["RegNo"];

                                                    row["DataState"] = 0;
                                                    row["tag"] = tag;
                                                    row["xgr"] = "定时服务";
                                                    row["xgrqsj"] = updateDate;
                                                    dataService.Submit_uepp_Ryzs(dt_ryzs);

                                                    #endregion

                                                    #region 企业人员关系表

                                                    DataTable dt_qyry = dataService.Get_uepp_Qyry(item["IDCardNo"].ToString2(), Public.ZzjgdmToStandard(item["CorpCode"].ToString2()), ryzyzglxid);
                                                    if (dt_qyry.Rows.Count == 0)
                                                    {
                                                        if (item["Status"].ToString2() != "2")
                                                        {
                                                            row = dt_qyry.NewRow();
                                                            dt_qyry.Rows.Add(row);
                                                            row["ryID"] = item["IDCardNo"].ToString2();
                                                            row["qyID"] = item["CorpCode"].ToString2();
                                                            row["ryzyzglxID"] = ryzyzglxid;
                                                            row["ryzyzglx"] = ryzyzglx;
                                                            row["DataState"] = 0;
                                                            row["tag"] = tag;
                                                            row["xgr"] = "定时服务";
                                                            row["xgrqsj"] = updateDate;
                                                            dataService.Submit_uepp_qyry(dt_qyry);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (item["Status"].ToString2() == "2")
                                                        {
                                                            dt_qyry.Rows[0].Delete();
                                                            dataService.Submit_uepp_qyry(dt_qyry);
                                                        }
                                                    }
                                                    #endregion
                                                }
                                                catch (Exception ex)
                                                {
                                                    row_SaveDataLog["SaveState"] = 0;
                                                    row_SaveDataLog["Msg"] = "出现异常，ryID：" + item["IDCardNo"].ToString2() + ",Exception:" + ex.Message;
                                                }
                                                dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        #endregion
                        //Public.WriteLog("获取" + tag + "机械员：");
                        #region 机械员
                        rygwlx = "0204";
                        ryzyzglxid = "11";
                        ryzyzglx = "机械员";
                        ryzslxid = "82";
                        ryzslx = "机械员上岗证";

                        bytes = newdataService.getPersonJobCert(userID, "320200", xzqdm, rygwlx, "0");

                        result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);

                        index = result.IndexOf("<ReturnInfo>");
                        if (index >= 0)
                        {
                            string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
                            if (!string.IsNullOrEmpty(returnResult))
                            {
                                ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
                                if (returnInfo.Status)
                                {
                                    string str = result.Substring(result.IndexOf("</ReturnInfo>") + 13, result.IndexOf("</ResultSet>") - result.IndexOf("</ReturnInfo>") - 13);
                                    if (!string.IsNullOrEmpty(str))
                                    {
                                        index = result.IndexOf("<PersonJobCertInfo>");
                                        if (index >= 0)
                                        {
                                            string personJobCertInfoString = result.Substring(index, result.LastIndexOf("</PersonJobCertInfo>") - index + 20);
                                            string msg = "";
                                            DataTable dt_personJobCertInfo = helper.ConvertXMLToDataTable("<dataTable>" + personJobCertInfoString + "</dataTable>", out msg);
                                            if (string.IsNullOrEmpty(msg) && dt_personJobCertInfo != null && dt_personJobCertInfo.Rows.Count > 0)
                                            {
                                                foreach (DataRow item in dt_personJobCertInfo.Rows)
                                                {
                                                    DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                                                    row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_ryxx["ID"];
                                                    row_SaveDataLog["DataXml"] = "";
                                                    row_SaveDataLog["PKID"] = item["IDCardNo"].ToString2();
                                                    row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                                                    int rowIndex = -1;
                                                    try
                                                    {
                                                        string updateDate = item["UpdateDate"].ToString2();

                                                        #region 人员基本信息

                                                        DataTable dt_ryxx = dataService.Get_UEPP_Ryjbxx(item["IDCardNo"].ToString2());
                                                        if (dt_ryxx.Rows.Count == 0)
                                                        {
                                                            row = dt_ryxx.NewRow();
                                                            dt_ryxx.Rows.Add(row);
                                                            row["ryID"] = item["IDCardNo"].ToString2();
                                                        }
                                                        else
                                                        {
                                                            row = dt_ryxx.Rows[0];
                                                            if (!string.IsNullOrEmpty(row["xgrqsj"].ToString2()) && !string.IsNullOrEmpty(updateDate))
                                                                if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(updateDate).ToString("yyyy-MM-dd"))
                                                                {
                                                                    continue;
                                                                }
                                                        }

                                                        if (string.IsNullOrEmpty(updateDate))
                                                            updateDate = DateTime.Now.ToString("yyyy-MM-dd");
                                                        row["tag"] = tag;
                                                        row["xm"] = item["PersonName"];
                                                        switch (item["IDCardType"].ToString2())
                                                        {
                                                            case "1":
                                                                row["zjlxID"] = "1";
                                                                row["zjlx"] = "身份证";
                                                                break;
                                                            case "2":
                                                                row["zjlxID"] = "3";
                                                                row["zjlx"] = "护照";
                                                                break;
                                                            case "3":
                                                                row["zjlxID"] = "2";
                                                                row["zjlx"] = "军官证";
                                                                break;
                                                            case "4":
                                                                row["zjlxID"] = "4";
                                                                row["zjlx"] = "台湾居民身份证";

                                                                break;
                                                            case "5":
                                                                row["zjlxID"] = "5";
                                                                row["zjlx"] = "香港永久性居民身份证";

                                                                break;
                                                            case "6":
                                                                row["zjlxID"] = "6";
                                                                row["zjlx"] = "警官证";

                                                                break;
                                                            case "其他":
                                                                row["zjlxID"] = "9";
                                                                row["zjlx"] = "其他";

                                                                break;
                                                        }

                                                        row["zjhm"] = item["IDCardNo"];
                                                        //row["xb"] = item["Sex"];
                                                        //if (!string.IsNullOrEmpty(item["Birthday"].ToString2()))
                                                        //    row["csrq"] = item["Birthday"];
                                                        row["xgrqsj"] = updateDate;
                                                        row["xgr"] = "定时服务";

                                                        row["DataState"] = 0;

                                                        row["AJ_EXISTINIDCARDS"] = "2";
                                                        row["AJ_IsRefuse"] = "0";
                                                        allCount_ryxx++;
                                                        if (!dataService.Submit_uepp_ryjbxx(dt_ryxx))
                                                        {
                                                            row_SaveDataLog["SaveState"] = 0;
                                                            row_SaveDataLog["Msg"] = "建设单位人员信息保存失败，ryID：" + item["IDCardNo"].ToString2();
                                                        }
                                                        else
                                                        {
                                                            success_count_ryxx++;
                                                            row_SaveDataLog["SaveState"] = 1;
                                                            row_SaveDataLog["Msg"] = "";
                                                        }
                                                        #endregion

                                                        #region 人员执业资格

                                                        DataTable dt_ryzyzg = dataService.Get_uepp_Ryzyzg(item["IDCardNo"].ToString2());
                                                        rowIndex = -1;
                                                        for (int i = 0; i < dt_ryzyzg.Rows.Count; i++)
                                                        {
                                                            if (dt_ryzyzg.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                                            {
                                                                rowIndex = i;
                                                                break;
                                                            }
                                                        }
                                                        if (rowIndex < 0)
                                                        {
                                                            row = dt_ryzyzg.NewRow();
                                                            dt_ryzyzg.Rows.Add(row);

                                                            row["ryID"] = item["IDCardNo"].ToString2();
                                                            row["ryzyzglxID"] = ryzyzglxid;
                                                            row["ryzyzglx"] = ryzyzglx;
                                                            row["balxID"] = 1;
                                                            row["balx"] = "长期备案";
                                                            row["DataState"] = 0;
                                                            row["tag"] = tag;
                                                            row["xgr"] = "定时服务";
                                                            row["xgrqsj"] = updateDate;
                                                            dataService.Submit_uepp_Ryzyzg(dt_ryzyzg);
                                                        }

                                                        #endregion

                                                        #region 人员专业明细

                                                        //if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                                        //{
                                                        //    DataTable dt_ryzymx = dataService.Get_uepp_Ryzymx(item["IDCardNo"].ToString2());
                                                        //    rowIndex = -1;
                                                        //    for (int i = 0; i < dt_ryzymx.Rows.Count; i++)
                                                        //    {
                                                        //        if (dt_ryzymx.Rows[i]["ryzyzglxID"].ToString2() == ryzyzglxid && dt_ryzymx.Rows[i]["zyzglb"].ToString2() == item["CertMajor"].ToString2().Trim())
                                                        //        {
                                                        //            rowIndex = i;
                                                        //            break;
                                                        //        }
                                                        //    }
                                                        //    if (rowIndex < 0)
                                                        //    {
                                                        //        row = dt_ryzymx.NewRow();
                                                        //        dt_ryzymx.Rows.Add(row);
                                                        //        row["ryID"] = item["IDCardNo"].ToString2();
                                                        //        row["ryzyzglxID"] = ryzyzglxid;
                                                        //        row["ryzyzglx"] = ryzyzglx;

                                                        //        if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                                        //        {
                                                        //            row["zyzglb"] = item["CertMajor"].ToString2().Trim();

                                                        //            SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                                        //            sp.Add("@CodeInfo", item["CertMajor"].ToString2().Trim());
                                                        //            sp.Add("@parentCode", ryzyzglxid);

                                                        //            DataTable dt_code = dataService.GetTable("select * from UEPP_Code where CodeType='人员资质类别' and ParentCodeType='人员执业资格类型' and ParentCode=@parentCode and CodeInfo=@CodeInfo", sp);
                                                        //            if (dt_code.Rows.Count == 0)
                                                        //            {
                                                        //                dt_code.Rows.Add(dt_code.NewRow());
                                                        //                dt_code.Rows[0]["ParentCodeType"] = "人员执业资格类型";
                                                        //                dt_code.Rows[0]["ParentCode"] = ryzyzglxid;
                                                        //                dt_code.Rows[0]["CodeType"] = "人员资质类别";
                                                        //                dt_code.Rows[0]["CodeInfo"] = item["CertMajor"].ToString2().Trim();
                                                        //                dt_code.Rows[0]["Code"] = dataService.Get_uepp_Code_NewCode("人员资质类别");
                                                        //                dt_code.Rows[0]["CodeDesc"] = "";
                                                        //                dt_code.Rows[0]["OrderID"] = dt_code.Rows[0]["Code"];
                                                        //                dataService.Submit_uepp_code(dt_code);

                                                        //            }
                                                        //            row["zyzglbID"] = dt_code.Rows[0]["Code"];
                                                        //        }


                                                        //        row["zzbz"] = "主项";
                                                        //        row["DataState"] = 0;
                                                        //        row["tag"] = tag;
                                                        //        row["xgr"] = "定时服务";
                                                        //        row["xgrqsj"] = updateDate;
                                                        //        dataService.Submit_uepp_Ryzymx(dt_ryzymx);
                                                        //    }
                                                        //}


                                                        #endregion

                                                        #region 人员证书基本信息
                                                        DataTable dt_ryzs = dataService.Get_uepp_Ryzs(item["IDCardNo"].ToString2());
                                                        rowIndex = -1;
                                                        for (int i = 0; i < dt_ryzs.Rows.Count; i++)
                                                        {
                                                            if (dt_ryzs.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                                            {
                                                                rowIndex = i;
                                                                break;
                                                            }
                                                        }
                                                        if (rowIndex < 0)
                                                        {

                                                            row = dt_ryzs.NewRow();
                                                            dt_ryzs.Rows.Add(row);
                                                            row["zsjlId"] = dataService.Get_uepp_RyzsNewID();
                                                            row["ryID"] = item["IDCardNo"].ToString2();
                                                            row["ryzyzglxID"] = ryzyzglxid;
                                                            row["ryzyzglx"] = ryzyzglx;
                                                            row["ryzslxID"] = ryzslxid;
                                                            row["ryzslx"] = ryzslx;
                                                        }
                                                        else
                                                        {
                                                            row = dt_ryzs.Rows[rowIndex];
                                                        }
                                                        row["sfzzz"] = 1;
                                                        row["zsbh"] = item["CertNo"].ToString2();
                                                        if (!string.IsNullOrEmpty(item["ValidDate"].ToString2()))
                                                            row["zsyxzrq"] = item["ValidDate"].ToString2();
                                                        row["fzdw"] = item["IssueOrgan"].ToString2();
                                                        if (!string.IsNullOrEmpty(item["IssueDate"].ToString2()))
                                                        {
                                                            row["fzrq"] = item["IssueDate"].ToString2();
                                                            row["zsyxqrq"] = item["IssueDate"].ToString2();
                                                        }
                                                        row["Status"] = item["Status"];
                                                        //if (dt_ryzs.Columns.Contains("QualIssueDate"))
                                                        //    row["QualIssueDate"] = item["QualIssueDate"];
                                                        //if (dt_ryzs.Columns.Contains("StampNo"))
                                                        //    row["StampNo"] = item["StampNo"];
                                                        //if (dt_ryzs.Columns.Contains("RegNo"))
                                                        //    row["RegNo"] = item["RegNo"];


                                                        row["DataState"] = 0;
                                                        row["tag"] = tag;
                                                        row["xgr"] = "定时服务";
                                                        row["xgrqsj"] = updateDate;
                                                        dataService.Submit_uepp_Ryzs(dt_ryzs);


                                                        #endregion

                                                        #region 企业人员关系表

                                                        DataTable dt_qyry = dataService.Get_uepp_Qyry(item["IDCardNo"].ToString2(), Public.ZzjgdmToStandard(item["CorpCode"].ToString2()), ryzyzglxid);
                                                        if (dt_qyry.Rows.Count == 0)
                                                        {
                                                            if (item["Status"].ToString2() != "2")
                                                            {
                                                                row = dt_qyry.NewRow();
                                                                dt_qyry.Rows.Add(row);
                                                                row["ryID"] = item["IDCardNo"].ToString2();
                                                                row["qyID"] = item["CorpCode"].ToString2();
                                                                row["ryzyzglxID"] = ryzyzglxid;
                                                                row["ryzyzglx"] = ryzyzglx;
                                                                row["DataState"] = 0;
                                                                row["tag"] = tag;
                                                                row["xgr"] = "定时服务";
                                                                row["xgrqsj"] = updateDate;
                                                                dataService.Submit_uepp_qyry(dt_qyry);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (item["Status"].ToString2() == "2")
                                                            {
                                                                dt_qyry.Rows[0].Delete();
                                                                dataService.Submit_uepp_qyry(dt_qyry);
                                                            }
                                                        }
                                                        #endregion

                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        row_SaveDataLog["SaveState"] = 0;
                                                        row_SaveDataLog["Msg"] = "出现异常，ryID：" + item["IDCardNo"].ToString2() + ",Exception:" + ex.Message;
                                                    }
                                                    dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                        Public.WriteLog("获取" + tag + "资料员：");
                        #region 资料员
                        rygwlx = "0205";
                        ryzyzglxid = "12";
                        ryzyzglx = "材料员";
                        ryzslxid = "83";
                        ryzslx = "资料员上岗证";
                        bytes = newdataService.getPersonJobCert(userID, "320200", xzqdm, rygwlx, "0");

                        result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);

                        index = result.IndexOf("<ReturnInfo>");
                        if (index >= 0)
                        {
                            string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
                            if (!string.IsNullOrEmpty(returnResult))
                            {
                                ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
                                if (returnInfo.Status)
                                {
                                    string str = result.Substring(result.IndexOf("</ReturnInfo>") + 13, result.IndexOf("</ResultSet>") - result.IndexOf("</ReturnInfo>") - 13);
                                    if (!string.IsNullOrEmpty(str))
                                    {
                                        index = result.IndexOf("<PersonJobCertInfo>");
                                        if (index >= 0)
                                        {
                                            string personJobCertInfoString = result.Substring(index, result.LastIndexOf("</PersonJobCertInfo>") - index + 20);
                                            string msg = "";
                                            DataTable dt_personJobCertInfo = helper.ConvertXMLToDataTable("<dataTable>" + personJobCertInfoString + "</dataTable>", out msg);
                                            if (string.IsNullOrEmpty(msg) && dt_personJobCertInfo != null && dt_personJobCertInfo.Rows.Count > 0)
                                            {
                                                foreach (DataRow item in dt_personJobCertInfo.Rows)
                                                {
                                                    DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                                                    row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_ryxx["ID"];
                                                    row_SaveDataLog["DataXml"] = "";
                                                    row_SaveDataLog["PKID"] = item["IDCardNo"].ToString2();
                                                    row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                                                    int rowIndex = -1;
                                                    try
                                                    {
                                                        string updateDate = item["UpdateDate"].ToString2();

                                                        #region 人员基本信息

                                                        DataTable dt_ryxx = dataService.Get_UEPP_Ryjbxx(item["IDCardNo"].ToString2());
                                                        if (dt_ryxx.Rows.Count == 0)
                                                        {
                                                            row = dt_ryxx.NewRow();
                                                            dt_ryxx.Rows.Add(row);
                                                            row["ryID"] = item["IDCardNo"].ToString2();
                                                        }
                                                        else
                                                        {
                                                            row = dt_ryxx.Rows[0];
                                                            if (!string.IsNullOrEmpty(row["xgrqsj"].ToString2()) && !string.IsNullOrEmpty(updateDate))
                                                                if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(updateDate).ToString("yyyy-MM-dd"))
                                                                {
                                                                    continue;
                                                                }
                                                        }

                                                        if (string.IsNullOrEmpty(updateDate))
                                                            updateDate = DateTime.Now.ToString("yyyy-MM-dd");
                                                        row["tag"] = tag;
                                                        row["xm"] = item["PersonName"];
                                                        switch (item["IDCardType"].ToString2())
                                                        {
                                                            case "1":
                                                                row["zjlxID"] = "1";
                                                                row["zjlx"] = "身份证";
                                                                break;
                                                            case "2":
                                                                row["zjlxID"] = "3";
                                                                row["zjlx"] = "护照";
                                                                break;
                                                            case "3":
                                                                row["zjlxID"] = "2";
                                                                row["zjlx"] = "军官证";
                                                                break;
                                                            case "4":
                                                                row["zjlxID"] = "4";
                                                                row["zjlx"] = "台湾居民身份证";

                                                                break;
                                                            case "5":
                                                                row["zjlxID"] = "5";
                                                                row["zjlx"] = "香港永久性居民身份证";

                                                                break;
                                                            case "6":
                                                                row["zjlxID"] = "6";
                                                                row["zjlx"] = "警官证";

                                                                break;
                                                            case "其他":
                                                                row["zjlxID"] = "9";
                                                                row["zjlx"] = "其他";

                                                                break;
                                                        }

                                                        row["zjhm"] = item["IDCardNo"];
                                                        //row["xb"] = item["Sex"];
                                                        //if (!string.IsNullOrEmpty(item["Birthday"].ToString2()))
                                                        //    row["csrq"] = item["Birthday"];
                                                        row["xgrqsj"] = updateDate;
                                                        row["xgr"] = "定时服务";
                                                        row["DataState"] = 0;
                                                        row["AJ_EXISTINIDCARDS"] = "2";
                                                        row["AJ_IsRefuse"] = "0";
                                                        allCount_ryxx++;
                                                        if (!dataService.Submit_uepp_ryjbxx(dt_ryxx))
                                                        {
                                                            row_SaveDataLog["SaveState"] = 0;
                                                            row_SaveDataLog["Msg"] = "建设单位人员信息保存失败，ryID：" + item["IDCardNo"].ToString2();
                                                        }
                                                        else
                                                        {
                                                            success_count_ryxx++;
                                                            row_SaveDataLog["SaveState"] = 1;
                                                            row_SaveDataLog["Msg"] = "";
                                                        }
                                                        #endregion

                                                        #region 人员执业资格

                                                        DataTable dt_ryzyzg = dataService.Get_uepp_Ryzyzg(item["IDCardNo"].ToString2());
                                                        rowIndex = -1;
                                                        for (int i = 0; i < dt_ryzyzg.Rows.Count; i++)
                                                        {
                                                            if (dt_ryzyzg.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                                            {
                                                                rowIndex = i;
                                                                break;
                                                            }
                                                        }
                                                        if (rowIndex < 0)
                                                        {
                                                            row = dt_ryzyzg.NewRow();
                                                            dt_ryzyzg.Rows.Add(row);

                                                            row["ryID"] = item["IDCardNo"].ToString2();
                                                            row["ryzyzglxID"] = ryzyzglxid;
                                                            row["ryzyzglx"] = ryzyzglx;
                                                            row["balxID"] = 1;
                                                            row["balx"] = "长期备案";
                                                            row["DataState"] = 0;
                                                            row["tag"] = tag;
                                                            row["xgr"] = "定时服务";
                                                            row["xgrqsj"] = updateDate;
                                                            dataService.Submit_uepp_Ryzyzg(dt_ryzyzg);
                                                        }

                                                        #endregion

                                                        #region 人员专业明细

                                                        //if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                                        //{
                                                        //    DataTable dt_ryzymx = dataService.Get_uepp_Ryzymx(item["IDCardNo"].ToString2());
                                                        //    rowIndex = -1;
                                                        //    for (int i = 0; i < dt_ryzymx.Rows.Count; i++)
                                                        //    {
                                                        //        if (dt_ryzymx.Rows[i]["ryzyzglxID"].ToString2() == ryzyzglxid && dt_ryzymx.Rows[i]["zyzglb"].ToString2() == item["CertMajor"].ToString2().Trim())
                                                        //        {
                                                        //            rowIndex = i;
                                                        //            break;
                                                        //        }
                                                        //    }
                                                        //    if (rowIndex < 0)
                                                        //    {
                                                        //        row = dt_ryzymx.NewRow();
                                                        //        dt_ryzymx.Rows.Add(row);
                                                        //        row["ryID"] = item["IDCardNo"].ToString2();
                                                        //        row["ryzyzglxID"] = ryzyzglxid;
                                                        //        row["ryzyzglx"] = ryzyzglx;

                                                        //        if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                                        //        {
                                                        //            row["zyzglb"] = item["CertMajor"].ToString2().Trim();

                                                        //            SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                                        //            sp.Add("@CodeInfo", item["CertMajor"].ToString2().Trim());
                                                        //            sp.Add("@parentCode", ryzyzglxid);

                                                        //            DataTable dt_code = dataService.GetTable("select * from UEPP_Code where CodeType='人员资质类别' and ParentCodeType='人员执业资格类型' and ParentCode=@parentCode and CodeInfo=@CodeInfo", sp);
                                                        //            if (dt_code.Rows.Count == 0)
                                                        //            {
                                                        //                dt_code.Rows.Add(dt_code.NewRow());
                                                        //                dt_code.Rows[0]["ParentCodeType"] = "人员执业资格类型";
                                                        //                dt_code.Rows[0]["ParentCode"] = ryzyzglxid;
                                                        //                dt_code.Rows[0]["CodeType"] = "人员资质类别";
                                                        //                dt_code.Rows[0]["CodeInfo"] = item["CertMajor"].ToString2().Trim();
                                                        //                dt_code.Rows[0]["Code"] = dataService.Get_uepp_Code_NewCode("人员资质类别");
                                                        //                dt_code.Rows[0]["CodeDesc"] = "";
                                                        //                dt_code.Rows[0]["OrderID"] = dt_code.Rows[0]["Code"];
                                                        //                dataService.Submit_uepp_code(dt_code);

                                                        //            }
                                                        //            row["zyzglbID"] = dt_code.Rows[0]["Code"];
                                                        //        }


                                                        //        row["zzbz"] = "主项";
                                                        //        row["DataState"] = 0;
                                                        //        row["tag"] = tag;
                                                        //        row["xgr"] = "定时服务";
                                                        //        row["xgrqsj"] = updateDate;
                                                        //        dataService.Submit_uepp_Ryzymx(dt_ryzymx);
                                                        //    }
                                                        //}

                                                        #endregion

                                                        #region 人员证书基本信息
                                                        DataTable dt_ryzs = dataService.Get_uepp_Ryzs(item["IDCardNo"].ToString2());
                                                        rowIndex = -1;
                                                        for (int i = 0; i < dt_ryzs.Rows.Count; i++)
                                                        {
                                                            if (dt_ryzs.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                                            {
                                                                rowIndex = i;
                                                                break;
                                                            }
                                                        }
                                                        if (rowIndex < 0)
                                                        {
                                                            row = dt_ryzs.NewRow();
                                                            dt_ryzs.Rows.Add(row);
                                                            row["zsjlId"] = dataService.Get_uepp_RyzsNewID();
                                                            row["ryID"] = item["IDCardNo"].ToString2();
                                                            row["ryzyzglxID"] = ryzyzglxid;
                                                            row["ryzyzglx"] = ryzyzglx;
                                                            row["ryzslxID"] = ryzslxid;
                                                            row["ryzslx"] = ryzslx;
                                                        }
                                                        else
                                                        {
                                                            row = dt_ryzs.Rows[rowIndex];
                                                        }
                                                        row["sfzzz"] = 1;
                                                        row["zsbh"] = item["CertNo"].ToString2();
                                                        if (!string.IsNullOrEmpty(item["ValidDate"].ToString2()))
                                                            row["zsyxzrq"] = item["ValidDate"].ToString2();
                                                        row["fzdw"] = item["IssueOrgan"].ToString2();
                                                        if (!string.IsNullOrEmpty(item["IssueDate"].ToString2()))
                                                        {
                                                            row["fzrq"] = item["IssueDate"].ToString2();
                                                            row["zsyxqrq"] = item["IssueDate"].ToString2();
                                                        }
                                                        row["Status"] = item["Status"];
                                                        //if (dt_personJobCertInfo.Columns.Contains("QualIssueDate"))
                                                        //    row["QualIssueDate"] = item["QualIssueDate"];
                                                        //if (dt_personJobCertInfo.Columns.Contains("StampNo"))
                                                        //    row["StampNo"] = item["StampNo"];
                                                        //if (dt_personJobCertInfo.Columns.Contains("RegNo"))
                                                        //    row["RegNo"] = item["RegNo"];

                                                        row["DataState"] = 0;
                                                        row["tag"] = tag;
                                                        row["xgr"] = "定时服务";
                                                        row["xgrqsj"] = updateDate;
                                                        dataService.Submit_uepp_Ryzs(dt_ryzs);

                                                        #endregion

                                                        #region 企业人员关系表

                                                        DataTable dt_qyry = dataService.Get_uepp_Qyry(item["IDCardNo"].ToString2(), Public.ZzjgdmToStandard(item["CorpCode"].ToString2()), ryzyzglxid);

                                                        if (dt_qyry.Rows.Count == 0)
                                                        {
                                                            if (item["Status"].ToString2() != "2")
                                                            {
                                                                row = dt_qyry.NewRow();
                                                                dt_qyry.Rows.Add(row);
                                                                row["ryID"] = item["IDCardNo"].ToString2();
                                                                row["qyID"] = item["CorpCode"].ToString2();
                                                                row["ryzyzglxID"] = ryzyzglxid;
                                                                row["ryzyzglx"] = ryzyzglx;
                                                                row["DataState"] = 0;
                                                                row["tag"] = tag;
                                                                row["xgr"] = "定时服务";
                                                                row["xgrqsj"] = updateDate;
                                                                dataService.Submit_uepp_qyry(dt_qyry);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (item["Status"].ToString2() == "2")
                                                            {
                                                                dt_qyry.Rows[0].Delete();
                                                                dataService.Submit_uepp_qyry(dt_qyry);
                                                            }
                                                        }
                                                        #endregion
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        row_SaveDataLog["SaveState"] = 0;
                                                        row_SaveDataLog["Msg"] = "出现异常，ryID：" + item["IDCardNo"].ToString2() + ",Exception:" + ex.Message;
                                                    }
                                                    dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        #endregion
                        //Public.WriteLog("获取" + tag + "材料员：");
                        #region 材料员
                        rygwlx = "0206";
                        ryzyzglxid = "9";
                        ryzyzglx = "材料员";
                        ryzslxid = "71";
                        ryzslx = "材料员上岗证";

                        bytes = newdataService.getPersonJobCert(userID, "320200", xzqdm, rygwlx, "0");

                        result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);

                        index = result.IndexOf("<ReturnInfo>");
                        if (index >= 0)
                        {
                            string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
                            if (!string.IsNullOrEmpty(returnResult))
                            {
                                ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
                                if (returnInfo.Status)
                                {
                                    index = result.IndexOf("<PersonJobCertInfo>");
                                    if (index >= 0)
                                    {
                                        string personJobCertInfoString = result.Substring(index, result.LastIndexOf("</PersonJobCertInfo>") - index + 20);
                                        string msg = "";
                                        DataTable dt_personJobCertInfo = helper.ConvertXMLToDataTable("<dataTable>" + personJobCertInfoString + "</dataTable>", out msg);
                                        if (string.IsNullOrEmpty(msg) && dt_personJobCertInfo != null && dt_personJobCertInfo.Rows.Count > 0)
                                        {
                                            foreach (DataRow item in dt_personJobCertInfo.Rows)
                                            {
                                                DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                                                row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_ryxx["ID"];
                                                row_SaveDataLog["DataXml"] = "";
                                                row_SaveDataLog["PKID"] = item["IDCardNo"].ToString2();
                                                row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                                                int rowIndex = -1;
                                                try
                                                {
                                                    string updateDate = item["UpdateDate"].ToString2();

                                                    #region 人员基本信息

                                                    DataTable dt_ryxx = dataService.Get_UEPP_Ryjbxx(item["IDCardNo"].ToString2());
                                                    if (dt_ryxx.Rows.Count == 0)
                                                    {
                                                        row = dt_ryxx.NewRow();
                                                        dt_ryxx.Rows.Add(row);
                                                        row["ryID"] = item["IDCardNo"].ToString2();
                                                    }
                                                    else
                                                    {
                                                        row = dt_ryxx.Rows[0];
                                                        if (!string.IsNullOrEmpty(row["xgrqsj"].ToString2()) && !string.IsNullOrEmpty(updateDate))
                                                            if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(updateDate).ToString("yyyy-MM-dd"))
                                                            {
                                                                continue;
                                                            }
                                                    }

                                                    if (string.IsNullOrEmpty(updateDate))
                                                        updateDate = DateTime.Now.ToString("yyyy-MM-dd");
                                                    row["tag"] = tag;
                                                    row["xm"] = item["PersonName"];
                                                    switch (item["IDCardType"].ToString2())
                                                    {
                                                        case "1":
                                                            row["zjlxID"] = "1";
                                                            row["zjlx"] = "身份证";
                                                            break;
                                                        case "2":
                                                            row["zjlxID"] = "3";
                                                            row["zjlx"] = "护照";
                                                            break;
                                                        case "3":
                                                            row["zjlxID"] = "2";
                                                            row["zjlx"] = "军官证";
                                                            break;
                                                        case "4":
                                                            row["zjlxID"] = "4";
                                                            row["zjlx"] = "台湾居民身份证";

                                                            break;
                                                        case "5":
                                                            row["zjlxID"] = "5";
                                                            row["zjlx"] = "香港永久性居民身份证";

                                                            break;
                                                        case "6":
                                                            row["zjlxID"] = "6";
                                                            row["zjlx"] = "警官证";

                                                            break;
                                                        case "其他":
                                                            row["zjlxID"] = "9";
                                                            row["zjlx"] = "其他";

                                                            break;
                                                    }

                                                    row["zjhm"] = item["IDCardNo"];
                                                    //row["xb"] = item["Sex"];
                                                    //if (!string.IsNullOrEmpty(item["Birthday"].ToString2()))
                                                    //    row["csrq"] = item["Birthday"];
                                                    row["xgrqsj"] = updateDate;
                                                    row["xgr"] = "定时服务";
                                                    row["DataState"] = 0;
                                                    row["AJ_EXISTINIDCARDS"] = "2";
                                                    row["AJ_IsRefuse"] = "0";
                                                    allCount_ryxx++;
                                                    if (!dataService.Submit_uepp_ryjbxx(dt_ryxx))
                                                    {
                                                        //Public.WriteLog("建设单位人员信息保存失败，ryID：" + item["IDCardNo"].ToString2());
                                                        row_SaveDataLog["SaveState"] = 0;
                                                        row_SaveDataLog["Msg"] = "建设单位人员信息保存失败，ryID：" + item["IDCardNo"].ToString2();
                                                    }
                                                    else
                                                    {
                                                        success_count_ryxx++;
                                                        row_SaveDataLog["SaveState"] = 1;
                                                        row_SaveDataLog["Msg"] = "";
                                                    }
                                                    #endregion

                                                    #region 人员执业资格

                                                    DataTable dt_ryzyzg = dataService.Get_uepp_Ryzyzg(item["IDCardNo"].ToString2());
                                                    rowIndex = -1;
                                                    for (int i = 0; i < dt_ryzyzg.Rows.Count; i++)
                                                    {
                                                        if (dt_ryzyzg.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                                        {
                                                            rowIndex = i;
                                                            break;
                                                        }
                                                    }
                                                    if (rowIndex < 0)
                                                    {
                                                        row = dt_ryzyzg.NewRow();
                                                        dt_ryzyzg.Rows.Add(row);

                                                        row["ryID"] = item["IDCardNo"].ToString2();
                                                        row["ryzyzglxID"] = ryzyzglxid;
                                                        row["ryzyzglx"] = ryzyzglx;
                                                        row["balxID"] = 1;
                                                        row["balx"] = "长期备案";
                                                        row["DataState"] = 0;
                                                        row["tag"] = tag;
                                                        row["xgr"] = "定时服务";
                                                        row["xgrqsj"] = updateDate;
                                                        dataService.Submit_uepp_Ryzyzg(dt_ryzyzg);
                                                    }

                                                    #endregion

                                                    #region 人员专业明细

                                                    //if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                                    //{
                                                    //    DataTable dt_ryzymx = dataService.Get_uepp_Ryzymx(item["IDCardNo"].ToString2());
                                                    //    rowIndex = -1;
                                                    //    for (int i = 0; i < dt_ryzymx.Rows.Count; i++)
                                                    //    {
                                                    //        if (dt_ryzymx.Rows[i]["ryzyzglxID"].ToString2() == ryzyzglxid && dt_ryzymx.Rows[i]["zyzglb"].ToString2() == item["CertMajor"].ToString2().Trim())
                                                    //        {
                                                    //            rowIndex = i;
                                                    //            break;
                                                    //        }
                                                    //    }
                                                    //    if (rowIndex < 0)
                                                    //    {
                                                    //        row = dt_ryzymx.NewRow();
                                                    //        dt_ryzymx.Rows.Add(row);
                                                    //        row["ryID"] = item["IDCardNo"].ToString2();
                                                    //        row["ryzyzglxID"] = ryzyzglxid;
                                                    //        row["ryzyzglx"] = ryzyzglx;

                                                    //        if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                                    //        {
                                                    //            row["zyzglb"] = item["CertMajor"].ToString2().Trim();

                                                    //            SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                                    //            sp.Add("@CodeInfo", item["CertMajor"].ToString2().Trim());
                                                    //            sp.Add("@parentCode", ryzyzglxid);

                                                    //            DataTable dt_code = dataService.GetTable("select * from UEPP_Code where CodeType='人员资质类别' and ParentCodeType='人员执业资格类型' and ParentCode=@parentCode and CodeInfo=@CodeInfo", sp);
                                                    //            if (dt_code.Rows.Count == 0)
                                                    //            {
                                                    //                dt_code.Rows.Add(dt_code.NewRow());
                                                    //                dt_code.Rows[0]["ParentCodeType"] = "人员执业资格类型";
                                                    //                dt_code.Rows[0]["ParentCode"] = ryzyzglxid;
                                                    //                dt_code.Rows[0]["CodeType"] = "人员资质类别";
                                                    //                dt_code.Rows[0]["CodeInfo"] = item["CertMajor"].ToString2().Trim();
                                                    //                dt_code.Rows[0]["Code"] = dataService.Get_uepp_Code_NewCode("人员资质类别");
                                                    //                dt_code.Rows[0]["CodeDesc"] = "";
                                                    //                dt_code.Rows[0]["OrderID"] = dt_code.Rows[0]["Code"];
                                                    //                dataService.Submit_uepp_code(dt_code);

                                                    //            }
                                                    //            row["zyzglbID"] = dt_code.Rows[0]["Code"];
                                                    //        }


                                                    //        row["zzbz"] = "主项";
                                                    //        row["DataState"] = 0;
                                                    //        row["tag"] = tag;
                                                    //        row["xgr"] = "定时服务";
                                                    //        row["xgrqsj"] = updateDate;
                                                    //        dataService.Submit_uepp_Ryzymx(dt_ryzymx);
                                                    //    }
                                                    //}

                                                    #endregion

                                                    #region 人员证书基本信息
                                                    DataTable dt_ryzs = dataService.Get_uepp_Ryzs(item["IDCardNo"].ToString2());
                                                    rowIndex = -1;
                                                    for (int i = 0; i < dt_ryzs.Rows.Count; i++)
                                                    {
                                                        if (dt_ryzs.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                                        {
                                                            rowIndex = i;
                                                            break;
                                                        }
                                                    }
                                                    if (rowIndex < 0)
                                                    {
                                                        row = dt_ryzs.NewRow();
                                                        dt_ryzs.Rows.Add(row);
                                                        row["zsjlId"] = dataService.Get_uepp_RyzsNewID();
                                                        row["ryID"] = item["IDCardNo"].ToString2();
                                                        row["ryzyzglxID"] = ryzyzglxid;
                                                        row["ryzyzglx"] = ryzyzglx;
                                                        row["ryzslxID"] = ryzslxid;
                                                        row["ryzslx"] = ryzslx;
                                                    }
                                                    else
                                                    {
                                                        row = dt_ryzs.Rows[rowIndex];
                                                    }

                                                    row["sfzzz"] = 1;
                                                    row["zsbh"] = item["CertNo"].ToString2();
                                                    if (!string.IsNullOrEmpty(item["ValidDate"].ToString2()))
                                                        row["zsyxzrq"] = item["ValidDate"].ToString2();
                                                    row["fzdw"] = item["IssueOrgan"].ToString2();
                                                    if (!string.IsNullOrEmpty(item["IssueDate"].ToString2()))
                                                    {
                                                        row["fzrq"] = item["IssueDate"].ToString2();
                                                        row["zsyxqrq"] = item["IssueDate"].ToString2();
                                                    }

                                                    row["Status"] = item["Status"];
                                                    //if (dt_personJobCertInfo.Columns.Contains("QualIssueDate"))
                                                    //    row["QualIssueDate"] = item["QualIssueDate"];
                                                    //if (dt_personJobCertInfo.Columns.Contains("StampNo"))
                                                    //    row["StampNo"] = item["StampNo"];
                                                    //if (dt_personJobCertInfo.Columns.Contains("RegNo"))
                                                    //    row["RegNo"] = item["RegNo"];

                                                    row["DataState"] = 0;
                                                    row["tag"] = tag;
                                                    row["xgr"] = "定时服务";
                                                    row["xgrqsj"] = updateDate;
                                                    dataService.Submit_uepp_Ryzs(dt_ryzs);

                                                    #endregion

                                                    #region 企业人员关系表

                                                    DataTable dt_qyry = dataService.Get_uepp_Qyry(item["IDCardNo"].ToString2(), Public.ZzjgdmToStandard(item["CorpCode"].ToString2()), ryzyzglxid);
                                                    if (dt_qyry.Rows.Count == 0)
                                                    {
                                                        if (item["Status"].ToString2() != "2")
                                                        {
                                                            row = dt_qyry.NewRow();
                                                            dt_qyry.Rows.Add(row);
                                                            row["ryID"] = item["IDCardNo"].ToString2();
                                                            row["qyID"] = item["CorpCode"].ToString2();
                                                            row["ryzyzglxID"] = ryzyzglxid;
                                                            row["ryzyzglx"] = ryzyzglx;
                                                            row["DataState"] = 0;
                                                            row["tag"] = tag;
                                                            row["xgr"] = "定时服务";
                                                            row["xgrqsj"] = updateDate;
                                                            dataService.Submit_uepp_qyry(dt_qyry);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (item["Status"].ToString2() == "2")
                                                        {
                                                            dt_qyry.Rows[0].Delete();
                                                            dataService.Submit_uepp_qyry(dt_qyry);
                                                        }
                                                    }
                                                    #endregion
                                                }
                                                catch (Exception ex)
                                                {
                                                    success_count_ryxx++;
                                                    row_SaveDataLog["SaveState"] = 0;
                                                    row_SaveDataLog["Msg"] = "出现异常，ryID：" + item["IDCardNo"].ToString2() + ",Exception:" + ex.Message;
                                                }
                                                dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        #endregion
                        //Public.WriteLog("获取" + tag + "造价员：");
                        #region 造价员
                        rygwlx = "0207";
                        ryzyzglxid = "42";
                        ryzyzglx = "造价员";
                        ryzslxid = "141";
                        ryzslx = "造价员上岗证";

                        bytes = newdataService.getPersonJobCert(userID, "320200", xzqdm, rygwlx, "0");

                        result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);

                        index = result.IndexOf("<ReturnInfo>");
                        if (index >= 0)
                        {
                            string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
                            if (!string.IsNullOrEmpty(returnResult))
                            {
                                ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
                                if (returnInfo.Status)
                                {
                                    string str = result.Substring(result.IndexOf("</ReturnInfo>") + 13, result.IndexOf("</ResultSet>") - result.IndexOf("</ReturnInfo>") - 13);
                                    if (!string.IsNullOrEmpty(str))
                                    {
                                        index = result.IndexOf("<PersonJobCertInfo>");
                                        if (index >= 0)
                                        {
                                            string personJobCertInfoString = result.Substring(index, result.LastIndexOf("</PersonJobCertInfo>") - index + 20);
                                            string msg = "";
                                            DataTable dt_personJobCertInfo = helper.ConvertXMLToDataTable("<dataTable>" + personJobCertInfoString + "</dataTable>", out msg);
                                            if (string.IsNullOrEmpty(msg) && dt_personJobCertInfo != null && dt_personJobCertInfo.Rows.Count > 0)
                                            {
                                                foreach (DataRow item in dt_personJobCertInfo.Rows)
                                                {
                                                    DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                                                    row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_ryxx["ID"];
                                                    row_SaveDataLog["DataXml"] = "";
                                                    row_SaveDataLog["PKID"] = item["IDCardNo"].ToString2();
                                                    row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                                                    int rowIndex = -1;
                                                    try
                                                    {
                                                        string updateDate = item["UpdateDate"].ToString2();

                                                        #region 人员基本信息

                                                        DataTable dt_ryxx = dataService.Get_UEPP_Ryjbxx(item["IDCardNo"].ToString2());
                                                        if (dt_ryxx.Rows.Count == 0)
                                                        {
                                                            row = dt_ryxx.NewRow();
                                                            dt_ryxx.Rows.Add(row);
                                                            row["ryID"] = item["IDCardNo"].ToString2();
                                                        }
                                                        else
                                                        {
                                                            row = dt_ryxx.Rows[0];
                                                            if (!string.IsNullOrEmpty(row["xgrqsj"].ToString2()) && !string.IsNullOrEmpty(updateDate))
                                                                if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(updateDate).ToString("yyyy-MM-dd"))
                                                                {
                                                                    continue;
                                                                }
                                                        }

                                                        if (string.IsNullOrEmpty(updateDate))
                                                            updateDate = DateTime.Now.ToString("yyyy-MM-dd");
                                                        row["tag"] = tag;
                                                        row["xm"] = item["PersonName"];
                                                        switch (item["IDCardType"].ToString2())
                                                        {
                                                            case "1":
                                                                row["zjlxID"] = "1";
                                                                row["zjlx"] = "身份证";
                                                                break;
                                                            case "2":
                                                                row["zjlxID"] = "3";
                                                                row["zjlx"] = "护照";
                                                                break;
                                                            case "3":
                                                                row["zjlxID"] = "2";
                                                                row["zjlx"] = "军官证";
                                                                break;
                                                            case "4":
                                                                row["zjlxID"] = "4";
                                                                row["zjlx"] = "台湾居民身份证";

                                                                break;
                                                            case "5":
                                                                row["zjlxID"] = "5";
                                                                row["zjlx"] = "香港永久性居民身份证";

                                                                break;
                                                            case "6":
                                                                row["zjlxID"] = "6";
                                                                row["zjlx"] = "警官证";

                                                                break;
                                                            case "其他":
                                                                row["zjlxID"] = "9";
                                                                row["zjlx"] = "其他";

                                                                break;
                                                        }

                                                        row["zjhm"] = item["IDCardNo"];
                                                        //row["xb"] = item["Sex"];
                                                        //if (!string.IsNullOrEmpty(item["Birthday"].ToString2()))
                                                        //    row["csrq"] = item["Birthday"];
                                                        row["xgrqsj"] = updateDate;
                                                        row["xgr"] = "定时服务";
                                                        row["DataState"] = 0;
                                                        row["AJ_EXISTINIDCARDS"] = "2";
                                                        row["AJ_IsRefuse"] = "0";
                                                        allCount_ryxx++;
                                                        if (!dataService.Submit_uepp_ryjbxx(dt_ryxx))
                                                        {
                                                            //Public.WriteLog("建设单位人员信息保存失败，ryID：" + item["IDCardNo"].ToString2());
                                                            row_SaveDataLog["SaveState"] = 0;
                                                            row_SaveDataLog["Msg"] = "建设单位人员信息保存失败，ryID：" + item["IDCardNo"].ToString2();
                                                        }
                                                        else
                                                        {
                                                            success_count_ryxx++;
                                                            row_SaveDataLog["SaveState"] = 1;
                                                            row_SaveDataLog["Msg"] = "";
                                                        }

                                                        #endregion

                                                        #region 人员执业资格

                                                        DataTable dt_ryzyzg = dataService.Get_uepp_Ryzyzg(item["IDCardNo"].ToString2());
                                                        rowIndex = -1;
                                                        for (int i = 0; i < dt_ryzyzg.Rows.Count; i++)
                                                        {
                                                            if (dt_ryzyzg.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                                            {
                                                                rowIndex = i;
                                                                break;
                                                            }
                                                        }
                                                        if (rowIndex < 0)
                                                        {
                                                            row = dt_ryzyzg.NewRow();
                                                            dt_ryzyzg.Rows.Add(row);

                                                            row["ryID"] = item["IDCardNo"].ToString2();
                                                            row["ryzyzglxID"] = ryzyzglxid;
                                                            row["ryzyzglx"] = ryzyzglx;
                                                            row["balxID"] = 1;
                                                            row["balx"] = "长期备案";
                                                            row["DataState"] = 0;
                                                            row["tag"] = tag;
                                                            row["xgr"] = "定时服务";
                                                            row["xgrqsj"] = updateDate;
                                                            dataService.Submit_uepp_Ryzyzg(dt_ryzyzg);
                                                        }

                                                        #endregion

                                                        #region 人员专业明细

                                                        //if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                                        //{
                                                        //    DataTable dt_ryzymx = dataService.Get_uepp_Ryzymx(item["IDCardNo"].ToString2());
                                                        //    rowIndex = -1;
                                                        //    for (int i = 0; i < dt_ryzymx.Rows.Count; i++)
                                                        //    {
                                                        //        if (dt_ryzymx.Rows[i]["ryzyzglxID"].ToString2() == ryzyzglxid && dt_ryzymx.Rows[i]["zyzglb"].ToString2() == item["CertMajor"].ToString2().Trim())
                                                        //        {
                                                        //            rowIndex = i;
                                                        //            break;
                                                        //        }
                                                        //    }
                                                        //    if (rowIndex < 0)
                                                        //    {
                                                        //        row = dt_ryzymx.NewRow();
                                                        //        dt_ryzymx.Rows.Add(row);
                                                        //        row["ryID"] = item["IDCardNo"].ToString2();
                                                        //        row["ryzyzglxID"] = ryzyzglxid;
                                                        //        row["ryzyzglx"] = ryzyzglx;

                                                        //        if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                                        //        {
                                                        //            row["zyzglb"] = item["CertMajor"].ToString2().Trim();

                                                        //            SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                                        //            sp.Add("@CodeInfo", item["CertMajor"].ToString2().Trim());
                                                        //            sp.Add("@parentCode", ryzyzglxid);

                                                        //            DataTable dt_code = dataService.GetTable("select * from UEPP_Code where CodeType='人员资质类别' and ParentCodeType='人员执业资格类型' and ParentCode=@parentCode and CodeInfo=@CodeInfo", sp);
                                                        //            if (dt_code.Rows.Count == 0)
                                                        //            {
                                                        //                dt_code.Rows.Add(dt_code.NewRow());
                                                        //                dt_code.Rows[0]["ParentCodeType"] = "人员执业资格类型";
                                                        //                dt_code.Rows[0]["ParentCode"] = ryzyzglxid;
                                                        //                dt_code.Rows[0]["CodeType"] = "人员资质类别";
                                                        //                dt_code.Rows[0]["CodeInfo"] = item["CertMajor"].ToString2().Trim();
                                                        //                dt_code.Rows[0]["Code"] = dataService.Get_uepp_Code_NewCode("人员资质类别");
                                                        //                dt_code.Rows[0]["CodeDesc"] = "";
                                                        //                dt_code.Rows[0]["OrderID"] = dt_code.Rows[0]["Code"];
                                                        //                dataService.Submit_uepp_code(dt_code);

                                                        //            }
                                                        //            row["zyzglbID"] = dt_code.Rows[0]["Code"];
                                                        //        }


                                                        //        row["zzbz"] = "主项";
                                                        //        row["DataState"] = 0;
                                                        //        row["tag"] = tag;
                                                        //        row["xgr"] = "定时服务";
                                                        //        row["xgrqsj"] = updateDate;
                                                        //        dataService.Submit_uepp_Ryzymx(dt_ryzymx);
                                                        //    }
                                                        //}

                                                        #endregion

                                                        #region 人员证书基本信息
                                                        DataTable dt_ryzs = dataService.Get_uepp_Ryzs(item["IDCardNo"].ToString2());
                                                        rowIndex = -1;
                                                        for (int i = 0; i < dt_ryzs.Rows.Count; i++)
                                                        {
                                                            if (dt_ryzs.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                                            {
                                                                rowIndex = i;
                                                                break;
                                                            }
                                                        }
                                                        if (rowIndex < 0)
                                                        {

                                                            row = dt_ryzs.NewRow();
                                                            dt_ryzs.Rows.Add(row);
                                                            row["zsjlId"] = dataService.Get_uepp_RyzsNewID();
                                                            row["ryID"] = item["IDCardNo"].ToString2();
                                                            row["ryzyzglxID"] = ryzyzglxid;
                                                            row["ryzyzglx"] = ryzyzglx;
                                                            row["ryzslxID"] = ryzslxid;
                                                            row["ryzslx"] = ryzslx;


                                                        }
                                                        else
                                                        {
                                                            row = dt_ryzs.Rows[rowIndex];
                                                        }

                                                        row["sfzzz"] = 1;
                                                        row["zsbh"] = item["CertNo"].ToString2();
                                                        if (!string.IsNullOrEmpty(item["ValidDate"].ToString2()))
                                                            row["zsyxzrq"] = item["ValidDate"].ToString2();
                                                        row["fzdw"] = item["IssueOrgan"].ToString2();
                                                        if (!string.IsNullOrEmpty(item["IssueDate"].ToString2()))
                                                        {
                                                            row["fzrq"] = item["IssueDate"].ToString2();
                                                            row["zsyxqrq"] = item["IssueDate"].ToString2();
                                                        }

                                                        row["Status"] = item["Status"];
                                                        //if (dt_personJobCertInfo.Columns.Contains("QualIssueDate"))
                                                        //    row["QualIssueDate"] = item["QualIssueDate"];
                                                        //if (dt_personJobCertInfo.Columns.Contains("StampNo"))
                                                        //    row["StampNo"] = item["StampNo"];
                                                        //if (dt_personJobCertInfo.Columns.Contains("RegNo"))
                                                        //    row["RegNo"] = item["RegNo"];


                                                        row["DataState"] = 0;
                                                        row["tag"] = tag;
                                                        row["xgr"] = "定时服务";
                                                        row["xgrqsj"] = updateDate;
                                                        dataService.Submit_uepp_Ryzs(dt_ryzs);
                                                        #endregion

                                                        #region 企业人员关系表

                                                        DataTable dt_qyry = dataService.Get_uepp_Qyry(item["IDCardNo"].ToString2(), Public.ZzjgdmToStandard(item["CorpCode"].ToString2()), ryzyzglxid);
                                                        if (dt_qyry.Rows.Count == 0)
                                                        {
                                                            if (item["Status"].ToString2() != "2")
                                                            {
                                                                row = dt_qyry.NewRow();
                                                                dt_qyry.Rows.Add(row);
                                                                row["ryID"] = item["IDCardNo"].ToString2();
                                                                row["qyID"] = item["CorpCode"].ToString2();
                                                                row["ryzyzglxID"] = ryzyzglxid;
                                                                row["ryzyzglx"] = ryzyzglx;
                                                                row["DataState"] = 0;
                                                                row["tag"] = tag;
                                                                row["xgr"] = "定时服务";
                                                                row["xgrqsj"] = updateDate;
                                                                dataService.Submit_uepp_qyry(dt_qyry);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (item["Status"].ToString2() == "2")
                                                            {
                                                                dt_qyry.Rows[0].Delete();
                                                                dataService.Submit_uepp_qyry(dt_qyry);
                                                            }
                                                        }
                                                        #endregion
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Public.WriteLog("出现异常，ryID：" + item["IDCardNo"].ToString2() + ",Exception:" + ex.Message);
                                                        row_SaveDataLog["SaveState"] = 0;
                                                        row_SaveDataLog["Msg"] = ex.Message;
                                                    }
                                                    dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }

                        #endregion
                        //Public.WriteLog("获取" + tag + "劳务员：");
                        #region 劳务员
                        rygwlx = "0208";
                        ryzyzglxid = "91";
                        ryzyzglx = "劳务员";
                        ryzslxid = "842";
                        ryzslx = "劳务员上岗证";

                        bytes = newdataService.getPersonJobCert(userID, "320200", xzqdm, rygwlx, "0");

                        result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);

                        index = result.IndexOf("<ReturnInfo>");
                        if (index >= 0)
                        {
                            string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
                            if (!string.IsNullOrEmpty(returnResult))
                            {
                                ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
                                if (returnInfo.Status)
                                {

                                    index = result.IndexOf("<PersonJobCertInfo>");
                                    if (index >= 0)
                                    {
                                        string personJobCertInfoString = result.Substring(index, result.LastIndexOf("</PersonJobCertInfo>") - index + 20);
                                        string msg = "";
                                        DataTable dt_personJobCertInfo = helper.ConvertXMLToDataTable("<dataTable>" + personJobCertInfoString + "</dataTable>", out msg);
                                        if (string.IsNullOrEmpty(msg) && dt_personJobCertInfo != null && dt_personJobCertInfo.Rows.Count > 0)
                                        {
                                            foreach (DataRow item in dt_personJobCertInfo.Rows)
                                            {
                                                DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                                                row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_ryxx["ID"];
                                                row_SaveDataLog["DataXml"] = "";
                                                row_SaveDataLog["PKID"] = item["IDCardNo"].ToString2();
                                                row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                                                int rowIndex = -1;
                                                try
                                                {
                                                    string updateDate = item["UpdateDate"].ToString2();

                                                    #region 人员基本信息

                                                    DataTable dt_ryxx = dataService.Get_UEPP_Ryjbxx(item["IDCardNo"].ToString2());
                                                    if (dt_ryxx.Rows.Count == 0)
                                                    {
                                                        row = dt_ryxx.NewRow();
                                                        dt_ryxx.Rows.Add(row);
                                                        row["ryID"] = item["IDCardNo"].ToString2();
                                                    }
                                                    else
                                                    {
                                                        row = dt_ryxx.Rows[0];
                                                        if (!string.IsNullOrEmpty(row["xgrqsj"].ToString2()) && !string.IsNullOrEmpty(updateDate))
                                                            if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(updateDate).ToString("yyyy-MM-dd"))
                                                            {
                                                                continue;
                                                            }
                                                    }

                                                    if (string.IsNullOrEmpty(updateDate))
                                                        updateDate = DateTime.Now.ToString("yyyy-MM-dd");
                                                    row["tag"] = tag;
                                                    row["xm"] = item["PersonName"];
                                                    switch (item["IDCardType"].ToString2())
                                                    {
                                                        case "1":
                                                            row["zjlxID"] = "1";
                                                            row["zjlx"] = "身份证";
                                                            break;
                                                        case "2":
                                                            row["zjlxID"] = "3";
                                                            row["zjlx"] = "护照";
                                                            break;
                                                        case "3":
                                                            row["zjlxID"] = "2";
                                                            row["zjlx"] = "军官证";
                                                            break;
                                                        case "4":
                                                            row["zjlxID"] = "4";
                                                            row["zjlx"] = "台湾居民身份证";

                                                            break;
                                                        case "5":
                                                            row["zjlxID"] = "5";
                                                            row["zjlx"] = "香港永久性居民身份证";

                                                            break;
                                                        case "6":
                                                            row["zjlxID"] = "6";
                                                            row["zjlx"] = "警官证";

                                                            break;
                                                        case "其他":
                                                            row["zjlxID"] = "9";
                                                            row["zjlx"] = "其他";

                                                            break;
                                                    }

                                                    row["zjhm"] = item["IDCardNo"];
                                                    //row["xb"] = item["Sex"];
                                                    //if (!string.IsNullOrEmpty(item["Birthday"].ToString2()))
                                                    //    row["csrq"] = item["Birthday"];
                                                    row["xgrqsj"] = updateDate;
                                                    row["xgr"] = "定时服务";
                                                    row["DataState"] = 0;
                                                    row["AJ_EXISTINIDCARDS"] = "2";
                                                    row["AJ_IsRefuse"] = "0";
                                                    allCount_ryxx++;
                                                    if (!dataService.Submit_uepp_ryjbxx(dt_ryxx))
                                                    {
                                                        row_SaveDataLog["SaveState"] = 0;
                                                        row_SaveDataLog["Msg"] = "建设单位人员信息保存失败，ryID：" + item["IDCardNo"].ToString2();
                                                    }
                                                    else
                                                    {
                                                        success_count_ryxx++;
                                                        row_SaveDataLog["SaveState"] = 1;
                                                        row_SaveDataLog["Msg"] = "";
                                                    }
                                                    #endregion

                                                    #region 人员执业资格

                                                    DataTable dt_ryzyzg = dataService.Get_uepp_Ryzyzg(item["IDCardNo"].ToString2());
                                                    rowIndex = -1;
                                                    for (int i = 0; i < dt_ryzyzg.Rows.Count; i++)
                                                    {
                                                        if (dt_ryzyzg.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                                        {
                                                            rowIndex = i;
                                                            break;
                                                        }
                                                    }
                                                    if (rowIndex < 0)
                                                    {
                                                        row = dt_ryzyzg.NewRow();
                                                        dt_ryzyzg.Rows.Add(row);

                                                        row["ryID"] = item["IDCardNo"].ToString2();
                                                        row["ryzyzglxID"] = ryzyzglxid;
                                                        row["ryzyzglx"] = ryzyzglx;
                                                        row["balxID"] = 1;
                                                        row["balx"] = "长期备案";
                                                        row["DataState"] = 0;
                                                        row["tag"] = tag;
                                                        row["xgr"] = "定时服务";
                                                        row["xgrqsj"] = updateDate;
                                                        dataService.Submit_uepp_Ryzyzg(dt_ryzyzg);
                                                    }

                                                    #endregion

                                                    #region 人员专业明细

                                                    //if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                                    //{
                                                    //    DataTable dt_ryzymx = dataService.Get_uepp_Ryzymx(item["IDCardNo"].ToString2());
                                                    //    rowIndex = -1;
                                                    //    for (int i = 0; i < dt_ryzymx.Rows.Count; i++)
                                                    //    {
                                                    //        if (dt_ryzymx.Rows[i]["ryzyzglxID"].ToString2() == ryzyzglxid && dt_ryzymx.Rows[i]["zyzglb"].ToString2() == item["CertMajor"].ToString2().Trim())
                                                    //        {
                                                    //            rowIndex = i;
                                                    //            break;
                                                    //        }
                                                    //    }
                                                    //    if (rowIndex < 0)
                                                    //    {
                                                    //        row = dt_ryzymx.NewRow();
                                                    //        dt_ryzymx.Rows.Add(row);
                                                    //        row["ryID"] = item["IDCardNo"].ToString2();
                                                    //        row["ryzyzglxID"] = ryzyzglxid;
                                                    //        row["ryzyzglx"] = ryzyzglx;

                                                    //        if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                                    //        {
                                                    //            row["zyzglb"] = item["CertMajor"].ToString2().Trim();

                                                    //            SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                                    //            sp.Add("@CodeInfo", item["CertMajor"].ToString2().Trim());
                                                    //            sp.Add("@parentCode", ryzyzglxid);

                                                    //            DataTable dt_code = dataService.GetTable("select * from UEPP_Code where CodeType='人员资质类别' and ParentCodeType='人员执业资格类型' and ParentCode=@parentCode and CodeInfo=@CodeInfo", sp);
                                                    //            if (dt_code.Rows.Count == 0)
                                                    //            {
                                                    //                dt_code.Rows.Add(dt_code.NewRow());
                                                    //                dt_code.Rows[0]["ParentCodeType"] = "人员执业资格类型";
                                                    //                dt_code.Rows[0]["ParentCode"] = ryzyzglxid;
                                                    //                dt_code.Rows[0]["CodeType"] = "人员资质类别";
                                                    //                dt_code.Rows[0]["CodeInfo"] = item["CertMajor"].ToString2().Trim();
                                                    //                dt_code.Rows[0]["Code"] = dataService.Get_uepp_Code_NewCode("人员资质类别");
                                                    //                dt_code.Rows[0]["CodeDesc"] = "";
                                                    //                dt_code.Rows[0]["OrderID"] = dt_code.Rows[0]["Code"];
                                                    //                dataService.Submit_uepp_code(dt_code);

                                                    //            }
                                                    //            row["zyzglbID"] = dt_code.Rows[0]["Code"];
                                                    //        }


                                                    //        row["zzbz"] = "主项";
                                                    //        row["DataState"] = 0;
                                                    //        row["tag"] = tag;
                                                    //        row["xgr"] = "定时服务";
                                                    //        row["xgrqsj"] = updateDate;
                                                    //        dataService.Submit_uepp_Ryzymx(dt_ryzymx);
                                                    //    }
                                                    //}

                                                    #endregion

                                                    #region 人员证书基本信息
                                                    DataTable dt_ryzs = dataService.Get_uepp_Ryzs(item["IDCardNo"].ToString2());
                                                    rowIndex = -1;
                                                    for (int i = 0; i < dt_ryzs.Rows.Count; i++)
                                                    {
                                                        if (dt_ryzs.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                                        {
                                                            rowIndex = i;
                                                            break;
                                                        }
                                                    }
                                                    if (rowIndex < 0)
                                                    {

                                                        row = dt_ryzs.NewRow();
                                                        dt_ryzs.Rows.Add(row);
                                                        row["zsjlId"] = dataService.Get_uepp_RyzsNewID();
                                                        row["ryID"] = item["IDCardNo"].ToString2();
                                                        row["ryzyzglxID"] = ryzyzglxid;
                                                        row["ryzyzglx"] = ryzyzglx;
                                                        row["ryzslxID"] = ryzslxid;
                                                        row["ryzslx"] = ryzslx;


                                                    }
                                                    else
                                                    {
                                                        row = dt_ryzs.Rows[rowIndex];
                                                    }

                                                    row["sfzzz"] = 1;
                                                    row["zsbh"] = item["CertNo"].ToString2();
                                                    if (!string.IsNullOrEmpty(item["ValidDate"].ToString2()))
                                                        row["zsyxzrq"] = item["ValidDate"].ToString2();
                                                    row["fzdw"] = item["IssueOrgan"].ToString2();
                                                    if (!string.IsNullOrEmpty(item["IssueDate"].ToString2()))
                                                    {
                                                        row["fzrq"] = item["IssueDate"].ToString2();
                                                        row["zsyxqrq"] = item["IssueDate"].ToString2();
                                                    }

                                                    row["Status"] = item["Status"];
                                                    //if (dt_personJobCertInfo.Columns.Contains("QualIssueDate"))
                                                    //    row["QualIssueDate"] = item["QualIssueDate"];
                                                    //if (dt_personJobCertInfo.Columns.Contains("StampNo"))
                                                    //    row["StampNo"] = item["StampNo"];
                                                    //if (dt_personJobCertInfo.Columns.Contains("RegNo"))
                                                    //    row["RegNo"] = item["RegNo"];

                                                    row["DataState"] = 0;
                                                    row["tag"] = tag;
                                                    row["xgr"] = "定时服务";
                                                    row["xgrqsj"] = updateDate;
                                                    dataService.Submit_uepp_Ryzs(dt_ryzs);

                                                    #endregion

                                                    #region 企业人员关系表


                                                    DataTable dt_qyry = dataService.Get_uepp_Qyry(item["IDCardNo"].ToString2(), Public.ZzjgdmToStandard(item["CorpCode"].ToString2()), ryzyzglxid);
                                                    if (dt_qyry.Rows.Count == 0)
                                                    {
                                                        if (item["Status"].ToString2() != "2")
                                                        {
                                                            row = dt_qyry.NewRow();
                                                            dt_qyry.Rows.Add(row);
                                                            row["ryID"] = item["IDCardNo"].ToString2();
                                                            row["qyID"] = item["CorpCode"].ToString2();
                                                            row["ryzyzglxID"] = ryzyzglxid;
                                                            row["ryzyzglx"] = ryzyzglx;
                                                            row["DataState"] = 0;
                                                            row["tag"] = tag;
                                                            row["xgr"] = "定时服务";
                                                            row["xgrqsj"] = updateDate;
                                                            dataService.Submit_uepp_qyry(dt_qyry);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (item["Status"].ToString2() == "2")
                                                        {
                                                            dt_qyry.Rows[0].Delete();
                                                            dataService.Submit_uepp_qyry(dt_qyry);
                                                        }
                                                    }
                                                    #endregion
                                                }
                                                catch (Exception ex)
                                                {
                                                    row_SaveDataLog["SaveState"] = 0;
                                                    row_SaveDataLog["Msg"] = "出现异常，ryID：" + item["IDCardNo"].ToString2() + ",Exception:" + ex.Message;
                                                }
                                                dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                        //Public.WriteLog("获取" + tag + "测量员：");
                        #region 测量员
                        rygwlx = "0209";
                        ryzyzglxid = "92";
                        ryzyzglx = "测量员";
                        ryzslxid = "843";
                        ryzslx = "测量员上岗证";

                        bytes = newdataService.getPersonJobCert(userID, "320200", xzqdm, rygwlx, "0");

                        result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);

                        index = result.IndexOf("<ReturnInfo>");
                        if (index >= 0)
                        {
                            string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
                            if (!string.IsNullOrEmpty(returnResult))
                            {
                                ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
                                if (returnInfo.Status)
                                {
                                    index = result.IndexOf("<PersonJobCertInfo>");
                                    if (index >= 0)
                                    {
                                        string personJobCertInfoString = result.Substring(index, result.LastIndexOf("</PersonJobCertInfo>") - index + 20);
                                        string msg = "";
                                        DataTable dt_personJobCertInfo = helper.ConvertXMLToDataTable("<dataTable>" + personJobCertInfoString + "</dataTable>", out msg);
                                        if (string.IsNullOrEmpty(msg) && dt_personJobCertInfo != null && dt_personJobCertInfo.Rows.Count > 0)
                                        {
                                            foreach (DataRow item in dt_personJobCertInfo.Rows)
                                            {
                                                DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                                                row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_ryxx["ID"];
                                                row_SaveDataLog["DataXml"] = "";
                                                row_SaveDataLog["PKID"] = item["IDCardNo"].ToString2();
                                                row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                                                int rowIndex = -1;
                                                try
                                                {
                                                    string updateDate = item["UpdateDate"].ToString2();

                                                    #region 人员基本信息

                                                    DataTable dt_ryxx = dataService.Get_UEPP_Ryjbxx(item["IDCardNo"].ToString2());
                                                    if (dt_ryxx.Rows.Count == 0)
                                                    {
                                                        row = dt_ryxx.NewRow();
                                                        dt_ryxx.Rows.Add(row);
                                                        row["ryID"] = item["IDCardNo"].ToString2();
                                                    }
                                                    else
                                                    {
                                                        row = dt_ryxx.Rows[0];
                                                        if (!string.IsNullOrEmpty(row["xgrqsj"].ToString2()) && !string.IsNullOrEmpty(updateDate))
                                                            if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(updateDate).ToString("yyyy-MM-dd"))
                                                            {
                                                                continue;
                                                            }
                                                    }

                                                    if (string.IsNullOrEmpty(updateDate))
                                                        updateDate = DateTime.Now.ToString("yyyy-MM-dd");
                                                    row["tag"] = tag;
                                                    row["xm"] = item["PersonName"];
                                                    switch (item["IDCardType"].ToString2())
                                                    {
                                                        case "1":
                                                            row["zjlxID"] = "1";
                                                            row["zjlx"] = "身份证";
                                                            break;
                                                        case "2":
                                                            row["zjlxID"] = "3";
                                                            row["zjlx"] = "护照";
                                                            break;
                                                        case "3":
                                                            row["zjlxID"] = "2";
                                                            row["zjlx"] = "军官证";
                                                            break;
                                                        case "4":
                                                            row["zjlxID"] = "4";
                                                            row["zjlx"] = "台湾居民身份证";

                                                            break;
                                                        case "5":
                                                            row["zjlxID"] = "5";
                                                            row["zjlx"] = "香港永久性居民身份证";

                                                            break;
                                                        case "6":
                                                            row["zjlxID"] = "6";
                                                            row["zjlx"] = "警官证";

                                                            break;
                                                        case "其他":
                                                            row["zjlxID"] = "9";
                                                            row["zjlx"] = "其他";

                                                            break;
                                                    }

                                                    row["zjhm"] = item["IDCardNo"];
                                                    //row["xb"] = item["Sex"];
                                                    //if (!string.IsNullOrEmpty(item["Birthday"].ToString2()))
                                                    //    row["csrq"] = item["Birthday"];
                                                    row["xgrqsj"] = updateDate;
                                                    row["xgr"] = "定时服务";
                                                    row["DataState"] = 0;
                                                    row["AJ_EXISTINIDCARDS"] = "2";
                                                    row["AJ_IsRefuse"] = "0";
                                                    allCount_ryxx++;
                                                    if (!dataService.Submit_uepp_ryjbxx(dt_ryxx))
                                                    {
                                                        row_SaveDataLog["SaveState"] = 0;
                                                        row_SaveDataLog["Msg"] = "建设单位人员信息保存失败，ryID：" + item["IDCardNo"].ToString2();
                                                    }
                                                    else
                                                    {
                                                        success_count_ryxx++;
                                                        row_SaveDataLog["SaveState"] = 1;
                                                        row_SaveDataLog["Msg"] = "";
                                                    }

                                                    #endregion

                                                    #region 人员执业资格

                                                    DataTable dt_ryzyzg = dataService.Get_uepp_Ryzyzg(item["IDCardNo"].ToString2());
                                                    rowIndex = -1;
                                                    for (int i = 0; i < dt_ryzyzg.Rows.Count; i++)
                                                    {
                                                        if (dt_ryzyzg.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                                        {
                                                            rowIndex = i;
                                                            break;
                                                        }
                                                    }
                                                    if (rowIndex < 0)
                                                    {
                                                        row = dt_ryzyzg.NewRow();
                                                        dt_ryzyzg.Rows.Add(row);

                                                        row["ryID"] = item["IDCardNo"].ToString2();
                                                        row["ryzyzglxID"] = ryzyzglxid;
                                                        row["ryzyzglx"] = ryzyzglx;
                                                        row["balxID"] = 1;
                                                        row["balx"] = "长期备案";
                                                        row["DataState"] = 0;
                                                        row["tag"] = tag;
                                                        row["xgr"] = "定时服务";
                                                        row["xgrqsj"] = updateDate;
                                                        dataService.Submit_uepp_Ryzyzg(dt_ryzyzg);
                                                    }

                                                    #endregion

                                                    #region 人员专业明细

                                                    //if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                                    //{
                                                    //    DataTable dt_ryzymx = dataService.Get_uepp_Ryzymx(item["IDCardNo"].ToString2());
                                                    //    rowIndex = -1;
                                                    //    for (int i = 0; i < dt_ryzymx.Rows.Count; i++)
                                                    //    {
                                                    //        if (dt_ryzymx.Rows[i]["ryzyzglxID"].ToString2() == ryzyzglxid && dt_ryzymx.Rows[i]["zyzglb"].ToString2() == item["CertMajor"].ToString2().Trim())
                                                    //        {
                                                    //            rowIndex = i;
                                                    //            break;
                                                    //        }
                                                    //    }
                                                    //    if (rowIndex < 0)
                                                    //    {
                                                    //        row = dt_ryzymx.NewRow();
                                                    //        dt_ryzymx.Rows.Add(row);
                                                    //        row["ryID"] = item["IDCardNo"].ToString2();
                                                    //        row["ryzyzglxID"] = ryzyzglxid;
                                                    //        row["ryzyzglx"] = ryzyzglx;

                                                    //        if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                                    //        {
                                                    //            row["zyzglb"] = item["CertMajor"].ToString2().Trim();

                                                    //            SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                                    //            sp.Add("@CodeInfo", item["CertMajor"].ToString2().Trim());
                                                    //            sp.Add("@parentCode", ryzyzglxid);

                                                    //            DataTable dt_code = dataService.GetTable("select * from UEPP_Code where CodeType='人员资质类别' and ParentCodeType='人员执业资格类型' and ParentCode=@parentCode and CodeInfo=@CodeInfo", sp);
                                                    //            if (dt_code.Rows.Count == 0)
                                                    //            {
                                                    //                dt_code.Rows.Add(dt_code.NewRow());
                                                    //                dt_code.Rows[0]["ParentCodeType"] = "人员执业资格类型";
                                                    //                dt_code.Rows[0]["ParentCode"] = ryzyzglxid;
                                                    //                dt_code.Rows[0]["CodeType"] = "人员资质类别";
                                                    //                dt_code.Rows[0]["CodeInfo"] = item["CertMajor"].ToString2().Trim();
                                                    //                dt_code.Rows[0]["Code"] = dataService.Get_uepp_Code_NewCode("人员资质类别");
                                                    //                dt_code.Rows[0]["CodeDesc"] = "";
                                                    //                dt_code.Rows[0]["OrderID"] = dt_code.Rows[0]["Code"];
                                                    //                dataService.Submit_uepp_code(dt_code);

                                                    //            }
                                                    //            row["zyzglbID"] = dt_code.Rows[0]["Code"];
                                                    //        }


                                                    //        row["zzbz"] = "主项";
                                                    //        row["DataState"] = 0;
                                                    //        row["tag"] = tag;
                                                    //        row["xgr"] = "定时服务";
                                                    //        row["xgrqsj"] = updateDate;
                                                    //        dataService.Submit_uepp_Ryzymx(dt_ryzymx);
                                                    //    }
                                                    //}

                                                    #endregion

                                                    #region 人员证书基本信息
                                                    DataTable dt_ryzs = dataService.Get_uepp_Ryzs(item["IDCardNo"].ToString2());
                                                    rowIndex = -1;
                                                    for (int i = 0; i < dt_ryzs.Rows.Count; i++)
                                                    {
                                                        if (dt_ryzs.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                                        {
                                                            rowIndex = i;
                                                            break;
                                                        }
                                                    }
                                                    if (rowIndex < 0)
                                                    {

                                                        row = dt_ryzs.NewRow();
                                                        dt_ryzs.Rows.Add(row);
                                                        row["zsjlId"] = dataService.Get_uepp_RyzsNewID();
                                                        row["ryID"] = item["IDCardNo"].ToString2();
                                                        row["ryzyzglxID"] = ryzyzglxid;
                                                        row["ryzyzglx"] = ryzyzglx;
                                                        row["ryzslxID"] = ryzslxid;
                                                        row["ryzslx"] = ryzslx;


                                                    }
                                                    else
                                                    {
                                                        row = dt_ryzs.Rows[rowIndex];
                                                    }

                                                    row["sfzzz"] = 1;
                                                    row["zsbh"] = item["CertNo"].ToString2();
                                                    if (!string.IsNullOrEmpty(item["ValidDate"].ToString2()))
                                                        row["zsyxzrq"] = item["ValidDate"].ToString2();
                                                    row["fzdw"] = item["IssueOrgan"].ToString2();
                                                    if (!string.IsNullOrEmpty(item["IssueDate"].ToString2()))
                                                    {
                                                        row["fzrq"] = item["IssueDate"].ToString2();
                                                        row["zsyxqrq"] = item["IssueDate"].ToString2();
                                                    }
                                                    row["Status"] = item["Status"];
                                                    //if (dt_personJobCertInfo.Columns.Contains("QualIssueDate"))
                                                    //    row["QualIssueDate"] = item["QualIssueDate"];
                                                    //if (dt_personJobCertInfo.Columns.Contains("StampNo"))
                                                    //    row["StampNo"] = item["StampNo"];
                                                    //if (dt_personJobCertInfo.Columns.Contains("RegNo"))
                                                    //    row["RegNo"] = item["RegNo"];

                                                    row["DataState"] = 0;
                                                    row["tag"] = tag;
                                                    row["xgr"] = "定时服务";
                                                    row["xgrqsj"] = updateDate;
                                                    dataService.Submit_uepp_Ryzs(dt_ryzs);
                                                    #endregion


                                                    #region 企业人员关系表

                                                    DataTable dt_qyry = dataService.Get_uepp_Qyry(item["IDCardNo"].ToString2(), Public.ZzjgdmToStandard(item["CorpCode"].ToString2()), ryzyzglxid);

                                                    if (dt_qyry.Rows.Count == 0)
                                                    {
                                                        if (item["Status"].ToString2() != "2")
                                                        {
                                                            row = dt_qyry.NewRow();
                                                            dt_qyry.Rows.Add(row);
                                                            row["ryID"] = item["IDCardNo"].ToString2();
                                                            row["qyID"] = item["CorpCode"].ToString2();
                                                            row["ryzyzglxID"] = ryzyzglxid;
                                                            row["ryzyzglx"] = ryzyzglx;
                                                            row["DataState"] = 0;
                                                            row["tag"] = tag;
                                                            row["xgr"] = "定时服务";
                                                            row["xgrqsj"] = updateDate;
                                                            dataService.Submit_uepp_qyry(dt_qyry);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (item["Status"].ToString2() == "2")
                                                        {
                                                            dt_qyry.Rows[0].Delete();
                                                            dataService.Submit_uepp_qyry(dt_qyry);
                                                        }
                                                    }
                                                    #endregion
                                                }
                                                catch (Exception ex)
                                                {
                                                    row_SaveDataLog["SaveState"] = 0;
                                                    row_SaveDataLog["Msg"] = "出现异常，ryID：" + item["IDCardNo"].ToString2() + ",Exception:" + ex.Message;
                                                }
                                                dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                                            }
                                        }
                                    }
                                }
                            }
                        }


                        #endregion
                        //Public.WriteLog("获取" + tag + "试验员：");
                        #region 试验员
                        rygwlx = "0210";
                        ryzyzglxid = "14";
                        ryzyzglx = "试验员";
                        ryzslxid = "84";
                        ryzslx = "试验员上岗证";

                        bytes = newdataService.getPersonJobCert(userID, "320200", xzqdm, rygwlx, "0");

                        result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);

                        index = result.IndexOf("<ReturnInfo>");
                        if (index >= 0)
                        {
                            string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
                            if (!string.IsNullOrEmpty(returnResult))
                            {
                                ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
                                if (returnInfo.Status)
                                {
                                    index = result.IndexOf("<PersonJobCertInfo>");
                                    if (index >= 0)
                                    {
                                        string personJobCertInfoString = result.Substring(index, result.LastIndexOf("</PersonJobCertInfo>") - index + 20);
                                        string msg = "";
                                        DataTable dt_personJobCertInfo = helper.ConvertXMLToDataTable("<dataTable>" + personJobCertInfoString + "</dataTable>", out msg);
                                        if (string.IsNullOrEmpty(msg) && dt_personJobCertInfo != null && dt_personJobCertInfo.Rows.Count > 0)
                                        {
                                            foreach (DataRow item in dt_personJobCertInfo.Rows)
                                            {
                                                DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                                                row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_ryxx["ID"];
                                                row_SaveDataLog["DataXml"] = "";
                                                row_SaveDataLog["PKID"] = item["IDCardNo"].ToString2();
                                                row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                                                int rowIndex = -1;
                                                try
                                                {
                                                    string updateDate = item["UpdateDate"].ToString2();

                                                    #region 人员基本信息

                                                    DataTable dt_ryxx = dataService.Get_UEPP_Ryjbxx(item["IDCardNo"].ToString2());
                                                    if (dt_ryxx.Rows.Count == 0)
                                                    {
                                                        row = dt_ryxx.NewRow();
                                                        dt_ryxx.Rows.Add(row);
                                                        row["ryID"] = item["IDCardNo"].ToString2();
                                                    }
                                                    else
                                                    {
                                                        row = dt_ryxx.Rows[0];
                                                        if (!string.IsNullOrEmpty(row["xgrqsj"].ToString2()) && !string.IsNullOrEmpty(updateDate))
                                                            if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(updateDate).ToString("yyyy-MM-dd"))
                                                            {
                                                                continue;
                                                            }
                                                    }

                                                    if (string.IsNullOrEmpty(updateDate))
                                                        updateDate = DateTime.Now.ToString("yyyy-MM-dd");
                                                    row["tag"] = tag;
                                                    row["xm"] = item["PersonName"];
                                                    switch (item["IDCardType"].ToString2())
                                                    {
                                                        case "1":
                                                            row["zjlxID"] = "1";
                                                            row["zjlx"] = "身份证";
                                                            break;
                                                        case "2":
                                                            row["zjlxID"] = "3";
                                                            row["zjlx"] = "护照";
                                                            break;
                                                        case "3":
                                                            row["zjlxID"] = "2";
                                                            row["zjlx"] = "军官证";
                                                            break;
                                                        case "4":
                                                            row["zjlxID"] = "4";
                                                            row["zjlx"] = "台湾居民身份证";

                                                            break;
                                                        case "5":
                                                            row["zjlxID"] = "5";
                                                            row["zjlx"] = "香港永久性居民身份证";

                                                            break;
                                                        case "6":
                                                            row["zjlxID"] = "6";
                                                            row["zjlx"] = "警官证";

                                                            break;
                                                        case "其他":
                                                            row["zjlxID"] = "9";
                                                            row["zjlx"] = "其他";

                                                            break;
                                                    }

                                                    row["zjhm"] = item["IDCardNo"];
                                                    //row["xb"] = item["Sex"];
                                                    //if (!string.IsNullOrEmpty(item["Birthday"].ToString2()))
                                                    //    row["csrq"] = item["Birthday"];
                                                    row["xgrqsj"] = updateDate;
                                                    row["xgr"] = "定时服务";
                                                    row["DataState"] = 0;
                                                    row["AJ_EXISTINIDCARDS"] = "2";
                                                    row["AJ_IsRefuse"] = "0";
                                                    allCount_ryxx++;
                                                    if (!dataService.Submit_uepp_ryjbxx(dt_ryxx))
                                                    {
                                                        row_SaveDataLog["SaveState"] = 0;
                                                        row_SaveDataLog["Msg"] = "建设单位人员信息保存失败，ryID：" + item["IDCardNo"].ToString2();
                                                    }
                                                    else
                                                    {
                                                        success_count_ryxx++;
                                                        row_SaveDataLog["SaveState"] = 1;
                                                        row_SaveDataLog["Msg"] = "";
                                                    }
                                                    #endregion

                                                    #region 人员执业资格

                                                    DataTable dt_ryzyzg = dataService.Get_uepp_Ryzyzg(item["IDCardNo"].ToString2());
                                                    rowIndex = -1;
                                                    for (int i = 0; i < dt_ryzyzg.Rows.Count; i++)
                                                    {
                                                        if (dt_ryzyzg.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                                        {
                                                            rowIndex = i;
                                                            break;
                                                        }
                                                    }
                                                    if (rowIndex < 0)
                                                    {
                                                        row = dt_ryzyzg.NewRow();
                                                        dt_ryzyzg.Rows.Add(row);

                                                        row["ryID"] = item["IDCardNo"].ToString2();
                                                        row["ryzyzglxID"] = ryzyzglxid;
                                                        row["ryzyzglx"] = ryzyzglx;
                                                        row["balxID"] = 1;
                                                        row["balx"] = "长期备案";
                                                        row["DataState"] = 0;
                                                        row["tag"] = tag;
                                                        row["xgr"] = "定时服务";
                                                        row["xgrqsj"] = updateDate;
                                                        dataService.Submit_uepp_Ryzyzg(dt_ryzyzg);
                                                    }

                                                    #endregion

                                                    #region 人员专业明细

                                                    //if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                                    //{
                                                    //    DataTable dt_ryzymx = dataService.Get_uepp_Ryzymx(item["IDCardNo"].ToString2());
                                                    //    rowIndex = -1;
                                                    //    for (int i = 0; i < dt_ryzymx.Rows.Count; i++)
                                                    //    {
                                                    //        if (dt_ryzymx.Rows[i]["ryzyzglxID"].ToString2() == ryzyzglxid && dt_ryzymx.Rows[i]["zyzglb"].ToString2() == item["CertMajor"].ToString2().Trim())
                                                    //        {
                                                    //            rowIndex = i;
                                                    //            break;
                                                    //        }
                                                    //    }
                                                    //    if (rowIndex < 0)
                                                    //    {
                                                    //        row = dt_ryzymx.NewRow();
                                                    //        dt_ryzymx.Rows.Add(row);
                                                    //        row["ryID"] = item["IDCardNo"].ToString2();
                                                    //        row["ryzyzglxID"] = ryzyzglxid;
                                                    //        row["ryzyzglx"] = ryzyzglx;

                                                    //        if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                                    //        {
                                                    //            row["zyzglb"] = item["CertMajor"].ToString2().Trim();

                                                    //            SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                                    //            sp.Add("@CodeInfo", item["CertMajor"].ToString2().Trim());
                                                    //            sp.Add("@parentCode", ryzyzglxid);

                                                    //            DataTable dt_code = dataService.GetTable("select * from UEPP_Code where CodeType='人员资质类别' and ParentCodeType='人员执业资格类型' and ParentCode=@parentCode and CodeInfo=@CodeInfo", sp);
                                                    //            if (dt_code.Rows.Count == 0)
                                                    //            {
                                                    //                dt_code.Rows.Add(dt_code.NewRow());
                                                    //                dt_code.Rows[0]["ParentCodeType"] = "人员执业资格类型";
                                                    //                dt_code.Rows[0]["ParentCode"] = ryzyzglxid;
                                                    //                dt_code.Rows[0]["CodeType"] = "人员资质类别";
                                                    //                dt_code.Rows[0]["CodeInfo"] = item["CertMajor"].ToString2().Trim();
                                                    //                dt_code.Rows[0]["Code"] = dataService.Get_uepp_Code_NewCode("人员资质类别");
                                                    //                dt_code.Rows[0]["CodeDesc"] = "";
                                                    //                dt_code.Rows[0]["OrderID"] = dt_code.Rows[0]["Code"];
                                                    //                dataService.Submit_uepp_code(dt_code);

                                                    //            }
                                                    //            row["zyzglbID"] = dt_code.Rows[0]["Code"];
                                                    //        }


                                                    //        row["zzbz"] = "主项";
                                                    //        row["DataState"] = 0;
                                                    //        row["tag"] = tag;
                                                    //        row["xgr"] = "定时服务";
                                                    //        row["xgrqsj"] = updateDate;
                                                    //        dataService.Submit_uepp_Ryzymx(dt_ryzymx);
                                                    //    }
                                                    //}

                                                    #endregion

                                                    #region 人员证书基本信息
                                                    DataTable dt_ryzs = dataService.Get_uepp_Ryzs(item["IDCardNo"].ToString2());
                                                    rowIndex = -1;
                                                    for (int i = 0; i < dt_ryzs.Rows.Count; i++)
                                                    {
                                                        if (dt_ryzs.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                                        {
                                                            rowIndex = i;
                                                            break;
                                                        }
                                                    }
                                                    if (rowIndex < 0)
                                                    {

                                                        row = dt_ryzs.NewRow();
                                                        dt_ryzs.Rows.Add(row);
                                                        row["zsjlId"] = dataService.Get_uepp_RyzsNewID();
                                                        row["ryID"] = item["IDCardNo"].ToString2();
                                                        row["ryzyzglxID"] = ryzyzglxid;
                                                        row["ryzyzglx"] = ryzyzglx;
                                                        row["ryzslxID"] = ryzslxid;
                                                        row["ryzslx"] = ryzslx;


                                                    }
                                                    else
                                                    {
                                                        row = dt_ryzs.Rows[rowIndex];
                                                    }
                                                    row["sfzzz"] = 1;
                                                    row["zsbh"] = item["CertNo"].ToString2();
                                                    if (!string.IsNullOrEmpty(item["ValidDate"].ToString2()))
                                                        row["zsyxzrq"] = item["ValidDate"].ToString2();
                                                    row["fzdw"] = item["IssueOrgan"].ToString2();
                                                    if (!string.IsNullOrEmpty(item["IssueDate"].ToString2()))
                                                    {
                                                        row["fzrq"] = item["IssueDate"].ToString2();
                                                        row["zsyxqrq"] = item["IssueDate"].ToString2();
                                                    }
                                                    row["Status"] = item["Status"];
                                                    //if (dt_personJobCertInfo.Columns.Contains("QualIssueDate"))
                                                    //    row["QualIssueDate"] = item["QualIssueDate"];
                                                    //if (dt_personJobCertInfo.Columns.Contains("StampNo"))
                                                    //    row["StampNo"] = item["StampNo"];
                                                    //if (dt_personJobCertInfo.Columns.Contains("RegNo"))
                                                    //    row["RegNo"] = item["RegNo"];

                                                    row["DataState"] = 0;
                                                    row["tag"] = tag;
                                                    row["xgr"] = "定时服务";
                                                    row["xgrqsj"] = updateDate;
                                                    dataService.Submit_uepp_Ryzs(dt_ryzs);

                                                    #endregion

                                                    #region 企业人员关系表

                                                    DataTable dt_qyry = dataService.Get_uepp_Qyry(item["IDCardNo"].ToString2(), Public.ZzjgdmToStandard(item["CorpCode"].ToString2()), ryzyzglxid);
                                                    if (dt_qyry.Rows.Count == 0)
                                                    {
                                                        if (item["Status"].ToString2() != "2")
                                                        {
                                                            row = dt_qyry.NewRow();
                                                            dt_qyry.Rows.Add(row);
                                                            row["ryID"] = item["IDCardNo"].ToString2();
                                                            row["qyID"] = item["CorpCode"].ToString2();
                                                            row["ryzyzglxID"] = ryzyzglxid;
                                                            row["ryzyzglx"] = ryzyzglx;
                                                            row["DataState"] = 0;
                                                            row["tag"] = tag;
                                                            row["xgr"] = "定时服务";
                                                            row["xgrqsj"] = updateDate;
                                                            dataService.Submit_uepp_qyry(dt_qyry);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (item["Status"].ToString2() == "2")
                                                        {
                                                            dt_qyry.Rows[0].Delete();
                                                            dataService.Submit_uepp_qyry(dt_qyry);
                                                        }
                                                    }
                                                    #endregion
                                                }
                                                catch (Exception ex)
                                                {
                                                    row_SaveDataLog["SaveState"] = 0;
                                                    row_SaveDataLog["Msg"] = "出现异常，ryID：" + item["IDCardNo"].ToString2() + ",Exception:" + ex.Message;
                                                }
                                                dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                                            }
                                        }
                                    }
                                }
                            }
                        }


                        #endregion
                        //Public.WriteLog("获取" + tag + "标准员：");
                        #region 标准员
                        rygwlx = "0211";
                        ryzyzglxid = "93";
                        ryzyzglx = "标准员";
                        ryzslxid = "844";
                        ryzslx = "标准员上岗证";

                        bytes = newdataService.getPersonJobCert(userID, "320200", xzqdm, rygwlx, "0");

                        result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);

                        index = result.IndexOf("<ReturnInfo>");
                        if (index >= 0)
                        {
                            string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
                            if (!string.IsNullOrEmpty(returnResult))
                            {
                                ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
                                if (returnInfo.Status)
                                {

                                    index = result.IndexOf("<PersonJobCertInfo>");
                                    if (index >= 0)
                                    {
                                        string personJobCertInfoString = result.Substring(index, result.LastIndexOf("</PersonJobCertInfo>") - index + 20);
                                        string msg = "";
                                        DataTable dt_personJobCertInfo = helper.ConvertXMLToDataTable("<dataTable>" + personJobCertInfoString + "</dataTable>", out msg);
                                        if (string.IsNullOrEmpty(msg) && dt_personJobCertInfo != null && dt_personJobCertInfo.Rows.Count > 0)
                                        {
                                            foreach (DataRow item in dt_personJobCertInfo.Rows)
                                            {
                                                DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                                                row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_ryxx["ID"];
                                                row_SaveDataLog["DataXml"] = "";
                                                row_SaveDataLog["PKID"] = item["IDCardNo"].ToString2();
                                                row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                                                int rowIndex = -1;
                                                try
                                                {
                                                    string updateDate = item["UpdateDate"].ToString2();

                                                    #region 人员基本信息

                                                    DataTable dt_ryxx = dataService.Get_UEPP_Ryjbxx(item["IDCardNo"].ToString2());
                                                    if (dt_ryxx.Rows.Count == 0)
                                                    {
                                                        row = dt_ryxx.NewRow();
                                                        dt_ryxx.Rows.Add(row);
                                                        row["ryID"] = item["IDCardNo"].ToString2();
                                                    }
                                                    else
                                                    {
                                                        row = dt_ryxx.Rows[0];
                                                        if (!string.IsNullOrEmpty(row["xgrqsj"].ToString2()) && !string.IsNullOrEmpty(updateDate))
                                                            if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(updateDate).ToString("yyyy-MM-dd"))
                                                            {
                                                                continue;
                                                            }
                                                    }

                                                    if (string.IsNullOrEmpty(updateDate))
                                                        updateDate = DateTime.Now.ToString("yyyy-MM-dd");
                                                    row["tag"] = tag;
                                                    row["xm"] = item["PersonName"];
                                                    switch (item["IDCardType"].ToString2())
                                                    {
                                                        case "1":
                                                            row["zjlxID"] = "1";
                                                            row["zjlx"] = "身份证";
                                                            break;
                                                        case "2":
                                                            row["zjlxID"] = "3";
                                                            row["zjlx"] = "护照";
                                                            break;
                                                        case "3":
                                                            row["zjlxID"] = "2";
                                                            row["zjlx"] = "军官证";
                                                            break;
                                                        case "4":
                                                            row["zjlxID"] = "4";
                                                            row["zjlx"] = "台湾居民身份证";

                                                            break;
                                                        case "5":
                                                            row["zjlxID"] = "5";
                                                            row["zjlx"] = "香港永久性居民身份证";

                                                            break;
                                                        case "6":
                                                            row["zjlxID"] = "6";
                                                            row["zjlx"] = "警官证";

                                                            break;
                                                        case "其他":
                                                            row["zjlxID"] = "9";
                                                            row["zjlx"] = "其他";

                                                            break;
                                                    }

                                                    row["zjhm"] = item["IDCardNo"];
                                                    //row["xb"] = item["Sex"];
                                                    //if (!string.IsNullOrEmpty(item["Birthday"].ToString2()))
                                                    //    row["csrq"] = item["Birthday"];
                                                    row["xgrqsj"] = updateDate;
                                                    row["xgr"] = "定时服务";
                                                    row["DataState"] = 0;
                                                    row["AJ_EXISTINIDCARDS"] = "2";
                                                    row["AJ_IsRefuse"] = "0";
                                                    allCount_ryxx++;
                                                    if (!dataService.Submit_uepp_ryjbxx(dt_ryxx))
                                                    {
                                                        row_SaveDataLog["SaveState"] = 0;
                                                        row_SaveDataLog["Msg"] = "建设单位人员信息保存失败，ryID：" + item["IDCardNo"].ToString2();
                                                    }
                                                    else
                                                    {
                                                        success_count_ryxx++;
                                                        row_SaveDataLog["SaveState"] = 1;
                                                        row_SaveDataLog["Msg"] = "";
                                                    }
                                                    #endregion

                                                    #region 人员执业资格

                                                    DataTable dt_ryzyzg = dataService.Get_uepp_Ryzyzg(item["IDCardNo"].ToString2());
                                                    rowIndex = -1;
                                                    for (int i = 0; i < dt_ryzyzg.Rows.Count; i++)
                                                    {
                                                        if (dt_ryzyzg.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                                        {
                                                            rowIndex = i;
                                                            break;
                                                        }
                                                    }
                                                    if (rowIndex < 0)
                                                    {
                                                        row = dt_ryzyzg.NewRow();
                                                        dt_ryzyzg.Rows.Add(row);

                                                        row["ryID"] = item["IDCardNo"].ToString2();
                                                        row["ryzyzglxID"] = ryzyzglxid;
                                                        row["ryzyzglx"] = ryzyzglx;
                                                        row["balxID"] = 1;
                                                        row["balx"] = "长期备案";
                                                        row["DataState"] = 0;
                                                        row["tag"] = tag;
                                                        row["xgr"] = "定时服务";
                                                        row["xgrqsj"] = updateDate;
                                                        dataService.Submit_uepp_Ryzyzg(dt_ryzyzg);
                                                    }

                                                    #endregion

                                                    #region 人员专业明细

                                                    //if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                                    //{
                                                    //    DataTable dt_ryzymx = dataService.Get_uepp_Ryzymx(item["IDCardNo"].ToString2());
                                                    //    rowIndex = -1;
                                                    //    for (int i = 0; i < dt_ryzymx.Rows.Count; i++)
                                                    //    {
                                                    //        if (dt_ryzymx.Rows[i]["ryzyzglxID"].ToString2() == ryzyzglxid && dt_ryzymx.Rows[i]["zyzglb"].ToString2() == item["CertMajor"].ToString2().Trim())
                                                    //        {
                                                    //            rowIndex = i;
                                                    //            break;
                                                    //        }
                                                    //    }
                                                    //    if (rowIndex < 0)
                                                    //    {
                                                    //        row = dt_ryzymx.NewRow();
                                                    //        dt_ryzymx.Rows.Add(row);
                                                    //        row["ryID"] = item["IDCardNo"].ToString2();
                                                    //        row["ryzyzglxID"] = ryzyzglxid;
                                                    //        row["ryzyzglx"] = ryzyzglx;

                                                    //        if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                                    //        {
                                                    //            row["zyzglb"] = item["CertMajor"].ToString2().Trim();

                                                    //            SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                                    //            sp.Add("@CodeInfo", item["CertMajor"].ToString2().Trim());
                                                    //            sp.Add("@parentCode", ryzyzglxid);

                                                    //            DataTable dt_code = dataService.GetTable("select * from UEPP_Code where CodeType='人员资质类别' and ParentCodeType='人员执业资格类型' and ParentCode=@parentCode and CodeInfo=@CodeInfo", sp);
                                                    //            if (dt_code.Rows.Count == 0)
                                                    //            {
                                                    //                dt_code.Rows.Add(dt_code.NewRow());
                                                    //                dt_code.Rows[0]["ParentCodeType"] = "人员执业资格类型";
                                                    //                dt_code.Rows[0]["ParentCode"] = ryzyzglxid;
                                                    //                dt_code.Rows[0]["CodeType"] = "人员资质类别";
                                                    //                dt_code.Rows[0]["CodeInfo"] = item["CertMajor"].ToString2().Trim();
                                                    //                dt_code.Rows[0]["Code"] = dataService.Get_uepp_Code_NewCode("人员资质类别");
                                                    //                dt_code.Rows[0]["CodeDesc"] = "";
                                                    //                dt_code.Rows[0]["OrderID"] = dt_code.Rows[0]["Code"];
                                                    //                dataService.Submit_uepp_code(dt_code);

                                                    //            }
                                                    //            row["zyzglbID"] = dt_code.Rows[0]["Code"];
                                                    //        }


                                                    //        row["zzbz"] = "主项";
                                                    //        row["DataState"] = 0;
                                                    //        row["tag"] = tag;
                                                    //        row["xgr"] = "定时服务";
                                                    //        row["xgrqsj"] = updateDate;
                                                    //        dataService.Submit_uepp_Ryzymx(dt_ryzymx);
                                                    //    }
                                                    //}

                                                    #endregion

                                                    #region 人员证书基本信息
                                                    DataTable dt_ryzs = dataService.Get_uepp_Ryzs(item["IDCardNo"].ToString2());
                                                    rowIndex = -1;
                                                    for (int i = 0; i < dt_ryzs.Rows.Count; i++)
                                                    {
                                                        if (dt_ryzs.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                                        {
                                                            rowIndex = i;
                                                            break;
                                                        }
                                                    }
                                                    if (rowIndex < 0)
                                                    {

                                                        row = dt_ryzs.NewRow();
                                                        dt_ryzs.Rows.Add(row);
                                                        row["zsjlId"] = dataService.Get_uepp_RyzsNewID();
                                                        row["ryID"] = item["IDCardNo"].ToString2();
                                                        row["ryzyzglxID"] = ryzyzglxid;
                                                        row["ryzyzglx"] = ryzyzglx;
                                                        row["ryzslxID"] = ryzslxid;
                                                        row["ryzslx"] = ryzslx;


                                                    }
                                                    else
                                                    {
                                                        row = dt_ryzs.Rows[rowIndex];
                                                    }
                                                    row["sfzzz"] = 1;
                                                    row["zsbh"] = item["CertNo"].ToString2();
                                                    if (!string.IsNullOrEmpty(item["ValidDate"].ToString2()))
                                                        row["zsyxzrq"] = item["ValidDate"].ToString2();
                                                    row["fzdw"] = item["IssueOrgan"].ToString2();
                                                    if (!string.IsNullOrEmpty(item["IssueDate"].ToString2()))
                                                    {
                                                        row["fzrq"] = item["IssueDate"].ToString2();
                                                        row["zsyxqrq"] = item["IssueDate"].ToString2();
                                                    }
                                                    row["Status"] = item["Status"];
                                                    //if (dt_personJobCertInfo.Columns.Contains("QualIssueDate"))
                                                    //    row["QualIssueDate"] = item["QualIssueDate"];
                                                    //if (dt_personJobCertInfo.Columns.Contains("StampNo"))
                                                    //    row["StampNo"] = item["StampNo"];
                                                    //if (dt_personJobCertInfo.Columns.Contains("RegNo"))
                                                    //    row["RegNo"] = item["RegNo"];
                                                    row["DataState"] = 0;
                                                    row["tag"] = tag;
                                                    row["xgr"] = "定时服务";
                                                    row["xgrqsj"] = updateDate;
                                                    dataService.Submit_uepp_Ryzs(dt_ryzs);

                                                    #endregion

                                                    #region 企业人员关系表

                                                    DataTable dt_qyry = dataService.Get_uepp_Qyry(item["IDCardNo"].ToString2(), Public.ZzjgdmToStandard(item["CorpCode"].ToString2()), ryzyzglxid);
                                                    if (dt_qyry.Rows.Count == 0)
                                                    {
                                                        if (item["Status"].ToString2() != "2")
                                                        {
                                                            row = dt_qyry.NewRow();
                                                            dt_qyry.Rows.Add(row);
                                                            row["ryID"] = item["IDCardNo"].ToString2();
                                                            row["qyID"] = item["CorpCode"].ToString2();
                                                            row["ryzyzglxID"] = ryzyzglxid;
                                                            row["ryzyzglx"] = ryzyzglx;
                                                            row["DataState"] = 0;
                                                            row["tag"] = tag;
                                                            row["xgr"] = "定时服务";
                                                            row["xgrqsj"] = updateDate;
                                                            dataService.Submit_uepp_qyry(dt_qyry);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (item["Status"].ToString2() == "2")
                                                        {
                                                            dt_qyry.Rows[0].Delete();
                                                            dataService.Submit_uepp_qyry(dt_qyry);
                                                        }
                                                    }
                                                    #endregion
                                                }
                                                catch (Exception ex)
                                                {
                                                    row_SaveDataLog["SaveState"] = 0;
                                                    row_SaveDataLog["Msg"] = "出现异常，ryID：" + item["IDCardNo"].ToString2() + ",Exception:" + ex.Message;
                                                }
                                                dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                                            }
                                        }
                                    }
                                }
                            }

                        }

                        #endregion

                        //Public.WriteLog("获取" + tag + "技术工人：");
                        #region 技术工人
                        DataTable dt_jsgrlb = dataService.GetTable("select * from UEPP_Code where CodeType='人员资质类别' and ParentCodeType='人员执业资格类型' and ParentCode=18 order by OrderID", null);

                        ryzyzglxid = "18";
                        ryzyzglx = "技术工人";
                        ryzslxid = "88";
                        ryzslx = "技术工人上岗证";

                        foreach (DataRow row_zyzglb in dt_jsgrlb.Rows)
                        {
                            string zyzglxid = row_zyzglb["Code"].ToString2();
                            string zyzglx = row_zyzglb["CodeInfo"].ToString2();

                           // Public.WriteLog("获取" + tag + "技术工人-" + zyzglx + "：");

                            try
                            {
                                bytes = newdataService.getPersonJobCert(userID, "320200", xzqdm, zyzglxid, "0");

                                result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);

                                index = result.IndexOf("<ReturnInfo>");
                                if (index >= 0)
                                {
                                    string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
                                    if (!string.IsNullOrEmpty(returnResult))
                                    {
                                        ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
                                        if (returnInfo.Status)
                                        {
                                            index = result.IndexOf("<PersonJobCertInfo>");
                                            if (index >= 0)
                                            {
                                                string personJobCertInfoString = result.Substring(index, result.LastIndexOf("</PersonJobCertInfo>") - index + 20);
                                                string msg = "";
                                                DataTable dt_personJobCertInfo = helper.ConvertXMLToDataTable("<dataTable>" + personJobCertInfoString + "</dataTable>", out msg);
                                                if (string.IsNullOrEmpty(msg) && dt_personJobCertInfo != null && dt_personJobCertInfo.Rows.Count > 0)
                                                {

                                                    foreach (DataRow item in dt_personJobCertInfo.Rows)
                                                    {
                                                        DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                                                        row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_ryxx["ID"];
                                                        row_SaveDataLog["DataXml"] = "";
                                                        row_SaveDataLog["PKID"] = item["IDCardNo"].ToString2();
                                                        row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                                                        int rowIndex = -1;
                                                        try
                                                        {
                                                            string updateDate = item["UpdateDate"].ToString2();

                                                            #region 人员基本信息

                                                            DataTable dt_ryxx = dataService.Get_UEPP_Ryjbxx(item["IDCardNo"].ToString2());
                                                            if (dt_ryxx.Rows.Count == 0)
                                                            {
                                                                row = dt_ryxx.NewRow();
                                                                dt_ryxx.Rows.Add(row);
                                                                row["ryID"] = item["IDCardNo"].ToString2();
                                                            }
                                                            else
                                                            {
                                                                row = dt_ryxx.Rows[0];
                                                                if (!string.IsNullOrEmpty(row["xgrqsj"].ToString2()) && !string.IsNullOrEmpty(updateDate))
                                                                    if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(updateDate).ToString("yyyy-MM-dd"))
                                                                    {
                                                                        continue;
                                                                    }
                                                            }

                                                            if (string.IsNullOrEmpty(updateDate))
                                                                updateDate = DateTime.Now.ToString("yyyy-MM-dd");
                                                            row["tag"] = tag;
                                                            row["xm"] = item["PersonName"];
                                                            switch (item["IDCardType"].ToString2())
                                                            {
                                                                case "1":
                                                                    row["zjlxID"] = "1";
                                                                    row["zjlx"] = "身份证";
                                                                    break;
                                                                case "2":
                                                                    row["zjlxID"] = "3";
                                                                    row["zjlx"] = "护照";
                                                                    break;
                                                                case "3":
                                                                    row["zjlxID"] = "2";
                                                                    row["zjlx"] = "军官证";
                                                                    break;
                                                                case "4":
                                                                    row["zjlxID"] = "4";
                                                                    row["zjlx"] = "台湾居民身份证";

                                                                    break;
                                                                case "5":
                                                                    row["zjlxID"] = "5";
                                                                    row["zjlx"] = "香港永久性居民身份证";

                                                                    break;
                                                                case "6":
                                                                    row["zjlxID"] = "6";
                                                                    row["zjlx"] = "警官证";

                                                                    break;
                                                                case "其他":
                                                                    row["zjlxID"] = "9";
                                                                    row["zjlx"] = "其他";

                                                                    break;
                                                            }

                                                            row["zjhm"] = item["IDCardNo"];
                                                            //row["xb"] = item["Sex"];
                                                            //if (!string.IsNullOrEmpty(item["Birthday"].ToString2()))
                                                            //    row["csrq"] = item["Birthday"];
                                                            row["xgrqsj"] = updateDate;
                                                            row["xgr"] = "定时服务";
                                                            row["AJ_EXISTINIDCARDS"] = "2";
                                                            row["AJ_IsRefuse"] = "0";
                                                            row["DataState"] = 0;
                                                            allCount_ryxx++;
                                                            if (!dataService.Submit_uepp_ryjbxx(dt_ryxx))
                                                            {
                                                                row_SaveDataLog["SaveState"] = 0;
                                                                row_SaveDataLog["Msg"] = "建设单位人员信息保存失败，ryID：" + item["IDCardNo"].ToString2();
                                                            }
                                                            else
                                                            {
                                                                success_count_ryxx++;
                                                                row_SaveDataLog["SaveState"] = 1;
                                                                row_SaveDataLog["Msg"] = "";
                                                            }
                                                            #endregion

                                                            #region 人员执业资格

                                                            DataTable dt_ryzyzg = dataService.Get_uepp_Ryzyzg(item["IDCardNo"].ToString2());
                                                            rowIndex = -1;
                                                            for (int i = 0; i < dt_ryzyzg.Rows.Count; i++)
                                                            {
                                                                if (dt_ryzyzg.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                                                {
                                                                    rowIndex = i;
                                                                    break;
                                                                }
                                                            }
                                                            if (rowIndex < 0)
                                                            {
                                                                row = dt_ryzyzg.NewRow();
                                                                dt_ryzyzg.Rows.Add(row);

                                                                row["ryID"] = item["IDCardNo"].ToString2();
                                                                row["ryzyzglxID"] = ryzyzglxid;
                                                                row["ryzyzglx"] = ryzyzglx;
                                                                row["balxID"] = 1;
                                                                row["balx"] = "长期备案";
                                                                row["DataState"] = 0;
                                                                row["tag"] = tag;
                                                                row["xgr"] = "定时服务";
                                                                row["xgrqsj"] = updateDate;
                                                                dataService.Submit_uepp_Ryzyzg(dt_ryzyzg);
                                                            }

                                                            #endregion

                                                            #region 人员专业明细

                                                            //if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                                            //{
                                                            //    DataTable dt_ryzymx = dataService.Get_uepp_Ryzymx(item["IDCardNo"].ToString2());
                                                            //    rowIndex = -1;
                                                            //    for (int i = 0; i < dt_ryzymx.Rows.Count; i++)
                                                            //    {
                                                            //        if (dt_ryzymx.Rows[i]["ryzyzglxID"].ToString2() == ryzyzglxid
                                                            //            && dt_ryzymx.Rows[i]["zyzglb"].ToString2() == zyzglx)
                                                            //        {
                                                            //            rowIndex = i;
                                                            //            break;
                                                            //        }
                                                            //    }
                                                            //    if (rowIndex < 0)
                                                            //    {
                                                            //        row = dt_ryzymx.NewRow();
                                                            //        dt_ryzymx.Rows.Add(row);
                                                            //        row["ryID"] = item["IDCardNo"].ToString2();
                                                            //        row["ryzyzglxID"] = ryzyzglxid;
                                                            //        row["ryzyzglx"] = ryzyzglx;
                                                            //        row["ryzslxID"] = ryzslxid;
                                                            //        row["ryzslx"] = ryzslx;

                                                            //        row["zyzglbID"] = zyzglxid;
                                                            //        row["zyzglb"] = zyzglx;

                                                            //        //if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                                            //        //{
                                                            //        //    row["zyzglb"] = item["CertMajor"].ToString2().Trim();

                                                            //        //    SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                                            //        //    sp.Add("@CodeInfo", item["CertMajor"].ToString2().Trim());
                                                            //        //    sp.Add("@parentCode", ryzyzglxid);

                                                            //        //    DataTable dt_code = dataService.GetTable("select * from UEPP_Code where CodeType='人员资质类别' and ParentCodeType='人员执业资格类型' and ParentCode=@parentCode and CodeInfo=@CodeInfo", sp);
                                                            //        //    if (dt_code.Rows.Count == 0)
                                                            //        //    {
                                                            //        //        dt_code.Rows.Add(dt_code.NewRow());
                                                            //        //        dt_code.Rows[0]["ParentCodeType"] = "人员执业资格类型";
                                                            //        //        dt_code.Rows[0]["ParentCode"] = ryzyzglxid;
                                                            //        //        dt_code.Rows[0]["CodeType"] = "人员资质类别";
                                                            //        //        dt_code.Rows[0]["CodeInfo"] = item["CertMajor"].ToString2().Trim();
                                                            //        //        dt_code.Rows[0]["Code"] = dataService.Get_uepp_Code_NewCode("人员资质类别");
                                                            //        //        dt_code.Rows[0]["CodeDesc"] = "";
                                                            //        //        dt_code.Rows[0]["OrderID"] = dt_code.Rows[0]["Code"];
                                                            //        //        dataService.Submit_uepp_code(dt_code);

                                                            //        //    }
                                                            //        //    row["zyzglbID"] = dt_code.Rows[0]["Code"];
                                                            //        //}


                                                            //        row["zzbz"] = "主项";
                                                            //        row["DataState"] = 0;
                                                            //        row["tag"] = tag;
                                                            //        row["xgr"] = "定时服务";
                                                            //        row["xgrqsj"] = updateDate;
                                                            //        dataService.Submit_uepp_Ryzymx(dt_ryzymx);
                                                            //    }
                                                            //}

                                                            #endregion

                                                            #region 人员证书基本信息
                                                            DataTable dt_ryzs = dataService.Get_uepp_Ryzs(item["IDCardNo"].ToString2());
                                                            rowIndex = -1;
                                                            for (int i = 0; i < dt_ryzs.Rows.Count; i++)
                                                            {
                                                                if (dt_ryzs.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                                                {
                                                                    rowIndex = i;
                                                                    break;
                                                                }
                                                            }
                                                            if (rowIndex < 0)
                                                            {
                                                                if (item["Status"].ToString2() != "2")
                                                                {
                                                                    row = dt_ryzs.NewRow();
                                                                    dt_ryzs.Rows.Add(row);
                                                                    row["zsjlId"] = dataService.Get_uepp_RyzsNewID();
                                                                    row["ryID"] = item["IDCardNo"].ToString2();
                                                                    row["ryzyzglxID"] = ryzyzglxid;
                                                                    row["ryzyzglx"] = ryzyzglx;
                                                                    row["ryzslxID"] = ryzslxid;
                                                                    row["ryzslx"] = ryzslx;

                                                                }
                                                            }
                                                            else
                                                            {
                                                                row = dt_ryzs.Rows[rowIndex];

                                                            }
                                                            row["sfzzz"] = 1;
                                                            row["zsbh"] = item["CertNo"].ToString2();
                                                            if (!string.IsNullOrEmpty(item["ValidDate"].ToString2()))
                                                                row["zsyxzrq"] = item["ValidDate"].ToString2();
                                                            row["fzdw"] = item["IssueOrgan"].ToString2();
                                                            if (!string.IsNullOrEmpty(item["IssueDate"].ToString2()))
                                                            {
                                                                row["fzrq"] = item["IssueDate"].ToString2();
                                                                row["zsyxqrq"] = item["IssueDate"].ToString2();
                                                            }

                                                            row["Status"] = item["Status"];
                                                            //if (dt_personJobCertInfo.Columns.Contains("QualIssueDate"))
                                                            //    row["QualIssueDate"] = item["QualIssueDate"];
                                                            //if (dt_personJobCertInfo.Columns.Contains("StampNo"))
                                                            //    row["StampNo"] = item["StampNo"];
                                                            //if (dt_personJobCertInfo.Columns.Contains("RegNo"))
                                                            //    row["RegNo"] = item["RegNo"];

                                                            row["DataState"] = 0;
                                                            row["tag"] = tag;
                                                            row["xgr"] = "定时服务";
                                                            row["xgrqsj"] = updateDate;
                                                            dataService.Submit_uepp_Ryzs(dt_ryzs);
                                                            #endregion

                                                            #region 企业人员关系表

                                                            DataTable dt_qyry = dataService.Get_uepp_Qyry(item["IDCardNo"].ToString2(), Public.ZzjgdmToStandard(item["CorpCode"].ToString2()), ryzyzglxid);
                                                            if (dt_qyry.Rows.Count == 0)
                                                            {
                                                                if (item["Status"].ToString2() != "2")
                                                                {
                                                                    row = dt_qyry.NewRow();
                                                                    dt_qyry.Rows.Add(row);
                                                                    row["ryID"] = item["IDCardNo"].ToString2();
                                                                    row["qyID"] = item["CorpCode"].ToString2();
                                                                    row["ryzyzglxID"] = ryzyzglxid;
                                                                    row["ryzyzglx"] = ryzyzglx;
                                                                    row["DataState"] = 0;
                                                                    row["tag"] = tag;
                                                                    row["xgr"] = "定时服务";
                                                                    row["xgrqsj"] = updateDate;
                                                                    dataService.Submit_uepp_qyry(dt_qyry);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (item["Status"].ToString2() == "2")
                                                                {
                                                                    dt_qyry.Rows[0].Delete();
                                                                    dataService.Submit_uepp_qyry(dt_qyry);
                                                                }
                                                            }
                                                            #endregion
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            row_SaveDataLog["SaveState"] = 0;
                                                            row_SaveDataLog["Msg"] = "出现异常，ryID：" + item["IDCardNo"].ToString2() + ",Exception:" + ex.Message;
                                                        }
                                                        dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                Public.WriteLog("获取" + zyzglx + "出现异常，" + ",Exception:" + ex.Message);
                            }
                        }

                        #endregion

                        //Public.WriteLog("获取" + tag + "特种作业人员：");
                        #region 特种作业人员
                        DataTable dt_tzzyry = dataService.GetTable("select * from UEPP_Code where CodeType='人员资质类别' and ParentCodeType='人员执业资格类型' and ParentCode=17 order by OrderID", null);

                        ryzyzglxid = "17";
                        ryzyzglx = "特种作业人员";
                        ryzslxid = "87";
                        ryzslx = "特种作业人员上岗证";
                        foreach (DataRow row_zyzglb in dt_jsgrlb.Rows)
                        {
                            string zyzglxid = row_zyzglb["Code"].ToString2();
                            string zyzglx = row_zyzglb["CodeInfo"].ToString2();

                            //Public.WriteLog("获取" + tag + "技术工人-" + zyzglx + "：");

                            try
                            {
                                bytes = newdataService.getPersonJobCert(userID, "320200", xzqdm, zyzglxid, "0");

                                result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);

                                index = result.IndexOf("<ReturnInfo>");
                                if (index >= 0)
                                {
                                    string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
                                    if (!string.IsNullOrEmpty(returnResult))
                                    {
                                        ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
                                        if (returnInfo.Status)
                                        {
                                            index = result.IndexOf("<PersonJobCertInfo>");
                                            if (index >= 0)
                                            {
                                                string personJobCertInfoString = result.Substring(index, result.LastIndexOf("</PersonJobCertInfo>") - index + 20);
                                                string msg = "";
                                                DataTable dt_personJobCertInfo = helper.ConvertXMLToDataTable("<dataTable>" + personJobCertInfoString + "</dataTable>", out msg);
                                                if (string.IsNullOrEmpty(msg) && dt_personJobCertInfo != null && dt_personJobCertInfo.Rows.Count > 0)
                                                {
                                                    foreach (DataRow item in dt_personJobCertInfo.Rows)
                                                    {
                                                        DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                                                        row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_ryxx["ID"];
                                                        row_SaveDataLog["DataXml"] = "";
                                                        row_SaveDataLog["PKID"] = item["IDCardNo"].ToString2();
                                                        row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                                                        int rowIndex = -1;
                                                        try
                                                        {
                                                            string updateDate = item["UpdateDate"].ToString2();

                                                            #region 人员基本信息

                                                            DataTable dt_ryxx = dataService.Get_UEPP_Ryjbxx(item["IDCardNo"].ToString2());
                                                            if (dt_ryxx.Rows.Count == 0)
                                                            {
                                                                row = dt_ryxx.NewRow();
                                                                dt_ryxx.Rows.Add(row);
                                                                row["ryID"] = item["IDCardNo"].ToString2();
                                                            }
                                                            else
                                                            {
                                                                row = dt_ryxx.Rows[0];
                                                                if (!string.IsNullOrEmpty(row["xgrqsj"].ToString2()) && !string.IsNullOrEmpty(updateDate))
                                                                    if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(updateDate).ToString("yyyy-MM-dd"))
                                                                    {
                                                                        continue;
                                                                    }
                                                            }

                                                            if (string.IsNullOrEmpty(updateDate))
                                                                updateDate = DateTime.Now.ToString("yyyy-MM-dd");
                                                            row["tag"] = tag;
                                                            row["xm"] = item["PersonName"];
                                                            switch (item["IDCardType"].ToString2())
                                                            {
                                                                case "1":
                                                                    row["zjlxID"] = "1";
                                                                    row["zjlx"] = "身份证";
                                                                    break;
                                                                case "2":
                                                                    row["zjlxID"] = "3";
                                                                    row["zjlx"] = "护照";
                                                                    break;
                                                                case "3":
                                                                    row["zjlxID"] = "2";
                                                                    row["zjlx"] = "军官证";
                                                                    break;
                                                                case "4":
                                                                    row["zjlxID"] = "4";
                                                                    row["zjlx"] = "台湾居民身份证";

                                                                    break;
                                                                case "5":
                                                                    row["zjlxID"] = "5";
                                                                    row["zjlx"] = "香港永久性居民身份证";

                                                                    break;
                                                                case "6":
                                                                    row["zjlxID"] = "6";
                                                                    row["zjlx"] = "警官证";

                                                                    break;
                                                                case "其他":
                                                                    row["zjlxID"] = "9";
                                                                    row["zjlx"] = "其他";

                                                                    break;
                                                            }

                                                            row["zjhm"] = item["IDCardNo"];
                                                            //row["xb"] = item["Sex"];
                                                            //if (!string.IsNullOrEmpty(item["Birthday"].ToString2()))
                                                            //    row["csrq"] = item["Birthday"];
                                                            row["xgrqsj"] = updateDate;
                                                            row["xgr"] = "定时服务";
                                                            row["AJ_EXISTINIDCARDS"] = "2";
                                                            row["AJ_IsRefuse"] = "0";


                                                            row["DataState"] = 0;
                                                            allCount_ryxx++;

                                                            if (!dataService.Submit_uepp_ryjbxx(dt_ryxx))
                                                            {
                                                                row_SaveDataLog["SaveState"] = 0;
                                                                row_SaveDataLog["Msg"] = "建设单位人员信息保存失败，ryID：" + item["IDCardNo"].ToString2();
                                                            }
                                                            else
                                                            {
                                                                success_count_ryxx++;
                                                                row_SaveDataLog["SaveState"] = 1;
                                                                row_SaveDataLog["Msg"] = "";
                                                            }

                                                            #endregion

                                                            #region 人员执业资格

                                                            DataTable dt_ryzyzg = dataService.Get_uepp_Ryzyzg(item["IDCardNo"].ToString2());
                                                            rowIndex = -1;
                                                            for (int i = 0; i < dt_ryzyzg.Rows.Count; i++)
                                                            {
                                                                if (dt_ryzyzg.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                                                {
                                                                    rowIndex = i;
                                                                    break;
                                                                }
                                                            }
                                                            if (rowIndex < 0)
                                                            {
                                                                row = dt_ryzyzg.NewRow();
                                                                dt_ryzyzg.Rows.Add(row);

                                                                row["ryID"] = item["IDCardNo"].ToString2();
                                                                row["ryzyzglxID"] = ryzyzglxid;
                                                                row["ryzyzglx"] = ryzyzglx;
                                                                row["balxID"] = 1;
                                                                row["balx"] = "长期备案";
                                                                row["DataState"] = 0;
                                                                row["tag"] = tag;
                                                                row["xgr"] = "定时服务";
                                                                row["xgrqsj"] = updateDate;
                                                                dataService.Submit_uepp_Ryzyzg(dt_ryzyzg);
                                                            }

                                                            #endregion

                                                            #region 人员专业明细

                                                            //if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                                            //{
                                                            //    DataTable dt_ryzymx = dataService.Get_uepp_Ryzymx(item["IDCardNo"].ToString2());
                                                            //    rowIndex = -1;
                                                            //    for (int i = 0; i < dt_ryzymx.Rows.Count; i++)
                                                            //    {
                                                            //        if (dt_ryzymx.Rows[i]["ryzyzglxID"].ToString2() == ryzyzglxid
                                                            //            && dt_ryzymx.Rows[i]["zyzglb"].ToString2() == zyzglx)
                                                            //        {
                                                            //            rowIndex = i;
                                                            //            break;
                                                            //        }
                                                            //    }
                                                            //    if (rowIndex < 0)
                                                            //    {
                                                            //        row = dt_ryzymx.NewRow();
                                                            //        dt_ryzymx.Rows.Add(row);
                                                            //        row["ryID"] = item["IDCardNo"].ToString2();
                                                            //        row["ryzyzglxID"] = ryzyzglxid;
                                                            //        row["ryzyzglx"] = ryzyzglx;
                                                            //        row["ryzslxID"] = ryzslxid;
                                                            //        row["ryzslx"] = ryzslx;

                                                            //        row["zyzglbID"] = zyzglxid;
                                                            //        row["zyzglb"] = zyzglx;

                                                            //        //if (!string.IsNullOrEmpty(item["CertMajor"].ToString2().Trim()))
                                                            //        //{
                                                            //        //    row["zyzglb"] = item["CertMajor"].ToString2().Trim();

                                                            //        //    SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                                                            //        //    sp.Add("@CodeInfo", item["CertMajor"].ToString2().Trim());
                                                            //        //    sp.Add("@parentCode", ryzyzglxid);

                                                            //        //    DataTable dt_code = dataService.GetTable("select * from UEPP_Code where CodeType='人员资质类别' and ParentCodeType='人员执业资格类型' and ParentCode=@parentCode and CodeInfo=@CodeInfo", sp);
                                                            //        //    if (dt_code.Rows.Count == 0)
                                                            //        //    {
                                                            //        //        dt_code.Rows.Add(dt_code.NewRow());
                                                            //        //        dt_code.Rows[0]["ParentCodeType"] = "人员执业资格类型";
                                                            //        //        dt_code.Rows[0]["ParentCode"] = ryzyzglxid;
                                                            //        //        dt_code.Rows[0]["CodeType"] = "人员资质类别";
                                                            //        //        dt_code.Rows[0]["CodeInfo"] = item["CertMajor"].ToString2().Trim();
                                                            //        //        dt_code.Rows[0]["Code"] = dataService.Get_uepp_Code_NewCode("人员资质类别");
                                                            //        //        dt_code.Rows[0]["CodeDesc"] = "";
                                                            //        //        dt_code.Rows[0]["OrderID"] = dt_code.Rows[0]["Code"];
                                                            //        //        dataService.Submit_uepp_code(dt_code);

                                                            //        //    }
                                                            //        //    row["zyzglbID"] = dt_code.Rows[0]["Code"];
                                                            //        //}


                                                            //        row["zzbz"] = "主项";
                                                            //        row["DataState"] = 0;
                                                            //        row["tag"] = tag;
                                                            //        row["xgr"] = "定时服务";
                                                            //        row["xgrqsj"] = updateDate;
                                                            //        dataService.Submit_uepp_Ryzymx(dt_ryzymx);
                                                            //    }
                                                            //}

                                                            #endregion

                                                            #region 人员证书基本信息
                                                            DataTable dt_ryzs = dataService.Get_uepp_Ryzs(item["IDCardNo"].ToString2());
                                                            rowIndex = -1;
                                                            for (int i = 0; i < dt_ryzs.Rows.Count; i++)
                                                            {
                                                                if (dt_ryzs.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxid)
                                                                {
                                                                    rowIndex = i;
                                                                    break;
                                                                }
                                                            }
                                                            if (rowIndex < 0)
                                                            {

                                                                row = dt_ryzs.NewRow();
                                                                dt_ryzs.Rows.Add(row);
                                                                row["zsjlId"] = dataService.Get_uepp_RyzsNewID();
                                                                row["ryID"] = item["IDCardNo"].ToString2();
                                                                row["ryzyzglxID"] = ryzyzglxid;
                                                                row["ryzyzglx"] = ryzyzglx;
                                                                row["ryzslxID"] = ryzslxid;
                                                                row["ryzslx"] = ryzslx;


                                                            }
                                                            else
                                                            {
                                                                row = dt_ryzs.Rows[rowIndex];
                                                            }
                                                            row["sfzzz"] = 1;
                                                            row["zsbh"] = item["CertNo"].ToString2();
                                                            if (!string.IsNullOrEmpty(item["ValidDate"].ToString2()))
                                                                row["zsyxzrq"] = item["ValidDate"].ToString2();
                                                            row["fzdw"] = item["IssueOrgan"].ToString2();
                                                            if (!string.IsNullOrEmpty(item["IssueDate"].ToString2()))
                                                            {
                                                                row["fzrq"] = item["IssueDate"].ToString2();
                                                                row["zsyxqrq"] = item["IssueDate"].ToString2();
                                                            }
                                                            row["Status"] = item["Status"];
                                                            //if (dt_personJobCertInfo.Columns.Contains("QualIssueDate"))
                                                            //    row["QualIssueDate"] = item["QualIssueDate"];
                                                            //if (dt_personJobCertInfo.Columns.Contains("StampNo"))
                                                            //    row["StampNo"] = item["StampNo"];
                                                            //if (dt_personJobCertInfo.Columns.Contains("RegNo"))
                                                            //    row["RegNo"] = item["RegNo"];

                                                            row["DataState"] = 0;
                                                            row["tag"] = tag;
                                                            row["xgr"] = "定时服务";
                                                            row["xgrqsj"] = updateDate;
                                                            dataService.Submit_uepp_Ryzs(dt_ryzs);
                                                            #endregion

                                                            #region 企业人员关系表

                                                            DataTable dt_qyry = dataService.Get_uepp_Qyry(item["IDCardNo"].ToString2(), Public.ZzjgdmToStandard(item["CorpCode"].ToString2()), ryzyzglxid);
                                                            if (dt_qyry.Rows.Count == 0)
                                                            {
                                                                if (item["Status"].ToString2() != "2")
                                                                {
                                                                    row = dt_qyry.NewRow();
                                                                    dt_qyry.Rows.Add(row);
                                                                    row["ryID"] = item["IDCardNo"].ToString2();
                                                                    row["qyID"] = item["CorpCode"].ToString2();
                                                                    row["ryzyzglxID"] = ryzyzglxid;
                                                                    row["ryzyzglx"] = ryzyzglx;
                                                                    row["DataState"] = 0;
                                                                    row["tag"] = tag;
                                                                    row["xgr"] = "定时服务";
                                                                    row["xgrqsj"] = updateDate;
                                                                    dataService.Submit_uepp_qyry(dt_qyry);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (item["Status"].ToString2() == "2")
                                                                {
                                                                    dt_qyry.Rows[0].Delete();
                                                                    dataService.Submit_uepp_qyry(dt_qyry);
                                                                }
                                                            }
                                                            #endregion
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            row_SaveDataLog["SaveState"] = 0;
                                                            row_SaveDataLog["Msg"] = "出现异常，ryID：" + item["IDCardNo"].ToString2() + ",Exception:" + ex.Message;
                                                        }
                                                        dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                Public.WriteLog("获取" + zyzglx + "出现异常，" + ",Exception:" + ex.Message);
                            }
                        }

                        #endregion

                    }

                    if (dt_SaveDataLog.Rows.Count > 0)
                        dataService.Submit_SaveDataLog(dt_SaveDataLog);
                    row_DataJkDataDetail_ryxx["allCount"] = allCount_ryxx;
                    row_DataJkDataDetail_ryxx["successCount"] = success_count_ryxx;
                    row_DataJkDataDetail_ryxx["IsOk"] = 1;
                    row_DataJkDataDetail_ryxx["ErrorMsg"] = "";

                    if (dt_DataJkDataDetail_ryxx.Rows.Count > 0)
                        dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_ryxx);


                    DateTime endTime = DateTime.Now;
                    TimeSpan span = compareDateTime(beginTime, endTime);
                    Public.WriteLog(string.Format("结束YourTask_PullDataFromSxxzx_Ryxx_Zygwgl任务:{0}，历时：{1}时{2}分{3}秒", endTime.ToString("yyyy-MM-dd HH:mm:ss"), span.Hours, span.Minutes, span.Seconds));

                    dataService.ExecuteSql("exec proc_DealZyrySameData");//2017-1-9新增；处理执业人员重复数据
                    Public.WriteLog(string.Format("完成人员重复数据处理:{0}",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                }
                catch (Exception ex)
                {
                    row_DataJkDataDetail_ryxx["allCount"] = allCount_ryxx;
                    row_DataJkDataDetail_ryxx["successCount"] = success_count_ryxx;
                    row_DataJkDataDetail_ryxx["IsOk"] = 0;
                    row_DataJkDataDetail_ryxx["ErrorMsg"] = ex.Message;

                    if (dt_DataJkDataDetail_ryxx.Rows.Count > 0)
                        dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_ryxx);
                }
            }
            catch (Exception ex)
            {
                Public.WriteLog("Exception:" + ex.Message);
            }
        }
        /// <summary>
        /// 获取省勘察设计系统勘察设计单位信息
        /// </summary>
        /// <param name="DataJkLogID"></param>
        void YourTask_PullDataFromSKcsj_qyxx()
        {
            string apiMessage = string.Empty;// 2016.10.21
            //string methodMessage = string.Empty;
            DataTable dtapizb = dataService.Get_API_zb_apiFlow("28");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                //往数据监控日志表添加一条记录
                DataTable dt_DataJkLog = dataService.GetSchema_DataJkLog();
                DataRow row_DataJkLog = dt_DataJkLog.NewRow();
                dt_DataJkLog.Rows.Add(row_DataJkLog);
                row_DataJkLog["ID"] = dataService.Get_DataJkLogNewID();
                row_DataJkLog["DataFlow"] = DataFlow.省勘察设计系统到无锡数据中心.ToInt32();
                row_DataJkLog["DataFlowName"] = DataFlow.省勘察设计系统到无锡数据中心.ToString();
                row_DataJkLog["ServiceUrl"] = "";
                row_DataJkLog["csTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dataService.Submit_DataJkLog(dt_DataJkLog);

                int allCount_qyxx = 0, successCount_qyxx = 0;
                try
                {
                    string tag = Tag.省勘察设计系统.ToString();

                    //往数据监控日志表项添加一条记录
                    DataTable dt_DataJkDataDetail_qyxx = dataService.GetSchema_DataJkDataDetail();
                    DataRow row_DataJkDataDetail_qyxx = dt_DataJkDataDetail_qyxx.NewRow();
                    dt_DataJkDataDetail_qyxx.Rows.Add(row_DataJkDataDetail_qyxx);

                    row_DataJkDataDetail_qyxx["ID"] = dataService.Get_DataJkDataDetailNewID().ToInt64();
                    row_DataJkDataDetail_qyxx["DataJkLogID"] = row_DataJkLog["ID"];
                    row_DataJkDataDetail_qyxx["tableName"] = "UEPP_Qyjbxx";
                    row_DataJkDataDetail_qyxx["MethodName"] = "直接从省勘察设计数据库读取";
                    row_DataJkDataDetail_qyxx["bz"] = "从省勘察设计系统拉取勘察设计企业信息";

                    try
                    {
                        DataTable dt_SaveDataLog = dataService.GetSchema_SaveDataLog();

                        DataTable dt = dataService.Get_Enterprise_Tab_Skcsj();

                        Public.WriteLog("获取省勘察设计系统勘察设计单位信息:" + dt.Rows.Count);

                        foreach (DataRow row in dt.Rows)
                        {
                            string qyID = row["ZZJGDM"].ToString2();
                            if (string.IsNullOrEmpty(qyID))
                            {
                                qyID = Public.ShxydmToZzjgdm(row["YYZZZCH"].ToString2());
                            }else if (qyID.Length == 9)
                            {
                                qyID = Public.ZzjgdmToStandard(qyID);
                            }
                            if (string.IsNullOrEmpty(qyID))
                            {
                                continue;
                            }

                            DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                            row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_qyxx["ID"];
                            row_SaveDataLog["DataXml"] = "";
                            row_SaveDataLog["PKID"] = qyID;
                            row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                            try
                            {
                                #region 企业信息

                                DataTable dt_uepp_kcsjqy = dataService.Get_uepp_Qyjbxx(qyID);
                                DataRow temp;
                                if (dt_uepp_kcsjqy.Rows.Count == 0)
                                {
                                    temp = dt_uepp_kcsjqy.NewRow();
                                    dt_uepp_kcsjqy.Rows.Add(temp);
                                    temp["qyID"] = qyID;
                                    temp["tag"] = tag;
                                }
                                else
                                {
                                    temp = dt_uepp_kcsjqy.Rows[0];
                                    qyID = temp["qyID"].ToString2();

                                    if (temp["tag"].ToString2().IndexOf(tag) < 0)
                                        continue;

                                    if (dt_uepp_kcsjqy.Rows[0]["xgrqsj"].ToString2() == row["OperateDate"].ToString2())
                                    {
                                        continue;
                                    }
                                }
                                allCount_qyxx++;

                                #region 勘察设计企业从事业务类型
                                int isKc = row["IsKc"].ToInt32(0), isSj = row["IsSj"].ToInt32(0);
                                DataTable dt_uepp_kcsj_csywlx = dataService.Get_uepp_qycsyw_kcsj(qyID);
                                DataRow tempRow;
                                int hasKcFlag = 0, hasSjFlag = 0;
                                for (int i = 0; i < dt_uepp_kcsj_csywlx.Rows.Count; i++)
                                {
                                    if (dt_uepp_kcsj_csywlx.Rows[i]["csywlxID"].ToString2().Equals("5"))
                                    {
                                        hasKcFlag = 1;
                                    }
                                    else
                                        if (dt_uepp_kcsj_csywlx.Rows[i]["csywlxID"].ToString2() == "6")
                                        {
                                            hasSjFlag = 1;
                                        }
                                }
                                int flag = 0;
                                if (isKc > 0 && hasKcFlag == 0)
                                {
                                    tempRow = dt_uepp_kcsj_csywlx.NewRow();
                                    dt_uepp_kcsj_csywlx.Rows.Add(tempRow);
                                    tempRow["qyID"] = qyID;
                                    tempRow["csywlxID"] = "5";
                                    tempRow["csywlx"] = "工程勘察";
                                    tempRow["balxID"] = "0";
                                    tempRow["balx"] = "无";
                                    tempRow["DataState"] = "0";
                                    tempRow["tag"] = tag;

                                    tempRow["xgrqsj"] = string.IsNullOrEmpty(row["OperateDate"].ToString2()) ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : row["OperateDate"].ToString2();
                                    tempRow["xgr"] = "";
                                    flag = 1;
                                }

                                if (isSj > 0 && hasSjFlag == 0)
                                {
                                    tempRow = dt_uepp_kcsj_csywlx.NewRow();
                                    dt_uepp_kcsj_csywlx.Rows.Add(tempRow);
                                    tempRow["qyID"] = qyID;
                                    tempRow["csywlxID"] = "6";
                                    tempRow["csywlx"] = "工程设计";
                                    tempRow["balxID"] = "0";
                                    tempRow["balx"] = "无";
                                    tempRow["DataState"] = "0";
                                    tempRow["tag"] = tag;
                                    tempRow["xgrqsj"] = string.IsNullOrEmpty(row["OperateDate"].ToString2()) ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : row["OperateDate"].ToString2();
                                    tempRow["xgr"] = "";
                                    flag = 1;
                                }

                                if (dt_uepp_kcsj_csywlx.Rows.Count > 0 && flag > 0)
                                {
                                    if (!dataService.Submit_uepp_qycsyw(dt_uepp_kcsj_csywlx))
                                    {
                                        Public.WriteLog("更新设计企业业务类型时失败！");
                                        continue;
                                    }
                                }

                                #endregion

                                temp["qymc"] = row["DWName"];
                                temp["zzjgdm"] = qyID;
                                temp["yyzzzch"] = row["YYZZZCH"];
                                temp["tyshxydm"] = row["YYZZZCH"];
                                
                                if (row["GSZCSZD_XZQHDM"] != DBNull.Value && !string.IsNullOrEmpty(row["GSZCSZD_XZQHDM"].ToString2()))
                                {
                                    temp["ProvinceID"] = row["GSZCSZD_XZQHDM"].ToString2().Substring(0, 2) + "0000";
                                    string Province = dataService.DB.ExeSqlForString("select codeInfo from UEPP_Code where codeType='城市地区' and code='" + temp["ProvinceID"] + "'", null);
                                    temp["Province"] = Province;

                                    temp["CityID"] = row["GSZCSZD_XZQHDM"].ToString2().Substring(0, 4) + "00";
                                    string City = dataService.DB.ExeSqlForString("select codeInfo from UEPP_Code where codeType='城市地区' and code='" + temp["CityID"] + "'", null);
                                    temp["City"] = City;

                                    temp["CountyID"] = row["GSZCSZD_XZQHDM"].ToString2().Length > 6 ? row["GSZCSZD_XZQHDM"].ToString2().Substring(0, 6) : row["GSZCSZD_XZQHDM"].ToString2();
                                    string County = dataService.DB.ExeSqlForString("select codeInfo from UEPP_Code where codeType='城市地区' and code='" + temp["CountyID"] + "'", null);
                                    temp["County"] = County;
                                }
                                temp["zcdd"] = row["GSZCDZ"];
                                temp["xxdd"] = row["XXAddress"];

                                temp["zczb"] = row["ZCZJ"];
                                if (!string.IsNullOrEmpty(row["CLDate"].ToString2()))
                                    temp["clrq"] = row["CLDate"].ToString2();
                                temp["yzbm"] = row["PostNo"];

                                temp["cz"] = row["ChuanZheng"];
                                temp["lxr"] = row["LXR_Name"];
                                temp["lxdh"] = row["LXR_LXDH"];

                                temp["fddbr"] = row["FRDBDW_FZRName"].ToString2();
                                temp["jsfzr"] = row["JSFZ"];

                                temp["xgrqsj"] = string.IsNullOrEmpty(row["OperateDate"].ToString2()) ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : row["OperateDate"].ToString2();
                                temp["xgr"] = "";
                                temp["DataState"] = 0;

                                if (dataService.Submit_uepp_qyjbxx(dt_uepp_kcsjqy))
                                {
                                    successCount_qyxx++;
                                    row_SaveDataLog["SaveState"] = 1;
                                    row_SaveDataLog["Msg"] = "";

                                }
                                else
                                {
                                    row_SaveDataLog["SaveState"] = 0;
                                    row_SaveDataLog["Msg"] = "企业信息保存失败！";
                                    dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                                    continue;
                                }
                                #endregion

                                #region 勘察设计企业资质信息


                                DataTable dt_uepp_qyzzmx_kcsj = dataService.Get_UEPP_Qyzzmx_kcsj(qyID);
                                DataTable dt_kcsjqy_qyzz = dataService.Get_Enterprise_ZZ_Tab_Skcsj(row["RowGuid"].ToString2());

                                foreach (DataRow row_qyzz in dt_kcsjqy_qyzz.Rows)
                                {
                                    int isExists = -1;
                                    string zzlb = "", zzlbId = "";
                                    string zzxlId = "", zzxl = "";
                                    string zzName = row_qyzz["ZZName"].ToString2();
                                    string zsbh = row_qyzz["ZZZSNO"].ToString2();
                                    string zsjb = row_qyzz["XKJG_JB"].ToString2();

                                    int kcIndex = zzName.IndexOf("勘察");
                                    int sjIndex = zzName.IndexOf("设计");

                                    int zzdjIndex = zzName.IndexOf('级');

                                    int zyIndex = zzName.IndexOf("专业");
                                    int lwIndex = zzName.IndexOf("劳务");
                                    int zhIndex = zzName.IndexOf("综合");

                                    int hyIndex = zzName.IndexOf("行业");
                                    int zxIndex = zzName.IndexOf("专项");
                                    int swsIndex = zzName.IndexOf("事务所");

                                    #region 企业资质证书
                                    string zslx = "", zslxID = "";

                                    if (!string.IsNullOrEmpty(zsbh))
                                    {
                                        DataTable dt_qyzzzs;
                                        if (kcIndex >= 0)
                                        {
                                            if (zsjb.Equals("省级"))
                                            {
                                                dt_qyzzzs = dataService.Get_UEPP_Qyzzzs_kcsj(qyID, "5", "51", zsbh);
                                                if (dt_qyzzzs.Rows.Count == 0)
                                                {

                                                    dt_qyzzzs.Rows.Add(dt_qyzzzs.NewRow());
                                                    dt_qyzzzs.Rows[0]["zsjlId"] = dataService.Get_uepp_qyzs_NewID();
                                                    dt_qyzzzs.Rows[0]["qyID"] = qyID;
                                                    dt_qyzzzs.Rows[0]["csywlx"] = "工程勘察";
                                                    dt_qyzzzs.Rows[0]["csywlxID"] = "5";

                                                    dt_qyzzzs.Rows[0]["zslxID"] = "51";
                                                    dt_qyzzzs.Rows[0]["zslx"] = "省工程勘察资质证";
                                                    dt_qyzzzs.Rows[0]["sfzzz"] = "1";
                                                }
                                                dt_qyzzzs.Rows[0]["zsbh"] = zsbh;
                                                dt_qyzzzs.Rows[0]["fzrq"] = row_qyzz["XKJG_Date"];
                                                dt_qyzzzs.Rows[0]["zsyxqrq"] = row_qyzz["ZS_YXQDate"];
                                                dt_qyzzzs.Rows[0]["zsyxzrq"] = row_qyzz["ZS_YXQEndDate"];
                                                dt_qyzzzs.Rows[0]["fzdw"] = row_qyzz["XKJG_Name"];

                                                dt_qyzzzs.Rows[0]["DataState"] = 0;
                                                dt_qyzzzs.Rows[0]["tag"] = tag;
                                                dt_qyzzzs.Rows[0]["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                                dataService.Submit_uepp_qyzzzs(dt_qyzzzs);
                                            }
                                            else
                                            {
                                                dt_qyzzzs = dataService.Get_UEPP_Qyzzzs_kcsj(qyID, "5", "50", zsbh);
                                                if (dt_qyzzzs.Rows.Count == 0)
                                                {
                                                    dt_qyzzzs.Rows.Add(dt_qyzzzs.NewRow());
                                                    dt_qyzzzs.Rows[0]["zsjlId"] = dataService.Get_uepp_qyzs_NewID();
                                                    dt_qyzzzs.Rows[0]["qyID"] = qyID;
                                                    dt_qyzzzs.Rows[0]["csywlx"] = "工程勘察";
                                                    dt_qyzzzs.Rows[0]["csywlxID"] = "5";

                                                    dt_qyzzzs.Rows[0]["zslxID"] = "50";
                                                    dt_qyzzzs.Rows[0]["zslx"] = "部工程勘察资质证";
                                                    dt_qyzzzs.Rows[0]["sfzzz"] = "1";
                                                }
                                                dt_qyzzzs.Rows[0]["zsbh"] = zsbh;
                                                dt_qyzzzs.Rows[0]["fzrq"] = row_qyzz["XKJG_Date"];
                                                dt_qyzzzs.Rows[0]["zsyxqrq"] = row_qyzz["ZS_YXQDate"];
                                                dt_qyzzzs.Rows[0]["zsyxzrq"] = row_qyzz["ZS_YXQEndDate"];
                                                dt_qyzzzs.Rows[0]["fzdw"] = row_qyzz["XKJG_Name"];

                                                dt_qyzzzs.Rows[0]["DataState"] = 0;
                                                dt_qyzzzs.Rows[0]["tag"] = tag;
                                                dt_qyzzzs.Rows[0]["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                                dataService.Submit_uepp_qyzzzs(dt_qyzzzs);
                                            }
                                        }
                                        else
                                        {
                                            if (zsjb.Equals("省级"))
                                            {
                                                dt_qyzzzs = dataService.Get_UEPP_Qyzzzs_kcsj(qyID, "6", "61", zsbh);

                                                if (dt_qyzzzs.Rows.Count == 0)
                                                {
                                                    dt_qyzzzs.Rows.Add(dt_qyzzzs.NewRow());
                                                    dt_qyzzzs.Rows[0]["zsjlId"] = dataService.Get_uepp_qyzs_NewID();
                                                    dt_qyzzzs.Rows[0]["qyID"] = qyID;
                                                    dt_qyzzzs.Rows[0]["csywlx"] = "工程设计";
                                                    dt_qyzzzs.Rows[0]["csywlxID"] = "6";

                                                    dt_qyzzzs.Rows[0]["zslxID"] = "61";
                                                    dt_qyzzzs.Rows[0]["zslx"] = "省工程设计资质证";
                                                    dt_qyzzzs.Rows[0]["sfzzz"] = "1";
                                                }
                                                dt_qyzzzs.Rows[0]["zsbh"] = zsbh;
                                                dt_qyzzzs.Rows[0]["fzrq"] = row_qyzz["XKJG_Date"];
                                                dt_qyzzzs.Rows[0]["zsyxqrq"] = row_qyzz["ZS_YXQDate"];
                                                dt_qyzzzs.Rows[0]["zsyxzrq"] = row_qyzz["ZS_YXQEndDate"];
                                                dt_qyzzzs.Rows[0]["fzdw"] = row_qyzz["XKJG_Name"];

                                                dt_qyzzzs.Rows[0]["DataState"] = 0;
                                                dt_qyzzzs.Rows[0]["tag"] = tag;
                                                dt_qyzzzs.Rows[0]["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                                dataService.Submit_uepp_qyzzzs(dt_qyzzzs);
                                            }
                                            else
                                            {
                                                dt_qyzzzs = dataService.Get_UEPP_Qyzzzs_kcsj(qyID, "6", "60", zsbh);

                                                if (dt_qyzzzs.Rows.Count == 0)
                                                {
                                                    dt_qyzzzs.Rows.Add(dt_qyzzzs.NewRow());
                                                    dt_qyzzzs.Rows[0]["zsjlId"] = dataService.Get_uepp_qyzs_NewID();
                                                    dt_qyzzzs.Rows[0]["qyID"] = qyID;
                                                    dt_qyzzzs.Rows[0]["csywlx"] = "工程设计";
                                                    dt_qyzzzs.Rows[0]["csywlxID"] = "6";

                                                    dt_qyzzzs.Rows[0]["zslxID"] = "60";
                                                    dt_qyzzzs.Rows[0]["zslx"] = "部工程设计资质证";
                                                    dt_qyzzzs.Rows[0]["sfzzz"] = "1";
                                                }
                                                dt_qyzzzs.Rows[0]["zsbh"] = zsbh;
                                                dt_qyzzzs.Rows[0]["fzrq"] = row_qyzz["XKJG_Date"];
                                                dt_qyzzzs.Rows[0]["zsyxqrq"] = row_qyzz["ZS_YXQDate"];
                                                dt_qyzzzs.Rows[0]["zsyxzrq"] = row_qyzz["ZS_YXQEndDate"];
                                                dt_qyzzzs.Rows[0]["fzdw"] = row_qyzz["XKJG_Name"];

                                                dt_qyzzzs.Rows[0]["DataState"] = 0;
                                                dt_qyzzzs.Rows[0]["tag"] = tag;
                                                dt_qyzzzs.Rows[0]["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                                dataService.Submit_uepp_qyzzzs(dt_qyzzzs);
                                            }

                                        }
                                        zslx = dt_qyzzzs.Rows[0]["zslx"].ToString2();
                                        zslxID = dt_qyzzzs.Rows[0]["zslxID"].ToString2();

                                    }

                                    #endregion

                                    #region 企业资质明细
                                    if (kcIndex >= 0)
                                    {
                                        if (zyIndex >= 0)
                                        {

                                            zzxlId = "10";
                                            zzxl = "专业类";

                                            if (zzName.LastIndexOf("-") - zyIndex - 2 > 0)
                                                zzlb = zzName.Substring(zyIndex + 3, zzName.LastIndexOf("-") - zyIndex - 2).Trim(new char[] { ' ', '-' });
                                        }
                                        else
                                            if (lwIndex >= 0)
                                            {
                                                zzxlId = "11";
                                                zzxl = "劳务类";
                                                if (zzName.LastIndexOf("-") - lwIndex - 2 > 0)
                                                    zzlb = zzName.Substring(lwIndex + 3, zzName.LastIndexOf("-") - lwIndex - 2).Trim(new char[] { ' ', '-' });
                                            }
                                            else if (zhIndex >= 0)
                                            {
                                                zzxlId = "9";
                                                zzxl = "综合类";
                                                if (zzName.LastIndexOf("-") - zhIndex - 2 > 0)
                                                    zzlb = zzName.Substring(zhIndex + 3, zzName.LastIndexOf("-") - zhIndex - 2).Trim(new char[] { ' ', '-' });
                                            }

                                    }
                                    else
                                    {
                                        if (zyIndex >= 0)
                                        {
                                            zzxlId = "14";
                                            zzxl = "专业资质";
                                            if (zzName.LastIndexOf("-") - zyIndex - 2 > 0)
                                                zzlb = zzName.Substring(zyIndex + 3, zzName.LastIndexOf("-") - zyIndex - 2).Trim(new char[] { ' ', '-' });
                                        }
                                        else
                                            if (zhIndex >= 0)
                                            {
                                                zzxlId = "12";
                                                zzxl = "综合资质";
                                                if (zzName.LastIndexOf("-") - zhIndex - 2 > 0)
                                                    zzlb = zzName.Substring(zhIndex + 3, zzName.LastIndexOf("-") - zhIndex - 2).Trim(new char[] { ' ', '-' });
                                            }
                                            else if (hyIndex >= 0)
                                            {
                                                zzxlId = "13";
                                                zzxl = "行业资质";

                                                if (zzName.LastIndexOf("-") - hyIndex - 2 > 0)
                                                    zzlb = zzName.Substring(hyIndex + 3, zzName.LastIndexOf("-") - hyIndex - 2).Trim(new char[] { ' ', '-' });
                                            }
                                            else if (zxIndex >= 0)
                                            {

                                                zzxlId = "15";
                                                zzxl = "专项资质";
                                                if (zzName.LastIndexOf("-") - zxIndex - 2 > 0)
                                                    zzlb = zzName.Substring(zxIndex + 3, zzName.LastIndexOf("-") - zxIndex - 2).Trim(new char[] { ' ', '-' });
                                            }
                                            else if (swsIndex >= 0)
                                            {
                                                zzxlId = "16";
                                                zzxl = "事务所资质";
                                                if (zzName.LastIndexOf("-") - swsIndex - 3 > 0)
                                                    zzlb = zzName.Substring(swsIndex + 4, zzName.LastIndexOf("-") - swsIndex - 3).Trim(new char[] { ' ', '-' });
                                            }
                                    }

                                    if (!string.IsNullOrEmpty(zzlb))
                                    {
                                        string sql = "select * from UEPP_Code where CodeType='企业资质类别' and parentCodeType='企业资质序列' and ParentCode=" + zzxlId + " and CodeInfo='" + zzlb + "'";
                                        DataTable dt_uepp_code = dataService.DB.ExeSqlForDataTable(sql, null, "dt");
                                        if (dt_uepp_code.Rows.Count == 0)
                                        {
                                            dt_uepp_code.Rows.Add(dt_uepp_code.NewRow());
                                            dt_uepp_code.Rows[0]["ParentCodeType"] = "企业资质序列";
                                            dt_uepp_code.Rows[0]["ParentCode"] = zzxlId;
                                            dt_uepp_code.Rows[0]["CodeType"] = "企业资质类别";
                                            dt_uepp_code.Rows[0]["Code"] = dataService.Get_uepp_Code_NewCode("企业资质类别");
                                            dt_uepp_code.Rows[0]["CodeInfo"] = zzlb;
                                            dt_uepp_code.Rows[0]["CodeDesc"] = "";
                                            dt_uepp_code.Rows[0]["OrderID"] = dt_uepp_code.Rows[0]["Code"];
                                            dataService.Submit_uepp_code(dt_uepp_code);
                                        }

                                        zzlbId = dt_uepp_code.Rows[0]["Code"].ToString2();
                                    }

                                    for (int i = 0; i < dt_uepp_qyzzmx_kcsj.Rows.Count; i++)
                                    {
                                        if (kcIndex >= 0)
                                        {
                                            if (zsjb.Equals("省级"))
                                            {
                                                if (dt_uepp_qyzzmx_kcsj.Rows[i]["zzlb"].ToString2().Equals(zzlb) && dt_uepp_qyzzmx_kcsj.Rows[i]["csywlxID"].ToString2().Equals("5") && dt_uepp_qyzzmx_kcsj.Rows[i]["zslxID"].ToString2().Equals("51"))
                                                {
                                                    isExists = i;
                                                    break;
                                                }

                                            }
                                            else
                                            {
                                                if (dt_uepp_qyzzmx_kcsj.Rows[i]["zzlb"].ToString2().Equals(zzlb) && dt_uepp_qyzzmx_kcsj.Rows[i]["csywlxID"].ToString2().Equals("5") && dt_uepp_qyzzmx_kcsj.Rows[i]["zslxID"].ToString2().Equals("50"))
                                                {
                                                    isExists = i;
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (zsjb.Equals("省级"))
                                            {
                                                if (dt_uepp_qyzzmx_kcsj.Rows[i]["zzlb"].ToString2().Equals(zzlb) && dt_uepp_qyzzmx_kcsj.Rows[i]["csywlxID"].ToString2().Equals("6") && dt_uepp_qyzzmx_kcsj.Rows[i]["zslxID"].ToString2().Equals("61"))
                                                {
                                                    isExists = i;
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                if (dt_uepp_qyzzmx_kcsj.Rows[i]["zzlb"].ToString2().Equals(zzlb) && dt_uepp_qyzzmx_kcsj.Rows[i]["csywlxID"].ToString2().Equals("6") && dt_uepp_qyzzmx_kcsj.Rows[i]["zslxID"].ToString2().Equals("60"))
                                                {
                                                    isExists = i;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    DataRow row_qyzzmx;
                                    if (isExists >= 0)
                                    {
                                        row_qyzzmx = dt_uepp_qyzzmx_kcsj.Rows[isExists];
                                        if (row_qyzzmx["tag"].ToString2().IndexOf(tag) < 0)
                                        {
                                            continue;
                                        }
                                        if (row_qyzzmx["xgrqsj"].ToString2() == row_qyzz["XKJG_Date"].ToString2())
                                        {
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        row_qyzzmx = dt_uepp_qyzzmx_kcsj.NewRow();
                                        dt_uepp_qyzzmx_kcsj.Rows.Add(row_qyzzmx);
                                        row_qyzzmx["ID"] = dataService.Get_uepp_qyxxmx_NewID();
                                    }

                                    row_qyzzmx["qyID"] = qyID;
                                    if (kcIndex >= 0)
                                    {
                                        row_qyzzmx["csywlx"] = "工程勘察";
                                        row_qyzzmx["csywlxID"] = "5";
                                        if (zsjb.Equals("省级"))
                                        {
                                            row_qyzzmx["zslx"] = "省工程勘察资质证";
                                            row_qyzzmx["zslxID"] = "51";
                                        }
                                        else
                                        {
                                            row_qyzzmx["zslx"] = "部工程勘察资质证";
                                            row_qyzzmx["zslxID"] = "50";
                                        }
                                    }
                                    else
                                    {
                                        row_qyzzmx["csywlx"] = "工程设计";
                                        row_qyzzmx["csywlxID"] = "6";
                                        if (zsjb.Equals("省级"))
                                        {
                                            row_qyzzmx["zslx"] = "工程设计资质证";
                                            row_qyzzmx["zslxID"] = "61";
                                        }
                                        else
                                        {
                                            row_qyzzmx["zslx"] = "部工程设计资质证";
                                            row_qyzzmx["zslxID"] = "60";
                                        }
                                    }

                                    if (row_qyzz["ZXZZ"].ToString2() == "是" || row_qyzz["ZXZZ"].ToString2() == "1")
                                    {
                                        row_qyzzmx["zzbz"] = "主项";
                                    }
                                    else
                                    {
                                        row_qyzzmx["zzbz"] = "增项";
                                    }
                                    row_qyzzmx["zzxl"] = zzxl;
                                    row_qyzzmx["zzxlID"] = zzxlId;
                                    if (!string.IsNullOrEmpty(zzlb))
                                    {
                                        row_qyzzmx["zzlb"] = zzlb;
                                        row_qyzzmx["zzlbID"] = zzlbId;
                                    }
                                    if (!string.IsNullOrEmpty(row_qyzz["ZZZSDJ"].ToString2()))
                                    {
                                        row_qyzzmx["zzdj"] = row_qyzz["ZZZSDJ"].ToString2();

                                        DataTable dt_uepp_code_zzdj = dataService.DB.ExeSqlForDataTable("select * from UEPP_Code where parentCodeType='企业资质序列' and parentCode='" + zzxlId + "' and CodeType='企业资质等级' and CodeInfo='" + row_qyzz["ZZZSDJ"].ToString2() + "'", null, "dt");

                                        if (dt_uepp_code_zzdj.Rows.Count == 0)
                                        {
                                            dt_uepp_code_zzdj.Rows.Add(dt_uepp_code_zzdj.NewRow());
                                            dt_uepp_code_zzdj.Rows[0]["ParentCodeType"] = "企业资质序列";
                                            dt_uepp_code_zzdj.Rows[0]["ParentCode"] = zzxlId;
                                            dt_uepp_code_zzdj.Rows[0]["CodeType"] = "企业资质等级";
                                            dt_uepp_code_zzdj.Rows[0]["Code"] = dataService.Get_uepp_Code_NewCode("企业资质等级");
                                            dt_uepp_code_zzdj.Rows[0]["CodeInfo"] = row_qyzz["ZZZSDJ"].ToString2();
                                            dt_uepp_code_zzdj.Rows[0]["CodeDesc"] = "";
                                            dt_uepp_code_zzdj.Rows[0]["OrderID"] = dt_uepp_code_zzdj.Rows[0]["Code"];
                                            dataService.Submit_uepp_code(dt_uepp_code_zzdj);
                                        }
                                        row_qyzzmx["zzdjID"] = dt_uepp_code_zzdj.Rows[0]["Code"];
                                    }
                                    row_qyzzmx["cjywfw"] = row_qyzz["ZZName"];

                                    row_qyzzmx["xgrqsj"] = string.IsNullOrEmpty(row_qyzz["XKJG_Date"].ToString2()) ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : row_qyzz["XKJG_Date"].ToString2();
                                    row_qyzzmx["tag"] = tag;
                                    row_qyzzmx["xgr"] = "";
                                    row_qyzzmx["DataState"] = 0;

                                    #endregion

                                }
                                if (dt_uepp_qyzzmx_kcsj.Rows.Count > 0)
                                    dataService.Submit_uepp_qyzzmx(dt_uepp_qyzzmx_kcsj);


                                #endregion
                            }
                            catch (Exception ex)
                            {
                                row_SaveDataLog["SaveState"] = 0;
                                row_SaveDataLog["Msg"] = "企业信息保存出现异常：" + ex.Message;
                            }
                            dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                        }
                        if (dt_SaveDataLog.Rows.Count > 0)
                            dataService.Submit_SaveDataLog(dt_SaveDataLog);

                        row_DataJkDataDetail_qyxx["allCount"] = allCount_qyxx;
                        row_DataJkDataDetail_qyxx["successCount"] = successCount_qyxx;
                        row_DataJkDataDetail_qyxx["IsOk"] = 1;
                        row_DataJkDataDetail_qyxx["ErrorMsg"] = "";

                        if (dt_DataJkDataDetail_qyxx.Rows.Count > 0)
                            dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_qyxx);
                    }
                    catch (Exception ex)
                    {
                        Public.WriteLog("Exception1:" + ex.Message);
                        row_DataJkDataDetail_qyxx["allCount"] = allCount_qyxx;
                        row_DataJkDataDetail_qyxx["successCount"] = successCount_qyxx;
                        row_DataJkDataDetail_qyxx["IsOk"] = 0;
                        row_DataJkDataDetail_qyxx["ErrorMsg"] = ex.Message;

                        if (dt_DataJkDataDetail_qyxx.Rows.Count > 0)
                            dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_qyxx);
                    }
                }
                catch (Exception ex)
                {
                    Public.WriteLog("Exception:" + ex.Message);
                    apiMessage += ex.Message;
                }

                finally
                {
                    DataTable dtapicb = dataService.GetSchema_API_cb();
                    DataRow row_apicb = dtapicb.NewRow();
                    dtapicb.Rows.Add(row_apicb);
                    row_apicb["apiCbID"] = dataService.Get_apiCbNewID();
                    row_apicb["apiFlow"] = "28";
                    row_apicb["apiMethod"] = "Get_Enterprise_Tab_Skcsj";
                    row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                    row_apicb["apiDyMessage"] = apiMessage;
                    row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dataService.Submit_API_cb(dtapicb);

                    dataService.UpdateZbJkzt("28", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

                }
            }
        }

        /// <summary>
        /// 省勘察设计系统获取勘察设计合同备案
        /// </summary>
        /// <param name="DataJkLogID"></param>
        void YourTask_PullDataFromSKcsj_htba()
        {
            string apiMessage = string.Empty; 
            DataTable dtapizb = dataService.Get_API_zb_apiFlow("28");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                //往数据监控日志表添加一条记录
                DataTable dt_DataJkLog = dataService.GetSchema_DataJkLog();
                DataRow row_DataJkLog = dt_DataJkLog.NewRow();
                dt_DataJkLog.Rows.Add(row_DataJkLog);
                row_DataJkLog["ID"] = dataService.Get_DataJkLogNewID();
                row_DataJkLog["DataFlow"] = DataFlow.省勘察设计系统到无锡数据中心.ToInt32();
                row_DataJkLog["DataFlowName"] = DataFlow.省勘察设计系统到无锡数据中心.ToString();
                row_DataJkLog["ServiceUrl"] = "";
                row_DataJkLog["csTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dataService.Submit_DataJkLog(dt_DataJkLog);

                int allCount_qyxx = 0, successCount_qyxx = 0;
                try
                {
                    string tag = Tag.省勘察设计系统.ToString();

                    //往数据监控日志表项添加一条记录
                    DataTable dt_DataJkDataDetail_qyxx = dataService.GetSchema_DataJkDataDetail();
                    DataRow row_DataJkDataDetail_qyxx = dt_DataJkDataDetail_qyxx.NewRow();
                    dt_DataJkDataDetail_qyxx.Rows.Add(row_DataJkDataDetail_qyxx);

                    row_DataJkDataDetail_qyxx["ID"] = dataService.Get_DataJkDataDetailNewID().ToInt64();
                    row_DataJkDataDetail_qyxx["DataJkLogID"] = row_DataJkLog["ID"];
                    row_DataJkDataDetail_qyxx["tableName"] = "TBContractRecordManage";
                    row_DataJkDataDetail_qyxx["MethodName"] = "直接从省勘察设计数据库读取";
                    row_DataJkDataDetail_qyxx["bz"] = "从省勘察设计系统拉取勘察设计合同备案";

                    try
                    {
                        DataTable dt_SaveDataLog = dataService.GetSchema_SaveDataLog();

                        DataTable dt = dataService.Get_Enterprise_Tab_Skcsj_htba();

                        Public.WriteLog("获取省勘察设计系统勘察设计合同备案:" + dt.Rows.Count);

                        foreach (DataRow row in dt.Rows)
                        {
                            allCount_qyxx++;
                            DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
                            row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_qyxx["ID"];
                            row_SaveDataLog["DataXml"] = "";
                            row_SaveDataLog["PKID"] = row["PKID"].ToString2();
                            row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                            try
                            {
                                #region 合同备案
                                string recordNum = row["RecordNum"].ToString2();
                                string recordInnerNum = row["RecordInnerNum"].ToString2();

                                DataTable dt_TBContractRecordManage = dataService.GetTBData_TBContractRecordManageByRecordNum(recordNum, recordInnerNum);
                                DataRow toSaveRow;
                                if (dt_TBContractRecordManage.Rows.Count == 0)
                                {
                                    toSaveRow = dt_TBContractRecordManage.NewRow();
                                    dt_TBContractRecordManage.Rows.Add(toSaveRow);
                                    DataTableHelp.DataRow2DataRow(row, toSaveRow);
                                    toSaveRow["cjrqsj"] = DateTime.Now;
                                    toSaveRow["xgrqsj"] = DateTime.Now;
                                    
                                }
                                else
                                {
                                    toSaveRow = dt_TBContractRecordManage.Rows[0];
                                    DataTableHelp.DataRow2DataRow(row, toSaveRow, new List<string>() { "PKID" });
                                    toSaveRow["xgrqsj"] = DateTime.Now;
                                }
                                toSaveRow["updateUser"] = "dbsynch";
                                toSaveRow["UpdateFlag"] = "U";

                                if (toSaveRow["PropietorCorpCode"].ToString2().Length > 18)
                                {
                                    toSaveRow["PropietorCorpCode"] = toSaveRow["PropietorCorpCode"].ToString2().Substring(0, 18);
                                }
                                if (toSaveRow["ContractorCorpCode"].ToString2().Length > 18)
                                {
                                    toSaveRow["ContractorCorpCode"] = toSaveRow["ContractorCorpCode"].ToString2().Substring(0, 18);
                                }
                                toSaveRow["sbdqbm"] = row["TJQHCode"].ToString2().Substring(0, 6);
                                toSaveRow["tag"] = Tag.省勘察设计系统;
                                if (dataService.Submit_TBContractRecordManage(dt_TBContractRecordManage))
                                {
                                    successCount_qyxx++;
                                }
                                else
                                {
                                    row_SaveDataLog["SaveState"] = 0;
                                    row_SaveDataLog["Msg"] = "勘察设计合同备案保存失败！";
                                    dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                                    continue;
                                }

                                

                                #endregion
                                 
                            }
                            catch (Exception ex)
                            {
                                row_SaveDataLog["SaveState"] = 0;
                                row_SaveDataLog["Msg"] = "勘察设计合同备案保存出现异常：" + ex.Message;
                                dt_SaveDataLog.Rows.Add(row_SaveDataLog);
                            }
                           
                        }
                        if (dt_SaveDataLog.Rows.Count > 0)
                            dataService.Submit_SaveDataLog(dt_SaveDataLog);

                        row_DataJkDataDetail_qyxx["allCount"] = allCount_qyxx;
                        row_DataJkDataDetail_qyxx["successCount"] = successCount_qyxx;
                        row_DataJkDataDetail_qyxx["IsOk"] = 1;
                        row_DataJkDataDetail_qyxx["ErrorMsg"] = "";

                        if (dt_DataJkDataDetail_qyxx.Rows.Count > 0)
                            dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_qyxx);
                    }
                    catch (Exception ex)
                    {
                        Public.WriteLog("Exception1:" + ex.Message);
                        row_DataJkDataDetail_qyxx["allCount"] = allCount_qyxx;
                        row_DataJkDataDetail_qyxx["successCount"] = successCount_qyxx;
                        row_DataJkDataDetail_qyxx["IsOk"] = 0;
                        row_DataJkDataDetail_qyxx["ErrorMsg"] = ex.Message;

                        if (dt_DataJkDataDetail_qyxx.Rows.Count > 0)
                            dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_qyxx);
                    }
                }
                catch (Exception ex)
                {
                    Public.WriteLog("Exception:" + ex.Message);
                    apiMessage += ex.Message;
                }

                finally
                {
                    DataTable dtapicb = dataService.GetSchema_API_cb();
                    DataRow row_apicb = dtapicb.NewRow();
                    dtapicb.Rows.Add(row_apicb);
                    row_apicb["apiCbID"] = dataService.Get_apiCbNewID();
                    row_apicb["apiFlow"] = "28";
                    row_apicb["apiMethod"] = "Get_Enterprise_Tab_Skcsj";
                    row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                    row_apicb["apiDyMessage"] = apiMessage;
                    row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dataService.Submit_API_cb(dtapicb);

                    dataService.UpdateZbJkzt("28", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

                }
            }
        }


        #endregion

        #region 推送数据

        /// <summary>
        /// 往省信息中心推送施工企业、注册建造师小型项目管理师人员信息数据
        /// </summary>
        void YourTask_PushDataToSxxzx_SgqyRyxx()
        {

            #region 施工单位

            try
            {
                string tableName = "qy_jzyqy";
                StringBuilder str = new StringBuilder();
                DataTable dt_qyjbxx_sgqy = dataService.Get_uepp_Qyjbxx_Sgqy_ToSb();
                foreach (DataRow row in dt_qyjbxx_sgqy.Rows)
                {
                    try
                    {
                        str.AppendFormat("<qybm>{0}</qybm>", xmlHelper.EncodeString(row["zzjgdm"].ToString2()));
                        str.AppendFormat("<zzjgdm>{0}</zzjgdm>", xmlHelper.EncodeString(row["zzjgdm"].ToString2()));
                        str.AppendFormat("<qymc>{0}</qymc>", xmlHelper.EncodeString(row["qymc"].ToString2()));
                        DateTime time;
                        if (!string.IsNullOrEmpty(row["clrq"].ToString2()) && DateTime.TryParse(row["clrq"].ToString2(), out time))
                            str.AppendFormat("<jlsj>{0}</jlsj>", xmlHelper.EncodeString(row["clrq"].ToString2()));
                        else
                            str.AppendFormat("<jlsj>{0}</jlsj>", xmlHelper.EncodeString("1900-1-1"));

                        str.AppendFormat("<zzzsbh>{0}</zzzsbh>", "");
                        if (string.IsNullOrEmpty(row["Province"].ToString2()))
                            str.AppendFormat("<sf>{0}</sf>", xmlHelper.EncodeString("无"));
                        else
                            str.AppendFormat("<sf>{0}</sf>", xmlHelper.EncodeString(row["Province"].ToString2()));

                        if (string.IsNullOrEmpty(row["City"].ToString2()))
                            str.AppendFormat("<sz>{0}</sz>", xmlHelper.EncodeString("无"));
                        else
                            str.AppendFormat("<sz>{0}</sz>", xmlHelper.EncodeString(row["City"].ToString2()));

                        if (string.IsNullOrEmpty(row["County"].ToString2()))
                            str.AppendFormat("<xq>{0}</xq>", xmlHelper.EncodeString("无"));
                        else
                            str.AppendFormat("<xq>{0}</xq>", xmlHelper.EncodeString(row["County"].ToString2()));

                        str.AppendFormat("<sszyzsqy>{0}</sszyzsqy>", xmlHelper.EncodeString(row["sfsyq"].ToString2()));
                        if (string.IsNullOrEmpty(row["zcdd"].ToString2()))
                            str.AppendFormat("<zcdz>{0}</zcdz>", xmlHelper.EncodeString("无"));
                        else
                            str.AppendFormat("<zcdz>{0}</zcdz>", xmlHelper.EncodeString(row["zcdd"].ToString2()));

                        str.AppendFormat("<xxdd>{0}</xxdd>", xmlHelper.EncodeString(row["xxdd"].ToString2()));
                        str.AppendFormat("<yyzzzch>{0}</yyzzzch>", xmlHelper.EncodeString(row["yyzzzch"].ToString2()));
                        if (row["fddbr"] == DBNull.Value || String.IsNullOrEmpty(row["fddbr"].ToString2()))
                            str.AppendFormat("<fddbr>{0}</fddbr>", xmlHelper.EncodeString("无"));
                        else
                            str.AppendFormat("<fddbr>{0}</fddbr>", xmlHelper.EncodeString(row["fddbr"].ToString2()));


                        str.AppendFormat("<fddbrsfzh>{0}</fddbrsfzh>", xmlHelper.EncodeString("无"));
                        str.AppendFormat("<fddbrsj>{0}</fddbrsj>", xmlHelper.EncodeString("无"));

                        str.AppendFormat("<qylx>{0}</qylx>", xmlHelper.EncodeString("10"));

                        str.AppendFormat("<lxdh>{0}</lxdh>", xmlHelper.EncodeString((row["lxdh"].ToString2().Length > 30 ? row["lxdh"].ToString2().Substring(0, 30) : row["lxdh"].ToString2())));
                        str.AppendFormat("<cz>{0}</cz>", xmlHelper.EncodeString(row["cz"].ToString2()));
                        str.AppendFormat("<gcjsryzs>{0}</gcjsryzs>", xmlHelper.EncodeString(row["gcjsry_zs"].ToString2()));
                        str.AppendFormat("<gcjsrygjzcrs>{0}</gcjsrygjzcrs>", xmlHelper.EncodeString(row["gcjsry_gjzcrs"].ToString2()));
                        str.AppendFormat("<gcjsryzjzcrs>{0}</gcjsryzjzcrs>", xmlHelper.EncodeString(row["gcjsry_zjzcrs"].ToString2()));

                        SqlParameterCollection sp = dataService.DB.CreateSqlParameterCollection();
                        sp.Add("@qyID", row["qyID"]);
                        string xmjlzs = dataService.DB.ExeSqlForString(@"select  count(*) from uepp_ryjbxx 
             where datastate<>-1 AND ryID in (select a.ryID from UEPP_QyRy a WHERE a.ryzyzglxid in ('1','2') and a.qyID=@qyID)", sp);
                        str.AppendFormat("<xmjlzs>{0}</xmjlzs>", xmlHelper.EncodeString(xmjlzs));
                        //str.AppendFormat("<xmjlzs>{0}</xmjlzs>", xmlHelper.EncodeString("0"));

                        string sjxmjl = dataService.DB.ExeSqlForString(@" select count(*) from uepp_ryjbxx c where c.ryID in (
             select ryID from UEPP_QyRy where ryzyzglxid=2 and  datastate<>-1 and qyID =@qyID ) ", sp);
                        str.AppendFormat("<sjxmjl>{0}</sjxmjl>", xmlHelper.EncodeString(sjxmjl));
                        //str.AppendFormat("<sjxmjl>{0}</sjxmjl>", xmlHelper.EncodeString("0"));

                        string aqy = dataService.DB.ExeSqlForString(@"select count(*) from uepp_ryjbxx c where c.ryID in (
             select ryID from UEPP_QyRy where ryzyzglxid=6 and  datastate<>-1 and qyID =@qyID )", sp);
                        str.AppendFormat("<aqy>{0}</aqy>", xmlHelper.EncodeString(aqy));
                        //str.AppendFormat("<aqy>{0}</aqy>", xmlHelper.EncodeString("0"));

                        string zjy = dataService.DB.ExeSqlForString(@"select count(*) from uepp_ryjbxx c where c.ryID in (
             select ryID from UEPP_QyRy where ryzyzglxid=8 and  datastate<>-1 and qyID =@qyID )", sp);
                        str.AppendFormat("<zjy>{0}</zjy>", xmlHelper.EncodeString(zjy));
                        //str.AppendFormat("<zjy>{0}</zjy>", xmlHelper.EncodeString("0"));

                        string sgy = dataService.DB.ExeSqlForString(@"select count(*) from uepp_ryjbxx c where c.ryID in (
             select ryID from UEPP_QyRy where ryzyzglxid=7 and  datastate<>-1 and qyID =@qyID )", sp);
                        str.AppendFormat("<sgy>{0}</sgy>", xmlHelper.EncodeString(sgy));
                        //str.AppendFormat("<sgy>{0}</sgy>", xmlHelper.EncodeString("0"));

                        string zcbz = row["zczb"].ToString2();
                        if (!string.IsNullOrEmpty(zcbz))
                        {
                            str.AppendFormat("<zczb>{0}</zczb>", xmlHelper.EncodeString(zcbz));
                        }
                        else
                        {
                            str.AppendFormat("<zczb>{0}</zczb>", "");
                        }
                        str.AppendFormat("<zczbbz>{0}</zczbbz>", "");
                        if (!string.IsNullOrEmpty(row["CountyID"].ToString2()))
                            str.AppendFormat("<xzdqbm>{0}</xzdqbm>", xmlHelper.EncodeString(row["CountyID"].ToString2()));
                        else
                            str.AppendFormat("<xzdqbm>{0}</xzdqbm>", xmlHelper.EncodeString("无"));

                        SqlParameterCollection sp2 = dataService.DB.CreateSqlParameterCollection();
                        sp2.Add("@zzbz", "主项");
                        sp2.Add("@qyID", row["qyID"]);
                        string zzdj = dataService.DB.ExeSqlForString("select  zzdj   from uepp_qyzzmx where qyid=@qyID and csywlxID=1 and zzbz=@zzbz", sp2);

                        if (zzdj.IndexOf("特级") >= 0)
                            str.AppendFormat("<zzdj>{0}</zzdj>", xmlHelper.EncodeString("00"));
                        else if (zzdj.IndexOf("一级") >= 0)
                            str.AppendFormat("<zzdj>{0}</zzdj>", xmlHelper.EncodeString("01"));
                        else
                            if (zzdj.IndexOf("二级") >= 0)
                                str.AppendFormat("<zzdj>{0}</zzdj>", xmlHelper.EncodeString("02"));
                            else if (zzdj.IndexOf("三级") >= 0)
                                str.AppendFormat("<zzdj>{0}</zzdj>", xmlHelper.EncodeString("03"));
                            else if (zzdj.IndexOf("不分") >= 0)
                                str.AppendFormat("<zzdj>{0}</zzdj>", xmlHelper.EncodeString("04"));
                            else
                                str.AppendFormat("<zzdj>{0}</zzdj>", "");

                        DataTable dt_aqscxkz = dataService.DB.ExeSqlForDataTable(" select top 1 zsbh,zsyxqrq,zsyxzrq from UEPP_Qyzs where csywlxID='1' and zslxID='11' and sfzzz=0 and QyID=@qyID ", sp, "dt");
                        if (dt_aqscxkz.Rows.Count > 0)
                        {
                            str.AppendFormat("<aqscxkz>{0}</aqscxkz>", xmlHelper.EncodeString(dt_aqscxkz.Rows[0]["zsbh"].ToString2()));

                            if (!string.IsNullOrEmpty(dt_aqscxkz.Rows[0]["zsyxqrq"].ToString2()) && DateTime.TryParse(dt_aqscxkz.Rows[0]["zsyxqrq"].ToString2(), out time))
                            {
                                str.AppendFormat("<aqscxkz_yxq>{0}</aqscxkz_yxq>", xmlHelper.EncodeString(dt_aqscxkz.Rows[0]["zsyxqrq"].ToString2()));
                            }
                            else
                            {
                                str.AppendFormat("<aqscxkz_yxq>{0}</aqscxkz_yxq>", "");
                            }
                        }
                        else
                        {
                            str.AppendFormat("<aqscxkz>{0}</aqscxkz>", "");
                            str.AppendFormat("<aqscxkz_yxq>{0}</aqscxkz_yxq>", "");
                        }

                        DataTable dt_saveLog = dataService.Get_SaveToStLog2("UEPP_Qyjbxx", row["zzjgdm"].ToString2());
                        DataRow row_saveLog;
                        if (dt_saveLog.Rows.Count == 0)
                        {
                            row_saveLog = dt_saveLog.NewRow();
                            dt_saveLog.Rows.Add(row_saveLog);
                            row_saveLog["TableName"] = "UEPP_Qyjbxx";
                            row_saveLog["PKID"] = row["zzjgdm"];
                            row_saveLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            row_saveLog = dt_saveLog.Rows[0];
                        }
                        row_saveLog["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        string msg = client.SaveStData(tableName, str.ToString(), userName_qyry, password_qyry, "U");
                        row_saveLog["SbToStMsg"] = msg;
                        if (msg.IndexOf("OK") >= 0)
                        {
                            row_saveLog["SbToStState"] = 0;
                        }
                        else
                        {
                            row_saveLog["SbToStState"] = 1;
                        }
                        row_saveLog["SbToStMsg"] = msg;
                        dataService.Submit_SaveToStLog2(dt_saveLog);
                        str.Remove(0, str.Length);
                    }
                    catch (Exception ex)
                    {
                        Public.WriteLog("上报施工企业时出现异常:zzjgdm:" + row["zzjgdm"] + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Public.WriteLog("上报施工企业时出现异常:" + ex.Message);
            }

            #endregion

            #region 企业资质信息 qy_jzyqy_zzqk

            try
            {
                string tableName = "qy_jzyqy_zzqk";

                DataTable dt_qyzz = dataService.Get_uepp_Qyzz();

                foreach (DataRow row in dt_qyzz.Rows)
                {
                    string zzjgdm = row["zzjgdm"].ToString2();
                    try
                    {
                        StringBuilder str = new StringBuilder();
                        str.AppendFormat("<qybm>{0}</qybm>", xmlHelper.EncodeString(zzjgdm));
                        string zzxl = row["zzxl"].ToString2().Trim(new char[] { ' ', '\r', '\n' });
                        string zzlb = row["zzlb"].ToString2().Trim(new char[] { ' ', '\r', '\n' });
                        #region

                        if (string.IsNullOrEmpty(zzxl) || string.IsNullOrEmpty(zzlb))
                            continue;

                        if (zzxl.IndexOf("施工总承包") >= 0)
                        {

                            str.AppendFormat("<zzxl>{0}</zzxl>", xmlHelper.EncodeString("A"));
                            if (zzlb.IndexOf("房屋建筑") >= 0)
                            {
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("01"));
                            }
                            else if (zzlb.IndexOf("公路") >= 0)
                            {
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("02"));
                            }
                            else if (zzlb.IndexOf("铁路") >= 0)
                            {
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("03"));
                            }
                            else if (zzlb.IndexOf("港口与航道") >= 0)
                            {
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("04"));
                            }
                            else if (zzlb.IndexOf("水利水电") >= 0)
                            {
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("05"));
                            }
                            else if (zzlb.IndexOf("电力") >= 0)
                            {
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("06"));
                            }
                            else if (zzlb.IndexOf("矿山") >= 0)
                            {
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("07"));
                            }
                            else if (zzlb.IndexOf("冶炼") >= 0)
                            {
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("08"));
                            }
                            else if (zzlb.IndexOf("化工石油") >= 0)
                            {
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("09"));
                            }
                            else if (zzlb.IndexOf("市政公用") >= 0)
                            {
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("10"));
                            }
                            else if (zzlb.IndexOf("通信") >= 0)
                            {
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("11"));
                            }
                            else if (zzlb.IndexOf("机电安装") >= 0)
                            {
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("12"));
                            }
                            else if (zzlb.IndexOf("钢结构主体") >= 0)
                            {
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("13"));
                            }
                            else
                                continue;

                        }
                        else if (zzxl.IndexOf("专业承包") >= 0)
                        {
                            str.AppendFormat("<zzxl>{0}</zzxl>", xmlHelper.EncodeString("B"));
                            if (zzlb.IndexOf("地基与基础") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("01"));
                            else if (zzlb.IndexOf("土石方") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("02"));
                            else if (zzlb.IndexOf("建筑装修") >= 0 || zzlb.IndexOf("建筑装饰") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("03"));
                            else if (zzlb.IndexOf("幕墙") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("04"));
                            else if (zzlb.IndexOf("预拌商品混凝土") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("05"));
                            else if (zzlb.IndexOf("混凝土预制构件") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("06"));
                            else if (zzlb.IndexOf("园林古建筑") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("07"));
                            else if (zzlb.IndexOf("钢结构") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("08"));
                            else if (zzlb.IndexOf("高耸构筑物") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("09"));
                            else if (zzlb.IndexOf("电梯安装") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("10"));
                            else if (zzlb.IndexOf("消防设施") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("11"));
                            else if (zzlb.IndexOf("建筑防水") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("12"));
                            else if (zzlb.IndexOf("防腐保温") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("13"));
                            else if (zzlb.IndexOf("附着升降脚手架") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("14"));
                            else if (zzlb.IndexOf("金属门窗") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("15"));
                            else if (zzlb.IndexOf("预应力") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("16"));
                            else if (zzlb.IndexOf("起重设备安装") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("17"));
                            else if (zzlb.IndexOf("机电设备安装") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("18"));
                            else if (zzlb.IndexOf("爆破与拆除") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("19"));
                            else if (zzlb.IndexOf("建筑智能化") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("20"));
                            else if (zzlb.IndexOf("环保") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("21"));
                            else if (zzlb.IndexOf("电信") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("22"));
                            else if (zzlb.IndexOf("电子") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("23"));
                            else if (zzlb.IndexOf("桥梁") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("24"));
                            else if (zzlb.IndexOf("隧道") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("25"));
                            else if (zzlb.IndexOf("公路路面") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("26"));
                            else if (zzlb.IndexOf("公路路基") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("27"));
                            else if (zzlb.IndexOf("公路交通") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("28"));
                            else if (zzlb.IndexOf("铁路电务") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("29"));
                            else if (zzlb.IndexOf("铁路铺轨架梁") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("30"));
                            else if (zzlb.IndexOf("铁路电气化") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("31"));
                            else if (zzlb.IndexOf("机场场道") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("32"));
                            else if (zzlb.IndexOf("机场空管工程及航站楼弱电系统") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("33"));
                            else if (zzlb.IndexOf("机场目视助航") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("34"));
                            else if (zzlb.IndexOf("港口与海岸") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("35"));
                            else if (zzlb.IndexOf("港口装卸设备安装") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("36"));
                            else if (zzlb.IndexOf("航道") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("37"));
                            else if (zzlb.IndexOf("通航建筑") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("38"));
                            else if (zzlb.IndexOf("通航设备安装") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("39"));
                            else if (zzlb.IndexOf("水上交通管制") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("40"));
                            else if (zzlb.IndexOf("水工建筑物基础处理") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("41"));
                            else if (zzlb.IndexOf("水工金属结构制作与安装") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("42"));
                            else if (zzlb.IndexOf("水利水电机电设备安装") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("43"));
                            else if (zzlb.IndexOf("河湖整治") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("44"));
                            else if (zzlb.IndexOf("堤防") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("45"));
                            else if (zzlb.IndexOf("水工大坝") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("46"));
                            else if (zzlb.IndexOf("水工隧洞") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("47"));
                            else if (zzlb.IndexOf("火电设备安装") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("48"));
                            else if (zzlb.IndexOf("送变电") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("49"));
                            else if (zzlb.IndexOf("核工程") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("50"));
                            else if (zzlb.IndexOf("炉窑") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("51"));
                            else if (zzlb.IndexOf("冶炼机电设备安装") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("52"));
                            else if (zzlb.IndexOf("化工石油设备管道安装") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("53"));
                            else if (zzlb.IndexOf("管道") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("54"));
                            else if (zzlb.IndexOf("无损检测") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("55"));
                            else if (zzlb.IndexOf("海洋石油") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("56"));
                            else if (zzlb.IndexOf("城市轨道交通") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("57"));
                            else if (zzlb.IndexOf("城市及道路照明") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("58"));
                            else if (zzlb.IndexOf("体育场地设施") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("59"));
                            else if (zzlb.IndexOf("特种专业") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("60"));
                            else if (zzlb.IndexOf("空气净化") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("61"));
                            else if (zzlb.IndexOf("高空作业") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("62"));
                            else if (zzlb.IndexOf("市政养护维修") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("63"));
                            else if (zzlb.IndexOf("凿井") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("64"));
                            else if (zzlb.IndexOf("公路养护") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("65"));
                            else if (zzlb.IndexOf("航道养护") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("66"));
                            else if (zzlb.IndexOf("可再生能源") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("67"));
                            else if (zzlb.IndexOf("建筑物非爆破拆除") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("68"));
                            else if (zzlb.IndexOf("城市道路桥梁") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("69"));
                            else if (zzlb.IndexOf("城市给水排水") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("70"));
                            else if (zzlb.IndexOf("城市垃圾处理") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("71"));
                            else if (zzlb.IndexOf("城市燃气供热") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("72"));
                            else if (zzlb.IndexOf("火电设备检修") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("73"));
                            else if (zzlb.IndexOf("住宅室内装饰装修专业") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("74"));
                            else if (zzlb.IndexOf("园林绿化") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("75"));
                            else if (zzlb.IndexOf("地基与基础") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("76"));
                            else if (zzlb.IndexOf("结构补强") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("77"));
                            else if (zzlb.IndexOf("建筑物纠偏和平移") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("78"));
                            else if (zzlb.IndexOf("特殊设备的起重吊装") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("79"));
                            else if (zzlb.IndexOf("特种防雷技术") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("80"));
                            else if (zzlb.IndexOf("桥梁伸缩缝安装") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("81"));

                            else if (zzlb.IndexOf("非开挖管道定向穿越") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("82"));
                            else if (zzlb.IndexOf("防渗") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("83"));
                            else if (zzlb.IndexOf("外墙保温") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("84"));
                            else if (zzlb.IndexOf("农村可再生能源") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("85"));
                            else
                                continue;

                        }
                        else if (zzxl.IndexOf("劳务分包") >= 0)
                        {
                            str.AppendFormat("<zzxl>{0}</zzxl>", xmlHelper.EncodeString("C"));
                            if (zzlb.IndexOf("木工") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("01"));
                            else if (zzlb.IndexOf("砌筑") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("02"));
                            else if (zzlb.IndexOf("抹灰") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("03"));
                            else if (zzlb.IndexOf("石制") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("04"));
                            else if (zzlb.IndexOf("油漆") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("05"));
                            else if (zzlb.IndexOf("钢筋") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("06"));
                            else if (zzlb.IndexOf("混凝土") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("07"));
                            else if (zzlb.IndexOf("脚手架") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("08"));
                            else if (zzlb.IndexOf("模板") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("09"));
                            else if (zzlb.IndexOf("焊接") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("10"));
                            else if (zzlb.IndexOf("水暖电安装") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("11"));
                            else if (zzlb.IndexOf("钣金") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("12"));
                            else if (zzlb.IndexOf("架线") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("13"));
                            else
                                continue;


                        }
                        else if (zzxl.IndexOf("设计施工一体化") >= 0)
                        {
                            str.AppendFormat("<zzxl>{0}</zzxl>", xmlHelper.EncodeString("D"));
                            if (zzlb.IndexOf("建筑装饰装修") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("01"));
                            else if (zzlb.IndexOf("建筑幕墙") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("02"));
                            else if (zzlb.IndexOf("建筑智能化") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("03"));
                            else if (zzlb.IndexOf("消防设施") >= 0)
                                str.AppendFormat("<zzlb>{0}</zzlb>", xmlHelper.EncodeString("04"));
                            else
                                continue;
                        }
                        else
                        {

                            // str.AppendFormat("<zzxl>{0}</zzxl>", xmlHelper.EncodeString("无"));
                            continue;
                        }
                        #endregion

                        string zzdj = row["zzdj"].ToString2().Trim(new char[] { ' ', '\r', '\n' });
                        if (zzdj.IndexOf("特级") >= 0)
                            str.AppendFormat("<qybm>{0}</qybm>", xmlHelper.EncodeString("00"));
                        else if (zzdj.IndexOf("一级") >= 0)
                            str.AppendFormat("<qybm>{0}</qybm>", xmlHelper.EncodeString("01"));
                        else if (zzdj.IndexOf("二级") >= 0)
                            str.AppendFormat("<qybm>{0}</qybm>", xmlHelper.EncodeString("02"));
                        else if (zzdj.IndexOf("三级") >= 0)
                            str.AppendFormat("<qybm>{0}</qybm>", xmlHelper.EncodeString("03"));
                        else if (zzdj.IndexOf("不分") >= 0)
                            str.AppendFormat("<qybm>{0}</qybm>", xmlHelper.EncodeString("04"));

                        str.AppendFormat("<fzjg>{0}</fzjg>", xmlHelper.EncodeString(row["fzdw"].ToString2()));
                        str.AppendFormat("<zzjwrq>{0}</zzjwrq>", "");
                        str.AppendFormat("<zzzsrq>{0}</zzzsrq>", xmlHelper.EncodeString(row["fzrq"].ToString2()));
                        str.AppendFormat("<zsbh>{0}</zsbh>", xmlHelper.EncodeString(row["zsbh"].ToString2()));
                        str.AppendFormat("<sfszxzz>{0}</sfszxzz>", xmlHelper.EncodeString(row["sfzzz"].ToString2()));
                        str.AppendFormat("<cbfw>{0}</cbfw>", "");

                        if (row["zsyxqrq"] == DBNull.Value || string.IsNullOrEmpty(row["zsyxqrq"].ToString2().Trim()))
                        {
                            str.AppendFormat("<yxksrq>{0}</yxksrq>", "");
                        }
                        else
                        {
                            str.AppendFormat("<yxksrq>{0}</yxksrq>", xmlHelper.EncodeString(row["zsyxqrq"].ToString2()));
                        }
                        if (row["zsyxzrq"] == DBNull.Value || string.IsNullOrEmpty(row["zsyxzrq"].ToString2().Trim()))
                        {
                            str.AppendFormat("<yxjsrq>{0}</yxjsrq>", "");
                        }
                        else
                        {
                            str.AppendFormat("<yxjsrq>{0}</yxjsrq>", xmlHelper.EncodeString(row["zsyxzrq"].ToString2()));
                        }
                        str.AppendFormat("<sfyx>{0}</sfyx>", xmlHelper.EncodeString("1"));

                        DataTable dt_saveLog = dataService.Get_SaveToStLog2("UEPP_Qyzzmx", row["ID"].ToString2());
                        DataRow row_saveLog;
                        if (dt_saveLog.Rows.Count == 0)
                        {
                            row_saveLog = dt_saveLog.NewRow();
                            dt_saveLog.Rows.Add(row_saveLog);
                            row_saveLog["PKID"] = row["ID"];
                            row_saveLog["TableName"] = "UEPP_Qyzzmx";
                            row_saveLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            row_saveLog = dt_saveLog.Rows[0];
                        }
                        row_saveLog["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        string msg = client.SaveStData(tableName, str.ToString(), userName, password, "U");
                        row_saveLog["SbToStMsg"] = msg;
                        if (msg.IndexOf("OK") >= 0)
                        {
                            row_saveLog["SbtoStState"] = 0;
                        }
                        else
                        {
                            row_saveLog["SbtoStState"] = 1;
                        }

                        dataService.Submit_SaveToStLog2(dt_saveLog);
                    }
                    catch (Exception ex)
                    {
                        Public.WriteLog("上报施工企业资质时出现异常:" + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Public.WriteLog("上报施工企业资质时出现异常:" + ex.Message + ex.StackTrace);
            }


            #endregion

            #region 注册建造师
            try
            {
                DataTable dt_qyry_zcjzs = dataService.Get_uepp_Qyry_zcjzs();
                foreach (DataRow row in dt_qyry_zcjzs.Rows)
                {
                    try
                    {
                        StringBuilder str = new StringBuilder();
                        SqlParameterCollection sp_ry = dataService.DB.CreateSqlParameterCollection();
                        sp_ry.Add("@ryID", row["ryID"]);

                        str.AppendFormat("<rybm>{0}</rybm>", xmlHelper.EncodeString(row["zjhm"].ToString2()));
                        str.AppendFormat("<xm>{0}</xm>", xmlHelper.EncodeString(row["xm"].ToString2()));
                        if (row["xb"].ToString2().Equals("男", StringComparison.CurrentCultureIgnoreCase))
                            str.AppendFormat("<xb>{0}</xb>", xmlHelper.EncodeString("1"));
                        else
                            str.AppendFormat("<xb>{0}</xb>", xmlHelper.EncodeString("0"));

                        DateTime time;
                        if (!string.IsNullOrEmpty(row["csrq"].ToString2()) && DateTime.TryParse(row["csrq"].ToString2(), out time))
                        {
                            str.AppendFormat("<csrq>{0}</csrq>", xmlHelper.EncodeString(row["csrq"].ToString2()));
                        }
                        else
                        {
                            str.AppendFormat("<csrq>{0}</csrq>", xmlHelper.EncodeString("1900-1-1"));
                        }

                        str.AppendFormat("<sfzh>{0}</sfzh>", xmlHelper.EncodeString(row["zjhm"].ToString2()));

                        string zjlx = row["zjlx"].ToString2();
                        if (zjlx.IndexOf("身份证") >= 0)
                            str.AppendFormat("<zjlx>{0}</zjlx>", xmlHelper.EncodeString("00"));
                        else if (zjlx.IndexOf("护照") >= 0)
                            str.AppendFormat("<zjlx>{0}</zjlx>", xmlHelper.EncodeString("01"));
                        else if (zjlx.IndexOf("军官证") >= 0)
                            str.AppendFormat("<zjlx>{0}</zjlx>", xmlHelper.EncodeString("02"));
                        else if (zjlx.IndexOf("武警警官证") >= 0)
                            str.AppendFormat("<zjlx>{0}</zjlx>", xmlHelper.EncodeString("03"));
                        else if (zjlx.IndexOf("士兵证") >= 0)
                            str.AppendFormat("<zjlx>{0}</zjlx>", xmlHelper.EncodeString("04"));
                        else if (zjlx.IndexOf("军队学员证") >= 0)
                            str.AppendFormat("<zjlx>{0}</zjlx>", xmlHelper.EncodeString("05"));
                        else if (zjlx.IndexOf("外国人居留证") >= 0)
                            str.AppendFormat("<zjlx>{0}</zjlx>", xmlHelper.EncodeString("06"));

                        string xl = row["xl"].ToString2();
                        if (xl.IndexOf("本科") >= 0)
                            str.AppendFormat("<zgxl>{0}</zgxl>", xmlHelper.EncodeString("01"));
                        else if (xl.IndexOf("大专") >= 0)
                            str.AppendFormat("<zgxl>{0}</zgxl>", xmlHelper.EncodeString("02"));
                        else if (xl.IndexOf("中专") >= 0)
                            str.AppendFormat("<zgxl>{0}</zgxl>", xmlHelper.EncodeString("03"));
                        else if (xl.IndexOf("硕士") >= 0)
                            str.AppendFormat("<zgxl>{0}</zgxl>", xmlHelper.EncodeString("04"));
                        else if (xl.IndexOf("博士") >= 0)
                            str.AppendFormat("<zgxl>{0}</zgxl>", xmlHelper.EncodeString("05"));
                        else if (xl.IndexOf("初中") >= 0)
                            str.AppendFormat("<zgxl>{0}</zgxl>", xmlHelper.EncodeString("06"));
                        else if (xl.IndexOf("小学") >= 0)
                            str.AppendFormat("<zgxl>{0}</zgxl>", xmlHelper.EncodeString("07"));
                        else if (xl.IndexOf("高中") >= 0)
                            str.AppendFormat("<zgxl>{0}</zgxl>", xmlHelper.EncodeString("08"));
                        else
                            str.AppendFormat("<zgxl>{0}</zgxl>", "");

                        if (String.IsNullOrEmpty(row["sxzy"].ToString2()))
                            str.AppendFormat("<sxzy>{0}</sxzy>", xmlHelper.EncodeString("无"));
                        else
                            str.AppendFormat("<sxzy>{0}</sxzy>", xmlHelper.EncodeString(row["sxzy"].ToString2()));

                        str.AppendFormat("<zc>{0}</zc>", xmlHelper.EncodeString(row["zc"].ToString2()));
                        str.AppendFormat("<zw>{0}</zw>", xmlHelper.EncodeString(row["zw"].ToString2()));

                        str.AppendFormat("<sf>{0}</sf>", "");
                        str.AppendFormat("<sz>{0}</sz>", "");
                        str.AppendFormat("<xq>{0}</xq>", "");
                        str.AppendFormat("<cjgzsj>{0}</cjgzsj>", "");
                        str.AppendFormat("<sgglnx>{0}</sgglnx>", xmlHelper.EncodeString(row["csgzjsnx"].ToString2()));
                        str.AppendFormat("<xcszy>{0}</xcszy>", xmlHelper.EncodeString(row["zczy"].ToString2()));
                        str.AppendFormat("<sgglgzjl>{0}</sgglgzjl>", xmlHelper.EncodeString(row["gzjl"].ToString2()));

                        DataTable dt_zyzgmc = dataService.DB.ExeSqlForDataTable("select distinct ryzyzglx from UEPP_Ryzyzg where  (DataState <>-1 or datastate is null) and ryId=@ryID ", sp_ry, "dt");
                        string zyzgmc = "";
                        foreach (DataRow item in dt_zyzgmc.Rows)
                        {
                            zyzgmc += item["ryzyzglx"] + ",";
                        }

                        str.AppendFormat("<zyzgmc>{0}</zyzgmc>", xmlHelper.EncodeString(zyzgmc.TrimEnd(',')));

                        string zzdj = row["ryzyzglxid"].ToString2();
                        if (zzdj.Equals("1"))
                        {
                            str.AppendFormat("<zzdj>{0}</zzdj>", xmlHelper.EncodeString("01"));
                            str.AppendFormat("<zcdj>{0}</zcdj>", xmlHelper.EncodeString("01"));
                        }
                        else
                        {
                            str.AppendFormat("<zzdj>{0}</zzdj>", xmlHelper.EncodeString("02"));
                            str.AppendFormat("<zcdj>{0}</zcdj>", xmlHelper.EncodeString("02"));
                        }
                        string zch = row["zsbh"].ToString2();
                        if (string.IsNullOrEmpty(zch))
                        {
                            str.AppendFormat("<zch>{0}</zch>", xmlHelper.EncodeString("无"));
                            str.AppendFormat("<zyyzh>{0}</zyyzh>", xmlHelper.EncodeString("无"));
                        }
                        else
                        {
                            str.AppendFormat("<zch>{0}</zch>", xmlHelper.EncodeString(zch));
                            str.AppendFormat("<zyyzh>{0}</zyyzh>", xmlHelper.EncodeString(zch));
                        }

                        str.AppendFormat("<zyfw>{0}</zyfw>", "");
                        str.AppendFormat("<zczy_zx>{0}</zczy_zx>", "");

                        str.AppendFormat("<qymc>{0}</qymc>", xmlHelper.EncodeString(row["zydw"].ToString2()));
                        str.AppendFormat("<qybm>{0}</qybm>", xmlHelper.EncodeString(row["zzjgdm"].ToString2()));
                        //str.AppendFormat("<yxksrq>{0}</yxksrq>", xmlHelper.EncodeString(row["zsyxqrq"].ToString2()));
                        //str.AppendFormat("<yxjsrq>{0}</yxjsrq>", xmlHelper.EncodeString(row["zsyxzrq"].ToString2()));

                        if (!string.IsNullOrEmpty(row["zsyxqrq"].ToString2()) && DateTime.TryParse(row["zsyxqrq"].ToString2(), out time))
                        {
                            str.AppendFormat("<yxksrq>{0}</yxksrq>", xmlHelper.EncodeString(time.ToString("yyyy-MM-dd")));
                        }
                        else
                        {
                            str.AppendFormat("<yxksrq>{0}</yxksrq>", xmlHelper.EncodeString("1900-1-1"));
                        }

                        if (!string.IsNullOrEmpty(row["zsyxzrq"].ToString2()) && DateTime.TryParse(row["zsyxzrq"].ToString2(), out time))
                        {
                            str.AppendFormat("<yxjsrq>{0}</yxjsrq>", xmlHelper.EncodeString(time.ToString("yyyy-MM-dd")));
                        }
                        else
                        {
                            str.AppendFormat("<yxjsrq>{0}</yxjsrq>", xmlHelper.EncodeString("1900-1-1"));
                        }

                        if (row["zsyxzrq"] != DBNull.Value && DateTime.Parse(row["zsyxzrq"].ToString()) <= DateTime.Now)
                        {
                            str.AppendFormat("<zszt>{0}</zszt>", xmlHelper.EncodeString("1"));
                        }
                        else
                        {
                            str.AppendFormat("<zszt>{0}</zszt>", xmlHelper.EncodeString("0"));
                        }

                        str.AppendFormat("<fzsj>{0}</fzsj>", xmlHelper.EncodeString(row["fzrq"].ToString2()));
                        str.AppendFormat("<fzjg>{0}</fzjg>", xmlHelper.EncodeString(row["fzdw"].ToString2()));
                        str.AppendFormat("<clbz>{0}</clbz>", xmlHelper.EncodeString("1"));
                        str.AppendFormat("<sfslsjzs>{0}</sfslsjzs>", xmlHelper.EncodeString("0"));
                        str.AppendFormat("<yddh>{0}</yddh>", xmlHelper.EncodeString(row["yddh"].ToString2()));

                        DataTable dt_saveLog = dataService.Get_SaveToStLog2("UEPP_Ryjbxx", row["ryID"].ToString2());
                        DataRow row_saveLog;
                        if (dt_saveLog.Rows.Count == 0)
                        {
                            row_saveLog = dt_saveLog.NewRow();
                            dt_saveLog.Rows.Add(row_saveLog);
                            row_saveLog["TableName"] = "UEPP_Ryjbxx";
                            row_saveLog["PKID"] = row["ryID"];
                            row_saveLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            row_saveLog = dt_saveLog.Rows[0];
                        }
                        row_saveLog["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        if (row["sfzsmj"] == DBNull.Value)
                        {
                            continue;
                        }
                        byte[] bytes = row["sfzsmj"] as byte[];


                        string msg = "";

                        try
                        {
                            msg = client.SaveStData("ry_xmjl", xmlHelper.EncodeString(string.Format("<sfzh>{0}</sfzh>", row["ryID"].ToString2())), userName_qyry, password_qyry, "D");
                        }
                        catch
                        {

                        }
                        if (row["AJ_IsRefuse"] != DBNull.Value &&
                            (row["AJ_IsRefuse"].ToString().Equals("1") || row["AJ_IsRefuse"].ToString().ToLower().Equals("true", StringComparison.CurrentCultureIgnoreCase)))
                        {
                            row_saveLog["SbToStMsg"] = msg;
                            if (msg.IndexOf("成功") >= 0)
                            {
                                row_saveLog["SbToStState"] = 0;
                            }
                            else
                            {
                                row_saveLog["SbToStState"] = 1;
                                Public.WriteLog("删除注册建造师失败:" + row["ryID"] + msg);
                            }
                        }
                        else
                        {
                            string returnMsg = "";
                            try
                            {
                                //returnMsg = client.saveZcjzs(str.ToString(), row["ryID"].ToString2(), bytes, "bmp", userName_qyry, password_qyry);
                            }
                            catch (Exception ex)
                            {
                                Public.WriteLog("上报注册建造师时saveZcjzs出现异常:ryID:" + row["ryID"] + ",Message:" + ex.Message);
                                returnMsg = ex.Message;
                            }

                            if (returnMsg.IndexOf("OK") >= 0)
                            {
                                row_saveLog["SbToStState"] = 0;
                                row_saveLog["SbToStMsg"] = returnMsg;
                            }
                            else
                            {
                                row_saveLog["SbToStState"] = 1;
                                row_saveLog["SbToStMsg"] = msg + ";" + returnMsg;
                                Public.WriteLog("上报注册建造师失败:" + row["ryID"] + msg + ";" + returnMsg);
                            }

                        }

                        dataService.Submit_SaveToStLog2(dt_saveLog);

                    }
                    catch (Exception ex)
                    {
                        Public.WriteLog("上报注册建造师时出现异常:ryID:" + row["ryID"] + ",Message:" + ex.Message + ex.StackTrace);
                    }
                }
            }
            catch (Exception ex)
            {
                Public.WriteLog("上报注册建造师时出现异常:" + ex.Message + ex.StackTrace);
            }
            #endregion




        }

        #endregion

        #region 公共方法
        private Qyxx getCsywlx(string certType)
        {
            Qyxx result = new Qyxx();
            string csywlxID = string.Empty, csywlx = string.Empty;
            switch (certType)
            {
                //施工
                case "建筑业":
                    csywlxID = "1";
                    csywlx = "建筑施工";
                    break;
                case "城市园林绿化":
                    csywlxID = "3";
                    csywlx = "园林绿化";
                    break;
                case "设计与施工一体化":
                    csywlxID = "2";
                    csywlx = "设计施工一体化";
                    break;
                case "房屋拆迁":
                    csywlxID = "13";
                    csywlx = "房屋拆迁";
                    break;
                case "安全生产许可证":
                    csywlxID = "14";
                    csywlx = "安全生产许可证";
                    break;
                //勘察
                case "工程勘察":
                    csywlxID = "5";
                    csywlx = "工程勘察";
                    break;
                //设计
                case "工程设计":
                    csywlxID = "6";
                    csywlx = "工程设计";
                    break;
                case "城市规划":
                    csywlxID = "18";
                    csywlx = "城市规划";
                    break;
                case "外商城市规划":
                    csywlxID = "19";
                    csywlx = "外商城市规划";
                    break;

                //中介机构
                case "工程招标代理":
                    csywlxID = "7";
                    csywlx = "招标代理";
                    break;
                case "工程监理":
                    csywlxID = "4";
                    csywlx = "工程监理";
                    break;
                case "工程造价咨询":
                    csywlxID = "8";
                    csywlx = "造价咨询";
                    break;
                case "工程质量检测":
                    csywlxID = "9";
                    csywlx = "工程检测";
                    break;
                case "施工图审查":
                    csywlxID = "15";
                    csywlx = "施工图审查";
                    break;
                case "房地产估价":
                    csywlxID = "16";
                    csywlx = "房地产估价";
                    break;
                case "物业服务":
                    csywlxID = "17";
                    csywlx = "物业服务";
                    break;
                default:
                    break;
            }
            result.csywlx = csywlx;
            result.csywlxID = csywlxID;
            return result;
        }

        private Qyxx getZslx(string certType)
        {
            Qyxx result = new Qyxx();
            switch (certType)
            {
                //施工
                case "建筑业":
                    result.zslxID = "10";
                    result.zslx = "建筑业资质证";
                    break;
                case "城市园林绿化":
                    result.zslxID = "30";
                    result.zslx = "园林绿化资质证";
                    break;
                case "设计与施工一体化":
                    result.zslxID = "20";
                    result.zslx = "设计施工一体化资质证";
                    break;
                case "房屋拆迁":
                    result.zslxID = "130";
                    result.zslx = "房屋拆迁资质证";
                    break;
                case "安全生产许可证":
                    result.zslxID = "140";
                    result.zslx = "安全生产许可证";
                    break;
                //勘察
                case "工程勘察":
                    result.zslxID = "51";
                    result.zslx = "省工程勘察资质证";
                    break;
                //设计
                case "工程设计":
                    result.zslxID = "61";
                    result.zslx = "省工程设计资质证";
                    break;
                case "城市规划":
                    result.zslxID = "18";
                    result.zslx = "城市规划资质证";
                    break;
                case "外商城市规划":
                    result.zslxID = "19";
                    result.zslx = "外商城市规划资质证";
                    break;

                //中介机构
                case "工程招标代理":
                    result.zslxID = "70";
                    result.zslx = "招标代理资质证";
                    break;
                case "工程监理":
                    result.zslxID = "40";
                    result.zslx = "工程监理资质证";
                    break;
                case "工程造价咨询":
                    result.zslxID = "80";
                    result.zslx = "造价咨询资质证";
                    //result.csywlx = "造价咨询资质证";
                    break;
                case "工程质量检测":
                    result.zslxID = "90";
                    result.zslx = "工程检测资质证";
                    break;
                case "施工图审查":
                    result.zslxID = "150";
                    result.zslx = "施工图审查资质证";
                    //result.csywlx = "施工图审查资质证";
                    break;
                case "房地产估价":
                    result.zslxID = "160";
                    result.zslx = "房地产估价资质证";
                    break;
                case "物业服务":
                    result.zslxID = "170";
                    result.zslx = "物业服务资质证";
                    break;
                default:
                    break;
            }
            return result;
        }

        #endregion
        

    }

    public enum Tag
    {
        江苏建设公共基础数据平台 = 0,
        局一号通系统 = 1,
        无锡市建设工程安全监督站 = 2,
        无锡市勘察设计行业信息管理系统 = 3,
        省一体化平台 = 4,
        省施工许可系统 = 5,
        省竣工备案系统 = 6,
        省质监系统 = 7,
        省勘察设计系统 = 8



    }

    public enum DataFlow
    {
        省一体化平台到无锡数据中心 = 0, 省审图系统到无锡数据中心 = 1, 省施工许可系统到无锡数据中心 = 2, 省质监系统到无锡数据中心 = 3, 省竣工备案系统到无锡数据中心 = 4,
        局一号通系统到无锡数据中心 = 5, 市勘察设计系统到无锡数据中心 = 6, 市招投标系统到无锡数据中心 = 7, 江阴市招投标系统到无锡数据中心 = 8, 宜兴市招投标系统到无锡数据中心 = 9, 市安监系统到无锡数据中心 = 10, 市质监系统到无锡数据中心 = 11, 信用考评系统到无锡数据中心 = 12, 惠山区到无锡数据中心 = 13, 滨湖区到无锡数据中心 = 14, 市中心四平台到无锡数据中心 = 15,
        无锡数据中心到市勘察设计系统 = 16, 无锡数据中心到市招投标系统 = 17, 无锡数据中心到江阴市招投标系统 = 18, 无锡数据中心到宜兴市招投标系统 = 19, 无锡数据中心到市安监系统 = 20, 无锡数据中心到市质监系统 = 21, 无锡数据中心到信用考评系统 = 22, 无锡数据中心到惠山区 = 23, 无锡数据中心到滨湖区 = 24, 无锡数据中心到市中心四平台 = 25, 江苏建设公共基础数据平台到无锡数据中心 = 26, 无锡数据中心到省一体化平台 = 27, 省勘察设计系统到无锡数据中心 = 28
    }
}
