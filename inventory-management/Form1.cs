using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

        private void viewInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // connection string
            string conStr = ConfigurationManager.ConnectionStrings["ISYS4283"].ConnectionString;

            // create connection
            SqlConnection con = new SqlConnection(conStr);

            try
            {
                // open connection to database server
                con.Open();

                // our SQL
                string sql = "SELECT * FROM [ISYS4283309].[dbo].[view_inventory]";

                // execute query
                SqlDataAdapter a = new SqlDataAdapter(sql, con);

                // make a dataset
                DataSet ds = new DataSet();

                // fill our dataset
                a.Fill(ds, "inventory");

                // set dataset = datagridview
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "inventory";
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
