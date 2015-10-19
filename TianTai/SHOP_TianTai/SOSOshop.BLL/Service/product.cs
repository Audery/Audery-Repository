using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Reflection;

namespace SOSOshop.BLL.Service
{
    /// <summary>
    /// 商品数据
    /// </summary>
    public class product : SOSOshop.BLL.Db
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string spid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from product where spid=@spid ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "spid", DbType.String, spid);
            return ((int)db.ExecuteScalar(dbCommand)) > 0;

        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(SOSOshop.Model.Service.product model)
        {
            if (model.beactive == "是")
            {
                model.Product_bShelves = 1;
            }
            else
            {
                model.Product_bShelves = 0;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into product(");
            strSql.Append("Product_ID,Goods_ID,DrugsBase_ID,Goods_Package_ID,Product_Name,Product_bShelves,beactive,Product_SaleNum,Product_KJZS,DrugsBase_ApprovalNumber,Registration_No,DrugsBase_Manufacturer,Manufacturer_Short,DrugsBase_DrugName,DrugsBase_Formulation,Goods_ConveRatio,Goods_ConveRatio_Unit_Name,Goods_ConveRatio_Unit,Goods_Package_Material,Goods_Package_Material_Name,Goods_Pcs,Goods_Pcs_Small,tag_ids,drug_sensitive,spid,Product_Advertisement,Product_SellingPoint,DrugsBase_ProName,Image,Original,Goods_Unit,DrugsBase_SimpeCode,DrugsBase_Specification,RetailPrice,LimitPrice,Product_ClickNum,Product_State)");

            strSql.Append(" values (");
            strSql.Append("@Product_ID,@Goods_ID,@DrugsBase_ID,@Goods_Package_ID,@Product_Name,@Product_bShelves,@beactive,@Product_SaleNum,@Product_KJZS,@DrugsBase_ApprovalNumber,@Registration_No,@DrugsBase_Manufacturer,@Manufacturer_Short,@DrugsBase_DrugName,@DrugsBase_Formulation,@Goods_ConveRatio,@Goods_ConveRatio_Unit_Name,@Goods_ConveRatio_Unit,@Goods_Package_Material,@Goods_Package_Material_Name,@Goods_Pcs,@Goods_Pcs_Small,@tag_ids,@drug_sensitive,@spid,@Product_Advertisement,@Product_SellingPoint,@DrugsBase_ProName,@Image,@Original,@Goods_Unit,@DrugsBase_SimpeCode,@DrugsBase_Specification,@RetailPrice,@LimitPrice,@Product_ClickNum,@Product_State)");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Product_ID", DbType.Int32, model.Product_ID);
            db.AddInParameter(dbCommand, "Goods_ID", DbType.Int32, model.Goods_ID);
            db.AddInParameter(dbCommand, "DrugsBase_ID", DbType.Int32, model.DrugsBase_ID);
            db.AddInParameter(dbCommand, "Goods_Package_ID", DbType.Int32, model.Goods_Package_ID);
            db.AddInParameter(dbCommand, "Product_Name", DbType.String, model.Product_Name);
            db.AddInParameter(dbCommand, "Product_bShelves", DbType.Int32, model.Product_bShelves);
            db.AddInParameter(dbCommand, "beactive", DbType.AnsiString, model.beactive);
            db.AddInParameter(dbCommand, "Product_SaleNum", DbType.Int32, model.Product_SaleNum);
            db.AddInParameter(dbCommand, "Product_KJZS", DbType.Int32, model.Product_KJZS);
            db.AddInParameter(dbCommand, "DrugsBase_ApprovalNumber", DbType.String, model.DrugsBase_ApprovalNumber);
            db.AddInParameter(dbCommand, "Registration_No", DbType.String, model.Registration_No);
            db.AddInParameter(dbCommand, "DrugsBase_Manufacturer", DbType.String, model.DrugsBase_Manufacturer);
            db.AddInParameter(dbCommand, "Manufacturer_Short", DbType.String, model.Manufacturer_Short);
            db.AddInParameter(dbCommand, "DrugsBase_DrugName", DbType.String, model.DrugsBase_DrugName);
            db.AddInParameter(dbCommand, "DrugsBase_Formulation", DbType.String, model.DrugsBase_Formulation);
            db.AddInParameter(dbCommand, "Goods_ConveRatio", DbType.Int32, model.Goods_ConveRatio);
            db.AddInParameter(dbCommand, "Goods_ConveRatio_Unit_Name", DbType.String, model.Goods_ConveRatio_Unit_Name);
            db.AddInParameter(dbCommand, "Goods_ConveRatio_Unit", DbType.String, model.Goods_ConveRatio_Unit);
            db.AddInParameter(dbCommand, "Goods_Package_Material", DbType.String, model.Goods_Package_Material);
            db.AddInParameter(dbCommand, "Goods_Package_Material_Name", DbType.String, model.Goods_Package_Material_Name);
            db.AddInParameter(dbCommand, "Goods_Pcs", DbType.Int32, model.Goods_Pcs);
            db.AddInParameter(dbCommand, "Goods_Pcs_Small", DbType.Int32, model.Goods_Pcs_Small);
            db.AddInParameter(dbCommand, "tag_ids", DbType.String, model.tag_ids);
            db.AddInParameter(dbCommand, "drug_sensitive", DbType.Boolean, model.drug_sensitive);
            db.AddInParameter(dbCommand, "spid", DbType.AnsiString, model.spid);
            db.AddInParameter(dbCommand, "Product_Advertisement", DbType.String, model.Product_Advertisement);
            db.AddInParameter(dbCommand, "Product_SellingPoint", DbType.String, model.Product_SellingPoint);
            db.AddInParameter(dbCommand, "DrugsBase_ProName", DbType.String, model.DrugsBase_ProName);
            db.AddInParameter(dbCommand, "Image", DbType.String, model.Image);
            db.AddInParameter(dbCommand, "Original", DbType.String, model.Original);
            db.AddInParameter(dbCommand, "Goods_Unit", DbType.String, model.Goods_Unit);
            db.AddInParameter(dbCommand, "DrugsBase_SimpeCode", DbType.String, model.DrugsBase_SimpeCode);
            db.AddInParameter(dbCommand, "DrugsBase_Specification", DbType.String, model.DrugsBase_Specification);
            db.AddInParameter(dbCommand, "RetailPrice", DbType.Double, model.RetailPrice);
            db.AddInParameter(dbCommand, "LimitPrice", DbType.Double, model.LimitPrice);
            db.AddInParameter(dbCommand, "Product_ClickNum", DbType.Int32, model.Product_ClickNum);
            db.AddInParameter(dbCommand, "Product_State", DbType.AnsiString, model.Product_State);
            db.ExecuteScalar(dbCommand);
            if (!Library.Lang.DataValidator.isNumber(model.Goods_Package_ID)) model.Goods_Package_ID = 0;
            if (!Library.Lang.DataValidator.isNumber(model.Goods_ID)) model.Goods_ID = 0;
            if (model.Goods_ID == 0)
            {
                new SOSOshop.BLL.Service.Goods().AddGoods(model);
            }
            else if (model.Goods_Package_ID == 0)
            {
                new SOSOshop.BLL.Service.Goods_Package().AddGoods_Package(model);
            }
            return true;
        }
        public static SOSOshop.Model.Service.product ReaderBind(IDataReader dataReader)
        {
            SOSOshop.Model.Service.product model = new SOSOshop.Model.Service.product();
            object ojb;
            ojb = dataReader["Product_ID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Product_ID = (int)ojb;
            }
            ojb = dataReader["Goods_ID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Goods_ID = (int)ojb;
            }
            ojb = dataReader["DrugsBase_ID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.DrugsBase_ID = (int)ojb;
            }
            ojb = dataReader["Goods_Package_ID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Goods_Package_ID = (int)ojb;
            }
            model.Product_Name = dataReader["Product_Name"].ToString().Trim();
            ojb = dataReader["Price_01"];
            ojb = dataReader["Product_bShelves"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Product_bShelves = (int)ojb;
            }
            model.beactive = dataReader["beactive"].ToString().Trim();
            ojb = dataReader["Product_SaleNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Product_SaleNum = (int)ojb;
            }
            ojb = dataReader["Product_KJZS"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Product_KJZS = (int)ojb;
            }
            model.DrugsBase_ApprovalNumber = dataReader["DrugsBase_ApprovalNumber"].ToString().Trim().Trim();
            model.Registration_No = dataReader["Registration_No"].ToString().Trim();
            model.DrugsBase_Manufacturer = dataReader["DrugsBase_Manufacturer"].ToString().Trim();
            model.Manufacturer_Short = dataReader["Manufacturer_Short"].ToString().Trim();
            model.DrugsBase_DrugName = dataReader["DrugsBase_DrugName"].ToString().Trim();
            model.DrugsBase_Formulation = dataReader["DrugsBase_Formulation"].ToString().Trim();
            ojb = dataReader["Goods_ConveRatio"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Goods_ConveRatio = (int)ojb;
            }
            model.Goods_ConveRatio_Unit_Name = dataReader["Goods_ConveRatio_Unit_Name"].ToString().Trim();
            model.Goods_ConveRatio_Unit = dataReader["Goods_ConveRatio_Unit"].ToString().Trim();
            model.Goods_Package_Material = dataReader["Goods_Package_Material"].ToString().Trim();
            model.Goods_Package_Material_Name = dataReader["Goods_Package_Material_Name"].ToString().Trim();
            ojb = dataReader["Goods_Pcs"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Goods_Pcs = (int)ojb;
            }
            ojb = dataReader["Goods_Pcs_Small"];
            if (ojb != null && ojb != DBNull.Value)
            {
                int temp;
                int.TryParse(ojb.ToString(), out temp);
                model.Goods_Pcs_Small = temp;
            }
            model.tag_ids = dataReader["tag_ids"].ToString().Trim();
            ojb = dataReader["drug_sensitive"];
            if (ojb != null && ojb != DBNull.Value)
            {
                if ("敏感购销" == ojb.ToString().Trim())
                {
                    model.drug_sensitive = true;
                }
                else
                {
                    model.drug_sensitive = false;
                }
            }
            model.spid = dataReader["spid"].ToString().Trim();
            model.Product_Advertisement = dataReader["Product_Advertisement"].ToString().Trim();
            model.Product_SellingPoint = dataReader["Product_SellingPoint"].ToString().Trim();
            model.DrugsBase_ProName = dataReader["DrugsBase_ProName"].ToString().Trim();
            model.Image = dataReader["Image"].ToString().Trim();
            model.Original = dataReader["Original"].ToString().Trim();
            model.Goods_Unit = dataReader["Goods_Unit"].ToString().Trim();
            model.DrugsBase_SimpeCode = dataReader["DrugsBase_SimpeCode"].ToString().Trim();
            model.DrugsBase_Specification = dataReader["DrugsBase_Specification"].ToString().Trim();
            ojb = dataReader["RetailPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.RetailPrice = decimal.Parse(ojb.ToString().Trim());
            }
            ojb = dataReader["LimitPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.LimitPrice = decimal.Parse(ojb.ToString().Trim());
            }
            ojb = dataReader["Product_ClickNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Product_ClickNum = (int)ojb;
            }
            model.Product_State = dataReader["Product_State"].ToString().Trim();
            return model;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SOSOshop.Model.Service.product model)
        {
            if (model.beactive == "是")
            {
                model.Product_bShelves = 1;
            }
            else
            {
                model.Product_bShelves = 0;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update product set ");
            strSql.Append("Product_ID=@Product_ID,");
            strSql.Append("Goods_ID=@Goods_ID,");
            strSql.Append("DrugsBase_ID=@DrugsBase_ID,");
            strSql.Append("Goods_Package_ID=@Goods_Package_ID,");
            strSql.Append("Product_Name=@Product_Name,");
            if (model.beactive != "是")
            {
                strSql.Append("Product_bShelves=@Product_bShelves,");
            }            
            strSql.Append("beactive=@beactive,");
            strSql.Append("DrugsBase_ApprovalNumber=@DrugsBase_ApprovalNumber,");
            strSql.Append("Registration_No=@Registration_No,");
            strSql.Append("DrugsBase_Manufacturer=@DrugsBase_Manufacturer,");
            strSql.Append("Manufacturer_Short=@Manufacturer_Short,");
            strSql.Append("DrugsBase_DrugName=@DrugsBase_DrugName,");
            strSql.Append("DrugsBase_Formulation=@DrugsBase_Formulation,");
            strSql.Append("Goods_ConveRatio=@Goods_ConveRatio,");
            strSql.Append("Goods_ConveRatio_Unit_Name=@Goods_ConveRatio_Unit_Name,");
            strSql.Append("Goods_ConveRatio_Unit=@Goods_ConveRatio_Unit,");
            strSql.Append("Goods_Package_Material=@Goods_Package_Material,");
            strSql.Append("Goods_Package_Material_Name=@Goods_Package_Material_Name,");
            strSql.Append("Goods_Pcs=@Goods_Pcs,");
            strSql.Append("Goods_Pcs_Small=@Goods_Pcs_Small,");
            strSql.Append("drug_sensitive=@drug_sensitive,");
            strSql.Append("spid=@spid,");
            strSql.Append("Product_Advertisement=@Product_Advertisement,");
            strSql.Append("Product_SellingPoint=@Product_SellingPoint,");
            strSql.Append("DrugsBase_ProName=@DrugsBase_ProName,");
            strSql.Append("Image=@Image,");
            strSql.Append("Original=@Original,");
            strSql.Append("Goods_Unit=@Goods_Unit,");
            strSql.Append("DrugsBase_SimpeCode=@DrugsBase_SimpeCode,");
            strSql.Append("DrugsBase_Specification=@DrugsBase_Specification,");
            strSql.Append("RetailPrice=@RetailPrice,");
            strSql.Append("LimitPrice=@LimitPrice");
            strSql.Append(" where Product_ID=@Product_ID ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Product_ID", DbType.Int32, model.Product_ID);
            db.AddInParameter(dbCommand, "Goods_ID", DbType.Int32, model.Goods_ID);
            db.AddInParameter(dbCommand, "DrugsBase_ID", DbType.Int32, model.DrugsBase_ID);
            db.AddInParameter(dbCommand, "Goods_Package_ID", DbType.Int32, model.Goods_Package_ID);
            db.AddInParameter(dbCommand, "Product_Name", DbType.String, model.Product_Name);
            if (model.beactive != "是")
            {
                db.AddInParameter(dbCommand, "Product_bShelves", DbType.Int32, model.Product_bShelves);
            }               
            db.AddInParameter(dbCommand, "beactive", DbType.AnsiString, model.beactive);
            db.AddInParameter(dbCommand, "DrugsBase_ApprovalNumber", DbType.String, model.DrugsBase_ApprovalNumber);
            db.AddInParameter(dbCommand, "Registration_No", DbType.String, model.Registration_No);
            db.AddInParameter(dbCommand, "DrugsBase_Manufacturer", DbType.String, model.DrugsBase_Manufacturer);
            db.AddInParameter(dbCommand, "Manufacturer_Short", DbType.String, model.Manufacturer_Short);
            db.AddInParameter(dbCommand, "DrugsBase_DrugName", DbType.String, model.DrugsBase_DrugName);
            db.AddInParameter(dbCommand, "DrugsBase_Formulation", DbType.String, model.DrugsBase_Formulation);
            db.AddInParameter(dbCommand, "Goods_ConveRatio", DbType.Int32, model.Goods_ConveRatio);
            db.AddInParameter(dbCommand, "Goods_ConveRatio_Unit_Name", DbType.String, model.Goods_ConveRatio_Unit_Name);
            db.AddInParameter(dbCommand, "Goods_ConveRatio_Unit", DbType.String, model.Goods_ConveRatio_Unit);
            db.AddInParameter(dbCommand, "Goods_Package_Material", DbType.String, model.Goods_Package_Material);
            db.AddInParameter(dbCommand, "Goods_Package_Material_Name", DbType.String, model.Goods_Package_Material_Name);
            db.AddInParameter(dbCommand, "Goods_Pcs", DbType.Int32, model.Goods_Pcs);
            db.AddInParameter(dbCommand, "Goods_Pcs_Small", DbType.Int32, model.Goods_Pcs_Small);
            db.AddInParameter(dbCommand, "drug_sensitive", DbType.Boolean, model.drug_sensitive);
            db.AddInParameter(dbCommand, "spid", DbType.AnsiString, model.spid);
            db.AddInParameter(dbCommand, "Product_Advertisement", DbType.String, model.Product_Advertisement);
            db.AddInParameter(dbCommand, "Product_SellingPoint", DbType.String, model.Product_SellingPoint);
            db.AddInParameter(dbCommand, "DrugsBase_ProName", DbType.String, model.DrugsBase_ProName);
            db.AddInParameter(dbCommand, "Image", DbType.String, model.Image);
            db.AddInParameter(dbCommand, "Original", DbType.String, model.Original);
            db.AddInParameter(dbCommand, "Goods_Unit", DbType.String, model.Goods_Unit);
            db.AddInParameter(dbCommand, "DrugsBase_SimpeCode", DbType.String, model.DrugsBase_SimpeCode);
            db.AddInParameter(dbCommand, "DrugsBase_Specification", DbType.String, model.DrugsBase_Specification);
            db.AddInParameter(dbCommand, "RetailPrice", DbType.Double, model.RetailPrice);
            db.AddInParameter(dbCommand, "LimitPrice", DbType.Double, model.LimitPrice);
            int rows = db.ExecuteNonQuery(dbCommand);
            if (!Library.Lang.DataValidator.isNumber(model.Goods_Package_ID)) model.Goods_Package_ID = 0;
            if (!Library.Lang.DataValidator.isNumber(model.Goods_ID)) model.Goods_ID = 0;
            if (model.Goods_ID == 0)
            {
                new SOSOshop.BLL.Service.Goods().AddGoods(model);
            }
            else if (model.Goods_Package_ID == 0)
            {
                new SOSOshop.BLL.Service.Goods_Package().AddGoods_Package(model);
            }
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
