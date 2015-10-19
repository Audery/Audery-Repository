using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Text;
using System.Data;

namespace _101shop.admin.v3.admin.JF
{
    public partial class Edit : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SOSOshop.BLL.PromptInfo.Popedom("006006004", "对不起，你没有修改该商品的权限");  
             int id = Convert.ToInt32(Request.QueryString["id"]);
                HPID.Value = id.ToString();
                Database db = DatabaseFactory.CreateDatabase();
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("select * from Jf where ID={0}", id));
                //db.AddInParameter(cmd,"@Name", DbType.String, name1);
                using (IDataReader reader = db.ExecuteReader(CommandType.Text, sb.ToString()))
                {
                    if (reader.Read())
                    {
                        this.TxtName.Text = reader.GetString(reader.GetOrdinal("Name"));
                        this.TxtIntegral.Text = Convert.ToString(reader.GetInt32(reader.GetOrdinal("Jf")));
                        this.TxtAttribute.Text = reader.GetString(reader.GetOrdinal("Pt"));
                    }
                }
            }
        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    string name = this.TextBox1.Text;
        //    int id = 1;


        // "update jf set name ='name' where id=id"
        //}

        protected void Button1_Click1(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(HPID.Value);
            string name = this.TxtName.Text;
            int jf = Convert.ToInt32(this.TxtIntegral.Text);
            string pt = this.TxtAttribute.Text;
            SOSOshop.BLL.JF.Jf bll = new SOSOshop.BLL.JF.Jf();
            bll.Edit(id, name, jf, pt);
            Response.Write("<script>alert('修改成功');window.location.href='display.aspx';</script>");
        }
    }
}