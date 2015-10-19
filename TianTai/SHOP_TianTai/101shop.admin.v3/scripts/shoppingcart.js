//删除购物车中的商品
function DeleteShoppingCart(pro_ID) {
    jQuery.ajax({
        type: "POST", url: "shoppingcart.aspx?is_ajax=1", async: false, data: "Option=delCart&pro_list=" + pro_ID, dataType: "text",
        success: function (res) {
            try {
                if (res.indexOf('login') == 0) {
                    window.location = "/membercenter/login.aspx?redirect_url=" + encodeURIComponent(document.URL);
                } else {
                    window.location.reload(); window.location = document.URL;
                }
            } catch (e) { } return true;
        },
        error: function (x, e) {
            alert("服务器连接失败！");
        },
        complete: function (x) {
            //alert(x.responseText);
        }
    });
}
//更改购物车中的商品数量
function SetBuyCount(input, pro_ID, pro_Stock, pro_State) {
    var quantity = parseInt(input.value);
    if (isNaN(quantity) || quantity <= 0) {
        alert("请填写正确的采购数量！"); input.focus(); return false;
    }
    if (quantity == parseInt(jQuery(input).attr("count"))) return true;
    jQuery.ajax({
        type: "POST", url: "shoppingcart.aspx?is_ajax=1", async: false, data: "Option=updatecart&pro_list=" + pro_ID + "," + quantity, dataType: "text",
        success: function (res) {
            try {
                if (res.indexOf('login') == 0) {
                    window.location = "/membercenter/login.aspx?redirect_url=" + encodeURIComponent(document.URL);
                } else {
                    window.location.reload(); window.location = document.URL;
                }
                CancShoppingCartQuantity();
            } catch (e) { } return true;
        },
        error: function (x, e) {
            alert("服务器连接失败！");
        },
        complete: function (x) {
            //alert(x.responseText);
        }
    });
}
//点击后结算
function ShoppingCart() {
    if (parseFloat(jQuery("#ShoppingCartAllPrice").html()) > 0) {
        window.location = "paymentorder.aspx";
    } else {
        alert("请先选择购买产品，并填写购买数量。在进行下一步！");
    }
}
//点击后返回继续购物
function ShoppingCartBackHome() {
    try{window.location = ShoppingCart_History == null ? SiteHttp : ShoppingCart_History;}catch(e){ }
}
//验证购物车功能
function CancShoppingCartQuantity() {
    var q = jQuery("#ShoppingCartQuantity"), c = jQuery("#ShoppingCartAllPrice"), cs = 0;
    var qs = jQuery(".gwc_nr_nr");
    qs.each(function (i) {
        var price = parseFloat(jQuery("span.price", this).text()), objcount = jQuery("input.count", this), count = parseInt(objcount.val());
        objcount.attr("count", count);
        cs += price * count;
    });
    q.html(qs.length); c.html(cs.toFixed(2));
    if (qs.length == 0) {
        jQuery("#ToOrder").hide();
    }
}
jQuery(document).ready(CancShoppingCartQuantity);
