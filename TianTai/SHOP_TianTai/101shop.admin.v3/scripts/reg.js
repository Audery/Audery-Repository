function $s(id) { return document.getElementById(id); }
function getindex() {
    var selectType = document.getElementById("question");
    document.getElementById("hfQuestion").value = selectType.options[selectType.selectedIndex].value;
}
window.onerror = function () {
    return true;
}

function onName() {
    if ($s("regusername").value == "") {

        $s("spname").innerHTML = "<img src='/images/check_error.gif'/>&nbsp;此项为必填项，请输入您的用户名！";
        $s("spname").style.color = "#000000";
        return false;
    }
    else if ($s("regusername").value.length < 6 || $s("regusername").value.length > 20) {
        $s("spname").innerHTML = "<img src='/images/check_error.gif'/>&nbsp;您的用户名长度只能在6-20位字符之间!";
        $s("spname").style.color = "#000000";
        return false;
    }
    else {
        var param = "Option=Main&UserName=" + $s("regusername").value;
        var options = {
            method: 'post',
            parameters: param,
            onComplete:
        function (transport) {
            var retv = transport.responseText;
            if (retv != "") {
                $s("spname").innerHTML = "<img src='/images/check_error.gif'/>&nbsp;" + retv + "";
            }
            else {
                $s("spname").innerHTML = "<img src='/images/check_right.gif'/>";
            }
            $s("spname").style.color = "#000000";
        }
        }
        new Ajax.Request('../filehandle/userregform.ashx', options);
    }
}
function Userpwd() {
    if ($s("reguserpwd").value == "") {
        $s("sppwd").innerHTML = "<img src='/images/check_error.gif'/>&nbsp;此项为必填项，请设置您的密码！";
        $s("sppwd").style.color = "#000000";
        alert("请设置您的密码"); $s("reguserpwd").focus();
        return false;
    }
    else if ($s("reguserpwd").value.length < 6 || $s("reguserpwd").value.length > 16) {
        $s("sppwd").innerHTML = "<img src='/images/check_error.gif'/>&nbsp;您设置密码长度只能在6-16位字符之间!";
        $s("sppwd").style.color = "#000000";
        alert("您设置密码长度只能在6-16位字符之间"); $s("reguserpwd").focus();
        return false;
    }
    else {
        return true;
    }
}
function onUserpwd() {
    if ($s("reguserpwd").value == "") {
        $s("sppwd").innerHTML = "<img src='/images/check_error.gif'/>&nbsp;此项为必填项，请设置您密码！";
        $s("sppwd").style.color = "#000000";
        return false;
    }
    else if ($s("reguserpwd").value.length < 6 || $s("reguserpwd").value.length > 16) {
        $s("sppwd").innerHTML = "<img src='/images/check_error.gif'/>&nbsp;您设置密码长度只能在6-16位字符之间!";
        $s("sppwd").style.color = "#000000";
        return false;
    }
    else {
        $s("sppwd").innerHTML = "<img src='/images/check_right.gif'/>";
    }
}
function Repwd() {
    if ($s("regrepwd").value == "" && $s("reguserpwd").value == "") {
        alert("请输入确认密码"); $s("regrepwd").focus();
        return false;
    }
    if ($s("regrepwd").value != $s("reguserpwd").value) {
        $s("sprepwd").innerHTML = "<img src='/images/check_error.gif'/>&nbsp;两次输入的密码不一致！";
        $s("sprepwd").style.color = "#000000";
        alert("两次输入的密码不一致"); $s("regrepwd").focus();
        return false;
    }
    else {
        return true;
    }
}
function onRepwd() {
    if ($s("regrepwd").value == "") return false;
    if ($s("regrepwd").value != $s("reguserpwd").value) {
        $s("sprepwd").innerHTML = "<img src='/images/check_error.gif'/>&nbsp;两次输入的密码不一致！";
        $s("sprepwd").style.color = "#000000";
        return false;
    }
    else {
        $s("sprepwd").innerHTML = "<img src='/images/check_right.gif'/>";
    }
}
function oninsolution() {
    if ($s("question").value == "") {
        $s("spquestion").innerHTML = "<img src='/images/check_error.gif'/>&nbsp;此项为必选项，请选择密码保护问题！";
        $s("spquestion").style.color = "#000000";
        return false;
    }
    else {
        $s("spquestion").innerHTML = "<img src='/images/check_right.gif'/>";
    }
    if ($s("insolution").value == "") {
        $s("spinsolution").innerHTML = "<img src='/images/check_error.gif'/>&nbsp;此项为必填项，请输入答案！";
        $s("spinsolution").style.color = "#000000";
        return false;
    }
    else if ($s("insolution").value.length < 2 || $s("insolution").value.length > 20) {
        $s("spinsolution").innerHTML = "<img src='/images/check_error.gif'/>&nbsp;您答案长度只能在2-20位字符之间!";
        $s("spinsolution").style.color = "#000000";
        return false;
    }
    else {
        $s("spinsolution").innerHTML = "<img src='/images/check_right.gif'/>";
    }

}
function TrueName() {
    if ($s("TrueName").value == "") {
        $s("spTrueName").innerHTML = "<img src='/images/check_error.gif'/>&nbsp;此项为必填项，请输入联系人姓名！";
        $s("spTrueName").style.color = "#000000";
        alert("请输入联系人姓名"); $s("TrueName").focus();
        return false;
    }
    else if ($s("TrueName").value.length < 2 || $s("TrueName").value.length > 16) {
        $s("spTrueName").innerHTML = "<img src='/images/check_error.gif'/>&nbsp;联系人姓名长度只能在2-16位字符之间!";
        $s("spTrueName").style.color = "#000000";
        alert("联系人姓名长度只能在2-16位字符之间"); $s("TrueName").focus();
        return false;
    }
    else {
        $s("spTrueName").innerHTML = "<img src='/images/check_right.gif'/>";
    }
    return true;
}
function ChinaArea() {
    var a1 = $s("province"), a2 = $s("city");
    if (a1.selectedIndex <= 0 || a2.selectedIndex <= 0) {
        alert("请选择你的地区(省、市为必选项)"); if (a2.selectedIndex <= 0) a2.focus(); if (a1.selectedIndex <= 0) a1.focus();
        return false;
    }
    var v1 = a1.options[a1.selectedIndex].value, v2 = a2.options[a2.selectedIndex].value;
    if (v1 == "" || v2 == "") {
        $s("spChinaArea").innerHTML = "<img src='/images/check_error.gif'/>&nbsp;此项为必填项，请选择你的地区！";
        $s("spChinaArea").style.color = "#000000";
        alert("请选择你的地区(省、市为必选项)"); if (a2.selectedIndex <= 0) a2.focus();if (a1.selectedIndex <= 0) a1.focus();
        return false;
    }
    else {
        $s("spChinaArea").innerHTML = "<img src='/images/check_right.gif'/>";
    }
    return true;
}
//提交
function RegionVerify() {
    //验证手机、邮箱、密码等
    if (Phone() && Userpwd() && Repwd() && TrueName() && Email() && ChinaArea()) {
//        if ($s("txtCode").value.length != 5) {
//            alert('验证码错误！！！');$s("txtCode").focus();
//            return false;
//        }
        $s("myreg").action = "../filehandle/userregform.ashx";
        document.forms[0].submit();
        return true;
    }
    else {
        return false;
    }
}
//邮箱验证
function Email() {
    if ($s("Email").value != "") {
        if ($s("Email").value.indexOf("@") == -1 || !IsEmail("#Email")) {
            $s("spEmail").innerHTML = "<img src='/images/check_error.gif'/>&nbsp;您输入Email格式不正确!";
            $s("spEmail").style.color = "#000000";
            alert("您输入Email格式不正确"); $s("Email").focus();
            return false;
        }
    } else {
        $s("spEmail").innerHTML = "<img src='/images/check_error.gif'/>&nbsp;请输入你的邮箱地址!";
        $s("spEmail").style.color = "#000000";
        alert("请输入你的邮箱地址");$s("Email").focus();
        return false;
    }
    if ($s("spEmail").innerHTML.indexOf("error") > 0) {
        alert("请输入你的邮箱地址"); $s("Email").focus();
        return false;
    }
    else {
        return true;
    }
}
function onEmail() {
    if ($s("Email").value != "") {
        if ($s("Email").value.indexOf("@") == -1 || !IsEmail("#Email")) {
            $s("spEmail").innerHTML = "<img src='/images/check_error.gif'/>&nbsp;您输入Email格式不正确!";
            $s("spEmail").style.color = "#000000";
            return false;
        } else {
            jQuery.ajax({
                type: "POST", url: "../filehandle/userregform.ashx?is_ajax=1", async: false, data: "Option=Email&Email=" + $s("Email").value, dataType: "text",
                success: function (retv) {
                    try {
                        if (retv != "") {
                            if (retv.indexOf('已存在') != -1) {
                                $s("spEmail").innerHTML = "<img src='/images/check_error.gif'/>&nbsp;此邮箱地址已被使用！";
                            } else {
                                $s("spEmail").innerHTML = "<img src='/images/check_error.gif'/>&nbsp;" + retv + "";
                            }
                            return false;
                        }
                        else {
                            $s("spEmail").innerHTML = "<img src='/images/check_right.gif'/>";
                            return true;
                        }
                    } catch (e) { }
                },
                error: function (x, e) {
                    //alert("服务器连接失败！");
                },
                complete: function (x) {
                    //alert(x.responseText);
                }
            });
        }
    }
    $s("spEmail").style.color = "#999999";
    $s("spEmail").innerHTML = "请输入你的邮箱地址（选填）";
    return true;
}
//手机验证
function Phone() {
    //手机号码验证，验证13系列和150-159(154除外)、180、185、186、187、188、189几种号码，长度11位
    if ($s("Phone").value == "") {
        $s("spPhone").innerHTML = "<img src='/images/check_error.gif'/>&nbsp;此项为必填项，请输入手机号码！";
        $s("spPhone").style.color = "#000000";
        $s("sendCode").className = "svc-disabled";
        alert("请输入手机号码"); $s("Phone").focus();
        return false;
    }
    if (($s("Phone").value.length != 11) || !IsMobile('#Phone', false)) {
        $s("spPhone").innerHTML = "<img src='/images/check_error.gif'/>&nbsp;您输入的手机号码格式不正确!";
        $s("spPhone").style.color = "#000000";
        $s("sendCode").className = "svc-disabled";
        alert("您输入的手机号码格式不正确"); $s("Phone").focus();
        return false;
    }
    if ($s("Phone").value.length == 11) { 
        if ($s("spPhone").innerHTML.indexOf("error") == -1) {
            $s("sendCode").className = "svc";
            return true;
        }
    }
    $s("sendCode").className = "svc-disabled";
    alert("请输入手机号码"); $s("Phone").focus();
    return false;
}
function onPhone() {
    //手机号码验证，验证13系列和150-159(154除外)、180、185、186、187、188、189几种号码，长度11位
    if ($s("Phone").value == "") {
        $s("spPhone").innerHTML = "<img src='/images/check_error.gif'/>&nbsp;此项为必填项，请输入手机号码！";
        $s("spPhone").style.color = "#000000";
        $s("sendCode").className = "svc-disabled";
        return false;
    }
    if (($s("Phone").value.length != 11) || !IsMobile('#Phone', false)) {
        $s("spPhone").innerHTML = "<img src='/images/check_error.gif'/>&nbsp;您输入的手机号码格式不正确!";
        $s("spPhone").style.color = "#000000";
        $s("sendCode").className = "svc-disabled";
        return false;
    }
    else {
        $s("sendCode").className = "svc-disabled";
        jQuery.ajax({
            type: "POST", url: "../filehandle/userregform.ashx?is_ajax=1", async: false, data: "Option=Phone&Phone=" + $s("Phone").value, dataType: "text",
            success: function (retv) {
                try {
                    if (retv != "") {
                        if (retv.indexOf('已存在') != -1) {
                            $s("spPhone").innerHTML = "<img src='/images/check_error.gif'/>&nbsp;<a href='login.aspx' style='color:blue'>此手机号的用户已注册成功，请登陆！</a>";
                            setCookie("username", $s("Phone").value, 30);
                        } else {
                            $s("spPhone").innerHTML = "<img src='/images/check_error.gif'/>&nbsp;" + retv + "";
                        }
                        return false;
                    }
                    else {
                        $s("spPhone").style.color = "#000000";
                        $s("spPhone").innerHTML = "<img src='/images/check_right.gif'/>";
                        if ($s("sendCode").innerHTML == "获取验证码") {
                            $s("sendCode").className = "svc";
                        }
                        return true;
                    }
                } catch (e) { }
            },
            error: function (x, e) {
                //alert("服务器连接失败！");
            },
            complete: function (x) {
                //alert(x.responseText);
            }
        });
    }
}

function sendPhoneCode() {
    if ($s("sendCode").innerHTML == "获取验证码" && $s("sendCode").className == "svc")
    {
        jQuery.ajax({
            type: "POST", url: "../filehandle/userregform.ashx?is_ajax=1", async: false, data: "Option=sendCode&Phone=" + $s("Phone").value, dataType: "text",
            success: function (retv) {
                try {
                    if (retv != "true") alert("发送失败");
                } catch (e) { }
            },
            error: function (x, e) {
                //alert("服务器连接失败！");
            },
            complete: function (x) {
                //alert(x.responseText);
            }
        });
        time();
    }
}
var sec = 60;
function time() {
    if (sec > 0) {
        jQuery("#sendCode").text(sec + "秒后重新获取");
        setTimeout("time()", 1000);
        jQuery("#sendCode").addClass("svc-disabled");
    }
    else {
        jQuery("#sendCode").text("重新获取验证码");
        jQuery("#sendCode").removeClass("svc-disabled");
        jQuery("#sendCode").addClass("svc");
        sec = 61;
    }
    jQuery("#sendCode").css("width", 120);
    jQuery("#sendCode").css("background", "url('/images/200949_719516335.png') no-repeat scroll 0 0 transparent");
    sec--;
}
setTimeout(function () { if ($s('Phone').value == "") { $s('Phone').focus(); } else { $s('reguserpwd').value = ""; $s('reguserpwd').focus(); } }, 2000);

function setCookie(name, value, date) {
    var name = escape(name);
    var value = escape(value);
    var expires = new Date();
    expires.setTime(expires.getTime() + date * 24 * 3600000);
    _expires = (typeof hours) == "string" ? "" : ";expires=" + expires.toUTCString();
    document.cookie = name + "=" + value + _expires;
}
//获取cookie值
function getCookieValue(name) {
    var name = escape(name);
    //读cookie属性，这将返回文档的所有cookie
    var allcookies = document.cookie;
    //查找名为name的cookie的开始位置
    name += "=";
    var pos = allcookies.indexOf(name);
    //如果找到了具有该名字的cookie，那么提取并使用它的值
    if (pos != -1) {                                             //如果pos值为-1则说明搜索"version="失败
        var start = pos + name.length;                  //cookie值开始的位置
        var end = allcookies.indexOf(";", start);        //从cookie值开始的位置起搜索第一个";"的位置,即cookie值结尾的位置
        if (end == -1) end = allcookies.length;        //如果end值为-1说明cookie列表里只有一个cookie
        var value = allcookies.substring(start, end); //提取cookie的值
        return unescape(value);                           //对它解码      
    }
    else return "";                               //搜索失败，返回空字符串
}
