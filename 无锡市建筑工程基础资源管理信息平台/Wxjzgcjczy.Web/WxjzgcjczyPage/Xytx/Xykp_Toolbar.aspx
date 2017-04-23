<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Xykp_Toolbar.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Xytx.Xykp_Toolbar" %>

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
<body style="padding: 0px; height:100%;">
    <form id="form1" runat="server">
    <table id="table1" width="100%" border="0" cellspacing="0" cellpadding="0" height="100%">
        <tr>
            <td style="padding:2px 4x 0px 2px;">
                <div id="tab1" style=" height: 100%; border: 1px solid #A3C0E8;  margin:0; padding:0;">
                    <div title="施工单位">
                        <iframe id="Iframe1" name="content1"  
                            frameborder="0" width="100%" height="100%" scrolling="auto"></iframe>
                    </div>
                   <div title="监理单位">
                        <iframe id="Iframe2" name="content2" src='' frameborder="0" width="100%" height="100%"
                            scrolling="auto"></iframe>
                    </div>
                      <div title="勘察单位">
                        <iframe id="Iframe3" name="content3" src='' frameborder="0" width="100%" height="100%"
                            scrolling="auto"></iframe>
                    </div>
                      <div title="设计单位">
                        <iframe id="Iframe4" name="content4" src='' frameborder="0" width="100%" height="100%"
                            scrolling="auto"></iframe>
                    </div>
                      <div title="招标代理机构">
                        <iframe id="Iframe5" name="content5" src='' frameborder="0" width="100%" height="100%"
                            scrolling="auto"></iframe>
                    </div>
                       <div title="造价咨询机构">
                        <iframe id="Iframe6" name="content6" src='' frameborder="0" width="100%" height="100%"
                            scrolling="auto"></iframe>
                    </div>
                     <div title="检测机构">
                        <iframe id="Iframe7" name="content7" src='' frameborder="0" width="100%" height="100%"
                            scrolling="auto"></iframe>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <div style="display: none">
    </div>

    <script type="text/javascript">

        function setHeight(iframeIndex) {

            var h = window.document.documentElement.clientHeight-30 - 5;
            $("#Iframe" + iframeIndex).height(h - 33-100);
            $("#Iframe" + iframeIndex).parent("div").height(h - 30 - 100);
            $("#tab1").height(h);
            $("#table1").height(h);
        }
//        function setHeight(iframeIndex) {

//            var h = getTabHeight() - 3;
//            $("#Iframe" + iframeIndex).height(h - 33);
//            $("#Iframe" + iframeIndex).parent("div").height(h - 30);
//            $("#tab1").height(h);
//        }

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
            var height = parent.window.document.documentElement.clientHeight - 6;

            $("#tab1").ligerTab({
                height: height,
                onBeforeSelectTabItem: function(tabid) {
                    if (tabid == 'tabitem1') {
                        $("#Iframe1").attr("src", 'Xykp_List.aspx?qylx=sgdw');
                    } else if (tabid == 'tabitem2') {
                    $("#Iframe2").attr("src", 'Xykp_List.aspx?qylx=jldw');
                    } else if (tabid == 'tabitem3') {
                    $("#Iframe3").attr("src", 'Xykp_List.aspx?qylx=kcdw');
                    } else if (tabid == 'tabitem4') {
                    $("#Iframe4").attr("src", 'Xykp_List.aspx?qylx=sjdw');
                    } else if (tabid == 'tabitem5') {
                    $("#Iframe5").attr("src", 'Xykp_List.aspx?qylx=zbdljg');
                    } else if (tabid == 'tabitem6') {
                    $("#Iframe6").attr("src", 'Xykp_List.aspx?qylx=zjzxjg');
                    } else if (tabid == 'tabitem7') {
                    $("#Iframe7").attr("src", 'Xykp_List.aspx?qylx=jcjg');
                    }

                }, onAfterSelectTabItem: function(tabid) {

                }
            });
            //$("#tab1").height(height + 1);
            $("#Iframe1").attr("src", 'Xykp_List.aspx?qylx=sgdw');

        });
          
    </script>

    </form>
</body>
</html>
