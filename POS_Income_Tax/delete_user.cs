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
    public partial class delete_user : Form
    {
        SqlConnection con = new SqlConnection(Properties.Settings.Default.con);
        public delete_user()
        {
            InitializeComponent();
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

        private void delete_user_Load(object sender, EventArgs e)
        {

            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            grid_data_call();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {

            
            if (dataGridView1.SelectedRows.Count > 0) {
                DialogResult ans = MessageBox.Show("You Seriously Want to Delete User", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (ans == DialogResult.Yes)
                {
                    try
                    {
                        int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["user_id"].Value);

                        string query = "DELETE FROM [User] WHERE user_id = '" + id + "' ";
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
                
            }
            else
            {
                MessageBox.Show("Please Select any row For Delete Operation");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserDetails userDetails = new UserDetails();
            userDetails.Show();
            this.Hide();
        }
    }
}
