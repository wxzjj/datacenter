use WJSJZX;

/* 安监申报gis信息表*/
CREATE TABLE [dbo].[Ap_ajsbb_gis](
    [pointId] [varchar](50) NOT NULL,/*记录主键*/
	[uuid] [varchar](50) NOT NULL,/*安监申报表编号,由一站式申报系统自动产生*/
	[modified] [datetime] NOT NULL, /*最后修改日期时间 */
	[mapLat] [decimal](18, 6) NULL,
	[mapLng] [decimal](18, 6) NULL,
	[pointOrder] [int] NULL, /*坐标点顺序 */
	[pointTeam] [int] NULL, /*坐标点分组*/
	[pointType] [int] NULL, /* 坐标点类型*/
	[updateFlag] [char](1) NULL, /* 数据更新标识:U新增或更新；D删除 */
	[createTime] [datetime] NULL, /*四库修改日期时间 */
	[updateTime] [datetime] NULL, /*四库修改日期时间 */
	[updateUser] [varchar](50) NULL,
    Primary key(pointId)
)

/* 更新权限表 */
alter table [WJSJZX].[dbo].[uepp_InterfaceUser] add Has_Ap_ajsbb_gis_Write int not null default 0;
update [WJSJZX].[dbo].[uepp_InterfaceUser] set Has_Ap_ajsbb_gis_Write = 1 where Id = 1;

/* 安监申报项目现场监督信息表*/
CREATE TABLE [WJSJZX].[dbo].[Ap_ajsbb_info](
	[uuid] [varchar](50) NOT NULL,/*安监申报表编号,由一站式申报系统自动产生*/
	[superviseStatus] [int] NULL,/* 现场监督状态，1.新增待审 2.在建 3.停工 4.节假日停工 5.终止安监 */
	[modified] [datetime] NULL, /*最后修改日期时间 */
	[createTime] [datetime] NULL, /*四库创建日期时间 */
	[updateTime] [datetime] NULL, /*四库修改日期时间 */
	[updateUser] [varchar](50) NULL,
	[updateFlag] [char](1) NULL, 
	Primary key(uuid)
)