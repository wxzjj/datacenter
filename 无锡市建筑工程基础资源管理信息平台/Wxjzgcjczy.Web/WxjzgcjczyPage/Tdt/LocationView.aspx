<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LocationView.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Tdt.LocationView" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta charset="UTF-8"/>
    <meta name="keywords" content="天地图" />
    <title>天地图</title>
    
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
        var LoginID = '<%#Eval("this.WorkUser.UserID") %>';

        var localsearch;

        function onLoad() {
            //初始化地图对象
            map = new TMap("mapDiv");
            //设置显示地图的中心点和级别
            map.centerAndZoom(new TLngLat(120.29709, 31.57552), zoom);
            //允许鼠标滚轮缩放地图
            map.enableHandleMouseScroll();
            //允许双击地图放大
            map.enableDoubleClickZoom();

            var config0 = {
                anchor: "TMAP_ANCHOR_BOTTOM_RIGHT", //设置鹰眼位置,"TMAP_ANCHOR_TOP_LEFT"表示左上，"TMAP_ANCHOR_TOP_RIGHT"表示右上，"TMAP_ANCHOR_BOTTOM_LEFT"表示左下，"TMAP_ANCHOR_BOTTOM_RIGHT"表示右下，"TMAP_ANCHOR_LEFT"表示左边，"TMAP_ANCHOR_TOP"表示上边，"TMAP_ANCHOR_RIGHT"表示右边，"TMAP_ANCHOR_BOTTOM"表示下边，"TMAP_ANCHOR_OFFSET"表示自定义位置,默认值为"TMAP_ANCHOR_BOTTOM_RIGHT" 
                size: new TSize(180, 120), 		//鹰眼显示的大小 
                isOpen: true						//鹰眼是否打开，true表示打开，false表示关闭，默认为关闭 
            };
            //创建鹰眼控件对象 
            //overviewMap = new TOverviewMapControl(config0);
            //添加鹰眼控件 
            //map.addControl(overviewMap);

            //var config = {
            //    pageCapacity: 10, //每页显示的数量 
            //    onSearchComplete: localSearchResult	//接收数据的回调函数 
            //};
            //创建搜索对象 
            //localsearch = new TLocalSearch(map, config);

        }

        function localSearchResult(result) {
            //清空地图及搜索列表 
            clearAll();

            //根据返回类型解析搜索结果 
            pois(result.pois);
        }

        //解析点数据结果 
        function pois(obj) {
            if (obj) {
                //显示搜索列表 
                var divMarker = document.createElement("div");
                divMarker.appendChild(document.createTextNode('共' + obj.length + '条记录'));
                divMarker.appendChild(document.createElement("br"));
                //坐标数组，设置最佳比例尺时会用到 
                var zoomArr = [];
                for (var i = 0; i < obj.length; i++) {
                    //闭包 
                    (function (i) {
                        //名称 
                        var name = obj[i].PrjName;
                        //地址 
                        var address = obj[i].programme_address;
                        //坐标 
                        if (obj[i].jd && obj[i].jd.length !== 0) {
                            var lnglat = new TLngLat(obj[i].jd, obj[i].wd);

                            var winHtml = "<a href=\"/IntegrativeShow2/SysFiles/Pages/Index_View.aspx?viewUrl=../PagesZHJG/Zhjg_Lxxmdj_Menu.aspx$LoginID=" + LoginID + "%PKID=" + obj[i].PKID + "%PrjNum=" + obj[i].PrjNum + "&titleName=" + obj[i].PrjName + "\" target=\"_blank\">"
							+ name + "</a>";
                            winHtml += "<br>地址:" + address;
                            winHtml += "<br>档号:" + obj[i].DocNum;
                            winHtml += "<br>案卷数:" + obj[i].DocCount;

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
                                map.centerAndZoom(lnglat, 14);
                                showPosition(marker, name, winHtml);
                            }
                            divMarker.appendChild(document.createTextNode((i + 1) + "."));
                            divMarker.appendChild(a);
                            divMarker.appendChild(document.createElement("br"));
                        } else {
                            var a = document.createElement("span");
                            a.innerHTML = name;
                            divMarker.appendChild(document.createTextNode((i + 1) + "."));
                            divMarker.appendChild(a);
                            divMarker.appendChild(document.createElement("br"));
                        }



                    })(i);
                }
                //显示地图的最佳级别
                if (zoomArr.length == 1) {
                    map.centerAndZoom(zoomArr[0], 14);
                } else {
                    map.setViewport(zoomArr);
                }


                //显示搜索结果 
                //divMarker.appendChild(document.createTextNode('共' + obj.length + '条记录，分' + localsearch.getCountPage() + '页,当前第' + localsearch.getPageIndex() + '页'));

                document.getElementById("searchDiv").appendChild(divMarker);
                document.getElementById("resultDiv").style.display = "block";
            }
            else {
                alert("无结果");
            }
            $('#searchLoadingIcon').hide();
        }

        function LoadLayer() {
            $('#searchLoadingIcon').show();
            clearAll();

            map.clearOverLays();

            var zoomArr = [];

            var prjNum = $("#prjNum").val();
            var prjName = $("#prjName").val();
            var prjAddress = $("#prjAddress").val();

            $.ajax({
                type: 'post', cache: false, async: false,
                url: '/WxjzgcjczyPage/Handler/Data.ashx?type=QueryProjectList',
                data: [
                  { name: 'prjNum', value: prjNum },
                  { name: 'prjName', value: prjName },
                  { name: 'prjAddress', value: prjAddress }
                  ],
                success: function (result) {
                    //var resultJson = $.parseJSON(result);
                    var resultJson = eval("(" + result + ")");
                    pois(resultJson.pois);
                },
                error: function (error) {
                    console.log("error:" + error)
                }
            });
        }

        //显示信息框 
        function showPosition(marker, name, winHtml) {
            var info = marker.openInfoWinHtml(winHtml);
            info.setTitle(name);
        }

        //解析项目 
        /**
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
        }*/

        //清空地图及搜索列表 
        function clearAll() {
            map.clearOverLays();
            document.getElementById("searchDiv").innerHTML = "";
            document.getElementById("resultDiv").style.display = "none";
            //document.getElementById("statisticsDiv").innerHTML = "";
            //document.getElementById("statisticsDiv").style.display = "none";
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
        <div id="mapDiv"></div>
        <div id="describediv" style="display: none;">
		<!-- 搜索面板 -->
		<div class="search">
			项目编号：<input type="text" id="prjNum" value=""/><br />
            项目名称：<input type="text" id="prjName" value=""><br />
            项目地址：<input type="text" id="prjAddress" value="">
			<!-- <input type="button" onclick="localsearch.search(document.getElementById(&#39;keyWord&#39;).value,2)" value="搜索">-->
			<div style="height:24px;">
				  <input type="button" onclick="LoadLayer()" value="搜索" style="height:24px;">
				  <img src="/WxjzgcjczyPage/Common/images/loading.gif" id = "searchLoadingIcon" style="width:24px;height:24px;display:none;vertical-align:middle"/>
			</div>
		</div>
		<br>
		<!-- 提示词面板 -->
		<div id="promptDiv" class="prompt"></div>
		<!-- 统计面板 -->
		<!--<div id="statisticsDiv" class="statistics" style="display: none;"></div>-->
		<!-- 搜索结果面板 -->
		<div id="resultDiv" class="result" style="display: block;">
			<div id="searchDiv" style="overflow:scroll;min-height: 150px;max-height: 300px;"><div></div></div>
            <!--
			<div id="pageDiv">
			    <input type="button" value="第一页" onclick="localsearch.firstPage()">
			    <input type="button" value="上一页" onclick="localsearch.previousPage()">
			    <input type="button" value="下一页" onclick="localsearch.nextPage()">
			    <input type="button" value="最后一页" onclick="localsearch.lastPage()">
			    <br>
				转到第<input type="text" value="1" id="pageId" size="3">页
			    <input type="button" onclick="localsearch.gotoPage(parseInt(document.getElementById(&#39;pageId&#39;).value));" value="转到">
			</div>-->
		</div>
	</div>    
    <div id="close"></div>
    </div>
    <!--<div id="codeButton0" class="codeButton0style2"></div>-->
    <div id="controlButton"></div>

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
