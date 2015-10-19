using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using AdvertisingManagement.BLL;
using AdvertisingManagement.Models;
using System.Text;
using System.IO;
using System.Web.Security;
using System.Reflection;
using System.Collections;

namespace AdvertisingManagement.Controllers
{
    public class ADController : Controller
    {
        //
        // GET: /AD/
        BLL.DbHelperSQL db = new BLL.DbHelperSQL();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddUser()
        {
            ViewBag.users = getUsers();
            ViewBag.user = getUserModel("dcyy");
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(User model)
        {
            return View();
        }


        #region 用户模块
        /// <summary>
        /// 取用户Model
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public User getUserModel(string no)
        {
            Models.User user = new Models.User();
            DataTable dt = getUserDataTable(string.Format("NetNo='{0}'", no));
            if (dt != null && dt.Rows.Count > 0)
            {
                user = DataConvert<User>.ToEntity(dt.Rows[0]);
                return user;
            }
            else
            {
                return null;
            }
        }

        public List<SelectListItem> getUserSelectList()
        {
            List<SelectListItem> itmes = new List<SelectListItem>();
            itmes.Add(new SelectListItem() { Text = "请选择", Value = "" });
            List<Models.User> users = new List<Models.User>();
            users = getUsers();
            if (users != null)
            {
                foreach (Models.User u in users)
                {
                    itmes.Add(new SelectListItem() { Text = u.NetName, Value = u.NetNo });
                }
            }
            return itmes;
        }

        /// <summary>
        /// 根据条件取用户列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable getUserDataTable(string where = null)
        {
            string sql = "select * from users";
            if (where != null)
            {
                sql = string.Format("{0} where {1}", sql, where);
            }
            DataTable dt = db.GetDS(sql).Tables[0];
            return dt;
        }

        /// <summary>
        /// 取用户对象列表
        /// </summary>
        /// <returns></returns>
        public List<User> getUsers()
        {
            DataTable dt = getUserDataTable();
            if (dt != null && dt.Rows.Count > 0)
            {
                return DataConvert<User>.ToList(dt);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string tableName, string whereFields, string fieldsValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select {0} from {1}", whereFields, tableName);
            strSql.AppendFormat(" where {0}={1}", whereFields, fieldsValue);
            DataTable obj = db.GetDS(strSql.ToString()).Tables[0];
            if (obj == null)
            {
                return false;
            }
            else
            {
                return 0 < obj.Rows.Count;
            }
        }

        public ActionResult Add_User(User user)
        {
            if (!Exists("users", "netno", "'" + user.NetNo + "'"))
            {
                string sql = string.Format("INSERT INTO users VALUES('{0}','{1}','{2}')", user.NetNo, user.NetName, user.NetKey);
                DbHelperSQL.ExecuteSql(sql);

            }
            return JavaScript("alert('OK');");
        }

        #endregion

        #region 广告配置模块
        /// <summary>
        /// 取用户Model
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public Ad_Config getConfigModel(int id)
        {
            Models.Ad_Config user = new Models.Ad_Config();
            DataTable dt = getConfigDataTable(string.Format("ConfigID={0}", id));
            if (dt != null && dt.Rows.Count > 0)
            {
                user = DataConvert<Ad_Config>.ToEntity(dt.Rows[0]);
                return user;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据条件取广告位列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable getConfigDataTable(string where = null)
        {
            string sql = "select * from ad_config";
            if (where != null)
            {
                sql = string.Format("{0} where {1}", sql, where);
            }
            DataTable dt = db.GetDS(sql).Tables[0];

            return dt;
        }

        public JsonResult GetConfigJson(int ConfigID)
        {
            return Json(getConfigModel(ConfigID));
        }

        /// <summary>
        /// 取广告位对象列表
        /// </summary>
        /// <returns></returns>
        public List<Ad_Config> getConfigs()
        {
            DataTable dt = getConfigDataTable();
            if (dt != null && dt.Rows.Count > 0)
            {
                return DataConvert<Ad_Config>.ToList(dt);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 取广告配置列表
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> getConfigSelectList(string userNo)
        {
            List<SelectListItem> itmes = new List<SelectListItem>();
            itmes.Add(new SelectListItem() { Text = "请选择", Value = "" });
            var users = getConfigs().Where((p) => p.NetNo == userNo).AsQueryable();
            foreach (Models.Ad_Config u in users)
            {
                itmes.Add(new SelectListItem() { Text = u.AdName, Value = u.ConfigID.ToString() });
            }
            return itmes;
        }

        /// <summary>
        /// 添加广告位
        /// </summary>
        /// <returns></returns>
        public ActionResult Config()
        {

            List<SelectListItem> itmes = new List<SelectListItem>();
            itmes.Add(new SelectListItem() { Text = "请选择", Value = "0" });
            itmes.Add(new SelectListItem() { Text = "JPEG", Value = "1" });
            itmes.Add(new SelectListItem() { Text = "FLASH", Value = "2" });

            this.ViewData["Resource"] = itmes;
            this.ViewData["NetName1"] = getUserSelectList();

            return View();

        }

        public ActionResult Config1()
        {
            List<SelectListItem> itmes = new List<SelectListItem>();
            itmes.Add(new SelectListItem() { Text = "请选择", Value = "0" });
            itmes.Add(new SelectListItem() { Text = "Jpeg", Value = "1" });
            itmes.Add(new SelectListItem() { Text = "Flash", Value = "2" });

            this.ViewData["Resource"] = itmes;
            this.ViewData["NetName1"] = getUserSelectList();

            DataTable dt = getConfigDataTable();
            string json = DataTableToJson("", dt);

            ViewBag.json = json;
            return View();
        }

        /// <summary>
        /// 存储广告位
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Config(Ad_Config config)
        {
            //this.ViewData["NetName1"] = getUserSelectList();
            string sql = "";
            if (!Exists("ad_config", "configid", config.ConfigID.ToString()))
            {
                sql = string.Format(@"INSERT INTO ad_config(AdName
                                          ,NetName
                                          ,Width
                                          ,Height
                                          ,Resource
                                          ,Channel
                                          ,NetNo) 
                                          VALUES('{0}','{1}',{2},{3},{4},'{5}','{6}')",
                    config.AdName,
                    config.NetName,
                    config.Width,
                    config.Height,
                    config.Resource,
                    config.Channel,
                    config.NetNo);
            }
            else
            {
                sql = string.Format(@"UPDATE ad_config
                                       SET AdName = '{0}'
                                          ,NetName = '{1}'
                                          ,Width = {2}
                                          ,Height = {3}
                                          ,Resource = {4}
                                          ,Channel = '{5}'
                                          ,NetNo = '{6}'
                                     WHERE ConfigID={7}"
                                           , config.AdName
                                           , config.NetName
                                           , config.Width
                                           , config.Height
                                           , config.Resource
                                           , config.Channel
                                           , config.NetNo
                                           , config.ConfigID);
            }
            DbHelperSQL.ExecuteSql(sql);
            return RedirectToAction("Config1", "Ad");
        }

        #endregion

        #region 广告内容管理模块
        /// <summary>
        /// 各站点添加配内容 
        /// </summary>
        /// <returns></returns>
        public ActionResult Contents()
        {

            ViewBag.data = DataConvert<Ad_Config>.ToList(getConfigTableInPicture());
            //this.ViewData["config"] = getConfigSelectList(netno);
            //this.ViewData["domain"] = domain;
            return View();

        }

        [HttpPost]
        public ActionResult Contents(Ad_Content content)
        {
            //if (!Exists("ad_content", "ContentID", content.ContentID.ToString()))
            string sql = "";
            if (content.ContentID == 0)
            {
                sql = string.Format(@"INSERT INTO ad_content([ConfigID]
                                          ,[Picture]
                                          ,[Url]
                                          ,[NetNo]
                                          ,[Detials]
                                          ,[Domain]  ) 
                                          VALUES({0},'{1}','{2}','{3}','{4}','{5}')",
                    content.ConfigID,
                    content.Picture,
                    content.Url,
                    content.NetNo,
                    content.Detials,
                    content.Domain);

            }
            else
            {
                sql = string.Format(@"UPDATE [ad_content] 
                                      SET [ConfigID] = {0} 
                                      ,[Picture] = '{1}' 
                                      ,[Url] = '{2}' 
                                      ,[NetNo] = '{3}' 
                                      ,[Detials] = '{4}'      
                                      ,Domain = '{5}'                                 
                                 WHERE ContentID={6}"
                                    , content.ConfigID
                                    , content.Picture
                                    , content.Url
                                    , content.NetNo
                                    , content.Detials
                                    , content.Domain
                                    , content.ContentID);

            } DbHelperSQL.ExecuteSql(sql);
            return Redirect("/Ad/Contents?netno=" + content.NetNo + "&domain=" + content.Domain);
        }

        /// <summary>
        /// 是否停用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool getIsStop(int id)
        {
            string sql = string.Format("select * from ad_content where ContentID={0}", id);
            DataSet ds = db.GetDS(sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["stoptime"] == DBNull.Value)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public JsonResult setStop(int contentId, int stop)
        {
            string sql = "";
            switch (stop)
            {
                case 0:
                    sql = string.Format("update ad_content set stoptime={0} where contentId={1}", "null", contentId);
                    break;
                case 1:
                    sql = string.Format("update ad_content set stoptime='{0}' where contentId={1}", DateTime.Now.ToLocalTime(), contentId);
                    break;
            }
            DbHelperSQL.ExecuteSql(sql);
            return Json(new { success = true });
        }


        /// <summary>
        /// 本方法返回的数据格式为jquery.ajax跨域调用的格式，请注意客户端使用的方法
        /// 在使用本函数的网站引用"/scripts/ad_loads.js"文件，在部署时间需修改"ad_loads.js"里的服务器地址
        /// </summary>
        /// <param name="id"></param>
        /// <param name="domain"></param>
        public void getContent(string id)
        {

            string callbackfun = Request["callbackfun"];
            //Response.Write(callbackfun + "({name:\"John\", message:\"hello John\"})");
            //Response.End();

            string sql = string.Format(@"SELECT  a.configid ,
                                                picture ,
                                                url ,
                                                width ,
                                                height
                                        FROM    ad_content a
                                                INNER JOIN dbo.ad_config b ON a.ConfigID = b.ConfigID
                                        WHERE   a.ConfigID IN ( {0} )
                                                and stoptime is null", id);
            string callbackFunName = Request["callbackparam"];
            try
            {
                DataTable dt = db.GetDS(sql).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    Response.Write(callbackfun + "(" + DataTableToJson("", dt) + ")");
                    //return Json(new { Success = true, FileName = "fileName", SaveName = "/Uploads/" });
                    //Response.Write("http://" + Request.Url.Host + ":" + Request.Url.Port + dt.Rows[0]["picture"]);
                }
                else
                {
                    Response.Write("no");
                }
            }
            catch
            {
                Response.Write(sql);
            }
            //return Json("", JsonRequestBehavior.AllowGet);
            //Response.Write("document.write('<img src=\""+Request.Url+"/Uploads/ad5600c5-b6cc-4ea6-9fe5-901135ce5a51.jpg\" width=80 height=90>')");

        }

        /// <summary>
        /// 根据条件取广告位列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable getConfigTableInPicture(string where = null)
        {
            string sql = string.Format(@"SELECT  a.* ,
        ISNULL(b.ContentID, 0) AS ContentID ,
        ISNULL(b.Picture, '') AS Picture ,
        ISNULL(b.Url, '') AS Url
FROM    ad_config a
        LEFT JOIN dbo.ad_content b ON a.ConfigID = b.ConfigID ");
            if (where != null)
            {
                sql = string.Format("{0} where {1}", sql, where);
            }
            DataTable dt = db.GetDS(sql).Tables[0];

            return dt;
        }

        #endregion

        #region 上传图片
        public ActionResult Upload()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Upload(HttpPostedFileBase fileData)
        {
            if (fileData != null)
            {
                try
                {
                    // 文件上传后的保存路径
                    string filePath = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    string fileName = Path.GetFileName(fileData.FileName);// 原始文件名称
                    string fileExtension = Path.GetExtension(fileName); // 文件扩展名
                    string saveName = Guid.NewGuid().ToString() + fileExtension; // 保存文件名称

                    fileData.SaveAs(filePath + saveName);

                    return Json(new { Success = true, FileName = fileName, SaveName = "/Uploads/" + saveName });
                }
                catch (Exception ex)
                {
                    return Json(new { Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {

                return Json(new { Success = false, Message = "请选择要上传的文件！" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion


        #region 用户登录
        private void login(string userNo)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                                userNo,
                                DateTime.Now,
                                DateTime.Now.AddHours(12),
                                false,
                                userNo,
                                FormsAuthentication.FormsCookiePath);
            string encTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie tk = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(tk);
            string json = ObjectToJson("", getConfigs());
        }
        #endregion

        //将datatable数据转换成JSON数据 
        public string DataTableToJson(string jsonName, DataTable dt)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append("[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString() + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]");
            return Json.ToString();
        }

        //列表数据转换到json数据 
        public string ObjectToJson<T>(string jsonName, IList<T> IL)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append("[");
            if (IL.Count > 0)
            {
                for (int i = 0; i < IL.Count; i++)
                {
                    T obj = Activator.CreateInstance<T>();
                    Type type = obj.GetType();
                    PropertyInfo[] pis = type.GetProperties();
                    Json.Append("{");
                    for (int j = 0; j < pis.Length; j++)
                    {
                        Json.Append("\"" + pis[j].Name.ToString() + "\":\"" + pis[j].GetValue(IL[i], null) + "\"");
                        if (j < pis.Length - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < IL.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]");
            return Json.ToString();
        }

    }
}
