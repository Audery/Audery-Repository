using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOSOshop.BLL
{
    public class MemberBrowserProductContentLog : Db
    {
        public MemberBrowserProductContentLog() { }

        /// <summary>
        /// 记录商品浏览记录
        /// </summary>
        /// <param name="uid">用户Id</param>
        /// <param name="proId">商品Id</param>
        public void RecordBrowse(object uid, object proId) 
        {
            int userId = 0;int.TryParse(uid.ToString(), out userId);

            if (userId != 0)
            {
                int productId = 0; int.TryParse(proId.ToString(), out productId);
                string sql = string.Format("INSERT INTO memberbrowserproductcontentlog (uid, ProId, AddDate, loginId) VALUES ({0},{1},getdate(),(SELECT max(id) FROM memberloginlog WHERE UID={0}))", userId, productId);

                Db db = new Db();
                try
                {
                    db.ExecuteNonQuery(sql);
                }
                catch (Exception e) { throw e; }
            }
        }
    }
}
