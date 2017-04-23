<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zhjg_Zlbj_View.aspx.cs" Inherits="IntegrativeShow2.SysFiles.PagesZhjg.Zhjg_Zlbj_View" %>

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
        <!-- 质量报监基本信息 -->
        <tr>
            <td class="view_head" style="height: 25px; vertical-align: bottom">
                <img src="../Images/TitleImgs/Title_gcjbxx.gif" height="25px" alt="" />
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
                            质量监督编码
                        </td>
                        <td class="td_value" colspan="3">
                            <Bigdesk8:DBText ID="zljdbm" ItemName="zljdbm" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            施工招标编码
                        </td>
                        <td class="td_value" colspan="3">
                            <Bigdesk8:DBText ID="sgzbbm" ItemName="sgzbbm" runat="server"></Bigdesk8:DBText>
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
                            质量监督机构名称
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="zljdjgmc" ItemName="zljdjgmc" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            质量监督组织机构代码<br />
                            （社会信用代码）
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="zjzbm" ItemName="zjzbm" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr> 
                    <tr>
                        <td class="td_text" width="15%">
                            工程造价（万元）
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="gczj" ItemName="gczj" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            工程面积（平方米）
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="jzmj" ItemName="jzmj" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr> 
                    <tr>
                        <td class="td_text" width="15%">
                            道路长度（米）
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="dlcd" ItemName="dlcd" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            结构类型
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="jglx" ItemName="jglx" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            层次
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="cc" ItemName="cc" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            建筑规模
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="jzgm" ItemName="jzgm" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            报监日期
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="sbrq" ItemName="sbrq" FieldType="Date" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            合同开工日期
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="kgrq" ItemName="kgrq" FieldType="Date" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            合同竣工日期
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="jhjgrq" ItemName="jhjgrq" FieldType="Date" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            形象进度
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="xxjd" ItemName="xxjd" runat="server"></Bigdesk8:DBText>
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
                    <tr>
                        <td class="td_text">
                            质量报监责任单位及人员
                        </td>
                        <td colspan="3" class="td_gridviewvalue">
                            <asp:GridView ID="Gdv_ZlbjZrryInfo" runat="server" AllowPaging="false" AutoGenerateColumns="false"
                                BorderWidth="1px" Width="100%">
                                <Columns>
                                    <asp:BoundField HeaderText="顺序号" DataField="Xh">
                                        <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="5%" HorizontalAlign="Center" />
                                        <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                                    </asp:BoundField>
                                      <asp:BoundField HeaderText="单位类型" DataField="dwlx">
                                        <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="7%" HorizontalAlign="Center" />
                                        <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                                    </asp:BoundField>
                                     <asp:BoundField HeaderText="单位名称" DataField="Dwmc">
                                        <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="20%" HorizontalAlign="left" />
                                        <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="项目负责人" DataField="xmfzrxm">
                                        <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="5%" HorizontalAlign="Center" />
                                        <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="项目负责人身份证号" DataField="xmfzrdm">
                                        <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="10%" HorizontalAlign="Center" />
                                        <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="项目负责人电话" DataField="xmfzr_lxdh">
                                        <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="10%" HorizontalAlign="Center" />
                                        <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="项目技术负责人" DataField="jsfzr">
                                        <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="10%" HorizontalAlign="Center" />
                                        <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="质量员" DataField="Zly">
                                        <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="5%" HorizontalAlign="Center" />
                                        <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="取样员" DataField="qyy">
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
