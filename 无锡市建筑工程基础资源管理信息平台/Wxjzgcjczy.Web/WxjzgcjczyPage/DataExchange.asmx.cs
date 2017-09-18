using System;
using System.Collections.Generic;
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
    [WebService(Namespace = "http://218.90.162.110:8889/WxjzgcjczyPage/")]
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
                string countryCodes = string.Empty;
                if ("320200".Equals(countryCode) || "320201".Equals(countryCode) || string.IsNullOrEmpty(countryCode))
                {
                    List<string> countryList = BLL.Get_tbXzqdmDic();
                    if(!countryList.Contains("320201")){
                        countryList.Add("320201");
                    }
                    countryCodes = string.Join(",", countryList.ToArray());
                }
               

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
                string countryCodes = string.Empty;
                if ("320200".Equals(countryCode) || "320201".Equals(countryCode) || string.IsNullOrEmpty(countryCode))
                {
                    List<string> countryList = BLL.Get_tbXzqdmDic();
                    if (!countryList.Contains("320201"))
                    {
                        countryList.Add("320201");
                    }
                    countryCodes = string.Join(",", countryList.ToArray());
                }


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
                DataTable dt_Data = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(resultXml, out message);
                //DataTable dt_Data = xmlHelper.ConvertXMLToDataTable(resultXml, out message);
                DataTable dwgcDtData = null;
                //单位工程列表
                int dwgcIndex = resultXml.IndexOf("<dwgcList>");
                string dwgcList = string.Empty;
                if (dwgcIndex >= 0)
                {
                    dwgcList = resultXml.Substring(dwgcIndex, resultXml.LastIndexOf("</dwgcList>") - dwgcIndex + "</dwgcList>".Length);
                    dwgcDtData = xmlHelper.ConvertXMLToDataTableWithBase64Decoding(dwgcList, out message);
                    //dwgcDtData = xmlHelper.ConvertXMLToDataTable(dwgcList, out message);
                }

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

                result = YZSSBBLL.pushZJSBJG(user, deptcode, sbPassword, dt_Data, dwgcDtData);

            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = ex.Message;
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


        /// <summary>
        /// 功能：向无锡数据中心传送项目登记补充数据
        /// 作者：huangzhengyu
        /// 时间：2017-08-28
        /// </summary>
        /// <param name="prjnum">16位纯数字编码</param>
        /// <param name="xmlData">XML内容</param>
        /// <param name="user">用户名称</param>
        /// <param name="password">用户密码</param>
        /// <returns></returns>
        /**
        [WebMethod]
        public string getProjectAdd(string prjnum, string xmlData, string user, string password)
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

                //验证Prjnum必须为16位纯数字编码
                if (string.IsNullOrEmpty(prjnum))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "Prjnum为空！";
                    return result.ResultMessage;
                }
                else if (!Validator.IsProjectNum(prjnum))
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "Prjnum必须为16位纯数字编码！";
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
                else if (dt_Data.Rows == null || dt_Data.Rows.Count != 1)
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "一次请传入一条项目登记补充数据";
                    return result.ResultMessage;
                }
                string xml = xmlHelper.ConvertDataTableToXML(dt_Data, "dataTable", "row");
                WebCommon.WriteLog("\r\n传入项目登记补充数据：" + ",DateTime:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\ndata:" + xml + "\r\n");
                //bool isSdSx = dt_user.Rows[0]["Flag"].ToString2() == "1";
                if (dt_user.Rows[0]["Has_TBProjectInfo_Write"].ToString2() == "0")
                {
                    result.code = ProcessResult.保存失败和失败原因;
                    result.message = "该用户不允许保存项目登记补充数据表数据！";
                    return result.ResultMessage;
                }
                result = BLL.SaveTBData_TBProjectAdditionalInfo(user, dt_Data);
            }
            catch (Exception ex)
            {
                result.code = ProcessResult.保存失败和失败原因;
                result.message = ex.Message;
            }

            return result.ResultMessage;

        }
         */


    }
}





