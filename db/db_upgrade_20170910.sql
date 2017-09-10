USE [WJSJZX]
GO

/*
DROP TABLE [dbo].[Ap_ajsbb];
DROP TABLE [dbo].[Ap_ajsbb_ht];
DROP TABLE [dbo].[Ap_ajsbb_dwry];
DROP TABLE [dbo].[Ap_ajsbb_clqd];
DROP TABLE [dbo].[Ap_ajsbb_hjssjd];
DROP TABLE [dbo].[Ap_ajsbb_wxyjdgcqd];
DROP TABLE [dbo].[Ap_ajsbb_cgmgcqd];

*/

/* 一站式平台对接帐号表 */
CREATE TABLE [dbo].[Ap_api_user](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[deptCode] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[deptName] [varchar](50) NOT NULL,
	[countryCode] [varchar](10) NULL,
	[deptType] [varchar](2) NULL,
	Primary key(id)
);

INSERT INTO [dbo].[Ap_api_user]
           ([deptCode]
           ,[password]
           ,[deptName]
           ,[countryCode]
		   ,[deptType])
     VALUES
           (N'AJ320204-1', N'F1FFAA5D-F915-4328-BADC-CE1EF800CA69',N'无锡市北塘区建筑工程安全监督站',N'320204',N'AJ')
		   ,(N'AJ320211-1', N'1B92390A-FA25-4459-B9CB-19E7E550A887',N'无锡市滨湖区建筑工程安全监督站',N'320211',N'AJ')
		   ,(N'AJ320202-1', N'4C8C371E-98D9-4B46-ABF9-05F494F23C47',N'无锡市崇安区建筑工程安全监督站',N'320202',N'AJ')
		   ,(N'AJ320206-1', N'09F8F79C-93D2-4BD8-825F-70C4130E261E',N'无锡市惠山区建筑工程安全监督站',N'320206',N'AJ')
		   ,(N'AJ320201-1', N'0DB725E6-9D50-4C05-B2A9-944E903FB3B0',N'无锡市建设工程安全监督站',N'320201',N'AJ')
		   ,(N'AJ320281-1', N'F1FFAA5D-F915-4328-BADC-CE1EF800CA69',N'无锡市江阴市建筑工程安全监督站',N'320281',N'AJ')
		   ,(N'AJ320208-1', N'A5567E3C-831E-4797-B5B0-72D2A500578D',N'无锡市梁溪区建筑管理站',N'320213',N'AJ')
		   ,(N'AJ320203-1', N'4F414446-35E7-4FE2-90DC-1AAF83B4416D',N'无锡市南长区建筑工程安全监督站',N'320203',N'AJ')
		   ,(N'AJ320205-1', N'56559114-68E9-4AF2-B5F9-A7B4BDF6FBD4',N'无锡市锡山区建筑工程安全监督站',N'320205',N'AJ')
		   ,(N'AJ320207-1', N'42A0034B-1F11-465D-8B5B-DC92C42D1D00',N'无锡市新吴区建筑工程安全监督站',N'320207',N'AJ')
		   ,(N'AJ320209-1', N'E5891789-03DA-438F-AC4F-AB637C0BCC44',N'无锡新区建设工程质量安全监督站',N'320214',N'AJ')
		   ,(N'3202011', N'C3D3F942-DE48-4949-8AE7-DFB8FBB1BEC5',N'无锡市建设工程质量监督站',N'320201',N'ZJ')
		   ,(N'3202810', N'713CFB27-FD7E-4B32-BD3B-DC954C30D2B3',N'江阴市建设工程质量安全监督站',N'320281',N'ZJ')
		   ;


/* 安监申报表*/
CREATE TABLE [dbo].[Ap_ajsbb](
	[uuid] [varchar](50) NOT NULL,/*安监申报表编号,由一站式申报系统自动产生*/
	[xmmc] [varchar](100) NOT NULL,/*安监项目名称 */
	[PrjNum] [varchar](20) NOT NULL, /* 项目编号 */
	[PrjName] [varchar](500) NOT NULL,
	[Ajjgmc] [varchar](200) NOT NULL,
	[AjCorpCode] [varchar](18) NOT NULL,
    [PrjSize] [varchar](500) NULL,
	[EconCorpName] [varchar](200) NOT NULL,
	[EconCorpCode] [varchar](15) NOT NULL,
	[PrjApprovalNum] [varchar](255) NULL,
	[BuldPlanNum] [varchar](255) NULL,
	[ProjectPlanNum] [varchar](255) NULL,
	[CityNum] [varchar](6) NOT NULL,
	[CountyNum] [varchar](6) NOT NULL,
	[PrjTypeNum] [varchar](255) NOT NULL,
	[sPrjTypeNum] [varchar](4) NULL,
	[PrjFunctionNum] [varchar](10) NOT NULL,
	[sbr] [varchar](50) NOT NULL,
	[sbryddh] [varchar](15) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[sfzps] [varchar](50) NULL,
	[sfbz] [varchar](50) NULL,
	[jdz] [decimal](18, 6) NULL,
	[wdz] [decimal](18, 6) NULL,
	[mj] [decimal](18, 4) NULL,
	[zj] [decimal](18, 4) NOT NULL,
	[jgcc] [varchar](200)  NULL,
	[sbmb] [varchar](100)  NULL,
	[sfjk] [varchar](50)  NULL,
	[sgxkz] [varchar](50)  NULL,
	[UpdateFlag] [char](1) NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[UpdateUser] [varchar](50) NULL,
    Primary key(uuid)
)

/* 安监申报表_相关合同*/
CREATE TABLE [dbo].[Ap_ajsbb_ht](
	[uuid] [varchar](50) NOT NULL, /* 安监申报表编号 */
	[RecordNum] [varchar](50) NOT NULL, /* 合同备案编码 */
	[ContractTypeNum] [varchar](3) NOT NULL, /* 合同类别,详见数据字典表TBCONTRACTTYPEDIC */
	[ContractMoney] [decimal](18, 6) NOT NULL, /* 合同金额（万元） */
	[CorpCode] [varchar](18) NOT NULL, /* 承包单位组织机构代码 */
	[CorpName] [varchar](200) NOT NULL, /* 承包单位名称 */
	[PrjSize] [varchar](500) NULL, /* 合同建设规模 */
    [xmfzr] [varchar](50) NOT NULL, /* 项目负责人 */
	[xmfzrsfzh] [varchar](18) NOT NULL, /* 项目负责人身份证号 */
    Primary key(uuid, RecordNum)
)


/* 安监申报表_单位人员 */
CREATE TABLE [dbo].[Ap_ajsbb_dwry](
	[uuid] [varchar](50) NOT NULL, /* 安监申报表编号 */
	[idCard] [varchar](18) NOT NULL, /* 身份证号码 */
	[dwlx] [varchar](20) NOT NULL, /* 单位类别,分为建设单位、勘察单位、设计单位、施工单位、监理单位、工程总承包单位 */
	[CorpCode] [varchar](18) NOT NULL, /* 单位组织机构代码 */
	[CorpName] [varchar](255) NOT NULL, /* 承包单位名称 */
	[zzzs] [varchar](500) NULL, /* 资质证书编号 */
	[zzlxdj] [varchar](500) NULL, /* 资质类型和等级 */
	[zzyxq] [varchar](500) NULL, /* 资质有效期 */
    [xm] [varchar](50) NOT NULL, /* 姓名 */
	[gw] [varchar](50) NOT NULL, /* 岗位 */
	[mp] [varchar](50) NULL, /* 手机号码 */
	[zgzh] [varchar](50) NULL, /* 资格类型及证号 */
	[zy] [varchar](50) NULL, /* 专业 */
	[jhjcsj] [datetime] NULL, /* 计划进场时间 */
	[jhccsj] [datetime] NULL, /* 计划出场时间 */
	[lhtsx] [varchar](1) NULL, /* 联合体属性, 0非联合体, 1联合体主体单位, 2联合体合作单位 */
    Primary key(uuid, idCard)
)

/* 安监申报表_材料清单 */
CREATE TABLE [dbo].[Ap_ajsbb_clqd](
	[uuid] [varchar](50) NOT NULL, /* 安监申报表编号 */
	[xh] [int] NOT NULL, /* 序号 */
	[sbzl] [varchar](255) NOT NULL, /* 申报资料 */
	[zshth] [varchar](255) NOT NULL, /* 证书（合同）号 */
	[blrq] [varchar](50) NOT NULL, /* 办理日期 */
	[smjdz] [varchar](255) NULL, /* 扫描件服务器地址 */
	[smjmc] [varchar](100) NULL, /* 扫描件文件名称 */
    Primary key(uuid, xh)
)

/* 安监申报表_环境及地下设施交底项目 */
CREATE TABLE [dbo].[Ap_ajsbb_hjssjd](
	[uuid] [varchar](50) NOT NULL, /* 安监申报表编号 */
	[xh] [int] NOT NULL, /* 序号 */
	[jdxm] [varchar](255) NOT NULL, /* 交底项目 */
	[jdqk] [varchar](255) NOT NULL, /* 交底情况 */
    Primary key(uuid, xh)
)

/* 安监申报表_危险源较大工程清单 */
CREATE TABLE [dbo].[Ap_ajsbb_wxyjdgcqd](
	[uuid] [varchar](50) NOT NULL, /* 安监申报表编号 */
	[fbfxgc] [varchar](50) NOT NULL, /* 分部分项工程 */
	[gcnr] [varchar](255) NOT NULL, /* 工程内容 */
	[yjsssj] [datetime] NULL, /* 预计实施时间 */
    Primary key(uuid, fbfxgc)
)

/* 安监申报表_超大规模危险源工程清单 */
CREATE TABLE [dbo].[Ap_ajsbb_cgmgcqd](
	[uuid] [varchar](50) NOT NULL, /* 安监申报表编号 */
	[fbfxgc] [varchar](50) NOT NULL, /* 分部分项工程 */
	[gcnr] [varchar](255) NOT NULL, /* 工程内容 */
	[yjsssj] [datetime] NULL, /* 预计实施时间 */
    Primary key(uuid, fbfxgc)
)


/* 字典表 */

/* tbPrjTypeDic项目分类字典表 
ALTER TABLE [dbo].[tbPrjTypeDic] ALTER COLUMN  Code char(10) NOT NULL;
INSERT [dbo].[tbPrjTypeDic] ([Code], [CodeInfo]) VALUES 
( N'0101', N'房地产开发项目'),
( N'0102', N'保障性住房'),
( N'010201', N'廉租房'),
( N'010202', N'公租房'),
( N'010203', N'经济适应房'),
( N'010299', N'其他保障房'),
( N'0199', N'其他房屋工程'),
( N'0201', N'道路工程'),
( N'0202', N'管线工程'),
( N'0299', N'其他市政工程'),
( N'9901', N'装饰装修工程'),
( N'9902', N'设备安装工程'),
( N'9903', N'管线敷设工程'),
( N'9904', N'电力工程'),
( N'9905', N'城市轨道工程'),
( N'9906', N'石油化工工程');*/

