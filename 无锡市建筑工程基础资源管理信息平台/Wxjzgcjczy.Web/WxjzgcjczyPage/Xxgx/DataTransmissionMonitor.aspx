<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataTransmissionMonitor.aspx.cs"
    Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Xxgx.DataTransmissionMonitor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link href="../Common/css/IndexStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../LigerUI/ligerUI/skins/Aqua/css/ligerui-dialog.css" rel="stylesheet"
        type="text/css" />
    <link href="../../SparkClient/DateTime_ligerui.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/MunSupervisionProject_Theme/Style.css" rel="stylesheet"
        type="text/css" />

    <script src="../../LigerUI/jquery/jquery-1.5.2.min.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/ligerui.min.js" type="text/javascript"></script>

    <script src="../../SparkClient/DateTime_ligerui.js" type="text/javascript"></script>

    <script src="../../SparkClient/control_ligerui.js" type="text/javascript"></script>

    <script src="../../SparkClient/jquery.validate.min.js" type="text/javascript"></script>

    <script src="../../LigerUI/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>

    <script src="../../LigerUI/js/common.js" type="text/javascript"></script>

    <script src="../../LigerUI/js/LG.js" type="text/javascript"></script>

    <script src="../Common/scripts/frame.js" type="text/javascript"></script>
    
    <style type="text/css">
        .img_center
        {
            text-align: center;
            padding: 0;
        }
        .up_green,.up_green,.up_red,.down_green,.down_red
        {
        	text-align: center;
            padding: 0;
        }
        .up_green img
        {
        	background-image:url('../Common/images/xtjk/up_green.png') no-repeat;
        }
        .up_red img
        {
        	background-image:url('../Common/images/xtjk/up_red.png') no-repeat;
        }
        .down_green
        {
        }
        .down_green img
        {
        	src:url('../Common/images/xtjk/down_green.png') no-repeat;
        
        }
        .down_red img
        {
        	background-image:url('../Common/images/xtjk/down_red.png') no-repeat;
        }
    </style>
    
    <script type="text/javascript">
        $(function() {

            var down_green = '../Common/images/xtjk/down_green.png';
            var down_red = '../Common/images/xtjk/down_red.png';
            var up_green = '../Common/images/xtjk/up_green.png';
            var up_red = '../Common/images/xtjk/up_red.png';




            //            var img_down_green = "<img src='" + down_green + "'/>";
            //            var img_down_red = "<img src='" + down_red + "'/>";
            //            var img_up_green = "<img src='" + up_green + "'/>";
            //            var img_up_red = "<img src='" + up_red + "'/>";

            //            $("td", "#tr1").html("<img src='../Common/images/xtjk/down_green.png'/>");
            //            $("td", "#tr2").html("<img src='../Common/images/xtjk/up_green.png'/>");
            //            $("td", "#tr3").html("<img src='../Common/images/xtjk/down_red.png'/>");
            $.ajax({
                type: 'post', cache: false, dataType: 'text', async: false,
                url: '../Handler/Data.ashx?type=xxgx_csjk',
                data: [
                //                               { name: 'ssdq', value: ssdq },
                //                              { name: 'xmdjrq', value: xmdjrq },
                //                              { name: 'xmmc', value: xmmc }
                              ],
                success: function(result) {

                    if (result) {

                        var arr = eval("(" + result + ")");

                        for (var i = 0; i < arr.length; i++) {
                            var json = arr[i];

                            if (json.enable == "1") {

                                if ($("img[name='" + json.value + "']").attr("direction") == "up") {
                                    $("img[name='" + json.value + "']").attr("src", up_green);
                                    $("img[name='" + json.value + "']").attr("enable", "1");
                                }
                                else {
                                    $("img[name='" + json.value + "']").attr("src", down_green);
                                    $("img[name='" + json.value + "']").attr("enable", "1");

                                }
                            }
                            else {
                                if ($("img[name='" + json.value + "']").attr("direction") == "up") {
                                    $("img[name='" + json.value + "']").attr("src", up_red);
                                    $("img[name='" + json.value + "']").attr("enable", "0");
                                }
                                else {
                                    $("img[name='" + json.value + "']").attr("src", down_red);
                                    $("img[name='" + json.value + "']").attr("enable", "0");
                                }
                            }
                        }
                    }

                    $("img[direction='up']").each(function() {

                        if ($(this).attr("src") == "") {
                            $(this).attr("src", up_red);
                        }
                    });

                    $("img[direction='down']").each(function() {

                        if ($(this).attr("src") == "") {
                            $(this).attr("src", down_red);
                        }
                    });
                },
                error: function() {
                    //alert("error");
                    //$.ligerDialog.error('error!');
                }
            });


            $("img[enable='1']").live("click", function() {

                
            });

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 1150px; border: 0;">
            <tr>
                <td style="text-align: center;">
                    <div style="width: 930px; float: none; margin: auto;">
                        <table style="width: 100%; height: 100%; border: 0;">
                            <tr>
                                <td>
                                    <img src="../Common/images/xtjk/jyht.png" />
                                </td>
                                <td>
                                    <img src="../Common/images/xtjk/sythpt.png" />
                                </td>
                                  <td>
                                    <img src="../Common/images/xtjk/sstxt.png" />
                                </td>
                              
                                <td>
                                    <img src="../Common/images/xtjk/ssgxk.png" />
                                </td>
                                <td>
                                    <img src="../Common/images/xtjk/szjxt.png" />
                                </td>
                                <td>
                                    <img src="../Common/images/xtjk/sjgba.png" />
                                </td> 
                                <td>
                                    <img src="../Common/images/xtjk/jsjsggjcsjpt.png" />
                                </td>
                            </tr>
                            <tr id="tr1">
                                <td>
                                   <img name="5" direction="down"  src=""/>
                                </td>
                                  <td>
                                   <img name="1"  direction="down"  src=""/>
                                </td>
                                  <td>
                                   <img name="0"  direction="down"  src=""/>
                                </td>
                                <td>
                                    <img name="2"  direction="down"  src=""/>
                                </td>
                                <td>
                                    <img name="3"  direction="down"  src=""/>
                                </td>
                                <td>
                                    <img name="4"  direction="down"  src=""/>
                                </td>
                                 <td>
                                    <img name="26"  direction="down"  src=""/>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: center;">
                    <img src="../Common/images/xtjk/jczypt.png" />
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%; height: 100%; border: 0; padding: 0;">
                        <tr id="tr2">
                         
                            <td class="img_center">
                                 <img name="6" direction="up" src='' />
                            </td>
                            <td class="img_center">
                                  <img name="7" direction="up"  src="" />
                            </td>
                            <td class="img_center">
                                   <img name="8" direction="up"  src="" />
                            </td>
                            <td class="img_center">
                               <img name="9" direction="up"  src="" />
                            </td>
                            <td class="img_center">
                                 <img name="10" direction="up"  src="" />
                            </td>
                            <td class="img_center">
                                 <img name="11" direction="up"  src="" />
                            </td>
                            <td class="img_center">
                                 <img name="12" direction="up"  src="" />
                            </td>
                            <td class="img_center">
                                   <img name="13" direction="up"  src="" />
                            </td>
                            <td class="img_center">
                                   <img name="14" direction="up"  src="" />
                            </td>
                            <td class="img_center">
                                  <img name="15" direction="up" src="" />
                            </td>
                        </tr>
                        <tr id="tr3">
                         
                            <td class="img_center">
                                 <img name="16" direction="down"  src="" />
                            </td>
                            <td class="img_center">
                                  <img name="17"  direction="down" src="" />
                            </td>
                            <td class="img_center">
                                   <img name="18"  direction="down" src="" />
                            </td>
                            <td class="img_center">
                               <img name="19"  direction="down" src="" />
                            </td>
                            <td class="img_center">
                                 <img name="20" direction="down"  src="" />
                            </td>
                            <td class="img_center">
                                 <img name="21" direction="down"  src="" />
                            </td>
                            <td class="img_center">
                                 <img name="22"  direction="down" src="" />
                            </td>
                            <td class="img_center">
                                   <img name="23" direction="down"  src="" />
                            </td>
                            <td class="img_center">
                                   <img name="24" direction="down"  src="" />
                            </td>
                            <td class="img_center">
                                  <img name="25" direction="down"  src="" />
                            </td>
                        </tr>
                        <tr>
                           
                            <td>
                                <img src="../Common/images/xtjk/skcsj.png" />
                            </td>
                            <td>
                                <img src="../Common/images/xtjk/sztb.png" />
                            </td>
                            <td>
                                <img src="../Common/images/xtjk/jysztb.png" />
                            </td>
                            <td>
                                <img src="../Common/images/xtjk/yxsztb.png" />
                            </td>
                            <td>
                                <img src="../Common/images/xtjk/saj.png" />
                            </td>
                            <td>
                                <img src="../Common/images/xtjk/szj.png" />
                            </td>
                            <td>
                                <img src="../Common/images/xtjk/xykp.png" />
                            </td>
                            <td>
                                <img src="../Common/images/xtjk/hsq.png" />
                            </td>
                            <td>
                                <img src="../Common/images/xtjk/bhq.png" />
                            </td>
                            <td>
                                <img src="../Common/images/xtjk/szxspt.png" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
