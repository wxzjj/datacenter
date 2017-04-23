using System;
using Bigdesk8.Data;

namespace Bigdesk8.Business.WorkflowTemplateManager
{
    /// <summary>
    /// 工作流模板信息管理接口
    /// </summary>
    public interface IWorkflowTemplateManager
    {
        /// <summary>
        /// 数据库访问对象
        /// </summary>
        DBOperator DB { get; set; }

        /// <summary>
        /// 添加工作流模板信息
        /// </summary>
        /// <param name="workflowTemplate">工作流模板信息</param>
        /// <returns>返回工作流模板信息</returns>
        BusinessWorkflowTemplate AddWorkflowTemplate(BusinessWorkflowTemplate workflowTemplate);

        /// <summary>
        /// 更新工作流模板信息
        /// </summary>
        /// <param name="workflowTemplate">工作流模板信息</param>
        void UpdateWorkflowTemplate(BusinessWorkflowTemplate workflowTemplate);

        /// <summary>
        /// 获得工作流模板信息
        /// </summary>
        /// <param name="templateID">工作流模板信息编号</param>
        /// <returns>返回工作流模板信息</returns>
        BusinessWorkflowTemplate GetWorkflowTemplate(Guid templateID);

        /// <summary>
        /// 获得工作流模板信息
        /// </summary>
        /// <param name="templateID">工作流模板信息编号</param>
        /// <returns>返回工作流模板信息</returns>
        BusinessWorkflowTemplate GetWorkflowTemplate(string templateID);
    }

    /// <summary>
    /// 工作流模板信息类，创建时，所有属性不能为NULL
    /// </summary>
    public class BusinessWorkflowTemplate
    {
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
        /// 模板编号GUID号
        /// </summary>
        public Guid TemplateID { get; internal set; }

        /// <summary>
        /// 模板内容
        /// </summary>
        public string TemplateContent { get; set; }

        /// <summary>
        /// 模板说明
        /// </summary>
        public string TemplateDesc { get; set; }

        /// <summary>
        /// 创建日期时间，格式为yyyy-MM-dd HH:mm:ss
        /// </summary>
        public DateTime CreateDateTime { get; internal set; }

        /// <summary>
        /// 最后修改日期时间，格式为yyyy-MM-dd HH:mm:ss
        /// </summary>
        public DateTime ModifyDateTime { get; internal set; }
    }
}
