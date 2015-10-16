var t, now = new Date(), outlookbar = new outlook();
//快捷菜单
t = outlookbar.addtitle('快捷菜单', '快捷导航', 1);
outlookbar.additem('买家库', t, 'member/BuyerLib.aspx');
//outlookbar.additem('商家商品管理', t, 'product/product_list.aspx');
//outlookbar.additem('产品基础数据管理', t, 'product/product_DrugsBaseList.aspx');
//outlookbar.additem('店铺信息管理', t, 'shop/shop_list.aspx');
//outlookbar.additem('添加会员', t, 'member/member_edit.aspx');
//outlookbar.additem('产品基础数据添加', t, 'product/product_DrugsBaseEdit.aspx');
//outlookbar.additem('商品基础数据添加', t, 'product/product_searchlist.aspx');
outlookbar.additem('网站参数配置', t, 'systeminfo/site_setting.aspx');

//产品管理
//t = outlookbar.addtitle('产品管理', '产品管理', 1);
//outlookbar.additem('剂型管理', t, 'product/FormulationsList.aspx');
//outlookbar.additem('药品类别管理', t, 'product/product_CategoryList.aspx');
//outlookbar.additems('产品基础数据管理', t, 'Goods/Default.aspx', '_blank');
//outlookbar.additem('产品基础数据管理', t, 'product/product_DrugsBaseList.aspx');
//outlookbar.additem('产品基础数据添加', t, 'product/product_DrugsBaseEdit.aspx');

//货源管理
//t = outlookbar.addtitle('货源管理', '产品管理', 1);
//outlookbar.additem('产品货源数据添加', t, 'import/ImportCRM.aspx?ClearSelected=1');
//outlookbar.additem('产品货源数据查询', t, 'import/SelectS.aspx');
//outlookbar.additem('产品货源价格管理', t, 'import/SelectS.aspx');
//outlookbar.additem('价格变动提醒设置', t, 'import/Product_Price_Alert.aspx');
//outlookbar.additem('价格变动提醒看板', t, 'import/Product_Price_AlertList.aspx');

//商品管理
t = outlookbar.addtitle('商品管理', '商品管理', 1);
outlookbar.additem('商家商品管理', t, 'product_manager/product_list.aspx');
outlookbar.additem('商品促销管理', t, 'cuxiao/cuxiao.aspx');
outlookbar.additem('商品锁库分析', t, 'product_manager/product_list_Lock.aspx');

t = outlookbar.addtitle('商品资质管理', '商品管理', 1);
outlookbar.additem('药检报告管理', t, 'product_manager/product_report.aspx');
outlookbar.additem('商品资质管理', t, 'product_manager/product_qualification.aspx');
//订单管理
t = outlookbar.addtitle('订单管理', '订单管理', 1);
outlookbar.additem('客服订单管理', t, 'order/order_list.aspx');
//outlookbar.additem('往来帐查询', t, 'order/AccountsList.aspx');
//库存管理 暂时屏蔽此功能项项
/*t = outlookbar.addtitle('库存管理', '订单管理', 1);
outlookbar.additem('验货管理', t, '_inventory/ExamineGoods.aspx');
outlookbar.additem('入库申请', t, '_inventory/GodownReceipts.aspx');
outlookbar.additem('入库审核', t, '_inventory/GodownReceiptsVerify.aspx');
outlookbar.additem('出库申请', t, '_inventory/OutGodownReceipts.aspx');
outlookbar.additem('出库审核', t, '_inventory/OutGodownReceiptsVerify.aspx');
outlookbar.additem('退货申请', t, '_inventory/returnGoods.aspx');
outlookbar.additem('退货审核', t, '_inventory/returnGoodsVerify.aspx');
outlookbar.additem('库存提醒设置', t, '_inventory/repertoryAlertSetting.aspx');
outlookbar.additem('库存提醒设置（全局）', t, '_inventory/repertoryAlertSettingGlobal.aspx');
outlookbar.additem('数量警戒提醒', t, '_inventory/repertoryQuantityChecking.aspx');
outlookbar.additem('库存动销提醒', t, '_inventory/repertoryIdleChecking.aspx');
outlookbar.additem('近效期提醒', t, '_inventory/repertoryFastExpired.aspx');*/

//t = outlookbar.addtitle('购物管理', '订单管理', 1);
//outlookbar.additem('购物车管理', t, 'order/order_shop_list.aspx');
//t = outlookbar.addtitle('明细记录', '订单管理', 1);
//outlookbar.additem('资金明细',t,'product/product_funds_list.aspx');
//outlookbar.additem('商品销售明细', t, 'product/product_sale_list.aspx');
//outlookbar.additem('发货明细', t, 'product/product_back_list.aspx');
//outlookbar.additem('退货明细', t, 'product/product_Reback_list.aspx');
//outlookbar.additem('订单过户明细',t,'product/product_trans_list.aspx');



//用户管理
t = outlookbar.addtitle('管理员', '用户管理', 1);
outlookbar.additem('管理员管理', t, 'member/admin_list.aspx');
outlookbar.additem('角色管理', t, 'member/role_list.aspx');
//outlookbar.additem('内销人员负责区域查询', t, 'member/Trader_List.aspx');
//outlookbar.additem('内销人员负责区域管理', t, 'member/TraderManage.aspx')
//outlookbar.additem('外销人员负责区域查询', t, 'member/OutSellPerson_List.aspx');
//outlookbar.additem('外销人员负责区域管理', t, 'member/OutSellPersonManage.aspx');
//outlookbar.additem('采购员移交管理', t, 'member/admin_supplier_setmember.aspx?id=34');
//outlookbar.additem('交易员移交管理', t, 'member/admin_order_setmember.aspx?id=33');
//outlookbar.additem('线下推广员移交管理', t, 'member/admin_popularize_setmember.aspx?id=37');

t = outlookbar.addtitle('会员管理', '用户管理', 1);
//outlookbar.additem('企业库1', t, 'member/MemberLib.aspx');
//outlookbar.additem('企业库', t, '_member/MemberLib.aspx');
//outlookbar.additem('企业建档', t, '_member/MemberLib.aspx?action=jian');
//outlookbar.additem('供应商1', t, 'member/ProductUserList.aspx');
//outlookbar.additem('供应商', t, '_member/ProductUserList.aspx');
outlookbar.additem('买家库', t, 'member/BuyerLib.aspx');
outlookbar.additem('客户交易意向', t, 'member/TradingIntention.aspx');
outlookbar.additem('买家行为', t, '_member/MemberAction.aspx');

t = outlookbar.addtitle('积分管理', '用户管理', 1);
outlookbar.additem('积分规则', t, 'member/MemberIntegralTemplate.aspx');
outlookbar.additem('积分礼品', t, 'member/MemberIntegralGift.aspx');
outlookbar.additem('积分排行', t, 'member/MemberIntegralRank.aspx');
outlookbar.additem('兑换礼品', t, 'member/MemberIntegralGiftExchange.aspx');


//资讯频道
t = outlookbar.addtitle('资讯频道', '资讯频道', 1);
outlookbar.additem('频道管理', t, 'systeminfo/articlechannel_list.aspx');
outlookbar.additem('资讯管理', t, 'systeminfo/article_list.aspx');



//网站配置
t = outlookbar.addtitle('网站配置', '系统设置', 1);
outlookbar.additem('网站参数配置', t, 'systeminfo/site_setting.aspx');

t = outlookbar.addtitle('系统日志', '综合管理', 1);
outlookbar.additem('系统日志', t, 'systeminfo/log.aspx');


t = outlookbar.addtitle('信息管理', '综合管理', 1);
outlookbar.additem('Sms信息管理', t, 'accessories/sms_list.aspx');

t = outlookbar.addtitle('友情链接', '综合管理', 1);
outlookbar.additem('友情链接管理', t, 'accessories/hailhellowlink_list.aspx');
outlookbar.additem('添加友情链接', t, 'accessories/hailhellowlink_edit.aspx');

t = outlookbar.addtitle('其它信息', '综合管理', 1);

outlookbar.additem('热门搜索设置', t, 'accessories/topsearchesseting_list.aspx');
t = outlookbar.addtitle('广告配置', '广告管理', 1);
outlookbar.additem('创建图片广告位', t, ImageAdvertisingPositionUrl);
outlookbar.additem('图片广告管理', t, ImageAdvertisingManage);
outlookbar.additem('商品广告管理', t, 'Advertising/Products.aspx?domain=99dcyy.com');
outlookbar.additem('添加广告商品', t, 'Advertising/AddProduct.aspx?domain=99dcyy.com');


var preClassName = "";
function list_sub_detail(Id, item) {
    if (preClassName != "") {
        getObject(preClassName).className = "left_back"
    }
    if (getObject(Id).className == "left_back") {
        getObject(Id).className = "left_back_onclick";
        outlookbar.getbyitem(item);
        preClassName = Id
    }
}
function getObject(objectId) {
    if (document.getElementById && document.getElementById(objectId)) {
        return document.getElementById(objectId)
    }
    else if (document.all && document.all(objectId)) {
        return document.all(objectId)
    }
    else if (document.layers && document.layers[objectId]) {
        return document.layers[objectId]
    }
    else {
        return false
    }
}
function outlook() {
    this.titlelist = new Array();
    this.itemlist = new Array();
    this.addtitle = addtitle;
    this.addtitles = addtitles;
    this.additem = additem;
    this.additems = additems;
    this.getbytitle = getbytitle;
    this.getbyitem = getbyitem;
    this.getdefaultnav = getdefaultnav;
    this.getdefaulttarget = 'manFrame';
}
function theitem(intitle, insort, inkey, inisdefault, intarget) {
    this.sortname = insort;
    this.key = inkey;
    this.title = intitle;
    this.isdefault = inisdefault;
    this.target = intarget;
}
function addtitle(intitle, sortname, inisdefault) {
    return addtitles(intitle, sortname, inisdefault, 'manFrame');
}
function addtitles(intitle, sortname, inisdefault, intarget) {
    if (("," + BigMenu + ",").indexOf("," + sortname + ",") != -1 || PowerType == "all") {
        var parentid = outlookbar.itemlist.length;
        outlookbar.itemlist[parentid] = new Array();
        //if ((SmallMenu + ",").indexOf(intitle + ",") != -1 || PowerType == "all")
        outlookbar.titlelist[parentid] = new theitem(intitle, sortname, 0, inisdefault, intarget);
        return (outlookbar.titlelist.length - 1);
    }
}
function additem(intitle, parentid, inkey) {
    return additems(intitle, parentid, inkey, 'manFrame');
}
function additems(intitle, parentid, inkey, intarget) {
    if (("," + SmallMenu + ",").indexOf("," + intitle + ",") != -1 || PowerType == "all") {
        if (parentid >= 0 && parentid < outlookbar.titlelist.length) {
            insort = "item_" + parentid;
            outlookbar.itemlist[parentid][outlookbar.itemlist[parentid].length] = new theitem(intitle, insort, inkey, 0, intarget);
            return (outlookbar.itemlist[parentid].length - 1);
        }
    }
}
function getdefaultnav(sortname) {
    var output = "";
    for (i = 0; i < outlookbar.titlelist.length; i++) {
        if (outlookbar.titlelist[i].isdefault == 1 && outlookbar.titlelist[i].sortname == sortname && outlookbar.itemlist[i].length > 0) {
            output += "<div class=list_tilte id=sub_sort_" + i + " onclick=\"hideorshow('sub_detail_" + i + "')\">";
            output += "<span>" + outlookbar.titlelist[i].title + "</span>";
            output += "</div>";
            output += "<div class=list_detail id=sub_detail_" + i + "><ul>";
            for (j = 0; j < outlookbar.itemlist[i].length; j++) {
                if (outlookbar.itemlist[i][j].target == '_blank') {
                    output += "<li id=" + outlookbar.itemlist[i][j].sortname + j + "><a href=\"" + outlookbar.itemlist[i][j].key + "\" target=\"_blank\">" + outlookbar.itemlist[i][j].title + "</a></li>"
                } else {
                    output += "<li id=" + outlookbar.itemlist[i][j].sortname + j + " onclick=\"changeframe('" + outlookbar.itemlist[i][j].title + "', '" + outlookbar.titlelist[i].title + "', '" + outlookbar.itemlist[i][j].key + "')\"><a href=#>" + outlookbar.itemlist[i][j].title + "</a></li>"
                }
            }
            output += "</ul></div>";
        }
    }
    getObject('right_main_nav').innerHTML = output;
}
function getbytitle(sortname) {
    if (outlookbar.titlelist.length > 0) {
        var output = "<ul>";
        for (i = 0; i < outlookbar.titlelist.length; i++) {
            if (outlookbar.titlelist[i].sortname == sortname && outlookbar.itemlist[i].length > 0) {
                output += "<li id=left_nav_" + i + " onclick=\"list_sub_detail(id, '" + outlookbar.titlelist[i].title + "')\" class=left_back>" + outlookbar.titlelist[i].title + "</li>"
            }
        }
        output += "</ul>";
    }
    getObject('left_main_nav').innerHTML = output;
}
function getbyitem(item) {
    var output = "";
    for (i = 0; i < outlookbar.titlelist.length; i++) {
        if (outlookbar.titlelist[i].title == item) {
            output = "<div class=list_tilte id=sub_sort_" + i + " onclick=\"hideorshow('sub_detail_" + i + "')\">";
            output += "<span>" + outlookbar.titlelist[i].title + "</span>";
            output += "</div>";
            output += "<div class=list_detail id=sub_detail_" + i + " style='display:block;'><ul>";
            for (j = 0; j < outlookbar.itemlist[i].length; j++) {
                output += "<li id=" + outlookbar.itemlist[i][j].sortname + "_" + j + " onclick=\"changeframe('" + outlookbar.itemlist[i][j].title + "', '" + outlookbar.titlelist[i].title + "', '" + outlookbar.itemlist[i][j].key + "')\"><a href=#>" + outlookbar.itemlist[i][j].title + "</a></li>"
            }
            output += "</ul></div>"
            //alert(output);
        }
    }
    getObject('right_main_nav').innerHTML = output;
}
function changeframe(item, sortname, src) {
    return changeframes(item, sortname, src, 'manFrame');
}
function changeframes(item, sortname, src, target) {
    if (item != "" && sortname != "") {
        window.top.frames['mainFrame'].getObject('show_text').innerHTML = sortname + "&nbsp;&nbsp;<img src=images/direct_blue.gif broder=0 />  " + item;
    }
    if (src != "") {
        window.top.frames[target].location = src;
    }
}
function hideorshow(divid) {
    subsortid = "sub_sort_" + divid.substring(11);
    if (getObject(divid).style.display == "none") {
        getObject(divid).style.display = "block";
        getObject(subsortid).className = "list_tilte";
    }
    else {
        getObject(divid).style.display = "none";
        getObject(subsortid).className = "list_tilte_onclick";
    }
}
function initinav(sortname) {
    outlookbar.getdefaultnav(sortname);
    outlookbar.getbytitle(sortname);
    document.getElementById("right_main_nav").style.height = document.documentElement.clientHeight - 68;
}
window.onload = function () {
    var _bms = "快捷导航,用户管理,订单管理,商品管理,资讯频道,系统设置,综合管理,统计报表";
    if (typeof (BigMenu) != "undefined" && BigMenu != "") { if (PowerType == "all" || ("," + BigMenu + ",").indexOf(',快捷导航,') != -1) { initinav('快捷导航'); } else { var bm = BigMenu.split(","), bms = _bms.split(","); for (var i = 0; i < bms.length; i++) { for (var j = 0; j < bm.length; j++) { if (bms[i] == bm[j]) { initinav(bm[j]); } } } initinav(window.top.frames['mainFrame'].getObject('nav').getElementsByTagName('li').item(0).innerHTML); }; }
};
