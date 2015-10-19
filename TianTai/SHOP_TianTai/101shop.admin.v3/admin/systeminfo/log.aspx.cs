using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Policy;



public partial class admin_systeminfo_log : SOSOshop.WEB.UI.ManageBasePage
{
    SOSOshop.BLL.Logs.Log bll = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        bll = new SOSOshop.BLL.Logs.Log(DropDownList1.SelectedValue);       
        if (!IsPostBack && isBrowse())
        {
            StartLoad(1, null);
        }
    }   
    protected override void StartLoad(int PageIndex, string strWhere)
    {

        int recordCount, pageCount;
        AspNetPager1.PageSize = int.Parse(pageSize.SelectedValue);
        Repeater1.DataSource = bll.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, out recordCount, out pageCount, order, orderField, like, whereField, whereString, DropDownList4.SelectedValue);
        AspNetPager1.RecordCount = recordCount;
        Repeater1.DataBind();

    }
    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        StartLoad(1, null);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        bll.DeleteAll();
        StartLoad(1, null);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        this.whereField = DropDownList3.SelectedValue;      
        if (!string.IsNullOrEmpty(TextBox1.Text))
        {
            if (bool.Parse(RadioButtonList1.SelectedValue))
            {   
                whereString = TextBox1.Text.Trim();
                like = true;
            }
            else
            {
                whereString = TextBox1.Text.Trim();
                like = false;
            }

        }
        else
        {
            whereString = null;
        }
        StartLoad(1, whereString);
    }
    public string url(object url)
    {
        if (url == null) return "";
        if (string.IsNullOrEmpty(url.ToString())) return "";
        System.Uri u = new Uri(url.ToString());
        return u.LocalPath;

    }
    public override void SetModuleTag()
    {
        ModuleBrowse = "012018001";
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        StartLoad(1, null);
    }
}