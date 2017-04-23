using System.Transactions;
using Bigdesk8.Business;
using Bigdesk8.Business.LogManager;
using Bigdesk8;
using Bigdesk8.Data;
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Linq;
namespace Wxjzgcjczy.BLL
{
    public static class BLLCommon
    {
        /// <summary>
        /// 根据配置项，确定在此事务域内是否启用事务
        /// </summary>
        /// <returns></returns>
        public static TransactionScope GetTransactionScope()
        {
            if (ConfigManager.HaveTransactionBeenTurnedOn())
                return new TransactionScope(TransactionScopeOption.Required);
            else
                return new TransactionScope(TransactionScopeOption.Suppress);
        }

        public static void WriteLog(string msg)
        {
            string filaname = AppDomain.CurrentDomain.BaseDirectory + "\\Log_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            try
            {
                if (!File.Exists(filaname))
                {
                    File.WriteAllText(filaname, msg + "\r\n", Encoding.UTF8);
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(filaname))
                    {
                        sw.WriteLine(msg);
                        sw.Flush();
                        sw.Close();
                    }
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// 判断是不是空或是否是空串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool DataFieldIsNullOrEmpty(object obj)
        {
            return obj.ToString2().Trim().Equals(String.Empty);
        }

        //public static bool DataFieldIsNullOrEmpty(Dictionary<string, object> fields, out string msg)
        //{
        //    msg = String.Empty;
        //    bool hasEmptyField = false;
        //    if (fields == null || fields.Count == 0) return false;
        //    foreach (KeyValuePair<string, object> item in fields)
        //    {
        //        if (item.Value.ToString2().Trim().Equals(String.Empty))
        //        {
        //            hasEmptyField = true;
        //            msg = item.Key + "、";
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(msg))
        //        msg = msg.TrimEnd('、');

        //    return hasEmptyField;
        //}

        public static bool DataFieldIsNullOrEmpty(string[] fields, DataRow row, out string msg)
        {
            List<string> invalidateValues = new List<string>();
            invalidateValues.Add(string.Empty);
            msg = String.Empty;
            bool hasEmptyField = false;
            if (row == null || fields == null || fields.Length == 0) return false;
            foreach (string fieldName in fields)
            {
                if (invalidateValues.Exists(p => p.Equals(row[fieldName].ToString2().Trim())))
                {
                    hasEmptyField = true;
                    msg += fieldName + "、";
                }
            }
            if (!string.IsNullOrEmpty(msg))
                msg = msg.TrimEnd('、');

            return hasEmptyField;
        }
        /// <summary>
        /// 检查数据行里字段是否存在和集合里面的值相等
        /// </summary>
        /// <param name="invalidateValues">检查字段可能的值</param>
        /// <param name="fields">待检查的字段</param>
        /// <param name="row">数据行</param>
        /// <param name="msg">输出信息，包含了不合法的字段信息</param>
        /// <returns></returns>
        public static bool DataFieldIsNullOrEmpty(List<string> invalidateValues, string[] fields, DataRow row, out string msg)
        {

            msg = String.Empty;
            bool hasEmptyField = false;
            if (invalidateValues == null || invalidateValues.Count==0 || 
                row == null || fields == null || fields.Length == 0) return false;
            foreach (string fieldName in fields)
            {
                if (invalidateValues.Exists(p => p.Equals(row[fieldName].ToString2().Trim())))
                {
                    hasEmptyField = true;
                    msg += fieldName + "、";
                }
            }
            if (!string.IsNullOrEmpty(msg))
                msg = msg.TrimEnd('、');

            return hasEmptyField;
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            foreach (var item in source)
                action(item);

            return source;
        }


    }



 



    /// <summary>
    /// 系统日志 的业务逻辑层
    /// </summary>
    public class AppLog
    {
        private readonly ILogManager logManager = LogManagerFactory.CreateLogManager(SubSystem.公司运营管理系统.ToString());
        private AppUser AppUser;

        public AppLog(AppUser workUser, DBOperator db)
        {
            logManager.DB = db;
            this.AppUser = workUser;
        }
        public AppLog(DBOperator db)
        {
            logManager.DB = db;
        }

        public void Create(ModuleCode moduleCode, string businessID, string operate)
        {
            Create(moduleCode, businessID, operate, "");
        }

        public void Create(ModuleCode moduleCode, string businessID, string operate, string opinion)
        {
            BusinessLog log = new BusinessLog();
            log.SystemName = SubSystem.苏州市园林绿化企业动态管理系统.ToString();
            log.CategoryName = "";
            log.PostStatus = "";
            log.PriorStatus = "";
            log.ModuleName = moduleCode.ToString();
            log.KeyString = businessID.ToString();
            log.Operation = operate;
            log.MessageInfo = opinion;
            log.OperatorID = AppUser.UserID.ToString();
            log.OperatorName = AppUser.UserName;
            logManager.AppendLog(log);
        }
        public void Create(ModuleCode moduleCode, string businessID, string operate, string opinion, string userid)
        {
            BusinessLog log = new BusinessLog();
            log.SystemName = SubSystem.苏州市园林绿化企业动态管理系统.ToString();
            log.CategoryName = "";
            log.PostStatus = "";
            log.PriorStatus = "";
            log.ModuleName = moduleCode.ToString();
            log.KeyString = businessID.ToString();
            log.Operation = operate;
            log.MessageInfo = opinion;
            log.OperatorID = userid;
            log.OperatorName = "";
            logManager.AppendLog(log);
        }


    }
}
