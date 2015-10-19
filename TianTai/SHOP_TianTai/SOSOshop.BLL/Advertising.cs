using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB;
using MongoDB.Driver.Builders;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace SOSOshop.BLL
{
    /// <summary>
    /// 前台商品广告位管理
    /// </summary>
    [Serializable]
    public class Advertising
    {
        [BsonId]
        public string id { get; set; }
        /// <summary>
        /// 广告位编码，由前台设计页面时指定
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 栏目名称，如“疯狂抢购”。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 商品ID列表
        /// </summary>
        public List<int> ProductID { get; set; }

        /// <summary>
        /// 该广告位所在站点的域名：如baidu.com，不能带前面的www
        /// </summary>
        public string Domin { get; set; }

        MongoHelper<Advertising> db = new MongoHelper<Advertising>();
       
        private void insert()
        {  
            db.Insert(this);
        }
        public void Delte(string id)
        {
            db._mongoCollection.Remove(Query.EQ("_id", id), RemoveFlags.Single, SafeMode.True);
        }
        public List<Advertising> GetList()
        {
            return db._mongoCollection.FindAll().ToList();
        }
       
        public Advertising GetModel(string id)
        {
          return db._mongoCollection.FindOneById(id);
        }
        public Advertising GetModelByCode(string code)
        {
            return db._mongoCollection.FindOne(Query.EQ("Code", code));
        }
        public void Update(Advertising mode)
        {
            Advertising ad = GetModelByCode(mode.Code);
           if (ad!=null)
            {
                mode.id = ad.id;
                db.Update(mode);
            }
            else 
            {
                insert();
            }
            
        }
    }
}
