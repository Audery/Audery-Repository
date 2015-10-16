using System;
using MongoDB;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;

namespace SOSOshop.BLL.Report
{
    public class DrugTestingReportDownloadCount
    {
        MongoHelper<DrugTestingReportDownloadCount> db = new MongoHelper<DrugTestingReportDownloadCount>();
        public string id { get; set; }
        public string DrugTestingReport_Id { get; set; }
        public int uid { get; set; }
        public DateTime Created { get; set; }

        /// <summary>
        /// 取得下载的次数
        /// </summary>
        /// <returns></returns>
        public static long GetDowCount(string DrugTestingReport_Id, int uid)
        {
            MongoHelper<DrugTestingReportDownloadCount> db = new MongoHelper<DrugTestingReportDownloadCount>();
            return db._mongoCollection.Find(Query.And(Query.EQ("DrugTestingReport_Id", DrugTestingReport_Id), Query.EQ("uid", uid))).Count();
        }
        public static string[] GetDrugTestingReport_Id(int uid)
        {
            List<string> ids = new List<string>();
            MongoHelper<DrugTestingReportDownloadCount> db = new MongoHelper<DrugTestingReportDownloadCount>();
            return db._mongoCollection.Find(Query.EQ("uid", uid)).Select<DrugTestingReportDownloadCount, string>(x => x.DrugTestingReport_Id).ToArray();
        }
        /// <summary>
        /// 取得下载的次数
        /// </summary>
        /// <returns></returns>
        public static long[] GetDowCount(string[] DrugTestingReport_Id, int uid)
        {
            MongoHelper<DrugTestingReportDownloadCount> db = new MongoHelper<DrugTestingReportDownloadCount>();
            List<long> vals = new List<long>();
            List<MongoDB.Bson.BsonValue> values = new List<MongoDB.Bson.BsonValue>();
            if (DrugTestingReport_Id != null)
            {
                Dictionary<string, long> dic = new Dictionary<string, long>();
                foreach (string s in DrugTestingReport_Id) values.Add(s);
                foreach (DrugTestingReportDownloadCount model in db._mongoCollection.Find(Query.And(Query.In("DrugTestingReport_Id", values), Query.EQ("uid", uid))))
                {
                    if (dic.ContainsKey(model.DrugTestingReport_Id))
                    {
                        dic[model.DrugTestingReport_Id] = dic[model.DrugTestingReport_Id] + 1;
                    }
                    else
                    {
                        dic.Add(model.DrugTestingReport_Id, 1);
                    }
                }
                foreach (string s in values)
                {
                    vals.Add(dic.ContainsKey(s) ? dic[s] : 0);
                }
            }
            return vals.ToArray();
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
        /// <param name="Products_Id"></param>
        /// <param name="pihao"></param>
        /// <returns></returns>
        public bool Exist(int uid, string DrugTestingReport_Id)
        {
            return db._mongoCollection.Count(Query.And(Query.EQ("DrugTestingReport_Id", DrugTestingReport_Id), Query.EQ("uid", uid))) > 0;
        }

    }
}
