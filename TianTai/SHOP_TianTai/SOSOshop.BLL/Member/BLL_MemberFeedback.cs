using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOSOshop.Model;
using System.Data;

namespace SOSOshop.BLL.Member
{
    /// <summary>
    /// 用户反馈
    /// </summary>
    public class BLL_MemberFeedback:Db
    {
        public BLL_MemberFeedback() { }

        /// <summary>
        /// 获取用户反馈信息集合
        /// </summary>
        /// <returns></returns>
        public List<MemberFeedbackModel> GetFeedbackInfo()
        {
            List<MemberFeedbackModel> objs = null;

            string sql = string.Format(@"SELECT m.Id, 
                                                    m.UID, 
                                                    m.HandPhone, 
                                                    m.Msg, 
                                                    m.CreateTime, 
                                                    m.Note , 
                                                    a.TrueName,
                                                    a.Province,
                                                    (SELECT name FROM region WHERE id=a.province) AS province_,
                                                    a.City,
                                                    (SELECT name FROM region WHERE id=a.city) AS city_,
                                                    a.Borough, 
                                                    (SELECT name FROM region WHERE id=a.borough) AS borough_,
                                                    m.EnterpriseName
                                            FROM dbo.MemberFeedback m,dbo.memberinfo a  
                                            WHERE m.UID=a.UID");
            DataTable dt = base.ExecuteTableForCache(sql, DateTime.Now.AddHours(1));

            if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
            {
                objs = new List<MemberFeedbackModel>();

                foreach (DataRow rowObj in dt.Rows)
                {
                    MemberFeedbackModel obj = new MemberFeedbackModel();
                    obj.Id = (int)rowObj["Id"];
                    obj.UID = (int)rowObj["UID"];
                    obj.HandPhone = rowObj["HandPhone"].ToString().Trim();
                    obj.Msg = rowObj["Msg"].ToString().Trim();
                    obj.CreateTime = (DateTime)rowObj["CreateTime"];
                    obj.Note = rowObj["Note"].ToString().Trim();
                    obj.TrueName = rowObj["TrueName"].ToString().Trim();
                    obj.Province = (int)rowObj["Province"];
                    obj.Province_ = rowObj["Province_"].ToString().Trim();
                    obj.City = (int)rowObj["City"];
                    obj.City_ = rowObj["City_"].ToString().Trim();
                    obj.Borough = (int)rowObj["Borough"];
                    obj.Borough_ = rowObj["Borough_"].ToString().Trim();
                    obj.EnterpriseName = rowObj["EnterpriseName"].ToString().Trim();

                    objs.Add(obj);
                }
            }

            return objs;
        }

        /*
        /// <summary>
        /// 获取指定用户反馈信息
        /// </summary>
        /// <returns></returns>
        public MemberFeedbackModel GetFeedbackInfo(int userId)
        {
            string sql = string.Format(@"SELECT m.Id, 
                                                    m.UID, 
                                                    m.HandPhone, 
                                                    m.EnterpriseId, 
                                                    m.Msg, 
                                                    m.CreateTime, 
                                                    m.Note , 
                                                    a.TrueName,
                                                    a.Province,
                                                    (SELECT name FROM region WHERE id=a.province) AS province_,
                                                    a.City,
                                                    (SELECT name FROM region WHERE id=a.city) AS city_,
                                                    a.Borough, 
                                                    (SELECT name FROM region WHERE id=a.borough) AS borough_,
                                                    b.name AS EnterpriseName
                                            FROM dbo.MemberFeedback m,dbo.memberinfo a ,dbo.DrugsBase_Enterprise b 
                                            WHERE m.UID=a.UID AND m.EnterpriseId=b.id AND m.UID={0}", userId);
            DataTable dt = base.ExecuteTableForCache(sql, DateTime.Now.AddHours(1));

            if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
            {
                MemberFeedbackModel obj = new MemberFeedbackModel();
                obj.Id = (int)dt.Rows[0]["Id"];
                obj.UID = (int)dt.Rows[0]["UID"];
                obj.HandPhone = dt.Rows[0]["HandPhone"].ToString().Trim();
                obj.EnterpriseId = (int)dt.Rows[0]["EnterpriseId"];
                obj.Msg = dt.Rows[0]["Msg"].ToString().Trim();
                obj.CreateTime = (DateTime)dt.Rows[0]["CreateTime"];
                obj.Note = dt.Rows[0]["Note"].ToString().Trim();
                obj.TrueName = dt.Rows[0]["TrueName"].ToString().Trim();
                obj.Province = (int)dt.Rows[0]["Province"];
                obj.Province_ = dt.Rows[0]["Province_"].ToString().Trim();
                obj.City = (int)dt.Rows[0]["City"];
                obj.City_ = dt.Rows[0]["City_"].ToString().Trim();
                obj.Borough = (int)dt.Rows[0]["Borough"];
                obj.Borough_ = dt.Rows[0]["Borough_"].ToString().Trim();
                obj.EnterpriseName = dt.Rows[0]["EnterpriseName"].ToString().Trim();

                return obj;
            }
            else
            {
                return null;
            }
        }
        */

        /// <summary>
        /// 分页查询用户反馈信息
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="whoProvince"></param>
        /// <param name="whoCity"></param>
        /// <param name="whoBorough"></param>
        /// <param name="orderBy"></param>
        /// <param name="enterpriseName"></param>
        /// <param name="uId"></param>
        /// <param name="trueName"></param>
        /// <param name="handPhone"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordTotal"></param>
        /// <returns></returns>
        public List<MemberFeedbackModel> QueryFeedbackInfo(string beginTime, string endTime, string whoProvince, string whoCity, string whoBorough, string orderBy,
                                              string enterpriseName, int uId, string trueName, string handPhone,
                                              int pageIndex, int pageSize, out int recordTotal)
        {

            recordTotal = 0;

            List<MemberFeedbackModel> rtnObjs = null;


            //组装where语句
            string whereSql = "";

            if (!string.IsNullOrEmpty(beginTime))
            {
                whereSql += " AND convert(char(10), CreateTime, 120)>='" + beginTime + "' ";
            }

            if (!string.IsNullOrEmpty(endTime))
            {
                whereSql += " AND convert(char(10), CreateTime, 120)<='" + endTime + "' ";
            }

            if (!string.IsNullOrEmpty(whoProvince))
            {
                whereSql += " AND Province_ LIKE '%" + whoProvince + "%' ";
            }

            if (!string.IsNullOrEmpty(whoCity))
            {
                whereSql += " AND City_ LIKE '%" + whoCity + "%' ";
            }

            if (!string.IsNullOrEmpty(whoBorough))
            {
                whereSql += " AND Borough_ LIKE '%" + whoBorough + "%' ";
            }

            if (!string.IsNullOrEmpty(enterpriseName))
            {
                whereSql += " AND EnterpriseName LIKE '%" + enterpriseName + "%' ";
            }

            if (uId > 0)
            {
                whereSql += " AND UID=" + uId + " ";
            }

            if (!string.IsNullOrEmpty(trueName))
            {
                whereSql += " AND TrueName LIKE '%" + trueName + "%' ";
            }

            if (!string.IsNullOrEmpty(handPhone))
            {
                whereSql += " AND HandPhone LIKE '%" + handPhone + "%' ";
            }

            //查询
            string sqlComm = @"SELECT m.Id, 
                                        m.UID, 
                                        m.HandPhone, 
                                        m.Msg, 
                                        m.CreateTime, 
                                        m.Note , 
                                        a.TrueName,
                                        a.Province,
                                        (SELECT name FROM region WHERE id=a.province) AS province_,
                                        a.City,
                                        (SELECT name FROM region WHERE id=a.city) AS city_,
                                        a.Borough, 
                                        (SELECT name FROM region WHERE id=a.borough) AS borough_,
                                        m.EnterpriseName
                                FROM dbo.MemberFeedback m,dbo.memberinfo a 
                                WHERE m.UID=a.UID " + whereSql;

            //查询总数
            string sqlTotal = string.Format("SELECT COUNT(*) FROM ({0}) c", sqlComm);
            DataTable dtTotal = base.ExecuteTableForCache(sqlTotal, DateTime.Now.AddMinutes(5));
            if (dtTotal == null || dtTotal.Rows.Count == 0)
            {
                return null;
            }

            recordTotal = (int)dtTotal.Rows[0][0];

            //分页查询
            string strOrder = (string.IsNullOrEmpty(orderBy)) ? "uId DESC" : orderBy;
            string sqlPage = string.Format("select top {0} * from (select row_number() over (order by {3}) as RowNumber,* from ({1}) aa) bb where rownumber >{2};", pageSize, sqlComm, pageSize * (pageIndex - 1), strOrder);
            DataTable dt = base.ExecuteTableForCache(sqlPage, DateTime.Now.AddMinutes(5));
            if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
            {
                rtnObjs = new List<MemberFeedbackModel>();
                foreach (DataRow rowObj in dt.Rows)
                {
                    MemberFeedbackModel tmp = new MemberFeedbackModel();
                    tmp.RowNumber = (long)rowObj["RowNumber"];
                    tmp.UID = (int)rowObj["UID"];
                    tmp.TrueName = rowObj["TrueName"].ToString();
                    tmp.HandPhone = rowObj["HandPhone"].ToString();
                    tmp.Province = (int)rowObj["Province"];
                    tmp.Province_ = rowObj["Province_"].ToString();
                    tmp.City = (int)rowObj["City"];
                    tmp.City_ = rowObj["City_"].ToString();
                    tmp.Borough = (int)rowObj["Borough"];
                    tmp.Borough_ = rowObj["Borough_"].ToString();
                    tmp.EnterpriseName = rowObj["EnterpriseName"].ToString();
                    tmp.Msg = rowObj["Msg"].ToString();
                    tmp.CreateTime = (DateTime)rowObj["CreateTime"];
                    tmp.Note = rowObj["Note"].ToString();


                    rtnObjs.Add(tmp);
                }
            }

            return rtnObjs;
        }

        /// <summary>
        /// 添加用户反馈记录
        /// </summary>
        /// <param name="handPhone">手机号</param>
        /// <param name="enterpriseName">企业名称</param>
        /// <param name="msg">反馈信息</param>
        /// <param name="note">备注</param>
        /// <returns></returns>
        public bool InsertFeedbackInfo(string handPhone, string enterpriseName, string msg, string note)
        {
            bool isSuccess = false;

            string sql = string.Format("INSERT INTO dbo.MemberFeedback (HandPhone, EnterpriseName, Msg, Note) VALUES('{0}', '{1}', '{2}', '{3}')",
                                        handPhone,
                                        enterpriseName,
                                        msg,
                                        note
                                       );
            int iRow = base.ExecuteNonQuery(sql);

            if (iRow > 0)
            {
                isSuccess = true;
            }

            return isSuccess;
        }

        /// <summary>
        /// 添加用户反馈记录
        /// </summary>
        /// <param name="userFeedback">用户反馈实体对象</param>
        /// <returns></returns>
        public bool InsertFeedbackInfo(MemberFeedbackModel userFeedback)
        {
            if (userFeedback == null)
            {
                return false;
            }

            bool isSuccess = InsertFeedbackInfo(userFeedback.HandPhone, userFeedback.EnterpriseName, userFeedback.Msg, userFeedback.Note);

            return isSuccess;
        }


        /// <summary>
        /// 更新用户反馈记录
        /// </summary>
        /// <param name="handPhone">手机号</param>
        /// <param name="enterpriseName">企业Id</param>
        /// <param name="msg">反馈信息</param>
        /// <param name="note">备注</param>
        /// <returns></returns>
        public bool UpdateFeedbackInfo(string handPhone, string enterpriseName, string msg, string note)
        {
            bool isSuccess = false;

            string sql = string.Format(@"UPDATE dbo.MemberFeedback SET (EnterpriseName='{1}', Msg='{2}', Note='{3}') WHERE HandPhone='{0}'",
                                        handPhone,
                                        enterpriseName,
                                        msg,
                                        note
                                       );
            int iRow = base.ExecuteNonQuery(sql);

            if (iRow > 0)
            {
                isSuccess = true;
            }

            return isSuccess;
        }

        /// <summary>
        /// 更新用户反馈记录
        /// </summary>
        /// <param name="userFeedback">用户反馈实体对象</param>
        /// <returns></returns>
        public bool UpdateFeedbackInfo(MemberFeedbackModel userFeedback)
        {
            if (userFeedback == null)
            {
                return false;
            }

            bool isSuccess = UpdateFeedbackInfo(userFeedback.HandPhone, userFeedback.EnterpriseName, userFeedback.Msg, userFeedback.Note);

            return isSuccess;
        }

        /// <summary>
        /// 删除指定用户的反馈记录
        /// </summary>
        /// <param name="handPhone">用户Id</param>
        /// <returns></returns>
        public bool DelFeedbackInfo(string handPhone)
        {
            bool isSuccess = false;

            string sql = string.Format(@"DEL FROM dbo.MemberFeedback WHERE HandPhone='{0}'", handPhone);
            int iRow = base.ExecuteNonQuery(sql);

            if (iRow > 0)
            {
                isSuccess = true;
            }

            return isSuccess;
        }
    }
}
