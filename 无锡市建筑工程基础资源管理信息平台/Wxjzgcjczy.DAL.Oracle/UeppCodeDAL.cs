using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bigdesk8.Data;
using System.Data;

namespace Wxjzgcjczy.DAL
{
    public class UeppCodeDAL
    {
        public DBOperator DB { get; set; }

        /// <summary>
        /// 获取一组代码键值信息(原三层架构写法)
        /// </summary>
        /// <param name="codeType">代码类型</param>
        /// <param name="parentCodeType">父代码类型</param>
        /// <param name="parentCodeInfo">父代码的名称（注意：不是父代码值）</param>
        /// <returns></returns>
        public Dictionary<string, string> SelectKeyValue(CodeType codeType, CodeType parentCodeType, string parentCodeInfo)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            p.Add("@CodeType", codeType.ToString());

            string sql;
            if (parentCodeType == CodeType.空父代码类型)
            {
                sql = @"select Code ,code ,CodeInfo  from UEPP_Code where ParentCodeType='' and ParentCode='' and CodeType=@CodeType order by OrderID";
            }
            else
            {
                p.Add("@ParentCodeType", parentCodeType.ToString());
                p.Add("@ParentCodeInfo", parentCodeInfo);

                sql = @"select a.Code ,a.Code ,a.CodeInfo  from uepp_Code  a
inner join uepp_Code  b on b.CodeType=a.ParentCodeType and b.Code=a.ParentCode and b.CodeType=@ParentCodeType and b.CodeInfo=@ParentCodeInfo
where a.CodeType=@CodeType
order by a.OrderID";
            }

            var dt = DB.ExeSqlForDataTable(sql, p, "t");

            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (DataRow dr in dt.Rows)
            {
                dic.Add(dr["Code"].ToString(), dr["CodeInfo"].ToString());
            }
            return dic;
        }


        /// <summary>
        /// 获取一组代码键值信息----最新写法
        /// </summary>
        /// <param name="codeType">代码类型</param>
        /// <param name="parentCodeType">父代码类型</param>
        /// <param name="parentCodeInfo">父代码的名称（注意：不是父代码值）</param>
        /// <returns></returns>
        public DataTable SelectKeyValue_New(CodeType codeType, CodeType parentCodeType, string parentCodeInfo)
        {
            SqlParameterCollection p = DB.CreateSqlParameterCollection();
            p.Add("@CodeType", codeType.ToString());

            string sql;
            if (parentCodeType == CodeType.空父代码类型)
            {
                sql = @"select Code  id,code  value,CodeInfo  text  from UEPP_Code where ParentCodeType='' and ParentCode='' and CodeType=@CodeType order by OrderID";
            }
            else
            {
                p.Add("@ParentCodeType", parentCodeType.ToString());
                p.Add("@ParentCodeInfo", parentCodeInfo);

                sql = @"select a.Code  id ,a.Code  value,a.CodeInfo  text from UEPP_Code  a
inner join UEPP_Code  b on b.CodeType=a.ParentCodeType and b.Code=a.ParentCode and b.CodeType=@ParentCodeType and b.CodeInfo=@ParentCodeInfo
where a.CodeType=@CodeType
order by a.OrderID";
            }

            return DB.ExeSqlForDataTable(sql, p, "t");
        }

        /// <summary>
        /// 读取所有模块名称
        /// </summary>
        /// <returns></returns>
        public DataTable ReadMkmc()
        {
            string sql = "select ModuleCode  id,ModuleCode  value,ModuleName  text from g_Module order by sort";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }

        /// <summary>
        /// 获取所有操作名称
        /// </summary>
        /// <returns></returns>
        public DataTable ReadCzmc()
        {
            string sql = "select OperateCode  id,OperateCode  value,OperateName  text from g_Operate order by sort";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }
        /// <summary>
        /// 读取codeinfo
        /// </summary>
        public string ReadCodeinfo(string code, string codetype)
        {
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@code", code);
            sp.Add("@codetype", codetype);
            string sql = "select codeinfo from uepp_code where code=@code and codetype=@codetype";
            return DB.ExeSqlForString(sql, sp);
        }

        public DataTable ReadJwn()
        {
            string sql = @"select DATENAME(YY,GETDATE())  id,DATENAME(YY,GETDATE())  value,DATENAME(YY,GETDATE())  text
                        union all
                        select (DATENAME(YY,GETDATE())-1)  id,(DATENAME(YY,GETDATE())-1)  value,(DATENAME(YY,GETDATE())-1)  text
                        union all
                        select (DATENAME(YY,GETDATE())-2)  id,(DATENAME(YY,GETDATE())-2)  value,(DATENAME(YY,GETDATE())-2)  text
                        union all
                        select (DATENAME(YY,GETDATE())-3)  id,(DATENAME(YY,GETDATE())-3)  value,(DATENAME(YY,GETDATE())-3)  text
                        union all
                        select (DATENAME(YY,GETDATE())-4)  id,(DATENAME(YY,GETDATE())-4)  value,(DATENAME(YY,GETDATE())-4)  text
                        ";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }

        public DataTable ReadQyjsscs()
        {
            string sql = "select code,codeinfo from uepp_code where codetype='城市地区' and (parentcode='320000' or code='000000')";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }
        public DataTable ReadQyjsscs2()
        {
            string sql = "select code  value,codeinfo  text,code  id from uepp_code where codetype='城市地区' and (parentcode='320000' or code='000000')";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }

        public DataTable ReadSsdqByParentCode(string parentcode)
        {
            string sql = "select code,codeinfo from uepp_code where parentcode='" + parentcode + "'";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }

        public DataTable ReadSsdqByParentCode2(string parentcode)
        {
            string sql = "select code  id,code  value,codeinfo  text from uepp_code where parentcode='" + parentcode + "'";
            return DB.ExeSqlForDataTable(sql, null, "dt");
        }
    }
}
