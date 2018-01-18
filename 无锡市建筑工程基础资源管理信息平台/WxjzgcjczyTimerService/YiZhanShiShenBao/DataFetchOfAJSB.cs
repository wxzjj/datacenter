using System;
using System.Collections.Generic;
using WxsjzxTimerService.Common;
using System.Data;
using Bigdesk8;
using WxsjzxTimerService;
using WxjzgcjczyTimerService.model;

namespace WxjzgcjczyTimerService.YiZhanShiShenBao
{
    class DataFetchOfAJSB
    {
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



        // 定义一个静态变量来保存类的实例
        private static DataFetchOfAJSB uniqueInstance;

        // 定义一个标识确保线程同步
        private static readonly object locker = new object();
        private XmlHelper xmlHelper = new XmlHelper();
        private ShenBaoDataService dataService = new ShenBaoDataService();

        // 定义私有构造函数，使外界不能创建该类实例
        private DataFetchOfAJSB()
        {
        }

        /// <summary>
        /// 定义公有方法提供一个全局访问点,同时你也可以定义公有属性来提供全局访问点
        /// </summary>
        /// <returns></returns>
        public static DataFetchOfAJSB GetInstance()
        {
            // 当第一个线程运行到这里时，此时会对locker对象 "加锁"，
            // 当第二个线程运行该方法时，首先检测到locker对象为"加锁"状态，该线程就会挂起等待第一个线程解锁
            // lock语句运行完之后（即线程运行完之后）会对该对象"解锁"
            // 双重锁定只需要一句判断就可以了
            if (uniqueInstance == null)
            {
                lock (locker)
                {
                    // 如果类的实例不存在则创建，否则直接返回
                    if (uniqueInstance == null)
                    {
                        uniqueInstance = new DataFetchOfAJSB();
                    }
                }
            }
            return uniqueInstance;
        }

        public void YourTask_PullSBDataFromSythpt()
        {
            DateTime now = DateTime.Now;
            YourTask_PullAJSBDataFromSythpt(now);
            YourTask_PullZJSBDataFromSythpt(now);

            DataTable refetchDt = dataService.GetAp_need_refetch();
            DateTime pullDate;
            string ajResult = string.Empty;
            string zjResult = string.Empty;
            
            foreach (DataRow row in refetchDt.Rows)
            {
                Public.WriteLog("fetchDate: " + row["fetchDate"].ToString());
                DateTime.TryParse(row["fetchDate"].ToString(), out pullDate);

                Public.WriteLog("pullDate: " + pullDate);
                ajResult = YourTask_PullAJSBDataFromSythpt(pullDate);
                zjResult = YourTask_PullZJSBDataFromSythpt(pullDate);

                if (!string.IsNullOrEmpty(ajResult) && !string.IsNullOrEmpty(zjResult))
                {
                    row["status"] = 1;
                }
            }

            dataService.Submit_Ap_need_refetch(refetchDt);


            //处理需要重新下行数据的安监uuid列表
            DataTable ajUuidDt = dataService.GetAp_need_refetch_uuids("AJ");
            foreach (DataRow row in ajUuidDt.Rows)
            {
                Public.WriteLog("fetch aj uuid: " + row["uuid"].ToString());
                ajResult = YourTask_PullAJSBDataFromSythptByUUID(row["deptCode"].ToString(), row["password"].ToString(), row["uuid"].ToString()); 
                if(ajResult.Equals("success")){
                    dataService.Delete_Ap_need_refetch_uuid(row["uuid"].ToString(), "AJ");
                }  
            }
            //处理需要重新下行数据的质监uuid列表
            DataTable zjUuidDt = dataService.GetAp_need_refetch_uuids("ZJ");
            foreach (DataRow row in zjUuidDt.Rows)
            {
                Public.WriteLog("fetch zj uuid: " + row["uuid"].ToString());
                ajResult = YourTask_PullZJSBDataFromSythptByUUID(row["deptCode"].ToString(), row["password"].ToString(), row["uuid"].ToString());
                if (ajResult.Equals("success"))
                {
                    dataService.Delete_Ap_need_refetch_uuid(row["uuid"].ToString(), "ZJ");
                }
            }
        }

        #region 拉取安监数据

        /// <summary>
        /// 从省一体化平台获取数据安监申报数据到无锡数据中心
        /// </summary>
        public string YourTask_PullAJSBDataFromSythpt(DateTime pullDate)
        {
            string apiMessage = string.Empty;

            //ShenBaoDataService sbDataService = new ShenBaoDataService();
            DataTable dtapizb = dataService.Get_API_zb_apiFlowDetail("0");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                try
                {
                    DateTime beginTime = DateTime.Now;
                    Public.WriteLog("开始执行YourTask_PullAJSBDataFromSythpt任务:" + beginTime.ToString("yyyy-MM-dd HH:mm:ss"));

                    //往数据监控日志表添加一条记录
                    DataTable dt_DataJkLog = dataService.GetSchema_DataJkLog();
                    DataRow row_DataJkLog = dt_DataJkLog.NewRow();
                    dt_DataJkLog.Rows.Add(row_DataJkLog);
                    string dataJkLogID = dataService.Get_DataJkLogNewID();
                    row_DataJkLog["ID"] = dataJkLogID;
                    row_DataJkLog["DataFlow"] = dtapizb.Rows[0]["apiFlow"];
                    row_DataJkLog["DataFlowName"] = dtapizb.Rows[0]["apiName"];
                    row_DataJkLog["ServiceUrl"] = dtapizb.Rows[0]["apiUrl"];
                    row_DataJkLog["csTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    dataService.Submit_DataJkLog(dt_DataJkLog);

                    ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient client = new ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient();

                    #region  从省一体化平台获取安监申报数据

                    DataTable dtApApiUsers = dataService.GetApApiUsers("AJ");//AJ-获取安监帐号列表
                    string pullDateStr = pullDate.ToString("yyyy-MM-dd");
                    //string pullDateStr = "2017-09-04";//测试时间
                    //DateTime pullDate = DateTime.Now;
                    //DateTime.TryParse(pullDateStr, out pullDate);
                    string getUUIDXml = String.Empty;
                    string getDetailDataXml = String.Empty;
                    string message_lxxm = String.Empty;
                    DataTable dt_DataJkDataDetail = dataService.GetSchema_DataJkDataDetail();

                    foreach (DataRow rowUser in dtApApiUsers.Rows)
                    {
                        try
                        {
                            Public.WriteLog("获取安监申报数据,机构：" + rowUser["deptCode"].ToString2() + ",日期：" + pullDateStr);
                            getUUIDXml = client.getAJSBBByDate(rowUser["deptCode"].ToString2(), rowUser["password"].ToString2(), pullDateStr);

                            Public.WriteLog("获取安监申报数据结果-getAJSBBByDate：" + getUUIDXml);

                            if (getUUIDXml.Contains("<?xml version=\"1.0\" encoding=\"GB2312\"?>"))
                            {
                                apiMessage = "sucess";
                                getUUIDXml = getUUIDXml.Replace("<?xml version=\"1.0\" encoding=\"GB2312\"?>", "").Replace("<body>", "").Replace("</body>", "");
                            }
                            else
                            {
                                //错误处理, 往数据监控详细日志表项添加一条记录
                                createMonitorLog(dataService, dt_DataJkDataDetail, dataJkLogID, "Ap_ajsbb", "getAJSBBByDate");

                                /**
                                DataRow row_DataJkDataDetail = dt_DataJkDataDetail.NewRow();
                                dt_DataJkDataDetail.Rows.Add(row_DataJkDataDetail);
                                Int64 id = dataService.Get_DataJkDataDetailNewID().ToInt64();
                                row_DataJkDataDetail["ID"] = id;
                                row_DataJkDataDetail["DataJkLogID"] = row_DataJkLog["ID"];
                                row_DataJkDataDetail["tableName"] = "Ap_ajsbb";
                                row_DataJkDataDetail["MethodName"] = "getAJSBBByDate";
                                 */

                            }

                            if (!string.IsNullOrEmpty(getUUIDXml.Trim()))
                            {
                                DataTable dt = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(getUUIDXml, out message_lxxm);
                                //string parseBase64Xml = xmlHelper.ConvertDataTableToXML(dt, "dataTable", "row");
                                //Public.WriteLog("获取安监申报数据结果：" + parseBase64Xml);

                                foreach (DataRow row in dt.Rows)
                                {
                                    try
                                    {
                                        //根据uuid获取安监申报详细数据

                                        Public.WriteLog("根据uuid获取安监申报详细数据：" + row[0].ToString());
                                        getDetailDataXml = client.getAJSBBByUuid(rowUser["deptCode"].ToString2(), rowUser["password"].ToString2(), row[0].ToString());
                                        //Public.WriteLog("结果：" + getDetailDataXml);
                                        if (getDetailDataXml.Contains("<?xml version=\"1.0\" encoding=\"gb2312\"?>"))
                                        {
                                            getDetailDataXml = getDetailDataXml.Replace("<?xml version=\"1.0\" encoding=\"gb2312\"?>", "").Replace("<body>", "").Replace("</body>", "");
                                            saveAJSBXmlDataToDb(row[0].ToString(), rowUser["deptCode"].ToString2(), getDetailDataXml, pullDate);

                                        }
                                        else
                                        {
                                            //错误处理, 往数据监控详细日志表项添加一条记录
                                            createMonitorLog(dataService, dt_DataJkDataDetail, dataJkLogID, "Ap_ajsbb", "getAJSBBByUuid");

                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Public.WriteLog("根据uuid获取安监申报详细数据方法出现异常:" + ex.Message);
                                        apiMessage += ex.Message;
                                    }
                                    

                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            Public.WriteLog("执行YourTask_PullAJSBDataFromSythpt方法出现异常1:" + ex.Message);
                            apiMessage += ex.Message;
                        }
                        


                    }

                    dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail);

                    #endregion

                }
                catch (Exception ex)
                {
                    Public.WriteLog("执行YourTask_PullAJSBDataFromSythpt方法出现异常2:" + ex.Message);
                    apiMessage += ex.Message;
                }
                finally
                {
                    DataTable dtapicb = dataService.GetSchema_API_cb();
                    DataRow row_apicb = dtapicb.NewRow();
                    dtapicb.Rows.Add(row_apicb);
                    row_apicb["apiCbID"] = dataService.Get_apiCbNewID();
                    row_apicb["apiFlow"] = "0";
                    row_apicb["apiMethod"] = "getAJSBBByDate";
                    row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                    row_apicb["apiDyMessage"] = apiMessage;
                    row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dataService.Submit_API_cb(dtapicb);

                    dataService.UpdateZbJkzt("0", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

                }
               
            }
            return apiMessage;
        }

        /// <summary>
        /// 从省一体化平台按uuid获取数据安监申报数据到无锡数据中心
        /// </summary>
        public string YourTask_PullAJSBDataFromSythptByUUID(string deptCode , string password, string uuid)
        {
            string apiMessage = string.Empty;
 
            DataTable dtapizb = dataService.Get_API_zb_apiFlowDetail("0");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                try
                {
                    ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient client = new ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient();
                    //根据uuid获取安监申报详细数据
                    string getDetailDataXml = String.Empty;

                    Public.WriteLog("YourTask_PullAJSBDataFromSythptByUUID：" + uuid);
                    getDetailDataXml = client.getAJSBBByUuid(deptCode, password, uuid);
                    //Public.WriteLog("结果：" + getDetailDataXml);
                    if (getDetailDataXml.Contains("<?xml version=\"1.0\" encoding=\"gb2312\"?>"))
                    {
                        apiMessage = "success";
                        getDetailDataXml = getDetailDataXml.Replace("<?xml version=\"1.0\" encoding=\"gb2312\"?>", "").Replace("<body>", "").Replace("</body>", "");
                        saveAJSBXmlDataToDb(uuid, deptCode, getDetailDataXml, DateTime.Now);

                    }

                }
                catch (Exception ex)
                {
                    Public.WriteLog("执行YourTask_PullAJSBDataFromSythptByUUID方法出现异常:" + ex.Message);
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

                    DataTable dt_Ap_ajsbb = dataService.Get_Ap_ajsbb(item["uuid"].ToString2());
                    DataRow toSaveRow;

                    if (dt_Ap_ajsbb != null && dt_Ap_ajsbb.Rows.Count > 0)
                    {
                        toSaveRow = dt_Ap_ajsbb.Rows[0];

                        int cmpFlag = DateTime.Compare(item["updateDate"].ToDateTime(), toSaveRow["updateDate"].ToDateTime());
                        //Public.WriteLog("====" + toSaveRow["Status"] + "|cmpFlag:" + cmpFlag);

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

                    //Public.WriteLog("====" + toSaveRow["uuid"] + toSaveRow["PrjNum"]);

                    if (dt_Ap_ajsbb.Rows.Count > 0)
                    {
                        if (!dataService.Save_Ap_ajsbb(dt_Ap_ajsbb))
                        {
                            //保存失败的错误处理
                            Public.WriteLog("Save_Ap_ajsbb:fail");
                        }
                    }

                    return "success";

                }
            }
            catch (Exception ex)
            {
                Public.WriteLog("执行saveMainListXmlDataToDb方法出现异常:" + ex.Message);
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
                    dataService.Delete_ApTable(AP_AJSBB_HT, uuid);
                    DataTable existDt = dataService.Get_ApTable(AP_AJSBB_HT);

                    foreach (DataRow item in dt.Rows)
                    {
                        DataRow toSaveRow = existDt.NewRow();
                        DataTableHelp.DataRow2DataRow(item, toSaveRow);
                        existDt.Rows.Add(toSaveRow);

                        //由于文档跟实际获取的xml不一致，特殊处理字段
                        //toSaveRow["CorpCode"] = item["contractorCorpCode"];
                        //toSaveRow["CorpName"] = item["contractorCorpName"];
                        //toSaveRow["RecordNum"] = item["recordNum"];
                        //toSaveRow["xmfzrsfzh"] = item["iDCard"];
                        //toSaveRow["xmfzr"] = item["prjHead"];

                    }
                    if (existDt.Rows.Count > 0)
                    {
                        if (!dataService.Save_Ap_ajsbb_ht(existDt))
                        {
                            //保存失败的错误处理
                            Public.WriteLog("Save_Ap_ajsbb:fail");
                        }
                    }
                    return "success";

                }
            }
            catch (Exception ex)
            {
                Public.WriteLog("执行saveHtListXmlDataToDb方法出现异常:" + ex.Message);
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
                dataService.Delete_ApTable(AP_AJSBB_DWRY, uuid);
                DataTable existDt = dataService.Get_ApTable(AP_AJSBB_DWRY);

                foreach (DataRow item in dt.Rows)
                {
                    DataRow toSaveRow = existDt.NewRow();
                    DataTableHelp.DataRow2DataRow(item, toSaveRow);
                    existDt.Rows.Add(toSaveRow);
                }
                if (existDt.Rows.Count > 0)
                {
                    //TODO, 循环保存单条数据，有待重构
                    if (!dataService.Save_Ap_ajsbb_dwry(existDt))
                    {
                        //保存失败的错误处理
                        Public.WriteLog("Save_Ap_ajsbb:fail");
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
                dataService.Delete_ApTable(AP_AJSBB_CLQD, uuid);
                DataTable existDt = dataService.Get_ApTable(AP_AJSBB_CLQD);

                foreach (DataRow item in dt.Rows)
                {
                    DataRow toSaveRow = existDt.NewRow();
                    DataTableHelp.DataRow2DataRow(item, toSaveRow);
                    existDt.Rows.Add(toSaveRow);
                }
                if (existDt.Rows.Count > 0)
                {
                    //TODO, 循环保存单条数据，有待重构
                    if (!dataService.Save_Ap_ajsbb_clqd(existDt))
                    {
                        //保存失败的错误处理
                        Public.WriteLog("Save_Ap_ajsbb:fail");
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
                dataService.Delete_ApTable(AP_AJSBB_HJSSJD, uuid);
                DataTable existDt = dataService.Get_ApTable(AP_AJSBB_HJSSJD);

                foreach (DataRow item in dt.Rows)
                {
                    DataRow toSaveRow = existDt.NewRow();
                    DataTableHelp.DataRow2DataRow(item, toSaveRow);
                    existDt.Rows.Add(toSaveRow);

                }
                if (existDt.Rows.Count > 0)
                {
                    //TODO, 循环保存单条数据，有待重构
                    if (!dataService.Save_Ap_ajsbb_hjssjd(existDt))
                    {
                        //保存失败的错误处理
                        Public.WriteLog("Save_Ap_ajsbb:fail");
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
                dataService.Delete_ApTable(AP_AJSBB_WXYJDGCQD, uuid);
                DataTable existDt = dataService.Get_ApTable(AP_AJSBB_WXYJDGCQD);

                foreach (DataRow item in dt.Rows)
                {
                    DataRow toSaveRow = existDt.NewRow();
                    DataTableHelp.DataRow2DataRow(item, toSaveRow);
                    existDt.Rows.Add(toSaveRow);

                }
                if (existDt.Rows.Count > 0)
                {
                    //TODO, 循环保存单条数据，有待重构
                    if (!dataService.Save_Ap_ajsbb_wxyjdgcqd(existDt))
                    {
                        //保存失败的错误处理
                        Public.WriteLog("Save_Ap_ajsbb:fail");
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
                dataService.Delete_ApTable(AP_AJSBB_CGMGCQD, uuid);
                DataTable existDt = dataService.Get_ApTable(AP_AJSBB_CGMGCQD);
                foreach (DataRow item in dt.Rows)
                {
                    DataRow toSaveRow = existDt.NewRow();
                    DataTableHelp.DataRow2DataRow(item, toSaveRow);
                    existDt.Rows.Add(toSaveRow);

                }
                if (existDt.Rows.Count > 0)
                {
                    //TODO, 循环保存单条数据，有待重构
                    if (!dataService.Save_Ap_ajsbb_cgmgcqd(existDt))
                    {
                        //保存失败的错误处理
                        Public.WriteLog("Save_Ap_ajsbb:fail");
                    }
                }
                return "success";

            }
        }

        #endregion

        #region 拉取质监数据

        /// <summary>
        /// 从省一体化平台获取数据质监申报数据到无锡数据中心
        /// </summary>
        public string YourTask_PullZJSBDataFromSythpt(DateTime pullDate)
        {
            string apiMessage = string.Empty;
            DataTable dtapizb = dataService.Get_API_zb_apiFlowDetail("0");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                try
                {
                    DateTime beginTime = DateTime.Now;
                    Public.WriteLog("开始执行YourTask_PullZJSBDataFromSythpt任务:" + beginTime.ToString("yyyy-MM-dd HH:mm:ss"));

                    //往数据监控日志表添加一条记录
                    DataTable dt_DataJkLog = dataService.GetSchema_DataJkLog();
                    DataRow row_DataJkLog = dt_DataJkLog.NewRow();
                    dt_DataJkLog.Rows.Add(row_DataJkLog);
                    string dataJkLogID = dataService.Get_DataJkLogNewID();
                    row_DataJkLog["ID"] = dataJkLogID;
                    row_DataJkLog["DataFlow"] = dtapizb.Rows[0]["apiFlow"];
                    row_DataJkLog["DataFlowName"] = dtapizb.Rows[0]["apiName"];
                    row_DataJkLog["ServiceUrl"] = dtapizb.Rows[0]["apiUrl"];
                    row_DataJkLog["csTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    dataService.Submit_DataJkLog(dt_DataJkLog);

                    ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient client = new ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient();
                    #region  从省一体化平台获取质监申报数据

                    DataTable dtApApiUsers = dataService.GetApApiUsers("ZJ");//AJ-获取安监帐号列表
                    //string pullDate = DateTime.Now.ToString("yyyy-MM-dd");
                    string pullDateStr = pullDate.ToString("yyyy-MM-dd");
                    //DateTime pullDate;
                    //DateTime.TryParse(pullDateStr, out pullDate);

                    string getUUIDXml = String.Empty;
                    string getDetailDataXml = String.Empty;
                    string message_lxxm = String.Empty;
                    DataTable dt_DataJkDataDetail = dataService.GetSchema_DataJkDataDetail();


                    foreach (DataRow rowUser in dtApApiUsers.Rows)
                    {

                        try
                        {
                            Public.WriteLog("获取质监申报数据,机构：" + rowUser["deptCode"].ToString2() + ",日期：" + pullDateStr);
                            getUUIDXml = client.getZJSBBByDate(rowUser["deptCode"].ToString2(), rowUser["password"].ToString2(), pullDateStr);

                            //Public.WriteLog("获取质监申报数据结果：" + getUUIDXml);

                            if (getUUIDXml.Contains("<?xml version=\"1.0\" encoding=\"GB2312\"?>"))
                            {
                                apiMessage = "success";
                                getUUIDXml = getUUIDXml.Replace("<?xml version=\"1.0\" encoding=\"GB2312\"?>", "").Replace("<body>", "").Replace("</body>", "");
                            }
                            else
                            {
                                //错误处理, 往数据监控详细日志表项添加一条记录
                                createMonitorLog(dataService, dt_DataJkDataDetail, dataJkLogID, "Ap_zjsbb", "getZJSBBByDate");

                                /**
                                DataRow row_DataJkDataDetail = dt_DataJkDataDetail.NewRow();
                                dt_DataJkDataDetail.Rows.Add(row_DataJkDataDetail);
                                Int64 id = dataService.Get_DataJkDataDetailNewID().ToInt64();
                                row_DataJkDataDetail["ID"] = id;
                                row_DataJkDataDetail["DataJkLogID"] = row_DataJkLog["ID"];
                                row_DataJkDataDetail["tableName"] = "Ap_ajsbb";
                                row_DataJkDataDetail["MethodName"] = "getAJSBBByDate";
                                 */

                            }

                            if (!string.IsNullOrEmpty(getUUIDXml.Trim()))
                            {
                                DataTable dt = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(getUUIDXml, out message_lxxm);
                                //string parseBase64Xml = xmlHelper.ConvertDataTableToXML(dt, "dataTable", "row");
                                //Public.WriteLog("获取安监申报数据结果：" + parseBase64Xml);

                                foreach (DataRow row in dt.Rows)
                                {
                                    try
                                    {
                                        //根据uuid获取质监申报详细数据
                                        Public.WriteLog("根据uuid获取质监申报详细数据：" + row[0].ToString());
                                        getDetailDataXml = client.getZJSBBByUuid(rowUser["deptCode"].ToString2(), rowUser["password"].ToString2(), row[0].ToString());
                                        //Public.WriteLog("getZJSBBByUuid结果：" + getDetailDataXml);
                                        if (getDetailDataXml.Contains("<?xml version=\"1.0\" encoding=\"gb2312\"?>"))
                                        {
                                            getDetailDataXml = getDetailDataXml.Replace("<?xml version=\"1.0\" encoding=\"gb2312\"?>", "").Replace("<body>", "").Replace("</body>", "");

                                            saveZJSBXmlDataToDb(row[0].ToString(), rowUser["deptCode"].ToString2(), getDetailDataXml, pullDate);

                                        }
                                        else
                                        {
                                            //错误处理, 往数据监控详细日志表项添加一条记录
                                            createMonitorLog(dataService, dt_DataJkDataDetail, dataJkLogID, "Ap_zjsbb", "getZJSBBByUuid");

                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Public.WriteLog("根据uuid获取质监申报详细数据出现异常:" + ex.Message);
                                        apiMessage += ex.Message;
                                    }
                                    

                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            Public.WriteLog("执行YourTask_PullZJSBDataFromSythpt方法出现异常:" + ex.Message);
                            apiMessage += ex.Message;
                        }

                        


                    }

                    dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail);

                    #endregion

                }
                catch (Exception ex)
                {
                    Public.WriteLog("执行YourTask_PullZJSBDataFromSythpt方法出现异常:" + ex.Message);
                    apiMessage += ex.Message;
                }
                finally
                {
                    DataTable dtapicb = dataService.GetSchema_API_cb();
                    DataRow row_apicb = dtapicb.NewRow();
                    dtapicb.Rows.Add(row_apicb);
                    row_apicb["apiCbID"] = dataService.Get_apiCbNewID();
                    row_apicb["apiFlow"] = "0";
                    row_apicb["apiMethod"] = "getZJSBBByDate";
                    row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                    row_apicb["apiDyMessage"] = apiMessage;
                    row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dataService.Submit_API_cb(dtapicb);

                    dataService.UpdateZbJkzt("0", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

                }
            }
            return apiMessage;
        }


        /// <summary>
        /// 从省一体化平台按uuid获取质监申报数据到无锡数据中心
        /// </summary>
        public string YourTask_PullZJSBDataFromSythptByUUID(string deptCode, string password, string uuid)
        {
            string apiMessage = string.Empty;

            DataTable dtapizb = dataService.Get_API_zb_apiFlowDetail("0");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                try
                {
                    ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient client = new ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient();
                    //根据uuid获取安监申报详细数据
                    string getDetailDataXml = String.Empty;

                    Public.WriteLog("YourTask_PullZJSBDataFromSythptByUUID：" + uuid);
                    //根据uuid获取质监申报详细数据

                    getDetailDataXml = client.getZJSBBByUuid(deptCode, password, uuid);
                    //Public.WriteLog("getZJSBBByUuid结果：" + getDetailDataXml);
                    if (getDetailDataXml.Contains("<?xml version=\"1.0\" encoding=\"gb2312\"?>"))
                    {
                        apiMessage = "success";
                        getDetailDataXml = getDetailDataXml.Replace("<?xml version=\"1.0\" encoding=\"gb2312\"?>", "").Replace("<body>", "").Replace("</body>", "");
                        saveZJSBXmlDataToDb(uuid , deptCode, getDetailDataXml, DateTime.Now);

                    }

                }
                catch (Exception ex)
                {
                    Public.WriteLog("执行YourTask_PullZJSBDataFromSythptByUUID方法出现异常:" + ex.Message);
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

                DataTable dt_Ap_zjsbb = dataService.Get_Ap_zjsbb(item["uuid"].ToString2());
                DataRow toSaveRow;

                if (dt_Ap_zjsbb != null && dt_Ap_zjsbb.Rows.Count > 0)
                {
                    toSaveRow = dt_Ap_zjsbb.Rows[0];
                    //Public.WriteLog("====" + toSaveRow["uuid"] + "|" +  toSaveRow["Status"]);

                    int cmpFlag = DateTime.Compare(item["updateDate"].ToDateTime() , toSaveRow["updateDate"].ToDateTime());
                    //Public.WriteLog("====" + toSaveRow["Status"] + "|cmpFlag:" + cmpFlag);

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


                //Public.WriteLog("====" + toSaveRow["uuid"] + toSaveRow["PrjNum"]);

                if (dt_Ap_zjsbb.Rows.Count > 0)
                {
                    if (!dataService.Save_Ap_sbb(dt_Ap_zjsbb, AP_ZJSBB))
                    {
                        //保存失败的错误处理
                        Public.WriteLog("Save_Ap_ajsbb:fail");
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
                dataService.Delete_ApTable(AP_ZJSBB_HT, uuid);
                DataTable existDt = dataService.Get_ApTable(AP_ZJSBB_HT);

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

                    //Public.WriteLog("====" + toSaveRow["uuid"] + toSaveRow["RecordNum"]);

                }
                if (existDt.Rows.Count > 0)
                {
                    if (!dataService.Save_Ap_sbb(existDt, AP_ZJSBB_HT))
                    {
                        //保存失败的错误处理
                        Public.WriteLog("Save_Ap_ajsbb:fail");
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
                dataService.Delete_ApTable(AP_ZJSBB_DWRY, uuid);
                DataTable existDt = dataService.Get_ApTable(AP_ZJSBB_DWRY);
                foreach (DataRow item in dt.Rows)
                {
                    DataRow  toSaveRow = existDt.NewRow();
                    DataTableHelp.DataRow2DataRow(item, toSaveRow);
                    existDt.Rows.Add(toSaveRow);

                }
                if (existDt.Rows.Count > 0)
                {
                    if (!dataService.Save_Ap_sbb(existDt, AP_ZJSBB_DWRY))
                    {
                        //保存失败的错误处理
                        Public.WriteLog("Save_Ap_ajsbb:fail");
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
                dataService.Delete_ApTable(AP_ZJSBB_SCHGS, uuid);
                DataTable existDt = dataService.Get_ApTable(AP_ZJSBB_SCHGS);
                foreach (DataRow item in dt.Rows)
                {
                    DataRow toSaveRow = existDt.NewRow();
                    DataTableHelp.DataRow2DataRow(item, toSaveRow);
                    existDt.Rows.Add(toSaveRow);
                }
                if (existDt.Rows.Count > 0)
                {
                    if (!dataService.Save_Ap_sbb(existDt, AP_ZJSBB_SCHGS))
                    {
                        //保存失败的错误处理
                        Public.WriteLog("Save_Ap_ajsbb:fail");
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
                dataService.Delete_ApTable(AP_ZJSBB_DWGC, uuid);
                DataTable existDt = dataService.Get_ApTable(AP_ZJSBB_DWGC);
                foreach (DataRow item in dt.Rows)
                {
                    DataRow toSaveRow = existDt.NewRow();
                    DataTableHelp.DataRow2DataRow(item, toSaveRow);
                    existDt.Rows.Add(toSaveRow);

                }
                if (existDt.Rows.Count > 0)
                {
                    if (!dataService.Save_Ap_sbb(existDt, AP_ZJSBB_DWGC))
                    {
                        //保存失败的错误处理
                        Public.WriteLog("Save_Ap_ajsbb:fail");
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
                dataService.Delete_ApTable(AP_ZJSBB_CLQD, uuid);
                DataTable existDt = dataService.Get_ApTable(AP_ZJSBB_CLQD);
                foreach (DataRow item in dt.Rows)
                {
                    DataRow toSaveRow = existDt.NewRow();
                    DataTableHelp.DataRow2DataRow(item, toSaveRow);
                    existDt.Rows.Add(toSaveRow);
                }
                if (existDt.Rows.Count > 0)
                {
                    if (!dataService.Save_Ap_sbb(existDt, AP_ZJSBB_CLQD))
                    {
                        //保存失败的错误处理
                        Public.WriteLog("Save_Ap_ajsbb:fail");
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
        public void createMonitorLog(ShenBaoDataService dataService, DataTable dt_DataJkDataDetail, string dataJkLogId,
            string tableName, string methodName)
        {
            DataRow row_DataJkDataDetail = dt_DataJkDataDetail.NewRow();
            dt_DataJkDataDetail.Rows.Add(row_DataJkDataDetail);
            Int64 id = dataService.Get_DataJkDataDetailNewID().ToInt64();
            row_DataJkDataDetail["ID"] = id;
            row_DataJkDataDetail["DataJkLogID"] = dataJkLogId;
            row_DataJkDataDetail["tableName"] = tableName;
            row_DataJkDataDetail["MethodName"] = methodName;
        }


    }
}
