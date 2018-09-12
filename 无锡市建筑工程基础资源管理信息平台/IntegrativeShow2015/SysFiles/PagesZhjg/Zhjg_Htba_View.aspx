<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zhjg_Htba_View.aspx.cs" Inherits="IntegrativeShow2.SysFiles.PagesZhjg.Zhjg_Htba_View" %>

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
        <!-- 安全监督基本信息 -->
        <tr>
            <td class="view_head" style="height: 25px; vertical-align: bottom">
                <div style="width: 200px; height: 25px; background: url('../Images/TitleImgs/Title_back.gif');">
                    <span class="view_tab_header">合同备案信息</span>
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
                            合同备案编号
                        </td>
                        <td class="td_value" colspan="3">
                            <Bigdesk8:DBText ID="RecordNum" ItemName="RecordNum" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>  
                    <tr>
                        <td class="td_text" width="15%">
                            合同备案内部编号
                        </td>
                        <td class="td_value" colspan="3">
                            <Bigdesk8:DBText ID="RecordInnerNum" ItemName="RecordInnerNum" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            合同编号
                        </td>
                        <td class="td_value" width="35%" colspan="3">
                            <Bigdesk8:DBText ID="ContractNum" ItemName="ContractNum" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            合同项目名称
                        </td>
                        <td class="td_value" width="35%" colspan="3">
                            <Bigdesk8:DBText ID="RecordName" ItemName="RecordName" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            合同类别
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="ContractType" ItemName="ContractType" runat="server"></Bigdesk8:DBText>
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
                            合同签订日期
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="ContractDate" ItemName="ContractDate" FieldType="Date" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            工程类型
                        </td>
                        <td class="td_value" width="35%">  
                            <Bigdesk8:DBText ID="PrjTypeLabel" ItemName="PrjTypeLabel" runat="server"></Bigdesk8:DBText>                          
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
                            发包单位名称
                        </td>
                        <td class="td_value" width="35%">
                           <%-- <Bigdesk8:DBText ID="PropietorCorpName" ItemName="PropietorCorpName" runat="server"></Bigdesk8:DBText>--%>
                              <asp:HyperLink ID="hlk_PropietorCorpName" runat="server"  Target="_blank"  Font-Underline="true" ></asp:HyperLink>
                        </td>
                        <td class="td_text" width="15%">
                            发包单位组织机构代码<br />
                            （社会信用代码）
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="PropietorCorpCode" ItemName="PropietorCorpCode" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            承包单位名称
                        </td>
                        <td class="td_value" width="35%">
                            <%--<Bigdesk8:DBText ID="ContractorCorpName" ItemName="ContractorCorpName" runat="server"></Bigdesk8:DBText>--%>
                              <asp:HyperLink ID="hlk_ContractorCorpName" runat="server"  Target="_blank"  Font-Underline="true" ></asp:HyperLink>
                            
                        </td>
                        <td class="td_text" width="15%">
                            承包单位组织机构代码<br />
                            （社会信用代码）
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="ContractorCorpCode" ItemName="ContractorCorpCode" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            联合体承包单位名称
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="UnionCorpName" ItemName="UnionCorpName" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                            联合体承包单位组织代码
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="UnionCorpCode" ItemName="UnionCorpCode" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            项目负责人
                        </td>
                        <td class="td_value" width="35%">
                           <%-- <Bigdesk8:DBText ID="PrjHead" ItemName="PrjHead" runat="server"></Bigdesk8:DBText>--%>
                              <asp:HyperLink ID="hlk_PrjHead" runat="server" Target="_blank"  Font-Underline="true" ></asp:HyperLink>
                           
                        </td>
                        <td class="td_text" width="15%">
                            项目负责人联系电话
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="PrjHeadPhone" ItemName="PrjHeadPhone" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            项目负责人证件号码
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="IDCard" ItemName="IDCard" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" width="15%">
                        </td>
                        <td class="td_value" width="35%">
                        </td>
                    </tr> 
                    <tr  style=" height:20px;">
                        <td class="td_text" width="15%">
                            合同详情
                        </td>
                        <td class="td_value" width="35%">
                           <asp:HyperLink ID="hl_htbaxxView" runat="server" Text="查看" Target="_blank" Font-Underline="true" ></asp:HyperLink>
                        </td>
                        <td class="td_text" width="15%">
                        </td>
                        <td class="td_value" width="35%">
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

