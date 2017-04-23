<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zljdzs.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Szgc.Zljdzs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查看安全监督项目信息</title>
    <style type="text/css">
        .td_css_center
        {
            background-color: #FFFFCC;
            font-size: 14px;
            text-align: center;
        }
        .td_css_left
        {
            background-color: #FFFFCC;
            text-align: left;
            font-size: 14px;
        }
    </style>
</head>
<body style="padding: 0px; margin-top: 2px;">
    <form id="form1" runat="server">
    <table cellspacing="1" cellpadding="0" width="100%" align="center" border="2" style="background-color: #FFFFCC;
        padding: 2px; border-color: #FF6600;">
        <tr>
            <td bgcolor="#FFFFCC" align="center">
                <table border="0" cellpadding="2" cellspacing="2" width="100%">
                    <tr>
                        <td width="10%">
                        </td>
                        <td width="35%">
                            <table width="100%" style="text-align: center" cellpadding="10">
                                <tr>
                                    <td align="right" style="font-size: 14px;">
                                        监督注册号：<span style="border-bottom: #666666  solid 1px;"> &nbsp;
                                            <%=zljdbh%>
                                            &nbsp;</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_left">
                                        <span style="border-bottom: #666666  solid 1px;">&nbsp;<%=sgdw %>&nbsp;</span>：
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_left">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;你单位建设的 <span style="border-bottom: #666666  solid 1px;">
                                            &nbsp;
                                            <%=xmmc %>&nbsp;</span> 工程，根据国务院
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_left">
                                        《建设工程质量管理条例》&nbsp;和有关规定，&nbsp;现准予办理工程质量监督
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_left">
                                        手续。
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_left">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;我站将依据国家有关法律、法规和工程建设强制性标准等规定，
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_left">
                                        对责任主体履行质量责任的行为、工程实体质量、混凝土预制构件及
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_left">
                                        预拌混凝土质量、有关工程质量的技术文件和资料进行监督检查和监
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_left">
                                        督巡查；&nbsp;&nbsp;对工程竣工验收实施监督；&nbsp;&nbsp;对责任主体违法违规行为进行
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_left">
                                        调查取证和核实，并按委托权限实施行政处罚。
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_left">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;请按照《建设工程质量责任主体质量行为资料》所列内容做好准
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_left">
                                        备，接受监督检查。
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_left">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;我站将在15个工作日内向工程参建各方进行&nbsp;《工程质量监督工
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_left">
                                        作方案》交底，请你单位组织建设项目负责人、项目经理、总监理工
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_left">
                                        程师和其他相关人员参加。
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_left">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;欢迎对我站及工作人员的工作进行监督，监督电话<span style="border-bottom: #666666  solid 1px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>。
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_left">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;联&nbsp;系&nbsp;人：<span style="border-bottom: #666666  solid 1px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>。
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_left">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;联系电话：<span style="border-bottom: #666666  solid 1px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>。
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="font-size: 14px;">
                                        <span style="border-bottom: #666666  solid 1px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>质量监督站&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="font-size: 14px;">
                                        年&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="10%">
                        </td>
                        <td width="35%">
                            <table cellspacing="1" cellpadding="5" width="100%" align="center" border="0" style="background-color: Black">
                                <tr>
                                    <td class="td_css_center" width="10%">
                                        序号
                                    </td>
                                    <td class="td_css_center" width="10%">
                                        单位
                                    </td>
                                    <td class="td_css_center" width="80%">
                                        资&nbsp;料&nbsp;名&nbsp;称
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_center">
                                        1
                                    </td>
                                    <td class="td_css_center" rowspan="7">
                                        建<br />
                                        <br />
                                        <br />
                                        设<br />
                                    </td>
                                    <td class="td_css_left">
                                        建设工程规划许可证
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_center">
                                        2
                                    </td>
                                    <td class="td_css_left">
                                        岩土工程勘察及施工图设计文件审查意见书、批准书
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_center">
                                        3
                                    </td>
                                    <td class="td_css_left">
                                        勘察、设计、监理和施工等单位的中标通知书、合同
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_center">
                                        4
                                    </td>
                                    <td class="td_css_left">
                                        建设工程质量监督申报表、通知书
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_center">
                                        5
                                    </td>
                                    <td class="td_css_left">
                                        建设工程施工许可证
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_center">
                                        6
                                    </td>
                                    <td class="td_css_left">
                                        单位法人对工程项目负责人出具的授权委托书
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_center">
                                        7
                                    </td>
                                    <td class="td_css_left">
                                        现场机构设置及人员基本情况表
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_center">
                                        8
                                    </td>
                                    <td class="td_css_center" rowspan="2">
                                        勘<br />
                                        <br />
                                        察<br />
                                    </td>
                                    <td class="td_css_left">
                                        资质证书
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_center">
                                        9
                                    </td>
                                    <td class="td_css_left">
                                        项目负责人执业资格证书
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_center">
                                        10
                                    </td>
                                    <td class="td_css_center" rowspan="3">
                                        设<br />
                                        <br />
                                        计<br />
                                    </td>
                                    <td class="td_css_left">
                                        资质证书
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_center">
                                        11
                                    </td>
                                    <td class="td_css_left">
                                        项目负责人以及建筑、结构设计人员执业资格证书
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_center">
                                        12
                                    </td>
                                    <td class="td_css_left">
                                        设计图纸会审、交底记录
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_center">
                                        13
                                    </td>
                                    <td class="td_css_center" rowspan="5">
                                        监<br />
                                        <br />
                                        <br />
                                        理<br />
                                    </td>
                                    <td class="td_css_left">
                                        资质证书
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_center">
                                        14
                                    </td>
                                    <td class="td_css_left">
                                        现场机构设计及人员基本情况表
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_center">
                                        15
                                    </td>
                                    <td class="td_css_left">
                                        监理工程师岗位证书、监理人员上岗证、监理单位对总监工程师出具的授权委托书、总监理工程师变更手续
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_center">
                                        16
                                    </td>
                                    <td class="td_css_left">
                                        见证取样人员上岗证
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_center">
                                        17
                                    </td>
                                    <td class="td_css_left">
                                        监理规划、细则、旁站监理方案
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_center">
                                        18
                                    </td>
                                    <td class="td_css_center" rowspan="5">
                                        施<br />
                                        <br />
                                        <br />
                                        工<br />
                                    </td>
                                    <td class="td_css_left">
                                        资质证书
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_center">
                                        19
                                    </td>
                                    <td class="td_css_left">
                                        项目经理、质量检查员、特殊工种等人员执业资格或上岗证、项目经理变更手续
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_center">
                                        20
                                    </td>
                                    <td class="td_css_left">
                                        质保体系、现场机构设置及人员基本情况表
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_center">
                                        21
                                    </td>
                                    <td class="td_css_left">
                                        质量管理制度
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_center">
                                        22
                                    </td>
                                    <td class="td_css_left">
                                        经审批的施工组织设计和施工方案
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_center">
                                        23
                                    </td>
                                    <td class="td_css_center" rowspan="2">
                                        检
                                        <br />
                                        <br />
                                        测<br />
                                    </td>
                                    <td class="td_css_left">
                                        资质证书
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_css_center">
                                        24
                                    </td>
                                    <td class="td_css_left">
                                        建设单位委托合同
                                    </td>
                                </tr>
                            </table>
                            <table cellspacing="1" cellpadding="2" width="100%" align="center" border="0">
                                <tr>
                                    <td align="left" style="font-family: 楷体; vertical-align: top; font-size: 11px" width="10%">
                                        注：1、
                                    </td>
                                    <td align="left" style="font-family: 楷体; font-size: 11px" width="90%">
                                        本表所列资料由监理（建设）单位收集并统一保管，存放现场备查；所有变化应及时办理相关手续。
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="vertical-align: top; font-family: 楷体; font-size: 11px" width="10%">
                                        &nbsp;&nbsp;&nbsp;&nbsp;2、
                                    </td>
                                    <td align="left" style="font-family: 楷体; font-size: 11px" width="90%">
                                        所提供的资料为加盖印章相应单位法人章的复印件，并注明原件存放地点，以备抽查。
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="10%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
