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
using System.Collections;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace POS_Income_Tax
{
    public partial class Add_product : Form
    {

        SqlConnection con = new SqlConnection(Properties.Settings.Default.con);
        public Add_product()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            Product_Details product = new Product_Details();
            product.Show();
            this.Hide();
        }

        private void btnaddcategory_Click(object sender, EventArgs e)
        {
            CreateCategory createCategory = new CreateCategory();   
            createCategory.Show();
            this.Hide();

        }

        private void btnaddsubcategory_Click(object sender, EventArgs e)
        {
            Sub_category_Details subCategory = new Sub_category_Details();
            subCategory.Show();
            this.Hide();
        }

        private void btnaddmodel_Click(object sender, EventArgs e)
        {
            ModelDetails modal = new ModelDetails();
            modal.Show();
            this.Hide();
        }

        private void Add_product_Load(object sender, EventArgs e)
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

        private void submit_Click(object sender, EventArgs e)
        {
            try
            {
                int category_id = Convert.ToInt32(cobcategory.SelectedValue);
                int subcategory_id = Convert.ToInt32(cobsubcategory.SelectedValue);
                int model_id = Convert.ToInt32(cobmodel.SelectedValue);
                int stock = Convert.ToInt32(txtstock.Text);
                int price = Convert.ToInt32(txtprice.Text);
                int tax = Convert.ToInt32(txttax.Text);

                if (stock != 0 && price != 0 && tax != 0)
                {

                    con.Open();

                    string query = "SELECT * from Product Where model_id = '" + model_id + "'";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    da.SelectCommand.Parameters.AddWithValue("@model_id", model_id);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                        string querry = "INSERT INTO [Product] (stock, price ,tax ,model_id) VALUES ('" + stock + "','" + price + "','" + tax + "','" + model_id + "')";
                        SqlCommand cmd = new SqlCommand(querry, con);
                        cmd.Parameters.AddWithValue("@stock", stock);
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.Parameters.AddWithValue("@tax", tax);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show(" Product is Created ");
                        con.Close();
                        txtstock.Text = "";
                        txttax.Text = "";
                        txtprice.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Product is already Created");
                    }

                }
                else
                {
                    MessageBox.Show("Some Field is Empty");
                }


            }
            catch (Exception ex) { 
                MessageBox.Show(ex.Message);
            }




        }
    }
}
