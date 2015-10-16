using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DSWebService.BLL.Data_Centre
{
    /// <summary>
    /// 处理合同企业的批号库存信息
    /// </summary>
    public class Product_Centre : DbBase
    {
        Memcached.ClientLibrary.MemcachedClient mc;
        public Product_Centre()
        {
            mc = new Memcached.ClientLibrary.MemcachedClient();
            mc.PoolName = "Price_Cache";
        }
        /// <summary>
        /// 更新同步数据
        /// </summary>
        /// <param name="iden"></param>
        public void InitializeData(int iden)
        {
            string centredb = System.Configuration.ConfigurationManager.AppSettings["centredb"];
            //是否自动上架
            int autoProduct_bShelves = int.Parse(System.Configuration.ConfigurationManager.AppSettings["autoProduct_bShelves"]);
            long start = Environment.TickCount;
            base.ChangeDBShop();
            //基本信息
            string sql = string.Format(@"UPDATE Product SET DrugsBase_Manufacturer=a.DrugsBase_Manufacturer FROM [_ViewDrugsBase] a WITH(NOLOCK) INNER JOIN Product b WITH(NOLOCK) ON a.DrugsBase_ID = b.DrugsBase_ID WHERE a.DrugsBase_Manufacturer<>b.DrugsBase_Manufacturer
                                  UPDATE Product SET DrugsBase_DrugName=a.DrugsBase_DrugName FROM [_ViewDrugsBase] a WITH(NOLOCK) INNER JOIN Product b WITH(NOLOCK) ON a.DrugsBase_ID = b.DrugsBase_ID WHERE a.DrugsBase_DrugName<>b.DrugsBase_DrugName
                                  UPDATE Product SET DrugsBase_ProName=a.DrugsBase_ProName FROM [_ViewDrugsBase] a WITH(NOLOCK) INNER JOIN Product b WITH(NOLOCK) ON a.DrugsBase_ID = b.DrugsBase_ID WHERE a.DrugsBase_ProName<>b.DrugsBase_ProName
                                  UPDATE Product SET DrugsBase_ApprovalNumber=a.DrugsBase_ApprovalNumber FROM [_ViewDrugsBase] a WITH(NOLOCK) INNER JOIN Product b WITH(NOLOCK) ON a.DrugsBase_ID = b.DrugsBase_ID WHERE a.DrugsBase_ApprovalNumber<>b.DrugsBase_ApprovalNumber
                                  UPDATE Product SET DrugsBase_Specification=a.DrugsBase_Specification FROM [_ViewDrugsBase] a WITH(NOLOCK) INNER JOIN Product b WITH(NOLOCK) ON a.DrugsBase_ID = b.DrugsBase_ID WHERE a.DrugsBase_Specification<>b.DrugsBase_Specification
                                  UPDATE Product SET DrugsBase_SimpeCode=a.DrugsBase_SimpeCode FROM [_ViewDrugsBase] a WITH(NOLOCK) INNER JOIN Product b WITH(NOLOCK) ON a.DrugsBase_ID = b.DrugsBase_ID WHERE a.DrugsBase_SimpeCode<>b.DrugsBase_SimpeCode
                                  UPDATE Product SET Product_Name=a.DrugsBase_DrugName FROM [_ViewDrugsBase] a WITH(NOLOCK) INNER JOIN Product b WITH(NOLOCK) ON a.DrugsBase_ID = b.DrugsBase_ID WHERE a.DrugsBase_DrugName<>b.Product_Name", iden);
            base.ExecuteNonQuery(sql);


            //中包装，件装
            sql = string.Format("UPDATE dbo.Product SET Goods_Pcs=b.Goods_Pcs,Goods_Pcs_Small=b.Goods_Pcs_Small FROM Product a INNER JOIN Goods_Package b ON a.Goods_Package_ID=b.Goods_Package_ID WHERE a.Goods_Pcs<>b.Goods_Pcs OR  a.Goods_Pcs_Small<>b.Goods_Pcs_Small", iden);
            base.ExecuteNonQuery(sql);
            //单位
            sql = string.Format("UPDATE Product SET Goods_Unit=a.Goods_Unit FROM dbo.[_ViewDrugsBaseAndGoods] a WITH(NOLOCK) INNER JOIN Product b WITH(NOLOCK) ON a.Goods_ID = b.Goods_ID WHERE a.Goods_Unit<>b.Goods_Unit", iden);
            base.ExecuteNonQuery(sql);
            sql = string.Format("UPDATE Product SET Goods_ConveRatio_Unit=a.Goods_ConveRatio_Unit FROM dbo.[_ViewDrugsBaseAndGoods] a WITH(NOLOCK) INNER JOIN Product b WITH(NOLOCK) ON a.Goods_ID = b.Goods_ID WHERE a.Goods_ConveRatio_Unit<>b.Goods_ConveRatio_Unit", iden);
            base.ExecuteNonQuery(sql);
            sql = string.Format("UPDATE Product SET Goods_ConveRatio_Unit_Name=a.Goods_ConveRatio_Unit_Name FROM dbo.[_ViewDrugsBaseAndGoods] a WITH(NOLOCK) INNER JOIN Product b WITH(NOLOCK) ON a.Goods_ID = b.Goods_ID WHERE a.Goods_ConveRatio_Unit_Name<>b.Goods_ConveRatio_Unit_Name", iden);
            base.ExecuteNonQuery(sql);
            //转换比
            sql = string.Format("UPDATE Product SET Goods_ConveRatio=a.Goods_ConveRatio FROM dbo.[_ViewDrugsBaseAndGoods] a WITH(NOLOCK) INNER JOIN Product b WITH(NOLOCK) ON a.Goods_ID = b.Goods_ID WHERE a.Goods_ConveRatio<>b.Goods_ConveRatio", iden);
            base.ExecuteNonQuery(sql);

            base.ChangeDBShop();

            #region 公司数据同步到平台
            sql = string.Format(@"Insert    Product(Product_ID,
											Goods_ID,
											DrugsBase_ID,
											Goods_Package_ID,
											Product_Name,											
											Product_bShelves,
											beactive,
											Product_SaleNum,
											Product_KJZS,
											DrugsBase_ApprovalNumber,
											Registration_No,
											DrugsBase_Manufacturer,
											Manufacturer_Short,
											DrugsBase_DrugName,
											DrugsBase_ProName,
											DrugsBase_Formulation,
											DrugsBase_Specification,
											DrugsBase_SimpeCode,
											Goods_ConveRatio,
											Goods_ConveRatio_Unit_Name,
											Goods_ConveRatio_Unit,
											Goods_Package_Material,
											Goods_Package_Material_Name,
											Goods_Pcs,
											Product_Advertisement,
											Goods_Pcs_Small,
											tag_ids,
											Product_SellingPoint,
											Goods_Unit,
											Image,
											Original,
											Product_ClickNum,
											Stock,
											Product_State,
											spid,
											minsell,
											maxsell,
											is_cl,
											pihao,
											sxrq,
											RetailPrice,
											LimitPrice,
											BidPrice,
											drug_sensitive,
											created,
											iden,
                                            jigid)
											SELECT 
											spid Product_ID,
											b.Goods_ID,
											b.DrugsBase_ID,
											b.product_id Goods_Package_ID,
											b.DrugsBase_DrugName Product_Name,											
											{1} Product_bShelves,
											'是' beactive,
											0 Product_SaleNum,
											1 Product_KJZS,
											ISNULL(DrugsBase_ApprovalNumber,'') DrugsBase_ApprovalNumber,
											ISNULL(Registration_No,'') Registration_No,
											DrugsBase_Manufacturer,
											DrugsBase_Manufacturer Manufacturer_Short,
											DrugsBase_DrugName,
											ISNULL(DrugsBase_ProName,'')DrugsBase_ProName,
											isnull(DrugsBase_Formulation,'')DrugsBase_Formulation,
											isnull(DrugsBase_Specification,'')DrugsBase_Specification,
											DrugsBase_SimpeCode,
											Goods_ConveRatio,
											Goods_ConveRatio_Unit_Name,
											Goods_ConveRatio_Unit,
											Goods_Package_Material,
											Goods_Package_Material_Name,
											Goods_Pcs,'' Product_Advertisement,
											Goods_Pcs_Small,
											ISNULL((SELECT TOP 1 Tag_Ids FROM DrugsBase_Tag_Ids WHERE DrugsBase_Id=b.DrugsBase_ID),'') tag_ids,
											'' Product_SellingPoint,
											ISNULL(Goods_Unit,'')[Goods_Unit],
											(SELECT TOP (1) Image FROM dbo.Goods_Image AS Goods_Image_1 WHERE (Goods_ID = b.Goods_ID))Image,
											(SELECT TOP (1) Original FROM dbo.Goods_Image WHERE (Goods_ID = b.Goods_ID))Original,
											0 Product_ClickNum,
											(SELECT ISNULL(SUM(Stock),0) FROM [{2}].dbo.View_Stock1 WHERE iden={0} AND id=a.t_id)*ISNULL((select sum FROM [{2}].dbo.Link_Mid WHERE id=a.id AND iden={0} AND StockType<>1),1)Stock,
											'' Product_State,
											t_id spid,
											1 minsell,
											0 maxsell,
											'是' is_cl,
											isnull((SELECT TOP 1 pihao FROM [{2}].dbo.View_Stock1 WHERE iden={0} AND id=a.t_id ORDER BY sxrq ASC),'')pihao,
											isnull( (SELECT TOP 1 sxrq FROM [{2}].dbo.View_Stock1 WHERE iden={0} AND id=a.t_id ORDER BY sxrq ASC),'')sxrq,
											0 RetailPrice,
											0 LimitPrice,
											0 BidPrice,
											0 drug_sensitive,
											created,
											{0} iden,
                                             0 jigid
											 FROM [{2}].dbo.Link a INNER JOIN _ViewDrugsBaseAndGoods b ON a.id=b.product_id WHERE iden={0} AND a.spid NOT IN 
											(
												SELECT Product_ID FROM Product
											)", iden, autoProduct_bShelves, centredb);
            ExecuteNonQuery(sql, 60 * 10);
            #endregion

            #region 取消映射的品种
            sql = string.Format("UPDATE Product SET Goods_Package_ID=0,Goods_ID=0,DrugsBase_ID=0 WHERE  Product_ID NOT IN (SELECT spid FROM [{1}].dbo.Link WHERE iden={0})", iden, centredb);
            ExecuteNonQuery(sql);
            #endregion

            #region 更新重新映射的产品数据
            sql = string.Format("UPDATE Product SET Goods_Package_ID=b.id,DrugsBase_ID=c.DrugsBase_ID,Goods_ID=c.Goods_ID,spid=b.t_id FROM Product a INNER JOIN {1}.dbo.Link b ON a.Product_ID=b.spid INNER JOIN Goods_Package c ON b.id=c.Goods_Package_ID WHERE a.Goods_Package_ID<>b.id", iden, centredb);
            ExecuteNonQuery(sql);
            #endregion

            //处理经营范围
            sql = string.Format("UPDATE Product SET BussinessScopeCode=b.BusinessScope FROM dbo.Product a INNER JOIN {0}.dbo.Product b ON a.spid=b.ID WHERE a.BussinessScopeCode<>b.BusinessScope", centredb);
            ExecuteNonQuery(sql);

            #region 处理库存变化
            base.ChangeDBData_Centre();
            sql = string.Format(@"SELECT a.pihao,a.Stock,a.sxrq,b.id FROM View_Stock1 a INNER JOIN dbo.Product_DEF b ON a.id = b.Product_id AND a.iden=b.iden where a.iden={0} except  SELECT a.pihao,a.Stock,a.sxrq,b.id FROM  View_Stock1_Temp a INNER JOIN dbo.Product_DEF b ON a.id = b.Product_id AND a.iden=b.iden where a.iden={0} UNION ALL SELECT a.pihao,a.Stock,a.sxrq,b.id FROM View_Stock1_Temp a INNER JOIN dbo.Product_DEF b ON a.id = b.Product_id WHERE a.iden={0} AND a.id NOT IN (SELECT id FROM View_Stock1 WHERE iden={0});
                                  --删除已经未过来的商品库存
                                  DELETE View_Stock1 WHERE id NOT IN (SELECT ID FROM dbo.Product WHERE iden={0}) AND dbo.View_Stock1.iden={0};", iden);
            DataTable dt = base.ExecuteTable(sql, 60 * 10);
            int l2 = dt.Rows.Count;
            //库存变化单条处理
            foreach (var item in dt.AsEnumerable().Select(x => x.Field<int>("id")).Distinct())
            {
                UpdateStock(item, iden);
            }
            sql = string.Format("DELETE View_Stock1_Temp WHERE iden={0};INSERT View_Stock1_Temp SELECT * FROM View_Stock1 WHERE iden={0}", iden);
            base.ExecuteNonQuery(sql);

            #endregion

            long end = Environment.TickCount;
            AddLog("执行了商品数据处理,企业编号:" + iden + ",共执行" + (end - start) + "毫秒!", 1);
        }

        /// <summary>
        /// 处理数据
        /// </summary>
        /// <param name="iden"></param>
        public void IdenData(int iden)
        {
            long start = Environment.TickCount;
            //给数据预分配编号
            string sql = string.Format(@"INSERT INTO Product_DEF (Product_id,iden)
	                            SELECT id,iden FROM dbo.Product WHERE iden={0} AND ID NOT IN (
	                            SELECT Product_id FROM Product_DEF WHERE iden={0}
                            )", iden);
            base.ChangeDBData_Centre();
            ExecuteNonQuery(sql);
            long end = Environment.TickCount;
            AddLog("1_执行了县级公司数据编号,企业编号:" + iden + ",共执行" + (end - start) + "毫秒!", 1);
        }


        /// <summary>
        /// 同步未建档的品种至平台
        /// </summary>
        /// <param name="iden"></param>
        public void InitializeDataNotfiling(int iden)
        {
            string centredb = System.Configuration.ConfigurationManager.AppSettings["centredb"];
            ChangeDBShop();
            string sql = string.Format(@"Insert Product(Product_ID,
											Goods_ID,
											DrugsBase_ID,
											Goods_Package_ID,
											Product_Name,
											Price_01,
											Price_02,
                                            Price_03,
											Product_bShelves,
											beactive,
											Product_SaleNum,
											Product_KJZS,
											DrugsBase_ApprovalNumber,
											Registration_No,
											DrugsBase_Manufacturer,
											Manufacturer_Short,
											DrugsBase_DrugName,
											DrugsBase_ProName,
											DrugsBase_Formulation,
											DrugsBase_Specification,
											DrugsBase_SimpeCode,
											Goods_ConveRatio,
											Goods_ConveRatio_Unit_Name,
											Goods_ConveRatio_Unit,
											Goods_Package_Material,
											Goods_Package_Material_Name,
											Goods_Pcs,
											Product_Advertisement,
											Goods_Pcs_Small,
											tag_ids,
											Product_SellingPoint,
											Goods_Unit,
											Image,
											Original,
											Product_ClickNum,
											Stock,
											Product_State,
											spid,
											minsell,
											maxsell,
											is_cl,
											pihao,
											sxrq,
											RetailPrice,
											LimitPrice,
											BidPrice,
											drug_sensitive,
											created,
											iden,
                                            jigid)
SELECT 
											b.id Product_ID,
											0 Goods_ID,
											0 DrugsBase_ID,
											0  Goods_Package_ID,
											DrugsBase_DrugName Product_Name,
											0 Price_01,
											0 Price_02,
                                            0 Price_03,
											1 Product_bShelves,
											'是' beactive,
											0 Product_SaleNum,
											1 Product_KJZS,
										    isnull(DrugsBase_ApprovalNumber,'')DrugsBase_ApprovalNumber,
											'' Registration_No,
											DrugsBase_Manufacturer,
											DrugsBase_Manufacturer Manufacturer_Short,
											DrugsBase_DrugName,
											DrugsBase_DrugName DrugsBase_ProName,
											isnull(DrugsBase_Formulation,'')DrugsBase_Formulation,
											isnull(DrugsBase_Specification,'')DrugsBase_Specification,
											dbo.fun_getPY(RTRIM(DrugsBase_DrugName)) DrugsBase_SimpeCode,
											Goods_ConveRatio,
											'' Goods_ConveRatio_Unit_Name,
											'' Goods_ConveRatio_Unit,
											'' Goods_Package_Material,
											'' Goods_Package_Material_Name,
											Goods_Pcs,'' Product_Advertisement,
											Goods_Pcs_Small,
											'' tag_ids,
											'' Product_SellingPoint,
											ISNULL(Goods_Unit,'')[Goods_Unit],
											'' Image,
											'' Original,
											0 Product_ClickNum,
											(SELECT ISNULL(SUM(Stock),0) FROM [{1}].dbo.View_Stock1 WHERE iden={0} AND id=a.id)Stock,
											'' Product_State,
											a.id spid,
											1 minsell,
											0 maxsell,
											'是' is_cl,
											(SELECT TOP 1 pihao FROM [{1}].dbo.View_Stock1 WHERE iden={0} AND id=a.id ORDER BY sxrq ASC)pihao,
											(SELECT TOP 1 sxrq FROM [{1}].dbo.View_Stock1 WHERE iden={0} AND id=a.id ORDER BY sxrq ASC)sxrq,
											0 RetailPrice,
											0 LimitPrice,
											0 BidPrice,
											0 drug_sensitive,
											created,
											{0} iden,
                                             0 jigid
										     FROM {1}.dbo.Product a INNER JOIN {1}.dbo.Product_DEF b ON a.id=b.product_id and a.iden=b.iden WHERE a.iden={0} AND b.id NOT IN 
											(
												SELECT Product_ID FROM Product
											) and a.id in (select id from {1}.dbo.View_Stock1 where Stock>0 and iden={0})", iden, centredb);
            ExecuteNonQuery(sql, 60 * 10);

            //给映射了的数据建立关系
            sql = string.Format("UPDATE Product SET Goods_ID=c.Goods_ID,DrugsBase_ID=c.DrugsBase_ID,Goods_Package_ID=c.product_id FROM  Product a INNER JOIN {0}.dbo.Link b ON a.Product_ID=b.spid INNER JOIN _ViewDrugsBaseAndGoods c ON b.id=c.product_id WHERE a.Goods_Package_ID=0", centredb);
            ExecuteNonQuery(sql);
            //清除没有库存的品种的库存
            sql = string.Format("UPDATE Product SET Stock=0 WHERE Stock>0 AND spid not IN (select ID FROM {0}.dbo.View_Stock1 WHERE Stock>0)", centredb);
            ExecuteNonQuery(sql, 60 * 3);
        }

        /// <summary>
        /// 更新库存数量
        /// </summary>
        /// <param name="spid"></param>
        /// <param name="iden"></param>
        public void UpdateStock(int spid, int iden)
        {

            string sql = string.Format(@"SELECT pihao,sxrq,(SELECT ISNULL(SUM(Stock),0) FROM dbo.View_Stock1 WHERE iden=v.iden AND id=v.id)Stock,ISNULL((SELECT sum FROM dbo.Link_Mid WHERE iden=v.iden AND id=(SELECT id FROM dbo.Link  WHERE iden={0} AND spid={1}) AND StockType>1),1)sum FROM dbo.View_Stock1 v WHERE iden={0} AND id=((SELECT Product_id FROM dbo.Product_DEF  WHERE id={1})) ORDER BY sxrq ASC", iden, spid);

            DataTable dt = ExecuteTable(sql, 120);
            if (dt.Rows.Count > 0)
            {
                decimal sum = 0;
                decimal.TryParse(dt.Rows[0]["sum"].ToString(), out sum);
                if (sum > 1)
                {
                    dt.Rows[0]["Stock"] = Convert.ToDecimal(sum * Convert.ToDecimal(dt.Rows[0]["Stock"].ToString()));
                }
                BLL.DbBase bll = new DbBase();
                bll.ChangeDBShop();
                //按库存含义更新库存数量
                sql = string.Format("UPDATE Product SET Stock={2},pihao='{3}',sxrq='{4}' WHERE Product_ID={0}", spid, iden, dt.Rows[0]["Stock"], dt.Rows[0]["pihao"], dt.Rows[0]["sxrq"]);
                bll.ExecuteNonQuery(sql);
            }

        }
        /// <summary>
        /// 每天执行一次更新公共数据变化(包装盒,标签)
        /// </summary>
        public void UpdatePuclic()
        {
            ChangeDBShop();
            DSWebService.BLL.Data_Centre.Config con = new DSWebService.BLL.Data_Centre.Config();
            string sql = "";
            foreach (var item in con.GetAllList())
            {
                sql = string.Format("UPDATE Product SET tag_ids=b.Tag_Ids FROM Product a INNER JOIN DrugsBase_Tag_Ids b ON a.DrugsBase_ID = b.DrugsBase_Id AND a.iden={0} WHERE a.tag_ids<>b.Tag_Ids", item.id);
                ExecuteNonQuery(sql);

                sql = string.Format(@"UPDATE Product SET Image=a1.Image FROM 
                                      (
	                                    SELECT Product_ID,(SELECT TOP (1) Image FROM Goods_Image AS Goods_Image_1 WHERE (Goods_ID = b.Goods_ID))Image FROM Product b
                                      )a1 INNER JOIN Product b ON a1.Product_ID = b.Product_ID  AND b.iden={0} AND a1.Image <> b.Image", item.id);
                ExecuteNonQuery(sql);

                sql = string.Format(@"UPDATE Product SET Original=a1.Original FROM 
                                     (
	                                     SELECT Product_ID,(SELECT TOP (1) Original FROM Goods_Image AS Goods_Image_1 WHERE (Goods_ID = b.Goods_ID))Original FROM Product b
                                     )a1 INNER JOIN Product b ON a1.Product_ID = b.Product_ID AND b.iden={0} AND a1.Original <> b.Original", item.id);
                ExecuteNonQuery(sql);


            }
        }
        /// <summary>
        /// 初始化库存(每天晚上执行一次，纠正错误的库存)
        /// </summary>
        public void initStock()
        {
            string sql = @" TRUNCATE TABLE Stock1
                            INSERT INTO Stock1 (id,iden,Stock)
                            SELECT id,iden,SUM(Stock)Stock FROM View_Stock1 GROUP BY id,iden
                            DECLARE @@tt TABLE(id VARCHAR(20),stock DECIMAL )
                            insert into @@tt select a1.id,a1.Stock*b1.sum Stock  from  Stock1 a1 INNER JOIN 
                            (
	                            SELECT a.id,a.iden,c.sum FROM Stock1 a INNER JOIN dbo.Link b ON a.id=b.t_id AND a.iden=b.iden
	                            INNER JOIN Link_Mid c ON b.id=c.id AND b.iden=c.iden WHERE c.StockType<>1
                            ) b1 ON a1.id=b1.id AND a1.iden=b1.iden
                            UPDATE Stock1 SET Stock=b2.Stock FROM Stock1 a1 INNER JOIN @@tt b2 ON a1.id=b2.id where a1.Stock<>b2.Stock";
            DbBase db = new DbBase();
            db.ChangeDBData_Centre();
            db.ExecuteNonQuery(sql, 60 * 10);            
            db.ChangeDBShop();
            sql = string.Format("UPDATE dbo.Product SET Stock=b.Stock FROM Product a INNER JOIN {0}.dbo.Stock1 b ON a.spid=b.id WHERE a.Stock<>b.Stock", System.Configuration.ConfigurationManager.AppSettings["centredb"]);
            db.ExecuteNonQuery(sql);            
        }
        public static void AddLog(string msg, int type)
        {
            BLL.Log bll = new BLL.Log();
            bll.created = DateTime.Now;
            bll.describe = msg;
            bll.detail = "";
            if (HttpContext.Current == null)
            {
                bll.ip = "local";
                bll.source = "AutoSupplyManage_YSPT";
            }
            else
            {
                bll.ip = HttpContext.Current.Request.UserHostAddress;
                bll.source = HttpContext.Current.Request.Url.ToString();
            }
            bll.type = type;
            bll.insert(bll);
        }
    }
}