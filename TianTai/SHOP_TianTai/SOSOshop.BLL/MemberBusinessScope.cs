using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SOSOshop.BLL
{
    public class MemberBusinessScope : Db
    {
        #region 单例
        private volatile static MemberBusinessScope _instance = null;
        private static readonly object lockHelper = new object();
        private MemberBusinessScope() { }
        public static MemberBusinessScope CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new MemberBusinessScope();
                }
            }
            return _instance;
        }
        #endregion

        /// <summary>
        /// 经营范围
        /// </summary>
        /// <param name="dt"></param>
        public void SyncMemberBusinessScope(DataTable dt)
        {

            using (var conn = (SqlConnection)db.CreateConnection())
            {
                //先全部同步到临时表
                ExecuteNonQuery("TRUNCATE TABLE _MemberBusinessScope");
                conn.Open();
                using (SqlBulkCopy bc = new SqlBulkCopy(conn, SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.UseInternalTransaction, null))
                {
                    bc.BulkCopyTimeout = 10 * 60;
                    bc.BatchSize = 100000;
                    bc.DestinationTableName = "_MemberBusinessScope";                    
                    bc.WriteToServer(dt);
                }
                //数据对比
                //1.删除已经失效的数据
                string StrSql = @"DELETE  FROM MemberBusinessScope
                                    WHERE   ID IN (
                                            SELECT  M.ID
                                            FROM    MemberBusinessScope M
                                                    JOIN ( SELECT   UID ,
                                                                    BussinessScopeCode
                                                           FROM     dbo.MemberBusinessScope
                                                           EXCEPT
                                                           SELECT   UID ,
                                                                    BussinessScopeCode
                                                           FROM     _MemberBusinessScope
                                                         ) AS N ON M.UID = N.UID
                                                                   AND M.BussinessScopeCode = N.BussinessScopeCode )";
                ExecuteNonQuery(StrSql);
                //2.插入差异数据
                StrSql = @"INSERT  INTO MemberBusinessScope
                            ( UID ,
                              BussinessScopeCode
                            )
                            SELECT  UID ,
                                    BussinessScopeCode
                            FROM    dbo._MemberBusinessScope
                            EXCEPT
                            SELECT  UID ,
                                    BussinessScopeCode
                            FROM    MemberBusinessScope";

                ExecuteNonQuery(StrSql);
            }
        }
    }
}
