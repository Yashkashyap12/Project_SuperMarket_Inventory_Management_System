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

namespace SuperMarket_Management_System
{
    public partial class UserModuleForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-49L09RTP\SQLEXPRESS;Initial Catalog=SuperMarketInventoryDb;Integrated Security=True;");
        SqlCommand cmd = new SqlCommand();
        public UserModuleForm()
        {
            InitializeComponent();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtPassword.Text != txtRetypePass.Text) 
                {
                    MessageBox.Show("Password did not Match!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }
                if(MessageBox.Show("Are you sure want to save this user?","Saving Record",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes) 
                {
                    cmd = new SqlCommand("INSERT INTO tblUser(username,fullname,password,phone)VALUES(@username,@fullname,@password,@phone)",conn);
                    cmd.Parameters.AddWithValue("@username",txtUsername.Text);
                    cmd.Parameters.AddWithValue("@fullname",txtFullname.Text);
                    cmd.Parameters.AddWithValue("@password",txtPassword.Text);
                    cmd.Parameters.AddWithValue("@phone",txtPhone.Text);   
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("User has been successfully saved");
                    Clear();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        public void Clear()
        {
            txtUsername.Clear();
            txtFullname.Clear();
            txtPassword.Clear();    
            txtPhone.Clear();
            txtRetypePass.Clear(); 
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPassword.Text != txtRetypePass.Text)
                {
                    MessageBox.Show("Password did not Match!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }   
                if (MessageBox.Show("Are you sure want to Update this user?", "Updating Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("UPDATE tblUser SET fullname = @fullname, password = @password, phone = @phone WHERE username LIKE '"+txtUsername.Text+"' ",conn);
                    cmd.Parameters.AddWithValue("@fullname", txtFullname.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("User has been successfully Updated");
                    this.Dispose();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
