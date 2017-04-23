using System;
using System.IO;
using System.Collections.Generic;
using Bigdesk8.Data;
using Bigdesk8.Business;

namespace Bigdesk8.Business.StateManager
{
    /// <summary>
    /// 业务信息状态管理接口
    /// </summary>
    public interface IStateManager
    {
        /// <summary>
        /// 数据库访问对象
        /// </summary>
        DBOperator DB { get; set; }

        /// <summary>
        /// 获取审核通过的下一状态
        /// </summary>
        int GetNextStateIfPast(int systemID, string moduleCode, int currentOperateState);

        /// <summary>
        /// 获取审核不通过的下一状态
        /// </summary>
        /// <param name="systemID"></param>
        /// <param name="moduleCode"></param>
        /// <param name="currentOperateState"></param>
        /// <returns></returns>
        int GetNextStateIfRejected(int systemID, string moduleCode, int currentOperateState);

        /// <summary>
        /// 获取撤销操作后的状态下可进行的撤销操作
        /// </summary>
        /// <param name="systemID"></param>
        /// <param name="moduleCode"></param>
        /// <param name="currentCancelState"></param>
        /// <returns></returns>
        int GetNextCancelStateWhenCancelled(int systemID, string moduleCode, int currentCancelState);

        /// <summary>
        /// 获取撤销操作后的状态下可进行的操作
        /// </summary>
        /// <param name="systemID"></param>
        /// <param name="moduleCode"></param>
        /// <param name="currentCancelState"></param>
        /// <returns></returns>
        int GetNextOperateStateWhenCancelled(int systemID, string moduleCode, int currentCancelState);

        /// <summary>
        /// 获取撤销操作后，数据记录接下来的总体状态情况
        /// </summary>
        RecordState GetFollowingStateWhenCancelled(int systemID, string moduleCode, int currentCancelState);
    }

    /// <summary>
    /// 业务记录的相关状态结构
    /// </summary>
    public struct RecordState
    {
        public int DataState;
        public int OperateState;
        public int CancelState;
    }
}
