<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Jsdw_View.aspx.cs" Inherits="WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Sczt.Jsdw_View" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../App_Themes/WxjzgcjczyQyb_B_Theme/base.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/WxjzgcjczyQyb_B_Theme/main.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            text-align: center;
            background-color: #F1F1F1;
            padding-left: 5px;
            padding-right: 5px;
            color: #595857;
            height: 27px;
        }
        .style2
        {
            text-align: left;
            background-color: #FFFFFF;
            color: #00557D;
            padding-left: 5px;
            height: 27px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table id="RwjbxxTab" cellspacing="0" cellpadding="1" width="100%" border="0">
        <tr>
            <td class="editpanel">
                <div class="edittitle">
                    基本信息<img alt="" src="../Common/images/clipboard.png" width="16" height="16" />
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
                            建设单位名称
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="DBLabel1" runat="server" ItemName="jsdw">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text" width="15%">
                            单位类型
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="DBText4" runat="server" ItemName="dwfl">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" width="15%">
                            单位地址
                        </td>
                        <td class="style2" width="35%">
                            <Bigdesk8:DBText ID="DBText1" runat="server" ItemName="dwdz">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            法定代表人
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBLabel9" runat="server" ItemName="fddbr">
                            </Bigdesk8:DBText>
                        </td>
                        <%--     <td class="style1" width="15%">
                            单位联系电话
                        </td>
                        <td class="style2" width="35%">
                            <Bigdesk8:DBText ID="DBText2" runat="server" ItemName="xcdh">
                            </Bigdesk8:DBText>
                        </td>--%>
                    </tr>
                    <%--      <tr>
                        <td class="td-text">
                            单位邮编
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText9" runat="server" ItemName="yb">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            单位传真
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText3" runat="server" ItemName="fax">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>--%>
                   <%-- <tr>
                          <td class="td-text">
                            法人联系电话
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText5" runat="server" ItemName="fddbrdh">
                            </Bigdesk8:DBText>
                        </td> 
                    </tr>--%>
                    <tr>
                        <td class="td-text">
                            组织机构代码<br />
                            （统一社会信用代码）
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText6" runat="server" ItemName="zzjgdm">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            营业执照
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText11" runat="server" ItemName="yyzz">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            股权结构
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText12" runat="server" ItemName="gqjg">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            主管部门
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText13" runat="server" ItemName="zgbm">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            成立时间
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText14" runat="server" ItemName="clsj">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            批准时间
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText15" runat="server" ItemName="pzsj">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            资质证书号
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText18" runat="server" ItemName="zzzsbh">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            资质等级
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText19" runat="server" ItemName="zzdj">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            资质有效起日期
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText20" runat="server" ItemName="zzyxqrq">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            资质有效止日期
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText21" runat="server" ItemName="zzyxzrq">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <%--  <tr>
                        <td class="td-text">
                            开户银行
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText22" runat="server" ItemName="khyh">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            开户银行账号
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText26" runat="server" ItemName="khyhzh">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="td-text">
                            网址
                        </td>
                        <td class="td-value" colspan="3">
                            <Bigdesk8:DBText ID="DBText24" runat="server" ItemName="wz">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            注册资本
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText23" runat="server" ItemName="zczb">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            电子邮箱
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText25" runat="server" ItemName="email">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            联系人
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText7" runat="server" ItemName="lxr">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                        </td>
                        <td class="td-value">
                        </td>
                        <%--     <td class="td-text">
                            联系人电话
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText8" runat="server" ItemName="lxdh">
                            </Bigdesk8:DBText>
                        </td>--%>
                    </tr>
                    <tr>
                        <td class="td-text">
                            备注
                        </td>
                        <td class="td-value" colspan="3">
                            <Bigdesk8:DBText ID="DBText10" runat="server" ItemName="bz">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            数据来源
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText16" runat="server" ItemName="tag">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            更新时间
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText17" runat="server" ItemName="xgrqsj" ItemType="DateTime">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
