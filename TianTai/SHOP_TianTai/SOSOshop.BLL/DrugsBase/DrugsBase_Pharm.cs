using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Threading.Tasks;
namespace SOSOshop.BLL.DrugsBase
{
    /// <summary>
    /// 药理分类
    /// </summary>
    public class DrugsBase_Pharm : DbBase
    {
        /// <summary>
        /// 当前类的缓存依赖项
        /// </summary>
        public const string dependkey = "DrugsBase_Pharm";
        /// <summary>
        /// 药理分类
        /// </summary>
        public DrugsBase_Pharm() { }
        /// <summary>
        /// 取得树状药理分类[缓存]
        /// </summary>
        /// <returns></returns>
        public DataTable GetList()
        {
            string key = string.Format(dependkey + "_{0}", 0);
            DataTable dt = GetDepend(key) as DataTable;
            if (dt == null)
            {
                string sql = "SELECT * FROM DrugsBase_Pharm";
                dt = base.ExecuteTable(sql);
                SetDepend(key, dt, dependkey);

            }
            return dt;
        }
        /// <summary>
        /// 取得树状药理分类[缓存]
        /// </summary>
        /// <param name="Pharm_ID">药理分类ID</param>
        /// <returns></returns>
        public DataTable GetList(int Pharm_ID)
        {
            string key = string.Format(dependkey + "_{0}", Pharm_ID);
            DataTable dt = GetDepend(key) as DataTable;
            if (dt == null)
            {
                dt = GetList();
                DataTable dt2 = dt.Clone();
                dt2.Rows.Clear();
                string Pharm_ID_Path = "";
                DataRow[] dr = dt.Select("Pharm_ID=" + Pharm_ID);
                if (dr.Length > 0)
                {
                    Pharm_ID_Path = dr[0]["Pharm_ID_Path"] + "\\";
                    DataRow dr2 = dt2.NewRow();
                    for (int k = 0; k < dt2.Columns.Count;k++ ) dr2[k] = dr[0][k];
                    dt2.Rows.Add(dr2);
                    Parallel.For(0, dt.Rows.Count, delegate(int i)
                    {
                        if ((dr2["Pharm_ID_Path"] + "\\").StartsWith(Pharm_ID_Path))
                        {
                            dr2 = dt2.NewRow();
                            for (int k = 0; k < dt2.Columns.Count; k++) dr2[k] = dt.Rows[i][k];
                            dt2.Rows.Add(dr2);
                        }
                    });
                    dt2.AcceptChanges();
                }
                SetDepend(key, dt2, dependkey);
                return dt2;
            }
            return dt;
        }
        /// <summary>
        /// 取得药品的药理分类
        /// </summary>
        /// <param name="DrugsBase_ID">药品ID</param>
        /// <returns></returns>
        public DataTable GetListByDrugsBase_ID(int DrugsBase_ID)
        {
            string sql = string.Format("SELECT * FROM DrugsBase_Pharm WHERE Pharm_ID IN (SELECT DISTINCT Pharm_ID FROM DrugsBase_PharmMediNameLink WHERE DrugsBase_ID={0})", DrugsBase_ID);
            DataSet ds = ExecuteDataSetForCache(sql,DateTime.Now.AddDays(1));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }
    }
}
