<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TdtMap.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.MainPage.TdtMap" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head runat="server">
    <meta name="keywords" content="天地图" />
    <title>天地图－地图API－范例－添加标注</title>

    <script src="http://api.tianditu.gov.cn/api?v=3.0&tk=2c07f1d908e4a411586d2c66ba11e0a3" type="text/javascript"></script>

    <script type="text/javascript" src="http://api.tianditu.com/js/service.js"></script>
    <script src="../../LigerUI/jquery/jquery-1.5.2.min.js" type="text/javascript"></script>

    <script src="../../MyDatePicker/WdatePicker.js" type="text/javascript"></script>

    <script src="../Common/scripts/Common.js" type="text/javascript"></script>
    
    <style type="text/css">
      .info_title
      {
      }
      .info_label
      {
       text-decoration:underline;
      }
    </style>
    

    

</head>
<body  style="margin: 0; padding: 0;">
    <form id="form1" runat="server">
    <div id="searchbox" style="border: 0; vertical-align: middle; font-size: 14px; font-family: 宋体;
        height: 20px; margin: 0; padding: 1px; text-align:left;">
        在建工程项目位置查询:<select id="ssdq" name="ssdq" style="width: 125px; font-size: 14px; font-family: 宋体;">
        </select>&nbsp;项目登记日期:
        <input name="xmdjrq" type="text" value="" style=" font-size: 14px;
            font-family: 宋体; margin-left:0; width:90px;" />
        <input name="xmmc" type="text" value="项目名称" style="color: Gray; font-size: 14px;
            font-family: 宋体;" />&nbsp;
        <input type="button" id="search" value="查询" style="width: 40px; font-size: 14px;
            font-family: 宋体; text-align:center; padding:0;" />
        <input type="button" id="Button1" value="更多" style="float: right; width: 35px; font-size: 14px;
            font-family: 宋体; text-align:center; padding:0; margin-top:2px;" />
    </div>
    <div id="mapDiv" style="position: absolute; width: 100%; height: 397px; margin-top: 3px;"
        align="left">
    </div>
    <div id="mapTypeStyle" style="position: absolute; left: 90%; top: 50px; ">
        <select id="mapTypeSelect" onchange="switchingMapType(this);">
            <option value="TMAP_NORMAL_MAP">地图</option>
            <option value="TMAP_SATELLITE_MAP">卫星</option>
            <option value="TMAP_HYBRID_MAP">卫星混合</option>
            <option value="TMAP_TERRAIN_MAP">地形</option>
            <option value="TMAP_TERRAIN_HYBRID_MAP">地形混合</option>
        </select>
    </div>
    <script language="javascript" type="text/javascript">




        var map;
        var zoom = 11;
        function onLoad() {

            var height = getParamValue("height", false);
            $("#mapDiv").height(height - $("#searchbox").height() - 23);
            //初始化地图对象 
            map = new TMap("mapDiv");
            //设置显示地图的中心点和级别 
            map.centerAndZoom(new TLngLat(120.31554, 31.49201), zoom);
            //允许鼠标滚轮缩放地图 
            map.enableHandleMouseScroll();


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


            //创建右键菜单对象 
            var menu = new TContextMenu();
            var txtMenuItem = [
			{
			    text: '放大',
			    callback: function() { map.zoomIn() }
			},
			{
			    text: '缩小',
			    callback: function() { map.zoomOut() }
			},
			{
			    text: '放置到最大级',
			    callback: function() { map.setZoom(18) }
			},
			{
			    text: '查看全国',
			    callback: function() { map.setZoom(4) }
			},
			{
			    text: '获得右键点击处坐标',
			    isDisable: false,
			    callback: function(lnglat) {
			        alert(lnglat.getLng() + "," + lnglat.getLat());
			    }
			},
			{
			    text: '获取当前地图中心点坐标',
			    isDisable: false,
			    callback: function getMapCenter() {
			        alert("当前地图中心点：" + map.getCenter().getLng() + "," + map.getCenter().getLat());
			    }
			},
			{
			    text: '获取当前地图缩放级别',
			    isDisable: false,
			    callback: function getMapZoom() {
			        alert("当前地图缩放级别：" + map.getZoom());
			    }
			},
			{
			    text: '获取地图当前可视范围坐标',
			    isDisable: false,
			    callback: function getMapBounds() {
			        var bs = map.getBounds();       //获取可视区域
			        var bssw = bs.getSouthWest();   //可视区域左下角
			        var bsne = bs.getNorthEast();   //可视区域右上角
			        alert("当前地图可视范围是：" + bssw.getLng() + "," + bssw.getLat() + "到" + bsne.getLng() + "," + bsne.getLat());
			    }
			}
		];
            for (var i = 0; i < txtMenuItem.length; i++) {
                //创建右键菜单参数接口对象 
                var options = new TMenuItemOptions();
                //设置右键菜单的宽度 
                options.width = 100;
                //添加菜单项 
                menu.addItem(new TMenuItem(txtMenuItem[i].text, txtMenuItem[i].callback, options));
                if (i == 1) {
                    //添加分割线 
                    menu.addSeparator();
                }
            }
            //添加右键菜单 
            map.addContextMenu(menu);
            drawShap();
            
            var zoomArr = [];
            var ssdq = "";
            var xmdjrq = "";
            var xmmc = "";

            $.ajax({
                type: 'post', cache: false, dataType: 'text', async: false,
                url: '../Handler/Data.ashx?type=xmxx',
                data: [
                   { name: 'ssdq', value: ssdq },
                  { name: 'xmdjrq', value: xmdjrq },
                  { name: 'xmmc', value: xmmc }
                  ],
                success: function(result) {

                    if (result) {

                        var markerArr =  eval("("+ result+")");

                        for (var i = 0; i < markerArr.length; i++) {

                            (function(i) {

                                var json = markerArr[i];
                                var p0 = json.point.split("|")[0];
                                var p1 = json.point.split("|")[1];
                                //创建标注对象
                                var lnglat = new TLngLat(parseFloat(p0), parseFloat(p1))
                                var marker = new TMarker(lnglat);
                                //marker.setIconImage(url:String,size:TSize,anchor:Array);
                                if (json.isSgbz == 1)
                                    marker.setIconImage('../Common/icons/marker2.png', new TSize(50, 50));
                                else
                                    if (json.isSgbz == 2)//从省施工许可系统获取
                                        marker.setIconImage('../Common/icons/marker.png', new TSize(50, 50));
                                    else
                                        marker.setIconImage('../Common/icons/marker.png', new TSize(50, 50));

                                //向地图上添加标注
                                map.addOverLay(marker);
                                marker.setInfoWinWidth(450);
                                // marker.setInfoWinHeight(100);
                                // var winHtml ="标注当前坐标："+marker.getLngLat().getLng()+","+marker.getLngLat().getLat();
                                var name = json.title;
                                var winHtml = "<span class=\"info_label\">项目编码：</span>" + json.prjNum
                                + "<br/><span class=\"info_label\">项目名称：</span><a target=\"_blank\" href=\"/IntegrativeShow2/SysFiles/Pages/Index_View.aspx?viewUrl=../PagesZHJG/Zhjg_Lxxmdj_Menu.aspx$LoginID=<%=this.WorkUser.UserID %>%PKID="
                                + json.PKID + "%PrjNum=" + json.prjNum + "&titleName=" + json.xmmc + "\" >"
                                + json.xmmc + "</a><br/><span class=\"info_label\">建设单位：</span>" + json.jsdw
                                + "<br/><span class=\"info_label\">施工总包单位：</span>" + json.sgdw
                                + "<br/><span class=\"info_label\">勘察单位：</span>" + json.kcdw
                                + "<br/><span class=\"info_label\">设计单位：</span>" + json.sjdw
                                + "<br/><span class=\"info_label\">监理单位：</span>" + json.jldw
                                + "<br/><span class=\"info_label\">开工日期：</span>" + json.kgrq
                                + "<br/><span class=\"info_label\">建设规模：</span>" + json.jsgm
                                + "<br/><span class=\"info_label\">项目总投资：</span>" + json.xmztz
                                + "<br/><span class=\"info_label\">是否手工标注：</span>" + (json.isSgbz == 1 ? "是" : "否");
                                
                                 TEvent.bind(
                                      marker, "click", marker, function() {
                                          var info = this.openInfoWinHtml(winHtml);
                                          info.setTitle(name);
                                      });
                                zoomArr.push(lnglat);
                            })(i);

                        }
                        //        markerclick = TEvent.bind(marker,"click",marker,function(){
                        //           alert("标注当前坐标："+marker.getLngLat().getLng()+","+marker.getLngLat().getLat());});
                        //显示地图的最佳级别
                        if (zoomArr.length > 0)
                            map.setViewport(zoomArr);

                    }
                },
                error: function() {
                    //alert("error");
                    //$.ligerDialog.error('error!');
                }
            });


            var config = {
                onSearchComplete: localSearchResult  //接收数据的回调函数 
            };
            //创建搜索对象
            localsearch = new TLocalSearch(map, config);
            localsearch.search("江苏省无锡市");

        }


        function localSearchResult(result) {

            //根据返回类型解析搜索结果 
            switch (parseInt(result.getResultType())) {
                case 3:
                    //解析行政区划边界 
                    area(result.getArea());
                    break;
            }
        }

        //解析行政区划边界
        function area(obj) {

            if (obj) {

                //坐标数组，设置最佳比例尺时会用到
                var pointsArr = [];
                var points = obj.points;
                for (var i = 0; i < points.length; i++) {
                    var regionLngLats = [];

                    var regionArr = points[i].region.split(",");
                    for (var m = 0; m < regionArr.length; m++) {
                        var lnglatArr = regionArr[m].split(" ");
                        var lnglat = new TLngLat(lnglatArr[0], lnglatArr[1]);
                        regionLngLats.push(lnglat);
                        pointsArr.push(lnglat);
                    }

                    //创建线对象 
                    var line = new TPolyline(regionLngLats, { strokeColor: "red", strokeWeight: 3, strokeOpacity: 1, strokeStyle: "dashed" });
                    //向地图上添加线
                    map.addOverLay(line);

                }
                //显示最佳比例尺 
                //map.setViewport(pointsArr);
            }
        }

        function LoadLayer() {

            map.clearOverLays();
            // map.ClearOverLay();
            //map.ClearOverLay(marker);
            //            for (var i = 0; i < markerArr.length; i++) {
            //                map.remove(marker);
            //
            //            }

            var zoomArr = [];
            var ssdq = $("select[name='ssdq'] option:selected").val();
            var xmdjrq = $("input[name='xmdjrq']").val();
            var xmmc = $("input[name='xmmc']").val();

            if (xmmc == "项目名称")
                xmmc = "";

            $.ajax({
                type: 'post', cache: false, dataType: 'text', async: false,
                url: '../Handler/Data.ashx?type=xmxx',
                data: [
                   { name: 'ssdq', value: ssdq },
                  { name: 'xmdjrq', value: xmdjrq },
                  { name: 'xmmc', value: xmmc }
                  ],
                success: function(result) {

                    if (result) {

                        var markerArr = eval("(" + result + ")");

                        for (var i = 0; i < markerArr.length; i++) {

                            (function(i) {

                                var json = markerArr[i];
                                var p0 = json.point.split("|")[0];
                                var p1 = json.point.split("|")[1];
                                //创建标注对象
                                var lnglat = new TLngLat(parseFloat(p0), parseFloat(p1))
                                var marker = new TMarker(lnglat);
                                if (json.isSgbz == 1) {
                                    marker.setIconImage('../Common/icons/marker2.png', new TSize(50, 50));
                                }
                                else {
                                    marker.setIconImage('../Common/icons/marker.png', new TSize(50, 50));
                                }
                                //向地图上添加标注
                                map.addOverLay(marker);
                                // var winHtml ="标注当前坐标："+marker.getLngLat().getLng()+","+marker.getLngLat().getLat();
                                //名称 
                                var name = json.title;
                                var winHtml = "<span class=\"info_label\">项目编码：</span>" + json.prjNum
                                + "<br/><span class=\"info_label\">项目名称：</span><a target=\"_blank\" href=\"/IntegrativeShow2/SysFiles/Pages/Index_View.aspx?viewUrl=../PagesZHJG/Zhjg_Lxxmdj_Menu.aspx$LoginID=<%=this.WorkUser.UserID %>%PKID="
                                + json.PKID + "%PrjNum=" + json.prjNum + "&titleName=" + json.xmmc + "\" >" 
                                + json.xmmc + "</a><br/><span class=\"info_label\">建设单位：</span>" + json.jsdw 
                                + "<br/><span class=\"info_label\">施工总包单位：</span>"+ json.sgdw
                                + "<br/><span class=\"info_label\">勘察单位：</span>" + json.kcdw
                                + "<br/><span class=\"info_label\">设计单位：</span>" + json.sjdw
                                + "<br/><span class=\"info_label\">监理单位：</span>" + json.jldw
                                + "<br/><span class=\"info_label\">开工日期：</span>" + json.kgrq
                                + "<br/><span class=\"info_label\">建设规模：</span>" + json.jsgm 
                                + "<br/><span class=\"info_label\">项目总投资：</span>" + json.xmztz
                                + "<br/><span class=\"info_label\">是否手工标注：</span>" + (json.isSgbz == 1 ? "是" : "否");
                                TEvent.bind(
          marker, "click", marker, function() {
              var info = this.openInfoWinHtml(winHtml);
              info.setTitle(name);
          });
                                zoomArr.push(lnglat);
                            })(i);

                        }
                        //        markerclick = TEvent.bind(marker,"click",marker,function(){
                        //           alert("标注当前坐标："+marker.getLngLat().getLng()+","+marker.getLngLat().getLat());});
                        //显示地图的最佳级别
                        if (zoomArr.length > 0)
                            map.setViewport(zoomArr);

                    }
                },
                error: function() {
                    //alert("error");
                    //$.ligerDialog.error('error!');
                }
            });
        }



        function switchingMapType(obj) {
            switch (obj.value) {
                case "TMAP_NORMAL_MAP":
                    setNormal();
                    break;
                case "TMAP_SATELLITE_MAP":
                    setSatellite();
                    break;
                case "TMAP_HYBRID_MAP":
                    setHybrid();
                    break;
                case "TMAP_TERRAIN_MAP":
                    setTerrain();
                    break;
                case "TMAP_TERRAIN_HYBRID_MAP":
                    setTerrainHybrid();
                    break;
            }
        }

        //地图 
        function setNormal() {
            map.setMapType(TMAP_NORMAL_MAP);
        }

        //卫星 
        function setSatellite() {
            map.setMapType(TMAP_SATELLITE_MAP);
        }

        //卫星混合 
        function setHybrid() {
            map.setMapType(TMAP_HYBRID_MAP);
        }

        //地形 
        function setTerrain() {
            map.setMapType(TMAP_TERRAIN_MAP);
        }

        //地形混合 
        function setTerrainHybrid() {
            map.setMapType(TMAP_TERRAIN_HYBRID_MAP);
        }

        // 缩放平移控件-标准样式
        function addNavControl() {
            map.removeControl(control);
            //获得缩放平移控件的样式
            var selectNavCss = document.getElementById("TMAP_NAVIGATION_CONTROL_LARGE");
            var index = selectNavCss.selectedIndex;
            var controlCss = selectNavCss.options[index].value;
            //获得缩放平移控件的位置
            var selectNavPosition = document.getElementById("TMAP_ANCHOR_TOP_LEFT");
            var index = selectNavPosition.selectedIndex;
            var controlPosition = selectNavPosition.options[index].value;
            //添加缩放平移控件 
            var config = {
                type: controlCss, 	//缩放平移控件的显示类型 
                anchor: controlPosition	//缩放平移控件显示的位置 
            };
            control = new TNavigationControl(config);
            map.addControl(control);
        }

        function drawShap() {

            points = [];

            points.push(new TLngLat(116.41136, 39.97569));
            points.push(new TLngLat(116.411794, 39.9068));
            points.push(new TLngLat(116.32969, 39.92940));
            points.push(new TLngLat(116.385438, 39.90610));
            //创建线对象 
            var line = new TPolyline(points, { strokeColor: "red", strokeWeight: 6, strokeOpacity: 1 });
            //向地图上添加线 
            map.addOverLay(line);

        }




        $(function() {
            window.onload = onLoad;
            
            $.ajax({
                type: 'post', cache: false, dataType: 'text',
                url: '../Handler/Data.ashx?type=ssdq',
                data: [
                             ],
                success: function(result) {
                    if (result) {

                        $("#ssdq").html(result);

                    }
                },
                error: function() {
                    $.ligerDialog.error('获取不到所属地区信息!');
                }
            });
            $("input[name='xmmc']").focus(function() {
                $(this).css("color", "black");
                if ($(this).val() == "项目名称") {
                    $(this).val("");
                }
            }).focusout(function() {
                if ($(this).val() == "") {
                    $(this).val("项目名称");
                    $(this).css("color", "Gray");
                }
            });

            //            $("input[name='xmdjrq']").focus(function() {
            //                $(this).css("color", "black");
            //                if ($(this).val() == "项目登记日期") {
            //                    $(this).val("");


            //                }
            //                  WdatePicker({ isShowClear: true, readOnly: true, });
            //            }).focusout(function() {
            //                if ($(this).val() == "") {
            //                
            //                    $(this).val("项目登记日期");
            //                    $(this).css("color", "Gray");
            //                }
            //            });

            $("#search").click(function() {
                LoadLayer();
            });

            $("input[name='xmdjrq']").click(function() {
                WdatePicker({ isShowClear: true, readOnly: true });
            });


        });
    </script>
    </form>
</body>
</html>
