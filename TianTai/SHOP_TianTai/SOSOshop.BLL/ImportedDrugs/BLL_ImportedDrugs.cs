using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SOSOshop.Model.DrugsBase;

namespace SOSOshop.BLL.ImportedDrugs
{
    /// <summary>
    /// 进口频道数据访问层
    /// </summary>
    public class BLL_ImportedDrugs : DbBase
    {
        public BLL_ImportedDrugs() { }

        #region 获取进口药生产企业及进口药数量
        /// <summary>
        /// 获取进口药生产企业及进口药数量
        /// </summary>
        /// <returns></returns>
        public List<DrugsBase_Manufacturer_Model> GetImpDrugsManufacts()
        {
            List<DrugsBase_Manufacturer_Model> objs = new List<DrugsBase_Manufacturer_Model>();

            string sql = @"SELECT DISTINCT tt1.drugsBase_Manufacturer_Id,
                                           tt1.drugsBase_Manufacturer,
                                           tt2.drugNum
                                FROM View_ImportedPharm_Manufacturer_Pharm tt1
                            INNER JOIN
                            (   SELECT  
                                        drugsBase_Manufacturer, count(drugsBase_Manufacturer) AS drugNum
                                FROM  (SELECT DISTINCT drugsBase_Manufacturer, drugsBase_Id FROM View_ImportedPharm_Manufacturer_Pharm) t1
                                GROUP BY drugsBase_Manufacturer
                            ) tt2
                            ON tt1.drugsBase_Manufacturer=tt2.drugsBase_Manufacturer";

            DataTable dt = base.ExecuteTableForCache(sql, DateTime.Now.AddHours(1));

            if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
            {
                foreach (DataRow rowObj in dt.Rows)
                {
                    DrugsBase_Manufacturer_Model obj = new DrugsBase_Manufacturer_Model();
                    obj.DrugsBase_Manufacturer_ID = (int)rowObj["DrugsBase_Manufacturer_Id"]; ; //生产企业Id
                    obj.DrugsBase_Manufacturer1 = rowObj["DrugsBase_Manufacturer"].ToString().Trim(); //生产企业名称
                    
                    obj.DrugNumber = (int)rowObj["drugNum"];  //统计数量
                    obj.PYJM = GetChineseSpell(obj.DrugsBase_Manufacturer1);

                    objs.Add(obj);
                }
            }

            List<DrugsBase_Manufacturer_Model> rtnObj = objs.OrderByDescending(b => b.DrugNumber).OrderBy(a => a.PYJM_FirstChar_).ToList<DrugsBase_Manufacturer_Model>();
            return rtnObj;
        }
        #endregion


        #region 指定厂家的二级进口药品分类信息
        /// <summary>
        /// 获取指定厂家的二级进口药品分类
        /// </summary>
        /// <param name="drugsBase_Manufacturer_Id">厂家名称</param>
        /// <returns></returns>
        public List<DrugsPharm> GetImpDrugsPharm(int drugsBase_Manufacturer_Id)
        {
            List<DrugsPharm> rtnObjs = new List<DrugsPharm>();

            List<DrugsPharm> drugsPharms = GetImpDrugsPharmInfo(); //全部药品分类
            List<PharmInfo> secondPharms = GetPharmInfo();  //二级分类的基础信息

            if (drugsPharms != null && drugsPharms.Count > 0 && secondPharms != null && secondPharms.Count > 0)
            {
                foreach (PharmInfo s in secondPharms)
                {
                    //查询药品的分类路径是否包含二级分类的分类路径
                    List<DrugsPharm> tmpDrugs = null;
                    tmpDrugs = drugsPharms.FindAll(d => d.Pharm_ID_Path.Contains(s.Pharm_ID_Path + ((s.Pharm_ID_Path.Substring(s.Pharm_ID_Path.Length - 1, 1) != @"\") ? @"\" : "")));

                    if (tmpDrugs != null && tmpDrugs.Count > 0)
                    {
                        DrugsPharm tmpDrug = new DrugsPharm();
                        tmpDrug.Pharm_ID = s.Pharm_ID;
                        tmpDrug.Pharm_Name = s.Pharm_Name;
                        tmpDrug.Pharm_ID_Path = s.Pharm_ID_Path;

                        //统计分类数量
                        int num = 0;
                        foreach (DrugsPharm d in tmpDrugs)
                        {
                            if (drugsBase_Manufacturer_Id>0) //指定药厂
                            {
                                if (d.DrugsBase_Manufacturer_Id == drugsBase_Manufacturer_Id)
                                {
                                    num++;
                                }
                            }
                            else    //未指定药厂
                            {
                                num++;
                            }
                        }
                        tmpDrug.DrugNumber = num;

                        rtnObjs.Add(tmpDrug);
                    }
                }

            }

            List<DrugsPharm> results = rtnObjs.OrderByDescending(d => d.DrugNumber).ToList<DrugsPharm>();
            return results;
        }

        /// <summary>
        /// 获取全部进口药品的分类信息
        /// </summary>
        /// <returns></returns>
        private List<DrugsPharm> GetImpDrugsPharmInfo()
        {
            List<DrugsPharm> rtnObjs = null;

            string sql = string.Format(@"SELECT drugsBase_Manufacturer_ID,
                                                drugsBase_Manufacturer,
                                                drugsBase_Id,
                                                pharm_id,
                                                pharm_name,
                                                pharm_parent_id,
                                                pharm_id_path 
                                        FROM View_ImportedPharm_Manufacturer_Pharm
                                        ORDER BY drugsBase_Manufacturer
                                    ");

            DataTable dt = base.ExecuteTableForCache(sql, DateTime.Now.AddHours(1));

            if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
            {
                rtnObjs = new List<DrugsPharm>();
                foreach (DataRow rowObj in dt.Rows)
                {
                    //如果该药品Id已添加，则不再添加（因为一种药品的子分类可能有多个）
                    if (rtnObjs.Find(p => (p.DrugsBase_ID == (int)rowObj["DrugsBase_ID"]) && (p.ParentId == (int)rowObj["pharm_parent_id"])) == null)
                    {
                        DrugsPharm obj = new DrugsPharm();
                        obj.DrugsBase_Manufacturer_Id = (int)rowObj["drugsBase_Manufacturer_id"];
                        obj.DrugsBase_Manufacturer = rowObj["drugsBase_Manufacturer"].ToString();
                        obj.DrugsBase_ID = (int)rowObj["drugsBase_Id"];

                        //最后的字符是否“\”，不是则添加。为后续做路径判断做准备。
                        string tmpPath = rowObj["pharm_id_path"].ToString();
                        if (tmpPath.Substring(tmpPath.Length - 1, 1) != @"\")
                        {
                            obj.Pharm_ID_Path = tmpPath + @"\";
                        }
                        else
                        {
                            obj.Pharm_ID_Path = tmpPath;
                        }

                        obj.Pharm_ID = (int)rowObj["pharm_id"];
                        obj.Pharm_Name = rowObj["pharm_name"].ToString();
                        obj.ParentId = (int)rowObj["pharm_parent_id"];
                        //obj.DrugNumber = (int)rowObj["pharmNum"];

                        rtnObjs.Add(obj);
                    }
                }
            }

            return rtnObjs;
        }
        #endregion

        /// <summary>
        /// 获取药品分类信息
        /// </summary>
        /// <param name="pharmLevel">药品分类层级</param>
        /// <returns></returns>
        private List<PharmInfo> GetPharmInfo()
        {
            List<PharmInfo> objs = new List<PharmInfo>();

            string sql = string.Format(@"SELECT Pharm_Id,Pharm_Name,Pharm_Parent_Id,Pharm_Id_Path FROM DrugsBase_Pharm WHERE pharm_level=2");
            DataTable dt = base.ExecuteTableForCache(sql, DateTime.Now.AddHours(1));

            if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
            {
                foreach (DataRow rowObj in dt.Rows)
                {
                    PharmInfo obj = new PharmInfo();
                    obj.Pharm_ID = (int)rowObj["Pharm_Id"];
                    obj.Pharm_Name = rowObj["Pharm_Name"].ToString().Trim();
                    obj.Pharm_Parent_ID = (int)rowObj["Pharm_Parent_Id"];
                    obj.Pharm_ID_Path = rowObj["Pharm_Id_Path"].ToString().Trim();

                    objs.Add(obj);
                }
            }

            return objs;
        }

        /// <summary>
        /// 取得网站图片地址
        /// </summary>
        /// <param name="ImagePath"></param>
        /// <returns></returns>
        private static string GetImagePath(object ImagePath)
        {
            if (Library.Lang.DataValidator.isNULL(ImagePath))
            {
                return "/images/nopic1.jpg";
            }
            else
            {
                return System.Configuration.ConfigurationManager.AppSettings["imageUrl"] + ImagePath;
            }
        }

        /// <summary>
        /// 提取汉字首字母
        /// </summary>
        /// <param name="strText">需要转换的字</param>
        /// <returns>转换结果</returns>
        private static string GetChineseSpell(string strText)
        {
            int len = strText.Length;
            string myStr = "";
            for (int i = 0; i < len; i++)
            {
                myStr += getSpell(strText.Substring(i, 1));
            }
            return myStr;
        }

        /// <summary>
        /// 获取单个汉字的首拼音
        /// </summary>
        /// <param name="myChar">需要转换的字符</param>
        /// <returns>转换结果</returns>
        private static string getSpell(string myChar)
        {
            byte[] arrCN = System.Text.Encoding.Default.GetBytes(myChar);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;

                if (code == 58109)
                {
                    return "K";
                }

                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return System.Text.Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "_";
            }
            else return myChar;
        }
    }
}
