using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;

namespace Bigdesk2010.Data
{
    /// <summary>
    /// SQL 参数集合
    /// </summary>
    internal sealed class OleDbParameterCollection : SqlParameterCollection
    {
        /// <summary>
        /// 在给定参数名称和参数值的情况下，将 <see cref="OleDbParameter"/> 添加到 <see cref="SqlParameterCollection"/> 中。
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">要添加到集合中的 <see cref="OleDbParameter"/> 的 <see cref="OleDbParameter"/>.Value。</param>
        /// <returns>新 <see cref="OleDbParameter"/> 对象的索引。</returns>
        public override DbParameter Add(string parameterName, object value)
        {
            OleDbParameter sp = new OleDbParameter();
            sp.ParameterName = parameterName;
            sp.Value = value;
            this.Add(sp);

            return sp;
        }

        /// <summary>
        /// 在给定参数名称、数据类型和参数值的情况下，将 <see cref="OleDbParameter"/> 添加到 <see cref="SqlParameterCollection"/> 中。
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">要添加到集合中的 <see cref="OleDbParameter"/> 的 <see cref="OleDbParameter"/>.Value。</param>
        /// <returns>新 <see cref="OleDbParameter"/> 对象的索引。</returns>
        public override DbParameter Add(string parameterName, DbType dataType, object value)
        {
            OleDbParameter sp = new OleDbParameter();
            sp.ParameterName = parameterName;
            sp.DbType = dataType;
            sp.Value = value;
            this.Add(sp);

            return sp;
        }

        /// <summary>
        /// 在给定参数名称、数据类型、参数值和列的长度的情况下，将 <see cref="OleDbParameter"/> 添加到 <see cref="SqlParameterCollection"/> 中。
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="size">列的长度</param>
        /// <param name="value">要添加到集合中的 <see cref="OleDbParameter"/> 的 <see cref="OleDbParameter"/>.Value。</param>
        /// <returns>新 <see cref="OleDbParameter"/> 对象的索引。</returns>
        public override DbParameter Add(string parameterName, DbType dataType, int size, object value)
        {
            OleDbParameter sp = new OleDbParameter();
            sp.ParameterName = parameterName;
            sp.DbType = dataType;
            sp.Size = size;
            sp.Value = value;
            this.Add(sp);

            return sp;
        }
    }
}
