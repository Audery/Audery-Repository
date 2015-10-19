using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Memcached.ClientLibrary;
using System.Configuration;

namespace SOSOshopService
{
    /// <summary>
    /// ERP 数据同步服务
    /// </summary>
    public partial class cr_yppf : ServiceBase
    {
        public cr_yppf()
        {
            InitializeComponent();
            SockIOPool pool2 = SockIOPool.GetInstance("Price_Cache");
            string[] serverlist2 = ConfigurationManager.AppSettings["ServerList2"].Split(',');
            pool2.SetServers(serverlist2);
            pool2.SetWeights(new int[] { 1 });
            pool2.InitConnections = 5;
            pool2.MinConnections = 5;
            pool2.MaxConnections = 280;
            pool2.MaxIdle = 1000 * 60 * 60 * 6;
            pool2.SocketTimeout = 1000 * 3;
            pool2.SocketConnectTimeout = 0;
            pool2.SocketTimeout = 3000;
            pool2.MaintenanceSleep = 60;
            pool2.Failover = true;
            pool2.Nagle = false;
            pool2.MaxBusy = 1000 * 10;
            pool2.Initialize();


            backgroundWorker4.RunWorkerAsync();//销售数量处理
            backgroundWorker5.RunWorkerAsync();//处理商品价格            
            SOSOshop.BLL.Logs.Log.LogServiceAdd("商城消息处理服务启动成功...", 0, "", "MSG", "", 1);
        }

        /// <summary>
        /// 销售数量处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void backgroundWorker4_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                System.Threading.Thread.Sleep(1000 * 60 * 60 * 12);//半天
                try
                {

                    Common.Timer.InitStock_Lock();

                }
                catch (Exception ex)
                {
                    SOSOshop.BLL.Logs.Log.LogServiceAdd(ex.Message, 0, "", "销售数量处理", ex.ToString(), 2);
                }
            }
        }
        private int dayWorker5 = 0;//标记今天是否执行重新初始化一次价格
        /// <summary>
        /// 处理商品价格变动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void backgroundWorker5_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {

                //每天晚上重新初始化一次价格
                try
                {
                    if (DateTime.Now.Hour > 2 && dayWorker5 != DateTime.Now.Day)
                    {
                        Common.Timer.InitProduct_SaleNum();
                        //Common.Timer.InitProduct_tag();
                        //Common.Timer.InitDrugsBase_tag();
                        Common.Timer.InitProduct_otc();
                        ///重新计算菜单数量
                        SOSOshop.BLL.Category.Menu.InitData();
                        //更新商品标签
                        SOSOshop.BLL.DbBase db = new SOSOshop.BLL.DbBase();
                        db.ChangeShop();
                        db.ExecuteNonQuery("UPDATE Product SET tag_ids=b.Tag_Ids FROM Product a INNER JOIN dbo.DrugsBase_Tag_Ids b ON a.DrugsBase_ID = b.DrugsBase_Id WHERE a.tag_ids<>b.Tag_Ids and  a.tag_ids<>b.Tag_Ids");
                        //禁止前台显示 含麻制剂（特特药品）,计生类药品,精神药品,蛋白同化制剂
                        db.ExecuteNonQuery("UPDATE Product SET Product_bShelves=0,beactive='删' WHERE tag_ids LIKE('%,81,%') OR tag_ids LIKE('%,82,%') OR tag_ids LIKE('%,4,%') OR tag_ids LIKE('%,5,%') OR tag_ids LIKE('%,6,%') OR tag_ids LIKE('%,89,%') and beactive<>'删'");
                        //更新是否有包装盒
                        db.ExecuteNonQuery(@"UPDATE dbo.Product SET bimage=1 WHERE Goods_ID IN (SELECT Goods_ID FROM dbo.Goods_Image) AND bimage<>1
                                             UPDATE dbo.Product SET bimage=0 WHERE Goods_ID NOT IN (SELECT Goods_ID FROM dbo.Goods_Image) AND bimage<>0", 120);
                        dayWorker5 = DateTime.Now.Day;
                    }
                }
                catch (Exception ex1)
                {
                    dayWorker5 = DateTime.Now.Day;
                    SOSOshop.BLL.Logs.Log.LogServiceAdd(ex1.Message, 0, "", "ERP价格处理", ex1.ToString(), 2);
                }
                System.Threading.Thread.Sleep(1000 * 60 * 1);//1分钟
            }
        }
    }
}

