<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage_GLB3.aspx.cs"
    Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.MainPage.MainPage_GLB3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-dialog.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-layout.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-form.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-tab.css" rel="stylesheet"
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

    <script src="../../LigerUI/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>

    <script src="../../LigerUI/js/ligerui.expand.js" type="text/javascript"></script>

    <script src="../../LigerUI/js/common.js" type="text/javascript"></script>

    <script src="../../LigerUI/js/LG.js" type="text/javascript"></script>

    <link href="../../LigerUI/css/common.css" rel="stylesheet" type="text/css" />

    <script src="../Common/scripts/Common.js" type="text/javascript"></script>

    <script src="../../SparkClient/DateUtil.js" type="text/javascript"></script>

    <script src="../Common/scripts/MarqueeScroll.js" type="text/javascript"></script>

    <style type="text/css">
        .noticeList a:hover
        {
            color: #FF0000;
            text-decoration: underline;
        }
        .noticeBlock
        {
            width: 600px;
            height: 24px;
            border: 0;
            margin-top: 3px;
            margin-left: 30px;
            margin-bottom: 3px;
            padding: 0;
            position: relative;
            overflow: hidden;
        }
        .noticeBlock .noticeList
        {
            color: #333333;
            float: left;
            height: 100%;
            overflow: hidden;
            width: 600px;
            padding-left: 0px;
            margin: 0;
            vertical-align: middle;
        }
        .noticeIcon
        {
            background: url(Common/images/Web/sprite.png) 0 0 no-repeat;
        }
        .noticeBlock .noticeList li
        {
            display: block;
            height: 24px;
            line-height: 24px;
            list-style: none;
        }
        .title
        {
            color: #000066;
            text-decoration: none;
        }
        .more
        {
            margin-right: 0;
            color: Blue;
            font-size: 11px;
            margin-left: 5px;
            vertical-align: top;
            margin-top: 0;
            padding-top: 0;
            width: 15px;
        }
        .more2
        {
            margin-right: 0;
            color: Blue;
            font-size: 11px;
            margin-left: 5px;
            vertical-align: top;
            margin-top: 0;
            padding-top: 0;
            float: left;
        }
    </style>

    <script type="text/javascript">

        var uid = '111';
        $(function() {

            //var width = $(window).width();
            var height = $(window).height() - $("#tr1").height() - 5;
            var width = $(window).width();
            var contentHeight = height - 27;

            $("#layout1").ligerLayout({ leftWidth: width, height: height, allowLeftCollapse: false, allowRightCollapse: false });
            $("#navtab").ligerTab({ height: height - 27 });

            $("#tdtMap").attr("src", "TdtMap.aspx?height=" + (height - 10));

            $("#iframe1").attr("src", "http://218.90.162.101:8041/xzsp/yiban_xzsp_two.aspx?type=1&pinma=" + uid);
            $("#iframe2").attr("src", "http://218.90.162.101:8041/xzsp/yiban_xzsp_two.aspx?type=2&pinma=" + uid);

            $(".l-tab-links").find("li").live("mouseover", function() {

                var tabid = $(this).attr("tabid");
                $("#navtab").ligerTab("selectTabItem", tabid);

            });

            //            $(".more").mouseover(function() {

            //                $(this).css("color", "red");
            //            }).mouseout(function() {
            //                $(this).css("color", "blue");
            //            });


            //            $("#dbsx").click(function() {

            //                $("#iframe1").attr("src", "http://218.90.162.101:8041/xzsp/yiban_xzsp_two_Index.aspx?type=1&pinma=" + uid);
            //            });
            //            $("#ybsx").click(function() {

            //                $("#iframe2").attr("src", "http://218.90.162.101:8041/xzsp/yiban_xzsp_two_Index.aspx?type=2&pinma=" + uid);
            //            });


        });

       
    </script>

    <script type="text/javascript">
        //        function scrollDisplay(noticeCount) {


        //            if (noticeCount >= 1) {
        //                $(".noticeBlock").addClass("noticeIcon");
        //            }
        //            if (noticeCount >= 2) {
        //                scroll2 = new ScrollText("notices", null, null, true, 4000, false);
        //                scroll2.LineHeight = 23;
        //            }
        //        }
    </script>

</head>
<body style="background-color: rgb(238,238,238); margin: 0; padding: 0;">
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td valign="top" style="padding-left: 1px; padding-right: 1px; padding-bottom: 0;">
                <table width="100%" border="0" cellspacing="0">
                    <tr>
                        <td style="padding: 0;">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr id="tr1" style="height: 26px;">
                                    <td>
                                        <ul id="Gwtz" runat="server" class="noticeList">
                                            <%=Session["firstGwtz"] %>
                                        </ul>
                                    </td>
                                    <td>
                                        <ul id="Gzzs" runat="server" class="noticeList">
                                            <%=Session["firstGzzs"] %>
                                        </ul>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding: 0px;">
                                        <div id="layout1">
                                            <div position="left" title="行政效能监督">
                                                <div id="navtab" style="width: 100%;">
                                                    <div tabid="tab1" title="待办行政审批事项" selected="true" style="height: 100%;">
                                                        <iframe id="iframe1" frameborder="0" name="showmessage" style="border: 0;" height="100%"
                                                            frameborder="0"></iframe>
                                                    </div>
                                                    <div tabid="tab2" title="已办行政审批事项">
                                                        <div style="height: 100%;">
                                                            <iframe id="iframe2" frameborder="0" name="showmessage" style="border: 0;" height="100%"
                                                                frameborder="0"></iframe>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--  <div position="center" title="天地图" style="padding: 0;">
                                                <iframe id="tdtMap" width="100%" height="100%" frameborder="0" style="overflow: hidden;
                                                    border: 0; padding: 0; margin: 0;"></iframe>
                                            </div>--%>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        var activeDialog;
        $("div.link").live("mouseover", function() {
            $(this).addClass("linkover");

        }).live("mouseout", function() {
            $(this).removeClass("linkover");


        }).live('click', function(e) {
            var text = $("a", this).html();
            var url = $(this).attr("url");
            parent.f_addTab(null, text, url);
        });
        function f_edit(zshfId) {
            OpenWindow('../Zlct/Gzzs_Gzfk_Edit.aspx?zshfId=' + zshfId, "工作指示回复", 950, 450, true);
        }


        function refresh() {
            $("#btnRefresh").click();
        }



        function OpenWindow(url, title, width, height, isReload) {
            var dialogOptions = { width: width, height: height, title: title, url: url, showMax: true, showMin: true, buttons: [
                { text: '回复', onclick: function(item, dialog) {
                    dialog.frame.f_save(dialog, null);
                    refresh();
                }
                },
                { text: '关闭', onclick: function(item, dialog) {
                    if (isReload) {

                    }
                    dialog.close();
                }
                }
                ], isResize: true, timeParmName: 'a'
            };
            activeDialog = parent.parent.$.ligerDialog.open(dialogOptions);
        }
    </script>

    </form>
</body>
</html>
