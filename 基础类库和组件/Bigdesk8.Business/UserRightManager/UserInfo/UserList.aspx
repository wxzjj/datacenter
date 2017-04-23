<%@ Page Title="用户管理" Language="C#" MasterPageFile="../../BusinessSite.Master" AutoEventWireup="true"
    CodeBehind="UserList.aspx.cs" Inherits="Bigdesk8.Business.UserRightManager.UserInfo.UserList" %>

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

        function ReloadGridData() {
            $("[id$='searchButton']").click();
        }
    </script>

    <table cellspacing="5" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                您的位置：<span style="color: Blue">用户管理</span>
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
                        <td style="background: #fff; width: 35%; height: 30px">
                            <Bigdesk8:DBTextBox ID="DBTextBox3" runat="server" ItemName="LoginName" />
                        </td>
                        <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                            中文实名
                        </td>
                        <td style="background: #fff; width: 35%; height: 30px">
                            <Bigdesk8:DBTextBox ID="DBTextBox1" runat="server" ItemName="UserName" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" style="width: 30%">
                            <input type="button" value="新增" onclick="javascript:window.location='Create.aspx'" />
                        </td>
                        <td align="center" style="width: 40%">
                        </td>
                        <td align="right" style="width: 30%">
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
                    OnPageIndexChanging="gridView_PageIndexChanging" OnRowDeleting="gridView_RowDeleting">
                    <HeaderStyle BackColor="#008ed2" ForeColor="White" />
                    <PagerSettings Position="TopAndBottom" FirstPageText="首页" LastPageText="尾页" NextPageText="下页"
                        PreviousPageText="上页"></PagerSettings>
                    <Columns>
                        <asp:BoundField DataField="UserID" HeaderText="用户编号" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="UserName" HeaderText="中文实名" ItemStyle-Width="20%" />
                        <asp:BoundField DataField="LoginName" HeaderText="登录名称" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="LoginPassword" HeaderText="登录密码" ItemStyle-Width="10%"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:HyperLinkField HeaderText="修改" ItemStyle-Width="4%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-ForeColor="Yellow" Text="修改" DataNavigateUrlFields="UserID" DataNavigateUrlFormatString="Modify.aspx?UserID={0}" />
                        <asp:TemplateField HeaderText="删除" ItemStyle-Width="4%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-ForeColor="Yellow">
                            <ItemTemplate>
                                <asp:LinkButton ID="lb_Delete" runat="server" CausesValidation="false" CommandName="Delete"
                                    Text="删除" OnClientClick="javascript:return confirm('确定要删除吗？');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_UserID" runat="server" Text='<%# Eval("UserID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </Bigdesk8:PowerDataGrid>
            </td>
        </tr>
    </table>
</asp:Content>
