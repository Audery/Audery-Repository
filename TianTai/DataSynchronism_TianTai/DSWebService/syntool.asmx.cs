using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Caching;
using System.Data;
using System.Collections;
using System.Threading;
using DSWebService.BLL.Data_Centre;
using System.Threading.Tasks;

namespace DSWebService
{
    /// <summary>
    /// syntool 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class syntool : System.Web.Services.WebService
    {
        #region 公共方法
        [WebMethod]
        public string Login(string name, string pwd)
        {
            BLL.DbBase bll = new BLL.DbBase();
            bll.ChangeDBShop();
            int c = (int)bll.ExecuteScalar(string.Format("SELECT COUNT(*) FROM yxs_administrators  WHERE NAME='{0}' AND PASSWORD='{1}'", Library.Lang.Input.Filter(name), Library.Lang.Input.MD5(pwd, false)));
            if (c == 1)
            {
                Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
                mc.PoolName = "Price_Cache";
                string authKey = MongoDB.Oid.NewOid().ToString();
                mc.Set(authKey, name, DateTime.Now.AddHours(12));
                return authKey;
            }
            return null;
        }

        /// <summary>
        /// 验证是否登陆
        /// </summary>
        /// <returns></returns>
        public bool islogin(string authKey)
        {
            if (authKey == null) return false;
            Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            mc.PoolName = "Price_Cache";
            string loginName = mc.Get(authKey) as string;
            if (loginName == null)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 取得登陆名
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetName(string key)
        {
            Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            mc.PoolName = "Price_Cache";
            return mc.Get(key) as string;
        }
        /// <summary>
        /// 取得权限
        /// </summary>
        /// <param name="authKey"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetPower(string authKey)
        {
            return "all";
            //string name = GetName(authKey);
            //BLL.Data_Centre.Link bll = new BLL.Data_Centre.Link();
            //bll.ChangeDBShop();
            //string sql = string.Format("SELECT iden FROM dbo.yxs_administrators WHERE name='{0}'", name);
            //string power = bll.ExecuteScalar(sql).ToString();
            //if (name == "admin" || name == "阎正")
            //{
            //    return "all";
            //}
            //else if (power == "0")
            //{
            //    return "limit";
            //}
            //else
            //{
            //    return power;
            //}
        }

        /// <summary>
        /// 用户是否有该权限
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [WebMethod]
        public bool GetUserPower(string value, string authKey)
        {
            string name = GetName(authKey);
            BLL.Data_Centre.Link bll = new BLL.Data_Centre.Link();
            bll.ChangeDBShop();
            string sql = string.Format(@"DECLARE @role VARCHAR(200)
                                        SELECT  @role = role
                                        FROM    yxs_administrators
                                        WHERE   name = '{0}'
                                        SELECT  operatecode
                                        FROM    dbo.yxs_roles_permissions
                                        WHERE   id IN ( SELECT  *
                                                        FROM    dbo.f_split(@role, ',') )", name);
            DataTable dt = bll.ExecuteTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                string v = dr["operatecode"].ToString();
                if (value.Equals(v))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 取得支持哪些公司
        /// </summary>
        /// <param name="authKey"></param>
        /// <returns></returns>
        [WebMethod]
        public List<keyValue> GetMenu(string authKey)
        {

            List<keyValue> li = new List<keyValue>();
            string Power = GetPower(authKey);
            foreach (var item2 in new BLL.Data_Centre.Config().GetAllList().OrderByDescending(x => x.id))
            {
                if (Power.Contains(item2.id) || Power == "all" || Power == "limit")
                {
                    if (Power == "limit")
                    {
                        if (!item2.cgy.Contains(GetName(authKey)))
                        {
                            continue;
                        }
                    }
                    var item = new keyValue();
                    item.name = item2.incName + "数据映射";
                    item.id = int.Parse(item2.id);
                    li.Add(item);
                }
            }
            return li;
        }
        [Serializable]
        public class keyValue
        {
            public int id { get; set; }
            public string name { get; set; }
        }
        #endregion


        #region 第三方商城合作公司数据映射系统
        /// <summary>
        /// 取得映射数据
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="order"></param>
        /// <param name="orderField"></param>
        /// <param name="like"></param>
        /// <param name="whereField"></param>
        /// <param name="whereString"></param>
        /// <param name="BinType"></param>
        /// <param name="authKey"></param>
        /// <param name="iden">合作企业标识10000天奇,10001蓉锦</param>
        /// <returns></returns>
        [WebMethod]
        public DataSet GetMaptoolList(int PageSize, int PageIndex, bool order, string orderField, bool like, string whereField, string whereString, int BinType, string authKey, int iden)
        {
            if (islogin(authKey))
            {
                string tablename = "View_Product1";
                BLL.Data_Centre.Link bll = new BLL.Data_Centre.Link();
                int recordCount, pageCount;
                string sql = " and iden=" + iden + " ";
                switch (BinType)
                {
                    //未映射
                    case 1:
                        {
                            //orderField = "Stock";
                            //order = !order;
                            sql += "and id not in (SELECT t_id FROM dbo.Link WHERE iden=" + iden + ")";
                            break;
                        }
                    //全部
                    case 0:
                        {
                            //sql += "and iden =" + iden;
                            break;
                        }
                    //已映射
                    case 2:
                        {
                            sql += "and id in (SELECT t_id FROM dbo.Link WHERE iden=" + iden + ")";
                            break;
                        }
                }
                if (!string.IsNullOrEmpty(whereString))
                {
                    sql += whereString;
                }
                whereString = null;
                var ds = bll.Data_CentreGetList(PageSize, PageIndex, order, orderField, like, whereField, whereString, out recordCount, out pageCount, sql, tablename).DataSet;
                ds.Tables[0].Columns.Add("StockToolTipText", typeof(string));
                ds.Tables[0].Columns.Add("remark", typeof(string));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    sql = string.Format("SELECT * FROM View_Stock1 WHERE iden={0} and id IN ({1})", iden, string.Join(",", ds.Tables[0].AsEnumerable().Select(x => "'" + x.Field<object>("id") + "'")));
                    var dtTemp = bll.ExecuteTable(sql);
                    sql = string.Format("SELECT id,tab FROM dbo.tags_2 WHERE iden={0} and id IN ({1})", iden, string.Join(",", ds.Tables[0].AsEnumerable().Select(x => "'" + x.Field<object>("id") + "'")));
                    DataTable dtT2 = bll.ExecuteTable(sql);
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        item["DrugsBase_DrugName"] = item["DrugsBase_DrugName"].ToString().Trim();
                        item["DrugsBase_Manufacturer"] = item["DrugsBase_Manufacturer"].ToString().Trim();
                        item["DrugsBase_ApprovalNumber"] = item["DrugsBase_ApprovalNumber"].ToString().Trim();

                        var temp = dtT2.Select("id='" + item["id"] + "'").ToArray();
                        if (temp.Count() > 0)
                        {
                            item["remark"] = temp[0]["tab"];
                            continue;
                        }
                        var drTemp = dtTemp.Select("id='" + item["id"] + "'").OrderBy(x => x.Field<string>("pihao")).ToArray();
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        for (int i = 0; i < drTemp.Count(); i++)
                        {
                            sb.AppendFormat("批号:{0} 效期:{1} 库存:{2}\r\n", drTemp[i]["pihao"].ToString().Trim(), drTemp[i]["sxrq"].ToString().Trim().Replace(" 0:00:00", ""), drTemp[i]["Stock"].ToString().Trim());
                            if (Library.Lang.DataValidator.IsDateTime(drTemp[i]["sxrq"].ToString()))
                            {
                                if (DateTime.Parse(drTemp[i]["sxrq"].ToString()) < DateTime.Now.AddYears(1))
                                {
                                    item["remark"] = "近效期";
                                }
                            }
                            else
                            {
                                item["remark"] = "效期异常";
                            }
                        }
                        item["StockToolTipText"] = sb.ToString();
                    }
                }
                DataTable dt = new DataTable();
                dt.Columns.Add("recordCount");
                dt.Columns.Add("pageCount");
                var dr = dt.NewRow();
                dr["recordCount"] = recordCount;
                dr["pageCount"] = pageCount;
                dt.Rows.Add(dr);
                ds.Tables.Add(dt);
                return ds;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 能产品标记
        /// </summary>
        /// <param name="id"></param>
        /// <param name="iden"></param>
        /// <param name="authKey"></param>
        /// <returns></returns>
        [WebMethod]
        public string SetTag(string id, int iden, string tab, string authKey)
        {
            if (islogin(authKey))
            {
                string sql = @"IF EXISTS(SELECT * FROM dbo.tags WHERE id='{0}' AND iden={1})
                               UPDATE tags SET tab='{2}' WHERE id='{0}' AND iden={1}
                               ELSE
                               INSERT dbo.tags VALUES('{0}',{1},'{2}')";
                sql = string.Format(sql, id, iden, tab);
                if (tab == "未处理")
                {
                    sql = string.Format("DELETE dbo.tags WHERE id='{0}' AND iden={1}", id, iden);
                }
                new BLL.Data_Centre.Link().ExecuteNonQuery(sql);
            }
            return "";
        }
        /// <summary>
        /// 添加自定义说明
        /// </summary>
        /// <param name="id"></param>
        /// <param name="iden"></param>
        /// <param name="tab"></param>
        /// <param name="authKey"></param>
        /// <returns></returns>
        [WebMethod]
        public string SetTag_2(string id, int iden, string tab, string authKey)
        {
            if (islogin(authKey))
            {
                string sql = @"IF EXISTS(SELECT * FROM dbo.tags_2 WHERE id='{0}' AND iden={1})
                               UPDATE tags_2 SET tab='{2}' WHERE id='{0}' AND iden={1}
                               ELSE
                               INSERT dbo.tags_2 VALUES('{0}',{1},'{2}')";
                sql = string.Format(sql, id, iden, tab);
                if (tab == "")
                {
                    sql = string.Format("DELETE dbo.tags_2 WHERE id='{0}' AND iden={1}", id, iden);
                }
                new BLL.Data_Centre.Link().ExecuteNonQuery(sql);
            }
            return "";
        }
        /// <summary>
        /// 合作厂家数据映射
        /// </summary>
        /// <param name="id">商品id(商城)</param>
        /// <param name="spid">商品id(erp)</param>
        /// <param name="t_id">商品id(第三方)</param>
        /// <param name="iden">第三方标识</param>
        /// <param name="Price_01">批发价</param>
        /// <param name="Price_02">otc价</param>
        /// <param name="Price_01Plus">批发价加点</param>
        /// <param name="Price_02Plus">otc价加点</param>
        /// <param name="authKey"></param>
        /// <returns></returns>
        [WebMethod]
        public bool AddLink(Model.Data_Centre.Link model, string authKey)
        {
            if (islogin(authKey))
            {
                model.created = DateTime.Now;
                model.updated = model.created;
                BLL.Data_Centre.Link bll = new BLL.Data_Centre.Link();
                if (!bll.Exists(model.id, model.t_id, model.iden))
                {
                    if (bll.Exists(model.id, model.iden))
                    {
                        return false;
                    }
                    bll.Add(model);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 取得已经映射过的产品
        /// </summary>
        /// <param name="t_id"></param>
        /// <param name="iden"></param>
        /// <param name="authKey"></param>
        /// <returns></returns>
        [WebMethod]
        public DataSet GetCompleteLink(string t_id, int iden, string authKey)
        {
            if (islogin(authKey))
            {
                string sql = string.Format("SELECT * FROM [Link] where t_id='{0}' and iden={1}", t_id, iden);
                BLL.Data_Centre.Link bll = new BLL.Data_Centre.Link();
                var dr = bll.ExecuteTable(sql).Rows[0];
                bll.ChangeDBShop();
                sql = string.Format("SELECT * FROM dbo._ViewDrugsBaseAndGoods WHERE Product_ID={0} ", dr["id"]);
                return bll.ExecuteDataSet(sql);
            }
            return null;
        }
        /// <summary>
        /// 取消数据映射
        /// </summary>
        /// <param name="t_id"></param>
        /// <param name="iden"></param>
        /// <param name="authKey"></param>
        /// <returns></returns>
        [WebMethod]
        public bool DelCompleteLink(string t_id, int iden, string authKey)
        {
            if (islogin(authKey))
            {
                BLL.Data_Centre.Link bll = new BLL.Data_Centre.Link();
                string sql = string.Format("DELETE dbo.Link WHERE t_id='{0}' AND iden={1}", t_id, iden);
                bll.ExecuteNonQuery(sql);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 取得映射数据
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="order"></param>
        /// <param name="orderField"></param>
        /// <param name="like"></param>
        /// <param name="whereField"></param>
        /// <param name="whereString"></param>
        /// <param name="BinType"></param>
        /// <param name="authKey"></param>
        /// <param name="iden">合作企业标识10000天奇,10001蓉锦</param>
        /// <returns></returns>
        [WebMethod]
        public DataSet GetShopList(int PageSize, int PageIndex, bool order, string orderField, bool like, string whereField, string whereString, int BinType, string authKey, int iden = 0)
        {
            if (islogin(authKey))
            {

                BLL.Data_Centre.Link bll = new BLL.Data_Centre.Link();
                bll.ChangeDBData_Centre();
                int recordCount, pageCount;
                string sql = "";
                if (iden == 0)
                {
                    switch (BinType)
                    {
                        //未映射
                        case 1:
                            {
                                sql += "and Product_ID NOT IN (SELECT id FROM Data.Data_Centre.dbo.Link)";
                                break;
                            }
                        //已经映射
                        case 2:
                            {
                                sql += "and Product_ID IN (SELECT id FROM Data.Data_Centre.dbo.Link)";
                                break;
                            }
                        //全部
                        case 0:
                            {
                                break;
                            }
                    }
                }

                if (!string.IsNullOrEmpty(whereString))
                {
                    sql += whereString;
                }
                whereString = null;
                DataSet ds;
                if (BinType == 1)
                {
                    //取101可映射的产品数据
                    ds = bll.GetList1012(PageSize, PageIndex, order, orderField, like, whereField, whereString, out recordCount, out pageCount, sql).DataSet;
                }
                else
                {
                    //取101的商品数据                    
                    ds = bll.GetList1013(PageSize, PageIndex, order, orderField, like, whereField, whereString, out recordCount, out pageCount, sql).DataSet;
                    
                    ds.Tables[0].Columns.Add("foreColor", typeof(int));

                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        item["iden"] = iden;
                    }
                }
                DataTable dt = new DataTable();
                dt.Columns.Add("recordCount");
                dt.Columns.Add("pageCount");
                var dr = dt.NewRow();
                dr["recordCount"] = recordCount;
                dr["pageCount"] = pageCount;
                dt.Rows.Add(dr);
                ds.Tables.Add(dt);
                return ds;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 设置默认货源
        /// </summary>
        /// <param name="id"></param>
        /// <param name="authKey"></param>
        /// <returns></returns>
        [WebMethod]
        public bool SetDefaultShop(int iden, string t_id, string is_default, string authKey)
        {
            if (islogin(authKey))
            {

                string sql = string.Format("UPDATE Link SET is_default={0} WHERE t_id='{1}' and iden={2}", is_default, t_id, iden);
                BLL.Data_Centre.Link bll = new BLL.Data_Centre.Link();
                bll.ExecuteNonQuery(sql);
                return true;
            }
            else
            {
                return false;
            }
        }


        #endregion

        /// <summary>
        /// 查询基础产品数据(包含件装转换比)
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="order"></param>
        /// <param name="orderField"></param>
        /// <param name="like"></param>
        /// <param name="whereField"></param>
        /// <param name="whereString"></param>
        /// <param name="authKey"></param>
        /// <returns></returns>
        [WebMethod]
        public DataSet GetListAll101(int PageSize, int PageIndex, bool order, string orderField, bool like, string whereField, string whereString, string sql, string authKey)
        {
            if (islogin(authKey))
            {
                BLL.Data_Centre.Link bll = new BLL.Data_Centre.Link();
                int recordCount, pageCount;
                if (!string.IsNullOrEmpty(whereString))
                {
                    sql += " and  beactive='是' and (DrugsBase_ApprovalNumber='" + whereString + "' or DrugsBase_ID in (SELECT DrugsBase_ID FROM dbo.DrugsBase_ApprovalNumber_MadeIn_Foreign WHERE Registration_No='" + whereString + "'))";
                    whereString = " and beactive='是'";
                }
                var ds = bll.GetList1012(PageSize, PageIndex, order, orderField, like, whereField, whereString, out recordCount, out pageCount, sql).DataSet;
                DataTable dt = new DataTable();
                dt.Columns.Add("recordCount");
                dt.Columns.Add("pageCount");
                var dr = dt.NewRow();
                dr["recordCount"] = recordCount;
                dr["pageCount"] = pageCount;
                dt.Rows.Add(dr);
                ds.Tables.Add(dt);
                return ds;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 取得系统全局配置
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public GlobalConfig GetGlobalConfig()
        {
            BLL.Data_Centre.GlobalConfig bll = new GlobalConfig();
            return bll.GetModle();
        }

        /// <summary>
        /// 修改系统全局配置
        /// </summary>
        /// <param name="model"></param>
        [WebMethod]
        public void UpdateGlobalConfig(GlobalConfig model)
        {
            BLL.Data_Centre.GlobalConfig bll = new GlobalConfig();
            bll.Update(model);
        }


        #region 货源管理2.0增加功能
        /// <summary>
        /// 数据标准化
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="order"></param>
        /// <param name="orderField"></param>
        /// <param name="like"></param>
        /// <param name="whereField"></param>
        /// <param name="whereString"></param>
        /// <param name="BinType"></param>
        /// <param name="authKey"></param>
        /// <param name="iden"></param>
        /// <returns></returns>
        [WebMethod]
        public DataSet GetMaptoolDataList(int PageSize, int PageIndex, bool order, string orderField, bool like, string whereField, string whereString, int BinType, string authKey, int iden)
        {
            if (islogin(authKey))
            {
                string tablename = "View_Product_Mid";
                BLL.Data_Centre.Link_Mid bll = new BLL.Data_Centre.Link_Mid();
                int recordCount, pageCount;
                string sql = " and iden=" + iden + " ";
                switch (BinType)
                {
                    //数据标准化
                    case 1:
                        {
                            sql += "and id in (SELECT t_id FROM dbo.Link WHERE iden=" + iden + ")";
                            break;
                        }
                }
                if (!string.IsNullOrEmpty(whereString))
                {
                    sql += whereString;
                }
                whereString = null;
                var ds = bll.Data_CentreGetList(PageSize, PageIndex, order, orderField, like, whereField, whereString, out recordCount, out pageCount, sql, tablename).DataSet;
                ds.Tables[0].Columns.Add("StockToolTipText", typeof(string));
                ds.Tables[0].Columns.Add("remark", typeof(string));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    sql = string.Format("SELECT * FROM View_Stock1 WHERE iden={0} and id IN ({1})", iden, string.Join(",", ds.Tables[0].AsEnumerable().Select(x => "'" + x.Field<object>("id") + "'")));
                    var dtTemp = bll.ExecuteTable(sql);
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        item["DrugsBase_DrugName"] = item["DrugsBase_DrugName"].ToString().Trim();
                        item["DrugsBase_Manufacturer"] = item["DrugsBase_Manufacturer"].ToString().Trim();
                        item["DrugsBase_ApprovalNumber"] = item["DrugsBase_ApprovalNumber"].ToString().Trim();

                        var drTemp = dtTemp.Select("id='" + item["id"] + "'").OrderBy(x => x.Field<string>("pihao")).ToArray();
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        for (int i = 0; i < drTemp.Count(); i++)
                        {
                            sb.AppendFormat("批号:{0} 效期:{1} 库存:{2}\r\n", drTemp[i]["pihao"].ToString().Trim(), drTemp[i]["sxrq"].ToString().Trim().Replace(" 0:00:00", ""), drTemp[i]["Stock"].ToString().Trim());
                            if (Library.Lang.DataValidator.IsDateTime(drTemp[i]["sxrq"].ToString()))
                            {
                                if (DateTime.Parse(drTemp[i]["sxrq"].ToString()) < DateTime.Now.AddYears(1))
                                {
                                    item["remark"] = "近效期";
                                }
                            }

                        }
                        item["StockToolTipText"] = sb.ToString();
                    }
                }
                DataTable dt = new DataTable();
                dt.Columns.Add("recordCount");
                dt.Columns.Add("pageCount");
                var dr = dt.NewRow();
                dr["recordCount"] = recordCount;
                dr["pageCount"] = pageCount;
                dt.Rows.Add(dr);
                ds.Tables.Add(dt);
                return ds;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 设置商城前台商品销售单位
        /// </summary>
        /// <param name="id"></param>
        /// <param name="selltype"></param>
        /// <param name="authKey"></param>
        /// <returns></returns>
        [WebMethod]
        public string SetSellType(int id, int selltype, string authKey)
        {
            int x = 0;
            if (islogin(authKey))
            {
                BLL.Data_Centre.Link_Mid bll = new Link_Mid();
                bll.ChangeDBShop();
                x = bll.ExecuteNonQuery(string.Format("update product set sellType={0} where product_id={1}", selltype, id));
            }
            return x.ToString();
        }

        /// <summary>
        /// 保存数据标准化的含义(解决中包装含义不清的问题)
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="authKey"></param>
        /// <param name="col">改变的列 1：数量改变，2：库存改变，3：价格改变</param>
        /// <returns></returns>
        [WebMethod]
        public string SetData(Model.Data_Centre.Link_Mid mid, int col, string authKey)
        {
            if (islogin(authKey))
            {
                int x = 0;
                BLL.Data_Centre.Link_Mid bll = new BLL.Data_Centre.Link_Mid();
                Product_Centre pc = new Product_Centre();
                int spid = new BLL.Data_Centre.Link().GetSpid(mid.id, mid.iden);
                if (mid.PriceType == mid.StockType && mid.StockType == 1)
                {
                    //删除库存和价格同时为最小单位的数据
                    if (bll.Exists(mid.id, mid.iden))
                    {
                        if (bll.ExecuteNonQuery(string.Format("delete from link_mid where id={0} and iden={1}", mid.id, mid.iden)) > 0)
                        {

                        }
                    }
                }
                else
                {
                    if (bll.Exists(mid.id, mid.iden))
                    {
                        //修改
                        if (bll.Update(mid))
                        {

                        }
                    }
                    else
                    {
                        //新增
                        if (bll.Add(mid))
                        {

                        }
                    }
                }
                pc.UpdateStock(spid, mid.iden);
                new BLL.Data_Centre.Price().Updates((string)pc.ExecuteScalar("SELECT t_id FROM dbo.Link WHERE id=" + mid.id));

            }
            return "";
        }

        /// <summary>
        /// 取得图片路径
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="goodsPackageId"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetImageUrl(int goodsId, int goodsPackageId)
        {
            BLL.DbBase db = new BLL.DbBase();
            db.ChangeDBShop();
            string url = db.ExecuteScalar("SELECT dbo.[GetImagePathOriginal](" + goodsId.ToString() + "," + goodsPackageId.ToString() + ")") as string;
            if (string.IsNullOrEmpty(url))
            {
                url = "images/nopic1.jpg";
            }
            return "http://image.101yao.com//" + url;
        }


        /// <summary>
        /// 获取映射后的ERP数据和基础数据
        /// </summary>
        /// <param name="iden"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="order"></param>
        /// <param name="orderField"></param>
        /// <param name="like"></param>
        /// <param name="whereField"></param>
        /// <param name="whereString"></param>
        /// <param name="authKey"></param>
        /// <returns></returns>
        [WebMethod]
        public DataSet GetCheckDataInfo(int iden, int PageSize, int PageIndex, bool order, string orderField, bool like, string whereField, string whereString, string authKey)
        {
            if (islogin(authKey))
            {
                string tablename = "View_CheckDataInfo";
                BLL.Data_Centre.Link bll = new BLL.Data_Centre.Link();
                int recordCount, pageCount;
                string sql = " AND iden=" + iden + " ";
                if (!string.IsNullOrEmpty(whereString))
                {
                    sql += whereString;
                }
                whereString = null;
                DataSet ds = bll.Data_CentreGetList(PageSize, PageIndex, order, orderField, like, whereField, whereString, out recordCount, out pageCount, sql, tablename).DataSet;

                if (ds != null && ds.Tables.Count > 0)
                {
                    ds.Tables[0].Columns.Add("strStockTypes");
                    ds.Tables[0].Columns.Add("strPriceTypes");
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        item["Erp_DrugName"] = item["Erp_DrugName"].ToString().Trim();
                        item["Erp_Manufacturer"] = item["Erp_Manufacturer"].ToString().Trim();
                        item["Erp_ApprovalNumber"] = item["Erp_ApprovalNumber"].ToString().Trim();

                        //库存含义
                        int iType = 0;
                        if (int.TryParse(item["StockTypes"].ToString(), out iType))
                        {

                            item["strStockTypes"] = GetUnit(iType);
                        }
                        //价格含义
                        iType = 0;
                        if (int.TryParse(item["PriceTypes"].ToString(), out iType))
                        {
                            item["strPriceTypes"] = GetUnit(iType);
                        }
                    }
                }

                DataTable dt = new DataTable();
                dt.Columns.Add("recordCount");
                dt.Columns.Add("pageCount");
                var dr = dt.NewRow();
                dr["recordCount"] = recordCount;
                dr["pageCount"] = pageCount;
                dt.Rows.Add(dr);
                ds.Tables.Add(dt);

                return ds;
            }
            else
            {
                return null;
            }
        }

        private string GetUnit(int unit)
        {
            switch (unit)
            {
                case 1:
                    return "最小单位";
                case 2:
                    return "中包装";
                case 3:
                    return "件装";
            }
            return "最小单位";
        }
        #endregion

        #region 流向管理
        /// <summary>
        /// 获取数据中心库里的商品流向设置列表
        /// </summary>
        /// <param name="authKey"></param>
        /// <returns></returns>
        [WebMethod]
        public DataTable GetLiuXiangList(string authKey)
        {
            if (islogin(authKey))
            {
                Link db = new Link();
                db.ChangeDBShop();
                string sql = "select * from liuxiang";
                return db.ExecuteTable(sql);
            }
            return null;
        }



        /// <summary>
        /// 获取地区信息
        /// </summary>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        [WebMethod]
        public DataTable GetRegion(int ParentId)
        {
            string sql = string.Format("select ID,Name from Region where ParentId={0}", ParentId);
            BLL.DbBase db = new BLL.DbBase();
            db.ChangeDBShop();
            return db.ExecuteTable(sql);
        }

        /// <summary>
        /// 获取商品的区域流向设置
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="authKey"></param>
        /// <returns></returns>
        [WebMethod]
        public DataTable GetQuYuLiuXiang(string pid, string authKey)
        {
            if (islogin(authKey))
            {
                Link db = new Link();
                db.ChangeDBShop();

                string exits = string.Format("select * from liuxiang where product_id={0} and liuxiang_type={1}", pid, 0);
                DataTable ex = db.ExecuteTable(exits);
                if (ex.Rows.Count > 0)
                {
                    string reg = string.Format("select * from region where id in({0})", ex.Rows[0]["addr_id"].ToString());

                    DataTable region = db.ExecuteTable(reg);
                    List<string> rn = new List<string>();
                    foreach (string dr in ex.Rows[0]["addr_id"].ToString().Split(','))
                    {
                        if (!string.IsNullOrEmpty(dr))
                        {
                            var query = from ax in region.AsEnumerable()
                                        where ax.Field<int>("ID") == Convert.ToInt32(dr)
                                        select ax;
                            DataTable d = query.CopyToDataTable();
                            if (d.Rows.Count > 0)
                            {
                                rn.Add(d.Rows[0]["Name"].ToString());
                            }
                            else
                            {
                                rn.Add("");
                            }
                        }
                    }
                    ex.Columns.Add("addr_name", typeof(string));
                    ex.Rows[0]["addr_name"] = string.Join(",", rn);
                }
                return ex;
            }
            return null;
        }

        /// <summary>
        /// 设置控制销售流向，
        /// </summary>
        /// <param name="pid">商品ID</param>
        /// <param name="aid">地区ID或用户分类</param>
        /// <param name="type"></param>
        /// <param name="way"></param>
        /// <param name="authKey"></param>
        /// <returns></returns>
        [WebMethod]
        public string SetLiuXiang(string pid, List<string> aid, int type, int way, string authKey)
        {
            if (islogin(authKey))
            {
                string addrid = string.Join(",", aid);
                Link db = new Link();
                db.ChangeDBShop();
                string exits = string.Format("select * from liuxiang where product_id={0} and liuxiang_type={1}", pid, type);
                DataTable ex = db.ExecuteTable(exits);
                //不存在就新增
                if (ex.Rows.Count == 0)
                {
                    string sql = string.Format(@"insert into liuxiang(product_id
                                                  ,liuxiang_type
                                                  ,liuxiang_way
                                                  ,addr_id
                                                  ,created) 
                                                  values({0},{1},{2},'{3}',getdate())", pid, type, way, addrid);

                    db.ExecuteNonQuery(sql);
                    //更新Product表
                    sql = string.Format("UPDATE dbo.Product SET isLiuXiang=1 WHERE Product_ID={0}", pid);
                    db.ExecuteNonQuery(sql);
                    BLL.Log.AddLog(string.Format("设置商品流向控制(新增),product_id:{0},type:{1},way:{2},addrid:{3}", pid, type, way, addrid), 3, GetName(authKey));
                }
                else//修改
                {
                    string sql = string.Format("update liuxiang set liuxiang_way={0},addr_id='{1}',created=getdate() where product_id={2} and liuxiang_type={3}", way, addrid, pid, type);
                    int x = db.ExecuteNonQuery(sql);
                    BLL.Log.AddLog(string.Format("设置商品流向控制(修改),product_id:{0},type:{1},way:{2},addrid:{3}", pid, type, way, addrid), 3, GetName(authKey));
                    return x > 0 ? "设置修改成功！" : "设置修改失败！";

                }

                return "保存成功";
            }
            return "无授权";
        }
        /// <summary>
        /// 取消商品流向控制设定
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="type"></param>
        /// <param name="authKey"></param>
        /// <returns></returns>
        [WebMethod]
        public bool SetCancelLiuXiang(string pid, string authKey)
        {
            if (islogin(authKey))
            {
                Link db = new Link();
                db.ChangeDBShop();
                string sql = string.Format("delete  from liuxiang where product_id={0}", pid);
                db.ExecuteNonQuery(sql);
                //更新Product表
                sql = string.Format("UPDATE dbo.Product SET isLiuXiang=0 WHERE Product_ID={0}", pid);
                db.ExecuteNonQuery(sql);
                BLL.Log.AddLog(string.Format("取消商品流向控制,product_id:{0}", pid), 3, GetName(authKey));

                return true;
            }
            return false;
        }

        /// <summary>
        /// 取全部区域信息
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public DataTable GetRegionAll()
        {
            string sql = "SELECT * FROM dbo.Region";
            BLL.DbBase db = new BLL.DbBase();
            db.ChangeDBShop();
            return db.ExecuteTable(sql);
        }
        #endregion
        /// <summary>
        /// 取得全部价格配置
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public List<ConfigPriceMe> GetConfigPriceMeList(string authKey)
        {
            if (islogin(authKey))
            {
                return new ConfigPriceMe().GetList();
            }
            return null;
        }

        [WebMethod]
        /// <summary>
        /// 根据ID查询不同价格
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DataTable GetPriceByID(string ID)
        {
            string sql = "SELECT * FROM dbo.Price WHERE ID='" + ID + "'";
            BLL.DbBase db = new BLL.DbBase();
            return db.ExecuteTable(sql);
        }

        [WebMethod]
        /// <summary>
        /// 设置价格加点
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Plus"></param>
        public void SetPricePlus(string ID, decimal Plus, string CateGory)
        {
            string sql = string.Format("UPDATE dbo.Price SET Price_Plus={0} WHERE ID='{1}' AND CateGory='{2}'", Plus, ID, CateGory);
            BLL.DbBase db = new BLL.DbBase();
            db.ExecuteNonQuery(sql);
            //价格立即生效
            Price price = new Price();
            price.Update(ID, CateGory);
        }

        /// <summary>
        /// 取得设置数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="authKey"></param>
        /// <returns></returns>
        [WebMethod]
        public List<ConfigPriceMe> GetAllConfigPriceMe(string authKey)
        {
            if (islogin(authKey))
            {
                BLL.Data_Centre.ConfigPriceMe bll = new BLL.Data_Centre.ConfigPriceMe();
                return bll.GetList();
            }
            return null;
        }

        /// <summary>
        /// 取得设置数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="authKey"></param>
        /// <returns></returns>
        [WebMethod]
        public bool SetConfigPriceMe(string authKey, DataTable dt)
        {
            if (islogin(authKey))
            {
                BLL.Data_Centre.ConfigPriceMe bll = new BLL.Data_Centre.ConfigPriceMe();

                foreach (DataRow item in dt.Rows)
                {
                    decimal Price_Plus = Convert.ToDecimal(item["Price_Plus"]);
                    string CateGory = Convert.ToString(item["CateGory"]);
                    bll.UpdateToSql(CateGory, Price_Plus);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 取得合作企业列表
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public List<BLL.Data_Centre.Config> GetConfigList()
        {
            return new BLL.Data_Centre.Config().GetAllList();
        }

        /// <summary>
        /// 批量保存设置数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="authKey"></param>
        /// <returns></returns>
        [WebMethod]
        public bool SetConfigList(List<BLL.Data_Centre.Config> list, string authKey)
        {
            if (islogin(authKey))
            {
                BLL.Data_Centre.Config bll = new BLL.Data_Centre.Config();
                bll.Update(list);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
