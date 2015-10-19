using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Reflection;

namespace SOSOshop.BLL
{
    /// <summary>
    /// 1、实例对象LogRecord，再调用Record()保存日志；
    /// 2、先初始化LogRecord.Init(),再调用LogRecord.Recording(修改前的记录对象,字段..)记录日志事件,最后Save(修改后的记录对象);
    /// </summary>
    public class SysLog : INotifyPropertyChanging, INotifyPropertyChanged
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private static string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

        #region 构造函数
        public SysLog()
        {
        }
        public SysLog(Log log)
        {
            this.log = log;
        }
        public SysLog(int UID, int AdminID, string Source, string EventDescription, string FieldName, string FieldForValue, string FieldAfterValue, string SubModule, string Keyword, string OperateCode, DateTime OperateTime)
        {
            Log log = new Log();
            log.UID = UID; log.AdminID = AdminID; log.Source = Source; log.EventDescription = EventDescription; log.FieldName = FieldName; log.FieldForValue = FieldForValue; log.FieldAfterValue = FieldAfterValue; log.SubModule = SubModule; log.Keyword = Keyword; log.OperateCode = OperateCode; log.OperateTime = OperateTime;
            this.log = log;
        }
        public SysLog(int UID, int AdminID, string Source, string EventDescription, string FieldName, string FieldForValue, string FieldAfterValue, string SubModule, string Keyword, OperateCode OperateCode, DateTime OperateTime)
        {
            Log log = new Log();
            log.UID = UID; log.AdminID = AdminID; log.Source = Source; log.EventDescription = EventDescription; log.FieldName = FieldName; log.FieldForValue = FieldForValue; log.FieldAfterValue = FieldAfterValue; log.SubModule = SubModule; log.Keyword = Keyword; log.OperateCode = OperateCode.ToString(); log.OperateTime = OperateTime;
            this.log = log;
        }
        public SysLog(int ID, int UID, int AdminID, string Source, string EventDescription, string FieldName, string FieldForValue, string FieldAfterValue, string SubModule, string Keyword, string OperateCode, DateTime OperateTime)
        {
            Log log = new Log();
            log.ID = ID; log.UID = UID; log.AdminID = AdminID; log.Source = Source; log.EventDescription = EventDescription; log.FieldName = FieldName; log.FieldForValue = FieldForValue; log.FieldAfterValue = FieldAfterValue; log.SubModule = SubModule; log.Keyword = Keyword; log.OperateCode = OperateCode; log.OperateTime = OperateTime;
            this.log = log;
        }
        public SysLog(int ID, int UID, int AdminID, string Source, string EventDescription, string FieldName, string FieldForValue, string FieldAfterValue, string SubModule, string Keyword, OperateCode OperateCode, DateTime OperateTime)
        {
            Log log = new Log();
            log.ID = ID; log.UID = UID; log.AdminID = AdminID; log.Source = Source; log.EventDescription = EventDescription; log.FieldName = FieldName; log.FieldForValue = FieldForValue; log.FieldAfterValue = FieldAfterValue; log.SubModule = SubModule; log.Keyword = Keyword; log.OperateCode = OperateCode.ToString(); log.OperateTime = OperateTime;
            this.log = log;
        }
        #endregion

        /// <summary>
        /// 记录日志,并保存到数据库
        /// </summary>
        /// <returns>记录保存成功返回true</returns>
        public bool Record()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                int rows_affected = 0;
                string sqlString = "INSERT INTO yxs_SysLog(UID,AdminID,Source,EventDescription,FieldName,FieldForValue,FieldAfterValue,SubModule,Keyword,OperateCode,OperateTime) VALUES(@UID,@AdminID,@Source,@EventDescription,@FieldName,@FieldForValue,@FieldAfterValue,@SubModule,@Keyword,@OperateCode,@OperateTime)";
                SqlCommand cmd = new SqlCommand(sqlString, connection);
                SqlParameter[] paras = new SqlParameter[11];
                paras[0] = new SqlParameter("@UID", SqlDbType.Int, 4);
                paras[0].Value = log.UID;
                paras[1] = new SqlParameter("@AdminID", SqlDbType.Int, 4);
                paras[1].Value = log.AdminID;
                paras[2] = new SqlParameter("@Source", SqlDbType.VarChar, 200);
                paras[2].Value = log.Source.Substring(0, Math.Min(200, log.Source.Length));
                paras[3] = new SqlParameter("@EventDescription", SqlDbType.VarChar, 100);
                paras[3].Value = log.EventDescription.Substring(0, Math.Min(100, log.EventDescription.Length));
                paras[4] = new SqlParameter("@FieldName", SqlDbType.VarChar, 50);
                paras[4].Value = log.FieldName.Substring(0, Math.Min(50, log.FieldName.Length));
                paras[5] = new SqlParameter("@FieldForValue", SqlDbType.VarChar, 200);
                paras[5].Value = log.FieldForValue.Substring(0, Math.Min(200, log.FieldForValue.Length));
                paras[6] = new SqlParameter("@FieldAfterValue", SqlDbType.VarChar, 200);
                paras[6].Value = log.FieldAfterValue.Substring(0, Math.Min(200, log.FieldAfterValue.Length));
                paras[7] = new SqlParameter("@SubModule", SqlDbType.VarChar, 50);
                paras[7].Value = log.SubModule.Substring(0, Math.Min(50, log.SubModule.Length));
                paras[8] = new SqlParameter("@Keyword", SqlDbType.VarChar, 50);
                paras[8].Value = log.Keyword.Substring(0, Math.Min(50, log.Keyword.Length));
                paras[9] = new SqlParameter("@OperateCode", SqlDbType.VarChar, 10);
                paras[9].Value = log.OperateCode.Substring(0, Math.Min(10, log.OperateCode.Length));
                paras[10] = new SqlParameter("@OperateTime", SqlDbType.DateTime);
                paras[10].Value = log.OperateTime;
                foreach (SqlParameter parm in paras) cmd.Parameters.Add(parm);
                try
                {
                    connection.Open();
                    rows_affected = cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    connection.Close();
                    throw new Exception(e.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
                return (rows_affected > 0);
            }
        }
        /// <summary>
        /// 记录日志,并保存到数据库
        /// </summary>
        /// <returns>记录保存成功返回id</returns>
        public void Record(ref int id_affected)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string sqlString = "INSERT INTO yxs_SysLog(UID,AdminID,Source,EventDescription,FieldName,FieldForValue,FieldAfterValue,SubModule,Keyword,OperateCode,OperateTime) VALUES(@UID,@AdminID,@Source,@EventDescription,@FieldName,@FieldForValue,@FieldAfterValue,@SubModule,@Keyword,@OperateCode,@OperateTime); SELECT @@IDENTITY";
                SqlCommand cmd = new SqlCommand(sqlString, connection);
                SqlParameter[] paras = new SqlParameter[11];
                paras[0] = new SqlParameter("@UID", SqlDbType.Int, 4);
                paras[0].Value = log.UID;
                paras[1] = new SqlParameter("@AdminID", SqlDbType.Int, 4);
                paras[1].Value = log.AdminID;
                paras[2] = new SqlParameter("@Source", SqlDbType.VarChar, 200);
                paras[2].Value = log.Source.Substring(0, Math.Min(200, log.Source.Length));
                paras[3] = new SqlParameter("@EventDescription", SqlDbType.VarChar, 100);
                paras[3].Value = log.EventDescription.Substring(0, Math.Min(100, log.EventDescription.Length));
                paras[4] = new SqlParameter("@FieldName", SqlDbType.VarChar, 50);
                paras[4].Value = log.FieldName.Substring(0, Math.Min(50, log.FieldName.Length));
                paras[5] = new SqlParameter("@FieldForValue", SqlDbType.VarChar, 200);
                paras[5].Value = log.FieldForValue.Substring(0, Math.Min(200, log.FieldForValue.Length));
                paras[6] = new SqlParameter("@FieldAfterValue", SqlDbType.VarChar, 200);
                paras[6].Value = log.FieldAfterValue.Substring(0, Math.Min(200, log.FieldAfterValue.Length));
                paras[7] = new SqlParameter("@SubModule", SqlDbType.VarChar, 50);
                paras[7].Value = log.SubModule.Substring(0, Math.Min(50, log.SubModule.Length));
                paras[8] = new SqlParameter("@Keyword", SqlDbType.VarChar, 50);
                paras[8].Value = log.Keyword.Substring(0, Math.Min(50, log.Keyword.Length));
                paras[9] = new SqlParameter("@OperateCode", SqlDbType.VarChar, 10);
                paras[9].Value = log.OperateCode.Substring(0, Math.Min(10, log.OperateCode.Length));
                paras[10] = new SqlParameter("@OperateTime", SqlDbType.DateTime);
                paras[10].Value = log.OperateTime;
                foreach (SqlParameter parm in paras) cmd.Parameters.Add(parm);
                try
                {
                    connection.Open();
                    object obj = cmd.ExecuteScalar();
                    if (!Object.Equals(obj, null) && !Object.Equals(obj, System.DBNull.Value))
                    {
                        id_affected = Convert.ToInt32(obj);
                    }
                }
                catch (SqlException e)
                {
                    connection.Close();
                    throw new Exception(e.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// 更新日志(依据ID),并保存到数据库
        /// </summary>
        /// <param name="log"></param>
        /// <returns>记录保存成功返回true</returns>
        public static bool UpdateRecord(Log log)
        {
            return UpdateRecord(log.ID, log.UID, log.AdminID, log.Source, log.EventDescription, log.FieldName, log.FieldForValue, log.FieldAfterValue, log.SubModule, log.Keyword);
        }
        /// <summary>
        /// 更新日志(依据UID或AdminID),并保存到数据库
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="AdminID"></param>
        /// <param name="Source"></param>
        /// <param name="EventDescription"></param>
        /// <param name="FieldName"></param>
        /// <param name="FieldForValue"></param>
        /// <param name="FieldAfterValue"></param>
        /// <param name="SubModule"></param>
        /// <param name="Keyword"></param>
        /// <returns>记录保存成功返回影响记录数</returns>
        public static int UpdateRecord(int UID, int AdminID, string Source, string EventDescription, string FieldName, string FieldForValue, string FieldAfterValue, string SubModule, string Keyword)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                int rows_affected = 0;
                string sqlString = "UPDATE  yxs_SysLog ";
                StringBuilder sqlStringSet = new StringBuilder();
                SqlCommand cmd = new SqlCommand(sqlString, connection);
                if (Source != null)
                {
                    SqlParameter para0 = new SqlParameter("@Source", SqlDbType.VarChar, 200);
                    para0.Value = Source.Substring(0, Math.Min(200, Source.Length));
                    cmd.Parameters.Add(para0);
                    sqlStringSet.Append(",Source=@Source");
                }
                if (EventDescription != null)
                {
                    SqlParameter para1 = new SqlParameter("@EventDescription", SqlDbType.VarChar, 100);
                    para1.Value = EventDescription.Substring(0, Math.Min(100, EventDescription.Length));
                    cmd.Parameters.Add(para1);
                    sqlStringSet.Append(",EventDescription=@EventDescription");
                }
                if (FieldName != null)
                {
                    SqlParameter para2 = new SqlParameter("@FieldName", SqlDbType.VarChar, 50);
                    para2.Value = FieldName.Substring(0, Math.Min(50, FieldName.Length));
                    cmd.Parameters.Add(para2);
                    sqlStringSet.Append(",FieldName=@FieldName");
                }
                if (FieldForValue != null)
                {
                    SqlParameter para3 = new SqlParameter("@FieldForValue", SqlDbType.VarChar, 200);
                    para3.Value = FieldForValue.Substring(0, Math.Min(200, FieldForValue.Length));
                    cmd.Parameters.Add(para3);
                    sqlStringSet.Append(",FieldForValue=@FieldForValue");
                }
                if (FieldAfterValue != null)
                {
                    SqlParameter para4 = new SqlParameter("@FieldAfterValue", SqlDbType.VarChar, 200);
                    para4.Value = FieldAfterValue.Substring(0, Math.Min(200, FieldAfterValue.Length));
                    cmd.Parameters.Add(para4);
                    sqlStringSet.Append(",FieldAfterValue=@FieldAfterValue");
                }
                if (SubModule != null)
                {
                    SqlParameter para5 = new SqlParameter("@SubModule", SqlDbType.VarChar, 50);
                    para5.Value = SubModule.Substring(0, Math.Min(50, SubModule.Length));
                    cmd.Parameters.Add(para5);
                    sqlStringSet.Append(",SubModule=@SubModule");
                }
                if (Keyword != null)
                {
                    SqlParameter para6 = new SqlParameter("@Keyword", SqlDbType.VarChar, 50);
                    para6.Value = Keyword.Substring(0, Math.Min(50, Keyword.Length));
                    cmd.Parameters.Add(para6);
                    sqlStringSet.Append(",Keyword=@Keyword");
                }
                if (sqlStringSet.ToString() != "")
                {
                    sqlString += " SET " + sqlStringSet.ToString().Substring(1);
                    if (UID > 0)
                    {
                        sqlString += " WHERE ";
                        sqlString += "UID=@UID ";
                        if (AdminID > 0) sqlString += "AND AdminID=@AdminID ";
                    }
                    else if (AdminID > 0)
                    {
                        sqlString += " WHERE ";
                        sqlString += "AdminID=@AdminID ";
                    }
                    else
                    {
                        sqlString += " WHERE 1=0";
                    }
                    if (UID > 0)
                    {
                        SqlParameter para = new SqlParameter("@UID", SqlDbType.Int, 4);
                        para.Value = UID;
                        cmd.Parameters.Add(para);
                    }
                    if (AdminID > 0)
                    {
                        SqlParameter para = new SqlParameter("@AdminID", SqlDbType.Int, 4);
                        para.Value = AdminID;
                        cmd.Parameters.Add(para);
                    }
                    try
                    {
                        connection.Open();
                        rows_affected = cmd.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        connection.Close();
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        cmd.Dispose();
                        connection.Close();
                    }
                }
                return rows_affected;
            }
        }
        /// <summary>
        /// 更新日志(依据ID),并保存到数据库
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="UID"></param>
        /// <param name="AdminID"></param>
        /// <param name="Source"></param>
        /// <param name="EventDescription"></param>
        /// <param name="FieldName"></param>
        /// <param name="FieldForValue"></param>
        /// <param name="FieldAfterValue"></param>
        /// <param name="SubModule"></param>
        /// <param name="Keyword"></param>
        /// <returns>记录保存成功返回true</returns>
        public static bool UpdateRecord(int ID, int UID, int AdminID, string Source, string EventDescription, string FieldName, string FieldForValue, string FieldAfterValue, string SubModule, string Keyword)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                int rows_affected = 0;
                string sqlString = "UPDATE  yxs_SysLog SET  ";
                string sqlStringSet = "";
                SqlCommand cmd = new SqlCommand(sqlString, connection);
                if (UID > 0)
                {
                    SqlParameter para0 = new SqlParameter("@UID", SqlDbType.Int, 4);
                    para0.Value = UID;
                    cmd.Parameters.Add(para0);
                    sqlStringSet += ",UID=@UID ";
                }
                if (AdminID > 0)
                {
                    SqlParameter para1 = new SqlParameter("@AdminID", SqlDbType.Int, 4);
                    para1.Value = AdminID;
                    cmd.Parameters.Add(para1);
                    sqlStringSet += ",AdminID=@AdminID ";
                }
                if (Source != null)
                {
                    SqlParameter para2 = new SqlParameter("@Source", SqlDbType.VarChar, 200);
                    para2.Value = Source.Substring(0, Math.Min(200, Source.Length));
                    cmd.Parameters.Add(para2);
                    sqlStringSet += ",Source=@Source ";
                }
                if (EventDescription != null)
                {
                    SqlParameter para3 = new SqlParameter("@EventDescription", SqlDbType.VarChar, 100);
                    para3.Value = EventDescription.Substring(0, Math.Min(100, EventDescription.Length));
                    cmd.Parameters.Add(para3);
                    sqlStringSet += ",EventDescription=@EventDescription ";
                }
                if (FieldName != null)
                {
                    SqlParameter para4 = new SqlParameter("@FieldName", SqlDbType.VarChar, 50);
                    para4.Value = FieldName.Substring(0, Math.Min(50, FieldName.Length));
                    cmd.Parameters.Add(para4);
                    sqlStringSet += ",FieldName=@FieldName ";
                }
                if (FieldForValue != null)
                {
                    SqlParameter para5 = new SqlParameter("@FieldForValue", SqlDbType.VarChar, 200);
                    para5.Value = FieldForValue.Substring(0, Math.Min(200, FieldForValue.Length));
                    cmd.Parameters.Add(para5);
                    sqlStringSet += ",FieldForValue=@FieldForValue ";
                }
                if (FieldAfterValue != null)
                {
                    SqlParameter para6 = new SqlParameter("@FieldAfterValue", SqlDbType.VarChar, 200);
                    para6.Value = FieldAfterValue.Substring(0, Math.Min(200, FieldAfterValue.Length));
                    cmd.Parameters.Add(para6);
                    sqlStringSet += ",FieldAfterValue=@FieldAfterValue ";
                }
                if (SubModule != null)
                {
                    SqlParameter para7 = new SqlParameter("@SubModule", SqlDbType.VarChar, 50);
                    para7.Value = SubModule.Substring(0, Math.Min(50, SubModule.Length));
                    cmd.Parameters.Add(para7);
                    sqlStringSet += ",SubModule=@SubModule ";
                }
                if (Keyword != null)
                {
                    SqlParameter para8 = new SqlParameter("@Keyword", SqlDbType.VarChar, 50);
                    para8.Value = Keyword.Substring(0, Math.Min(50, Keyword.Length));
                    cmd.Parameters.Add(para8);
                    sqlStringSet += ",Keyword=@Keyword ";
                }
                if (sqlStringSet != "")
                {
                    sqlString += sqlStringSet.Substring(1);
                    if (ID > 0)
                    {
                        sqlString += " WHERE ";
                        sqlString += " ID=@ID ";
                        SqlParameter para9 = new SqlParameter("@ID", SqlDbType.Int, 8);
                        para9.Value = ID;
                        cmd.Parameters.Add(para9);
                        try
                        {
                            connection.Open();
                            rows_affected = cmd.ExecuteNonQuery();
                        }
                        catch (SqlException e)
                        {
                            connection.Close();
                            throw new Exception(e.Message);
                        }
                        finally
                        {
                            cmd.Dispose();
                            connection.Close();
                        }
                    }
                }
                return (rows_affected > 0);
            }
        }

        /// <summary>
        /// 读取日志
        /// </summary>
        /// <param name="where">条件?字段[UID,AdminID,Source,EventDescription,FieldName,FieldForValue,FieldAfterValue,SubModule,Keyword,OperateCode,OperateTime]</param>
        /// <returns></returns>
        public static int SelectCount(string where)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataSet ds = new DataSet();
                int rows_affected = 0;
                where = string.IsNullOrEmpty(where) ? "1=1" : where.Replace("where", "");
                string sqlString = "SELECT COUNT(1) FROM yxs_SysLog WHERE " + where;
                SqlCommand cmd = new SqlCommand(sqlString, connection);
                try
                {
                    connection.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        rows_affected = da.Fill(ds);
                        if (rows_affected > 0) rows_affected = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                    }
                }
                catch (SqlException e)
                {
                    connection.Close();
                    throw new Exception(e.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
                return rows_affected;
            }
        }
        /// <summary>
        /// 读取日志
        /// </summary>
        /// <param name="where">条件?字段[UID,AdminID,Source,EventDescription,FieldName,FieldForValue,FieldAfterValue,SubModule,Keyword,OperateCode,OperateTime]</param>
        /// <returns></returns>
        public static int SelectID(string where)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataSet ds = new DataSet();
                int rows_affected = 0;
                where = string.IsNullOrEmpty(where) ? "1=1" : where.Replace("where", "");
                string sqlString = "SELECT ID FROM yxs_SysLog WHERE " + where;
                SqlCommand cmd = new SqlCommand(sqlString, connection);
                try
                {
                    connection.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        rows_affected = da.Fill(ds);
                        if (rows_affected > 0) rows_affected = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                    }
                }
                catch (SqlException e)
                {
                    connection.Close();
                    throw new Exception(e.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
                return rows_affected;
            }
        }
        /// <summary>
        /// 读取日志
        /// </summary>
        /// <param name="where">条件?字段[UID,AdminID,Source,EventDescription,FieldName,FieldForValue,FieldAfterValue,SubModule,Keyword,OperateCode,OperateTime]</param>
        /// <returns></returns>
        public static DataSet Select(string where)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataSet ds = new DataSet();
                int rows_affected = 0;
                where = string.IsNullOrEmpty(where) ? "1=1" : where.Replace("where", "");
                string sqlString = "SELECT * FROM yxs_SysLog WHERE " + where;
                SqlCommand cmd = new SqlCommand(sqlString, connection);
                try
                {
                    connection.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        rows_affected = da.Fill(ds);
                    }
                }
                catch (SqlException e)
                {
                    connection.Close();
                    throw new Exception(e.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
                return ds;
            }
        }
        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="where">条件?字段[UID,AdminID,Source,EventDescription,FieldName,FieldForValue,FieldAfterValue,SubModule,Keyword,OperateCode,OperateTime]</param>
        /// <returns></returns>
        public static int Delete(string where)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                int rows_affected = 0;
                where = string.IsNullOrEmpty(where) ? "1=1" : where.Replace("where", "");
                string sqlString = "DELETE FROM yxs_SysLog WHERE " + where;
                SqlCommand cmd = new SqlCommand(sqlString, connection);
                try
                {
                    connection.Open();
                    rows_affected = cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    connection.Close();
                    throw new Exception(e.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
                return rows_affected;
            }
        }
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public static int Query(string sqlString)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataSet ds = new DataSet();
                int rows_affected = 0;
                SqlCommand cmd = new SqlCommand(sqlString, connection);
                try
                {
                    connection.Open();
                    rows_affected = cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    connection.Close();
                    throw new Exception(e.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
                return rows_affected;
            }
        }

        public static void Init()
        {
            logs = new List<Log>();
        }
        public static void Recording(object obj, String FieldName, int UID, int AdminID, string Source, string EventDescription, string SubModule, string Keyword, string OperateCode, DateTime OperateTime)
        {
            if (!string.IsNullOrEmpty(FieldName))
            {
                PropertyInfo[] properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo info in properties)
                {
                    if (FieldName.ToUpper() == info.Name.ToUpper())
                    {
                        string FieldForValue = Convert.ToString(info.GetValue(obj, null));
                        logs.Add(new Log(UID, AdminID, Source, EventDescription, FieldName, FieldForValue, string.Empty, SubModule, Keyword, OperateCode, OperateTime));
                    }
                }
            }
        }
        public static void Recording(object obj, String FieldName, int UID, int AdminID, string Source, string EventDescription, string SubModule, string Keyword, OperateCode OperateCode, DateTime OperateTime)
        {
            if (!string.IsNullOrEmpty(FieldName))
            {
                PropertyInfo[] properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo info in properties)
                {
                    if (FieldName.ToUpper() == info.Name.ToUpper())
                    {
                        string FieldForValue = Convert.ToString(info.GetValue(obj, null));
                        logs.Add(new Log(UID, AdminID, Source, EventDescription, FieldName, FieldForValue, string.Empty, SubModule, Keyword, OperateCode, OperateTime));
                    }
                }
            }
        }
        public static void Recording(object obj, String[] FieldNames, int UID, int AdminID, string Source, string EventDescription, string SubModule, string Keyword, string OperateCode, DateTime OperateTime)
        {
            if (FieldNames.Length > 0)
            {
                PropertyInfo[] properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo info in properties)
                {
                    foreach (string FieldName in FieldNames)
                    {
                        if (FieldName.ToUpper() == info.Name.ToUpper())
                        {
                            string FieldForValue = Convert.ToString(info.GetValue(obj, null));
                            logs.Add(new Log(UID, AdminID, Source, EventDescription, FieldName, FieldForValue, string.Empty, SubModule, Keyword, OperateCode, OperateTime));
                        }
                    }
                }
            }
        }
        public static void Recording(object obj, String[] FieldNames, int UID, int AdminID, string Source, string EventDescription, string SubModule, string Keyword, OperateCode OperateCode, DateTime OperateTime)
        {
            if (FieldNames.Length > 0)
            {
                PropertyInfo[] properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo info in properties)
                {
                    foreach (string FieldName in FieldNames)
                    {
                        if (FieldName.ToUpper() == info.Name.ToUpper())
                        {
                            string FieldForValue = Convert.ToString(info.GetValue(obj, null));
                            logs.Add(new Log(UID, AdminID, Source, EventDescription, FieldName, FieldForValue, string.Empty, SubModule, Keyword, OperateCode, OperateTime));
                        }
                    }
                }
            }
        }
        public static bool Save(object obj)
        {
            try
            {
                if (logs.Count > 0)
                {
                    PropertyInfo[] properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (PropertyInfo info in properties)
                    {
                        foreach (Log log in logs)
                        {
                            if (log.FieldAfterValue != string.Empty) continue;
                            if (log.FieldName.ToUpper() == info.Name.ToUpper())
                            {
                                string FieldForValue = log.FieldForValue;
                                string FieldAfterValue = Convert.ToString(info.GetValue(obj, null));
                                if (FieldForValue != FieldAfterValue)
                                {
                                    Log _log = log;
                                    _log.FieldAfterValue = FieldAfterValue;
                                    SysLog LogRecord = new SysLog(_log);
                                    LogRecord.Record();
                                }
                            }
                        }
                    }
                    Init();
                }
            }
            catch
            {
                Init();
                return false;
            }
            return true;
        }

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Model
        private static List<Log> logs = new List<Log>();
        private Log log = new Log();
        /// <summary>
        /// 主键
        /// </summary>
        public int ID
        {
            get { return log.ID; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UID
        {
            get { return log.UID; }
        }
        /// <summary>
        /// 管理员ID
        /// </summary>
        public int AdminID
        {
            get { return log.AdminID; }
        }
        /// <summary>
        /// 来源
        /// </summary>
        public string Source
        {
            get { return log.Source; }
        }
        /// <summary>
        /// 事件描述
        /// </summary>
        public string EventDescription
        {
            get { return log.EventDescription; }
        }
        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName
        {
            get { return log.FieldName; }
        }
        /// <summary>
        /// 字段原值
        /// </summary>
        public string FieldForValue
        {
            get { return log.FieldForValue; }
        }
        /// <summary>
        /// 字段现值
        /// </summary>
        public string FieldAfterValue
        {
            get { return log.FieldAfterValue; }
        }
        /// <summary>
        /// 子模块
        /// </summary>
        public string SubModule
        {
            get { return log.SubModule; }
        }
        /// <summary>
        /// 关键词
        /// </summary>
        public string Keyword
        {
            get { return log.Keyword; }
        }
        /// <summary>
        /// 操作代码
        /// </summary>
        public string OperateCode
        {
            get { return log.OperateCode; }
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperateTime
        {
            get { return log.OperateTime; }
        }
        #endregion Model

    }
    public class Log
    {
        #region 构造函数
        public Log()
        {
        }
        public Log(int UID, int AdminID, string Source, string EventDescription, string FieldName, string FieldForValue, string FieldAfterValue, string SubModule, string Keyword, string OperateCode, DateTime OperateTime)
        {
            this.UID = UID; this.AdminID = AdminID; this.Source = Source; this.EventDescription = EventDescription; this.FieldName = FieldName; this.FieldForValue = FieldForValue; this.FieldAfterValue = FieldAfterValue; this.SubModule = SubModule; this.Keyword = Keyword; this.OperateCode = OperateCode; this.OperateTime = OperateTime;
        }
        public Log(int UID, int AdminID, string Source, string EventDescription, string FieldName, string FieldForValue, string FieldAfterValue, string SubModule, string Keyword, OperateCode OperateCode, DateTime OperateTime)
        {
            this.UID = UID; this.AdminID = AdminID; this.Source = Source; this.EventDescription = EventDescription; this.FieldName = FieldName; this.FieldForValue = FieldForValue; this.FieldAfterValue = FieldAfterValue; this.SubModule = SubModule; this.Keyword = Keyword; this.OperateCode = OperateCode.ToString(); this.OperateTime = OperateTime;
        }
        public Log(int ID, int UID, int AdminID, string Source, string EventDescription, string FieldName, string FieldForValue, string FieldAfterValue, string SubModule, string Keyword, string OperateCode, DateTime OperateTime)
        {
            this.ID = ID; this.UID = UID; this.AdminID = AdminID; this.Source = Source; this.EventDescription = EventDescription; this.FieldName = FieldName; this.FieldForValue = FieldForValue; this.FieldAfterValue = FieldAfterValue; this.SubModule = SubModule; this.Keyword = Keyword; this.OperateCode = OperateCode; this.OperateTime = OperateTime;
        }
        public Log(int ID, int UID, int AdminID, string Source, string EventDescription, string FieldName, string FieldForValue, string FieldAfterValue, string SubModule, string Keyword, OperateCode OperateCode, DateTime OperateTime)
        {
            this.ID = ID; this.UID = UID; this.AdminID = AdminID; this.Source = Source; this.EventDescription = EventDescription; this.FieldName = FieldName; this.FieldForValue = FieldForValue; this.FieldAfterValue = FieldAfterValue; this.SubModule = SubModule; this.Keyword = Keyword; this.OperateCode = OperateCode.ToString(); this.OperateTime = OperateTime;
        }
        #endregion

        #region Model
        private int _id;
        private int _uid;
        private int _adminid;
        private string _source;
        private string _eventdescription;
        private string _fieldname;
        private string _fieldforvalue;
        private string _fieldaftervalue;
        private string _submodule;
        private string _keyword;
        private string _operatecode;
        private DateTime _operatetime;
        /// <summary>
        /// 主键
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UID
        {
            set { _uid = value; }
            get { return _uid; }
        }
        /// <summary>
        /// 管理员ID
        /// </summary>
        public int AdminID
        {
            set { _adminid = value; }
            get { return _adminid; }
        }
        /// <summary>
        /// 来源
        /// </summary>
        public string Source
        {
            set { _source = value; }
            get { return _source; }
        }
        /// <summary>
        /// 事件描述
        /// </summary>
        public string EventDescription
        {
            set { _eventdescription = value; }
            get { return _eventdescription; }
        }
        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName
        {
            set { _fieldname = value; }
            get { return _fieldname; }
        }
        /// <summary>
        /// 字段原值
        /// </summary>
        public string FieldForValue
        {
            set { _fieldforvalue = value; }
            get { return _fieldforvalue; }
        }
        /// <summary>
        /// 字段现值
        /// </summary>
        public string FieldAfterValue
        {
            set { _fieldaftervalue = value; }
            get { return _fieldaftervalue; }
        }
        /// <summary>
        /// 子模块
        /// </summary>
        public string SubModule
        {
            set { _submodule = value; }
            get { return _submodule; }
        }
        /// <summary>
        /// 关键词
        /// </summary>
        public string Keyword
        {
            set { _keyword = value; }
            get { return _keyword; }
        }
        /// <summary>
        /// 操作代码
        /// </summary>
        public string OperateCode
        {
            set { _operatecode = value; }
            get { return _operatecode; }
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperateTime
        {
            set { _operatetime = value; }
            get { return _operatetime; }
        }
        #endregion Model
    }
    /// <summary>
    /// 操作代码
    /// </summary>
    public enum OperateCode
    {
        /// <summary>
        /// 添加记录
        /// </summary>
        添加记录 = 0,
        /// <summary>
        /// 修改记录
        /// </summary>
        修改记录 = 1,
        /// <summary>
        /// 删除记录
        /// </summary>
        删除记录 = 2
    }

}
