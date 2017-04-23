<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Jkztjk_List.aspx.cs" Inherits="WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Xxgx.Jkztjk_List" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>信息共享</title>
    <link href="../../App_Themes/WxjzgcjczyQyb_B_Theme/base.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/WxjzgcjczyQyb_B_Theme/main.css" rel="stylesheet" type="text/css" />

    <script src="../Common/scripts/jquery-1.7.1.min.js" type="text/javascript"></script>

    <script src="../Common/scripts/common.js" type="text/javascript"></script>

    <link href="../../SparkClient/jquery.ui-1.8.2.css" rel="stylesheet" type="text/css" />

    <script src="../../SparkClient/jquery.ui-1.8.2.min.js" type="text/javascript"></script>

    <script src="../../SparkClient/jquery.validate.min.js" type="text/javascript"></script>

    <script src="../../SparkClient/script.js" type="text/javascript"></script>

    <script src="../../SparkClient/Calendar.js" type="text/javascript"></script>

    <script src="../../SparkClient/control.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="1" cellpadding="0">
        <tr>
            <td class="searchpanel">
                <div class="searchtitle">
                    <span>搜索</span><img alt="" src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/searchtool.gif" />
                    <div class="togglebtn">
                    </div>
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
                            接口名称
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBTextBox ID="DBTextBox1" ItemName="apiFromSys" runat="server" ItemRelation="Like"></Bigdesk8:DBTextBox>
                        </td>
                        <td class="td-text" width="15%">
                            接口运行状态
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBDropDownList runat="server" ID="ddl_sfzcyx" ItemName="apiRunState" Width="150px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="1">正常</asp:ListItem>
                                <asp:ListItem Value="0">异常</asp:ListItem>
                            </Bigdesk8:DBDropDownList>
                        </td>
                    </tr>
                    <%--  <td class="td-text" width="15%">
                            接口来自哪个系统
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBTextBox ID="DBTextBox2" ItemName="apiFromSys" runat="server" ItemRelation="Like"></Bigdesk8:DBTextBox>
                        </td>--%>
                    <%--   </tr>
                    <tr>--%>
                    <%-- <td class="td-text" width="15%">
                            接口创建时间
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBDatePicker ID="bjrq1" FieldType="Date" Width="150px" ItemRelation="GreaterThanOrEqual"
                                ItemName="apiCjTime" runat="server"></Bigdesk8:DBDatePicker>
                            至
                            <Bigdesk8:DBDatePicker ID="bjrq2" FieldType="Date" Width="150px" ItemRelation="LessThanOrEqual"
                                ItemName="apiCjTime" runat="server"></Bigdesk8:DBDatePicker>
                        </td>--%>
                    <%-- </tr>--%>
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
                    Width="100%" AllowPaging="true" PageSize="20" OnPageIndexChanging="gridView_PageIndexChanging"
                    OnRowDataBound="gridView_RowDataBound" OnRowCommand="gridView_OnRowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="apiFlow" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblapiFlow" runat="server" Text='<%# Eval("apiFlow") %>'></asp:Label>
                                <asp:Label ID="lblapiControl" runat="server" Text='<%# Eval("apiControl") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="1%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="接口名称">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("apiFromSys") %>' NavigateUrl='<%#string.Format("Jkztjk_Menu.aspx?apiFlow={0}",Eval("apiFlow")) %>'
                                    Target="_blank" />
                            </ItemTemplate>
                            <ItemStyle CssClass="pdg-itemstyle-left" Width="4%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:TemplateField>
                  <%--      <asp:BoundField HeaderText="接口来自哪个系统" DataField="apiFromSys">
                            <ItemStyle CssClass="pdg-itemstyle-left" Width="3%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:BoundField>--%>
                        <asp:BoundField HeaderText="接口地址" DataField="apiUrl">
                            <ItemStyle CssClass="pdg-itemstyle-left" Width="3%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:BoundField>
                       <%-- <asp:BoundField HeaderText="接口创建时间" DataField="apiCjTime" DataFormatString="{0:yyyy-MM-dd}">
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:BoundField>--%>
                        <asp:TemplateField HeaderText="接口运行状态">
                            <ItemTemplate>
                                <asp:Label ID="lblapiRunState" runat="server" Text='<%# Eval("apiRunState") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="接口开关">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lnkbtnApiControl" CommandName="Jkkz" Text='<%# Eval("apiControl").ToString()=="1"?"关闭":"开启" %>'
                                    OnClientClick="javascript:return confirm('确定要开启/关闭当前接口吗?')"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:TemplateField>
                    </Columns>
                </Bigdesk8:PowerDataGrid>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
