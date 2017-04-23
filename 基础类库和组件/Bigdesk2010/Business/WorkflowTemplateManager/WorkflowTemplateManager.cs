using System;
using System.Configuration;
using System.Data;
using Bigdesk2010.Data;

namespace Bigdesk2010.Business.WorkflowTemplateManager
{
    /// <summary>
    /// 工作流模板信息管理
    /// </summary>
    public static class WorkflowTemplateManagerFactory
    {
        /// <summary>
        /// 获取工作流模板信息管理器
        /// </summary>
        /// <param name="workflowTemplateName">workflowTemplateName</param>
        /// <returns>返回指定的工作流模板信息管理器</returns>
        public static IWorkflowTemplateManager CreateWorkflowTemplateManager(string workflowTemplateName)
        {
            return new DBWorkflowTemplateManager();
        }
    }

    /// <summary>
    /// 工作流模板信息管理
    /// </summary>
    internal class DBWorkflowTemplateManager : IWorkflowTemplateManager
    {
        #region IWorkflowTemplateManager 成员

        /// <summary>
        /// 数据库访问对象
        /// </summary>
        public DBOperator DB { get; set; }

        /// <summary>
        /// 添加工作流模板信息
        /// </summary>
        /// <param name="workflowTemplate">工作流模板信息</param>
        /// <returns>返回工作流模板信息</returns>
        public BusinessWorkflowTemplate AddWorkflowTemplate(BusinessWorkflowTemplate workflowTemplate)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@SystemName", workflowTemplate.SystemName);
            spc.Add("@ModuleName", workflowTemplate.ModuleName);
            spc.Add("@CategoryName", workflowTemplate.CategoryName);

            spc.Add("@TemplateContent", workflowTemplate.TemplateContent);
            spc.Add("@TemplateDesc", workflowTemplate.TemplateDesc);

            workflowTemplate.CreateDateTime = DateTime.Now;
            workflowTemplate.ModifyDateTime = DateTime.Now;

            spc.Add("@CreateDateTime", workflowTemplate.CreateDateTime);
            spc.Add("@ModifyDateTime", workflowTemplate.ModifyDateTime);

            workflowTemplate.TemplateID = getNewTemplateID();
            spc.Add("@TemplateID", workflowTemplate.TemplateID.ToString());

            string sql = "insert into g_BusinessWorkflowTemplate(SystemName, ModuleName, CategoryName, TemplateID, TemplateContent, TemplateDesc, CreateDateTime, ModifyDateTime)"
                + "values(@SystemName, @ModuleName, @CategoryName, @TemplateID, @TemplateContent, @TemplateDesc, @CreateDateTime, @ModifyDateTime)";
            DB.ExecuteNonQuerySql(sql, spc);

            return workflowTemplate;
        }

        private Guid getNewTemplateID()
        {
            Guid g = Guid.NewGuid();
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@TemplateID", g.ToString());
            if (DB.ExeSqlForObject("select count(1) from g_BusinessWorkflowTemplate where TemplateID=@TemplateID", spc).ToInt32() > 0)
                return getNewTemplateID();
            else
                return g;
        }

        /// <summary>
        /// 更新工作流模板信息
        /// </summary>
        /// <param name="workflowTemplate">工作流模板信息</param>
        public void UpdateWorkflowTemplate(BusinessWorkflowTemplate workflowTemplate)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@SystemName", workflowTemplate.SystemName);
            spc.Add("@ModuleName", workflowTemplate.ModuleName);
            spc.Add("@CategoryName", workflowTemplate.CategoryName);

            spc.Add("@TemplateContent", workflowTemplate.TemplateContent);
            spc.Add("@TemplateDesc", workflowTemplate.TemplateDesc);

            workflowTemplate.ModifyDateTime = DateTime.Now;

            spc.Add("@ModifyDateTime", workflowTemplate.ModifyDateTime);

            spc.Add("@TemplateID", workflowTemplate.TemplateID.ToString());

            string sql = "update g_BusinessWorkflowTemplate set SystemName=@SystemName, ModuleName=@ModuleName, CategoryName=@CategoryName, TemplateContent=@TemplateContent, TemplateDesc=@TemplateDesc, ModifyDateTime=@ModifyDateTime where TemplateID=@TemplateID";
            DB.ExecuteNonQuerySql(sql, spc);
        }

        /// <summary>
        /// 获得工作流模板信息
        /// </summary>
        /// <param name="templateID">工作流模板信息编号</param>
        /// <returns>返回工作流模板信息</returns>
        public BusinessWorkflowTemplate GetWorkflowTemplate(Guid templateID)
        {
            return GetWorkflowTemplate(templateID.ToString());
        }

        /// <summary>
        /// 获得工作流模板信息
        /// </summary>
        /// <param name="templateID">工作流模板信息编号</param>
        /// <returns>返回工作流模板信息</returns>
        public BusinessWorkflowTemplate GetWorkflowTemplate(string templateID)
        {
            string sql = "select * from g_BusinessWorkflowTemplate where TemplateID=@TemplateID";

            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@TemplateID", templateID);

            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "template");
            if (dt.Rows.Count <= 0) return null;

            return DataRow2WorkflowTemplate(dt.Rows[0]);
        }

        #endregion IWorkflowTemplateManager 成员

        private BusinessWorkflowTemplate DataRow2WorkflowTemplate(DataRow dr)
        {
            BusinessWorkflowTemplate template = new BusinessWorkflowTemplate();
            template.SystemName = dr["SystemName"].ToString();
            template.ModuleName = dr["ModuleName"].ToString();
            template.CategoryName = dr["CategoryName"].ToString();

            template.TemplateID = dr["TemplateID"].ToGuid();
            template.TemplateContent = dr["TemplateContent"].ToString();
            template.TemplateDesc = dr["TemplateDesc"].ToString();

            template.CreateDateTime = dr["CreateDateTime"].ToDateTime();
            template.ModifyDateTime = dr["ModifyDateTime"].ToDateTime();
            return template;
        }
    }
}
