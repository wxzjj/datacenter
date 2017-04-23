
//一级菜单动态背景效果(new)
function tdover(id) {
    var _id = "#" + id;
    $(_id).removeClass(id + "_0");
    $(_id).addClass(id + "_1");

}
function tdout(id) {
    if (document.getElementById("hd").value != id) {
        var _id = "#" + id;
        $(_id).removeClass(id + "_1");
        $(_id).addClass(id + "_0");
    }
}
function menuClick(id, tdid) {
    var _id = "#" + id;
    var a = document.getElementById("hd").value;
    var _a = "#" + a;
    if (a != "") {
        $(_a).removeClass(a + "_1");
        $(_a).addClass(a + "_0");
    }
    document.getElementById("hd").value = tdid;
    $(_id).removeClass(id + "_0");
    $(_id).addClass(id + "_1");
    tdout(tdid);
}


/* 一级菜单点击 */
function Loading(nodeID, id, tdid) {

    switch (nodeID) {
        case "00000000":
            //            top.MainIf.location = "MainPage_GLB3.aspx";
            top.MainIf.location = "Index_Gcxm.aspx"
            break;
        //工程项目                       
        case "10000000":
            top.MainIf.location = "Index_Gcxm.aspx"
            break;
        //市场主体                       
        case "20000000":
            top.MainIf.location = "Index_Szqy.aspx";
            break;
        //执业人员                       
        case "30000000":
            top.MainIf.location = "Index_Zyry.aspx";
            break;
        //信用体系
        case "xytx":
            top.MainIf.location = "Index_Xytx.aspx";
            //window.open("http://218.90.162.101:8088/EpointFrameZS_WXSZJS/WuxiCenter/Login.aspx?LoginGuid=e344a77c-a55a-4713-8266-3ffb97a2fc91");
            break;
        case "tdt":
            top.MainIf.location = "Index_tdt.aspx";
            break;   
            
        //跟踪预警                       
        case "40000000":
            top.MainIf.location = "Index_Gzyj.aspx";
            break;
        //统计分析                      
        case "50000000":
            top.MainIf.location = "Index_Tjfx.aspx";
            break;
        //决策辅助                        
        case "60000000":
            top.MainIf.location = "Index_Jcfz.aspx";
            break;
        //政令畅通                      
        case "70000000":
            top.MainIf.location = "Index_Zlct.aspx";
            break;
        case "xxgx":
            top.MainIf.location = "Index_xxgx.aspx";
            break;     
        case "yhgl":
            top.MainIf.location = "Index_yhgl.aspx";
            break;
        case "xxcj":
            top.MainIf.location = "Index_xxcj.aspx";
            break;   
        default:
            top.MainIf.location = "test3.html";
            break;
    }
    if (nodeID != "00000000")
        menuClick(id, tdid);
    return false;
}



//左边二级菜单动态背景效果(new)
function leftover(id) {
    var _id = "#" + id;
    $(_id).removeClass("left_icon_leave");
    $(_id).addClass("left_icon_over");

    var img = $(_id).find("img");
    if (img != null && img != undefined) {
        var src = img.attr("src");
        if (src != null && src != undefined)
            $(_id).find("img").attr("src", src.substr(0, src.length - 5) + "1.png");
    }

    var tdSpan = $(_id).parent().next("tr");
    if (tdSpan != null && tdSpan != undefined) {
        tdSpan = tdSpan.find("td");
        if (tdSpan != null && tdSpan != undefined) {
            tdSpan.removeClass("menuSpan");
            tdSpan.addClass("menuSpan_1");
        }
    }

}
function leftout(id) {
    if (document.getElementById("hd2").value != id) {
        var _id = "#" + id;

        $(_id).removeClass("left_icon_over");
        $(_id).addClass("left_icon_leave");

        var img = $(_id).find("img");
        if (img != null && img != undefined) {
            var src = img.attr("src");
            if (src != null && src != undefined)
                $(_id).find("img").attr("src", src.substr(0, src.length - 5) + "0.png");
        }

        var tdSpan = $(_id).parent().next("tr");
        if (tdSpan != null && tdSpan != undefined) {
            tdSpan = tdSpan.find("td");
            if (tdSpan != null && tdSpan != undefined)
                tdSpan.removeClass("menuSpan_1").addClass("menuSpan");
        }
    }
}
function leftmenuClick(id) {
    
    var _id = "#" + id;
    var a = document.getElementById("hd2").value;
    var _a = "#" + a;
    if (a != "") {
        $(_a).removeClass("left_icon_over");
        $(_a).addClass("left_icon_leave");

        var img = $(_a).find("img");
        if (img != null && img != undefined) {
            var src = img.attr("src");
            if (src != null && src != undefined)
                $(_a).find("img").attr("src", src.substr(0, src.length - 5) + "0.png");
        }

        var tdSpan = $(_a).parent().next("tr");
        if (tdSpan != null && tdSpan != undefined) {
            tdSpan = tdSpan.find("td");
            if (tdSpan != null && tdSpan != undefined)
                tdSpan.removeClass("menuSpan_1").addClass("menuSpan");
        }

    }

    $(_id).removeClass("left_icon_leave");
    $(_id).addClass("left_icon_over");

    var img = $(_id).find("img");
    if (img != null && img != undefined) {
        var src = img.attr("src");
        if (src != null && src != undefined)
            $(_id).find("img").attr("src", src.substr(0, src.length - 5) + "1.png");
    }

    var tdSpan = $(_id).parent().next("tr");
    if (tdSpan != null && tdSpan != undefined) {
        tdSpan = tdSpan.find("td");
        if (tdSpan != null && tdSpan != undefined)
            tdSpan.removeClass("menuSpan").addClass("menuSpan_1");
    }

    document.getElementById("hd2").value = id;
    //leftout(id);
}


/* 左侧二级菜单点击 */
function leftclick(nodeID, id, type) {
    switch (nodeID) {


//        //工程项目                  
//        case "01010000":
//            ItemIf.location = "../Szgc/Aqjd_List.aspx";
//            break;
//        case "01020000":
//            ItemIf.location = "../Szgc/Zljd_List.aspx";
//            break;
//        case "01030000":
//            ItemIf.location = "../Szgc/Sgxk_List.aspx";
//            break;
//        case "01040000":
//            ItemIf.location = "../Szgc/Gcxm_List.aspx";
//            break;
//        case "01050000":
//            ItemIf.location = "../Szgc/Lxxm_List.aspx";
//            break;
//        case "01060000":
//            ItemIf.location = "/IntegrativeShow2/SysFiles/PagesZhjg/Zhjg_Menu.aspx";
//            break;


//        case "gcxm_zbtb":
//            ItemIf.location = "../Gcxm/Zbtb_Menu.aspx?menu=0";
//            break;

        //工程项目  

        case "01010000":
            ItemIf.location = "/IntegrativeShow2/SysFiles/PagesZhjg/Zhjg_Lxxmdj_List.aspx";
            break;
        case "01020000":
            ItemIf.location = "../Gcxm/Kcsjht_List.aspx";
            break;
        case "01030000":
            ItemIf.location = "/IntegrativeShow2/SysFiles/PagesZhjg/Zhjg_Sgtsc_List.aspx?BeFrom=Zhjg_Menu";
            break;
        case "01040000":
            ItemIf.location = "/IntegrativeShow2/SysFiles/PagesZhjg/Zhjg_Zbtb_List.aspx?BeFrom=Zhjg_Menu";
            break;
        case "01050000":
            //ItemIf.location = "../Gcxm/Sgjlht_List.aspx";
            ItemIf.location = "/IntegrativeShow2/SysFiles/PagesZhjg/Zhjg_Htba_List.aspx?BeFrom=Zhjg_Menu";
            break;
        case "01060000":
            ItemIf.location = "/IntegrativeShow2/SysFiles/PagesZhjg/Zhjg_Aqbj_List.aspx?BeFrom=Zhjg_Menu";
            break;
        case "01070000":
            ItemIf.location = "/IntegrativeShow2/SysFiles/PagesZhjg/Zhjg_Zlbj_List.aspx?BeFrom=Zhjg_Menu";
            break;
        case "01080000":
            ItemIf.location = "/IntegrativeShow2/SysFiles/PagesZhjg/Zhjg_Sgxkz_List.aspx?BeFrom=Zhjg_Menu";
            break;
        case "01090000":
            ItemIf.location = "/IntegrativeShow2/SysFiles/PagesZhjg/Zhjg_Jgba_List.aspx?BeFrom=Zhjg_Menu";
            break;
            

        //市场主体          
        case "02010000":
            ItemIf.location = "../Szqy/Jsdw_List.aspx";
            break;
        case "02020000":
            ItemIf.location = "../Szqy/Kcdw_List.aspx";
            break;
        case "02030000":
            ItemIf.location = "../Szqy/Sgdw_List.aspx";
            break;
        case "02040000":
            ItemIf.location = "../Szqy/Zjjg_List.aspx";
            break;
        case "02050000":
            ItemIf.location = "../Szqy/Qtdw_List.aspx";
            break;
   case "02060000":
            ItemIf.location = "../Szqy/Sjdw_List.aspx";
            break;

        //执业人员                    
        case "03010000":
            ItemIf.location = "../Zyry/Zczyry_List.aspx";
            break;
        case "03020000":
            ItemIf.location = "../Zyry/Aqscglry_List.aspx";
            break;
        case "03030000":
            ItemIf.location = "../Zyry/Qyjjry_List.aspx";
            break;
        case "03040000":
            ItemIf.location = "../Zyry/Zygwglry_List.aspx";
            break;
            
        //信用体系 
        case "xytx_xykp":
            ItemIf.location = "../Xytx/Xykp_Toolbar.aspx";
            break;
        case "xytx_xzcf":
            ItemIf.location = "../Xytx/Xzcf_List.aspx";
            break;
        case "xytx_pypj":
            ItemIf.location = "../Xytx/Pypj_List.aspx";
            break;
            
        //天地图
        case "tdt_1":
            ItemIf.location = "../Tdt/LocationView.aspx";
            break;
        case "tdt_2":
            ItemIf.location = "../Tdt/XmLocation_List.aspx";
            break;

        //跟踪预警                     
        case "04010000":
            ItemIf.location = "../Gzyj/Qyzsgq_List.aspx";
            break;
        case "04020000":
            ItemIf.location = "../Gzyj/Ryzsgq_List.aspx";
            break;
        case "04030000":
            ItemIf.location = "../Gzyj/Zjgyxm_List.aspx";
            break;
        case "04040000":
            ItemIf.location = "../Gzyj/Wbsgxkz_List.aspx";
            break;
        case "04050000":
            ItemIf.location = "../Gzyj/Gcxmbg_List.aspx";
            break;
        case "04060000":
            ItemIf.location = "../Gzyj/Wbzljd_List.aspx";
            break;
        case "04070000":
            ItemIf.location = "../Gzyj/Wbaj_List.aspx";
            break;
        case "04080000":
            ItemIf.location = "../Gzyj/JgbaLcyj_List.aspx";
            break;
            
        case "gzyj":
            ItemIf.location = "../MainPage/MainPage_Gzyj.aspx";
            break;

        //统计分析              
               case "tjfx_1":
                   ItemIf.location = "../Tjfx/Tjfx_Toolbar.aspx";
                   break;
               case "tjfx_2":
                   ItemIf.location = "../Report/Htba_Toolbar.aspx";
                   break;
               case "tjfx":
                   ItemIf.location = "../MainPage/MainPage_Tjfx.aspx";
                   break;                

        //决策辅助                    
        case "06010000":
            ItemIf.location = "../Jcfz/ToolBar_Jctz.aspx";
            break;
        case "06020000":
            ItemIf.location = "../Jcfz/ToolBar_Tjfx.aspx";
            break;
        case "06030000":
            ItemIf.location = "../Jcfz/Yjjc_List.aspx";
            break;

        //政令畅通                        
        case "07010000":
            ItemIf.location = "../Zlct/Gzzs_Toolbar.aspx";
            break;
        case "07020000":
            ItemIf.location = "../../../MajordomoMVC/OnlineOffice/Index";
            break;
        case "07030000":
            ItemIf.location = "../Zlct/Dxjb_Toolbar.aspx";
            break;

        case "08010000":
            ItemIf.location = "../Szyh/Yhgc_List.aspx";
            break;
        case "08020000":
            ItemIf.location = "../Szyh/Yhjl_List.aspx";
            break;
        case "yhgl_1"://用户信息
            ItemIf.location = "../Yhgl/Yhxx_List.aspx";
            break;
        case "xxcj_ajxx": //信息采集-安监信息 
            ItemIf.location = "../Xxcj/Ajxx_List.aspx";
            break;
        case "xxcj_zjxx": //信息采集-质监信息 
            ItemIf.location = "../Xxcj/Zjxx_List.aspx";
            break;

        //信息共享
//        case "xxgx_csjk":
//            ItemIf.location = "../Xxgx/DataTransmissionMonitor.aspx";
//            break; 
        default:
            top.MainIf.location = "";
            break;
    }
    if (id!=undefined && id!= "") {
        leftmenuClick(id);
    }
    $(document).scrollTop(0);
    return false;
}

//function leftclick2(nodeID, id, state, ssdq, type) {
//    switch (nodeID) {


//        //工程项目           
//        case "01010000":
//            ItemIf.location = "../Szgc/Aqjd_List.aspx?state=" + state + "&ssdq=" + ssdq;
//            break;
//        case "01020000":
//            ItemIf.location = "../Szgc/Zljd_List.aspx";
//            break;
//        case "01030000":
//            ItemIf.location = "../Szgc/Sgxk_List.aspx";
//            break;
//        case "01040000":
//            ItemIf.location = "../Szgc/Gcxm_List.aspx";
//            break;
//        case "01050000":
//            ItemIf.location = "../Szgc/Lxxm_List.aspx";
//            break;
//        case "01060000":
//            ItemIf.location = "/IntegrativeShow2/SysFiles/PagesZhjg/Zhjg_Menu.aspx";
//            break;
//        default:
//            top.MainIf.location = "";
//            break;
//    }

//    leftmenuClick(id);
//    $(document).scrollTop(0);
//    return false;
//}


function leftclick3(nodeID, id, dwlx, type) {
    switch (nodeID) {

        //市场主体        
        case "02010000":
            ItemIf.location = "../Szqy/Jsdw_List.aspx?dwlx=" + dwlx;
            break;
        case "02020000":
            ItemIf.location = "../Szqy/Sjdw_List.aspx?dwlx=" + dwlx;
            break;
        case "02030000":
            ItemIf.location = "../Szqy/Sgdw_List.aspx?dwlx=" + dwlx;
            break;
        case "02040000":
            ItemIf.location = "../Szqy/Zjjg_List.aspx?dwlx=" + dwlx;
            break;
        case "02050000":
            ItemIf.location = "../Szqy/Qtdw_List.aspx?dwlx=" + dwlx;
            break;

        default:
            top.MainIf.location = "";
            break;
    }
    leftmenuClick(id);
    $(document).scrollTop(0);
    return false;
}

function leftclick4(nodeID, id, zyry, rylx, type) {
    switch (nodeID) {

        //执业人员  
        case "03010000":
            ItemIf.location = "../Zyry/Zczyry_List.aspx?zyry=" + zyry + "&rylx=" + rylx;
            break;
        case "03020000":
            ItemIf.location = "../Zyry/Aqscglry_List.aspx?zyry=" + zyry + "&rylx=" + rylx;
            break;
        case "03030000":
            ItemIf.location = "../Zyry/Qyjjry_List.aspx?zyry=" + zyry + "&rylx=" + rylx;
            break;
        case "03040000":
            ItemIf.location = "../Zyry/Zygwglry_List.aspx?zyry=" + zyry + "&rylx=" + rylx;
            break;

        default:
            top.MainIf.location = "";
            break;
    }
    leftmenuClick(id);
    $(document).scrollTop(0);
    return false;
}


$(document).ready(function() {
    $(window).scroll(function() {
        nowtop = parseInt($(document).scrollTop());
        if (nowtop >= 0) {
            $('#leftmenu').css('top', nowtop + 'px');
        }
        else {

            $('#leftmenu').css('top', '100px');
        }
    });

    $('#toTop').click(function() {
        $(document).scrollTop(0);
    });

});

function itemIFheight(pageheight, canseeheigth) {
    if (pageheight > canseeheigth) {
        return pageheight;
    }
    else {
        return canseeheigth;
    }
}




