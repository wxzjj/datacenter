<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage_Gzyj.aspx.cs"
    Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.MainPage.MainPage_Gzyj" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-dialog.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-layout.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-form.css" rel="stylesheet"
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

    <script src="../../LigerUI/js/ligerui.expand.js" type="text/javascript"></script>

    <script src="../../LigerUI/js/common.js" type="text/javascript"></script>

    <script src="../../LigerUI/js/LG.js" type="text/javascript"></script>

    <link href="../../LigerUI/css/common.css" rel="stylesheet" type="text/css" />

    <script src="../Common/scripts/Common.js" type="text/javascript"></script>

    <script src="../../SparkClient/DateUtil.js" type="text/javascript"></script>

    <script src="../Common/scripts/MarqueeScroll.js" type="text/javascript"></script>

</head>
<body style=" margin:5px;">
    <form id="form1" runat="server">
    <div >
        <asp:ScriptManager runat="server">
        </asp:ScriptManager>
        
        <table width="100%" cellpadding="0" cellspacing="0" border="0" style="margin-top: 5px;">
            <tr>
                <td style="background-color: rgb(101,128,183);">
                    <table width="100" cellpadding="0" cellspacing="0" border="0">
                        <tr style="height: 23px;">
                            <td style="width: 16px;">
                                <img src="../Common/icons/pencil.png" height="16" width="16" />
                            </td>
                            <td style="width: 80px;">
                                <span style="color: White;">跟踪预警</span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="border: solid 1px rgb(101,108,183); background-color: White; padding: 1px;">
                    <!--工作区一：预警提醒-->
                    <div id="yjts" style="overflow: hidden; width: 100%; height: 180px;">
                        <marquee onmousemove="this.stop()" id="Marq04" onmouseout="this.start()" scrollamount="2"
                            direction="up" loop="-1" height="180px">
                                                                        <asp:DataList ID="DataList_Gzyj" runat="server" Width="100%">
                                                                            <ItemTemplate>
                                                                            [<font style=" color:Red;"><%#Eval("lx") %></font>]<a target="_blank" href='<%#Eval("url").ToString()+Eval("row_id").ToString() %>'><font style=" color:Black;">&nbsp;<%#Eval("mc") %></font></a>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Height="24px" />
                                                                        </asp:DataList></marquee>
                    </div>
                </td>
            </tr>
        </table>
        <div style="display: none;">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Button ID="btnRefresh" runat="server" Text="刷新" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</body>
</html>
