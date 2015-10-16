using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
namespace _101shop.admin.v3.Api
{
    /// <summary>
    /// Customer 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class API : System.Web.Services.WebService
    {
        private const string key = "7307C5E7-CE7F-43DC-B3C9-520C76CAC4CA";

        /// <summary>
        /// 批量添加客户信息
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="authKey"></param>
        /// <returns></returns>
        [WebMethod]
        public bool SendMember(DataSet ds, string authKey)
        {
            bool ret = false;
            if (authKey == key)
            {
                try
                {
                   
                    SOSOshop.BLL.DbBase db = new SOSOshop.BLL.DbBase();
                    var dt = db.ExecuteTable("SELECT * FROM _memberinfo");
                   
                    foreach (DataRow item in ds.Tables[0].AsEnumerable().Except(dt.AsEnumerable()))
                    {
                        adduser(item);
                    }
                    db.ExecuteNonQuery("TRUNCATE TABLE _memberinfo");
                    List<string> c = new List<string>();
                    foreach (DataColumn item in dt.Columns)
                    {
                        c.Add(item.ColumnName);
                    }
                    db.BulkToDB(ds.Tables[0], "_memberinfo", c.ToArray());
                }
                catch (Exception ex)
                {
                    SOSOshop.BLL.Logs.Log.LogServiceAdd(ex.Message, 0, "", "api.asmx SendMember()", ex.StackTrace, 2);
                }

            }
            return ret;
        }
        /// <summary>
        /// 添加近效期产品
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="authKey"></param>
        /// <returns></returns>
        [WebMethod]
        public bool AddExpirationTimeProduct(DataSet ds, string authKey)
        {
            bool ret = false;
            if (authKey == key)
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            string Extended1 = dr["Extended1"] == null ? "" : dr["Extended1"].ToString();
                            decimal Stock = dr["Stock"] == null ? 0 : Convert.ToDecimal(dr["Stock"]);
                            decimal Price = dr["Price"] == null ? 0 : Convert.ToDecimal(dr["Price"]);
                            string ExpirationTime = dr["ExpirationTime"] == null ? "" : dr["ExpirationTime"].ToString();
                            string Erp_ID = dr["Erp_ID"] == null ? "" : dr["Erp_ID"].ToString();
                            string Goods_Unit = dr["Goods_Unit"] == null ? "" : dr["Goods_Unit"].ToString();

                            var obj = SOSOshop.BLL.Product.ExpirationTime.CreateInstance().ExecuteScalar(string.Format("SELECT ID FROM Product_ExpirationTime WHERE Erp_ID='{0}'", Erp_ID));
                            string SqlStr = string.Empty;
                            if (obj != null)
                            {
                                //更新
                                int ID = (int)obj;
                                SqlStr = string.Format(@"UPDATE  dbo.Product_ExpirationTime
                                                        SET     ExpirationTime = '{0}' ,
                                                                Extended1 = '{1}' ,
                                                                Goods_Unit = '{2}' ,
                                                                Price = {3} ,
                                                                Stock = {4}", ExpirationTime, Extended1, Goods_Unit, Price, Stock);
                            }
                            else
                            {
                                //新增
                                SqlStr = string.Format(@"INSERT  INTO Product_ExpirationTime
                                                                    ( Product_ID,--默认赋值0
                                                                      Extended1 ,
                                                                      Stock ,
                                                                      Price ,
                                                                      ExpirationTime ,
                                                                      Erp_ID ,
                                                                      Goods_Unit
                                                                    )
                                                            VALUES  ( 0,
                                                                      '{0}' ,
                                                                      {1} ,
                                                                      {2} ,
                                                                      '{3}' ,
                                                                      '{4}',
                                                                      '{5}'
                                                                    )
                                                            ", Extended1, Stock, Price, ExpirationTime, Erp_ID, Goods_Unit);
                            }

                            SOSOshop.BLL.Product.ExpirationTime.CreateInstance().ExecuteNonQuery(SqlStr);
                        }
                    }
                    ret = true;
                }

            }

            return ret;
        }


        /// <summary>
        /// 新增企业
        /// </summary>
        /// <param name="dr"></param>
        private void addEenerprise(DataRow dr)
        {
            if (dr != null)
            {
                SOSOshop.Model.DrugsBase_Enterprise de = new SOSOshop.Model.DrugsBase_Enterprise();
                SOSOshop.BLL.DrugsBase_Enterprise debll = new SOSOshop.BLL.DrugsBase_Enterprise();
                de = debll.GetModel(dr["公司编号"].ToString());
                if (de != null)
                {
                    modifyEenerprise(dr);
                    return;
                }
                else if (de == null)
                {
                    de = new SOSOshop.Model.DrugsBase_Enterprise();
                }
                de.Address = dr["地址"].ToString();
                de.BuyFilingStatus = 1;                
                de.Code = dr["公司编号"].ToString();
                de.Name = dr["公司名称"].ToString();
                de.OfficePhone = dr["座机"].ToString();
                de.Status = 0;

                de.PYJM = SOSOshop.BLL.Common.GetPY.Get(de.Name.Trim());
                de.TrueName = dr["联系人名"].ToString();
                debll.Add(de);
            }
        }

        /// <summary>
        /// 修改企业信息
        /// </summary>
        /// <param name="dr"></param>
        private void modifyEenerprise(DataRow dr)
        {
            if (dr != null)
            {
                try
                {
                    SOSOshop.Model.DrugsBase_Enterprise de = new SOSOshop.Model.DrugsBase_Enterprise();
                    SOSOshop.BLL.DrugsBase_Enterprise debll = new SOSOshop.BLL.DrugsBase_Enterprise();
                    de = debll.GetModel(dr["公司编号"].ToString());
                    if (de == null)
                    {
                        addEenerprise(dr);
                        return;
                    }
                    de.Address = dr["地址"].ToString();
                    //de.BuyFilingStatus = (bool)dr["是否建档"] ? 1 : 2;
                    de.Name = dr["公司名称"].ToString();
                    de.OfficePhone = dr["座机"].ToString();
                    de.PYJM = SOSOshop.BLL.Common.GetPY.Get(de.Name.Trim());
                    de.TrueName = dr["联系人名"].ToString();
                    debll.Update(de);
                }
                catch (Exception ex)
                {
                    SOSOshop.BLL.Logs.Log.LogServiceAdd(ex.Message, 0, "", "modifyEenerprise", ex.ToString(), 2);
                }
                
            }
        }

        /// <summary>
        /// 添加会员账号
        /// </summary>
        /// <param name="dr"></param>
        private void addMemberAccount(DataRow dr)
        {
            if (dr != null)
            {
                SOSOshop.BLL.MemberAccount ma = new SOSOshop.BLL.MemberAccount();
                SOSOshop.Model.MemberAccount mam = new SOSOshop.Model.MemberAccount();

                object UID = ma.ExecuteScalar("select UID from MemberAccount where MobilePhone='" + dr["联系手机"].ToString() + "'");
                if (UID == null)
                {
                    mam.MobilePhone = dr["联系手机"].ToString();
                    mam.CompanyClass = dr["客户类型"].ToString();
                    mam.State = 1;

                    mam.RegisterDate = DateTime.Now;
                    mam.PassWord = ChangeHope.Common.DEncryptHelper.Encrypt("123456", 1);
                    mam.PeriodOfValidity = DateTime.Now.AddYears(3);
                    mam.UserId = dr["会员编号"].ToString();
                    ma.Add(mam);
                }
                else
                {
                    modifyMemberAccount(dr);
                }
            }
        }

        /// <summary>
        /// 修改会员账号
        /// </summary>
        /// <param name="dr"></param>
        private void modifyMemberAccount(DataRow dr)
        {
            if (dr != null)
            {
                SOSOshop.BLL.MemberAccount ma = new SOSOshop.BLL.MemberAccount();
                SOSOshop.Model.MemberAccount mam = new SOSOshop.Model.MemberAccount();
                object UID = ma.ExecuteScalar("select UID from MemberAccount where MobilePhone='" + dr["联系手机"].ToString() + "'");

                if (UID != null)
                {
                    int x = 0;
                    if (int.TryParse(UID.ToString(), out x))
                    {
                        mam = ma.GetModel(x);
                        mam.MobilePhone = dr["联系手机"].ToString();
                        mam.CompanyClass = dr["客户类型"].ToString();
                        mam.UserId = dr["会员编号"].ToString();
                        ma.Update(mam);
                    }
                    else
                    {
                        SOSOshop.BLL.Logs.Log.LogAdminAdd("没有该会员信息" + dr["联系手机"].ToString().Trim(), 0, "", 0);
                    }

                }
                else
                {
                    addMemberAccount(dr);
                }
            }
        }

        /// <summary>
        /// 添加会员信息
        /// </summary>
        /// <param name="dr"></param>
        private void addMemberinfo(DataRow dr)
        {
            if (dr != null)
            {
                SOSOshop.Model.MemberInfo mi = new SOSOshop.Model.MemberInfo();
                SOSOshop.BLL.MemberInfo mibll = new SOSOshop.BLL.MemberInfo();
                SOSOshop.BLL.MemberPermission mpbll = new SOSOshop.BLL.MemberPermission();
                SOSOshop.Model.MemberPermission mp = new SOSOshop.Model.MemberPermission();
                object UID = mibll.ExecuteScalar("select UID from MemberAccount where MobilePhone='" + dr["联系手机"].ToString() + "'");
                if (!mibll.Exists((int)UID))
                {
                    mi.Address = dr["地址"].ToString();
                    mi.Code = dr["会员编号"].ToString();
                    mi.discount = 1;
                    mi.Member_Type = 1;
                    object r = mibll.ExecuteScalar("select ID from DrugsBase_Enterprise where name='" + dr["公司名称"].ToString() + "'");
                    mi.ParentId = (int)r;
                    mi.TrueName = dr["联系人名"].ToString();
                    mi.UID = (int)UID;
                    mi.PriceCategory = dr["价格类型"] as string;
                    mibll.Add(mi);
                    mp.UID = mi.UID;
                    mp.IsMoneyAndShipping = true;
                    mi.Province = GetRegionId(dr["省"].ToString());
                    mi.City = GetRegionId(dr["市"].ToString());
                    mi.Borough = GetRegionId(dr["区"].ToString());
                    mpbll.Add(mp);
                }
                else
                {
                    modifyMemberinfo(dr);
                }
            }
        }

        /// <summary>
        /// 修改会员信息
        /// </summary>
        /// <param name="dr"></param>
        private void modifyMemberinfo(DataRow dr)
        {
            if (dr != null)
            {
                SOSOshop.Model.MemberInfo mi = new SOSOshop.Model.MemberInfo();
                SOSOshop.BLL.MemberInfo mibll = new SOSOshop.BLL.MemberInfo();
                SOSOshop.BLL.MemberPermission mpbll = new SOSOshop.BLL.MemberPermission();
                SOSOshop.Model.MemberPermission mp = new SOSOshop.Model.MemberPermission();
                object UID = mibll.ExecuteScalar("select UID from MemberAccount where MobilePhone='" + dr["联系手机"].ToString() + "'");
                int x = 0;
                if (int.TryParse(UID.ToString(), out x))
                {
                    if (mibll.Exists(x))
                    {
                        mi = mibll.GetModel(x);
                        mi.Address = dr["地址"].ToString();
                        mi.Code = dr["会员编号"].ToString();
                        object r = mibll.ExecuteScalar("select ID from DrugsBase_Enterprise where name='" + dr["公司名称"].ToString().Trim() + "'");
                        mi.ParentId = (int)r;
                        mi.TrueName = dr["联系人名"].ToString();
                        mi.PriceCategory = dr["价格类型"] as string;
                        mi.UID = x;
                        mi.Province = GetRegionId(dr["省"].ToString());
                        mi.City = GetRegionId(dr["市"].ToString());
                        mi.Borough = GetRegionId(dr["区"].ToString());
                        mibll.Update(mi);
                        SOSOshop.BLL.Logs.Log.LogAdminAdd("修改" + dr["联系手机"].ToString().Trim(), 0, UID.ToString(), 1);
                    }
                }
                else
                {
                    //SOSOshop.BLL.Logs.Log.LogAdminAdd("添加" + dr["联系手机"].ToString().Trim(), 0, rii.ToString(), 1);
                    addMemberinfo(dr);
                }
            }
        }

        /// <summary>
        /// 获取地区Id
        /// </summary>
        /// <param name="regionName"></param>
        /// <returns></returns>
        private int GetRegionId(string regionName)
        {
            int rtn = 0;
            if (!string.IsNullOrEmpty(regionName))
            {
                SOSOshop.BLL.DbBase db = new SOSOshop.BLL.DbBase();
                var dt = db.ExecuteScalar(string.Format("SELECT TOP 1 id FROM Region WHERE Name='{0}'", regionName));
                if (dt != null)
                {
                    int.TryParse(dt.ToString(), out rtn);
                }
            }

            return rtn;
        }



        /// <summary>
        /// 添加企业、会员信息
        /// </summary>
        /// <param name="dr"></param>
        private void adduser(DataRow dr)
        {
            if (!string.IsNullOrEmpty(dr["联系手机"].ToString()))
            {
                #region 处理企业信息部分
                addEenerprise(dr);
                #endregion

                #region 处理会员账号部分
                addMemberAccount(dr);
                #endregion

                #region 处理会员信息部分
                addMemberinfo(dr);
                #endregion
            }
        }

        /// <summary>
        /// 修改客户信息
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="authKey"></param>
        /// <returns></returns>
        [WebMethod]
        public bool ModifyCustomer(DataSet ds, string authKey)
        {
            bool ret = false;
            if (authKey == key)
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr != null)
                        {
                            modifyuser(dr);
                        }
                    }
                    ret = true;
                }
            }
            return ret;
        }

        /// <summary>
        /// 修改企业、会员信息
        /// </summary>
        /// <param name="dr"></param>
        private void modifyuser(DataRow dr)
        {
            if (!string.IsNullOrEmpty(dr["联系手机"].ToString()))
            {
                #region 处理企业信息部分
                modifyEenerprise(dr);
                #endregion

                #region 处理会员账号部分

                modifyMemberAccount(dr);
                #endregion

                #region 处理会员信息部分
                modifyMemberinfo(dr);
                #endregion
            }
        }

        /// <summary>
        /// 删除客户信息
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="authKey"></param>
        /// <returns></returns>
        [WebMethod]
        public bool DeleteCustomer(DataSet ds, string authKey)
        {
            bool ret = false;
            if (authKey == key)
            {

                if (ds != null && ds.Tables.Count > 0)
                {
                    SOSOshop.BLL.MemberAccount ma = new SOSOshop.BLL.MemberAccount();
                    SOSOshop.BLL.DrugsBase_Enterprise debll = new SOSOshop.BLL.DrugsBase_Enterprise();
                    SOSOshop.BLL.MemberInfo mibll = new SOSOshop.BLL.MemberInfo();
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        object uid = mibll.ExecuteScalar("select UID from MemberAccount where MobilePhone='" + dr["联系手机"].ToString() + "'");
                        if (uid != null)
                        {
                            mibll.Delete((int)uid);
                        }
                    }
                    ret = true;
                }
            }
            return ret;
        }

        

        /// <summary>
        /// 取得未同步至erp的订单
        /// </summary>
        /// <param name="authKey"></param>
        /// <returns></returns>
        [WebMethod]
        public List<SOSOshop.BLL.Order.OrderList> GetOrderList(string authKey)
        {
            if (authKey == key)
            {
                SOSOshop.BLL.Order.Orders bll = new SOSOshop.BLL.Order.Orders();
                return bll.GetOrdersMQ_1();
            }
            return null;
        }
        /// <summary>
        /// 取得所有未建档的会员的手机号
        /// </summary>
        /// <param name="authKey"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetNotFiling(string authKey)
        {
            if (authKey == key)
            {
                SOSOshop.BLL.Order.Orders bll = new SOSOshop.BLL.Order.Orders();
                return string.Join(",", bll.ExecuteTable("SELECT MobilePhone FROM dbo.memberinfo a INNER JOIN dbo.memberaccount b ON b.UID = a.UID WHERE a.Code=''").AsEnumerable().Select(x => "'" + x.Field<string>("MobilePhone") + "'"));
            }
            return "";
        }
        /// <summary>
        /// 更新订单消息状态
        /// </summary>
        /// <param name="orderid"></param>
        [WebMethod]
        public void UpdateOrdersMQ_1(string orderid, string authKey)
        {
            if (authKey == key)
            {
                SOSOshop.BLL.Order.Orders bll = new SOSOshop.BLL.Order.Orders();
                bll.UpdateOrdersMQ_1(orderid);
            }
        }
               
        /// <summary>
        /// 同步erp订单状态至商城
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="OrderStatus"></param>
        [WebMethod]
        public void UpdateOrderStatusForErp(string orderid, string spid, int OrderStatus, string authKey)
        {
            if (authKey == key)
            {
                SOSOshop.BLL.Order.Orders bll = new SOSOshop.BLL.Order.Orders();
                bll.UpdateOrderStatusForErp(orderid, spid, OrderStatus);
            }
        }

        [WebMethod]
        /// <summary>
        /// 同步买家经营范围
        /// </summary>
        public void SyncMemberBusinessScope(DataSet ds, string authKey)
        {
            if (authKey == key && ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                SOSOshop.BLL.MemberBusinessScope.CreateInstance().SyncMemberBusinessScope(ds.Tables[0]);

            }
        }
    }
}
