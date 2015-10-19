using System;
using MongoDB;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System.Data;
using System.Windows.Forms;
namespace SOSOshop.BLL.Report
{
    public class DrugTestingReport
    {
        MongoHelper<DrugTestingReport> db = new MongoHelper<DrugTestingReport>();
        [MongoDB.Bson.Serialization.Attributes.BsonId]
        public string id { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public int Products_Id { get; set; }
        /// <summary>
        /// 药检来源
        /// </summary>
        public int iden { get; set; }
        /// <summary>
        /// 批号
        /// </summary>
        public string pihao { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string file { get; set; }
        /// <summary>
        /// 下载次数
        /// </summary>
        public int dowCount { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime created { get; set; }
        /// <summary>
        /// 取得上传的列表
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public List<DrugTestingReport> GetList(int _id)
        {
            return db._mongoCollection.Find(Query.EQ("Products_Id", _id)).ToList();
        }
        /// <summary>
        /// 取得上传的列表
        /// </summary>
        /// <param name="Products_Id"></param>
        /// <param name="pihao"></param>
        /// <returns></returns>
        public List<DrugTestingReport> GetList(int Products_Id, string pihao = null)
        {
            if (string.IsNullOrEmpty(pihao))
                return db._mongoCollection.Find(Query.EQ("Products_Id", Products_Id)).ToList();
            else
                return db._mongoCollection.Find(Query.And(Query.EQ("Products_Id", Products_Id), Query.EQ("pihao", pihao))).ToList();
        }
        /// <summary>
        /// 取得上传的列表
        /// </summary>
        /// <returns></returns>
        public string[] GetFileList(int[] Products_Id, string[] pihao, bool containsId = true)
        {
            MongoHelper<DrugTestingReport> db = new MongoHelper<DrugTestingReport>();
            List<string> vals = new List<string>();
            List<MongoDB.Bson.BsonValue> values1 = new List<MongoDB.Bson.BsonValue>();
            List<MongoDB.Bson.BsonValue> values2 = new List<MongoDB.Bson.BsonValue>();
            if (Products_Id != null && pihao != null && Products_Id.Length == pihao.Length)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                foreach (int s in Products_Id) values1.Add(s);
                foreach (string s in pihao) values2.Add(s.Trim());
                foreach (DrugTestingReport model in db._mongoCollection.Find(Query.And(Query.In("Products_Id", values1), Query.In("pihao", values2))))
                {
                    if (string.IsNullOrEmpty(model.file)) continue;
                    if (dic.ContainsKey(model.Products_Id + model.pihao.Trim()))
                    {
                        dic[model.Products_Id + model.pihao.Trim()] = dic[model.Products_Id + model.pihao.Trim()] + "," + (containsId ? model.id + "-" : "") + model.file;
                    }
                    else
                    {
                        dic.Add(model.Products_Id + model.pihao.Trim(), (containsId ? model.id + "-" : "") + model.file);
                    }
                }
                for (int i = 0; i < Products_Id.Length; i++)
                {
                    vals.Add(dic.ContainsKey(Products_Id[i] + pihao[i].Trim()) ? dic[Products_Id[i] + pihao[i].Trim()] : "");
                }
            }
            return vals.ToArray();
        }
        /// <summary>
        /// 未下载过
        /// </summary>
        public string[] GetFileListByNotDownload(int uid, int[] Products_Id, string[] pihao, bool containsId = true)
        {
            MongoHelper<DrugTestingReport> db = new MongoHelper<DrugTestingReport>();
            List<string> vals = new List<string>();
            List<MongoDB.Bson.BsonValue> values1 = new List<MongoDB.Bson.BsonValue>();
            List<MongoDB.Bson.BsonValue> values2 = new List<MongoDB.Bson.BsonValue>();
            List<MongoDB.Bson.BsonValue> values3 = new List<MongoDB.Bson.BsonValue>();
            if (Products_Id != null && pihao != null && Products_Id.Length == pihao.Length)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                foreach (int s in Products_Id) values1.Add(s);
                foreach (string s in pihao) values2.Add(s.Trim());
                foreach (string s in DrugTestingReportDownloadCount.GetDrugTestingReport_Id(uid)) values3.Add(s);
                foreach (DrugTestingReport model in db._mongoCollection.Find(Query.And(Query.In("Products_Id", values1), Query.In("pihao", values2), Query.NotIn("_id", values3))))
                {
                    if (string.IsNullOrEmpty(model.file)) continue;
                    if (dic.ContainsKey(model.Products_Id + model.pihao.Trim()))
                    {
                        dic[model.Products_Id + model.pihao.Trim()] = dic[model.Products_Id + model.pihao.Trim()] + "," + (containsId ? model.id + "-" : "") + model.file;
                    }
                    else
                    {
                        dic.Add(model.Products_Id + model.pihao.Trim(), (containsId ? model.id + "-" : "") + model.file);
                    }
                }
                for (int i = 0; i < Products_Id.Length; i++)
                {
                    vals.Add(dic.ContainsKey(Products_Id[i] + pihao[i].Trim()) ? dic[Products_Id[i] + pihao[i].Trim()] : "");
                }
            }
            return vals.ToArray();
        }

        /// <summary>
        /// 增加新的药检测
        /// </summary>
        public void Insert()
        {
            this.id = MongoDB.Bson.BsonObjectId.GenerateNewId().ToString();
            this.pihao = this.pihao.Trim();
            db._mongoCollection.Insert(this, SafeMode.True);
        }
        public DrugTestingReport GetModle(string id)
        {
            return db._mongoCollection.FindOneById(id);
        }
        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            var model = db._mongoCollection.FindOne(Query.And(Query.EQ("pihao", pihao), Query.EQ("iden", iden), Query.EQ("Products_Id", Products_Id)));
            if (model == null)
            {
                Insert();
            }
            else
            {
                if (this.file == "")
                {
                    db._mongoCollection.Update(Query.EQ("_id", model.id), MongoDB.Driver.Builders.Update.Set("pihao", pihao));
                }
                else
                {
                    db._mongoCollection.Update(Query.EQ("_id", model.id), MongoDB.Driver.Builders.Update.Set("pihao", pihao).Set("file", file));
                    db._gridFS.Delete(model.file);
                }
            }
        }
        public void Delte(string id)
        {
            var model = db._mongoCollection.FindOneById(id);
            db._gridFS.Delete(model.file);
            db._mongoCollection.Remove(Query.EQ("_id", id), RemoveFlags.Single);
        }

        /// <summary>
        /// 更新文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="file"></param>
        public void Update(string id, string file)
        {
            var u = MongoDB.Driver.Builders.Update.Set("file", file).AddToSet("created", DateTime.Now);
            db._mongoCollection.Update(Query.EQ("_id", id), u);
        }
        /// <summary>
        /// 更新下载次数
        /// </summary>
        /// <param name="id"></param>
        public void UpdateDowCount(string id)
        {
            db._mongoCollection.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update.Inc("dowCount", 1));
        }
        /// <summary>
        /// 取得已上传的商品id
        /// </summary>
        /// <returns></returns>
        public string GetProducts_Id()
        {
            return string.Join(",", db._mongoCollection.FindAll().Where(x => x.file != "").Select(x => x.Products_Id));
        }
        /// <summary>
        /// 判断是否上传
        /// </summary>
        /// <param name="Products_Id"></param>
        /// <param name="pihao"></param>
        /// <returns></returns>
        public bool Exist(int Products_Id, string pihao)
        {
            return db._mongoCollection.Count(Query.And(Query.EQ("Products_Id", Products_Id), Query.EQ("pihao", pihao))) > 0;
        }
        public string AddFile(string path)
        {
            string temp = MongoDB.Bson.BsonObjectId.GenerateNewId().ToString() + ".jpg";
            db._gridFS.Upload(path, temp);
            return temp;
        }
        /// <summary>
        /// 添加文件
        /// </summary>
        /// <param name="fs"></param>
        /// <returns></returns>
        public string AddFile(Stream fs)
        {
            string temp = MongoDB.Bson.BsonObjectId.GenerateNewId().ToString() + ".jpg";
            db._gridFS.Upload(fs, temp);
            return temp;
        }
        /// <summary>
        /// 取得文件流
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Stream GetFile(string name)
        {
            return db._gridFS.FindOne(name).OpenRead();
        }      
        /// <summary>
        /// 打包下载
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public MemoryStream GetPackageFile(string names, int userid)
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            ZipOutputStream newzipstream = new ZipOutputStream(stream);
            newzipstream.IsStreamOwner = false;
            newzipstream.SetLevel(6);
            string[] f = names.Split(',');
            foreach (var item in f)
            {
                using (var newstream = GetFile(item))
                {
                    ZipEntry newEntry = new ZipEntry(item);
                    byte[] setbuffer = new byte[newstream.Length];
                    newstream.Read(setbuffer, 0, setbuffer.Length);//读入文件
                    //设置时间-长度
                    newEntry.DateTime = DateTime.Now;
                    newEntry.Size = newstream.Length;
                    newzipstream.PutNextEntry(newEntry);//压入
                    Crc32 objCrc32 = new Crc32();
                    objCrc32.Reset();
                    objCrc32.Update(setbuffer);
                    newEntry.Crc = objCrc32.Value;
                    newzipstream.Write(setbuffer, 0, setbuffer.Length);
                    Inc(item);                    
                }

            }
            newzipstream.Finish();
            return stream;
        }
        /// <summary>
        /// 增加一次下载数
        /// </summary>
        /// <param name="file"></param>
        public void Inc(string file)
        {
            db._mongoCollection.Update(Query.EQ("file", file), MongoDB.Driver.Builders.Update.Inc("dowCount", 1));
        }
        /// <summary>
        /// 增加一次下载数
        /// </summary>
        /// <param name="file"></param>
        public void Inc(string file, int userid)
        {
            DrugTestingReportDownloadCount bll = new DrugTestingReportDownloadCount();
            bll.Created = DateTime.Now;
            bll.DrugTestingReport_Id = db._mongoCollection.FindOne(Query.EQ("file", file)).id;
            bll.uid = userid;
            bll.Insert();
            db._mongoCollection.Update(Query.EQ("file", file), MongoDB.Driver.Builders.Update.Inc("dowCount", 1));
        }
        /// <summary>
        /// 取得药品上传的药检数量
        /// </summary>
        /// <param name="Products_Id"></param>
        /// <returns></returns>
        public static long GetCount(int Products_Id)
        {
            MongoHelper<DrugTestingReport> db = new MongoHelper<DrugTestingReport>();
            return db._mongoCollection.Count(Query.EQ("Products_Id", Products_Id));
        }
        /// <summary>
        /// 取得下载的次数
        /// </summary>
        /// <param name="Products_Id"></param>
        /// <returns></returns>
        public static long GetDowCount(int Products_Id)
        {
            MongoHelper<DrugTestingReport> db = new MongoHelper<DrugTestingReport>();
            return db._mongoCollection.Find(Query.EQ("Products_Id", Products_Id)).Sum(x => x.dowCount);
        }
        /// <summary>
        /// 取得下载的次数
        /// </summary>
        /// <param name="Products_Id"></param>
        /// <param name="pihao"></param>
        /// <returns></returns>
        public static long GetDowCount(int Products_Id, string pihao)
        {
            MongoHelper<DrugTestingReport> db = new MongoHelper<DrugTestingReport>();
            return db._mongoCollection.Find(Query.And(Query.EQ("Products_Id", Products_Id), Query.EQ("pihao", pihao))).Sum(x => x.dowCount);
        }
        /// <summary>
        /// 上载金仁的药检报告到我们的药检报告系统
        /// </summary>
        //public static void DownloadYJBG10002()
        //{
        //    SOSOshop.BLL.DbBase bll = new DbBase();
        //    bll.ChangeData_Centre();
        //    string sql = "SELECT TOP 50 * FROM Data_Centre_File.dbo.YJBG10002 WHERE state=0 AND PRODUCT_ID IN (SELECT t_id FROM Data_Centre.dbo.Link WHERE iden=10002) ORDER BY id DESC";
        //    DataTable dt = bll.ExecuteTable(sql);
        //    foreach (DataRow item in dt.Rows)
        //    {
        //        //图片加水印章
        //        var image = (byte[])item["IMAGE"];
        //        using (System.IO.MemoryStream ms = new MemoryStream(image))
        //        {
        //            using (var b = SOSOshop.BLL.Common.ImageWater.AddStreamWatermarkAsJPG(ms, Application.StartupPath + "\\101zhang.png", SOSOshop.BLL.Common.ImageWater.MarkPosition.MP_Left_Top))
        //            {
        //                string TempPath = Application.StartupPath + "\\" + MongoDB.Bson.BsonObjectId.GenerateNewId().ToString();
        //                b.Save(TempPath);
        //                int spid = (int)bll.ExecuteScalar(string.Format("SELECT spid FROM Data_Centre.dbo.Link WHERE iden=10002 AND t_id='{0}'", item["PRODUCT_ID"]));
        //                DrugTestingReport dtr = new DrugTestingReport();
        //                dtr.created = DateTime.Now;
        //                dtr.dowCount = 0;
        //                dtr.file = dtr.AddFile(TempPath);
        //                dtr.iden = 10002;
        //                dtr.pihao = item["PIHAO"] as string;
        //                dtr.Products_Id = spid;
        //                dtr.Update();
        //                Library.IO.File.DeleFile(TempPath);
        //            }
        //        }
        //        sql = string.Format("UPDATE Data_Centre_File.dbo.YJBG10002 SET state=1 WHERE ID='{0}'", item["ID"]);
        //        bll.ExecuteNonQuery(sql);
        //        System.Threading.Thread.Sleep(5000);
        //    }
        //}

        /// <summary>
        /// 将以前未建档，现在又建档的药检报告标记为未处理
        /// </summary>
        //public static void UpdateYJBG10002()
        //{
        //    try
        //    {
        //        SOSOshop.BLL.DbBase bll = new DbBase();
        //        bll.ChangeData_Centre();
        //        bll.ExecuteNonQuery("UPDATE Data_Centre_File.dbo.YJBG10002 SET state=0 WHERE PRODUCT_ID IN (SELECT t_id FROM Data_Centre.dbo.Link WHERE iden=10002)AND state=-1");
        //    }
        //    catch (Exception ex)
        //    {
        //        BLL.Logs.Log.LogServiceAdd(ex.Message, 0, "", "", ex.ToString(), 2);
        //    }
        //}
    }
}
