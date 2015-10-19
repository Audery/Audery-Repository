function SendAjax(a,b,c){jQuery.ajax({url:a,type:"post",dataType:"text",data:b,async:false,success:c})};
jQuery(document).ready(function () {
    jQuery(".sellType").each(function () {
        var a = jQuery(this), b = jQuery.trim(a.text());
        SendAjax("../../filehandle/productSelect.ashx", "Option=GetSellTypeName&id=" + b, function (d, c) { a.html("<font color=Black>" + d + "</font>") })
    })
});
