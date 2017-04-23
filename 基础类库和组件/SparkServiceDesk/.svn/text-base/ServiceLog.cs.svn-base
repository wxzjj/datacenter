using System;
using System.Configuration;
using Bigdesk8.Business.LogManager;
using Bigdesk8.Data;

namespace SparkServiceDesk
{
    public class ServiceLog
    {
        public static void Log(string systemName, string moduleName, string fileName, string messageInfo)
        {
            ConnectionStringSettings connstr = ConfigurationManager.ConnectionStrings["SparkServiceDesk_LogManagerConnectionString"];
            DBOperator db = DBOperatorFactory.GetDBOperator(connstr);
            ILogManager logManager = LogManagerFactory.CreateLogManager("DEFAULT");
            logManager.DB = db;
            BusinessLog bizLog = new BusinessLog();
            bizLog.SystemName = systemName;
            bizLog.ModuleName = moduleName;
            bizLog.CategoryName = "上传文件";
            bizLog.KeyString = fileName;
            bizLog.Operation = "上传文件";
            bizLog.PriorStatus = "操作前";
            bizLog.PostStatus = "操作后";
            bizLog.MessageInfo = messageInfo;
            bizLog.OperatorID = "-1";
            bizLog.OperatorName = "无名氏";
            logManager.AppendLog(bizLog);
        }
    }
}
