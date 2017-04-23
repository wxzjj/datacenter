using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Bigdesk2010.Web.Controls
{
    /*
     * 1.初始界面，显示一个浏览文件按钮
     * 2。选定文件后，隐藏浏览按钮，显示上传进度界面，上传文件过程中，锁定界面（可设置），并可以随时取消上传
     * 3。上传完成后，返回文件的关键信息，隐藏上传界面，显示上传文件链接和删除按钮
     * 4。当属性设置为只读时，显示上传文件链接
     * 
     */

    /*
     * 在使用wcf方式时，
     * 需要注意必须使用Bigdesk2010.Web.Controls.IUploadFileService做为wcf的基类，
     * 并且wcf类的名称必须为UploadFileService，但命名空间可以任意指定。
     * 添加服务引用后，会自动生成一个配置文件，必须要手工删除这个配置文件ServiceReferences.ClientConfig
     */

    /// <summary>
    /// 大文件上传控件
    /// 
    /// 此控件依赖于 Silverlight 3, Silverlight.js, jquery-1.4.2.min.js, jquery.Silverlight.bigfileupload.js
    /// 
    /// 功能：
    /// 支持大文件上传,
    /// 支持文件完整性校验,
    /// 支持断点续传,
    /// 支持文件类型、文件大小客户端校验,
    /// 提示上传速度，上传进度，剩余时间
    /// 
    /// </summary>
    [Description("大文件上传控件")]
    [DefaultProperty("ScriptRootUrl")]
    [PersistChildren(false)]
    [Designer(typeof(BigFileUploadDesigner))]
    [ToolboxData("<{0}:BigFileUpload runat=server></{0}:BigFileUpload>")]
    public class BigFileUpload : WebControl, IPostBackDataHandler
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public BigFileUpload()
            : base(HtmlTextWriterTag.Div)
        {
            base.EnableViewState = true;
            this.Width = new Unit(250);
            this.Height = new Unit(100);
        }

        #endregion 构造函数

        #region 属性

        /// <summary>
        /// 脚本所在目录，结束不要加“/”，如：/scripts
        /// </summary>
        [Browsable(true)]
        [Category("扩展属性")]
        [Description("脚本所在目录，结束不要加“/”，如：/scripts")]
        [DefaultValue("")]
        public virtual string ScriptRootUrl
        {
            get
            {
                return this.ViewState["_ScriptRootUrl"].TrimString();
            }
            set
            {
                this.ViewState["_ScriptRootUrl"] = value;
            }
        }

        /// <summary>
        /// 上传服务的Url，这个地址是相对于xap文件的位置
        /// </summary>
        [Browsable(true)]
        [Category("扩展属性")]
        [Description("上传服务的Url，这个地址是相对于xap文件的位置")]
        [DefaultValue("")]
        public virtual string UploadFileServiceUrl
        {
            get
            {
                if (this.ViewState["_UploadFileServiceUrl"].IsEmpty()) return "";
                return this.ViewState["_UploadFileServiceUrl"].TrimString();
            }
            set
            {
                this.ViewState["_UploadFileServiceUrl"] = value;
            }
        }

        /// <summary>
        /// 文件上传服务方式，取值范围：WCF、HttpHandler。
        /// </summary>
        [Browsable(true)]
        [Category("扩展属性")]
        [Description("文件上传服务方式，取值范围：WCF、HttpHandler。")]
        [DefaultValue("WCF")]
        public virtual string FileUploaderType
        {
            get
            {
                if (this.ViewState["_FileUploaderType"].IsEmpty())
                    return "WCF";
                return this.ViewState["_FileUploaderType"].TrimString();
            }
            set
            {
                this.ViewState["_FileUploaderType"] = value;
            }
        }

        /// <summary>
        /// 上传文件的最大块大小，默认值为int.MaxValue
        /// </summary>
        [Browsable(true)]
        [Category("扩展属性")]
        [Description("上传文件的最大块大小，默认值为int.MaxValue")]
        [DefaultValue(int.MaxValue)]
        public long MaxChunkSize
        {
            get
            {
                if (this.ViewState["_MaxChunkSize"].IsEmpty()) return int.MaxValue;
                return (long)this.ViewState["_MaxChunkSize"];
            }
            set
            {
                this.ViewState["_MaxChunkSize"] = value;
            }
        }

        /// <summary>
        /// 最大重试次数，默认值为0，0表示无限制
        /// </summary>
        [Browsable(true)]
        [Category("扩展属性")]
        [Description("最大重试次数，默认值为0，0表示无限制")]
        [DefaultValue(0)]
        public int MaxRetryCount
        {
            get
            {
                if (this.ViewState["_MaxRetryCount"].IsEmpty()) return 0;
                return this.ViewState["_MaxRetryCount"].ToInt32();
            }
            set
            {
                this.ViewState["_MaxRetryCount"] = value;
            }
        }

        /// <summary>
        /// 允许上传的最大文件大小，默认值为1GB
        /// </summary>
        [Browsable(true)]
        [Category("扩展属性")]
        [Description("允许上传的最大文件大小，默认值为1GB")]
        [DefaultValue(1073741824)]
        public long MaxFileSize
        {
            get
            {
                if (this.ViewState["_MaxFileSize"].IsEmpty()) return 1073741824;

                return (long)this.ViewState["_MaxFileSize"];
            }
            set
            {
                this.ViewState["_MaxFileSize"] = value;
            }
        }

        /// <summary>
        /// 允许上传的文件类型
        /// </summary>
        [Browsable(true)]
        [Category("扩展属性")]
        [Description("允许上传的文件类型，文档文件 (*.doc;*.xls;*.ppt;*.txt;*.pdf)|*.doc;*.xls;*.ppt;*.txt;*.pdf|图片文件 (*.jpg;*.jpeg;*.gif;*.bmp;*.png)|*.jpg;*.jpeg;*.gif;*.bmp;*.png|压缩文件 (*.zip;*.rar)|*.zip;*.rar|多媒体文件 (*.wav;*.mp3;*.wma;*.wmv;*.mpg;*.avi;*.3gp;*.mp4)|*.wav;*.mp3;*.wma;*.wmv;*.mpg;*.avi;*.3gp;*.mp4|所有文件 (*.*)|*.*")]
        [DefaultValue("所有文件 (*.*)|*.*")]
        public string FileFilter
        {
            get
            {
                if (this.ViewState["_FileFilter"].IsEmpty()) return "所有文件 (*.*)|*.*";
                return this.ViewState["_FileFilter"].TrimString();
            }
            set
            {
                this.ViewState["_FileFilter"] = value;
            }
        }

        /// <summary>
        /// 自定义参数, 例如CustomParams.Add("SystemName", "发承包子系统")
        /// </summary>
        [Browsable(false)]
        public virtual Dictionary<string, string> CustomParams
        {
            get
            {
                if (this.ViewState["_CustomParams"] == null)
                    this.ViewState["_CustomParams"] = new Dictionary<string, string>();

                return (Dictionary<string, string>)this.ViewState["_CustomParams"];
            }
            set
            {
                this.ViewState["_CustomParams"] = value;
            }
        }

        /// <summary>
        /// 文件的唯一编号
        /// </summary>
        [Browsable(true)]
        [Category("扩展属性")]
        [Description("文件的唯一编号")]
        [DefaultValue("")]
        public virtual string FileID
        {
            get
            {
                if (this.ViewState["_FileID"].IsEmpty()) return "";
                return this.ViewState["_FileID"].TrimString();
            }
            set
            {
                this.ViewState["_FileID"] = value;
            }
        }

        /// <summary>
        /// 文件的原始名称(包含扩展名)，不包含目录路径
        /// </summary>
        [Browsable(true)]
        [Category("扩展属性")]
        [Description("文件的原始名称(包含扩展名)，不包含目录路径")]
        [DefaultValue("")]
        public virtual string FileName
        {
            get
            {
                if (this.ViewState["_FileName"].IsEmpty()) return "";
                return this.ViewState["_FileName"].TrimString();
            }
            set
            {
                this.ViewState["_FileName"] = value;
            }
        }

        /// <summary>
        /// 文件的大小（以字节为单位）
        /// </summary>
        [Browsable(true)]
        [Category("扩展属性")]
        [Description("文件的大小（以字节为单位）")]
        [DefaultValue(0)]
        public virtual long FileSize
        {
            get
            {
                if (this.ViewState["_FileSize"].IsEmpty()) return 0;
                return this.ViewState["_FileSize"].ToInt64();
            }
            set
            {
                this.ViewState["_FileSize"] = value;
            }
        }

        /// <summary>
        /// 是否处于调试状态
        /// </summary>
        [Browsable(true)]
        [Category("扩展属性")]
        [Description("是否处于调试状态")]
        [DefaultValue(false)]
        public bool IsDebug
        {
            get
            {
                if (this.ViewState["_IsDebug"].IsEmpty()) return false;
                return this.ViewState["_IsDebug"].ToBoolean();
            }
            set
            {
                this.ViewState["_IsDebug"] = value;
            }
        }

        /// <summary>
        /// 正在上传时是否锁定界面
        /// </summary>
        [Browsable(true)]
        [Category("扩展属性")]
        [Description("正在上传时是否锁定界面")]
        [DefaultValue(true)]
        public bool IsLockPage
        {
            get
            {
                if (this.ViewState["_IsLockPage"].IsEmpty()) return true;
                return this.ViewState["_IsLockPage"].ToBoolean();
            }
            set
            {
                this.ViewState["_IsLockPage"] = value;
            }
        }

        /// <summary>
        /// 启用视图状态，该属性一直为true
        /// </summary>
        [Browsable(false)]
        [DefaultValue(true)]
        public override bool EnableViewState
        {
            get
            {
                base.EnableViewState = true;
                return base.EnableViewState;
            }
            set { }
        }

        /// <summary>
        /// 获取或设置背景色。
        /// </summary>
        [Browsable(true)]
        [Category("扩展属性")]
        [Description("获取或设置背景色")]
        [DefaultValue(typeof(Color), "White")]
        public override Color BackColor
        {
            get
            {
                if (base.BackColor.IsEmpty)
                    return Color.White;
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }

        /// <summary>
        /// 是否有文件
        /// </summary>
        public bool HasFile
        {
            get
            {
                return !this.FileID.IsEmpty();
            }
        }

        #endregion 属性

        #region Render

        /// <summary>
        /// 将需要呈现的 HTML 属性和样式添加到指定的 <see cref="HtmlTextWriterTag"/> 中。此方法主要由控件开发人员使用。
        /// </summary>
        /// <param name="writer">表示要在客户端呈现 HTML 内容的输出流</param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (!this.IsLockPage)
            {
                writer.AddAttribute("IsLockPage", "false");
            }
            base.AddAttributesToRender(writer);
        }

        /// <summary>
        /// 在加载 Control 对象之后、呈现之前发生。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            this.Page.RegisterRequiresPostBack(this);

            // 注册隐藏控件
            base.Page.ClientScript.RegisterHiddenField(this.ClientID + "__hidden_FileID", this.FileID);
            base.Page.ClientScript.RegisterHiddenField(this.ClientID + "__hidden_FileName", this.FileName);
            base.Page.ClientScript.RegisterHiddenField(this.ClientID + "__hidden_FileSize", this.FileSize.ToString());

            //// 注册脚本
            //string js1 = string.Format("<script type=\"text/javascript\" src=\"{0}/jquery-1.4.2.min.js\"></script>", this.ScriptRootUrl);
            //string js2 = string.Format("<script type=\"text/javascript\" src=\"{0}/Silverlight.js\"></script>", this.ScriptRootUrl);
            //string js3 = string.Format("<script type=\"text/javascript\" src=\"{0}/jquery.Silverlight.bigfileupload.js\"></script>", this.ScriptRootUrl);

            //if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "BigFileUpload8_script"))
            //{
            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "BigFileUpload8_script", js1 + js2 + js3, false);
            //}
        }

        /// <summary>
        /// 将控件的内容呈现到指定的编写器中。此方法主要由控件开发人员使用。
        /// </summary>
        /// <param name="writer">表示要在客户端呈现 HTML 内容的输出流。</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            string xap = string.Format("{0}/BigFileUpload_SilverlightApp.xap", this.ScriptRootUrl);

            string customParams = "";
            if (this.CustomParams != null)
            {
                foreach (string key in this.CustomParams.Keys)
                {
                    customParams += string.Format("{0}={1};", key, this.CustomParams[key]);
                }
            }

            string initParams = string.Format(@"ParentID={8},FileUploaderType={9},UploadFileServiceUrl={0},MaxChunkSize={1},MaxRetryCount={2},MaxFileSize={3},FileFilter={4},ReadOnly={5},Debug={6},CustomParams={7}",
                this.UploadFileServiceUrl, this.MaxChunkSize, this.MaxRetryCount, this.MaxFileSize, this.FileFilter, !this.Enabled, this.IsDebug, customParams, this.ClientID, this.FileUploaderType);

            string controljs = "<script type=\"text/javascript\">$(function(){createBigFileUploadControl(\"" + xap + "\", \"" + this.ClientID + "_pluginID" + "\", \"" + this.ClientID + "\", \"" + initParams + "\", \"" + ColorTranslator.ToHtml(this.BackColor) + "\");});</script>";
            writer.Write(controljs);

            base.RenderContents(writer);
        }

        #endregion Render

        #region IPostBackDataHandler 成员

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postDataKey"></param>
        /// <param name="postCollection"></param>
        /// <returns></returns>
        protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            this.FileID = postCollection[postDataKey + "__hidden_FileID"];
            this.FileName = postCollection[postDataKey + "__hidden_FileName"];
            long filesize;
            long.TryParse(postCollection[postDataKey + "__hidden_FileSize"], out filesize);
            this.FileSize = filesize;

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void RaisePostDataChangedEvent()
        {

        }

        bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            return LoadPostData(postDataKey, postCollection);
        }

        void IPostBackDataHandler.RaisePostDataChangedEvent()
        {
            RaisePostDataChangedEvent();
        }

        #endregion
    }

    /// <summary>
    /// <see cref="BigFileUploadDesigner"/> 服务器控件设计器。
    /// </summary>
    internal class BigFileUploadDesigner : ControlDesigner
    {
        /// <summary>
        /// 初始化 <see cref="BigFileUploadDesigner"/> 的新实例
        /// </summary>
        public BigFileUploadDesigner() { }

        private BigFileUpload bigFileUpload = null;

        /// <summary>
        /// 使设计器准备查看、编辑和设计指定的组件
        /// </summary>
        /// <param name="component">此设计器的组件</param>
        public override void Initialize(IComponent component)
        {
            this.bigFileUpload = (BigFileUpload)component;
            base.Initialize(component);
        }

        /// <summary>
        /// 获取用于在设计时表示关联控件的 HTML。
        /// </summary>
        /// <returns>用于在设计时表示控件的 HTML。</returns>
        public override string GetDesignTimeHtml()
        {
            StringWriter sw = new StringWriter();

            HtmlTextWriter htw = new HtmlTextWriter(sw);
            HtmlTable t = new HtmlTable();
            t.CellPadding = 3;
            t.CellSpacing = 0;
            t.BorderColor = ColorTranslator.ToHtml(Color.Black);
            t.BgColor = ColorTranslator.ToHtml(this.bigFileUpload.BackColor);
            t.Width = "200px";
            t.Height = "50px";
            HtmlTableRow tr = new HtmlTableRow();
            HtmlTableCell td = new HtmlTableCell();
            td.VAlign = "top";
            td.Align = "center";

            // inner table for iframe
            HtmlTable iframe = new HtmlTable();
            iframe.BgColor = "#FFFFFF";
            iframe.Width = "100%";
            iframe.Height = "100%";
            iframe.CellPadding = 0;
            iframe.CellSpacing = 0;
            iframe.Style.Add("border", "1px solid " + ColorTranslator.ToHtml(Color.DarkBlue));
            HtmlTableRow tr2 = new HtmlTableRow();
            HtmlTableCell td2 = new HtmlTableCell();
            td2.VAlign = "middle";
            td2.Align = "center";
            td2.Controls.Add(new LiteralControl(this.bigFileUpload.ID));
            tr2.Cells.Add(td2);
            iframe.Rows.Add(tr2);

            td.Controls.Add(iframe);
            tr.Cells.Add(td);
            t.Rows.Add(tr);
            t.RenderControl(htw);
            return sw.ToString();
        }

        /// <summary>
        /// 获取在呈现控件时遇到错误后在设计时为指定的异常显示的 HTML。
        /// </summary>
        /// <param name="e">要为其显示错误信息的异常。</param>
        /// <returns>设计时为指定的异常显示的 HTML。</returns>
        protected override string GetErrorDesignTimeHtml(Exception e)
        {
            string errorstr = "创建控件时出错！" + e.Message;
            return CreatePlaceHolderDesignTimeHtml(errorstr);
        }
    }
}