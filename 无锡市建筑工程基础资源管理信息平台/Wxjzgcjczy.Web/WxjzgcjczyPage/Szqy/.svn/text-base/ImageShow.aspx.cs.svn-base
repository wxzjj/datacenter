using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Wxjzgcjczy.BLL;
using System.IO;

namespace Wxjzgcjczy.Web.WxjzgcjczyPage.Szqy
{
    public partial class ImageShow : BasePage
    {
        protected SzqyBLL BLL;
        private string RegId;
        //protected string RyID;
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    RyID=Request.QueryString["ryId"];
        //    BLL=new SzqyBLL(WorkUser);
        //    DataRow model = BLL.ReadQyZyryxx(this.RyID).Result.Rows[0];

        //    if (model != null && model["Zp"] != DBNull.Value && model["Zp"].ToString().Length > 0)
        //    {
        //        Byte[] b = (byte[])model["Zp"];
             
        //        if (b != null && b.Length > 0)
        //        {
        //            MemoryStream mymemorystream = new MemoryStream(b, 0, b.Length);
        //            System.Drawing.Image image = System.Drawing.Image.FromStream(mymemorystream);
        //            System.Drawing.Image.GetThumbnailImageAbort cb = new System.Drawing.Image.GetThumbnailImageAbort(CallBack);
        //            System.Drawing.Image thumbnailImage = image.GetThumbnailImage(100, 125, cb, System.IntPtr.Zero);
        //            MemoryStream ms = new MemoryStream();
        //            thumbnailImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        //            Response.BinaryWrite(ms.ToArray());
        //        }
        //        else
        //        {
        //            Response.Write("<span style=\" color:Red;text-align:center;vertical-align:middle;height:100%;padding-top:45%;\">无照片信息</span>");
        //        }
        //    }
        //    else
        //    {
        //        Response.Write("<span style=\" color:Red;text-align:center;vertical-align:middle;height:100%;padding-top:45%;\">无照片信息</span>");
        //    }
        //}

        //private FjxxBLL BLL;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.RegId = HttpUtility.UrlDecode(this.Request.QueryString["RegId"]);
            //BLL = new FjxxBLL(this.WorkUser);
            //DataTable dt = BLL.Read(RegId).Result;
            //if (dt.Rows.Count > 0)
            //{
            try
            {
                //Byte[] b = (byte[])dt.Rows[0]["XxContent"];
                //MemoryStream mymemorystream = new MemoryStream(b, 0, b.Length);
                System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath(RegId));//System.Drawing.Image.FromStream(mymemorystream);
                System.Drawing.Image.GetThumbnailImageAbort cb = new System.Drawing.Image.GetThumbnailImageAbort(CallBack);
                System.Drawing.Image thumbnailImage = image.GetThumbnailImage(200, 300, cb, System.IntPtr.Zero);
                MemoryStream ms = new MemoryStream();
                thumbnailImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                Response.BinaryWrite(ms.ToArray());
            }
            catch (Exception ee)
            {
            }
            // }
        }
        public bool CallBack()
        {
            return false;
        }

    }
}
