using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WxjzgcjczyQyb.BLL;
using System.Data;
using Bigdesk8;
using Bigdesk8.Data;
using Bigdesk8.Web;
using Bigdesk8.Web.Controls;
using System.IO;

namespace WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Zyry
{
    public partial class ImageShow : BasePage
    {
        private string ryID;
        private ZcryBLL BLL;
        private AppUser WorkUser = new AppUser();

        protected void Page_Load(object sender, EventArgs e)
        {
            BLL = new ZcryBLL(this.WorkUser);
            ryID = this.Request.QueryString["ryid"];
            DataTable dt = BLL.Read(ryID);
            DataRow model = null;
            if (dt != null && dt.Rows.Count > 0)
                model = dt.Rows[0];
            if (model != null && model["sfzsmj"] != DBNull.Value && model["sfzsmj"].ToString().Length > 0)
            {
                Byte[] b = (byte[])model["sfzsmj"];
                //this.Response.Write(b.Length);
                if (b != null && b.Length > 0)
                {
                    MemoryStream mymemorystream = new MemoryStream(b, 0, b.Length);
                    System.Drawing.Image image = System.Drawing.Image.FromStream(mymemorystream);
                    System.Drawing.Image.GetThumbnailImageAbort cb = new System.Drawing.Image.GetThumbnailImageAbort(CallBack);
                    System.Drawing.Image thumbnailImage = image.GetThumbnailImage(110, 137, cb, System.IntPtr.Zero);
                    MemoryStream ms = new MemoryStream();
                    thumbnailImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    Response.BinaryWrite(ms.ToArray());
                }
                else
                {
                    Response.Write("<span style=\" color:Red;text-align:center;vertical-align:middle;height:100%;padding-top:45%;\">无照片信息</span>");
                }
            }
            else
            {
                Response.Write("<span style=\" color:Red;text-align:center;vertical-align:middle;height:100%;padding-top:45%;\">无照片信息</span>");
            }
        }
        public bool CallBack()
        {
            return false;
        }
    }
}
