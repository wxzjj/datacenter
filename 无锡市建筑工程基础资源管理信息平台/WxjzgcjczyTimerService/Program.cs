using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using WxsjzxTimerService;
using WxsjzxTimerService.Common;
using System.Data;
using WxjzgcjczyTimerService.model;
using Bigdesk8;
using Bigdesk8.Data;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Collections;
using System.Xml;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Xml.Schema;

namespace WxjzgcjczyTimerService
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new Service1() ,
                new Service2() ,
                new Service3()
            };
            ServiceBase.Run(ServicesToRun);
            //UploadToSzx_SafeProdadmper();
            //UploadToSzx_ConstrProjectInfo();
            //string str = "[{\"RYXM\":\"史海全\",\"SFZH\":\"61040319780812007X\",\"ZSBM\":\"苏建安B(2014)0200551\",\"ZYZGLX\":\"注册建造师\",\"DWMC\":\"江苏景苑绿化建设有限公司\"},{\"RYXM\":\"王聿骏\",\"SFZH\":\"320223197809266172\",\"ZSBM\":\"苏建安C（2008）0200516\",\"ZYZGLX\":\"注册建造师\",\"DWMC\":\"江苏苏禾建设发展有限公司\"}]";
            //Upload(str);

            //YourTask_PullDataFromSxxzx_Ryxx_Zczyry("111");
            //YourTask_PullDataFromSxxzx_Jsdw("1111");
            //YourTask_PullDataFromSxxzx_Swqyxx("222");
            //YourTask_PullDataFromSxxzx_Swryxx("111");


            //YourTask_PullDataFromSxxzx_Ryxx_Aqscgl("111");

            //YourTask_PullDataFromSxxzx_qyxx("111");
            //YourTask_PullDataFromSxxzx_Ryxx_Zygwgl("111");
        }


        private static string _url = "http://58.215.18.222:8008/zhujianju_dpl1";

        static void Upload(string content)
        {
            Uri address = null;
            HttpWebRequest request = null;
            address = new Uri(_url + string.Format("/certif_person"));//拼接完整的url
            request = (HttpWebRequest)HttpWebRequest.Create(address);
            request.ContentType = "application/json";
            request.Method = "POST";
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
            finally
            {
            }
            //获取返回信息.
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader1 = new StreamReader(response.GetResponseStream());
                string message = string.Empty;

                string temp_resultStr = reader1.ReadToEnd().Replace("<br>", "").Replace("\\", "");
                temp_resultStr = temp_resultStr.Substring(1, temp_resultStr.Length - 2);

                XmlDocument Xd = new XmlDocument();
                Xd.LoadXml(temp_resultStr.Trim());
                string isSuccess = Xd.SelectSingleNode("result").SelectSingleNode("success").InnerText;
                string resultMessage = Xd.SelectSingleNode("result").SelectSingleNode("message").InnerText;
                //if (isSuccess == "false")
                //{
                //    this.WindowAlert("上传失败！\n" + resultMessage);
                //}
                //else
                //{
                //    this.WindowAlert("上传成功！\n" + resultMessage);
                //}
            }
        }

        static  void UploadToSzx_SafeProdadmper()
        {
            DataService dataService = new DataService();
      
 
  
            int all_count = 0, success_count = 0;

            DataTable dt = dataService.Get_SafeProdadmper();

   

            if (dt.Rows.Count == 0)
            {
                return;
            }

            #region 保存至市一中心四平台（uepp_ryjbxx_aqscglry_szxspt）
            try
            {
                string json = "";

                DataRow row;
                foreach (DataRow dataRow in dt.Rows)
                {
                    json = ToJson(dataRow);

                    string resultSt = string.Empty;
                    string PK = dataRow["SFZH"].ToString();
                    if (!string.IsNullOrEmpty(dataRow["ZYZGLX"].ToString()))
                    {
                        PK = PK + "," + dataRow["ZYZGLX"].ToString();
                    }

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
              

            }
            catch (Exception ex)
            {
              
            }

            #endregion

        }


        static string ToJson(DataRow dataRow)
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



        static void UploadToSzx_ConstrProjectInfo()
        {
            DataService dataService = new DataService();
            //DataRow row_DataJkDataDetail = dt_DataJkDataDetail.NewRow();
            //dt_DataJkDataDetail.Rows.Add(row_DataJkDataDetail);

            //row_DataJkDataDetail["ID"] = Id_DataJkDataDetail;
            //row_DataJkDataDetail["DataJkLogID"] = Id_DataJkLog;
            //row_DataJkDataDetail["tableName"] = "TBProjectInfo";
            //row_DataJkDataDetail["MethodName"] = "UploadToSzx_ConstrProjectInfo";
            //row_DataJkDataDetail["bz"] = "往市中心四平台推送工程项目基本信息";
            //int all_count = 0, success_count = 0;

            DataTable dt = dataService.Get_ConstrProjectInfo();


            if (dt.Rows.Count == 0)
            {
                return;
            }

            #region 保存至市一中心四平台（TBProjectInfo）
            try
            {
                string json = "";

                DataRow row;
                foreach (DataRow dataRow in dt.Rows)
                {
                    string PK = dataRow["PKID"].ToString();
                    json = ToJson(dataRow);

                    string resultSt = string.Empty;
                    //string resultSt = Contact(json, "http://58.215.18.222:8008/zhujianju_dpl1/constr_project_info");

                    DataTable dt_log_TBProjectInfo = dataService.GetTBData_SaveToStLog("TBProjectInfo", PK);

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
                        row["PKID"] = PK;
                        row["TableName"] = "TBProjectInfo";
                        row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        row["UpdateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    //all_count++;

                    if (!resultSt.Contains("成功"))
                    {
                        row["OperateState"] = 1;
                        row["Msg"] = resultSt;
                    }
                    //else
                    //{
                    //    success_count++;
                    //    row["OperateState"] = 0;
                    //    row["Msg"] = resultSt;
                    //}
                    if (dt_log_TBProjectInfo.Rows.Count > 0)
                    {
                        dataService.SaveTBData_SaveToStLog(dt_log_TBProjectInfo);
                    }
                }
                //row_DataJkDataDetail["allCount"] = all_count;
                //row_DataJkDataDetail["successCount"] = success_count;
                //row_DataJkDataDetail["IsOk"] = 1;
                //row_DataJkDataDetail["ErrorMsg"] = "";

            }
            catch (Exception ex)
            {
                //row_DataJkDataDetail["allCount"] = all_count;
                //row_DataJkDataDetail["successCount"] = success_count;
                //row_DataJkDataDetail["IsOk"] = 0;
                //row_DataJkDataDetail["ErrorMsg"] = ex.Message;
            }

            #endregion

        }




        /// <summary>
        /// 从江苏建设公共基础数据平台拉取企业（建设单位除外）信息(没问题？？？)
        /// </summary>
        static void YourTask_PullDataFromSxxzx_qyxx(string DataJkLogID)
        {
            Public.WriteLog("执行YourTask_PullDataFromSxxzx_qyxx方法：");
            string tag = Tag.江苏建设公共基础数据平台.ToString();
            string userID = "wxszjj01";
            DataService dataService = new DataService();
            DataTable dt_xzqhdm = dataService.Get_tbXzqdmDic();
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
            row_DataJkDataDetail_qyxx["tableName"] = "UEPP_Ryjbxx";
            row_DataJkDataDetail_qyxx["MethodName"] = "getCorpInfoStream_Inc";
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
                                        }
                                        row["tag"] = tag;
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
                                        tempRow_qycsyw["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd");



                                    if (dt_qycsyw.Rows.Count > 0)
                                    {
                                        dataService.Submit_uepp_qycsyw(dt_qycsyw);
                                    }
                                    #endregion

                                    DataTable dt_jsdw_zzmx = dataService.Get_uepp_zzmxxx_qyxx(corpCertQual.CorpCode, csywlxID);

                                    int rowIndex = -1;

                                    for (int i = 0; i < dt_jsdw_zzmx.Rows.Count; i++)
                                    {
                                        //if (dt_jsdw_zzmx.Rows[i].RowState == DataRowState.Deleted)
                                        //    continue;

                                        if (csywlxID == dt_jsdw_zzmx.Rows[i]["csywlxID"].ToString2()
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
                                        if (!string.IsNullOrEmpty(corpCertQual.UpdateDate) && !string.IsNullOrEmpty(row["xgrqsj"].ToString2()))
                                            if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(corpCertQual.UpdateDate).ToString("yyyy-MM-dd"))
                                            {
                                                continue;
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

                                        if (corpCertInfo.CertType == dt_qy_zzzs.Rows[i]["csywlx"].ToString2()
                                            && dt_qy_zzzs.Rows[i]["zsbh"].ToString2() == corpCertInfo.CertCode
                                            && dt_qy_zzzs.Rows[i]["csywlx"].ToString2() == corpCertInfo.CertType)
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
                                        row["qyID"] = corpCertInfo.CorpCode;
                                    }
                                    else
                                    {
                                        row = dt_qy_zzzs.Rows[rowIndex];
                                        if (!string.IsNullOrEmpty(corpCertInfo.UpdateDate))
                                            if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(corpCertInfo.UpdateDate).ToString("yyyy-MM-dd"))
                                            {
                                                continue;
                                            }
                                    }

                                    row["csywlx"] = csywlx;
                                    row["csywlxID"] = csywlxID;

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
                                    Public.WriteLog("保存企业资质证书信息时出现异常：" + ex.Message);
                                }
                            }
                        }
                        #endregion

                        #endregion
                    }
                }
                if (dt_SaveDataLog.Rows.Count > 0)
                    dataService.Submit_SaveDataLog(dt_SaveDataLog);

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
        ///  已测过一次(没问题？？？)
        /// </summary>
        /// <param name="DataJkLogID"></param>
        static void YourTask_PullDataFromSxxzx_Ryxx_Zczyry(string DataJkLogID)
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
                            try
                            {
                                string xzqdm = row_xzqdm["Code"].ToString2();
                                byte[] bytes;
                                string result = String.Empty;


                                #region 获取注册执业人员信息
                                Public.WriteLog("获取" + tag + "注册执业人员信息：");
                                bytes = newdataService.getPersonRegCert_Inc(userID, "320200", xzqdm, regType[retp], ConfigurationManager.AppSettings["ZczyrybeginDate"].ToString(), DateTime.Now.ToString("yyyy-MM-dd"), "0");
                                result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                                #endregion
                            }
                            catch (Exception ex)
                            {
                                string message = ex.Message;
                            }

                        }
                    }

                    if (dt_SaveDataLog.Rows.Count > 0)
                        dataService.Submit_SaveDataLog(dt_SaveDataLog);

                    DateTime endTime = DateTime.Now;
                    TimeSpan span1 = new TimeSpan(beginTime.Year, beginTime.Month, beginTime.Second);
                    TimeSpan span2 = new TimeSpan(endTime.Year, endTime.Month, endTime.Second);
                    TimeSpan span = span2 - span1;
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
        /// 从江苏建设公共基础数据平台拉取人员（安全生产管理人员）信息(没问题)
        /// </summary>
        static void YourTask_PullDataFromSxxzx_Ryxx_Aqscgl(string DataJkLogID)
        {
            try
            {
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
                row_DataJkDataDetail_ryxx["MethodName"] = "getPersonJobCert";
                row_DataJkDataDetail_ryxx["bz"] = "从江苏建设公共基础数据平台拉取人员（安全生产管理人员）信息";

                try
                {
                    DataTable dt_SaveDataLog = dataService.GetSchema_SaveDataLog();

                    NewDataService.NewDataService newdataService = new WxjzgcjczyTimerService.NewDataService.NewDataService();
                    DataRow row;
                    foreach (DataRow row_xzqdm in dt_xzqhdm.Rows)
                    {
                        string xzqdm = row_xzqdm["Code"].ToString2();

                        Public.WriteLog("获取" + tag + "A类安全员：");
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

                        Public.WriteLog("获取" + tag + "B类安全员：");
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

                        Public.WriteLog("获取" + tag + "C类安全员：");
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
        /// 从江苏建设公共基础数据平台拉取人员（专业岗位管理人员）信息(没问题)
        /// </summary>
        static void YourTask_PullDataFromSxxzx_Ryxx_Zygwgl(string DataJkLogID)
        {
            try
            {
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
                row_DataJkDataDetail_ryxx["MethodName"] = "getPersonJobCertStream";
                row_DataJkDataDetail_ryxx["bz"] = "从江苏建设公共基础数据平台拉取人员（专业岗位管理人员）信息";

                try
                {
                    DataTable dt_SaveDataLog = dataService.GetSchema_SaveDataLog();

                    NewDataService.NewDataService newdataService = new WxjzgcjczyTimerService.NewDataService.NewDataService();
                    DataRow row;

                    foreach (DataRow row_xzqdm in dt_xzqhdm.Rows)
                    {
                        string xzqdm = row_xzqdm["Code"].ToString2();

                        Public.WriteLog("获取" + tag + "施工员：");
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
                        Public.WriteLog("获取" + tag + "质量员：");
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
                        Public.WriteLog("获取" + tag + "机械员：");
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
                        Public.WriteLog("获取" + tag + "材料员：");
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
                        Public.WriteLog("获取" + tag + "造价员：");
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
                        Public.WriteLog("获取" + tag + "劳务员：");
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
                        Public.WriteLog("获取" + tag + "测量员：");
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
                        Public.WriteLog("获取" + tag + "试验员：");
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
                        Public.WriteLog("获取" + tag + "标准员：");
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

                        Public.WriteLog("获取" + tag + "技术工人：");
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

                            Public.WriteLog("获取" + tag + "技术工人-" + zyzglx + "：");

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

                        Public.WriteLog("获取" + tag + "特种作业人员：");
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

                            Public.WriteLog("获取" + tag + "技术工人-" + zyzglx + "：");

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
        /// 从江苏建设公共基础数据平台获取建设单位信息(没问题)
        /// </summary>
        /// <param name="DataJkLogID"></param>
        static void YourTask_PullDataFromSxxzx_Jsdw(string DataJkLogID)
        {
            Public.WriteLog("执行YourTask_PullDataFromSxxzx_Jsdw方法：");

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
            row_DataJkDataDetail_qyxx["MethodName"] = "getCorpInfo";
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


        #region 省外企业+人员
        //        static void YourTask_PullDataFromSxxzx_Swqyxx(string DataJkLogID)
        //        {
        //            Public.WriteLog("执行YourTask_PullDataFromSxxzx_qyxx方法：");
        //            string tag = Tag.江苏建设公共基础数据平台.ToString();
        //            string userID = "wxszjj01";
        //            DataService dataService = new DataService();
        //            DataTable dt_xzqhdm = dataService.Get_tbXzqdmDic();
        //            XmlHelper helper = new XmlHelper();

        //            DataDownService.dataDownService dataDownService = new DataDownService.dataDownService();
        //            DataRow row;

        //            //往数据监控日志表项添加一条记录
        //            DataTable dt_DataJkDataDetail_qyxx = dataService.GetSchema_DataJkDataDetail();
        //            long Id_DataJkDataDetail_qyxx = dataService.Get_DataJkDataDetailNewID().ToInt64();

        //            int allCount_qyxx = 0, success_count_qyxx = 0;
        //            DataRow row_DataJkDataDetail_qyxx = dt_DataJkDataDetail_qyxx.NewRow();
        //            dt_DataJkDataDetail_qyxx.Rows.Add(row_DataJkDataDetail_qyxx);

        //            row_DataJkDataDetail_qyxx["ID"] = Id_DataJkDataDetail_qyxx++;
        //            row_DataJkDataDetail_qyxx["DataJkLogID"] = DataJkLogID;
        //            row_DataJkDataDetail_qyxx["tableName"] = "UEPP_Qyjbxx";
        //            row_DataJkDataDetail_qyxx["MethodName"] = "getOutCorpInfo_Inc";
        //            row_DataJkDataDetail_qyxx["bz"] = "从江苏建设公共基础数据平台拉取省外企业信息";
        //            try
        //            {
        //                DataTable dt_SaveDataLog = dataService.GetSchema_SaveDataLog();

        //                NewDataService.NewDataService newDataService = new WxjzgcjczyTimerService.NewDataService.NewDataService();

        //                string beginDate = "2016-03-11";
        //                string endDate = DateTime.Now.ToString("yyyy-MM-dd");

        //                byte[] bytes;
        //                int index;
        //                string result;

        //                bytes = newDataService.getPersonRegCert(userID, "320200", "", "", "0");
        //                result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);

        //                #region  获取指定日期范围内发生变化的省外企业信息
        //                bytes = newDataService.getOutCorpInfo_Inc(userID, beginDate, endDate, "0");
        //                result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        //                //Public.WriteLog("省外企业信息.txt" , result);
        //                index = result.IndexOf("<ReturnInfo>");

        //                if (index >= 0)
        //                {
        //                    string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
        //                    if (string.IsNullOrEmpty(returnResult))
        //                    {
        //                        return;
        //                    }
        //                    ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
        //                    if (!returnInfo.Status)
        //                    {
        //                        return;
        //                    }
        //                }

        //                index = result.IndexOf("<OutCorpBasicInfo>");
        //                if (index >= 0)
        //                {
        //                    string corpBasicInfoString = result.Substring(index, result.LastIndexOf("</OutCorpBasicInfo>") - index + 19);
        //                    OutCorpBasicInfoBody outCorpBasicInfoBody = helper.DeserializeXML<OutCorpBasicInfoBody>("<OutCorpBasicInfoBody><OutCorpBasicInfoArray>" + corpBasicInfoString + "</OutCorpBasicInfoArray></OutCorpBasicInfoBody>");
        //                    if (outCorpBasicInfoBody != null)
        //                    {
        //                        foreach (OutCorpBasicInfo corpBasicInfo in outCorpBasicInfoBody.array)
        //                        {

        //                            if (corpBasicInfo.CorpCode.Length == 9)
        //                            {
        //                                corpBasicInfo.CorpCode = corpBasicInfo.CorpCode.Substring(0, 8) + '-' + corpBasicInfo.CorpCode.Substring(8, 1);
        //                            }
        //                            //检查该企业是否存在社会信用代码，若存在，则转化为社会信用代码
        //                            if (corpBasicInfo.CorpCode.Length == 10)
        //                            {
        //                                string qyShxydm = dataService.Get_UEPP_Qyjbxx_Shxydm(corpBasicInfo.CorpCode);
        //                                if (!string.IsNullOrEmpty(qyShxydm))
        //                                {
        //                                    corpBasicInfo.CorpCode = qyShxydm;

        //                                    string sql = "update  UEPP_Qyzs set qyID=@qyIDNew where qyID=@qyID;update UEPP_Qyzzmx set qyID=@qyIDNew where qyID=@qyID;update  UEPP_Qycsyw set qyID=@qyIDNew where qyID=@qyID";
        //                                    SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
        //                                    sp.Add("@qyID", corpBasicInfo.CorpCode);
        //                                    sp.Add("@qyIDNew", qyShxydm);
        //                                    dataService.ExecuteNonQuerySql2(sql, sp);
        //                                }
        //                            }

        //                            DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
        //                            row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_qyxx["ID"];
        //                            row_SaveDataLog["DataXml"] = "";
        //                            row_SaveDataLog["PKID"] = corpBasicInfo.CorpCode;
        //                            row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");
        //                            dt_SaveDataLog.Rows.Add(row_SaveDataLog);

        //                            try
        //                            {
        //                                #region  更新企业基本信息

        //                                DataTable dt = dataService.Get_uepp_Qyjbxx(corpBasicInfo.CorpCode);
        //                                if (dt.Rows.Count == 0)
        //                                {
        //                                    row = dt.NewRow();
        //                                    dt.Rows.Add(row);
        //                                }
        //                                else
        //                                {
        //                                    row = dt.Rows[0];
        //                                    if (!string.IsNullOrEmpty(corpBasicInfo.UpdateDate) && !string.IsNullOrEmpty(row["xgrqsj"].ToString2()))
        //                                        if (DateTime.Parse(row["xgrqsj"].ToString2()).ToString("yyyy-MM-dd") == DateTime.Parse(corpBasicInfo.UpdateDate).ToString("yyyy-MM-dd"))
        //                                        {
        //                                            continue;
        //                                        }
        //                                }
        //                                row["qyID"] = corpBasicInfo.CorpCode;
        //                                row["zzjgdm"] = corpBasicInfo.CorpCode;
        //                                row["tag"] = tag;

        //                                row["qymc"] = corpBasicInfo.CorpName;
        //                                row["yyzzzch"] = corpBasicInfo.LicenseNo;

        //                                if (!string.IsNullOrEmpty(corpBasicInfo.ProvinceCode.ToString2()))
        //                                {
        //                                    SqlParameterCollection sp = dataService.CreateSqlParameterCollection();

        //                                    row["Province"] = corpBasicInfo.ProvinceCode;
        //                                    sp.Add("@CodeInfo", corpBasicInfo.ProvinceCode.ToString2());
        //                                    string provinceCode = dataService.ExecuteSql("select top 1 Code from  UEPP_Code where CodeType='城市地区' and  CodeInfo=@CodeInfo", sp);
        //                                    if (!string.IsNullOrEmpty(provinceCode))
        //                                    {
        //                                        row["ProvinceID"] = provinceCode;
        //                                        sp.Clear();
        //                                        if (!string.IsNullOrEmpty(corpBasicInfo.CityCode.ToString2()))
        //                                        {
        //                                            row["City"] = corpBasicInfo.CityCode;

        //                                            sp.Add("@CodeInfo", corpBasicInfo.CityCode.ToString2());
        //                                            sp.Add("@parentCode", provinceCode);
        //                                            string cityCode = dataService.ExecuteSql("select top 1 Code from  UEPP_Code where CodeType='城市地区' and ParentCode=@parentCode and  CodeInfo=@CodeInfo", sp);
        //                                            if (!string.IsNullOrEmpty(cityCode))
        //                                            {
        //                                                row["CityID"] = cityCode;
        //                                            }

        //                                            sp.Clear();
        //                                            if (!string.IsNullOrEmpty(corpBasicInfo.CountyCode.ToString2()))
        //                                            {
        //                                                row["County"] = corpBasicInfo.CountyCode;

        //                                                sp.Add("@CodeInfo", corpBasicInfo.CountyCode.ToString2());
        //                                                sp.Add("@parentCode", cityCode);
        //                                                string countyCode = dataService.ExecuteSql("select top 1 Code from  UEPP_Code where CodeType='城市地区' and ParentCode=@parentCode and  CodeInfo=@CodeInfo", sp);
        //                                                if (!string.IsNullOrEmpty(countyCode))
        //                                                {
        //                                                    row["CountyID"] = countyCode;
        //                                                }

        //                                            }
        //                                        }
        //                                    }
        //                                }

        //                                row["zcdd"] = corpBasicInfo.RegAddress;
        //                                row["xxdd"] = corpBasicInfo.CorpAddress;
        //                                if (!string.IsNullOrEmpty(corpBasicInfo.FoundDate))
        //                                    row["clrq"] = corpBasicInfo.FoundDate;
        //                                row["jjxz"] = corpBasicInfo.CorpTypeDesc.ToString2().Trim();
        //                                if (!string.IsNullOrEmpty(corpBasicInfo.CorpTypeDesc))
        //                                {
        //                                    SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
        //                                    sp.Add("@CodeInfo", corpBasicInfo.CorpTypeDesc.ToString2().Trim());
        //                                    string jjxzID = dataService.ExecuteSql("select * from  UEPP_Code where CodeType='企业经济性质' and  CodeInfo=@CodeInfo", sp);
        //                                    if (!string.IsNullOrEmpty(jjxzID))
        //                                    {
        //                                        row["jjxzID"] = jjxzID;
        //                                    }
        //                                }

        //                                row["zczb"] = corpBasicInfo.RegCapital;
        //                                row["cz"] = corpBasicInfo.Fax;
        //                                row["lxdh"] = corpBasicInfo.LinkPhone;
        //                                row["fddbr"] = corpBasicInfo.LegalMan;

        //                                if (!string.IsNullOrEmpty(corpBasicInfo.UpdateDate))
        //                                    row["xgrqsj"] = corpBasicInfo.UpdateDate;
        //                                else
        //                                    row["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd");

        //                                row["DataState"] = 0;
        //                                allCount_qyxx++;
        //                                if (!dataService.Submit_uepp_qyjbxx(dt))
        //                                {
        //                                    row_SaveDataLog["SaveState"] = 0;
        //                                    row_SaveDataLog["Msg"] = "从江苏建设公共基础数据平台获取的单位：" + corpBasicInfo.CorpName + "," + corpBasicInfo.CorpCode + "，基本信息保存失败！";
        //                                    continue;
        //                                }
        //                                else
        //                                {
        //                                    success_count_qyxx++;
        //                                    row_SaveDataLog["SaveState"] = 1;
        //                                    row_SaveDataLog["Msg"] = "";
        //                                }
        //                                #endregion
        //                            }
        //                            catch (Exception ex)
        //                            {
        //                                row_SaveDataLog["SaveState"] = 0;
        //                                row_SaveDataLog["Msg"] = ex.Message;
        //                            }
        //                        }
        //                    }
        //                }
        //                #endregion

        //                //增量获取企业资质信息
        //                bytes = newDataService.getOutCorpQual_Inc(userID, beginDate, endDate, "0");

        //                result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        //                //Public.WriteLog("省外企业资质信息.txt",  result);

        //                index = result.IndexOf("<ReturnInfo>");

        //                if (index >= 0)
        //                {
        //                    string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
        //                    if (string.IsNullOrEmpty(returnResult))
        //                    {
        //                        return;
        //                    }
        //                    ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
        //                    if (!returnInfo.Status)
        //                    {
        //                        return;
        //                    }
        //                }

        //                index = result.IndexOf("<OutCorpCertQual>");
        //                if (index >= 0)
        //                {
        //                    string returnResult = result.Substring(index, result.LastIndexOf("</OutCorpCertQual>") - index + 18);
        //                    if (string.IsNullOrEmpty(returnResult))
        //                    {
        //                        return;
        //                    }
        //                    OutCorpCertQualBody outCorpCertQualBody = helper.DeserializeXML<OutCorpCertQualBody>("<OutCorpCertQualBody><OutCorpCertQualArray>" + returnResult + "</OutCorpCertQualArray></OutCorpCertQualBody>");

        //                    #region 更新企业资质(TCorpCertQual)
        //                    if (outCorpCertQualBody != null)
        //                    {
        //                        foreach (OutCorpCertQual corpCertQual in outCorpCertQualBody.array)
        //                        {
        //                            try
        //                            {
        //                                if (corpCertQual.CorpCode.Length == 9)
        //                                {
        //                                    corpCertQual.CorpCode = corpCertQual.CorpCode.Substring(0, 8) + '-' + corpCertQual.CorpCode.Substring(8, 1);
        //                                }
        //                                //检查该企业是否存在社会信用代码，若存在，则转化为社会信用代码
        //                                if (corpCertQual.CorpCode.Length == 10)
        //                                {
        //                                    string qyShxydm = dataService.Get_UEPP_Qyjbxx_Shxydm(corpCertQual.CorpCode);
        //                                    if (!string.IsNullOrEmpty(qyShxydm))
        //                                    {
        //                                        corpCertQual.CorpCode = qyShxydm;

        //                                        string sql = "update  UEPP_Qyzs set qyID=@qyIDNew where qyID=@qyID;update UEPP_Qyzzmx set qyID=@qyIDNew where qyID=@qyID;update  UEPP_Qycsyw set qyID=@qyIDNew where qyID=@qyID";
        //                                        SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
        //                                        sp.Add("@qyID", corpCertQual.CorpCode);
        //                                        sp.Add("@qyIDNew", qyShxydm);
        //                                        dataService.ExecuteNonQuerySql2(sql, sp);
        //                                    }
        //                                }

        //                                string csywlxID = "", csywlx = "";
        //                                switch (corpCertQual.CertType)
        //                                {
        //                                    //施工
        //                                    case "建筑业":
        //                                        csywlxID = "1";
        //                                        csywlx = "建筑施工";
        //                                        break;
        //                                    case "城市园林绿化":
        //                                        csywlxID = "3";
        //                                        csywlx = "园林绿化";
        //                                        break;
        //                                    case "设计与施工一体化":
        //                                        csywlxID = "2";
        //                                        csywlx = "设计施工一体化";
        //                                        break;
        //                                    case "房屋拆迁":
        //                                        csywlxID = "13";
        //                                        csywlx = "房屋拆迁";
        //                                        break;
        //                                    case "安全生产许可证":
        //                                        csywlxID = "14";
        //                                        csywlx = "安全生产许可证";
        //                                        break;
        //                                    //勘察
        //                                    case "工程勘察":
        //                                        csywlxID = "5";
        //                                        csywlx = "工程勘察";
        //                                        break;
        //                                    //设计
        //                                    case "工程设计":
        //                                        csywlxID = "6";
        //                                        csywlx = "工程设计";
        //                                        break;
        //                                    case "城市规划":
        //                                        csywlxID = "18";
        //                                        csywlx = "城市规划";
        //                                        break;
        //                                    case "外商城市规划":
        //                                        csywlxID = "19";
        //                                        csywlx = "外商城市规划";
        //                                        break;

        //                                    //中介机构
        //                                    case "工程招标代理":
        //                                        csywlxID = "7";
        //                                        csywlx = "招标代理";
        //                                        break;
        //                                    case "工程监理":
        //                                        csywlxID = "4";
        //                                        csywlx = "工程监理";
        //                                        break;
        //                                    case "工程造价咨询":
        //                                        csywlxID = "8";
        //                                        csywlx = "造价咨询";
        //                                        break;
        //                                    case "工程质量检测":
        //                                        csywlxID = "9";
        //                                        csywlx = "工程检测";
        //                                        break;
        //                                    case "施工图审查":
        //                                        csywlxID = "15";
        //                                        csywlx = "施工图审查";
        //                                        break;
        //                                    case "房地产估价":
        //                                        csywlxID = "16";
        //                                        csywlx = "房地产估价";
        //                                        break;
        //                                    case "物业服务":
        //                                        csywlxID = "17";
        //                                        csywlx = "物业服务";
        //                                        break;
        //                                    default:
        //                                        break;
        //                                }
        //                                if (string.IsNullOrEmpty(csywlxID))
        //                                    continue;

        //                                #region 企业从事业务类型

        //                                DataTable dt_qycsyw = dataService.Get_uepp_Qycsyw_sjsgyth(corpCertQual.CorpCode, csywlxID);

        //                                DataRow tempRow_qycsyw;

        //                                if (dt_qycsyw.Rows.Count == 0)
        //                                {
        //                                    tempRow_qycsyw = dt_qycsyw.NewRow();
        //                                    dt_qycsyw.Rows.Add(tempRow_qycsyw);
        //                                    tempRow_qycsyw["qyID"] = corpCertQual.CorpCode;
        //                                }
        //                                else
        //                                {
        //                                    tempRow_qycsyw = dt_qycsyw.Rows[0];
        //                                }

        //                                tempRow_qycsyw["csywlxID"] = csywlxID;
        //                                tempRow_qycsyw["csywlx"] = csywlx;

        //                                tempRow_qycsyw["balxID"] = "1";
        //                                tempRow_qycsyw["balx"] = "长期备案";
        //                                tempRow_qycsyw["DataState"] = "0";
        //                                tempRow_qycsyw["tag"] = tag;

        //                                if (!string.IsNullOrEmpty(corpCertQual.UpdateDate))
        //                                    tempRow_qycsyw["xgrqsj"] = corpCertQual.UpdateDate;
        //                                else
        //                                    tempRow_qycsyw["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd");

        //                                if (dt_qycsyw.Rows.Count > 0)
        //                                {
        //                                    dataService.Submit_uepp_qycsyw(dt_qycsyw);
        //                                }
        //                                #endregion

        //                                DataTable dt_jsdw_zzmx = dataService.Get_uepp_zzmxxx_qyxx(corpCertQual.CorpCode, csywlxID);

        //                                int rowIndex = -1;

        //                                for (int i = 0; i < dt_jsdw_zzmx.Rows.Count; i++)
        //                                {
        //                                    //if (dt_jsdw_zzmx.Rows[i].RowState == DataRowState.Deleted)
        //                                    //    continue;

        //                                    if (csywlxID == dt_jsdw_zzmx.Rows[i]["csywlxID"].ToString2()
        //                                        && (
        //                                         corpCertQual.TradeType == "工程勘察综合类" && dt_jsdw_zzmx.Rows[i]["zzxl"].ToString2() == "综合类"
        //                                         || corpCertQual.TradeType == "工程勘察专业类" && dt_jsdw_zzmx.Rows[i]["zzxl"].ToString2() == "专业类"
        //                                         || corpCertQual.TradeType == "工程勘察劳务类" && dt_jsdw_zzmx.Rows[i]["zzxl"].ToString2() == "劳务类"
        //                                         || corpCertQual.TradeType == dt_jsdw_zzmx.Rows[i]["zzxl"].ToString2()
        //                                        )
        //                                        && (dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "岩土工程勘察" && corpCertQual.MajorType == "岩土工程（勘察）"
        //                                        || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "岩土工程设计" && corpCertQual.MajorType == "岩土工程（设计）"
        //                                          || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "岩土工程测试、监测、检测" && corpCertQual.MajorType == "岩土工程（物探测试检测监测）"
        //                                          || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "岩土工程咨询、监理" && corpCertQual.MajorType == "岩土工程（咨询监理）"

        //                                          || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "建筑装修装饰" && corpCertQual.MajorType == "建筑装饰装修工程"
        //                                          || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "模板作业" && corpCertQual.MajorType == "模板作业分包"
        //                                          || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "脚手架搭设作业" && corpCertQual.MajorType == "脚手架作业分包"
        //                                          || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == corpCertQual.MajorType
        //                                        )
        //                                        )
        //                                    {
        //                                        rowIndex = i;
        //                                        break;
        //                                    }
        //                                }

        //                                if (rowIndex < 0)
        //                                {
        //                                    row = dt_jsdw_zzmx.NewRow();
        //                                    dt_jsdw_zzmx.Rows.Add(row);
        //                                    row["ID"] = dataService.Get_uepp_qyxxmx_NewID();
        //                                    row["qyID"] = corpCertQual.CorpCode;
        //                                    row["csywlx"] = csywlx;
        //                                    row["csywlxID"] = csywlxID;
        //                                }
        //                                else
        //                                {
        //                                    row = dt_jsdw_zzmx.Rows[rowIndex];
        //                                    if (!string.IsNullOrEmpty(corpCertQual.UpdateDate) && !string.IsNullOrEmpty(row["xgrqsj"].ToString2()))
        //                                        if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(corpCertQual.UpdateDate).ToString("yyyy-MM-dd"))
        //                                        {
        //                                            continue;
        //                                        }
        //                                }
        //                                if (corpCertQual.IsMaster == "主项")
        //                                    row["zzbz"] = "主项";
        //                                else
        //                                    row["zzbz"] = "增项";

        //                                if (corpCertQual.TradeType == "工程勘察综合类")
        //                                {
        //                                    row["zzxl"] = "综合类";
        //                                    row["zzxlID"] = "9";
        //                                }
        //                                else
        //                                    if (corpCertQual.TradeType == "工程勘察专业类")
        //                                    {
        //                                        row["zzxl"] = "专业类";
        //                                        row["zzxlID"] = "10";
        //                                    }
        //                                    else
        //                                        if (corpCertQual.TradeType == "工程勘察劳务类")
        //                                        {
        //                                            row["zzxl"] = "劳务类";
        //                                            row["zzxlID"] = "11";
        //                                        }
        //                                        else
        //                                            if (corpCertQual.TradeType == "工程设计综合")
        //                                            {
        //                                                row["zzxl"] = "综合资质";
        //                                                row["zzxlID"] = "12";
        //                                            }
        //                                            else
        //                                            {
        //                                                row["zzxl"] = corpCertQual.TradeType;
        //                                                if (!string.IsNullOrEmpty(csywlxID))
        //                                                {
        //                                                    string sql = @"select Code from UEPP_Code where  CodeType='企业资质序列' and ParentCodeType='企业从事业务类型'
        // and ParentCode=@parentCode and CodeInfo=@CodeInfo ";
        //                                                    SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
        //                                                    sp.Add("@CodeInfo", corpCertQual.TradeType);
        //                                                    sp.Add("@parentCode", csywlxID);
        //                                                    string zzxlID = dataService.ExecuteSql(sql, sp);
        //                                                    if (!string.IsNullOrEmpty(zzxlID))
        //                                                        row["zzxlID"] = zzxlID;
        //                                                }
        //                                            }

        //                                if (corpCertQual.MajorType == "岩土工程（勘察）")
        //                                {
        //                                    row["zzlb"] = "岩土工程勘察";
        //                                    row["zzlbID"] = "300";
        //                                }
        //                                else
        //                                    if (corpCertQual.MajorType == "岩土工程（设计）")
        //                                    {
        //                                        row["zzlb"] = "岩土工程设计";
        //                                        row["zzlbID"] = "301";
        //                                    }
        //                                    else
        //                                        if (corpCertQual.MajorType == "岩土工程（物探测试检测监测）")
        //                                        {
        //                                            row["zzlb"] = "岩土工程测试、监测、检测";
        //                                            row["zzlbID"] = "302";
        //                                        }
        //                                        else
        //                                            if (corpCertQual.MajorType == "岩土工程（咨询监理））")
        //                                            {
        //                                                row["zzlb"] = "岩土工程咨询、监理";
        //                                                row["zzlbID"] = "303";
        //                                            }
        //                                            else
        //                                                if (corpCertQual.TradeType == "建筑装饰装修工程")
        //                                                {
        //                                                    row["zzlb"] = "建筑装修装饰";
        //                                                    row["zzlbID"] = "33";
        //                                                }
        //                                                else if (corpCertQual.TradeType == "模板作业分包")
        //                                                {
        //                                                    row["zzlb"] = "模板作业";
        //                                                    row["zzlbID"] = "128";
        //                                                }
        //                                                else
        //                                                    if (corpCertQual.TradeType == "脚手架作业分包")
        //                                                    {
        //                                                        row["zzlb"] = "脚手架搭设作业";
        //                                                        row["zzlbID"] = "127";
        //                                                    }
        //                                                    else
        //                                                    {
        //                                                        row["zzlb"] = corpCertQual.MajorType;
        //                                                        if (!string.IsNullOrEmpty(row["zzxlID"].ToString2().Trim()))
        //                                                        {
        //                                                            string sql = @"select Code from UEPP_Code where  CodeType='企业资质类别' and  ParentCodeType='企业资质序列'
        // and ParentCode=@parentCode and (CodeInfo=@CodeInfo or CodeInfo=@CodeInfo1) ";
        //                                                            SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
        //                                                            sp.Add("@CodeInfo", corpCertQual.MajorType);
        //                                                            sp.Add("@parentCode", row["zzxlID"]);
        //                                                            sp.Add("@CodeInfo1", corpCertQual.MajorType.ToString2().Replace("分包", ""));
        //                                                            string zzlbID = dataService.ExecuteSql(sql, sp);
        //                                                            if (!string.IsNullOrEmpty(zzlbID))
        //                                                                row["zzlbID"] = zzlbID;
        //                                                        }
        //                                                    }




        //                                row["zzdj"] = corpCertQual.TitleLevel;

        //                                if (!string.IsNullOrEmpty(corpCertQual.TitleLevel))
        //                                {
        //                                    SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
        //                                    sp.Add("@CodeInfo", corpCertQual.TitleLevel);

        //                                    string sql = "select Code from UEPP_Code  where  CodeType='企业资质等级' and ParentCodeType='企业资质序列' and CodeInfo=@CodeInfo ";
        //                                    string zzdjID = dataService.ExecuteSql(sql, sp);
        //                                    if (!string.IsNullOrEmpty(zzdjID))
        //                                        row["zzdjID"] = zzdjID;
        //                                    else
        //                                    {
        //                                        // 新增代码表
        //                                    }
        //                                }

        //                                if (corpCertQual.Status == "0")
        //                                {
        //                                    row["DataState"] = -1;
        //                                }
        //                                else
        //                                {
        //                                    row["DataState"] = 0;
        //                                }

        //                                row["tag"] = tag;
        //                                row["xgrqsj"] = corpCertQual.UpdateDate;

        //                                if (!dataService.Submit_uepp_qyzzmx(dt_jsdw_zzmx))
        //                                {
        //                                    Public.WriteLog("单位ID：" + corpCertQual.CorpCode + "，企业资质保存失败！");
        //                                }
        //                            }
        //                            catch (Exception ex)
        //                            {
        //                                Public.WriteLog("保存企业资质时出现异常：" + ex.Message);
        //                            }
        //                        }
        //                    }
        //                    #endregion
        //                }

        //                #region 更新企业资质证书信息(TCorpCertInfo)
        //                //if (corpCertInfoArr != null)
        //                //{
        //                //    foreach (CorpCertInfo corpCertInfo in corpCertInfoArr.array)
        //                //    {
        //                //        try
        //                //        {
        //                //            if (corpCertInfo.CorpCode.Length == 9)
        //                //            {
        //                //                corpCertInfo.CorpCode = corpCertInfo.CorpCode.Substring(0, 8) + '-' + corpCertInfo.CorpCode.Substring(8, 1);
        //                //            }
        //                //            //检查该企业是否存在社会信用代码，若存在，则转化为社会信用代码
        //                //            if (corpCertInfo.CorpCode.Length == 10)
        //                //            {
        //                //                string qyShxydm = dataService.Get_UEPP_Qyjbxx_Shxydm(corpCertInfo.CorpCode);
        //                //                if (!string.IsNullOrEmpty(qyShxydm))
        //                //                {
        //                //                    corpCertInfo.CorpCode = qyShxydm;
        //                //                }
        //                //            }

        //                //            int rowIndex = -1;
        //                //            string csywlxID = "", csywlx = "";
        //                //            switch (corpCertInfo.CertType)
        //                //            {
        //                //                //施工
        //                //                case "建筑业":
        //                //                    csywlxID = "1";
        //                //                    csywlx = "建筑施工";
        //                //                    break;
        //                //                case "城市园林绿化":
        //                //                    csywlxID = "3";
        //                //                    csywlx = "园林绿化";
        //                //                    break;
        //                //                case "设计与施工一体化":
        //                //                    csywlxID = "2";
        //                //                    csywlx = "设计施工一体化";
        //                //                    break;
        //                //                case "房屋拆迁":
        //                //                    csywlxID = "13";
        //                //                    csywlx = "房屋拆迁";
        //                //                    break;
        //                //                case "安全生产许可证":
        //                //                    csywlxID = "14";
        //                //                    csywlx = "安全生产许可证";
        //                //                    break;
        //                //                //勘察
        //                //                case "工程勘察":
        //                //                    csywlxID = "5";
        //                //                    csywlx = "工程勘察";
        //                //                    break;
        //                //                //设计
        //                //                case "工程设计":
        //                //                    csywlxID = "6";
        //                //                    csywlx = "工程设计";
        //                //                    break;
        //                //                case "城市规划":
        //                //                    csywlxID = "18";
        //                //                    csywlx = "城市规划";
        //                //                    break;
        //                //                case "外商城市规划":
        //                //                    csywlxID = "19";
        //                //                    csywlx = "外商城市规划";
        //                //                    break;

        //                //                //中介机构
        //                //                case "工程招标代理":
        //                //                    csywlxID = "7";
        //                //                    csywlx = "招标代理";
        //                //                    break;
        //                //                case "工程监理":
        //                //                    csywlxID = "4";
        //                //                    csywlx = "工程监理";
        //                //                    break;
        //                //                case "工程造价咨询":
        //                //                    csywlxID = "8";
        //                //                    csywlx = "造价咨询";
        //                //                    break;
        //                //                case "工程质量检测":
        //                //                    csywlxID = "9";
        //                //                    csywlx = "工程检测";
        //                //                    break;
        //                //                case "施工图审查":
        //                //                    csywlxID = "15";
        //                //                    csywlx = "施工图审查";
        //                //                    break;
        //                //                case "房地产估价":
        //                //                    csywlxID = "16";
        //                //                    csywlx = "房地产估价";
        //                //                    break;
        //                //                case "物业服务":
        //                //                    csywlxID = "17";
        //                //                    csywlx = "物业服务";
        //                //                    break;
        //                //                default:
        //                //                    break;
        //                //            }
        //                //            if (string.IsNullOrEmpty(csywlxID))
        //                //                continue;

        //                //            DataTable dt_qy_zzzs = dataService.Get_uepp_zzzsxx_qyxx(corpCertInfo.CorpCode);

        //                //            for (int i = 0; i < dt_qy_zzzs.Rows.Count; i++)
        //                //            {
        //                //                //if (dt_jsdw_zzzs.Rows[i].RowState == DataRowState.Deleted)
        //                //                //    continue;

        //                //                if (corpCertInfo.CertType == dt_qy_zzzs.Rows[i]["csywlx"].ToString2()
        //                //                    && dt_qy_zzzs.Rows[i]["zsbh"].ToString2() == corpCertInfo.CertCode
        //                //                    && dt_qy_zzzs.Rows[i]["csywlx"].ToString2() == corpCertInfo.CertType)
        //                //                {
        //                //                    rowIndex = i;
        //                //                    break;
        //                //                }
        //                //            }

        //                //            if (rowIndex < 0)
        //                //            {
        //                //                row = dt_qy_zzzs.NewRow();
        //                //                dt_qy_zzzs.Rows.Add(row);
        //                //                row["zsjlId"] = dataService.Get_uepp_qyQyzs_NewID();
        //                //            }
        //                //            else
        //                //            {
        //                //                row = dt_qy_zzzs.Rows[rowIndex];
        //                //                if (!string.IsNullOrEmpty(corpCertInfo.UpdateDate))
        //                //                    if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(corpCertInfo.UpdateDate).ToString("yyyy-MM-dd"))
        //                //                    {
        //                //                        continue;
        //                //                    }
        //                //            }
        //                //            row["qyID"] = corpCertInfo.CorpCode;
        //                //            row["csywlx"] = csywlx;
        //                //            row["csywlxID"] = csywlxID;

        //                //            //if (!string.IsNullOrEmpty(corpCertInfo.CertType))
        //                //            //{
        //                //            //    SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
        //                //            //    sp.Add("@parentCode", csywlxID);
        //                //            //    sp.Add("@CodeInfo", corpCertInfo.CertType);

        //                //            //    string sql = @"select * from UEPP_Code  where  CodeType ='企业证书类型' and ParentCodeType='企业从事业务类型' and ParentCode=@parentCode and CodeInfo=@CodeInfo ";
        //                //            //    string zslxID = dataService.ExecuteSql(sql, sp);
        //                //            //    if (!string.IsNullOrEmpty(zslxID))
        //                //            //    {
        //                //            //        row["zslxID"] = zslxID;
        //                //            //    }
        //                //            //}
        //                //            //row["zslx"] = "开发企业资质证书";

        //                //            row["sfzzz"] = "1";
        //                //            string zslx = "", zslxID = "";
        //                //            switch (corpCertInfo.CertType)
        //                //            {
        //                //                //施工
        //                //                case "建筑业":
        //                //                    zslxID = "10";
        //                //                    zslx = "建筑业资质证";
        //                //                    break;
        //                //                case "城市园林绿化":
        //                //                    zslxID = "30";
        //                //                    zslx = "园林绿化资质证";
        //                //                    break;
        //                //                case "设计与施工一体化":
        //                //                    zslxID = "20";
        //                //                    zslx = "设计施工一体化资质证";
        //                //                    break;
        //                //                case "房屋拆迁":
        //                //                    zslxID = "130";
        //                //                    zslx = "房屋拆迁资质证";
        //                //                    break;
        //                //                case "安全生产许可证":
        //                //                    zslxID = "140";
        //                //                    zslx = "安全生产许可证";
        //                //                    break;
        //                //                //勘察
        //                //                case "工程勘察":
        //                //                    zslxID = "51";
        //                //                    csywlx = "省工程勘察资质证";
        //                //                    break;
        //                //                //设计
        //                //                case "工程设计":
        //                //                    zslxID = "61";
        //                //                    zslx = "省工程设计资质证";
        //                //                    break;
        //                //                case "城市规划":
        //                //                    zslxID = "18";
        //                //                    zslx = "城市规划资质证";
        //                //                    break;
        //                //                case "外商城市规划":
        //                //                    zslxID = "19";
        //                //                    zslx = "外商城市规划资质证";
        //                //                    break;

        //                //                //中介机构
        //                //                case "工程招标代理":
        //                //                    zslxID = "70";
        //                //                    zslx = "招标代理资质证";
        //                //                    break;
        //                //                case "工程监理":
        //                //                    zslxID = "40";
        //                //                    zslx = "工程监理资质证";
        //                //                    break;
        //                //                case "工程造价咨询":
        //                //                    zslxID = "80";
        //                //                    csywlx = "造价咨询资质证";
        //                //                    break;
        //                //                case "工程质量检测":
        //                //                    zslxID = "90";
        //                //                    zslx = "工程检测资质证";
        //                //                    break;
        //                //                case "施工图审查":
        //                //                    zslxID = "150";
        //                //                    csywlx = "施工图审查资质证";
        //                //                    break;
        //                //                case "房地产估价":
        //                //                    zslxID = "160";
        //                //                    zslx = "房地产估价资质证";
        //                //                    break;
        //                //                case "物业服务":
        //                //                    zslxID = "170";
        //                //                    zslx = "物业服务资质证";
        //                //                    break;
        //                //                default:
        //                //                    break;
        //                //            }
        //                //            row["zslxID"] = zslxID;
        //                //            row["zslx"] = zslx;
        //                //            row["zsbh"] = corpCertInfo.CertCode;
        //                //            if (!string.IsNullOrEmpty(corpCertInfo.ValidDate.Trim()))
        //                //                row["zsyxzrq"] = corpCertInfo.ValidDate;
        //                //            if (!string.IsNullOrEmpty(corpCertInfo.IssueDate.Trim()))
        //                //            {
        //                //                row["fzrq"] = corpCertInfo.IssueDate;
        //                //                row["zsyxqrq"] = corpCertInfo.IssueDate;
        //                //            }

        //                //            row["fzdw"] = corpCertInfo.IssueOrgan;
        //                //            row["xgrqsj"] = corpCertInfo.UpdateDate;
        //                //            row["xgr"] = "定时服务";
        //                //            row["tag"] = tag;
        //                //            row["DataState"] = 0;

        //                //            if (!dataService.Submit_uepp_qyzzzs(dt_qy_zzzs))
        //                //            {
        //                //                Public.WriteLog("单位ID：" + corpCertInfo.CorpCode + "，企业资质证书信息保存失败！");
        //                //            }

        //                //        }
        //                //        catch (Exception ex)
        //                //        {
        //                //            Public.WriteLog("保存企业资质证书信息时出现异常：" + ex.Message);
        //                //        }
        //                //    }
        //                //}
        //                #endregion

        //                if (dt_SaveDataLog.Rows.Count > 0)
        //                    dataService.Submit_SaveDataLog(dt_SaveDataLog);

        //                row_DataJkDataDetail_qyxx["allCount"] = allCount_qyxx;
        //                row_DataJkDataDetail_qyxx["successCount"] = success_count_qyxx;
        //                row_DataJkDataDetail_qyxx["IsOk"] = 1;
        //                row_DataJkDataDetail_qyxx["ErrorMsg"] = "";

        //                if (dt_DataJkDataDetail_qyxx.Rows.Count > 0)
        //                    dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_qyxx);
        //            }
        //            catch (Exception ex)
        //            {
        //                row_DataJkDataDetail_qyxx["allCount"] = allCount_qyxx;
        //                row_DataJkDataDetail_qyxx["successCount"] = success_count_qyxx;
        //                row_DataJkDataDetail_qyxx["IsOk"] = 0;
        //                row_DataJkDataDetail_qyxx["ErrorMsg"] = "从江苏建设公共基础数据平台获取省外企业信息时出现异常:" + ex.Message;

        //                if (dt_DataJkDataDetail_qyxx.Rows.Count > 0)
        //                    dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_qyxx);
        //            }

        //        }

        //        static void YourTask_PullDataFromSxxzx_Swryxx(string DataJkLogID)
        //        {
        //            int allCount_ryxx = 0, success_count_ryxx = 0;
        //            string ryzyzglx = "", ryzyzglxID = "";
        //            DataRow row;
        //            Public.WriteLog("执行YourTask_PullDataFromSxxzx_Swryxx方法：");
        //            string tag = Tag.江苏建设公共基础数据平台.ToString();
        //            string userID = "wxszjj01";
        //            DataService dataService = new DataService();
        //            XmlHelper helper = new XmlHelper();

        //            NewDataService.NewDataService newDataService = new WxjzgcjczyTimerService.NewDataService.NewDataService();
        //            byte[] bytes = newDataService.getOutPersonCert(userID, "0");

        //            string result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        //            Public.WriteLog("省外人员信息.txt");
        //            int index = result.IndexOf("<ReturnInfo>");
        //            if (index >= 0)
        //            {
        //                string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
        //                if (string.IsNullOrEmpty(returnResult))
        //                {
        //                    return;
        //                }

        //                ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
        //                if (!returnInfo.Status)
        //                {
        //                    return;
        //                }

        //            }
        //            else
        //            {
        //                return;
        //            }

        //            index = result.IndexOf("<OutChiefPerson>");
        //            if (index >= 0)
        //            {
        //                string personBasicInfoString = result.Substring(index, result.LastIndexOf("</OutChiefPerson>") - index + 17);
        //                PersonBasicInfoBody personBasicInfoBody = helper.DeserializeXML<PersonBasicInfoBody>("<PersonBasicInfoBody><PersonBasicInfoArray>" + personBasicInfoString + "</PersonBasicInfoArray></PersonBasicInfoBody>");

        //                //PersonRegCertBody personRegCertBody = new PersonRegCertBody();
        //                //PersonRegMajorBody personRegMajorBody = new PersonRegMajorBody();

        //                //index = result.IndexOf("<PersonRegCert>");
        //                //if (index >= 0)
        //                //{

        //                //    string personRegCertString = result.Substring(index, result.LastIndexOf("</PersonRegCert>") - index + 16);
        //                //    personRegCertBody = helper.DeserializeXML<PersonRegCertBody>("<PersonRegCertBody><PersonRegCertArray>" + personRegCertString + "</PersonRegCertArray></PersonRegCertBody>");

        //                //}

        //                //index = result.IndexOf("<PersonRegMajor>");
        //                //if (index >= 0)
        //                //{
        //                //    string personRegMajorString = result.Substring(index, result.LastIndexOf("</PersonRegMajor>") - index + 17);
        //                //    personRegMajorBody = helper.DeserializeXML<PersonRegMajorBody>("<PersonRegMajorBody><PersonRegMajorArray>" + personRegMajorString + "</PersonRegMajorArray></PersonRegMajorBody>");

        //                //}

        //                #region

        //                if (personBasicInfoBody != null)
        //                {
        //                    int rowIndex = -1;
        //                    DataTable dt_SaveDataLog = dataService.GetSchema_SaveDataLog();
        //                    //往数据监控日志表项添加一条记录
        //                    DataTable dt_DataJkDataDetail_qyxx = dataService.GetSchema_DataJkDataDetail();
        //                    long Id_DataJkDataDetail_qyxx = dataService.Get_DataJkDataDetailNewID().ToInt64();

        //                    int allCount_qyxx = 0, success_count_qyxx = 0;
        //                    DataRow row_DataJkDataDetail_qyxx = dt_DataJkDataDetail_qyxx.NewRow();
        //                    dt_DataJkDataDetail_qyxx.Rows.Add(row_DataJkDataDetail_qyxx);

        //                    row_DataJkDataDetail_qyxx["ID"] = Id_DataJkDataDetail_qyxx++;
        //                    row_DataJkDataDetail_qyxx["DataJkLogID"] = DataJkLogID;
        //                    row_DataJkDataDetail_qyxx["tableName"] = "UEPP_Ryjbxx";
        //                    row_DataJkDataDetail_qyxx["MethodName"] = "getOutPersonCert";
        //                    row_DataJkDataDetail_qyxx["bz"] = "从江苏建设公共基础数据平台拉取省外人员信息";


        //                    foreach (PersonBasicInfo personBasicInfo in personBasicInfoBody.array)
        //                    {
        //                        DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
        //                        row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_qyxx["ID"];
        //                        row_SaveDataLog["DataXml"] = "";
        //                        row_SaveDataLog["PKID"] = personBasicInfo.IDCardNo;
        //                        row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");

        //                        try
        //                        {
        //                            #region 人员基本信息
        //                            DataTable dt_ryxx = dataService.Get_UEPP_Ryjbxx(personBasicInfo.IDCardNo);
        //                            if (dt_ryxx.Rows.Count == 0)
        //                            {
        //                                row = dt_ryxx.NewRow();
        //                                dt_ryxx.Rows.Add(row);
        //                                row["ryID"] = personBasicInfo.IDCardNo;
        //                            }
        //                            else
        //                            {
        //                                row = dt_ryxx.Rows[0];
        //                                if (!string.IsNullOrEmpty(row["xgrqsj"].ToString2()) && !string.IsNullOrEmpty(personBasicInfo.UpdateDate))
        //                                    if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(personBasicInfo.UpdateDate).ToString("yyyy-MM-dd"))
        //                                    {
        //                                        continue;
        //                                    }
        //                            }
        //                            row["tag"] = tag;
        //                            row["xm"] = personBasicInfo.PersonName;
        //                            switch (personBasicInfo.IDCardType)
        //                            {
        //                                case "身份证":
        //                                    row["zjlxID"] = "1";
        //                                    break;
        //                                case "护照":
        //                                    row["zjlxID"] = "3";
        //                                    break;
        //                                case "军官证":
        //                                    row["zjlxID"] = "2";
        //                                    break;
        //                                case "台湾居民身份证":
        //                                    row["zjlxID"] = "4";
        //                                    break;
        //                                case "香港永久性居民身份证":
        //                                    row["zjlxID"] = "5";
        //                                    break;
        //                                case "警官证":
        //                                    row["zjlxID"] = "6";
        //                                    break;
        //                                case "其他":
        //                                    row["zjlxID"] = "9";
        //                                    break;
        //                            }

        //                            row["zjlx"] = personBasicInfo.IDCardType;
        //                            row["zjhm"] = personBasicInfo.IDCardNo;
        //                            row["xb"] = personBasicInfo.Sex;
        //                            if (!string.IsNullOrEmpty(personBasicInfo.Birthday))
        //                                row["csrq"] = personBasicInfo.Birthday;
        //                            row["mz"] = personBasicInfo.Nationality;
        //                            row["zc"] = personBasicInfo.ProTitle;
        //                            row["zw"] = personBasicInfo.DutyDesc;
        //                            if (string.IsNullOrEmpty(personBasicInfo.UpdateDate))
        //                                personBasicInfo.UpdateDate = DateTime.Now.ToString("yyyy-MM-dd");
        //                            row["xgrqsj"] = personBasicInfo.UpdateDate;

        //                            row["xgr"] = "定时服务";
        //                            row["DataState"] = 0;
        //                            row["AJ_EXISTINIDCARDS"] = "2";
        //                            row["AJ_IsRefuse"] = "0";

        //                            allCount_ryxx++;
        //                            if (!dataService.Submit_uepp_ryjbxx(dt_ryxx))
        //                            {
        //                                //Public.WriteLog("建设单位人员信息保存失败，ryID：" + personBasicInfo.IDCardNo);
        //                                row_SaveDataLog["SaveState"] = 0;
        //                                row_SaveDataLog["Msg"] = "建设单位人员信息保存失败，ryID：" + personBasicInfo.IDCardNo;
        //                            }
        //                            else
        //                            {
        //                                success_count_ryxx++;

        //                                row_SaveDataLog["SaveState"] = 1;
        //                                row_SaveDataLog["Msg"] = "";
        //                            }
        //                            #endregion

        //                            dt_SaveDataLog.Rows.Add(row_SaveDataLog);

        //                            #region 人员执业资格

        //                            DataTable dt_ryzyzg = dataService.Get_uepp_Ryzyzg(personBasicInfo.IDCardNo);
        //                            rowIndex = -1;
        //                            for (int i = 0; i < dt_ryzyzg.Rows.Count; i++)
        //                            {
        //                                if (dt_ryzyzg.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxID)
        //                                {
        //                                    rowIndex = i;
        //                                    break;
        //                                }
        //                            }
        //                            if (rowIndex < 0)
        //                            {
        //                                row = dt_ryzyzg.NewRow();
        //                                dt_ryzyzg.Rows.Add(row);
        //                                row["ryID"] = personBasicInfo.IDCardNo;
        //                                row["ryzyzglxID"] = ryzyzglxID;
        //                                row["ryzyzglx"] = "注册建筑师";

        //                                row["balxID"] = 1;
        //                                row["balx"] = "长期备案";

        //                                row["DataState"] = 0;
        //                                row["tag"] = tag;
        //                                row["xgr"] = "定时服务";
        //                                row["xgrqsj"] = personBasicInfo.UpdateDate;
        //                                dataService.Submit_uepp_Ryzyzg(dt_ryzyzg);
        //                            }

        //                            #endregion

        //                            #region 人员专业明细

        //                            DataTable dt_ryzymx = dataService.Get_uepp_Ryzymx(personBasicInfo.IDCardNo);
        //                            rowIndex = -1;
        //                            for (int i = 0; i < dt_ryzymx.Rows.Count; i++)
        //                            {
        //                                if (dt_ryzymx.Rows[i]["ryzyzglxID"].ToString2() == ryzyzglxID)
        //                                {
        //                                    rowIndex = i;
        //                                    break;
        //                                }
        //                            }
        //                            if (rowIndex < 0)
        //                            {
        //                                row = dt_ryzymx.NewRow();
        //                                dt_ryzymx.Rows.Add(row);
        //                                row["ryID"] = personBasicInfo.IDCardNo;
        //                                row["ryzyzglxID"] = ryzyzglxID;
        //                                row["ryzyzglx"] = "注册建筑师";
        //                                row["zyzgdjID"] = 21;
        //                                row["zyzgdj"] = "壹级";
        //                                row["DataState"] = 0;
        //                                row["tag"] = tag;
        //                                row["xgr"] = "定时服务";
        //                                row["xgrqsj"] = personBasicInfo.UpdateDate;
        //                                dataService.Submit_uepp_Ryzymx(dt_ryzymx);
        //                            }
        //                            #endregion
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            //Public.WriteLog("出现异常，ryID：" + personBasicInfo.IDCardNo + ",Exception:" + ex.Message);

        //                            row_SaveDataLog["SaveState"] = 0;
        //                            row_SaveDataLog["Msg"] = ex.Message;
        //                            dt_SaveDataLog.Rows.Add(row_SaveDataLog);
        //                        }

        //                    }
        //                }
        //                #endregion

        //                #region 人员证书

        //                //if (personRegCertBody != null)
        //                //{
        //                //    foreach (PersonRegCert personRegCert in personRegCertBody.array)
        //                //    {
        //                //        try
        //                //        {
        //                //            if (string.IsNullOrEmpty(personRegCert.UpdateDate))
        //                //                personRegCert.UpdateDate = DateTime.Now.ToString("yyyy-MM-dd");

        //                //            #region 人员证书基本信息
        //                //            DataTable dt_ryzs = dataService.Get_uepp_Ryzs(personRegCert.IDCardNo);
        //                //            int rowIndex = -1;
        //                //            for (int i = 0; i < dt_ryzs.Rows.Count; i++)
        //                //            {
        //                //                if (dt_ryzs.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxID)
        //                //                {
        //                //                    rowIndex = i;
        //                //                    break;
        //                //                }
        //                //            }
        //                //            if (rowIndex < 0)
        //                //            {

        //                //                row = dt_ryzs.NewRow();
        //                //                dt_ryzs.Rows.Add(row);
        //                //                row["zsjlId"] = dataService.Get_uepp_RyzsNewID();
        //                //                row["ryID"] = personRegCert.IDCardNo;
        //                //                row["ryzyzglxID"] = ryzyzglxID;
        //                //                row["ryzyzglx"] = "注册建筑师";
        //                //                row["ryzslxID"] = 151;
        //                //                row["ryzslx"] = "注册建筑师资格证";

        //                //            }
        //                //            else
        //                //            {
        //                //                row = dt_ryzs.Rows[rowIndex];

        //                //            }
        //                //            row["sfzzz"] = 1;
        //                //            row["zsbh"] = personRegCert.QualCertNo;
        //                //            if (!string.IsNullOrEmpty(personRegCert.IssueDate))
        //                //            {
        //                //                row["fzrq"] = personRegCert.IssueDate;
        //                //                row["zsyxqrq"] = personRegCert.IssueDate;
        //                //            }
        //                //            if (!string.IsNullOrEmpty(personRegCert.ValidDate))
        //                //                row["zsyxzrq"] = personRegCert.ValidDate;

        //                //            row["fzdw"] = personRegCert.IssueOrgan;

        //                //            row["Status"] = personRegCert.Status;
        //                //            if (!string.IsNullOrEmpty(personRegCert.QualIssueDate))
        //                //                row["QualIssueDate"] = personRegCert.QualIssueDate;
        //                //            row["StampNo"] = personRegCert.StampNo;
        //                //            row["RegNo"] = personRegCert.RegNo;

        //                //            row["DataState"] = 0;
        //                //            row["tag"] = tag;
        //                //            row["xgr"] = "定时服务";
        //                //            row["xgrqsj"] = personRegCert.UpdateDate;
        //                //            dataService.Submit_uepp_Ryzs(dt_ryzs);

        //                //            #endregion

        //                //            #region 企业人员关系表

        //                //            DataTable dt_qyry = dataService.Get_uepp_Qyry(personRegCert.IDCardNo, personRegCert.CorpCode, ryzyzglxID);
        //                //            if (dt_qyry.Rows.Count == 0)
        //                //            {
        //                //                if (personRegCert.Status.ToString2() != "2")
        //                //                {
        //                //                    row = dt_qyry.NewRow();
        //                //                    dt_qyry.Rows.Add(row);
        //                //                    row["ryID"] = personRegCert.IDCardNo;
        //                //                    row["qyID"] = personRegCert.CorpCode;
        //                //                    row["ryzyzglxID"] = ryzyzglxID;
        //                //                    row["ryzyzglx"] = "注册建筑师";
        //                //                    row["DataState"] = 0;
        //                //                    row["tag"] = tag;
        //                //                    row["xgr"] = "定时服务";
        //                //                    row["xgrqsj"] = personRegCert.UpdateDate;
        //                //                    dataService.Submit_uepp_qyry(dt_qyry);
        //                //                }
        //                //            }
        //                //            else
        //                //            {
        //                //                if (personRegCert.Status.ToString2() == "2")
        //                //                {
        //                //                    foreach (DataRow item in dt_qyry.Rows)
        //                //                    {
        //                //                        item.Delete();
        //                //                    }

        //                //                    dataService.Submit_uepp_qyry(dt_qyry);
        //                //                }
        //                //            }
        //                //            #endregion
        //                //        }
        //                //        catch (Exception ex)
        //                //        {
        //                //            Public.WriteLog("更新人员证书出现异常，ryID：" + personRegCert.IDCardNo + ",Exception:" + ex.Message);
        //                //        }
        //                //    }
        //                //}

        //                #endregion

        //            }
        //        }
        #endregion


        /// <summary>
        /// 已测一次(没问题)
        /// </summary>
        /// <param name="DataJkLogID"></param>
        static void YourTask_PullDataFromSxxzx_Swqyxx(string DataJkLogID)
        {
            Public.WriteLog("执行YourTask_PullDataFromSxxzx_qyxx方法：");
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
            row_DataJkDataDetail_qyxx["MethodName"] = "getOutCorpInfo_Inc";
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

                index = result.IndexOf("<OutCorpCertQual>");
                if (index >= 0)
                {
                    string returnResult = result.Substring(index, result.LastIndexOf("</OutCorpCertQual>") - index + 18);
                    if (string.IsNullOrEmpty(returnResult))
                    {
                        return;
                    }
                    OutCorpCertQualBody outCorpCertQualBody = helper.DeserializeXML<OutCorpCertQualBody>("<OutCorpCertQualBody><OutCorpCertQualArray>" + returnResult + "</OutCorpCertQualArray></OutCorpCertQualBody>");

                    #region 更新企业资质(TCorpCertQual)
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
                                    tempRow_qycsyw["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd");

                                if (dt_qycsyw.Rows.Count > 0)
                                {
                                    dataService.Submit_uepp_qycsyw(dt_qycsyw);
                                }
                                #endregion

                                DataTable dt_jsdw_zzmx = dataService.Get_uepp_zzmxxx_qyxx(corpCertQual.CorpCode, csywlxID);

                                int rowIndex = -1;

                                for (int i = 0; i < dt_jsdw_zzmx.Rows.Count; i++)
                                {
                                    //if (dt_jsdw_zzmx.Rows[i].RowState == DataRowState.Deleted)
                                    //    continue;

                                    if (csywlxID == dt_jsdw_zzmx.Rows[i]["csywlxID"].ToString2()
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
                                    if (!string.IsNullOrEmpty(corpCertQual.UpdateDate) && !string.IsNullOrEmpty(row["xgrqsj"].ToString2()))
                                        if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(corpCertQual.UpdateDate).ToString("yyyy-MM-dd"))
                                        {
                                            continue;
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
                    #endregion
                }

                #region 更新企业资质证书信息(TCorpCertInfo)
                //if (corpCertInfoArr != null)
                //{
                //    foreach (CorpCertInfo corpCertInfo in corpCertInfoArr.array)
                //    {
                //        try
                //        {
                //            if (corpCertInfo.CorpCode.Length == 9)
                //            {
                //                corpCertInfo.CorpCode = corpCertInfo.CorpCode.Substring(0, 8) + '-' + corpCertInfo.CorpCode.Substring(8, 1);
                //            }
                //            //检查该企业是否存在社会信用代码，若存在，则转化为社会信用代码
                //            if (corpCertInfo.CorpCode.Length == 10)
                //            {
                //                string qyShxydm = dataService.Get_UEPP_Qyjbxx_Shxydm(corpCertInfo.CorpCode);
                //                if (!string.IsNullOrEmpty(qyShxydm))
                //                {
                //                    corpCertInfo.CorpCode = qyShxydm;
                //                }
                //            }

                //            int rowIndex = -1;
                //            string csywlxID = "", csywlx = "";
                //            switch (corpCertInfo.CertType)
                //            {
                //                //施工
                //                case "建筑业":
                //                    csywlxID = "1";
                //                    csywlx = "建筑施工";
                //                    break;
                //                case "城市园林绿化":
                //                    csywlxID = "3";
                //                    csywlx = "园林绿化";
                //                    break;
                //                case "设计与施工一体化":
                //                    csywlxID = "2";
                //                    csywlx = "设计施工一体化";
                //                    break;
                //                case "房屋拆迁":
                //                    csywlxID = "13";
                //                    csywlx = "房屋拆迁";
                //                    break;
                //                case "安全生产许可证":
                //                    csywlxID = "14";
                //                    csywlx = "安全生产许可证";
                //                    break;
                //                //勘察
                //                case "工程勘察":
                //                    csywlxID = "5";
                //                    csywlx = "工程勘察";
                //                    break;
                //                //设计
                //                case "工程设计":
                //                    csywlxID = "6";
                //                    csywlx = "工程设计";
                //                    break;
                //                case "城市规划":
                //                    csywlxID = "18";
                //                    csywlx = "城市规划";
                //                    break;
                //                case "外商城市规划":
                //                    csywlxID = "19";
                //                    csywlx = "外商城市规划";
                //                    break;

                //                //中介机构
                //                case "工程招标代理":
                //                    csywlxID = "7";
                //                    csywlx = "招标代理";
                //                    break;
                //                case "工程监理":
                //                    csywlxID = "4";
                //                    csywlx = "工程监理";
                //                    break;
                //                case "工程造价咨询":
                //                    csywlxID = "8";
                //                    csywlx = "造价咨询";
                //                    break;
                //                case "工程质量检测":
                //                    csywlxID = "9";
                //                    csywlx = "工程检测";
                //                    break;
                //                case "施工图审查":
                //                    csywlxID = "15";
                //                    csywlx = "施工图审查";
                //                    break;
                //                case "房地产估价":
                //                    csywlxID = "16";
                //                    csywlx = "房地产估价";
                //                    break;
                //                case "物业服务":
                //                    csywlxID = "17";
                //                    csywlx = "物业服务";
                //                    break;
                //                default:
                //                    break;
                //            }
                //            if (string.IsNullOrEmpty(csywlxID))
                //                continue;

                //            DataTable dt_qy_zzzs = dataService.Get_uepp_zzzsxx_qyxx(corpCertInfo.CorpCode);

                //            for (int i = 0; i < dt_qy_zzzs.Rows.Count; i++)
                //            {
                //                //if (dt_jsdw_zzzs.Rows[i].RowState == DataRowState.Deleted)
                //                //    continue;

                //                if (corpCertInfo.CertType == dt_qy_zzzs.Rows[i]["csywlx"].ToString2()
                //                    && dt_qy_zzzs.Rows[i]["zsbh"].ToString2() == corpCertInfo.CertCode
                //                    && dt_qy_zzzs.Rows[i]["csywlx"].ToString2() == corpCertInfo.CertType)
                //                {
                //                    rowIndex = i;
                //                    break;
                //                }
                //            }

                //            if (rowIndex < 0)
                //            {
                //                row = dt_qy_zzzs.NewRow();
                //                dt_qy_zzzs.Rows.Add(row);
                //                row["zsjlId"] = dataService.Get_uepp_qyQyzs_NewID();
                //            }
                //            else
                //            {
                //                row = dt_qy_zzzs.Rows[rowIndex];
                //                if (!string.IsNullOrEmpty(corpCertInfo.UpdateDate))
                //                    if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(corpCertInfo.UpdateDate).ToString("yyyy-MM-dd"))
                //                    {
                //                        continue;
                //                    }
                //            }
                //            row["qyID"] = corpCertInfo.CorpCode;
                //            row["csywlx"] = csywlx;
                //            row["csywlxID"] = csywlxID;

                //            //if (!string.IsNullOrEmpty(corpCertInfo.CertType))
                //            //{
                //            //    SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
                //            //    sp.Add("@parentCode", csywlxID);
                //            //    sp.Add("@CodeInfo", corpCertInfo.CertType);

                //            //    string sql = @"select * from UEPP_Code  where  CodeType ='企业证书类型' and ParentCodeType='企业从事业务类型' and ParentCode=@parentCode and CodeInfo=@CodeInfo ";
                //            //    string zslxID = dataService.ExecuteSql(sql, sp);
                //            //    if (!string.IsNullOrEmpty(zslxID))
                //            //    {
                //            //        row["zslxID"] = zslxID;
                //            //    }
                //            //}
                //            //row["zslx"] = "开发企业资质证书";

                //            row["sfzzz"] = "1";
                //            string zslx = "", zslxID = "";
                //            switch (corpCertInfo.CertType)
                //            {
                //                //施工
                //                case "建筑业":
                //                    zslxID = "10";
                //                    zslx = "建筑业资质证";
                //                    break;
                //                case "城市园林绿化":
                //                    zslxID = "30";
                //                    zslx = "园林绿化资质证";
                //                    break;
                //                case "设计与施工一体化":
                //                    zslxID = "20";
                //                    zslx = "设计施工一体化资质证";
                //                    break;
                //                case "房屋拆迁":
                //                    zslxID = "130";
                //                    zslx = "房屋拆迁资质证";
                //                    break;
                //                case "安全生产许可证":
                //                    zslxID = "140";
                //                    zslx = "安全生产许可证";
                //                    break;
                //                //勘察
                //                case "工程勘察":
                //                    zslxID = "51";
                //                    csywlx = "省工程勘察资质证";
                //                    break;
                //                //设计
                //                case "工程设计":
                //                    zslxID = "61";
                //                    zslx = "省工程设计资质证";
                //                    break;
                //                case "城市规划":
                //                    zslxID = "18";
                //                    zslx = "城市规划资质证";
                //                    break;
                //                case "外商城市规划":
                //                    zslxID = "19";
                //                    zslx = "外商城市规划资质证";
                //                    break;

                //                //中介机构
                //                case "工程招标代理":
                //                    zslxID = "70";
                //                    zslx = "招标代理资质证";
                //                    break;
                //                case "工程监理":
                //                    zslxID = "40";
                //                    zslx = "工程监理资质证";
                //                    break;
                //                case "工程造价咨询":
                //                    zslxID = "80";
                //                    csywlx = "造价咨询资质证";
                //                    break;
                //                case "工程质量检测":
                //                    zslxID = "90";
                //                    zslx = "工程检测资质证";
                //                    break;
                //                case "施工图审查":
                //                    zslxID = "150";
                //                    csywlx = "施工图审查资质证";
                //                    break;
                //                case "房地产估价":
                //                    zslxID = "160";
                //                    zslx = "房地产估价资质证";
                //                    break;
                //                case "物业服务":
                //                    zslxID = "170";
                //                    zslx = "物业服务资质证";
                //                    break;
                //                default:
                //                    break;
                //            }
                //            row["zslxID"] = zslxID;
                //            row["zslx"] = zslx;
                //            row["zsbh"] = corpCertInfo.CertCode;
                //            if (!string.IsNullOrEmpty(corpCertInfo.ValidDate.Trim()))
                //                row["zsyxzrq"] = corpCertInfo.ValidDate;
                //            if (!string.IsNullOrEmpty(corpCertInfo.IssueDate.Trim()))
                //            {
                //                row["fzrq"] = corpCertInfo.IssueDate;
                //                row["zsyxqrq"] = corpCertInfo.IssueDate;
                //            }

                //            row["fzdw"] = corpCertInfo.IssueOrgan;
                //            row["xgrqsj"] = corpCertInfo.UpdateDate;
                //            row["xgr"] = "定时服务";
                //            row["tag"] = tag;
                //            row["DataState"] = 0;

                //            if (!dataService.Submit_uepp_qyzzzs(dt_qy_zzzs))
                //            {
                //                Public.WriteLog("单位ID：" + corpCertInfo.CorpCode + "，企业资质证书信息保存失败！");
                //            }

                //        }
                //        catch (Exception ex)
                //        {
                //            Public.WriteLog("保存企业资质证书信息时出现异常：" + ex.Message);
                //        }
                //    }
                //}
                #endregion
                #endregion

                if (dt_SaveDataLog.Rows.Count > 0)
                    dataService.Submit_SaveDataLog(dt_SaveDataLog);

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
                row_DataJkDataDetail_qyxx["ErrorMsg"] = "从江苏建设公共基础数据平台获取省外企业信息时出现异常:" + ex.Message;

                if (dt_DataJkDataDetail_qyxx.Rows.Count > 0)
                    dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_qyxx);
            }

        }

        /// <summary>
        /// 已测一次（没问题）
        /// </summary>
        /// <param name="DataJkLogID"></param>
        static void YourTask_PullDataFromSxxzx_Swryxx(string DataJkLogID)
        {
            int allCount_ryxx = 0, success_count_ryxx = 0;
            DataRow row;
            Public.WriteLog("执行YourTask_PullDataFromSxxzx_Swryxx方法：");
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
            row_DataJkDataDetail_ryxx["tableName"] = "UEPP_Qyjbxx";
            row_DataJkDataDetail_ryxx["MethodName"] = "getOutCorpInfo_Inc";
            row_DataJkDataDetail_ryxx["bz"] = "从江苏建设公共基础数据平台拉取省外企业信息";

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

                                #region 人员执业资格

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






        #region 从江苏建设公共基础数据平台拉取企业（建设单位除外）信息-旧版
        //        void YourTask_PullDataFromSxxzx_qyxx(string DataJkLogID)
        //        {
        //            Public.WriteLog("执行YourTask_PullDataFromSxxzx_qyxx方法：");
        //            string tag = Tag.江苏建设公共基础数据平台.ToString();
        //            string userID = "wxszjj01";
        //            DataService dataService = new DataService();
        //            DataTable dt_xzqhdm = dataService.Get_tbXzqdmDic();
        //            XmlHelper helper = new XmlHelper();

        //            DataDownService.dataDownService dataDownService = new DataDownService.dataDownService();
        //            DataRow row;

        //            //往数据监控日志表项添加一条记录
        //            DataTable dt_DataJkDataDetail_qyxx = dataService.GetSchema_DataJkDataDetail();
        //            long Id_DataJkDataDetail_qyxx = dataService.Get_DataJkDataDetailNewID().ToInt64();

        //            int allCount_qyxx = 0, success_count_qyxx = 0;
        //            DataRow row_DataJkDataDetail_qyxx = dt_DataJkDataDetail_qyxx.NewRow();
        //            dt_DataJkDataDetail_qyxx.Rows.Add(row_DataJkDataDetail_qyxx);

        //            row_DataJkDataDetail_qyxx["ID"] = Id_DataJkDataDetail_qyxx++;
        //            row_DataJkDataDetail_qyxx["DataJkLogID"] = DataJkLogID;
        //            row_DataJkDataDetail_qyxx["tableName"] = "UEPP_Ryjbxx";
        //            row_DataJkDataDetail_qyxx["MethodName"] = "getCorpInfoStream_Inc";
        //            row_DataJkDataDetail_qyxx["bz"] = "从江苏建设公共基础数据平台拉取企业（建设单位除外）信息";
        //            try
        //            {
        //                DataTable dt_SaveDataLog = dataService.GetSchema_SaveDataLog();

        //                foreach (DataRow row_xzqdm in dt_xzqhdm.Rows)
        //                {
        //                    string xzqdm = row_xzqdm["Code"].ToString2();
        //                    //byte[] bytes=  dataDownService.getCorpInfoStream(userID, "320200", xzqdm, String.Empty, "0");

        //                    byte[] bytes = dataDownService.getCorpInfoStream_Inc(userID, "320200", xzqdm,
        //                        "", "1991-01-01", DateTime.Now.ToString("yyyy-MM-dd"), "0");

        //                    string result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);

        //                    var index = result.IndexOf("<ReturnInfo>");

        //                    if (index >= 0)
        //                    {
        //                        string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
        //                        if (string.IsNullOrEmpty(returnResult))
        //                        {
        //                            continue;
        //                        }
        //                        ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
        //                        if (!returnInfo.Status)
        //                        {
        //                            continue;
        //                        }
        //                    }

        //                    index = result.IndexOf("<CorpBasicInfo>");
        //                    if (index >= 0)
        //                    {
        //                        string corpBasicInfoString = result.Substring(index, result.LastIndexOf("</CorpBasicInfo>") - index + 16);
        //                        CorpBasicInfoBody corpBasicInfoBody = helper.DeserializeXML<CorpBasicInfoBody>("<CorpBasicInfoBody><CorpBasicInfoArray>" + corpBasicInfoString + "</CorpBasicInfoArray></CorpBasicInfoBody>");
        //                        if (corpBasicInfoBody != null)
        //                        {
        //                            CorpCertInfoBody corpCertInfoArr = new CorpCertInfoBody();
        //                            int CorpCertInfoIndex = result.IndexOf("<CorpCertInfo>");
        //                            if (CorpCertInfoIndex >= 0)
        //                            {
        //                                string corpCertInfoString = result.Substring(CorpCertInfoIndex, result.LastIndexOf("</CorpCertInfo>") - CorpCertInfoIndex + 15);
        //                                corpCertInfoArr = helper.DeserializeXML<CorpCertInfoBody>(
        //                                    "<CorpCertInfoBody><CorpCertInfoArray>" + corpCertInfoString + "</CorpCertInfoArray></CorpCertInfoBody>");
        //                            }

        //                            CorpCertQualBody corpCertQualArr = new CorpCertQualBody();
        //                            int CorpCertQualIndex = result.IndexOf("<CorpCertQual>");

        //                            if (CorpCertQualIndex >= 0)
        //                            {
        //                                string corpCertQualString = result.Substring(CorpCertQualIndex, result.LastIndexOf("</CorpCertQual>") - CorpCertQualIndex + 15);
        //                                corpCertQualArr = helper.DeserializeXML<CorpCertQualBody>(
        //                                    "<CorpCertQualBody><CorpCertQualArray>" + corpCertQualString + "</CorpCertQualArray></CorpCertQualBody>");

        //                            }

        //                            if (corpCertQualArr == null)
        //                            {
        //                                continue;
        //                            }
        //                            foreach (CorpBasicInfo corpBasicInfo in corpBasicInfoBody.array)
        //                            {
        //                                if (corpCertQualArr.array.Exists(p => p.CorpCode == corpBasicInfo.CorpCode && p.CertType == "房地产开发"))
        //                                {
        //                                    continue;
        //                                }

        //                                if (corpBasicInfo.CorpCode.Length == 9)
        //                                {
        //                                    corpBasicInfo.CorpCode = corpBasicInfo.CorpCode.Substring(0, 8) + '-' + corpBasicInfo.CorpCode.Substring(8, 1);
        //                                }
        //                                //检查该企业是否存在社会信用代码，若存在，则转化为社会信用代码
        //                                if (corpBasicInfo.CorpCode.Length == 10)
        //                                {
        //                                    string qyShxydm = dataService.Get_UEPP_Qyjbxx_Shxydm(corpBasicInfo.CorpCode);
        //                                    if (!string.IsNullOrEmpty(qyShxydm))
        //                                    {
        //                                        corpBasicInfo.CorpCode = qyShxydm;
        //                                    }
        //                                }

        //                                DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
        //                                row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_qyxx["ID"];
        //                                row_SaveDataLog["DataXml"] = "";
        //                                row_SaveDataLog["PKID"] = corpBasicInfo.CorpCode;
        //                                row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");
        //                                dt_SaveDataLog.Rows.Add(row_SaveDataLog);

        //                                try
        //                                {
        //                                    #region  更新企业基本信息

        //                                    DataTable dt = dataService.Get_uepp_Qyjbxx(corpBasicInfo.CorpCode);

        //                                    if (dt.Rows.Count == 0)
        //                                    {
        //                                        row = dt.NewRow();
        //                                        dt.Rows.Add(row);
        //                                    }
        //                                    else
        //                                    {
        //                                        row = dt.Rows[0];
        //                                        //if (!string.IsNullOrEmpty(corpBasicInfo.UpdateDate) && !string.IsNullOrEmpty(row["xgrqsj"].ToString2()))
        //                                        //    if (DateTime.Parse(row["xgrqsj"].ToString2()).ToString("yyyy-MM-dd") == DateTime.Parse(corpBasicInfo.UpdateDate).ToString("yyyy-MM-dd"))
        //                                        //    {
        //                                        //        continue;
        //                                        //    }
        //                                        //if (row["tag"].ToString2().IndexOf(tag) < 0)
        //                                        //{
        //                                        //    row["tag"] = row["tag"].ToString2().TrimEnd(',') + "," + tag;
        //                                        //}
        //                                    }
        //                                    row["tag"] = tag;
        //                                    row["qymc"] = corpBasicInfo.CorpName;
        //                                    row["zzjgdm"] = corpBasicInfo.CorpCode;
        //                                    row["yyzzzch"] = corpBasicInfo.LicenseNo;

        //                                    if (!string.IsNullOrEmpty(corpBasicInfo.ProvinceCode.ToString2()))
        //                                    {
        //                                        SqlParameterCollection sp = dataService.CreateSqlParameterCollection();

        //                                        row["Province"] = corpBasicInfo.ProvinceCode;
        //                                        sp.Add("@CodeInfo", corpBasicInfo.ProvinceCode.ToString2());
        //                                        string provinceCode = dataService.ExecuteSql("select top 1 Code from  UEPP_Code where CodeType='城市地区' and  CodeInfo=@CodeInfo", sp);
        //                                        if (!string.IsNullOrEmpty(provinceCode))
        //                                        {
        //                                            row["ProvinceID"] = provinceCode;
        //                                            sp.Clear();
        //                                            if (!string.IsNullOrEmpty(corpBasicInfo.CityCode.ToString2()))
        //                                            {
        //                                                row["City"] = corpBasicInfo.CityCode;

        //                                                sp.Add("@CodeInfo", corpBasicInfo.CityCode.ToString2());
        //                                                sp.Add("@parentCode", provinceCode);
        //                                                string cityCode = dataService.ExecuteSql("select top 1 Code from  UEPP_Code where CodeType='城市地区' and ParentCode=@parentCode and  CodeInfo=@CodeInfo", sp);
        //                                                if (!string.IsNullOrEmpty(cityCode))
        //                                                {
        //                                                    row["CityID"] = cityCode;
        //                                                }

        //                                                sp.Clear();
        //                                                if (!string.IsNullOrEmpty(corpBasicInfo.CountyCode.ToString2()))
        //                                                {
        //                                                    row["County"] = corpBasicInfo.CountyCode;

        //                                                    sp.Add("@CodeInfo", corpBasicInfo.CountyCode.ToString2());
        //                                                    sp.Add("@parentCode", cityCode);
        //                                                    string countyCode = dataService.ExecuteSql("select top 1 Code from  UEPP_Code where CodeType='城市地区' and ParentCode=@parentCode and  CodeInfo=@CodeInfo", sp);
        //                                                    if (!string.IsNullOrEmpty(countyCode))
        //                                                    {
        //                                                        row["CountyID"] = countyCode;
        //                                                    }

        //                                                }
        //                                            }
        //                                        }
        //                                    }

        //                                    row["zcdd"] = corpBasicInfo.RegAddress;
        //                                    row["xxdd"] = corpBasicInfo.CorpAddress;
        //                                    if (!string.IsNullOrEmpty(corpBasicInfo.FoundDate))
        //                                        row["clrq"] = corpBasicInfo.FoundDate;
        //                                    row["jjxz"] = corpBasicInfo.CorpTypeDesc.ToString2().Trim();
        //                                    if (!string.IsNullOrEmpty(corpBasicInfo.CorpTypeDesc))
        //                                    {
        //                                        SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
        //                                        sp.Add("@CodeInfo", corpBasicInfo.CorpTypeDesc.ToString2().Trim());
        //                                        string jjxzID = dataService.ExecuteSql("select * from  UEPP_Code where CodeType='企业经济性质' and  CodeInfo=@CodeInfo", sp);
        //                                        if (!string.IsNullOrEmpty(jjxzID))
        //                                        {
        //                                            row["jjxzID"] = jjxzID;
        //                                        }
        //                                    }

        //                                    row["zczb"] = corpBasicInfo.RegCapital;
        //                                    row["cz"] = corpBasicInfo.Fax;
        //                                    row["lxdh"] = corpBasicInfo.LinkPhone;
        //                                    row["fddbr"] = corpBasicInfo.LegalMan;

        //                                    if (!string.IsNullOrEmpty(corpBasicInfo.UpdateDate))
        //                                        row["xgrqsj"] = corpBasicInfo.UpdateDate;
        //                                    else
        //                                        row["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd");

        //                                    row["DataState"] = 0;
        //                                    allCount_qyxx++;
        //                                    if (!dataService.Submit_uepp_qyjbxx(dt))
        //                                    {
        //                                        row_SaveDataLog["SaveState"] = 0;
        //                                        row_SaveDataLog["Msg"] = "从江苏建设公共基础数据平台获取的单位：" + corpBasicInfo.CorpName + "," + corpBasicInfo.CorpCode + "，基本信息保存失败！";
        //                                        continue;
        //                                    }
        //                                    else
        //                                    {
        //                                        success_count_qyxx++;
        //                                        row_SaveDataLog["SaveState"] = 1;
        //                                        row_SaveDataLog["Msg"] = "";
        //                                    }
        //                                    #endregion
        //                                }
        //                                catch (Exception ex)
        //                                {
        //                                    row_SaveDataLog["SaveState"] = 0;
        //                                    row_SaveDataLog["Msg"] = ex.Message;
        //                                }
        //                            }

        //                            #region 更新企业资质(TCorpCertQual)
        //                            if (corpCertQualArr != null)
        //                            {
        //                                foreach (CorpCertQual corpCertQual in corpCertQualArr.array)
        //                                {
        //                                    //if (corpCertQual.CertType == "房地产开发")
        //                                    //    break;
        //                                    //for (int i = 0; i < dt_jsdw_zzmx.Rows.Count; i++)
        //                                    //{
        //                                    //    if (!list.Exists(p => p.CertType == "房地产开发" && dt_jsdw_zzmx.Rows[i]["csywlxID"].ToString2() == "11"
        //                                    //        && (p.TradeType == "不分行业" || p.TradeType == dt_jsdw_zzmx.Rows[i]["zzxl"].ToString2())
        //                                    //        && p.MajorType == dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2()))
        //                                    //    {
        //                                    //        dt_jsdw_zzmx.Rows[i].Delete();
        //                                    //    }
        //                                    //}

        //                                    try
        //                                    {
        //                                        if (corpCertQual.CorpCode.Length == 9)
        //                                        {
        //                                            corpCertQual.CorpCode = corpCertQual.CorpCode.Substring(0, 8) + '-' + corpCertQual.CorpCode.Substring(8, 1);
        //                                        }
        //                                        //检查该企业是否存在社会信用代码，若存在，则转化为社会信用代码
        //                                        if (corpCertQual.CorpCode.Length == 10)
        //                                        {
        //                                            string qyShxydm = dataService.Get_UEPP_Qyjbxx_Shxydm(corpCertQual.CorpCode);
        //                                            if (!string.IsNullOrEmpty(qyShxydm))
        //                                            {
        //                                                corpCertQual.CorpCode = qyShxydm;
        //                                            }
        //                                        }

        //                                        string csywlxID = "", csywlx = "";
        //                                        switch (corpCertQual.CertType)
        //                                        {
        //                                            //施工
        //                                            case "建筑业":
        //                                                csywlxID = "1";
        //                                                csywlx = "建筑施工";
        //                                                break;
        //                                            case "城市园林绿化":
        //                                                csywlxID = "3";
        //                                                csywlx = "园林绿化";
        //                                                break;
        //                                            case "设计与施工一体化":
        //                                                csywlxID = "2";
        //                                                csywlx = "设计施工一体化";
        //                                                break;
        //                                            case "房屋拆迁":
        //                                                csywlxID = "13";
        //                                                csywlx = "房屋拆迁";
        //                                                break;
        //                                            case "安全生产许可证":
        //                                                csywlxID = "14";
        //                                                csywlx = "安全生产许可证";
        //                                                break;
        //                                            //勘察
        //                                            case "工程勘察":
        //                                                csywlxID = "5";
        //                                                csywlx = "工程勘察";
        //                                                break;
        //                                            //设计
        //                                            case "工程设计":
        //                                                csywlxID = "6";
        //                                                csywlx = "工程设计";
        //                                                break;
        //                                            case "城市规划":
        //                                                csywlxID = "18";
        //                                                csywlx = "城市规划";
        //                                                break;
        //                                            case "外商城市规划":
        //                                                csywlxID = "19";
        //                                                csywlx = "外商城市规划";
        //                                                break;

        //                                            //中介机构
        //                                            case "工程招标代理":
        //                                                csywlxID = "7";
        //                                                csywlx = "招标代理";
        //                                                break;
        //                                            case "工程监理":
        //                                                csywlxID = "4";
        //                                                csywlx = "工程监理";
        //                                                break;
        //                                            case "工程造价咨询":
        //                                                csywlxID = "8";
        //                                                csywlx = "造价咨询";
        //                                                break;
        //                                            case "工程质量检测":
        //                                                csywlxID = "9";
        //                                                csywlx = "工程检测";
        //                                                break;
        //                                            case "施工图审查":
        //                                                csywlxID = "15";
        //                                                csywlx = "施工图审查";
        //                                                break;
        //                                            case "房地产估价":
        //                                                csywlxID = "16";
        //                                                csywlx = "房地产估价";
        //                                                break;
        //                                            case "物业服务":
        //                                                csywlxID = "17";
        //                                                csywlx = "物业服务";
        //                                                break;
        //                                            default:
        //                                                break;
        //                                        }
        //                                        if (string.IsNullOrEmpty(csywlxID))
        //                                            continue;

        //                                        #region 企业从事业务类型

        //                                        DataTable dt_qycsyw = dataService.Get_uepp_Qycsyw_sjsgyth(corpCertQual.CorpCode, csywlxID);

        //                                        DataRow tempRow_qycsyw;

        //                                        if (dt_qycsyw.Rows.Count == 0)
        //                                        {
        //                                            tempRow_qycsyw = dt_qycsyw.NewRow();
        //                                            dt_qycsyw.Rows.Add(tempRow_qycsyw);
        //                                            tempRow_qycsyw["qyID"] = corpCertQual.CorpCode;
        //                                        }
        //                                        else
        //                                        {
        //                                            tempRow_qycsyw = dt_qycsyw.Rows[0];
        //                                        }

        //                                        tempRow_qycsyw["csywlxID"] = csywlxID;
        //                                        tempRow_qycsyw["csywlx"] = csywlx;

        //                                        tempRow_qycsyw["balxID"] = "1";
        //                                        tempRow_qycsyw["balx"] = "长期备案";
        //                                        tempRow_qycsyw["DataState"] = "0";
        //                                        tempRow_qycsyw["tag"] = tag;

        //                                        if (!string.IsNullOrEmpty(corpCertQual.UpdateDate))
        //                                            tempRow_qycsyw["xgrqsj"] = corpCertQual.UpdateDate;
        //                                        else
        //                                            tempRow_qycsyw["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd");



        //                                        if (dt_qycsyw.Rows.Count > 0)
        //                                        {
        //                                            dataService.Submit_uepp_qycsyw(dt_qycsyw);
        //                                        }
        //                                        #endregion

        //                                        DataTable dt_jsdw_zzmx = dataService.Get_uepp_zzmxxx_qyxx(corpCertQual.CorpCode, csywlxID);

        //                                        int rowIndex = -1;

        //                                        for (int i = 0; i < dt_jsdw_zzmx.Rows.Count; i++)
        //                                        {
        //                                            //if (dt_jsdw_zzmx.Rows[i].RowState == DataRowState.Deleted)
        //                                            //    continue;

        //                                            if (csywlxID == dt_jsdw_zzmx.Rows[i]["csywlxID"].ToString2()
        //                                                && (
        //                                                 corpCertQual.TradeType == "工程勘察综合类" && dt_jsdw_zzmx.Rows[i]["zzxl"].ToString2() == "综合类"
        //                                                 || corpCertQual.TradeType == "工程勘察专业类" && dt_jsdw_zzmx.Rows[i]["zzxl"].ToString2() == "专业类"
        //                                                 || corpCertQual.TradeType == "工程勘察劳务类" && dt_jsdw_zzmx.Rows[i]["zzxl"].ToString2() == "劳务类"
        //                                                 || corpCertQual.TradeType == dt_jsdw_zzmx.Rows[i]["zzxl"].ToString2()
        //                                                )
        //                                                && (dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "岩土工程勘察" && corpCertQual.MajorType == "岩土工程（勘察）"
        //                                                || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "岩土工程设计" && corpCertQual.MajorType == "岩土工程（设计）"
        //                                                  || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "岩土工程测试、监测、检测" && corpCertQual.MajorType == "岩土工程（物探测试检测监测）"
        //                                                  || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "岩土工程咨询、监理" && corpCertQual.MajorType == "岩土工程（咨询监理）"

        //                                                  || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "建筑装修装饰" && corpCertQual.MajorType == "建筑装饰装修工程"
        //                                                  || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "模板作业" && corpCertQual.MajorType == "模板作业分包"
        //                                                  || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == "脚手架搭设作业" && corpCertQual.MajorType == "脚手架作业分包"
        //                                                  || dt_jsdw_zzmx.Rows[i]["zzlb"].ToString2() == corpCertQual.MajorType
        //                                                )
        //                                                )
        //                                            {
        //                                                rowIndex = i;
        //                                                break;
        //                                            }
        //                                        }

        //                                        if (rowIndex < 0)
        //                                        {
        //                                            row = dt_jsdw_zzmx.NewRow();
        //                                            dt_jsdw_zzmx.Rows.Add(row);
        //                                            row["ID"] = dataService.Get_uepp_qyxxmx_NewID();
        //                                            row["qyID"] = corpCertQual.CorpCode;
        //                                            row["csywlx"] = csywlx;
        //                                            row["csywlxID"] = csywlxID;
        //                                        }
        //                                        else
        //                                        {
        //                                            row = dt_jsdw_zzmx.Rows[rowIndex];
        //                                            if (!string.IsNullOrEmpty(corpCertQual.UpdateDate) && !string.IsNullOrEmpty(row["xgrqsj"].ToString2()))
        //                                                if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(corpCertQual.UpdateDate).ToString("yyyy-MM-dd"))
        //                                                {
        //                                                    continue;
        //                                                }
        //                                        }
        //                                        if (corpCertQual.IsMaster == "主项")
        //                                            row["zzbz"] = "主项";
        //                                        else
        //                                            row["zzbz"] = "增项";

        //                                        if (corpCertQual.TradeType == "工程勘察综合类")
        //                                        {
        //                                            row["zzxl"] = "综合类";
        //                                            row["zzxlID"] = "9";
        //                                        }
        //                                        else
        //                                            if (corpCertQual.TradeType == "工程勘察专业类")
        //                                            {
        //                                                row["zzxl"] = "专业类";
        //                                                row["zzxlID"] = "10";
        //                                            }
        //                                            else
        //                                                if (corpCertQual.TradeType == "工程勘察劳务类")
        //                                                {
        //                                                    row["zzxl"] = "劳务类";
        //                                                    row["zzxlID"] = "11";
        //                                                }
        //                                                else
        //                                                    if (corpCertQual.TradeType == "工程设计综合")
        //                                                    {
        //                                                        row["zzxl"] = "综合资质";
        //                                                        row["zzxlID"] = "12";
        //                                                    }
        //                                                    else
        //                                                    {
        //                                                        row["zzxl"] = corpCertQual.TradeType;
        //                                                        if (!string.IsNullOrEmpty(csywlxID))
        //                                                        {
        //                                                            string sql = @"select Code from UEPP_Code where  CodeType='企业资质序列' and ParentCodeType='企业从事业务类型'
        // and ParentCode=@parentCode and CodeInfo=@CodeInfo ";
        //                                                            SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
        //                                                            sp.Add("@CodeInfo", corpCertQual.TradeType);
        //                                                            sp.Add("@parentCode", csywlxID);
        //                                                            string zzxlID = dataService.ExecuteSql(sql, sp);
        //                                                            if (!string.IsNullOrEmpty(zzxlID))
        //                                                                row["zzxlID"] = zzxlID;
        //                                                        }
        //                                                    }

        //                                        if (corpCertQual.MajorType == "岩土工程（勘察）")
        //                                        {
        //                                            row["zzlb"] = "岩土工程勘察";
        //                                            row["zzlbID"] = "300";
        //                                        }
        //                                        else
        //                                            if (corpCertQual.MajorType == "岩土工程（设计）")
        //                                            {
        //                                                row["zzlb"] = "岩土工程设计";
        //                                                row["zzlbID"] = "301";
        //                                            }
        //                                            else
        //                                                if (corpCertQual.MajorType == "岩土工程（物探测试检测监测）")
        //                                                {
        //                                                    row["zzlb"] = "岩土工程测试、监测、检测";
        //                                                    row["zzlbID"] = "302";
        //                                                }
        //                                                else
        //                                                    if (corpCertQual.MajorType == "岩土工程（咨询监理））")
        //                                                    {
        //                                                        row["zzlb"] = "岩土工程咨询、监理";
        //                                                        row["zzlbID"] = "303";
        //                                                    }
        //                                                    else
        //                                                        if (corpCertQual.TradeType == "建筑装饰装修工程")
        //                                                        {
        //                                                            row["zzlb"] = "建筑装修装饰";
        //                                                            row["zzlbID"] = "33";
        //                                                        }
        //                                                        else if (corpCertQual.TradeType == "模板作业分包")
        //                                                        {
        //                                                            row["zzlb"] = "模板作业";
        //                                                            row["zzlbID"] = "128";
        //                                                        }
        //                                                        else
        //                                                            if (corpCertQual.TradeType == "脚手架作业分包")
        //                                                            {
        //                                                                row["zzlb"] = "脚手架搭设作业";
        //                                                                row["zzlbID"] = "127";
        //                                                            }
        //                                                            else
        //                                                            {
        //                                                                row["zzlb"] = corpCertQual.MajorType;
        //                                                                if (!string.IsNullOrEmpty(row["zzxlID"].ToString2().Trim()))
        //                                                                {
        //                                                                    string sql = @"select Code from UEPP_Code where  CodeType='企业资质类别' and  ParentCodeType='企业资质序列'
        // and ParentCode=@parentCode and (CodeInfo=@CodeInfo or CodeInfo=@CodeInfo1) ";
        //                                                                    SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
        //                                                                    sp.Add("@CodeInfo", corpCertQual.MajorType);
        //                                                                    sp.Add("@parentCode", row["zzxlID"]);
        //                                                                    sp.Add("@CodeInfo1", corpCertQual.MajorType.ToString2().Replace("分包", ""));
        //                                                                    string zzlbID = dataService.ExecuteSql(sql, sp);
        //                                                                    if (!string.IsNullOrEmpty(zzlbID))
        //                                                                        row["zzlbID"] = zzlbID;
        //                                                                }
        //                                                            }




        //                                        row["zzdj"] = corpCertQual.TitleLevel;

        //                                        if (!string.IsNullOrEmpty(corpCertQual.TitleLevel))
        //                                        {
        //                                            SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
        //                                            sp.Add("@CodeInfo", corpCertQual.TitleLevel);

        //                                            string sql = "select Code from UEPP_Code  where  CodeType='企业资质等级' and ParentCodeType='企业资质序列' and CodeInfo=@CodeInfo ";
        //                                            string zzdjID = dataService.ExecuteSql(sql, sp);
        //                                            if (!string.IsNullOrEmpty(zzdjID))
        //                                                row["zzdjID"] = zzdjID;
        //                                            else
        //                                            {
        //                                                // 新增代码表
        //                                            }
        //                                        }

        //                                        if (corpCertQual.Status == "1" || corpCertQual.Status == "有效")
        //                                        {
        //                                            row["DataState"] = 0;
        //                                        }
        //                                        else
        //                                        {
        //                                            row["DataState"] = -1;
        //                                        }

        //                                        row["tag"] = tag;
        //                                        row["xgrqsj"] = corpCertQual.UpdateDate;

        //                                        if (!dataService.Submit_uepp_qyzzmx(dt_jsdw_zzmx))
        //                                        {
        //                                            Public.WriteLog("单位ID：" + corpCertQual.CorpCode + "，企业资质保存失败！");
        //                                        }
        //                                    }
        //                                    catch (Exception ex)
        //                                    {
        //                                        Public.WriteLog("保存企业资质时出现异常：" + ex.Message);
        //                                    }
        //                                }
        //                            }
        //                            #endregion

        //                            #region 更新企业资质证书信息(TCorpCertInfo)
        //                            if (corpCertInfoArr != null)
        //                            {
        //                                //List<CorpCertInfo> list = corpCertInfoArr.array.FindAll(p => p.CorpCode == corpBasicInfo.CorpCode);

        //                                //DataTable dt_jsdw_zzzs = dataService.Get_uepp_jsdw_zzzsxx(corpBasicInfo.CorpCode);

        //                                //for (int i = 0; i < dt_jsdw_zzzs.Rows.Count; i++)
        //                                //{
        //                                //    if (!list.Exists(p => p.CertType == "房地产开发" && p.CertCode == dt_jsdw_zzzs.Rows[i]["zsbh"].ToString2()
        //                                //        && p.CertType == "开发企业资质证书"))
        //                                //    {
        //                                //        dt_jsdw_zzzs.Rows[i].Delete();
        //                                //    }
        //                                //}

        //                                foreach (CorpCertInfo corpCertInfo in corpCertInfoArr.array)
        //                                {
        //                                    try
        //                                    {
        //                                        if (corpCertInfo.CorpCode.Length == 9)
        //                                        {
        //                                            corpCertInfo.CorpCode = corpCertInfo.CorpCode.Substring(0, 8) + '-' + corpCertInfo.CorpCode.Substring(8, 1);
        //                                        }
        //                                        //检查该企业是否存在社会信用代码，若存在，则转化为社会信用代码
        //                                        if (corpCertInfo.CorpCode.Length == 10)
        //                                        {
        //                                            string qyShxydm = dataService.Get_UEPP_Qyjbxx_Shxydm(corpCertInfo.CorpCode);
        //                                            if (!string.IsNullOrEmpty(qyShxydm))
        //                                            {
        //                                                corpCertInfo.CorpCode = qyShxydm;
        //                                            }
        //                                        }

        //                                        int rowIndex = -1;
        //                                        string csywlxID = "", csywlx = "";
        //                                        switch (corpCertInfo.CertType)
        //                                        {
        //                                            //施工
        //                                            case "建筑业":
        //                                                csywlxID = "1";
        //                                                csywlx = "建筑施工";
        //                                                break;
        //                                            case "城市园林绿化":
        //                                                csywlxID = "3";
        //                                                csywlx = "园林绿化";
        //                                                break;
        //                                            case "设计与施工一体化":
        //                                                csywlxID = "2";
        //                                                csywlx = "设计施工一体化";
        //                                                break;
        //                                            case "房屋拆迁":
        //                                                csywlxID = "13";
        //                                                csywlx = "房屋拆迁";
        //                                                break;
        //                                            case "安全生产许可证":
        //                                                csywlxID = "14";
        //                                                csywlx = "安全生产许可证";
        //                                                break;
        //                                            //勘察
        //                                            case "工程勘察":
        //                                                csywlxID = "5";
        //                                                csywlx = "工程勘察";
        //                                                break;
        //                                            //设计
        //                                            case "工程设计":
        //                                                csywlxID = "6";
        //                                                csywlx = "工程设计";
        //                                                break;
        //                                            case "城市规划":
        //                                                csywlxID = "18";
        //                                                csywlx = "城市规划";
        //                                                break;
        //                                            case "外商城市规划":
        //                                                csywlxID = "19";
        //                                                csywlx = "外商城市规划";
        //                                                break;

        //                                            //中介机构
        //                                            case "工程招标代理":
        //                                                csywlxID = "7";
        //                                                csywlx = "招标代理";
        //                                                break;
        //                                            case "工程监理":
        //                                                csywlxID = "4";
        //                                                csywlx = "工程监理";
        //                                                break;
        //                                            case "工程造价咨询":
        //                                                csywlxID = "8";
        //                                                csywlx = "造价咨询";
        //                                                break;
        //                                            case "工程质量检测":
        //                                                csywlxID = "9";
        //                                                csywlx = "工程检测";
        //                                                break;
        //                                            case "施工图审查":
        //                                                csywlxID = "15";
        //                                                csywlx = "施工图审查";
        //                                                break;
        //                                            case "房地产估价":
        //                                                csywlxID = "16";
        //                                                csywlx = "房地产估价";
        //                                                break;
        //                                            case "物业服务":
        //                                                csywlxID = "17";
        //                                                csywlx = "物业服务";
        //                                                break;
        //                                            default:
        //                                                break;
        //                                        }
        //                                        if (string.IsNullOrEmpty(csywlxID))
        //                                            continue;

        //                                        DataTable dt_qy_zzzs = dataService.Get_uepp_zzzsxx_qyxx(corpCertInfo.CorpCode);

        //                                        for (int i = 0; i < dt_qy_zzzs.Rows.Count; i++)
        //                                        {
        //                                            //if (dt_jsdw_zzzs.Rows[i].RowState == DataRowState.Deleted)
        //                                            //    continue;

        //                                            if (corpCertInfo.CertType == dt_qy_zzzs.Rows[i]["csywlx"].ToString2()
        //                                                && dt_qy_zzzs.Rows[i]["zsbh"].ToString2() == corpCertInfo.CertCode
        //                                                && dt_qy_zzzs.Rows[i]["csywlx"].ToString2() == corpCertInfo.CertType)
        //                                            {
        //                                                rowIndex = i;
        //                                                break;
        //                                            }
        //                                        }

        //                                        if (rowIndex < 0)
        //                                        {
        //                                            row = dt_qy_zzzs.NewRow();
        //                                            dt_qy_zzzs.Rows.Add(row);
        //                                            row["zsjlId"] = dataService.Get_uepp_qyQyzs_NewID();
        //                                            row["qyID"] = corpCertInfo.CorpCode;
        //                                        }
        //                                        else
        //                                        {
        //                                            row = dt_qy_zzzs.Rows[rowIndex];
        //                                            if (!string.IsNullOrEmpty(corpCertInfo.UpdateDate))
        //                                                if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(corpCertInfo.UpdateDate).ToString("yyyy-MM-dd"))
        //                                                {
        //                                                    continue;
        //                                                }
        //                                        }

        //                                        row["csywlx"] = csywlx;
        //                                        row["csywlxID"] = csywlxID;

        //                                        //if (!string.IsNullOrEmpty(corpCertInfo.CertType))
        //                                        //{
        //                                        //    SqlParameterCollection sp = dataService.CreateSqlParameterCollection();
        //                                        //    sp.Add("@parentCode", csywlxID);
        //                                        //    sp.Add("@CodeInfo", corpCertInfo.CertType);

        //                                        //    string sql = @"select * from UEPP_Code  where  CodeType ='企业证书类型' and ParentCodeType='企业从事业务类型' and ParentCode=@parentCode and CodeInfo=@CodeInfo ";
        //                                        //    string zslxID = dataService.ExecuteSql(sql, sp);
        //                                        //    if (!string.IsNullOrEmpty(zslxID))
        //                                        //    {
        //                                        //        row["zslxID"] = zslxID;
        //                                        //    }
        //                                        //}
        //                                        //row["zslx"] = "开发企业资质证书";

        //                                        row["sfzzz"] = "1";
        //                                        string zslx = "", zslxID = "";
        //                                        switch (corpCertInfo.CertType)
        //                                        {
        //                                            //施工
        //                                            case "建筑业":
        //                                                zslxID = "10";
        //                                                zslx = "建筑业资质证";
        //                                                break;
        //                                            case "城市园林绿化":
        //                                                zslxID = "30";
        //                                                zslx = "园林绿化资质证";
        //                                                break;
        //                                            case "设计与施工一体化":
        //                                                zslxID = "20";
        //                                                zslx = "设计施工一体化资质证";
        //                                                break;
        //                                            case "房屋拆迁":
        //                                                zslxID = "130";
        //                                                zslx = "房屋拆迁资质证";
        //                                                break;
        //                                            case "安全生产许可证":
        //                                                zslxID = "140";
        //                                                zslx = "安全生产许可证";
        //                                                break;
        //                                            //勘察
        //                                            case "工程勘察":
        //                                                zslxID = "51";
        //                                                csywlx = "省工程勘察资质证";
        //                                                break;
        //                                            //设计
        //                                            case "工程设计":
        //                                                zslxID = "61";
        //                                                zslx = "省工程设计资质证";
        //                                                break;
        //                                            case "城市规划":
        //                                                zslxID = "18";
        //                                                zslx = "城市规划资质证";
        //                                                break;
        //                                            case "外商城市规划":
        //                                                zslxID = "19";
        //                                                zslx = "外商城市规划资质证";
        //                                                break;

        //                                            //中介机构
        //                                            case "工程招标代理":
        //                                                zslxID = "70";
        //                                                zslx = "招标代理资质证";
        //                                                break;
        //                                            case "工程监理":
        //                                                zslxID = "40";
        //                                                zslx = "工程监理资质证";
        //                                                break;
        //                                            case "工程造价咨询":
        //                                                zslxID = "80";
        //                                                csywlx = "造价咨询资质证";
        //                                                break;
        //                                            case "工程质量检测":
        //                                                zslxID = "90";
        //                                                zslx = "工程检测资质证";
        //                                                break;
        //                                            case "施工图审查":
        //                                                zslxID = "150";
        //                                                csywlx = "施工图审查资质证";
        //                                                break;
        //                                            case "房地产估价":
        //                                                zslxID = "160";
        //                                                zslx = "房地产估价资质证";
        //                                                break;
        //                                            case "物业服务":
        //                                                zslxID = "170";
        //                                                zslx = "物业服务资质证";
        //                                                break;
        //                                            default:
        //                                                break;
        //                                        }
        //                                        row["zslxID"] = zslxID;
        //                                        row["zslx"] = zslx;
        //                                        row["zsbh"] = corpCertInfo.CertCode;
        //                                        if (!string.IsNullOrEmpty(corpCertInfo.ValidDate.Trim()))
        //                                            row["zsyxzrq"] = corpCertInfo.ValidDate;
        //                                        if (!string.IsNullOrEmpty(corpCertInfo.IssueDate.Trim()))
        //                                        {
        //                                            row["fzrq"] = corpCertInfo.IssueDate;
        //                                            row["zsyxqrq"] = corpCertInfo.IssueDate;
        //                                        }

        //                                        row["fzdw"] = corpCertInfo.IssueOrgan;
        //                                        row["xgrqsj"] = corpCertInfo.UpdateDate;
        //                                        row["xgr"] = "定时服务";
        //                                        row["tag"] = tag;
        //                                        row["DataState"] = 0;

        //                                        if (!dataService.Submit_uepp_qyzzzs(dt_qy_zzzs))
        //                                        {
        //                                            Public.WriteLog("单位ID：" + corpCertInfo.CorpCode + "，企业资质证书信息保存失败！");
        //                                        }

        //                                    }
        //                                    catch (Exception ex)
        //                                    {
        //                                        Public.WriteLog("保存企业资质证书信息时出现异常：" + ex.Message);
        //                                    }
        //                                }
        //                            }
        //                            #endregion
        //                        }
        //                    }
        //                }
        //                if (dt_SaveDataLog.Rows.Count > 0)
        //                    dataService.Submit_SaveDataLog(dt_SaveDataLog);

        //                row_DataJkDataDetail_qyxx["allCount"] = allCount_qyxx;
        //                row_DataJkDataDetail_qyxx["successCount"] = success_count_qyxx;
        //                row_DataJkDataDetail_qyxx["IsOk"] = 1;
        //                row_DataJkDataDetail_qyxx["ErrorMsg"] = "";

        //                if (dt_DataJkDataDetail_qyxx.Rows.Count > 0)
        //                    dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_qyxx);
        //            }
        //            catch (Exception ex)
        //            {
        //                row_DataJkDataDetail_qyxx["allCount"] = allCount_qyxx;
        //                row_DataJkDataDetail_qyxx["successCount"] = success_count_qyxx;
        //                row_DataJkDataDetail_qyxx["IsOk"] = 0;
        //                row_DataJkDataDetail_qyxx["ErrorMsg"] = "从江苏建设公共基础数据平台获取企业信息时出现异常:" + ex.Message;

        //                if (dt_DataJkDataDetail_qyxx.Rows.Count > 0)
        //                    dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_qyxx);
        //            }
        //        }
        #endregion


        #region 从江苏建设公共基础数据平台获取建设单位信息-旧版
        //void YourTask_PullDataFromSxxzx_Jsdw(string DataJkLogID)
        //{
        //    Public.WriteLog("执行YourTask_PullDataFromSxxzx_Jsdw方法：");

        //    string tag = Tag.江苏建设公共基础数据平台.ToString();
        //    string userID = "wxszjj01";
        //    DataService dataService = new DataService();
        //    DataTable dt_xzqhdm = dataService.Get_tbXzqdmDic();
        //    XmlHelper helper = new XmlHelper();

        //    //往数据监控日志表项添加一条记录
        //    int allCount_qyxx = 0, success_count_qyxx = 0;
        //    DataTable dt_DataJkDataDetail_qyxx = dataService.GetSchema_DataJkDataDetail();
        //    long Id_DataJkDataDetail_qyxx = dataService.Get_DataJkDataDetailNewID().ToInt64();

        //    DataRow row_DataJkDataDetail_qyxx = dt_DataJkDataDetail_qyxx.NewRow();
        //    dt_DataJkDataDetail_qyxx.Rows.Add(row_DataJkDataDetail_qyxx);

        //    row_DataJkDataDetail_qyxx["ID"] = Id_DataJkDataDetail_qyxx++;
        //    row_DataJkDataDetail_qyxx["DataJkLogID"] = DataJkLogID;
        //    row_DataJkDataDetail_qyxx["tableName"] = "UEPP_Qyjbxx";
        //    row_DataJkDataDetail_qyxx["MethodName"] = "getCorpInfoStream_Inc";
        //    row_DataJkDataDetail_qyxx["bz"] = "从江苏建设公共基础数据平台获取建设单位信息";

        //    //DataDownService.dataDownService dataDownService = new DataDownService.dataDownService();

        //    NewDataService.NewDataService newDataService = new NewDataService.NewDataService();
        //    DataRow row;

        //    try
        //    {
        //        DataTable dt_SaveDataLog = dataService.GetSchema_SaveDataLog();
        //        foreach (DataRow row_xzqdm in dt_xzqhdm.Rows)
        //        {
        //            string xzqdm = row_xzqdm["Code"].ToString2();

        //            //byte[] bytes=  dataDownService.getCorpInfoStream(userID, "320200", xzqdm, String.Empty, "0");

        //            //byte[] bytes = dataDownService.getCorpInfoStream_Inc(userID, "320200", xzqdm,
        //            //    "13", "1991-01-01", DateTime.Now.ToString("yyyy-MM-dd"), "0");

        //            byte[] bytes = newDataService.getCorpInfo(userID, "320200", xzqdm, "13", "0");

        //            string result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);

        //            var index = result.IndexOf("<ReturnInfo>");
        //            if (index >= 0)
        //            {
        //                string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
        //                if (string.IsNullOrEmpty(returnResult))
        //                {
        //                    continue;
        //                }

        //                ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
        //                if (!returnInfo.Status)
        //                {
        //                    continue;
        //                }

        //            }
        //            index = result.IndexOf("<CorpBasicInfo>");
        //            if (index >= 0)
        //            {
        //                string corpBasicInfoString = result.Substring(index, result.LastIndexOf("</CorpBasicInfo>") - index + 16);
        //                CorpBasicInfoBody corpBasicInfoBody = helper.DeserializeXML<CorpBasicInfoBody>("<CorpBasicInfoBody><CorpBasicInfoArray>" + corpBasicInfoString + "</CorpBasicInfoArray></CorpBasicInfoBody>");
        //                if (corpBasicInfoBody != null)
        //                {
        //                    //CorpChiefPersonBody corpChiefPersonArr = new CorpChiefPersonBody();
        //                    //int corpChiefPersonIndex = result.IndexOf("<CorpChiefPerson>");
        //                    //if (corpChiefPersonIndex >= 0)
        //                    //{
        //                    //    string corpChiefPersonString = result.Substring(corpChiefPersonIndex, result.LastIndexOf("</CorpChiefPerson>") - corpChiefPersonIndex + 18);
        //                    //    corpChiefPersonArr = helper.DeserializeXML<CorpChiefPersonBody>(
        //                    //      "<CorpChiefPersonBody><CorpChiefPersonArray>" + corpChiefPersonString + "</CorpChiefPersonArray></CorpChiefPersonBody>");
        //                    //}

        //                    CorpCertInfoBody corpCertInfoArr = new CorpCertInfoBody();
        //                    int CorpCertInfoIndex = result.IndexOf("<CorpCertInfo>");
        //                    if (CorpCertInfoIndex >= 0)
        //                    {
        //                        string corpCertInfoString = result.Substring(CorpCertInfoIndex, result.LastIndexOf("</CorpCertInfo>") - CorpCertInfoIndex + 15);
        //                        corpCertInfoArr = helper.DeserializeXML<CorpCertInfoBody>(
        //                            "<CorpCertInfoBody><CorpCertInfoArray>" + corpCertInfoString + "</CorpCertInfoArray></CorpCertInfoBody>");
        //                    }

        //                    CorpCertQualBody corpCertQualArr = new CorpCertQualBody();
        //                    int CorpCertQualIndex = result.IndexOf("<CorpCertQual>");

        //                    if (CorpCertQualIndex >= 0)
        //                    {
        //                        string corpCertQualString = result.Substring(CorpCertQualIndex, result.LastIndexOf("</CorpCertQual>") - CorpCertQualIndex + 15);
        //                        corpCertQualArr = helper.DeserializeXML<CorpCertQualBody>(
        //                            "<CorpCertQualBody><CorpCertQualArray>" + corpCertQualString + "</CorpCertQualArray></CorpCertQualBody>");
        //                    }

        //                    foreach (CorpBasicInfo corpBasicInfo in corpBasicInfoBody.array)
        //                    {
        //                        DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
        //                        row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_qyxx["ID"];
        //                        row_SaveDataLog["DataXml"] = "";
        //                        row_SaveDataLog["PKID"] = corpBasicInfo.CorpCode;
        //                        row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");

        //                        try
        //                        {
        //                            #region  更新企业基本信息

        //                            DataTable dt = dataService.Get_uepp_jsdw(corpBasicInfo.CorpCode);

        //                            if (corpBasicInfo.CorpCode.Length == 9)
        //                            {
        //                                corpBasicInfo.CorpCode = corpBasicInfo.CorpCode.Substring(0, 8) + '-' + corpBasicInfo.CorpCode.Substring(8, 1);
        //                            }
        //                            //检查该企业是否存在社会信用代码，若存在，则转化为社会信用代码
        //                            if (corpBasicInfo.CorpCode.Length == 10)
        //                            {
        //                                string qyShxydm = dataService.Get_UEPP_Jsdw_Shxydm(corpBasicInfo.CorpCode);
        //                                if (!string.IsNullOrEmpty(qyShxydm))
        //                                {
        //                                    corpBasicInfo.CorpCode = qyShxydm;
        //                                }
        //                            }

        //                            if (dt.Rows.Count == 0)
        //                            {
        //                                row = dt.NewRow();
        //                                dt.Rows.Add(row);
        //                            }
        //                            else
        //                            {
        //                                row = dt.Rows[0];
        //                                if (!string.IsNullOrEmpty(corpBasicInfo.UpdateDate))
        //                                {
        //                                    if (DateTime.Parse(row["xgrqsj"].ToString2()).ToString("yyyy-MM-dd") == DateTime.Parse(corpBasicInfo.UpdateDate).ToString("yyyy-MM-dd"))
        //                                    {
        //                                        continue;
        //                                    }
        //                                }
        //                            }
        //                            row["jsdwID"] = corpBasicInfo.CorpCode;
        //                            row["tag"] = tag;

        //                            row["zzjgdm"] = corpBasicInfo.CorpCode;
        //                            row["jsdw"] = corpBasicInfo.CorpName;
        //                            row["dwflID"] = "1";
        //                            row["dwfl"] = "房地产开发企业";
        //                            row["yb"] = corpBasicInfo.PostCode;
        //                            row["dwdz"] = corpBasicInfo.CorpAddress;
        //                            row["fddbr"] = corpBasicInfo.LegalMan;
        //                            row["yyzz"] = corpBasicInfo.LicenseNo;
        //                            row["fax"] = corpBasicInfo.Fax;
        //                            row["lxdh"] = corpBasicInfo.LinkPhone;
        //                            if (!string.IsNullOrEmpty(corpBasicInfo.UpdateDate))
        //                                row["xgrqsj"] = corpBasicInfo.UpdateDate;
        //                            else
        //                                row["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        //                            row["OperateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //                            row["DataState"] = 0;
        //                            allCount_qyxx++;
        //                            if (!dataService.Submit_uepp_jsdw(dt))
        //                            {
        //                                Public.WriteLog("建设单位：" + corpBasicInfo.CorpName + "," + corpBasicInfo.CorpCode + "，基本信息保存失败！");
        //                                row_SaveDataLog["SaveState"] = 0;
        //                                row_SaveDataLog["Msg"] = "建设单位：" + corpBasicInfo.CorpName + "," + corpBasicInfo.CorpCode + "，基本信息保存失败！";
        //                            }
        //                            else
        //                            {
        //                                success_count_qyxx++;
        //                                row_SaveDataLog["SaveState"] = 1;
        //                                row_SaveDataLog["Msg"] = "";
        //                            }
        //                            #endregion
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            row_SaveDataLog["SaveState"] = 0;
        //                            row_SaveDataLog["Msg"] = "出现异常：" + ex.Message;
        //                        }
        //                        dt_SaveDataLog.Rows.Add(row_SaveDataLog);

        //                    }
        //                }
        //            }
        //        }
        //        if (dt_SaveDataLog.Rows.Count > 0)
        //        {
        //            dataService.Submit_SaveDataLog(dt_SaveDataLog);
        //        }

        //        row_DataJkDataDetail_qyxx["allCount"] = allCount_qyxx;
        //        row_DataJkDataDetail_qyxx["successCount"] = success_count_qyxx;
        //        row_DataJkDataDetail_qyxx["IsOk"] = 1;
        //        row_DataJkDataDetail_qyxx["ErrorMsg"] = "";

        //        if (dt_DataJkDataDetail_qyxx.Rows.Count > 0)
        //            dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_qyxx);
        //    }
        //    catch (Exception ex)
        //    {
        //        row_DataJkDataDetail_qyxx["allCount"] = allCount_qyxx;
        //        row_DataJkDataDetail_qyxx["successCount"] = success_count_qyxx;
        //        row_DataJkDataDetail_qyxx["IsOk"] = 0;
        //        row_DataJkDataDetail_qyxx["ErrorMsg"] = ex.Message;

        //        if (dt_DataJkDataDetail_qyxx.Rows.Count > 0)
        //            dataService.Submit_DataJkDataDetail(dt_DataJkDataDetail_qyxx);
        //    }
        //}
        #endregion


        #region 省外人员信息--旧版
        //void YourTask_PullDataFromSxxzx_Swryxx(string DataJkLogID)
        //{
        ////    int allCount_ryxx = 0, success_count_ryxx = 0;
        ////    string ryzyzglxID = "";
        ////    Public.WriteLog("执行YourTask_PullDataFromSxxzx_Swryxx方法：");
        ////    string tag = Tag.江苏建设公共基础数据平台.ToString();
        ////    string userID = "wxszjj01";
        ////    DataService dataService = new DataService();
        ////    XmlHelper helper = new XmlHelper();
        ////    DataRow row;
        ////    NewDataService.NewDataService newDataService = new WxjzgcjczyTimerService.NewDataService.NewDataService();
        ////    byte[] bytes = newDataService.getOutPersonCert(userID, "0");

        ////    string result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);

        ////    int index = result.IndexOf("<ReturnInfo>");
        ////    if (index >= 0)
        ////    {
        ////        string returnResult = result.Substring(index, result.LastIndexOf("</ReturnInfo>") - index + 13);
        ////        if (string.IsNullOrEmpty(returnResult))
        ////        {
        ////            return;
        ////        }

        ////        ReturnInfo returnInfo = helper.DeserializeXML<ReturnInfo>(returnResult);
        ////        if (!returnInfo.Status)
        ////        {
        ////            return;
        ////        }

        ////    }
        ////    else
        ////    {
        ////        return;
        ////    }

        ////    index = result.IndexOf("<OutChiefPerson>");
        ////    if (index >= 0)
        ////    {
        ////        string outChiefPersonString = result.Substring(index, result.LastIndexOf("</OutChiefPerson>") - index + 17);
        ////        OutChiefPersonBody personBasicInfoBody = helper.DeserializeXML<OutChiefPersonBody>("<OutChiefPersonBody><OutChiefPersonArray>" + outChiefPersonString + "</OutChiefPersonArray></OutChiefPersonBody>");

        ////        index = result.IndexOf("<OutConsInfo>");
        ////        if (index >= 0)
        ////        {
        ////            string outConsInfoString = result.Substring(index, result.LastIndexOf("</OutConsInfo>") - index + 14);
        ////           OutConsInfoBody  outConsInfoBody = helper.DeserializeXML<OutConsInfoBody>("<OutConsInfoBody><OutConsInfoArray>" + outConsInfoString + "</OutConsInfoArray></OutConsInfoBody>");

        ////        }

        ////        #region

        ////        if (personBasicInfoBody != null)
        ////        {
        ////            int rowIndex = -1;
        ////            DataTable dt_SaveDataLog = dataService.GetSchema_SaveDataLog();
        ////            //往数据监控日志表项添加一条记录
        ////            DataTable dt_DataJkDataDetail_qyxx = dataService.GetSchema_DataJkDataDetail();
        ////            long Id_DataJkDataDetail_qyxx = dataService.Get_DataJkDataDetailNewID().ToInt64();

        ////            int allCount_qyxx = 0, success_count_qyxx = 0;
        ////            DataRow row_DataJkDataDetail_qyxx = dt_DataJkDataDetail_qyxx.NewRow();
        ////            dt_DataJkDataDetail_qyxx.Rows.Add(row_DataJkDataDetail_qyxx);

        ////            row_DataJkDataDetail_qyxx["ID"] = Id_DataJkDataDetail_qyxx++;
        ////            row_DataJkDataDetail_qyxx["DataJkLogID"] = DataJkLogID;
        ////            row_DataJkDataDetail_qyxx["tableName"] = "UEPP_Ryjbxx";
        ////            row_DataJkDataDetail_qyxx["MethodName"] = "getOutCorpInfo";
        ////            row_DataJkDataDetail_qyxx["bz"] = "从江苏建设公共基础数据平台拉取省外人员信息";

        ////            foreach (OutChiefPerson personBasicInfo in personBasicInfoBody.array)
        ////            {
        ////                DataRow row_SaveDataLog = dt_SaveDataLog.NewRow();
        ////                row_SaveDataLog["DataJkDataDetailID"] = row_DataJkDataDetail_qyxx["ID"];
        ////                row_SaveDataLog["DataXml"] = "";
        ////                row_SaveDataLog["PKID"] = personBasicInfo.IDCardNo;
        ////                row_SaveDataLog["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");

        ////                try
        ////                {
        ////                    #region 人员基本信息
        ////                    DataTable dt_ryxx = dataService.Get_UEPP_Ryjbxx(personBasicInfo.IDCardNo);
        ////                    if (dt_ryxx.Rows.Count == 0)
        ////                    {
        ////                        row = dt_ryxx.NewRow();
        ////                        dt_ryxx.Rows.Add(row);
        ////                        row["ryID"] = personBasicInfo.IDCardNo;
        ////                    }
        ////                    else
        ////                    {
        ////                        row = dt_ryxx.Rows[0];
        ////                        if (!string.IsNullOrEmpty(row["xgrqsj"].ToString2()) && !string.IsNullOrEmpty(personBasicInfo.UpdateDate))
        ////                            if (DateTime.Parse(row["xgrqsj"].ToString()).ToString("yyyy-MM-dd") == DateTime.Parse(personBasicInfo.UpdateDate).ToString("yyyy-MM-dd"))
        ////                            {
        ////                                continue;
        ////                            }
        ////                    }
        ////                    row["tag"] = tag;
        ////                    row["xm"] = personBasicInfo.PersonName;
        ////                    switch (personBasicInfo.IDCardType)
        ////                    {
        ////                        case "身份证":
        ////                            row["zjlxID"] = "1";
        ////                            break;
        ////                        case "护照":
        ////                            row["zjlxID"] = "3";
        ////                            break;
        ////                        case "军官证":
        ////                            row["zjlxID"] = "2";
        ////                            break;
        ////                        case "台湾居民身份证":
        ////                            row["zjlxID"] = "4";
        ////                            break;
        ////                        case "香港永久性居民身份证":
        ////                            row["zjlxID"] = "5";
        ////                            break;
        ////                        case "警官证":
        ////                            row["zjlxID"] = "6";
        ////                            break;
        ////                        case "其他":
        ////                            row["zjlxID"] = "9";
        ////                            break;
        ////                    }

        ////                    row["zjlx"] = personBasicInfo.IDCardType;
        ////                    row["zjhm"] = personBasicInfo.IDCardNo;
        ////                    row["xb"] = personBasicInfo.Sex;
        ////                    if (!string.IsNullOrEmpty(personBasicInfo.Birthday))
        ////                        row["csrq"] = personBasicInfo.Birthday;
        ////                    row["mz"] = personBasicInfo.Nationality;
        ////                    row["zc"] = personBasicInfo.ProTitle;
        ////                    row["zw"] = personBasicInfo.DutyDesc;
        ////                    if (string.IsNullOrEmpty(personBasicInfo.UpdateDate))
        ////                        personBasicInfo.UpdateDate = DateTime.Now.ToString("yyyy-MM-dd");
        ////                    row["xgrqsj"] = personBasicInfo.UpdateDate;

        ////                    row["xgr"] = "定时服务";
        ////                    row["DataState"] = 0;
        ////                    row["AJ_EXISTINIDCARDS"] = "2";
        ////                    row["AJ_IsRefuse"] = "0";

        ////                    allCount_ryxx++;
        ////                    if (!dataService.Submit_uepp_ryjbxx(dt_ryxx))
        ////                    {
        ////                        //Public.WriteLog("建设单位人员信息保存失败，ryID：" + personBasicInfo.IDCardNo);
        ////                        row_SaveDataLog["SaveState"] = 0;
        ////                        row_SaveDataLog["Msg"] = "建设单位人员信息保存失败，ryID：" + personBasicInfo.IDCardNo;
        ////                    }
        ////                    else
        ////                    {
        ////                        success_count_ryxx++;

        ////                        row_SaveDataLog["SaveState"] = 1;
        ////                        row_SaveDataLog["Msg"] = "";
        ////                    }
        ////                    #endregion

        ////                    dt_SaveDataLog.Rows.Add(row_SaveDataLog);

        ////                    #region 人员执业资格

        ////                    DataTable dt_ryzyzg = dataService.Get_uepp_Ryzyzg(personBasicInfo.IDCardNo);
        ////                    rowIndex = -1;
        ////                    for (int i = 0; i < dt_ryzyzg.Rows.Count; i++)
        ////                    {
        ////                        if (dt_ryzyzg.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxID)
        ////                        {
        ////                            rowIndex = i;
        ////                            break;
        ////                        }
        ////                    }
        ////                    if (rowIndex < 0)
        ////                    {
        ////                        row = dt_ryzyzg.NewRow();
        ////                        dt_ryzyzg.Rows.Add(row);
        ////                        row["ryID"] = personBasicInfo.IDCardNo;
        ////                        row["ryzyzglxID"] = ryzyzglxID;
        ////                        row["ryzyzglx"] = "注册建筑师";

        ////                        row["balxID"] = 1;
        ////                        row["balx"] = "长期备案";

        ////                        row["DataState"] = 0;
        ////                        row["tag"] = tag;
        ////                        row["xgr"] = "定时服务";
        ////                        row["xgrqsj"] = personBasicInfo.UpdateDate;
        ////                        dataService.Submit_uepp_Ryzyzg(dt_ryzyzg);
        ////                    }

        ////                    #endregion

        ////                    #region 人员专业明细

        ////                    //DataTable dt_ryzymx = dataService.Get_uepp_Ryzymx(personBasicInfo.IDCardNo);
        ////                    //rowIndex = -1;
        ////                    //for (int i = 0; i < dt_ryzymx.Rows.Count; i++)
        ////                    //{
        ////                    //    if (dt_ryzymx.Rows[i]["ryzyzglxID"].ToString2() == ryzyzglxID)
        ////                    //    {
        ////                    //        rowIndex = i;
        ////                    //        break;
        ////                    //    }
        ////                    //}
        ////                    //if (rowIndex < 0)
        ////                    //{
        ////                    //    row = dt_ryzymx.NewRow();
        ////                    //    dt_ryzymx.Rows.Add(row);
        ////                    //    row["ryID"] = personBasicInfo.IDCardNo;
        ////                    //    row["ryzyzglxID"] = ryzyzglxID;
        ////                    //    row["ryzyzglx"] = "注册建筑师";
        ////                    //    row["zyzgdjID"] = 21;
        ////                    //    row["zyzgdj"] = "壹级";
        ////                    //    row["DataState"] = 0;
        ////                    //    row["tag"] = tag;
        ////                    //    row["xgr"] = "定时服务";
        ////                    //    row["xgrqsj"] = personBasicInfo.UpdateDate;
        ////                    //    dataService.Submit_uepp_Ryzymx(dt_ryzymx);
        ////                    //}
        ////                    #endregion
        ////                }
        ////                catch (Exception ex)
        ////                {
        ////                    //Public.WriteLog("出现异常，ryID：" + personBasicInfo.IDCardNo + ",Exception:" + ex.Message);

        ////                    row_SaveDataLog["SaveState"] = 0;
        ////                    row_SaveDataLog["Msg"] = ex.Message;
        ////                    dt_SaveDataLog.Rows.Add(row_SaveDataLog);
        ////                }

        ////            }
        ////        }
        ////        #endregion

        ////        //#region 人员证书

        ////        //if (personRegCertBody != null)
        ////        //{
        ////        //    foreach (PersonRegCert personRegCert in personRegCertBody.array)
        ////        //    {
        ////        //        try
        ////        //        {
        ////        //            if (string.IsNullOrEmpty(personRegCert.UpdateDate))
        ////        //                personRegCert.UpdateDate = DateTime.Now.ToString("yyyy-MM-dd");

        ////        //            #region 人员证书基本信息
        ////        //            DataTable dt_ryzs = dataService.Get_uepp_Ryzs(personRegCert.IDCardNo);
        ////        //            int rowIndex = -1;
        ////        //            for (int i = 0; i < dt_ryzs.Rows.Count; i++)
        ////        //            {
        ////        //                if (dt_ryzs.Rows[i]["ryzyzglxid"].ToString2() == ryzyzglxID)
        ////        //                {
        ////        //                    rowIndex = i;
        ////        //                    break;
        ////        //                }
        ////        //            }
        ////        //            if (rowIndex < 0)
        ////        //            {

        ////        //                row = dt_ryzs.NewRow();
        ////        //                dt_ryzs.Rows.Add(row);
        ////        //                row["zsjlId"] = dataService.Get_uepp_RyzsNewID();
        ////        //                row["ryID"] = personRegCert.IDCardNo;
        ////        //                row["ryzyzglxID"] = ryzyzglxID;
        ////        //                row["ryzyzglx"] = "注册建筑师";
        ////        //                row["ryzslxID"] = 151;
        ////        //                row["ryzslx"] = "注册建筑师资格证";

        ////        //            }
        ////        //            else
        ////        //            {
        ////        //                row = dt_ryzs.Rows[rowIndex];

        ////        //            }
        ////        //            row["sfzzz"] = 1;
        ////        //            row["zsbh"] = personRegCert.QualCertNo;
        ////        //            if (!string.IsNullOrEmpty(personRegCert.IssueDate))
        ////        //            {
        ////        //                row["fzrq"] = personRegCert.IssueDate;
        ////        //                row["zsyxqrq"] = personRegCert.IssueDate;
        ////        //            }
        ////        //            if (!string.IsNullOrEmpty(personRegCert.ValidDate))
        ////        //                row["zsyxzrq"] = personRegCert.ValidDate;

        ////        //            row["fzdw"] = personRegCert.IssueOrgan;

        ////        //            row["Status"] = personRegCert.Status;
        ////        //            if (!string.IsNullOrEmpty(personRegCert.QualIssueDate))
        ////        //                row["QualIssueDate"] = personRegCert.QualIssueDate;
        ////        //            row["StampNo"] = personRegCert.StampNo;
        ////        //            row["RegNo"] = personRegCert.RegNo;

        ////        //            row["DataState"] = 0;
        ////        //            row["tag"] = tag;
        ////        //            row["xgr"] = "定时服务";
        ////        //            row["xgrqsj"] = personRegCert.UpdateDate;
        ////        //            dataService.Submit_uepp_Ryzs(dt_ryzs);

        ////        //            #endregion

        ////        //            #region 企业人员关系表

        ////        //            DataTable dt_qyry = dataService.Get_uepp_Qyry(personRegCert.IDCardNo, personRegCert.CorpCode, ryzyzglxID);
        ////        //            if (dt_qyry.Rows.Count == 0)
        ////        //            {
        ////        //                if (personRegCert.Status.ToString2() != "2")
        ////        //                {
        ////        //                    row = dt_qyry.NewRow();
        ////        //                    dt_qyry.Rows.Add(row);
        ////        //                    row["ryID"] = personRegCert.IDCardNo;
        ////        //                    row["qyID"] = personRegCert.CorpCode;
        ////        //                    row["ryzyzglxID"] = ryzyzglxID;
        ////        //                    row["ryzyzglx"] = "注册建筑师";
        ////        //                    row["DataState"] = 0;
        ////        //                    row["tag"] = tag;
        ////        //                    row["xgr"] = "定时服务";
        ////        //                    row["xgrqsj"] = personRegCert.UpdateDate;
        ////        //                    dataService.Submit_uepp_qyry(dt_qyry);
        ////        //                }
        ////        //            }
        ////        //            else
        ////        //            {
        ////        //                if (personRegCert.Status.ToString2() == "2")
        ////        //                {
        ////        //                    foreach (DataRow item in dt_qyry.Rows)
        ////        //                    {
        ////        //                        item.Delete();
        ////        //                    }

        ////        //                    dataService.Submit_uepp_qyry(dt_qyry);
        ////        //                }
        ////        //            }
        ////        //            #endregion
        ////        //        }
        ////        //        catch (Exception ex)
        ////        //        {
        ////        //            Public.WriteLog("更新人员证书出现异常，ryID：" + personRegCert.IDCardNo + ",Exception:" + ex.Message);
        ////        //        }
        ////        //    }
        ////        //}

        ////        //#endregion

        ////    }
        //}
        #endregion






        static void YourTask_PullDataFromSythpt()
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
            }
        }

    }
}
