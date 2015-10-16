// JavaScript Document
var jumpto=true;
function showlabel(obj,mouse,inc,chtype)
{
	if(mouse=='over')
	{
		obj.style.filter='Alpha(Opacity=80)';
		obj.style.cursor='hand';
		obj.title='鐐瑰嚮淇敼';
	}
	else if(mouse=='out')
	{
		obj.style.filter='Alpha(Opacity=50)';
	}
	else if(mouse=='click'&&jumpto==true)
	{
	    window.self.location='label/selectlabelclass.aspx?LT=' + inc + '&LN=' + chtype + '';
	}
}

function showlabeltype(obj,mouse,inc,chtype,filename,filepath,styleid)
{
	if(mouse=='click'&&jumpto==true)
	{
//	alert('label/selectlabelclass.aspx?LT=' + inc + '&FileName=' + filename + '&FilePath=' + filepath + '&StyleId='+styleid+'&LN=' + chtype + '')
	    window.self.location='label/selectlabelclass.aspx?LT=' + inc + '&FileName=' + filename + '&FilePath=' + filepath + '&StyleId='+styleid+'&LN=' + chtype + '';
	}
}

//璺冲埌淇敼鑷敱鏍囩�?
function showFreelabel(obj,mouse,inc){
	if(mouse=='over'){
		obj.style.filter='Alpha(Opacity=80)';
		obj.style.cursor='hand';
		obj.title='鐐瑰嚮淇敼';
	}else if(mouse=='out'){
		obj.style.filter='Alpha(Opacity=50)';
	}else if(mouse=='click'&&jumpto==true){
	window.self.location='label/freedomlabel_edit.aspx?id=' + inc +'&type=0';
	}
}

function ckjump(type)
{
	if(type==1)
	{
		jumpto=true;
	}
	else
	{
		jumpto=false;
	}
}
var layobj=null;
var potype=null;
var ifchange=null;
function change_po(type,t,tag)
{
	ifchange=t;
	layobj=document.getElementById(tag);
	potype=type;
	change_ls();
}

function change_ls(){
 
	var obj=layobj;
	var type=potype;
	if(type=='up'){
		num=(parseInt(obj.style.height)-1);
		obj.style.height=num+'px';
	}else if(type=='left'){
		num=(parseInt(obj.style.width)-1);
		obj.style.width=num+'px';
	}else if(type=='down'){
		num=(parseInt(obj.style.height)+1);
		obj.style.height=num+'px';
	}else if(type=='right'){
		num=(parseInt(obj.style.width)+1);
		obj.style.width=num+'px';
	}

	if(ifchange==1){
		window.setTimeout('change_ls()',40);
	}
}


