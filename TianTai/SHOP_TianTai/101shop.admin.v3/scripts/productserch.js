//使用jquery框架
jQuery(function(){
  setSelect();
})
function setSelect(){
  //获取当前隐藏域中的浏览器当前请求地址
    var url = jQuery("#hUrl").attr("value");
  //遍历带有指定元素下的所有元素
  jQuery(".Lb_fl_ctr a").each(function () {
    if (jQuery(this).attr("href").toLowerCase() == url.toLowerCase())
    {
        jQuery(this).css({ "background-color": "#006000", "color": "#fff" });
    }
  })
}
