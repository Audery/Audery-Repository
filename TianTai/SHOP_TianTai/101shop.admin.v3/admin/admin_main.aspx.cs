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
using System.Collections.Generic;

namespace _101shop.admin.v3
{
    public partial class admin_main : System.Web.UI.Page
    {
		public SOSOshop.Model.AdminInfo adminInfo = null;
        public bool isJG = false/*监管*/;

        public string script = "";
        public string AdminRole = "";
        public string AdminPowerType = "";
        public List<string> BigMenu = new List<string>();
        public List<string> SmallMenu = new List<string>();
        SOSOshop.BLL.Roles_Permissions bll = new SOSOshop.BLL.Roles_Permissions();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                adminInfo = (SOSOshop.Model.AdminInfo)SOSOshop.BLL.AdministrorManager.Get();
                if (adminInfo != null)
                {
					isJG = SOSOshop.BLL.AdministrorManager.GetJGManager()/*监管*/;
                    AdminRole = adminInfo.AdminRole;
                    AdminPowerType = adminInfo.AdminPowerType;
                }
            }
        }
        //初始化权限设置
        private void InitPowers(string AdminRole)
        {
            int RoleID = 0;
            string[] Roles = AdminRole.Replace("%2c", ",").Split(',');
            List<SOSOshop.Model.Roles_Permissions> list = new List<SOSOshop.Model.Roles_Permissions>();
            if (AdminPowerType == "all")
            {
                foreach (SOSOshop.Model.Roles_Permissions item in bll.GetAll())
                {
                    list.Add(item);
                }
                BigMenu.Add("快捷导航");
            }
            else
            {
                foreach (string Role in Roles)
                {
                    try { RoleID = int.Parse(Role); }
                    catch { }
                    foreach (SOSOshop.Model.Roles_Permissions item in bll.GetListByColumn("ID", RoleID))
                    {
                        if (!list.Contains(item)) list.Add(item);
                    }
                }
            }
            if (list != null && list.Count > 0)
            {
                foreach (TreeNode tNode in TreeView1.Nodes)
                {
                    InitChildPowers(tNode, ref list);
                }
            }
        }
        private void InitChildPowers(TreeNode tNode, ref List<SOSOshop.Model.Roles_Permissions> list)
        {
            int nodeValue = 0;
            if (ChangeHope.Common.ValidateHelper.IsNumber(tNode.Value))//防止xml输入错误数据引起的错误、、处理方式：跳过错误数据的处理
            {
                if (list == null)
                    return;
                else if (list.Count < 1)
                    return;
                nodeValue = int.Parse(tNode.Value);
                bool bl = false;
                foreach (SOSOshop.Model.Roles_Permissions rModel in list)
                {
                    if (nodeValue == rModel.OperateCode)
                    {
                        tNode.Checked = true;
                        tNode.Expanded = true;
                        if (tNode.Depth == 0)
                        {
                            BigMenu.Add(tNode.Text);
                        }
                        else
                        {
                            if (tNode.ChildNodes.Count > 0) SmallMenu.Add(tNode.Text);
                        }
                        if (list.Remove(rModel))
                        {
                            bl = true;
                            break;
                        }
                    }
                }
                if (bl)
                {
                    foreach (TreeNode node in tNode.ChildNodes)
                    {
                        InitChildPowers(node, ref list);//递归
                    }
                }
            }
        }
        protected void TreeView1_DataBound(object sender, EventArgs e)
        {
            InitPowers(AdminRole);
            script = "BigMenu='" + string.Join(",", BigMenu.ToArray()) + "';SmallMenu='" + string.Join(",", SmallMenu.ToArray()) + "';PowerType='" + AdminPowerType + "';";
            TreeView1.Visible = false;
        }

    }
}
