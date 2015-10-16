using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOSOshop.Model
{
    /// <summary>
    /// 会员类型
    /// </summary>
    public class CompanyClass
    {
        /// <summary>
        /// 类型编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string CompanyClassName { get; set; }
        /// <summary>
        /// 此企业类型应该执行的哪个价格
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// 是否执行基药价(如果有)
        /// </summary>
        public bool IsBaseDrg { get; set; }

        /// <summary>
        /// 取得公司类型
        /// </summary>
        /// <returns></returns>
        public static List<CompanyClass> GetList()
        {
            List<CompanyClass> li = new List<CompanyClass>();
            string[] arr = { "批发公司", "零售连锁", "单体药房/诊所", "民营医院", "公立医院" };
            for (int i = 0; i < arr.Length; i++)
            {
                CompanyClass model = new CompanyClass();
                model.ID = i;
                model.CompanyClassName = arr[i];
                model.IsBaseDrg = false;
                li.Add(model);
            }
            li[0].Price = "Price_01";
            li[1].Price = "Price_02";
            li[2].Price = "Price_02";
            li[3].Price = "Price_02";
            li[4].Price = "Price_02";
            

            li[3].IsBaseDrg = false;
            li[4].IsBaseDrg = false;

            return li;
        }
        /// <summary>
        /// 对得会员类型所对应的最价格规则
        /// </summary>
        /// <param name="CompanyClassName"></param>
        /// <returns></returns>
        public static CompanyClass GetModel(string CompanyClassName)
        {
            if (GetList().Where(x => x.CompanyClassName == CompanyClassName).Count() == 0)
            {
                return GetList().Where(x => x.CompanyClassName == "单体药房/诊所").First();
            }
            return GetList().Where(x => x.CompanyClassName == CompanyClassName).First();
        }
    }
}

