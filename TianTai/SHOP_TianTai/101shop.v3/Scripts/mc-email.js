$(document).ready(function () {
    $(".topC0").click(function () {
        $(".topC0").removeClass("topC1");
        $(this).addClass("topC1");
        $(".NewsTop_cnt .d_01").hide();
        $(".NewsTop_cnt .d_01").eq($(this).index()).show();
    });
});

function CheckForm(fx) {
    $(fx).attr("disabled", "disabled");
    var ret = true;
    f = $("#form1");
    //alert($('#Step2Table').length);
    if ($('#Step1Table').length == 1) {
        f.action = "EmailUpdate1";
        var Email = $.trim($('#Email').val());
        var Captcha = $.trim($('#Captcha').val());
        if (Email == '') {
            alert('请填写邮箱地址！');
            ret = false;
        }
        if (Captcha == '') {
            alert('请填写验证码！');
            ret = false;
        }
    }
    else if ($('#Step2Table').length == 1) {
        f.action = "EmailUpdate2";
        var Email = $.trim($('#Email').val());
        var Captcha = $.trim($('#Captcha').val());
        if (Email == '') {
            alert('请填写邮箱地址！');
            ret = false;
        }
        else if (document.getElementById("Email").className.indexOf("error") != -1) {
            alert('邮箱地址错误！');
            ret = false;
        }
        if (Captcha == '') {
            alert('请填写验证码！');
            ret = false;
        }
        else if (document.getElementById("Captcha").className.indexOf("error") != -1) {
            alert('验证码错误！');
            ret = false;
        }

    }
    if (!ret) {
        $(fx).attr("disabled", "");
    }
    return ret;
}
function onCheck() {
    $.ajax({
        type: 'POST',
        url: '/Account/LogOnCheck',
        data: { act: "ExistsCaptcha", Captcha: $("#Captcha").val() },
        dataType: "json",
        success: function (msg) {
            if (parseInt(msg) == 0) {
                alert("您输入的验证码不正确");
                //$("#Captcha").focus();
                $("#codes").addClass("input_c_error");
            }
            else {
                $("#codes").removeClass("input_c_error");
            }

        }
    });
}
//邮箱验证
function sEmail() {

    var s = "#s5";
    var em = "#Email";
    if ($(em).val() != "") {
        if ($(em).val().indexOf("@") == -1 || !isEmail($(em).val())) {           
        $(s).addClass("input_c_error");
        $(s).html("您输入Email格式不正确!");
        $(s).show();
        alert("您输入Email格式不正确!");
        return false;
          }

    } else {
    alert("请输入你的邮箱地址!");
    $(s).addClass("input_c_error");
         $(s).html("请输入你的邮箱地址!");
        $(s).show();
        return false;
    }
    if (onEmail()) {
        $(s).removeClass("input_c_error");
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
    if ($(em).val() != "") {
        if ($(em).val().indexOf("@") == -1 || !isEmail($(em).val())) {
            $(s).addClass("input_c_error");
            //$(s).html("您输入Email格式不正确!");
            alert("您输入Email格式不正确!");
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
                    $(s).addClass("input_c_error");
                    //$(s).html("该邮箱已经存在，请您更换邮箱！");
                    alert("该邮箱已经存在，请您更换邮箱！");
                    $(s).show();
                    return false;
                }
                else {
                    $(em).removeClass("input_c_error");
                    $(s).hide();
                }
            }
        });
        }
    }
    return true;
}