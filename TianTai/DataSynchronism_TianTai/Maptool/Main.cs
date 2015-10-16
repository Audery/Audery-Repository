using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Maptool.BaseForm;

namespace Maptool
{
    public partial class Main : DevComponents.DotNetBar.Office2007RibbonForm
    {
        public static Maptool.soso.keyValue[] Menu;
        public int iden { get; set; }
        public string idenName { get; set; }

        private string curIdenName = "";

        public void bit_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.RibbonTabItem bit = (DevComponents.DotNetBar.RibbonTabItem)sender;
            iden = int.Parse(bit.Tooltip);
            idenName = bit.Text.Replace("数据映射", "");

            //Yj2015.0302 Add--
            if (curIdenName != idenName)
            {
                curIdenName = idenName;
                CloseForm();
            }
            //--

            ribbonPanel1.Show();
        }
        public Main()
        {
            InitializeComponent();
            ribbonTabItem1.Visible = false;
            Maptool.soso.syntoolSoapClient bll = new Maptool.soso.syntoolSoapClient();
            Menu = bll.GetMenu(Login.authKey);
            if (Menu.Count() > 0)
            {
                iden = Menu[Menu.Count() - 1].id;
                idenName = Menu[Menu.Count() - 1].name.Replace("数据映射", "");
                curIdenName = idenName;
            }
            foreach (var item in Menu)
            {
                DevComponents.DotNetBar.RibbonTabItem bit = new DevComponents.DotNetBar.RibbonTabItem();
                bit.Click += new EventHandler(bit_Click);
                bit.Text = item.name;
                bit.Tooltip = item.id.ToString();
                ribbonControl1.Items.Add(bit, 0);
                bit.Select();
            }
            ribbonPanel1.Show();
            this.WindowState = FormWindowState.Maximized;
            this.FormClosed += new FormClosedEventHandler(Main_FormClosed);
            this.Shown += new EventHandler(Main_AutoSizeChanged);

            string powor = new Maptool.soso.syntoolSoapClient().GetPower(Login.authKey);
            Maptool.soso.syntoolSoapClient soso=new soso.syntoolSoapClient();

            if (powor == "limit")
            {
                
                //ribbonBar2.Visible = false;
                //ribbonBar3.Visible = false;
                //ribbonTabItem3.Visible = false;
            }
            else if (powor != "all")
            {
                if (!soso.GetUserPower("12020006", Login.authKey))
                {
                    ribbonBar1.Visible = false;
                }
                if (!soso.GetUserPower("12020007", Login.authKey))
                {
                    ribbonBar2.Visible = false;
                }
                if (!soso.GetUserPower("12020005", Login.authKey))
                {
                    ribbonBar3.Visible = false;
                }
                if (!soso.GetUserPower("12020008", Login.authKey))
                {
                    ribbonBar5.Visible = false;
                }
                if (!soso.GetUserPower("12020009", Login.authKey))
                {
                    ribbonBar6.Visible = false;
                }
                if (!soso.GetUserPower("12020010", Login.authKey))
                {
                    ribbonBar7.Visible = false;
                }
                ribbonTabItem3.Visible = false;
                //ribbonBar4.Visible = false;
                //ribbonBar1.Visible = false;
                //ribbonBar5.Visible = false;
            }
        }
        private void Main_AutoSizeChanged(object sender, EventArgs e)
        {
            // buttonItem1_Click_1(null, null);
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.ExitThread();
        }
        private void buttonItem13_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void buttonItem23_Click(object sender, EventArgs e)
        {
            CloseForm();
            BaseForm.Mapping mp = new BaseForm.Mapping(panel1.Height, iden);
            mp.BinType = 1;
            mp.iden = iden;
            if (panel1.Controls.Count > 0)
            {
                (panel1.Controls[0] as DevComponents.DotNetBar.Office2007Form).Close();
            }
            panel1.Controls.Clear();
            panel1.Controls.Add(mp);
            mp.Bind();
            mp.Show();
        }

        private void buttonItem24_Click(object sender, EventArgs e)
        {
            CloseForm();
            BaseForm.Mapping mp = new BaseForm.Mapping(panel1.Height, iden);
            mp.BinType = 2;
            mp.iden = iden;
            if (panel1.Controls.Count > 0)
            {
                (panel1.Controls[0] as DevComponents.DotNetBar.Office2007Form).Close();
            }
            panel1.Controls.Clear();
            panel1.Controls.Add(mp);
            mp.Bind();
            mp.Show();
        }

        private void buttonItem25_Click(object sender, EventArgs e)
        {
            CloseForm();
            BaseForm.Mapping mp = new BaseForm.Mapping(panel1.Height, iden);
            mp.BinType = 0;
            mp.iden = iden;
            if (panel1.Controls.Count > 0)
            {
                (panel1.Controls[0] as DevComponents.DotNetBar.Office2007Form).Close();
            }
            panel1.Controls.Clear();
            panel1.Controls.Add(mp);
            mp.Bind();
            mp.Show();
        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem8_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 合作企业管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem4_Click_1(object sender, EventArgs e)
        {
            CloseForm();
            BaseForm.ConfigList mp = new BaseForm.ConfigList(panel1.Height);
            mp.BinType = 0;
            mp.iden = iden;
            if (panel1.Controls.Count > 0)
            {
                (panel1.Controls[0] as DevComponents.DotNetBar.Office2007Form).Close();
            }
            panel1.Controls.Clear();
            panel1.Controls.Add(mp);
            mp.Bind();
            mp.Show();
        }

        /// <summary>
        /// 全局配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem7_Click(object sender, EventArgs e)
        {
            ShopForm.GlobalConfig f = new ShopForm.GlobalConfig();
            f.ShowDialog();
        }

        /// <summary>
        /// 释放子窗体
        /// </summary>
        private void CloseForm()
        {
            foreach (Control c in panel1.Controls)
            {
                var t = c as Form;
                t.Close();
            }
        }
        private void buttonItem3_Click(object sender, EventArgs e)
        {
            Config mp = new Config(iden.ToString());
            mp.ShowDialog();
        }

       
        private void buttonItem2_Click(object sender, EventArgs e)
        {
            CloseForm();
            BaseForm.Mapping_Pirce mp = new BaseForm.Mapping_Pirce(panel1.Height, iden);
            mp.BinType = 2;
            mp.iden = iden;
            if (panel1.Controls.Count > 0)
            {
                (panel1.Controls[0] as DevComponents.DotNetBar.Office2007Form).Close();
            }
            panel1.Controls.Clear();
            panel1.Controls.Add(mp);
            mp.Bind();
            mp.Show();
        }

        /// <summary>
        /// 标准化E商数据(中包装\价格\库存含义）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem5_Click(object sender, EventArgs e)
        {
            CloseForm();
            BaseForm.Mapping_Mid mp = new BaseForm.Mapping_Mid(panel1.Height, iden);
            mp.BinType = 1;
            mp.iden = iden;
            if (panel1.Controls.Count > 0)
            {
                (panel1.Controls[0] as DevComponents.DotNetBar.Office2007Form).Close();
            }
            panel1.Controls.Clear();
            panel1.Controls.Add(mp);
            mp.Bind();
            mp.Show();
        }

        private void buttonItem6_Click(object sender, EventArgs e)
        {
            BaseForm.DrugBase db = new DrugBase(panel1.Height, iden,idenName);
            if (panel1.Controls.Count > 0)
            {
                (panel1.Controls[0] as DevComponents.DotNetBar.Office2007Form).Close();
            }
            panel1.Controls.Clear();
            panel1.Controls.Add(db);
            db.Show();
        }

        private void buttonItem7_Click_1(object sender, EventArgs e)
        {
            BaseForm.DrugBaseZyc db = new DrugBaseZyc(panel1.Height, iden, idenName);
            if (panel1.Controls.Count > 0)
            {
                (panel1.Controls[0] as DevComponents.DotNetBar.Office2007Form).Close();
            }
            panel1.Controls.Clear();
            panel1.Controls.Add(db);
            db.Show();
        }

        /// <summary>
        /// 检查销售单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckGoodsUnit_Click(object sender, EventArgs e)
        {
            CloseForm();
            BaseForm.frmCheckData CheckUnit = new BaseForm.frmCheckData(panel1.Height, iden, 2, CheckDataType.销售单位);

            if (panel1.Controls.Count > 0)
            {
                (panel1.Controls[0] as DevComponents.DotNetBar.Office2007Form).Close();
            }
            panel1.Controls.Clear();
            panel1.Controls.Add(CheckUnit);
            CheckUnit.Show();
        }

        /// <summary>
        /// 检查批准文号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckApprovalNumber_Click(object sender, EventArgs e)
        {
            CloseForm();
            BaseForm.frmCheckData CheckApprovalNumber = new BaseForm.frmCheckData(panel1.Height, iden, 2, CheckDataType.批准文号);

            if (panel1.Controls.Count > 0)
            {
                (panel1.Controls[0] as DevComponents.DotNetBar.Office2007Form).Close();
            }
            panel1.Controls.Clear();
            panel1.Controls.Add(CheckApprovalNumber);
            CheckApprovalNumber.Show();
        }

        private void buttonItem9_Click(object sender, EventArgs e)
        {
            //BaseForm.ImageList db = new BaseForm.ImageList();
            BaseForm.ImageList db = new BaseForm.ImageList(panel1.Height, iden, idenName);
            if (panel1.Controls.Count > 0)
            {
                (panel1.Controls[0] as DevComponents.DotNetBar.Office2007Form).Close();
            }
            panel1.Controls.Clear();
            panel1.Controls.Add(db);
            db.Show();
        }

        private void buttonItem10_Click(object sender, EventArgs e)
        {
            BaseForm.NoDrugBase db = new BaseForm.NoDrugBase(panel1.Height, iden, idenName);
            if (panel1.Controls.Count > 0)
            {
                (panel1.Controls[0] as DevComponents.DotNetBar.Office2007Form).Close();
            }
            panel1.Controls.Clear();
            panel1.Controls.Add(db);
            db.Show();
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            CloseForm();
            BaseForm.LiuXiang mp = new BaseForm.LiuXiang(panel1.Height);
            mp.BinType = 2;
            mp.iden = iden;
            if (panel1.Controls.Count > 0)
            {
                (panel1.Controls[0] as DevComponents.DotNetBar.Office2007Form).Close();
            }
            panel1.Controls.Clear();
            panel1.Controls.Add(mp);
            mp.Bind();
            mp.Show();
        }

       
    }
}
