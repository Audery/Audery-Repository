using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;

namespace Maptool
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //发布帐号 exchange.101yao.com
            //发布密码 exchange!@#exchange
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }
        /// <summary>
        /// 是同步中药材的企业编号
        /// </summary>
        public static int[] iden_in_zyc = { 10003 };
    }

}
