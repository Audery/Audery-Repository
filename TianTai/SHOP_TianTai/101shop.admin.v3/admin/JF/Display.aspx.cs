using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SOSOshop.BLL.JF;
using SOSOshop.WEB.UI;

using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Text;
using System.Data;
using SOSOshop.Model.JF;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Data.OleDb;
using System.Collections;


namespace _101shop.admin.v3.admin.JF
{
    public partial class Display : ManageBasePage
    {
        System.Text.StringBuilder sb = new StringBuilder();
        protected List<SOSOshop.Model.JF.Jf_Model> testList = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SOSOshop.BLL.PromptInfo.Popedom("006006000");
                //testList = new SOSOshop.BLL.JF.Jf().list();
                StartLoad(1, null);
                //Literal1.Text =Convert.ToString(bll.GetList("Jf", "*", PageSize,PageIndex, true,"id",true, null,sb.ToString(), out recordCount, out pageCount));
            }
            if (ChangeHope.WebPage.PageRequest.GetFormString("operate") != string.Empty && ChangeHope.WebPage.PageRequest.GetFormInt("id") > 0)
            {
                string types = Request.Form["operate"].Trim();
                int id = ChangeHope.WebPage.PageRequest.GetFormInt("id");
                if (types == "del")
                {
                    Jf bll = new Jf();
                    bll.Delete(id);
                }
                Response.End();
                return;
            }
             
        }
        protected void Button1_Click1(object sender, EventArgs e)
        {
            string name = this.name.Text;
            string pt = this.pt.Text;
            int jf = string.IsNullOrEmpty(this.jf.Text) ? 0 : Convert.ToInt32(this.jf.Text);
            //testList = new SOSOshop.BLL.JF.Jf().list1(name, pt, jf);
            StartLoad(1, null);
        }
        public override void SetModuleTag()
        {

        }


        //分页数据初始化
        protected override void StartLoad(int PageIndex, string strWhere)
        {
            Jf bll = new Jf();
            int recordCount, pageCount;
            sb.Append(" ");
            string name = Request.Form["name"];
            string pt = Request.Form["pt"];
            string jf = Request.Form["jf"];
            if (!string.IsNullOrEmpty(name))
            {
                sb.AppendFormat(" and name='{0}'", name);
            }
            if (!string.IsNullOrEmpty(pt))
            {
                sb.AppendFormat(" and pt='{0}'", pt);
            }
            if (!string.IsNullOrEmpty(jf))
            {
                sb.AppendFormat(" and jf={0}", Convert.ToInt32(jf));
            }

            AspNetPager1.PageSize = 10;

            var DT = bll.GetListByPage("jf", "*", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, " ID ASC", sb.ToString(), out recordCount, out pageCount);

          
            AspNetPager1.RecordCount = recordCount;
            testList = new List<Jf_Model>();
            if (DT != null && DT.Rows.Count > 0)
            {

                foreach (DataRow item in DT.Rows)
                {
                    testList.Add(new Jf_Model
                    {
                        id = Convert.ToInt32(item["id"]),
                        img = Convert.ToString(item["img"]),
                        jf = Convert.ToInt32(item["jf"]),
                        name = Convert.ToString(item["name"]),
                        pt = Convert.ToString(item["pt"])
                    });
                }
            }


        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            StartLoad(1, null);
        }
    }

}