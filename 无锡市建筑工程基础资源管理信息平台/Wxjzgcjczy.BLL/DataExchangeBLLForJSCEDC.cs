using Wxjzgcjczy.Common;
using System;
using System.Data;
using Wxjzgcjczy.BLL.model;
using Bigdesk8;

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





        private static TimeSpan compareDateTime(DateTime beginTime, DateTime endTime)
        {
            TimeSpan span1 = new TimeSpan(beginTime.Hour, beginTime.Minute, beginTime.Second);
            TimeSpan span2 = new TimeSpan(endTime.Hour, endTime.Minute, endTime.Second);
            TimeSpan span = span2 - span1;
            return span;
        }


    }
}
