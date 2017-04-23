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
using System.Collections.Generic;
using Bigdesk8;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;


namespace IntegrativeShow2
{
    //文 件  名：InterfaceBusiness.cs
    //作      用：综合监管业务层实现
    //创 建 人 ：李贯涛 
    //创建日期：2015-3-27
    //修 改 人 ：李贯涛
    //修改日期 : 2015-3-27

    /// <summary>
    /// 基础增删改查实现
    /// </summary>
    public class HandleBusinessBase : IHandleBusiness
    {
        #region HandleBusinessBase成员
        /// <summary>
        /// 命名空间字符串常量
        /// </summary>
        protected const string strIntegrativeShow2 = "IntegrativeShow2";

        /// <summary>
        /// 数据处理实例化对象
        /// </summary>
        DataHandle dh;

        /// <summary>
        /// SQL处理实例化对象
        /// </summary>
        IHandleSQL ihs;
        #endregion

        #region IHandleBusiness 成员

        public DataTable SearchInfo(string Instance)
        {
            //初始化数据操作对象
            dh = new DataHandle(DataBaseName.WJSJZX);
            //初始化SQL处理对象
            ihs = (IHandleSQL)Assembly.Load(strIntegrativeShow2).CreateInstance(strIntegrativeShow2 + '.' + Instance);
            //SQL以及参数初始化
            ihs.HandleSQL(dh);
            //返回执行结果
            return dh.Read();
        }

        public DataTable SearchInfo(string Instance, string[] strParas)
        {
            //初始化数据操作对象
            dh = new DataHandle(DataBaseName.WJSJZX);
            //初始化SQL处理对象
            ihs = (IHandleSQL)Assembly.Load(strIntegrativeShow2).CreateInstance(strIntegrativeShow2 + '.' + Instance);
            //SQL以及参数初始化
            ihs.HandleSQL(dh, strParas);
            //返回执行结果
            return dh.Read();
        }

        public DataTable SearchInfo(string Instance, Control cl)
        {
            //初始化数据操作对象
            dh = new DataHandle(DataBaseName.WJSJZX);
            //初始化SQL处理对象
            ihs = (IHandleSQL)Assembly.Load(strIntegrativeShow2).CreateInstance(strIntegrativeShow2 + '.' + Instance);
            //SQL以及参数初始化
            ihs.HandleSQL(dh, cl);
            //返回执行结果
            return dh.Read();
        }

        public DataTable SearchInfo(string Instance, string[] strParas, Control cl)
        {
            //初始化数据操作对象
            dh = new DataHandle(DataBaseName.WJSJZX);
            //初始化SQL处理对象
            ihs = (IHandleSQL)Assembly.Load(strIntegrativeShow2).CreateInstance(strIntegrativeShow2 + '.' + Instance);
            //SQL以及参数初始化
            ihs.HandleSQL(dh, strParas, cl);

            //返回执行结果
            return dh.Read();
        }


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="Instance"></param>
        ///// <param name="strParas"></param>
        ///// <param name="cl">PowerDataGrid控件所属页面</param>
        ///// <param name="gridId">PowerDataGrid控件ID</param>
        ///// <param name="pageIndex">当前页索引</param>
        ///// <param name="pageSize">每页记录数</param>
        ///// <returns></returns>
        //public void SearchInfo(string Instance, Control cl, string gridId, int pageIndex, int pageSize)
        //{
        //    //初始化数据操作对象
        //    dh = new DataHandle(DataBaseName.WJSJZX);
        //    //初始化SQL处理对象
        //    ihs = (IHandleSQL)Assembly.Load(strIntegrativeShow2).CreateInstance(strIntegrativeShow2 + '.' + Instance);
        //    //SQL以及参数初始化
        //    ihs.HandleSQL(dh, cl);

        //    Page p = cl as Page;
        //    PowerDataGrid g = p.FindControl(gridId) as PowerDataGrid;

        //    int allRecordCount = 0;
        //    string tableName = "dt";
        //    DataTable dt = dh.DB.ExeSqlForDataTable(dh.strSQL, dh.spc, tableName, dh.orderBy, pageSize, pageIndex, out allRecordCount);
        //    g.RecordCount = allRecordCount;
        //    g.PageIndex = pageIndex;
        //    g.DataSource = dt;
        //    g.DataBind();

        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Instance"></param>
        /// <param name="strParas"></param>
        /// <param name="cl">PowerDataGrid控件所属页面</param>
        /// <param name="gridId">PowerDataGrid控件ID</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        public void SearchInfo(string Instance, string[] strParas, Control cl, string gridId, int pageIndex, int pageSize)
        {
            //初始化数据操作对象
            dh = new DataHandle(DataBaseName.WJSJZX);
            //初始化SQL处理对象
            ihs = (IHandleSQL)Assembly.Load(strIntegrativeShow2).CreateInstance(strIntegrativeShow2 + '.' + Instance);
            //SQL以及参数初始化
            if (strParas == null || strParas.Length == 0)
                ihs.HandleSQL(dh, cl);
            else
                ihs.HandleSQL(dh, strParas, cl);

            Page p = cl as Page;
            PowerDataGrid g = p.FindControl(gridId) as PowerDataGrid;

            int allRecordCount = 0;
            string tableName = "dt";
            DataTable dt = dh.DB.ExeSqlForDataTable(dh.strSQL, dh.spc, tableName, dh.orderBy, pageSize, pageIndex, out allRecordCount);
            g.RecordCount = allRecordCount;
            g.PageIndex = pageIndex;
            g.DataSource = dt;
            g.DataBind();

        }


        public bool DelInfo(string Instance, string[] strKeys)
        {
            //初始化数据操作对象
            dh = new DataHandle(DataBaseName.WJSJZX);
            //初始化SQL处理对象
            ihs = (IHandleSQL)Assembly.Load(strIntegrativeShow2).CreateInstance(strIntegrativeShow2 + '.' + Instance);
            //SQL以及参数初始化
            ihs.HandleSQL(dh, strKeys);
            //返回执行结果
            return dh.Delete();
        }

        public bool InsertInfo(string Instance, DataTable dt)
        {
            //初始化数据操作对象
            dh = new DataHandle(DataBaseName.WJSJZX);
            //初始化SQL处理对象
            ihs = (IHandleSQL)Assembly.Load(strIntegrativeShow2).CreateInstance(strIntegrativeShow2 + '.' + Instance);
            //SQL以及参数初始化(因为不需要参数，所以传空参数)
            ihs.HandleSQL(dh);
            //返回执行结果
            return dh.Update(dt);
        }

        public bool AlterInfo(string Instance, DataTable dt, string[] strKeys)
        {
            //初始化数据操作对象
            dh = new DataHandle(DataBaseName.WJSJZX);
            //初始化SQL处理对象
            ihs = (IHandleSQL)Assembly.Load(strIntegrativeShow2).CreateInstance(strIntegrativeShow2 + '.' + Instance);
            //SQL以及参数初始化
            ihs.HandleSQL(dh, strKeys);
            //返回执行结果
            return dh.Update(dt);
        }

        #endregion

        #region IHandleBusiness 成员




        #endregion
    }
}
