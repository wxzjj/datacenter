<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tjfx_Toolbar.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Tjfx.Tjfx_Toolbar" %>

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
                    <div title="项目数量-开工">
                        <iframe id="Iframe1" name="content1" frameborder="0" width="100%" height="100%" scrolling="auto">
                        </iframe>
                    </div>
                    <div title="项目数量-竣工">
                        <iframe id="Iframe2" name="content2" frameborder="0" width="100%" height="100%" scrolling="auto">
                        </iframe>
                    </div>
                    <div title="项目总投资-开工">
                        <iframe id="Iframe3" name="content3" src='' frameborder="0" width="100%" height="100%"
                            scrolling="auto"></iframe>
                    </div>
                    <div title="项目总面积-开工">
                        <iframe id="Iframe4" name="content4" src='' frameborder="0" width="100%" height="100%"
                            scrolling="auto"></iframe>
                    </div>
                    <div title="项目总面积-竣工">
                        <iframe id="Iframe5" name="content5" src='' frameborder="0" width="100%" height="100%"
                            scrolling="auto"></iframe>
                    </div>
                    <div title="施工许可项目数量-发证">
                        <iframe id="Iframe6" name="content6" src='' frameborder="0" width="100%" height="100%"
                            scrolling="auto"></iframe>
                    </div>
                    <div title="施工许可面积-发证">
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
        $(function() {
            var height = parent.window.document.documentElement.clientHeight;

            $("#tab1").ligerTab({
                height: height,
                onBeforeSelectTabItem: function(tabid) {
                    if (tabid == 'tabitem1') {
                        $("#Iframe1").attr("src", 'Tjfx_Xmsl.aspx');
                    } else if (tabid == 'tabitem2') {
                        $("#Iframe2").attr("src", 'Tjfx_Xmsl_Jg.aspx');
                    } else if (tabid == 'tabitem3') {
                        $("#Iframe3").attr("src", 'Tjfx_XmZtz.aspx');
                    }
                    else if (tabid == 'tabitem4') {
                        $("#Iframe4").attr("src", 'Tjfx_XmZmj.aspx');
                    }
                    else if (tabid == 'tabitem5') {
                        $("#Iframe5").attr("src", 'Tjfx_XmZmj_Jg.aspx');
                    }

                    else if (tabid == 'tabitem6') {
                        $("#Iframe6").attr("src", 'Tjfx_SgxkXms.aspx');
                    }
                    else if (tabid == 'tabitem7') {
                        $("#Iframe7").attr("src", 'Tjfx_Sgxkmj.aspx');
                    }

                }, onAfterSelectTabItem: function(tabid) {

                }
            });
            //$("#tab1").height(height + 1);
            $("#Iframe1").attr("src", 'Tjfx_Xmsl.aspx');

        });
          
    </script>

    </form>
</body>
</html>
