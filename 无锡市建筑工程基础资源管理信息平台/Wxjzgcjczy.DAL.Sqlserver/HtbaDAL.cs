using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bigdesk8.Data;
using System.Data;

namespace Wxjzgcjczy.DAL.Sqlserver
{
    public class HtbaDAL
    {
        public DBOperator DB { get; set; }

        public DataTable Get_HtbnByRecordNum(string recordNum)
        {
            string sql = @"SELECT * FROM [WJSJZX].[dbo].[TBContractRecordManage] WHERE RecordNum=@recordNum ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@recordNum", recordNum);
            return DB.ExeSqlForDataTable(sql, sp, "htba");
        }

        public void UpdateHtbaPrjType(string recordNum, string prjType)
        {
            string sql = "update [dbo].[TBContractRecordManage] set PrjType=@prjType where RecordNum=@recordNum ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@prjType", prjType);
            sp.Add("@recordNum", recordNum);
            this.DB.ExecuteNonQuerySql(sql, sp);
        }
        public void UpdateHtbaUnion(string recordNum, string unionCorpName, string unionCorpCode)
        {
            string sql = "update [dbo].[TBContractRecordManage] set UnionCorpName=@unionCorpName,UnionCorpCode=@unionCorpCode where RecordNum=@recordNum ";
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            sp.Add("@unionCorpName", unionCorpName);
            sp.Add("@unionCorpCode", unionCorpCode);
            sp.Add("@recordNum", recordNum);
            this.DB.ExecuteNonQuerySql(sql, sp);
        }
 
    }

}
