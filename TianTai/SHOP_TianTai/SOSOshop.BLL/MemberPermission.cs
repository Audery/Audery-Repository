using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.Common;
using SOSOshop.Model;

namespace SOSOshop.BLL
{
    /// <summary>
    /// 数据访问类MemberPermission。
    /// </summary>
    public class MemberPermission : Db
    {
        public MemberPermission()
        { }
        #region  成员方法

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int UID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from memberaccount a inner join memberpermission c on a.UID=c.UID");
            strSql.Append(" where a.UID=" + UID);
            object obj = ExecuteScalar(strSql.ToString());
            if (obj == null)
            {
                return false;
            }
            else
            {
                return 0 < Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 是否存在该账号[包括手机号和邮箱]
        /// </summary>
        public bool Exists(string userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from memberaccount a inner join memberpermission c on a.UID=c.UID");

            strSql.Append(" where " + GetUserIdSqlParam(userId));

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserId", DbType.AnsiString, userId);
            return 0 < Convert.ToInt32(db.ExecuteScalar(dbCommand));
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.MemberPermission model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("if (not exists(select top(1) * from memberpermission where UID=@UID)) insert into memberpermission(");
            strSql.Append("UID, IsTrade, IsLookStock, IsLookPrice_01, IsLookPrice_02, IsLookProduct_01, IsLookProduct_02, IsPeriodicalSettle, IsMoneyAndShipping, IsCOD, IsPriorDistribution, IsShippingFor48h, IsSpecialTrade)");
            strSql.Append(" values (");
            strSql.Append("@UID, @IsTrade, @IsLookStock, @IsLookPrice_01, @IsLookPrice_02, @IsLookProduct_01, @IsLookProduct_02, @IsPeriodicalSettle, @IsMoneyAndShipping, @IsCOD, @IsPriorDistribution, @IsShippingFor48h, @IsSpecialTrade)");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UID", DbType.Int32, model.UID);
            db.AddInParameter(dbCommand, "IsTrade", DbType.Int32, model.IsTrade ? 1 : 0);
            db.AddInParameter(dbCommand, "IsLookStock", DbType.Int32, model.IsLookStock ? 1 : 0);
            db.AddInParameter(dbCommand, "IsLookPrice_01", DbType.Int32, model.IsLookPrice_01 ? 1 : 0);
            db.AddInParameter(dbCommand, "IsLookPrice_02", DbType.Int32, model.IsLookPrice_02 ? 1 : 0);
            db.AddInParameter(dbCommand, "IsLookProduct_01", DbType.Int32, model.IsLookProduct_01 ? 1 : 0);
            db.AddInParameter(dbCommand, "IsLookProduct_02", DbType.Int32, model.IsLookProduct_02 ? 1 : 0);
            db.AddInParameter(dbCommand, "IsPeriodicalSettle", DbType.Int32, model.IsPeriodicalSettle ? 1 : 0);
            db.AddInParameter(dbCommand, "IsMoneyAndShipping", DbType.Int32, model.IsMoneyAndShipping ? 1 : 0);
            db.AddInParameter(dbCommand, "IsCOD", DbType.Int32, model.IsCOD ? 1 : 0);
            db.AddInParameter(dbCommand, "IsPriorDistribution", DbType.Int32, model.IsPriorDistribution ? 1 : 0);
            db.AddInParameter(dbCommand, "IsShippingFor48h", DbType.Int32, model.IsShippingFor48h ? 1 : 0);
            db.AddInParameter(dbCommand, "IsSpecialTrade", DbType.Int32, model.IsSpecialTrade ? 1 : 0);
            return 0 < db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.MemberPermission model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update memberpermission set ");
            strSql.Append("IsTrade=@IsTrade,");
            strSql.Append("IsLookStock=@IsLookStock,");
            strSql.Append("IsLookPrice_01=@IsLookPrice_01,");
            strSql.Append("IsLookPrice_02=@IsLookPrice_02,");
            strSql.Append("IsLookProduct_01=@IsLookProduct_01,");
            strSql.Append("IsLookProduct_02=@IsLookProduct_02,");
            strSql.Append("IsPeriodicalSettle=@IsPeriodicalSettle,");
            strSql.Append("IsMoneyAndShipping=@IsMoneyAndShipping,");
            strSql.Append("IsCOD=@IsCOD,");
            strSql.Append("IsPriorDistribution=@IsPriorDistribution,");
            strSql.Append("IsShippingFor48h=@IsShippingFor48h,");
            strSql.Append("IsSpecialTrade=@IsSpecialTrade");
            strSql.Append(" where UID=@UID ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UID", DbType.Int32, model.UID);
            db.AddInParameter(dbCommand, "IsTrade", DbType.Int32, model.IsTrade ? 1 : 0);
            db.AddInParameter(dbCommand, "IsLookStock", DbType.Int32, model.IsLookStock ? 1 : 0);
            db.AddInParameter(dbCommand, "IsLookPrice_01", DbType.Int32, model.IsLookPrice_01 ? 1 : 0);
            db.AddInParameter(dbCommand, "IsLookPrice_02", DbType.Int32, model.IsLookPrice_02 ? 1 : 0);
            db.AddInParameter(dbCommand, "IsLookProduct_01", DbType.Int32, model.IsLookProduct_01 ? 1 : 0);
            db.AddInParameter(dbCommand, "IsLookProduct_02", DbType.Int32, model.IsLookProduct_02 ? 1 : 0);
            db.AddInParameter(dbCommand, "IsPeriodicalSettle", DbType.Int32, model.IsPeriodicalSettle ? 1 : 0);
            db.AddInParameter(dbCommand, "IsMoneyAndShipping", DbType.Int32, model.IsMoneyAndShipping ? 1 : 0);
            db.AddInParameter(dbCommand, "IsCOD", DbType.Int32, model.IsCOD ? 1 : 0);
            db.AddInParameter(dbCommand, "IsPriorDistribution", DbType.Int32, model.IsPriorDistribution ? 1 : 0);
            db.AddInParameter(dbCommand, "IsShippingFor48h", DbType.Int32, model.IsShippingFor48h ? 1 : 0);
            db.AddInParameter(dbCommand, "IsSpecialTrade", DbType.Int32, model.IsSpecialTrade ? 1 : 0);
            return 0 < db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 自动更新买家[类别 ?批发客户或OTC拆零客户]的权限
        /// </summary>
        public bool AutoUpdate(int UID, MemberKeyValue.Member_Class member_Class)
        {
            Model.MemberPermission model = GetModel(UID);
            switch (member_Class)
            {
                case MemberKeyValue.Member_Class.批发客户:
                    model.IsLookPrice_01 = true;
                    model.IsLookPrice_02 = false;
                    model.IsLookProduct_01 = true;
                    model.IsLookProduct_02 = false;
                    model.IsPriorDistribution = true;
                    model.IsMoneyAndShipping = true;
                    model.IsCOD = false;
                    break;
                case MemberKeyValue.Member_Class.OTC客户:
                    model.IsLookPrice_01 = false;
                    model.IsLookPrice_02 = true;
                    model.IsLookProduct_01 = false;
                    model.IsLookProduct_02 = true;
                    model.IsMoneyAndShipping = false;
                    model.IsCOD = true;
                    break;
            }
            return Update(model);
        }

        /// <summary>
        /// 更新任意一个字段
        /// </summary>
        /// <param name="id"></param>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Amend(int id, string columnName, object value)
        {
            string sequel = "Update memberpermission set ";
            sequel += "[" + columnName + "] = @Value ";
            sequel += "Where UID = @UID";

            DbCommand dbCommand = db.GetSqlStringCommand(sequel);
            db.AddInParameter(dbCommand, "Value", DbType.AnsiString, value);
            db.AddInParameter(dbCommand, "UID", DbType.Int32, id);
            return 0 < db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.MemberPermission GetModelWithNoCache(int UID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from memberpermission ");
            strSql.Append(" where UID=" + UID);
            DataSet ds = base.ExecuteDataSet(strSql.ToString());
            var model = GetModelByDataSet(ds);
            if (model == null)
            {
                if (model == null)
                {
                    model = new Model.MemberPermission();
                    model.IsMoneyAndShipping = true;
                    model.IsBuyFilingStatus = true;
                    model.IsMoneyAndShipping = true;
                    model.UID = UID;
                    Add(model);
                }
            }
            return model;
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.MemberPermission GetModel(int UID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from memberpermission ");
            strSql.Append(" where UID=" + UID);
            DataSet ds = base.ExecuteDataSetForCache(strSql.ToString(), "bll_MemberPermission_" + UID, DateTime.Now.AddMinutes(1));
            var model = GetModelByDataSet(ds);
            if (model == null)
            {
                model = new Model.MemberPermission();
                model.IsMoneyAndShipping = true;
                model.IsBuyFilingStatus = true;
                model.IsMoneyAndShipping = true;
            }
            return model;
        }
        /// <summary>
        /// 得到101商城的一个对象实体
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public Model.MemberPermission GetModel(string userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from memberpermission where UID = (select top(1) UID from memberaccount");

            strSql.Append(" where " + GetUserIdSqlParam(userId) + ")");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UserId", DbType.AnsiString, userId);
            DataSet ds = db.ExecuteDataSet(dbCommand);
            return GetModelByDataSet(ds);
        }

        /// <summary>
        /// 获取登陆用户名类型
        /// </summary>
        /// <param name="UserId">登陆用户名</param>
        /// <returns></returns>
        public int GetUserIdNameClass(string UserId)
        {
            return new MemberAccount().GetUserIdNameClass(UserId);
        }
        private string GetUserIdSqlParam(string UserId)
        {
            return new MemberAccount().GetUserIdSqlParam(UserId);
        }
        private Model.MemberPermission GetModelByDataSet(DataSet ds)
        {
            Model.MemberPermission model = null;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model = GetModelByDataRow(ds.Tables[0].Rows[0]);
            }
            return model;
        }
        private Model.MemberPermission GetModelByDataRow(DataRow dr)
        {
            Model.MemberPermission model = new Model.MemberPermission();
            model.UID = int.Parse(dr["UID"].ToString());
            model.IsTrade = int.Parse(dr["IsTrade"].ToString()) == 1;
            model.IsLookStock = int.Parse(dr["IsLookStock"].ToString()) == 1;
            model.IsLookPrice_01 = int.Parse(dr["IsLookPrice_01"].ToString()) == 1;
            model.IsLookPrice_02 = int.Parse(dr["IsLookPrice_02"].ToString()) == 1;
            model.IsLookProduct_01 = int.Parse(dr["IsLookProduct_01"].ToString()) == 1;
            model.IsLookProduct_02 = int.Parse(dr["IsLookProduct_02"].ToString()) == 1;
            model.IsPeriodicalSettle = int.Parse(dr["IsPeriodicalSettle"].ToString()) == 1;
            model.IsMoneyAndShipping = int.Parse(dr["IsMoneyAndShipping"].ToString()) == 1;
            model.IsCOD = int.Parse(dr["IsCOD"].ToString()) == 1;
            model.IsPriorDistribution = int.Parse(dr["IsPriorDistribution"].ToString()) == 1;
            model.IsShippingFor48h = int.Parse(dr["IsShippingFor48h"].ToString()) == 1;
            model.IsSpecialTrade = int.Parse(dr["IsSpecialTrade"].ToString()) == 1;
            model.IsBuyFilingStatus = GetBuyFilingStatus(model.UID);
            return model;
        }

        public Model.MemberPermission GetModelByDataReader(IDataReader dr)
        {
            Model.MemberPermission model = new Model.MemberPermission();
            model.UID = int.Parse(dr["UID"].ToString());
            model.IsTrade = int.Parse(dr["IsTrade"].ToString()) == 1;
            model.IsLookStock = int.Parse(dr["IsLookStock"].ToString()) == 1;
            model.IsLookPrice_01 = int.Parse(dr["IsLookPrice_01"].ToString()) == 1;
            model.IsLookPrice_02 = int.Parse(dr["IsLookPrice_02"].ToString()) == 1;
            model.IsLookProduct_01 = int.Parse(dr["IsLookProduct_01"].ToString()) == 1;
            model.IsLookProduct_02 = int.Parse(dr["IsLookProduct_02"].ToString()) == 1;
            model.IsPeriodicalSettle = int.Parse(dr["IsPeriodicalSettle"].ToString()) == 1;
            model.IsMoneyAndShipping = int.Parse(dr["IsMoneyAndShipping"].ToString()) == 1;
            model.IsCOD = int.Parse(dr["IsCOD"].ToString()) == 1;
            model.IsPriorDistribution = int.Parse(dr["IsPriorDistribution"].ToString()) == 1;
            model.IsShippingFor48h = int.Parse(dr["IsShippingFor48h"].ToString()) == 1;
            model.IsSpecialTrade = int.Parse(dr["IsSpecialTrade"].ToString()) == 1;
            model.IsBuyFilingStatus = GetBuyFilingStatus(model.UID);
            return model;
        }

        /// <summary>
        /// 用户单位GSP是否建档
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public bool GetBuyFilingStatus(int uid)
        {
            string sql = string.Format("SELECT (SELECT BuyFilingStatus FROM DrugsBase_Enterprise WHERE ID=(SELECT ParentId FROM memberinfo WHERE UID={0})) BuyFilingStatus", uid);
            SOSOshop.BLL.Db db = new Db();
            //DataTable dt = db.ExecuteTable(sql);
            DataTable dt = db.ExecuteTableForCache(sql, DateTime.Now.AddMinutes(1));
            if (dt.Rows.Count == 0) return false;
            try
            {
                return int.Parse(dt.Rows[0][0].ToString()) == 1;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// 查所有 根据条件
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<Model.MemberPermission> GetAll(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select b.* from memberpermission b inner join memberaccount a on a.UID=b.UID ");
            if (!string.IsNullOrEmpty(where))
            {
                strSql.Append("where " + where);
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            DataSet ds = db.ExecuteDataSet(dbCommand);

            List<Model.MemberPermission> permissionList = new List<Model.MemberPermission>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    permissionList.Add(GetModelByDataRow(dr));
                }
            }
            return permissionList;
        }

        #endregion  成员方法

    }
}
