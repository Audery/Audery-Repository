var ad_div_id_name = "ad_";//广告位div 标示id命名规则 “ad_x”x为id号

function getAdContent(ad_id) {
    $.ajax({
        type: "get",
        async: false,
        url: ad_server + "/Ad/getContent", //实际上访问时产生的地址为: ajax.ashx?callbackfun=jsonpCallback&id=10
        data: { id: ad_id },
        cache: false, //默认值true
        dataType: "jsonp",
        jsonp: "callbackfun", //传递给请求处理程序或页面的，用以获得jsonp回调函数名的参数名(默认为:callback)
        jsonpCallback: "jsonpCallback",
        //自定义的jsonp回调函数名称，默认为jQuery自动生成的随机函数名
        //如果这里自定了jsonp的回调函数，则success函数则不起作用;否则success将起作用
        success: function (json) {
            console.log(json)
        },
        error: function () {
            //alert("Success");
        }
    });

}
function jsonpCallback(data) //回调函数
{
    $.each(data, function (index, item) {
	if(item.picture.length>4)
	{
        var ad_text = "<a href='" + item.url + "' target='_blank'>";
        ad_text += "<img src='" + ad_server + item.picture + "' width='" + item.width + "' height='" + item.height + "'>";
        ad_text += "</a>";
        $("#"+ad_div_id_name + item.configid).html(ad_text);
	}
});
hdpjz();
}