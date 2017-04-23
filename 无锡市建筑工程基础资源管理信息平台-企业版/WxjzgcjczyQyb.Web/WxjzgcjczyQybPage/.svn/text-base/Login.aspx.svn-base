<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Login" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
    <title>无锡市住房和城乡建设局公共信息服务平台</title>
    <script src="Common/jquery-easyui-1.3.3/jquery-1.8.0.min.js" type="text/javascript"></script>
    <style type="text/css">
        *{padding:0px; margin:0px; list-style:none; font-family:微软雅黑;}
        a:link{ text-decoration:none;} a:visited{text-decoration:none;}
        body{width:100%;margin:0px;padding:0px;background:url(Common/images/Login/bg03.png);width:100%;height:100%}
        .top{width:1024px;height:300px;margin:0 auto; background:url(Common/images/Login/bg01s.png) no-repeat; position:relative; background-position:center 0;}
        .bottom{width:1024px;height:390px;margin:0 auto; background:url(Common/images/Login/bg02.png) no-repeat; position:relative;background-position:center 0;}
        .bottom-copyright{margin:0 auto;position:relative;width:300px;padding-top:5px; text-align:center;color:White; font-size:14px;top:340px;}
        .login{width:800px;height:120px;margin:0 auto; background:url(Common/images/Login/login.png) no-repeat; position:relative;top:-450px;}
    </style>
</head>
<body style="overflow:hidden;">
<form id="form1" runat="server">
<div class="top">
</div>
<div class="bottom">
    <div class="bottom-copyright">
        <span>技术支持：南京群耀软件系统有限公司</span>
        </div>
</div> 
<div id="login" class="login">
    <div style="position:absolute; top:28px;left:185px;">
            <div style="width:255px;height: 38px; float:left;">
                <Bigdesk8:DBTextBox runat="server" ID="txtUsername" style="width:178px;height:35px; border:solid 0px; background-color:White;" />
            </div>
            <div style="width:200px;height: 38px; float:left;">
                <Bigdesk8:DBPasswordBox runat="server" ID="txtPassword" style="width:178px;height:35px;border:solid 0px; background-color:White;" ></Bigdesk8:DBPasswordBox>
            </div>
            <div style="height:38px; float:left;"> 
                <Bigdesk8:SubmitButton runat="server" ID="btn_login" OnClick="login_Click"  Text="登 录" style="height:36px; width:120px; background-color:#005aad;border:solid 0px; color:White; font-size:14px;cursor:pointer;" />
        </div>
     </div>
</div>
<script type="text/javascript">
    $(document).ready(function() {
        $(document).keyup(function(event) {
            if (event.keyCode == 13) {
                $("#btn_login").trigger("click");
            }
        });
    });
</script>
</form>
</body>
</html>
