/*城市选择*/
function GetCity(controlid, code, value) {
    if (code.length > 0) {
        var control = jQuery("#" + controlid);
        if (control.length) {
            control.nextAll("select").each(function () {
                this.options.length = 0;
                this.options.add(new Option("请选择", ""));
            });
            control = jQuery("#" + controlid).get(0);
            var url = "../../admin/plugin/getcity.aspx?parentid=" + code;
            jQuery.ajax({
                type: "GET",
                url: url,
                async: true,
                data: "ajax=1",
                success: function (ret) {
                    try {
                        eval("var contentjson = " + ret);
                        control.options.length = 0;
                        control.options.add(new Option("请选择", ""));
                        for (var i = 0; i < contentjson.city.length; i++) {
                            control.options.add(new Option(contentjson.city[i].content, contentjson.city[i].code));
                            if (value == contentjson.city[i].code) {
                                control.options[i + 1].selected = true;
                            }
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
