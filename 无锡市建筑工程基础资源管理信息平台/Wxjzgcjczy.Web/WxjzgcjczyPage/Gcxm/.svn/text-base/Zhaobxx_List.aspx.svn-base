<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zhaobxx_List.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Gcxm.Zhaobxx_List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../App_Themes/<%= this.Theme %>/Stylesheet1.css" rel="Stylesheet"
        type="text/css" />
</head>
<body class="body_haveBackImg">
    <form id="form1" runat="server">
    <table width="100%" align="center" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td class="td_search">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <table width="100%" cellpadding="2" cellspacing="1" border="0" class="table">
                                <tr>
                                    <td class="td_text" width="15%">
                                        项目编号
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBTextBox ID="DBTextBox1" ItemName="PrjNum" runat="server">
                                        </Bigdesk8:DBTextBox>
                                    </td>
                                    <td class="td_text" width="15%">
                                        项目名称
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBTextBox ID="DBTextBox2" ItemName="PrjName" runat="server">
                                        </Bigdesk8:DBTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_text" width="15%">
                                        行政审批编号
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBTextBox ID="PrjInnerNum" ItemName="PrjInnerNum" runat="server">
                                        </Bigdesk8:DBTextBox>
                                    </td>
                                    <td class="td_text" width="15%">
                                        建设单位名称
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBTextBox ID="BuildCorpName" ItemName="BuildCorpName" runat="server">
                                        </Bigdesk8:DBTextBox>
                                    </td>
                                    <%--    <td class="td_text" width="15%">
                                        立项文号
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBTextBox ID="PrjApprovalNum" ItemName="PrjApprovalNum" runat="server"></Bigdesk8:DBTextBox>
                                    </td>--%>
                                </tr>
                                <tr>
                                    <td class="td_text" width="15%">
                                        项目分类
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBDropDownList ID="ddl_Xmfl" runat="server" ItemRelation="Equal" ToolTip="PrjType"
                                            ItemName="PrjTypeNum">
                                        </Bigdesk8:DBDropDownList>
                                    </td>
                                    <td class="td_text" width="15%">
                                        建设性质
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBDropDownList ID="ddl_jsxz" runat="server" ItemRelation="Equal" ToolTip="PrjProperty"
                                            ItemName="PrjPropertyNum">
                                        </Bigdesk8:DBDropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_text" width="15%">
                                        立项级别
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBDropDownList ID="ddl_lxjb" runat="server" ItemRelation="Equal" ToolTip="ApprovalLevel"
                                            ItemName="PrjApprovalLevelNum">
                                        </Bigdesk8:DBDropDownList>
                                    </td>
                                    <td class="td_text" width="15%">
                                        报省状态
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBDropDownList ID="DBDropDownList2" runat="server" ItemRelation="Equal"
                                            ItemName="OperateState">
                                            <asp:ListItem Value=""> </asp:ListItem>
                                            <asp:ListItem Value="0">已上报</asp:ListItem>
                                            <asp:ListItem Value="1">未上报</asp:ListItem>
                                        </Bigdesk8:DBDropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_text" width="15%">
                                        项目属地
                                    </td>
                                    <td class="td_value" width="85%" colspan="3">
                                        <asp:CheckBoxList ID="cbl_ssdq" runat="server" RepeatColumns="9">
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_value" style="text-align: right; padding-right: 5px;" colspan="4">
                                        <asp:ImageButton ID="ImageButton" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="view_head">
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
