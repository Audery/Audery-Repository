using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;

namespace SOSOshop.BLL
{
    /// <summary>
    /// 搜索引擎记录
    /// </summary>
    public class SearchEngines
    {
        MongoHelper<SearchEngines> db = null;
        public SearchEngines()
        {
            db = new MongoHelper<SearchEngines>();
        }

        [MongoDB.Bson.Serialization.Attributes.BsonId]
        public string id { get; set; }
        public string Engines { get; set; }
        public string ip { get; set; }
        public DateTime created { get; set; }
        public void insert()
        {
            db.Insert(this);
        }
        /// <summary>
        /// 判断是否可以查看
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public bool isPower(string Engines)
        {
            try
            {
                return db._mongoCollection.Find(Query.And(Query.EQ("Engines", Engines), Query.GT("created", DateTime.Now.AddSeconds(-10)))).Count() < 11;
            }
            catch { }
            return true;
        }
    }
}
