// JavaScript Document
  $(document).ready(function(){

    $("#Classes").css({display:""});//����ҳ�������ʾ�Ĳ�

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
	
	//=========================================�ҵ��ղ�
	//������¼�
	//�༶��
	$("#MyClass").click(function(){
	  $("#Box_MyClass").slideToggle(200);
	});
	
	//���ֲ�
	$("#MyClub").click(function(){
	  $("#Box_MyClub").slideToggle(200);
	});
	
	//�����ʴ�
	$("#MyQuest").click(function(){
	  $("#Box_MyQuest").slideToggle(200);
	});
	
	//�����Ƶ��
	$("#MyComputer").click(function(){
	  $("#Box_MyComputer").slideToggle(200);
	});
	
	//���ɴ�ȫ
	$("#MyJiqiao").click(function(){
	  $("#Box_MyJiqiao").slideToggle(200);
	});
	
	//=========================================��������
    $("#TitleClass").css({display:"block"});//����ҳ�������ʾ�Ĳ�	
	$("#TitleClub").css({display:"block"});
	
	//�༶��
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
	
	//���ֲ�
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
	//============�������ı���
//    $("#TitleClass").css({display:"block"});//����ҳ�������ʾ�Ĳ�	
//	$("#TitleClub").css({display:"block"});
//	
//	//�༶��
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
//	//���ֲ�
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