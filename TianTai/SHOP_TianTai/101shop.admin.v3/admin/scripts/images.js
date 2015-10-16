/*鼠标指向后带图片的提示效果*/
function showPic(sUrl)
{
    if(sUrl.length==1)
    {
       return false;
    }
     var x,y;
     x = event.clientX;
     y = event.clientY;
     document.getElementById("Layer1").style.left = x;
     document.getElementById("Layer1").style.top = y;
     document.getElementById("Layer1").innerHTML = "<img src=\"" + sUrl + "\">";
     document.getElementById("Layer1").style.display = "block";
}
function hiddenPic()
{
     document.getElementById("Layer1").innerHTML = "";
     document.getElementById("Layer1").style.display = "none";
}