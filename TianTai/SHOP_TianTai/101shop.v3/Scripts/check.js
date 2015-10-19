function reloadimg() {
    document.getElementById("imgCode").src = "/include/captcha.ashx?r=" + Math.random();
}
function onPhone() {
    if ($("#UserName").val().length == 11) {
    //    $.ajax({
    //        type: 'POST',
    //        url: '/Account/LogOnCheck',
    //        data: { act: "ExistsUserName", UserName: $("#UserName").val() },
    //        dataType: "json",
    //        success: function (msg) {
    //            if (parseInt(msg) == 1) {
    //                $("#UserName").removeClass().addClass("ok");

    //                return true;
    //            }
    //            else {
    //                $("#UserName").removeClass("ok").addClass("error");
    //                $("#ValidationSummary").hide();
    //                $("#hdAttention").html("您输入的手机号码不存在，请核对后重新输入");
    //                $("#hdAttention").show();

    //                return false;
    //            }
    //        }
        //    });
        $("#UserName").removeClass().addClass("ok");

        return true;
    }
    else {
        $("#UserName").removeClass("ok").addClass("error");
        $("#ValidationSummary").hide();
        $("#hdAttention").html("您输入的手机号码不合法，请核对后重新输入");
        $("#hdAttention").show();

        return false;
    }
}

function onPass() {
    if ($("#PassWord").val().length>5) {
        $("#PassWord").removeClass("error").addClass("ok");
        $("#hdAttention").hide();

        return true;
    }
    else {
        $("#PassWord").removeClass("ok").addClass("error");
        $("#ValidationSummary").hide();
        $("#hdAttention").html("您输入密码少于6位，请重新输入");
        $("#hdAttention").show();

        return false;
    }
}

function onCaptcha() {
    if ($("#Captcha").val().length == 4) {
        $("#Captcha").removeClass("error").addClass("ok");
        $("#hdAttention").hide();

        return true;
    }
    else {
        $("#Captcha").removeClass("ok").addClass("error");
        $("#ValidationSummary").hide();
        $("#hdAttention").html("您输入的验证码有误，请重新输入");
        $("#hdAttention").show();

        return false;
    }
}

//是否有错误标识
function isError(xid) {
    //alert(document.getElementById(xid).className.indexOf("error"));
    if (document.getElementById(xid).className.indexOf("error") == -1) {
        return false;
    }
    else {
        return true;
    }
}

//判断值是否为空
function checkValIsEmmpty(id) {
    var id_obj = document.getElementById(id);
    var value = id_obj.value;
    if (value == "" || value == undefined) {
        return true;
    }
    else {
        return false;
    }
}

//是否是默认的字符
function isDefultChar(xid) {
    alert(xid);
    alert($("#" + xid).val()+ " "+ $("#" + xid).attr("placeholder"));
    if ($("#" + xid).val() == $("#" + xid).attr("placeholder")) {
        $("#" + xid).removeClass("ok").addClass("error");
        return true;
    }
    else {
        return false;
    }
}

function onCheck() {
    $.ajax({
        type: 'POST',
        url: '/Account/LogOnCheck',
        data: { act: "ExistsCaptcha", Captcha: $("#Captcha").val() },
        dataType: "json",
        success: function (msg) {
            if (parseInt(msg) == 0) {
                $("#Captcha").removeClass("ok").addClass("error");
                $("#ValidationSummary").hide();
                $("#hdAttention").html("您输入的验证码有误，请重新输入");
                $("#hdAttention").show();
            }
            else {
                $("#Captcha").removeClass("error").addClass("ok");
                $("#hdAttention").hide();
            }
        }
    });
}
var _checksubmit = 0;//提交表单
function checksubmit() {
    if (onPhone() && onPass() && onCaptcha()) {
        if (!checkValIsEmmpty("UserName") && !checkValIsEmmpty("PassWord") && !checkValIsEmmpty("Captcha")) {
            if (_checksubmit == 0 && !isError("UserName") && !isError("PassWord") && !isError("Captcha")) {
                _checksubmit = 1;
                return true;
            }
            else {
                return false;
            }
        }
        else if (checkValIsEmmpty("UserName")) {
            $("#ValidationSummary").hide();
            $("#hdAttention").html("请输入手机号码");
            $("#hdAttention").show();

            return false;
        }
        else if (checkValIsEmmpty("PassWord")) {
            $("#ValidationSummary").hide();
            $("#hdAttention").html("请输入密码");
            $("#hdAttention").show();

            return false;
        }
        else if (checkValIsEmmpty("Captcha")) {
            $("#ValidationSummary").hide();
            $("#hdAttention").html("请输入验证码");
            $("#hdAttention").show();

            return false;
        }
        else {
            $("#hdAttention").hide();
        }
    }
    else {
        return false;
    }
}

//忘记密码处理
function fp_check_submit() {
    var p = $("#phone").val();
    var c = $("#code").val();
    if (p == "") {
        $("#s1").html("请输入您注册时用的手机号码");
        $("#s1").show();
        return false;
    }
    else {
        if ($("#s1").css("display") == "block") {
            return false;
        }
    }
    if (c == "") {
        $("#s2").html("请输入验证码");
        $("#s2").show();
        return false;
    }
    else {
        if ($("#s2").css("display") == "block") {
            return false;
        }
    }
    return true;
}

function fp_check_sms_submit() {
    var p = $("#smscode").val();    
    if (p == "") {
        $("#s2").html("请输入验证码");
        $("#s2").show();
        return false;
    }
    else {
        if ($("#s2").css("display") == "block") {
            return false;
        }
    }

    return true;
}

function fd_foucs(xid) {
    $("#" + xid).html("请输入您注册时用的手机号码");
    $("#" + xid).show();
}

function fd_blur(xid) {
    $("#" + xid).hide();
    $.ajax({
        type: 'POST',
        url: '/Account/LogOnCheck',
        data: { act: "ExistsUserName", UserName: $("#phone").val() },
        dataType: "json",
        success: function (msg) {
            if (parseInt(msg) == 1) {
                $("#s1").hide();
            }
            else {
                $("#s1").html("您输入的手机号码不存在，请核对后重新输入。");
                $("#s1").show();                
            }
        }
    });
}

function fd_code_blur(xid) {
    var cr=$("#"+xid).val();
    if (cr == "") {
        $("#s2").html("验证码不能为空");
        $("#s2").show();
    }
    else {
        $.ajax({
            type: 'POST',
            url: '/Account/LogOnCheck',
            data: { act: "ExistsCaptcha", Captcha: cr },
            dataType: "json",
            success: function (msg) {
                if (parseInt(msg) == 0) {
                    $("#s2").html("验证码错误");
                    $("#s2").show();
                }
                else {
                    $("#s2").hide();
                }
            }
        });
    }
}

function sendcode() {
    $.ajax({
        type: 'POST',
        url: '/Account/GetSms',
        data: { act: "ExistsCaptcha", phone: $("#sphone").val() },
        dataType: "json",
        success: function (msg) {
            if (msg.state == 0) {
                $("#fp_btn_sms").hide();
                timers();
            }
            else {
                alert(msg.message);
            }
        }
    });
}

function fp_pass_submit() {
    if ($("#pass").val() == $("#passc").val() && $("#passc").val() != "" && $("#passc").val().length>5) {
        return true;
    }
    else {
        $("#sp").show();
        $("#sp").html("密码长度需大于5个字符，并且两次输入的内容需一致");
    }
    return false;
}

function timers() {
    var s = 120; var sh = setInterval(function () { s = s - 1; if (s < 0) { $("#fp_btn_sms").show(); clearInterval(sh); $("#ss1").hide(); } else { var ss = s > 60 ? "1分"+(s-60)+"秒" : s+"秒"; $("#ss1").html("验证码已经发送到你的手机上，" + ss + "内未收到短信，请重新获取！"); $("#ss1").show().css("padding-left","3px"); } }, 1000);
}
