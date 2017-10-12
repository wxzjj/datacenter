use WJSJZX;
alter table [dbo].[Ap_ajsbb_dwry] add [zyzgdj] [varchar](100) NULL;
alter table [dbo].[Ap_zjsbb_dwry] add [zyzgdj] [varchar](100) NULL;

alter table [dbo].[Ap_ajsbjg] alter column [jdzch] varchar(100) null;
alter table [dbo].[Ap_zjsbjg] alter column [jdzch] varchar(100) null;
alter table [dbo].[Ap_zjsbjg_dwgc] alter column [dwjdzch] varchar(100) null;
alter table [dbo].[Ap_zjsbjg_dwgc] alter column [dwgcbm] varchar(100) null;