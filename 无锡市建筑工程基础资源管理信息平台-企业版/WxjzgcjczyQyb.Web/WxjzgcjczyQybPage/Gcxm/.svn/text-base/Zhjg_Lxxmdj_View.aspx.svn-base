<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zhjg_Lxxmdj_View.aspx.cs"
    Inherits="WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Gcxm.Zhjg_Lxxmdj_View" %>

<%@ Register Assembly="Bigdesk8" Namespace="Bigdesk8.Web.Controls" TagPrefix="Bigdesk8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../App_Themes/WxjzgcjczyQyb_B_Theme/base.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/WxjzgcjczyQyb_B_Theme/main.css" rel="stylesheet" type="text/css" />

    <script src="../Common/scripts/jquery-1.7.1.min.js" type="text/javascript"></script>

    <script language="javascript" src="http://api.tianditu.com/js/maps.js" type="text/javascript"></script>

    <style type="text/css">
        .sureCss
        {
            padding: 0;
            margin: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table id="RwjbxxTab" cellspacing="0" cellpadding="1" width="100%" border="0">
        <tr>
            <td class="editpanel">
                <div class="edittitle">
                    工程基本信息<img alt="" src="../Common/images/clipboard.png" width="16" height="16" />
                    <!-- name="edit3"  要和下面的td中的一致-->
                </div>
                <div class="navline" style="margin-bottom: 4px; margin-top: 4px;">
                </div>
            </td>
        </tr>
      <tr>
            <td class="editbox" name="edit3">
                <table id="Table1" width="100%" border="0" cellspacing="1" cellpadding="0" class="table-bk"
                    runat="server">
                    <tr>
                        <td class="td-text" width="15%">
                            项目编号
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="txtPrjNum" ItemName="PrjNum" runat="server"></Bigdesk8:DBText>
                            <div style="display: none;">
                                <Bigdesk8:DBText ID="db_IsSgbz" ItemName="IsSgbz" runat="server"></Bigdesk8:DBText>
                            </div>
                        </td>
                        <td class="td-value" colspan="2" rowspan="20">
                            <div id="mapTypeStyle" style="position: absolute; left: 90%;">
                                <select id="mapTypeSelect" onchange="switchingMapType(this);">
                                    <option value="TMAP_NORMAL_MAP">地图</option>
                                    <option value="TMAP_SATELLITE_MAP">卫星</option>
                                    <option value="TMAP_HYBRID_MAP">卫星混合</option>
                                    <option value="TMAP_TERRAIN_MAP">地形</option>
                                    <option value="TMAP_TERRAIN_HYBRID_MAP">地形混合</option>
                                </select>
                            </div>
                            <div id="map" style="width: 100%; height: 550px;">
                            </div>
                        </td>
                    </tr> 
                    <tr>
                        <td class="td-text" width="15%">
                            行政审批编号
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="PrjInnerNum" ItemName="PrjInnerNum" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            立项文号
                        </td>
                        <td class="td-value">
                            <Bigdesk8:DBText ID="PrjApprovalNum" ItemName="PrjApprovalNum" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            项目名称
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="PrjName" ItemName="PrjName" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            项目分类
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="PrjType" ItemName="PrjType" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            工程用途
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="PrjFunction" ItemName="PrjFunction" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            建设规模
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="PrjSize" ItemName="PrjSize" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            建设性质
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="PrjProperty" ItemName="PrjProperty" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr> 
                    <tr>
                        <td class="td-text" width="15%">
                            建设单位名称
                        </td>
                        <td class="td-value" width="35%">
                           
                            <asp:HyperLink ID="hlk_Jsdw" runat="server" Target="_blank"></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            建设单位组织机构代码<br />
                            （统一社会信用代码）
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="BuildCorpCode" ItemName="BuildCorpCode" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            项目所在省
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="Province" ItemName="Province" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            项目所在地市
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="City" ItemName="City" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            项目所在区县
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="County" ItemName="County" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            立项级别
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="PrjApprovalLevelNum" ItemName="PrjApprovalLevel" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            经度
                        </td>
                        <td class="td-value" width="35%" style="padding-bottom: 2px;">
                            <Bigdesk8:DBText ID="gis_jd" ItemName="jd" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            纬度
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="gis_wd" ItemName="wd" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            建设用地规划许可证编号
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="BuldPlanNum" ItemName="BuldPlanNum" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            建设工程规划许可证编号
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="ProjectPlanNum" ItemName="ProjectPlanNum" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            项目金额/总投资（万元）
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="AllInvest" ItemName="AllInvest" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" width="15%">
                            总面积（平方米）
                        </td>
                        <td class="td-value" width="35%">
                            <Bigdesk8:DBText ID="AllArea" ItemName="AllArea" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text">
                            单项项目列表
                        </td>
                        <td class="td-value" colspan="3">
                            <asp:GridView ID="Gdv_LxxmDxgcInfo" runat="server" AllowPaging="false" AutoGenerateColumns="false"
                                BorderWidth="1px" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="项目名称">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink_View" runat="server" Text='<%#Eval("xmmc") %>' NavigateUrl='<%#string.Format("Zhjg_Lxxmdj_Dxgc_View.aspx?PKID={0}",Eval("PKID")) %>'
                                                Target="_blank" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                                        <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="单项项目分类" DataField="gclb">
                                        <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                                        <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="结构类型" DataField="jglx">
                                        <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                                        <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="工程用途" DataField="jsyt">
                                        <HeaderStyle CssClass="pdg-headerstyle-center"></HeaderStyle>
                                        <ItemStyle CssClass="pdg-itemstyle-center" Width="2%"></ItemStyle>
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
        <!-- 招标投标基本信息 -->
        <tr>
            <td class="editpanel">
                <div class="edittitle">
                    统计信息<img alt="" src="../Common/images/clipboard.png" width="16" height="16" />
                    <!-- name="edit3"  要和下面的td中的一致-->
                </div>
                <div class="navline" style="margin-bottom: 4px; margin-top: 4px;">
                </div>
            </td>
        </tr>
        <tr>
            <td class="editbox" name="edit3">
                <table id="Table2" width="100%" border="0" cellspacing="1" cellpadding="0" class="table-bk"
                    runat="server">
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            单项工程数
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="DBLabel8" ItemName="DxgcCount" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td-text" style="width: 15%;">
                            施工图审查数
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="DBLabel1" ItemName="SgtscCount" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            招投标信息数
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="DBLabel2" ItemName="ZtbxxCount" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td-text" style="width: 15%;">
                            合同备案数
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="DBLabel3" ItemName="HtbaCount" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            安全报监数
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="DBLabel4" ItemName="AqbjCount" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td-text" style="width: 15%;">
                            质量报监数
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="DBLabel5" ItemName="ZlbjCount" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-text" style="width: 15%;">
                            施工许可数
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="DBLabel6" ItemName="SgxkCount" runat="server"></Bigdesk8:DBText>
                        </td>
                        <td class="td-text" style="width: 15%;">
                            竣工备案数
                        </td>
                        <td class="td-value" style="width: 35%;">
                            <Bigdesk8:DBText ID="DBLabel7" ItemName="JgbaCount" runat="server"></Bigdesk8:DBText>
                        </td>
                    </tr>
                </table>
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
            //添加比例尺控件 
            map.addControl(scale);

            var x = $("[id$='gis_jd']").html();
            var y = $("[id$='gis_wd']").html();

            if (x != "" && y != "") {

                var lnglat = new TLngLat(parseFloat(x), parseFloat(y));
                marker = addOverLay(lnglat);
            }

        }


        function addOverLay(lnglat) {

            //创建标注对象
            // var lnglat = new TLngLat(parseFloat(x), parseFloat(y))
            marker = new TMarker(lnglat);
            map.addOverLay(marker);
            //marker.setIconImage(url:String,size:TSize,anchor:Array);
            if ($("[id$='db_IsSgbz']").html() == "1")
                marker.setIconImage('../../Common/images/marker2.png', new TSize(50, 50));
            else
                marker.setIconImage('../../Common/images/marker.png', new TSize(50, 50));

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


        $(function() {

            window.onload = loadMap;

        });
    </script>

    </form>
</body>
</html>
