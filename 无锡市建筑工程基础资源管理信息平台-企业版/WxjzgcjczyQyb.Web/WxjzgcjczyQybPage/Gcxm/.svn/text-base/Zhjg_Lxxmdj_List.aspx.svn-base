<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zhjg_Lxxmdj_List.aspx.cs"
    Inherits="WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Gcxm.Zhjg_Lxxmdj_List" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>我负责的任务</title>
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
                    <span>您的当前位置：</span><span>工程项目>项目信息</span><img alt="" src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/searchtool.gif" />
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
                            项目编号
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBTextBox ID="DBTextBox1" ItemName="PrjNum" runat="server">
                            </Bigdesk8:DBTextBox>
                        </td>
                        <td class="td-text" width="15%">
                            项目名称
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBTextBox ID="DBTextBox2" ItemName="PrjName" runat="server">
                            </Bigdesk8:DBTextBox>
                        </td>
                    </tr>
                    <tr>
                       <%-- <td class="td-text" width="15%">
                            行政审批编号
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBTextBox ID="PrjInnerNum" ItemName="PrjInnerNum" runat="server">
                            </Bigdesk8:DBTextBox>
                        </td>--%>
                        <td class="td-text" width="15%">
                            建设单位名称
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBTextBox ID="BuildCorpName" ItemName="BuildCorpName" runat="server">
                            </Bigdesk8:DBTextBox>
                        </td>
                   
                        <td class="td-text" width="15%">
                            项目分类
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBDropDownList ID="ddl_Xmfl" runat="server" ItemRelation="Equal" ToolTip="PrjType"
                                ItemName="PrjTypeNum">
                            </Bigdesk8:DBDropDownList>
                        </td>
                      <%--  <td class="td-text" width="15%">
                            建设性质
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBDropDownList ID="ddl_jsxz" runat="server" ItemRelation="Equal" ToolTip="PrjProperty"
                                ItemName="PrjPropertyNum">
                            </Bigdesk8:DBDropDownList>
                        </td>--%>
                    </tr>
                   <%-- <tr>
                        <td class="td-text" width="15%">
                            立项级别
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBDropDownList ID="ddl_lxjb" runat="server" ItemRelation="Equal" ToolTip="ApprovalLevel"
                                ItemName="PrjApprovalLevelNum">
                            </Bigdesk8:DBDropDownList>
                        </td>
                        <td class="td-text" width="15%">
                            报省状态
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBDropDownList ID="DBDropDownList2" runat="server" ItemRelation="Equal"
                                ItemName="OperateState">
                                <asp:ListItem Value=""> </asp:ListItem>
                                <asp:ListItem Value="0">已上报</asp:ListItem>
                                <asp:ListItem Value="1">未上报</asp:ListItem>
                                <asp:ListItem Value="2">来自省一体化平台</asp:ListItem>
                            </Bigdesk8:DBDropDownList>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="td-text" width="15%">
                            项目属地
                        </td>
                        <td class="td-value" width="85%" colspan="3">
                            <asp:CheckBoxList ID="cbl_ssdq" runat="server" RepeatColumns="9" ForeColor="Black">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <asp:PlaceHolder runat="server" ID="pl_gjss">
                        <tr>
                            <td class="td-text" width="15%">
                                开工日期
                            </td>
                            <td class="td-value" width="35%">
                                <Bigdesk8:DBDatePicker ID="DBTextBox5" ItemName="BDate" ItemRelation="GreaterThanOrEqual"
                                    Width="80px" runat="server">
                                </Bigdesk8:DBDatePicker>
                                至
                                <Bigdesk8:DBDatePicker ID="DBTextBox6" ItemName="BDate" ItemRelation="LessThanOrEqual"
                                    Width="80px" runat="server">
                                </Bigdesk8:DBDatePicker>
                            </td>
                            <td class="td-text" width="15%">
                                竣工日期
                            </td>
                            <td class="td-value" width="35%">
                                <Bigdesk8:DBDatePicker ID="EDate1" ItemName="EDate" ItemRelation="GreaterThanOrEqual"
                                    Width="80px" runat="server">
                                </Bigdesk8:DBDatePicker>
                                至
                                <Bigdesk8:DBDatePicker ID="EDate2" ItemName="EDate" ItemRelation="LessThanOrEqual"
                                    Width="80px" runat="server">
                                </Bigdesk8:DBDatePicker>
                            </td>
                        </tr>
                    <%--    <tr>
                            <td class="td-text" width="15%">
                                有无单项工程
                            </td>
                            <td class="td-value" width="35%">
                                <Bigdesk8:DBDropDownList ID="DBDropDownList9" runat="server" ItemRelation="Equal"
                                    ItemName="HasDxgc">
                                    <asp:ListItem Value="">
                                    </asp:ListItem>
                                    <asp:ListItem Value="1">有</asp:ListItem>
                                    <asp:ListItem Value="0">无</asp:ListItem>
                                </Bigdesk8:DBDropDownList>
                            </td>
                            <td class="td-text" width="15%">
                                有无施工图审查
                            </td>
                            <td class="td-value" width="35%">
                                <Bigdesk8:DBDropDownList ID="DBDropDownList3" runat="server" ItemRelation="Equal"
                                    ItemName="HasSgtsc">
                                    <asp:ListItem Value="">
                                    </asp:ListItem>
                                    <asp:ListItem Value="1">有</asp:ListItem>
                                    <asp:ListItem Value="0">无</asp:ListItem>
                                </Bigdesk8:DBDropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td-text" width="15%">
                                有无招投标信息
                            </td>
                            <td class="td-value" width="35%">
                                <Bigdesk8:DBDropDownList ID="DBDropDownList1" runat="server" ItemRelation="Equal"
                                    ItemName="HasZtbxx">
                                    <asp:ListItem Value="">
                                    </asp:ListItem>
                                    <asp:ListItem Value="1">有</asp:ListItem>
                                    <asp:ListItem Value="0">无</asp:ListItem>
                                </Bigdesk8:DBDropDownList>
                            </td>
                            <td class="td-text" width="15%">
                                合同备案类型
                            </td>
                            <td class="td-value" width="35%">
                                <Bigdesk8:DBDropDownList ID="ddl_Heba" runat="server">
                                    <asp:ListItem Value="">
                                    </asp:ListItem>
                                    <asp:ListItem Value="0">合同备案</asp:ListItem>
                                    <asp:ListItem Value="1">勘察设计合同</asp:ListItem>
                                    <asp:ListItem Value="2">施工合同备案</asp:ListItem>
                                    <asp:ListItem Value="3">监理合同备案</asp:ListItem>
                                </Bigdesk8:DBDropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td-text" width="15%">
                                有无安全报监
                            </td>
                            <td class="td-value" width="35%">
                                <Bigdesk8:DBDropDownList ID="DBDropDownList5" runat="server" ItemRelation="Equal"
                                    ItemName="HasAqbj">
                                    <asp:ListItem Value="">
                                    </asp:ListItem>
                                    <asp:ListItem Value="1">有</asp:ListItem>
                                    <asp:ListItem Value="0">无</asp:ListItem>
                                </Bigdesk8:DBDropDownList>
                            </td>
                            <td class="td-text" width="15%">
                                有无质量报监
                            </td>
                            <td class="td-value" width="35%">
                                <Bigdesk8:DBDropDownList ID="DBDropDownList6" runat="server" ItemRelation="Equal"
                                    ItemName="HasZlbj">
                                    <asp:ListItem Value="">
                                    </asp:ListItem>
                                    <asp:ListItem Value="1">有</asp:ListItem>
                                    <asp:ListItem Value="0">无</asp:ListItem>
                                </Bigdesk8:DBDropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td-text" width="15%">
                                有无施工许可
                            </td>
                            <td class="td-value" width="35%">
                                <Bigdesk8:DBDropDownList ID="DBDropDownList7" runat="server" ItemRelation="Equal"
                                    ItemName="HasSgxk">
                                    <asp:ListItem Value="">
                                    </asp:ListItem>
                                    <asp:ListItem Value="1">有</asp:ListItem>
                                    <asp:ListItem Value="0">无</asp:ListItem>
                                </Bigdesk8:DBDropDownList>
                            </td>
                            <td class="td-text" width="15%">
                                有无竣工备案
                            </td>
                            <td class="td-value" width="35%">
                                <Bigdesk8:DBDropDownList ID="DBDropDownList8" runat="server" ItemRelation="Equal"
                                    ItemName="HasJgba">
                                    <asp:ListItem Value="">
                                    </asp:ListItem>
                                    <asp:ListItem Value="1">有</asp:ListItem>
                                    <asp:ListItem Value="0">无</asp:ListItem>
                                </Bigdesk8:DBDropDownList>
                            </td>
                        </tr>--%>
                    </asp:PlaceHolder>
                    <tr>
                        <td class="td-value" colspan="6">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="padding-left: 10px; text-align: left; height: 30px;">
                                    </td>
                                    <td style="text-align: right; margin-right:5px;">
                                        <asp:LinkButton ID="linkButton1" runat="server" Text="打开高级搜索" Font-Underline="true"
                                            Font-Size="13px" OnClick="linkButton_Click"></asp:LinkButton>
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
                      <%--  <asp:TemplateField HeaderText="报省状态">
                            <ItemTemplate>
                                <%# Int32.Parse(Eval("OperateState").ToString())==0?"已上报":(Int32.Parse(Eval("OperateState").ToString())==2?"来自省一体化平台":"未上报")%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="4%"></ItemStyle>
                        </asp:TemplateField>--%>
                        <asp:BoundField HeaderText="项目编号" DataField="PrjNum">
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="4%"></ItemStyle>
                        </asp:BoundField>
                      <%--  <asp:BoundField HeaderText="行政审批编号" DataField="PrjInnerNum">
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="7%"></ItemStyle>
                        </asp:BoundField>--%>
                        <asp:TemplateField HeaderText="项目名称">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("PrjName") %>' NavigateUrl='<%#string.Format("Zhjg_Lxxmdj_Menu.aspx?PrjNum={0}",Eval("PrjNum")) %>'
                                    Target="_blank" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            <ItemStyle CssClass="pdg-itemstyle-left" Width="8%"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="项目分类" DataField="PrjType">
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="4%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="项目属地" DataField="County">
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="4%"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="建设单位名称">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("BuildCorpName") %>'
                                    NavigateUrl='<%#string.Format("../Sczt/JsdwxxToolBar.aspx?jsdwid={0}",Eval("jsdwID")) %>'
                                    Target="_blank" Enabled='<%# Eval("jsdwID")!=DBNull.Value %>' />
                            </ItemTemplate>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            <ItemStyle CssClass="pdg-itemstyle-left" Width="8%"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="开工日期" DataField="BDate">
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="4%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="竣工日期" DataField="EDate">
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="4%"></ItemStyle>
                        </asp:BoundField>
                      <%--  <asp:BoundField HeaderText="单项工程数" DataField="DxgcCount">
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="5%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="施工图审查数" DataField="SgtscCount">
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="6%"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="招投标数">
                            <ItemTemplate>
                                <%#Eval("ZtbxxCount") %>
                            </ItemTemplate>
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="5%"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="合同备案数" DataField="HtbaCount">
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="5%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="安全报监数" DataField="AqbjCount">
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="5%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="质量报监数" DataField="ZlbjCount">
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="5%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="施工许可数" DataField="SgxkCount">
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="5%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="竣工备案数" DataField="JgbaCount">
                            <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                            <ItemStyle CssClass="pdg-itemstyle-center" Width="5%"></ItemStyle>
                        </asp:BoundField>--%>
                    </Columns>
                </Bigdesk8:PowerDataGrid>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
