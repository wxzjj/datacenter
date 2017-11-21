using System;
using Bigdesk8.Business;
using Wxjzgcjczy.Common;
using System.Collections.Generic;

/*
 * 采用枚举取代常量的原因主要是，枚举更严格些，更有助于保证程序逻辑的严谨性。
 * 
 * 特别注意！！！！！！！！！！！
 * 1. 各枚举类型中的枚举项都必须明确地定义值，不允许采用0，1，2，。。。这种自然递增的赋值方式；
 * 2. 一旦系统投入实际运行，修改已有枚举项的定义（包括枚举项的名称和值）时要特别当心，以防搞乱数据库中已有数据的标识字段的含义。
 *    可能的情况下，不要修改。但可以增加枚举项的定义。
 */

namespace Wxjzgcjczy
{
    /// <summary>
    /// 操作状态枚举
    /// </summary>
    /// 一般对应表结构中的字段OperateState
    public enum OperateState
    {
        /// <summary>
        /// 无可行操作
        /// </summary>
        NoFeasibleAction = 0,
        /// <summary>
        /// 等待生成
        /// </summary>
        WaitingCreate = 10,

        /// <summary>
        /// 等待提交
        /// </summary>
        WaitingSubmit = 50,

        /// <summary>
        /// 等待一审
        /// </summary>
        WaitingFirstCheck = 100,

        /// <summary>
        /// 等待二审
        /// </summary>
        WaitingSecondCheck = 200,

        /// <summary>
        /// 等待三审
        /// </summary>
        WaitingThirdCheck = 400,

        /// <summary>
        /// 业务结束
        /// </summary>
        Completed = 999,
    }
    /// <summary>
    /// 编辑类型
    /// </summary>
    public enum OperateSt
    {
        add,
        edit
    }

    /// <summary>
    /// 数据状态枚举
    /// </summary>
    /// 一般对应表结构中的字段DataState
    public enum DataState
    {
        /// <summary>
        /// 新数据
        /// </summary>
        New = 0,

        /// <summary>
        /// 已提交(已申报)
        /// </summary>
        Submited = 10,
     
        /// <summary>
        /// 已撤销提交（撤销申报）
        /// </summary>
        SubmitCancelled = -10,

        /// <summary>
        /// 审核通过（通过预审）
        /// </summary>
        CheckPast = 20,
        /// <summary>
        /// 审核不通过
        /// </summary>
        CheckRejected = -20,

        /// <summary>
        /// 进入实施状态,项目启动状态
        /// </summary>
        FirstCheckPast = 40,

        /// <summary>
        /// 一审不通过
        /// </summary>
        FirstCheckRejected = -100,

        /// <summary>
        /// 一审已撤销
        /// </summary>
        FirstCheckCancelled = 110,

        /// <summary>
        /// 二审通过
        /// </summary>
        SecondCheckPast = 200,

        /// <summary>
        /// 二审不通过
        /// </summary>
        SecondCheckRejected = -200,

        /// <summary>
        /// 二审已撤销
        /// </summary>
        SecondCheckCancelled = 210,

        /// <summary>
        /// 三审通过
        /// </summary>
        ThirdCheckPast = 400,

        /// <summary>
        /// 三审不通过
        /// </summary>
        ThirdCheckRejected = -400,

        /// <summary>
        /// 三审已撤销
        /// </summary>
        ThirdCheckCancelled = 410,

        /// <summary>
        /// 已删除
        /// </summary>
        Deleted = -1,
    }

    /// <summary>
    /// 代码表中的代码类型
    /// </summary>
    public enum CodeType
    {
        空父代码类型 = 0,
        数据状态 = 10,
        是否 = 20,
        企业经济性质 = 40,
        企业资质等级 = 60,
        企业资质序列 = 30,
        人员职务 = 70,
        人员职称 = 80,
        企业证书类型 = 90,
        人员学历 = 100,
        人员执业资格类型 = 110,
        人员附件类别 = 120,
        人员资质等级 = 130,
        企业变更事项 = 140,
        人员变更事项 = 150,
        项目附件类别 = 160,
        人员职称专业 = 170,
        企业市域类别 = 180,
        所属城市 = 190,
        所属地区 = 200,
      
        层次=210,
        结构=220,
        投资类型 =230,
        专业分类 =240, 
        使用类别=250,
        企业从事业务类型=260,
        项目负责人资质等级,
        政令畅通人员
    }
    /// <summary>
    /// 工程变更类型
    /// </summary>
    public enum Gcbglx
    {
        现场经济签证变更,
        技术核定变更,
        材料认价变更,
        设计变更
    }
    /// <summary>
    /// 业务阶段
    /// </summary>
    public enum Ywjd
    {
        计划立项阶段,
        建设程序阶段,
        工程施工阶段,
        竣工验收阶段
    }
    /// <summary>
    /// 业务类型
    /// </summary>
    public enum Ywlx
    {
        项目建议,
        初步设计,
        可行性研究,
        环境评估,
        规划选址,
        建设用地,
        项目立项,
        投资评审,
        会议纪要,
        项目规费,
        图纸审查,
        招标投标,
        合同备案,
        施工许可,
        竣工备案,
        工程变更,
        工程款支付,
        施工进度,
        动态报告,
        分部验收,
        竣工验收,
        竣工移交,
        竣工结算
    }
    /// <summary>
    /// 文件名称
    /// </summary>
    public enum Wjmc
    {
        项目建议书,
        项目建议书批复,
        初步设计送审申请表,
        初步设计批复,
        初步设计方案,
        可行性研究报告,
        可行性研究报告审批意见,
        环境影响报告书,
        环境影响评价批复意见,
        选址申请报告,
        选址规划意见书,
        工程规划许可证,
        用地规划许可证,
        用地申请报告,
        建设用地批准书,
        国有土地使用证,
        项目立项申请报告,
        项目立项批复,
        关于立项的有关会议纪要,
        投资评审报告,
        工程水文地质勘察报告,
        地形测量成果报告,
        初步设计图纸及说明,
        技术设计图纸及说明,
        施工图含设计计算书及其说明,
        设计方案审查意见书,
        施工图审查合格证书,
        施工图纸自审记录,
        施工图纸会审意见,
        招标文件,
        投标文件,
        工程量清单报价,
        中标通知书,
        合同备案表,
        合同电子附件,
        安全监督通知书,
        质量监督通知书,
        工程施工许可证,
        建设单位项目管理机构及管理人员名单,
        施工单位项目管理机构及管理人员名单,
        监理单位项目管理机构及管理人员名单,
        竣工验收备案表,
        专项验收认可文件,
        工程变更令,
        设计变更通知书,
        现场签证单,
        技术核定单,
        价格材料回执单,
        工程款支付申请单,
        工程款支付发票,
        施工进度计划表,
        建设单位日报,
        施工单位月报,
        监理单位月报,
        初验工作报告和计划安排,
        分部验收报告,
        单位工程质量验收记录,
        竣工验收报告,
        竣工验收证明书,
        工程质量保修书,
        工程项目移交报告,
        交付使用财产总表,
        财产明细表,
        竣工决算书,
        竣工决算送审表,
        工程款支付情况表,
        工人工资支付情况表,
        结算审核成果备案表,
        工程造价咨询报告书,
        工程造价咨询合同,
        工程结算审定单,
        会议纪要,
        项目规费材料

    }

    /// <summary>
    /// 可进行哪些撤销操作的枚举
    /// </summary>
    /// 一般对应表结构中的字段CancelState
    public enum CancelState
    {
        /// <summary>
        /// 无可撤销，没有动作可撤销
        /// </summary>
        NoActionToCancel = 0,

        /// <summary>
        /// 等待撤销提交
        /// </summary>
        WaitingCancelSubmit = 100,

        /// <summary>
        /// 等待撤销一审
        /// </summary>
        WaitingCancelFirstCheck = 200,

        /// <summary>
        /// 等待撤销二审
        /// </summary>
        WaitingCancelSecondCheck = 300,

        /// <summary>
        /// 等待撤销三审
        /// </summary>
        WaitingCancelThirdCheck = 400,

        /// <summary>
        /// 禁止撤销
        /// </summary>
        Forbidden = 999
    }

    /// <summary>
    /// 审核（撤销审核）结果状态
    /// </summary>
    /// 一般对应表结构中的各阶段的审核结果字段，如Checkbz1
    public enum CheckState
    {
        /// <summary>
        /// 未审核
        /// </summary>
        NotChecked = 0,

        /// <summary>
        /// 审核通过
        /// </summary>
        Past = 100,

        /// <summary>
        /// 审核不通过
        /// </summary>
        Rejected = -100,

        /// <summary>
        /// 已撤销(审核)
        /// </summary>
        Cancelled = -1
    }

    /// <summary>
    /// 操作代码枚举
    /// </summary>
    /// 并不对应业务表的某个字段。而是对一个业务模块的可能的操作的列举，也是权限管理的一个主要维度。
    /// 但并非每个配置项都要在权限管理配置表中配置，如：Retrieve, Update, Delete, CancelSubmit, CancelFirstCheck, 
    /// CancelSecondCheck, CancelThirdCheck就只是用于程序逻辑。
    public enum OperateCode
    {
        /// <summary>
        /// 添加
        /// </summary>
        Create = 10,

        /// <summary>
        /// 查询
        /// </summary>
        Retrieve = 20,

        /// <summary>
        /// 修改
        /// </summary>
        Update = 30,

        /// <summary>
        /// 删除
        /// </summary>
        Delete = 40,

        /// <summary>
        /// 申报
        /// </summary>
        Submit = 50,

        /// <summary>
        /// 撤销申报
        /// </summary>
        CancelSubmit = 60,

        /// <summary>
        /// 审核
        /// </summary>
        FirstCheck = 70,

        /// <summary>
        /// 撤销审核
        /// </summary>
        CancelFirstCheck = 90,

        /// <summary>
        /// 二审
        /// </summary>
        SecondCheck = 100,

        /// <summary>
        /// 撤销二审
        /// </summary>
        CancelSecondCheck = 120,

        /// <summary>
        /// 三审
        /// </summary>
        ThirdCheck = 130,

        /// <summary>
        /// 撤销三审
        /// </summary>
        CancelThirdCheck = 150
    }

    /// <summary>
    /// 业务模块，系统功能模块
    /// </summary>
    public enum ModuleCode
    {
        //超级管理员
        系统角色 = 10,
        用户管理 = 20,
        权限中心 = 30,
        //企业版
        企业信息注册 = 39,
        企业信息填报_基本信息 = 40,
        企业信息填报_企业资质 = 41,
        企业信息填报_主要股东 = 42,
        企业信息填报_管理人员 = 43,
        企业信息填报_各类证书 = 44,
        企业信息填报_企业申报 = 46,

        人员信息填报 = 50,

        企业信息变更 = 60,

        人员信息变更 = 70,

        人员调入申请 = 80,

        工程项目备案 = 90,
        工程项目备案_电子材料 = 91,

        企业资质申请 = 100,
        企业资质备案 = 105,
        企业出市备案 = 106,

        企业综合考评 = 110,

        企业良好信息 = 120,

        企业不良信息 = 130,

        企业证书附件 = 140,
        //管理版
        企业用户管理 = 150,
        企业信息审核 = 160,
        人员信息审核 = 170,
        工程项目信息 = 180,
        一级资质预审 = 190,
        二级资质初审 = 200,
        三级资质审核 = 210,

    }

    /// <summary>
    /// 常用检索角度枚举
    /// </summary>
    public enum WhatToRetrieve
    {
        未定义 = 0,

        我的所有业务 = 5,

        所有未删除 = 8,

        等待申报 = 10,

        已申报 = 40,

        等待审核 = 50,

        /// <summary>
        /// 包括审核通过，审核不通过
        /// </summary>
        已审核 = 80,

        等待二审 = 90,

        /// <summary>
        /// 包括二审通过，二审不通过
        /// </summary>
        已二审 = 120,
    }

    /// <summary>
    /// 当前系统信息
    /// </summary>
    public struct AppInfo
    {
        /// <summary>
        /// 当前应用系统ID
        /// </summary>
        public static int SystemID = Convert.ToInt32(SubSystem.常熟高新区建设项目建设管理系统);

        public static string SystemName = SubSystem.常熟高新区建设项目建设管理系统.ToString();
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

        public List<ModuleOperate> list;
 
    }

    public enum Xmlx
    { 
        监管项目,代建项目,承接项目,全部工程
    }
    /// <summary>
    /// 数据监控数据流向
    /// </summary>

    public enum DataFlow
    {
        省一体化平台到无锡数据中心 = 0, 省审图系统到无锡数据中心 = 1, 省施工许可系统到无锡数据中心 = 2, 省质监系统到无锡数据中心 = 3, 省竣工备案系统到无锡数据中心 = 4,
        局一号通系统到无锡数据中心 = 5, 市勘察设计系统到无锡数据中心 = 6, 市招投标系统到无锡数据中心 = 7, 江阴市招投标系统到无锡数据中心 = 8, 宜兴市招投标系统到无锡数据中心 = 9, 市安监系统 = 10, 市质监系统到无锡数据中心 = 11, 信用考评系统到无锡数据中心 = 12, 惠山区到无锡数据中心 = 13, 滨湖区到无锡数据中心 = 14, 市中心四平台到无锡数据中心 = 15,
        无锡数据中心到市勘察设计系统 = 16, 无锡数据中心到市招投标系统 = 17, 无锡数据中心到江阴市招投标系统 = 18, 无锡数据中心到宜兴市招投标系统 = 19, 无锡数据中心到市安监系统 = 20, 无锡数据中心到市质监系统 = 21, 无锡数据中心到信用考评系统 = 22, 无锡数据中心到惠山区 = 23, 无锡数据中心到滨湖区 = 24, 无锡数据中心到市中心四平台 = 25
    }


    public enum Tag
    {
        江苏建设公共基础数据平台 = 0,
        局一号通系统 = 1,
        无锡市建设工程安全监督站 = 2,
        无锡市勘察设计行业信息管理系统 = 3,
        省一体化平台 = 4,
        省施工许可系统 = 5,
        省竣工备案系统 = 6,
        省质监系统 = 7,
        住建局政务服务网 = 8

    }
   
}