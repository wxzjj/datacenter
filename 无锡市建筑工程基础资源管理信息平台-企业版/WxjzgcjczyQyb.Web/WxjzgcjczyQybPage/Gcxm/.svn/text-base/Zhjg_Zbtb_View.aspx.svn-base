<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zhjg_Zbtb_View.aspx.cs" Inherits="WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Gcxm.Zhjg_Zbtb_View" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls"
    TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link href="../../App_Themes/WxjzgcjczyQyb_B_Theme/base.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/WxjzgcjczyQyb_B_Theme/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table id="RwjbxxTab" cellspacing="0" cellpadding="1" width="100%" border="0">
        <tr>
            <td class="editpanel">
                <div class="edittitle">
                    招标投标基本信息<img alt="" src="../Common/images/clipboard.png" width="16" height="16" />
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
                            <Bigdesk8:DBText ID="PrjNum" ItemName="PrjNum" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            中标通知书编号
                        </td>
                        <td class="td-value" colspan="3">
                            <Bigdesk8:DBText ID="TenderNum" ItemName="TenderNum" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            中标通知书内部编号
                        </td>
                        <td class="td-value" colspan="3">
                            <Bigdesk8:DBText ID="TenderInnerNum" ItemName="TenderInnerNum" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            标段名称
                        </td>
                        <td class="td-value" width="35%" colspan="3">
                            <Bigdesk8:DBText ID="TenderName" ItemName="TenderName" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            招标类型
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="TenderClass" ItemName="TenderClass" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td-text" width="15%">
                            招标方式
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="TenderType" ItemName="TenderType" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            中标日期
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="TenderResultDate" ItemName="TenderResultDate" FieldType="Date" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td-text" width="15%">
                            中标金额
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="TenderMoney" ItemName="TenderMoney" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            建设规模
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="PrjSize" ItemName="PrjSize" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td-text" width="15%">
                            面积（平方米）
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="Area" ItemName="Area" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            招标代理单位名称
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="AgencyCorpName" ItemName="AgencyCorpName" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td-text" width="15%">
                            招标代理单位组织机构代码<br />
                            （统一社会信用代码）
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="AgencyCorpCode" ItemName="AgencyCorpCode" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>                    
                    <tr>
                        <td class="td-text" width="15%">
                            中标单位名称
                        </td>
                        <td class="td-value" width="35%">
                           <%-- <Bigdesk8:DBText ID="TenderCorpName" ItemName="TenderCorpName" runat="server"></Bigdesk8:DBText>--%>
                            <asp:HyperLink ID="hlk_TenderCorpName" runat="server" Target="_blank"></asp:HyperLink>
                        </td>
                        <td class="td-text" width="15%">
                            中标单位组织机构代码<br />
                            （统一社会信用代码）
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="TenderCorpCode" ItemName="TenderCorpCode" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            项目经理/总监理工程师姓名
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="ConstructorName" ItemName="ConstructorName" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td-text" width="15%">
                            项目经理/总监理工程师电话
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="ConstructorPhone" ItemName="ConstructorPhone" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            项目经理/总监理工程师证件类型
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="IDCardType" ItemName="IDCardType" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td-text" width="15%">
                            项目经理/总监理工程师证件号码
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="ConstructorIDCard" ItemName="ConstructorIDCard" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            是否采用了三合一招标
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="shypbf" ItemName="shypbf" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td-text" width="15%">
                        </td>
                        <td class="td-value" width="35%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td style=" height:30px;">
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
