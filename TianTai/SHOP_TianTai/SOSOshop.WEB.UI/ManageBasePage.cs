using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.Security;
using System.Web;
using Library.UI;
using System.Web.UI.WebControls;
using SOSOshop.Model;

namespace SOSOshop.WEB.UI
{
    public abstract class ManageBasePage : System.Web.UI.Page
    {

        #region 自定义属性
        /// <summary>
        /// 管理员ID
        /// </summary>
        protected int UserId
        {
            get
            {
                if (this.ViewState["userid"] == null)
                {
                    return 0;
                }
                return int.Parse(this.ViewState["userid"].ToString());
            }
            set { this.ViewState["userid"] = value; }
        }
        /// <summary>
        /// 管理员帐户
        /// </summary>
        protected string UserName
        {
            get
            {
                return this.ViewState["UserName"] as string;
            }
            set { this.ViewState["UserName"] = value; }
        }

        #endregion

        public ManageBasePage()
        {
            this.Load += new EventHandler(ManagePage_Load);
#if DEBUG
            this.LoadComplete += new EventHandler(ManageBasePage_LoadComplete);//如果是在调试模式则输入页面执行时间
#endif

        }
        protected void ManageBasePage_LoadComplete(object sender, EventArgs e)
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), "runtime", string.Format("//{0}", Application["runtime"]), true);
        }

        protected void ManagePage_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IsLogin();//判断是否登陆，没登陆否转到登陆页面
                SetModuleTag();//设置权限     
                //设置分页
                System.Web.UI.WebControls.DropDownList PageSize = this.FindControl("pageSize") as System.Web.UI.WebControls.DropDownList;
                if (PageSize != null)
                {
                    PageSize.Items.Clear();
                    for (int i = 1; i < 11; i++)
                    {
                        string temp = (5 * i).ToString();
                        PageSize.Items.Add(new ListItem { Value = temp, Text = temp });
                    }
                    PageSize.SelectedValue = string.IsNullOrEmpty(GetCookie("pageSize")) ? "10" : GetCookie("pageSize");
                }
            }
        }

        #region 登陆登出
        /// <summary>
        /// 登出
        /// </summary>
        protected void LoginOut()
        {
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            Response.Redirect("/");
        }
        protected void IsLogin()
        {
//#if DEBUG//本地测试不用登陆
//            if (!Request.IsAuthenticated)
//            {
//                SOSOshop.BLL.Administrators bll = new SOSOshop.BLL.Administrators();
//                SOSOshop.Model.Administrators model = bll.GetModelByAdminName("admin");
//                SOSOshop.Model.AdminInfo admin = new SOSOshop.Model.AdminInfo();
//                if (model.Power.Equals(0))
//                {
//                    admin.AdminPowerType = "all";
//                }
//                else
//                {
//                    //非管理员权限，等待添加相关内容
//                    admin.AdminPowerType = "";
//                }

//                admin.AdminId = model.AdminId;
//                admin.AdminName = model.Name;
//                admin.AdminRole = model.Role;
//                SOSOshop.BLL.AdministrorManager.Set(admin);
//            }
//#endif
            if (!SOSOshop.BLL.AdministrorManager.CheckAdmin())
            {
                Response.Redirect("/admin/index.aspx");
                Response.End();
            }
            AdminInfo adminModel = (AdminInfo)SOSOshop.BLL.AdministrorManager.Get();
            this.UserId = adminModel.AdminId;
            this.UserName = adminModel.AdminName;
        }
        #endregion
        #region 选择分页大小
        protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.DropDownList pageSize = sender as System.Web.UI.WebControls.DropDownList;
            AddPageIndex(int.Parse(pageSize.SelectedValue));
            StartLoad(1, null);
        }
        protected virtual void StartLoad(int PageIndex, string strWhere) { }
        #endregion

        #region 页面提示
        protected void ShowRight()
        {
            WEB.UI.WebHint.ShowRight();
        }
        protected void ShowError(int i)
        {
            WEB.UI.WebHint.ShowError("操作失败,没有此项功给的权限！");
        }
        protected void ShowError()
        {
            WEB.UI.WebHint.ShowError();
        }
        protected void ShowRight(string RightMsg)
        {
            WEB.UI.WebHint.ShowRight(RightMsg, "", false);
        }
        protected void ShowRight(string RightMsg, string url)
        {
            WEB.UI.WebHint.ShowRight(RightMsg, url, false);
        }
        protected void ShowRight(string url, bool b)
        {
            WEB.UI.WebHint.ShowRight("操作成功！", url, b);
        }
        protected void ShowError(string ErrMsg)
        {
            WEB.UI.WebHint.ShowError(ErrMsg);
        }

        #endregion

        #region 辅助方法
        protected void LogAdd(string msg)
        {
            try
            {

                SOSOshop.Model.Logs.Log model = new SOSOshop.Model.Logs.Log
                {
                    created = DateTime.Now,
                    describe = msg,
                    ip = Request.UserHostAddress,
                    source = this.Request.Url.ToString(),
                    type = 1,
                    userid = UserId,
                    username = UserName
                };
                new SOSOshop.BLL.Logs.Log("LogAdmin").insert(model);
            }
            catch { }

        }
        protected void LogAdd(string msg, params object[] arg)
        {
            LogAdd(string.Format(msg, arg));
        }
        protected void AddPageIndex(int pageSize)
        {
            AddCookie("pageSize", pageSize.ToString());
        }
        protected void AddCookie(string key, string value, DateTime Expires)
        {
            HttpCookie hc = new HttpCookie(key);
            hc.Name = key;
            hc.Value = value;
            hc.Expires = Expires;
            Response.Cookies.Add(hc);
        }
        protected void AddCookie(string key, string value)
        {
            AddCookie(key, value, DateTime.Now.AddMinutes(30));
        }
        protected string GetCookie(string key)
        {
            HttpCookie hc = Request.Cookies[key];
            if (hc != null) return hc.Value;
            return "";
        }
        /// <summary>
        /// 设置浏览器缓存
        /// </summary>
        /// <param name="Seconds">以秒为单位</param>
        public void SetClientCache(int Seconds)
        {
            DateTime IfModifiedSince;
            if (DateTime.TryParse(this.Request.Headers.Get("If-Modified-Since"), out IfModifiedSince))
            {
                if ((DateTime.Now - IfModifiedSince).TotalSeconds < Seconds)
                {
                    Response.Status = "304 Not Modified";
                    Response.StatusCode = 304;
                    Response.End();
                }
            }
            Response.CacheControl = "Public";
            Response.Cache.SetLastModified(DateTime.Now);
        }
        /// <summary>
        /// 刷新商品详细页缓存
        /// </summary>
        /// <param name="id"></param>
        public void FlushHtml(int id)
        {
            try
            {
                System.IO.File.Delete(Server.MapPath(string.Format("/product/html/{0}.html", id)));
            }
            catch { }
        }
        #endregion

        #region 数据效验
        /// <summary>
        /// 数据验证
        /// </summary>
        /// <returns></returns>
        protected bool Checking()
        {
            return FindChecking(this.Controls);

        }

        /// <summary>
        /// 数据循环效验证
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        private bool FindChecking(ControlCollection control)
        {
            foreach (Control item in control)
            {
                if (item.HasControls())
                {
                    FindChecking(item.Controls);
                }
                if (item is TextBoxManage)
                {
                    TextBoxManage temp = item as TextBoxManage;
                    if (temp.CanBeNull == "必填" && string.IsNullOrEmpty(temp.Text))
                    {
                        ShowError(temp.tip + "为必须填项");
                        return false;
                    }
                    switch (temp.RequiredFieldType)
                    {
                        case "isurl":
                            {
                                if (!Library.Lang.DataValidator.IsUrl(temp.Text) && !string.IsNullOrEmpty(temp.Text))
                                {
                                    ShowError(temp.tip + "格式不正确");
                                    return false;
                                }
                                break;
                            }
                        case "isphone":
                            {
                                if (!Library.Lang.DataValidator.IsTel(temp.Text) && !string.IsNullOrEmpty(temp.Text))
                                {
                                    ShowError(temp.tip + "格式不正确");
                                    return false;
                                }
                                break;
                            }
                        case "ismobile":
                            {
                                if (!Library.Lang.DataValidator.IsMobile(temp.Text) && !string.IsNullOrEmpty(temp.Text))
                                {
                                    ShowError(temp.tip + "格式不正确");
                                    return false;
                                }
                                break;
                            }
                        case "ispostcode":
                            {
                                if (!Library.Lang.DataValidator.IsPostCode(temp.Text) && !string.IsNullOrEmpty(temp.Text))
                                {
                                    ShowError(temp.tip + "格式不正确");
                                    return false;
                                }
                                break;
                            }
                        case "isqq":
                            {
                                if (!Library.Lang.DataValidator.isNumber(temp.Text) && !string.IsNullOrEmpty(temp.Text))
                                {
                                    ShowError(temp.tip + "格式不正确");
                                    return false;
                                }
                                break;
                            }
                        case "ismsn":
                            {
                                if (!Library.Lang.DataValidator.IsEmail(temp.Text) && !string.IsNullOrEmpty(temp.Text))
                                {
                                    ShowError(temp.tip + "格式不正确");
                                    return false;
                                }
                                break;
                            }
                        case "isidnumber":
                            {
                                if (!Library.Lang.DataValidator.IsIDCard(temp.Text) && !string.IsNullOrEmpty(temp.Text))
                                {
                                    ShowError(temp.tip + "格式不正确");
                                    return false;
                                }
                                break;
                            }
                        case "isemail":
                            {
                                if (!Library.Lang.DataValidator.IsEmail(temp.Text) && !string.IsNullOrEmpty(temp.Text))
                                {
                                    ShowError(temp.tip + "格式不正确");
                                    return false;
                                }
                                break;
                            }
                        case "isint":
                            {
                                if (!Library.Lang.DataValidator.IsInt(temp.Text) && !string.IsNullOrEmpty(temp.Text))
                                {
                                    ShowError(temp.tip + "格式不正确");
                                    return false;
                                }
                                break;
                            }
                        case "isfloat":
                            {
                                if (!Library.Lang.DataValidator.isFloat(temp.Text) && !string.IsNullOrEmpty(temp.Text))
                                {
                                    ShowError(temp.tip + "格式不正确");
                                    return false;
                                }
                                break;
                            }
                        case "isnumber":
                            {
                                if (!Library.Lang.DataValidator.isNumber(temp.Text) && !string.IsNullOrEmpty(temp.Text))
                                {
                                    ShowError(temp.tip + "格式不正确");
                                    return false;
                                }
                                break;
                            }
                        case "ishttpurl":
                            {
                                if (!Library.Lang.DataValidator.IsUrl(temp.Text) && !string.IsNullOrEmpty(temp.Text))
                                {
                                    ShowError(temp.tip + "格式不正确");
                                    return false;
                                }
                                break;
                            }
                    }
                }

            }
            return true;
        }
        #endregion

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

        #region 权限操作
        public string ModuleAdd
        {
            get { return ViewState["ModuleAdd"] as string; }
            set { ViewState["ModuleAdd"] = value; }
        }
        public string ModuleEdit
        {
            get { return ViewState["ModuleEdit"] as string; }
            set { ViewState["ModuleEdit"] = value; }
        }
        public string ModuleDelete
        {
            get { return ViewState["ModuleDelete"] as string; }
            set { ViewState["ModuleDelete"] = value; }
        }
        public string ModuleBrowse
        {
            get { return ViewState["ModuleBrowse"] as string; }
            set { ViewState["ModuleBrowse"] = value; }
        }
        public abstract void SetModuleTag();
        /// <summary>
        /// 是否有增加权限
        /// </summary>
        /// <returns></returns>
        public bool isAdd()
        {
            if (!SOSOshop.BLL.PowerPass.isPass(ModuleAdd))
            {
                ShowError(0);
            }
            return SOSOshop.BLL.PowerPass.isPass(ModuleAdd);
        }
        /// <summary>
        /// 是否有编辑权限
        /// </summary>
        /// <returns></returns>
        public bool isEdit()
        {
            if (!SOSOshop.BLL.PowerPass.isPass(ModuleEdit))
            {
                ShowError(0);
            }
            return SOSOshop.BLL.PowerPass.isPass(ModuleEdit);
        }
        /// <summary>
        /// 是否有删除权限
        /// </summary>
        /// <returns></returns>
        public bool isDelete()
        {
            if (!SOSOshop.BLL.PowerPass.isPass(ModuleDelete))
            {
                ShowError(0);
            }
            return SOSOshop.BLL.PowerPass.isPass(ModuleDelete);
        }
        /// <summary>
        /// 是否有查看权限
        /// </summary>
        /// <returns></returns>
        public bool isBrowse()
        {
            if (!SOSOshop.BLL.PowerPass.isPass(ModuleBrowse))
            {
                ShowError(0);
            }
            return SOSOshop.BLL.PowerPass.isPass(ModuleBrowse);
        }
        /// <summary>
        /// 判断是否有特别权限
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public bool isPass(string pass)
        {
            return SOSOshop.BLL.PowerPass.isPass(ModuleAdd);
        }

        /// <summary>
        /// 是否是管理员登陆
        /// </summary>
        /// <returns></returns>
        public static bool isAdmin()
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                FormsAuthenticationTicket ticket = id.Ticket;
                if ("admin".Equals(ticket.UserData.Split(',')[1]))//判断是前台登陆还是后台登陆
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
