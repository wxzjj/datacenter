using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using Bigdesk8.Data;

namespace Bigdesk8.Business.LogManager
{
    /// <summary>
    /// 日志管理
    /// </summary>
    public static class LogManagerFactory
    {
        /// <summary>
        /// 获取日志管理器
        /// </summary>
        /// <param name="loggerName">loggerName</param>
        /// <returns>返回指定的日志管理器</returns>
        public static ILogManager CreateLogManager(string loggerName)
        {
            return new DBLogManager();
        }
    }

    /// <summary>
    /// 随着业务日志量的增加, 也许需要调整日志的增加和查询方式;
    /// 例如, 根据业务名称的不同, 将日志放到不同的业务日志表, 查询也到不同的表中查询.
    ///       这时可以设置一个中间配置表A, 从中可以查出业务名称和具体日志表的对应关系,
    ///       由此将不同业务的日志放到不同的表中.
    /// 这些调整将来就在BusinessLogger类内部完成, 不影响对外接口.
    /// </summary>
    internal class DBLogManager : ILogManager
    {
        #region IBusinessLogger 成员

        /// <summary>
        /// 数据库访问对象
        /// </summary>
        public DBOperator DB { get; set; }

        public BusinessLog AppendLog(BusinessLog businessLog)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@SystemName", businessLog.SystemName);
            spc.Add("@ModuleName", businessLog.ModuleName);
            spc.Add("@CategoryName", businessLog.CategoryName);
            spc.Add("@Operation", businessLog.Operation);
            spc.Add("@KeyString", businessLog.KeyString);
            spc.Add("@PriorStatus", businessLog.PriorStatus);
            spc.Add("@PostStatus", businessLog.PostStatus);
            spc.Add("@MessageInfo", businessLog.MessageInfo);
            spc.Add("@OperatorID", businessLog.OperatorID);
            spc.Add("@OperatorName", businessLog.OperatorName);

            businessLog.OperateDateTime = DateTime.Now;

            spc.Add("@OperateDateTime", businessLog.OperateDateTime);

            string sql = "insert into g_BusinessLog(SystemName, ModuleName, CategoryName, KeyString, Operation, PriorStatus, PostStatus, MessageInfo, OperatorID, OperatorName, OperateDateTime)"
                + "values(@SystemName, @ModuleName, @CategoryName, @KeyString, @Operation, @PriorStatus, @PostStatus, @MessageInfo, @OperatorID, @OperatorName, @OperateDateTime)";
            DB.ExecuteNonQuerySql(sql, spc);

            businessLog.LogID = DB.ExeSqlForObject("select max(LogID) from g_BusinessLog", null).ToInt64();
            return businessLog;
        }

        /// <summary>
        /// 获取日志
        /// </summary>
        /// <param name="logID">日志编号</param>
        /// <returns>返回日志</returns>
        public BusinessLog GetLog(Int64 logID)
        {
            string sql = "select * from g_BusinessLog where LogID=@LogID";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@LogID", logID);

            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "log");
            if (dt.Rows.Count <= 0) return null;

            return DataRow2BusinessLog(dt.Rows[0]);
        }

        public List<BusinessLog> GetLog(string systemName, string moduleName, string keyString, string operation)
        {
            return GetLog(systemName, moduleName, keyString, new string[] { operation });
        }

        public List<BusinessLog> GetLog(string systemName, string moduleName, string keyString, string[] operation)
        {
            string sql = "select * from g_BusinessLog where SystemName=@SystemName and ModuleName=@ModuleName and KeyString=@KeyString";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@SystemName", systemName);
            spc.Add("@ModuleName", moduleName);
            spc.Add("@KeyString", keyString);

            sql += " and (1=2";
            for (int i = 0; i < operation.Length; i++)
            {
                sql += " or Operation=@Operation" + i;
                spc.Add("@Operation" + i, operation[i]);
            }
            sql += ")";

            sql += " order by LogID desc";
            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "log");
            return DataTable2BusinessLogs(dt);
        }

        public List<BusinessLog> GetLog(List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();

            string sql = "select * from g_BusinessLog where 1=1";

            condition.GetSearchClause(spc, ref sql);

            string orderby = "LogID desc";
            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "t", orderby, pageSize, pageIndex, out allRecordCount);

            return DataTable2BusinessLogs(dt);
        }

        #endregion

        private List<BusinessLog> DataTable2BusinessLogs(DataTable dt)
        {
            List<BusinessLog> list = new List<BusinessLog>();
            foreach (DataRow dr in dt.Rows)
            {
                BusinessLog log = DataRow2BusinessLog(dr);
                list.Add(log);
            }
            return list;
        }
        private BusinessLog DataRow2BusinessLog(DataRow dr)
        {
            BusinessLog log = new BusinessLog();
            log.LogID = dr["LogId"].ToInt64();
            log.SystemName = dr["SystemName"].ToString();
            log.ModuleName = dr["ModuleName"].ToString();
            log.CategoryName = dr["CategoryName"].ToString();
            log.Operation = dr["Operation"].ToString();
            log.KeyString = dr["KeyString"].ToString();
            log.PriorStatus = dr["PriorStatus"].ToString();
            log.PostStatus = dr["PostStatus"].ToString();
            log.MessageInfo = dr["MessageInfo"].ToString();
            log.OperatorID = dr["OperatorId"].ToString();
            log.OperatorName = dr["OperatorName"].ToString();
            log.OperateDateTime = dr["OperateDateTime"].ToDateTime();
            return log;
        }
    }
}
