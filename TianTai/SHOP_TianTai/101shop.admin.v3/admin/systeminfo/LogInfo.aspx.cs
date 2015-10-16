using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_systeminfo_LogInfo :SOSOshop.WEB.UI.ManageBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Literal1.Text = new SOSOshop.BLL.Logs.Log(Request.QueryString["type"]).GetModel(Request.QueryString["id"]).detail;
    }

    public override void SetModuleTag()
    {
        
    }
}