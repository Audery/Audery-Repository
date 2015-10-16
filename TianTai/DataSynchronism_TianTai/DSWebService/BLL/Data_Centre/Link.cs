using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Data.Common;

namespace DSWebService.BLL.Data_Centre
{
    public class Link : DbBase
    {
        public Link()
        {
            base.ChangeDBData_Centre();
        }

        public int GetSpid(int id, int iden)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Link where id=@id and iden=@iden");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "id", DbType.Int32, id);
            db.AddInParameter(dbCommand, "iden", DbType.Int32, iden);
            DataTable obj = db.ExecuteDataSet(dbCommand).Tables[0];
            if (obj.Rows.Count > 0)
            {
                return Convert.ToInt32(obj.Rows[0]["spid"]);
            }
            return 0;
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
        public DataTable GetList101(int PageSize, int PageIndex, bool order, string orderField, bool like, string whereField, string whereString, out int recordCount, out int pageCount, string extStirng)
        {
            base.ChangeDBShop();
            string sql = " 1=1";
            if (extStirng != null)
            {
                sql += extStirng;
            }
            return base.GetList("product_online_v", sql, PageSize, PageIndex, order, orderField, like, whereField, whereString, out recordCount, out pageCount);
        }
        public DataTable GetList1012(int PageSize, int PageIndex, bool order, string orderField, bool like, string whereField, string whereString, out int recordCount, out int pageCount, string extStirng)
        {
            base.ChangeDBShop();
            string sql = " DrugsBase_bStop=0 and Goods_bStop=0 and Goods_Package_bStop=0 ";
            if (extStirng != null)
            {
                sql += extStirng;
            }
            return base.GetList("_ViewDrugsBaseAndGoods", sql, PageSize, PageIndex, order, orderField, like, whereField, whereString, out recordCount, out pageCount);
        }
        /// <summary>
        /// 取得全部数据
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="order"></param>
        /// <param name="orderField"></param>
        /// <param name="like"></param>
        /// <param name="whereField"></param>
        /// <param name="whereString"></param>
        /// <param name="recordCount"></param>
        /// <param name="pageCount"></param>
        /// <param name="extStirng"></param>
        /// <returns></returns>
        public DataTable GetList1013(int PageSize, int PageIndex, bool order, string orderField, bool like, string whereField, string whereString, out int recordCount, out int pageCount, string extStirng)
        {
            base.ChangeDBShop();
            string sql = " 1=1 ";
            if (extStirng != null)
            {
                sql += extStirng;
            }
            return base.GetList("Product", sql, PageSize, PageIndex, order, orderField, like, whereField, whereString, out recordCount, out pageCount);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id, string t_id, int iden)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Link where id=@id and t_id=@t_id and iden=@iden");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "id", DbType.Int32, id);
            db.AddInParameter(dbCommand, "t_id", DbType.String, t_id);
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
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 判断此合作厂家是否映射过
        /// </summary>
        public bool Exists(int id, int iden)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Link where id=@id and iden=@iden");
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
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 增加一条数据 返回是否默认货源
        /// </summary>
        public bool Add(DSWebService.Model.Data_Centre.Link model)
        {
            BLL.DbBase db1 = new DbBase();
            db1.ChangeDBShop();
            //如果选择的数据是有件装的，则直接映射
            string sql = "SELECT Goods_Pcs+Goods_Pcs_Small FROM dbo.Goods_Package WHERE Goods_Package_ID=" + model.id;
            object ret = db1.ExecuteScalar(sql);

            if ((ret==null?0:(int)ret) > 2)
            {
                goto label1;
            }
            sql = string.Format("SELECT TOP 1 Goods_Pcs,Goods_Pcs_Small FROM dbo.Product WHERE id='{0}' AND iden={1}", model.t_id, model.iden);
            #region 添加转件装中包装
            var dt = ExecuteTable(sql);
            int Goods_Pcs = (int)dt.Rows[0]["Goods_Pcs"];
            int Goods_Pcs_Small = (int)dt.Rows[0]["Goods_Pcs_Small"];
            if ((Goods_Pcs + Goods_Pcs_Small) > 2)
            {
                db1.ChangeDBShop();
                sql = string.Format("SELECT  Goods_Pcs,Goods_Pcs_Small,Goods_ID,DrugsBase_ID,Goods_Unit_ID FROM Goods_Package WHERE Goods_Package_ID={0}", model.id);
                var dr = db1.ExecuteTable(sql).Rows[0];
                int Goods_Pcs1 = (int)dr["Goods_Pcs"];
                int Goods_Pcs_Small1 = (int)dr["Goods_Pcs_Small"];
                if (Goods_Pcs1 == Goods_Pcs && Goods_Pcs_Small1 == Goods_Pcs_Small)
                {
                    goto label1;
                }
                if ((Goods_Pcs1 + Goods_Pcs_Small1) < 3)
                {
                    //更新中包装，件装
                    sql = string.Format("UPDATE Goods_Package SET Goods_Pcs={1},Goods_Pcs_Small={2} WHERE Goods_Package_ID={0}", model.id, Goods_Pcs, Goods_Pcs_Small);
                    db1.ExecuteNonQuery(sql);
                    goto label1;
                }
                if (Goods_Pcs1 == Goods_Pcs && Goods_Pcs_Small1 < 2)
                {
                    //更新中包装
                    sql = string.Format("UPDATE Goods_Package SET Goods_Pcs_Small={1} WHERE Goods_Package_ID={0}", model.id, Goods_Pcs_Small);
                    db1.ExecuteNonQuery(sql);
                    goto label1;
                }
                if (Goods_Pcs1 == Goods_Pcs && Goods_Pcs_Small1 != Goods_Pcs_Small)
                {
                    goto label2;
                }
                if (Goods_Pcs1 != Goods_Pcs && Goods_Pcs_Small1 != Goods_Pcs_Small)
                {
                    goto label2;
                }
            label2:
                //增加新的包装，件装
                sql = string.Format("SELECT Goods_Package_ID FROM dbo.Goods_Package WHERE Goods_ID={0} AND Goods_Pcs={1} AND Goods_Pcs_Small={2}", dr["Goods_ID"], Goods_Pcs, Goods_Pcs_Small);
                object o = db1.ExecuteScalar(sql);
                if (Library.Lang.DataValidator.isNumber(o))
                {
                    model.id = int.Parse(o.ToString());
                    goto label1;
                }
                sql = string.Format("SELECT TOP 1 Goods_Package_ID FROM dbo.Goods_Package WHERE Goods_ID={0} AND Goods_Pcs={1} AND Goods_Pcs_Small<2", dr["Goods_ID"], Goods_Pcs, 0);
                o = db1.ExecuteScalar(sql);
                if (Library.Lang.DataValidator.isNumber(o))
                {
                    sql = string.Format("UPDATE Goods_Package SET Goods_Pcs_Small={2} WHERE Goods_ID={0} AND Goods_Pcs={1} AND Goods_Pcs_Small<2", dr["Goods_ID"], Goods_Pcs, Goods_Pcs_Small);
                    db1.ExecuteNonQuery(sql);
                    model.id = int.Parse(o.ToString());
                    goto label1;
                }
                sql = string.Format("INSERT INTO Goods_Package(DrugsBase_ID,Goods_ID,Goods_Unit_ID,Goods_Pcs,Goods_Pcs_Small) VALUES({0},{1},{2},{3},{4});SELECT @@IDENTITY", dr["DrugsBase_ID"], dr["Goods_ID"], dr["Goods_Unit_ID"], Goods_Pcs, Goods_Pcs_Small);
                model.id = int.Parse(db1.ExecuteScalar(sql).ToString());
            }
            #endregion
        label1:

            object spid = ExecuteScalar(string.Format("SELECT id FROM Product_DEF WHERE Product_id='{0}' and iden='{1}'", model.t_id, model.iden));
            if (!Library.Lang.DataValidator.isNumber(spid))
            {
                new BLL.Data_Centre.Product_Centre().IdenData(model.iden);
            }
            spid = ExecuteScalar(string.Format("SELECT id FROM Product_DEF WHERE Product_id='{0}' and iden='{1}'", model.t_id, model.iden));
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Link(");
            strSql.Append("id,spid,t_id,iden,created,updated,is_default)");

            strSql.Append(" values (");
            strSql.Append("@id,@spid,@t_id,@iden,@created,@updated,@is_default)");
            model.is_default = false;
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "id", DbType.Int32, model.id);
            db.AddInParameter(dbCommand, "spid", DbType.String, spid);
            db.AddInParameter(dbCommand, "t_id", DbType.String, model.t_id);
            db.AddInParameter(dbCommand, "iden", DbType.Int32, model.iden);            
            db.AddInParameter(dbCommand, "created", DbType.DateTime, model.created);
            db.AddInParameter(dbCommand, "updated", DbType.DateTime, model.updated);
            db.AddInParameter(dbCommand, "is_default", DbType.Boolean, model.is_default);
            db.ExecuteNonQuery(dbCommand);
            return model.is_default;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DSWebService.Model.Data_Centre.Link model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Link set ");
            strSql.Append("spid=@spid,");
            strSql.Append("t_id=@t_id,");
            strSql.Append("iden=@iden,");        
            strSql.Append("created=@created,");
            strSql.Append("updated=@updated,");
            strSql.Append("is_default=@is_default");
            strSql.Append(" where id=@id ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "id", DbType.Int32, model.id);
            db.AddInParameter(dbCommand, "spid", DbType.String, model.spid);
            db.AddInParameter(dbCommand, "t_id", DbType.String, model.t_id);
            db.AddInParameter(dbCommand, "iden", DbType.Int32, model.iden);           
            db.AddInParameter(dbCommand, "created", DbType.DateTime, model.created);
            db.AddInParameter(dbCommand, "updated", DbType.DateTime, model.updated);
            db.AddInParameter(dbCommand, "is_default", DbType.Boolean, model.is_default);
            int rows = db.ExecuteNonQuery(dbCommand);

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