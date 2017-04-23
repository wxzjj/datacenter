<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Gwtz_Toolbar.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Zlct.Gwtz_Toolbar" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Common/css/base.css" rel="stylesheet" type="text/css" />
    <link href="../../SparkClient/DateTime_ligerui.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/MunSupervisionProject_Theme/Style.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-dialog.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-form.css" rel="stylesheet"
        type="text/css" />

   <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-tab.css" rel="stylesheet"
        type="text/css" />
    <link href="../Common/css/base.css" rel="stylesheet" type="text/css" />
    <script src="../../LigerUI/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/core/base.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../MyDatePicker/WdatePicker.js"></script>

    <script src="../../LigerUI/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>

    <script src="../../LigerUI/js/common.js" type="text/javascript"></script>

    <script src="../../LigerUI/js/LG.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>

    <script src="../Common/scripts/Common.js" type="text/javascript"></script>
</head>
<body style="padding: 0px;">
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" height="100%">
        <tr>
            <td style="padding: 2px;">
                <div id="tab1" style="width: 100%; height: auto; border: 1px solid #A3C0E8; background-color: #EEEEEE;">
                    <div title="发件箱">
                        <iframe id="Iframe1" name="content1" src='../../../MajordomoMVC/OnlineOffice/ListGwtz' onload="setHeight(1,this.document.body.scrollHeight)"
                            frameborder="0" width="100%" height="100%" scrolling="no"></iframe><!--http://10.1.1.175:8006/MajordomoMVC/OnlineOffice/ListGwtz-->
                    </div>
                    <div title="收件箱">
                        <iframe id="Iframe2" name="content2" src='' frameborder="0" width="100%" height="100%"
                            onload="setHeight(2,this.document.body.scrollHeight)" scrolling="no"></iframe>
                    </div>
                     <div title="短信">
                        <iframe id="Iframe3" name="content2" src='' frameborder="0" width="100%" height="100%"
                            onload="setHeight(3,this.document.body.scrollHeight)" scrolling="no"></iframe>
                    </div>
                     <div title="通讯录">
                        <iframe id="Iframe4" name="content2" src='' frameborder="0" width="100%" height="100%"
                            onload="setHeight(4,this.document.body.scrollHeight)" scrolling="no"></iframe>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <div style="display: none">
    </div>

    <script type="text/javascript">
        var Height = 605;
        var sumHeight = 0;
        var baseHeight = 30;
        function setHeight(iframeIndex, height) {

            sumHeight = height + baseHeight;
            if (sumHeight > Height) {
                $("#Iframe" + iframeIndex).height(height);
                $("#Iframe" + iframeIndex).parent("div").height(sumHeight);
                $("#tab1").height(sumHeight + 1);
            }
            else {
                $("#Iframe" + iframeIndex).height(height);
                $("#Iframe" + iframeIndex).parent("div").height(Height);
                $("#tab1").height(Height);
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


        $(function() {
            $("#tab1").ligerTab({ onBeforeSelectTabItem: function(tabid) {
                if (tabid == 'tabitem1') {
                    $("#Iframe1").attr("src", '../../../MajordomoMVC/OnlineOffice/ListGwtz');

                } else if (tabid == 'tabitem2') {
                $("#Iframe2").attr("src", '../../../MajordomoMVC/OnlineOffice/ListRecieveGwtz');

                } else if (tabid == 'tabitem3') {
                $("#Iframe3").attr("src", '../../../MajordomoMVC/OnlineOffice/ListShortMsg');

                } else if (tabid == 'tabitem4') {
                $("#Iframe4").attr("src", '../../../MajordomoMVC/OnlineOffice/ListGgfz');
                } 
            }, onAfterSelectTabItem: function(tabid) {

            }
            });

        });
          
    </script>

    </form>
</body>
</html>

