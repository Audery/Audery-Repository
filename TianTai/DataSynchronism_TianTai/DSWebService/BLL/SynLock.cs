using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB;
namespace DSWebService.BLL
{
    /// <summary>
    /// 系统状态锁
    /// </summary>
    public class SynLock
    {
        [MongoDB.Attributes.MongoId]
        public string id { get; set; }
        /// <summary>
        /// 系统锁定状态
        /// </summary>
        public bool lck { get; set; }

        /// <summary>
        /// 系统加锁
        /// </summary>
        public void Lock()
        {
            using (MDbBase db = new MDbBase())
            {
                this.id = "1";
                this.lck = true;
                if (db.GetCollection<SynLock>().Count(x => x.id == "1") == 0)
                {
                    db.GetCollection<SynLock>().Insert(this);
                }
                else
                {
                    db.GetCollection<SynLock>().Save(this);
                }

            }
        }
        /// <summary>
        /// 系统解锁
        /// </summary>
        public void UnLock()
        {
            using (MDbBase db = new MDbBase())
            {
                this.id = "1";
                this.lck = false;
                if (db.GetCollection<SynLock>().Count(x => x.id == "1") == 0)
                {
                    db.GetCollection<SynLock>().Insert(this);
                }
                else
                {
                    db.GetCollection<SynLock>().Save(this);
                }

            }
        }
        public bool GetLock()
        {
            using (MDbBase db = new MDbBase())
            {
                if (db.GetCollection<SynLock>().Count(x => x.id == "1") == 0)
                {
                    return false;
                }
                else
                {
                    return db.GetCollection<SynLock>().FindOne(x => x.id == "1").lck;
                }
            }
        }
    }
}