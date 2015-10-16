using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data;
using System.Net;
using SOSOshop.BLL;
using SOSOshop.BLL.Common;
namespace _101shop.v3.Controllers
{
    public class HomeController : Controller
    {

        //
        // GET: /Home/           
        public ActionResult Index()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ClearCache"])) new SOSOshop.BLL.DbBase().ClearCache();
            if (!Request.IsAuthenticated)
            {
                WebClient web = new WebClient();
                web.Encoding = System.Text.Encoding.UTF8;
                string url = "http://" + Request.Url.Host + ":" + Request.Url.Port + "/Home/Index_Cache" + Request.Url.Query;
                string html = web.DownloadString(url);
                return Content(html);
            }
            IndexData();
            return View();
        }


        [OutputCache(Duration = 60 * 60 * 1)]
        public ActionResult Index_Cache()
        {
            IndexData();
            return View("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns>Dictionary pid和title</returns>
        public static Dictionary<string, string> GetADID(string code)
        {
            Advertising ad = new Advertising().GetModelByCode(code);
            string str = "0";
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (ad != null)
            {
                if (ad.ProductID != null)
                {
                    str = string.Join(",", ad.ProductID);
                }
                if (str == "")
                {
                    str = "0";
                }
                dic.Add("pid", str);
                dic.Add("title", ad.Title);
            }
            else
            {
                dic.Add("pid", str);
                dic.Add("title", "　");
            }
            return dic;
        }

        /// <summary>
        /// 首页取数据
        /// </summary>
        [NonAction]
        public void IndexData()
        {
            ViewBag.PriceInfo = Price.GetPriceShowString();
            SOSOshop.BLL.Product.Product bll = new SOSOshop.BLL.Product.Product();
            ViewBag.本周特贡 = bll.ExecuteTableForCache("SELECT " + SOSOshop.BLL.Product.Product._PriceTableColumns
                + SOSOshop.BLL.Product.Product._ProductInfoColumns_NotIn_PriceTableColumns + ", '￥9.5 ' noPrice,'￥9.2' showPrice FROM product_online_v WHERE Product_ID=151");
            ViewBag._101资讯 = bll.ExecuteTableForCache("SELECT TOP 5 id,Title,Channel,LinkUrl FROM dbo.yxs_article WHERE Channel LIKE('100104%') AND State=1 ORDER BY IsTop DESC,id DESC");
            //一排
            string pid = "839,1870,1573,25481";
            Dictionary<string, string> a1 = GetADID("首页_1");
            string pid1 = a1["pid"];
            ViewBag.A1 = a1["title"];

            if (pid1 != "")
            {
                pid = pid1;
            }
            ViewBag.疯狂抢购 = bll.GetPageList(pid).GetPriceTable();
            pid = "383,491,775,54";

            Dictionary<string, string> a2 = GetADID("首页_2");
            pid1 = a2["pid"];
            ViewBag.A2 = a2["title"];
            if (pid1 != "")
            {
                pid = pid1;
            }
            ViewBag.热销商品 = bll.GetPageList(pid).GetPriceTable();
            //ViewBag.新品上架 = bll.GetPageList("1202,57,609,44").GetSwitchPriceTable();
            ViewBag.本周新品 = bll.ExecuteTableForCache("SELECT TOP 4 a.* FROM dbo.product_online_v a  WHERE Image!='' and Goods_ID IN (SELECT Goods_ID FROM dbo.Goods_Image)", DateTime.Now.AddHours(1)).GetSwitchPriceTable();
            pid = "1549,4710,6838,34698";
            Dictionary<string, string> a4 = GetADID("首页_4");
            pid1 = a4["pid"];
            ViewBag.A4 = a4["title"];
            if (pid1 != "")
            {
                pid = pid1;
            }
            ViewBag.促销推荐 = bll.GetPageList(pid).GetSwitchPriceTable();

            string sql = "SELECT TOP 8 " + SOSOshop.BLL.Product.Product._PriceTableColumns + "Product_SellingPoint,Product_Advertisement,maid1,ggy1,drug_sensitive,Product_ID,DrugsBase_Specification,DrugsBase_Manufacturer,Product_Name,Image, " +
                                  "Product_SaleNum/Goods_pcs Product_SaleNum" +
                                 ",Goods_ConveRatio_Unit_Name,Goods_ConveRatio_Unit,Goods_ConveRatio,'件' Goods_Unit, DrugsBase_ID " +
                                 "FROM product_online_v p " +
                                 "where (Price_01  is not null and Price_01!=0.000000) " +
                                 "AND p.Goods_Pcs > 1 " +
                                 "AND p.DrugsBase_Manufacturer != '云南升和药业股份有限公司' " +//2014-1-23修改 不显示升和的商品
                                 "AND DrugsBase_ID in (SELECT product_id FROM dbo.Tag_PharmProduct WHERE Tag_PharmAttribute_id IN (SELECT id FROM Tag_PharmAttribute WHERE tag_id=69)) " +
                                 "ORDER BY Product_SaleNum DESC";
            //二排(中标基药)
            ViewBag.z热销榜 = bll.ExecuteTableForCache(sql).GetSwitchPriceTable();

            //三排(高毛利) 
            ViewBag.g热销榜 = bll.ExecuteTableForCache("SELECT TOP 8  " + SOSOshop.BLL.Product.Product._PriceTableColumns + "Product_SellingPoint,Product_Advertisement,maid1,ggy1,Product_ID,DrugsBase_Specification,DrugsBase_Manufacturer,Product_Advertisement,Product_Name, Image," +
                             "Goods_pcs, Goods_ConveRatio_Unit_Name,Goods_ConveRatio_Unit,Goods_ConveRatio,Goods_Unit,drug_sensitive,Product_SaleNum " +
                             "FROM product_online_v p " +
                             "where Price_02>0 " +
                             "ORDER BY Product_SaleNum DESC ").GetSwitchPriceTable();

            //中药材
            ViewBag.热销榜3f = bll.ExecuteTableForCache("SELECT TOP 8  " + SOSOshop.BLL.Product.Product._PriceTableColumns + "Product_SellingPoint,Product_Advertisement,maid1,ggy1,Product_ID,DrugsBase_Specification,DrugsBase_Manufacturer,Product_Advertisement,Product_Name, Image,Goods_pcs, Goods_ConveRatio_Unit_Name,Goods_ConveRatio_Unit,Goods_ConveRatio,Goods_Unit,drug_sensitive,Product_SaleNum  FROM product_online_v p where DrugsBase_ID IN (SELECT DrugsBase_ID FROM dbo.DrugsBase_PharmMediNameLink WHERE Pharm_ID IN (	SELECT Pharm_ID FROM dbo.DrugsBase_Pharm WHERE Pharm_ID_Path LIKE('%\\2973%'))) ORDER BY Product_SaleNum DESC ").GetSwitchPriceTable();

            //医疗器械
            ViewBag.热销榜4f = bll.ExecuteTableForCache("SELECT TOP 8  " + SOSOshop.BLL.Product.Product._PriceTableColumns + "Product_SellingPoint,Product_Advertisement,maid1,ggy1,Product_ID,DrugsBase_Specification,DrugsBase_Manufacturer,Product_Advertisement,Product_Name, Image,Goods_pcs, Goods_ConveRatio_Unit_Name,Goods_ConveRatio_Unit,Goods_ConveRatio,Goods_Unit,drug_sensitive,Product_SaleNum  FROM product_online_v p where DrugsBase_ID IN (SELECT DrugsBase_ID FROM dbo.DrugsBase_PharmMediNameLink WHERE Pharm_ID IN (	SELECT Pharm_ID FROM dbo.DrugsBase_Pharm WHERE Pharm_ID_Path LIKE('%\\2968%'))) ORDER BY Product_SaleNum DESC ").GetSwitchPriceTable();
            //保健品
            ViewBag.热销榜5f = bll.ExecuteTableForCache("SELECT TOP 8  " + SOSOshop.BLL.Product.Product._PriceTableColumns + "Product_SellingPoint,Product_Advertisement,maid1,ggy1,Product_ID,DrugsBase_Specification,DrugsBase_Manufacturer,Product_Advertisement,Product_Name, Image,Goods_pcs, Goods_ConveRatio_Unit_Name,Goods_ConveRatio_Unit,Goods_ConveRatio,Goods_Unit,drug_sensitive,Product_SaleNum  FROM product_online_v p where DrugsBase_ID IN (SELECT DrugsBase_ID FROM dbo.DrugsBase_PharmMediNameLink WHERE Pharm_ID IN (	SELECT Pharm_ID FROM dbo.DrugsBase_Pharm WHERE Pharm_ID_Path LIKE('%\\3051%'))) ORDER BY Product_SaleNum DESC ").GetSwitchPriceTable();
            //计生用品
            ViewBag.热销榜6f = bll.ExecuteTableForCache("SELECT TOP 8  " + SOSOshop.BLL.Product.Product._PriceTableColumns + "Product_SellingPoint,Product_Advertisement,maid1,ggy1,Product_ID,DrugsBase_Specification,DrugsBase_Manufacturer,Product_Advertisement,Product_Name, Image,Goods_pcs, Goods_ConveRatio_Unit_Name,Goods_ConveRatio_Unit,Goods_ConveRatio,Goods_Unit,drug_sensitive,Product_SaleNum  FROM product_online_v p where DrugsBase_ID IN (SELECT DrugsBase_ID FROM dbo.DrugsBase_PharmMediNameLink WHERE Pharm_ID IN (	SELECT Pharm_ID FROM dbo.DrugsBase_Pharm WHERE Pharm_ID_Path LIKE('%\\3075%'))) ORDER BY Product_SaleNum DESC ").GetSwitchPriceTable();


            ViewBag.交易大厅 = GetTrancactionRecordForDisplay();
        }

        /// <summary>
        /// 分类导航
        /// </summary>
        /// <returns></returns>
        [OutputCache(CacheProfile = "public")]
        public ActionResult Navigate()
        {
            return View();
        }

        public ActionResult jydt()
        {
            return View();
        }

        /// <summary>
        /// 搜索URL拼接
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loc"></param>
        /// <returns></returns>
        public static string SearchUrl(int id, int loc)
        {
            int[] urls = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 };
            urls[loc] = id;
            return string.Format("/products/{0}.html", string.Join("-", urls));
        }
        /// <summary>
        /// 搜索URL拼接
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loc"></param>
        /// <returns></returns>
        public static string SearchUrl(int[] id, int[] loc)
        {
            int[] urls = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 };
            for (int i = 0; i < id.Length; i++)
            {
                urls[loc[i]] = id[i];
            }
            return string.Format("/products/{0}.html", string.Join("-", urls));
        }

        /// <summary>
        /// 获取总的交易记录数据（30条）
        /// </summary>
        /// <param name="bll"></param>
        /// <returns></returns>
        public DataTable GetTrancactionRecordForCache()
        {
            SOSOshop.BLL.Product.Product bll = new SOSOshop.BLL.Product.Product();
            DataTable transactionRecordForCache = bll.ExecuteTableForCache(@"select top 30 
                                                  a.Id,
                                                  c.Product_Name,
                                                  c.DrugsBase_Specification,
                                                  DrugsBase_Manufacturer,
                                                  a.ConsigneeProvince+ConsigneeCity+ConsigneeBorough ReceiveAddress,
                                                  proPrice*ProNum cost,
                                                  a.OrderDate
                                                  from orders a,OrderProduct b,product c where a.OrderId=b.OrderId and b.ProId=c.product_ID order by a.OrderDate desc
            ", DateTime.Now.AddHours(1));

            return transactionRecordForCache;
        }

        /// <summary>
        /// 获得需要展示的6条数据
        /// </summary>
        /// <returns></returns>
        private DataTable GetTrancactionRecordForDisplay(int Take = 6)
        {
            
            var dt=GetTrancactionRecordForCache();
            if (dt != null)
            {
                var resultTable = dt.Clone();
                if (dt.Rows.Count > 0)
                {
                    for (int i = dt.Rows.Count-1; i >= 0; i--)
                    {
                        resultTable.Rows.Add(dt.Rows[i].ItemArray);
                        if (resultTable.Rows.Count >= 6)
                            break;
                    }
                    //DataTable tmpTable = GetTrancactionRecordForCache().AsEnumerable().Take(Take).CopyToDataTable();
                    //resultTable = tmpTable.Copy();
                }
                return resultTable;
            }
            return null;
        }

        /// <summary>
        /// 首页获取最新交易记录（交易平台）
        /// </summary>
        /// <returns></returns>
        public ActionResult TransactionPlatformData()
        {
            SOSOshop.BLL.Product.Product bll = new SOSOshop.BLL.Product.Product();
            int id = string.IsNullOrEmpty(Request["id"].ToString().Trim()) ? 0 : Convert.ToInt32(Request["id"].ToString().Trim());
            string addTime = string.IsNullOrEmpty(Request["time"]) ? "" : Convert.ToString(Request["time"]);


            DataTable transactionRecordForCache = GetTrancactionRecordForCache();
            DataTable newTransactionData = transactionRecordForCache.Clone();

            int j = 0;
            for (int i = transactionRecordForCache.Rows.Count-1; i >= 0; i--)
            {
                var item = transactionRecordForCache.Rows[i];
                string addTime2 = item["OrderDate"].ToString();
                if (id<Convert.ToInt32(item["Id"]))
                {
                    newTransactionData.Rows.Add(item.ItemArray);
                    break;
                }
            }
                //foreach (DataRow item in transactionRecordForCache.Rows)
                //{
                //    //string addTime2 = item["rq"].ToString() + item["ontime"].ToString();
                //    string addTime2 = item["OrderDate"].ToString();
                //    //if (!item["rq"].ToString().Equals("") && !item["ontime"].ToString().Equals(""))
                //    //{
                //    if ((Convert.ToInt32(item["Id"].ToString().Trim()) == id) && (addTime2.Equals(addTime)))
                //    {
                //        DataRow row;
                //        if (j == transactionRecordForCache.Rows.Count - 1)
                //        {
                //            row = transactionRecordForCache.Rows[0];
                //        }
                //        else
                //        {
                //            row = transactionRecordForCache.Rows[j + 1];
                //        }

                //        newTransactionData.ImportRow(row);

                //        break;
                //    }
                //    //}
                //    j++;
                //}

            ViewBag.交易大厅新数据 = newTransactionData;

            return View();
        }

        //独立的交易大厅
        public ActionResult g()
        {
            ViewBag.交易大厅 = GetTrancactionRecordForDisplay(12);
            return View();
        }

        public ActionResult mquery(string q)
        {
            string search = Library.Lang.Input.Filter(q);
            List<string> list = new List<string>();
            SOSOshop.BLL.Product.Product bll = new SOSOshop.BLL.Product.Product();
            bll.ChangeShop();
            string sql = "SELECT DISTINCT top 10 Product_Name,Product_ClickNum FROM dbo.product_online_v ORDER BY Product_ClickNum DESC";
            if (!string.IsNullOrEmpty(search))
            {
                sql = string.Format("SELECT DISTINCT top 10 Product_Name FROM dbo.product_online_v WHERE Product_Name LIKE '{0}%' OR DrugsBase_SimpeCode LIKE '{0}%'", search);
            }
            DataTable dt = bll.ExecuteTableForCache(sql, search);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(dr["product_name"].ToString());
                }
            }
            return Content((string.Join("|x" + Environment.NewLine, list)));
        }
    }
}
