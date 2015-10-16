using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
namespace SOSOshop.BLL.Member
{
    /// <summary>
    /// 用户行为统计分析
    /// </summary>
    public partial class MemberAction : Db
    {
        public MemberAction()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from MemberAction where id=@id ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "id", DbType.Int32, id);
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
        /// <param name="model"></param>
        /// <param name="operate">是否操作，如果是操作的话就不更新页面停留时间(如添加进购物车)</param>
        /// <returns></returns>
        public void Add(SOSOshop.Model.Member.MemberAction model, bool operate)
        {
            if (operate == true) model.SleepTime = -1;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MemberAction(");
            strSql.Append("uid,controller,action,Query,HttpMethod,sessionid,url,actuation,ActuationValue,created,SleepTime,OS,WebBrowser,distinguishability)");

            strSql.Append(" values (");
            if (operate != true)
            {
                strSql.Append("@uid,@controller,@action,@Query,@HttpMethod,@sessionid,@url,@actuation,@ActuationValue,getdate(),0,@OS,@WebBrowser,@distinguishability)"); 
            }
            else
            {
                strSql.Append("@uid,@controller,@action,@Query,@HttpMethod,@sessionid,@url,@actuation,@ActuationValue,getdate(),-1,@OS,@WebBrowser,@distinguishability)"); 
            }
            strSql.Append(";select @@IDENTITY");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "uid", DbType.Int32, model.uid);
            db.AddInParameter(dbCommand, "controller", DbType.AnsiString, model.controller);
            db.AddInParameter(dbCommand, "action", DbType.AnsiString, model.action);
            db.AddInParameter(dbCommand, "Query", DbType.String, model.Query);
            db.AddInParameter(dbCommand, "HttpMethod", DbType.AnsiString, model.HttpMethod);
            db.AddInParameter(dbCommand, "sessionid", DbType.AnsiString, model.sessionid);
            db.AddInParameter(dbCommand, "url", DbType.AnsiString, model.url);
            db.AddInParameter(dbCommand, "actuation", DbType.AnsiString, model.actuation);            
            db.AddInParameter(dbCommand, "ActuationValue", DbType.String, model.ActuationValue);            
            db.AddInParameter(dbCommand, "SleepTime", DbType.Int32, model.SleepTime);
            db.AddInParameter(dbCommand, "OS", DbType.AnsiString, model.OS);
            db.AddInParameter(dbCommand, "WebBrowser", DbType.AnsiString, model.WebBrowser);
            db.AddInParameter(dbCommand, "distinguishability", DbType.AnsiString, model.distinguishability);
            string sql = string.Format("UPDATE MemberAction SET SleepTime=DATEDIFF(s,created,GETDATE()) WHERE id=(SELECT MAX(id) FROM MemberAction WHERE id<{0} AND SleepTime<>-1 AND  sessionid='{1}')", db.ExecuteScalar(dbCommand), model.sessionid);
            base.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SOSOshop.Model.Member.MemberAction model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MemberAction set ");
            strSql.Append("uid=@uid,");
            strSql.Append("controller=@controller,");
            strSql.Append("action=@action,");
            strSql.Append("Query=@Query,");
            strSql.Append("HttpMethod=@HttpMethod,");
            strSql.Append("sessionid=@sessionid,");
            strSql.Append("url=@url,");
            strSql.Append("actuation=@actuation,");
            strSql.Append("ActuationValue=@ActuationValue,");
            strSql.Append("created=@created,");
            strSql.Append("SleepTime=@SleepTime,");
            strSql.Append("OS=@OS,");
            strSql.Append("WebBrowser=@WebBrowser,");
            strSql.Append("distinguishability=@distinguishability");
            strSql.Append(" where id=@id ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "id", DbType.Int32, model.id);
            db.AddInParameter(dbCommand, "uid", DbType.Int32, model.uid);
            db.AddInParameter(dbCommand, "controller", DbType.AnsiString, model.controller);
            db.AddInParameter(dbCommand, "action", DbType.AnsiString, model.action);
            db.AddInParameter(dbCommand, "Query", DbType.String, model.Query);
            db.AddInParameter(dbCommand, "HttpMethod", DbType.AnsiString, model.HttpMethod);
            db.AddInParameter(dbCommand, "sessionid", DbType.AnsiString, model.sessionid);
            db.AddInParameter(dbCommand, "url", DbType.AnsiString, model.url);
            db.AddInParameter(dbCommand, "actuation", DbType.AnsiString, model.actuation);
            db.AddInParameter(dbCommand, "ActuationValue", DbType.String, model.ActuationValue);
            db.AddInParameter(dbCommand, "created", DbType.DateTime, model.created);
            db.AddInParameter(dbCommand, "SleepTime", DbType.Int32, model.SleepTime);
            db.AddInParameter(dbCommand, "OS", DbType.AnsiString, model.OS);
            db.AddInParameter(dbCommand, "WebBrowser", DbType.AnsiString, model.WebBrowser);
            db.AddInParameter(dbCommand, "distinguishability", DbType.AnsiString, model.distinguishability);
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
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from MemberAction ");
            strSql.Append(" where id=@id ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "id", DbType.Int32, id);
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
        /// 得到一个对象实体
        /// </summary>
        public SOSOshop.Model.Member.MemberAction GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,uid,controller,action,Query,HttpMethod,sessionid,url,actuation,ActuationValue,created,SleepTime,OS,WebBrowser,distinguishability from MemberAction ");
            strSql.Append(" where id=@id ");

            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "id", DbType.Int32, id);
            SOSOshop.Model.Member.MemberAction model = null;
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
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<SOSOshop.Model.Member.MemberAction> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,uid,controller,action,Query,HttpMethod,sessionid,url,actuation,ActuationValue,created,SleepTime,OS,WebBrowser,distinguishability ");
            strSql.Append(" FROM MemberAction ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<SOSOshop.Model.Member.MemberAction> list = new List<SOSOshop.Model.Member.MemberAction>();

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
        public SOSOshop.Model.Member.MemberAction ReaderBind(IDataReader dataReader)
        {
            SOSOshop.Model.Member.MemberAction model = new SOSOshop.Model.Member.MemberAction();
            object ojb;
            ojb = dataReader["id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.id = (int)ojb;
            }
            ojb = dataReader["uid"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.uid = (int)ojb;
            }
            model.controller = dataReader["controller"].ToString();
            model.action = dataReader["action"].ToString();
            model.Query = dataReader["Query"].ToString();
            model.HttpMethod = dataReader["HttpMethod"].ToString();
            model.sessionid = dataReader["sessionid"].ToString();
            model.url = dataReader["url"].ToString();
            model.actuation = dataReader["actuation"].ToString();
            model.ActuationValue = dataReader["ActuationValue"].ToString();
            ojb = dataReader["created"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.created = (DateTime)ojb;
            }
            ojb = dataReader["SleepTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SleepTime = (int)ojb;
            }
            model.OS = dataReader["OS"].ToString();
            model.WebBrowser = dataReader["WebBrowser"].ToString();
            model.distinguishability = dataReader["distinguishability"].ToString();
            return model;
        }

        #endregion  Method
    }
}

