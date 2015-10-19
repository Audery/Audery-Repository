using System;
using MongoDB;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
namespace SOSOshop.BLL.Logs
{

    public class Log
    {
        MongoHelper<SOSOshop.Model.Logs.Log> db = null;
        public Log(string collectionName)
        {
            db = new MongoHelper<SOSOshop.Model.Logs.Log>(collectionName);
        }
        public void insert(SOSOshop.Model.Logs.Log model)
        {
            db.Insert(model);
        }
        /// <summary>
        /// 转换到价格变动数据库
        /// </summary>
        public Log()
        {
            db = new MongoHelper<SOSOshop.Model.Logs.Log>();
            string PriceLog_MongoName = System.Configuration.ConfigurationManager.AppSettings["PriceLog_MongoName"];
            db.ChangeDB(PriceLog_MongoName);
        }
        public SOSOshop.Model.Logs.Log GetModel(string id)
        {
            return db._mongoCollection.AsQueryable().Where(x => x.id == id).First();
        }

        public SOSOshop.Model.Logs.Log GetModel(int userid, string describe)
        {
            var sel = db._mongoCollection.AsQueryable().Where(x => x.userid == userid && x.describe.Contains(describe));
            return sel.Count() > 0 ? sel.Last() : null;
        }

        public long GetCount()
        {
            return db._mongoCollection.Count();
        }
        public List<SOSOshop.Model.Logs.Log> GetList(int PageSize, int PageIndex, out int recordCount, out int pageCount, bool order, string orderField, bool like, string whereField, string whereString, string type)
        {
            IMongoQuery q = null;
            if (type != "-1" && type != "-100")
            {
                q = Query.EQ("type", int.Parse(type));
            }
            if (type == "-100")
            {
                q = Query.And(Query.EQ("type", 3), Query.Matches("describe", "商品价格发生变化"));
            }
            if (!string.IsNullOrEmpty(whereString))
            {
                if (like)
                {
                    if (q != null)
                    {
                        q = Query.And(q, Query.EQ(whereField, whereString));
                    }
                    else
                    {
                        q = Query.EQ(whereField, whereString);
                    }
                }
                else
                {
                    if (q != null)
                    {
                        q = Query.And(q, Query.Matches(whereField, whereString));
                    }
                    else
                    {
                        q = Query.Matches(whereField, whereString);
                    }
                }
            }
            recordCount = (int)db._mongoCollection.Count(q);
            pageCount = recordCount / PageSize;
            if ((recordCount % PageSize) != 0) pageCount++;
            return db._mongoCollection.Find(q).OrderByDescending(x => x.created).Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

        }

        public void Delete(string id)
        {
            db._mongoCollection.Remove(Query.EQ("_id", id));
        }

        public void DeleteAll()
        {
            db._mongoCollection.Remove(MongoDB.Driver.Builders.Query.Or(MongoDB.Driver.Builders.Query.EQ("type", 0), MongoDB.Driver.Builders.Query.EQ("type", 2)), MongoDB.Driver.RemoveFlags.None, MongoDB.Driver.SafeMode.False);
            db._mongoCollection.Remove(MongoDB.Driver.Builders.Query.LT("created", DateTime.Now.AddMonths(-3)), MongoDB.Driver.RemoveFlags.None, MongoDB.Driver.SafeMode.False);
        }
        /// <summary>
        /// 前台日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="UserId"></param>
        /// <param name="UserName"></param>
        /// <param name="type">日志类型0系统日志,1 用户操作日志,2 异常日志,3 HttpCode404日志</param>
        public static void LogShopAdd(string msg, string detail, int UserId, string UserName, int type = 2)
        {
            try
            {

                SOSOshop.Model.Logs.Log model = new SOSOshop.Model.Logs.Log()
                {
                    created = DateTime.Now,
                    describe = msg,
                    ip = System.Web.HttpContext.Current.Request.UserHostAddress,
                    source = System.Web.HttpContext.Current.Request.Url.ToString(),
                    type = type,
                    userid = UserId,
                    username = UserName,
                    detail = detail
                };
                new Log("LogShop").insert(model);
            }
            catch { }

        }
        /// <summary>
        /// 后台用户操作日志日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="UserId"></param>
        /// <param name="UserName"></param>
        /// <param name="type">日志类型0系统日志,1 用户操作日志,2 异常日志,3 HttpCode404日志</param>
        public static void LogAdminAdd(string msg, int UserId, string UserName, int type = 1)
        {
            try
            {

                SOSOshop.Model.Logs.Log model = new SOSOshop.Model.Logs.Log()
                {
                    created = DateTime.Now,
                    describe = msg,
                    ip = System.Web.HttpContext.Current.Request.UserHostAddress,
                    source = System.Web.HttpContext.Current.Request.Url.ToString(),
                    type = type,
                    userid = UserId,
                    username = UserName
                };
                new Log("LogAdmin").insert(model);
            }
            catch { }
        }
        /// <summary>
        /// 服务日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="UserId"></param>
        /// <param name="UserName"></param>
        /// <param name="type">日志类型0系统日志,1 用户操作日志,2 异常日志,3 HttpCode404日志</param>
        public static void LogServiceAdd(string msg, int UserId, string UserName, string source, string detail, int type = 1)
        {
            try
            {

                SOSOshop.Model.Logs.Log model = new SOSOshop.Model.Logs.Log()
                {
                    created = DateTime.Now,
                    describe = msg,
                    type = type,
                    userid = UserId,
                    username = UserName,
                    detail = detail,
                    source = source
                };
                new Log("LogService").insert(model);
            }
            catch { }
        }

    }
}

