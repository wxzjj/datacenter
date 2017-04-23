<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Txl_Tree.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Zlct.Txl_Tree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <%--   <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />--%>
    <link rel="stylesheet" type="text/css" href="../../zTree-v3.5.14/css/demo.css" />
    <link rel="stylesheet" type="text/css" href="../../zTree-v3.5.14/css/zTreeStyle/zTreeStyle.css" />

    <script type="text/javascript" src="../../zTree-v3.5.14/js/jquery-1.4.4.min.js"></script>

    <script type="text/javascript" src="../../zTree-v3.5.14/js/jquery.ztree.core-3.5.min.js"></script>

    <script type="text/javascript" src="../../zTree-v3.5.14/js/jquery.ztree.excheck-3.5.min.js"></script>

    <script src="../Common/scripts/Common.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">

        var type = getParamValue("type");
        var gzzsId = getParamValue("gzzsId");
        var dxjbId = getParamValue("dxjbId");
        var setting = {
            view: {
                showIcon: isShowIcon,
                fontCss: getFont
            },
            check: {
                enable: true,
                autoCheckTrigger: true,
                chkStyle: "checkbox"
            },
            data: {
                simpleData: {
                    enable: true
                }
            },
            callback: {
                onDblClick: false,
                beforeCheck: beforeCheck,
                onCheck: onChecked,
                beforeClick: beforeClicked
            }
        };
        var zNodes = "";
        function LoadSjmlZTree() {

            if (type == "gzzs") {
                $.ajax({
                    type: 'POST',
                    url: '../Handler/Data.ashx?type=getTxlTree&gzzsId=' + gzzsId,
                    async: false,
                    data: null,
                    success: function(res) {
                        closeBg();
                        zNodes = eval('(' + res + ')');
                        $.fn.zTree.init($("#treeTxl"), setting, zNodes);

                    }
                });
            }
            else
                if (type == "dxjb") {
                $.ajax({
                    type: 'POST',
                    url: '../Handler/Data.ashx?type=getTxlTree_Dxjb&dxjbId=' + dxjbId,
                    async: false,
                    data: null,
                    success: function(res) {
                        closeBg();
                        zNodes = eval('(' + res + ')');
                        $.fn.zTree.init($("#treeTxl"), setting, zNodes);

                    }
                });
            }
        }

        function isShowIcon(treeId, treeNode) {

            return true; // treeNode.isParent;
        }

        function getFont(treeId, node) {
            return node.font ? node.font : {};
        }

        function beforeCheck(treeId, treeNode, clickFlag) {

            var zTree = $.fn.zTree.getZTreeObj("treeTxl");

            if (!treeNode.isParent) {
                var name = treeNode.name;

                var number = name.substring(name.indexOf('【') + 1, name.indexOf('】'));
                if (number == null || number == "" || number == "undefined") {

                    if (window.parent.location.href.indexOf("Dxjb_Send.aspx") >= 0) {
                        if (!treeNode.checked) {
                            parentAlert("该联系人没有手机号码，无法发送<br/>短信息！");
                            return false;
                        }
                    }
                }
            }
            return true;
        }


        function beforeClicked(treeId, treeNode, clickFlag) {
            var zTree = $.fn.zTree.getZTreeObj("treeTxl");
            if (treeNode.isParent) {
                zTree.expandNode(treeNode, null, null, null, null);
            }

            return false;
        }


        function onChecked(event, treeId, treeNode) {
            var winP = parent || window;
            var zTree = $.fn.zTree.getZTreeObj("treeTxl");
            if (!treeNode.isParent) {
                if (treeNode.checked) {
                    winP.fSelectedSjr(treeNode.id, treeNode.name);
                }
                else {
                    fCanbeDelete(treeNode.id);
                }
            }
        }

        function fCanbeDelete(code) {
            /*  此处方法，解决了重复添加、删除问题。
            提出了一种思想：
            定义一个用来承载判断结果true或false的变量，
            做操作前，先判断是否要进行该操作，将判断结果赋值给该变量，
            然后变量为true则进行，false则取消*/
            //找到所有被Checked的节点
            var treeObj = $.fn.zTree.getZTreeObj("treeTxl");
            var oCheckedNodes = treeObj.getCheckedNodes(true);
            var bCanDelete = true;

            for (var i = 0; i < oCheckedNodes.length; i++) {
                /*  如果已勾选的节点ID与要删除的节点ID相等，
                那就说明有同一个人被重复勾选，
                且此时该人员还有被勾选的节点，
                则判断它不能删除。 */
                if (oCheckedNodes[i].checked) {
                    if (!oCheckedNodes[i].isParent) {
                        if (oCheckedNodes[i].id === code) {
                            bCanDelete = false; //给CanDelete 赋值为false
                            break;
                        }
                    }
                }
            }
            var winP = parent || window;
            return winP.fDeleteSjr(code, bCanDelete);
        }
        function parentAlert(msg) {
            var winP = parent || window;
            if (winP.showMessage != "undefined")
                return winP.showWarn(msg);

        }
        //显示灰色JS遮罩层
        function showBg() {
            var ct = "dialog";
            var bH = $(document).height() + 30;
            var bW = $(document).width() + 16;
            var objWH = getObjWh(ct);

            var tbT = objWH.split("|")[0] + "px";
            var tbL = objWH.split("|")[1] + "px";

            $("#fullbg").css({ width: bW, height: bH, display: "block" });
            $("#" + ct).css({ top: tbT, left: tbL, display: "block" });
            $(window).scroll(function() { resetBg() });
            $(window).resize(function() { resetBg() });

            window.setTimeout(LoadSjmlZTree, 10);
        }

        function getObjWh(obj) {

            var st = document.documentElement.scrollTop; //滚动条距顶部的距离
            var sl = document.documentElement.scrollLeft; //滚动条距左边的距离
            var ch = document.documentElement.clientHeight; //屏幕的高度
            var cw = document.documentElement.clientWidth; //屏幕的宽度
            var objH = $("#" + obj).height(); //浮动对象的高度
            var objW = $("#" + obj).width(); //浮动对象的宽度
            var objT = Number(st) + (Number(ch) - Number(objH)) / 2;
            var objL = Number(sl) + (Number(cw) - Number(objW)) / 2;

            return objT + "|" + objL;
        }
        function resetBg() {
            var fullbg = $("#fullbg").css("display");
            if (fullbg == "block") {
                var bH2 = $(document).height();
                var bW2 = $(document).width();
                $("#fullbg").css({ width: bW2, height: bH2 });
                var objV = getObjWh("dialog");
                var tbT = objV.split("|")[0] + "px";
                var tbL = objV.split("|")[1] + "px";
                $("#dialog").css({ top: (tbT - 100), left: tbL });
            }
        }

        //关闭灰色JS遮罩层和操作窗口
        function closeBg() {
            $("#fullbg").css("display", "none");
            $("#dialog").css("display", "none");
        }

        $(document).ready(function() {
            showBg();
        });
    </script>

    <style type="text/css">
        body
        {
            color: #000; /* MAIN BODY TEXT COLOR */
            font-family: "Lucida Grande" , "Lucida Sans Unicode" ,Arial,Verdana,sans-serif;
            font-size: 16px;
        }
        #fullbg
        {
            background-color: #33393C;
            display: none;
            z-index: 1000;
            position: absolute;
            left: 0px;
            top: 0px;
            filter: Alpha(Opacity=40);
            -moz-opacity: 0.4;
            opacity: 0.4;
        }
        #dialog
        {
            position: absolute;
            width: 200px;
            height: 40px;
            background: #EFEFEF;
            display: none;
            z-index: 1005;
            vertical-align: middle;
            text-align: left;
        }
    </style>
</head>
<body style="border: none;">
    <ul id="treeTxl" class="ztree" style="width: 95%; margin-top: 1px; background-color: transparent;
        height: 430px; border: none;">
    </ul>
    <!-- JS遮罩层 -->
    <div id="fullbg">
    </div>
    <!-- end JS遮罩层 -->
    <!-- JS遮罩层上方的对话框 -->
    <div id="dialog">
        <div style="text-align: center; width: 100%; height: 100%; padding-top: 7px;">
            <img src="../../jquery-easyui-1.3.0/themes/gray/images/panel_loading.gif" />
            收件人列表加载中...</div>
    </div>
</body>
</html>
