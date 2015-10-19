using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
namespace SOSOshop.BLL.Logs
{
    /// <summary>
    /// 回收站（记录所有删除的数据）
    /// </summary>
    public class RecycleLog
    {
        MongoHelper<RecycleLog> db = new MongoHelper<RecycleLog>();
        [MongoDB.Bson.Serialization.Attributes.BsonId]
        public string id { get; set; }
        /// <summary>
        /// 删除对象的name
        /// </summary>
        public string ObjName { get; set; }
        /// <summary>
        /// 删除对象的值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime created { get; set; }
        public void Insert()
        {
            db.Insert(this);
        }
    }
}
