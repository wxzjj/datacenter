<%@ Page Title="设置角色权限" Language="C#" MasterPageFile="../../BusinessSite.Master"
    AutoEventWireup="true" CodeBehind="SetRoleRight.aspx.cs" Inherits="Bigdesk8.Business.UserRightManager.RoleRight.SetRoleRight" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Master_Content" runat="server">
    <table width="100%" border="0" cellspacing="5" cellpadding="0">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            您的位置：<span style="color: Blue">角色权限 - 维护 - 设置角色权限</span>
                        </td>
                        <td align="right">
                            <a href="RoleRightList.aspx" title="返回">
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
                            角色名称
                        </td>
                        <td style="background: #fff; height: 30px; width: 35%">
                            <Bigdesk8:DBText ID="DBText2" runat="server" ItemName="RoleName" />
                        </td>
                        <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                            系统名称
                        </td>
                        <td style="background: #fff; height: 30px; width: 35%">
                            <Bigdesk8:DBText ID="DBTextBox1" runat="server" ItemName="SystemName" />
                        </td>
                    </tr>
                    <tr>
                        <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                            模块名称
                        </td>
                        <td style="background: #fff; width: 35%; height: 30px">
                            <Bigdesk8:DBTextBox ID="DBTextBox3" runat="server" ItemName="ModuleName" />
                        </td>
                        <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                            操作名称
                        </td>
                        <td style="background: #fff; width: 35%; height: 30px">
                            <Bigdesk8:DBTextBox ID="DBTextBox4" runat="server" ItemName="OperateName" />
                        </td>
                    </tr>
                    <tr>
                        <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                            业务分类名称
                        </td>
                        <td style="background: #fff; width: 35%; height: 30px">
                            <Bigdesk8:DBTextBox ID="DBTextBox5" runat="server" ItemName="DataTypeName" />
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
                    OnPageIndexChanging="gridView_PageIndexChanging">
                    <HeaderStyle BackColor="#008ed2" ForeColor="White" />
                    <PagerSettings Position="TopAndBottom" FirstPageText="首页" LastPageText="尾页" NextPageText="下页"
                        PreviousPageText="上页"></PagerSettings>
                    <Columns>
                        <asp:TemplateField HeaderText="选择" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="cb_select" runat="server" Checked='<%# Eval("Selected") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ModuleCode" HeaderText="模块代码" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ModuleName" HeaderText="模块名称" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="OperateCode" HeaderText="操作代码" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="OperateName" HeaderText="操作名称" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="DataTypeCode" HeaderText="业务分类代码" ItemStyle-Width="8%"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="DataTypeName" HeaderText="业务分类名称" ItemStyle-Width="10%"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_ModuleID" runat="server" Text='<%# Eval("ModuleID") %>' />
                                <asp:Label ID="lbl_OperateID" runat="server" Text='<%# Eval("OperateID") %>' />
                                <asp:Label ID="lbl_DataTypeID" runat="server" Text='<%# Eval("DataTypeID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
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
