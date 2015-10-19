function reloadimg() {
    document.getElementById("imgCode").src = "/include/captcha.ashx?r=" + Math.random();
}

if (!('placeholder' in document.createElement('input'))) {
    $('.placeholder').find('input[placeholder], textarea[placeholder]').each(function (k, v) {
        var $obj = $(v),
                val = $obj.val(),
                placeholder = $obj.attr('placeholder');

        if (val == '') {
            $obj.val(placeholder);
        }

        $obj.focus(function () {
            if ($obj.val() === placeholder) {
                $obj.val('');
            }
        }).blur(function () {
            val = $obj.val();
            if (val == '' || val == placeholder) {
                $obj.val(placeholder);
            }
        });
    });
}

function onUser() {

    if ($("#UserName").val() == "") {
        $("#UserName").removeClass("input_ok");
        $("#s1").removeClass("reg_ok").removeClass("reg_error");
        $("#s1").hide();
        return false;
    }
    if ($("#UserName").val().length != 11) {
        //if (document.getElementById("UserName").className == "reg_ok") {
        //    $("#s1").addClass("reg_error");
        // }
        //else {
        $("#s1").removeClass("reg_ok").addClass("reg_error");
        // }
        $("#s1").html("您输入的手机号码格式不正确!");
        $("#s1").show();
        $("#UserName").removeClass("input_ok").addClass("input_error");
        return false;
    }
    else {
        $.ajax({
            type: 'POST',
            url: '/Account/LogOnCheck',
            data: { act: "ExistsUserName", UserName: $("#UserName").val() },
            dataType: "json",
            async: false,
            success: function (msg) {
                if (parseInt(msg) != 0) {
                    $("#s1").removeClass("reg_ok").addClass("reg_error");
                    $("#s1").html("<a href='logon'>立即登录</a> <a href='FindPassword'>忘记密码?</a> 该手机号已经注册过！");
                    $("#UserName").removeClass("input_ok").addClass("input_error");
                    $("#s1").show();
                    return false;
                }
                else {
                    $("#UserName").removeClass("input_error").addClass("input_ok");
                    $("#s1").hide();
                    return true;
                }
            }
        });
    }
    return true;
}

$(document).ready(function () {
    $("#UserName").blur(function () {
        if ($(this).val() == "") {
            $("#s1").hide();
        }
        else {
            onUser();
        }
    });
    $("#UserName").focus(function () {
        var s = "#s1";
        $(s).removeClass("reg_error").addClass("reg_ok");
        $(s).html("请输入11位的手机号码");
        $(s).show();
    });
    $("#PassWord").focus(function () {
        var s = "#s2";
        $(s).removeClass("reg_error").addClass("reg_ok");
        $(s).html("6-20位字符，可使用字母、数字或符号的组合");
        $(s).show();
    });
    $("#PassWord").blur(function () {
        var s = "#s2";
        if ($(this).val().length < 6 && $(this).val() != "") {
            $(s).addClass("reg_error").removeClass("reg_ok");
            $(this).removeClass("input_ok").addClass("input_error");
            $(s).html("密码长度要大于等于6个字符！");
            $(s).show();
        }
        else if ($(this).val() == "") {
            $(s).hide();
            $(this).removeClass("input_error").removeClass("input_ok");
        }
        else {
            $(s).removeClass("reg_error").addClass("reg_ok");
            $(this).removeClass("input_error").addClass("input_ok");
            $(s).hide();
        }
    });
    $("#ConfirmPassword").focus(function () {
        var s = "#s3";
        $(s).removeClass("reg_error").addClass("reg_ok");
        $(s).html("请再次输入密码");
        $(s).show();
    });
    $("#ConfirmPassword").blur(function () {
        var s = "#s3";
        if ($(this).val().length < 6 && $(this).val() != "") {
            $(this).removeClass("input_ok").addClass("input_error");
            $(s).html("密码长度要大于等于6个字符！");
            $(s).removeClass("reg_error").addClass("reg_ok");
            $(s).show();
        }
        else if ($("#PassWord").val() == $(this).val()) {
            $(this).removeClass("input_error").addClass("input_ok");
            $(s).hide();
        }
        else {
            $(this).removeClass("input_ok").addClass("input_error");
            $(s).html("两次输入的密码内容要一致！");
            $(s).removeClass("reg_ok").addClass("reg_error");
            $(s).show();
        }
        if ($(this).val() == "" && $("#PassWord").val() == "") {
            $(this).removeClass("input_ok").removeClass("input_error");
            $(s).removeClass("reg_error").removeClass("reg_ok");
            $(s).hide();
        }

    });
    $("#LinkMan").focus(function () {
        var s = "#s4";
        $(s).removeClass("reg_error").addClass("reg_ok");
        $(s).html("请填写真实姓名，以便审核及时通过!");
        $(s).show();
    });
    $("#LinkMan").blur(function () {
        var s = "#s4";
        if ($(this).val() == "") {
            $(this).removeClass("input_error").removeClass("input_ok");
            $(s).removeClass("reg_error").removeClass("reg_ok");
            $(s).hide();
        }
        else {
            $(this).removeClass("input_error").addClass("input_ok");
            $(s).removeClass("reg_error").removeClass("reg_ok");
            $(s).hide();
        }
    });

    $("#province").blur(function () {
        var s = "#s6";
        $(s).removeClass("input_c_ok").removeClass("input_c_error");
        $(s).hide();
    });

    $("#city").blur(function () {
        var s = "#s6";
        $(s).removeClass("input_c_ok").removeClass("input_c_error");
        $(s).hide();
    });

    $("#county").blur(function () {
        var s = "#s6";
        if ($(this).val() == "") {
            $(s).removeClass("input_c_ok").addClass("input_c_error");
            $(s).show();
        }
        else {
            var area_Selector = document.getElementById("ChinaArea");
            //var s6_width = area_Selector.offsetWidth - 20 + "px";
            $(s).removeClass("input_c_error").addClass("input_c_ok");
            //$(s).css("padding-left", s6_width);
            //$(s).css("margin-left", s6_width);
            $(s).show();
            $(s).css("display", "");
        }
    });

    $("#CompanyClass").blur(function () {
        var s = "#s8";
        if ($(this).val() == "") {
            $(s).removeClass("input_c_ok").addClass("input_c_error");
            $(s).show();
        }
        else {
            $(s).removeClass("input_c_error").addClass("input_c_ok");
            $(s).show();
        }
    });

    $("#Email").focus(function () {
        var s = "#s5";
        if ($.trim($(this).val()) == "") $(this).removeClass("input_ok").removeClass("input_error");
        $(s).removeClass("reg_error").addClass("reg_ok");
        $(s).html('请填写有效电子邮箱，推荐使用<span style="color:#d32503">QQ邮箱</span>');
        $(s).show();
    });

    $("#Email").blur(function () {
        var s = "#s5";
        if ($.trim($(this).val()) != "") {
            Email();
        }
        else {
            $(this).removeClass("input_ok").addClass("input_error");
            $(s).removeClass("reg_ok").addClass("reg_error");
            $(s).html('邮箱不能为空，推荐使用<span style="color:#d32503">QQ邮箱</span>');
            $(s).show();
        }
    });


    $("#Captcha").blur(function () {
        var s = "#s7";
        if ($(this).val() == "") {
            $(s).removeClass("input_c_ok").addClass("input_c_error");
            $(s).show();
        }
        else {
            $.ajax({
                type: 'POST',
                url: '/Account/LogOnCheck',
                async: false,
                data: { act: "ExistsCaptcha", Captcha: $("#Captcha").val() },
                dataType: "json",
                success: function (msg) {
                    if (parseInt(msg) == 0) {
                        $(s).removeClass("input_c_ok").addClass("input_c_error");
                        $(s).show();
                    }
                    else {
                        $(s).removeClass("input_c_error").addClass("input_c_ok");
                        $(s).show();
                        $("#UserName").blur();
                        $("#PassWord").blur();
                        $("#Email").blur();
                        $("#ConfirmPassword").blur();
                    }
                }
            });
        }
    });

    $("#CompanyName").blur(function () {
        var s = "#s9";
        if ($(this).val() == "") {
            $(s).removeClass("input_c_ok").addClass("input_c_error");
            $(s).show();
        }
        else {
            $(s).removeClass("input_c_error").addClass("input_c_ok");
            $(s).show();
        }
    });
});

function checksubmit() {
    ret = true;  
    var u = $("#UserName").val();
    var p = $("#PassWord").val();
    var cp = $("#ConfirmPassword").val();
    var l = $("#LinkMan").val();
    var province = $("#province").val();
    var city = $("#city").val();
    var c = $("#county").val();
    var y = $("#Captcha").val();
    var e = $("#Email").val();
    var q = $("#CompanyClass").val();
    var companyName = $("#CompanyName").val();

    if (u.length != 11) {
        $("#s1").removeClass("reg_ok").addClass("reg_error");
        $("#s1").html("您输入的手机号码格式不正确!");
        $("#s1").show();
        $("#UserName").removeClass("input_ok").addClass("input_error");
        ret = false;
    }
    else {
        //alert(document.getElementById("UserName").className);
        if ($("#UserName").attr("class").indexOf("ok") == -1) {
            ret = false;
        }

    }
    var s = "#s2";
    if (p == "" || p.length < 6) {
        $(s).addClass("reg_error").removeClass("reg_ok");
        $("#PassWord").removeClass("input_ok").addClass("input_error");
        $(s).html("密码长度要大于等于6个字符！");
        $(s).show();
        ret = false;
    }
    else {
        if (document.getElementById("PassWord").className.indexOf("ok") == -1) {
            ret = false;
        }
    }
    s = "#s3";
//    if (cp == "" || cp.length < 6) {
//        $(s).addClass("reg_error").removeClass("reg_ok");
//        $("#ConfirmPassword").removeClass("input_ok").addClass("input_error");
//        $(s).html("确认密码长度要大于等于6个字符！");
//        $(s).show();
//        ret = false;
//    }
//    else {
//        if (document.getElementById("ConfirmPassword").className.indexOf("ok") == -1) {
//            ret = false;
//        }
//    }
    if (cp != p) {
        $(s).addClass("reg_error").removeClass("reg_ok");
        $("#ConfirmPassword").removeClass("input_ok").addClass("input_error");
        $(s).html("两次输入的密码要一致!");
        $(s).show();
        ret = false;
    }

    s = "#s4";
    if (l == "") {
        $("#LinkMan").removeClass("input_ok").addClass("input_error");
        $(s).removeClass("reg_ok").addClass("reg_error");
        $(s).html("请填写真实姓名，以便审核及时通过！");
        $(s).show();
        ret = false;
    }
    else {
        if (document.getElementById("LinkMan").className.indexOf("ok") == -1) {
            ret = false;
        }
    }
    s = "#s6";
    if (c == "" || c == "请选择") {
        $(s).removeClass("input_c_ok").addClass("input_c_error");
        $(s).show();
        ret = false;
    }
    else {
        if (document.getElementById("s6").className.indexOf("ok") == -1) {
            ret = false;
        }
    }

    s = "#s8";
    if (q == "") {
        $(s).removeClass("input_c_ok").addClass("input_c_error");
        $(s).show();
        ret = false;
    }


    s = "#s7";
    if (y == "") {
        $(s).removeClass("input_c_ok").addClass("input_c_error");
        $(s).show();
        ret = false;
    }
    else {
        if (document.getElementById("s7").className.indexOf("ok") == -1) {
            ret = false;
        }
    }
    s = "#s5";
    if (e == "") {
        $("#Email").removeClass("input_ok").addClass("input_error");
        $(s).removeClass("reg_ok").addClass("reg_error");
        $(s).html('邮箱不能为空，推荐使用<span style="color:#d32503">QQ邮箱</span>');
        $(s).show();
        ret = false;
    }
    else {
        if (document.getElementById("s5").className.indexOf("ok") == -1) {
            ret = false;
        }
    }
    if (ret) {
        $(".RegisterForm").submit();
    }
    s = "#s9";
    if (companyName == "" || companyName == "请输入企业名称") {
        $(s).removeClass("input_c_ok").addClass("input_c_error");
        $(s).show();
        ret = false;
    }

    if (ret) {
        $(".RegisterForm").submit();
    }     
    return ret;
}

//邮箱验证
function Email() {
    //alert(isEmail($s("Email").value));
    var s = "#s5";
    var em = "#Email";
    if ($(em).val() != "") {
        if ($s("Email").value.indexOf("@") == -1 || !isEmail($s("Email").value)) {
            $(em).removeClass("input_ok").addClass("input_error");
            $(s).removeClass("reg_ok").addClass("reg_error");
            $(s).html('您输入的邮箱格式不正确，推荐使用<span style="color:#d32503">QQ邮箱</span>');
            $(s).show();
            return false;
        }
    } else {
        $(s).html('邮箱不能为空，推荐使用<span style="color:#d32503">QQ邮箱</span>');
        $(s).show();
        return false;
    }
    if (onEmail()) {
        $(s).hide();
        return true;
    }
    else {
        return false;
    }

}
function isEmail(obj) {
    //reg = /^\w{3,}@\w+(\.\w+)+$/;
    reg = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (reg.test(obj)) {
        return true;
    } else {
        return false;
    }
}
function onEmail() {
    var s = "#s5";
    var em = "#Email";
    if ($s("Email").value != "") {
        if ($s("Email").value.indexOf("@") == -1 || !isEmail($s("Email").value)) {
            $(em).removeClass("input_ok").addClass("input_error");
            $(s).removeClass("reg_ok").addClass("reg_error");
            $(s).html('您输入的邮箱格式不正确，推荐使用<span style="color:#d32503">QQ邮箱</span>');
            $(s).show();
            return false;
        } else {
            $.ajax({
                type: 'POST',
                url: '/Account/LogOnCheck',
                data: { act: "ExistsEMail", Email: $("#Email").val() },
                dataType: "json",
                success: function (msg) {

                    if (parseInt(msg) == 1) {
                        $(em).removeClass("input_ok").addClass("input_error");
                        $(s).removeClass("reg_ok").addClass("reg_error");
                        $(s).html("该邮箱已经存在，请您更换邮箱！");
                        $(s).show();
                        return false;
                    }
                    else {
                        $(em).removeClass("input_error").addClass("input_ok");
                        $(s).hide();
                    }
                }
            });
        }
    } else {
        $(em).removeClass("input_ok").addClass("input_error");
        $(s).removeClass("reg_ok").addClass("reg_error");
        $(s).html('邮箱不能为空，推荐使用<span style="color:#d32503">QQ邮箱</span>');
        $(s).show();
        return false;
    }
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

function xieyi() {
    $(".submit_register_ok").toggle();
}

function $s(id) { return document.getElementById(id); }

//只输入数字
$.fn.numeral = function () {
    $(this).css("ime-mode", "disabled");
    this.bind("keypress", function () {
        //alert(event.keyCode);
        if (event.keyCode == 46) {
            //if (this.value.indexOf(".") != -1) {
            return false;
            //}
        } else {
            return event.keyCode >= 46 && event.keyCode <= 57;
        }
    });
    this.bind("blur", function () {
        if (this.value.lastIndexOf(".") == (this.value.length - 1)) {
            this.value = this.value.substr(0, this.value.length - 1);
        } else if (isNaN(this.value)) {
            this.value = "";
        }
    });
    this.bind("paste", function () {
        var s = clipboardData.getData('text');
        if (!/\D/.test(s));
        value = s.replace(/^0*/, '');
        return false;
    });
    this.bind("dragenter", function () {
        return false;
    });
    this.bind("keyup", function () {
        if (/(^0+)/.test(this.value)) {
            this.value = this.value.replace(/^0*/, '');
        }
    });
};

//限制为输入的内容为数字
$("#UserName").numeral();