<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zhjg_Zlbj_List.aspx.cs"
    Inherits="WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Gcxm.Zhjg_Zlbj_List" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
                    <span>您的当前位置：</span><span>工程项目>质量报监</span><img alt="" src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/searchtool.gif" />
                  <%--  <div class="togglebtn">
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
                            项目编号
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBTextBox ID="DBTextBox1" ItemName="PrjNum" runat="server"></Bigdesk8:DBTextBox>
                        </td>
                        <td class="td-text" width="15%">
                            项目名称
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBTextBox ID="DBTextBox2" ItemName="PrjName" runat="server"></Bigdesk8:DBTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            质量监督编码
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBTextBox ID="zljdbm" ItemName="zljdbm" runat="server"></Bigdesk8:DBTextBox>
                        </td>
                        <td class="td-text" width="15%">
                            报监工程名称
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBTextBox ID="gcmc" ItemName="gcmc" runat="server"></Bigdesk8:DBTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            质量监督机构名称
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBTextBox ID="zljdjgmc" ItemName="zljdjgmc" runat="server"></Bigdesk8:DBTextBox>
                        </td>
                        <td class="td-text" width="15%">
                            项目属地
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBDropDownList ID="DDL_xmsd" ItemName="CountyNum" DropDownListType="Value"
                                ForeColor="Blue" ToolTip="Xzqdm" runat="server">
                            </Bigdesk8:DBDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            结构类型
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBDropDownList ID="DDL_jglx" ItemName="jglx" DropDownListType="Value" ForeColor="Blue"
                                ToolTip="PrjStructureType" runat="server">
                            </Bigdesk8:DBDropDownList>
                        </td>
                        <td class="td-text" width="15%">
                            报监日期
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBDatePicker ID="sbrq1" Width="80px" ItemName="sbrq" ItemRelation="GreaterThanOrEqual"
                                runat="server"></Bigdesk8:DBDatePicker>
                            至
                            <Bigdesk8:DBDatePicker ID="sbrq2" Width="80px" ItemName="sbrq" ItemRelation="LessThanOrEqual"
                                runat="server"></Bigdesk8:DBDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            报省状态
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBDropDownList ID="DBDropDownList2" runat="server" ItemRelation="Equal"
                                ItemName="OperateState">
                                <asp:ListItem Value=""> </asp:ListItem>
                                <asp:ListItem Value="1">已上报</asp:ListItem>
                                <asp:ListItem Value="0">未上报</asp:ListItem>
                            </Bigdesk8:DBDropDownList>
                        </td>
                        <td class="td-text" width="15%">
                        </td>
                        <td class="td-value" width="35%">
                        </td>
                    </tr>
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
            </td>
        </tr>
        <tr>
            <td class="maingrid">
                <Bigdesk8:PowerDataGrid ID="gridView" runat="server" AutoGenerateColumns="False"
                    Width="100%" AllowPaging="true" PageSize="20" OnPageIndexChanging="gridView_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="报省状态">
                            <ItemTemplate>
                                <%# Int32.Parse(Eval("OperateState").ToString())>0?"已上报":"未上报"%>
                            </ItemTemplate>
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="项目编号" DataField="PrjNum">
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="项目名称">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("PrjName") %>' NavigateUrl='<%#string.Format("Zhjg_Lxxmdj_Menu.aspx?PrjNum={0}",Eval("PrjNum")) %>'
                                    Target="_blank" />
                            </ItemTemplate>
                            <ItemStyle CssClass="pdg-itemstyle-left" Width="3%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="质量监督编码" DataField="zljdbm">
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="报监工程名称">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("gcmc") %>' NavigateUrl='<%#string.Format("Zhjg_Zlbj_View.aspx?PKID={0}&zljdbm={1}",Eval("PKID"),Eval("zljdbm")) %>'
                                    Target="_blank" />
                            </ItemTemplate>
                            <ItemStyle CssClass="pdg-itemstyle-left" Width="3%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="质量监督机构名称" DataField="zljdjgmc">
                            <ItemStyle CssClass="pdg-itemstyle-left" Width="3%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="结构类型" DataField="StructureType">
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:BoundField>
                       <%-- <asp:BoundField HeaderText="工程造价（万元）" DataField="gczj">
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:BoundField>--%>
                        <asp:BoundField HeaderText="报监日期" DataField="sbrq">
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
