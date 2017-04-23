$(function() {
    function keyControl(event) {
        return (event.keyCode == 8)//backspace
            || (event.keyCode == 9)//tab
            || (event.keyCode == 13)//enter
            || ((event.keyCode > 32) && (event.keyCode < 41))//prior,next,end,home,left,up,right,down
            || (event.keyCode == 45)//insert
            || (event.keyCode == 46)//delete
            || (event.ctrlKey && ((event.keyCode == 65)//A
            || (event.keyCode == 67)//C
            || (event.keyCode == 86)//V
            || (event.keyCode == 88)//X
            || (event.keyCode == 89)//Y
            || (event.keyCode == 90)//Z
            ));
    }

    var input_controls = $(":text,:password,textarea,select,:file").filter(":visible");
    var enabled_input_controls = input_controls.filter(":enabled[readonly!='readonly'][readonly!=true]");

    enabled_input_controls.filter("[mytype='decimal'],[mytype='double'],[mytype='int32'],[mytype='int64'],[mytype='date'],[mytype='time'],[mytype='datetime']").addClass("textbox_ime_disabled");
    enabled_input_controls.filter("[mytype='int32'],[mytype='int64']").keydown(function(event) {
        return ((event.keyCode > 47) && (event.keyCode < 58)) || keyControl(event);
    });
    enabled_input_controls.filter("[mytype='decimal'],[mytype='double']").keydown(function(event) {
        return ((event.keyCode > 47) && (event.keyCode < 58)) || (event.keyCode == 190) || keyControl(event);
    });

    /*绑定编辑器*/
    enabled_input_controls.filter("[mytype='date']").datepicker({
        closeText: '关闭',
        prevText: '&#x3c;上月',
        nextText: '下月&#x3e;',
        currentText: '今天',
        monthNamesShort: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
        dayNamesMin: ['日', '一', '二', '三', '四', '五', '六'],
        dateFormat: 'yy-mm-dd',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: true,
        showOtherMonths: true,
        selectOtherMonths: true,
        yearSuffix: '',
        changeYear: true,
        changeMonth: true,
        showButtonPanel: true
    });
    var time = enabled_input_controls.filter("[mytype='time']");
    time.filter("[myshowsecond!='true']").timepicker({
        currentText: '当前时间',
        closeText: '关闭',
        timeOnlyTitle: '选择时间',
        timeText: '时间',
        hourText: '时',
        minuteText: '分',
        showSecond: false,
        timeFormat: 'hh:mm'
    });
    time.filter("[myshowsecond='true']").timepicker({
        currentText: '当前时间',
        closeText: '关闭',
        timeOnlyTitle: '选择时间',
        timeText: '时间',
        hourText: '时',
        minuteText: '分',
        secondText: '秒',
        showSecond: true,
        timeFormat: 'hh:mm:ss'
    });

    /*错误的编辑样式*/
    enabled_input_controls.filter("[myerror='true']").addClass("textbox_error").keydown(function(event) {
        $(this).removeClass("textbox_error");
    }).first().focus();

    /*正常的编辑样式*/
    input_controls.addClass("textbox");
    enabled_input_controls.hover(function() {
        $(this).addClass("textbox_hover");
    }, function() {
        $(this).removeClass("textbox_hover");
    }).focus(function() {
        $(this).addClass("textbox_focus");
    }).blur(function() {
        $(this).removeClass("textbox_focus");
    });

    //    $("button,:button,:submit,:reset").button();

    /*绑定校验器*/
    jQuery.validator.addMethod("time", function(value, element) {
        return this.optional(element) || /^([0-1]\d|2[0-3]):[0-5]\d(|:[0-5]\d)$/.test(value);
    }, "请输入合法的时间");

    jQuery.extend(jQuery.validator.messages, {
        required: "必选字段",
        remote: "请修正该字段",
        email: "请输入合法的电子邮件",
        url: "请输入合法的网址",
        dateISO: "请输入合法的日期",
        number: "请输入合法的数字",
        digits: "请输入合法的整数",
        equalTo: "请再次输入相同的值",
        accept: "请输入拥有合法后缀名的字符串",
        maxlength: jQuery.validator.format("请输入一个长度最多是 {0} 的字符串"),
        minlength: jQuery.validator.format("请输入一个长度最少是 {0} 的字符串"),
        rangelength: jQuery.validator.format("请输入一个长度介于 {0} 和 {1} 之间的字符串"),
        range: jQuery.validator.format("请输入一个介于 {0} 和 {1} 之间的值"),
        max: jQuery.validator.format("请输入一个最大为 {0} 的值"),
        min: jQuery.validator.format("请输入一个最小为 {0} 的值")
    });

    enabled_input_controls.change(function() {
        if (typeof $(this).valid == 'function') {
            $(this).valid();
        }
    });

    $("form").validate({
        errorClass: "textbox_error",
        validClass: "textbox",
        submitHandler: function(form) {
            form.submit();
        }
    });
});
