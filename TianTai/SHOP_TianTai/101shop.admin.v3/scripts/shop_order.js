
function UpPno() {
    if (confirm("您确认要修改效期和批号吗？")) {
        var id = document.getElementById("ctl00_workspace_h_o_p_Id").value;
        var pno = document.getElementById("ctl00_workspace_txtpro_pno").value;
        var pdate = document.getElementById("ctl00_workspace_txtpro_pdate").value;
        if (parseInt(id) > 0) {
            var now = new Date();
            var year = now.getFullYear();
            var month = now.getMonth() + 1;
            var stateTime = year + "-" + month;
            if (pdate != "") {
                if (year > pdate.substring(0, 4) || (year >= pdate.substring(0, 4) && month > pdate.substring(5, 7))) {
                    alert("效期不能小于当前时间月份!");
                    return;
                }
            }
            var param = "option=up&orderId=" + id + "&pro_pdate=" + pdate + "&pro_pno=" + pno;
            jQuery.ajax({
                type: "POST",
                url: "shop_order_edit.aspx",
                async: true,
                data: "ajax=1&" + param,
                success: function (ret) {
                    try {
                        if (ret == "ok") {
                            alert("保存成功!");
                            window.location.href = window.location.href;
                        }
                        else {
                            alert("操作失败!暂不能修改批号和效期！");
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
    }
}
//修改效期
function ShowPno_tr(id, p_no, p_date) {
    if (document.getElementById("pno_tr").style.display == "none") {
        document.getElementById("pno_tr").style.display = "";
        document.getElementById("ctl00_workspace_h_o_p_Id").value = id;
        document.getElementById("ctl00_workspace_txtpro_pno").value = p_no;
        var pdate = document.getElementById("ctl00_workspace_txtpro_pdate").value = p_date;
    } else
        document.getElementById("pno_tr").style.display = "none";
}
//修改单价
function ShowP_buy_tr(id, p_name, p_price, p_num) {
    if (document.getElementById("dianjia_tr").style.display == "none") {
        document.getElementById("dianjia_tr").style.display = "";
        document.getElementById("hpro_id").value = id;
        document.getElementById("txt_p_price").value = p_price;
        document.getElementById("txt_buycount").value = p_num;
        document.getElementById("lblP_name").innerHTML = p_name;
    } else
        document.getElementById("dianjia_tr").style.display = "none";
}
function UpP_Buycount() {
    if (confirm("您确认要修改该商品的价格和数量吗？")) {
        var id = document.getElementById("hpro_id").value;
        var txt_buycount = document.getElementById("txt_buycount").value;
        var txt_p_price = document.getElementById("txt_p_price").value;

        if (parseInt(id) > 0 || parseInt(txt_buycount) > 0) {
            var param = "option=upCount&orderId=" + id + "&buycount=" + txt_buycount + "&p_price=" + txt_p_price;
            jQuery.ajax({
                type: "POST",
                url: "shop_order_edit.aspx",
                async: true,
                data: "ajax=1&" + param,
                success: function (ret) {
                    try {
                        if (ret == "ok") {
                            alert("保存成功!");
                            window.location.href = window.location.href;
                        }
                        else if (ret == "noprice") {
                            alert("操作失败!修改价格已限制为不能上调价格!");
                        }
                        else {
                            alert("操作失败!暂不能修改价格和数量!");
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
        } else
            alert("操作失败!价格和数量请输入数字！");
    }
}