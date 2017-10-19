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

namespace Wxjzgcjczy.BLL
{
    /// <summary>
    /// 功能： 无锡数据中心与GIS相关处理
    /// 作者：顾立强
    /// 时间：2017-10-10
    /// </summary>
    public class DataExchangeBLLForGIS
    {

        private readonly Wxjzgcjczy.DAL.Sqlserver.DataExchangeDALForGIS DAL = new Wxjzgcjczy.DAL.Sqlserver.DataExchangeDALForGIS();

        XmlHelper xmlHelper = new XmlHelper();

        ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient client = new ReceiveDataServiceSpace.ReceiveDataServicePortTypeClient();


        public DataExchangeBLLForGIS()
        {
            DAL.DB = new Wxjzgcjczy.DAL.Sqlserver.DatabaseOperator();
        }

        public bool SubmitData(string sql, DataTable dt)
        {

            return DAL.DB.Update(sql, null, dt);

        }

        #region 获取项目信息

        public DataTable GetProjectInfo(string countyNum, string beginDate, string endDate)
        {
            DataTable dt = DAL.GetProject(null, null, null, countyNum, beginDate, endDate);
            return dt;
        }

        public DataTable GetBuilderLicenceManage(string sbdqbm, string beginDate, string endDate)
        {
            DataTable dt = DAL.GetBuildingLicense(null, sbdqbm, beginDate, endDate);
            return dt;
        }

        public DataTable GetProjectFinishManage(string sbdqbm, string beginDate, string endDate)
        {
            DataTable dt = DAL.GetProjectFinish(null, sbdqbm, beginDate, endDate);
            return dt;
        }

        public DataTable GetProject(string prjNum, string prjName, string location)
        {
            DataTable dt = DAL.GetProject(prjNum, prjName, location, null, null, null);
            return dt;
        }

        public DataTable GetProjectByRange(string range)
        {
            DataTable dt = DAL.GetProjectByRange(range);
            return dt;
        }

        public DataTable GetProject(string prjNum)
        {
            DataTable dt = DAL.GetProject(prjNum, null, null, null, null, null);
            return dt;
        }

        public DataTable GetSubProject(string prjNum)
        {
            DataTable dt = DAL.GetSubProject(prjNum);
            return dt;
        }

        public DataTable GetBuildingLicense(string prjNum)
        {
            DataTable dt = DAL.GetBuildingLicense(prjNum, null, null, null);
            return dt;
        }

        public DataTable GetProjectFinish(string prjNum)
        {
            DataTable dt = DAL.GetProjectFinish(prjNum, null, null, null);
            return dt;
        }

        #endregion


        #region 接收档案相关信息
        /// <summary>
        /// 接收档案相关信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        public ProcessResultData saveProjectDocInfo(string fxbm, string docNum)
        {
            ProcessResultData result = new ProcessResultData();

            int effects = DAL.SaveProjectDoc(fxbm, docNum);

            if (effects > 0)
            {
                result.code = ProcessResult.数据保存成功;
            }
            else
            {
                result.code = ProcessResult.保存失败和失败原因;
            }

            return result;
        }

        #endregion

    }
}
