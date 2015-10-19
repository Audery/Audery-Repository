using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using SOSOshop.BLL;
using SOSOshop.BLL.Common;

namespace _101shop.admin.v3.admin.cuxiao
{
    public partial class Cuxiao : System.Web.UI.Page
    {
        public bool isSeeAll = false;
        public bool isSeeMe = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            isSeeAll = SOSOshop.BLL.PowerPass.isPass("001030000");//查看所有
            if (!isSeeAll) { SOSOshop.BLL.PromptInfo.Popedom("001030001"); isSeeMe = true; }//查看采购员自己的
            SOSOshop.Model.AdminInfo aInfo = SOSOshop.BLL.AdministrorManager.Get();

            System.Text.StringBuilder s = new System.Text.StringBuilder();

            #region 查询条件
            s.Append(" and beactive<>'删' ");
            //商品名称
            string shopname = Request["shopname"];
            if (!string.IsNullOrEmpty(shopname))
            {
                s.Append(" and a.Product_Name like '%" + filter(shopname).Trim() + "%'");
            }
            //生产厂家
            string changjia = Request["changjia"];
            if (!string.IsNullOrEmpty(changjia))
            {
                s.Append(" and a.DrugsBase_Manufacturer like '%" + filter(changjia).Trim() + "%'");
            }

            string pihao = Request["pihao"];
            if (!string.IsNullOrEmpty(pihao))
            {
                s.AppendFormat(" and a.DrugsBase_ApprovalNumber like '%{0}%'", pihao);
            }

            string Price = Request["Price"];
            if (!string.IsNullOrEmpty(Price) && Price != "0")
            {
                if (Price == "1") s.Append(" and a.Product_ID in(select Product_ID from product_online_v_1 as b where Price_01 > 0)");
                if (Price == "2") s.Append(" and a.Product_ID in(select Product_ID from product_online_v_1 as b where Price_02 > 0)");
                if (Price == "3") s.Append(" and a.Product_ID in(select Product_ID from product_online_v_1 as b where Price_02 > 0 and Price_01 > 0)");
                if (Price == "4") s.Append(" and a.Product_ID not in(select Product_ID from product_online_v_1 as b where Price_02 >0 or Price_01 > 0)");
                if (Price == "5") s.Append(" and a.Product_ID not in(SELECT Product_ID FROM dbo.product_online_v_1 WHERE Price_02>0)");
                if (Price == "6") s.Append(" and a.Product_ID not in(SELECT Product_ID FROM dbo.product_online_v_1 WHERE Price_01>0)");
            }
            string bGoodsImage = Request["bGoodsImage"];

            if (!string.IsNullOrEmpty(bGoodsImage) && bGoodsImage != "0")
            {
                switch (bGoodsImage)
                {
                    case "1":
                        {
                            s.Append(" and a.Goods_ID in (SELECT Goods_ID FROM dbo.Goods_Image)");
                            break;
                        }
                    case "2":
                        {
                            s.Append(" and a.Goods_ID not in (SELECT Goods_ID FROM dbo.Goods_Image)");
                            break;
                        }
                }
            }

            string cuxiao = Request["cuxiao"];

            if (!string.IsNullOrEmpty(cuxiao) && cuxiao != "0")
            {
                switch (cuxiao)
                {
                    case "1":
                        {
                            s.Append(" and (a.CuPrice>0 or a.Discount>0) and a.Discount<>1");
                            break;
                        }
                    case "2":
                        {
                            s.Append(" and a.CuPrice=0 and (a.discount=0 or a.discount=1) ");
                            break;
                        }
                }
            }

            //string bMaid = Request["bMaid"];
            //if (!string.IsNullOrEmpty(bMaid) && bMaid != "0")
            //{
            //    switch (bMaid)
            //    {
            //        case "1":
            //            {
            //                s.Append(" and a.spid in (SELECT spid FROM dbo.spzl WHERE maid<>'')");
            //                break;
            //            }
            //        case "2":
            //            {
            //                s.Append(" and a.spid in (SELECT spid FROM dbo.spzl WHERE maid='')");
            //                break;
            //            }
            //        case "3":
            //            {
            //                s.Append(" and a.spid in (SELECT spid FROM dbo.spzl WHERE maid1<>'')");
            //                break;
            //            }
            //        case "4":
            //            {
            //                s.Append(" and a.spid in (SELECT spid FROM dbo.spzl WHERE maid1='')");
            //                break;
            //            }
            //    }
            //}
            //string bGgy = Request["bGgy"];
            //if (!string.IsNullOrEmpty(bGgy) && bGgy != "0")
            //{
            //    switch (bGgy)
            //    {
            //        case "1":
            //            {
            //                s.Append(" and a.spid in (SELECT spid FROM dbo.spzl WHERE ggy<>'')");
            //                break;
            //            }
            //        case "2":
            //            {
            //                s.Append(" and a.spid in (SELECT spid FROM dbo.spzl WHERE ggy='')");
            //                break;
            //            }
            //        case "3":
            //            {
            //                s.Append(" and a.spid in (SELECT spid FROM dbo.spzl WHERE ggy1<>'')");
            //                break;
            //            }
            //        case "4":
            //            {
            //                s.Append(" and a.spid in (SELECT spid FROM dbo.spzl WHERE ggy1='')");
            //                break;
            //            }
            //    }
            //}
            string bBq1 = Request["bBq1"];
            if (!string.IsNullOrEmpty(bBq1) && bBq1 != "0")
            {
                switch (bBq1)
                {
                    case "1"://基药标签有
                        {
                            s.Append(" and exists(select top(1) tp1.product_id from [Drugsbase].[dbo].Tag_PharmProduct tp1 INNER JOIN [Drugsbase].[dbo].Tag_PharmAttribute tpa1 ON tp1.Tag_PharmAttribute_id=tpa1.id WHERE tpa1.tag_id=69 and tp1.product_id=a.DrugsBase_ID)");
                            break;
                        }
                    case "2"://基药标签无
                        {
                            s.Append(" and not exists(select top(1) tp1.product_id from [Drugsbase].[dbo].Tag_PharmProduct tp1 INNER JOIN [Drugsbase].[dbo].Tag_PharmAttribute tpa1 ON tp1.Tag_PharmAttribute_id=tpa1.id WHERE tpa1.tag_id=69 and tp1.product_id=a.DrugsBase_ID)");
                            break;
                        }
                }
            }
            string bBq2 = Request["bBq2"];
            if (!string.IsNullOrEmpty(bBq2) && bBq2 != "0")
            {
                switch (bBq2)
                {
                    case "1"://OTC标签有
                        {
                            s.Append(" and (exists(select top(1) tp2.product_id from [Drugsbase].[dbo].Tag_PharmProduct tp2 INNER JOIN [Drugsbase].[dbo].Tag_PharmAttribute tpa2 ON tp2.Tag_PharmAttribute_id=tpa2.id WHERE tpa2.tag_id=71 and tp2.product_id=a.DrugsBase_ID)");
                            s.Append(" )");
                            break;
                        }
                    case "2"://OTC标签无
                        {
                            s.Append(" and (not exists(select top(1) tp2.product_id from [Drugsbase].[dbo].Tag_PharmProduct tp2 INNER JOIN [Drugsbase].[dbo].Tag_PharmAttribute tpa2 ON tp2.Tag_PharmAttribute_id=tpa2.id WHERE tpa2.tag_id=71 and tp2.product_id=a.DrugsBase_ID)");
                            s.Append(" )");
                            break;
                        }
                }
            }
            string bBq3 = Request["bBq3"];
            if (!string.IsNullOrEmpty(bBq3) && bBq3 != "0")
            {
                switch (bBq3)
                {
                    case "1"://520标签有
                        {
                            s.Append(" and exists(select top(1) product_id from [Drugsbase].[dbo].[Tag_Product] tp3 where tp3.tag_id=66 and tp3.product_id=a.DrugsBase_ID)");
                            break;
                        }
                    case "2"://520标签无
                        {
                            s.Append(" and not exists(select top(1) product_id from [Drugsbase].[dbo].[Tag_Product] tp3 where tp3.tag_id=66 and tp3.product_id=a.DrugsBase_ID)");
                            break;
                        }
                }
            }
            string bQtzs = Request["bQtzs"];
            if (!string.IsNullOrEmpty(bQtzs) && bQtzs != "0")
            {
                switch (bQtzs)
                {
                    case "1":
                        {
                            s.Append(" and Product_ID in(select Product_ID from product_online_v) ");
                            break;
                        }
                    case "2":
                        {
                            s.Append(" and Product_ID not in(select Product_ID from product_online_v) ");
                            break;
                        }
                }
            }
            string bJyQtzs = Request["bJyQtzs"];
            if (!string.IsNullOrEmpty(bJyQtzs) && bJyQtzs != "0")
            {
                switch (bJyQtzs)
                {
                    case "1":
                        {
                            s.Append(" and a.product_id in (SELECT c.product_id FROM [Tag_PharmAttribute] as a inner join dbo.Tag_PharmProduct as b on b.Tag_PharmAttribute_id = a.id and a.tag_id=69 and a.name!='麻醉药' and a.name!='治疗精神障碍药' and b.product_key='d' inner join product_online_v as c on c.DrugsBase_ID=b.product_id");
                            s.Append(" where c.Price_01>0 AND (exists(select top(1) goods_id from [exchange].[dbo].[LinkRegionBidPricing] a where a.goods_id=c.goods_id) or exists(select top(1) goods_id from [exchange].[dbo].[LinkRegionLimitPricing] a where a.goods_id=c.goods_id)) ");
                            s.Append(")");
                            break;
                        }
                    case "2":
                        {
                            s.Append(" and a.product_id not in (SELECT c.product_id FROM [Tag_PharmAttribute] as a inner join dbo.Tag_PharmProduct as b on b.Tag_PharmAttribute_id = a.id and a.tag_id=69 and a.name!='麻醉药' and a.name!='治疗精神障碍药' and b.product_key='d' inner join product_online_v as c on c.DrugsBase_ID=b.product_id");
                            s.Append(" where c.Price_01>0 AND (exists(select top(1) goods_id from [exchange].[dbo].[LinkRegionBidPricing] a where a.goods_id=c.goods_id) or exists(select top(1) goods_id from [exchange].[dbo].[LinkRegionLimitPricing] a where a.goods_id=c.goods_id)) ");
                            s.Append(")");
                            break;
                        }
                }
            }
            string bYsJg = Request["bYsJg"];
            if (!string.IsNullOrEmpty(bYsJg) && bYsJg != "0")
            {
                switch (bYsJg)
                {
                    case "1":
                        {
                            s.Append(" and a.product_id in (SELECT product_id FROM dbo.product c WHERE (exists(select top(1) goods_id from [exchange].[dbo].[LinkRegionBidPricing] a where a.goods_id=c.goods_id) or exists(select top(1) goods_id from [exchange].[dbo].[LinkRegionLimitPricing] a where a.goods_id=c.goods_id)))");
                            break;
                        }
                    case "2":
                        {
                            s.Append(" and a.product_id not in (SELECT product_id FROM dbo.product c WHERE (exists(select top(1) goods_id from [exchange].[dbo].[LinkRegionBidPricing] a where a.goods_id=c.goods_id) or exists(select top(1) goods_id from [exchange].[dbo].[LinkRegionLimitPricing] a where a.goods_id=c.goods_id)))");
                            break;
                        }
                }
            }
            string bYsFl = Request["bYsFl"];
            if (!string.IsNullOrEmpty(bYsFl) && bYsFl != "0")
            {
                switch (bYsFl)
                {
                    case "1":
                        {
                            s.Append(" and a.product_id in (SELECT c.product_id FROM [Tag_PharmAttribute] as a inner join dbo.Tag_PharmProduct as b on b.Tag_PharmAttribute_id = a.id and a.tag_id=69 and b.product_key='d' inner join product as c on c.DrugsBase_ID=b.product_id)");
                            break;
                        }
                    case "2":
                        {
                            s.Append(" and a.product_id not in (SELECT c.product_id FROM [Tag_PharmAttribute] as a inner join dbo.Tag_PharmProduct as b on b.Tag_PharmAttribute_id = a.id and a.tag_id=69 and b.product_key='d' inner join product as c on c.DrugsBase_ID=b.product_id)");
                            break;
                        }
                }
            }

            string bStock = Request["bStock"];
            if (!string.IsNullOrEmpty(bStock) && bStock != "0")
            {
                switch (bStock)
                {
                    case "1":
                        {
                            s.Append(" and a.Product_ID in (select Product_ID from product_online_v_1 as b where b.Stock> 0)");
                            break;
                        }
                    case "2":
                        {
                            s.Append(" and a.Product_ID not in (select Product_ID from product_online_v_1 as b where b.Stock>0)");
                            break;
                        }
                }
            }
            //string is_cl = Request["is_cl"];
            //if (!string.IsNullOrEmpty(is_cl) && is_cl != "0")
            //{
            //    if (is_cl == "1") s.Append(" and a.Product_ID in(select Product_ID from product_online_v_1 as b where is_cl = '是')");
            //    if (is_cl == "2") s.Append(" and a.Product_ID in(select Product_ID from product_online_v_1 as b where is_cl <> '是')");
            //}

            //销售方式
            string sellType = Request["sellType"];
            if (!string.IsNullOrEmpty(sellType) && sellType != "0")
            {
                s.Append(" and sellType=" + sellType);
            }
            //e商
            //SelectES();
            //string ess = Request["es"];
            //if (!string.IsNullOrEmpty(ess) && ess != "0")
            //{
            //    s.Append(" and spid in (select spid from spzl where sx1='" + ess + "') ");
            //}

            string bShelves = Request["bShelves"];
            if (!string.IsNullOrEmpty(bShelves) && bShelves != "0")
            {
                if (bShelves == "1") s.Append(" and a.Product_ID in(select Product_ID from product_online_v)");//上架
                if (bShelves == "2") s.Append(" and a.Product_bShelves=0");//下架
                if (bShelves == "3") s.Append(" and a.Product_ID not in(select Product_ID from product_online_v) and a.Product_bShelves=1 ");//待上架
                if (bShelves == "4") s.Append(" and a.Product_bShelves=-1");//暂不上架
            }

            ////查询采购员
            //SelectCgyEditer();
            //string cgy = Request["cgy"];
            //if (isSeeMe)//查看采购员自己的
            //{
            //    s.AppendFormat(" and a.spid in (SELECT p1.spid FROM dbo.spzl p1 inner join dbo.spzl_jg p2 on p1.spid=p2.spid AND p1.sx1=p2.jigid WHERE p2.jigid<>'002' AND p2.jigid<>'003' AND p2.cgy='{0}')", aInfo.AdminName);
            //}
            //else if (!string.IsNullOrEmpty(cgy) && cgy != "0")
            //{
            //    if (cgy == "-1")
            //    {
            //        s.AppendFormat(" and a.spid in (SELECT p1.spid FROM dbo.spzl p1 inner join dbo.spzl_jg p2 on p1.spid=p2.spid AND p1.sx1=p2.jigid WHERE p2.jigid<>'002' AND p2.jigid<>'003' AND p2.cgy='')");
            //    }
            //    else
            //    {
            //        s.AppendFormat(" and a.spid in (SELECT p1.spid FROM dbo.spzl p1 inner join dbo.spzl_jg p2 on p1.spid=p2.spid AND p1.sx1=p2.jigid WHERE p2.jigid<>'002' AND p2.jigid<>'003' AND p2.cgy='{0}')", cgy);
            //    }
            //}

            //说明书
            string bSms = Request["bSms"];
            if (!string.IsNullOrEmpty(bSms) && bSms != "0")
            {
                switch (bSms)
                {
                    case "1":
                        {
                            s.Append(" and exists(SELECT top(1) * FROM dbo.DrugsBase d INNER JOIN dbo.Drugsbase_Direct t ON d.Drugsbase_Direct_ID=t.Drugsbase_Direct_ID where d.DrugsBase_ID=a.DrugsBase_ID AND cast(t.Drugsbase_Direct_Context as varchar)<>'')");
                            break;
                        }
                    case "2":
                        {
                            s.Append(" and not exists(SELECT top(1) * FROM dbo.DrugsBase d INNER JOIN dbo.Drugsbase_Direct t ON d.Drugsbase_Direct_ID=t.Drugsbase_Direct_ID where d.DrugsBase_ID=a.DrugsBase_ID AND cast(t.Drugsbase_Direct_Context as varchar)<>'')");
                            break;
                        }
                }
            }
            //药理药效
            string bYlyx = Request["bYlyx"];
            if (!string.IsNullOrEmpty(bYlyx) && bYlyx != "0")
            {
                switch (bYlyx)
                {
                    case "1":
                        {
                            s.Append(" and exists(SELECT top(1) * FROM dbo.DrugsBase d1 INNER JOIN dbo.DrugsBase_PharmMediNameLink t1 ON d1.DrugsBase_ID=t1.DrugsBase_ID where d1.DrugsBase_ID=a.DrugsBase_ID)");
                            break;
                        }
                    case "2":
                        {
                            s.Append(" and not exists(SELECT top(1) * FROM dbo.DrugsBase d1 INNER JOIN dbo.DrugsBase_PharmMediNameLink t1 ON d1.DrugsBase_ID=t1.DrugsBase_ID where d1.DrugsBase_ID=a.DrugsBase_ID)");
                            break;
                        }
                }
            }
            //供应商：
            //string bgys = Request["bgys"];
            //switch (bgys)
            //{

            //    case "0":
            //        {
            //            break;
            //        }
            //    case "1":
            //        {
            //            s.Append(" and a.Product_ID not in(SELECT product_id FROM dbo.Product_Centre)");
            //            break;
            //        }
            //    default:
            //        {
            //            if (!string.IsNullOrEmpty(bgys))
            //            {
            //                s.Append(" and a.Product_ID in(SELECT product_id FROM dbo.Product_Centre WHERE iden=" + bgys + ")");
            //            }
            //            break;
            //        }
            //}


            //是否控销
            string bKong = Request["bKong"];
            if (bKong == "1")
            {
                s.Append(" and Product_ID IN (SELECT Product_ID FROM dbo.product_online_v WHERE Price_03>0)");
            }
            else if (bKong == "2")
            {
                s.Append(" and  Product_ID not IN (SELECT Product_ID FROM dbo.product_online_v WHERE Price_03>0)");
            }

            string bjgqj_s = Request["bjgqj_s"];
            string bjgqj_e = Request["bjgqj_e"];
            if (Request["bjgqj"] == "1")
            {
                if (!string.IsNullOrEmpty(bjgqj_s))
                {
                    s.AppendFormat("and Product_ID IN (SELECT Product_ID FROM dbo.product_online_v_2 WHERE  Price_01>={0} )", bjgqj_s);
                }
                if (!string.IsNullOrEmpty(bjgqj_e))
                {
                    s.AppendFormat("and Product_ID IN(SELECT Product_ID FROM dbo.product_online_v_2 WHERE Price_01<={0})", bjgqj_e);
                }
            }
            else if (Request["bjgqj"] == "2")
            {
                if (!string.IsNullOrEmpty(bjgqj_s))
                {
                    s.AppendFormat("and Product_ID IN (SELECT Product_ID FROM dbo.product_online_v_2 WHERE  Price_02>={0} )", bjgqj_s);
                }
                if (!string.IsNullOrEmpty(bjgqj_e))
                {
                    s.AppendFormat("and Product_ID IN(SELECT Product_ID FROM dbo.product_online_v_2 WHERE Price_02<={0})", bjgqj_e);
                }
            }
            else if (Request["bjgqj"] == "3")
            {
                if (!string.IsNullOrEmpty(bjgqj_s))
                {
                    s.AppendFormat("and Product_ID IN (SELECT Product_ID FROM dbo.product_online_v_2 WHERE  Price_03>={0} )", bjgqj_s);
                }
                if (!string.IsNullOrEmpty(bjgqj_e))
                {
                    s.AppendFormat("and Product_ID IN(SELECT Product_ID FROM dbo.product_online_v_2 WHERE Price_03<={0})", bjgqj_e);
                }
            }
            else if (Request["bjgqj"] == "4")
            {
                if (!string.IsNullOrEmpty(bjgqj_s))
                {
                    s.AppendFormat("and Product_ID IN (SELECT Product_ID FROM dbo.product_online_v_2 WHERE  CuPrice>={0} )", bjgqj_s);
                }
                if (!string.IsNullOrEmpty(bjgqj_e))
                {
                    s.AppendFormat("and Product_ID IN(SELECT Product_ID FROM dbo.product_online_v_2 WHERE CuPrice<={0})", bjgqj_e);
                }
            }
            else if (Request["bjgqj"] == "5")
            {
                if (!string.IsNullOrEmpty(bjgqj_s))
                {
                    s.AppendFormat("and Product_ID IN (SELECT Product_ID FROM dbo.product_online_v_2 WHERE discount>0 and discount<>1 and discount>={0} )", bjgqj_s);
                }
                if (!string.IsNullOrEmpty(bjgqj_e))
                {
                    s.AppendFormat("and Product_ID IN(SELECT Product_ID FROM dbo.product_online_v_2 WHERE discount<={0})", bjgqj_e);
                }
            }

            string bStock_s = Request["bStock_s"];
            string bStock_e = Request["bStock_e"];
            if (!string.IsNullOrEmpty(bStock_s))
            {
                s.AppendFormat("and a.Stock>={0} ", bStock_s);
            }
            if (!string.IsNullOrEmpty(bStock_e))
            {
                s.AppendFormat("and a.Stock<={0} ", bStock_e);
            }

            #endregion


            if (s.Length > 1)
            {
                where = s.ToString();
            }
            else
            {
                where = "";
            }

        }


        private string filter(string str)
        {
            return str + " ".Replace("'", "").Replace("\"", "");
        }

        private string where { get; set; }
        private string param { get; set; }

        private string getFrom()
        {
            
            return " from product as a "; 


        }

        public string show_list()
        {
            pagesize = 16;
            int pi = string.IsNullOrEmpty(Request.QueryString["current"]) ? 1 : int.Parse(Request.QueryString["current"]);
            pageindex = pi;
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();
            db.ChangeShop();

            #region 数据查询
            string sql = string.Format("select top {0} *," +
                "ISNULL((SELECT Product_ID FROM product_online_v WHERE Product_ID=a.Product_ID),0)st," +//是否上架(在前台显示)
                "(select b.Price_01 from product_online_v_1 as b where b.Product_ID= a.Product_ID) as price_01," +
                "(select b.Price_02 from product_online_v_1 as b where b.Product_ID= a.Product_ID) as price_02," +
                "(select stock from product_online_v_1 as c where c.Product_ID=a.Product_ID) as stock," +
                "(select isnull(stock,0) from Stock_Lock as c where c.Product_ID=a.Product_ID) as stock1" +
                "{1} WHERE (a.Product_ID NOT IN(SELECT TOP ({0} * ({2} - 1)) a.Product_ID {1} {3} order by a.Product_ID desc)) {4} order by a.Product_ID desc", pagesize, getFrom(), pageindex, where.Length > 1 ? " where 1=1 " + where : "", where);


            SOSOshop.BLL.MongoHelper<MongoDB.Bson.BsonDocument> dbM = new SOSOshop.BLL.MongoHelper<MongoDB.Bson.BsonDocument>("Config");
            dbM.ChangeDB("datasynchronism");
            var cli = dbM._mongoCollection.FindAll();
            DataTable dt = db.ExecuteTable(sql);
            #endregion

            //生成HTML
            s = GetHtml(dt);
            return s.ToString();

        }

        /// <summary>
        /// 返回设定的表格内容
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        private string table(object field)
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.Append("<td>");
            s.Append(field.ToString());
            s.Append("</td>");
            return s.ToString();
        }


        /// <summary>
        /// 生成HTML
        /// </summary>
        /// <param name="s"></param>
        /// <param name="dt"></param>
        private StringBuilder GetHtml(DataTable dt)
        {
            SOSOshop.BLL.Db db = new Db();
            db.ChangeDataCenter();
            StringBuilder s = new StringBuilder();
            List<string> Colums = new List<string>() { "编号", "商品名称", "规格", "生产厂家", "促销价", "折扣率", "促销总数", "每天可促数", "每会员可购", "促销开始时间", 
                "促销截止时间", "库存", "状态", "设置促销"  };
            List<string> BaseColums = new List<string>();
            BaseColums.AddRange(Colums);

            #region 先循环生成列头，否则会出现先有2种价格，后面又是3种价格，错位的情况
            //查询出所有列
            DataTable PriceCategory = db.ExecuteTable(string.Format("SELECT category FROM dbo.Price GROUP BY category"));
            foreach (DataRow item in PriceCategory.Rows)
            {
                if (!Colums.Contains(item["category"].ToString()))
                {
                    Colums.Insert(4, item["category"].ToString());

                }

            }
            //添加列头
            var TempStr = GetHtmlHeader(Colums);
            #endregion

            foreach (DataRow item in dt.Rows)
            {
                #region 循环生成每一行
                s.Append("<tr>");
                s.Append(table(item["Product_ID"]));
                s.Append(table("<a title='" + item["created"] + "'>" + item["Product_Name"] + "</a>"));
                var Specification = Public.GetSpecificationAndS(item);
                s.Append("<td title='规格：" + Specification + " 剂型：" + item["DrugsBase_Formulation"] + "'><div  class='textOverFlow'>" + Specification + "<div></td>");//+ 
                s.Append(table(item["DrugsBase_Manufacturer"]));

                //动态添加价格类型
                string ID = item["spid"].ToString();
                foreach (var temp in Colums.Except(BaseColums))
                {
                    DataTable PriceTable = db.ExecuteTable(string.Format("SELECT * FROM dbo.Price WHERE ID='{0}'", ID));
                    if (PriceTable != null && PriceTable.Rows.Count > 0)
                    {


                        var DrList = PriceTable.Select(string.Format("category='{0}'", temp));
                        decimal tempprice = 0;
                        if (DrList != null && DrList.Count() > 0)
                        {
                            tempprice = DrList.First()["Price_N"] == null ? 0 : Convert.ToDecimal(DrList.First()["Price_N"]);
                        }
                        s.Append(table(string.Format("{0:f2}", tempprice)));

                    }
                    else
                    {
                        s.Append(table(string.Format("{0:f2}", 0)));
                    }
                }

                s.Append(table(string.Format("<span id='cu{0}'>{1:f2}</span>", item["Product_ID"], item["CuPrice"])));
                s.Append(table(string.Format("<span id='dis{0}'>{1:f2}</span>", item["Product_ID"], item["Discount"])));
                s.Append(table(string.Format("<span id='max{0}'>{1}</span>", item["Product_ID"], item["maxsell"])));
                s.Append(table(string.Format("<span id='otcmin{0}'>{1}</span>", item["Product_ID"], item["otcminsell"])));
                s.Append(table(string.Format("<span id='min{0}'>{1}</span>", item["Product_ID"], item["minsell"])));
                s.Append(table(string.Format("<span id='b{0}'>{1}</span>", item["Product_ID"], item["BeginDate"].ToString().Replace(" ", "<br>"))));
                s.Append(table(string.Format("<span id='e{0}'>{1}</span>", item["Product_ID"], item["EndDate"].ToString().Replace(" ", "<br>"))));
                if (!Library.Lang.DataValidator.isNumber(item["stock"]))
                {
                    item["stock"] = 0;
                }
                if (!Library.Lang.DataValidator.isNumber(item["stock1"]))
                {
                    item["stock1"] = 0;
                }
                s.Append(table(string.Format("<a title='库存:{1},锁库:{2}'>{0}</a>", int.Parse(item["stock"].ToString()) - int.Parse(item["stock1"].ToString()), item["stock"], item["stock1"])));


                string disabled = "";
                if ((int)item["st"] == 0)
                {
                    if ((int)item["Product_bShelves"] == 0)
                    {
                        s.Append(table("下架"));
                    }
                    else
                    {
                        if ((int)item["Product_bShelves"] == -1)
                        {
                            s.Append(table("暂不上架"));
                        }
                        else
                        {
                            s.Append(table("待上架"));
                            disabled = "disabled";
                        }
                    }
                }
                else
                {
                    s.Append(table("上架"));
                }
                s.Append(table(string.Format("<a href='javascript:putad(" + item["product_id"] + ")'>设置</a> {0}", item["enddate"] == DBNull.Value ? "" : "<a href='javascript:cancelcu(" + item["product_id"] + ")'>取消</a>")));
                s.Append("</tr>");
                #endregion
            }
            return TempStr.Append(s);
        }

        /// <summary>
        /// 生成html头
        /// </summary>
        /// <param name="Colums"></param>
        /// <returns></returns>
        private StringBuilder GetHtmlHeader(List<string> Colums)
        {
            StringBuilder Sb = new StringBuilder();
            Sb.Append("<tbody>");
            Sb.Append("<tr style=\"background-color: rgba(0, 0, 0, 0);\">");
            foreach (string Colum in Colums)
            {
                Sb.AppendFormat("<th scope=\"col\" style=\"width: 55px\">{0}</th>", Colum);
            }
            Sb.Append("</tr>");
            Sb.Append("</tbody>");
            return Sb;
        }


        /// <summary>
        /// 商品停用启用操作
        /// </summary>
        /// <param name="stop"></param>
        /// <returns></returns>
        private string bstop(int stop, string id)
        {
            string s = "";
            if (stop == 1)
            {
                s = "停用";
            }
            else
            {
                s = "启用";
            }
            return "<a href='javascript:void(0)' onclick='istop(" + (stop == 0 ? 1 : 0) + "," + id + ")'>" + s + "</a>";
        }

        /// <summary>
        /// 商品上下架操作
        /// </summary>
        /// <param name="stop"></param>
        /// <returns></returns>
        private string bShelves(int stop, string id, string disabled = "")
        {
            if (disabled == "disabled")
            {
                return "<span style='color:#BAC9C6'>上架<span>";
            }
            string s = "";
            //stop==1是下架的状态，在客户端显示为（上架）便于指示操作员操作，
            if (stop == 1)
            {
                s = "下架";
            }
            else
            {
                s = "上架";
            }
            return "<a href='javascript:void(0)' " + disabled + " onclick='ishelves(" + (stop == 1 ? 0 : 1) + "," + id + ")'>" + s + "</a>";
        }

        public string page()
        {
            param = "&shopname=" + Request["shopname"];
            param += "&changjia=" + Request["changjia"];
            param += "&pihao=" + Request["pihao"];
            param += "&Price=" + Request["Price"];
            param += "&is_cl=" + Request["is_cl"];
            param += "&sellType=" + Request["sellType"];
            param += "&es=" + Request["es"];
            param += "&bStock=" + Request["bStock"];
            param += "&bShelves=" + Request["bShelves"];
            param += "&bGoodsImage=" + Request["bGoodsImage"];
            param += "&bMaid=" + Request["bMaid"];
            param += "&bGgy=" + Request["bGgy"];
            param += "&bQtzs=" + Request["bQtzs"];
            param += "&bJyQtzs=" + Request["bJyQtzs"];
            param += "&bYsJg=" + Request["bYsJg"];
            param += "&bYsFl=" + Request["bYsFl"];
            param += "&cgy=" + Request["cgy"];
            param += "&bKong=" + Request["bKong"];
            param += "&cuxiao=" + Request["cuxiao"];
            param += "&bSms=" + Request["bSms"] + "&bYlyx=" + Request["bYlyx"] +
                    "&bBq1=" + Request["bBq1"] + "&bBq2=" + Request["bBq2"] + "&bBq3=" + Request["bBq3"] + "&bgys=" + Request["bgys"]
                   + "&bjgqj=" + Request["bjgqj"]
                    + "&bjgqj_s=" + Request["bjgqj_s"]
                    + "&bjgqj_e=" + Request["bjgqj_e"]
                    + "&bStock_s=" + Request["bStock_s"]
                    + "&bStock_e=" + Request["bStock_e"];
            SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();
            string sql = "select count(a.product_name) as c" + getFrom() + (where.Length > 1 ? " where 1=1 " + where + "" : "");
            //Response.Write(sql);
            int recordcount = (int)db.ExecuteTable(sql).Rows[0]["c"];
            double cs = (double)recordcount / pagesize;
            //页总数
            pagecount = int.Parse(Math.Ceiling(cs).ToString());
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.Append("共<span style='color: Red'>" + recordcount + "</span>条记录");
            s.Append("<a href=\"?current=1" + param + "\">");
            s.Append("<<");
            s.Append("</a> ");
            int j, i;
            j = i = 0;
            if (pageindex > 5)
            {
                i = pageindex - 5;
                j = i;
            }
            for (; i < j + 9 && i < pagecount; i++)
            {
                s.Append("<a href=\"?current=" + (i + 1) + param +
                    (pageindex == i + 1 ? "\" style=\"color:Red" : "")
                    + "\">");
                s.Append(i + 1);
                s.Append("</a> ");
            }
            s.Append("<a href=\"?current=" + pagecount + param + "\">");
            s.Append(">>");
            s.Append("</a> ");

            return s.ToString();
        }

        public int pagesize { get; set; }
        public int pageindex { get; set; }
        public int pagecount { get; set; }
    }
}