using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bigdesk8.Web.Controls
{
    /// <summary>
    /// <see cref="SubmitButton"/> 提交数据按钮(可防止重复点击)，可作为保存，更新等操作按钮
    /// </summary>
    [ToolboxData("<{0}:SubmitButton runat=server></{0}:SubmitButton>")]
    public class SubmitButton : Button
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SubmitButton()
            : base()
        {
            this.Attributes["onclick"] = "this.style.width=150;this.value='正在处理，请稍候...';this.disabled=true;";
        }

        #endregion 构造函数

        /// <summary>
        /// 本控件使用 ASP.NET 回发机制。返回值总是 false;
        /// </summary>
      
        public override bool UseSubmitBehavior
        {
            get
            {
                if (base.UseSubmitBehavior)
                    base.UseSubmitBehavior = false;

                return base.UseSubmitBehavior;
            }
            set
            { }
        }
    }
}
