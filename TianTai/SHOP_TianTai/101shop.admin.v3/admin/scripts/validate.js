/*
Name:数据验证
Author:yz
Time:2010-7-27
*/
String.prototype.trim = function () { return jQuery.trim(this) };
//验证是否为空
function IsNull(control, min, max) { var str = jQuery(control).val().trim(); return !(str.length < min || str.length > max) }
//验证是不是网址
function IsHttpUrl(control, allownull) { var str = jQuery(control).val().trim(); return (allownull && str.length == 0) || (str.length && /(http[s]?|ftp):\/\/[^\/\.]+?\..+\w$/i.test(str)) }
//验证是不是网址
function IsUrl(control, allownull) { var str = jQuery(control).val().trim(); return (allownull && str.length == 0) || (str.length && /^\w+([-+.]\w+)*.\w+([-.]\w+)*\.\w+([-.]\w+)*$/i.test(str)) }
//验证电话号码
function IsPhone(control, min, allownull) { var str = jQuery(control).val().trim(); return (allownull && str.length == 0) || (str.length && /^(([0\+]\d{2,3}-)?(0\d{2,3})-)?(\d{7,8})(-(\d{2,6}))?$/i.test(str) || min == 0) }
//验证手机号码
function IsMobile(control, allownull) { var str = jQuery(control).val().trim(); return (allownull && str.length == 0) || (str.length && /^(1[0123456789])\d{9}$/i.test(str)) }
//验证是电话号码或者手机号码
function IsPhoneOrIsMobile(control, allownull) { var str = jQuery(control).val().trim(); return (allownull && str.length == 0) || (str.length && /^(([0\+]\d{2,3}-)?(0\d{2,3})-)?(\d{7,8})(-(\d{2,6}))?$/i.test(str) || /^(1[0123456789])\d{9}$/i.test(str)) }
//验证邮编
function IsPostcode(control, allownull) { var str = jQuery(control).val().trim(); return (allownull && str.length == 0) || (str.length && /^\d(9|[0-7])\d{4}$/i.test(str)) }
//验证身份证号码
function IsIdNumber(control) {
    var vcity = { 11: "北京", 12: "天津", 13: "河北", 14: "山西", 15: "内蒙古",
        21: "辽宁", 22: "吉林", 23: "黑龙江", 31: "上海", 32: "江苏",
        33: "浙江", 34: "安徽", 35: "福建", 36: "江西", 37: "山东", 41: "河南",
        42: "湖北", 43: "湖南", 44: "广东", 45: "广西", 46: "海南", 50: "重庆",
        51: "四川", 52: "贵州", 53: "云南", 54: "西藏", 61: "陕西", 62: "甘肃",
        63: "青海", 64: "宁夏", 65: "新疆", 71: "台湾", 81: "香港", 82: "澳门", 91: "国外"
    };
    var isum = 0;
    var cardidstr = jQuery(control).val().trim();
    //检查地址是否符合要求
    if (isNaN(cardidstr) || vcity[parseInt(cardidstr.substr(0, 2))] == null) return false;
    //检查出生日期是否合法
    var sbirthday = cardidstr.substr(6, 4) + "/" + cardidstr.substr(10, 2) + "/" + cardidstr.substr(12, 2);
    var date = new Date(sbirthday);
    return sbirthday == date.getFullYear() + "/" + (date.getMonth() + 1) + "/" + date.getDate();
}
//验证是不是邮箱
function IsEmail(control, allownull) { var str = jQuery(control).val().trim(); return (allownull && str.length == 0) || (str.length && /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/i.test(str)) }
//验证是不是MSN
function IsMSN(control, allownull) { var str = jQuery(control).val().trim(); return (allownull && str.length == 0) || (str.length && /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/i.test(str)) }
//验证长度
function CheckLength(control, len) { var str = jQuery(control).val().trim(); return str.length >= len }
//验证长度等于多少
function CheckEqualsLeng(control, len) { var str = jQuery(control).val().trim(); return str.length == len }
//验证Int
function IsInt(control, min, max, allownull) { var str = jQuery(control).val().trim(); return (allownull && str.length == 0) || ((/^\d*$/i.test(str)) && !isNaN(parseInt(str)) && parseInt(str) >= parseInt(min) && parseInt(str) <= parseInt(max)) }
//验证QQ
function IsQQ(control, allownull) { var str = jQuery(control).val().trim(); return (allownull && str.length == 0) || (str.length && (/^\d*$/i.test(str)) && str.length >= 5 && str.length <= 11) }
//验证float
function IsFloat(control, allownull) { var str = jQuery(control).val().trim(); return (allownull && str.length == 0) || (str.length && (/^\d+(\.\d+)?$/i.test(str))) }
//验证是否是 正负整数
function IsNumber(control, allownull) { var str = jQuery(control).val().trim(); return (allownull && str.length == 0) || (str.length && (/^-?\d+\.?\d*$/i.test(str))) }

function GetClass(result) { return result ? "msgOK" : "msgError" }
//比较字符串
function Compare(control) { return jQuery(control).val().trim() == jQuery("#" + jQuery(control).attr("id") + "Re").val().trim() }
//验证函数
function CheckValue(control) {
    var result = false, control = jQuery(control), controlId = control.attr("id"), controlTip = control.attr("tip"), validatetype = control.attr("validatetype"), controlValue = control.val(), checkresult = false;
    if (validatetype == "no") { ChangeMessageClass(control, "msgOK"); return true; }
    //进行分解
    var validate = validatetype, min = 1, max = 10000, para;
    if (validatetype.indexOf("if") > -1) {
        if (controlValue == "") {
            validate = validatetype.replace("if", "");
        }
        else {
            validate = validatetype.replace("if", "");
        }
    }
    if (validatetype.indexOf("_") > -1) {
        para = validatetype.replace("if", "").replace("?", "").split("_");
        validate = para[0];
        if (para.length == 2) min = para[1];
        if (para.length == 3) { min = para[1]; max = para[2]; }
    }
    if (validate.indexOf("compare") >= 0) {
        result = Compare(control, validate.indexOf("?") >= 0);
        ChangeMessageClass(control, GetClass(result))
        return result;
    }
    if (validate.indexOf("isurl") >= 0) {
        result = IsUrl(control, validate.indexOf("?") >= 0);
        ChangeMessageClass(control, GetClass(result))
        return result;
    }
    if (validate.indexOf("isnull") >= 0) {
        result = IsNull(control, min, max, validate.indexOf("?") >= 0);
        ChangeMessageClass(control, GetClass(result))
        return result;
    }
    if (validate.indexOf("isno") >= 0) {
        if (control.val() != "") {
            result = IsNull(control, min, max, validate.indexOf("?") >= 0);
            ChangeMessageClass(control, GetClass(result))
            return result;
        }
    }
    if (validate.indexOf("isphone") >= 0) {
        result = IsPhone(control, min, validate.indexOf("?") >= 0);
        ChangeMessageClass(control, GetClass(result))
        return result;
    }
    if (validate.indexOf("isphone_1") >= 0) {
        result = IsPhoneOrIsMobile(control, validate.indexOf("?") >= 0);
        ChangeMessageClass(control, GetClass(result))
        return result;
    }
    if (validate.indexOf("ismobile") >= 0) {
        result = IsMobile(control, validate.indexOf("?") >= 0);
        ChangeMessageClass(control, GetClass(result))
        return result;
    }
    if (validate.indexOf("ispostcode") >= 0) {
        result = IsPostcode(control, validate.indexOf("?") >= 0);
        ChangeMessageClass(control, GetClass(result))
        return result;
    }
    if (validate.indexOf("isqq") >= 0) {
        result = IsQQ(control, validate.indexOf("?") >= 0);
        ChangeMessageClass(control, GetClass(result))
        return result;
    }
    if (validate.indexOf("ismsn") >= 0) {
        result = IsMSN(control, validate.indexOf("?") >= 0);
        ChangeMessageClass(control, GetClass(result))
        return result;
    }

    if (validate.indexOf("isidnumber") >= 0) {
        result = IsIdNumber(control, validate.indexOf("?") >= 0);
        ChangeMessageClass(control, GetClass(result))
        return result;
    }
    if (validate.indexOf("isemail") >= 0) {
        result = IsEmail(control, validate.indexOf("?") >= 0);
        ChangeMessageClass(control, GetClass(result))
        return result;
    }
    if (validate.indexOf("isint") >= 0) {
        result = IsInt(control, min, max, validate.indexOf("?") >= 0);
        ChangeMessageClass(control, GetClass(result))
        return result;
    }
    if (validate.indexOf("isfloat") >= 0) {
        result = IsFloat(control, validate.indexOf("?") >= 0);
        ChangeMessageClass(control, GetClass(result))
        return result;
    }
    if (validate.indexOf("isnumber") >= 0) {
        result = IsNumber(control, validate.indexOf("?") >= 0);
        ChangeMessageClass(control, GetClass(result))
        return result;
    }
    if (validate.indexOf("ishttpurl") >= 0) {
        result = IsHttpUrl(control, validate.indexOf("?") >= 0);
        ChangeMessageClass(control, GetClass(result))
        return result;
    }
}
function CheckForm(f) {
    f = f == undefined || f == false ? document.forms[0] : f;
    var controls = f.elements;
    for (var i = 0; i < controls.length; i++) {
        var control = jQuery(controls[i]);
        var controlTip = control.attr("tip");
        if (controlTip != null && !CheckValue(control)) {
            return false;
        }
    }
    return true;
}
function ChangeMessageClass(control, className) {
    var control = jQuery(control), messageTip = jQuery('*[id="' + control.attr("id") + 'Tip"]');
    if (messageTip.length == 0) messageTip = jQuery('*[id="*' + control.attr("id") + 'Tip"]');
    if (messageTip.length) {
        messageTip.removeClass('msgOK'); messageTip.removeClass('msgError'); messageTip.removeClass('msgOnFocus'); messageTip.addClass(className);
        var tip = control.attr("tip"), warning = control.attr("warning"), error = control.attr("error");
        if ((className == 'msgNormal' || className == 'msgOnFocus') && warning != null) {
            warning = warning == "" ? " " : warning + "<br/>";
            messageTip.html(warning + tip);
        }
        else if (className == 'msgError' && error != null) {
            error = error == "" ? " " : error + "<br/>";
            messageTip.html(error + tip);
        }
        else {
            messageTip.html(tip);
        }
    }
}
function InitForm() {
    jQuery("form").each(function () {
        var f = jQuery(this), el = jQuery("*[tip]", f);
        f.bind("submit", function (e) { return CheckForm(this) });
        if (el.length) {
            el.each(function (i) {
                var control = jQuery(this), messageTip = jQuery('*[id="' + control.attr("id") + 'Tip"]');
                if (messageTip.length == 0) messageTip = jQuery('*[id="*' + control.attr("id") + 'Tip"]');
                if (messageTip.length) {
                    try {
                        var tip0 = control.attr("tip"); if (tip0 == null || tip0 == "") { control.attr("tip", jQuery.trim(messageTip.text())); tip0 = control.attr("tip"); }
                        var tip1 = jQuery.trim(messageTip.text()); if (tip1 == null || tip1 == "") { messageTip.text(tip0); }
                        control.blur(function () { CheckValue(this) }).focus(function () { ChangeMessageClass(this, "msgOnFocus") });
                        if (!messageTip.hasClass("msgNormal")) { messageTip.addClass("msgNormal"); }
                    } catch (e) { }
                }
            });
        }
    });

}
