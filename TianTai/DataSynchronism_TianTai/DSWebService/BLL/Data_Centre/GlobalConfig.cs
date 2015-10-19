using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB;

namespace DSWebService.BLL.Data_Centre
{
    /// <summary>
    /// 系统全局配置
    /// </summary>
    [Serializable]
    public class GlobalConfig
    {
        #region Model
        [MongoDB.Attributes.MongoId]
        public string id { get; set; }
        /// <summary>
        /// 库存滤值(库存必须大于此值才算是有库存-只针对映射数据)
        /// </summary>
        [MongoDB.Attributes.MongoDefault(30)]
        public int Stocklimit { get; set; }
        #endregion
        public GlobalConfig GetModle()
        {
            using (MDbBase db = new MDbBase())
            {
                if (db.GetCollection<GlobalConfig>().Linq().Count() == 0)
                {
                    var m=new GlobalConfig();
                    m.Stocklimit=30;
                    return m;
                }
                return db.GetCollection<GlobalConfig>().Linq().Where(x => x.id == "1").FirstOrDefault();
            }
        }
        /// <summary>
        /// 更新配置
        /// </summary>
        /// <param name="mode"></param>
        public void Update(GlobalConfig mode)
        {
            mode.id = "1";
            using (MDbBase db = new MDbBase())
            {
                if (db.GetCollection<GlobalConfig>().Count() == 0)
                {
                    db.GetCollection<GlobalConfig>().Insert(mode);
                }
                else
                {
                    db.GetCollection<GlobalConfig>().Update(mode);
                }
            }
        }
    }
}