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
    /// 如果超然新添加的转换比则写入基础数据平台
    /// </summary>
    internal class Goods
    {
        Database db = DatabaseFactory.CreateDatabase("ConnectionStringBase");
        Database db2 = DatabaseFactory.CreateDatabase("ConnectionString");
        /// <summary>
        /// 添加新的转无比件和件装并处理
        /// </summary>
        /// <param name="model"></param>
        public void AddGoods(SOSOshop.Model.Service.product model)
        {
            int Goods_ID = Exists((int)model.DrugsBase_ID, (int)model.Goods_ConveRatio);
            string sql = "";
            if (Goods_ID == 0)
            {
                SOSOshop.Model.DrugsBase.Goods mo = new Model.DrugsBase.Goods();
                mo.DrugsBase_ID = (int)model.DrugsBase_ID;
                mo.Goods_ConveRatio = (int)model.Goods_ConveRatio;
                int Goods_ConveRatio_Unit_ID = 0;
                object obj = db.ExecuteScalar(db.GetSqlStringCommand("SELECT Goods_ConveRatio_Unit_ID FROM dbo.Goods_ConveRatio_Unit WHERE Goods_ConveRatio_Unit='" + model.Goods_ConveRatio_Unit.Trim() + "'"));
                if (Library.Lang.DataValidator.isNumber(obj))
                {
                    Goods_ConveRatio_Unit_ID = (int)obj;
                }
                mo.Goods_ConveRatio_Unit_ID = Goods_ConveRatio_Unit_ID;
                mo.Goods_ConveRatio_Unit_Name = model.Goods_ConveRatio_Unit_Name;
                Goods_ID = mo.Goods_ID = Add(mo);
                sql = "INSERT INTO Goods_Units (Goods_ID,Goods_Unit_ID) VALUES(" + mo.Goods_ID + ",ISNULL((SELECT Goods_Unit_ID FROM dbo.Goods_Unit WHERE Goods_Unit='" + model.Goods_Unit.Trim() + "'),1))";
                db.ExecuteNonQuery(db.GetSqlStringCommand(sql));
            }
            model.Goods_ID = Goods_ID;
            sql = "UPDATE Product SET Goods_ID=" + Goods_ID + " WHERE spid='" + model.spid + "'";
            db2.ExecuteNonQuery(db2.GetSqlStringCommand(sql));
            Database db3 = DatabaseFactory.CreateDatabase("ConnectionStringERP");
            sql = "disable trigger spzl_trigger_update on spzl;UPDATE dbo.spzl SET Goods_ID=" + Goods_ID + " WHERE spid='" + model.spid + "';enable trigger spzl_trigger_update on spzl;";
            db3.ExecuteNonQuery(db3.GetSqlStringCommand(sql));
            new Goods_Package().AddGoods_Package(model);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        private int Exists(int DrugsBase_ID, int Goods_ConveRatio)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT a.Goods_ID FROM dbo.Goods a WHERE a.DrugsBase_ID=@DrugsBase_ID and a.Goods_ConveRatio=@Goods_ConveRatio");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "DrugsBase_ID", DbType.Int32, DrugsBase_ID);
            db.AddInParameter(dbCommand, "Goods_ConveRatio", DbType.Int32, Goods_ConveRatio);            
      
            object obj = db.ExecuteScalar(dbCommand);
            if (obj == null)
            {
                return 0;
            }
            return (int)obj;
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        private int Add(SOSOshop.Model.DrugsBase.Goods model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Goods(");
            strSql.Append("DrugsBase_ID,Goods_ConveRatio,Goods_ConveRatio_Unit_ID,Goods_ConveRatio_Unit_Name)");

            strSql.Append(" values (");
            strSql.Append("@DrugsBase_ID,@Goods_ConveRatio,@Goods_ConveRatio_Unit_ID,@Goods_ConveRatio_Unit_Name)");
            strSql.Append(";select @@IDENTITY");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "DrugsBase_ID", DbType.Int32, model.DrugsBase_ID);
            db.AddInParameter(dbCommand, "Goods_ConveRatio", DbType.Int32, model.Goods_ConveRatio);
            db.AddInParameter(dbCommand, "Goods_ConveRatio_Unit_ID", DbType.Int32, model.Goods_ConveRatio_Unit_ID);
            db.AddInParameter(dbCommand, "Goods_ConveRatio_Unit_Name", DbType.String, model.Goods_ConveRatio_Unit_Name);
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
