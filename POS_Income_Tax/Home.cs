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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            DialogResult ans = MessageBox.Show("You Seriously Want to logOut ", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (ans == DialogResult.Yes)
            {
                Login login = new Login();
                login.Show();
                this.Hide();
            }
           
        }

       

        private void btnSalesDetails_Click(object sender, EventArgs e)
        {
            Sales_Details sales_Details = new Sales_Details();  
            sales_Details.Show();
            this.Hide();
        }

        private void btnUserDetails_Click(object sender, EventArgs e)
        {
            string role = Login.user_role;
            if (role == "Admin")
            {
                this.Hide();
                UserDetails user = new UserDetails();
                user.Show();
            }
            else {
                MessageBox.Show("You have Casher Account that Why You Can not Visit User details");
            }
        }

        private void btnCustomerDetails_Click(object sender, EventArgs e)
        {
            this.Hide();
            CustomerDetails customer = new CustomerDetails();
            customer.Show();
        }

        private void btnProductDetails_Click(object sender, EventArgs e)
        {
            Product_Details details = new Product_Details();    
            details.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
          DialogResult ans =  MessageBox.Show("You Seriously Want to Shut Down Application ", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (ans == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string role = Login.user_role.ToString();
            if (role == "Admin")
            {
                Sales_List sales_List = new Sales_List();
                sales_List.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("You have Casher Account that why you cannot acces it");
            }
            
        }
    }
}
