using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _101shop.v3.Models
{
    public class TabModels
    {
        /// <summary>
        /// 首页
        /// </summary>
        public int Home { get;set;}

        /// <summary>
        /// 中标基药
        /// </summary>
        public int DrugBase { get; set; }

        /// <summary>
        /// 热销品种
        /// </summary>
        public int HotSale { get; set; }

        /// <summary>
        /// 空间指数
        /// </summary>
        public int PlaceIndex { get; set; }

        /// <summary>
        /// 热卖
        /// </summary>
        public int Hot { get; set; }

        /// <summary>
        /// 精品
        /// </summary>
        public int Competitive { get; set; }

        /// <summary>
        /// 最新
        /// </summary>
        public int IsNew { get; set; }

        /// <summary>
        /// 可拆零OTC
        /// </summary>
        public int Otc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        public static string[] GetName(string tb)
        {
            TabModels t = tab(tb);            
            if (t.Otc == 1)
            {
                return "1|可拆零|拆".Split('|');
            }
            if (t.IsNew == 1)
            {

                return "2|新品|新".Split('|');
            }
            return null;
        }

        /// <summary>
        /// 标识
        /// </summary>
        /// <param name="tb">库状态串</param>
        /// <returns></returns>
        public static TabModels tab(string tb)
        {
            TabModels t = new TabModels();
            string temp = tb + "0";
            int[] ts = (from a in temp.Split('|') select int.Parse(a)).ToArray();
            t.Home = ts[0];
            t.DrugBase = ts[1];
            t.HotSale = ts[2];
            t.PlaceIndex = ts[3];
            t.Hot = ts[4];
            t.Competitive = ts[5];
            t.IsNew = ts[6];
            return t;
        }
    }


}