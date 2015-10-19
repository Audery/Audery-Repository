using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOSOshop.Model.DrugsBase;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace SOSOshop.BLL.Service
{
    /// <summary>
    /// 从数据库中读取基础数据，整理成企业大全需要的数据格式，再保存入库
    /// </summary>
    public class AllEnterprise
    {
        public AllEnterprise() { }


        /// <summary>
        /// 保存企业大全页面需要的数据入库
        /// </summary>
        public void SaveEnterpriseData()
        {
            //省名集合
            List<Region_Model> provinceList = null;
            //城市名集合
            List<Region_Model> cityList = null;

            #region 获取地区集合
            SOSOshop.BLL.DrugsBase.BLL_Region regionBll = new SOSOshop.BLL.DrugsBase.BLL_Region();
            List<Region_Model> regionObjs = regionBll.GetRegionList();
            if (regionObjs != null && regionObjs.Count > 0)
            {
                //删除城市名集合中含香港名称的数据
                Region_Model xg = regionObjs.Find(r => r.Name.Contains("香港") && r.Depth == 2);
                if (xg != null)
                {
                    regionObjs.Remove(xg);
                }

                //删除澳门
                List<Region_Model> aoMen = regionObjs.FindAll(r => r.Name.Contains("澳门"));
                if (aoMen != null && aoMen.Count > 0)
                {
                    foreach (Region_Model a in aoMen)
                    {
                        regionObjs.Remove(a);
                    }
                }

                //插入国外企业
                regionObjs.Add(new Region_Model((int)Enum_OtherRegions.国外企业, "国外企业", 0, 1));

                //获取省名集合
                provinceList = regionObjs.FindAll(province => province.Depth == 1);
                if (provinceList != null && provinceList.Count > 0)
                {
                    //因为北京的Id靠后，所以如此处理
                    Region_Model beiJing = provinceList.Find(r => r.Name.Contains("北京"));
                    if (beiJing != null)
                    {
                        provinceList.Remove(beiJing);
                        provinceList.Insert(0, beiJing);
                    }
                    //插入“所有”
                    provinceList.Insert(0, new Region_Model((int)Enum_OtherRegions.所有, "所有", 0, 1));
                }

                //获取城市名集合
                List<Region_Model> tmpList = regionObjs.FindAll(city => city.Depth == 2);
                if (tmpList != null && tmpList.Count > 0)
                {
                    cityList = tmpList.OrderBy(c => c.ParentId).ToList<Region_Model>();
                }
            }
            #endregion


            #region 获取药厂集合

            //获取药厂大全
            List<DrugsBase_Manufacturer_Model> yaoChangList = null;

            SOSOshop.BLL.DrugsBase.DrugsBase_Manufacturer bll = new SOSOshop.BLL.DrugsBase.DrugsBase_Manufacturer();

            List<DrugsBase_Manufacturer_Model> manufactList = bll.GetManufactInitData();
            if (manufactList != null && manufactList.Count > 0)
            {
                yaoChangList = new List<DrugsBase_Manufacturer_Model>();

                //先搜索药厂名称中含省名的
                if (provinceList != null && provinceList.Count > 0)
                {
                    foreach (Region_Model rObj in provinceList)
                    {
                        List<DrugsBase_Manufacturer_Model> tmp = manufactList.FindAll(e => e.DrugsBase_Manufacturer1.Contains(rObj.Name.Replace("省", "")));
                        if (tmp != null && tmp.Count > 0)
                        {
                            foreach (DrugsBase_Manufacturer_Model ycObj in tmp)
                            {
                                //将省份Id赋值给药厂的省份Id字段
                                ycObj.Province = rObj.ID;

                                //从objs中移除已经搜索过的数据
                                manufactList.Remove(ycObj);
                            }

                            yaoChangList.AddRange(tmp);
                        }
                    }

                }
                //再搜索药厂名称中含城市名的
                if (cityList != null && cityList.Count > 0)
                {
                    //将南京中山单独归类
                    List<DrugsBase_Manufacturer_Model> tmp = manufactList.FindAll(e => e.DrugsBase_Manufacturer1.Contains("南京中山"));
                    if (tmp != null && tmp.Count > 0)
                    {
                        foreach (DrugsBase_Manufacturer_Model ycObj in tmp)
                        {
                            //将城市的父Id赋值给药厂的省份Id字段
                            ycObj.Province = provinceList.Find(p => p.Name.Contains("江苏")).ID;

                            //从objs中移除已经搜索过的数据
                            manufactList.Remove(ycObj);
                        }

                        yaoChangList.AddRange(tmp);
                    }

                    //将成都吉安康单独归类
                    List<DrugsBase_Manufacturer_Model> tmp2 = manufactList.FindAll(e => e.DrugsBase_Manufacturer1.Contains("成都吉安康"));
                    if (tmp2 != null && tmp2.Count > 0)
                    {
                        foreach (DrugsBase_Manufacturer_Model ycObj in tmp2)
                        {
                            //将城市的父Id赋值给药厂的省份Id字段
                            ycObj.Province = provinceList.Find(p => p.Name.Contains("四川")).ID;

                            //从objs中移除已经搜索过的数据
                            manufactList.Remove(ycObj);
                        }

                        yaoChangList.AddRange(tmp2);
                    }

                    //搜索药厂名称中含城市名的
                    foreach (Region_Model cObj in cityList)
                    {
                        List<DrugsBase_Manufacturer_Model> tmp3 = manufactList.FindAll(e => e.DrugsBase_Manufacturer1.Contains(cObj.Name.Replace("市", "")));
                        if (tmp3 != null && tmp3.Count > 0)
                        {
                            foreach (DrugsBase_Manufacturer_Model ycObj in tmp3)
                            {
                                //将城市的父Id赋值给药厂的省份Id字段
                                ycObj.Province = cObj.ParentId;

                                //从objs中移除已经搜索过的数据
                                manufactList.Remove(ycObj);
                            }

                            yaoChangList.AddRange(tmp3);
                        }
                    }
                }

                //剩下药厂名称不含省名和城市名的
                if (manufactList != null && manufactList.Count > 0)
                {
                    foreach (DrugsBase_Manufacturer_Model ycObj in manufactList)
                    {
                        if (ycObj.DrugsBase_Manufacturer1.Contains("西南")
                            || ycObj.DrugsBase_Manufacturer1.Contains("九寨")
                            || ycObj.DrugsBase_Manufacturer1.Contains("洪雅")
                            || ycObj.DrugsBase_Manufacturer1.Contains("广汉"))
                        {
                            Region_Model tmp = provinceList.Find(p => p.Name.Contains("四川"));
                            if (tmp != null)
                            {
                                ycObj.Province = tmp.ID;
                            }
                        }
                        else if (ycObj.DrugsBase_Manufacturer1.Contains("天圣制药"))
                        {
                            Region_Model tmp = provinceList.Find(p => p.Name.Contains("重庆"));
                            if (tmp != null)
                            {
                                ycObj.Province = tmp.ID;
                            }
                        }
                        else if (ycObj.DrugsBase_Manufacturer1.Contains("悦康药业")
                            || ycObj.DrugsBase_Manufacturer1.Contains("朗致集团")
                            || ycObj.DrugsBase_Manufacturer1.Contains("华润双鹤"))
                        {
                            Region_Model tmp = provinceList.Find(p => p.Name.Contains("北京"));
                            if (tmp != null)
                            {
                                ycObj.Province = tmp.ID;
                            }
                        }
                        else if (ycObj.DrugsBase_Manufacturer1.Contains("天士力"))
                        {
                            Region_Model tmp = provinceList.Find(p => p.Name.Contains("天津"));
                            if (tmp != null)
                            {
                                ycObj.Province = tmp.ID;
                            }
                        }
                        else if (ycObj.DrugsBase_Manufacturer1.Contains("滇")
                            || ycObj.DrugsBase_Manufacturer1.Contains("西双版纳"))
                        {
                            Region_Model tmp = provinceList.Find(p => p.Name.Contains("云南"));
                            if (tmp != null)
                            {
                                ycObj.Province = tmp.ID;
                            }
                        }
                        else if (ycObj.DrugsBase_Manufacturer1.Contains("哈药")
                            || ycObj.DrugsBase_Manufacturer1.Contains("哈高科"))
                        {
                            Region_Model tmp = provinceList.Find(p => p.Name.Contains("黑龙江"));
                            if (tmp != null)
                            {
                                ycObj.Province = tmp.ID;
                            }
                        }
                        else if (ycObj.DrugsBase_Manufacturer1.Contains("修正药业"))
                        {
                            Region_Model tmp = provinceList.Find(p => p.Name.Contains("吉林"));
                            if (tmp != null)
                            {
                                ycObj.Province = tmp.ID;
                            }
                        }
                        else if (ycObj.DrugsBase_Manufacturer1.Contains("齐鲁")
                            || ycObj.DrugsBase_Manufacturer1.Contains("莱阳")
                            || ycObj.DrugsBase_Manufacturer1.Contains("辰欣药业")
                            || ycObj.DrugsBase_Manufacturer1.Contains("荣成百合")
                            || ycObj.DrugsBase_Manufacturer1.Contains("蓬莱")
                            || ycObj.DrugsBase_Manufacturer1.Contains("瑞阳制药"))
                        {
                            Region_Model tmp = provinceList.Find(p => p.Name.Contains("山东"));
                            if (tmp != null)
                            {
                                ycObj.Province = tmp.ID;
                            }
                        }
                        else if (ycObj.DrugsBase_Manufacturer1.Contains("亚宝药业")
                            || ycObj.DrugsBase_Manufacturer1.Contains("石药")
                            || ycObj.DrugsBase_Manufacturer1.Contains("国药集团"))
                        {
                            Region_Model tmp = provinceList.Find(p => p.Name.Contains("山西"));
                            if (tmp != null)
                            {
                                ycObj.Province = tmp.ID;
                            }
                        }
                        else if (ycObj.DrugsBase_Manufacturer1.Contains("白云山")
                            || ycObj.DrugsBase_Manufacturer1.Contains("四会")
                            || ycObj.DrugsBase_Manufacturer1.Contains("潮安")
                            || ycObj.DrugsBase_Manufacturer1.Contains("丽珠")
                            || ycObj.DrugsBase_Manufacturer1.Contains("华润三九"))
                        {
                            Region_Model tmp = provinceList.Find(p => p.Name.Contains("广东"));
                            if (tmp != null)
                            {
                                ycObj.Province = tmp.ID;
                            }
                        }
                        else if (ycObj.DrugsBase_Manufacturer1.Contains("扬子江")
                            || ycObj.DrugsBase_Manufacturer1.Contains("常熟")
                            || ycObj.DrugsBase_Manufacturer1.Contains("昆山")
                            || ycObj.DrugsBase_Manufacturer1.Contains("惠氏制药")
                            || ycObj.DrugsBase_Manufacturer1.Contains("阿斯利康")
                            || ycObj.DrugsBase_Manufacturer1.Contains("精华制药"))
                        {
                            Region_Model tmp = provinceList.Find(p => p.Name.Contains("江苏"));
                            if (tmp != null)
                            {
                                ycObj.Province = tmp.ID;
                            }
                        }
                        else if (ycObj.DrugsBase_Manufacturer1.Contains("江中"))
                        {
                            Region_Model tmp = provinceList.Find(p => p.Name.Contains("江西"));
                            if (tmp != null)
                            {
                                ycObj.Province = tmp.ID;
                            }
                        }
                        else if (ycObj.DrugsBase_Manufacturer1.Contains("湘北")
                            || ycObj.DrugsBase_Manufacturer1.Contains("石门")
                            || ycObj.DrugsBase_Manufacturer1.Contains("九芝堂")
                            || ycObj.DrugsBase_Manufacturer1.Contains("康普药业"))
                        {
                            Region_Model tmp = provinceList.Find(p => p.Name.Contains("湖南"));
                            if (tmp != null)
                            {
                                ycObj.Province = tmp.ID;
                            }
                        }
                        else if (ycObj.DrugsBase_Manufacturer1.Contains("马应龙")
                            || ycObj.DrugsBase_Manufacturer1.Contains("华中")
                            || ycObj.DrugsBase_Manufacturer1.Contains("远大医药"))
                        {
                            Region_Model tmp = provinceList.Find(p => p.Name.Contains("湖北"));
                            if (tmp != null)
                            {
                                ycObj.Province = tmp.ID;
                            }
                        }
                        else if (ycObj.DrugsBase_Manufacturer1.Contains("容生制药")
                            || ycObj.DrugsBase_Manufacturer1.Contains("百正药业")
                            || ycObj.DrugsBase_Manufacturer1.Contains("辅仁药业"))
                        {
                            Region_Model tmp = provinceList.Find(p => p.Name.Contains("河南"));
                            if (tmp != null)
                            {
                                ycObj.Province = tmp.ID;
                            }
                        }
                        else if (ycObj.DrugsBase_Manufacturer1.Contains("华北")
                            || ycObj.DrugsBase_Manufacturer1.Contains("涿州")
                            || ycObj.DrugsBase_Manufacturer1.Contains("颈复康")
                            || ycObj.DrugsBase_Manufacturer1.Contains("神威药业")
                            || ycObj.DrugsBase_Manufacturer1.Contains("石药集团")
                            || ycObj.DrugsBase_Manufacturer1.Contains("药都制药")
                            || ycObj.DrugsBase_Manufacturer1.Contains("协和药业"))
                        {
                            Region_Model tmp = provinceList.Find(p => p.Name.Contains("河北"));
                            if (tmp != null)
                            {
                                ycObj.Province = tmp.ID;
                            }
                        }
                        else if (ycObj.DrugsBase_Manufacturer1.Contains("京都念慈菴总厂"))
                        {
                            Region_Model tmp = provinceList.Find(p => p.Name.Contains("香港"));
                            if (tmp != null)
                            {
                                ycObj.Province = tmp.ID;
                            }
                        }
                        else if (ycObj.DrugsBase_Manufacturer1.Contains("先声药业"))
                        {
                            Region_Model tmp = provinceList.Find(p => p.Name.Contains("海南"));
                            if (tmp != null)
                            {
                                ycObj.Province = tmp.ID;
                            }
                        }
                        else if (ycObj.DrugsBase_Manufacturer1.Contains("白求恩医科大学制药厂二分厂"))
                        {
                            Region_Model tmp = provinceList.Find(p => p.Name.Contains("内蒙古"));
                            if (tmp != null)
                            {
                                ycObj.Province = tmp.ID;
                            }
                        }
                        else
                        {
                            if (!IsCN(ycObj.DrugsBase_Manufacturer1)) //全中文的剔除
                            {
                                //将国外企业Id（-1）赋值给药厂的省份Id字段
                                ycObj.Province = (int)Enum_OtherRegions.国外企业;
                            }

                        }
                    }

                    yaoChangList.AddRange(manufactList);
                }

                if (yaoChangList != null && yaoChangList.Count > 0)
                {
                    //计算各省药厂数量
                    if (provinceList != null && provinceList.Count > 0)
                    {
                        foreach (Region_Model rObj in provinceList)
                        {
                            if (rObj.ID == (int)Enum_OtherRegions.所有) //所有
                            {
                                rObj.ManufactNum = yaoChangList.Count;
                            }
                            else
                            {
                                List<DrugsBase_Manufacturer_Model> tmp = yaoChangList.FindAll(e => e.Province == rObj.ID);
                                if (tmp != null && tmp.Count > 0)
                                {
                                    rObj.ManufactNum = tmp.Count;
                                }
                            }
                        }
                    }
                }

                //将地区集合与药厂集合写入数据库
                InsertEnterprise(provinceList, yaoChangList);

            }
            #endregion
        }

        /// <summary>
        /// 保存入库
        /// </summary>
        /// <param name="provinces">省级地区集合</param>
        /// <param name="manufacturers">药厂集合</param>
        private void InsertEnterprise(List<Region_Model> provinces, List<DrugsBase_Manufacturer_Model> manufacturers)
        {
            Database db = DatabaseFactory.CreateDatabase("ConnectionString");

            using (var conn = (SqlConnection)db.CreateConnection())
            {
                conn.Open();
                SqlCommand sqlComm = new SqlCommand();
                sqlComm.Connection = conn;
                SqlTransaction trans = conn.BeginTransaction();
                sqlComm.Transaction = trans;

                try
                {
                    string sql = "";
                    if (provinces != null && provinces.Count > 0)
                    {
                        foreach (Region_Model province in provinces)
                        {
                            sql = string.Format("UPDATE T_Region SET ID={0}, Name='{1}', ParentId={2}, Depth={3}, ManufactNum={4} WHERE ID={0}",
                                  province.ID, province.Name, province.ParentId, province.Depth, province.ManufactNum);

                            sqlComm.CommandText = sql;
                            int i = sqlComm.ExecuteNonQuery();

                            if (i == 0)
                            {
                                sql = string.Format("INSERT INTO T_Region (ID, Name, ParentId, Depth, ManufactNum) VALUES ({0}, '{1}', {2}, {3}, {4})",
                                  province.ID, province.Name, province.ParentId, province.Depth, province.ManufactNum);
                            }

                            sqlComm.CommandText = sql;
                            sqlComm.ExecuteNonQuery();
                        }
                    }

                    if (manufacturers != null && manufacturers.Count > 0)
                    {
                        foreach (DrugsBase_Manufacturer_Model manufacturer in manufacturers)
                        {
                            sql = string.Format("UPDATE T_Manufacturer SET DrugsBase_Manufacturer_ID={0}, DrugsBase_Manufacturer='{1}', Province={2}, City={3}, DrugNumber={4} WHERE DrugsBase_Manufacturer_ID={0}",
                                  manufacturer.DrugsBase_Manufacturer_ID, manufacturer.DrugsBase_Manufacturer1, manufacturer.Province, manufacturer.City, manufacturer.DrugNumber);

                            sqlComm.CommandText = sql;
                            int i = sqlComm.ExecuteNonQuery();

                            if (i == 0)
                            {
                                sql = string.Format("INSERT INTO T_Manufacturer (DrugsBase_Manufacturer_ID, DrugsBase_Manufacturer, Province, City, DrugNumber) VALUES ({0}, '{1}', {2}, {3}, {4})",
                                  manufacturer.DrugsBase_Manufacturer_ID, manufacturer.DrugsBase_Manufacturer1, manufacturer.Province, manufacturer.City, manufacturer.DrugNumber);
                            }

                            sqlComm.CommandText = sql;
                            sqlComm.ExecuteNonQuery();
                        }
                    }

                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }

                conn.Close();
            }

        }

        /// <summary>
        /// 判断字符串是否全中文
        /// </summary>
        /// <param name="strInput">待验证字符串</param>
        /// <returns>true:全中文，false：不是全中文</returns>
        private static bool IsCN(string strInput)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("^[\u4e00-\u9fa5]+$");
            return reg.IsMatch(strInput);
        }


        /// <summary>
        /// 数据库中没有的省级地区分类
        /// </summary>
        public enum Enum_OtherRegions
        {
            国外企业=-1,
            所有=0
        }
    }
}
