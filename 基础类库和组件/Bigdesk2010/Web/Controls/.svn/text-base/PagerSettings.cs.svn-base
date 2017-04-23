using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;

namespace Bigdesk2010.Web.Controls
{
    /// <summary>
    /// 表示支持分页的控件中的分页控件的属性。无法继承此类。
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public sealed class PagerSettings : IStateManager
    {
        /// <summary>
        /// 属性更改值时发生
        /// </summary>
        [Browsable(false)]
        public event EventHandler PropertyChanged;

        private void OnPropertyChanged()
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 获取或设置为第一页按钮显示的图像的 URL。为第一页按钮显示的图像的 URL。默认值为空字符串 ("")，表示尚未设置。
        /// </summary>
        [NotifyParentProperty(true),
        Editor(typeof(ImageUrlEditor), typeof(UITypeEditor)),
        UrlProperty,
        DefaultValue(""),
        Category("Appearance"),
        Description("获取或设置为第一页按钮显示的图像的 URL。为第一页按钮显示的图像的 URL。默认值为空字符串，表示尚未设置。")]
        public string FirstPageImageUrl
        {
            get
            {
                object obj2 = this.ViewState["FirstPageImageUrl"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return string.Empty;
            }
            set
            {
                if (this.FirstPageImageUrl != value)
                {
                    this.ViewState["FirstPageImageUrl"] = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 获取或设置为第一页按钮显示的文字。默认值为 首页
        /// </summary>
        [Category("Appearance"),
        Description("获取或设置为第一页按钮显示的文字。默认值为 首页"),
        DefaultValue("首页"),
        NotifyParentProperty(true)]
        public string FirstPageText
        {
            get
            {
                object obj2 = this.ViewState["FirstPageText"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return "首页";
            }
            set
            {
                if (this.FirstPageText != value)
                {
                    this.ViewState["FirstPageText"] = value;
                    this.OnPropertyChanged();
                }
            }
        }
        
        /// <summary>
        /// 获取或设置当鼠标指针悬停在第一页按钮上时显示的文本。默认值为 转到第一页
        /// </summary>
        [Category("Appearance"),
        Description("获取或设置当鼠标指针悬停在第一页按钮上时显示的文本。默认值为 转到第一页"),
        DefaultValue("转到第一页"),
        NotifyParentProperty(true)]
        public string FirstPageToolTipText
        {
            get
            {
                object obj2 = this.ViewState["FirstPageToolTipText"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return "转到第一页";
            }
            set
            {
                if (this.FirstPageToolTipText != value)
                {
                    this.ViewState["FirstPageToolTipText"] = value;
                    this.OnPropertyChanged();
                }
            }
        }
        
        /// <summary>
        /// 获取或设置为最后一页按钮显示的图像的 URL。为最后一页按钮显示的图像的 URL。默认值为空字符串 ("")，表示尚未设置。
        /// </summary>
        [NotifyParentProperty(true),
        Editor(typeof(ImageUrlEditor), typeof(UITypeEditor)),
        UrlProperty,
        DefaultValue(""),
        Category("Appearance"),
        Description("获取或设置为最后一页按钮显示的图像的 URL。为最后一页按钮显示的图像的 URL。默认值为空字符串，表示尚未设置。")]
        public string LastPageImageUrl
        {
            get
            {
                object obj2 = this.ViewState["LastPageImageUrl"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return string.Empty;
            }
            set
            {
                if (this.LastPageImageUrl != value)
                {
                    this.ViewState["LastPageImageUrl"] = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 获取或设置为最后一页按钮显示的文字。默认值为 尾页
        /// </summary>
        [Category("Appearance"),
        Description("获取或设置为最后一页按钮显示的文字。默认值为 尾页"),
        DefaultValue("尾页"),
        NotifyParentProperty(true)]
        public string LastPageText
        {
            get
            {
                object obj2 = this.ViewState["LastPageText"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return "尾页";
            }
            set
            {
                if (this.LastPageText != value)
                {
                    this.ViewState["LastPageText"] = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 获取或设置当鼠标指针悬停在最后一页按钮上时显示的文本。默认值为 转到最后一页
        /// </summary>
        [Category("Appearance"),
        Description("获取或设置当鼠标指针悬停在最后一页按钮上时显示的文本。默认值为 转到最后一页"),
        DefaultValue("转到最后一页"),
        NotifyParentProperty(true)]
        public string LastPageToolTipText
        {
            get
            {
                object obj2 = this.ViewState["LastPageToolTipText"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return "转到最后一页";
            }
            set
            {
                if (this.LastPageToolTipText != value)
                {
                    this.ViewState["LastPageToolTipText"] = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 获取或设置为下一页按钮显示的图像的 URL。为下一页按钮显示的图像的 URL。默认值为空字符串 ("")，表示尚未设置。
        /// </summary>
        [NotifyParentProperty(true),
        Editor(typeof(ImageUrlEditor), typeof(UITypeEditor)),
        UrlProperty,
        DefaultValue(""),
        Category("Appearance"),
        Description("获取或设置为下一页按钮显示的图像的 URL。为下一页按钮显示的图像的 URL。默认值为空字符串，表示尚未设置。")]
        public string NextPageImageUrl
        {
            get
            {
                object obj2 = this.ViewState["NextPageImageUrl"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return string.Empty;
            }
            set
            {
                if (this.NextPageImageUrl != value)
                {
                    this.ViewState["NextPageImageUrl"] = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 获取或设置为下一页按钮显示的文字。默认值为 下页
        /// </summary>
        [Category("Appearance"),
        Description("获取或设置为下一页按钮显示的文字。默认值为 下页"),
        DefaultValue("下页"),
        NotifyParentProperty(true)]
        public string NextPageText
        {
            get
            {
                object obj2 = this.ViewState["NextPageText"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return "下页";
            }
            set
            {
                if (this.NextPageText != value)
                {
                    this.ViewState["NextPageText"] = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 获取或设置当鼠标指针悬停在下一页按钮上时显示的文本。默认值为 转到下一页
        /// </summary>
        [Category("Appearance"),
        Description("获取或设置当鼠标指针悬停在下一页按钮上时显示的文本。默认值为 转到下一页"),
        DefaultValue("转到下一页"),
        NotifyParentProperty(true)]
        public string NextPageToolTipText
        {
            get
            {
                object obj2 = this.ViewState["NextPageToolTipText"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return "转到下一页";
            }
            set
            {
                if (this.NextPageToolTipText != value)
                {
                    this.ViewState["NextPageToolTipText"] = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 获取或设置为上一页按钮显示的图像的 URL。为上一页按钮显示的图像的 URL。默认值为空字符串 ("")，表示尚未设置。
        /// </summary>
        [NotifyParentProperty(true),
        Editor(typeof(ImageUrlEditor), typeof(UITypeEditor)),
        UrlProperty,
        DefaultValue(""),
        Category("Appearance"),
        Description("获取或设置为上一页按钮显示的图像的 URL。为上一页按钮显示的图像的 URL。默认值为空字符串，表示尚未设置。")]
        public string PreviousPageImageUrl
        {
            get
            {
                object obj2 = this.ViewState["PreviousPageImageUrl"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return string.Empty;
            }
            set
            {
                if (this.PreviousPageImageUrl != value)
                {
                    this.ViewState["PreviousPageImageUrl"] = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 获取或设置为上一页按钮显示的文字。默认值为 上页
        /// </summary>
        [Category("Appearance"),
        Description("获取或设置为上一页按钮显示的文字。默认值为 上页"),
        DefaultValue("上页"),
        NotifyParentProperty(true)]
        public string PreviousPageText
        {
            get
            {
                object obj2 = this.ViewState["PreviousPageText"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return "上页";
            }
            set
            {
                if (this.PreviousPageText != value)
                {
                    this.ViewState["PreviousPageText"] = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 获取或设置当鼠标指针悬停在上一页按钮上时显示的文本。默认值为 转到上一页
        /// </summary>
        [Category("Appearance"),
        Description("获取或设置当鼠标指针悬停在上一页按钮上时显示的文本。默认值为 转到上一页"),
        DefaultValue("转到上一页"),
        NotifyParentProperty(true)]
        public string PreviousPageToolTipText
        {
            get
            {
                object obj2 = this.ViewState["PreviousPageToolTipText"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return "转到上一页";
            }
            set
            {
                if (this.PreviousPageToolTipText != value)
                {
                    this.ViewState["PreviousPageToolTipText"] = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 获取或设置页导航中显示的页按钮的数量。默认值为 10
        /// </summary>
        [DefaultValue(10), 
        NotifyParentProperty(true),
        Description("获取或设置页导航中显示的页按钮的数量。默认值为 10"),
        Category("Appearance")]
        public int PageButtonCount
        {
            get
            {
                object obj2 = this.ViewState["PageButtonCount"];
                if (obj2 != null)
                {
                    return (int)obj2;
                }
                return 10;
            }
            set
            {
                if (value < 1)
                {
                    value = 1;
                }
                if (this.PageButtonCount != value)
                {
                    this.ViewState["PageButtonCount"] = value;
                    this.OnPropertyChanged();
                }
            }
        }

        #region IStateManager 成员

        private bool _isTracking;
        private StateBag _viewState = new StateBag();

        bool IStateManager.IsTrackingViewState
        {
            get
            {
                return this._isTracking;
            }
        }

        void IStateManager.LoadViewState(object state)
        {
            if (state != null)
            {
                ((IStateManager)this.ViewState).LoadViewState(state);
            }
        }

        object IStateManager.SaveViewState()
        {
            return ((IStateManager)this.ViewState).SaveViewState();
        }

        void IStateManager.TrackViewState()
        {
            this._isTracking = true;
            ((IStateManager)this.ViewState).TrackViewState();
        }

        StateBag ViewState
        {
            get
            {
                return this._viewState;
            }
        }

        #endregion
    }
}
