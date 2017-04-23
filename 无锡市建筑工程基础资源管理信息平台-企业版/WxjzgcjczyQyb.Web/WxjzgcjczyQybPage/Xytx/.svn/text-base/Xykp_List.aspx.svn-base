<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Xykp_List.aspx.cs" Inherits="WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Xytx.Xykp_List" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>施工企业</title>
    <link href="../../App_Themes/WxjzgcjczyQyb_B_Theme/base.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/WxjzgcjczyQyb_B_Theme/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="1" cellpadding="0">
        <tr>
            <td class="searchpanel">
                <div class="searchtitle">
                    <span>您的当前位置：</span><span>信用体系>施工企业</span>
                    <img alt="" src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/searchtool.gif" />
                    <%--<div class="togglebtn">
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
                            企业名称
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
                    <tr>
                        <td width="15%" class="td-text">
                            资质类别
                        </td>
                        <td width="35%" class="td-value">
                            <Bigdesk8:DBCheckBoxList ID="cbl_zzlb" runat="server" RepeatColumns="9" ForeColor="Black">
                                <asp:ListItem Value="总承包企业">总承包企业</asp:ListItem>
                                <asp:ListItem Value="专业承包企业">专业承包企业</asp:ListItem>
                                <asp:ListItem Value="一体化">一体化</asp:ListItem>
                                <asp:ListItem Value="劳务分包">劳务分包</asp:ListItem>
                            </Bigdesk8:DBCheckBoxList>
                        </td>
                        <td class="td-text" width="15%">
                            考评年度
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBDropDownList runat="server" ID="Xxly" ItemName="kpnd">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="2012"> 2012</asp:ListItem>
                                <asp:ListItem Value="2013"> 2013</asp:ListItem>
                                <asp:ListItem Value="2014"> 2014</asp:ListItem>
                                <asp:ListItem Value="2015"> 2015</asp:ListItem>
                                <asp:ListItem Value="2016"> 2016</asp:ListItem>
                            </Bigdesk8:DBDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            项目属地
                        </td>
                        <td class="td-value" width="85%" colspan="3">
                            <Bigdesk8:DBCheckBoxList ID="cbl_ssdq" runat="server" RepeatColumns="9" ForeColor="Black">
                            </Bigdesk8:DBCheckBoxList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="td-value" colspan="6">
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
        <tr>
            <td class="maingrid">
                <Bigdesk8:PowerDataGrid ID="gridView" runat="server" AutoGenerateColumns="False"
                    Width="100%" AllowPaging="true" PageSize="20" OnPageIndexChanging="gridView_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="企业名称">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("qymc") %>' NavigateUrl='<%#string.Format("../Sczt/QyxxToolBar.aspx?qyid={0}",Eval("qyID")) %>'
                                    Target="_blank" />
                            </ItemTemplate>
                            <ItemStyle CssClass="pdg-itemstyle-left" Width="2%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="组织机构代码" DataField="zzjgdm">
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="3%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="资质类型" DataField="zzlb">
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="考评年度" DataField="kpnd">
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="企业属地" DataField="qysd">
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="基本分" DataField="jbf">
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="综合大检查得分" DataField="zhdjcdf">
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="3%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="日常考核扣分" DataField="rckhkf">
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="3%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="信用分" DataField="xyf">
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
