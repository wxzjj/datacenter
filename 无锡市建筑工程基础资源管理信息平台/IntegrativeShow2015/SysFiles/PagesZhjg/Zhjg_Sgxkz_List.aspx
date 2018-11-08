<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zhjg_Sgxkz_List.aspx.cs"
    Inherits="IntegrativeShow2.SysFiles.PagesZhjg.Zhjg_Sgxkz_List" %>

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
                                        项目编号
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBTextBox ID="DBTextBox3" ItemName="Prjnum" runat="server"></Bigdesk8:DBTextBox>
                                    </td>
                                    <td class="td_text" width="15%">
                                        项目名称
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBTextBox ID="DBTextBox4" ItemName="PrjName" runat="server"></Bigdesk8:DBTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_text" width="15%">
                                        施工许可证编号
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBTextBox ID="BuilderLicenceNum" ItemName="BuilderLicenceNum" runat="server"></Bigdesk8:DBTextBox>
                                    </td>
                                    <td class="td_text" width="15%">
                                        施工许可项目名称
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBTextBox ID="BuilderLicenceName" ItemName="BuilderLicenceName" runat="server"></Bigdesk8:DBTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_text" width="15%">
                                        勘察单位名称
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBTextBox ID="DBTextBox1" ItemName="EconCorpName" runat="server"></Bigdesk8:DBTextBox>
                                    </td>
                                    <td class="td_text" width="15%">
                                        设计单位名称
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBTextBox ID="DBTextBox2" ItemName="DesignCorpName" runat="server"></Bigdesk8:DBTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_text" width="15%">
                                        施工单位名称
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBTextBox ID="ConsCorpName" ItemName="ConsCorpName" runat="server"></Bigdesk8:DBTextBox>
                                    </td>
                                    <td class="td_text" width="15%">
                                        监理单位名称
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBTextBox ID="db_SuperCorpName" ItemName="SuperCorpName" runat="server"></Bigdesk8:DBTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_text" width="15%">
                                        建设单位名称
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBTextBox ID="BuildCorpName" ItemName="BuildCorpName" runat="server"></Bigdesk8:DBTextBox>
                                    </td>
                                    <td class="td_text" width="15%">
                                        发证日期
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBTextBox ID="IssueCertDate1" FieldType="Date" Width="40%" ItemName="IssueCertDate"
                                            ItemRelation="GreaterThanOrEqual" runat="server"></Bigdesk8:DBTextBox>
                                        至
                                        <Bigdesk8:DBTextBox ID="IssueCertDate2" FieldType="Date" Width="40%" ItemName="IssueCertDate"
                                            ItemRelation="LessThanOrEqual" runat="server"></Bigdesk8:DBTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    
                                    <td class="td_text" width="15%">
                                        项目属地
                                    </td>
                                    <td class="td_value" width="35%">
                                        <Bigdesk8:DBDropDownList ID="DDL_xmsd" ItemName="CountyNum" DropDownListType="Value"
                                            ForeColor="Blue" ToolTip="Xzqdm" runat="server">
                                        </Bigdesk8:DBDropDownList>
                                    </td>
                                    <td class="td_value" width="50%" colspan="2" >
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
                <Bigdesk8:PowerDataGrid ID="Gdv_SgxkzInfo" runat="server" OnPageIndexChanging="powerGridView_PageIndexChanging"
                    PageSize="20" Width="100%" DataKeyNames="PKID" AutoGenerateColumns="false" OnRowDataBound="powerGridView_RowDataBound"
                    AllowPaging="true">
                    <Columns>
                        <asp:BoundField HeaderText="项目编号" DataField="PrjNum">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="7%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <%--    <asp:BoundField HeaderText="项目名称" DataField="PrjName">
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
                        <asp:BoundField HeaderText="施工许可证编号" DataField="BuilderLicenceNum">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="7%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="施工许可项目名称">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("BuilderLicenceName") %>'
                                    NavigateUrl='<%#string.Format("{0}?viewUrl=../PagesZHJG/Zhjg_Sgxkz_View.aspx$LoginID={1}%PKID={2}&titleName={3}",publicViewUrl,this.WorkUser.UserID,Eval("PKID"),"施工许可-"+Eval("BuilderLicenceName")) %>'
                                    Target="_blank" />
                            </ItemTemplate>
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="18%" HorizontalAlign="left" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" ForeColor="Yellow" />
                        </asp:TemplateField>
                        <%--  <asp:BoundField HeaderText="施工图审查合格书编号" DataField="CensorNum">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="7%" HorizontalAlign="left" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>   --%>
                        <%--   <asp:BoundField HeaderText="勘察单位名称" DataField="EconCorpName">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="11%" HorizontalAlign="left" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>   --%>
                        <asp:TemplateField HeaderText="建设单位名称">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("BuildCorpName") %>'
                                    NavigateUrl='<%#string.Format("/WxjzgcjczyPage/Szqy/QyxxToolBar.aspx?qyid={0}",Eval("BuildCorpCode")) %>'
                                    Target="_blank" Enabled='<%# Eval("BuildCorpCode")!=DBNull.Value %>' />
                            </ItemTemplate>
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="11%" HorizontalAlign="left" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" ForeColor="Yellow" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="勘察单位名称">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("EconCorpName") %>'
                                    NavigateUrl='<%#string.Format("/WxjzgcjczyPage/Szqy/QyxxToolBar.aspx?qyid={0}",Eval("kcqyID")) %>'
                                    Target="_blank" Enabled='<%# Eval("kcqyID")!=DBNull.Value %>' />
                            </ItemTemplate>
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="11%" HorizontalAlign="left" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" ForeColor="Yellow" />
                        </asp:TemplateField>
                        <%-- <asp:BoundField HeaderText="设计单位名称" DataField="DesignCorpName">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="11%" HorizontalAlign="left" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>--%>
                        <asp:TemplateField HeaderText="设计单位名称">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("DesignCorpName") %>'
                                    NavigateUrl='<%#string.Format("/WxjzgcjczyPage/Szqy/QyxxToolBar.aspx?qyid={0}&befrom=sjdw",Eval("sjqyID")) %>'
                                    Target="_blank" Enabled='<%# Eval("sjqyID")!=DBNull.Value %>' />
                            </ItemTemplate>
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="11%" HorizontalAlign="left" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" ForeColor="Yellow" />
                        </asp:TemplateField>
                        <%--   <asp:BoundField HeaderText="施工单位名称" DataField="ConsCorpName">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="11%" HorizontalAlign="left" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>--%>
                        <asp:TemplateField HeaderText="施工单位名称">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("ConsCorpName") %>'
                                    NavigateUrl='<%#string.Format("/WxjzgcjczyPage/Szqy/QyxxToolBar.aspx?qyid={0}",Eval("sgqyID")) %>'
                                    Target="_blank" Enabled='<%# Eval("sgqyID")!=DBNull.Value %>' />
                            </ItemTemplate>
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="11%" HorizontalAlign="left" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" ForeColor="Yellow" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="监理单位名称" DataField="SuperCorpName">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="11%" HorizontalAlign="center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="发证日期" DataField="IssueCertDate">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="6%" HorizontalAlign="center" />
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
