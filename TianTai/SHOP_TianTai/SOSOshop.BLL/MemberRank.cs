using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace SOSOshop.BLL
{
    /// <summary>
    /// 数据访问类MemberRank。
    /// </summary>
    public class MemberRank : Db
    {
        public MemberRank()
        {
            ChangeHope.DataBase.SQLServerHelper.connectionString = new SOSOshop.BLL.Db()._db.ConnectionString;
        }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from MemberRank");
            strSql.Append(" where [name]=@Name ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "Name", DbType.AnsiString, name);
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
        public int Add(Model.MemberRank model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MemberRank(");
            strSql.Append("Name,LogoPic,Discount,MinScore,MaxScore,Priority,IsSpecial,IsShowPrice,Article,Product,ArticleAuditing,ProductAuditing,MaxMoney,UpgradeMoney,IsUpgrade)");
            strSql.Append(" values (");
            strSql.Append("@Name,@LogoPic,@Discount,@MinScore,@MaxScore,@Priority,@IsSpecial,@IsShowPrice,@Article,@Product,@ArticleAuditing,@ProductAuditing,@MaxMoney,@UpgradeMoney,@IsUpgrade)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@LogoPic", SqlDbType.NVarChar,200),
					new SqlParameter("@Discount", SqlDbType.Float,8),
					new SqlParameter("@MinScore", SqlDbType.Float,8),
					new SqlParameter("@MaxScore", SqlDbType.Float,8),
					new SqlParameter("@Priority", SqlDbType.Int,4),
					new SqlParameter("@IsSpecial", SqlDbType.Int,4),
					new SqlParameter("@IsShowPrice", SqlDbType.Int,4),
					new SqlParameter("@Article", SqlDbType.Int,4),
					new SqlParameter("@Product", SqlDbType.Int,4),
					new SqlParameter("@ArticleAuditing", SqlDbType.Int,4),
					new SqlParameter("@ProductAuditing", SqlDbType.Int,4),
					new SqlParameter("@MaxMoney", SqlDbType.Float,8),
					new SqlParameter("@UpgradeMoney", SqlDbType.Float,8),
					new SqlParameter("@IsUpgrade", SqlDbType.Int,4)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.LogoPic;
            parameters[2].Value = model.Discount;
            parameters[3].Value = model.MinScore;
            parameters[4].Value = model.MaxScore;
            parameters[5].Value = model.Priority;
            parameters[6].Value = model.IsSpecial;
            parameters[7].Value = model.IsShowPrice;
            parameters[8].Value = model.Article;
            parameters[9].Value = model.Product;
            parameters[10].Value = model.ArticleAuditing;
            parameters[11].Value = model.ProductAuditing;
            parameters[12].Value = model.MaxMoney;
            parameters[13].Value = model.UpgradeMoney;
            parameters[14].Value = model.IsUpgrade;

            object obj = ChangeHope.DataBase.SQLServerHelper.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(Model.MemberRank model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MemberRank set ");
            strSql.Append("Name=@Name,");
            strSql.Append("LogoPic=@LogoPic,");
            strSql.Append("Discount=@Discount,");
            strSql.Append("MinScore=@MinScore,");
            strSql.Append("MaxScore=@MaxScore,");
            strSql.Append("Priority=@Priority,");
            strSql.Append("IsSpecial=@IsSpecial,");
            strSql.Append("IsShowPrice=@IsShowPrice,");
            strSql.Append("Article=@Article,");
            strSql.Append("Product=@Product,");
            strSql.Append("ArticleAuditing=@ArticleAuditing,");
            strSql.Append("ProductAuditing=@ProductAuditing,");
            strSql.Append("MaxMoney=@MaxMoney,");
            strSql.Append("UpgradeMoney=@UpgradeMoney,");
            strSql.Append("IsUpgrade=@IsUpgrade");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@LogoPic", SqlDbType.NVarChar,200),
					new SqlParameter("@Discount", SqlDbType.Float,8),
					new SqlParameter("@MinScore", SqlDbType.Float,8),
					new SqlParameter("@MaxScore", SqlDbType.Float,8),
					new SqlParameter("@Priority", SqlDbType.Int,4),
					new SqlParameter("@IsSpecial", SqlDbType.Int,4),
					new SqlParameter("@IsShowPrice", SqlDbType.Int,4),
					new SqlParameter("@Article", SqlDbType.Int,4),
					new SqlParameter("@Product", SqlDbType.Int,4),
					new SqlParameter("@ArticleAuditing", SqlDbType.Int,4),
					new SqlParameter("@ProductAuditing", SqlDbType.Int,4),
					new SqlParameter("@MaxMoney", SqlDbType.Float,8),
					new SqlParameter("@UpgradeMoney", SqlDbType.Float,8),
					new SqlParameter("@IsUpgrade", SqlDbType.Int,4)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.LogoPic;
            parameters[3].Value = model.Discount;
            parameters[4].Value = model.MinScore;
            parameters[5].Value = model.MaxScore;
            parameters[6].Value = model.Priority;
            parameters[7].Value = model.IsSpecial;
            parameters[8].Value = model.IsShowPrice;
            parameters[9].Value = model.Article;
            parameters[10].Value = model.Product;
            parameters[11].Value = model.ArticleAuditing;
            parameters[12].Value = model.ProductAuditing;
            parameters[13].Value = model.MaxMoney;
            parameters[14].Value = model.UpgradeMoney;
            parameters[15].Value = model.IsUpgrade;

            ChangeHope.DataBase.SQLServerHelper.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from MemberRank ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            ChangeHope.DataBase.SQLServerHelper.ExecuteSql(strSql.ToString(), parameters);
        }

        public string GetView()
        {
            ChangeHope.WebPage.DataView dv = new ChangeHope.WebPage.DataView();
            dv.Sql = "[select] * [from] MemberRank [where] 1=1 [order by] Priority asc";
            dv.RowHead = "序号/5%,徽标/5%,等级名称/30%,打折率/10%,积分上限/10%,积分下限/10%,升级金额/10%,操作/30%";
            dv.RowText = "{0}$@allnum,<img src='/{0}'/>$LogoPic,Name,{0}%$Discount,{0}点积分$MinScore,{0}点积分$MaxScore,{0}元人民币$UpgradeMoney,<a href='?id={0}' onclick='return confirm(\"是否真的要删除？\\n删除后将不能恢复该数据\")'>删除</a>&nbsp;&nbsp;<a href='member_rank_edit.aspx?id={0}'>编辑</a>$Id";
            string view = dv.GetView();
            dv = null;
            return view;
        }

        public DataTable GetList()
        {
            string strSql = "Select * From yxs_MemberRank";
            return ChangeHope.DataBase.SQLServerHelper.Query(strSql).Tables[0];
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.MemberRank GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from MemberRank ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;
            Model.MemberRank model = null;
            using (SqlDataReader reader = ChangeHope.DataBase.SQLServerHelper.ExecuteReader(strSql.ToString(), parameters))
            {
                while (reader.Read())
                {
                    model = new Model.MemberRank();
                    model.Id = (int)reader["Id"];
                    model.Name = (string)reader["Name"];
                    model.LogoPic = (string)reader["LogoPic"];
                    model.Discount = Convert.ToDecimal(reader["Discount"]);
                    model.MinScore = Convert.ToDecimal(reader["MinScore"]);
                    model.MaxScore = Convert.ToDecimal(reader["MaxScore"]);
                    model.Priority = Convert.ToInt32(reader["Priority"]);
                    model.IsSpecial = Convert.ToInt32(reader["IsSpecial"]);
                    model.IsShowPrice = Convert.ToInt32(reader["IsShowPrice"]);
                    model.Article = Convert.ToInt32(reader["Article"]);
                    model.Product = Convert.ToInt32(reader["Product"]);
                    model.ArticleAuditing = Convert.ToInt32(reader["ArticleAuditing"]);
                    model.ProductAuditing = Convert.ToInt32(reader["ProductAuditing"]);
                    model.MaxMoney = Convert.ToDecimal(reader["MaxMoney"]);
                    model.UpgradeMoney = Convert.ToDecimal(reader["UpgradeMoney"]);
                    model.IsUpgrade = Convert.ToInt32(reader["IsUpgrade"]);
                }
            }
            return model;

        }
        /// <summary>
        /// 查所有集合 kevin创建
        /// </summary>
        /// <returns></returns>
        public List<Model.MemberRank> GetAllMemberRank()
        {
            string strSql = "select * from MemberRank";
            List<Model.MemberRank> ranklist = new List<Model.MemberRank>();
            using (SqlDataReader reader = ChangeHope.DataBase.SQLServerHelper.ExecuteReader(strSql))
            {
                while (reader.Read())
                {
                    Model.MemberRank model = new Model.MemberRank();
                    model.Id = (int)reader["Id"];
                    model.Name = (string)reader["Name"];
                    model.LogoPic = (string)reader["LogoPic"];
                    model.Discount = Convert.ToDecimal(reader["Discount"]);
                    model.MinScore = Convert.ToDecimal(reader["MinScore"]);
                    model.MaxScore = Convert.ToDecimal(reader["MaxScore"]);
                    model.Priority = Convert.ToInt32(reader["Priority"]);
                    model.IsSpecial = Convert.ToInt32(reader["IsSpecial"]);
                    model.IsShowPrice = Convert.ToInt32(reader["IsShowPrice"]);
                    model.Article = Convert.ToInt32(reader["Article"]);
                    model.Product = Convert.ToInt32(reader["Product"]);
                    model.ArticleAuditing = Convert.ToInt32(reader["ArticleAuditing"]);
                    model.ProductAuditing = Convert.ToInt32(reader["ProductAuditing"]);
                    model.MaxMoney = Convert.ToDecimal(reader["MaxMoney"]);
                    model.UpgradeMoney = Convert.ToDecimal(reader["UpgradeMoney"]);
                    model.IsUpgrade = Convert.ToInt32(reader["IsUpgrade"]);
                    ranklist.Add(model);
                }
            }
            return ranklist;

        }

        #endregion  成员方法
    }
}
