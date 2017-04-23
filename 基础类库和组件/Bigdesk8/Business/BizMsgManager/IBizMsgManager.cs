using System;
using System.Collections.Generic;
using Bigdesk8.Data;

namespace Bigdesk8.Business.BizMsgManager
{
    /// <summary>
    /// 业务消息管理接口
    /// </summary>
    public interface IBizMsgManager
    {
        /// <summary>
        /// 数据库访问对象
        /// </summary>
        DBOperator DB { get; set; }

        /// <summary>
        /// 追加业务消息
        /// </summary>
        BusinessMsg AppendBizMsg(BusinessMsg businessMsg);

        /// <summary>
        /// 获取业务消息
        /// </summary>
        BusinessMsg GetBizMsg(Int64 msgID);

        /// <summary>
        /// 获取业务消息
        /// </summary>
        List<BusinessMsg> GetBizMsg(string systemName);

        /// <summary>
        /// 获取业务消息
        /// </summary>
        List<BusinessMsg> GetBizMsg(string systemName, string moduleName, string keyString);

    }

    /// <summary>
    /// 业务消息类，创建时，所有属性不能为NULL
    /// </summary>
    public class BusinessMsg
    {
        /// <summary>
        /// 流水号
        /// </summary>
        public Int64 MsgID { get; internal set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 主键标识
        /// </summary>
        public string KeyString { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public BizMsgType MsgType { get; set; }

        /// <summary>
        /// 消息内容，可以是xml格式
        /// </summary>
        public string MsgInfo { get; set; }

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

        public BizMsgStatus MsgStatus { get; set; }
    }

    public enum BizMsgType
    {
        领导批注,

        政务移交
    }

    public enum BizMsgStatus
    {
        新消息
    }
}
