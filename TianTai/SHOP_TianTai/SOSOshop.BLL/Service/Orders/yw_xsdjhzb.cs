using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using System.Data.OracleClient;

namespace SOSOshop.BLL.Service.Orders
{
    /// <summary>
    /// 天奇订单表
    /// </summary>
    public partial class yw_xsdjhzb
    {
        public OracleDatabase db;
        public yw_xsdjhzb()
        {
            //db = new OracleDatabase(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringTQ"].ConnectionString);
            db = new OracleDatabase("DATA SOURCE=192.168.1.6:1521/shop;PASSWORD=sosoyy&101;PERSIST SECURITY INFO=True;USER ID=SCOTT");
        }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(decimal djid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from \"yw_xsdjhzb\" where \"djid\"=:djid");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "djid", DbType.Int32, djid);
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
        public bool Add(SOSOshop.Model.Service.Orders.yw_xsdjhzb model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into yw_xsdjhzb(");
            strSql.Append("djid,jgid,jjid,rq,djbh,djbs,dwid,ywyid,lxrid,jsfsid,hsje,name,url,is_kp)");

            strSql.Append(" values (");
            strSql.Append("@djid,@jgid,@jjid,@rq,@djbh,@djbs,@dwid,@ywyid,@lxrid,@jsfsid,@hsje,@name,@url,@is_kp)");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "djid", DbType.String, model.djid);
            db.AddInParameter(dbCommand, "jgid", DbType.String, model.jgid);
            db.AddInParameter(dbCommand, "jjid", DbType.String, model.jjid);
            db.AddInParameter(dbCommand, "rq", DbType.String, model.rq);
            db.AddInParameter(dbCommand, "djbh", DbType.String, model.djbh);
            db.AddInParameter(dbCommand, "djbs", DbType.String, model.djbs);
            db.AddInParameter(dbCommand, "dwid", DbType.String, model.dwid);
            db.AddInParameter(dbCommand, "ywyid", DbType.String, model.ywyid);
            db.AddInParameter(dbCommand, "lxrid", DbType.String, model.lxrid);
            db.AddInParameter(dbCommand, "jsfsid", DbType.String, model.jsfsid);
            db.AddInParameter(dbCommand, "hsje", DbType.String, model.hsje);
            db.AddInParameter(dbCommand, "name", DbType.String, model.name);
            db.AddInParameter(dbCommand, "url", DbType.String, model.url);
            db.AddInParameter(dbCommand, "is_kp", DbType.String, model.is_kp);
            int result;
            object obj = db.ExecuteScalar(dbCommand);
            if (!int.TryParse(obj.ToString(), out result))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SOSOshop.Model.Service.Orders.yw_xsdjhzb model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update yw_xsdjhzb set ");
            strSql.Append("djid=@djid,");
            strSql.Append("jgid=@jgid,");
            strSql.Append("jjid=@jjid,");
            strSql.Append("rq=@rq,");
            strSql.Append("djbh=@djbh,");
            strSql.Append("djbs=@djbs,");
            strSql.Append("dwid=@dwid,");
            strSql.Append("ywyid=@ywyid,");
            strSql.Append("lxrid=@lxrid,");
            strSql.Append("jsfsid=@jsfsid,");
            strSql.Append("hsje=@hsje,");
            strSql.Append("name=@name,");
            strSql.Append("url=@url,");
            strSql.Append("is_kp=@is_kp");
            strSql.Append(" where djid=@djid ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "djid", DbType.String, model.djid);
            db.AddInParameter(dbCommand, "jgid", DbType.String, model.jgid);
            db.AddInParameter(dbCommand, "jjid", DbType.String, model.jjid);
            db.AddInParameter(dbCommand, "rq", DbType.String, model.rq);
            db.AddInParameter(dbCommand, "djbh", DbType.String, model.djbh);
            db.AddInParameter(dbCommand, "djbs", DbType.String, model.djbs);
            db.AddInParameter(dbCommand, "dwid", DbType.String, model.dwid);
            db.AddInParameter(dbCommand, "ywyid", DbType.String, model.ywyid);
            db.AddInParameter(dbCommand, "lxrid", DbType.String, model.lxrid);
            db.AddInParameter(dbCommand, "jsfsid", DbType.String, model.jsfsid);
            db.AddInParameter(dbCommand, "hsje", DbType.String, model.hsje);
            db.AddInParameter(dbCommand, "name", DbType.String, model.name);
            db.AddInParameter(dbCommand, "url", DbType.String, model.url);
            db.AddInParameter(dbCommand, "is_kp", DbType.String, model.is_kp);
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
        public bool Delete(decimal djid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from yw_xsdjhzb ");
            strSql.Append(" where djid=@djid ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "djid", DbType.String, djid);
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


        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            
            DbCommand dbCommand = db.GetStoredProcCommand("UP_GetRecordByPage");
            db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "yw_xsdjhzb");
            db.AddInParameter(dbCommand, "fldName", DbType.AnsiString, "djid");
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
        public List<SOSOshop.Model.Service.Orders.yw_xsdjhzb> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select djid,jgid,jjid,rq,djbh,djbs,dwid,ywyid,lxrid,jsfsid,hsje,name,url,is_kp ");
            strSql.Append(" FROM yw_xsdjhzb ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<SOSOshop.Model.Service.Orders.yw_xsdjhzb> list = new List<SOSOshop.Model.Service.Orders.yw_xsdjhzb>();

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
        public SOSOshop.Model.Service.Orders.yw_xsdjhzb ReaderBind(IDataReader dataReader)
        {
            SOSOshop.Model.Service.Orders.yw_xsdjhzb model = new SOSOshop.Model.Service.Orders.yw_xsdjhzb();
            object ojb;
            ojb = dataReader["djid"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.djid = (decimal)ojb;
            }
            ojb = dataReader["jgid"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.jgid = (decimal)ojb;
            }
            ojb = dataReader["jjid"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.jjid = (decimal)ojb;
            }
            ojb = dataReader["rq"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.rq = (DateTime)ojb;
            }
            model.djbh = dataReader["djbh"].ToString();
            model.djbs = dataReader["djbs"].ToString();
            ojb = dataReader["dwid"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.dwid = (decimal)ojb;
            }
            ojb = dataReader["ywyid"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ywyid = (decimal)ojb;
            }
            ojb = dataReader["lxrid"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.lxrid = (decimal)ojb;
            }
            ojb = dataReader["jsfsid"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.jsfsid = (decimal)ojb;
            }
            ojb = dataReader["hsje"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.hsje = (decimal)ojb;
            }
            model.name = dataReader["name"].ToString();
            model.url = dataReader["url"].ToString();
            ojb = dataReader["is_kp"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.is_kp = (decimal)ojb;
            }
            return model;
        }

        #endregion  Method
    }
}

