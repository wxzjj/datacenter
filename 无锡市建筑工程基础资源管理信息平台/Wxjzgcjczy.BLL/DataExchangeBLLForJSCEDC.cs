using Wxjzgcjczy.Common;
using System;
using System.Data;
using Wxjzgcjczy.BLL.model;
using Bigdesk8;
using Bigdesk8.Data;

namespace Wxjzgcjczy.BLL
{


    /// <summary>
    /// 功能：从江苏建设公共基础数据平台下行数据的处理类
    /// 作者：huangzhengyu
    /// 时间：2018-06-22
    /// </summary>
    public class DataExchangeBLLForJSCEDC
    {
        private readonly Wxjzgcjczy.DAL.Sqlserver.DataExchangeDAL DAL = new Wxjzgcjczy.DAL.Sqlserver.DataExchangeDAL();
        string userID = "wxszjj01";

        XmlHelper xmlHelper = new XmlHelper();
        

        public DataExchangeBLLForJSCEDC()
        {
            DAL.DB = new Wxjzgcjczy.DAL.Sqlserver.DatabaseOperator();
        }
 
        #region 下行企业注册执业人员

        /// <summary>
        /// 从江苏建设公共基础数据平台拉取人员（注册执业人员）信息
        /// </summary>
        public string PullDataCorpRegStaff(String qyID)
        {
            string resultMsg = "OK";
            try
            {
                DateTime beginTime = DateTime.Now;
                BLLCommon.WriteLog("手动获取企业注册执业人员：" + qyID  + " ["+ beginTime.ToString("yyyy-MM-dd HH:mm:ss") + "]");
                string tag = Tag.江苏建设公共基础数据平台.ToString();
                string userID = "wxszjj01";
                string[] regType = { "1001", "1002", "1101", "1102", "1210", "1220", "1310", "1320", "1330", "1410", "1420", "1511", "1512", "1521", "1522", "1530", "1600", "1700", "1800", "1900", "2000", "2100", "2200" };
 
                XmlHelper helper = new XmlHelper();
                Base64EncodeHelper base64EncodeHelper = new Base64EncodeHelper();

                NewDataService.NewDataService newDataService = new Wxjzgcjczy.BLL.NewDataService.NewDataService();
                DataRow row;

                try
                {

                    for (int retp = 0; retp < regType.Length; retp++)
                    {
                        byte[] bytes;
                        string result = String.Empty;
                        int index = -1;

                        #region 获取注册执业人员信息

                        if (regType[retp] == "1210")
                        {
                            BLLCommon.WriteLog("1210" );
                        }

                        bytes = newDataService.getPersonRegCert_Corp(userID, qyID, regType[retp], "0");
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
                                BLLCommon.WriteLog("获取注册执业人员数目：" + personRegCertBody.array.Count);
                                foreach (PersonRegCert personRegCert in personRegCertBody.array)
                                {
                                    string ryzyzglxID = String.Empty;
                                    string ryzclb = String.Empty;
                                    string ryzyzglx = String.Empty;
                                    string ryzslxID = String.Empty;
                                    string ryzslx = String.Empty;
                                    string zyzgdjID = String.Empty;
                                    string zyzgdj = String.Empty;

                                    if (string.IsNullOrEmpty(personRegCert.UpdateDate))
                                        personRegCert.UpdateDate = DateTime.Now.ToString("yyyy-MM-dd");


                                    try
                                    {
                                        #region 人员基本信息
                                        DataTable dt_ryxx = DAL.Get_UEPP_Ryjbxx(personRegCert.IDCardNo);
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

                                            if (!DAL.Submit_uepp_ryjbxx(dt_ryxx))
                                            {
                                                BLLCommon.WriteLog("建设单位人员信息保存失败，ryID：" + personRegCert.IDCardNo);

                                            }
                                        }

                                        #endregion


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


                                        DataTable dt_ryzyzg = DAL.Get_uepp_Ryzyzg(personRegCert.IDCardNo);
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
                                            DAL.Submit_uepp_Ryzyzg(dt_ryzyzg);
                                        }

                                        #endregion

                                        #region 人员证书基本信息
                                        DataTable dt_ryzs = DAL.Get_uepp_Ryzs(personRegCert.IDCardNo);
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
                                            row["zsjlId"] = DAL.Get_uepp_RyzsNewID();
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
                                        DAL.Submit_uepp_Ryzs(dt_ryzs);

                                        #endregion

                                        #region 企业人员关系表

                                        DataTable dt_qyry = DAL.Get_uepp_Qyry(personRegCert.IDCardNo, personRegCert.CorpCode, ryzyzglxID);
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
                                                DAL.Submit_uepp_qyry(dt_qyry);
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

                                                DAL.Submit_uepp_qyry(dt_qyry);
                                            }
                                        }
                                        #endregion


                                    }
                                    catch (Exception ex)
                                    {
                                        
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
                                BLLCommon.WriteLog("人员专业明细size:" + personRegMajorBody.array.Count);
                                foreach (PersonRegMajor personRegMajor in personRegMajorBody.array)
                                {
                                    string ryzyzglxID = String.Empty;
                                    string ryzclb = String.Empty;
                                    string ryzyzglx = String.Empty;
                                    string ryzslxID = String.Empty;
                                    string ryzslx = String.Empty;
                                    string zyzgdjID = String.Empty;
                                    string zyzgdj = String.Empty;

                                   
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

                                        DataTable dt_ryzymx = DAL.Get_uepp_Ryzymx(personRegMajor.IDCardNo);
                                        int rowIndex = -1;
                                        bool needUpdateFlag = false;
                                        for (int i = 0; i < dt_ryzymx.Rows.Count; i++)
                                        {
                                            if (dt_ryzymx.Rows[i]["ryzyzglxID"].ToString() == ryzyzglxID
                                                && dt_ryzymx.Rows[i]["ryzslxID"].ToString2() == ryzslxID)
                                            {
                                                rowIndex = i;
                                                break;
                                            }
                                        }
                                        //BLLCommon.WriteLog("人员专业明细详细:" + personRegMajor.IDCardNo + "," + personRegMajor.RegType + ",rowIndex:" + rowIndex);

                                        if (rowIndex < 0)
                                        {
                                            row = dt_ryzymx.NewRow();
                                            dt_ryzymx.Rows.Add(row);
                                        }
                                        else
                                        {
                                            row = dt_ryzymx.Rows[rowIndex];
                                            if (!string.IsNullOrEmpty(personRegMajor.UpdateDate) && !string.IsNullOrEmpty(row["xgrqsj"].ToString()) &&
                                                DateTime.Parse(personRegMajor.UpdateDate).CompareTo(DateTime.Parse(row["xgrqsj"].ToString())) > 0)
                                            {
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
                                            DAL.Submit_uepp_Ryzymx(dt_ryzymx);
                                        }

                                        #endregion
                                    }
                                    catch (Exception ex)
                                    {
                                         
                                    }
                                }
                            }

                        }

                        #endregion
                    }
                  
                    DateTime endTime = DateTime.Now;
                    TimeSpan span = compareDateTime(beginTime, endTime);
                    BLLCommon.WriteLog(string.Format("结束YourTask_PullDataFromSxxzx_Ryxx_Zczyry任务:{0}，历时：{1}时{2}分{3}秒", endTime.ToString("yyyy-MM-dd HH:mm:ss"), span.Hours, span.Minutes, span.Seconds));

 
                }
                catch (Exception ex)
                {
                    resultMsg = ex.Message;
                    BLLCommon.WriteLog("ex.Message:" + ex.Message);

                }
            }
            catch (Exception ex)
            {
                resultMsg = ex.Message;
                BLLCommon.WriteLog("Exception:" + ex.Message);
            }
            return resultMsg;
        }

        
        #endregion


        #region 企业资质信息
        /// <summary>
        /// 下行企业相关信息
        /// </summary>
        /// <param name="qyID"></param>
        /// <returns></returns>
        public string PullDataCorpCert(String qyID)
        {
            string resultMsg = "OK";

            DateTime beginTime = DateTime.Now;
            BLLCommon.WriteLog("PullDataCorpCert：" + beginTime.ToString("yyyy-MM-dd HH:mm:ss"));
            string tag = Tag.江苏建设公共基础数据平台.ToString();

            XmlHelper helper = new XmlHelper();
            string[] certType = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "14", "15", "16", "20" };

            NewDataService.NewDataService newDataService = new Wxjzgcjczy.BLL.NewDataService.NewDataService();
            DataRow row;

            for (int cttp = 0; cttp < certType.Length; cttp++)
            {
                try
                {
                    #region 获取企业基本信息
                    byte[] bytes = newDataService.getCorpInfo_Single(userID, qyID, certType[cttp], "0");
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
                                    string qyShxydm = DAL.Get_UEPP_Qyjbxx_Shxydm(corpBasicInfo.CorpCode);
                                    if (!string.IsNullOrEmpty(qyShxydm))
                                    {
                                        corpBasicInfo.CorpCode = qyShxydm;
                                    }
                                }


                                try
                                {
                                    #region  更新企业基本信息

                                    DataTable dt = DAL.Get_uepp_qyjbxx(corpBasicInfo.CorpCode);

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
                                        if (!row["needUpdateFlag"].ToBoolean())
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
                                        SqlParameterCollection sp = DAL.CreateSqlParameterCollection();

                                        row["Province"] = corpBasicInfo.ProvinceCode;
                                        sp.Add("@CodeInfo", corpBasicInfo.ProvinceCode.ToString2());
                                        string provinceCode = DAL.ExecuteSql("select top 1 Code from  UEPP_Code where CodeType='城市地区' and  CodeInfo=@CodeInfo", sp);
                                        if (!string.IsNullOrEmpty(provinceCode))
                                        {
                                            row["ProvinceID"] = provinceCode;
                                            sp.Clear();
                                            if (!string.IsNullOrEmpty(corpBasicInfo.CityCode.ToString2()))
                                            {
                                                row["City"] = corpBasicInfo.CityCode;

                                                sp.Add("@CodeInfo", corpBasicInfo.CityCode.ToString2());
                                                sp.Add("@parentCode", provinceCode);
                                                string cityCode = DAL.ExecuteSql("select top 1 Code from  UEPP_Code where CodeType='城市地区' and ParentCode=@parentCode and  CodeInfo=@CodeInfo", sp);
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
                                                    string countyCode = DAL.ExecuteSql("select top 1 Code from  UEPP_Code where CodeType='城市地区' and ParentCode=@parentCode and  CodeInfo=@CodeInfo", sp);
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
                                        SqlParameterCollection sp = DAL.CreateSqlParameterCollection();
                                        sp.Add("@CodeInfo", corpBasicInfo.CorpTypeDesc.ToString2().Trim());
                                        string jjxzID = DAL.ExecuteSql("select * from  UEPP_Code where CodeType='企业经济性质' and  CodeInfo=@CodeInfo", sp);
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

                                    if (!DAL.Submit_uepp_qyjbxx(dt))
                                    {
                                        BLLCommon.WriteLog("从江苏建设公共基础数据平台获取的单位：" + corpBasicInfo.CorpName + "," + corpBasicInfo.CorpCode + "，基本信息保存失败！");
                                        continue;
                                    }
                                    #endregion
                                }
                                catch (Exception ex)
                                {
                                    BLLCommon.WriteLog("从江苏建设公共基础数据平台获取的单位：" + corpBasicInfo.CorpName + "," + corpBasicInfo.CorpCode + "，基本信息保存失败！" + ex.Message);
                                }
                            }

                        }
                    }
                    #endregion

                    #region  获取企业证书信息
                    CorpCertInfoBody corpCertInfoArr = new CorpCertInfoBody(); //企业证书信息
                    bytes = newDataService.getCorpCert_Single(userID, qyID, certType[cttp], "0");
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

                    #endregion

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
                                    string qyShxydm = DAL.Get_UEPP_Qyjbxx_Shxydm(corpCertQual.CorpCode);
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

                                DataTable dt_qycsyw = DAL.Get_uepp_Qycsyw_sjsgyth(corpCertQual.CorpCode, csywlxID);

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
                                    DAL.Submit_uepp_qycsyw(dt_qycsyw);
                                }
                                #endregion

                                DataTable dt_jsdw_zzmx = DAL.Get_uepp_zzmxxx_qyxx_nostatus(corpCertQual.CorpCode, csywlxID);

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
                                    row["ID"] = DAL.Get_uepp_qyxxmx_NewID();
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
                                                    SqlParameterCollection sp = DAL.CreateSqlParameterCollection();
                                                    sp.Add("@CodeInfo", corpCertQual.TradeType);
                                                    sp.Add("@parentCode", csywlxID);
                                                    string zzxlID = DAL.ExecuteSql(sql, sp);
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
                                                            SqlParameterCollection sp = DAL.CreateSqlParameterCollection();
                                                            sp.Add("@CodeInfo", corpCertQual.MajorType);
                                                            sp.Add("@parentCode", row["zzxlID"]);
                                                            sp.Add("@CodeInfo1", corpCertQual.MajorType.ToString2().Replace("分包", ""));
                                                            string zzlbID = DAL.ExecuteSql(sql, sp);
                                                            if (!string.IsNullOrEmpty(zzlbID))
                                                                row["zzlbID"] = zzlbID;
                                                        }
                                                    }




                                row["zzdj"] = corpCertQual.TitleLevel;
                                //新增证书跟资质的一对多关联关系
                                row["zsbh"] = corpCertQual.CertCode;

                                if (!string.IsNullOrEmpty(corpCertQual.TitleLevel))
                                {
                                    SqlParameterCollection sp = DAL.CreateSqlParameterCollection();
                                    sp.Add("@CodeInfo", corpCertQual.TitleLevel);

                                    string sql = "select Code from UEPP_Code  where  CodeType='企业资质等级' and ParentCodeType='企业资质序列' and CodeInfo=@CodeInfo ";
                                    string zzdjID = DAL.ExecuteSql(sql, sp);
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

                                if (!DAL.Submit_uepp_qyzzmx(dt_jsdw_zzmx))
                                {
                                    BLLCommon.WriteLog("单位ID：" + corpCertQual.CorpCode + "，企业资质保存失败！");
                                }
                            }
                            catch (Exception ex)
                            {
                                BLLCommon.WriteLog("保存企业资质时出现异常：" + ex.Message);
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
                                    string qyShxydm = DAL.Get_UEPP_Qyjbxx_Shxydm(corpCertInfo.CorpCode);
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

                                DataTable dt_qy_zzzs = DAL.Get_uepp_zzzsxx_qyxx(corpCertInfo.CorpCode);

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
                                    row["zsjlId"] = DAL.Get_uepp_qyQyzs_NewID();
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

                                if (!DAL.Submit_uepp_qyzzzs(dt_qy_zzzs))
                                {
                                    BLLCommon.WriteLog("单位ID：" + corpCertInfo.CorpCode + "，企业资质证书信息保存失败！");
                                }

                            }
                            catch (Exception ex)
                            {
                                BLLCommon.WriteLog("保存企业资质证书信息时出现异常：" + corpCertInfo.CorpCode + ex.Message);
                            }
                        }
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    resultMsg = ex.Message;
                    BLLCommon.WriteLog(ex.Message);
                }

            }

            DateTime endTime = DateTime.Now;
            TimeSpan span = compareDateTime(beginTime, endTime);
            BLLCommon.WriteLog(string.Format("结束YourTask_PullDataFromSxxzx_qyxx任务:{0}，历时：{1}时{2}分{3}秒", endTime.ToString("yyyy-MM-dd HH:mm:ss"), span.Hours, span.Minutes, span.Seconds));
            return resultMsg;

        }

        #endregion



        private static TimeSpan compareDateTime(DateTime beginTime, DateTime endTime)
        {
            TimeSpan span1 = new TimeSpan(beginTime.Hour, beginTime.Minute, beginTime.Second);
            TimeSpan span2 = new TimeSpan(endTime.Hour, endTime.Minute, endTime.Second);
            TimeSpan span = span2 - span1;
            return span;
        }


    }
}
