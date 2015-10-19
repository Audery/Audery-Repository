using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace _101shop.admin.v3.admin.Advertising
{
    public partial class Products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SOSOshop.BLL.PromptInfo.Popedom("015002000");
                if (ChangeHope.WebPage.PageRequest.GetFormString("Option") != string.Empty && ChangeHope.WebPage.PageRequest.GetFormInt("id") > 0)
                {
                    Response.End();
                }
                string[] proid = System.Configuration.ConfigurationManager.AppSettings["Ad_Pro_Id"].Split(',');
                Dictionary<string, string> dic = new Dictionary<string, string>();
                proid.ToList().ForEach(x => dic.Add(x, x));
                dropdown.DataSource = dic;
                dropdown.DataTextField = "Key";
                dropdown.DataValueField = "Value";
                dropdown.DataBind();
            }
           
        }

        

        /// <summary>
        /// 取商品广告位里的商品名称
        /// </summary>
        /// <param name="pid">商品ID列表</param>
        /// <param name="code">广告位编码</param>
        /// <returns></returns>
        protected string GetProductName(List<int> pid,string code)
        {
            if (pid.Count > 0)
            {
                SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();

                DataTable dt = db.ExecuteTable("select Product_Name,product_id from product where product_id in(" + string.Join(",", pid) + ")");
                List<string> pname = new List<string>();
                dt.AsEnumerable().ToList().ForEach(x => pname.Add(string.Format("<span onmouseover=\"shows(this,{0},'{1}')\">{2}</span>", x.Field<int>("product_id"), code, x.Field<string>("product_name"))));
                return pname.Count == 0 ? "" : string.Join("、", pname);
            }
            else
            {
                return "";
            }
        }

        protected void Create_Click(object sender,EventArgs e)
        {
            SOSOshop.BLL.Advertising ad = new SOSOshop.BLL.Advertising();
            SOSOshop.BLL.Advertising newad = ad.GetModelByCode(dropdown.SelectedValue);
            if (newad != null)
            {
                newad.Code = dropdown.SelectedValue;
                newad.Title = w_l_sitename.Text;
                ad.Update(newad);
            }
            else
            {
                ad.Code = dropdown.SelectedValue;
                ad.Title = w_l_sitename.Text;
                ad.Update(ad);
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            SOSOshop.BLL.Advertising ad = new SOSOshop.BLL.Advertising();
            SOSOshop.BLL.Advertising newad = ad.GetModelByCode(this.Code.Text);
            List<int> pid = newad.ProductID;
            pid.Remove(Convert.ToInt32(this.PorductId.Text));
            newad.ProductID = pid;
            ad.Update(newad);
            //this.w_l_sitename.Text = "ddd" + TextBox1.Text;
            //this.lblList.Text = GetList();
        }
    }
}