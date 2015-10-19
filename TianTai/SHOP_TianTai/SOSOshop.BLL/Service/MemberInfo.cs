using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Specialized;

namespace SOSOshop.BLL.Service
{
    /// <summary>
    /// 商品数据
    /// </summary>
    public class MemberInfo : SOSOshop.BLL.Db
    {
        /// <summary>
        /// 同步买家数据[马上同步已经存在的买家到ERP]，并通知交易员
        /// </summary>
        /// <param name="Code">买家编号[一级单位]</param>
        public static int Syn_AtOnce_Exists(Model.MemberInfo oldmodel, Model.MemberInfo model)
        {
            int result = 0;
            string Code = model.Code;
            Db db = new Db();
            DbBase db2 = new DbBase(); db2.ChangeDB("ConnectionStringERP");

            string sql = @"SELECT TrueName, MobilePhone, Email, OfficePhone, HandPhone, Fax, Address, 
                            ISNULL((SELECT TOP(1) Name FROM Region WHERE Depth=1 AND ID=b.Province),'') AS Province, 
                            ISNULL((SELECT TOP(1) Name FROM Region WHERE Depth=2 AND ID=b.City),'') AS City, 
                            ISNULL((SELECT TOP(1) Name FROM Region WHERE Depth=3 AND ID=b.Borough),'') AS Borough, 
                            QQ, ISNULL((SELECT TOP(1) name FROM yxs_administrators WHERE adminid=b.Editer),'') AS admin 
                            FROM memberaccount a INNER JOIN memberinfo b ON a.UID=b.UID WHERE b.Code='{0}'";
            string sql2 = @"IF EXISTS(SELECT TOP(1) wldwid FROM wldwzl WHERE wldwid=@Code) BEGIN 
                            UPDATE wldwzl SET wldwname=@TrueName, lxshj=@MobilePhone, Email=@Email, lxdh=@OfficePhone, 
                            lxrdh=@HandPhone, lxchzh=@Fax, dzhdh=@Address, shengfen=@Province, chengshi=@City, quyufl=@Borough, 
                            QQ=@QQ WHERE wldwid=@Code 
                            UPDATE wldwzl SET kpy=@admin WHERE wldwid=@Code 
                            AND EXISTS(SELECT TOP(1) * FROM zhiyzl WHERE is_czy='是' and beactive='是' and zhiyname=@admin and @admin<>'') 
                            SELECT @Code END
                            SELECT TOP(1) zyphone FROM zhiyzl WHERE is_czy='是' and beactive='是' and zhiyname=@admin and @admin<>''";

            DataSet ds = db.ExecuteDataSet(string.Format(sql, Code));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                DbCommand dbCommand = db2._db.GetSqlStringCommand(sql2);
                db2._db.AddInParameter(dbCommand, "Code", DbType.String, Code);
                db2._db.AddInParameter(dbCommand, "TrueName", DbType.String, Convert.ToString(dr["TrueName"]).Trim());
                db2._db.AddInParameter(dbCommand, "MobilePhone", DbType.String, Convert.ToString(dr["MobilePhone"]).Trim());
                db2._db.AddInParameter(dbCommand, "Email", DbType.String, Convert.ToString(dr["Email"]).Trim());
                db2._db.AddInParameter(dbCommand, "OfficePhone", DbType.String, Convert.ToString(dr["OfficePhone"]).Trim());
                db2._db.AddInParameter(dbCommand, "HandPhone", DbType.String, Convert.ToString(dr["HandPhone"]).Trim());
                db2._db.AddInParameter(dbCommand, "Fax", DbType.String, Convert.ToString(dr["Fax"]).Trim());
                db2._db.AddInParameter(dbCommand, "Address", DbType.String, Convert.ToString(dr["Address"]).Trim());
                db2._db.AddInParameter(dbCommand, "Province", DbType.String, Convert.ToString(dr["Province"]).Trim());
                db2._db.AddInParameter(dbCommand, "City", DbType.String, Convert.ToString(dr["City"]).Trim());
                db2._db.AddInParameter(dbCommand, "Borough", DbType.String, Convert.ToString(dr["Borough"]).Trim());
                db2._db.AddInParameter(dbCommand, "QQ", DbType.String, Convert.ToString(dr["QQ"]).Trim());
                db2._db.AddInParameter(dbCommand, "admin", DbType.String, Convert.ToString(dr["admin"]).Trim());

                try
                {
                    DataSet ds2 = db2._db.ExecuteDataSet(dbCommand);
                    if (ds2 != null && ds2.Tables.Count > 0)
                    {
                        result = (ds2.Tables.Count > 1 ? 1 : -1);
                        //短信通知交易员
                        if (oldmodel.Editer != model.Editer && model.Editer > 0)
                        {
                            string MobilePhone = Convert.ToString(ds2.Tables[ds2.Tables.Count - 1].Rows[0][0]).Trim();
                            string SmsMsg = Convert.ToString(dr["admin"]).Trim() + "，现有买家[" + model.TrueName + "]已经分配给你，请注意跟进此客户!";
                            string from = "系统";
                            string to = MobilePhone;
                            Sms.SendAndSaveDataBase(MobilePhone, SmsMsg, from, to);
                        }
                    }
                }
                catch//ERP异常，则恢复商城更新操作
                {
                    BLL.MemberInfo bll = new BLL.MemberInfo();
                    bll.Update(oldmodel, false);
                    result = 0;
                }
            }
            return result;
        }

        /// <summary>
        /// 从ERP获取客户-往来单位-类别
        /// </summary>
        /// <param name="Code">往来单位编码[二级单位]</param>
        /// <param name="Member_Class">0,批发；1,OTC</param>
        /// <param name="Crm_Class">Crm分类</param>
        /// <returns></returns>
        public static string GetErp_KeHuLB(string Code, ref int Member_Class, ref string Crm_Class)
        {
            Member_Class = -1; Crm_Class = "";
            DbBase db = new DbBase(); db.ChangeDB("ConnectionStringERP");
            char sep = '§';
            string lb = Convert.ToString(db.ExecuteScalar("SELECT LTRIM(RTRIM(ISNULL(kehulb,'')))+'" + sep + "'+LTRIM(RTRIM(ISNULL(kehufl,''))) FROM wldwzl WHERE wldwid='" + Code + "'")).Trim();
            return SetErp_KeHuLB(lb, ref Member_Class, ref Crm_Class);
        }
        private static string SetErp_KeHuLB(string lb, ref int Member_Class, ref string Crm_Class)
        {
            char sep = '§';
            NameValueCollection pf = new NameValueCollection();
            pf.Add("零售连锁", "0000100003000030000300003");//CRM 药店/连锁公司/意向客户
            pf.Add("商业公司", "00001000030000200005");//CRM 批发企业/意向客户
            pf.Add("商业公司A", "00001000030000200005");//CRM 批发企业/意向客户
            pf.Add("商业公司B", "00001000030000200005");//CRM 批发企业/意向客户
            pf.Add("生产企业", "00001000030000100003");//CRM 生产企业/意向客户
            pf.Add("民营医院", "0000100003000040000200003");//CRM 医院/意向客户
            pf.Add("公立医院", "0000100003000040000200003");//CRM 医院/意向客户            
            NameValueCollection otc = new NameValueCollection();
            otc.Add("零售药店", "0000100003000030000200003");//CRM 药店/单体药店/意向客户                        
            otc.Add("单体/加盟药店", "0000100003000030000200003");//CRM 药店/单体药店/意向客户                        
            otc.Add("诊所", "0000100003000040000100003");//CRM 诊所/意向客户
            string[] ls = lb.Split(sep);
            foreach (string l in ls)
            {
                if (string.IsNullOrEmpty(l)) continue;
                if (pf[l] != null)
                {
                    Member_Class = 0; Crm_Class = pf[l];
                    break;
                }
                if (otc[l] != null)
                {
                    Member_Class = 1; Crm_Class = otc[l];
                    break;
                }
            }
            return lb.Trim(sep);
        }

        /// <summary>
        /// 同步买家建档后的分类Member_Class
        /// </summary>
        /// <param name="Code">买家编号[一级单位]</param>
        /// <param name="Member_Class">0,批发；1,OTC</param>
        /// <returns></returns>
        public static bool Syn_JianDang(string Code, int Member_Class = -1)
        {
            string sql2 = @"update memberinfo set Member_Class=@Member_Class where Code=@Code";
            Db db2 = new Db();
            DbCommand dbCommand = db2._db.GetSqlStringCommand(sql2);
            db2._db.AddInParameter(dbCommand, "Code", DbType.String, Code);
            db2._db.AddInParameter(dbCommand, "Member_Class", DbType.Int32, Member_Class);
            return 0 < db2._db.ExecuteNonQuery(dbCommand);
        }
        /// <summary>
        /// 同步买家所在单位建档后的分类Member_Class
        /// </summary>
        /// <param name="Code">往来单位编码[二级单位]</param>
        /// <param name="Member_Class">0,批发；1,OTC</param>
        /// <returns></returns>
        public static bool Syn_Enterprise_JianDang(string Code, int Member_Class = -1)
        {
            string Crm_Class = "";
            if (Member_Class == -1) GetErp_KeHuLB(Code, ref Member_Class, ref Crm_Class);
            string sql2 = @"update memberinfo set Member_Class=@Member_Class where ParentId in (SELECT ID FROM DrugsBase_Enterprise WHERE Code=@Code)";
            if (Code.StartsWith("del", StringComparison.CurrentCultureIgnoreCase))
            {
                Code = Code.Substring(3);
                sql2 = @"update memberinfo set Member_Class=@Member_Class where ParentId in (SELECT ID FROM DrugsBase_Enterprise WHERE Code LIKE '%'+@Code) 
                        update memberaccount set State=2 
                        where uid in (select uid from memberinfo where ParentId in (SELECT ID FROM DrugsBase_Enterprise WHERE Code LIKE '%'+@Code))";
            }

            Db db2 = new Db();
            DbCommand dbCommand = db2._db.GetSqlStringCommand(sql2);
            db2._db.AddInParameter(dbCommand, "Code", DbType.String, Code);
            db2._db.AddInParameter(dbCommand, "Member_Class", DbType.Int32, Member_Class);
            return 0 < db2._db.ExecuteNonQuery(dbCommand);
        }
        /// <summary>
        /// 同步买家数据
        /// </summary>
        /// <param name="Code">买家编号[一级单位]</param>
        public static void Syn(string Code)
        {
            DbBase db = new DbBase(); db.ChangeDB("ConnectionStringERP");
            char sep = '§';
            string sql = string.Format(@"SELECT TOP 1 wldwname AS TrueName, lxshj AS MobilePhone, Email, lxdh AS OfficePhone, 
                lxrdh AS HandPhone, lxchzh AS Fax, dzhdh AS Address, shengfen AS Province, chengshi AS City, quyufl AS Borough, 
                QQ, kpy, kehsx1, (LTRIM(RTRIM(ISNULL(kehulb,'')))+'{1}'+LTRIM(RTRIM(ISNULL(kehufl,'')))) AS Member_Class, caozy AS admin, beactive FROM wldwzl WHERE wldwid='{0}' {2}
                SELECT dywldwid,dywldwname,(SELECT TOP 1 LTRIM(RTRIM(ISNULL(kehulb,''))) FROM wldwzl WHERE wldwid=wldwzl_dygx.dywldwid) AS Member_Class 
                FROM wldwzl_dygx WHERE wldwid='{0}'", Code, sep, " AND jigid='000' ");//买家只同步000机构买家[2014-10-27日改按机构分类分单]

            string sql2 = @"declare @UID int, @UID_BuyFilingStatus int, @ParentId0 int, @ParentId1 int, @inserted int, @updated int set @inserted=0 set @updated=0 
                IF (@IsDel=1)
                begin
                    set @UID=ISNULL((select top(1) a.UID from memberaccount a inner join memberinfo b on a.UID=b.UID 
                    WHERE Code LIKE '%'+@DelCode), 0)
                    update memberaccount set State=2 
                    where UID=@UID 
                    set @updated=@UID 
                end
                else
                begin
                set @UID=ISNULL((select top(1) a.UID from memberaccount a inner join memberinfo b on a.UID=b.UID 
                where (@MobilePhone<>'' and MobilePhone=@MobilePhone) or (Code=@Code)), 0)
                DECLARE @sParents nvarchar(200) SET @sParents=''
                declare @id int
                declare cursor1 cursor for
                SELECT ID FROM DrugsBase_Enterprise WHERE @Parents like '%§'+Code+'§%'
                open cursor1
                fetch next from cursor1 into @id
                while @@fetch_status=0
                begin
	                SET @sParents=@sParents+CAST(@id AS nvarchar)+','
	                fetch next from cursor1 into @id
                end
                close cursor1
                deallocate cursor1
                if(@sParents='' and @ParentNames<>'')
                begin
                declare cursor1 cursor for
                SELECT ID FROM DrugsBase_Enterprise WHERE @ParentNames like '%§'+Name+'§%'
                open cursor1
                fetch next from cursor1 into @id
                while @@fetch_status=0
                begin
	                SET @sParents=@sParents+CAST(@id AS nvarchar)+','
	                fetch next from cursor1 into @id
                end
                close cursor1
                deallocate cursor1
                end
                if(@sParents<>'')SET @sParents=SUBSTRING(@sParents,1,LEN(@sParents)-1)
                SET @Parents=@sParents
                set @ParentId1=ISNULL((SELECT TOP(1) ID FROM DrugsBase_Enterprise WHERE Code=@ParentId),0)
                if(@ParentId1=0 and @ParentName<>'') set @ParentId1=ISNULL((SELECT TOP(1) ID FROM DrugsBase_Enterprise WHERE Name=@ParentName),0)

                if(@UID=0) 
                begin 
                insert into memberaccount(UserType, MobilePhone, Email, PassWord, State, PeriodOfValidity, CompanyClass)
                values (@UserType, @MobilePhone, @Email, @PassWord, @State, '2999/12/30', @CompanyClass)
                SELECT @UID=SCOPE_IDENTITY()
                insert into memberinfo(UID, Code, TrueName, Province, City, Borough, 
                Address, OfficePhone, HandPhone, Fax, QQ, ParentId, Parents, Member_Type, Member_Class)
                values (@UID, @Code, @TrueName, ISNULL((SELECT TOP(1) ID FROM Region WHERE Depth=1 AND Name=@Province),0), ISNULL((SELECT TOP(1) ID FROM Region WHERE Depth=2 AND Name=@City),0), ISNULL((SELECT TOP(1) ID FROM Region WHERE Depth=3 AND Name=@Borough AND ParentId=(SELECT TOP(1) ID FROM Region WHERE Depth=2 AND Name=@City)),0), 
                @Address, @OfficePhone, @HandPhone, @Fax, @QQ, @ParentId1, @Parents, @Member_Type, @Member_Class)
                if(exists(SELECT TOP(1) adminid FROM yxs_administrators WHERE name=@kpy)) update memberinfo set Editer=(SELECT TOP(1) adminid FROM yxs_administrators WHERE name=@kpy) where UID=@UID  
                insert into memberpermission(UID, IsTrade, IsLookStock, IsLookPrice_01, IsLookPrice_02, IsLookProduct_01, IsLookProduct_02, IsPeriodicalSettle, IsMoneyAndShipping, IsCOD, IsPriorDistribution, IsShippingFor48h, IsSpecialTrade)
                values (@UID, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0)
                set @UID_BuyFilingStatus=(SELECT TOP(1) m.UID FROM memberinfo AS m INNER JOIN DrugsBase_Enterprise AS e ON m.ParentId=e.ID where m.UID=@UID AND e.BuyFilingStatus=1)
                update memberpermission set IsTrade=@IsTrade, IsLookStock=@IsLookStock, IsLookPrice_01=@IsLookPrice_01, IsLookPrice_02=@IsLookPrice_02, 
                IsLookProduct_01=@IsLookProduct_01, IsLookProduct_02=@IsLookProduct_02, IsPeriodicalSettle=@IsPeriodicalSettle, IsMoneyAndShipping=@IsMoneyAndShipping, IsCOD=@IsCOD, IsPriorDistribution=@IsPriorDistribution, IsShippingFor48h=@IsShippingFor48h, IsSpecialTrade=@IsSpecialTrade
                where UID=@UID_BuyFilingStatus
                set @inserted=@UID
                end 
                else 
                begin 
                update memberaccount set UserType=@UserType,MobilePhone=@MobilePhone,Email=@Email,CompanyClass=@CompanyClass 
                where UID=@UID 
                set @ParentId0=(SELECT TOP(1) ParentId FROM memberinfo where UID=@UID)
                update memberinfo set TrueName=@TrueName,OfficePhone=@OfficePhone,HandPhone=@HandPhone,Fax=@Fax,Address=@Address, 
                Province=ISNULL((SELECT TOP(1) ID FROM Region WHERE Depth=1 AND Name=@Province),0),City=ISNULL((SELECT TOP(1) ID FROM Region WHERE Depth=2 AND Name=@City),0),Borough=ISNULL((SELECT TOP(1) ID FROM Region WHERE Depth=3 AND Name=@Borough AND ParentId=(SELECT TOP(1) ID FROM Region WHERE Depth=2 AND Name=@City)),0),
                QQ=@QQ,Member_Class=@Member_Class,ParentId=@ParentId1,Parents=@Parents,Code=@Code   
                where UID=@UID 
                if(exists(SELECT TOP(1) adminid FROM yxs_administrators WHERE name=@kpy)) update memberinfo set Editer=(SELECT TOP(1) adminid FROM yxs_administrators WHERE name=@kpy) where UID=@UID  
                set @UID_BuyFilingStatus=(SELECT TOP(1) m.UID FROM memberinfo AS m INNER JOIN DrugsBase_Enterprise AS e ON m.ParentId=e.ID where m.UID=@UID AND e.BuyFilingStatus=1)
                if(@ParentId1=0)
                begin 
                update memberpermission set IsTrade=0, IsLookStock=0, IsLookPrice_01=0, IsLookPrice_02=0, 
                IsLookProduct_01=0, IsLookProduct_02=0, IsPeriodicalSettle=0, IsMoneyAndShipping=1, IsCOD=0, IsPriorDistribution=0, IsShippingFor48h=0 
                where UID=@UID
                end 
                else
                begin 
                update memberpermission set IsTrade=@IsTrade, IsLookStock=@IsLookStock, IsLookPrice_01=@IsLookPrice_01, IsLookPrice_02=@IsLookPrice_02, 
                IsLookProduct_01=@IsLookProduct_01, IsLookProduct_02=@IsLookProduct_02, IsPeriodicalSettle=@IsPeriodicalSettle, IsMoneyAndShipping=@IsMoneyAndShipping, IsCOD=@IsCOD, IsPriorDistribution=@IsPriorDistribution, IsShippingFor48h=@IsShippingFor48h, IsSpecialTrade=@IsSpecialTrade
                where UID=@UID AND ISNULL(@UID_BuyFilingStatus,0)>0 AND UID=(SELECT TOP(1) UID FROM memberaccount where UID=@UID AND State=1)
                update memberaccount set State=0 
                where UID=@UID AND ISNULL(@UID_BuyFilingStatus,0)>0 AND @IsDel=0
                end 
                set @updated=@UID 
                end 
                end 
                select ISNULL(@inserted,0),ISNULL(@updated,0),ISNULL(@UID_BuyFilingStatus,0),ISNULL(@ParentId0,0),ISNULL(@ParentId1,0),(SELECT TOP(1) State FROM memberaccount where UID=@UID)";

            string admin = "admin";
            string err = "";
            DataSet ds = db.ExecuteDataSet(sql);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0/* && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0*/)
            {

                DataRow dr = ds.Tables[0].Rows[0];
                string MobilePhone = Convert.ToString(dr["MobilePhone"]).Trim();
                string kehsx1 = Convert.ToString(dr["kehsx1"]).Trim();
                if (kehsx1 != "一级")
                {
                    return;
                }
                //判断是否应该删除企业库中的数据（在修改了一二级类型的情况可能企业库中会有冗余数据）
                if (kehsx1 == "一级")
                {
                    SOSOshop.Model.DrugsBase_Enterprise model = new Model.DrugsBase_Enterprise();
                    var ebll = new SOSOshop.BLL.DrugsBase_Enterprise();
                    model = ebll.GetModel(Code);
                    if (model != null)
                    {
                        SOSOshop.BLL.Logs.RecycleLog rbll = new Logs.RecycleLog();
                        rbll.created = DateTime.Now;
                        rbll.ObjName = "DrugsBase_Enterprise";
                        rbll.Value = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                        rbll.Insert();//将删除的数据记录下来
                        ebll.Delete(model.ID);
                    }
                }
                if (!string.IsNullOrEmpty(MobilePhone) && (kehsx1 == "一级" || kehsx1 == "一二级")
                    && !string.IsNullOrEmpty(Convert.ToString(dr["Province"]).Trim()) && !string.IsNullOrEmpty(Convert.ToString(dr["City"]).Trim()))
                {
                    admin = Convert.ToString(dr["admin"]).Trim();
                    string beactive = Convert.ToString(dr["beactive"]).Trim();
                    Db db2 = new Db();
                    DbCommand dbCommand = db2._db.GetSqlStringCommand(sql2);
                    string lb = Convert.ToString(dr["Member_Class"]).Trim();
                    string CompanyClass = lb.Split(sep)[0];
                    int Member_Class = -1;//0,批发；1,OTC
                    string Crm_Class = "";//Crm分类
                    lb = SetErp_KeHuLB(lb, ref Member_Class, ref Crm_Class);
                    if (Member_Class == -1 && (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0))
                    {
                        DataRow dr2 = ds.Tables[1].Rows[0];
                        lb = Convert.ToString(dr2["Member_Class"]).Trim();
                        lb = SetErp_KeHuLB(lb, ref Member_Class, ref Crm_Class);
                    }
                    if (Member_Class == -1)
                    {
                        err = "同步商城买家" + Code + "失败，未知客户分类[未选择类别]！";
                    }
                    else
                    {
                        int Member_Type = 1;
                        int UserType = 1;
                        string TrueName = Convert.ToString(dr["TrueName"]).Trim();
                        string PassWord = Guid.NewGuid().ToString("D").Substring(0, 6);
                        db2._db.AddInParameter(dbCommand, "Code", DbType.String, Code);
                        db2._db.AddInParameter(dbCommand, "TrueName", DbType.String, TrueName);
                        db2._db.AddInParameter(dbCommand, "MobilePhone", DbType.String, MobilePhone);
                        db2._db.AddInParameter(dbCommand, "Email", DbType.String, Convert.ToString(dr["Email"]).Trim());
                        db2._db.AddInParameter(dbCommand, "OfficePhone", DbType.String, Convert.ToString(dr["OfficePhone"]).Trim());
                        db2._db.AddInParameter(dbCommand, "HandPhone", DbType.String, Convert.ToString(dr["HandPhone"]).Trim());
                        db2._db.AddInParameter(dbCommand, "Fax", DbType.String, Convert.ToString(dr["Fax"]).Trim());
                        db2._db.AddInParameter(dbCommand, "Address", DbType.String, Convert.ToString(dr["Address"]).Trim());
                        db2._db.AddInParameter(dbCommand, "Province", DbType.String, Convert.ToString(dr["Province"]).Trim());
                        db2._db.AddInParameter(dbCommand, "City", DbType.String, Convert.ToString(dr["City"]).Trim());
                        db2._db.AddInParameter(dbCommand, "Borough", DbType.String, Convert.ToString(dr["Borough"]).Trim());
                        db2._db.AddInParameter(dbCommand, "QQ", DbType.String, Convert.ToString(dr["QQ"]).Trim());
                        db2._db.AddInParameter(dbCommand, "Member_Class", DbType.Int32, Member_Class);
                        db2._db.AddInParameter(dbCommand, "Member_Type", DbType.Int32, Member_Type);
                        db2._db.AddInParameter(dbCommand, "UserType", DbType.Int32, UserType);
                        db2._db.AddInParameter(dbCommand, "kpy", DbType.String, Convert.ToString(dr["kpy"]).Trim());

                        string ParentId = kehsx1 == "一二级" ? Code : ""; string ParentName = "";
                        string Parents = kehsx1 == "一二级" ? sep + Code + sep : ""; string ParentNames = "";
                        int Level = 1;
                        if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                        {
                            Level = 2;
                            ParentId = ""; Parents = "";
                            foreach (DataRow dr2 in ds.Tables[1].Rows)
                            {
                                Parents += sep + dr2[0].ToString();
                                ParentNames += sep + dr2[1].ToString();
                            }
                            ParentId = ds.Tables[1].Rows[0][0].ToString();
                            ParentName = ds.Tables[1].Rows[0][1].ToString();
                            Parents += sep;
                            ParentNames += sep;
                        }
                        db2._db.AddInParameter(dbCommand, "ParentId", DbType.String, ParentId);
                        db2._db.AddInParameter(dbCommand, "Parents", DbType.String, Parents);
                        db2._db.AddInParameter(dbCommand, "ParentName", DbType.String, ParentName);
                        db2._db.AddInParameter(dbCommand, "ParentNames", DbType.String, ParentNames);
                        db2._db.AddInParameter(dbCommand, "State", DbType.Int32, Member_Class == -1 ? 1 : 0);
                        db2._db.AddInParameter(dbCommand, "PassWord", DbType.String, ChangeHope.Common.DEncryptHelper.Encrypt(PassWord, 1));
                        db2._db.AddInParameter(dbCommand, "CompanyClass", DbType.String, CompanyClass);
                        //权限
                        db2._db.AddInParameter(dbCommand, "IsTrade", DbType.Int32, Member_Class == -1 ? 0 : 1);
                        db2._db.AddInParameter(dbCommand, "IsLookStock", DbType.Int32, 0);
                        db2._db.AddInParameter(dbCommand, "IsLookPrice_01", DbType.Int32, Member_Class == -1 ? 0 : (Member_Class == 0 ? 1 : 0));//批发的批发价格查看权限
                        db2._db.AddInParameter(dbCommand, "IsLookPrice_02", DbType.Int32, Member_Class == -1 ? 0 : (Member_Class == 1 ? 1 : 0));//OTC的OTC拆零价格查看权限
                        db2._db.AddInParameter(dbCommand, "IsLookProduct_01", DbType.Int32, Member_Class == -1 ? 0 : (Member_Class == 0 ? 1 : 0));//批发的批发商品查看权限
                        db2._db.AddInParameter(dbCommand, "IsLookProduct_02", DbType.Int32, Member_Class == -1 ? 0 : (Member_Class == 1 ? 1 : 0));//OTC的OTC拆零商品查看权限
                        db2._db.AddInParameter(dbCommand, "IsPeriodicalSettle", DbType.Int32, 0);
                        db2._db.AddInParameter(dbCommand, "IsMoneyAndShipping", DbType.Int32, Member_Class == -1 ? 0 : (Member_Class == 0 ? 1 : 0));//批发款到发货权限
                        db2._db.AddInParameter(dbCommand, "IsCOD", DbType.Int32, Member_Class == -1 ? 0 : (Member_Class == 1 ? 1 : 0));//OTC货到付款权限
                        db2._db.AddInParameter(dbCommand, "IsPriorDistribution", DbType.Int32, 0);//批发和OTC都无有货先发的权限，批发可以以后设置，而otc则不能设置
                        db2._db.AddInParameter(dbCommand, "IsShippingFor48h", DbType.Int32, 0);
                        db2._db.AddInParameter(dbCommand, "IsSpecialTrade", DbType.Int32, 0);
                        //ERP删除后
                        bool IsDel = Code.StartsWith("del", StringComparison.CurrentCultureIgnoreCase) || beactive != "是";
                        db2._db.AddInParameter(dbCommand, "IsDel", DbType.Int32, IsDel ? 1 : 0);
                        db2._db.AddInParameter(dbCommand, "DelCode", DbType.String, Code.Substring(3));

                        int inserted = 0; int updated = 0; int state = 0;
                        int UID_BuyFilingStatus = 0;//买家现建档通过的单位
                        int ParentId0 = 0;//买家之前所有单位
                        int ParentId1 = 0;//买家现在所有单位
                        DataSet dsResult = db2._db.ExecuteDataSet(dbCommand);
                        if (dsResult != null && dsResult.Tables.Count > 0 && dsResult.Tables[dsResult.Tables.Count - 1].Rows.Count > 0)
                        {
                            int.TryParse(dsResult.Tables[dsResult.Tables.Count - 1].Rows[0][0].ToString(), out inserted);
                            int.TryParse(dsResult.Tables[dsResult.Tables.Count - 1].Rows[0][1].ToString(), out updated);
                            int.TryParse(dsResult.Tables[dsResult.Tables.Count - 1].Rows[0][2].ToString(), out UID_BuyFilingStatus);
                            int.TryParse(dsResult.Tables[dsResult.Tables.Count - 1].Rows[0][3].ToString(), out ParentId0);
                            int.TryParse(dsResult.Tables[dsResult.Tables.Count - 1].Rows[0][4].ToString(), out ParentId1);
                            int.TryParse(dsResult.Tables[dsResult.Tables.Count - 1].Rows[0][5].ToString(), out state);
                        }
                        if (inserted <= 0 && updated <= 0)
                        {
                            if (!IsDel)
                            {
                                err = "同步商城买家" + Code + "失败！";
                            }
                        }
                        else//添加或编辑的买家
                        {
                            int UID = inserted; if (updated > 0) UID = updated;
                            DataSet ds3 = db2.ExecuteDataSet("select ParentId,TrueName,MobilePhone,OfficePhone,Fax,Email,Address from memberaccount a inner join memberinfo b on a.UID=b.UID where a.UID=" + UID);
                            if (ds3 != null && ds3.Tables.Count > 0 && ds3.Tables[0].Rows.Count > 0)
                            {
                                DataRow dr3 = ds3.Tables[0].Rows[0];
                                int incId = Convert.ToInt32(dr3[0]);//单位ID
                                if (0 >= incId)
                                {
                                    if (Level == 2) err = "同步CRM买家" + Code + "失败，未知单位！";
                                }
                                else
                                {
                                    bool crm_ok = CrmActionHandle(UID, incId, Crm_Class, Convert.ToString(dr3[1]), Convert.ToString(dr3[2]), Convert.ToString(dr3[3]), Convert.ToString(dr3[4]), Convert.ToString(dr3[5]), Convert.ToString(dr3[6]), admin);
                                    if (!crm_ok)
                                    {
                                        err = "同步CRM买家" + Code + "失败！";
                                    }
                                    else if (inserted > 0)//添加的买家
                                    {
                                        if (state != 2/*未冻结状态下发送短信*/) Sms.SendAndSaveDataBase(MobilePhone, "尊敬的" + TrueName + "，恭喜您的首营资料已经通过审核，请凭手机号登录，立即畅享医药电商的网上采购快感", "系统", MobilePhone);
                                    }
                                }
                                //权限【拥有快捷开通交易的权限】, 第一次建档状态.通过 > 允许已经建档通过的会员的定单可以执行流程
                                SOSOshop.BLL.MemberPermission pbll = new SOSOshop.BLL.MemberPermission();
                                SOSOshop.Model.MemberPermission c = pbll.GetModel(UID);
                                //拥有快捷开通交易的权限
                                if (updated > 0 && UID_BuyFilingStatus > 0 && c.IsSpecialTrade && !Code.StartsWith("del", StringComparison.CurrentCultureIgnoreCase))
                                {
                                    SOSOshop.BLL.Order.Orders obll = new SOSOshop.BLL.Order.Orders();
                                    obll.LetOrders2(Code, "一级单位");
                                    if (state != 2/*未冻结状态下发送短信*/) Sms.SendAndSaveDataBase(MobilePhone, "尊敬的" + TrueName + "，恭喜您的首营资料已经通过审核，请凭手机号登录，立即畅享医药电商的网上采购快感", "系统", MobilePhone);
                                }
                                //单位有更换
                                else if (updated > 0 && ParentId0 != ParentId1 && (ParentId0 > 0 || ParentId1 > 0) && !Code.StartsWith("del", StringComparison.CurrentCultureIgnoreCase))
                                {
                                    if (ParentId0 > 0 && (ParentId1 == 0 || UID_BuyFilingStatus == 0))//删除或取消建档单位
                                    {

                                    }
                                    else if (ParentId0 == 0 && ParentId1 > 0 && UID_BuyFilingStatus > 0)//添加单位
                                    {
                                        if (state != 2/*未冻结状态下发送短信*/) Sms.SendAndSaveDataBase(MobilePhone, "尊敬的" + TrueName + "，恭喜您的首营资料已经通过审核，请凭手机号登录，立即畅享医药电商的网上采购快感", "系统", MobilePhone);
                                    }
                                    else if (ParentId0 > 0 && ParentId1 > 0 && UID_BuyFilingStatus > 0)//单位变更
                                    {
                                        if (state != 2/*未冻结状态下发送短信*/) Sms.SendAndSaveDataBase(MobilePhone, "尊敬的" + TrueName + "，恭喜您的首营资料已经通过审核，请凭手机号登录，立即畅享医药电商的网上采购快感", "系统", MobilePhone);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(MobilePhone))
                    {
                        //err = "同步商城买家" + Code + "失败，未知手机号！";
                    }
                    if (!(kehsx1 == "一级" || kehsx1 == "一二级"))
                    {
                        err = "同步商城买家" + Code + "失败，未知买家[一级或一二级]！";
                    }
                    if (string.IsNullOrEmpty(Convert.ToString(dr["Province"]).Trim()) || string.IsNullOrEmpty(Convert.ToString(dr["City"]).Trim()))
                    {
                        err = "同步商城买家" + Code + "失败，未知地区[未选择省市]！";
                    }
                }
                if (err != "") SOSOshop.BLL.Logs.Log.LogServiceAdd(err, 0, "", "往来单位消息处理1", "同步商城买家" + Code + "失败！", 0);
            }
        }
        /// <summary>
        /// 同步CRM
        /// </summary>
        /// <param name="UID">买家UID</param>
        /// <param name="ParentId">单位ID</param>
        /// <param name="Style">客户分类</param>
        /// <param name="TrueName">买家姓名</param>
        /// <param name="MobilePhone">手机号</param>
        /// <param name="OfficePhone">电话</param>
        /// <param name="Fax">传真</param>
        /// <param name="Email">邮箱</param>
        /// <param name="Address">地址</param>
        /// <param name="admin">管理员</param>
        /// <param name="ParentEnterpriseName">单位名称</param>
        /// <param name="RegionID">区域ID</param>
        /// <returns></returns>
        private static bool CrmActionHandle(int UID, int ParentId, string Style, string TrueName, string MobilePhone, string OfficePhone, string Fax, string Email, string Address, string admin, string ParentEnterpriseName = "", int RegionID = 0)
        {
            Db db = new Db();
            Db crm_db = new Db();
            crm_db.ChangeDB("ConnectionStringCRM");
            bool ok = false;
            if (ParentId <= 0) return ok;
            SOSOshop.BLL.DrugsBase_Enterprise bll = new SOSOshop.BLL.DrugsBase_Enterprise();
            if (!bll.Exists(ParentId)) return ok;
            if (string.IsNullOrEmpty(ParentEnterpriseName) || RegionID <= 0)
            {
                SOSOshop.Model.DrugsBase_Enterprise enterprise = bll.GetModel(ParentId);
                if (enterprise == null) return ok;
                if (string.IsNullOrEmpty(ParentEnterpriseName))
                {
                    ParentEnterpriseName = enterprise.Name;
                }
                if (RegionID <= 0)
                {
                    if (enterprise.Province != null && enterprise.Province > 0) RegionID = (int)enterprise.Province;
                    if (enterprise.City != null && enterprise.City > 0) RegionID = (int)enterprise.City;
                    if (enterprise.Borough != null && enterprise.Borough > 0) RegionID = (int)enterprise.Borough;
                }
            }
            //crmAction 1:将买家信息添加到CRM
            if (!ok)
            {
                int uid = UID; object obj = null;
                string Modifyer = admin.ToString();
                string CreatorTypeID = "";//工作员工
                string Department = "";//部门
                DataSet ds = crm_db.ExecuteDataSet("SELECT TypeID,Department FROM CRM_Employee WHERE Name='" + Modifyer + "'");
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    CreatorTypeID = ds.Tables[0].Rows[0][0].ToString();
                    Department = ds.Tables[0].Rows[0][1].ToString();
                }
                //客户类别
                if (string.IsNullOrEmpty(Style))
                {
                    int orderEditer = 0; int.TryParse(Convert.ToString(db.ExecuteScalar("SELECT TOP(1) Editer FROM orders WHERE ReceiverId=" + UID + " ORDER BY ID DESC")), out orderEditer);
                    int adminId = orderEditer;//线上
                    if (orderEditer == 38)//彭宴
                    {
                        if (CreatorTypeID == "") CreatorTypeID = "0000000000";//工作员工
                        if (Department == "") Department = "001006";//部门,网络运营部
                        Style = "000010000300005";//客户\个体客户
                    }
                    else
                    {
                        string adminRole = "";
                        ds = db.ExecuteDataSet("SELECT name,role FROM yxs_administrators WHERE name='" + admin + "'");
                        obj = (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 ? (ds.Tables[0].Rows[0].IsNull(0) ? null : ds.Tables[0].Rows[0][0]) : null);
                        if (obj != null) adminRole = ds.Tables[0].Rows[0][1].ToString();
                        if (("," + adminRole + ",").Contains(",33,"))//交易员
                        {
                            if (CreatorTypeID == "") CreatorTypeID = "0000000000";//工作员工
                            if (Department == "") Department = "001001";//部门,交易中心
                            if (0 < orderEditer)
                            {
                                Style = "00001000030000200004";//客户\批发企业\成交客户
                            }
                            else
                            {
                                Style = "00001000030000200005";//客户\批发企业\意向客户
                            }
                        }
                        else//其他
                        {
                            Modifyer = "蒋淮";
                            if (CreatorTypeID == "") CreatorTypeID = "0000000002";
                            if (Department == "") Department = "001";//部门,全部
                            Style = "00001000030000200003";//客户\批发企业\目标客户
                        }
                    }
                }
                //确认是买家
                string Style1 = "0000100003"; if (Style.Replace("0", "").Trim() == "" || !Style.StartsWith(Style1)) return ok;
                string sqlString = "DECLARE @ID int,@TypeID varchar(30) ";
                sqlString += "SELECT TOP(1) @ID=ID,@TypeID=TypeID FROM CRM_Interunit WHERE Name=@Name AND Style LIKE '" + Style1 + "%' ";
                sqlString += "IF (@ID<=0 OR @TypeID IS NULL) ";
                sqlString += "BEGIN INSERT INTO CRM_Interunit (TypeID, SerialNumber, CreateDate, ModifyDate, CreatorTypeID, Name, Telephone, Fax, Email, Description, Style, ";
                sqlString += "Classification1, Classification2, Classification3, Field1, Field2, Field3, Field4, Field5, Department, DepartmentRange, ";
                sqlString += "Classification4, Classification5, Dbl1, Dbl2, Dbl3, Dbl4, Field6, Field7, Field8, Field9, Field10, Field11, Field12, Field13, ";
                sqlString += "Field14, Field15, Field16, Field17, Field18, Field19, Field20, Field21, Field22, Field23, Field24, Field25, IsDeleted, ";
                sqlString += "Department1, Department1Range, Department2, Department2Range, Department3, Department3Range, Department4, Department4Range, ";
                sqlString += "IsSigned, Classification4Name_DESCRIPTION, InterunitNamePy, InterunitExpField1, InterunitExpField2, InterunitExpField3, ";
                sqlString += "Modifyer, IsAudit, Field26, Field27, Field28, Field29, Field30, Field31, Field32, Field33, Field34, Field35, ";
                sqlString += "updateTime, IsShare, ShareStarTime, ShareEndTime, CoIsShare) ";
                sqlString += "VALUES ('','',GETDATE(),GETDATE(),@CreatorTypeID,@Name,@Telephone,@Fax,@Email,'',@Style,";
                sqlString += "0,0,@Classification3,@Address,'','','','',@Department,0,";
                sqlString += "0,0,0,0,0,0,'','','','','','准确','','','','','','','','','','','','','','',0,";
                sqlString += "'',0,'',0,'',0,'',0,";
                sqlString += "1,'','','','','',";
                sqlString += "@Modifyer,1,'','','','','','','','','','',";
                sqlString += "GETDATE(),0,GETDATE(),GETDATE(),0) ";
                sqlString += "SELECT @ID=SCOPE_IDENTITY(),@TypeID=(replicate('0', 10 - len(cast(@ID as varchar))) + cast(@ID as varchar)) ";
                sqlString += "UPDATE CRM_Interunit SET TypeID=@TypeID,SerialNumber=('W-' + replicate('0', 6 - len(cast(@ID as varchar))) + cast(@ID as varchar)) WHERE ID=@ID END ";
                sqlString += "ELSE BEGIN UPDATE CRM_Interunit SET ModifyDate=GETDATE(),CreatorTypeID=@CreatorTypeID,Telephone=@Telephone,Fax=@Fax,Email=@Email,Style=@Style,Classification3=@Classification3,Field1=@Address,Department=@Department,Modifyer=@Modifyer,IsDeleted=0 WHERE ID=@ID END ";
                sqlString += "IF (NOT EXISTS(SELECT TOP(1) * FROM CRM_LinkMan WHERE Name=@TrueName AND InterunitTypeID=@TypeID)) ";
                sqlString += "BEGIN INSERT INTO CRM_LinkMan (TypeID, InterunitTypeID, CreatorTypeID, CreateDate, ModifyDate, Name, Sex, BirthDay, Job, Department, ";
                sqlString += "OfficePhone, Fax, OfficeEMail, PersonEMail, MailAddress, Address, PostalCode, HomePhone, Call, MobilePhone, ";
                sqlString += "MimiPhone, Description, DefaultLinkMan, DepartmentTypeID, Classification1, QQNum, Instrist, Hometown, Field1, ";
                sqlString += "Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, Field10, Field11, Field12, Field13, Field14, Field15, Field16, ";
                sqlString += "Field17, Field18, Field19, Field20, IsSL, Inactive) ";
                sqlString += "VALUES ('',@TypeID,@CreatorTypeID,GETDATE(),GETDATE(),@TrueName,'Male','1900/1/1 0:00:00','','',";
                sqlString += "@Telephone1,@Fax1,@Email1,@Email1,@Address,@Address,'','','',@MobilePhone,";
                sqlString += "'','',0,'',0,'','','','','','','','','','','','','','','','','','','','','','','',1,0) ";
                sqlString += "SELECT @ID=SCOPE_IDENTITY() UPDATE CRM_LinkMan SET TypeID=(replicate('0', 10 - len(cast(@ID as varchar))) + cast(@ID as varchar)) WHERE ID=@ID END ";
                sqlString += "ELSE BEGIN UPDATE CRM_LinkMan SET ModifyDate=GETDATE(),CreatorTypeID=@CreatorTypeID,OfficePhone=@Telephone1,Fax=@Fax1,OfficeEMail=@Email1,PersonEMail=@Email1,MailAddress=@Address,Address=@Address,MobilePhone=@MobilePhone WHERE Name=@TrueName AND InterunitTypeID=@TypeID END ";
                sqlString += "SELECT TOP(1) ID FROM CRM_LinkMan WHERE Name=@TrueName AND InterunitTypeID=@TypeID ";
                sqlString += "";

                DbCommand dbCommand = crm_db._db.GetSqlStringCommand(sqlString);
                crm_db._db.AddInParameter(dbCommand, "Name", DbType.AnsiString, ParentEnterpriseName + " " + TrueName);
                crm_db._db.AddInParameter(dbCommand, "TrueName", DbType.AnsiString, TrueName);
                crm_db._db.AddInParameter(dbCommand, "MobilePhone", DbType.AnsiString, MobilePhone);
                crm_db._db.AddInParameter(dbCommand, "Telephone", DbType.AnsiString, OfficePhone);
                crm_db._db.AddInParameter(dbCommand, "Fax", DbType.AnsiString, Fax);
                crm_db._db.AddInParameter(dbCommand, "Email", DbType.AnsiString, Email);
                crm_db._db.AddInParameter(dbCommand, "Address", DbType.AnsiString, Address);
                crm_db._db.AddInParameter(dbCommand, "Department", DbType.AnsiString, Department);
                crm_db._db.AddInParameter(dbCommand, "Style", DbType.AnsiString, Style);
                crm_db._db.AddInParameter(dbCommand, "CreatorTypeID", DbType.AnsiString, CreatorTypeID);
                crm_db._db.AddInParameter(dbCommand, "Modifyer", DbType.AnsiString, Modifyer);
                crm_db._db.AddInParameter(dbCommand, "Telephone1", DbType.AnsiString, OfficePhone);
                crm_db._db.AddInParameter(dbCommand, "Fax1", DbType.AnsiString, Fax);
                crm_db._db.AddInParameter(dbCommand, "Email1", DbType.AnsiString, Email);
                crm_db._db.AddInParameter(dbCommand, "Classification3", DbType.Int32, RegionID);

                obj = crm_db._db.ExecuteScalar(dbCommand);
                if (obj != null)
                {
                    db.ExecuteNonQuery(string.Format("UPDATE memberinfo SET CRMID = {0} WHERE UID = {1}", obj, UID));
                    ok = true;
                }
            }
            return ok;
        }
        public static string CrmActionHandle(string Code, string ParentCode, string CompanyClass, string TrueName, string MobilePhone, string OfficePhone, string Fax, string Email, string Address, string admin, string ParentEnterpriseName, string Region)
        {
            if (string.IsNullOrEmpty(Code) || string.IsNullOrEmpty(ParentCode)
                || Code.StartsWith("del", StringComparison.CurrentCultureIgnoreCase)
                || ParentCode.StartsWith("del", StringComparison.CurrentCultureIgnoreCase)) return "";
            Db db = new Db();
            Db crm_db = new Db();
            crm_db.ChangeDB("ConnectionStringCRM");
            int UID = 0; int RegionID = 0;
            if (!string.IsNullOrEmpty(Region))
            {
                object objRegion = db.ExecuteScalar("SELECT ID FROM Region WHERE Depth=3 AND Name='" + Region + "'");
                if (objRegion == null) objRegion = db.ExecuteScalar("SELECT ID FROM Region WHERE Depth=2 AND Name='" + Region + "'");
                int.TryParse(Convert.ToString(objRegion), out RegionID);
            }
            if (RegionID <= 0)
            {
                SOSOshop.BLL.MemberInfo bll = new SOSOshop.BLL.MemberInfo();
                if (!bll.Exists(Code)) return "CrmWebService同步买家【" + Code + "】时，未选择区域！";
                SOSOshop.Model.MemberInfo model = bll.GetModel(Code);
                if (model != null)
                {
                    UID = model.UID;
                    if (model.Province != null && model.Province > 0) RegionID = (int)model.Province;
                    if (model.City != null && model.City > 0) RegionID = (int)model.City;
                    if (model.Borough != null && model.Borough > 0) RegionID = (int)model.Borough;
                }
            }
            //crmAction 1:将买家信息添加到CRM
            if (UID <= 0)
            {
                object objUser = db.ExecuteScalar("SELECT UID FROM memberinfo WHERE Code='" + Code + "'");
                //if (objUser == null) return "CrmWebService同步买家【" + Code + "】时，未知买家！";
                int.TryParse(Convert.ToString(objUser), out UID);
            }
            string Modifyer = admin.ToString();
            string CreatorTypeID = "";//工作员工
            string Department = "";//部门
            DataSet ds = crm_db.ExecuteDataSet("SELECT TypeID,Department FROM CRM_Employee WHERE Name='" + Modifyer + "'");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                CreatorTypeID = ds.Tables[0].Rows[0][0].ToString();
                Department = ds.Tables[0].Rows[0][1].ToString();
            }
            //客户类别
            string Style = "";
            int Member_Class = -1; SetErp_KeHuLB(CompanyClass, ref Member_Class, ref Style);
            if (string.IsNullOrEmpty(Style))
            {
                return "CrmWebService同步买家【" + Code + "】时，未知买家类别！";
            }
            //确认是买家
            string Style1 = "0000100003"; if (Style.Replace("0", "").Trim() == "" || !Style.StartsWith(Style1)) return "CrmWebService同步买家【" + Code + "】时，未知买家类别！";
            string sqlString = "DECLARE @ID int,@TypeID varchar(30) ";
            sqlString += "SELECT TOP(1) @ID=ID,@TypeID=TypeID FROM CRM_Interunit WHERE Name=@Name AND Style LIKE '" + Style1 + "%' ";
            sqlString += "IF (@ID<=0 OR @TypeID IS NULL) ";
            sqlString += "BEGIN INSERT INTO CRM_Interunit (TypeID, SerialNumber, CreateDate, ModifyDate, CreatorTypeID, Name, Telephone, Fax, Email, Description, Style, ";
            sqlString += "Classification1, Classification2, Classification3, Field1, Field2, Field3, Field4, Field5, Department, DepartmentRange, ";
            sqlString += "Classification4, Classification5, Dbl1, Dbl2, Dbl3, Dbl4, Field6, Field7, Field8, Field9, Field10, Field11, Field12, Field13, ";
            sqlString += "Field14, Field15, Field16, Field17, Field18, Field19, Field20, Field21, Field22, Field23, Field24, Field25, IsDeleted, ";
            sqlString += "Department1, Department1Range, Department2, Department2Range, Department3, Department3Range, Department4, Department4Range, ";
            sqlString += "IsSigned, Classification4Name_DESCRIPTION, InterunitNamePy, InterunitExpField1, InterunitExpField2, InterunitExpField3, ";
            sqlString += "Modifyer, IsAudit, Field26, Field27, Field28, Field29, Field30, Field31, Field32, Field33, Field34, Field35, ";
            sqlString += "updateTime, IsShare, ShareStarTime, ShareEndTime, CoIsShare) ";
            sqlString += "VALUES ('','',GETDATE(),GETDATE(),@CreatorTypeID,@Name,@Telephone,@Fax,@Email,'',@Style,";
            sqlString += "0,0,@Classification3,@Address,'','','','',@Department,0,";
            sqlString += "0,0,0,0,0,0,'','','','','','准确','','','','','','','','','','','','','','',0,";
            sqlString += "'',0,'',0,'',0,'',0,";
            sqlString += "1,'','','','','',";
            sqlString += "@Modifyer,1,'','','','','','','','','','',";
            sqlString += "GETDATE(),0,GETDATE(),GETDATE(),0) ";
            sqlString += "SELECT @ID=SCOPE_IDENTITY(),@TypeID=(replicate('0', 10 - len(cast(@ID as varchar))) + cast(@ID as varchar)) ";
            sqlString += "UPDATE CRM_Interunit SET TypeID=@TypeID,SerialNumber=('W-' + replicate('0', 6 - len(cast(@ID as varchar))) + cast(@ID as varchar)) WHERE ID=@ID END ";
            sqlString += "ELSE BEGIN UPDATE CRM_Interunit SET ModifyDate=GETDATE(),CreatorTypeID=@CreatorTypeID,Telephone=@Telephone,Fax=@Fax,Email=@Email,Style=@Style,Classification3=@Classification3,Field1=@Address,Department=@Department,Modifyer=@Modifyer,IsDeleted=0 WHERE ID=@ID END ";
            sqlString += "IF (NOT EXISTS(SELECT TOP(1) * FROM CRM_LinkMan WHERE Name=@TrueName AND InterunitTypeID=@TypeID)) ";
            sqlString += "BEGIN INSERT INTO CRM_LinkMan (TypeID, InterunitTypeID, CreatorTypeID, CreateDate, ModifyDate, Name, Sex, BirthDay, Job, Department, ";
            sqlString += "OfficePhone, Fax, OfficeEMail, PersonEMail, MailAddress, Address, PostalCode, HomePhone, Call, MobilePhone, ";
            sqlString += "MimiPhone, Description, DefaultLinkMan, DepartmentTypeID, Classification1, QQNum, Instrist, Hometown, Field1, ";
            sqlString += "Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9, Field10, Field11, Field12, Field13, Field14, Field15, Field16, ";
            sqlString += "Field17, Field18, Field19, Field20, IsSL, Inactive) ";
            sqlString += "VALUES ('',@TypeID,@CreatorTypeID,GETDATE(),GETDATE(),@TrueName,'Male','1900/1/1 0:00:00','','',";
            sqlString += "@Telephone1,@Fax1,@Email1,@Email1,@Address,@Address,'','','',@MobilePhone,";
            sqlString += "'','',0,'',0,'','','','','','','','','','','','','','','','','','','','','','','',1,0) ";
            sqlString += "SELECT @ID=SCOPE_IDENTITY() UPDATE CRM_LinkMan SET TypeID=(replicate('0', 10 - len(cast(@ID as varchar))) + cast(@ID as varchar)) WHERE ID=@ID END ";
            sqlString += "ELSE BEGIN UPDATE CRM_LinkMan SET ModifyDate=GETDATE(),CreatorTypeID=@CreatorTypeID,OfficePhone=@Telephone1,Fax=@Fax1,OfficeEMail=@Email1,PersonEMail=@Email1,MailAddress=@Address,Address=@Address,MobilePhone=@MobilePhone WHERE Name=@TrueName AND InterunitTypeID=@TypeID END ";
            sqlString += "SELECT TOP(1) ID FROM CRM_LinkMan WHERE Name=@TrueName AND InterunitTypeID=@TypeID ";
            sqlString += "";

            DbCommand dbCommand = crm_db._db.GetSqlStringCommand(sqlString);
            crm_db._db.AddInParameter(dbCommand, "Name", DbType.AnsiString, ParentEnterpriseName + " " + TrueName);
            crm_db._db.AddInParameter(dbCommand, "TrueName", DbType.AnsiString, TrueName);
            crm_db._db.AddInParameter(dbCommand, "MobilePhone", DbType.AnsiString, MobilePhone);
            crm_db._db.AddInParameter(dbCommand, "Telephone", DbType.AnsiString, OfficePhone);
            crm_db._db.AddInParameter(dbCommand, "Fax", DbType.AnsiString, Fax);
            crm_db._db.AddInParameter(dbCommand, "Email", DbType.AnsiString, Email);
            crm_db._db.AddInParameter(dbCommand, "Address", DbType.AnsiString, Address);
            crm_db._db.AddInParameter(dbCommand, "Department", DbType.AnsiString, Department);
            crm_db._db.AddInParameter(dbCommand, "Style", DbType.AnsiString, Style);
            crm_db._db.AddInParameter(dbCommand, "CreatorTypeID", DbType.AnsiString, CreatorTypeID);
            crm_db._db.AddInParameter(dbCommand, "Modifyer", DbType.AnsiString, Modifyer);
            crm_db._db.AddInParameter(dbCommand, "Telephone1", DbType.AnsiString, OfficePhone);
            crm_db._db.AddInParameter(dbCommand, "Fax1", DbType.AnsiString, Fax);
            crm_db._db.AddInParameter(dbCommand, "Email1", DbType.AnsiString, Email);
            crm_db._db.AddInParameter(dbCommand, "Classification3", DbType.Int32, RegionID);

            object obj = crm_db._db.ExecuteScalar(dbCommand);
            if (obj != null)
            {
                db.ExecuteNonQuery(string.Format("UPDATE memberinfo SET CRMID = {0} WHERE UID = {1}", obj, UID));
                return "";
            }
            return "CrmWebService同步买家【" + Code + "】失败！";
        }
        public static bool CrmActionHandle(int UID)
        {
            Db db = new Db();
            DataSet ds = db.ExecuteDataSet("SELECT b.Code,b.ParentId,b.TrueName,a.MobilePhone,b.OfficePhone,b.Fax,a.Email,b.Address,c.Name AS ParentEnterpriseName,b.Borough AS  RegionID,a.CompanyClass,(SELECT TOP(1) name FROM yxs_administrators WHERE adminid=b.Editer) FROM memberaccount AS a INNER JOIN memberinfo AS b ON a.UID=b.UID LEFT JOIN DrugsBase_Enterprise AS c ON b.ParentId=c.ID WHERE a.UID=" + UID);
            bool ok = false;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                string Code = Convert.ToString(dr[0]);
                int ParentId = 0; int.TryParse(Convert.ToString(dr[1]), out ParentId);
                string TrueName = Convert.ToString(dr[2]); string MobilePhone = Convert.ToString(dr[3]); string OfficePhone = Convert.ToString(dr[4]); string Fax = Convert.ToString(dr[5]); string Email = Convert.ToString(dr[6]); string Address = Convert.ToString(dr[7]);
                string ParentEnterpriseName = Convert.ToString(dr[8]); int RegionID = 0; int.TryParse(Convert.ToString(dr[9]), out RegionID);
                string CompanyClass = Convert.ToString(dr[10]); string admin = Convert.ToString(dr[11]);
                string Style = ""; int Member_Class = -1; SetErp_KeHuLB(CompanyClass, ref Member_Class, ref Style);
                ok = CrmActionHandle(UID, ParentId, Style, TrueName, MobilePhone, OfficePhone, Fax, Email, Address, admin, ParentEnterpriseName, RegionID);
            }
            return ok;
        }
        public static bool CrmActionHandle(string Code)
        {
            Db db = new Db();
            DataSet ds = db.ExecuteDataSet("SELECT b.UID,b.ParentId,b.TrueName,a.MobilePhone,b.OfficePhone,b.Fax,a.Email,b.Address,c.Name AS ParentEnterpriseName,b.Borough AS  RegionID,a.CompanyClass,(SELECT TOP(1) name FROM yxs_administrators WHERE adminid=b.Editer) FROM memberaccount AS a INNER JOIN memberinfo AS b ON a.UID=b.UID LEFT JOIN DrugsBase_Enterprise AS c ON b.ParentId=c.ID WHERE b.Code='" + Code + "'");
            bool ok = false;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                int UID = 0; int.TryParse(Convert.ToString(dr[0]), out UID);
                int ParentId = 0; int.TryParse(Convert.ToString(dr[1]), out ParentId);
                string TrueName = Convert.ToString(dr[2]); string MobilePhone = Convert.ToString(dr[3]); string OfficePhone = Convert.ToString(dr[4]); string Fax = Convert.ToString(dr[5]); string Email = Convert.ToString(dr[6]); string Address = Convert.ToString(dr[7]);
                string ParentEnterpriseName = Convert.ToString(dr[8]); int RegionID = 0; int.TryParse(Convert.ToString(dr[9]), out RegionID);
                string CompanyClass = Convert.ToString(dr[10]); string admin = Convert.ToString(dr[11]);
                string Style = ""; int Member_Class = -1; SetErp_KeHuLB(CompanyClass, ref Member_Class, ref Style);
                ok = CrmActionHandle(UID, ParentId, Style, TrueName, MobilePhone, OfficePhone, Fax, Email, Address, admin, ParentEnterpriseName, RegionID);
            }
            return ok;
        }
    }
}
