using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using System.Data.Common;

namespace Bigdesk8.Data
{
    /// <summary>
    /// OleDb 数据库访问类	
    /// </summary>
    public class OleDbOperator : DBOperator
    {
        #region 私有成员

        private DataBaseType MyDataBaseType;

        /// <summary>
        /// 表示 OleDb 数据库的一个打开的连接
        /// </summary>
        private readonly OleDbConnection _dbConnection;

        /// <summary>
        /// SQL 事务
        /// </summary>
        private OleDbTransaction _dbTransaction;

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
        public OleDbOperator(ConnectionStringSettings connectionStringSetting)
            : base(connectionStringSetting)
        {
            this._dbConnection = new OleDbConnection(connectionStringSetting.ConnectionString);
        }

        public OleDbOperator(string connectionString, DataBaseType dataBaseType)
        {
            this._dbConnection = new OleDbConnection(connectionString);
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
        /// 将 <see cref="SqlParameterCollection"/> 对象中的参数加入到 <see cref="OleDbParameterCollection"/> 对象中。
        /// </summary>
        /// <param name="sqlParameterCollection"><see cref="SqlParameterCollection"/> 对象</param>
        /// <param name="oleDbParameterCollection"><see cref="OleDbParameterCollection"/> 对象</param>
        private void AddDbParameter(SqlParameterCollection sqlParameterCollection, System.Data.OleDb.OleDbParameterCollection oleDbParameterCollection)
        {
            if (sqlParameterCollection == null || oleDbParameterCollection == null) return;

            OleDbParameter[] clonedParameters = new OleDbParameter[sqlParameterCollection.Count];
            for (int i = 0, j = sqlParameterCollection.Count; i < j; i++)
            {
                OleDbParameter p = (OleDbParameter)((ICloneable)sqlParameterCollection[i]).Clone();

                // 检查未分配值的输入参数,将其分配以DBNull.Value.
                if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) && (p.Value == null))
                {
                    p.Value = DBNull.Value;
                }
                clonedParameters[i] = p;
            }
            oleDbParameterCollection.AddRange(clonedParameters);
        }

        #endregion 私有方法

        #region 操作数据库的基础

        /// <summary>
        /// SQL 参数集合
        /// </summary>
        /// <returns>一个 <see cref="SqlParameterCollection"/> 对象。</returns>
        public override SqlParameterCollection CreateSqlParameterCollection()
        {
            return new OleDbParameterCollection();
        }

        /// <summary>
        /// 创建 <see cref="DbParameter"/> 对象的新实例。
        /// </summary>
        /// <returns>一个 <see cref="DbParameter"/> 对象。</returns>
        public override DbParameter CreateDbParameter()
        {
            return new OleDbParameter();
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
            selectSQL = Bigdesk8.Security.AntiSqlInjection.GetSafeSql(selectSQL);
            DataTable dt = new DataTable(dataTableName);
            OleDbDataAdapter ad = new OleDbDataAdapter(selectSQL, this._dbConnection);

            this.AddDbParameter(sqlParameterCollection, ad.SelectCommand.Parameters);

            if (this._isTransacting && ad.SelectCommand != null)
                ad.SelectCommand.Transaction = this._dbTransaction;

            ad.Fill(dt);
            return dt;
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
            selectSQL = Bigdesk8.Security.AntiSqlInjection.GetSafeSql(selectSQL);
            DataSet ds = new DataSet();
            OleDbDataAdapter ad = new OleDbDataAdapter(selectSQL, this._dbConnection);

            this.AddDbParameter(sqlParameterCollection, ad.SelectCommand.Parameters);

            if (this._isTransacting && ad.SelectCommand != null)
                ad.SelectCommand.Transaction = this._dbTransaction;

            ad.Fill(ds, dataTableName);
            return ds;
        }

        /// <summary>
        /// 执行 SELECT SQL 语句，返回 DataSet，其中可以同时包含多个DataTable
        /// </summary>
        /// <param name="selectSQL">生成数据集的 Select SQL</param>
        /// <param name="sqlParameterCollection">SQL 参数集合</param>
        /// <returns>生成数据集</returns>
        public override DataSet ExeSqlForDataSetWithMultiDataTable(string selectSQL, SqlParameterCollection sqlParameterCollection)
        {
            selectSQL = Bigdesk8.Security.AntiSqlInjection.GetSafeSql(selectSQL);
            DataSet ds = new DataSet();
            OleDbDataAdapter ad = new OleDbDataAdapter(selectSQL, this._dbConnection);

            this.AddDbParameter(sqlParameterCollection, ad.SelectCommand.Parameters);

            if (this._isTransacting && ad.SelectCommand != null)
                ad.SelectCommand.Transaction = this._dbTransaction;

            ad.Fill(ds);
            return ds;
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
            selectSQL = Bigdesk8.Security.AntiSqlInjection.GetSafeSql(selectSQL);
            OleDbDataAdapter ad = new OleDbDataAdapter(selectSQL, this._dbConnection);

            this.AddDbParameter(sqlParameterCollection, ad.SelectCommand.Parameters);

            if (this._isTransacting && ad.SelectCommand != null)
                ad.SelectCommand.Transaction = this._dbTransaction;

            ad.Fill(dataSet, dataTableName);
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
            commandSQL = Bigdesk8.Security.AntiSqlInjection.GetSafeSql(commandSQL);
            return this.ExecuteNonQuerySql2(commandSQL, sqlParameterCollection);
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
            OleDbCommand cmd = new OleDbCommand(commandSQL, this._dbConnection);

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
            selectSQL = Bigdesk8.Security.AntiSqlInjection.GetSafeSql(selectSQL);
            OleDbDataAdapter ad = new OleDbDataAdapter(selectSQL, this._dbConnection);

            this.AddDbParameter(sqlParameterCollection, ad.SelectCommand.Parameters);

            OleDbCommandBuilder objComBuilder = new OleDbCommandBuilder(ad);
            if (this._isTransacting)
            {
                if (ad.SelectCommand != null) ad.SelectCommand.Transaction = this._dbTransaction;
                if (ad.DeleteCommand != null) ad.DeleteCommand.Transaction = this._dbTransaction;
                if (ad.InsertCommand != null) ad.InsertCommand.Transaction = this._dbTransaction;
                if (ad.UpdateCommand != null) ad.UpdateCommand.Transaction = this._dbTransaction;
            }

            if (commandTimeout > 0)
            {
                if (ad.SelectCommand != null) ad.SelectCommand.CommandTimeout = commandTimeout;
                if (ad.DeleteCommand != null) ad.DeleteCommand.CommandTimeout = commandTimeout;
                if (ad.InsertCommand != null) ad.InsertCommand.CommandTimeout = commandTimeout;
                if (ad.UpdateCommand != null) ad.UpdateCommand.CommandTimeout = commandTimeout;
            }

            return ad.Update(dataTable)>0;
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
            selectSQL = Bigdesk8.Security.AntiSqlInjection.GetSafeSql(selectSQL);
            OleDbDataAdapter ad = new OleDbDataAdapter(selectSQL, this._dbConnection);

            this.AddDbParameter(sqlParameterCollection, ad.SelectCommand.Parameters);

            OleDbCommandBuilder objComBuilder = new OleDbCommandBuilder(ad);
            if (this._isTransacting)
            {
                if (ad.SelectCommand != null) ad.SelectCommand.Transaction = this._dbTransaction;
                if (ad.DeleteCommand != null) ad.DeleteCommand.Transaction = this._dbTransaction;
                if (ad.InsertCommand != null) ad.InsertCommand.Transaction = this._dbTransaction;
                if (ad.UpdateCommand != null) ad.UpdateCommand.Transaction = this._dbTransaction;
            }

            if (commandTimeout > 0)
            {
                if (ad.SelectCommand != null) ad.SelectCommand.CommandTimeout = commandTimeout;
                if (ad.DeleteCommand != null) ad.DeleteCommand.CommandTimeout = commandTimeout;
                if (ad.InsertCommand != null) ad.InsertCommand.CommandTimeout = commandTimeout;
                if (ad.UpdateCommand != null) ad.UpdateCommand.CommandTimeout = commandTimeout;
            }

            return ad.Update(dataSet, dataTableName)>0;
        }

        #endregion 通过更新数据集更新数据的相关方法


        /// <summary>
        /// 返回数据库服务器端当前日期时间
        /// </summary>
        /// <returns></returns>
        public override DateTime GetCurrentDateTime()
        {
            return Convert.ToDateTime(ExeSqlForObject("select now()", null));
        }

        /// <summary>
        /// 未实现
        /// </summary>
        /// <returns></returns>
        public override object GetDataReader(string commandSQL, SqlParameterCollection sqlParameterCollection, CommandBehavior cmdBehavior)
        {
            return null;
        }
    }
}
