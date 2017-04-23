using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Web.UI;
using Bigdesk2010.Data;

namespace Bigdesk2010.Web.Controls
{
    /// <summary>
    /// 统一文件管理控件，使用此控件不需要关心上传文件的存储、浏览等问题。
    /// 
    /// 此控件从 BigFileUpload 控件继承
    /// 此控件依赖于 DBBigFileUpload_Service.svc 或 DBBigFileUpload_Service.ashx
    /// 
    /// </summary>
    [Description("统一文件管理控件")]
    [ToolboxData("<{0}:DBBigFileUpload runat=server></{0}:DBBigFileUpload>")]
    public class DBBigFileUpload : BigFileUpload
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DBBigFileUpload()
        {
            this.FileUploaderType = "HttpHandler";
        }

        #endregion 构造函数

        #region 属性

        /// <summary>
        /// 文件所在的系统编号
        /// </summary>
        [Browsable(true)]
        [Category("扩展属性")]
        [Description("文件所在的系统编号")]
        [DefaultValue(0)]
        public int SystemID
        {
            get
            {
                return this.ViewState["_SystemID"].ToInt32(0);
            }
            set
            {
                this.ViewState["_SystemID"] = value;
            }
        }

        /// <summary>
        /// 文件所在的模块代码
        /// </summary>
        [Browsable(true)]
        [Category("扩展属性")]
        [Description("文件所在的模块代码")]
        [DefaultValue("")]
        public string BizModuleCode
        {
            get
            {
                return this.ViewState["_ModuleCode"].TrimString();
            }
            set
            {
                this.ViewState["_ModuleCode"] = value;
            }
        }

        /// <summary>
        /// 文件的种类代码
        /// </summary>
        [Browsable(true)]
        [Category("扩展属性")]
        [Description("文件的种类代码")]
        [DefaultValue("")]
        public string CategoryCode
        {
            get
            {
                return this.ViewState["_CategoryCode"].TrimString();
            }
            set
            {
                this.ViewState["_CategoryCode"] = value;
            }
        }

        /// <summary>
        /// 业务关键字字串
        /// </summary>
        [Browsable(true)]
        [Category("扩展属性")]
        [Description("业务关键字字串")]
        [DefaultValue("")]
        public string MasterID
        {
            get
            {
                return this.ViewState["_MasterID"].TrimString();
            }
            set
            {
                this.ViewState["_MasterID"] = value;
            }
        }

        /// <summary>
        /// 脚本所在目录，结束不要加“/”，默认值为：/ClientResource
        /// </summary>
        [Browsable(true)]
        [Category("扩展属性")]
        [Description("脚本所在目录，结束不要加“/”，默认值为：/ClientResource")]
        [DefaultValue("/ClientResource")]
        public override string ScriptRootUrl
        {
            get
            {
                if (base.ScriptRootUrl.IsEmpty())
                    return "/ClientResource";
                else
                    return base.ScriptRootUrl;
            }
            set
            {
                base.ScriptRootUrl = value;
            }
        }

        /// <summary>
        /// 上传服务的Url，这个地址是相对于xap文件的位置，默认值为DBBigFileUpload_Service.ashx
        /// </summary>
        [Browsable(true)]
        [Category("扩展属性")]
        [Description("上传服务的Url，这个地址是相对于xap文件的位置，默认值为DBBigFileUpload_Service.ashx")]
        [DefaultValue("")]
        public override string UploadFileServiceUrl
        {
            get
            {
                if (base.UploadFileServiceUrl.IsEmpty())
                    return "DBBigFileUpload_Service.ashx";
                else
                    return base.UploadFileServiceUrl;
            }
            set
            {
                base.UploadFileServiceUrl = value;
            }
        }

        #endregion 属性

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            base.CustomParams["SystemID"] = this.SystemID.ToString();
            base.CustomParams["BizModuleCode"] = this.BizModuleCode;
            base.CustomParams["CategoryCode"] = this.CategoryCode;
            base.CustomParams["MasterID"] = this.MasterID;
        }
    }
}
