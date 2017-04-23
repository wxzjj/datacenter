<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tjtb.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Tjfx.Tjtb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>统计分析</title>
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

    <script type="text/javascript">
        $(document).ready(function() {
            $("#box img").mouseover(function() {
                var width = $(this).width();    // 图片实际宽度  
                var height = $(this).height();  // 图片实际高度
                $(this).css("border", "2px solid blue");
                $(this).css("width", width - 4);
                $(this).css("height", height - 4);
            });
            $("#box img").mouseout(function() {
                var width = $(this).width();    // 图片实际宽度  
                var height = $(this).height();  // 图片实际高度
                $(this).css("border", "none");
                $(this).css("width", width + 4);
                $(this).css("height", height + 4);
            });
        });
        function f_openTb(title, url) {
            OpenWindow(url, title, 650, 600, true);

            //            window.showModalDialog(url, window, "dialogWidth=750px;dialogHeight=400px");
        }
        function f_openTb1(title, url) {
            OpenWindow(url, title, 700, 450, true);

            //            window.showModalDialog(url, window, "dialogWidth=750px;dialogHeight=400px");
        }
        function OpenWindow(url, title, width, height, isReload) {
            var dialogOptions = { width: width, height: height, title: title, url: url, showMax: false, showMin: true, buttons: [

                { text: '关闭', onclick: function(item, dialog) {
                    dialog.close();
                }
                }
            ], isResize: true, timeParmName: 'a'
            };
            activeDialog = parent.parent.parent.$.ligerDialog.open(dialogOptions);
        }
    </script>

</head>
<body style="background-color: #4364A9; padding: 0; height: 100%;">
    <form id="Form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="height: 100%;
        padding: 0px 0px 0px 0px;">
        <tr>
            <td id="ItemTd" bgcolor="#EEEEEE" style="padding: 2px 0 0 3px; height: 100%">
                <table id="box" border="0" cellpadding="0" cellspacing="2" width="70%">
                    <tr>
                        <td width="50%" style="height: 250px; text-align: center">
                            <a href="javascript:f_openTb('施工单位类型统计','../Tjfx/Sgdwzz.aspx');">
                                <img width="350px" height="200px" src="../Common/images/Tjfx/Sgdwlx.jpg" /></a>
                        </td>
                        <td width="50%" style="height: 250px; text-align: center">
                            <a href="javascript:f_openTb('执业人员类型统计','../Tjfx/Zyrylx.aspx');">
                                <img width="350px" height="200px" src="../Common/images/Tjfx/Zyrylx.jpg" /></a>
                        </td>
                    </tr>
                    <tr>
                        <td height="16" align="center">
                            <asp:Label ID="dt_title1" runat="server" Font-Size="14.25pt" Font-Bold="true" ForeColor="26, 59, 105"
                                Text="施工企业类型统计"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Label ID="DBText1" runat="server" Font-Size="14.25pt" Font-Bold="true" ForeColor="26, 59, 105"
                                Text="执业人员类型统计"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 15px;" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" style="height: 250px; text-align: center">
                            <a href="javascript:f_openTb1('近五年项目统计','../Tjfx/Xmnftj.aspx');">
                                <img width="350px" height="200px" src="../Common/images/Tjfx/Xmnftj.jpg" /></a>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td height="16" align="center">
                            <asp:Label ID="Label1" runat="server" Font-Size="14.25pt" Font-Bold="true" ForeColor="26, 59, 105"
                                Text="项目年份统计"></asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                    <td style=" height:200px;"></td>
                    </tr>
                </table>
                <%-- <iframe id="ItemIf" height="100%" width="100%" style="padding: 0; margin: 0;" scrolling="no"
                    frameborder="0"></iframe>--%>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
