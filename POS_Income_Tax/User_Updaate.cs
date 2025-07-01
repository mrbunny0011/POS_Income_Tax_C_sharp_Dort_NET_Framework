using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace POS_Income_Tax
{
    public partial class User_Updaate : Form
    {
        SqlConnection con = new SqlConnection(Properties.Settings.Default.con);
        public User_Updaate()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            int id = Convert.ToInt32(dataGridView1.Rows[rowindex].Cells["user_id"].Value);
            string name = dataGridView1.Rows[rowindex].Cells["name"].Value.ToString();
            string email = dataGridView1.Rows[rowindex].Cells["email"].Value.ToString();
            string phone = dataGridView1.Rows[rowindex].Cells["phone_number"].Value.ToString();
            string password = dataGridView1.Rows[rowindex].Cells["password"].Value.ToString();
            string role = dataGridView1.Rows[rowindex].Cells["role"].Value.ToString();

            dataGridView1.EndEdit(); // forcefully commits edit
            string query = "UPDATE [User] SET name ='"+name+"', email ='"+email+"' ,password ='"+password+ "',phone_number ='"+phone+"' ,role ='" + role+"' WHERE  user_id='"+id+"'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void User_Updaate_Load(object sender, EventArgs e)
        {

            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            grid_data_call();
        }

        private void grid_data_call()
        {
            //dataGridView1.Enabled = false;
            //dataGridView1.Columns[0].Visible = false;
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
            UserDetails userDetails = new UserDetails();
            userDetails.Show();
            this.Hide();
        }
    }
}
