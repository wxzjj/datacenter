<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage_Tjfx.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.MainPage.MainPage_Tjfx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
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
<body style=" margin:5px">
    <form id="form1" runat="server">
    <div>
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td style="background-color: rgb(101,128,183); padding: 0;">
                    <table width="100" cellpadding="0" cellspacing="0" border="0">
                        <tr style="height: 23px;">
                            <td style="width: 16px;">
                                <img src="../Common/icons/pencil.png" height="16" width="16" />
                            </td>
                            <td style="width: 80px;">
                                <span style="color: White;">简要统计</span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="border: solid 1px rgb(101,108,183); background-color: White; padding: 1px;">
                    <div id="jytj" style="overflow: hidden; width: 100%; height: 180px;">
                        <!--工作区一：简要统计-->
                        <marquee onmousemove="this.stop()" id="Marq01" onmouseout="this.start()" scrollamount="2"
                            direction="up" loop="-1" height="100%">
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
                                    <td width="25%">
                                        <a href="Index_Szgc.aspx?state=zj&xmlx=aqjd&ssdq=sxq">市辖区（<span style="color: Red"><%#Eval("sxsq1") %></span>个）</a>
                                    </td>
                                    <td width="25%">
                                        <a href="Index_Szgc.aspx?state=zj&xmlx=aqjd&ssdq=xsq">锡山区（<span style="color: Red"><%#Eval("xsq1") %></span>个）</a>
                                    </td>
                                    <td width="25%">
                                        <a href="Index_Szgc.aspx?state=zj&xmlx=aqjd&ssdq=hsq">惠山区（<span style="color: Red"><%#Eval("hsq1") %></span>个）</a>
                                    </td>
                                    <td width="25%">
                                        <a href="Index_Szgc.aspx?state=zj&xmlx=aqjd&ssdq=bhq">滨湖区（<span style="color: Red"><%#Eval("bhq1") %></span>个）</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <a href="Index_Szgc.aspx?state=zj&xmlx=aqjd&ssdq=xq">新区（<span style="color: Red"><%#Eval("xq1") %></span>个）</a>
                                    </td>
                                    <td>
                                        <a href="Index_Szgc.aspx?state=zj&xmlx=aqjd&ssdq=jy">江阴（<span style="color: Red"><%#Eval("jy1") %></span>个）</a>
                                    </td>
                                    <td>
                                        <a href="Index_Szgc.aspx?state=zj&xmlx=aqjd&ssdq=yx">宜兴（<span style="color: Red"><%#Eval("yx1") %></span>个）</a>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="4">
                                        <b>竣工项目：安全监督</b>（<span style="color: Red"><%#Eval("jgajxm") %></span></span>个）
                                    </td>
                                </tr>
                                <tr>
                                    <td width="25%">
                                        <a href="Index_Szgc.aspx?state=jg&xmlx=aqjd&ssdq=sxq">市辖区（<span style="color: Red"><%#Eval("sxsq2") %></span>个）</a>
                                    </td>
                                    <td width="25%">
                                        <a href="Index_Szgc.aspx?state=jg&xmlx=aqjd&ssdq=xsq">锡山区（<span style="color: Red"><%#Eval("xsq2") %></span>个）</a>
                                    </td>
                                    <td width="25%">
                                        <a href="Index_Szgc.aspx?state=jg&xmlx=aqjd&ssdq=hsq">惠山区（<span style="color: Red"><%#Eval("hsq2") %></span>个）</a>
                                    </td>
                                    <td width="25%">
                                        <a href="Index_Szgc.aspx?state=jg&xmlx=aqjd&ssdq=bhq">滨湖区（<span style="color: Red"><%#Eval("bhq2") %></span>个）</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <a href="Index_Szgc.aspx?state=jg&xmlx=aqjd&ssdq=xq">新区（<span style="color: Red"><%#Eval("xq2") %></span>个）</a>
                                    </td>
                                    <td>
                                        <a href="Index_Szgc.aspx?state=jg&xmlx=aqjd&ssdq=jy">江阴（<span style="color: Red"><%#Eval("jy2") %></span>个）</a>
                                    </td>
                                    <td>
                                        <a href="Index_Szgc.aspx?state=jg&xmlx=aqjd&ssdq=yx">宜兴（<span style="color: Red"><%#Eval("yx2") %></span>个）</a>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="4">
                                        <span style="font-size: 15px; color: Blue"><b>市场主体</b></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <table border="0" width="100%" cellpadding="0" cellspacing="2">
                                            <tr>
                                                <td width="33%">
                                                    <a href="Index_Szqy.aspx?dwlx=jsdw">建设单位（<span style="color: Red"><%#Eval("jsdwNo") %></span>个）</a>
                                                </td>
                                                <td width="33%">
                                                    <a href="Index_Szqy.aspx?dwlx=kcsj">勘察设计单位（<span style="color: Red"><%#Eval("kcsjdwNo") %></span>个）</a>
                                                </td>
                                                <td width="33%">
                                                    <a href="Index_Szqy.aspx?dwlx=sgdw">施工单位（<span style="color: Red"><%#Eval("sgdwNo") %></span>个）</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="33%">
                                                    <a href="Index_Szqy.aspx?dwlx=zjjg">中介机构（<span style="color: Red"><%#Eval("zjjgNo") %></span>个）</a>
                                                </td>
                                                <td width="33%">
                                                    <a href="Index_Szqy.aspx?dwlx=qt">其 他（<span style="color: Red"><%#Eval("qtNo") %></span>个）</a>
                                                </td>
                                                <td width="33%">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                            <tr>
                                                <td align="left" colspan="5">
                                                    <span style="font-size: 15px; color: Blue"><b>执业人员</b></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="5">
                                                    <span><b>注册（执业）人员</b></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="20%">
                                                    <a href="Index_Zyry.aspx?zyry=zczyry&rylx=jzs">建造师（<span style="color: Red"><%#Eval("jzsNo") %></span>个）</a>
                                                </td>
                                                <td width="20%">
                                                    <a href="Index_Zyry.aspx?zyry=zczyry&rylx=jls">监理师（<span style="color: Red"><%#Eval("jlsNo") %></span>个）</a>
                                                </td>
                                                <td width="20%">
                                                    <a href="Index_Zyry.aspx?zyry=zczyry&rylx=zjs">造价师（<span style="color: Red"><%#Eval("zjsNo") %></span>个）</a>
                                                </td>
                                                <td width="20%">
                                                    <a href="Index_Zyry.aspx?zyry=zczyry&rylx=jzhus">建筑师（<span style="color: Red"><%#Eval("jzhusNo") %></span>个）</a>
                                                </td>
                                                <td width="20%">
                                                    <a href="Index_Zyry.aspx?zyry=zczyry&rylx=jgs">结构师（<span style="color: Red"><%#Eval("jgsNo") %></span>个）</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <span><b>安全生产管理人员</b></span>
                                                </td>
                                                <td colspan="2">
                                                    <span><b>技经人员</b></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <a href="Index_Zyry.aspx?zyry=aqscglry&rylx=al">A类（<span style="color: Red"><%#Eval("alNo") %></span>个）</a>
                                                </td>
                                                <td>
                                                    <a href="Index_Zyry.aspx?zyry=aqscglry&rylx=bl">B类（<span style="color: Red"><%#Eval("blNo") %></span>个）</a>
                                                </td>
                                                <td>
                                                    <a href="Index_Zyry.aspx?zyry=aqscglry&rylx=cl">C类（<span style="color: Red"><%#Eval("clNo") %></span>个）</a>
                                                </td>
                                                <td>
                                                    <a href="Index_Zyry.aspx?zyry=qyjjry&rylx=jsry">技术人员（<span style="color: Red"><%#Eval("jsryNo") %></span>个）</a>
                                                </td>
                                                <td>
                                                    <a href="Index_Zyry.aspx?zyry=qyjjry&rylx=jjry">经济人员（<span style="color: Red"><%#Eval("jjryNo") %></span>个）</a>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            </ItemTemplate>
                            <itemstyle height="24px" />
                            </asp:DataList></marquee>
                    </div>
                </td>
            </tr>
        </table>
        
    </div>
    </form>
</body>
</html>
