<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zzmc_Tree.aspx.cs" Inherits="Wxjzgcjczy.Web.WxjzgcjczyPage.Szqy.Zzmc_Tree" %>

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
                showIcon: true, //isShowIcon,
                fontCss: getFont
            },

            data: {
                simpleData: {
                    enable: true
                }
            },
            callback: {
                onClick: onClick,
                onDblClick: false
            }
        };
        function LoadSjmlZTree() {
          
            $.ajax({
                type: 'POST',
                url: '../Handler/Data.ashx?type=getZzmc',
                async: false,
                data: null,
                success: function(res) {
             
                    zNodes = eval('(' + res + ')');
                    $.fn.zTree.init($("#treeZzmc"), setting, zNodes);

                }
            });
        }

        function onClick(event, treeId, treeNode, clickFlag) {
            var winP = parent || window;
            var zTree = $.fn.zTree.getZTreeObj("treeZzmc");
            if (!treeNode.isParent) {
                winP.fSelectedZzmc(treeNode.id, treeNode.name);
            }
        }

        function getFont(treeId, node) {
            return node.font ? node.font : {};
        }

        $(document).ready(function() {
            window.setTimeout(LoadSjmlZTree, 10);
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
<body style="border: none;  margin:0; padding:0; width:190px; ">
    <ul id="treeZzmc" class="ztree" style="width: 100%; margin-top: 1px 0 0 0; background-color: transparent; 
        height: 350px; border: none;overflow:scroll;">
    </ul>
</body>
</html>
