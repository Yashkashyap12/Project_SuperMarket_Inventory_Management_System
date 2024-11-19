using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMarket_Management_System
{
    public partial class CustomerModuleForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-49L09RTP\SQLEXPRESS;Initial Catalog=SuperMarketInventoryDb;Integrated Security=True;");
        SqlCommand cmd = new SqlCommand();
        public CustomerModuleForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure want to save this Customer?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO tblCustomer(cname,cphone)VALUES(@cname,@cphone)", conn);
                    cmd.Parameters.AddWithValue("@cname", txtCustomerName.Text);
                    cmd.Parameters.AddWithValue("@cphone", txtCustomerPhone.Text);
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
        public void Clear()
        {
            txtCustomerName.Clear();
            txtCustomerPhone.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure want to Update this Customer?", "Updating Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("UPDATE tblCustomer SET cname = @cname, cphone = @cphone WHERE cid LIKE '" + lblCustomerId.Text + "' ", conn);
                    cmd.Parameters.AddWithValue("@cname", txtCustomerName.Text);
                    cmd.Parameters.AddWithValue("@cphone", txtCustomerPhone.Text);                    
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Customer has been successfully Updated");
                    this.Dispose();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
