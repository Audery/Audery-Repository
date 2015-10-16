using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Policy;


public partial class admin_systeminfo_log : System.Web.UI.Page
{
    DSWebService.BLL.Log bll = new DSWebService.BLL.Log();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (pageSize != null)
            {
                pageSize.Items.Clear();
                for (int i = 1; i < 11; i++)
                {
                    string temp = (5 * i).ToString();
                    pageSize.Items.Add(new ListItem { Value = temp, Text = temp });
                }
                pageSize.SelectedValue = string.IsNullOrEmpty(GetCookie("pageSize")) ? "10" : GetCookie("pageSize");
            }
            StartLoad(1, null);
            init();

        }
    }
    protected void init()
    {
        //DropDownList4.DataSource = new SOSOYY.BLL.Users().GetList("");
        //DropDownList4.DataTextField = "UserName";
        //DropDownList4.DataValueField = "UserName";
        //DropDownList4.DataBind();
        DropDownList4.Items.Insert(0, new ListItem() { Value = "", Text = "全部", Selected = true });
        DropDownList4.Items.Insert(1, new ListItem() { Value = "0", Text = "错误", });
        DropDownList4.Items.Insert(2, new ListItem() { Value = "1", Text = "消息", });
        DropDownList4.Items.Insert(3, new ListItem() { Value = "2", Text = "同步", });
        DropDownList4.Items.Add(new ListItem() { Value = "500", Text = "服务器端错误", });
        DropDownList4.Items.Add(new ListItem() { Value = "404", Text = "更新服务消息", });
    }
    protected string GetCookie(string key)
    {
        HttpCookie hc = Request.Cookies[key];
        if (hc != null) return hc.Value;
        return "";
    }
    protected void StartLoad(int PageIndex, string strWhere)
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
        //this.order = bool.Parse(DropDownList4.SelectedValue);
        //this.orderField = DropDownList3.SelectedValue;
        if (!string.IsNullOrEmpty(TextBox1.Text))
        {
            if (bool.Parse(RadioButtonList1.SelectedValue))
            {
                //whereString = string.Format("%{0}%", TextBox1.Text);
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
        try
        {
            if (url == null) return "";
            if (string.IsNullOrEmpty(url.ToString())) return "";
            System.Uri u = new Uri(url.ToString());
            return u.LocalPath;
        }
        catch{}
        return url.ToString();
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.DropDownList pageSize = sender as System.Web.UI.WebControls.DropDownList;
        AddPageIndex(int.Parse(pageSize.SelectedValue));
        StartLoad(1, null);
    }
    protected void AddPageIndex(int pageSize)
    {
        AddCookie("pageSize", pageSize.ToString());
    }
    protected void AddCookie(string key, string value)
    {
        AddCookie(key, value, DateTime.Now.AddMinutes(30));
    }
    protected void AddCookie(string key, string value, DateTime Expires)
    {
        HttpCookie hc = new HttpCookie(key);
        hc.Name = key;
        hc.Value = value;
        hc.Expires = Expires;
        Response.Cookies.Add(hc);
    }
    #region 分页属性
    protected bool order
    {
        get
        {
            if (this.ViewState["order"] == null)
            {
                return true;
            }
            return bool.Parse(this.ViewState["order"].ToString());
        }
        set { this.ViewState["order"] = value; }
    }
    protected string orderField
    {
        get
        {
            if (ViewState["orderField"] == null)
            {
                return "id";
            }
            return ViewState["orderField"].ToString();
        }
        set
        {
            ViewState["orderField"] = value;
        }
    }
    protected bool like
    {
        get
        {
            if (ViewState["like"] == null)
            {
                return false;
            }
            return bool.Parse(ViewState["like"].ToString());
        }
        set
        {
            ViewState["like"] = value;
        }
    }
    protected string whereField
    {
        get
        {
            if (ViewState["whereField"] == null)
            {
                return "Title";
            }
            return ViewState["whereField"].ToString();
        }
        set
        {
            ViewState["whereField"] = value;
        }
    }
    protected string whereString
    {
        get
        {
            if (ViewState["whereString"] == null)
            {
                return null;
            }
            return ViewState["whereString"].ToString();
        }
        set
        {
            ViewState["whereString"] = value;
        }
    }
    #endregion

    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        StartLoad(1, null);
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Cache["reboot"] = DateTime.Now.Ticks.ToString();
    }
}