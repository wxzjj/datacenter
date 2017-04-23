using System;
using System.Configuration;

namespace Bigdesk8.Data
{
    /// <summary>
    /// 创建一个 Factory 类，实现自动数据库切换的管理
    /// (随着数据库种类的增加，判断的方法可能会有所变化，应当根据实际情况来做相应的调整)
    /// </summary>
	public sealed class DBOperatorFactory
	{
		/// <summary>
		/// 连接数据源，创建数据库操作对象
		/// </summary>
		/// <param name="connectionStringSetting">连接数据源的字符串</param>
		/// <returns>数据库操作对象</returns>
		[Obsolete("请改用GetDBOperator(string connectionString, string dataBaseType)")]
		public static DBOperator GetDBOperator(ConnectionStringSettings connectionStringSetting)
		{
			if (connectionStringSetting == null || connectionStringSetting.ProviderName == null)
				throw new Exception("ConnectionStringSettings.ProviderName 配置错误或者 DBOperator 不支持的数据库类型!");

			switch ((DataBaseType)Enum.Parse(typeof(DataBaseType), connectionStringSetting.ProviderName.Trim().ToUpper()))
			{
				case DataBaseType.SQLSERVER2000:
					return new SqlServerDbOperator(connectionStringSetting);
				case DataBaseType.SQLSERVER2005:
					return new SqlServerDbOperator(connectionStringSetting);
				case DataBaseType.SQLSERVER2008:
					return new SqlServerDbOperator(connectionStringSetting);
				case DataBaseType.ACCESS2000:
					return new OleDbOperator(connectionStringSetting);
				default:
					throw new Exception("ProviderName 配置错误或者 DBOperator 不支持的数据库类型!");
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="dataBaseType"></param>
		/// <returns></returns>
		public static DBOperator GetDBOperator(string connectionString, string dataBaseType)
		{
			try
			{
				switch ((DataBaseType)Enum.Parse(typeof(DataBaseType), dataBaseType.ToUpper()))
				{
					case DataBaseType.SQLSERVER2000:
						return new SqlServerDbOperator(connectionString, DataBaseType.SQLSERVER2000);
					case DataBaseType.SQLSERVER2005:
						return new SqlServerDbOperator(connectionString, DataBaseType.SQLSERVER2005);
					case DataBaseType.SQLSERVER2008:
						return new SqlServerDbOperator(connectionString, DataBaseType.SQLSERVER2008);
					case DataBaseType.ACCESS2000:
						return new OleDbOperator(connectionString, DataBaseType.ACCESS2000);
					case DataBaseType.ORACLE11G:
						return new OracleOperator(connectionString, DataBaseType.ORACLE11G);
					default:
						throw new Exception("ProviderName 配置错误或者 DBOperator 不支持的数据库类型!");
				}
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="dataBaseType"></param>
		/// <returns></returns>
		public static DBOperator GetDBOperator(string connectionString, string dataBaseType, int commandTimeout)
		{
			try
			{
				switch ((DataBaseType)Enum.Parse(typeof(DataBaseType), dataBaseType.ToUpper()))
				{
					case DataBaseType.SQLSERVER2000:
						return new SqlServerDbOperator(connectionString, DataBaseType.SQLSERVER2000, commandTimeout);
					case DataBaseType.SQLSERVER2005:
						return new SqlServerDbOperator(connectionString, DataBaseType.SQLSERVER2005, commandTimeout);
					case DataBaseType.SQLSERVER2008:
						return new SqlServerDbOperator(connectionString, DataBaseType.SQLSERVER2008, commandTimeout);
					case DataBaseType.ACCESS2000:
						return new OleDbOperator(connectionString, DataBaseType.ACCESS2000);
					case DataBaseType.ORACLE11G:
						return new OracleOperator(connectionString, DataBaseType.ORACLE11G);
					default:
						throw new Exception("ProviderName 配置错误或者 DBOperator 不支持的数据库类型!");
				}
			}
			catch (Exception e)
			{
				throw e;
			}
		}
	}

    /// <summary>
    /// 
    /// </summary>
    public enum DataBaseType
    {
        /// <summary>
        /// 
        /// </summary>
        SQLSERVER2000 = 1,

        /// <summary>
        /// 
        /// </summary>
        SQLSERVER2005 = 2,

        /// <summary>
        /// 
        /// </summary>
        SQLSERVER2008 = 3,

        /// <summary>
        /// 
        /// </summary>
        ORACLE11G = 11,

        /// <summary>
        /// 
        /// </summary>
        ACCESS2000 = 21,
    }
}
