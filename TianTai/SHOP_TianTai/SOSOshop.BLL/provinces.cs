using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Library.Data;

namespace SOSOshop.BLL
{
    /// <summary>
    /// 省市区
    /// </summary>
    public partial class provinces : DbBase
    {
        public provinces()
        { }
        /// <summary>
        /// 取得所有省份
        /// </summary>
        /// <returns></returns>
        public DataTable GetProvincesList()
        {
            string key = "SOSOYY.BLL.provinces";
            Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            DataTable dt = mc.Get(key) as DataTable;
            if (dt == null)
            {
                dt = db.ExecuteDataSet(CommandType.Text, "select Id,Name AS CityName,Name from Region where ParentId=0").Tables[0];
                mc.Set(key, dt);
            }
            return dt;
        }
        /// <summary>
        /// 取得所有
        /// </summary>
        /// <returns></returns>
        public DataTable GetList()
        {
            string key = "SOSOYY.BLL.provinces.GetList";
            Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            DataTable dt = mc.Get(key) as DataTable;
            if (dt == null)
            {
                dt = db.ExecuteDataSet(CommandType.Text, "select Id,Name AS CityName,ParentId,Name from Region").Tables[0];
                mc.Set(key, dt);
            }
            return dt;
        }
        /// <summary>
        /// 取得省份的名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetName(int id)
        {
            if (id == 0) return "";
            return (from a in GetList().AsEnumerable() where a.Field<int>("Id") == id select a.Field<string>("CityName")).First();
        }

        /// <summary>
        /// 根据ParentID获取省份信息
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public DataTable GetCity(string ParentID) 
        {
            DataView dv = new DataView(GetList());
            dv.RowFilter = "ParentId=" + ParentID;
            return dv.ToTable();
        }
    }
}
