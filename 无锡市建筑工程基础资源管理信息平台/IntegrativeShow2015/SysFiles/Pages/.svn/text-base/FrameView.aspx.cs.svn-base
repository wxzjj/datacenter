using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegrativeShow2.SysFiles.Pages
{
    public partial class FrameView :System.Web.UI.Page
    {
        public string viewUrl = string.Empty, titleName=string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            viewUrl = Request.QueryString["viewUrl"];
            
            titleName = Request.QueryString["titleName"];
            viewUrl = WebCommon.FormatUrl(viewUrl);
            if (!IsPostBack)
            {

            }
        }
    }
}
