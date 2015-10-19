using System;
using System.Collections.Generic;
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

namespace ErpToDataCentreService.BLL
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

    }
}