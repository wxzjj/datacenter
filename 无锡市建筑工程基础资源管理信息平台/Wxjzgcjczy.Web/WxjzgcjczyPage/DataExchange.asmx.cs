﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Xml;
using System.IO;
using System.Text;
using Wxjzgcjczy.Common;
using Wxjzgcjczy.BLL;
using Bigdesk8.Data;

using System.Transactions;
using Bigdesk8;


using Wxjzgcjczy.Web.WxjzgcjczyPage.Common;
using System.Xml.Serialization;



namespace Wxjzgcjczy.Web.WxjzgcjczyPage
{
    /// <summary>
    /// DataExchange 服务：无锡数据中心与各县市业务系统的数据交换接口
    /// </summary>
    [WebService(Namespace = "http://58.215.18.222:8889/WxjzgcjczyPage/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class DataExchange : System.Web.Services.WebService
    {
        string theUserName = ConfigManager.GetServiceUserName(), thePassword = ConfigManager.GetServicePassword();
        Wxjzgcjczy.Common.XmlHelper xmlHelper = new Wxjzgcjczy.Common.XmlHelper();


        #region 读取数据

        /// <summary>
        /// 功能： 从无锡数据中心读取数据
        /// 作者：孙刚
        /// 时间：2015-03-31
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="user">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="password"></param>
        /// <returns>返回查询到的XML格式数据的字符串表示</returns>
        [WebMethod]
        public string ReadTBDataFromZx(string tableName, string user, string password, string beginDate, string endDate)
        {
            string xmlData = String.Empty;
            DataExchangeBLL BLL = new DataExchangeBLL();

            string apiMessage = string.Empty;// 2016.10.21
            //string methodMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    return xmlData;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    return xmlData;
                }

                List<IDataItem> list = new List<IDataItem>();
                IDataItem item;

                if (string.IsNullOrEmpty(tableName))
                {
                    return xmlData;
                }
                DataTable dt;

                if (!string.IsNullOrEmpty(beginDate))
                {
                    DateTime date;
                    if (DateTime.TryParse(beginDate, out date))
                    {
                        item = new DataItem();
                        item.ItemName = "CREATEDATE";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.GreaterThanOrEqual;
                        item.ItemType = DataType.String;
                        item.ItemData = date.ToString("yyyy-MM-dd");
                        list.Add(item);
                    }
                }

                if (!string.IsNullOrEmpty(endDate))
                {
                    DateTime date;
                    if (DateTime.TryParse(endDate, out date))
                    {
                        item = new DataItem();
                        item.ItemName = "CREATEDATE";
                        item.ItemData = date.ToString("yyyy-MM-dd");
                        item.ItemType = DataType.String;
                        item.ItemRelation = Bigdesk8.Data.DataRelation.LessThanOrEqual;
                        list.Add(item);
                    }
                }

                switch (tableName.ToLower())
                {
                    case "tbprojectinfo"://TBProjectInfo

                        if (dt_user.Rows[0]["Has_TBProjectInfo"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "CountyNum";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = user;
                            list.Add(item);
                        }

                        dt = BLL.GetTBData_TBProjectInfo(list);

                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;

                    case "xm_gcdjb_dtxm":

                        if (dt_user.Rows[0]["Has_xm_gcdjb_dtxm"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "sbdqbm";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = user;
                            list.Add(item);
                        }


                        dt = BLL.GetTBData_xm_gcdjb_dtxm(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;

                    case "tbtenderinfo"://TBTenderInfo

                        if (dt_user.Rows[0]["Has_TBTenderInfo"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "sbdqbm";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = user;
                            list.Add(item);
                        }


                        dt = BLL.GetTBData_TBTenderInfo(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    case "tbcontractrecordmanage": //TBContractRecordManage

                        if (dt_user.Rows[0]["Has_TBContractRecordManage"].ToString2() == "0")
                        {
                            return xmlData;
                        }


                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "sbdqbm";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = user;
                            list.Add(item);
                        }
                        dt = BLL.GetTBData_TBContractRecordManage(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    case "tbprojectcensorinfo"://TBProjectCensorInfo

                        if (dt_user.Rows[0]["Has_TBProjectCensorInfo"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "sbdqbm";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = user;
                            list.Add(item);
                        }

                        dt = BLL.GetTBData_TBProjectCensorInfo(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    case "tbprojectdesigneconuserinfo"://TBProjectDesignEconUserInfo

                        if (dt_user.Rows[0]["Has_TBProjectDesignEconUserInfo"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "sbdqbm";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = user;
                            list.Add(item);
                        }

                        dt = BLL.GetTBData_TBProjectDesignEconUserInfo(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    case "tbbuilderlicencemanage"://TBBuilderLicenceManage

                        if (dt_user.Rows[0]["Has_TBBuilderLicenceManage"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "sbdqbm";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = user;
                            list.Add(item);
                        }

                        dt = BLL.GetTBData_TBBuilderLicenceManage(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    case "tbprojectBuilderuserinfo"://TBProjectBuilderUserInfo

                        if (dt_user.Rows[0]["Has_TBProjectBuilderUserInfo"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "sbdqbm";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = user;
                            list.Add(item);
                        }

                        dt = BLL.GetTBData_TBProjectBuilderUserInfo(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    case "tbprojectfinishmanage"://TBProjectFinishManage

                        if (dt_user.Rows[0]["Has_TBProjectFinishManage"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "sbdqbm";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = user;
                            list.Add(item);
                        }

                        dt = BLL.GetTBData_TBProjectFinishManage(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    case "aj_gcjbxx":

                        if (dt_user.Rows[0]["Has_aj_gcjbxx"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "sbdqbm";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = user;
                            list.Add(item);
                        }

                        dt = BLL.GetTBData_aj_gcjbxx(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    case "zj_gcjbxx":
                        if (dt_user.Rows[0]["Has_zj_gcjbxx"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "sbdqbm";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = user;
                            list.Add(item);
                        }

                        dt = BLL.GetTBData_zj_gcjbxx(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    case "zj_gcjbxx_zrdw":

                        if (dt_user.Rows[0]["Has_zj_gcjbxx"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        dt = BLL.GetTBData_zj_gcjbxx_zrdw(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    case "aj_zj_sgxk_relation":
                        if (dt_user.Rows[0]["Has_aj_zj_sgxk_relation"].ToString2() == "0")
                        {
                            return xmlData;
                        }
                        dt = BLL.GetTBData_aj_zj_sgxk_relation(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    default:

                        break;
                }


                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = "29";
                row_apicb["apiMethod"] = "ReadTBDataFromZx";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt("29", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                DataTable dt = BLL.GetAPIUnable();
                xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
            }

            return xmlData;

        }

        /// 功能： 从无锡数据中心读取数据
        /// 作者：孙刚
        /// 时间：2015-04-28
        /// <param name="tableName">表名称</param>
        /// <param name="user">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="prjNum">项目编号</param>
        /// <param name="buildCorpCode">建设单位组织机构代码</param>
        /// <param name="buildCorpName">建设单位名称</param>
        /// <returns>返回查询到的XML格式数据的字符串表示</returns>
        [WebMethod]
        public string ReadTBDataFromZx2(string tableName, string user, string password, string beginDate, string endDate, string prjNum, string buildCorpCode, string buildCorpName)
        {
            string xmlData = String.Empty;
            DataExchangeBLL BLL = new DataExchangeBLL();

            string apiMessage = string.Empty;// 2016.10.21
            //string methodMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    return xmlData;
                }
                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    return xmlData;
                }

                List<IDataItem> list = new List<IDataItem>();
                IDataItem item;


                if (string.IsNullOrEmpty(tableName))
                {
                    return xmlData;
                }
                DataTable dt;


                if (!string.IsNullOrEmpty(beginDate))
                {
                    DateTime date;
                    if (DateTime.TryParse(beginDate, out date))
                    {
                        item = new DataItem();
                        item.ItemName = "CREATEDATE";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.GreaterThanOrEqual;
                        item.ItemType = DataType.String;
                        item.ItemData = date.ToString("yyyy-MM-dd");
                        list.Add(item);
                    }
                }

                if (!string.IsNullOrEmpty(endDate))
                {
                    DateTime date;
                    if (DateTime.TryParse(endDate, out date))
                    {
                        item = new DataItem();
                        item.ItemName = "CREATEDATE";
                        item.ItemData = date.ToString("yyyy-MM-dd");
                        item.ItemType = DataType.String;
                        item.ItemRelation = Bigdesk8.Data.DataRelation.LessThanOrEqual;
                        list.Add(item);
                    }
                }

                if (!string.IsNullOrEmpty(prjNum))
                {
                    item = new DataItem();
                    item.ItemName = "PrjNum";
                    item.ItemData = prjNum;
                    item.ItemType = DataType.String;
                    item.ItemRelation = Bigdesk8.Data.DataRelation.Like;
                    list.Add(item);
                }

                switch (tableName.ToLower())
                {
                    case "tbprojectinfo"://TBProjectInfo

                        if (dt_user.Rows[0]["Has_TBProjectInfo"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "CountyNum";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = user;
                            list.Add(item);


                        }


                        if (!string.IsNullOrEmpty(buildCorpCode))
                        {
                            item = new DataItem();
                            item.ItemName = "BuildCorpCode";
                            item.ItemData = buildCorpCode;
                            item.ItemType = DataType.String;
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Like;
                            list.Add(item);
                        }

                        if (!string.IsNullOrEmpty(buildCorpName))
                        {
                            item = new DataItem();
                            item.ItemName = "BuildCorpName";
                            item.ItemData = buildCorpName;
                            item.ItemType = DataType.String;
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Like;
                            list.Add(item);
                        }

                        dt = BLL.GetTBData_TBProjectInfo(list);

                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;

                    case "xm_gcdjb_dtxm":
                        if (dt_user.Rows[0]["Has_xm_gcdjb_dtxm"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "sbdqbm";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = user;
                            list.Add(item);


                        }

                        dt = BLL.GetTBData_xm_gcdjb_dtxm(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;

                    case "tbtenderinfo"://TBTenderInfo

                        if (dt_user.Rows[0]["Has_TBTenderInfo"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "sbdqbm";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = user;
                            list.Add(item);


                        }

                        dt = BLL.GetTBData_TBTenderInfo(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    case "tbcontractrecordmanage": //TBContractRecordManage

                        if (dt_user.Rows[0]["Has_TBContractRecordManage"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "sbdqbm";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = user;
                            list.Add(item);


                        }

                        dt = BLL.GetTBData_TBContractRecordManage(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    case "tbprojectcensorinfo"://TBProjectCensorInfo

                        if (dt_user.Rows[0]["Has_TBProjectCensorInfo"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "sbdqbm";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = user;
                            list.Add(item);


                        }

                        dt = BLL.GetTBData_TBProjectCensorInfo(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    case "tbprojectdesigneconuserinfo"://TBProjectDesignEconUserInfo

                        if (dt_user.Rows[0]["Has_TBProjectDesignEconUserInfo"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "sbdqbm";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = user;
                            list.Add(item);


                        }

                        dt = BLL.GetTBData_TBProjectDesignEconUserInfo(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    case "tbbuilderlicencemanage"://TBBuilderLicenceManage

                        if (dt_user.Rows[0]["Has_TBBuilderLicenceManage"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "sbdqbm";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = user;
                            list.Add(item);


                        }

                        dt = BLL.GetTBData_TBBuilderLicenceManage(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    case "tbprojectBuilderuserinfo"://TBProjectBuilderUserInfo

                        if (dt_user.Rows[0]["Has_TBProjectBuilderUserInfo"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "sbdqbm";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = user;
                            list.Add(item);


                        }

                        dt = BLL.GetTBData_TBProjectBuilderUserInfo(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    case "tbprojectfinishmanage"://TBProjectFinishManage

                        if (dt_user.Rows[0]["Has_TBProjectFinishManage"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "sbdqbm";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = user;
                            list.Add(item);


                        }

                        dt = BLL.GetTBData_TBProjectFinishManage(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    case "aj_gcjbxx":

                        if (dt_user.Rows[0]["Has_aj_gcjbxx"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "sbdqbm";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = user;
                            list.Add(item);


                        }

                        dt = BLL.GetTBData_aj_gcjbxx(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    case "zj_gcjbxx":
                        if (dt_user.Rows[0]["Has_zj_gcjbxx"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "sbdqbm";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = user;
                            list.Add(item);


                        }

                        dt = BLL.GetTBData_zj_gcjbxx(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    case "zj_gcjbxx_zrdw":

                        if (dt_user.Rows[0]["Has_zj_gcjbxx"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "sbdqbm";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = user;
                            list.Add(item);
                        }

                        dt = BLL.GetTBData_zj_gcjbxx_zrdw(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    case "aj_zj_sgxk_relation":
                        if (dt_user.Rows[0]["Has_aj_zj_sgxk_relation"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        dt = BLL.GetTBData_zj_gcjbxx_zrdw(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    default:

                        break;
                }

                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = "29";
                row_apicb["apiMethod"] = "ReadTBDataFromZx2";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt("29", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                DataTable dt = BLL.GetAPIUnable();
                xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
            }



            return xmlData;

        }

        public string ReadTBDataFromZx3(string tableName, string user, string password, string pkId)
        {
            string xmlData = String.Empty;
            DataExchangeBLL BLL = new DataExchangeBLL();
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
            {
                return xmlData;
            }
            DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
            if (dt_user.Rows.Count == 0)
            {
                return xmlData;
            }


            List<IDataItem> list = new List<IDataItem>();
            IDataItem item;


            if (string.IsNullOrEmpty(tableName))
            {
                return xmlData;
            }
            DataTable dt;

            if (!string.IsNullOrEmpty(pkId))
            {
                item = new DataItem();
                item.ItemName = "PKID";
                item.ItemData = pkId;
                item.ItemType = DataType.String;
                item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                list.Add(item);
            }

            switch (tableName.ToLower())
            {
                case "tbprojectinfo"://TBProjectInfo

                    if (dt_user.Rows[0]["Has_TBProjectInfo"].ToString2() == "0")
                    {
                        return xmlData;
                    }


                    if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                    {
                        item = new DataItem();
                        item.ItemName = "CountyNum";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                        item.ItemType = DataType.String;
                        item.ItemData = user;
                        list.Add(item);


                    }

                    dt = BLL.GetTBData_TBProjectInfo(list);

                    xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                    break;

                case "xm_gcdjb_dtxm":

                    if (dt_user.Rows[0]["Has_xm_gcdjb_dtxm"].ToString2() == "0")
                    {
                        return xmlData;
                    }

                    if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                    {
                        item = new DataItem();
                        item.ItemName = "sbdqbm";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                        item.ItemType = DataType.String;
                        item.ItemData = user;
                        list.Add(item);


                    }

                    dt = BLL.GetTBData_xm_gcdjb_dtxm(list);
                    xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                    break;

                case "tbtenderinfo"://TBTenderInfo

                    if (dt_user.Rows[0]["Has_TBTenderInfo"].ToString2() == "0")
                    {
                        return xmlData;
                    }

                    if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                    {
                        item = new DataItem();
                        item.ItemName = "sbdqbm";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                        item.ItemType = DataType.String;
                        item.ItemData = user;
                        list.Add(item);


                    }

                    dt = BLL.GetTBData_TBTenderInfo(list);
                    xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                    break;
                case "tbcontractrecordmanage": //TBContractRecordManage

                    if (dt_user.Rows[0]["Has_TBContractRecordManage"].ToString2() == "0")
                    {
                        return xmlData;
                    }

                    if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                    {
                        item = new DataItem();
                        item.ItemName = "sbdqbm";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                        item.ItemType = DataType.String;
                        item.ItemData = user;
                        list.Add(item);


                    }

                    dt = BLL.GetTBData_TBContractRecordManage(list);
                    xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                    break;
                case "tbprojectcensorinfo"://TBProjectCensorInfo

                    if (dt_user.Rows[0]["Has_TBProjectCensorInfo"].ToString2() == "0")
                    {
                        return xmlData;
                    }

                    if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                    {
                        item = new DataItem();
                        item.ItemName = "sbdqbm";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                        item.ItemType = DataType.String;
                        item.ItemData = user;
                        list.Add(item);


                    }

                    dt = BLL.GetTBData_TBProjectCensorInfo(list);
                    xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                    break;
                case "tbprojectdesigneconuserinfo"://TBProjectDesignEconUserInfo

                    if (dt_user.Rows[0]["Has_TBProjectDesignEconUserInfo"].ToString2() == "0")
                    {
                        return xmlData;
                    }

                    if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                    {
                        item = new DataItem();
                        item.ItemName = "sbdqbm";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                        item.ItemType = DataType.String;
                        item.ItemData = user;
                        list.Add(item);


                    }

                    dt = BLL.GetTBData_TBProjectDesignEconUserInfo(list);
                    xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                    break;
                case "tbbuilderlicencemanage"://TBBuilderLicenceManage

                    if (dt_user.Rows[0]["Has_TBBuilderLicenceManage"].ToString2() == "0")
                    {
                        return xmlData;
                    }

                    if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                    {
                        item = new DataItem();
                        item.ItemName = "sbdqbm";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                        item.ItemType = DataType.String;
                        item.ItemData = user;
                        list.Add(item);


                    }

                    dt = BLL.GetTBData_TBBuilderLicenceManage(list);
                    xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                    break;
                case "tbprojectBuilderuserinfo"://TBProjectBuilderUserInfo

                    if (dt_user.Rows[0]["Has_TBProjectBuilderUserInfo"].ToString2() == "0")
                    {
                        return xmlData;
                    }


                    if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                    {
                        item = new DataItem();
                        item.ItemName = "sbdqbm";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                        item.ItemType = DataType.String;
                        item.ItemData = user;
                        list.Add(item);


                    }

                    dt = BLL.GetTBData_TBProjectBuilderUserInfo(list);
                    xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                    break;
                case "tbprojectfinishmanage"://TBProjectFinishManage

                    if (dt_user.Rows[0]["Has_TBProjectFinishManage"].ToString2() == "0")
                    {
                        return xmlData;
                    }

                    if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                    {
                        item = new DataItem();
                        item.ItemName = "sbdqbm";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                        item.ItemType = DataType.String;
                        item.ItemData = user;
                        list.Add(item);


                    }

                    dt = BLL.GetTBData_TBProjectFinishManage(list);
                    xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                    break;
                case "aj_gcjbxx":


                    if (dt_user.Rows[0]["Has_aj_gcjbxx"].ToString2() == "0")
                    {
                        return xmlData;
                    }

                    if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                    {
                        item = new DataItem();
                        item.ItemName = "sbdqbm";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                        item.ItemType = DataType.String;
                        item.ItemData = user;
                        list.Add(item);


                    }

                    dt = BLL.GetTBData_aj_gcjbxx(list);
                    xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                    break;
                case "zj_gcjbxx":
                    if (dt_user.Rows[0]["Has_zj_gcjbxx"].ToString2() == "0")
                    {
                        return xmlData;
                    }

                    if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                    {
                        item = new DataItem();
                        item.ItemName = "sbdqbm";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                        item.ItemType = DataType.String;
                        item.ItemData = user;
                        list.Add(item);


                    }

                    dt = BLL.GetTBData_zj_gcjbxx(list);
                    xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                    break;
                case "zj_gcjbxx_zrdw":
                    if (dt_user.Rows[0]["Has_zj_gcjbxx"].ToString2() == "0")
                    {
                        return xmlData;
                    }

                    dt = BLL.GetTBData_zj_gcjbxx_zrdw(list);
                    xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                    break;
                case "aj_zj_sgxk_relation":

                    if (dt_user.Rows[0]["Has_aj_zj_sgxk_relation"].ToString2() == "0")
                    {
                        return xmlData;
                    }
                    dt = BLL.GetTBData_zj_gcjbxx_zrdw(list);
                    xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                    break;
                default:

                    break;
            }

            return xmlData;

        }

        public string ReadTBDataFromZx_ByInnerNum(string tableName, string user, string password, string innerNum)
        {
            string xmlData = String.Empty;
            DataExchangeBLL BLL = new DataExchangeBLL();
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
            {
                return xmlData;
            }
            DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
            if (dt_user.Rows.Count == 0)
            {
                return xmlData;
            }


            List<IDataItem> list = new List<IDataItem>();
            IDataItem item;



            if (string.IsNullOrEmpty(tableName))
            {
                return xmlData;
            }
            DataTable dt;

            switch (tableName.ToLower())
            {
                case "tbprojectinfo"://TBProjectInfo

                    if (dt_user.Rows[0]["Has_TBProjectInfo"].ToString2() == "0")
                    {
                        return xmlData;
                    }

                    if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                    {
                        item = new DataItem();
                        item.ItemName = "CountyNum";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                        item.ItemType = DataType.String;
                        item.ItemData = user;
                        list.Add(item);

                    }


                    if (!string.IsNullOrEmpty(innerNum))
                    {
                        item = new DataItem();
                        item.ItemName = "PrjInnerNum";
                        item.ItemData = innerNum;
                        item.ItemType = DataType.String;
                        item.ItemRelation = Bigdesk8.Data.DataRelation.Like;
                        list.Add(item);
                    }
                    dt = BLL.GetTBData_TBProjectInfo(list);

                    xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                    break;

                case "xm_gcdjb_dtxm":

                    if (dt_user.Rows[0]["Has_xm_gcdjb_dtxm"].ToString2() == "0")
                    {
                        return xmlData;
                    }

                    if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                    {
                        item = new DataItem();
                        item.ItemName = "sbdqbm";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                        item.ItemType = DataType.String;
                        item.ItemData = user;
                        list.Add(item);


                    }

                    dt = BLL.GetTBData_xm_gcdjb_dtxm(list);
                    xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                    break;

                case "tbtenderinfo"://TBTenderInfo

                    if (dt_user.Rows[0]["Has_TBTenderInfo"].ToString2() == "0")
                    {
                        return xmlData;
                    }

                    if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                    {
                        item = new DataItem();
                        item.ItemName = "sbdqbm";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                        item.ItemType = DataType.String;
                        item.ItemData = user;
                        list.Add(item);


                    }


                    if (!string.IsNullOrEmpty(innerNum))
                    {
                        item = new DataItem();
                        item.ItemName = "TenderInnerNum";
                        item.ItemData = innerNum;
                        item.ItemType = DataType.String;
                        item.ItemRelation = Bigdesk8.Data.DataRelation.Like;
                        list.Add(item);
                    }
                    dt = BLL.GetTBData_TBTenderInfo(list);
                    xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                    break;
                case "tbcontractrecordmanage": //TBContractRecordManage

                    if (dt_user.Rows[0]["Has_TBContractRecordManage"].ToString2() == "0")
                    {
                        return xmlData;
                    }

                    if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                    {
                        item = new DataItem();
                        item.ItemName = "sbdqbm";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                        item.ItemType = DataType.String;
                        item.ItemData = user;
                        list.Add(item);


                    }


                    if (!string.IsNullOrEmpty(innerNum))
                    {
                        item = new DataItem();
                        item.ItemName = "RecordInnerNum";
                        item.ItemData = innerNum;
                        item.ItemType = DataType.String;
                        item.ItemRelation = Bigdesk8.Data.DataRelation.Like;
                        list.Add(item);
                    }
                    dt = BLL.GetTBData_TBContractRecordManage(list);
                    xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                    break;
                case "tbprojectcensorinfo"://TBProjectCensorInfo

                    if (dt_user.Rows[0]["Has_TBProjectCensorInfo"].ToString2() == "0")
                    {
                        return xmlData;
                    }


                    if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                    {
                        item = new DataItem();
                        item.ItemName = "sbdqbm";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                        item.ItemType = DataType.String;
                        item.ItemData = user;
                        list.Add(item);


                    }

                    if (!string.IsNullOrEmpty(innerNum))
                    {
                        item = new DataItem();
                        item.ItemName = "CensorInnerNum";
                        item.ItemData = innerNum;
                        item.ItemType = DataType.String;
                        item.ItemRelation = Bigdesk8.Data.DataRelation.Like;
                        list.Add(item);
                    }


                    dt = BLL.GetTBData_TBProjectCensorInfo(list);
                    xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                    break;
                case "tbprojectdesigneconuserinfo"://TBProjectDesignEconUserInfo

                    if (dt_user.Rows[0]["Has_TBProjectDesignEconUserInfo"].ToString2() == "0")
                    {
                        return xmlData;
                    }

                    if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                    {
                        item = new DataItem();
                        item.ItemName = "sbdqbm";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                        item.ItemType = DataType.String;
                        item.ItemData = user;
                        list.Add(item);

                    }


                    dt = BLL.GetTBData_TBProjectDesignEconUserInfo(list);
                    xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                    break;
                case "tbbuilderlicencemanage"://TBBuilderLicenceManage

                    if (dt_user.Rows[0]["Has_TBBuilderLicenceManage"].ToString2() == "0")
                    {
                        return xmlData;
                    }

                    if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                    {
                        item = new DataItem();
                        item.ItemName = "sbdqbm";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                        item.ItemType = DataType.String;
                        item.ItemData = user;
                        list.Add(item);


                    }

                    if (!string.IsNullOrEmpty(innerNum))
                    {
                        item = new DataItem();
                        item.ItemName = "BuilderLicenceInnerNum";
                        item.ItemData = innerNum;
                        item.ItemType = DataType.String;
                        item.ItemRelation = Bigdesk8.Data.DataRelation.Like;
                        list.Add(item);
                    }

                    dt = BLL.GetTBData_TBBuilderLicenceManage(list);
                    xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                    break;
                case "tbprojectBuilderuserinfo"://TBProjectBuilderUserInfo

                    if (dt_user.Rows[0]["Has_TBProjectBuilderUserInfo"].ToString2() == "0")
                    {
                        return xmlData;
                    }

                    if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                    {
                        item = new DataItem();
                        item.ItemName = "sbdqbm";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                        item.ItemType = DataType.String;
                        item.ItemData = user;
                        list.Add(item);


                    }

                    dt = BLL.GetTBData_TBProjectBuilderUserInfo(list);
                    xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                    break;
                case "tbprojectfinishmanage"://TBProjectFinishManage

                    if (dt_user.Rows[0]["Has_TBProjectFinishManage"].ToString2() == "0")
                    {
                        return xmlData;
                    }

                    if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                    {
                        item = new DataItem();
                        item.ItemName = "sbdqbm";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                        item.ItemType = DataType.String;
                        item.ItemData = user;
                        list.Add(item);


                    }

                    if (!string.IsNullOrEmpty(innerNum))
                    {
                        item = new DataItem();
                        item.ItemName = "PrjFinishInnerNum";
                        item.ItemData = innerNum;
                        item.ItemType = DataType.String;
                        item.ItemRelation = Bigdesk8.Data.DataRelation.Like;
                        list.Add(item);
                    }
                    dt = BLL.GetTBData_TBProjectFinishManage(list);
                    xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                    break;
                case "aj_gcjbxx":

                    if (dt_user.Rows[0]["Has_aj_gcjbxx"].ToString2() == "0")
                    {
                        return xmlData;
                    }

                    if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                    {
                        item = new DataItem();
                        item.ItemName = "sbdqbm";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                        item.ItemType = DataType.String;
                        item.ItemData = user;
                        list.Add(item);


                    }

                    dt = BLL.GetTBData_aj_gcjbxx(list);
                    xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                    break;
                case "zj_gcjbxx":

                    if (dt_user.Rows[0]["Has_zj_gcjbxx"].ToString2() == "0")
                    {
                        return xmlData;
                    }

                    if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                    {
                        item = new DataItem();
                        item.ItemName = "sbdqbm";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                        item.ItemType = DataType.String;
                        item.ItemData = user;
                        list.Add(item);


                    }

                    dt = BLL.GetTBData_zj_gcjbxx(list);
                    xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                    break;
                case "zj_gcjbxx_zrdw":
                    if (dt_user.Rows[0]["Has_zj_gcjbxx"].ToString2() == "0")
                    {
                        return xmlData;
                    }

                    dt = BLL.GetTBData_zj_gcjbxx_zrdw(list);
                    xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                    break;
                case "aj_zj_sgxk_relation":

                    if (dt_user.Rows[0]["Has_aj_zj_sgxk_relation"].ToString2() == "0")
                    {
                        return xmlData;
                    }
                    dt = BLL.GetTBData_zj_gcjbxx_zrdw(list);
                    xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                    break;
                default:

                    break;
            }

            return xmlData;

        }

        /// <summary>
        ///获取无锡数据中心的建设单位信息
        /// </summary>
        /// <param name="csywlx"></param>
        /// <returns></returns>
        [WebMethod]
        public string Read_Jsdwxx(string user, string password, string dwfl)
        {
            DataExchangeBLL BLL = new DataExchangeBLL();
            SparkSoftDataBody sparkSoftDataBody = new SparkSoftDataBody();
            StringBuilder str = new StringBuilder();
            str.Append("<SparkSoftDataBody><ReturnInfo>");

            string apiMessage = string.Empty;// 2016.10.21
            //string methodMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {


                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>用户名或密码不正确！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }
                if (dt_user.Rows[0]["Has_Jsdw"].ToString2() == "0")
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>该用户没有权限获取建设单位信息！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }

                if (string.IsNullOrEmpty(dwfl))
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>企业分类参数错误！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }

                List<string> list_dwfl = new List<string>() { "房地产开发企业", "其它" };

                String[] arr = dwfl.Trim().Split(',');
                foreach (string item in arr)
                {
                    if (!list_dwfl.Exists(p => p == item))
                    {
                        str.Append("<Status>0</Status>");
                        str.AppendFormat("<Description>企业分类参数错误：{0}</Description>", item);
                        str.Append("</ReturnInfo></SparkSoftDataBody>");
                        return str.ToString();
                    }
                }

                str.Append("<Status>1</Status>");
                str.Append("<Description></Description></ReturnInfo>");

                DataTable dt_jsdw = BLL.Get_uepp_jsdwByDwfl(dwfl);
                if (dt_jsdw.Rows.Count > 0)
                {
                    str.Append("<JsdwArray>");
                    foreach (DataRow row in dt_jsdw.Rows)
                    {
                        str.Append("<Jsdw>");
                        str.AppendFormat("<jsdwID>{0}<jsdwID>", row["jsdwID"].ToString2());
                        str.AppendFormat("<jsdw>{0}<jsdw>", row["jsdw"].ToString2());
                        str.AppendFormat("<zzjgdm>{0}<zzjgdm>", row["zzjgdm"].ToString2());
                        str.AppendFormat("<dwflID>{0}<dwflID>", row["dwflID"].ToString2());
                        str.AppendFormat("<dwfl>{0}<dwfl>", row["dwfl"].ToString2());
                        str.AppendFormat("<dwdz>{0}<dwdz>", row["dwdz"].ToString2());
                        str.AppendFormat("<yb>{0}<yb>", row["yb"].ToString2());
                        str.AppendFormat("<fax>{0}<fax>", row["fax"].ToString2());
                        str.AppendFormat("<fddbr>{0}<fddbr>", row["fddbr"].ToString2());

                        str.AppendFormat("<fddbrdh>{0}<fddbrdh>", row["fddbrdh"].ToString2());
                        str.AppendFormat("<lxr>{0}<lxr>", row["lxr"].ToString2());
                        str.AppendFormat("<lxdh>{0}<lxdh>", row["lxdh"].ToString2());
                        str.AppendFormat("<xgrqsj>{0}<xgrqsj>", row["xgrqsj"].ToString2());
                        str.AppendFormat("<yyzz>{0}<yyzz>", row["yyzz"].ToString2());

                        str.Append("</Jsdw>");
                    }
                    str.Append("</JsdwArray>");
                }
                else
                {
                    str.Append("<JsdwArray></JsdwArray>");
                }


                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = "29";
                row_apicb["apiMethod"] = "Read_Jsdwxx";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);
                BLL.UpdateZbJkzt("29", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                str.Append("<jkgb>接口关闭</jkgb>");
                str.Append("</ReturnInfo>");
                return str.ToString();
            }

            str.Append("</SparkSoftDataBody>");
            return str.ToString();
        }
        
        /// <summary>
        /// 根据企业从事业务类型获取企业信息
        /// </summary>
        /// <param name="csywlx"></param>
        /// <returns></returns>
        [WebMethod]
        public string Read_Sgdwxx(string user, string password, string csywlx, string clrqS, string clrqE)
        {
            SparkSoftDataBody sparkSoftDataBody = new SparkSoftDataBody();
            StringBuilder str = new StringBuilder();
            DataExchangeBLL BLL = new DataExchangeBLL();

            str.Append("<SparkSoftDataBody><ReturnInfo>");

            string apiMessage = string.Empty;// 2016.10.21
            //string methodMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {


                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>用户名或密码不正确！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }
                if (dt_user.Rows[0]["Has_Sgdw"].ToString2() == "0")
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>该用户没有权限获取施工单位信息！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }


                if (string.IsNullOrEmpty(csywlx))
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>企业分类参数错误！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }


                if (string.IsNullOrEmpty(clrqS) || string.IsNullOrEmpty(clrqE))
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>成立年度参数错误！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }

                if (!clrqS.IsDate() || !clrqE.IsDate())
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>成立年度参数错误！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }


                List<string> list_qycsywlx = BLL.Get_uepp_Qycsywlx();

                String[] arr = csywlx.Trim().Split(',');
                foreach (string item in arr)
                {
                    if (!list_qycsywlx.Exists(p => p == item))
                    {
                        str.Append("<Status>0</Status>");
                        str.AppendFormat("<Description>企业分类参数错误：{0}</Description>", item);
                        str.Append("</ReturnInfo></SparkSoftDataBody>");
                        return str.ToString();
                    }
                }

                str.Append("<Status>1</Status>");
                str.Append("<Description></Description></ReturnInfo>");

                DataTable dt_Sgqyxx = BLL.Get_uepp_Sgdw(csywlx, clrqS, clrqE);
                if (dt_Sgqyxx.Rows.Count > 0)
                {
                    str.Append("<QyxxArray>");
                    foreach (DataRow row in dt_Sgqyxx.Rows)
                    {
                        str.Append("<Qyxx>");
                        #region 企业基本信息
                        str.Append("<Qyjbxx>");

                        foreach (DataColumn col in dt_Sgqyxx.Columns)
                        {
                            str.AppendFormat("<{0}>{1}</{2}>", col.ColumnName, row[col.ColumnName].ToString2(), col.ColumnName);
                        }
                        str.Append("</Qyjbxx>");
                        #endregion

                        #region 企业资质信息
                        DataTable dt_qyzzxx = BLL.Get_uepp_SgQyzzByQyID(row["qyID"].ToString2());
                        str.Append("<QyzzArray>");
                        foreach (DataRow row_qyzzmx in dt_qyzzxx.Rows)
                        {
                            str.Append("<Qyzz>");
                            foreach (DataColumn col in dt_qyzzxx.Columns)
                            {
                                str.AppendFormat("<{0}>{1}</{2}>", col.ColumnName, row_qyzzmx[col.ColumnName].ToString2(), col.ColumnName);
                            }
                            str.Append("</Qyzz>");

                        }
                        str.Append("</QyzzArray>");
                        #endregion

                        #region 企业证书信息
                        DataTable dt_qyzs = BLL.Get_uepp_SgQyzsByQyID(row["qyID"].ToString2());
                        str.Append("<QyzsArray>");
                        foreach (DataRow row_qyzsmx in dt_qyzs.Rows)
                        {
                            str.Append("<Qyzs>");
                            foreach (DataColumn col in dt_qyzs.Columns)
                            {
                                str.AppendFormat("<{0}>{1}</{2}>", col.ColumnName, row_qyzsmx[col.ColumnName].ToString2(), col.ColumnName);
                            }
                            str.Append("</Qyzs>");

                        }
                        str.Append("</QyzsArray>");
                        #endregion
                        str.Append("</Qyxx>");
                    }
                    str.Append("</QyxxArray>");
                }
                else
                {
                    str.Append("<QyxxArray></QyxxArray>");
                }

                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = "29";
                row_apicb["apiMethod"] = "Read_Sgdwxx";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt("29", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                str.Append("<jkgb>接口关闭</jkgb>");
                str.Append("</ReturnInfo>");
                return str.ToString();
            }

            str.Append("</SparkSoftDataBody>");
            return str.ToString();
        }
        /// <summary>
        /// 根据企业从事业务类型获取勘察单位信息
        /// </summary>
        /// <param name="csywlx"></param>
        /// <returns></returns>
        [WebMethod]
        public string Read_Kcdwxx(string user, string password)
        {
            SparkSoftDataBody sparkSoftDataBody = new SparkSoftDataBody();
            StringBuilder str = new StringBuilder();
            str.Append("<SparkSoftDataBody><ReturnInfo>");
            DataExchangeBLL BLL = new DataExchangeBLL();

            string apiMessage = string.Empty;// 2016.10.21
            //string methodMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {


                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>用户名或密码不正确！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }
                if (dt_user.Rows[0]["Has_Kcdw"].ToString2() == "0")
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>该用户没有权限获取勘察单位信息！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }

                str.Append("<Status>1</Status>");
                str.Append("<Description></Description></ReturnInfo>");



                DataTable dt_Kcqyxx = BLL.Get_uepp_Kcdw();
                if (dt_Kcqyxx.Rows.Count > 0)
                {
                    str.Append("<QyxxArray>");
                    foreach (DataRow row in dt_Kcqyxx.Rows)
                    {
                        str.Append("<Qyxx>");
                        #region 企业基本信息
                        str.Append("<Qyjbxx>");

                        foreach (DataColumn col in dt_Kcqyxx.Columns)
                        {
                            str.AppendFormat("<{0}>{1}<{2}>", col.ColumnName, row[col.ColumnName].ToString2(), col.ColumnName);
                        }
                        str.Append("</Qyjbxx>");

                        #endregion

                        #region 企业资质信息

                        DataTable dt_qyzz = BLL.Get_uepp_KcQyzzByQyID(row["qyID"].ToString2());
                        str.Append("<QyzzArray>");
                        foreach (DataRow row_qyzzmx in dt_qyzz.Rows)
                        {
                            str.Append("<Qyzz>");
                            foreach (DataColumn col in dt_qyzz.Columns)
                            {
                                str.AppendFormat("<{0}>{1}<{2}>", col.ColumnName, row_qyzzmx[col.ColumnName].ToString2(), col.ColumnName);
                            }
                            str.Append("</Qyzz>");

                        }
                        str.Append("</QyzzArray>");
                        #endregion

                        #region 企业证书信息
                        DataTable dt_qyzs = BLL.Get_uepp_KcQyzsByQyID(row["qyID"].ToString2());
                        str.Append("<QyzsArray>");
                        foreach (DataRow row_qyzsmx in dt_qyzs.Rows)
                        {
                            str.Append("<Qyzs>");
                            foreach (DataColumn col in dt_qyzs.Columns)
                            {
                                str.AppendFormat("<{0}>{1}<{2}>", col.ColumnName, row_qyzsmx[col.ColumnName].ToString2(), col.ColumnName);
                            }
                            str.Append("</Qyzs>");

                        }
                        str.Append("</QyzsArray>");
                        #endregion

                        str.Append("</Qyxx>");
                    }
                    str.Append("</QyxxArray>");

                }
                else
                {
                    str.Append("<QyxxArray></QyxxArray>");
                }

                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = "29";
                row_apicb["apiMethod"] = "Read_Kcdwxx";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt("29", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                str.Append("<jkgb>接口关闭</jkgb>");
                str.Append("</ReturnInfo>");
                return str.ToString();
            }
            str.Append("</SparkSoftDataBody>");
            return str.ToString();
        }
        /// <summary>
        /// 根据企业从事业务类型获取设计单位信息
        /// </summary>
        /// <param name="csywlx"></param>
        /// <returns></returns>
        [WebMethod]
        public string Read_Sjdwxx(string user, string password, string csywlx)
        {
            SparkSoftDataBody sparkSoftDataBody = new SparkSoftDataBody();
            StringBuilder str = new StringBuilder();

            str.Append("<SparkSoftDataBody><ReturnInfo>");

            DataExchangeBLL BLL = new DataExchangeBLL();
            string apiMessage = string.Empty;// 2016.10.21
            //string methodMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {



                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>用户名或密码不正确！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }
                if (dt_user.Rows[0]["Has_Sjdw"].ToString2() == "0")
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>该用户没有权限获取设计单位信息！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }

                if (string.IsNullOrEmpty(csywlx.ToString2().Trim()))
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>企业分类参数错误！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }
                List<string> list_qycsywlx = BLL.Get_uepp_Qycsywlx();

                String[] arr = csywlx.Trim().Split(',');
                foreach (string item in arr)
                {
                    if (!list_qycsywlx.Exists(p => p == item))
                    {
                        str.Append("<Status>0</Status>");
                        str.AppendFormat("<Description>企业分类参数错误：{0}</Description>", item);
                        str.Append("</ReturnInfo></SparkSoftDataBody>");
                        return str.ToString();
                    }
                }

                str.Append("<Status>1</Status>");
                str.Append("<Description></Description></ReturnInfo>");

                DataTable dt_Sjqyxx = BLL.Get_uepp_Sjdw(csywlx);
                if (dt_Sjqyxx.Rows.Count > 0)
                {
                    str.Append("<QyxxArray>");
                    foreach (DataRow row in dt_Sjqyxx.Rows)
                    {
                        str.Append("<Qyxx>");
                        #region 企业基本信息
                        str.Append("<Qyjbxx>");

                        foreach (DataColumn col in dt_Sjqyxx.Columns)
                        {
                            str.AppendFormat("<{0}>{1}<{2}>", col.ColumnName, row[col.ColumnName].ToString2(), col.ColumnName);
                        }
                        str.Append("</Qyjbxx>");
                        #endregion

                        #region 企业资质
                        DataTable dt_qyzz = BLL.Get_uepp_SjQyzzByQyID(row["qyID"].ToString2());
                        str.Append("<QyzzArray>");
                        foreach (DataRow row_qyzzmx in dt_qyzz.Rows)
                        {
                            str.Append("<Qyzz>");
                            foreach (DataColumn col in dt_qyzz.Columns)
                            {
                                str.AppendFormat("<{0}>{1}<{2}>", col.ColumnName, row_qyzzmx[col.ColumnName].ToString2(), col.ColumnName);
                            }
                            str.Append("</Qyzz>");

                        }
                        str.Append("</QyzzArray>");
                        #endregion

                        #region 企业证书
                        DataTable dt_qyzs = BLL.Get_uepp_SjQyzsByQyID(row["qyID"].ToString2());
                        str.Append("<QyzsArray>");
                        foreach (DataRow row_qyzsmx in dt_qyzs.Rows)
                        {
                            str.Append("<Qyzs>");
                            foreach (DataColumn col in dt_qyzs.Columns)
                            {
                                str.AppendFormat("<{0}>{1}<{2}>", col.ColumnName, row_qyzsmx[col.ColumnName].ToString2(), col.ColumnName);
                            }
                            str.Append("</Qyzs>");

                        }
                        str.Append("</QyzsArray>");
                        #endregion
                        str.Append("</Qyxx>");
                    }
                    str.Append("</QyxxArray>");

                }
                else
                {
                    str.Append("<QyxxArray></QyxxArray>");
                }

                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = "29";
                row_apicb["apiMethod"] = "Read_Sjdwxx";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt("29", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                str.Append("<jkgb>接口关闭</jkgb>");
                str.Append("</ReturnInfo>");
                return str.ToString();
            }

            str.Append("</SparkSoftDataBody>");
            return str.ToString();
        }
        /// <summary>
        /// 根据企业从事业务类型获取中介机构信息
        /// </summary>
        /// <param name="csywlx"></param>
        /// <returns></returns>
        [WebMethod]
        public string Read_Zjjgxx(string user, string password, string csywlx)
        {
            SparkSoftDataBody sparkSoftDataBody = new SparkSoftDataBody();
            StringBuilder str = new StringBuilder();

            str.Append("<SparkSoftDataBody><ReturnInfo>");

            DataExchangeBLL BLL = new DataExchangeBLL();
            string apiMessage = string.Empty;// 2016.10.21
            //string methodMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {



                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>用户名或密码不正确！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }
                if (dt_user.Rows[0]["Has_Zjjg"].ToString2() == "0")
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>该用户没有权限获取中介机构信息！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }


                if (string.IsNullOrEmpty(csywlx.ToString2().Trim()))
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>企业分类参数错误！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }
                List<string> list_qycsywlx = BLL.Get_uepp_Qycsywlx();

                String[] arr = csywlx.Trim().Split(',');
                foreach (string item in arr)
                {
                    if (!list_qycsywlx.Exists(p => p == item))
                    {
                        str.Append("<Status>0</Status>");
                        str.AppendFormat("<Description>企业分类参数错误：{0}</Description>", item);
                        str.Append("</ReturnInfo></SparkSoftDataBody>");
                        return str.ToString();
                    }
                }

                str.Append("<Status>1</Status>");
                str.Append("<Description></Description></ReturnInfo>");

                DataTable dt_Kcqyxx = BLL.Get_uepp_Zjjg(csywlx);
                if (dt_Kcqyxx.Rows.Count > 0)
                {
                    str.Append("<QyxxArray>");
                    foreach (DataRow row in dt_Kcqyxx.Rows)
                    {
                        str.Append("<Qyxx>");
                        #region 企业基本信息
                        str.Append("<Qyjbxx>");
                        foreach (DataColumn col in dt_Kcqyxx.Columns)
                        {
                            str.AppendFormat("<{0}>{1}<{2}>", col.ColumnName, row[col.ColumnName].ToString2(), col.ColumnName);
                        }
                        str.Append("</Qyjbxx>");
                        #endregion

                        #region 企业资质
                        DataTable dt_qyzz = BLL.Get_uepp_ZjjgQyzzByQyID(row["qyID"].ToString2());
                        str.Append("<QyzzArray>");
                        foreach (DataRow row_qyzzmx in dt_qyzz.Rows)
                        {
                            str.Append("<Qyzz>");
                            foreach (DataColumn col in dt_qyzz.Columns)
                            {
                                str.AppendFormat("<{0}>{1}<{2}>", col.ColumnName, row_qyzzmx[col.ColumnName].ToString2(), col.ColumnName);
                            }
                            str.Append("</Qyzz>");

                        }
                        str.Append("</QyzzArray>");

                        #endregion

                        #region 企业证书
                        DataTable dt_qyzs = BLL.Get_uepp_ZjjgQyzsByQyID(row["qyID"].ToString2());
                        str.Append("<QyzsArray>");
                        foreach (DataRow row_qyzsmx in dt_qyzs.Rows)
                        {
                            str.Append("<Qyzs>");
                            foreach (DataColumn col in dt_qyzs.Columns)
                            {
                                str.AppendFormat("<{0}>{1}<{2}>", col.ColumnName, row_qyzsmx[col.ColumnName].ToString2(), col.ColumnName);
                            }
                            str.Append("</Qyzs>");

                        }
                        str.Append("</QyzsArray>");
                        #endregion
                        str.Append("</Qyxx>");

                    }
                    str.Append("</QyxxArray>");
                }
                else
                {
                    str.Append("<QyxxArray></QyxxArray>");
                }

                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = "29";
                row_apicb["apiMethod"] = "Read_Zjjgxx";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt("29", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                str.Append("<jkgb>接口关闭</jkgb>");
                str.Append("</ReturnInfo>");
                return str.ToString();
            }

            str.Append("</SparkSoftDataBody>");
            return str.ToString();
        }

        /// <summary>
        /// 获取注册执业人员信息
        /// </summary>
        /// <param name="ryzyzglx"></param>
        /// <returns></returns>
        [WebMethod]
        public string Read_Ryxx_Zczyry(string user, string password, string ryzyzglx)
        {
            SparkSoftDataBody sparkSoftDataBody = new SparkSoftDataBody();
            StringBuilder str = new StringBuilder();
            str.Append("<SparkSoftDataBody><ReturnInfo>");
            DataExchangeBLL BLL = new DataExchangeBLL();
            string apiMessage = string.Empty;// 2016.10.21
            //string methodMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {


                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>用户名或密码不正确！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }
                if (dt_user.Rows[0]["Has_Zczyry"].ToString2() == "0")
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>该用户没有权限获取注册执业人员信息！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }


                if (string.IsNullOrEmpty(ryzyzglx))
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>企业分类参数错误！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }

                List<string> list_ryzyzglx = BLL.Get_uepp_Ryzyzglx();

                String[] arr = ryzyzglx.Trim().Split(',');
                foreach (string item in arr)
                {
                    if (!list_ryzyzglx.Exists(p => p == item))
                    {
                        str.Append("<Status>0</Status>");
                        str.AppendFormat("<Description>企业分类参数错误：{0}</Description>", item);
                        str.Append("</ReturnInfo></SparkSoftDataBody>");
                        return str.ToString();
                    }
                }

                str.Append("<Status>1</Status>");
                str.Append("<Description></Description></ReturnInfo>");

                DataTable dt_Ryxx_Zczyry = BLL.Get_Ryxx_Zczyry(ryzyzglx);
                if (dt_Ryxx_Zczyry.Rows.Count > 0)
                {
                    str.Append("<RyxxArray>");
                    foreach (DataRow row in dt_Ryxx_Zczyry.Rows)
                    {
                        str.Append("<Ryxx>");

                        #region 人员基本信息

                        str.Append("<Ryjbxx>");

                        foreach (DataColumn col in dt_Ryxx_Zczyry.Columns)
                        {
                            str.AppendFormat("<{0}>{1}<{2}>", col.ColumnName, row[col.ColumnName].ToString2(), col.ColumnName);
                        }

                        str.Append("</Ryjbxx>");
                        #endregion

                        #region 人员专业信息
                        str.Append("<RyzyArray>");
                        DataTable dt_Ryzy_Zczyry = BLL.Get_Ryzy_Zczyry(row["ryID"].ToString());
                        foreach (DataRow row_ryzyxx in dt_Ryzy_Zczyry.Rows)
                        {
                            str.Append("<Ryzy>");

                            foreach (DataColumn col in dt_Ryzy_Zczyry.Columns)
                            {
                                str.AppendFormat("<{0}>{1}<{2}>", col.ColumnName, row_ryzyxx[col.ColumnName].ToString2(), col.ColumnName);
                            }

                            str.Append("</Ryzy>");
                        }
                        str.Append("</RyzyArray>");
                        #endregion

                        #region 人员证书信息
                        str.Append("<RyzsArray>");
                        DataTable dt_Ryzs_Zczyry = BLL.Get_Ryzs_Zczyry(row["ryID"].ToString());
                        foreach (DataRow row_ryzs in dt_Ryzs_Zczyry.Rows)
                        {
                            str.Append("<Ryzs>");

                            foreach (DataColumn col in dt_Ryzs_Zczyry.Columns)
                            {
                                str.AppendFormat("<{0}>{1}<{2}>", col.ColumnName, row_ryzs[col.ColumnName].ToString2(), col.ColumnName);
                            }

                            str.Append("</Ryzs>");
                        }
                        str.Append("</RyzsArray>");
                        #endregion

                        str.Append("</Ryxx>");
                    }
                    str.Append("</RyxxArray>");
                }
                else
                {
                    str.Append("<RyxxArray></RyxxArray>");
                }

                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = "29";
                row_apicb["apiMethod"] = "Read_Ryxx_Zczyry";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt("29", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                str.Append("<jkgb>接口关闭</jkgb>");
                str.Append("</ReturnInfo>");
                return str.ToString();
            }

            str.Append("</SparkSoftDataBody>");
            return str.ToString();
        }

        /// <summary>
        /// 获取安全生产管理人员信息
        /// </summary>
        /// <param name="ryzyzglx"></param>
        /// <returns></returns>
        [WebMethod]
        public string Read_Ryxx_Acscglry(string user, string password, string ryzyzglx)
        {
            SparkSoftDataBody sparkSoftDataBody = new SparkSoftDataBody();
            StringBuilder str = new StringBuilder();
            str.Append("<SparkSoftDataBody><ReturnInfo>");

            DataExchangeBLL BLL = new DataExchangeBLL();
            string apiMessage = string.Empty;// 2016.10.21
            //string methodMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {



                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>用户名或密码不正确！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }
                if (dt_user.Rows[0]["Has_Aqscglry"].ToString2() == "0")
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>该用户没有权限获取安生产管理人员信息！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }

                if (string.IsNullOrEmpty(ryzyzglx))
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>企业分类参数错误！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }
                List<string> list_ryzyzglx = BLL.Get_uepp_Ryzyzglx();

                String[] arr = ryzyzglx.Trim().Split(',');
                foreach (string item in arr)
                {
                    if (!list_ryzyzglx.Exists(p => p == item))
                    {
                        str.Append("<Status>0</Status>");
                        str.AppendFormat("<Description>企业分类参数错误：{0}</Description>", item);
                        str.Append("</ReturnInfo></SparkSoftDataBody>");
                        return str.ToString();
                    }
                }

                str.Append("<Status>1</Status>");
                str.Append("<Description></Description></ReturnInfo>");

                DataTable dt_Ryxx_Zczyry = BLL.Get_Ryxx_Aqscglry(ryzyzglx);
                if (dt_Ryxx_Zczyry.Rows.Count > 0)
                {
                    str.Append("<RyxxArray>");
                    foreach (DataRow row in dt_Ryxx_Zczyry.Rows)
                    {
                        str.Append("<Ryxx>");

                        #region 人员基本信息
                        str.Append("<Ryjbxx>");

                        foreach (DataColumn col in dt_Ryxx_Zczyry.Columns)
                        {
                            str.AppendFormat("<{0}>{1}<{2}>", col.ColumnName, row[col.ColumnName].ToString2(), col.ColumnName);
                        }

                        str.Append("</Ryjbxx>");

                        #endregion

                        #region 人员专业信息
                        str.Append("<RyzyArray>");
                        DataTable dt_Ryzy_Zczyry = BLL.Get_Ryzy_Aqscglry(row["ryID"].ToString());
                        foreach (DataRow row_ryzyxx in dt_Ryzy_Zczyry.Rows)
                        {
                            str.Append("<Ryzy>");

                            foreach (DataColumn col in dt_Ryzy_Zczyry.Columns)
                            {
                                str.AppendFormat("<{0}>{1}<{2}>", col.ColumnName, row_ryzyxx[col.ColumnName].ToString2(), col.ColumnName);
                            }

                            str.Append("</Ryzy>");
                        }
                        str.Append("</RyzyArray>");

                        #endregion

                        #region 人员证书信息
                        str.Append("<RyzsArray>");
                        DataTable dt_Ryzs_Zczyry = BLL.Get_Ryzs_Aqscglry(row["ryID"].ToString());
                        foreach (DataRow row_ryzs in dt_Ryzs_Zczyry.Rows)
                        {
                            str.Append("<Ryzs>");

                            foreach (DataColumn col in dt_Ryzs_Zczyry.Columns)
                            {
                                str.AppendFormat("<{0}>{1}<{2}>", col.ColumnName, row_ryzs[col.ColumnName].ToString2(), col.ColumnName);
                            }

                            str.Append("</Ryzs>");
                        }
                        str.Append("</RyzsArray>");

                        #endregion

                        str.Append("</Ryxx>");
                    }
                    str.Append("</RyxxArray>");
                }
                else
                {
                    str.Append("<RyxxArray></RyxxArray>");
                }

                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = "29";
                row_apicb["apiMethod"] = "Read_Ryxx_Acscglry";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt("29", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                str.Append("<jkgb>接口关闭</jkgb>");
                str.Append("</ReturnInfo>");
                return str.ToString();
            }

            str.Append("</SparkSoftDataBody>");
            return str.ToString();
        }

        /// <summary>
        /// 获取专业岗位管理人员信息
        /// </summary>
        /// <param name="ryzyzglx"></param>
        /// <returns></returns>
        [WebMethod]
        public string Read_Ryxx_Zygwglry(string user, string password, string ryzyzglx)
        {
            SparkSoftDataBody sparkSoftDataBody = new SparkSoftDataBody();
            StringBuilder str = new StringBuilder();
            str.Append("<SparkSoftDataBody><ReturnInfo>");

            DataExchangeBLL BLL = new DataExchangeBLL();
            string apiMessage = string.Empty;// 2016.10.21
            //string methodMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>用户名或密码不正确！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }
                if (dt_user.Rows[0]["Has_Zygwglry"].ToString2() == "0")
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>该用户没有权限获取专业岗位管理人员信息！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }

                if (string.IsNullOrEmpty(ryzyzglx))
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>企业分类参数错误！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }

                List<string> list_ryzyzglx = BLL.Get_uepp_Ryzyzglx();

                String[] arr = ryzyzglx.Trim().Split(',');
                foreach (string item in arr)
                {
                    if (!list_ryzyzglx.Exists(p => p == item))
                    {
                        str.Append("<Status>0</Status>");
                        str.AppendFormat("<Description>企业分类参数错误：{0}</Description>", item);
                        str.Append("</ReturnInfo></SparkSoftDataBody>");
                        return str.ToString();
                    }
                }

                str.Append("<Status>1</Status>");
                str.Append("<Description></Description></ReturnInfo>");

                DataTable dt_Ryxx_Zczyry = BLL.Get_Ryxx_Zygwglry(ryzyzglx);
                if (dt_Ryxx_Zczyry.Rows.Count > 0)
                {
                    str.Append("<RyxxArray>");
                    foreach (DataRow row in dt_Ryxx_Zczyry.Rows)
                    {
                        str.Append("<Ryxx>");

                        #region 人员基本信息
                        str.Append("<Ryjbxx>");

                        foreach (DataColumn col in dt_Ryxx_Zczyry.Columns)
                        {
                            str.AppendFormat("<{0}>{1}<{2}>", col.ColumnName, row[col.ColumnName].ToString2(), col.ColumnName);
                        }

                        str.Append("</Ryjbxx>");
                        #endregion

                        #region 人员专业信息
                        str.Append("<RyzyArray>");
                        DataTable dt_Ryzy_Zczyry = BLL.Get_Ryzy_Zygwglry(row["ryID"].ToString());
                        foreach (DataRow row_ryzyxx in dt_Ryzy_Zczyry.Rows)
                        {
                            str.Append("<Ryzy>");

                            foreach (DataColumn col in dt_Ryzy_Zczyry.Columns)
                            {
                                str.AppendFormat("<{0}>{1}<{2}>", col.ColumnName, row_ryzyxx[col.ColumnName].ToString2(), col.ColumnName);
                            }

                            str.Append("</Ryzy>");
                        }
                        str.Append("</RyzyArray>");

                        #endregion

                        #region 人员证书信息
                        str.Append("<RyzsArray>");
                        DataTable dt_Ryzs_Zczyry = BLL.Get_Ryzs_Zygwglry(row["ryID"].ToString());
                        foreach (DataRow row_ryzs in dt_Ryzs_Zczyry.Rows)
                        {
                            str.Append("<Ryzs>");

                            foreach (DataColumn col in dt_Ryzs_Zczyry.Columns)
                            {
                                str.AppendFormat("<{0}>{1}<{2}>", col.ColumnName, row_ryzs[col.ColumnName].ToString2(), col.ColumnName);
                            }

                            str.Append("</Ryzs>");
                        }
                        str.Append("</RyzsArray>");

                        #endregion
                        str.Append("</Ryxx>");
                    }

                    str.Append("</RyxxArray>");
                }
                else
                {
                    str.Append("<RyxxArray></RyxxArray>");
                }

                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = "29";
                row_apicb["apiMethod"] = "Read_Ryxx_Zygwglry";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt("29", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                str.Append("<jkgb>接口关闭</jkgb>");
                str.Append("</ReturnInfo>");
                return str.ToString();
            }
            str.Append("</SparkSoftDataBody>");
            return str.ToString();

        }

        /// <summary>
        /// 获取信用考评数据信息
        /// </summary>
        /// <param name="ryzyzglx"企业从事业务类型，如有多个业务类型，之间用“,”连接</param>
        /// <returns></returns>
        [WebMethod]
        public string Read_QyXykp(string user, string password, string csywlx, string zzjgdm, string qysd, int kpnd)
        {
            SparkSoftDataBody sparkSoftDataBody = new SparkSoftDataBody();
            StringBuilder str = new StringBuilder();
            str.Append("<SparkSoftDataBody><ReturnInfo>");

            DataExchangeBLL BLL = new DataExchangeBLL();
            string apiMessage = string.Empty;// 2016.10.21
            //string methodMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {


                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>用户名或密码不正确！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }
                if (dt_user.Rows[0]["Has_Xykp"].ToString2() == "0")
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>该用户没有权限获取企业信用考评信息！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }

                if (string.IsNullOrEmpty(csywlx))
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>企业从事业务类型参数错误！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }
                List<string> list_qycsywlx = BLL.Get_uepp_Qycsywlx();

                String[] arr = csywlx.Trim().Split(new char[] { ',', '，' });
                foreach (string item in arr)
                {
                    if (!list_qycsywlx.Exists(p => p == item))
                    {
                        str.Append("<Status>0</Status>");
                        str.AppendFormat("<Description>企业从事业务类型参数错误：{0}</Description>", item);
                        str.Append("</ReturnInfo></SparkSoftDataBody>");
                        return str.ToString();
                    }
                }

                str.Append("<Status>1</Status>");
                str.Append("<Description></Description></ReturnInfo>");

                List<IDataItem> list = new List<IDataItem>();
                IDataItem dataItem = new DataItem();
                dataItem.ItemName = "csywlx";
                dataItem.ItemData = csywlx;
                list.Add(dataItem);

                dataItem = new DataItem();
                dataItem.ItemName = "zzjgdm";
                dataItem.ItemData = zzjgdm;
                list.Add(dataItem);

                dataItem = new DataItem();
                dataItem.ItemName = "qysd";
                dataItem.ItemData = qysd;
                list.Add(dataItem);

                dataItem = new DataItem();
                dataItem.ItemName = "kpnd";
                dataItem.ItemData = kpnd.ToString();
                list.Add(dataItem);

                DataTable dt_QyXykp = BLL.Get_QyXykp(list);
                if (dt_QyXykp.Rows.Count > 0)
                {
                    str.Append("<XykpArray>");
                    foreach (DataRow row in dt_QyXykp.Rows)
                    {
                        str.Append("<Xykp>");

                        foreach (DataColumn col in dt_QyXykp.Columns)
                        {
                            str.AppendFormat("<{0}>{1}<{2}>", col.ColumnName, row[col.ColumnName].ToString2(), col.ColumnName);
                        }
                        str.Append("</Xykp>");
                    }

                    str.Append("</XykpArray>");
                }
                else
                {
                    str.Append("<XykpArray></XykpArray>");
                }

                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = "29";
                row_apicb["apiMethod"] = "Read_QyXykp";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt("29", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                str.Append("<jkgb>接口关闭</jkgb>");
                str.Append("</ReturnInfo>");
                return str.ToString();
            }

            str.Append("</SparkSoftDataBody>");
            return str.ToString();

        }



        /// <summary>
        /// 获取施工监理合同备案信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [WebMethod]
        public string Read_Htba(string user, string password)
        {
            SparkSoftDataBody sparkSoftDataBody = new SparkSoftDataBody();
            StringBuilder str = new StringBuilder();
            str.Append("<SparkSoftDataBody><ReturnInfo>");

            DataExchangeBLL BLL = new DataExchangeBLL();
            string apiMessage = string.Empty;// 2016.10.21
            //string methodMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {


                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>用户名或密码不正确！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }
                if (dt_user.Rows[0]["Has_TBContractRecordManage"].ToString2() == "0")
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>该用户没有权限获取合同备案信息！</Description>");
                    str.Append("</ReturnInfo></SparkSoftDataBody>");
                    return str.ToString();
                }

                str.Append("<Status>1</Status>");
                str.Append("<Description></Description></ReturnInfo>");

                DataTable dt_htba_sgjl = BLL.Get_TBContractRecordManage_SgJlHtba();
                if (dt_htba_sgjl.Rows.Count > 0)
                {
                    str.Append("<TBContractRecordManageArray>");
                    foreach (DataRow row in dt_htba_sgjl.Rows)
                    {
                        str.Append("<TBContractRecordManage>");

                        foreach (DataColumn col in dt_htba_sgjl.Columns)
                        {
                            str.AppendFormat("<{0}>{1}<{2}>", col.ColumnName, row[col.ColumnName].ToString2(), col.ColumnName);
                        }
                        str.Append("</TBContractRecordManage>");
                    }

                    str.Append("</TBContractRecordManageArray>");
                }
                else
                {
                    str.Append("<TBContractRecordManageArray></TBContractRecordManageArray>");
                }

                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = "29";
                row_apicb["apiMethod"] = "Read_Htba";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt("29",string.IsNullOrEmpty(apiMessage) == true ? "1" : "0",apiMessage);
            }
            else
            {
                str.Append("<jkgb>接口关闭</jkgb>");
                str.Append("</ReturnInfo>");
                return str.ToString();
            }

            str.Append("</SparkSoftDataBody>");
            return str.ToString();
        }




        /// <summary>
        /// 获取施工图审查信息
        /// </summary>
        /// <param name="PrjNum">项目统一编号</param>
        /// <returns></returns>
        [WebMethod]
        public string Read_Sgtsc(string PrjNum)
        {
            SparkSoftDataBody sparkSoftDataBody = new SparkSoftDataBody();
            StringBuilder str = new StringBuilder();
            str.Append("<SparkSoftDataBody>");

            DataExchangeBLL BLL = new DataExchangeBLL();
            string apiMessage = string.Empty;// 2016.10.21
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                DataTable dt_sgtsc = BLL.Get_TBProjectCensorInfoByPrjNum(PrjNum);
                if (dt_sgtsc.Rows.Count > 0)
                {
                    str.Append("<TBProjectCensorInfoArray>");
                    foreach (DataRow row in dt_sgtsc.Rows)
                    {
                        str.Append("<TBProjectCensorInfo>");

                        foreach (DataColumn col in dt_sgtsc.Columns)
                        {
                            str.AppendFormat("<{0}>{1}</{2}>", col.ColumnName, row[col.ColumnName].ToString2(), col.ColumnName);
                        }
                        str.Append("</TBProjectCensorInfo>");
                    }

                    str.Append("</TBProjectCensorInfoArray>");
                }
                else
                {
                    str.Append("<TBProjectCensorInfoArray></TBProjectCensorInfoArray>");
                }


                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = "29";
                row_apicb["apiMethod"] = "Read_Sgtsc";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt("29", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                str.Append("<jkgb>接口关闭</jkgb>");
                return str.ToString();
            }

            str.Append("</SparkSoftDataBody>");
            return str.ToString();
        }
        #endregion

        #region 关于一站式申报对接接口
        /// <summary>
        /// 获取无锡市某区质监机构某一日的申报数据
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="date"></param>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        [WebMethod]
        public string getAJSBBByDate(string user, string password, string date, string countryCode)
        {
            string result = String.Empty;
            string mainXml = string.Empty;
            DataExchangeBLL BLL = new DataExchangeBLL();
            DataExchangeBLLForYZSSB SBBLL = new DataExchangeBLLForYZSSB();

            string apiMessage = string.Empty; 
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    return result;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    return result;
                }
                string countryCodes = getCountryCodes(countryCode,"AJ", BLL);
               
                DataTable mainDt = SBBLL.GetAp_ajsbb(date, countryCodes);

                if (mainDt == null || mainDt.Rows.Count == 0)
                {
                    return String.Empty;
                }

                StringBuilder str = new StringBuilder();
                try
                {
                    str.AppendLine("<?xml version=\"1.0\" encoding=\"gb2312\"?>");

                    DataTable tempDt;

                    foreach (DataRow dataRow in mainDt.Rows)
                    {
                        str.AppendFormat("<{0}>", "data");

                        str.AppendFormat("<{0}>", "mainList"); 
                        str.AppendFormat("<{0}>", "main"); 
                        mainXml = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);
                        str.Append(mainXml);
                        str.AppendFormat("</{0}>", "main");
                        str.AppendFormat("</{0}>", "mainList");

                        tempDt = SBBLL.GetAp_ajsbb_ht(dataRow["uuid"].ToString());
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "htList", "htcontent"));

                        tempDt = SBBLL.GetAp_ajsbb_dwry(dataRow["uuid"].ToString());
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "dwryList", "dwrycontent"));

                        tempDt = SBBLL.GetAp_ajsbb_clqd(dataRow["uuid"].ToString());
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "clList", "clcontent"));

                        tempDt = SBBLL.GetAp_ajsbb_hjssjd(dataRow["uuid"].ToString());
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "hjssjdList", "hjssjdcontent"));

                        tempDt = SBBLL.GetAp_ajsbb_wxyjdgcqd(dataRow["uuid"].ToString());
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "wxygcList", "wxygccontent"));

                        tempDt = SBBLL.GetAp_ajsbb_cgmgcqd(dataRow["uuid"].ToString());
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "cgmgcList", "cgmgccontent"));


                        str.AppendFormat("</{0}>", "data");

                    }
                   
                     
                    return str.ToString();

                }
                catch
                {
                    return string.Empty;
                }

                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = "29";
                row_apicb["apiMethod"] = "getAJSBBByDate";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt("29", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                DataTable dt = BLL.GetAPIUnable();
                result = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
            }

            return result;

        }


        /// <summary>
        /// 按日期获取当日安监申报的uuid列表
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="date"></param>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        [WebMethod]
        public string getAJSBUuidsByDate(string user, string password, string date, string countryCode)
        {
            string result = String.Empty;
            string mainXml = string.Empty;
            DataExchangeBLL BLL = new DataExchangeBLL();
            DataExchangeBLLForYZSSB SBBLL = new DataExchangeBLLForYZSSB();

            string apiMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    return result;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    return result;
                }
                string countryCodes = getCountryCodes(countryCode, "AJ", BLL);

                DataTable mainDt = SBBLL.GetAp_ajsbb(date, countryCodes);

                if (mainDt == null || mainDt.Rows.Count == 0)
                {
                    return String.Empty;
                }

                StringBuilder str = new StringBuilder();
                try
                {
                    str.AppendLine("<?xml version=\"1.0\" encoding=\"gb2312\"?>");
                    str.AppendFormat("<{0}>", "body");
                    str.AppendFormat("<{0}>", "uuidList");
                    foreach (DataRow dataRow in mainDt.Rows)
                    {
                        str.AppendFormat("<{0}>", "primarykey");
                        mainXml = xmlHelper.EncodeString(dataRow["uuid"].ToString());
                        str.Append(mainXml);
                        str.AppendFormat("</{0}>", "primarykey");
                    }
                    str.AppendFormat("</{0}>", "uuidList");
                    str.AppendFormat("</{0}>", "body");
                    return str.ToString();

                }
                catch
                {
                    return string.Empty;
                }

                createApiLog("29", "getAJSBUuidsByDate", apiMessage, BLL);

            }
            else
            {
                DataTable dt = BLL.GetAPIUnable();
                result = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
            }

            return result;

        }


        /// <summary>
        /// 按uuid获取安监申报数据
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="uuid"></param>
        /// <returns></returns>
        [WebMethod]
        public string getAJSBBByUuid(string user, string password, string uuid)
        {
            string result = String.Empty;
            string mainXml = string.Empty;
            DataExchangeBLL BLL = new DataExchangeBLL();
            DataExchangeBLLForYZSSB SBBLL = new DataExchangeBLLForYZSSB();

            string apiMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    return result;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    return result;
                }

                DataTable mainDt = SBBLL.GetAp_ajsbb_byuuid(uuid);

                if (mainDt == null || mainDt.Rows.Count == 0)
                {
                    return String.Empty;
                }

                StringBuilder str = new StringBuilder();
                try
                {
                    str.AppendLine("<?xml version=\"1.0\" encoding=\"gb2312\"?>");

                    DataTable tempDt;

                    foreach (DataRow dataRow in mainDt.Rows)
                    {
                        str.AppendFormat("<{0}>", "data");

                        str.AppendFormat("<{0}>", "mainList");
                        str.AppendFormat("<{0}>", "main");
                        mainXml = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);
                        str.Append(mainXml);
                        str.AppendFormat("</{0}>", "main");
                        str.AppendFormat("</{0}>", "mainList");

                        tempDt = SBBLL.GetAp_ajsbb_ht(dataRow["uuid"].ToString());
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "htList", "htcontent"));

                        tempDt = SBBLL.GetAp_ajsbb_dwry(dataRow["uuid"].ToString());
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "dwryList", "dwrycontent"));

                        tempDt = SBBLL.GetAp_ajsbb_clqd(dataRow["uuid"].ToString());
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "clList", "clcontent"));

                        tempDt = SBBLL.GetAp_ajsbb_hjssjd(dataRow["uuid"].ToString());
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "hjssjdList", "hjssjdcontent"));

                        tempDt = SBBLL.GetAp_ajsbb_wxyjdgcqd(dataRow["uuid"].ToString());
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "wxygcList", "wxygccontent"));

                        tempDt = SBBLL.GetAp_ajsbb_cgmgcqd(dataRow["uuid"].ToString());
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "cgmgcList", "cgmgccontent"));

                        str.AppendFormat("</{0}>", "data");
                    }


                    return str.ToString();

                }
                catch
                {
                    return string.Empty;
                }

                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = "29";
                row_apicb["apiMethod"] = "getAJSBBByUuid";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt("29", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                DataTable dt = BLL.GetAPIUnable();
                result = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
            }

            return result;

        }


        /// <summary>
        /// 获取安监状态信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="tableName"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [WebMethod]
        public string getAJInfo(string user, string password, string tableName, string beginDate, string endDate)
        {
            string result = String.Empty;
            string xmlData = String.Empty;
            DataExchangeBLL BLL = new DataExchangeBLL();
            DataExchangeBLLForYZSSB SBBLL = new DataExchangeBLLForYZSSB();

            string apiMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    return result;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    return result;
                }

                List<IDataItem> list = new List<IDataItem>();
                IDataItem item;

                if (string.IsNullOrEmpty(tableName))
                {
                    return xmlData;
                }
                DataTable dt;

                if (!string.IsNullOrEmpty(beginDate))
                {
                    DateTime date;
                    if (DateTime.TryParse(beginDate, out date))
                    {
                        item = new DataItem();
                        item.ItemName = "modified";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.GreaterThanOrEqual;
                        item.ItemType = DataType.String;
                        item.ItemData = date.ToString("yyyy-MM-dd");
                        list.Add(item);
                    }
                }

                if (!string.IsNullOrEmpty(endDate))
                {
                    DateTime date;
                    if (DateTime.TryParse(endDate, out date))
                    {
                        item = new DataItem();
                        item.ItemName = "modified";
                        item.ItemData = date.ToString("yyyy-MM-dd");
                        item.ItemType = DataType.String;
                        item.ItemRelation = Bigdesk8.Data.DataRelation.LessThanOrEqual;
                        list.Add(item);
                    }
                }
                switch (tableName.ToLower())
                {
                    case "ap_ajsbb_info":
                        dt = SBBLL.GetAp_ajsbb_bytable(list);
                        result = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    case "ap_ajsbb_status_info":
                        dt = SBBLL.GetAp_ajsbb_status_bytable(list);
                        result = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    default:
                        result = "传入的表名不正确！";
                        break;

                }   

            }
            else
            {
                DataTable dt = BLL.GetAPIUnable();
                result = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
            }

            return result;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="date"></param>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        [WebMethod]
        public string getZJSBBByDate(string user, string password, string date, string countryCode)
        {
            string result = String.Empty;
            string mainXml = string.Empty;
            DataExchangeBLL BLL = new DataExchangeBLL();
            DataExchangeBLLForYZSSB SBBLL = new DataExchangeBLLForYZSSB();

            string apiMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    return result;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    return result;
                }
                string countryCodes = getCountryCodes(countryCode, "ZJ", BLL);

                DataTable mainDt = SBBLL.GetAp_zjsbb(date, countryCodes);

                if (mainDt == null || mainDt.Rows.Count == 0)
                {
                    return String.Empty;
                }

                StringBuilder str = new StringBuilder();
                try
                {
                    str.AppendLine("<?xml version=\"1.0\" encoding=\"gb2312\"?>");

                    DataTable tempDt;

                    foreach (DataRow dataRow in mainDt.Rows)
                    {
                        str.AppendFormat("<{0}>", "data");

                        str.AppendFormat("<{0}>", "mainList");
                        str.AppendFormat("<{0}>", "main");
                        mainXml = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);
                        str.Append(mainXml);
                        str.AppendFormat("</{0}>", "main");
                        str.AppendFormat("</{0}>", "mainList");

                        tempDt = SBBLL.GetAp_zjsbb_ht(dataRow["uuid"].ToString());
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "htList", "htcontent"));

                        tempDt = SBBLL.GetAp_zjsbb_dwry(dataRow["uuid"].ToString());
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "dwryList", "dwrycontent"));

                        tempDt = SBBLL.GetAp_zjsbb_schgs(dataRow["uuid"].ToString());
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "sgtscList", "sgtsccontent"));

                        tempDt = SBBLL.GetAp_zjsbb_dwgc(dataRow["uuid"].ToString());
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "dwgcList", "dwgccontent"));

                        tempDt = SBBLL.GetAp_zjsbb_clqd(dataRow["uuid"].ToString());
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "clList", "clcontent"));

                        str.AppendFormat("</{0}>", "data");

                    }


                    return str.ToString();

                }
                catch
                {
                    return string.Empty;
                }

                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = "29";
                row_apicb["apiMethod"] = "getAJSBBByDate";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt("29", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                DataTable dt = BLL.GetAPIUnable();
                result = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
            }

            return result;

        }

        [WebMethod]
        public string getZJSBBByDateAndDeptCode(string user, string password, string date, string deptCode)
        {
            string result = String.Empty;
            string mainXml = string.Empty;
            DataExchangeBLL BLL = new DataExchangeBLL();
            DataExchangeBLLForYZSSB SBBLL = new DataExchangeBLLForYZSSB();

            string apiMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    return result;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    return result;
                }


                DataTable mainDt = SBBLL.GetAp_zjsbb_byDeptCode(date, deptCode);

                if (mainDt == null || mainDt.Rows.Count == 0)
                {
                    return String.Empty;
                }

                StringBuilder str = new StringBuilder();
                try
                {
                    str.AppendLine("<?xml version=\"1.0\" encoding=\"gb2312\"?>");

                    DataTable tempDt;

                    foreach (DataRow dataRow in mainDt.Rows)
                    {
                        str.AppendFormat("<{0}>", "data");

                        str.AppendFormat("<{0}>", "mainList");
                        str.AppendFormat("<{0}>", "main");
                        mainXml = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);
                        str.Append(mainXml);
                        str.AppendFormat("</{0}>", "main");
                        str.AppendFormat("</{0}>", "mainList");

                        tempDt = SBBLL.GetAp_zjsbb_ht(dataRow["uuid"].ToString());
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "htList", "htcontent"));

                        tempDt = SBBLL.GetAp_zjsbb_dwry(dataRow["uuid"].ToString());
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "dwryList", "dwrycontent"));

                        tempDt = SBBLL.GetAp_zjsbb_schgs(dataRow["uuid"].ToString());
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "sgtscList", "sgtsccontent"));

                        tempDt = SBBLL.GetAp_zjsbb_dwgc(dataRow["uuid"].ToString());
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "dwgcList", "dwgccontent"));

                        tempDt = SBBLL.GetAp_zjsbb_clqd(dataRow["uuid"].ToString());
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "clList", "clcontent"));

                        str.AppendFormat("</{0}>", "data");

                    }


                    return str.ToString();

                }
                catch
                {
                    return string.Empty;
                }

                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = "29";
                row_apicb["apiMethod"] = "getZJSBBByDateAndDeptCode";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt("29", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                DataTable dt = BLL.GetAPIUnable();
                result = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
            }

            return result;

        }


        /// <summary>
        /// 按日期获取当日质监申报的uuid列表
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="date"></param>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        [WebMethod]
        public string getZJSBUuidsByDate(string user, string password, string date, string countryCode)
        {
            string result = String.Empty;
            string mainXml = string.Empty;
            DataExchangeBLL BLL = new DataExchangeBLL();
            DataExchangeBLLForYZSSB SBBLL = new DataExchangeBLLForYZSSB();

            string apiMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    return result;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    return result;
                }
                string countryCodes = getCountryCodes(countryCode, "ZJ", BLL);

                DataTable mainDt = SBBLL.GetAp_zjsbb(date, countryCodes);

                if (mainDt == null || mainDt.Rows.Count == 0)
                {
                    return String.Empty;
                }

                StringBuilder str = new StringBuilder();
                try
                {
                    str.AppendLine("<?xml version=\"1.0\" encoding=\"gb2312\"?>");

                    str.AppendFormat("<{0}>", "body");
                    str.AppendFormat("<{0}>", "uuidList");
                    foreach (DataRow dataRow in mainDt.Rows)
                    {
                        
                        str.AppendFormat("<{0}>", "primarykey");
                        mainXml = xmlHelper.EncodeString(dataRow["uuid"].ToString());
                        str.Append(mainXml);
                        str.AppendFormat("</{0}>", "primarykey");
                        
                    }
                    str.AppendFormat("</{0}>", "uuidList");
                    str.AppendFormat("</{0}>", "body");
                    return str.ToString();

                }
                catch
                {
                    return string.Empty;
                }

                createApiLog("29", "getZJSBUuidsByDate", apiMessage, BLL);

            }
            else
            {
                DataTable dt = BLL.GetAPIUnable();
                result = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
            }

            return result;

        }

        [WebMethod]
        public string getZJSBUuidsByDateAndDeptCode(string user, string password, string date, string deptCode)
        {
            string result = String.Empty;
            string mainXml = string.Empty;
            DataExchangeBLL BLL = new DataExchangeBLL();
            DataExchangeBLLForYZSSB SBBLL = new DataExchangeBLLForYZSSB();

            string apiMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    return result;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    return result;
                }


                DataTable mainDt = SBBLL.GetAp_zjsbb_byDeptCode(date, deptCode);

                if (mainDt == null || mainDt.Rows.Count == 0)
                {
                    return String.Empty;
                }

                StringBuilder str = new StringBuilder();
                try
                {
                    str.AppendLine("<?xml version=\"1.0\" encoding=\"gb2312\"?>");

                    str.AppendFormat("<{0}>", "body");
                    str.AppendFormat("<{0}>", "uuidList");
                    foreach (DataRow dataRow in mainDt.Rows)
                    {

                        str.AppendFormat("<{0}>", "primarykey");
                        mainXml = xmlHelper.EncodeString(dataRow["uuid"].ToString());
                        str.Append(mainXml);
                        str.AppendFormat("</{0}>", "primarykey");

                    }
                    str.AppendFormat("</{0}>", "uuidList");
                    str.AppendFormat("</{0}>", "body");
                    return str.ToString();

                }
                catch
                {
                    return string.Empty;
                }

                createApiLog("29", "getZJSBUuidsByDateAndDeptCode", apiMessage, BLL);

            }
            else
            {
                DataTable dt = BLL.GetAPIUnable();
                result = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
            }

            return result;

        }


        /// <summary>
        /// 按uuid获取质监申报数据
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="uuid"></param>
        /// <returns></returns>
        [WebMethod]
        public string getZJSBBByUuid(string user, string password, string uuid)
        {
            string result = String.Empty;
            string mainXml = string.Empty;
            DataExchangeBLL BLL = new DataExchangeBLL();
            DataExchangeBLLForYZSSB SBBLL = new DataExchangeBLLForYZSSB();

            string apiMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    return result;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    return result;
                } 

                DataTable mainDt = SBBLL.GetAp_zjsbb_byuuid(uuid);

                if (mainDt == null || mainDt.Rows.Count == 0)
                {
                    return String.Empty;
                }

                StringBuilder str = new StringBuilder();
                try
                {
                    str.AppendLine("<?xml version=\"1.0\" encoding=\"gb2312\"?>");

                    DataTable tempDt;

                    foreach (DataRow dataRow in mainDt.Rows)
                    {
                        str.AppendFormat("<{0}>", "data");

                        str.AppendFormat("<{0}>", "mainList");
                        str.AppendFormat("<{0}>", "main");
                        mainXml = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);
                        str.Append(mainXml);
                        str.AppendFormat("</{0}>", "main");
                        str.AppendFormat("</{0}>", "mainList");

                        tempDt = SBBLL.GetAp_zjsbb_ht(dataRow["uuid"].ToString());
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "htList", "htcontent"));

                        tempDt = SBBLL.GetAp_zjsbb_dwry(dataRow["uuid"].ToString());
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "dwryList", "dwrycontent"));

                        tempDt = SBBLL.GetAp_zjsbb_schgs(dataRow["uuid"].ToString());
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "sgtscList", "sgtsccontent"));

                        tempDt = SBBLL.GetAp_zjsbb_dwgc(dataRow["uuid"].ToString());
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "dwgcList", "dwgccontent"));

                        tempDt = SBBLL.GetAp_zjsbb_clqd(dataRow["uuid"].ToString());
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "clList", "clcontent"));

                        str.AppendFormat("</{0}>", "data");

                    }
                    return str.ToString();

                }
                catch
                {
                    return string.Empty;
                }

                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = "29";
                row_apicb["apiMethod"] = "getAJSBBByDate";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt("29", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                DataTable dt = BLL.GetAPIUnable();
                result = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
            }

            return result;

        }


        [WebMethod]
        public string pushAJSBJG(string user, string password, String deptcode, String sbPassword, String resultXml)
        {
            ProcessResultData result = new ProcessResultData();
            try
            {
                DataExchangeBLL BLL = new DataExchangeBLL();
                DataExchangeBLLForYZSSB YZSSBBLL = new DataExchangeBLLForYZSSB();
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    return result.ResultMessage;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    return result.ResultMessage;
                }

                if (string.IsNullOrEmpty(resultXml))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "传入的XML格式数据为空！";
                    return result.ResultMessage;
                }
                string message;
                DataTable dt_Data = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(resultXml, out message);
                //DataTable dt_Data = xmlHelper.ConvertXMLToDataTable(resultXml, out message);

                if (dt_Data == null)
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = message;
                    return result.ResultMessage;
                }
                string xml = xmlHelper.ConvertDataTableToXML(dt_Data, "dataTable", "row");
                WebCommon.WriteLog("\r\n传入数据：DateTime:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\ndata:" + xml + "\r\n");

                /** TODO：用户写操作权限待添加
                if (dt_user.Rows[0]["Has_TBProjectInfo_Write"].ToString2() == "0")
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "该用户不允许保存" + tableName + "表数据！";
                    return result.ResultMessage;
                }*/

                result = YZSSBBLL.pushAJSBJG(user,deptcode, sbPassword, dt_Data);
                
            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = ex.Message;
            }
            WebCommon.WriteLog("pushAJSBJG:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "|" + result.ResultMessage + "\r\n");
            return result.ResultMessage;
        }

        /// <summary>
        /// 推送监督通知书
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="deptcode"></param>
        /// <param name="sbPassword"></param>
        /// <param name="resultXml"></param>
        /// <returns></returns>
        [WebMethod]
        public string pushAJTZS(string user, string password, String deptcode, String sbPassword, String resultXml)
        {
            ProcessResultData result = new ProcessResultData();
            try
            {
                DataExchangeBLL BLL = new DataExchangeBLL();
                DataExchangeBLLForYZSSB YZSSBBLL = new DataExchangeBLLForYZSSB();
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    return result.ResultMessage;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    return result.ResultMessage;
                }

                if (string.IsNullOrEmpty(resultXml))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "传入的XML格式数据为空！";
                    return result.ResultMessage;
                }
                string message;
                string tzsXml = string.Empty;
                
                //DataTable dt_Data = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(resultXml, out message);
                //DataTable dt_Data = xmlHelper.ConvertXMLToDataTable(resultXml, out message);
                DataTable dt_Data = null;

                DataTable jdryDtData = null;
                //监督人员列表
                int dwgcIndex = resultXml.IndexOf("<jdryList>");
                string jdryListXml = string.Empty;

                //tzsXml = resultXml.Substring(0, dwgcIndex);

                if (dwgcIndex >= 0)
                {
                    tzsXml = resultXml.Substring(0, dwgcIndex) + resultXml.Substring(resultXml.LastIndexOf("</jdryList>") + "</jdryList>".Length);
                    WebCommon.WriteLog("tzsXml:" + tzsXml);
                    dt_Data = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(tzsXml, out message);

                    jdryListXml = resultXml.Substring(dwgcIndex, resultXml.LastIndexOf("</jdryList>") - dwgcIndex + "</jdryList>".Length);
                    jdryListXml = "<root>" + jdryListXml + "</root>";
                    jdryDtData = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(jdryListXml, out message);
                    //jdryDtData = xmlHelper.ConvertXMLToDataTable(dwgcList, out message);
                }
                else
                {
                    dt_Data = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(resultXml, out message);
                }


                if ((dt_Data == null) || (jdryDtData == null))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = message;
                    return result.ResultMessage;
                }
                string xml = xmlHelper.ConvertDataTableToXML(dt_Data, "dataTable", "row");
                WebCommon.WriteLog("\r\n传入数据：DateTime:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\ndata:" + xml + "\r\n");
                string jdryXml = xmlHelper.ConvertDataTableToXML(jdryDtData, "dataTable", "row");
                WebCommon.WriteLog("\r\n传入数据：DateTime:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\ndata:" + jdryXml + "\r\n");

                /** TODO：用户写操作权限待添加
                if (dt_user.Rows[0]["Has_TBProjectInfo_Write"].ToString2() == "0")
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "该用户不允许保存" + tableName + "表数据！";
                    return result.ResultMessage;
                }*/

                result = YZSSBBLL.pushAJTZS(user, deptcode, sbPassword, dt_Data, jdryDtData);

            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = ex.Message;
                WebCommon.WriteLog("ex.Message:" + ex.Message);
            
            }
            WebCommon.WriteLog("pushAJTZS:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "|" + result.ResultMessage + "\r\n");
            return result.ResultMessage;
        }

        /// <summary>
        /// 推送终止施工安全监督告知书
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="deptcode"></param>
        /// <param name="sbPassword"></param>
        /// <param name="resultXml"></param>
        /// <returns></returns>
        [WebMethod]
        public string pushAJZZGZ(string user, string password, String deptcode, String sbPassword, String resultXml)
        {
            ProcessResultData result = new ProcessResultData();
            try
            {
                DataExchangeBLL BLL = new DataExchangeBLL();
                DataExchangeBLLForYZSSB YZSSBBLL = new DataExchangeBLLForYZSSB();
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    return result.ResultMessage;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    return result.ResultMessage;
                }

                if (string.IsNullOrEmpty(resultXml))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "传入的XML格式数据为空！";
                    return result.ResultMessage;
                }
                string message;
                DataTable dt_Data = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(resultXml, out message);
                //DataTable dt_Data = xmlHelper.ConvertXMLToDataTable(resultXml, out message);

                if (dt_Data == null)
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = message;
                    return result.ResultMessage;
                }
                string xml = xmlHelper.ConvertDataTableToXML(dt_Data, "dataTable", "row");
                WebCommon.WriteLog("\r\n传入数据：DateTime:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\ndata:" + xml + "\r\n");

                /** TODO：用户写操作权限待添加
                if (dt_user.Rows[0]["Has_TBProjectInfo_Write"].ToString2() == "0")
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "该用户不允许保存" + tableName + "表数据！";
                    return result.ResultMessage;
                }*/

                result = YZSSBBLL.pushAJZZGZ(user, deptcode, sbPassword, dt_Data);

            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = ex.Message;
            }

            return result.ResultMessage;
        }


        [WebMethod]
        public string pushZJSBJG(string user, string password, String deptcode, String sbPassword, String resultXml)
        {
            ProcessResultData result = new ProcessResultData();
            try
            {
                DataExchangeBLL BLL = new DataExchangeBLL();
                DataExchangeBLLForYZSSB YZSSBBLL = new DataExchangeBLLForYZSSB();
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    return result.ResultMessage;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    return result.ResultMessage;
                }

                if (string.IsNullOrEmpty(resultXml))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "传入的XML格式数据为空！";
                    return result.ResultMessage;
                }
                string message;
                DataTable dt_Data = null;
                //DataTable dt_Data = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(resultXml, out message);
                //DataTable dt_Data = xmlHelper.ConvertXMLToDataTable(resultXml, out message);
                DataTable dwgcDtData = null;
                //单位工程列表
                int dwgcIndex = resultXml.IndexOf("<dwgcList>");
                string dwgcList = string.Empty;
                string sbjgXml = string.Empty;

                if (dwgcIndex >= 0)
                {
                    sbjgXml = resultXml.Substring(0, dwgcIndex) + resultXml.Substring(resultXml.LastIndexOf("</dwgcList>") + "</dwgcList>".Length);
                    WebCommon.WriteLog("sbjgXml:" + sbjgXml);
                    dt_Data = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(sbjgXml, out message);

                    dwgcList = resultXml.Substring(dwgcIndex, resultXml.LastIndexOf("</dwgcList>") - dwgcIndex + "</dwgcList>".Length);
                    dwgcList = "<root>" + dwgcList + "</root>";
                    dwgcDtData = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(dwgcList, out message);
                    //dwgcDtData = xmlHelper.ConvertXMLToDataTable(dwgcList, out message);
                }
                else
                {
                    dt_Data = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(resultXml, out message);
                }

                if (dt_Data == null)
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = message;
                    return result.ResultMessage;
                }
                string xml = xmlHelper.ConvertDataTableToXML(dt_Data, "dataTable", "row");
                WebCommon.WriteLog("\r\n传入数据：DateTime:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\ndata:" + xml + "\r\n");
                if (dwgcDtData != null)
                {
                    WebCommon.WriteLog("\r\n" + xmlHelper.ConvertDataTableToXML(dwgcDtData, "dataTable", "row"));
                }
                /** TODO：用户写操作权限待添加
                if (dt_user.Rows[0]["Has_TBProjectInfo_Write"].ToString2() == "0")
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "该用户不允许保存" + tableName + "表数据！";
                    return result.ResultMessage;
                }*/

                result = YZSSBBLL.pushZJSBJG(user, deptcode, sbPassword, dt_Data, dwgcDtData);

            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = ex.Message;
            }

            return result.ResultMessage;
        }

        /// <summary>
        /// 推送质量监督通知书
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="deptcode"></param>
        /// <param name="sbPassword"></param>
        /// <param name="resultXml"></param>
        /// <returns></returns>
        [WebMethod]
        public string pushZJTZS(string user, string password, String deptcode, String sbPassword, String resultXml)
        {
            ProcessResultData result = new ProcessResultData();
            try
            {
                DataExchangeBLL BLL = new DataExchangeBLL();
                DataExchangeBLLForYZSSB YZSSBBLL = new DataExchangeBLLForYZSSB();
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    return result.ResultMessage;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    return result.ResultMessage;
                }

                if (string.IsNullOrEmpty(resultXml))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "传入的XML格式数据为空！";
                    return result.ResultMessage;
                }
                string message;
                string tzsXml = string.Empty;

                //DataTable dt_Data = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(resultXml, out message);
                //DataTable dt_Data = xmlHelper.ConvertXMLToDataTable(resultXml, out message);
                DataTable dt_Data = null;

                DataTable jdryDtData = null;
                //监督人员列表
                int dwgcIndex = resultXml.IndexOf("<jdryList>");
                string jdryListXml = string.Empty;

                tzsXml = resultXml.Substring(0, dwgcIndex);

                if (dwgcIndex >= 0)
                {
                    tzsXml = resultXml.Substring(0, dwgcIndex) + resultXml.Substring(resultXml.LastIndexOf("</jdryList>") + "</jdryList>".Length);
                    WebCommon.WriteLog("tzsXml:" + tzsXml);
                    dt_Data = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(tzsXml, out message);

                    jdryListXml = resultXml.Substring(dwgcIndex, resultXml.LastIndexOf("</jdryList>") - dwgcIndex + "</jdryList>".Length);
                    jdryListXml = "<root>" + jdryListXml + "</root>";
                    jdryDtData = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(jdryListXml, out message);
                    //jdryDtData = xmlHelper.ConvertXMLToDataTable(dwgcList, out message);
                }
                else
                {
                    dt_Data = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(resultXml, out message);
                }


                if ((dt_Data == null) || (jdryDtData == null))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = message;
                    return result.ResultMessage;
                }
                string xml = xmlHelper.ConvertDataTableToXML(dt_Data, "dataTable", "row");
                WebCommon.WriteLog("\r\n传入数据：DateTime:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\ndata:" + xml + "\r\n");
                string jdryXml = xmlHelper.ConvertDataTableToXML(jdryDtData, "dataTable", "row");
                WebCommon.WriteLog("\r\n传入数据：DateTime:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\ndata:" + jdryXml + "\r\n");

                /** TODO：用户写操作权限待添加
                if (dt_user.Rows[0]["Has_TBProjectInfo_Write"].ToString2() == "0")
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "该用户不允许保存" + tableName + "表数据！";
                    return result.ResultMessage;
                }*/

                result = YZSSBBLL.pushZJTZS(user, deptcode, sbPassword, dt_Data, jdryDtData);

            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = ex.Message;
                WebCommon.WriteLog("ex.Message:" + ex.Message);

            }

            return result.ResultMessage;
        }


        /// <summary>
        /// 按uuid获取质监申报数据
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="uuid"></param>
        /// <returns></returns>
        [WebMethod]
        public string fetchDataByUuid(string user, string apiPassword, string uuid, string deptCode, string password , string deptType )
        {
            string result = String.Empty;
            string mainXml = string.Empty;
            DataExchangeBLL BLL = new DataExchangeBLL();
            DataExchangeBLLForYZSSBDownload SBBLL = new DataExchangeBLLForYZSSBDownload();

            string apiMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("27");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(apiPassword))
                {
                    return result;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, apiPassword);
                if (dt_user.Rows.Count == 0)
                {
                    return result;
                }
                 
                try
                {
                    if ("AJ".Equals(deptType))
                    {
                        result = SBBLL.YourTask_PullAJSBDataFromSythptByUUID(deptCode, password, uuid);
                    }
                    else {
                        result = SBBLL.YourTask_PullZJSBDataFromSythptByUUID(deptCode, password, uuid);
                    }

                }
                catch(Exception e)
                {
                    result = e.Message; 
                }
 

            }
             

            return result;

        }


        /// <summary>
        /// 获取没有标注位置信息的项目
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetProjectInfoWithoutAddressPoint(string user, string password, string beginDate, string endDate)
        {
            string apiFlowId = "30";

            string mainXml = string.Empty;
            DataExchangeBLL BLL = new DataExchangeBLL();
            DataExchangeBLLForGIS SBBLL = new DataExchangeBLLForGIS();

            ProcessResultData result = new ProcessResultData();

            string apiMessage = string.Empty;
            if (isApiOpen(apiFlowId, BLL))
            {
                if (!accessValidate(user, password, BLL))
                {
                    result.code = ProcessResult.用户名或密码错误;
                    return result.ResultMessage;
                }

                DataTable mainDt = SBBLL.GetProjectInfoWithAddressPoint(beginDate, endDate, "FALSE");

                if (mainDt == null || mainDt.Rows.Count == 0)
                {
                    result.code = ProcessResult.未找到对应项目;
                    return result.ResultMessage;
                }

                StringBuilder str = new StringBuilder();
                try
                {
                    str.AppendLine("<?xml version=\"1.0\" encoding=\"gb2312\"?>");

                    str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(mainDt, "dataTable", "row"));

                    return str.ToString();

                }
                catch (Exception ex)
                {
                    result.code = ProcessResult.内部错误;
                    result.message = ex.Message;
                    return result.ResultMessage;
                }

                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = apiFlowId;
                row_apicb["apiMethod"] = "getProjectInfo";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt(apiFlowId, string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                result.code = ProcessResult.接口关闭;
            }

            return result.ResultMessage;
        }

        /// <summary>
        /// 获取项目信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="countyNum"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetProjectInfo(string user, string password, string countyNum, string beginDate, string endDate)
        {
            string apiFlowId = "30";

            string mainXml = string.Empty;
            DataExchangeBLL BLL = new DataExchangeBLL();
            DataExchangeBLLForGIS SBBLL = new DataExchangeBLLForGIS();

            ProcessResultData result = new ProcessResultData();

            string apiMessage = string.Empty;
            if (isApiOpen(apiFlowId, BLL))
            {
                if (!accessValidate(user, password, BLL))
                {
                    result.code = ProcessResult.用户名或密码错误;
                    return result.ResultMessage;
                }

                DataTable mainDt = SBBLL.GetProjectInfo(countyNum, beginDate, endDate);

                if (mainDt == null || mainDt.Rows.Count == 0)
                {
                    result.code = ProcessResult.未找到对应项目;
                    return result.ResultMessage;
                }

                StringBuilder str = new StringBuilder();
                try
                {
                    str.AppendLine("<?xml version=\"1.0\" encoding=\"gb2312\"?>");

                    str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(mainDt, "dataTable", "row"));

                    return str.ToString();

                }
                catch (Exception ex)
                {
                    result.code = ProcessResult.内部错误;
                    result.message = ex.Message;
                    return result.ResultMessage;
                }

                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = apiFlowId;
                row_apicb["apiMethod"] = "getProjectInfo";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt(apiFlowId, string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                result.code = ProcessResult.接口关闭;
            }

            return result.ResultMessage;
        }

        /// <summary>
        /// 获取子项目信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetSubProjectInfo(string user, string password, string beginDate, string endDate)
        {
            string apiFlowId = "30";

            string mainXml = string.Empty;
            DataExchangeBLL BLL = new DataExchangeBLL();
            DataExchangeBLLForGIS SBBLL = new DataExchangeBLLForGIS();

            ProcessResultData result = new ProcessResultData();

            string apiMessage = string.Empty;
            if (isApiOpen(apiFlowId, BLL))
            {
                if (!accessValidate(user, password, BLL))
                {
                    result.code = ProcessResult.用户名或密码错误;
                    return result.ResultMessage;
                }

                DataTable mainDt = SBBLL.GetSubProjectInfo("", beginDate, endDate);

                if (mainDt == null || mainDt.Rows.Count == 0)
                {
                    result.code = ProcessResult.未找到对应项目;
                    return result.ResultMessage;
                }

                StringBuilder str = new StringBuilder();
                try
                {
                    str.AppendLine("<?xml version=\"1.0\" encoding=\"gb2312\"?>");

                    str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(mainDt, "dataTable", "row"));

                    return str.ToString();

                }
                catch (Exception ex)
                {
                    result.code = ProcessResult.内部错误;
                    result.message = ex.Message;
                    return result.ResultMessage;
                }

                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = apiFlowId;
                row_apicb["apiMethod"] = "getProjectInfo";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt(apiFlowId, string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                result.code = ProcessResult.接口关闭;
            }

            return result.ResultMessage;
        }

        /// <summary>
        /// 获取项目施工许可证
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="sbdqbm"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetBuilderLicenceManage(string user, string password, string sbdqbm, string beginDate, string endDate)
        {
            string apiFlowId = "30";

            string mainXml = string.Empty;
            DataExchangeBLL BLL = new DataExchangeBLL();
            DataExchangeBLLForGIS SBBLL = new DataExchangeBLLForGIS();

            ProcessResultData result = new ProcessResultData();

            string apiMessage = string.Empty;
            if (isApiOpen(apiFlowId, BLL))
            {
                if (!accessValidate(user, password, BLL))
                {
                    result.code = ProcessResult.用户名或密码错误;
                    return result.ResultMessage;
                }

                DataTable mainDt = SBBLL.GetBuilderLicenceManage(sbdqbm, beginDate, endDate);

                if (mainDt == null || mainDt.Rows.Count == 0)
                {
                    result.code = ProcessResult.未找到对应项目;
                    return result.ResultMessage;
                }

                StringBuilder str = new StringBuilder();
                try
                {
                    str.AppendLine("<?xml version=\"1.0\" encoding=\"gb2312\"?>");

                    str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(mainDt, "dataTable", "row"));

                    return str.ToString();

                }
                catch (Exception ex)
                {
                    result.code = ProcessResult.内部错误;
                    result.message = ex.Message;
                    return result.ResultMessage;
                }

                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = apiFlowId;
                row_apicb["apiMethod"] = "GetBuilderLicenceManage";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt(apiFlowId, string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                result.code = ProcessResult.接口关闭;
            }

            return result.ResultMessage;
        }

        /// <summary>
        /// 获取项目竣工备案信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="sbdqbm"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetProjectFinishManage(string user, string password, string sbdqbm, string beginDate, string endDate)
        {
            string apiFlowId = "30";

            string mainXml = string.Empty;
            DataExchangeBLL BLL = new DataExchangeBLL();
            DataExchangeBLLForGIS SBBLL = new DataExchangeBLLForGIS();

            ProcessResultData result = new ProcessResultData();

            string apiMessage = string.Empty;
            if (isApiOpen(apiFlowId, BLL))
            {
                if (!accessValidate(user, password, BLL))
                {
                    result.code = ProcessResult.用户名或密码错误;
                    return result.ResultMessage;
                }

                DataTable mainDt = SBBLL.GetProjectFinishManage(sbdqbm, beginDate, endDate);

                if (mainDt == null || mainDt.Rows.Count == 0)
                {
                    result.code = ProcessResult.未找到对应项目;
                    return result.ResultMessage;
                }

                StringBuilder str = new StringBuilder();
                try
                {
                    str.AppendLine("<?xml version=\"1.0\" encoding=\"gb2312\"?>");

                    str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(mainDt, "dataTable", "row"));

                    return str.ToString();

                }
                catch (Exception ex)
                {
                    result.code = ProcessResult.内部错误;
                    result.message = ex.Message;
                    return result.ResultMessage;
                }

                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = apiFlowId;
                row_apicb["apiMethod"] = "GetProjectFinishManage";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt(apiFlowId, string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                result.code = ProcessResult.接口关闭;
            }

            return result.ResultMessage;
        }

        /// <summary>
        /// 获取项目施工图审查信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="sbdqbm"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetProjectCensorInfo(string user, string password, string sbdqbm, string beginDate, string endDate)
        {
            string apiFlowId = "30";

            string mainXml = string.Empty;
            DataExchangeBLL BLL = new DataExchangeBLL();
            DataExchangeBLLForGIS SBBLL = new DataExchangeBLLForGIS();

            ProcessResultData result = new ProcessResultData();

            string apiMessage = string.Empty;
            if (isApiOpen(apiFlowId, BLL))
            {
                if (!accessValidate(user, password, BLL))
                {
                    result.code = ProcessResult.用户名或密码错误;
                    return result.ResultMessage;
                }

                DataTable mainDt = SBBLL.GetProjectCensorInfo(sbdqbm, beginDate, endDate);

                if (mainDt == null || mainDt.Rows.Count == 0)
                {
                    result.code = ProcessResult.未找到对应项目;
                    return result.ResultMessage;
                }

                StringBuilder str = new StringBuilder();
                try
                {
                    str.AppendLine("<?xml version=\"1.0\" encoding=\"gb2312\"?>");

                    str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(mainDt, "dataTable", "row"));

                    return str.ToString();

                }
                catch (Exception ex)
                {
                    result.code = ProcessResult.内部错误;
                    result.message = ex.Message;
                    return result.ResultMessage;
                }

                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = apiFlowId;
                row_apicb["apiMethod"] = "GetProjectFinishManage";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt(apiFlowId, string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                result.code = ProcessResult.接口关闭;
            }

            return result.ResultMessage;
        }

        /// <summary>
        /// 查询项目列表
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="prjNum"></param>
        /// <param name="prjName"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        [WebMethod]
        public string queryProjectList(string user, string password, string prjNum, string prjName, string location)
        {
            return this.queryProjectListEx(user, password, prjNum, prjName, "", "", location);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="prjNum"></param>
        /// <param name="prjName"></param>
        /// <param name="buildCorpCode"></param>
        /// <param name="buildCorpName"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        [WebMethod]
        public string queryProjectListEx(string user, string password, string prjNum, string prjName, String buildCorpCode, String buildCorpName, string location)
        {

            string apiFlowId = "30";

            string mainXml = string.Empty;
            DataExchangeBLL BLL = new DataExchangeBLL();
            DataExchangeBLLForGIS SBBLL = new DataExchangeBLLForGIS();

            ProcessResultData result = new ProcessResultData();

            string apiMessage = string.Empty;
            if (isApiOpen(apiFlowId, BLL))
            {
                if (!accessValidate(user, password, BLL))
                {
                    result.code = ProcessResult.用户名或密码错误;
                    return result.ResultMessage;
                }

                DataTable mainDt = SBBLL.GetProject_Additional(prjNum, prjName, buildCorpCode, buildCorpName, location);

                if (mainDt == null || mainDt.Rows.Count == 0)
                {
                    result.code = ProcessResult.未找到对应项目;
                    return result.ResultMessage;
                }

                StringBuilder str = new StringBuilder();
                try
                {
                    str.AppendLine("<?xml version=\"1.0\" encoding=\"gb2312\"?>");

                    str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(mainDt, "dataTable", "row"));

                    return str.ToString();

                }
                catch (Exception ex)
                {
                    result.code = ProcessResult.内部错误;
                    result.message = ex.Message;
                    return result.ResultMessage;
                }

                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = apiFlowId;
                row_apicb["apiMethod"] = "queryProjectList";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt(apiFlowId, string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                result.code = ProcessResult.接口关闭;
            }

            return result.ResultMessage;

        }

        /// <summary>
        /// 查询项目列表
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        [WebMethod]
        public string queryProjectListByRange(string user, string password, string range, string beginDate, string endDate)
        {
            string apiFlowId = "30";

            string mainXml = string.Empty;
            DataExchangeBLL BLL = new DataExchangeBLL();
            DataExchangeBLLForGIS SBBLL = new DataExchangeBLLForGIS();

            ProcessResultData result = new ProcessResultData();

            string apiMessage = string.Empty;
            if (isApiOpen(apiFlowId, BLL))
            {
                if (!accessValidate(user, password, BLL))
                {
                    result.code = ProcessResult.用户名或密码错误;
                    return result.ResultMessage;
                }

                DataTable mainDt = SBBLL.GetProjectByRange(range, "", beginDate, endDate);

                if (mainDt == null || mainDt.Rows.Count == 0)
                {
                    result.code = ProcessResult.未找到对应项目;
                    return result.ResultMessage;
                }

                StringBuilder str = new StringBuilder();
                try
                {
                    str.AppendLine("<?xml version=\"1.0\" encoding=\"gb2312\"?>");

                    str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(mainDt, "dataTable", "row"));

                    return str.ToString();

                }
                catch (Exception ex)
                {
                    result.code = ProcessResult.内部错误;
                    result.message = ex.Message;
                    return result.ResultMessage;
                }

                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = apiFlowId;
                row_apicb["apiMethod"] = "queryProjectList";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt(apiFlowId, string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                result.code = ProcessResult.接口关闭;
            }

            return result.ResultMessage;
        }

        /// <summary>
        /// 统计指定年限、坐标范围内的项目信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="range"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [WebMethod]
        public string StatisticsPrjListByRange(string user, string password, string range, string beginDate, string endDate)
        {
            string apiFlowId = "30";

            DataExchangeBLL BLL = new DataExchangeBLL();

            ProcessResultData result = new ProcessResultData();

            string apiMessage = string.Empty;
            if (isApiOpen(apiFlowId, BLL))
            {
                if (!accessValidate(user, password, BLL))
                {
                    result.code = ProcessResult.用户名或密码错误;
                    return result.ResultMessage;
                }
                try
                {
                    DataExchangeBLLForGIS SBBLL = new DataExchangeBLLForGIS();
                    DataTable dt = SBBLL.GetProjectByRange(range, "", beginDate, endDate);
                    return this.getStatistics(dt, "");
                }
                catch (Exception ex)
                {
                    result.code = ProcessResult.内部错误;
                    result.message = ex.Message;
                    return result.ResultMessage;
                }

            }
            else
            {
                result.code = ProcessResult.接口关闭;
            }
            return result.ResultMessage;
        }

        /// <summary>
        /// 统计指定年限、区域内的项目信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="qy"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [WebMethod]
        public string StatisticsPrjListByQy(string user, string password, string qy, string beginDate, string endDate)
        {

            string apiFlowId = "30";

            DataExchangeBLL BLL = new DataExchangeBLL();

            ProcessResultData result = new ProcessResultData();

            string apiMessage = string.Empty;
            if (isApiOpen(apiFlowId, BLL))
            {
                if (!accessValidate(user, password, BLL))
                {
                    result.code = ProcessResult.用户名或密码错误;
                    return result.ResultMessage;
                }
                try
                {
                    DataExchangeBLLForGIS SBBLL = new DataExchangeBLLForGIS();
                    DataTable dt = SBBLL.GetProjectByRange("", qy, beginDate, endDate);
                    return this.getStatistics(dt, qy);
                }
                catch (Exception ex)
                {
                    result.code = ProcessResult.内部错误;
                    result.message = ex.Message;
                    return result.ResultMessage;
                }

            }
            else
            {
                result.code = ProcessResult.接口关闭;
            }
            return result.ResultMessage;
        }

        private string getStatistics(DataTable dt, string qy)
        {

            int prjNum = dt.Rows.Count;
            int buildNum = 0;
            int finishNum = 0;
            int jsdwNum = 0;
            int kcdwNum = 0;
            int sjdwNum = 0;
            int sgdwNum = 0;
            int jldwNum = 0;
            int stdwNum = 0;
            int daNum = 0;

            ArrayList jsdwlist = new ArrayList();
            ArrayList kcdwlist = new ArrayList();
            ArrayList sjdwlist = new ArrayList();
            ArrayList sgdwlist = new ArrayList();
            ArrayList jldwlist = new ArrayList();
            ArrayList stdwlist = new ArrayList();

            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

            foreach (DataRow row in dt.Rows)
            {
                string edate = row["EDate"].ToString2();
                if (!string.IsNullOrEmpty(edate) && string.Compare(currentDate, edate)>=0)
                {
                    finishNum++;
                }
                else
                {
                    buildNum++;
                }
                string jsdw = row["BuildCorpName"].ToString2();
                string kcdw = row["EconCorpName"].ToString2();
                string sjdw = row["DesignCorpName"].ToString2();
                string sgdw = row["ConsCorpName"].ToString2();
                string jldw = row["SuperCorpName"].ToString2();
                string stdw = row["CensorCorpName"].ToString2();

                this.parseEntity(jsdwlist, jsdw);
                this.parseEntity(kcdwlist, kcdw);
                this.parseEntity(sjdwlist, sjdw);
                this.parseEntity(sgdwlist, sgdw);
                this.parseEntity(jldwlist, jldw);
                this.parseEntity(stdwlist, stdw);

                if (!string.IsNullOrEmpty(row["DocCount"].ToString2()))
                {
                    daNum += row["DocCount"].ToInt32();
                }

            }

            jsdwNum = jsdwlist.ToArray().Length;
            kcdwNum = kcdwlist.ToArray().Length;
            sjdwNum = sjdwlist.ToArray().Length;
            sgdwNum = sgdwlist.ToArray().Length;
            jldwNum = jldwlist.ToArray().Length;
            stdwNum = stdwlist.ToArray().Length;

            DataTable resultTable = new DataTable();

            resultTable.Columns.Add("qy", typeof(string));
            resultTable.Columns.Add("prjNum", typeof(int));
            resultTable.Columns.Add("buildNum", typeof(int));
            resultTable.Columns.Add("finishNum", typeof(int));
            resultTable.Columns.Add("jsdwNum", typeof(int));
            resultTable.Columns.Add("kcdwNum", typeof(int));
            resultTable.Columns.Add("sjdwNum", typeof(int));
            resultTable.Columns.Add("sgdwNum", typeof(int));
            resultTable.Columns.Add("jldwNum", typeof(int));
            resultTable.Columns.Add("stdwNum", typeof(int));
            resultTable.Columns.Add("daNum", typeof(int));

            DataRow dataRow = resultTable.NewRow();

            dataRow["qy"] = qy;
            dataRow["prjNum"] = prjNum;
            dataRow["buildNum"] = buildNum;
            dataRow["finishNum"] = finishNum;
            dataRow["jsdwNum"] = jsdwNum;
            dataRow["kcdwNum"] = kcdwNum;
            dataRow["sjdwNum"] = sjdwNum;
            dataRow["sgdwNum"] = sgdwNum;
            dataRow["jldwNum"] = jldwNum;
            dataRow["stdwNum"] = stdwNum;
            dataRow["daNum"] = daNum;

            resultTable.Rows.Add(dataRow);

            return xmlHelper.ConvertDataTableToXMLWithBase64Encoding(resultTable, "dataTable", "row");
        }

        /// <summary>
        /// parse string and add into list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="str"></param>
        private void parseEntity(ArrayList list, string str)
        {
            if(!string.IsNullOrEmpty(str))
            {
                string[] arr = str.Split('/');
                foreach(string item in arr)
                {
                    if (!item.Equals("无") && !item.Equals("-") && !list.Contains(item))
                    {
                        list.Add(item);
                    }
                }
            }
        }

        /// <summary>
        /// 查询项目信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="prjNum"></param>
        /// <returns></returns>
        [WebMethod]
        public string queryProjectInfo(string user, string password, string prjNum)
        {
            string apiFlowId = "30";

            string mainXml = string.Empty;
            DataExchangeBLL BLL = new DataExchangeBLL();
            DataExchangeBLLForGIS SBBLL = new DataExchangeBLLForGIS();

            ProcessResultData result = new ProcessResultData();

            string apiMessage = string.Empty;
            if (isApiOpen(apiFlowId, BLL))
            {
                if (!accessValidate(user, password, BLL))
                {
                    result.code = ProcessResult.用户名或密码错误;
                    return result.ResultMessage;
                }

                DataTable mainDt = SBBLL.GetProject(prjNum);

                if (mainDt == null || mainDt.Rows.Count == 0)
                {
                    result.code = ProcessResult.未找到对应项目;
                    return result.ResultMessage;
                }

                StringBuilder str = new StringBuilder();
                try
                {
                    str.AppendLine("<?xml version=\"1.0\" encoding=\"gb2312\"?>");

                    DataTable tempDt;

                    /*
                    mainDt.Columns.Add("EconCorpName", typeof(string));
                    mainDt.Columns.Add("DesignCorpName", typeof(string));
                    mainDt.Columns.Add("ConsCorpName", typeof(string));
                    mainDt.Columns.Add("SuperCorpName", typeof(string));
                    mainDt.Columns.Add("CensorCorpName", typeof(string));
                    */
                    foreach (DataRow dataRow in mainDt.Rows)
                    {
                        str.AppendFormat("<{0}>", "data");

                        str.AppendFormat("<{0}>", "project");

                        DataTable blDataTable = SBBLL.GetBuildingLicense(prjNum);

                        DataTable cencorDataTable = SBBLL.GetProjectCensor(prjNum);

                        /*改用SQL方式获取
                        StringBuilder consStr = new StringBuilder();
                        StringBuilder designStr = new StringBuilder();
                        StringBuilder econStr = new StringBuilder();
                        StringBuilder superStr = new StringBuilder();
                        StringBuilder censorStr = new StringBuilder();
                        foreach (DataRow dr in blDataTable.Rows)
                        {
                            string econ = dr["EconCorpName"].ToString2();
                            if (!this.isItemEmpty(econ) && !econStr.ToString2().Contains(econ))
                            {
                                econStr.Append("/").Append(econ);
                            }
                            string design = dr["DesignCorpName"].ToString2();
                            if (!this.isItemEmpty(design) && !designStr.ToString2().Contains(design))
                            {
                                designStr.Append("/").Append(design);
                            }
                            string cons = dr["ConsCorpName"].ToString2();
                            if (!this.isItemEmpty(econ) && !consStr.ToString2().Contains(cons))
                            {
                                consStr.Append("/").Append(cons);
                            }
                            string super = dr["SuperCorpName"].ToString2();
                            if (!this.isItemEmpty(super) && !superStr.ToString2().Contains(super))
                            {
                                superStr.Append("/").Append(super);
                            }
                        }

                        dataRow["EconCorpName"] = this.handleStringBuilder(consStr);
                        dataRow["DesignCorpName"] = this.handleStringBuilder(designStr);
                        dataRow["ConsCorpName"] = this.handleStringBuilder(econStr);
                        dataRow["SuperCorpName"] = this.handleStringBuilder(superStr);

                        

                        foreach (DataRow dr in cencorDataTable.Rows)
                        {
                            string censor = dr["CensorCorpName"].ToString2();
                            if (!this.isItemEmpty(censor) && !censorStr.ToString2().Contains(censor))
                            {
                                censorStr.Append("/").Append(censor);
                            }
                        }

                        dataRow["CensorCorpName"] = this.handleStringBuilder(censorStr);
                        */

                        mainXml = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);
                        str.Append(mainXml);
                        str.AppendFormat("</{0}>", "project");

                        tempDt = SBBLL.GetSubProject(prjNum);
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "subProjectList", "subProject"));

                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(blDataTable, "buildingLicenseList", "buildingLicense"));

                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(cencorDataTable, "projectCensorList", "projectCensor"));

                        tempDt = SBBLL.GetProjectFinish(prjNum);
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "projectFinishList", "projectFinish"));

                        str.AppendFormat("</{0}>", "data");

                    }

                    return str.ToString();

                }
                catch (Exception ex)
                {
                    result.code = ProcessResult.内部错误;
                    result.message = ex.Message;
                    return result.ResultMessage;
                }

                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = apiFlowId;
                row_apicb["apiMethod"] = "queryProjectInfo";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt(apiFlowId, string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                result.code = ProcessResult.接口关闭;
            }

            return result.ResultMessage;
        }

        private bool isItemEmpty(string item)
        {
            bool flag = false;
            if (string.IsNullOrEmpty(item))
            {
                flag = true;
            }else if ("-".Equals(item) || " ".Equals(item))
            {
                flag = true;
            }
            return flag;
        }

        private string handleStringBuilder(StringBuilder sb)
        {
            if (sb.Length > 0)
            {
                sb.Remove(0, 1);
            }
            return sb.ToString2();
        }

        /// <summary>
        /// 配套费登录验证
        /// </summary>
        /// <param name="prjNum"></param>
        /// <param name="prjPassword"></param>
        /// <returns></returns>
        [WebMethod]
        public string MatchFeeLogin(string prjNum, string prjPassword)
        {
            string apiFlowId = "30";

            string mainXml = string.Empty;
            DataExchangeBLL BLL = new DataExchangeBLL();
            DataExchangeBLLForUpload SBBLL = new DataExchangeBLLForUpload();

            ProcessResultData result = new ProcessResultData();

            string apiMessage = string.Empty;
            if (isApiOpen(apiFlowId, BLL))
            {

                DataTable mainDt = SBBLL.GetTBData_TBProjectAdditionalInfo(prjNum);

                if (mainDt == null || mainDt.Rows.Count == 0)
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "项目编号不存在";
                    
                }
                else
                {
                    foreach(DataRow dr in mainDt.Rows)
                    {
                        string target = dr["prjpassword"].ToString2();
                        if (prjPassword.Equals2(target, false))
                        {
                            result.code = ProcessResult.数据保存成功;
                        }
                        else
                        {
                            result.code = ProcessResult.用户名或密码错误;
                            result.message = "项目编号或密码错误";
                        }
                    }

                }

                return result.ResultMessage;

                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = apiFlowId;
                row_apicb["apiMethod"] = "MatchFeeLogin";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt(apiFlowId, string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                result.code = ProcessResult.接口关闭;
            }

            return result.ResultMessage;
        }

        #endregion

        #region 关于处罚系统的接口
        [WebMethod]
        public string queryProjectAndEntity(string user, string password, string prjNum, string projectName, string partyCode, string partyName)
        {

            string apiFlowId = "30";

            string mainXml = string.Empty;
            DataExchangeBLL BLL = new DataExchangeBLL();
            DataExchangeBLLForGIS BLLGIS = new DataExchangeBLLForGIS();

            ProcessResultData result = new ProcessResultData();

            string apiMessage = string.Empty;
            if (isApiOpen(apiFlowId, BLL))
            {
                if (!accessValidate(user, password, BLL))
                {
                    result.code = ProcessResult.用户名或密码错误;
                    return result.ResultMessage;
                }

                DataTable mainDt = BLLGIS.queryProjectAndEntity(prjNum, projectName, partyCode, partyName);

                if (mainDt == null || mainDt.Rows.Count == 0)
                {
                    result.code = ProcessResult.未找到对应项目;
                    return result.ResultMessage;
                }

                StringBuilder str = new StringBuilder();
                try
                {
                    str.AppendLine("<?xml version=\"1.0\" encoding=\"gb2312\"?>");

                    str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(mainDt, "dataTable", "row"));

                    return str.ToString();

                }
                catch (Exception ex)
                {
                    result.code = ProcessResult.内部错误;
                    result.message = ex.Message;
                    return result.ResultMessage;
                }

                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = apiFlowId;
                row_apicb["apiMethod"] = "queryProjectAndEntity";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt(apiFlowId, string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                result.code = ProcessResult.接口关闭;
            }

            return result.ResultMessage;
        }

        #endregion

        #region 保存数据

        /// <summary>
        /// 功能：向无锡数据中心传送数据
        /// 作者：孙刚
        /// 时间：2015-03-31
        /// </summary>
        /// <param name="tableName">数据表名</param>
        /// <param name="xmlData">XML内容</param>
        /// <param name="user">用户名称</param>
        /// <param name="password">用户密码</param>
        /// <returns></returns>
        [WebMethod]
        public string SaveTBDataToZx(string tableName, string xmlData, string user, string password)
        {
            ProcessResultData result = new ProcessResultData();
            try
            {
                DataExchangeBLL BLL = new DataExchangeBLL();
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    return result.ResultMessage;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    return result.ResultMessage;
                }

                if (string.IsNullOrEmpty(tableName))
                {
                    result.code = ProcessResult.数据表名不正确;
                    result.message = "数据表名不正确！";
                    return result.ResultMessage;
                }

                if (string.IsNullOrEmpty(xmlData))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "传入的XML格式数据为空！";
                    return result.ResultMessage;
                }
                string message;
                DataTable dt_Data = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(xmlData, out message);

                if (dt_Data == null)
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = message;
                    return result.ResultMessage;
                }
                string xml = xmlHelper.ConvertDataTableToXML(dt_Data, "dataTable", "row");
                WebCommon.WriteLog("\r\n传入数据：" + "tableName:" + tableName + ",DateTime:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\ndata:" + xml + "\r\n");
                //bool isSdSx = dt_user.Rows[0]["Flag"].ToString2() == "1";
                switch (tableName.ToLower())
                {
                    case "tbprojectinfo"://TBProjectInfo 
                        if (dt_user.Rows[0]["Has_TBProjectInfo_Write"].ToString2() == "0")
                        {
                            result.code = ProcessResult.保存失败和失败原因;
                            result.message = "该用户不允许保存" + tableName + "表数据！";
                            return result.ResultMessage;
                        }
                        result = BLL.SaveTBData_TBProjectInfo(user, dt_Data);

                        break;
                    case "tbprojectadditionalinfo"://TBProjectAdditionalInfo 
                        if (dt_user.Rows[0]["Has_TBProjectInfo_Write"].ToString2() == "0")
                        {
                            result.code = ProcessResult.保存失败和失败原因;
                            result.message = "该用户不允许保存" + tableName + "表数据！";
                            return result.ResultMessage;
                        }
                        else if (dt_Data.Rows == null || dt_Data.Rows.Count != 1)
                        {
                            result.code = ProcessResult.保存失败和失败原因;
                            result.message = "一次请传入一条项目登记补充数据";
                            return result.ResultMessage;
                        }
                        result = BLL.SaveTBData_TBProjectAdditionalInfo(user, dt_Data);

                        break;
                    case "xykp"://xypj_kpjlhz, 信用考评数据 
                        if (dt_user.Rows[0]["Has_Xykp_Write"].ToString2() == "0")
                        {
                            result.code = ProcessResult.保存失败和失败原因;
                            result.message = "该用户不允许保存" + tableName + "表数据！";
                            return result.ResultMessage;
                        }
                        else if (dt_Data.Rows == null || dt_Data.Rows.Count != 1)
                        {
                            result.code = ProcessResult.保存失败和失败原因;
                            result.message = "一次请传入一条信用考评";
                            return result.ResultMessage;
                        }
                        result = BLL.SaveTBData_Xykp(user, dt_Data);

                        break;

                    case "xm_gcdjb_dtxm":
                        if (dt_user.Rows[0]["Has_xm_gcdjb_dtxm_Write"].ToString2() == "0")
                        {
                            result.code = ProcessResult.保存失败和失败原因;
                            result.message = "该用户不允许保存" + tableName + "表数据！";
                            return result.ResultMessage;
                        }
                        result = BLL.SaveTBData_xm_gcdjb_dtxm(user, dt_Data);
                        break;

                    case "tbtenderinfo"://TBTenderInfo
                        if (dt_user.Rows[0]["Has_TBTenderInfo_Write"].ToString2() == "0")
                        {
                            result.code = ProcessResult.保存失败和失败原因;
                            result.message = "该用户不允许保存" + tableName + "表数据！";
                            return result.ResultMessage;
                        }
                        result = BLL.SaveTBData_TBTenderInfo(user, dt_Data);
                        break;
                    case "tbcontractrecordmanage": //TBContractRecordManage
                        if (dt_user.Rows[0]["Has_TBContractRecordManage_Write"].ToString2() == "0")
                        {
                            result.code = ProcessResult.保存失败和失败原因;
                            result.message = "该用户不允许保存" + tableName + "表数据！";
                            return result.ResultMessage;
                        }
                        result = BLL.SaveTBData_TBContractRecordManage(user, dt_Data);
                        break;
                    case "tbprojectcensorinfo"://TBProjectCensorInfo

                        if (dt_user.Rows[0]["Has_TBProjectCensorInfo_Write"].ToString2() == "0")
                        {
                            result.code = ProcessResult.保存失败和失败原因;
                            result.message = "该用户不允许保存" + tableName + "表数据！";
                            return result.ResultMessage;
                        }
                        result = BLL.SaveTBData_TBProjectCensorInfo(user, dt_Data);
                        break;
                    case "tbprojectdesigneconuserinfo"://TBProjectDesignEconUserInfo
                        if (dt_user.Rows[0]["Has_TBProjectDesignEconUserInfo_Write"].ToString2() == "0")
                        {
                            result.code = ProcessResult.保存失败和失败原因;
                            result.message = "该用户不允许保存" + tableName + "表数据！";
                            return result.ResultMessage;
                        }

                        result = BLL.SaveTBData_TBProjectDesignEconUserInfo(user, dt_Data);
                        break;
                    case "tbbuilderlicencemanage"://TBBuilderLicenceManage
                        if (dt_user.Rows[0]["Has_TBBuilderLicenceManage_Write"].ToString2() == "0")
                        {
                            result.code = ProcessResult.保存失败和失败原因;
                            result.message = "该用户不允许保存" + tableName + "表数据！";
                            return result.ResultMessage;
                        }

                        result = BLL.SaveTBData_TBBuilderLicenceManage(user, dt_Data);
                        break;
                    case "tbprojectbuilderuserinfo"://TBProjectBuilderUserInfo
                        if (dt_user.Rows[0]["Has_TBProjectBuilderUserInfo_Write"].ToString2() == "0")
                        {
                            result.code = ProcessResult.保存失败和失败原因;
                            result.message = "该用户不允许保存" + tableName + "表数据！";
                            return result.ResultMessage;
                        }
                        result = BLL.SaveTBData_TBProjectBuilderUserInfo(user, dt_Data);
                        break;
                    case "tbprojectfinishmanage"://TBProjectFinishManage
                        if (dt_user.Rows[0]["Has_TBProjectFinishManage_Write"].ToString2() == "0")
                        {
                            result.code = ProcessResult.保存失败和失败原因;
                            result.message = "该用户不允许保存" + tableName + "表数据！";
                            return result.ResultMessage;
                        }
                        result = BLL.SaveTBData_TBProjectFinishManage(user, dt_Data);
                        break;
                    case "aj_gcjbxx":
                        if (dt_user.Rows[0]["Has_aj_gcjbxx_Write"].ToString2() == "0")
                        {
                            result.code = ProcessResult.保存失败和失败原因;
                            result.message = "该用户不允许保存" + tableName + "表数据！";
                            return result.ResultMessage;
                        }
                        result = BLL.SaveTBData_aj_gcjbxx(user, dt_Data);
                        break;
                    case "zj_gcjbxx":
                        if (dt_user.Rows[0]["Has_zj_gcjbxx_Write"].ToString2() == "0")
                        {
                            result.code = ProcessResult.保存失败和失败原因;
                            result.message = "该用户不允许保存" + tableName + "表数据！";
                            return result.ResultMessage;
                        }
                        result = BLL.SaveTBData_zj_gcjbxx(user, dt_Data);
                        break;
                    case "zj_gcjbxx_zrdw":
                        if (dt_user.Rows[0]["Has_zj_gcjbxx_Write"].ToString2() == "0")
                        {
                            result.code = ProcessResult.保存失败和失败原因;
                            result.message = "该用户不允许保存" + tableName + "表数据！"; ;
                            return result.ResultMessage;
                        }
                        result = BLL.SaveTBData_zj_gcjbxx_zrdw(user, dt_Data);
                        break;
                    case "aj_zj_sgxk_relation":
                        if (dt_user.Rows[0]["Has_aj_zj_sgxk_relation_Write"].ToString2() == "0")
                        {
                            result.code = ProcessResult.保存失败和失败原因;
                            result.message = "该用户不允许保存" + tableName + "表数据！";
                            return result.ResultMessage;
                        }
                        result = BLL.SaveTBData_aj_zj_sgxk_relation(user, dt_Data);
                        break;
                    default:
                        result.code = ProcessResult.数据表名不正确;
                        result.message = "传入的表名不正确！";
                        break;
                }
            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = ex.Message;
            }

            return result.ResultMessage;

        }
        /// <summary>
        /// 向无锡数据中心推送企业信用考评数据
        /// 作者：孙刚
        /// 时间：2015-11-19
        /// </summary>
        /// <param name="xmlData"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [WebMethod]
        public string SaveQyXykpToZx(string xmlData, string user, string password)
        {
            ProcessResultData result = new ProcessResultData();
            try
            {
                DataExchangeBLL BLL = new DataExchangeBLL();
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    return result.ResultMessage;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    return result.ResultMessage;
                }

                if (string.IsNullOrEmpty(xmlData))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "传入的XML格式数据为空！";
                    return result.ResultMessage;
                }
                string message;
                DataTable dt_Data = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(xmlData, out message);

                if (dt_Data == null)
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = message;
                    return result.ResultMessage;
                }
                string xml = xmlHelper.ConvertDataTableToXML(dt_Data, "dataTable", "row");
                WebCommon.WriteLog("\r\n传入数据：" + ",DateTime:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\ndata:" + xml + "\r\n");

                if (dt_user.Rows[0]["Has_Xykp_Write"].ToString2() == "0")
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "该用户不允许保存企业信用考评数据！";
                    return result.ResultMessage;
                }
                result = BLL.SaveData_QyXmkp(user, dt_Data);

            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = ex.Message;
            }

            return result.ResultMessage;

        }

        /// <summary>
        /// 保存无锡市行政处罚信息
        /// </summary>
        /// <param name="xmlData"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [WebMethod]
        public string SaveXzcfToZx(string xmlData, string user, string password)
        {
            ProcessResultData result = new ProcessResultData();
            try
            {
                DataExchangeBLL BLL = new DataExchangeBLL();
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    return result.ResultMessage;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    return result.ResultMessage;
                }

                if (string.IsNullOrEmpty(xmlData))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "传入的XML格式数据为空！";
                    return result.ResultMessage;
                }
                string message;
                DataTable dt_Data = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(xmlData, out message);

                if (dt_Data == null)
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = message;
                    return result.ResultMessage;
                }
                string xml = xmlHelper.ConvertDataTableToXML(dt_Data, "dataTable", "row");
                WebCommon.WriteLog("\r\n传入行政处罚数据：" + ",DateTime:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\ndata:" + xml + "\r\n");

                if (dt_user.Rows[0]["Has_Xzcf_Write"].ToString2() == "0")
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "该用户不允许保存行政处罚数据！";
                    return result.ResultMessage;
                }
                result = BLL.SaveData_Xzcf(user, dt_Data);

            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = ex.Message;
            }

            return result.ResultMessage;

        }

        /// <summary>
        /// 保存安监申报项目信息
        /// </summary>
        /// <param name="xmlData"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [WebMethod]
        public string SaveAjsbbToZx(string tableName, string xmlData, string user, string password)
        {
            ProcessResultData result = new ProcessResultData();
            try
            {
                DataExchangeBLL BLL = new DataExchangeBLL();
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    return result.ResultMessage;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    return result.ResultMessage;
                }

                if (string.IsNullOrEmpty(xmlData))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "传入的XML格式数据为空！";
                    return result.ResultMessage;
                }
                string message;
                DataTable dt_Data = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(xmlData, out message);

                if (dt_Data == null)
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = message;
                    return result.ResultMessage;
                }
                string xml = xmlHelper.ConvertDataTableToXML(dt_Data, "dataTable", "row");
                WebCommon.WriteLog("\r\nSaveAjsbbGisToZx：DateTime:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\ndata:" + xml + "\r\n");

                if (dt_user.Rows[0]["Has_Ap_ajsbb_gis_Write"].ToString2() == "0")
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "该用户不允许保存Ap_ajsbb_gis表数据！";
                    return result.ResultMessage;
                }
                switch (tableName.ToLower())
                {
                    case "ap_ajsbb_gis": 
                        result = BLL.SaveAjsbbGis(user, dt_Data);
                        break;
                    case "ap_ajsbb_info":
                        //result = BLL.SaveAjsbbSuperviseInfo(user, dt_Data);
                        break;
                    case "ap_ajsbb_info_manual":
                        result = BLL.SaveAjsbbSuperviseInfo(user, dt_Data);
                        break;
                    case "ap_ajsbb_status_info":
                        result = BLL.SaveAjsbbStausInfo(user, dt_Data);
                        break;
                    default:
                        result.code = ProcessResult.数据表名不正确;
                        result.message = "传入的表名不正确！";
                        break;

                }  
                
            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = ex.Message;
            }

            return result.ResultMessage;

        }

        /// <summary>
        /// 保存无锡市保障办（保障房源信息）
        /// </summary>
        /// <param name="xmlData"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [WebMethod]
        public string SaveBzbBZFY(string xmlData)
        {
            ProcessResultData result = new ProcessResultData();
            try
            {
                DataExchangeBLL BLL = new DataExchangeBLL();
                
                if (string.IsNullOrEmpty(xmlData))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "传入的XML格式数据为空！";
                    return result.ResultMessage;
                }
                string message;
                DataTable dt_Data = xmlHelper.ConvertXMLToDataTable(xmlData, out message);

                if (dt_Data == null)
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = message;
                    return result.ResultMessage;
                }

                result = BLL.SaveBzbBZFY(dt_Data);

            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = ex.Message;
            }

            return result.ResultMessage;

        }

        /// <summary>
        /// 保存无锡市保障办（保障对象信息）
        /// </summary>
        /// <param name="xmlData"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [WebMethod]
        public string SaveBzbBZDX(string xmlData)
        {
            ProcessResultData result = new ProcessResultData();
            try
            {
                DataExchangeBLL BLL = new DataExchangeBLL();

                if (string.IsNullOrEmpty(xmlData))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "传入的XML格式数据为空！";
                    return result.ResultMessage;
                }
                string message;
                DataTable dt_Data = xmlHelper.ConvertXMLToDataTable(xmlData, out message);

                if (dt_Data == null)
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = message;
                    return result.ResultMessage;
                }

                result = BLL.SaveBzbBZDX(dt_Data);

            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = ex.Message;
            }

            return result.ResultMessage;

        }


        /// <summary>
        ///  保存无锡市保障办（保障对象家庭成员）
        /// </summary>
        /// <param name="xmlData"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [WebMethod]
        public string SaveBzbBZJTCY(string xmlData)
        {
            ProcessResultData result = new ProcessResultData();
            try
            {
                DataExchangeBLL BLL = new DataExchangeBLL();

                if (string.IsNullOrEmpty(xmlData))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "传入的XML格式数据为空！";
                    return result.ResultMessage;
                }
                string message;
                DataTable dt_Data = xmlHelper.ConvertXMLToDataTable(xmlData, out message);

                if (dt_Data == null)
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = message;
                    return result.ResultMessage;
                }

                result = BLL.SaveBzbBZJTCY(dt_Data);

            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = ex.Message;
            }

            return result.ResultMessage;

        }

        /// <summary>
        /// 保存项目档案信息接口
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        [WebMethod]
        public string SaveProjectDocInfo(string user, string password, string xmlData)
        {
            ProcessResultData result = new ProcessResultData();
            try
            {
                DataExchangeBLL BLL = new DataExchangeBLL();
                DataExchangeBLLForGIS BLLGIS = new DataExchangeBLLForGIS();

                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    return result.ResultMessage;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    return result.ResultMessage;
                }

                if (string.IsNullOrEmpty(xmlData))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "传入的XML格式数据为空！";
                    return result.ResultMessage;
                }
                string message;
                DataTable dt_Data = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(xmlData, out message);

                if (dt_Data == null)
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = message;
                    return result.ResultMessage;
                }
                string xml = xmlHelper.ConvertDataTableToXML(dt_Data, "dataTable", "row");
                WebCommon.WriteLog("\r\n传入项目档案数据：" + ",DateTime:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\ndata:" + xml + "\r\n");

                /*
                if (dt_user.Rows[0]["Has_Xzcf_Write"].ToString2() == "0")
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "该用户不允许保存行政处罚数据！";
                    return result.ResultMessage;
                }
                */
                if (dt_Data != null && dt_Data.Rows.Count > 0)
                {
                    foreach (DataRow row in dt_Data.Rows)
                    {
                        result = BLLGIS.saveProjectDocInfo(row);
                    }
                }
            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = ex.Message;
            }

            return result.ResultMessage;

        }

        /// <summary>
        /// 保存项目档案信息（临时）接口，以后会关闭掉
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        [WebMethod]
        public string SaveProjectDocInfoAdd(string user, string password, string xmlData)
        {
            ProcessResultData result = new ProcessResultData();
            try
            {
                DataExchangeBLL BLL = new DataExchangeBLL();
                DataExchangeBLLForGIS BLLGIS = new DataExchangeBLLForGIS();

                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    return result.ResultMessage;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    return result.ResultMessage;
                }

                if (string.IsNullOrEmpty(xmlData))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "传入的XML格式数据为空！";
                    return result.ResultMessage;
                }
                string message;
                DataTable dt_Data = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(xmlData, out message);

                if (dt_Data == null)
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = message;
                    return result.ResultMessage;
                }
                string xml = xmlHelper.ConvertDataTableToXML(dt_Data, "dataTable", "row");
                WebCommon.WriteLog("\r\n传入项目档案数据（临时）：" + ",DateTime:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\ndata:" + xml + "\r\n");

                /*
                if (dt_user.Rows[0]["Has_Xzcf_Write"].ToString2() == "0")
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "该用户不允许保存行政处罚数据！";
                    return result.ResultMessage;
                }
                */
                if (dt_Data != null && dt_Data.Rows.Count > 0)
                {
                    BLLGIS.saveProjectDocInfoAdd(dt_Data);
                }
            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = ex.Message;
            }

            return result.ResultMessage;

        }

        /// <summary>
        /// 保存单体项目档案信息接口
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        [WebMethod]
        public string SaveSubProjectDocInfo(string user, string password, string xmlData)
        {
            ProcessResultData result = new ProcessResultData();
            try
            {
                DataExchangeBLL BLL = new DataExchangeBLL();
                DataExchangeBLLForGIS BLLGIS = new DataExchangeBLLForGIS();

                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    return result.ResultMessage;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    return result.ResultMessage;
                }

                if (string.IsNullOrEmpty(xmlData))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "传入的XML格式数据为空！";
                    return result.ResultMessage;
                }
                string message;
                DataTable dt_Data = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(xmlData, out message);

                if (dt_Data == null)
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = message;
                    return result.ResultMessage;
                }
                string xml = xmlHelper.ConvertDataTableToXML(dt_Data, "dataTable", "row");
                WebCommon.WriteLog("\r\n传入单体项目档案数据：" + ",DateTime:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\ndata:" + xml + "\r\n");

                /*
                if (dt_user.Rows[0]["Has_Xzcf_Write"].ToString2() == "0")
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "该用户不允许保存行政处罚数据！";
                    return result.ResultMessage;
                }
                */
                if (dt_Data != null && dt_Data.Rows.Count > 0)
                {
                    foreach (DataRow row in dt_Data.Rows)
                    {
                        result = BLLGIS.saveSubProjectDocInfo(row);
                    }
                }
            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = ex.Message;
            }

            return result.ResultMessage;

        }

        /// <summary>
        /// 保存项目位置信息接口
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        [WebMethod]
        public string SaveProjectPositionInfo(string user, string password, string xmlData)
        {
            ProcessResultData result = new ProcessResultData();
            try
            {
                DataExchangeBLL BLL = new DataExchangeBLL();
                DataExchangeBLLForGIS BLLGIS = new DataExchangeBLLForGIS();

                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    return result.ResultMessage;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    return result.ResultMessage;
                }

                if (string.IsNullOrEmpty(xmlData))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "传入的XML格式数据为空！";
                    return result.ResultMessage;
                }
                string message;
                DataTable dt_Data = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(xmlData, out message);

                if (dt_Data == null)
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = message;
                    return result.ResultMessage;
                }
                string xml = xmlHelper.ConvertDataTableToXML(dt_Data, "dataTable", "row");
                WebCommon.WriteLog("\r\n传入项目档案数据：" + ",DateTime:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\ndata:" + xml + "\r\n");

                /*
                if (dt_user.Rows[0]["Has_Xzcf_Write"].ToString2() == "0")
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "该用户不允许保存行政处罚数据！";
                    return result.ResultMessage;
                }
                */
                if (dt_Data != null && dt_Data.Rows.Count > 0)
                {
                    foreach (DataRow row in dt_Data.Rows)
                    {
                        result = BLLGIS.saveProjectPositionInfo(row);
                    }
                }
            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = ex.Message;
            }

            return result.ResultMessage;

        }

        #endregion


        #region 为政务服务网提供接口
        [WebMethod]
        public string SaveJsdwxx(string user, string password, string xmlData)
        {
            ProcessResultData result = new ProcessResultData();
            try
            {
                DataExchangeBLL BLL = new DataExchangeBLL();
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    return result.ResultXmlMessage;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    return result.ResultXmlMessage;
                }

                if (string.IsNullOrEmpty(xmlData))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "传入的XML格式数据为空！";
                    return result.ResultXmlMessage;
                }
                string message;
                DataTable dt_Data = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(xmlData, out message);

                if (dt_Data == null)
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = message;
                    return result.ResultXmlMessage;
                }
                string xml = xmlHelper.ConvertDataTableToXML(dt_Data, "dataTable", "row");
                WebCommon.WriteLog("\r\nSaveJsdwxx传入数据：" + ",DateTime:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\ndata:" + xml + "\r\n");
                if (dt_user.Rows[0]["Has_platform_write"].ToString2() == "0")
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "该用户不允许保存SaveJsdwxx表数据！";
                    return result.ResultXmlMessage;
                 }
                result = BLL.SaveJsdw(user, dt_Data);
                
            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = ex.Message;
            }

            return result.ResultXmlMessage;

        }
        [WebMethod]
        public string SaveQyjbxx(string user, string password, string operate, string xmlData)
        {
            ProcessResultData result = new ProcessResultData();
            try
            {
                DataExchangeBLL BLL = new DataExchangeBLL();
                
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    //return result.ResultXmlMessage;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    result.code = ProcessResult.用户名或密码错误;
                    result.message = "用户名或密码错误！";
                    //return result.ResultXmlMessage;
                }

                if (string.IsNullOrEmpty(xmlData))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "传入的XML格式数据为空！";
                    //return result.ResultXmlMessage;
                }
                string message;
                DataTable dt_Data = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(xmlData, out message);

                if (dt_Data == null)
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = message;
                    return result.ResultXmlMessage;
                }
                string xml = xmlHelper.ConvertDataTableToXML(dt_Data, "dataTable", "row");
                WebCommon.WriteLog("\r\nSaveJsdwxx传入数据：" + ",DateTime:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\ndata:" + xml + "\r\n");
                if (dt_user.Rows[0]["Has_platform_write"].ToString2() == "0")
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "该用户不允许保存SaveJsdwxx表数据！";
                    return result.ResultXmlMessage;
                }

                result = BLL.SaveQyjbxx(user,operate,dt_Data);

            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = ex.Message;
            }

            return result.ResultXmlMessage;

        }
        

        #endregion

        #region 各区读取数据接口
        /// <summary>
        /// 功能： 从无锡数据中心读取数据
        /// 作者：孙刚
        /// 时间：2015-03-31
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="user">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="password"></param>
        /// <returns>返回查询到的XML格式数据的字符串表示</returns>
        [WebMethod]
        public string ReadTBDataForCounty(string tableName, string user, string password, string beginDate, string endDate)
        {
            string xmlData = String.Empty;
       
            DataExchangeBLLForCounty BLL = new DataExchangeBLLForCounty();
            DataExchangeBLLForYZSSB SBBLL = new DataExchangeBLLForYZSSB();

            string apiMessage = string.Empty; 
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    return xmlData;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    return xmlData;
                }

                List<IDataItem> list = new List<IDataItem>();
                IDataItem item;

                List<IDataItem> ajsbbList = new List<IDataItem>();
                IDataItem ajsbbItem;

                if (string.IsNullOrEmpty(tableName))
                {
                    return xmlData;
                }
                DataTable dt;

                if (!string.IsNullOrEmpty(beginDate))
                {
                    DateTime date;
                    if (DateTime.TryParse(beginDate, out date))
                    {
                        item = new DataItem();
                        item.ItemName = "CREATEDATE";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.GreaterThanOrEqual;
                        item.ItemType = DataType.String;
                        item.ItemData = date.ToString("yyyy-MM-dd");
                        list.Add(item);
                    }
                }

                if (!string.IsNullOrEmpty(endDate))
                {
                    DateTime date;
                    if (DateTime.TryParse(endDate, out date))
                    {
                        item = new DataItem();
                        item.ItemName = "CREATEDATE";
                        item.ItemData = date.ToString("yyyy-MM-dd");
                        item.ItemType = DataType.String;
                        item.ItemRelation = Bigdesk8.Data.DataRelation.LessThanOrEqual;
                        list.Add(item);
                    }
                }
                //从接口用户中提取区划编码
                string countyNum = null;
                if (user.Length > 6)
                {
                    countyNum = user.Substring(0,6);
                }
                else
                {
                    countyNum = user;
                }

                StringBuilder str = new StringBuilder();
                string mainXml = string.Empty;
                switch (tableName.ToLower())
                {
                    case "tbprojectinfo"://TBProjectInfo

                        if (dt_user.Rows[0]["Has_TBProjectInfo"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "CountyNum";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = user;
                            list.Add(item);
                        }

                        dt = BLL.GetTBData_TBProjectInfo(list);

                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;

                    case "xm_gcdjb_dtxm":

                        if (dt_user.Rows[0]["Has_xm_gcdjb_dtxm"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "sbdqbm";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = user;
                            list.Add(item);
                        }


                        dt = BLL.GetTBData_xm_gcdjb_dtxm(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;

                    case "tbtenderinfo"://TBTenderInfo

                        if (dt_user.Rows[0]["Has_TBTenderInfo"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "CountyNum";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = countyNum;
                            list.Add(item);
                        }


                        dt = BLL.GetTBData_TBTenderInfo(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    case "tbcontractrecordmanage": //TBContractRecordManage

                        if (dt_user.Rows[0]["Has_TBContractRecordManage"].ToString2() == "0")
                        {
                            return xmlData;
                        }


                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "CountyNum";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = countyNum;
                            list.Add(item);
                        }
                        dt = BLL.GetTBData_TBContractRecordManage(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    case "tbprojectcensorinfo"://TBProjectCensorInfo

                        if (dt_user.Rows[0]["Has_TBProjectCensorInfo"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "CountyNum";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = countyNum;
                            list.Add(item);
                        }

                        dt = BLL.GetTBData_TBProjectCensorInfo(list);
                        //xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        
                        str.AppendLine("<?xml version=\"1.0\" encoding=\"gb2312\"?>");

                        DataTable tempDt;
                        

                        foreach (DataRow dataRow in dt.Rows)
                        {
                            str.AppendFormat("<{0}>", "data");
                             
                            str.AppendFormat("<{0}>", "TBProjectCensorInfo");
                            mainXml = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);
                            str.Append(mainXml);
                            str.AppendFormat("</{0}>", "TBProjectCensorInfo");

                            tempDt = BLL.GetTBData_TBProjectDesignEconUserInfo(dataRow["CensorNum"].ToString()); 
                            str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "TBProjectDesignEconUserInfoList", "TBProjectDesignEconUserInfo"));
                            str.AppendFormat("</{0}>", "data");
                        }

                        xmlData = str.ToString();

                        break;
                    case "tbbuilderlicencemanage"://TBBuilderLicenceManage

                        if (dt_user.Rows[0]["Has_TBBuilderLicenceManage"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "CountyNum";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = countyNum;
                            list.Add(item);
                        }

                        dt = BLL.GetTBData_TBBuilderLicenceManage(list);
                        //xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");

                        str.AppendFormat("<{0}>", "result");
                        foreach (DataRow dataRow in dt.Rows)
                        {
                            str.AppendFormat("<{0}>", "data");

                            str.AppendFormat("<{0}>", "TBBuilderLicenceManage");
                            mainXml = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);
                            str.Append(mainXml);
                            str.AppendFormat("</{0}>", "TBBuilderLicenceManage");

                            tempDt = BLL.GetTBData_TBBuilderLicenceManageCanJianDanW(dataRow["BuilderLicenceNum"].ToString());
                            str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "TBBuilderLicenceManageCanJianDanWList", "TBBuilderLicenceManageCanJianDanW"));
                            str.AppendFormat("</{0}>", "data");
                        }
                        str.AppendFormat("</{0}>", "result");

                        xmlData = str.ToString();
                        break;
                    case "tbprojectbuilderuserinfo"://TBProjectBuilderUserInfo

                        if (dt_user.Rows[0]["Has_aj_gcjbxx"].ToString2() == "0")
                        {
                            return xmlData;
                        }
 
                        dt = BLL.GetTBData_TBProjectBuilderUserInfoForCounty(user);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    case "tbprojectfinishmanage"://TBProjectFinishManage

                        if (dt_user.Rows[0]["Has_TBProjectFinishManage"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "CountyNum";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = countyNum;
                            list.Add(item);
                        }

                        dt = BLL.GetTBData_TBProjectFinishManage(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    case "aj_gcjbxx":

                        if (dt_user.Rows[0]["Has_aj_gcjbxx"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "CountyNum";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = countyNum;
                            list.Add(item);
                        }

                        dt = BLL.GetTBData_aj_gcjbxx(list);
                        //xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                         str.AppendFormat("<{0}>", "result");
                        foreach (DataRow dataRow in dt.Rows)
                        {
                            str.AppendFormat("<{0}>", "data");

                            str.AppendFormat("<{0}>", "aj_gcjbxx");
                            mainXml = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);
                            str.Append(mainXml);
                            str.AppendFormat("</{0}>", "aj_gcjbxx");

                            tempDt = BLL.GetTBProjectBuilderUserInfo(dataRow["aqjdbm"].ToString());
                            str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "BuilderUserInfoList", "BuilderUserInfo"));
                            str.AppendFormat("</{0}>", "data");
                        }
                        str.AppendFormat("</{0}>", "result");
                        xmlData = str.ToString();
                        break;
                    case "zj_gcjbxx":
                        if (dt_user.Rows[0]["Has_zj_gcjbxx"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            item = new DataItem();
                            item.ItemName = "CountyNum";
                            item.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            item.ItemType = DataType.String;
                            item.ItemData = countyNum;
                            list.Add(item);
                        }

                        dt = BLL.GetTBData_zj_gcjbxx(list);
                        //xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        str.AppendFormat("<{0}>", "result");
                        foreach (DataRow dataRow in dt.Rows)
                        {
                            str.AppendFormat("<{0}>", "data");

                            str.AppendFormat("<{0}>", "zj_gcjbxx");
                            mainXml = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);
                            str.Append(mainXml);
                            str.AppendFormat("</{0}>", "zj_gcjbxx");

                            tempDt = BLL.GetTBData_zj_gcjbxx_zrdw(dataRow["zljdbm"].ToString());
                            str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "zj_gcjbxx_zrdwList", "zj_gcjbxx_zrdw"));
                            str.AppendFormat("</{0}>", "data");
                        }
                        str.AppendFormat("</{0}>", "result");
                        xmlData = str.ToString();
                        break;
                    case "zj_gcjbxx_zrdw":

                        if (dt_user.Rows[0]["Has_zj_gcjbxx"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        dt = BLL.GetTBData_zj_gcjbxx_zrdw(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    case "aj_zj_sgxk_relation":
                        if (dt_user.Rows[0]["Has_aj_zj_sgxk_relation"].ToString2() == "0")
                        {
                            return xmlData;
                        }
                        dt = BLL.GetTBData_zj_gcjbxx_zrdw(list);
                        xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                        break;
                    case "ap_ajsbb"://ap_ajsbb

                        if (dt_user.Rows[0]["Has_aj_gcjbxx"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (!string.IsNullOrEmpty(beginDate))
                        {
                            DateTime date;
                            if (DateTime.TryParse(beginDate, out date))
                            {
                                ajsbbItem = new DataItem();
                                ajsbbItem.ItemName = "updateDate";
                                ajsbbItem.ItemRelation = Bigdesk8.Data.DataRelation.GreaterThanOrEqual;
                                ajsbbItem.ItemType = DataType.String;
                                ajsbbItem.ItemData = date.ToString("yyyy-MM-dd");
                                ajsbbList.Add(ajsbbItem);
                            }
                        }

                        if (!string.IsNullOrEmpty(endDate))
                        {
                            DateTime date;
                            if (DateTime.TryParse(endDate, out date))
                            {
                                ajsbbItem = new DataItem();
                                ajsbbItem.ItemName = "updateDate";
                                ajsbbItem.ItemData = date.ToString("yyyy-MM-dd");
                                ajsbbItem.ItemType = DataType.String;
                                ajsbbItem.ItemRelation = Bigdesk8.Data.DataRelation.LessThanOrEqual;
                                ajsbbList.Add(ajsbbItem);
                            }
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            ajsbbItem = new DataItem();
                            ajsbbItem.ItemName = "CountyNum";
                            ajsbbItem.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            ajsbbItem.ItemType = DataType.String;
                            ajsbbItem.ItemData = countyNum;
                            ajsbbList.Add(ajsbbItem);
                        }

                        dt = BLL.GetTBData_ap_ajsbb(ajsbbList);
                        //xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");

                        str.AppendFormat("<{0}>", "result");
                        foreach (DataRow dataRow in dt.Rows)
                        {
                            str.AppendFormat("<{0}>", "data");

                            str.AppendFormat("<{0}>", "ap_ajsbb");
                            mainXml = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);
                            str.Append(mainXml);
                            str.AppendFormat("</{0}>", "ap_ajsbb");

                            tempDt = BLL.GetTBData_ap_ajsbb_jg(dataRow["uuid"].ToString());
                            str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "ap_ajsbb_approvelist", "ap_ajsbb_approve"));

                            tempDt = SBBLL.GetAp_ajsbb_ht(dataRow["uuid"].ToString());
                            str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "htList", "htcontent"));

                            tempDt = SBBLL.GetAp_ajsbb_dwry(dataRow["uuid"].ToString());
                            str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "dwryList", "dwrycontent"));

                            tempDt = SBBLL.GetAp_ajsbb_clqd(dataRow["uuid"].ToString());
                            str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "clList", "clcontent"));

                            tempDt = SBBLL.GetAp_ajsbb_hjssjd(dataRow["uuid"].ToString());
                            str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "hjssjdList", "hjssjdcontent"));

                            tempDt = SBBLL.GetAp_ajsbb_wxyjdgcqd(dataRow["uuid"].ToString());
                            str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "wxygcList", "wxygccontent"));

                            tempDt = SBBLL.GetAp_ajsbb_cgmgcqd(dataRow["uuid"].ToString());
                            str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "cgmgcList", "cgmgccontent"));

                            str.AppendFormat("</{0}>", "data");
                        }
                        str.AppendFormat("</{0}>", "result");

                        xmlData = str.ToString();
                        break;
                    case "ap_zjsbb"://ap_zjsbb

                        if (dt_user.Rows[0]["Has_zj_gcjbxx"].ToString2() == "0")
                        {
                            return xmlData;
                        }

                        if (!string.IsNullOrEmpty(beginDate))
                        {
                            DateTime date;
                            if (DateTime.TryParse(beginDate, out date))
                            {
                                ajsbbItem = new DataItem();
                                ajsbbItem.ItemName = "updateDate";
                                ajsbbItem.ItemRelation = Bigdesk8.Data.DataRelation.GreaterThanOrEqual;
                                ajsbbItem.ItemType = DataType.String;
                                ajsbbItem.ItemData = date.ToString("yyyy-MM-dd");
                                ajsbbList.Add(ajsbbItem);
                            }
                        }

                        if (!string.IsNullOrEmpty(endDate))
                        {
                            DateTime date;
                            if (DateTime.TryParse(endDate, out date))
                            {
                                ajsbbItem = new DataItem();
                                ajsbbItem.ItemName = "updateDate";
                                ajsbbItem.ItemData = date.ToString("yyyy-MM-dd");
                                ajsbbItem.ItemType = DataType.String;
                                ajsbbItem.ItemRelation = Bigdesk8.Data.DataRelation.LessThanOrEqual;
                                ajsbbList.Add(ajsbbItem);
                            }
                        }

                        if (dt_user.Rows[0]["Flag"].ToString2() == "1")
                        {
                            ajsbbItem = new DataItem();
                            ajsbbItem.ItemName = "CountyNum";
                            ajsbbItem.ItemRelation = Bigdesk8.Data.DataRelation.Equal;
                            ajsbbItem.ItemType = DataType.String;
                            ajsbbItem.ItemData = countyNum;
                            ajsbbList.Add(ajsbbItem);
                        }

                        dt = BLL.GetTBData_ap_zjsbb(ajsbbList);
                        //xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");

                        str.AppendFormat("<{0}>", "result");
                        foreach (DataRow dataRow in dt.Rows)
                        {
                            str.AppendFormat("<{0}>", "data");

                            str.AppendFormat("<{0}>", "ap_zjsbb");
                            mainXml = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);
                            str.Append(mainXml);
                            str.AppendFormat("</{0}>", "ap_zjsbb");

                            tempDt = SBBLL.GetAp_zjsbb_ht(dataRow["uuid"].ToString());
                            str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "htList", "htcontent"));

                            tempDt = SBBLL.GetAp_zjsbb_dwry(dataRow["uuid"].ToString());
                            str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "dwryList", "dwrycontent"));

                            tempDt = SBBLL.GetAp_zjsbb_schgs(dataRow["uuid"].ToString());
                            str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "sgtscList", "sgtsccontent"));

                            tempDt = SBBLL.GetAp_zjsbb_dwgc(dataRow["uuid"].ToString());
                            str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "dwgcList", "dwgccontent"));

                            tempDt = SBBLL.GetAp_zjsbb_clqd(dataRow["uuid"].ToString());
                            str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "clList", "clcontent"));

                            str.AppendFormat("</{0}>", "data");
                        }
                        str.AppendFormat("</{0}>", "result");

                        xmlData = str.ToString();
                        break;
                    default:
                        break;
                }


                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = "29";
                row_apicb["apiMethod"] = "ReadTBDataFromZx";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt("29", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                DataTable dt = BLL.GetAPIUnable();
                xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
            }

            return xmlData;

        }

        [WebMethod]
        public string ReadJsdwxxForCounty(string user, string password, string beginDate, string endDate)
        {
             string xmlData = String.Empty;
       
            DataExchangeBLLForCounty BLL = new DataExchangeBLLForCounty(); 

            string apiMessage = string.Empty; 
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    return xmlData;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    return xmlData;
                }

                //从接口用户中提取区划编码
                string countyNum = null;
                if (user.Length > 6)
                {
                    countyNum = user.Substring(0, 6);
                }
                else
                {
                    countyNum = user;
                }

                DataTable dt = BLL.Get_uepp_jsdw_bycounty(countyNum, beginDate, endDate);

                xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");

            }

            return xmlData;
        }

        [WebMethod]
        public string ReadSgdwxxForCounty(string user, string password, string beginDate, string endDate,string onlyJbxx)
        {
            return ReadQyxxForCounty("sgdw", user, password, beginDate, endDate, onlyJbxx);
        }

        [WebMethod]
        public string ReadSjdwxxForCounty(string user, string password, string beginDate, string endDate, string onlyJbxx)
        {
            return ReadQyxxForCounty("sjdw", user, password, beginDate, endDate, onlyJbxx);
        }

        [WebMethod]
        public string ReadZjjgxxForCounty(string user, string password, string beginDate, string endDate, string onlyJbxx)
        {
            return ReadQyxxForCounty("zjjg", user, password, beginDate, endDate, onlyJbxx);
        }

        [WebMethod]
        public string ReadKcdwxxForCounty(string user, string password, string beginDate, string endDate, string onlyJbxx)
        {
            return ReadQyxxForCounty("kcdw", user, password, beginDate, endDate, onlyJbxx);
            /**
            string xmlData = String.Empty;

            DataExchangeBLLForCounty BLL = new DataExchangeBLLForCounty();

            string apiMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            StringBuilder str = new StringBuilder();
            

            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    return xmlData;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    return xmlData;
                }

                //从接口用户中提取区划编码
                string countyNum = null;
                if (user.Length > 6)
                {
                    countyNum = user.Substring(0, 6);
                }
                else
                {
                    countyNum = user;
                }
                List<IDataItem> list = new List<IDataItem>();
                IDataItem item;
                if (!string.IsNullOrEmpty(beginDate))
                {
                    DateTime date;
                    if (DateTime.TryParse(beginDate, out date))
                    {
                        item = new DataItem();
                        item.ItemName = "CreateDate";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.GreaterThanOrEqual;
                        item.ItemType = DataType.String;
                        item.ItemData = date.ToString("yyyy-MM-dd");
                        list.Add(item);
                    }
                }

                if (!string.IsNullOrEmpty(endDate))
                {
                    DateTime date;
                    if (DateTime.TryParse(endDate, out date))
                    {
                        item = new DataItem();
                        item.ItemName = "CreateDate";
                        item.ItemData = date.ToString("yyyy-MM-dd");
                        item.ItemType = DataType.String;
                        item.ItemRelation = Bigdesk8.Data.DataRelation.LessThanOrEqual;
                        list.Add(item);
                    }
                }

                DataTable dt = BLL.Get_uepp_kcdw_bycounty(countyNum, list);
                DataTable tempDt;
                string mainXml = string.Empty;

                //xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                str.AppendFormat("<{0}>", "result");
                foreach (DataRow dataRow in dt.Rows)
                {
                    str.AppendFormat("<{0}>", "data");

                    str.AppendFormat("<{0}>", "Qyjbxx");
                    mainXml = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);
                    str.Append(mainXml);
                    str.AppendFormat("</{0}>", "Qyjbxx");

                    //获取企业资质信息
                    tempDt = BLL.GetCorpCert(dataRow["qyID"].ToString());
                    str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "QyzsList", "Qyzs"));

                    //获取企业人员信息
                    tempDt = BLL.GetCorpStaff(dataRow["qyID"].ToString());
                    str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "QyryList", "Qyry"));

                    str.AppendFormat("</{0}>", "data");
                }
                str.AppendFormat("</{0}>", "result");

            }

            return str.ToString();
            */
        }

        public string ReadQyxxForCounty(string qylx, string user, string password, string beginDate, string endDate, string onlyJbxx)
        {
            string xmlData = String.Empty;

            DataExchangeBLLForCounty BLL = new DataExchangeBLLForCounty();

            string apiMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            StringBuilder str = new StringBuilder();


            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    return xmlData;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    return xmlData;
                }

                //从接口用户中提取区划编码
                string countyNum = null;
                if (user.Length > 6)
                {
                    countyNum = user.Substring(0, 6);
                }
                else
                {
                    countyNum = user;
                }
                List<IDataItem> list = new List<IDataItem>();
                IDataItem item;
                if (!string.IsNullOrEmpty(beginDate))
                {
                    DateTime date;
                    if (DateTime.TryParse(beginDate, out date))
                    {
                        item = new DataItem();
                        item.ItemName = "CreateDate";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.GreaterThanOrEqual;
                        item.ItemType = DataType.String;
                        item.ItemData = date.ToString("yyyy-MM-dd");
                        list.Add(item);
                    }
                }

                if (!string.IsNullOrEmpty(endDate))
                {
                    DateTime date;
                    if (DateTime.TryParse(endDate, out date))
                    {
                        item = new DataItem();
                        item.ItemName = "CreateDate";
                        item.ItemData = date.ToString("yyyy-MM-dd");
                        item.ItemType = DataType.String;
                        item.ItemRelation = Bigdesk8.Data.DataRelation.LessThanOrEqual;
                        list.Add(item);
                    }
                }

                DataTable dt = null;

                switch (qylx)
                {
                    case "sgdw":
                        dt = BLL.Get_uepp_sgdw_bycounty(countyNum, list);
                        break;
                    case "kcdw":
                        dt = BLL.Get_uepp_kcdw_bycounty(countyNum, list);
                        break;
                    case "sjdw":
                        dt = BLL.Get_uepp_sjdw_bycounty(countyNum, list);
                        break;
                    case "zjjg":
                        dt = BLL.Get_uepp_zjjg_bycounty(countyNum, list);
                        break;
                    case "qtdw":
                        break;
                }
                
                DataTable tempDt;
                string mainXml = string.Empty;

                //xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                str.AppendFormat("<{0}>", "result");
                foreach (DataRow dataRow in dt.Rows)
                {
                    str.AppendFormat("<{0}>", "data");

                    str.AppendFormat("<{0}>", "Qyjbxx");
                    mainXml = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);
                    str.Append(mainXml);
                    str.AppendFormat("</{0}>", "Qyjbxx");

                    //获取企业从事业务类型
                    tempDt = BLL.GetQycsywlx(dataRow["qyID"].ToString());
                    str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "QycsywlxList", "Qycsywlx"));

                    if (string.IsNullOrEmpty(onlyJbxx))
                    {
                        //获取企业资质信息
                        tempDt = BLL.GetCorpCert(dataRow["qyID"].ToString());
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "QyzsList", "Qyzs"));

                        //获取企业人员信息
                        tempDt = BLL.GetCorpStaff(dataRow["qyID"].ToString());
                        str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "QyryList", "Qyry"));
                    }

                    str.AppendFormat("</{0}>", "data");
                }
                str.AppendFormat("</{0}>", "result");

            }

            return str.ToString();
        }

        [WebMethod]
        public string ReadRyxxForCounty(string tableName, string user, string password, string beginDate, string endDate)
        {
            string xmlData = String.Empty;

            DataExchangeBLLForCounty BLL = new DataExchangeBLLForCounty();

            string apiMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            StringBuilder str = new StringBuilder();


            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    return xmlData;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    return xmlData;
                }

                //从接口用户中提取区划编码
                string countyNum = null;
                if (user.Length > 6)
                {
                    countyNum = user.Substring(0, 6);
                }
                else
                {
                    countyNum = user;
                }
                List<IDataItem> list = new List<IDataItem>();
                IDataItem item;
                if (!string.IsNullOrEmpty(beginDate))
                {
                    DateTime date;
                    if (DateTime.TryParse(beginDate, out date))
                    {
                        item = new DataItem();
                        item.ItemName = "xgrqsj";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.GreaterThanOrEqual;
                        item.ItemType = DataType.String;
                        item.ItemData = date.ToString("yyyy-MM-dd");
                        list.Add(item);
                    }
                }

                if (!string.IsNullOrEmpty(endDate))
                {
                    DateTime date;
                    if (DateTime.TryParse(endDate, out date))
                    {
                        item = new DataItem();
                        item.ItemName = "xgrqsj";
                        item.ItemData = date.ToString("yyyy-MM-dd");
                        item.ItemType = DataType.String;
                        item.ItemRelation = Bigdesk8.Data.DataRelation.LessThanOrEqual;
                        list.Add(item);
                    }
                }

                DataTable dt = null;

                switch (tableName)
                {
                    case "UEPP_Ryjbxx":
                        dt = BLL.Get_uepp_ryjbxx_bycounty(countyNum, list);
                        if (!string.IsNullOrEmpty(beginDate) && !string.IsNullOrEmpty(endDate))
                        {
                            DataRow[] filterDataRows = dt.Select("xgrqsj>=#" + beginDate + "# AND xgrqsj <#" + endDate + "#");

                            str.AppendFormat("<{0}>", "dataTable");
                            foreach (DataRow dataRow in filterDataRows)
                            {
                                str.AppendFormat("<{0}>", "row");
                                str.Append(xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow));
                                str.AppendFormat("</{0}>", "row");
                            }
                            str.AppendFormat("</{0}>", "dataTable");
                            xmlData = str.ToString2();
                        }  
                        break;
                    case "UEPP_QyRy":
                        dt = BLL.Get_uepp_qyry_bycounty(countyNum, list);
                        break;
                    case "UEPP_Ryzs":
                        dt = BLL.Get_uepp_ryzs_bycounty(countyNum, list);
                        break;
                    case "UEPP_Ryzymx":
                        dt = BLL.Get_uepp_ryzymx_bycounty(countyNum, list);
                        break;
                    
                }

                //DataTable tempDt;
                //string mainXml = string.Empty;
                if (string.IsNullOrEmpty(xmlData))
                {
                    xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                }
            }

            return xmlData;
        }

        [WebMethod]
        public string ReadQyxxForCounty(string tableName, string user, string password, string beginDate, string endDate)
        {
            string xmlData = String.Empty;

            DataExchangeBLLForCounty BLL = new DataExchangeBLLForCounty();

            string apiMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            StringBuilder str = new StringBuilder();


            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    return xmlData;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    return xmlData;
                }

                //从接口用户中提取区划编码
                string countyNum = null;
                if (user.Length > 6)
                {
                    countyNum = user.Substring(0, 6);
                }
                else
                {
                    countyNum = user;
                }
                List<IDataItem> list = new List<IDataItem>();
                IDataItem item;
                if (!string.IsNullOrEmpty(beginDate))
                {
                    DateTime date;
                    if (DateTime.TryParse(beginDate, out date))
                    {
                        item = new DataItem();
                        item.ItemName = "xgrqsj";
                        item.ItemRelation = Bigdesk8.Data.DataRelation.GreaterThanOrEqual;
                        item.ItemType = DataType.String;
                        item.ItemData = date.ToString("yyyy-MM-dd");
                        list.Add(item);
                    }
                }

                if (!string.IsNullOrEmpty(endDate))
                {
                    DateTime date;
                    if (DateTime.TryParse(endDate, out date))
                    {
                        item = new DataItem();
                        item.ItemName = "xgrqsj";
                        item.ItemData = date.ToString("yyyy-MM-dd");
                        item.ItemType = DataType.String;
                        item.ItemRelation = Bigdesk8.Data.DataRelation.LessThanOrEqual;
                        list.Add(item);
                    }
                }

                DataTable dt = null;

                switch (tableName)
                {
                    case "UEPP_Qyjbxx":
                        dt = BLL.Get_uepp_qyjbxx_bycounty(countyNum, list);
                        break;
                    case "UEPP_Qycsyw":
                        dt = BLL.Get_uepp_qycsyw_bycounty(countyNum, list);
                        break;
                    case "UEPP_Qyzs":
                        dt = BLL.Get_uepp_qyzs_bycounty(countyNum, list);
                        break;
                    case "UEPP_Qyzzmx":
                        dt = BLL.Get_uepp_qyzzmx_bycounty(countyNum, list);
                        break;
                }

                //DataTable tempDt;
                //string mainXml = string.Empty;

                xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");


            }

            return xmlData;
        }


        [WebMethod]
        public string ReadQyxxForCountySingle(string user, string password, string qyID)
        {
            string xmlData = String.Empty;

            DataExchangeBLLForCounty BLL = new DataExchangeBLLForCounty();

            string apiMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            StringBuilder str = new StringBuilder();


            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    return xmlData;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    return xmlData;
                }

                //从接口用户中提取区划编码
                string countyNum = null;
                if (user.Length > 6)
                {
                    countyNum = user.Substring(0, 6);
                }
                else
                {
                    countyNum = user;
                }


                //todo: 检查是否这个区所属或者在这个区项目所涉及的企业

                DataTable dt = BLL.GetQyjbxx(qyID);
                DataTable tempDt;
                string mainXml = string.Empty;

                //xmlData = xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt, "dataTable", "row");
                str.AppendFormat("<{0}>", "result");
                foreach (DataRow dataRow in dt.Rows)
                {
                    str.AppendFormat("<{0}>", "data");

                    str.AppendFormat("<{0}>", "Qyjbxx");
                    mainXml = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);
                    str.Append(mainXml);
                    str.AppendFormat("</{0}>", "Qyjbxx");

                    //获取企业从事业务类型
                    tempDt = BLL.GetQycsywlx(dataRow["qyID"].ToString());
                    str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "QycsywlxList", "Qycsywlx"));

                    //获取企业资质信息
                    tempDt = BLL.GetCorpCert(dataRow["qyID"].ToString());
                    str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "QyzsList", "Qyzs"));

                    //获取企业人员信息
                    tempDt = BLL.GetCorpStaff(dataRow["qyID"].ToString());
                    str.Append(xmlHelper.ConvertDataTableToXMLWithBase64Encoding(tempDt, "QyryList", "Qyry"));

                    str.AppendFormat("</{0}>", "data");
                }
                str.AppendFormat("</{0}>", "result");

            }

            return str.ToString();
        }

        [WebMethod]
        public string ReadJsdwxxForCountySingle(string user, string password, string qyID)
        {
            string xmlData = String.Empty;

            DataExchangeBLLForCounty BLL = new DataExchangeBLLForCounty();

            string apiMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            StringBuilder str = new StringBuilder();


            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
                {
                    return xmlData;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    return xmlData;
                }

                //从接口用户中提取区划编码
                string countyNum = null;
                if (user.Length > 6)
                {
                    countyNum = user.Substring(0, 6);
                }
                else
                {
                    countyNum = user;
                }


                //todo: 检查是否这个区所属或者在这个区项目所涉及的企业
                 
                DataTable dt = BLL.Get_uepp_jsdw_by_qyid(qyID);
 
                string mainXml = string.Empty;
                 
                str.AppendFormat("<{0}>", "result");
                foreach (DataRow dataRow in dt.Rows)
                {
                    str.AppendFormat("<{0}>", "data");

                    str.AppendFormat("<{0}>", "Qyjbxx");
                    mainXml = xmlHelper.ConvertDataRowToXMLWithBase64Encoding(dataRow);
                    str.Append(mainXml);
                    str.AppendFormat("</{0}>", "Qyjbxx");

                    str.AppendFormat("</{0}>", "data");
                }
                str.AppendFormat("</{0}>", "result");

            }

            return str.ToString();
        }


        /// <summary>
        /// 按企业组织机构代码获取建设单位信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="dwfl"></param>
        /// <returns></returns>
        [WebMethod]
        public string Read_Jsdwxx_Single(string user, string password, string corpCode)
        {
            DataExchangeBLLForCounty BLL = new DataExchangeBLLForCounty();
            //SparkSoftDataBody sparkSoftDataBody = new SparkSoftDataBody();
            StringBuilder str = new StringBuilder();
            str.Append("<ResultSet><ReturnInfo>");

            string apiMessage = string.Empty;
            //string methodMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>用户名或密码不正确！</Description>");
                    str.Append("</ReturnInfo></ResultSet>");
                    return str.ToString();
                }
                //if (dt_user.Rows[0]["Has_Jsdw"].ToString2() == "0")
                //{
                //    str.Append("<Status>0</Status>");
                //    str.Append("<Description>该用户没有权限获取建设单位信息！</Description>");
                //    str.Append("</ReturnInfo></SparkSoftDataBody>");
                //    return str.ToString();
                //}

                if (string.IsNullOrEmpty(corpCode))
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>请填写正确格式的企业组织机构代码如：xxxxxxxx-x</Description>");
                    str.Append("</ReturnInfo></ResultSet>");
                    return str.ToString();
                }
                str.Append("<Status>1</Status>");
                str.Append("<Description></Description></ReturnInfo>");

                DataTable dt_jsdw = BLL.Get_uepp_jsdw_by_qyid(corpCode);
                //List<string> excludeColumns = new List<string>(); 
                if (dt_jsdw.Rows.Count > 0)
                {
                    str.Append("<Qyjbxx>");
                    foreach (DataRow row in dt_jsdw.Rows)
                    {
                        str.Append(xmlHelper.ConvertDataRowToXML(row));
                    }
                    str.Append("</Qyjbxx>");
                }
                else
                {
                    str.Append("<Qyjbxx></Qyjbxx>");
                }


                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = "29";
                row_apicb["apiMethod"] = "Read_Jsdwxx";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);
                BLL.UpdateZbJkzt("29", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);
            }
            else
            {
                str.Append("<msg>接口关闭</msg>");
                str.Append("</ReturnInfo>");
                return str.ToString();
            }

            str.Append("</ResultSet>");
            return str.ToString();
        }


        [WebMethod]
        public string Read_Sgdwxx_Single(string user, string password, string corpCode)
        { 
            StringBuilder str = new StringBuilder();
            DataExchangeBLLForCounty BLL = new DataExchangeBLLForCounty();

            str.Append("<ResultSet><ReturnInfo>");

            string apiMessage = string.Empty;// 2016.10.21
            //string methodMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {


                DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
                if (dt_user.Rows.Count == 0)
                {
                    str.Append("<Status>0</Status>");
                    str.Append("<Description>用户名或密码不正确！</Description>");
                    str.Append("</ReturnInfo></ResultSet>");
                    return str.ToString();
                }
                //if (dt_user.Rows[0]["Has_Sgdw"].ToString2() == "0")
                //{
                //    str.Append("<Status>0</Status>");
                //    str.Append("<Description>该用户没有权限获取施工单位信息！</Description>");
                //    str.Append("</ReturnInfo></ResultSet>");
                //    return str.ToString();
                //}

                str.Append("<Status>1</Status>");
                str.Append("<Description></Description></ReturnInfo>");

                DataTable dt_Sgqyxx = BLL.Get_uepp_sgdw_single(corpCode);

                List<string> excludeColumns = new List<string>();
                excludeColumns.Add("UserID");
                excludeColumns.Add("xgr");
                excludeColumns.Add("needUpdateFlag");

                if (dt_Sgqyxx.Rows.Count > 0)
                {
                    str.Append("<QyxxArray>");
                    str.Append("<Qyxx>");
                    foreach (DataRow row in dt_Sgqyxx.Rows)
                    {
                        #region 企业基本信息
                        str.Append("<Qyjbxx>");
                        str.Append(xmlHelper.ConvertDataRowToXML(row, excludeColumns));
                        str.Append("</Qyjbxx>");
                        #endregion

                        #region 企业资质信息
                        DataTable dt_qyzzxx = BLL.Get_uepp_all_qyzz(row["qyID"].ToString2());
                        str.Append("<QyzzArray>");
                        foreach (DataRow row_qyzzmx in dt_qyzzxx.Rows)
                        {
                            str.Append("<Qyzz>");
                            foreach (DataColumn col in dt_qyzzxx.Columns)
                            {
                                str.AppendFormat("<{0}>{1}</{2}>", col.ColumnName, row_qyzzmx[col.ColumnName].ToString2(), col.ColumnName);
                            }
                            str.Append("</Qyzz>");

                        }
                        str.Append("</QyzzArray>");
                        #endregion

                        #region 企业证书信息
                        DataTable dt_qyzs = BLL.Get_uepp_SgQyzsByQyID(row["qyID"].ToString2());
                        str.Append("<QyzsArray>");
                        foreach (DataRow row_qyzsmx in dt_qyzs.Rows)
                        {
                            str.Append("<Qyzs>");
                            foreach (DataColumn col in dt_qyzs.Columns)
                            {
                                str.AppendFormat("<{0}>{1}</{2}>", col.ColumnName, row_qyzsmx[col.ColumnName].ToString2(), col.ColumnName);
                            }
                            str.Append("</Qyzs>");

                        }
                        str.Append("</QyzsArray>");
                        #endregion
                        str.Append("</Qyxx>");
                    }
                    str.Append("</QyxxArray>");
                }
                else
                {
                    str.Append("<QyxxArray></QyxxArray>");
                }

                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = "29";
                row_apicb["apiMethod"] = "Read_Sgdwxx";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt("29", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);

            }
            else
            {
                str.Append("<Description>接口关闭</Description>");
                str.Append("</ReturnInfo>");
                return str.ToString();
            }

            str.Append("</ResultSet>");
            return str.ToString();
        }


        #endregion

        [WebMethod]
        public string DecodeString(string xmlData)
        {
            DataExchangeBLL BLL = new DataExchangeBLL();
            string message = "";
            DataTable dt_Data = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(xmlData, out message);

            string apiMessage = string.Empty;// 2016.10.21
            //string methodMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = "29";
                row_apicb["apiMethod"] = "DecodeString";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt("29", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);
            }
            else
            {
                dt_Data = BLL.GetAPIUnable();
            }


            return xmlHelper.ConvertDataTableToXML(dt_Data, "dataTable", "row");
        }

        [WebMethod]
        public string EncodeString(string xmlData)
        {
            string message = "";
            DataExchangeBLL BLL = new DataExchangeBLL();
            DataTable dt_Data = xmlHelper.ConvertXMLToDataTable(xmlData, out message);

            string apiMessage = string.Empty;// 2016.10.21
            //string methodMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("29");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                DataTable dtapicb = BLL.GetSchema_API_cb();
                DataRow row_apicb = dtapicb.NewRow();
                dtapicb.Rows.Add(row_apicb);
                row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
                row_apicb["apiFlow"] = "29";
                row_apicb["apiMethod"] = "EncodeString";
                row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
                row_apicb["apiDyMessage"] = apiMessage;
                row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BLL.Submit_API_cb(dtapicb);

                BLL.UpdateZbJkzt("29", string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);
            }
            else
            {
                dt_Data = BLL.GetAPIUnable();
            }


            return xmlHelper.ConvertDataTableToXMLWithBase64Encoding(dt_Data, "dataTable", "row");
        }

        #region  公共代码区域
        private static string getCountryCodes(string countryCode, string deptType, DataExchangeBLL BLL)
        {
            string countryCodes = string.Empty;
            if ("320200".Equals(countryCode) || "320201".Equals(countryCode) || string.IsNullOrEmpty(countryCode))
            {
                List<string> countryList = BLL.Get_tbXzqdmDicForShenBao(deptType);
                countryCodes = string.Join(",", countryList.ToArray());
            }
            else
            {
                countryCodes = countryCode;
            }
            return countryCodes;
        }

        private static void createApiLog(string apiFlow, string apiMethod, string apiMessage, DataExchangeBLL BLL)
        {
            DataTable dtapicb = BLL.GetSchema_API_cb();
            DataRow row_apicb = dtapicb.NewRow();
            dtapicb.Rows.Add(row_apicb);
            row_apicb["apiCbID"] = BLL.Get_apiCbNewID();
            row_apicb["apiFlow"] = apiFlow;
            row_apicb["apiMethod"] = apiMethod;
            row_apicb["apiDyResult"] = string.IsNullOrEmpty(apiMessage) == true ? "成功" : "失败";
            row_apicb["apiDyMessage"] = apiMessage;
            row_apicb["apiDyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            BLL.Submit_API_cb(dtapicb);
            BLL.UpdateZbJkzt(apiFlow, string.IsNullOrEmpty(apiMessage) == true ? "1" : "0", apiMessage);
        }

        private static bool isApiOpen(string id, DataExchangeBLL BLL)
        {
            bool flag = false;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow(id);
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                flag = true;
            }
            return flag;
        }

        private static bool accessValidate(string user, string password, DataExchangeBLL BLL)
        {
            bool flag = true;

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
            {
                flag = false;
            }

            DataTable dt_user = BLL.GetInterfaceUserInfo(user, password);
            if (dt_user.Rows.Count == 0)
            {
                flag = false;
            }

            return flag;
        }

        #endregion



        #region 省系统同步代码

        [WebMethod]
        public string synchCorpFromProvincialDC(string user, string apiPassword, string qyid, string type)
        {
            string result = String.Empty;
            DataExchangeBLL BLL = new DataExchangeBLL(); 

            string apiMessage = string.Empty;
            DataTable dtapizb = BLL.Get_API_zb_apiFlow("31");
            if (dtapizb.Rows[0][0].ToString() == "1")
            {
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(apiPassword) || string.IsNullOrEmpty(qyid))
                {
                    result = "请检查传入的参数";
                    return result;
                }

                DataTable dt_user = BLL.GetInterfaceUserInfoForDataCenter(user, apiPassword);
                if (dt_user.Rows.Count == 0)
                {
                    result = "无同步权限";
                    return result;
                }

                DataExchangeBLLForJSCEDC synchBLL = new DataExchangeBLLForJSCEDC();

                try
                {
                    string msg = null;
                    if (type == "2")
                    {
                        msg = synchBLL.PullDataOutCorpCert(qyid);

                    }
                    else if (type == "1")
                    {
                        msg = synchBLL.PullDataCorpCert(qyid);
                    }
                    else
                    {
                        msg = "不支持的类型";
                    }
                   
                    result = msg;

                }
                catch (Exception ex)
                {
                    result = ex.Message;

                } 

            }

            return result;

        }

        #endregion


    }
}





