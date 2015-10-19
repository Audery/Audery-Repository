function goToPay() {
    //    if (member.uid == 0) {
    //        loginAttention();
    //    }
    //    else {
    window.location = '/Shoppingcart/MyShoppingCart';
    //    }
}

//排序
function sort(x) {
    s = "sort=" + x;
    sumurl(s, "sort");
}

function list(x) {
    s = "show=" + x;
    sumurl(s, "show");
}

function sumurl(s, index) {
    pr = "";
    a = false;
    url = document.URL.split('?');
    if (url.length > 1) {
        p = url[1].split('&');
        for (i = 0; i < p.length; i++) {
            if (p[i].indexOf(index) >= 0) {
                p[i] = s;
                a = true;
            }
            if (i == 0) {
                pr = pr + "?" + p[i];
            }
            else {
                pr = pr + "&" + p[i];
            }
        }
    }
    if (a) {
        document.location.href = url[0] + pr;
    }
    else {
        if (document.URL.indexOf('?') > 0) {
            document.location.href += "&" + s;
        }
        else {
            document.location.href += "?" + s;
        }

    }
}
//去掉用户的搜索的条件
function deleteSearch() {
    url = document.URL.split('?');
    if (url.length > 1) {
        p = url[1].split('&');
        alert(p + p.length);
        for (j = 0; j < p.length; j++) {
            if (p[j].indexOf("p=") >= 0) {
                pr = pr + "&" + p[i];
            }
        }
        alert(pr);
        document.location.href = url[0] + pr;
    }

}

//检查购买数量
function checkSellType(input) {
    input = $(input), val = parseFloat(input.val());
    if (isNaN(val) || val <= 0) {
        input.val(input.attr("oldvalue"));
        return "请填写大于零的购买数量";
    }
    var maxsell = parseFloat(input.attr("maxsell"));
    if (!isNaN(maxsell) && maxsell > 0 && maxsell < val) {
        input.val(input.attr("oldvalue"));
        return "不能超过最大购买数量【" + maxsell + "】";
    }
    var selltype = parseInt(input.attr("selltype")), jz = parseFloat(input.attr("jz")), zbz = parseFloat(input.attr("zbz"));
    if (isNaN(selltype)) { selltype = 1; } if (isNaN(jz) || jz < 1) { jz = 1; } if (isNaN(zbz) || zbz < 1) { zbz = 1; }
    if (selltype == 3 && val % jz > 0) {
        //input.val(input.attr("oldvalue"));
        //return "购买数量必须是件装量【" + jz + "】的整数倍";
    }
    else if (selltype == 2 && val % zbz > 0) {
        input.val(input.attr("oldvalue"));
        return "购买数量必须是中包装【" + zbz + "】的整数倍";
    }
    return "";
}

//计算件装数量
function henum(pid) {
    var p = $("#h-" + pid);
    minsell = p.attr("minsell");
    var maxsell = p.attr("maxsell");

    //检查购买数量
    var err = checkSellType(p.get(0));
    if (err) {
        alert(err); return false;
    }

    s = parseFloat(p.val());

    if (s < parseFloat(minsell)) {
        p.val(minsell);
    }
    if (parseFloat(maxsell) != 0) {
        if (s > parseFloat(maxsell)) {
            p.val(maxsell);
        }
    }
    /*计算件数隐藏
    jz = parseInt($("#jz-" + pid).val());
    j = $("#j-" + pid).val() == "" ? 0 : parseInt($("#j-" + pid).val());
    //alert(s + " " + jz + " " + j);
    if (jz <= s) {
    js = Math.floor(s / jz);
    $("#j-" + pid).val((j + js));
    $("#h-" + pid).val(s - js * jz);
    }*/
}

function jhenum(pid) {
    minsell = $("#j-" + pid).attr("minsell");
    var maxsell = $("#h-" + pid).attr("maxsell");

    s = parseInt($("#j-" + pid).val());
    if (s < parseInt(minsell)) {
        $("#j-" + pid).val(minsell);
    }
    else if (s > parseInt(maxsell)) {
        $("#j-" + pid).val(maxsell);
    }
    /*计算件数隐藏
    jz = parseInt($("#jz-" + pid).val());
    j = $("#j-" + pid).val() == "" ? 0 : parseInt($("#j-" + pid).val());
    //alert(s + " " + jz + " " + j);
    if (jz <= s) {
    js = Math.floor(s / jz);
    $("#j-" + pid).val((j + js));
    $("#h-" + pid).val(s - js * jz);
    }*/
}

//增加数量
function add(pid, plus) {
    var snum = 0; var input = $("#h-" + pid);
    var val = parseFloat(input.val());
    var selltype = parseInt(input.attr("selltype"));
    var jz = parseFloat(input.attr("jz"));
    var zbz = parseFloat(input.attr("zbz"));
    minsell = input.attr("minsell");
    if (isNaN(val)) {
        val = parseFloat(minsell);
        if (isNaN(val)) { val = 1; }
    }
    s = input.val();
    if (zbz < 1) {
        zbz = 1;
    }
    if (selltype == 3) {
        snum = val + jz;
    } else if (selltype == 2) {
        snum = val + zbz;
    } else {
        snum = val + plus;
    }
    if (s == "") {
        input.val(minsell.toFixed(2));
    }
    else {
        input.val(snum.toFixed(2));
    }
    henum(pid);
}
//减少数量
function sub(pid, plus) {
    var snum = 0;
    var input = $("#h-" + pid);
    var val = parseFloat(input.val());
    var selltype = parseInt(input.attr("selltype"));
    var jz = parseFloat(input.attr("jz"));
    var zbz = parseFloat(input.attr("zbz"));
    minsell = input.attr("minsell");
    if (isNaN(val)) {
        val = parseFloat(minsell);
        if (isNaN(val)) {
            val = 1;
        }
    }
    s = input.val();
    if (zbz < 1) {
        zbz = 1;
    }
    if (selltype == 3) {
        snum = val - jz;
    } else if (selltype == 2) {
        snum = val - zbz;
    } else {
        snum = val - plus;
    }
    if (val >= parseFloat(minsell) && snum >= parseFloat(minsell) && snum > 0) {
        $("#h-" + pid).val(snum.toFixed(2));
    }
}

//件数量控制
function jadd(pid) {
    s = $("#j-" + pid).val();
    if (s == "") {
        $("#j-" + pid).val("1");
    }
    else {
        $("#j-" + pid).val(parseInt(s) + 1);
    }
}

function jsub(pid) {
    s = $("#j-" + pid).val() == "" ? 1 : parseInt($("#j-" + pid).val());
    if (s > 1) {
        $("#j-" + pid).val(s - 1);
    }
}

//添加购物车到服务器
function addshoppingcart(proid, jian, he, cls, expiration) {
    //if (cls==1) {
    //    var content = "<div class='demo' style='width:240px;height:90px;padding-top:17px;'><div style='margin-left:20px;margin-right:20px;color:red;'>亲！订单1000元以上享9.8折优惠哟！</div><div>&nbsp;</div><div><a href='javascript:void(0)' class='wBox_close' style='font-size:13px;margin-left:20px;'><span style='padding-right:2px;'><</span>继续购物</a> <a class='cart_js' href='/Shoppingcart/MyShoppingCart' style='font-size:13px;margin-left:10px; color:#fff;'>去购物车结算</a></div></div>";
    //}
    //else {
    var content = "<div class='demo' style='width:300px;height:90px;padding-top:20px;'><div><a href='javascript:void(0)' class='wBox_close' style='font-size:16px;margin-left:60px;color:red'><span style='padding-right:2px;font-size:14px;color:red'><</span>继续购物</a> <a class='cart_js' href='/Shoppingcart/MyShoppingCart' style='font-size:13px;margin-left:20px; color:#fff;'>去购物车结算</a></div></div>";
    //}
    $.ajax({
        type: 'POST',
        url: '/Shoppingcart/Addcart',
        data: { pid: proid, jian: jian, count: he, expiration: expiration },
        dataType: "json",
        success: function (msg, textStatus) {
            switch (msg["state"]) {
                case 0:
                    var wBox = $("#wbox1").wBox({
                        title: msg["message"],
                        html: content,
                        timeout: 10000
                    });
                    wBox.showBox();
                    $(".shopping-amount").html(parseInt($(".shopping-amount").html()) + 1);
                    break;
                case 1:
                    loginAttention();
                    break;
                case 2:
                    alert(msg["message"]);
                    document.location.href = msg.url;
                    break;
                case 7:
                    alert(msg["message"]);
                    document.location.href = msg.url;
                    break;
                case 10: //四部控销品种
                    kong();
                    break;
                default:
                    alert(msg["message"]);
                    if (msg["message"].indexOf("整数倍") > 0 && /\/\d+\.html$/.test(location.href)) {
                        location.href = location.href + "?clearCache=1";
                    }
                    break;
            }

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown + " error");
            // 通常情况下textStatus和errorThown只有其中一个有值 
            this; // the options for this ajax request
        }
    });
}

//加入购物车
function addShopCar(proid, cls, expiration) {
    if (false) {//if (member.uid == 0) {//四部控销售品种，没登陆也要执行
        loginAttention();
    }
    else {
        jianshu = $("#j-" + proid).val();
        he = $("#h-" + proid).val();
        var maxsell = $("#h-" + proid).attr("maxsell");
        var stock = $("#h-" + proid).attr("stock");
        var pro_name = $("#h-" + proid).attr("pro_name");
        var memberclass = $("#h-" + proid).attr("memberclass");
        var count, jian;
        if (undefined == he) {
            count = 0;
        }
        else {
            count = parseInt(he);
            if (isNaN(count)) {
                alert("请输入数字！");
                return false;
            }
        }

        if (undefined == jianshu) {
            jian = 0;
        }
        else {
            jian = parseInt(jianshu);
        }
        if (jianshu == 0 && he == 0) {
            alert("请先输入您要购买的数量，再放入购物车！");
        }
        else {
            addshoppingcart(proid, jianshu, he, cls, expiration);
        }
    }
}
function otc_AddShopCar(proid, minsell, jz, zbz, sellType) {
    if (false) {//if (member.uid == 0) { 四部控销售品种，没登陆也要执行
        loginAttention();
    }
    else {
        minsell = minsell > 1 ? minsell : 1;
        if (sellType == 2 && zbz > 0 && zbz > minsell) { minsell = zbz; }
        addshoppingcart(proid, 0, minsell);
    }
}


//列表展开
function openlist(xid) {
    $(xid).toggle();
    if ($(xid + "1").html() == "展开") {
        $(xid + "1").html("收起");
        $(xid + "1").removeClass("sx_last").addClass("sx_last_1");
    }
    else {
        $(xid + "1").html("展开");
        $(xid + "1").removeClass("sx_last_1").addClass("sx_last");
    }


}

//放入收藏夹
function shore(pid) {
    if (member.uid > 0 && !isNaN(pid) && pid > 0) {
        SendAjax('act=AddFavorite&pid=' + pid,
                function (res) {
                    if (res == '1') {
                        alert("成功关注该商品。");

                        return true;
                    } else {
                        alert("添加关注失败！");
                        return false;
                    }
                });
    } else if (member.uid == 0) {
        loginAttention();
    } else {
        alert("操作失败！");
        return false;
    }
}

//Ajax
function SendAjax(data, success_function) { var aurl = '/include/ajax.ashx?is_ajax=true'; jQuery.ajax({ url: aurl, type: "post", dataType: "text", data: data, async: false, success: success_function }); }

//只输入数字
$.fn.numeral = function () {
    $(this).css("ime-mode", "disabled");
    var oldv = $(this).attr("oldvalue"); if (oldv == null || oldv == "") { $(this).attr("oldvalue", $(this).val()); }
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
    this.bind("focus", function () {
        $(this).attr("oldvalue", $(this).val());
    });
    this.bind("blur", function () {
        if (this.value == "") { this.value = $(this).attr("oldvalue"); return false; }
        if (this.value.lastIndexOf(".") == (this.value.length - 1)) {
            this.value = this.value.substr(0, this.value.length - 1);
        }

        //检查购买数量
        var err = checkSellType(this); if (err) { alert(err); return false; }
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

//热销图片切换
function showlist(xi) {
    $(".hr").removeClass("hr").addClass("hh");
    $("#hli-" + xi).removeClass("hh").addClass("hr");
    $(".hotx").hide();
    $("#hot-" + xi).show();
    $(".shuzi").css("background", "");
    $("#sli-" + xi).css("background", "#d00");
}

function loginAttention() {
    var wBox = $("#wbox1").wBox({
        title: "提示",
        html: "<div class='demo' style='width:240px;height:90px;padding-top:10px;margin-left:20px;'>您还没有登录，请登录后再来操作<br><div style='margin-top:10px;'>注册用户<a href='/account/logon' style='color:red;font-weight: bold'>点击这里</a>去登录</div><div style='margin-top:10px;'>未注册用户<a href='/account/register' style='color:red;font-weight: bold'>点击这里</a>去注册</div></div>",
        timeout: 10000
    });
    wBox.showBox();

    return false;
}

//限制为输入的内容为数字
$(".numbers").numeral();

function kong() {
    var wBox = $("#wbox1").wBox({ title: "云南东昌医药控销联盟申明", requestType: "ajax", target: "/apply/agreement" });
    wBox.showBox();
}