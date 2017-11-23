function resize() {
    var width = 0;
    width = getTotalWidth();
    if (width < 600) {
        width = 600;
    }
    jQuery("#outter").width(width - 1);
    jQuery("#outter").height(getTotalHeight() - 30);

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

    jQuery("#codeButton0").addClass("codeButton0style1");

    jQuery("#describediv").fadeOut();

    jQuery("#codeButton0").click(function () {
        var a = jQuery("#codeButton0").attr("class");
        if (a == "codeButton0style1") {
            jQuery("#codeButton0").removeClass("codeButton0style1").addClass('codeButton0style2');
            jQuery("#describediv").fadeIn();
        } else {
            jQuery("#codeButton0").removeClass("codeButton0style2").addClass('codeButton0style1');
            jQuery("#describediv").fadeOut();
        }
    });


});
