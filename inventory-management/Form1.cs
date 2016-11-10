using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inventory_management
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void issueGoodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            issue_goods_form ig = new issue_goods_form();
            ig.ShowDialog();
        }

        private void createProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            create_product_form cp = new create_product_form();
            cp.ShowDialog();
        }

        private void editProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            edit_product_form ep = new edit_product_form();
            ep.ShowDialog();
        }
    }
}
