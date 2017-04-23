using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IntegrativeShow2;
using Bigdesk8.Data;

namespace IntegrativeShow2
{
    //文 件  名：DataHandle.cs 
    //作      用：数据增删改查基本操作
    //创 建 人 ：李贯涛 
    //创建日期：2015-3-27
    //修 改 人 ：李贯涛
    //修改日期 : 2015-3-27

    /// <summary>
    /// 数据操作层
    /// </summary>
    public class DataHandle
    {
        /// <summary>
        /// 数据库底层操作实例化对象
        /// </summary>
        public DBOperator DB { get; set; }
        /// <summary>
        /// 数据库操作SQL语句
        /// </summary>
        public string strSQL;
        /// <summary>
        /// 操作执行结果
        /// </summary>
        protected string strResult;
        /// <summary>
        /// SQL参数集
        /// </summary>
        public SqlParameterCollection spc;

        public string orderBy = String.Empty;




        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbn">数据库名称</param>
        public DataHandle(DataBaseName dbn)
        {
            //根据不同数据库名称初始化不同数据操作（改变连接字符串）
            switch(dbn)
            {
                //case DataBaseName.SCIC60: DB = WebCommon.GetDB_WJSJZX(); break;
                //case DataBaseName.WebPlat50: DB = WebCommon.GetDB_WEBPLA50(); break;
                //case DataBaseName.DBSZHCACredit: DB = WebCommon.GetDB_DBSZHCACREDIT(); break;
                case DataBaseName.WJSJZX: DB = WebCommon.GetDB_WJSJZX(); break;
            }
            //初始化参数
            spc = DB.CreateSqlParameterCollection();
        }

        /// <summary>
        /// 执行查询操作
        /// </summary>
        /// <returns>数据集</returns>
        public DataTable Read()
        {
            if (!string.IsNullOrEmpty(orderBy.Trim()))
                strSQL = strSQL + " order by "+ orderBy;

            return DB.ExeSqlForDataTable(strSQL, spc, "dt");
        }

        /// <summary>
        /// 执行删除操作
        /// </summary>
        /// <returns>删除语句影响的行数</returns>
        public bool Delete()
        {
            try
            {
                int iExecuteResult =  DB.ExecuteNonQuerySql(strSQL, spc);
                //执行结果影响行数若大于0才返回True
                if (iExecuteResult > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 执行数据更新操作（修改和新增）
        /// <para>新增：SQL语句用于获取新且为空表架构</para>
        /// <para>修改：SQL语句用于获取关键字相关记录</para>
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <returns>操作结果</returns>
        public bool Update(DataTable dt)
        {
            try
            {
                DB.Update(strSQL, spc, dt);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }    
}
