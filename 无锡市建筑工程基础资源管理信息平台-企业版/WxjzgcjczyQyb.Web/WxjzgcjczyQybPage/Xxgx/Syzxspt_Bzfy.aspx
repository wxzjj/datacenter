<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Syzxspt_Bzfy.aspx.cs" Inherits="WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Xxgx.Syzxspt_Bzfy" %>

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
                        <td class="td-text" width="13%">
                            房源编号
                        </td>
                        <td class="td-value" width="20%">
                            <Bigdesk8:DBTextBox ID="DBTextBox1" ItemName="CJHOUSENO" runat="server" ItemRelation="Like"></Bigdesk8:DBTextBox>
                        </td>
                        <td class="td-text" width="13%">
                            小区名称
                        </td>
                        <td class="td-value" width="20%">
                            <Bigdesk8:DBTextBox ID="DBTextBox2" ItemName="PROJECTNAME" runat="server" ItemRelation="Like"></Bigdesk8:DBTextBox>
                        </td>
                        <td class="td-text" width="13%">
                            房源性质
                        </td>
                        <td class="td-value" width="20%">
                            <Bigdesk8:DBTextBox ID="DBTextBox4" ItemName="HSTYPE" runat="server" ItemRelation="Like"></Bigdesk8:DBTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="13%">
                            数据保存时间
                        </td>
                        <td class="td-value" width="20%">
                            <Bigdesk8:DBDatePicker ID="DBDatePicker2" ItemName="CreateDate" runat="server" ItemRelation="GreaterThanOrEqual"
                                Width="100px"> </Bigdesk8:DBDatePicker>
                            <Bigdesk8:DBDatePicker ID="DBDatePicker3" ItemName="CreateDate" runat="server" ItemRelation="LessThanOrEqual"
                                Width="100px"> </Bigdesk8:DBDatePicker>
                        </td>
                        <td class="td-text" width="13%">
                            是否已推送
                        </td>
                        <td class="td-value" width="20%">
                            <Bigdesk8:DBRadioButtonList runat="server" ID="DBChl_sfyts" ItemName="IsYts" RepeatDirection="Horizontal" 
                                ForeColor="Black" 
                                onselectedindexchanged="DBChl_sfyts_SelectedIndexChanged">
                                <asp:ListItem Value="是">是</asp:ListItem>
                                <asp:ListItem Value="否">否</asp:ListItem>
                            </Bigdesk8:DBRadioButtonList>
                        </td>
                        <td class="td-text" width="13%">
                            推送时间
                        </td>
                        <td class="td-value" width="20%">
                            <Bigdesk8:DBDatePicker ID="DBDatePicker1" ItemName="TsTime" runat="server" ItemRelation="GreaterThanOrEqual"
                                Width="100px"> </Bigdesk8:DBDatePicker>
                            <Bigdesk8:DBDatePicker ID="DBDatePicker4" ItemName="TsTime" runat="server" ItemRelation="LessThanOrEqual"
                                Width="100px"> </Bigdesk8:DBDatePicker>
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
                </table>
            </td>
        </tr>
        <tr>
            <td class="maingrid">
                <Bigdesk8:PowerDataGrid ID="gridView" runat="server" AutoGenerateColumns="False"
                    Width="100%" AllowPaging="true" PageSize="20" OnPageIndexChanging="gridView_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                编号
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="1%"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="房源编号" DataField="CJHOUSENO">
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            <ItemStyle CssClass="pdg-itemstyle-left" Width="4%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="所属小区" DataField="PROJECTNAME">
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            <ItemStyle CssClass="pdg-itemstyle-left" Width="4%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="所属楼幢" DataField="BUILDNAME">
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="户室号" DataField="HOUSENO">
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="户型" DataField="LAYOUT">
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="建筑面积" DataField="BUILDAREA">
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="房源性质" DataField="HSTYPE">
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="3%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="所在层数" DataField="FLOOR">
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="数据保存时间" DataField="CreateDate" DataFormatString="{0: yyyy-MM-dd}">
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="3%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="是否已推送" DataField="IsYts">
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="推送时间" DataField="TsTime" DataFormatString="{0: yyyy-MM-dd}">
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="3%"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                </Bigdesk8:PowerDataGrid>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
