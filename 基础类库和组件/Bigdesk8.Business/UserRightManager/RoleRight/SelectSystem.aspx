<%@ Page Title="角色权限 - 维护 - 选择系统" Language="C#" MasterPageFile="../../BusinessSite.Master"
    AutoEventWireup="true" CodeBehind="SelectSystem.aspx.cs" Inherits="Bigdesk8.Business.UserRightManager.RoleRight.SelectSystem" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Master_Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Master_Content" runat="server">

    <script type="text/javascript">
        $(function() {
            $("[id*='gridView']").DataGridUI({
                rowBeginID: 2,
                rowEndID: 1
            });
        })
    </script>

    <table cellspacing="5" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            您的位置：<span style="color: Blue">角色权限 - 维护 - 选择系统</span>
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
                            <Bigdesk8:DBText ID="DBTextBox17" runat="server" ItemName="RoleName" />
                        </td>
                        <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                            角色描述
                        </td>
                        <td style="background: #fff; height: 30px; width: 35%">
                            <Bigdesk8:DBText ID="DBTextBox1" runat="server" ItemName="RoleDesc" />
                        </td>
                    </tr>
                    <tr>
                        <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                            系统名称
                        </td>
                        <td style="background: #fff; width: 35%; height: 30px">
                            <Bigdesk8:DBTextBox ID="DBTextBox2" runat="server" ItemName="SystemName" />
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
                        <asp:BoundField DataField="SystemID" HeaderText="系统编号" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SystemName" HeaderText="系统名称" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SystemDesc" HeaderText="系统描述" ItemStyle-Width="20%" />
                        <asp:TemplateField HeaderText="设置角色权限" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-ForeColor="Yellow">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" Text="设置角色权限" NavigateUrl='<%# string.Format("SetRoleRight.aspx?RoleID={0}&SystemID={1}",this.roleID,Eval("SystemID")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </Bigdesk8:PowerDataGrid>
            </td>
        </tr>
    </table>
</asp:Content>
