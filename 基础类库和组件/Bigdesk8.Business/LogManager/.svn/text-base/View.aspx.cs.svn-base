using System;
using System.Collections.Generic;
using System.Data;
using Bigdesk8;
using Bigdesk8.Data;
using Bigdesk8.Web.Controls;

namespace Bigdesk8.Business.LogManager
{
    public partial class View : SystemBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                long logID = this.Request.QueryString["ID"].ToInt64();

                ILogManager logger = LogManagerFactory.CreateLogManager("");
                BusinessLog log = logger.GetLog(logID);

                if (log == null)
                {
                    this.ResponseRedirect("访问的数据不存在！错误代码：001");
                    return;
                }

                List<IDataItem> list = log.ToDataItem();
                this.SetControlValue(list);
            }
        }
    }
}