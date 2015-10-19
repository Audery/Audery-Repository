<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="_101shop.admin.v3.admin.Advertising.AddProduct" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/admin/style/admin.css" type="text/css" />
    <link rel="stylesheet" href="/admin/style/admin2.css" type="text/css" />
    <script src="/admin/scripts/global.js" type="text/javascript"></script>
    <link rel="stylesheet" href="../style/validator.css" type="text/css" />
    <link rel="stylesheet" href="../style/toolbar.css" type="text/css" />
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
    <form id="form1" runat="server" action="/admin/Advertising/AddProduct.aspx">
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
                <option value="2" <%if(Request["Price"]=="2"){%> selected<%} %>>OTC价</option>
                <option value="3" <%if(Request["Price"]=="3"){%> selected<%} %>>批发价&OTC价</option>
                <option value="5" <%if(Request["Price"]=="5"){%> selected<%} %>>OTC为零</option>
                <option value="6" <%if(Request["Price"]=="6"){%> selected<%} %>>批发为零</option>
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
                <option value="1" <%if(Request["bjgqj"]=="1"){%> selected<%} %>>现款价</option>
                <option value="2" <%if(Request["bjgqj"]=="2"){%> selected<%} %>>零售价</option>
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
                <tbody>
                    <tr>
                         <th scope="col" style="width:55px">
                            编号
                        </th>
                        <th scope="col">
                            商品名称
                        </th>
                        <th scope="col" style="width: 150px">
                            规格（含转换比、件装）
                        </th>
                        <th scope="col">
                            批准文号
                        </th>
                        <th scope="col">
                            生产厂家
                        </th>
                        <th scope="col">
                            现款价
                        </th>
                        <th scope="col">
                            零售价
                        </th>
                        <th scope="col">
                            库存
                        </th>                      
                       
                        <th scope="col">
                            销售方式
                        </th>
                        <th scope="col">
                            状态
                        </th>                      
                        
                        <th scope="col" style="width: 60px;">
                            <asp:DropDownList ID="dropdown" runat="server"></asp:DropDownList>
                        </th>
                    </tr>
                </tbody>
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
<script language="javascript" type="text/javascript" src="/admin/scripts/jquery-1.5.1.min.js"></script>
<script language="javascript" type="text/javascript">
    function putad(proid) {
        if ($("#dropdown").val() == "") {
            alert("请先选择要放入的广告位");
        }
        else {
            var c = $("#dropdown").val();
            $.ajax({
                type: 'POST',
                url: '/admin/product_manager/add_product_to_ad.ashx',
                data: { pid: proid, code: c },
                dataType: "json",
                success: function (msg, textStatus) {
                    alert(msg["message"]);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(errorThrown + " error");
                    // 通常情况下textStatus和errorThown只有其中一个有值 
                    this; // the options for this ajax request
                }
            });
        }
    }
    function istop(sp, proid) {
        if (sp == 0) {
            sx = "启用";
        }
        else {
            sx = "停用";
        }

        if (confirm("确认" + sx + "该商品！")) {
            $.ajax({
                type: 'POST',
                url: '/admin/product_manager/product_stop.ashx',
                data: { pid: proid, stop: sp },
                dataType: "json",
                success: function (msg, textStatus) {
                    //alert(msg + textStatus);
                    switch (msg["state"]) {
                        case -1:
                            alert(msg["message"]);
                            break;
                        case 0:
                            alert(msg["message"]);
                            break;
                        case 1:
                            alert(msg["message"]);
                            document.location.reload();
                            break;
                        default:
                            alert(msg["message"]);
                            break;
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

    function ishelves(sp, proid) {
        if (sp == 1) {
            sx = "上架";
        }
        else if (sp == -1) {
            sx = "暂不上架";
        }
        else {
            sx = "下架";
        }

        if (confirm("确认" + sx + "该商品！")) {
            $.ajax({
                type: 'POST',
                url: '/admin/product_manager/product_shelves.ashx',
                data: { pid: proid, shelves: sp },
                dataType: "json",
                success: function (msg, textStatus) {
                    //alert(msg + textStatus);
                    switch (msg["state"]) {
                        case -1:
                            alert(msg["message"]);
                            break;
                        case 0:
                            alert(msg["message"]);
                            break;
                        case 1:
                            alert(msg["message"]);
                            document.location.reload();
                            break;
                        default:
                            alert(msg["message"]);
                            break;
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

    function verify(sp, proid) {
        if (confirm("确认审核通过该商品！")) {
            $.ajax({
                type: 'POST',
                url: '/admin/product_manager/product_verify.ashx',
                data: { pid: proid, verify: sp },
                dataType: "json",
                success: function (msg, textStatus) {
                    //alert(msg + textStatus);
                    switch (msg["state"]) {
                        case -1:
                            alert(msg["message"]);
                            break;
                        case 0:
                            alert(msg["message"]);
                            break;
                        case 1:
                            alert(msg["message"]);
                            document.location.reload();
                            break;
                        default:
                            alert(msg["message"]);
                            break;
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

    jQuery(document).ready(function () {
        jQuery(".datatable span").each(function (i) {
            var val = jQuery.trim(jQuery(this).html());
            if (val == '' || val == 'N/A' || val == '&nbsp;&nbsp;') {
                this.style.backgroundColor = "#EEE";
            }
            if (val == '') jQuery(this).html('&nbsp;&nbsp;');
        });
    });
</script>
