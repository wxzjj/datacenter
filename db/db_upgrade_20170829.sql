USE [WJSJZX]
GO

/*为人员专业明细增加更新时间 */
ALTER TABLE [dbo].[UEPP_Ryjbxx] ADD UpdateTime datetime NULL;
ALTER TABLE [dbo].[UEPP_Ryzymx] ADD UpdateTime datetime NULL;
ALTER TABLE [dbo].[UEPP_Ryzs] ADD UpdateTime datetime NULL;
ALTER TABLE [dbo].[UEPP_Ryzyzg] ADD UpdateTime datetime NULL;

/* 项目登记增加补充字段*/
CREATE TABLE [dbo].[TBProjectAdditionalInfo](
	[PKID] [varchar](50) NOT NULL,
	[prjnum] [varchar](20) NOT NULL,
	[prjpassword] [varchar](8) NULL,
	[gyzzpl] [decimal](15, 4) NULL,
	[dzyx] [varchar](50) NULL,
	[lxr] [varchar](50) NULL,
	[yddh] [varchar](50) NULL,
	[xmtz] [decimal](15, 4) NULL,
	[gytze] [decimal](15, 4) NULL,
	[gytzbl] [decimal](15, 4) NULL,
	[lxtzze] [decimal](15, 4) NULL,
	[sbdqbm] [varchar](6) NOT NULL,
	[programme_address] [varchar](500) NULL,
	[createtime] [datetime] NOT NULL,
	[updatetime] [datetime] NOT NULL,
	[datastate] [int] NULL,
	[updateuser] [varchar](50) NULL,
    Primary key(PKID)
)
