using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SOSOshop.BLL
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public class Role : Db
    {
        public Role()
        {
            ChangeHope.DataBase.SQLServerHelper.connectionString = new SOSOshop.BLL.Db()._db.ConnectionString;
        }
        #region "DataBase Operation"
        /// <summary>
        /// 在数据库中新增一个持久化对象,自增长列User_ID的值会自动从数据库中返回
        /// </summary>
        /// <remarks></remarks>
        public int Create(Model.Role model)
        {
            string sequel = "Insert into [yxs_role](";
            sequel = sequel + "[name],[description])";
            sequel = sequel + "Values(";
            sequel = sequel + "@Name,@Description) Select scope_IDENTITY() ";
            SqlParameter[] paras = (SqlParameter[])this.ValueParas(model);
            object obj = ChangeHope.DataBase.SQLServerHelper.ExecuteSql(sequel, paras);
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
        /// 删除数据库中指定的持久化对象的数据, 并提供事务支持
        /// </summary>
        /// <remarks></remarks>
        public int Delete(int id)
        {
            string sequel = string.Empty;
            sequel = "Delete From [yxs_role]" + this.UpdateWhereSequel;
            SqlParameter[] parameters = (SqlParameter[])this.UpdateParas(id);
            object obj = ChangeHope.DataBase.SQLServerHelper.ExecuteSql(sequel, parameters);
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
        /// 将一个持久化对象的修改保存到数据库, 并提供事务支持
        /// </summary>
        /// <remarks></remarks>
        public int Amend(Model.Role model)
        {
            string sequel = "Update [yxs_role] set ";
            sequel = sequel + "[name] =@Name , [description] =@Description";
            sequel = sequel + UpdateWhereSequel;
            object obj = ChangeHope.DataBase.SQLServerHelper.ExecuteSql(sequel, (SqlParameter[])this.ValueParas(model));
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        #endregion

        #region "Data Load"
        /// <summary>
        /// 返回数据
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public Model.Role GetModel(System.Data.DataRow row)
        {
            Model.Role model = new Model.Role();
            if (row != null)
            {
                model.ID = int.Parse(row["id"].ToString());
                model.Name = row["name"].ToString();
                model.Description = row["description"].ToString();
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
        public Model.Role GetModelID(int id)
        {
            string sequel = SelectSequel + UpdateWhereSequel;
            SqlParameter[] paras = (SqlParameter[])this.UpdateParas(id);
            DataSet ds = ChangeHope.DataBase.SQLServerHelper.Query(sequel, paras);
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
        /// 所有记录
        /// </summary>
        /// <remarks></remarks>
        public ChangeHope.DataBase.DataByPage GetList()
        {
            ChangeHope.DataBase.DataByPage dataPage = new ChangeHope.DataBase.DataByPage();
            dataPage.Sql = "[select] * [from] yxs_role [where] 1=1 [order by] id asc";
            dataPage.GetRecordSetByPage();
            return dataPage;
        }
        /// <summary>
        /// 装载指定列的值等于指定值的的所有持久性Role对象到集合,一字段查询
        /// </summary>
        /// <remarks></remarks>
        public DataTable GetListByColumn(string columnName, Object value)
        {
            string sequel = (new Role()).SelectSequel + " Where [" + columnName + "] =@Value ";
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@Value", value) };
            DataTable dt = ChangeHope.DataBase.SQLServerHelper.Query(sequel, paras).Tables[0];
            return dt;
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
                    selectSequel = "Select	[id],[name],[description], 1 PersistStatus  From [yxs_role] ";
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
                return " Where [id] = @id";
            }
        }
        /// <summary>
        /// 该数据访问对象的属性值装载到数据库更新参数数组
        /// </summary>
        /// <remarks></remarks>
        public IDbDataParameter[] ValueParas(Model.Role model)
        {
            SqlParameter[] paras = new SqlParameter[3];
            paras[0] = new SqlParameter("@ID", model.ID);
            paras[1] = new SqlParameter("@Name", model.Name == string.Empty ? DBNull.Value : (object)model.Name);
            paras[2] = new SqlParameter("@Description", model.Description == string.Empty ? DBNull.Value : (object)model.Description);

            paras[0].DbType = DbType.Int32;
            paras[1].DbType = DbType.String;
            paras[2].DbType = DbType.String;

            return paras;


        }
        public IDbDataParameter[] UpdateParas(int id)
        {
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@id", (object)id);
            paras[0].DbType = DbType.Int32;
            return paras;
        }
        #endregion
    }
}
