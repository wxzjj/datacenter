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
using System.Reflection;
using Bigdesk8.Data;

namespace IntegrativeShow2
{
    //文 件  名：InterfaceBusiness.cs
    //作      用：综合监管业务层接口
    //创 建 人 ：李贯涛 
    //创建日期：2015-3-27
    //修 改 人 ：李贯涛
    //修改日期 : 2015-3-27

    /// <summary>
    /// 综合监管业务层接口
    /// </summary>
    public interface IHandleBusiness
    {
        /// <summary>
        /// 数据查询接口(+3重载)
        /// </summary>
        /// <param name="Instance">需加载的SQL处理方法</param>
        /// <returns>数据集</returns>
        DataTable SearchInfo(string Instance);

        /// <summary>
        /// 数据查询接口(+3重载)
        /// </summary>
        /// <param name="Instance">需加载的SQL处理方法</param>
        /// <param name="strParas">参数集</param>
        /// <returns>数据集</returns>
        DataTable SearchInfo(string Instance,string[] strParas);

        /// <summary>
        /// 数据查询接口(+3重载)
        /// </summary>
        /// <param name="Instance">需加载的SQL处理方法</param>
        /// <param name="cl">页面控件</param>
        /// <returns>数据集</returns>
        DataTable SearchInfo(string Instance, Control cl);

        /// <summary>
        /// 数据查询接口(+3重载)
        /// </summary>
        /// <param name="Instance">需加载的SQL处理方法</param>
        /// <param name="strParas">参数集</param>
        /// <param name="cl">页面控件</param>
        /// <returns>数据集</returns>
        DataTable SearchInfo(string Instance, string[] strParas, Control cl);

        /// <summary>
        /// 数据查询接口(+3重载)
        /// </summary>
        /// <param name="Instance">需加载的SQL处理方法</param>
        /// <param name="strParas">参数集</param>
        /// <param name="cl">页面控件</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页记录数</param>
        void SearchInfo(string Instance, string[] strParas, Control cl,string gridId,int pageIndex,int pageSize);

        /// <summary>
        /// 删除数据接口
        /// </summary>
        /// <param name="Instance">需加载的SQL处理方法</param>
        /// <param name="strKeys">需要删除数据的索引</param>
        /// <returns>操作结果</returns>
        bool DelInfo(string Instance,string[] strKeys);

        /// <summary>
        /// 插入数据接口
        /// </summary>
        /// <param name="Instance">需加载的SQL处理方法</param>
        /// <param name="dt">数据源</param>
        /// <returns>操作结果</returns>
        bool InsertInfo(string Instance,DataTable dt);

        /// <summary>
        /// 更新数据的接口
        /// </summary>
        /// <param name="Instance">需加载的SQL处理方法</param>
        /// <param name="dt">数据源</param>
        /// <param name="strKeys">需更新数据的索引</param>
        /// <returns>操作结果</returns>
        bool AlterInfo(string Instance,DataTable dt, string[] strKeys);
    }

    /// <summary>
    /// SQL查询语句处理接口
    /// </summary>
    public interface IHandleSQL
    {
        /// <summary>
        /// 处理SQL和参数方法(+3重载)
        /// </summary>
        /// <param name="dh">数据处理对象</param>
        void HandleSQL(DataHandle dh);

        /// <summary>
        /// 处理SQL和参数方法(+3重载)
        /// </summary>
        /// <param name="dh">数据处理对象</param>
        /// <param name="strParas">参数组</param>
        void HandleSQL(DataHandle dh, string[] strParas);

        /// <summary>
        /// 处理SQL和参数方法(+3重载)
        /// </summary>
        /// <param name="dh">数据处理对象</param>
        /// <param name="Control">控件集合</param>
        void HandleSQL(DataHandle dh, Control cl);

        /// <summary>
        /// 处理SQL和参数方法(+3重载)
        /// </summary>
        /// <param name="dh">数据处理对象</param>
        /// <param name="strParas">参数组</param>
        /// <param name="cl">控件集合</param>
        void HandleSQL(DataHandle dh, string[] strParas, Control cl);




    }
}
