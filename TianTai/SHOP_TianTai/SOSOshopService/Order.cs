using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace SOSOshopService
{
    /// <summary>
    /// 订单处理服务
    /// </summary>
    public partial class Order : ServiceBase
    {
        public Order()
        {
            InitializeComponent();
            //定时分单
            backgroundWorker1.RunWorkerAsync();
            SOSOshop.BLL.Logs.Log.LogServiceAdd("商城分单服务启动成功...", 0, "", "MSG", "", 1);

        }
        /// <summary>
        /// 定时分单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {
                    SOSOshop.BLL.Order.Orders bll = new SOSOshop.BLL.Order.Orders();
                    bll.AsySplitOrder();
                }
                catch (Exception ex)
                {
                    SOSOshop.BLL.Logs.Log.LogServiceAdd(ex.Message, 0, "", "定时分单backgroundWorker1_DoWork", ex.ToString(), 2);
                }
                System.Threading.Thread.Sleep(20 * 1000);
            }
        }

    }
}
