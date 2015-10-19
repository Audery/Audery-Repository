using System;
using MongoDB;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
using System.Data;
namespace SOSOshop.BLL.Member
{
    public class MemberinfoTemp
    {
        [MongoDB.Bson.Serialization.Attributes.BsonId]
        public string id { get; set; }
        public List<DataRow> Memberinfo { get; set; }
        public void Insert()
        {
            MongoHelper<MemberinfoTemp> db = new MongoHelper<MemberinfoTemp>();
            db.Insert(this);
        }
    }
}
