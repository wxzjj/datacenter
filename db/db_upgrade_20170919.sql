USE [WJSJZX]
GO

/* 增加申报提交时间 */
alter table [dbo].[Ap_ajsbb] add updateDate [datetime] NULL;
alter table [dbo].[Ap_zjsbb] add updateDate [datetime] NULL;

