<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page_List.aspx.cs" Inherits="WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Page_List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <title>无锡市建筑市场监管公共服务平台</title>
    <meta name="keywords" content="abc" />
    <meta name="description" content="abc" />
    <link href="Common/css/Startcss_1.css" rel="stylesheet" type="text/css" />
    <link href="Common/css/Startcss_2.css" rel="stylesheet" type="text/css" />

    <script src="Common/scripts/jquery-1.9.1.js" type="text/javascript"></script>

    <style type="text/css">
        .menu
        {
            display: none;
        }
        #share
        {
            position: fixed;
            _position: absolute;
            _top: expression(eval(document.documentElement.scrollTop+document.documentElement.clientHeight)-64+"px");
            bottom: 64px;
            left: 92%;
            width: 30px;
            zoom: 1;
        }
        #share a
        {
            background-image: url(Common/images/Web/share.png);
            background-repeat: no-repeat;
            display: block;
            width: 30px;
            height: 30px;
            margin-bottom: 2px;
            overflow: hidden;
            text-indent: -999px;
            -webkit-transition: all 0.2s ease-in-out;
            -moz-transition: all 0.2s ease-in-out;
            -o-transition: all 0.2s ease-in-out;
            -ms-transition: all 0.2s ease-in-out;
            transition: all 0.2s ease-in-out;
        }
        #share a
        {
        }
        #share .sina
        {
            background-position: 0 0;
            position: absolute;
            bottom: 62px;
        }
        #share a.sina:hover
        {
            background-position: -30px 0;
        }
        #share .tencent
        {
            background-position: 0 -30px;
            position: absolute;
            bottom: 30px;
        }
        #share a.tencent:hover
        {
            background-position: -30px -30px;
        }
        #share a#totop
        {
            background-position: 0 -120px;
            position: absolute;
            bottom: 94px;
            cursor: pointer;
        }
        #share a#totop:hover
        {
            background-position: -30px -120px;
        }
    </style>
    <!--[if IE]>
  <script src="Common/scripts/html5.js"></script>
<![endif]-->

    <script type="text/javascript">
        //scrolltotop
        $(function() {
            //首先将#back-to-top隐藏
            $("#totop").hide();
            //当滚动条的位置处于距顶部100像素以下时，跳转链接出现，否则消失
            $(function() {
                $(window).scroll(function() {
                    if ($(window).scrollTop() > 100) {
                        $("#totop").fadeIn();
                    }
                    else {
                        $("#totop").fadeOut();
                    }
                });
                //当点击跳转链接后，回到页面顶部位置
                $("#totop").click(function() {
                    $('body,html').animate({ scrollTop: 0 }, 500);
                    return false;
                });
            });
        }); 
    </script>

</head>
<body class="body_bg">
    <form id="form1" runat="server">
    <!-- Page_Top:begin -->
    <iframe width="100%" height="190px" frameborder="0"  scrolling="no" src="Page_Top.aspx">
    </iframe>
    <!-- Page_Top:end -->
    <!-- Page_Main:begin -->
    <div class="w1002 clearfix">
        <div class="list-left">
            <div class="list-left-main">
                <h2>
                    <span id="listtitle">导航菜单</span></h2>
                <ul class="menu" id="menu_01">
                    <li class="li-link"><a href="#" onclick="javascript:document.getElementById('iframe_main').src='Gcxm/Zhjg_Lxxmdj_List.aspx'">
                        项目信息</a></li>
                    <li class="li-link"><a href="#" onclick="javascript:document.getElementById('iframe_main').src='Gcxm/Zhjg_Sgtsc_List.aspx'">
                        施工图审查</a></li>
                    <li class="li-link"><a href="#" onclick="javascript:document.getElementById('iframe_main').src='Gcxm/Zhjg_Zbtb_List.aspx'">
                        招标投标</a></li>
                    <li class="li-link"><a href="#" onclick="javascript:document.getElementById('iframe_main').src='Gcxm/Zhjg_Htba_List.aspx'">
                        合同备案</a></li>
                    <li class="li-link"><a href="#" onclick="javascript:document.getElementById('iframe_main').src='Gcxm/Zhjg_Aqbj_List.aspx'">
                        安全监督</a></li>
                    <li class="li-link"><a href="#" onclick="javascript:document.getElementById('iframe_main').src='Gcxm/Zhjg_Zlbj_List.aspx'">
                        质量报监</a></li>
                    <li class="li-link"><a href="#" onclick="javascript:document.getElementById('iframe_main').src='Gcxm/Zhjg_Sgxkz_List.aspx'">
                        施工许可</a></li>
                    <li class="li-link"><a href="#" onclick="javascript:document.getElementById('iframe_main').src='Gcxm/Zhjg_Jgba_List.aspx'">
                        竣工备案</a></li>
                </ul>
                <ul class="menu" id="menu_02">
                    <li class="li-link"><a href="#" onclick="javascript:document.getElementById('iframe_main').src='Sczt/Jsdw_List.aspx'">
                        建设单位</a></li>
                    <li class="li-link"><a href="#" onclick="javascript:document.getElementById('iframe_main').src='Sczt/Kcdw_List.aspx'">
                        勘察单位</a></li>
                    <li class="li-link"><a href="#" onclick="javascript:document.getElementById('iframe_main').src='Sczt/Sjdw_List.aspx'">
                        设计单位</a></li>
                    <li class="li-link"><a href="#" onclick="javascript:document.getElementById('iframe_main').src='Sczt/Sgdw_List.aspx'">
                        施工单位</a></li>
                    <li class="li-link"><a href="#" onclick="javascript:document.getElementById('iframe_main').src='Sczt/Zjjg_List.aspx'">
                        中介机构</a></li>
                </ul>
                <ul class="menu" id="menu_03">
                    <li class="li-link"><a href="javascript:void(0)" onclick="javascript:document.getElementById('iframe_main').src='Zyry/Zczyry_List.aspx'">
                        注册执业人员</a></li>
                    <li class="li-link"><a href="javascript:void(0)" onclick="javascript:document.getElementById('iframe_main').src='Zyry/Aqscglry_List.aspx'">
                        安全生产管理人员</a></li>
                    <li class="li-link"><a href="javascript:void(0)" onclick="javascript:document.getElementById('iframe_main').src='Zyry/Qyjjry_List.aspx'">
                        企业技经人员</a></li>
                    <li class="li-link"><a href="javascript:void(0)" onclick="javascript:document.getElementById('iframe_main').src='Zyry/Zygwglry_List.aspx'">
                        专业岗位管理人员</a></li>
                </ul>
                <ul class="menu" id="menu_04">
                    <li class="li-link"><a href="javascript:void(0)" onclick="javascript:document.getElementById('iframe_main').src='Xytx/Xykp_List.aspx'">
                        施工企业</a></li>
                </ul>
            </div>
        </div>
        <div class="list-right">
            <iframe id="iframe_main" width="100%" frameborder="0" onload="this.height=588" scrolling="no">
            </iframe>

            <script type="text/javascript">
                function reinitIframe() {
                    var iframe = document.getElementById("iframe_main");
                    try {
                        var bHeight = iframe.contentWindow.document.body.scrollHeight;
                        var dHeight = iframe.contentWindow.document.documentElement.scrollHeight;
                        var height = Math.max(bHeight, dHeight);
                        iframe.height = height;
                    }
                    catch (ex) {
                    }
                }
                window.setInterval("reinitIframe()", 200); 
            </script>

        </div>
    </div>
    <!-- Page_Main:end-->
    <div id="share">
        <a id="totop" title="">返回顶部</a> <a href="##" target="_blank" class="sina">新浪微博</a>
        <a href="##" target="_blank" class="tencent">腾讯微博</a>
    </div>
    <!-- Page_End:begin -->
    <iframe width="100%" frameborder="0" height="96" scrolling="no" src="Page_End.aspx">
    </iframe>
    <!-- Page_End:end -->

    <script type="text/javascript">
        $(document).ready(function() {
            var menuno = '<%=menuno %>';
            var itemno = '<%=itemno %>';
            var contentId = '<%=contentId %>';

            $("#menu_" + menuno).show();
            //alert($("#listtitle").html());
            switch (menuno) {
                case "01":
                    {
                        if (itemno == "01") {
                            document.getElementById("iframe_main").src = "Gcxm/Zhjg_Lxxmdj_List.aspx";
                        }
                        if (itemno == "02") {
                            document.getElementById("iframe_main").src = "Gcxm/Zhjg_Sgtsc_List.aspx";
                        }
                        if (itemno == "03") {
                            document.getElementById("iframe_main").src = "Gcxm/Zhjg_Zbtb_List.aspx";
                        }
                        if (itemno == "04") {
                            document.getElementById("iframe_main").src = "Gcxm/Zhjg_Htba_List.aspx";
                        }
                        if (itemno == "05") {
                            document.getElementById("iframe_main").src = "Gcxm/Zhjg_Aqbj_List.aspx";
                        }
                        if (itemno == "06") {
                            document.getElementById("iframe_main").src = "Gcxm/Zhjg_Zlbj_List.aspx";
                        }
                        if (itemno == "07") {
                            document.getElementById("iframe_main").src = "Gcxm/Zhjg_Sgxkz_List.aspx";
                        }
                        if (itemno == "08") {
                            document.getElementById("iframe_main").src = "Gcxm/Zhjg_Jgba_List.aspx";
                        }


                        if (itemno == "") {
                            document.getElementById("iframe_main").src = "Gcxm/Zhjg_Lxxmdj_List.aspx";
                        }
                        $("#listtitle").html("工程项目");

                    }
                    break;
                case "02":
                    {
                        if (itemno == "01") {
                            document.getElementById("iframe_main").src = "Sczt/Jsdw_List.aspx";
                        }
                        if (itemno == "02") {
                            document.getElementById("iframe_main").src = "Sczt/Kcdw_List.aspx";
                        }
                        if (itemno == "03") {
                            document.getElementById("iframe_main").src = "Sczt/Sjdw_List.aspx";
                        }
                        if (itemno == "04") {
                            document.getElementById("iframe_main").src = "Sczt/Sgdw_List.aspx";
                        }
                        if (itemno == "05") {
                            document.getElementById("iframe_main").src = "Sczt/Zjjg_List.aspx";
                        }
                        if (itemno == "06") {
                            document.getElementById("iframe_main").src = "Sczt/Qtdw_List.aspx";
                        }
                        if (itemno == "") {
                            document.getElementById("iframe_main").src = "Sczt/Jsdw_List.aspx";
                        }

                        $("#listtitle").html("市场主体");
                    }
                    break;
                case "03":
                    {
                        if (itemno == "01") {
                            document.getElementById("iframe_main").src = "Zyry/Zczyry_List.aspx";
                        }
                        if (itemno == "02") {
                            document.getElementById("iframe_main").src = "Zyry/Aqscglry_List.aspx";
                        }
                        if (itemno == "03") {
                            document.getElementById("iframe_main").src = "Zyry/Qyjjry_List.aspx";
                        }
                        if (itemno == "04") {
                            document.getElementById("iframe_main").src = "Zyry/Zygwglry_List.aspx";
                        }

                        if (itemno == "") {
                            document.getElementById("iframe_main").src = "Zyry/Zczyry_List.aspx";
                        }

                        $("#listtitle").html("执业人员");
                    }
                    break;
                case "04":
                    if (itemno == "") {
                        document.getElementById("iframe_main").src = "Xytx/Xykp_List.aspx";
                    }
                    if (itemno == "01") {
                        document.getElementById("iframe_main").src = "Xytx/Xykp_List.aspx";
                    }

                    { $("#listtitle").html("信用体系"); }
                    break;
            }
        }
        );

        function menuClick(url, menuno, itemno, menuText) {

            document.getElementById('iframe_main').src = url + "?menuno=" + menuno + "&itemno=" + itemno + "&menutext=" + menuText;
        }

        function menuClick2(url, menuno, menuText) {

            document.getElementById('iframe_main').src = url + "?menuno=" + menuno + "&itemno=" + itemno + "&menutext=" + menuText;
        }
    </script>

    </form>
</body>
</html>
