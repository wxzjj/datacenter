using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using Bigdesk8.Data;

namespace Bigdesk8.Business.BizMsgManager
{
    /// <summary>
    /// 日志管理
    /// </summary>
    public static class BizMsgManagerFactory
    {
        /// <summary>
        /// 获取业务消息管理器
        /// </summary>
        public static IBizMsgManager CreateBizMsgManager(string managerName)
        {
            return new DBBizMsgManager();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class DBBizMsgManager : IBizMsgManager
    {
        #region IBizMsgManager 成员

        /// <summary>
        /// 数据库访问对象
        /// </summary>
        public DBOperator DB { get; set; }

        public BusinessMsg AppendBizMsg(BusinessMsg businessMsg)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@SystemName", businessMsg.SystemName);
            spc.Add("@ModuleName", businessMsg.ModuleName);
            spc.Add("@KeyString", businessMsg.KeyString);
            spc.Add("@MsgType", businessMsg.MsgType.ToString());
            spc.Add("@MsgInfo", businessMsg.MsgInfo);
            spc.Add("@OperatorID", businessMsg.OperatorID);
            spc.Add("@OperatorName", businessMsg.OperatorName);

            businessMsg.OperateDateTime = DateTime.Now;

            spc.Add("@OperateDateTime", businessMsg.OperateDateTime);

            string sql = "insert into g_BusinessMsg(SystemName, ModuleName, KeyString, MsgType, MsgInfo, OperatorID, OperatorName, OperateDateTime)"
                + "values(@SystemName, @ModuleName, @KeyString, @MsgType, @MsgInfo, @OperatorID, @OperatorName, @OperateDateTime)";
            DB.ExecuteNonQuerySql(sql, spc);

            businessMsg.MsgID = DB.ExeSqlForObject("select max(MsgID) from g_BusinessMsg", null).ToInt64();
            return businessMsg;
        }

        /// <summary>
        /// 
        /// </summary>
        public BusinessMsg GetBizMsg(Int64 msgID)
        {
            string sql = "select * from g_BusinessMsg where msgID=@MsgID";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@MsgID", msgID);

            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "bizmsg");
            if (dt.Rows.Count <= 0) return null;

            return DataRow2BusinessMsg(dt.Rows[0]);
        }

        public List<BusinessMsg> GetBizMsg(string systemName)
        {
            string sql = "select * from g_BusinessMsg where SystemName=@SystemName";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@SystemName", systemName);

            sql += " order by MsgID desc";
            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "bizmsg");
            return DataTable2BusinessMsgs(dt);
        }

        public List<BusinessMsg> GetBizMsg(string systemName, string moduleName, string keyString)
        {
            string sql = "select * from g_BusinessMsg where SystemName=@SystemName and ModuleName=@ModuleName and KeyString=@KeyString";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@SystemName", systemName);
            spc.Add("@ModuleName", moduleName);
            spc.Add("@KeyString", keyString);

            sql += " order by MsgID desc";
            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "bizmsg");
            return DataTable2BusinessMsgs(dt);
        }

        #endregion

        private List<BusinessMsg> DataTable2BusinessMsgs(DataTable dt)
        {
            List<BusinessMsg> list = new List<BusinessMsg>();
            foreach (DataRow dr in dt.Rows)
            {
                BusinessMsg bizmsg = DataRow2BusinessMsg(dr);
                list.Add(bizmsg);
            }
            return list;
        }
        private BusinessMsg DataRow2BusinessMsg(DataRow dr)
        {
            BusinessMsg bizmsg = new BusinessMsg();
            bizmsg.MsgID = dr["MsgId"].ToInt64();
            bizmsg.SystemName = dr["SystemName"].ToString();
            bizmsg.ModuleName = dr["ModuleName"].ToString();
            bizmsg.KeyString = dr["KeyString"].ToString();
            bizmsg.MsgType = (BizMsgType)Enum.Parse(typeof(BizMsgType),dr["MsgType"].ToString());
            bizmsg.MsgInfo = dr["MsgInfo"].ToString();
            bizmsg.OperatorID = dr["OperatorId"].ToString();
            bizmsg.OperatorName = dr["OperatorName"].ToString();
            bizmsg.OperateDateTime = dr["OperateDateTime"].ToDateTime();
            return bizmsg;
        }
    }
}
