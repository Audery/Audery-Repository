using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Text;
using System.Data;
using SOSOshop.BLL.Common;

namespace _101shop.v3.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 取得用户登陆的Id
        /// </summary>
        /// <returns></returns>
        public static int GetUserId()
        {
            return Public.GetUserId();
        }

        /// <summary>
        /// 获取用户信息ViewBag.UID,ViewBag.Member_IsLogOn是否登陆?ViewBag.UserType,ViewBag.Member_Type,ViewBag.Member_Class,ViewBag.MemberPermission权限等
        /// </summary>
        public static void SetAccount(dynamic ViewBag)
        {
            int UID = GetUserId();//账户ID
            ViewBag.UID = UID;
            ViewBag.Member_IsLogOn = false;
            if (UID > 0)
            {
                Models.UserModel model = GetUserModel(UID);
                if (model != null)
                {
                    ViewBag.Member_IsLogOn = true;
                    //类别
                    ViewBag.UserType = model.UserType;
                    ViewBag.Member_Type = model.Member_Type;
                    ViewBag.Member_Class = model.Member_Class;
                    ViewBag.UserId = model.UserId;
                    ViewBag.MobilePhone = model.MobilePhone;
                    ViewBag.Email = model.Email;
                    ViewBag.TrueName = model.LinkMan;
                    ViewBag.IncName = model.IncName;
                    ViewBag.ParentId = model.ParentId;
                    ViewBag.Parents = model.IncName;
                    ViewBag.Province = model.Province;
                    ViewBag.City = model.City;
                    ViewBag.Borough = model.Borough;
                    ViewBag.Address = model.Address;
                    ViewBag.OfficePhone = model.OfficePhone;
                    ViewBag.Fax = model.Fax;
                    //验证
                    ViewBag.membercheckM = model.CheckM;
                    ViewBag.membercheckE = model.CheckE;
                    //权限
                    ViewBag.MemberPermission = model.MemberPermission;
                }
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public static Models.UserModel GetUserModel(int UID)
        {
            Models.UserModel model = null;
            if (UID > 0)
            {
                //缓存
                DateTime expiry = DateTime.Now.AddMinutes(1);
                string key = "BaseController.GetUserModel." + UID;
                Memcached.ClientLibrary.MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
                if (mc.KeyExists(key))
                {
                    model = mc.Get(key) as Models.UserModel;
                    if (model != null) return model;
                }

                SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();
                StringBuilder sql = new StringBuilder();
                sql.Append("select UserId, MobilePhone, Email, UserType, UserGroup, ");//账号
                sql.Append("TrueName, ");//联系人
                sql.Append("isnull((select top(1) Name from DrugsBase_Enterprise where ID=b.ParentId),'') as IncName, b.ParentId, b.Parents, ");//单位
                sql.Append("Member_Type, Member_Class, ");//用户类别
                sql.Append("b.Province,b.City,b.Borough,b.Address, ");//所在地
                sql.Append("b.OfficePhone, b.Fax, ");//电话、传真
                sql.Append("ISNULL((SELECT TOP(1) 1 AS M FROM membercheck WHERE CheckType='M' AND UID=a.UID),0) AS membercheckM, ");//手机验证
                sql.Append("ISNULL((SELECT TOP(1) 1 AS M FROM membercheck WHERE CheckType='E' AND UID=a.UID),0) AS membercheckE, ");//邮箱验证
                sql.Append("c.* ");//权限
                sql.AppendFormat("from memberaccount a inner join memberinfo b on a.UID=b.UID inner join memberpermission c on a.UID=c.UID where a.UID={0}", UID);
                using (IDataReader rd = (IDataReader)db.ExecuteReader(sql.ToString()))
                {
                    if (rd != null && rd.Read())
                    {
                        //类别
                        model = new Models.UserModel();
                        model.UserType = int.Parse(rd["UserType"].ToString()) < 0 ? SOSOshop.Model.MemberKeyValue.UserType.无 : (SOSOshop.Model.MemberKeyValue.UserType)Enum.Parse(typeof(SOSOshop.Model.MemberKeyValue.UserType), rd["UserType"].ToString());
                        model.Member_Class = int.Parse(rd["Member_Class"].ToString()) < 0 ? SOSOshop.Model.MemberKeyValue.Member_Class.无 : (SOSOshop.Model.MemberKeyValue.Member_Class)Enum.Parse(typeof(SOSOshop.Model.MemberKeyValue.Member_Class), rd["Member_Class"].ToString());
                        model.Member_Type = (SOSOshop.Model.MemberKeyValue.Member_Type)Enum.Parse(typeof(SOSOshop.Model.MemberKeyValue.Member_Type), rd["Member_Type"].ToString());
                        model.UserId = Convert.ToString(rd["UserId"]);
                        model.MobilePhone = Convert.ToString(rd["MobilePhone"]);
                        model.Email = Convert.ToString(rd["Email"]);
                        model.LinkMan = Convert.ToString(rd["TrueName"]);
                        model.IncName = Convert.ToString(rd["IncName"]);
                        model.ParentId = int.Parse(rd["ParentId"].ToString());
                        model.Province = int.Parse(rd["Province"].ToString());
                        model.City = int.Parse(rd["City"].ToString());
                        model.Borough = int.Parse(rd["Borough"].ToString());
                        model.Address = Convert.ToString(rd["Address"]);
                        model.OfficePhone = Convert.ToString(rd["OfficePhone"]);
                        model.Fax = Convert.ToString(rd["Fax"]);
                        //验证
                        model.CheckM = int.Parse(rd["membercheckM"].ToString()) == 1;
                        model.CheckE = int.Parse(rd["membercheckE"].ToString()) == 1;
                        //权限
                        SOSOshop.BLL.MemberPermission mpBll = new SOSOshop.BLL.MemberPermission();
                        model.MemberPermission = mpBll.GetModelByDataReader(rd);
                        rd.Close();
                        //缓存
                        mc.Set(key, model, expiry);
                    }
                }
            }
            return model;
        }

        public static UserAddressModel GetUserAddress(int id)
        {
            try
            {
                string sql = "select [id],[uid],[username] ,[mobile],[phone] ,"
                    + "(select CityName from [yxs_provinces] where id=province) as province,"
                    + "(select CityName from [yxs_provinces] where id=city) as city,"
                    + "(select CityName from [yxs_provinces] where id=Borough) as borough,"
                    + "[address],[zip],[email],[constructionsigns],[consignestime],[stat] from memberreceaddress where id=" + id;
                SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();
                DataTable dt = db.ExecuteTable(sql);
                UserAddressModel ua = new UserAddressModel();
                if (dt != null)
                {
                    ua.ID = (int)dt.Rows[0]["id"];
                    ua.Uid = (int)dt.Rows[0]["uid"];
                    ua.Username = dt.Rows[0]["username"].ToString();
                    ua.Mobile = dt.Rows[0]["mobile"].ToString();
                    ua.Phone = dt.Rows[0]["phone"].ToString();
                    ua.Province = dt.Rows[0]["province"].ToString();
                    ua.City = dt.Rows[0]["city"].ToString();
                    ua.Borough = dt.Rows[0]["borough"].ToString();
                    ua.Address = dt.Rows[0]["address"].ToString();
                    ua.Zip = dt.Rows[0]["zip"].ToString();
                    ua.Email = dt.Rows[0]["email"].ToString();
                    ua.Constructionsigns = dt.Rows[0]["constructionsigns"].ToString();
                    ua.Consignestime = dt.Rows[0]["consignestime"].ToString();
                    ua.stat = (Boolean)dt.Rows[0]["stat"];
                }
                return ua;
            }
            catch
            {

            }
            return null;
        }

        /// <summary>
        /// 获得授权单位用户列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetUserWorkList()
        {
            try
            {
                SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();
                DataTable dt = db.ExecuteTable("select case when Parents is null or parents='' then CONVERT(varchar,[ParentId]) else CONVERT(varchar,[ParentId])+','+parents end  from memberinfo  where uid=" + GetUserId());
                if (dt != null)
                {
                    string ids = dt.Rows[0][0].ToString();
                    string sql = "select id,name from DrugsBase_Enterprise where id in (" + ids + ") ";
                    return db.ExecuteTable(sql);
                }
            }
            catch
            {
            }
            return null;
        }

        /// <summary>
        /// 获得用户单位信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataRow GetUserWorker(int id)
        {
            SOSOshop.BLL.Db db = new SOSOshop.BLL.Db();
            string sql = "select id,name from DrugsBase_Enterprise where id = " + id;
            return db.ExecuteTable(sql).Rows[0];
        }
        /// <summary>
        /// 获取用户单位实例
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public UserWorderModel GetUserModel(DataRow dr)
        {
            UserWorderModel u = new UserWorderModel();
            u.ID = (int)dr["id"];
            u.Name = dr["name"].ToString();
            return u;
        }

        public MyOdersProductModel GetMyOdersProductModel(DataRow dr)
        {
            MyOdersProductModel m = new MyOdersProductModel();
            m.Id = (int)dr["id"];
            m.OrderId = dr[""].ToString();
            m.ProId = (int)dr[""];
            m.OrderId = dr[""].ToString();
            m.OrderId = dr[""].ToString();
            m.OrderId = dr[""].ToString();
            m.OrderId = dr[""].ToString();
            m.OrderId = dr[""].ToString();
            return m;
        }
        /// <summary>
        /// 设置当前页的栏目样式
        /// </summary>
        /// <param name="vc"></param>
        /// <param name="Controller"></param>
        /// <returns></returns>
        public static string GetSelectNav(ViewContext vc, string Controller)
        {
            if (vc.RouteData.Values["Controller"].ToString() == Controller)
            {
                return "class=nav_hov_01";
            }
            return "";
        }

        /// <summary>
        /// 返回ajax调用的信息提示
        /// </summary>
        /// <param name="no">消息类型</param>
        /// <param name="msg">消息内容</param>
        /// <returns></returns>
        public static string Json(int no, string msg)
        {
            return "{\"state\":" + no + ",\"message\":\"" + msg + "\"}";
        }

        /// <summary>
        /// 返回ajax调用的信息提示
        /// </summary>
        /// <param name="no">消息类型</param>
        /// <param name="msg">消息内容</param>
        /// <param name="url">跳转地址</param>
        /// <returns></returns>
        public static string Json(int no, string msg, string url)
        {
            return "{\"state\":" + no + ",\"message\":\"" + msg + "\",\"url\":\"" + url + "\"}";
        }



        //获取用户的权限体系
        public SOSOshop.Model.MemberPermission GetUserRight()
        {
            int uid = GetUserId();
            SOSOshop.Model.MemberPermission d_User = new SOSOshop.Model.MemberPermission();

            if (uid != 0)
            {
                SOSOshop.BLL.MemberPermission f_User = new SOSOshop.BLL.MemberPermission();
                d_User = f_User.GetModel(uid);
            }

            return d_User;
        }

        /// <summary>
        /// 获取交易人员的电话和QQ
        /// </summary>
        /// <returns></returns>
        public DataTable GetAdminPhoneAndQQ()
        {
            int uid = GetUserId();
            DataTable resultTable = new DataTable();

            if (uid != 0)
            {
                SOSOshop.BLL.DbBase db = new SOSOshop.BLL.DbBase();

                string sql = "SELECT top 1 dbo.yxs_administrators.OfficePhone,dbo.yxs_administrators.QQ, yxs_administrators.name " +
                             "FROM dbo.memberinfo INNER JOIN dbo.yxs_administrators ON Editer = adminid " +
                             "WHERE UID=" + uid;

                resultTable = db.ExecuteTableForCache(sql);
                if (resultTable.Rows.Count == 0)
                {
                    sql = "SELECT top 1 dbo.yxs_administrators.OfficePhone,dbo.yxs_administrators.QQ, yxs_administrators.name " +
                                 "FROM dbo.yxs_administrators " +
                                 "WHERE adminID=1";

                    resultTable = db.ExecuteTableForCache(sql);
                }
            }
            else
            {
                SOSOshop.BLL.DbBase db = new SOSOshop.BLL.DbBase();

                string sql = "SELECT top 1 dbo.yxs_administrators.OfficePhone,dbo.yxs_administrators.QQ, yxs_administrators.name " +
                             "FROM dbo.yxs_administrators " +
                             "WHERE adminID=1";

                resultTable = db.ExecuteTableForCache(sql);
            }

            return resultTable;
        }
    }

    public class UserWorderModel
    {
        public UserWorderModel()
        {

        }

        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class UserAddressModel
    {
        public UserAddressModel()
        {
        }

        public int ID { get; set; }
        public int Uid { get; set; }
        public string Username { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Borough { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }
        public string Constructionsigns { get; set; }
        public string Consignestime { get; set; }
        public Boolean stat { get; set; }
    }

    public class MyOdersProductModel
    {
        public MyOdersProductModel()
        {
        }

        public int Id { get; set; }
        public string OrderId { get; set; }
        public int ProId { get; set; }
        public string ProName { get; set; }
        public decimal ProPrice { get; set; }
        public int ProNum { get; set; }
        public DateTime AddTime { get; set; }
        public string pro_pno { get; set; }
        public string pro_pdate { get; set; }
        public int Status { get; set; }
        public Boolean issplit { get; set; }
    }
}
