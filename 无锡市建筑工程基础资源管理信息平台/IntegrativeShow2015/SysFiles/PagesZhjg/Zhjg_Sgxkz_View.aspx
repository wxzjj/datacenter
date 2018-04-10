<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zhjg_Sgxkz_View.aspx.cs" Inherits="IntegrativeShow2.SysFiles.PagesZhjg.Zhjg_Sgxkz_View" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls"
    TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../App_Themes/Themes_Standard/Stylesheet1.css" rel="Stylesheet" type="text/css" />
</head>
<body class="body_haveBackImg">
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <!-- 施工许可证基本信息 -->
        <tr>
            <td class="view_head" style="height: 25px; vertical-align: bottom">
                <div style="width: 200px; height: 25px; background: url('../Images/TitleImgs/Title_back.gif');">
                    <span class="view_tab_header">施工许可信息</span>
                </div>
            </td>
        </tr>        
        <tr>
            <td class="view_center">
                <table cellspacing="1" cellpadding="0" width="100%" align="center" border="0" class="table">
                    <tr>
                        <td class="td_text" width="15%">
                            项目编号
                        </td>
                        <td class="td_value" colspan="3">
                            <Bigdesk8:DBText ID="PrjNum" ItemName="PrjNum" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            施工许可证编号
                        </td>
                        <td class="td_value" colspan="3">
                            <Bigdesk8:DBText ID="BuilderLicenceNum" ItemName="BuilderLicenceNum" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr> 
                    <tr>
                        <td class="td_text" width="15%">
                            施工许可证内部编号
                        </td>
                        <td class="td_value" colspan="3">
                            <Bigdesk8:DBText ID="BuilderLicenceInnerNum" ItemName="BuilderLicenceInnerNum" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            项目名称
                        </td>
                        <td class="td_value" width="35%" colspan="3">
                            <Bigdesk8:DBText ID="BuilderLicenceName" ItemName="BuilderLicenceName" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            建设用地规划许可证编号
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="BuldPlanNum" ItemName="BuldPlanNum" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            建设工程规划许可证编号
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="ProjectPlanNum" ItemName="ProjectPlanNum" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr> 
                    <tr>
                        <td class="td_text" width="15%">
                            施工图审查合格书编号
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="CensorNum" ItemName="CensorNum" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            合同金额(万元)
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="ContractMoney" ItemName="ContractMoney" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr> 
                    <tr>
                        <td class="td_text" width="15%">
                            面积（平方米）
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="Area" ItemName="Area" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            建设规模
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="PrjSize" ItemName="PrjSize" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            项目经理姓名
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="ConstructorName" ItemName="ConstructorName" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            项目经理证件类型
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="CIDCardType" ItemName="CIDCardType" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            项目经理证件号码
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="ConstructorIDCard" ItemName="ConstructorIDCard" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            项目经理电话号码
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="ConstructorPhone" ItemName="ConstructorPhone" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            总监理工程师姓名
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="SupervisionName" ItemName="SupervisionName" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            总监理工程师证件类型
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="SIDCardType" ItemName="SIDCardType" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            总监理工程师证件号码
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="SupervisionIDCard" ItemName="SupervisionIDCard" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            总监理工程师电话
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="SupervisionPhone" ItemName="SupervisionPhone" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            发证日期
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="IssueCertDate" ItemName="IssueCertDate" FieldType="Date" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                        </td>
                        <td class="td_value" width="35%">
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            勘察单位名称
                        </td>
                        <td class="td_value" width="35%">
                           <%-- <Bigdesk8:DBText ID="EconCorpName" ItemName="EconCorpName" runat="server"></Bigdesk8:DBText>--%>
                           <asp:HyperLink ID="hlk_EconCorpName" runat="server"  Target="_blank"></asp:HyperLink>
                        </td>
                        <td class="td_text" width="15%">
                            勘察单位组织机构代码<br />
                            （社会信用代码）
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="EconCorpCode" ItemName="EconCorpCode" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>                
                    <tr>
                        <td class="td_text" width="15%">
                            设计单位名称
                        </td>
                        <td class="td_value" width="35%">
                           <%-- <Bigdesk8:DBText ID="DesignCorpName" ItemName="DesignCorpName" runat="server"></Bigdesk8:DBText>--%>
                           <asp:HyperLink ID="hlk_DesignCorpName" runat="server" Target="_blank"></asp:HyperLink>
                           
                        </td>
                        <td class="td_text" width="15%">
                            设计单位组织机构代码<br />
                            （社会信用代码）
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="DesignCorpCode" ItemName="DesignCorpCode" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            施工单位名称
                        </td>
                        <td class="td_value" width="35%">
                           <%-- <Bigdesk8:DBText ID="ConsCorpName" ItemName="ConsCorpName" runat="server"></Bigdesk8:DBText>--%>
                           <asp:HyperLink ID="hlk_ConsCorpName" runat="server" Target="_blank"></asp:HyperLink>
                           
                        </td>
                        <td class="td_text" width="15%">
                            施工单位组织机构代码<br />
                            （社会信用代码）
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="ConsCorpCode" ItemName="ConsCorpCode" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            施工单位安全生产许可
                        </td>
                        <td class="td_value" width="35%" colspan="3">
                            <Bigdesk8:DBText ID="SafetyCerID" ItemName="SafetyCerID" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                     <tr>
                        <td class="td_text" width="15%">
                            监理单位名称
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="SuperCorpName" ItemName="SuperCorpName" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            监理单位组织机构代码<br />
                            （社会信用代码）
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="SuperCorpCode" ItemName="SuperCorpCode" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>                    
                    <tr>
                        <td class="td_text">
                            施工从业安全人员表
                        </td>
                        <td colspan="3" class="td_gridviewvalue">
                            <asp:GridView ID="Gdv_SgxkCyryInfo" runat="server" AllowPaging="false" AutoGenerateColumns="false"
                                BorderWidth="1px" Width="100%">
                                <Columns>
                                    <asp:BoundField HeaderText="人员姓名" DataField="UserName">
                                        <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="10%" HorizontalAlign="left" />
                                        <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="所属单位名称" DataField="UserName">
                                        <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="20%" HorizontalAlign="left" />
                                        <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="安全生产管理人员类型" DataField="UserType">
                                        <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="10%" HorizontalAlign="Center" />
                                        <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="安全生产许可证编号" DataField="SafetyCerID">
                                        <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="10%" HorizontalAlign="Center" />
                                        <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="证件类型" DataField="IDCardType">
                                        <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="10%" HorizontalAlign="Center" />
                                        <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="人员证件号码" DataField="IDCard">
                                        <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="10%" HorizontalAlign="Center" />
                                        <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                                    </asp:BoundField>                                    
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>