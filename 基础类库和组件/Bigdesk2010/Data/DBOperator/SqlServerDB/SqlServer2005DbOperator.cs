using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using System.Data.Common;

namespace Bigdesk2010.Data
{
    /// <summary>
    /// SQL Server 2005 数据库访问类	
    /// </summary>
    /// 一般来说，DAL能在sql2005上运行，也就能在sql2008上运行，则DAL中的数据库访问类就不能说死是访问sql2005，还是sql2008。
    /// 这时，不管DAL中的数据库访问类是从SqlServer2005DbOperator继承，还是SqlServer2008DbOperator（尚未定义）继承，都会造成名字上的混淆。
    /// 故将本类的功能直接合并到SqlServerDBOperator中。
    [Obsolete("本类的功能已合并到了SqlServerDBOperator中")]
    public class SqlServer2005DbOperator : SqlServerDbOperator
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionStringSetting">连接字符串</param>
        public SqlServer2005DbOperator(ConnectionStringSettings connectionStringSetting)
            : base(connectionStringSetting)
        { }

        #endregion 构造函数

        //public override DataTable ExeSqlForDataTable(string selectSQL, SqlParameterCollection sqlParameterCollection, string dataTableName, string orderby, int pageSize, int pageIndex, out int allRecordCount)
        //{
        //    allRecordCount = this.ExeSqlForObject(this.GetRowCountQuerySQL(selectSQL), sqlParameterCollection).ToInt32();

        //    PagedData pagedData = new PagedData(allRecordCount, pageSize, pageIndex);

        //    return this.ExeSqlForDataTable(this.GetPagedQuerySQL(selectSQL, orderby, pagedData), sqlParameterCollection, dataTableName);
        //}

        #region 分页语句

        /// <summary>
        /// SQL 分页查询 SQL 语句
        /// </summary>
        /// <param name="querySQL">SQL 语句，注意不带排序字段</param>
        /// <param name="orderby">排序字段，注意：不要带 order by 关键字，如: order1 desc,order2 asc</param>
        /// <param name="pageData">分页数据</param>
        /// <returns>返回整理过的分页 SQL 语句</returns>
        protected override string GetPagedQuerySQL(string querySQL, string orderby, PagedData pageData)
        {
            if (querySQL.IsEmpty()) throw new Exception("参数 querySQL 不能为空！");
            if (orderby.IsEmpty()) throw new Exception("参数 orderby 不能为空！");
            if (pageData == null) throw new Exception("参数 pageData 不能为空！");

            if (pageData.RecordCount == 0) return querySQL;

            const string FirstPagedQuerySQLText = "select top {0} * from ({1}) as ___table0 order by {2}";
            const string PagedQuerySQLText = "select top {0} * from (select row_number() over(order by {1}) as ___rownumber,* from ({2}) as ___table0) as ___table1 where ___rownumber>={3}";

            if (pageData.IsFirstPage)
            {
                return string.Format(FirstPagedQuerySQLText, pageData.RecordCountInPage, querySQL, orderby);
            }

            // 定义一个配置数组用来填充查询语句
            object[] configFields = new object[5];
            configFields[0] = pageData.RecordCountInPage;//top
            configFields[1] = orderby;//orderby
            configFields[2] = querySQL;//table
            configFields[3] = pageData.FirstIndexInPage + 1;//firstindexinpage

            // 填充查询语句
            return string.Format(PagedQuerySQLText, configFields);
        }

        /// <summary>
        /// SQL 查询记录总数语句
        /// </summary>
        /// <param name="querySQL">SQL 语句，注意不带排序字段</param>
        /// <returns>返回记录总数 SQL 语句</returns>
        protected override string GetRowCountQuerySQL(string querySQL)
        {
            const string RowCountSQL = "select count(1) from ({0}) as ___table0";
            return string.Format(RowCountSQL, querySQL);
        }

        #endregion
    }
}
