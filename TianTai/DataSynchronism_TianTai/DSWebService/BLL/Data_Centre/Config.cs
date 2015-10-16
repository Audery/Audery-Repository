using System;
using MongoDB;
using System.Linq;
using System.Collections.Generic;

namespace DSWebService.BLL.Data_Centre
{
    [Serializable]
    public class Config
    {

        public Config()
        {

        }
        /// <summary>
        /// 合作企业ID
        /// </summary>
        [MongoDB.Attributes.MongoId]
        public string id { get; set; }
        /// <summary>
        ///合作公司名称
        /// </summary>
        public string incName { get; set; }

        
        /// <summary>
        /// 折扣率
        /// </summary>
        [MongoDB.Attributes.MongoDefault(1)]
        public float discountRate { get; set; }

        /// <summary>
        /// 采购员
        /// </summary>
        public string cgy { get; set; }

        public Config GetModel(string iden)
        {
            using (MDbBase db = new MDbBase())
            {
                return db.GetCollection<Config>().Linq().Where(x => x.id == iden).FirstOrDefault();
            }
        }

        public void Update(Config model)
        {
            using (MDbBase db = new MDbBase())
            {
                if (db.GetCollection<Config>().Linq().Where(x => x.id == model.id).Count() == 0)
                {
                    db.GetCollection<Config>().Insert(model);
                }
                else
                {
                    db.GetCollection<Config>().Update(model, x => x.id == model.id);
                }
            }
        }

        public List<Config> GetAllList()
        {
            using (MDbBase db = new MDbBase())
            {
                return db.GetCollection<Config>().FindAll().Documents.OrderBy(x => x.id).ToList();
            }
        }
        /// <summary>
        /// 取得采购员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetCgy(string id)
        {
            using (MDbBase db = new MDbBase())
            {
                return db.GetCollection<Config>().FindOne(x => x.id == id).cgy;
            }
        }
        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="list"></param>
        public void Update(List<Config> list)
        {
            using (MDbBase db = new MDbBase())
            {
                foreach (var item in list)
                {
                    if (db.GetCollection<Config>().Count(x => x.id == item.id) > 0)
                    {
                        var model = GetModel(item.id);
                        //这里不修改加点设置
                        item.discountRate = model.discountRate;
                        db.GetCollection<Config>().Update(item, x => x.id == item.id);
                    }
                    else
                    {
                        db.GetCollection<Config>().Insert(item);
                    }
                }
                var all = GetAllList();
                foreach (var item in all)
                {
                    if (list.Find(x => x.id == item.id) == null)
                    {
                        db.GetCollection<Config>().Remove(x => x.id == item.id);
                    }
                }
            }
        }
    }
}