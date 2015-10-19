// 弹出层=========================================

var showc=function()
{
	$("show").style.display="block";	
	$("show").style.left= (document.body.offsetWidth - $("show").offsetWidth)/2 + "px";
	$("show").style.top=document.body.scrollTop + 120 + "px";
	//alert(document.body.offsetWidth);
	$("bg").style.display = "block";
	var h = document.body.offsetHeight > document.documentElement.offsetHeight ? document.body.offsetHeight : document.documentElement.offsetHeight;
	$("bg").style.height = h + "px";
}
var hidden=function()
{
	$("show").style.display="none";	
	$("bg").style.display = "none";
}


//弹出页面背景半透明======================================================================
var isIe=(document.all)?true:false;

//设置select的可见状态
function setSelectState(state)
{
	var objl=document.getElementsByTagName('select');
	for(var i=0;i<objl.length;i++)
	{
		objl[i].style.visibility=state;
	}
}

//弹出方法
function showMessageBox(content,wWidth,wHeight)
{
	closeWindow();
	var bWidth= parseInt(document.documentElement.scrollWidth);
	var bHeight=parseInt(document.documentElement.scrollHeight);
	
	var sWidth= parseInt((document.documentElement.scrollWidth - wWidth) / 2);
	var sHeight=parseInt(document.documentElement.scrollTop + (document.documentElement.clientHeight-wHeight) / 2);
	
	if(isIe){
		setSelectState('hidden');
	}
	var back=document.createElement("div");
	back.id="back";
	var styleStr="top:0px;left:0px;position:absolute;z-index:50;background:#666;width:"+bWidth+"px;height:"+bHeight+"px;";
	styleStr+=(isIe)?"filter:alpha(opacity=70);":"opacity:0.70;";
	back.style.cssText=styleStr;
	back.innerHTML="<div style=width:"+bWidth+"px;height:"+bHeight+"px; onclick='closeWindow();'></div>";
	document.body.appendChild(back);
	var mesW=document.createElement("div");
	mesW.id="mesWindow";
	mesW.className="mesWindow";
	mesW.innerHTML="<div id='mesWindowContent'>"+content+"</div>";
	styleStr="left:"+sWidth+"px;top:"+sHeight+"px;width:"+wWidth+"px;position:absolute;z-index:100;";
	mesW.style.cssText=styleStr;
	document.body.appendChild(mesW);
}

function showBackground(obj,endInt)
{
	obj.filters.alpha.opacity+=1;
	if(obj.filters.alpha.opacity<endInt)
	{
		setTimeout(function(){showBackground(obj,endInt)},8);
	}
}

//关闭窗口
function closeWindow()
{
	if(document.getElementById('back')!=null)
	{
		document.getElementById('back').parentNode.removeChild(document.getElementById('back'));
	}
	if(document.getElementById('mesWindow')!=null)
	{
		document.getElementById('mesWindow').parentNode.removeChild(document.getElementById('mesWindow'));
	}
	if(isIe){
		setSelectState('');
	}
}

function noselect(){
	
	if (document.getElementById("relationshop").value == ''){
		alert("您与宝宝的关系不能为空");
		return false;
	}
}



function limit(){  
     var txtNote;//文本框  
     var txtLimit;// 提示字数的input  
     var limitCount;//限制的字数  
     var isbyte;// 是否使用字节长度限制（1汉字=2字符）  
     var txtlength;// 到达限制时，字符串的长度  
     var txtByte;  
     this.init=function(){  
         txtNote=this.txtNote;  
         txtLimit=this.txtLimit;  
         limitCount=this.limitCount;  
         isbyte=this.isbyte;  
         txtNote.onkeydown=function(){wordsLimit()};txtNote.onkeyup=function(){wordsLimit()};  
         txtLimit.value=limitCount;        
     }     
     function wordsLimit(){  
         var noteCount=0;          
         if(isbyte){noteCount=txtNote.value.replace(/[^\x00-\xff]/g,"xx").length}else{noteCount=txtNote.value.length}  
         if(noteCount>limitCount){  
             if(isbyte){  
                 txtNote.value=txtNote.value.substring(0,txtlength+Math.floor((limitCount-txtByte)/2));  
                 txtByte=txtNote.value.replace(/[^\x00-\xff]/g,"xx").length;           
                 txtLimit.value=limitCount-txtByte;  
             }else{  
                 txtNote.value=txtNote.value.substring(0,limitCount);  
                 txtLimit.value=0;  
             }     
         }else{  
             txtLimit.value=limitCount-noteCount;  
         }  
         txtlength=txtNote.value.length;//记录每次输入后的长度  
         txtByte=txtNote.value.replace(/[^\x00-\xff]/g,"xx").length;  
     }  
 }  