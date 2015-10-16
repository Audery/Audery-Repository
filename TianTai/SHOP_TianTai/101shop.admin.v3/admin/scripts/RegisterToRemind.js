<!--
//var config
var adminroot = document.URL.substring(0,document.URL.indexOf("/admin/")+7)+"";
var adminRegisterToRemindURL = adminroot.replace("/admin/","/") + "filehandle/userregform.ashx", adminRegisterToRemindParam = "Option=NewRegisteredMembers";
var box20, content20, contentLen = 0, showfacebox20 = true, showfaceboxTimeout = null, showfaceboxTimeoutMinutes = 1000;
//output html
document.write('<div class="facebox20" onmouseover="showfacebox20=false;" style="display:none;">');
document.write('<h3>提示消息</h3><a href="#" class="close" onclick="box20.fadeOut(1000, closefacebox);confirm2();"></a>');
document.write('<p class="content"><br /></p>');
document.write('<a href="'+adminroot+'member/member_list.aspx?remind=view" target="manFrame" class="more">点此查看</a></div>');
document.write('<style type="text/css">.facebox20{ position:fixed;right:0;bottom:1px;clear:both;background:url('+adminroot+'images/face-box/bg.png) no-repeat 0 0 ;width:191px;height:131px;font-size:12px;color:#366F8a;line-height:16px;}.facebox20 h3{ background:url('+adminroot+'images/face-box/icon.png) no-repeat 5px center;padding-left:30px;line-height:25px;font-weight:normal;font-size:12px;margin:0;}.facebox20 .yel{ color:#FF6500;font-weight:bold;}.facebox20 a{color:#366F8a;text-decoration:none;}.facebox20 a:hover{ text-decoration:underline;}.facebox20 p{ margin:5px 10px 0px 10px;height:80px;}.facebox20 p a{ display:block; margin-bottom:3px;}.facebox20 .close{ position:absolute;top:3px;right:5px;height:18px;width:20px;background:url('+adminroot+'images/face-box/x.gif) no-repeat 0 0;}.facebox20 .close:hover{ background:url('+adminroot+'images/face-box/x.gif) no-repeat 0 -18px;}.facebox20 .a{display:inline;padding:2px 3px;width:78px;float:left;overflow:hidden;height:16px;word-break:break-all;}.facebox20 .more{ position:absolute;top:114px;right:10px;}</style><!--[if IE]><style type="text/css">.facebox20{ position:absolute;right:0;bottom:auto;top:expression(eval(document.compatMode&&document.compatMode=="CSS1Compat")?documentElement.scrollTop+(documentElement.clientHeight-this.clientHeight)-1: document.body.scrollTop+(document.body.clientHeight-this.clientHeight)-1);clear:both;</style><![endif]-->');
//defined functions
var showfaceboxFunction = function () {
    box20 = jQuery("div.facebox20"); content20 = jQuery("p.content", box20);
    if (showfacebox20 && box20.length && content20.length) {
        jQuery.ajax({
            type: "POST", url: adminRegisterToRemindURL, async: false, data: adminRegisterToRemindParam, dataType: "text",
            success: function (ret) {
                try {
                    eval("ret=" + ret);
                    if (ret && ret.result && ret.result.length != contentLen) {
                        contentLen = ret.result.length;
                        var htm = '<a href=' + adminroot + 'member/member_list.aspx?remind=view target=manFrame class=yel>新用户注册!</a>';
                        for (var i = 0, txt = '', tit = '', obj = null; i < ret.result.length; i++) {
                            obj = ret.result[i];
                            txt = obj.name;
                            if(txt.indexOf('买家')==0||txt.indexOf('会员')==0){
                                if(obj.truename!=""){
                                txt = obj.truename; tit = obj.name + "[手机:" +(obj.mobilephone!=""?""+obj.mobilephone:"?")+"，位置:"+(obj.location!=""?""+obj.location:"?")+ "]";
                                }else if(obj.mobilephone!=""){
                                txt = obj.mobilephone; tit = obj.name + "[位置:" +(obj.location!=""?""+obj.location:"?")+ "]";
                                }else if(obj.email!=""){
                                txt = obj.email; tit = obj.name + "[位置:" +(obj.location!=""?""+obj.location:"?")+ "]";
                                }else{
                                tit = obj.name + "[位置:" +(obj.location!=""?""+obj.location:"?")+ "]";
                                }
                            }else{
                                tit = obj.name + "[手机:" +(obj.mobilephone!=""?""+obj.mobilephone:"?")+"，位置:"+(obj.location!=""?""+obj.location:"?")+ "]";
                            }
                            htm += '<a href="' + adminroot + 'member/member_view.aspx?remind=view&uid=' + obj.id + '" target="manFrame" title="' + txt + ' ' + tit + '" class="a">' + txt + '</a>';
                        }
                        htm += '<br />';
                        content20.html(htm); box20.fadeIn(1000);
                        if (showfaceboxTimeout) clearTimeout(showfaceboxTimeout);
                        showfaceboxTimeout = setTimeout(function () { if (showfacebox20) box20.fadeOut(1000, closefacebox); }, 5000);
                    }
                } catch (e) { }
            },
            error: function (x, e) {
                //alert("服务器连接失败！");
            },
            complete: function (x) {
                //alert(x.responseText);
            }
        });
    }
};
var closefacebox = function () {
    showfacebox20 = true;
    showfaceboxTimeoutMinutes = showfaceboxTimeoutMinutes * 3;
    if (showfaceboxTimeout) clearTimeout(showfaceboxTimeout);
    showfaceboxTimeout = setTimeout(showfaceboxFunction, showfaceboxTimeoutMinutes);
};
var confirm2 = function(){
    if(confirm("不要再提示此信息了？")){
        jQuery.ajax({
            type: "POST", url: adminRegisterToRemindURL, async: false, data: adminRegisterToRemindParam+"&DoNotTip=Yes", dataType: "text",
            success: function (ret) { },error: function (x, e) { }, complete: function (x) { }
        });
    }
};
closefacebox();
-->