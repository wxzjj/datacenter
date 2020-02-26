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
            //��ʼ����ͼ���� 
            map = new TMap("map");
            //������ʾ��ͼ�����ĵ�ͼ��� 
            map.centerAndZoom(new TLngLat(120.31554, 31.49201), zoom);
            //�������������ŵ�ͼ 
            map.enableHandleMouseScroll();

            var config = {
                type: "TMAP_NAVIGATION_CONTROL_LARGE", //����ƽ�Ƶ���ʾ���� 
                anchor: "TMAP_ANCHOR_TOP_LEFT", 		//����ƽ�ƿؼ���ʾ��λ�� 
                offset: [0, 0], 						//����ƽ�ƿؼ���ƫ��ֵ 
                showZoomInfo: true						//�Ƿ���ʾ������ʾ��Ϣ��true��ʾ��ʾ��false��ʾ���ء� 

            };

            var config1 = {
                anchor: "TMAP_ANCHOR_BOTTOM_RIGHT", //����ӥ��λ��,"TMAP_ANCHOR_TOP_LEFT"��ʾ���ϣ�"TMAP_ANCHOR_TOP_RIGHT"��ʾ���ϣ�"TMAP_ANCHOR_BOTTOM_LEFT"��ʾ���£�"TMAP_ANCHOR_BOTTOM_RIGHT"��ʾ���£�"TMAP_ANCHOR_LEFT"��ʾ��ߣ�"TMAP_ANCHOR_TOP"��ʾ�ϱߣ�"TMAP_ANCHOR_RIGHT"��ʾ�ұߣ�"TMAP_ANCHOR_BOTTOM"��ʾ�±ߣ�"TMAP_ANCHOR_OFFSET"��ʾ�Զ���λ��,Ĭ��ֵΪ"TMAP_ANCHOR_BOTTOM_RIGHT" 
                size: new TSize(160, 110), 		//ӥ����ʾ�Ĵ�С 
                isOpen: true						//ӥ���Ƿ�򿪣�true��ʾ�򿪣�false��ʾ�رգ�Ĭ��Ϊ�ر� 
            };
            //����ӥ�ۿؼ����� 
            overviewMap = new TOverviewMapControl(config1);
            //���ӥ�ۿؼ� 
            map.addControl(overviewMap);

            //��������ƽ�ƿؼ����� 
            control = new TNavigationControl(config);
            //�������ƽ�ƿؼ� 
            map.addControl(control);
            //��������ͼ������ק
            map.enableInertia();

            //�����Զ���ؼ�������ӵ���ͼ�� 
            var mapTypeStyle = document.getElementById("mapTypeStyle");
            var mapTyleControl = new THtmlElementControl(mapTypeStyle);
            mapTyleControl.setRight(5);
            mapTyleControl.setTop(5);
            map.addControl(mapTyleControl);

            //���������߿ؼ����� 
            var scale = new TScaleControl();
            //��ӱ����߿ؼ� 
            map.addControl(scale);

            var x = $("[id$='gis_jd']").val();
            var y = $("[id$='gis_wd']").val();

            if (x != "" && y != "") {

                var lnglat = new TLngLat(parseFloat(x), parseFloat(y));
                theMarker = addTheOverLay(lnglat);
            }
          
            var config = {

                pageCapacity: 8,    //ÿҳ��ʾ������ 

                onSearchComplete: localSearchResult  //�������ݵĻص����� 

            };

            //������������
            localsearch = new TLocalSearch(map, config);
            localsearch.search("����ʡ������");

            addMapClick();
        }
        
        function addMapClick()
        {
            if(mapclick==null)
            {
                //ע���ͼ�ĵ���¼�
                mapclick = TEvent.addListener(map, "click", function(lnglat, btn) {

                    if (btn == 2 || btn == "2") {
                        return;
                    }

                    var prjNum = $("[id$='db_PKID']").val();
                    if (prjNum == undefined || prjNum == "") {
                        //alert("��������Ƿ���");
                        return;
                    }

                    //����������ת���ɾ�γ������
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

            //������ע����
            // var lnglat = new TLngLat(parseFloat(x), parseFloat(y))
            var marker = new TMarker(lnglat);
            map.addOverLay(marker);
            //marker.setIconImage(url:String,size:TSize,anchor:Array);
            marker.setIconImage('../Common/icons/marker2.png', new TSize(50, 50));
            //marker.enableDragging();
//            //ע���ע����꿪ʼ�϶��괥���¼�
//            TEvent.addListener(marker, "dragstart", function(lnglat) {

//                var x = lnglat.getLng();
//                var y = lnglat.getLat();

//                $("#x").val(x);
//                $("#y").val(y);
//            }); 
//          

//            //ע���ע������϶���ע���֮�󴥷��¼�
//            TEvent.addListener(marker, "dragend", function(lnglat) {

//                AdjustLocation(lnglat);
//             

//            });

            //���ͼ����ӱ�ע
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
                if (confirm("����Ŀ�Ѿ���ע��ȷ��Ҫ���±�ע��"))
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

                                    theMarker.setLngLat(lnglat); //���ñ�ע������
                                }
                                else {
                                    theMarker = addTheOverLay(lnglat); //����һ����ע
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
                                alert("��עʧ�ܣ�");
                            }
                        }
                    },
                    error: function() {
                        //alert("Ajax���ִ���,��ˢ��ҳ������ԣ�");
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

            //������ע����
            var marker = new TMarker(lnglat);
            //��ͼ����ӱ�ע��
            map.addOverLay(marker);

            var isTrue = false;
            if (jd != "" && wd != "") {

                if (jd == x && wd == y) {

                    return;
                }

                if (confirm("����Ŀ�Ѿ���ע��ȷ��Ҫ���±�ע��")) {
                    isTrue = true;

                }
                else {
                    isTrue = false;
                }
            }
            else {
                if (confirm("ȷ��Ҫ��ӱ�ע��")) {
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

                                    theMarker.setLngLat(lnglat); //���ñ�ע������
                                }
                                else {
                                    theMarker = addTheOverLay(lnglat); //����һ����ע
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
                                alert("��עʧ�ܣ�");
                            }
                        }
                    },
                    error: function() {
                        //alert("Ajax���ִ���,��ˢ��ҳ������ԣ�");

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

            if (!confirm("���϶��˱�ע��ȷ��Ҫ���¸ñ�עλ����")) {

                var gis_jd = $("[id$='gis_jd']").val();
                var gis_wd = $("[id$='gis_wd']").val();
                var lnglat = new TLngLat(parseFloat(gis_jd), parseFloat(gis_wd));
                if (theMarker) {

                    theMarker.setLngLat(lnglat); //���ñ�ע������
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

                                theMarker.setLngLat(lnglat); //���ñ�ע������
                            }
                            else {

                                theMarker = addTheOverLay(lnglat); //����һ����ע

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

                            alert("��עʧ�ܣ�");


                        }

                    }

                },
                error: function() {
                    //alert("Ajax���ִ���,��ˢ��ҳ������ԣ�");

                }
            });

        }


        function localSearchResult(result) {
            //��յ�ͼ�������б� 
            clearAll();

            //�����ʾ�� 
            //prompt(result);

            //���ݷ������ͽ���������� 
            switch (parseInt(result.getResultType())) {
                case 1:
                    //���������ݽ�� 
                    pois(result.getPois());
                    break;
                case 2:
                    //�����Ƽ����� 
                    statistics(result.getStatistics());
                    break;
                case 3:
                    //�������������߽� 
                    area(result.getArea());
                    break;
                case 4:
                    //�����������Ϣ 
                    suggests(result.getSuggests());
                    break;
                //                case 5:      
                //                    //����������Ϣ       
                //                    lineData(result.getLineData());      
                //                    break;    
            }

        }

        //������ʾ�� 
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
                        promptHtml += "<p>���Ƿ�Ҫ��" + promptAdmins[0].name + "</strong>�����������<strong>" + obj.getKeyword() + "</strong>��������ݣ�<p>";
                    }
                    else if (promptType == 2) {
                        promptHtml += "<p>��<strong>" + promptAdmins[0].name + "</strong>û����������<strong>" + obj.getKeyword() + "</strong>��صĽ����<p>";
                        if (meanprompt) {
                            promptHtml += "<p>���Ƿ�Ҫ�ң�<font weight='bold' color='#035fbe'><strong>" + meanprompt + "</strong></font><p>";
                        }
                    }
                    else if (promptType == 3) {
                        promptHtml += "<p style='margin-bottom:3px;'>��������ؽ�������Ƿ�Ҫ�ң�</p>"
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

        //���������ݽ�� 
        function pois(obj) {
            if (obj) {
                //��ʾ�����б� 
                var divMarker = document.createElement("div");
                //�������飬������ѱ�����ʱ���õ�
                var zoomArr = [];
                for (var i = 0; i < obj.length; i++) {
                    //�հ� 
                    (function(i) {
                        //���� 
                        var name = obj[i].name;
                        //��ַ 
                        var address = obj[i].address;
                        //���� 
                        var lnglatArr = obj[i].lonlat.split(" ");
                        var lnglat = new TLngLat(lnglatArr[0], lnglatArr[1]);

                        var winHtml = "��ַ:" + address;
                        winHtml += "<br/><input type='button' name='btnOK' name='btnOK' value='ȷ��' onclick='OKClick(this);' style='margin:1px 0 1px0;' />";

                        //������ע����
                        var marker = new TMarker(lnglat);

                        //��ͼ����ӱ�ע��
                        map.addOverLay(marker);

                        searchPointsOverLays.push(marker);

                        //ע���ע��ĵ���¼�
                        TEvent.bind(marker, "click", marker, function() {

                            $("#x").val(lnglat.getLng());
                            $("#y").val(lnglat.getLat());

                            var info = this.openInfoWinHtml(winHtml);
                            info.setTitle(name);
                          
                            //TEvent.cancelBubble("onclick");

                        });
                        zoomArr.push(lnglat);

                        //��ҳ������ʾ�������б� 
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
                //��ʾ��ͼ����Ѽ��� 
                map.setViewport(zoomArr);
                //��ʾ������� 
                divMarker.appendChild(document.createTextNode('��' + localsearch.getCountNumber() + '����¼����' + localsearch.getCountPage() + 'ҳ,��ǰ��' + localsearch.getPageIndex() + 'ҳ'));
                document.getElementById("searchDiv").appendChild(divMarker);
                document.getElementById("resultDiv").style.display = "block";

                removeMapClick();
            }
           
        }


        //��ʾ��Ϣ�� 
        function showPosition(marker, name, winHtml) {
            var info = marker.openInfoWinHtml(winHtml);
            info.setTitle(name);
        }

        //�����Ƽ����� 
        function statistics(obj) {
            if (obj) {
                //�������飬������ѱ�����ʱ���õ� 
                var pointsArr = [];
                var priorityCitysHtml = "";
                var allAdminsHtml = "";
                var priorityCitys = obj.priorityCitys;
                if (priorityCitys) {
                    //�Ƽ�������ʾ  
                    priorityCitysHtml += "���й����³����н��<ul>";
                    for (var i = 0; i < priorityCitys.length; i++) {
                        priorityCitysHtml += "<li>" + priorityCitys[i].name + "(" + priorityCitys[i].count + ")</li>";
                    }
                    priorityCitysHtml += "</ul>";
                }

                var allAdmins = obj.allAdmins;
                if (allAdmins) {
                    allAdminsHtml += "�������<ul>";
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

        //�������������߽�
        function area(obj) {

            if (obj) {

                searchLinesOverLays = [];
                //�������飬������ѱ�����ʱ���õ�
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

                    //�����߶��� 
                    var line = new TPolyline(regionLngLats, { strokeColor: "red", strokeWeight: 3, strokeOpacity: 1, strokeStyle: "dashed" });
                    //���ͼ�������
                    map.addOverLay(line);

                }
                //��ʾ��ѱ����� 
                //map.setViewport(pointsArr);
            }
        }


        function area2() {

            if (searchLinesOverLays.length > 0) {

                for (var i = 0; i < searchLinesOverLays.length; i++) {
                    var regionLngLats = searchLinesOverLays[i];
                    //�����߶��� 
                    var line = new TPolyline(regionLngLats, { strokeColor: "red", strokeWeight: 3, strokeOpacity: 1, strokeStyle: "dashed" });
                    //���ͼ�������
                    map.addOverLay(line);
                }
                //��ʾ��ѱ����� 
                //map.setViewport(pointsArr);
            }
        }

        //�����������Ϣ 
        function suggests(obj) {
            if (obj) {
                //�������ʾ�������������Ϊ�����滮����ʻ򹫽�վ����ʱ�����ؽ��Ϊ������Ϣ�Ľ���ʡ� 
                var suggestsHtml = "�������ʾ<ul>";
                for (var i = 0; i < obj.length; i++) {
                    suggestsHtml += "<li>" + obj[i].name + "&nbsp;&nbsp;<font style='color:#666666'>" + obj[i].address + "</font></li>";
                }
                suggestsHtml += "</ul>";
                document.getElementById("suggestsDiv").style.display = "block";
                document.getElementById("suggestsDiv").innerHTML = suggestsHtml;
            }
        }

        //        //����������Ϣ 
        //        function lineData(obj) {
        //            if (obj) {
        //                //������ʾ 
        //                var lineDataHtml = "������ʾ<ul>";
        //                for (var i = 0; i < obj.length; i++) {
        //                    lineDataHtml += "<li>" + obj[i].name + "&nbsp;&nbsp;<font style='color:#666666'>��" + obj[i].stationNum + "վ</font></li>";
        //                }
        //                lineDataHtml += "</ul>";
        //                document.getElementById("lineDataDiv").style.display = "block";
        //                document.getElementById("lineDataDiv").innerHTML = lineDataHtml;
        //            }
        //        }

        //��յ�ͼ�������б�
        function clearAll() {

            if (searchPointsOverLays.length > 0) {

                for (var i = 0; i < searchPointsOverLays.length; i++) {
                    //�ر���Ϣ��
                    searchPointsOverLays[i].closeInfoWindow();
                    //ɾ��ע��������¼�
                    TEvent.deposeNode(searchPointsOverLays[i]);
                    //�ӵ�ͼ�������ע
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
        <!-- ������� -->
        <div class="search">
            �������ݣ�<input type="text" id="keyWord" value="" />
            <input type="button" onclick="localsearch.search(document.getElementById('keyWord').value)"
                value="����" style="height: 30px;" />
            <input type="button" onclick="closeSearch();" value="�ر�" style="height: 30px;" />
        </div>
        <br />
        <!-- ��ʾ����� -->
        <div id="promptDiv" class="prompt">
        </div>
        <!-- ͳ����� -->
        <div id="statisticsDiv" class="statistics">
        </div>
        <!-- �������� -->
        <div id="suggestsDiv" class="suggests">
        </div>
        <!-- ������ʾ��� -->
        <div id="lineDataDiv" class="lineData">
        </div>
        <!-- ���������� -->
        <div id="resultDiv" class="result">
            <div id="searchDiv">
            </div>
            <div id="pageDiv">
                <input type="button" value="��һҳ" onclick="localsearch.firstPage()" />
                <input type="button" value="��һҳ" onclick="localsearch.previousPage()" />
                <input type="button" value="��һҳ" onclick="localsearch.nextPage()" />
                <input type="button" value="���һҳ" onclick="localsearch.lastPage()" />
                <br />
                ת����<input type="text" value="1" id="pageId" size="3" />ҳ
                <input type="button" onclick="localsearch.gotoPage(parseInt(document.getElementById('pageId').value));"
                    value="ת��" />
            </div>
        </div>
    </div>
    <div id="mapTypeStyle" style="position: absolute; left: 90%; top: 50px;">
        <select id="mapTypeSelect" onchange="switchingMapType(this);">
            <option value="TMAP_NORMAL_MAP">��ͼ</option>
            <option value="TMAP_SATELLITE_MAP">����</option>
            <option value="TMAP_HYBRID_MAP">���ǻ��</option>
            <option value="TMAP_TERRAIN_MAP">����</option>
            <option value="TMAP_TERRAIN_HYBRID_MAP">���λ��</option>
        </select>
    </div>
    </form>
</body>
</html>
