
var tips; var theTop = 40; var old = theTop;

function initFloatTips() {

tips = document.getElementById('divQQbox');

moveTips();

};

function moveTips() {

var tt=50;

if (window.innerHeight) {

pos = window.pageYOffset

}

else if (document.documentElement && document.documentElement.scrollTop) {

pos = document.documentElement.scrollTop

}

else if (document.body) {

pos = document.body.scrollTop;

}

pos=pos-tips.offsetTop+theTop;

pos=tips.offsetTop+pos/10;



if (pos < theTop) pos = theTop;

if (pos != old) {

tips.style.top = pos+"px";

tt=10;

//alert(tips.style.top);

}

old = pos;

setTimeout(moveTips,tt);

}

//!]]>

initFloatTips();

function OnlineOver(){

document.getElementById("divMenu").style.display = "none";

document.getElementById("divOnline").style.display = "block";

document.getElementById("divQQbox").style.width = "145px";

}



function OnlineOut(){

document.getElementById("divMenu").style.display = "block";

document.getElementById("divOnline").style.display = "none";

}


function hideMsgBox(theEvent){ 

 if (theEvent){

 var browser=navigator.userAgent; 

 if (browser.indexOf("Firefox")>0){ 

 if (document.getElementById('divOnline').contains(theEvent.relatedTarget)) { 

 return; 
} 

} 

if (browser.indexOf("MSIE")>0){ //IE

if (document.getElementById('divOnline').contains(event.toElement)) { 

return; 

}

}


}


document.getElementById("divMenu").style.display = "block";

document.getElementById("divOnline").style.display = "none";

}

