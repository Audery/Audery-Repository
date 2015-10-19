using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace SOSOshop.BLL.DrugsBase
{
    public class Goods
    {
        /// <summary>
        /// 是否存在该记录（如果存在相同转换比怎么处理，明天和超然沟通）
        /// </summary>
        public bool Exists(int Goods_ID)
        {
            Database db = DatabaseFactory.CreateDatabase("ConnectionStringBase");
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Goods where Goods_ID=@Goods_ID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Goods_ID", DbType.Int32, Goods_ID);
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
        public int Add(SOSOshop.Model.DrugsBase.Goods model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Goods(");
            strSql.Append("DrugsBase_ID,Goods_ConveRatio,Goods_ConveRatio_Unit_ID,Goods_ConveRatio_Unit_Name)");

            strSql.Append(" values (");
            strSql.Append("@DrugsBase_ID,@Goods_ConveRatio,@Goods_ConveRatio_Unit_ID,@Goods_ConveRatio_Unit_Name)");
            strSql.Append(";select @@IDENTITY");
            Database db = DatabaseFactory.CreateDatabase("ConnectionStringBase");
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
