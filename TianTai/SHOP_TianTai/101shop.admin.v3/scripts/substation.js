<!-- 
//ajax提示框功能
var Obj=''
document.onmouseup=MUp
document.onmousemove=MMove

function MDown(Object){
Obj=Object.id
document.all(Obj).setCapture()
pX=event.x-document.all(Obj).style.pixelLeft;
pY=event.y-document.all(Obj).style.pixelTop;
}

function MMove(){
if(Obj!=''){
  document.all(Obj).style.left=event.x-pX;
  document.all(Obj).style.top=event.y-pY;
  }
}

function MUp(){
if(Obj!=''){
  document.all(Obj).releaseCapture();
  Obj='';
  }
}

//ajax提示框========================================
function openWithIframe(tit,url,w,h)
{
	var sWidth,sHeight;
	sWidth=document.body.clientWidth;
	sHeight=document.body.scrollHeight;
	if(sHeight<window.screen.height){sHeight=window.screen.height;}
	var bgObj=document.createElement("div");
	bgObj.setAttribute('id','bgDiv');
	bgObj.style.position="absolute";
	bgObj.style.top="0";
	bgObj.style.background="#000000";
	bgObj.style.filter="Alpha(Opacity=30);";
	bgObj.style.left="0";
	bgObj.style.width=sWidth + "px";
	bgObj.style.height=sHeight + "px";
	bgObj.style.zIndex = "10000";
    document.body.appendChild(bgObj);

    massage_box.style.left = (document.body.clientWidth - w) / 2;
    massage_box.style.top = (screen.height - h) / 2-80;
    massage_box.style.screenx = (document.body.clientWidth - w) / 2;//仅适用于Netscape
    massage_box.style.screeny = (screen.height - h) / 2-80;//仅适用于Netscape
    massage_box.style.width = w+"px";
    massage_box.style.height = h+"px";
    pop_title.innerHTML=tit;
    massage_box.style.display=''
    var popiframe='<iframe src="'+url+'" width="'+(w-11)+'px"  height="'+(h-36)+'px" frameborder=0 scrolling=no></iframe>';
    pop_iframe.innerHTML=popiframe;
}
function closeWithIframe()
{
    massage_box.style.display="none";
    document.body.removeChild(document.getElementById("bgDiv"));
}
document.write('<div id="massage_box" style="position:absolute; FILTER: progid:DXImageTransform.Microsoft.DropShadow();z-index:10001;display:none;">');
document.write('<div style="border-width:1 1 3 1; width:100%; height:100%; background:#fff; color:#666666; font-size:12px; line-height:150%">');
document.write('<div onmousedown=MDown(massage_box) style="background:#666666; height:20px; font-family:Verdana, Arial, Helvetica, sans-serif; font-size:12px;color:#fff;cursor:move;padding:0 0 4px 0">');
document.write('<div style="display:inline; width:200px; position:absolute;padding:3px 0 0 5px" id=pop_title></div>');
document.write('<span onClick="closeWithIframe()" style="float:right; display:inline; cursor:pointer;padding:3px 5px 0 0;font-size:12px">关闭</span>');
document.write('</div>');
document.write('<div style="padding:5px;overFlow-x:hidden;overFlow-y:scroll;" id=pop_iframe></div>');
document.write('</div>');
document.write('</div>');
function openWithDiv(tit,url,w,h)
{
	var sWidth,sHeight;
	sWidth=document.body.clientWidth;
	sHeight=document.body.scrollHeight;
	if(sHeight<window.screen.height){sHeight=window.screen.height;}
	var bgObj=document.createElement("div");
	bgObj.setAttribute('id','bgDiv');
	bgObj.style.position="absolute";
	bgObj.style.top="0";
	bgObj.style.background="#000000";
	bgObj.style.filter="Alpha(Opacity=30);";
	bgObj.style.left="0";
	bgObj.style.width=sWidth + "px";
	bgObj.style.height=sHeight + "px";
	bgObj.style.zIndex = "10000";
    document.body.appendChild(bgObj);

    massage_box.style.left = (document.body.clientWidth - w) / 2;
    massage_box.style.top = (screen.height - h) / 2-80;
    massage_box.style.screenx = (document.body.clientWidth - w) / 2;//仅适用于Netscape
    massage_box.style.screeny = (screen.height - h) / 2-80;//仅适用于Netscape
    massage_box.style.width = w+"px";
    massage_box.style.height = h+"px";
    
    pop_iframe.style.height=(h-20)+"px";
    
    pop_title.innerHTML=tit;
    massage_box.style.display=''

    ToAjax(url,null);
}
function openWithDivRe(tit,url,w,h)
{
	var sWidth,sHeight;
	sWidth=document.body.clientWidth;
	sHeight=document.body.scrollHeight;
	if(sHeight<window.screen.height){sHeight=window.screen.height;}
	var bgObj=document.createElement("div");
	bgObj.setAttribute('id','bgDiv');
	bgObj.style.position="absolute";
	bgObj.style.top="0";
	bgObj.style.background="#000000";
	bgObj.style.filter="Alpha(Opacity=30);";
	bgObj.style.left="0";
	bgObj.style.width=sWidth + "px";
	bgObj.style.height=sHeight + "px";
	bgObj.style.zIndex = "10000";
    document.body.appendChild(bgObj);

    massage_box.style.left = (document.body.clientWidth - w) / 2;
    massage_box.style.top = (screen.height - h) / 2-80;
    massage_box.style.screenx = (document.body.clientWidth - w) / 2;//仅适用于Netscape
    massage_box.style.screeny = (screen.height - h) / 2-80;//仅适用于Netscape
    massage_box.style.width = w+"px";
    massage_box.style.height = h+"px";
    
    pop_iframe.style.height=(h-20)+"px";
    
    pop_title.innerHTML=tit;
    massage_box.style.display=''
    ToAjax(url,null);
}

//ajax提示框功能========================================


function InitAjax(){
var ajax=false; 
try { ajax = new ActiveXObject("Msxml2.XMLHTTP"); } 
catch (e) { try { ajax = new ActiveXObject("Microsoft.XMLHTTP"); } catch (E) { ajax = false; } }
if (!ajax && typeof XMLHttpRequest!='undefined') { ajax = new XMLHttpRequest(); } 
return ajax;}

//公用ajax
function ToAjax(url,Post){
	if (url!=""){
		var getinfo = "";
		var ajax = InitAjax();
		ajax.open("POST", url, true); 
		ajax.setRequestHeader("CONTENT-TYPE","application/x-www-form-urlencoded; charset=GB2312"); 
		ajax.send(Post);
	    ajax.onreadystatechange = function(){
		  if (ajax.readyState == 4){getinfo = ajax.responseText;}
	      pop_iframe.innerHTML = getinfo;	      
		  }
	}
}

// -->
