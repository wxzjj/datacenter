<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zhjg_Jgba_View.aspx.cs"
    Inherits="IntegrativeShow2.SysFiles.PagesZhjg.Zhjg_Jgba_View" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../App_Themes/Themes_Standard/Stylesheet1.css" rel="Stylesheet" type="text/css" />
</head>
<body class="body_haveBackImg">
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <!-- 竣工备案基本信息 -->
        <tr>
            <td class="view_head" style="height: 25px; vertical-align: bottom">
                <img src="../Images/TitleImgs/Title_jgbaxx.gif" height="25px" alt="" />
            </td>
        </tr>
        <tr>
            <td class="view_center">
                <table cellspacing="1" cellpadding="0" width="100%" align="center" border="0" class="table">
                    <tr>
                        <td class="td_text" width="15%">
                            项目编号
                        </td>
                        <td class="td_value" colspan="3">
                            <Bigdesk8:DBText ID="PrjNum" ItemName="PrjNum" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            竣工备案编号
                        </td>
                        <td class="td_value" colspan="3">
                            <Bigdesk8:DBText ID="PrjFinishNum" ItemName="PrjFinishNum" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            施工许可证编号
                        </td>
                        <td class="td_value" width="35%" colspan="3">
                            <Bigdesk8:DBText ID="BuilderLicenceNum" ItemName="BuilderLicenceNum" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            备案项目名称
                        </td>
                        <td class="td_value" width="35%" colspan="3">
                            <Bigdesk8:DBText ID="PrjFinishName" ItemName="PrjFinishName" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            质量检测机构名称
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="QCCorpName" ItemName="QCCorpName" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            质量检测机构组织机构代码<br />
                            （社会信用代码）
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="QCCorpCode" ItemName="QCCorpCode" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            实际造价（万元）
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="FactCost" ItemName="FactCost" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            实际面积（平方米）
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="FactArea" ItemName="FactArea" FieldType="Date" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            实际开工日期
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="BDate" ItemName="BDate" FieldType="Date" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            实际竣工验收日期
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="EDate" ItemName="EDate" FieldType="Date" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            结构体系
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="PrjStructureType" ItemName="PrjStructureType" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                        </td>
                        <td class="td_value" width="35%">
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            实际建设规模
                        </td>
                        <td class="td_value" width="35%" colspan="3">
                            <Bigdesk8:DBText ID="FactSize" ItemName="FactSize" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            备注
                        </td>
                        <td class="td_value" width="35%" colspan="3">
                            <Bigdesk8:DBText ID="Mark" ItemName="Mark" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
