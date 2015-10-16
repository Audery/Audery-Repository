using System;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace SOSOshop.BLL.Service
{
    public class Enterprise2Level
    {
        Database erp = DatabaseFactory.CreateDatabase("ConnectionStringERP");
        Database db = DatabaseFactory.CreateDatabase("ConnectionStringBase");
        Database v3db = DatabaseFactory.CreateDatabase("ConnectionString");

        private string ID { get; set; }

     

        /// <summary>
        /// 是否为新单位
        /// </summary>
        /// <returns></returns>
        private bool IsNewInfo()
        {
            string sql = string.Format("select count(id) from DrugsBase_Enterprise where code='{0}'", ID);
            DbCommand comm = db.GetSqlStringCommand(sql);
            return int.Parse(db.ExecuteDataSet(comm).Tables[0].Rows[0][0].ToString()) == 0;
        }


        /// <summary>
        /// 确定是否可以更新单位内容，如果没有客户类别或是一级单位的不进行处理
        /// </summary>
        /// <param name="khlb">客户类别</param>
        /// <param name="dwlx">单位级别</param>
        /// <returns></returns>
        private bool IsExecute(string khlb, string dwlx)
        {
            if (string.IsNullOrEmpty(khlb.Trim()))
            {
                SOSOshop.BLL.Logs.Log.LogServiceAdd("客户类别为空[" + ID + "]", 0, "往来单位消息处理2", ID, "", 2);
                return false;
            }
            if (string.IsNullOrEmpty(dwlx.Trim()))
            {
                return false;
            }
            if ("一级".Equals(dwlx.Trim()))
            {
                return false;
            }
            return true;
        }

       


        private int MClass(int c)
        {
            int ret = -1;
            switch (c)
            {
                case 1:
                    ret = 0;
                    break;
                case 2:
                    ret = 0;
                    break;
                case 3:
                    ret = 0;
                    break;
                case 5:
                    ret = 0;
                    break;
                case 7:
                    ret = -1;
                    break;
                default:
                    ret = 1;
                    break;
            }
            //Console.WriteLine("class={0} ", ret);
            return ret;
        }

        /// <summary>
        /// 企业类型
        /// </summary>
        /// <param name="kehblb"></param>
        /// <returns></returns>
        private int buyIncType(string kehblb)
        {
            int ret = 7;
            //Console.WriteLine("Code={0} ,客户类别={1}", ID, kehblb);
            switch (kehblb.Trim())
            {
                case "生产企业":
                    ret = 1;
                    break;
                case "商业公司A":
                    ret = 2;
                    break;
                case "商业公司B":
                    ret = 2;
                    break;
                case "零售连锁":
                    ret = 5;
                    break;
                case "零售药店":
                    ret = 4;
                    break;
                case "单体/加盟药店":
                    ret = 4;
                    break;
                case "民营医院":
                    ret = 3;
                    break;
                case "公立医院":
                    ret = 3;
                    break;
                case "诊所":
                    ret = 6;
                    break;
                default:
                    ret = 7;
                    break;
            }
            /*
            pf.Add("零售连锁", "0000100003000030000300003");//CRM 药店/连锁公司/意向客户
            pf.Add("商业公司A", "00001000030000200005");//CRM 批发企业/意向客户
            pf.Add("商业公司B", "00001000030000200005");//CRM 批发企业/意向客户
            NameValueCollection otc = new NameValueCollection();
            otc.Add("零售药店", "0000100003000030000200003");//CRM 药店/单体药店/意向客户
            otc.Add("民营医院", "0000100003000040000200003");//CRM 医院/意向客户
            otc.Add("公立医院", "0000100003000040000200003");//CRM 医院/意向客户
            otc.Add("生产企业", "00001000030000100003");//CRM 生产企业/意向客户
            otc.Add("诊所", "0000100003000040000100003");//CRM 诊所/意向客户*/
            return ret;
        }

        


        /// <summary>
        /// 修改企业库字段
        /// </summary>
        /// <returns></returns>
        private string EnterpriseFieldsModify()
        {
            StringBuilder s = new StringBuilder();
            s.Append("update DrugsBase_Enterprise set ");
            s.Append("Code=@Code,");
            s.Append("PYJM=@PYJM,");
            s.Append("Name=@Name,");
            s.Append("Fax=@Fax,");
            s.Append("Email=@Email,");
            s.Append("ShortName=@ShortName,");
            s.Append("TrueName=@TrueName,");
            s.Append("MobilePhone=@MobilePhone,");
            s.Append("OfficePhone=@OfficePhone,");
            s.Append("Province=(select top 1 id from Region where Name=@Province and Depth=1),");
            s.Append("City=(select top 1 id from Region where (Name=@Province and Depth=1) and (Name=@City and Depth=2)),");
            s.Append("Borough=(select top 1 id from Region where (Name=@Province and Depth=1) and (Name=@City and Depth=2) and (Name=@Borough and Depth=3)),");
            s.Append("Address=@Address,");
            s.Append("Money=@Money,");
            s.Append("LegalRepresentative=@LegalRepresentative,");
            s.Append("Nature=@Nature,");
            s.Append("Limits=@Limits,");
            s.Append("IncType=@IncType,");
            s.Append("buyIncType=@buyIncType,");
            s.Append("Status=@Status,");
            s.Append("TaxpayerID=@TaxpayerID,");
            s.Append("SellFilingStatus=@SellFilingStatus,");
            s.Append("BuyFilingStatus=@BuyFilingStatus,");
            s.Append("IsSell=1,");
            s.Append("IsBuy=1");
            s.AppendFormat(" where Code='{0}'", ID);
            return s.ToString();
        }

        /// <summary>
        /// 发送短信给用户
        /// </summary>
        public void SendSMS()
        {
            string sql = string.Format("select id from DrugsBase_Enterprise where code='{0}'", ID);

            DbCommand comm = db.GetSqlStringCommand(sql);
            DataTable dt = db.ExecuteDataSet(comm).Tables[0];
            var sms = new SOSOshop.MSG.Sms();
            foreach (DataRow dr in dt.Rows)
            {
                string sqlv3 = string.Format("select MobilePhone from memberaccount where uid in (select uid from memberinfo where Parents like('%{0}%'))", dr["id"]);
                DbCommand v3comm = v3db.GetSqlStringCommand(sqlv3);
                DataTable v3dt = v3db.ExecuteDataSet(v3comm).Tables[0];
                foreach (DataRow dv in v3dt.Rows)
                {
                    string MobilePhone = (string)dv["MobilePhone"];
                    if (string.IsNullOrEmpty(MobilePhone))
                    {
                        string SmsMsg = "您的首营资料已经通过建档审核，已经开通您在101商城的交易权限及价格查看权限！";
                        string from = "系统";
                        string to = MobilePhone;
                        Sms.SendAndSaveDataBase(MobilePhone, SmsMsg, from, to);
                    }
                }
            }
        }

        /// <summary>
        /// 判断指定的单位GSP是否已经建档
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool IsBuyFilingStatus(string code)
        {
            string sql = string.Format("Select BuyFilingStatus FROM DrugsBase_Enterprise WHERE Code='{0}'", code);
            DbCommand comm = db.GetSqlStringCommand(sql);
            DataTable dt = db.ExecuteDataSet(comm).Tables[0];
            //DataTable dt = db.ExecuteTableForCache(sql);
            if (dt == null)
            {
                return false;
            }
            else
            {
                try
                {
                    return int.Parse(dt.Rows[0][0].ToString()) == 1;
                }
                catch
                {
                    return false;
                }
            }
        }
       
    }
}
