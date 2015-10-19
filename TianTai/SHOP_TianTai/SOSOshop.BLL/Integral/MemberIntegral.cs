using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace SOSOshop.BLL.Integral
{
    /// <summary>
    /// 会员积分
    /// </summary>
    public class MemberIntegral : DbBase
    {
        public MemberIntegral()
        {
            base.ChangeShop();
        }

        /// <summary>
        /// 取得会员可用积分
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int GetRealityIntegral(int uid)
        {
            string sql = "SELECT realityIntegral FROM dbo.MemberIntegral WHERE uid=" + uid;
            object o = ExecuteScalar(sql);
            if (Library.Lang.DataValidator.isNULL(o))
            {
                o = 0;
            }
            return (int)o;
        }
        /// <summary>
        /// 建档成功
        /// </summary>
        /// <param name="code"></param>
        public void FilingStatus(string code)
        {
            string sql = string.Format("SELECT UID FROM dbo.memberinfo WHERE uid='{0}'", code);
            SOSOshop.BLL.DbBase bll = new DbBase();
            bll.ChangeShop();
            foreach (System.Data.DataRow item in bll.ExecuteTable(sql).Rows)
            {
                MemberIntegralLock ml = new MemberIntegralLock();
                if (ml.isAllow((int)item["UID"], MemberIntegralTemplateEnum.建档通过))
                {
                    //注册送积分(建档成功才开始送会员积分)
                    AddIntegral((int)item["UID"], 0, SOSOshop.BLL.Integral.MemberIntegralTemplateEnum.会员注册, "");
                    AddIntegral((int)item["UID"], 0, MemberIntegralTemplateEnum.建档通过, "");
                }
            }
        }
        /// <summary>
        /// 订单成交后累计计分
        /// </summary>
        /// <param name="orderid"></param>
        public void OrderSucceed(string orderid)
        {
            MemberIntegralLock ml = new MemberIntegralLock();
            if (ml.isAllow(orderid))
            {
                string sql = string.Format("SELECT TotalPrice,ReceiverId FROM dbo.Orders WHERE OrderId='{0}'", orderid);
                SOSOshop.BLL.DbBase bll = new DbBase();
                bll.ChangeShop();
                DataTable dt = bll.ExecuteTable(sql);
                decimal TotalPrice = decimal.Parse(bll.ExecuteScalar(string.Format("SELECT ISNULL(SUM(ProNum*ProPrice),0) FROM dbo.OrderProduct WHERE OrderId='{0}' AND Status IN (8,9,10)", orderid)).ToString());
                AddIntegral((int)dt.Rows[0]["ReceiverId"], TotalPrice, MemberIntegralTemplateEnum.成交订单, orderid, orderid);
            }
        }

        /// <summary>
        /// 增加积分
        /// </summary>
        /// <param name="uid">会员ID</param>
        /// <param name="price">如果是订单则传入订单金额</param>
        /// <param name="me">模板类型</param>
        /// <param name="msg">如果是订单则传入订单号</param>
        public void AddIntegral(int uid, decimal price, MemberIntegralTemplateEnum me, string msg, string orderid = "")
        {
            string sql = "";
            MemberIntegralLock ml = new MemberIntegralLock();
            if (!ml.isAllow(uid, me)) return;
            decimal dc = 0;
            if (me == MemberIntegralTemplateEnum.成交订单)
            {

                // OTC
                sql = "SELECT CompanyClass FROM dbo.memberaccount WHERE UID=" + uid;
                string obj = ExecuteScalar(sql) as string;
                if (obj == null) return;
                var cc = SOSOshop.Model.CompanyClass.GetModel(obj);
                if (cc.Price == "Price_02")
                {
                    DateTime time = (DateTime)ExecuteScalar(string.Format("SELECT ShopDate FROM dbo.Orders WHERE OrderId='{0}'", orderid));
                    OtcIntegralDay bll = new OtcIntegralDay();
                    var m = bll.GetList(((int)time.DayOfWeek).ToString());
                    if (m.Count > 0)
                    {
                        var model = m.First();
                        if (model.state)
                        {
                            dc = price * model.multiple;
                        }
                    }
                }
                else //批发
                {
                    dc = new MemberIntegralTemplate().GetIntegral(me);
                    if (dc == 0) return;
                    dc = price * dc;
                }
            }
            else
            {
                dc = new MemberIntegralTemplate().GetIntegral(me);
            }
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction tran = conn.BeginTransaction();
                try
                {
                    //更新积分表
                    sql = string.Format(@"IF NOT EXISTS( SELECT * FROM dbo.MemberIntegral WHERE uid={0})
INSERT INTO MemberIntegral VALUES({0},{1},{1})
ELSE
UPDATE MemberIntegral SET integral=integral+{1},realityIntegral=realityIntegral+{1} WHERE uid={0}", uid, (int)dc);
                    db.ExecuteNonQuery(db.GetSqlStringCommand(sql), tran);
                    SOSOshop.BLL.Integral.MemberIntegralDetail bll = new MemberIntegralDetail();
                    SOSOshop.Model.Integral.MemberIntegralDetail model = new Model.Integral.MemberIntegralDetail();
                    model.action = "增加";
                    model.created = DateTime.Now;
                    model.integral = (int)dc;
                    model.remarks = string.Format("{0}:{1}", me, msg);
                    model.uid = uid;
                    bll.Add(model, tran);
                    tran.Commit();
                    ml.created = (DateTime)model.created;
                    ml.mte = me;
                    ml.uid = uid;
                    ml.orderid = orderid;
                    ml.insert();
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    throw e;
                }
            }
        }
        /// <summary>
        /// 赠送积分
        /// </summary>
        /// <param name="uid">会员ID</param>
        /// <param name="integral">积分</param>
        /// <param name="remarks">描述</param>
        /// <param name="isFanHuan">返回积分不更新积分表</param>
        public void PresentIntegral(int uid, int integral, string remarks, bool isFanHuan = false)
        {
            MemberIntegralTemplateEnum me = MemberIntegralTemplateEnum.赠送积分;
            MemberIntegralLock ml = new MemberIntegralLock();
            if (!ml.isAllow(uid, me)) return;
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction tran = conn.BeginTransaction();
                try
                {
                    //更新积分表
                    if (!isFanHuan)
                    {
                        string sql = string.Format(@"IF NOT EXISTS( SELECT * FROM dbo.MemberIntegral WHERE uid={0})
INSERT INTO MemberIntegral VALUES({0},{1},{1})
ELSE
UPDATE MemberIntegral SET integral=integral+{1},realityIntegral=realityIntegral+{1} WHERE uid={0}", uid, integral);
                        db.ExecuteNonQuery(db.GetSqlStringCommand(sql), tran);
                    }
                    SOSOshop.BLL.Integral.MemberIntegralDetail bll = new MemberIntegralDetail();
                    SOSOshop.Model.Integral.MemberIntegralDetail model = new Model.Integral.MemberIntegralDetail();
                    model.action = "增加";
                    model.created = DateTime.Now;
                    model.integral = integral;
                    model.remarks = remarks;
                    model.uid = uid;
                    bll.Add(model, tran);
                    tran.Commit();
                    ml.created = (DateTime)model.created;
                    ml.mte = me;
                    ml.uid = uid;
                    ml.insert();
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    throw e;
                }
            }
        }
    }
}
