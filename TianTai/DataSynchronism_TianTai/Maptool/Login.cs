using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Maptool
{
    public partial class Login : DevComponents.DotNetBar.Office2007Form
    {
        /// <summary>
        /// 登陆认证字符串
        /// </summary>
        public static string authKey = null;
        public static string userName = null;
        public Login()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            //textBoxX1.Text = "admin";
            //textBoxX2.Text = "101administrator";
            textBoxX2.KeyDown += new KeyEventHandler(textBoxX2_KeyDown);
            backgroundWorker1.RunWorkerAsync();
        }
        private void textBoxX2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonX1_Click(null, null);
            }
        }
        private void buttonX2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            soso.syntoolSoapClient bll = new soso.syntoolSoapClient();
            if (string.IsNullOrEmpty(textBoxX1.Text))
            {
                MessageBox.Show("请输入用户名!", "验证失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(textBoxX2.Text))
            {
                MessageBox.Show("请输入登陆密码!", "验证失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            authKey = bll.Login(textBoxX1.Text, textBoxX2.Text);
            if (!string.IsNullOrEmpty(authKey))
            {
                userName = textBoxX1.Text;
                this.Hide();
                Main main = new Main();
                main.Show();
                this.Hide();
            }
            else
            {

                MessageBox.Show("用户名或密码输入有误!", "验证失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            new soso.syntoolSoapClient().Login("", "");
        }
    }
}
