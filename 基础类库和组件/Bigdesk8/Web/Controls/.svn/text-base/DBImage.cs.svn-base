using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Bigdesk8.Data;

namespace Bigdesk8.Web.Controls
{
    /// <summary>
    /// 支持把内存流中的图片直接显示到页面
    /// 用法：
    /// 在web配置文件<httpHandlers></httpHandlers>中添加
    /// <add verb="*" path="ImageExHandle.axd" type="Bigdesk8.Web.ImageExHandle,Bigdesk8" validate="false"/>
    /// </summary>
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ImageEx runat=server></{0}:ImageEx>")]
    public class ImageEx :Image
    {
        /// <summary>
        /// 存储图像数据的内存流
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public MemoryStream ImageStream
        {
            set
            {
                string imgName = "DBImage" + Guid.NewGuid().ToString();
                HttpContext.Current.Session.Add(imgName, value);
                this.ImageUrl = "ImageExHandle.axd?i=" + imgName;
            }
        }
    }
}
