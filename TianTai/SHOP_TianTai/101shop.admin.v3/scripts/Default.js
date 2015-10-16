

function Orders(OrderId)
{
if(OrderId=="")
{
  alert("请输入订单号!");
  return false;
}
  SendAjax("ConMain",OrderId);
}

function SendAjax(op,OrderId)
{
    var param = "Option="+ op +"&OrderId="+ OrderId;
    var options={
        method:'post',
        parameters:param,
        onComplete:
        function(transport)
	    {
	      var retv=transport.responseText;alert(retv);
		}     

	  }
	new  Ajax.Request('Default.aspx',options);
}
function showorhidden(id)
{
	var o=document.getElementById(id);
	if(o.style.display=="none")
	{
		o.style.display="block";
	}else{
		o.style.display="none";
	}
}


function tpck(strid,path)
{
    window.open(''+path+'showVote.aspx?id='+strid,'_blank','height=100%, width=300, top=500,left=500, toolbar=no, menubar=no, scrollbars=no, resizable=no,location=no');
}

function tpsf(strid,path)
{
    alert(strid);
    var dx= 0;
    var sel = "sf"+strid;
    for (var i = 0; i < document.getElementsByName(sel).length; i++)
 　 {
  　    if(document.getElementsByName(sel)[i].checked)
  　　　{
   　　     dx = document.getElementsByName(sel)[i].value;
  　　  }
 　 }
    if(dx==0)
    {
        alert("请选择投票！");
        return false;
    }
    else
    {
        VoteAjax('voteTpsf',strid,dx,path);
    }
}


function tptj(strid,path)
{
    var dx= 0;
    var sel = "dx"+strid;
    for (var i = 0; i < document.getElementsByName(sel).length; i++)
 　 {
  　    if(document.getElementsByName(sel)[i].checked)
  　　　{
   　　     dx = document.getElementsByName(sel)[i].value;
  　  　}
 　 }
    if(dx==0)
    {
        alert("请选择投票！");
        return false;
    }
    else
    {
       VoteAjax('voteTptj',strid,dx,path);
     }
}



function tpdx(strid,path)
{
    var dx= "";
    var sel = "dxs"+strid;
    for (var i = 0; i < document.getElementsByName(sel).length; i++)
 　 {
        if(document.getElementsByName(sel)[i].checked)
  　　　{
            if(dx=="")
            { 
                dx= document.getElementsByName(sel)[i].value;
            }
            else
            {
   　　　　     dx = dx+","+ document.getElementsByName(sel)[i].value;
            }
  　　   }
 　}

   if(dx=="")
   {
   
       alert("请选择投票！");
       return false;
   }
   else 
   {
        VoteAjax('voteTpdx',strid,dx,path);
  }	 
}

function tphd(strid,path)
{
    
   var dxx = "hd"+strid;
   var x =document.getElementById(dxx).value;
   if(document.getElementById(dxx).value.length==0) 
   { 
       alert( "请填写问题!"); 
       return false; 
   }
   else
   {
       VoteAjax('voteTphd',strid,x,path);
       document.getElementById(dxx).value="";
   }
}

function VoteAjax(type,strid,x,str)
{
     var param = "Option="+type+"&strid="+ strid+"&x="+x;
        var options={
        method:'post',
        parameters:param,
        onComplete:
        function(transport)
	    {   
	        var retv = transport.responseText;
            if(retv=="ok")
            {
                alert("谢谢参与！");
                var o=document.getElementById(strid);
	            if(o.style.display=="none")
	            {
		            o.style.display="block";
	            }
	            else
	            {
		            o.style.display="none";
	            }
            }  
		}     

	  }
	  new  Ajax.Request(str+'showVote.aspx',options);
}

/*下至上滚动*/
var rollspeed = 3;
var stop_height =25;
var stop_time =5000;
var inter;
var stop_scroll;
stop_scroll = setInterval(StartRollV, stop_time);
function MarqueeV(){
	var ooRollV=document.getElementById("oRollV");
	var ooRollV1=document.getElementById("oRollV1");
	var ooRollV2=document.getElementById("oRollV2");
	
	if(ooRollV2.offsetTop-ooRollV.scrollTop<=0) {
		ooRollV.scrollTop-=ooRollV1.offsetHeight;
	}else{
		ooRollV.scrollTop++;
	}
	if (ooRollV.scrollTop % stop_height == 0) {
		clearInterval(inter);
		clearInterval(stop_scroll);
		stop_scroll = setInterval(StartRollV, stop_time);
	}
}
function StartRollV() {
	clearInterval(stop_scroll);
	var ooRollV=document.getElementById("oRollV");
	var ooRollV1=document.getElementById("oRollV1");
	var ooRollV2=document.getElementById("oRollV2");
	if (ooRollV) {
		if (parseInt(ooRollV.style.height)>=ooRollV2.offsetTop) {
			ooRollV.style.height = ooRollV2.offsetTop;
			return;
		}
		ooRollV2.innerHTML=ooRollV1.innerHTML;
		inter=setInterval(MarqueeV,rollspeed);
		
		ooRollV.onmouseover=function() {
			clearInterval(inter);
			clearInterval(stop_scroll);
		};
		ooRollV.onmouseout=function() {
			clearInterval(inter);
			clearInterval(stop_scroll);
			inter=setInterval(MarqueeV,rollspeed);
		};
	}
}


