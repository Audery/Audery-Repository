jQuery(function ($) {
    //验证提示规则
    $(document).ready(function () {
        $(".form_table_input tr").each(function () {
            $(this).find(".tip").attr("id", $(this).find(".FormBase").attr("id") + "Tip");
        });
        TableTrHover();
    });
    //全选和反选
    $("#chkAll").click(function () {
        $(".datatable tr input[type=checkbox]").each(function () {
            if ($(this).attr("class").indexOf("unchkAll") == -1) {
                $(this).attr("checked", $("#chkAll").attr("checked"));
                if ($(this).attr("checked")) {
                    $(this).parent().parent().parent().css("background-color", "#F2C493");
                    tempcolor = "#F2C493";
                }
                else {
                    $(this).parent().parent().parent().css("background-color", "#FFFFFF");
                    tempcolor = "#FFFFFF";
                }
            }            
        });
    });
    //删除提示
    $(".del").click(function () {
        return confirm("你确认删除吗？");
    });
    $(".datatable tr input[type=checkbox]").click(function () {
        if ($(this).attr("checked")) {
            $(this).parent().parent().parent().css("background-color", "#F2C493");
            tempcolor = "#F2C493";
        }
        else {
            $(this).parent().parent().parent().css("background-color", "#FFFFFF");
            tempcolor = "#FFFFFF";
        }
    });
});

//表格变换
//var tempcolor;
function TableTrHover() {
    $(".datatable tr").hover(function () {
        tempcolor = $(this).css("background-color");
        $(this).css("background-color", "#F2C493");
    },
             function () {
                 $(this).css("background-color", tempcolor);
             });

             $(".datatable tr td").click(function () {
                 $(this).parent().find("input:checkbox").each(function () {
                     if ($(this).attr("class").indexOf("unchkAll") == -1) {
                         $(this).click();
                     }
                 });
                 if ($(this).parent().find("input:checkbox").attr("checked")) {
                     $(this).parent().css("background-color", "#F2C493");
                     tempcolor = "#F2C493";
                 }
                 else {
                     $(this).parent().css("background-color", "#FFFFFF");
                     tempcolor = "#FFFFFF";
                 }

             });
    //删除行选择事件
    $(".unclick").each(function () {
        $(this).unbind("click");
    });
    SetCheckedColor();
}

//设置选中行的颜色
function SetCheckedColor() {
    $(".datatable tr").find("input:checkbox").each(function () {
        if ($(this).attr("checked")) {
            $(this).parent().parent().parent().css("background-color", "#F2C493");
        }
        else {
            $(this).parent().parent().parent().css("background-color", "#FFFFFF");
        }
    });
}