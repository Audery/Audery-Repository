var href = location.href, adminPath = href.lastIndexOf("admin/") == -1 ? href.substring(0, href.lastIndexOf("/") + 1) : href.substring(0, href.lastIndexOf("admin/") + 6);
function $s(id) { return document.getElementById(id);}
/*关闭对话窗口后进行的处理*/closedivfunc = function () { };function closediv(objDiv) { closedivfunc(); objDiv.parentNode.removeChild(objDiv); }function closedivreload() { window.location.reload(); }
/*确认*/
function ReturnFun(Return_Strs) {
    if (isArray(LastSelectObj)) { for (var i = 0; i < LastSelectObj.length; i++) { SetValue(LastSelectObj[i], Return_Strs[i]); }}
    else SetValue(LastSelectObj, Return_Strs);
    $s("s_id").style.display = "none";
    if (typeof (closedivfunc) == "function") closedivfunc();
}
function SetValue(obj, val) {
    if (obj == null || typeof (obj) == "undefined") return;
    if (typeof (obj) == "string") obj = $s(obj);
    if (val == null || typeof (val) == "undefined") val = '';
    if (typeof (obj.value) == "undefined") obj.innerHTML = val; else obj.value = val;
}
function ReturnLabelValueText(value) {
    try { if (value != "") { var oEditor = FCKeditorAPI.GetInstance("ctl00_workspace_txtContent"); if (oEditor.EditMode == FCK_EDITMODE_WYSIWYG) oEditor.InsertHtml(value);else return false;}}
    catch (e) { insert(value); } finally { $s("LabelDivid").style.display = "none"; if (typeof (closedivfunc) == "function") closedivfunc(); }
}
/*浮动在页面上方的页面*/
function ShowImg(control) { if (typeof (control) != "undefined" && typeof (control.id) != "undefined" && $s(control.id + "Img")!=null) $s(control.id + "Img").src = control.value;}
/*判断是否数组*/
function isArray(obj) { if (obj != undefined) return Array.isArray(obj) || d.type(obj) === "array" || obj.constructor == window.Array;else return false;}
function showlabelDiv(obj, content, width) {
    var pos = getPosition(obj), objDiv = $s("LabelDivid");
    if (objDiv == null) { objDiv = document.createElement("div"); objDiv.id = "LabelDivid";}
    objDiv.style.className = "selectStyle"; //For IE
    objDiv.style.border = "1px double #B4C9C6";
    objDiv.style.backgroundColor = "#F4FAFF";
    objDiv.style.color = "#000000";
    objDiv.style.lineheight = "18px";
    objDiv.style.padding = "1px";
    objDiv.style.filter = "progid:DXImageTransform.Microsoft.DropShadow(color=#C5C5C5,offX=2,offY=2,positives=true)";
    objDiv.style.position = "absolute";
    var tempheight = pos.y, tempwidth1, tempheight1, windowwidth = document.body.clientWidth;
    var isIE5 = (navigator.appVersion.indexOf("MSIE 5") > 0) || (navigator.appVersion.indexOf("MSIE") > 0 && parseInt(navigator.appVersion) > 4);
    var isIE55 = (navigator.appVersion.indexOf("MSIE 5.5") > 0);var isIE6 = (navigator.appVersion.indexOf("MSIE 6") > 0);var isIE7 = (navigator.appVersion.indexOf("MSIE 7") > 0);
    if (isIE5 || isIE55 || isIE6 || isIE7) { var tempwidth = pos.x + 305; } else { var tempwidth = pos.x + 312; }
    objDiv.style.width = width + "px"; objDiv.innerHTML = content;
    if (tempwidth > windowwidth) {tempwidth1 = tempwidth - windowwidth; objDiv.style.left = (pos.x - tempwidth1) + "px";}
    else { if (isIE5 || isIE55 || isIE6 || isIE7) { objDiv.style.left = (pos.x) + "px"; } else { objDiv.style.left = (pos.x) + "px"; }}
    if (isIE5 || isIE55 || isIE6 || isIE7) { objDiv.style.top = (pos.y + 22) + "px"; } else { objDiv.style.top = (pos.y + 22) + "px"; }
    objDiv.style.display = "";document.ondblclick = function () {if (objDiv.style.display == "") { objDiv.style.display = "none"; } if (typeof (closedivfunc) == "function") closedivfunc();};
    if ($s("CloseBtn") != null) $s("CloseBtn").click = function () { if (objDiv.style.display == "") { objDiv.style.display = "none"; } if (typeof (closedivfunc) == "function") closedivfunc();};
    document.body.appendChild(objDiv);
}
/*后台全选-取消*/
function CheckAll(form) {
    for (var i = 0; i < form.elements.length; i++) {
        var e = form.elements[i];
        if (e.type == "checkbox" && e.name == "chk" && !e.disabled && e.value) {
            e.checked = form.chkAll.checked;
            e.onclick = function (e) { e = e.target || event.srcElement; if (!e.checked) form.chkAll.checked = false; SetAllChecked(form); };
        } else if (e.type == "checkbox" && e.name == "chk" && !e.value) {
            e.disabled = true;
        }
    }
    SetAllChecked(form);
}
/*后台全选-取值*/
function GetAllChecked() {
    var v = "";
    var tb = $s("tablist");
    for (var i = 0; i < tb.rows.length; i++) {
        var tr = tb.rows[i]; if (tr.cells.length < 2) continue; var td = tr.cells[0];
        for (var k = 0; k < td.childNodes.length; k++) {
            var e = td.childNodes[k];
            if (e.type == "checkbox" && e.name == "chk" && !e.disabled && e.value && e.checked) v += e.value + ",";
        }
    }
    return v.length?v.substring(0,v.length-1):v;
}
function SetAllChecked(form) {
    if (typeof (form.chks) == "undefined") {
        var chks = document.createElement("input"); chks.setAttribute("type", "hidden"); chks.setAttribute("name", "chks");
        chks.value = GetAllChecked(); form.appendChild(chks);
    } else {
        form.chks.value = GetAllChecked();
    }
}
/*选项卡切换*/
function ShowDiv(id, num, changeClass) {
    changeClass = changeClass || 0;
    for (var i = 0, tab; i < num; i++) {
        tab = jQuery("#tab" + i); if (tab.length == 0) tab = jQuery("#" + i); 
        if (tab.length) {
            tab = tab.get(0);
            if (i == id) {
                if (changeClass) jQuery("#TabTitle" + i).get(0).className = "titlemouseover";
                tab.style.display = 'block';
            }
            else {
                if (changeClass) jQuery("#TabTitle" + i).get(0).className = "tabtitle";
                tab.style.display = 'none';
            }
        }
    }
}
function ShowTabs(id, num, changeClass) { ShowDiv(id, num, typeof (changeClass) == "undefined" || !changeClass ? "titlemouseover" : changeClass);}
function MaxLength(field, maxlimit) { var j = field.value.replace(/[^\x00-\xff]/g, "**").length; var tempString = field.value; var tt = ""; if (j > maxlimit) { for (var i = 0; i < maxlimit; i++) { if (tt.replace(/[^\x00-\xff]/g, "**").length < maxlimit) tt = tempString.substr(0, i + 1); else break;}if (tt.replace(/[^\x00-\xff]/g, "**").length > maxlimit) tt = tt.substr(0, tt.length - 1); field.value = tt;}}
position = function (x, y) {  this.x = x; this.y = y;};
getPosition = function (oElement) { var objParent = oElement; var oPosition = new position(0, 0); while (objParent != null && objParent.tagName != "BODY") { oPosition.x += objParent.offsetLeft; oPosition.y += objParent.offsetTop; objParent = objParent.offsetParent; } return oPosition; };
function showfDiv(obj, content, width) { //return showlabelDiv(obj, content, width);
    var pos = new position(0, 0);if (obj != null) { if (obj.getAttribute("position") != null && obj.getAttribute("position").toString() != "") { eval("pos=" + obj.getAttribute("position").toString());} else pos = getPosition(obj);}
    var objDiv = $s("s_id"); if (objDiv == null) { objDiv = document.createElement("div"); objDiv.id = "s_id"; }
    objDiv.className = "selectStyle"; objDiv.style.position = "absolute"; objDiv.style.backgroundColor = "#FFFFFF";
    var tempheight = pos.y,tempwidth1, tempheight1,windowwidth = document.body.clientWidth;
    var isIE5 = (navigator.appVersion.indexOf("MSIE 5") > 0) || (navigator.appVersion.indexOf("MSIE") > 0 && parseInt(navigator.appVersion) > 4),isIE55 = (navigator.appVersion.indexOf("MSIE 5.5") > 0), isIE6 = (navigator.appVersion.indexOf("MSIE 6") > 0), isIE7 = (navigator.appVersion.indexOf("MSIE 7") > 0);
    if (isIE5 || isIE55 || isIE6 || isIE7) { var tempwidth = pos.x + 305; } else { var tempwidth = pos.x + 312; }
    objDiv.style.width = width + "px"; objDiv.innerHTML = content;
    if (tempwidth > windowwidth) { tempwidth1 = tempwidth - windowwidth; objDiv.style.left = (pos.x - tempwidth1) + "px"; }
    else { if (isIE5 || isIE55 || isIE6 || isIE7) { objDiv.style.left = (pos.x) + "px"; } else { objDiv.style.left = (pos.x) + "px"; }}
    if (isIE5 || isIE55 || isIE6 || isIE7) { objDiv.style.top = (pos.y + 22) + "px"; } else { objDiv.style.top = (pos.y + 22) + "px"; }
    objDiv.style.display = ""; document.ondblclick = function () { if (objDiv.style.display == "") { objDiv.style.display = "none"; } if (typeof (closedivfunc) == "function") closedivfunc();};
    if ($s("CloseBtn") != null) $s("CloseBtn").click = function () { if (objDiv.style.display == "") { objDiv.style.display = "none"; } if (typeof (closedivfunc) == "function") closedivfunc();};
    document.body.appendChild(objDiv);
}
/*移动*/
drag = function (a, o) { var d = document; if (!a) a = window.event; if (!a.pageX) a.pageX = a.clientX; if (!a.pageY) a.pageY = a.clientY;var x = a.pageX, y = a.pageY; if (o.setCapture) o.setCapture(); else if (window.captureEvents) window.captureEvents(Event.MOUSEMOVE | Event.MOUSEUP); var backData = { x: o.style.top, y: o.style.left };d.onmousemove = function (a) { if (!a) a = window.event; if (!a.pageX) a.pageX = a.clientX; if (!a.pageY) a.pageY = a.clientY; var tx = a.pageX - x + parseInt(o.style.left), ty = a.pageY - y + parseInt(o.style.top); o.style.left = tx + "px"; o.style.top = ty + "px"; x = a.pageX; y = a.pageY; };d.onmouseup = function (a) { if (!a) a = window.event; if (o.releaseCapture) o.releaseCapture(); else if (window.captureEvents) window.captureEvents(Event.MOUSEMOVE | Event.MOUSEUP);d.onmousemove = null; d.onmouseup = null; if (!a.pageX) a.pageX = a.clientX; if (!a.pageY) a.pageY = a.clientY; if (!document.body.pageWidth) document.body.pageWidth = document.body.clientWidth;if (!document.body.pageHeight) document.body.pageHeight = document.body.clientHeight; if (a.pageX < 1 || a.pageY < 1 || a.pageX > document.body.pageWidth || a.pageY > document.body.pageHeight) {o.style.left = backData.y; o.style.top = backData.x;}}};
function selectFile(type, obj, height, width, dummypaht, queryString) {
    var ShowObj = obj; if (isArray(obj) && obj.length > 1) ShowObj = obj[1];
    if (typeof (ShowObj) == "string") ShowObj = $s(ShowObj);
    showfDiv(ShowObj, "页面加载中...", width);
    LastSelectObj = obj;
    var options = { method: 'get', parameters: "widths=" + width + "&heights=" + height + "&queryString=" + queryString + "&adminPath=" + adminPath, onComplete: function (transport) { showfDiv(ShowObj, transport.responseText, width); } }; dummypaht = (typeof (dummypaht) == "undefined" || dummypaht == false ? adminPath.replace("admin/", "") : dummypaht);
    new Ajax.Request(dummypaht + 'admin/include/dialog.aspx?FileType=' + type.split("|")[0], options);
}
function showPath(type, obj, title, label_width, height, path, id) {
    var label_temp1 = "<div onmousedown=\"drag(event,$s('LabelDivid'));\" style=\"text-decoration: none;padding-left:3px;background-color:#EDEFEA;font-size: 12px;color: #4499CC;font-weight:\" class=\"titile_bg\" style=\"cursor:move;\">\
    <table style=\"width:100%;height:26px\">\
    <tr>\
    <td>\
    <font style='font-size:12px' color=\"#0099CC\">" + title + "</font></td>\
    <td style=\"width:40px\">\
    <span style='cursor:pointer;font-size:12px;font-weight:bold;color:#0099CC' onclick=\"closediv($s('LabelDivid'));\">关闭</span>\
    </td>\
    </tr>\
    </table>\
    </div>\
    <iframe src=";
    var label_temp2 = " frameborder=\"0\" id=\"select_main\" scrolling=\"yes\" name=\"select_main\" width=\"100%\" height=\"" + height + "px\" />";
    var label_temp3 = "";
    switch (type) {
        case "ProductSparepart": //选择配件
            label_temp3 = label_temp1 + "" + path + "admin/include/sparepartproduct.aspx?sparepartId=" + id + "" + label_temp2;
            break;
        case "StartSpecifications": //开启规格
            label_temp3 = label_temp1 + "" + path + "admin/include/specifications_add.aspx?type=1" + label_temp2;
            break;
        case "AddSpecifications": //新增规格
            label_temp3 = label_temp1 + "" + path + "admin/include/specifications_add.aspx?type=2" + label_temp2;
            break;
        case "UserShoppingInfo": //修改收货人信息
            label_temp3 = label_temp1 + "" + path + "admin/include/usershoppinginfo.aspx?userid=" + id + label_temp2;
            break;
        case "SetMemberPrice": //设置会员价格
            var strId = id.split(';');
            var shopPrice = $s(strId[0]).value;
            var memberPrice = $s(strId[1]).value;
            label_temp3 = label_temp1 + "" + path + "admin/include/memberpriceset.aspx?txtContrl=" + strId[1] + "&shopPrice=" + shopPrice + "&MemberPrice=" + memberPrice + label_temp2;
            break;
        default:
            break;
    }
    showlabelDiv(obj, label_temp3, label_width);
}
function show(type, obj, title, label_width, height) {
    var label_temp1 = "<div onmousedown=\"drag(event,$s('LabelDivid'));\" style=\"text-decoration: none;padding-left:3px;background-color:#EDEFEA;font-size: 12px;color: #4499CC;font-weight:\" class=\"titile_bg\" style=\"cursor:move;\">\
    <table style=\"width:100%;height:26px\">\
    <tr>\
    <td>\
    <font style='font-size:12px' color=\"#0099CC\">" + title + "</font></td>\
    <td style=\"width:40px\">\
    <span style='cursor:pointer;font-size:12px;font-weight:bold;color:#0099CC' onclick=\"closediv($s('LabelDivid'));\">关闭</span>\
    </td>\
    </tr>\
    </table>\
    </div>\
    <iframe src=";
    var label_temp2 = " frameborder=\"0\" id=\"select_main\" scrolling=\"yes\" name=\"select_main\" width=\"100%\" height=\"" + height + "px\" />";
    var label_temp3 = "";
    switch (type) {
        case "systemlabel": //系统标签
            label_temp3 = label_temp1 + "label/selectlabel.aspx?w_d_tid=1" + label_temp2;
            break;
        case "definlabel": //用户标签
            label_temp3 = label_temp1 + "label/selectlabel.aspx?w_n_tid=1" + label_temp2;
            break;
        case "freelabel": //自由标签
            label_temp3 = label_temp1 + "label/selectfreelabel.aspx?w_d_free_labelclass=3" + label_temp2;
            break;
        case "ProductBrand":
            label_temp3 = label_temp1 + "productbrand_parameter.aspx" + label_temp2;
            break;
        case "ProductListMode":
            label_temp3 = label_temp1 + "productlistmode_parameter.aspx" + label_temp2;
            break;
        case "ProductClass":
            label_temp3 = label_temp1 + "productclass_parameter.aspx" + label_temp2;
            break;
        case "ProductList":
            label_temp3 = label_temp1 + "productlist_parameter.aspx?LabelClass=0" + label_temp2;
            break;
        case "StoreProductList":
            label_temp3 = label_temp1 + "productlist_parameter.aspx?LabelClass=1" + label_temp2;
            break;
        case "ProductContent":
            label_temp3 = label_temp1 + "productcontent_parameter.aspx" + label_temp2;
            break;
        case "TeamBuy":
            label_temp3 = label_temp1 + "Lable/TeamBuy_Parameter.aspx" + label_temp2;
            break;
        case "Friendlink":
            label_temp3 = label_temp1 + "friedlink_parameter.aspx" + label_temp2;
            break;
        case "Survey":
            label_temp3 = label_temp1 + "Lable/Survey.aspx" + label_temp2;
            break;
        case "AdvancedSearch":
            label_temp3 = label_temp1 + "advancedsearch_parameter.aspx" + label_temp2;
            break;
        case "LeaveWrodInfo": //留言信息列表
            label_temp3 = label_temp1 + "leaveword_parameter.aspx" + label_temp2;
            break;
        case "LeaveWrodForm": //留言表单
            label_temp3 = label_temp1 + "leavewordform_parameter.aspx" + label_temp2;
            break;
        case "MemberRegForm": //用户注册表单
            label_temp3 = label_temp1 + "memberregform_parameter.aspx" + label_temp2;
            break;
        case "MemberAgree": //用户注册协议
            label_temp3 = label_temp1 + "memberagree_parameter.aspx" + label_temp2;
            break;
        case "shoppingcart":
            label_temp3 = label_temp1 + "shopintcart_parameter.aspx" + label_temp2;
            break;
        case "NewsList":
            label_temp3 = label_temp1 + "articlelist_parameter.aspx" + label_temp2;
            break;
        case "NewsContent":
            label_temp3 = label_temp1 + "refercontent_parameter.aspx" + label_temp2;
            break;
        case "ImportFile":
            label_temp3 = label_temp1 + "importfile.aspx" + label_temp2;
            break;
        case "websitetop":
            label_temp3 = label_temp1 + "label_top.aspx" + label_temp2;
            break;
        case "websitebottom":
            label_temp3 = label_temp1 + "label_bottom.aspx" + label_temp2;
            break;
        case "CreateURL":
            label_temp3 = label_temp1 + "label_createurl.aspx" + label_temp2;
            break; Advertise
        case "Advertise":
            label_temp3 = label_temp1 + "advertise_parameter.aspx" + label_temp2;
            break;
        case "CommentInfo":
            label_temp3 = label_temp1 + "commentinfo_parameter.aspx" + label_temp2;
            break;
        case "ReplyInfo":
            label_temp3 = label_temp1 + "replyinfo_parameter.aspx" + label_temp2;
            break;
        case "CommentForm":
            label_temp3 = label_temp1 + "commentform_parameter.aspx" + label_temp2;
            break;
        case "Login": //会员登陆
            label_temp3 = label_temp1 + "login_parameter.aspx" + label_temp2;
            break;
        case "usershopinginfo":
            label_temp3 = label_temp1 + "usershopinginfo_parameter.aspx" + label_temp2;
            break;
        case "ProductGroupBuyInfo": //团购商品活动信息
            label_temp3 = label_temp1 + "groupbuyactivityinfo_parameter.aspx" + label_temp2;
            break;
        case "scanproduct":
            label_temp3 = label_temp1 + "scantype_parameter.aspx" + label_temp2;
            break;
        case "History":
            label_temp3 = label_temp1 + "history_parameter.aspx" + label_temp2;
            break;
        case "ProductExtract":
            label_temp3 = label_temp1 + "extract_parameter.aspx" + label_temp2;
            break;
        case "StoreInfo": //店铺信息
            label_temp3 = label_temp1 + "storeinfo_parameter.aspx" + label_temp2;
            break;
        case "StoreType": //店铺类型
            label_temp3 = label_temp1 + "storetype_parameter.aspx" + label_temp2;
            break;
        case "StoreSearch": //店铺搜索
            label_temp3 = label_temp1 + "storesearchform_parameter.aspx" + label_temp2;
            break;
        case "ProductProperty":
            label_temp3 = label_temp1 + "property_parameter.aspx" + label_temp2;
            break;
        default:
            break;
    }
    showlabelDiv(obj, label_temp3, label_width);
}
function showShopCart(type, obj, title, label_width, height, productId, path, specificationValue) {
    var label_temp1 = "<div onmousedown=\"drag(event,$s('LabelDivid'));\" style=\"text-decoration: none;padding-left:3px;background-color:#EDEFEA;font-size: 12px;color: #4499CC;font-weight:\" class=\"titile_bg\" style=\"cursor:move;\">\
    <table style=\"width:100%;height:26px\">\
    <tr>\
    <td>\
    <font style='font-size:12px' color=\"#0099CC\">" + title + "</font></td>\
    <td style=\"width:40px\">\
    <span style='cursor:pointer;font-size:12px;font-weight:bold;color:#0099CC' onclick=\"javascript:removeChild($s('LabelDivid'))\">双击关闭</span>\
    </td>\
    </tr>\
    </table>\
    </div>\
    <iframe src=" + path;
    var label_temp2 = " frameborder=\"0\" scrolling=\"no\" id=\"select_main\" scrolling=\"yes\" name=\"select_main\" width=\"100%\" height=\"" + height + "px\" />";
    var label_temp3 = "";
    switch (type) {
        case "showShopCart":
            label_temp3 = label_temp1 + "controls/floatingshopcart.aspx?productId=" + productId + "&q_proSpecification=" + specificationValue + "" + label_temp2;
            break;
    }
    showlabelDiv(obj, label_temp3, label_width);
}
