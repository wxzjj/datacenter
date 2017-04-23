using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bigdesk8.Data;
using Bigdesk8;
using Bigdesk8.Web;
using System.Data;

namespace Wxjzgcjczy.DAL.Sqlserver
{
    public class SystemDAL
    {
        public DBOperator DB { get; set; }
        
        /// <summary>
        /// 1.获取某个用户的信息
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public DataTable GetUserInfo(string username, string password)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            p.Add("@username", username);
            p.Add("@password", password);
            string sql = @" select a.*,b.JGID,c.JGMC,c.ZZJGDM from G_User  a
left join Uepp_JgUserRelation b on a.UserID=b.UserID
left join  Uepp_Jgjbxx c on b.JGID=c.JGID
where a.LoginName=@username and a.LoginPassword=@password ";
            return DB.ExeSqlForDataTable(sql, p, "t");
        }
        /// <summary>
        /// 获取某个用户的信息
        /// </summary>
        /// <param name="userid">userid</param>
        /// <returns></returns>
        public DataTable GetUserInfo(string userid)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            p.Add("@UserID", userid);
            string sql = @"select a.*,b.JGID,c.JGMC,c.ZZJGDM from G_User  a
left join Uepp_JgUserRelation b on a.UserID=b.UserID
left join  Uepp_Jgjbxx c on b.JGID=c.JGID
where a.UserID=@UserID";
            return DB.ExeSqlForDataTable(sql, p, "t");
        }
        /// <summary>
        /// 根据登录名获取用户
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public DataTable GetUserInfoByLoginName(string loginName)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            p.Add("@username", loginName);
            string sql = @"select a.*,b.JGID,c.JGMC,c.ZZJGDM from G_User  a
left join Uepp_JgUserRelation b on a.UserID=b.UserID
left join  Uepp_Jgjbxx c on b.JGID=c.JGID
where a.LoginName=@username ";
            return DB.ExeSqlForDataTable(sql, p, "t");
        }

        /// <summary>
        /// 2. 获取角色信息
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public DataTable GetRolesInfo(string username, string password)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            p.Add("@username", username);
            p.Add("@password", password);
            string sql = " select b.* from G_User  a "
                       + " inner join G_UserRole  b on a.UserID=b.UserID "
                       + " where a.LoginName=@username and a.LoginPassword=@password";
            return DB.ExeSqlForDataTable(sql, p, "t");
        }
        /// <summary>
        /// 更新用户登录时间
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="lastLoginTime">最后一次登录时间</param>
        public void UpdateLastLoginTime(string userID, string lastLoginTime)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            p.Add("@userid", userID);
            p.Add("@lastlogintime", lastLoginTime);
            string sql = "update g_user set LastLoginTime=@lastLoginTime where UserID=@userid";
            DB.ExecuteNonQuerySql(sql, p);
        }

        /// <summary>
        /// 判断是否存在该用户
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Login(string username, string password)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            p.Add("@username", username);
            p.Add("@password", password);

            string sql = "select * from G_User where LoginName=@username and LoginPassword=@password";
            return DB.ExeSqlForDataTable(sql, p, "t").Rows.Count > 0;
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public void ChangePwd(string userid, string password)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            p.Add("@userid", userid);
            p.Add("@password", password);

            string sql = "update G_User set LoginPassword=@password where userid=@userid ";
            DB.ExecuteNonQuerySql(sql, p);
        }
        ///// <summary>
        ///// 获取企业用户信息
        ///// </summary>
        ///// <param name="userid"></param>
        //public DataTable GetQyInfo(string userid)
        //{
        //    SqlParameterCollection p = DB.CreateSqlParameterCollection();
        //    p.Add("@UserID", userid);

        //    string sql = "select * from UEPP_Qyjbxx where UserID=@UserID";
        //    return DB.ExeSqlForDataTable(sql, p, "t");
        //}
        ///// <summary>
        ///// 获取实施单位（建设单位）用户信息
        ///// </summary>
        ///// <param name="userid"></param>
        ///// <returns></returns>
        //public DataTable GetJsdwInfo(string userid)
        //{
        //    SqlParameterCollection p = DB.CreateSqlParameterCollection();
        //    p.Add("@UserID", userid);

        //    string sql = "select * from Szgkjc_Jsxm_Jsdw where UserID=@UserID";
        //    return DB.ExeSqlForDataTable(sql, p, "t");
        //}
//        /// <summary>
//        /// 获取公文通知信息
//        /// </summary>
//        /// <param name="workUser"></param>
//        /// <returns></returns>
//        public DataTable ReadGwtz(AppUser workUser)
//        {
//            SqlParameterCollection p = DB.CreateSqlParameterCollection();
//            p.Add("@UserID", workUser.UserID);

//            string sql = @"select c.*
//                           from Gwtz_Info_Sjml  a
//                           inner join Gwtz_Sjml  b on b.SjmlID = a.SjmlID 
//                           inner join Gwtz_User_Relation  d on b.SjmlID=d.sjmlID
//                           inner join Gwtz_Info  c on c.InfoId = a.InfoId 
//                           where 1=1 and a.InnerMailReaded = 0 and c.IsDeleted<>1 and c.InfoType='公文通知' and d.UserID=@UserID";
//            return DB.ExeSqlForDataTable(sql, p, "t");
//        }

//        /// <summary>
//        /// 创建一个新的用户
//        /// </summary>
//        /// <param name="userName"></param>
//        /// <param name="loginName"></param>
//        /// <param name="passWord"></param>
//        /// <returns></returns>
//        public int CreateUser(string userName, string loginName, string passWord)
//        {
//            string sql = "select * from g_user where 1=2";
//            DataTable dt = DB.ExeSqlForDataTable(sql, null, "t");
//            dt.Rows.Add(dt.NewRow());
//            dt.Rows[0]["LoginName"] = loginName;
//            dt.Rows[0]["LoginPassWord"] = passWord;
//            dt.Rows[0]["UserName"] = userName;
//            dt.Rows[0]["UserType"] = UserType.实施单位;
//            dt.Rows[0]["UserState"] = "正常用户";
//            int userID = DB.ExeSqlForObject("select max(userID) from g_user", null).ToInt32(0) + 1;
//            dt.Rows[0]["UserID"] = userID;
//            DB.Update(sql, null, dt);

//            sql = "select * from g_UserRole where 1=2";
//            dt = DB.ExeSqlForDataTable(sql, null, "t");
//            dt.Rows.Add(dt.NewRow());
//            dt.Rows[0]["RoleId"] = 2;
//            dt.Rows[0]["UserID"] = userID;
//            DB.Update(sql, null, dt);

//            return userID;
//        }
        /// <summary>
        /// 判断该用户名是否已存在
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public bool CheckLoginName(string loginName)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            p.Add("@LoginName", loginName);

            string sql = "select * from g_user where LoginName=@LoginName";
            return DB.ExeSqlForDataTable(sql, p, "t").Rows.Count > 0 ? true : false;
        }




        public DataSet GetDs(string start,string end)
        {
            DataSet ds = new DataSet();
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@start", start);
            sp.Add("@end", end);

            string sql1 = @" select distinct a.xm as '人员姓名' ,a.ryID as '身份证号', b.ryzyzglx as '执业资格类型', b.zsbh as '证书编号', d.qymc as '单位名称'  
            from UEPP_Ryjbxx a inner join UEPP_Ryzs b on a.ryID=b.ryID    inner join  UEPP_QyRy c on a.ryID=c.ryID and b.ryzyzglxid=c.ryzyzglxid   inner join UEPP_Qyjbxx  d
            on c.qyID=d.qyID   where (b.ryzyzglxid in ( 1,21,41,51) or (b.ryzslxID=161 and b.ryzyzglxid=61))  and a.xgrqsj>=@start and a.xgrqsj<=@end";

            ds.Tables.Add(DB.ExeSqlForDataTable(sql1, sp, "1.注册执业人员"));

            sql1 = @" select distinct a.xm as '人员姓名' ,a.ryID as '身份证号', b.ryzyzglx as '执业资格类型', b.zsbh as '证书编号', d.qymc as '单位名称'  
 from UEPP_Ryjbxx a inner join UEPP_Ryzs b on a.ryID=b.ryID    inner join  UEPP_QyRy c on a.ryID=c.ryID and b.ryzyzglxid=c.ryzyzglxid   inner join UEPP_Qyjbxx  d
 on c.qyID=d.qyID   where (b.ryzyzglxID=4 or (b.ryzyzglxID=5 and b.ryzslxID=51 and b.ryzyzglx='项目B类人员') 
   or (b.ryzyzglxID=6 and b.ryzslxID=51 and b.ryzyzglx='安全员(C类人员)')
 )and a.xgrqsj>=@start and a.xgrqsj<=@end";

            ds.Tables.Add(DB.ExeSqlForDataTable(sql1, sp, "2.安全生产管理人员"));

            sql1 = @" select distinct a.xm as '人员姓名' ,a.ryID as '身份证号', b.ryzyzglx as '执业资格类型', b.zsbh as '证书编号', d.qymc as '单位名称'  
 from UEPP_Ryjbxx a inner join UEPP_Ryzs b on a.ryID=b.ryID    inner join  UEPP_QyRy c on a.ryID=c.ryID and b.ryzyzglxid=c.ryzyzglxid   inner join UEPP_Qyjbxx  d
 on c.qyID=d.qyID   where (b.ryzyzglxid in ( 7,8,9,11,12,14,15,16,17,18,22,42) )  and a.xgrqsj>=@start and a.xgrqsj<=@end
";

            ds.Tables.Add(DB.ExeSqlForDataTable(sql1, sp, "3、专业岗位管理人员"));

            sql1 = @" select a.PrjNum as '全省统一项目编号',a.BuilderLicenceInnerNum as '许可证内部编号',b.PrjName as '工程项目名称',a.IssueCertDate as '发证日期'
,a.PrjSize as '建设规模',a.ConsCorpName as '施工单位名称',a.SuperCorpName as '监理单位名称',a.EconCorpName as '勘察单位名称',a.DesignCorpName as '设计单位名称'
 from TBBuilderLicenceManage a left join TBProjectInfo b on a.PrjNum=b.PrjNum
 
 where  a.CreateDate>=@start and a.CreateDate<=@end and a.UpdateFlag='U'
";

            ds.Tables.Add(DB.ExeSqlForDataTable(sql1, sp, "4、建设工程施工许可证发放信息"));

            sql1 = @" select a.PrjName as '项目名称',a.PrjNum as '全省统一项目编号',b.codeinfo as '项目分类'
,a.BuildCorpName as '建设单位名称',a.BuildCorpCode as '建设单位组织机构代码',a.ProvinceNum as '项目所在省',
a.CityNum as '项目所在地市',a.CountyNum as '项目所在区县' 
 ,a.AllInvest as '总投资（万元）',a.AllArea as '总面积（平方米）',
a.PrjSize as '建设规模',a.PrjPropertyNum as '建设性质',a.PrjFunctionNum as '工程用途',a.Bdate as '实际开工日期',
a.Edate as '实际竣工日期',a.PrjApprovalNum as '立项批文号',
a.BuldPlanNum as '建设用地规划许可证号',a.ProjectPlanNum as '建设工程规划许可证号'
 from TBProjectInfo a left join TBPRJTYPEDIC b on a.PrjTypeNum=b.code
 where  a.CreateDate>=@start and a.CreateDate<=@end and a.UpdateFlag='U'
";

            ds.Tables.Add(DB.ExeSqlForDataTable(sql1, sp, "5.工程项目基本信息"));

            sql1 = @" select a.TenderName as '招标项目名称',a.PrjNum as '全省统一项目编号',a.TenderCorpName as '中标单位名称'
,a.TenderCorpCode as '中标单位组织机构代码编号',b.codeinfo as '招标类型',c.codeinfo as '招标方式',
a.PrjSize as '建设规模',a.Area  as '面积（平方米）' 
 ,a.TenderMoney as '中标金额',a.TenderResultDate as '中标日期'
 from TBTenderInfo a left join TBTENDERCLASSDIC b on a.TenderClassNum=b.code
  left join TBTENDERTYPEDIC c on a.TenderTypeNum=c.code
 where  a.CreateDate>=@start and a.CreateDate<=@end and a.UpdateFlag='U'
";

            ds.Tables.Add(DB.ExeSqlForDataTable(sql1, sp, "6、工程招标中标信息"));
            return ds;
        }


        public DataTable GetStLog(List<IDataItem> condition, int pageSize, int pageIndex, out int allRecordCount, string sfsccg, string orderby)
        {
            string sql = @"select * from (  select *,case when OperateState='0' then '是' else '否' end as OperateStateMsg from SaveToStLog 
                           where     1=1) as a where 1=1 and  datediff(day,UpdateDate,GETDATE())=0";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();

            if (!string.IsNullOrEmpty(sfsccg))
            {
                sp.Add("@OperateStateMsg", sfsccg);
                sql = sql + " and OperateStateMsg=@OperateStateMsg ";
            }

            condition.GetSearchClause(sp, ref sql);
            return DB.ExeSqlForDataTable(sql, sp, "t", orderby, pageSize, pageIndex, out allRecordCount);
        }

    }
}
