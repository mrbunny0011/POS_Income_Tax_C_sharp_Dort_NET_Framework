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
    public partial class product_all : Form
    {

        SqlConnection con = new SqlConnection(Properties.Settings.Default.con);
        public product_all()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Product_Details product_Details = new Product_Details();
            product_Details.Show();
            this.Hide();
        }

        private void product_all_Load(object sender, EventArgs e)
        {
            category(); // load categories only
            cobcategory.SelectedIndexChanged += cobcategory_SelectedIndexChanged;
            cobsubcategory.SelectedIndexChanged += cobsubcategory_SelectedIndexChanged;
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

        public void model(int subcategory_id)
        {
            string query = "SELECT model_id, name FROM Model WHERE subcategory_id = @subcategory_id";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            da.SelectCommand.Parameters.AddWithValue("@subcategory_id", subcategory_id);

            DataTable dt = new DataTable();
            da.Fill(dt);

            cobmodel.DataSource = dt;
            cobmodel.DisplayMember = "name";
            cobmodel.ValueMember = "model_id";
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

        private void cobsubcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobsubcategory.SelectedValue != null)
            {
                int subcategory_id;
                if (int.TryParse(cobsubcategory.SelectedValue.ToString(), out subcategory_id))
                {
                    model(subcategory_id);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int category_id = Convert.ToInt32(cobcategory.SelectedValue);
            int subcategory_id = Convert.ToInt32(cobsubcategory.SelectedValue);
            int model_id = Convert.ToInt32(cobmodel.SelectedValue);

            string query = "SELECT Product.product_id,Category.name,Subcategory.name,Model.name,Product.price,Product.tax,Product.stock,Model.specification FROM Model,Product,Subcategory ,Category WHERE Product.model_id= '" + model_id + "' AND Product.model_id=Model.model_id AND Model.subcategory_id=Subcategory.subcategory_id AND Subcategory.category_id = Category.category_id ";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            da.SelectCommand.Parameters.AddWithValue("@subcategory_id", subcategory_id);

            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;

        }
    }
}
