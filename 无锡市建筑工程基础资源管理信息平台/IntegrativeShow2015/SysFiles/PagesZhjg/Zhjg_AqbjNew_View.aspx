<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zhjg_AqbjNew_View.aspx.cs"
    Inherits="IntegrativeShow2.SysFiles.PagesZhjg.Zhjg_AqbjNew_View" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
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
                <div style="width: 200px; height: 25px; background: url('../Images/TitleImgs/Title_back.gif');">
                    &nbsp;&nbsp;&nbsp;安监申报表</div>
            </td>
        </tr>
        <tr>
            <td class="view_center">
                <table cellspacing="1" cellpadding="0" width="100%" align="center" border="0" class="table">
                    <tr>
                        <td class="td_text" width="15%">
                            安监申报表编号
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="uuid" ItemName="uuid" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            安监项目名称
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="xmmc" ItemName="xmmc" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            项目编号
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="PrjNum" ItemName="PrjNum" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            立项项目名称
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="PrjName" ItemName="PrjName" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            安全监督机构名称
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="Ajjgmc" ItemName="Ajjgmc" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            安全监督机构组织机构代码
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="AjCorpCode" ItemName="AjCorpCode" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            建设规模
                        </td>
                        <td class="td_value" width="35%" colspan="3">
                            <Bigdesk8:DBText ID="PrjSize" ItemName="PrjSize" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            建设单位名称
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="EconCorpName" ItemName="EconCorpName" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            建设单位组织机构代码
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="EconCorpCode" ItemName="EconCorpCode" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            立项批准文号
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="PrjApprovalNum" ItemName="PrjApprovalNum" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            建设用地规划许可证号
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="BuldPlanNum" ItemName="BuldPlanNum" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            建设工程规划许可证号
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="ProjectPlanNum" ItemName="ProjectPlanNum" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            工程用途
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="PrjFunctionLabel" ItemName="PrjFunctionLabel" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            所在市州
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="CityLabel" ItemName="CityLabel" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            所在县区
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="CountryLabel" ItemName="CountryLabel" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            项目分类
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="PrjTypeLabel" ItemName="PrjTypeLabel" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            项目分类小类
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="sPrjTypeLabel" ItemName="sPrjTypeLabel" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            申办人
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="sbr" ItemName="sbr" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            申办人移动电话
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="sbryddh" ItemName="sbryddh" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            是否是装配式
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="sfzps" ItemName="sfzps" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            是否是保障房
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="sfbz" ItemName="sfbz" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            坐标经度
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="jdz" ItemName="jdz" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            坐标纬度
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="wdz" ItemName="wdz" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            项目面积
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="mj" ItemName="mj" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            项目造价
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="zj" ItemName="zj" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            结构层次
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="jgcc" ItemName="jgcc" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            申报目标
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="sbmb" ItemName="sbmb" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            是否符合安装远程监控条件
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="sfjk" ItemName="sfjk" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            施工许可证号
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="sgxkz" ItemName="sgxkz" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            报监日期
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="updateDate" ItemName="updateDate" FieldType="Date" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            记录登记日期
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="CreateDate" ItemName="CreateDate" FieldType="Date" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            四库更新日期
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="UpdateTime" ItemName="UpdateTime" FieldType="Date" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            数据更新标识
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="UpdateFlag" ItemName="UpdateFlag" FieldType="Date" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
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
    <br />
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td class="view_head" style="height: 25px; vertical-align: bottom">
                <div style="width: 200px; height: 25px; background: url('../Images/TitleImgs/Title_back.gif');">
                    &nbsp;&nbsp;&nbsp;相关合同（<%=this.Gdv_AqbjNew_ht.Rows.Count %>）</div>
            </td>
        </tr>
        <tr>
            <td class="view_center">
                <asp:GridView ID="Gdv_AqbjNew_ht" runat="server" AllowPaging="false" AutoGenerateColumns="false"
                    BorderWidth="1px" Width="100%">
                    <Columns>
                        <asp:BoundField HeaderText="合同备案编码" DataField="RecordNum">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="13%" HorizontalAlign="left" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="合同类别" DataField="ContractTypeLabel">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="7%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="合同金额（万元）" DataField="ContractMoney">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="10%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="承包单位组织机构代码" DataField="CorpCode">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="10%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="承包单位名称" DataField="CorpName">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="13%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="合同建设规模" DataField="PrjSize">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="30%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="项目负责人" DataField="xmfzr">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="7%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="项目负责人身份证号" DataField="xmfzrsfzh">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="10%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td class="view_head" style="height: 25px; vertical-align: bottom">
                <div style="width: 200px; height: 25px; background: url('../Images/TitleImgs/Title_back.gif');">
                    &nbsp;&nbsp;&nbsp;单位人员（<%=this.Gdv_AqbjNew_dwry.Rows.Count %>）</div>
            </td>
        </tr>
        <tr>
            <td class="view_center">
                <asp:GridView ID="Gdv_AqbjNew_dwry" runat="server" AllowPaging="false" AutoGenerateColumns="false"
                    BorderWidth="1px" Width="100%">
                    <Columns>
                        <asp:BoundField HeaderText="单位类别" DataField="dwlx">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="6%" HorizontalAlign="left" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="单位名称" DataField="CorpName">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="12%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="单位组织机构代码" DataField="CorpCode">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="8%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="岗位" DataField="gw">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="8%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="姓名" DataField="xm">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="8%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="身份证号码" DataField="idCard">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="10%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="手机号码" DataField="mp">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="10%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="资质证书编号" DataField="zzzs">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="10%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="资质类型和等级" DataField="zzlxdj">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="5%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="资质有效期" DataField="zzyxq">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="5%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="资格类型及证号" DataField="zgzh">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="10%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="计划进场时间" DataField="jhjcsj">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="5%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="计划出场时间" DataField="jhccsj">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="5%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td class="view_head" style="height: 25px; vertical-align: bottom">
                <div style="width: 200px; height: 25px; background: url('../Images/TitleImgs/Title_back.gif');">
                    &nbsp;&nbsp;&nbsp;材料清单（<%=this.Gdv_AqbjNew_clqd.Rows.Count %>）</div>
            </td>
        </tr>
        <tr>
            <td class="view_center">
                <asp:GridView ID="Gdv_AqbjNew_clqd" runat="server" AllowPaging="false" AutoGenerateColumns="false"
                    BorderWidth="1px" Width="100%">
                    <Columns>
                        <asp:BoundField HeaderText="序号" DataField="xh">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="5%" HorizontalAlign="left" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="申报资料" DataField="sbzl">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="20%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="证书（合同）号" DataField="zshth">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="15%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="办理日期" DataField="blrq">
                            <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="10%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="文件查看">
                            <ItemTemplate>
                                
                                 <a href='<%# Eval("smjdz").ToString() == "" ? "#" : string.Format("{0}{1}", Eval("smjdz"), Eval("smjmc"))%>'  Target="_blank"><%# Eval("smjdz").ToString() == "" ? "" : "查看"%></a>
                                
                               
                                <!--<asp:HyperLink ID="HyperLink_View" runat="server" Text='查看' NavigateUrl='<%# Eval("smjdz").ToString() == "" ? "void(0)" : string.Format("{0}{1}", Eval("smjdz"), Eval("smjmc"))%>'
                                    Target="_blank" />-->
                            </ItemTemplate>
                           <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="10%" HorizontalAlign="Center" />
                            <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                        </asp:TemplateField>
                        
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <br />
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
