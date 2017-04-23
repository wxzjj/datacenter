/*用户表*/ 
if exists (select * from dbo.sysobjects where name = 'g_User')
drop table g_User
go

create table g_User
(
UserID				int			not null,/*用户编号*/
UserName 			varchar(200)  	not null,/*用户名称*/
LoginName 			varchar(100)  	not null,/*登录名称*/
LoginPassword 		varchar(50)  	not null,/*登录密码*/
primary key(UserID)
)
go

/*角色表，每个用户都有匿名用户角色*/ 
if exists (select * from dbo.sysobjects where name = 'g_Role')
drop table g_Role
go

create table g_Role
(
RoleID				int			not null,/*角色编号*/
RoleName 			varchar(100)  	not null,/*角色名称*/
RoleDesc 			varchar(200)  	not null,/*角色描述*/
Sort	 			int			not null,/*排序字段*/
primary key(RoleID)
)
go

/*用户角色表*/
if exists (select * from dbo.sysobjects where name = 'g_UserRole')
drop table g_UserRole
go

create table g_UserRole
(
UserID				int			not null,/*用户编号*/
RoleID				int			not null,/*角色编号*/
primary key(UserID,RoleID)
)
go

/*系统表*/
if exists (select * from dbo.sysobjects where name = 'g_System')
drop table g_System
go

create table g_System
(
SystemID		int					not null,/*系统编号*/
SystemName		varchar(100)		not null,/*系统名称*/
SystemDesc		varchar(200)		not null,/*系统描述*/
Sort	 		int					not null,/*排序字段*/
primary key(SystemID)
)
go

/*模块表*/
if exists (select * from dbo.sysobjects where name = 'g_Module')
drop table g_Module
go

create table g_Module
(
SystemID		int					not null,/*系统编号*/
ModuleID        int identity(1,1)   not null,/*模块编号*/
ModuleCode		varchar(100)		not null,/*模块代码*/
ModuleName		varchar(100)		not null,/*模块名称*/
ModuleDesc		varchar(200)		not null,/*模块描述*/
Sort	 		int					not null,/*排序字段*/
primary key(ModuleID)
)
go

/*操作表*/
if exists (select * from dbo.sysobjects where name = 'g_Operate')
drop table g_Operate
go

create table g_Operate
(
SystemID		int					not null,/*系统编号*/
OperateID		int identity(1,1)   not null,/*操作编号*/
OperateCode		varchar(100)		not null,/*操作代码*/
OperateName		varchar(100)		not null,/*操作名称*/
OperateDesc		varchar(200)		not null,/*操作描述*/
Sort	 		int					not null,/*排序字段*/
primary key(OperateID)
)
go

/*业务数据分类表*/
if exists (select * from dbo.sysobjects where name = 'g_DataType')
drop table g_DataType
go

create table g_DataType
(
SystemID			int                 not null,/*系统编号*/
DataTypeID			int identity(1,1)   not null,/*业务数据分类编号*/
DataTypeCode		varchar(100)		not null,/*业务数据分类代码*/
DataTypeName		varchar(100)		not null,/*业务数据分类名称*/
DataTypeDesc		varchar(200)		not null,/*业务数据分类描述*/
Sort	 			int					not null,/*排序字段*/
primary key(DataTypeID)
)
go

/*角色权限表*/
if exists (select * from dbo.sysobjects where name = 'g_RoleRight')
drop table g_RoleRight
go

create table g_RoleRight
(
RoleID			int			not null,/*角色编号*/
SystemID		int			not null,/*系统编号*/
ModuleID        int         not null,/*模块编号*/
OperateID		int			not null,/*操作编号*/
DataTypeID		int			not null,/*业务数据分类编号*/
primary key(RoleID,SystemID,ModuleID,OperateID,DataTypeID)
)
go


/*视图表*/
/*用户特性视图*/
if exists (select table_name from information_schema.views where table_name = 'g_v_UserAttribute')
drop view g_v_UserAttribute
go

create view g_v_UserAttribute(UserID,UserName,LoginName,AttributeID,AttributeName,AttributeValue,AttributeSort)
AS
select a.UserID,a.UserName,a.LoginName,c.AttributeID,c.AttributeName,b.AttributeValue,c.Sort from g_User as a
inner join g_UserAttribute as b on b.UserID=a.UserID
inner join g_Attribute as c on c.AttributeID=b.AttributeID
go

/*用户角色视图*/
if exists (select table_name from information_schema.views where table_name = 'g_v_UserRole')
drop view g_v_UserRole
go

create view g_v_UserRole(UserID,UserName,LoginName,RoleID,RoleName,RoleSort)
AS
select a.UserID,a.UserName,a.LoginName,c.RoleID,c.RoleName,c.Sort from g_User as a
inner join g_UserRole as b on b.UserID=a.UserID
inner join g_Role as c on c.RoleID=b.RoleID
go

/*角色权限视图*/
if exists (select table_name from information_schema.views where table_name = 'g_v_RoleRight')
drop view g_v_RoleRight
go

create view g_v_RoleRight(
RoleID,RoleName,RoleSort,
SystemID,SystemName,SystemSort,
ModuleID,ModuleCode,ModuleName,ModuleSort,
OperateID,OperateCode,OperateName,OperateSort,
DataTypeID,DataTypeCode,DataTypeName,DataTypeSort
)
AS
select 
a.RoleID,a.RoleName,a.Sort,
c.SystemID,c.SystemName,c.Sort,
d.ModuleID,d.ModuleCode,d.ModuleName,d.Sort,
e.OperateID,e.OperateCode,e.OperateName,e.Sort,
f.DataTypeID,f.DataTypeCode,f.DataTypeName,f.Sort
from g_Role as a
inner join g_RoleRight as b on b.RoleID=a.RoleID
inner join g_System as c on c.SystemID=b.SystemID
inner join g_Module as d on d.SystemID=b.SystemID and d.ModuleID=b.ModuleID
inner join g_Operate as e on e.SystemID=b.SystemID and e.OperateID=b.OperateID
inner join g_DataType as f on f.SystemID=b.SystemID and f.DataTypeID=b.DataTypeID
go

/*用户角色权限视图*/
if exists (select table_name from information_schema.views where table_name = 'g_v_UserRoleRight')
drop view g_v_UserRoleRight
go

create view g_v_UserRoleRight(
UserID,UserName,LoginName,
RoleID,RoleName,RoleSort,
SystemID,SystemName,SystemSort,
ModuleID,ModuleCode,ModuleName,ModuleSort,
OperateID,OperateCode,OperateName,OperateSort,
DataTypeID,DataTypeCode,DataTypeName,DataTypeSort
)
AS
select
a.UserID,a.UserName,a.LoginName,
b.RoleID,b.RoleName,b.RoleSort,
b.SystemID,b.SystemName,b.SystemSort,
b.ModuleID,b.ModuleCode,b.ModuleName,b.ModuleSort,
b.OperateID,b.OperateCode,b.OperateName,b.OperateSort,
b.DataTypeID,b.DataTypeCode,b.DataTypeName,b.DataTypeSort
from g_v_UserRole as a
inner join g_v_RoleRight as b on b.RoleID=a.RoleID
go



/*内置用户角色*/
/*用户表*/
/*预置GUEST、ADMIN、SPARKSOFT这几个用户*/
delete from g_User where LoginName='GUEST' or LoginName='ADMIN' or LoginName='SPARKSOFT'
go

Insert into g_User(UserID, LoginName, LoginPassword, UserName)values(1, 'GUEST', '', '匿名用户')
Insert into g_User(UserID, LoginName, LoginPassword, UserName)values(2, 'ADMIN', '123456', '系统管理员')
Insert into g_User(UserID, LoginName, LoginPassword, UserName)values(3, 'SPARKSOFT', '123456', '群耀软件')
go

/*特性表*/
/*预置建设单位、招标代理机构、施工企业、监理企业这几个特性*/
delete from g_Attribute where AttributeID between 1 and 105
go

insert into g_Attribute(AttributeID,AttributeName,AttributeDesc,Sort)values(1,'系统管理员','负责系统运行维护',1)
insert into g_Attribute(AttributeID,AttributeName,AttributeDesc,Sort)values(101,'管理用户','管理用户',101)
insert into g_Attribute(AttributeID,AttributeName,AttributeDesc,Sort)values(102,'建设单位','建设单位',102)
insert into g_Attribute(AttributeID,AttributeName,AttributeDesc,Sort)values(103,'招标代理机构','招标代理企业',103)
insert into g_Attribute(AttributeID,AttributeName,AttributeDesc,Sort)values(104,'施工企业','建筑施工企业',104)
insert into g_Attribute(AttributeID,AttributeName,AttributeDesc,Sort)values(105,'监理企业','监理企业',105)
go

/*用户特性*/
delete from g_UserAttribute where ((UserID=2 and AttributeID in (1,101)) or (UserID=3 and AttributeID in (1,101)))
go

insert into g_UserAttribute(UserID, AttributeID, AttributeValue)values(2, 1, '')
insert into g_UserAttribute(UserID, AttributeID, AttributeValue)values(2, 101, '')
insert into g_UserAttribute(UserID, AttributeID, AttributeValue)values(3, 1, '')
insert into g_UserAttribute(UserID, AttributeID, AttributeValue)values(3, 101, '')
go

/*角色表*/
/*预置匿名用户、系统管理员这几种角色*/
delete from g_Role where RoleName='匿名用户' or RoleName='系统管理员'
go

insert into g_Role(RoleID, RoleName, RoleDesc, Sort)values(1, '匿名用户', '未登陆系统默认设置为匿名用户状态，在网站上查看不需要权限的信息就可以把浏览权限设置给匿名用户角色,注意:所有用户都有匿名用户角色',1)
insert into g_Role(RoleID, RoleName, RoleDesc, Sort)values(2, '系统管理员', '管理网站平台运行的基础设置',2)
go

/*用户角色表*/
delete from g_UserRole 
where (UserID=1 and RoleID=1) 
or (UserID=2 and RoleID=1) or (UserID=2 and RoleID=2)
or (UserID=3 and RoleID=1) or (UserID=3 and RoleID=2)
go

insert into g_UserRole(UserID, RoleID) values(1, 1)
insert into g_UserRole(UserID, RoleID) values(2, 1)
insert into g_UserRole(UserID, RoleID) values(2, 2)
insert into g_UserRole(UserID, RoleID) values(3, 1) 
insert into g_UserRole(UserID, RoleID) values(3, 2)
go
