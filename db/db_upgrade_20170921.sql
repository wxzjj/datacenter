use WJSJZX;
alter table [dbo].[Ap_ajsbb_wxyjdgcqd] alter column yjsssj varchar(50) null;
alter table [dbo].[Ap_ajsbb_cgmgcqd] alter column yjsssj varchar(50) null;

alter table [dbo].[Ap_ajsbb] alter column [xmmc] [varchar](500) NOT NULL;
alter table [dbo].[Ap_zjsbb] alter column [xmmc] [varchar](500) NOT NULL;