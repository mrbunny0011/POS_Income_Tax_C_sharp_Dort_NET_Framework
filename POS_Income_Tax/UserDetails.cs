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
    public partial class UserDetails : Form
    {
        SqlConnection con = new SqlConnection(Properties.Settings.Default.con);
        public UserDetails()
        {
            InitializeComponent();
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
            string password = txtpassword.Text; 
            string phone = txtphone.Text;
            string role = cbrole.Text;

            if(name != "" && email != "" && password != "")
            {
                con.Open();
                string querry = "INSERT INTO [User] (name, password, phone_number, email ,role) VALUES ('" + name + "','" + password + "','" + phone + "','" + email + "','" + role + "')";
                SqlCommand cmd = new SqlCommand(querry, con);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@role", role);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Account is Created");
                con.Close();
                txtname.Text = "";
                txtemail.Text = "";
                txtpassword.Text = "";
                txtphone.Text = "";
                grid_data_call();
            }
            else
            {
                MessageBox.Show("Input Field Is Empty ");
            }
            
        }

        private void UserDetails_Load(object sender, EventArgs e)
        {
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

            grid_data_call();
        }
        
        private void grid_data_call()
        {
            //dataGridView1.Enabled = false;
            //dataGridView1.Columns[0].Visible= false;
            con.Open();
            string query = "SELECT * FROM [User]";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User_Updaate userupdate = new User_Updaate();
            userupdate.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string role = Login.user_role;
            if (role == "Admin")
            {
                delete_user userdelete = new delete_user();
                userdelete.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("You have Casher Account that Why You Can not Delete Customer Details");
            }
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
