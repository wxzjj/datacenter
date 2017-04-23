// 创建上传控件
function createBigFileUploadControl(xap, pluginID, parentID, initParams, backColor) {
    if (!Silverlight.isInstalled("3.0")) return;

    var altHTML = "<a href='javascript:Silverlight.getSilverlight(\"{1}\");' style='text-decoration: none;'>"
        + "<img src='{2}' alt='获取微软官方 Silverlight' style='border-style: none'/></a>";

    Silverlight.createObject(
    xap,
    document.getElementById(parentID),
    pluginID,
    {
        width: '100%',
        height: '100%',
        background: backColor,
        isWindowless: 'false',
        windowless: 'true',
        framerate: '24',
        version: '3.0',
        alt: altHTML
    },
    {
        onLoad: onSLLoad
    },
    initParams,
    "bigfileupload");
}

function onSLLoad(plugin, userContext, sender) {
    plugin.Content.BigFileUpload.UpFile_Begined = function (sender, beginedEventArgs) { uploadFile_Begined(sender, beginedEventArgs, plugin, userContext); };
    plugin.Content.BigFileUpload.UpFile_Progressing = function (sender, progressingEventArgs) { uploadFile_Progressing(sender, progressingEventArgs, plugin, userContext); };
    plugin.Content.BigFileUpload.UpFile_Cancelled = function (sender, eventArgs) { uploadFile_Cancelled(sender, eventArgs, plugin, userContext); };
    plugin.Content.BigFileUpload.UpFile_Completed = function (sender, completedEventArgs) { uploadFile_Completed(sender, completedEventArgs, plugin, userContext); };
    plugin.Content.BigFileUpload.UpFile_Deleted = function (sender, eventArgs) { uploadFile_Deleted(sender, eventArgs, plugin, userContext); };
}

// 当上传完成时执行
var onSLCompleted = function (sender, completedEventArgs, plugin, userContext) { };

// 开始上传事件--打开锁定界面--由Silverlight发起
function uploadFile_Begined(sender, beginedEventArgs, plugin, userContext) {
    $("#fileNameBindSpan").text(beginedEventArgs.FileNameBind);

    // 判断是否锁定界面
    if ($("#" + plugin.parentElement.id).attr("IsLockPage") != "false") {
        openLoading(plugin, userContext);
    }
}

// 上传过程中--显示进度--由Silverlight发起
function uploadFile_Progressing(sender, progressingEventArgs, plugin, userContext) {
    //    $("#PercentageSpan").text(progressingEventArgs.Percentage);
    $("#ProgressBindSpan").text(progressingEventArgs.ProgressBind);
    $("#SpeedBindSpan").text(progressingEventArgs.SpeedBind);
    $("#RemainingTimeBindSpan").text(progressingEventArgs.RemainingTimeBind);
}

// 取消上传后--关闭锁定界面--由Silverlight发起
function uploadFile_Cancelled(sender, eventArgs, plugin, userContext) {
    closeLoading();
}

// 上传成功时的事件--将文件信息写到界面上--关闭锁定界面--由Silverlight发起
function uploadFile_Completed(sender, completedEventArgs, plugin, userContext) {
    //completedEventArgs.FileID
    //completedEventArgs.FileName
    //completedEventArgs.FileSize
    closeLoading();

    //
    if (jQuery.isFunction(onSLCompleted)) {
        onSLCompleted(sender, completedEventArgs, plugin, userContext);
    }
}

//删除成功后--删除界面上的文件信息--由Silverlight发起
function uploadFile_Deleted(sender, eventArgs, plugin, userContext) {
}

// 取消正在上传的文件事件--由Javascript发起
function uploadFile_Cancel(plugin, userContext) {
    plugin.Content.BigFileUpload.UpFile_Cancel();
}

//----屏蔽界面功能---//
// 打开上传界面模式动画--锁定界面，不能操作其它按钮
function openLoading(plugin) {
    $("#CancelUpFileBtn").click(function () { uploadFile_Cancel(plugin); });
    $("#LoadingControl").show();
    setLoadingBackStyle();
    setLoadingStyle();
    $("#LoadingBack").fadeTo(100, 0.3);
    $("#Loading").fadeTo(100, 0.9);
}
// 判断上传界面--解锁界面，可以操作其它按钮
function closeLoading() {
    $("#LoadingControl").hide();
}
// 设定动画背景样式
function setLoadingBackStyle() {
    var bw = $(document.body).innerWidth();
    var ww = $(window).width();
    var bh = $(document.body).innerHeight();
    var wh = $(window).height();
    var w = ww > bw ? ww : bw;
    var h = wh > bh ? wh : bh;
    var css = { width: w + "px", height: h + "px", left: "0px", top: "0px" };
    $("#LoadingBack").css(css);
}
// 设定动画样式
function setLoadingStyle() {
    var w = 400, h = 150;
    var windowW = $(window).width();
    var windowH = $(window).height();
    var scrollLeft = $(document.documentElement).scrollLeft();
    var scrollTop = $(document.documentElement).scrollTop();
    var l = windowW < w ? 10 : scrollLeft + (windowW - w) / 2;
    var t = windowH < h ? 10 : scrollTop + (windowH - h) / 2;
    var css = { width: w + "px", height: h + "px", left: l + "px", top: t + "px" };
    $("#Loading").css(css);
}
// 初始化上传界面控件
function initLoadingControls() {
    var loadingBack = '<div id="LoadingBack" style="position: absolute; z-index: 999998; background-color: Gray"></div>';
    var loading = '<div id="Loading" style="position: absolute; z-index: 999999;background-color: White; padding: 10px; font-size: 12px; border: 1px solid black"><table cellspacing="5" cellpadding="0" width="100%" border="0"><tr><td style="width: 35%; text-align: right">文件名称(文件大小)：</td><td style="width: 65%; text-align: left"><span id="fileNameBindSpan"></span></td></tr><tr><td style="text-align: right">上传进度：</td><td style="text-align: left"><span id="ProgressBindSpan"></span></td></tr><tr><td style="text-align: right">上传速度：</td><td style="text-align: left"><span id="SpeedBindSpan"></span></td></tr><tr><td style="text-align: right">估计剩余时间：</td><td style="text-align: left"><span id="RemainingTimeBindSpan"></span></td></tr><tr><td colspan="2" style="text-align: center"><input type="button" id="CancelUpFileBtn" value="取消" style="width: 75px; height: 27px" /></td></tr></table></div>'

    $(document.body).append('<div id="LoadingControl"></div>');
    $("#LoadingControl").html(loadingBack + loading);
    $("#LoadingControl").hide();

    $(window).scroll(function (e) {
        setLoadingBackStyle();
        setLoadingStyle();
    }).resize(function () {
        setLoadingBackStyle();
        setLoadingStyle();
    });
}

// 文档加载完成，执行
$(function () {
    initLoadingControls();
});
