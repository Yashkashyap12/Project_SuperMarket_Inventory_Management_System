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

namespace SuperMarket_Management_System
{
    public partial class LoginForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-49L09RTP\SQLEXPRESS;Initial Catalog=SuperMarketInventoryDb;Integrated Security=True;");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Exit Application","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void lblClear_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new SqlCommand("SELECT * FROM tblUser WHERE username=@username AND password=@password",conn);
                cmd.Parameters.AddWithValue("username", txtUsername.Text);
                cmd.Parameters.AddWithValue("password",txtPassword.Text);
                conn.Open();
                dr = cmd.ExecuteReader();
                dr.Read();
                if(dr.HasRows)
                {
                    MessageBox.Show("Welcome  " + dr["fullname"].ToString()+ " | ","ACCESS GRANTED",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    MainForm main = new MainForm();
                    this.Hide();
                    main.ShowDialog();                  
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password!", "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void rdocheckboxPassword_CheckedChanged_1(object sender, EventArgs e)
        {
            {
                if (rdocheckboxPassword.Checked == false)
                    txtPassword.UseSystemPasswordChar = true;
                else
                    txtPassword.UseSystemPasswordChar = false;
            }
        }
    }
}
