<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LocationBz.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Tdt.LocationBz" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script src="http://api.tianditu.gov.cn/api?v=3.0&tk=2c07f1d908e4a411586d2c66ba11e0a3" type="text/javascript"></script>

    <script type="text/javascript" src="http://api.tianditu.com/js/service.js"></script>

    <script src="../../SparkClient/jquery-1.3.2.min.js" type="text/javascript"></script>

    <style type="text/css">
        .searchBox
        {
            position: absolute;
            right: 10px;
            top: 40px;
            z-index: 1000px;
            background-color: White;
            width: 320px;
            padding: 8px;
            border: 1px solid #999999;
        }
        .search
        {
            font-size: 13px;
            border: 1px solid #999999;
            height: 35px;
            vertical-align: middle;
            line-height: 35px;
        }
        .ls
        {
            line-height: 27px;
            padding-left: 7px;
        }
        .prompt
        {
            display: none;
            font-size: 13px;
            border: 1px solid #999999;
        }
        .statistics
        {
            display: none;
            font-size: 13px;
            border: 1px solid #999999;
            overflow-y: scroll;
            height: 150px;
        }
        .suggests
        {
            display: none;
            font-size: 13px;
            border: 1px solid #999999;
        }
        .lineData
        {
            display: none;
            font-size: 13px;
            border: 1px solid #999999;
        }
        .result
        {
            display: none;
            font-size: 12px;
            border: 1px solid #999999;
            line-height: 27px;
            padding-left: 7px;
        }
    </style>
</head>
<body style="margin: 0; padding: 0;">

    <script type="text/javascript">
        var map;
        var zoom = 11;
        var mapclick = null;
        var theMarker = null;
        var searchLinesOverLays = [];
        var searchPointsOverLays = [];
        var localsearch;
        function loadMap() {
            //初始化地图对象 
            map = new TMap("map");
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
                size: new TSize(160, 110), 		//鹰眼显示的大小 
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
            mapTyleControl.setRight(5);
            mapTyleControl.setTop(5);
            map.addControl(mapTyleControl);

            //创建比例尺控件对象 
            var scale = new TScaleControl();
            //添加比例尺控件 
            map.addControl(scale);

            var x = $("[id$='gis_jd']").val();
            var y = $("[id$='gis_wd']").val();

            if (x != "" && y != "") {

                var lnglat = new TLngLat(parseFloat(x), parseFloat(y));
                theMarker = addTheOverLay(lnglat);
            }
          
            var config = {

                pageCapacity: 8,    //每页显示的数量 

                onSearchComplete: localSearchResult  //接收数据的回调函数 

            };

            //创建搜索对象
            localsearch = new TLocalSearch(map, config);
            localsearch.search("江苏省无锡市");

            addMapClick();
        }
        
        function addMapClick()
        {
            if(mapclick==null)
            {
                //注册地图的点击事件
                mapclick = TEvent.addListener(map, "click", function(lnglat, btn) {

                    if (btn == 2 || btn == "2") {
                        return;
                    }

                    var prjNum = $("[id$='db_PKID']").val();
                    if (prjNum == undefined || prjNum == "") {
                        //alert("传入参数非法！");
                        return;
                    }

                    //将像素坐标转换成经纬度坐标
                    var lnglat = map.fromContainerPixelToLngLat(lnglat);

                    mapClickHandler(lnglat);

                });
            }
        }
        
        function removeMapClick() {

            if (mapclick)
                TEvent.removeListener(mapclick);

            mapclick = null;        
        }

        function closeSearch() {
        
            $("#keyWord").val("");
            clearAll();
            addMapClick();
        }  


        function addTheOverLay(lnglat) {

            //创建标注对象
            // var lnglat = new TLngLat(parseFloat(x), parseFloat(y))
            var marker = new TMarker(lnglat);
            map.addOverLay(marker);
            //marker.setIconImage(url:String,size:TSize,anchor:Array);
            marker.setIconImage('../Common/icons/marker2.png', new TSize(50, 50));
            //marker.enableDragging();
//            //注册标注的鼠标开始拖动标触发事件
//            TEvent.addListener(marker, "dragstart", function(lnglat) {

//                var x = lnglat.getLng();
//                var y = lnglat.getLat();

//                $("#x").val(x);
//                $("#y").val(y);
//            }); 
//          

//            //注册标注的鼠标拖动标注完成之后触发事件
//            TEvent.addListener(marker, "dragend", function(lnglat) {

//                AdjustLocation(lnglat);
//             

//            });

            //向地图上添加标注
            return marker;

        }


        function OKClick(obj) {

            var PKID = $("[id$='db_PKID']").val();

            var jd = $("[id$='gis_jd']").val();
            var wd = $("[id$='gis_wd']").val();
            var x = $("#x").val();
            var y = $("#y").val();
            var isTrue = false;

            if (jd != "" && wd != "") {
                if (confirm("该项目已经标注，确定要更新标注吗？"))
                    isTrue = true;
                else
                    isTrue = false;
            }
            else 
            {
                isTrue = true;
            }

            if (isTrue) 
            {
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

                                if (obj)
                                    $(obj).remove();

                                $("[id$='gis_jd']").val(x);
                                $("[id$='gis_wd']").val(y);

                                var lnglat = new TLngLat(parseFloat(x), parseFloat(y));
                                if (theMarker) {

                                    theMarker.setLngLat(lnglat); //设置标注的坐标
                                }
                                else {
                                    theMarker = addTheOverLay(lnglat); //新增一个标注
                                }

                                if (searchPointsOverLays.length > 0) {

                                    for (var i = 0; i < searchPointsOverLays.length; i++) {
                                        map.removeOverLay(searchPointsOverLays[i]);
                                    }
                                }
                                searchPointsOverLays = [];

                                clearAll();

                            }
                            else {
                                alert("标注失败！");
                            }
                        }
                    },
                    error: function() {
                        //alert("Ajax出现错误,请刷新页面后重试！");
                    }
                });

            }
        }

        function mapClickHandler(lnglat) {

            var PKID = $("[id$='db_PKID']").val();

            var jd = $("[id$='gis_jd']").val();
            var wd = $("[id$='gis_wd']").val();

            var x = lnglat.getLng();
            var y = lnglat.getLat();

            //创建标注对象
            var marker = new TMarker(lnglat);
            //地图上添加标注点
            map.addOverLay(marker);

            var isTrue = false;
            if (jd != "" && wd != "") {

                if (jd == x && wd == y) {

                    return;
                }

                if (confirm("该项目已经标注，确定要更新标注吗？")) {
                    isTrue = true;

                }
                else {
                    isTrue = false;
                }
            }
            else {
                if (confirm("确定要添加标注吗？")) {
                    isTrue = true;
                }
                else {
                    isTrue = false;
                }
            }

            map.removeOverLay(marker);

            if (isTrue) {

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

                                var lnglat = new TLngLat(parseFloat(x), parseFloat(y));
                                if (theMarker) {

                                    theMarker.setLngLat(lnglat); //设置标注的坐标
                                }
                                else {
                                    theMarker = addTheOverLay(lnglat); //新增一个标注
                                }

                                if (searchPointsOverLays.length > 0) {

                                    for (var i = 0; i < searchPointsOverLays.length; i++) {
                                        map.removeOverLay(searchPointsOverLays[i]);
                                    }
                                }
                                searchPointsOverLays = [];

                                clearAll();
                            }
                            else {
                                alert("标注失败！");
                            }
                        }
                    },
                    error: function() {
                        //alert("Ajax出现错误,请刷新页面后重试！");

                    }
                });
            }
        }


        function AdjustLocation(lnglat) {

            var PKID = $("[id$='db_PKID']").val();

            var x = lnglat.getLng();
            var y = lnglat.getLat();

            var x_begin = $("#x").val();
            var y_begin = $("#y").val();
            if (x == x_begin && y == y_begin) {
                return true ;
            }

            if (!confirm("您拖动了标注，确定要更新该标注位置吗？")) {

                var gis_jd = $("[id$='gis_jd']").val();
                var gis_wd = $("[id$='gis_wd']").val();
                var lnglat = new TLngLat(parseFloat(gis_jd), parseFloat(gis_wd));
                if (theMarker) {

                    theMarker.setLngLat(lnglat); //设置标注的坐标
                }
                return true;
            }

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

                            if (obj)
                                $(obj).remove();

                            $("[id$='gis_jd']").val(x);
                            $("[id$='gis_wd']").val(y);

                            var lnglat = new TLngLat(parseFloat(x), parseFloat(y));
                            if (theMarker) {

                                theMarker.setLngLat(lnglat); //设置标注的坐标
                            }
                            else {

                                theMarker = addTheOverLay(lnglat); //新增一个标注

                            }

                            if (searchPointsOverLays.length > 0) {

                                for (var i = 0; i < searchPointsOverLays.length; i++) {
                                    map.removeOverLay(searchPointsOverLays[i]);
                                }
                            }
                            searchPointsOverLays = [];
                            clearAll();

                        }
                        else {

                            alert("标注失败！");


                        }

                    }

                },
                error: function() {
                    //alert("Ajax出现错误,请刷新页面后重试！");

                }
            });

        }


        function localSearchResult(result) {
            //清空地图及搜索列表 
            clearAll();

            //添加提示词 
            //prompt(result);

            //根据返回类型解析搜索结果 
            switch (parseInt(result.getResultType())) {
                case 1:
                    //解析点数据结果 
                    pois(result.getPois());
                    break;
                case 2:
                    //解析推荐城市 
                    statistics(result.getStatistics());
                    break;
                case 3:
                    //解析行政区划边界 
                    area(result.getArea());
                    break;
                case 4:
                    //解析建议词信息 
                    suggests(result.getSuggests());
                    break;
                //                case 5:      
                //                    //解析公交信息       
                //                    lineData(result.getLineData());      
                //                    break;    
            }

        }

        //解析提示词 
        function prompt(obj) {
            var prompts = obj.getPrompt();
            if (prompts) {
                var promptHtml = "";
                for (var i = 0; i < prompts.length; i++) {
                    var prompt = prompts[i];
                    var promptType = prompt.type;
                    var promptAdmins = prompt.admins;
                    var meanprompt = prompt.DidYouMean;
                    if (promptType == 1) {
                        promptHtml += "<p>您是否要在" + promptAdmins[0].name + "</strong>搜索更多包含<strong>" + obj.getKeyword() + "</strong>的相关内容？<p>";
                    }
                    else if (promptType == 2) {
                        promptHtml += "<p>在<strong>" + promptAdmins[0].name + "</strong>没有搜索到与<strong>" + obj.getKeyword() + "</strong>相关的结果。<p>";
                        if (meanprompt) {
                            promptHtml += "<p>您是否要找：<font weight='bold' color='#035fbe'><strong>" + meanprompt + "</strong></font><p>";
                        }
                    }
                    else if (promptType == 3) {
                        promptHtml += "<p style='margin-bottom:3px;'>有以下相关结果，您是否要找：</p>"
                        for (i = 0; i < promptAdmins.length; i++) {
                            promptHtml += "<p>" + promptAdmins[i].name + "</p>";
                        }
                    }
                }
                if (promptHtml != "") {
                    document.getElementById("promptDiv").style.display = "block";
                    document.getElementById("promptDiv").innerHTML = promptHtml;
                }
            }
        }

        //解析点数据结果 
        function pois(obj) {
            if (obj) {
                //显示搜索列表 
                var divMarker = document.createElement("div");
                //坐标数组，设置最佳比例尺时会用到
                var zoomArr = [];
                for (var i = 0; i < obj.length; i++) {
                    //闭包 
                    (function(i) {
                        //名称 
                        var name = obj[i].name;
                        //地址 
                        var address = obj[i].address;
                        //坐标 
                        var lnglatArr = obj[i].lonlat.split(" ");
                        var lnglat = new TLngLat(lnglatArr[0], lnglatArr[1]);

                        var winHtml = "地址:" + address;
                        winHtml += "<br/><input type='button' name='btnOK' name='btnOK' value='确定' onclick='OKClick(this);' style='margin:1px 0 1px0;' />";

                        //创建标注对象
                        var marker = new TMarker(lnglat);

                        //地图上添加标注点
                        map.addOverLay(marker);

                        searchPointsOverLays.push(marker);

                        //注册标注点的点击事件
                        TEvent.bind(marker, "click", marker, function() {

                            $("#x").val(lnglat.getLng());
                            $("#y").val(lnglat.getLat());

                            var info = this.openInfoWinHtml(winHtml);
                            info.setTitle(name);
                          
                            //TEvent.cancelBubble("onclick");

                        });
                        zoomArr.push(lnglat);

                        //在页面上显示搜索的列表 
                        var a = document.createElement("a");
                        a.href = "javascript://";
                        a.innerHTML = name;
                        a.onclick = function() {
                            showPosition(marker, name, winHtml);
                        }
                        divMarker.appendChild(document.createTextNode((i + 1) + "."));
                        divMarker.appendChild(a);
                        divMarker.appendChild(document.createElement("br"));
                    })(i);
                }
                //显示地图的最佳级别 
                map.setViewport(zoomArr);
                //显示搜索结果 
                divMarker.appendChild(document.createTextNode('共' + localsearch.getCountNumber() + '条记录，分' + localsearch.getCountPage() + '页,当前第' + localsearch.getPageIndex() + '页'));
                document.getElementById("searchDiv").appendChild(divMarker);
                document.getElementById("resultDiv").style.display = "block";

                removeMapClick();
            }
           
        }


        //显示信息框 
        function showPosition(marker, name, winHtml) {
            var info = marker.openInfoWinHtml(winHtml);
            info.setTitle(name);
        }

        //解析推荐城市 
        function statistics(obj) {
            if (obj) {
                //坐标数组，设置最佳比例尺时会用到 
                var pointsArr = [];
                var priorityCitysHtml = "";
                var allAdminsHtml = "";
                var priorityCitys = obj.priorityCitys;
                if (priorityCitys) {
                    //推荐城市显示  
                    priorityCitysHtml += "在中国以下城市有结果<ul>";
                    for (var i = 0; i < priorityCitys.length; i++) {
                        priorityCitysHtml += "<li>" + priorityCitys[i].name + "(" + priorityCitys[i].count + ")</li>";
                    }
                    priorityCitysHtml += "</ul>";
                }

                var allAdmins = obj.allAdmins;
                if (allAdmins) {
                    allAdminsHtml += "更多城市<ul>";
                    for (var i = 0; i < allAdmins.length; i++) {
                        allAdminsHtml += "<li>" + allAdmins[i].name + "(" + allAdmins[i].count + ")";
                        var childAdmins = allAdmins[i].childAdmins;
                        if (childAdmins) {
                            for (var m = 0; m < childAdmins.length; m++) {
                                allAdminsHtml += "<blockquote>" + childAdmins[m].name + "(" + childAdmins[m].count + ")</blockquote>";
                            }
                        }
                        allAdminsHtml += "</li>"
                    }
                    allAdminsHtml += "</ul>";
                }
                document.getElementById("statisticsDiv").style.display = "block";
                document.getElementById("statisticsDiv").innerHTML = priorityCitysHtml + allAdminsHtml;
            }
        }

        //解析行政区划边界
        function area(obj) {

            if (obj) {

                searchLinesOverLays = [];
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
                    searchLinesOverLays.push(regionLngLats);

                    //创建线对象 
                    var line = new TPolyline(regionLngLats, { strokeColor: "red", strokeWeight: 3, strokeOpacity: 1, strokeStyle: "dashed" });
                    //向地图上添加线
                    map.addOverLay(line);

                }
                //显示最佳比例尺 
                //map.setViewport(pointsArr);
            }
        }


        function area2() {

            if (searchLinesOverLays.length > 0) {

                for (var i = 0; i < searchLinesOverLays.length; i++) {
                    var regionLngLats = searchLinesOverLays[i];
                    //创建线对象 
                    var line = new TPolyline(regionLngLats, { strokeColor: "red", strokeWeight: 3, strokeOpacity: 1, strokeStyle: "dashed" });
                    //向地图上添加线
                    map.addOverLay(line);
                }
                //显示最佳比例尺 
                //map.setViewport(pointsArr);
            }
        }

        //解析建议词信息 
        function suggests(obj) {
            if (obj) {
                //建议词提示，如果搜索类型为公交规划建议词或公交站搜索时，返回结果为公交信息的建议词。 
                var suggestsHtml = "建议词提示<ul>";
                for (var i = 0; i < obj.length; i++) {
                    suggestsHtml += "<li>" + obj[i].name + "&nbsp;&nbsp;<font style='color:#666666'>" + obj[i].address + "</font></li>";
                }
                suggestsHtml += "</ul>";
                document.getElementById("suggestsDiv").style.display = "block";
                document.getElementById("suggestsDiv").innerHTML = suggestsHtml;
            }
        }

        //        //解析公交信息 
        //        function lineData(obj) {
        //            if (obj) {
        //                //公交提示 
        //                var lineDataHtml = "公交提示<ul>";
        //                for (var i = 0; i < obj.length; i++) {
        //                    lineDataHtml += "<li>" + obj[i].name + "&nbsp;&nbsp;<font style='color:#666666'>共" + obj[i].stationNum + "站</font></li>";
        //                }
        //                lineDataHtml += "</ul>";
        //                document.getElementById("lineDataDiv").style.display = "block";
        //                document.getElementById("lineDataDiv").innerHTML = lineDataHtml;
        //            }
        //        }

        //清空地图及搜索列表
        function clearAll() {

            if (searchPointsOverLays.length > 0) {

                for (var i = 0; i < searchPointsOverLays.length; i++) {
                    //关闭消息框
                    searchPointsOverLays[i].closeInfoWindow();
                    //删除注册的所有事件
                    TEvent.deposeNode(searchPointsOverLays[i]);
                    //从地图上清除标注
                    map.removeOverLay(searchPointsOverLays[i]);
                }
            }
            searchPointsOverLays = [];

            document.getElementById("searchDiv").innerHTML = "";
            document.getElementById("resultDiv").style.display = "none";
            document.getElementById("statisticsDiv").innerHTML = "";
            document.getElementById("statisticsDiv").style.display = "none";
            document.getElementById("promptDiv").innerHTML = "";
            document.getElementById("promptDiv").style.display = "none";
            document.getElementById("suggestsDiv").innerHTML = "";
            document.getElementById("suggestsDiv").style.display = "none";
            document.getElementById("lineDataDiv").innerHTML = "";
            document.getElementById("lineDataDiv").style.display = "none";
        }



        $(function() {

            window.onload = loadMap;


        });



    </script>

    <form id="form1" runat="server">
    <div style="display: none;">
        <input type="text" id="x" />
        <input type="text" id="y" />
        <Bigdesk8:DBTextBox ID="db_PKID" runat="server"></Bigdesk8:DBTextBox>
        <Bigdesk8:DBTextBox ID="gis_jd" runat="server" ItemName="jd"></Bigdesk8:DBTextBox>
        <Bigdesk8:DBTextBox ID="gis_wd" runat="server" ItemName="wd"></Bigdesk8:DBTextBox>
    </div>
    <div id="map" style="width: 100%; height: 460px;">
    </div>
    <div class="searchBox">
        <!-- 搜索面板 -->
        <div class="search">
            搜索内容：<input type="text" id="keyWord" value="" />
            <input type="button" onclick="localsearch.search(document.getElementById('keyWord').value)"
                value="搜索" style="height: 30px;" />
            <input type="button" onclick="closeSearch();" value="关闭" style="height: 30px;" />
        </div>
        <br />
        <!-- 提示词面板 -->
        <div id="promptDiv" class="prompt">
        </div>
        <!-- 统计面板 -->
        <div id="statisticsDiv" class="statistics">
        </div>
        <!-- 建议词面板 -->
        <div id="suggestsDiv" class="suggests">
        </div>
        <!-- 公交提示面板 -->
        <div id="lineDataDiv" class="lineData">
        </div>
        <!-- 搜索结果面板 -->
        <div id="resultDiv" class="result">
            <div id="searchDiv">
            </div>
            <div id="pageDiv">
                <input type="button" value="第一页" onclick="localsearch.firstPage()" />
                <input type="button" value="上一页" onclick="localsearch.previousPage()" />
                <input type="button" value="下一页" onclick="localsearch.nextPage()" />
                <input type="button" value="最后一页" onclick="localsearch.lastPage()" />
                <br />
                转到第<input type="text" value="1" id="pageId" size="3" />页
                <input type="button" onclick="localsearch.gotoPage(parseInt(document.getElementById('pageId').value));"
                    value="转到" />
            </div>
        </div>
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
