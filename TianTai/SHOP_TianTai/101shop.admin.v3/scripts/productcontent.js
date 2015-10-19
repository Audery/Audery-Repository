function isNumberStr(s) {
    var p = /^[0-9]+$/;
    return p.test("" + s);
}
//文本数-1 ，最小等于1
function setAmount_reduce(str) {
    if ($(str).val() <= 1) {
        return;
    }
    else {
        $(str).val($(str).val() - 1);
    }
}
//文本数+1 ，最大等于999
function setAmount_add(str) {
    $(str).val(parseInt($(str).val()) + 1);
}
//添加购物车
function shoppingcart_add(quantity, pro_ID) {
    var ckb_products = ""; 
    if (quantity != undefined && pro_ID != undefined) {//单选
        if (isNaN(parseInt(pro_ID)) || parseInt(pro_ID) < 1) return;
        quantity = isNaN(parseInt(quantity)) ? $(quantity).val() : quantity;
        if (isNaN(parseInt(quantity)) || parseInt(quantity) < 1) return alert("请先填写购买数量！");
        ckb_products = pro_ID + "," + quantity;
    } else {//多选
        var ckb_product = $('input[name="ckb_product"]');
        if (ckb_product.length == 0) return;
        ckb_product.each(function () { if(this.checked) ckb_products += this.id + "," + 1 + ";"; });
        if (ckb_products == "") return alert("请先选择要采购的商品！");
    }
    //保存
    jQuery.ajax({
        type: "POST", url: "/product/shoppingcart.aspx?is_ajax=1", async: false, data: "Option=addCart&pro_list=" + ckb_products, dataType: "text",
        success: function (res) {
            try {
                if (res.indexOf('login') == 0) {
                    if (window.confirm("请先登陆后再进行您的操作，是否去登陆？")) { window.location = "/membercenter/login.aspx?redirect_url=" + encodeURIComponent(document.URL); }
                } else if (res.indexOf('ok') == 0) {
                    shoppingcart_added();
                } else { }
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
//成功添加购物车
function shoppingcart_added() {
    GetShoppingCartList(function (m, l, c, data) {
        var CartInfo = $("#CartInfo").show();
        CartInfo.find(".quantity").html("" + l);
        CartInfo.find(".price").html("" + c);
    });
}
//添加收藏夹
function favorite_add(pro_ID) {
    //保存
    jQuery.ajax({
        type: "POST", url: "/productContent.aspx?is_ajax=1&id=1", async: false, data: "option=f&proid=" + pro_ID, dataType: "text",
        success: function (res) {
            try {
                if (res.indexOf('login') == 0) {
                    if (window.confirm("请先登陆后再进行您的操作，是否去登陆？")) { window.location = "/membercenter/login.aspx?redirect_url=" + encodeURIComponent(document.URL); }
                } else if (res.indexOf('ok') == 0) {
                    favorite_added();
                } else {
                    window.alert("您收藏的商品数目已经达到了上限(50个)；如果还需要收藏，请先删除一部分收藏的商品。");
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
//成功添加收藏夹
function favorite_added() {
    var FavoriteInfo = $("#FavoriteInfo").show();
        jQuery.ajax({
            type: "POST", url: "/productContent.aspx?is_ajax=1&id=1", async: false, data: "option=fc&proid=", dataType: "text",
            success: function (res) {
                try {
                    FavoriteInfo.find(".quantity").html("" + res);
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
//加载页面后处理
jQuery(document).ready(function () {
    //一月销量排行榜
    var $ = jQuery, yzx_xbt = $(".yzx_xbt").children(), tsMb = $(".tsMb").children(), s_cls = "yzx_xbtwx,yzx_xbtxz".split(',');
    if (yzx_xbt.length == tsMb.length) {
        yzx_xbt.each(function (i) { tsMb.each(function () { var o = $("li", this).eq(0), s = o.siblings(); o.find("div").show(); s.find("div").hide(); }); $(this).attr("i", i).hover(function () { var c = parseInt($(this).attr("i")); yzx_xbt.attr("class", s_cls[0]); yzx_xbt.eq(c).attr("class", s_cls[1]); tsMb.hide(); tsMb.eq(c).show(); }, function () { }); });
        $("li", tsMb).hover(function () { var o = $(this), s = o.siblings(); o.find("div").show(); s.find("div").hide(); }, function () { });
    }
    //----------------
    $("#ss_nyxl").css("display", "");
    ///判断是否输入数字
    var pamount = $("#pamount");
        pamount.keyup(function (e) {
            if (!isNumberStr($(this).val())) { $(this).val($(this).val().replace(/[^\d]/g, "")); }
        });
        pamount.blur(function (e) {
            if ($("#pamount").val() == "" || $("#pamount").val() == "0") {
                $("#pamount").val("1");
            }
        });
});
