using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Bigdesk8.Business;

namespace IntegrativeShow2
{
    /// <summary>
    /// 数据库名称枚举
    /// </summary>
    public enum DataBaseName
    { 
        ///// <summary>
        ///// 苏州住建局集群库
        ///// </summary>
        //SCIC60, 

        ///// <summary>
        ///// 苏州诚信监管平台；
        ///// 存放12类企业人员相关信息
        ///// </summary>
        //DBSZHCACredit,

        ///// <summary>
        ///// 本系统中用于提取用户信息
        ///// </summary>
        //WebPlat50,

        /// <summary>
        /// 本系统项目信息信息
        /// </summary>
        WJSJZX
    }

    public enum InstanceName
    {
        /// <summary>
        /// 读取安全报监详细信息
        /// </summary>
        Instance_Read_AqbjInfo,

        /// <summary>
        /// 读取合同备案详细信息
        /// </summary>
        Instance_Read_HtbaInfo,

        /// <summary>
        /// 读取竣工备案详细信息
        /// </summary>
        Instance_Read_JgbaInfo,

        /// <summary>
        /// 读取招标投标详细信息
        /// </summary>
        Instance_Read_ZbtbInfo,

        /// <summary>
        /// 读取立项项目详细信息
        /// </summary>
        Instance_Read_LxxmInfo,

        /// <summary>
        /// 读取立项项目单项工程详细信息
        /// </summary>
        Instance_Read_LxxmDxgcInfo,

        /// <summary>
        /// 读取施工图审查详细信息
        /// </summary>
        Instance_Read_SgtscInfo,

        /// <summary>
        /// 读取施工许可详细信息
        /// </summary>
        Instance_Read_SgxkzInfo,

        /// <summary>
        /// 读取质量报监详细信息
        /// </summary>
        Instance_Read_ZlbjInfo,

        /// <summary>
        /// 读取安监申报表详细信息
        /// </summary>
        Instance_Read_AqbjNewInfo,

        /// <summary>
        /// 读取质监申报表详细信息
        /// </summary>
        Instance_Read_ZlbjNewInfo,
  
    }

    public enum BeFrom
    { 
        //立项项目登记Menu
        Zhjg_Lxxmdj_Menu=0,
        Zhjg_Menu=1
    }

    /// <summary>
    /// 用户类型
    /// </summary>
    public enum UserType
    {
        未定义 = 0,
        //
        系统管理员 = 10,
        //
        管理用户 = 20,
        //
        代理机构 = 30,
        //
        实施单位 = 40,

        监理单位 = 50,

        申报部门 = 60,

    }
    /// <summary>
    /// 应用系统用户信息结构体
    /// </summary>
    [Serializable]
    public struct AppUser
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID;
        /// <summary>
        /// 用户登录名
        /// </summary>
        public string LoginName;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName;

        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public string LastLoginTime;
        /// <summary>
        /// 用户类型
        /// </summary>
        public UserType UserType;

        /// <summary>
        /// 企业ID
        /// </summary>
        public string qyID;
        /// <summary>
        /// 组织机构代码
        /// </summary>
        public string zzjgdm;
    }

}
