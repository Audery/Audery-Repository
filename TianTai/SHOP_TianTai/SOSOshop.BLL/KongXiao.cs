using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using MongoDB.Driver;

namespace SOSOshop.BLL
{
    /// <summary>
    /// 四部控销申请表
    /// </summary>
    public class KongXiao
    {
        #region Model
        [BsonId]
        public string id { get; set; }
        /// <summary>
        /// 药店名称
        /// </summary>
        [Display(Name = "用户名")]
        [Required(ErrorMessage = "必填")]
        public string Title { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public string address { get; set; }
        /// <summary>
        /// 主要负责人姓名
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public string TrueName { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        [Required(ErrorMessage = "必填")]
        //[RegularExpression(Model.VerifyRegular.mobile, ErrorMessage = "手机格式不正确")]
        public string mobile { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthdate { get; set; }
        /// <summary>
        /// 职称
        /// </summary>
        public string technical { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string sex { get; set; }
        /// <summary>
        /// 微信
        /// </summary>
        public string weixin { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        public string education { get; set; }
        /// <summary>
        /// 业务负责人
        /// </summary>
        public string contactName { get; set; }
        /// <summary>
        /// 业务负责人手机
        /// </summary>
        public string contacMobile { get; set; }
        /// <summary>
        /// 业务负责人QQ
        /// </summary>
        public string contacQQ { get; set; }
        /// <summary>
        /// 业务负责人出生日期
        /// </summary>
        public DateTime contacBirthdate { get; set; }
        /// <summary>
        /// 业务负责人职称
        /// </summary>
        public string contacTechnical { get; set; }
        /// <summary>
        /// 业务负责人性别
        /// </summary>
        public string contacSex { get; set; }
        /// <summary>
        /// 业务负责人微信
        /// </summary>
        public string contacWeixin { get; set; }
        /// <summary>
        /// 联系人学历
        /// </summary>
        public string contacEducation { get; set; }
        /// <summary>
        /// 店铺数量
        /// </summary>
        public int shopCount { get; set; }
        /// <summary>
        /// 们店面积(m2)
        /// </summary>
        public int shopArea { get; set; }
        /// <summary>
        /// 经营范围(西药 中药材 食品 保健品)
        /// </summary>
        public string businessScope { get; set; }
        /// <summary>
        /// 本店入会后希望封闭的区域(①本乡（镇）②本店所在街道 ③本店周围500米内④本店1000米内 ⑤无所谓)
        /// </summary>
        public string limitArea { get; set; }
        /// <summary>
        /// 本店现在的薄弱环节(①品种特色性不强②毛利率不高人员培训不够④店面装修老化⑤其他，如：)
        /// </summary>
        public string deficiency { get; set; }
        /// <summary>
        /// 本店现在的薄弱环节 其它的文字
        /// </summary>
        public string deficiencyOther { get; set; }
        /// <summary>
        /// 兴趣爱号
        /// </summary>
        public string interest { get; set; }
        /// <summary>
        /// 申请人签名
        /// </summary>
        public string signature { get; set; }
        /// <summary>
        /// 未处理/已通过/未通过
        /// </summary>
        public string state { get; set; }
        public DateTime created { get; set; }
        #endregion
        MongoHelper<KongXiao> db = new MongoHelper<KongXiao>();
        public void insert()
        {
            this.created = DateTime.Now;
            db.Insert(this);
        }
        public List<KongXiao> GetList(int PageSize, int PageIndex, out int recordCount, out int pageCount, bool order, string orderField, bool like, string whereField, string whereString, string state)
        {

            IMongoQuery q = null;
            if (state != "全部")
            {
                q = Query.EQ("state", state);
            }
            if (!string.IsNullOrEmpty(whereString))
            {
                if (like)
                {
                    if (q != null)
                    {
                        q = Query.And(q, Query.EQ(whereField, whereString));
                    }
                    else
                    {
                        q = Query.EQ(whereField, whereString);
                    }
                }
                else
                {
                    if (q != null)
                    {
                        q = Query.And(q, Query.Matches(whereField, whereString));
                    }
                    else
                    {
                        q = Query.Matches(whereField, whereString);
                    }
                }
            }
            recordCount = (int)db._mongoCollection.Count(q);
            pageCount = recordCount / PageSize;
            if ((recordCount % PageSize) != 0) pageCount++;
            return db._mongoCollection.Find(q).OrderByDescending(x => x.created).Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();

        }
        public void Delte(string id)
        {
            db._mongoCollection.Remove(Query.EQ("_id", id), RemoveFlags.Single, SafeMode.True);
        }
        public KongXiao GetModel(string id)
        {
            return db._mongoCollection.FindOneById(id);
        }
        public void UpdateState(string id, string state)
        {
            db._mongoCollection.Update(Query.EQ("_id", id), Update.Set("state", state));
        }
    }
}
