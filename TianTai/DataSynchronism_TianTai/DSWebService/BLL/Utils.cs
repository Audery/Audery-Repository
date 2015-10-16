using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.ObjectModel;
using CompressDataSet;
namespace DSWebService.BLL
{

    public class Utils
    {
        /// <summary>
        /// 压缩字节数组
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static byte[] Compress(byte[] data)
        {
            MemoryStream _MemoryStream = new MemoryStream();
            Stream _zipStream = new GZipStream(_MemoryStream, CompressionMode.Compress, true);
            _zipStream.Write(data, 0, data.Length);
            _zipStream.Close();
            _MemoryStream.Position = 0;
            byte[] _buffer = new byte[_MemoryStream.Length];
            _MemoryStream.Read(_buffer, 0, int.Parse(_MemoryStream.Length.ToString()));
            return _buffer;
        }

        /// <summary>
        /// 返回压缩后的字节数组
        /// </summary>
        /// <param name="_DataSet">DataSet</param>
        /// <returns>byte[]</returns>
        public static byte[] GetZipBytesByDataSet(DataSet _DataSet)
        {
            DataSetSurrogate _DataSetSurrogate = new DataSetSurrogate(_DataSet);
            BinaryFormatter _BinaryFormatter = new BinaryFormatter();
            MemoryStream _MemoryStream = new MemoryStream();
            _BinaryFormatter.Serialize(_MemoryStream, _DataSetSurrogate);
            byte[] _buffer = _MemoryStream.ToArray();
            byte[] _Zipbuffer = Compress(_buffer);
            //
            _MemoryStream.Close();
            _MemoryStream.Dispose();
            //
            return _Zipbuffer;
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        static byte[] Decompress(byte[] buf)
        {
            long totalLength = 0;
            int size = 0;
            MemoryStream ms = new MemoryStream(), msD = new MemoryStream();
            ms.Write(buf, 0, buf.Length);
            ms.Seek(0, SeekOrigin.Begin);
            GZipStream zip;
            zip = new GZipStream(ms, CompressionMode.Decompress);
            byte[] db;
            bool readed = false;
            while (true)
            {
                size = zip.ReadByte();
                if (size != -1)
                {
                    if (!readed) readed = true;
                    totalLength++;
                    msD.WriteByte((byte)size);
                }
                else
                {
                    if (readed) break;
                }
            }
            zip.Close();
            db = msD.ToArray();
            msD.Close();

            return db;
        }

        /// <summary>
        /// 把压缩后的字节数组 解压并反序列化成DataSet
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns>DataSet</returns>
        public static DataSet GetDataSetByZipBytes(byte[] byteArray)
        {
            DataSetSurrogate sds = null;

            MemoryStream _MemoryStream = new MemoryStream(Decompress(byteArray));
            BinaryFormatter _BinaryFormatter = new BinaryFormatter();
            object o = _BinaryFormatter.Deserialize(_MemoryStream);
            sds = (DataSetSurrogate)o;
            return sds.ConvertToDataSet();
        }
        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="ds"></param>
        public static void CreateTable(DataSet ds, string TableName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("CREATE TABLE [" + TableName + "](");
            foreach (System.Data.DataRow item in ds.Tables[0].Rows)
            {
                sb.Append(string.Format("[{0}] [{1}] {2} {3},", item["ColName"], item["ColTypeName"], GetLen(item["ColTypeName"].ToString(), int.Parse(item["length"].ToString())), int.Parse(item["AllowNull"].ToString()) == 1 ? "NULL" : "NOT NULL"));
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(")");
            DbBase db = new DbBase();
            db.ExecuteNonQuery(sb.ToString());
        }
        public static void CreateTable(DataSet ds, string TableName, string DataBase)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("CREATE TABLE [" + TableName + "](");
            foreach (System.Data.DataRow item in ds.Tables[0].Rows)
            {
                sb.Append(string.Format("[{0}] [{1}] {2} {3},", item["ColName"], item["ColTypeName"], GetLen(item["ColTypeName"].ToString(), int.Parse(item["length"].ToString())), int.Parse(item["AllowNull"].ToString()) == 1 ? "NULL" : "NOT NULL"));
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(")");
            DbBase db = new DbBase();
            db.ChangeDB(DataBase).ExecuteNonQuery(sb.ToString());
        }
        /// <summary>
        /// 取得列宽
        /// </summary>
        /// <param name="ColTypeName"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetLen(string ColTypeName, int length)
        {
            if (ColTypeName == "int") return "";
            if (ColTypeName == "datetime") return "";
            return "(" + length + ")";
        }
        /// <summary>
        /// 判断表存在不
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static bool ExistTable(string TableName)
        {
            DbBase db = new DbBase();
            string sql = "select COUNT( *) from sysobjects where id = OBJECT_ID('[" + TableName + "]')";
            return (int)db.ExecuteScalar(sql) > 0;

        }
        /// <summary>
        /// 判断表存在不
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static bool ExistTable(string TableName, string DataBase)
        {
            DbBase db = new DbBase();
            string sql = "select COUNT( *) from sysobjects where id = OBJECT_ID('[" + TableName + "]')";
            return (int)db.ChangeDB(DataBase).ExecuteScalar(sql) > 0;

        }
    }
}