using System;
using System.Collections.Generic;
using System.Data;
using Bigdesk8;
using Bigdesk8.Data;
using Bigdesk8.Web.Controls;
using System.Web;
using System.Text;

namespace Bigdesk8.Business.FileManager
{
    public partial class View : SystemBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                int fileID = this.Request.QueryString["ID"].ToInt32();

                IFileManager fm = FileManagerFactory.CreateFileManager("");
                FullFileModel file = fm.GetFullFileModel(fileID);

                if (file == null)
                {
                    this.ResponseRedirect("访问的数据不存在！错误代码：001");
                    return;
                }

                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.Charset = "utf-8";
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(file.FileName, Encoding.UTF8));
                Response.BinaryWrite(file.FileContent);
                Response.Flush();
                Response.End();
            }
        }
    }
}