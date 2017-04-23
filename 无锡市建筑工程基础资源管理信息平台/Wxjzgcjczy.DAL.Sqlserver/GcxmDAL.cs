using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bigdesk8.Data;
using System.Data;

namespace Wxjzgcjczy.DAL.Sqlserver
{
    public class GcxmDAL
    {
        public DBOperator DB { get; set; }
        /// <summary>
        /// 勘查设计合同
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public DataTable RetrieveKcsjht(List<IDataItem> conditions,int pagesize, int page, string orderby, out int allRecordCount)
        {
            allRecordCount = 0;
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string strSQL= @"SELECT * from (
                              SELECT  a.PKID,a.RecordNum,a.RecordInnerNum,
                                a.ContractNum,
                                a.RecordName,
                                a.PropietorCorpName,
                                a.ContractorCorpName,
                                convert(char(10),a.ContractDate,20) as ContractDate,a.ContractTypeNum,
                                (select CodeInfo from tbContractTypeDic where Code= a.ContractTypeNum) as ContractType,
                                a.PrjNum,
                                b.PrjName,b.PKID as LxPKID
                                FROM TBContractRecordManage a 
                                left join TBProjectInfo b on a.PrjNum=b.PrjNum where a.UpdateFlag='U' and a.ContractTypeNum in ('100','200')
                          ) as aaa WHERE 1=1 ";
          
            conditions.GetSearchClause(sp, ref strSQL);
            return DB.ExeSqlForDataTable(strSQL, sp, "t", orderby, pagesize, page, out allRecordCount);
        }

        /// <summary>
        /// 施工监理合同
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <param name="allRecordCount"></param>
        /// <returns></returns>
        public DataTable RetrieveSgjlht(List<IDataItem> conditions, int pagesize, int page, string orderby, out int allRecordCount)
        {
            allRecordCount = 0;
            SqlParameterCollection sp = DB.CreateSqlParameterCollection();
            string strSQL = @"SELECT * from (
                              SELECT  a.PKID,a.RecordNum,a.RecordInnerNum,
                                a.ContractNum,
                                a.RecordName,
                                a.PropietorCorpName,
                                a.ContractorCorpName,
                                convert(char(10),a.ContractDate,20) as ContractDate,a.ContractTypeNum,
                                (select CodeInfo from tbContractTypeDic where Code= a.ContractTypeNum) as ContractType,
                                a.PrjNum,
                                b.PrjName,b.PKID as LxPKID
                                FROM TBContractRecordManage a 
                                left join TBProjectInfo b on a.PrjNum=b.PrjNum where a.UpdateFlag='U' and a.ContractTypeNum <>'100' and a.ContractTypeNum <>'200'
                          ) as aaa WHERE 1=1 ";

            conditions.GetSearchClause(sp, ref strSQL);
            return DB.ExeSqlForDataTable(strSQL, sp, "t", orderby, pagesize, page, out allRecordCount);
        }
    }



}
