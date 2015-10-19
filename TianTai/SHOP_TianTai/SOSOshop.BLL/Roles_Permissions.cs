using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace SOSOshop.BLL
{
    public class Roles_Permissions : Db
    {
        public Roles_Permissions()
        {
            ChangeHope.DataBase.SQLServerHelper.connectionString = new SOSOshop.BLL.Db()._db.ConnectionString;
        }
        #region "DataBase Operation"
        /// <summary>
        /// 在数据库中新增一个持久化对象,自增长列id的值会自动从数据库中返回
        /// </summary>
        /// <remarks></remarks>
        public int Create(Model.Roles_Permissions model)
        {
            string sequel = "IF NOT EXISTS(SELECT TOP(1) * FROM yxs_roles_permissions WHERE [id]=@ID and [operatecode]=@OperateCode) Insert into [yxs_roles_permissions](";
            sequel = sequel + "[id],[operatecode])";
            sequel = sequel + "Values(";
            sequel = sequel + "@ID,@OperateCode) Select @@rowcount ";

            DbCommand dbCommand = db.GetSqlStringCommand(sequel.ToString());
            db.AddInParameter(dbCommand, "ID", DbType.AnsiString, model.ID);
            db.AddInParameter(dbCommand, "OperateCode", DbType.AnsiString, model.OperateCode);
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
            return cmdresult;
        }

        public int Add(List<Model.Roles_Permissions> list)
        {
            int reInt = 0;
            StringBuilder sb = new StringBuilder();
            sb.Append("IF NOT EXISTS(SELECT TOP(1) * FROM yxs_roles_permissions WHERE [id]=@ID and [operatecode]=@OperateCode) Insert into [yxs_roles_permissions]");
            sb.Append("([id],[operatecode])");
            sb.Append("Values");
            sb.Append("(@ID,@OperateCode) Select @@rowcount ");
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@ID", SqlDbType.Int);
            parameters[1] = new SqlParameter("@OperateCode", SqlDbType.Int);
            SqlConnection sqlConn = ChangeHope.DataBase.SQLServerHelper.Connection;
            try
            {
                SqlTransaction sqlTran;
                sqlTran = sqlConn.BeginTransaction("AmdinRoles");
                foreach (Model.Roles_Permissions rModel in list)
                {
                    if (sqlTran.Connection == null)
                    {
                        sqlTran = null;
                        break;
                    }
                    parameters[0].Value = rModel.ID;
                    parameters[1].Value = rModel.OperateCode;
                    try
                    {
                        int result = ChangeHope.DataBase.SQLServerHelper.ExecuteNonQuery(sqlTran, sb.ToString(), parameters);
                        if (result < 1)
                            sqlTran.Rollback();
                        else
                            reInt++;
                    }
                    catch
                    {
                        if (sqlTran != null)
                        {
                            sqlTran.Rollback();
                            sqlTran.Dispose();
                        }
                        sqlTran.Dispose();
                    }
                }
                try
                {
                    sqlTran.Commit();
                }
                catch
                {
                    if (sqlTran != null)
                    {
                        sqlTran.Rollback();
                        sqlTran.Dispose();
                    }
                }
            }
            finally
            {
                sqlConn.Close();
                sqlConn.Dispose();
            }
            return reInt;
        }
        /// <summary>
        /// 删除数据库中指定的持久化对象的数据, 并提供事务支持
        /// </summary>
        /// <remarks></remarks>
        public int Delete(int id)
        {
            string sequel = string.Empty;
            sequel = "Delete From [yxs_roles_permissions]" + this.UpdateWhereSequel;
            DbCommand dbCommand = db.GetSqlStringCommand(sequel.ToString());
            db.AddInParameter(dbCommand, "ID", DbType.AnsiString, id);
            return db.ExecuteNonQuery(dbCommand);
        }
        public int Del(List<Model.Roles_Permissions> list)
        {
            int reInt = 0;
            StringBuilder sb = new StringBuilder();
            sb.Append("Delete From [yxs_roles_permissions]");
            sb.Append(" Where ");
            sb.Append("[id]=@ID and [operatecode]=@OperateCode");
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@ID", SqlDbType.Int);
            parameters[1] = new SqlParameter("@OperateCode", SqlDbType.Int);
            SqlConnection sqlConn = ChangeHope.DataBase.SQLServerHelper.Connection;
            try
            {
                SqlTransaction sqlTran;
                sqlTran = sqlConn.BeginTransaction("AmdinRoles");
                foreach (Model.Roles_Permissions rModel in list)
                {
                    if (sqlTran.Connection == null)
                    {
                        sqlTran = null;
                        break;
                    }
                    parameters[0].Value = rModel.ID;
                    parameters[1].Value = rModel.OperateCode;
                    try
                    {
                        int result = ChangeHope.DataBase.SQLServerHelper.ExecuteNonQuery(sqlTran, sb.ToString(), parameters);
                        if (result < 1)
                            sqlTran.Rollback();
                        else
                            reInt++;
                    }
                    catch
                    {
                        if (sqlTran != null)
                        {
                            sqlTran.Rollback();
                            sqlTran.Dispose();
                        }
                    }
                }
                try
                {
                    sqlTran.Commit();//执行事务
                }
                catch
                {
                    if (sqlTran != null)
                    {
                        sqlTran.Rollback();
                        sqlTran.Dispose();
                    }
                }
            }
            finally
            {
                sqlConn.Close();
                sqlConn.Dispose();
            }
            return reInt;
        }
        #endregion

        #region "Data Load"
        /// <summary>
        /// 返回数据
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public Model.Roles_Permissions GetModel(System.Data.DataRow row)
        {
            Model.Roles_Permissions model = new Model.Roles_Permissions();
            if (row != null)
            {
                model.ID = int.Parse(row["id"].ToString());
                model.OperateCode = int.Parse(row["operatecode"].ToString());
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// ID查询
        /// </summary>
        /// <remarks></remarks>
        public Model.Roles_Permissions GetModelID(int id)
        {
            string sequel = SelectSequel + UpdateWhereSequel;
            DbCommand dbCommand = db.GetSqlStringCommand(sequel.ToString());
            db.AddInParameter(dbCommand, "ID", DbType.AnsiString, id);
            DataSet ds = db.ExecuteDataSet(dbCommand);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return GetModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 装载指定列的值等于指定值的的所有持久性Roles_Permissions对象到集合,一字段查询
        /// </summary>
        /// <remarks></remarks>
        public List<Model.Roles_Permissions> GetListByColumn(string Condition)
        {
            string sequel = (new Roles_Permissions()).SelectSequel + " Where " + Condition + "";
            DbCommand dbCommand = db.GetSqlStringCommand(sequel.ToString());
            DataSet ds = db.ExecuteDataSet(dbCommand);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return LoadListFromDataTable(ds.Tables[0].DefaultView);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 装载对象Roles_Permissions的所有持久性对象到集合，如果数据量大，调用此方法会造成性能上的问题，请谨慎使用
        /// </summary>
        /// <remarks></remarks>
        public List<Model.Roles_Permissions> GetAll()
        {
            DataTable dt = ChangeHope.DataBase.SQLServerHelper.Query((new Roles_Permissions()).SelectSequel).Tables[0];
            return LoadListFromDataTable(dt.DefaultView);
        }
        /// <summary>
        /// 装载指定列的值等于指定值的的所有持久性Roles_Permissions对象到集合,一字段查询
        /// </summary>
        /// <remarks></remarks>
        public List<Model.Roles_Permissions> GetListByColumn(string columnName, Object value)
        {
            string sequel = (new Roles_Permissions()).SelectSequel + " Where [" + columnName + "] =@Value order by operatecode";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@Value", value) };
            DataTable dt = ChangeHope.DataBase.SQLServerHelper.Query(sequel, paras).Tables[0];
            return LoadListFromDataTable(dt.DefaultView);
        }
        /// <summary>
        /// 从DataView装载持久性Roles_Permissions对象到集合
        /// </summary>
        /// <remarks></remarks>
        protected List<Model.Roles_Permissions> LoadListFromDataTable(DataView dv)
        {
            List<Model.Roles_Permissions> list = new List<Model.Roles_Permissions>();
            for (int index = 0; index <= dv.Count - 1; index++)
            {
                list.Add(new Model.Roles_Permissions(dv[index].Row));
            }
            return list;
        }
        #endregion

        #region "Other function"
        string selectSequel = string.Empty;
        /// <summary>
        /// 该数据访问对象从数据库中提取数据的Sql语句 
        /// </summary>
        /// <remarks></remarks>
        protected string SelectSequel
        {
            get
            {
                if (selectSequel == string.Empty)
                    selectSequel = "Select Distinct	[id],[operatecode], 1 PersistStatus  From [yxs_roles_permissions] ";
                return selectSequel;
            }
            set
            {
                this.selectSequel = value;
            }
        }
        protected string UpdateWhereSequel
        {
            get
            {
                return " Where [id] = @ID";
            }
        }
        #endregion
    }
}
