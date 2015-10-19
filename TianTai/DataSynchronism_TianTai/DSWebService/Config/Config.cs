using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;

namespace DSWebService.Config
{
    public class Config
    {
        /// <summary>
        /// 缓存服务器群集
        /// </summary>
        public static string ServerList
        {
            get
            {
                return ConfigurationManager.AppSettings["ServerList"];
                //if (HttpContext.Current.Cache["ServerList"] == null)
                //{
                //    DataCache.SetCache("ServerList", ConfigurationManager.AppSettings["ServerList"]);
                //}
                //return DataCache.GetCache("ServerList").ToString();
            }
        }

        /// <summary>
        /// 允许上传文件的最大大小
        /// </summary>
        public static int MaxFileUploadLength
        {
            get
            {
                return 3 * 1024 * 1024;
            }
        }
        /// <summary>
        /// 允许上传文件的后缀名
        /// </summary>
        public static string[] FileUploadExpand
        {
            get
            {
                string[] Expand = { "jpg", "gif", "jpeg", "png" };
                return Expand;
            }
        }
        /// <summary>
        /// 网站常规数据缓存时间以分钟为单位
        /// </summary>
        public static int MemcachedCacheTime
        {
            get
            {
                return 60;
            }
        }
        /// <summary>
        /// 网站缓存数据过期时间
        /// </summary>
        public static DateTime MemcachedTime
        {
            get
            {
                return DateTime.Now.AddMinutes(MemcachedCacheTime);
            }
        }
        /// <summary>
        /// Mongo连接字符串
        /// </summary>
        public static string MongoConnectionString
        {

            get
            {
                return ConfigurationManager.AppSettings["MongoConnectionString"];
                //if (HttpContext.Current.Cache["MongoConnectionString"] == null)
                //{
                //    DataCache.SetCache("MongoConnectionString", ConfigurationManager.AppSettings["MongoConnectionString"]);
                //}
                //return DataCache.GetCache("MongoConnectionString").ToString();
            }
        }
        /// <summary>
        /// Mongo数据库
        /// </summary>
        public static string MongoName
        {
            get
            {
                return ConfigurationManager.AppSettings["MongoName"];               
            }
        }
        /// <summary>
        /// 任务变动标识
        /// </summary>
        public static string TaskKey
        {
            get
            {
                return "EBE3BB15EE";
            }
        }
    }
}
