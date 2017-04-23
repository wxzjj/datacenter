using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using Bigdesk8.Data;

namespace Bigdesk8.Business.DebugManager
{
    /// <summary>
    /// 调试信息管理
    /// </summary>
    public static class DebugManagerFactory
    {
        /// <summary>
        /// 获取调试信息管理器
        /// </summary>
        /// <param name="debugerName">debugerName</param>
        /// <returns>返回指定的调试信息管理器</returns>
        public static IDebugManager CreateDebugManager(string debugerName)
        {
            return new DBDebugManager();
        }
    }

    /// <summary>
    /// 调试信息管理
    /// </summary>
    internal class DBDebugManager : IDebugManager
    {
        #region IBusinessLogger 成员

        /// <summary>
        /// 数据库访问对象
        /// </summary>
        public DBOperator DB { get; set; }

        /// <summary>
        /// 添加调试信息
        /// </summary>
        /// <param name="businessDebug">调试信息</param>
        /// <returns>返回调试信息</returns>
        public BusinessDebug AddDebug(BusinessDebug businessDebug)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@SystemName", businessDebug.SystemName);
            spc.Add("@ModuleName", businessDebug.ModuleName);
            spc.Add("@CategoryName", businessDebug.CategoryName);
            spc.Add("@DebugMessage", businessDebug.DebugMessage);
            spc.Add("@DebugText", businessDebug.DebugText);

            businessDebug.DebugDateTime = DateTime.Now;

            spc.Add("@DebugDateTime", businessDebug.DebugDateTime);

            string sql = "insert into g_BusinessDebug(SystemName, ModuleName, CategoryName, DebugMessage, DebugText, DebugDateTime)"
                + "values(@SystemName, @ModuleName, @CategoryName, @DebugMessage, @DebugText, @DebugDateTime)";
            DB.ExecuteNonQuerySql(sql, spc);

            businessDebug.DebugID = DB.ExeSqlForObject("select max(DebugID) from g_BusinessDebug", null).ToInt64();
            return businessDebug;
        }

        /// <summary>
        /// 获得调试信息
        /// </summary>
        /// <param name="debugID">调试信息编号</param>
        /// <returns>返回调试信息</returns>
        public BusinessDebug GetDebug(Int64 debugID)
        {
            string sql = "select * from g_BusinessDebug where DebugID=@DebugID";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@DebugID", debugID);

            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "debug");
            if (dt.Rows.Count <= 0) return null;

            return DataRow2BusinessDebug(dt.Rows[0]);
        }

        /// <summary>
        /// 获得调试信息
        /// </summary>
        /// <param name="debugMessage">查询条件</param>
        /// <param name="timeSpan">最近多长时间</param>
        /// <returns>返回符合条件的调试信息</returns>
        public List<BusinessDebug> GetDebug(string debugMessage, TimeSpan timeSpan)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();

            string sql = "select * from g_BusinessDebug where DebugDateTime>=dateadd(day,-" + timeSpan.Days + ",getdate())";

            if (!debugMessage.IsEmpty())
            {
                sql += " and DebugMessage like @DebugMessage";
                spc.Add("@DebugMessage", "%" + debugMessage.TrimString() + "%");
            }

            sql += " order by DebugID desc";
            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "debug");
            return DataTable2BusinessDebugs(dt);
        }

        public List<BusinessDebug> GetDebug(List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();

            string sql = "select * from g_BusinessDebug where 1=1";

            condition.GetSearchClause(spc, ref sql);

            string orderby = "DebugID desc";
            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "t", orderby, pageSize, pageIndex, out allRecordCount);

            return DataTable2BusinessDebugs(dt);
        }

        #endregion

        private List<BusinessDebug> DataTable2BusinessDebugs(DataTable dt)
        {
            List<BusinessDebug> list = new List<BusinessDebug>();
            foreach (DataRow dr in dt.Rows)
            {
                BusinessDebug debug = DataRow2BusinessDebug(dr);
                list.Add(debug);
            }
            return list;
        }
        private BusinessDebug DataRow2BusinessDebug(DataRow dr)
        {
            BusinessDebug debug = new BusinessDebug();
            debug.DebugID = dr["DebugID"].ToInt64();
            debug.SystemName = dr["SystemName"].ToString();
            debug.ModuleName = dr["ModuleName"].ToString();
            debug.CategoryName = dr["CategoryName"].ToString();
            debug.DebugMessage = dr["DebugMessage"].ToString();
            debug.DebugText = dr["DebugText"].ToString();
            debug.DebugDateTime = dr["DebugDateTime"].ToDateTime();
            return debug;
        }
    }
}
