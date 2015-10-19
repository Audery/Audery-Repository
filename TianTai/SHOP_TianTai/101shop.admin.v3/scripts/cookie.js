//取得项名称为offset的cookie值 
function GetCookieVal (offset)
{
    var endstr = document.cookie.indexOf (";", offset);
    if (endstr == -1)
    endstr = document.cookie.length;
    return unescape(document.cookie.substring(offset, endstr));
}
 
//取得名称为name的cookie值 
function GetCookie (name) {
    var arg = name + "=";
    var alen = arg.length;
    var clen = document.cookie.length;
    var i = 0;
    while (i < clen)
    {
        var j = i + alen;
        if (document.cookie.substring(i, j) == arg)
        return GetCookieVal (j);
        i = document.cookie.indexOf(" ", i) + 1;
        if (i == 0) break;
    }
    return null;
}
 
//删除名称为name的Cookie
function DeleteCookie (name) 
{   
    var exp = new Date();
    exp.setTime (exp.getTime() - 1);
    var cval = GetCookie (name);
    document.cookie = name + "=" + cval + "; expires=" + exp.toGMTString();
}
 
//清除COOKIE
function ClearCookies()
{
    var temp=document.cookie.split(";");
    var ts;
    for (var i=0;;i++)
    {
       if(!temp[i])break;
       ts=temp[i].split("=")[0];
       var regstr = "history_"    
       var re = new RegExp(regstr); 
       if(ts.search(re) != -1)
       {
          DeleteCookie(ts);
       }            
    }
    document.getElementById("ulhistory").innerHTML="<span>暂无浏览记录！</span>";
}
