using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SOSOshop.BLL;
using System.Data;
using YXShop.BLL.Order;
using System.Text;
using SOSOshop.Model;
using System.Collections;
using System.Configuration;
using SOSOshop.BLL.Common;
using _101shop.Common;

namespace _101shop.v3.Controllers
{
    public class ShoppingcartController : Controller
    {
        Db db = new Db();
        Cart cart = new Cart();
        int UID = BaseController.GetUserId();
        public SOSOshop.BLL.MemberPermission memberper = new SOSOshop.BLL.MemberPermission();
        Memberfavorite favorite = new Memberfavorite();
        public ReceAddress address = new ReceAddress();
        provinces city = new provinces();
        Dictionary<string, string> DelList;
        public ShoppingcartController()
        { }
        public ShoppingcartController(ControllerContext cc)
        {
            base.ControllerContext = cc;
        }
        //
        // GET: /Shoppingcart/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 加入购物车
        /// </summary> 
        /// 返回 0.成功 1.未登录 2.未审核 3.冻结 4.失败 ,10. 是四部控销品种，且没有购买的权限
        public string Addcart()
        {
            //此品种需要 xxx 的资质，您的无此资质，请联系采购顾问。

            int ret = 4;
            string productID = Request["pid"] == null ? "" : System.Web.HttpContext.Current.Request["pid"].ToString();
            int jian = (!Library.Lang.DataValidator.isNumber(Request["jian"])) ? 0 : Convert.ToInt32(Request["jian"]);

            var tempcount = Library.Lang.Input.Filter(Request["count"]);
            Decimal count = 1;
            decimal.TryParse(tempcount, out count);
            //近效期产品
            bool ExpirationTime = !string.IsNullOrEmpty(Library.Lang.Input.Filter(Request["Expiration"])) && Library.Lang.Input.Filter(Request["Expiration"]) == "1";
            ret = addCarts(productID, jian, count, ExpirationTime: ExpirationTime);
            if (ret == 0)
            {
                if (Session[UID.ToString()] != null)
                {
                    DelList = Session[UID.ToString()] as Dictionary<string, string>;
                    DelList.Remove(productID);
                }
            }
            return ShowInfo(ret);
        }

        /// <summary>
        /// 为客户端返回用户操作商品进入购物车后的状态
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string ShowInfo(int s, string msg = "")
        {
            int uid = BaseController.GetUserId();
            string ret = "";
            switch (s)
            {
                case 0:
                    ret = BaseController.Json(0, "商品已成功加入购物车！", "/Shoppingcart/MyShoppingCart");
                    break;
                case 1:
                    ret = BaseController.Json(1, "您未登录，不能加入购物车，自动跳转到登录页面", "/Account/LogOn");
                    break;
                case 2:
                    ret = BaseController.Json(2, "您还未通过审核，暂时不能购买商品！", "/MemberCenter/Upgrade");
                    break;
                case 3:
                    ret = BaseController.Json(3, "您已被冻结，暂时不能购买商品！");
                    break;
                case 6:
                    if (uid > 0)
                    {

                        SOSOshop.BLL.MemberInfo bll = new SOSOshop.BLL.MemberInfo();
                        if (bll.GetMember_Class(uid) == Member_Class.OTC客户)
                        {
                            ret = BaseController.Json(6, "仅限商业客户购买！", "/MemberCenter/Upgrade");
                        }
                        else
                        {
                            ret = BaseController.Json(6, "仅限OTC客户购买！", "/MemberCenter/Upgrade");
                        }
                    }
                    else
                    {
                        ret = BaseController.Json(6, "您无购买权限！", "/MemberCenter/Upgrade");
                    }
                    break;
                case 7:
                    ret = BaseController.Json(7, "您无交易权限！", "/MemberCenter/Upgrade");
                    break;
                case 8:
                    ret = BaseController.Json(8, "商品不存在或已下架！");
                    break;
                case 10:
                    ret = BaseController.Json(10, "控销品种，请提交申请协议！");
                    break;
                case 11://合作公司不能超过库存
                    ret = BaseController.Json(11, msg);
                    break;
                case 12:
                    {
                        SOSOshop.BLL.MemberInfo bll = new SOSOshop.BLL.MemberInfo();
                        if (bll.GetMember_Class(uid) == Member_Class.OTC客户)
                        {
                            ret = BaseController.Json(6, "仅限商业客户购买！", "/MemberCenter/Upgrade");
                        }
                        else
                        {
                            ret = BaseController.Json(6, "此为敏感控销药品，详询右上角相关采购顾问！", "/MemberCenter/Upgrade");
                        }
                        break;
                    }
                case 13:
                    {
                        ret = BaseController.Json(13, "此品种不在您所在的区域销售"); break;
                    }
                case 14:
                    {
                        ret = BaseController.Json(14, "此品种不在您所在的经营范围销售"); break;
                    }
                default:
                    ret = BaseController.Json(4, "出现未知错误，购买失败！Code=[" + s + "]");
                    break;
            }
            return ret;
        }
        /// <summary>
        /// 此商品是否需呀显示控销连接
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static bool isshowKong(int productID)
        {
            //此版本无控销
            return false;
        }

        /// <summary>
        /// 取用户当天指定品种购买的数量
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        private int GetUserTodayBuyCount(int productID)
        {
            string sql = string.Format(@"SELECT ISNULL(SUM(ProNum),0) as num FROM OrderProduct WHERE orderid IN (SELECT OrderId FROM orders WHERE ShopDate BETWEEN ( SELECT   BeginDate
                                               FROM     dbo.Product
                                               WHERE    Product_ID = {1} AND DATEDIFF(DAY,GETDATE(),AddTime)=0
                                             )
                                     AND      ( SELECT   EndDate
                                               FROM     dbo.Product
                                               WHERE    Product_ID = {1}
                                             ) AND ReceiverId={0}) AND proid={1}", Public.GetUserId(), productID);
            return (int)db.ExecuteScalar(sql);
        }

        /// <summary>
        /// 取当天指定品种购买过的数量
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        private int GetTodayBuyCount(int productID)
        {
            string sql = string.Format(@"SELECT ISNULL(SUM(ProNum),0) as num FROM OrderProduct WHERE orderid IN (SELECT OrderId FROM orders WHERE ShopDate BETWEEN ( SELECT   BeginDate
                                               FROM     dbo.Product
                                               WHERE    Product_ID = {0} AND DATEDIFF(DAY,GETDATE(),AddTime)=0
                                             )
                                     AND      ( SELECT   EndDate
                                               FROM     dbo.Product
                                               WHERE    Product_ID = {0}
                                             )) AND proid={0}", productID);
            return (int)db.ExecuteScalar(sql);
        }

        private int GetBuyCount(int productID)
        {
            string sql = string.Format(@"SELECT  ISNULL(SUM(ProNum), 0) AS num
                                        FROM    OrderProduct
                                        WHERE   orderid IN ( SELECT OrderId
                                                             FROM   orders
                                                             WHERE  ShopDate BETWEEN ( SELECT   BeginDate
                                                                                       FROM     dbo.Product
                                                                                       WHERE    Product_ID = {0}
                                                                                     )
                                                                             AND      ( SELECT   EndDate
                                                                                       FROM     dbo.Product
                                                                                       WHERE    Product_ID = {0}
                                                                                     ) )
                                                AND proid = {0}", productID);
            return (int)db.ExecuteScalar(sql);
        }




        /// <summary>
        /// 产品加入购物车
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="jian"></param>
        /// <param name="count"></param>
        /// <returns>返回 0.成功 1.未登录 2.未审核 3.冻结 4.失败 5.无查看批发权限 6.无价格权限[新处理：仅限批发或OTC客户可预订！] 7.无交易权限 8.商品不存在或已下架，10. 是四部控销品种，且没有购买的权限,11 合作公司的品种则不能超过库存  </returns>
        public int addCarts(string productID, int jian, Decimal count, bool isCopy = false, bool ExpirationTime = false)
        {
            var bll = new SOSOshop.BLL.Db();
            int uid = BaseController.GetUserId(), pid = 0;
            if (int.TryParse(productID, out pid) == false) return 4;//4.失败,商品不存在           
            if (uid == 0)
            {
                return 1;
            }
            #region 新增流向判断by陈佳宇
            bool isLiuXiang = (bool)bll.ExecuteScalarForCache(string.Format("SELECT isLiuXiang FROM dbo.Product  WHERE Product_ID={0}", pid));

            if (isLiuXiang)
            {
                DataTable DtLiuxiang = bll.ExecuteTableForCache(string.Format("SELECT liuxiang_way,addr_id FROM dbo.liuxiang WHERE product_id={0}", pid));
                int Borough = (int)bll.ExecuteScalarForCache(string.Format("SELECT Borough FROM dbo.memberinfo WHERE uid={0}", uid));
                if (DtLiuxiang != null && DtLiuxiang.Rows.Count > 0 && Borough > 0)
                {
                    //1表示控制销售，0表示控制不销售
                    int liuxiang_way = Convert.ToInt32(DtLiuxiang.Rows[0][0]);
                    string addr_id = Convert.ToString(DtLiuxiang.Rows[0][1]);
                    bool ISContains = addr_id.Split(',').Contains(Borough.ToString());
                    if ((liuxiang_way == 1 && !ISContains) || (liuxiang_way == 0 && ISContains))
                    {
                        //如果是设置控制销售，只要区域不包含此客户地域，则无法销售
                        //如果是设置控制不销售，只要区域包含此客户区域，则无法销售
                        return 13;
                    }
                }

            }
            #endregion

            #region 新增经营范围判断by陈佳宇
            if (ConfigurationManager.AppSettings["IsBusinessScopeEnable"].Trim() == "1")
            {
                string ProductBusinessScope = bll.ExecuteScalarForCache(string.Format("SELECT ISNULL(BussinessScopeCode,'') AS BussinessScopeCode FROM dbo.Product where product_id={0}", pid)).ToString();
                if (!string.IsNullOrEmpty(ProductBusinessScope))
                {

                    var Temp = bll.ExecuteScalarForCache(string.Format(@"SELECT  [ID]
                                            FROM    [MemberBusinessScope]
                                            WHERE   UID = '{0}'
                                                    AND BussinessScopeCode = '{1}'", uid, ProductBusinessScope));
                    if (Temp == null)
                    {
                        //没有对应的经营范围
                        return 14;
                    }

                }
            }

            #endregion

            int State = (int)bll.ExecuteTableForCache("SELECT State FROM dbo.memberaccount where uid=" + uid).Rows[0][0];
            if (State == 1)  //未审核用户
            {
                return 2;
            }
            else if (State == 2)
            {
                return 3;
            }
            //无交易权限
            if (isSee() == 4)
            {
                return 7;
            }
            if (productID != "")
            {
                DataTable table = GetProList(productID);
                if (table == null)
                {
                    return 8;
                }
                else if (table.Rows.Count == 0)
                {
                    return 8;
                }
                else
                {
                    if ((bool)table.Rows[0]["drug_sensitive"])
                    {
                        return 12;
                    }
                    try
                    {
                        if (table.Rows[0]["showPrice"] as string == "暂时无货")
                        {
                            Response.Write(BaseController.Json(11, "暂时无货，请采购同品类其他药品!"));
                            Response.End();
                        }
                        decimal price = (decimal)table.Rows[0]["Price"];
                        if (price <= 0)//没有用户角色对应的体系价格
                        {
                            return 6;
                        }
                    }
                    catch
                    {
                        return 6;
                    }
                    int jz = Convert.ToInt32(table.Rows[0]["jz"]); if (jz <= 0) jz = 1;//件装量
                    int zbz = Convert.ToInt32(table.Rows[0]["zbz"]); if (zbz <= 0) zbz = 1;//中包装

                    int sellType = Convert.ToInt32(table.Rows[0]["sellType"]); if (sellType <= 0) sellType = 1;//销售方式(1,不限制,2中包装，3整件)
                    if (count == 0 && jian == 0)
                    {
                        count = jz;
                    }

                   
                    //销售方式处理
                    switch ((SellTypes)Enum.Parse(typeof(SellTypes), sellType.ToString()))
                    {
                        case SellTypes.整件:
                            if (!isCopy)
                            {
                                count = count * jz;
                            }
                            break;
                        case SellTypes.中包装:
                            if (zbz > 0 && count % zbz > 0)
                            {
                                Response.Write(BaseController.Json(11, "购买数量必须是中包装【" + zbz + "】的整数倍"));
                                Response.End();
                                return 11;
                            }
                            break;
                    }

                    if (count > 0)
                    {
                        //判断如果是合作公司的品种则不能超过库存,和如果机构id非 000也不能购买大于库存数量
                        string sql = "SELECT a.Stock-ISNULL(b.Stock,0) FROM Product a LEFT JOIN Stock_Lock b ON a.Product_ID = b.Product_ID WHERE a.Product_ID=" + productID;
                        if (ExpirationTime)
                        {
                            sql = string.Format("SELECT stock - ISNULL(dbo.fn_select_Expiration_lockStock({0}),0) FROM Product_ExpirationTime  WHERE product_id={0}", productID);
                        }
                        double Stock = 0; double.TryParse(Convert.ToString(bll.ExecuteScalar(sql)), out Stock);

                        if (Stock <= 0 || (sellType.Equals(2) && (Convert.ToInt32(Stock / zbz) <= 0)) || (sellType.Equals(3) && (Convert.ToInt32(Stock / jz) <= 0)))
                        {
                            Response.Write(BaseController.Json(11, "暂时无货"));
                            Response.End();
                            return 11;
                        }
                        //超过库存提醒
                        string StockTip = sellType.Equals(3) ? Convert.ToInt32(Stock / jz) + "件" : Stock + "";
                        if (count > Convert.ToDecimal(Stock))
                        {
                            Response.Write(BaseController.Json(11, "购买数量不能超过库存最大数量【" + StockTip + "】"));
                            Response.End();
                            return 11;
                        }

                        SOSOshop.BLL.MemberInfo bllmi = new SOSOshop.BLL.MemberInfo();
                        var mclass = bllmi.GetMember_Class(BaseController.GetUserId());

                        //处理促销商品，检查是否超出促销库存规定
                        if ((bool)table.Rows[0]["iscu"] && !ExpirationTime)
                        {
                            int maxsell = Convert.ToInt32(table.Rows[0]["maxsell"]);
                            int minsell = Convert.ToInt32(table.Rows[0]["minsell"]);
                            int daysell = Convert.ToInt32(table.Rows[0]["otcminsell"]);
                            int buyStock = GetBuyCount(Convert.ToInt32(productID));
                            int todaybuystock = GetTodayBuyCount(Convert.ToInt32(productID));
                            int todaystock = GetUserTodayBuyCount(Convert.ToInt32(productID));
                            //可用库存数=(促销总库存-促销时间段内已销售库存-当天已售库存-当天个人已购买数
                            int useStock = (maxsell - buyStock) > daysell ? daysell - todaybuystock : (maxsell - buyStock);
                            //会员当天可用库存
                            useStock = useStock > minsell ? minsell - todaystock : useStock - todaystock;
                            //判断用户数量是否超出每天每会员的限购数量
                            if (useStock < count)
                            {
                                Response.Write(BaseController.Json(11, "促销商品今天的最大购买数量不能超过【" + useStock + "】"));
                                Response.End();
                                return 11;
                            }
                            if (todaystock >= minsell)
                            {
                                Response.Write(BaseController.Json(11, "促销商品今天的最大购买数量不能超过【" + useStock + "】"));
                                Response.End();
                                return 11;
                            }
                        }


                        if (cart.AddByContent(productID, count, uid.ToString(), ExpirationTime))
                        {
                            try
                            {
                                //用户行为访问统计
                                SOSOshop.Model.Member.MemberAction model = new SOSOshop.Model.Member.MemberAction();
                                if (RouteData != null)
                                {
                                    model.action = RouteData.Values["action"] as string;
                                    model.controller = RouteData.Values["controller"] as string;
                                }
                                else
                                {
                                    model.action = "Addcart";
                                    model.controller = "Shoppingcart";
                                }
                                //model.distinguishability = string.Format("{0}x{0}", Request.Browser.ScreenPixelsWidth, Request.Browser.ScreenPixelsHeight);
                                model.HttpMethod = Request.HttpMethod;
                                model.OS = Public.GetOSNameByUserAgent(Request.ServerVariables["HTTP_USER_AGENT"]);
                                model.Query = Request.Url.Query;
                                model.sessionid = Session.SessionID;
                                model.url = Request.Url.ToString();
                                model.uid = Public.GetUserId();
                                model.WebBrowser = Request.Browser.Browser + Request.Browser.Version;
                                model.actuation = "添加到购物车[spid:" + productID + "]";
                                if (Request.UrlReferrer.ToString().ToLower().Contains("products"))
                                {
                                    model.ActuationValue = "列表页添加";
                                }
                                else if (Request.UrlReferrer.ToString().ToLower().Contains("jy"))
                                {
                                    model.ActuationValue = "基药列表页添加";
                                }
                                else
                                {
                                    model.ActuationValue = "详细页添加";
                                }
                                new SOSOshop.BLL.Member.MemberAction().Add(model, true);
                            }
                            catch (Exception e)
                            {

                            }
                            return 0;
                        }
                        else
                        {
                            return 5;
                        }
                    }
                }
                return 4;
            }
            return 4;
        }

        public string AddFavorite()
        {
            string pid = string.IsNullOrEmpty(Request["pid"]) ? "" : Request["pid"];
            int UID = BaseController.GetUserId();
            if (UID == 0)
                return "login";
            pid = Library.Lang.Input.Filter(pid);
            if (pid.Trim() != "")
            {
                string str = favorite.AddMemberFavorite(UID, Convert.ToInt32(pid));
                if (str == "1")
                    return "关注成功！";
                if (str == "0")
                    return "no";
                if (str == "2")
                    return "已关注过！";
            }
            return "no";
        }


        /// <summary>
        /// 提交到我的购物车
        /// </summary>
        /// <returns></returns>
        public ActionResult SubmitToShoppingCart()
        {
            if (UID == 0)
                return RedirectToAction("MyShoppingCart", "Shoppingcart");
            //直接提交，不进行数据验证
            char sep = ',';
            string pids = Convert.ToString(Request["pids"]).TrimEnd(sep);
            string nums = Convert.ToString(Request["nums"]).TrimEnd(sep);
            if (pids.Length > 0 && nums.Length > 0)
            {
                string[] pid = pids.Split(sep);
                string[] num = nums.Split(sep);
                if (pid.Length == num.Length)
                {
                    DateTime now = DateTime.Now;
                    string CartKey = ChangeHope.Common.DEncryptHelper.GetRandomNumber();
                    StringBuilder sql = new StringBuilder();
                    for (int i = 0; i < pid.Length; i++)
                    {
                        int p = 0; int.TryParse(pid[i], out p);
                        int n = 0; int.TryParse(num[i], out n);
                        if (p > 0 && n > 0)
                            sql.AppendFormat("delete from yxs_cart where productid={2} and uid={5} insert yxs_cart values({0},{1},{2},{3},'',0,'','',1,'{4}',{5}) \n",
                                UID, CartKey, p, n, now, UID);
                    }
                    if (sql.Length > 0)
                    {
                        var bll = new SOSOshop.BLL.Db();
                        bll.ExecuteNonQuery(sql.ToString());
                    }
                }
            }
            return RedirectToAction("MyShoppingCart", "Shoppingcart");
        }
        /// <summary>
        /// 查看我的购物车
        /// </summary>
        /// <returns></returns>
        public ActionResult MyShoppingCart()
        {
            int press = isSee();
            if (press == 0)
            {
                Dictionary<string, string> delp = null;
                if (Session[UID.ToString()] != null)
                    delp = Session[UID.ToString()] as Dictionary<string, string>;
                if (delp != null && delp.Keys.Count != 0)
                {
                    string proID = "";
                    foreach (string key in delp.Keys)
                    {
                        proID += key + ",";
                    }
                    proID = proID.Substring(0, proID.Length - 1);
                    ViewBag.DelPro = GetProList(proID);
                }
                ViewBag.product = GetShoppingCartProduct();

                //用户类型
                SOSOshop.BLL.MemberInfo member = new SOSOshop.BLL.MemberInfo();
                ViewBag.Memberclass = member.GetModel(BaseController.GetUserId()).Member_Class;//用户类型 0 批发客户，1 OTC批零客户
            }
            else
            {
                string ret = null;
                switch (press)
                {
                    case 1:
                        ViewBag.errortype = 1;
                        ret = "请您先登录，再来操作。";
                        break;
                    case 4:
                        ViewBag.errortype = 4;
                        ret = "您无交易权限，请您升级为企业会员！";
                        break;
                }
                ViewBag.error = ret;
                if (ViewBag.errortype == 1)
                {
                    return RedirectToAction("LogOn", "Account");
                }
                if (ViewBag.errortype == 4)
                {
                    return RedirectToAction("Upgrade", "MemberCenter");
                }
            }
            return View();
        }

        public ActionResult GetMyShoppingCartInMenu()
        {
            if (isSee() == 0)
            {
                DataTable dt = GetShoppingCartProduct();
                List<Hashtable> li = new List<Hashtable>();
                decimal totalprice = 0;
                foreach (System.Data.DataRow item in dt.Rows)
                {
                    Hashtable ht1 = new Hashtable();
                    ht1.Add("id", item["id"]);
                    ht1.Add("pid", item["product_id"]);
                    ht1.Add("name", item["name"]);
                    ht1.Add("gg", (item["gg"].ToString().Length > 20) ? item["gg"].ToString().Substring(0, 20) : item["gg"]);
                    ht1.Add("sl", item["sl"]);
                    ht1.Add("dw", item["dw"] == DBNull.Value ? "" : item["dw"]);
                    ht1.Add("image", item["image"] == DBNull.Value ? "" : item["image"]);
                    ht1.Add("price", item["showprice"]);
                    //ht1.Add("DrugsBaseCount", item["DrugsBaseCount"]);

                    li.Add(ht1);
                    totalprice += decimal.Parse(item["sl"].ToString()) * decimal.Parse(item["price"].ToString());

                }
                Hashtable ht = new Hashtable();
                ht.Add("yaoli", li);
                ht.Add("price", string.Format("{0:f2}", totalprice));
                return Json(ht,JsonRequestBehavior.AllowGet);
                //return Json.JsonConvert.SerializeObject(ht);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取商品信息
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public DataTable GetProList(string pid)
        {
            string sql = "select " + SOSOshop.BLL.Product.Product._PriceTableColumns + @"drug_sensitive, Product_ID,Product_ID as pid,DrugsBase_Manufacturer as cj, DrugsBase_Specification as gg,Product_Name as name,Goods_Pcs as jz,Goods_Pcs_Small as zbz,is_cl,( SELECT    CASE ISNULL(ExpirationTime, '')
                      WHEN '' THEN 0
                      ELSE 1
                    END
          FROM      Product_ExpirationTime
          WHERE     product_online_v.Product_ID = Product_ID
        ) AS IsExpirationProduct from product_online_v where [Product_ID] in (" + pid + ")";
            return db.ExecuteTable(sql).GetPriceTable();
        }

        /// <summary>
        /// 删除购物车中的商品
        /// </summary>
        public string DelShoppingCartProduct()
        {
            DataTable table = new DataTable();
            if (UID == 0)
            {
                return "login";
            }
            string pid = string.IsNullOrEmpty(Request.Form["pid"]) ? "" : Request.Form["pid"];
            string count = string.IsNullOrEmpty(Request.Form["count"]) ? "" : Request.Form["count"];
            pid = Library.Lang.Input.Filter(pid);
            count = Library.Lang.Input.Filter(count);
            if (string.IsNullOrEmpty(pid)) return "ok";
            if (pid.Trim() != "")
            {
                if (cart.DelShoppingCartProduct(Convert.ToInt32(pid), UID))
                {
                    if (Session[UID.ToString()] != null && (Session[UID.ToString()] as Dictionary<string, string>) != null)
                    {
                        DelList = Session[UID.ToString()] as Dictionary<string, string>;
                        string x = null;
                        if (!DelList.TryGetValue(pid, out x))
                        {
                            DelList.Add(pid, count);
                        }
                    }
                    else
                    {
                        DelList = new Dictionary<string, string>();
                        DelList.Add(pid, count);
                        Session[UID.ToString()] = DelList;
                    }
                    return "ok";
                }
                else
                    return "no";
            }
            return "no";
        }



        /// <summary>
        /// 获取购物车商品信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetShoppingCartProduct()
        {
            return GetShoppingCart();
        }

        public DataTable GetShoppingCart(string ids = null)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select p.Goods_Unit as dw,");
            sql.Append(SOSOshop.BLL.Product.Product._PriceTableColumns1);
            sql.Append("isnull((select isnull(stock,0) from Stock_Lock where Product_ID=p.Product_ID),0) as stock1,");
            sql.Append("p.is_cl,");
            sql.Append("p.Product_ID,");
            sql.Append("p.drug_sensitive,");
            sql.Append("p.Product_Name as name,");
            sql.Append("p.DrugsBase_Manufacturer as cj,");
            sql.Append("p.DrugsBase_Specification as gg,");
            sql.Append("p.Goods_Pcs as jz,");
            sql.Append("p.Goods_Pcs_Small as zbz,");
            sql.Append("c.quantity as sl,");
            sql.Append("c.[uid] as [uid],");
            sql.Append("c.id,p.Image as image,");
            sql.Append("Goods_ConveRatio,Goods_Unit,");
            sql.Append("p.DrugsBase_Specification,");
            sql.Append("Goods_ConveRatio_Unit_Name,");
            sql.Append("0 iden,");
            sql.Append("Goods_ConveRatio_Unit,");
            sql.Append("IsExpirationProduct, ");
            //袋装量
            sql.Append(@"ISNULL(( SELECT ISNULL(BagCapacity, 1) AS BagCapacity
                 FROM   dbo.DrugsBase_ZYC
                 WHERE  DrugsBase_ID = p.DrugsBase_ID
               ), 1) AS BagCapacity");
            sql.AppendFormat(" from product_online_v p INNER JOIN (select id,productid,quantity,uid,IsExpirationProduct from yxs_cart) c ON p.Product_ID=c.productid and c.uid={0} {1} order by Product_Name asc,DrugsBase_Specification asc", UID, string.IsNullOrEmpty(ids) ? "" : " and c.id in(" + ids + ")");
            DataTable shoppingCarProList = db.ExecuteTable(sql.ToString()).GetPriceTable();
            ViewBag.ShoppingCarLimitProNum = GetShoppingCarLimitProNum(shoppingCarProList);

            return shoppingCarProList;
        }

        /// <summary>
        /// 取得购物车中的品种的限购数量
        /// </summary>
        /// <returns></returns>
        public DataTable GetShoppingCarLimitProNum(DataTable shoppingCarProList)
        {
            string sql = null;
            string proIds = null;

            if (shoppingCarProList != null)
            {
                if (shoppingCarProList.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(proIds))
                    {
                        proIds = proIds.TrimStart(',');
                    }
                }
            }

            sql = string.Format(@"SELECT ProId, SUM(proNum) as proNum
                    FROM dbo.OrderProduct AS a
                    WHERE [Status] != 6
	                      AND OrderId IN (SELECT OrderId  FROM dbo.Orders WHERE ReceiverId={0} AND ProId IN ({1}) AND DATEDIFF(DAY, OrderDate, GETDATE())=0)
                    GROUP BY ProId", UID, proIds);

            if (string.IsNullOrEmpty(proIds))
            {
                return null;
            }
            else
            {
                return db.ExecuteTable(sql);
            }
        }

        /// <summary>
        /// 填写核对订单信息
        /// </summary>
        /// <returns></returns>

        public ActionResult CheckOrders()
        {
            if (Request.UrlReferrer == null)
            {
                return Redirect("/Shoppingcart/MyShoppingCart");
            }
            if (!Request.UrlReferrer.ToString().ToLower().EndsWith("myshoppingcart")
                && !Request.UrlReferrer.ToString().ToLower().Contains("membercenter/cartpro")
                && !Request.UrlReferrer.ToString().ToLower().Contains("shoppingcart/checkorders"))
            {
                return Redirect("/Shoppingcart/MyShoppingCart");
            }

            if (isSee() == 4)
            {
                ViewBag.error = "您无交易权限，请与管理员联系，谢谢！<a href='/MemberCenter/Upgrade'>点击这里</a>提交资料成为企业会员";
                return View();
            }
            if (isSee() == 1)
            {
                ViewBag.error = "您未登陆，或登陆状态已失效，请重新登陆，谢谢！<a href='/account/logon'>点击这里</a>登陆网站";
                return View();
            }

            //取用户授权信息
            int memberClass = 0;
            ViewBag.member = memberper.GetModel(UID);
            memberClass = new SOSOshop.BLL.MemberInfo().GetModel(UID).Member_Class;
            ViewBag.Memberclass = memberClass;

            //用户在购物车中选中的商品ID
            string cid = Request["cid"];
            ViewBag.cid = cid;
            DataTable dt = GetShoppingCart(cid);

            #region 检查订单的合法性
            SOSOshop.BLL.Product.Product bll = new SOSOshop.BLL.Product.Product();
            foreach (DataRow item in dt.Rows)
            {
                int productID = (int)item["product_id"];
                //判断如果是合作公司的品种则不能超过库存
                string sql = "SELECT a.Stock-ISNULL(b.Stock,0) FROM Product a LEFT JOIN Stock_Lock b ON a.Product_ID = b.Product_ID WHERE a.Product_ID=" + productID;
                decimal Stock = 0; decimal.TryParse(Convert.ToString(bll.ExecuteScalar(sql)), out Stock);
                int jz = int.Parse(item["jz"].ToString()); if (jz <= 0) jz = 1;//件装
                int zbz = int.Parse(item["zbz"].ToString()); if (zbz <= 0) zbz = 1;//中包装
                int sellType = int.Parse(item["sellType"].ToString()); if (sellType <= 0) sellType = 1;//销售方式


                if (Stock <= 0 || (sellType.Equals(2) && (Convert.ToInt32(Stock / zbz) <= 0)) || (sellType.Equals(3) && (Convert.ToInt32(Stock / jz) <= 0)))
                {
                    Library.Client.Jscript.AlertAndRedirect("【" + item["name"] + "】暂时无货!", "/Shoppingcart/MyShoppingCart");
                    return Content("");
                }
                //超过库存提醒
                string StockTip = sellType.Equals(3) ? Convert.ToInt32(Stock / jz) + "件" : Stock + "";
                if (Convert.ToDecimal(item["sl"]) > Stock)
                {
                    Library.Client.Jscript.AlertAndRedirect("【" + item["name"] + "】购买数量不能超过库存最大数量【" + StockTip + "】", "/Shoppingcart/MyShoppingCart");
                    return Content("");
                }


                //处理促销商品，检查是否超出促销库存规定
                if ((bool)item["iscu"])
                {
                    int maxsell = Convert.ToInt32(item["maxsell"]);
                    int minsell = Convert.ToInt32(item["minsell"]);
                    int daysell = Convert.ToInt32(item["otcminsell"]);
                    int buyStock = GetBuyCount(Convert.ToInt32(productID));
                    int todaybuystock = GetTodayBuyCount(Convert.ToInt32(productID));
                    int todaystock = GetUserTodayBuyCount(Convert.ToInt32(productID));
                    //可用库存数=(促销总库存-促销时间段内已销售库存-当天已售库存-当天个人已购买数
                    int useStock = (maxsell - buyStock) > daysell ? daysell - todaybuystock : (maxsell - buyStock);
                    //会员当天可用库存
                    useStock = useStock > minsell ? minsell - todaystock : useStock - todaystock;
                    //判断用户数量是否超出每天每会员的限购数量
                    if (useStock < (int)item["sl"])
                    {
                        Library.Client.Jscript.AlertAndRedirect("【" + item["name"] + "】促销商品今天的最大购买数量不能超过【" + useStock + "】", "/Shoppingcart/MyShoppingCart");
                        return Content("");
                    }
                }
            }
            #endregion
            //取选中商品的列表
            ViewBag.product = dt;
            //取用户收货地址列表         
            ViewBag.Address = address.GetAddressListByWhere(" and uid=" + UID + " order by stat");
            //取用户单位列表
            ViewBag.WorkList = BaseController.GetUserWorkList();
            return View();
        }

        /// <summary>
        /// 删除购物车/选中商品
        /// </summary>
        /// <returns></returns>
        public string DelCart()
        {
            string pids = string.IsNullOrEmpty(Request["pids"]) ? "" : Request["pids"].ToString().Substring(0, Request["pids"].ToString().Length - 1);
            string counts = string.IsNullOrEmpty(Request["counts"]) ? "" : Request["counts"].ToString().Substring(0, Request["counts"].ToString().Length - 1);
            if (string.IsNullOrEmpty(pids))
            {
                return "no1";
            }
            pids = Library.Lang.Input.Filter(pids);
            if (pids.Trim() != "")
            {
                string[] proids = pids.Split(',');
                string[] sl = counts.Split(',');
                cart.DeleteCartByMemberId(UID.ToString(), pids);
                if (Session[UID.ToString()] != null)
                {
                    for (int i = 0; i < proids.Length; i++)
                    {
                        DelList = Session[UID.ToString()] as Dictionary<string, string>;
                        string x = null;
                        if (!DelList.TryGetValue(proids[i], out x) && counts.Length > i)
                        {
                            DelList.Add(proids[i], sl[i]);
                        }
                    }
                }
                else
                {
                    DelList = new Dictionary<string, string>();
                    DelList.Add(proids[0], sl[0]);
                    Session[UID.ToString()] = DelList;
                    for (int i = 1; i < proids.Length; i++)
                    {
                        DelList = Session[UID.ToString()] as Dictionary<string, string>;
                        string x = null;
                        if (!DelList.TryGetValue(proids[i], out x) && counts.Length > i)
                        {
                            DelList.Add(proids[i], sl[i]);
                        }

                    }
                }
                return "ok";
            }
            return "no";
        }

        public string deleteCart()
        {
            return "";
        }


        public ActionResult aa()
        {
            return View("AddAddress");
        }

        /// <summary>
        /// 获取省份信息
        /// </summary>
        /// <returns></returns>
        public string GetCity()
        {
            string pid = string.IsNullOrEmpty(Request["pid"]) ? "" : Request["pid"];
            if (pid != "" && pid != "-1")
            {
                StringBuilder citys = new StringBuilder();
                foreach (DataRow dv in city.GetCity(pid).Rows)
                {
                    citys.Append("{name:\"" + dv["Name"] + "\",ID:\"" + dv["Id"] + "\"},");
                }
                return "{\"city\":[" + citys.ToString() + "]}";
            }
            return "";
        }

        /// <summary>
        /// 是否可以操作购物车0表示可以
        /// </summary>
        /// <returns>0表示可以操作购物，1表示未登录，2批发客户没有查看批发价格的，3 OTC没有批零查看权限，4 没有交易权限</returns>
        public int isSee()
        {
            //初始化用户权限
            SOSOshop.Model.MemberPermission model = new SOSOshop.Model.MemberPermission();
            SOSOshop.BLL.MemberPermission m = new SOSOshop.BLL.MemberPermission();

            //获取用户id
            int uid = BaseController.GetUserId();
            if (uid > 0)
            {
                if (!m.GetBuyFilingStatus(uid))
                {
                    //未建档不能操作(2013-6-26设定)
                    //去掉不能操作，对应为快捷开通(2013-7-2设定)
                    //return 4;

                    //获取用户类型 0 批发客户，1 OTC批零客户
                    SOSOshop.BLL.MemberInfo member = new SOSOshop.BLL.MemberInfo();
                    int user_class = member.GetModel(uid).Member_Class;
                    ViewBag.UserClass = user_class;
                    model = m.GetModel(uid);
                    //交易权限去掉（2013.7.2）
                    //调整给快捷开通权限(2013.7.2)
                    if (model.IsSpecialTrade)
                    {
                        //批发客户

                        //if (user_class == 0)
                        //{
                        /*    if (!model.IsLookPrice_01)
                            {
                                return 2;
                            }*/
                        // }
                        // else if (user_class == 1)//OTC客户
                        // {
                        /*     if (!model.IsLookPrice_02)
                             {
                                 return 3;
                             }*/
                        // }
                    }
                    else
                    {
                        return 4;
                    }
                }
            }
            else
            {
                //未登录用户
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// 添加用户地址
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string AddUseAddress(UserAddress model)
        {
            string s = "";
            int uid = BaseController.GetUserId();
            if (uid > 0)
            {
                if (model != null && !string.IsNullOrEmpty(model.Province) && !string.IsNullOrEmpty(model.City)
                    && model.Province.ToLower() != "null" && model.City.ToLower() != "null")
                {
                    string sql = "";
                    SOSOshop.BLL.Db db = new Db();
                    if (model.ID == 0)
                    {
                        sql = "insert into memberreceaddress([uid],[username],[mobile],[phone],[province],[city],[borough],[address],[zip],[email],stat)"
                            + " values(" + uid + ","
                            + "'" + model.Name.Trim() + "',"
                            + "'" + model.Mobile + "',"
                            + "'" + model.Phone + "',"
                            + "'" + model.Province + "',"
                            + "'" + model.City + "',"
                            + "'" + model.Borough + "',"
                            + "'" + model.Address + "',"
                            + "'" + model.Zip + "',"
                            + "'" + model.Email + "'"
                            + ",1) SELECT SCOPE_IDENTITY()";
                    }
                    else
                    {
                        sql = "update memberreceaddress set username='" + model.Name.Trim()
                            + "',mobile='" + model.Mobile
                            + "',phone='" + model.Phone
                            + "',province='" + model.Province
                            + "',city='" + model.City + "',borough='" + model.Borough
                            + "',address='" + model.Address
                            + "',zip='" + model.Zip
                            + "',email='" + model.Email
                            + "' where id=" + model.ID
                            + " SELECT " + model.ID;
                    }
                    try
                    {
                        object ret = db.ExecuteScalar(sql);
                        if (ret != null)
                        {
                            s = "{\"state\":1,\"message\":\"" + ret + "\"}";
                        }
                        else
                        {
                            s = "{\"state\":0,\"message\":\"添加失败！\"}";
                        }
                    }
                    catch
                    {
                        s = "{\"state\":-1,\"message\":\"添加失败！\"}";
                    }
                }
                else
                {
                    s = "{\"state\":-1,\"message\":\"添加失败！\"}";
                }
            }
            else
            {
                s = "{\"state\":-1,\"message\":\"登录过期，请重新登录！\"}";
            }
            return s;
        }

        [HttpPost]
        public string DeleteUseAddress()
        {
            string s = "";
            if (User.Identity.IsAuthenticated)
            {
                string aid = Request["aid"];
                if (string.IsNullOrEmpty(aid))
                {
                    s = "{\"state\":-1,\"message\":\"操作失败！\"}";
                }
                else
                {
                    if (address.DeleteAddress(aid, BaseController.GetUserId().ToString()))
                    {
                        s = "{\"state\":1,\"message\":\"删除成功\"}";
                    }
                    else
                    {
                        s = "{\"state\":-1,\"message\":\"操作失败！\"}";
                    }
                }

            }
            else
            {
                s = "{\"state\":-1,\"message\":\"登录过期，请重新登录！\"}";
            }
            return s;
        }

        /// <summary>
        /// 订单处理 将购物车的内容放入订单表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult OrdersOk()
        {
            ViewBag.url = Request.UrlReferrer;
            //用户所选的商品id
            string cartid = Request["cid"];
            if (!string.IsNullOrEmpty(cartid))
            {
                DataTable dt = GetShoppingCart(cartid);
                if (!User.Identity.IsAuthenticated)
                {
                    ViewBag.error = "登录过期，请重新登录后再来提交订单，谢谢！";
                }
                else if (dt.Rows.Count > 0)//商品数量大于零
                {
                    try
                    {
                        //用户ID
                        int uid = BaseController.GetUserId();

                        //收货人地址ID
                        SOSOshop.Model.UserAddress ua = new SOSOshop.Model.UserAddress();
                        string sname = Request.Form["listaddress"];
                        if (!string.IsNullOrEmpty(sname))
                        {
                            SOSOshop.BLL.ReceAddress ra = new SOSOshop.BLL.ReceAddress();
                            ua = ra.getModelorCity(address.GetAddressListByWhereArea(" and uid=" + uid + " and a.id=" + sname).Rows[0]);
                        }
                        else
                        {
                            ViewBag.error = "没选择收货人地址，请重新选择！";
                            return View();
                        }

                        //付款方式
                        int pay = 2;
                        //支付类型
                        int paytype = 2;

                        if (string.IsNullOrEmpty(Request["payway"]))
                        {
                            ViewBag.error = "未知付款方式，请重新选择！";
                            return View();
                        }
                        else
                        {
                            pay = int.Parse(Request["payway"]);
                        }

                        ViewBag.PayWay = pay;

                        //判断用户是否有该付款方式的权限
                        SOSOshop.Model.MemberPermission mp = new SOSOshop.Model.MemberPermission();
                        mp = memberper.GetModel(uid);
                        switch (pay)
                        {
                            case 1:
                                if (!mp.IsCOD)
                                {
                                    ViewBag.error = "对不起，您暂无货到付款的权限。";
                                    return View();
                                }
                                break;
                            case 2:
                                if (!mp.IsMoneyAndShipping)
                                {
                                    ViewBag.error = "对不起，您暂无款到发货的权限。";
                                    return View();
                                }
                                break;
                            case 3:
                                if (!mp.IsPeriodicalSettle)
                                {
                                    ViewBag.error = "对不起，您暂无定期结算的权限。";
                                    return View();
                                }
                                break;
                        }

                        if (pay == 2)
                        {
                            if (string.IsNullOrEmpty(Request["paytype"]))
                            {
                                ViewBag.error = "未知支付类型，请重新选择！";
                                return View();
                            }
                            else
                            {
                                paytype = int.Parse(Request["paytype"]);
                            }
                        }
                        ViewBag.Paytype = paytype;

                        //配送方式
                        string carriage = Request["songhuoway"];
                        if (string.IsNullOrEmpty(carriage))
                        {
                            ViewBag.error = "未知配送方式，请重新选择！";
                            return View();
                        }

                        //送货时间
                        string carriagetime = Request["songhuotime"];
                        if (string.IsNullOrEmpty(carriagetime))
                        {
                            ViewBag.error = "未知配送方式，请重新选择！";
                            return View();
                        }

                        //是否开发票
                        int invoice = 0;
                        BaseController worker = new BaseController();
                        UserWorderModel uwm = new UserWorderModel();
                        if (BaseController.GetUserWorkList().Rows.Count == 0)
                        {
                            uwm.ID = 0;
                            uwm.Name = "";
                        }
                        else
                        {
                            uwm = worker.GetUserModel(BaseController.GetUserWorkList().Rows[0]);
                        }
                        if (!string.IsNullOrEmpty(Request["invoice"]))
                        {
                            invoice = 1;
                            if (!string.IsNullOrEmpty(Request["workid"]))
                            {
                                uwm = worker.GetUserModel(BaseController.GetUserWorker(int.Parse(Request["workid"])));
                            }
                        }


                        //备注
                        string remark = "";
                        if (!string.IsNullOrEmpty(Request["remark"]))
                        {
                            remark = Request["remark"];
                        }

                        //先发有货商品
                        bool issend = false;
                        if (!string.IsNullOrEmpty(Request["issend"]))
                        {
                            issend = true;
                        }

                        //添加订单
                        SOSOshop.BLL.Order.Orders bll = new SOSOshop.BLL.Order.Orders();
                        SOSOshop.Model.Order.Orders order = new SOSOshop.Model.Order.Orders();
                        List<SOSOshop.Model.Order.OrderProduct> list = new List<SOSOshop.Model.Order.OrderProduct>();
                        order.Invoice = invoice;
                        order.Carriage = int.Parse(carriage);
                        order.ConsignesTime = carriagetime;

                        order.parentCorpName = uwm.Name;
                        order.parentid = uwm.ID;//16522
                        order.Payment = pay;
                        order.PaymentStatus = 0;
                        order.PaymentType = paytype;
                        order.ReceiverId = uid;
                        order.Remark = remark;
                        order.ShopDate = DateTime.Now;
                        order.IsSend = issend;
                        order.TradeFees = 0;
                        order.TradeFeesPay = 0;
                        order.UserName = new SOSOshop.BLL.MemberInfo().GetUserName(uid);

                        order.ConsigneeAddress = ua.Address;
                        order.ConsigneeBorough = ua.Borough;
                        order.ConsigneeCity = ua.City;
                        order.ConsigneeConstructionSigns = Request.UserHostAddress;// ua.ConstructionSigns;                        
                        order.ConsigneeEmail = ua.Email;
                        order.ConsigneeFax = "";
                        order.ConsigneeName = ua.Name;
                        order.ConsigneePhone = ua.Mobile;
                        order.ConsigneeProvince = ua.Province;
                        order.ConsigneeRealName = ua.Name;
                        order.ConsigneeTel = ua.Phone;
                        order.ConsigneeZip = ua.Zip;

                        order.BillingCorp = 0;
                        order.BillingCorpName = "";
                        order.BusinessCheckDate = null;
                        order.BusinessmanID = -1;
                        order.BusinessmanName = ConfigurationManager.AppSettings["CompanyFullName"];

                        order.ContractNo = "";
                        order.Editer = 0;
                        order.Fees = 0;
                        order.FinancialCheckDate = null;

                        order.IsBusinessCheck = 0;
                        order.isFinancialReview = 0;
                        order.OgisticsStatus = 0;
                        order.OrderDate = DateTime.Now;
                        order.OrderStatus = 1;
                        order.OrderType = 1;
                        order.OtherFees = 0;

                        foreach (DataRow dr in dt.Rows)
                        {
                            SOSOshop.Model.Order.OrderProduct pro = new SOSOshop.Model.Order.OrderProduct();
                            pro.AddTime = order.ShopDate;
                            pro.OrderId = order.OrderId;
                            pro.pro_pdate = "";
                            pro.pro_pno = "";
                            pro.iden = 0;
                            pro.jigid = "000";
                            pro.ProId = (int)dr["Product_ID"];
                            pro.ProName = dr["name"].ToString();
                            pro.ProNum = decimal.Parse(dr["sl"].ToString());
                            pro.ProPrice = (decimal)dr["price"];
                            pro.Status = (int)Enums.OrderProductStatus.Submit;
                            pro.issplit = false;
                            pro.IsExpirationProduct = Convert.ToInt32(dr["IsExpirationProduct"]);
                            list.Add(pro);
                            //计入订单的商品总价=商品单价x商品数量                        
                            order.TotalPrice += (decimal)(pro.ProPrice * pro.ProNum);
                        }

                        //OTC客户如果收货地址为省外同步订单金额没有满3000则加100块运费
                        if (ua.Province.Trim() != "云南省" && Price.GetMember_Class() == 1 && order.TotalPrice < 3000)
                        {
                            order.TradeFees = 100;
                        }
                        string orderNo = bll.Add(order, list);
                        if (string.IsNullOrEmpty(orderNo))
                        {
                            ViewBag.error = "订单提交失败!";
                        }
                        else
                        {

                            //删除购物车中已经放入订单中的商品 
                            string sql = "delete from [yxs_cart] where id in (" + cartid + ")";
                            try
                            {
                                //Response.Write(sql);
                                // Response.End();
                                db.ExecuteNonQuery(sql);

                                //如果是款到发货，短信通知 订单提交成功，提醒付款
                                //if (pay == 1)
                                //{
                                //    string phone = Convert.ToString(db.ExecuteScalar("SELECT MobilePhone FROM memberaccount WHERE UID=" + uid));
                                //    SOSOshop.BLL.Sms.SendAndSaveDataBase(phone, order.UserName + "，您的订单（单号：" + order.OrderId + "，金额：￥" + order.TotalPrice.ToString("f2") + "）已提交成功，请尽快完成支付，24小时内未到款，订单将自动取消", "系统", phone);
                                //}

                                //如果是在线支付，则进入在线支付页面
                                if (paytype == 1)
                                {
                                    Response.Redirect("/Payment/" + order.OrderId, true);
                                }
                            }
                            catch (Exception ex)
                            {
                                //ViewBag.error = "未知错误!";
                                SOSOshop.BLL.Logs.Log.LogShopAdd(ex.Message + " id:" + cartid + " " + sql, ex.ToString(), BaseController.GetUserId(), User.Identity.Name);
                            }
                            ViewBag.No = orderNo;
                            ViewBag.Money = string.Format("{0:C}", order.TotalPrice + order.TradeFees);
                            ViewBag.GSP = new SOSOshop.BLL.MemberPermission().GetBuyFilingStatus(uid);
                            ViewBag.Order = order;
                            ViewBag.OK = 1;

                            string traderName = null;//交易员姓名
                            string traderPhone = null;//交易员电话
                            DataTable trader = bll.ExecuteTable("select top 1 name, OfficePhone from yxs_administrators where adminid=(select Editer from memberaccount a inner join memberinfo b on a.UID=b.UID where a.UID='" + uid + "')");
                            if (trader != null)
                            {
                                if (trader.Rows.Count == 1)
                                {
                                    traderName = trader.Rows[0]["name"].ToString();
                                }
                            }

                        }

                    }
                    catch (Exception x)
                    {
                        ViewBag.error = "未知错误！请重新提交订单，或联系系统管理员。";
                        SOSOshop.BLL.Logs.Log.LogShopAdd("订单提交出错：" + x.Message, x.ToString(), BaseController.GetUserId(), User.Identity.Name);
                    }
                }
                else
                {
                    ViewBag.error = "请您选择商品后，再来提交订单.";
                }
            }
            else
            {
                ViewBag.error = "请您选择商品后，再来提交订单.";
            }

            return View();
        }



        /// <summary>
        /// 更新购物车中指定商品的数量
        /// </summary>
        /// <returns></returns>
        public string AddShoppinNum()
        {
            string ret = "";
            string sid = Library.Lang.Input.Filter(Request["sid"]);
            string num = Library.Lang.Input.Filter(Request["num"]);
            num = !string.IsNullOrEmpty(num) ? Math.Abs(Convert.ToDecimal(num)).ToString() : "1";
            if (User.Identity.IsAuthenticated)
            {
                if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(num))
                {
                    ret = BaseController.Json(-1, "未知错误，请重新提交数据。");
                }
                else
                {
                    int uid = BaseController.GetUserId();

                    string sql = "update yxs_cart set quantity=" + num + " where uid=" + uid + " and id=" + sid;
                    SOSOshop.BLL.Db db = new Db();
                    int r = db.ExecuteNonQuery(sql);
                    if (r > 0)
                    {
                        ret = BaseController.Json(r, "ok");
                    }
                    else
                    {
                        ret = BaseController.Json(r, "数量更新失败，请重新提交");
                    }
                }
            }
            else
            {
                ret = BaseController.Json(-1, "登录失效，请重新登录后再来操作！");
            }
            return ret;
        }
        /// <summary>
        /// 返回用户地址json格式 
        /// </summary>
        /// <returns></returns>
        public string GetUserAddress()
        {
            int id = 0;
            string ret = BaseController.Json(-1, " 读取失败");
            if (!string.IsNullOrEmpty(Request["aid"]) && User.Identity.IsAuthenticated)
            {
                id = int.Parse(Request["aid"]);
                UserAddressModel ua = new UserAddressModel();
                ua = BaseController.GetUserAddress(id);
                ret = "{\"state\":1,"
                    + "\"id\":" + ua.ID + ","
                    + "\"ua\":\"" + ua.Username + "\","
                    + "\"pr\":\"" + ua.Province + "\","
                    + "\"ci\":\"" + ua.City + "\","
                    + "\"bo\":\"" + ua.Borough + "\","
                    + "\"ad\":\"" + ua.Address + "\","
                    + "\"mo\":\"" + ua.Mobile + "\","
                    + "\"ph\":\"" + ua.Phone + "\","
                    + "\"em\":\"" + ua.Email + "\","
                    + "\"zi\":\"" + ua.Zip + "\""
                    + "}";
            }
            return ret;
        }

        /// <summary>
        /// 取得购物车里商品的件数量
        /// </summary>
        /// <returns></returns>
        public string GetCartNum()
        {
            string ret = "0";
            SOSOshop.BLL.Db db = new Db();
            if (Request.IsAuthenticated)
            {
                try
                {
                    ret = db.ExecuteScalar("select COUNT(0) from yxs_cart a INNER JOIN dbo.product_online_v b ON a.productid=b.Product_ID where uid=" + BaseController.GetUserId()).ToString();
                }
                catch { }
            }
            return BaseController.Json(0, ret);
        }
    }
}
