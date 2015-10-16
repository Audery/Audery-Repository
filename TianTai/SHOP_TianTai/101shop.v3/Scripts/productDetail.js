$(document).ready(function () {
    //彩页样盒切换
    var imgSwitch = jQuery(".imgSwitch a");
    imgSwitch.each(function () {
        var a = jQuery(this);
        var f1 = (function (t) {
            var a = jQuery(t),
            json = a.attr("json"),
            ok = (json != null && json.toString().toLowerCase().indexOf(".jpg") > 0);
            return ok;
        });
        var f2 = function () {
            var a = jQuery(this),
            ok = (f1(this) || (a.attr("href").toString().toLowerCase().indexOf(".jpg") < 0));
            if (ok) return false;
        };
        a.hover(function () {
            if (f1(this)) jQuery("#lb_img").attr("src", jQuery(this).attr("json"));
        },
        function () { });
        a.click(f2);
        a.dblclick(f2);
    });
    //说明书切换
    var topC1 = "topC1",
    topC0 = jQuery(".topC0");
    topC0.each(function () {
        var d_01 = jQuery(".d_01"),
        s_1 = "s_1";
    });
});
//Ajax
function SendAjax(data, success_function) {
    var aurl = '/include/ajax.ashx?is_ajax=true';
    jQuery.ajax({
        url: aurl,
        type: "post",
        dataType: "text",
        data: data,
        async: false,
        success: success_function
    });
}
//检查购买的数量
function CheckQuantity(input) {
    if ($(input).length == 0) return false;
    var input = $(input);    
    var quantity = parseFloat(input.val());
    var minsell = parseFloat(input.attr('minsell'));
    var maxsell = parseFloat(input.attr('maxsell'));
    if (isNaN(minsell)) minsell = 1;
    if (isNaN(quantity) || quantity < minsell) {
        input.val(minsell);
        return false;
    } else if (maxsell != 0) {
        if (quantity > maxsell) {
            input.val(maxsell);

            return false;
        } else {
            return true;
        }
    } else {
        return true;
    }
}
//添加购买的数量
function AddQuantity(quantity, plus) {
    var pid = parseFloat($('#Product_ID').val()),
    pcs = parseFloat($.trim($('#Goods_Pcs').text())),
    zbz = parseFloat($.trim($('#Goods_Pcs_Small').text())),
    sellType = parseFloat($.trim($('#sellType').val()));
    var quantity1 = $(quantity + pid);

    if (!isNaN(pid) && pid > 0 && !isNaN(pcs) && pcs > 0) {
        var chk = CheckQuantity(quantity1),
        quantity = parseFloat(quantity1.val());
        var minsell = parseFloat(quantity1.attr('minsell'));
        if (isNaN(minsell)) minsell = 1;
        var maxsell = parseFloat(quantity1.attr('maxsell'));

        if (!isNaN(zbz) && zbz > 0 && sellType == 2) {
            plus = plus * zbz;
        }

        if (chk) {
            if (quantity + plus < minsell) {
                quantity1.val(minsell.toFixed(2));
            } else if (maxsell != 0) {
                if ((quantity + plus >= minsell) && (quantity + plus < maxsell)) {
                    quantity1.val((quantity + plus).toFixed(2));
                } else {
                    quantity1.val(maxsell.toFixed(2));
                }
            } else {
                quantity1.val((quantity + plus).toFixed(2));
            }
        }

        return chk;
    } else {
        alert("系统异常，请刷新网页后再试。");
        quantity1.attr('disabled', 'disabled');

        return false;
    }
}
function AddQuantity1(plus) {
    return AddQuantity('#h-', plus);
}
function AddQuantity2() {
    return AddQuantity('#j-', 1);
}
function DelQuantity1(plus) {
    return AddQuantity('#h-', -plus);
}
function DelQuantity2() {
    return AddQuantity('#j-', -1);
}