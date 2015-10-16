/*** 表单验证***/
ro_ids = []; rr_ids = '.r_list input[type="radio"]';  r_ids = "#province,#city,#county,#userAddr,#ConstructionSigns,#userZip,#userName,#userEmail,#userPhone,#userMobile,#ConsigneTime";
//收货人信息检查
checkMemberAddress_err = "请选择送货信息！";
function checkMemberAddress() {
    var ok = 0, r_id = 0;
    checkMemberAddress_err = "请选择送货信息！";
    jQuery(rr_ids).each(function (i) { if (this.checked == true) r_id = this.value; });
    if (!r_id || r_id == "" || r_id == "0") {
        jQuery(r_ids).each(function (i) {
            var v = jQuery(this).val();
            switch (i) {
                case 3: if (v != "" && v != "0") { ok = 1; } else { ok = 0; this.focus(); checkMemberAddress_err = "请填写详细地址！"; return false; } break;
                case 4: ok = 1; break;
                case 5: if (v != "" && /^[0-9]{5,7}$/i.test(v) == true) { ok = 1; } else { ok = 0; this.focus(); checkMemberAddress_err = "请填写正确的邮政编码！"; return false; } break;
                case 6: if (v != "" && v != "0") { ok = 1; } else { ok = 0; this.focus(); checkMemberAddress_err = "请填写收货人姓名！"; return false; } break;
                case 7: ok = 1; if (v != "" && /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/i.test(v) == false) { ok = 0; this.focus(); checkMemberAddress_err = "请填写正确的邮箱地址！"; return false; } break;
                case 8: ok = 1; if (v != "" && /^[\-0-9]{7,13}$/i.test(v) == false) { ok = 0; this.focus(); checkMemberAddress_err = "请填写正确的电话号码(028-86666666)！"; return false; } break;
                case 9: if (jQuery("#userPhone").val() == "" && v == "") { ok = 0; this.focus(); checkMemberAddress_err = "电话或者手机，必须填写一个！"; return false; } ok = 1; if (v != "" && /^[0-9]{11,11}$/i.test(v) == false) { ok = 0; this.focus(); checkMemberAddress_err = "请填写正确的手机号码(13986666666)！"; return false; } break;
                case 10: if (v != "" && v != "0") { ok = 1; } else { ok = 0; this.focus(); checkMemberAddress_err = "请填写最佳送货时间！"; return false; } break;
            }
        });
    } else {
        ok = 1;
    }
    return ok;
}
function checkOrders() {
    //收货人信息检查
    var ok = checkMemberAddress();
    if (!ok) {
        alert("温馨提示：您必须完善收货人信息！" + checkMemberAddress_err);
        return ;
    }
    //配送方式
    var peisongMethod = document.getElementsByName("peisong");
    var isPeisongMethodChecked = false;
    if (peisongMethod) {
        for (var j = 0; j < peisongMethod.length; j++) {
            if (peisongMethod[j].id.indexOf("peisong") > -1) {
                if (peisongMethod[j].checked)
                    isPeisongMethodChecked = true;
            }
        }
    }
    if (!isPeisongMethodChecked) {
        alert("温馨提示：您必须选定一个配送方式！");
        return ;
    }
    //支付方式paymentMethod
    //var paymentMethod = document.getElementsByName("pay_ment");
    var isPaymentMethodChecked = false;
//    if (paymentMethod) {
//        for (var j = 0; j < paymentMethod.length; j++) {
//            if (paymentMethod[j].id.indexOf("pay_ment") > -1) {
//                if (paymentMethod[j].checked)
//                    isPaymentMethodChecked = true;
//            }
//        }
    //    }
    for (var z = 0; z < 2; z++) {
        var paymentMethod = document.getElementById("PayWay" + (z + 1));
        if (paymentMethod.checked) {
            isPaymentMethodChecked = true;
        }
    }
        if (!isPaymentMethodChecked) {
            alert("温馨提示：您必须选定一个支付方式！");
            return;
        }

    //配送方式DeliveryModeTable
    //    var deliveryModeTable = document.getElementsByName("DeliveryMode");
    //    var isDeliveryMethodChecked = false;
    //    if (deliveryModeTable) {
    //        for (var j = 0; j < deliveryModeTable.length; j++) {
    //            if (deliveryModeTable[j].checked) {
    //                isDeliveryMethodChecked = true;
    //            }
    //        }
    //    }
    //    if (!isDeliveryMethodChecked) {
    //        alert("温馨提示：您必须选定一个配送方式！");
    //        return false;
    //    }

    document.getElementById("form1").action = "FinishOrder.aspx";
    document.getElementById("form1").submit();
}

function DeliveryCost(str) {
    if (document.getElementById("productTotalPrice")) {
        var productPrice = document.getElementById("productTotalPrice").value;
        var feeTotalPrice = document.getElementById("FeeTotalPrice").value;
        var info = "商品总价: ￥" + productPrice + "元";
        var totalPrice = parseFloat(productPrice);
        if (feeTotalPrice != "0.00" && feeTotalPrice != "") {
            totalPrice = totalPrice + parseFloat(feeTotalPrice);
            info = info + " + " + "手续费: ￥" + feeTotalPrice + "元";
        }
        if (str != "0.00" && str != "") {
            totalPrice = totalPrice + parseFloat(str);
            document.getElementById("freightTotalPrice").value = str;
            info = info + " + " + "配送费用: ￥" + str + "元";
        }

        document.getElementById("TotalCostMoney").innerHTML = totalPrice;
        document.getElementById("TotalPriceInfo").innerHTML = info;
        document.getElementById("TotalPriceInfo").style.display = "";
        document.getElementById("productTotalPrice").value = totalPrice;
    }
}

function PaymentCost(Cost) {
    if (document.getElementById("productTotalPrice")) {
        var freightTotalPrice = document.getElementById("freightTotalPrice").value;
        var productPrice = document.getElementById("productTotalPrice").value;
        var info = "商品总价: ￥" + productPrice + "元";
        var totalPrice = parseFloat(productPrice);
        if (freightTotalPrice != "0.00" && freightTotalPrice != "") {
            totalPrice = totalPrice + parseFloat(freightTotalPrice);
            info = info + " + " + "配送费用: ￥" + freightTotalPrice + "元";
        }
        if (Cost != "0.00" && Cost != "") {
            totalPrice = totalPrice + parseFloat(Cost);
            document.getElementById("FeeTotalPrice").value = Cost;
            info = info + " + " + "手续费: ￥" + Cost + "元";
        }

        document.getElementById("TotalCostMoney").innerHTML = totalPrice;
        document.getElementById("TotalPriceInfo").innerHTML = info;
        document.getElementById("TotalPriceInfo").style.display = "";
        document.getElementById("productTotalPrice").value = totalPrice;
    }
}

function AddMemberAddress(a, b, c) {
    if (typeof (c) == "undefined" || !c) c = 0;
    var o = jQuery("#r_otherdiv"), oi = jQuery("#ChinaArea input"), os = jQuery(r_ids);
    if (o.length) {
        if (b == true) {
            o.show();
            oi[0].value = os.eq(0).val(); oi[1].value = os.eq(1).val(); oi[2].value = os.eq(2).val();
        }
        else {
            o.hide();
            os.each(function (i) { if (i < 3) { if (i == 0) { oi[0].value = ro_ids[c].province; oi[1].value = ro_ids[c].city; oi[2].value = ro_ids[c].county; } } else { this.value = ro_ids[c][this.id + ""]; } });
        }
    }
}
jQuery(function(){
	var l = ro_ids.length;
	if(l>0){
	    var a = jQuery(rr_ids)[0]; a.checked = true;
		AddMemberAddress(a, 0, 0);
		if(l>=5){
			jQuery(".r_list li:last").html("进入 <a href=\"../membercenter/member_index.aspx\">我的账户</a> > <a href=\"../membercenter/member_index.aspx?target=ReceAddress_list\">收货地址管理</a>");
		}
	}else{
		jQuery(".r_list li:last").html("");jQuery("#r_otherdiv").show();
	}
});