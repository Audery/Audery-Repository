using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOSOshop.BLL
{
    /// <summary>
    /// 分类导航-筛选-数据
    /// </summary>
    public class Navigate : DbBase
    {
        /// <summary>
        /// 当前类的缓存依赖项
        /// </summary>
        public const string dependkey = "Navigate_data";

        /// <summary>
        /// 分类筛选-药理药效={id, name}
        /// </summary>
        public Dictionary<string, string> data_ylyx = new Dictionary<string, string>();
        /// <summary>
        /// 分类筛选-适用科室={id, name}
        /// </summary>
        public Dictionary<string, string> data_syks = new Dictionary<string, string>();
        /// <summary>
        /// 分类筛选-厂家数量={id, name}
        /// </summary>
        public Dictionary<string, string> data_cjsl = new Dictionary<string, string>();
        /// <summary>
        /// 分类筛选-价格区间={id, name}
        /// </summary>
        public Dictionary<string, string> data_price = new Dictionary<string, string>();
        /// <summary>
        /// 分类筛选-剂型={id, name}
        /// </summary>
        public Dictionary<string, string> data_jx = new Dictionary<string, string>();

        /// <summary>
        /// 分类导航-筛选-数据
        /// </summary>
        public Navigate()
        {
            //Config/数据-初始化
            DataAdapter_Data(ref data_ylyx, "分类筛选-药理药效"); 
            DataAdapter_Data(ref data_syks, "分类筛选-适用科室"); 
            DataAdapter_Data(ref data_cjsl, "分类筛选-厂家数量");
            DataAdapter_Data(ref data_price, "分类筛选-价格区间");
            DataAdapter_Data(ref data_jx, "分类筛选-剂型");
        }

        protected void DataAdapter_Data(ref Dictionary<string, string> dic, string Data_FilePath)
        {
            string key = "Navigate_" + dic.ToString();
            Dictionary<string, string> obj = GetDepend(key) as Dictionary<string, string>;
            if (obj == null)
            {
                dic = new Dictionary<string, string>();
                string[] data = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("/Config/" + Data_FilePath + ".config")).Split('\n');
                for (int i = 0; i < data.Length; i++) if (data[i].Trim() != "") { string[] item = data[i].Trim().Split("｜|".ToCharArray()); if (!dic.ContainsKey(item[0].Replace("，", ","))) dic.Add(item[0].Replace("，", ","), item[1]); }
                SetDepend(key, obj, dependkey);
            }
            else
            {
                dic = obj;
            }
        }
    }
}
