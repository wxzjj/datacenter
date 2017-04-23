using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.Common;

namespace Bigdesk2010.Data
{
    /// <summary>
    /// 通用数据库访问抽象类
    /// <![CDATA[
    /// 
    /// 客户端代码示例：
    /// 
    /// ConnectionStringSettings connectionStringSettings = new ConnectionStringSettings();
    /// connectionStringSettings.ProviderName = "sqlserver2005";
    /// connectionStringSettings.ConnectionString = "Provider=SQLOLEDB;Data Source=(local);Integrated Security=SSPI";
    /// 
    /// DBOperator db = DBOperatorFactory.GetDBOperator(connectionStringSettings);
    /// db.BeginTransaction();
    /// try
    /// {
    ///     string sql1 = string.Empty;
    ///     SqlParameterCollection spc1 = db.CreateSqlParameterCollection();
    ///     spc1.Add("parameterName1", 1);
    ///     spc1.Add("parameterName2", 2);
    ///     spc1.Add("parameterName3", 2);
    ///     db.ExecuteNonQuerySql(sql1, spc1);
    /// 
    ///     string sql2 = string.Empty;
    ///     SqlParameterCollection spc2 = db.CreateSqlParameterCollection();
    ///     spc2.Add("parameterName1", "a");
    ///     spc2.Add("parameterName4", "b");
    ///     spc2.Add("parameterName5", "c");
    ///     DataTable dataTable = new DataTable();
    ///     db.Update(sql2, spc2, dataTable);
    /// 
    ///     db.CommitTransaction();
    /// }
    /// catch (Exception ex)
    /// {
    ///     db.RollbackTransaction();
    /// 
    ///     throw ex;
    ///  }
    ///  
    /// ]]>
    /// </summary>
    public abstract class DBOperator
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionStringSetting">连接字符串</param>
        public DBOperator(ConnectionStringSettings connectionStringSetting)
        {
            this.ConnectionStringSetting = connectionStringSetting;
        }

        /// <summary>
        /// 
        /// </summary>
        public DBOperator()
        {

        }

        #endregion 构造函数

        #region 操作数据库的基础

        /// <summary>
        /// 数据库连接字符串设置
        /// </summary>
        public ConnectionStringSettings ConnectionStringSetting { get; private set; }

        /// <summary>
        /// SQL 参数集合
        /// </summary>
        /// <returns>一个 <see cref="SqlParameterCollection"/> 对象。</returns>
        public abstract SqlParameterCollection CreateSqlParameterCollection();

        /// <summary>
        /// 创建 <see cref="DbParameter"/> 对象的新实例。
        /// </summary>
        /// <returns>一个 <see cref="DbParameter"/> 对象。</returns>
        public abstract DbParameter CreateDbParameter();

        /// <summary>
        /// 开始数据库事务
        /// </summary>
        public abstract void BeginTransaction();

        /// <summary>
        /// 以指定的隔离级别启动数据库事务。
        /// </summary>
        /// <param name="isolationLevel">指定事务的隔离级别。</param>
        /// <returns>表示新事务的对象。</returns>
        public abstract void BeginTransaction(IsolationLevel isolationLevel);

        /// <summary>
        /// 提交数据库事务
        /// </summary>
        public abstract void CommitTransaction();

        /// <summary>
        /// 回滚数据库事务
        /// </summary>
        public abstract void RollbackTransaction();

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
        public abstract DataTable ExeSqlForDataTable(string selectSQL, SqlParameterCollection sqlParameterCollection, string dataTableName);

        #endregion DataTable

        #region DataSet

        /// <summary>
        /// 执行 SELECT SQL 语句，返回 DataSet
        /// </summary>
        /// <param name="selectSQL">生成数据集的 Select SQL</param>
        /// <param name="sqlParameterCollection">SQL 参数集合</param>
        /// <param name="dataTableName">数据表名，不必与数据库的一致</param>
        /// <returns>生成数据集</returns>
        public abstract DataSet ExeSqlForDataSet(string selectSQL, SqlParameterCollection sqlParameterCollection, string dataTableName);

        /// <summary>
        /// 执行 SELECT SQL 语句，返回 DataSet，其中可以同时包含多个DataTable
        /// </summary>
        /// <param name="selectSQL">生成数据集的 Select SQL</param>
        /// <param name="sqlParameterCollection">SQL 参数集合</param>
        /// <returns>生成数据集</returns>
        public abstract DataSet ExeSqlForDataSetWithMultiDataTable(string selectSQL, SqlParameterCollection sqlParameterCollection);

        /// <summary>
        /// 执行 SELECT SQL 语句，将新数据表加入到 DataSet 中.
        /// </summary>
        /// <param name="selectSQL">生成数据集的 Select SQL</param>
        /// <param name="sqlParameterCollection">SQL 参数集合</param>
        /// <param name="dataTableName">数据表名，不必与数据库中的一致</param>
        /// <param name="dataSet">目标数据集</param>
        public abstract void AddTableToDataSet(string selectSQL, SqlParameterCollection sqlParameterCollection, string dataTableName, DataSet dataSet);

        #endregion DataSet

        #region string/object

        /// <summary>
        /// 执行 SELECT SQL 语句，得到一个 object 类型的值
        /// </summary>
        /// <param name="selectSQL">执行的 Select SQL 语句</param>
        /// <param name="sqlParameterCollection">SQL 参数集合</param>
        /// <returns>得到一个 object 类型的值</returns>
        public object ExeSqlForObject(string selectSQL, SqlParameterCollection sqlParameterCollection)
        {
            DataTable dt = this.ExeSqlForDataTable(selectSQL, sqlParameterCollection, "dataTableName");
            if (dt.Rows.Count > 0)
            {
                object obj = dt.Rows[0][0];
                return (obj == DBNull.Value) ? null : obj;
            }
            return null;
        }

        /// <summary>
        /// 执行 SELECT SQL 语句，得到一个字符串值
        /// </summary>
        /// <param name="selectSQL">执行的 Select SQL 语句</param>
        /// <param name="sqlParameterCollection">SQL 参数集合</param>
        /// <returns>得到一个字符串值</returns>
        public string ExeSqlForString(string selectSQL, SqlParameterCollection sqlParameterCollection)
        {
            return this.ExeSqlForObject(selectSQL, sqlParameterCollection).ToString2();
        }

        #endregion string/object

        #region string[]/ArrayList

        /// <summary>
        /// 执行 SELECT SQL 语句，得到一个 string 类型的数组
        /// </summary>
        /// <param name="selectSQL">执行的 Select SQL 语句</param>
        /// <param name="sqlParameterCollection">SQL 参数集合</param>
        /// <returns>得到一个字符串数组</returns>
        public string[] ExeSqlForStringArray(string selectSQL, SqlParameterCollection sqlParameterCollection)
        {
            ArrayList al = this.ExeSqlForArrayList(selectSQL, sqlParameterCollection);
            return (string[])al.ToArray(typeof(string));
        }

        /// <summary>
        /// 执行 SELECT SQL 语句，得到一个 ArrayList 类型的数组
        /// </summary>
        /// <param name="selectSQL">执行的 Select SQL 语句</param>
        /// <param name="sqlParameterCollection">SQL 参数集合</param>
        /// <returns>得到一个 ArrayList</returns>
        public ArrayList ExeSqlForArrayList(string selectSQL, SqlParameterCollection sqlParameterCollection)
        {
            DataTable dt = this.ExeSqlForDataTable(selectSQL, sqlParameterCollection, "dataTableName");

            ArrayList al = new ArrayList();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                object obj = dt.Rows[i][0];

                if (obj == DBNull.Value)
                    obj = null;

                al.Add(obj);
            }
            return al;
        }

        #endregion string[]/Array

        #endregion 通过执行 SELECT SQL 语句提取数据的相关方法

        #region 通过执行 INSERT/DELETE/UPDATE SQL 语句更新数据的相关方法

        /// <summary>
        /// 执行 INSERT/DELETE/UPDATE SQL 语句, 返回受影响的行数,当前为 SELECT SQL 语句时，返回 -1
        /// </summary>
        /// <param name="commandSQL">INSERT/DELETE/UPDATE SQL 语句</param>
        /// <param name="sqlParameterCollection">SQL 参数集合</param>
        /// <returns>返回受影响的行数</returns>
        public abstract int ExecuteNonQuerySql(string commandSQL, SqlParameterCollection sqlParameterCollection);

        /// <summary>
        /// 注意，本函数不对commandSQL进行安全处理，以允许批量执行sql语句
        /// </summary>
        /// <param name="commandSQL"></param>
        /// <param name="sqlParameterCollection"></param>
        /// <returns></returns>
        public abstract int ExecuteNonQuerySql2(string commandSQL, SqlParameterCollection sqlParameterCollection);

        #endregion 通过执行 INSERT/DELETE/UPDATE SQL 语句更新数据的相关方法

        #region 更新数据集的相关方法

        /// <summary>
        /// 更新数据表
        /// </summary>
        /// <param name="selectSQL">生成数据集的 SELECT SQL 语句</param>
        /// <param name="sqlParameterCollection">SQL 参数集合</param>
        /// <param name="dataTable">需要更新的数据集</param>
        public bool Update(string selectSQL, SqlParameterCollection sqlParameterCollection, DataTable dataTable)
        {
            return this.Update(selectSQL, sqlParameterCollection, dataTable, 0);
        }

        /// <summary>
        /// 更新数据表
        /// </summary>
        /// <param name="selectSQL">生成数据集的 SELECT SQL 语句</param>
        /// <param name="sqlParameterCollection">SQL 参数集合</param>
        /// <param name="dataTable">需要更新的数据集</param>
        /// <param name="commandTimeout">数据库更新的超时时间,0或负数表示采用缺省设置,单位:秒</param>
        public abstract bool Update(string selectSQL, SqlParameterCollection sqlParameterCollection, DataTable dataTable, int commandTimeout);

        /// <summary>
        /// 更新数据集中的某数据表
        /// </summary>
        /// <param name="selectSQL">生成数据集的 SELECT SQL 语句</param>
        /// <param name="sqlParameterCollection">SQL 参数集合</param>
        /// <param name="dataTableName">数据表名，不必与数据库中的一致</param>
        /// <param name="dataSet">需要更新的数据集</param>
        public virtual bool Update(string selectSQL, SqlParameterCollection sqlParameterCollection, string dataTableName, DataSet dataSet)
        {
            return this.Update(selectSQL, sqlParameterCollection, dataTableName, dataSet, 0);
        }

        /// <summary>
        /// 更新数据集中的某数据表
        /// </summary>
        /// <param name="selectSQL">生成数据集的 SELECT SQL 语句</param>
        /// <param name="sqlParameterCollection">SQL 参数集合</param>
        /// <param name="dataTableName">数据表名，不必与数据库中的一致</param>
        /// <param name="dataSet">需要更新的数据集</param>
        /// <param name="commandTimeout">数据库更新的超时时间,0或负数表示采用缺省设置</param>
        public abstract bool Update(string selectSQL, SqlParameterCollection sqlParameterCollection, string dataTableName, DataSet dataSet, int commandTimeout);

        #endregion 通过更新数据集更新数据的相关方法

        #region 通过执行 SELECT SQL 语句分页提取数据的相关方法

        /// <summary>
        /// 执行 SELECT SQL 语句，分页提取数据，返回 DataTable
        /// </summary>
        /// <param name="selectSQL">执行的 Select SQL 语句，注意：不要带 order by 语句</param>
        /// <param name="sqlParameterCollection">SQL 参数集合</param>
        /// <param name="dataTableName">数据表名，不必与数据库的一致</param>
        /// <param name="orderby">排序字段，注意：不要带 order by 关键字，如: order1 desc,order2 asc</param>
        /// <param name="pageSize">每页最大记录数</param>
        /// <param name="pageIndex">提取第几页的数据，从 0 开始。</param>
        /// <returns>生成数据集</returns>
        public DataTable ExeSqlForDataTable(string selectSQL, SqlParameterCollection sqlParameterCollection, string dataTableName, string orderby, int pageSize, int pageIndex)
        {
            int allRecordCount;
            return this.ExeSqlForDataTable(selectSQL, sqlParameterCollection, dataTableName, orderby, pageSize, pageIndex, out allRecordCount);
        }

        /// <summary>
        /// 执行 SELECT SQL 语句，分页提取数据，返回 DataTable 和 allRecordCount
        /// </summary>
        /// <param name="selectSQL">执行的 Select SQL 语句，注意：不要带 order by 语句</param>
        /// <param name="sqlParameterCollection">SQL 参数集合</param>
        /// <param name="dataTableName">数据表名，不必与数据库的一致</param>
        /// <param name="orderby">排序字段，注意：不要带 order by 关键字，如: order1 desc,order2 asc</param>
        /// <param name="pageSize">每页最大记录数</param>
        /// <param name="pageIndex">提取第几页的数据，从 0 开始。</param>
        /// <param name="allRecordCount">所有符合条件的记录总数</param>
        /// <returns>生成数据集</returns>
        public DataTable ExeSqlForDataTable(string selectSQL, SqlParameterCollection sqlParameterCollection, string dataTableName, string orderby, int pageSize, int pageIndex, out int allRecordCount)
        {
            allRecordCount = this.ExeSqlForObject(this.GetRowCountQuerySQL(selectSQL), sqlParameterCollection).ToInt32();

            PagedData pagedData = new PagedData(allRecordCount, pageSize, pageIndex);

            return this.ExeSqlForDataTable(this.GetPagedQuerySQL(selectSQL, orderby, pagedData), sqlParameterCollection, dataTableName);
        }

        public DataTable ExeSqlForDataTable(string selectSQL, SqlParameterCollection sqlParameterCollection, string dataTableName, string orderby, int pageSize, int pageIndex, out int allRecordCount, bool isAdd, int num)
        {
            allRecordCount = this.ExeSqlForObject(this.GetRowCountQuerySQL(selectSQL), sqlParameterCollection).ToInt32();
            if (isAdd)
            {
                allRecordCount += num;
            }
            PagedData pagedData = new PagedData(allRecordCount, pageSize, pageIndex);

            return this.ExeSqlForDataTable(this.GetPagedQuerySQL(selectSQL, orderby, pagedData), sqlParameterCollection, dataTableName);
        }
        /// <summary>
        /// 是否有追加数据的操作
        /// </summary>
        /// <param name="selectSQL"></param>
        /// <param name="sqlParameterCollection"></param>
        /// <param name="dataTableName"></param>
        /// <param name="orderby"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="allRecordCount"></param>
        /// <param name="isAppend">是否追加数据，如果是追加数据，下一页超出所有记录，返回null；不再返回最后的数据</param>
        /// <returns>生成数据集</returns>
        public DataTable ExeSqlForDataTable(string selectSQL, SqlParameterCollection sqlParameterCollection, string dataTableName, string orderby, int pageSize, int pageIndex, out int allRecordCount, bool isAppend)
        {
            allRecordCount = this.ExeSqlForObject(this.GetRowCountQuerySQL(selectSQL), sqlParameterCollection).ToInt32();
            if (isAppend)
            {
                if (allRecordCount <= pageSize * pageIndex)
                {
                    return null;
                }
            }

            return this.ExeSqlForDataTable(selectSQL, sqlParameterCollection, dataTableName, orderby, pageSize, pageIndex, out allRecordCount);
        }
        #region 分页语句

        /// <summary>
        /// SQL 分页查询 SQL 语句
        /// </summary>
        /// <param name="querySQL">SQL 语句，注意不带排序字段</param>
        /// <param name="orderby">排序字段，注意：不要带 order by 关键字，如: order1 desc,order2 asc</param>
        /// <param name="pageData">分页数据</param>
        /// <returns>返回整理过的分页 SQL 语句</returns>
        protected virtual string GetPagedQuerySQL(string querySQL, string orderby, PagedData pageData)
        {
            /*
            if (querySQL.IsEmpty()) throw new Exception("参数 querySQL 不能为空！");
            if (orderby.IsEmpty()) throw new Exception("参数 orderby 不能为空！");
            if (pageData == null) throw new Exception("参数 pageData 不能为空！");

            if (pageData.RecordCount == 0) return querySQL;

             //sql代码出现在DBOperator这个基类中是不合适的，因为不同的数据库SqlServer, Access, Oracle等的写法很可能不一样，缪卫华 2011-5-25
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
            */
            throw new NotImplementedException("如需使用本函数相关的功能，请各子类自己实现本函数，呵呵。");
        }

        /// <summary>
        /// SQL 查询记录总数语句
        /// </summary>
        /// <param name="querySQL">SQL 语句，注意不带排序字段</param>
        /// <returns>返回记录总数 SQL 语句</returns>
        protected virtual string GetRowCountQuerySQL(string querySQL)
        {
            /*sql代码出现在DBOperator这个基类中是不合适的，因为不同的数据库SqlServer, Access, Oracle等的写法很可能不一样，缪卫华 2011-5-25
             *const string RowCountSQL = "select count(1) from ({0}) as ___table0"; 
             *return string.Format(RowCountSQL, querySQL);
             */
            throw new NotImplementedException("如需使用本函数相关的功能，请各子类自己实现本函数，呵呵。");
        }

        /// <summary>
        /// 获得相反的排序字符串
        /// </summary>
        /// <param name="orderby">排序字符串</param>
        /// <returns>返回相反的排序字符串</returns>
        protected string GetOrderbyDesc(string orderby)
        {
            string[] orderbys = orderby.Split(',');
            const string asc = " asc";
            const string desc = " desc";
            const int ascLength = 3;
            const int descLength = 4;
            string result = "";

            foreach (string s in orderbys)
            {
                if (result != "")
                {
                    result += ",";
                }

                string s2 = s.TrimString();
                if (s2.EndsWith(desc, StringComparison.CurrentCultureIgnoreCase))
                {
                    result += s2.Substring(0, s2.Length - descLength) + asc;
                }
                else if (s2.EndsWith(asc, StringComparison.CurrentCultureIgnoreCase))
                {
                    result += s2.Substring(0, s2.Length - ascLength) + desc;
                }
                else
                {
                    result += s2 + desc;
                }
            }

            return result;
        }

        #endregion

        #endregion 通过执行 SELECT SQL 语句分页提取数据的相关方法

        /// <summary>
        /// 返回数据库服务器端当前日期时间
        /// </summary>
        /// <returns></returns>
        public abstract DateTime GetCurrentDateTime();
    }
}