<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LocationView.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Tdt.LocationView" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8"/>
    <meta name="keywords" content="天地图" />
    <title>天地图－地图API－范例－添加标注</title>
    
    <link rel="stylesheet" type="text/css" href="../Common/css/shCore.css" />

    <script src="../Common/js/scale.fix.js"></script>
    <script src="../Common/js/jquery-1.6.4.min.js"></script>
    <script type="text/javascript" src="../Common/js/ZeroClipboard.js"></script>
    <script type="text/javascript" src="../Common/js/allscript.js"></script>
    <script type="text/javascript" src="../Common/js/config.js"></script>
    
    
    <script language="javascript" src="http://api.tianditu.com/js/maps.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://api.tianditu.com/js/service.js"></script>
    
    <script type="text/javascript" src="../Common/js/showCode.js"></script>
    
    <script>
        var map, zoom = 12;

        var localsearch;

        function onLoad() {
            //初始化地图对象
            map = new TMap("mapDiv");
            //设置显示地图的中心点和级别
            map.centerAndZoom(new TLngLat(120.29709, 31.57552), zoom);

            var config0 = {
                anchor: "TMAP_ANCHOR_BOTTOM_RIGHT", //设置鹰眼位置,"TMAP_ANCHOR_TOP_LEFT"表示左上，"TMAP_ANCHOR_TOP_RIGHT"表示右上，"TMAP_ANCHOR_BOTTOM_LEFT"表示左下，"TMAP_ANCHOR_BOTTOM_RIGHT"表示右下，"TMAP_ANCHOR_LEFT"表示左边，"TMAP_ANCHOR_TOP"表示上边，"TMAP_ANCHOR_RIGHT"表示右边，"TMAP_ANCHOR_BOTTOM"表示下边，"TMAP_ANCHOR_OFFSET"表示自定义位置,默认值为"TMAP_ANCHOR_BOTTOM_RIGHT" 
                size: new TSize(180, 120), 		//鹰眼显示的大小 
                isOpen: true						//鹰眼是否打开，true表示打开，false表示关闭，默认为关闭 
            };
            //创建鹰眼控件对象 
            overviewMap = new TOverviewMapControl(config0);
            //添加鹰眼控件 
            map.addControl(overviewMap);

            var config = {
                pageCapacity: 10, //每页显示的数量 
                onSearchComplete: localSearchResult	//接收数据的回调函数 
            };
            //创建搜索对象 
            localsearch = new TLocalSearch(map, config);

        }

        function localSearchResult(result) {
            //清空地图及搜索列表 
            clearAll();

            //根据返回类型解析搜索结果 
            switch (parseInt(result.getResultType())) {
                case 1:
                    //解析点数据结果 
                    pois(result.getPois());
                    break;
                case 2:
                    //解析统计城市 
                    statistics(result.getStatistics());
                    break;
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
                    (function (i) {
                        //名称 
                        var name = obj[i].name;
                        //地址 
                        var address = obj[i].address;
                        //坐标 
                        var lnglatArr = obj[i].lonlat.split(" ");
                        var lnglat = new TLngLat(lnglatArr[0], lnglatArr[1]);

                        var winHtml = "地址:" + address;

                        //创建标注对象 
                        var marker = new TMarker(lnglat);
                        //地图上添加标注点 
                        map.addOverLay(marker);
                        //注册标注点的点击事件 
                        TEvent.bind(marker, "click", marker, function () {
                            var info = this.openInfoWinHtml(winHtml);
                            info.setTitle(name);
                        });
                        zoomArr.push(lnglat);

                        //在页面上显示搜索的列表 
                        var a = document.createElement("a");
                        a.href = "javascript://";
                        a.innerHTML = name;
                        a.onclick = function () {
                            showPosition(marker, name, winHtml);
                        }
                        divMarker.appendChild(document.createTextNode((i + 1) + "."));
                        divMarker.appendChild(a);
                        divMarker.appendChild(document.createElement("br"));
                    })(i);
                }
                //显示搜索结果 
                divMarker.appendChild(document.createTextNode('共' + localsearch.getCountNumber() + '条记录，分' + localsearch.getCountPage() + '页,当前第' + localsearch.getPageIndex() + '页'));
                document.getElementById("searchDiv").appendChild(divMarker);
                document.getElementById("resultDiv").style.display = "block";
            }
            else {
                alert("无结果");
            }
        }

        function LoadLayer() {

            map.clearOverLays();

            var zoomArr = [];

            var prjNum = $("input[id='prjNum']").val();
            var prjName = $("input[id='prjName']").val();
            var prjAddress = $("input[id='prjAddress']").val();

            $.ajax({
                type: 'post', cache: false, dataType: 'json', async: false,
                url: 'http://218.90.162.110:8889/WxjzgcjczyPage/Handler/Data.ashx?type=QueryProjectList',
                data: [
                  { name: 'prjNum', value: prjNum },
                  { name: 'prjName', value: prjName },
                  { name: 'prjAddress', value: prjAddress }
                  ],
                success: function (result) {

                },
                error: function () {
                    alert("error");
                    //$.ligerDialog.error('error!');
                }
            });
        }

        //显示信息框 
        function showPosition(marker, name, winHtml) {
            var info = marker.openInfoWinHtml(winHtml);
            info.setTitle(name);
        }

        //解析项目 
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
            else {
                alert("无结果");
            }
        }

        //清空地图及搜索列表 
        function clearAll() {
            map.clearOverLays();
            document.getElementById("searchDiv").innerHTML = "";
            document.getElementById("resultDiv").style.display = "none";
            document.getElementById("statisticsDiv").innerHTML = "";
            document.getElementById("statisticsDiv").style.display = "none";
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
        
    </script>
</head>
<body onLoad="onLoad()">

    <div id="outter" style="width: 1055px; height: 477px;">
	<div id="mapDiv" align="left" style="position: relative; overflow: hidden; background: url(&quot;http://api.tianditu.com/img/map/bgImg.gif&quot;); cursor: default;"><div id="platform" style="position: absolute; z-index: 100; cursor: default; user-select: none; overflow: visible; left: 527.5px; top: 238.5px;"><div id="t_maskDiv" style="position: absolute; z-index: 180; width: 1055px; height: 477px; background-image: url(&quot;http://api.tianditu.com/img/map/mask.gif&quot;); user-select: none; opacity: 0; left: -527.5px; top: -238.5px;"></div><div style="position: absolute; z-index: 1; user-select: none;"><div id="mapsDiv_11" style="position: absolute; z-index: 100;"><img src="./DataServer" style="width: 256px; height: 256px; user-select: none; position: absolute; opacity: 1; transition: opacity 0.4s ease-in-out; left: 248px; top: -416px;"><img src="./DataServer(1)" style="width: 256px; height: 256px; user-select: none; position: absolute; opacity: 1; transition: opacity 0.4s ease-in-out; left: -8px; top: -416px;"><img src="./DataServer(2)" style="width: 256px; height: 256px; user-select: none; position: absolute; opacity: 1; transition: opacity 0.4s ease-in-out; left: -264px; top: -416px;"><img src="./DataServer(3)" style="width: 256px; height: 256px; user-select: none; position: absolute; opacity: 1; transition: opacity 0.4s ease-in-out; left: -264px; top: -160px;"><img src="./DataServer(4)" style="width: 256px; height: 256px; user-select: none; position: absolute; opacity: 1; transition: opacity 0.4s ease-in-out; left: -8px; top: -160px;"><img src="./DataServer(5)" style="width: 256px; height: 256px; user-select: none; position: absolute; opacity: 1; transition: opacity 0.4s ease-in-out; left: 248px; top: -160px;"><img src="./DataServer(6)" style="width: 256px; height: 256px; user-select: none; position: absolute; opacity: 1; transition: opacity 0.4s ease-in-out; left: -264px; top: 96px;"><img src="./DataServer(7)" style="width: 256px; height: 256px; user-select: none; position: absolute; opacity: 1; transition: opacity 0.4s ease-in-out; left: -8px; top: 96px;"><img src="./DataServer(8)" style="width: 256px; height: 256px; user-select: none; position: absolute; opacity: 1; transition: opacity 0.4s ease-in-out; left: 248px; top: 96px;"><img src="./DataServer(9)" style="width: 256px; height: 256px; user-select: none; position: absolute; opacity: 1; transition: opacity 0.4s ease-in-out; left: -520px; top: -416px;"><img src="./DataServer(10)" style="width: 256px; height: 256px; user-select: none; position: absolute; opacity: 1; transition: opacity 0.4s ease-in-out; left: -520px; top: -160px;"><img src="./DataServer(11)" style="width: 256px; height: 256px; user-select: none; position: absolute; opacity: 1; transition: opacity 0.4s ease-in-out; left: -520px; top: 96px;"><img src="./DataServer(12)" style="width: 256px; height: 256px; user-select: none; position: absolute; opacity: 1; transition: opacity 0.4s ease-in-out; left: -776px; top: -416px;"><img src="./DataServer(13)" style="width: 256px; height: 256px; user-select: none; position: absolute; opacity: 1; transition: opacity 0.4s ease-in-out; left: -776px; top: -160px;"><img src="./DataServer(14)" style="width: 256px; height: 256px; user-select: none; position: absolute; opacity: 1; transition: opacity 0.4s ease-in-out; left: -776px; top: 96px;"><img src="./DataServer(15)" style="width: 256px; height: 256px; user-select: none; position: absolute; opacity: 1; transition: opacity 0.4s ease-in-out; left: 504px; top: -416px;"><img src="./DataServer(16)" style="width: 256px; height: 256px; user-select: none; position: absolute; opacity: 1; transition: opacity 0.4s ease-in-out; left: 504px; top: -160px;"><img src="./DataServer(17)" style="width: 256px; height: 256px; user-select: none; position: absolute; opacity: 1; transition: opacity 0.4s ease-in-out; left: 504px; top: 96px;"></div><div style="position:absolute;left:0px;top:0px;z-index:101;"><img src="./DataServer(18)" style="position: absolute; z-index: 400; user-select: none; border: 0px; padding: 0px; margin: 0px; opacity: 1; transition: opacity 0.4s ease-in-out; left: -8px; top: -416px; width: 256px; height: 256px;"><img src="./DataServer(19)" style="position: absolute; z-index: 400; user-select: none; border: 0px; padding: 0px; margin: 0px; opacity: 1; transition: opacity 0.4s ease-in-out; left: 248px; top: -416px; width: 256px; height: 256px;"><img src="./DataServer(20)" style="position: absolute; z-index: 400; user-select: none; border: 0px; padding: 0px; margin: 0px; opacity: 1; transition: opacity 0.4s ease-in-out; left: -264px; top: -416px; width: 256px; height: 256px;"><img src="./DataServer(21)" style="position: absolute; z-index: 400; user-select: none; border: 0px; padding: 0px; margin: 0px; opacity: 1; transition: opacity 0.4s ease-in-out; left: -264px; top: -160px; width: 256px; height: 256px;"><img src="./DataServer(22)" style="position: absolute; z-index: 400; user-select: none; border: 0px; padding: 0px; margin: 0px; opacity: 1; transition: opacity 0.4s ease-in-out; left: -8px; top: -160px; width: 256px; height: 256px;"><img src="./DataServer(23)" style="position: absolute; z-index: 400; user-select: none; border: 0px; padding: 0px; margin: 0px; opacity: 1; transition: opacity 0.4s ease-in-out; left: 248px; top: -160px; width: 256px; height: 256px;"><img src="./DataServer(24)" style="position: absolute; z-index: 400; user-select: none; border: 0px; padding: 0px; margin: 0px; opacity: 1; transition: opacity 0.4s ease-in-out; left: -520px; top: -416px; width: 256px; height: 256px;"><img src="./DataServer(25)" style="position: absolute; z-index: 400; user-select: none; border: 0px; padding: 0px; margin: 0px; opacity: 1; transition: opacity 0.4s ease-in-out; left: -520px; top: -160px; width: 256px; height: 256px;"><img src="./DataServer(26)" style="position: absolute; z-index: 400; user-select: none; border: 0px; padding: 0px; margin: 0px; opacity: 1; transition: opacity 0.4s ease-in-out; left: -520px; top: 96px; width: 256px; height: 256px;"><img src="./DataServer(27)" style="position: absolute; z-index: 400; user-select: none; border: 0px; padding: 0px; margin: 0px; opacity: 1; transition: opacity 0.4s ease-in-out; left: -264px; top: 96px; width: 256px; height: 256px;"><img src="./DataServer(28)" style="position: absolute; z-index: 400; user-select: none; border: 0px; padding: 0px; margin: 0px; opacity: 1; transition: opacity 0.4s ease-in-out; left: -8px; top: 96px; width: 256px; height: 256px;"><img src="./DataServer(29)" style="position: absolute; z-index: 400; user-select: none; border: 0px; padding: 0px; margin: 0px; opacity: 1; transition: opacity 0.4s ease-in-out; left: 248px; top: 96px; width: 256px; height: 256px;"><img src="./DataServer(30)" style="position: absolute; z-index: 400; user-select: none; border: 0px; padding: 0px; margin: 0px; opacity: 1; transition: opacity 0.4s ease-in-out; left: -776px; top: -416px; width: 256px; height: 256px;"><img src="./DataServer(31)" style="position: absolute; z-index: 400; user-select: none; border: 0px; padding: 0px; margin: 0px; opacity: 1; transition: opacity 0.4s ease-in-out; left: -776px; top: -160px; width: 256px; height: 256px;"><img src="./DataServer(32)" style="position: absolute; z-index: 400; user-select: none; border: 0px; padding: 0px; margin: 0px; opacity: 1; transition: opacity 0.4s ease-in-out; left: -776px; top: 96px; width: 256px; height: 256px;"><img src="./DataServer(33)" style="position: absolute; z-index: 400; user-select: none; border: 0px; padding: 0px; margin: 0px; opacity: 1; transition: opacity 0.4s ease-in-out; left: 504px; top: -416px; width: 256px; height: 256px;"><img src="./DataServer(34)" style="position: absolute; z-index: 400; user-select: none; border: 0px; padding: 0px; margin: 0px; opacity: 1; transition: opacity 0.4s ease-in-out; left: 504px; top: -160px; width: 256px; height: 256px;"><img src="./DataServer(35)" style="position: absolute; z-index: 400; user-select: none; border: 0px; padding: 0px; margin: 0px; opacity: 1; transition: opacity 0.4s ease-in-out; left: 504px; top: 96px; width: 256px; height: 256px;"></div></div><div id="t_overlaysDiv" style="position: absolute; z-index: 500; width: 1055px; height: 477px;"><div style="position: absolute; z-index: 490; border: 0px solid rgb(0, 0, 0); font-size: 12px; color: rgb(0, 0, 0); padding: 2px; white-space: nowrap; left: 1062px; top: -52px;"><div style="position: relative; cursor: pointer; z-index: 500;"><div style="position: relative; left: 0px; top: 0px"><img src="./marker.png" style="width: 20px; height: 34px;"></div></div></div><div style="position: absolute; z-index: 490; border: 0px solid rgb(0, 0, 0); font-size: 12px; color: rgb(0, 0, 0); padding: 2px; white-space: nowrap; left: 1066px; top: -73px;"><div style="position: relative; cursor: pointer; z-index: 500;"><div style="position: relative; left: 0px; top: 0px"><img src="./marker.png" style="width: 20px; height: 34px;"></div></div></div><div style="position: absolute; z-index: 510; border: 0px solid rgb(0, 0, 0); font-size: 12px; color: rgb(0, 0, 0); padding: 2px; white-space: nowrap; left: 1370px; top: -213px;"><div style="position: relative; cursor: pointer; z-index: 500;"><div style="position: relative; left: 0px; top: 0px"><img src="./marker.png" style="width: 20px; height: 34px;"></div></div></div><div style="position: absolute; z-index: 490; border: 0px solid rgb(0, 0, 0); font-size: 12px; color: rgb(0, 0, 0); padding: 2px; white-space: nowrap; left: 1072px; top: -62px;"><div style="position: relative; cursor: pointer; z-index: 500;"><div style="position: relative; left: 0px; top: 0px"><img src="./marker.png" style="width: 20px; height: 34px;"></div></div></div><div style="position: absolute; z-index: 490; border: 0px solid rgb(0, 0, 0); font-size: 12px; color: rgb(0, 0, 0); padding: 2px; white-space: nowrap; left: 1057px; top: -63px;"><div style="position: relative; cursor: pointer; z-index: 500;"><div style="position: relative; left: 0px; top: 0px"><img src="./marker.png" style="width: 20px; height: 34px;"></div></div></div><div style="position: absolute; z-index: 490; border: 0px solid rgb(0, 0, 0); font-size: 12px; color: rgb(0, 0, 0); padding: 2px; white-space: nowrap; left: 1062px; top: -64px;"><div style="position: relative; cursor: pointer; z-index: 500;"><div style="position: relative; left: 0px; top: 0px"><img src="./marker.png" style="width: 20px; height: 34px;"></div></div></div><div style="position: absolute; z-index: 490; border: 0px solid rgb(0, 0, 0); font-size: 12px; color: rgb(0, 0, 0); padding: 2px; white-space: nowrap; left: 1056px; top: -51px;"><div style="position: relative; cursor: pointer; z-index: 500;"><div style="position: relative; left: 0px; top: 0px"><img src="./marker.png" style="width: 20px; height: 34px;"></div></div></div><div style="position: absolute; z-index: 490; border: 0px solid rgb(0, 0, 0); font-size: 12px; color: rgb(0, 0, 0); padding: 2px; white-space: nowrap; left: 1063px; top: -66px;"><div style="position: relative; cursor: pointer; z-index: 500;"><div style="position: relative; left: 0px; top: 0px"><img src="./marker.png" style="width: 20px; height: 34px;"></div></div></div><div style="position: absolute; z-index: 490; border: 0px solid rgb(0, 0, 0); font-size: 12px; color: rgb(0, 0, 0); padding: 2px; white-space: nowrap; left: 1080px; top: -51px;"><div style="position: relative; cursor: pointer; z-index: 500;"><div style="position: relative; left: 0px; top: 0px"><img src="./marker.png" style="width: 20px; height: 34px;"></div></div></div><div style="position: absolute; z-index: 490; border: 0px solid rgb(0, 0, 0); font-size: 12px; color: rgb(0, 0, 0); padding: 2px; white-space: nowrap; left: 1062px; top: -52px;"><div style="position: relative; cursor: pointer; z-index: 500;"><div style="position: relative; left: 0px; top: 0px"><img src="./marker.png" style="width: 20px; height: 34px;"></div></div></div></div></div><div style="position: absolute; z-index: 65535; left: 0px; bottom: 0px;"><div style="margin-left: 4px; font-size: 12px; text-decoration: none; color: black;"><img style="position:absolute;bottom:0px;left:2px;background-color:transparent;background-image:url(http://api.tianditu.com/img/map/logo.png);filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=image,src=http://api.tianditu.com/img/map/logo.png);MozOpacity:1;opacity:1;" src="./logo.png" width="53px" height="22px" opacity="0"><div style="position:absolute;bottom:0px;left:58px;white-space:nowrap;">GS(2017)508号</div></div></div></div>
		<div id="describediv" style="display: none;">
		<!-- 搜索面板 -->
		<div class="search">
			搜索内容：<input type="text" id="keyWord" value="">
			<!-- <input type="button" onclick="localsearch.search(document.getElementById(&#39;keyWord&#39;).value,2)" value="搜索">-->
			<input type="button" onclick="LoadLayer()" value="搜索">
		</div>
		<br>
		<!-- 提示词面板 -->
		<div id="promptDiv" class="prompt"></div>
		<!-- 统计面板 -->
		<div id="statisticsDiv" class="statistics" style="display: none;"></div>
		<!-- 搜索结果面板 -->
		<div id="resultDiv" class="result" style="display: block;">
			<div id="searchDiv"><div></div></div>
			<div id="pageDiv">
			    <input type="button" value="第一页" onclick="localsearch.firstPage()">
			    <input type="button" value="上一页" onclick="localsearch.previousPage()">
			    <input type="button" value="下一页" onclick="localsearch.nextPage()">
			    <input type="button" value="最后一页" onclick="localsearch.lastPage()">
			    <br>
				转到第<input type="text" value="1" id="pageId" size="3">页
			    <input type="button" onclick="localsearch.gotoPage(parseInt(document.getElementById(&#39;pageId&#39;).value));" value="转到">
			</div>
		</div>
	</div>    
 
    </div>
    <div id="codeButton0" class="codeButton0style2"></div>

    <div id="mapTypeStyle" class="mapTypeStyle">
        <select id="mapTypeSelect" onchange="switchingMapType(this);" style="height:30px">
            <option value="TMAP_NORMAL_MAP">地图</option>
            <option value="TMAP_SATELLITE_MAP">卫星</option>
            <option value="TMAP_HYBRID_MAP">卫星混合</option>
            <option value="TMAP_TERRAIN_MAP">地形</option>
            <option value="TMAP_TERRAIN_HYBRID_MAP">地形混合</option>
        </select>
    </div>

</body>
</html>
