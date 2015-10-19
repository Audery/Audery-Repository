using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace SOSOshop.BLL.DrugsBase
{
    public class Goods_Package
    {
        public bool Exists(int Goods_Package_ID)
        {
            Database db = DatabaseFactory.CreateDatabase("ConnectionStringBase");
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Goods_Package where Goods_Package_ID=@Goods_Package_ID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Goods_Package_ID", DbType.Int32, Goods_Package_ID);
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
        /// 增加一条数据
        /// </summary>
        public int Add(SOSOshop.Model.DrugsBase.Goods_Package model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Goods_Package(");
            strSql.Append("DrugsBase_ID,Goods_ID,Goods_Unit_ID,Goods_Package_Material,Goods_Package_Material_Name,Goods_Pcs,Goods_Pcs_Small)");

            strSql.Append(" values (");
            strSql.Append("@DrugsBase_ID,@Goods_ID,@Goods_Unit_ID,@Goods_Package_Material,@Goods_Package_Material_Name,@Goods_Pcs,@Goods_Pcs_Small)");
            strSql.Append(";select @@IDENTITY");
            Database db = DatabaseFactory.CreateDatabase("ConnectionStringBase");
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
