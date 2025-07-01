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
using System.Collections;
using System.Diagnostics;

namespace POS_Income_Tax
{
    public partial class Sales_Details : Form
    {

        SqlConnection con = new SqlConnection(Properties.Settings.Default.con);
        public Sales_Details()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();

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

        private void customer()
        {
            string query = "SELECT customer_id, name FROM Customeer";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cobcustomer.DataSource = dt;
            cobcustomer.DisplayMember = "name";
            cobcustomer.ValueMember = "customer_id";
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

        private void Sales_Details_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = false;
            customer();
            category(); 
            cobcategory.SelectedIndexChanged += cobcategory_SelectedIndexChanged;
            cobsubcategory.SelectedIndexChanged += cobsubcategory_SelectedIndexChanged;
        }
        int quen, total_amount, tax, net_amount , stock;
        private void button1_Click(object sender, EventArgs e)
        {
            cobcustomer.Enabled = false;
            int customer_id = Convert.ToInt32(cobcustomer.SelectedValue);
            int model_id = Convert.ToInt32(cobmodel.SelectedValue);
            quen = Convert.ToInt32(quentity.Value.ToString());
            total_amount = Convert.ToInt32(labtotal_price.Text);
            tax = Convert.ToInt32(labtax_amount.Text);
            net_amount = Convert.ToInt32(labnet_amount.Text);


            string query = "SELECT Product.product_id,Category.name,Subcategory.name,Model.name,Product.price,Product.tax,Product.stock,Model.specification FROM Model,Product,Subcategory ,Category WHERE Product.model_id= '" + model_id + "' AND Product.model_id=Model.model_id AND Model.subcategory_id=Subcategory.subcategory_id AND Subcategory.category_id = Category.category_id ";
            //string query = "SELECT * FROM Product WHERE model_id = '"+model_id+"'";
            SqlDataAdapter da = new SqlDataAdapter(query, con);

            DataTable dt = new DataTable();
            da.Fill(dt);
             if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Your Model do not have any product or Out of Stock please Make product of this model");
            }
            else
            {
                stock = Convert.ToInt32(dt.Rows[0]["stock"].ToString());
                dt.Rows[0]["stock"] = (stock - quen).ToString();

                if (!dt.Columns.Contains("quantity"))
                {
                    dt.Columns.Add("quantity", typeof(int));

                }
                if (stock <= 0 || stock < quen) {
                    MessageBox.Show("Stock is Out of Order");
                    }
                else
                {
                    dt.Rows[0]["quantity"] = quen;
                    stock = Convert.ToInt32(dt.Rows[0]["stock"].ToString());
                    total_amount += (Convert.ToInt32(dt.Rows[0]["price"].ToString())) * quen;
                    tax += (Convert.ToInt32(dt.Rows[0]["tax"].ToString())) * quen;
                    net_amount = total_amount + tax;
                    labtotal_price.Text = total_amount.ToString();
                    labtax_amount.Text = tax.ToString();
                    labnet_amount.Text = net_amount.ToString();

                    AppendData(dt);
                    dataGridView1.DataSource = mainTable;

                }
            }
        }
        DataTable mainTable = new DataTable();

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void cobcategory_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (mainTable.Columns.Count != 0)
            {
                
                string date = dateTimePicker1.Value.ToString();
                int customer_id = Convert.ToInt32(cobcustomer.SelectedValue);
                int user_id = Login.userId;

                con.Open();
                string querry = "INSERT INTO [Sales] (sale_date, total_amount ,tax_amount,net_amount,customer_id ,user_id) VALUES ('" + date + "','" + total_amount + "','" + tax + "','" + net_amount + "' ,'" + customer_id + "','" + user_id + "')";
                SqlCommand cmd = new SqlCommand(querry, con);
                cmd.Parameters.AddWithValue("@total_amount", total_amount);
                cmd.Parameters.AddWithValue("@tax", tax);
                cmd.Parameters.AddWithValue("@net_amount", net_amount);
                cmd.ExecuteNonQuery();
                

                string sales_id_query = "SELECT TOP 1 sale_id FROM Sales ORDER BY sale_id DESC";
                SqlCommand cmdd = new SqlCommand(sales_id_query, con);
                object result = cmdd.ExecuteScalar();
                int SaleId = Convert.ToInt32(result);

                foreach (DataRow row in mainTable.Rows)
                {
                    int row_price = Convert.ToInt32(row["price"]);
                    int row_tax = Convert.ToInt32(row["tax"]);
                    int row_stock = Convert.ToInt32(row["stock"]);
                    int row_quen = Convert.ToInt32(row["quantity"]);
                    int row_product_id = Convert.ToInt32(row["product_id"]);
                    

                    int total_price = (row_price + row_tax) * row_quen;


                    string sale_item = "INSERT INTO [Sale_item] (quantity ,unit_price ,total_price,product_id,sales_id ,tax_amount) VALUES ('" +row_quen+ "','" + row_price + "','" +total_price+ "','" + row_product_id + "' ,'" + SaleId + "','" +row_tax+ "')";
                    SqlCommand cm = new SqlCommand(sale_item, con);
                    cm.Parameters.AddWithValue("@quantity", row_quen);
                    cm.Parameters.AddWithValue("@unit_price", row_price);
                    cm.Parameters.AddWithValue("@total_price", total_price);
                    cm.Parameters.AddWithValue("@product_id", row_product_id);
                    cm.Parameters.AddWithValue("@sales_id", SaleId);
                    cm.Parameters.AddWithValue("@tax_amount", row_tax);
                    cm.ExecuteNonQuery();

                    string update_product = "UPDATE Product SET stock = '" + row_stock + "' WHERE product_id = '"+ row_product_id + "' ";
                    SqlCommand cmmd = new SqlCommand(update_product, con);
                    cmmd.ExecuteNonQuery();
                }

                 cobcustomer.Enabled = true;
                 labtotal_price.Text = "00";
                 labtax_amount.Text = "00";
                 labnet_amount.Text = "00";
                 

                 con.Close();
                MessageBox.Show("Order is placed ");
                
                mainTable.Clear();

            }
            else
            {
                MessageBox.Show("Please Select any itom for sale");
            }

        }

        void AppendData(DataTable newData)
        {
            if (mainTable.Columns.Count == 0)
            {
                mainTable = newData.Clone(); 
            }
            foreach (DataRow row in newData.Rows)
            {
                mainTable.ImportRow(row);
            }

            dataGridView1.DataSource = null; 
            dataGridView1.DataSource = mainTable;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cobcustomer.Enabled = true;
            labtotal_price.Text = "00";
            labtax_amount.Text = "00";
            labnet_amount.Text = "00";
            mainTable.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["product_id"].Value);

                // Find and delete the row from the DataTable
                foreach (DataRow row in mainTable.Rows)
                {
                    if (Convert.ToInt32(row["product_id"]) == id)
                    {
                        int row_price = Convert.ToInt32(row["price"]);
                        int row_tax= Convert.ToInt32(row["tax"]);
                        int row_stock= Convert.ToInt32(row["stock"]);
                        int row_quen = Convert.ToInt32(row["quantity"]);
                        total_amount -= row_price * row_quen;
                        tax -= row_tax * row_quen;
                        stock = stock + row_quen;
                        net_amount = (total_amount + tax);
                        labtotal_price.Text = total_amount.ToString();
                        labtax_amount.Text = tax.ToString();
                        labnet_amount.Text = net_amount.ToString();
                        row.Delete();  // Marks the row for deletion
                        break;
                    }
            
                }

                mainTable.AcceptChanges(); // Actually removes the row
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = mainTable;
                
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }

        }
    }
}
