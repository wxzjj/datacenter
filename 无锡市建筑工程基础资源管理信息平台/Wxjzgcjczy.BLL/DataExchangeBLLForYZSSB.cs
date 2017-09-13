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
    /// 功能： 无锡数据中心与各县市系统数据交换业务处理类之一站式申报数据处理
    /// 作者：黄正宇
    /// 时间：2017-09-10
    /// </summary>
    public class DataExchangeBLLForYZSSB
    {
        private readonly Wxjzgcjczy.DAL.Sqlserver.DataExchangeDALForYZSSB DAL = new Wxjzgcjczy.DAL.Sqlserver.DataExchangeDALForYZSSB();

        XmlHelper xmlHelper = new XmlHelper();

        public DataExchangeBLLForYZSSB()
        {
            DAL.DB = new Wxjzgcjczy.DAL.Sqlserver.DatabaseOperator();
        }

        public bool SubmitData(string sql, DataTable dt)
        {

            return DAL.DB.Update(sql, null, dt);

        }

        #region 读取安监申报数据

        public DataTable GetAp_ajsbb(string date, string countryCodes)
        {
            DataTable dt = DAL.GetAp_ajsbb(date, countryCodes);
            return dt;
        }

        public DataTable GetAp_ajsbb_ht(string uuid)
        {
            DataTable dt = DAL.GetAp_ajsbb_ht(uuid);
            return dt;
        }

        public DataTable GetAp_ajsbb_dwry(string uuid)
        {
            DataTable dt = DAL.GetAp_ajsbb_dwry(uuid);
            return dt;
        }

        public DataTable GetAp_ajsbb_clqd(string uuid)
        {
            DataTable dt = DAL.GetAp_ajsbb_clqd(uuid);
            return dt;
        }

        public DataTable GetAp_ajsbb_hjssjd(string uuid)
        {
            DataTable dt = DAL.GetAp_ajsbb_hjssjd(uuid);
            return dt;
        }

        public DataTable GetAp_ajsbb_wxyjdgcqd(string uuid)
        {
            DataTable dt = DAL.GetAp_ajsbb_wxyjdgcqd(uuid);
            return dt;
        }

        public DataTable GetAp_ajsbb_cgmgcqd(string uuid)
        {
            DataTable dt = DAL.GetAp_ajsbb_cgmgcqd(uuid);
            return dt;
        }

        #endregion

        #region 读取质监申报数据

        public DataTable GetAp_zjsbb(string date, string countryCodes)
        {
            DataTable dt = DAL.GetAp_zjsbb(date, countryCodes);
            return dt;
        }

        public DataTable GetAp_zjsbb_ht(string uuid)
        {
            DataTable dt = DAL.GetAp_zjsbb_ht(uuid);
            return dt;
        }

        public DataTable GetAp_zjsbb_dwry(string uuid)
        {
            DataTable dt = DAL.GetAp_zjsbb_dwry(uuid);
            return dt;
        }

        public DataTable GetAp_zjsbb_schgs(string uuid)
        {
            DataTable dt = DAL.GetAp_zjsbb_schgs(uuid);
            return dt;
        }

        public DataTable GetAp_zjsbb_dwgc(string uuid)
        {
            DataTable dt = DAL.GetAp_zjsbb_dwgc(uuid);
            return dt;
        }

        public DataTable GetAp_zjsbb_clqd(string uuid)
        {
            DataTable dt = DAL.GetAp_zjsbb_clqd(uuid);
            return dt;
        }


        #endregion

    }
}
