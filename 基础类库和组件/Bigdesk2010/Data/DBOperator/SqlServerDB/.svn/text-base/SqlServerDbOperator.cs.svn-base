using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using System.Data.Common;

namespace Bigdesk2010.Data
{
    /// <summary>
    /// SqlServer 数据库访问类	
    /// </summary>
    public class SqlServerDbOperator : DBOperator
    {
        #region 私有成员

        private DataBaseType MyDataBaseType;

        /// <summary>
        /// 表示 SqlServer 数据库的一个打开的连接
        /// </summary>
        private readonly SqlConnection _dbConnection;

        /// <summary>
        /// SQL 事务
        /// </summary>
        private SqlTransaction _dbTransaction;

        /// <summary>
        /// 指示当前是否正处于事务中
        /// </summary>
        private bool _isTransacting = false;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionStringSetting">连接字符串</param>
        public SqlServerDbOperator(ConnectionStringSettings connectionStringSetting)
            : base(connectionStringSetting)
        {
            this._dbConnection = new SqlConnection(connectionStringSetting.ConnectionString);
            this.MyDataBaseType = (DataBaseType)Enum.Parse(typeof(DataBaseType), connectionStringSetting.ProviderName.ToUpper());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="dataBaseType"></param>
        public SqlServerDbOperator(string connectionString, DataBaseType dataBaseType)
        {
            this._dbConnection = new SqlConnection(connectionString);
            this.MyDataBaseType = dataBaseType;
        }

        #endregion 构造函数

        #region 私有方法

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        private void Open()
        {
            switch (this._dbConnection.State)
            {
                case ConnectionState.Closed:
                    this._dbConnection.Open();
                    break;
                case ConnectionState.Broken:
                    this._dbConnection.Close();
                    this._dbConnection.Open();
                    break;
                default:
                case ConnectionState.Open:
                case ConnectionState.Connecting:
                case ConnectionState.Executing:
                case ConnectionState.Fetching:
                    break;
            }
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        private void Close()
        {
            switch (this._dbConnection.State)
            {
                case ConnectionState.Closed:
                    break;
                default:
                case ConnectionState.Open:
                case ConnectionState.Connecting:
                case ConnectionState.Executing:
                case ConnectionState.Fetching:
                case ConnectionState.Broken:
                    this._dbConnection.Close();
                    break;
            }
        }

        /// <summary>
        /// 将 <see cref="SqlParameterCollection"/> 对象中的参数加入到 <see cref="System.Data.SqlClient.SqlParameterCollection"/> 对象中。
        /// </summary>
        /// <param name="sqlParameterCollection"><see cref="SqlParameterCollection"/> 对象</param>
        /// <param name="sqlServerSqlParameterCollection"><see cref="System.Data.SqlClient.SqlParameterCollection"/> 对象</param>
        private void AddDbParameter(SqlParameterCollection sqlParameterCollection, System.Data.SqlClient.SqlParameterCollection sqlServerSqlParameterCollection)
        {
            if (sqlParameterCollection == null || sqlServerSqlParameterCollection == null) return;

            for (int i = 0, j = sqlParameterCollection.Count; i < j; i++)
            {
                SqlParameter p = (SqlParameter)((ICloneable)sqlParameterCollection[i]).Clone();

                // 检查未分配值的输入参数,将其分配以DBNull.Value.
                if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) && (p.Value == null))
                {
                    p.Value = DBNull.Value;
                }
                sqlServerSqlParameterCollection.Add(p);
            }
        }

        #endregion 私有方法

        #region 操作数据库的基础

        /// <summary>
        /// SQL 参数集合
        /// </summary>
        /// <returns>一个 <see cref="SqlParameterCollection"/> 对象。</returns>
        public override SqlParameterCollection CreateSqlParameterCollection()
        {
            return new SqlServerDbParameterCollection();
        }

        /// <summary>
        /// 创建 <see cref="DbParameter"/> 对象的新实例。
        /// </summary>
        /// <returns>一个 <see cref="DbParameter"/> 对象。</returns>
        public override DbParameter CreateDbParameter()
        {
            return new SqlParameter();
        }

        /// <summary>
        /// 开始数据库事务
        /// </summary>
        public override void BeginTransaction()
        {
            this.Open();
            this._dbTransaction = this._dbConnection.BeginTransaction();
            this._isTransacting = true;
        }

        /// <summary>
        /// 以指定的隔离级别启动数据库事务。
        /// </summary>
        /// <param name="isolationLevel">指定事务的隔离级别。</param>
        /// <returns>表示新事务的对象。</returns>
        public override void BeginTransaction(IsolationLevel isolationLevel)
        {
            this.Open();
            this._dbTransaction = this._dbConnection.BeginTransaction(isolationLevel);
            this._isTransacting = true;
        }

        /// <summary>
        /// 提交数据库事务
        /// </summary>
        public override void CommitTransaction()
        {
            this._dbTransaction.Commit();
            this._isTransacting = false;
            this.Close();
        }

        /// <summary>
        /// 回滚数据库事务
        /// </summary>
        public override void RollbackTransaction()
        {
            this._dbTransaction.Rollback();
            this._dbTransaction.Dispose();
            this._isTransacting = false;
            this.Close();
        }

        #endregion

        #region 通过执行 SELECT SQL 语句提取数据的相关方法

        #region DataTable

        /// <summary>
        /// 执行 SELECT SQL 语句，返回 DataTable
        /// </summary>
        /// <param name="selectSQL">执行的 Select SQL 语句</param>
        /// <param name="sqlParameterCollection">SQL 参数集合</param>
        /// <param name="dataTableName">数据表名，不必与数据库的一致</param>
        /// <returns>生成数据集</returns>
        public override DataTable ExeSqlForDataTable(string selectSQL, SqlParameterCollection sqlParameterCollection, string dataTableName)
        {
            selectSQL = Bigdesk2010.Security.AntiSqlInjection.GetSafeSql(selectSQL);
            using (SqlDataAdapter ad = new SqlDataAdapter(selectSQL, this._dbConnection))
            {
                this.AddDbParameter(sqlParameterCollection, ad.SelectCommand.Parameters);

                if (this._isTransacting && ad.SelectCommand != null)
                    ad.SelectCommand.Transaction = this._dbTransaction;

                DataTable dt = new DataTable(dataTableName);
                ad.Fill(dt);
                return dt;
            }
        }

        #endregion DataTable

        #region DataSet

        /// <summary>
        /// 执行 SELECT SQL 语句，返回 DataSet
        /// </summary>
        /// <param name="selectSQL">生成数据集的 Select SQL</param>
        /// <param name="sqlParameterCollection">SQL 参数集合</param>
        /// <param name="dataTableName">数据表名，不必与数据库的一致</param>
        /// <returns>生成数据集</returns>
        public override DataSet ExeSqlForDataSet(string selectSQL, SqlParameterCollection sqlParameterCollection, string dataTableName)
        {
            selectSQL = Bigdesk2010.Security.AntiSqlInjection.GetSafeSql(selectSQL);
            using (SqlDataAdapter ad = new SqlDataAdapter(selectSQL, this._dbConnection))
            {
                this.AddDbParameter(sqlParameterCollection, ad.SelectCommand.Parameters);

                if (this._isTransacting && ad.SelectCommand != null)
                    ad.SelectCommand.Transaction = this._dbTransaction;

                DataSet ds = new DataSet();
                ad.Fill(ds, dataTableName);
                return ds;
            }
        }

        /// <summary>
        /// 执行 SELECT SQL 语句，返回 DataSet，其中可以同时包含多个DataTable
        /// </summary>
        /// <param name="selectSQL">生成数据集的 Select SQL</param>
        /// <param name="sqlParameterCollection">SQL 参数集合</param>
        /// <returns>生成数据集</returns>
        public override DataSet ExeSqlForDataSetWithMultiDataTable(string selectSQL, SqlParameterCollection sqlParameterCollection)
        {
            selectSQL = Bigdesk2010.Security.AntiSqlInjection.GetSafeSql(selectSQL);
            using (SqlDataAdapter ad = new SqlDataAdapter(selectSQL, this._dbConnection))
            {
                this.AddDbParameter(sqlParameterCollection, ad.SelectCommand.Parameters);

                if (this._isTransacting && ad.SelectCommand != null)
                    ad.SelectCommand.Transaction = this._dbTransaction;

                DataSet ds = new DataSet();
                ad.Fill(ds);
                return ds;
            }
        }

        /// <summary>
        /// 执行 SELECT SQL 语句，将新数据表加入到 DataSet 中.
        /// </summary>
        /// <param name="selectSQL">生成数据集的 Select SQL</param>
        /// <param name="sqlParameterCollection">SQL 参数集合</param>
        /// <param name="dataTableName">数据表名，不必与数据库中的一致</param>
        /// <param name="dataSet">目标数据集</param>
        public override void AddTableToDataSet(string selectSQL, SqlParameterCollection sqlParameterCollection, string dataTableName, DataSet dataSet)
        {
            selectSQL = Bigdesk2010.Security.AntiSqlInjection.GetSafeSql(selectSQL);
            using (SqlDataAdapter ad = new SqlDataAdapter(selectSQL, this._dbConnection))
            {
                this.AddDbParameter(sqlParameterCollection, ad.SelectCommand.Parameters);

                if (this._isTransacting && ad.SelectCommand != null)
                    ad.SelectCommand.Transaction = this._dbTransaction;

                ad.Fill(dataSet, dataTableName);
            }
        }

        #endregion DataSet

        #endregion 通过执行 SELECT SQL 语句提取数据的相关方法

        #region 通过执行 INSERT/DELETE/UPDATE SQL 语句更新数据的相关方法

        /// <summary>
        /// 执行 INSERT/DELETE/UPDATE SQL 语句, 返回受影响的行数,当前为 SELECT SQL 语句时，返回 -1
        /// </summary>
        /// <param name="commandSQL">INSERT/DELETE/UPDATE SQL 语句</param>
        /// <param name="sqlParameterCollection">SQL 参数集合</param>
        /// <returns>返回受影响的行数</returns>
        public override int ExecuteNonQuerySql(string commandSQL, SqlParameterCollection sqlParameterCollection)
        {
            commandSQL = Security.AntiSqlInjection.GetSafeSql(commandSQL);
            return ExecuteNonQuerySql2(commandSQL, sqlParameterCollection);
        }

        /// <summary>
        /// 注意，本函数不对commandSQL进行安全处理，以允许批量执行sql语句
        /// </summary>
        /// <param name="commandSQL"></param>
        /// <param name="sqlParameterCollection"></param>
        /// <returns></returns>
        public override int ExecuteNonQuerySql2(string commandSQL, SqlParameterCollection sqlParameterCollection)
        {
            int result = -1;
            SqlCommand cmd = new SqlCommand(commandSQL, this._dbConnection);

            this.AddDbParameter(sqlParameterCollection, cmd.Parameters);

            if (this._isTransacting)
            {
                cmd.Transaction = this._dbTransaction;
                result = cmd.ExecuteNonQuery();
            }
            else
            {
                this.Open();
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                finally
                {
                    this.Close();
                }
            }
            return result;
        }

        #endregion 通过执行 INSERT/DELETE/UPDATE SQL 语句更新数据的相关方法

        #region 更新数据集的相关方法

        /// <summary>
        /// 更新数据表
        /// </summary>
        /// <param name="selectSQL">生成数据集的 SELECT SQL 语句</param>
        /// <param name="sqlParameterCollection">SQL 参数集合</param>
        /// <param name="dataTable">需要更新的数据集</param>
        /// <param name="commandTimeout">数据库更新的超时时间,0或负数表示采用缺省设置</param>
        public override bool Update(string selectSQL, SqlParameterCollection sqlParameterCollection, DataTable dataTable, int commandTimeout)
        {
            selectSQL = Bigdesk2010.Security.AntiSqlInjection.GetSafeSql(selectSQL);
            using (SqlDataAdapter ad = new SqlDataAdapter(selectSQL, this._dbConnection))
            {
                this.AddDbParameter(sqlParameterCollection, ad.SelectCommand.Parameters);

                SqlCommandBuilder objComBuilder = new SqlCommandBuilder(ad);
                if (this._isTransacting)
                {
                    ad.SelectCommand.Transaction = this._dbTransaction;
                    if (ad.DeleteCommand != null) ad.DeleteCommand.Transaction = this._dbTransaction;
                    if (ad.InsertCommand != null) ad.InsertCommand.Transaction = this._dbTransaction;
                    if (ad.UpdateCommand != null) ad.UpdateCommand.Transaction = this._dbTransaction;
                }

                if (commandTimeout > 0)
                {
                    ad.SelectCommand.CommandTimeout = commandTimeout;
                    if (ad.DeleteCommand != null) ad.DeleteCommand.CommandTimeout = commandTimeout;
                    if (ad.InsertCommand != null) ad.InsertCommand.CommandTimeout = commandTimeout;
                    if (ad.UpdateCommand != null) ad.UpdateCommand.CommandTimeout = commandTimeout;
                }
                return  ad.Update(dataTable)>0;
            }
        }

        /// <summary>
        /// 更新数据集中的某数据表
        /// </summary>
        /// <param name="selectSQL">生成数据集的 SELECT SQL 语句</param>
        /// <param name="sqlParameterCollection">SQL 参数集合</param>
        /// <param name="dataTableName">数据表名，不必与数据库中的一致</param>
        /// <param name="dataSet">需要更新的数据集</param>
        /// <param name="commandTimeout">数据库更新的超时时间,0或负数表示采用缺省设置</param>
        public override bool Update(string selectSQL, SqlParameterCollection sqlParameterCollection, string dataTableName, DataSet dataSet, int commandTimeout)
        {
            selectSQL = Bigdesk2010.Security.AntiSqlInjection.GetSafeSql(selectSQL);
            using (SqlDataAdapter ad = new SqlDataAdapter(selectSQL, this._dbConnection))
            {
                this.AddDbParameter(sqlParameterCollection, ad.SelectCommand.Parameters);

                SqlCommandBuilder objComBuilder = new SqlCommandBuilder(ad);
                if (this._isTransacting)
                {
                    ad.SelectCommand.Transaction = this._dbTransaction;
                    if (ad.DeleteCommand != null) ad.DeleteCommand.Transaction = this._dbTransaction;
                    if (ad.InsertCommand != null) ad.InsertCommand.Transaction = this._dbTransaction;
                    if (ad.UpdateCommand != null) ad.UpdateCommand.Transaction = this._dbTransaction;
                }

                if (commandTimeout > 0)
                {
                    ad.SelectCommand.CommandTimeout = commandTimeout;
                    if (ad.DeleteCommand != null) ad.DeleteCommand.CommandTimeout = commandTimeout;
                    if (ad.InsertCommand != null) ad.InsertCommand.CommandTimeout = commandTimeout;
                    if (ad.UpdateCommand != null) ad.UpdateCommand.CommandTimeout = commandTimeout;
                }

                return  ad.Update(dataSet, dataTableName)>0;
            }
        }

        #endregion 通过更新数据集更新数据的相关方法

        /// <summary>
        /// 返回数据库服务器端当前日期时间
        /// </summary>
        /// <returns></returns>
        public override DateTime GetCurrentDateTime()
        {
            return Convert.ToDateTime(ExeSqlForObject("select getdate()", null));
        }

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

            switch (MyDataBaseType)
            {
                case DataBaseType.SQLSERVER2000:
                    {
                        const string FirstPagedQuerySQLText = "select top {0} * from ({1}) as ___table0 order by {2}";
                        const string PagedQuerySQLText = "select * from (select top {0} * from (select top {1} * from ({2}) as ___table0 order by {3}) as ___table1 order by {4}) as ___table2 order by {3}";

                        if (pageData.IsFirstPage)
                        {
                            return string.Format(FirstPagedQuerySQLText, pageData.RecordCountInPage, querySQL, orderby);
                        }

                        // 定义一个配置数组用来填充查询语句
                        object[] configFields = new object[5];
                        configFields[0] = pageData.RecordCountInPage;
                        configFields[1] = pageData.LastIndexInPage + 1;
                        configFields[2] = querySQL;
                        configFields[3] = orderby;
                        configFields[4] = this.GetOrderbyDesc(orderby); // 和 orderby 的排序相反的排序字段字符串

                        // 填充查询语句
                        return string.Format(PagedQuerySQLText, configFields);
                    }
                case DataBaseType.SQLSERVER2005:
                case DataBaseType.SQLSERVER2008:
                    {
                        //对sql2005, sql2008，换成下面一种写法，以提高查询效率
                        const string FirstPagedQuerySQLText = "select top {0} * from ({1}) as ___table0 order by {2}";
                        const string PagedQuerySQLText = @"select top {0} * from (select row_number() over(order by {1}) 
                        as ___rownumber,* from ({2}) as ___table0) as ___table1 where ___rownumber>={3} order by ___rownumber";

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
                default:
                    throw new Exception("不正确的数据库类型" + MyDataBaseType.ToString());
            }
        }

        /// <summary>
        /// SQL 查询记录总数语句
        /// </summary>
        /// <param name="querySQL">SQL 语句，注意不带排序字段</param>
        /// <returns>返回记录总数 SQL 语句</returns>
        protected override string GetRowCountQuerySQL(string querySQL)
        {
            const string RowCountSQL = "select count(1) from ({0})  as ___table0";
            return string.Format(RowCountSQL, querySQL);
        }
    }
}
