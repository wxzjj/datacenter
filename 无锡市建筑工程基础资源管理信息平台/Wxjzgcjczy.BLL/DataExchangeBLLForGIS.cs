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
            DataTable dt = DAL.GetProject(null, null, null, null, null, countyNum, beginDate, endDate);
            return dt;
        }

        public DataTable GetSubProjectInfo(string sbdqbm, string beginDate, string endDate)
        {
            DataTable dt = DAL.GetSubProject(null, sbdqbm, beginDate, endDate);
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

        public DataTable GetProjectCensorInfo(string sbdqbm, string beginDate, string endDate)
        {
            DataTable dt = DAL.GetProjectCensorInfo(null, sbdqbm, beginDate, endDate);
            return dt;
        }

        public DataTable GetProject(string prjNum, string prjName, String buildCorpCode, String buildCorpName, string location)
        {
            DataTable dt = DAL.GetProject(prjNum, prjName, buildCorpCode, buildCorpName, location, null, null, null);
            return dt;
        }

        public DataTable GetProject(string prjNum, string prjName, string location)
        {
            DataTable dt = DAL.GetProject(prjNum, prjName, null, null, location, null, null, null);
            return dt;
        }

        public DataTable GetProjectByRange(string range, string qy, string beginDate, string endDate)
        {
            DataTable dt = DAL.GetProjectByRange(range, qy, beginDate, endDate);
            return dt;
        }

        public DataTable GetProject(string prjNum)
        {
            DataTable dt = DAL.GetProject(prjNum, null,null, null, null, null, null, null);
            return dt;
        }

        public DataTable GetSubProject(string prjNum)
        {
            DataTable dt = DAL.GetSubProject(prjNum, null, null, null);
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

        public DataTable GetProjectCensor(string prjNum)
        {
            DataTable dt = DAL.GetProjectCensorInfo(prjNum, null, null, null);
            return dt;
        }

        #endregion


        #region 接收档案相关信息

        /// <summary>
        /// 接收项目档案相关信息
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public ProcessResultData saveProjectDocInfo(DataRow row)
        {
            ProcessResultData result = new ProcessResultData();

            int effects = DAL.SaveProjectDoc(row);

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

        /// <summary>
        /// 接收项目位置相关信息
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public ProcessResultData saveProjectPositionInfo(DataRow row)
        {
            ProcessResultData result = new ProcessResultData();

            int effects = DAL.SaveProjectPosition(row);

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

        

        /// <summary>
        /// 接收单体项目档案相关信息
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public ProcessResultData saveSubProjectDocInfo(DataRow row)
        {
            ProcessResultData result = new ProcessResultData();

            int effects = DAL.SaveSubProjectDoc(row);

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
