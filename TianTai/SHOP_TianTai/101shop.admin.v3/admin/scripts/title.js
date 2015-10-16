function list_sub_nav(Id, sortname) {
    var li = getObject("nav").getElementsByTagName("li"), o = getObject(Id);
    if (li.length && o != null) {
        for (var i = 0; i < li.length; i++) li[i].className = (li[i].id == Id) ? "bg_image_onclick" : "bg_image";
        showInnerText(Id);
        var outlookbar = window.top.frames['leftFrame'].outlookbar;
        if (outlookbar != null && typeof (outlookbar.getbytitle) != "undefined") { outlookbar.getbytitle(sortname); }
        if (outlookbar != null && typeof (outlookbar.getdefaultnav) != "undefined") { outlookbar.getdefaultnav(sortname); }
    }
}
function showInnerText(Id){
    var o = getObject(Id);
	if(o!=null)getObject('show_text').innerHTML = o.innerHTML;
}
 //获取对象属性兼容方法
 function getObject(objectId) {
    if(document.getElementById && document.getElementById(objectId)) {
	// W3C DOM
	return document.getElementById(objectId);
    } else if (document.all && document.all(objectId)) {
	// MSIE 4 DOM
	return document.all(objectId);
    } else if (document.layers && document.layers[objectId]) {
	// NN 4 DOM.. note: this won't find nested layers
	return document.layers[objectId];
    } else {
	return false;
    }
}
window.onload = function () { var li = getObject("nav").getElementsByTagName("li"); if (li.length) { li[0].className = 'bg_image_onclick'; showInnerText(li[0].id); list_sub_nav(li[0].id, li[0].innerHTML); } };
