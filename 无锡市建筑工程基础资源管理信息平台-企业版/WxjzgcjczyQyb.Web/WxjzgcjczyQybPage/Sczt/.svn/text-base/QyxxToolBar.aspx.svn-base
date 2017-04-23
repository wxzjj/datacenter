<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QyxxToolBar.aspx.cs" Inherits="WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Sczt.QyxxToolBar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查看企业信息</title>
    <link href="../../App_Themes/WxjzgcjczyQyb_B_Theme/base.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/WxjzgcjczyQyb_B_Theme/main.css" rel="stylesheet" type="text/css" />

    <script src="../Common/scripts/jquery-1.7.1.min.js" type="text/javascript"></script>

    <script src="../Common/scripts/common.js" type="text/javascript"></script>

    <link href="../../SparkClient/jquery.ui-1.8.2.css" rel="stylesheet" type="text/css" />

    <script src="../../SparkClient/jquery.ui-1.8.2.min.js" type="text/javascript"></script>

    <script src="../../SparkClient/jquery.validate.min.js" type="text/javascript"></script>

    <script src="../../SparkClient/script.js" type="text/javascript"></script>

    <script src="../../SparkClient/Calendar.js" type="text/javascript"></script>

    <script src="../../SparkClient/control.js" type="text/javascript"></script>

    <script src="../Common/jquery-easyui-1.3.3/jquery.easyui.min.js" type="text/javascript"></script>

    <link href="../Common/jquery-easyui-1.3.3/themes/metro/easyui.css" rel="stylesheet"
        type="text/css" />
    <link href="../Common/jquery-easyui-1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        $(document).ready(function() {
            var _addHeight = $(window).height() - $("body").outerHeight(true);
            var height = $(window).height();
            var width = $(window).width();
            tabResize(width, height);

            $(".childtab").load(function() {
                $("." + this.id).css("display", "none");
            });
        });

        function tabResize(width, height) {

            $("#tabs").tabs({
                width: width - 10,
                height: height,
                tabPosition: "top",
                onSelect: function(title) {
                    var url = window.location.href;
                    var len = url.length;
                    var qyid = url.substring(url.indexOf('qyid=') + 5, len);

                    switch (title) {
                        case "基本信息":
                            $('#iframe1').attr("src", "Qyxx_View.aspx?qyid=" + qyid);
                            break;
                        case "资质信息":
                            $('#iframe2').attr("src", "Zzxx.aspx?qyid=" + qyid);
                            break;
                        case "经营证照":
                            $('#iframe3').attr("src", "Jyzz.aspx?qyid=" + qyid);
                            break;
                        case "执业人员":
                            $('#iframe4').attr("src", "Zyry.aspx?qyid=" + qyid);
                            break;
                        case "承揽工程":
                            $('#iframe5').attr("src", "Clgc.aspx?qyid=" + qyid);
                            break;

                    }

                }
            });
        }

        function selectTab(tabIndex) {
            $("#tabs").tabs("select", tabIndex)
        }
 
     
    </script>

</head>
<body style="margin: 0px; padding: 0; overflow: hidden;">
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="1" width="100%" border="0">
        <asp:Panel ID="panel_header" runat="server" Width="100%">
            <tr>
                <td style="height: 145px; width: 100%;">
                    <iframe src="../Public/TitleFrame.aspx" height="145px" name="topFrame" framespacing="0"
                        frameborder="0" scrolling="no" id="topFrame" width="100%"></iframe>
                </td>
            </tr>
            <tr>
                <td class="workarea-title">
                    <div class="navbar">
                        <div class="navbar-icon">
                            <img alt="" src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/sign_in.png"
                                height="16" width="16" /></div>
                        <div class="navbar-inner">
                            <b><span class="text">您的位置 ：</span><span class="val">企业信息查看</span></b>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
            </tr>
        </asp:Panel>
    </table>
    <div style="width: 100%; margin: 0 2px 0 5px;">
        <div id="tabs" style="margin: 0; padding: 0;" class="easyui-tabs">
            <div title="基本信息" style="width: 100%; padding: 5px 2px 3px 3px;">
                <div class="iframe1">
                    <img src="../Common/images/icons/loading.gif" align="absmiddle" /><span style="color: #F97C10;">页面载入中...</span>
                </div>
                <iframe id="iframe1" class="childtab" scrolling="auto" frameborder="0" style="width: 100%;
                    padding: 0; margin: 0; background-color: #FFF; height: 99%;"></iframe>
            </div>
            <div title="资质信息" style="width: 100%; padding: 5px 2px 3px 3px;">
                <div class="iframe2">
                    <img src="../Common/images/icons/loading.gif" align="absmiddle" /><span style="color: #F97C10;">页面载入中...</span>
                </div>
                <iframe id="iframe2" class="childtab" scrolling="auto" frameborder="0" style="width: 100%;
                    padding: 0; margin: 0; background-color: #FFF; height: 99%;"></iframe>
            </div>
            <div title="经营证照" style="width: 100%; padding: 5px 2px 3px 3px;">
                <div class="iframe3">
                    <img src="../Common/images/icons/loading.gif" align="absmiddle" /><span style="color: #F97C10;">页面载入中...</span>
                </div>
                <iframe id="iframe3" class="childtab" scrolling="auto" frameborder="0" style="width: 100%;
                    padding: 0; margin: 0; background-color: #FFF; height: 99%;"></iframe>
            </div>
            <div title="执业人员" style="width: 100%; padding: 5px 2px 3px 3px;">
                <div class="iframe4">
                    <img src="../Common/images/icons/loading.gif" align="absmiddle" /><span style="color: #F97C10;">页面载入中...</span>
                </div>
                <iframe id="iframe4" class="childtab" scrolling="auto" frameborder="0" style="width: 100%;
                    padding: 0; margin: 0; background-color: #FFF; height: 75%;"></iframe>
            </div>
            <div title="承揽工程" style="width: 100%; padding: 5px 2px 3px 3px;">
                <div class="iframe5">
                    <img src="../Common/images/icons/loading.gif" align="absmiddle" /><span style="color: #F97C10;">页面载入中...</span>
                </div>
                <iframe id="iframe5" class="childtab" scrolling="auto" frameborder="0" style="width: 100%;
                    padding: 0; margin: 0; background-color: #FFF; height: 75%;"></iframe>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
