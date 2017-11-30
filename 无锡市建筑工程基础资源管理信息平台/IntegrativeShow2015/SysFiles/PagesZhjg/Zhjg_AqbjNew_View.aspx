<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zhjg_AqbjNew_View.aspx.cs"
    Inherits="IntegrativeShow2.SysFiles.PagesZhjg.Zhjg_AqbjNew_View" %>

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
        <tr>
            <td class="view_head" style="height: 25px; vertical-align: bottom">
                <!--<img src="../Images/TitleImgs/Title_gcjbxx.gif" height="25px" alt="" />-->
                <div style="width:200px;height:25px; background:url('../Images/TitleImgs/Title_back.jpg');">&nbsp;&nbsp;&nbsp;安监申报表</div>
            </td>
        </tr>
        <tr>
            <td class="view_center">
                <table cellspacing="1" cellpadding="0" width="100%" align="center" border="0" class="table">
                <!--
                    <tr>
                        <td class="td_text" width="15%">
                            项目编号
                        </td>
                        <td class="td_value" colspan="3">
                            <Bigdesk8:DBText ID="PrjNum" ItemName="xmbm" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            安全监督编码
                        </td>
                        <td class="td_value" colspan="3">
                            <Bigdesk8:DBText ID="db_aqjdbm" ItemName="aqjdbm" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            报监工程名称
                        </td>
                        <td class="td_value" width="35%" colspan="3">
                            <Bigdesk8:DBText ID="gcmc" ItemName="gcmc" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            施工招标编码
                        </td>
                        <td class="td_value" width="35%" colspan="3">
                            <Bigdesk8:DBText ID="sgzbbm" ItemName="sgzbbm" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            安全监督机构名称
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="Aqjdjgmc" ItemName="Aqjdjgmc" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            安全监督组织机构代码<br />
                            （社会信用代码）
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="sdCode" ItemName="sdcode" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            工程造价（万元）
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="gcgk_yszj" ItemName="gcgkYszj" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            工程面积（平方米）
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="gcgk_jzmj" ItemName="gcgkJzmj" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            结构类型
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="gcgk_jglx" ItemName="gcgkJglx" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            层次
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="gcgk_cc" ItemName="gcgkCc" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            经度
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="gis_jd" ItemName="gis_jd" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            纬度
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="gis_wd" ItemName="gis_wd" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            报监日期
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="bjrq" ItemName="bjrq" FieldType="Date" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                        </td>
                        <td class="td_value" width="35%">
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            合同开工日期
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="gcgk_kgrq" ItemName="gcgkKgrq" FieldType="Date" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            合同竣工日期
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="gcgk_jhjgrq" ItemName="gcgkJhjgrq" FieldType="Date" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            总包单位名称
                        </td>
                        <td class="td_value" width="85%"  colspan="3">
                           <%-- <Bigdesk8:DBText ID="zbdw_dwmc" ItemName="zbdw_dwmc" runat="server"></Bigdesk8:DBText>--%>
                           <asp:HyperLink ID="hlk_dwmc" runat="server" Target="_blank"></asp:HyperLink>
                        </td>
                    </tr>
                      <tr>
                        <td class="td_text" width="15%">
                            总包单位组织机构代码<br />
                            （社会信用代码）
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="zbdw_dwdm" ItemName="zbdwDwdm" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            总包单位安全生产许可证
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="zbdw_aqxkzh" ItemName="zbdwAqxkzh" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            总包单位注册建造师
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="zbdw_zcjzs" ItemName="zbdwZcjzs" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            总包单位注册建造师身份证号
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="zbdw_zcjzsdm" ItemName="zbdwZcjzsdm" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            总包单位注册建造师电话
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="zbdw_zcjzs_lxdh" ItemName="zbdwZcjzslxdh" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                        </td>
                        <td class="td_value" width="35%">
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            总包单位专职安全员1
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="zbdw_aqy" ItemName="zbdwAqy" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            总包单位专职安全员证号1
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="zbdw_aqyzh" ItemName="zbdwAqyzh1" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                     <tr>
                        <td class="td_text" width="15%">
                            总包单位专职安全员2
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="DBText1" ItemName="zbdwAqy2" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            总包单位专职安全员证号2
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="DBText2" ItemName="zbdwAqyzh2" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                     <tr>
                        <td class="td_text" width="15%">
                            总包单位专职安全员3
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="DBText3" ItemName="zbdwAqy3" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            总包单位专职安全员证号3
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="DBText4" ItemName="zbdwAqyzh3" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            监理单位名称
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="jldw_dwmc" ItemName="jldwDwmc" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            监理单位组织机构代码<br />
                            （社会信用代码）
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="jldw_dwdm" ItemName="jldwDwdm" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            总监姓名
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="jldw_xmzj" ItemName="jldwXmzj" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            总监身份证号
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="jldw_zjdm" ItemName="jldwZjdm" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        
                        <td class="td_text" width="15%">
                            监理员1
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="jldw_jlgcs" ItemName="jldwJlgcs1" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                             监理员1
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="DBText5" ItemName="jldwJlgcs2" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        
                        <td class="td_text" width="15%">
                            监理员3
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="DBText6" ItemName="jldwJlgcs3" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                          
                        </td>
                        <td class="td_value" width="35%">
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            备注
                        </td>
                        <td class="td_value" width="35%" colspan="3">
                            <Bigdesk8:DBText ID="bz" ItemName="bz" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    -->
                    <!-- 
                      <tr>
                        <td class="td_text">
                            安全报监责任单位及人员
                        </td>
                        <td colspan="3" class="td_gridviewvalue">
                            <asp:GridView ID="Gdv_SgxkCyryInfo" runat="server" AllowPaging="false" AutoGenerateColumns="false"
                                BorderWidth="1px" Width="100%">
                                <Columns>
                                     <asp:BoundField HeaderText="单位名称" DataField="CorpName">
                                        <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="15%" HorizontalAlign="left" />
                                        <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="组织机构代码（社会信用代码）" DataField="CorpCode">
                                        <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="8%" HorizontalAlign="Center" />
                                        <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField HeaderText="姓名" DataField="UserName">
                                        <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="5%" HorizontalAlign="Center" />
                                        <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                                    </asp:BoundField>
                                     <asp:BoundField HeaderText="证件类型" DataField="IDCardType">
                                        <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="5%" HorizontalAlign="Center" />
                                        <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="证件号码" DataField="IDCard">
                                        <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="10%" HorizontalAlign="Center" />
                                        <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                                    </asp:BoundField>
                                    
                                    
                                    <asp:BoundField HeaderText="电话" DataField="UserPhone">
                                        <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="7%" HorizontalAlign="Center" />
                                        <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="安全生产管理人员类型" DataField="UserType">
                                        <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="7%" HorizontalAlign="Center" />
                                        <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                                    </asp:BoundField>
                                    
                                         <asp:BoundField HeaderText="安全生产许可证编号" DataField="SafetyCerID">
                                        <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="8%" HorizontalAlign="Center" />
                                        <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                                    </asp:BoundField>
                                              <asp:BoundField HeaderText="安全生产考核合格证书编号" DataField="CertID">
                                        <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="8%" HorizontalAlign="Center" />
                                        <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                                    </asp:BoundField>       
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    -->
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
