using System;
using System.Collections.Generic;
using Bigdesk8.Data;

namespace Bigdesk8.Business.DebugManager
{
    /// <summary>
    /// 调试信息管理接口
    /// </summary>
    public interface IDebugManager
    {
        /// <summary>
        /// 数据库访问对象
        /// </summary>
        DBOperator DB { get; set; }

        /// <summary>
        /// 添加调试信息
        /// </summary>
        /// <param name="businessDebug">调试信息</param>
        /// <returns>返回调试信息</returns>
        BusinessDebug AddDebug(BusinessDebug businessDebug);

        /// <summary>
        /// 获得调试信息
        /// </summary>
        /// <param name="debugID">调试信息编号</param>
        /// <returns>返回调试信息</returns>
        BusinessDebug GetDebug(Int64 debugID);

        /// <summary>
        /// 获得调试信息
        /// </summary>
        /// <param name="debugMessage">查询条件</param>
        /// <param name="timeSpan">最近多长时间</param>
        /// <returns>返回符合条件的调试信息</returns>
        List<BusinessDebug> GetDebug(string debugMessage, TimeSpan timeSpan);

        /// <summary>
        /// 获得调试信息
        /// </summary>
        /// <param name="condition">搜索条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="allRecordCount">总记录数</param>
        /// <returns></returns>
        List<BusinessDebug> GetDebug(List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount);
    }

    /// <summary>
    /// 调试信息类，创建时，所有属性不能为NULL
    /// </summary>
    public class BusinessDebug
    {
        /// <summary>
        /// 流水号
        /// </summary>
        public Int64 DebugID { get; internal set; }

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
        /// 调试消息
        /// </summary>
        public string DebugMessage { get; set; }

        /// <summary>
        /// 调试详细信息
        /// </summary>
        public string DebugText { get; set; }

        /// <summary>
        /// 调试日期时间
        /// </summary>
        public DateTime DebugDateTime { get; internal set; }
    }
}
