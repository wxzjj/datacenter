using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bigdesk8;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;
using Wxjzgcjczy.BLL;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Text;
using System.Data;
using System.Configuration;

namespace Wxjzgcjczy.Web
{
    public static class WebCommon
    {

        public static string ConnectionString_WJSJZX
        {
            get
            {
                DecryptAndEncryptionHelper helper = new DecryptAndEncryptionHelper(ConfigInformation.Key, ConfigInformation.Vector);
                string encrytStr =ConfigurationManager.AppSettings["ConnectionString"];
                return helper.Decrypto(encrytStr);
            }
        }

        /// <summary>
        /// 获取以“/”结尾的应用程序根路径
        /// </summary>
        /// <returns></returns>
        /// 
        public static string GetApplicationPath()
        {
            string applicationPath = HttpContext.Current.Request.ApplicationPath;
            if (!applicationPath.EndsWith("/"))
            {
                applicationPath += "/";
            }
            return applicationPath;
        }

        /// <summary>
        /// 绑定下拉框
        /// </summary>
        public static void BindListControl(ListControl listControl, CodeType codeType, bool isAddSpaceItem)//项目属地
        {
            //缺省按照键-值的方式来取代码
            BindListControl(listControl, codeType, true, isAddSpaceItem);
        }

        public static void BindListControl(ListControl listControl, CodeType codeType, bool isKeyValue, bool isAddSpaceItem)
        {
            UeppCodeBLL bfpSundries = new UeppCodeBLL();
            if (isKeyValue)
                listControl.ListControlDataBind(bfpSundries.SelectKeyValue(codeType).Result, isAddSpaceItem);//键值方式
            else
                listControl.ListControlDataBind(bfpSundries.SelectValueOnly(codeType).Result, isAddSpaceItem);
        }
        public static void BindListControl(ListControl listControl, CodeType codeType, CodeType parentCodeType, string parentCodeInfo, bool isKeyValue, bool isAddSpaceItem)
        {
            UeppCodeBLL bfpSundries = new UeppCodeBLL();
            if (isKeyValue)
                listControl.ListControlDataBind(bfpSundries.SelectKeyValue(codeType, parentCodeType, parentCodeInfo).Result, isAddSpaceItem);
            else
                listControl.ListControlDataBind(bfpSundries.SelectValueOnly(codeType, parentCodeType, parentCodeInfo).Result, isAddSpaceItem);
        }


        /// <summary>
        /// 填充 DropDownList
        /// </summary>
        public static void DropDownListDataBind(DBDropDownList dropdonlist, bool addSpaceItem)
        {
            string sql = string.Empty;
            switch (dropdonlist.ToolTip)
            {
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
                    sql = "select CodeInfo,Code from tbContractTypeDic ";
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

                case "KCSJContractType":
                    sql = "select CodeInfo,Code from tbContractTypeDic where Code='100' or  Code='200'  ";
                    break;
                case "SGJLContractType":
                    sql = "select CodeInfo,Code from tbContractTypeDic where Code<>'100' and  Code<>'200' ";
                    break;


            }
            if (sql != string.Empty)
            {
                GcxmBLL bll=new GcxmBLL(new AppUser());
                DataTable dt = bll.GetTable(sql, null, "t");
                UIUtility.ListControlDataBind(dropdonlist, dt, addSpaceItem);
            }
        }

        public static void DropDownListDataBind(DBDropDownList dropdonlist,string flag, bool addSpaceItem)
        {
            string sql = string.Empty;
            switch (flag)
            {
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
                    sql = "select CodeInfo,Code from tbContractTypeDic ";
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

                case "KCSJContractType":
                    sql = "select CodeInfo,Code from tbContractTypeDic where Code='100' or  Code='200'  ";
                    break;
                case "SGJLContractType":
                    sql = "select CodeInfo,Code from tbContractTypeDic where Code<>'100' and  Code<>'200' ";
                    break;


            }
            if (sql != string.Empty)
            {
                GcxmBLL bll = new GcxmBLL(new AppUser());
                DataTable dt = bll.GetTable(sql, null, "t");
                UIUtility.ListControlDataBind(dropdonlist, dt, addSpaceItem);
            }
        }



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


        /// <summary>
        /// 填充 RadioButtonList
        /// </summary>
        public static void RadioButtonListDataBind(RadioButtonList radioBtnList)
        {
            string sql = "select CodeInfo,Code from tbContractTypeDic where OrderID >=0 order by OrderID  ";
            DataTable dt = WebCommon.GetDB_WJSJZX().ExeSqlForDataTable(sql, null, "t");
            foreach (DataRow row in dt.Rows)
            {
                radioBtnList.Items.Add(new ListItem(row[0].ToString(), row[1].ToString()));
            }

        }


        /// <summary>
        /// 将当前页面重定向到信息提示页面
        /// </summary>
        public static void RedirectToMessagePage(this Page page, string msg)
        {
            msg = msg.Replace(Environment.NewLine, "<br/>");

            if (msg.Length > 100)
            {
                string nam = "BuildingProjectMSG" + Guid.NewGuid().ToString();
                page.Session[nam] = msg;
                page.Response.Redirect(WebCommon.GetApplicationPath() + "WxjzgcjczyPage/Msg.aspx?nam=" + nam, true);
            }
            else
            {
                page.Response.Redirect(WebCommon.GetApplicationPath() + "WxjzgcjczyPage/Msg.aspx?msg=" + msg, true);
            }
        }

        /// <summary>
        /// 将当前页面重定向到信息提示页面
        /// </summary>
        public static void RedirectToMessagePage(this Page page, Exception msg)
        {
            page.RedirectToMessagePage(GetExceptionMessage(msg));
        }

        /// <summary>
        /// 在一个新窗口中打开信息提示页面
        /// </summary>
        public static void OpenNewMessagePage(this Page page, string msg)
        {
            msg = msg.Replace(Environment.NewLine, "<br/>");

            if (msg.Length > 100)
            {
                string nam = "BuildingProjectMSG" + Guid.NewGuid().ToString();
                page.Session[nam] = msg;
                page.OpenNewWindow(WebCommon.GetApplicationPath() + "WxjzgcjczyPage/Msg.aspx?nam=" + nam, true);
            }
            else
            {
                page.OpenNewWindow(WebCommon.GetApplicationPath() + "WxjzgcjczyPage/Msg.aspx?msg=" + msg, true);
            }
        }

        /// <summary>
        /// 在一个新窗口中打开信息提示页面
        /// </summary>
        public static void OpenNewMessagePage(this Page page, Exception msg)
        {
            page.OpenNewMessagePage(GetExceptionMessage(msg));
        }

        #region 结果处理

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
        /// 对gridrow中的permittedOperates，对行中的超链接、按钮等进行一些设置。
        /// </summary>
        public static void SetGridRow(GridView gvSender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            string permittedOperates = gvSender.DataKeys[e.Row.RowIndex].Values["permittedOperates"].ToString2();
            if (string.IsNullOrEmpty(permittedOperates))
            {
                Label lblPermitOperates = (Label)e.Row.FindControl("Label_PermittedOperates");
                if (lblPermitOperates != null)
                    permittedOperates = lblPermitOperates.Text;
            }

            //编辑
            HyperLink hl_Edit = (HyperLink)e.Row.FindControl("HyperLink_Edit");
            if (hl_Edit != null)
            {
                if (permittedOperates.Contains(OperateCode.Update.ToString()))
                    hl_Edit.Enabled = true;
                else
                    hl_Edit.Enabled = false;
            }

            //删除
            LinkButton lb_Delete = (LinkButton)e.Row.FindControl("LinkButton_Delete");
            if (lb_Delete != null)
            {
                if (permittedOperates.Contains(OperateCode.Delete.ToString()))
                    lb_Delete.Enabled = true;
                else
                {
                    lb_Delete.Enabled = false;
                    lb_Delete.OnClientClick = string.Empty; //去除客户端事件处理代码
                }
            }

            //申报
            HyperLink hl_Submit = (HyperLink)e.Row.FindControl("HyperLink_Submit");
            if (hl_Submit != null)
            {
                hl_Submit.Enabled = false;
                if (permittedOperates.Contains(OperateCode.Submit.ToString()))
                    hl_Submit.Enabled = true;
                else if (permittedOperates.Contains(OperateCode.CancelSubmit.ToString()))
                {
                    hl_Submit.Enabled = true;
                    hl_Submit.Text = "取消" + hl_Submit.Text;
                }  
            }

            //一审
            HyperLink hl_FirstCheck = (HyperLink)e.Row.FindControl("HyperLink_FirstCheck");
            if (hl_FirstCheck != null)
            {
                hl_FirstCheck.Enabled = false;
                if (permittedOperates.Contains(OperateCode.FirstCheck.ToString()))
                    hl_FirstCheck.Enabled = true;
                else if (permittedOperates.Contains(OperateCode.CancelFirstCheck.ToString()))
                {
                    hl_FirstCheck.Enabled = true;
                    hl_FirstCheck.Text = "取消" + hl_FirstCheck.Text;
                }
            }
        }

        /// <summary>
        /// 在新页面中打开报表打印页面
        /// </summary>
        /// <param name="page"></param>
        /// <param name="reportPath">报表路径</param>
        /// <param name="reportParameters">报表参数</param>
        public static void RedirectToReportPrintPage(this Page page, string reportPath, IEnumerable<ReportParameter> reportParameters)
        {
            string url = string.Format("{0}WxjzgcjczyPage/ReportPrint.aspx?ReportPath={1}&ReportParameters={2}&rd={3}",
                WebCommon.GetApplicationPath(), reportPath, Bigdesk8.MsReporting.WebAssistant.ReportParametersToNormalizedString(reportParameters), Guid.NewGuid().ToString());

            page.OpenNewWindow(url, true);
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
        /// 创建报表参数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="parameterValue">参数值</param>
        /// <returns></returns>
        public static ReportParameter CreateParameter(string parameterName, string parameterValue)
        {
            return new ReportParameter(parameterName, parameterValue);
        }
        public static void AddDataItem(List<IDataItem> litm, string itemName, string itemData)
        {
            IDataItem idt = new DataItem();
            idt.ItemName = itemName;
            idt.ItemData = itemData;
            litm.Add(idt);
        }

        public static List<SqlPara> ConvertSql( string text)
        {
            string splitString = "##";
            string textCopy = String.Copy(text);
            List<SqlPara> sqls = new List<SqlPara>();
            int count = 0;
            while (count < text.Length)
            {
                int begin = textCopy.IndexOf(splitString);
                int end;

                if (begin >= 0)
                {
                    textCopy = textCopy.Remove(0, begin + splitString.Length);
                    count += begin + splitString.Length;
                    end = textCopy.IndexOf(splitString);

                    if (end >= 0)
                    {
                        SqlPara para = new SqlPara();
                        para.begin = count;
                        para.end = end + count - 1;
                        para.oldStr = splitString + textCopy.Substring(0, end) + splitString;
                        para.executeSql = textCopy.Substring(0, end);

                        textCopy = textCopy.Remove(0, end + splitString.Length);
                        count += end + splitString.Length;
                        sqls.Add(para);
                    }
                    else
                    {
                        //格式有问题
                    }
                }
                else
                {
                    count += textCopy.Length;
                }
            }
            return sqls;
        }

        public static void WriteLog(string msg)
        {
            string filaname = AppDomain.CurrentDomain.BaseDirectory + "\\Log_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            try
            {
                if (!File.Exists(filaname))
                {
                    File.WriteAllText(filaname, msg + "\r\n", Encoding.UTF8);
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(filaname))
                    {
                        sw.WriteLine(msg);
                        sw.Flush();
                        sw.Close();
                    }
                }
            }
            catch
            {

            }
        }
    }


    public class SqlPara
    {
        public int begin;
        public int end;
        public string oldStr;
        public string executeSql;
        public string newStr;

    }

}
