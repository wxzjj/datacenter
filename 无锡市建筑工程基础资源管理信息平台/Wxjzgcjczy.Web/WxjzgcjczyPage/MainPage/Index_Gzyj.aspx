<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index_Gzyj.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.MainPage.Index_Gzyj" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>跟踪预警</title>
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

        $(function() {
            var height = window.document.documentElement.clientHeight;
            $("#ItemIf").height(height);
            $("#ItemTd").height(height);
        });
    </script>

</head>
<body style="background-color: #4364A9; padding: 0;" onload="leftclick('04010000','left_icon_1_4');">
    <form id="Form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="padding: 0px 0px 0px 0px;">
        <tr>
            <td width="150" valign="top">
                <div id="leftmenu" style="position: absolute; top: 0px;">
                    <table border="0" cellspacing="0" cellpadding="0" width="150">
                        <tr>
                            <input type="hidden" id="hd2" value="" />
                            <input type="hidden" id="hd2_num" value="" />
                            <td id="left_icon_1_4" class="left_icon_leave" onmousemove="leftover(this.id);" onmouseout="leftout(this.id);"
                                onclick="leftclick('04010000',this.id);">
                                <table>
                                    <tr>
                                        <td class="tdImg">
                                            <img src="../Common/images/FrameImages/04_04_0.png" />
                                        </td>
                                        <td class="tdFont">
                                            企业证书过期
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
                            <td id="left_icon_1_1" class="left_icon_leave" onmousemove="leftover(this.id);" onmouseout="leftout(this.id);"
                                onclick="leftclick('04020000',this.id);" style="padding; 0; margin: 0">
                                <table>
                                    <tr>
                                        <td class="tdImg">
                                            <img src="../Common/images/FrameImages/04_04_0.png" />
                                        </td>
                                        <td class="tdFont">
                                            人员证书过期
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
                            <td id="left_icon_1_2" class="left_icon_leave" onmousemove="leftover(this.id);" onmouseout="leftout(this.id);"
                                onclick="leftclick('04030000',this.id);">
                                <table>
                                    <tr>
                                        <td class="tdImg">
                                            <img src="../Common/images/FrameImages/04_04_0.png" />
                                        </td>
                                        <td class="tdFont">
                                            造价过亿项目
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
                            <td id="left_icon_1_3" class="left_icon_leave" onmousemove="leftover(this.id);" onmouseout="leftout(this.id);"
                                onclick="leftclick('04040000',this.id);">
                                <table>
                                    <tr>
                                        <td class="tdImg">
                                            <img src="../Common/images/FrameImages/04_04_0.png" />
                                        </td>
                                        <td class="tdFont">
                                            未办施工许可证
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
                            <td id="Td1" class="left_icon_leave" onmousemove="leftover(this.id);" onmouseout="leftout(this.id);"
                                onclick="leftclick('04060000',this.id);">
                                <table>
                                    <tr>
                                        <td class="tdImg">
                                            <img src="../Common/images/FrameImages/04_04_0.png" />
                                        </td>
                                        <td class="tdFont">
                                            未报质量监督
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
                            <td id="Td2" class="left_icon_leave" onmousemove="leftover(this.id);" onmouseout="leftout(this.id);"
                                onclick="leftclick('04070000',this.id);">
                                <table>
                                    <tr>
                                        <td class="tdImg">
                                            <img src="../Common/images/FrameImages/04_04_0.png" />
                                        </td>
                                        <td class="tdFont">
                                            未报安全监督
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
                            <td id="Td3" class="left_icon_leave" onmousemove="leftover(this.id);" onmouseout="leftout(this.id);"
                                onclick="leftclick('04080000',this.id);">
                                <table>
                                    <tr>
                                        <td class="tdImg">
                                            <img src="../Common/images/FrameImages/04_04_0.png" />
                                        </td>
                                        <td class="tdFont">
                                            竣工备案过程缺失
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="menuSpan">
                            </td>
                        </tr>
                        <%-- <tr>
                            <td id="Td1" class="left_icon_leave" onmousemove="leftover(this.id);" onmouseout="leftout(this.id);"
                                onclick="leftclick('04050000',this.id);">
                                <table >
                                    <tr>
                                        <td  class="tdImg">
                                            <img src="../Common/images/FrameImages/04_05_0.png" />
                                        </td>
                                        <td  class="tdFont">
                                            工程项目变更
                                        </td>
                                    </tr>
                                </table>
                            </td>
                      
                        <tr>
                            <td class="menuSpan">
                            </td>
                        </tr>  </tr>--%>
                    </table>
                </div>
            </td>
            <td id="ItemTd" bgcolor="#EEEEEE" style="padding-left: 3px; padding-top: 0; padding-bottom: 0;
                padding-right: 0;">
                <iframe id="ItemIf" width="100%" style="padding: 0;" scrolling="no" frameborder="0">
                </iframe>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
