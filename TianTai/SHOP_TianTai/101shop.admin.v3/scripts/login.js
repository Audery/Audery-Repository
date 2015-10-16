function CheckLogin(userName, passWrod, Valitar, checkbox1, checkbox2) {
    var uname = userName.value;
    var pwd = passWrod.value;
    var vali = Valitar.value;
    if (uname == "") {
        alert("温馨提示：手机号不能为空！");
        userName.focus();
        return false;
    }
    if (uname.length != 11 || !(/^(13[0123456789]|14[57]|15[012356789]|18[0256789])\d{8}$/i.test(uname))) {
        alert("温馨提示：您输入的手机号码格式不正确！");
        userName.focus();
        return false;
    }
    if (pwd == "") {
        alert("温馨提示：密码不能为空！");
        passWrod.focus();
        return false;
    }
    if (vali == "") {
        alert("温馨提示：验证码不能为空！");
        Valitar.focus();
        return false;
    }
    if (checkbox1.checked) {
        setCookie("username",uname,30);
    }
    doAjax("ConMain", "txtLoginName=" + uname + "&txtPassword=" + pwd + "&txtCode=" + vali, false,
            function (retv) {
                if (retv.indexOf("true") == 0) {
                    var url = document.referrer, home = "../", i = document.URL.indexOf("redirect_url=");
                    if (retv.indexOf("true-OTC") == 0) {
                        var urlOtc = home;
                        if (retv.replace("true-OTC", "") != "") urlOtc = retv.replace("true-OTC", "");
                        window.location = urlOtc;
                    } else if (url == "" || url.indexOf("getpass_") > 0 || document.URL.indexOf("getpass_") > 0) {
                        window.location = home;
                    } else if (url.indexOf("membercenter/") > 0) {
                        window.location = home + "membercenter/";
                    } else if (i > 0) {
                        try { window.location = "loginFinsh.aspx?" + retv.split('?')[1]; }
                        catch (e) { window.location = home; }
                    } else {
                        window.location = url;
                    }
                }
                else {
                    if (retv != "") alert(retv);
                    document.getElementById("imgChange").src = "../filehandle/ValidateCode.ashx?abc=" + Math.random();
                }
            });
}

function doAjax(a, d, c, b) { jQuery.ajax({ type: "POST", url: "../filehandle/loginform.ashx", async: c, data: "Option=" + a + "&" + d, dataType: "text", success: function (e) { b(e) }, complete: function (e) { if (e.responseBody != undefined) { /*alert(e.responseBody)*/ } } }) };
function loaduser() { var a = $("#txtLoginName"), b = $("#txtPassword"); a.val(getCookieValue("username")); a.val() != "" ? b.val("")[0].focus() : a[0].focus() };
function submit() { var el = function (id) { return document.getElementById(id) }; CheckLogin(el("txtLoginName"), el("txtPassword"), el("txtCode"), el("checkbox1"), el("checkbox2"));}
document.onkeydown = function (e) { if (!e) e = window.event; if ((e.keyCode || e.which) == 13) submit();}
function setCookie(c, d, b) { var c = escape(c); var d = escape(d); var a = new Date(); a.setTime(a.getTime() + b * 24 * 3600000); _expires = (typeof hours) == "string" ? "" : ";expires=" + a.toUTCString(); document.cookie = c + "=" + d + _expires } function getCookieValue(b) { var b = escape(b); var c = document.cookie; b += "="; var f = c.indexOf(b); if (f != -1) { var e = f + b.length; var a = c.indexOf(";", e); if (a == -1) { a = c.length } var d = c.substring(e, a); return unescape(d) } else { return "" } };
