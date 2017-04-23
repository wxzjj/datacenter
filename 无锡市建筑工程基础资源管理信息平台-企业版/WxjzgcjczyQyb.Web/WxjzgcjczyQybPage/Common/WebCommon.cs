using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bigdesk8;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;
using System.Collections.Specialized;
using System.Linq;
using System.Data;
using WxjzgcjczyQyb;
using WxjzgcjczyQyb.BLL;
using Bigdesk8.Data;
using System.Configuration;


namespace WxjzgcjczyQyb.Web
{
    internal static class WebCommon
    {
        #region CodeType
        public static void BindListControl(ListControl listControl, CodeType codeType, bool isKeyValue, bool isAddSpaceItem)
        {
            SundriesBFP bfpSundries = new SundriesBFP();
            DataTable dtCode_CodeInfo = bfpSundries.GetCode_CodeInfo(codeType, isKeyValue).Result;
            BindListControlByDataTable(listControl, dtCode_CodeInfo, isAddSpaceItem);
        }

        public static void BindListControl(ListControl listControl, Dictionary<string, string> dic, bool isAddSpaceItem)
        {
            //缺省按照键-值的方式来取代码
            listControl.ListControlDataBind(dic, isAddSpaceItem);
        }

        /// <summary>
        ///SQL字段名称必须as Code,CodeInfo
        /// </summary>
        public static void BindListControlBySql(ListControl listControl, string SQL, bool isAddSpaceItem)
        {
            SundriesBFP bfpSundries = new SundriesBFP();
            DataTable dtCode_CodeInfo = bfpSundries.GetDTBySql(SQL).Result;
            BindListControlByDataTable(listControl, dtCode_CodeInfo, isAddSpaceItem);
        }

        /// <summary>
        /// 用DataTable做数据源绑定下拉列表, dt必须要有两列Code,CodeInfo! 
        /// 若某listControl中要求选项的key和value一样，则需在dt中保证Code,CodeInfo的内容一样
        /// </summary>
        public static void BindListControlByDataTable(ListControl listControl, DataTable dt, bool isAddSpaceItem)
        {
            if (isAddSpaceItem)
            {
                listControl.Items.Add(new ListItem("", ""));
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                listControl.Items.Add(new ListItem(Convert.ToString(dt.Rows[i]["CodeInfo"]), Convert.ToString(dt.Rows[i]["Code"])));
            }
        }

        #endregion

        #region 结果处理

        /// <summary>
        /// 页面跳转，显示提示信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg">提示信息</param>
        public static void RedirectToMessagePage(this Page page, string msg)
        {
            page.RedirectToMessagePage(msg, "");
        }

        /// <summary>
        /// 页面跳转，显示提示信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg">提示信息</param>
        /// <param name="window">目标窗口名称</param>
        /// 先将msg存入到Session中，再有消息显示页面从Session中读出并显示出来。
        /// 为什么不直接把msg用querystring传给显示页面？
        /// 因为msg中的内容是不可知的，对某些msg内容，如果放到querystring里，浏览器会报"不安全的querystring"
        public static void RedirectToMessagePage(this Page page, string msg, string window)
        {
            msg = msg.Replace(Environment.NewLine, "<br/>");

            if (window.IsEmpty())
            {
                //if (msg.Length > 100)
                //{
                string nam = "UEPPMSG" + Guid.NewGuid().ToString();
                page.Session[nam] = msg;
                page.Response.Redirect(ConfigManager.GetRootUrl() + "Msg.aspx?nam=" + nam, true);
                //}
                //else
                //{
                //    page.Response.Redirect(ConfigManager.GetRootUrl() + "Msg.aspx?msg=" + msg, true);
                //}
            }
            else
            {
                string nam = "UEPPMSG" + Guid.NewGuid().ToString();
                page.Session[nam] = msg;
                page.WindowLocation(ConfigManager.GetRootUrl() + "Msg.aspx?nam=" + nam, window, true);
            }
        }

        /// <summary>
        /// 页面跳转，显示提示信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg">提示信息</param>
        public static void RedirectToMessagePage(this Page page, Exception msg)
        {
            page.RedirectToMessagePage(msg, "");
        }
        /// <summary>
        /// 页面跳转，显示提示信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg">提示信息</param>
        /// <param name="window">目标窗口名称</param>
        public static void RedirectToMessagePage(this Page page, Exception msg, string window)
        {
            page.RedirectToMessagePage(GetExceptionMessage(msg), window);
        }
        /// <summary>
        /// 处理返回的结果,
        /// 成功返回true，程序可以继续运行；失败返回false，程序不能继续运行。
        /// </summary>
        public static bool DealResult(this Page page, FunctionResult fr)
        {
            if (fr == null) return true;

            FunctionResultException a = fr.Message.GetFunctionResultException();
            if (a != null) fr = a.Result;

            switch (fr.Status)
            {
                default:
                case FunctionResultStatus.None:
                    return true;
                case FunctionResultStatus.Info:
                    page.WindowAlert(fr.Message.Message);
                    return true;
                case FunctionResultStatus.Warn:
                    page.WindowAlert(fr.Message.Message);
                    return false;
                case FunctionResultStatus.Error:
                    page.RedirectToMessagePage(fr.Message);
                    return false;
            }
        }
        /// <summary>
        /// 处理返回的结果,
        /// 成功返回true，程序可以继续运行；失败返回false，程序不能继续运行。
        /// </summary>
        public static bool DealResult<T>(this Page page, FunctionResult<T> fr)
        {
            if (fr == null) return true;

            FunctionResultException<T> a = fr.Message.GetFunctionResultException<T>();
            if (a != null) fr = a.Result;

            switch (fr.Status)
            {
                default:
                case FunctionResultStatus.None:
                    return true;
                case FunctionResultStatus.Info:
                    page.WindowAlert(fr.Message.Message);
                    return true;
                case FunctionResultStatus.Warn:
                    page.WindowAlert(fr.Message.Message);
                    return false;
                case FunctionResultStatus.Error:
                    page.RedirectToMessagePage(fr.Message);
                    return false;
            }
        }
        /// <summary>
        /// 处理返回的结果,
        /// 成功返回true，程序可以继续运行；失败直接抛出异常FunctionResultException。
        /// </summary>
        public static bool DealResult(FunctionResult fr)
        {
            if (fr == null)
                throw new ArgumentNullException("fr");

            FunctionResultException a = fr.Message.GetFunctionResultException();
            if (a != null) fr = a.Result;

            switch (fr.Status)
            {
                default:
                case FunctionResultStatus.None:
                    return true;
                case FunctionResultStatus.Info:
                case FunctionResultStatus.Warn:
                case FunctionResultStatus.Error:
                    {
                        throw new FunctionResultException(fr);
                    }
            }
        }
        /// <summary>
        /// 处理返回的结果,
        /// 成功返回true，程序可以继续运行；失败直接抛出异常FunctionResultException。
        /// </summary>
        public static bool DealResult<T>(FunctionResult<T> fr)
        {
            if (fr == null)
                throw new ArgumentNullException("fr");

            FunctionResultException<T> a = fr.Message.GetFunctionResultException<T>();
            if (a != null) fr = a.Result;

            switch (fr.Status)
            {
                default:
                case FunctionResultStatus.None:
                    return true;
                case FunctionResultStatus.Info:
                case FunctionResultStatus.Warn:
                case FunctionResultStatus.Error:
                    {
                        throw new FunctionResultException<T>(fr);
                    }
            }
        }

        private static string GetExceptionMessage(Exception ex)
        {
            if (ex == null) return "";
            string r = ex.Message;
            if (ConfigManager.HaveDebugBeenTurnedOn())
                r += Environment.NewLine + ex.StackTrace;
            return r + Environment.NewLine + GetExceptionMessage(ex.InnerException);
        }

        #endregion 结果处理

        /// <summary>
        /// 格式化字符串，将1转换成√，-1转换成×，0转换成－，其它转换成""
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string FormatString(object obj)
        {
            string s = obj.ToString2();
            switch (s)
            {
                case "1":
                    return "√";
                case "-1":
                    return "×";
                default:
                    return "";
            }
        }

        /// <summary> 
        /// 将DataTable数据导出到EXCEL，调用该方法后自动返回可下载的文件流 
        /// </summary> 
        /// <param name="dtData">要导出的数据源</param> 
        public static void DataTable1Excel(System.Data.DataTable dtData, string name)
        {
            System.Web.UI.WebControls.GridView gvExport = null;
            // 当前对话 
            System.Web.HttpContext curContext = System.Web.HttpContext.Current;
            // IO用于导出并返回excel文件 
            System.IO.StringWriter strWriter = null;
            System.Web.UI.HtmlTextWriter htmlWriter = null;

            if (dtData != null)
            {
                // 设置编码和附件格式 
                curContext.Response.ContentType = "application/vnd.ms-excel";
                curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(name, Encoding.UTF8).ToString());
                curContext.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
                curContext.Response.Charset = "utf-8";

                // 导出excel文件 
                strWriter = new System.IO.StringWriter();
                htmlWriter = new System.Web.UI.HtmlTextWriter(strWriter);
                // 为了解决gvData中可能进行了分页的情况，需要重新定义一个无分页的GridView 
                gvExport = new System.Web.UI.WebControls.GridView();
                gvExport.DataSource = dtData.DefaultView;
                gvExport.AllowPaging = false;

                gvExport.DataBind();

                // 返回客户端 
                gvExport.RenderControl(htmlWriter);
                curContext.Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\" />" + strWriter.ToString());
                curContext.Response.End();
            }
        }

        /// <summary>
        /// 上传PDF文件
        /// </summary>
        /// <param name="fileUpload"></param>
        /// <param name="saveFileUrl"></param>
        /// <returns></returns>
        public static List<string> UploadPDFFile(FileUpload fileUpload, string saveFileUrl)
        {
            List<string> listStr = new List<string>();
            try
            {
                string FileUrl = fileUpload.PostedFile.FileName;
                string LastName = FileUrl.Substring(FileUrl.LastIndexOf(".")).ToLower();
                string NewFileName = DateTime.Now.ToString("yyyy-MM-dd") + "-" + DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0') + DateTime.Now.Millisecond.ToString().PadLeft(3, '0') + LastName;
                string NewFileUrl = saveFileUrl + NewFileName;

                if (LastName == ".pdf")
                {
                    if (fileUpload.PostedFile.ContentLength > 2 * 1024 * 1024)
                    {
                        listStr.Add("FileError");
                        return listStr;
                    }
                }
                else
                {
                    listStr.Add("FormatError");
                    return listStr;
                }

                fileUpload.PostedFile.SaveAs(System.Web.HttpContext.Current.Server.MapPath(NewFileUrl));
                listStr.Add(null);
                listStr.Add(NewFileUrl);
                listStr.Add(NewFileName);
            }
            catch (Exception ex)
            {
                listStr.Add("上传发生错误！原因是：" + ex.ToString());
                return listStr;
            }
            return listStr;
        }

        public static void AddDataItem(List<IDataItem> litm, string itemName, string itemData)
        {
            IDataItem idt = new DataItem();
            idt.ItemName = itemName;
            idt.ItemData = itemData;
            litm.Add(idt);
        }

        public static string ConnectionString_WJSJZX
        {
            get
            {
                return  ConfigManager.GetConnectionString();
            }
        }

        /// <summary>
        /// 获得 WJSJZX 数据库操作者
        /// </summary>
        /// <returns></returns>
        public static DBOperator GetDB_WJSJZX()
        {
            return DBOperatorFactory.GetDBOperator(ConnectionString_WJSJZX, DataBaseType.SQLSERVER2008.ToString());
        }

        /// <summary>
        /// 填充 DropDownList
        /// </summary>
        public static void DropDownListDataBind(DBDropDownList dropdonlist, bool addSpaceItem)
        {
            string sql = string.Empty;
            switch (dropdonlist.ToolTip)
            {
                /*2015-3-31 李贯涛 综合监管招标方式*/
                case "zbfs"://招标方式
                    sql = "select CodeInfo,Code from tbTenderTypeDic ";
                    break;

                case "PrjType":
                    sql = "select CodeInfo,Code from tbPrjTypeDic ";
                    break;
                case "Lxjb":
                    sql = "select CodeInfo,Code from tbLxjbDic ";
                    break;
                case "PrjProperty":
                    sql = "select CodeInfo,Code from tbPrjPropertyDic ";
                    break;
                case "PrjStructureType":
                    sql = "select CodeInfo,Code from tbPrjStructureTypeDic ";
                    break;
                case "TenderClass":
                    sql = "select CodeInfo,Code from tbTenderClassDic ";
                    break;
                case "TenderType":
                    sql = "select CodeInfo,Code from tbTenderTypeDic ";
                    break;
                case "ContractType":
                    sql = "select CodeInfo,Code from tbContractTypeDic order by OrderID ";
                    break;
                case "WorkDuty":
                    sql = "select CodeInfo,Code from tbWorkDutyDic ";
                    break;
                case "IDCardType":
                    sql = "select CodeInfo,Code from tbIDCardTypeDic ";
                    break;
                case "SpecialtyType":
                    sql = "select CodeInfo,Code from tbSpecialtyTypeDic ";
                    break;
                case "Xzqdm":
                    sql = "select CodeInfo,Code from tbXzqdmDic where parentCode='320200' ";
                    break;
                case "ApprovalLevel":
                    sql = "select CodeInfo,Code from tbLxjbDic  ";
                    break;
                case "xmsd":
                    sql = "select CodeInfo,Code from tbXzqdmDic where parentCode='320200' ";
                    break;


            }
            if (sql != string.Empty)
            {
                DataTable dt = WebCommon.GetDB_WJSJZX().ExeSqlForDataTable(sql, null, "t");
                UIUtility.ListControlDataBind(dropdonlist, dt, addSpaceItem);
            }
        }

        /// <summary>
        /// 填充 DropDownList
        /// </summary>
        public static void CheckBoxListDataBind(CheckBoxList checkBoxList)
        {
            string sql = string.Empty;
            switch (checkBoxList.ClientID)
            {
                case "cbl_ssdq":
                    sql = "select CodeInfo,Code from tbXzqdmDic where parentCode='320200' ";
                    break;
                case "cbl_Htlb":
                    sql = "select CodeInfo,Code from tbContractTypeDic where OrderID >=0 order by OrderID  ";
                    break;
            }


            if (sql != string.Empty)
            {
                DataTable dt = WebCommon.GetDB_WJSJZX().ExeSqlForDataTable(sql, null, "t");
                foreach (DataRow row in dt.Rows)
                {
                    checkBoxList.Items.Add(new ListItem(row[0].ToString(), row[1].ToString()));
                }
            }
        }


        #region 身份证号位数校验

        /// <summary>
        /// 由身份证号前位计算第位
        /// </summary>
        /// <param name="IDCard17">身份证号前位</param>
        /// <returns>身份证号第位</returns>
        private static string GetIDCard18LastNo(string IDCard17)
        {
            string result = string.Empty;
            try
            {
                if (IDCard17.Length != 17)
                {
                    return string.Empty;
                }
                // 加权因子 
                int[] W = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };

                // 校验码对应值 
                string LastCode = "10X98765432";

                int checkSum = 0;
                for (int i = 0; i < IDCard17.Length; i++)
                {
                    checkSum += Convert.ToInt32(IDCard17.Substring(i, 1)) * W[i];
                }
                int j = checkSum % 11;
                result = LastCode.Substring(j, 1);
            }
            catch
            {
                result = result + "前位必须为数字";
            }
            return result;
        }

        /// <summary>
        /// 18位身份证校验码有效性检查
        /// </summary>
        /// <param name="IDCard18">18位身份证号</param>
        /// <returns>验证通过[return string.Empty]，验证不通过[return 具体原因]</returns>
        public static string CheckIDCard18(string IDCard18)
        {
            IDCard18 = IDCard18.ToUpper();//防止最后一位是x造成校验不通过
            string result = string.Empty;
            int length = IDCard18.Length;
            string pattern = @"\d{17}[0-9X]";
            string patternDate = @"(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})(((0[13578]|1[02])(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)(0[1-9]|[12][0-9]|30))|(02(0[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))0229)";

            if (length != 18)   // 验证长度为位
            {
                result = "身份证号位数不正确，当前为" + length.ToString() + "位，请采用18位。";
                return result;
            }

            if (Regex.Match(IDCard18, pattern).Value == "") // 验证前位为数字，最后一位为数字或X
            {
                result = "身份证号格式有误，正确的格式是：前17位为数字，最后一位为数字或X";
                return result;
            }

            string IDCardDate = IDCard18.Substring(6, 8);
            if (Regex.Match(IDCardDate, patternDate).Value == "")  // 验证出生日期的有效性
            {
                result = "身份证号格式有误，当前至位出生日期为" + IDCardDate + "，这是一个无效的日期";
                return result;
            }
            //string IDCard17 = IDCard18.Substring(0, 17);
            //string lastNo = GetIDCard18LastNo(IDCard17);
            //if (lastNo != IDCard18.Substring(17, 1))
            //{
            //    result = "身份证号格式有误，最后一位校验码应该为" + lastNo + "。";
            //    return result;
            //}
            return result;
        }

        #endregion

        #region XML操作

        public static DataSet XmlStringToDataSet(string xmlString)
        {
            DataSet dataSet = new DataSet();
            using (System.IO.StringReader stringReader = new System.IO.StringReader(xmlString))
            {
                dataSet.ReadXml(stringReader);
            }
            return dataSet;
        }

        /// <summary>
        /// 将 dataTable1 的数据行追加到 dataTable2 中
        /// 如果数据列名匹配,则赋值到 dataTable2
        /// </summary>
        /// <param name="dataTable1">数据表1</param>
        /// <param name="dataTable2">数据表2</param>
        public static void CopyDataTable1ToDataTable2(DataTable dataTable1, DataTable dataTable2)
        {
            foreach (DataRow dr in dataTable1.Rows)
            {
                DataRow newDataRow = dataTable2.NewRow();
                foreach (DataColumn dc in dataTable1.Columns)
                {
                    if (!dataTable2.Columns.Contains(dc.ColumnName)) continue;
                    if (dr[dc.ColumnName] == null) continue;
                    if (string.IsNullOrEmpty(dr[dc.ColumnName].ToString())) continue;
                    newDataRow[dc.ColumnName] = dr[dc.ColumnName];
                }
                dataTable2.Rows.Add(newDataRow);
            }
        }
        #endregion


        #region 解析参数URL

        public static void ParseUrl(string url, out string baseUrl, out Dictionary<string, string> dic)
        {
            if (url == null)
                throw new ArgumentNullException("url");

            dic = new Dictionary<string, string>();
            baseUrl = "";

            if (url == "")
                return;

            int questionMarkIndex = url.IndexOf('?');

            if (questionMarkIndex == -1)
            {
                baseUrl = url;
                return;
            }
            baseUrl = url.Substring(0, questionMarkIndex);
            if (questionMarkIndex == url.Length - 1)
                return;
            string ps = url.Substring(questionMarkIndex + 1);

            //string[] _ps;
            if (ps.IndexOf("&") != -1)
            {
                var aQuery = ps.Split('&'); ;
                for (var i = 0; i < aQuery.Length; i++)
                {
                    var k = aQuery[i].IndexOf("=");
                    if (k == -1) continue;
                    var key = aQuery[i].Substring(0, k);
                    var value = aQuery[i].Substring(k + 1);

                    if (!dic.ContainsKey(key))
                    {
                        dic.Add(key, value);
                    }
                }
            }
            else
            {
                var k = ps.IndexOf("=");
                if (k != -1)
                {
                    var key = ps.Substring(0, k);
                    var value = ps.Substring(k + 1);
                    dic.Add(key, value);
                }
            }
        }

        #endregion



        #region UserRight
        public static DBOperator GetUserRightDBOperator()
        {
            return DBOperatorFactory.GetDBOperator(ConfigManager.GetConnectionString(), ConfigManager.GetDatabaseType().ToUpper());
        }

        public static Bigdesk8.Business.UserRightManager.IUserRightManager CreateUserRightManager()
        {
            Bigdesk8.Business.UserRightManager.IUserRightManager urm = new Bigdesk8.Business.UserRightManager.UserRightManager();
            urm.DB = GetUserRightDBOperator();
            return urm;
        }

        public static Bigdesk8.Business.UserRightManager.IUserRightInfo CreateUserRightInfo(int userID)
        {
            return new Bigdesk8.Business.UserRightManager.UserRightInfo(GetUserRightDBOperator(), userID);
        }

        public static Bigdesk8.Business.UserRightManager.IUserRightInfo CreateUserRightInfo(string loginName)
        {
            return new Bigdesk8.Business.UserRightManager.UserRightInfo(GetUserRightDBOperator(), loginName);
        }

        public static Bigdesk8.Business.UserRightManager.IUserRightInfo CreateUserRightInfo(string loginName, string passWord)
        {
            return new Bigdesk8.Business.UserRightManager.UserRightInfo(GetUserRightDBOperator(), loginName, passWord);
        }
        #endregion

    }
}
