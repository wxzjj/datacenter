﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Jsdw_List.aspx.cs" Inherits="WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Sczt.Jsdw_List" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>建设单位信息列表</title>
    <link href="../../App_Themes/WxjzgcjczyQyb_B_Theme/base.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/WxjzgcjczyQyb_B_Theme/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="1" cellpadding="0">
        <tr>
            <td class="searchpanel">
                <div class="searchtitle">
                    <span>您的当前位置：</span><span>市场主体>建设单位</span><img alt="" src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/searchtool.gif" />
                   <%-- <div class="togglebtn">
                    </div>--%>
                </div>
                <div class="navline" style="margin-bottom: 4px; margin-top: 4px;">
                </div>
            </td>
        </tr>
        <tr>
            <td class="searchbox">
                <table width="100%" border="0" cellspacing="1" cellpadding="2" class="table-bk">
                    <tr>
                        <td class="td-text" width="15%">
                            单位名称
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBTextBox ID="DBTextBox1" ItemName="jsdw" runat="server"></Bigdesk8:DBTextBox>
                        </td>
                        <td class="td-text" width="15%">
                            组织机构代码 （统一社会信用代码）
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBTextBox ID="DBTextBox2" ItemName="zzjgdm" runat="server"></Bigdesk8:DBTextBox>
                        </td>
                    </tr>
                    <%-- <tr>
                        <td class="td-text" width="15%">
                            信息来源
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBDropDownList runat="server" ID="Xxly" ItemName="tag">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="江苏建设公共基础数据平台"> 江苏建设公共基础数据平台</asp:ListItem>
                                <asp:ListItem Value="局一号通系统"> 局一号通系统</asp:ListItem>
                            </Bigdesk8:DBDropDownList>
                        </td>
                        <td class="td-text" width="15%">
                            单位类型
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBDropDownList runat="server" ID="dwlx" ItemName="dwflid">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="1"> 房地产开发企业</asp:ListItem>
                                <asp:ListItem Value="3"> 其它</asp:ListItem>
                            </Bigdesk8:DBDropDownList>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="td-value" colspan="4">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="padding-left: 10px; text-align: left; height: 30px;">
                                    </td>
                                    <td style="width: 6%; text-align: right; padding-right: 5px;">
                                        <asp:Button ID="Button_Search" runat="server" Text="查 询" SkinID="btn_a" OnClick="Button_Search_Click"
                                            Height="27px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
    </td> </tr>
    <tr>
        <td class="maingrid">
            <Bigdesk8:PowerDataGrid ID="gridView" runat="server" AutoGenerateColumns="False"
                Width="100%" AllowPaging="true" PageSize="20" OnPageIndexChanging="gridView_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="单位名称">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("jsdw") %>' NavigateUrl='<%#string.Format("JsdwxxToolBar.aspx?jsdwid={0}",Eval("jsdwid")) %>'
                                Target="_blank" />
                        </ItemTemplate>
                        <ItemStyle CssClass="pdg-itemstyle-left" Width="5%"></ItemStyle>
                        <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="组织机构代码(统一社会信用代码)" DataField="zzjgdm">
                        <ItemStyle CssClass="pdg-itemstyle-center" Width="4%"></ItemStyle>
                        <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="单位地址" DataField="dwdz">
                        <ItemStyle CssClass="pdg-itemstyle-left" Width="5%"></ItemStyle>
                        <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                    </asp:BoundField>
                    <%--    <asp:BoundField HeaderText="法定代表人" DataField="fddbr">
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:BoundField>--%>
                    <%--    <asp:BoundField HeaderText="联系人" DataField="lxr">
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:BoundField>--%>
                    <asp:BoundField HeaderText="单位类型" DataField="dwfl">
                        <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                        <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                    </asp:BoundField>
                </Columns>
            </Bigdesk8:PowerDataGrid>
        </td>
    </tr>
    </table>
    </form>
</body>
</html>
