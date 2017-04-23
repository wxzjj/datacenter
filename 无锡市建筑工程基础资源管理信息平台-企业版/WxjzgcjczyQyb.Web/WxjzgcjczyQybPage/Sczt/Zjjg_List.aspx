<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zjjg_List.aspx.cs" Inherits="WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Sczt.Zjjg_List" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>中介机构信息列表</title>
    <link href="../../App_Themes/WxjzgcjczyQyb_B_Theme/base.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/WxjzgcjczyQyb_B_Theme/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="1" cellpadding="0">
        <tr>
            <td class="searchpanel">
                <div class="searchtitle">
                    <span>您的当前位置：</span><span>市场主体>中介机构</span><img alt="" src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/searchtool.gif" />
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
                            <Bigdesk8:DBTextBox ID="DBTextBox1" ItemName="qymc" runat="server"></Bigdesk8:DBTextBox>
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
                            单位类型
                        </td>
                        <td class="td-value" width="85%" colspan="3">
                            <Bigdesk8:DBCheckBoxList runat="server" ID="Xxly" RepeatDirection="Horizontal" ForeColor="Black"
                                ItemName="csywlxid">
                                <asp:ListItem Value="4">工程监理</asp:ListItem>
                                <asp:ListItem Value="7">招标代理</asp:ListItem>
                                <asp:ListItem Value="8">造价咨询</asp:ListItem>
                                <asp:ListItem Value="9">工程检测</asp:ListItem>
                                <asp:ListItem Value="15">施工图审查</asp:ListItem>
                                <asp:ListItem Value="16">房地产估价</asp:ListItem>
                                <asp:ListItem Value="17">物业服务</asp:ListItem>
                            </Bigdesk8:DBCheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            属地
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBDropDownList runat="server" ID="ssdq" ItemName="CountyID" ToolTip="Xzqdm">
                            </Bigdesk8:DBDropDownList>
                        </td>
                        <td class="td-text" width="15%">
                        </td>
                        <td class="td-value" width="35%">
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
                Width="100%" AllowPaging="true" PageSize="20" OnPageIndexChanging="gridView_PageIndexChanging"
                OnRowDataBound="gridView_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="单位名称">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("qymc") %>' NavigateUrl='<%#string.Format("QyxxToolBar.aspx?qyid={0}",Eval("qyid")) %>'
                                Target="_blank" />
                        </ItemTemplate>
                        <ItemStyle CssClass="pdg-itemstyle-left" Width="5%"></ItemStyle>
                        <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="组织机构代码(统一社会信用代码)" DataField="zzjgdm">
                        <ItemStyle CssClass="pdg-itemstyle-center" Width="4%"></ItemStyle>
                        <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="单位地址" DataField="xxdd">
                        <ItemStyle CssClass="pdg-itemstyle-left" Width="5%"></ItemStyle>
                        <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                    </asp:BoundField>
                    <%-- <asp:BoundField HeaderText="营业执照注册号" DataField="yyzzzch">
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:BoundField>--%>
                    <%-- <asp:BoundField HeaderText="企业联系电话" DataField="lxdh">
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:BoundField>--%>
                    <%--    <asp:BoundField HeaderText="法定代表人" DataField="fddbr">
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:BoundField>--%>
                    <asp:BoundField HeaderText="单位类型" DataField="csywlx">
                        <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                        <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                    </asp:BoundField>
                    <%--  <asp:BoundField HeaderText="属地" DataField="county">
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:BoundField>--%>
                </Columns>
            </Bigdesk8:PowerDataGrid>
        </td>
    </tr>
    </table>
    </form>
</body>
</html>
