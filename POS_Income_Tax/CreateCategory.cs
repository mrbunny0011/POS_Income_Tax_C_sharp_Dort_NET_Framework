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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Linq.Expressions;

namespace POS_Income_Tax
{
    public partial class CreateCategory : Form
    {

        SqlConnection con = new SqlConnection(Properties.Settings.Default.con);
        public CreateCategory()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            Add_product product = new Add_product();
            product.Show();
            this.Hide();
        }

        private void btncreatecategory_Click(object sender, EventArgs e)
        {
            string name = txtname.Text;
            string status = txtstatuss.Text;

            con.Open();
            string querry = "INSERT INTO [Category] (name, status) VALUES ('" + name + "','" + status + "')";
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.ExecuteNonQuery();
            MessageBox.Show("  Category is Created ");
            con.Close();
            txtname.Text = "";
            txtstatuss.Text = "";
            grid_data_call();

        }
        private void grid_data_call()
        {
            //dataGridView1.Enabled = false;
            //dataGridView1.Columns[0].Visible= false;
            con.Open();
            string query = "SELECT * FROM [Category]";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridecategory.DataSource = dt;
            con.Close();
        }

        private void CreateCategory_Load(object sender, EventArgs e)
        {
            grid_data_call();
        }

        private void gridecategory_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            int id = Convert.ToInt32(gridecategory.Rows[rowindex].Cells["category_id"].Value);
            string name = gridecategory.Rows[rowindex].Cells["name"].Value.ToString();
            string status = gridecategory.Rows[rowindex].Cells["status"].Value.ToString();
    

            gridecategory.EndEdit(); // forcefully commits edit
            string query = "UPDATE [Category] SET name ='" + name + "', status ='" + status + "'  WHERE  category_id='" + id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }

        private void btndeletecategory_Click(object sender, EventArgs e)
        {
            if (gridecategory.SelectedRows.Count > 0)
            {
                try
                {
                    int id = Convert.ToInt32(gridecategory.SelectedRows[0].Cells["category_id"].Value);

                    string query = "DELETE FROM [Category] WHERE category_id = '" + id + "' ";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    grid_data_call();
                    MessageBox.Show($"Your Category ID : {id} is deleted");
                }
                catch (Exception ex) { 
                    MessageBox.Show(ex.Message );
                }

                
            }
            else
            {
                MessageBox.Show("Please Select any row For Delete Operation");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
