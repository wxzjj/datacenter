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
using System.Linq;
using System.Text.RegularExpressions;

namespace Wxjzgcjczy.BLL
{

    public enum Xmhj
    {
        Zbtzs, Htbawj, Sgtschgs, Sgxkz, Jgysba
    }

    /// <summary>
    /// 功能： 无锡数据中心与各县市系统数据交换业务处理类
    /// 作者：孙刚
    /// 时间：2015-03-31
    /// </summary>
    public class DataExchangeBLL
    {
        private readonly Wxjzgcjczy.DAL.Sqlserver.DataExchangeDAL DAL = new Wxjzgcjczy.DAL.Sqlserver.DataExchangeDAL();
        string userName = "320200", password = "we&gjh45H";

        private readonly DataExchangeBLLForYHT DALYHT = new DataExchangeBLLForYHT();

        XmlHelper xmlHelper = new XmlHelper();
        ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient client = new ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient();

        public DataExchangeBLL()
        {
            DAL.DB = new Wxjzgcjczy.DAL.Sqlserver.DatabaseOperator();
        }

        public bool SubmitData(string sql, DataTable dt)
        {

            return DAL.DB.Update(sql, null, dt);

        }
        #region 读取
        /// <summary>
        /// 读取无锡数据中心数据(TBProjectInfo)
        /// </summary>
        /// <returns>处理结果信息</returns>
        public DataTable GetTBData_TBProjectInfo(List<IDataItem> conditions)
        {
            DataTable dt_TBProjectInfo = DAL.GetTBData_TBProjectInfo(conditions);
            return dt_TBProjectInfo;

        }

        public string ExecSaveTBDataToSt(string tableName, string xmlData)
        {
            string resultSt = client.SaveTBDataToSt("tableName", xmlData, userName, password);
            return resultSt;
        }

        /// <summary>
        /// 读取无锡数据中心数据(xm_gcdjb_dtxm)
        /// </summary>
        /// <returns></returns>
        public DataTable GetTBData_xm_gcdjb_dtxm(List<IDataItem> conditions)
        {
            DataTable dt_TBProjectInfo = DAL.GetTBData_xm_gcdjb_dtxm(conditions);
            return dt_TBProjectInfo;

        }
        /// <summary>
        /// 读取无锡数据中心数据(TBTenderInfo)
        /// </summary>
        /// <returns></returns>
        public DataTable GetTBData_TBTenderInfo(List<IDataItem> conditions)
        {
            DataTable dt = DAL.GetTBData_TBTenderInfo(conditions);
            return dt;

        }
        /// <summary>
        ///  读取无锡数据中心数据(TBContractRecordManage)
        /// </summary>
        /// <returns></returns>
        public DataTable GetTBData_TBContractRecordManage(List<IDataItem> conditions)
        {
            DataTable dt = DAL.GetTBData_TBContractRecordManage(conditions);
            return dt;

        }
        /// <summary>
        /// 读取无锡数据中心数据(TBProjectCensorInfo)
        /// </summary>
        /// <returns></returns>
        public DataTable GetTBData_TBProjectCensorInfo(List<IDataItem> conditions)
        {
            DataTable dt = DAL.GetTBData_TBProjectCensorInfo(conditions);
            return dt;

        }
        public DataTable GetNumAndSbState_TBProjectCensorInfo(List<IDataItem> conditions)
        {
            DataTable dt = DAL.GetNumAndSbState_TBProjectCensorInfo(conditions);
            return dt;

        }
        /// <summary>
        /// 读取无锡数据中心数据(TBProjectDesignEconUserInfo)
        /// </summary>
        /// <returns></returns>
        public DataTable GetTBData_TBProjectDesignEconUserInfo(List<IDataItem> conditions)
        {
            DataTable dt = DAL.GetTBData_TBProjectDesignEconUserInfo(conditions);
            return dt;

        }
        /// <summary>
        /// 读取无锡数据中心数据(TBBuilderLicenceManage)
        /// </summary>
        /// <returns></returns>
        public DataTable GetTBData_TBBuilderLicenceManage(List<IDataItem> conditions)
        {
            DataTable dt = DAL.GetTBData_TBBuilderLicenceManage(conditions);
            return dt;

        }
        /// <summary>
        /// 读取无锡数据中心数据(TBProjectBuilderUserInfo)
        /// </summary>
        /// <returns></returns>
        public DataTable GetTBData_TBProjectBuilderUserInfo(List<IDataItem> conditions)
        {
            DataTable dt = DAL.GetTBData_TBProjectBuilderUserInfo(conditions);
            return dt;

        }
        /// <summary>
        /// 读取无锡数据中心数据(TBProjectFinishManage)
        /// </summary>
        /// <returns></returns>
        public DataTable GetTBData_TBProjectFinishManage(List<IDataItem> conditions)
        {
            DataTable dt = DAL.GetTBData_TBProjectFinishManage(conditions);
            return dt;

        }
        /// <summary>
        /// 读取无锡数据中心数据(aj_gcjbxx)
        /// </summary>
        /// <returns></returns>
        public DataTable GetTBData_aj_gcjbxx(List<IDataItem> conditions)
        {
            DataTable dt = DAL.GetTBData_aj_gcjbxx(conditions);
            return dt;

        }
        /// <summary>
        /// 读取无锡数据中心数据(zj_gcjbxx)
        /// </summary>
        /// <returns></returns>
        public DataTable GetTBData_zj_gcjbxx(List<IDataItem> conditions)
        {
            DataTable dt = DAL.GetTBData_zj_gcjbxx(conditions);
            return dt;

        }
        /// <summary>
        /// 读取无锡数据中心数据(zj_gcjbxx_zrdw)
        /// </summary>
        /// <returns></returns>
        public DataTable GetTBData_zj_gcjbxx_zrdw(List<IDataItem> conditions)
        {
            DataTable dt = DAL.GetTBData_zj_gcjbxx_zrdw(conditions);
            return dt;

        }

        public DataTable GetTBData_aj_zj_sgxk_relation(List<IDataItem> conditions)
        {
            DataTable dt = DAL.GetTBData_aj_zj_sgxk_relation(conditions);
            return dt;

        }



        #region 获取代码表里的代码信息
        /// <summary>
        /// 获取项目分类编号集合
        /// </summary>
        /// <returns></returns>
        public List<string> GetPrjType()
        {
            List<string> list = new List<string>();
            DataTable dt = DAL.GetPrjType();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(row["Code"].ToString2());
            }
            return list;

        }

        /// <summary>
        /// 获取项目分类编号集合
        /// </summary>
        /// <returns></returns>
        public List<string> GetPrjProperty()
        {
            List<string> list = new List<string>();
            DataTable dt = DAL.GetPrjProperty();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(row["Code"].ToString2());
            }
            return list;

        }
        /// <summary>
        /// 工程用途
        /// </summary>
        /// <returns></returns>
        public List<string> GetPrjFunction()
        {
            List<string> list = new List<string>();
            DataTable dt = DAL.GetPrjFunction();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(row["Code"].ToString2());
            }
            return list;

        }
        /// <summary>
        /// 招标类型
        /// </summary>
        /// <returns></returns>
        public List<string> GetTenderClass()
        {
            List<string> list = new List<string>();
            DataTable dt = DAL.GetTenderClass();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(row["Code"].ToString2());
            }
            return list;

        }
        /// <summary>
        /// 招标方式
        /// </summary>
        /// <returns></returns>
        public List<string> GetTenderType()
        {
            List<string> list = new List<string>();
            DataTable dt = DAL.GetTenderType();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(row["Code"].ToString2());
            }
            return list;

        }
        /// <summary>
        /// 证件类型
        /// </summary>
        /// <returns></returns>
        public List<string> GetIDCardType()
        {
            List<string> list = new List<string>();
            DataTable dt = DAL.GetIDCardType();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(row["Code"].ToString2());
            }
            return list;

        }
        /// <summary>
        /// 合同类别
        /// </summary>
        /// <returns></returns>
        public List<string> GetContractType()
        {
            List<string> list = new List<string>();
            DataTable dt = DAL.GetContractType();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(row["Code"].ToString2());
            }
            return list;

        }
        /// <summary>
        /// 勘察设计从业人员承担角色
        /// </summary>
        /// <returns></returns>
        public List<string> GetWorkDuty()
        {
            List<string> list = new List<string>();
            DataTable dt = DAL.GetWorkDuty();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(row["Code"].ToString2());
            }
            return list;

        }
        /// <summary>
        /// 注册人员注册类型及等级
        /// </summary>
        /// <returns></returns>
        public List<string> GetSpecialtyType()
        {
            List<string> list = new List<string>();
            DataTable dt = DAL.GetSpecialtyType();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(row["Code"].ToString2());
            }
            return list;

        }
        /// <summary>
        /// 结构体系
        /// </summary>
        /// <returns></returns>
        public List<string> GetStructureType()
        {
            List<string> list = new List<string>();
            DataTable dt = DAL.GetStructureType();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(row["Code"].ToString2());
            }
            return list;

        }

        public List<string> Get_tbXzqdmDic()
        {
            List<string> list = new List<string>();
            DataTable dt = DAL.Get_tbXzqdmDic();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(row["Code"].ToString2());
            }
            return list;

        }

        public List<string> Get_tbXzqdmDicForShenBao(string deptType)
        {
            List<string> list = new List<string>();
            DataTable dt = DAL.GetCountryCodes(deptType);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(row["countryCode"].ToString2());
            }
            return list;

        }
        /// <summary>
        /// 获取接口用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public DataTable GetInterfaceUserInfo(string userName, string pwd)
        {
            return DAL.GetInterfaceUserInfo(userName, pwd);


        }

        /// <summary>
        /// 获取四库内部接口用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public DataTable GetInterfaceUserInfoForDataCenter(string userName, string pwd)
        {
            return DAL.GetInterfaceUserInfoForDataCenter(userName, pwd);
        }


        #endregion



        /// <summary>
        /// 根据单位分类获取建设单位信息
        /// </summary>
        /// <param name="dwfl"></param>
        /// <returns></returns>
        public DataTable Get_uepp_jsdwByDwfl(string dwfl)
        {
            return DAL.Get_uepp_jsdwByDwfl(dwfl);
        }

        /// <summary>
        /// 获取企业从事业务类型
        /// </summary>
        /// <returns></returns>
        public List<string> Get_uepp_Qycsywlx()
        {
            DataTable dt = DAL.Get_uepp_Qycsywlx();
            List<string> list = new List<string>();

            foreach (DataRow row_csywlx in dt.Rows)
            {
                list.Add(row_csywlx["CodeInfo"].ToString());
            }
            return list;
        }

        /// <summary>
        /// 获取人员执业资格类型
        /// </summary>
        /// <returns></returns>
        public List<string> Get_uepp_Ryzyzglx()
        {
            DataTable dt = DAL.Get_uepp_Ryzyzglx();
            List<string> list = new List<string>();

            foreach (DataRow row_ryzyzglx in dt.Rows)
            {
                list.Add(row_ryzyzglx["CodeInfo"].ToString());
            }
            return list;
        }

        public DataTable Get_uepp_Sgdw(string dwfl,string clrqS,string clrqE)
        {
            return DAL.Get_uepp_Sgdw(dwfl, clrqS, clrqE);
        }

        /// <summary>
        /// 获取勘察单位基本信息
        /// </summary>
        /// <returns></returns>
        public DataTable Get_uepp_Kcdw()
        {
            return DAL.Get_uepp_Kcdw();
        }

        public DataTable Get_uepp_Sjdw(string dwfl)
        {

            return DAL.Get_uepp_Sjdw(dwfl);
        }

        /// <summary>
        /// 获取中介机构信息
        /// </summary>
        /// <param name="dwfl"></param>
        /// <returns></returns>
        public DataTable Get_uepp_Zjjg(string dwfl)
        {
            return DAL.Get_uepp_Zjjg(dwfl);
        }


        /// <summary>
        /// 获取注册执业人员信息
        /// </summary>
        /// <param name="ryzyzglx"></param>
        /// <returns></returns>
        public DataTable Get_Ryxx_Zczyry(string ryzyzglx)
        {
            return DAL.Get_Ryxx_Zczyry(ryzyzglx);
        }

        /// <summary>
        /// 获取安全生产管理人员信息
        /// </summary>
        /// <param name="ryzyzglx"></param>
        /// <returns></returns>
        public DataTable Get_Ryxx_Aqscglry(string ryzyzglx)
        {
            return DAL.Get_Ryxx_Aqscglry(ryzyzglx);
        }

        /// <summary>
        /// 获取专业岗位管理人员信息
        /// </summary>
        /// <param name="ryzyzglx"></param>
        /// <returns></returns>
        public DataTable Get_Ryxx_Zygwglry(string ryzyzglx)
        {
            return DAL.Get_Ryxx_Zygwglry(ryzyzglx);
        }

        /// <summary>
        /// 获取人员证书信息
        /// </summary>
        /// <param name="ryID"></param>
        /// <returns></returns>
        public DataTable Get_Ryzs_Aqscglry(string ryID)
        {
            return DAL.Get_Ryzs_Aqscglry(ryID);
        }



        /// <summary>
        /// 获取人员专业信息
        /// </summary>
        /// <param name="qyID"></param>
        /// <returns></returns>
        public DataTable Get_Ryzy_Aqscglry(string ryID)
        {
            return DAL.Get_Ryzy_Aqscglry(ryID);
        }

        /// <summary>
        /// 获取施工监理合同备案信息
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public DataTable Get_TBContractRecordManage_SgJlHtba()
        {
            return DAL.Get_TBContractRecordManage_SgJlHtba();
        }


        /// <summary>
        /// 获取施工图审查信息
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public DataTable Get_TBProjectCensorInfoByPrjNum(string PrjNum)
        {
            return DAL.Get_TBProjectCensorInfoByPrjNum(PrjNum);
        }

        /// <summary>
        /// 获取企业资质信息
        /// </summary>
        /// <param name="qyID"></param>
        /// <returns></returns>
        public DataTable Get_uepp_SgQyzzByQyID(string qyID)
        {
            return DAL.Get_uepp_SgQyzzByQyID(qyID);
        }
        public DataTable Get_uepp_KcQyzzByQyID(string qyID)
        {
            return DAL.Get_uepp_KcQyzzByQyID(qyID);
        }

        /// <summary>
        /// 获取设计企业证书信息
        /// </summary>
        /// <param name="qyID"></param>
        /// <returns></returns>
        public DataTable Get_uepp_SjQyzsByQyID(string qyID)
        {
            return DAL.Get_uepp_SjQyzsByQyID(qyID);
        }

        /// <summary>
        /// 获取施工企业证书信息
        /// </summary>
        /// <param name="qyID"></param>
        /// <returns></returns>
        public DataTable Get_uepp_SgQyzsByQyID(string qyID)
        {
            return DAL.Get_uepp_SgQyzsByQyID(qyID);
        }
        public DataTable Get_uepp_ZjjgQyzzByQyID(string qyID)
        {
            return DAL.Get_uepp_ZjjgQyzzByQyID(qyID);

        }
        /// <summary>
        /// 获取中介机构信息的证书信息
        /// </summary>
        /// <param name="qyID"></param>
        /// <returns></returns>
        public DataTable Get_uepp_ZjjgQyzsByQyID(string qyID)
        {
            return DAL.Get_uepp_ZjjgQyzsByQyID(qyID);
        }
        /// <summary>
        /// 获取勘察企业证书基本信息
        /// </summary>
        /// <param name="qyID"></param>
        /// <returns></returns>
        public DataTable Get_uepp_KcQyzsByQyID(string qyID)
        {
            return DAL.Get_uepp_KcQyzsByQyID(qyID);
        }
        /// <summary>
        /// 获取人员专业信息
        /// </summary>
        /// <param name="qyID"></param>
        /// <returns></returns>
        public DataTable Get_Ryzy_Zczyry(string ryID)
        {
            return DAL.Get_Ryzy_Zczyry(ryID);

        }
        /// <summary>
        /// 获取人员专业信息
        /// </summary>
        /// <param name="qyID"></param>
        /// <returns></returns>
        public DataTable Get_Ryzy_Zygwglry(string ryID)
        {
            return DAL.Get_Ryzy_Zygwglry(ryID);
        }
        /// <summary>
        /// 获取人员证书信息
        /// </summary>
        /// <param name="ryID"></param>
        /// <returns></returns>
        public DataTable Get_Ryzs_Zczyry(string ryID)
        {
            return DAL.Get_Ryzs_Zczyry(ryID);
        }
        /// <summary>
        /// 获取人员证书信息
        /// </summary>
        /// <param name="ryID"></param>
        /// <returns></returns>
        public DataTable Get_Ryzs_Zygwglry(string ryID)
        {
            return DAL.Get_Ryzs_Zygwglry(ryID);

        }


        public DataTable Get_uepp_SjQyzzByQyID(string qyID)
        {
            return DAL.Get_uepp_SjQyzzByQyID(qyID);
        }

        /// <summary>
        /// 获取企业信用考评信息
        /// </summary>
        /// <param name="ryID"></param>
        /// <returns></returns>
        public DataTable Get_QyXykp(List<IDataItem> list)
        {
            return DAL.Get_QyXykp(list);
        }
        #endregion


        public List<string> GetPrjNums_TBProjectInfo()
        {
            List<string> list = new List<string>();
            DataTable dt = DAL.GetPrjNums_TBProjectInfo();
            foreach (DataRow item in dt.Rows)
            {
                list.Add(item["PrjNum"].ToString2());
            }
            return list;
        }

        public List<string> GetCensorNums_TBProjectCensorInfo()
        {
            List<string> list = new List<string>();
            DataTable dt = DAL.GetCensorNums_TBProjectCensorInfo();
            foreach (DataRow item in dt.Rows)
            {
                list.Add(item["CensorNum"].ToString2());
            }
            return list;
        }

        #region 保存
        /// <summary>
        /// 向无锡数据中心传送数据(TBProjectInfo)
        /// </summary>
        /// <param name="xmlData">XML内容</param>
        /// <returns>处理结果信息</returns>
        public ProcessResultData SaveTBData_TBProjectInfo(string user, DataTable dt_Data)
        {
            ProcessResultData result = new ProcessResultData();
            StringBuilder prjInnerNumSb = new StringBuilder();

            string msg = String.Empty;
            string[] fields = new string[] { "PrjNum", "PrjInnerNum", "PrjName", "BuildCorpName", "BDate", "EDate" };

            List<string> PrjTypes = this.GetPrjType();
            List<string> PrjFunctionList = this.GetPrjFunction();

            //Regex reg_zzjgdm = new Regex(@"^\d{8}-[A-z0-9]$");
            Regex reg_province = new Regex(@"\d{2}0000");
            Regex reg_city = new Regex(@"\d{4}00");
            Regex reg_county = new Regex(@"\d{6}");

            foreach (DataRow item in dt_Data.Rows)
            {
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
                if (item["PrjNum"].ToString2().Length != 16)
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "PrjNum编码格式不正确，必须是由数字组成的16位编码！";
                    return result;
                }

                if (!PrjTypes.Exists(p => p.Equals(item["PrjTypeNum"].ToString2(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "PrjTypeNum不合法，引用自tbPrjTypeDic表！";
                    return result;
                }

                if (!PrjFunctionList.Exists(p => p.Equals(item["PrjFunctionNum"].ToString2(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "PrjFunctionNum不合法，引用自tbPrjFunctionDic表！";
                    return result;
                }

                //if (!reg_zzjgdm.IsMatch(item["BuildCorpCode"].ToString2()))
                //if (item["BuildCorpCode"].ToString2().Length != 10 && item["BuildCorpCode"].ToString2().Length != 18)
                if (!Validator.IsUnifiedSocialCreditCodeOrOrgCode(item["BuildCorpCode"].ToString2()))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "BuildCorpCode不合法,格式不正确，应该为“XXXXXXXX-X”格式！";
                    return result;
                }

                if (!reg_province.IsMatch(item["ProvinceNum"].ToString2()))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "ProvinceNum不合法，引用自tbXzqdmDic表！";
                    return result;
                }

                if (!reg_city.IsMatch(item["CityNum"].ToString2()))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "CityNum不合法，引用自tbXzqdmDic表！";
                    return result;
                }

                if (!reg_county.IsMatch(item["CountyNum"].ToString2()))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "CountyNum不合法，引用自tbXzqdmDic表！";
                    return result;
                }

                prjInnerNumSb.AppendFormat("{0},", item["PrjInnerNum"].ToString2());
            }
            if (prjInnerNumSb.Length > 0)
                prjInnerNumSb.Remove(prjInnerNumSb.Length - 1, 1);

            DataTable dt_TBProjectInfo = DAL.GetTBData_TBProjectInfo(prjInnerNumSb.ToString());
            DataRow row;

            foreach (DataRow item in dt_Data.Rows)
            {
                int rowIndex = -1;
                for (int i = 0; i < dt_TBProjectInfo.Rows.Count; i++)
                {
                    if (dt_TBProjectInfo.Rows[i]["PrjInnerNum"].ToString2().Equals(item["PrjInnerNum"].ToString2()))
                    {
                        rowIndex = i;
                        break;
                    }
                }

                if (rowIndex >= 0)
                {
                    row = dt_TBProjectInfo.Rows[rowIndex];
                    DataTableHelp.DataRow2DataRow(item, row, new List<string>() { "PKID", "PrjInnerNum" });
                }
                else
                {
                    row = dt_TBProjectInfo.NewRow();
                    DataTableHelp.DataRow2DataRow(item, row);
                    row["PKID"] = Guid.NewGuid();
                    dt_TBProjectInfo.Rows.Add(row);
                }

                row["updateUser"] = user;
                row["xgrqsj"] = DateTime.Now;

                //如果立项文号为空，则重新补充立项文号：(lxwh_type+fb_year+num) FROM [WJSJZX].[dbo].[DG_Programme_Info]
                try
                {
                    if (string.IsNullOrEmpty(row["PrjApprovalNum"].ToString()))
                    {
                        BLLCommon.WriteLog(row["PrjNum"].ToString() + " 立项文号为空:");
                        DataTable dtPrjApprovalNum = DALYHT.GetTBProjectInfo_PrjApprovalNum(row["PrjNum"].ToString2());
                        if (dtPrjApprovalNum != null && dtPrjApprovalNum.Rows.Count > 0)
                        {
                            row["PrjApprovalNum"] = dtPrjApprovalNum.Rows[0][0].ToString();
                            BLLCommon.WriteLog("重新获取立项文号:" + row["PrjApprovalNum"].ToString());
                        }
                    }
                }
                catch (Exception e)
                {
                    BLLCommon.WriteLog("重新补充立项文号出错:" + e.Message);
                }
                

            }
            if (dt_TBProjectInfo.Rows.Count > 0)
            {
                if (DAL.SaveTBData_TBProjectInfo(dt_TBProjectInfo))
                {
                    try
                    {
                        BLLCommon.WriteLog("获取了 " + dt_TBProjectInfo.Rows.Count + " 条TBProjectInfo数据！");
                        string xmlData = "";

                        foreach (DataRow dataRow in dt_TBProjectInfo.Rows)
                        {
                            dataRow["sbdqbm"] = "320200";
                            if (dataRow["BuildCorpCode"].ToString2().Length == 18)
                            {
                                string shxydm = dataRow["BuildCorpCode"].ToString2();
                                dataRow["BuildCorpCode"] = shxydm.Substring(8, 8) + "-" + shxydm.Substring(16, 1);
                            }

                            xmlData = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);

                            string resultSt = client.SaveTBDataToSt("TBProjectInfo", xmlData, userName, password);

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
                            }
                            else
                            {
                                row["OperateState"] = 0;
                                row["Msg"] = "上传成功";
                            }
                            if (dt.Rows.Count > 0)
                            {
                                DAL.SaveTBData_SaveToStLog(dt);
                            }
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
                    }

                    result.code = ProcessResult.数据保存成功;
                    result.message = "数据保存成功！";
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
            BLLCommon.WriteLog("SaveTBData_TBProjectInfo结果：" + result.message);
            return result;
        }


        /// <summary>
        /// 向无锡数据中心传送项目登记补充数据(TBProjectInfoAddtional)
        /// </summary>
        /// <param name="xmlData">XML内容</param>
        /// <returns>处理结果信息</returns>
        public ProcessResultData SaveTBData_TBProjectAdditionalInfo(string user, DataTable dt_Data)
        {
            ProcessResultData result = new ProcessResultData();
            StringBuilder prjInnerNumSb = new StringBuilder();

            string msg = String.Empty;
            string[] fields = new string[] { "prjnum", "prjpassword", "gyzzpl", "dzyx", "lxr", "yddh", "xmtz", "gytze", "gytzbl", "lxtzze", "sbdqbm" };

            List<string> districts = this.Get_tbXzqdmDic();
            //以前有些项目使用320200区划编码的问题
            districts.Add("320200");

            //一次传送一条项目登记补充数据
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
            if (!Validator.IsProjectNum(item["prjnum"].ToString2()))
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = "prjnum格式不正确，必须是由数字组成的16位编码！";
                return result;
            }

            if (!districts.Exists(p => p.Equals(item["sbdqbm"].ToString2(), StringComparison.CurrentCultureIgnoreCase)))
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = "sbdqbm不合法，引用自tbXzqdmDic表！";
                return result;
            }

            DataTable dt_TBProjectAddInfo = DAL.GetTBData_TBProjectAdditionalInfo(item["prjnum"].ToString2());
            DataRow row;

            if (dt_TBProjectAddInfo != null && dt_TBProjectAddInfo.Rows.Count > 0)
            {
                row = dt_TBProjectAddInfo.Rows[0];
                DataTableHelp.DataRow2DataRow(item, row, new List<string>() { "PKID", "prjnum" });
                row["updatetime"] = row["createtime"];
            }
            else
            {
                row = dt_TBProjectAddInfo.NewRow();
                DataTableHelp.DataRow2DataRow(item, row);
                row["PKID"] = Guid.NewGuid();
                row["createtime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                row["updatetime"] = row["createtime"];
                dt_TBProjectAddInfo.Rows.Add(row);
            }
            row["updateuser"] = user;

            if (dt_TBProjectAddInfo.Rows.Count > 0)
            {
                if (DAL.SaveTBData_TBProjectAddInfo(dt_TBProjectAddInfo))
                {
                    BLLCommon.WriteLog("获取了 " + dt_TBProjectAddInfo.Rows.Count + " 条TBProjectAdditionalInfo数据！");
                    string xmlData = "";

                    DataRow dataRow = dt_TBProjectAddInfo.Rows[0];
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
                        BLLCommon.WriteLog("向省一体化平台传送项目登记补充数据:" + xmlData + "\n结果：" + addResultSt);

                        if (addResultSt != "OK")
                        {
                            row["OperateState"] = 1;
                            row["Msg"] = addResultSt;

                            //1分钟后重新上报一次
                            /**
                            System.Timers.Timer timer = new System.Timers.Timer(60*1000);
                            timer.Elapsed += delegate(object sender, System.Timers.ElapsedEventArgs e)
                            {
                                timer.Enabled = false;
                                client.getProjectAdd(dataRow["prjnum"].ToString(), xmlData, userName, password);
                                BLLCommon.WriteLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "再次向省一体化平台传送项目登记补充数据:" + xmlData + "\n结果：" + addResultSt);
                            };
                            timer.Enabled = true; 
                            */
                        }
                        else
                        {
                            row["OperateState"] = 0;
                            row["Msg"] = "上传成功";
                        }


                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            BLLCommon.WriteLog(ex.Message);
                            row["OperateState"] = 1;
                            row["Msg"] = ex.Message;
                        }
                        catch { }
                    }
                    finally
                    {
                        if (dt.Rows.Count > 0)
                        {
                            DAL.SaveTBData_SaveToStLog(dt);
                        }
                    }

                    result.code = ProcessResult.数据保存成功;
                    result.message = "数据保存成功！";
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
            BLLCommon.WriteLog("SaveTBData_TBProjectAdditionalInfo结果：" + result.message);
            return result;
        }

        /// <summary>
        /// 向无锡数据中心传送信用考评数据(Xypj_kpjlhz)
        /// </summary>
        /// <param name="xmlData">XML内容</param>
        /// <returns>处理结果信息</returns>
        public ProcessResultData SaveTBData_Xykp(string user, DataTable dt_Data)
        {
            ProcessResultData result = new ProcessResultData();
            StringBuilder prjInnerNumSb = new StringBuilder();

            string msg = String.Empty;
            string[] fields = new string[] { "kp_nf", "kp_jd", "kpqymc", "kpqy_zzjgdm", "zhdf"};

            //一次传送一条信用考评数据
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
            
            if (!Validator.IsUnifiedSocialCreditCodeOrOrgCode(item["kpqy_zzjgdm"].ToString2()))
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = "企业组织机构代码kpqy_zzjgdm格式不正确，必须XXXXXXXX-X或者统一社会信用代码";
                return result;
            }
            else if (Validator.IsUnifiedSocialCreditCode(item["kpqy_zzjgdm"].ToString2()))
            {
                //统一社会信用代码转换成组织机构代码
                item["kpqy_zzjgdm"] = item["kpqy_zzjgdm"].ToString2().Substring(8, 8) + "-" + item["kpqy_zzjgdm"].ToString2().Substring(16, 1);
            }

            if (string.IsNullOrEmpty(item["khnf"].ToString2()))
            {
                item["khnf"] = item["kp_nf"];
            }
            if (string.IsNullOrEmpty(item["khyf"].ToString2()))
            {
                item["khyf"] = item["kp_jd"];
            }

            //kp_type为空值或者没填，默认设置为0
            if (string.IsNullOrEmpty(item["kp_type"].ToString2()))
            {
                item["kp_type"] = 0;
            }

            DataTable dt_Xykp = DAL.GetTBData_Xykp(item["kpqy_zzjgdm"].ToString2(), item["kp_nf"].ToInt32(), item["kp_jd"].ToInt32(), item["kp_type"].ToInt32());
            DataRow row;

            if (dt_Xykp != null && dt_Xykp.Rows.Count > 0)
            {
                row = dt_Xykp.Rows[0];
                DataTableHelp.DataRow2DataRow(item, row, new List<string>() { "kpqy_zzjgdm", "kp_nf", "kp_jd","kp_type" });
                row["updateTime"] = DateTime.Now;
            }
            else
            {
                row = dt_Xykp.NewRow();
                DataTableHelp.DataRow2DataRow(item, row);
                row["createTime"] = DateTime.Now;
                row["updateTime"] = row["createTime"];
                dt_Xykp.Rows.Add(row);
            }
            row["updateUser"] = user;

            if (dt_Xykp.Rows.Count > 0)
            {
                if (DAL.SaveTBData_Xpkp(dt_Xykp))
                {
                    //BLLCommon.WriteLog("获取了 " + dt_Xykp.Rows.Count + " 条TBProjectAdditionalInfo数据！");
                    string xmlData = "";

                    DataRow dataRow = dt_Xykp.Rows[0];
                    //dataRow["sbdqbm"] = "320200";//设置上报地区编码为无锡市

                    try
                    {
                        //向省一体化平台传送项目登记补充数据, 目前考评类型kp_type为2的市政的，只保存在四库，不往省里推送
                        if (dataRow["kp_type"].ToString() == null || dataRow["kp_type"].ToInt32() != 2)
                        {
                            xmlData = xmlHelper.ConvertDataRowToXMLWithBase64EncodingInclude(dataRow, new string[] { "kp_nf", "kp_jd", "kpqymc", "kpqy_zzjgdm", "zhdf", "khnf", "khyf" });
                            string addResultSt = client.SaveStData("xypj_kpjlhz", xmlData, userName, "W123YheAge", dataRow["oper"].ToString2());
                            BLLCommon.WriteLog("向省一体化平台传送信用考评数据:" + xmlData + "\n结果：" + addResultSt);

                            if (addResultSt != "OK")
                            {
                                result.code = ProcessResult.保存失败和失败原因;
                                result.message = addResultSt;
                            }
                            else
                            {
                                result.code = ProcessResult.数据保存成功;
                                result.message = addResultSt;
                            }
                        }
                        else
                        {
                            result.code = ProcessResult.数据保存成功;
                            result.message = "成功保存到四库";
                        }
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            BLLCommon.WriteLog(ex.Message);
                            result.code = ProcessResult.保存失败和失败原因;
                            result.message = ex.Message;
                        }
                        catch { }
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
            BLLCommon.WriteLog("SaveTBData_Xykp结果：" + result.message);
            return result;
        }

        /// <summary>
        /// 向无锡数据中心传送数据(xm_gcdjb_dtxm)
        /// </summary>
        /// <param name="xmlData">XML内容</param>
        /// <returns>处理结果信息</returns>
        public ProcessResultData SaveTBData_xm_gcdjb_dtxm(string user, DataTable dt_Data)
        {
            ProcessResultData result = new ProcessResultData();

            StringBuilder fxnbbmSb = new StringBuilder();
            string[] fields = new string[] { "fxbm", "fxnbbm", "xmmc" };
            string msg = String.Empty;
            List<string> list_PrjNums = GetPrjNums_TBProjectInfo();
            if (list_PrjNums.Count == 0)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = "还没有立项项目信息！";
                return result;
            }


            List<string> novalidates = new List<string>();
            novalidates.Add(String.Empty);
            novalidates.Add("无");
            novalidates.Add("/");

            foreach (DataRow item in dt_Data.Rows)
            {
                if (!list_PrjNums.Exists(p => p.Equals(item["PrjNum"].ToString2())))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = String.Format("PrjNum不存在，值“{0}”非法！", item["PrjNum"].ToString2());
                    return result;
                }

                if (BLL.BLLCommon.DataFieldIsNullOrEmpty(novalidates, fields, item, out msg))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = msg + "不能为空！";
                    return result;
                }

                fxnbbmSb.AppendFormat("{0},", item["fxnbbm"].ToString2());
            }
            if (fxnbbmSb.Length > 0)
                fxnbbmSb.Remove(fxnbbmSb.Length - 1, 1);

            DataTable dt = DAL.GetTBData_xm_gcdjb_dtxm(fxnbbmSb.ToString());

            DataRow row;
            StringBuilder PKIDs = new StringBuilder();
            foreach (DataRow item in dt_Data.Rows)
            {
                int rowIndex = -1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["fxnbbm"].ToString2().Equals(item["fxnbbm"].ToString2(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        rowIndex = i;
                    }
                }

                if (rowIndex >= 0)
                {
                    row = dt.Rows[0];
                    DataTableHelp.DataRow2DataRow(item, row, new List<string>() { "PKID" });
                    PKIDs.AppendFormat("{0},", row["PKID"]);
                }
                else
                {
                    row = dt.NewRow();
                    DataTableHelp.DataRow2DataRow(item, row);
                    dt.Rows.Add(row);
                }

                row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                row["updateUser"] = user;
            }

            if (dt.Rows.Count > 0)
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (DAL.SaveTBData_xm_gcdjb_dtxm(dt))
                    {
                        //DataTable dt_log = DAL.GetTBData_SaveToStLog2("xm_gcdjb_dtxm", PKIDs.ToString());
                        if (PKIDs.Length > 0)
                        {
                            PKIDs.Remove(PKIDs.Length - 1, 1);
                            DAL.UpdateSaveToStLog("xm_gcdjb_dtxm", PKIDs.ToString());
                        }
                        //foreach (DataRow row2 in dt_log.Rows)
                        //{
                        //    row2["OperateState"] = 1;
                        //    row2["Msg"] = "已修改，未上报至省厅";
                        //}
                        //if (dt_log.Rows.Count > 0)
                        //{
                        //    if (DAL.SaveTBData_SaveToStLog(dt_log))
                        //    {
                        //        scope.Complete();
                        //        result.code = ProcessResult.数据保存成功;
                        //        result.message = "数据保存成功！";
                        //    }
                        //    else
                        //    {
                        //        result.code = ProcessResult.保存失败和失败原因;
                        //        result.message = "数据保存失败！";
                        //    }
                        //}
                        //else
                        //{
                        //    scope.Complete();
                        //    result.code = ProcessResult.数据保存成功;
                        //    result.message = "数据保存成功！";
                        //}
                        scope.Complete();
                        result.code = ProcessResult.数据保存成功;
                        result.message = "数据保存成功！";
                    }
                    else
                    {
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = "网络故障，数据保存失败！";

                    }
                }

            }
            else
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = "非法格式的数据！";
            }
            return result;
        }


        /// <summary>
        /// 向无锡数据中心传送数据(TBTenderInfo)
        /// </summary>
        /// <param name="xmlData">XML内容</param>
        /// <returns>处理结果信息</returns>
        public ProcessResultData SaveTBData_TBTenderInfo(string user, DataTable dt_Data)
        {
            ProcessResultData result = new ProcessResultData();
            List<string> list_PrjNums = GetPrjNums_TBProjectInfo();
            if (list_PrjNums.Count == 0)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = "还没有立项项目信息！";
                return result;
            }

            string[] fields = new string[] { "TenderName", "TenderInnerNum", "TenderResultDate", "TenderMoney", "Area", "TenderCorpName", "shypbf", "TenderCorpCode" };
            string[] agencyCorpName = new string[] { "AgencyCorpName" };
            string[] agencyCorpCode = new string[] { "AgencyCorpCode"};
            string msg = String.Empty;

            List<string> novalidates = new List<string>();
            novalidates.Add(String.Empty);
            novalidates.Add("无");
            novalidates.Add("/");

            List<string> tenderClassList = this.GetTenderClass();
            List<string> tenderTypeList = this.GetTenderType();
            List<string> IDCardTypeList = this.GetIDCardType();

            //Regex reg_zzjgdm = new Regex(@"^\d{8}-[A-z0-9]$");

            foreach (DataRow item in dt_Data.Rows)
            {
                if (!list_PrjNums.Exists(p => p.Equals(item["PrjNum"].ToString2(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "PrjNum不正确，编号为“" + item["PrjNum"] + "”的立项项目不存在！";
                    return result;
                }

                if (!tenderClassList.Exists(p => p.Equals(item["TenderClassNum"].ToString2(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "TenderClassNum不正确，值为“" + item["TenderClassNum"] + "”未参照引用自表tbTenderClassDic！";
                    return result;
                }
                if (!IDCardTypeList.Exists(p => p.Equals(item["IDCardTypeNum"].ToString2(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "IDCardTypeNum不正确，值为“" + item["IDCardTypeNum"] + "”未参照引用自表tbIDCardTypeDic！";
                    return result;
                }
                if (!tenderTypeList.Exists(p => p.Equals(item["TenderTypeNum"].ToString2(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "TenderTypeNum不正确，值为“" + item["TenderTypeNum"] + "”未参照引用自表tbTenderTypeDic！";
                    return result;
                }

                //if (!reg.IsMatch(item["AgencyCorpCode"].ToString2()))
                //对于直接发包（TenderTypeNum=003）的招投标，AgencyCorpCode,AgencyCorpName, 可以为空

                //BLLCommon.WriteLog("AgencyCorpCode:" + item["AgencyCorpCode"].ToString2() + ", AgencyCorpName:" + item["AgencyCorpName"].ToString2());
                if (!(item["TenderTypeNum"].ToString2() == "003" && BLL.BLLCommon.DataFieldIsNullOrEmpty(novalidates, agencyCorpCode, item, out msg)))
                {
                    if ((item["AgencyCorpCode"].ToString2().Length != 10 || item["AgencyCorpCode"].ToString2().IndexOf('-') != 8) && item["AgencyCorpCode"].ToString2().Length != 18)
                    {
                        if (item["AgencyCorpCode"].ToString2().Length == 9)
                        {
                            item["AgencyCorpCode"] = item["AgencyCorpCode"].ToString2().Substring(0, 8) + "-" + item["AgencyCorpCode"].ToString2().Substring(8, 1);
                        }
                        else
                        {
                            result.code = ProcessResult.保存失败和失败原因;
                            result.message = "AgencyCorpCode不合法,格式不正确，应该为“XXXXXXXX-X”格式！";
                            return result;
                        }
                    }
                    msg = String.Empty;
                    if (BLL.BLLCommon.DataFieldIsNullOrEmpty(novalidates, agencyCorpName, item, out msg))
                    {
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = msg + "不能为空！";
                        return result;
                    }
                }
                msg = String.Empty;
                if (BLL.BLLCommon.DataFieldIsNullOrEmpty(novalidates, fields, item, out msg))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = msg + "不能为空！";
                    return result;
                }

                //校验中标单位组织机构代码
                if (!Validator.IsUnifiedSocialCreditCodeOrOrgCode(item["TenderCorpCode"].ToString2()))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "TenderCorpCode不合法,格式不正确，应该为“XXXXXXXX-X”格式或者18位统一社会信用代码！";
                    return result;
                }


            }

            DataRow row;
            DataTable dt;

            foreach (DataRow item in dt_Data.Rows)
            {
                dt = DAL.GetTBData_TBTenderInfo(item["TenderInnerNum"].ToString2());
                int flag = 0;
                if (dt.Rows.Count > 0)
                {
                    row = dt.Rows[0];
                    DataTableHelp.DataRow2DataRow(item, row, new List<string>() { "PKID", "TenderNum" });
                    flag++;
                }
                else
                {
                    row = dt.NewRow();
                    DataTableHelp.DataRow2DataRow(item, row);
                    row["PKID"] = Guid.NewGuid();
                    row["TenderNum"] = this.GetMaxXmhjNumber_TBTenderInfo(Xmhj.Zbtzs, row["PrjNum"].ToString(), row["TenderClassNum"].ToString());
                    dt.Rows.Add(row);

                }
                row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                row["updateUser"] = user;

                if (!DAL.SaveTBData_TBTenderInfo(dt))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "数据保存失败！";
                    return result;
                }
                if (flag > 0)
                {
                    DataTable dt_log = DAL.GetTBData_SaveToStLog("TBTenderInfo", dt.Rows[0]["PKID"].ToString());
                    if (dt_log.Rows.Count > 0)
                    {
                        dt_log.Rows[0]["OperateState"] = 1;
                        dt_log.Rows[0]["Msg"] = "已修改，未上报至省厅";
                        DAL.SaveTBData_SaveToStLog(dt_log);
                    }

                }

            }
            result.code = ProcessResult.数据保存成功;
            result.message = "数据保存成功！";
            return result;
        }

        /// <summary>
        /// 向无锡数据中心传送数据(TBContractRecordManage)
        /// </summary>
        /// <param name="xmlData">XML内容</param>
        /// <returns>处理结果信息</returns>
        public ProcessResultData SaveTBData_TBContractRecordManage(string user, DataTable dt_Data)
        {
            ProcessResultData result = new ProcessResultData();

            List<string> list_PrjNums = GetPrjNums_TBProjectInfo();
            if (list_PrjNums.Count == 0)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = "还没有立项项目信息！";
                return result;
            }
            List<string> list_ContractType = this.GetContractType();
            string[] fields = new string[] { "RecordName", "RecordInnerNum", "ContractNum", "ContractTypeNum", "ContractMoney", "ContractDate", "PropietorCorpName", "ContractorCorpName", "ContractorCorpCode", "PrjHead" };
            string msg = String.Empty;
            List<string> novalidates = new List<string>();
            novalidates.Add(String.Empty);
            novalidates.Add("无");
            novalidates.Add("/");

            //Regex reg_zzjgdm = new Regex(@"^\d{8}-[A-z0-9]$");

            foreach (DataRow item in dt_Data.Rows)
            {

                if (!list_PrjNums.Exists(p => p.Equals(item["PrjNum"].ToString2(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "立项项目编号不正确，编号为“" + item["PrjNum"] + "”的立项项目不存在！";
                    return result;
                }

                if (!list_ContractType.Exists(p => p.Equals(item["ContractTypeNum"].ToString2(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "ContractTypeNum不正确，值为“" + item["ContractTypeNum"] + "”未参照引用自表tbContractTypeDic！";
                    return result;
                }

                //if (!reg.IsMatch(item["PropietorCorpCode"].ToString2()))
                /** 勘察设计系统以前发包单位的组织机构代码是用户填写，有可能格式不准确或者为空，放开发包单位限制
                if ((item["PropietorCorpCode"].ToString2().Length != 10 || item["PropietorCorpCode"].ToString2().IndexOf('-') != 8) && item["PropietorCorpCode"].ToString2().Length != 18)
                {
                    if (item["PropietorCorpCode"].ToString2().Length == 9)
                    {
                        item["PropietorCorpCode"] = item["PropietorCorpCode"].ToString2().Substring(0, 8) + "-" + item["PropietorCorpCode"].ToString2().Substring(8, 1);
                    }
                    else
                    {
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = "PropietorCorpCode不合法,格式不正确，应该为“XXXXXXXX-X”格式！";
                        return result;
                    }
                }*/
                //if (!reg.IsMatch(item["ContractorCorpCode"].ToString2()))
                if ((item["ContractorCorpCode"].ToString2().Length != 10 || item["ContractorCorpCode"].ToString2().IndexOf('-') != 8) && item["ContractorCorpCode"].ToString2().Length != 18)
                {
                    if (item["ContractorCorpCode"].ToString2().Length == 9)
                    {
                        item["ContractorCorpCode"] = item["ContractorCorpCode"].ToString2().Substring(0, 8) + "-" + item["ContractorCorpCode"].ToString2().Substring(8, 1);
                    }
                    else
                    {
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = "ContractorCorpCode不合法,格式不正确，应该为“XXXXXXXX-X”格式！";
                        return result;
                    }
                }

                if (BLL.BLLCommon.DataFieldIsNullOrEmpty(novalidates, fields, item, out msg))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = msg + "不能为空！";
                    return result;
                }
            }

            DataTable dt;

            DataRow row;
            foreach (DataRow item in dt_Data.Rows)
            {
                int flag = 0;
                dt = DAL.GetTBData_TBContractRecordManage(item["RecordInnerNum"].ToString());

                if (dt.Rows.Count > 0)
                {
                    row = dt.Rows[0];
                    DataTableHelp.DataRow2DataRow(item, row, new List<string>() { "PKID", "RecordNum" });
                    flag++;
                }
                else
                {
                    row = dt.NewRow();
                    DataTableHelp.DataRow2DataRow(item, row);
                    row["RecordNum"] = this.GetMaxXmhjNumber_TBContractRecordManage(Xmhj.Htbawj, row["PrjNum"].ToString(), row["ContractTypeNum"].ToString());

                    row["PKID"] = Guid.NewGuid();
                    dt.Rows.Add(row);

                }
                row["xgrqsj"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (string.IsNullOrEmpty(row["CreateDate"].ToString()))
                {
                    row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                row["updateUser"] = user;

                row["Tag"] = Tag.无锡市勘察设计行业信息管理系统.ToString();

                if (!DAL.SaveTBData_TBContractRecordManage(dt))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "数据保存失败！";
                    return result;
                }

                //if (flag > 0)
                //{
                //    DataTable dt_log = DAL.GetTBData_SaveToStLog("TBContractRecordManage", dt.Rows[0]["PKID"].ToString());
                //    if (dt_log.Rows.Count > 0)
                //    {
                //        dt_log.Rows[0]["OperateState"] = 1;
                //        dt_log.Rows[0]["Msg"] = "已修改，未上报至省厅";
                //        DAL.SaveTBData_SaveToStLog(dt_log);
                //    }
                //}
            }

            result.code = ProcessResult.数据保存成功;
            result.message = "数据保存成功！";

            return result;
        }

        /// <summary>
        /// 向无锡数据中心传送数据(TBProjectCensorInfo)
        /// </summary>
        /// <param name="xmlData">XML内容</param>
        /// <returns>处理结果信息</returns>
        public ProcessResultData SaveTBData_TBProjectCensorInfo(string user, DataTable dt_Data)
        {
            ProcessResultData result = new ProcessResultData();
            List<string> list_PrjNums = GetPrjNums_TBProjectInfo();
            if (list_PrjNums.Count == 0)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = "还没有立项项目信息！";
                return result;
            }
            List<string> novalidates = new List<string>();
            novalidates.Add(String.Empty);
            novalidates.Add("无");
            novalidates.Add("/");
            //Regex reg_zzjgdm = new Regex(@"^\d{8}-[A-z0-9]$");

            string[] fields = new string[] { "CensorInnerNum", "CensorCorpName", "CensorCorpCode", "CensorEDate", "EconCorpName", "EconCorpCode", "DesignCorpName", "DesignCorpCode" };
            string msg = String.Empty;

            foreach (DataRow item in dt_Data.Rows)
            {

                if (!list_PrjNums.Exists(p => p.Equals(item["PrjNum"].ToString2(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "立项项目编号不正确，编号为“" + item["PrjNum"] + "”的立项项目不存在！";
                    return result;
                }

                //if (!reg_zzjgdm.IsMatch(item["EconCorpCode"].ToString2()))
                //{
                //    result.code = ProcessResult.保存失败和失败原因;
                //    result.message = string.Format("EconCorpCode不合法，值“{0}”，应该为“XXXXXXXX-X”格式！", item["EconCorpCode"].ToString2());
                //    return result;
                //}
                //if (!reg_zzjgdm.IsMatch(item["DesignCorpCode"].ToString2()))
                //{
                //    result.code = ProcessResult.保存失败和失败原因;
                //    result.message = string.Format("DesignCorpCode不合法，值“{0}”，应该为“XXXXXXXX-X”格式！", item["DesignCorpCode"].ToString2());
                //    return result;
                //}

                if (BLL.BLLCommon.DataFieldIsNullOrEmpty(novalidates, fields, item, out msg))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = msg + "不能为空！";
                    return result;
                }

            }

            DataTable dt;

            DataRow row;
            foreach (DataRow item in dt_Data.Rows)
            {
                int flag = 0;
                dt = DAL.GetTBData_TBProjectCensorInfo(item["CensorInnerNum"].ToString2());

                if (dt.Rows.Count > 0)
                {
                    row = dt.Rows[0];
                    DataTableHelp.DataRow2DataRow(item, row, new List<string>() { "PKID", "CensorNum" });
                    flag++;
                }
                else
                {
                    row = dt.NewRow();
                    DataTableHelp.DataRow2DataRow(item, row);
                    row["PKID"] = Guid.NewGuid();
                    row["CensorNum"] = this.GetMaxXmhjNumber_TBProjectCensorInfo(Xmhj.Sgtschgs, row["PrjNum"].ToString(), string.Empty);
                    dt.Rows.Add(row);

                }
                row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                row["updateUser"] = user;

                if (!DAL.SaveTBData_TBProjectCensorInfo(dt))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "数据保存失败！";
                    return result;
                }
                if (flag > 0)
                {
                    DataTable dt_log = DAL.GetTBData_SaveToStLog("TBProjectCensorInfo", dt.Rows[0]["PKID"].ToString());
                    if (dt_log.Rows.Count > 0)
                    {
                        dt_log.Rows[0]["OperateState"] = 1;
                        dt_log.Rows[0]["Msg"] = "已修改，未上报至省厅";
                        DAL.SaveTBData_SaveToStLog(dt_log);
                    }

                }

            }

            result.code = ProcessResult.数据保存成功;
            result.message = "数据保存成功！";
            return result;
        }

        /// <summary>
        /// 向无锡数据中心传送数据(TBProjectDesignEconUserInfo)
        /// </summary>
        /// <param name="xmlData">XML内容</param>
        /// <returns>处理结果信息</returns>
        public ProcessResultData SaveTBData_TBProjectDesignEconUserInfo(string user, DataTable dt_Data)
        {
            ProcessResultData result = new ProcessResultData();
            List<string> list_PrjNums = GetPrjNums_TBProjectInfo();
            List<string> list_CensorNums = GetCensorNums_TBProjectCensorInfo();

            if (list_PrjNums.Count == 0)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = "还没有立项项目信息！";
                return result;
            }
            List<string> IDCardTypeList = this.GetIDCardType();
            List<string> list_WorkDuty = this.GetWorkDuty();
            List<string> list_SpecialtyType = this.GetSpecialtyType();

            List<string> novalidates = new List<string>();
            novalidates.Add(String.Empty);
            novalidates.Add("无");
            novalidates.Add("/");
            Regex reg_zzjgdm = new Regex(@"^\d{8}-[A-z0-9]$");

            string censorNumString = String.Empty;
            string[] fields = new string[] { "PrjNum", "CensorNum", "CorpName", "UserName", "IDCardTypeNum", "PrjDuty" };// "CorpCode","IDCard",
            string msg = String.Empty;
            for (int i = 0; i < dt_Data.Rows.Count; i++)
            {

                if (!list_PrjNums.Exists(p => p.Equals(dt_Data.Rows[i]["PrjNum"].ToString2(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "立项项目编号不正确，编号为“" + dt_Data.Rows[i]["PrjNum"] + "”的立项项目不存在！";
                    return result;
                }

                //if (!reg_zzjgdm.IsMatch(item["CorpCode"].ToString2()))
                //if (item["CorpCode"].ToString2().Length != 10 || item["CorpCode"].ToString2().IndexOf('-') != 8)
                //{
                //    result.code = ProcessResult.保存失败和失败原因;
                //    result.message = string.Format("CorpCode不合法，值“{0}”，应该为“XXXXXXXX-X”格式！", item["CorpCode"].ToString2());
                //    return result;
                //}
                if (!IDCardTypeList.Exists(p => p.Equals(dt_Data.Rows[i]["IDCardTypeNum"].ToString2(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "IDCardTypeNum非法，值“" + dt_Data.Rows[i]["IDCardTypeNum"] + "”未参考引用自表tbIDCardTypeDic！";
                    return result;
                }
                //if (!list_SpecialtyType.Exists(p => p.Equals(item["SpecialtyTypNum"].ToString2())))
                //{
                //    result.code = ProcessResult.保存失败和失败原因;
                //    result.message = "SpecialtyTypNum非法，值“" + item["SpecialtyTypNum"] + "”未参考引用自表tbSpecialtyTypeDic！";
                //    return result;
                //}

                //if (item["IDCardTypeNum"].ToString2().Equals("1"))
                //{
                //    if (!Utilities.IsValidCardNo(item["IDCard"].ToString2()))
                //    {
                //        result.code = ProcessResult.保存失败和失败原因;
                //        result.message = "IDCard非法，值“" + item["IDCard"] + "”不是有效的身份证号码！";
                //        return result;
                //    }
                //}

                if (dt_Data.Rows[i]["IDCard"].ToString2().Equals("无数据", StringComparison.CurrentCultureIgnoreCase))
                {
                    dt_Data.Rows[i]["IDCard"] = "";
                }

                if (!list_WorkDuty.Exists(p => p.Equals(dt_Data.Rows[i]["PrjDuty"].ToString2(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "PrjDuty非法，值“" + dt_Data.Rows[i]["PrjDuty"] + "”未参考引用自表tbWorkDutyDic！";
                    return result;
                }

                if (BLL.BLLCommon.DataFieldIsNullOrEmpty(novalidates, fields, dt_Data.Rows[i], out msg))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = msg + "不能为空！";
                    return result;
                }

                DataTable dt_TBProjectCensorInfo = DAL.GetTBData_TBProjectCensorInfoByCensorInnerNum(dt_Data.Rows[i]["CensorNum"].ToString2());
                if (dt_TBProjectCensorInfo.Rows.Count == 0)
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "施工图审查合格书内部编号：" + dt_Data.Rows[i]["CensorNum"].ToString2() + "，对应的施工图审查合格书编号不存在，请先保存施工图审查信息，再保存其人员信息！";
                    return result;
                }
                dt_Data.Rows[i]["CensorNum"] = dt_TBProjectCensorInfo.Rows[0]["CensorNum"];

                if (censorNumString.IndexOf(dt_Data.Rows[i]["CensorNum"].ToString2()) < 0)
                {
                    censorNumString += string.Format("{0},", dt_Data.Rows[i]["CensorNum"]);
                }
            }

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                DAL.Delete_ProjectDesignEconUserInfo(censorNumString.Trim(','));
                DAL.Delete_SaveToStLog_ProjectDesignEconUserInfo(censorNumString.Trim(','));
                //DAL.Delete_ProjectDesignEconUserInfoByCensorInnerNum(censorNumString.Trim(','));
                //DAL.Delete_SaveToStLog_ProjectDesignEconUserInfoByCensorInnerNum(censorNumString.Trim(','));

                DataTable dt = DAL.GetSchema_TBProjectDesignEconUserInfo();
                DataRow row;
                foreach (DataRow item in dt_Data.Rows)
                {
                    row = dt.NewRow();
                    DataTableHelp.DataRow2DataRow(item, row);

                    row["PKID"] = Guid.NewGuid();
                    row["updateUser"] = user;

                    dt.Rows.Add(row);
                }

                if (dt_Data.Rows.Count > 0)
                {
                    if (DAL.SaveTBData_TBProjectDesignEconUserInfo(dt))
                    {
                        scope.Complete();

                        result.code = ProcessResult.数据保存成功;
                        result.message = "数据保存成功！";

                    }
                    else
                    {
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = "网络故障，数据保存失败！";

                    }

                }
                else
                {
                    result.code = ProcessResult.未找到对应项目;
                    result.message = "非法格式的数据！";
                }
            }
            return result;
        }

        /// <summary>
        /// 向无锡数据中心传送数据(TBBuilderLicenceManage)
        /// </summary>
        /// <param name="xmlData">XML内容</param>
        /// <returns>处理结果信息</returns>
        public ProcessResultData SaveTBData_TBBuilderLicenceManage(string user, DataTable dt_Data)
        {
            ProcessResultData result = new ProcessResultData();

            List<string> list_PrjNums = GetPrjNums_TBProjectInfo();
            if (list_PrjNums.Count == 0)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = "还没有立项项目信息！";
                return result;
            }
            List<string> novalidates = new List<string>();
            novalidates.Add(String.Empty);
            novalidates.Add("无");
            novalidates.Add("/");
            //Regex reg_zzjgdm = new Regex(@"^\d{8}-[A-z0-9]$");

            List<string> IDCardTypeList = this.GetIDCardType();


            string[] fields = new string[] { "BuilderLicenceName", "BuilderLicenceInnerNum", "RecordInnerNum", "PrjNum", "Area", "IssueCertDate", "EconCorpName", "EconCorpCode", "DesignCorpName", "DesignCorpCode", "ConsCorpName", "ConsCorpCode", "SuperCorpName", "SuperCorpCode", "ConstructorName", "CIDCardTypeNum", "ConstructorIDCard", "SupervisionName", "SIDCardTypeNum", "SupervisionIDCard" };
            string msg = String.Empty;
            foreach (DataRow item in dt_Data.Rows)
            {
                if (!list_PrjNums.Exists(p => p.Equals(item["PrjNum"].ToString2(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "立项项目编号不正确，编号为“" + item["PrjNum"] + "”的立项项目不存在！";
                    return result;
                }

                //if (!reg_zzjgdm.IsMatch(item["EconCorpCode"].ToString2()))
                if ((item["EconCorpCode"].ToString2().Length != 10 || item["EconCorpCode"].ToString2().IndexOf('-') != 8) && item["EconCorpCode"].ToString2().Length != 18)
                {
                    if (item["EconCorpCode"].ToString2().Length == 9)
                    {
                        item["EconCorpCode"] = item["EconCorpCode"].ToString2().Substring(0, 8) + "-" + item["EconCorpCode"].ToString2().Substring(8, 1);
                    }
                    else
                    {
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = string.Format("EconCorpCode不合法，值“{0}”，应该为“XXXXXXXX-X”格式！", item["EconCorpCode"].ToString2());
                        return result;
                    }
                }

                if ((item["DesignCorpCode"].ToString2().Length != 10 || item["DesignCorpCode"].ToString2().IndexOf('-') != 8) && item["DesignCorpCode"].ToString2().Length != 18)
                {
                    if (item["DesignCorpCode"].ToString2().Length == 9)
                    {
                        item["DesignCorpCode"] = item["DesignCorpCode"].ToString2().Substring(0, 8) + "-" + item["DesignCorpCode"].ToString2().Substring(8, 1);
                    }
                    else
                    {
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = string.Format("DesignCorpCode不合法，值“{0}”，应该为“XXXXXXXX-X”格式！", item["DesignCorpCode"].ToString2());
                        return result;
                    }
                }
                //if (!reg_zzjgdm.IsMatch(item["ConsCorpCode"].ToString2()))

                if ((item["ConsCorpCode"].ToString2().Length != 10 || item["ConsCorpCode"].ToString2().IndexOf('-') != 8) && item["ConsCorpCode"].ToString2().Length != 18)
                {
                    if (item["ConsCorpCode"].ToString2().Length == 9)
                    {
                        item["ConsCorpCode"] = item["ConsCorpCode"].ToString2().Substring(0, 8) + "-" + item["ConsCorpCode"].ToString2().Substring(8, 1);
                    }
                    else
                    {
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = string.Format("ConsCorpCode不合法，值“{0}”，应该为“XXXXXXXX-X”格式！", item["ConsCorpCode"].ToString2());
                        return result;
                    }
                }

                //if (!reg_zzjgdm.IsMatch(item["SuperCorpCode"].ToString2()))
                if ((item["SuperCorpCode"].ToString2().Length != 10 || item["SuperCorpCode"].ToString2().IndexOf('-') != 8) && item["SuperCorpCode"].ToString2().Length != 18)
                {
                    if (item["SuperCorpCode"].ToString2().Length == 9)
                    {
                        item["SuperCorpCode"] = item["SuperCorpCode"].ToString2().Substring(0, 8) + "-" + item["SuperCorpCode"].ToString2().Substring(8, 1);
                    }
                    else
                    {
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = string.Format("SuperCorpCode不合法，值“{0}”，应该为“XXXXXXXX-X”格式！", item["SuperCorpCode"].ToString2());
                        return result;
                    }
                }


                if (!IDCardTypeList.Exists(p => p.Equals(item["CIDCardTypeNum"].ToString2(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = string.Format("CIDCardTypeNum不合法，值“{0}”，未参考引用到表tbIDCardTypeDic！", item["CIDCardTypeNum"].ToString2());
                    return result;
                }

                if (item["CIDCardTypeNum"].ToString2().Equals("1", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (!Utilities.IsValidCardNo(item["ConstructorIDCard"].ToString2()))
                    {
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = "ConstructorIDCard非法，值“" + item["ConstructorIDCard"] + "”不是有效的身份证号码！";
                        return result;
                    }
                }

                if (!IDCardTypeList.Exists(p => p.Equals(item["SIDCardTypeNum"].ToString2(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = string.Format("SIDCardTypeNum不合法，值“{0}”，未参考引用到表tbIDCardTypeDic！", item["SIDCardTypeNum"].ToString2());
                    return result;
                }
                if (item["SIDCardTypeNum"].ToString2().Equals("1", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (!Utilities.IsValidCardNo(item["SupervisionIDCard"].ToString2()))
                    {
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = "SupervisionIDCard非法，值“" + item["SupervisionIDCard"] + "”不是有效的身份证号码！";
                        return result;
                    }
                }


                if (BLL.BLLCommon.DataFieldIsNullOrEmpty(novalidates, fields, item, out msg))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = msg + "不能为空！";
                    return result;
                }

            }

            DataTable dt;
            DataRow row;
            try
            {
                foreach (DataRow item in dt_Data.Rows)
                {
                    int flag = 0;
                    dt = DAL.GetTBData_TBBuilderLicenceManage(item["BuilderLicenceInnerNum"].ToString2());

                    if (dt.Rows.Count > 0)
                    {
                        row = dt.Rows[0];
                        DataTableHelp.DataRow2DataRow(item, row, new List<string>() { "PKID", "BuilderLicenceNum" });
                        flag++;
                    }
                    else
                    {
                        row = dt.NewRow();
                        DataTableHelp.DataRow2DataRow(item, row);

                        row["PKID"] = Guid.NewGuid();
                        row["BuilderLicenceNum"] = this.GetMaxXmhjNumber_TBBuilderLicenceManage(Xmhj.Sgxkz, row["PrjNum"].ToString(), string.Empty);
                        dt.Rows.Add(row);

                    }
                    row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    row["updateUser"] = user;

                    if (!DAL.SaveTBData_TBBuilderLicenceManage(dt))
                    {
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = "网络故障，数据保存失败！";
                    }
                    if (flag > 0)
                    {
                        DataTable dt_log = DAL.GetTBData_SaveToStLog("TBBuilderLicenceManage", dt.Rows[0]["PKID"].ToString());
                        if (dt_log.Rows.Count > 0)
                        {
                            dt_log.Rows[0]["OperateState"] = 1;
                            dt_log.Rows[0]["Msg"] = "已修改，未上报至省厅";
                            DAL.SaveTBData_SaveToStLog(dt_log);
                        }

                    }
                }
                result.code = ProcessResult.数据保存成功;
                result.message = "数据保存成功！";
            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = "数据保存失败，异常：" + ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 向无锡数据中心传送数据(TBProjectBuilderUserInfo)
        /// </summary>
        /// <param name="xmlData">XML内容</param>
        /// <returns>处理结果信息</returns>
        public ProcessResultData SaveTBData_TBProjectBuilderUserInfo(string user, DataTable dt_Data)
        {
            ProcessResultData result = new ProcessResultData();

            List<string> list_PrjNums = GetPrjNums_TBProjectInfo();
            if (list_PrjNums.Count == 0)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = "还没有立项项目信息！";
                return result;
            }
            StringBuilder str_pkid = new StringBuilder();
            List<string> novalidates = new List<string>();
            novalidates.Add(String.Empty);
            novalidates.Add("无");
            novalidates.Add("/");
            //Regex reg_zzjgdm = new Regex(@"^\d{8}-[A-z0-9]$");

            List<string> IDCardTypeList = this.GetIDCardType();

            string[] fields = new string[] { "PKID", "PrjNum", "aqjdbm", "CorpName", "CorpCode", "UserName", "IDCardTypeNum", "IDCard", "UserType" };
            string msg = String.Empty;
            List<string> list_UserType = new List<string>() { "1", "2", "3" };

            try
            {
                foreach (DataRow item in dt_Data.Rows)
                {
                    if (!list_PrjNums.Exists(p => p.Equals(item["PrjNum"].ToString2(), StringComparison.CurrentCultureIgnoreCase)))
                    {
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = "立项项目编号不正确，编号为“" + item["PrjNum"] + "”的立项项目不存在！";
                        return result;
                    }

                    //if (!reg_zzjgdm.IsMatch(item["CorpCode"].ToString2()))
                    if ((item["CorpCode"].ToString2().Length != 10 || item["CorpCode"].ToString2().IndexOf('-') != 8) && item["CorpCode"].ToString2().Length != 18)
                    {
                        if (item["CorpCode"].ToString2().Length == 9)
                        {
                            item["CorpCode"] = item["CorpCode"].ToString2().Substring(0, 8) + "-" + item["CorpCode"].ToString2().Substring(8, 1);
                        }
                        else
                        {
                            result.code = ProcessResult.保存失败和失败原因;
                            result.message = string.Format("CorpCode不合法，值“{0}”，应该为“XXXXXXXX-X”格式！", item["CorpCode"].ToString2());
                            return result;
                        }
                    }

                    if (!IDCardTypeList.Exists(p => p.Equals(item["IDCardTypeNum"].ToString2(), StringComparison.CurrentCultureIgnoreCase)))
                    {
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = string.Format("IDCardTypeNum不合法，值“{0}”，未参考引用到表tbIDCardTypeDic！", item["IDCardTypeNum"].ToString2());
                        return result;
                    }

                    if (item["IDCardTypeNum"].ToString2().Equals("1", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (!Utilities.IsValidCardNo(item["IDCard"].ToString2()))
                        {
                            result.code = ProcessResult.保存失败和失败原因;
                            result.message = "IDCard非法，值“" + item["IDCard"] + "”不是有效的身份证号码！";
                            return result;
                        }
                    }

                    if (!list_UserType.Exists(p => p.Equals(item["UserType"].ToString2(), StringComparison.CurrentCultureIgnoreCase)))
                    {
                        result.message = "UserType不合法，只能取1(主要负责人)，2(项目负责人),3(安全员)三个值！";
                        return result;
                    }


                    if (BLL.BLLCommon.DataFieldIsNullOrEmpty(novalidates, fields, item, out msg))
                    {
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = msg + "不能为空！";
                        return result;
                    }

                    //if (BLLCommon.DataFieldIsNullOrEmpty(item["aqjdbm"]))
                    //{
                    //    result.code = ProcessResult.保存失败和失败原因;
                    //    result.message = "安全监督编码不能为空！";
                    //    return result;
                    //}

                    //if (BLLCommon.DataFieldIsNullOrEmpty(item["PrjNum"]))
                    //{
                    //    result.code = ProcessResult.保存失败和失败原因;
                    //    result.message = "立项项目编号不能为空！";
                    //    return result;
                    //}

                    //if (aqjdbmString.IndexOf(item["aqjdbm"].ToString2()) < 0)
                    //{
                    //    aqjdbmString += string.Format("{0},", item["aqjdbm"]);
                    //}
                    str_pkid.AppendFormat("{0},", item["PKID"]);
                }
                if (str_pkid.Length > 0)
                    str_pkid.Remove(str_pkid.Length - 1, 1);

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    //DAL.Delete_ProjectBuilderUserInfo(aqjdbmString.Trim(','));
                    //DAL.Delete_SaveToStLog_ProjectBuilderUserInfo(aqjdbmString.Trim(','));

                    //DataTable dt = DAL.GetSchema_TBProjectBuilderUserInfo();
                    DataTable dt = DAL.GetTBData_TBProjectBuilderUserInfoByPKIDs(str_pkid.ToString());

                    DataRow row;
                    foreach (DataRow item in dt_Data.Rows)
                    {
                        int rowIndex = -1;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["PKID"].ToString().Equals(item["PKID"].ToString(), StringComparison.CurrentCultureIgnoreCase))
                            {
                                rowIndex = i;
                                break;
                            }
                        }

                        if (rowIndex >= 0)
                        {
                            row = dt.Rows[rowIndex];
                            DataTableHelp.DataRow2DataRow(item, row, new List<string>() { "PKID" });
                        }
                        else
                        {
                            row = dt.NewRow();
                            dt.Rows.Add(row);
                            DataTableHelp.DataRow2DataRow(item, row);
                        }

                        DataTable dt_BuilderLicenceNum = DAL.GetBuilderLicenceNumByAqjdbm(item["aqjdbm"].ToString2());
                        if (dt_BuilderLicenceNum.Rows.Count > 0)
                        {
                            if (dt_BuilderLicenceNum.Rows[0]["BuilderLicenceNum2"].ToString2().IndexOf("-SX-") >= 0)
                                row["BuilderLicenceNum"] = dt_BuilderLicenceNum.Rows[0]["BuilderLicenceNum2"];
                            else
                                row["BuilderLicenceNum"] = dt_BuilderLicenceNum.Rows[0]["BuilderLicenceNum"];
                        }
                        row["updateUser"] = user;
                    }
                    if (dt_Data.Rows.Count > 0)
                    {
                        if (DAL.SaveTBData_TBProjectBuilderUserInfo(dt))
                        {
                            if (str_pkid.Length > 0)
                            {
                                DAL.UpdateSaveToStLog("TBProjectBuilderUserInfo", str_pkid.ToString());
                            }
                            scope.Complete();
                            result.code = ProcessResult.数据保存成功;
                            result.message = "数据保存成功！";
                        }
                        else
                        {
                            result.code = ProcessResult.保存失败和失败原因;
                            result.message = "网络故障，数据保存失败！";
                        }
                    }
                    else
                    {
                        result.code = ProcessResult.未找到对应项目;
                        result.message = "非法格式的数据！";
                    }
                }
            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = "数据保存失败，异常：" + ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 向无锡数据中心传送数据(TBProjectFinishManage)
        /// </summary>
        /// <param name="xmlData">XML内容</param>
        /// <returns>处理结果信息</returns>
        public ProcessResultData SaveTBData_TBProjectFinishManage(string user, DataTable dt_Data)
        {
            ProcessResultData result = new ProcessResultData();

            List<string> list_PrjNums = GetPrjNums_TBProjectInfo();
            if (list_PrjNums.Count == 0)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = "还没有立项项目信息！";
                return result;
            }
            List<string> novalidates = new List<string>();
            novalidates.Add(String.Empty);
            novalidates.Add("无");
            novalidates.Add("/");

            string[] fields = new string[] { "PrjFinishName", "PrjFinishInnerNum", "PrjNum", "BuilderLicenceNum", "FactCost", "FactArea", "BDate", "EDate" };
            string msg = String.Empty;
            foreach (DataRow item in dt_Data.Rows)
            {

                //if (BLLCommon.DataFieldIsNullOrEmpty(item["PrjFinishInnerNum"]))
                //{
                //    result.code = ProcessResult.保存失败和失败原因;
                //    result.message = "竣工备案内部编号不能为空！";
                //    return result;
                //}

                if (!list_PrjNums.Exists(p => p.Equals(item["PrjNum"].ToString2(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "立项项目编号不正确，编号为“" + item["PrjNum"] + "”的立项项目不存在！";
                    return result;
                }

                if (BLL.BLLCommon.DataFieldIsNullOrEmpty(novalidates, fields, item, out msg))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = msg + "不能为空！";
                    return result;
                }

                if (!DAL.IsExistsSgxkxx(item["BuilderLicenceNum"].ToString2()))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = string.Format("施工许可证号非法，施工许可证号为“{0}”的施工许可信息不存在！", item["BuilderLicenceNum"].ToString2());
                    return result;
                }

            }

            DataTable dt;
            DataRow row;

            foreach (DataRow item in dt_Data.Rows)
            {
                int flag = 0;
                dt = DAL.GetTBData_TBProjectFinishManage(item["PrjFinishInnerNum"].ToString2());

                if (dt.Rows.Count > 0)
                {
                    row = dt.Rows[0];
                    DataTableHelp.DataRow2DataRow(item, row, new List<string>() { "PKID", "PrjFinishNum" });
                    flag++;
                }
                else
                {
                    row = dt.NewRow();
                    DataTableHelp.DataRow2DataRow(item, row);

                    row["PKID"] = Guid.NewGuid();
                    row["PrjFinishNum"] = this.GetMaxXmhjNumber_TBProjectFinishManage(Xmhj.Jgysba, row["PrjNum"].ToString(), string.Empty);

                    dt.Rows.Add(row);

                }
                row["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                row["updateUser"] = user;

                if (DAL.SaveTBData_TBProjectFinishManage(dt))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "数据保存失败！";

                }
                if (flag > 0)
                {
                    DataTable dt_log = DAL.GetTBData_SaveToStLog("TBProjectFinishManage", dt.Rows[0]["PKID"].ToString());
                    if (dt_log.Rows.Count > 0)
                    {
                        dt_log.Rows[0]["OperateState"] = 1;
                        dt_log.Rows[0]["Msg"] = "已修改，未上报至省厅";
                        DAL.SaveTBData_SaveToStLog(dt_log);
                    }

                }

            }

            result.code = ProcessResult.数据保存成功;
            result.message = "数据保存成功！";

            return result;
        }

        /// <summary>
        /// 向无锡数据中心传送(质监、安监)数据,并上报到省厅
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dt_Data"></param>
        /// <returns></returns>
        public ProcessResultData SaveTBData_aj_gcjbxx(string user, DataTable dt_Data)
        {
            #region  数据检查
            ProcessResultData result = new ProcessResultData();
            List<string> list_PrjNums = GetPrjNums_TBProjectInfo();
            if (list_PrjNums.Count == 0)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = "还没有立项项目信息！";
                return result;
            }
            List<string> list_StructType = this.GetStructureType();

            List<string> novalidates = new List<string>();
            novalidates.Add(String.Empty);
            novalidates.Add("无");
            novalidates.Add("/");
            //Regex reg_zzjgdm = new Regex(@"^\d{8}-[A-z0-9]$");

            string[] fields = new string[] { "pKID", "aqjdbm", "gcmc", "xmbm", "gcgkYszj", "gcgkJzmj", "bjrq", "gcgkKgrq", "gcgkJhjgrq", "zbdwDwdm", "zbdwDwmc", "zbdwAqxkzh", "zbdwZcjzs", "zbdwZcjzsdm", "zbdwZcjzslxdh", "zbdwAqy1", "zbdwAqyzh1", "createDate", "updateFlag", "sbdqbm" };
            string msg = String.Empty;
            StringBuilder str = new StringBuilder();
            foreach (DataRow item in dt_Data.Rows)
            {

                //if (BLLCommon.DataFieldIsNullOrEmpty(item["aqjdbm"]))
                //{
                //    result.code = ProcessResult.保存失败和失败原因;
                //    result.message = "安全监督编码不能为空！";
                //    return result;
                //}
                if (!list_PrjNums.Exists(p => p.Equals(item["xmbm"].ToString2(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "立项项目编号不正确，编号为“" + item["xmbm"] + "”的立项项目不存在！";
                    return result;
                }

                if (BLL.BLLCommon.DataFieldIsNullOrEmpty(novalidates, fields, item, out msg))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = msg + "不能为空！";
                    return result;
                }

                //if (!reg_zzjgdm.IsMatch(item["zbdw_dwdm"].ToString2()))
                if (item["zbdwDwdm"].ToString2().Length != 10 && item["zbdwDwdm"].ToString2().Length != 18)
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = string.Format("zbdwDwdm不合法，值“{0}”，应该为“XXXXXXXX-X”格式！", item["zbdwDwdm"].ToString2());
                    return result;
                }

                //if (!Utilities.IsValidCardNo(item["zbdwZcjzsdm"].ToString2()))
                //{
                //    result.code = ProcessResult.保存失败和失败原因;
                //    result.message = string.Format("zbdwZcjzsdm不合法，值“{0}”不是有效的身份证号码！", item["zbdwZcjzsdm"].ToString2());
                //    return result;
                //}


                if (!list_StructType.Exists(p => p.Equals(item["gcgkJglx"].ToString2(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "gcgkJglx非法，值“" + item["gcgkJglx"] + "”未参考引用自表tbWorkDutyDic！";
                    return result;
                }
                str.AppendFormat("{0},", item["pKID"].ToString2());

            }
            if (str.Length > 0)
            {
                str.Remove(str.Length - 1, 1);
            }

            #endregion

            DataTable dt = DAL.GetTBData_aj_gcjbxxByPKIDs(str.ToString().Trim(','));
            DataRow row;

            try
            {
                foreach (DataRow item in dt_Data.Rows)
                {
                    int flag = 0;
                    int rowIndex = -1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["pKID"].ToString2().Equals(item["pKID"].ToString(), StringComparison.CurrentCultureIgnoreCase))
                        {
                            flag = 1;
                            rowIndex = i;
                            break;
                        }
                    }

                    if (flag == 1)
                    {
                        row = dt.Rows[rowIndex];
                        DataTableHelp.DataRow2DataRow(item, row, new List<string>() { "pKID" });
                    }
                    else
                    {
                        row = dt.NewRow();
                        DataTableHelp.DataRow2DataRow(item, row);
                        //if (row["pKID"] == DBNull.Value || string.IsNullOrEmpty(row["pKID"].ToString2()))
                        //    row["pKID"] = Guid.NewGuid().ToString();
                        dt.Rows.Add(row);
                    }
                    row["updateUser"] = user;
                }

                if (dt.Rows.Count > 0)
                {
                    if (DAL.SaveTBData_aj_gcjbxx(dt))
                    {
                        if (str.Length > 0)
                        {
                            DAL.UpdateSaveToStLog("aj_gcjbxx", str.ToString());
                        }
                        result.code = ProcessResult.数据保存成功;
                        result.message = "数据保存成功！";
                    }
                    else
                    {
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = "网络故障，数据保存失败！";
                    }
                }
                else
                {
                    result.code = ProcessResult.未找到对应项目;
                    result.message = "非法格式的数据！";
                }
            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = "数据保存失败,异常：" + ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 向无锡数据中心传送数据(zj_gcjbxx)
        /// </summary>
        /// <param name="xmlData">XML内容</param>
        /// <returns>处理结果信息</returns>
        public ProcessResultData SaveTBData_zj_gcjbxx(string user, DataTable dt_Data)
        {
            ProcessResultData result = new ProcessResultData();
            List<string> list_PrjNums = GetPrjNums_TBProjectInfo();
            if (list_PrjNums.Count == 0)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = "还没有立项项目信息！";
                return result;
            }
            List<string> novalidates = new List<string>();
            novalidates.Add(String.Empty);
            novalidates.Add("无");
            novalidates.Add("/");
            //Regex reg_zzjgdm = new Regex(@"^\d{8}-[A-z0-9]$");

            StringBuilder str = new StringBuilder();
            string[] fields = new string[] { "pKID", "zljdbm", "gcmc", "prjNum", "gczj", "sbrq", "kgrq", "jhjgrq", "createDate", "updateFlag", "sbdqbm" };
            string msg = String.Empty;
            try
            {
                foreach (DataRow item in dt_Data.Rows)
                {
                    if (!list_PrjNums.Exists(p => p.Equals(item["prjNum"].ToString2(), StringComparison.CurrentCultureIgnoreCase)))
                    {
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = "立项项目编号不正确，编号为“" + item["prjNum"] + "”的立项项目不存在！";
                        return result;
                    }

                    if (BLL.BLLCommon.DataFieldIsNullOrEmpty(novalidates, fields, item, out msg))
                    {
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = msg + "不能为空！";
                        return result;
                    }
                    if (!item["updateFlag"].ToString2().Equals("U") && !item["updateFlag"].ToString2().Equals("D"))
                    {
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = "UpdateFlag字段取值不正确，只能为：“U”或“D”！";
                        return result;
                    }

                    str.AppendFormat("{0},", item["pKID"].ToString2());
                }
                if (str.Length > 0)
                    str.Remove(str.Length - 1, 1);

                DataTable dt = DAL.GetTBData_zj_gcjbxxByPKIDs(str.ToString());
                DataRow row;
                foreach (DataRow item in dt_Data.Rows)
                {
                    int flag = 0;
                    int rowIndex = -1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["pKID"].ToString2().Equals(item["pKID"].ToString2(), StringComparison.CurrentCultureIgnoreCase))
                        {
                            flag = 1;
                            rowIndex = i;
                            break;
                        }
                    }

                    if (flag == 1)
                    {
                        row = dt.Rows[rowIndex];
                        DataTableHelp.DataRow2DataRow(item, row, new List<string>() { "pKID" });
                    }
                    else
                    {
                        row = dt.NewRow();
                        DataTableHelp.DataRow2DataRow(item, row);
                        dt.Rows.Add(row);
                    }
                    row["updateUser"] = user;
                    //if (isSdSx)
                    //    row["sbdqbm"] = user;
                }
                if (dt_Data.Rows.Count > 0)
                {
                    if (DAL.SaveTBData_zj_gcjbxx(dt))
                    {
                        if (str.Length > 0)
                        {
                            DAL.UpdateSaveToStLog("zj_gcjbxx", str.ToString());
                        }

                        result.code = ProcessResult.数据保存成功;
                        result.message = "数据保存成功！";
                    }
                    else
                    {
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = "网络故障，数据保存失败！";
                    }
                }
                else
                {
                    result.code = ProcessResult.未找到对应项目;
                    result.message = "非法格式的数据！";
                }
            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = "数据保存失败,异常：" + ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 向无锡数据中心传送数据(zj_gcjbxx_zrdw)
        /// </summary>
        /// <param name="xmlData">XML内容</param>
        /// <returns>处理结果信息</returns>
        public ProcessResultData SaveTBData_zj_gcjbxx_zrdw(string user, DataTable dt_Data)
        {
            ProcessResultData result = new ProcessResultData();
            //string zljdbmString = String.Empty;
            string[] fields = new string[] { "pKID", "zljdbm", "dwlx", "xh", "dwmc", "dwdm", "xmfzrxm", "xmfzrdm" };
            string msg = String.Empty;
            List<string> list_dwlx = new List<string>() { "建设单位", "勘察单位", "设计单位", "施工单位", "监理单位", "质量检测机构", "混凝土供应商" };
            //建设单位传1勘察单位传2设计单位传3监理单位传4施工单位传5
            //List<string> list_xh = new List<string>() { "1", "2", "3", "4", "5" };

            List<string> novalidates = new List<string>();
            novalidates.Add(String.Empty);
            novalidates.Add("无");
            novalidates.Add("/");
            //Regex reg_zzjgdm = new Regex(@"^\d{8}-[A-z0-9]$");

            foreach (DataRow item in dt_Data.Rows)
            {
                if (BLL.BLLCommon.DataFieldIsNullOrEmpty(novalidates, fields, item, out msg))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = msg + "不能为空！";
                    return result;
                }

                if (!list_dwlx.Exists(p => p.Equals(item["dwlx"].ToString2(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = string.Format("dwlx的取值“{0}”不合法，应为“建设单位、勘察单位、设计单位、施工单位、监理单位、质量检测机构、混凝土供应商”里的其中之一！", item["dwlx"].ToString2());
                    return result;
                }

                //if (!list_xh.Exists(p => p.Equals(item["xh"].ToString2(), StringComparison.CurrentCultureIgnoreCase)))
                //{
                //    result.code = ProcessResult.保存失败和失败原因;
                //    result.message = string.Format("xh的取值“{0}”不合法，应为“1、2、3、4、5”里的其中之一！", item["xh"].ToString2());
                //    return result;
                //}

                //if (!reg_zzjgdm.IsMatch(item["dwdm"].ToString2()))
                if (item["dwdm"].ToString2().Length != 10 && item["dwdm"].ToString2().Length != 18)
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = string.Format("dwdm不合法，值“{0}”，应该为“XXXXXXXX-X”格式！", item["dwdm"].ToString2());
                    return result;
                }

                //if (!Utilities.IsValidCardNo(item["xmfzrdm"].ToString2()))
                //{
                //    result.code = ProcessResult.保存失败和失败原因;
                //    result.message = "xmfzrdm非法，值“" + item["xmfzrdm"] + "”不是有效的身份证号码！";
                //    return result;
                //}

                //if (item["zljdbm"] != DBNull.Value && item["zljdbm"] != null && zljdbmString.IndexOf(item["zljdbm"].ToString2()) < 0)
                //{
                //    zljdbmString += string.Format("{0},", item["zljdbm"]);
                //}

                DataTable dt_zj_gcjbxx = DAL.Read_zj_gcjbxxByPKID(item["pKID"].ToString2());
                if (dt_zj_gcjbxx.Rows.Count == 0)
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "字段pKID,值“" + item["pKID"].ToString2() + "”对应的质量监督信息不存在！";
                    return result;
                }
            }
            int flag = 0;
            DataRow row;

            try
            {
                foreach (DataRow item in dt_Data.Rows)
                {
                    DataTable dt = DAL.Read_zj_gcjbxx_zrdw(item["pKID"].ToString2(), item["xh"].ToString2());
                    if (dt.Rows.Count == 0)
                    {
                        row = dt.NewRow();
                        dt.Rows.Add(row);
                        DataTableHelp.DataRow2DataRow(item, row);
                    }
                    else
                    {
                        row = dt.Rows[0];
                        DataTableHelp.DataRow2DataRow(item, row, new List<string>() { "pKID", "xh" });
                    }

                    row["updateUser"] = user;


                    if (DAL.SaveTBData_zj_gcjbxx_zrdw(dt))
                    {
                        flag++;

                        DAL.UpdateSaveToStLog("zj_gcjbxx_zrdw", item["pKID"].ToString2() + "-" + item["xh"].ToString2());

                    }
                    else
                    {
                        result.message += string.Format("pKID:{0},xh:{1}、", item["pKID"].ToString2(), item["xh"].ToString2());
                    }
                }

                if (string.IsNullOrEmpty(result.message))
                {
                    result.code = ProcessResult.数据保存成功;
                    result.message = "数据保存成功！";
                }
                else
                {
                    if (flag > 0)
                    {
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = result.message.Trim('、') + "等信息保存失败，其它保存成功！";
                    }
                    else
                    {
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = "数据保存失败！";
                    }
                }
            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = "数据保存失败,发生异常：" + ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 向无锡数据中心传送数据(aj_zj_sgxk_relation)
        /// </summary>
        /// <param name="dt_Data"></param>
        /// <returns></returns>
        public ProcessResultData SaveTBData_aj_zj_sgxk_relation(string user, DataTable dt_Data)
        {
            ProcessResultData result = new ProcessResultData();
            string aqjdbmString = String.Empty;
            string[] fields = new string[] { "aqjdbm", "BuilderLicenceNum" };
            string msg = String.Empty;
            List<string> novalidates = new List<string>();
            novalidates.Add(String.Empty);
            novalidates.Add("无");
            novalidates.Add("/");

            foreach (DataRow item in dt_Data.Rows)
            {
                if (BLL.BLLCommon.DataFieldIsNullOrEmpty(novalidates, fields, item, out msg))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = msg + "不能为空！";
                    return result;
                }

                //if (  BLLCommon.DataFieldIsNullOrEmpty(item["aqjdbm"]) 
                //    ||BLLCommon.DataFieldIsNullOrEmpty(item["BuilderLicenceNum"])  
                //    ||BLLCommon.DataFieldIsNullOrEmpty(item["zljdbm"]) 
                //   ) 
                //{
                //    result.code = ProcessResult.保存失败和失败原因;
                //    result.message = "安全监督编码、质量监督编码、施工许可证编码不能为空！";
                //    return result;
                //}

                if (aqjdbmString.IndexOf(item["aqjdbm"].ToString2()) < 0)
                {
                    aqjdbmString += string.Format("{0},", item["aqjdbm"]);
                }
            }

            DataTable dt = DAL.GetTBData_aj_zj_sgxk_relation(aqjdbmString.Trim(','));
            DataRow row;
            foreach (DataRow item in dt_Data.Rows)
            {
                int flag = 0;
                int rowIndex = -1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["aqjdbm"].ToString2().Equals(item["aqjdbm"].ToString2(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        flag = 1;
                        rowIndex = i;
                        break;
                    }
                }

                if (flag == 1)
                {
                    row = dt.Rows[rowIndex];
                    DataTableHelp.DataRow2DataRow(item, row, new List<string>() { "PKID", "aqjdbm" });
                }
                else
                {
                    row = dt.NewRow();

                    DataTableHelp.DataRow2DataRow(item, row);
                    row["PKID"] = Guid.NewGuid();
                    dt.Rows.Add(row);
                }
                row["updateUser"] = user;

            }
            if (dt_Data.Rows.Count > 0)
            {
                if (DAL.SaveTBData_aj_zj_sgxk_relation(dt))
                {
                    DAL.Update_TBProjectBuilderUserInfo_Num();

                    result.code = ProcessResult.数据保存成功;
                    result.message = "数据保存成功！";

                }
                else
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "网络故障，数据保存失败！";

                }
            }
            else
            {
                result.code = ProcessResult.未找到对应项目;
                result.message = "非法格式的数据！";
            }
            return result;
        }


        public string GetXmHjCode(Xmhj xmhj, string xmlx)
        {
            string zblxCode = "";

            switch (xmhj)
            {
                case Xmhj.Zbtzs:
                    switch (xmlx)
                    {
                        case "001"://勘察
                            zblxCode = "BB";
                            break;
                        case "002"://设计
                            zblxCode = "BA";
                            break;
                        case "003"://施工
                            zblxCode = "BD";
                            break;
                        case "004"://监理
                            zblxCode = "BE";
                            break;
                        case "005"://设计施工一体化
                            zblxCode = "BC";
                            break;
                        case "006"://施工总承包
                            zblxCode = "BG";
                            break;
                        case "007"://项目管理
                            zblxCode = "BM";
                            break;
                        default:
                            break;
                    }
                    break;
                case Xmhj.Htbawj:
                    switch (xmlx)
                    {
                        case "100"://勘察
                            zblxCode = "HB";
                            break;
                        case "200"://设计
                            zblxCode = "HA";
                            break;
                        case "301"://施工总包
                            zblxCode = "HZ";
                            break;
                        case "302"://施工分包
                            zblxCode = "HF";
                            break;
                        case "303"://施工劳务
                            zblxCode = "HL";
                            break;
                        case "400"://监理
                            zblxCode = "HE";
                            break;
                        case "500"://设计施工一体化
                            zblxCode = "HC";
                            break;
                        case "600"://项目总承包
                            zblxCode = "HG";
                            break;
                        case "700"://项目管理
                            zblxCode = "HM";
                            break;
                        default:
                            break;
                    }
                    break;
                case Xmhj.Sgtschgs:
                    zblxCode = "TX";
                    break;
                case Xmhj.Sgxkz:
                    zblxCode = "SX";
                    break;
                case Xmhj.Jgysba:
                    zblxCode = "JX";
                    break;
                default:
                    break;
            }

            return zblxCode;
        }

        public string GetMaxXmhjNumber_TBTenderInfo(Xmhj xmhj, string prjNum, string xmlx)
        {
            string code = GetXmHjCode(xmhj, xmlx);

            string xmhjNumber = prjNum + "-" + code + "-" + DAL.GetMaxXmhjNumber_TBTenderInfo(prjNum, code);
            return xmhjNumber;
        }

        public string GetMaxXmhjNumber_TBContractRecordManage(Xmhj xmhj, string prjNum, string xmlx)
        {
            string code = GetXmHjCode(xmhj, xmlx);

            string xmhjNumber = prjNum + "-" + code + "-" + DAL.GetMaxXmhjNumber_TBContractRecordManage(prjNum, code);
            return xmhjNumber;
        }

        public string GetMaxXmhjNumber_TBBuilderLicenceManage(Xmhj xmhj, string prjNum, string xmlx)
        {
            string code = GetXmHjCode(xmhj, xmlx);

            string xmhjNumber = prjNum + "-" + code + "-" + DAL.GetMaxXmhjNumber_TBProjectCensorInfo(prjNum, code);
            return xmhjNumber;
        }

        public string GetMaxXmhjNumber_TBProjectCensorInfo(Xmhj xmhj, string prjNum, string xmlx)
        {
            string code = GetXmHjCode(xmhj, xmlx);

            string xmhjNumber = prjNum + "-" + code + "-" + DAL.GetMaxXmhjNumber_TBProjectCensorInfo(prjNum, code);
            return xmhjNumber;
        }
        public string GetMaxXmhjNumber_TBProjectFinishManage(Xmhj xmhj, string prjNum, string xmlx)
        {
            string code = GetXmHjCode(xmhj, xmlx);

            string xmhjNumber = prjNum + "-" + code + "-" + DAL.GetMaxXmhjNumber_TBProjectFinishManage(prjNum, code);
            return xmhjNumber;
        }
        /// <summary>
        /// 保存信用考评
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dt_Data"></param>
        /// <returns></returns>
        public ProcessResultData SaveData_QyXmkp(string user, DataTable dt_Data)
        {
            ProcessResultData result = new ProcessResultData();
            StringBuilder ids = new StringBuilder();
            string msg = String.Empty;
            string[] fields = new string[] { "qymc", "zzjgdm", "zzlb", "qysd", "kpnd" };

            //List<string> PrjTypes = this.GetPrjType();

            List<string> novalidates = new List<string>();
            novalidates.Add(String.Empty);
            novalidates.Add(" ");
            novalidates.Add("无");
            novalidates.Add("无数据");
            novalidates.Add("/");

            foreach (DataRow item in dt_Data.Rows)
            {
                if (BLLCommon.DataFieldIsNullOrEmpty(novalidates, fields, item, out msg))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = msg + "不能为空！";
                    return result;
                }

                //if (!PrjTypes.Exists(p => p.Equals(item["PrjTypeNum"].ToString2(), StringComparison.CurrentCultureIgnoreCase)))
                //{
                //    result.code = ProcessResult.保存失败和失败原因;
                //    result.message = "PrjTypeNum不合法，引用自tbPrjTypeDic表！";
                //    return result;
                //}

                if ((item["zzjgdm"].ToString2().Length != 10 || item["zzjgdm"].ToString2().IndexOf('-') != 8) && item["zzjgdm"].ToString2().Length != 18)
                {
                    if (item["zzjgdm"].ToString2().Length == 9)
                    {
                        item["zzjgdm"] = item["zzjgdm"].ToString2().Substring(0, 8) + "-" + item["zzjgdm"].ToString2().Substring(8, 1);
                    }
                    else
                    {
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = "zzjgdm不合法,格式不正确！";
                        return result;
                    }
                }

                ids.AppendFormat("{0},", item["ID"].ToString2());
            }
            if (ids.Length > 0)
                ids.Remove(ids.Length - 1, 1);

            DataTable dt_QyXykp = DAL.GetData_QyXykp(ids.ToString());
            DataRow row;

            foreach (DataRow item in dt_Data.Rows)
            {
                int flag = 0;
                int rowIndex = -1;
                for (int i = 0; i < dt_QyXykp.Rows.Count; i++)
                {
                    if (dt_QyXykp.Rows[i]["ID"].ToString2().Equals(item["ID"].ToString2()))
                    {
                        flag = 1;
                        rowIndex = i;
                        break;
                    }
                }

                if (flag == 1)
                {
                    row = dt_QyXykp.Rows[rowIndex];
                    DataTableHelp.DataRow2DataRow(item, row, new List<string>() { "ID" });
                }
                else
                {
                    row = dt_QyXykp.NewRow();
                    DataTableHelp.DataRow2DataRow(item, row);
                    dt_QyXykp.Rows.Add(row);
                }

                row["updateUser"] = user;
                row["updateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            }
            if (dt_QyXykp.Rows.Count > 0)
            {
                if (DAL.Save_QyXykp(dt_QyXykp))
                {
                    result.code = ProcessResult.数据保存成功;
                    result.message = "数据保存成功！";
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

        /// <summary>
        /// 保存行政处罚
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dt_Data"></param>
        /// <returns></returns>
        public ProcessResultData SaveData_Xzcf(string user, DataTable dt_Data)
        {
            ProcessResultData result = new ProcessResultData();
            StringBuilder ids = new StringBuilder();
            string msg = String.Empty;
            string[] fields = new string[] { "ajNo", "cflx", "wfwgxm", "wfwgdwry", "wfxw", "updateFlag" };

            //List<string> PrjTypes = this.GetPrjType();

            List<string> novalidates = new List<string>();
            novalidates.Add(String.Empty);
            novalidates.Add(" ");
            novalidates.Add("无");
            novalidates.Add("无数据");
            novalidates.Add("/");

            foreach (DataRow item in dt_Data.Rows)
            {
                if (BLLCommon.DataFieldIsNullOrEmpty(novalidates, fields, item, out msg))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = msg + "不能为空！";
                    return result;
                }
                ids.AppendFormat("{0},", item["ajNo"].ToString2());
            }
            if (ids.Length > 0)
                ids.Remove(ids.Length - 1, 1);

            DataTable dt_Xzcf = DAL.GetData_Xzcf(ids.ToString());
            DataRow row;

            foreach (DataRow item in dt_Data.Rows)
            {
                int flag = 0;
                int rowIndex = -1;
                for (int i = 0; i < dt_Xzcf.Rows.Count; i++)
                {
                    if (dt_Xzcf.Rows[i]["ajNo"].ToString2().Equals(item["ajNo"].ToString2()))
                    {
                        flag = 1;
                        rowIndex = i;
                        break;
                    }
                }

                if (flag == 1)
                {
                    row = dt_Xzcf.Rows[rowIndex];
                    DataTableHelp.DataRow2DataRow(item, row, new List<string>() { "ajNo" });
                    row["updateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    row = dt_Xzcf.NewRow();
                    DataTableHelp.DataRow2DataRow(item, row);
                    dt_Xzcf.Rows.Add(row);
                    row["createDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    row["updateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }

                row["updateUser"] = user;

            }
            if (dt_Xzcf.Rows.Count > 0)
            {
                if (DAL.Save_Xzcf(dt_Xzcf))
                {
                    result.code = ProcessResult.数据保存成功;
                    result.message = "数据保存成功！";
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


        /// <summary>
        /// 保存保障房源信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dt_Data"></param>
        /// <returns></returns>
        public ProcessResultData SaveBzbBZFY(DataTable dt_Data)
        {
            ProcessResultData result = new ProcessResultData();

            foreach (DataRow item in dt_Data.Rows)
            {
                DataTable dt_Bzfy = DAL.GetData_Bzfy(item["CJHOUSENO"].ToString());
                DataRow dr;

                if (dt_Bzfy.Rows.Count > 0)
                {
                    dr = dt_Bzfy.Rows[0];
                    for (int i = 0; i < dt_Data.Columns.Count; i++)
                    {
                        dr[dt_Data.Columns[i].ColumnName] = item[dt_Data.Columns[i].ColumnName];
                    }
                    dr["UpdateTime"] = DateTime.Now.Date.ToString();
                }
                else
                {
                    dr = dt_Bzfy.NewRow();
                    for (int i = 0; i < dt_Data.Columns.Count; i++)
                    {
                        dr[dt_Data.Columns[i].ColumnName] = item[dt_Data.Columns[i].ColumnName];
                    }

                    dr["CreateDate"] = DateTime.Now.Date.ToString();
                    dr["UpdateTime"] = DateTime.Now.Date.ToString();
                    dt_Bzfy.Rows.Add(dr);
                }

                if (!DAL.SaveBzbBZFY(dt_Bzfy))
                {
                    result.message += item["CJHOUSENO"] + "、";
                }
            }


            if (string.IsNullOrEmpty(result.message))
            {
                result.code = ProcessResult.数据保存成功;
                result.message = "数据保存成功！";
            }
            else
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = "房源编号为：" + result.message.Trim('、') + "；未能成功保存！ ";
            }


            return result;
        }


        /// <summary>
        /// 保存保障对象信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dt_Data"></param>
        /// <returns></returns>
        public ProcessResultData SaveBzbBZDX(DataTable dt_Data)
        {
            ProcessResultData result = new ProcessResultData();

            foreach (DataRow item in dt_Data.Rows)
            {
                DataTable dt_Bzdx = DAL.GetData_Bzdx(item["SOAPPLYIDCARDNO"].ToString());
                DataRow dr;

                if (dt_Bzdx.Rows.Count > 0)
                {
                    dr = dt_Bzdx.Rows[0];
                    for (int i = 0; i < dt_Data.Columns.Count; i++)
                    {
                        dr[dt_Data.Columns[i].ColumnName] = item[dt_Data.Columns[i].ColumnName];
                    }
                    dr["UpdateTime"] = DateTime.Now.Date.ToString();
                }
                else
                {
                    dr = dt_Bzdx.NewRow();
                    for (int i = 0; i < dt_Data.Columns.Count; i++)
                    {
                        dr[dt_Data.Columns[i].ColumnName] = item[dt_Data.Columns[i].ColumnName];
                    }
                    dr["CreateDate"] = DateTime.Now.Date.ToString();
                    dr["UpdateTime"] = DateTime.Now.Date.ToString();
                    dt_Bzdx.Rows.Add(dr);
                }

                if (!DAL.SaveBzbBZDX(dt_Bzdx))
                {
                    result.message += item["SOAPPLYIDCARDNO"] + "、";
                }
            }


            if (string.IsNullOrEmpty(result.message))
            {
                result.code = ProcessResult.数据保存成功;
                result.message = "数据保存成功！";
            }
            else
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = "申请人身份证号为：" + result.message.Trim('、') + "；未能成功保存！ ";
            }


            return result;
        }



        /// <summary>
        /// 保存保障对象家庭成员
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dt_Data"></param>
        /// <returns></returns>
        public ProcessResultData SaveBzbBZJTCY(DataTable dt_Data)
        {
            ProcessResultData result = new ProcessResultData();

            foreach (DataRow item in dt_Data.Rows)
            {
                DataTable dt_Bzdxjtcy = DAL.GetData_Bzdxjtcy(item["IDCARDNO"].ToString());
                DataRow dr;

                if (dt_Bzdxjtcy.Rows.Count > 0)
                {
                    dr = dt_Bzdxjtcy.Rows[0];
                    for (int i = 0; i < dt_Data.Columns.Count; i++)
                    {
                        dr[dt_Data.Columns[i].ColumnName] = item[dt_Data.Columns[i].ColumnName];
                    }
                    dr["UpdateTime"] = DateTime.Now.Date.ToString();
                }
                else
                {
                    dr = dt_Bzdxjtcy.NewRow();
                    for (int i = 0; i < dt_Data.Columns.Count; i++)
                    {
                        dr[dt_Data.Columns[i].ColumnName] = item[dt_Data.Columns[i].ColumnName];
                    }
                    dr["CreateDate"] = DateTime.Now.Date.ToString();
                    dr["UpdateTime"] = DateTime.Now.Date.ToString();
                    dt_Bzdxjtcy.Rows.Add(dr);
                }

                if (!DAL.SaveBzbBZDXJTCY(dt_Bzdxjtcy))
                {
                    result.message += item["IDCARDNO"] + "、";
                }
            }


            if (string.IsNullOrEmpty(result.message))
            {
                result.code = ProcessResult.数据保存成功;
                result.message = "数据保存成功！";
            }
            else
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = "成员身份证号为：" + result.message.Trim('、') + "；未能成功保存！ ";
            }


            return result;
        }


        public ProcessResultData SaveAjsbbGis(string user, DataTable dt_Data)
        {
            ProcessResultData result = new ProcessResultData();

            /**
            List<string> novalidates = new List<string>();
            novalidates.Add(String.Empty);
            novalidates.Add("无");
            novalidates.Add("/");
            string[] fields = new string[] { "pointId", "uuid", "modified", "mapLat", "mapLng", "pointOrder", "pointTeam", "pointType" ,"updateFlag"};
            string msg = String.Empty;
            foreach (DataRow item in dt_Data.Rows)
            {
                if (BLL.BLLCommon.DataFieldIsNullOrEmpty(novalidates, fields, item, out msg))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = msg + "不能为空！";
                    return result;
                }
            }
             */

            DataTable dt;
            DataRow row;

            foreach (DataRow item in dt_Data.Rows)
            {
                dt = DAL.GetTBData_Ap_ajsbb_gis(item["pointId"].ToString2());

                if (dt.Rows.Count > 0)
                {
                    row = dt.Rows[0];
                    DataTableHelp.DataRow2DataRow(item, row, new List<string>() { "pointId" });
                }
                else
                {
                    row = dt.NewRow();
                    DataTableHelp.DataRow2DataRow(item, row);
                    row["CreateTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dt.Rows.Add(row);
                }
                row["updateTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                row["updateUser"] = user;

                if (DAL.SaveTBData_Ap_ajsbb_gis(dt))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "数据保存失败！";
                }

            }

            result.code = ProcessResult.数据保存成功;
            result.message = "数据保存成功！";

            return result;
        }

        public ProcessResultData SaveAjsbbSuperviseInfo(string user, DataTable dt_Data)
        {
            ProcessResultData result = new ProcessResultData();

            DataTable dt;
            DataRow row;

            foreach (DataRow item in dt_Data.Rows)
            {
                dt = DAL.GetTBData_Ap_ajsbb_info(item["uuid"].ToString2());

                if (dt.Rows.Count > 0)
                {
                    row = dt.Rows[0];
                    DataTableHelp.DataRow2DataRow(item, row, new List<string>() { "uuid" });
                }
                else
                {
                    row = dt.NewRow();
                    DataTableHelp.DataRow2DataRow(item, row);
                    row["CreateTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dt.Rows.Add(row);
                }
                row["updateTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                row["updateUser"] = user;

                if (DAL.SaveTBData_Ap_ajsbb_info(dt))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "数据保存失败！";
                }

            }

            result.code = ProcessResult.数据保存成功;
            result.message = "数据保存成功！";

            return result;
        }


        #endregion


        #region 政务服务网相关接口
        public ProcessResultData SaveJsdw(string user, DataTable dt_Data)
        {
            ProcessResultData result = new ProcessResultData();
            string msg = String.Empty;
            string[] fields = new string[] { "jsdwID", "jsdw", "zzjgdm"};

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

            if (!Validator.IsUnifiedSocialCreditCodeOrOrgCode(item["jsdwID"].ToString2()))
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = "jsdwID不合法,格式不正确，应该为“XXXXXXXX-X”格式！";
                return result;
            }
            string jsdwID = item["jsdwID"].ToString();
            if (jsdwID.Length == 9)
            {
                item["jsdwID"] = jsdwID.Substring(0, 8) + "-" + jsdwID.Substring(8, 1);
            }
            DataTable dt_uepp_jsdw = DAL.Get_uepp_jsdw(jsdwID);
            DataRow row = null ;
            
            if (dt_uepp_jsdw.Rows.Count > 0)
            {
                //从江苏建设公共基础数据平台下行下来的数据不需要更新，原一号通或者现在住建局政府服务网上添加的建设单位才需要更新
                if(dt_uepp_jsdw.Rows[0]["tag"].ToString2().IndexOf(Tag.江苏建设公共基础数据平台.ToString()) < 0)
                {
                    int cmpFlag = DateTime.Compare(dt_uepp_jsdw.Rows[0]["xgrqsj"].ToDateTime() , item["xgrqsj"].ToDateTime());
                    if(cmpFlag < 0 )
                    {
                        row = dt_uepp_jsdw.Rows[0];
                        DataTableHelp.DataRow2DataRow(item, row, new List<string>() { "jsdwID" });
                        row["tag"] = Tag.住建局政务服务网;
                    }
                }            
            }
            else
            {
                row = dt_uepp_jsdw.NewRow();
                DataTableHelp.DataRow2DataRow(item, row);
                row["jsdwID"] = jsdwID;
                row["tag"] = Tag.住建局政务服务网;
                row["dwflid"] = 3;//单位分类ID
                row["dwfl"] = "其它";//单位分类ID
                dt_uepp_jsdw.Rows.Add(row);
            }
            if (row != null)
            {
                row["OperateDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                row["xgr"] = "";
                row["DataState"] = 0;
                try
                {
                    if (!DAL.SaveJsdw(dt_uepp_jsdw))
                    {
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = "数据保存失败!";
                    }
                }
                catch (Exception e)
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "数据保存失败:" + e.Message;
                    BLLCommon.WriteLog("SaveJsdw：" + e.Message);
                }
                
            }
            BLLCommon.WriteLog("SaveJsdw结果：" + result.message);
            return result;
        }

        public ProcessResultData SaveQyjbxx(string user, string operate, DataTable dt_Data )
        {
            ProcessResultData result = new ProcessResultData();
            string msg = String.Empty;
            string[] fields = new string[] { "qyID", "qymc", "fddbr" };

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

            if (!Validator.IsUnifiedSocialCreditCodeOrOrgCode(item["qyID"].ToString2()))
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = "qyID不合法,格式不正确，应该为18位统一社会信用代码或者10位组织机构代码“XXXXXXXX-X”格式！";
                return result;
            }
            string qyID = item["qyID"].ToString();

            if ("save".Equals(operate))
            {
                //保存企业基本信息接口：建设单位、非建设单位分开保存在不同的表里
                if (item["sfjsdw"].ToInt32() == 1)
                {
                    //建设单位
                    DataTable dt_uepp_jsdw = DAL.Get_uepp_jsdw(qyID);
                    DataRow row = null;
                    if (dt_uepp_jsdw.Rows.Count > 0)
                    {
                        row = dt_uepp_jsdw.Rows[0];
                        row["jsdw"] = item["qymc"].ToString();
                        row["fddbr"] = item["fddbr"].ToString();
                        row["fddbr_ryid"] = item["fddbrID"].ToString();
                    }
                    else
                    {
                        row = dt_uepp_jsdw.NewRow();
                        row["jsdwID"] = item["qyID"].ToString();
                        row["jsdw"] = item["qymc"].ToString();
                        row["fddbr"] = item["fddbr"].ToString();
                        row["fddbr_ryid"] = item["fddbrID"].ToString();

                        row["dwflid"] = 3;//单位分类ID
                        row["dwfl"] = "其它";//单位分类ID

                        row["xgrqsj"] = DateTime.Now;
                        dt_uepp_jsdw.Rows.Add(row);
                    }
                    row["xgr"] = user;
                    row["tag"] = Tag.住建局政务服务网;

                    try
                    {
                        if (!DAL.SaveJsdw(dt_uepp_jsdw))
                        {
                            result.code = ProcessResult.保存失败和失败原因;
                            result.message = "数据保存失败!";
                        }
                    }
                    catch (Exception e)
                    {
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = "数据保存失败:" + e.Message;
                        BLLCommon.WriteLog("SaveJsdw：" + e.Message);
                    }

                }
                else
                {
                    //非建设单位
                    DataTable dt_qy = DAL.Get_uepp_qyjbxx(qyID);
                    DataRow row = null;
                    if (dt_qy.Rows.Count > 0)
                    {
                        row = dt_qy.Rows[0];
                        row["qymc"] = item["qymc"].ToString();
                        row["fddbr"] = item["fddbr"].ToString();
                        row["fddbr_ryid"] = item["fddbrID"].ToString();
                    }
                    else
                    {
                        row = dt_qy.NewRow();
                        row["qyID"] = item["qyID"].ToString(); 
                        row["qymc"] = item["qymc"].ToString();
                        row["fddbr"] = item["fddbr"].ToString();
                        row["fddbr_ryid"] = item["fddbrID"].ToString();
                        if (item["qyID"].ToString().Length == 18)
                        {
                            row["tyshxydm"] = item["qyID"].ToString();
                        }
                        row["xgrqsj"] = DateTime.Now;
                        row["xgrqsj"] = DateTime.Now;
                        
                        dt_qy.Rows.Add(row);
                    }
                    row["xgr"] = user;
                    row["tag"] = Tag.住建局政务服务网;

                    try
                    {
                        if (!DAL.SaveQyjbxx(dt_qy))
                        {
                            result.code = ProcessResult.保存失败和失败原因;
                            result.message = "数据保存失败!";
                        }
                    }
                    catch (Exception e)
                    {
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = "数据保存失败:" + e.Message;
                        BLLCommon.WriteLog("SaveJsdw：" + e.Message);
                    }
                }
            }
            else
            {
                //校验企业是否存在接口
                if (item["sfjsdw"].ToInt32() == 1)
                {
                    DataTable dt_uepp_jsdw = DAL.Get_uepp_jsdw(qyID);
                    if (dt_uepp_jsdw.Rows.Count > 0)
                    {
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = "企业重复";
                    }
                }
                else
                {
                    DataTable dt_qy = DAL.Get_uepp_qyjbxx(qyID);
                    if (dt_qy.Rows.Count > 0)
                    {
                        result.code = ProcessResult.保存失败和失败原因;
                        result.message = "企业重复";
                    }
                }

            }

            BLLCommon.WriteLog("SaveJsdw结果：" + result.ResultMessage);
            return result;
        }

        #endregion

        public DataTable Get_API_zb_apiFlow(string apiFlow)
        {
            return DAL.Get_API_zb_apiFlow(apiFlow);
        }

        public DataTable GetSchema_API_cb()
        {
            return DAL.GetSchema_API_cb();
        }

        public DataTable GetAPIUnable()
        {
            return DAL.GetAPIUnable();
        }

        public string Get_apiCbNewID()
        {
            return DAL.Get_apiCbNewID();
        }

        public void UpdateZbJkzt(string apiFlow, string apiRunState, string apiRunMessage)
        {
            DAL.UpdateZbJkzt(apiFlow, apiRunState, apiRunMessage);
        }

        public bool Submit_API_zb(DataTable dt)
        {
            return DAL.Submit_API_zb(dt);
        }

        public bool Submit_API_cb(DataTable dt)
        {
            return DAL.Submit_API_cb(dt);
        }


    }
}
