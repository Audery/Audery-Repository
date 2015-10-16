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
    /// <summary>
    /// 药品资质管理
    /// </summary>
    public class Qualification
    {
        [MongoDB.Bson.Serialization.Attributes.BsonIgnore]
        MongoHelper<Qualification> db = new MongoHelper<Qualification>();
        [MongoDB.Bson.Serialization.Attributes.BsonId]
        public string id { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public int Products_Id { get; set; }
        /// <summary>
        /// 资质类型
        /// </summary>
        public int QualType { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string file { get; set; }
        /// <summary>
        /// 下载次数
        /// </summary>
        public int dowCount { get; set; }

        /// <summary>
        /// 上传人
        /// </summary>
        public string editer { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime created { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime outOfDate { get; set; }
        /// <summary>
        /// 取得上传的列表
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public List<Qualification> GetList(int _id)
        {
            return db._mongoCollection.Find(Query.EQ("Products_Id", _id)).ToList();
        }

        /// <summary>
        /// 增加新的资质
        /// </summary>
        public void Insert()
        {
            var q = Query.And(Query.EQ("QualType", QualType), Query.EQ("Products_Id", Products_Id));
            if (db._mongoCollection.Count(q) == 0)
            {
                this.id = MongoDB.Bson.BsonObjectId.GenerateNewId().ToString();
                db._mongoCollection.Insert(this, SafeMode.True);
            }
            else
            {
                var m = db._mongoCollection.FindOne(q);
                this.id = m.id;
                if (!string.IsNullOrEmpty(m.file))
                {
                    db._gridFS.Delete(m.file);
                }

                Update();
            }
        }

        public void Update()
        {
            db.Update(this);
        }
        public Qualification GetModle(string id)
        {
            return db._mongoCollection.FindOneById(id);
        }

        public void Delte(string id)
        {
            var model = db._mongoCollection.FindOneById(id);
            if (model.file != null)
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
            return string.Join(",", db._mongoCollection.FindAll().Select(x => x.Products_Id));
        }


        public string AddFile(string path, string ext)
        {
            string temp = MongoDB.Bson.BsonObjectId.GenerateNewId().ToString() + ext;
            db._gridFS.Upload(path, temp);
            return temp;
        }

        /// <summary>
        /// 添加文件
        /// </summary>
        /// <param name="fs"></param>
        /// <returns></returns>
        public string AddFile(Stream fs, string ext)
        {
            string temp = MongoDB.Bson.BsonObjectId.GenerateNewId().ToString() + ext;
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
        /// 取得文件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetName(string file)
        {
            MongoHelper<Qualification> db = new MongoHelper<Qualification>();
            return GetName(db._mongoCollection.FindOne(Query.EQ("file", file)).QualType) + System.IO.Path.GetExtension(file);
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
                    ZipEntry newEntry = new ZipEntry(GetName(item));
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
        /// 取得药品上传的药检数量
        /// </summary>
        /// <param name="Products_Id"></param>
        /// <returns></returns>
        public static long GetCount(int Products_Id)
        {
            MongoHelper<Qualification> db = new MongoHelper<Qualification>();
            return db._mongoCollection.Count(Query.EQ("Products_Id", Products_Id));
        }
        /// <summary>
        /// 取得下载的次数
        /// </summary>
        /// <param name="Products_Id"></param>
        /// <returns></returns>
        public static long GetDowCount(int Products_Id)
        {
            MongoHelper<Qualification> db = new MongoHelper<Qualification>();
            if (db._mongoCollection.Find(Query.EQ("Products_Id", Products_Id)).Count() == 0)
            {
                return 0;
            }
            return db._mongoCollection.Find(Query.EQ("Products_Id", Products_Id)).Sum(x => x.dowCount);
        }
        public string GetName(int id)
        {
            return GetQualList().Where(x => x.id == id).First().name;
        }

        /// <summary>
        /// 取得资质列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.SearchModel> GetQualList()
        {
            List<Model.SearchModel> li = new List<Model.SearchModel>();
            li.Add(new Model.SearchModel { id = 1, name = "药品再注册批件/药品注册证" });
            li.Add(new Model.SearchModel { id = 2, name = "质量标准" });
            li.Add(new Model.SearchModel { id = 3, name = "补充申请批件" });
            li.Add(new Model.SearchModel { id = 4, name = "省(市)检" });
            li.Add(new Model.SearchModel { id = 5, name = "包装备案" });
            li.Add(new Model.SearchModel { id = 6, name = "说明书备案" });
            li.Add(new Model.SearchModel { id = 7, name = "生物制品批签发" });
            li.Add(new Model.SearchModel { id = 8, name = "进口药品(医药产品)注册证" });
            li.Add(new Model.SearchModel { id = 9, name = "进口药品补充申请批件" });
            li.Add(new Model.SearchModel { id = 10, name = "保健品批准证书" });
            li.Add(new Model.SearchModel { id = 11, name = "卫生安全评价报告" });
            li.Add(new Model.SearchModel { id = 12, name = "消毒剂和消毒器械卫生许可批件" });
            li.Add(new Model.SearchModel { id = 13, name = "全国工业产品生产许可证" });
            li.Add(new Model.SearchModel { id = 14, name = "特殊用途化妆品批件/非特殊用途化妆品备案凭证" });
            li.Add(new Model.SearchModel { id = 15, name = "医疗器械注册证/医疗器械产品制造认可表" });
            li.Add(new Model.SearchModel { id = 16, name = "疾控中心检验报告" });
            return li;
        }
    }


}
