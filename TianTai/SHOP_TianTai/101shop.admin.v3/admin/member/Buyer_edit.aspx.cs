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
        /// ʡ/��/��
        /// </summary>
        public string ConsigneeBorough = "", ConsigneeCity = "", ConsigneeProvince = "";
        /// <summary>
        /// ��Ҳ�ѯ
        /// </summary>
        private SOSOshop.BLL.MemberInfo mbll = new SOSOshop.BLL.MemberInfo();

        /// <summary>
        /// ��ȡ����Ա���ƹ���Ա��ɫidֵ
        /// </summary>
        //string jyyId = "33";
        //string tgryId = "37";
        bool enabledErpAction = false;

        #region ͳ��ʱ��(����)
        public DateTime _StatisticalTime = DateTime.Now;//��ʼʱ��
        public int[] StatisticalTime = new int[3];//��ʼ��,��������,����ҳ��
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
                    SOSOshop.BLL.PromptInfo.Popedom("000000000000", "�Բ�����û�в鿴��Ȩ��!");
                }
                DropDownList1.DataSource = SOSOshop.Model.CompanyClass.GetList();
                DropDownList1.DataTextField = "CompanyClassName";
                DropDownList1.DataValueField = "CompanyClassName";
                DropDownList1.DataBind();

                //�󶨼۸�����
                SOSOshop.BLL.DbBase db = new DbBase();
                db.ChangeDataCenter();
                DropDownList3.DataSource = db.ExecuteTable("SELECT DISTINCT category from dbo.Price");
                DropDownList3.DataTextField = "category";
                DropDownList3.DataBind();
                DropDownList3.Items.Insert(0, new ListItem() { Text = "��ѡ��", Value = "" });

                if (ChangeHope.WebPage.PageRequest.GetQueryInt("uid") > 0)
                {
                    int memberId = ChangeHope.WebPage.PageRequest.GetQueryInt("uid");
                    ViewState["memberId"] = memberId;
                    GetAccountAndInfo(memberId);
                    SelectEditer();
                }
                else
                {
                    string p = "<span id=\"spanParents\"><span><br>&nbsp;<a href=\"javascript:void(0)\" title=\"������������λ\" onclick=\"addInc(this)\">���</a>"
                    + "<span>&nbsp;<input name=\"ParentIncName\" type=\"text\" value=\"\" position=\"{x:235,y:110}\" onclick=\"selectParentWindow(this)\" style=\"height:18px;width:300px;cursor:pointer;color:black;\">"
                    + "<input type=\"hidden\" name=\"ParentId\" value=\"0\">"
                    + "</span></span></span>";
                    //this.DropDownList2.Items[1].Enabled = false;
                    //this.DropDownList2.Items[2].Enabled = false;
                    SelectEditer(); // ��ѯ����Ա
                    BindOutSellPerson();//��������Ա
                }
            }
            //������Ȩ��
            bool isCheckUp = SOSOshop.BLL.PowerPass.isPass("008009013");
            cb_resetPwd.Visible = isCheckUp;
            CheckUp_Div.Visible = isCheckUp;
        }

        /// <summary>
        /// ��������Ա
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

            ListItem li = new ListItem("ȫ����Ա", "0");
            ddlOSP.Items.Insert(0, li);
        }

        /// <summary>
        /// ��ѯ����Ա
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
                //sqlText5 = "SELECT ',' + REPLACE(REPLACE(STUFF((SELECT CAST(i AS varchar) + ',' AS a  FROM (SELECT DISTINCT LTRIM(RTRIM(zhiyname)) AS i FROM zhiyzl WHERE beactive='��' AND issp='��' AND jigid='000') AS t FOR XML path('')), 1, 0, ''), '<a>', ''), '</a>', '')";
                //DbBase db2 = new DbBase(); db2.ChangeDB("ConnectionStringERP");
                //string ErpAdmin = Convert.ToString(db2.ExecuteScalarForCache(sqlText5));
                //foreach (DataRow dr in dt.Rows)
                //{
                //    //����Erp����û��
                //    if (!_101Admin.Contains("," + dr[1] + ",") && !ErpAdmin.Contains("," + dr[1] + ",")) dr.Delete();
                //}
                ddl_Editer.Items.Clear();
                ddl_Editer.DataSource = dt;
                ddl_Editer.DataBind();
            }
            ListItem li = new ListItem("ȫ����Ա", "0");
            ddl_Editer.Items.Insert(0, li);
        }
        protected void SelectDropDownList(DropDownList ddl, string value)
        {
            foreach (ListItem item in ddl.Items) { item.Selected = false; }
            foreach (ListItem item in ddl.Items) { if (item.Value == value) { item.Selected = true; break; } }
        }

        #region �󶨻�����Ϣ
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
                #region ��Ա���״̬
                DropDownList2.SelectedValue = model.State.ToString();
                DropDownList2.Enabled = SOSOshop.BLL.PowerPass.isPass("008009013");//Ȩ��


                CheckBox1.ToolTip = DropDownList2.SelectedValue;
                if (model.State == 1 && infomodel.Editer == adminInfo.AdminId)
                {
                    CheckBox1.Visible = CheckBox1.Enabled = CheckBox1.Checked = true;
                }
                #endregion
                DropDownList1.SelectedValue = model.CompanyClass;
                //�޸�Ȩ��
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
                lblUserLevel.Text = mrmodel != null ? mrmodel.Name : "δ֪";//��ҵȼ�
                txtMobilePhone.Text = model.MobilePhone;
                txtEmail.Text = model.Email;//�����ʼ�
                txtEmail_QQ.Text = model.Email_QQ;//QQ����
                //��½��Ч��
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

                string p = "<span id=\"spanParents\" style=\"\"><span><br>&nbsp;<a href=\"javascript:void(0)\" title=\"������������λ\" onclick=\"addInc(this)\">���</a>"
                   + "<span>&nbsp;<input name=\"ParentIncName\" type=\"text\" value=\"\" position=\"{x:235,y:110}\" onclick=\"selectParentWindow(this)\" style=\"height:18px;width:300px;cursor:pointer;color:black;\">"
                   + "<input type=\"hidden\" name=\"ParentId\" value=\"0\">"
                   + "</span></span></span>";
                this.txtUId.Value = id.ToString();
                this.txtTrueName.Text = infomodel.TrueName;
                this.txtAddress.Text = infomodel.Address;
                this.txtOfficePhone.Text = infomodel.OfficePhone;
                this.txtFax.Text = infomodel.Fax;

                #region ʵ����ʡ��������
                DataSet dsProvinces = bll.ExecuteDataSet("select isnull((select TOP(1) Name from Region where ID=" + infomodel.Province + "),'') as a,isnull((select TOP(1) Name from Region where ID=" + infomodel.City + "),'') as b,isnull((select TOP(1) Name from Region where ID=" + infomodel.Borough + "),'') as c");
                if (dsProvinces != null && dsProvinces.Tables.Count > 0 && dsProvinces.Tables[0].Rows.Count > 0)
                {
                    ConsigneeProvince = dsProvinces.Tables[0].Rows[0][0].ToString();
                    ConsigneeCity = dsProvinces.Tables[0].Rows[0][1].ToString();
                    ConsigneeBorough = dsProvinces.Tables[0].Rows[0][2].ToString();
                }
                #endregion

                #region ���������λ

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

                SelectEditer(); // ��ѯ����Ա
                SelectDropDownList(ddl_Editer, infomodel.Editer.ToString());
                BindOutSellPerson(); // ��ѯ������Ա
                SelectDropDownList(ddlOSP, infomodel.OSPId.ToString());
                if (infomodel.Editer <= 0)
                {

                    ddl_Editer.Visible = ddl_Editer.Enabled = true; tipEditer.Visible = true;
                    lblEditer.Text = "";
                }
                else if (ddl_Editer.SelectedIndex > 0)
                {
                    //δ���ʱ
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
                    lblEditer.Text = "�ȴ�����ͷ�...";
                }
                if (int.Parse(DropDownList2.SelectedValue) != 1 && ddlOSP.SelectedIndex > 0)
                {
                    ddlOSP.Visible = ddlOSP.Enabled = true; tipOSP.Visible = true;
                    if (ddlOSP.SelectedIndex > 0) lbOSP.Text = ddlOSP.SelectedItem.Text;
                }
            }
        }

        /// <summary>
        /// ��Ч��ѡ�񣬷��ʼ� for js @yangzhou
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
        /// ����ʱ�����жϸ�ʽ��ֵ
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
                if (time.Trim().IndexOf("����") != -1)
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
                            label.Text += " <font color=\"green\" style=\"line-height:200%;\">��Ҫ����</font> ";
                        }
                    }
                    else
                    {
                        label.Text += " <font color=\"red\" style=\"line-height:200%;\">�Ѿ�����</font> ";
                    }
                    return dt.ToString("yyyy-MM-dd");
                }
                catch { }
                return "";
            }
        }
        #endregion

        #region ����
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SOSOshop.BLL.PromptInfo.Popedom("008009004", "�Բ�����û��Ȩ�޽��б༭");
            SOSOshop.BLL.MemberInfo bll = new SOSOshop.BLL.MemberInfo();
            int uid = ChangeHope.WebPage.PageRequest.GetQueryInt("uid");
            bool edit = (uid > 0);
            //try
            //{
            //if (ChangeHope.WebPage.PageRequest.GetFormString("ParentId").Replace("0,", "").Trim(',') == "")
            //{
            //    this.ltlMsg.Text = "����ʧ��" + "\r\n�ϼ���λ����Ϊ�գ�";
            //    this.pnlMsg.Visible = true;
            //    this.pnlMsg.CssClass = "actionErr";
            //    return;
            //}
            if (this.txtTrueName.Text.Trim() == "")
            {
                this.ltlMsg.Text = "����ʧ��" + "\r\n��ϵ�˲���Ϊ�գ�";
                this.pnlMsg.Visible = true;
                this.pnlMsg.CssClass = "actionErr";
                return;
            }
            if (this.txtMobilePhone.Text.Trim() == "")
            {
                this.ltlMsg.Text = "����ʧ��" + "\r\n�ֻ��Ų���Ϊ�գ�";
                this.pnlMsg.Visible = true;
                this.pnlMsg.CssClass = "actionErr";
                return;
            }
            else if (!Regex.IsMatch(this.txtMobilePhone.Text.Trim(), @"^[0-9\-/ ]+$", RegexOptions.IgnoreCase))
            {
                this.ltlMsg.Text = "����ʧ��" + "\r\n�ֻ�����д����";
                this.pnlMsg.Visible = true;
                this.pnlMsg.CssClass = "actionErr";
                return;
            }
            else if ((!edit && bll.ExecuteScalar("select 1 from memberaccount where MobilePhone like '" + this.txtMobilePhone.Text.Trim() + "%'") != null)
                || (edit && bll.ExecuteScalar("select 1 from memberaccount where MobilePhone like '" + this.txtMobilePhone.Text.Trim() + "%' and UID!=" + uid) != null))
            {
                this.ltlMsg.Text = "����ʧ��" + "\r\n�ֻ�����д���󣡴��ֻ����Ѿ���ʹ�ã����������д��ȷ��";
                this.pnlMsg.Visible = true;
                this.pnlMsg.CssClass = "actionErr";
                return;
            }
            if (this.txtEmail.Text.Trim() != string.Empty)
            {
                if (!Regex.IsMatch(this.txtEmail.Text.Trim(), @"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$", RegexOptions.IgnoreCase))
                {
                    this.ltlMsg.Text = "����ʧ��" + "\r\n������д����";
                    this.pnlMsg.Visible = true;
                    this.pnlMsg.CssClass = "actionErr";
                    return;
                }
                else if (bll.ExecuteScalar("select 1 from memberaccount where Email = '" + this.txtEmail.Text.Trim() + "' and UID!=" + uid) != null)
                {
                    this.ltlMsg.Text = "����ʧ��" + "\r\n������д���󣡴������Ѿ���ʹ�ã����������д��ȷ��";
                    this.pnlMsg.Visible = true;
                    this.pnlMsg.CssClass = "actionErr";
                    return;
                }
            }
            if (this.txtEmail_QQ.Text.Trim() != string.Empty)
            {
                if (!Regex.IsMatch(this.txtEmail_QQ.Text.Trim(), @"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$", RegexOptions.IgnoreCase))
                {
                    this.ltlMsg.Text = "����ʧ��" + "\r\n������д����";
                    this.pnlMsg.Visible = true;
                    this.pnlMsg.CssClass = "actionErr";
                    return;
                }
                else if (bll.ExecuteScalar("select 1 from memberaccount where Email_QQ = '" + this.txtEmail_QQ.Text.Trim() + "' and UID!=" + uid) != null)
                {
                    this.ltlMsg.Text = "����ʧ��" + "\r\n������д���󣡴������Ѿ���ʹ�ã����������д��ȷ��";
                    this.pnlMsg.Visible = true;
                    this.pnlMsg.CssClass = "actionErr";
                    return;
                }
            }
            //if (ddl_Editer.SelectedValue == "0" || ddl_Editer.SelectedValue == "")
            //{
            //    this.ltlMsg.Text = "����ʧ��" + "\r\n��ѡ������Ա���ٱ��棡";
            //    this.pnlMsg.Visible = true;
            //    this.pnlMsg.CssClass = "actionErr";
            //    return;
            //}
            //if (CRM_InterunitStyle_ID.Value == "0" || CRM_InterunitStyle_ID.Value == "")
            //{
            //    this.ltlMsg.Text = "����ʧ��" + "\r\n��ѡ��CRM�ͻ�������ٱ��棡";
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
                this.ltlMsg.Text = "����ʧ��" + "\r\nû��ѡ��ʡ�ݳ��У����飡";
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
                    if (ml.isAllow(uid, MemberIntegralTemplateEnum.����ͨ��))
                    {
                        //ע���ͻ���(�����ɹ��ſ�ʼ�ͻ�Ա����)
                        bllmi.AddIntegral(uid, 0, SOSOshop.BLL.Integral.MemberIntegralTemplateEnum.��Աע��, "");
                        bllmi.AddIntegral(uid, 0, MemberIntegralTemplateEnum.����ͨ��, "");
                    }
                }
                #region ��̨�û�������־��¼
                SOSOshop.Model.AdminInfo adminInfo = SOSOshop.BLL.AdministrorManager.Get();
                SOSOshop.BLL.Logs.Log.LogAdminAdd("�޸������Ϣ", (adminInfo == null ? 0 : adminInfo.AdminId), (adminInfo == null ? "" : adminInfo.AdminName), 1);
                #endregion
                #region �������
                SOSOshop.BLL.DbBase db1 = new SOSOshop.BLL.DbBase(); db1.ClearCache();
                #endregion
            }
            else
            {
                AddAccount();
                #region ��̨�û�������־��¼
                SOSOshop.Model.AdminInfo adminInfo = SOSOshop.BLL.AdministrorManager.Get();
                SOSOshop.BLL.Logs.Log.LogAdminAdd("��������Ϣ", (adminInfo == null ? 0 : adminInfo.AdminId), (adminInfo == null ? "" : adminInfo.AdminName), 1);
                #endregion
            }
            //}
            //catch (Exception ex)
            //{

            //    this.ltlMsg.Text = (edit ? "�༭" : "���") + "�������ʧ��" + "\r\n" + ex.Message;
            //    this.pnlMsg.Visible = true;
            //    this.pnlMsg.CssClass = "actionErr";
            //}
        }
        /// <summary>
        /// �޸��˻�
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
                    if (DropDownList1.SelectedValue == "������ҵ" || DropDownList1.SelectedValue == "��ҵ��˾" || DropDownList1.SelectedValue == "��ӪҽԺ")
                    {
                        model.Member_Class = 0;
                    }
                    else
                    {
                        model.Member_Class = 1;
                    }


                    //д�����ݿ�
                    bool ok = bll.Update(model, enabledErpAction && int.Parse(DropDownList2.SelectedValue) == 0/* && model.ParentId > 0*/);
                }
                this.ltlMsg.Text = "����ɹ���<script>if(confirm('�༭�ɹ��������༭����ȷ����')){location.href='Buyer_edit.aspx?type=lib&uid=" + ViewState["memberId"] + "&act=new';}else{location.href='BuyerLib.aspx';}</script>";
                this.pnlMsg.Visible = true;
                this.pnlMsg.CssClass = "actionOk";
            }

        }
        /// <summary>
        /// �޸��˻�����
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
                            this.ltlMsg.Text = "����ʧ�ܣ��һ���������ѡ�������ѡ��һ�����⣡";
                            this.pnlMsg.Visible = true;
                            this.pnlMsg.CssClass = "actionErr";
                            return false;
                        }
                        if (Answer == "")
                        {
                            this.ltlMsg.Text = "����ʧ�ܣ��һ��������д���󣬲���Ϊ�գ�";
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
                            this.ltlMsg.Text = "����ʧ�ܣ�ԭ�һ���������ѡ�����";
                            this.pnlMsg.Visible = true;
                            this.pnlMsg.CssClass = "actionErr";
                            return false;
                        }
                        if (oldAnswer == "" || oldAnswer != model.Answer)
                        {
                            this.ltlMsg.Text = "����ʧ�ܣ�ԭ�һ��������д����";
                            this.pnlMsg.Visible = true;
                            this.pnlMsg.CssClass = "actionErr";
                            return false;
                        }
                        if (newAnswer == "")
                        {
                            this.ltlMsg.Text = "����ʧ�ܣ����һ�����𰸲���Ϊ�գ�";
                            this.pnlMsg.Visible = true;
                            this.pnlMsg.CssClass = "actionErr";
                            return false;
                        }
                        model.Answer = newAnswer;
                        this.palOld.CssClass = "msgNormal";
                    }
                }
                model.State = int.Parse(DropDownList2.SelectedValue);//Ĭ�� ͨ����� ע��Ͷ��� 2 ��һ�������᲻�ܵ�½��
                enabledErpAction = model.State != 2;//ֻͬ����˵����
                accountBll.Update(model);
                //���Ͷ���֪ͨ�Ѿ�ͨ�����
                if (model.State == 0)
                {
                    if (CheckBox1.ToolTip != "0" && CheckBox1.Checked)
                    {
                        string msg = "��ϲ���Ѿ�ͨ����ˣ���ƾ�ֻ��ŵ�¼" + (password == resetPwd ? "����¼����Ϊ" + password : "");
                        SOSOshop.BLL.Sms.SendAndSaveDataBase(txtMobilePhone.Text.Trim(), msg, "ϵͳ", txtMobilePhone.Text.Trim());
                    }
                }
                return true;
            }
            return false;
        }

        #region ��֤����Ƿ����
        /// <summary>
        /// ����Ƿ����
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
                        RegStr = "�����õ������Ϊ�Ƿ��������";
                        break;
                    }
                }
            }

            if (bll.Exists(parm))
            {
                RegStr = "�����õ�������Ѵ��ڣ����������������";
            }
            return RegStr;
        }
        /// <summary>
        /// ��������Ƿ����
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
                        RegStr = "�������а����Ƿ��ַ���";
                        break;
                    }
                }
            }

            if (bll.ExistEmail(parm))
            {
                RegStr = "������ĵ��������Ѵ��ڣ���������������䣡";
            }
            return RegStr;
        }

        /// <summary>
        /// ���QQ�����Ƿ����
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
                        RegStr = "�������а����Ƿ��ַ���";
                        break;
                    }
                }
            }

            if (bll.ExistEmail_QQ(parm))
            {
                RegStr = "�������QQ�����Ѵ��ڣ����������µ�QQ���䣡";
            }
            return RegStr;
        }

        /// <summary>
        /// �����˺ŵõ�����ID
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
        /// ����˻�
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
                        this.ltlMsg.Text = "����ʧ�ܣ��һ���������ѡ�������ѡ��һ�����⣡";
                        this.pnlMsg.Visible = true;
                        this.pnlMsg.CssClass = "actionErr";
                        return;
                    }
                    if (Answer == "")
                    {
                        this.ltlMsg.Text = "����ʧ�ܣ��һ��������д���󣬲���Ϊ�գ�";
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
                        this.ltlMsg.Text = "����ʧ�ܣ�ԭ�һ���������ѡ�����";
                        this.pnlMsg.Visible = true;
                        this.pnlMsg.CssClass = "actionErr";
                        return;
                    }
                    if (oldAnswer == "" || oldAnswer != model.Answer)
                    {
                        this.ltlMsg.Text = "����ʧ�ܣ�ԭ�һ��������д����";
                        this.pnlMsg.Visible = true;
                        this.pnlMsg.CssClass = "actionErr";
                        return;
                    }
                    if (newAnswer == "")
                    {
                        this.ltlMsg.Text = "����ʧ�ܣ����һ�����𰸲���Ϊ�գ�";
                        this.pnlMsg.Visible = true;
                        this.pnlMsg.CssClass = "actionErr";
                        return;
                    }
                    model.Answer = newAnswer;
                    this.palOld.CssClass = "msgNormal";
                }
            }
            model.State = int.Parse(DropDownList2.SelectedValue);//Ĭ�� ͨ����� ע��Ͷ��� 2 ��һ�������᲻�ܵ�½��
            int uid = accountBll.Add(model);
            model.UID = uid;
            if (uid > 0)
            {

                #region ��ӻ��ּ�¼��ȡ���˹��ܣ�
                //YXShop.Model.Member.Integral modelInte = new YXShop.Model.Member.Integral();
                //YXShop.BLL.Member.Integral bllInte = new YXShop.BLL.Member.Integral();
                //modelInte.Userid = GetIdByUserId(model.UserId);
                //modelInte.IntegralClass = 3;
                //modelInte.Origin = "ע������";
                //modelInte.IntegralNum = model.Points;
                //modelInte.GainDate = DateTime.Now;
                //modelInte.NoteDate = DateTime.Now;
                //modelInte.NoteName = "ϵͳ�Զ���¼";
                //modelInte.Remark = "ע�����ʱ���͵Ļ���";
                //modelInte.IntegralStatus = 0;
                //try
                //{
                //    bllInte.Add(modelInte);
                //}
                //catch
                //{
                //    this.ltlMsg.Text = "����������ʧ�ܣ�";
                //    this.pnlMsg.Visible = true;
                //    this.pnlMsg.CssClass = "actionErr";
                //    return;
                //}
                #endregion

                #region ��ӵ���¼��ȡ���˹��ܣ�
                //YXShop.Model.Member.UserInfoNote modelNote = new YXShop.Model.Member.UserInfoNote();
                //YXShop.BLL.Member.UserInfoNote bllNote = new YXShop.BLL.Member.UserInfoNote();
                //modelNote.UserID = GetIdByUserId(model.UserId);
                //modelNote.TicketCount = 0;
                //modelNote.Causation = "ע�����͵ĵ��";
                //modelNote.BosomNote = "ע�����͵ĵ��";
                //modelNote.NoteDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                //modelNote.NoteName = "ϵͳ�Զ���¼";
                //modelNote.NoteType = 0;
                //modelNote.BuckleOrAdd = 0;
                //modelNote.Username = model.UserId;
                //try
                //{
                //    bllNote.Add(modelNote);
                //}
                //catch
                //{
                //    this.ltlMsg.Text = "����������ʧ�ܣ�";
                //    this.pnlMsg.Visible = true;
                //    this.pnlMsg.CssClass = "actionErr";
                //    return;
                //}
                #endregion

                #region ��Ӹ�����Ϣ
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
                //���Ȩ��
                if (ok)
                {
                    SOSOshop.BLL.MemberPermission cBll = new SOSOshop.BLL.MemberPermission();
                    SOSOshop.Model.MemberPermission c = new SOSOshop.Model.MemberPermission();
                    c.UID = uid;
                    c.IsMoneyAndShipping = true;//�����Ȩ��
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
                //�Ѿ�ͨ�����,ͬ��CRM,ERP
                if (ok && int.Parse(DropDownList2.SelectedValue) == 0 && modelInfo.ParentId > 0)
                {
                    //ͬ��CRM
                    CrmActionHandle(modelInfo);
                }
                if (ok)
                {
                    this.ltlMsg.Text = "����ɹ���<script>if(confirm('��ӳɹ��������������ȷ����')){location.href='Buyer_edit.aspx?act=new';}else{location.href='BuyerLib.aspx';}</script>";
                    this.pnlMsg.Visible = true;
                    this.pnlMsg.CssClass = "actionOk";
                }
                else
                {
                    this.ltlMsg.Text = "����������ʧ�ܣ�";
                    this.pnlMsg.Visible = true;
                    this.pnlMsg.CssClass = "actionErr";
                    return;
                }
                #endregion

                //���Ͷ���֪ͨ�Ѿ�ͨ�����
                if (model.State == 0)
                {
                    if (CheckBox1.ToolTip != "0" && CheckBox1.Checked)
                    {
                        string msg = "��ϲ���Ѿ�ͨ������Ա��ˣ���ƾ�ֻ��ŵ�¼����¼����Ϊ" + password + "��";
                        SOSOshop.BLL.Sms.SendAndSaveDataBase(txtMobilePhone.Text.Trim(), msg, "ϵͳ", txtMobilePhone.Text.Trim());
                    }
                }
            }
            else
            {
                this.ltlMsg.Text = "����������ʧ�ܣ�";
                this.pnlMsg.Visible = true;
                this.pnlMsg.CssClass = "actionErr";
                return;
            }
        }
        #endregion

        /// <summary>
        /// ͬ��CRM
        /// </summary>
        /// <param name="model">�����ϢModel</param>
        private bool CrmActionHandle(SOSOshop.Model.MemberInfo model)
        {
            return true;
        }

        /// <summary>
        /// ������Ч
        /// </summary>
        public DateTime Date_rgPeriodOfValidity { get { return DateTime.Parse("2999-12-30"); } }

        public EventHandler ddl_Editer_DataBinding { get; set; }
    }
}
