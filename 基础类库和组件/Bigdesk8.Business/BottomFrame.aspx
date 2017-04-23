<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BottomFrame.aspx.cs" Inherits="Bigdesk8.Business.BottomFrame" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>底部框架</title>
    <meta http-equiv="refresh" content='300;URL=BottomFrame.aspx' />
    <link href="Styles/frame.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellspacing="0" cellpadding="5" width="100%">
        <tr>
            <td height="30" background="Images/FrameImages/BottomFrame_Bk.jpg" width="50%">
                &nbsp;&nbsp;<font color="#ffffff">技术支持：</font><a href="http://www.sparkcn.com/" target="_blank"><font
                    color="#ffffff">南京群耀软件系统有限公司</font></a>
            </td>
            <td height="30" background="Images/FrameImages/BottomFrame_Bk.jpg" width="50%">
                <div align="right">
                    <font color="#ffffff">欢迎您：</font><span id="lbl_zsmc" runat="server" style="color: White">系统管理员</span>&nbsp;
                    <a id="a_reloginPageUrl" runat="server" href="/" target="_top"><font color="#ffffff">
                        [ 重新登录 ]</font></a>&nbsp;<a id="a_modifyPasswordPageUrl" runat="server" href="/"
                            target="_top"><font color="#ffffff">[ 修改密码 ]</font></a><a href="####" onclick="javascript:top.opener=null;top.open('','_self','');top.close();"><font
                                color="#ffffff">[ 退出管理 ]</font></a> &nbsp;&nbsp;</div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
