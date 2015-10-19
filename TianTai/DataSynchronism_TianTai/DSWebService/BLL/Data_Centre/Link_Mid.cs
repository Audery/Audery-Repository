using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Data.Common;

namespace DSWebService.BLL.Data_Centre
{
    /// <summary>
    /// 货源管理2.0新增 数据标准化方法
    /// </summary>
    public class Link_Mid : DbBase
    {
        public Link_Mid()
        {
            base.ChangeDBData_Centre();
        }
        public DataTable Data_CentreGetList(int PageSize, int PageIndex, bool order, string orderField, bool like, string whereField, string whereString, out int recordCount, out int pageCount, string extStirng, string tablename)
        {
            string sql = " 1=1";
            if (extStirng != null)
            {
                sql += extStirng;
            }
            return base.GetList(tablename, sql, PageSize, PageIndex, order, orderField, like, whereField, whereString, out recordCount, out pageCount);
        }

        public Model.Data_Centre.Link_Mid GetModel(int id,int iden)
        {
            DataTable dt=base.ExecuteTable(string.Format("select * from link_mid where id={0} and iden={1}",id,iden));
            Model.Data_Centre.Link_Mid m = new Model.Data_Centre.Link_Mid();
            foreach (DataRow r in dt.Rows)
            {
                m.id = id;
                m.iden = iden;
                m.Sum = (int)r["sum"];
                m.PriceType = (int)r["pricetype"];
                m.StockType = (int)r["stocktype"];
                m.Created = (DateTime)r["created"];
                m.Updated = (DateTime)r["updated"];
            }
            return m;
        }

        //public DataTable GetList101(int PageSize, int PageIndex, bool order, string orderField, bool like, string whereField, string whereString, out int recordCount, out int pageCount, string extStirng)
        //{
        //    base.ChangeDBShop();
        //    string sql = " 1=1";
        //    if (extStirng != null)
        //    {
        //        sql += extStirng;
        //    }
        //    return base.GetList("product_online_v", sql, PageSize, PageIndex, order, orderField, like, whereField, whereString, out recordCount, out pageCount);
        //}
        //public DataTable GetList1012(int PageSize, int PageIndex, bool order, string orderField, bool like, string whereField, string whereString, out int recordCount, out int pageCount, string extStirng)
        //{
        //    base.ChangeDBShop();
        //    string sql = " 1=1 ";
        //    if (extStirng != null)
        //    {
        //        sql += extStirng;
        //    }
        //    return base.GetList("product_online_v_1", sql, PageSize, PageIndex, order, orderField, like, whereField, whereString, out recordCount, out pageCount);
        //}
        ///// <summary>
        ///// 取得全部数据
        ///// </summary>
        ///// <param name="PageSize"></param>
        ///// <param name="PageIndex"></param>
        ///// <param name="order"></param>
        ///// <param name="orderField"></param>
        ///// <param name="like"></param>
        ///// <param name="whereField"></param>
        ///// <param name="whereString"></param>
        ///// <param name="recordCount"></param>
        ///// <param name="pageCount"></param>
        ///// <param name="extStirng"></param>
        ///// <returns></returns>
        //public DataTable GetList1013(int PageSize, int PageIndex, bool order, string orderField, bool like, string whereField, string whereString, out int recordCount, out int pageCount, string extStirng)
        //{
        //    base.ChangeDBShop();
        //    string sql = " 1=1 ";
        //    if (extStirng != null)
        //    {
        //        sql += extStirng;
        //    }
        //    return base.GetList("product_online_v_Data_Centre", sql, PageSize, PageIndex, order, orderField, like, whereField, whereString, out recordCount, out pageCount);
        //}
       
        /// <summary>
        /// 判断是否存在数据
        /// </summary>
        public bool Exists(int id, int iden)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Link_Mid where id=@id and iden=@iden");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "id", DbType.Int32, id);
            db.AddInParameter(dbCommand, "iden", DbType.Int32, iden);
            int cmdresult;
            object obj = db.ExecuteScalar(dbCommand);
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            return cmdresult == 0 ? false : true;
            
        }
        /// <summary>
        /// 增加一条数据 返回是否成功
        /// </summary>
        public bool Add(DSWebService.Model.Data_Centre.Link_Mid model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Link_Mid(");
            strSql.Append("id,iden,sum,StockType,PriceType)");

            strSql.Append(" values (");
            strSql.Append("@id,@iden,@sum,@StockType,@PriceType)");            
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "id", DbType.Int32, model.id);
            db.AddInParameter(dbCommand, "iden", DbType.Int32, model.iden);
            db.AddInParameter(dbCommand, "Sum", DbType.Decimal, model.Sum);
            db.AddInParameter(dbCommand, "StockType", DbType.Decimal, model.StockType);
            db.AddInParameter(dbCommand, "PriceType", DbType.Decimal, model.PriceType);
            int x=db.ExecuteNonQuery(dbCommand);
            return x > 0 ? true : false;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DSWebService.Model.Data_Centre.Link_Mid model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Link_Mid set ");
            strSql.Append("iden=@iden,");
            strSql.Append("Sum=@Sum,");
            strSql.Append("StockType=@StockType,");
            strSql.Append("PriceType=@PriceType,");
            strSql.Append("updated=@updated");
            strSql.Append(" where id=@id and iden=@iden ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "id", DbType.Int32, model.id);
            db.AddInParameter(dbCommand, "iden", DbType.Int32, model.iden);
            db.AddInParameter(dbCommand, "Sum", DbType.Decimal, model.Sum);
            db.AddInParameter(dbCommand, "StockType", DbType.Decimal, model.StockType);
            db.AddInParameter(dbCommand, "PriceType", DbType.Decimal, model.PriceType);
            db.AddInParameter(dbCommand, "updated", DbType.DateTime, model.Updated);
            int rows = db.ExecuteNonQuery(dbCommand);
            return rows > 0 ? true : false;
        }

        public static string GetUnit(int unit)
        {
            switch (unit)
            {
                case 1:
                    return "最小单位";
                case 2:
                    return "中包装";
                case 3:
                    return "件装";
            }
            return "";
        }

    }
}