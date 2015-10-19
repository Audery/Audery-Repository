using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace SOSOshop.BLL
{
    public class MailReceiver
    {
        public MailReceiver()
        {
            ChangeHope.DataBase.SQLServerHelper.connectionString = new SOSOshop.BLL.Db()._db.ConnectionString;
        }
        #region  "DataBase Operation"
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.MailReceiver model)
        {
            SqlParameter[] paras = (SqlParameter[])this.ValueParas(model);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into yxs_mailreceiver(");
            strSql.Append("[receiverid],[receiver],[receiveTime],[stat],[isread],[title],[body],[sender] )");
            strSql.Append(" values (");
            strSql.Append("@receiverid,@receiver,@receiveTime,@stat,@isread,@title,@body,@sender)");
            strSql.Append(";select @@IDENTITY");
            object obj = ChangeHope.DataBase.SQLServerHelper.GetSingle(strSql.ToString(), paras);
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
        /// 更新一条数据
        /// </summary>
        public void Amend(Model.MailReceiver model)
        {
            SqlParameter[] paras = (SqlParameter[])this.ValueParas(model);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update yxs_mailreceiver set ");
            strSql.Append("receiverid=@receiverid,");
            strSql.Append("receiver=@receiver,");
            strSql.Append("receiveTime=@receiveTime,");
            strSql.Append("stat=@stat,");
            strSql.Append("isread=@isread");
            strSql.Append("title=@title");
            strSql.Append("body=@body");
            strSql.Append("sender=@sender");
            strSql.Append(" where id=@id ");
            ChangeHope.DataBase.SQLServerHelper.ExecuteSql(strSql.ToString(), paras);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(string id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from yxs_mailreceiver ");
            strSql.Append(" where [id] in (" + id + ") ");
            ChangeHope.DataBase.SQLServerHelper.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Amend(int id, string columnName, object value)
        {
            string sequel = "Update [yxs_mailreceiver] set ";
            sequel = sequel + "[" + columnName + "] =@Value ";
            sequel = sequel + UpdateWhereSequel;
            SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter("@Value", value),
                new SqlParameter("@id", id) 
            };
            object obj = ChangeHope.DataBase.SQLServerHelper.GetSingle(sequel, paras);
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

        #region  "Data Load"
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.MailReceiver GetModelByID(int id)
        {
            Model.MailReceiver model = new Model.MailReceiver();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 [id],[receiverid],[receiver],[receiveTime],[stat],[isread],[title],[body],[sender] from yxs_mailreceiver ");
            strSql.Append(" where [id]=@id ");
            SqlParameter[] parameters = (SqlParameter[])this.ValueIDPara(id);
            using (SqlDataReader reader = ChangeHope.DataBase.SQLServerHelper.ExecuteReader(strSql.ToString(), parameters))
            {
                if (reader.Read())
                {
                    model.ID = reader.GetInt32(0);
                    model.ReceiverId = reader.GetInt32(1);
                    model.Receiver = reader.GetString(2);
                    model.ReceiveTime = reader.GetDateTime(3);
                    model.Stat = reader.GetInt32(4);
                    model.IsRead = reader.GetInt32(5);
                    model.Title = reader.GetString(6);
                    model.Body = reader.GetString(7);
                    model.Sender = reader.GetString(8);
                }
            }
            return model;
        }

        /// <summary>
        /// 所有数据集合
        /// </summary>
        /// <returns></returns>
        public List<Model.MailReceiver> GetAll()
        {
            List<Model.MailReceiver> list = new List<Model.MailReceiver>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [id],[receiverid],[receiver],[receiveTime],[stat],[isread],[title],[body],[sender] from yxs_mailreceiver ");
            strSql.Append(" where 1=1");
            using (SqlDataReader reader = ChangeHope.DataBase.SQLServerHelper.ExecuteReader(strSql.ToString()))
            {
                while (reader.Read())
                {
                    Model.MailReceiver model = new Model.MailReceiver();
                    model.ID = reader.GetInt32(0);
                    model.ReceiverId = reader.GetInt32(1);
                    model.Receiver = reader.GetString(2);
                    model.ReceiveTime = reader.GetDateTime(3);
                    model.Stat = reader.GetInt32(4);
                    model.IsRead = reader.GetInt32(5);
                    model.Title = reader.GetString(6);
                    model.Body = reader.GetString(7);
                    model.Sender = reader.GetString(8);
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 得到指定条件的所有短消息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public List<Model.MailReceiver> GetAll(string strWhere)
        {
            List<Model.MailReceiver> list = new List<Model.MailReceiver>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [id],[receiverid],[receiver],[receiveTime],[stat],[isread],[title],[body],[sender] from yxs_mailreceiver ");
            if (strWhere != null && strWhere != "")
            {
                strSql.Append("where " + strWhere + " ");
            }
            using (SqlDataReader reader = ChangeHope.DataBase.SQLServerHelper.ExecuteReader(strSql.ToString()))
            {
                while (reader.Read())
                {
                    Model.MailReceiver model = new Model.MailReceiver();
                    model.ID = reader.GetInt32(0);
                    model.ReceiverId = reader.GetInt32(1);
                    model.Receiver = reader.GetString(2);
                    model.ReceiveTime = reader.GetDateTime(3);
                    model.Stat = reader.GetInt32(4);
                    model.IsRead = reader.GetInt32(5);
                    model.Title = reader.GetString(6);
                    model.Body = reader.GetString(7);
                    model.Sender = reader.GetString(8);
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 得到不同条件得到列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public ChangeHope.DataBase.DataByPage GetList(string strWhere)
        {
            ChangeHope.DataBase.DataByPage dataPage = new ChangeHope.DataBase.DataByPage();
            dataPage.Sql = "[select] * [from] yxs_mailreceiver [where] " + strWhere + " [order by] id desc";
            dataPage.GetRecordSetByPage();
            return dataPage;
        }
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <returns></returns>
        public ChangeHope.DataBase.DataByPage GetList()
        {
            ChangeHope.DataBase.DataByPage dataPage = new ChangeHope.DataBase.DataByPage();
            dataPage.Sql = "[select] * [from] yxs_mailreceiver [where] 1=1 [order by] id desc";
            dataPage.GetRecordSetByPage();
            return dataPage;
        }
        #endregion

        #region "Other function"

        /// <summary>
        /// 更新条件
        /// </summary>
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
        public IDbDataParameter[] ValueParas(Model.MailReceiver model)
        {
            SqlParameter[] paras = new SqlParameter[9];
            paras[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            paras[0].Value = model.ID;
            paras[1] = new SqlParameter("@receiverid", SqlDbType.Int, 4);
            paras[1].Value = model.ReceiverId;
            paras[2] = new SqlParameter("@receiver", SqlDbType.VarChar, 50);
            paras[2].Value = model.Receiver;
            paras[3] = new SqlParameter("@receiveTime", SqlDbType.DateTime);
            paras[3].Value = model.ReceiveTime;
            paras[4] = new SqlParameter("@stat", SqlDbType.Int, 4);
            paras[4].Value = model.Stat;
            paras[5] = new SqlParameter("@isread", SqlDbType.Int, 4);
            paras[5].Value = model.IsRead;
            paras[6] = new SqlParameter("@title", SqlDbType.VarChar, 500);
            paras[6].Value = model.Title;
            paras[7] = new SqlParameter("@body", SqlDbType.NText);
            paras[7].Value = model.Body;
            paras[8] = new SqlParameter("@sender", SqlDbType.VarChar, 50);
            paras[8].Value = model.Sender;
            return paras;
        }

        /// <summary>
        /// 主键参数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IDbDataParameter[] ValueIDPara(int id)
        {
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@id", id);
            paras[0].DbType = DbType.Int32;
            return paras;
        }

        #endregion
    }
}
