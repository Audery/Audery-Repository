using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using YXShop.Log;
using System.Text;

namespace _101shop.admin.v3.accessories
{
    public partial class syslog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SOSOshop.BLL.PromptInfo.Popedom("012013000");
                if (ChangeHope.WebPage.PageRequest.GetFormString("Option") != string.Empty && ChangeHope.WebPage.PageRequest.GetFormString("id") != string.Empty)
                {
                    string types = Request.Form["Option"].Trim();
                    string StrID = ChangeHope.WebPage.PageRequest.GetFormString("id");
                    if (types == "del")
                    {
                        if (SOSOshop.BLL.PowerPass.isPass("012013001"))
                        {
                            Response.Write(Del(StrID) ? "ok" : "no");
                        }
                        else
                        {
                            Response.Write("no");
                        }
                    }
                    Response.End();
                    return;
                }
                this.lblList.Text = GetList();
            }
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        protected string GetList()
        {
            ChangeHope.WebPage.Table table = new ChangeHope.WebPage.Table();
            // 得到所有记录
            string strWhere = "1=1";
            if (q.Text.Trim() != string.Empty) strWhere += " and isnull(Keyword,'') + isnull(EventDescription,'') like '%" + q.Text.Trim().Replace("'", "") + "%' ";
            if (t.Text.Trim() != string.Empty) strWhere += " and convert(char(10),OperateTime,120)='" + t.Text.Trim().Replace("'", "") + "' ";
            ChangeHope.DataBase.DataByPage dataPage = new ChangeHope.DataBase.DataByPage();
            dataPage.Sql = "[select] * [from] yxs_SysLog  [where] " + strWhere + " [order by] id desc";
            dataPage.GetRecordSetByPage();
            //第一步先添加表头
            table.AddHeadCol("5", "<input type=\"checkbox\" id=\"chkAll\" onclick=\"CheckAll(this.form)\" alt=\"全选/取消\" title=\"全选/取消\" />");
            table.AddHeadCol("70", "关键词");
            table.AddHeadCol("120", "系统模块");
            table.AddHeadCol("", "事件描述");
            table.AddHeadCol("45", "用户ID");
            table.AddHeadCol("", "来源");
            table.AddHeadCol("115", "时间");
            //table.AddHeadCol("", "操作");
            table.AddRow();
            //添加表的内容
            if (dataPage.DataReader != null)
            {
                int curpage = ChangeHope.WebPage.PageRequest.GetInt("pageindex");
                if (curpage < 0)
                {
                    curpage = 1;
                }
                while (dataPage.DataReader.Read())
                {
                    table.AddCol("<input ID=\"cBox\" type=\"checkbox\" name=\"chk\" value=\"" + dataPage.DataReader["ID"].ToString() + "\" />");
                    table.AddCol(dataPage.DataReader["Keyword"].ToString());
                    table.AddCol(dataPage.DataReader["SubModule"].ToString());
                    table.AddCol(dataPage.DataReader["EventDescription"].ToString());
                    string UID = dataPage.DataReader["UID"].ToString();
                    string AdminID = dataPage.DataReader["AdminID"].ToString();
                    table.AddCol(string.IsNullOrEmpty(UID) ? (string.IsNullOrEmpty(AdminID) ? "" : "" + new SOSOshop.BLL.Db().ExecuteScalar("select name from yxs_administrators where adminid=" + AdminID)) : "" + new SOSOshop.BLL.Db().ExecuteScalar("select isnull(TrueName,IncName) from yxs_memberinfo where UID=" + UID));
                    table.AddCol(dataPage.DataReader["Source"].ToString());
                    table.AddCol(dataPage.DataReader["OperateTime"].ToString());
                    //table.AddCol("");
                    table.AddRow();
                }
            }
            string view = table.GetTable() + dataPage.PageToolBar;
            dataPage.Dispose();
            dataPage = null;
            return view;
        }

        private bool Del(string id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from yxs_SysLog ");
            strSql.Append(" where ID in (" + id + ") ");
            return 0 != ChangeHope.DataBase.SQLServerHelper.ExecuteSql(strSql.ToString());
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.lblList.Text = GetList();
        }
    }
}
