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
using System.Diagnostics;

namespace POS_Income_Tax
{
    public partial class Product_Update : Form
    {

        SqlConnection con = new SqlConnection(Properties.Settings.Default.con);
        public Product_Update()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            Product_Details product_Details = new Product_Details();
            product_Details.Show();
            this.Hide();
        }

        private void Product_Update_Load(object sender, EventArgs e)
        {
            grid_data_call();
        }
        private void grid_data_call()
        {
            //dataGridView1.Enabled = false;
            //dataGridView1.Columns[0].Visible = false;
            con.Open();
            string query = "SELECT Product.product_id,Category.name,Subcategory.name,Model.name,Product.price,Product.tax,Product.stock,Model.specification FROM Model,Product,Subcategory ,Category WHERE Product.model_id=Model.model_id AND Model.subcategory_id=Subcategory.subcategory_id AND Subcategory.category_id = Category.category_id ";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
       

        private void btndelete_Click(object sender, EventArgs e)
        {
            grid_data_call();
        }

        private void dataGridView1_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            int id = Convert.ToInt32(dataGridView1.Rows[rowindex].Cells["product_id"].Value);
            string price = dataGridView1.Rows[rowindex].Cells["price"].Value.ToString();
            string stock = dataGridView1.Rows[rowindex].Cells["stock"].Value.ToString();
            string tax = dataGridView1.Rows[rowindex].Cells["tax"].Value.ToString();

            dataGridView1.EndEdit(); // forcefully commits edit
            string query = "UPDATE [Product] SET price ='" + price + "', stock ='" + stock + "' ,tax ='" + tax + "' WHERE  product_id='" + id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
