jQuery(function () {
    var $ = jQuery, url;
    function split(val) { return val.split(/[,，]\s*/) };
    function autosubmitevent(event) {
        // autosubmit
        if (event.keyCode == 13) {
            var a = $(this).attr('autosubmit'), btn;
            if (a != null && (btn = $('input[id*=' + a + ']')).length) {
                setTimeout(function () { try { btn[0].click(); } catch (e) { btn.trigger('click'); } }, 50); return false;
            }
        }
    }
    if (typeof (autocomplete_url) != "undefined") { url = autocomplete_url; } else {
        url = location.href.lastIndexOf("?") == -1 ? location.href.substring((location.href.lastIndexOf("/")) + 1) : location.href.substring((location.href.lastIndexOf("/")) + 1, location.href.lastIndexOf("?"));
        url += "?is_ajax=1&is_autocomplete=1&act=autocomplete";
    }
    $("input.autocomplete").autocomplete({
        source: function (request, response) {
            var id = this.element.attr('id'), name = this.element.attr('autoinput'), val = request.term;
            if (id == null || $.trim(id.toString()) == '' || name == null || $.trim(name.toString()) == '' || val == null || $.trim(val.toString()) == '') return false;
            id = $.trim(id.toString()); name = $.trim(name.toString()); val = $.trim(val.toString());
            var names = name.split(','), vals = (val.indexOf(',') != -1 ? val.split(',') : (val.indexOf('，') != -1 ? val.split('，') : (val.indexOf(' ') != -1 ? val.split(' ') : [val])));
            var param = { id: id, name: name, val: val };
            var ind = Math.min(names.length - 1, vals.length - 1);
            param.name = names[ind]; param.val = vals[ind];
            if (this.element.attr('param') != null) {
                var params = this.element.attr('param').toString().split(',');
                for (var i = 0; i < params.length; i++) {
                    var o = $('*[name$="' + params[i] + '"]');
                    if (o.length) param[params[i]] = o.val();
                }
            }
            $.getJSON(url, param, response);
        },
        search: function (event, ui) {
            // custom minLength
            var terms = split(this.value), term = terms.pop();
            if (term.length < 2) return false;
        },
        open: function (event, ui) {
            $('li.ui-menu-item a').each(function (i) { $(this).attr('title', $(this).text()); });
        },
        focus: function (event, ui) {
            // prevent value inserted on focus
            return false;
        },
        select: function (event, ui) {
            if (ui.item.value.indexOf('(:') > 0) {
                ui.item.value = ui.item.value.substring(0, ui.item.value.indexOf('(:'));
            }
            var terms = split(this.value);
            // remove the current input
            terms.pop();
            // add the selected item
            terms.push(ui.item.value);
            // add placeholder to get the comma-and-space at the end
            // if (this.value.indexOf(",") != -1 || this.value.indexOf("，") != -1) terms.push("");
            this.value = terms.join(",");
            return false;
        }
    }).focus(autosubmitevent).keydown(autosubmitevent);
});
