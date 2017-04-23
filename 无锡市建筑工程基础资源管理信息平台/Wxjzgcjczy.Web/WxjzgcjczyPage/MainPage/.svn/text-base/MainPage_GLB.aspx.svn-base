<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage_GLB.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.MainPage.MainPage_GLB" %>

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

    <%--  <script language="javascript"">
        $.ajax({
                 type: 'POST',
                 url: '../Handler/Select.ashx?type=mmjgnd&rnd=' + (new Date()).toString(),
                 async: false,
                 cache: false,
                 data: null,
                 dataType: 'json',
                 success: function(res) {
                     $("#year").val(res.nd);
                  
                 },
                 error: function(err) {
                 }
             });

    </script>--%>
</head>
<body style="background-color: rgb(238,238,238);">
    <form runat="server">
    <asp:ScriptManager runat="server">
    </asp:ScriptManager>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td style="height: 25px;">
            </td>
        </tr>
        <tr>
            <td valign="top" style="padding-left: 30px; padding-right: 30px;">
                <table width="100%" border="0" cellspacing="0">
                    <tr>
                        <td>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="49.5%" valign="top">
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td style="width: 10px; height: 245px;">
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
                                                            <td style="height: 219px; border: solid 1px rgb(101,108,183); background-color: White;
                                                                padding: 5px;">
                                                                <!--background-image: url(../Common/images/FrameWorkArea/work1_04.gif);
                                                                background-repeat: repeat-x;"-->
                                                                <div id="company" style="overflow: hidden; width: 100%; height: 219px;">
                                                                    <!--工作区一：简要统计-->
                                                                    <marquee onmousemove="this.stop()" id="Marq01" onmouseout="this.start()" scrollamount="2"
                                                                        direction="up" loop="-1" height="219px">
                                                                       <asp:DataList ID="DataList_Jytj" runat="server" Width="100%">
                                                                            <ItemTemplate>
                                                                               <table width="100%" border="0" cellspacing="2" cellpadding="0">
                                                                                <tr>
                                                                                <td align="left" colspan="4">
                                                                               <span style="font-size:15px; color:Blue"> <b>工程项目</b></span>
                                                                                </td>
                                                                                </tr>
                                                                                 <tr>
                                                                                <td align="left" colspan="4">
                                                                              <b>在建项目：安全监督</b>（<span style="color:Red"><%#Eval("zjajxm") %></span></span>个）
                                                                                </td>
                                                                                </tr>
                                                                                <tr>
                                                                                <td width="25%"><a href="Index_Szgc.aspx?state=zj&xmlx=aqjd&ssdq=sxq">市辖区（<span style="color:Red"><%#Eval("sxsq1") %></span>个）</a></td><td width="25%"><a href="Index_Szgc.aspx?state=zj&xmlx=aqjd&ssdq=xsq">锡山区（<span style="color:Red"><%#Eval("xsq1") %></span>个）</a></td><td width="25%"><a href="Index_Szgc.aspx?state=zj&xmlx=aqjd&ssdq=hsq">惠山区（<span style="color:Red"><%#Eval("hsq1") %></span>个）</a></td><td width="25%"><a href="Index_Szgc.aspx?state=zj&xmlx=aqjd&ssdq=bhq">滨湖区（<span style="color:Red"><%#Eval("bhq1") %></span>个）</a></td>
                                                                                </tr>
                                                                                <tr>
                                                                                 <td ><a href="Index_Szgc.aspx?state=zj&xmlx=aqjd&ssdq=xq">新区（<span style="color:Red"><%#Eval("xq1") %></span>个）</a></td><td ><a href="Index_Szgc.aspx?state=zj&xmlx=aqjd&ssdq=jy">江阴（<span style="color:Red"><%#Eval("jy1") %></span>个）</a></td><td ><a href="Index_Szgc.aspx?state=zj&xmlx=aqjd&ssdq=yx">宜兴（<span style="color:Red"><%#Eval("yx1") %></span>个）</a></td><td ></td>
                                                                                </tr>
                                                                                
                                                                                
                                                                                 <tr>
                                                                                <td align="left" colspan="4">
                                                                               <b>竣工项目：安全监督</b>（<span style="color:Red"><%#Eval("jgajxm") %></span></span>个）
                                                                                </td>
                                                                                </tr>
                                                                                <tr>
                                                                                <td width="25%"><a href="Index_Szgc.aspx?state=jg&xmlx=aqjd&ssdq=sxq">市辖区（<span style="color:Red"><%#Eval("sxsq2") %></span>个）</a></td><td width="25%"><a href="Index_Szgc.aspx?state=jg&xmlx=aqjd&ssdq=xsq">锡山区（<span style="color:Red"><%#Eval("xsq2") %></span>个）</a></td><td width="25%"><a href="Index_Szgc.aspx?state=jg&xmlx=aqjd&ssdq=hsq">惠山区（<span style="color:Red"><%#Eval("hsq2") %></span>个）</a></td><td width="25%"><a href="Index_Szgc.aspx?state=jg&xmlx=aqjd&ssdq=bhq">滨湖区（<span style="color:Red"><%#Eval("bhq2") %></span>个）</a></td>
                                                                                </tr>
                                                                                <tr>
                                                                                 <td ><a href="Index_Szgc.aspx?state=jg&xmlx=aqjd&ssdq=xq">新区（<span style="color:Red"><%#Eval("xq2") %></span>个）</a></td><td ><a href="Index_Szgc.aspx?state=jg&xmlx=aqjd&ssdq=jy">江阴（<span style="color:Red"><%#Eval("jy2") %></span>个）</a></td><td ><a href="Index_Szgc.aspx?state=jg&xmlx=aqjd&ssdq=yx">宜兴（<span style="color:Red"><%#Eval("yx2") %></span>个）</a></td><td ></td>
                                                                                </tr>
                                                                                 <tr>
                                                                                <td align="left" colspan="4">
                                                                               <span style="font-size:15px; color:Blue"> <b>市场主体</b></span>
                                                                                </td>
                                                                                </tr>
                                                                                <tr>
                                                                                <td colspan="4">
                                                                                <table border="0" width="100%" cellpadding="0" cellspacing="2">
                                                                                <tr>
                                                                                <td width="33%"><a href="Index_Szqy.aspx?dwlx=jsdw">建设单位（<span style="color:Red"><%#Eval("jsdwNo") %></span>个）</a></td>
                                                                                <td width="33%"><a href="Index_Szqy.aspx?dwlx=kcsj">勘察设计单位（<span style="color:Red"><%#Eval("kcsjdwNo") %></span>个）</a></td>
                                                                                <td width="33%"><a href="Index_Szqy.aspx?dwlx=sgdw">施工单位（<span style="color:Red"><%#Eval("sgdwNo") %></span>个）</a></td>
                                                                                </tr>
                                                                                <tr>
                                                                                 <td width="33%"><a href="Index_Szqy.aspx?dwlx=zjjg">中介机构（<span style="color:Red"><%#Eval("zjjgNo") %></span>个）</a></td>
                                                                                <td width="33%"><a href="Index_Szqy.aspx?dwlx=qt">其 他（<span style="color:Red"><%#Eval("qtNo") %></span>个）</a></td>
                                                                                <td width="33%"></td>
                                                                                </tr>
                                                                                </table>
                                                                                </td>
                                                                                </tr>
                                                                                
                                                                                <tr>
                                                                                <td colspan="4">
                                                                                <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                                                 <tr>
                                                                                <td align="left" colspan="5">
                                                                               <span style="font-size:15px; color:Blue"> <b>执业人员</b></span>
                                                                                </td>
                                                                                </tr>
                                                                                 <tr>
                                                                                <td align="left" colspan="5">
                                                                               <span > <b>注册（执业）人员</b></span>
                                                                                </td>
                                                                                </tr>
                                                                                <tr>
                                                                                  <td width="20%"><a href="Index_Zyry.aspx?zyry=zczyry&rylx=jzs">建造师（<span style="color:Red"><%#Eval("jzsNo") %></span>个）</a></td>
                                                                                <td width="20%"><a href="Index_Zyry.aspx?zyry=zczyry&rylx=jls">监理师（<span style="color:Red"><%#Eval("jlsNo") %></span>个）</a></td>
                                                                                <td width="20%"><a href="Index_Zyry.aspx?zyry=zczyry&rylx=zjs">造价师（<span style="color:Red"><%#Eval("zjsNo") %></span>个）</a></td>
                                                                                <td width="20%"><a href="Index_Zyry.aspx?zyry=zczyry&rylx=jzhus">建筑师（<span style="color:Red"><%#Eval("jzhusNo") %></span>个）</a></td>
                                                                                 <td width="20%"><a href="Index_Zyry.aspx?zyry=zczyry&rylx=jgs">结构师（<span style="color:Red"><%#Eval("jgsNo") %></span>个）</a></td>
                                                                                </tr>
                                                                                <tr>
                                                                                <td colspan="3">
                                                                                 <span > <b>安全生产管理人员</b></span>
                                                                                </td>
                                                                                 <td colspan="2">
                                                                                 <span > <b>技经人员</b></span>
                                                                                </td>
                                                                                </tr>
                                                                                <tr>
                                                                                 <td ><a href="Index_Zyry.aspx?zyry=aqscglry&rylx=al">A类（<span style="color:Red"><%#Eval("alNo") %></span>个）</a></td>
                                                                                <td ><a href="Index_Zyry.aspx?zyry=aqscglry&rylx=bl">B类（<span style="color:Red"><%#Eval("blNo") %></span>个）</a></td>
                                                                                <td ><a href="Index_Zyry.aspx?zyry=aqscglry&rylx=cl">C类（<span style="color:Red"><%#Eval("clNo") %></span>个）</a></td>
                                                                                <td > <a href="Index_Zyry.aspx?zyry=qyjjry&rylx=jsry">技术人员（<span style="color:Red"><%#Eval("jsryNo") %></span>个）</a></td>
                                                                                 <td ><a href="Index_Zyry.aspx?zyry=qyjjry&rylx=jjry">经济人员（<span style="color:Red"><%#Eval("jjryNo") %></span>个）</a></td>
                                                                                </tr>
                                                                                </table>
                                                                                
                                                                                </td>
                                                                              
                                                                                </tr>
                                                                                </table>
                                                                             
                                                                            </ItemTemplate>
                                                                            <ItemStyle Height="24px" />
                                                                        </asp:DataList>
                                                                        </marquee>
                                                                </div>
                                                                <%--
                                                                <script>
                                                                    var speed = 65
                                                                    var dl_dbsy = document.getElementById("DataList_Jytj");
                                                                    if (dl_dbsy.children[0].rows.length >= 3) {
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
                                                                </script>--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 10px; height: 245px;">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="1%" valign="top">
                                        &nbsp;
                                    </td>
                                    <td width="49.5%" valign="top">
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td style="width: 10px; height: 245px;">
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
                                                                            <span style="color: White;">跟踪预警</span>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 219px; border: solid 1px rgb(101,108,183); background-color: White;
                                                                padding: 5px;">
                                                                <!--工作区一：预警提醒-->
                                                                <div id="Div10" style="overflow: hidden; width: 100%; height: 219px;">
                                                                    <marquee onmousemove="this.stop()" id="Marq04" onmouseout="this.start()" scrollamount="2"
                                                                        direction="up" loop="-1" height="219px">
                                                                        <asp:DataList ID="DataList_Gzyj" runat="server" Width="100%">
                                                                            <ItemTemplate>
                                                                            [<font style=" color:Red;"><%#Eval("lx") %></font>]<a target="_blank" href='<%#Eval("url").ToString()+Eval("row_id").ToString() %>'><font style=" color:Black;">&nbsp;<%#Eval("mc") %></font></a>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Height="24px" />
                                                                        </asp:DataList></marquee>
                                                                    <%-- <div id="Div11" style="overflow: hidden">
                                                                    </div>
                                                                    <div id="Div12">
                                                                    </div>--%>
                                                                </div>
                                                                <%-- <script>
                                                                    var speed = 65
                                                                    var dl_Zlct = document.getElementById("DataList_Gzyj");
                                                                    if (dl_Zlct) {
                                                                        if (dl_Zlct.children[0].rows.length >= 4) {
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
                                                                    }
                                                                </script>--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 10px; height: 245px;">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="25">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top" style="padding-left: 30px; padding-right: 30px;">
                <table width="100%" border="0" cellspacing="0">
                    <tr>
                        <td>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="49.5%" valign="top">
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td style="width: 10px; height: 245px;">
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
                                                                            <span style="color: White;">公文通知</span>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" style="height: 219px; border: solid 1px rgb(101,108,183); background-color: White;
                                                                padding: 5px;">
                                                                <!--工作区一：公文通知-->
                                                                <div id="Div4" style="overflow: hidden; width: 100%; height: 219px;">
                                                                    <marquee onmousemove="this.stop()" id="Marq02" onmouseout="this.start()" scrollamount="2"
                                                                        direction="up" loop="-1" height="219px">
                                                                        <asp:DataList ID="DataList_Gwtz" runat="server" Width="100%">
                                                                            <ItemTemplate>
                                                                                &nbsp; &nbsp; &nbsp; <a href="http://58.211.133.50:81/MajordomoMVC/OnlineOffice/LookGwtz?infoID=<%#Eval("infoID")%>"
                                                                                    style='color: #000066; text-decoration: none;' target="_blank" title='<%#Eval("infotitle")%>'>
                                                                                    <%#Eval("xxmc")%></a>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Height="24px" />
                                                                        </asp:DataList></marquee>
                                                                    <div id="Div6">
                                                                    </div>
                                                                    --%>
                                                                </div>
                                                                <%--  <script>
                                                                    var speed = 65
                                                                    var dl_aqyh = document.getElementById("DataList_Gwtz");
                                                                    if (dl_aqyh != null) {
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
                                                                    }
                                                                </script>--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 10px; height: 245px;">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="1%" valign="top">
                                        &nbsp;
                                    </td>
                                    <td width="49.5%" valign="top">
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td style="width: 10px; height: 245px; border: 0;">
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
                                                            <td style="height: 219px; border: solid 1px rgb(101,108,183); background-color: White;
                                                                padding: 5px;">
                                                                <!--工作区一：工作指示-->
                                                                <div id="Div7" style="overflow: hidden; width: 100%; height: 219px;">
                                                                    <marquee onmousemove="this.stop()" id="Marq03" onmouseout="this.start()" scrollamount="2"
                                                                        direction="up" loop="-1" height="219px">
                                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DataList ID="DataList_gzzs" runat="server" Width="100%">
                                                                                    <ItemTemplate>
                                                                                      
                                                                                            <font style="font-family: 宋体;">[指示时间]<%#Eval("Zssj")%>
                                                                                                &nbsp; [指示主题]<a style="text-decoration: none;color:#000066;" href='javascript:void(0)' onclick='f_edit(<%#Eval("ZshfId") %>)'><%#Eval("Gzzszt") %></a> &nbsp;[指示人]：<%#Eval("ZsrName")%>
                                                                                        </font>
                                                                                        <asp:HiddenField ID="hdZshfId" runat="server" Value='<%#Eval("ZshfId") %>' />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Height="24px" />
                                                                                </asp:DataList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel></marquee>
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
                                                <td style="width: 10px; height: 245px;">
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
