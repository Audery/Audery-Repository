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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Caching;
using YXShop.Common;
using System.Data.SqlClient;
using SOSOshop.BLL;
using System.Text;

namespace _101shop.admin.v3.member
{
    public partial class BuyerLib : SOSOshop.WEB.UI.ManageBasePage
    {
        /// <summary>
        /// 查询商城数据库
        /// </summary>
        SOSOshop.BLL.Db bll = new SOSOshop.BLL.Db();
        string columnNames = "a.LoginCount,a.UserType, a.UserGroup, a.UserId, a.MobilePhone, a.Email, a.Email_QQ, a.PassWord, a.Question, a.Answer, a.State, a.RegisterDate, a.RegisterIP, a.Capital, a.Coupons, a.Points, a.PeriodOfValidity, a.CompanyClass, b.* ";
        string tableNames = "memberaccount a inner join memberinfo b on a.UID=b.UID";
        /// <summary>
        /// 省/市/区
        /// </summary>
        public int province = 0, city = 0, county = 0, chk_no_province = 0;
        public int editer = 0;
        public int editer_xx = 0;

        SOSOshop.Model.AdminInfo aInfo = null;
        public bool act_enabled = true;
        public bool only_see_me = false;
        /// <summary>
        /// 买家审核权限
        /// </summary>
        public bool isCheckUp = false;
        public bool IsEdit = false;
        public bool IsDelete = false;
        public bool IsPresentIntegral = false;

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
            tablist.Sorting += new GridViewSortEventHandler(tablist_Sorting);
            if (!IsPostBack)
            {
                #region ajax
                if (ChangeHope.WebPage.PageRequest.GetFormString("Option") != string.Empty && ChangeHope.WebPage.PageRequest.GetFormString("id") != "")
                {
                    string types = Request["Option"].Trim();
                    string id = ChangeHope.WebPage.PageRequest.GetFormString("id");
                    string ids = ChangeHope.WebPage.PageRequest.GetFormString("ids").Trim(',');
                    int uid = 0; int.TryParse(id, out uid);
                    //获取上级单位
                    if (types == "GetParentIncName")
                    {
                        if (!string.IsNullOrEmpty(ids))
                        {
                            StringBuilder s = new StringBuilder();
                            DataSet IncNames = bll.ExecuteDataSet("select ID, Name, isnull(BuyFilingStatus,2) from DrugsBase_Enterprise where ID in (" + ids + ")");
                            if (IncNames != null && IncNames.Tables.Count > 0 && IncNames.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr in IncNames.Tables[0].Rows)
                                {
                                    s.Append("{ID:" + dr[0] + ",Name:'" + dr[1].ToString().Replace("'", "\\'") + "',BuyFilingStatus:" + dr[2] + "},");
                                }
                            }
                            Response.Write("[" + s.ToString().TrimEnd(',') + "]");
                        }
                        else if (uid > 0)
                        {
                            string s = "";
                            object IncName = bll.ExecuteScalar("select Name from DrugsBase_Enterprise where ID='" + uid + "'");
                            if (IncName != null) s = IncName.ToString();
                            Response.Write(s);
                        }
                    }
                    //删除
                    else if (types == "del")
                    {
                        if (isDelete())
                        {
                            bool noBecause = ChangeHope.WebPage.PageRequest.GetFormString("noBecause") == "";
                            if (noBecause && bll.ExecuteScalar("select 1 from orders where ReceiverId in (" + id + ")") != null)
                            {
                                Response.Write("noBecauseOrders");
                            }
                            else
                            {
                                bool ok = false;
                                if (uid > 0)
                                {
                                    ok = new SOSOshop.BLL.MemberAccount().Delete(uid);
                                }
                                else
                                {
                                    ok = new SOSOshop.BLL.MemberAccount().DeleteAll(id);
                                }
                                Response.Write(ok ? "ok" : "");
                                if (ok)
                                {
                                    #region 后台用户操作日志记录
                                    SOSOshop.Model.AdminInfo adminInfo = SOSOshop.BLL.AdministrorManager.Get();
                                    SOSOshop.BLL.Logs.Log.LogAdminAdd("删除买家信息", (adminInfo == null ? 0 : adminInfo.AdminId), (adminInfo == null ? "" : adminInfo.AdminName), 1);
                                    #endregion
                                }
                            }
                        }
                        else
                        {
                            Response.Write("no");
                        }
                    }
                    //审核
                    else if (types == "State")
                    {
                        if (isEdit())
                        {
                            bool ok = 0 < bll.ExecuteNonQuery("UPDATE memberaccount SET State = 0 where State <> 0 and UID in (" + id + ")");
                            Response.Write(ok ? "ok" : "");
                        }
                        else
                        {
                            Response.Write("no");
                        }
                    }
                    //获取积分
                    else if (types == "getIntegral")
                    {
                        int integral = 0;
                        if (SOSOshop.BLL.PowerPass.isPass("008009001"))
                        {
                            try
                            {
                                SOSOshop.BLL.Integral.MemberIntegral bll2 = new SOSOshop.BLL.Integral.MemberIntegral();
                                integral = bll2.GetRealityIntegral(uid);
                            }
                            catch { }
                            Response.Write(integral);
                        }
                        else
                        {
                            Response.Write("no");
                        }
                    }
                    //赠送积分
                    else if (types == "editIntegral")
                    {
                        int integral = 0; int.TryParse(Request["integral"], out integral);
                        if (SOSOshop.BLL.PowerPass.isPass("008009015"))
                        {
                            bool ok = false;
                            try
                            {
                                SOSOshop.BLL.Integral.MemberIntegral bll2 = new SOSOshop.BLL.Integral.MemberIntegral();
                                SOSOshop.Model.AdminInfo adminInfo = SOSOshop.BLL.AdministrorManager.Get();
                                string remarks = string.Format("管理员{0}赠送积分", (adminInfo == null ? "" : adminInfo.AdminName));
                                bll2.PresentIntegral(uid, integral, remarks);
                                ok = true;
                                #region 后台用户操作日志记录
                                SOSOshop.BLL.Logs.Log.LogAdminAdd(remarks + "给" + Request["TrueName"] + "：" + integral, (adminInfo == null ? 0 : adminInfo.AdminId), (adminInfo == null ? "" : adminInfo.AdminName), 1);
                                #endregion
                            }
                            catch { }
                            Response.Write(ok ? "ok" : "");
                        }
                        else
                        {
                            Response.Write("no");
                        }
                    }
                    Response.End();
                    return;
                }
                #endregion
                if (!SOSOshop.BLL.PowerPass.isPass("008009001") && !SOSOshop.BLL.PowerPass.isPass("008009012"))
                {
                    SOSOshop.BLL.PromptInfo.Popedom("000000000000", "对不起，您没有查看的权限!");
                }
                //显示列表
                Search_Click(null, null);
            }
            //权限赋值
            isCheckUp = SOSOshop.BLL.PowerPass.isPass("008009013");
            IsEdit = SOSOshop.BLL.PowerPass.isPass("008009004");
            IsDelete = SOSOshop.BLL.PowerPass.isPass("008009003");
            IsPresentIntegral = SOSOshop.BLL.PowerPass.isPass("008009015");
        }

        /// <summary>
        /// 绑定外销人员（线下推广人员）
        /// </summary>
        protected void BindOutSellPerson()
        {
            SOSOshop.BLL.MemberInfo mbll = new SOSOshop.BLL.MemberInfo();
            string sql = @"SELECT DISTINCT a.name as ospname, adminid as ospid
                           FROM dbo.yxs_administrators AS a 
                                INNER JOIN yxs_role ON a.[role] LIKE '%' + (SELECT CAST(id AS VARCHAR(10)) WHERE yxs_role.name='外销人员') + '%' ";
            DataTable dt = mbll.ExecuteTableForCache(sql);

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
            DataTable dt = mbll.ExecuteTableForCache(sqlText5);
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
                ddlEditer.Items.Clear();
                ddlEditer.DataSource = dt;
                ddlEditer.DataBind();
            }
            ListItem li = new ListItem("全部人员", "");
            ddlEditer.Items.Insert(0, li);
            li = new ListItem("无", "0");
            ddlEditer.Items.Add(li);

            w_d_Member_Class.DataSource = SOSOshop.Model.CompanyClass.GetList();
            w_d_Member_Class.DataTextField = "CompanyClassName";
            w_d_Member_Class.DataValueField = "CompanyClassName";
            w_d_Member_Class.DataBind();
            li = new ListItem() { Text = "请选择", Value = "" };
            w_d_Member_Class.Items.Insert(0, li);            
        }

        protected string GetSqlWhere()
        {
            string where = "1=1";//定义查询条件
            //权限
            aInfo = SOSOshop.BLL.AdministrorManager.Get();
            if (!SOSOshop.BLL.PowerPass.isPass("008009001"))
            {
                this.only_see_me = true;
            }
            //省/市/区
            int.TryParse(Request["province"], out province);
            int.TryParse(Request["city"], out city);
            int.TryParse(Request["county"], out county);
            int.TryParse(Request["chk_no_province"], out chk_no_province);
            if (chk_no_province == 0)
            {
                if (province > 0)
                {
                    where += " and (province = '" + province + "')";
                }
                if (city > 0)
                {
                    where += " and (city = '" + city + "')";
                }
                if (county > 0)
                {
                    where += " and (Borough = '" + county + "')";
                }
            }
            else
            {
                where += " and (province = 0)";
            }
            //买家姓名
            string trueName = this.w_d_TrueName.Text.Trim();
            if (!string.IsNullOrEmpty(trueName))
            {
                where += string.Format(" and trueName like '%{0}%'", trueName.Replace("'", "''"));
            }
            //手机号
            string mobilePhone = this.w_l_MobilePhone.Text.Trim();
            if (!string.IsNullOrEmpty(mobilePhone))
            {
                where += string.Format(" and mobilePhone like '%{0}%'", mobilePhone.Replace("'", "''"));
            }
            //单位
            string parents = this.txtParents.Text.Trim();
            if (!string.IsNullOrEmpty(parents))
            {
                where += string.Format(" and ParentId in (select ID from DrugsBase_Enterprise where Name like '%{0}%')", parents.Replace("'", "''"));
            }         
            //会员类别
            string member_Class = this.w_d_Member_Class.SelectedValue.Trim();
            if (!string.IsNullOrEmpty(member_Class))
            {
                where += string.Format(" and CompanyClass = '{0}'", member_Class.Replace("'", "''"));
            }
            //状态
            if (!IsPostBack)
            {
                if (Request["State"] != null)
                {
                    w_d_State.SelectedValue = Request["State"];
                }
            }
            string state = w_d_State.SelectedValue.Trim();
            if (!string.IsNullOrEmpty(state))
            {
                where += string.Format(" and state = '{0}'", state.Replace("'", "''"));
            }
            //状态
            string member_Type = this.w_d_Member_Type.SelectedValue.Trim();
            if (!string.IsNullOrEmpty(member_Type))
            {
                where += string.Format(" and member_Type = '{0}'", member_Type.Replace("'", "''"));
            }
            //GSP审核
            string gsp = this.w_d_Gsp.SelectedValue.Trim();
            if (!string.IsNullOrEmpty(gsp))
            {
                if (gsp == "1")
                {
                    where += " and exists(SELECT 1 FROM DrugsBase_Enterprise WHERE BuyFilingStatus=1 AND ID=T.ParentId)";
                }
                if (gsp == "0")
                {
                    where += " and not exists(SELECT 1 FROM DrugsBase_Enterprise WHERE BuyFilingStatus=1 AND ID=T.ParentId)";
                }
            }
            //上级单位
            string hasParent = this.w_d_hasParent.SelectedValue.Trim();
            if (!string.IsNullOrEmpty(hasParent))
            {
                if (hasParent == "1")
                {
                    where += " and ParentId<>0";
                }
                if (hasParent == "0")
                {
                    where += " and ParentId=0";
                }
            }
            //注册时间
            DateTime fDate = new DateTime(); DateTime.TryParse(this.fromDate.Text.Trim(), out fDate);
            DateTime tDate = new DateTime(); DateTime.TryParse(this.toDate.Text.Trim(), out tDate);
            if (fDate != new DateTime())
            {
                where += string.Format(" and convert(char(10),RegisterDate,120)>='{0}'", this.fromDate.Text.Trim());
            }
            if (tDate != new DateTime())
            {
                where += string.Format(" and convert(char(10),RegisterDate,120)<='{0}'", this.toDate.Text.Trim());
            }
            //未下过订单的，时间为注册时间
            string hasOrder = this.w_d_hasOrder.SelectedValue.Trim();
            if (!string.IsNullOrEmpty(hasOrder))
            {
                int day = 0; int.TryParse(hasOrder, out day);
                if (day > 0)
                {
                    where += string.Format(" and (not exists(select 1 from Orders where ReceiverId=T.UID) and datediff(day,RegisterDate,getdate())>{0})", day);
                }
            }
            //交易员选择
            if (ddlEditer.SelectedIndex > 0 && !string.IsNullOrEmpty(ddlEditer.SelectedValue))
            {
                where += string.Format(" and Editer={0}", ddlEditer.SelectedValue);
            }
            //外销人员选择
            if (ddlOSP.SelectedIndex > 0 && !string.IsNullOrEmpty(ddlOSP.SelectedValue))
            {
                //外销按地区
                if (bll.ExecuteScalarForCache("SELECT role FROM dbo.yxs_administrators WHERE adminid=" + ddlOSP.SelectedValue).ToString().Contains("60"))
                {
                    where += string.Format(" and (Borough IN (SELECT ResponseCounty FROM ResponseRegionsOfOutSellPerson WHERE PersonID={0}) or OSPId={0})", ddlOSP.SelectedValue);
                }
                else
                {
                    where += string.Format(" and OSPId={0}", ddlOSP.SelectedValue);
                }
            }
            //是否成交
            if (DropDownList1.SelectedValue != "")
            {
                if (DropDownList1.SelectedValue == "0")
                {
                    where += " and uid in (SELECT ReceiverId FROM dbo.Orders WHERE OrderStatus>0)";
                }
                else if (DropDownList1.SelectedValue == "1")
                {
                    where += " and uid not in (SELECT ReceiverId FROM dbo.Orders WHERE OrderStatus>0)";
                }
            }
            return where;
        }

        //分页数据初始化
        protected override void StartLoad(int PageIndex, string strWhere)
        {
            if (!IsPostBack)
            {
                SelectEditer();
                //BindOutSellPerson();
            }

            int recordCount, pageCount;
            AspNetPager1.PageSize = 15;

            string strWhereExt = GetSqlWhere();
            if (!base.isPass("008009001"))
            {
                //外销按地区
                if (bll.ExecuteScalarForCache("SELECT role FROM dbo.yxs_administrators WHERE adminid=" + UserId).ToString().Contains("60"))
                {
                    strWhereExt += string.Format(" and (Borough IN (SELECT ResponseCounty FROM ResponseRegionsOfOutSellPerson WHERE PersonID={0}) or OSPId={0})", UserId);
                }
                else
                {
                    strWhereExt += string.Format(" and (Editer={0} or OSPId={0})", UserId);
                }
            }
            if (!Library.Lang.DataValidator.isNULL(this.tablist.Attributes["SortExpression"], this.tablist.Attributes["SortDirection"]))
            {
                order = tablist.Attributes["SortDirection"] != "DESC";
                orderField = tablist.Attributes["sortExpression"];
            }
            string tableName = "(SELECT " + columnNames + ",(SELECT name FROM dbo.yxs_administrators WHERE adminid=Editer)adminname" + " FROM " + tableNames + ") AS T";
            tablist.DataSource = bll.GetList(tableName, strWhereExt, AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex,
                order, orderField, like, whereField, whereString, out recordCount, out pageCount);
            AspNetPager1.RecordCount = recordCount;
            tablist.DataBind();
        }
        protected void Search_Click(object sender, EventArgs e)
        {
            this.whereField = "";
            this.orderField = "RegisterDate";
            this.order = true;
            this.like = false;
            this.whereString = "";
            StartLoad(1, whereString);
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            StartLoad(1, null);
        }

        /// <summary>
        /// GSP状态
        /// </summary>
        /// <param name="id">企业ID</param>
        /// <returns></returns>
        public static int GetGSP(int uid)
        {
            string sql = string.Format("SELECT (SELECT BuyFilingStatus FROM DrugsBase_Enterprise WHERE ID=(SELECT ParentId FROM memberinfo WHERE UID={0})) BuyFilingStatus", uid);
            SOSOshop.BLL.Db db = new Db();
            DataTable dt = db.ExecuteTable(sql);
            if (dt == null)
            {
                return 0;
            }
            else
            {
                try
                {
                    return int.Parse(dt.Rows[0][0].ToString());
                }
                catch
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 最近成交日期
        /// </summary>
        /// <param name="id">会员ID</param>
        /// <returns></returns>
        public static string GetOrderDate(int id)
        {
            SOSOshop.BLL.Db bll = new SOSOshop.BLL.Db();
            object obj = bll.ExecuteScalar("SELECT TOP(1) OrderDate FROM orders " +
                " WHERE ReceiverId=" + id +
                " and OrderStatus>0 ORDER BY OrderDate DESC");
            return (obj == null ? "" : Convert.ToDateTime(obj).ToString("yyyy-MM-dd"));
        }
        /// <summary>
        /// 登录次数
        /// </summary>
        /// <param name="id">会员ID</param>
        /// <returns></returns>
        public static int GetLoginTimes(int id)
        {
            SOSOshop.BLL.Db bll = new SOSOshop.BLL.Db();
            object obj = bll.ExecuteScalar("SELECT COUNT(*) FROM memberloginlog WHERE UID=" + id);
            return (obj == null ? 0 : Convert.ToInt32(obj));
        }

        public override void SetModuleTag()
        {
            base.ModuleBrowse = "008009001";
            base.ModuleAdd = "008009001";
            base.ModuleDelete = "008009001";
            base.ModuleEdit = "008009001";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void tablist_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression.ToString();
            string sortDirection = "ASC";
            if (sortExpression == this.tablist.Attributes["SortExpression"])
            {
                sortDirection = (this.tablist.Attributes["SortDirection"].ToString() == sortDirection ? "DESC" : "ASC");
            }
            this.tablist.Attributes["SortExpression"] = e.SortExpression.ToString();
            this.tablist.Attributes["SortDirection"] = sortDirection;
            tablist.HeaderRow.Cells[9].Text = string.Format("登录{0}", sortDirection == "ASC" ? "↑" : "↓");
            StartLoad(1, null);
        }
    }
}
