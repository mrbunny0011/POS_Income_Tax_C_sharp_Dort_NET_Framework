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
namespace POS_Income_Tax
{
    public partial class Sales_List : Form
    {
        SqlConnection con = new SqlConnection(Properties.Settings.Default.con);
        public Sales_List()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void Sales_List_Load(object sender, EventArgs e)
        {
            customer();
            user();
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
        private void user()
        {
            string query = "SELECT user_id, name FROM [User]";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cobuser.DataSource = dt;
            cobuser.DisplayMember = "name";
            cobuser.ValueMember = "user_id";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int user_id = Convert.ToInt32(cobuser.SelectedValue);

            string query = "SELECT Customeer.name,Sales.* from Sales,Customeer Where user_id = '" + user_id+"' AND Sales.customer_id=Customeer.customer_id ";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            da.SelectCommand.Parameters.AddWithValue("@subcategory_id", user_id);

            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int customer_id = Convert.ToInt32(cobcustomer.SelectedValue);

            string query = "SELECT * from Sales,Sale_Item Where Sales.customer_id = '" + customer_id + "' AND Sale_Item.sales_id = Sales.sale_id";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            da.SelectCommand.Parameters.AddWithValue("@subcategory_id", customer_id);

            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        
    }
}
