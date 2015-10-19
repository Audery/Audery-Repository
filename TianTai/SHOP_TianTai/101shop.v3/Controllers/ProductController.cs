using System;
using System.Web.Mvc;
using System.Data;
using System.Text;
using System.Collections.Specialized;
using SOSOshop.BLL;
using SOSOshop.BLL.Common;

namespace _101shop.v3.Controllers
{
    public class ProductController : Controller
    {
        // 商品内容展示?id
        // GET: /Product/

        /// <summary>
        /// 是否近效期
        /// </summary>
        public bool ExpirationTime
        {
            get
            {
                return !string.IsNullOrEmpty(Library.Lang.Input.Filter(Request["Expiration"])) && Library.Lang.Input.Filter(Request["Expiration"]) == "1";
            }

        }

        public ActionResult Index(int? id)
        {
            DateTime StartTime = DateTime.Now, InitTime = StartTime;
            StringBuilder InitData = new StringBuilder();
            if (id == null)
            {
                return Redirect("/");
            }
            ViewBag.ProductID = id.ToString();
            SOSOshop.BLL.Product.Product bll = new SOSOshop.BLL.Product.Product();
            int Member_Class;
            Price.GetMemberpermission(out Member_Class);

            InitData.Append("开始-商品信息:" + (DateTime.Now - InitTime).TotalSeconds + "s|"); InitTime = DateTime.Now;
            bool clearCache = !string.IsNullOrEmpty(Request["clearCache"]);//清除缓存

            DataTable PriceTable = bll.GetProduct((int)id,
                SOSOshop.BLL.Product.Product._ProductInfoColumns_NotIn_PriceTableColumns + ","//品种信息
                + SOSOshop.BLL.Product.Product._PriceTableColumns//价格信息
                + "(SELECT TOP 1 Image FROM Goods_Picture WHERE Goods_ID=product_online_v.Goods_ID) pro_Picture, "//彩页
                + "(SELECT TOP 1 a.Drugsbase_Direct_Context FROM Drugsbase_Direct AS a INNER JOIN DrugsBase AS b ON a.Drugsbase_Direct_ID=b.Drugsbase_Direct_ID WHERE b.Drugsbase_ID=product_online_v.Drugsbase_ID) Drugsbase_Direct, "//说明书
                + "created, drug_sensitive,is_cl"//其他
                , clearCache);
            InitData.Append("结束-商品信息:" + (DateTime.Now - InitTime).TotalSeconds + "s|"); InitTime = DateTime.Now;
            if (PriceTable != null && PriceTable.Rows.Count > 0)
            {
                //实时销售方式处理 清除缓存
                int sellType = 1; int.TryParse(Convert.ToString(PriceTable.Rows[0]["sellType"]), out sellType);
                int sellType1 = 1; int.TryParse(Convert.ToString(bll.GetProductAttr((int)id, SOSOshop.BLL.Product.Product.sellTypeSql())), out sellType1);
                if (sellType != sellType1)
                {
                    clearCache = true;
                    PriceTable = bll.GetProduct((int)id,
                SOSOshop.BLL.Product.Product._ProductInfoColumns_NotIn_PriceTableColumns + ","//品种信息
                + SOSOshop.BLL.Product.Product._PriceTableColumns//价格信息
                + "(SELECT TOP 1 Image FROM Goods_Picture WHERE Goods_ID=product_online_v.Goods_ID) pro_Picture, "//彩页
                + "(SELECT TOP 1 a.Drugsbase_Direct_Context FROM Drugsbase_Direct AS a INNER JOIN DrugsBase AS b ON a.Drugsbase_Direct_ID=b.Drugsbase_Direct_ID WHERE b.Drugsbase_ID=product_online_v.Drugsbase_ID) Drugsbase_Direct, "//说明书
                + "created, drug_sensitive,is_cl"//其他
                , clearCache);
                }

                PriceTable = PriceTable.GetPriceTable();
                SOSOshop.Model.ProductInfo model = bll.ReaderBind(PriceTable.Rows[0], Member_Class);
                ViewBag.ProductDataRow = PriceTable.Rows[0];//原始数据行

                string maxsell = null;

                if (string.IsNullOrEmpty(maxsell))
                {
                    maxsell = "0";
                }
                ViewBag.MaxSell = maxsell;
                ViewBag.OldPrice = PriceTable.Rows[0]["OrigPrice"];
                ViewBag.Price = PriceTable.Rows[0]["Price"];
                ViewBag.IsProp = PriceTable.Rows[0]["iscu"];

                DateTime addDate = DateTime.MinValue;
                DateTime.TryParse(PriceTable.Rows[0]["created"].ToString(), out addDate);
                ViewBag.AddedDate = addDate.ToShortDateString();

                ViewBag.spid = PriceTable.Rows[0]["spid"];
                ViewBag.pihao = PriceTable.Rows[0]["pihao"];
                ViewBag.sxrq = PriceTable.Rows[0]["sxrq"];
                SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();
                //国家基药
                int DrugsBase_bNationalEssentialDrug = 0;
                if (model.tag_ids.IndexOf(",66,") >= 0)
                {
                    DrugsBase_bNationalEssentialDrug = 1;
                }
                ViewBag.DrugsBase_bNationalEssentialDrug = DrugsBase_bNationalEssentialDrug;

                InitData.Append("开始-适应症分类:" + (DateTime.Now - InitTime).TotalSeconds + "s|"); InitTime = DateTime.Now;

                //OTC适应症分类
                string syzsql = string.Format("select id,name from Tag_PharmAttribute where fullPath like '%/'+convert(varchar,(select top 1 case when ParentId=0 then id else ParentId end from Tag_PharmAttribute where id in(SELECT Tag_PharmAttribute_id FROM Tag_PharmProduct WHERE product_key='d' and product_id={0}) and tag_id=71))+'/%' order by ParentId", model.DrugsBase_ID);
                //取该商品适应症的二级类型名称
                string sql2level = string.Format("select id,name from Tag_PharmAttribute where id in(select id from Tag_PharmAttribute where id in(SELECT Tag_PharmAttribute_id FROM Tag_PharmProduct WHERE product_key='d' and product_id={0}) and tag_id=71)", model.DrugsBase_ID);
                DataSet ds_fl = db.ExecuteDataSetForCache(syzsql + " " + sql2level, DateTime.Now.AddDays(1));
                ViewBag.SyzList = ds_fl.Tables.Count > 0 ? ds_fl.Tables[0] : new DataTable();
                ViewBag.SyzName = "";
                StringBuilder syzname = new StringBuilder();
                bool frist = true; DataTable dt = ds_fl.Tables.Count > 1 ? ds_fl.Tables[1] : new DataTable();
                foreach (DataRow dr in dt.Rows)
                {
                    //适应症产品推荐
                    if (frist)
                    {
                        string sqlSyzTj = string.Format("select top 4 * from product_online_v where DrugsBase_ID in( select product_id from Tag_PharmProduct where product_key='d' and Tag_PharmAttribute_id={0})", dr["id"]);
                        ViewBag.SyzTjTitle = dr["name"].ToString();
                        ViewBag.SyzTj = db.ExecuteTableForCache(sqlSyzTj).GetPriceTable();
                        ViewBag.SyzTjCount = db.ExecuteScalarForCache(sqlSyzTj.Replace("top 4 *", "count(distinct product_id)"));
                        frist = false;
                    }
                    //显示所有贴有标签的类
                    ViewBag.OtcLink = _101shop.v3.Controllers.HomeController.SearchUrl(int.Parse(dr["id"].ToString()), 1);
                    syzname.AppendFormat("<a href='{0}'>{1}</a>&nbsp;&nbsp;", _101shop.v3.Controllers.HomeController.SearchUrl(int.Parse(dr["id"].ToString()), 1), dr["name"].ToString());
                }
                ViewBag.SyzName = syzname.ToString();
                InitData.Append("结束-适应症分类:" + (DateTime.Now - InitTime).TotalSeconds + "s|"); InitTime = DateTime.Now;

                //获取账户ViewBag.UID,ViewBag.Member_IsLogOn是否登陆?ViewBag.UserType,ViewBag.Member_Type,ViewBag.Member_Class,ViewBag.MemberPermission权限等
                BaseController.SetAccount(ViewBag);
                InitData.Append("会员账户信息:" + (DateTime.Now - InitTime).TotalSeconds + "s|"); InitTime = DateTime.Now;
                //获取其他价格
                InitData.Append("其他价格信息:" + (DateTime.Now - InitTime).TotalSeconds + "s|"); InitTime = DateTime.Now;

                decimal RetailPrice = 0;//建议零售价
                if (Member_Class != 1)//建议零售价
                {
                    RetailPrice = model.RetailPrice;
                }

                decimal Price_Temp = 0;
                decimal.TryParse(PriceTable.Rows[0]["Price"].ToString(), out Price_Temp);
                //是否批发
                ViewBag.is_pf = model.Price_01 > 0;
                //是否可拆零
                ViewBag.is_cl = PriceTable.Rows[0]["is_cl"];

                //袋装量
                var obj = bll.ExecuteScalar("SELECT BagCapacity FROM dbo.DrugsBase_ZYC WHERE DrugsBase_ID={0}", model.DrugsBase_ID);

                ViewBag.BagCapacity = obj != null ? Math.Round(Convert.ToDecimal(obj), 2) : 1;
                ViewBag.Price = Price_Temp;
                ViewBag.ShowPrice = PriceTable.Rows[0]["showPrice"].ToString();

                string minsell = string.Empty;
                if (ViewBag.is_cl == "是")
                {
                    minsell = PriceTable.Rows[0]["minsell"].ToString();
                }
                else
                {
                    minsell = "1";
                }
                ViewBag.minsell = minsell;
                //包装盒、彩页
                ViewBag.pro_Image = "" + PriceTable.Rows[0]["Original"];
                string pro_Picture = "" + PriceTable.Rows[0]["pro_Picture"];
                if (!pro_Picture.ToLower().Contains("/picture/"))//旧的彩页
                {
                    pro_Picture = pro_Picture.ToLower().Replace(".jpg", "-sy1.jpg");
                }
                ViewBag.pro_Picture = pro_Picture;
                //说明书
                ViewBag.Drugsbase_Direct = "" + PriceTable.Rows[0]["Drugsbase_Direct"];

                string sql = "SELECT a.Stock-ISNULL(b.Stock,0) FROM Product a LEFT JOIN Stock_Lock b ON a.Product_ID = b.Product_ID WHERE a.Product_ID=" + id;
                //近效期特殊处理
                ViewBag.ISExpirationTime = false;
                if (ExpirationTime)
                {
                    sql = string.Format("SELECT  *  FROM Product_ExpirationTime  Product_ExpirationTime WHERE product_id={0}", id);

                    DataTable DT_ExpirationTime = bll.ExecuteTable(sql);
                    if (DT_ExpirationTime != null && DT_ExpirationTime.Rows.Count > 0)
                    {
                        ViewBag.ISExpirationTime = true;
                        ViewBag.Goods_Unit = DT_ExpirationTime.Rows[0]["Goods_Unit"].ToString();
                        //取近效期价格
                        ViewBag.ShowPrice = DT_ExpirationTime.Rows[0]["Price"].ToString();
                    }
                    //锁库查询
                    sql = string.Format("SELECT stock - ISNULL(dbo.fn_select_Expiration_lockStock({0}),0) FROM Product_ExpirationTime  WHERE product_id={0}", id);
                }
                int Product_Stock = 0; int.TryParse(Convert.ToString(db.ExecuteScalar(sql)), out Product_Stock);
                //库存取整
                if (sellType == 2) { int zbz = 0; int.TryParse(Convert.ToString(PriceTable.Rows[0]["Goods_Pcs_Small"]), out zbz); if (zbz > 0 && Math.Floor((double)Product_Stock / zbz) <= 0) Product_Stock = 0; }
                if (sellType == 3) { int jz = 0; int.TryParse(Convert.ToString(PriceTable.Rows[0]["Goods_Pcs"]), out jz); if (jz > 0 && Math.Floor((double)Product_Stock / jz) <= 0) Product_Stock = 0; }
                ViewBag.Product_Stock = Product_Stock;
                InitData.Append("实时库存:" + (DateTime.Now - InitTime).TotalSeconds + "s|"); InitTime = DateTime.Now;

                //药品导航
                DataTable DrugsBase_Pharm_Of_Product = new SOSOshop.BLL.DrugsBase.DrugsBase_Pharm().GetListByDrugsBase_ID(model.DrugsBase_ID);
                ViewBag.DrugsBase_Pharm_Of_Product = DrugsBase_Pharm_Of_Product;
                InitData.Append("药品导航:" + (DateTime.Now - InitTime).TotalSeconds + "s|"); InitTime = DateTime.Now;
                //其他生产厂家
                DataTable drugsBase_Manufacturer = new SOSOshop.BLL.DrugsBase.DrugsBase_Manufacturer().GetOhterManufacturerList(50, model.Product_Name, model.DrugsBase_Manufacturer);
                ViewBag.DrugsBase_Manufacturer_Of_ProductList = drugsBase_Manufacturer;
                if (drugsBase_Manufacturer != null)
                {
                    ViewBag.DrugsBase_Manufacturer_Count = DrugsBase_Pharm_Of_Product == null ? 0 : DrugsBase_Pharm_Of_Product.Rows.Count;
                }
                InitData.Append("其他生产厂家:" + (DateTime.Now - InitTime).TotalSeconds + "s|"); InitTime = DateTime.Now;

                #region 本月热销排行榜
                //本月热销排行榜
                ViewBag.Member_Class = Member_Class;
                try
                {
                    string sqlhot = string.Empty;
                    if (Member_Class == 1)
                    {
                        sqlhot = "SELECT TOP 8  " + SOSOshop.BLL.Product.Product._PriceTableColumns + "ggy1,drug_sensitive,Product_ID,DrugsBase_Specification,DrugsBase_Manufacturer,Product_Name,Product_Advertisement,Image," +
                                 "Product_SaleNum as SaleNum,Goods_ConveRatio_Unit_Name,Goods_ConveRatio_Unit,Goods_ConveRatio,Goods_Unit, DrugsBase_ID " +
                                 "FROM product_online_v p " +
                                 "where (Price_02  is not null and Price_02!=0.000000) " +
                                 "ORDER BY SaleNum DESC";
                    }
                    else
                    {
                        sqlhot = "SELECT TOP 8  " + SOSOshop.BLL.Product.Product._PriceTableColumns + "ggy1,Product_ID,DrugsBase_Specification,DrugsBase_Manufacturer,Product_Advertisement,Product_Name, Image," +
                                 "( " +
                                     "SELECT SUM(Product_SaleNum/Goods_pcs) " +
                                     "FROM product_online_v " +
                                     "WHERE DrugsBase_ID=p.DrugsBase_ID " +
                                 ")SaleNum,Goods_ConveRatio_Unit_Name,Goods_ConveRatio_Unit,Goods_ConveRatio,'件' Goods_Unit,drug_sensitive,Product_SaleNum " +
                                 "FROM product_online_v p " +
                                 "where (Price_01  is not null and Price_01!=0.000000)  and DrugsBase_Manufacturer != '云南升和药业股份有限公司' " +
                                 "AND p.Goods_Pcs != 1 " +
                                 "ORDER BY SaleNum DESC";
                    }
                    ViewBag.ThisMonthRanking_ProductList = db.ExecuteTableForCache(sqlhot).GetPriceTable();
                    InitData.Append("本月热销排行榜:" + (DateTime.Now - InitTime).TotalSeconds + "s|"); InitTime = DateTime.Now;
                }
                catch
                {
                    ViewBag.ThisMonthRanking_ProductList = new DataTable();
                }
                #endregion

                #region 分类热销品种
                //分类热销品种
                ViewBag.DrugsBase_Pharm1_Of_Product_Class = null;
                ViewBag.DrugsBase_Pharm1_Of_Product_ClassList = null;
                if (DrugsBase_Pharm_Of_Product != null)
                {
                    NameValueCollection na = new NameValueCollection();
                    string[] Pharm_ID_Path = DrugsBase_Pharm_Of_Product.Rows[0]["Pharm_ID_Path"].ToString().Trim('\\').Split('\\');
                    string[] Pharm_Name_Path = DrugsBase_Pharm_Of_Product.Rows[0]["Pharm_Name_Path"].ToString().Trim('\\').Split('\\');
                    if (Pharm_ID_Path.Length == Pharm_Name_Path.Length)
                    {
                        for (var i = 0; i < Pharm_Name_Path.Length; i++)
                        {
                            na[Pharm_Name_Path[i]] = Pharm_ID_Path[i];
                        }
                    }
                    ViewBag.DrugsBase_Pharm1_Of_Product = na;
                    string name = na.Keys.Get(na.Count - 1);
                    int value = int.Parse(na[na.Count - 1]);
                    ViewBag.ThisClass = name;//分类
                    ViewBag.ThisClassID = value;//分类ID
                    //分类热销品种列表
                    DataTable ThisClass_ProductList = bll.Get_ThisClass_ProductList(4, value).GetPriceTable();
                    ViewBag.ThisClass_ProductList = ThisClass_ProductList;
                    ViewBag.ThisClass_ProductList_Count = bll.Get_ThisClass_ProductList_Count(value);
                }
                InitData.Append("分类热销品种:" + (DateTime.Now - InitTime).TotalSeconds + "s|"); InitTime = DateTime.Now;
                #endregion

                #region 厂家的其他品种列表
                //厂家的其他品种列表
                DataTable ThisDrugsBase_Manufacturer_Of_ProductList = bll.Get_ThisDrugsBase_Manufacturer_Of_ProductList(8, model.DrugsBase_Manufacturer, 0).GetPriceTable();
                int ThisDrugsBase_Manufacturer_Of_ProductList_Count = ThisDrugsBase_Manufacturer_Of_ProductList == null ? 0 : ThisDrugsBase_Manufacturer_Of_ProductList.Rows.Count;
                if (ThisDrugsBase_Manufacturer_Of_ProductList_Count > 0)
                    foreach (DataRow dr in ThisDrugsBase_Manufacturer_Of_ProductList.Rows)
                        if ((int)dr["Product_ID"] == model.Product_ID) { ThisDrugsBase_Manufacturer_Of_ProductList.Rows.Remove(dr); break; }
                ViewBag.ThisDrugsBase_Manufacturer_Of_ProductList = ThisDrugsBase_Manufacturer_Of_ProductList;
                ViewBag.ThisDrugsBase_Manufacturer_Of_ProductList_Count = bll.Get_ThisDrugsBase_Manufacturer_Of_ProductList_Count(model.DrugsBase_Manufacturer, (int)id);
                InitData.Append("厂家的其他品种列表:" + (DateTime.Now - InitTime).TotalSeconds + "s|"); InitTime = DateTime.Now;

                #endregion

                //访问日志
                if (User.Identity.IsAuthenticated && ViewBag.UID != null && ViewBag.Member_IsLogOn == true)
                {
                    MemberBrowserProductContentLog bllLog = new SOSOshop.BLL.MemberBrowserProductContentLog();
                    bllLog.RecordBrowse(ViewBag.UID, id);
                    //最近浏览过的商品
                    ViewBag.History_Of_ProductList = bll.Get_History_Of_ProductList(10, (int)ViewBag.UID).GetPriceTable();
                }

                InitData.Append("最近浏览过的商品:" + (DateTime.Now - InitTime).TotalSeconds + "s|");
                ViewBag.InitData = InitData + "总共:" + (DateTime.Now - StartTime).TotalSeconds + "s";

                return View(model);
            }
            else
            {
                return View("Error");
            }
        }
    }
}
