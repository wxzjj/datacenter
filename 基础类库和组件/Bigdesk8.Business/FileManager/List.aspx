<%@ Page Title="文件管理" Language="C#" MasterPageFile="../BusinessSite.Master" AutoEventWireup="true"
    CodeBehind="List.aspx.cs" Inherits="Bigdesk8.Business.FileManager.List" %>

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
                您的位置：<span style="color: Blue">文件管理</span>
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
                            系统名称
                        </td>
                        <td style="background: #fff; width: 35%; height: 30px">
                            <Bigdesk8:DBTextBox ID="DBTextBox3" runat="server" ItemName="SystemName" />
                        </td>
                        <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                            模块名称
                        </td>
                        <td style="background: #fff; width: 35%; height: 30px">
                            <Bigdesk8:DBTextBox ID="DBTextBox1" runat="server" ItemName="ModuleName" />
                        </td>
                    </tr>
                    <tr>
                        <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                            种类名称
                        </td>
                        <td style="background: #fff; width: 35%; height: 30px">
                            <Bigdesk8:DBTextBox ID="DBTextBox2" runat="server" ItemName="CategoryName" />
                        </td>
                        <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                            文件名称
                        </td>
                        <td style="background: #fff; width: 35%; height: 30px">
                            <Bigdesk8:DBTextBox ID="DBTextBox22" runat="server" ItemName="FileName" />
                        </td>
                    </tr>
                    <tr>
                        <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                            创建日期
                        </td>
                        <td style="background: #fff; width: 35%; height: 30px">
                            <Bigdesk8:DBDatePicker ID="dp_begin" runat="server" ItemRelation="GreaterThanOrEqual"
                                Width="75px" ItemName="CreateDateTime" ItemNameCN="创建日期" />
                            至
                            <Bigdesk8:DBDatePicker ID="dp_end" runat="server" ItemRelation="LessThanOrEqual"
                                Width="75px" ItemName="CreateDateTime" ItemNameCN="创建日期" />
                        </td>
                        <td style="background: #fff; height: 30px; width: 15%; text-align: center">
                        </td>
                        <td style="background: #fff; width: 35%; height: 30px">
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
                    OnPageIndexChanging="gridView_PageIndexChanging">
                    <HeaderStyle BackColor="#008ed2" ForeColor="White" />
                    <PagerSettings Position="TopAndBottom" FirstPageText="首页" LastPageText="尾页" NextPageText="下页"
                        PreviousPageText="上页"></PagerSettings>
                    <Columns>
                        <asp:BoundField DataField="SystemName" HeaderText="系统名称" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ModuleName" HeaderText="模块名称" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="CategoryName" HeaderText="种类名称" ItemStyle-Width="10%"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FileName" HeaderText="文件名称" ItemStyle-Width="20%" />
                        <asp:TemplateField HeaderText="文件大小" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# Bigdesk8.Utilities.ToByteString(Eval("FileSize")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="创建时间" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center"
                            DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" DataField="CreateDateTime" />
                        <asp:HyperLinkField HeaderText="下载" ItemStyle-Width="4%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-ForeColor="Yellow" Text="下载" DataNavigateUrlFields="FileID" DataNavigateUrlFormatString="View.aspx?ID={0}"
                            Target="_blank" />
                    </Columns>
                </Bigdesk8:PowerDataGrid>
            </td>
        </tr>
    </table>
</asp:Content>
