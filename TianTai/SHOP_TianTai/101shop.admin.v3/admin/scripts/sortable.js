$(document).ready(function () {
    var clss0 = 'sort', clss2 = 'clickable', clss3 = 'hover', clss5 = 'sort-number', clss6 = 'sort-alpha', clss7 = 'asc', clss8 = 'desc';
    $('table.' + clss0).each(function () {
        var $table = $(this), $th = $('th', $table);
        $th.each(function (column) {
            var $header = $(this), cls = $header.attr('class');
            if (cls != null && cls.indexOf('sort') != -1) {
                $header
                    .addClass(clss2)
                    .hover(
                        function () { $header.addClass(clss3) },
                        function () {
                            $header.removeClass(clss3);
                        })
                    .click(function () {
                        var rows = $table.find('tbody > tr.' + clss0).get();
                        var rowsort = 1, rowsortway = 1;
                        if ($header.is('.' + clss6)) {
                            rowsortway = 1;
                        } else if ($header.is('.' + clss5)) {
                            rowsortway = 2;
                        }
                        if ($header.hasClass(clss8) || !$header.hasClass(clss7)) {
                            rowsort = 1; $th.removeClass(clss7).removeClass(clss8);
                            $header.addClass(clss7);
                        } else {
                            rowsort = 0; $th.removeClass(clss7).removeClass(clss8);
                            $header.addClass(clss8);
                        }
                        rows.sort(function (a, b) {
                            var keyA = $(a).children('td').eq(column).text().toUpperCase();
                            var keyB = $(b).children('td').eq(column).text().toUpperCase();
                            if (rowsortway == 1);
                            else if (rowsortway == 2) {
                                keyA = parseFloat(keyA);
                                keyB = parseFloat(keyB);
                            }
                            if (keyA < keyB) return (rowsort ? 1 : -1);
                            if (keyA > keyB) return (rowsort ? -1 : 1);
                            return 0;
                        });
                        $.each(rows, function (index, row) {
                            $table.children('tbody').prepend(row);
                        });
                    });
            }
        });
    });
});
