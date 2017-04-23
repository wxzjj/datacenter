<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TopFrame.aspx.cs" Inherits="WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.EntranceGlyh.TopFrame" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
    <title>顶部框架-管理版</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />

    <script src="../Common/scripts/jquery-1.7.1.min.js" type="text/javascript"></script>

    <script src="../Common/scripts/frame.js" type="text/javascript"></script>

    <link href="../../App_Themes/WxjzgcjczyQyb_B_Theme/frame.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="Form1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="top-glb-bk">
        <tr>
            <td class="top-glb">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="height: 67px;">
                        </td>
                    </tr>
                    <tr>
                        <td height="30" valign="top">
                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 150px; color: White; padding-left: 2px;">
                                        <input type="hidden" id="hd" value="" />
                                        <input type="hidden" id="hd_num" value="" />
                                    </td>
                                    <td>
                                        <div id="menuContent">
                                            <table cellpadding="0" cellspacing="0" style="color: White; font-size: 12.5px; font-weight: bold;
                                                word-spacing: 1em;">
                                                <tr>
                                                    <%--       <td id="10000000" class="link" onmousemove="tdmove(this.id)" onmouseout="tdout(this.id)">
                                                        <asp:Label ID="menu_1" onclick="Loading('10000000');" runat="server" CssClass="cursor"
                                                            Text="工程项目" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td style="width: 1px;">
                                                    </td>
                                                    <td id="20000000" class="link" onmousemove="tdmove(this.id)" onmouseout="tdout(this.id)">
                                                        <asp:Label ID="menu_2" onclick="Loading('20000000');" runat="server" CssClass="cursor"
                                                            Text="市场主体" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td style="width: 1px;">
                                                    </td>
                                                    <td id="30000000" class="link" onmousemove="tdmove(this.id)" onmouseout="tdout(this.id)">
                                                        <asp:Label ID="menu_4" onclick="Loading('30000000');" runat="server" CssClass="cursor"
                                                            Text="执业人员" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td style="width: 1px;">
                                                    </td>
                                                    <td id="40000000" class="link" onmousemove="tdmove(this.id)" onmouseout="tdout(this.id)">
                                                        <asp:Label ID="menu_7" onclick="Loading('40000000');" runat="server" CssClass="cursor"
                                                            Text="信用体系" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td style="width: 1px;">
                                                    </td>--%>
                                                    <%--  <td id="50000000" class="link" onmousemove="tdmove(this.id)" onmouseout="tdout(this.id)">
                                                        <asp:Label ID="Label1" onclick="Loading('50000000');" runat="server" CssClass="cursor"
                                                            Text="信息共享" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td style="width: 1px;">
                                    </td>--%>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                    <td style="text-align: right;">

                                        <script type="text/javascript">
                                            //一级菜单动态背景效果(new)
                                            $(document).ready(function() {
                                                Loading('50000000', "menu_1");
                                            });

                                            function tdmove(id) {
                                                var _id = "#" + id;
                                                $(_id).addClass("linkover");
                                            }
                                            function tdout(id) {
                                                if (document.getElementById("hd").value != id) {
                                                    var _id = "#" + id;
                                                    $(_id).removeClass("linkover");
                                                }
                                            }
                                            function menuClick(id) {
                                                var a = document.getElementById("hd").value;
                                                if (a != "") {
                                                    $("#" + a).removeClass("linkover");
                                                }
                                                document.getElementById("hd").value = id;
                                                $("#" + id).addClass("linkover");

                                                tdout(id);
                                            }
                                            /* 左边菜单显示 */
                                            function Loading(nodeID) {
                                                top.leftFrame.location = "LeftFrame.aspx?parentNodeID=" + nodeID;
                                                menuClick(nodeID);
                                                return false;
                                            }
                                        </script>

                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px;">
                        </td>
                    </tr>
                </table>
                <div style="position: fixed; top: 15px; right: 20px; height: 100px; float: right;">
                    <table align="right" style="width: 200px; text-align: center; color: white;" border="0"
                        cellspacing="0" cellpadding="0">
                        <tr style="height: 50px; vertical-align: bottom;">
                            <td style="width: 55px;">
                                <a style="color: White;" href="##">
                                    <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/frameimages/btn_home.png"
                                        height="45" width="45" border="0" /></a>
                            </td>
                            <td style="width: 10px;">
                            </td>
                            <td style="width: 55px;">
                                <a style="color: White;" href="<% = reloginPageUrl %>" target="_top">
                                    <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/frameimages/btn_logout.png"
                                        height="45" width="45" border="0" /></a>
                            </td>
                            <td style="width: 10px;">
                            </td>
                            <td style="width: 55px;">
                                <a style="color: White;" href="##" onclick="javascript:top.window.close();">
                                    <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/frameimages/btn_quit.png"
                                        height="45" width="45" border="0" /></a>
                            </td>
                        </tr>
                        <tr style="height: 15px;">
                            <td style="width: 55px;">
                                <a style="color: White;" href="##">返回首页</a>
                            </td>
                            <td style="width: 10px;">
                            </td>
                            <td style="width: 55px;">
                                <a style="color: White;" href="<% = reloginPageUrl %>" target="_top">用户注销</a>
                            </td>
                            <td style="width: 10px;">
                            </td>
                            <td style="width: 55px;">
                                <a style="color: White;" href="##" onclick="javascript:top.window.close();">安全退出</a>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
