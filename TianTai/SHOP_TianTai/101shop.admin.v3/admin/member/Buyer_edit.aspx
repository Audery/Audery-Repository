<%@ Page Language="C#" MasterPageFile="~/admin/admin_page.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="Buyer_edit.aspx.cs" Inherits="_101shop.admin.v3.member.Buyer_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/style/validator.css" rel="stylesheet" type="text/css" />
    <link href="/admin/style/toolbar.css" rel="stylesheet" type="text/css" />
    <link href="/scripts/jquery/css/ui-lightness/jquery-ui-1.8.10.custom.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript" src="/scripts/jquery-ui.min.js"></script>
    <script src="/filehandle/LocationJson.ashx?f=jsonp" type="text/javascript"></script>
    <script src="/scripts/jquery/YLChinaArea/YlChinaArea.js" type="text/javascript"></script>
    <script type="text/javascript">        jQuery(document).ready(function () { jQuery("#ChinaArea").jChinaArea({ aspnet: true, s1: "<%=ConsigneeProvince %>", s2: "<%=ConsigneeCity %>", s3: "<%=ConsigneeBorough %>" }) });</script>
    <script type="text/javascript" src="/scripts/jquery/datepicker/jquery.ui.datepicker.min.js"></script>
    <script type="text/javascript" src="/scripts/jquery/datepicker/jquery.ui.datepicker-zh-CN.js"></script>
    <script type="text/javascript" src="/scripts/validate.js"></script>
    <script type="text/javascript" src="/admin/scripts/public.js"></script>
    <link href="/scripts/jquery/weebox/stylesheets/weebox.css" id="弹窗css" rel="stylesheet"
        type="text/css" />
    <script src="/scripts/jquery/weebox/scripts/weebox.js" id="弹窗js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //region crm数据同步（当买家被审核后）
            var id = jQuery("#<%=txtUId.ClientID %>").val();
            var ti = jQuery("#<%=CRM_InterunitStyle_ID.ClientID %>");
            var tx = jQuery("#txtCRM_InterunitStyle_Select");
            var isChecked = false, selChecked = jQuery("#<%=DropDownList2.ClientID %>");
            var txtTrueName = jQuery("#<%=txtTrueName.ClientID %>");
            jQuery("option", selChecked).each(function () { if (this.selected && this.value == "0") isChecked = true; });
            jQuery("#<%=crmAction.ClientID %>").val("0");
            selChecked.change(function () {
                ti = jQuery("#<%=CRM_InterunitStyle_ID.ClientID %>");
                txtTrueName = jQuery("#<%=txtTrueName.ClientID %>");
                var crmAction = jQuery("#<%=crmAction.ClientID %>");
                var isChecked1 = false, selChecked1 = jQuery(this);
                jQuery("option", selChecked1).each(function () { if (this.selected && this.value == "0") isChecked1 = true; });
                if (isChecked1) document.getElementById("inputCRM_InterunitStyle_Select").checked = true;
                if (!isChecked) {
                    crmAction.val(isChecked1 ? "1" : "0");
                }
                crmAction.val(document.getElementById("inputCRM_InterunitStyle_Select").checked == true ? "1" : "0"); //从CRM删除客户数据
                if (isChecked1) {
                   
                }
            });
            //查询目标客户主分类
            if (id != "" && id != "0") {
                jQuery.ajax({ type: "POST", url: "CRM_InterunitStyle.aspx?is_ajax=1", async: false, data: "act=Select_CRM_InterunitStyle:" + id, dataType: "text",
                    success: function (ret) {
                        if (ret.indexOf('{') == 0) {
                            eval('ret=' + ret);
                            if (ret.name != '') { tx.html('<b>【同步CRM】客户分类：</b><br><span style="padding-left:14px;color:#3366DD;">' + ret.name.replace(';', '<br>') + '</span>'); }
                            if (ret.id != '') { ti.val(ret.id); document.getElementById("inputCRM_InterunitStyle_Select").checked = false; }
                            else if (isChecked) {
                                var url = 'CRM_InterunitStyle.aspx?UID=' + id + '&SID=' + ti.val() + '&Name=' + encodeURIComponent(txtTrueName.val());
                                
                            }
                        }
                    }
                });
                document.getElementById("inputCRM_InterunitStyle_Button").style.display = 'none';
            } else {
                document.getElementById("inputCRM_InterunitStyle_Select").checked = false;
            }
            var ParentId = parseInt(jQuery('input[name="ParentId"]').val());
            if (isNaN(ParentId) || ParentId == 0) {
                jQuery("#spanParents").hide();
                jQuery("#ParentId_Div").hide();
            }
            jQuery("#inputCRM_InterunitStyle_Select").click(function () {
                var crmAction = jQuery("#<%=crmAction.ClientID %>");
                if (this.checked == true) {
                    ti = jQuery("#<%=CRM_InterunitStyle_ID.ClientID %>");
                    txtTrueName = jQuery("#<%=txtTrueName.ClientID %>");
                    if (txtTrueName.val() == "") {
                        this.checked = false;
                        crmAction.val("0");
                        return alert("请先填写联系人");
                    }
                    var ParentId = jQuery('input[name="ParentId"]').val();
                    if (ParentId == "0" || ParentId == "") {
                        this.checked = false;
                        crmAction.val("0");
                        return alert("请先选择买家所在单位");
                    }
                    crmAction.val("1");
                    var SID = ti.val(); if (SID == "" || SID == "0") SID = "1";
                    //选择CRM主分类，添加客户数据到CRM
                    window.__f = jQuery.weeboxs.open('<iframe src="CRM_InterunitStyle.aspx?UID=' + id + '&SID=' + SID + '&Name=' + encodeURIComponent(txtTrueName.val()) + '&89927" width="500" height="480" frameborder="no" scrolling="no" noresize="noresize" />', {
                        title: '<span style="padding-left:16px;"><s></s><font color="Green">选择CRM客户分类</font></span>', showCancel: false, okBtnName: '', width: 520, height: 490,
                        onopen: function (f) { f.db.hide(); }, onok: function (f) { }
                    });
                } else {
                    crmAction.val("0");
                }
            });
            //end region

        });

        //region crm数据同步（当买家被审核后）回调函数
        function CRM_InterunitStyle_Select(uid, sid, ret) {
            var crmAction = jQuery("#<%=crmAction.ClientID %>");
            if (sid != "" && sid != "0") {
                document.getElementById("inputCRM_InterunitStyle_Select").checked = true;
                crmAction.val("1");
            }
            var ti = jQuery("#<%=CRM_InterunitStyle_ID.ClientID %>");
            ti.val(sid);
            var tx = jQuery("#txtCRM_InterunitStyle_Select");
            tx.html('<b>【同步CRM】客户分类：</b><br><span style="padding-left:14px;color:#3366DD;">' + ret.replace(';', '<br>') + '</span>');
            if (window.__f) window.__f.close();
        }
        //end region
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagetitle" runat="server">
    <div style="height: 25px;">
        <div style="float: left; height: 25px;">
            买家信息
            <asp:Button ID="button1" runat="server" CssClass="inputbutton" OnClick="btnSave_Click"
                Text="保  存" /></div>
        <div style="float: left; height: 25px; width: 500px;">
            &nbsp;
            <asp:HyperLink ID="HyperLink1" runat="server" CssClass="inputbutton" NavigateUrl="BuyerLib.aspx"
                Width="65px" Height="24px" Style="vertical-align: bottom;">返 回</asp:HyperLink></div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="workspace" runat="server">
    <asp:HiddenField ID="CRM_InterunitStyle_ID" runat="server" />
    <asp:HiddenField ID="crmAction" runat="server" />
    <asp:HiddenField ID="txtUId" runat="server" />
    <asp:Panel ID="pnlMsg" runat="server" Visible="false" CssClass="pnlReturnMessageErr">
        <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
    </asp:Panel>
    <div>
        <div style="margin-left: 10px;">
            <div>
                <h3>
                    帐号信息</h3>
                <hr />
                <table class="form_table_input" border="0" width="100%" cellspacing="0" cellpadding="0">
                    <tr style="display: none;">
                        <td width="115" height="26" class="form_table_input_info">
                            买家帐号：
                        </td>
                        <td>
                            <%if (Request["uid"] == null)
                              { %><asp:TextBox ID="txtUserId" CssClass="long_input" runat="server"></asp:TextBox>
                            <%}
                              else
                              { %><asp:Label ID="lblUserId" runat="server"></asp:Label>
                            <%} %>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <%if (Request["uid"] == null)
                      { %>
                    <tr>
                        <td width="120" height="26" class="form_table_input_info">
                            登陆密码：
                        </td>
                        <td>
                            <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="long_input" runat="server"
                                Text="123456" ToolTip="默认为123456" Width="94"></asp:TextBox>
                            <span style="color: Red">*</span>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <%}
                      else
                      { %>
                    <tr>
                        <td width="120" height="26" class="form_table_input_info">
                            登陆密码：
                        </td>
                        <td>
                            <asp:CheckBox ID="cb_resetPwd" runat="server" Text="重置密码" ToolTip="密码为123456" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <%} %>
                    <tr style="display: none">
                        <td width="120" height="26" class="form_table_input_info" valign="middle">
                            会员等级：
                        </td>
                        <td>
                            <asp:Label ID="lblUserLevel" runat="server" Text="未知"></asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <% if (false)
                       { %>
                    <tr id="findPassword1" runat="server">
                        <td height="43" class="form_table_input_info">
                            是否修改找回密码问题答案：
                        </td>
                        <td>
                            <asp:RadioButtonList ID="radType" runat="server" RepeatLayout="Flow" RepeatColumns="2">
                                <asp:ListItem Value="0" Text="否" Selected="True" />
                                <asp:ListItem Value="1" Text="是" />
                            </asp:RadioButtonList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td height="26" class="form_table_input_info">
                            找回密码问题：
                        </td>
                        <td>
                            <select name="ddlQuestion" id="ddlQuestion" runat="server" width="170" onchange="">
                                <option value="" selected="selected">请选择一个问题</option>
                                <option value="我就读的第一所学校的名称？">我就读的第一所学校的名称？</option>
                                <option value="我最喜欢的休闲运动是什么？">我最喜欢的休闲运动是什么？</option>
                                <option value="我最喜欢的运动员是谁？">我最喜欢的运动员是谁？</option>
                                <option value="我最喜欢的物品的名称？">我最喜欢的物品的名称？</option>
                                <option value="我最喜欢的歌曲？">我最喜欢的歌曲？</option>
                                <option value="我最喜欢的食物？">我最喜欢的食物？</option>
                                <option value="我最爱的人的名字？">我最爱的人的名字？</option>
                                <option value="我最爱的电影？">我最爱的电影？</option>
                                <option value="我妈妈的生日？">我妈妈的生日？</option>
                                <option value="我的初恋日期？">我的初恋日期？</option>
                            </select>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr id="findPassword" runat="server">
                        <td class="form_table_input_info">
                            找回密码答案：
                        </td>
                        <td>
                            <asp:TextBox ID="txtAnswer" runat="server" CssClass="long_input"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr id="findPassword2" runat="server">
                        <td class="form_table_input_info">
                            原找回密码答案：
                        </td>
                        <td>
                            <asp:TextBox ID="txtOldAnswer" runat="server" CssClass="long_input"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Panel ID="palOld" runat="server" CssClass="msgNormal">
                                如果要修改找回密码答案（原来答案必须填写正确）</asp:Panel>
                        </td>
                    </tr>
                    <tr id="findPassword3" runat="server">
                        <td class="form_table_input_info">
                            新找回密码答案：
                        </td>
                        <td>
                            <asp:TextBox ID="txtNewAnswer" CssClass="long_input" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Panel ID="palNew" runat="server" CssClass="msgNormal">
                                如果要修改，新的密码答案不能为空</asp:Panel>
                        </td>
                    </tr>
                    <% } %>
                    <tr>
                        <td width="120" class="form_table_input_info">
                            注册有效期：
                        </td>
                        <td>
                            <label class="RadioGroup">
                                <input type="radio" name="rgPeriodOfValidity" value="0" />
                                <asp:TextBox runat="server" ID="txtPeriodOfValidity" MaxLength="10" Width="70" CssClass="datepicker"></asp:TextBox>
                            </label>
                            <label class="RadioGroup">
                                <input type="radio" name="rgPeriodOfValidity" value="1" <%=txtPeriodOfValidity.Text==""? "checked":"" %> />
                                永久有效</label>
                            <asp:Label ID="PeriodOfValiditymsg" runat="server"></asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <h3>
                    级别</h3>
                <hr />
                <table class="form_table_input" border="0" width="100%" cellspacing="0" cellpadding="0">
                    <tr id="Parent_Div">
                        <td width="120" class="form_table_input_info" valign="middle">
                            上级单位：
                        </td>
                        <td width="420" style="padding-bottom: 13px;">
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr runat="server" id="tdDropDownList1">
                        <td width="120" class="form_table_input_info" valign="middle">
                            企业类型：
                        </td>
                        <td width="420">
                            <asp:DropDownList ID="DropDownList1" runat="server" Style="margin: 4px;">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr runat="server" id="Tr1">
                        <td width="120" class="form_table_input_info" valign="middle">
                            价格类型：
                        </td>
                        <td width="420">
                            <asp:DropDownList ID="DropDownList3" runat="server" Style="margin: 4px;">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr runat="server" id="trdiscount">
                        <td width="120" class="form_table_input_info" valign="middle">
                            会员折扣：
                        </td>
                        <td width="420">
                            <asp:TextBox ID="txtdiscount" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            如果不折扣则填写1，如果打98折就填写 0.98
                        </td>
                    </tr>
                </table>
            </div>
            <div id="select_Editer_Div" runat="server">
                <h3>
                    商城交易</h3>
                <hr />
                <table class="form_table_input" border="0" width="100%" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="115" height="54" class="form_table_input_info">
                            客服：
                        </td>
                        <td width="420">
                            <asp:DropDownList ID="ddl_Editer" runat="server" DataTextField="name" DataValueField="id"
                                Style="margin: 4px;">
                            </asp:DropDownList>
                            <asp:Label ID="lblEditer" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="tipEditer" runat="server" Text="请选择商城交易人员"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="115" height="54" class="form_table_input_info">
                            销售员：
                        </td>
                        <td width="420">
                            <asp:DropDownList ID="ddlOSP" runat="server" DataTextField="ospname" DataValueField="ospid"
                                Style="margin: 4px;">
                            </asp:DropDownList>
                            <asp:Label ID="lbOSP" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="tipOSP" runat="server" Text="请选择销售员"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <h3>
                    联系信息</h3>
                <hr />
                <table class="form_table_input" border="0" width="100%" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="form_table_input_info">
                            联系人：
                        </td>
                        <td>
                            <asp:TextBox ID="txtTrueName" CssClass="long_input" runat="server" tip="联系人姓名(请填写真实姓名)"
                                validatetype="isnull" warning="" error="请填写" MaxLength="30"></asp:TextBox><span style="color: Red">
                                    *</span>
                        </td>
                        <td>
                            <div id="ctl00_workspace_txtTrueNameTip" class="msgNormal">
                                联系人姓名(请填写真实姓名)</div>
                        </td>
                    </tr>
                    <tr>
                        <td width="115" class="form_table_input_info">
                            手机号：
                        </td>
                        <td>
                            <asp:TextBox ID="txtMobilePhone" CssClass="long_input" runat="server" tip="手机号，如：13028282828"
                                validatetype="ismobile" warning="" error="请填写" MaxLength="11"></asp:TextBox><span
                                    style="color: Red"> *</span>
                        </td>
                        <td>
                            <div id="ctl00_workspace_txtMobilePhoneTip" class="msgNormal">
                                请填写正确手机号码（可以用来找回密码或者收取相关重要通知短信！），如：13028282828</div>
                        </td>
                    </tr>
                    <tr>
                        <td width="106" class="form_table_input_info">
                            电子邮件：
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmail" CssClass="long_input" runat="server" tip="买家的邮箱,可用于登陆,或者发送一些与买家相关的信息,比如密码发送等"
                                validatetype="isemail?" warning="" error="邮箱格式错误，正确格式如：abc@163.com" MaxLength="30"></asp:TextBox>
                        </td>
                        <td>
                            <div id="ctl00_workspace_txtEmailTip" class="msgNormal">
                                邮箱正确格式如：abc@163.com
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="106" class="form_table_input_info">
                            QQ邮箱：
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmail_QQ" CssClass="long_input" runat="server" tip="买家的邮箱,可用于登陆,或者发送一些与买家相关的信息,比如密码发送等"
                                validatetype="isemail?" warning="" error="邮箱格式错误，正确格式如：abc@qq.com"></asp:TextBox>
                        </td>
                        <td>
                            <div id="ctl00_workspace_txtEmailTip_QQ" class="msgNormal">
                                邮箱正确格式如：1213245@qq.com
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="form_table_input_info">
                            办公电话：
                        </td>
                        <td>
                            <asp:TextBox ID="txtOfficePhone" CssClass="long_input" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <div class="msgNormal">
                                请填写正确电话号码，比如：028-85809898</div>
                        </td>
                    </tr>
                    <tr>
                        <td class="form_table_input_info">
                            传真：
                        </td>
                        <td>
                            <asp:TextBox ID="txtFax" CssClass="long_input" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <div class="msgNormal">
                                请填写正确传真号，比如：028-85809898</div>
                        </td>
                    </tr>
                    <tr>
                        <td class="form_table_input_info">
                            地区：
                        </td>
                        <td>
                            <span id="ChinaArea" class="ChinaArea">省：<select id="province" name="province" style="width: 70px;"></select>
                                市：<select id="city" name="city" style="width: 80px;" tip="该买家所在的行政区划" validatetype="isint"
                                    warning="" error="请选择地区"></select>
                                <span>区：<select id="county" name="county" style="width: 100px;"></select></span><span
                                    style="color: Red"> *</span>
                                <input type="hidden" id="ddlProvince" name="ddlProvince" />
                                <input type="hidden" id="ddlCity" name="ddlCity" />
                                <input type="hidden" id="ddlBorough" name="ddlBorough" />
                            </span>
                        </td>
                        <td>
                            <div id="cityTip" class="msgNormal">
                                请选择地区</div>
                        </td>
                    </tr>
                    <tr>
                        <td class="form_table_input_info">
                            联系地址：
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddress" CssClass="long_input" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <div class="msgNormal">
                                该买家所在地的详细地址，详细联系地址</div>
                        </td>
                    </tr>                   
                </table>
            </div>
            <div id="CheckUp_Div" runat="server">
                <h3>
                    审核状态</h3>
                <hr />
                <table class="form_table_input" border="0" width="100%" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="115" height="54" class="form_table_input_info">
                            审核：
                        </td>
                        <td width="420">
                            <asp:DropDownList ID="DropDownList2" runat="server" Style="margin: 4px; width: 88px;">
                                <asp:ListItem Value="1">未审核</asp:ListItem>
                                <asp:ListItem Value="0">已审核</asp:ListItem>
                                <asp:ListItem Value="2">已冻结</asp:ListItem>
                            </asp:DropDownList>
                            <asp:CheckBox ID="CheckBox1" runat="server" Text="发送短信通知已经通过审核" Visible="false" />
                        </td>
                        <td>
                            <div id="DropDownList2Tip" class="msgNormal">
                                请选择会员的审核状态
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        //提交表单
        jQuery(function () {
            InitForm(); //验证输入
            jQuery('input[type="submit"]').bind('click', function () {
                //                var ParentId = jQuery('input[name="ParentId"]');
                //                if (ParentId.length == 0) {
                //                    alert("请选择上级单位后再保存！");
                //                    return false;
                //                }
                //                var ok = true; ParentId.each(function () { if (this.value == '' || this.value == '0') ok = false; });
                //                if (!ok) {
                //                    alert("请选择上级单位后再保存！");
                //                    return false;
                //                }
                if (jQuery.trim(jQuery('#workspace_txtTrueName').val()) == '') {
                    alert("请填写联系人姓名！");
                    return false;
                }
                if (jQuery.trim(jQuery('#workspace_txtMobilePhone').val()) == '') {
                    alert("请填写手机号！");
                    return false;
                } else if (!IsMobile(jQuery('#workspace_txtMobilePhone'), false)) {
                    alert("请填写正确的手机号！");
                    return false;
                }
                var RegExp1 = new RegExp("^\\w+((-\\w+)|(\\.\\w+))*\\@[A-Za-z0-9]+((\\.|-)[A-Za-z0-9]+)*\\.[A-Za-z0-9]+$");
                var workspace_txtEmail = jQuery('#workspace_txtEmail');
                if (workspace_txtEmail.val() != '' && !RegExp1.test(workspace_txtEmail.val())) {
                    alert("请填写正确的电子邮件地址！");
                    workspace_txtEmail.get(0).focus();
                    return false;
                }
                var workspace_txtEmail_QQ = jQuery('#workspace_txtEmail_QQ');
                if (workspace_txtEmail_QQ.val() != '' && !RegExp1.test(workspace_txtEmail_QQ.val())) {
                    alert("请填写正确的QQ邮箱地址！");
                    workspace_txtEmail_QQ.get(0).focus();
                    return false;
                }
                var province = parseInt(jQuery('#province').val()), city = parseInt(jQuery('#city').val());
                if (isNaN(province) || province <= 0 || isNaN(city) || city <= 0) {
                    alert("请选择买家所在省市区！");
                    return false;
                }
                var qi = jQuery("#<%=DropDownList1.ClientID %>");
                if (qi.length && !qi.get(0).disabled && (qi.val() == "0" || qi.val() == "")) {
                    alert("请选择企业类型！");
                    return false;
                }
                var ei = jQuery("#<%=ddl_Editer.ClientID %>").val();
                if (ei == "0" || ei == "") {
                    alert("请选择交易人员后再保存！");
                    return false;
                }
                //                var ti = jQuery("#<%=CRM_InterunitStyle_ID.ClientID %>").val();
                //                if (ti == "0" || ti == "") {
                //                    alert("请选择CRM客户分类后再保存！");
                //                    document.getElementById("inputCRM_InterunitStyle_Select").checked = false;
                //                    return false;
                //                }
               
                var ste = jQuery("#<%=DropDownList2.ClientID %>"), st = ste.val();              
                return true;
            });
            //<asp:Literal id="lblScript" runat="server"></asp:Literal>
        });
        //拼音输入
        function getpinyin(str, input) {
            str = jQuery.trim(str);
            if (str != "" && input != null) {
                var url = "../../filehandle/pinyin.ashx", param = "str=" + str + "&ishead=1";
                jQuery.ajax({
                    type: "POST", url: url, async: true, data: param, dataType: "text",
                    success: function (ret) {
                        try { if (ret != "") input.value = ret.toUpperCase(); } catch (e) { }
                    },
                    error: function (x, e) {
                        //alert("服务器连接失败！");
                    },
                    complete: function (x) {
                        //alert(x.responseText);
                    }
                });
            }
        }

        //添加删除父级单位
        var selectParentIndex = 0;
        var btns1 = jQuery("<span class=\"del\">&nbsp;<a href=\"javascript:void(0)\" title=\"点击删除此单位\" onclick=\"delInc(this)\" style=\"display:none\">删除</a>" + "&nbsp;<a href=\"javascript:void(0)\" title=\"点击此单位为默认\" onclick=\"defInc(this)\">默认</a></span>");
        var btns0 = jQuery("<span class=\"del\">&nbsp;<a href=\"javascript:void(0)\" title=\"点击删除此单位\" onclick=\"delInc(this)\" style=\"display:none\">删除</a>" + "&nbsp;默认</span>");
        function addInc(a) {
            var btn = jQuery(a);
            if (btn.length) {
                var span = btn.parent(), td = span.parent();
                span = jQuery("<span>" + span.html().replace("addInc(this)", "").replace("点击添加其他单位", "").replace("添加", "&nbsp;&nbsp;&nbsp;&nbsp;").replace("red", "black") + "</span>");
                span.find('input[type="text"]').css('margin-left', '12px').css('margin-top', '2px');
                span.appendTo(td);
            }
        }
   
     
        function selectParentWindow(o) {
            var input = jQuery(o), hd = input.parent().find('input[type="hidden"]');
            input.parent().find('input[type="text"]').attr('readonly', true);
            input.attr('id', 'id_' + Math.round(Math.random() * 10000));
            hd.attr('id', 'id_' + Math.round(Math.random() * 10000));
            jQuery("#spanParents").children().each(function (i) { if (jQuery('input[type="hidden"]', this).attr('id') == hd.attr('id')) selectParentIndex = i; });
            selectFile('Memberlist', new Array(hd.attr('id'), input.attr('id'), input.attr('id')), 420, 600, "../../", "MemberLevel=1");
        }       
        var lb = jQuery("#workspace_DropDownList1"); if (lb.length && lb.get(0).selectedIndex <= 0 && lb.get(0).disabled == true) lb.removeAttr("disabled");
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="pagebottom" runat="server" ID="ContentBottom">
    <asp:Button ID="button2" runat="server" CssClass="inputbutton" OnClick="btnSave_Click"
        Text="保  存" />
    <!--StatisticalTime(ms[Init,Load,Render])=[<%=StatisticalTime[0]%>,<%=StatisticalTime[1]%>,<%=StatisticalTime[2]%>]-->
</asp:Content>
