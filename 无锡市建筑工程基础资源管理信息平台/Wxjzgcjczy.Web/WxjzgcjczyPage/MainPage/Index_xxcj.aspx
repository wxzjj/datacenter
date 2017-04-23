<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index_xxcj.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.MainPage.Index_xxcj" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>信息采集</title>
    <link href="../Common/css/IndexStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-dialog.css" rel="stylesheet"
        type="text/css" />
    <link href="../../SparkClient/DateTime_ligerui.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/MunSupervisionProject_Theme/Style.css" rel="stylesheet"
        type="text/css" />

    <script src="../../LigerUI/jquery/jquery-1.5.2.min.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/ligerui.min.js" type="text/javascript"></script>

    <script src="../../SparkClient/DateTime_ligerui.js" type="text/javascript"></script>

    <script src="../../SparkClient/control_ligerui.js" type="text/javascript"></script>

    <script src="../../SparkClient/jquery.validate.min.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>

    <script src="../../LigerUI/js/common.js" type="text/javascript"></script>

    <script src="../../LigerUI/js/LG.js" type="text/javascript"></script>

    <script src="../Common/scripts/frame.js" type="text/javascript"></script>

</head>
<body style="background-color: #4364A9; padding: 0;">
    <form id="Form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="padding: 0px 0px 0px 0px;">
        <tr>
            <td width="150px" valign="top">
                <div id="leftmenu" style="position: absolute; top: 0px;">
                    <input type="hidden" id="hd2" value="" />
                    <input type="hidden" id="hd2_num" value="" />
                    <table id="menuTable" border="0" cellspacing="0" cellpadding="0" width="150">
                    </table>
                </div>
            </td>
            <td id="ItemTd" bgcolor="#EEEEEE" style="padding-left: 3px; padding-top: 0; padding-bottom: 0;
                padding-right: 0;">
                <iframe id="ItemIf" width="100%" style="padding: 0;" scrolling="no" frameborder="0">
                </iframe>
            </td>
        </tr>
    </table>
    </form>

    <script type="text/javascript">

        $(function() {

            $.post("../Handler/Data.ashx?type=getXxcjMenu", {}, function(result) {
                if (result) {

                    $("#menuTable").append(result);

                    $("#menuTable").find("td[class^=left_icon]").eq(0).trigger("click");
                }

            }, 'text');

            var height = window.document.documentElement.clientHeight;
            $("#ItemIf").height(height);
            $("#ItemTd").height(height);

        });


        function showMsg(msg) {
            $.ligerDialog.closeWaitting();
            $.ligerDialog.alert(msg);
        }
        function showWarn(msg) {
            $.ligerDialog.closeWaitting();
            $.ligerDialog.warn(msg);
        }
        function showError(msg) {
            $.ligerDialog.closeWaitting();
            $.ligerDialog.error(msg);
        }
    </script>

</body>
</html>
