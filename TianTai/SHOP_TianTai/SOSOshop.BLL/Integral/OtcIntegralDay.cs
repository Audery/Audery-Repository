using System;
using MongoDB;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
using MongoDB.Bson.Serialization.Attributes;
namespace SOSOshop.BLL.Integral
{
    /// <summary>
    /// OTC客户按日定制客户积分倍数规则
    /// </summary>
    public class OtcIntegralDay
    {
        [BsonIgnore]
        MongoHelper<OtcIntegralDay> db = null;
        #region Model
        /// <summary>
        /// 星期序号
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 积分倍数
        /// </summary>
        public int multiple { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool state { get; set; }
        #endregion

        public OtcIntegralDay()
        {
            db = new MongoHelper<OtcIntegralDay>();
            if (db._mongoCollection.Count() == 0)
            {
                for (int i = 0; i < 7; i++)
                {
                    OtcIntegralDay model = new OtcIntegralDay("");
                    model.id = i.ToString();
                    model.multiple = 1;
                    model.state = true;
                    db._mongoCollection.Insert(model);
                }
            }
        }
        public OtcIntegralDay(string t)
        {

        }
        /// <summary>
        /// 取得积分规则列表
        /// </summary>
        /// <param name="week"></param>
        /// <returns></returns>
        public List<OtcIntegralDay> GetList(string week = "")
        {
            if (week == "")
            {
                return db._mongoCollection.FindAll().ToList();
            }
            else
            {
                return db._mongoCollection.FindAll().Where(x => x.id == week).ToList();
            }
        }
        //更新积分倍数
        public void update(string id, int multiple)
        {
            db._mongoCollection.Update(Query.EQ("_id", id), Update.Set("multiple", multiple));
        }
    }
}
