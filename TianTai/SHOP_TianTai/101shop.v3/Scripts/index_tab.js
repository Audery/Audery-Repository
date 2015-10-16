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

    $(".ProductHotList li,.ProductHotList dd").hover(function () {
        $(this).parent().find(".gght-list-2").hide();
        $(this).find(".gght-list-2").show();
    });
    $(".cate1 .grid-c1-cont-left ul li").hover(function () {
        $(".cate1 .grid-c1-cont-left ul li a").removeClass("cate1-a-cur");
        $(this).find("a").addClass("cate1-a-cur");
        $(".cate1 .grid-c1-cont-center .s_1").hide();
        $(".cate1 .grid-c1-cont-center .s_1").eq($(this).index()).show();
        $(".cate1 .pImage_index").each(function (index, element) {
            if ($(this).attr("data-original") != $(this).attr("src"))
                $(this).attr("src", $(this).attr("data-original"));
        });
    });

    $(".cate2 .grid-c1-cont-left ul li").hover(function () {
        $(".cate2 .grid-c1-cont-left ul li a").removeClass("cate2-a-cur");
        $(this).find("a").addClass("cate2-a-cur");
        $(".cate2 .grid-c1-cont-center .s_1").hide();
        $(".cate2 .grid-c1-cont-center .s_1").eq($(this).index()).show();
        $(".cate2 .pImage_index").each(function (index, element) {
            if ($(this).attr("data-original") != $(this).attr("src"))
                $(this).attr("src", $(this).attr("data-original"));
        });
    });

    $(".cate4 .grid-c1-cont-left ul li").hover(function () {
        $(".cate4 .grid-c1-cont-left ul li a").removeClass("cate4-a-cur");
        $(this).find("a").addClass("cate4-a-cur");
        $(".cate4 .grid-c1-cont-center .s_1").hide();
        $(".cate4 .grid-c1-cont-center .s_1").eq($(this).index()).show();
        $(".cate4 .pImage_index").each(function (index, element) {
            if ($(this).attr("data-original") != $(this).attr("src"))
                $(this).attr("src", $(this).attr("data-original"));
        });
    });

    $(".cate5 .grid-c1-cont-left ul li").hover(function () {
        $(".cate5 .grid-c1-cont-left ul li a").removeClass("cate5-a-cur");
        $(this).find("a").addClass("cate5-a-cur");
        $(".cate5 .grid-c1-cont-center .s_1").hide();
        $(".cate5 .grid-c1-cont-center .s_1").eq($(this).index()).show();
        $(".cate5 .pImage_index").each(function (index, element) {
            if ($(this).attr("data-original") != $(this).attr("src"))
                $(this).attr("src", $(this).attr("data-original"));
        });
    });

    $(".cate6 .grid-c1-cont-left ul li").hover(function () {
        $(".cate6 .grid-c1-cont-left ul li a").removeClass("cate6-a-cur");
        $(this).find("a").addClass("cate6-a-cur");
        $(".cate6 .grid-c1-cont-center .s_1").hide();
        $(".cate6 .grid-c1-cont-center .s_1").eq($(this).index()).show();
        $(".cate6 .pImage_index").each(function (index, element) {
            if ($(this).attr("data-original") != $(this).attr("src"))
                $(this).attr("src", $(this).attr("data-original"));
        });
    });
    $(".pImage_index").lazyload({ effect: "fadeIn" });    
});