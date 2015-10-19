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

namespace _101shop.admin.v3.member
{
    public partial class role_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SOSOshop.BLL.PromptInfo.Popedom("007002001");
                if (ChangeHope.WebPage.PageRequest.GetFormString("Option") != string.Empty && ChangeHope.WebPage.PageRequest.GetFormInt("id") > 0)
                {

                    string types = Request.Form["Option"].Trim();
                    int id = ChangeHope.WebPage.PageRequest.GetFormInt("id");
                    if (types == "del")
                    {
                        if (SOSOshop.BLL.PowerPass.isPass("007002003"))
                        {
                            del(id);
                            #region 后台用户操作日志记录
                            SOSOshop.Model.AdminInfo adminInfo = SOSOshop.BLL.AdministrorManager.Get();
                            SOSOshop.BLL.Logs.Log.LogAdminAdd("删除角色", (adminInfo == null ? 0 : adminInfo.AdminId), (adminInfo == null ? "" : adminInfo.AdminName), 1);
                            #endregion
                        }
                        else
                        {
                            Response.Write("no");
                        }
                    }
                    Response.End();
                    return;
                }
                this.Literal1.Text = GetList();
            }
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        protected string GetList()
        {
            SOSOshop.BLL.SysParameter sp = new SOSOshop.BLL.SysParameter();
            ChangeHope.WebPage.Table table = new ChangeHope.WebPage.Table();
            SOSOshop.BLL.Role data = new SOSOshop.BLL.Role();
            ChangeHope.DataBase.DataByPage dataPage = data.GetList();
            //第一步先添加表头
            table.AddHeadCol("10%", "序号");
            table.AddHeadCol("25%", "角色名");
            table.AddHeadCol("40%", "描述");
            table.AddHeadCol("25%", "操作");
            table.AddRow();
            //添加表的内容
            if (dataPage.DataReader != null)
            {
                int curpage = ChangeHope.WebPage.PageRequest.GetInt("pageindex");
                if (curpage < 0)
                {
                    curpage = 1;
                }
                int count = 0;
                while (dataPage.DataReader.Read())
                {
                    count++;
                    string No = (15 * (curpage - 1) + count).ToString();
                    table.AddCol("No." + No);
                    table.AddCol(dataPage.DataReader["name"].ToString());
                    table.AddCol(dataPage.DataReader["description"].ToString());
                    table.AddCol(string.Format("<a href=role_edit.aspx?id={0}>编辑</a> <a href='role_setmember.aspx?id={0}'>设置成员</a> <a href='popedom_manage.aspx?id={0}'>权限管理</a> <a href='#' onclick='Del({0})'>删除</a>", dataPage.DataReader["id"].ToString()));

                    table.AddRow();
                }
            }
            string view = table.GetTable() + dataPage.PageToolBar;
            dataPage.Dispose();
            dataPage = null;
            return view;
        }

        private void del(int id)
        {
            SOSOshop.BLL.Role bll = new SOSOshop.BLL.Role();
            if (bll.Delete(id) > 0)
            {
                Response.Write("ok");
            }
        }
    }
}
