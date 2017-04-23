<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataExChange_List.aspx.cs"
    Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.DataExChange_List" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../App_Themes/Themes_Standard/Stylesheet1.css" rel="Stylesheet" type="text/css" />
</head>
<body class="body_haveBackImg">
    <form id="form1" runat="server">
    <table width="100%" align="center" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td class="td_search" style="width: 100%;">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <table width="100%" cellpadding="2" cellspacing="1" border="0" class="table">
                                <tr>
                                    <td class="td_text" width="15%">
                                        表名称
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBTextBox ID="txtTabName" ItemName="tableName" runat="server">
                                        </Bigdesk8:DBTextBox>
                                    </td>
                                    <td class="td_text" width="15%">
                                        主键
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBTextBox ID="txtPkid" ItemName="PKID" runat="server">
                                        </Bigdesk8:DBTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_text" width="15%">
                                        上传是否成功
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBDropDownList ID="DropScsfcg" runat="server" Width="100px">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>否</asp:ListItem>
                                            <asp:ListItem>是</asp:ListItem>
                                        </Bigdesk8:DBDropDownList>
                                    </td>
                                    <td class="td_text" width="15%">
                                    </td>
                                    <td class="td_value" width="35%">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background-color: White" colspan="4">
                                        <table width="100%" cellpadding="2" cellspacing="0" border="0">
                                            <tr>
                                                <td style="width: 5%; text-align: right">
                                                </td>
                                                <td style="width: 15%;">
                                                </td>
                                                <td style="width: 5%; text-align: right">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                                <td style="width: 6%; text-align: right; padding-right: 5px;">
                                                    <asp:ImageButton ID="ImageButton" runat="server" ImageUrl="Common/Images/LinksButton/Search_Button3.png"
                                                        OnClick="ImageButton_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="view_head" style="width: 100%;">
                <Bigdesk8:PowerDataGrid ID="gridView" runat="server" OnPageIndexChanging="powerGridView_PageIndexChanging"
                    PageSize="20" Width="100%" AutoGenerateColumns="false" AllowPaging="true">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                编号
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="3%" HorizontalAlign="Center" Height="25"/>
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" CssClass="powerGridViewHeader"
                                Height="30" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="表名称" DataField="tableName">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="3%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" CssClass="powerGridViewHeader" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="主键" DataField="PKID">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="9%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" CssClass="powerGridViewHeader" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="是否上传成功" DataField="OperateStateMsg">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="3%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" CssClass="powerGridViewHeader" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="数据返回信息" DataField="Msg">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="9%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" CssClass="powerGridViewHeader" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="创建日期" DataField="CreateDate">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="3%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" CssClass="powerGridViewHeader" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="修改日期" DataField="UpdateDate">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="3%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" CssClass="powerGridViewHeader" />
                        </asp:BoundField>
                    </Columns>
                </Bigdesk8:PowerDataGrid>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
