using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Income_Tax
{
    public partial class Product_Details : Form
    {
        public Product_Details()
        {
            InitializeComponent();
        }

      

        private void btnUserDetails_Click(object sender, EventArgs e)
        {
            Add_product product = new Add_product();
            product.Show();
            this.Hide();
        }

        private void btnProductDetails_Click(object sender, EventArgs e)
        {
           Product_Delete product_Delete = new Product_Delete();
            product_Delete.Show();
            this.Hide();
        }

        private void btnSalesDetails_Click(object sender, EventArgs e)
        {
            Product_Update product_Update = new Product_Update();
            product_Update.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            product_all product = new product_all();
            product.Show();
            this.Hide();
        }
    }
}
