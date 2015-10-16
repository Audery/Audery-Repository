using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using Library.Data;

namespace SOSOshop.BLL
{
    /// <summary>
    /// 资质数据库
    /// </summary>    
    public partial class Qualifications : Db
    {
        public Qualifications() { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Qualifications where ID=@ID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ID", DbType.Int32, ID);
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
        public int Add(SOSOshop.Model.Qualifications model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Qualifications(");
            strSql.Append("DrugsBase_Enterprise_ID,Name,Attachment,Classify,ExpiryDate,AlertDate,IsTransition,IsYearCheck,Remark,Ceated)");

            strSql.Append(" values (");
            strSql.Append("@DrugsBase_Enterprise_ID,@Name,@Attachment,@Classify,@ExpiryDate,@AlertDate,@IsTransition,@IsYearCheck,@Remark,@Ceated)");
            strSql.Append(";select @@IDENTITY");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "DrugsBase_Enterprise_ID", DbType.Int32, model.DrugsBase_Enterprise_ID);
            db.AddInParameter(dbCommand, "Name", DbType.Int32, model.Name);
            db.AddInParameter(dbCommand, "Attachment", DbType.String, model.Attachment);
            db.AddInParameter(dbCommand, "Classify", DbType.String, model.Classify);
            db.AddInParameter(dbCommand, "ExpiryDate", DbType.Int32, model.ExpiryDate);
            db.AddInParameter(dbCommand, "AlertDate", DbType.String, model.AlertDate);
            db.AddInParameter(dbCommand, "IsTransition", DbType.Boolean, model.IsTransition);
            db.AddInParameter(dbCommand, "IsYearCheck", DbType.Boolean, model.IsYearCheck);
            db.AddInParameter(dbCommand, "Remark", DbType.String, model.Remark);
            db.AddInParameter(dbCommand, "Ceated", DbType.DateTime, model.Ceated);
            int result;
            object obj = db.ExecuteScalar(dbCommand);
            if (!int.TryParse(obj.ToString(), out result))
            {
                return 0;
            }
            return result;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SOSOshop.Model.Qualifications model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Qualifications set ");
            strSql.Append("DrugsBase_Enterprise_ID=@DrugsBase_Enterprise_ID,");
            strSql.Append("Name=@Name,");
            strSql.Append("Attachment=@Attachment,");
            strSql.Append("Classify=@Classify,");
            strSql.Append("ExpiryDate=@ExpiryDate,");
            strSql.Append("AlertDate=@AlertDate,");
            strSql.Append("IsTransition=@IsTransition,");
            strSql.Append("IsYearCheck=@IsYearCheck,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("Ceated=@Ceated");
            strSql.Append(" where ID=@ID ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ID", DbType.Int32, model.ID);
            db.AddInParameter(dbCommand, "DrugsBase_Enterprise_ID", DbType.Int32, model.DrugsBase_Enterprise_ID);
            db.AddInParameter(dbCommand, "Name", DbType.Int32, model.Name);
            db.AddInParameter(dbCommand, "Attachment", DbType.String, model.Attachment);
            db.AddInParameter(dbCommand, "Classify", DbType.String, model.Classify);
            db.AddInParameter(dbCommand, "ExpiryDate", DbType.Int32, model.ExpiryDate);
            db.AddInParameter(dbCommand, "AlertDate", DbType.String, model.AlertDate);
            db.AddInParameter(dbCommand, "IsTransition", DbType.Boolean, model.IsTransition);
            db.AddInParameter(dbCommand, "IsYearCheck", DbType.Boolean, model.IsYearCheck);
            db.AddInParameter(dbCommand, "Remark", DbType.String, model.Remark);
            db.AddInParameter(dbCommand, "Ceated", DbType.DateTime, model.Ceated);
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

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Qualifications ");
            strSql.Append(" where ID=@ID ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ID", DbType.Int32, ID);
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
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Qualifications ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SOSOshop.Model.Qualifications GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,DrugsBase_Enterprise_ID,Name,Attachment,Classify,ExpiryDate,AlertDate,IsTransition,IsYearCheck,Remark,Ceated from Qualifications ");
            strSql.Append(" where ID=@ID ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ID", DbType.Int32, ID);
            SOSOshop.Model.Qualifications model = null;
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    model = ReaderBind(dataReader);
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,DrugsBase_Enterprise_ID,Name,Attachment,Classify,ExpiryDate,AlertDate,IsTransition,IsYearCheck,Remark,Ceated ");
            strSql.Append(" FROM Qualifications ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            return db.ExecuteDataSet(CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ID,DrugsBase_Enterprise_ID,Name,Attachment,Classify,ExpiryDate,AlertDate,IsTransition,IsYearCheck,Remark,Ceated ");
            strSql.Append(" FROM Qualifications ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);

            return db.ExecuteDataSet(CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Qualifications ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from Qualifications T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            
            DbCommand dbCommand = db.GetStoredProcCommand("UP_GetRecordByPage");
            db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "Qualifications");
            db.AddInParameter(dbCommand, "fldName", DbType.AnsiString, "ID");
            db.AddInParameter(dbCommand, "PageSize", DbType.Int32, PageSize);
            db.AddInParameter(dbCommand, "PageIndex", DbType.Int32, PageIndex);
            db.AddInParameter(dbCommand, "IsReCount", DbType.Boolean, 0);
            db.AddInParameter(dbCommand, "OrderType", DbType.Boolean, 0);
            db.AddInParameter(dbCommand, "strWhere", DbType.AnsiString, strWhere);
            return db.ExecuteDataSet(dbCommand);
        }*/

        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<SOSOshop.Model.Qualifications> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,DrugsBase_Enterprise_ID,Name,Attachment,Classify,ExpiryDate,AlertDate,IsTransition,IsYearCheck,Remark,Ceated ");
            strSql.Append(" FROM Qualifications ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<SOSOshop.Model.Qualifications> list = new List<SOSOshop.Model.Qualifications>();

            using (IDataReader dataReader = db.ExecuteReader(CommandType.Text, strSql.ToString()))
            {
                while (dataReader.Read())
                {
                    list.Add(ReaderBind(dataReader));
                }
            }
            return list;
        }


        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public SOSOshop.Model.Qualifications ReaderBind(IDataReader dataReader)
        {
            SOSOshop.Model.Qualifications model = new SOSOshop.Model.Qualifications();
            object ojb;
            ojb = dataReader["ID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ID = (int)ojb;
            }
            ojb = dataReader["DrugsBase_Enterprise_ID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.DrugsBase_Enterprise_ID = (int)ojb;
            }
            ojb = dataReader["Name"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Name = (int)ojb;
            }
            model.Attachment = dataReader["Attachment"].ToString();
            model.Classify = dataReader["Classify"].ToString();
            ojb = dataReader["ExpiryDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ExpiryDate = (int)ojb;
            }
            ojb = dataReader["AlertDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AlertDate = (DateTime)ojb;
            }
            ojb = dataReader["IsTransition"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IsTransition = (bool)ojb;
            }
            ojb = dataReader["IsYearCheck"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IsYearCheck = (bool)ojb;
            }
            model.Remark = dataReader["Remark"].ToString();
            ojb = dataReader["Ceated"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Ceated = (DateTime)ojb;
            }
            return model;
        }

        #endregion  Method
    }
}

