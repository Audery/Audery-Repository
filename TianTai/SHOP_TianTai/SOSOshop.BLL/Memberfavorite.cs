using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SOSOshop.BLL
{
    public class Memberfavorite : Db
    {

        /// <summary>
        /// 关注此商品的人还关注了
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public DataTable GetFavorites(int pid)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select Product_ID as pid,Product_Name as name,Price_01 as pfjg,Price_02 as cljg,DrugsBase_Specification as gg ");
            sql.AppendFormat("from product_online_v where Product_ID in( select proid from memberfavorite where uid in( select uid from memberfavorite where proid={0} and proid !={1})", pid, pid);
            return base.ExecuteTable(sql.ToString());
        }

        /// <summary>
        /// 关注商品
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pid"></param>
        /// <returns>1.关注成功  0.关注失败  2.已关注过</returns>
        public string AddMemberFavorite(int uid, int pid)
        {
            StringBuilder sql = new StringBuilder();
            StringBuilder sqlex = new StringBuilder();
            sqlex.AppendFormat("select count(*) from memberfavorite where uid={0}and ProId={1}", uid, pid);
            if ((int)base.ExecuteScalar(sqlex.ToString()) > 0)
                return "2";
            else
            {
                sql.Append("insert memberfavorite values");
                sql.AppendFormat("({0},{1},'{2}')", uid, pid, DateTime.Now);
                if (base.ExecuteNonQuery(sql.ToString()) > 0)
                    return "1";
                else
                    return "0";
            }
        }

        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="id">关注的ID</param>
        /// <returns>1、取消关注成功，2、取消关注失败</returns>
        public string CancelMemberFavorite(int id, int uid) 
        {
            StringBuilder sbStr = new StringBuilder();
            sbStr.AppendFormat("delete from memberfavorite where id={0} and uid={1}", id, uid);

            if ((int)db.ExecuteNonQuery(CommandType.Text, sbStr.ToString()) > 0)
            {
                return "1";
            }
            else 
            {
                return "0";
            }

        }
    }
}
