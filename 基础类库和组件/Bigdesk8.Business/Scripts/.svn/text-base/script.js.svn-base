//========================
// 打开一个模式窗口
// 参数含义：
// url：页面链接
// width：页面窗口宽度
// height：页面窗口高度
//========================
function OpenModalDialog(url, width, height) {
    url = url + (url.indexOf("?") < 0 ? "?" : "&") + "rd=" + (new Date()).toLocaleString() + Math.random();

    if (width == null) width = 850;
    if (height == null) height = 525;

    return window.showModalDialog(url, '', 'dialogHeight:' + height + 'px;dialogWidth:' + width + 'px;edge:raised;center:Yes;help:No;resizable:Yes;status:no;scroll:yes;unadorned:yes;');
}

//========================
// 将控件 ID 包含 subParentControlID 的控件中的所有子控件类型为 checkbox、id 包含子串 subCheckBoxID 的控件的 checked 属性设置为 value。
// 参数含义：
// subParentControlID：父控件 ID 的子字符串
// subCheckBoxID：CheckBox.ID 中的子字符串
// value：将要设置的 checked 的值
//========================
function SetCheckBoxValue(subParentControlID, subCheckBoxID, value) {
    $("[id*='" + subParentControlID + "']").filter("input[type='checkbox'][disabled!='disabled'][disabled!=true][id*='" + subCheckBoxID + "']").attr("checked", value);
}

//========================
// 将控件 ID 包含 subParentControlID 的控件中的所有子控件类型为 radio、id 包含子串 subRadioID 的控件的 checked 属性设置为 value。
// 参数含义：
// subParentControlID：父控件 ID 的子字符串
// subRadioID：Radio.ID 中的子字符串
// value：将要设置的 checked 的值
//========================
function SetRadioValue(subParentControlID, subRadioID, value) {
    $("[id*='" + subParentControlID + "']").filter("input[type='radio'][disabled!='disabled'][disabled!=true][id*='" + subRadioID + "']").attr("checked", value);
}

//========================
// 判断控件 ID 包含 subParentControlID 的控件中的所有子控件类型为 checkbox、id 包含子串 subCheckBoxID 的控件的 checked 属性是否都为 value？
// 参数含义：
// subParentControlID：父控件 ID 的子字符串
// subCheckBoxID：CheckBox.ID 中的子字符串
// value：将要设置的 checked 的值
//========================
function IsCheckBoxAllChecked(subParentControlID, subCheckBoxID, value) {
    return $("[id*='" + subParentControlID + "']").filter("input[type='checkbox'][disabled!='disabled'][disabled!=true][id*='" + subCheckBoxID + "'][checked!=" + value + "]").length <= 0;
}

//========================
// 返回一个指定范围内的随机整数。
// 参数含义：
// minValue: 返回的随机整数的下界（随机整数可取该下界值）。
// maxValue: 返回的随机整数的上界（随机整数不能取该上界值）。maxValue 必须大于等于 minValue。
//========================
function SysRandom(minValue, maxValue) {
    if (maxValue < minValue) return 0;
    if (maxValue === minValue) return minValue;
    return Math.floor(Math.random() * (maxValue - minValue)) + minValue;
}

//==========================
// 表示键/值对的集合，这些键/值对根据键的哈希代码进行组织。
//
// 示例：
//    var hashtable = new jQuery.Hashtable();
//    $(function ()
//    {
//        $('#btnAdd').click(function ()
//        {
//            hashtable.add($('#txtAddKey').val(), $('#txtAddValue').val());
//        });
//        $('#btnGet').click(function ()
//        {
//            alert(hashtable.get($('#txtGetKey').val()))
//        });
//    })
//==========================
(function($) {
    jQuery.Hashtable = function() {
        this.items = new Array();
        this.itemsCount = 0;
        this.add = function(key, value) {
            if (!this.containsKey(key)) {
                this.items[key] = value;
                this.itemsCount++;
            }
            else {
                $.error('关键字 key "' + key + '" 已经存在了.');
            }
        }
        this.get = function(key) {
            if (this.containsKey(key))
                return this.items[key];
            else
                return null;
        }
        this.remove = function(key) {
            if (this.containsKey(key)) {
                delete this.items[key];
                this.itemsCount--;
            }
            else {
                $.error('关键字 key "' + key + '" 不存在.');
            }
        }
        this.containsKey = function(key) {
            return typeof (this.items[key]) != "undefined";
        }
        this.containsValue = function containsValue(value) {
            for (var item in this.items) {
                if (this.items[item] == value)
                    return true;
            }
            return false;
        }
        this.contains = function(keyOrValue) {
            return this.containsKey(keyOrValue) || this.containsValue(keyOrValue);
        }
        this.getKeys = function() {
            var a = new Array();
            for (var k in this.items) {
                a.push(k);
            }
            return a;
        }
        this.getValues = function() {
            var a = new Array();
            for (var k in this.items) {
                a.push(this.items[k]);
            }
            return a;
        }
        this.clear = function() {
            this.items = new Array();
            itemsCount = 0;
        }
        this.size = function() {
            return this.itemsCount;
        }
        this.isEmpty = function() {
            return this.size() == 0;
        }
    };
})(jQuery);


//==========================
// table tr 奇偶行颜色交替，鼠标移上高亮显示
//
// 示例：
//    $(".table1").DataGridUI({
//        rowBeginID: 1,                              // 内容行开始的行号，行号从0开始，从前往后数的正数
//        rowEndID: 1,                                // 内容行结束的行号，行号从0开始，从后往前数的正数
//        rowStyle: "gridrow",                        // 行样式
//        alternatingRowStyle: "gridalternatingrow",  // 交替行样式
//        activeRowStyle: "gridactiverow"             // 鼠标所在行样式
////    });
//==========================
(function($) {
    $.fn.DataGridUI = function(options) {
        var settings = {
            rowBeginID: 1,                              // 内容行开始的行号，行号从0开始，从前往后数的正数
            rowEndID: 1,                                // 内容行结束的行号，行号从0开始，从后往前数的正数
            rowStyle: "gridrow",                        // 行样式
            alternatingRowStyle: "gridalternatingrow",  // 交替行样式
            isHighlight: true,                          // 是否将鼠标所在行高亮
            activeRowStyle: "gridactiverow"             // 鼠标所在行样式
        }

        if (options) {
            $.extend(settings, options);
        }

        this.each(function() {
            var $table = $(this);
            var $alltr = $table.find("tr:first").parent().children("tr");
            var $tr = $alltr.slice(settings.rowBeginID, $alltr.length - settings.rowEndID);

            // 添加奇偶行样式
            $tr.filter("tr:even").addClass(settings.rowStyle);
            $tr.filter("tr:odd").addClass(settings.alternatingRowStyle);

            // 添加活动行样式
            if (settings.isHighlight) {
                $tr.mouseover(function() {
                    $(this).addClass(settings.activeRowStyle);
                }).mouseout(function() {
                    $(this).removeClass(settings.activeRowStyle);
                });
            }
        });
    };
})(jQuery);


//function client_OnTreeNodeChecked() {
//    var ele = event.srcElement;
//    if (ele.type == 'checkbox') {
//        var childrenDivID = ele.id.replace('CheckBox', 'Nodes');
//        var div = document.getElementById(childrenDivID);
//        if (div != null) {
//            var checkBoxs = div.getElementsByTagName('INPUT');
//            for (var i = 0; i < checkBoxs.length; i++) {
//                if (checkBoxs[i].type == 'checkbox')
//                    checkBoxs[i].checked = ele.checked;
//            }
//        }
//        else {
//            var div = GetParentByTagName(ele, 'DIV');
//            var checkBoxs = div.getElementsByTagName('INPUT');
//            var parentCheckBoxID = div.id.replace('Nodes', 'CheckBox');
//            var parentCheckBox = document.getElementById(parentCheckBoxID);
//            for (var i = 0; i < checkBoxs.length; i++) {
//                if (checkBoxs[i].type == 'checkbox' && checkBoxs[i].checked) {
//                    parentCheckBox.checked = true;
//                    return;
//                }
//            }
//            parentCheckBox.checked = false;
//        }
//    }
//}

//function GetParentByTagName(element, tagName) {
//    var parent = element.parentNode;
//    var upperTagName = tagName.toUpperCase();
//    while (parent && (parent.tagName.toUpperCase() != upperTagName)) {
//        parent = parent.parentNode ? parent.parentNode : parent.parentElement;
//    }
//    return parent;
//}



////判断时间格式是否正确
////参数：文本框控件
////返回值：布尔值
//function isTime(timeTextbox)
//{
//    var b = isTimeString(timeTextbox.value);
//    if (!b)
//    {
//        alert('时间格式错误！采用24时制,例如:09:00,15:30');
//        return false;
//    }
//    return true;
//}

////判断上午时间格式是否正确
////参数：文本框控件
////返回值：布尔值
//function isTimeAM(timeAMTextbox)
//{
//    var b = isTimeAMString(timeAMTextbox.value);
//    if (!b)
//    {
//        alert("上午时间格式错误.采用24时制,例如:09:00,09:30 ");
//        return false;
//    }
//    return true;
//}

////判断下午时间格式是否正确
////参数：文本框控件
////返回值：布尔值
//function isTimePM(timePMTextbox)
//{
//    var b = isTimePMString(timePMTextbox.value);
//    if (!b)
//    {
//        alert("下午时间格式错误.采用24时制,例如:15:30,16:00 ");
//        return false;
//    }
//    return true;
//}


////判断时间格式是否正确
////参数：字符串值
////返回值：布尔值
//function isTimeString(timeString)
//{
//    var str = timeString;
//    if ((str == null) || (str == "")) return false;

//    var a = str.match(/^(\d{1,2})([:]{1})(\d{1,2})$/);
//    if (a == null) return false;

//    if (a[1] < 0 || a[1] > 23 || a[3] < 0 || a[3] > 59) return false;

//    return true;
//}

////判断上午时间格式是否正确
////参数：字符串值
////返回值：布尔值
//function isTimeAMString(timeAMString)
//{
//    var str = timeAMString;
//    if ((str == null) || (str == "")) return false;

//    var a = str.match(/^(\d{1,2})([:]{1})(\d{1,2})$/);
//    if (a == null) return false;

//    if (a[1] < 0 || a[1] > 12 || a[3] < 0 || a[3] > 59) return false;

//    return true;
//}

////判断下午时间格式是否正确
////参数：字符串值
////返回值：布尔值
//function isTimePMString(timePMString)
//{
//    var str = timePMString;
//    if ((str == null) || (str == "")) return false;

//    var a = str.match(/^(\d{1,2})([:]{1})(\d{1,2})$/);
//    if (a == null) return false;

//    if (a[1] < 12 || a[1] > 23 || a[3] < 0 || a[3] > 59) return false;

//    return true;
//}
