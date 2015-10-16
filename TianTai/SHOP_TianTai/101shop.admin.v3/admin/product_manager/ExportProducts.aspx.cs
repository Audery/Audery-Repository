using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SOSOshop.BLL.Common;

namespace _101shop.admin.v3.admin.product_manager
{
    public partial class ExportProducts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SOSOshop.BLL.PromptInfo.Popedom("001009006");

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
            string bMaid = Request["bMaid"];
            if (!string.IsNullOrEmpty(bMaid) && bMaid != "0")
            {
                switch (bMaid)
                {
                    case "1":
                        {
                            s.Append(" and a.spid in (SELECT spid FROM dbo.spzl WHERE maid<>'')");
                            break;
                        }
                    case "2":
                        {
                            s.Append(" and a.spid in (SELECT spid FROM dbo.spzl WHERE maid='')");
                            break;
                        }
                    case "3":
                        {
                            s.Append(" and a.spid in (SELECT spid FROM dbo.spzl WHERE maid1<>'')");
                            break;
                        }
                    case "4":
                        {
                            s.Append(" and a.spid in (SELECT spid FROM dbo.spzl WHERE maid1='')");
                            break;
                        }
                }
            }
            string bGgy = Request["bGgy"];
            if (!string.IsNullOrEmpty(bGgy) && bGgy != "0")
            {
                switch (bGgy)
                {
                    case "1":
                        {
                            s.Append(" and a.spid in (SELECT spid FROM dbo.spzl WHERE ggy<>'')");
                            break;
                        }
                    case "2":
                        {
                            s.Append(" and a.spid in (SELECT spid FROM dbo.spzl WHERE ggy='')");
                            break;
                        }
                    case "3":
                        {
                            s.Append(" and a.spid in (SELECT spid FROM dbo.spzl WHERE ggy1<>'')");
                            break;
                        }
                    case "4":
                        {
                            s.Append(" and a.spid in (SELECT spid FROM dbo.spzl WHERE ggy1='')");
                            break;
                        }
                }
            }
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
                            s.Append(" and exists(select top(1) spid from dbo.spzl AS sp2 where sp2.spid=a.spid and sp2.is_cl = '是'))");
                            break;
                        }
                    case "2"://OTC标签无
                        {
                            s.Append(" and (not exists(select top(1) tp2.product_id from [Drugsbase].[dbo].Tag_PharmProduct tp2 INNER JOIN [Drugsbase].[dbo].Tag_PharmAttribute tpa2 ON tp2.Tag_PharmAttribute_id=tpa2.id WHERE tpa2.tag_id=71 and tp2.product_id=a.DrugsBase_ID)");
                            s.Append(" and exists(select top(1) spid from dbo.spzl AS sp2 where sp2.spid=a.spid and sp2.is_cl = '是'))");
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
            string ess = Request["es"];
            if (!string.IsNullOrEmpty(ess) && ess != "0")
            {
                s.Append(" and exists(select p2.jigid from dbo.spzl p1 inner join dbo.spzl_jg p2 on p1.spid=p2.spid AND p1.sx1=p2.jigid and p1.spid=a.spid and p2.jigid='" + ess + "')");
            }

            string bShelves = Request["bShelves"];
            if (!string.IsNullOrEmpty(bShelves) && bShelves != "0")
            {
                if (bShelves == "1") s.Append(" and a.Product_ID in(select Product_ID from product_online_v)");//上架
                if (bShelves == "2") s.Append(" and a.Product_bShelves=0");//下架
                if (bShelves == "3") s.Append(" and a.Product_ID not in(select Product_ID from product_online_v) and a.Product_bShelves=1 ");//待上架
                if (bShelves == "4") s.Append(" and a.Product_bShelves=-1");//暂不上架
            }            
            string cgy = Request["cgy"];
            if (!string.IsNullOrEmpty(cgy) && cgy != "0")
            {
                if (cgy == "-1")
                {
                    s.AppendFormat(" and a.spid in (SELECT p1.spid FROM dbo.spzl p1 inner join dbo.spzl_jg p2 on p1.spid=p2.spid AND p1.sx1=p2.jigid WHERE p2.jigid<>'002' AND p2.jigid<>'003' AND p2.cgy='')");
                }
                else
                {
                    s.AppendFormat(" and a.spid in (SELECT p1.spid FROM dbo.spzl p1 inner join dbo.spzl_jg p2 on p1.spid=p2.spid AND p1.sx1=p2.jigid WHERE p2.jigid<>'002' AND p2.jigid<>'003' AND p2.cgy='{0}')", cgy);
                }
            }

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
            string bgys = Request["bgys"];
            switch (bgys)
            {

                case "0":
                    {
                        break;
                    }
                case "1":
                    {
                        s.Append(" and a.Product_ID not in(SELECT product_id FROM dbo.Product_Centre)");
                        break;
                    }
                default:
                    {
                        if (!string.IsNullOrEmpty(bgys))
                        {
                            s.Append(" and a.Product_ID in(SELECT product_id FROM dbo.Product_Centre WHERE iden=" + bgys + ")");
                        }
                        break;
                    }
            }


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
            else
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

            string bStock_s = Request["bStock_s"];
            string bStock_e = Request["bStock_e"];
            if (!string.IsNullOrEmpty(bStock_s))
            {
                s.AppendFormat("and Product_ID IN (SELECT product_id FROM(SELECT a.product_id,ISNULL(b.Stock1,a.Stock)-ISNULL(c.Stock,0)Stock FROM Product_Stock a LEFT JOIN dbo.Product_Centre b ON a.product_id = b.product_id LEFT JOIN dbo.Stock_Lock c ON a.product_id=c.Product_ID)a WHERE Stock>={0})", bStock_s);
            }
            if (!string.IsNullOrEmpty(bStock_e))
            {
                s.AppendFormat("and Product_ID IN (SELECT product_id FROM(SELECT a.product_id,ISNULL(b.Stock1,a.Stock)-ISNULL(c.Stock,0)Stock FROM Product_Stock a LEFT JOIN dbo.Product_Centre b ON a.product_id = b.product_id LEFT JOIN dbo.Stock_Lock c ON a.product_id=c.Product_ID)a WHERE Stock<={0})", bStock_e);
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

            if (SOSOshop.BLL.AdministrorManager.Get().AdminName == "admin")
            {
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "GB2312";
                Response.AppendHeader("Content-Disposition", "attachment;filename=shopping.xls");
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                Response.ContentType = "application/ms-excel";
                //this.EnableViewState = false;
                string wLine;
                System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
                wLine = show_list();
                oStringWriter.Write(wLine);//把字符串写到输入流
                Response.Write(oStringWriter.ToString());
                Response.End();
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
            pagesize = 1000;
            int pi = string.IsNullOrEmpty(Request.QueryString["current"]) ? 1 : int.Parse(Request.QueryString["current"]);
            pageindex = pi;
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();

            string sql = string.Format("select *," +
                  "(SELECT top 1 fullPath FROM dbo.Tag_PharmAttribute WHERE id IN (SELECT Tag_PharmAttribute_id FROM dbo.Tag_PharmProduct WHERE product_id=a.DrugsBase_ID AND Tag_PharmAttribute_id IN (SELECT id FROM dbo.Tag_PharmAttribute WHERE tag_id=69)))fullPath," +
                 "(select count(*) from [exchange].[dbo].[LinkRegionBidPricing]a1 where a1.goods_id=a.goods_id) as 中标," +
                "(select count(*) from [exchange].[dbo].[LinkRegionLimitPricing]b1 where b1.goods_id=a.goods_id) as 挂网," +
                "(select p2.cgy from dbo.spzl p1 inner join dbo.spzl_jg p2 on p1.spid=p2.spid AND p1.sx1=p2.jigid and p1.spid=a.spid) as cgy," +//采购员
                //"(select b.is_cl from product_online_v_1 as b where b.Product_ID= a.Product_ID) as is_cl," +//可拆零
                "(select b.Price_01 from product_online_v_1 as b where b.Product_ID= a.Product_ID) as price_01," +
                "(select b.Price_02 from product_online_v_1 as b where b.Product_ID= a.Product_ID) as price_02," +
                "ISNULL((SELECT Product_ID FROM product_online_v WHERE Product_ID=a.Product_ID),0)st," +//是否上架(在前台显示)
                "(select stock from product_online_v_1 as c where c.Product_ID=a.Product_ID) as stock," +                
                "(select isnull(stock,0) from Stock_Lock as c where c.Product_ID=a.Product_ID) as stock1," +
                "(SELECT iden FROM dbo.Product_Centre WHERE product_id=a.Product_ID) iden," +
                "ISNULL((SELECT 1 FROM DrugsBase_ZYC WHERE DrugsBase_ID=a.DrugsBase_ID),0) is_ZYC," +
                "(select p2.jigid from dbo.spzl p1 inner join dbo.spzl_jg p2 on p1.spid=p2.spid AND p1.sx1=p2.jigid and p1.spid=a.spid) as es " +//e商
                "{1} WHERE (a.Product_ID NOT IN(SELECT TOP ({0} * ({2} - 1)) a.Product_ID {1} {3} order by a.Product_ID desc)) {4} order by a.Product_ID desc", pagesize, getFrom(), pageindex, where.Length > 1 ? " where 1=1 " + where : "", where);
            DataTable dt = db.ExecuteTable(sql);
            SOSOshop.BLL.MongoHelper<MongoDB.Bson.BsonDocument> dbM = new SOSOshop.BLL.MongoHelper<MongoDB.Bson.BsonDocument>("Config");
            dbM.ChangeDB("datasynchronism");
            var cli = dbM._mongoCollection.FindAll();
            foreach (DataRow item in dt.Rows)
            {
                s.Append("<tr>");
                s.Append(table(item["Product_ID"]));
                s.Append(table(item["Product_Name"]));
                s.Append("<td>" +Public.GetSpecificationAndS(item) + "ｘ" + item["Goods_Pcs"] + "</td>");//+ +item["DrugsBase_Specification"].ToString() + "ｘ" + item["Goods_ConveRatio"].ToString() + "ｘ" + item["Goods_Pcs"].ToString() + "</td>");                                
                s.Append(table(item["DrugsBase_Formulation"]));
                s.Append(table(item["DrugsBase_ApprovalNumber"]));
                s.Append(table(item["DrugsBase_Manufacturer"]));
                s.Append(table(item["Product_Advertisement"]));
                s.Append(table(item["Product_SellingPoint"]));
                decimal Price_01 = 0; decimal.TryParse(Convert.ToString(item["Price_01"]), out Price_01);
                s.Append(table(string.Format("{0:f2}", Price_01)));
                decimal Price_02 = 0; decimal.TryParse(Convert.ToString(item["Price_02"]), out Price_02);
                s.Append(table(string.Format("{0:f2}", Price_02)));
                if (!Library.Lang.DataValidator.isNumber(item["stock"]))
                {
                    item["stock"] = 0;
                }
                if (!Library.Lang.DataValidator.isNumber(item["stock1"]))
                {
                    item["stock1"] = 0;
                }
                s.Append(table(string.Format("<a title='库存:{1},锁库:{2}'>{0}</a>", int.Parse(item["stock"].ToString()) - int.Parse(item["stock1"].ToString()), item["stock"], item["stock1"])));                
                //e商
                s.Append(table(item["es"]));
                //供货商
                string iden = "101";
                if (cli.Count() > 0 && !item.IsNull("iden") && Library.Lang.DataValidator.isNumber(item["iden"]))
                {
                    var idens = cli.Where(x => x["_id"].AsString == item["iden"].ToString());
                    if (idens.Count() > 0) iden = idens.First()["_id"].AsString;
                }
                s.Append(table(iden));
                if ("1" == Request["bBq1"])
                {
                    s.Append(table(0 < (int)item["中标"] ? "中标基本药物" : "调入基本药物"));
                    if (item["fullPath"] != DBNull.Value)
                    {
                        s.Append(table(fullPath((string)item["fullPath"])));
                    }
                    else
                    {
                        s.Append(table(""));
                    }

                }
                s.Append(table(_101shop.Common.SellType.GetType(int.Parse(item["sellType"].ToString()))));
                //s.Append(table(Convert.ToString(item["is_cl"]).Trim() == "" ? "否" : Convert.ToString(item["is_cl"]).Trim()));//可拆零
                s.Append(table(item["cgy"]));
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
                            s.Append(table("<a title='单击变更状态为暂不上架' style='' class='dbShelves' href='javascript:ishelves(-1, " + item["Product_id"] + ")'>待上架</a>"));
                        }
                    }
                }
                else
                {
                    s.Append(table("上架"));
                }
                s.Append("</tr>");
            }

            string jystr = "";
            if ("1" == Request["bBq1"])
            {
                jystr = @" <th scope='col'>
                            药品属性
                        </th>
                        <th scope='col'>
                            基药分类
                        </th>";
            }
            return string.Format("<table border='1'><tbody><tr><th>商品编号</th><th>商品名称</th><th>规格（含转换比、件装</th><th>剂型</th><th>批准文号</th>"
                + "<th>生产厂家</th><th>广告词</th><th>卖点</th><th>批发价</th><th>OTC价</th><th>库存</th><th>e商</th><th>供应商</th>" + jystr + "<th>销售方式</th><th scope=col>采购员</th><th>状态</th></tr></tbody>{0}</table>", s.ToString());
        }
        /// <summary>
        /// 取得基药分类
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string fullPath(string obj)
        {
            var dt = new SOSOshop.BLL.Db().ExecuteTableForCache("SELECT id,name FROM dbo.Tag_PharmAttribute WHERE tag_id=69");
            List<string> li = new List<string>();
            foreach (var item in obj.Trim('/').Split('/'))
            {
                li.Add(dt.AsEnumerable().Where(x => x.Field<int>("id") == int.Parse(item)).Select(x => x.Field<string>("name")).First());
            }
            return string.Join("/", li.ToArray());
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

        public string page()
        {
            param = "&shopname=" + Request["shopname"];
            param += "&changjia=" + Request["changjia"];
            param += "&pihao=" + Request["pihao"];
            param += "&Price=" + Request["Price"];
            param += "&is_cl=" + Request["is_cl"];
            param += "&bStock=" + Request["bStock"];
            param += "&bShelves=" + Request["bShelves"];
            param += "&bGoodsImage=" + Request["bGoodsImage"];
            SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();
            string sql = "select count(a.product_name) as c" + getFrom() + (where.Length > 1 ? " where 1=1 " + where + "" : "");
            //Response.Write(sql);
            int recordcount = (int)db.ExecuteTable(sql).Rows[0]["c"];
            double cs = (int)recordcount / pagesize;
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
            for (; i < j + 9 && i <= pagecount; i++)
            {
                s.Append("<a href=\"?current=" + (i + 1) + param + "\">");
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