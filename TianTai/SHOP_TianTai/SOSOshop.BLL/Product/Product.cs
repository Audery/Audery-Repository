using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;
using SOSOshop.Model;
using System.Collections;
using System.Data.SqlClient;

namespace SOSOshop.BLL.Product
{
    public class Product : Db
    {
        /// <summary>
        /// 调用GetPriceTable需要的字段 [当销售方式对应的件装或中包装为0时 按最小包装处理]
        /// </summary>
        public const string _PriceTableColumns = " is_ZYC,zbj,drj,(case when (sellType=2 and Goods_Pcs_Small<=0) or (sellType=3 and Goods_Pcs<=0) then 1 else sellType end)sellType,tag_ids,minsell,maxsell,spid,Stock,pihao,sxrq,Price_01,Price_02,Price_03,'' test001,";
        public const string _PriceTableColumns1 = " is_ZYC,zbj,drj,(case when (sellType=2 and Goods_Pcs_Small<=0) or (sellType=3 and Goods_Pcs<=0) then 1 else sellType end)sellType,tag_ids,minsell,maxsell,spid,Stock,p.pihao,p.sxrq,Price_01,Price_02,Price_03,'' test001,";
        /// <summary>
        /// 查询语句 [当销售方式对应的件装或中包装为0时 按最小包装处理]
        /// </summary>
        /// <param name="tableName">如 product_online_v.</param>
        /// <returns></returns>
        public static string sellTypeSql(string tableName = "")
        {
            return string.Format("(case when ({0}sellType=2 and {0}Goods_Pcs_Small<=0) or ({0}sellType=3 and {0}Goods_Pcs<=0) then 1 else {0}sellType end)sellType", tableName);
        }
        /// <summary>
        /// 商品查询
        /// </summary>
        public Product() { }
        /// <summary>
        /// 查询商品对象实体数据【缓存】
        /// </summary>
        /// <param name="Product_ID"></param>
        /// <returns></returns>
        public ProductInfo GetProductInfo(int Product_ID)
        {
            string key = "ProductInfo:Product_ID=" + Product_ID;
            SOSOshop.Model.ProductInfo model = Get(key) as SOSOshop.Model.ProductInfo;
            if (model == null)
            {
                string sql = "SELECT a.*,r.LimitPrice LimitPrice1,r.RetailPrice RetailPrice1,r.BidPrice BidPrice1 " +
                    "FROM product_online_v a  LEFT JOIN Goods_RegionPricing r on a.Goods_ID=r.Goods_ID and r.Region=10 " +
                    "where Product_ID=@Product_ID";
                DbCommand dbCommand = db.GetSqlStringCommand(sql);
                db.AddInParameter(dbCommand, "Product_ID", System.Data.DbType.Int32, Product_ID);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    if (dataReader.Read())
                    {
                        model = ReaderBind(dataReader);
                    }
                }
                Set(key, model, DateTime.Now.AddHours(1));
            }
            return model;
        }
        public ProductInfo GetProductInfo(int Product_ID, int Member_Class)
        {
            string key = "ProductInfo:Product_ID=" + Product_ID + "_" + Member_Class;
            SOSOshop.Model.ProductInfo model = Get(key) as SOSOshop.Model.ProductInfo;
            if (model == null)
            {
                string sql = "SELECT a.*,r.LimitPrice LimitPrice1,r.RetailPrice RetailPrice1,r.BidPrice BidPrice1 " +
                    "FROM product_online_v a  LEFT JOIN Goods_RegionPricing r on a.Goods_ID=r.Goods_ID and r.Region=10 " +
                    "where Product_ID=@Product_ID";
                DbCommand dbCommand = db.GetSqlStringCommand(sql);
                db.AddInParameter(dbCommand, "Product_ID", System.Data.DbType.Int32, Product_ID);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    if (dataReader.Read())
                    {
                        model = ReaderBind(dataReader, Member_Class);
                    }
                }
                Set(key, model, DateTime.Now.AddHours(1));
            }
            return model;
        }

        /// <summary>
        /// 查询商品列表信息
        /// </summary>
        public DataTable GetProductInfoListByWhere(int pagesize, int pageIndex, string where, string order, ref int recordCount, ref int pageCount)
        {
            string query = " a.*,r.LimitPrice LimitPrice1,r.RetailPrice RetailPrice1,r.BidPrice BidPrice1 ";
            string table = Regex.IsMatch(where, @"Pharm_", RegexOptions.IgnoreCase) ? " product_online_v a inner join DrugsBase_PharmMediNameLink b on a.DrugsBase_Id = b.DrugsBase_ID inner join DrugsBase_Pharm p on b.Pharm_ID=p.Pharm_ID LEFT JOIN Goods_RegionPricing r on a.Goods_ID=r.Goods_ID and r.Region=10 " : " product_online_v a  LEFT JOIN Goods_RegionPricing r on a.Goods_ID=r.Goods_ID and r.Region=10 ";
            return GetList("(select " + query + " from " + table + ") T", where, pagesize, pageIndex, order.ToLower().EndsWith("desc"), order.ToLower().Replace(" asc", "").Replace(" desc", ""), false, null, null, out recordCount, out pageCount);
        }
        /// <summary>
        /// 查询商品列表信息总记录数
        /// </summary>
        public int GetProductInfoCountByWhere(string where)
        {
            object o = ExecuteScalar("select count(1) from" +
                (Regex.IsMatch(where, @"Pharm_", RegexOptions.IgnoreCase) ? " product_online_v a inner join DrugsBase_PharmMediNameLink b on a.DrugsBase_Id = b.DrugsBase_ID inner join DrugsBase_Pharm p on b.Pharm_ID=p.Pharm_ID " : " product_online_v a ") +
                (!string.IsNullOrEmpty(where) ? " where " + where : ""));
            if (o != null && o.ToString() != "")
                return int.Parse(o.ToString());
            return 0;
        }

        /// <summary>
        /// 商品数据字段[不包括价格]
        /// </summary>
        public const string _ProductInfoColumns_NotIn_PriceTableColumns = "Product_ID,Product_Name,DrugsBase_DrugName,DrugsBase_Formulation,DrugsBase_Manufacturer,DrugsBase_ProName,DrugsBase_SimpeCode,DrugsBase_Specification,DrugsBase_ApprovalNumber,Registration_No,Product_KJZS,DrugsBase_ID,Goods_ID,Goods_Package_ID,Goods_ConveRatio,Goods_Pcs,Goods_Pcs_Small,maid1,ggy1,Product_SellingPoint,Product_Advertisement,Product_ClickNum,Product_SaleNum,Product_State,Product_bShelves,Goods_ConveRatio_Unit,Goods_ConveRatio_Unit_Name,Goods_Package_Material,Goods_Package_Material_Name,Goods_Unit,Image,Original,tag_ids";
        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public ProductInfo ReaderBind(IDataReader dataReader, int Member_Class)
        {
            SOSOshop.Model.ProductInfo model = new SOSOshop.Model.ProductInfo();
            model.Product_ID = Convert.IsDBNull(dataReader["Product_ID"]) ? 0 : (int)dataReader["Product_ID"];
            model.Product_KJZS = Convert.IsDBNull(dataReader["Product_KJZS"]) ? 0 : (int)dataReader["Product_KJZS"];
            model.Goods_ID = Convert.IsDBNull(dataReader["Goods_ID"]) ? 0 : (int)dataReader["Goods_ID"];
            model.Price_01 = Convert.IsDBNull(dataReader["Price_01"]) ? 0 : decimal.Parse(dataReader["Price_01"].ToString());
            model.Price_02 = Convert.IsDBNull(dataReader["Price_02"]) ? 0 : decimal.Parse(dataReader["Price_02"].ToString());
            model.Product_Name = dataReader["Product_Name"].ToString();
            //model.Product_Type = Convert.IsDBNull(dataReader["Product_Type"]) ? 0 : (int)dataReader["Product_Type"];
            //model.Product_Synopsis = dataReader["Product_Synopsis"].ToString();
            //model.Product_Remark = dataReader["Product_Remark"].ToString();
            //if (Member_Class == 1)
            //{
            //    model.Product_SellingPoint = dataReader["maid1"].ToString();
            //    model.Product_Advertisement = dataReader["ggy1"].ToString();
            //}
            //else
            //{
            model.Product_SellingPoint = Convert.IsDBNull(dataReader["Product_SellingPoint"]) ? "" : dataReader["Product_SellingPoint"].ToString();
            model.Product_Advertisement = Convert.IsDBNull(dataReader["Product_Advertisement"]) ? "" : dataReader["Product_Advertisement"].ToString();
            //}

            model.Product_ClickNum = Convert.IsDBNull(dataReader["Product_ClickNum"]) ? 0 : (int)dataReader["Product_ClickNum"];
            model.Product_SaleNum = Convert.IsDBNull(dataReader["Product_SaleNum"]) ? 0 : (int)dataReader["Product_SaleNum"];
            model.Product_State = dataReader["Product_State"].ToString();
            //model.Product_bStop = Convert.IsDBNull(dataReader["Product_bStop"]) ? 0 : (int)dataReader["Product_bStop"];
            //model.Product_bDiscount = Convert.IsDBNull(dataReader["Product_bDiscount"]) ? 0 : (int)dataReader["Product_bDiscount"];
            model.Product_bShelves = Convert.IsDBNull(dataReader["Product_bShelves"]) ? 0 : (int)dataReader["Product_bShelves"];
            //model.Product_bPriceConstantlyChanging = Convert.IsDBNull(dataReader["Product_bPriceConstantlyChanging"]) ? 0 : (int)dataReader["Product_bPriceConstantlyChanging"];
            model.Goods_ConveRatio = Convert.IsDBNull(dataReader["Goods_ConveRatio"]) ? 0 : (int)dataReader["Goods_ConveRatio"];
            //model.Goods_ConveRatio_Unit_ID = Convert.IsDBNull(dataReader["Goods_ConveRatio_Unit_ID"]) ? 0 : (int)dataReader["Goods_ConveRatio_Unit_ID"];
            model.Goods_ConveRatio_Unit = dataReader["Goods_ConveRatio_Unit"].ToString();
            model.Goods_ConveRatio_Unit_Name = dataReader["Goods_ConveRatio_Unit_Name"].ToString();
            model.Goods_Package_ID = Convert.IsDBNull(dataReader["Goods_Package_ID"]) ? 0 : (int)dataReader["Goods_Package_ID"];
            model.Goods_Pcs = Convert.IsDBNull(dataReader["Goods_Pcs"]) ? 0 : (int)dataReader["Goods_Pcs"];
            model.Goods_Pcs_Small = Convert.IsDBNull(dataReader["Goods_Pcs_Small"]) ? 0 : (int)dataReader["Goods_Pcs_Small"];
            model.Goods_Package_Material = dataReader["Goods_Package_Material"].ToString();
            model.Goods_Package_Material_Name = dataReader["Goods_Package_Material_Name"].ToString();
            //model.Goods_Unit_ID = Convert.IsDBNull(dataReader["Goods_Unit_ID"]) ? 0 : (int)dataReader["Goods_Unit_ID"];
            model.Goods_Unit = dataReader["Goods_Unit"].ToString();
            model.Image = dataReader["Image"].ToString();
            model.Original = dataReader["Original"].ToString();
            //model.CreateDate = Convert.IsDBNull(dataReader["CreateDate"]) ? new DateTime() : DateTime.Parse(dataReader["CreateDate"].ToString());
            //model.UpdateDate = Convert.IsDBNull(dataReader["UpdateDate"]) ? new DateTime() : DateTime.Parse(dataReader["UpdateDate"].ToString());

            model.RetailPrice = Convert.IsDBNull(dataReader["RetailPrice1"]) ? 0 : decimal.Parse(dataReader["RetailPrice1"].ToString());
            model.LimitPrice = Convert.IsDBNull(dataReader["LimitPrice1"]) ? 0 : decimal.Parse(dataReader["LimitPrice1"].ToString());
            //model.BidPrice = Convert.IsDBNull(dataReader["BidPrice1"]) ? 0 : decimal.Parse(dataReader["BidPrice1"].ToString());

            model.tag_ids = dataReader["tag_ids"].ToString();
            //model.IsSpecial = Convert.IsDBNull(dataReader["IsSpecial"]) || !(bool)dataReader["IsSpecial"] ? 0 : 1;
            //model.SpecialRole = Convert.IsDBNull(dataReader["SpecialRole"]) ? 0 : (int)dataReader["SpecialRole"];
            //model.Supplier_Province = Convert.IsDBNull(dataReader["Supplier_Province"]) ? 0 : (int)dataReader["Supplier_Province"];
            //model.Comment = dataReader["Comment"].ToString();
            model.drug_sensitive = Convert.IsDBNull(dataReader["drug_sensitive"]) || !(bool)dataReader["drug_sensitive"] ? false : true;

            model.DrugsBase_ID = Convert.IsDBNull(dataReader["DrugsBase_ID"]) ? 0 : (int)dataReader["DrugsBase_ID"];
            //model.DrugsBase_bCommissionProcessing = Convert.IsDBNull(dataReader["DrugsBase_bCommissionProcessing"]) ? 0 : (int)dataReader["DrugsBase_bCommissionProcessing"];
            //model.DrugsBase_bFinished = Convert.IsDBNull(dataReader["DrugsBase_bFinished"]) ? 0 : (int)dataReader["DrugsBase_bFinished"];
            //model.DrugsBase_bHealthInsuranceType = Convert.IsDBNull(dataReader["DrugsBase_bHealthInsuranceType"]) ? 0 : (int)dataReader["DrugsBase_bHealthInsuranceType"];
            //model.DrugsBase_bNationalEssentialDrug = Convert.IsDBNull(dataReader["DrugsBase_bNationalEssentialDrug"]) ? 0 : (int)dataReader["DrugsBase_bNationalEssentialDrug"];
            //model.DrugsBase_bNewDrugs = Convert.IsDBNull(dataReader["DrugsBase_bNewDrugs"]) ? 0 : (int)dataReader["DrugsBase_bNewDrugs"];
            //model.DrugsBase_bOTC = Convert.IsDBNull(dataReader["DrugsBase_bOTC"]) ? 0 : (int)dataReader["DrugsBase_bOTC"];
            //model.DrugsBase_bRaw = Convert.IsDBNull(dataReader["DrugsBase_bRaw"]) ? 0 : (int)dataReader["DrugsBase_bRaw"];
            //model.DrugsBase_bStop = Convert.IsDBNull(dataReader["DrugsBase_bStop"]) ? 0 : (int)dataReader["DrugsBase_bStop"];
            //model.Drugsbase_Direct_ID = Convert.IsDBNull(dataReader["Drugsbase_Direct_ID"]) ? 0 : (int)dataReader["Drugsbase_Direct_ID"];
            //model.DrugsBase_MadeIn = Convert.IsDBNull(dataReader["DrugsBase_MadeIn"]) ? 0 : (int)dataReader["DrugsBase_MadeIn"];
            //model.DrugsBase_MedicineType = Convert.IsDBNull(dataReader["DrugsBase_MedicineType"]) ? 0 : (int)dataReader["DrugsBase_MedicineType"];
            //model.DrugsBase_ProtectMedicine_ID = Convert.IsDBNull(dataReader["DrugsBase_ProtectMedicine_ID"]) ? 0 : (int)dataReader["DrugsBase_ProtectMedicine_ID"];
            //model.DDDJ = Convert.IsDBNull(dataReader["DDDJ"]) ? 0 : (int)dataReader["DDDJ"];
            //model.GMP = Convert.IsDBNull(dataReader["GMP"]) ? 0 : (int)dataReader["GMP"];
            //model.JK = Convert.IsDBNull(dataReader["JK"]) ? 0 : Convert.ToInt32(dataReader["JK"]);
            //model.YC = Convert.IsDBNull(dataReader["YC"]) ? 0 : (int)dataReader["YC"];
            //model.YY = Convert.IsDBNull(dataReader["YY"]) ? 0 : (int)dataReader["YY"];
            //model.YZYJ = Convert.IsDBNull(dataReader["YZYJ"]) ? 0 : (int)dataReader["YZYJ"];
            //model.ZL = Convert.IsDBNull(dataReader["ZL"]) ? 0 : (int)dataReader["ZL"];
            model.DrugsBase_ApprovalNumber = dataReader["DrugsBase_ApprovalNumber"].ToString();
            model.Registration_No = dataReader["Registration_No"].ToString();
            //model.DrugsBase_BaseCode = dataReader["DrugsBase_BaseCode"].ToString();
            model.DrugsBase_DrugName = dataReader["DrugsBase_DrugName"].ToString();
            model.DrugsBase_Formulation = dataReader["DrugsBase_Formulation"].ToString();
            model.DrugsBase_Manufacturer = dataReader["DrugsBase_Manufacturer"].ToString();
            model.DrugsBase_ProName = dataReader["DrugsBase_ProName"].ToString();
            //model.DrugsBase_QualityStandards = dataReader["DrugsBase_QualityStandards"].ToString();
            model.DrugsBase_SimpeCode = dataReader["DrugsBase_SimpeCode"].ToString();
            model.DrugsBase_Specification = dataReader["DrugsBase_Specification"].ToString();
            //model.DrugsBase_Address = dataReader["DrugsBase_Address"].ToString();
            //model.DrugsBase_RegisteredTrademark = dataReader["DrugsBase_RegisteredTrademark"].ToString();
            //model.DrugsBase_RegisteredGMP = dataReader["DrugsBase_RegisteredGMP"].ToString();
            model.sellType = Convert.IsDBNull(dataReader["sellType"]) ? 1 : (int)dataReader["sellType"];
            return model;
        }
        public ProductInfo ReaderBind(DataRow dataRow, int Member_Class)
        {
            SOSOshop.Model.ProductInfo model = new SOSOshop.Model.ProductInfo();
            model.Product_ID = Convert.IsDBNull(dataRow["Product_ID"]) ? 0 : (int)dataRow["Product_ID"];
            model.Product_KJZS = Convert.IsDBNull(dataRow["Product_KJZS"]) ? 0 : (int)dataRow["Product_KJZS"];
            model.Goods_ID = Convert.IsDBNull(dataRow["Goods_ID"]) ? 0 : (int)dataRow["Goods_ID"];
            model.Price_01 = Convert.IsDBNull(dataRow["Price_01"]) ? 0 : decimal.Parse(dataRow["Price_01"].ToString());
            model.Price_02 = Convert.IsDBNull(dataRow["Price_02"]) ? 0 : decimal.Parse(dataRow["Price_02"].ToString());
            model.Product_Name = dataRow["Product_Name"].ToString();
            //model.Product_Type = Convert.IsDBNull(dataReader["Product_Type"]) ? 0 : (int)dataReader["Product_Type"];
            //model.Product_Synopsis = dataReader["Product_Synopsis"].ToString();
            //model.Product_Remark = dataReader["Product_Remark"].ToString();
            //if (Member_Class == 1)
            //{
            //    if (dataRow.Table.Columns.Contains("maid1")) model.Product_SellingPoint = dataRow["maid1"].ToString();
            //    if (dataRow.Table.Columns.Contains("ggy1")) model.Product_Advertisement = dataRow["ggy1"].ToString();
            //}
            //else
            //{
            if (dataRow.Table.Columns.Contains("Product_SellingPoint")) model.Product_SellingPoint = dataRow["Product_SellingPoint"].ToString();
            if (dataRow.Table.Columns.Contains("Product_Advertisement")) model.Product_Advertisement = dataRow["Product_Advertisement"].ToString();
            //}

            model.Product_ClickNum = Convert.IsDBNull(dataRow["Product_ClickNum"]) ? 0 : (int)dataRow["Product_ClickNum"];
            model.Product_SaleNum = Convert.IsDBNull(dataRow["Product_SaleNum"]) ? 0 : (int)dataRow["Product_SaleNum"];
            model.Product_State = dataRow["Product_State"].ToString();
            //model.Product_bStop = Convert.IsDBNull(dataReader["Product_bStop"]) ? 0 : (int)dataReader["Product_bStop"];
            //model.Product_bDiscount = Convert.IsDBNull(dataReader["Product_bDiscount"]) ? 0 : (int)dataReader["Product_bDiscount"];
            model.Product_bShelves = Convert.IsDBNull(dataRow["Product_bShelves"]) ? 0 : (int)dataRow["Product_bShelves"];
            //model.Product_bPriceConstantlyChanging = Convert.IsDBNull(dataReader["Product_bPriceConstantlyChanging"]) ? 0 : (int)dataReader["Product_bPriceConstantlyChanging"];
            model.Goods_ConveRatio = Convert.IsDBNull(dataRow["Goods_ConveRatio"]) ? 0 : (int)dataRow["Goods_ConveRatio"];
            //model.Goods_ConveRatio_Unit_ID = Convert.IsDBNull(dataReader["Goods_ConveRatio_Unit_ID"]) ? 0 : (int)dataReader["Goods_ConveRatio_Unit_ID"];
            model.Goods_ConveRatio_Unit = dataRow["Goods_ConveRatio_Unit"].ToString();
            model.Goods_ConveRatio_Unit_Name = dataRow["Goods_ConveRatio_Unit_Name"].ToString();
            model.Goods_Package_ID = Convert.IsDBNull(dataRow["Goods_Package_ID"]) ? 0 : (int)dataRow["Goods_Package_ID"];
            model.Goods_Pcs = Convert.IsDBNull(dataRow["Goods_Pcs"]) ? 0 : (int)dataRow["Goods_Pcs"];
            model.Goods_Pcs_Small = Convert.IsDBNull(dataRow["Goods_Pcs_Small"]) ? 0 : (int)dataRow["Goods_Pcs_Small"];
            model.Goods_Package_Material = dataRow["Goods_Package_Material"].ToString();
            model.Goods_Package_Material_Name = dataRow["Goods_Package_Material_Name"].ToString();
            //model.Goods_Unit_ID = Convert.IsDBNull(dataReader["Goods_Unit_ID"]) ? 0 : (int)dataReader["Goods_Unit_ID"];
            model.Goods_Unit = dataRow["Goods_Unit"].ToString();
            model.Image = dataRow["Image"].ToString();
            model.Original = dataRow["Original"].ToString();
            //model.CreateDate = Convert.IsDBNull(dataReader["CreateDate"]) ? new DateTime() : DateTime.Parse(dataReader["CreateDate"].ToString());
            //model.UpdateDate = Convert.IsDBNull(dataReader["UpdateDate"]) ? new DateTime() : DateTime.Parse(dataReader["UpdateDate"].ToString());

            if (dataRow.Table.Columns.Contains("RetailPrice1")) model.RetailPrice = Convert.IsDBNull(dataRow["RetailPrice1"]) ? 0 : decimal.Parse(dataRow["RetailPrice1"].ToString());
            if (dataRow.Table.Columns.Contains("LimitPrice1")) model.LimitPrice = Convert.IsDBNull(dataRow["LimitPrice1"]) ? 0 : decimal.Parse(dataRow["LimitPrice1"].ToString());
            //if (dataRow.Table.Columns.Contains("BidPrice1")) model.BidPrice = Convert.IsDBNull(dataReader["BidPrice1"]) ? 0 : decimal.Parse(dataReader["BidPrice1"].ToString());

            model.tag_ids = dataRow["tag_ids"].ToString();
            //model.IsSpecial = Convert.IsDBNull(dataReader["IsSpecial"]) || !(bool)dataReader["IsSpecial"] ? 0 : 1;
            //model.SpecialRole = Convert.IsDBNull(dataReader["SpecialRole"]) ? 0 : (int)dataReader["SpecialRole"];
            //model.Supplier_Province = Convert.IsDBNull(dataReader["Supplier_Province"]) ? 0 : (int)dataReader["Supplier_Province"];
            //model.Comment = dataReader["Comment"].ToString();
            model.drug_sensitive = Convert.IsDBNull(dataRow["drug_sensitive"]) || !(bool)dataRow["drug_sensitive"] ? false : true;

            model.DrugsBase_ID = Convert.IsDBNull(dataRow["DrugsBase_ID"]) ? 0 : (int)dataRow["DrugsBase_ID"];
            //model.DrugsBase_bCommissionProcessing = Convert.IsDBNull(dataReader["DrugsBase_bCommissionProcessing"]) ? 0 : (int)dataReader["DrugsBase_bCommissionProcessing"];
            //model.DrugsBase_bFinished = Convert.IsDBNull(dataReader["DrugsBase_bFinished"]) ? 0 : (int)dataReader["DrugsBase_bFinished"];
            //model.DrugsBase_bHealthInsuranceType = Convert.IsDBNull(dataReader["DrugsBase_bHealthInsuranceType"]) ? 0 : (int)dataReader["DrugsBase_bHealthInsuranceType"];
            //model.DrugsBase_bNationalEssentialDrug = Convert.IsDBNull(dataReader["DrugsBase_bNationalEssentialDrug"]) ? 0 : (int)dataReader["DrugsBase_bNationalEssentialDrug"];
            //model.DrugsBase_bNewDrugs = Convert.IsDBNull(dataReader["DrugsBase_bNewDrugs"]) ? 0 : (int)dataReader["DrugsBase_bNewDrugs"];
            //model.DrugsBase_bOTC = Convert.IsDBNull(dataReader["DrugsBase_bOTC"]) ? 0 : (int)dataReader["DrugsBase_bOTC"];
            //model.DrugsBase_bRaw = Convert.IsDBNull(dataReader["DrugsBase_bRaw"]) ? 0 : (int)dataReader["DrugsBase_bRaw"];
            //model.DrugsBase_bStop = Convert.IsDBNull(dataReader["DrugsBase_bStop"]) ? 0 : (int)dataReader["DrugsBase_bStop"];
            //model.Drugsbase_Direct_ID = Convert.IsDBNull(dataReader["Drugsbase_Direct_ID"]) ? 0 : (int)dataReader["Drugsbase_Direct_ID"];
            //model.DrugsBase_MadeIn = Convert.IsDBNull(dataReader["DrugsBase_MadeIn"]) ? 0 : (int)dataReader["DrugsBase_MadeIn"];
            //model.DrugsBase_MedicineType = Convert.IsDBNull(dataReader["DrugsBase_MedicineType"]) ? 0 : (int)dataReader["DrugsBase_MedicineType"];
            //model.DrugsBase_ProtectMedicine_ID = Convert.IsDBNull(dataReader["DrugsBase_ProtectMedicine_ID"]) ? 0 : (int)dataReader["DrugsBase_ProtectMedicine_ID"];
            //model.DDDJ = Convert.IsDBNull(dataReader["DDDJ"]) ? 0 : (int)dataReader["DDDJ"];
            //model.GMP = Convert.IsDBNull(dataReader["GMP"]) ? 0 : (int)dataReader["GMP"];
            //model.JK = Convert.IsDBNull(dataReader["JK"]) ? 0 : Convert.ToInt32(dataReader["JK"]);
            //model.YC = Convert.IsDBNull(dataReader["YC"]) ? 0 : (int)dataReader["YC"];
            //model.YY = Convert.IsDBNull(dataReader["YY"]) ? 0 : (int)dataReader["YY"];
            //model.YZYJ = Convert.IsDBNull(dataReader["YZYJ"]) ? 0 : (int)dataReader["YZYJ"];
            //model.ZL = Convert.IsDBNull(dataReader["ZL"]) ? 0 : (int)dataReader["ZL"];
            model.DrugsBase_ApprovalNumber = dataRow["DrugsBase_ApprovalNumber"].ToString();
            model.Registration_No = dataRow["Registration_No"].ToString();
            //model.DrugsBase_BaseCode = dataReader["DrugsBase_BaseCode"].ToString();
            model.DrugsBase_DrugName = dataRow["DrugsBase_DrugName"].ToString();
            model.DrugsBase_Formulation = dataRow["DrugsBase_Formulation"].ToString();
            model.DrugsBase_Manufacturer = dataRow["DrugsBase_Manufacturer"].ToString();
            model.DrugsBase_ProName = dataRow["DrugsBase_ProName"].ToString();
            //model.DrugsBase_QualityStandards = dataReader["DrugsBase_QualityStandards"].ToString();
            model.DrugsBase_SimpeCode = dataRow["DrugsBase_SimpeCode"].ToString();
            model.DrugsBase_Specification = dataRow["DrugsBase_Specification"].ToString();
            //model.DrugsBase_Address = dataReader["DrugsBase_Address"].ToString();
            //model.DrugsBase_RegisteredTrademark = dataReader["DrugsBase_RegisteredTrademark"].ToString();
            //model.DrugsBase_RegisteredGMP = dataReader["DrugsBase_RegisteredGMP"].ToString();
            model.sellType = Convert.IsDBNull(dataRow["sellType"]) ? 1 : (int)dataRow["sellType"];
            return model;
        }
        public ProductInfo ReaderBind(IDataReader dataReader)
        {
            return ReaderBind(dataReader, 0);
        }

        /// <summary>
        /// 根据ID集合取得页面展示的商品【缓存】
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public DataTable GetPageList(string ids)
        {
            string key = Library.Lang.Input.MD5("Product_GetPageList_" + ids);
            string sql = "SELECT " + _PriceTableColumns + "DrugsBase_ID,Product_SellingPoint,Product_Advertisement,maid1,ggy1,drug_sensitive,is_cl,DrugsBase_Specification,Goods_ConveRatio_Unit_Name,Goods_ConveRatio_Unit,Goods_ConveRatio,Goods_Unit,Product_ID,Product_Name,DrugsBase_Manufacturer,RetailPrice,Product_Advertisement,Image,Goods_Pcs,Goods_Pcs_Small FROM product_online_v WHERE Product_ID IN(" + ids + ")";
            return ExecuteTableForCache(sql, key);
        }
        /// <summary>
        /// 根据条件取得页面展示的商品【缓存】
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable GetPageListByWhere(int top, string where)
        {
            if (!where.Trim().ToLower().StartsWith("and")) where = " and " + where;
            string sql = "SELECT TOP(" + top + ") " + _PriceTableColumns + "DrugsBase_ID,Product_SellingPoint,Product_Advertisement,maid1,ggy1,drug_sensitive,is_cl,DrugsBase_Specification,Goods_ConveRatio_Unit_Name,Goods_ConveRatio_Unit,Goods_ConveRatio,Goods_Unit,Product_ID,Product_Name,DrugsBase_Manufacturer,RetailPrice,Image,Goods_Pcs,Goods_Pcs_Small FROM product_online_v WHERE 1=1 " + where;
            return ExecuteTableForCache(sql, DateTime.Now.AddHours(1));
        }
        /// <summary>
        /// 根据ID集合取得页面展示的商品(带商品零售价)【缓存】
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public DataTable GetOtcPageList(string ids)
        {
            string key = Library.Lang.Input.MD5("Product_GetOtcPageList_" + ids);
            string sql = "SELECT " + _PriceTableColumns + "DrugsBase_ID,Product_SellingPoint,Product_Advertisement,maid1,ggy1,drug_sensitive,is_cl,DrugsBase_Specification,Goods_ConveRatio_Unit_Name,Goods_ConveRatio_Unit,Goods_ConveRatio,Goods_Unit,DrugsBase_Manufacturer,Product_ID,Product_Name,RetailPrice,Image,Goods_Pcs,Goods_Pcs_Small FROM product_online_v WHERE Product_ID IN(" + ids + ")";
            return ExecuteTableForCache(sql, key);
        }
        /// <summary>
        /// 根据ID集合取得促销页面的商品，不限制库存
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public DataTable GetOtcPageList2(string ids)
        {
            string key = Library.Lang.Input.MD5("Product_GetOtcPageList2_" + ids);
            string sql = "SELECT " + _PriceTableColumns + "DrugsBase_ID,Product_SellingPoint,Product_Advertisement,maid1,ggy1,drug_sensitive,is_cl,DrugsBase_Specification,Goods_ConveRatio_Unit_Name,Goods_ConveRatio_Unit,Goods_ConveRatio,Goods_Unit,DrugsBase_Manufacturer,Product_ID,Product_Name,RetailPrice,Image,(SELECT Stock FROM dbo.Stock_Lock WHERE Product_ID=product_online_v_2.Product_ID)Stock3,Goods_Pcs,Goods_Pcs_Small FROM product_online_v_2 WHERE Product_ID IN(" + ids + ")";
            return ExecuteTable(sql);
        }

        /// <summary>
        /// 根据ID集合取得页面展示的商品【缓存】
        /// </summary>
        /// <param name="ids">商品ID，分隔</param>
        /// <param name="columns">表product_online_v的字段</param>
        /// <returns></returns>
        public DataTable GetList(string ids, string columns, bool clearCache = false)
        {
            string sql = "SELECT " + columns + " FROM product_online_v WHERE Product_ID IN(" + ids + ")";
            string key = Library.Lang.Input.MD5(sql);
            if (clearCache) Set(key, null);
            return ExecuteTableForCache(sql, key);
        }
        /// <summary>
        /// 查询商品详情【缓存】
        /// </summary>
        /// <param name="id"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public DataTable GetProduct(int id, string columns, bool clearCache = false)
        {
            string sql = "SELECT " + columns + " FROM product_online_v WHERE Product_ID=" + id;
            string key = Library.Lang.Input.MD5(sql);
            if (clearCache) Set(key, null);
            return ExecuteTableForCache(sql, DateTime.Now.AddMinutes(180), key);
        }
        /// <summary>
        /// 查询商品字段
        /// </summary>
        /// <param name="id"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public object GetProductAttr(int id, string column)
        {
            string sql = "SELECT " + column + " FROM product_online_v WHERE Product_ID=" + id;
            return ExecuteScalar(sql);
        }
        /// <summary>
        /// 根据ID集合取得页面展示的商品
        /// </summary>
        /// <param name="ids">商品ID，分隔</param>
        /// <param name="columns">表product_online_v的字段</param>
        /// <returns></returns>
        public DataTable GetListNoCache(string ids, string columns)
        {
            string sql = "SELECT " + columns + " FROM product_online_v WHERE Product_ID IN(" + ids + ")";
            return ExecuteTable(sql);
        }

        #region 会员中心主页商品列表查询
        /// <summary>
        /// 本周特供【缓存】
        /// </summary>
        /// <param name="showNumber">记录数</param>
        /// <returns></returns>
        public DataTable Get_ThisWeekRanking_ProductList(int showNumber, int UID)
        {
            StringBuilder sbStr = new StringBuilder();
            sbStr.AppendFormat("select distinct top {0} ", showNumber);
            sbStr.Append(_PriceTableColumns);
            sbStr.Append(" p.Product_ID,p.Product_KJZS,p.Product_Name,p.Product_SaleNum,p.Product_SellingPoint,");
            sbStr.Append(" p.DrugsBase_Specification,p.Goods_ConveRatio,p.Goods_ConveRatio_Unit,p.Goods_Pcs,p.Goods_Pcs_Small,p.Image,p.drug_sensitive,");
            sbStr.Append("p.DrugsBase_Manufacturer,p.Product_Advertisement,p.is_cl");
            sbStr.Append(" from product_online_v p");
            sbStr.Append(" where p.Product_ID in (0)");
            return ExecuteTableForCache(sbStr.ToString(), DateTime.Now.AddDays(1));
        }
        /// <summary>
        /// 新品推荐【缓存】
        /// </summary>
        /// <param name="showNumber">记录数</param>
        /// <returns></returns>
        public DataTable Get_New_ProductList(int showNumber, int UID)
        {
            StringBuilder sbStr = new StringBuilder();
            sbStr.AppendFormat("select distinct top {0} ", showNumber);
            sbStr.Append(_PriceTableColumns);
            sbStr.Append(" p.Product_ID,p.Product_KJZS,p.Product_Name,p.Product_SaleNum,p.Product_SellingPoint,");
            sbStr.Append(" p.DrugsBase_Specification,p.Goods_ConveRatio,p.Goods_ConveRatio_Unit,p.Goods_Pcs,p.Goods_Pcs_Small,p.Image,p.drug_sensitive,");
            sbStr.Append("p.DrugsBase_Manufacturer,p.Product_Advertisement,p.is_cl");
            sbStr.Append(" from product_online_v p");
            sbStr.Append(" where p.Product_State like '_|_|1|%'");
            return ExecuteTableForCache(sbStr.ToString(), DateTime.Now.AddDays(1));
        }
        /// <summary>
        /// 疯狂抢购【缓存】
        /// </summary>
        /// <param name="showNumber">记录数</param>
        /// <returns></returns>
        public DataTable Get_Crazy_ProductList(int showNumber, int UID)
        {
            StringBuilder sbStr = new StringBuilder();
            sbStr.AppendFormat("select distinct top {0} ", showNumber);
            sbStr.Append(_PriceTableColumns);
            sbStr.Append(" p.Product_ID,p.Product_KJZS,p.Product_Name,p.Product_SaleNum,p.Product_SellingPoint,");
            sbStr.Append(" p.DrugsBase_Specification,p.Goods_ConveRatio,p.Goods_ConveRatio_Unit,p.Goods_Pcs,p.Goods_Pcs_Small,p.Image,p.drug_sensitive,");
            sbStr.Append("p.DrugsBase_Manufacturer,p.Product_Advertisement,p.is_cl");
            sbStr.Append(" from product_online_v p");
            sbStr.Append(" where p.Product_State like '_|_|_|1|%'");
            return ExecuteTableForCache(sbStr.ToString(), DateTime.Now.AddDays(1));
        }
        /// <summary>
        /// 您关注的商品【缓存】
        /// </summary>
        /// <param name="showNumber">记录数</param>
        /// <returns></returns>
        public DataTable Get_MemberFavorite_ProductList(int showNumber, int UID)
        {
            StringBuilder sbStr = new StringBuilder();
            sbStr.AppendFormat("select distinct top {0} ", showNumber);
            sbStr.Append(_PriceTableColumns);
            sbStr.Append(" p.Product_ID,p.Product_KJZS,p.Product_Name,p.Product_SaleNum,p.Product_SellingPoint,");
            sbStr.Append(" p.DrugsBase_Specification,p.Goods_ConveRatio,p.Goods_ConveRatio_Unit,p.Goods_Pcs,p.Goods_Pcs_Small,p.Image,p.drug_sensitive,");
            sbStr.Append("p.DrugsBase_Manufacturer,p.Product_Advertisement,p.Goods_ConveRatio_Unit_Name,p.Goods_Unit,p.is_cl");
            sbStr.Append(" from product_online_v p");
            sbStr.AppendFormat(" inner join (select top(1000) ProId from memberfavorite where uid={0} order by AddDate desc) b", UID);
            sbStr.Append(" on p.Product_ID = b.ProId");
            return ExecuteTableForCache(sbStr.ToString(), DateTime.Now.AddDays(1));
        }
        #endregion

        #region 商品详细页面列表查询
        /// <summary>
        /// 本月热销排行榜【缓存】
        /// </summary>
        /// <param name="showNumber">记录数</param>
        /// <returns></returns>
        public DataTable Get_ThisMonthRanking_ProductList(int showNumber)
        {
            StringBuilder sbStr = new StringBuilder();
            sbStr.AppendFormat("select distinct top {0} ", showNumber);
            sbStr.Append(" Stock,minsell,maxsell,p.spid,p.Product_ID,p.Product_KJZS,Price_01,Price_02,p.Product_Name,p.Product_SaleNum,p.Product_SellingPoint,");
            sbStr.Append(" p.DrugsBase_Specification,p.Goods_ConveRatio,p.Goods_ConveRatio_Unit,p.sellType,p.Goods_Pcs,p.Goods_Pcs_Small,p.Image,p.drug_sensitive,");
            sbStr.Append("p.DrugsBase_Manufacturer,p.Product_Advertisement,p.Goods_ConveRatio_Unit_Name,p.Goods_Unit,p.is_cl");
            sbStr.Append(" from orders o inner join orderproduct op on op.OrderId=o.OrderId");
            sbStr.Append(" inner join product_online_v p on p.Product_ID=op.ProId");
            sbStr.Append(" inner join DrugsBase_PharmMediNameLink b on p.DrugsBase_ID = b.DrugsBase_ID");
            sbStr.Append(" where datediff(month,o.OrderDate,getdate())<=1 ");
            sbStr.Append(" order by p.Product_SaleNum desc");
            return ExecuteTableForCache(sbStr.ToString(), DateTime.Now.AddDays(1));
        }
        /// <summary>
        /// 分类热销品种【缓存】
        /// </summary>
        /// <param name="showNumber">记录数</param>
        /// <param name="pharm_ID">药理分类ID</param>
        /// <returns></returns>
        public DataTable Get_ThisClass_ProductList(int showNumber, int pharm_ID)
        {
            StringBuilder sbStr = new StringBuilder();
            sbStr.AppendFormat("select distinct top {0} ", showNumber);
            sbStr.Append(_PriceTableColumns);
            sbStr.Append(" p.Product_ID,maid1,ggy1,p.Product_KJZS,p.Product_Name,p.Product_SaleNum,p.Product_SellingPoint,");
            sbStr.Append(" p.DrugsBase_Specification,p.Goods_ConveRatio,p.Goods_ConveRatio_Unit,p.Goods_Pcs,p.Goods_Pcs_Small,p.Image,p.drug_sensitive,");
            sbStr.Append(" p.DrugsBase_Manufacturer,p.Product_Advertisement,p.Goods_ConveRatio_Unit_Name,p.Goods_Unit,is_cl");
            sbStr.Append(" from product_online_v p");
            sbStr.Append(" inner join DrugsBase_PharmMediNameLink b on p.DrugsBase_ID = b.DrugsBase_ID");
            sbStr.AppendFormat(" where b.Pharm_ID={0} ", pharm_ID);
            sbStr.Append(" order by p.Product_SaleNum desc");
            return ExecuteTableForCache(sbStr.ToString(), DateTime.Now.AddDays(1));
        }
        public int Get_ThisClass_ProductList_Count(int pharm_ID)
        {
            StringBuilder sbStr = new StringBuilder();
            sbStr.Append("select count(distinct Product_ID) ");
            sbStr.Append(" from product_online_v p");
            sbStr.Append(" inner join DrugsBase_PharmMediNameLink b on p.DrugsBase_ID = b.DrugsBase_ID");
            sbStr.AppendFormat(" where b.Pharm_ID={0} ", pharm_ID);
            int c = 0; int.TryParse(Convert.ToString(ExecuteScalarForCache(sbStr.ToString(), DateTime.Now.AddDays(1))), out c);
            return c;
        }
        /// <summary>
        /// 树状分类热销品种【缓存】
        /// </summary>
        /// <param name="showNumber">记录数</param>
        /// <param name="pharm_ID">药理分类ID</param>
        /// <returns></returns>
        public DataTable Get_ThisClasses_ProductList(int showNumber, int pharm_ID)
        {
            StringBuilder sbStr = new StringBuilder();
            sbStr.AppendFormat("select distinct top {0} ", showNumber);
            sbStr.Append(_PriceTableColumns);
            sbStr.Append(" p.Product_ID,maid1,ggy1,p.Product_KJZS,p.Product_Name,p.Product_SaleNum,p.Product_SellingPoint,");
            sbStr.Append(" p.DrugsBase_Specification,p.Goods_ConveRatio,p.Goods_ConveRatio_Unit,p.Goods_Pcs,p.Goods_Pcs_Small,p.Image,p.drug_sensitive,");
            sbStr.Append(" p.DrugsBase_Manufacturer,p.Product_Advertisement,p.Goods_ConveRatio_Unit_Name,p.Goods_Unit,is_cl");
            sbStr.Append(" from product_online_v p");
            sbStr.Append(" inner join DrugsBase_PharmMediNameLink b on p.DrugsBase_ID = b.DrugsBase_ID inner join DrugsBase_Pharm d on b.Pharm_ID=d.Pharm_ID");
            sbStr.Append(" where d.Pharm_ID_Path+'\\' LIKE '%\\" + pharm_ID + "\\%'");
            sbStr.Append(" order by p.Product_SaleNum desc");
            return ExecuteTableForCache(sbStr.ToString(), DateTime.Now.AddDays(1));
        }
        /// <summary>
        /// 厂家的其他品种列表【缓存】
        /// </summary>
        /// <param name="showNumber">记录数</param>
        /// <param name="DrugsBase_Manufacturer">厂家ID</param>
        /// <param name="Product_ID">当前商品ID</param>
        /// <returns></returns>
        public DataTable Get_ThisDrugsBase_Manufacturer_Of_ProductList(int showNumber, string DrugsBase_Manufacturer, int Product_ID)
        {
            StringBuilder sbStr = new StringBuilder();
            sbStr.AppendFormat("select top {0} * from (", showNumber);
            sbStr.AppendFormat("select distinct ");
            sbStr.Append(_PriceTableColumns);
            sbStr.Append(" p.Product_ID,p.maid1,p.ggy1,p.Product_KJZS,p.Product_Name,p.Product_SaleNum,p.Product_SellingPoint,");
            sbStr.Append(" p.DrugsBase_Specification,p.Goods_ConveRatio,p.Goods_ConveRatio_Unit,p.Goods_Pcs,p.Goods_Pcs_Small,p.Image,p.drug_sensitive,");
            sbStr.Append("p.DrugsBase_Manufacturer,p.Product_Advertisement,p.Goods_ConveRatio_Unit_Name,p.Goods_Unit,p.is_cl");
            sbStr.Append(" from product_online_v p");
            sbStr.AppendFormat(" where p.Product_ID<>{0} and p.DrugsBase_Manufacturer like '%{1}%'", Product_ID, DrugsBase_Manufacturer.Replace("'", "''").Replace("%", ""));
            sbStr.Append(" )t order by Product_SaleNum desc");
            return ExecuteTableForCache(sbStr.ToString(), DateTime.Now.AddDays(1));
        }
        public int Get_ThisDrugsBase_Manufacturer_Of_ProductList_Count(string DrugsBase_Manufacturer, int Product_ID)
        {
            StringBuilder sbStr = new StringBuilder();
            sbStr.Append("select count(distinct Product_ID) ");
            sbStr.Append(" from product_online_v p");
            sbStr.AppendFormat(" where p.Product_ID<>{0} and p.DrugsBase_Manufacturer like '%{1}%'", Product_ID, DrugsBase_Manufacturer.Replace("'", "''").Replace("%", ""));
            int c = 0; int.TryParse(Convert.ToString(ExecuteScalarForCache(sbStr.ToString(), DateTime.Now.AddDays(1))), out c);
            return c;
        }
        /// <summary>
        /// 最近浏览过的商品
        /// </summary>
        /// <param name="showNumber">记录数</param>
        /// <returns></returns>
        public DataTable Get_History_Of_ProductList(int showNumber, int UID)
        {
            StringBuilder sbStr = new StringBuilder();
            sbStr.AppendFormat("select distinct top {0} * from (select top {1} ", showNumber, showNumber * 2);
            sbStr.Append(_PriceTableColumns);
            sbStr.Append(" p.Product_ID,p.Product_SellingPoint,p.Product_Advertisement,p.maid1,p.ggy1,p.Product_KJZS,p.Product_Name,p.Product_SaleNum,");
            sbStr.Append(" p.DrugsBase_Specification,p.Goods_ConveRatio,p.Goods_ConveRatio_Unit,p.Goods_Pcs,p.Goods_Pcs_Small,p.Image,p.drug_sensitive,");
            sbStr.Append(" p.DrugsBase_Manufacturer,p.Goods_ConveRatio_Unit_Name,p.Goods_Unit,p.is_cl");
            sbStr.Append(" from (select ProId from (select top(1000) Id,ProId from memberbrowserproductcontentlog where uid=17848 order by Id desc)b)b left join product_online_v p on p.Product_ID=b.ProId)t");
            sbStr.Append(" where Product_ID is not null ");
            DataSet ds = ExecuteDataSet(sbStr.ToString());
            return ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 ? ds.Tables[0] : null;
        }
        #endregion


        /// <summary>
        /// 最新上架
        /// </summary>
        /// <param name="showNumber">记录数</param>
        /// <returns></returns>
        public DataTable Get_New_HomeList(int top, int otcid)
        {
            StringBuilder sbStr = new StringBuilder();
            sbStr.AppendFormat("SELECT TOP {0} a.* FROM dbo.product_online_v a  WHERE Goods_ID IN (SELECT Goods_ID FROM dbo.Goods_Image) and DrugsBase_id in(SELECT product_id FROM Tag_PharmProduct WHERE Tag_PharmAttribute_id in (select id from Tag_PharmAttribute where fullPath like '%/{1}/%'))", top, otcid);
            return ExecuteTableForCache(sbStr.ToString(), DateTime.Now.AddDays(1));
        }

        /// <summary>
        /// 首页按标签取数据，按销量
        /// </summary>
        /// <param name="showNumber">记录数</param>
        /// <returns></returns>
        public DataTable Get_HomeList(int top, int otcid)
        {
            StringBuilder sbStr = new StringBuilder();
            sbStr.AppendFormat("SELECT TOP {0} a.* FROM dbo.product_online_v a INNER JOIN dbo.Product b ON a.Product_ID=b.Product_ID  WHERE b.bimage=1 and a.DrugsBase_id in(SELECT product_id FROM Tag_PharmProduct WHERE and Tag_PharmAttribute_id in (select id from Tag_PharmAttribute where fullPath like '%/{1}/%')) ORDER BY a.Product_SaleNum DESC", top, otcid);
            return ExecuteTableForCache(sbStr.ToString(), DateTime.Now.AddDays(1));
        }
        /// <summary>
        /// 首页按标签取数据，按销量
        /// </summary>
        /// <param name="showNumber">记录数</param>
        /// <returns></returns>
        public DataTable Get_HomeJinKouList(int top)
        {
            StringBuilder sbStr = new StringBuilder();
            sbStr.AppendFormat("SELECT TOP {0} a.* FROM dbo.product_online_v a WHERE DrugsBase_ID IN (SELECT product_id FROM dbo.Tag_PharmProduct WHERE Tag_PharmAttribute_id IN(SELECT id FROM dbo.Tag_PharmAttribute WHERE tag_id=88)) ORDER BY a.Product_SaleNum DESC", top);
            return ExecuteTableForCache(sbStr.ToString(), DateTime.Now.AddDays(1));
        }
        /// <summary>
        /// 首页按中药饮片取数据，按销量
        /// </summary>
        /// <param name="showNumber">记录数</param>
        /// <returns></returns>
        public DataTable Get_HomeZYYPList(int top, int otcid)
        {
            StringBuilder sbStr = new StringBuilder();
            sbStr.AppendFormat("SELECT TOP {0} a.* FROM dbo.product_online_v a INNER JOIN dbo.Product b ON a.Product_ID=b.Product_ID WHERE b.bimage=1 AND a.DrugsBase_id in(select DrugsBase_ID from [DrugsBase_PharmMediNameLink] where [Pharm_ID] in (select [Pharm_ID] from [DrugsBase_Pharm] where [Pharm_ID_Path] like '%\\{1}%')) ORDER BY a.Product_SaleNum DESC", top, otcid);
            return ExecuteTableForCache(sbStr.ToString(), DateTime.Now.AddDays(1));
        }
        /// <summary>
        /// 首页按中药饮片药理分类取数据[jigid='005']，按销量[ot=0]，随机[ot=1]
        /// </summary>
        /// <param name="top">记录数</param>
        /// <param name="otcid">分类</param>
        /// <param name="ot">方式</param>
        /// <param name="isCX">促销</param>
        /// <param name="cached">缓存</param>
        /// <returns></returns>
        public DataTable Get_ZYYPHomeList(int top, string otcid, int ot = 0, bool isCX = false, bool cached = true)
        {
            StringBuilder sbStr = new StringBuilder();
            sbStr.AppendFormat("SELECT TOP {0} a.*,b.ProductionBrandName,b.ProductionMethodName,b.ProductionAddress,b.ProductionLevelName FROM dbo.product_online_v a LEFT JOIN _ViewDrugsBase_ZYC b ON a.DrugsBase_ID=b.DrugsBase_ID INNER JOIN dbo.Product c ON a.Product_ID=c.Product_ID WHERE c.bimage=1 and b.DrugsBase_ID is not null and exists(select p2.jigid from dbo.spzl p1 inner join dbo.spzl_jg p2 on p1.spid=p2.spid AND p1.sx1=p2.jigid and p1.spid=a.spid and p2.jigid='005') and exists(SELECT 1 FROM DrugsBase_PharmMediNameLink x INNER JOIN DrugsBase_Pharm y ON x.Pharm_ID=y.Pharm_ID WHERE x.DrugsBase_ID=a.DrugsBase_ID AND y.Pharm_ID_Path LIKE '%\\{1}%')", top, otcid);
            if (isCX)
            {
                string Product_ID = "";
                try
                {
                    //从价格体系里面取得促销品种
                    JTTX.Price p = new JTTX.Price();
                    Product_ID = string.Join(",", "0");
                }
                catch { }
                if (Product_ID == "") Product_ID = "0";
                sbStr.Append(" and (a.Product_ID in(" + Product_ID + ")");
                sbStr.Append(@"OR EXISTS(SELECT 1 FROM ZENGPFAZL a1 INNER JOIN ZENGPFA_SP b1 ON a1.zengpfaid=b1.Zengpfaid 
WHERE a1.is_1sp_dzp='是' AND a1.beactive='是' and b1.Spid=a.Spid and CONVERT(DATETIME,Start_rq) <GETDATE() AND CONVERT(DATETIME,end_rq) >GETDATE() ))");
            }
            if (ot == 1) sbStr.Append(" ORDER BY NEWID()");
            else sbStr.Append(" ORDER BY a.Product_SaleNum DESC");
            if (cached) return ExecuteTableForCache(sbStr.ToString(), DateTime.Now.AddDays(1));
            else return ExecuteTable(sbStr.ToString());
        }
        public DataTable Get_ZYYPHomeList(string ids, bool cached = true)
        {
            StringBuilder sbStr = new StringBuilder();
            sbStr.AppendFormat("SELECT a.*,b.ProductionBrandName,b.ProductionMethodName,b.ProductionAddress,b.ProductionLevelName FROM dbo.product_online_v a LEFT JOIN _ViewDrugsBase_ZYC b ON a.DrugsBase_ID=b.DrugsBase_ID WHERE Goods_ID IN (SELECT Goods_ID FROM dbo.Goods_Image) and b.DrugsBase_ID is not null and exists(select p2.jigid from dbo.spzl p1 inner join dbo.spzl_jg p2 on p1.spid=p2.spid AND p1.sx1=p2.jigid and p1.spid=a.spid and p2.jigid='005') and a.Product_ID in({0})", ids);
            if (cached) return ExecuteTableForCache(sbStr.ToString(), DateTime.Now.AddDays(1));
            else return ExecuteTable(sbStr.ToString());
        }
        /// <summary>
        /// 首页按药理药效取数据，按销量
        /// </summary>
        /// <param name="showNumber">记录数</param>
        /// <returns></returns>
        public DataTable Get_HomeYLYXList(int top, int Pharm_ID)
        {
            StringBuilder sbStr = new StringBuilder();
            sbStr.AppendFormat(@"SELECT TOP {0} a.* FROM dbo.product_online_v a INNER JOIN dbo.Product b ON a.Product_ID=b.Product_ID  WHERE b.bimage=1 and a.DrugsBase_id in(SELECT DrugsBase_ID FROM dbo.DrugsBase_PharmMediNameLink WHERE Pharm_ID IN (SELECT Pharm_ID FROM dbo.DrugsBase_Pharm WHERE Pharm_ID_Path LIKE('%\{1}%'))) ORDER BY a.Product_SaleNum DESC", top, Pharm_ID);
            return ExecuteTableForCache(sbStr.ToString(), DateTime.Now.AddDays(1));
        }

        /// <summary>
        /// 取得商品是否为中药饮片[取消使用]
        /// </summary>
        /// <param name="Product_ID"></param>
        /// <returns></returns>
        public bool IsZyyp(int Product_ID)
        {
            lock (this)
            {
                bool ok = false;
                string sql = "SELECT DISTINCT Product_ID FROM product_online_v p INNER JOIN DrugsBase_ZYC b ON p.DrugsBase_ID=b.DrugsBase_ID";
                string key = Library.Lang.Input.MD5(sql);
                List<int> ids = Get(key) as List<int>;
                if (ids == null || ids.Count == 0)
                {
                    ids = new List<int>();
                    DataTable dt = ExecuteTable(sql);
                    foreach (DataRow dr in dt.Rows) ids.Add((int)dr[0]);
                    Set(key, ids, DateTime.Now.AddDays(1));
                }
                if (ids != null)
                {
                    ok = ids.Contains(Product_ID);
                }
                return ok;
            }
        }
        /// <summary>
        /// 取得商品Spid[取消使用]
        /// </summary>
        /// <param name="Product_ID"></param>
        /// <returns></returns>
        public string GetSpid(int Product_ID)
        {
            lock (this)
            {
                string spid = "";
                string sql = "SELECT Product_ID,spid FROM Product";
                string key = Library.Lang.Input.MD5(sql);
                Dictionary<int, string> ids = Get(key) as Dictionary<int, string>;
                if (ids == null || ids.Count == 0 || !ids.ContainsKey(Product_ID))
                {
                    ids = new Dictionary<int, string>();
                    DataTable dt = ExecuteTable(sql);
                    foreach (DataRow dr in dt.Rows) ids[(int)dr[0]] = dr[1] + "";
                    Set(key, ids, DateTime.Now.AddDays(1));
                }
                if (ids != null && ids.ContainsKey(Product_ID))
                {
                    spid = ids[Product_ID];
                }
                return spid;
            }
        }

        /// <summary>
        /// 判断商品erp的编号是否存在
        /// </summary>
        /// <param name="spid"></param>
        /// <returns></returns>
        public bool Exist(string spid)
        {
            string sql = string.Format("SELECT COUNT(*) FROM dbo.Product where spid='{0}'", spid);
            return (int)base.ExecuteScalar(sql) > 0;
        }



    }
}
