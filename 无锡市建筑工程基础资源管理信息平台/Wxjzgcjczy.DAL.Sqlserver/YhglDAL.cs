using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bigdesk8.Data;
using Bigdesk8;
using System.Data;
using Wxjzgcjczy.Common;



namespace Wxjzgcjczy.DAL.Sqlserver
{
    public class YhglDAL
    {
        public DBOperator DB { get; set; }
        #region 表结构
        public DataTable GetSchema_User()
        {
            string sql = "select * from g_user where 1=2 ";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }
        public DataTable GetSchema_Role()
        {
            string sql = "select * from g_Role where 1=2 ";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }
        public DataTable GetSchema_UseRole()
        {
            string sql = @"select * from g_UserRole where 1=2  ";
            return DB.ExeSqlForDataTable(sql, null, "t");
        }
        #endregion

        public Int64 GetNewID_User()
        {
            string sql = "select max(UserID) from g_user ";
            return (DB.ExeSqlForString(sql, null).ToInt64(0) + 1);
        }
        public Int64 GetNewID_Role()
        {
            string sql = "select max(RoleID) from g_Role ";
            return (DB.ExeSqlForString(sql, null).ToInt64(0) + 1);
        }


        #region 列表
        public DataTable Retrieve_g_user(AppUser userInfo, FilterTranslator ft, int pageSize, int pageIndex, string orderby, out int allRecordCount)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = "select * from g_user where USERSTATE=@UserState and  ";

            DALHelper.GetSearchClause(ref sp, ft);
            sql += ft.CommandText;
            sp.Add("@UserState", "正常用户");
            return DB.ExeSqlForDataTable(sql, sp, "t", orderby, pageSize, pageIndex, out allRecordCount);
        }
        public DataTable Retrieve_RoleRight_List(string userID,AppUser userInfo, FilterTranslator ft, int pageSize, int pageIndex, string orderby, out int allRecordCount)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
//            string sql = @"select a.*,b.ModuleName,c.OperateName from g_RoleRight a 
//inner join g_Module b on a.ModuleCode=b.ModuleCode
//inner join g_Operate c on a.OperateCode=c.OperateCode and a.ModuleCode=c.ModuleCode
//where RoleID=@RoleID and  ";
            string sql = "select *,(select count(*) from g_UserRole where UserID=@userID and RoleID=a.RoleID) as HasRole from  g_Role a where 1=1 and  ";
            sp.Add("@UserID", userID);
            DALHelper.GetSearchClause(ref sp, ft);
            sql += ft.CommandText;
            return DB.ExeSqlForDataTable(sql, sp, "t", orderby, pageSize, pageIndex, out allRecordCount);
        }

        public DataTable Get_RoleModules_List(AppUser userInfo, string roleID)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @"select  a.ModuleCode,a.ModuleName,(select COUNT(*) from g_RoleRight where ModuleCode=a.ModuleCode and RoleID=@RoleID) as Rights  
 from g_Module a order by a.Sort ";
            sp.Add("@RoleID", roleID);
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }
        public DataTable Get_UserRights_List(AppUser userInfo)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @"select a.*,b.ModuleName,c.OperateName from (
                          select distinct ModuleCode,OperateCode from  g_RoleRight where RoleID  in (
                            select RoleID from  g_UserRole where UserID=@UserID
                          )
                          ) as a 
                          inner join g_Module b on a.ModuleCode=b.ModuleCode 
                          inner join g_Operate c on a.OperateCode=c.OperateCode  and b. ModuleCode=c.ModuleCode
                          order by b.Sort, c.Sort ";
            sp.Add("@UserID", userInfo.UserID);
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }



        public DataTable Get_ModuleOperators_List(AppUser userInfo, string roleID,string moduleCode)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
//            string sql = @"select *,(select COUNT(*) from  g_RoleRight where ModuleCode=aaa.ModuleCode and  OperateCode=aaa.OperateCode and RoleID=@RoleID) as HasRight from (
//select b.ModuleName,b.Sort as ModuleSort, b.ModuleCode,a.OperateName,a.OperateCode,a.Sort as OperateSort
// from g_Operate a 
//left join g_Module  b on a.ModuleCode=b.ModuleCode
//) as aaa order by ModuleSort,OperateSort ";
            string sql = @"select a.ModuleCode,a.OperateName,a.OperateCode,(
select COUNT(*) from  g_RoleRight where ModuleCode=a.ModuleCode and  OperateCode=a.OperateCode and RoleID=@RoleID) as HasRight
from g_Operate a  where a.ModuleCode=@moduleCode
order by a.Sort ";

            sp.Add("@RoleID", roleID);
            sp.Add("@moduleCode", moduleCode);

            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable Get_UserRoles_List(string userID)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @"select * from g_UserRole where UserID=@UserID  ";
            sp.Add("@userID", userID);
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }

        public DataTable Get_RoleRights_List(AppUser userInfo,string roleId)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = @"select * from g_RoleRight where RoleID=@RoleID  ";
            sp.Add("@RoleID", roleId);
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }


        #endregion

        #region 单条记录
        public DataTable ReadUser(string userID)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = "select * from g_user where UserID=@UserID ";
            sp.Add("@UserID", userID);
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }
        public DataTable ReadRole(string roleID)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string sql = "select * from g_Role where RoleID=@RoleID ";
            sp.Add("@RoleID", roleID);
            return DB.ExeSqlForDataTable(sql, sp, "t");
        }
        #endregion

        #region 更新记录

        public bool SubmitUser(DataTable dt)
        {
            string sql = "select * from g_user where 1=2 ";
            return DB.Update(sql, null, dt);
        }
        public bool SubmitRole(DataTable dt)
        {
            string sql = "select * from g_Role where 1=2 ";
            return DB.Update(sql, null, dt);
        }
        public bool SubmitUserRole(DataTable dt)
        {
            string sql = "select * from g_UserRole where 1=2 ";
            return DB.Update(sql, null, dt);
        }
        public bool SubmitRoleRight(DataTable dt)
        {
            string sql = "select * from g_RoleRight where 1=2 ";
            return DB.Update(sql, null, dt);
        }
        #endregion






    }
}
