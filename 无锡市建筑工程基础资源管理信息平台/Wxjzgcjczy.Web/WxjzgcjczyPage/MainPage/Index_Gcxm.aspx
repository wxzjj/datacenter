<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index_Gcxm.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.MainPage.Index_Gcxm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>市政工程专题</title>
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
            $("#ItemIf").height(height-5);
            $("#ItemTd").height(height);
        });
    </script>

</head>

  <body style="background-color: #4364A9; padding: 0;" onload="leftclick('01010000','left_icon_1_1');">
    <form id="Form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="padding: 0px 0px 0px 0px;">
        <tr>
            <td width="150" valign="top">
                 <div id="leftmenu" style="position: absolute; top: 0px;">
                    <table border="0" cellspacing="0" cellpadding="0" width="150">
                       <tr>
                            <input type="hidden" id="hd2" value="" />
                            <input type="hidden" id="hd2_num" value="" />
                            <td id="left_icon_1_1" class="left_icon_leave" onmousemove="leftover(this.id);" onmouseout="leftout(this.id);"
                                onclick="leftclick('01010000',this.id);">
                                <table >
                                    <tr>
                                        <td class="tdImg">
                                            <img src="../Common/images/FrameImages/01_05_0.png" />
                                        </td>
                                        <td class="tdFont">
                                            项目信息
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
                                onclick="leftclick('01030000',this.id);">
                                <table >
                                    <tr>
                                        <td  class="tdImg">
                                            <img src="../Common/images/FrameImages/01_04_0.png" />
                                        </td>
                                        <td  class="tdFont">
                                            施工图审查
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
                            <td id="left_icon_1_4" class="left_icon_leave" onmousemove="leftover(this.id);" onmouseout="leftout(this.id);"
                                onclick="leftclick('01040000',this.id);">
                                <table >
                                    <tr>
                                        <td  class="tdImg">
                                            <img src="../Common/images/FrameImages/01_01_0.png" />
                                        </td>
                                        <td  class="tdFont">
                                            招标投标
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
                            <td id="left_icon_1_5" class="left_icon_leave" onmousemove="leftover(this.id);" onmouseout="leftout(this.id);"
                                onclick="leftclick('01050000',this.id);">
                                <table >
                                    <tr>
                                        <td  class="tdImg">
                                            <img src="../Common/images/FrameImages/01_02_0.png" />
                                        </td>
                                        <td  class="tdFont">
                                            合同备案
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
                            <td id="left_icon_1_10" class="left_icon_leave" onmousemove="leftover(this.id);" onmouseout="leftout(this.id);"
                                onclick="leftclick('01100000',this.id);">
                                <table >
                                    <tr>
                                        <td  class="tdImg">
                                            <img src="../Common/images/FrameImages/03-01-0.png" />
                                        </td>
                                        <td  class="tdFont">
                                            安全监督（新）
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
                            <td id="left_icon_1_11" class="left_icon_leave" onmousemove="leftover(this.id);" onmouseout="leftout(this.id);"
                                onclick="leftclick('01110000',this.id);">
                                <table >
                                    <tr>
                                        <td  class="tdImg">
                                            <img src="../Common/images/FrameImages/01-06-0.png" />
                                        </td>
                                        <td  class="tdFont">
                                            质量报监（新）
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
                                onclick="leftclick('01060000',this.id);">
                                <table >
                                    <tr>
                                        <td  class="tdImg">
                                            <img src="../Common/images/FrameImages/03-01-0.png" />
                                        </td>
                                        <td  class="tdFont">
                                            安全监督
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
                                onclick="leftclick('01070000',this.id);">
                                <table >
                                    <tr>
                                        <td  class="tdImg">
                                            <img src="../Common/images/FrameImages/01-06-0.png" />
                                        </td>
                                        <td  class="tdFont">
                                            质量报监
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
                            <td id="left_icon_1_8" class="left_icon_leave" onmousemove="leftover(this.id);" onmouseout="leftout(this.id);"
                                onclick="leftclick('01080000',this.id);">
                                <table >
                                    <tr>
                                        <td  class="tdImg">
                                            <img src="../Common/images/FrameImages/01_03_0.png" />
                                        </td>
                                        <td  class="tdFnt">
                                            施工许可
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
                            <td id="left_icon_1_9" class="left_icon_leave" onmousemove="leftover(this.id);" onmouseout="leftout(this.id);"
                                onclick="leftclick('01090000',this.id);">
                                <table >
                                    <tr>
                                        <td  class="tdImg">
                                            <img src="../Common/images/FrameImages/06_01_0.png" />
                                        </td>
                                        <td  class="tdFont">
                                            竣工备案
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="menuSpan">
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td id="ItemTd" bgcolor="#EEEEEE" style="padding-left: 0; padding-top: 0; padding-bottom: 5px;
                padding-right: 0;">
                <iframe id="ItemIf" name="ItemIf" width="100%" style="padding:0;" scrolling="auto" frameborder="0" >
                </iframe>
               
            </td>
        </tr>
    </table>
    </form>
</body>
</html>




