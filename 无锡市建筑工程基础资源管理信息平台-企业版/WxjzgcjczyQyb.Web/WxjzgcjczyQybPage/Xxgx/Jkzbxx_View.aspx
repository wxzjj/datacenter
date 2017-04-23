<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Jkzbxx_View.aspx.cs" Inherits="WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Xxgx.Jkzbxx_View" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
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
                            接口名称
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="DBLabel1" runat="server" ItemName="apiFromSys">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text" width="15%">
                        </td>
                        <td class="td-value" width="35%">
                        </td>
                        <%-- <td class="td-text" width="15%">
                            接口来自哪个系统
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="DBText4" runat="server" ItemName="apiFromSys">
                            </Bigdesk8:DBText>
                        </td>--%>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            接口地址
                        </td>
                        <td class="td-value" width="85%" colspan="3" style="height: 50px;">
                            <Bigdesk8:DBText ID="DBText1" runat="server" ItemName="apiUrl">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            接口描述
                        </td>
                        <td class="td-value" width="85%" colspan="3" style="height: 50px;">
                            <Bigdesk8:DBText ID="DBText6" runat="server" ItemName="apiDescript">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <%-- <tr>
                        <td class="td-text">
                            接口创建时间
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText13" runat="server" ItemName="apiCjTime" ItemType="Date">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                        </td>
                        <td class="td-value">
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="td-text">
                            接口开关状态
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText3" runat="server">
                            </Bigdesk8:DBText>
                        </td>
                        <td class="td-text">
                            接口运行状态
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="DBText12" runat="server">
                            </Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            接口运行异常信息
                        </td>
                        <td class="td-value" width="85%" colspan="3" style="height: 50px;">
                            <Bigdesk8:DBText ID="DBText2" runat="server" ItemName="apiRunMessage">
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
