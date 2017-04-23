<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Jkcbxx_View.aspx.cs" Inherits="WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Xxgx.Jkcbxx_View" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>接口调用信息详情</title>
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
                            <Bigdesk8:DBTextBox ID="DBTextBox1" ItemName="apiName" runat="server" ItemRelation="Like"></Bigdesk8:DBTextBox>
                        </td>
                        <td class="td-text" width="15%">
                            接口调用结果
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBDropDownList runat="server" ID="DBDropDownList1" ItemName="apiDyResult"
                                Width="150px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="成功">成功</asp:ListItem>
                                <asp:ListItem Value="失败">失败</asp:ListItem>
                            </Bigdesk8:DBDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            接口调用时间
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBDatePicker ID="bjrq1" FieldType="Date" Width="150px" ItemRelation="GreaterThanOrEqual"
                                ItemName="apiDyTime" runat="server"></Bigdesk8:DBDatePicker>
                            至
                            <Bigdesk8:DBDatePicker ID="bjrq2" FieldType="Date" Width="150px" ItemRelation="LessThanOrEqual"
                                ItemName="apiDyTime" runat="server"></Bigdesk8:DBDatePicker>
                        </td>
                        <td class="td-text" width="15%">
                        </td>
                        <td class="td-value" width="35%">
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
                    Width="100%" AllowPaging="true" PageSize="20" OnPageIndexChanging="gridView_PageIndexChanging"
                    OnRowDataBound="gridView_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="接口名称" DataField="apiName">
                            <ItemStyle CssClass="pdg-itemstyle-left" Width="4%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="接口调用结果" DataField="apiDyResult">
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="接口调用异常详情" DataField="apiDyMessage">
                            <ItemStyle CssClass="pdg-itemstyle-left" Width="8%"></ItemStyle>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="接口调用时间" DataField="apiDyTime" DataFormatString="{0:yyyy-MM-dd hh:mm:ss}">
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
