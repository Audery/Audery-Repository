using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Collections.Generic;
using SOSOshop.BLL;
using System.Data.Common;
using SOSOshop.BLL.Integral;

namespace _101shop.admin.v3.member
{
    public partial class Buyer_edit : System.Web.UI.Page
    {

        /// <summary>
        /// 省/市/区
        /// </summary>
        public string ConsigneeBorough = "", ConsigneeCity = "", ConsigneeProvince = "";
        /// <summary>
        /// 买家查询
        /// </summary>
        private SOSOshop.BLL.MemberInfo mbll = new SOSOshop.BLL.MemberInfo();

        /// <summary>
        /// 获取交易员和推广人员角色id值
        /// </summary>
        //string jyyId = "33";
        //string tgryId = "37";
        bool enabledErpAction = false;

        #region 统计时间(毫秒)
        public DateTime _StatisticalTime = DateTime.Now;//开始时间
        public int[] StatisticalTime = new int[3];//初始化,加载数据,呈现页面
        protected override void OnPreLoad(EventArgs e)
        {
            StatisticalTime[0] = (int)((DateTime.Now - _StatisticalTime).TotalMilliseconds);
            base.OnPreLoad(e);
        }
        protected override void OnPreRender(EventArgs e)
        {
            StatisticalTime[1] = (int)((DateTime.Now - _StatisticalTime).TotalMilliseconds) - StatisticalTime[0];
            base.OnPreRender(e);
        }
        protected override void OnPreRenderComplete(EventArgs e)
        {
            StatisticalTime[2] = (int)((DateTime.Now - _StatisticalTime).TotalMilliseconds) - StatisticalTime[1];
            base.OnPreRenderComplete(e);
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                trdiscount.Visible = SOSOshop.BLL.PowerPass.isPass("008009016");
                if (!SOSOshop.BLL.PowerPass.isPass("008009001") && !SOSOshop.BLL.PowerPass.isPass("008009012"))
                {
                    SOSOshop.BLL.PromptInfo.Popedom("000000000000", "对不起，您没有查看的权限!");
                }
                DropDownList1.DataSource = SOSOshop.Model.CompanyClass.GetList();
                DropDownList1.DataTextField = "CompanyClassName";
                DropDownList1.DataValueField = "CompanyClassName";
                DropDownList1.DataBind();

                //绑定价格类型
                SOSOshop.BLL.DbBase db = new DbBase();
                db.ChangeDataCenter();
                DropDownList3.DataSource = db.ExecuteTable("SELECT DISTINCT category from dbo.Price");
                DropDownList3.DataTextField = "category";
                DropDownList3.DataBind();
                DropDownList3.Items.Insert(0, new ListItem() { Text = "请选择", Value = "" });

                if (ChangeHope.WebPage.PageRequest.GetQueryInt("uid") > 0)
                {
                    int memberId = ChangeHope.WebPage.PageRequest.GetQueryInt("uid");
                    ViewState["memberId"] = memberId;
                    GetAccountAndInfo(memberId);
                    SelectEditer();
                }
                else
                {
                    string p = "<span id=\"spanParents\"><span><br>&nbsp;<a href=\"javascript:void(0)\" title=\"点击添加其他单位\" onclick=\"addInc(this)\">添加</a>"
                    + "<span>&nbsp;<input name=\"ParentIncName\" type=\"text\" value=\"\" position=\"{x:235,y:110}\" onclick=\"selectParentWindow(this)\" style=\"height:18px;width:300px;cursor:pointer;color:black;\">"
                    + "<input type=\"hidden\" name=\"ParentId\" value=\"0\">"
                    + "</span></span></span>";
                    //this.DropDownList2.Items[1].Enabled = false;
                    //this.DropDownList2.Items[2].Enabled = false;
                    SelectEditer(); // 查询交易员
                    BindOutSellPerson();//绑定外销人员
                }
            }
            //买家审核权限
            bool isCheckUp = SOSOshop.BLL.PowerPass.isPass("008009013");
            cb_resetPwd.Visible = isCheckUp;
            CheckUp_Div.Visible = isCheckUp;
        }

        /// <summary>
        /// 绑定外销人员
        /// </summary>
        protected void BindOutSellPerson()
        {
            SOSOshop.BLL.MemberInfo mbll = new SOSOshop.BLL.MemberInfo();
            string sql = @"SELECT DISTINCT name as ospname, adminid as ospid
                           FROM dbo.yxs_administrators  ";
            DataTable dt = mbll.ExecuteTable(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                ddlOSP.Items.Clear();
                ddlOSP.DataSource = dt;
                ddlOSP.DataBind();
            }

            ListItem li = new ListItem("全部人员", "0");
            ddlOSP.Items.Insert(0, li);
        }

        /// <summary>
        /// 查询交易员
        /// </summary>
        protected void SelectEditer()
        {
            SOSOshop.BLL.MemberInfo mbll = new SOSOshop.BLL.MemberInfo();
            string sqlText5 = "SELECT DISTINCT adminid AS id,name FROM yxs_administrators WHERE name<>'admin'";
            DataTable dt = mbll.ExecuteTable(sqlText5);
            if (dt != null && dt.Rows.Count > 0)
            {
                sqlText5 = "SELECT ',' + REPLACE(REPLACE(STUFF((SELECT CAST(i AS varchar) + ',' AS a  FROM (SELECT DISTINCT b.name AS i FROM memberinfo a INNER JOIN yxs_administrators b ON a.Editer=b.adminid) AS t FOR XML path('')), 1, 0, ''), '<a>', ''), '</a>', '')";
                string _101Admin = Convert.ToString(mbll.ExecuteScalarForCache(sqlText5));
                dt.Columns.Add("OK", typeof(int));
                //sqlText5 = "SELECT ',' + REPLACE(REPLACE(STUFF((SELECT CAST(i AS varchar) + ',' AS a  FROM (SELECT DISTINCT LTRIM(RTRIM(zhiyname)) AS i FROM zhiyzl WHERE beactive='是' AND issp='是' AND jigid='000') AS t FOR XML path('')), 1, 0, ''), '<a>', ''), '</a>', '')";
                //DbBase db2 = new DbBase(); db2.ChangeDB("ConnectionStringERP");
                //string ErpAdmin = Convert.ToString(db2.ExecuteScalarForCache(sqlText5));
                //foreach (DataRow dr in dt.Rows)
                //{
                //    //处理Erp中有没有
                //    if (!_101Admin.Contains("," + dr[1] + ",") && !ErpAdmin.Contains("," + dr[1] + ",")) dr.Delete();
                //}
                ddl_Editer.Items.Clear();
                ddl_Editer.DataSource = dt;
                ddl_Editer.DataBind();
            }
            ListItem li = new ListItem("全部人员", "0");
            ddl_Editer.Items.Insert(0, li);
        }
        protected void SelectDropDownList(DropDownList ddl, string value)
        {
            foreach (ListItem item in ddl.Items) { item.Selected = false; }
            foreach (ListItem item in ddl.Items) { if (item.Value == value) { item.Selected = true; break; } }
        }

        #region 绑定基本信息
        private void GetAccountAndInfo(int id)
        {
            var adminInfo = AdministrorManager.Get();
            SOSOshop.BLL.MemberAccount bll = new SOSOshop.BLL.MemberAccount();
            SOSOshop.Model.MemberAccount model = bll.GetModelNoCache(id);
            SOSOshop.BLL.MemberInfo bllInfo = new SOSOshop.BLL.MemberInfo();
            SOSOshop.Model.MemberInfo infomodel = bllInfo.GetModel(id);
            DropDownList3.SelectedValue = infomodel.PriceCategory;

            txtdiscount.Text = infomodel.discount.ToString();
            if (model != null && infomodel != null)
            {
                DropDownList1.SelectedValue = model.CompanyClass;
                #region 会员审核状态
                DropDownList2.SelectedValue = model.State.ToString();
                DropDownList2.Enabled = SOSOshop.BLL.PowerPass.isPass("008009013");//权限


                CheckBox1.ToolTip = DropDownList2.SelectedValue;
                if (model.State == 1 && infomodel.Editer == adminInfo.AdminId)
                {
                    CheckBox1.Visible = CheckBox1.Enabled = CheckBox1.Checked = true;
                }
                #endregion
                DropDownList1.SelectedValue = model.CompanyClass;
                //修改权限
                bool isEdit = SOSOshop.BLL.PowerPass.isPass("008009004");
                if (model.Question == null || model.Question.Trim() == "")
                {
                    findPassword.Visible = true;
                    findPassword1.Visible = findPassword2.Visible = findPassword3.Visible = false;
                }
                else
                {
                    findPassword.Visible = false;
                    findPassword1.Visible = findPassword2.Visible = findPassword3.Visible = true;
                }
                lblUserId.Text = model.UserId;
                SOSOshop.BLL.MemberRank mrbll = new SOSOshop.BLL.MemberRank();
                SOSOshop.Model.MemberRank mrmodel = mrbll.GetModel(Convert.ToInt32(model.UserGroup));
                lblUserLevel.Text = mrmodel != null ? mrmodel.Name : "未知";//买家等级
                txtMobilePhone.Text = model.MobilePhone;
                txtEmail.Text = model.Email;//电子邮件
                txtEmail_QQ.Text = model.Email_QQ;//QQ邮箱
                //登陆有效期
                if (model.PeriodOfValidity.ToString("yyyy-MM-dd") == Date_rgPeriodOfValidity.ToString("yyyy-MM-dd"))
                    txtPeriodOfValidity.Text = "";
                else
                    txtPeriodOfValidity.Text = CheckTimeOut(model.PeriodOfValidity.ToString(), PeriodOfValiditymsg);
                CheckTimeAccessKey(txtPeriodOfValidity);

                ViewState["RegisterDate"] = model.RegisterDate.ToString();
                ViewState["RegisterIP"] = model.RegisterIP.ToString();
                ViewState["Capital"] = model.Capital.ToString();
                ViewState["Coupons"] = model.Coupons.ToString();
                ViewState["Points"] = model.Points.ToString();
                ViewState["PeriodOfValidity"] = model.PeriodOfValidity.ToString();

                string p = "<span id=\"spanParents\" style=\"\"><span><br>&nbsp;<a href=\"javascript:void(0)\" title=\"点击添加其他单位\" onclick=\"addInc(this)\">添加</a>"
                   + "<span>&nbsp;<input name=\"ParentIncName\" type=\"text\" value=\"\" position=\"{x:235,y:110}\" onclick=\"selectParentWindow(this)\" style=\"height:18px;width:300px;cursor:pointer;color:black;\">"
                   + "<input type=\"hidden\" name=\"ParentId\" value=\"0\">"
                   + "</span></span></span>";
                this.txtUId.Value = id.ToString();
                this.txtTrueName.Text = infomodel.TrueName;
                this.txtAddress.Text = infomodel.Address;
                this.txtOfficePhone.Text = infomodel.OfficePhone;
                this.txtFax.Text = infomodel.Fax;

                #region 实例化省市区联动
                DataSet dsProvinces = bll.ExecuteDataSet("select isnull((select TOP(1) Name from Region where ID=" + infomodel.Province + "),'') as a,isnull((select TOP(1) Name from Region where ID=" + infomodel.City + "),'') as b,isnull((select TOP(1) Name from Region where ID=" + infomodel.Borough + "),'') as c");
                if (dsProvinces != null && dsProvinces.Tables.Count > 0 && dsProvinces.Tables[0].Rows.Count > 0)
                {
                    ConsigneeProvince = dsProvinces.Tables[0].Rows[0][0].ToString();
                    ConsigneeCity = dsProvinces.Tables[0].Rows[0][1].ToString();
                    ConsigneeBorough = dsProvinces.Tables[0].Rows[0][2].ToString();
                }
                #endregion

                #region 添加其他单位

                if (infomodel.ParentId >= 0)
                {
                    DataSet ds1 = bll.ExecuteDataSet("SELECT ID UID,Name IncName FROM DrugsBase_Enterprise WHERE id IN (" + (string.IsNullOrEmpty(infomodel.Parents) ? infomodel.ParentId.ToString() : infomodel.Parents.TrimEnd(',')) + ")");
                    if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        p = "<span id=\"spanParents\">";
                        for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                        {
                            DataRow dr1 = ds1.Tables[0].Rows[i];
                            bool isdef = (infomodel.ParentId == Convert.ToInt32(dr1[0]));
                            if (i == 0)
                            {
                                if (infomodel.ParentId <= 0) isdef = true;
                                Literal1.Text = dr1[1].ToString();
                            }
                            else
                            {
                                Literal1.Text = dr1[0].ToString();
                            }
                        }
                        p += "</span>";
                        bool editParent = false;// int ddl2 = 1; int.TryParse(DropDownList2.SelectedValue, out ddl2); editParent = (ddl2 == 1);                        
                    }
                }
                #endregion

                SelectEditer(); // 查询交易员
                SelectDropDownList(ddl_Editer, infomodel.Editer.ToString());
                BindOutSellPerson(); // 查询外销人员
                SelectDropDownList(ddlOSP, infomodel.OSPId.ToString());
                if (infomodel.Editer <= 0)
                {

                    ddl_Editer.Visible = ddl_Editer.Enabled = true; tipEditer.Visible = true;
                    lblEditer.Text = "";
                }
                else if (ddl_Editer.SelectedIndex > 0)
                {
                    //未审核时
                    if (int.Parse(DropDownList2.SelectedValue) == 1)
                    {
                        ddl_Editer.Visible = ddl_Editer.Enabled = true; tipEditer.Visible = true;
                        if (ddl_Editer.SelectedIndex > 0) lblEditer.Text = ddl_Editer.SelectedItem.Text;
                    }
                    else
                    {
                        ddl_Editer.Visible = ddl_Editer.Enabled = true; tipEditer.Visible = true;
                        if (ddl_Editer.SelectedIndex > 0) lblEditer.Text = ddl_Editer.SelectedItem.Text;
                    }
                }
                else
                {
                    ddl_Editer.Visible = ddl_Editer.Enabled = true; tipEditer.Visible = true;
                    lblEditer.Text = "等待分配客服...";
                }
                if (int.Parse(DropDownList2.SelectedValue) != 1 && ddlOSP.SelectedIndex > 0)
                {
                    ddlOSP.Visible = ddlOSP.Enabled = true; tipOSP.Visible = true;
                    if (ddlOSP.SelectedIndex > 0) lbOSP.Text = ddlOSP.SelectedItem.Text;
                }
            }
        }

        /// <summary>
        /// 有效期选择，访问键 for js @yangzhou
        /// </summary>
        /// <param name="tb"></param>
        private void CheckTimeAccessKey(TextBox tb)
        {
            if (tb != null)
            {
                string txt = tb.Text;
                tb.Attributes.Add("ForAccessKey", string.IsNullOrEmpty(txt) ? "-1" : (txt.Length < 10 || int.Parse(txt.Substring(0, 4)) - DateTime.Now.Year >= 100 ? "1" : "0"));
            }
        }
        /// <summary>
        /// 加载时间是判断格式与值
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private string CheckTimeOut(string time, Label label)
        {
            if (time == null || time.Trim() == "")
            {
                return "";
            }
            else
            {
                if (time.Trim().IndexOf("永久") != -1)
                {
                    return "";
                }
                try
                {
                    DateTime dt = DateTime.Parse(time.Trim()); DateTime now = DateTime.Now;
                    if (dt > now)
                    {
                        if (dt.AddDays(-7) <= now)
                        {
                            label.Text += " <font color=\"green\" style=\"line-height:200%;\">快要过期</font> ";
                        }
                    }
                    else
                    {
                        label.Text += " <font color=\"red\" style=\"line-height:200%;\">已经过期</font> ";
                    }
                    return dt.ToString("yyyy-MM-dd");
                }
                catch { }
                return "";
            }
        }
        #endregion

        #region 保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SOSOshop.BLL.PromptInfo.Popedom("008009004", "对不起，您没有权限进行编辑");
            SOSOshop.BLL.MemberInfo bll = new SOSOshop.BLL.MemberInfo();
            int uid = ChangeHope.WebPage.PageRequest.GetQueryInt("uid");
            bool edit = (uid > 0);
            //try
            //{
            //if (ChangeHope.WebPage.PageRequest.GetFormString("ParentId").Replace("0,", "").Trim(',') == "")
            //{
            //    this.ltlMsg.Text = "保存失败" + "\r\n上级单位不能为空！";
            //    this.pnlMsg.Visible = true;
            //    this.pnlMsg.CssClass = "actionErr";
            //    return;
            //}
            if (this.txtTrueName.Text.Trim() == "")
            {
                this.ltlMsg.Text = "保存失败" + "\r\n联系人不能为空！";
                this.pnlMsg.Visible = true;
                this.pnlMsg.CssClass = "actionErr";
                return;
            }
            if (this.txtMobilePhone.Text.Trim() == "")
            {
                this.ltlMsg.Text = "保存失败" + "\r\n手机号不能为空！";
                this.pnlMsg.Visible = true;
                this.pnlMsg.CssClass = "actionErr";
                return;
            }
            else if (!Regex.IsMatch(this.txtMobilePhone.Text.Trim(), @"^[0-9\-/ ]+$", RegexOptions.IgnoreCase))
            {
                this.ltlMsg.Text = "保存失败" + "\r\n手机号填写错误！";
                this.pnlMsg.Visible = true;
                this.pnlMsg.CssClass = "actionErr";
                return;
            }
            else if ((!edit && bll.ExecuteScalar("select 1 from memberaccount where MobilePhone like '" + this.txtMobilePhone.Text.Trim() + "%'") != null)
                || (edit && bll.ExecuteScalar("select 1 from memberaccount where MobilePhone like '" + this.txtMobilePhone.Text.Trim() + "%' and UID!=" + uid) != null))
            {
                this.ltlMsg.Text = "保存失败" + "\r\n手机号填写错误！此手机号已经在使用，请检查后再填写正确！";
                this.pnlMsg.Visible = true;
                this.pnlMsg.CssClass = "actionErr";
                return;
            }
            if (this.txtEmail.Text.Trim() != string.Empty)
            {
                if (!Regex.IsMatch(this.txtEmail.Text.Trim(), @"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$", RegexOptions.IgnoreCase))
                {
                    this.ltlMsg.Text = "保存失败" + "\r\n邮箱填写错误！";
                    this.pnlMsg.Visible = true;
                    this.pnlMsg.CssClass = "actionErr";
                    return;
                }
                else if (bll.ExecuteScalar("select 1 from memberaccount where Email = '" + this.txtEmail.Text.Trim() + "' and UID!=" + uid) != null)
                {
                    this.ltlMsg.Text = "保存失败" + "\r\n邮箱填写错误！此邮箱已经在使用，请检查后再填写正确！";
                    this.pnlMsg.Visible = true;
                    this.pnlMsg.CssClass = "actionErr";
                    return;
                }
            }
            if (this.txtEmail_QQ.Text.Trim() != string.Empty)
            {
                if (!Regex.IsMatch(this.txtEmail_QQ.Text.Trim(), @"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$", RegexOptions.IgnoreCase))
                {
                    this.ltlMsg.Text = "保存失败" + "\r\n邮箱填写错误！";
                    this.pnlMsg.Visible = true;
                    this.pnlMsg.CssClass = "actionErr";
                    return;
                }
                else if (bll.ExecuteScalar("select 1 from memberaccount where Email_QQ = '" + this.txtEmail_QQ.Text.Trim() + "' and UID!=" + uid) != null)
                {
                    this.ltlMsg.Text = "保存失败" + "\r\n邮箱填写错误！此邮箱已经在使用，请检查后再填写正确！";
                    this.pnlMsg.Visible = true;
                    this.pnlMsg.CssClass = "actionErr";
                    return;
                }
            }
            //if (ddl_Editer.SelectedValue == "0" || ddl_Editer.SelectedValue == "")
            //{
            //    this.ltlMsg.Text = "保存失败" + "\r\n请选择交易人员后再保存！";
            //    this.pnlMsg.Visible = true;
            //    this.pnlMsg.CssClass = "actionErr";
            //    return;
            //}
            //if (CRM_InterunitStyle_ID.Value == "0" || CRM_InterunitStyle_ID.Value == "")
            //{
            //    this.ltlMsg.Text = "保存失败" + "\r\n请选择CRM客户分类后再保存！";
            //    this.pnlMsg.Visible = true;
            //    this.pnlMsg.CssClass = "actionErr";
            //    return;
            //}              

            string Province = ChangeHope.WebPage.PageRequest.GetFormString("province");
            string City = ChangeHope.WebPage.PageRequest.GetFormString("city");
            string Borough = ChangeHope.WebPage.PageRequest.GetFormString("county");
            if (Province == null || Province.Trim() == string.Empty || !Regex.IsMatch(Province, @"^[0-9]{1,4}$", RegexOptions.IgnoreCase)
                || City == null || City.Trim() == string.Empty || !Regex.IsMatch(City, @"^[0-9]{1,4}$", RegexOptions.IgnoreCase))
            {
                this.ltlMsg.Text = "保存失败" + "\r\n没有选择省份城市！请检查！";
                this.pnlMsg.Visible = true;
                this.pnlMsg.CssClass = "actionErr";
                return;
            }
            if (edit)
            {
                if (!UpdateAccount()) return;
                UpdateInfo();
                if (DropDownList2.SelectedValue == "0")
                {
                    MemberIntegral bllmi = new MemberIntegral();
                    MemberIntegralLock ml = new MemberIntegralLock();
                    if (ml.isAllow(uid, MemberIntegralTemplateEnum.建档通过))
                    {
                        //注册送积分(建档成功才开始送会员积分)
                        bllmi.AddIntegral(uid, 0, SOSOshop.BLL.Integral.MemberIntegralTemplateEnum.会员注册, "");
                        bllmi.AddIntegral(uid, 0, MemberIntegralTemplateEnum.建档通过, "");
                    }
                }
                #region 后台用户操作日志记录
                SOSOshop.Model.AdminInfo adminInfo = SOSOshop.BLL.AdministrorManager.Get();
                SOSOshop.BLL.Logs.Log.LogAdminAdd("修改买家信息", (adminInfo == null ? 0 : adminInfo.AdminId), (adminInfo == null ? "" : adminInfo.AdminName), 1);
                #endregion
                #region 清除缓存
                SOSOshop.BLL.DbBase db1 = new SOSOshop.BLL.DbBase(); db1.ClearCache();
                #endregion
            }
            else
            {
                AddAccount();
                #region 后台用户操作日志记录
                SOSOshop.Model.AdminInfo adminInfo = SOSOshop.BLL.AdministrorManager.Get();
                SOSOshop.BLL.Logs.Log.LogAdminAdd("添加买家信息", (adminInfo == null ? 0 : adminInfo.AdminId), (adminInfo == null ? "" : adminInfo.AdminName), 1);
                #endregion
            }
            //}
            //catch (Exception ex)
            //{

            //    this.ltlMsg.Text = (edit ? "编辑" : "添加") + "买家资料失败" + "\r\n" + ex.Message;
            //    this.pnlMsg.Visible = true;
            //    this.pnlMsg.CssClass = "actionErr";
            //}
        }
        /// <summary>
        /// 修改账户
        /// </summary>
        private void UpdateInfo()
        {
            SOSOshop.BLL.MemberInfo bll = new SOSOshop.BLL.MemberInfo();
            if (ViewState["memberId"] != null)
            {
                SOSOshop.Model.MemberInfo model = bll.GetModel(Convert.ToInt32(ViewState["memberId"]));
                if (model != null)
                {
                    model.TrueName = txtTrueName.Text.Trim();
                    model.Photo = "";//txtPhoto.Text;
                    int area = 0; int.TryParse(Request["province"], out area);
                    model.Province = area;
                    area = 0; int.TryParse(Request["city"], out area);
                    model.City = area;
                    area = 0; int.TryParse(Request["county"], out area);
                    model.Borough = area;
                    model.Address = txtAddress.Text;
                    model.OfficePhone = txtOfficePhone.Text;
                    model.HomePhone = "";// txtHomePhone.Text;
                    model.Fax = txtFax.Text;
                    model.PersonWebSite = "";// txtPersonWebSite.Text;
                    model.QQ = "";// txtQQ.Text;        
                    model.discount = decimal.Parse(txtdiscount.Text);
                    area = 0; int.TryParse(ddl_Editer.SelectedValue, out area);
                    model.Editer = area;
                    model.OSPId = int.Parse(ddlOSP.SelectedValue);
                    model.PriceCategory = DropDownList3.SelectedValue;
                    if (DropDownList1.SelectedValue == "生产企业" || DropDownList1.SelectedValue == "商业公司" || DropDownList1.SelectedValue == "民营医院")
                    {
                        model.Member_Class = 0;
                    }
                    else
                    {
                        model.Member_Class = 1;
                    }


                    //写入数据库
                    bool ok = bll.Update(model, enabledErpAction && int.Parse(DropDownList2.SelectedValue) == 0/* && model.ParentId > 0*/);
                }
                this.ltlMsg.Text = "保存成功·<script>if(confirm('编辑成功！继续编辑请点击确定。')){location.href='Buyer_edit.aspx?type=lib&uid=" + ViewState["memberId"] + "&act=new';}else{location.href='BuyerLib.aspx';}</script>";
                this.pnlMsg.Visible = true;
                this.pnlMsg.CssClass = "actionOk";
            }

        }
        /// <summary>
        /// 修改账户详情
        /// </summary>
        private bool UpdateAccount()
        {
            SOSOshop.BLL.MemberAccount accountBll = new SOSOshop.BLL.MemberAccount();
            if (ViewState["memberId"] != null)
            {
                SOSOshop.Model.MemberAccount model = accountBll.GetModel(Convert.ToInt32(ViewState["memberId"]));
                string password = model.PassWord;
                string resetPwd = "123456";
                model.PassWord = "";
                if (this.cb_resetPwd.Visible && this.cb_resetPwd.Checked)
                {
                    model.PassWord = password = resetPwd;
                    model.PassWord = ChangeHope.Common.DEncryptHelper.Encrypt(model.PassWord, 1);
                }
                model.CompanyClass = DropDownList1.SelectedValue;
                model.RegisterDate = Convert.ToDateTime(ViewState["RegisterDate"].ToString());
                model.RegisterIP = ViewState["RegisterIP"].ToString();
                model.MobilePhone = this.txtMobilePhone.Text.Trim();
                model.Capital = Convert.ToDecimal(ViewState["Capital"].ToString());
                model.Coupons = Convert.ToInt32(ViewState["Coupons"].ToString());
                model.Points = Convert.ToInt32(ViewState["Points"].ToString());
                model.Email = this.txtEmail.Text.Trim().ToString();
                model.Email_QQ = this.txtEmail_QQ.Text.Trim().ToString();
                //if (this.ddlUserType.Enabled) model.UserType = Convert.ToInt32(this.ddlUserType.SelectedValue);
                int rgPeriodOfValidity = ChangeHope.WebPage.PageRequest.GetFormInt("rgPeriodOfValidity");
                if (rgPeriodOfValidity == -1)
                {
                    model.PeriodOfValidity = ViewState["PeriodOfValidity"] == null ? Date_rgPeriodOfValidity : Convert.ToDateTime(ViewState["PeriodOfValidity"].ToString());
                }
                else
                {
                    model.PeriodOfValidity = rgPeriodOfValidity == 1 ? Date_rgPeriodOfValidity : Convert.ToDateTime(txtPeriodOfValidity.Text);
                }
                string type = this.radType.SelectedValue;
                if (type == "1")
                {
                    if (model.Question == null || model.Question.Trim() == "")
                    {
                        string Question = this.ddlQuestion.Value;
                        string Answer = this.txtAnswer.Text.Trim().ToString();
                        if (Question == "")
                        {
                            this.ltlMsg.Text = "操作失败：找回密码问题选择错误，请选择一个问题！";
                            this.pnlMsg.Visible = true;
                            this.pnlMsg.CssClass = "actionErr";
                            return false;
                        }
                        if (Answer == "")
                        {
                            this.ltlMsg.Text = "操作失败：找回密码答案填写错误，不能为空！";
                            this.pnlMsg.Visible = true;
                            this.pnlMsg.CssClass = "actionErr";
                            return false;
                        }
                        model.Question = Question;
                        model.Answer = Answer;
                        this.palOld.CssClass = "msgNormal";
                    }
                    else
                    {

                        string oldQuestion = this.ddlQuestion.Value;
                        string oldAnswer = this.txtOldAnswer.Text.Trim().ToString();
                        string newAnswer = this.txtNewAnswer.Text.Trim().ToString();
                        if (oldQuestion == "" || oldQuestion != model.Question)
                        {
                            this.ltlMsg.Text = "操作失败：原找回密码问题选择错误！";
                            this.pnlMsg.Visible = true;
                            this.pnlMsg.CssClass = "actionErr";
                            return false;
                        }
                        if (oldAnswer == "" || oldAnswer != model.Answer)
                        {
                            this.ltlMsg.Text = "操作失败：原找回密码答案填写错误！";
                            this.pnlMsg.Visible = true;
                            this.pnlMsg.CssClass = "actionErr";
                            return false;
                        }
                        if (newAnswer == "")
                        {
                            this.ltlMsg.Text = "操作失败：新找回密码答案不能为空！";
                            this.pnlMsg.Visible = true;
                            this.pnlMsg.CssClass = "actionErr";
                            return false;
                        }
                        model.Answer = newAnswer;
                        this.palOld.CssClass = "msgNormal";
                    }
                }
                model.State = int.Parse(DropDownList2.SelectedValue);//默认 通过审核 注意和冻结 2 不一样；冻结不能登陆。
                enabledErpAction = model.State != 2;//只同步审核的买家
                accountBll.Update(model);
                //发送短信通知已经通过审核
                if (model.State == 0)
                {
                    if (CheckBox1.ToolTip != "0" && CheckBox1.Checked)
                    {
                        string msg = "恭喜你已经通过审核，请凭手机号登录" + (password == resetPwd ? "，登录密码为" + password : "");
                        SOSOshop.BLL.Sms.SendAndSaveDataBase(txtMobilePhone.Text.Trim(), msg, "系统", txtMobilePhone.Text.Trim());
                    }
                }
                return true;
            }
            return false;
        }

        #region 验证买家是否存在
        /// <summary>
        /// 买家是否存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        protected string UserVa(string UserName)
        {
            string RegStr = "";
            YXShop.BLL.Member.MemberAccount bll = new YXShop.BLL.Member.MemberAccount();
            string parm = ChangeHope.Common.StringHelper.InputTexts(UserName);
            SOSOshop.BLL.SysParameter sp = new SOSOshop.BLL.SysParameter();
            if (sp.ForbidUserId != "")
            {
                string[] strForbid = sp.ForbidUserId.Split(',');
                foreach (string str in strForbid)
                {
                    if (parm.IndexOf(str) > -1)
                    {
                        RegStr = "您设置的买家名为非法买家名！";
                        break;
                    }
                }
            }

            if (bll.Exists(parm))
            {
                RegStr = "您设置的买家名已存在，请另输入买家名！";
            }
            return RegStr;
        }
        /// <summary>
        /// 买家邮箱是否存在
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        protected string EmailVa(string Email)
        {
            string RegStr = "";
            YXShop.BLL.Member.MemberAccount bll = new YXShop.BLL.Member.MemberAccount();
            string parm = ChangeHope.Common.StringHelper.InputTexts(Email);
            SOSOshop.BLL.SysParameter sp = new SOSOshop.BLL.SysParameter();
            if (sp.ForbidUserId != "")
            {
                string[] strForbid = sp.ForbidUserId.Split(',');
                foreach (string str in strForbid)
                {
                    if (parm.IndexOf(str) > -1)
                    {
                        RegStr = "您输入中包含非法字符！";
                        break;
                    }
                }
            }

            if (bll.ExistEmail(parm))
            {
                RegStr = "您输入的电子邮箱已存在，请另输入电子邮箱！";
            }
            return RegStr;
        }

        /// <summary>
        /// 买家QQ邮箱是否存在
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        protected string EmailVa_QQ(string Email_QQ)
        {
            string RegStr = "";
            YXShop.BLL.Member.MemberAccount bll = new YXShop.BLL.Member.MemberAccount();
            string parm = ChangeHope.Common.StringHelper.InputTexts(Email_QQ);
            SOSOshop.BLL.SysParameter sp = new SOSOshop.BLL.SysParameter();
            if (sp.ForbidUserId != "")
            {
                string[] strForbid = sp.ForbidUserId.Split(',');
                foreach (string str in strForbid)
                {
                    if (parm.IndexOf(str) > -1)
                    {
                        RegStr = "您输入中包含非法字符！";
                        break;
                    }
                }
            }

            if (bll.ExistEmail_QQ(parm))
            {
                RegStr = "您输入的QQ邮箱已存在，请另输入新的QQ邮箱！";
            }
            return RegStr;
        }

        /// <summary>
        /// 根据账号得到自增ID
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        protected int GetIdByUserId(string UserId)
        {
            YXShop.BLL.Member.MemberAccount bll = new YXShop.BLL.Member.MemberAccount();
            YXShop.Model.Member.MemberAccount model = bll.GetModel(UserId);
            if (model != null)
            {
                return model.UID;
            }
            else
            {
                return -1;
            }
        }
        #endregion

        /// <summary>
        /// 添加账户
        /// </summary>
        /// <returns></returns>
        private void AddAccount()
        {
            enabledErpAction = true;
            SOSOshop.BLL.MemberAccount accountBll = new SOSOshop.BLL.MemberAccount();
            SOSOshop.Model.MemberAccount model = new SOSOshop.Model.MemberAccount();
            model.UserId = this.txtUserId.Text;
            string password = model.PassWord;
            string resetPwd = "123456";
            model.PassWord = "";
            if (this.cb_resetPwd.Visible && this.cb_resetPwd.Checked)
            {
                model.PassWord = password = resetPwd;
                model.PassWord = ChangeHope.Common.DEncryptHelper.Encrypt(model.PassWord, 1);
            }
            else if (this.txtPassword.Text.Trim() != "")
            {
                model.PassWord = password = this.txtPassword.Text.Trim();
                model.PassWord = ChangeHope.Common.DEncryptHelper.Encrypt(model.PassWord, 1);
            }
            if (model.PassWord == "")
            {
                model.PassWord = password = resetPwd;
                model.PassWord = ChangeHope.Common.DEncryptHelper.Encrypt(model.PassWord, 1);
            }
            if (Request.Form["rgPeriodOfValidity"] == "1")
            {
                model.PeriodOfValidity = DateTime.Now.AddYears(100);
            }
            else
            {
                model.PeriodOfValidity = DateTime.Parse(txtPeriodOfValidity.Text);
            }

            model.RegisterDate = DateTime.Now;
            model.RegisterIP = ChangeHope.WebPage.PageRequest.GetIP();
            model.MobilePhone = this.txtMobilePhone.Text.Trim();
            model.Email = this.txtEmail.Text.Trim().ToString();
            model.Email_QQ = this.txtEmail_QQ.Text.Trim().ToString();
            model.Question = "";
            model.Answer = "";
            string type = this.radType.SelectedValue;
            if (type == "1")
            {
                if (model.Question == null || model.Question.Trim() == "")
                {
                    string Question = this.ddlQuestion.Value;
                    string Answer = this.txtAnswer.Text.Trim().ToString();
                    if (Question == "")
                    {
                        this.ltlMsg.Text = "操作失败：找回密码问题选择错误，请选择一个问题！";
                        this.pnlMsg.Visible = true;
                        this.pnlMsg.CssClass = "actionErr";
                        return;
                    }
                    if (Answer == "")
                    {
                        this.ltlMsg.Text = "操作失败：找回密码答案填写错误，不能为空！";
                        this.pnlMsg.Visible = true;
                        this.pnlMsg.CssClass = "actionErr";
                        return;
                    }
                    model.Question = Question;
                    model.Answer = Answer;
                    this.palOld.CssClass = "msgNormal";
                }
                else
                {

                    string oldQuestion = this.ddlQuestion.Value;
                    string oldAnswer = this.txtOldAnswer.Text.Trim().ToString();
                    string newAnswer = this.txtNewAnswer.Text.Trim().ToString();
                    if (oldQuestion == "" || oldQuestion != model.Question)
                    {
                        this.ltlMsg.Text = "操作失败：原找回密码问题选择错误！";
                        this.pnlMsg.Visible = true;
                        this.pnlMsg.CssClass = "actionErr";
                        return;
                    }
                    if (oldAnswer == "" || oldAnswer != model.Answer)
                    {
                        this.ltlMsg.Text = "操作失败：原找回密码答案填写错误！";
                        this.pnlMsg.Visible = true;
                        this.pnlMsg.CssClass = "actionErr";
                        return;
                    }
                    if (newAnswer == "")
                    {
                        this.ltlMsg.Text = "操作失败：新找回密码答案不能为空！";
                        this.pnlMsg.Visible = true;
                        this.pnlMsg.CssClass = "actionErr";
                        return;
                    }
                    model.Answer = newAnswer;
                    this.palOld.CssClass = "msgNormal";
                }
            }
            model.State = int.Parse(DropDownList2.SelectedValue);//默认 通过审核 注意和冻结 2 不一样；冻结不能登陆。
            int uid = accountBll.Add(model);
            model.UID = uid;
            if (uid > 0)
            {

                #region 添加积分记录（取消此功能）
                //YXShop.Model.Member.Integral modelInte = new YXShop.Model.Member.Integral();
                //YXShop.BLL.Member.Integral bllInte = new YXShop.BLL.Member.Integral();
                //modelInte.Userid = GetIdByUserId(model.UserId);
                //modelInte.IntegralClass = 3;
                //modelInte.Origin = "注册赠送";
                //modelInte.IntegralNum = model.Points;
                //modelInte.GainDate = DateTime.Now;
                //modelInte.NoteDate = DateTime.Now;
                //modelInte.NoteName = "系统自动记录";
                //modelInte.Remark = "注册买家时赠送的积分";
                //modelInte.IntegralStatus = 0;
                //try
                //{
                //    bllInte.Add(modelInte);
                //}
                //catch
                //{
                //    this.ltlMsg.Text = "添加买家资料失败！";
                //    this.pnlMsg.Visible = true;
                //    this.pnlMsg.CssClass = "actionErr";
                //    return;
                //}
                #endregion

                #region 添加点卷记录（取消此功能）
                //YXShop.Model.Member.UserInfoNote modelNote = new YXShop.Model.Member.UserInfoNote();
                //YXShop.BLL.Member.UserInfoNote bllNote = new YXShop.BLL.Member.UserInfoNote();
                //modelNote.UserID = GetIdByUserId(model.UserId);
                //modelNote.TicketCount = 0;
                //modelNote.Causation = "注册赠送的点卷";
                //modelNote.BosomNote = "注册赠送的点卷";
                //modelNote.NoteDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                //modelNote.NoteName = "系统自动记录";
                //modelNote.NoteType = 0;
                //modelNote.BuckleOrAdd = 0;
                //modelNote.Username = model.UserId;
                //try
                //{
                //    bllNote.Add(modelNote);
                //}
                //catch
                //{
                //    this.ltlMsg.Text = "添加买家资料失败！";
                //    this.pnlMsg.Visible = true;
                //    this.pnlMsg.CssClass = "actionErr";
                //    return;
                //}
                #endregion

                #region 添加附属信息
                SOSOshop.BLL.MemberInfo bllInfo = new SOSOshop.BLL.MemberInfo();
                SOSOshop.Model.MemberInfo modelInfo = new SOSOshop.Model.MemberInfo();
                modelInfo.UID = uid;
                modelInfo.Member_Type = 1;
                modelInfo.TrueName = txtTrueName.Text.Trim();
                modelInfo.Photo = "";//txtPhoto.Text;
                int area = 0; int.TryParse(Request["province"], out area);
                modelInfo.Province = area;
                area = 0; int.TryParse(Request["city"], out area);
                modelInfo.City = area;
                area = 0; int.TryParse(Request["county"], out area);
                modelInfo.Borough = area;
                modelInfo.Address = txtAddress.Text;
                modelInfo.OfficePhone = txtOfficePhone.Text;
                modelInfo.HomePhone = "";// txtHomePhone.Text;
                modelInfo.Fax = txtFax.Text;
                modelInfo.PersonWebSite = "";// txtPersonWebSite.Text;
                modelInfo.QQ = "";// txtQQ.Text;
                modelInfo.PriceCategory = DropDownList3.SelectedValue;
                modelInfo.ParentId = 0;
                modelInfo.Parents = "";

                bool ok = bllInfo.Add(modelInfo, enabledErpAction && int.Parse(DropDownList2.SelectedValue) == 0/* && modelInfo.ParentId > 0*/);
                //添加权限
                if (ok)
                {
                    SOSOshop.BLL.MemberPermission cBll = new SOSOshop.BLL.MemberPermission();
                    SOSOshop.Model.MemberPermission c = new SOSOshop.Model.MemberPermission();
                    c.UID = uid;
                    c.IsMoneyAndShipping = true;//款到发货权限
                    //if (int.Parse(DropDownList2.SelectedValue) == 0 && modelInfo.ParentId > 0 && 1 == BuyerLib.GetGSP(modelInfo.ParentId))
                    //{
                    //    modelInfo = bllInfo.GetModel(modelInfo.UID);
                    //    int Member_Class = modelInfo.Member_Class;
                    //    //SOSOshop.BLL.Service.MemberInfo.GetErp_KeHuLB(modelInfo.Code, ref Member_Class, ref Crm_Class);
                    //    if (Member_Class == 0)
                    //    {
                    //        c.IsTrade = true;
                    //        c.IsLookPrice_01 = true;
                    //        c.IsLookProduct_01 = true;
                    //        c.IsLookPrice_02 = false;
                    //        c.IsLookProduct_02 = false;
                    //    }
                    //    else if (Member_Class == 1)
                    //    {
                    //        c.IsTrade = true;
                    //        c.IsLookPrice_01 = false;
                    //        c.IsLookProduct_01 = false;
                    //        c.IsLookPrice_02 = true;
                    //        c.IsLookProduct_02 = true;
                    //    }
                    //}
                    ok = cBll.Add(c);
                }
                //已经通过审核,同步CRM,ERP
                if (ok && int.Parse(DropDownList2.SelectedValue) == 0 && modelInfo.ParentId > 0)
                {
                    //同步CRM
                    CrmActionHandle(modelInfo);
                }
                if (ok)
                {
                    this.ltlMsg.Text = "保存成功·<script>if(confirm('添加成功！继续添加请点击确定。')){location.href='Buyer_edit.aspx?act=new';}else{location.href='BuyerLib.aspx';}</script>";
                    this.pnlMsg.Visible = true;
                    this.pnlMsg.CssClass = "actionOk";
                }
                else
                {
                    this.ltlMsg.Text = "添加买家资料失败！";
                    this.pnlMsg.Visible = true;
                    this.pnlMsg.CssClass = "actionErr";
                    return;
                }
                #endregion

                //发送短信通知已经通过审核
                if (model.State == 0)
                {
                    if (CheckBox1.ToolTip != "0" && CheckBox1.Checked)
                    {
                        string msg = "恭喜你已经通过管理员审核，请凭手机号登录，登录密码为" + password + "。";
                        SOSOshop.BLL.Sms.SendAndSaveDataBase(txtMobilePhone.Text.Trim(), msg, "系统", txtMobilePhone.Text.Trim());
                    }
                }
            }
            else
            {
                this.ltlMsg.Text = "添加买家资料失败！";
                this.pnlMsg.Visible = true;
                this.pnlMsg.CssClass = "actionErr";
                return;
            }
        }
        #endregion

        /// <summary>
        /// 同步CRM
        /// </summary>
        /// <param name="model">买家信息Model</param>
        private bool CrmActionHandle(SOSOshop.Model.MemberInfo model)
        {
            return true;
        }

        /// <summary>
        /// 永久有效
        /// </summary>
        public DateTime Date_rgPeriodOfValidity { get { return DateTime.Parse("2999-12-30"); } }

        public EventHandler ddl_Editer_DataBinding { get; set; }
    }
}
