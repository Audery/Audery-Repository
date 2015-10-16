// JavaScript Document
  $(document).ready(function(){

    $("#Classes").css({display:""});//设置页面加载显示的层

    $("#ClassNavage > ul > li:eq(0)").hover(function(){
	  $("#Classes").css({display:""});
	  $("#Wenzhang").css({display:"none"});
	  $(this).addClass("MyZone_Title_Class_Head_Navgle_Hover");
	  $("#ClassNavage > ul > li:eq(1)").removeClass("MyZone_Title_Class_Head_Navgle_Hover");
	});
    $("#ClassNavage > ul > li:eq(1)").hover(function(){
	  $("#Classes").css({display:"none"});
	  $("#Wenzhang").css({display:""});
	  	  $(this).addClass("MyZone_Title_Class_Head_Navgle_Hover");
	  $("#ClassNavage > ul > li:eq(0)").removeClass("MyZone_Title_Class_Head_Navgle_Hover");
	});

    $("#ClassNavage > ul > li:eq(0)").hover(function(){
	  $("#Classes").fadeIn(600);
	  $("#Wenzhang").fadeOut(600);
	  $(this).addClass("MyZone_Title_Class_Head_Navgle_Hover");
	  $("#ClassNavage > ul > li:eq(1)").removeClass("MyZone_Title_Class_Head_Navgle_Hover");
	});
    $("#ClassNavage > ul > li:eq(1)").hover(function(){
	  $("#Classes").fadeOut(600);
	  $("#Wenzhang").fadeIn(600);
	  	  $(this).addClass("MyZone_Title_Class_Head_Navgle_Hover");
	  $("#ClassNavage > ul > li:eq(0)").removeClass("MyZone_Title_Class_Head_Navgle_Hover");
	});
	
	//=========================================我的收藏
	//鼠标点击事件
	//班级汇
	$("#MyClass").click(function(){
	  $("#Box_MyClass").slideToggle(200);
	});
	
	//俱乐部
	$("#MyClub").click(function(){
	  $("#Box_MyClub").slideToggle(200);
	});
	
	//考题问答
	$("#MyQuest").click(function(){
	  $("#Box_MyQuest").slideToggle(200);
	});
	
	//计算机频到
	$("#MyComputer").click(function(){
	  $("#Box_MyComputer").slideToggle(200);
	});
	
	//技巧大全
	$("#MyJiqiao").click(function(){
	  $("#Box_MyJiqiao").slideToggle(200);
	});
	
	//=========================================个人中心
    $("#TitleClass").css({display:"block"});//设置页面加载显示的层	
	$("#TitleClub").css({display:"block"});
	
	//班级汇
    $("#Head_Text > ul > li:eq(0)").hover(function(){
//	  $("#TitleClass").show(1000);
//	  $("#TitleArticle").hide(1000);
	  $(this).addClass("MyZone_Class_Head_Text_Hover");
	  $("#Head_Text > ul > li:eq(1)").removeClass("MyZone_Class_Head_Text_Hover");
	});
    $("#Head_Text > ul > li:eq(1)").hover(function(){
//	  $("#TitleClass").hide(1000);
//	  $("#TitleArticle").show(1000);
	  	  $(this).addClass("MyZone_Class_Head_Text_Hover");
	  $("#Head_Text > ul > li:eq(0)").removeClass("MyZone_Class_Head_Text_Hover");
	});
	
	//俱乐部
    $("#Head_Club > ul > li:eq(0)").hover(function(){
//	  $("#TitleClub").show(1000);
//	  $("#ArticleClub").hide(1000);
	  $(this).addClass("MyZone_Class_Head_Text_Hover");
	  $("#Head_Club > ul > li:eq(1)").removeClass("MyZone_Class_Head_Text_Hover");
	});
    $("#Head_Club > ul > li:eq(1)").hover(function(){
//	  $("#TitleClub").hide(1000);
//	  $("#ArticleClub").show(1000);
	  	  $(this).addClass("MyZone_Class_Head_Text_Hover");
	  $("#Head_Club > ul > li:eq(0)").removeClass("MyZone_Class_Head_Text_Hover");
	});	
	//============个人中心备份
//    $("#TitleClass").css({display:"block"});//设置页面加载显示的层	
//	$("#TitleClub").css({display:"block"});
//	
//	//班级汇
//    $("#Head_Text > ul > li:eq(0)").hover(function(){
//	  $("#TitleClass").css({display:"block"});
//	  $("#TitleArticle").css({display:"none"});
//	  $(this).addClass("MyZone_Class_Head_Text_Hover");
//	  $("#Head_Text > ul > li:eq(1)").removeClass("MyZone_Class_Head_Text_Hover");
//	});
//    $("#Head_Text > ul > li:eq(1)").hover(function(){
//	  $("#TitleClass").css({display:"none"});
//	  $("#TitleArticle").css({display:"block"});
//	  	  $(this).addClass("MyZone_Class_Head_Text_Hover");
//	  $("#Head_Text > ul > li:eq(0)").removeClass("MyZone_Class_Head_Text_Hover");
//	});
//	
//	//俱乐部
//    $("#Head_Club > ul > li:eq(0)").hover(function(){
//	  $("#TitleClub").css({display:"block"});
//	  $("#ArticleClub").css({display:"none"});
//	  $(this).addClass("MyZone_Class_Head_Text_Hover");
//	  $("#Head_Club > ul > li:eq(1)").removeClass("MyZone_Class_Head_Text_Hover");
//	});
//    $("#Head_Club > ul > li:eq(1)").hover(function(){
//	  $("#TitleClub").css({display:"none"});
//	  $("#ArticleClub").css({display:"block"});
//	  	  $(this).addClass("MyZone_Class_Head_Text_Hover");
//	  $("#Head_Club > ul > li:eq(0)").removeClass("MyZone_Class_Head_Text_Hover");
//	});		
	
  })