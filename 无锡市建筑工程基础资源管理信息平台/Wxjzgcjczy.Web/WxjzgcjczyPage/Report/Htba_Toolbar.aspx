<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Htba_Toolbar.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Report.Htba_Toolbar" %>

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
<body style="padding: 0px; height: 100%;">
    <form id="form1" runat="server">
    <table id="table1" width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td style="padding: 2px 4x 0px 2px; overflow: hidden;">
                <div id="tab1" style="height: 100%; border: 1px solid #A3C0E8; margin: 0; padding: 0;
                    overflow: hidden;">
                    <div title="合同备案报表">
                        <iframe id="Iframe1" name="content1" frameborder="0" width="100%" height="100%" scrolling="auto">
                        </iframe>
                    </div>
                    <div title="合同备案项目数-合同签订">
                        <iframe id="Iframe2" name="content2" frameborder="0" width="100%" height="100%" scrolling="auto">
                        </iframe>
                    </div>
                    <div title="合同备案合同金额-合同签订">
                        <iframe id="Iframe3" name="content3" src='' frameborder="0" width="100%" height="100%"
                            scrolling="auto"></iframe>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <div style="display: none">
    </div>

    <script type="text/javascript">
        $(function() {
            var height = parent.window.document.documentElement.clientHeight;

            $("#tab1").ligerTab({
                height: height,
                onBeforeSelectTabItem: function(tabid) {
                    if (tabid == 'tabitem1') {
                        $("#Iframe1").attr("src", 'HtbaReport.aspx');
                    } else if (tabid == 'tabitem2') {
                        $("#Iframe2").attr("src", 'Tjfx_HtbaXms.aspx');
                    } else if (tabid == 'tabitem3') {
                        $("#Iframe3").attr("src", 'Tjfx_HtbaHtje.aspx');
                    }


                }, onAfterSelectTabItem: function(tabid) {

                }
            });
            //$("#tab1").height(height + 1);
            $("#Iframe1").attr("src", 'HtbaReport.aspx');

        });
          
    </script>

    </form>
</body>
</html>
