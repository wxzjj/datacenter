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

    /// <summary>
    /// 功能： 无锡数据中心与各县市系统数据交换业务处理类
    /// 作者：孙刚
    /// 时间：2015-03-31
    /// </summary>
    public class DataExchangeBLLForCounty
    {
        private readonly Wxjzgcjczy.DAL.Sqlserver.DataExchangeDALForCounty DAL = new Wxjzgcjczy.DAL.Sqlserver.DataExchangeDALForCounty();
        string userName = "320200", password = "we&gjh45H";

        XmlHelper xmlHelper = new XmlHelper();
        ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient client = new ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient();

        public DataExchangeBLLForCounty()
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

        /// <summary>
        /// 读取无锡数据中心数据(TBProjectDesignEconUserInfo)
        /// </summary>
        /// <returns></returns>
        public DataTable GetTBData_TBProjectDesignEconUserInfo(string censorNum)
        {
            DataTable dt = DAL.GetTBData_TBProjectDesignEconUserInfo(censorNum);
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
        /// 读取无锡数据中心数据-一站式安监申报(ap_ajsbb)
        /// </summary>
        /// <returns></returns>
        public DataTable GetTBData_ap_ajsbb(List<IDataItem> conditions)
        {
            DataTable dt = DAL.GetTBData_ap_ajsbb(conditions);
            return dt;
        }

        /// <summary>
        /// 按uuid获取安监申报审批数据
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public DataTable GetTBData_ap_ajsbb_jg(string uuid)
        {
            DataTable dt = DAL.GetTBData_ap_ajsbb_jg(uuid);
            return dt;
        }

        /// <summary>
        /// 读取无锡数据中心数据-一站式安监申报(zp_ajsbb)
        /// </summary>
        /// <returns></returns>
        public DataTable GetTBData_ap_zjsbb(List<IDataItem> conditions)
        {
            DataTable dt = DAL.GetTBData_ap_zjsbb(conditions);
            return dt;
        }

        /// <summary>
        /// 读取无锡数据中心数据(TBBuilderLicenceManageCanJianDanW)
        /// </summary>
        /// <returns></returns>
        public DataTable GetTBData_TBBuilderLicenceManageCanJianDanW(string builderLicenceNum)
        {
            DataTable dt = DAL.GetTBData_TBBuilderLicenceManageCanJianDanW(builderLicenceNum);
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

        public DataTable GetTBData_zj_gcjbxx_zrdw(string zljdbm)
        {
            DataTable dt = DAL.GetTBData_zj_gcjbxx_zrdw(zljdbm);
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

        public DataTable Get_uepp_jsdw_by_qyid(string qyid)
        {
            return DAL.Get_uepp_jsdw_by_qyid(qyid);
        }

        public DataTable Get_uepp_jsdw_bycounty(string countyNum, string startDate, string endDate)
        {
            return DAL.Get_uepp_jsdw_bycounty(countyNum, startDate, endDate); ;
        }
        public DataTable Get_uepp_sgdw_bycounty(String countyNum, List<IDataItem> conditions)
        {
            return DAL.Get_uepp_sgdw_bycounty(countyNum,conditions); ;
        }
        public DataTable Get_uepp_kcdw_bycounty(String countyNum, List<IDataItem> conditions)
        {
            return DAL.Get_uepp_kcdw_bycounty(countyNum, conditions); ;
        }
        public DataTable Get_uepp_sjdw_bycounty(String countyNum, List<IDataItem> conditions)
        {
            return DAL.Get_uepp_sjdw_bycounty(countyNum, conditions); ;
        }
        public DataTable Get_uepp_zjjg_bycounty(String countyNum, List<IDataItem> conditions)
        {
            return DAL.Get_uepp_zjjg_bycounty(countyNum, conditions); ;
        }

       

        public DataTable GetQyjbxx(string qyid)
        {
            return DAL.GetQyjbxx(qyid);
        }

        /// <summary>
        /// 获取企业证书信息
        /// </summary>
        /// <param name="qyID"></param>
        /// <returns></returns>
        public DataTable GetCorpCert(String qyID )
        {
            return DAL.GetCorpCert(qyID); ;
        }

        /// <summary>
        /// 获取企业人员信息
        /// </summary>
        /// <param name="qyID"></param>
        /// <returns></returns>
        public DataTable GetCorpStaff(String qyID)
        {
            return DAL.GetCorpStaff(qyID); ;
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

        public DataTable Get_uepp_sgdw_single(string corpCode)
        {
            return DAL.Get_uepp_sgdw_single(corpCode);
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

        /// <summary>
        /// 获取企业所有资质信息
        /// </summary>
        /// <param name="qyID"></param>
        /// <returns></returns>
        public DataTable Get_uepp_all_qyzz(string qyID)
        {
            return DAL.Get_uepp_all_qyzz(qyID);
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

        /// <summary>
        /// 获取企业所有证书信息
        /// </summary>
        /// <param name="qyID"></param>
        /// <returns></returns>
        public DataTable Get_uepp_all_qyzs(string qyID)
        {
            return DAL.Get_uepp_all_qyzs(qyID);
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
