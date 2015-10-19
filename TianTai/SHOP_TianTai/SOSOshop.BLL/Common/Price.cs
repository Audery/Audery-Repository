using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.Security;
using Microsoft.Practices.EnterpriseLibrary.Data;
namespace SOSOshop.BLL.Common
{
    public static class Price
    {
        /// <summary>
        /// 周三会员日，对于满2000元的OTC客户执行9.8折的会员日价格
        /// </summary>
        public static double Discount = 0.98;
        /// <summary>
        /// 周三会员日，对于满1000元的OTC客户执行9.8折的会员日价格
        /// 达到优惠金额的阀值
        /// </summary>
        public static double DisTotal = 1000;
        /// <summary>
        /// 取得价格显示字符
        /// </summary>
        /// <returns></returns>
        public static string GetPriceShowString()
        {
            //int Member_Class = GetMember_Class();
            //switch (Member_Class)
            //{
            //    case 0: return "现款价";
            //    case 1: return "欠款价";
            //    default: return "商城价";
            //}
            return "商城价";
        }

        /// <summary>
        /// 取商品价格和促销库存
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static PriceTx GetPrice(int productId)
        {
            PriceTx price = new PriceTx();
            SOSOshop.BLL.Product.Product bll = new SOSOshop.BLL.Product.Product();
            DataTable dt = bll.ExecuteTable(string.Format("select price_01,price_02,price_03,cuprice,discount,maxsell,minsell,otcminsell,begindate,enddate from product where product_id={0}", productId));
            foreach (DataRow dr in dt.Rows)
            {
                price.Price_01 = (decimal)dr["price_01"];
                price.Price_02 = (decimal)dr["price_02"];
                price.Price_03 = (decimal)dr["price_03"];
                price.CuPrice = (decimal)dr["cuprice"];
                price.Maxsell = (int)dr["maxsell"];
                price.Minsell = (int)dr["minsell"];
                price.OtcMinsell = (int)dr["otcminsell"];
                price.Discount = Convert.ToSingle(dr["Discount"]);
                price.BeginDate = dr["begindate"] == DBNull.Value ? DateTime.Now.AddYears(10) : (DateTime)dr["begindate"];
                price.EndDate = dr["enddate"] == DBNull.Value ? DateTime.Now.AddYears(-10) : (DateTime)dr["enddate"];
            }
            return price;

        }

        /// <summary>
        /// 商品价格构成
        /// </summary>
        public struct PriceTx
        {
            /// <summary>
            /// 欠款价（商业价）
            /// </summary>
            public decimal Price_01;
            /// <summary>
            /// 超市价（OTC价）（医院用户在没有基药价时使用该价格）
            /// </summary>
            public decimal Price_02;
            /// <summary>
            /// 基药价（医院用户价）
            /// </summary>
            public decimal Price_03;
            /// <summary>
            /// 商品促销价            
            /// </summary>
            public decimal CuPrice;
            /// <summary>
            /// 促销折扣率
            /// </summary>
            public float Discount;
            /// <summary>
            /// 最大可购买数量（促销商品使用）
            /// </summary>
            public int Maxsell;
            /// <summary>
            /// 每天促销库存数
            /// </summary>
            public int OtcMinsell;
            /// <summary>
            /// 每会员每天可购库存数
            /// </summary>
            public int Minsell;
            /// <summary>
            /// 促销开始时间
            /// </summary>
            public DateTime BeginDate;
            /// <summary>
            /// 促销截止时间
            /// </summary>
            public DateTime EndDate;
        }

        /// <summary>
        /// 敏感控销
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        private static bool isDrugSensitive(int productId)
        {
            SOSOshop.BLL.Product.Product bll = new SOSOshop.BLL.Product.Product();
            object sens = bll.ExecuteScalar(string.Format("SELECT isnull( drug_sensitive,0) from dbo.Product where product_id={0}", productId));
            if (sens != null)
            {
                bool b = false;
                bool.TryParse(sens.ToString(), out b);
                return b;
            }
            return false;

        }

        /// <summary>
        /// 根据会员的角色取得会员品种显示的价格
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable GetPriceTable(this DataTable dt)
        {
            if (dt == null) return dt;
            #region 判断是否有必须的列
            if (!dt.Columns.Contains("test001"))
            {
                string[] Columns = SOSOshop.BLL.Product.Product._PriceTableColumns.Trim().TrimEnd(',').Split(',');
                foreach (var item in Columns)
                {
                    if (item == "'' test001" || item.EndsWith("sellType")) continue;
                    if (!dt.Columns.Contains(item.Trim()))
                    {
                        throw new Exception("如果要调用'GetPriceTable' 方法,请拼接常量 'SOSOshop.BLL.Product._PriceTableColumns',并注意不要和常量字段中出现过的字段重复！" + item);
                        throw new Exception("GetPriceTable() 缺少列:" + item);
                    }
                }
            }

            if (!dt.Columns.Contains("sellType"))
            {
                throw new Exception("GetPriceTable() 缺少列:sellType");
            }
            #endregion
            dt.Columns.Add("showPrice", typeof(string));
            dt.Columns.Add("Price", typeof(decimal));
            if (!dt.Columns.Contains("BeginDate")) dt.Columns.Add("BeginDate", typeof(DateTime));
            if (!dt.Columns.Contains("EndDate")) dt.Columns.Add("EndDate", typeof(DateTime));
            if (!dt.Columns.Contains("iscu")) dt.Columns.Add("iscu", typeof(bool));
            if (!dt.Columns.Contains("otcMinSell")) dt.Columns.Add("otcMinSell", typeof(int));
            if (!dt.Columns.Contains("OrigPrice")) dt.Columns.Add("OrigPrice", typeof(decimal));
            //客户类别（0.批发客户,1.OTC拆零客户, -1未知【刚注册的】）
            int Member_Class1 = 0;
            var mp = GetMemberpermission(out Member_Class1);
            int RegisterDate = 0;
            if (mp != null)
            {
                if (mp.IsSpecialTrade)
                {
                    RegisterDate = (int)new SOSOshop.BLL.Db().ExecuteTableForCache("SELECT DATEDIFF(d,isIsSpecialTradeDate,GETDATE())  FROM dbo.memberaccount WHERE UID=" + Public.GetUserId()).Rows[0][0];
                }
            }
            //可拆零
            bool is_cl = dt.Columns.Contains("is_cl");
            SOSOshop.BLL.JTTX.Price bll = new SOSOshop.BLL.JTTX.Price();
            string PriceCategory = Public.GetPriceCategory();
            //用户享有折扣率
            decimal user_discount = SOSOshop.BLL.MemberInfo.GetDiscount(Public.GetUserId());
            foreach (DataRow item in dt.Rows)
            {

                item["showPrice"] = "<a href='/account/logon'>登录可见</a>";
                item["Price"] = 100000;
                item["OrigPrice"] = 100000;

                item["iscu"] = false;
                if (Public.GetUserId() == 0)
                {
                    item["showPrice"] = "<a href='/account/logon'>登录可见</a>";
                    continue;
                }
                if (Public.GetUserId() < 0)
                {
                    item["showPrice"] = "<a href='/account/logon'>会员可见</a>";
                    continue;
                }
                if (is_cl) item["is_cl"] = (int)item["sellType"] < 3 ? "是" : "否";
                if ((int)item["minsell"] == 0) item["minsell"] = 1;
                item["showPrice"] = "<a style=\"cursor:pointer\" onclick='javascript:loginAttention()'>会员可见</a>";
                item["Price"] = 0M;
                if (mp == null)
                {
                    item["showPrice"] = "<a href='/account/logon'>登录可见</a>";
                    item["Price"] = 0;
                    continue;
                }
                if (isDrugSensitive((int)item["Product_ID"]))
                {
                    item["showPrice"] = "<a href='javascript:alert(\"" + Public.GetNetSiteInfo().Phone + "\")'>电话报价</a>";
                    item["Price"] = 0;
                    continue;
                }
                if (!mp.IsBuyFilingStatus && !mp.IsSpecialTrade)
                {
                    item["showPrice"] = "<a href='/MemberCenter/Upgrade'>会员可见</a>";
                    item["Price"] = 0;
                    continue;
                }
                //拥有快捷交易的权限，看权限是否过期
                if (mp.IsSpecialTrade && !mp.IsBuyFilingStatus)
                {
                    if (RegisterDate > 7)
                    {
                        item["showPrice"] = "<a href='/MemberCenter/Upgrade'>会员可见</a>";
                        item["Price"] = 0;
                        continue;
                    }
                }

                if ((bool)item["drug_sensitive"])//敏感购销品种
                {
                    item["showPrice"] = "电话报价";
                    item["Price"] = 0;
                    continue;
                }
                //近效期产品特殊处理
                if (dt.Columns.Contains("IsExpirationProduct"))
                {
                    bool IsExpirationProduct = item["IsExpirationProduct"] != null && item["IsExpirationProduct"] != DBNull.Value && Convert.ToBoolean(item["IsExpirationProduct"]);
                    if (IsExpirationProduct)
                    {
                        int Product_ID = (int)item["Product_ID"];

                        DataTable dt_temp = SOSOshop.BLL.Product.ExpirationTime.CreateInstance().ExecuteTable("SELECT * FROM dbo.Product_ExpirationTime WHERE product_id=" + Product_ID);
                        if (dt_temp != null && dt_temp.Rows.Count > 0)
                        {
                            if (dt.Columns.Contains("showPrice"))
                            {
                                item["showPrice"] = dt_temp.Rows[0]["price"];
                            }
                            if (dt.Columns.Contains("dw"))
                            {
                                item["dw"] = dt_temp.Rows[0]["goods_unit"];
                            }
                            if (dt.Columns.Contains("Price"))
                            {
                                item["Price"] = dt_temp.Rows[0]["price"];
                            }
                            if (dt.Columns.Contains("OrigPrice"))
                            {
                                item["OrigPrice"] = dt_temp.Rows[0]["price"];
                            }
                        }
                        continue;
                    }
                }

                //取出实时的当前库存价格
                PriceTx model = GetPrice((int)item["Product_ID"]);
                var priceCache = bll.GetModel((int)item["Product_ID"]);
                if (priceCache.ContainsKey(PriceCategory))
                {
                    if (priceCache[PriceCategory] > 0)
                    {
                        item["showPrice"] = string.Format("{0:C2}", priceCache[PriceCategory]);
                        item["Price"] = priceCache[PriceCategory];
                        item["OrigPrice"] = priceCache[PriceCategory];
                    }
                    else
                    {
                        item["showPrice"] = "暂无售价";
                        item["Price"] = 0M;
                        item["OrigPrice"] = 0M;
                    }

                }
                else
                {
                    item["showPrice"] = "暂无售价";
                    item["Price"] = 0M;
                    item["OrigPrice"] = 0M;
                }
                //有促销价格执行促销价
                if (model.CuPrice > 0)
                {
                    DateTime ctime = DateTime.Now;
                    int seconds = ctime.Subtract(model.BeginDate).Seconds;
                    if (ctime.Subtract(model.BeginDate).Seconds > 0 && ctime.Subtract(model.EndDate).Seconds < 0)
                    {
                        item["showPrice"] = string.Format("{0:C2}", model.CuPrice);
                        item["Price"] = model.CuPrice;
                        item["Maxsell"] = model.Maxsell;
                        item["Minsell"] = model.Minsell;
                        item["OtcMinsell"] = model.OtcMinsell;
                        item["BeginDate"] = model.BeginDate;
                        item["EndDate"] = model.EndDate;
                        item["iscu"] = true;
                    }
                }
                else if (model.Discount > 0)//执行折扣价
                {
                    DateTime ctime = DateTime.Now;
                    if (ctime.Subtract(model.BeginDate).Seconds > 0 && ctime.Subtract(model.EndDate).Seconds < 0)
                    {
                        item["showPrice"] = string.Format("{0:C2}", Convert.ToDouble(item["Price"]) * model.Discount);
                        item["Price"] = Convert.ToDouble(item["Price"]) * model.Discount;
                        item["Maxsell"] = model.Maxsell;
                        item["Minsell"] = model.Minsell;
                        item["OtcMinsell"] = model.OtcMinsell;
                        item["BeginDate"] = model.BeginDate;
                        item["EndDate"] = model.EndDate;
                        item["iscu"] = true;
                    }
                }
                //用户享有的折扣率
                //float user_discount = 0;
                if (!(bool)item["iscu"] && user_discount > 0)
                {
                    decimal disprice = Convert.ToDecimal(item["Price"]) * user_discount;
                    if (disprice > 0)
                    {
                        item["showPrice"] = string.Format("{0:C2}", disprice);
                        item["Price"] = disprice;
                    }
                }
            }
            return dt;
        }


        /// <summary>
        /// 根据会员的角色取得会员品种显示的价格(都要显示价格)//已作废必须登陆才能显示
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable GetSwitchPriceTable(this DataTable dt)
        {
            return GetPriceTable(dt);
        }

        /// <summary>
        /// 取得会员权限
        /// </summary>
        /// <returns></returns>
        public static SOSOshop.Model.MemberPermission GetMemberpermission()
        {
            if (System.Web.HttpContext.Current.Request.IsAuthenticated)
            {
                return new SOSOshop.BLL.MemberPermission().GetModel(Public.GetUserId());
            }
            return null;
        }
        /// <summary>
        /// 取得会员权限
        /// </summary>
        /// <returns></returns>
        public static SOSOshop.Model.MemberPermission GetMemberpermission(out int Member_Class)
        {
            //客户类别（0.批发客户,1.OTC拆零客户, -1未知【刚注册的】）
            Member_Class = -1;
            if (System.Web.HttpContext.Current.Request.IsAuthenticated)
            {
                int userid = Public.GetUserId();
                SOSOshop.BLL.MemberPermission bll = new SOSOshop.BLL.MemberPermission();
                SOSOshop.Model.MemberPermission model = new SOSOshop.Model.MemberPermission();
                Member_Class = GetMember_Class();
                return bll.GetModel(userid);
            }
            return null;
        }
        /// <summary>
        /// 取得客户类别（0.批发客户,1.OTC拆零客户, -1未知【刚注册的】）
        /// </summary>
        /// <returns></returns>
        public static int GetMember_Class()
        {
            //东昌默认只有批发客户
            return 0;
            //客户类别（0.批发客户,1.OTC拆零客户, -1未知【刚注册的】）
            int Member_Class = -1;
            if (System.Web.HttpContext.Current.Request.IsAuthenticated)
            {
                int userid = Public.GetUserId();
                SOSOshop.BLL.MemberPermission bll = new SOSOshop.BLL.MemberPermission();
                object obj = bll.ExecuteScalarForCache("SELECT Member_Class FROM dbo.memberinfo WHERE UID=" + userid, DateTime.Now.AddMinutes(5));
                if (Library.Lang.DataValidator.isNumber(obj))
                {
                    Member_Class = (int)obj;
                }
            }
            return Member_Class;
        }

    }
}
