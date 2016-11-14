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
    public partial class issue_goods_form : Form
    {
        protected int product_id;

        public issue_goods_form(int pid)
        {
            this.product_id = pid;
            
            InitializeComponent();
            lblProductID.Text = pid.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
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
                string sp = "[ISYS4283].[dbo].[issue_goods_receipt]";

                // new command
                SqlCommand cmd = new SqlCommand(sp, con);
                cmd.CommandType = CommandType.StoredProcedure;

                // add out parameters
                cmd.Parameters.Add("@product", SqlDbType.Int).Value = product_id;
                cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = (int)numericUpDown1.Value;

                // execute
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                throw ex;
            }
            finally
            {
                con.Close();
                Close();
            }
        }
    }
}
