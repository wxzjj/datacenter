using System;
using System.Configuration;
using System.Data;
using Bigdesk2010.Data;
using System.Collections.Generic;

namespace Bigdesk2010.Business.StateManager
{
    /// <summary>
    /// some note
    /// </summary>
    public sealed class StateManagerFactory
    {
        /// <summary>
        /// some note
        /// </summary>
        /// <param name="managerName"></param>
        /// <returns></returns>
        public static IStateManager CreateStateManager(string managerName)
        {
            return new DBStateManager();
        }
    }


    internal class DBStateManager : IStateManager
    {
        #region IStateManager 成员

        /// <summary>
        /// 数据库访问对象
        /// </summary>
        public DBOperator DB { get; set; }

        public int GetNextStateIfPast(int systemID, string moduleCode, int currentState)
        {
            string sql = "select NextStateIfPast from g_statetransferrule where systemid=@SystemID and ModuleCode=@ModuleCode and CurrentState=@CurrentState ";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@SystemID", systemID);
            spc.Add("@ModuleCode", moduleCode);
            spc.Add("@CurrentState", currentState);

            return DB.ExeSqlForObject(sql, spc).ToInt32();
        }

        public int GetNextStateIfRejected(int systemID, string moduleCode, int currentState)
        {
            string sql = "select NextStateIfRejected from g_statetransferrule where systemid=@SystemID and ModuleCode=@ModuleCode and CurrentState=@CurrentState ";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@SystemID", systemID);
            spc.Add("@ModuleCode", moduleCode);
            spc.Add("@CurrentState", currentState);

            return DB.ExeSqlForObject(sql, spc).ToInt32();
        }

        public int GetNextCancelStateWhenCancelled(int systemID, string moduleCode, int currentCancelState)
        {
            string sql = " select NextCancelState from g_statecancelrule where systemid=@SystemID and ModuleCode=@ModuleCode and CancelState=@CancelState ";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@SystemID", systemID);
            spc.Add("@ModuleCode", moduleCode);
            spc.Add("@CancelState", currentCancelState);
           
            return DB.ExeSqlForObject(sql, spc).ToInt32();
        }

        public int GetNextOperateStateWhenCancelled(int systemID, string moduleCode, int currentCancelState)
        {
            string sql = " select NextOperateState from g_statecancelrule where systemid=@SystemID and ModuleCode=@ModuleCode and CancelState=@CancelState";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@SystemID", systemID);
            spc.Add("@ModuleCode", moduleCode);
            spc.Add("@CancelState", currentCancelState);
           
            return DB.ExeSqlForObject(sql, spc).ToInt32();
        }

        public RecordState GetFollowingStateWhenCancelled(int systemID, string moduleCode, int currentCancelState)
        {
            string sql = " select NextOperateState,NextCancelState from g_statecancelrule where systemid=@SystemID and ModuleCode=@ModuleCode and CancelState=@CancelState";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@SystemID", systemID);
            spc.Add("@ModuleCode", moduleCode);
            spc.Add("@CancelState", currentCancelState);

            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "g_statecancelrule");
            RecordState recState = new RecordState();
            recState.OperateState = dt.Rows[0]["NextOperateState"].ToInt32();
            recState.CancelState = dt.Rows[0]["NextCancelState"].ToInt32();
            return recState;
        }
        #endregion
    }
}
