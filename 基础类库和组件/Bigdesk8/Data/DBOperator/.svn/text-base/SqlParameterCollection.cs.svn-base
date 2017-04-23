using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

namespace Bigdesk8.Data
{
    /// <summary>
    /// SQL 参数集合
    /// </summary>
    public abstract class SqlParameterCollection : List<DbParameter>
    {
        /// <summary>
        /// 在给定参数名称和参数值的情况下，将 <see cref="DbParameter"/> 添加到 <see cref="SqlParameterCollection"/> 中。
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">要添加到集合中的 <see cref="DbParameter"/> 的 <see cref="DbParameter"/>.Value。</param>
        /// <returns>新 <see cref="DbParameter"/> 对象的索引。</returns>
        public abstract DbParameter Add(string parameterName, object value);

        /// <summary>
        /// 在给定参数名称、数据类型和参数值的情况下，将 <see cref="DbParameter"/> 添加到 <see cref="SqlParameterCollection"/> 中。
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">要添加到集合中的 <see cref="DbParameter"/> 的 <see cref="DbParameter"/>.Value。</param>
        /// <returns>新 <see cref="DbParameter"/> 对象的索引。</returns>
        public abstract DbParameter Add(string parameterName, DbType dataType, object value);

        /// <summary>
        /// 在给定参数名称、数据类型、参数值和列的长度的情况下，将 <see cref="DbParameter"/> 添加到 <see cref="SqlParameterCollection"/> 中。
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="size">列的长度</param>
        /// <param name="value">要添加到集合中的 <see cref="DbParameter"/> 的 <see cref="DbParameter"/>.Value。</param>
        /// <returns>新 <see cref="DbParameter"/> 对象的索引。</returns>
        public abstract DbParameter Add(string parameterName, DbType dataType, int size, object value);
    }
}
