


/* 返回首页（new） */
function home() {
    top.leftFrame.location = "LeftFrame.aspx?parentNodeID=10000000";
    top.rightFrame.location = "RightFrame.aspx?url=WorkArea.aspx?MenuText=我的工作台";
    
    return false;
}
//这种写法可以解决webkit内核浏览器的兼容性问题
function opFrameset() {
    var fs = parent.document.getElementsByTagName("frameset")[2];
    //alert(fs.cols);
    if (fs.cols == "30,5,*") {
        fs.cols = "147,5,*";
        //$("#dhoperate").html("隐藏导航栏");
        $("#table_show").css("display", "block");
        $("#table_hidden").css("display", "none");
        $("#img_left").css("display", "block");
        $("#img_right").css("display", "none");
    }
    else {
        fs.cols = "30,5,*";
        //$("#dhoperate").html("显示导航栏");
        $("#table_hidden").css("display", "block");
        $("#table_show").css("display", "none");
        $("#img_left").css("display", "none");
        $("#img_right").css("display", "block");
    }
}



