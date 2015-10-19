using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace SOSOshop.Config
{
    /// <summary>
    /// 获取服务器及网站的相关信息
    /// </summary>
    public class ServerInfo
    {
        /// <summary>
        /// 取得网站的根目录的URL
        /// </summary>
        /// <returns></returns>
        public static string GetRootURI()
        {
            string appPath = "";
            HttpContext HttpCurrent = HttpContext.Current;
            HttpRequest Req;
            if (HttpCurrent != null)
            {
                Req = HttpCurrent.Request;
                string UrlAuthority = Req.Url.GetLeftPart(UriPartial.Authority);
                if (Req.ApplicationPath == null || Req.ApplicationPath == "/")
                {
                    appPath = UrlAuthority;
                }
                else
                {
                    appPath = UrlAuthority + Req.ApplicationPath;
                }
            }
            return appPath;
        }

        /// <summary>
        /// 获取网站的根目录的物理路径
        /// </summary>
        /// <returns></returns>
        public static string GetRootPath()
        {
            string appPath = "";
            HttpContext HttpCurrent = HttpContext.Current;
            if (HttpCurrent != null)
            {
                appPath = HttpCurrent.Server.MapPath("~/");
            }
            else
            {
                appPath = AppDomain.CurrentDomain.BaseDirectory;
                if (Regex.Match(appPath, @"\\$", RegexOptions.Compiled).Success)
                {
                    appPath = appPath.Substring(0, appPath.Length - 1);
                }
            }
            return appPath;
        }

        /// <summary>
        /// 获取网站目录路径
        /// </summary>
        /// <returns></returns>
        public static string GetAppPath()
        {
            if (HttpContext.Current.Request.ApplicationPath == "/")
            {
                return string.Empty;
            }
            else
            {
                return HttpContext.Current.Request.ApplicationPath;
            }
        }

        public static string GetServerPath()
        {
            return HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"];
        }
        /// <summary>
        /// 版本信息
        /// </summary>
        /// <returns></returns>
        public static string VersionInformation()
        {
            string vesion = "101Shop1.0";
            string filePath = GetRootPath() + "admin/vesion.ini";
            if (File.Exists(filePath))
                using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
                    vesion = sr.ReadToEnd();
            return vesion.Trim();
        }

        public static string GetDataTablePrefix()
        {
            return (System.Configuration.ConfigurationManager.AppSettings["DataTablePrefix"] == null ? "yxs_" : System.Configuration.ConfigurationManager.AppSettings["DataTablePrefix"]);
        }
    }
}
