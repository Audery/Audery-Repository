using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SOSOshop.BLL;
namespace _101shop.admin.v3.admin._member
{
    public partial class MemberAction : SOSOshop.WEB.UI.ManageBasePage
    {
        SOSOshop.BLL.Member.MemberAction bll = new SOSOshop.BLL.Member.MemberAction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!SOSOshop.BLL.PowerPass.isPass("008010001") && !SOSOshop.BLL.PowerPass.isPass("008010002"))
                {
                    SOSOshop.BLL.PromptInfo.Popedom("000000000000", "对不起，您没有查看的权限!");
                }
                SelectEditer();
                StartLoad(1, null);
            }
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
                //sqlText5 = "SELECT ',' + REPLACE(REPLACE(STUFF((SELECT CAST(i AS varchar) + ',' AS a  FROM (SELECT DISTINCT b.name AS i FROM memberinfo a INNER JOIN yxs_administrators b ON a.Editer=b.adminid) AS t FOR XML path('')), 1, 0, ''), '<a>', ''), '</a>', '')";
                //string _101Admin = Convert.ToString(mbll.ExecuteScalarForCache(sqlText5));
                //dt.Columns.Add("OK", typeof(int));
                //sqlText5 = "SELECT ',' + REPLACE(REPLACE(STUFF((SELECT CAST(i AS varchar) + ',' AS a  FROM (SELECT DISTINCT LTRIM(RTRIM(zhiyname)) AS i FROM zhiyzl WHERE beactive='是' AND issp='是' AND jigid='000') AS t FOR XML path('')), 1, 0, ''), '<a>', ''), '</a>', '')";
                //DbBase db2 = new DbBase(); db2.ChangeDB("ConnectionStringERP");
                //string ErpAdmin = Convert.ToString(db2.ExecuteScalarForCache(sqlText5));
                //foreach (DataRow dr in dt.Rows)
                //{
                //    //处理Erp中有没有
                //    if (!_101Admin.Contains("," + dr[1] + ",") && !ErpAdmin.Contains("," + dr[1] + ",")) dr.Delete();
                //}
                //ddlEditer.Items.Clear();
                //ddlEditer.DataSource = dt;
                //ddlEditer.DataBind();
            }
            ListItem li = new ListItem("全部人员", "");
            ddlEditer.Items.Insert(0, li);
            li = new ListItem("无", "0");
            ddlEditer.Items.Add(li);
        }

        public override void SetModuleTag()
        {
            base.ModuleBrowse = "008010000";
        }
        //分页数据初始化
        protected override void StartLoad(int PageIndex, string strWhere)
        {
            bll = new SOSOshop.BLL.Member.MemberAction();
            int recordCount, pageCount;
            AspNetPager1.PageSize = int.Parse(pageSize.SelectedValue);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            SOSOshop.Model.AdminInfo aInfo = SOSOshop.BLL.AdministrorManager.Get();

            if (!string.IsNullOrEmpty(whereStringTe.Text))
            {
                switch (whereFieldDr.SelectedValue)
                {
                    case "Name":
                        {
                            sb.AppendFormat(" and uid in (SELECT UID FROM dbo.memberinfo WHERE TrueName LIKE('%{0}%'))", whereStringTe.Text.Trim());
                            break;
                        }
                    case "MobilePhone":
                        {
                            sb.AppendFormat(" and uid =(SELECT UID FROM dbo.memberaccount WHERE MobilePhone='{0}')", whereStringTe.Text.Trim());
                            break;
                        }
                    case "sessionid":
                        {
                            sb.AppendFormat(" and sessionid ='{0}'", whereStringTe.Text.Trim());
                            break;
                        }
                }
            }
            if (!string.IsNullOrEmpty(TextBox4.Text))
            {
                sb.AppendFormat(" and created>'{0}'", TextBox4.Text);
            }
            if (!string.IsNullOrEmpty(TextBox1.Text))
            {
                sb.AppendFormat(" and actuation like('%{0}%')", TextBox1.Text);
            }
            if (!string.IsNullOrEmpty(TextBox2.Text))
            {
                sb.AppendFormat(" and actuationvalue like('%{0}%')", TextBox2.Text);
            }

            if (!SOSOshop.BLL.PowerPass.isPass("008010001") && SOSOshop.BLL.PowerPass.isPass("008010002"))
            {
                //外销按地区
                if (bll.ExecuteScalarForCache("SELECT role FROM dbo.yxs_administrators WHERE adminid=" + aInfo.AdminId).ToString().Contains("60"))
                {
                    sb.AppendFormat(" and uid in (SELECT uid FROM memberinfo WHERE Borough IN (SELECT ResponseCounty FROM ResponseRegionsOfOutSellPerson WHERE PersonID={0}) or OSPId={0})", aInfo.AdminId);
                }
                else
                {
                    sb.AppendFormat(" and uid in (SELECT uid FROM memberinfo WHERE Editer={0} or OSPId={0})", aInfo.AdminId);
                }
            }
            else
            {
                //交易员选择
                if (ddlEditer.SelectedIndex > 0 && !string.IsNullOrEmpty(ddlEditer.SelectedValue))
                {
                    sb.AppendFormat(" and uid in (SELECT uid FROM memberinfo WHERE Editer={0})", ddlEditer.SelectedValue);
                }
            }

            tablist.DataSource = bll.GetListByPage("MemberAction", "*,(SELECT TrueName FROM memberinfo WHERE UID=t.uid)TrueName,(SELECT MobilePhone FROM memberaccount WHERE UID=t.uid)MobilePhone", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, "id desc", sb.ToString(), out recordCount, out pageCount);
            AspNetPager1.RecordCount = recordCount;
            tablist.DataBind();
        }
        protected void Search_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            StartLoad(1, null);
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            StartLoad(1, null);
        }
        protected string GetShowTime(object o)
        {
            if (-1 == (int)o)
            {
                return "功能";
            }
            if (Library.Lang.DataValidator.isNumber(o))
            {  
                TimeSpan tmspan = new TimeSpan(0, 0, (int)o);
                string str = "";
                if (tmspan.Hours != 0) str += tmspan.Hours + "时";
                if (tmspan.Minutes != 0) str += tmspan.Minutes + "分";
                if (tmspan.Seconds != 0) str += tmspan.Seconds + "秒";
                return str;
            }
            return "";
        }
    }
}