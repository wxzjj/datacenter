USE [WJSJZX]
GO

/* 增加来源，更新时间 */
alter table [dbo].[xzcf] add source [varchar](30) NULL;
alter table [dbo].[xzcf] add updateDate [datetime] NULL;
alter table [dbo].[xzcf] alter column prjNum [varchar](30) NULL;

