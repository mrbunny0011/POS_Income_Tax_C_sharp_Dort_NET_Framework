using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace POS_Income_Tax
{
    public partial class Customer_Delete : Form
    {

        SqlConnection con = new SqlConnection(Properties.Settings.Default.con);
        public Customer_Delete()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            CustomerDetails customerDetails = new CustomerDetails();
            customerDetails.Show();
            this.Hide();
        }

        private void Customer_Delete_Load(object sender, EventArgs e)
        {
            grid_data_call();
        }
        private void grid_data_call()
        {
            //dataGridView1.Enabled = false;
            //dataGridView1.Columns[0].Visible = false;
            con.Open();
            string query = "SELECT * FROM [Customeer]";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {

                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["customer_id"].Value);

                    string query = "DELETE FROM [Customeer] WHERE customer_id = '" + id + "' ";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    grid_data_call();
                }
                catch (Exception ex) { 
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please Select any row For Delete Operation");
            }
        }
    }
}
