using System;
using MongoDB;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;

namespace SOSOshop.BLL.Report
{
    public class DrugTestingReportDownloadOrder
    {
        MongoHelper<DrugTestingReportDownloadOrder> db = new MongoHelper<DrugTestingReportDownloadOrder>();
        public string id { get; set; }
        public string OrderId { get; set; }
        public int uid { get; set; }
        public DateTime Created { get; set; }

        /// <summary>
        /// 取得下载的次数
        /// </summary>
        /// <returns></returns>
        public static long GetDowCount(string OrderId, int uid)
        {
            MongoHelper<DrugTestingReportDownloadOrder> db = new MongoHelper<DrugTestingReportDownloadOrder>();
            return db._mongoCollection.Find(Query.And(Query.EQ("OrderId", OrderId), Query.EQ("uid", uid))).Count();
        }

        /// <summary>
        /// 增加
        /// </summary>
        public void Insert()
        {
            this.id = MongoDB.Bson.BsonObjectId.GenerateNewId().ToString();
            Created = DateTime.Now;
            db._mongoCollection.Insert(this, SafeMode.True);
        }
        /// <summary>
        /// 判断是否下载
        /// </summary>
        /// <returns></returns>
        public bool Exist(int uid, string OrderId)
        {
            return db._mongoCollection.Count(Query.And(Query.EQ("OrderId", OrderId), Query.EQ("uid", uid))) > 0;
        }

    }
}
