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

    public partial class Product_Delete : Form
    {

        SqlConnection con = new SqlConnection(Properties.Settings.Default.con);
        public Product_Delete()
        {
            InitializeComponent();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["product_id"].Value);

                    string query = "DELETE FROM [Product] WHERE product_id = '" + id + "' ";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    grid_data_call();

                    MessageBox.Show("Your Selected row is Delete");

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
        private void grid_data_call()
        {
            //dataGridView1.Enabled = false;
            //dataGridView1.Columns[0].Visible = false;
            con.Open();
            string query = "SELECT Category.name,Subcategory.name,Model.name,Product.price,Product.tax,Product.stock,Model.specification FROM Model,Product,Subcategory ,Category WHERE Product.model_id=Model.model_id AND Model.subcategory_id=Subcategory.subcategory_id AND Subcategory.category_id = Category.category_id ";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            Product_Details product_Details = new Product_Details();    
            product_Details.Show();
            this.Hide();
        }

        private void Product_Delete_Load(object sender, EventArgs e)
        {
            grid_data_call();
        }
    }
}
