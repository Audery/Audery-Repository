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
    /// 数据访问类memberTradingIntention。
    /// </summary>
    public class memberTradingIntention : Db
    {
        public memberTradingIntention()
        { }
        #region  成员方法

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int UID, string DrugsBase_Name, string guige)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from memberTradingIntention");
            strSql.Append(string.Format(" where UID={0} and DrugsBase_Name='{1}' and Guige='{2}'", UID, DrugsBase_Name, guige));
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
        /// 增加一条数据
        /// </summary>
        public int Add(Model.memberTradingIntention model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into memberTradingIntention(");
            strSql.Append("UID, DrugsBase_Name, GuiGe, JiXing, QiYe,pzwh,jz, ProNum, ArrivalPeriod, Detail, AddDate, UpdateDate, State)");
            strSql.Append(" values (");
            strSql.Append("@UID, @DrugsBase_Name, @GuiGe, @JiXing, @QiYe,@pzwh,@jz, @ProNum, @ArrivalPeriod, @Detail, @AddDate, @UpdateDate, @State)");
            strSql.Append(" SELECT SCOPE_IDENTITY()");
            DateTime now = DateTime.Now;

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "UID", DbType.Int32, model.UID);
            db.AddInParameter(dbCommand, "DrugsBase_Name", DbType.String, model.DrugsBase_Name);
            db.AddInParameter(dbCommand, "GuiGe", DbType.String, model.Guige);
            db.AddInParameter(dbCommand, "JiXing", DbType.String, model.JiXing);
            db.AddInParameter(dbCommand, "QiYe", DbType.String, model.QiYe);
            db.AddInParameter(dbCommand, "pzwh", DbType.String, model.pzwh);
            db.AddInParameter(dbCommand, "jz", DbType.Int32, model.jz);
            db.AddInParameter(dbCommand, "ProNum", DbType.Int32, model.ProNum);
            db.AddInParameter(dbCommand, "ArrivalPeriod", DbType.Int32, model.ArrivalPeriod);
            db.AddInParameter(dbCommand, "Detail", DbType.String, model.Detail);
            db.AddInParameter(dbCommand, "AddDate", DbType.DateTime, now);
            db.AddInParameter(dbCommand, "UpdateDate", DbType.DateTime, now);
            db.AddInParameter(dbCommand, "State", DbType.Int32, model.State);
            object obj = db.ExecuteScalar(dbCommand);
            return obj != null ? Convert.ToInt32(obj) : 0;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.memberTradingIntention model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update memberTradingIntention set ");
            strSql.Append("UID=@UID,");
            strSql.Append("DrugsBase_Name=@DrugsBase_Name,");
            strSql.Append("Guige=@Guige,");
            strSql.Append("JiXing=@JiXing,");
            strSql.Append("QiYe=@QiYe,");
            strSql.Append("pzwh=@pzwh,");
            strSql.Append("jz=@jz,");
            strSql.Append("ProNum=@ProNum,");
            strSql.Append("ArrivalPeriod=@ArrivalPeriod,");
            strSql.Append("Detail=@Detail,");
            strSql.Append("UpdateDate=@UpdateDate,");
            strSql.Append("State=@State");
            strSql.Append(" where ID=@ID ");
            DateTime now = DateTime.Now;

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ID", DbType.Int32, model.ID);
            db.AddInParameter(dbCommand, "UID", DbType.Int32, model.UID);
            db.AddInParameter(dbCommand, "DrugsBase_Name", DbType.String, model.DrugsBase_Name);
            db.AddInParameter(dbCommand, "Guige", DbType.String, model.Guige);
            db.AddInParameter(dbCommand, "JiXing", DbType.String, model.JiXing);
            db.AddInParameter(dbCommand, "QiYe", DbType.String, model.QiYe);
            db.AddInParameter(dbCommand, "pzwh", DbType.String, model.pzwh);
            db.AddInParameter(dbCommand, "jz", DbType.Int32, model.jz);
            db.AddInParameter(dbCommand, "ProNum", DbType.Int32, model.ProNum);
            db.AddInParameter(dbCommand, "ArrivalPeriod", DbType.Int32, model.ArrivalPeriod);
            db.AddInParameter(dbCommand, "Detail", DbType.String, model.Detail);
            db.AddInParameter(dbCommand, "UpdateDate", DbType.DateTime, now);
            db.AddInParameter(dbCommand, "State", DbType.Int32, model.State);
            return 0 < db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from memberTradingIntention ");
            strSql.Append("where ID=@ID");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "ID", DbType.Int32, ID);
            return 0 < db.ExecuteNonQuery(dbCommand);
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
            string sequel = "Update memberTradingIntention set ";
            sequel += "[" + columnName + "] = @Value ";
            sequel += "Where ID = @ID";

            DbCommand dbCommand = db.GetSqlStringCommand(sequel);
            db.AddInParameter(dbCommand, "Value", DbType.AnsiString, value);
            db.AddInParameter(dbCommand, "ID", DbType.Int32, id);
            return 0 < db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.memberTradingIntention GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from memberTradingIntention ");
            strSql.Append(" where ID="+ID);
            DataSet ds = base.ExecuteDataSet(strSql.ToString());
            Model.memberTradingIntention model = null;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GetModelByDataRow(ds.Tables[0].Rows[0]);
            }
            return model;
        }
        /// <summary>
        /// 得到101商城的一个对象实体
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public List<Model.memberTradingIntention> GetModels(int UID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from memberpermission where UID="+UID);
            DataSet ds = db.ExecuteDataSet(strSql.ToString());
            return GetModelByDataSet(ds);
        }

        private List<Model.memberTradingIntention> GetModelByDataSet(DataSet ds)
        {
            List<Model.memberTradingIntention> List = new List<Model.memberTradingIntention>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    List.Add(GetModelByDataRow(dr));
                }
            }
            return List;
        }
        public Model.memberTradingIntention GetModelByDataRow(DataRow dr)
        {
            Model.memberTradingIntention model = new Model.memberTradingIntention();
            model.ID = int.Parse(dr["ID"].ToString());
            model.UID = int.Parse(dr["UID"].ToString());
            model.DrugsBase_Name = dr["DrugsBase_Name"].ToString();
            model.Guige = dr["Guige"].ToString();
            model.JiXing = dr["JiXing"].ToString();
            model.QiYe = dr["QiYe"].ToString();
            model.pzwh = dr["pzwh"].ToString();
            model.Disposition = dr["Disposition"].ToString();
            int jz = 0; int.TryParse(Convert.ToString(dr["jz"]), out jz);
            model.jz = jz;            
            int ProNum = 0; int.TryParse(Convert.ToString(dr["ProNum"]), out ProNum);
            model.ProNum = ProNum;
            int ArrivalPeriod = 0; int.TryParse(Convert.ToString(dr["ArrivalPeriod"]), out ArrivalPeriod);
            model.ArrivalPeriod = ArrivalPeriod;
            model.Detail = Convert.ToString(dr["Detail"]);
            DateTime AddDate = new DateTime(); DateTime.TryParse(Convert.ToString(dr["AddDate"]), out AddDate);
            model.AddDate = AddDate;
            DateTime UpdateDate = new DateTime(); DateTime.TryParse(Convert.ToString(dr["UpdateDate"]), out UpdateDate);
            model.UpdateDate = UpdateDate;
            int State = 0; int.TryParse(Convert.ToString(dr["State"]), out State);
            model.State = State;
            return model;
        }

        public Model.memberTradingIntention GetModelByDataReader(IDataReader dr)
        {
            Model.memberTradingIntention model = new Model.memberTradingIntention();
            model.ID = int.Parse(dr["ID"].ToString());
            model.UID = int.Parse(dr["UID"].ToString());
            model.DrugsBase_Name = dr["DrugsBase_Name"].ToString();
            model.Guige = dr["Guige"].ToString();
            model.JiXing = dr["JiXing"].ToString();
            model.QiYe = dr["QiYe"].ToString();
            model.pzwh = dr["pzwh"].ToString();
            model.Disposition = dr["Disposition"].ToString();
            int jz = 0; int.TryParse(Convert.ToString(dr["jz"]), out jz);
            model.jz = jz;            
            int ProNum = 0; int.TryParse(Convert.ToString(dr["ProNum"]), out ProNum);
            model.ProNum = ProNum;
            int ArrivalPeriod = 0; int.TryParse(Convert.ToString(dr["ArrivalPeriod"]), out ArrivalPeriod);
            model.ArrivalPeriod = ArrivalPeriod;
            model.Detail = Convert.ToString(dr["Detail"]);
            DateTime AddDate = new DateTime(); DateTime.TryParse(Convert.ToString(dr["AddDate"]), out AddDate);
            model.AddDate = AddDate;
            DateTime UpdateDate = new DateTime(); DateTime.TryParse(Convert.ToString(dr["UpdateDate"]), out UpdateDate);
            model.UpdateDate = UpdateDate;
            int State = 0; int.TryParse(Convert.ToString(dr["State"]), out State);
            model.State = State;
            return model;
        }


        /// <summary>
        /// 查所有 根据条件
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<Model.memberTradingIntention> GetAll(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select b.* from memberTradingIntention b inner join memberaccount a on a.UID=b.UID ");
            if (!string.IsNullOrEmpty(where))
            {
                strSql.Append("where " + where);
            }
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            DataSet ds = db.ExecuteDataSet(dbCommand);

            List<Model.memberTradingIntention> List = new List<Model.memberTradingIntention>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    List.Add(GetModelByDataRow(dr));
                }
            }
            return List;
        }

        #endregion  成员方法

    }
}
