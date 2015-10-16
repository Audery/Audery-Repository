using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB;
using MongoDB.Linq;
namespace DSWebService.BLL.Data_Centre
{
    /// <summary>
    /// 自动计算最优货源日志记录
    /// </summary>
    public class AutoSupplySwitchLog
    {
        [MongoDB.Attributes.MongoId]
        public string id { get; set; }
        public string mark { get; set; }

        public void insert(int h)
        {
            using (MDbBase db = new MDbBase())
            {
                this.mark = DateTime.Now.ToString("yyyy-MM-dd") + ":" + h;
                id = MongoDB.Oid.NewOid().ToString();
                db.GetCollection<AutoSupplySwitchLog>().Insert(this);
            }
        }
        /// <summary>
        /// 判断是否可以执行
        /// </summary>
        /// <param name="mark"></param>
        /// <returns></returns>
        public bool IsExcu(int h)
        {
            using (MDbBase db = new MDbBase())
            {
                int c = db.GetCollection<AutoSupplySwitchLog>().Linq().Where(x => x.mark == DateTime.Now.ToString("yyyy-MM-dd") + ":" + h).Count();
                return c == 0;
            }
        }
    }
}