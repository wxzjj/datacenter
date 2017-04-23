$(function() {
    //全局事件----按钮
    $(".textbox").focus(function() {
        $(this).addClass("textbox-focus");
    }).blur(function() {
        $(this).removeClass("textbox-focus");
    });


    //搜索框 收缩/展开
    $(".searchtitle .togglebtn").live('click', function() {
        if ($(this).hasClass("togglebtn-down")) $(this).removeClass("togglebtn-down");
        else $(this).addClass("togglebtn-down");
        //var searchbox = $(this).parent().nextAll(".searchbox");
        $(".searchbox").slideToggle('fast');
    });

    //编辑框 收缩/展开
    $(".edittitle .togglebtn").live('click', function() {
        if ($(this).hasClass("togglebtn-down")) $(this).removeClass("togglebtn-down");
        else $(this).addClass("togglebtn-down");
        var editname = $(this).attr("name");
        //获取td下的属性名称和togglebtn按钮所对应的名称一致的对象
        $("td:[name=" + editname + "]").slideToggle('fast');

    });
    //所有的按钮变成手型
    $(":button").css("cursor", "pointer");
    $(":submit").css("cursor", "pointer");

    //左侧菜单树 收缩/展开
    $(".menulist .togglebtn").live('click', function() {
        if ($(this).hasClass("togglebtn-down")) {
            $(this).removeClass("togglebtn-down");
        }
        else {
            $(this).addClass("togglebtn-down");
        }

        var editname = $(this).attr("name");
        //获取td下的属性名称和togglebtn按钮所对应的名称一致的对象
        $("div:[id=" + editname + "] li").slideToggle('fast');


    });
});