<%@ Page Language="C#" MasterPageFile="../BusinessSite.Master" AutoEventWireup="true"
    CodeBehind="View.aspx.cs" Inherits="Bigdesk8.Business.LogManager.View" Title="系统日志 - 查看页面" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Master_Content" runat="server">
    <table cellspacing="5" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                您的位置：<span style="color: Blue">系统日志 - 查看页面</span>
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
                                        主键标识
                                    </td>
                                    <td style="background: #fff; height: 30px; width: 35%">
                                        <Bigdesk8:DBText ID="tb_jsdw" runat="server" ItemName="KeyString"></Bigdesk8:DBText>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                                        操作
                                    </td>
                                    <td style="background: #fff; height: 30px; width: 35%">
                                        <Bigdesk8:DBText ID="DBText15" runat="server" ItemName="Operation"></Bigdesk8:DBText>
                                    </td>
                                    <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                                        操作信息
                                    </td>
                                    <td style="background: #fff; height: 30px; width: 35%">
                                        <Bigdesk8:DBMemo ID="DBMemoBox1" runat="server" ItemName="MessageInfo"></Bigdesk8:DBMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                                        操作前状态
                                    </td>
                                    <td style="background: #fff; height: 30px; width: 35%">
                                        <Bigdesk8:DBText ID="DBText17" runat="server" ItemName="PriorStatus"></Bigdesk8:DBText>
                                    </td>
                                    <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                                        操作后状态
                                    </td>
                                    <td style="background: #fff; height: 30px; width: 35%">
                                        <Bigdesk8:DBText ID="DBText18" runat="server" ItemName="PostStatus"></Bigdesk8:DBText>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                                        操作人编号
                                    </td>
                                    <td style="background: #fff; height: 30px; width: 35%">
                                        <Bigdesk8:DBText ID="DBText19" runat="server" ItemName="OperatorID"></Bigdesk8:DBText>
                                    </td>
                                    <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                                        操作人名称
                                    </td>
                                    <td style="background: #fff; height: 30px; width: 35%">
                                        <Bigdesk8:DBText ID="DBText20" runat="server" ItemName="OperatorName" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                                        操作日期时间
                                    </td>
                                    <td style="background: #fff; height: 30px; width: 35%">
                                        <Bigdesk8:DBText ID="DBText10" runat="server" ItemName="OperateDateTime" ItemType="DateTime"
                                            ItemFormat="yyyy-MM-dd HH:mm:ss"></Bigdesk8:DBText>
                                    </td>
                                    <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                                    </td>
                                    <td style="background: #fff; height: 30px; width: 35%">
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
