<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LocationBz2.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Tdt.LocationBz2" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
       <script language="javascript" src="http://api.tianditu.com/js/maps.js" type="text/javascript"></script>


    <script src="../../SparkClient/jquery-1.3.2.min.js" type="text/javascript"></script>

    <style type="text/css">
        .sureCss
        {
            padding: 0;
            margin: 0;
        }
    </style>

    
</head>
<body style=" margin:0; padding:0;">
<script type="text/javascript">
        var map;
        var zoom = 11;
        var mapclick;
        var marker;
        var label;
        function loadMap() {
            //初始化地图对象 
            map = new TMap("map");
            //设置显示地图的中心点和级别 
            map.centerAndZoom(new TLngLat(120.31554, 31.49201), zoom);
            //允许鼠标滚轮缩放地图 
            //map.enableHandleMouseScroll();


            var config = {
                type: "TMAP_NAVIGATION_CONTROL_LARGE", //缩放平移的显示类型 
                anchor: "TMAP_ANCHOR_TOP_LEFT", 		//缩放平移控件显示的位置 
                offset: [0, 0], 						//缩放平移控件的偏移值 
                showZoomInfo: true						//是否显示级别提示信息，true表示显示，false表示隐藏。 

            };

            var config1 = {
                anchor: "TMAP_ANCHOR_BOTTOM_RIGHT", //设置鹰眼位置,"TMAP_ANCHOR_TOP_LEFT"表示左上，"TMAP_ANCHOR_TOP_RIGHT"表示右上，"TMAP_ANCHOR_BOTTOM_LEFT"表示左下，"TMAP_ANCHOR_BOTTOM_RIGHT"表示右下，"TMAP_ANCHOR_LEFT"表示左边，"TMAP_ANCHOR_TOP"表示上边，"TMAP_ANCHOR_RIGHT"表示右边，"TMAP_ANCHOR_BOTTOM"表示下边，"TMAP_ANCHOR_OFFSET"表示自定义位置,默认值为"TMAP_ANCHOR_BOTTOM_RIGHT" 
                size: new TSize(180, 120), 		//鹰眼显示的大小 
                isOpen: true						//鹰眼是否打开，true表示打开，false表示关闭，默认为关闭 
            };
            //创建鹰眼控件对象 
            overviewMap = new TOverviewMapControl(config1);
            //添加鹰眼控件 
            map.addControl(overviewMap);

            //创建缩放平移控件对象 
            control = new TNavigationControl(config);
            //添加缩放平移控件 
            map.addControl(control);
            //允许鼠标地图惯性拖拽
            map.enableInertia();

            //创建自定义控件，并添加到地图上 
            var mapTypeStyle = document.getElementById("mapTypeStyle");
            var mapTyleControl = new THtmlElementControl(mapTypeStyle);
            mapTyleControl.setRight(10);
            mapTyleControl.setTop(10);
            map.addControl(mapTyleControl);

            //创建比例尺控件对象 
            var scale = new TScaleControl();
            //添加比例尺控件 
            map.addControl(scale);

            var x = $("[id$='gis_jd']").val();
            var y = $("[id$='gis_wd']").val();

            if (x != "" && y != "") {

                var lnglat = new TLngLat(parseFloat(x), parseFloat(y));
                marker = addOverLay(lnglat);
            }
            addMapClick();


        }







        function addOverLay(lnglat) {

            //创建标注对象
            // var lnglat = new TLngLat(parseFloat(x), parseFloat(y))
            marker = new TMarker(lnglat);
            map.addOverLay(marker);
            //marker.setIconImage(url:String,size:TSize,anchor:Array);
            marker.setIconImage('../Common/icons/marker2.png', new TSize(50, 50));
            //向地图上添加标注

            addMapClick();
        }


        function addMapClick() {
            //移除地图的点击事件 
            removeMapClick();
            //注册地图的点击事件

            //注册地图的点击事件
            mapclick = TEvent.addListener(map, "click", function(p, btn) {

                if (btn == 2 || btn == "2") {
                    return;
                }

                var prjNum = $("[id$='db_PKID']").val();
                if (prjNum == undefined || prjNum == "") {
                    alert("传入参数非法！");
                    return;
                }

                //将像素坐标转换成经纬度坐标
                var lnglat = map.fromContainerPixelToLngLat(p);

                //addOverLay(lnglat);



                var winHtml = "<div id='div1' class='sureCss'><input type='button' id='btnOK' value='确定' style='margin:3px 0 3px 3px;' /><input type='button' id='btnCancle' value='取消' style='margin:3px 3px 3px 5px;' /></div>";
                //                var info = marker.openInfoWinHtml(winHtml);
                //                info.setTitle("title");
                map.clearOverLays();
                marker = addOverLay(lnglat);

                $("#x").val(lnglat.getLng());
                $("#y").val(lnglat.getLat());

                var config = {
                    text: winHtml,
                    offset: new TPixel(5, 10),
                    position: lnglat
                };
                //创建地图文本对象 
                label = new TLabel(config);
                map.addOverLay(label);
                removeMapClick();

            });

        }

        function removeMapClick() {

            //移除地图的点击事件
            if (mapclick)
                TEvent.removeListener(mapclick);
        }




        $(function() {

            window.onload = loadMap;

            $("#btnOK").live("click", function() {

                var PKID = $("[id$='db_PKID']").val();
                var x = $("#x").val();
                var y = $("#y").val();
                $.ajax({
                    type: 'post',
                    cache: false,
                    dataType: 'json',
                    url: '../Handler/Data.ashx?type=SetLxxmGIS',
                    data: {
                        PKID: PKID,
                        x: x,
                        y: y
                    },
                    beforeSend: function(XMLHttpRequest) {

                    },
                    complete: function(XMLHttpRequest, textStatus) {

                    },
                    success: function(result) {
                        if (result) {
                            var json = result;

                            if (json.isSuccess) {

                                $("[id$='gis_jd']").val(x);
                                $("[id$='gis_wd']").val(y);
                                if (label)
                                    map.removeOverLay(label);
                            }
                            else {
                                map.clearOverLays();
                                x = $("[id$='gis_jd']").val();
                                y = $("[id$='gis_wd']").val();
                                if (x != "" && y != "") {

                                    var lnglat = new TLngLat(parseFloat(x), parseFloat(y))

                                    marker = addOverLay(lnglat);

                                }
                                else {
                                    marker = null;
                                }

                            }

                        }
                        else {

                            map.clearOverLays();
                            x = $("[id$='gis_jd']").val();
                            y = $("[id$='gis_wd']").val();
                            if (x != "" && y != "") {

                                var lnglat = new TLngLat(parseFloat(x), parseFloat(y))

                                marker = addOverLay(lnglat);

                            }
                            else {

                                marker = null;
                            }

                            alert("保存坐标信息失败！");
                        }

                    },
                    error: function() {
                        alert("Ajax出现错误,请刷新页面后重试！");
                        map.clearOverLays();
                        x = $("[id$='gis_jd']").val();
                        y = $("[id$='gis_wd']").val();
                        if (x != "" && y != "") {

                            var lnglat = new TLngLat(parseFloat(x), parseFloat(y))

                            marker = addOverLay(lnglat);

                        }
                        else {

                            marker = null;
                        }

                    }
                });

                addMapClick();

            });



            $("#btnCancle").live("click", function() {
                map.clearOverLays();
                x = $("[id$='gis_jd']").val();
                y = $("[id$='gis_wd']").val();
                if (x != "" && y != "") {

                    var lnglat = new TLngLat(parseFloat(x), parseFloat(y))

                    marker = addOverLay(lnglat);

                }
                else {
                    marker = null;
                }
                addMapClick();
            });





        });
    </script>

    <form id="form1" runat="server">

        <div style="display: none;">
            <input type="text" id="x" />
            <input type="text" id="y" />
            <Bigdesk8:DBTextBox ID="db_PKID" runat="server" ></Bigdesk8:DBTextBox>
            <Bigdesk8:DBTextBox ID="gis_jd" runat="server" ItemName="jd" ></Bigdesk8:DBTextBox>
            <Bigdesk8:DBTextBox ID="gis_wd" runat="server" ItemName="wd" ></Bigdesk8:DBTextBox>
            
        </div>
        <div id="map" style="width: 100%; height: 460px;">
        </div>
        <div id="mapTypeStyle" style="position: absolute; left: 90%; top: 50px;">
            <select id="mapTypeSelect" onchange="switchingMapType(this);">
                <option value="TMAP_NORMAL_MAP">地图</option>
                <option value="TMAP_SATELLITE_MAP">卫星</option>
                <option value="TMAP_HYBRID_MAP">卫星混合</option>
                <option value="TMAP_TERRAIN_MAP">地形</option>
                <option value="TMAP_TERRAIN_HYBRID_MAP">地形混合</option>
            </select>
        </div>
    </form>
</body>
</html>

