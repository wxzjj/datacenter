<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeftFrame.aspx.cs" Inherits="WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.EntranceGlyh.LeftFrame" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
    <title>左侧框架-企业版</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <script src="../Common/scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="../Common/scripts/common.js" type="text/javascript"></script>
    <script src="../Common/scripts/frame.js" type="text/javascript"></script>

</head>
<body class="leftframe">
    <form id="form1" runat="server">
    <div id="table_show">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 147px;">
            <tr class="menutitlebk">
                <td class="menutitleshow">
                    <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/home.png" align="absmiddle" height="16" width="16" />
                    系统菜单
                </td>
                <td style="text-align: right; padding-right: 10px; width: 16px;">
                    <img id="img_left" onclick="opFrameset();" src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/dhhide.png"
                        style="height: 16px; width: 16px; cursor: pointer" align="absmiddle" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="mainmenu" style="overflow: auto;">
                        <input type="hidden" id="hd" name="hd" value="" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="table_hidden" style="display: none;">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td class="menutitlehide"> 
                    <img id="img_right" onclick="opFrameset();" src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/dhshow.png"
                        style="height: 16px; width: 16px; display: none; cursor: pointer" align="absmiddle" />
                </td>
            </tr>
            <tr>
                <td class="menutitlehidefont">
                    <span onclick="opFrameset();">系<br />
                        统<br />
                        菜<br />
                        单<br />
                    </span>
                </td>
            </tr>
        </table>
    </div>
    <input type="hidden" id="Hidden1" value="" />
    <input type="hidden" id="hd_num" value="" />
    <script type="text/javascript">

        function opFramesetRevert() {
            var fs = parent.document.getElementsByTagName("frameset")[2];
            //alert(fs.cols);
            if (fs.cols == "30,5,*") {
                fs.cols = "147,5,*";
                //$("#dhoperate").html("隐藏导航栏");
                $("#table_show").css("display", "block");
                $("#table_hidden").css("display", "none");
                $("#img_left").css("display", "block");
                $("#img_right").css("display", "none");
            }
        }


        function initmenu(parentNodeID) {
            opFramesetRevert();
            $("ul.menulist li").live('click', function() {
                var jitem = $(this);
                var jitem_text = $("span", this).html();
                //alert(jitem_text);
                var tabid = jitem.attr("tabid");
                var url = jitem.attr("url");
                if (!url) return;
                if (!tabid) {
                    //给url附加menuno
                    var menuno = jitem.attr("menuno");
                    if (url.indexOf('?') > -1) url += ";";
                    else url += "?";
                    if (jitem_text == "数据共享") {
                        url += "MenuNo=" + menuno + ";MenuText=" + "数据共享-智慧无锡一中心四平台数据交互";
                    }
                    else {
                        url += "MenuNo=" + menuno + ";MenuText=" + jitem_text;
                    }
                    jitem.attr("url", url);
                    var hdval = $("#hd").val();
                    if (hdval != '') {
                        $("#" + hdval).removeClass("over");
                    }
                    $("#hd").val(menuno);
                    jitem.addClass("over");
                    top.parent.rightFrame.location = "RightFrame.aspx?url=" + url;
                }
            }).live('mouseover', function() {
                var jitem = $(this);
                jitem.addClass("over");
            }).live('mouseout', function() {
                var jitem = $(this);
                if (jitem.attr("menuno") != $("#hd").val()) {
                    jitem.removeClass("over");
                }
            });

            var mainmenu = $("#mainmenu");
            $.ajax({
                type: "post",
                url: "../Handler/TreeData.ashx?parentNodeID=" + parentNodeID,
                datatype: "json",
                success: function(result) {
                    //alert(result);
                    var menus = eval('(' + result + ')');
                    /*--------------------- 菜单初始化：BEGIN--------------------------*/
                    var i = 0;
                    var item;
                    $(menus).each(function(i, menu) {
                        //                        i = i + 1;
                        //                        if (i != 1) {
                        //                            item = $('<div id=' + menu.text + '><ul class="menulist"><span style="padding-left:30px;">' + menu.text + '</span><span name=' + menu.text + ' class="togglebtn togglebtn-down"/></span></ul></div>');
                        //                            $(menu.children).each(function(j, submenu) {
                        //                                var subitem = $('<li style="display:none;"><img/><span></span><div class="menuitem-l"></div><div class="menuitem-r"></div></li>');
                        //                                subitem.attr({
                        //                                    url: submenu.url,
                        //                                    menuno: submenu.tabid,
                        //                                    id: submenu.tabid
                        //                                });
                        //                                $("img", subitem).attr("src", "<%=baseimgurl %>" + submenu.imgurl);
                        //                                $("span", subitem).html(submenu.text);

                        //                                $("ul:first", item).append(subitem);
                        //                            });
                        //                            mainmenu.append(item);
                        //                        }
                        //                        else {
                        item = $('<div id=' + menu.text + '><ul class="menulist"><span style="padding-left:30px;">' + menu.text + '</span><span name=' + menu.text + ' class="togglebtn togglebtn-down"/></span></ul></div>');
                        $(menu.children).each(function(j, submenu) {
                            var subitem = $('<li><img/><span></span><div class="menuitem-l"></div><div class="menuitem-r"></div></li>');
                            subitem.attr({
                                url: submenu.url,
                                menuno: submenu.tabid,
                                id: submenu.tabid
                            });
                            $("img", subitem).attr("src", "<%=baseimgurl %>" + submenu.imgurl);
                            $("span", subitem).html(submenu.text);

                            $("ul:first", item).append(subitem);
                        });
                        mainmenu.append(item);
                        //                        }
                        //document.write(mainmenu.html());

                    });
                    /*--------------------- 菜单初始化：END--------------------------*/

                },
                error: function(request, status, error) {
                    //alert("数据连接超时！");
                }

            });
        }

    </script>
    </form>
</body>
</html>
