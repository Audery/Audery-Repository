﻿var Ajax; if (!Ajax) Ajax = {};Ajax.Request = function (url, options) { var defaults = { url: url, method: 'post', parameters: null, cache: false, dataType: 'text', evalJSON: false, evalJS: false, evalXML: false }; var options = jQuery.extend(defaults, options); options.type = options.method; options.data = options.parameters; options.async = options.asynchronous; options.scriptCharset = options.encoding; options.dataType = options.evalJSON ? 'json' : (options.evalJS ? 'script' : (options.evalXML ? 'xml' : options.dataType)); options.beforeSend = options.onCreate; options.complete = options.onComplete; options.success = options.onSuccess; return jQuery.ajax(options); };
var $; if (!$) $ = {}; $.post2 = function (url, params) { var form = document.createElement('form'); form.action = url; form.method = 'POST'; form.style.display = 'none'; for (var param in params) { var input = document.createElement('input'); input.type = 'hidden'; input.name = param; input.value = params[param]; form.appendChild(input); } document.body.appendChild(form); form.submit(); return false; }; $.get2 = function (url, params) { var form = document.createElement('form'); form.action = url; form.method = 'GET'; form.style.display = 'none'; for (var param in params) { var input = document.createElement('input'); input.type = 'hidden'; input.name = param; input.value = params[param]; form.appendChild(input); } document.body.appendChild(form); form.submit(); return false; }; $.getCookie = function (sName) { var aCookie = document.cookie.split("; "); for (var i = 0; i < aCookie.length; i++) { var aCrumb = aCookie[i].split("="); if (sName == aCrumb[0]) return decodeURIComponent(aCrumb[1]); } return ''; }; $.setCookie = function (sName, sValue, sExpires) { var sCookie = sName + "=" + encodeURIComponent(sValue); if (sExpires != null) sCookie += "; expires=" + sExpires; document.cookie = sCookie; }; $.removeCookie = function (sName) { document.cookie = sName + "=; expires=Fri, 31 Dec 1999 23:59:59 GMT;"; };
var $s;if (!$s) $s = function (id) { var obj = document.getElementById(id); if (obj == null) { obj = $(id); return (obj.length?obj[0]:obj); } else { return obj;}};
var GetStringInfo; if (!GetStringInfo) GetStringInfo = function (url, controlid) { new Ajax.Request(url, { method: 'get', encoding: 'GBK', onSuccess: function (transport) { var lblCheckInfo=document.getElementById(controlid);if(lblCheckInfo!=null)lblCheckInfo.innerHTML=(transport.responseText);}})}
var Ajax;if(!Ajax)Ajax={};Ajax.Request=function(_,A){var $={url:_,method:"post",parameters:null,cache:false,dataType:"text",evalJSON:false,evalJS:false,evalXML:false},A=jQuery.extend($,A);A.type=A.method;A.data=A.parameters;A.async=A.asynchronous;A.scriptCharset=A.encoding;A.dataType=A.evalJSON?"json":(A.evalJS?"script":(A.evalXML?"xml":A.dataType));A.beforeSend=A.onCreate;A.complete=A.onComplete;A.success=A.onSuccess;return jQuery.ajax(A)};var $;if(!$)$={};$.post2=function(C,B){var _=document.createElement("form");_.action=C;_.method="POST";_.style.display="none";for(var A in B){var $=document.createElement("input");$.type="hidden";$.name=A;$.value=B[A];_.appendChild($)}document.body.appendChild(_);_.submit();return false};$.get2=function(C,B){var _=document.createElement("form");_.action=C;_.method="GET";_.style.display="none";for(var A in B){var $=document.createElement("input");$.type="hidden";$.name=A;$.value=B[A];_.appendChild($)}document.body.appendChild(_);_.submit();return false};$.getCookie=function(_){var $=document.cookie.split("; ");for(var B=0;B<$.length;B++){var A=$[B].split("=");if(_==A[0])return decodeURIComponent(A[1])}return""};$.setCookie=function(B,_,A){var $=B+"="+encodeURIComponent(_);if(A!=null)$+="; expires="+A;document.cookie=$};$.removeCookie=function($){document.cookie=$+"=; expires=Fri, 31 Dec 1999 23:59:59 GMT;"};var $s;if(!$s)$s=function(_){var A=document.getElementById(_);if(A==null){A=$(_);return(A.length?A[0]:A)}else return A};var GetStringInfo;if(!GetStringInfo)GetStringInfo=function(_,$){new Ajax.Request(_,{method:"get",encoding:"GBK",onSuccess:function(A){var _=document.getElementById($);if(_!=null)_.innerHTML=(A.responseText)}})}