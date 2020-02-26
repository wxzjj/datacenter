using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage
{
    public partial class UserCheck : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                string name = ConfigManager.GetValidateName();
                string pass = ConfigManager.GetValidatePass();


                WebClient Wc = new WebClient();
                byte[] content = new byte[Request.ContentLength];
                Request.InputStream.Read(content, 0, content.Length);
                byte[] returnvalue = Wc.UploadData(string.Format("http://218.90.163.147:8088/netoffice/WXJSJHB/Pages/TYLogin/TYValidate.aspx?name={0}&pass={1}", name, pass), content);
                Response.Clear();
                if (Encoding.UTF8.GetString(returnvalue) == "true")
                {
                    Response.Write("http://58.215.18.222:8889/WxjzgcjczyPage/UserAuthorization.aspx");
                }
                else
                {
                    Response.Write("false");
                }
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
