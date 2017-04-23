<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage_GLB2.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.MainPage.MainPage_GLB2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-grid.css" rel="stylesheet"
        type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-dialog.css" rel="stylesheet"
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

    <script src="../../LigerUI/js/ligerui.expand.js" type="text/javascript"></script>

    <script src="../../LigerUI/js/common.js" type="text/javascript"></script>

    <script src="../../LigerUI/js/LG.js" type="text/javascript"></script>

    <link href="../../LigerUI/css/common.css" rel="stylesheet" type="text/css" />

    <script src="../Common/scripts/Common.js" type="text/javascript"></script>

    <script src="../../SparkClient/DateUtil.js" type="text/javascript"></script>

</head>
<body style="background-color: rgb(238,238,238);">
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    
      <div style=" width:1100px; float:none; margin:auto;">
      
      <table width="100%" border="0" cellspacing="0" cellpadding="0">

        <tr>
            <td style="height: 20px;">
            </td>
        </tr>
        <tr>
            <td valign="top" style="padding-left: 0px; padding-right: 0px;">
                  <table width="100%" border="0" cellspacing="0">
                    <tr>
                        <td>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="48%" valign="top">
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td style="width: 10px; height: 230px;">
                                                </td>
                                                <td>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td style="background-color: rgb(101,128,183);">
                                                                <!--background-image: url(../Common/images/FrameWorkArea/work1_03.gif)-->
                                                                <table width="100" cellpadding="0" cellspacing="0" border="0">
                                                                    <tr>
                                                                        <td style="width: 16px; height: 26px;">
                                                                            <img src="../Common/icons/pencil.png" height="16" width="16" />
                                                                        </td>
                                                                        <td style="width: 80px; height: 26px;">
                                                                            <span style="color: White;">简要统计</span>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" style="height: 204px; background-image: url(../Common/images/FrameWorkArea/work1_04.gif);
                                                                background-repeat: repeat-x;">
                                                                <div id="company" style="overflow: hidden; width: 100%; height: 190px;">
                                                                    <!--工作区一：简要统计-->
                                                                    <div id="company1" style="overflow: hidden">
                                                                        <asp:DataList ID="DataList_Jytj" runat="server">
                                                                            <ItemTemplate>
                                                                                &nbsp; &nbsp; &nbsp;<font style="font-family: 宋体; color: #000066;"><%#Eval("tjxx") %></font>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Height="30px" />
                                                                        </asp:DataList>
                                                                    </div>
                                                                    <div id="company2">
                                                                    </div>
                                                                </div>

                                                                <script>
                                                                    var speed = 65
                                                                    var dl_dbsy = document.getElementById("DataList_Jytj");
                                                                    if (dl_dbsy.children[0].rows.length >= 4) {
                                                                        company2.innerHTML = company1.innerHTML
                                                                        function Marquee2() {
                                                                            if (company2.offsetTop - company.scrollTop <= 0)
                                                                                company.scrollTop -= company1.offsetHeight
                                                                            else {
                                                                                company.scrollTop++
                                                                            }
                                                                        }
                                                                        var MyMar2 = setInterval(Marquee2, speed)
                                                                        company.onmouseover = function() { clearInterval(MyMar2) }
                                                                        company.onmouseout = function() { MyMar2 = setInterval(Marquee2, speed) }
                                                                    }
                                                                </script>

                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 10px; height: 230px;">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="10px" valign="top">
                                        &nbsp;
                                    </td>
                                    <td width="48%" valign="top">
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td style="width: 10px; height: 200px;">
                                                </td>
                                                <td>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td style="background-color: rgb(101,128,183);">
                                                                <table width="100" cellpadding="0" cellspacing="0" border="0">
                                                                    <tr>
                                                                        <td style="width: 16px; height: 26px;">
                                                                            <img src="../Common/icons/pencil.png" height="16" width="16" />
                                                                        </td>
                                                                        <td style="width: 80px; height: 26px;">
                                                                            <span style="color: White;">公文通知</span>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 174px; background-image: url(../Common/images/FrameWorkArea/work1_04.gif)">
                                                                <!--工作区一：公文通知-->
                                                                <div id="Div4" style="overflow: hidden; width: 100%; height: 160px;">
                                                                    <div id="Div5" style="overflow: hidden">
                                                                        <asp:DataList ID="DataList_Gwtz" runat="server">
                                                                            <ItemTemplate>
                                                                                &nbsp; &nbsp; &nbsp; <a href="http://58.211.133.50:81/MajordomoMVC/OnlineOffice/LookGwtz?infoID=<%#Eval("infoID")%>"
                                                                                    style='color: #000066; text-decoration: none;' target="_blank" title='<%#Eval("infotitle")%>'>
                                                                                    <%#Eval("xxmc")%></a>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Height="30px" />
                                                                        </asp:DataList>
                                                                    </div>
                                                                    <div id="Div6">
                                                                    </div>
                                                                </div>
                                                                
                                                                 <script>
                                                                     var speed = 65
                                                                     var dl_aqyh = document.getElementById("DataList_Gwtz");
                                                                     if (dl_aqyh.children[0].rows.length >= 4) {
                                                                         Div6.innerHTML = Div5.innerHTML
                                                                         function Marquee4() {
                                                                             if (Div6.offsetTop - Div4.scrollTop <= 0)
                                                                                 Div4.scrollTop -= Div5.offsetHeight
                                                                             else {
                                                                                 Div4.scrollTop++
                                                                             }
                                                                         }
                                                                         var MyMar4 = setInterval(Marquee4, speed)
                                                                         Div4.onmouseover = function() { clearInterval(MyMar4) }
                                                                         Div4.onmouseout = function() { MyMar4 = setInterval(Marquee4, speed) }
                                                                     }
                                                        </script>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 10px; height: 200px;">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="20">
                            &nbsp;
                        </td>
                    </tr>
                </table>
     
            
            
                
            </td>
        </tr>
        <tr>
            <td valign="top" style="padding-left: 0px; padding-right: 0px;">
                <table width="100%" border="0" cellspacing="0">
                    <tr>
                        <td>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="48%" valign="top">
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td style="width: 10px; height: 200px;">
                                                    <!--background-image: url(../Common/images/FrameWorkArea/work1_01.gif)background-repeat: no-repeat;-->
                                                </td>
                                                <td>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td style="background-color: rgb(101,128,183);">
                                                                <table width="100" cellpadding="0" cellspacing="0" border="0">
                                                                    <tr>
                                                                        <td style="width: 16px; height: 26px;">
                                                                            <img src="../Common/icons/pencil.png" height="16" width="16" />
                                                                        </td>
                                                                        <td style="width: 80px; height: 26px;">
                                                                            <span style="color: White;">工作指示</span>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" style="height: 174px; background-image: url(../Common/images/FrameWorkArea/work1_04.gif);
                                                                background-repeat: repeat-x;">
                                                                <!--工作区一：工作指示-->
                                                                <div id="Div7" style="overflow: hidden; width: 100%; height: 160px; padding-top: 5px;
                                                                    padding-left: 3px;">
                                                                    <marquee onmousemove="this.stop()" id="Marq03" onmouseout="this.start()" scrollamount="2"
                                                                        direction="up" loop="-1" height="100%">
                                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DataList ID="DataList_gzzs" runat="server">
                                                                                    <ItemTemplate>
                                                                                      
                                                                                            <font style="font-family: 宋体;">[指示时间]<%#Eval("Zssj")%>
                                                                                                &nbsp; [指示主题]<a style="text-decoration: none;color:#000066;" href='javascript:void(0)' onclick='f_edit(<%#Eval("ZshfId") %>)'><%#Eval("Gzzszt") %></a> &nbsp;[指示人]：<%#Eval("ZsrName")%>
                                                                                        </font>
                                                                                        <asp:HiddenField ID="hdZshfId" runat="server" Value='<%#Eval("ZshfId") %>' />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Height="24px" />
                                                                                </asp:DataList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </marquee>
                                                                </div>
                                                                <div style="display: none;">
                                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:Button ID="btnRefresh" runat="server" Text="刷新" />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>

                                                                <script>
                                                                    //                                                                    var speed = 65
                                                                    //                                                                    var dl_gzzs = document.getElementById("DataList_gzzs");
                                                                    //                                                                    if (dl_gzzs) {
                                                                    //                                                                        if (dl_gzzs.children[0].rows.length >= 5) {
                                                                    //                                                                            Div9.innerHTML = Div8.innerHTML
                                                                    //                                                                            function Marquee3() {
                                                                    //                                                                                if (Div9.offsetTop - Div7.scrollTop <= 0)
                                                                    //                                                                                    Div7.scrollTop -= Div8.offsetHeight
                                                                    //                                                                                else {
                                                                    //                                                                                    Div7.scrollTop++
                                                                    //                                                                                }
                                                                    //                                                                            }
                                                                    //                                                                            var MyMar3 = setInterval(Marquee3, speed)
                                                                    //                                                                            Div7.onmouseover = function() { clearInterval(MyMar3) }
                                                                    //                                                                            Div7.onmouseout = function() { MyMar3 = setInterval(Marquee3, speed) }
                                                                    //                                                                        }
                                                                    //                                                                    }

                                                                       
                                                                </script>

                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 10px; height: 200px;">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="4%" valign="top">
                                        &nbsp;
                                    </td>
                                    <td width="48%" valign="top">
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td style="width: 10px; height: 200px;">
                                                </td>
                                                <td>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td style="background-color: rgb(101,128,183);">
                                                                <table width="100" cellpadding="0" cellspacing="0" border="0">
                                                                    <tr>
                                                                        <td style="width: 16px; height: 26px;">
                                                                            <img src="../Common/icons/pencil.png" height="16" width="16" />
                                                                        </td>
                                                                        <td style="width: 80px; height: 26px;">
                                                                            <span style="color: White;">预警提醒</span>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 174px; background-image: url(../Common/images/FrameWorkArea/work1_04.gif)">
                                                                <!--工作区一：预警提醒-->
                                                                <div id="Div10" style="overflow: hidden; width: 100%; height: 160px;">
                                                                    <div id="Div11" style="overflow: hidden">
                                                                    </div>
                                                                    <div id="Div12">
                                                                    </div>
                                                                </div>

                                                                <script>
                                                                    //                                                                        var speed = 65
                                                                    //                                                                        var dl_Zlct = document.getElementById("DataList_Zlct");
                                                                    //                                                                        if (dl_Zlct) {
                                                                    //                                                                            if (dl_Zlct.children[0].rows.length >= 4) {
                                                                    //                                                                                Div6.innerHTML = Div5.innerHTML
                                                                    //                                                                                function Marquee4() {
                                                                    //                                                                                    if (Div6.offsetTop - Div4.scrollTop <= 0)
                                                                    //                                                                                        Div4.scrollTop -= Div5.offsetHeight
                                                                    //                                                                                    else {
                                                                    //                                                                                        Div4.scrollTop++
                                                                    //                                                                                    }
                                                                    //                                                                                }
                                                                    //                                                                                var MyMar4 = setInterval(Marquee4, speed)
                                                                    //                                                                                Div4.onmouseover = function() { clearInterval(MyMar4) }
                                                                    //                                                                                Div4.onmouseout = function() { MyMar4 = setInterval(Marquee4, speed) }
                                                                    //                                                                            }
                                                                    //                                                                        }
                                                                </script>

                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 10px; height: 200px;">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
      
      </div>
    
    

    <script type="text/javascript">
        var activeDialog;
        $("div.link").live("mouseover", function() {
            $(this).addClass("linkover");

        }).live("mouseout", function() {
            $(this).removeClass("linkover");


        }).live('click', function(e) {
            var text = $("a", this).html();
            var url = $(this).attr("url");
            parent.f_addTab(null, text, url);
        });
        function f_edit(zshfId) {
            OpenWindow('../Zlct/Gzzs_Gzfk_Edit.aspx?zshfId=' + zshfId, "工作指示回复", 950, 450, true);
        }


        function refresh() {
            $("#btnRefresh").click();
        }



        function OpenWindow(url, title, width, height, isReload) {
            var dialogOptions = { width: width, height: height, title: title, url: url, showMax: true, showMin: true, buttons: [
                { text: '回复', onclick: function(item, dialog) {
                    dialog.frame.f_save(dialog, null);
                    refresh();
                }
                },
                { text: '关闭', onclick: function(item, dialog) {
                    if (isReload) {

                    }
                    dialog.close();
                }
                }
                ], isResize: true, timeParmName: 'a'
            };
            activeDialog = parent.parent.$.ligerDialog.open(dialogOptions);
        }
    </script>

    </form>
</body>
</html>
