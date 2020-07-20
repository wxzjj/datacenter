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
using System.Threading;
using WxjzgcjczyTimerService.model;

namespace WxjzgcjczyTimerService
{
    partial class Service3 : ServiceBase
    {
        public Service3()
        {
            InitializeComponent();
        }

        System.Timers.Timer myTimer;
        int timeSpan = 1;
        DataService dataService = new DataService();
        XmlHelper xmlHelper = new XmlHelper();
        public bool isRunning;
        public object obj = "111";

        const string fileName_New = "往市一中心四平台";

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
                List<string> setTimes = ConfigurationManager.AppSettings["setTime_ToSzxspt"].ToString().Split(',').ToList();
                int f = 0;
                for (int i = 0; i < setTimes.Count; i++)
                {
                    int hour = setTimes[i].Substring(0, 2).ToInt32();
                    int minute = setTimes[i].Substring(2, 2).ToInt32();

                    if (DateTime.Now.Hour == hour && DateTime.Now.Minute < minute + timeSpan && DateTime.Now.Minute >= minute)
                    {
                        if (isRunning)
                        {
                            WriteLog_New(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "上一次服务正在运行中。。。");
                            WriteLog_New("结束记录日志:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
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
                    WriteLog_New("开始记录日志:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")); //往市一中心四平台推送人员项目数据---2016.9.18


                    #region 推送数据
                    YourTask_PushDataToSyzxspt_Ryxmjbxx(); //往市一中心四平台推送人员项目数据---2016.9.18
                    WriteLog_New("\r\n");

                    #endregion

                    lock (obj)
                    {
                        isRunning = false;
                    }
                    WriteLog_New("结束记录日志:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")); //往市一中心四平台推送人员项目数据---2016.9.18

                }
            }
            catch (Exception ex)
            {
                lock (obj)
                {
                    isRunning = false;
                }
                WriteLog_New(ex.Message);
            }
        }


        /// <summary>
        /// 新增-2016.9.14 往市一中心四平台推送人员项目数据
        /// </summary>
        /// <param name="dt_DataJkDataDetail"></param>
        /// <param name="Id_DataJkLog"></param>
        /// <param name="Id_DataJkDataDetail"></param>
        void YourTask_PushDataToSyzxspt_Ryxmjbxx()
        {
            string apiMessage = string.Empty;
            //string methodMessage = string.Empty;
            DataService dataService = new DataService();
            DataTable dtapizb = dataService.Get_API_zb_apiFlow("25");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                try
                {

                    //往数据监控日志表添加一条记录
                    DataTable dt_DataJkLog = dataService.GetSchema_DataJkLog();
                    DataRow row_DataJkLog = dt_DataJkLog.NewRow();
                    dt_DataJkLog.Rows.Add(row_DataJkLog);
                    row_DataJkLog["ID"] = dataService.Get_DataJkLogNewID();
                    row_DataJkLog["DataFlow"] = DataFlow.无锡数据中心到市中心四平台.ToInt32();
                    row_DataJkLog["DataFlowName"] = DataFlow.无锡数据中心到市中心四平台.ToString();
                    row_DataJkLog["ServiceUrl"] = "";
                    row_DataJkLog["csTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dataService.Submit_DataJkLog(dt_DataJkLog);

                    //往数据监控日志表项添加一条记录
                    //DataTable dt_DataJkDataDetail = dataService.GetSchema_DataJkDataDetail();
                    //long Id_DataJkDataDetail = dataService.Get_DataJkDataDetailNewID().ToInt64();

                    DateTime beginTime = DateTime.Now;
                    WriteLog_New("开始执行YourTask_PushDataToSyzxspt_Ryxmjbxx任务:" + beginTime.ToString("yyyy-MM-dd HH:mm:ss"));

                    if (ConfigurationManager.AppSettings["IsPush_certifperson"].ToString2() == "1")   //注册执业人员
                        UploadToSzx_CertifPerson(row_DataJkLog["ID"].ToString2());  //注册执业人员
                    //Execute(UploadToSzx_CertifPerson, row_DataJkLog["ID"].ToString2(),TimeSpan.FromSeconds(6000),3);

                    if (ConfigurationManager.AppSettings["IsPush_completeacc"].ToString2() == "1")   //竣工验收备案信息
                        UploadToSzx_CompleteAcc(row_DataJkLog["ID"].ToString2());  //竣工验收备案信息

                    if (ConfigurationManager.AppSettings["IsPush_constrlicence"].ToString2() == "1")   //建设工程施工许可证发放信息
                        UploadToSzx_Constrlicence(row_DataJkLog["ID"].ToString2());  //建设工程施工许可证发放信息

                    if (ConfigurationManager.AppSettings["IsPush_constrprojectinfo"].ToString2() == "1")   //工程项目基本信息
                        UploadToSzx_ConstrProjectInfo(row_DataJkLog["ID"].ToString2());  //工程项目基本信息

                    if (ConfigurationManager.AppSettings["IsPush_engbidinfo"].ToString2() == "1")   //工程招标中标信息
                        UploadToSzx_EngBidInfo(row_DataJkLog["ID"].ToString2());  //工程招标中标信息

                    if (ConfigurationManager.AppSettings["IsPush_profadmper"].ToString2() == "1")   //专业岗位管理人员
                        UploadToSzx_ProFadmper(row_DataJkLog["ID"].ToString2());  //专业岗位管理人员

                    if (ConfigurationManager.AppSettings["IsPush_safeprodadmper"].ToString2() == "1")   //安全生产管理人员
                        UploadToSzx_SafeProdadmper(row_DataJkLog["ID"].ToString2());  //安全生产管理人员

                    DateTime endTime = DateTime.Now;
                    TimeSpan span1 = new TimeSpan(beginTime.Hour, beginTime.Minute, beginTime.Second);
                    TimeSpan span2 = new TimeSpan(endTime.Hour, endTime.Minute, endTime.Second);
                    TimeSpan span = span2 - span1;

                    WriteLog_New(string.Format("结束YourTask_PushDataToSyzxspt_Ryxmjbxx任务:{0}，历时：{1}时{2}分{3}秒", endTime.ToString("yyyy-MM-dd HH:mm:ss"), span.Hours, span.Minutes, span.Seconds));
                }
                catch (Exception ex)
                {
                    WriteLog_New("执行YourTask_PushDataToSyzxspt_Ryxmjbxx方法出现异常:" + ex.Message);
                    apiMessage += ex.Message;
                }

                finally
                {
                    DataTable dtapicb = dataService.GetSchema_API_cb();
                    DataRow row_apicb = dtapicb.NewRow();
                    dtapicb.Rows.Add(row_apicb);
                    row_apicb["apiCbID"] = dataService.Get_apiCbNewID();
                    row_apicb["apiFlow"] = "25";
                    row_apicb["apiMethod"] = "CertifPerson;CompleteAcc;Constrlicence;ConstrProjectInfo;EngBidInfo;ProFadmper;SafeProdadmper";
                    row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                    row_apicb["apiDyMessage"] = apiMessage;
                    row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dataService.Submit_API_cb(dtapicb);

                    dataService.UpdateZbJkzt("25", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

                }
            }


        }

        #region 往市一中心四平台推送人员项目数据

        //public void test()
        //{
        //    try
        //    {
        //        string str = "[{\"RYXM\":\"史海全\",\"SFZH\":\"61040319780812007X\",\"ZS\":\"苏建安B(2014)0200551\",\"ZYZGLX\":\"注册建造师\",\"DWMC\":\"江苏景苑绿化建设有限公司\"},{\"RYXM\":\"王聿骏\",\"SFZH\":\"320223197809266172\",\"ZSBM\":\"苏建安C（2008）0200516\",\"ZYZGLX\":\"注册建造师\",\"DWMC\":\"江苏苏禾建设发展有限公司\"}]";
        //        string resultSt = Contact(str, "http://58.215.18.222:8008/zhujianju_dpl1/certif_person");
        //        WriteLog_New(resultSt);
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLog_New(ex.Message);
        //    }
        //}


        /// <summary>
        /// 注册执业人员
        /// </summary>
        /// <param name="dt_DataJkDataDetail"></param>
        /// <param name="Id_DataJkLog"></param>
        /// <param name="Id_DataJkDataDetail"></param>
        public void UploadToSzx_CertifPerson(string Id_DataJkLog)
        {
            DataTable dt_DataJkDataDetail = dataService.GetSchema_DataJkDataDetail();
            DataRow row_DataJkDataDetail = dt_DataJkDataDetail.NewRow();
            dt_DataJkDataDetail.Rows.Add(row_DataJkDataDetail);

            row_DataJkDataDetail["DataJkLogID"] = Id_DataJkLog;
            row_DataJkDataDetail["tableName"] = "uepp_ryjbxx_zczyry_szxspt";
            row_DataJkDataDetail["MethodName"] = "UploadToSzx_CertifPerson";
            row_DataJkDataDetail["bz"] = "往市中心四平台推送注册执业人员";
            int all_count = 0, success_count = 0;

            DataTable dt = dataService.Get_CertifPerson();

            WriteLog_New("获取了 " + dt.Rows.Count + " 条uepp_ryjbxx_zczyry_szxspt数据！");
            all_count = dt.Rows.Count;
            if (dt.Rows.Count == 0)
            {
                return;
            }

            #region 保存至市一中心四平台（uepp_ryjbxx_zczyry_szxspt）
            try
            {
                String url = "http://2.20.101.170:8000/zhujianju_dpl/certif_person";
                sendToSzxspt(url, dt, ref success_count);

                WriteLog_New("总条数" + all_count + ",上报了 " + success_count + " 条uepp_ryjbxx_zczyry_szxspt数据！");
                /**
                string json = "";

                DataRow row;
                foreach (DataRow dataRow in dt.Rows)
                {
                    string PK = dataRow["PKID"].ToString();
                    json = ToJson(dataRow);
                    string resultSt = Contact(json, "http://2.20.101.170:8000/zhujianju_dpl/certif_person");
                    WriteLog_New("注册执业人员 resultSt " + resultSt);
                    DataTable dt_log_TBProjectInfo = dataService.GetTBData_SaveToStLog("uepp_ryjbxx_zczyry_szxspt", PK);

                    if (dt_log_TBProjectInfo.Rows.Count > 0)
                    {
                        row = dt_log_TBProjectInfo.Rows[0];
                        row["TableName"] = "uepp_ryjbxx_zczyry_szxspt";
                        row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                    }
                    else
                    {
                        row = dt_log_TBProjectInfo.NewRow();
                        dt_log_TBProjectInfo.Rows.Add(row);
                        row["PKID"] = PK;
                        row["TableName"] = "uepp_ryjbxx_zczyry_szxspt";
                        row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    all_count++;

                    if (!resultSt.Contains("成功"))
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
            */

            }
            catch (Exception ex)
            {
                row_DataJkDataDetail["allCount"] = all_count;
                row_DataJkDataDetail["successCount"] = success_count;
                row_DataJkDataDetail["IsOk"] = 0;
                row_DataJkDataDetail["ErrorMsg"] = ex.Message;
            }
            finally
            {
                /**
                long Id_DataJkDataDetail = dataService.Get_DataJkDataDetailNewID().ToInt64();
                row_DataJkDataDetail["ID"] = Id_DataJkDataDetail;
                if (dt_DataJkDataDetail.Rows.Count > 0)
                {
                    dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail);
                }*/
            }

            #endregion

        }


        /// <summary>
        /// 竣工验收备案信息
        /// </summary>
        /// <param name="dt_DataJkDataDetail"></param>
        /// <param name="Id_DataJkLog"></param>
        /// <param name="Id_DataJkDataDetail"></param>
        public void UploadToSzx_CompleteAcc(string Id_DataJkLog)
        {
            DataTable dt_DataJkDataDetail = dataService.GetSchema_DataJkDataDetail();
            DataRow row_DataJkDataDetail = dt_DataJkDataDetail.NewRow();
            dt_DataJkDataDetail.Rows.Add(row_DataJkDataDetail);


            row_DataJkDataDetail["DataJkLogID"] = Id_DataJkLog;
            row_DataJkDataDetail["tableName"] = "TBProjectFinishManage_szxspt";
            row_DataJkDataDetail["MethodName"] = "UploadToSzx_CompleteAcc";
            row_DataJkDataDetail["bz"] = "往市中心四平台推送竣工验收备案信息";
            int all_count = 0, success_count = 0;

            DataTable dt = dataService.Get_CompleteAcc();

            WriteLog_New("获取了 " + dt.Rows.Count + " 条TBProjectFinishManage_szxspt数据！");
            all_count = dt.Rows.Count;
            if (dt.Rows.Count == 0)
            {
                return;
            }

            #region 保存至市一中心四平台（TBProjectFinishManage_szxspt）
            try
            {
                String url = "http://2.20.101.170:8000/zhujianju_dpl/complete_acc";
                sendToSzxspt(url, dt, ref success_count);
                WriteLog_New("总条数" + all_count + ",上报了 " + success_count + " 条TBProjectFinishManage_szxspt数据！");
                /**
                foreach (DataRow dataRow in dt.Rows)
                {
                    string PK = dataRow["PKID"].ToString();
                    json = ToJson(dataRow);
                    string resultSt = Contact(json, "http://2.20.101.170:8000/zhujianju_dpl/complete_acc");
                    WriteLog_New("竣工验收备案信息 resultSt " + resultSt);

                    DataTable dt_log_TBProjectInfo = dataService.GetTBData_SaveToStLog("TBProjectFinishManage_szxspt", PK);

                    if (dt_log_TBProjectInfo.Rows.Count > 0)
                    {
                        row = dt_log_TBProjectInfo.Rows[0];
                        row["TableName"] = "TBProjectFinishManage_szxspt";
                        row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                    }
                    else
                    {
                        row = dt_log_TBProjectInfo.NewRow();
                        dt_log_TBProjectInfo.Rows.Add(row);
                        row["PKID"] = PK;
                        row["TableName"] = "TBProjectFinishManage_szxspt";
                        row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    all_count++;

                    if (!resultSt.Contains("成功"))
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
                */
            }
            catch (Exception ex)
            {
                row_DataJkDataDetail["allCount"] = all_count;
                row_DataJkDataDetail["successCount"] = success_count;
                row_DataJkDataDetail["IsOk"] = 0;
                row_DataJkDataDetail["ErrorMsg"] = ex.Message;
                WriteLog_New("error msg:" + ex.Message);
            }
            finally
            {
                /**
                long Id_DataJkDataDetail = dataService.Get_DataJkDataDetailNewID().ToInt64();
                row_DataJkDataDetail["ID"] = Id_DataJkDataDetail;
                if (dt_DataJkDataDetail.Rows.Count > 0)
                {
                    dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail);
                }*/
            }

            #endregion

        }

        

        /// <summary>
        /// 施工许可证发放信息
        /// </summary>
        /// <param name="dt_DataJkDataDetail"></param>
        /// <param name="Id_DataJkLog"></param>
        /// <param name="Id_DataJkDataDetail"></param>
        public void UploadToSzx_Constrlicence(string Id_DataJkLog)
        {
            DataTable dt_DataJkDataDetail = dataService.GetSchema_DataJkDataDetail();

            DataRow row_DataJkDataDetail = dt_DataJkDataDetail.NewRow();
            dt_DataJkDataDetail.Rows.Add(row_DataJkDataDetail);

            row_DataJkDataDetail["DataJkLogID"] = Id_DataJkLog;
            row_DataJkDataDetail["tableName"] = "TBBuilderLicenceManage_szxspt";
            row_DataJkDataDetail["MethodName"] = "UploadToSzx_Constrlicence";
            row_DataJkDataDetail["bz"] = "往市中心四平台推送建设工程施工许可证";
            int all_count = 0, success_count = 0;

            DataTable dt = dataService.Get_Constrlicence();

            WriteLog_New("获取了 " + dt.Rows.Count + " 条TBBuilderLicenceManage_szxspt数据！");
            all_count = dt.Rows.Count;
            if (dt.Rows.Count == 0)
            {
                return;
            }

            #region 保存至市一中心四平台（TBBuilderLicenceManage_szxspt）
            try
            {

                String url = "http://2.20.101.170:8000/zhujianju_dpl/constr_licence";
                sendToSzxspt(url, dt, ref success_count);

                WriteLog_New("总条数" + all_count + ",上报了 " + success_count + " 条TBBuilderLicenceManage_szxspt数据！");
                /**
                string json = "";

                DataRow row;
                foreach (DataRow dataRow in dt.Rows)
                {
                    string PK = dataRow["PKID"].ToString();
                    json = ToJson(dataRow);

                    string resultSt = Contact(json, "http://2.20.101.170:8000/zhujianju_dpl/constr_licence");
                    WriteLog_New("施工许可证发放信息 resultSt " + resultSt);
                    DataTable dt_log_TBProjectInfo = dataService.GetTBData_SaveToStLog("TBBuilderLicenceManage_szxspt", PK);

                    if (dt_log_TBProjectInfo.Rows.Count > 0)
                    {
                        row = dt_log_TBProjectInfo.Rows[0];
                        row["TableName"] = "TBBuilderLicenceManage_szxspt";
                        row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                    }
                    else
                    {
                        row = dt_log_TBProjectInfo.NewRow();
                        dt_log_TBProjectInfo.Rows.Add(row);
                        row["PKID"] = PK;
                        row["TableName"] = "TBBuilderLicenceManage_szxspt";
                        row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    all_count++;

                    if (!resultSt.Contains("成功"))
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
                 */

            }
            catch (Exception ex)
            {
                row_DataJkDataDetail["allCount"] = all_count;
                row_DataJkDataDetail["successCount"] = success_count;
                row_DataJkDataDetail["IsOk"] = 0;
                row_DataJkDataDetail["ErrorMsg"] = ex.Message;
            }
            finally
            {
                /**
                long Id_DataJkDataDetail = dataService.Get_DataJkDataDetailNewID().ToInt64();
                row_DataJkDataDetail["ID"] = Id_DataJkDataDetail;
                if (dt_DataJkDataDetail.Rows.Count > 0)
                {
                    dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail);
                }*/
            }

            #endregion

        }

        /// <summary>
        /// 工程项目基本信息
        /// </summary>
        /// <param name="dt_DataJkDataDetail"></param>
        /// <param name="Id_DataJkLog"></param>
        /// <param name="Id_DataJkDataDetail"></param>
        public void UploadToSzx_ConstrProjectInfo(string Id_DataJkLog)
        {
            DataTable dt_DataJkDataDetail = dataService.GetSchema_DataJkDataDetail();

            DataRow row_DataJkDataDetail = dt_DataJkDataDetail.NewRow();
            dt_DataJkDataDetail.Rows.Add(row_DataJkDataDetail);

            row_DataJkDataDetail["DataJkLogID"] = Id_DataJkLog;
            row_DataJkDataDetail["tableName"] = "TBProjectInfo_szxspt";
            row_DataJkDataDetail["MethodName"] = "UploadToSzx_ConstrProjectInfo";
            row_DataJkDataDetail["bz"] = "往市中心四平台推送工程项目基本信息";
            int all_count = 0, success_count = 0;

            DataTable dt = dataService.Get_ConstrProjectInfo();

            WriteLog_New("获取了 " + dt.Rows.Count + " 条TBProjectInfo_szxspt数据！");
            all_count = dt.Rows.Count;
            if (dt.Rows.Count == 0)
            {
                return;
            }

            #region 保存至市一中心四平台（TBProjectInfo_szxspt）
            try
            {
                String url = "http://2.20.101.170:8000/zhujianju_dpl/constr_project_info";
                sendToSzxspt(url, dt, ref success_count);

                WriteLog_New("总条数" + all_count + ",上报了 " + success_count + " 条TBProjectInfo_szxspt数据！");
                /**
                string json = "";

                DataRow row;
                foreach (DataRow dataRow in dt.Rows)
                {
                    string PK = dataRow["PKID"].ToString();
                    json = ToJson(dataRow);

                    string resultSt = Contact(json, "http://2.20.101.170:8000/zhujianju_dpl/constr_project_info");
                    WriteLog_New("工程项目基本信息 resultSt " + resultSt);
                    DataTable dt_log_TBProjectInfo = dataService.GetTBData_SaveToStLog("TBProjectInfo_szxspt", PK);

                    if (dt_log_TBProjectInfo.Rows.Count > 0)
                    {
                        row = dt_log_TBProjectInfo.Rows[0];
                        row["TableName"] = "TBProjectInfo_szxspt";
                        row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                    }
                    else
                    {
                        row = dt_log_TBProjectInfo.NewRow();
                        dt_log_TBProjectInfo.Rows.Add(row);
                        row["PKID"] = PK;
                        row["TableName"] = "TBProjectInfo_szxspt";
                        row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    all_count++;

                    if (!resultSt.Contains("成功"))
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
                */
            }
            catch (Exception ex)
            {
                row_DataJkDataDetail["allCount"] = all_count;
                row_DataJkDataDetail["successCount"] = success_count;
                row_DataJkDataDetail["IsOk"] = 0;
                row_DataJkDataDetail["ErrorMsg"] = ex.Message;
            }
            finally
            {   /**
                long Id_DataJkDataDetail = dataService.Get_DataJkDataDetailNewID().ToInt64();
                row_DataJkDataDetail["ID"] = Id_DataJkDataDetail;
                if (dt_DataJkDataDetail.Rows.Count > 0)
                {
                    dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail);
                }*/
            }
            #endregion

        }

        /// <summary>
        /// 工程招标中标信息
        /// </summary>
        /// <param name="dt_DataJkDataDetail"></param>
        /// <param name="Id_DataJkLog"></param>
        /// <param name="Id_DataJkDataDetail"></param>
        public void UploadToSzx_EngBidInfo(string Id_DataJkLog)
        {
            DataTable dt_DataJkDataDetail = dataService.GetSchema_DataJkDataDetail();

            DataRow row_DataJkDataDetail = dt_DataJkDataDetail.NewRow();
            dt_DataJkDataDetail.Rows.Add(row_DataJkDataDetail);

            row_DataJkDataDetail["DataJkLogID"] = Id_DataJkLog;
            row_DataJkDataDetail["tableName"] = "TBTenderInfo_szxspt";
            row_DataJkDataDetail["MethodName"] = "UploadToSzx_EngBidInfo";
            row_DataJkDataDetail["bz"] = "往市中心四平台推送工程招标中标信息";
            int all_count = 0, success_count = 0;

            DataTable dt = dataService.Get_EngBidInfo();

            WriteLog_New("获取了 " + dt.Rows.Count + " 条TBTenderInfo_szxspt数据！");
            all_count = dt.Rows.Count;
            if (dt.Rows.Count == 0)
            {
                return;
            }

            #region 保存至市一中心四平台（TBTenderInfo_szxspt）
            try
            {
                String url = "http://2.20.101.170:8000/zhujianju_dpl/eng_bid_info";
                sendToSzxspt(url, dt, ref success_count);

                WriteLog_New("总条数" + all_count + ",上报了 " + success_count + " 条TBTenderInfo_szxspt数据！");
                /**
                string json = "";

            

                DataRow row;
                foreach (DataRow dataRow in dt.Rows)
                {
                    string PK = dataRow["PKID"].ToString();
                    json = ToJson(dataRow);

                    string resultSt = Contact(json, "http://2.20.101.170:8000/zhujianju_dpl/eng_bid_info");
                    WriteLog_New("工程招标中标信息 resultSt " + resultSt);
                    DataTable dt_log_TBProjectInfo = dataService.GetTBData_SaveToStLog("TBTenderInfo_szxspt", PK);

                    if (dt_log_TBProjectInfo.Rows.Count > 0)
                    {
                        row = dt_log_TBProjectInfo.Rows[0];
                        row["TableName"] = "TBTenderInfo_szxspt";
                        row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                    }
                    else
                    {
                        row = dt_log_TBProjectInfo.NewRow();
                        dt_log_TBProjectInfo.Rows.Add(row);
                        row["PKID"] = PK;
                        row["TableName"] = "TBTenderInfo_szxspt";
                        row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    all_count++;

                    if (!resultSt.Contains("成功"))
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
                */
            }
            catch (Exception ex)
            {
                row_DataJkDataDetail["allCount"] = all_count;
                row_DataJkDataDetail["successCount"] = success_count;
                row_DataJkDataDetail["IsOk"] = 0;
                row_DataJkDataDetail["ErrorMsg"] = ex.Message;
            }
            finally
            {   /**
                long Id_DataJkDataDetail = dataService.Get_DataJkDataDetailNewID().ToInt64();
                row_DataJkDataDetail["ID"] = Id_DataJkDataDetail;
                if (dt_DataJkDataDetail.Rows.Count > 0)
                {
                    dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail);
                }*/
            }
            #endregion

        }

        /// <summary>
        /// 专业岗位管理人员
        /// </summary>
        /// <param name="dt_DataJkDataDetail"></param>
        /// <param name="Id_DataJkLog"></param>
        /// <param name="Id_DataJkDataDetail"></param>
        public void UploadToSzx_ProFadmper(string Id_DataJkLog)
        {
            DataTable dt_DataJkDataDetail = dataService.GetSchema_DataJkDataDetail();
            DataRow row_DataJkDataDetail = dt_DataJkDataDetail.NewRow();
            dt_DataJkDataDetail.Rows.Add(row_DataJkDataDetail);

            row_DataJkDataDetail["DataJkLogID"] = Id_DataJkLog;
            row_DataJkDataDetail["tableName"] = "uepp_ryjbxx_zygwglry_szxspt";
            row_DataJkDataDetail["MethodName"] = "UploadToSzx_ProFadmper";
            row_DataJkDataDetail["bz"] = "往市中心四平台推送专业岗位管理人员";
            int all_count = 0, success_count = 0;

            DataTable dt = dataService.Get_ProFadmper();

            WriteLog_New("获取了 " + dt.Rows.Count + " 条uepp_ryjbxx_zygwglry_szxspt数据！");
            all_count = dt.Rows.Count;
            if (dt.Rows.Count == 0)
            {
                return;
            }

            #region 保存至市一中心四平台（uepp_ryjbxx_zygwglry_szxspt）
            try
            {
                String url = "http://2.20.101.170:8000/zhujianju_dpl/prof_adm_per";
                sendToSzxspt(url, dt, ref success_count);

                WriteLog_New("总条数" + all_count + ",上报了 " + success_count + " 条uepp_ryjbxx_zygwglry_szxspt数据！");
                /**
                string json = "";

                DataRow row;
                foreach (DataRow dataRow in dt.Rows)
                {
                    string PK = dataRow["PKID"].ToString();
                    json = ToJson(dataRow);

                    string resultSt = Contact(json, "http://2.20.101.170:8000/zhujianju_dpl/prof_adm_per");
                    WriteLog_New("专业岗位管理人员 resultSt " + resultSt);
                    DataTable dt_log_TBProjectInfo = dataService.GetTBData_SaveToStLog("uepp_ryjbxx_zygwglry_szxspt", PK);

                    if (dt_log_TBProjectInfo.Rows.Count > 0)
                    {
                        row = dt_log_TBProjectInfo.Rows[0];
                        row["TableName"] = "uepp_ryjbxx_zygwglry_szxspt";
                        row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                    }
                    else
                    {
                        row = dt_log_TBProjectInfo.NewRow();
                        dt_log_TBProjectInfo.Rows.Add(row);
                        row["PKID"] = PK;
                        row["TableName"] = "uepp_ryjbxx_zygwglry_szxspt";
                        row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    all_count++;

                    if (!resultSt.Contains("成功"))
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
                */
            }
            catch (Exception ex)
            {
                row_DataJkDataDetail["allCount"] = all_count;
                row_DataJkDataDetail["successCount"] = success_count;
                row_DataJkDataDetail["IsOk"] = 0;
                row_DataJkDataDetail["ErrorMsg"] = ex.Message;
            }

            finally
            {   /**
                long Id_DataJkDataDetail = dataService.Get_DataJkDataDetailNewID().ToInt64();
                row_DataJkDataDetail["ID"] = Id_DataJkDataDetail;
                if (dt_DataJkDataDetail.Rows.Count > 0)
                {
                    dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail);
                }*/
            }
            #endregion

        }

        /// <summary>
        /// 安全生产管理人员
        /// </summary>
        /// <param name="dt_DataJkDataDetail"></param>
        /// <param name="Id_DataJkLog"></param>
        /// <param name="Id_DataJkDataDetail"></param>
        public void UploadToSzx_SafeProdadmper(string Id_DataJkLog)
        {
            DataTable dt_DataJkDataDetail = dataService.GetSchema_DataJkDataDetail();
            DataRow row_DataJkDataDetail = dt_DataJkDataDetail.NewRow();
            dt_DataJkDataDetail.Rows.Add(row_DataJkDataDetail);

            row_DataJkDataDetail["DataJkLogID"] = Id_DataJkLog;
            row_DataJkDataDetail["tableName"] = "uepp_ryjbxx_aqscglry_szxspt";
            row_DataJkDataDetail["MethodName"] = "UploadToSzx_SafeProdadmper";
            row_DataJkDataDetail["bz"] = "往市中心四平台推送安全生产管理人员";
            int all_count = 0, success_count = 0;

            DataTable dt = dataService.Get_SafeProdadmper();

            WriteLog_New("获取了 " + dt.Rows.Count + " 条uepp_ryjbxx_aqscglry_szxspt数据！");
            all_count = dt.Rows.Count;
            if (dt.Rows.Count == 0)
            {
                return;
            }

            #region 保存至市一中心四平台（uepp_ryjbxx_aqscglry_szxspt）
            try
            {
                String url = "http://2.20.101.170:8000/zhujianju_dpl/safe_prod_adm_per";
                sendToSzxspt(url, dt, ref success_count);

                WriteLog_New("总条数" + all_count + ",上报了 " + success_count + " 条uepp_ryjbxx_aqscglry_szxspt数据！");
                /**
                string json = "";

                DataRow row;
                foreach (DataRow dataRow in dt.Rows)
                {
                    string PK = dataRow["PKID"].ToString();
                    json = ToJson(dataRow);

                    string resultSt = Contact(json, "http://2.20.101.170:8000/zhujianju_dpl/safe_prod_adm_per");
                    WriteLog_New("安全生产管理人员 resultSt " + resultSt);
                    DataTable dt_log_TBProjectInfo = dataService.GetTBData_SaveToStLog("uepp_ryjbxx_aqscglry_szxspt", PK);

                    if (dt_log_TBProjectInfo.Rows.Count > 0)
                    {
                        row = dt_log_TBProjectInfo.Rows[0];
                        row["TableName"] = "uepp_ryjbxx_aqscglry_szxspt";
                        row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                    }
                    else
                    {
                        row = dt_log_TBProjectInfo.NewRow();
                        dt_log_TBProjectInfo.Rows.Add(row);
                        row["PKID"] = PK;
                        row["TableName"] = "uepp_ryjbxx_aqscglry_szxspt";
                        row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    all_count++;

                    if (!resultSt.Contains("成功"))
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
                */
            }
            catch (Exception ex)
            {
                row_DataJkDataDetail["allCount"] = all_count;
                row_DataJkDataDetail["successCount"] = success_count;
                row_DataJkDataDetail["IsOk"] = 0;
                row_DataJkDataDetail["ErrorMsg"] = ex.Message;
            }
            finally
            {   /**
                long Id_DataJkDataDetail = dataService.Get_DataJkDataDetailNewID().ToInt64();
                row_DataJkDataDetail["ID"] = Id_DataJkDataDetail;
                if (dt_DataJkDataDetail.Rows.Count > 0)
                {
                    dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail);
                }*/
            }
            #endregion

        }
        #endregion


        #region 公用方法
        public void WriteLog_New(string msg)   //2016.9.14 往市一中心四平台
        {
            Public.WriteLog(fileName_New, msg);
        }



        public static string Execute(Action<string> func, string arg1, TimeSpan retryInterval, int retryCount)
        {
            bool isNeedRetry = true;
            string returnstring = string.Empty;
            var exceptions = new List<Exception>();

            for (int retry = 0; retry < retryCount; retry++)
            {
                try
                {
                    if (isNeedRetry)
                    {
                        func(arg1);
                        isNeedRetry = false;
                    }
                }
                catch (Exception ex)
                {
                    isNeedRetry = true;
                    exceptions.Add(ex);
                    Thread.Sleep(retryInterval);
                }
            }

            foreach (Exception ex in exceptions)
            {
                returnstring += ex.Message + ";";
            }
            return returnstring.Trim(';');
        }



        public string ToJson(DataRow dataRow)  //2016.9.14 往市一中心四平台
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
            ArrayList arrayList = new ArrayList();

            Dictionary<string, object> dictionary = new Dictionary<string, object>();  //实例化一个参数集合
            foreach (DataColumn dataColumn in dataRow.Table.Columns)
            {
                if (dataColumn.ColumnName != "PKID")
                {
                    dictionary.Add(dataColumn.ColumnName, dataRow[dataColumn.ColumnName].ToString());
                }
            }
            arrayList.Add(dictionary); //ArrayList集合中添加键值


            return javaScriptSerializer.Serialize(arrayList);  //返回一个json字符串
        }

        public string DataTableToJson(DataTable dt)  
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
            ArrayList arrayList = new ArrayList();

            foreach (DataRow dataRow in dt.Rows)
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();  //实例化一个参数集合
                foreach (DataColumn dataColumn in dataRow.Table.Columns)
                {
                    if (dataColumn.ColumnName != "PKID")
                    {
                        dictionary.Add(dataColumn.ColumnName, dataRow[dataColumn.ColumnName].ToString());
                    }
                }
                arrayList.Add(dictionary); //ArrayList集合中添加键值
            }

            return javaScriptSerializer.Serialize(arrayList);  //返回一个json字符串
        }

        private void sendToSzxspt(String url, DataTable dt, ref int success_count)
        {
            string json = "";
            JavaScriptSerializer js = new JavaScriptSerializer();

            int recordNum = ConfigurationManager.AppSettings["RecordNum_ToSzxspt"].ToInt32();
            //WriteLog_New("recordNum:" + recordNum);

            DataTable sendDataTable = dt.Clone();
            sendDataTable.Clear();
            for (int i = 1; i <= dt.Rows.Count; i++)
            {
                sendDataTable.Rows.Add(dt.Rows[i - 1].ItemArray);

                if ((i % recordNum) == 0 || i == dt.Rows.Count)
                {
                    try
                    {
                        //每100个发送一次或者最后发送一次
                        json = DataTableToJson(sendDataTable);
                        string resultSt = Contact(json, url);
                        //WriteLog_New("resultSt" + resultSt);
                        ReturnBean model = js.Deserialize<ReturnBean>(resultSt);

                        // && 
                        if (model != null)
                        {
                            //all_count += model.size;
                            success_count += model.SuccessSize;
                            if (!"200000".Equals(model.code))
                            {
                                WriteLog_New(url + "\n" + json);
                                WriteLog_New("resultSt" + resultSt);
                            }

                        }
                        else
                        {
                            WriteLog_New(url + "return model null");
                        }
                    }
                    catch (Exception ex)
                    {

                        WriteLog_New(url + "|error msg:" + ex.Message);
                    }
                    finally
                    {
                        sendDataTable.Clear();
                    }

                }

            }
        }


        public string Contact(string content, string _url) //2016.9.14 往市一中心四平台
        {
            //WriteLog_New("json: " + content);
            HttpWebRequest request = null;
            Uri address = new Uri(_url);//拼接完整的url
            request = (HttpWebRequest)HttpWebRequest.Create(address);
            request.ContentType = "application/json";
            request.Method = "POST";
            string base64Credentials = GetEncodedCredentials();
            request.Headers.Add("Authorization", "Basic " + base64Credentials);
            try
            {
                byte[] bytes;
                bytes = Encoding.UTF8.GetBytes(content);
                int count = 0;
                request.ContentLength = bytes.Length;
                request.Timeout = 1000 * 300;
                using (Stream postStream = request.GetRequestStream())
                {
                    System.IO.MemoryStream buffStream = new MemoryStream(bytes);
                    byte[] buff = new byte[2048];
                    while ((count = buffStream.Read(buff, 0, buff.Length)) > 0)
                    {
                        postStream.Write(buff, 0, count);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog_New(ex.Message);
            }
            //获取返回信息.
            WebResponse response = request.GetResponse();
            StreamReader reader1 = new StreamReader(response.GetResponseStream());
            string temp_resultStr = reader1.ReadToEnd().Replace("<br>", "").Replace("\\", "");
            //temp_resultStr = temp_resultStr.Substring(1, temp_resultStr.Length - 2);
            return temp_resultStr;
        }
        
        private string GetEncodedCredentials()
        {
            string mergedCredentials = string.Format("{0}:{1}", "zjj_jk", "zjj_jk@2020");
            byte[] byteCredentials = UTF8Encoding.UTF8.GetBytes(mergedCredentials);
            return Convert.ToBase64String(byteCredentials);
        }
        #endregion


        protected override void OnStop()
        {
            if (myTimer != null)
                myTimer.Stop();
        }

    }
}
