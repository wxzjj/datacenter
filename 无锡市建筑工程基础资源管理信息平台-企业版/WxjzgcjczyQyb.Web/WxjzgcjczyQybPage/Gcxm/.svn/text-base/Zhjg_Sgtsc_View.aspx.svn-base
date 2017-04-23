<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zhjg_Sgtsc_View.aspx.cs"
    Inherits="WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Gcxm.Zhjg_Sgtsc_View" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../App_Themes/WxjzgcjczyQyb_B_Theme/base.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/WxjzgcjczyQyb_B_Theme/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table id="RwjbxxTab" cellspacing="0" cellpadding="1" width="100%" border="0">
        <tr>
            <td class="editpanel">
                <div class="edittitle">
                    施工图审查信息<img alt="" src="../Common/images/clipboard.png" width="16" height="16" />
                    <!-- name="edit3"  要和下面的td中的一致-->
                </div>
                <div class="navline" style="margin-bottom: 4px; margin-top: 4px;">
                </div>
            </td>
        </tr>
        <tr>
            <td class="editbox" name="edit3">
                <table id="Table1" width="100%" border="0" cellspacing="1" cellpadding="0" class="table-bk"
                    runat="server">
                    <tr>
                        <td class="td-text" width="15%">
                            项目编号
                        </td>
                        <td class="td-value" colspan="3">
                            <Bigdesk8:DBText ID="txtPrjNum" ItemName="PrjNum" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            施工图审查合格书编号
                        </td>
                        <td class="td-value" colspan="3">
                            <Bigdesk8:DBText ID="txtCensorNum" ItemName="CensorNum" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            施工图审查合格证号
                        </td>
                        <td class="td-value" colspan="3">
                            <Bigdesk8:DBText ID="DBLabel1" ItemName="CensorInnerNum" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            项目名称
                        </td>
                        <td class="td-value" width="35%" colspan="3">
                            <Bigdesk8:DBText ID="txtPrjName" ItemName="PrjName" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            建设规模
                        </td>
                        <td class="td-value" width="35%" colspan="3">
                            <Bigdesk8:DBText ID="txtPrjSize" ItemName="PrjSize" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            一次审查是否通过
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="txtOneCensorIsPass" ItemName="OneCensorIsPass" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td-text" width="15%">
                            审查完成日期
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="txtCensorEDate" ItemName="CensorEDate" FieldType="Date" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            一次审查时违反强条数
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="OneCensorWfqtCount" ItemName="OneCensorWfqtCount" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td-text" width="15%">
                            一次审查时违反的强条
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="OneCensorWfqtContent" ItemName="OneCensorWfqtContent" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            施工图审查机构名称
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="CensorCorpName" ItemName="CensorCorpName" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td-text" width="15%">
                            施工图审查机构组织机构代码<br />
                            （统一社会信用代码）
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="CensorCorpCode" ItemName="CensorCorpCode" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            勘察单位名称
                        </td>
                        <td class="td-value" width="35%">
                            <%--  <Bigdesk8:DBText ID="EconCorpName" ItemName="EconCorpName" runat="server"></Bigdesk8:DBText>--%>
                            <asp:HyperLink ID="hlk_EconCorpName" runat="server" Target="_blank"></asp:HyperLink>
                        </td>
                        <td class="td-text" width="15%">
                            勘察单位组织机构代码<br />
                            （统一社会信用代码）
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="EconCorpCode" ItemName="EconCorpCode" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            设计单位名称
                        </td>
                        <td class="td-value" width="35%">
                            <%--<Bigdesk8:DBText ID="DesignCorpName" ItemName="DesignCorpName" runat="server"></Bigdesk8:DBText>--%>
                            <asp:HyperLink ID="hlk_DesignCorpName" runat="server" Target="_blank"></asp:HyperLink>
                        </td>
                        <td class="td-text" width="15%">
                            设计单位组织机构代码<br />
                            （统一社会信用代码）
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="DesignCorpCode" ItemName="DesignCorpCode" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            勘察合同编码
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="EconCorpNum" ItemName="EconCorpNum" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td-text" width="15%">
                            设计合同编码
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="DesignCorpNum" ItemName="DesignCorpNum" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            勘察设计审图人员
                        </td>
                        <td colspan="3" class="td-value">
                            <asp:GridView ID="Gdv_SgtscRyInfo" runat="server" AllowPaging="false" AutoGenerateColumns="false"
                                BorderWidth="1px" Width="100%">
                                <Columns>
                                    <asp:BoundField HeaderText="姓名" DataField="UserName">
                                        <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                                        <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="所属单位名称" DataField="CorpName">
                                        <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                                        <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="注册类型及等级" DataField="SpecialtyTyp">
                                        <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                                        <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="承担角色" DataField="PrjDuty">
                                        <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                                        <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="证件类型" DataField="IDCardType">
                                        <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                                        <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="证件号码" DataField="IDCard">
                                        <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                                        <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
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
