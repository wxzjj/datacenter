<%@ Page Title="设置角色" Language="C#" MasterPageFile="../../BusinessSite.Master" AutoEventWireup="true"
    CodeBehind="SetUserRole.aspx.cs" Inherits="Bigdesk8.Business.UserRightManager.UserRole.SetUserRole" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Master_Content" runat="server">
    <table width="100%" border="0" cellspacing="5" cellpadding="0">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            您的位置：<span style="color: Blue">用户角色 - 维护 - 设置角色</span>
                        </td>
                        <td align="right">
                            <a href="UserRoleList.aspx" title="返回">
                                <img src="../../Styles/images/back.png" style="width: 24px; height: 24px" /></a>
                        </td>
                    </tr>
                </table>
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
            <td style="background: #666">
                <table width="100%" border="0" cellspacing="1" cellpadding="5">
                    <tr>
                        <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                            登录名称
                        </td>
                        <td style="background: #fff; height: 30px; width: 35%">
                            <Bigdesk8:DBText ID="DBTextBox17" runat="server" ItemName="LoginName"></Bigdesk8:DBText>
                        </td>
                        <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                            中文实名
                        </td>
                        <td style="background: #fff; height: 30px; width: 35%">
                            <Bigdesk8:DBText ID="DBText1" runat="server" ItemName="UserName"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                            角色名称
                        </td>
                        <td style="background: #fff; width: 35%; height: 30px">
                            <Bigdesk8:DBTextBox ID="DBTextBox2" runat="server" ItemName="RoleName" />
                        </td>
                        <td colspan="2" style="background: #fff; height: 30px">
                            <Bigdesk8:SubmitButton ID="searchButton" runat="server" Text="查询" OnClick="searchButton_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <Bigdesk8:PowerDataGrid ID="gridView" runat="server" AutoGenerateColumns="False"
                    CssClass="grid" CellPadding="5" AllowPaging="True" PageSize="15" EmptyDataText="&lt;span style='color:red'&gt;没有符合条件的信息&lt;/span&gt;"
                    OnPageIndexChanging="gridView_PageIndexChanging" OnRowDataBound="gridView_RowDataBound">
                    <HeaderStyle BackColor="#008ed2" ForeColor="White" />
                    <PagerSettings Position="TopAndBottom" FirstPageText="首页" LastPageText="尾页" NextPageText="下页"
                        PreviousPageText="上页"></PagerSettings>
                    <Columns>
                        <asp:TemplateField HeaderText="选择" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="cb_select" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="角色编号" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lbl_RoleID" runat="server" Text='<%# Eval("RoleID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="RoleName" HeaderText="角色名称" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                </Bigdesk8:PowerDataGrid>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" style="width: 30%">
                        </td>
                        <td align="center" style="width: 40%">
                            <Bigdesk8:SubmitButton ID="btn_Save" runat="server" Text="保存" OnClick="btn_Save_Click" />
                        </td>
                        <td align="right" style="width: 30%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
