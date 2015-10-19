﻿using System;
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
using SOSOshop.BLL;


namespace _101shop.admin.v3.member
{
    public partial class MemberIntegralRank : System.Web.UI.Page
    {
        //查看所有客户的积分
        public bool seeAll = false;
        public SOSOshop.BLL.Integral.MemberIntegralRank bll = new SOSOshop.BLL.Integral.MemberIntegralRank();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                SOSOshop.BLL.PromptInfo.Popedom("008011003");
                GetList();
            }
        }

        private void GetList()
        {
            //查看所有客户的积分
            seeAll = SOSOshop.BLL.PowerPass.isPass("008011015");
            if (seeAll) SelectEditer();
            DropDownList1.DataSource = SOSOshop.Model.CompanyClass.GetList();
            DropDownList1.DataTextField = "CompanyClassName";
            DropDownList1.DataValueField = "CompanyClassName";
            DropDownList1.DataBind();
            var li = new ListItem() { Text = "请选择", Value = "" };
            DropDownList1.Items.Insert(0, li);

            #region where
            string where = ""; string date = "";
            string truename = Request["truename"];
            if (!string.IsNullOrEmpty(truename))
            {
                this.TextBox_truename.Text = truename;
                where += " and truename like '%" + truename.Replace("'", "''").Replace("%", "") + "%' ";
            }
            string CompanyName = Request["CompanyName"];
            if (!string.IsNullOrEmpty(CompanyName))
            {
                this.TextBox_CompanyName.Text = CompanyName;
                where += " and CompanyName like '%" + CompanyName.Replace("'", "''").Replace("%", "") + "%' ";
            }
            string phone = Request["phone"];
            if (!string.IsNullOrEmpty(phone))
            {
                this.TextBox_phone.Text = phone;
                where += " and phone like '%" + phone.Replace("'", "''").Replace("%", "") + "%' ";
            }
            string lx = Request["DropDownList1"];
            if (!string.IsNullOrEmpty(lx))
            {
                this.DropDownList1.SelectedValue = lx;
                where += " and CompanyClass = '" + lx.Replace("'", "''") + "' ";
            }
            string fromDate = Request["fromDate"];
            if (!string.IsNullOrEmpty(fromDate))
            {
                this.fromDate.Text = fromDate;
                date += " and convert(char(10),c.created,120)>='" + fromDate.Replace("'", "''") + "' ";
            }
            string toDate = Request["toDate"];
            if (!string.IsNullOrEmpty(toDate))
            {
                this.toDate.Text = toDate;
                date += " and convert(char(10),c.created,120)<='" + toDate.Replace("'", "''") + "' ";
            }
            SOSOshop.Model.AdminInfo adminInfo = SOSOshop.BLL.AdministrorManager.Get();
            this.CheckBox1.Checked = !string.IsNullOrEmpty(Request["CheckBox1"]) && Request["CheckBox1"] == "1";
            this.CheckBox1.Visible = seeAll;
            string Editer = Request["Editer"];
            if (this.CheckBox1.Checked || !seeAll)
            {
                where += " and UID IN (SELECT UID FROM memberinfo WHERE Editer=" + adminInfo.AdminId + ") ";
            }
            else if (!string.IsNullOrEmpty(Editer) && seeAll)
            {
                this.ddlEditer.SelectedValue = Editer;
                where += " and UID IN (SELECT UID FROM memberinfo WHERE Editer=" + Editer + ") ";
            }
            #endregion

            int pageindex = 1; int.TryParse(Request["current"], out pageindex);
            int pagesize = 15;
            string sort = Request["sort"];//排序
            if (string.IsNullOrEmpty(sort) || (sort.Contains("asc") == false && sort.Contains("desc") == false)) sort = "integral DESC";
            Repeater1.DataSource = date != "" ? bll.GetList(where, date, sort, pageindex, pagesize) : bll.GetList(where, sort, pageindex, pagesize);
            Repeater1.DataBind();
            int recordcount = date != "" ? bll.GetListCount(where, date) : bll.GetListCount(where);
            page(recordcount, pageindex, pagesize);//分页
        }

        /// <summary>
        /// 查询交易员
        /// </summary>
        protected void SelectEditer()
        {
            SOSOshop.BLL.MemberInfo mbll = new SOSOshop.BLL.MemberInfo();
            string sqlText5 = "SELECT DISTINCT adminid AS id,name FROM yxs_administrators WHERE name<>'admin'";
            DataTable dt = mbll.ExecuteTableForCache(sqlText5);
            if (dt != null && dt.Rows.Count > 0)
            {
                sqlText5 = "SELECT ',' + REPLACE(REPLACE(STUFF((SELECT CAST(i AS varchar) + ',' AS a  FROM (SELECT DISTINCT b.name AS i FROM memberinfo a INNER JOIN yxs_administrators b ON a.Editer=b.adminid) AS t FOR XML path('')), 1, 0, ''), '<a>', ''), '</a>', '')";
                string _101Admin = Convert.ToString(mbll.ExecuteScalarForCache(sqlText5));
                dt.Columns.Add("OK", typeof(int));
                sqlText5 = "SELECT ',' + REPLACE(REPLACE(STUFF((SELECT CAST(i AS varchar) + ',' AS a  FROM (SELECT DISTINCT LTRIM(RTRIM(zhiyname)) AS i FROM zhiyzl WHERE beactive='是' AND issp='是' AND jigid='000') AS t FOR XML path('')), 1, 0, ''), '<a>', ''), '</a>', '')";
                //DbBase db2 = new DbBase(); db2.ChangeDB("ConnectionStringERP");
                //string ErpAdmin = Convert.ToString(db2.ExecuteScalarForCache(sqlText5));
                //foreach (DataRow dr in dt.Rows)
                //{
                //    //处理Erp中有没有
                //    if (!_101Admin.Contains("," + dr[1] + ",") && !ErpAdmin.Contains("," + dr[1] + ",")) dr.Delete();
                //}
                ddlEditer.Items.Clear();
                ddlEditer.DataSource = dt;
                ddlEditer.DataBind();
            }
            ListItem li = new ListItem("全部人员", "");
            ddlEditer.Items.Insert(0, li);
            li = new ListItem("无", "0");
            ddlEditer.Items.Add(li);
        }

        public void page(int recordcount, int pageindex, int pagesize)
        {
            string param = "&truename=" + Request["truename"] + "&CompanyName=" + Request["CompanyName"] + "&phone=" + Request["phone"] + "&DropDownList1=" + Request["DropDownList1"] + "&fromDate=" + Request["fromDate"] + "&toDate=" + Request["toDate"] + "&CheckBox1=" + Request["CheckBox1"] + "&Editer=" + Request["Editer"];

            double cs = (int)recordcount / pagesize;
            //页总数
            int pagecount = (recordcount % pagesize == 0 ? 0 : 1) + int.Parse(Math.Floor(cs).ToString());
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.Append("共<span style='color: Red'>" + recordcount + "</span>条记录");
            s.Append("<a href=\"?current=1" + param + "\">");
            s.Append("<<");
            s.Append("</a> ");
            int j, i;
            j = i = 0;
            if (pageindex > 5)
            {
                i = pageindex - 5;
                j = i;
            }
            for (; i < j + 9 && i < pagecount; i++)
            {
                s.Append("<a href=\"?current=" + (i + 1) + param +
                    (pageindex == i + 1 ? "\" style=\"color:Red" : "")
                    + "\">");
                s.Append(i + 1);
                s.Append("</a> ");
            }
            s.Append("<a href=\"?current=" + pagecount + param + "\">");
            s.Append(">>");
            s.Append("</a> ");

            pages.Text = s.ToString();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            GetList();
        }
    }
}
