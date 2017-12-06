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
    //将示例代码加载到页面中
    var s = document.location.href.split('/');
    var str = s[s.length - 1];
    var re = "";
    jQuery.ajax({
        async: false,
        url: "../APIExample/" + str,
        success: function (result) {
            re = result;
        }
    });
    jQuery('#code').text(re);
    SyntaxHighlighter.all('code');
    var clip = new ZeroClipboard(jQuery("#copy-button"), {
        moviePath: "../swf/ZeroClipboard.swf"
    });
    jQuery("#copy-button").css({"z-index": "-1"});
    jQuery("#codeButton0").addClass("codeButton0style1");
    jQuery("#controlButton").addClass("controlButtonstyle2");
    jQuery("#describediv").fadeIn();
    jQuery("#close").fadeIn();
    jQuery('#codeContainer').mouseover(function () {
        jQuery(".toolbar").css({display: "none"});
    });
    jQuery("#codeButton1").click(function () {
        jQuery("#codeButton1").fadeOut();
        jQuery("#codeContainer").slideDown("slow", function () {
            jQuery("#codeButton2").fadeIn();
            jQuery("#copy-button").css({"z-index": "1000"});
        });
        jQuery("#codeButton0").removeClass("codeButton0style1").addClass('codeButton0style2');

    });
    jQuery("#codeButton2").click(function () {
        jQuery("#codeButton2").fadeOut();
        jQuery("#copy-button").css({"z-index": "-1"});
        jQuery("#codeContainer").slideUp("slow", function () {
            jQuery("#codeButton1").fadeIn();
        });
        jQuery("#codeButton0").removeClass("codeButton0style2").addClass('codeButton0style1');
    });
    jQuery("#codeButton0").click(function () {
        var a = jQuery("#codeButton0").attr("class");
        if (a == "codeButton0style1") {
            jQuery("#codeButton1").click();
            jQuery("#codeButton0").removeClass("codeButton0style1").addClass('codeButton0style2');
        } else {
            jQuery("#codeButton2").click();
            jQuery("#codeButton0").removeClass("codeButton0style2").addClass('codeButton0style1');
        }
    });
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
