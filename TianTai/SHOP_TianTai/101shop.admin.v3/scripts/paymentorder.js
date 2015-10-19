var ffAlertTxt = '您的输入含有非法字符，请检查！';
var isRemarkOpen = false;
window.onload = function () {

    //检查是否存在收货人信息
    $.ajax({
        type: "get",
        datatype: "json",
        url: "../filehandle/ForOrderInfo.ashx?",
        data: { "load": "IfPresence" },
        success: function (data) {
            if (data == "yes") {
                showclose_consignee("close");
                showclose_payway("update");
                loadinfo_no();
                $("#button2").attr("id", "UpdateInfo");
            }
            else if (data == "no") {
                showclose_consignee("update");
                showclose_payway("close");
                loadinfo_yes("all");
                $("#button2").attr("id", "SaveInfo");
            }
        }
    });
    $("#close_payway li table tr:eq(0) td:eq(1)").html('款到发货（在线支付）');
    $("#close_payway li table tr:eq(1) td:eq(1)").html('第三方物流 ');
    $("#close_payway li table tr:eq(3) td:eq(1)").html('只工作日送货(双休日、假日不用送)');
    removeAlert('infoId01');
    var newd = document.createElement("span");
    newd.id = "infoId01";
    newd.className = 'InfoId';
    newd.innerHTML = "2/1//2/1";
    g('close_payway').appendChild(newd);
}
function loadinfo_yes(str) {
    $.ajax({
        type: "get",
        dataType: "json",
        url: "../filehandle/ForOrderInfo.ashx?",
        data: { "load": str },
        success: function (data) {
            if (str == "all_update") {
                var newd = document.createElement("span");
                newd.id = "infoId";
                newd.className = 'InfoId';
                newd.innerHTML = data.consignee_id;
                g('gwc_bt').appendChild(newd);
            }
            $("#consignee_addressName").val(data.consignee_addressName);
            $("#consignee_address").val(data.consignee_address);
            $("#consignee_message").val(data.consignee_message);
            $("#consignee_phone").val(data.consignee_phone);
            $("#consignee_email").val(data.consignee_email);
            $("#consignee_postcode").val(data.consignee_post);
            var optionP = "<option value='-1'>请选择</option>";
            $.each(data.consignee_province, function (i, n) {
                if (n["code"] == data.consignee_provinceSel) {
                    optionP += "<option style=\"width:auto\" value='" + n["code"] + "' selected=\"selected\">" + n["content"] + "</option>";
                }
                else {
                    optionP += "<option style=\"width:auto\" value='" + n["code"] + "'>" + n["content"] + "</option>";
                }
            });
            $("#province").html(optionP);
            var optionCi = "<option value='-1'>请选择</option>";
            var optionC = "<option value='-1'>请选择</option>";
            if (str == "all_update") {
                $.each(data.consignee_city, function (i, n) {
                    if (n["code"] == data.consignee_citySel) {
                        optionCi += "<option style=\"width:auto\" value='" + n["code"] + "' selected=\"selected\">" + n["content"] + "</option>";
                    }
                    else {
                        optionCi += "<option style=\"width:auto\" value='" + n["code"] + "'>" + n["content"] + "</option>";
                    }
                });
                $.each(data.consignee_county, function (i, n) {
                    if (data.consignee_countySel == n["code"]) {
                        optionC += "<option style=\"width:auto\" value='" + n["code"] + "'selected=\"selected\">" + n["content"] + "</option>";
                    }
                    else {
                        optionC += "<option style=\"width:auto\" value='" + n["code"] + "'>" + n["content"] + "</option>";
                    }
                });
            }
            $("#city").html(optionCi);
            $("#county").html(optionC);
        }
    });
}
function loadinfo_no() {
    $.ajax({
        type: "get",
        dataType: "json",
        url: "../filehandle/ForOrderInfo.ashx?",
        data: { "load": "all_No" },
        success: function (data) {
            $("#o_show li table tr:eq(0) td:eq(1)").html(data.consignee_addressName);
            $("#o_show li table tr:eq(1) td:eq(1)").html(data.consignee_province + data.consignee_city + data.consignee_county);
            $("#o_show li table tr:eq(2) td:eq(1)").html(data.consignee_province + data.consignee_city + data.consignee_county + data.consignee_address);
            $("#o_show li table tr:eq(3) td:eq(1)").html(data.consignee_message);
            $("#o_show li table tr:eq(4) td:eq(1)").html(data.consignee_phone);
            $("#o_show li table tr:eq(5) td:eq(1)").html(data.consignee_email);
            $("#o_show li table tr:eq(6) td:eq(1)").html(data.consignee_post);
        }
    });
}
function g(nodeId) {
    return document.getElementById(nodeId);
}
//删除提示信息
function removeAlert(infoSign) {
    if (g(infoSign) == null) { return; }
    g(infoSign).parentNode.removeChild(g(infoSign));
}
//显示提示信息
function showAlert(info, obj, infoSign) {
    if (g(infoSign) != null) { return; }
    var newd = document.createElement("span");
    newd.id = infoSign;
    newd.className = 'alertInfo';
    newd.innerHTML = info;
    obj.appendChild(newd);
}
function trimTxt(txt) {
    return txt.replace(/(^\s*)|(\s*$)/g, "");
}
//检查是否为空
function isEmpty(inputId) {
    if (trimTxt(g(inputId).value) == '') { return true }
    return false;
}
//非法字符过滤
function is_forbid(temp_str) {
    temp_str = trimTxt(temp_str);
    temp_str = temp_str.replace('*', "@");
    temp_str = temp_str.replace('--', "@");
    temp_str = temp_str.replace('/', "@");
    temp_str = temp_str.replace('+', "@");
    temp_str = temp_str.replace('\'', "@");
    temp_str = temp_str.replace('\\', "@");
    temp_str = temp_str.replace('$', "@");
    temp_str = temp_str.replace('^', "@");
    temp_str = temp_str.replace('.', "@");
    temp_str = temp_str.replace('#', "@");
    temp_str = temp_str.replace(';', "@");
    temp_str = temp_str.replace('<', "@");
    temp_str = temp_str.replace('>', "@");
    temp_str = temp_str.replace('"', "@");
    temp_str = temp_str.replace('=', "@");
    temp_str = temp_str.replace('{', "@");
    temp_str = temp_str.replace('}', "@");
    var forbid_str = new String('@,%,~,&');
    var forbid_array = new Array();
    forbid_array = forbid_str.split(',');
    for (i = 0; i < forbid_array.length; i++) {
        if (temp_str.search(new RegExp(forbid_array[i])) != -1)
            return false;
    }
    return true;
}
//检查收货人姓名
function check_addressName() {
    removeAlert("addressName_empty");
    removeAlert('addressName_ff');
    var pNode = g('consignee_addressName').parentNode;
    if (isEmpty('consignee_addressName')) {
        showAlert('收货人姓名不能为空！', pNode, 'addressName_empty'); return false;
    }
    if (!is_forbid(g('consignee_addressName').value)) { showAlert(ffAlertTxt, pNode, 'addressName_ff'); return false; }
    return true;
}
//检查收货人地址
function check_address() {
    removeAlert('address_empty');
    removeAlert('address_ff');

    var pNode = g('consignee_address').parentNode;
    if (isEmpty('consignee_address')) { showAlert('收货地址不能为空！', pNode, 'address_empty'); return false; }
    if (!is_forbid(g('consignee_address').value)) { showAlert(ffAlertTxt, pNode, 'address_ff'); return false; }
    return true;
}
//检查手机号
function check_message() {
    removeAlert('message_ff');
    if (g('consignee_message').value != '') {
        var pNode = g('consignee_message').parentNode;
        var myReg = /(^\s*)(((\(\d{3}\))|(\d{3}\-))?13\d{9}|1\d{10})(\s*$)/;
        if (!myReg.test(g('consignee_message').value)) { showAlert('手机号格式不正确', pNode, 'message_ff'); return false; }
    }
    return true;
}
//检查联系电话
function check_phone() {
    removeAlert('phone_ff');
    var pNode = g('consignee_phone').parentNode;
    var myReg = /(\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$/;
    if (!isEmpty('consignee_phone') && !myReg.test(g('consignee_phone').value)) { showAlert('固定电话格式不正确', pNode, 'phone_ff'); return false; }
    if (!isEmpty('consignee_phone') && g('consignee_phone').value.length > 20) { showAlert('固定电话格式不正确', pNode, 'phone_ff'); return false; }
    return true;
}
//检查电话和手机是否都填写了
function check_phoneAndMob() {
    removeAlert('phone_empty');
    var pNode = g('consignee_phone').parentNode;
    if (isEmpty('consignee_phone') && isEmpty('consignee_message')) { showAlert('固定电话和手机号码请至少填写一项！', pNode, 'phone_empty'); return false; }
    return true;
}
//检查Email
function check_email() {
    var iSign = 'email';
    removeAlert(iSign + '_ff');
    if (g('consignee_' + iSign).value != '') {
        var pNode = g('consignee_' + iSign).parentNode;
        var myReg = /(^\s*)\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*(\s*$)/;
        if (!myReg.test(g('consignee_' + iSign).value)) { showAlert('电子邮件格式不正确', pNode, iSign + '_ff'); return false; }
    }
    return true;
}
//检查邮政编码
function check_postcode() {
    removeAlert('postcode_ff');
    if (g('consignee_postcode').value != '') {
        var pNode = g('consignee_postcode').parentNode;
        var myReg = /(^\s*)\d{6}(\s*$)/;
        if (!myReg.test(g('consignee_postcode').value)) { showAlert('邮编格式不正确', pNode, 'postcode_ff'); return false; }
    }
    return true;
}

//省市联动
function changeArea() {
    if ($("#province option:selected").val() == -1) {
        $("#city").empty();
        $("#city").html("<option value=\"-1\">请选择</option>")
        $("#county").empty();
        $("#county").html("<option value=\"-1\">请选择</option>")
    }
    else {
        $("#county").empty();
        $("#county").html("<option value=\"-1\">请选择</option>")
        change($("#province option:selected").val(), "#city");
    }
}

//市县级联动
function changeCity() {
    if ($("#city option:selected").val() == -1) {
        $("#county").empty();
        $("#county").html("<option value=\"-1\">请选择</option>")
    }
    else {
        change($("#city option:selected").val(), "#county");
    }
}
//检查省市区
function check_con_town() {
    removeAlert('area_check'); 
    if (g('consignee_arae').childNodes[5].value == '-1') {
        showAlert('地区信息不完整！', g('consignee_arae').parentNode, 'area_check')
        return false;
    }
    return true;
}
function change(str, strName) {
    $(strName).empty();
    $.ajax({
        type: "get",
        dataType: "json",
        url: "../filehandle/ForOrderInfo.ashx?",
        data: { "load": "province", "province": str },
        success: function (data) {
            var options = "<option value=\"-1\">请选择</option>";
            $.each(data.city, function (i, n) {
                options += "<option style=\"width:auto\" value='" + n["code"] + "'>" + n["content"] + "</option>";
            });
            $(strName).html(options);
        }
    });
}
//保存收货人信息时的检查
function check_con() {
    var res = true;
    if (!check_addressName()) { res = false; }
    if (!check_con_town()) { res = false; }
    if (!check_address()) { res = false; }
    if (!check_postcode()) { res = false; }
    if (!check_phoneAndMob()) { res = false; }
    if (!check_phone()) { res = false; }
    if (!check_message()) { res = false; }
    if (!check_email()) { res = false; }

    return res;
}
function clearSubmitError(obj) {
    if (obj.parentNode.childNodes.length > 0) {
        if (obj.parentNode.lastChild.name == 'errorInfo') {
            obj.parentNode.removeChild(obj.parentNode.lastChild);
        }
    }
}

function savePart_consignee(obj) {
    clearSubmitError(obj);
    var str = $(obj).attr("id");
    if (str == "UpdateInfo") {
        str = str + "_" + $("#infoId").html();
    }
    if (check_con()) {
        $.ajax({
            type: "get",
            dataType: "json",
            url: "../filehandle/ForOrderInfo.ashx?",
            data: { "load": str, "consignee_addressName": g('consignee_addressName').value, "consignee_message": g('consignee_message').value, "consignee_phone": g('consignee_phone').value, "consignee_email": g('consignee_email').value, "province": $("#province option:selected").val(), "city": $("#city option:selected").val(), "county": $("#county option:selected").val(), "consignee_address": g('consignee_address').value, "consignee_postcode": g('consignee_postcode').value },
            success: function () {
                showclose_consignee("close");
            }, error: function () {
                showclose_consignee("close");
            }
        });
    }
}

function showclose_consignee(str) {
    if (str == "close") {
        $("#o_write").css("display", "none");
        $("#o_show").css("display", "");
        loadinfo_no();
    }
    else if (str == "update") {
        $("#o_write").css("display", "");
        $("#o_show").css("display", "none");
        loadinfo_yes("all_update");
    }
}
function showclose_payway(str) {
    if (str == "close") {
        $("#close_payway").css("display", "none");
        $("#show_payway").css("display", "");
    }
    else if (str == "update") {
        $("#close_payway").css("display", "");
        $("#show_payway").css("display", "none");
    }
}

function showclose_pay(sign1, sign2) {
    $(sign1).css("display", "");
    $(sign2).css("display", "none");
}
function showclose_pay01(sign1, sign2, str, str2, str3) {
    removeAlert('area_ff');
    removeAlert('errbank');
    removeAlert('errbank01');
    $(sign1).css("display", str);
    $(sign2).css("display", str);
    if (str2 == 'true') {
       return errorArea('pw', '目前仅支持成都武侯,青羊，锦江，金牛，成华区客户现金支付', 'area_ff');
    }
    if (str2 == 'false' && str3 == 'true') {
        bankinfo();
    }
}

function bankinfo() {
    $.ajax({
        type: "get",
        dataType: "json",
        url: "../filehandle/ForOrderInfo.ashx?",
        data: { "load": "loadbank" },
        success: function (data) {
            var options = "<option value='-1'>请选择</option>"
            $.each(data.city, function (i, n) {
                options += "<option value=" + n["bank_id"] + ">" + n["bank_name"] + "</optrion>"
            });
            $("#bankSel").html(options);
            $("#bankSel01").html(options);
            $("#kaihuhang").html("××××××××");
            $("#kaihuhang01").html("××××××××");
            $("#kaihuming").html("××××××××");
            $("#kaihuming01").html("××××××××");
            $("#zhanghao").html("××××××××");
            $("#zhanghao01").html("××××××××");
        }
    });
}

function changeBank(sign1, sign2, sign3, sign4) {
    removeAlert('errbank');
    removeAlert('errbank01');
    $(sign1 + " option:first").attr("disabled", "disabled");
    $.ajax({
        type: "get",
        dataType: "json",
        url: "../filehandle/ForOrderInfo.ashx?",
        data: { "load": "loadbankOne", "bank_id": $(sign1 + " option:selected").val() },
        success: function (data) {
            $.each(data.city, function (i, n) {
                $(sign2).html(n["bank_bankac"]);
                $(sign3).html(n["bank_account"]);
                $(sign4).html(n["bank_number"]);
            });
        }
    });
}
function errorArea(sign, str, errorId) {
    var area = $("#o_show li table tr:eq(1) td:eq(1)").html();
    var pNode = g(sign).parentNode;
    if (area.indexOf('成都市青羊区') > 0 || area.indexOf('成都市武侯区') > 0 || area.indexOf('成都市成华区') > 0 || area.indexOf('成都市金牛区') > 0 || area.indexOf('成都市锦江区') > 0 || area.indexOf('成都市高新区')) {
        return true;
    }
    else {
        showAlert(str, pNode, errorId); return false;
    }
}
function showclose_pay02(str) {
    removeAlert('area01_ff');
    if (str == 'true') {
       return errorArea('sp', '目前仅支持成都武侯,青羊，锦江，金牛，成华，高新区客户送货上门服务', 'area01_ff');
    }
}

//保存送货信息
function saveSonghuo() {
    if ($("input[name='wuliu']:first").attr("checked")) {
        if (showclose_pay02('true')) {
            $("#close_payway li table tr:eq(1) td:eq(1)").html('送货上门');
        }
        else {
            return false;
        }
    }
    if ($("input[name='payway']:checked").val() == 1 && $("input[name='payway1']:checked").val() == 2 && $("#bankSel option:selected").val() == -1) {
        showAlert("请选择需要转账的银行", g('zz').parentNode, "errbank");
        return false;
    }
    if ($("input[name='payway']:checked").val() == 2 && $("input[name='payway2']:checked").val() == 2 && $("#bankSel01 option:selected").val() == -1) {
        showAlert("请选择需要转账的银行", g('zz01').parentNode, "errbank01");
        return false;
    }
    var payway = '';
    var bankId = '';
    var paywayShow = '';
    var record = '';
    record += $("input[name='payway']:checked").val();
    if ($("input[name='payway']:checked").val() == 1) {
        paywayShow = '货到付款';
        payway = $("input[name='payway1']:not(:first):checked").val();
        bankId = $("input[name='payway1']:checked").val() == 2 ? $("#bankSel option:selected").val() : '';
        record += "/" + payway + "/" + bankId;
    }
    if ($("input[name='payway']:checked").val() == 2) {
        paywayShow = '款到发货';
        payway = $("input[name='payway2']:checked").val();
        bankId = $("input[name='payway2']:checked").val() == 2 ? $("#bankSel01 option:selected").val() : '';
        record += "/" + payway + "/" + bankId;
    }
    switch (payway) {
        case '1': paywayShow += '（在线支付）'; break;
        case '3': paywayShow += '（支票）'; break;
        case '2': paywayShow += '（转账）'; break;
    }
    switch ($("input[name='wuliu']:not(:first):checked").val()) {
        case '3': $("#close_payway li table tr:eq(1) td:eq(1)").html('自提'); break;
        case '2': $("#close_payway li table tr:eq(1) td:eq(1)").html('第三方物流'); break;
    }
    switch ($("input[name='songhuoDate']:checked").val()) {
        case '1': $("#close_payway li table tr:eq(3) td:eq(1)").html('<font color="#FF0000">只工作日送货(双休日、假日不用送)</font>'); break;
        case '3': $("#close_payway li table tr:eq(3) td:eq(1)").html('<font color="#FF0000">工作日、双休日与假日均可送货</font>'); break;
        case '2': $("#close_payway li table tr:eq(3) td:eq(1)").html('<font color="#FF0000">只双休日、假日送货(工作日不用送)</font>'); break;
    }

    if ($("input[name='payway']:checked").val() == 1 && $("#pw").attr("checked")) {
        if ($("input[name='payway']:checked").val() == 1 && showclose_pay01('#pay_zhuan', '#pay_zhuan01', 'none', 'true', 'false')) {
            paywayShow += "（现金）";
            record = '';
            record += $("input[name='payway']:checked").val() + "/-1/";
        }
        else {
            return false;
        }
    }
    record += "/" + $("input[name='wuliu']:checked").val() + "/" + $("input[name='songhuoDate']:checked").val();
    removeAlert('infoId01');
    var newd = document.createElement("span");
    newd.id = "infoId01";
    newd.className = 'InfoId';
    newd.innerHTML = record;
    g('close_payway').appendChild(newd);
    $("#close_payway li table tr:eq(0) td:eq(1)").html(paywayShow);
    showclose_payway("update");
}
function SaveOrderInfo() {
    var pid = '';
    $.each($(".dd_nr_nr .dd_nr_bg_bt01 span"), function (i) {
        pid += $(".dd_nr_nr .dd_nr_bg_bt01 span").eq(i).html().replace(/\s+/, "") + ",";
    });
    var price = '';
    $.each($(".dd_nr_nr .dd_nr_bg_bt07 span"), function (i) {
        price += $(".dd_nr_nr .dd_nr_bg_bt07 span").eq(i).html().replace(/\s+/, "") + ",";
    });
    var pronum = '';
    var margins = '';
    $.each($(".dd_nr_nr .dd_nr_bg_bt08 span"), function (i) {
        pronum += $(".dd_nr_nr .dd_nr_bg_bt08 span").eq(i).html().replace(/\s+/, "") + ",";
        margins += 0 + ',';
    });
    var record = $("#infoId01").html();
    if (record == null) {
        alert("重复提交订单");return false;
    }
   
    var remark;  //备注
    if ($("#CheckBox01").attr("checked")) {
        remark = $("#textarea").val()=="限15个字"?"":$("#textarea").val();
    }
    else {
        remark="";
    }

    $.ajax({
        type: "get",
        dataType: "json",
        url: "../filehandle/ForOrderInfo.ashx?", 
        data: { "load": "SavePayandSend", "payment": record.split('/')[0], "payway": record.split('/')[1], "bankId": record.split('/')[2], "wuliu": record.split('/')[3], "songhuo": record.split('/')[4], "pid": pid, "price": price, "pronum": pronum, "remark": remark, "amcount": $("#amcount").html().replace("元", ""), "margins": margins },
        success: function (data) {
            if (data.msg == 'nosave') {
                alert('请先保存收货人信息！！！');
                return false;
            }
            else if (data.msg == 'yes') {
                alert('购买商品成功！！！');
                window.location.href = '/product/FinshOrders.aspx';
            }
        }, error: function (XMLHttpRequest, errorCode, status) {
            alert(errorCode + ":购买商品失败");
        }
    });
}

//-----------------备注-----------------

function clearWaitInfo() {
    try {
        if (g('waitInfo') != null) { g('waitInfo').parentNode.removeChild(g('waitInfo')); }
    } catch (e) { }
}
//显示等待信息
function showWaitInfo(info, obj) {
    try {
        if (obj == null) return;
        clearWaitInfo();
        var newd = document.createElement("span");
        newd.className = 'waitInfo';
        newd.id = 'waitInfo';
        newd.innerHTML = info;
        obj.parentNode.appendChild(newd);
    } catch (e) { }
}

function showForm_remark(obj) {
    showWaitInfo('正在读取订单备注信息，请等待！', g('showForm_remark'));
}
function close_remark(obj) {
    clearWaitInfo();
}
function showForm(obj) {
    if (obj.checked) {
        showForm_remark(obj);
        $("#showOrHidden").css("display", "");
        $("#textarea").val("限15个字");
        $("#textarea").css("color", "#cccccc");
        close_remark(obj);
    }
    else {
        $("#showOrHidden").css("display","none");
      }
}