
$(function() {
    //全局事件----按钮
    $(".textbox").focus(function() {
        $(this).addClass("textbox-focus");
    }).blur(function() {
        $(this).removeClass("textbox-focus");
    });
});


// 获取地址栏的参数数组
function getUrlParams() {
    var search = window.location.search;
    // 写入数据字典
    var tmparray = search.substr(1, search.length).split("&");
    var paramsArray = new Array;
    if (tmparray != null) {
        for (var i = 0; i < tmparray.length; i++) {
            var reg = /[=|^==]/;    // 用=进行拆分，但不包括==
            var set1 = tmparray[i].replace(reg, '&');
            var tmpStr2 = set1.split('&');
            var array = new Array;
            array[tmpStr2[0]] = tmpStr2[1];
            paramsArray.push(array);
        }
    }
    // 将参数数组进行返回
    return paramsArray;
}

// 根据参数名称获取参数值
function getParamValue(name, isUnEncode) {
    var paramsArray = getUrlParams();
    if (paramsArray != null) {
        for (var i = 0; i < paramsArray.length; i++) {
            for (var j in paramsArray[i]) {
                if (j == name) {
                    var v = paramsArray[i][j];
                    if (isUnEncode) {
                        return unescape(v);
                    }
                    else {
                        return v;
                    }
                }
            }
        }
    }
    return null;
}

function getGridIsScroll() {
    return true;
}

function getGridHeight() {

    return window.document.documentElement.clientHeight - $("[class='l-form']").height() - 20;
}

function getContentHeight() {
    return window.document.documentElement.clientHeight;
}

function getTabHeight() {
    return document.body.clientHeight; //window.document.documentElement.clientHeight;
}

function getTabGridHeight() {
  
    return (window||parent.window).document.documentElement.clientHeight - $("[class='l-form']").height() - 60;
}

function setHeight(iframeIndex) {

    var h = getTabHeight() - 3;
  
    $("#Iframe" + iframeIndex).height(h - 33);
    $("#Iframe" + iframeIndex).parent("div").height(h - 30);
    $("#tab1").height(h);
}


function getSzgcToolbarContentHeight() {

    return window.document.documentElement.clientHeight - 37;
}



