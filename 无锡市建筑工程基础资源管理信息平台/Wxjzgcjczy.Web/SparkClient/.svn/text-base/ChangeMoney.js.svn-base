function regInput(reg) {
    var srcElem = event.srcElement
    var oSel = document.selection.createRange()
    oSel = oSel.duplicate()

    oSel.text = ""
    var srcRange = srcElem.createTextRange()
    oSel.setEndPoint("StartToStart", srcRange)
    var num = oSel.text + String.fromCharCode(event.keyCode) + srcRange.text.substr(oSel.text.length)
    event.returnvalue = reg.test(num)
}

function chineseNumber(num) {
    if (isNaN(num) || num > Math.pow(10, 12)) return ""
    var cn = "零壹贰叁肆伍陆柒捌玖"
    var unit = new Array("拾百千", "分角")
    var unit1 = new Array("万亿", "")
    var numArray = num.toString().split(".")
    var start = new Array(numArray[0].length - 1, 2)

    function toChinese(num, index) {
        var num = num.replace(/\d/g, function($1) {
            return cn.charAt($1) + unit[index].charAt(start-- % 4 ? start % 4 : -1)
        })
        return num
    }

    for (var i = 0; i < numArray.length; i++) {
        var tmp = ""
        for (var j = 0; j * 4 < numArray[i].length; j++) {
            var strIndex = numArray[i].length - (j + 1) * 4
            var str = numArray[i].substring(strIndex, strIndex + 4)
            var start = i ? 2 : str.length - 1
            var tmp1 = toChinese(str, i)
            tmp1 = tmp1.replace(/(零.)+/g, "零").replace(/零+$/, "")
            tmp1 = tmp1.replace(/^壹拾/, "拾")
            tmp = (tmp1 + unit1[i].charAt(j - 1)) + tmp
        }
        numArray[i] = tmp
    }

    numArray[1] = numArray[1] ? numArray[1] : ""
    numArray[0] = numArray[0] ? numArray[0] + "元" : numArray[0], numArray[1] = numArray[1].replace(/^零+/, "")
    numArray[1] = numArray[1].match(/分/) ? numArray[1] : numArray[1] + "整"
    return numArray[0] + numArray[1]
}

function aNumber(num) {
    var numArray = new Array()
    var unit = "亿万元$"
    for (var i = 0; i < unit.length; i++) {
        var re = eval_r("/" + (numArray[i - 1] ? unit.charAt(i - 1) : "") + "(.*)" + unit.charAt(i) + "/")
        if (num.match(re)) {
            numArray[i] = num.match(re)[1].replace(/^拾/, "壹拾")
            numArray[i] = numArray[i].replace(/[零壹贰叁肆伍陆柒捌玖]/g, function($1) {
                return "零壹贰叁肆伍陆柒捌玖".indexOf($1)
            })
            numArray[i] = numArray[i].replace(/[分角拾百千]/g, function($1) {
                return "*" + Math.pow(10, "分角 拾百千 ".indexOf($1) - 2) + "+"
            }).replace(/^\*|\+$/g, "").replace(/整/, "0")
            numArray[i] = "(" + numArray[i] + ")*" + Math.ceil(Math.pow(10, (2 - i) * 4))
        }
        else numArray[i] = 0
    }
    return eval_r(numArray.join("+"))
}
//将浮点数字符串按照格式返回指定位数的小数字符串
function GetFormatPoint(nStr, n) {

    var result = "";
    if (nStr == "" || nStr == undefined) {
        result += "0.";
        for (var i = 0; i < n; i++) {

            result += "0";
        }
        return result;
    }
    var index = nStr.indexOf('.');
    if (index < 0) {
        if (nStr.length > 0) {
            result = nStr + ".";
            for (var i = 0; i < n; i++) {
                result += "0";
            }
            return result;
        }
    }
    else {
        var Npart = nStr.substr(0, index);
        var pointPart = nStr.substr(index + 1);

        result = nStr.substring(0, index + 1);
        if (pointPart.length > n) {
            if (parseInt(pointPart.substr(n, 1)) > 4)
                return result + pointPart.substr(0, n - 1) + (parseInt(pointPart.substr(n - 1, 1)) + 1).toString();
            else
                return result + pointPart.substr(0, n);
        }
        else {
            var y = 0;
            for (var m = 1; m <= pointPart.length; m++) {
                result += nStr.substr(index + m, 1);
                y++;
            }
            if (y < n) {
                for (var i = y; i < n; i++) {

                    result += "0";
                }
            }
            return result;
        }
    }
}