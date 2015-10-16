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

namespace _101shop.admin.v3.accessories
{
    public partial class hailhellowlink_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
            {
                SOSOshop.BLL.PromptInfo.Popedom("012008001");
                if (ChangeHope.WebPage.PageRequest.GetFormString("Option") != string.Empty && ChangeHope.WebPage.PageRequest.GetFormInt("id") > 0)
                {
                    string types = Request.Form["Option"].Trim();
                    int id = ChangeHope.WebPage.PageRequest.GetFormInt("id");
                    if (types == "del")
                    {
                        if (SOSOshop.BLL.PowerPass.isPass("012008003"))
                        {
                            Del(id);
                        }
                        else
                        {
                            Response.Write("no");
                        }
                    }
                    else if (types == "leavel")
                    {
                        
                        //优先级
                        if (SOSOshop.BLL.PowerPass.isPass("012008004"))
                        {
                        int Num = ChangeHope.WebPage.PageRequest.GetFormInt("leavelID");
                        SetLeavel(id, Num);
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
            YXShop.BLL.Accessories.Hailhellowlink bll = new YXShop.BLL.Accessories.Hailhellowlink();
            ChangeHope.DataBase.DataByPage dataPage = bll.GetList();
            //第一步先添加表头
            table.AddHeadCol("", "序号");
            table.AddHeadCol("", "会员名称");
            table.AddHeadCol("", "优先级");
            table.AddHeadCol("", "点击数");
            table.AddHeadCol("", "会员类型");
            table.AddHeadCol("", "审核状态");
            table.AddHeadCol("", "操作");
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
                    table.AddCol(No);
                    table.AddCol(string.Format("<a href='?id={0}'>{1}</a>", dataPage.DataReader["id"].ToString(), dataPage.DataReader["sitename"].ToString()));
                    table.AddCol(string.Format("<input id='leavel{0}' value='{1}' size='5' onblur='SetLeavel({0})'/>", dataPage.DataReader["id"].ToString(), dataPage.DataReader["sitelevel"].ToString()));
                    table.AddCol(dataPage.DataReader["siteclickcount"].ToString());
                    table.AddCol(dataPage.DataReader["putouttyid"].ToString() == "0" ? "管理员" : "会员");
                    table.AddCol(string.Format("<img src='../images/{0}.gif' alt='审核与否'/>", dataPage.DataReader["sitestate"].ToString()));
                    table.AddCol(string.Format("<a href=hailhellowlink_edit.aspx?id={0}>编辑</a> <a href='javascript:void(0)' onclick='Del({0})'>删除</a>", dataPage.DataReader["id"].ToString()));
                    table.AddRow();
                }
            }
            string view = table.GetTable() + dataPage.PageToolBar;
            dataPage.Dispose();
            dataPage = null;
            return view;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        private void Del(int id)
        {
          
            YXShop.BLL.Accessories.Hailhellowlink bll = new YXShop.BLL.Accessories.Hailhellowlink();
            bll.Delete(id);
            Response.Write("ok");
           
        }

        /// <summary>
        /// 修改优先级
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sort"></param>
        private void SetLeavel(int id, int sitelevel)
        {
           
                YXShop.BLL.Accessories.Hailhellowlink bll = new YXShop.BLL.Accessories.Hailhellowlink();
                bll.Amend(id, "sitelevel", sitelevel);
                Response.Write("ok");
           
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            this.lblList.Text = GetList();
        }
    }
}
