var ajaxDebug = true;
var listTable = new Object;
listTable.query = "query";
listTable.filter = new Object;
if (typeof (aurl) != "undefined") {
    listTable.url = aurl;
} else {
    listTable.url = location.href.lastIndexOf("?") == -1 ? location.href.substring((location.href.lastIndexOf("/")) + 1) : location.href.substring((location.href.lastIndexOf("/")) + 1, location.href.lastIndexOf("?"));
    listTable.url += "?is_ajax=1";
}
//alert(listTable.url);
/**
 * 可编辑区样式
 */
listTable.over = function(obj){
    var obj = typeof (obj.tagName) != "undefined" ? $(obj) : $(this);
    obj.attr('title_', (obj.attr('title') == '' ? (typeof obj.attr('title_') == 'undefined' || obj.attr('title_') == '' ? '内容' : obj.attr('title_')) : obj.attr('title')));
    obj.attr('title',(obj.attr('title_').indexOf('点击修改')!=-1?obj.attr('title_'):'点击修改' + obj.attr('title_')));
    obj.attr('background-color', obj.css('background-color')).css('background-color', '#EEF6FB').attr('text-decoration', obj.css('text-decoration')).css('text-decoration', 'blink');
};
listTable.out = function(obj){
    var obj=typeof(obj.tagName)!="undefined"?$(obj):$(this);
    obj.attr('title','');
    obj.css('background-color', obj.attr('background-color')).css('text-decoration', obj.attr('text-decoration'));
};
/**
 * 创建一个可编辑区
 */
listTable.edit = function (obj, act, id, refresh) {
    var fun = obj.getAttribute("function");
    var tag = obj.firstChild.tagName;
    var newtag = "INPUT";
    var oldval = "";
    if (typeof (tag) != "undefined" && (tag.toUpperCase() == "INPUT" || tag.toUpperCase() == "SELECT")) {
        return;
    }
    if (id == undefined || id.toString() == "0") {
        return alert("抱歉，不能进行当前操作，原因是你正在编辑的记录没有提供ID主键。");
    }
    if (typeof (refresh) == "undefined") {
        refresh = 0;
    }
    var params = new Array();
    if (arguments.length > 3) {
        if (arguments.length == 5 && typeof (arguments[3]) != 'undefined' && typeof (arguments[3].tagName) != 'undefined') {
            newtag = arguments[3].tagName; oldval = arguments[4];
        } else {
            for (var i = 3; i < arguments.length; i++) params.push(escape(trim(arguments[i])));
        }
    }

    /* 保存原始的内容 */
    var org = obj.innerHTML;
    var val = window.ActiveXObject ? obj.innerText : obj.textContent;

    /* 创建一个输入框 */
    var txt = null;
    if (newtag.toUpperCase() == "INPUT") {
        txt = document.createElement("INPUT");
        txt.type = "text";
        txt.value = (val == 'N/A' || val.replace(/^[\s&nbsp;]|[\s&nbsp;]$/ig, '') == '') ? '' : val;
        txt.style.width = (obj.offsetWidth < 45 ? obj.offsetWidth + 10 : obj.offsetWidth - 15) + "px";

        /* 隐藏对象中的内容，并将输入框加入到对象中 */
        obj.innerHTML = "";
        obj.appendChild(txt);
        txt.focus();
        try { if (txt.value.length < 20) txt.select(); } catch (e) { }

        /* 编辑区输入事件处理函数 */
        txt.onkeypress = function (e) {
            var evt = (typeof e == "undefined") ? window.event : e;
            var obj = document.all ? evt.srcElement : evt.target;

            if (evt.keyCode == 13) {
                obj.blur();
                return false;
            }

            if (evt.keyCode == 27) {
                obj.parentNode.innerHTML = org;
            }
        };

        /* 编辑区失去焦点的处理函数 */
        txt.onblur = function (e) {
            if (trim(txt.value).length > 0 || true) {
                var val = trim(txt.value); txt.disabled = true;
                $.ajax({
                    url: listTable.url,
                    type: "post",
                    dataType: "text",
                    data: "act=" + act + "&val=" + escape(val) + "&id=" + id + (params.length == 0 ? "" : ("&params=" + params.join(","))),
                    success: function (res, status) {
                        var ok = (res == "ok");
                        if (ok); else if (ajaxDebug) alert(res);
                        if (ok && refresh) {
                            var b = $('input[type="submit"][name^="submit"]');
                            if (document.URL.indexOf('?') == -1 && b.length) {
                                b[0].click();
                            } else if (document.URL.indexOf('?') > 0) {
                                location = document.URL;
                            }
                            return;
                        }
                        if (val == '') {
                            $(obj).html('&nbsp;&nbsp;');
                        } else {
                            obj.innerHTML = ok ? val : org;
                            if (ok) $(obj).html(val).css('background', '').css('text-decoration', '');
                            if (fun != null && fun != "") eval(fun + ".apply(null,['" + obj.id + "'])");
                        }
                    }
                });
            }
            else {
                obj.innerHTML = org;
            }
        };
    } else if (newtag.toUpperCase() == "SELECT") {
        txt = document.createElement("SELECT");
        txt.value = (val == 'N/A' || val.replace('&nbsp;', '') == '') ? '' : val;
        var opt = null, opts = arguments[3].options;
        opt = document.createElement("OPTION");
        opt.value = ''; opt.innerHTML = '请选择';
        txt.appendChild(opt);
        for (var i = 0; i < opts.length; i++) {
            opt = document.createElement("OPTION");
            opt.value = opts[i].value; opt.innerHTML = opts[i].innerHTML;
            opt.selected = (oldval == opt.value);
            txt.appendChild(opt);
        }
        txt.style.width = "77px";

        /* 隐藏对象中的内容，并将输入框加入到对象中 */
        obj.innerHTML = "";
        obj.appendChild(txt);
        txt.focus();

        /* 编辑区失去焦点的处理函数 */
        txt.onchange = function (e) {
            if (txt.selectedIndex > 0) {
                var val = trim(txt.options[txt.selectedIndex].value), valtxt = trim(txt.options[txt.selectedIndex].innerHTML); txt.disabled = true;
                $.ajax({
                    url: listTable.url,
                    type: "post",
                    dataType: "text",
                    data: "act=" + act + "&val=" + escape(val) + "&id=" + id + (params.length == 0 ? "" : ("&params=" + params.join(","))),
                    success: function (res, status) {
                        var ok = (res == "ok");
                        if (ok); else if (ajaxDebug) alert(res);
                        if (ok && refresh) {
                            var b = $('input[type="submit"][name^="submit"]');
                            if (document.URL.indexOf('?') == -1 && b.length) {
                                b[0].click();
                            } else if (document.URL.indexOf('?') > 0) {
                                location = document.URL;
                            }
                            return;
                        }
                        obj.innerHTML = ok ? valtxt : org;
                        if (ok) $(obj).html(valtxt).css('background', '').css('text-decoration', '');
                        if (fun != null && fun != "") eval(fun + ".apply(null,['" + obj.id + "'])");
                    }
                });
            }
            else {
                obj.innerHTML = org;
            }
        };
        /* 编辑区失去焦点的处理函数 */
        txt.onblur = function (e) {
            if (trim(txt.options[txt.selectedIndex].value) == '' || org == trim(txt.options[txt.selectedIndex].innerHTML)) obj.innerHTML = org;
        };
    }
    if (txt == null) return;
};
/**
 * 切换状态
 */
listTable.toggle = function(obj, act, id)
{
  var val = (obj.src.match(/yes.gif/i)) ? 0 : 1;
  $.ajax({
        url: listTable.url,
        type: "post",
        dataType: "json",
        data: "act="+act+"&val=" + val + "&id=" +id,
        success:function(res,status){
              if (res.message)
              {
                alert(res.message);
              }

              if (res.error == 0)
              {
                obj.src = (res.content > 0) ? '/member/images/yes.gif' : '/member/images/no.gif';
              }
        }
   });
};
/**
 * 切换排序方式
 */
listTable.sort = function(sort_by, sort_order)
{
  var args = "act="+this.query+"&sort_by="+sort_by+"&sort_order=";

  if (this.filter.sort_by == sort_by)
  {
    args += this.filter.sort_order == "DESC" ? "ASC" : "DESC";
  }
  else
  {
    args += "DESC";
  }

  for (var i in this.filter)
  {
    if (typeof(this.filter[i]) != "function" &&
      i != "sort_order" && i != "sort_by" && !isEmpty(this.filter[i]))
    {
      args += "&" + i + "=" + this.filter[i];
    }
  }

  this.filter['page_size'] = this.getPageSize();

  Ajax.call(this.url, args, this.listCallback, "POST", "JSON");
};
/**
 * 翻页
 */
listTable.gotoPage = function(page)
{
  if (page != null) this.filter['page'] = page;

  if (this.filter['page'] > this.pageCount) this.filter['page'] = 1;

  this.filter['page_size'] = this.getPageSize();

  this.loadList();
};
/**
 * 载入列表
 */
listTable.loadList = function()
{
  var args = "act="+this.query+"" + this.compileFilter();

  Ajax.call(this.url, args, this.listCallback, "POST", "JSON");
};
/**
 * 删除列表中的一个记录
 */
listTable.remove = function(id, cfm, opt)
{
  if (opt == null)
  {
    opt = "remove";
  }

  if (confirm(cfm))
  {
    var args = "act=" + opt + "&id=" + id + this.compileFilter();

    Ajax.call(this.url, args, this.listCallback, "GET", "JSON");
  }
}
;
listTable.gotoPageFirst = function()
{
  if (this.filter.page > 1)
  {
    listTable.gotoPage(1);
  }
};

listTable.gotoPagePrev = function()
{
  if (this.filter.page > 1)
  {
    listTable.gotoPage(this.filter.page - 1);
  }
};

listTable.gotoPageNext = function()
{
  if (this.filter.page < listTable.pageCount)
  {
    listTable.gotoPage(parseInt(this.filter.page) + 1);
  }
};

listTable.gotoPageLast = function()
{
  if (this.filter.page < listTable.pageCount)
  {
    listTable.gotoPage(listTable.pageCount);
  }
};

listTable.changePageSize = function(e)
{
    var evt = (typeof e == "undefined") ? window.event : e;
    if (evt.keyCode == 13)
    {
        listTable.gotoPage();
        return false;
    };
};

listTable.listCallback = function(result, txt)
{
  if (result.error > 0)
  {
    alert(result.message);
  }
  else
  {
    try
    {
      document.getElementById('listDiv').innerHTML = result.content;

      if (typeof result.filter == "object")
      {
        listTable.filter = result.filter;
      }

      listTable.pageCount = result.page_count;
    }
    catch (e)
    {
      alert(e.message);
    }
  }
};

listTable.selectAll = function(obj, chk)
{
  if (chk == null)
  {
    chk = 'checkboxes';
  }

  var elems = obj.form.getElementsByTagName("INPUT");

  for (var i=0; i < elems.length; i++)
  {
    if (elems[i].name == chk || elems[i].name == chk + "[]")
    {
      elems[i].checked = obj.checked;
    }
  }
};

listTable.compileFilter = function()
{
  var args = '';
  for (var i in this.filter)
  {
    if (typeof(this.filter[i]) != "function" && typeof(this.filter[i]) != "undefined")
    {
      args += "&" + i + "=" + encodeURIComponent(this.filter[i]);
    }
  }

  return args;
};

listTable.getPageSize = function()
{
  var ps = 15;

  pageSize = document.getElementById("pageSize");

  if (pageSize)
  {
    ps = isInt(pageSize.value) ? pageSize.value : 15;
    document.cookie = "ECSCP[page_size]=" + ps + ";";
  }
};

listTable.addRow = function(checkFunc)
{
  cleanWhitespace(document.getElementById("listDiv"));
  var table = document.getElementById("listDiv").childNodes[0];
  var firstRow = table.rows[0];
  var newRow = table.insertRow(-1);
  newRow.align = "center";
  var items = new Object();
  for(var i=0; i < firstRow.cells.length;i++) {
    var cel = firstRow.cells[i];
    var celName = cel.getAttribute("name");
    var newCel = newRow.insertCell(-1);
    if (!cel.getAttribute("ReadOnly") && cel.getAttribute("Type")=="TextBox")
    {
      items[celName] = document.createElement("input");
      items[celName].type  = "text";
      items[celName].style.width = "50px";
      items[celName].onkeypress = function(e)
      {
          var evt = (typeof e == "undefined") ? window.event : e;
          var obj = document.all ? evt.srcElement : evt.target;

        if (evt.keyCode == 13)
        {
          listTable.saveFunc();
        }
      }
      newCel.appendChild(items[celName]);
    }
    if (cel.getAttribute("Type") == "Button")
    {
      var saveBtn   = document.createElement("input");
      saveBtn.type  = "image";
      saveBtn.src = "./images/icon_add.gif";
      saveBtn.value = save;
      newCel.appendChild(saveBtn);
      this.saveFunc = function()
      {
        if (checkFunc)
        {
          if (!checkFunc(items))
          {
            return false;
          }
        }
        var str = "act=add";
        for(var key in items)
        {
          if (typeof(items[key]) != "function")
          {
            str += "&" + key + "=" + items[key].value;
          }
        }
        res = Ajax.call(listTable.url, str, null, "POST", "JSON", false);
        if (res.error)
        {
          alert(res.message);
          table.deleteRow(table.rows.length-1);
          items = null;
        }
        else
        {
          document.getElementById("listDiv").innerHTML = res.content;
          if (document.getElementById("listDiv").childNodes[0].rows.length < 6)
          {
             listTable.addRow(checkFunc);
          }
          items = null;
        }
      }
      saveBtn.onclick = this.saveFunc;
    }
  }

};
function htmlEncode (text) {
    return text.replace(/&/g, '&amp;').replace(/"/g, '&quot;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
}
function trim (text) {
    if (typeof (text) == "string") {
        return text.replace(/^\s*|\s*$/g, "");
    }
    else {
        return text;
    }
}
function isEmpty (val) {
    switch (typeof (val)) {
        case 'string':
            return trim(val).length == 0 ? true : false;
            break;
        case 'number':
            return val == 0;
            break;
        case 'object':
            return val == null;
            break;
        case 'array':
            return val.length == 0;
            break;
        default:
            return true;
    }
}
function isNumber (val) {
    var reg = /^[\d|\.|,]+$/;
    return reg.test(val);
}
function isInt (val) {
    if (val == "") {
        return false;
    }
    var reg = /\D+/;
    return !reg.test(val);
}
function isEmail (email) {
    var reg1 = /([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)/;
    return reg1.test(email);
}
function isTel (tel) {
    var reg = /^[\d|\-|\s|\_]+$/; //只允许使用数字-空格等

    return reg.test(tel);
}
function isTime (val) {
    var reg = /^\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}$/;

    return reg.test(val);
}
function getPosition(o) {
    var t = o.offsetTop;
    var l = o.offsetLeft;
    while (o = o.offsetParent) {
        t += o.offsetTop;
        l += o.offsetLeft;
    }
    var pos = { top: t, left: l };
    return pos;
}
function cleanWhitespace(element) {
    var element = element;
    for (var i = 0; i < element.childNodes.length; i++) {
        var node = element.childNodes[i];
        if (node.nodeType == 3 && !/\S/.test(node.nodeValue))
            element.removeChild(node);
    }
}
