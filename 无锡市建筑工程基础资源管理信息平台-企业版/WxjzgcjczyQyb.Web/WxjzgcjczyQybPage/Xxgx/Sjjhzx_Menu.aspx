<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sjjhzx_Menu.aspx.cs" Inherits="WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Xxgx.Sjjhzx_Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>数据交互展现</title>
    <link href="../../App_Themes/WxjzgcjczyQyb_B_Theme/base.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/WxjzgcjczyQyb_B_Theme/main.css" rel="stylesheet" type="text/css" />

    <script src="../Common/scripts/jquery-1.7.1.min.js" type="text/javascript"></script>

    <script src="../Common/scripts/common.js" type="text/javascript"></script>

    <link href="../../SparkClient/jquery.ui-1.8.2.css" rel="stylesheet" type="text/css" />

    <script src="../../SparkClient/jquery.ui-1.8.2.min.js" type="text/javascript"></script>

    <script src="../../SparkClient/jquery.validate.min.js" type="text/javascript"></script>

    <script src="../../SparkClient/script.js" type="text/javascript"></script>

    <script src="../../SparkClient/Calendar.js" type="text/javascript"></script>

    <script src="../../SparkClient/control.js" type="text/javascript"></script>

    <script src="../Common/jquery-easyui-1.3.3/jquery.easyui.min.js" type="text/javascript"></script>

    <link href="../Common/jquery-easyui-1.3.3/themes/metro/easyui.css" rel="stylesheet"
        type="text/css" />
    <link href="../Common/jquery-easyui-1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        $(document).ready(function() {
            var _addHeight = $(window).height() - $("body").outerHeight(true);
            var height = $(window).height();
            var width = $(window).width();
            tabResize(width, height);

            $(".childtab").load(function() {
                $("." + this.id).css("display", "none");
            });
        });

        function tabResize(width, height) {

            $("#tabs").tabs({
                width: width - 10,
                height: height,
                tabPosition: "top",
                onSelect: function(title) {
                    switch (title) {
                        case "人员信息":
                            $('#iframe1').attr("src", "Syzxspt_Ryxx.aspx");
                            break;
                        case "竣工验收备案":
                            $('#iframe2').attr("src", "Syzxspt_Jgba.aspx");
                            break;
                        case "施工许可证":
                            $('#iframe3').attr("src", "Syzxspt_Sgxk.aspx");
                            break;
                        case "工程项目信息":
                            $('#iframe4').attr("src", "Syzxspt_Gcxm.aspx");
                            break;
                        case "工程招标中标":
                            $('#iframe5').attr("src", "Syzxspt_Zbzb.aspx");
                            break;
                        case "保障房源":
                            $('#iframe6').attr("src", "Syzxspt_Bzfy.aspx");
                            break;
                        case "保障对象":
                            $('#iframe7').attr("src", "Syzxspt_Bzdx.aspx");
                            break;
                        case "保障对象家庭成员":
                            $('#iframe8').attr("src", "Syzxspt_Bzdxjtcy.aspx");
                            break;
                        case "物业管理从业人员":
                            $('#iframe9').attr("src", "Syzxspt_Wyglcyry.aspx");
                            break;
                    }

                }
            });
        }

        function selectTab(tabIndex) {
            $("#tabs").tabs("select", tabIndex)
        }
 
     
    </script>

</head>
<body style="margin: 0px; padding: 0; overflow: hidden;">
    <form id="form1" runat="server">
    <div style="width: 100%; margin: 0 2px 0 5px;">
        <div id="tabs" style="margin: 0; padding: 0;" class="easyui-tabs">
            <div title="人员信息" style="width: 100%; padding: 5px 2px 3px 3px;">
                <div class="iframe1">
                    <img src="../Common/images/icons/loading.gif" align="absmiddle" /><span style="color: #F97C10;">页面载入中...</span>
                </div>
                <iframe id="iframe1" class="childtab" scrolling="auto" frameborder="0" style="width: 100%;
                    padding: 0; margin: 0; background-color: #FFF; height: 99%;"></iframe>
            </div>
            <div title="竣工验收备案" style="width: 100%; padding: 5px 2px 3px 3px;">
                <div class="iframe2">
                    <img src="../Common/images/icons/loading.gif" align="absmiddle" /><span style="color: #F97C10;">页面载入中...</span>
                </div>
                <iframe id="iframe2" class="childtab" scrolling="auto" frameborder="0" style="width: 100%;
                    padding: 0; margin: 0; background-color: #FFF; height: 99%;"></iframe>
            </div>
            <div title="施工许可证" style="width: 100%; padding: 5px 2px 3px 3px;">
                <div class="iframe3">
                    <img src="../Common/images/icons/loading.gif" align="absmiddle" /><span style="color: #F97C10;">页面载入中...</span>
                </div>
                <iframe id="iframe3" class="childtab" scrolling="auto" frameborder="0" style="width: 100%;
                    padding: 0; margin: 0; background-color: #FFF; height: 99%;"></iframe>
            </div>
            <div title="工程项目信息" style="width: 100%; padding: 5px 2px 3px 3px;">
                <div class="iframe4">
                    <img src="../Common/images/icons/loading.gif" align="absmiddle" /><span style="color: #F97C10;">页面载入中...</span>
                </div>
                <iframe id="iframe4" class="childtab" scrolling="auto" frameborder="0" style="width: 100%;
                    padding: 0; margin: 0; background-color: #FFF; height: 99%;"></iframe>
            </div>
            <div title="工程招标中标" style="width: 100%; padding: 5px 2px 3px 3px;">
                <div class="iframe5">
                    <img src="../Common/images/icons/loading.gif" align="absmiddle" /><span style="color: #F97C10;">页面载入中...</span>
                </div>
                <iframe id="iframe5" class="childtab" scrolling="auto" frameborder="0" style="width: 100%;
                    padding: 0; margin: 0; background-color: #FFF; height: 99%;"></iframe>
            </div>
            <div title="保障房源" style="width: 100%; padding: 5px 2px 3px 3px;">
                <div class="iframe6">
                    <img src="../Common/images/icons/loading.gif" align="absmiddle" /><span style="color: #F97C10;">页面载入中...</span>
                </div>
                <iframe id="iframe6" class="childtab" scrolling="auto" frameborder="0" style="width: 100%;
                    padding: 0; margin: 0; background-color: #FFF; height: 99%;"></iframe>
            </div>
            <div title="保障对象" style="width: 100%; padding: 5px 2px 3px 3px;">
                <div class="iframe7">
                    <img src="../Common/images/icons/loading.gif" align="absmiddle" /><span style="color: #F97C10;">页面载入中...</span>
                </div>
                <iframe id="iframe7" class="childtab" scrolling="auto" frameborder="0" style="width: 100%;
                    padding: 0; margin: 0; background-color: #FFF; height: 99%;"></iframe>
            </div>
            <div title="保障对象家庭成员" style="width: 100%; padding: 5px 2px 3px 3px;">
                <div class="iframe8">
                    <img src="../Common/images/icons/loading.gif" align="absmiddle" /><span style="color: #F97C10;">页面载入中...</span>
                </div>
                <iframe id="iframe8" class="childtab" scrolling="auto" frameborder="0" style="width: 100%;
                    padding: 0; margin: 0; background-color: #FFF; height: 99%;"></iframe>
            </div>
            <div title="物业管理从业人员" style="width: 100%; padding: 5px 2px 3px 3px;">
                <div class="iframe9">
                    <img src="../Common/images/icons/loading.gif" align="absmiddle" /><span style="color: #F97C10;">页面载入中...</span>
                </div>
                <iframe id="iframe9" class="childtab" scrolling="auto" frameborder="0" style="width: 100%;
                    padding: 0; margin: 0; background-color: #FFF; height: 99%;"></iframe>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
