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
    public partial class customer_update : Form
    {

        SqlConnection con = new SqlConnection(Properties.Settings.Default.con);
        public customer_update()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            CustomerDetails customer = new CustomerDetails();   
            customer.Show();
            this.Hide();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            int id = Convert.ToInt32(dataGridView1.Rows[rowindex].Cells["customer_id"].Value);
            string name = dataGridView1.Rows[rowindex].Cells["name"].Value.ToString();
            string email = dataGridView1.Rows[rowindex].Cells["email"].Value.ToString();
            string phone = dataGridView1.Rows[rowindex].Cells["phone_number"].Value.ToString();

            dataGridView1.EndEdit(); // forcefully commits edit
            string query = "UPDATE [Customeer] SET name ='" + name + "', email ='" + email + "',phone_number ='"+phone+"'  WHERE  customer_id='" + id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
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

        private void customer_update_Load(object sender, EventArgs e)
        {
            grid_data_call();
        }
    }
}
