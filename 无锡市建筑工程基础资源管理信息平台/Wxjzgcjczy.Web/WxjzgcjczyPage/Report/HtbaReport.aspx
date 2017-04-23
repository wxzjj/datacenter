<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HtbaReport.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Report.HtbaReport" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-form.css" rel="stylesheet"
        type="text/css" />
    <link href="../../App_Themes/MunSupervisionProject_Theme/Style.css" rel="stylesheet"
        type="text/css" />
    <link href="../../App_Themes/Themes_Standard/Stylesheet1.css" rel="Stylesheet" type="text/css" />

    <script src="../../LigerUI/jquery/jquery-1.5.2.min.js" type="text/javascript"></script>

    <link href="../../LigerUI/css/common.css" rel="stylesheet" type="text/css" />

    <script src="../../SparkClient/jquery.ui-1.8.2.min.js" type="text/javascript"></script>

    <link href="../../SparkClient/jquery.ui-1.8.2.css" rel="stylesheet" type="text/css" />

    <script src="../../SparkClient/Calendar.js" type="text/javascript"></script>

    <script src="../../SparkClient/control.js" type="text/javascript"></script>

    <style type="text/css">
        .tdtext
        {
            text-align: center;
            background-color: White;
            padding-left: 5px;
            padding-right: 5px;
            color: #333333;
            height: 25px;
        }
    </style>
</head>
<body style="background-color: #EEEEEE;">
    <form runat="server" id="formsearch" class="l-form" style="padding: 0px 0px 0px 0px;
    margin: 1px;">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellspacing="1" width="100%" align="center" border="0" class="table-bk">
        <tr>
            <td class="td-text" width="15%">
                合同签订日期
            </td>
            <td class="td-value" colspan="3" width="35%">
                <Bigdesk8:DBDatePicker ID="ContractDate1" Width="150px" ItemRelation="GreaterThanOrEqual"
                    runat="server"></Bigdesk8:DBDatePicker>
                至
                <Bigdesk8:DBDatePicker ID="ContractDate2" Width="150px" ItemRelation="LessThanOrEqual"
                    runat="server"></Bigdesk8:DBDatePicker>
            </td>
        </tr>
        <tr>
            <td class="td_text" width="15%">
                合同类别
            </td>
            <td class="td_value" colspan="3" width="35%">
                <Bigdesk8:DBCheckBoxList ID="cbl_Htlb" runat="server" RepeatColumns="10" ForeColor="Black">
                </Bigdesk8:DBCheckBoxList>
            </td>
        </tr>
        <tr>
            <td class="td-text" width="15%">
                项目属地
            </td>
            <td class="td-value" width="35%" colspan="3">
                <asp:CheckBoxList ID="cbl_ssdq" runat="server" RepeatColumns="11">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td style="background-color: White" colspan="6">
                <table width="100%" cellpadding="2" cellspacing="0" border="0">
                    <tr>
                        <td style="width: 5%; text-align: right; padding-left: 140px;">
                            <asp:Button ID="btnReport" runat="server" class="td-text" Text="统计汇总" OnClick="btnReport_Click"
                                Height="26px" BackColor="White" />
                        </td>
                        <td style="width: 15%;">
                        </td>
                        <td style="width: 5%; text-align: right">
                        </td>
                        <td style="width: 35%">
                        </td>
                        <td style="text-align: right;">
                            <asp:Button ID="btnDc" runat="server" class="td-text" Text="导出PDF" Height="26px"
                                BackColor="White" OnClick="btnDc_Click" />
                        </td>
                        <td style="width: 10%; padding-right: 10px; text-align: right;">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table cellspacing="1" width="100%" align="center" border="0" class="table-bk" style="height: 300px;">
                            <tr>
                                <td rowspan="5" class="tdtext" style="width: 20%;">
                                    江苏省内企业在锡合同备案统计
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    行业
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    项目数(个数)
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    合同金额(万元)
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    总投资(万元)
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    总面积(平方米)
                                </td>
                            </tr>
                            <tr>
                                <td class="tdtext" style="width: 16%;">
                                    建筑工程
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText7" ItemName="SnqyJzgcXmCount"></Bigdesk8:DBText>
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText8" ItemName="SnqyJzgcHtje"></Bigdesk8:DBText>
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText9" ItemName="SnqyJzgcZtz"></Bigdesk8:DBText>
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText10" ItemName="SnqyJzgcZmj"></Bigdesk8:DBText>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdtext" style="width: 16%;">
                                    勘察
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText12" ItemName="SnqyKcXmCount"></Bigdesk8:DBText>
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText13" ItemName="SnqyKcHtje"></Bigdesk8:DBText>
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText14" ItemName="SnqyKcZtz"></Bigdesk8:DBText>
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText15" ItemName="SnqyKcZmj"></Bigdesk8:DBText>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdtext" style="width: 16%;">
                                    市政
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText17" ItemName="SnqySzXmCount"></Bigdesk8:DBText>
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText18" ItemName="SnqySzHtje"></Bigdesk8:DBText>
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText19" ItemName="SnqySzZtz"></Bigdesk8:DBText>
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText20" ItemName="SnqySzZmj"></Bigdesk8:DBText>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdtext" style="width: 16%;">
                                    合计
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText22" ItemName="SnqyHjXmCount"></Bigdesk8:DBText>
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText23" ItemName="SnqyHjHtje"></Bigdesk8:DBText>
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText24" ItemName="SnqyHjZtz"></Bigdesk8:DBText>
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText25" ItemName="SnqyHjZmj"></Bigdesk8:DBText>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdtext" style="width: 16%;" rowspan="4">
                                    省外企业在锡合同备案统计
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    建筑工程
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText2" ItemName="SwqyJzgcXmCount"></Bigdesk8:DBText>
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText3" ItemName="SwqyJzgcHtje"></Bigdesk8:DBText>
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText4" ItemName="SwqyJzgcZtz"></Bigdesk8:DBText>
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText1" ItemName="SwqyJzgcZmj"></Bigdesk8:DBText>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdtext" style="width: 16%;">
                                    勘察
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText5" ItemName="SwqyKcXmCount"></Bigdesk8:DBText>
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText6" ItemName="SwqyKcHtje"></Bigdesk8:DBText>
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText11" ItemName="SwqyKcZtz"></Bigdesk8:DBText>
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText16" ItemName="SwqyKcZmj"></Bigdesk8:DBText>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdtext" style="width: 16%;">
                                    市政
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText21" ItemName="SwqySzXmCount"></Bigdesk8:DBText>
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText26" ItemName="SwqySzHtje"></Bigdesk8:DBText>
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText27" ItemName="SwqySzZtz"></Bigdesk8:DBText>
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText28" ItemName="SwqySzZmj"></Bigdesk8:DBText>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdtext" style="width: 16%;">
                                    合计
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText29" ItemName="SwqyHjXmCount"></Bigdesk8:DBText>
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText30" ItemName="SwqyHjHtje"></Bigdesk8:DBText>
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText31" ItemName="SwqyHjZtz"></Bigdesk8:DBText>
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText32" ItemName="SwqyHjZmj"></Bigdesk8:DBText>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdtext" style="width: 16%;">
                                    合计
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    合计
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText33" ItemName="HjXmCount"></Bigdesk8:DBText>
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText34" ItemName="HjHtje"></Bigdesk8:DBText>
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText35" ItemName="HjZtz"></Bigdesk8:DBText>
                                </td>
                                <td class="tdtext" style="width: 16%;">
                                    <Bigdesk8:DBText runat="server" ID="DBText36" ItemName="HjZmj"></Bigdesk8:DBText>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
