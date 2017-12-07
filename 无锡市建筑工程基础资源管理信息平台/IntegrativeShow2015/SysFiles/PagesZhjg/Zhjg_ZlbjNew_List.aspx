<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zhjg_ZlbjNew_List.aspx.cs" Inherits="IntegrativeShow2.SysFiles.PagesZhjg.Zhjg_ZlbjNew_List" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../App_Themes/Themes_Standard/Stylesheet1.css" rel="Stylesheet" type="text/css" />
     <script src="../../Common/jquery-1.3.2.min.js" type="text/javascript"></script>
       <script src="../../MyDatePicker/WdatePicker.js" type="text/javascript"></script>
       
     <script type="text/javascript">
         $(function () {

             $("input[FieldType='Date']").click(function () {
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
                                        质监申报表编号
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBTextBox ID="DBTextBox1" ItemName="uuid" runat="server"></Bigdesk8:DBTextBox>
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
                                        质量监督机构名称
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBTextBox ID="Zjjgmc" ItemName="Zjjgmc" runat="server"></Bigdesk8:DBTextBox>
                                    </td>
                                    <td class="td_text" width="15%">
                                        报监工程名称
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBTextBox ID="xmmc" ItemName="xmmc" runat="server"></Bigdesk8:DBTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_text" width="15%">
                                        建设单位名称
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBTextBox ID="EconCorpName" ItemName="EconCorpName" runat="server"></Bigdesk8:DBTextBox>
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
                                        状态
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBDropDownList ID="DBDropDownList2" runat="server" ItemRelation="Equal"
                                            ItemName="Status">
                                            <asp:ListItem Value=""> </asp:ListItem>
                                            <asp:ListItem Value="2">审批通过</asp:ListItem>
                                            <asp:ListItem Value="1">审批退回</asp:ListItem>
                                            <asp:ListItem Value="0">未受理</asp:ListItem>
                                        </Bigdesk8:DBDropDownList>
                                    </td>
                                    <td class="td_text" width="15%">
                                        报监日期
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBTextBox ID="updateDate1" FieldType="Date" Width="40%" ItemName="updateDate" ItemRelation="GreaterThanOrEqual"
                                            runat="server"></Bigdesk8:DBTextBox>
                                        至
                                        <Bigdesk8:DBTextBox ID="updateDate2" FieldType="Date" Width="40%" ItemName="updateDate" ItemRelation="LessThanOrEqual"
                                            runat="server"></Bigdesk8:DBTextBox>
                                    </td>
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
                <Bigdesk8:PowerDataGrid ID="Gdv_ZlbjNewInfo" runat="server" OnPageIndexChanging="powerGridView_PageIndexChanging"
                    PageSize="20" Width="100%" DataKeyNames="uuid" AutoGenerateColumns="false" OnRowDataBound="powerGridView_RowDataBound"
                    AllowPaging="true">
                    <Columns>
                        <asp:TemplateField HeaderText="质监申报表编号">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("uuid") %>' NavigateUrl='<%#string.Format("{0}?viewUrl=../PagesZHJG/Zhjg_ZlbjNew_View.aspx$LoginID={1}%PKID={2}&titleName={3}",publicViewUrl,this.WorkUser.UserID,Eval("uuid"),"质量报监-"+Eval("xmmc")) %>'
                                    Target="_blank" />
                            </ItemTemplate>
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="10%" HorizontalAlign="left" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" ForeColor="Yellow" />
                        </asp:TemplateField>

                        <asp:BoundField HeaderText="项目编号" DataField="PrjNum">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="8%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        
                        <asp:TemplateField HeaderText="项目名称">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("PrjName") %>' NavigateUrl='<%#string.Format("{0}?viewUrl=../PagesZHJG/Zhjg_Lxxmdj_Menu.aspx$LoginID={1}%PKID={2}%PrjNum={3}&titleName={4}",publicViewUrl,this.WorkUser.UserID,Eval("LxPKID"),Eval("PrjNum"),"项目信息-"+Eval("PrjName")) %>'
                                    Target="_blank" />
                            </ItemTemplate>
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="15%" HorizontalAlign="left" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" ForeColor="Yellow" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="报监工程名称">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("xmmc") %>' NavigateUrl='<%#string.Format("{0}?viewUrl=../PagesZHJG/Zhjg_ZlbjNew_View.aspx$LoginID={1}%PKID={2}&titleName={3}",publicViewUrl,this.WorkUser.UserID,Eval("uuid"),"质量报监-"+Eval("xmmc")) %>'
                                    Target="_blank" />
                            </ItemTemplate>
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="15%" HorizontalAlign="left" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" ForeColor="Yellow" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="质量监督机构名称" DataField="Zjjgmc">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="7%" HorizontalAlign="left" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="建设单位名称" DataField="EconCorpName">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="10%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="立项批准文号" DataField="PrjApprovalNum">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="7%" HorizontalAlign="right" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="报监日期" DataField="updateDate"  DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="7%" HorizontalAlign="center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="状态" DataField="StatusLabel">
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