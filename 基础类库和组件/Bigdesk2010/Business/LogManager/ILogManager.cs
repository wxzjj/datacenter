using System;
using System.Collections.Generic;
using Bigdesk2010.Data;

namespace Bigdesk2010.Business.LogManager
{
    /// <summary>
    /// 日志管理接口
    /// </summary>
    public interface ILogManager
    {
        /// <summary>
        /// 数据库访问对象
        /// </summary>
        DBOperator DB { get; set; }

        /// <summary>
        /// 追加日志
        /// </summary>
        /// <param name="businessLog">日志内容</param>
        BusinessLog AppendLog(BusinessLog businessLog);

        /// <summary>
        /// 获取日志
        /// </summary>
        /// <param name="logID">日志编号</param>
        /// <returns>返回日志</returns>
        BusinessLog GetLog(Int64 logID);

        /// <summary>
        /// 获取日志
        /// </summary>
        /// <param name="systemName">系统名称</param>
        /// <param name="moduleName">模块名称</param>
        /// <param name="keyString">关键信息</param>
        /// <param name="operation">操作</param>
        /// <returns></returns>
        List<BusinessLog> GetLog(string systemName, string moduleName, string keyString, string operation);

        /// <summary>
        /// 获取日志
        /// </summary>
        /// <param name="systemName">系统名称</param>
        /// <param name="moduleName">模块名称</param>
        /// <param name="keyString">关键信息</param>
        /// <param name="operation">操作</param>
        /// <returns></returns>
        List<BusinessLog> GetLog(string systemName, string moduleName, string keyString, string[] operation);

        /// <summary>
        /// 获取日志
        /// </summary>
        /// <param name="condition">搜索条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="allRecordCount">总记录数</param>
        /// <returns></returns>
        List<BusinessLog> GetLog(List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount);
    }

    /// <summary>
    /// 日志类，创建时，所有属性不能为NULL
    /// </summary>
    public class BusinessLog
    {
        /// <summary>
        /// 流水号
        /// </summary>
        public Int64 LogID { get; internal set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 种类名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 主键标识
        /// </summary>
        public string KeyString { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public string Operation { get; set; }

        /// <summary>
        /// 操作前状态
        /// </summary>
        public string PriorStatus { get; set; }

        /// <summary>
        /// 操作后状态
        /// </summary>
        public string PostStatus { get; set; }

        /// <summary>
        /// 信息，意见等
        /// </summary>
        public string MessageInfo { get; set; }

        /// <summary>
        /// 操作人LoginId
        /// </summary>
        public string OperatorID { get; set; }

        /// <summary>
        /// 操作人名称
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 操作日期时间
        /// </summary>
        public DateTime OperateDateTime { get; internal set; }
    }
}
