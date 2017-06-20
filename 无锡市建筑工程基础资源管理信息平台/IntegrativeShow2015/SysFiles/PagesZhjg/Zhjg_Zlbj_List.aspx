<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zhjg_Zlbj_List.aspx.cs"
    Inherits="IntegrativeShow2.SysFiles.PagesZhjg.Zhjg_Zlbj_List" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../App_Themes/Themes_Standard/Stylesheet1.css" rel="Stylesheet" type="text/css" />
     <script src="../../Common/jquery-1.3.2.min.js" type="text/javascript"></script>
       <script src="../../MyDatePicker/WdatePicker.js" type="text/javascript"></script>
       
     <script type="text/javascript">
         $(function() {

             $("input[FieldType='Date']").click(function() {
                 WdatePicker({ isShowClear: true, readOnly: true });
             });

         });
     </script>
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
                                        <Bigdesk8:DBTextBox ID="DBTextBox1" ItemName="PrjNum" runat="server"></Bigdesk8:DBTextBox>
                                    </td>
                                    <td class="td_text" width="15%">
                                        项目名称
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBTextBox ID="DBTextBox2" ItemName="PrjName" runat="server"></Bigdesk8:DBTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_text" width="15%">
                                        质量监督编码
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBTextBox ID="zljdbm" ItemName="zljdbm" runat="server"></Bigdesk8:DBTextBox>
                                    </td>
                                    <td class="td_text" width="15%">
                                        报监工程名称
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBTextBox ID="gcmc" ItemName="gcmc" runat="server"></Bigdesk8:DBTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_text" width="15%">
                                        质量监督机构名称
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBTextBox ID="zljdjgmc" ItemName="zljdjgmc" runat="server"></Bigdesk8:DBTextBox>
                                    </td>
                                    <td class="td_text" width="15%">
                                        项目属地
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBDropDownList ID="DDL_xmsd" ItemName="CountyNum" DropDownListType="Value"
                                            ForeColor="Blue" ToolTip="Xzqdm" runat="server">
                                        </Bigdesk8:DBDropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_text" width="15%">
                                        结构类型
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBDropDownList ID="DDL_jglx" ItemName="jglx" DropDownListType="Value" ForeColor="Blue"
                                            ToolTip="PrjStructureType" runat="server">
                                        </Bigdesk8:DBDropDownList>
                                    </td>
                                    <td class="td_text" width="15%">
                                        报监日期
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBTextBox ID="sbrq1" FieldType="Date" Width="40%" ItemName="sbrq" ItemRelation="GreaterThanOrEqual"
                                            runat="server"></Bigdesk8:DBTextBox>
                                        至
                                        <Bigdesk8:DBTextBox ID="sbrq2" FieldType="Date" Width="40%" ItemName="sbrq" ItemRelation="LessThanOrEqual"
                                            runat="server"></Bigdesk8:DBTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_text" width="15%">
                                        报省状态
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBDropDownList ID="DBDropDownList2" runat="server" ItemRelation="Equal"
                                            ItemName="OperateState">
                                            <asp:ListItem Value=""> </asp:ListItem>
                                            <asp:ListItem Value="1">已上报</asp:ListItem>
                                            <asp:ListItem Value="0">未上报</asp:ListItem>
                                        </Bigdesk8:DBDropDownList>
                                    </td>
                                    <td class="td_text" width="15%">
                                    </td>
                                    <td class="td_value" width="35%">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background-color: White" colspan="6">
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
                                                <td style="text-align: right;">
                                                </td>
                                                <td style="width: 10%; padding-right: 10px; text-align: right;">
                                                    <asp:ImageButton ID="ImageButton" runat="server" ImageUrl="../Images/LinksButton/Search_Button3.png"
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
            <td class="view_head">
                <Bigdesk8:PowerDataGrid ID="Gdv_ZlbjInfo" runat="server" OnPageIndexChanging="powerGridView_PageIndexChanging"
                    PageSize="20" Width="100%" DataKeyNames="PKID" AutoGenerateColumns="false" OnRowDataBound="powerGridView_RowDataBound"
                    AllowPaging="true">
                    <Columns>
                        <asp:TemplateField HeaderText="报省状态">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%# Int32.Parse(Eval("OperateState").ToString())>0?"已上报":"未上报"%>' 
                                NavigateUrl='<%#string.Format("/WxjzgcjczyQybPage/Xxgx/JbZxjk_FailureList.aspx?pkid={0}",Eval("PKID")) %>'
                                    Target="_blank" />
                            </ItemTemplate>
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="3%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="项目编号" DataField="PrjNum">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="8%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <%--  <asp:BoundField HeaderText="项目名称" DataField="PrjName">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="13%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>--%>
                        <asp:TemplateField HeaderText="项目名称">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("PrjName") %>' NavigateUrl='<%#string.Format("{0}?viewUrl=../PagesZHJG/Zhjg_Lxxmdj_Menu.aspx$LoginID={1}%PKID={2}%PrjNum={3}&titleName={4}",publicViewUrl,this.WorkUser.UserID,Eval("LxPKID"),Eval("PrjNum"),"项目信息-"+Eval("PrjName")) %>'
                                    Target="_blank" />
                            </ItemTemplate>
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="16%" HorizontalAlign="left" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" ForeColor="Yellow" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="质量监督编码" DataField="zljdbm">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="8%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="报监工程名称">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("gcmc") %>' NavigateUrl='<%#string.Format("{0}?viewUrl=../PagesZHJG/Zhjg_Zlbj_View.aspx$LoginID={1}%PKID={2}&titleName={3}",publicViewUrl,this.WorkUser.UserID,Eval("PKID"),"质量报监-"+Eval("gcmc")) %>'
                                    Target="_blank" />
                            </ItemTemplate>
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="16%" HorizontalAlign="left" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" ForeColor="Yellow" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="质量监督机构名称" DataField="zljdjgmc">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="15%" HorizontalAlign="left" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <%--  <asp:BoundField HeaderText="质量监督机构代码" DataField="zjzbm">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="5%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>--%>
                        <asp:BoundField HeaderText="结构类型" DataField="StructureType">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="7%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="工程造价（万元）" DataField="gczj">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="5%" HorizontalAlign="right" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="报监日期" DataField="sbrq">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="5%" HorizontalAlign="center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                    </Columns>
                </Bigdesk8:PowerDataGrid>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
