using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace ErpShopService
{
    public partial class Service1 : ServiceBase
    {

        public Service1()
        {
            InitializeComponent();

            ////处理订单数据
            BackgroundWorker b2 = new BackgroundWorker();
            b2.DoWork += new DoWorkEventHandler(b2_DoWork);
            b2.RunWorkerAsync();

            //处理会员数据
            BackgroundWorker b3 = new BackgroundWorker();
            b3.DoWork += new DoWorkEventHandler(b3_DoWork);
            b3.RunWorkerAsync();

            //处理会员经营范围
            BackgroundWorker b7 = new BackgroundWorker();
            b7.DoWork += new DoWorkEventHandler(b7_DoWork);
            b7.RunWorkerAsync();
           
            //处理近效期商品
            BackgroundWorker b6 = new BackgroundWorker();
            b6.DoWork += new DoWorkEventHandler(b6_DoWork);
            b6.RunWorkerAsync();
        }
        

        /// <summary>
        /// 订单数据处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void b2_DoWork(object sender, DoWorkEventArgs e)
        {
            BLL.Command bll = new BLL.Command();
            while (true)
            {
                try
                {
                    bll.ImportOrderList();
                    bll.SendOrderStatus();
                }
                catch (Exception ex)
                {
                    bll.AddLog(ex.ToString());
                }
                System.Threading.Thread.Sleep(1000 * 60 * int.Parse(System.Configuration.ConfigurationManager.AppSettings["orderTime"]));
            }
        }

        /// <summary>
        /// 会员数据处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void b3_DoWork(object sender, DoWorkEventArgs e)
        {
            BLL.Command bll = new BLL.Command();
            while (true)
            {
                try
                {
                    bll.SendMemberinfo();
                }
                catch (Exception ex)
                {
                    bll.AddLog(ex.ToString());
                }
                System.Threading.Thread.Sleep(1000 * 60 * int.Parse(System.Configuration.ConfigurationManager.AppSettings["memberTime"]));
            }
        }


       

      
        /// <summary>
        /// 近效期产品处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void b6_DoWork(object sender, DoWorkEventArgs e)
        {
            BLL.Command bll = new BLL.Command();
            while (true)
            {
                try
                {
                    bll.SyncExpritationProduct();
                    
                }
                catch (Exception ex)
                {
                    bll.AddLog(ex.ToString());
                }
                System.Threading.Thread.Sleep(1000 * 60 * int.Parse(System.Configuration.ConfigurationManager.AppSettings["ExpirationTimeProduct"]));
            }
        }

        /// <summary>
        /// 会员经营范围处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void b7_DoWork(object sender, DoWorkEventArgs e)
        {
            BLL.Command bll = new BLL.Command();
            while (true)
            {
                try
                {
                    bll.SyncMemberBusinessScope();

                }
                catch (Exception ex)
                {
                    bll.AddLog(ex.ToString());
                }
                System.Threading.Thread.Sleep(1000 * 60 * int.Parse(System.Configuration.ConfigurationManager.AppSettings["ExpirationTimeProduct"]));
            }
        }
        protected override void OnStart(string[] args)
        {

        }

        protected override void OnStop()
        {

        }


    }
}
