<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.MainPage.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>无锡市建筑工程基础信息资源管理信息平台</title>
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

    <style type="text/css">
        *
        {
            margin: 0;
            padding: 0;
        }
        .top_line
        {
            background-image: url(../Common/images/FrameImages/top_line.png);
            background-repeat: no-repeat;
        }
        .appName
        {
            background-image: url(../Common/images/FrameImages/top_title.jpg);
            background-repeat: no-repeat;
            cursor: pointer;
            width: 550px;
        }
    </style>

    <script type="text/javascript">
        $(function() {
            $("#RightMove").hover(function() {

                $(this).css("background-image", 'url(../Common/images/FrameImages/top_left2_over.png)');
            }, function() {

                $(this).css("background-image", 'url(../Common/images/FrameImages/top_left2.jpg)');
            });
            $("#LeftMove").hover(function() {

                $(this).css("background-image", 'url(../Common/images/FrameImages/top_right2_over.png)');
            }, function() {

                $(this).css("background-image", 'url(../Common/images/FrameImages/top_right2.jpg)');
            });
        });
    </script>

</head>
<body style="background-color: #4364A9; padding: 0; margin: 0;" onload="Loading('00000000','','');">
    <form id="Form1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0" style="border: 0;
        padding: 0; margin: 0;">
        <tr style="padding: 0; margin: 0; border: 0; height: 100px;">
            <td id="td_systemTitle" class="appName">
            </td>
            <td id="RightMove" style="width: 23px; text-align: center; padding: 0px; vertical-align: middle;
                background-image: url(../Common/images/FrameImages/top_left2.jpg); cursor: pointer;">
            </td>
            <td width="1" class="top_line">
            </td>
            <td style="padding: 0; overflow: hidden;">
                <div id="menuSpace" style="border: 0; height: 100%; overflow: hidden;">
                    <div id="divSpace" style="width: 7000px; height: 100%; border: 0;">
                        <table id="menu" border="0" cellspacing="0" cellpadding="0" style="height: 100%;">
                            <tr id="menu_tr">
                                <td width="0">
                                    <input type="hidden" id="hd_num" value="" />
                                    <input type="hidden" id="hd" value="" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
            <td width="1" class="top_line">
            </td>
            <td id="LeftMove" style="width: 23px; text-align: center; padding: 0px; vertical-align: middle;
                cursor: pointer; background: url(../Common/images/FrameImages/top_right2.jpg) no-repeat;">
            </td>
        </tr>
    </table>
    <div style="height: 100%; margin-top: 0; margin-bottom: 0; padding: 0; margin-left: 4px;
        margin-right: 4px;">
        <iframe id="MainIf" width="100%" height="100%" scrolling="auto" frameborder="0" marginheight="0"
            marginwidth="0" style="margin: 0; padding: 0; border: 0;"></iframe>
    </div>
    <div id="bottom" style="height: 25px; line-height: 25px; color: Black; font-size: 13px;
        margin: 0; padding: 0px 65px 0px 10px; margin: 0; margin-top: 4px; background-color: rgb(220,220,220);
        text-align: right; font-family: 宋体;">
        登录时间：
        <asp:Label ID="lblLoginTime" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
        当前用户：
        <asp:Label ID="lblUserName" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; <a id="loginOut" href="javascript:void(0)" style="color: Blue;
            text-decoration: none; font-family: 宋体;">注销</a> &nbsp; <a href="../Login.htm" style="color: Blue;
                text-decoration: none; font-family: 宋体;">重新登录</a>
    </div>
    </form>
</body>

<script type="text/javascript">
    var userPanelHeight = 25;
    var intVal;
    var moveSpeed = 30, moveLength = 3;

    function moveLeft() {

        var menuSpaceWidth = $("#menuSpace").width();
        var divSpaceWidth = $("#divSpace").width();
        var divSpaceMarginLeft = parseInt($("#divSpace").css("margin-left").replace("px", ""));
        if (divSpaceWidth > menuSpaceWidth && Math.abs(divSpaceMarginLeft) < divSpaceWidth - menuSpaceWidth) {
            divSpaceMarginLeft -= moveLength;
            if (Math.abs(divSpaceMarginLeft) >= divSpaceWidth - menuSpaceWidth) {
                divSpaceMarginLeft = -(divSpaceWidth - menuSpaceWidth);
                window.clearInterval(intVal);
            }
            $("#divSpace").css("margin-left", divSpaceMarginLeft + "px");
        }
    }

    function moveRight() {

        var menuSpaceWidth = $("#menuSpace").width();
        var divSpaceWidth = $("#divSpace").width();
        var divSpaceMarginLeft = parseInt($("#divSpace").css("margin-left").replace("px", ""));
        if (divSpaceMarginLeft < 0) {
            divSpaceMarginLeft += moveLength;
            if (divSpaceMarginLeft >= 0) {
                divSpaceMarginLeft = 0;
                window.clearInterval(intVal);
            }
            $("#divSpace").css("margin-left", divSpaceMarginLeft + "px");

        }
    }


    function OpenWindow(url, title, width, height, isReload) {
        var dialogOptions = { width: width, height: height, title: title, url: url, showMax: false, showMin: false, buttons: [
            { text: '关闭', onclick: function(item, dialog) {
                if (isReload) {
                    f_reload();
                }
                dialog.close();
            }
            }
            ], isResize: true, timeParmName: 'a'
        };
        activeDialog = $.ligerDialog.open(dialogOptions);
    }
    function showWarn(msg) {
        $.ligerDialog.warn(msg);
    }

    $(function() {

        var width = window.document.documentElement.clientWidth;
        var height = window.document.documentElement.clientHeight - 87 - userPanelHeight;

        $.post("../Handler/Data.ashx?type=getIndexTopMenu", {}, function(result) {
            if (result) {

                $("#menu_tr").append(result);

                $("#menuSpace").width(width - $("#td_systemTitle").width() - 46);

                $("#menuSpace").css("background", "url('../Common/images/FrameImages/top_space.jpg') x-repeat");
                
                $("#divSpace").width($("#menu").width());

                $("#menuSpace").offset().top = "0px";

                $("#LeftMove").mouseover(function() {
                    intVal = window.setInterval(moveLeft, moveSpeed);
                }).mouseout(function() {

                    window.clearInterval(intVal);
                });

                $("#RightMove").mouseover(function() {
                    intVal = window.setInterval(moveRight, moveSpeed);
                }).mouseout(function() {

                    window.clearInterval(intVal);
                });

                $("#MainIf").height(height);

                $("#bottom").height(userPanelHeight);

            }

        }, 'text');


        $("#loginOut").click(function() {

            $.ajax({
                type: 'post', cache: false, dataType: 'json',
                url: '../Handler/Login.ashx',
                data: [
                        { name: 'Action', value: 'Exist' }
                        ],
                success: function(result) {

                    if (result) {
                        window.location = "../Login.htm";
                    }
                },
                error: function() {
                    //$.ligerDialog.error('发送系统错误,请与系统管理员联系!');
                    window.location = "../Login.htm";
                }
            });
        });

        $("#td_systemTitle").click(function() {
//            $("#MainIf").attr("src", "MainPage_GLB3.aspx");
        });
    });


</script>

</html>
