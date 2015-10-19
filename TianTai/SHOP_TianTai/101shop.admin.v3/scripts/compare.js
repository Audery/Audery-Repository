/*
compare
*/
var initScrollY=0;
var proIDs=new Array();
function compare(){
	if (jQuery("#compare").get(0)==null){
		jQuery("body").append("<div id='compare'><h5><a title='清空' class='close' onclick='clearCompare()'></a>商品比较</h5><div class='comPro'><ul id='comProlist'></ul><img src='../images/compare_15.gif' id='compareImg' onclick='openCompare()'/></div></div>")	
		jQuery("#compare").css({position:"absolute",top:"220px",right:"0px"});
		isCoo();
	}
	if (jQuery.browser.msie){
		var defaultY=document.documentElement.scrollTop;
		var perceH=0.3*(defaultY-initScrollY);
		if(perceH>0){perceH=Math.ceil(perceH);}
		else {perceH=Math.floor(perceH);}
		jQuery("#compare").get(0).style.top=parseInt(jQuery("#compare").get(0).style.top)+perceH+"px";
		initScrollY=initScrollY+perceH;
		setTimeout("compare()",50)
	}else{
		window.onscroll=function(){
			jQuery("#compare").get(0).style.top=parseInt(jQuery("#compare").get(0).style.top)+"px";
			jQuery("#compare").get(0).style.position="fixed";
		}
	}
}
function clearCompare(){
	jQuery("#comProlist").empty();
	jQuery("#compare").hide();
	createCookie("compare","");
	proIDs=new Array();
}
function addToCompare(checkobj,checkid,checkProName){

	compare();
	jQuery("#compare").show();
	jQuery(".comPro").show();
	var proIDsTemp=proIDs.join(".");
	if (proIDsTemp.indexOf(checkid)==-1){
		if (proIDs.length<3){
			proIDs.push(checkid);
			jQuery("#comProlist").append("<li id='check_"+ checkid +"'><a title='删除' class='close' onclick='reduceCompare("+ checkid +")'></a>"+checkProName+"</li>");
			writeCompare(checkid,checkProName);
		}else{
			alert("对不起，最多可以选择三种商品进行对比！");
		}
	}else{
		alert("对不起，您已经选择此商品！");
		return;
	}	
}
function reduceCompare(checkid){
	jQuery("#check_"+ checkid).remove();
	jQuery.each(proIDs,function(i,n){
		if (checkid==n){
			proIDs.splice(i,1);			
		}
	});
	var coo=readCookie("compare");
	var idindexstart=coo.indexOf(checkid);
	var idindexend=coo.indexOf("|||",idindexstart)+3;
	var delStr=coo.substring(idindexstart,idindexend);
	var innerStr=coo.replace(delStr,"")
	createCookie("compare",innerStr);
	if (proIDs.length==0){jQuery(".comPro").hide();}
}
function openCompare(){
	switch (proIDs.length){
		case 1:
			alert("对不起，最少选择两种商品进行对比！");
			break;
		case 2:
			window.open("product_contrast.aspx?s1="+ proIDs[0] +"&s2="+ proIDs[1]);
			break;
		case 3:
			window.open("product_contrast.aspx?s1="+ proIDs[0] +"&s2="+ proIDs[1] +"&s3="+ proIDs[2]);
			break;	
		default:
			alert("请选择2-3件商品进行对比！");
			return;
	}	
}
function writeCompare(checkid,checkProName){
	var compareList=readCookie("compare");
	if (compareList==null){compareList="";}
	compareList+=checkid+"||"+escape(checkProName)+"|||";
	createCookie("compare",compareList);
}
function isCoo(){
	var coo=readCookie("compare");
	if (coo){
		var cootemp=coo.split("|||");
		var compareListTemp="";
		for(var i=0;i<cootemp.length-1;i++){
			compareListTemp+="<li id='check_"+ cootemp[i].split("||")[0] +"'><a title='删除' class='close' onclick='reduceCompare("+ cootemp[i].split("||")[0] +")'></a>"+unescape(cootemp[i].split("||")[1])+"</li>";
			proIDs.push(cootemp[i].split("||")[0]);
		}
		jQuery("#comProlist").html(compareListTemp);
		jQuery("#compare").show();
		jQuery(".comPro").show();
	}	
}
/*
cookie
*/
function createCookie(name,value,days,Tdom){
	var Tdom=(Tdom)?Tdom:"/";
	if (days){
		var date = new Date();
		date.setTime(date.getTime()+(days*24*60*60*1000));
		var expires = "; expires="+date.toGMTString();
	}else{
		var expires = "";		
	}
	document.cookie = name+"="+value+expires+"; path="+Tdom;
}
function readCookie(name){
	var nameEQ = name + "=";
	var ca = document.cookie.split(';');
	for(var i=0;i < ca.length;i++){
		var c = ca[i];
		while (c.charAt(0)==' ') {c = c.substring(1,c.length);}
		if (c.indexOf(nameEQ) == 0) {return c.substring(nameEQ.length,c.length);}
	}
	return null;
}
