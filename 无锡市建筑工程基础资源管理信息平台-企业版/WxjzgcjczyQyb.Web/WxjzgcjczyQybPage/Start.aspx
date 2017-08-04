<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Start.aspx.cs" Inherits="WxjzgcjczyQyb.Web.WxjzgcjczyQybPage.Start" %>

<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <meta name="description" content="">
    <meta name="author" content="">

    <title>无锡市住建局政务服务网</title>

    <!-- Bootstrap core CSS -->
    <link href="Common/css/bootstrap.min.css" rel="stylesheet">

    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <link href="Common/css/ie10-viewport-bug-workaround.css" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="Common/css/home.css" rel="stylesheet">


    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="js/html5shiv.min.js"></script>
      <script src="js/respond.min.js"></script>
    <![endif]-->
  </head>

  <body>
  <div class="container" >
    <nav class="navbar" style="border-bottom: 4px solid #4178be; margin-bottom: 5px;">

        <div class="navbar-header">
          <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>
          <div class="top_logo">
            <a class="top_logo_a" href="#">
              <img src="Common/images/logo.png" style="height: 60px;"> <span>无锡市住建局政务服务网</span>
            </a>
          </div>
        </div>

        <div class="top_main">
          <div class="top_nav">
            <a class="top_nav_sub top_nav_subOn" href="#">首页</a>
            <a class="top_nav_sub" href="Page_List.aspx?menuno=01">工程项目</a>
            <a class="top_nav_sub" href="Page_List.aspx?menuno=02">市场主体</a>
            <a class="top_nav_sub" href="Page_List.aspx?menuno=03">执业人员</a>
            <a class="top_nav_sub" href="Page_List.aspx?menuno=04">诚信信息</a>
            <a class="top_nav_sub" href="Map.html">地图服务</a>
          </div>

          <span id="top1"><a href="Login.aspx" class="js_denglu">你好，请登录</a><a href="#" class="js_zhuce">马上注册</a></span>
        </div>

        <!--
        <div id="navbar" class="navbar-collapse collapse">
          <form class="navbar-form navbar-right">
            <div class="form-group">
              <input type="text" placeholder="Email" class="form-control">
            </div>
            <div class="form-group">
              <input type="password" placeholder="Password" class="form-control">
            </div>
            <button type="submit" class="btn btn-success">Sign in</button>
          </form>
        </div>--> <!--/.navbar-collapse -->

    </nav>
    </div>

    <div class="container" style="margin-bottom: 2px;">

      <div class="row">
        <div class="col-md-2"><span class="js_search_01">办件进度查询：</span> </div>
        <div class="col-md-8">
          <input type="text" name="q" autocomplete="off" class="js_search_02" value="请输入 经办人/申请人、单位" onclick="if(this.value==&quot;请输入 经办人/申请人、单位&quot;){this.value=&quot;&quot;;this.focus();}" onblur="if(this.value==&quot;&quot;){this.value=&quot;请输入 经办人/申请人、单位&quot;}">

        </div>
        <div class="col-md-2">
          <input type="submit" class="js_search_03" value="查询">
        </div>
      </div>

      <!--
      <div class="left_tit_search">
        <span class="js_search_01">办件进度查询：</span>
        <input type="text" name="q" autocomplete="off" class="js_search_02" value="请输入 经办人/申请人、单位" onclick="if(this.value==&quot;请输入 经办人/申请人、单位&quot;){this.value=&quot;&quot;;this.focus();}" onblur="if(this.value==&quot;&quot;){this.value=&quot;请输入 经办人/申请人、单位&quot;}">
        <input type="submit" class="js_search_03" value="查询">
      </div> -->

    </div>

    <!-- Main jumbotron for a primary marketing message or call to action -->

    <div class="container" style="">

        <div class="left_tit_main">服务事项</div>
        <div class="left_list_main">
          <div class="scp2_rmfw_main">

            <a class="scp2_rmfw_sub" href="Detail.html" title="项目登记" target="_blank" style="background-image:url(Common/images/1.png);"><span>项目登记</span></a>
            <a class="scp2_rmfw_sub" href="Detail.html" title="招投标" target="_blank" style="background-image:url(Common/images/2.png);"><span>招投标</span></a>
            <a class="scp2_rmfw_sub" href="Detail.html" title="施工图审查" target="_blank" style="background-image:url(Common/images/3.png);"><span>施工图审查</span></a>
            <a class="scp2_rmfw_sub" href="#" title="勘察设计合同备案" target="_blank" style="background-image:url(Common/images/4.png);"><span>勘察设计合同备案</span></a>
            <a class="scp2_rmfw_sub" href="#" title="施工监理合同备案" target="_blank" style="background-image:url(Common/images/5.png);"><span>施工监理合同备案</span></a>
            <a class="scp2_rmfw_sub" href="#" title="造价咨询合同备案" target="_blank" style="background-image:url(Common/images/6.png);"><span>造价咨询合同备案</span></a>
            <a class="scp2_rmfw_sub" href="#" title="施工许可证" target="_blank" style="background-image:url(Common/images/7.png);"><span>施工许可证</span></a>
            <a class="scp2_rmfw_sub" href="#" title="安全监督" target="_blank" style="background-image:url(Common/images/8.png);"><span>安全监督</span></a>
            <a class="scp2_rmfw_sub" href="#" title="质量监督" target="_blank" style="background-image:url(Common/images/9.png);"><span>质量监督</span></a>
            <a class="scp2_rmfw_sub" href="#" title="竣工备案" target="_blank" style="background-image:url(Common/images/10.png);"><span>竣工备案</span></a>
            <a class="scp2_rmfw_sub" href="#" title="档案接收" target="_blank" style="background-image:url(Common/images/1.png);"><span>档案接收</span></a>
            <a class="scp2_rmfw_sub" href="#" title="白蚁防治" target="_blank" style="background-image:url(Common/images/2.png);"><span>白蚁防治</span></a>
            <a class="scp2_rmfw_sub" href="#" title="房屋安全管理" target="_blank" style="background-image:url(Common/images/3.png);"><span>房屋安全管理</span></a>
            <a class="scp2_rmfw_sub" href="#" title="住房保障" target="_blank" style="background-image:url(Common/images/4.png);"><span>住房保障</span></a>
            <a class="scp2_rmfw_sub" href="#" title="物业管理" target="_blank" style="background-image:url(Common/images/5.png);"><span>物业管理</span></a>
            <a class="scp2_rmfw_sub" href="#" title="维修资金管理" target="_blank" style="background-image:url(Common/images/6.png);"><span>维修资金管理</span></a>
            <a class="scp2_rmfw_sub" href="#" title="公房管理" target="_blank" style="background-image:url(Common/images/7.png);"><span>公房管理</span></a>
            <a class="scp2_rmfw_sub" href="#" title="商品房备案" target="_blank" style="background-image:url(Common/images/8.png);"><span>商品房备案</span></a>
            <a class="scp2_rmfw_sub" href="#" title="存量房备案" target="_blank" style="background-image:url(Common/images/9.png);"><span>存量房备案</span></a>

          </div></div>

    </div>


    <div class="container">
      <!-- Example row of columns -->
      <div class="news_title">最新资讯</div>

      <div class="row">
        <div class="col-md-7 news_area">
          <div class="bs-example bs-example-tabs" data-example-id="togglable-tabs">
            <ul id="myTabs" class="nav nav-tabs" role="tablist">
              <li role="presentation" class="active"><a href="#policy" id="policy-tab" role="tab" data-toggle="tab" aria-controls="policy" aria-expanded="true">政策法规</a></li>
              <li role="presentation"><a href="#notification" role="tab" id="notification-tab" data-toggle="tab" aria-controls="notification">文件通知</a></li>
              <li role="presentation"><a href="#announcement" role="tab" id="announcement-tab" data-toggle="tab" aria-controls="announcement">公示公告</a></li>
              <div class="news_more">
                <a href="#">
                  <span class="glyphicon-class">更多</span>
                  <span class="glyphicon glyphicon-th-large" aria-hidden="true"></span>
                </a>
              </div>
            </ul>

            <div id="myTabContent" class="tab-content">
              <div role="tabpanel" class="tab-pane fade in active" id="policy" aria-labelledby="policy-tab">
                <div class="activeTinyTabContent" item_code="jsbpp_news_tzgg" id="latestnews_tab1">
                  <ul class="news_ul">

                    <li><i class="dota_ranking">1</i> <span>2017-06-07</span>
                      <a class="formsubmit" target="_blank" >
                        关于征求《施工总承包企业特级资质标准》（征求意见稿）意见的函
                      </a></li>

                    <li><i class="dota_ranking">2</i> <span>2017-06-01</span>
                      <a class="formsubmit" target="_blank">
                        住房城乡建设部办公厅关于定期报送加强建筑设计管理等有关工作进...
                      </a></li>

                    <li><i class="dota_ranking">3</i> <span>2017-05-25</span>
                      <a class="formsubmit" target="_blank" >
                        住房城乡建设部办公厅关于2017年一季度建筑工程施工转包违法...
                      </a></li>

                    <li><i class="dota_ranking">4</i> <span>2017-05-08</span>
                      <a class="formsubmit" target="_blank">
                        住房城乡建设部关于开展全过程工程咨询试点工作的通知
                      </a></li>

                    <li><i class="dota_ranking">5</i> <span>2017-05-08</span>
                      <a class="formsubmit" target="_blank" >
                        住房城乡建设部关于印发工程勘察设计行业发展“十三五”规划的通...
                      </a></li>

                    <li><i class="dota_ranking">6</i> <span>2017-05-04</span>
                      <a class="formsubmit" target="_blank" >
                        住房城乡建设部关于印发建筑业发展“十三五”规划的通知
                      </a></li>

                    <li><i class="dota_ranking">7</i> <span>2017-04-28</span>
                      <a class="formsubmit" target="_blank">
                        住房城乡建设部办公厅关于江西众森建筑发展有限公司等9家企业资...
                      </a></li>

                    <li><i class="dota_ranking">8</i> <span>2017-04-26</span>
                      <a class="formsubmit" target="_blank" >
                        住房城乡建设部办公厅关于黄崇顺申报一级建造师注册弄虚作假行为...
                      </a></li>

                  </ul>
                </div>
              </div>
              <div role="tabpanel" class="tab-pane fade" id="notification" aria-labelledby="notification-tab">
                <ul class="news_ul">

                  <li><i class="dota_ranking">1</i> <span>2017-06-07</span>
                    <a class="formsubmit" target="_blank" >
                      关于征求《施工总承包企业特级资质标准》（征求意见稿）意见的函
                    </a></li>

                  <li><i class="dota_ranking">2</i> <span>2017-06-01</span>
                    <a class="formsubmit" target="_blank" >
                      住房城乡建设部办公厅关于定期报送加强建筑设计管理等有关工作进...
                    </a></li>

                  <li><i class="dota_ranking">3</i> <span>2017-05-25</span>
                    <a class="formsubmit" target="_blank" >
                      住房城乡建设部办公厅关于2017年一季度建筑工程施工转包违法...
                    </a></li>

                  <li><i class="dota_ranking">4</i> <span>2017-05-08</span>
                    <a class="formsubmit" target="_blank" >
                      住房城乡建设部关于开展全过程工程咨询试点工作的通知
                    </a></li>

                  <li><i class="dota_ranking">5</i> <span>2017-05-08</span>
                    <a class="formsubmit" target="_blank" >
                      住房城乡建设部关于印发工程勘察设计行业发展“十三五”规划的通...
                    </a></li>

                  <li><i class="dota_ranking">6</i> <span>2017-05-04</span>
                    <a class="formsubmit" target="_blank" >
                      住房城乡建设部关于印发建筑业发展“十三五”规划的通知
                    </a></li>

                  <li><i class="dota_ranking">7</i> <span>2017-04-28</span>
                    <a class="formsubmit" target="_blank" >
                      住房城乡建设部办公厅关于江西众森建筑发展有限公司等9家企业资...
                    </a></li>

                  <li><i class="dota_ranking">8</i> <span>2017-04-26</span>
                    <a class="formsubmit" target="_blank" >
                      住房城乡建设部办公厅关于黄崇顺申报一级建造师注册弄虚作假行为...
                    </a></li>

                </ul>
              </div>
              <div role="tabpanel" class="tab-pane fade" id="announcement" aria-labelledby="announcement-tab">
                <ul class="news_ul">

                  <li><i class="dota_ranking">1</i> <span>2017-06-07</span>
                    <a class="formsubmit" target="_blank" >
                      关于征求《施工总承包企业特级资质标准》（征求意见稿）意见的函
                    </a></li>

                  <li><i class="dota_ranking">2</i> <span>2017-06-01</span>
                    <a class="formsubmit" target="_blank" >
                      住房城乡建设部办公厅关于定期报送加强建筑设计管理等有关工作进...
                    </a></li>

                  <li><i class="dota_ranking">3</i> <span>2017-05-25</span>
                    <a class="formsubmit" target="_blank" >
                      住房城乡建设部办公厅关于2017年一季度建筑工程施工转包违法...
                    </a></li>

                  <li><i class="dota_ranking">4</i> <span>2017-05-08</span>
                    <a class="formsubmit" target="_blank" >
                      住房城乡建设部关于开展全过程工程咨询试点工作的通知
                    </a></li>

                  <li><i class="dota_ranking">5</i> <span>2017-05-08</span>
                    <a class="formsubmit" target="_blank" >
                      住房城乡建设部关于印发工程勘察设计行业发展“十三五”规划的通...
                    </a></li>

                  <li><i class="dota_ranking">6</i> <span>2017-05-04</span>
                    <a class="formsubmit" target="_blank" >
                      住房城乡建设部关于印发建筑业发展“十三五”规划的通知
                    </a></li>

                  <li><i class="dota_ranking">7</i> <span>2017-04-28</span>
                    <a class="formsubmit" target="_blank" >
                      住房城乡建设部办公厅关于江西众森建筑发展有限公司等9家企业资...
                    </a></li>

                  <li><i class="dota_ranking">8</i> <span>2017-04-26</span>
                    <a class="formsubmit" target="_blank" >
                      住房城乡建设部办公厅关于黄崇顺申报一级建造师注册弄虚作假行为...
                    </a></li>

                </ul>
              </div>
            </div>
          </div>
        </div>
        <div class="col-md-5 news_area">
          <div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
            <!-- Indicators -->
            <ol class="carousel-indicators">
              <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
              <li data-target="#carousel-example-generic" data-slide-to="1"></li>
              <!--<li data-target="#carousel-example-generic" data-slide-to="2"></li>-->
            </ol>

            <!-- Wrapper for slides -->
            <div class="carousel-inner" role="listbox">
              <div class="item active">
                <img src="Common/images/news1.jpg" alt="..." style="width: 100%; height: 360px;">
                <div class="carousel-caption">
                  李克强在全国深化简政放权放管结合优化服务改革电视电话会议上发表重要讲话
                </div>
              </div>
              <div class="item">
                <img src="Common/images/news2.jpg" alt="..." style="width: 100%; height: 360px;">
                <div class="carousel-caption">
                  国务院安委会召开全国安全生产电视电话会议
                </div>
              </div>
              ...
            </div>

            <!-- Controls -->
            <a class="left carousel-control" href="#carousel-example-generic" role="button" data-slide="prev">
              <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
              <span class="sr-only">Previous</span>
            </a>
            <a class="right carousel-control" href="#carousel-example-generic" role="button" data-slide="next">
              <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
              <span class="sr-only">Next</span>
            </a>
          </div>
        </div>

      </div>

      <hr>

      <footer>
	    <center>
          <p >无锡市住房和城乡建设局版权所有</p>
		  <p >技术支持：无锡市建设信息中心</p>
		</center>
      </footer>
    </div> <!-- /container -->


    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="Common/js/jquery.1.12.4.min.js"></script>
    <script src="Common/js/bootstrap.min.js"></script>
    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <script src="Common/js/ie10-viewport-bug-workaround.js"></script>
  </body>
</html>
