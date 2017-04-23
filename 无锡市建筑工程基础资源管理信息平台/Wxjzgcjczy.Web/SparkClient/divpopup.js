this.screenshotPreview = function() {

    /*对于框架结构的网页，还需要再调整*/

    var sourceDiv = "td.sourceclass"; //鼠标悬停容器
    var sourceDiv_PopupAttrName = "popupid"; //鼠标悬停容器上的属性名，属性值为显示容器Id
    var popupDiv = "div.popupclass";

    $(sourceDiv).hover(function(e) {

        var o = $("#" + $(this).attr(sourceDiv_PopupAttrName));

        var otopY = e.pageY;
        var oleftX = e.pageX;

        var owidth = o.width();
        var oheight = o.height();

        var clientWidth = document.body.clientWidth;
        var clientHeight = document.body.clientHeight;

        var scrollTop = document.body.scrollTop;
        var scrollLeft = document.body.scrollLeft;

        if (oleftX + owidth - scrollLeft + 20 > clientWidth)
            oleftX = scrollLeft + clientWidth - owidth - 20;
        else
            oleftX = oleftX + 10;

        if (otopY + oheight - scrollTop + 20 > clientHeight)
            otopY = scrollTop + clientHeight - oheight - 20;
        else
            otopY = otopY + 10;

        o.css("top", otopY + "px")
            .css("left", oleftX + "px")
            .css("display", "block")
            .css("position", "absolute")
            .fadeTo("slow", 0.85);
    },
    function() {
        $("#" + $(this).attr(sourceDiv_PopupAttrName)).css("display", "none");
    });

    $(popupDiv).hover(function(e) {
        $(this).css("display", "block");
    },
    function() {
        $(this).css("display", "none");
    });
};

$(function() {
    screenshotPreview();
});
