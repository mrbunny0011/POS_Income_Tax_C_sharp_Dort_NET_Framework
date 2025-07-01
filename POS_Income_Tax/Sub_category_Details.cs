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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace POS_Income_Tax
{
    public partial class Sub_category_Details : Form
    {
        SqlConnection con = new SqlConnection(Properties.Settings.Default.con);
        public Sub_category_Details()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            Add_product add_Product = new Add_product();
            add_Product.Show();
            this.Hide();
        }

        private void btncreatecategory_Click(object sender, EventArgs e)
        {
            string name = txtname.Text;
            string status= txtstatuss.Text;
            int category_id = Convert.ToInt32(cobcategoryid.SelectedValue);

            con.Open();
            string querry = "INSERT INTO [Subcategory] (name, status ,category_id) VALUES ('" + name + "','" + status + "','"+category_id+"')";
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@category_id", category_id);
            cmd.ExecuteNonQuery();
            MessageBox.Show(" Sub_Category is Created ");
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
            string query = "SELECT * FROM [Subcategory]";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridecategory.DataSource = dt;
            con.Close();
        }

        private void Sub_category_Details_Load(object sender, EventArgs e)
        {
            LoadComboBoxWithValue();
            grid_data_call();
        }
        private void LoadComboBoxWithValue()
        {
            string query = "SELECT category_id, name FROM Category";

           
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

            cobcategoryid.DataSource = dt;
            cobcategoryid.DisplayMember = "name"; // Show in dropdown
            cobcategoryid.ValueMember = "category_id";     // Actual value (can get with comboBox1.SelectedValue)
            
        }

        private void gridecategory_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            int id = Convert.ToInt32(gridecategory.Rows[rowindex].Cells["subcategory_id"].Value);
            string name = gridecategory.Rows[rowindex].Cells["name"].Value.ToString();
            string status = gridecategory.Rows[rowindex].Cells["status"].Value.ToString();


            gridecategory.EndEdit(); // forcefully commits edit
            string query = "UPDATE [Subcategory] SET name ='" + name + "', status ='" + status + "'  WHERE  subcategory_id='" + id + "'";
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

                    int id = Convert.ToInt32(gridecategory.SelectedRows[0].Cells["subcategory_id"].Value);

                    string query = "DELETE FROM [Subcategory] WHERE subcategory_id = '" + id + "' ";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    grid_data_call();
                    MessageBox.Show($"Your Category ID : {id} is deleted");
                }catch(Exception ex)
                {
                    MessageBox.Show("Error : ",ex.Message );
                }
            }
            else
            {
                MessageBox.Show("Please Select any row For Delete Operation");
            }
        }

        private void btnupdatecategory_Click(object sender, EventArgs e)
        {

        }

        private void gridecategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtstatuss_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtname_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cobcategoryid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
