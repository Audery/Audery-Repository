using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Maptool.ShopForm
{
    public partial class GlobalConfig : DevComponents.DotNetBar.Office2007Form
    {
        
        Maptool.soso.syntoolSoapClient bll = new Maptool.soso.syntoolSoapClient();
        public GlobalConfig()
        {
            InitializeComponent();
            var model = bll.GetGlobalConfig();
            textBoxX1.Text = model.Stocklimit.ToString();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            Maptool.soso.GlobalConfig model = new Maptool.soso.GlobalConfig();
            model.Stocklimit = int.Parse(textBoxX1.Text);
            bll.UpdateGlobalConfig(model);
            this.Close();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}
