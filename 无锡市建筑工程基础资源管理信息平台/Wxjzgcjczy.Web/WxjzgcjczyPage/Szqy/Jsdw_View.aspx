<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Jsdw_View.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Szqy.Jsdw_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>建设单位基本信息</title>
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-dialog.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-form.css" rel="stylesheet"
        type="text/css" />
    <link href="../../App_Themes/MunSupervisionProject_Theme/Style.css" rel="stylesheet"
        type="text/css" />

    <script src="../../LigerUI/jquery/jquery-1.5.2.min.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/core/base.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/ligerui.min.js" type="text/javascript"></script>

    <script src="../../LigerUI/json2.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>

    <script src="../../LigerUI/js/ligerui.expand.js" type="text/javascript"></script>

    <script src="../../LigerUI/js/common.js" type="text/javascript"></script>

    <script src="../../LigerUI/js/LG.js" type="text/javascript"></script>

    <link href="../../LigerUI/css/common.css" rel="stylesheet" type="text/css" />

    <script src="../Common/scripts/Common.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>

    <script type="text/javascript">
        function openLdpsWindow() {
            var url = "../Zlct/Gzzs_Gzzs_Edit.aspx?operate=add";
            var arguments = window;
            var features = "dialogWidth=950px;dialogHeight=530px;center=yes;status=yes";
            var argReturn = window.showModalDialog(url, arguments, features);
        }
        function OpenWin(url, title, width, height, isReload) {
            var dialogOptions = { width: width, height: height, title: title, url: url, showMax: true, showMin: true, buttons: [
                { text: '发送', onclick: function(item, dialog) {
                    dialog.frame.f_send(dialog, null);
                }
                },
                { text: '关闭', onclick: function(item, dialog) {
                    dialog.close();
                }
                }
            ], isResize: true, timeParmName: 'a'
            };
            activeDialog = $.ligerDialog.open(dialogOptions);
        }
    </script>

</head>
<body style="padding: 0px; margin-top: 2px;">
    <form id="form1" runat="server">
    <table cellspacing="1" cellpadding="1" width="100%" align="center" border="0" class="table-bk">
        <%-- <tr>
                        <td colspan="4" class="td-value">
                            <span style="font-size: 15px; color: blue;">建设单位信息</span>
                        </td>
                    </tr>--%>
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
            <td class="td-text" width="15%">
                单位地址
            </td>
            <td class="td-value" width="35%">
                <Bigdesk8:DBText ID="DBText1" runat="server" ItemName="dwdz">
                </Bigdesk8:DBText>
            </td>
            <td class="td-text" width="15%">
                单位联系电话
            </td>
            <td class="td-value" width="35%">
                <Bigdesk8:DBText ID="DBText2" runat="server" ItemName="xcdh">
                </Bigdesk8:DBText>
            </td>
        </tr>
        <tr>
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
        </tr>
        <tr>
            <td class="td-text">
                法定代表人
            </td>
            <td class="td-value">
                <Bigdesk8:DBText ID="DBLabel9" runat="server" ItemName="fddbr">
                </Bigdesk8:DBText>
            </td>
            <td class="td-text">
                法人联系电话
            </td>
            <td class="td-value">
                <Bigdesk8:DBText ID="DBText5" runat="server" ItemName="fddbrdh">
                </Bigdesk8:DBText>
            </td>
        </tr>
        <tr>
            <td class="td-text">
                组织机构代码<br />
                （社会信用代码） 
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
         <tr>
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
        </tr>
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
                联系人电话
            </td>
            <td class="td-value">
                <Bigdesk8:DBText ID="DBText8" runat="server" ItemName="lxdh">
                </Bigdesk8:DBText>
            </td>
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
      <%--  <tr>
            <td class="td-value" style="text-align: center" colspan="4">
                <br />
                <input type="button" value="领导批示" onclick="openLdpsWindow()" class="button" style="width: 100px;
                    height: 30px;" />
            </td>
        </tr>--%>
    </table>
    <%--   <div style="padding: 2px 0px 2px 0px;">
        <div id="maingrid" style="background-color: White;">
        </div>
    </div>--%>
    <%-- <table cellspacing="1" cellpadding="0" width="100%" align="center" border="0" class="table-bk">
        <tr>
            <td class="td-value" style="text-align: center">
                <br />
                <input type="button" value="领导批示" onclick="openLdpsWindow()" class="button" style="width: 100px;
                    height: 30px;" />
            </td>
        </tr>
    </table>--%>
    </form>
</body>
</html>
