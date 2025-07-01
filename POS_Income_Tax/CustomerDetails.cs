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
using System.Xml.Linq;

namespace POS_Income_Tax
{
    public partial class CustomerDetails : Form
    {

        SqlConnection con = new SqlConnection(Properties.Settings.Default.con);
        public CustomerDetails()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Customer_Delete customer = new Customer_Delete();
            customer.Show();
            this.Hide();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void btnSalesDetails_Click(object sender, EventArgs e)
        {
            
            
            string name = txtname.Text;
            string email = txtemail.Text;
            string phone = txtphone.Text;

            if (name != "" && email != "" && phone != "")
            {
                con.Open();
                string querry = "INSERT INTO [Customeer] (name, phone_number, email) VALUES ('" + name + "','" + phone + "','" + email + "')";
                SqlCommand cmd = new SqlCommand(querry, con);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.ExecuteNonQuery();
                MessageBox.Show(" Customer Account is Created ");
                con.Close();
                txtname.Text = "";
                txtemail.Text = "";
                txtphone.Text = "";
            }
            else
            {
                MessageBox.Show("Some Fields Are Empty ");
            }
            
            grid_data_call();
        }
        private void grid_data_call()
        {
            //dataGridView1.Enabled = false;
            //dataGridView1.Columns[0].Visible= false;
            con.Open();
            string query = "SELECT * FROM [Customeer]";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void CustomerDetails_Load(object sender, EventArgs e)
        {
            grid_data_call();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            customer_update update = new customer_update();
            update.Show();
            this.Hide();

        }
    }
}
