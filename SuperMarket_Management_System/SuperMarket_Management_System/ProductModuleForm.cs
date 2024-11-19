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
    public partial class ProductModuleForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-49L09RTP\SQLEXPRESS;Initial Catalog=SuperMarketInventoryDb;Integrated Security=True;");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public ProductModuleForm()
        {
            InitializeComponent();
            LoadCategory();
        }
      public void LoadCategory()
        {
            comboCategory.Items.Clear();
            cmd = new SqlCommand("SELECT catname FROM tblCategory",conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read()) 
            {
                comboCategory.Items.Add(dr[0].ToString());
            }
            dr.Close();
            conn.Close();
        }
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure want to save this Product?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO tblProduct(pname,pqty,pprice,pdescription,pcategory)VALUES(@pname,@pqty,@pprice,@pdescription,@pcategory)", conn);
                    cmd.Parameters.AddWithValue("@pname", txtProductName.Text);
                    cmd.Parameters.AddWithValue("@pqty", Convert.ToInt32(txtQuantity.Text));
                    cmd.Parameters.AddWithValue("@pprice", Convert.ToInt32(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@pdescription", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@pcategory",comboCategory.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Product has been successfully saved");
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
            txtProductName.Clear();
            txtQuantity.Clear();
            txtPrice.Clear();
            txtDescription.Clear();
            comboCategory.Text = ""; 
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure want to Update this Product?", "Updating Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("UPDATE tblProduct SET pname = @pname, pqty = @pqty, pprice = @pprice,pdescription = @pdescription,pcategory = @pcategory WHERE pid LIKE '" + lblProductId.Text + "' ", conn);
                    cmd.Parameters.AddWithValue("@pname", txtProductName.Text);
                    cmd.Parameters.AddWithValue("@pqty", Convert.ToInt32(txtQuantity.Text));
                    cmd.Parameters.AddWithValue("@pprice", Convert.ToInt32(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@pdescription",txtDescription.Text);
                    cmd.Parameters.AddWithValue("@pcategory",comboCategory.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Prpduct has been successfully Updated");
                    this.Dispose();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
