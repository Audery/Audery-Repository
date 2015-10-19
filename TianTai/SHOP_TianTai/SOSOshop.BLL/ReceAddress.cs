using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SOSOshop.Model;

namespace SOSOshop.BLL
{
    public class ReceAddress:Db
    {
        public DataTable GetSetByWhere(string where)
        {
            string sql = " select a.id,a.phone,a.mobile,a.uid,a.username,a.address,a.zip,a.email,a.stat,a.ConstructionSigns,a.ConsignesTime,tel=('手机：'+a.mobile+';座机：'+a.phone+';'),province=((isnull(provinces1.CityName,''))+(isnull(provinces2.CityName,''))+(isnull(provinces3.CityName,''))) from dbo.yxs_receaddress a left join (select CityName,Id,ParentId from yxs_provinces where isuse=1 and Depth=1)as provinces1 on provinces1.Id =a.province left join (select CityName,Id,ParentId from yxs_provinces where isuse=1 and Depth=2) as provinces2 on provinces2.Id =a.city left join (select CityName,Id,ParentId from yxs_provinces where isuse=1 and Depth=3)as provinces3 on provinces3.Id =a.borough where 1=1 " + where;
            return base.ExecuteTable(sql);
        }

        /// <summary>
        /// 获得用户地址列表，省市区为组合字段
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable GetAddressListByWhere(string where)
        {
            string sql = " select a.id,a.phone,a.mobile,a.uid,a.username,a.address,a.zip,a.email,a.stat,a.ConstructionSigns,a.ConsignesTime,tel=('手机：'+a.mobile+';座机：'+a.phone+';'),province=((isnull(provinces1.CityName,''))+(isnull(provinces2.CityName,''))+(isnull(provinces3.CityName,''))) from dbo.memberreceaddress a left join (select CityName,Id,ParentId from yxs_provinces where isuse=1 and Depth=1)as provinces1 on provinces1.Id =a.province left join (select CityName,Id,ParentId from yxs_provinces where isuse=1 and Depth=2) as provinces2 on provinces2.Id =a.city left join (select CityName,Id,ParentId from yxs_provinces where isuse=1 and Depth=3)as provinces3 on provinces3.Id =a.borough where 1=1 " + where;            
            return base.ExecuteTable(sql);
        }
        /// <summary>
        /// 获得用户地址列表，省市区为单独字段
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable GetAddressListByWhereArea(string where)
        {
            string sql = "select a.id,a.phone,a.mobile,a.uid,a.username,a.address,a.zip,a.email,a.stat,a.ConstructionSigns,a.ConsignesTime,a.phone,provinces1.cityname as province,provinces2.CityName as city, provinces3.CityName as borough  from dbo.memberreceaddress a left join (select CityName,Id,ParentId from yxs_provinces where isuse=1 and Depth=1)as provinces1 on provinces1.Id =a.province left join (select CityName,Id,ParentId from yxs_provinces where isuse=1 and Depth=2) as provinces2 on provinces2.Id =a.city left join (select CityName,Id,ParentId from yxs_provinces where isuse=1 and Depth=3)as provinces3 on provinces3.Id =a.borough where 1=1 " + where;
            return base.ExecuteTable(sql);
        }

        /// <summary>
        /// 删除用户地址
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public bool DeleteAddress(string aid,string uid)
        {
            string sql = "delete from memberreceaddress where id=" + aid + " and uid=" + uid;           
            return base.ExecuteNonQuery(sql) > 0 ? true : false;
        }

        /// <summary>
        /// 解析用户地址数据，省市区为组合字段
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public UserAddress getModel(DataRow dr)
        {
            UserAddress ua = new UserAddress();
            ua.ID = (int)dr["id"];
            ua.Name = dr["username"].ToString();
            ua.Address = dr["address"].ToString();
            ua.Zip = dr["zip"].ToString();
            ua.Mobile = dr["mobile"].ToString();
            ua.Phone = dr["phone"].ToString();
            ua.Email = dr["email"].ToString();
            ua.Stat = (bool)dr["stat"];
            ua.ConstructionSigns = dr["ConstructionSigns"].ToString();
            ua.Consignestime = dr["ConsignesTime"].ToString();
            ua.Province = dr["province"].ToString();
            return ua;
        }

        /// <summary>
        /// 解析用户地址数据,包含省市区，是单独字段
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public UserAddress getModelorCity(DataRow dr)
        {
            UserAddress ua = new UserAddress();
            ua.ID = (int)dr["id"];
            ua.Name = dr["username"].ToString();
            ua.Address = dr["address"].ToString();
            ua.Zip = dr["zip"].ToString();
            ua.Mobile = dr["mobile"].ToString();
            ua.Phone = dr["phone"].ToString();
            ua.Email = dr["email"].ToString();
            ua.Stat = (bool)dr["stat"];
            ua.ConstructionSigns = dr["ConstructionSigns"].ToString();
            ua.Consignestime = dr["ConsignesTime"].ToString();
            ua.Province = dr["Province"].ToString();
            ua.City = dr["City"].ToString();
            ua.Borough = dr["Borough"].ToString();
            return ua;
        }
       
    }
}
