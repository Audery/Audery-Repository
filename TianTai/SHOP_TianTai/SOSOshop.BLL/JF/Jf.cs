using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using SOSOshop.Model.JF;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace SOSOshop.BLL.JF
{
    public class Jf:DbBase
    {
        MongoHelper<SOSOshop.Model.JF.Jf_Model> d= null;
        public void Add(SOSOshop.Model.JF.Jf_Model model)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into Jf values(@Name,@Jf,@Pt,@Img);SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sb.ToString());
            db.AddInParameter(cmd, "@Name", DbType.String, model.name);
            db.AddInParameter(cmd, "@Jf", DbType.Int32, model.jf);
            db.AddInParameter(cmd, "@Pt", DbType.String, model.pt);
            db.AddInParameter(cmd, "@Img", DbType.String, model.img);
            try
            {
                db.ExecuteScalar(cmd);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool Delete(int id)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder sb = new StringBuilder();
            sb.Append("delete from Jf ");
            sb.Append(" where ID=@ID");
            DbCommand cmd = db.GetSqlStringCommand(sb.ToString());
            db.AddInParameter(cmd, "@ID", DbType.Int32, id);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }
        public List<Jf_Model> list()
        {
            Jf bll = new Jf();
            //DataTable dt = bll.GetList();
            List<Jf_Model> list = new List<Jf_Model>();
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from Jf");
            DbCommand cmd = db.GetSqlStringCommand(sb.ToString());
            IDataReader reader = db.ExecuteReader(cmd);
            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("ID"));
                string name = reader.GetString(reader.GetOrdinal("Name"));
                int jf = reader.GetInt32(reader.GetOrdinal("Jf"));
                string pt = reader.GetString(reader.GetOrdinal("Pt"));
                string img = reader.GetString(reader.GetOrdinal("Img"));
                list.Add(new Jf_Model
                    {
                        id = id,
                        name = name,
                        jf = jf,
                        pt = pt,
                        img = img
                    });
            }
            return list;
        }
        public List<Jf_Model> list1(string name1, string pt1, int jf1)
        {
            List<Jf_Model> list2 = new List<Jf_Model>();
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from jf where 1=1 ");
            if (!string.IsNullOrEmpty(name1))
            {
                sb.Append(string.Format(" and name='{0}' ", name1));
            }
            if (!string.IsNullOrEmpty(pt1))
            {
                sb.Append(string.Format(" and pt='{0}' ", pt1));
            }
            if (jf1 != 0)
            {
                sb.Append(string.Format("and jf={0} ", jf1));
            }
            DataSet ds = db.ExecuteDataSet(CommandType.Text, sb.ToString());
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                var dt = ds.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {

                        list2.Add(new Jf_Model
                        {
                            name = item["name"].ToString(),
                            jf = Convert.ToInt32(item["jf"]),
                            pt = item["pt"].ToString(),
                            img = item["img"].ToString(),
                            id = Convert.ToInt32(item["id"])
                        });
                    }
                }
            }
            return list2;
        }
        public void Edit(int id, string name, int jf, string pt)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder sb = new StringBuilder();
            sb.Append("update jf set name=@name,jf=@jf,pt=@pt where ID=@id");
            DbCommand cmd = db.GetSqlStringCommand(sb.ToString());
            db.AddInParameter(cmd, "@ID", DbType.Int32, id);
            db.AddInParameter(cmd, "@name", DbType.String, name);
            db.AddInParameter(cmd, "@jf", DbType.Int32, jf);
            db.AddInParameter(cmd, "@pt", DbType.String, pt);
            db.ExecuteNonQuery(cmd);
        }
    }
}
