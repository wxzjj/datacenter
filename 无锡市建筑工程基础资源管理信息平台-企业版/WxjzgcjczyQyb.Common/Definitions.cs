using System;
using Bigdesk8.Business;

/*
 * 采用枚举取代常量的原因主要是，枚举更严格些，更有助于保证程序逻辑的严谨性。
 * 
 * 特别注意！！！！！！！！！！！
 * 1. 各枚举类型中的枚举项都必须明确地定义值，不允许采用0，1，2，。。。这种自然递增的赋值方式；
 * 2. 一旦系统投入实际运行，修改已有枚举项的定义（包括枚举项的名称和值）时要特别当心，以防搞乱数据库中已有数据的标识字段的含义。
 *    可能的情况下，不要修改。但可以增加枚举项的定义。
 */

namespace WxjzgcjczyQyb
{
    /// <summary>
    /// 数据状态
    /// </summary>
    public enum DataState
    {
        新数据 = 0,
        已上报 = 1,
        已打回 = 2,
        审核通过 = 3,
        变更中 = 4,
        变更待审 = 5,
        已删除 = -1
    }

    /// <summary>
    /// 统一基础代码表中的代码类型
    /// </summary>
    public enum CodeType
    {
        空父代码类型,
        模块代码表,
        所属地区,
        所属地区除去外省,
        通讯录类别,
        人员科室,
        人员职务,
        任务类型,
        任务优先级,
        备案录附件
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
        /// </summary>
        /// 用户类型
        /// </summary>
        public UserType UserType;

    }

    /// <summary>
    /// 用户类型
    /// </summary>
    public enum UserType
    {
        未定义 = 0,
        //系统管理员
        管理用户 = 10,
        //管理用户
        建设单位 = 20,
        //施工企业
        施工单位 = 30,
    }

    /// <summary>
    /// 业务模块，系统功能模块
    /// </summary>
    public enum ModuleCode
    {
        /// <summary>
        /// 任务登记
        /// </summary>
        RWDJ = 1,

        /// <summary>
        /// 任务分配
        /// </summary>
        RWFP = 2,

        /// <summary>
        /// 用户管理
        /// </summary>
        YHGL = 3,

        /// <summary>
        /// 角色管理
        /// </summary>
        JSGL = 4


    }

    public enum BeFrom
    {
        //立项项目登记Menu
        Zhjg_Lxxmdj_Menu = 0,
        Zhjg_Menu = 1
    }

    public enum OperateCode
    {
        /// <summary>
        /// 添加
        /// </summary>
        CREATE = 10,

   
        /// <summary>
        /// 分配科室
        /// </summary>
        FPBM = 20,

    }

}