$(document).ready(function () {
    $(".topC0").hover(function () {
        $(".topC0").removeClass("topC1");
        $(this).addClass("topC1");
        $(".NewsTop_cnt .d_01").hide();
        $(".NewsTop_cnt .d_01").eq($(this).index()).show();
    });
    $(".topd0").hover(function () {
        $(".topd0").removeClass("topd1");
        $(this).addClass("topd1");
        $(".NewsTop_cnt .d_02").hide();
        $(".NewsTop_cnt .d_02").eq($(this).index()).show();
    });
    $(".tope0").hover(function () {
        $(".tope0").removeClass("tope1");
        $(this).addClass("tope1");
        $(".NewsTop_cnt .d_03").hide();
        $(".NewsTop_cnt .d_03").eq($(this).index()).show();
    });
});