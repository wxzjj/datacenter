/*工作流模板表*/ 
if exists (select * from dbo.sysobjects where name = 'g_BusinessWorkflowTemplate')
drop table g_BusinessWorkflowTemplate
go

CREATE TABLE  g_BusinessWorkflowTemplate
(
SystemName 		varchar(200)  	not null,/*系统名称*/
ModuleName      varchar(100)    not null,/*模块名称*/
CategoryName    varchar(100)    not null,/*种类名称*/

TemplateID			varchar(50)			not null,	/*模板编号GUID号*/
TemplateContent		varchar(max)		not null,	/*模板内容*/
TemplateDesc		varchar(max)		not null,	/*模板说明*/

CreateDateTime			datetime			not null,	/*创建日期时间*/
ModifyDateTime			datetime			not null,	/*最后修改日期时间*/
primary key(TemplateID)
)
Go

select * from g_BusinessWorkflowTemplate
