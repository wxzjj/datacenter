<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RightFrame.aspx.cs" Inherits="WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.EntranceJsdw.RightFrame" %>

<html>
<head id="Head1" runat="server">
    <title>右部框架</title>
    <link href="../Common/jquery-easyui-1.3.3/themes/metro/easyui.css" rel="stylesheet"
        type="text/css" />
    <link href="../Common/jquery-easyui-1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="../Common/scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="../Common/jquery-easyui-1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
</head>
<body class="rightframe" style="overflow-y: hidden;">

    <script type="text/javascript">
        $(document).ready(function() {
           // win_resize();

            var slide = "<%=slide %>";
            if (slide == "show") {
                $.messager.show({
                    title: '提示',
                    msg: '欢迎登陆《无锡市住房和城乡建设局公共信息服务平台》！',
                    timeout: 5000,
                    showType: 'slide',
                    iconCls: 'icon-comment'
                });
            }

        });
    </script>

    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td class="workarea-title">
                <div class="navbar">
                    <div class="navbar-icon">
                        <img alt="" src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/nav.png" height="16" width="16" /></div>
                    <div class="navbar-inner">
                         <b><span class="text">
                            您的位置 ：</span><span class="val">
                                <asp:Label runat="server" ID="lb_nav"></asp:Label>
                            </span></b> 
                    </div>
                    <div class="navbar-fresh-icon">
                        <img alt="" src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/refresh.png" height="16" width="16" />
                    </div>
                    <div class="navbar-fresh-text">
                        <span onclick="javascript:document.getElementById('iframe_item').src='<%=iframeUrl %>';">
                            刷新</span>
                    </div>
                    <div class="navbar-back-icon">
                        <img alt="" src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/navback.png" height="16" width="16" />
                    </div>
                    <div class="navbar-back-text">
                        <span onclick="javascript:top.window.history.back();return false;">返回</span>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <div id="div_content">
        <div id="div_load" style="z-index:1000; position:absolute; margin:5px;">
            <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/loading.gif" align="absmiddle" /><span style="color:#ff6600;">页面载入中...</span></div>
        <iframe id="iframe_item" width="100%" height="100%" name="iframe_item" src="<%=iframeUrl %>" scrolling="auto"
            frameborder="0" style="padding-bottom:25px;"  allowTransparency="true"></iframe>
            <script type="text/javascript">
                //loading
                $("#iframe_item").load(function() {
                    $("#div_load").hide();
                });
            </script>
    </div>
</body>
</html>