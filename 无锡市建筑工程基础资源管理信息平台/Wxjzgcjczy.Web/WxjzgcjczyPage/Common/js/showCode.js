function resize() {
    var width = 0;
    width = getTotalWidth();
    if (width < 600) {
        width = 600;
    }
    jQuery("#outter").width(width - 1);
    jQuery("#outter").height(getTotalHeight() - 1);
    var leftOffset = width / 2 - 46;
    jQuery("#codeButton1,#codeButton2").css({left: leftOffset});
}
function getTotalHeight() {
    if (jQuery.browser.msie) {
        return document.compatMode == "CSS1Compat" ? document.documentElement.clientHeight : document.body.clientHeight;
    }
    else {
        return self.innerHeight;
    }
}

function getTotalWidth() {
    if (jQuery.browser.msie) {
        return document.compatMode == "CSS1Compat" ? document.documentElement.clientWidth : document.body.clientWidth;
    }
    else {
        return self.innerWidth;
    }
}
jQuery(document).ready(function () {
    window.onresize = resize;
    resize();
    onLoad();
	jQuery("#controlButton").addClass("controlButtonstyle2");
    jQuery("#describediv").fadeIn();
    //将示例代码加载到页面中
    
    jQuery("#controlButton").click(function () {
        var a = jQuery("#controlButton").attr("class");
        if (a == "controlButtonstyle1") {
            jQuery("#controlButton").removeClass("controlButtonstyle1").addClass('controlButtonstyle2');
            jQuery("#describediv").fadeIn();
            jQuery("#close").fadeIn();
        } else {
            jQuery("#controlButton").removeClass("controlButtonstyle2").addClass('controlButtonstyle1');
            jQuery("#describediv").fadeOut();
            jQuery("#close").fadeOut();
        }

    });
    jQuery("#close").click(function () {
        jQuery("#controlButton").click();
    });

});
