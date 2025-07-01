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
    public partial class ModelDetails : Form
    {
        SqlConnection con = new SqlConnection(Properties.Settings.Default.con);
        public ModelDetails()
        {
            InitializeComponent();
        }

        private void btncreatecategory_Click(object sender, EventArgs e)
        {
            string name = txtname.Text;
            string status = txtstatuss.Text;
            string specification = txtspecification.Text;
            int subcategory_id = Convert.ToInt32(cobsubcategory.SelectedValue);

            con.Open();
            string querry = "INSERT INTO [Model] (name, status ,subcategory_id ,specification) VALUES ('"+ name +"','"+ status +"','"+ subcategory_id +"','"+ specification +"')";
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@subcategory_id", subcategory_id);
            cmd.ExecuteNonQuery();
            MessageBox.Show(" Model is Created ");
            con.Close();
            txtname.Text = "";
            txtstatuss.Text = "";
            txtspecification.Text = "";
            grid_data_call();

        }

        private void ModelDetails_Load(object sender, EventArgs e)
        {
            category(); // load categories only
            cobcategory.SelectedIndexChanged += cobcategory_SelectedIndexChanged;

            grid_data_call();
        }
        private void category()
        {
            string query = "SELECT category_id, name FROM Category";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cobcategory.DataSource = dt;
            cobcategory.DisplayMember = "name";
            cobcategory.ValueMember = "category_id";
        }
        public void Sub_category(int category_id)
        {
            string query = "SELECT subcategory_id, name FROM Subcategory WHERE category_id = @category_id";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            da.SelectCommand.Parameters.AddWithValue("@category_id", category_id);

            DataTable dt = new DataTable();
            da.Fill(dt);

            cobsubcategory.DataSource = dt;
            cobsubcategory.DisplayMember = "name";
            cobsubcategory.ValueMember = "subcategory_id";
        }

        private void cobcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobcategory.SelectedValue != null)
            {
                int category_id;
                if (int.TryParse(cobcategory.SelectedValue.ToString(), out category_id))
                {
                    Sub_category(category_id);
                }
            }
        }
        private void grid_data_call()
        {
            //dataGridView1.Enabled = false;
            //dataGridView1.Columns[0].Visible= false;
            con.Open();
            string query = "SELECT * FROM [Model]";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridecategory.DataSource = dt;
            con.Close();
        }

        private void gridecategory_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            int id = Convert.ToInt32(gridecategory.Rows[rowindex].Cells["subcategory_id"].Value);
            string name = gridecategory.Rows[rowindex].Cells["name"].Value.ToString();
            string status = gridecategory.Rows[rowindex].Cells["status"].Value.ToString();
            string specification = gridecategory.Rows[rowindex].Cells["specification"].Value.ToString();


            gridecategory.EndEdit(); // forcefully commits edit
            string query = "UPDATE [Model] SET name ='" + name + "', status ='" + status + "',specification ='"+ specification + "'  WHERE  model_id='" + id + "'";
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

                    int id = Convert.ToInt32(gridecategory.SelectedRows[0].Cells["model_id"].Value);

                    string query = "DELETE FROM [Model] WHERE model_id = '" + id + "' ";
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

        private void btnlogin_Click(object sender, EventArgs e)
        {
            Add_product add_Product = new Add_product();
            add_Product.Show();
            this.Hide();
        }
    }
}
