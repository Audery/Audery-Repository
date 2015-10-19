using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
namespace SOSOshop.BLL.Integral
{
    /// <summary>
    /// 积分条件控制
    /// </summary>
    public class MemberIntegralLock
    {
        #region Model
        public string id { get; set; }
        public int uid { get; set; }
        public string orderid { get; set; }
        public MemberIntegralTemplateEnum mte { get; set; }
        public DateTime created { get; set; }
        #endregion
        MongoHelper<MemberIntegralLock> db = new MongoHelper<MemberIntegralLock>();
        public void insert()
        {
            bool b = db._mongoCollection.Database.CollectionExists("MemberIntegralLock");
            this.created = DateTime.Now;
            db.Insert(this);
            if (!b)
            {
                IndexKeysBuilder keys = new IndexKeysBuilder();
                keys.Descending("created");
                keys.Ascending("uid");
                keys.Ascending("mte");
                db._mongoCollection.CreateIndex(keys);
            }
        }
        /// <summary>
        /// 此订单号是否已经送过积分
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public bool isAllow(string orderid)
        {
            return db._mongoCollection.AsQueryable().Where(x => x.orderid == orderid).Count() == 0;
        }
        /// <summary>
        /// 是否允许积分
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="mte"></param>
        /// <returns></returns>
        public bool isAllow(int uid, MemberIntegralTemplateEnum mte)
        {
            if (mte == MemberIntegralTemplateEnum.建档通过)
            {
                return db._mongoCollection.AsQueryable().Where(x => x.uid == uid && x.mte == mte).Count() == 0;
            }

            if (mte == MemberIntegralTemplateEnum.会员注册)
            {
                return db._mongoCollection.AsQueryable().Where(x => x.uid == uid && x.mte == mte).Count() == 0;
            }

            if (mte == MemberIntegralTemplateEnum.每日签到)
            {
                return db._mongoCollection.AsQueryable().Where(x => x.uid == uid && x.mte == mte && x.created > DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"))).Count() == 0;
            }
            if (mte == MemberIntegralTemplateEnum.提交交易意向)
            {
                return db._mongoCollection.AsQueryable().Where(x => x.uid == uid && x.mte == mte && x.created > DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"))).Count() < 10;
            }
            return true;
        }
        /// <summary>
        /// 取得连续签到的次数
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int GetContSignin(int uid)
        {
            var li = db._mongoCollection.AsQueryable().Where(x => x.uid == uid && x.mte == MemberIntegralTemplateEnum.每日签到).OrderByDescending(x => x.created).Select(x => x.created).ToArray();
            int c = 0;
            int k = 0;
            if (li.Count() > 0)
            {
                if (li[0].ToString("yyyyMMdd") != DateTime.Now.ToString("yyyMMdd"))
                {
                    k = 1;
                }
            }
            for (int i = 0; i < li.Count(); i++)
            {
                if (li[i].ToString("yyyyMMdd") == DateTime.Now.AddDays(-(i + k)).ToString("yyyyMMdd"))
                {
                    c++;
                }
                else
                {
                    break;
                }
            }
            return c;
        }
    }
}
