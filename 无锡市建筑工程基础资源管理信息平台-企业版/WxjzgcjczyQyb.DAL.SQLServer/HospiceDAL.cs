using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Data;
using Bigdesk8;
using Bigdesk8.Data;
using Bigdesk8.Business;

namespace WxjzgcjczyQyb.DAL
{
    /// <summary>
    ///  数据访问层中无处可放的功能的收容所
    /// </summary>
    public class HospiceDAL
    {
        public DBOperator DB { get; set; }

        #region 用户管理
        public DataTable ReadUser(string loginName)
        {
            string sql = "select UserID,LoginName,UserName,UserType from g_user where LoginName=@LoginName";
            SqlParameterCollection spc = this.DB.CreateSqlParameterCollection();
            spc.Add("@LoginName", loginName);
            return DB.ExeSqlForDataTable(sql, spc, "t");
        }

        public DataTable ReadUser(int UserID)
        {
            string sql = "select UserID,LoginName,UserName,UserType from g_user where UserID=@UserID";
            SqlParameterCollection spc = this.DB.CreateSqlParameterCollection();
            spc.Add("@UserID", UserID);
            return DB.ExeSqlForDataTable(sql, spc, "t");
        }

        public DataTable ReadUserByzzjgdm(string zzjgdm)
        {
            string sql = "select * from sgtyk_qyjbqkb where replace(enterp_zzjgdm,'-','')=@zzjgdm";
            SqlParameterCollection spc = this.DB.CreateSqlParameterCollection();
            spc.Add("@zzjgdm", zzjgdm);
            return DB.ExeSqlForDataTable(sql, spc, "t");
        }

        //普通登录
        public DataTable ReadUser(string loginName, string password)
        {
            string sql = "select UserID,LoginName,UserName,UserType from g_user where LoginName=@loginName and LoginPassword=@password ";
            SqlParameterCollection spc = this.DB.CreateSqlParameterCollection();
            spc.Add("@loginName", loginName);
            spc.Add("@password", password);
            return DB.ExeSqlForDataTable(sql, spc, "t");
        }

        /// <summary>
        /// 获取施工企业企业相关信息
        /// </summary>
        /// <param name="userid"></param>
        public DataTable GetQyInfo(string UserID)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            p.Add("@UserID", UserID);

            string sql = "select UserID,LoginName,UserName,UserType from g_user where UserID=@UserID ";
            return DB.ExeSqlForDataTable(sql, p, "t");
        }

        public DataTable ReadUserByqyid(string UserID)
        {
            string sql = "select * from g_user where tykid = @tykid";
            SqlParameterCollection spc = this.DB.CreateSqlParameterCollection();
            spc.Add("@UserID", UserID);
            return DB.ExeSqlForDataTable(sql, spc, "t");
        }

        #endregion

        #region 代码表
        /// <summary>
        /// 获取一组代码信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetCode_CodeInfo(CodeType codeType, bool isKeyValue)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();

            string sql;
            switch (codeType)
            {
                case CodeType.模块代码表:
                    sql = " select moduleCode as Code,moduleName  as CodeInfo from d_sgtyk_modules";
                    break;
                case CodeType.人员科室:
                    sql = " select  Code,CodeInfo from WxjzgcjczyQyb_D_Code where CodeType ='人员科室'";
                    break;
               

                default:
                    sql = @"select a.Code,a.CodeInfo from WxjzgcjczyQyb_Code as a
                                where a.CodeType=@CodeType order by a.OrderID";
                    p.Add("@CodeType", codeType.ToString());
                    break;
            }

            var dt = DB.ExeSqlForDataTable(sql, p, "t");
            return dt;
        }

        /// <summary>
        /// 根据sql语句返回datatable
        /// </summary>
        /// <param name="Sql"></param>
        /// <returns></returns>
        public DataTable GetDTBySql(string Sql)
        {
            var dt = DB.ExeSqlForDataTable(Sql, null, "t");
            return dt;
        }

        /// <summary>
        /// 通过Code查询代码表
        /// </summary>
        /// <param name="codeType"></param>
        /// <returns></returns>
        public DataRow ReadCode(string code)
        {
            string sql = "select * from WxjzgcjczyQyb_Code where Code=@Code";
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@Code", code);
            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "t");
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        public DataTable GetCode(string parentCodeType, string parentCode, string codeType)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@ParentCodeType", parentCodeType);
            spc.Add("@ParentCode", parentCode);
            spc.Add("@CodeType", codeType);
            string sql = "select * from WxjzgcjczyQyb_code where parentcodetype=@ParentCodeType and parentcode=@ParentCode and codetype=@CodeType";
            return DB.ExeSqlForDataTable(sql, spc, "t");
        }

        /// <summary>
        /// 通过省系统CodeType和Code查询省系统代码对照表
        /// </summary>
        /// <param name="codetype">代码类型</param>
        /// <param name="codeweb">省系统代码</param>
        /// <returns></returns>
        public DataRow ReadCodeDZ(string codetype, string codeweb)
        {
            string sql = "select * from WxjzgcjczyQyb_CodeDZ where CodeType=@CodeType and CodeWeb=@CodeWeb";
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@CodeWeb", codeweb);
            spc.Add("@CodeType", codetype);
            DataTable dt = DB.ExeSqlForDataTable(sql, spc, "t");
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        /// <summary>
        /// 通过code和codetype获得code信息
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="codeType">代码类型</param>
        /// <returns></returns>
        public DataTable ReadCode(string code, string codeType)
        {
            string sql = "select * from WxjzgcjczyQyb_Code where CodeType=@CodeType and code=@Code";
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@CodeType", codeType);
            spc.Add("@Code", code);
            return DB.ExeSqlForDataTable(sql, spc, "t");
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 得到数据库服务器时间
        /// </summary>
        public DateTime GetServerDateTime()
        {
            return Convert.ToDateTime(DB.ExeSqlForString("select getdate()", null));
        }
        /// <summary>
        /// 用户所具有的权限(针对 模块)
        /// </summary>
        public bool GetUserRight(string moduleCode, string UserID)
        {
            SqlParameterCollection spc = this.DB.CreateSqlParameterCollection();
            spc.Add("@moduleCode", moduleCode);
            spc.Add("@UserID", UserID);
            string sql = " select top 1 d.* from webplat50..UserRoles as a "
                   + " inner join webplat50..UserInfo as b  on a.UserID=b.UserID "
                   + " inner join webplat50..Roles as c on a.roleid=c.roleid "
                   + " inner join sgtyk_right as d on c.roleID = d.roleID "
                   + " where b.UserID =@UserID "
                   + " and moduleCode=@moduleCode and (d.right_add ='1' or d.right_edit='1' or d.right_del ='1' or d.right_verify ='1' )";

            return DB.ExeSqlForDataTable(sql, spc, "talbe").Rows.Count > 0;
        }
        /// <summary>
        /// 获得用户是否有新增的权限(针对 模块)
        /// </summary>
        /// <param name="moduleCode">功能模块代码</param>
        /// <returns></returns>
        public bool GetUserAddRight(string moduleCode, string UserID)
        {
            SqlParameterCollection spc = this.DB.CreateSqlParameterCollection();
            spc.Add("@moduleCode", moduleCode);
            spc.Add("@UserID", UserID);
            string sql = " select top 1 d.* from webplat50..UserRoles as a "
                   + " inner join webplat50..UserInfo as b  on a.UserID=b.UserID "
                   + " inner join webplat50..Roles as c on a.roleid=c.roleid "
                   + " inner join sgtyk_right as d on c.roleID = d.roleID "
                   + " where b.UserID =@UserID "
                   + " and moduleCode=@moduleCode and d.right_add ='1' ";

            return DB.ExeSqlForDataTable(sql, spc, "talbe").Rows.Count > 0;
        }

        /// <summary>
        /// 获得用户是否有编辑的权限(针对 模块)
        /// </summary>
        /// <param name="moduleCode">功能模块代码</param>
        /// <returns></returns>
        public bool GetUserEditRight(string moduleCode, string UserID)
        {
            SqlParameterCollection spc = this.DB.CreateSqlParameterCollection();
            spc.Add("@moduleCode", moduleCode);
            spc.Add("@UserID", UserID);
            string sql = " select top 1 d.* from webplat50..UserRoles as a "
                   + " inner join webplat50..UserInfo as b  on a.UserID=b.UserID "
                   + " inner join webplat50..Roles as c on a.roleid=c.roleid "
                   + " inner join sgtyk_right as d on c.roleID = d.roleID "
                   + " where b.UserID =@UserID "
                   + " and moduleCode=@moduleCode and d.right_edit='1'";

            return DB.ExeSqlForDataTable(sql, spc, "talbe").Rows.Count > 0;
        }

        /// <summary>
        /// 获得用户是否有删除的权限(针对 模块)
        /// </summary>
        /// <param name="moduleCode">功能模块代码</param>
        /// <returns></returns>
        public bool GetUserDelRight(string moduleCode, string UserID)
        {
            SqlParameterCollection spc = this.DB.CreateSqlParameterCollection();
            spc.Add("@moduleCode", moduleCode);
            spc.Add("@UserID", UserID);
            string sql = " select top 1 d.* from webplat50..UserRoles as a "
                   + " inner join webplat50..UserInfo as b  on a.UserID=b.UserID "
                   + " inner join webplat50..Roles as c on a.roleid=c.roleid "
                   + " inner join sgtyk_right as d on c.roleID = d.roleID "
                   + " where b.UserID =@UserID "
                   + " and moduleCode=@moduleCode and  d.right_del ='1'";

            return DB.ExeSqlForDataTable(sql, spc, "talbe").Rows.Count > 0;
        }

        /// <summary>
        /// 获得用户是否有 审核/调动/发放/统计 权限的权限(针对 模块)
        /// </summary>
        /// <param name="moduleCode">功能模块代码</param>
        /// <returns></returns>
        public bool GetUserVerifyRight(string moduleCode, string UserID)
        {
            SqlParameterCollection spc = this.DB.CreateSqlParameterCollection();
            spc.Add("@moduleCode", moduleCode);
            spc.Add("@UserID", UserID);
            string sql = " select top 1 d.* from webplat50..UserRoles as a "
                   + " inner join webplat50..UserInfo as b  on a.UserID=b.UserID "
                   + " inner join webplat50..Roles as c on a.roleid=c.roleid "
                   + " inner join sgtyk_right as d on c.roleID = d.roleID "
                   + " where b.UserID =@UserID "
                   + " and moduleCode=@moduleCode and d.right_verify ='1' ";

            return DB.ExeSqlForDataTable(sql, spc, "talbe").Rows.Count > 0;
        }
        #endregion

        public DataSet GetUserRealName(string UserID)
        {
            SqlParameterCollection spc = DB.CreateSqlParameterCollection();
            spc.Add("@UserID", UserID);
            //主表信息
            string sql = @" select UserRealName from Webplat50..UserInfo where UserID = @UserID ";

            DataSet ds = DB.ExeSqlForDataSet(sql, spc, "t1");
            return ds;
        }

    }
}
