<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cuxiao.aspx.cs" Inherits="_101shop.admin.v3.admin.cuxiao.Cuxiao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="/scripts/My97DatePicker/WdatePicker.js"></script>
    <link rel="stylesheet" href="/admin/style/admin.css" type="text/css" />
    <link rel="stylesheet" href="/admin/style/admin2.css" type="text/css" />
    <script src="/admin/scripts/global.js" type="text/javascript"></script>
    <link rel="stylesheet" href="../style/validator.css" type="text/css" />
    <link rel="stylesheet" href="../style/toolbar.css" type="text/css" />
    <%--  <link rel="stylesheet" type="text/css" href="/admin/scripts/jquery-easyui-1.3.1/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="/admin/scripts/jquery-easyui-1.3.1/themes/icon.css" />
<%--    <link rel="stylesheet" type="text/css" href="/admin/scripts/jquery-easyui-1.3.1/demo.css" />--%>
    <%--    <script type="text/javascript" src="/admin/scripts/jquery-easyui-1.3.1/jquery.min.js"></script>--%>
    <%--    <script type="text/javascript" src="/admin/scripts/jquery-easyui-1.3.1/jquery.easyui.min.js"></script>--%>
    <style type="text/css">
        td
        {
            font-size: 12px;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            padding: 1px 1px 4px 4px;
            height: 22px;
        }
        div
        {
            font-size: 12px;
        }
        .textOverFlow
        {
            display: block; /*内联对象需加*/
            width: 150px;
            word-break: keep-all; /* 不换行 */
            white-space: nowrap; /* 不换行 */
            overflow: hidden; /* 内容超出宽度时隐藏超出部分的内容 */
            text-overflow: ellipsis; /* 当对象内文本溢出时显示省略标记(...) ；需与overflow:hidden;一起使用。*/
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" action="/admin/cuxiao/cuxiao.aspx">
    <!--内容搜索-->
    <div class="page_toolbar" id="pagetoolbar">
        <div class="page_title">
        </div>
        <div class="page_info">
            商品名称：<input name="shopname" value="<%=Request["shopname"]%>" style="width: 120px;" />
            生产厂家：<input name="changjia" value="<%=Request["changjia"]%>" style="width: 120px;" />
            批准文号：<input name="pihao" value="<%=Request["pihao"]%>" style="width: 120px;" />
            促销：<select name="cuxiao" style="width: 50px">
                <option value="0">全部</option>
                <option value="1" <%if (Request["cuxiao"] == "1"){%> selected<%} %>>有</option>
                <option value="2" <%if (Request["cuxiao"] == "2"){%> selected<%} %>>无</option>
            </select>
            <br />
            价格：<select name="Price" style="width: 50px">
                <option value="0">全部</option>
                <option value="1" <%if(Request["Price"]=="1"){%> selected<%} %>>批发价</option>
                <option value="2" <%if(Request["Price"]=="2"){%> selected<%} %>>零售价</option>
                <option value="3" <%if(Request["Price"]=="3"){%> selected<%} %>>批发价&零售价</option>
                <option value="5" <%if(Request["Price"]=="5"){%> selected<%} %>>零售价为零</option>
                <option value="6" <%if(Request["Price"]=="6"){%> selected<%} %>>批发价为零</option>
                <option value="4" <%if(Request["Price"]=="4"){%> selected<%} %>>都为零</option>
            </select>
            销售方式：<select name="sellType" style="width: 50px">
                <option value="0">全部</option>
                <option value="1" <%if(Request["sellType"]=="1"){%> selected<%} %>>最小包装</option>
                <option value="2" <%if(Request["sellType"]=="2"){%> selected<%} %>>中包装</option>
                <option value="3" <%if(Request["sellType"]=="3"){%> selected<%} %>>整件</option>
            </select>
            库存：<select name="bStock" style="width: 50px">
                <option value="0">全部</option>
                <option value="1" <%if(Request["bStock"]=="1"){%> selected<%} %>>有</option>
                <option value="2" <%if(Request["bStock"]=="2"){%> selected<%} %>>无</option>
            </select>
            状态：<select name="bShelves" style="width: 50px">
                <option value="0">全部</option>
                <option value="1" <%if(Request["bShelves"]=="1"){%> selected<%} %>>上架</option>
                <option value="2" <%if(Request["bShelves"]=="2"){%> selected<%} %>>下架</option>
                <option value="3" <%if(Request["bShelves"]=="3"){%> selected<%} %>>待上架</option>
                <option value="4" <%if(Request["bShelves"]=="4"){%> selected<%} %>>暂不上架</option>
            </select>
            包装盒：<select name="bGoodsImage">
                <option value="0">全部</option>
                <option value="1" <%if(Request["bGoodsImage"]=="1"){%> selected<%} %>>有</option>
                <option value="2" <%if(Request["bGoodsImage"]=="2"){%> selected<%} %>>无</option>
            </select>
            是否控销：
            <select name="bKong">
                <option value="0">全部</option>
                <option value="1" <%if(Request["bKong"]=="1"){%> selected<%} %>>是</option>
                <option value="2" <%if(Request["bKong"]=="2"){%> selected<%} %>>否</option>
            </select>
            价格区间:
            <select name="bjgqj">
                <option value="1" <%if(Request["bjgqj"]=="1"){%> selected<%} %>>批发价</option>
                <option value="2" <%if(Request["bjgqj"]=="2"){%> selected<%} %>>零售价</option>
                <option value="3" <%if(Request["bjgqj"]=="3"){%> selected<%} %>>基药价</option>
                <option value="4" <%if(Request["bjgqj"]=="4"){%> selected<%} %>>促销价</option>
                <option value="5" <%if(Request["bjgqj"]=="5"){%> selected<%} %>>折扣价</option>
            </select>
            <input type="text" name="bjgqj_s" value="<%=Request["bjgqj_s"] %>" style="width: 50px" />至
            <input type="text" name="bjgqj_e" value="<%=Request["bjgqj_e"] %>" style="width: 50px" />
            <br />
            前台展示：<select name="bQtzs" style="width: 50px">
                <option value="0">全部</option>
                <option value="1" <%if(Request["bQtzs"]=="1"){%> selected<%} %>>是</option>
                <option value="2" <%if(Request["bQtzs"]=="2"){%> selected<%} %>>否</option>
            </select>
            库存区间:
            <input type="text" name="bStock_s" value="<%=Request["bStock_s"] %>" style="width: 50px" />至
            <input type="text" name="bStock_e" value="<%=Request["bStock_e"] %>" style="width: 50px" />
            说明书：<select name="bSms" title="有无说明书">
                <option value="0">全部</option>
                <option value="1" <%if(Request["bSms"]=="1"){%> selected<%} %>>有</option>
                <option value="2" <%if(Request["bSms"]=="2"){%> selected<%} %>>无</option>
            </select>
            药理药效：<select name="bYlyx" title="有无药理药效">
                <option value="0">全部</option>
                <option value="1" <%if(Request["bYlyx"]=="1"){%> selected<%} %>>有</option>
                <option value="2" <%if(Request["bYlyx"]=="2"){%> selected<%} %>>无</option>
            </select>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <input type="submit" value="查询" />
            &nbsp;&nbsp;
            <!--br />                
        Ｅ商名称：<input name="eshop" value="<%=Request["eshop"]%>"/>申请时间：<input name="begin"value="<%=Request["begin"]%>" />到<input name="end" value="<%=Request["end"]%>"/>审核时间：从<input name="sbegin" value="<%=Request["sbegin"]%>"/>到<input name="send" value="<%=Request["send"]%>"/-->
            <%if (SOSOshop.BLL.AdministrorManager.Get().AdminName == "admin")
              {%>
            <%-- <input type="submit" value="导出" name="input" />--%><%} %></div>
        <!--内容列表-->
        <div>
            <table class="datatable" cellspacing="0" rules="all" border="1" id="tablist" style="border-collapse: collapse;
                width: 100%">              
                <% =show_list()%>
            </table>
        </div>
        <div class="quotes" style="float: right;">
            <div>
                <%=page()%>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
<script language="javascript" type="text/javascript" src="/admin/scripts/jquery-1.8.3.min.js"></script>
<script language="javascript" type="text/javascript" src="/admin/scripts/layer/layer.min.js"></script>
<script language="javascript" type="text/javascript" src="/admin/scripts/kalendae.css"></script>
<script language="javascript" type="text/javascript" src="/admin/scripts/kalendae.standalone.min.js"></script>
<script language="javascript" type="text/javascript">
    var pid = 0;
    function save() {
        $("#btnsave").attr('disabled', false);
        //折扣类型
        var zhe = $('input:radio:checked').val();
        //设置的值
        var cuvalue = $("#zhi").val();
        //开始时间
        var begint = $("#begin").val();
        //截止时间
        var endt = $("#end").val();

        var minsell = $("#minsell").val();
        var otcminsell = $("#otcMinSell").val();

        if (zhe == undefined) {
            layer.alert('亲，请选择执行折扣价或是折扣率！', 11, !1);
        }
        else if (cuvalue == "" || begint == "" || endt == "") {
            layer.alert('亲，输入的内容不能有空！', 11, !1);
        }
        else if (!$.isNumeric(cuvalue)) {
            layer.alert('亲，价格或折扣必须是数字！', 11, !1);
        }

        else {
            var maxsell = $("#maxsell").val();
            if (maxsell != "") {
                if (!$.isNumeric(maxsell)) {
                    layer.alert('亲，总促销库存必须是数字！', 11, !1);
                }
            }
            else {
                maxsell = 0;
            }
            if (otcminsell != "") {
                if (!$.isNumeric(otcminsell)) {
                    layer.alert('亲，每天可促销数必须是数字！', 11, !1);
                }
            }
            else {
                otcminsell = 0;
            }
            if (minsell != "") {
                if (!$.isNumeric(minsell)) {
                    layer.alert('亲，每会员每天可促销数必须是数字！', 11, !1);
                }
            }
            else {
                minsell = 0;
            }

            //折扣价
            var price = 0;
            //折扣率
            var discount = 0;
            //根据用户的设定来计算促销价或折扣率
            switch (parseInt(zhe)) {
                case 1:
                    price = cuvalue;
                    break;
                case 2:
                    discount = cuvalue;
                    break;
            }
            $.ajax({
                type: 'POST',
                url: '/admin/cuxiao/SetCuxiao.ashx',
                data: { pid: pid, cuprice: price, discount: discount, maxsell: maxsell, minsell: minsell, otcminsell: otcminsell, btime: begint, etime: endt },
                dataType: "json",
                loading: {
                    type: 0
                },
                success: function (msg, textStatus) {

                    if (msg["state"] == -1) {
                        layer.alert(msg["message"], 11, !1)
                    }
                    else {
                        layer.msg(msg["message"], 2, 1);
                        layer.close(pageii);
                        location.reload();
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(errorThrown + " error");
                    // 通常情况下textStatus和errorThown只有其中一个有值 
                    this; // the options for this ajax request
                }
            });

        }
    }
    var pageii;
    function putad(proid) {
        pid = proid;
        var cuprice = $("#cu" + proid).text();
        var discount = $("#dis" + proid).text();
        var btime = $("#b" + proid).text().replace("0:00:00", "");
        var etime = $("#e" + proid).text().replace("0:00:00", "");
        var max = $("#max" + proid).text();
        var otc = $("#otcmin" + proid).text();
        var min = $("#min" + proid).text();
        var str = '<div style="padding:10px; width:0 auto;"><fieldset><legend>促销设置</legend><div style="padding:5px;">';
        var str1 = '<input id="c1" name="c1" type="radio" value="1" />促销价<input id="c2" name="c1" type="radio" value="2" />折扣率</div>';
        var str2 = '<div style="padding:5px;">价格或折扣：<input type="text" id="zhi" name="zhi" value="" /></div>';
        if (parseFloat(cuprice) > 0 && (parseFloat(discount) == 0 || parseFloat(discount) == 1)) {
            str1 = '<input id="c1" name="c1" type="radio" value="1" checked />促销价<input id="c2" name="c1" type="radio" value="2" />折扣率</div>';
            str2 = '<div style="padding:5px;">价格或折扣：<input type="text" id="zhi" name="zhi" value="' + cuprice + '" /></div>';
        }
        else if (parseFloat(cuprice) == 0 && (parseFloat(discount) > 0 && parseFloat(discount) != 1.0)) {
            str1 = '<input id="c1" name="c1" type="radio" value="1" />促销价<input id="c2" name="c1" type="radio" value="2" checked />折扣率</div>';
            str2 = '<div style="padding:5px;">价格或折扣：<input type="text" id="zhi" name="zhi" value="' + parseFloat(discount) + '" /></div>';
        }
        var str3 = '<div style="padding:5px;">开始时间：　<input type="text" name="begin" id="begin"  onFocus="WdatePicker({lang:\'zh-cn\'})" value="' + btime + '" /></div>';
        var str4 = '<div style="padding:5px;">到期时间：　<input type="text" name="end" id="end" onFocus="WdatePicker({lang:\'zh-cn\'})" value="' + etime + '" /></div> ';
        var str5 = '<div style="padding:5px;">总促销库存：<input type="text" name="maxsell" id="maxsell" value="' + retnull(max) + '" /></div> ';
        var str6 = '<div style="padding:5px;">每天可促最大库存：<input type="text" name="otcMinSell" id="otcMinSell" value="' + retnull(otc) + '" /></div>';
        var str7 = '<div style="padding:5px;">每会员每天可购库存：<input type="text" name="minsell" id="minsell" value="' + retnull(min) + '" /></div>';
        //在这里面输入任何合法的js语句（以下将演示一个自定义风格的层）
        pageii = $.layer({
            type: 1,   //0-4的选择,（1代表page层）
            area: ['355px', '370px'],
            //shade: [0],  //不显示遮罩
            //border: [0], //不显示边框
            title: [
                '设置商品促销价格和折扣率',
            //自定义标题风格，如果不需要，直接title: '标题' 即可
                'border:none; background:#61BA7A; color:#fff;'
            ],
            bgcolor: '#eee', //设置层背景色
            page: {
                html: str + str1 + str2 + str3 + str4 + str5 + str6 + str7 + ' <div style="padding:1px; width:90px;margin:0 auto;">\
                                <input style="padding:5px; width:90px;margin:0 auto;" type="button" id="btnsave" value="提交" onclick="save()" />\
                                 </div>\
                              </fieldset></div>'
            },
            shift: 'top' //从上动画弹出
        });

    }

    function retnull(vare) {
        if (parseInt(vare) == 0) {
            return "";
        }
        else {
            return vare;
        }
    }
    //清除促销设置
    function cancelcu(proid) {
        var c = 1;
        $.ajax({
            type: 'POST',
            url: '/admin/cuxiao/SetCuxiao.ashx',
            data: { pid: proid, canel: '1' },
            dataType: "json",
            loading: { type: 3 },
            success: function (msg, textStatus) {
                if (msg["state"] == -1) {
                    layer.alert(msg["message"], 11, !1)
                }
                else {
                    layer.msg(msg["message"], 2, 1);
                    location.reload();
                }

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(errorThrown + " error");
                // 通常情况下textStatus和errorThown只有其中一个有值 
                this; // the options for this ajax request
            }
        });
    }
   
</script>
