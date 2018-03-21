<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zhjg_Lxxmdj_View.aspx.cs"
    Inherits="IntegrativeShow2.SysFiles.PagesZhjg.Zhjg_Lxxmdj_View" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../App_Themes/Themes_Standard/Stylesheet1.css" rel="Stylesheet" type="text/css" />

    <script language="javascript" src="http://api.tianditu.com/js/maps.js" type="text/javascript"></script>

    <script src="../../Common/jquery-1.3.2.min.js" type="text/javascript"></script>

    <style type="text/css">
        .sureCss
        {
            padding: 0;
            margin: 0;
        }
    </style>

</head>
<body class="body_haveBackImg">
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <!-- 招标投标基本信息 -->
        <tr>
            <td class="view_head" style="height: 25px; vertical-align: bottom">
                <img src="../Images/TitleImgs/Title_gcjbxx.gif" height="25px" alt="" />
            </td>
        </tr>
        <tr>
            <td class="view_center" style="height: auto;" id="td">
                <table cellspacing="1" cellpadding="0" width="100%" align="center" border="0" class="table">
                    <tr>
                        <td class="td_text" width="15%">
                            项目编号
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="PrjNum" ItemName="PrjNum" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_value"  colspan="2" rowspan="21">
                         <div style=" display:none;">
                         <Bigdesk8:DBText ID="db_IsSgbz" ItemName="IsSgbz" runat="server" ></Bigdesk8:DBText>
                        </div>
                            <div id="map" style="width: 100%; height:400px;">
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
                        </td>
                    </tr>
                    <tr>
                     <td class="td_text" width="15%">
                            行政审批编号
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="PrjInnerNum" ItemName="PrjInnerNum" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            立项文号
                        </td>
                        <td class="td_value" >
                            <Bigdesk8:DBText ID="PrjApprovalNum" ItemName="PrjApprovalNum" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            项目名称
                        </td>
                        <td class="td_value" width="35%" >
                            <Bigdesk8:DBText ID="PrjName" ItemName="PrjName" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            项目分类
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="PrjType" ItemName="PrjType" runat="server"></Bigdesk8:DBText>
                        </td>
                       
                    </tr>
                    <tr>
                     <td class="td_text" width="15%">
                            工程用途
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="PrjFunction" ItemName="PrjFunction" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            建设规模
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="PrjSize" ItemName="PrjSize" runat="server"></Bigdesk8:DBText>
                        </td>
                      
                    </tr>
                    <tr>
                      <td class="td_text" width="15%">
                            建设性质
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="PrjProperty" ItemName="PrjProperty" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            建设单位名称
                        </td>
                        <td class="td_value" width="35%">
                           <%-- <Bigdesk8:DBText ID="BuildCorpName" ItemName="BuildCorpName" runat="server"></Bigdesk8:DBText>--%>
                            <asp:HyperLink ID="hlk_Jsdw" runat="server" Target="_blank"></asp:HyperLink>
                            
                        </td>
                      
                    </tr>
                    <tr>
                      <td class="td_text" width="15%">
                            建设单位组织机构代码<br />
                            （社会信用代码）
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="BuildCorpCode" ItemName="BuildCorpCode" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            项目所在省
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="Province" ItemName="Province" runat="server"></Bigdesk8:DBText>
                        </td>
                      
                    </tr>
                    <tr>
                      <td class="td_text" width="15%">
                            项目所在地市
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="City" ItemName="City" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            项目所在区县
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="County" ItemName="County" runat="server"></Bigdesk8:DBText>
                        </td>
                       
                    </tr>
		  <tr>
                        <td class="td_text" width="15%">
                            项目地址
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="programme_address" ItemName="programme_address" runat="server"></Bigdesk8:DBText>
                        </td>
                       
                    </tr>
                    <tr>
                     <td class="td_text" width="15%">
                            立项级别
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="PrjApprovalLevelNum" ItemName="PrjApprovalLevel" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            经度
                        </td>
                        <td class="td_value" width="35%" style="padding-bottom: 2px;">
                            <Bigdesk8:DBText ID="gis_jd" ItemName="jd" runat="server" ></Bigdesk8:DBText>
                          
                        </td>
                      
                    </tr>
                    <tr>
                      <td class="td_text" width="15%">
                            纬度
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="gis_wd" ItemName="wd" runat="server" ></Bigdesk8:DBText>
                        </td>
                    </tr>
                 
                    <tr>
                        <td class="td_text" width="15%">
                            建设用地规划许可证编号
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="BuldPlanNum" ItemName="BuldPlanNum" runat="server"></Bigdesk8:DBText>
                        </td>
                       
                    </tr>
                    <tr>
                     <td class="td_text" width="15%">
                            建设工程规划许可证编号
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="ProjectPlanNum" ItemName="ProjectPlanNum" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" width="15%">
                            项目金额/总投资（万元）
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="AllInvest" ItemName="AllInvest" runat="server"></Bigdesk8:DBText>
                        </td>
                       
                    </tr>
                    <tr>
                     <td class="td_text" width="15%">
                            总面积（平方米）
                        </td>
                        <td class="td_value" width="35%">
                            <Bigdesk8:DBText ID="AllArea" ItemName="AllArea" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text">
                            单项项目列表
                        </td>
                        <td colspan="3" class="td_gridviewvalue">
                            <asp:GridView ID="Gdv_LxxmDxgcInfo" runat="server" AllowPaging="false" AutoGenerateColumns="false"
                                BorderWidth="1px" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="项目名称">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("xmmc") %>' NavigateUrl='<%#string.Format("{0}?viewUrl=../PagesZHJG/Zhjg_Lxxmdj_Dxgc_View.aspx$LoginID={1}%PKID={2}&titleName={3}",publicViewUrl,this.WorkUser.UserID,Eval("PKID"),"单项工程-"+Eval("xmmc")) %>'
                                                Target="_blank" />
                                        </ItemTemplate>
                                        <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="20%" HorizontalAlign="left" />
                                        <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="单项项目分类" DataField="gclb">
                                        <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="10%" HorizontalAlign="Center" />
                                        <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="结构类型" DataField="jglx">
                                        <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="10%" HorizontalAlign="Center" />
                                        <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="工程用途" DataField="jsyt">
                                        <ItemStyle BorderWidth="1px" BorderColor="#7B7B7B" Width="20%" HorizontalAlign="Center" />
                                        <HeaderStyle BorderWidth="1px" BorderColor="#7B7B7B" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

     <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <!-- 项目补充信息 -->
        <tr>
            <td class="view_head" style="height: 25px; vertical-align: bottom">
                <img src="../Images/TitleImgs/Title_bcxx.gif" height="25px" alt="" />
            </td>
        </tr>
        <tr>
            <td class="view_center">
                <table cellspacing="1" cellpadding="0" width="100%" align="center" border="0" class="table">
                    <tr>
                        <td class="td_text" style="width: 15%;">
                            <span>建设规预制装配率</span></td>
                        <td class="td_value" style="width: 35%;">
                            <Bigdesk8:DBText ID="gyzzpl" ItemName="gyzzpl" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" style="width: 15%;">
                            <span>电子邮箱</span></td>
                        <td class="td_value" style="width: 35%;">
                            <Bigdesk8:DBText ID="dzyx" ItemName="dzyx" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" style="width: 15%;">
                            <span>联系人</span></td>
                        <td class="td_value" style="width: 35%;">
                            <Bigdesk8:DBText ID="lxr" ItemName="lxr" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" style="width: 15%;">
                            <span>移动电话</span></td>
                        <td class="td_value" style="width: 35%;">
                            <Bigdesk8:DBText ID="yddh" ItemName="yddh" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" style="width: 15%;">
                            <span>项目投资总额</span></td>
                        <td class="td_value" style="width: 35%;">
                            <Bigdesk8:DBText ID="xmtz" ItemName="xmtz" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" style="width: 15%;">
                            <span>国有投资额</span></td>
                        <td class="td_value" style="width: 35%;">
                            <Bigdesk8:DBText ID="gytze" ItemName="gytze" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" style="width: 15%;">
                            <span>国有投资占比</span></td>
                        <td class="td_value" style="width: 35%;">
                            <Bigdesk8:DBText ID="gytzbl" ItemName="gytzbl" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" style="width: 15%;">
                            <span>立项批准项目投资总额</span>
                        </td>
                        <td class="td_value" style="width: 35%;">
                            <Bigdesk8:DBText ID="lxtzze" ItemName="lxtzze" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
   
                    <tr>
                        <td class="td_text" style="width: 15%;">
                            <span>操作</span></td>
                        <td class="td_value" style="width: 35%;" colspan="3">
                            <% if ("wangyj" == this.WorkUser.LoginName.ToString() || "wangxp" == this.WorkUser.LoginName.ToString()) { %> 
                                <button type="button" id ="uploadToStBtn" onclick='uploadToStTBProjectAddInfo()'>上报项目补充信息</button>
                            <% } else { %>  <% } %>
                             
                        </td>
                       
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    

    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <!-- 招标投标基本信息 -->
        <tr>
            <td class="view_head" style="height: 25px; vertical-align: bottom">
                <img src="../Images/TitleImgs/Title_tjxx.gif" height="25px" alt="" />
            </td>
        </tr>
        <tr>
            <td class="view_center">
                <table cellspacing="1" cellpadding="0" width="100%" align="center" border="0" class="table">
                    <tr>
                        <td class="td_text" style="width: 15%;">
                            单项工程数
                        </td>
                        <td class="td_value" style="width: 35%;">
                            <Bigdesk8:DBText ID="DBLabel8" ItemName="DxgcCount" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" style="width: 15%;">
                            施工图审查数
                        </td>
                        <td class="td_value" style="width: 35%;">
                            <Bigdesk8:DBText ID="DBLabel1" ItemName="SgtscCount" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" style="width: 15%;">
                            招投标信息数
                        </td>
                        <td class="td_value" style="width: 35%;">
                            <Bigdesk8:DBText ID="DBLabel2" ItemName="ZtbxxCount" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" style="width: 15%;">
                            合同备案数
                        </td>
                        <td class="td_value" style="width: 35%;">
                            <Bigdesk8:DBText ID="DBLabel3" ItemName="HtbaCount" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" style="width: 15%;">
                            安全报监数
                        </td>
                        <td class="td_value" style="width: 35%;">
                            <Bigdesk8:DBText ID="DBLabel4" ItemName="AqbjCount" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" style="width: 15%;">
                            质量报监数
                        </td>
                        <td class="td_value" style="width: 35%;">
                            <Bigdesk8:DBText ID="DBLabel5" ItemName="ZlbjCount" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_text" style="width: 15%;">
                            施工许可数
                        </td>
                        <td class="td_value" style="width: 35%;">
                            <Bigdesk8:DBText ID="DBLabel6" ItemName="SgxkCount" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" style="width: 15%;">
                            竣工备案数
                        </td>
                        <td class="td_value" style="width: 35%;">
                            <Bigdesk8:DBText ID="DBLabel7" ItemName="JgbaCount" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <!-- 档案信息 -->
        <tr>
            <td class="view_head" style="height: 25px; vertical-align: bottom">
                <div style="width: 200px; height: 25px; background: url('../Images/TitleImgs/Title_back.gif');">
                    <span class="view_tab_header">档案信息</span>
                 </div>
            </td>
        </tr>
        <tr>
            <td class="view_center">
                <table cellspacing="1" cellpadding="0" width="100%" align="center" border="0" class="table">
                    <tr>
                        <td class="td_text" style="width: 15%;">
                            <span>档号</span></td>
                        <td class="td_value" style="width: 35%;">
                            <Bigdesk8:DBText ID="DocNum" ItemName="DocNum" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td_text" style="width: 15%;">
                            <span>案卷数</span></td>
                        <td class="td_value" style="width: 35%;">
                            <Bigdesk8:DBText ID="DocCount" ItemName="DocCount" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    
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
            //var zoomArr = [];
            //添加比例尺控件 
            map.addControl(scale);

            var x = $("[id$='gis_jd']").html();
            var y = $("[id$='gis_wd']").html();
            if (x != "" && y != "") {

                var lnglat = new TLngLat(parseFloat(x), parseFloat(y));
                //zoomArr.push(lnglat);
                marker = addOverLay(lnglat);
                //map.setViewport(zoomArr);
                map.centerAndZoom(lnglat, 15);
                map.enableScrollWheelZoom(); //滚轮放大缩小
            }

        }


        function addOverLay(lnglat) {

            //创建标注对象
            // var lnglat = new TLngLat(parseFloat(x), parseFloat(y))
            marker = new TMarker(lnglat);
            map.addOverLay(marker);
            //marker.setIconImage(url:String,size:TSize,anchor:Array);
            //if ($("[id$='db_IsSgbz']").html() == "1")
            //    marker.setIconImage('../../Common/images/marker2.png', new TSize(50, 50));
            //else
            marker.setIconImage('../../Common/images/marker.png', new TSize(20, 34));

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

        function uploadToStTBProjectAddInfo() {
            var prjnum = document.getElementById("PrjNum").innerHTML;
            $.ajax({
                type: 'POST',
                url: '/WxjzgcjczyPage/Handler/Data.ashx?type=uploadToStTBProjectAddInfo&PrjNum=' + prjnum,
                async: false,
                data: null,
                success: function (result) {
                    alert(result);
                }
            });
        }


        $(function () {

            window.onload = loadMap;

        });
    </script>
    
    </form>
</body>
</html>
