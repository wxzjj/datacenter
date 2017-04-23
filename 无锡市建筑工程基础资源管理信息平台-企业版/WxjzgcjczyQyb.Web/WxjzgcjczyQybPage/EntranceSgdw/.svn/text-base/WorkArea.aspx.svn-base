<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkArea.aspx.cs" Inherits="WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Entrance.WorkArea" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script src="../Common/jquery-easyui-1.3.3/jquery-1.8.0.min.js" type="text/javascript"></script>

    <link href="../../App_Themes/WxjzgcjczyQyb_B_Theme/base.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/WxjzgcjczyQyb_B_Theme/workarea.css" rel="stylesheet"
        type="text/css" />
</head>
<body style="padding: 20px; overflow-x: hidden;">
    <div class="wnavbar">
        <div class="wnavbar-icon">
            <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/logperson.png" /></div>
        <div class="wnavbar-inner">
            <b><span id="Span1" style="color: #46a3ff;">当前登录人：<asp:Label ID="label1" ForeColor="Red"
                runat="server" /></span><span>，</span>欢迎登录使用《无锡市住房和城乡建设局公共信息服务平台》！ </b>
        </div>
    </div>
    <br />
    <div class="wnavline">
    </div>
    <br />
    <div class="wnavbar">
        <div class="wnavbar-icon">
            <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/bestseller.png" /></div>
        <div class="wnavbar-inner">
            <b>办事动态</b>
        </div>
    </div>
    <div class="wcontent">
        <table style="border-width: 0px; width: 100%;" cellpadding="2px" cellspacing="0px">
            <tr>
                <td style="width: 50%">
                    <table style="border: 1px solid #e0e0e0; width: 100%; min-height: 120px;" cellpadding="2px"
                        cellspacing="0px">
                        <tr>
                            <td style="width: 30px; border-right: 1px solid #e0e0e0; background-color: #f5f5f5;
                                text-align: center;">
                                <%--     任<br />
                                务<br />
                                管<br />
                                理--%>
                            </td>
                            <td style="vertical-align: top;">
                                <%-- <div style="position: relative; width: 105px; height: 45px; background-color: #ff7777;
                                    left: 10px; top: 10px; float: left; margin-bottom: 10px; margin-right: 10px;">
                                    <div style="position: absolute; top: 10px; left: 8px; float: left;">
                                        <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/a.png" />
                                    </div>
                                    <div style="position: absolute; top: 5px; left: 35px; float: left;">
                                        <span style="font-size: 14px; color: White;">待办</span> <span style="font-size: 14px;
                                            color: White;">
                                            <asp:Label runat="server" ID="lbldb"></asp:Label></span>
                                    </div>
                                </div>
                                <div style="position: relative; width: 105px; height: 45px; background-color: #3aba7d;
                                    left: 10px; float: left; top: 10px; margin-bottom: 10px; margin-right: 10px;">
                                    <div style="position: absolute; top: 10px; left: 8px; float: left;">
                                        <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/a.png" />
                                    </div>
                                    <div style="position: absolute; top: 5px; left: 35px; float: left;">
                                        <span style="font-size: 14px; color: White;">已办结</span> <span style="font-size: 14px;
                                            color: White;">
                                            <asp:Label runat="server" ID="lblwc"></asp:Label></span>
                                    </div>
                                </div>--%>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 10px;">
                    &nbsp;
                </td>
                <td style="width: 50%">
                    <table style="border: 1px solid #e0e0e0; width: 100%; min-height: 120px;" cellpadding="2px"
                        cellspacing="0px">
                        <tr>
                            <td style="width: 30px; border-right: 1px solid #e0e0e0; background-color: #f5f5f5;
                                text-align: center;">
                                <%--   工<br />
                                作<br />
                                提<br />
                                醒--%>
                            </td>
                            <td style="vertical-align: top;">
                                <%--      <div id="demo" style="overflow: hidden; width: 100%; height: 120px; margin-left: 10px;">
                                    <div id="demo1">
                                        <asp:DataList ID="DataList_Rbbl" runat="server">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <div style="height: 15px;">
                                                                <asp:Label runat="server" ID="lbl_shjd" Text='<%#Eval("TaskTx").ToString() %>'></asp:Label></div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle />
                                        </asp:DataList>
                                    </div>
                                    <div id="demo2" runat="server">
                                    </div>

                                    <script type="text/javascript">
                                        var IsExist = document.getElementById("DataList_Rbbl");
                                        if (IsExist) {
                                            var speed = 100
                                            if (document.getElementById("DataList_Rbbl").rows.length >= 7) {
                                                demo2.innerHTML = demo1.innerHTML
                                                function Marquee() {
                                                    if (demo2.offsetTop - demo.scrollTop <= 0) {
                                                        demo.scrollTop -= demo1.offsetHeight
                                                    }
                                                    else {
                                                        demo.scrollTop++
                                                    }
                                                }
                                                var MyMar = setInterval(Marquee, speed)
                                                demo.onmouseover = function() { clearInterval(MyMar) }
                                                demo.onmouseout = function() { MyMar = setInterval(Marquee, speed) }
                                            }
                                        }
                                    </script>

                                </div>--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="wnavbar">
        <div class="wnavbar-l">
        </div>
        <div class="wnavbar-r">
        </div>
        <div class="wnavbar-icon">
            <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/comment2.png" /></div>
        <div class="wnavbar-inner">
            <b>系统操作指南</b>
        </div>
    </div>
    <br />
    <div class="wl-clear">
    </div>
    <br />
    <br />
    <div class="wnavline">
    </div>
    <div class="wwithicon">
        <div class="icon">
            <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/icons/alert.png" align="absmiddle" /></div>
        <span style="padding-left: 33px;">业务咨询电话：<span style="color: Red;">0512-65109046</span>
    </div>
    <%-- <div class="wlinks">
        <div class="wlink" runat="server" id="cyrw" visible="false">
            <a href="../Module_MyWork/MyJoinTask_List.aspx" target="_self" style="color: #333333;">
                <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/frameworkarea/a.png" />我参与的任务</a>
        </div>
        <div class="wlink" runat="server" id="fzrw" visible="false">
            <a href="../Module_MyWork/MyChargeTask_List.aspx" target="_self" style="color: #333333;">
                <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/frameworkarea/b.png" />我负责的任务</a></div>
        <div class="wlink" runat="server" id="dchf" visible="false">
            <a href="../Module_MyWork/MyDcYjHf_List.aspx" target="_self" style="color: #333333;">
                <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/frameworkarea/b.png" />督查意见回复</a></div>
        <div class="wlink" runat="server" id="rwdj" visible="false">
            <a href="../Module_Rwgl/TaskDj_List.aspx" target="_self" style="color: #333333;">
                <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/frameworkarea/a.png" />任务登记</a>
        </div>
        <div class="wlink" runat="server" id="rwfp" visible="false">
            <a href="../Module_Rwgl/TaskFp_List.aspx" target="_self" style="color: #333333;">
                <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/frameworkarea/b.png" />任务分配</a></div>
        <div class="wlink" runat="server" id="rwdc" visible="false">
            <a href="../Module_Rwgl/TaskDc_List.aspx" target="_self" style="color: #333333;">
                <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/frameworkarea/b.png" />任务督查</a></div>
        <div class="wlink" runat="server" id="rwfk" visible="false">
            <a href="../Module_Rwgl/TaskJzFk_List.aspx" target="_self" style="color: #333333;">
                <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/frameworkarea/b.png" />任务反馈</a></div>
        <div class="wlink" runat="server" id="rwpj" visible="false">
            <a href="../Module_Rwgl/TaskPj_List.aspx" target="_self" style="color: #333333;">
                <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/frameworkarea/b.png" />任务评价</a></div>
        <div class="wlink" runat="server" id="wcqk" visible="false">
            <a href="../Module_Bbtj/Rwwcqk_List.aspx" target="_self" style="color: #333333;">
                <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/frameworkarea/c.png" />任务完成情况统计
            </a>
        </div>
        <div class="wlink" runat="server" id="wczl" visible="false">
            <a href="../Module_Bbtj/RwwcZlqK_List.aspx" target="_self" style="color: #333333;">
                <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/frameworkarea/c.png" />任务完成质量统计
            </a>
        </div>
        <div class="wlink">
            <a href="../Module_Documents/MyDocuments_List.aspx" target="_self" style="color: #333333;">
                <img src="../../App_Themes/WxjzgcjczyQyb_B_Theme/images/frameworkarea/d.png" />文档阅读</a>
        </div>
    </div>--%>
    <%--   <div class="wl-clear">
    </div>
    <br />
    <div class="wnavline">
    </div>
--%>
    <%--   <script type="text/javascript">
        $("div.wlink").live("mouseover", function() {
            $(this).addClass("wlinkover");

        }).live("mouseout", function() {
            $(this).removeClass("wlinkover");


        })
       .live("click", function() {
           var text = $("a", this).text();
           if (text == "我参与的任务" || text == "我负责的任务" || text == "督查意见回复") {
               var a = top.topFrame.$("#hd").val();
               if (a != "10000000") {
                   top.topFrame.$("#" + a).removeClass("linkover");
               }
               top.topFrame.$("#hd").val("10000000");
               top.topFrame.$("#10000000").addClass("linkover");
               top.leftFrame.$("#我的任务").addClass("over");
               switch (text) {
                   case "我参与的任务":
                       top.leftFrame.$("#hd").val("10001010");
                       top.leftFrame.$("#任务管理 ul #10001010").addClass("over");
                       break;
                   case "我负责的任务":
                       top.leftFrame.$("#hd").val("10001020");
                       top.leftFrame.$("#任务管理 ul #10001020").addClass("over");
                       break;
                   case "督查意见回复":
                       top.leftFrame.$("#hd").val("10001030");
                       top.leftFrame.$("#任务管理 ul #10001030").addClass("over");
                       break;
               }


           }
           if (text == "任务登记" || text == "任务分配" || text == "任务督查" || text == "任务反馈" || text == "任务评价" || text == "任务完成情况统计" || text == "任务完成质量统计") {
               var a = top.topFrame.$("#hd").val();
               if (a != "20000000") {
                   top.topFrame.$("#" + a).removeClass("linkover");
               }
               top.topFrame.$("#hd").val("20000000");
               top.topFrame.$("#20000000").addClass("linkover");
               top.leftFrame.location = "LeftFrame.aspx?parentNodeID=20000000";

               switch (text) {
                   case "任务登记":
                       top.leftFrame.$("#hd").val("20001010");
                       top.leftFrame.$("#任务管理").addClass("over");
                       top.leftFrame.$("#任务管理 ul #20001010").addClass("over");
                       break;
                   case "任务分配":
                       top.leftFrame.$("#hd").val("20001020");
                       top.leftFrame.$("#任务管理").addClass("over");
                       top.leftFrame.$("#任务管理 ul #20001020").addClass("over");
                       break;
                   case "任务督查":
                       top.leftFrame.$("#hd").val("20001030");
                       top.leftFrame.$("#任务管理").addClass("over");
                       top.leftFrame.$("#任务管理 ul #20001030").addClass("over");
                       break;
                   case "任务反馈":
                       top.leftFrame.$("#hd").val("20001040");
                       top.leftFrame.$("#任务管理").addClass("over");
                       top.leftFrame.$("#任务管理 ul #20001040").addClass("over");
                       break;
                   case "任务评价":
                       top.leftFrame.$("#hd").val("20001050");
                       top.leftFrame.$("#任务管理").addClass("over");
                       top.leftFrame.$("#任务管理 ul #20001050").addClass("over");
                       break;

                   case "任务完成情况统计":
                       top.leftFrame.$("#hd").val("20002010");
                       top.leftFrame.$("#统计汇总").addClass("over");
                       top.leftFrame.$("#统计汇总 ul #20002010").addClass("over");
                       break;
                   case "任务完成质量统计":
                       top.leftFrame.$("#hd").val("20002020");
                       top.leftFrame.$("#统计汇总").addClass("over");
                       top.leftFrame.$("#统计汇总 ul #20002020").addClass("over");
                       break;
               }
           }
           if (text == "文档阅读") {
               var a = top.topFrame.$("#hd").val();
               if (a != "40000000") {
                   top.topFrame.$("#" + a).removeClass("linkover");
               }

               top.topFrame.$("#hd").val("40000000");
               top.topFrame.$("#40000000").addClass("linkover");


               top.leftFrame.location = "LeftFrame.aspx?parentNodeID=40000000";
               top.leftFrame.$("#hd").val("40001010");
               top.leftFrame.$("#文档管理").addClass("over");
               top.leftFrame.$("#文档管理 ul #40001010").addClass("over");
           }
       });
    </script>
--%>
</body>
</html>
