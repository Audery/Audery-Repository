using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace SOSOshop.BLL.Service
{
    /// <summary>
    /// 如果超然新增加件装，则写如基础数据
    /// </summary>
    internal class Goods_Package
    {
        Database db = DatabaseFactory.CreateDatabase("ConnectionStringBase");
        Database db2 = DatabaseFactory.CreateDatabase("ConnectionString");
        /// <summary>
        /// 添加新的件装并返回传状ID
        /// </summary>
        /// <param name="model"></param>
        public void AddGoods_Package(SOSOshop.Model.Service.product model)
        {

            int Goods_Package_ID = Exists((int)model.Goods_ID, (int)model.Goods_Pcs);
            if (Goods_Package_ID == 0)
            {
                Goods_Package_ID = Exists_1((int)model.Goods_ID, (int)model.Goods_Pcs);
            }
            if (Goods_Package_ID == 0)
            {
                SOSOshop.Model.DrugsBase.Goods_Package mod = new Model.DrugsBase.Goods_Package();
                mod.DrugsBase_ID = (int)model.DrugsBase_ID;
                mod.Goods_ID = (int)model.Goods_ID;
                mod.Goods_Package_Material = "";
                mod.Goods_Package_Material_Name = "";
                if (model.Goods_Pcs != null)
                {
                    mod.Goods_Pcs = (int)model.Goods_Pcs;
                }
                else
                {
                    mod.Goods_Pcs = 1;
                }
                mod.Goods_Pcs_Small = (int)model.Goods_Pcs_Small;
                mod.Goods_Unit_ID = 0;
                Goods_Package_ID = Add(mod);
            }
            else//如果存在则更新中包装
            {
                string sqlstr = string.Format("UPDATE dbo.Goods_Package SET Goods_Pcs_Small={0} WHERE Goods_Package_ID={1}", model.Goods_Pcs_Small, Goods_Package_ID);
                db.ExecuteNonQuery(db.GetSqlStringCommand(sqlstr));
            }
            string sql = "UPDATE Product SET Goods_Package_ID=" + Goods_Package_ID + " WHERE spid='" + model.spid + "'";
            db2.ExecuteNonQuery(db2.GetSqlStringCommand(sql));
            Database db3 = DatabaseFactory.CreateDatabase("ConnectionStringERP");
            sql = "disable trigger spzl_trigger_update on spzl;UPDATE dbo.spzl SET goods_package_id=" + Goods_Package_ID + " WHERE spid='" + model.spid + "';enable trigger spzl_trigger_update on spzl;";
            db3.ExecuteNonQuery(db3.GetSqlStringCommand(sql));
        }
        /// <summary>
        /// 是否存在该记录如果存在返回记录ID不存在返回0
        /// </summary>
        private int Exists(int Goods_ID, int Goods_Pcs)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Goods_Package_ID from Goods_Package where Goods_ID=@Goods_ID and  Goods_Pcs=@Goods_Pcs");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Goods_ID", DbType.Int32, Goods_ID);
            db.AddInParameter(dbCommand, "Goods_Pcs", DbType.Int32, Goods_Pcs);
            object obj = db.ExecuteScalar(dbCommand);
            if (obj == null)
            {
                return 0;
            }
            return (int)obj;
        }
        /// <summary>
        /// 是否存在件装是1的，如果存在，则将件装为1的更新成新增加的
        /// </summary>
        private int Exists_1(int Goods_ID, int Goods_Pcs)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Goods_Package_ID from Goods_Package where Goods_ID=@Goods_ID and  Goods_Pcs=@Goods_Pcs");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Goods_ID", DbType.Int32, Goods_ID);
            db.AddInParameter(dbCommand, "Goods_Pcs", DbType.Int32, 1);
            object obj = db.ExecuteScalar(dbCommand);
            if (obj == null)
            {
                return 0;
            }
            string sql = string.Format("UPDATE dbo.Goods_Package SET Goods_Pcs={0} WHERE Goods_Package_ID={1}", Goods_Pcs, obj);
            db.ExecuteNonQuery(db.GetSqlStringCommand(sql));
            return (int)obj;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        private int Add(SOSOshop.Model.DrugsBase.Goods_Package model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Goods_Package(");
            strSql.Append("DrugsBase_ID,Goods_ID,Goods_Unit_ID,Goods_Package_Material,Goods_Package_Material_Name,Goods_Pcs,Goods_Pcs_Small)");

            strSql.Append(" values (");
            strSql.Append("@DrugsBase_ID,@Goods_ID,@Goods_Unit_ID,@Goods_Package_Material,@Goods_Package_Material_Name,@Goods_Pcs,@Goods_Pcs_Small)");
            strSql.Append(";select @@IDENTITY");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "DrugsBase_ID", DbType.Int32, model.DrugsBase_ID);
            db.AddInParameter(dbCommand, "Goods_ID", DbType.Int32, model.Goods_ID);
            db.AddInParameter(dbCommand, "Goods_Unit_ID", DbType.Int32, model.Goods_Unit_ID);
            db.AddInParameter(dbCommand, "Goods_Package_Material", DbType.String, model.Goods_Package_Material);
            db.AddInParameter(dbCommand, "Goods_Package_Material_Name", DbType.String, model.Goods_Package_Material_Name);
            db.AddInParameter(dbCommand, "Goods_Pcs", DbType.Int32, model.Goods_Pcs);
            db.AddInParameter(dbCommand, "Goods_Pcs_Small", DbType.Int32, model.Goods_Pcs_Small);
            int result;
            object obj = db.ExecuteScalar(dbCommand);
            if (!int.TryParse(obj.ToString(), out result))
            {
                return 0;
            }
            return result;
        }
    }
}
