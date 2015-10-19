using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace SOSOshop.BLL
{
    /// <summary>
    /// 数据访问类ShopSetting。
    /// </summary>
    public class ShopSetting : Db
    {
        public ShopSetting()
        { }
        #region  成员方法

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.ShopSetting model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update yxs_ShopSetting set ");
            strSql.Append("AllowVisitorBuy=@AllowVisitorBuy,");
            strSql.Append("Coupon=@Coupon,");
            strSql.Append("Thumbnails=@Thumbnails,");
            strSql.Append("WaterMark=@WaterMark,");
            strSql.Append("NumberLimit=@NumberLimit,");
            strSql.Append("OrdersReceive=@OrdersReceive,");
            strSql.Append("OrdersText=@OrdersText,");
            strSql.Append("Tel=@Tel,");
            strSql.Append("Adress=@Adress,");
            strSql.Append("Zip=@Zip,");
            strSql.Append("allowgroupbuydeposit=@allowgroupbuydeposit,");
            strSql.Append("allowauctiondeposit=@allowauctiondeposit");
            strSql.Append(" where ShopId=@ShopId ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ShopId", DbType.Int32, model.ShopId);
            db.AddInParameter(dbCommand, "AllowVisitorBuy", DbType.Int32, model.AllowVisitorBuy);
            db.AddInParameter(dbCommand, "Coupon", DbType.Int32, model.Coupon);
            db.AddInParameter(dbCommand, "Thumbnails", DbType.Int32, model.Thumbnails);
            db.AddInParameter(dbCommand, "WaterMark", DbType.Int32, model.WaterMark);
            db.AddInParameter(dbCommand, "NumberLimit", DbType.Int32, model.NumberLimit);
            db.AddInParameter(dbCommand, "OrdersReceive", DbType.Int32, model.OrdersReceive);
            db.AddInParameter(dbCommand, "OrdersText", DbType.AnsiString, model.OrdersText);
            db.AddInParameter(dbCommand, "Tel", DbType.AnsiString, model.Tel);
            db.AddInParameter(dbCommand, "Adress", DbType.AnsiString, model.Adress);
            db.AddInParameter(dbCommand, "Zip", DbType.AnsiString, model.Zip);
            db.AddInParameter(dbCommand, "allowgroupbuydeposit", DbType.Int32, model.AllowGroupbuyDeposit);
            db.AddInParameter(dbCommand, "allowauctiondeposit", DbType.Int32, model.AllowAuctionDeposit);

            return 0 < db.ExecuteNonQuery(dbCommand);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.ShopSetting GetModel()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ShopId,AllowVisitorBuy,Coupon,Thumbnails,WaterMark,NumberLimit,OrdersReceive,OrdersText,Tel,Adress,Zip,allowgroupbuydeposit,allowauctiondeposit from yxs_ShopSetting ");
            
            Model.ShopSetting model = new Model.ShopSetting();
            DataSet ds = ExecuteDataSet(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ShopId"].ToString() != "")
                {
                    model.ShopId = int.Parse(ds.Tables[0].Rows[0]["ShopId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AllowVisitorBuy"].ToString() != "")
                {
                    model.AllowVisitorBuy = int.Parse(ds.Tables[0].Rows[0]["AllowVisitorBuy"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Coupon"].ToString() != "")
                {
                    model.Coupon = int.Parse(ds.Tables[0].Rows[0]["Coupon"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Thumbnails"].ToString() != "")
                {
                    model.Thumbnails = int.Parse(ds.Tables[0].Rows[0]["Thumbnails"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WaterMark"].ToString() != "")
                {
                    model.WaterMark = int.Parse(ds.Tables[0].Rows[0]["WaterMark"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NumberLimit"].ToString() != "")
                {
                    model.NumberLimit = int.Parse(ds.Tables[0].Rows[0]["NumberLimit"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrdersReceive"].ToString() != "")
                {
                    model.OrdersReceive = int.Parse(ds.Tables[0].Rows[0]["OrdersReceive"].ToString());
                }
                model.OrdersText = ds.Tables[0].Rows[0]["OrdersText"].ToString();
                model.Tel = ds.Tables[0].Rows[0]["Tel"].ToString();
                model.Adress = ds.Tables[0].Rows[0]["Adress"].ToString();
                model.Zip = ds.Tables[0].Rows[0]["Zip"].ToString();
                if (ds.Tables[0].Rows[0]["allowgroupbuydeposit"].ToString() != "")
                {
                    model.AllowGroupbuyDeposit = int.Parse(ds.Tables[0].Rows[0]["allowgroupbuydeposit"].ToString());
                }
                if (ds.Tables[0].Rows[0]["allowauctiondeposit"].ToString() != "")
                {
                    model.AllowAuctionDeposit = int.Parse(ds.Tables[0].Rows[0]["allowauctiondeposit"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        #endregion  成员方法
    }
}
