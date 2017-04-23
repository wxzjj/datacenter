using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bigdesk8;
using Bigdesk8.Web.Controls;
using Bigdesk8.Web;
using Bigdesk8.Data;
using Wxjzgcjczy.BLL;
using System.Data;
using System.IO;
using Microsoft.Office.Interop.Word;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Report
{
    public partial class HtbaReport : BasePage
    {
        private TjfxBLL BLL;
        private Microsoft.Office.Interop.Word.Document _DocumentClass = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL = new TjfxBLL(this.WorkUser);
            if (!this.IsPostBack)
            {
                WebCommon.CheckBoxListDataBind(this.cbl_ssdq);
                WebCommon.CheckBoxListDataBind(this.cbl_Htlb);
                searchData();
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            searchData();
        }


        public void searchData()
        {
            string ssdq = "";
            foreach (ListItem item in this.cbl_ssdq.Items)
            {
                if (item.Selected)
                    ssdq += item.Value + ",";
            }
            if (!string.IsNullOrEmpty(ssdq))
            {
                ssdq = ssdq.Substring(0, ssdq.Length - 1);
            }

            string tblb = "";
            foreach (ListItem item in this.cbl_Htlb.Items)
            {
                if (item.Selected)
                    tblb += item.Value + ",";
            }
            tblb = tblb.Trim(',');

            DataTable dt = BLL.RetrieveTjfx_Htba_Report(ContractDate1.Text, ContractDate2.Text, ssdq, tblb).Result;
            this.SetControlValue(dt.Rows[0].ToDataItem());
        }

        protected void btnDc_Click(object sender, EventArgs e)
        {
            #region Word打印PDF
            //string strReportDir = "";
            //string filename = "HtBa" + DateTime.Now.ToString("yyyyMMddHHmmss");

            //SetWord(filename);
            //  ConvertToPDF(filename);

            //  strReportDir = Server.MapPath("~/WxjzgcjczyPage/UploadFile" + "/" + filename + ".doc");
            // System.IO.File.Delete(strReportDir);
            //   Response.Write("<script type='text/javascript'> window.open('http://218.90.162.110:8889/WxjzgcjczyPage/UploadFile/" + filename + ".pdf" + "');</script>");
            #endregion

            #region 微软报表
            string condition = string.Empty;
            string ssdq = "";
            foreach (ListItem item in this.cbl_ssdq.Items)
            {
                if (item.Selected)
                    ssdq += item.Value + ",";
            }
            if (!string.IsNullOrEmpty(ssdq))
            {
                ssdq = ssdq.Substring(0, ssdq.Length - 1);
            }

            string tblb = "";
            foreach (ListItem item in this.cbl_Htlb.Items)
            {
                if (item.Selected)
                    tblb += item.Value + ",";
            }
            tblb = tblb.Trim(',');

            condition = "htqdRqStart=" + ContractDate1.Text + "&htqdRqEnd=" + ContractDate2.Text + "&ssdq=" + ssdq + "&ContractTypeNum=" + tblb;
            this.OpenNewWindow("http://218.90.162.110:8889/WxjzgcjczyBb/WebForm1.aspx?" + condition);
            #endregion
        }

        public string ConvertToPDF(string Filename)
        {
            string returnString = string.Empty;
            object htmlPdfFileUrl = string.Empty;
            Microsoft.Office.Interop.Word.Document wordDocument = new Microsoft.Office.Interop.Word.Document();
            Microsoft.Office.Interop.Word.Application wordApplication = new Microsoft.Office.Interop.Word.Application();

            if (!string.IsNullOrEmpty(Filename))
            {
                object _NullObject = System.Reflection.Missing.Value;

                //文件保存目录URL
                String saveUrl = Request.ApplicationPath + "/WxjzgcjczyPage/UploadFile/";
                String dirPath = Server.MapPath(saveUrl);

                string htmlFileFullName;


                htmlFileFullName = Filename + ".doc";



                object htmlFileUrl = dirPath + htmlFileFullName;

                try
                {
                    wordApplication.Options.SaveInterval = 0;
                    //让word按照url打开文件的原因是，保证对文件中图片的src、超链接等能正确解释，从而能正确地转化成pdf
                    wordDocument = wordApplication.Documents.Open(ref htmlFileUrl, ref _NullObject, ref _NullObject, ref _NullObject,
                        ref _NullObject, ref _NullObject, ref _NullObject, ref _NullObject, ref _NullObject, ref _NullObject,
                        ref _NullObject, ref _NullObject, ref _NullObject, ref _NullObject, ref _NullObject, ref _NullObject);

                    //f命名
                    string pdfFileName = Filename + ".pdf";
                    object pdfFileFullName = dirPath + pdfFileName;
                    htmlPdfFileUrl = "http://" + Request.ServerVariables["server_name"];
                    if (!string.IsNullOrEmpty(Request.ServerVariables["server_port"]))
                        htmlPdfFileUrl += ":" + Request.ServerVariables["server_port"];
                    htmlPdfFileUrl += "/" + saveUrl + pdfFileName;

                    returnString = pdfFileFullName.ToString();

                    //保存
                    object _FormateDate = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;
                    wordApplication.NormalTemplate.Saved = true;
                    wordDocument.SaveAs(ref pdfFileFullName, ref _FormateDate, ref _NullObject,
                                    ref _NullObject, ref _NullObject, ref _NullObject,
                                    ref _NullObject, ref _NullObject, ref _NullObject,
                                    ref _NullObject, ref _NullObject, ref _NullObject,
                                    ref _NullObject, ref _NullObject, ref _NullObject, ref _NullObject);
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    object saveOption = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
                    if (wordDocument != null)
                    {
                        wordDocument.Close(ref saveOption, ref _NullObject, ref _NullObject);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(wordDocument);
                        wordDocument = null;
                    }
                    if (wordApplication != null)
                    {
                        wordApplication.Quit(ref saveOption, ref _NullObject, ref _NullObject);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApplication);
                        wordApplication = null;
                    }
                    GC.Collect();
                }
            }
            return returnString;
        }

        #region 动态加载数据到word并保存
        public void SetWord(string savefilename)
        {
            string condition = string.Empty;
            string ssdq = "";
            foreach (ListItem item in this.cbl_ssdq.Items)
            {
                if (item.Selected)
                    ssdq += item.Value + ",";
            }
            if (!string.IsNullOrEmpty(ssdq))
            {
                ssdq = ssdq.Substring(0, ssdq.Length - 1);
            }

            string tblb = "";
            foreach (ListItem item in this.cbl_Htlb.Items)
            {
                if (item.Selected)
                    tblb += item.Value + ",";
            }
            tblb = tblb.Trim(',');

            DataTable dt = BLL.RetrieveTjfx_Htba_Report(ContractDate1.Text,ContractDate2.Text, ssdq, tblb).Result;
            object oMissing = System.Reflection.Missing.Value;
            object _NullObject = System.Reflection.Missing.Value;

            System.IO.File.Copy(HttpContext.Current.Server.MapPath("~/WxjzgcjczyPage/DownLoad/HtBa.docx"), HttpContext.Current.Server.MapPath("~/WxjzgcjczyPage/UploadFile/" + savefilename + ".doc"));//复制原始模板到Upload
            object _Path = HttpContext.Current.Server.MapPath("~/WxjzgcjczyPage/UploadFile/" + savefilename + ".doc");

            Microsoft.Office.Interop.Word.ApplicationClass _Application = new Microsoft.Office.Interop.Word.ApplicationClass();
            _Application.Visible = true;
            object replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
            _DocumentClass = _Application.Documents.Open(ref _Path, ref _NullObject, ref _NullObject, ref _NullObject, ref _NullObject
                   , ref _NullObject, ref _NullObject, ref _NullObject, ref _NullObject, ref _NullObject, ref _NullObject, ref _NullObject, ref _NullObject, ref _NullObject, ref _NullObject, ref _NullObject);


            Replace("{SnqyJzgcXmCount}", dt.Rows[0]["SnqyJzgcXmCount"].ToString());
            Replace("{SnqyKcXmCount}", dt.Rows[0]["SnqyKcXmCount"].ToString());
            Replace("{SnqySzXmCount}", dt.Rows[0]["SnqySzXmCount"].ToString());
            Replace("{SnqyHjXmCount}", dt.Rows[0]["SnqyHjXmCount"].ToString());

            Replace("{SnqyJzgcHtje}", dt.Rows[0]["SnqyJzgcHtje"].ToString());
            Replace("{SnqyKcHtje}", dt.Rows[0]["SnqyKcHtje"].ToString());
            Replace("{SnqySzHtje}", dt.Rows[0]["SnqySzHtje"].ToString());
            Replace("{SnqyHjHtje}", dt.Rows[0]["SnqyHjHtje"].ToString());

            Replace("{SnqyJzgcZtz}", dt.Rows[0]["SnqyJzgcZtz"].ToString());
            Replace("{SnqyKcZtz}", dt.Rows[0]["SnqyKcZtz"].ToString());
            Replace("{SnqySzZtz}", dt.Rows[0]["SnqySzZtz"].ToString());
            Replace("{SnqyHjZtz}", dt.Rows[0]["SnqyHjZtz"].ToString());

            Replace("{SnqyJzgcZmj}", dt.Rows[0]["SnqyJzgcZmj"].ToString());
            Replace("{SnqyKcZmj}", dt.Rows[0]["SnqyKcZmj"].ToString());
            Replace("{SnqySzZmj}", dt.Rows[0]["SnqySzZmj"].ToString());
            Replace("{SnqyHjZmj}", dt.Rows[0]["SnqyHjZmj"].ToString());

            Replace("{SwqyJzgcXmCount}", dt.Rows[0]["SwqyJzgcXmCount"].ToString());
            Replace("{SwqyKcXmCount}", dt.Rows[0]["SwqyKcXmCount"].ToString());
            Replace("{SwqySzXmCount}", dt.Rows[0]["SwqySzXmCount"].ToString());
            Replace("{SwqyHjXmCount}", dt.Rows[0]["SwqyHjXmCount"].ToString());

            Replace("{SwqyJzgcHtje}", dt.Rows[0]["SwqyJzgcHtje"].ToString());
            Replace("{SwqyKcHtje}", dt.Rows[0]["SwqyKcHtje"].ToString());
            Replace("{SwqySzHtje}", dt.Rows[0]["SwqySzHtje"].ToString());
            Replace("{SwqyHjHtje}", dt.Rows[0]["SwqyHjHtje"].ToString());

            Replace("{SwqyJzgcZtz}", dt.Rows[0]["SwqyJzgcZtz"].ToString());
            Replace("{SwqyKcZtz}", dt.Rows[0]["SwqyKcZtz"].ToString());
            Replace("{SwqySzZtz}", dt.Rows[0]["SwqySzZtz"].ToString());
            Replace("{SwqyHjZtz}", dt.Rows[0]["SwqyHjZtz"].ToString());

            Replace("{SwqyJzgcZmj}", dt.Rows[0]["SwqyJzgcZmj"].ToString());
            Replace("{SwqyKcZmj}", dt.Rows[0]["SwqyKcZmj"].ToString());
            Replace("{SwqySzZmj}", dt.Rows[0]["SwqySzZmj"].ToString());
            Replace("{SwqyHjZmj}", dt.Rows[0]["SwqyHjZmj"].ToString());

            Replace("{HjXmCount}", dt.Rows[0]["HjXmCount"].ToString());
            Replace("{HjHtje}", dt.Rows[0]["HjHtje"].ToString());
            Replace("{HjZtz}", dt.Rows[0]["HjZtz"].ToString());
            Replace("{HjZmj}", dt.Rows[0]["HjZmj"].ToString());

            _DocumentClass.Save();
            _DocumentClass.Close(ref _NullObject, ref _NullObject, ref _NullObject);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(_DocumentClass);
            _DocumentClass = null;
            _Application.Quit(ref _NullObject, ref _NullObject, ref _NullObject);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(_Application);
            _Application = null;

            GC.Collect();
        }


        /// 浅析C#Word文档替换操作，在word 中查找一个字符串直接替换所需要的文本  
        /// ﹤/summary﹥  
        /// ﹤param name="strOldText"﹥原文本﹤/param﹥  
        /// ﹤param name="strNewText"﹥新文本﹤/param﹥  
        /// ﹤returns﹥﹤/returns﹥  
        public void Replace(string strOldText, string strNewText)
        {
            _DocumentClass.Content.Find.Text = strOldText;
            object FindText, ReplaceWith, ReplaceType;
            object MissingValue = Type.Missing;
            FindText = strOldText;//要查找的文本  
            ReplaceWith = strNewText;//替换文本     
            //判断字符串内容是否过长，如果过长则自动切断
            if (strNewText.Length > 220)
            {
                ReplaceWith = strNewText.Substring(0, 220) + strOldText;
            }
            ReplaceType = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
            /***
             * wdReplaceAll - 替换找到的所有项。    
             * wdReplaceNone - 不替换找到的任何项。 
             * wdReplaceOne - 替换找到的第一项。  
             ***/
            _DocumentClass.Content.Find.ClearFormatting();  //移除Find的搜索文本和段落格式设置  
            if (_DocumentClass.Content.Find.Execute(ref FindText, ref MissingValue, ref MissingValue, ref MissingValue,
                ref MissingValue, ref MissingValue, ref MissingValue, ref MissingValue, ref MissingValue, ref ReplaceWith,
                ref ReplaceType, ref MissingValue, ref MissingValue, ref MissingValue, ref MissingValue))
            {
                if (strNewText.Length > 220)
                {
                    Replace(FindText.ToString(), strNewText.Substring(221, strNewText.Length - 221));
                }
            }
        }

        #endregion
    }
}
