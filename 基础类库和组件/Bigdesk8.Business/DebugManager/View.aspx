<%@ Page Language="C#" MasterPageFile="../BusinessSite.Master" AutoEventWireup="true"
    CodeBehind="View.aspx.cs" Inherits="Bigdesk8.Business.DebugManager.View" Title="调试跟踪 - 查看页面" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Master_Content" runat="server">
    <table cellspacing="5" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                您的位置：<span style="color: Blue">调试跟踪 - 查看页面</span>
            </td>
        </tr>
        <tr>
            <td height="1" bgcolor="#999999">
            </td>
        </tr>
        <tr>
            <td height="5">
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="5" cellpadding="0">
                    <tr>
                        <td style="background: #666">
                            <table width="100%" border="0" cellspacing="1" cellpadding="5">
                                <tr>
                                    <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                                        系统名称
                                    </td>
                                    <td style="background: #fff; height: 30px; width: 35%">
                                        <Bigdesk8:DBText ID="DBText13" runat="server" ItemName="SystemName"></Bigdesk8:DBText>
                                    </td>
                                    <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                                        模块名称
                                    </td>
                                    <td style="background: #fff; height: 30px; width: 35%">
                                        <Bigdesk8:DBText ID="DBText14" runat="server" ItemName="ModuleName" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                                        种类名称
                                    </td>
                                    <td style="background: #fff; height: 30px; width: 35%">
                                        <Bigdesk8:DBText ID="tb_xmmc" runat="server" ItemName="CategoryName"></Bigdesk8:DBText>
                                    </td>
                                    <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                                    </td>
                                    <td style="background: #fff; height: 30px; width: 35%">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                                        调试消息
                                    </td>
                                    <td style="background: #fff; height: 30px; width: 35%">
                                        <Bigdesk8:DBText ID="tb_jsdw" runat="server" ItemName="DebugMessage"></Bigdesk8:DBText>
                                    </td>
                                    <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                                        调试日期时间
                                    </td>
                                    <td style="background: #fff; height: 30px; width: 35%">
                                        <Bigdesk8:DBText ID="DBText10" runat="server" ItemName="DebugDateTime" ItemType="DateTime"
                                            ItemFormat="yyyy-MM-dd HH:mm:ss"></Bigdesk8:DBText>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                                        调试详细信息
                                    </td>
                                    <td colspan="3" style="background: #fff; height: 30px">
                                        <Bigdesk8:DBMemo ID="DBMemoBox1" runat="server" ItemName="DebugText"></Bigdesk8:DBMemo>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
