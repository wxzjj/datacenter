<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index_Zlct.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.MainPage.Index_Zlct" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>政令畅通</title>
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

    <script src="../Common/scripts/Common.js" type="text/javascript"></script>
</head>
<body style="background-color: #4364A9; padding: 0;" onload="leftclick('07010000','left_icon_7_1','left_icon_7_1');">
    <form id="Form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="padding: 0;">
        <tr>
            <td width="150" valign="top">
                <div id="leftmenu" style="position: absolute; top: 0px;">
                    <table border="0" cellspacing="0" cellpadding="0" width="150">
                        <tr>
                            <input type="hidden" id="hd2" value="" />
                            <input type="hidden" id="hd2_num" value="" />
                            <td id="left_icon_7_1" class="left_icon_leave" onmousemove="leftover(this.id);" onmouseout="leftout(this.id);"
                                onclick="leftclick('07010000',this.id,this.id,qyid);">
                                <table >
                                    <tr>
                                         <td class="tdImg" >
                                            <img  src="../Common/images/FrameImages/07_01_0.png" />
                                        </td>
                                        <td  class="tdFont" >
                                            工作指示
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="menuSpan">
                            </td>
                        </tr>
                        <tr>
                            <td id="left_icon_7_2" class="left_icon_leave" onmousemove="leftover(this.id);" onmouseout="leftout(this.id);"
                                onclick="leftclick('07020000',this.id,this.id,qyid);">
                                <table>
                                    <tr>
                                         <td  class="tdImg" >
                                            <img  src="../Common/images/FrameImages/07_02_0.png" />
                                        </td>
                                        <td  class="tdFont" >
                                            公文通知
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="menuSpan">
                            </td>
                        </tr>
                        <tr>
                            <td id="left_icon_7_3" class="left_icon_leave" onmousemove="leftover(this.id);" onmouseout="leftout(this.id);"
                                onclick="leftclick('07030000',this.id,this.id,qyid);">
                                <table >
                                    <tr>
                                         <td class="tdImg" >
                                            <img  src="../Common/images/FrameImages/07_03_0.png" />
                                        </td>
                                        <td  class="tdFont" >
                                            短信简报
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="menuSpan">
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td id="ItemTd" bgcolor="#EEEEEE" style="padding: 1px 1px 0 3px">
                <iframe id="ItemIf" width="100%"  height="100%" style="padding: 0; margin:0;" scrolling="no"
                     frameborder="0">
                </iframe>
            </td>
        </tr>
    </table>
    </form>

    <script type="text/javascript">
        var flag = 0;
        var qyid;
       
//        function setContentHeight(height) {
//            h = getContentHeight();
//            $("#ItemIf").height(h - 2);
//            $("#ItemTd").height(h);
//        }

        //        function showMsg(msg) {
        //            $.ligerDialog.closeWaitting();
        //            $.ligerDialog.alert(msg);
        //        }

        //        function showWarn(msg) {
        //            $.ligerDialog.closeWaitting();
        //            $.ligerDialog.warn(msg);
        //        }
        //        function showError(msg) {
        //            $.ligerDialog.closeWaitting();
        //            $.ligerDialog.error(msg);
        //        }

        //        function OpenWindow(url, title, width, height, isReload) {
        //            var dialogOptions = { width: width, height: height, title: title, url: url, showMax: false, showMin: false, buttons: [
        //            { text: '关闭', onclick: function(item, dialog) {
        //                if (isReload) {
        //                    f_reload();
        //                }
        //                dialog.close();
        //            }
        //            }
        //            ], isResize: true, timeParmName: 'a'
        //            };
        //            activeDialog = $.ligerDialog.open(dialogOptions);
        //        }

        //        function openSrc(src) {
        //            $("#ItemIf").attr("src", src);
        //        }

        $(function() {

            //            $(".imgClass").mouseover(function() {
            //                $(this).attr("src", "../Common/images/FrameImages/01_1.png");
            //                flag = 1;

            //            });
            //            $(".imgClass").mouseout(function() {
            //                if (flag == 0)
            //                    $(this).attr("src", "../Common/images/FrameImages/01_0.png");

            //                flag = 0;

            //            });
            //            $(".imgClass").click(function() {
            //                $(this).attr("src", "../Common/images/FrameImages/01_1.png");
            //                flag = 1;

            //            });
            var h = getContentHeight();
            $("#ItemIf").height(h);
            $("#ItemTd").height(h);
        });
    </script>

</body>
</html>
