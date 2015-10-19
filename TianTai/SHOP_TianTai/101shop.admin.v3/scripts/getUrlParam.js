var site = getParam("requestSite");
var url = getParam("resURL");
var isOutUrl = "false";

$(function () {

    $.get("../OutSideCustomer.aspx", {
        Action: "get",
        requestSite: site,
        resURL: url
    },
     function (data, textStatus) {
         if (data != "") {
             IsShowTel(data);
         }
     });
})

function IsShowTel(isShow) {

    if (isShow == "true") {
        $(".qqLI").each(function () {
            if ($(this).attr("id") != "liShow" && $(this).attr("id") != "liTime") {
                $(this).hide();
            }
        })

        $("#liShow").show();
        $("#spTel").text("咨询电话");
        $("#liGys").hide();
        $("#aFloatTools_Hide").attr('style', 'top:35px');
        isOutUrl = isShow;
    } else {
        $("#liShow").hide();
        $("#spTel").text("产品咨询");
    }
}


function IsOpen() {
    jQuery('#divFloatToolsView').animate({ width: 'show', opacity: 'show' }, 'normal',
        function () {
            jQuery('#divFloatToolsView').show();

        });
    jQuery('#aFloatTools_Show').attr('style', 'display:none');
    jQuery('#aFloatTools_Hide').attr('style', 'display:block');
    //alert($("#liShow").is(":hidden"));
    if (isOutUrl=="true") {
        $("#aFloatTools_Hide").attr('style', 'top:35px');
    }

    return false;
}

function IsHide() {
    jQuery('#divFloatToolsView').animate({ width: 'hide', opacity: 'hide' }, 'normal',
        function () {
            jQuery('#divFloatToolsView').hide();

        });
    jQuery('#aFloatTools_Show').attr('style', 'display:block');
    jQuery('#aFloatTools_Hide').attr('style', 'display:none');
    //alert($("#liShow").is(":hidden"));
    if (isOutUrl=="true") {
        $("#aFloatTools_Show").attr('style', 'top:35px');
    }
    return false;
}


//获取指定的URL参数值
function getParam(paramName) {
    paramValue = "";
    isFound = false;
    if (this.location.search.indexOf("?") == 0 && this.location.search.indexOf("=") > 1) {
        arrSource = unescape(this.location.search).substring(1, this.location.search.length).split("&");
        i = 0;
        while (i < arrSource.length && !isFound) {
            if (arrSource[i].indexOf("=") > 0) {
                if (arrSource[i].split("=")[0].toLowerCase() == paramName.toLowerCase()) {
                    paramValue = arrSource[i].split("=")[1];
                    isFound = true;
                }
            }
            i++;
        }
    }
    return paramValue;
}
