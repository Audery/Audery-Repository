using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
namespace SOSOshop.BLL.Service
{
    /// <summary>
    /// 是否发送送短信记录
    /// </summary>
    public class MsgLog
    {
        MongoHelper<MsgLog> db = new MongoHelper<MsgLog>();
        [MongoDB.Bson.Serialization.Attributes.BsonId]
        public string id { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string orderid { get; set; }
        /// <summary>
        /// 是否发送已经通知发货的短信
        /// </summary>
        public bool fhsend { get; set; }
        public void Insert()
        {
            if (orderid.Contains("-"))
            {
                orderid = orderid.Split('-')[0];
            }
            db.Insert(this);
        }
        /// <summary>
        /// 判断是否已经发送过发货短信
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public bool isfhsend(string orderid)
        {
            if (orderid.Contains("-"))
            {
                orderid = orderid.Split('-')[0];
            }
            return (db._mongoCollection.Count(Query.And(Query.EQ("orderid", orderid), Query.EQ("fhsend", true))) > 0);
        }
    }
}
