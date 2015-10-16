using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.Security;
using System.Web;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Net;
using System.IO;
using System.Threading.Tasks;
namespace SOSOshop.BLL.Common
{
    public static class Public
    {
        /// <summary>
        /// 取得规格显示方式
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static string GetSpecification(DataRow dr)
        {
            return SOSOshop.BLL.Order.OrderProduct.GetSpecification(dr).Trim('x');
        }
        /// <summary>
        /// 取得规格显示方式
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static DataTable GetSpecification(this DataTable dt)
        {
            foreach (DataRow item in dt.Rows)
            {
                item["DrugsBase_Specification"] = GetSpecificationAndS(item).Trim('x');
            }
            return dt;
        }
        /// <summary>
        /// 取得规格显示方式
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static string GetSpecificationAndS(DataRow dr)
        {
            // return dr["DrugsBase_Specification"] as string;
            SOSOshop.BLL.Product.Product bll = new SOSOshop.BLL.Product.Product();
            //中药饮片
            //if (bll.IsZyyp((int)dr["Product_ID"]))            
            if (dr.Table.Columns.Contains("is_ZYC") && (int)dr["is_ZYC"] == 1)
            {
                return string.Format("{0}", dr["DrugsBase_Specification"]);
            }
            return string.Format("{0}x{1}", dr["DrugsBase_Specification"], SOSOshop.BLL.Order.OrderProduct.GetSpecification(dr)).TrimStart('x');
        }

        /// <summary>
        /// 取得网站图片地址
        /// </summary>
        /// <param name="ImagePath"></param>
        /// <returns></returns>
        public static string RawImage(object ImagePath)
        {
            if (Library.Lang.DataValidator.isNULL(ImagePath))
            {
                return "/images/nopic1.jpg";
            }
            string imageUrl = System.Configuration.ConfigurationManager.AppSettings["imageUrl"];
            string iden = System.Configuration.ConfigurationManager.AppSettings["Iden"];
            string ImageUrl_101 = imageUrl + iden + "/" + ImagePath;

            string BasePath = HttpContext.Current.Server.MapPath("/images");
            if (!Directory.Exists(BasePath))//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(BasePath);
            }
            else
            {
                string TempPath = BasePath + "\\" + ImagePath.ToString().Trim().Replace('/', '\\');
                //判断图片是否存在
                if (File.Exists(TempPath))
                {
                    return "/images/" + ImagePath;
                }
                else
                {
                    Task.Factory.StartNew(() => DownloadImage(ImageUrl_101, TempPath));
                    //去101下载图片

                }
            }
            return ImageUrl_101;
        }

        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="Uri"></param>
        /// <param name="FilePath"></param>
        private static void DownloadImage(string Uri, string FilePath)
        {
            WebClient client = new WebClient();

            try
            {
                string DirectoryName = Path.GetDirectoryName(FilePath);
                if (!Directory.Exists(DirectoryName))
                {
                    Directory.CreateDirectory(DirectoryName);
                }
                client.DownloadFile(Uri, FilePath);
            }
            catch (Exception)
            {                
               
            }
            
        }

        /// <summary>
        /// 处理药品广告语
        /// </summary>
        /// <param name="addvString"></param>
        /// <returns></returns>
        public static string DealAddvertismentString(string addvString)
        {
            string resultString = string.Empty;

            if (addvString.Trim().Equals(""))
            {
                resultString = "";
            }
            else
            {
                resultString = "【" + Library.Lang.Input.GetSubString(addvString, 24) + "】";
            }

            return resultString;
        }
        /// <summary>
        /// 取得商品标签
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static string Tag(DataRow dr)
        {
            return "";
            SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();
            StringBuilder sb = new StringBuilder();

            //拆零
            bool is_cl = (string)dr["is_cl"] == "是";
            //控销
            bool is_kong = (decimal)dr["price_03"] > 0;
            //2012基药标签
            bool is_Jy = ((string)dr["tag_ids"]).Contains(",66,");
            //包含中标价、调入挂网价

            bool is_Zb = !dr.IsNull("zbj") && decimal.Parse(dr["zbj"].ToString()) > 0;
            bool is_Dr = !dr.IsNull("drj") && decimal.Parse(dr["drj"].ToString()) > 0;

            if (is_cl || is_kong || is_Jy || is_Zb || is_Dr)
            {
                sb.Append(" <div class='detail_img_ico'>");
                if (is_Jy)
                {
                    sb.Append(" <span class=\"bkjy_ico bkjy_ico_1\">520</span>");
                }
                if (is_Zb)
                {
                    sb.Append(" <span class=\"bkjy_ico bkjy_ico_2\">中标基</span>");
                }
                if (is_Dr)
                {
                    sb.Append(" <span class=\"bkjy_ico bkjy_ico_3\">调入基</span>");
                }
                if (is_cl)
                {
                    sb.Append(" <span class='bkcl_ico'>拆零</span>");
                }
                if (is_kong)
                {
                    sb.Append(" <span class='kx_ico'>控</span>");
                }

                sb.Append("</div>");
            }
            return sb.ToString();
        }

        public static List<T> List<T>(this DataTable dt)
        {
            var list = new List<T>();
            Type t = typeof(T);
            var plist = new List<PropertyInfo>(typeof(T).GetProperties());

            foreach (DataRow item in dt.Rows)
            {
                T s = System.Activator.CreateInstance<T>();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PropertyInfo info = plist.Find(p => p.Name == dt.Columns[i].ColumnName);
                    if (info != null)
                    {
                        if (!Convert.IsDBNull(item[i]))
                        {
                            info.SetValue(s, item[i], null);
                        }
                    }
                }
                list.Add(s);
            }
            return list;
        }

        /// <summary>
        /// 取得用户登陆的Id
        /// </summary>
        /// <returns></returns>
        public static int GetUserId()
        {
            int uid = 0;
            if (System.Web.HttpContext.Current.Request.IsAuthenticated)
            {
                FormsIdentity id = (FormsIdentity)System.Web.HttpContext.Current.User.Identity;
                FormsAuthenticationTicket ticket = id.Ticket;
                int.TryParse(ticket.UserData, out uid);
            }
            return uid;
        }
        /// <summary>
        /// 取得用户登陆的Code
        /// </summary>
        /// <returns></returns>
        public static string GetUserCode()
        {
            SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();
            string sql = "SELECT Code FROM dbo.memberinfo WHERE UID=" + GetUserId();
            return (string)db.ExecuteScalarForCache(sql);
        }
        /// <summary>
        /// 取得会员应该执行的价格类型
        /// </summary>
        /// <returns></returns>
        public static string GetPriceCategory()
        {
            SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();
            string sql = "SELECT PriceCategory FROM dbo.memberinfo WHERE UID=" + GetUserId();
            return (string)db.ExecuteScalarForCache(sql);
        }
        /// <summary>
        /// 取热门搜索列表
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetHotSearch()
        {
            string sql = "select top 5 name,linkad from yxs_topsearches where isshow=1 order by [sort]";
            SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();
            DataTable dt = db.ExecuteTableForCache(sql, DateTime.Now.AddHours(1));
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic = dt.AsEnumerable().ToDictionary(x => x.Field<string>("name"), x => x.Field<string>("linkad"));
            return dic;
        }

        /// <summary>
        /// 取网站配置信息
        /// </summary>
        /// <returns></returns>
        public static WebSiteInfo GetNetSiteInfo()
        {
            //WebSiteInfo wsi = new WebSiteInfo();
            string sql = @"SELECT TOP 1 [id]
                              ,[websitetitle]
                              ,[tel]
                              ,[fax]
                              ,[email]
                              ,[metekey]
                              ,[meteinfo]
                              ,[websitename]
                              ,[usersagreement]
                              ,[websitedomain]
                          FROM [yxs_websetting]";
            SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();
            DataTable dt = db.ExecuteTableForCache(sql, DateTime.Now.AddDays(1));
            IList<WebSiteInfo> list = dt.AsEnumerable().Select(x =>
                new WebSiteInfo
                {
                    NetName = x.Field<string>("websitename"),
                    Domain = System.Configuration.ConfigurationManager.AppSettings["CompanyDomainName"],
                    NetTitle = x.Field<string>("websitetitle"),
                    Phone = x.Field<string>("tel"),
                    Fax = x.Field<string>("fax"),
                    Email = x.Field<string>("email"),
                    Keys = x.Field<string>("metekey"),
                    NetInfo = x.Field<string>("meteinfo"),
                    NetProtocol = x.Field<string>("usersagreement")
                }).ToList();

            return list[0];
        }

        /// <summary>
        /// 网站配置结构
        /// </summary>
        public struct WebSiteInfo
        {
            /// <summary>
            /// 网站名称
            /// </summary>
            public string NetName;
            /// <summary>
            /// 网站域名
            /// </summary>
            public string Domain;
            /// <summary>
            /// 网站标题
            /// </summary>
            public string NetTitle;
            /// <summary>
            /// 联系电话
            /// </summary>
            public string Phone;
            /// <summary>
            /// 联系传真
            /// </summary>
            public string Fax;
            /// <summary>
            /// 电子邮件
            /// </summary>
            public string Email;
            /// <summary>
            /// 网站关键字
            /// </summary>
            public string Keys;
            /// <summary>
            /// 网站描述
            /// </summary>
            public string NetInfo;
            /// <summary>
            /// 网站协议
            /// </summary>
            public string NetProtocol;

        }

        /// <summary>
        /// 根据 User Agent 获取操作系统名称
        /// </summary>
        public static string GetOSNameByUserAgent(string userAgent)
        {
            string osVersion = userAgent;
            if (osVersion == null) return "";
            if (userAgent.Contains("NT 6.0"))
            {
                osVersion = "Windows Vista/Server 2008";
            }
            else if (userAgent.Contains("NT 6.1"))
            {
                osVersion = "Windows 7/Server 2008 R2";
            }
            else if (userAgent.Contains("NT 6.2"))
            {
                osVersion = "Windows 8";
            }
            else if (userAgent.Contains("NT 6.3"))
            {
                osVersion = "Windows 8.1/Server 2012 R2";
            }
            else if (userAgent.Contains("NT 5.2"))
            {
                osVersion = "Windows Server 2003";
            }
            else if (userAgent.Contains("NT 5.1"))
            {
                osVersion = "Windows XP";
            }
            else if (userAgent.Contains("NT 5"))
            {
                osVersion = "Windows 2000";
            }
            else if (userAgent.Contains("NT 4"))
            {
                osVersion = "Windows NT4";
            }
            else if (userAgent.Contains("Me"))
            {
                osVersion = "Windows Me";
            }
            else if (userAgent.Contains("98"))
            {
                osVersion = "Windows 98";
            }
            else if (userAgent.Contains("95"))
            {
                osVersion = "Windows 95";
            }
            else if (userAgent.Contains("Mac"))
            {
                osVersion = "Mac";
            }
            else if (userAgent.Contains("Unix"))
            {
                osVersion = "UNIX";
            }
            else if (userAgent.Contains("Linux"))
            {
                osVersion = "Linux";
            }
            else if (userAgent.Contains("SunOS"))
            {
                osVersion = "SunOS";
            }
            else if (userAgent.Contains("Android"))
            {
                osVersion = "Android";
            }
            return osVersion;
        }
        /// <summary>
        /// 判断这个访问是否是搜索引擎过来的
        /// </summary>
        /// <returns></returns>
        public static bool IsSearchEngines()
        {
            if (HttpContext.Current.Request.UrlReferrer == null)
            {
                return false;
            }
            string[] SearchEngine = { "google", "yahoo", "msn", "baidu", "sogou", "sohu", "sina", "163", "lycos", "tom", "yisou", "iask", "soso", "gougou", "zhongsou", "bing" };
            string tmpReferrer = HttpContext.Current.Request.UrlReferrer.ToString().ToLower();
            for (int i = 0; i < SearchEngine.Length; i++)
            {
                if (tmpReferrer.IndexOf(SearchEngine[i]) >= 0)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 判断这个访问是否是搜索引擎过来的
        /// </summary>
        /// <returns></returns>
        public static string IsSearchEnginesGet()
        {
            if (HttpContext.Current.Request.UserAgent == null)
            {
                return null;
            }
            string[] SearchEngine = { "google", "yahoo", "msn", "baidu", "sogou", "sohu", "sina", "163", "lycos", "tom", "yisou", "iask", "soso", "gougou", "zhongsou", "bing" };
            string tmpReferrer = HttpContext.Current.Request.UserAgent.ToLower();
            //System.IO.File.AppendAllText(HttpContext.Current.Server.MapPath("/crm/log.txt"), tmpReferrer + "\r\n");
            for (int i = 0; i < SearchEngine.Length; i++)
            {
                if (tmpReferrer.IndexOf(SearchEngine[i]) != -1)
                {
                    return SearchEngine[i];
                }
            }
            return null;
        }

        /// <summary>
        /// 判断是否为数字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInt(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*$");
        }
    }
}
