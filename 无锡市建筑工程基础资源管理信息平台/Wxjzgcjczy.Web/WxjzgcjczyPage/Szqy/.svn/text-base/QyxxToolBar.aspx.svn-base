<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QyxxToolBar.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Szqy.QyxxToolBar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查看企业信息</title>
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

    <script src="../../SparkClient/DateUtil.js" type="text/javascript"></script>

    <script>
        var activeDialog;
        var Height;
        var menuHeight = 35;

        function loadData() {

            Height = window.document.documentElement.clientHeight - 17;
            $("#iframe1").height(Height - menuHeight);

            $('.td_menu td').click(function() {

                var linka = $(this).find("a");
                var thiscity = $(linka).attr("link");
                if (thiscity == "undefined" || thiscity == "" || thiscity == null) {
                    return;
                }

                $("#iframe1").attr("src", thiscity + "&height=" + (Height - menuHeight));
                $(this).siblings(".td_current").removeClass("td_current").addClass("td_other");
                $(this).removeClass("td_other").addClass("td_current");

            });

            $('.td_menu td').eq(0).trigger("click");
        }

        window.onload = loadData;

        function OpenWindow(url, title, width, height, isReload) {
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

        function OpenWindowView(url, title, width, height, isReload) {
            var dialogOptions = { width: width, height: height, title: title, url: url, showMax: true, buttons: [
                { text: '关闭', onclick: function(item, dialog) {
                    dialog.close();
                }
                }
                ], isResize: true, timeParmName: 'a'
            };
            activeDialog = $.ligerDialog.open(dialogOptions);
        }
     

        
    </script>

    <style>
        .td_menu
        {
            float: none;
            margin: auto;
        }
        .td_menu a
        {
            font-size: 10pt;
            font-family: 宋体;
            font-weight: bold;
            word-spacing: 2;
        }
        .td_current
        {
            text-align: center;
            vertical-align: middle;
            height: 30px;
            width: 108;
            background-color: rgb(101,175,227);
            border-top: solid 1px rgb(101,175,227);
            border-right: solid 1px rgb(101,175,227);
            border-left: solid 1px rgb(101,175,227);
            border-bottom: 0;
            cursor: pointer;
        }
        .td_other
        {
            text-align: center;
            vertical-align: middle;
            height: 30px;
            width: 108;
            background-color: rgb(238,238,238);
            border-top: solid 1px rgb(101,175,227);
            border-right: solid 1px rgb(101,175,227);
            border-left: solid 1px rgb(101,175,227);
            border-bottom: 0;
            cursor: pointer;
        }
        .td_space
        {
            width: 1px;
            height: 30px;
        }
        .td_naline
        {
            width: 100%;
            background-color: rgb(101,175,227);
            height: 2px;
        }
    </style>
</head>
<body style="padding: 3px; padding-bottom: 0; overflow-x: hidden">
    <form id="form1" runat="server">
    <div style="width: 90%; margin: auto; float: none;">
        <table width="100%" style="" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 100%; height: 100%;">
                    <table class="td_menu" style="width: 70%; height: 100%;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="td_current">
                                <a link='Qyxx_View.aspx?qyid=<%=qyID %>&befrom=<%=befrom %>'>基本信息</a>
                            </td>
                            <td class="td_space">
                            </td>
                            <td class="td_other">
                                <a link='Zzxx.aspx?qyid=<%=qyID %>&befrom=<%=befrom %>'>资质信息</a>
                            </td>
                            <td class="td_space">
                            </td>
                            <td class="td_other">
                                <a link='Jyzz.aspx?qyid=<%=qyID %>&befrom=<%=befrom %>'>经营证照</a>
                            </td>
                            <td class="td_space">
                            </td>
                            <%--  <td class="td_other">
                                <a link='Xysc.aspx?rowid=<%=rowID %>&befrom=toolbar'>信用手册</a>
                            </td>
                            <td class="td_space">
                            </td>--%>
                            <td class="td_other">
                                <a link='Zyry.aspx?qyid=<%=qyID %>&befrom=<%=befrom %>'>执业人员</a>
                            </td>
                            <td class="td_space">
                            </td>
                            <td class="td_other">
                                <a link='Clgc.aspx?qyid=<%=qyID %>&befrom=<%=befrom %>'>承揽工程</a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td_naline" style="width: 100%;">
                </td>
            </tr>
            <tr>
                <td style="width: 100%; padding: 2px 0 0 0;" align="center">
                    <iframe id="iframe1" name="iframe1" src='' frameborder="0" width="100%" height="100%"
                        style="padding: 0;" scrolling="auto"></iframe>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
