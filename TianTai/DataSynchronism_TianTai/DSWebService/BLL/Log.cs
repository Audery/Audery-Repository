using System;
using MongoDB;
using System.Linq;
using System.Collections.Generic;
namespace DSWebService.BLL
{

    [Serializable]
    public class Log
    {
        public Log()
        { }
        #region Model
        /// <summary>
        /// 
        /// </summary>
        [MongoDB.Attributes.MongoId]
        public string id { get; set; }
        /// <summary>
        /// 操作IP地址
        /// </summary>
        public string ip { get; set; }
        /// <summary>
        /// 事件类型0错误,1消息,2同步,500服务器端错误,404客户端错误
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 事件来源
        /// </summary>
        public string source { get; set; }
        /// <summary>
        /// 事件描述
        /// </summary>
        public string describe { get; set; }
        /// <summary>
        /// 操作人员编号
        /// </summary>
        public int userid { get; set; }
        /// <summary>
        /// 管理员帐号
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 错误详细
        /// </summary>
        public string detail { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime created { get; set; }
        #endregion Model

        public void insert(Log model)
        {

            using (MDbBase db = new MDbBase())
            {
                model.id = MongoDB.Oid.NewOid().ToString();
                db.GetCollection<Log>().Insert(model);
            }
        }

        public Log GetModel(string id)
        {
            using (MDbBase db = new MDbBase())
            {
                return db.GetCollection<Log>().FindOne(t => t.id == id);
            }
        }

        public long GetCount()
        {
            using (MDbBase db = new MDbBase())
            {
                return db.GetCollection<MDbBase>().Count();
            }
        }

        public List<Log> GetList(int PageSize, int PageIndex, out int recordCount, out int pageCount, bool order, string orderField, bool like, string whereField, string whereString, string type)
        {
            using (MDbBase db = new MDbBase())
            {
                var q = db.GetCollection<Log>().Linq();
                int typeint;
                if (int.TryParse(type, out typeint))
                {
                    q = q.Where(x => x.type == typeint);
                }
                if (!string.IsNullOrEmpty(username))
                {
                    q = q.Where(x => x.username == username);
                }

                if (!string.IsNullOrEmpty(whereString))
                {
                    if (like)
                    {
                        switch (whereField)
                        {
                            case "describe":
                                {
                                    q = q.Where(x => x.describe == whereString);
                                    break;
                                }
                            case "ip":
                                {
                                    q = q.Where(x => x.ip == whereString);
                                    break;
                                }
                            case "source":
                                {
                                    q = q.Where(x => x.source == whereString);
                                    break;
                                }
                        }
                    }
                    else
                    {
                        switch (whereField)
                        {
                            case "describe":
                                {
                                    q = q.Where(x => x.describe.Contains(whereString));
                                    break;
                                }
                            case "ip":
                                {
                                    q = q.Where(x => x.ip.Contains(whereString));
                                    break;
                                }
                            case "source":
                                {
                                    q = q.Where(x => x.source.Contains(whereString));
                                    break;
                                }
                        }
                    }
                }
                recordCount = q.Count();
                pageCount = recordCount / PageSize;
                if ((recordCount % PageSize) != 0) pageCount++;
                return q.OrderByDescending(x => x.created).Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();
            }
        }

        public void Delete(string describe)
        {
            using (MDbBase db = new MDbBase())
            {
                db.GetCollection<Log>().Remove(x => x.describe == describe);
            }
        }

        public void Update(Log model)
        {
            using (MDbBase db = new MDbBase())
            {
                string id = db.GetCollection<Log>().Linq().Where(x => x.describe == model.describe).First().id;
                db.GetCollection<Log>().Update(model, x => x.id == id);
            }
        }

        public string test()
        {
            using (MDbBase db = new MDbBase())
            {
                var dt = new BLL.DbBase().ExecuteTable("select top 10 * from yy_User");
                db.GetCollection<System.Data.DataTable>().Insert(dt);
                return "";
            }
        }
        public void DeleteAll()
        {
            using (MDbBase db = new MDbBase())
            {
                db.GetCollection<Log>().Remove(true);
            }
        }
        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="describe"></param>
        /// <returns></returns>
        public bool Exist(string describe)
        {
            using (MDbBase db = new MDbBase())
            {
                return db.GetCollection<Log>().Find(x => x.describe == describe).Documents.Count() > 0;
            }
        }
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="type">0错误，1消息,2同步,500服务器端错误,404更新服务消息</param>
        public static void AddLog(string msg, int type)
        {
            try
            {
                BLL.Log bll = new BLL.Log();
                bll.created = DateTime.Now;
                bll.describe = msg;
                bll.detail = "";
                bll.ip = "";
                bll.source = "";
                bll.type = type;
                bll.insert(bll);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(Environment.CurrentDirectory + "\t.txt", ex.ToString());
            }
        }
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="type">0错误，1消息,2同步,3操作日志，500服务器端错误,404更新服务消息</param>
        public static void AddLog(string msg, int type, string username)
        {
            BLL.Log bll = new BLL.Log();
            bll.created = DateTime.Now;
            bll.describe = msg;
            bll.detail = "";
            bll.ip = "";
            bll.source = "";
            bll.type = type;
            bll.username = username;
            bll.insert(bll);

        }
    }
}

