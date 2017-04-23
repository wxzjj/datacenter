using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegrativeShow2.SysFiles.Pages
{
    public partial class Index_View : System.Web.UI.Page
    {
        public string viewUrl = string.Empty, titleName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            viewUrl = Request.QueryString["viewUrl"];
            viewUrl = viewUrl.Replace("%","%25");//%为特殊字符，在url传递要将带有%的后面加25才可以
            titleName = Request.QueryString["titleName"];
            if (!IsPostBack)
            {

            }
        }
    }
}
