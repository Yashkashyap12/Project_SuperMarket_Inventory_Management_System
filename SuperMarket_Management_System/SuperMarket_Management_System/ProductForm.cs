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
    public partial class ProductForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-49L09RTP\SQLEXPRESS;Initial Catalog=SuperMarketInventoryDb;Integrated Security=True;");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public ProductForm()
        {
            InitializeComponent();
            LoadProduct();
        }
        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM tblProduct WHERE CONCAT(pid, pname, pprice, pdescription, pcategory) LIKE '%"+txtSearchBox.Text+"%' ", conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }
            dr.Close();
            conn.Close();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductModuleForm formModule = new ProductModuleForm();
            formModule.btnSave.Enabled = true;
            formModule.btnUpdate.Enabled = false;
            formModule.ShowDialog();
            LoadProduct();
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProduct.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                ProductModuleForm formModule = new ProductModuleForm();
                formModule.lblProductId.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                formModule.txtProductName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
                formModule.txtQuantity.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
                formModule.txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
                formModule.txtDescription.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
                formModule.comboCategory.Text = dgvProduct.Rows[e.RowIndex].Cells[6].Value.ToString();

                formModule.btnSave.Enabled = false;
                formModule.btnUpdate.Enabled = true;
                formModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure want to delete this Product?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cmd = new SqlCommand("DELETE FROM tblProduct WHERE pid LIKE'" + dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Record has been successfully deleted!");
                }
            }
            LoadProduct();
        }

        private void txtSearchBox_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }
    }
}
