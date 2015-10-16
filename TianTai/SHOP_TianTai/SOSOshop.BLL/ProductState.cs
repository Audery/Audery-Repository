using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOSOshop.BLL
{
    public class ProductState
    {
        /// <summary>
        /// 标识
        /// </summary>
        /// <param name="tb">库状态串</param>
        /// <returns></returns>
        public static Model.ProductState GetModel(string tb)
        {
            Model.ProductState t = new Model.ProductState();
            try
            {
                string temp = tb;
                int[] ts = (from a in temp.Split('|') select int.Parse(a)).ToArray();
                if (ts.Length >= 5)
                {
                    t.IsNew_0 = ts[0];
                    t.QiangGou_1 = ts[1];
                    t.CuXiao_2 = ts[2];
                    t.TuiJian_3 = ts[3];
                    t.Hot_4 = ts[4];                   
                }
                else
                {
                    t.IsNew_0 = 0;
                    t.QiangGou_1 = 0;
                    t.CuXiao_2 = 0;
                    t.TuiJian_3 = 0;
                    t.Hot_4 = 0;     
                }
            }
            catch
            {
                t.IsNew_0 = 0;
                t.QiangGou_1 = 0;
                t.CuXiao_2 = 0;
                t.TuiJian_3 = 0;
                t.Hot_4 = 0;  
            }
            return t;
        }

    }
}
