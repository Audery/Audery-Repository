﻿/*
* Compressed by JSA(www.xidea.org)
*/
(function ($) { $.fn.jChinaArea = function (D) { D = $.extend({ aspnet: false, s1: null, s2: null, s3: null }, D || {}); var _ = $(this), K = $("select", _), F = K.eq(0), I = K.eq(1), J = K.eq(2), E = new Location(); F.empty(); I.empty(); J.empty(); E.fillOption(F, "0", D.s1); if (D.s1 != null && D.s1 != "") E.fillOption(I, "0," + F.val(), D.s2); if (D.s2 != null && D.s2 != "") E.fillOption(J, "0," + F.val() + "," + I.val(), D.s3); E.initOption(F, "", "请选择", D.s1 == D.s2 && D.s2 == D.s3); if (I && I.length) E.initOption(I, "", "请选择", D.s1 == D.s2 && D.s2 == D.s3); if (J && J.length) E.initOption(J, "", "请选择", D.s1 == D.s2 && D.s2 == D.s3); if (D.aspnet) { var H = $("input", _), B = H.eq(0), G = H.eq(1), A = H.eq(2); C() } F.click = function () { }; F.change(function () { I.empty(); E.fillOption(I, "0," + F.val()); E.initOption(I, "", "请选择", 1); J.empty(); E.initOption(J, "", "请选择", 1); E.fillOption(J, "0," + F.val() + "," + I.val()); if (D.aspnet) C() }); I.change(function () { J.empty(); E.fillOption(J, "0," + F.val() + "," + I.val()); E.initOption(J, "", "请选择", 1); if (D.aspnet) C() }); J.change(function () { if (D.aspnet) C() }); function C() { B.val($(":selected", F).text()); G.val($(":selected", I).text()); A.val($(":selected", J).text()) } } })(jQuery)