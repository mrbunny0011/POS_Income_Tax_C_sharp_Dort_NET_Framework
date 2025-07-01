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
    public partial class Login : Form
    {

        SqlConnection con = new SqlConnection(Properties.Settings.Default.con);
        public Login()
        {
            InitializeComponent();
        }
        public static int userId;
        public static string user_role;

        private void btnlogin_Click(object sender, EventArgs e)
        {
            string email = txtemail.Text;
            string password = txtpassword.Text;
            if (email.Equals(""))
            {
                MessageBox.Show("Please Enter Your Email");
            }
            if (password.Equals(""))
            {
                MessageBox.Show("please Enter Your Passsword");
            }
            else
            {
                con.Open();
                string query = "SELECT * FROM [User]";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                bool user = false;
                foreach (DataRow dr in dt.Rows)
                {
                    string dremail = dr["email"].ToString();
                    string drpassword = dr["password"].ToString();
                    if (dremail == email && drpassword == password)
                    {

                        userId = Convert.ToInt32(dr["user_id"].ToString());
                        user_role = dr["role"].ToString();
                        user = true;
                        Home home = new Home();
                        home.Show();
                        this.Hide();

                    }
                }
                    if (!user) 
                    {
                    txtemail.Text = "";
                    txtpassword.Text = "";
                    MessageBox.Show(" Your Email And Password Is Wrong ");
                    }
                    con.Close();
                
            }
        }

        private void txtemail_TextChanged(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
